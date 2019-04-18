using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

using System.Collections;
using gloPatientSaving;
using gloGlobal;


namespace gloRxPatientSaving
{
    public static class PatSavGeneral
    {
       public static string ConnectionString = string.Empty;
       public static string FilePath = string.Empty;
       public static PatSavDataClassDataContext objDataContext;

       public static string getFileName()
       {
           //string _filePath;
           //DateTime dtDate=DateTime.Now ;
           //Int32 i=0;
           //_filePath = dtDate.ToString("MMddyyyyhhmmssffftt") + ".xml";
           //while (File.Exists(FilePath + _filePath) == true && i!= Int32.MaxValue )
           //{
           //    i=i+1;
           //    _filePath = dtDate.ToString("MMddyyyyhhmmssffftt") + "-" + i + ".xml";
           //}
           //return (FilePath + _filePath) ;
           return gloGlobal.clsFileExtensions.NewDocumentName(FilePath, ".xml", "MMddyyyyHHmmssffff");
       }
       public static Int32 gnClinicID = 1;
       public static Int64 gnTaskUsedID = 1;
       public static string gsTaskUserName = "admin";
       public static Int64 gnLoginID = 0;
       public static string gsLoginName = "admin";
    }

    public class clsDataExtraction
    {
         
        public bool ExtractQueueData(Int64 _QueueID)
        {
           
            //string _FilePath = string.Empty;
            MemoryStream oFileStream = null;
            clsDataInsertionLayer objDataInsertionLayer=null;
            try
            {
                oFileStream = getQueueData(_QueueID);
                if (oFileStream != null)
                {
                    Int64 PatientID = 0;
                    string _PatientID;
                    string FirstNM = "", LatsNM = "", gender = "";
                    DateTime dob=DateTime.Now;
                    PatientSavingsNotificationType objPatientSavingsNotification = gloSerialization.GetClinicalDocument(oFileStream);
                    if (objPatientSavingsNotification == null)
                    {
                        AssociatePatientandUpdateQ(PatientID, _QueueID, FirstNM, LatsNM, gender, dob, "Invalid Data in File");
                        return false;
                    }
                                       
                        if (objPatientSavingsNotification.Patient != null)
                        {
                            if (objPatientSavingsNotification.Patient.Name != null)
                            {
                                LatsNM = objPatientSavingsNotification.Patient.Name.LastName;
                                FirstNM = objPatientSavingsNotification.Patient.Name.FirstName;
                            }
                            //gender = objPatientSavingsNotification.Patient.Gender != null ? objPatientSavingsNotification.Patient.Gender.ToString() : null;
                            gender =   objPatientSavingsNotification.Patient.Gender.ToString()  ;

                            dob = objPatientSavingsNotification.Patient.DateOfBirth != null ? objPatientSavingsNotification.Patient.DateOfBirth.Item : new DateTime();
                            _PatientID = objPatientSavingsNotification.Patient.Identification != null ? objPatientSavingsNotification.Patient.Identification.MedicalRecordIdentificationNumberEHR : null;
                            if (_PatientID != null && _PatientID != "")
                            {
                                Int64.TryParse(_PatientID.Trim(), out PatientID);
                                if (AssociatePatientandUpdateQ(PatientID, _QueueID, FirstNM, LatsNM, gender, dob, ""))
                                {
                                    objDataInsertionLayer = new clsDataInsertionLayer();
                                    objDataInsertionLayer.InsertPatientSavingMessage(objPatientSavingsNotification, PatientID, _QueueID);
                                    
                                }
                                else
                                {
                                    //Generate Unmatch Patient Task
                                    //Int64 _Taskid;
                                    String _Subject, _Note;

                                    _Subject = "New patient : " + FirstNM + " " + LatsNM + " found in Patient Saving Message file";
                                    _Note = "New Patient: " + FirstNM + " " + LatsNM + " found in Patient Saving Message file for " + PatSavGeneral.gsTaskUserName;
                                    //_Taskid = GenerateTasks(0, _Subject, _Note, _QueueID);
                                    //if (_Taskid != 0)
                                    //{
                                    //    UpdateTaskID_Queue(_QueueID, _Taskid);
                                    //}
                                }
                            }
                            else if (_PatientID == null)
                            {
                                if (FirstNM == "" && LatsNM == "")
                                {
                                    AssociatePatientandUpdateQ(PatientID, _QueueID, FirstNM, LatsNM, gender, dob, "Invalid Patient Data in File");

                                }
                                else
                                {
                                    //Generate Unmatch Patient Task
                                   // Int64 _Taskid;
                                    String _Subject, _Note;

                                    _Subject = "New patient : " + FirstNM + " " + LatsNM + " found in Patient Saving Message file";
                                    _Note = "New Patient: " + FirstNM + " " + LatsNM + " found in Patient Saving Message file for " + PatSavGeneral.gsTaskUserName;
                                    //_Taskid = GenerateTasks(0, _Subject, _Note, _QueueID);
                                    //if (_Taskid != 0)
                                    //{
                                    //    UpdateTaskID_Queue(_QueueID, _Taskid);
                                    //}
                                }
                            }
                           
                        }
                        objPatientSavingsNotification = null;
                }
            }
            catch //(Exception ex)
            {
                return false;
            }
            finally
            {
                objDataInsertionLayer = null;
                if (oFileStream != null)
                {
                    oFileStream.Dispose();
                    oFileStream = null;
                }
            }
            return true;
        }

        public MemoryStream getQueueData(Int64 _QueueID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            string strQuery = "";
            Byte[] AttachmentData = null;
            MemoryStream oFileStream = null;
            try
            {
                if (_QueueID > 0)
                {
                    oDB = new gloDatabaseLayer.DBLayer(PatSavGeneral.ConnectionString);
                    strQuery = "SELECT iXMLData FROM dbo.PatSav_Queue WHERE nPatSavQID=" + _QueueID + "";
                    oDB.Connect(false);
                    AttachmentData = (Byte[])oDB.ExecuteScalar_Query(strQuery);
                    oDB.Disconnect();

                    if (AttachmentData != null)
                    {
                        oFileStream = new MemoryStream(AttachmentData);
                    }

                }
            }
            catch //(Exception ex)
            {
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                AttachmentData = null;
            }
            return oFileStream;
        }

        public bool AssociatePatientandUpdateQ(Int64 _PatientID, Int64 _QueueID,string FirstNM,string LastNM,string gender,DateTime dob,string Comments)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters=null;
            
            try
            {
                if (_QueueID > 0 )
                {
                    oDB = new gloDatabaseLayer.DBLayer(PatSavGeneral.ConnectionString);
                    oDBParameters = new gloDatabaseLayer.DBParameters();
                    oDBParameters.Add("@nPatientID", _PatientID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@nQueueID", _QueueID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sFirstName", FirstNM, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sLastName", LastNM, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@dtDOB", dob , ParameterDirection.Input, SqlDbType.DateTime);
                    oDBParameters.Add("@sGender", gender, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sComments", Comments, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Connect(false);
                    string _res= oDB.ExecuteScalar("PatSav_ChkPatientUpdateQStatus", oDBParameters).ToString ();
                    oDB.Disconnect();
                    if (_res != "" && _res != "0")
                        _result = true;
                }
            }
            catch //(Exception ex)
            {
                _result = false;
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }

        //private long GenerateTasks(Int64 _PatientID, string _Subject, string _Note, Int64 _QueueID)
        //{
        //    long functionReturnValue = 0;
        //    gloTaskMail.Task oTask = null;
        //    gloTaskMail.gloTask ogloTask = null;
        //    gloTaskMail.TaskAssign oTaskAssign = null;
        //    try
        //    {
        //        long _TaskID = 0;

        //        if (PatSavGeneral.gnTaskUsedID != null | PatSavGeneral.gnTaskUsedID != 0)
        //        {                   
        //            //' Send the Task to The Users
        //            PatSavGeneral.gnLoginID = GetLoginId(PatSavGeneral.gsLoginName);
        //            DateTime dtDueDate = DateTime.Now;
        //            DateTime dtTaskDate = DateTime.Now;
        //            dtTaskDate = Convert.ToDateTime(dtTaskDate.ToString("MM/dd/yyyy") + " " + dtTaskDate.ToShortTimeString());

        //            oTask = new gloTaskMail.Task();
        //            ogloTask = new gloTaskMail.gloTask(PatSavGeneral.ConnectionString);
        //            oTaskAssign = new gloTaskMail.TaskAssign(PatSavGeneral.ConnectionString);

        //            oTask.TaskID = 0;
        //            oTask.UserID = PatSavGeneral.gnLoginID;
        //            oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtTaskDate));
        //            oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtTaskDate));
        //            oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtDueDate));
        //            oTask.Subject = _Subject;
        //            oTask.PriorityID = 3;
        //            //'High
        //            oTask.Notes = _Note;
        //            oTask.PatientID = _PatientID;
        //            oTask.ReferenceID1 = _QueueID;
        //            oTask.ClinicID = PatSavGeneral.gnClinicID;
        //            oTask.OwnerID = PatSavGeneral.gnLoginID;
        //            oTask.TaskType = gloTaskMail.TaskType.PatientSavingMessageUnmatched ;
        //            //If _TaskType = 9 Then
        //            //    oTask.TaskType = gloTaskMail.TaskType.CCD
        //            //ElseIf _TaskType = 10 Then
        //            //    oTask.TaskType = gloTaskMail.TaskType.CCDUnmatchedPatient
        //            //End If

        //            oTaskAssign.AssignFromID = PatSavGeneral.gnLoginID;
        //            oTaskAssign.AssignFromName = PatSavGeneral.gsLoginName;
        //            oTaskAssign.AssignToID = PatSavGeneral.gnTaskUsedID;
        //            if (oTaskAssign.AssignFromID == oTaskAssign.AssignToID)
        //            {
        //                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
        //                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
        //            }
        //            else
        //            {
        //                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned;
        //                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold;
        //            }
        //            oTaskAssign.AssignToName = PatSavGeneral.gsTaskUserName ;
        //            oTask.Assignment.Add(oTaskAssign);
        //            oTaskAssign.Dispose();

        //            //'Task Assign Properties
        //            //'Task Progress Values
        //            oTask.Progress.TaskID = 0;
        //            oTask.Progress.Complete = 0;
        //            oTask.Progress.Description = _Note;
        //            oTask.Progress.StatusID = 1;
        //            //'Not Started
        //            oTask.Progress.DateTime = DateTime.Now.Date;
        //            oTask.Progress.ClinicID = PatSavGeneral.gnClinicID;
        //            _TaskID = ogloTask.Add(oTask);
        //            return _TaskID;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
                
        //    }
        //    finally
        //    {
        //        if ((oTask != null))
        //        {
        //            oTask.Dispose();
        //            oTask = null;
        //        }
        //        if ((ogloTask != null))
        //        {
        //            ogloTask.Dispose();
        //            ogloTask = null;
        //        }
        //        if ((oTaskAssign != null))
        //        {
        //            oTaskAssign.Dispose();
        //            oTaskAssign = null;
        //        }
        //    }
        //    return functionReturnValue;

        //}

        public Int64 GetLoginId(string UserName)
        {
            object _result = 0;
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;

            try
            {
                if (UserName != "")
                {
                    oDB = new gloDatabaseLayer.DBLayer(PatSavGeneral.ConnectionString);
                    oDBParameters = new gloDatabaseLayer.DBParameters();
                    oDBParameters.Add("@sLogin", UserName, ParameterDirection.Input , SqlDbType.VarChar,100);
                    oDBParameters.Add("@nUserID", _result, ParameterDirection.Output, SqlDbType.BigInt);
                    oDB.Connect(false);
                    oDB.Execute("gsp_GetUser_Mst", oDBParameters,out  _result);
                    oDB.Disconnect();
                    if (_result == null)
                        return 0;
                }
            }
            catch //(Exception ex)
            {
                _result = 0;
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return (Int64)_result;
        }

        public bool UpdateTaskID_Queue(Int64 _QueueID, Int64 _TaskID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            string strQuery = "";
            try
            {
                if (_QueueID > 0)
                {
                    oDB = new gloDatabaseLayer.DBLayer(PatSavGeneral.ConnectionString);
                    strQuery = "Update dbo.PatSav_Queue set nTaskId=" + _TaskID + " where nPatSavQID=" + _QueueID + "";
                    oDB.Connect(false);
                    oDB.Execute_Query(strQuery);
                    oDB.Disconnect();
                }
            }
            catch //(Exception ex)
            {
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return true;
        }

    }
}
