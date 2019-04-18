using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.Data.SqlClient; 
using gloSettings;
using System.IO;


namespace gloTaskMail
{
    public enum TaskType
    {
        None = 0,
        Task = 1,
        OrderRadiology = 2,
        FAX = 3,
        LabOrder = 4,
        DMS = 5,
        Exam = 6,
        UnmatchedPatient = 7,  //Added by kanchan on 20100303 for Emdeon
        UnmatchedLabOrder = 8, //Added by kanchan on 20100309 for Emdeon
        Flowsheet = 9, //Added by Rahul on 20101025 for FlowSheet
        Drug = 10, //Added by Rahul on 20101025 for Drug
        CCD = 11,
        CCDUnmatchedPatient = 12,
        ExternalChargesPatientConflict = 13, //Added by Abhijeet on 20111004 for Inbound charges task through HL7
        ExternalChargesPatientNotFound = 14, //Added by Abhijeet on 20111004 for Inbound charges task through HL7
        OpenEmdeonlabOrder = 15, //Sanjog - Added to open endeon lab from task window
        HL7LabInboundFailureNotifyTask = 16, // Added by Abhijeet on 20111123 for Lab result failure notification task during HL7 inbound processing
        PatientPortalTask = 17, // Added by Abhijeet on 20111214 for task to process inbound data of patient portal
        //Developer:Sanjog Dhamke
        //Date:2012 Jan 12
        //PRD Name: Lab Usability for 6060
        //Reason: To add another type for those task which is generated from smart work flow
        PlaceLabOrder =18 ,
        Vitals=19,//added new task type for OB vital
        IntuitSecureMessageTask=20,
        IntuitAppointmentTask=21,
        IntuitRxRenewalTask=22,
        IntuitBillPayTask = 23,
        HL7DocumentTask = 24, //Added New task type by manoj jadhav on 20130219 for processing of DMS document
        Reconcile = 25,

        #region MU2 Patient Portal
        PPSecureMessageTask = 26,
        #endregion

        UnsolicitedTask = 27,

        //00000926 : Added new task type fo Assign Lab and DMS Task.
        AssignedLabOrder = 28,
        AssignedDMS = 29,
        PatientPortalPatientFormTask = 30,
        RCMDocs = 31,
        PortalPHI = 32,
        ImReconcile = 33,
        ImForecastTask = 34
    }

    public class TaskChangeEventArg : EventArgs
    {
        public Int64 TaskID = 0;
        public string Subject = "";
        public string FaxTiffFileName = "";
        public Int64 ReferenceID1 = 0;
        public Int64 ReferenceID2 = 0;
        public TaskType oTaskType = TaskType.None;
        public bool IsUpdated = false;
        public bool IsEMREnabled = false;
        public bool IsOpenFromView = false;
        public bool IsTaskClose = false;
        public String btnTag = "";
        public String sMessage = "";
        public bool IsIntuitBillPay { get; set; }
        public decimal IBPCheckamount { get; set; }
        public string IBPReferenceNumber { get; set; }
        public string IBPCardType { get; set; }
        public string IBPAuthNumber { get; set; }
        public Boolean IBPToken { get; set; }
        public Int64 PatientID { get; set; } 
    }

    public class gloTask : IDisposable
    {
        #region " Declarations "

        private string _databaseconnectionstring = "";
        //private string _messageBoxCaption = "gloPMS";
        private string _messageBoxCaption = String.Empty;
        public Boolean isFromTrackTask = false;
        public Boolean isSmartTask = false;
        public Boolean isFromReminder = false;//Developer:Pradeep/Date:01/09/2012/Bug ID:18512/Reason:from textbox was showing blank
        public Boolean IsRightToCompleteTaskForAllUsers = false;
        public Boolean isTaskForwardedFromIntuitMessage = false;
        //End
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion " Declarations "


        #region "Constructor & Distructor"


        public gloTask(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion

            this.OnGloTask_Change += new OnGloTaskChange(gloTask_OnGloTask_Change);
        }

        //void gloTask_OnGloTask_Change(object sender, EventArgs e, TaskChangeEventArg e2)
        //{

        //}

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //Bug #47721: 00000416 : Error Message - EMR
                    OnGloTask_Change -= new OnGloTaskChange(gloTask_OnGloTask_Change);
                    OnGloTask_Change = null;
                }
            }
            disposed = true;
        }

        ~gloTask()
        {
            Dispose(false);
        }

        #endregion

        #region " Comment "
        // //Task
        //public Int64 Add(Task oTask)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //    Int64 _result = 0;
        //    Object oResult = new object();

        //    try
        //    {
        //        oResult = null;
        //        oDB.Connect(false);
        //        //Pass TaskID = 0 for adding new record.
        //        oParameters.Add("@nTaskID", oTask.TaskID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //        oParameters.Add("@nProviderID", oTask.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nPatientID", oTask.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nStartDate", oTask.StartDate, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@sSubject", oTask.Subject, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@nDueDate", oTask.DueDate, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nStatusID", oTask.StatusID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nPriorityID", oTask.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@dComplete", Convert.ToDecimal(oTask.Complete), ParameterDirection.Input, SqlDbType.Decimal);
        //        oParameters.Add("@nCategoryID", oTask.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nFollowupID", oTask.FollowupID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@bIsPrivate", oTask.IsPrivate, ParameterDirection.Input, SqlDbType.Bit);
        //        oParameters.Add("@nOwnerID", oTask.OwnerID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nAssignedToID", oTask.AssignedToID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@sDescription", oTask.Description, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@nDateCompleted", oTask.DueDate, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@sNotes", oTask.Notes, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@nUserID", oTask.UserID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nClinicID", oTask.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

        //        _result = oDB.Execute("TM_INUP_Task_MST", oParameters, out oResult);

        //        if (oResult != null)
        //        {
        //            _result = Convert.ToInt64(oResult);
        //            if (!(_result > 0))
        //            {
        //                MessageBox.Show("ERROR : Adding Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return 0;

        //            }
        //        }
        //        return _result;

        //    }
        //    catch (gloDatabaseLayer.DBException dbErr)
        //    {
        //        MessageBox.Show("ERROR :" + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR :" + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return 0;

        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //        oParameters.Dispose();
        //        oResult = null;
        //    }
        //}

        // public bool Modify(Task oTask)
        // {
        //     gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //     gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //     Int64 _result = 0;
        //     Object oResult = new object();

        //     try
        //     {
        //         oResult = null;
        //         oDB.Connect(false);
        //         //Pass TaskID = 0 for adding new record.
        //         oParameters.Add("@nTaskID", oTask.TaskID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //         oParameters.Add("@nProviderID", oTask.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
        //         oParameters.Add("@nPatientID", oTask.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
        //         oParameters.Add("@nStartDate", oTask.StartDate, ParameterDirection.Input, SqlDbType.Int);
        //         oParameters.Add("@sSubject", oTask.Subject, ParameterDirection.Input, SqlDbType.VarChar);
        //         oParameters.Add("@nDueDate", oTask.DueDate, ParameterDirection.Input, SqlDbType.Int);
        //         oParameters.Add("@nStatusID", oTask.StatusID, ParameterDirection.Input, SqlDbType.BigInt);
        //         oParameters.Add("@nPriorityID", oTask.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
        //         oParameters.Add("@dComplete", oTask.Complete, ParameterDirection.Input, SqlDbType.Decimal);
        //         oParameters.Add("@nCategoryID", oTask.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
        //         oParameters.Add("@nFollowupID", oTask.FollowupID, ParameterDirection.Input, SqlDbType.BigInt);
        //         oParameters.Add("@bIsPrivate", oTask.IsPrivate, ParameterDirection.Input, SqlDbType.Bit);
        //         oParameters.Add("@nOwnerID", oTask.OwnerID, ParameterDirection.Input, SqlDbType.BigInt);
        //         oParameters.Add("@nAssignedToID", oTask.AssignedToID, ParameterDirection.Input, SqlDbType.BigInt);
        //         oParameters.Add("@sDescription", oTask.Description, ParameterDirection.Input, SqlDbType.VarChar);
        //         oParameters.Add("@nDateCompleted", oTask.DateCompleted, ParameterDirection.Input, SqlDbType.BigInt);
        //         oParameters.Add("@sNotes", oTask.Notes, ParameterDirection.Input, SqlDbType.VarChar);
        //         oParameters.Add("@nUserID", oTask.UserID, ParameterDirection.Input, SqlDbType.BigInt);
        //         oParameters.Add("@nClinicID", oTask.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

        //         _result = oDB.Execute("TM_INUP_Task_MST", oParameters, out oResult);

        //         if (oResult != null)
        //         {
        //             _result = Convert.ToInt64(oResult);
        //             if (!(_result > 0))
        //             {
        //                 MessageBox.Show("ERROR : Modify Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                 return false;

        //             }
        //         }
        //         return true;

        //     }
        //     catch (gloDatabaseLayer.DBException dbErr)
        //     {
        //         MessageBox.Show("ERROR :" + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //         return false;
        //     }
        //     catch (Exception ex)
        //     {
        //         MessageBox.Show("ERROR :" + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //         return false;

        //     }
        //     finally
        //     {
        //         oDB.Disconnect();
        //         oDB.Dispose();
        //         oParameters.Dispose();
        //         oResult = null;
        //     }
        // }

        // public bool Delete(Int64 TaskID)
        // {
        //     gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //     string strQuery = "";
        //     try
        //     {
        //         oDB.Connect(false);
        //         if (TaskID > 0)
        //         {

        //             strQuery = "delete from TM_Task_MST where nTaskID = " + TaskID;
        //             int result = oDB.Execute_Query(strQuery);
        //             if (result > 0)
        //             {
        //                 return true;
        //             }
        //         }
        //         return false;

        //     }
        //     catch (gloDatabaseLayer.DBException dbErr)
        //     {
        //         MessageBox.Show("ERROR :" + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //         return false;
        //     }
        //     catch (Exception ex)
        //     {
        //         MessageBox.Show("ERROR :" + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //         return false;

        //     }
        //     finally
        //     {
        //         oDB.Disconnect();
        //         oDB.Dispose();

        //     }
        // }

        // public bool IsDelete(Int64 TaskID)
        // {
        //     return false;
        // }

        // public bool IsExists(string TaskName)
        // {
        //     gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //     string strQuery = "";
        //     try
        //     {
        //         oDB.Connect(false);
        //         strQuery = "select count(nTaskID) from TM_Task_MST where sSubject = '" + TaskName + " ' ";
        //         object _intresult = null;
        //         _intresult = oDB.ExecuteScalar_Query(strQuery);
        //         if (_intresult != null)
        //         {
        //             if (_intresult.ToString().Trim() != "")
        //             {
        //                 if (Convert.ToInt64(_intresult) > 0)
        //                 {
        //                     return true;
        //                 }
        //             }
        //         }
        //         return false;
        //     }
        //     catch (gloDatabaseLayer.DBException dbErr)
        //     {
        //         MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //         return false;

        //     }
        //     catch (Exception ex)
        //     {
        //         MessageBox.Show("ERROR : " + ex.Message, "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //         return false;
        //     }
        //     finally
        //     {
        //         oDB.Disconnect();
        //         oDB.Dispose();


        //     }
        // }

        // public Task GetTask(Int64 TaskID)
        // {
        //     gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //     DataTable dtTask = new DataTable();
        //     Task oTask = new Task();
        //     string strQuery = "";

        //     try
        //     {
        //         oDB.Connect(false);
        //         if (TaskID <= 0)
        //         {
        //             return null;
        //         }


        //         strQuery = "select * from TM_Task_MST where nTaskID =" + TaskID;
        //         oDB.Retrive_Query(strQuery, out dtTask);
        //         if (dtTask != null && dtTask.Rows.Count > 0)
        //         {
        //             //dbo.TM_Task_MST -->nTaskID,nProviderID,nPatientID,sSubject,nStartDate,nDueDate,
        //             //nStatusID,nPriorityID,dComplete,nCategoryID,nFollowupID,bIsPrivate,nOwnerID,nAssignedToID,
        //             //sDescription,nDateCompleted,sNotes,nUserID,nClinicID

        //             oTask.TaskID = Convert.ToInt64(dtTask.Rows[0]["nTaskID"]);

        //             oTask.ProviderID = Convert.ToInt64(dtTask.Rows[0]["nProviderID"]);
        //             gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
        //             oTask.ProviderName = oResource.GetProviderName(oTask.ProviderID);
        //             //oResource.Dispose();

        //             oTask.PatientID = Convert.ToInt64(dtTask.Rows[0]["nPatientID"]);
        //             gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
        //             oTask.PatientName = ogloPatient.GetPatientName(oTask.PatientID);
        //             ogloPatient.Dispose();


        //             oTask.Subject = Convert.ToString(dtTask.Rows[0]["sSubject"]);
        //             oTask.StartDate = Convert.ToInt64(dtTask.Rows[0]["nStartDate"]);
        //             oTask.DueDate = Convert.ToInt64(dtTask.Rows[0]["nDueDate"]);

        //             gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

        //             oTask.StatusID = Convert.ToInt64(dtTask.Rows[0]["nStatusID"]);
        //             gloTasksMails.Common.Status oStatus = new gloTasksMails.Common.Status();
        //             oStatus = oTaskMail.GetStatus(oTask.StatusID);
        //             oTask.Status = oStatus.Description;
        //             oStatus.Dispose();

        //             oTask.PriorityID = Convert.ToInt64(dtTask.Rows[0]["nPriorityID"]);
        //             gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
        //             oPriority = oTaskMail.GetPriority(oTask.PriorityID);
        //             oTask.Priority = oPriority.Description;
        //             oPriority.Dispose();

        //             oTask.Complete = Convert.ToDecimal(dtTask.Rows[0]["dComplete"]);

        //             oTask.CategoryID = Convert.ToInt64(dtTask.Rows[0]["nCategoryID"]);
        //             gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
        //             oCategory = oTaskMail.GetCategory(oTask.CategoryID);
        //             oTask.Category = oCategory.Description;
        //             oCategory.Dispose();

        //             oTask.FollowupID = Convert.ToInt64(dtTask.Rows[0]["nFollowupID"]);
        //             gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();
        //             oFollowUp = oTaskMail.GetFollowUp(oTask.FollowupID);
        //             oTask.Followup = oFollowUp.Description;
        //             oFollowUp.Dispose();

        //             oTask.IsPrivate = Convert.ToBoolean(dtTask.Rows[0]["bIsPrivate"]);

        //             oTask.OwnerID = Convert.ToInt64(dtTask.Rows[0]["nOwnerID"]);
        //             //gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
        //             oTask.OwnerName = oResource.GetProviderName(oTask.OwnerID);
        //             //oResource.Dispose();


        //             oTask.AssignedToID = Convert.ToInt64(dtTask.Rows[0]["nAssignedToID"]);
        //             //gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
        //             oTask.AssignedToName = oResource.GetProviderName(oTask.AssignedToID);
        //             oResource.Dispose();

        //             oTask.Description = Convert.ToString(dtTask.Rows[0]["sDescription"]);
        //             oTask.DateCompleted = Convert.ToInt64(dtTask.Rows[0]["nDateCompleted"]);
        //             oTask.Notes = Convert.ToString(dtTask.Rows[0]["sNotes"]);

        //             oTask.UserID = Convert.ToInt64(dtTask.Rows[0]["nUserID"]);
        //             //oTask.UserName; 

        //             oTask.ClinicID = Convert.ToInt64(dtTask.Rows[0]["nClinicID"]);
        //             //oTask.ClinicName;

        //             oTaskMail.Dispose();
        //             return oTask;


        //         }//END-if (dtTask != null && dtTask.Rows.Count > 0)

        //         return null;

        //     }//END - try
        //     catch (gloDatabaseLayer.DBException dbErr)
        //     {
        //         MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //         return null;
        //     }
        //     catch (Exception ex)
        //     {
        //         MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //         return null;

        //     }
        //     finally
        //     {
        //         oDB.Disconnect();
        //         dtTask.Dispose();
        //     }
        // }//END - GetTask

        // public Tasks GetTasks()
        // {

        //     gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //     DataTable dtTask = new DataTable();
        //     Task oTask;
        //     Tasks oTasks = new Tasks();
        //     string strQuery = "";

        //     try
        //     {
        //         oDB.Connect(false);

        //         strQuery = "select * from TM_Task_MST ";
        //         oDB.Retrive_Query(strQuery, out dtTask);
        //         if (dtTask != null && dtTask.Rows.Count > 0)
        //         {
        //             //dbo.TM_Task_MST -->nTaskID,nProviderID,nPatientID,sSubject,nStartDate,nDueDate,
        //             //nStatusID,nPriorityID,dComplete,nCategoryID,nFollowupID,bIsPrivate,nOwnerID,nAssignedToID,
        //             //sDescription,nDateCompleted,sNotes,nUserID,nClinicID
        //             for (int i = 0; i <= dtTask.Rows.Count - 1; i++)
        //             {

        //                 oTask = new Task();

        //                 oTask.TaskID = Convert.ToInt64(dtTask.Rows[i]["nTaskID"]);

        //                 oTask.ProviderID = Convert.ToInt64(dtTask.Rows[i]["nProviderID"]);
        //                 gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
        //                 oTask.ProviderName = oResource.GetProviderName(oTask.ProviderID);
        //                 //oResource.Dispose();

        //                 oTask.PatientID = Convert.ToInt64(dtTask.Rows[i]["nPatientID"]);
        //                 gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
        //                 oTask.PatientName = ogloPatient.GetPatientName(oTask.PatientID);
        //                 ogloPatient.Dispose();


        //                 oTask.Subject = Convert.ToString(dtTask.Rows[i]["sSubject"]);
        //                 oTask.StartDate = Convert.ToInt64(dtTask.Rows[i]["nStartDate"]);
        //                 oTask.DueDate = Convert.ToInt64(dtTask.Rows[i]["nDueDate"]);

        //                 gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

        //                 oTask.StatusID = Convert.ToInt64(dtTask.Rows[i]["nStatusID"]);
        //                 gloTasksMails.Common.Status oStatus = new gloTasksMails.Common.Status();
        //                 oStatus = oTaskMail.GetStatus(oTask.StatusID);
        //                 oTask.Status = oStatus.Description;
        //                 oStatus.Dispose();

        //                 oTask.PriorityID = Convert.ToInt64(dtTask.Rows[i]["nPriorityID"]);
        //                 gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
        //                 oPriority = oTaskMail.GetPriority(oTask.PriorityID);
        //                 oTask.Priority = oPriority.Description;
        //                 oPriority.Dispose();

        //                 oTask.Complete = Convert.ToDecimal(dtTask.Rows[i]["dComplete"]);

        //                 oTask.CategoryID = Convert.ToInt64(dtTask.Rows[i]["nCategoryID"]);
        //                 gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
        //                 oCategory = oTaskMail.GetCategory(oTask.CategoryID);
        //                 oTask.Category = oCategory.Description;
        //                 oCategory.Dispose();

        //                 oTask.FollowupID = Convert.ToInt64(dtTask.Rows[i]["nFollowupID"]);
        //                 gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();
        //                 oFollowUp = oTaskMail.GetFollowUp(oTask.FollowupID);
        //                 oTask.Followup = oFollowUp.Description;
        //                 oFollowUp.Dispose();

        //                 oTask.IsPrivate = Convert.ToBoolean(dtTask.Rows[i]["bIsPrivate"]);

        //                 oTask.OwnerID = Convert.ToInt64(dtTask.Rows[i]["nOwnerID"]);
        //                 //gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
        //                 oTask.OwnerName = oResource.GetProviderName(oTask.OwnerID);
        //                 //oResource.Dispose();


        //                 oTask.AssignedToID = Convert.ToInt64(dtTask.Rows[i]["nAssignedToID"]);
        //                 //gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
        //                 oTask.AssignedToName = oResource.GetProviderName(oTask.AssignedToID);
        //                 oResource.Dispose();

        //                 oTask.Description = Convert.ToString(dtTask.Rows[i]["sDescription"]);
        //                 oTask.DateCompleted = Convert.ToInt64(dtTask.Rows[i]["nDateCompleted"]);
        //                 oTask.Notes = Convert.ToString(dtTask.Rows[0]["sNotes"]);

        //                 oTask.UserID = Convert.ToInt64(dtTask.Rows[i]["nUserID"]);
        //                 //oTask.UserName; 

        //                 oTask.ClinicID = Convert.ToInt64(dtTask.Rows[i]["nClinicID"]);
        //                 //oTask.ClinicName;

        //                 oTasks.Add(oTask);
        //                 oTaskMail.Dispose();

        //             }//END - for ( int i =0 ; i <= dtTask.Rows.Count -1 ; i++)

        //             return oTasks;

        //         }//END-if (dtTask != null && dtTask.Rows.Count > 0)

        //         return null;

        //     }//END - try
        //     catch (gloDatabaseLayer.DBException dbErr)
        //     {
        //         MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //         return null;
        //     }
        //     catch (Exception ex)
        //     {
        //         MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //         return null;

        //     }
        //     finally
        //     {
        //         oDB.Disconnect();
        //         dtTask.Dispose();
        //     }
        // }//END - GetTasks

        // public Tasks GetProviderTasks(Int64 ProviderId)
        // {

        //     gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //     DataTable dtTask = new DataTable();
        //     Task oTask;
        //     Tasks oTasks = new Tasks();
        //     string strQuery = "";

        //     try
        //     {
        //         oDB.Connect(false);

        //         strQuery = "select * from TM_Task_MST where nProviderID= " + ProviderId + " OR nAssignedToID=" + ProviderId + " ";
        //         oDB.Retrive_Query(strQuery, out dtTask);
        //         if (dtTask != null && dtTask.Rows.Count > 0)
        //         {
        //             //dbo.TM_Task_MST -->nTaskID,nProviderID,nPatientID,sSubject,nStartDate,nDueDate,
        //             //nStatusID,nPriorityID,dComplete,nCategoryID,nFollowupID,bIsPrivate,nOwnerID,nAssignedToID,
        //             //sDescription,nDateCompleted,sNotes,nUserID,nClinicID
        //             for (int i = 0; i <= dtTask.Rows.Count - 1; i++)
        //             {

        //                 oTask = new Task();

        //                 oTask.TaskID = Convert.ToInt64(dtTask.Rows[i]["nTaskID"]);

        //                 oTask.ProviderID = Convert.ToInt64(dtTask.Rows[i]["nProviderID"]);
        //                 gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
        //                 oTask.ProviderName = oResource.GetProviderName(oTask.ProviderID);
        //                 //oResource.Dispose();

        //                 oTask.PatientID = Convert.ToInt64(dtTask.Rows[i]["nPatientID"]);
        //                 gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
        //                 oTask.PatientName = ogloPatient.GetPatientName(oTask.PatientID);
        //                 ogloPatient.Dispose();


        //                 oTask.Subject = Convert.ToString(dtTask.Rows[i]["sSubject"]);
        //                 oTask.StartDate = Convert.ToInt64(dtTask.Rows[i]["nStartDate"]);
        //                 oTask.DueDate = Convert.ToInt64(dtTask.Rows[i]["nDueDate"]);

        //                 gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

        //                 oTask.StatusID = Convert.ToInt64(dtTask.Rows[i]["nStatusID"]);
        //                 gloTasksMails.Common.Status oStatus = new gloTasksMails.Common.Status();
        //                 oStatus = oTaskMail.GetStatus(oTask.StatusID);
        //                 oTask.Status = oStatus.Description;
        //                 oStatus.Dispose();

        //                 oTask.PriorityID = Convert.ToInt64(dtTask.Rows[i]["nPriorityID"]);
        //                 gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
        //                 oPriority = oTaskMail.GetPriority(oTask.PriorityID);
        //                 oTask.Priority = oPriority.Description;
        //                 oPriority.Dispose();

        //                 oTask.Complete = Convert.ToDecimal(dtTask.Rows[i]["dComplete"]);

        //                 oTask.CategoryID = Convert.ToInt64(dtTask.Rows[i]["nCategoryID"]);
        //                 gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
        //                 oCategory = oTaskMail.GetCategory(oTask.CategoryID);
        //                 oTask.Category = oCategory.Description;
        //                 oCategory.Dispose();

        //                 oTask.FollowupID = Convert.ToInt64(dtTask.Rows[i]["nFollowupID"]);
        //                 gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();
        //                 oFollowUp = oTaskMail.GetFollowUp(oTask.FollowupID);
        //                 oTask.Followup = oFollowUp.Description;
        //                 oFollowUp.Dispose();

        //                 oTask.IsPrivate = Convert.ToBoolean(dtTask.Rows[i]["bIsPrivate"]);

        //                 oTask.OwnerID = Convert.ToInt64(dtTask.Rows[i]["nOwnerID"]);
        //                 //gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
        //                 oTask.OwnerName = oResource.GetProviderName(oTask.OwnerID);
        //                 //oResource.Dispose();


        //                 oTask.AssignedToID = Convert.ToInt64(dtTask.Rows[i]["nAssignedToID"]);
        //                 //gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
        //                 oTask.AssignedToName = oResource.GetProviderName(oTask.AssignedToID);
        //                 oResource.Dispose();

        //                 oTask.Description = Convert.ToString(dtTask.Rows[i]["sDescription"]);
        //                 oTask.DateCompleted = Convert.ToInt64(dtTask.Rows[i]["nDateCompleted"]);
        //                 oTask.Notes = Convert.ToString(dtTask.Rows[0]["sNotes"]);

        //                 oTask.UserID = Convert.ToInt64(dtTask.Rows[i]["nUserID"]);
        //                 //oTask.UserName; 

        //                 oTask.ClinicID = Convert.ToInt64(dtTask.Rows[i]["nClinicID"]);
        //                 //oTask.ClinicName;

        //                 oTasks.Add(oTask);
        //                 oTaskMail.Dispose();

        //             }//END - for ( int i =0 ; i <= dtTask.Rows.Count -1 ; i++)

        //             return oTasks;

        //         }//END-if (dtTask != null && dtTask.Rows.Count > 0)

        //         return null;

        //     }//END - try
        //     catch (gloDatabaseLayer.DBException dbErr)
        //     {
        //         MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //         return null;
        //     }
        //     catch (Exception ex)
        //     {
        //         MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //         return null;

        //     }
        //     finally
        //     {
        //         oDB.Disconnect();
        //         dtTask.Dispose();
        //     }
        // }//END - GetTasks
        #endregion " Comment "

        public delegate void OnGloTaskChange(object sender, EventArgs e, TaskChangeEventArg e2,object objfrmtask=null);
        public event OnGloTaskChange OnGloTask_Change;

        //---New Methods after db Changes 

        #region " Add Task Method "

        //function in 5061 Add(Task oTask)
        public Int64 Add(Task oTask)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Int64 _ReturnTaskID = 0;
            Object oResult = new object();
            TaskAssign oTaskAssign;
            TaskProgress oTaskProgress;

            // check the autoacceptTask setting added by pradeep
            
            object oResultExplictlyAcceptTask = null;
            Int32 _ExplictlyAcceptTask = 0;
            
            // If any other task than the HL7 Lab processing Failure check the settings as this tasks required to be implemented auto accepted by default
            if (oTask.TaskType != TaskType.HL7LabInboundFailureNotifyTask)  
            {
                gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                ogloSettings.GetSetting("ExplicitlyAcceptTask", out oResultExplictlyAcceptTask);

                if (oResultExplictlyAcceptTask != null && !string.IsNullOrEmpty(oResultExplictlyAcceptTask.ToString()))
                {
                    _ExplictlyAcceptTask = Convert.ToInt32(oResultExplictlyAcceptTask);
                }
                ogloSettings.Dispose();
                ogloSettings = null;
            }
            else
            {
                _ExplictlyAcceptTask = 0;
            }
            
            //** check the autoacceptTask setting added by pradeep

            try
            {
                //setting off
                if (_ExplictlyAcceptTask != 0)
                {
                    #region " Without Auto Accept save method "

                    oResult = null;
                    oDB.Connect(false);

                    //Table -> TM_TaskMST
                    //nTaskID,nProviderID,nPatientID,sSubject,nStartDate,nDueDate,nPriorityID,nCategoryID
                    //nFollowUpID,bIsPrivate,nOwnerID,nDateCreated,sNoteExt,nUserID,nClinicID

                    //Pass TaskID = 0 for adding new record.
                    oParameters.Add("@nTaskID", oTask.TaskID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oParameters.Add("@nProviderID", oTask.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPatientID", oTask.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sSubject", oTask.Subject, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nStartDate", oTask.StartDate, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nDueDate", oTask.DueDate, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPriorityID", oTask.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nCategoryID", oTask.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nFollowUpID", oTask.FollowupID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@bIsPrivate", oTask.IsPrivate, ParameterDirection.Input, SqlDbType.Bit);
                    oParameters.Add("@nOwnerID", oTask.OwnerID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nDateCreated", oTask.DateCreated, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sNoteExt", oTask.Notes, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nUserID", oTask.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nTaskType", oTask.TaskType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oParameters.Add("@sFaxTiffFileName", oTask.FaxTiffFileName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sMachineName", oTask.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nReferenceID1", oTask.ReferenceID1, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nReferenceID2", oTask.ReferenceID2, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID", oTask.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nTaskGroupID", oTask.TaskGroupID, ParameterDirection.Input, SqlDbType.BigInt);

                    //First make entry for Task Master & get the TaskID for this saved Task.
                    _result = oDB.Execute("TM_IN_Task", oParameters, out oResult);

                    //Check for proper entry in Task Master otherwise abort
                    if (_result == 0 && oResult == null)
                    {
                        MessageBox.Show("ERROR : Adding Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;
                    }

                    else
                    {
                        // Get the TaskID for the saved Task.
                        _ReturnTaskID = Convert.ToInt64(oResult);
                        if (isSmartTask == true)//Only for smart setting when task form viewable.
                        {
                            if (oTask.Notes != "")
                            {
                                oTask.Notes = oTask.Notes.Replace("'", "''");
                            }

                            gloDatabaseLayer.DBLayer oDBs = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                            string strQry = "UPDATE tm_taskmst SET sNoteEXT='" + oTask.Notes + "' where ntaskid=" + _ReturnTaskID;
                            oDB.Connect(false);
                            oDB.Execute_Query(strQry);

                        }
                    }




                    // * Make entry for the Assignments 
                    for (int i = 0; i < oTask.Assignment.Count; i++)
                    {
                        oTaskAssign = new TaskAssign();
                        oTaskAssign = oTask.Assignment[i];
                        //Assign the TaskID of the saved Task_MST Task (from above)
                        oTaskAssign.TaskID = _ReturnTaskID;
                        //changes made for adding responsibility to task,ownership to task
                        bool result = SaveTaskAssignments(oTaskAssign, false, 0, 0, oTask.TaskGroupID);

                        if (!result)
                        {
                            //TODO : if not sucessful we can delete the MaterTable entry from here as we have 
                            //the TaskID (equivalent to rollback).

                            MessageBox.Show("ERROR : Adding Task Assignments Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return 0;
                        }

                        oTaskAssign.Dispose();
                    }




                    // * Make entry for the Task Progress

                    oTaskProgress = new TaskProgress();
                    oTaskProgress = oTask.Progress;
                    oTaskProgress.TaskID = _ReturnTaskID;
                    oTaskProgress.ClinicID = oTask.ClinicID;
                    bool retresult = SaveTaskProgress(oTaskProgress);

                    if (!retresult)
                    {
                        //TODO : if not sucessful we can delete the MaterTable entry from here as we have 
                        //the TaskID (equivalent to rollback).

                        MessageBox.Show("ERROR : Adding Task Progress Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;

                    }
                    oTaskProgress.Dispose();

                    #endregion
                }
                else
                {
                    //setting on
                    #region " Auto Accept save method "
                    oResult = null;
                    oDB.Connect(false);

                    #region " Save Assignment "

                    // * Make entry for the Assignments 
                    for (int i = 0; i < oTask.Assignment.Count; i++)
                    {

                        #region " Save Master "
                        //Table -> TM_TaskMST
                        //nTaskID,nProviderID,nPatientID,sSubject,nStartDate,nDueDate,nPriorityID,nCategoryID
                        //nFollowUpID,bIsPrivate,nOwnerID,nDateCreated,sNoteExt,nUserID,nClinicID

                        //Pass TaskID = 0 for adding new record.
                        oParameters.Clear();
                        oParameters.Add("@nTaskID", oTask.TaskID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                        oParameters.Add("@nProviderID", oTask.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nPatientID", oTask.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@sSubject", oTask.Subject, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@nStartDate", oTask.StartDate, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nDueDate", oTask.DueDate, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nPriorityID", oTask.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nCategoryID", oTask.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nFollowUpID", oTask.FollowupID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@bIsPrivate", oTask.IsPrivate, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nOwnerID", oTask.OwnerID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nDateCreated", oTask.DateCreated, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@sNoteExt", oTask.Notes, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@nUserID", oTask.Assignment[i].AssignToID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nTaskType", oTask.TaskType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                        oParameters.Add("@sFaxTiffFileName", oTask.FaxTiffFileName, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sMachineName", oTask.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@nReferenceID1", oTask.ReferenceID1, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nReferenceID2", oTask.ReferenceID2, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", oTask.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nTaskGroupID", oTask.TaskGroupID, ParameterDirection.Input, SqlDbType.BigInt);
                        //First make entry for Task Master & get the TaskID for this saved Task.
                        _result = oDB.Execute("TM_IN_Task", oParameters, out oResult);

                        //Check for proper entry in Task Master otherwise abort
                        if (_result == 0 && oResult == null)
                        {
                            MessageBox.Show("ERROR : Adding Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return 0;
                        }

                        else
                        {
                            // Get the TaskID for the saved Task.
                            _ReturnTaskID = Convert.ToInt64(oResult);
                            //Reverting changes "Add Audit for Task" for incident #CAS-11436-G3Y6Z8 - Task not creating from services
                            //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.Task, gloAuditTrail.ActivityType.Add, "Patient Task Created", oTask.PatientID, _ReturnTaskID, oTask.ProviderID, gloAuditTrail.ActivityOutCome.Success);
                            if (isSmartTask == true)//Only for smart setting when task form viewable.
                            {
                                if (oTask.Notes != "")
                                {
                                    oTask.Notes = oTask.Notes.Replace("'", "''");
                                }

                                gloDatabaseLayer.DBLayer oDBs = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                string strQry = "UPDATE tm_taskmst SET sNoteEXT='" + oTask.Notes + "' where ntaskid=" + _ReturnTaskID;
                                oDB.Connect(false);
                                oDB.Execute_Query(strQry);

                            }

                        }

                        #endregion


                        oTaskAssign = new TaskAssign();
                        oTaskAssign = oTask.Assignment[i];
                        oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                        oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                        //Assign the TaskID of the saved Task_MST Task (from above)
                        oTaskAssign.TaskID = _ReturnTaskID;

                        bool result = SaveTaskAssignments(oTaskAssign);

                        if (!result)
                        {
                            //TODO : if not sucessful we can delete the MaterTable entry from here as we have 
                            //the TaskID (equivalent to rollback).

                            MessageBox.Show("ERROR : Adding Task Assignments Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return 0;
                        }

                        oTaskAssign.Dispose();


                        #region " Save Progress "
                        // * Make entry for the Task Progress


                        oTaskProgress = new TaskProgress();
                        oTaskProgress = oTask.Progress;
                        oTaskProgress.TaskID = _ReturnTaskID;
                        oTaskProgress.ClinicID = oTask.ClinicID;
                        bool retresult = SaveTaskProgress(oTaskProgress);

                        if (!retresult)
                        {
                            //TODO : if not sucessful we can delete the MaterTable entry from here as we have 
                            //the TaskID (equivalent to rollback).

                            MessageBox.Show("ERROR : Adding Task Progress Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return 0;

                        }
                        oTaskProgress.Dispose();
                        #endregion



                    }

                    #endregion



                    #endregion
                }

                return _ReturnTaskID;
                

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                // MessageBox.Show("ERROR :" + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dbErr.ERROR_Log(dbErr.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR :" + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                oResult = null;

            }
        }

        // function in 5070 Add(Task oTask)
        public Int64 Add5070(Task oTask)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Int64 _ReturnTaskID = 0;
            Object oResult = new object();
            TaskAssign oTaskAssign;
            TaskProgress oTaskProgress;
            Task oTask1;
            oTask1 = oTask;
            // check the autoacceptTask setting added by pradeep
            object oResultExplictlyAcceptTask = null;
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            ogloSettings.GetSetting("ExplicitlyAcceptTask", out oResultExplictlyAcceptTask);
            ogloSettings.Dispose();
            ogloSettings = null;
            Int32 _ExplictlyAcceptTask = 0;
            if (oResultExplictlyAcceptTask != null && !string.IsNullOrEmpty(oResultExplictlyAcceptTask.ToString()))
            {
                _ExplictlyAcceptTask = Convert.ToInt32(oResultExplictlyAcceptTask);
            }

            try
            {
                {
                    #region " Without Auto Accept save method "

                    oResult = null;
                    oDB.Connect(false);

                    //Table -> TM_TaskMST
                    //nTaskID,nProviderID,nPatientID,sSubject,nStartDate,nDueDate,nPriorityID,nCategoryID
                    //nFollowUpID,bIsPrivate,nOwnerID,nDateCreated,sNoteExt,nUserID,nClinicID

                    //Pass TaskID = 0 for adding new record.
                    oParameters.Add("@nTaskID", oTask.TaskID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oParameters.Add("@nProviderID", oTask.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPatientID", oTask.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sSubject", oTask.Subject, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nStartDate", oTask.StartDate, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nDueDate", oTask.DueDate, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPriorityID", oTask.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nCategoryID", oTask.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nFollowUpID", oTask.FollowupID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@bIsPrivate", oTask.IsPrivate, ParameterDirection.Input, SqlDbType.Bit);
                    oParameters.Add("@nOwnerID", oTask.OwnerID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nDateCreated", oTask.DateCreated, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sNoteExt", oTask.Notes, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nUserID", oTask.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nTaskType", oTask.TaskType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oParameters.Add("@sFaxTiffFileName", oTask.FaxTiffFileName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sMachineName", oTask.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nReferenceID1", oTask.ReferenceID1, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nReferenceID2", oTask.ReferenceID2, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID", oTask.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                    //First make entry for Task Master & get the TaskID for this saved Task.
                    _result = oDB.Execute("TM_IN_Task", oParameters, out oResult);

                    //Check for proper entry in Task Master otherwise abort
                    if (_result == 0 && oResult == null)
                    {
                        MessageBox.Show("ERROR : Adding Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;

                    }
                    else
                    {
                        // Get the TaskID for the saved Task.
                        _ReturnTaskID = Convert.ToInt64(oResult);

                    }



                    // * Make entry for the Assignments 


                    for (int i = 0; i < oTask.Assignment.Count; i++)
                    {
                        oTaskAssign = new TaskAssign();
                        oTaskAssign = oTask.Assignment[i];
                        //Assign the TaskID of the saved Task_MST Task (from above)
                        //oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned ;
                        //oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold;
                        oTaskAssign.TaskID = _ReturnTaskID;
                        if (_ExplictlyAcceptTask == 0)
                        {
                            oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                        }

                        bool result = SaveTaskAssignments(oTaskAssign);

                        if (!result)
                        {
                            //TODO : if not sucessful we can delete the MaterTable entry from here as we have 
                            //the TaskID (equivalent to rollback).

                            MessageBox.Show("ERROR : Adding Task Assignments Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return 0;
                        }

                        oTaskAssign.Dispose();

                    }



                    // * Make entry for the Task Progress

                    oTaskProgress = new TaskProgress();
                    oTaskProgress = oTask.Progress;
                    oTaskProgress.TaskID = _ReturnTaskID;
                    oTaskProgress.ClinicID = oTask.ClinicID;

                    bool retresult = SaveTaskProgress(oTaskProgress);

                    if (!retresult)
                    {
                        //TODO : if not sucessful we can delete the MaterTable entry from here as we have 
                        //the TaskID (equivalent to rollback).

                        MessageBox.Show("ERROR : Adding Task Progress Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;

                    }
                    oTaskProgress.Dispose();

                    #endregion
                }
                if (_ExplictlyAcceptTask == 0)
                {
                    //setting on
                    #region " Auto Accept save method "
                    oResult = null;
                    oDB.Connect(false);

                    #region " Save Assignment "

                    // * Make entry for the Assignments 
                    for (int i = 0; i < oTask1.Assignment.Count; i++)
                    {

                        #region " Save Master "
                        //Table -> TM_TaskMST
                        //nTaskID,nProviderID,nPatientID,sSubject,nStartDate,nDueDate,nPriorityID,nCategoryID
                        //nFollowUpID,bIsPrivate,nOwnerID,nDateCreated,sNoteExt,nUserID,nClinicID

                        //Pass TaskID = 0 for adding new record.
                        if (oTask1.Assignment[i].AssignToID != (oTask1.Assignment[i].AssignFromID))
                        {
                            oParameters.Clear();
                            oParameters.Add("@nTaskID", oTask1.TaskID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            oParameters.Add("@nProviderID", oTask1.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nPatientID", oTask1.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@sSubject", oTask1.Subject, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@nStartDate", oTask1.StartDate, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nDueDate", oTask1.DueDate, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nPriorityID", oTask1.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nCategoryID", oTask1.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nFollowUpID", oTask1.FollowupID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@bIsPrivate", oTask1.IsPrivate, ParameterDirection.Input, SqlDbType.Bit);
                            oParameters.Add("@nOwnerID", oTask1.Assignment[i].AssignToID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nDateCreated", oTask1.DateCreated, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@sNoteExt", oTask1.Notes, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@nUserID", oTask1.Assignment[i].AssignToID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nTaskType", oTask1.TaskType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oParameters.Add("@sFaxTiffFileName", oTask1.FaxTiffFileName, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@sMachineName", oTask1.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@nReferenceID1", oTask1.ReferenceID1, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nReferenceID2", oTask1.ReferenceID2, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nClinicID", oTask1.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                            //First make entry for Task Master & get the TaskID for this saved Task.
                            _result = oDB.Execute("TM_IN_Task", oParameters, out oResult);

                            //Check for proper entry in Task Master otherwise abort
                            if (_result == 0 && oResult == null)
                            {
                                MessageBox.Show("ERROR : Adding Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return 0;

                            }
                            else
                            {
                                // Get the TaskID for the saved Task.
                                _ReturnTaskID = Convert.ToInt64(oResult);

                            }


                        #endregion

                            oTaskAssign = new TaskAssign();
                            oTaskAssign = oTask1.Assignment[i];
                            oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                            oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                            //Assign the TaskID of the saved Task_MST Task (from above)
                            oTaskAssign.TaskID = _ReturnTaskID;

                            bool result = SaveTaskAssignments(oTaskAssign);

                            if (!result)
                            {
                                //TODO : if not sucessful we can delete the MaterTable entry from here as we have 
                                //the TaskID (equivalent to rollback).

                                MessageBox.Show("ERROR : Adding Task Assignments Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return 0;
                            }

                            oTaskAssign.Dispose();


                            #region " Save Progress "
                            // * Make entry for the Task Progress


                            oTaskProgress = new TaskProgress();
                            oTaskProgress = oTask1.Progress;
                            oTaskProgress.TaskID = _ReturnTaskID;
                            oTaskProgress.ClinicID = oTask1.ClinicID;

                            bool retresult = SaveTaskProgress(oTaskProgress);

                            if (!retresult)
                            {
                                //TODO : if not sucessful we can delete the MaterTable entry from here as we have 
                                //the TaskID (equivalent to rollback).

                                MessageBox.Show("ERROR : Adding Task Progress Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return 0;

                            }
                            oTaskProgress.Dispose();

                        }
                            #endregion



                    }

                    #endregion



                    #endregion
                }


                // }


                return _ReturnTaskID;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                // MessageBox.Show("ERROR :" + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dbErr.ERROR_Log(dbErr.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR :" + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                oResult = null;
            }
        }

        //changes made for adding responsibility to task,ownership to task
        private bool SaveTaskAssignments(TaskAssign oTaskAssign, bool ownertask=false , Int64 AssigntoID=0, Int64 AssignFromID=0,Int64 GroupID=0)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();


            try
            {
                if (oTaskAssign != null)
                {
                    //Table -> dbo.TM_Task_Assign
                    //nTaskID,nAssignToID numeric(18, 0),nAssignFromID numeric(18, 0),nSelfAssigned smallint,nAcceptRejectHold smallint,
                    //nClinicID numeric(18, 0)
                    oDB.Connect(false);
                    oParameters.Add("@nTaskID", oTaskAssign.TaskID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nAssignToID", oTaskAssign.AssignToID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nAssignFromID", oTaskAssign.AssignFromID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nSelfAssigned", oTaskAssign.SelfAssigned, ParameterDirection.Input, SqlDbType.SmallInt);
                    oParameters.Add("@nAcceptRejectHold", oTaskAssign.AcceptRejectHold, ParameterDirection.Input, SqlDbType.SmallInt);
                    oParameters.Add("@nClinicID", oTaskAssign.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@bIsowner", ownertask , ParameterDirection.Input, SqlDbType.Bit );
                    oParameters.Add("@TskOwnnAssignToID", AssigntoID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@TskOwnnnAssignFromID", AssignFromID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nTaskGroupID", GroupID, ParameterDirection.Input, SqlDbType.BigInt);
                    int _returnvalue = oDB.Execute("TM_IN_TaskAssign", oParameters);
                    if (_returnvalue > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

                return false;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
            }
        }

//changes made for adding responsibility,ownership to task
        private bool SaveTaskAssignmentsForTracktaskIntuitMessage(TaskAssign oTaskAssign,bool isFromTrackTask= false ,bool isTaskForwardedFromIntuitMessage = false)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();


            try
            {
                if (oTaskAssign != null)
                {
                    //Table -> dbo.TM_Task_Assign
                    //nTaskID,nAssignToID numeric(18, 0),nAssignFromID numeric(18, 0),nSelfAssigned smallint,nAcceptRejectHold smallint,
                    //nClinicID numeric(18, 0)
                    oDB.Connect(false);
                    oParameters.Add("@nTaskID", oTaskAssign.TaskID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nAssignToID", oTaskAssign.AssignToID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nAssignFromID", oTaskAssign.AssignFromID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nSelfAssigned", oTaskAssign.SelfAssigned, ParameterDirection.Input, SqlDbType.SmallInt);
                    oParameters.Add("@nAcceptRejectHold", oTaskAssign.AcceptRejectHold, ParameterDirection.Input, SqlDbType.SmallInt);
                    oParameters.Add("@nClinicID", oTaskAssign.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@blnisFromTrackTask", isFromTrackTask, ParameterDirection.Input, SqlDbType.Bit );
                    oParameters.Add("@isTaskForwardedFromIntuitMessage", isTaskForwardedFromIntuitMessage, ParameterDirection.Input, SqlDbType.Bit);

                    int _returnvalue = oDB.Execute("TM_IN_TaskAssign_TracktaskIntuitMessage", oParameters);
                    if (_returnvalue > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

                return false;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
            }
        }



    //     Private Sub LoadDisplaySettings()
    //    Dim Con As SqlConnection = Nothing
    //    Dim cmd As SqlCommand = Nothing
    //    Dim da As SqlDataAdapter = Nothing ''slr new not needed 
    //    Dim dt As DataTable = Nothing ''slr new not needed 
    //    Try
    //        'Get Users Display Settings
    //        Con = New SqlConnection(GetConnectionString)
    //        cmd = New SqlCommand("gsp_GET_UserDisplaySettings", Con)
    //        cmd.CommandType = CommandType.StoredProcedure
    //        Dim objParam As SqlParameter

    //        objParam = cmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
    //        objParam.Direction = ParameterDirection.Input
    //        objParam.Value = gnLoginID

    //        objParam = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar)
    //        objParam.Direction = ParameterDirection.Input
    //        objParam.Value = gstrClientMachineName

    //        da = New SqlDataAdapter
    //        dt = New DataTable
    //        da.SelectCommand = cmd
    //        da.Fill(dt)

    //        'Load Display Settings
    //        If IsNothing(dt) = False Then
    //            If dt.Rows.Count > 0 Then
    //                If IsDBNull(dt.Rows(0)("iStyle")) = False Then
    //                    Dim oBytesArry As Byte() = CType(dt.Rows(0)("iStyle"), Byte())
    //                    Dim memStream As MemoryStream = New MemoryStream(oBytesArry)
    //                    uiPanelManager1.LoadLayoutFile(memStream)
    //                    'SLR: clear obytesarray
    //                    If Not oBytesArry Is Nothing Then
    //                        oBytesArry = Nothing
    //                    End If
    //                    memStream.Close()
    //                    memStream.Dispose()
    //                    memStream = Nothing
    //                End If
    //            End If
    //        End If
    //    Catch ex As SqlException
    //        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    //        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Load, "LoadDisplaySettings -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    //    Catch ex As Exception
    //        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Load, "LoadDisplaySettings -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    //        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    //    Finally
    //        If Not IsNothing(dt) Then
    //            dt.Dispose()
    //            dt = Nothing
    //        End If
    //        If Not IsNothing(da) Then
    //            da.Dispose()
    //            da = Nothing
    //        End If
    //        If Not IsNothing(cmd) Then
    //            cmd.Parameters.Clear()
    //            cmd.Dispose()
    //            cmd = Nothing
    //        End If
    //        If Not IsNothing(Con) Then
    //            If Con.State = ConnectionState.Open Then
    //                Con.Close()
    //            End If
    //            Con.Dispose()
    //            Con = Nothing
    //        End If
    //    End Try
    //End Sub

        public object  LoadDisplaySettings(long UserID,string MachineName)
        {
          gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt = null;
             
                    
            try
            {
               
                    oDB.Connect(false);
                    oParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sMachineName", MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                  
                    oParameters.Add("@nDisplayType", 1, ParameterDirection.Input, SqlDbType.Int);
                 
                    oDB.Retrive ("gsp_GET_UserDisplaySettings",oParameters,  out  dt);
                    oParameters.Clear(); 
    
                if (dt!=null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                  //Byte[] oBytesArry=  (byte[])( dt.Rows[0]["istyle"]);
                            return dt.Rows[0]["istyle"];
                        }
                        
                    }
                    else
                    {
                        return null;
                    }

                }
               

            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
                
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
            }
            return null;
        }

        public void SaveDisplaySettings(MemoryStream stream,Int64 loginID,string Machinename)
        {

            SqlTransaction trn = null;
            SqlConnection Con = new SqlConnection(_databaseconnectionstring);
            SqlCommand cmd = null;
            SqlParameter objParam = null;


            try
            {
                //Save Display Settings To MemoryStream
                //MemoryStream memStream = default(MemoryStream);
                //memStream = new MemoryStream();
                //uiPanelManager1.SaveLayoutFile(memStream);

                //-----------------Save MemoryStream To database
                Con.Open();

                trn = Con.BeginTransaction();

                //Delete Previous Settings
                cmd = new SqlCommand("gsp_DELETE_UserDisplaySettings", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trn;

                objParam = cmd.Parameters.Add("@nUserID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = loginID;

                objParam = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = Machinename ;

                objParam = cmd.Parameters.Add("@nDisplayType", SqlDbType.VarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = 1;

                cmd.ExecuteNonQuery();
                //'Slr dispose cmd
                if ((cmd != null))
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                //Save New Settings
                cmd = new SqlCommand("gsp_INUP_UserDisplaySettings", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trn;

                objParam = cmd.Parameters.Add("@nUserID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = loginID;

                objParam = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = Machinename;

                objParam = cmd.Parameters.Add("@nPanel", SqlDbType.Int);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = 0;

                objParam = cmd.Parameters.Add("@nSize", SqlDbType.Decimal);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = 0;

                objParam = cmd.Parameters.Add("@iStyle", SqlDbType.Image);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = stream.ToArray();


                objParam = cmd.Parameters.Add("@nDisplayType", SqlDbType.Int);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = 1;

                // Con.Open()
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                stream.Dispose();
                trn.Commit();

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Add, "Save_UserDisplaySettings--"  + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString() , _messageBoxCaption , MessageBoxButtons.OK, MessageBoxIcon.Error);
                trn.Rollback();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Add, "Save_UserDisplaySettings -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(),_messageBoxCaption , MessageBoxButtons.OK, MessageBoxIcon.Error);
                trn.Rollback();

            }
            finally
            {

                //'slr dispose cmd
                if ((cmd != null))
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                //'slr dispose trn
                if ((trn != null))
                {
                    trn.Dispose();
                }
                //'slr dispose con
                if ((Con != null))
                {
                    if (Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }
                    Con.Dispose();
                }
                Con = null;
                trn = null;
                Con = null;
            }
        }

       

  
        
        
        
        
        
        private bool SaveTaskProgress(TaskProgress oTaskProgress)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                if (oTaskProgress != null)
                {
                    //Table -> TM_Task_Progress
                    //nTaskID numeric(18, 0),@nStatusID numeric(18, 0),@dComplete decimal(18, 0),@sDescription varchar(200),
                    //@nDateTime numeric(18, 0),@nClinicID numeric(18, 0)
                    oDB.Connect(false);
                    oParameters.Add("@nTaskID", oTaskProgress.TaskID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nStatusID", oTaskProgress.StatusID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@dComplete", oTaskProgress.Complete, ParameterDirection.Input, SqlDbType.Decimal);
                    oParameters.Add("@sDescription", oTaskProgress.Description, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nDateTime", oTaskProgress.DateTime, ParameterDirection.Input, SqlDbType.DateTime);
                    oParameters.Add("@nClinicID", oTaskProgress.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    int _returnresult = oDB.Execute("TM_IN_TaskProgress", oParameters);

                    if (_returnresult > 0)
                        return true;
                    else
                        return false;

                }

                return false;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
            }


        }

        #endregion " Add Task Method "

        #region " Modify Methods "

        //public Int64 Modify(Task oTask)
        //{
        //    //While Modifying Task we will Update the TaskMaster & TaskProgress Entries
        //    //But if the Task is Modified to assign some other User then we will 
        //    //delete the entry of the Task from the TM_Task_Assign Table & will 
        //    //make new entries according to the assignment in modification.


        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //    Int64 _result = 0;
        //    Int64 _ReturnTaskID = 0;
        //    Object oResult = new object();
        //    TaskAssign oTaskAssign;
        //    TaskProgress oTaskProgress;

        //    try
        //    {
        //        oResult = null;
        //        oDB.Connect(false);

        //        #region " Task Master Modify "
        //        //Table -> TM_TaskMST
        //        //nTaskID,nProviderID,nPatientID,sSubject,nStartDate,nDueDate,nPriorityID,nCategoryID
        //        //nFollowUpID,bIsPrivate,nOwnerID,nDateCreated,sNoteExt,nUserID,nClinicID

        //        oParameters.Add("@nTaskID", oTask.TaskID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //        oParameters.Add("@nProviderID", oTask.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nPatientID", oTask.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@sSubject", oTask.Subject, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@nStartDate", oTask.StartDate, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nDueDate", oTask.DueDate, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nPriorityID", oTask.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nCategoryID", oTask.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nFollowUpID", oTask.FollowupID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@bIsPrivate", oTask.IsPrivate, ParameterDirection.Input, SqlDbType.Bit);
        //        oParameters.Add("@nOwnerID", oTask.OwnerID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nDateCreated", oTask.DateCreated, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@sNoteExt", oTask.Notes, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@nUserID", oTask.UserID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nTaskType", oTask.TaskType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //        oParameters.Add("@sFaxTiffFileName", oTask.FaxTiffFileName, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@sMachineName", oTask.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@nReferenceID1", oTask.ReferenceID1, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nReferenceID2", oTask.ReferenceID2, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nClinicID", oTask.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //        //oParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //        //First update entry for Task Master. 
        //        _result = oDB.Execute("TM_UP_Task", oParameters, out oResult);


        //        //Check for proper entry in Task Master otherwise abort
        //        if (_result == 0 && oResult == null)
        //        {
        //            MessageBox.Show("ERROR : Modifying Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return 0;

        //        }
        //        else
        //        {
        //            // Get the TaskID for the update Task.
        //            _ReturnTaskID = Convert.ToInt64(oResult);

        //            //Delete the previous assignments for this Task.
        //            //i.e Delete assignment entry for the task from TM_Task_Assign Table
        //            string strDeleteAssignment;
        //            //if (isFromTrackTask)
        //            //{
        //            //    strDeleteAssignment = " delete TM_Task_Assign where nTaskID=" + _ReturnTaskID;
        //            //}
        //            //else
        //            {

        //                strDeleteAssignment = " delete TM_Task_Assign where nTaskID=" + _ReturnTaskID + " AND nSelfAssigned = 1";
        //            }

        //            int _delResult = oDB.Execute_Query(strDeleteAssignment);
        //            if (_delResult <= 0)
        //            {
        //                //MessageBox.Show("Error : Updating Record.",_messageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Error);
        //                return 0;
        //            }

        //        }
        //        #endregion " Task Master Modify "

        //        #region " Assignment Updates "

        //        // * Make entry for the Assignments 

        //        for (int i = 0; i < oTask.Assignment.Count; i++)
        //        {
        //            oTaskAssign = new TaskAssign();
        //            oTaskAssign = oTask.Assignment[i];
        //            //Assign the TaskID of the saved Task_MST Task (from above)
        //            oTaskAssign.TaskID = _ReturnTaskID;

        //            bool result = SaveTaskAssignments(oTaskAssign);

        //            if (!result)
        //            {
        //                //TODO : if not sucessful we can delete the MaterTable entry from here as we have 
        //                //the TaskID (equivalent to rollback).

        //                MessageBox.Show("ERROR : Adding Task Assignments Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return 0;
        //            }

        //            oTaskAssign.Dispose();

        //        }

        //        #endregion " Assignment Updates "

        //        #region " Progress Updates "
        //        // * Make entry for the Task Progress

        //        oTaskProgress = new TaskProgress();
        //        oTaskProgress = oTask.Progress;
        //        oTaskProgress.TaskID = _ReturnTaskID;
        //        oTaskProgress.Description = oTask.Progress.Description;    


        //        bool retresult = ModifyTaskProgress(oTaskProgress);

        //        if (!retresult)
        //        {
        //            //TODO : if not sucessful we can delete the MaterTable entry from here as we have 
        //            //the TaskID (equivalent to rollback).

        //            MessageBox.Show("ERROR : Adding Task Progress Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return 0;

        //        }
        //        oTaskProgress.Dispose();

        //        #endregion " Progress Updates "


        //        return _ReturnTaskID;

        //    }
        //    catch (gloDatabaseLayer.DBException dbErr)
        //    {
        //        //MessageBox.Show("ERROR :" + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        dbErr.ERROR_Log(dbErr.ToString());
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR :" + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return 0;

        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //        oParameters.Dispose();
        //        oResult = null;
        //    }


        //}//public Int64 Modify(Task oTask)

        //modified by pradeep 20100924 -from 5060_HF 0r 5061
        public Int64 Modify(Task oTask)
        {
            //While Modifying Task we will Update the TaskMaster & TaskProgress Entries
            //But if the Task is Modified to assign some other User then we will 
            //delete the entry of the Task from the TM_Task_Assign Table & will 
            //make new entries according to the assignment in modification.
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Int64 _ReturnTaskID = 0;
            Int64 _TmpReturnTaskID = 0;
            Object oResult = new object();
            TaskAssign oTaskAssign;
            TaskProgress oTaskProgress;
            string strDeleteAssignment;
            bool bisowner = false;
            Int64 nAssignToID = 0;
              Int64 nAssignFromID=0;
              DataTable dttaskOwner;
            // check the autoacceptTask setting added by pradeep
            object oResultExplictlyAcceptTask = null;
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            ogloSettings.GetSetting("ExplicitlyAcceptTask", out oResultExplictlyAcceptTask);
            ogloSettings.Dispose();
            ogloSettings = null;
            Int32 _ExplictlyAcceptTask = 0;
            if (oResultExplictlyAcceptTask != null && !string.IsNullOrEmpty(oResultExplictlyAcceptTask.ToString()))
            {
                _ExplictlyAcceptTask = Convert.ToInt32(oResultExplictlyAcceptTask);
            }
            try
            {
                oResult = null;
                oDB.Connect(false);
                if (_ExplictlyAcceptTask != 0)
                {


                    #region " Task Master Modify "
                    //Table -> TM_TaskMST
                    //nTaskID,nProviderID,nPatientID,sSubject,nStartDate,nDueDate,nPriorityID,nCategoryID
                    //nFollowUpID,bIsPrivate,nOwnerID,nDateCreated,sNoteExt,nUserID,nClinicID

                    oParameters.Add("@nTaskID", oTask.TaskID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oParameters.Add("@nProviderID", oTask.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPatientID", oTask.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sSubject", oTask.Subject, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nStartDate", oTask.StartDate, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nDueDate", oTask.DueDate, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPriorityID", oTask.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nCategoryID", oTask.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nFollowUpID", oTask.FollowupID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@bIsPrivate", oTask.IsPrivate, ParameterDirection.Input, SqlDbType.Bit);
                    oParameters.Add("@nOwnerID", oTask.OwnerID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nDateCreated", oTask.DateCreated, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sNoteExt", oTask.Notes, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nUserID", oTask.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nTaskType", oTask.TaskType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oParameters.Add("@sFaxTiffFileName", oTask.FaxTiffFileName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sMachineName", oTask.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nReferenceID1", oTask.ReferenceID1, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nReferenceID2", oTask.ReferenceID2, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID", oTask.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nTaskGroupID", oTask.TaskGroupID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    //First update entry for Task Master. 
                    _result = oDB.Execute("TM_UP_Task", oParameters, out oResult);







                    //Check for proper entry in Task Master otherwise abort
                    if (_result == 0 && oResult == null)
                    {
                        MessageBox.Show("ERROR : Modifying Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;

                    }
                    else
                    {
                        // Get the TaskID for the update Task.
                        _ReturnTaskID = Convert.ToInt64(oResult);

                        //Delete the previous assignments for this Task.
                        //i.e Delete assignment entry for the task from TM_Task_Assign Table

                        if (isFromTrackTask)
                        {
                            strDeleteAssignment = " delete TM_Task_Assign where nTaskID=" + _ReturnTaskID;
                        }
                        else
                        {

                            strDeleteAssignment = " delete TM_Task_Assign where nTaskID=" + _ReturnTaskID + " AND nSelfAssigned = 1";
                        }
                      dttaskOwner=  CheckTaskOwnerID(_ReturnTaskID,  ! isFromTrackTask);
                        int _delResult = oDB.Execute_Query(strDeleteAssignment);
                        //Commented by sanjog 2011 Jan 29 to show task
                        //if (_delResult <= 0)
                        //{
                        //    //MessageBox.Show("Error : Updating Record.",_messageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Error);
                        //    return 0;
                        //}
                        //Commented by sanjog 2011 Jan 29 to show task
                    }
                    #endregion " Task Master Modify "

                    #region " Assignment Updates "

                    // * Make entry for the Assignments 
                    if (dttaskOwner != null)
                    {
                        if (dttaskOwner.Rows.Count > 0)
                        {
                            bisowner =Convert.ToBoolean(   dttaskOwner.Rows[0]["Column1"]);
                            nAssignToID = Convert.ToInt64(dttaskOwner.Rows[0]["Column2"]);
                            nAssignFromID = Convert.ToInt64(dttaskOwner.Rows[0]["Column3"]);
                        }
                    }
                    for (int i = 0; i < oTask.Assignment.Count; i++)
                    {
                        oTaskAssign = new TaskAssign();
                        oTaskAssign = oTask.Assignment[i];
                        //Assign the TaskID of the saved Task_MST Task (from above)
                        oTaskAssign.TaskID = _ReturnTaskID;
                        bool result =false ;
                       //changes made for adding responsibility to task,ownership to task
                            result = SaveTaskAssignments(oTaskAssign, bisowner, nAssignToID, nAssignFromID);
                       
                        if (!result)
                        {
                            //TODO : if not sucessful we can delete the MaterTable entry from here as we have 
                            //the TaskID (equivalent to rollback).

                            MessageBox.Show("ERROR : Adding Task Assignments Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return 0;
                        }

                        oTaskAssign.Dispose();

                    }

                    #endregion " Assignment Updates "

                    #region " Progress Updates "
                    // * Make entry for the Task Progress

                    oTaskProgress = new TaskProgress();
                    oTaskProgress = oTask.Progress;
                    oTaskProgress.TaskID = _ReturnTaskID;
                    oTaskProgress.Description = oTask.Progress.Description;

                    bool retresult = false;
                    retresult = ModifyTaskProgress(oTaskProgress);


                    if (frmTask._IsCompleteAllTask == true)
                    {
                        CompleteAll(_ReturnTaskID);
                    }


                    if (!retresult)
                    {
                        //TODO : if not sucessful we can delete the MaterTable entry from here as we have 
                        //the TaskID (equivalent to rollback).

                        MessageBox.Show("Task cannot be saved as it might be deleted.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;

                    }
                    oTaskProgress.Dispose();

                    #endregion " Progress Updates "

                }
                else
                {
                    //setting on
                    #region " Auto Accept save method "
                    oResult = null;
                    oDB.Connect(false);

                    #region " Save Assignment "

                    // * Make entry for the Assignments 
                    for (int i = 0; i < oTask.Assignment.Count; i++)
                    {

                        #region " Save Master "
                        //Table -> TM_TaskMST
                        //nTaskID,nProviderID,nPatientID,sSubject,nStartDate,nDueDate,nPriorityID,nCategoryID
                        //nFollowUpID,bIsPrivate,nOwnerID,nDateCreated,sNoteExt,nUserID,nClinicID
                        //if (isFromTrackTask)
                        //{

                        //    strDeleteAssignment = " delete TM_Task_Assign where nTaskID=" + oTask.TaskID + " AND nAssignToID = " + oTask.Assignment[i].AssignToID + " ";
                        //}
                        //else if(isTaskForwardedFromIntuitMessage)
                        //{
                        //    //delete the task from the user who is forwarding it
                        //    strDeleteAssignment = " delete TM_Task_Assign where nTaskID=" + oTask.TaskID + " AND nSelfAssigned = 1  AND nAssignToID = " + oTask.Assignment[i].AssignFromID + " ";
                        //}
                        //else
                        //{
                        //    strDeleteAssignment = " delete TM_Task_Assign where nTaskID=" + oTask.TaskID + " AND nSelfAssigned = 1  AND nAssignToID = " + oTask.Assignment[i].AssignToID + " ";
                        //}


                       

                     //   int _delResult = oDB.Execute_Query(strDeleteAssignment);
                       
                        //Pass TaskID = 0 for adding new record.
                        if (oTask.Assignment[i].AssignToID == oTask.OwnerID)
                        {
                            #region " Task Master Modify "
                            //Table -> TM_TaskMST
                            //nTaskID,nProviderID,nPatientID,sSubject,nStartDate,nDueDate,nPriorityID,nCategoryID
                            //nFollowUpID,bIsPrivate,nOwnerID,nDateCreated,sNoteExt,nUserID,nClinicID
                            oParameters.Clear();
                            oParameters.Add("@nTaskID", oTask.TaskID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            oParameters.Add("@nProviderID", oTask.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nPatientID", oTask.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@sSubject", oTask.Subject, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@nStartDate", oTask.StartDate, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nDueDate", oTask.DueDate, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nPriorityID", oTask.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nCategoryID", oTask.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nFollowUpID", oTask.FollowupID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@bIsPrivate", oTask.IsPrivate, ParameterDirection.Input, SqlDbType.Bit);
                            oParameters.Add("@nOwnerID", oTask.OwnerID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nDateCreated", oTask.DateCreated, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@sNoteExt", oTask.Notes, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@nUserID", oTask.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nTaskType", oTask.TaskType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oParameters.Add("@sFaxTiffFileName", oTask.FaxTiffFileName, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@sMachineName", oTask.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@nReferenceID1", oTask.ReferenceID1, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nReferenceID2", oTask.ReferenceID2, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nClinicID", oTask.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nTaskGroupID", oTask.TaskGroupID, ParameterDirection.Input, SqlDbType.BigInt);
                            //oParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                            //First update entry for Task Master. 
                            _result = oDB.Execute("TM_UP_Task", oParameters, out oResult);


                            //Check for proper entry in Task Master otherwise abort
                            if (_result == 0 && oResult == null)
                            {
                                MessageBox.Show("ERROR : Modifying Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return 0;

                            }
                            else
                            {
                                // Get the TaskID for the update Task.
                                _ReturnTaskID = Convert.ToInt64(oResult);
                                _TmpReturnTaskID = Convert.ToInt64(oResult);

                                //Delete the previous assignments for this Task.
                                //i.e Delete assignment entry for the task from TM_Task_Assign Table


                            }
                            #endregion " Task Master Modify "

                        } //   if (oTask.Assignment[i].AssignToID== oTask.OwnerID )
                        else
                        {
                            // if (oTask.Assignment[i].AssignToID != oTask.OwnerID)
                            # region"Delete Task"
                            if (oTask.TaskID != _TmpReturnTaskID && _TmpReturnTaskID != 0)
                            {
                                DeleteTask(oTask.TaskID);
                            }
                            #endregion

                            #region "Create New Task"
                            oParameters.Clear();
                            oParameters.Add("@nTaskID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            oParameters.Add("@nProviderID", oTask.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nPatientID", oTask.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@sSubject", oTask.Subject, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@nStartDate", oTask.StartDate, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nDueDate", oTask.DueDate, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nPriorityID", oTask.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nCategoryID", oTask.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nFollowUpID", oTask.FollowupID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@bIsPrivate", oTask.IsPrivate, ParameterDirection.Input, SqlDbType.Bit);
                            oParameters.Add("@nOwnerID", oTask.OwnerID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nDateCreated", oTask.DateCreated, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@sNoteExt", oTask.Notes, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@nUserID", oTask.Assignment[i].AssignToID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nTaskType", oTask.TaskType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oParameters.Add("@sFaxTiffFileName", oTask.FaxTiffFileName, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@sMachineName", oTask.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@nReferenceID1", oTask.ReferenceID1, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nReferenceID2", oTask.ReferenceID2, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nClinicID", oTask.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nTaskGroupID", oTask.TaskGroupID, ParameterDirection.Input, SqlDbType.BigInt);
                            //First make entry for Task Master & get the TaskID for this saved Task.
                            _result = oDB.Execute("TM_IN_Task", oParameters, out oResult);

                            //Check for proper entry in Task Master otherwise abort
                            if (_result == 0 && oResult == null)
                            {
                                MessageBox.Show("ERROR : Adding Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return 0;

                            }
                            else
                            {
                                // Get the TaskID for the saved Task.
                                _ReturnTaskID = Convert.ToInt64(oResult);

                            }
                        }

                            #endregion

                        oTaskAssign = new TaskAssign();
                        oTaskAssign = oTask.Assignment[i];
                        oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                        oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                        //Assign the TaskID of the saved Task_MST Task (from above)
                        oTaskAssign.TaskID = _ReturnTaskID;
                        //changes made for adding responsibility to task,ownership to task
                        bool result = SaveTaskAssignmentsForTracktaskIntuitMessage(oTaskAssign, isFromTrackTask, isTaskForwardedFromIntuitMessage);

                        if (!result)
                        {
                            //TODO : if not sucessful we can delete the MaterTable entry from here as we have 
                            //the TaskID (equivalent to rollback).

                            MessageBox.Show("ERROR : Adding Task Assignments Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return 0;
                        }

                        oTaskAssign.Dispose();


                        #region " Save Progress "
                        // * Make entry for the Task Progress


                        oTaskProgress = new TaskProgress();
                        oTaskProgress = oTask.Progress;
                        oTaskProgress.TaskID = _ReturnTaskID;
                        oTaskProgress.ClinicID = oTask.ClinicID;
                        bool retresult;
                        if (oTask.Assignment[i].AssignToID == oTask.OwnerID)
                        {
                            retresult = ModifyTaskProgress(oTaskProgress);
                            if (frmTask._IsCompleteAllTask == true)
                            {
                                CompleteAll(_ReturnTaskID);
                            }
                        }
                        else
                        {
                            retresult = SaveTaskProgress(oTaskProgress);
                        }


                        //if (!retresult)
                        // {
                        //     //TODO : if not sucessful we can delete the MaterTable entry from here as we have 
                        //     //the TaskID (equivalent to rollback).
                        //     MessageBox.Show("ERROR : Adding Task Progress Record. Try Again", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //     return 0;
                        // }
                        oTaskProgress.Dispose();
                        oParameters.Dispose();
                        #endregion



                    }
                        #endregion "Create New Task"
                    #endregion



                    #endregion

                }



                return _ReturnTaskID;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                //MessageBox.Show("ERROR :" + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dbErr.ERROR_Log(dbErr.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR :" + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                oResult = null;
            }


        }

        private DataTable  CheckTaskOwnerID(Int64 TaskId, bool selfassigned)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
              DataTable dtdata=null;
                    //Table -> TM_Task_Progress
                    //nTaskID numeric(18, 0),@nStatusID numeric(18, 0),@dComplete decimal(18, 0),@sDescription varchar(200),
                    //@nDateTime numeric(18, 0),@nClinicID numeric(18, 0)
                    oDB.Connect(false);
                    oParameters.Add("@nTaskID", TaskId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nSelfAssigned", selfassigned, ParameterDirection.Input, SqlDbType.Bit );
               
                  oDB.Retrive ("gsp_gettaskOwner", oParameters,out dtdata);

                  return dtdata;
                }

            
          
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
            }

        }

        private bool ModifyTaskProgress(TaskProgress oTaskProgress)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                if (oTaskProgress != null)
                {
                    //Table -> TM_Task_Progress
                    //nTaskID numeric(18, 0),@nStatusID numeric(18, 0),@dComplete decimal(18, 0),@sDescription varchar(200),
                    //@nDateTime numeric(18, 0),@nClinicID numeric(18, 0)
                    oDB.Connect(false);
                    oParameters.Add("@nTaskID", oTaskProgress.TaskID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nStatusID", oTaskProgress.StatusID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@dComplete", oTaskProgress.Complete, ParameterDirection.Input, SqlDbType.Decimal);
                    oParameters.Add("@sDescription", oTaskProgress.Description, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nDateTime", oTaskProgress.DateTime, ParameterDirection.Input, SqlDbType.DateTime);
                    oParameters.Add("@nClinicID", oTaskProgress.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    int _returnresult = oDB.Execute("TM_UP_TaskProgress", oParameters);

                    if (_returnresult > 0)
                        return true;
                    else
                        return false;

                }

                return false;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
            }


        }

        //private bool ModifyTaskProgress(TaskProgress oTaskProgress,Boolean _IsCompleteAll)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

        //    try
        //    {
        //        if (oTaskProgress != null)
        //        {
        //            //Table -> TM_Task_Progress
        //            //nTaskID numeric(18, 0),@nStatusID numeric(18, 0),@dComplete decimal(18, 0),@sDescription varchar(200),
        //            //@nDateTime numeric(18, 0),@nClinicID numeric(18, 0)
        //            oDB.Connect(false);
        //            oParameters.Add("@nTaskID", oTaskProgress.TaskID, ParameterDirection.Input, SqlDbType.BigInt);
        //            oParameters.Add("@nStatusID", oTaskProgress.StatusID, ParameterDirection.Input, SqlDbType.BigInt);
        //            oParameters.Add("@dComplete", oTaskProgress.Complete, ParameterDirection.Input, SqlDbType.Decimal);
        //            oParameters.Add("@sDescription", oTaskProgress.Description, ParameterDirection.Input, SqlDbType.VarChar);
        //            oParameters.Add("@nDateTime", oTaskProgress.DateTime, ParameterDirection.Input, SqlDbType.DateTime);
        //            oParameters.Add("@nClinicID", oTaskProgress.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //            oParameters.Add("@IsCompleteAll", _IsCompleteAll, ParameterDirection.Input, SqlDbType.Bit);
        //            int _returnresult = oDB.Execute("gsp_CompleteAll_Task", oParameters);

        //            if (_returnresult > 0)
        //                return true;
        //            else
        //                return false;

        //        }

        //        return false;
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //        oParameters.Dispose();
        //    }


        //}

        //Modify Priority of a Task 

        public Boolean ModifyTaskPriority(Int64 TaskId, Int64 PriorityID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                strQuery = "Update TM_TaskMST set nPriorityID = " + PriorityID + " where nTaskID = " + TaskId + " ";
                int _result = oDB.Execute_Query(strQuery);
                if (_result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();

            }
        }

        public Boolean ModifyTaskComplete(Int64 TaskId, Int64 Complete)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            Int32 _StatusID = 0;

            try
            {
                oDB.Connect(false);
                if (Complete == 0)
                {
                    _StatusID = gloTaskMail.frmTask.StatusType.NotStarted.GetHashCode();
                }
                else if (Complete > 0 && Complete < 100)
                {
                    _StatusID = gloTaskMail.frmTask.StatusType.InProgress.GetHashCode();
                }
                else if (Complete == 100)
                {
                    
                    _StatusID = gloTaskMail.frmTask.StatusType.Completed.GetHashCode();
                }
                //strQuery = "UPDATE TM_Task_Progress SET dComplete = " + Complete + " where nTaskID = " + TaskId + " ";
                //strQuery = "UPDATE TM_Task_Progress SET dComplete = " + Complete + ", nStatusID = " + Convert.ToInt64(gloTaskMail.frmTask.StatusType.Completed.GetHashCode()) + " where nTaskID = " + TaskId + " ";
                strQuery = "UPDATE TM_Task_Progress SET dComplete = " + Complete + ", nStatusID = " + _StatusID + " where nTaskID = " + TaskId + " ";
               
                int _result = oDB.Execute_Query(strQuery);
                if (_result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();

            }
        }

        #endregion " Modify Methods "

        #region " Delete Task Method "

        public bool DeleteRequestedTask(Int64 TaskId, Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                strQuery = " Update TM_Task_Assign set nAcceptRejectHold= 4 where nAssignToID=" + UserId + " AND nTaskID = " + TaskId + " ";
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : Deleting Task", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

        }

        public bool DeleteRequestedTask(Int64 TaskId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                strQuery = " Delete from TM_Task_Assign where nTaskID = " + TaskId + " ";
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : Deleting Task", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

        }

        public bool BlockTask(Int64 TaskId, Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "";
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

        }

        public bool CanDeleteTask(Int64 TaskId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //Check for if the Task Assigned is on Hold for the Assigned person if it is on Hold status for 
                //any of the assinged person the we cannot delete the Task.
                strQuery = "select count(nTaskID) from TM_Task_Assign where nAcceptRejectHold = " + gloTasksMails.Common.AcceptRejectHold.Hold.GetHashCode() + " AND nTaskID = " + TaskId + " ";
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool DeleteTask(Int64 TaskId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strQuery = "";
            int _result = 0;

            try
            {
                oDB.Connect(false);
                _strQuery = " delete from TM_Task_Progress where nTaskID = " + TaskId + " ";
                _result = oDB.Execute_Query(_strQuery);
                _strQuery = " delete from TM_TaskMST where nTaskID = " + TaskId + " ";
                _result = oDB.Execute_Query(_strQuery);
                //To Remove Reminder for deleted task.
                if (_result == 1)
                {
                    _strQuery = " Update RM_Reminder_Mst set bIsDismissed= '" + true + "'  where nReferenceID = " + TaskId + " and nRefrenceType=2";
                    oDB.Execute_Query(_strQuery);
                }
                return Convert.ToBoolean(_result);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        #endregion " Delete Task Method "

        public bool BlockRequestedTask(Int64 TaskId, Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                strQuery = "UPDATE TM_Task_Assign SET bIsBlocked = '" + true + "' where nAssignToID = " + UserId + " AND nTaskID= " + TaskId + "";
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();

            }
        }

        // code to check whether task is assigned to multiple user or not - pradeep 20100722        
        public DataTable get_multiTask(long taskid)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTaskAssign = new DataTable();
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                // strQuery = "SELECT * FROM TM_Task_Assign WHERE nTaskID='" + taskid + "'"; //Remove select *
                strQuery = "SELECT nTaskID, nAssignToID, nAssignFromID, nSelfAssigned, nAcceptRejectHold, nClinicID, bIsBlocked, nNewTaskID FROM TM_Task_Assign WHERE nTaskID='" + taskid + "'";

                oDB.Retrive_Query(strQuery, out dtTaskAssign);
                return dtTaskAssign;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }

            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (dtTaskAssign != null)
                {
                    dtTaskAssign.Dispose();
                    dtTaskAssign = null;
                }
            }


        }

        #region "Common Task Methods "

        //public Tasks GetUserTasks(Int64 UserId)
        //{

        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    DataTable dtTask = new DataTable();
        //    Task oTask;
        //    Tasks oTasks = new Tasks();
        //    TaskAssign oAssign;
        //    TaskProgress oProgress;
        //    TaskAssigns oAssigns = new TaskAssigns();
        //    string strQuery = "";
        //    gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

        //    try
        //    {
        //        oDB.Connect(false);

        //        //strQuery = "select * from TM_Task_MST where nProviderID= " + ProviderId + " OR nAssignedToID=" + ProviderId + " ";

        //        /*
        //        strQuery = "SELECT DISTINCT TM_Task_Assign.nAssignToID, TM_Task_Assign.nAssignFromID, TM_Task_Assign.nSelfAssigned, "
        //                   + "TM_Task_Assign.nAcceptRejectHold,TM_Task_Progress.nStatusID, TM_Task_Progress.dComplete, "
        //                   + "TM_Task_Progress.sDescription, TM_Task_Progress.nDateTime,TM_TaskMST.nProviderID, TM_TaskMST.nPatientID, "
        //                   + "TM_TaskMST.sSubject, TM_TaskMST.nStartDate, TM_TaskMST.nDueDate,TM_TaskMST.nPriorityID, TM_TaskMST.nCategoryID, "
        //                   + "TM_TaskMST.nFollowUpID, TM_TaskMST.bIsPrivate, TM_TaskMST.nOwnerID,TM_TaskMST.nDateCreated, "
        //                   + "TM_TaskMST.sNoteExt, TM_TaskMST.nUserID, TM_TaskMST.nClinicID,TM_TaskMST.nTaskID "
        //                   + "FROM TM_Task_Progress RIGHT OUTER JOIN TM_TaskMST ON TM_Task_Progress.nTaskID = TM_TaskMST.nTaskID "
        //                   + "LEFT OUTER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID "
        //                   + "WHERE  TM_TaskMST.nUserID = " + UserId;
        //        */

        //         strQuery = "SELECT    TM_TaskMST.nTaskID,TM_TaskMST.nProviderID, TM_TaskMST.nPatientID, TM_TaskMST.sSubject, "
        //              + "TM_TaskMST.nStartDate , TM_TaskMST.nDueDate , TM_TaskMST.nPriorityID , TM_TaskMST.nCategoryID , "
        //              + "TM_TaskMST.nFollowUpID , TM_TaskMST.bIsPrivate , TM_TaskMST.nOwnerID , TM_TaskMST.nDateCreated , "
        //              + "TM_TaskMST.sNoteExt , TM_TaskMST.nUserID , TM_TaskMST.nClinicID , TM_Task_Progress.nStatusID, "
        //              + "TM_Task_Progress.dComplete, TM_Task_Progress.sDescription, TM_Task_Progress.nDateTime, TM_Task_Assign.nAssignToID, "
        //              + "TM_Task_Assign.nAssignFromID, TM_Task_Assign.nSelfAssigned, TM_Task_Assign.nAcceptRejectHold "
        //              + "FROM TM_TaskMST INNER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID INNER JOIN "
        //              + "TM_Task_Progress ON TM_TaskMST.nTaskID = TM_Task_Progress.nTaskID "
        //              + "WHERE (TM_Task_Assign.nSelfAssigned = 1 AND TM_Task_Assign.nAcceptRejectHold <> 4) AND (TM_TaskMST.nUserID = " + UserId + ") Order By TM_TaskMST.nDueDate desc";




        //        oDB.Retrive_Query(strQuery, out dtTask);
        //        if (dtTask != null && dtTask.Rows.Count > 0)
        //        {

        //            for (int i = 0; i <= dtTask.Rows.Count - 1; i++)
        //            {


        //                oTask = new Task();

        //                oTask.TaskID = Convert.ToInt64(dtTask.Rows[i]["nTaskID"]);

        //                oTask.ProviderID = Convert.ToInt64(dtTask.Rows[i]["nProviderID"]);
        //                gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
        //                if (oTask.ProviderID > 0)
        //                {
        //                    oTask.ProviderName = oResource.GetProviderName(oTask.ProviderID);
        //                }
        //                //oResource.Dispose();

        //                oTask.PatientID = Convert.ToInt64(dtTask.Rows[i]["nPatientID"]);
        //                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
        //                if (oTask.PatientID > 0)
        //                {
        //                    oTask.PatientName = ogloPatient.GetPatientName(oTask.PatientID);
        //                }
        //                ogloPatient.Dispose();


        //                oTask.Subject = Convert.ToString(dtTask.Rows[i]["sSubject"]);
        //                oTask.StartDate = Convert.ToInt64(dtTask.Rows[i]["nStartDate"]);
        //                oTask.DueDate = Convert.ToInt64(dtTask.Rows[i]["nDueDate"]);

        //                oTask.PriorityID = Convert.ToInt64(dtTask.Rows[i]["nPriorityID"]);
        //                gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
        //                oPriority = oTaskMail.GetPriority(oTask.PriorityID);
        //                oTask.Priority = oPriority.Description;
        //                oTask.PriorityLevel = oPriority.PriorityLevel;
        //                oPriority.Dispose();

        //                oTask.CategoryID = Convert.ToInt64(dtTask.Rows[i]["nCategoryID"]);
        //                gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
        //                oCategory = oTaskMail.GetCategory(oTask.CategoryID);
        //                oTask.Category = oCategory.Description;
        //                oCategory.Dispose();
        //                if (dtTask.Rows[0]["nFollowupID"] == DBNull.Value)
        //                {
        //                    oTask.FollowupID = 0;

        //                }
        //                else
        //                {
        //                    oTask.FollowupID = Convert.ToInt64(dtTask.Rows[0]["nFollowupID"]);
        //                    gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();
        //                    oFollowUp = oTaskMail.GetFollowUp(oTask.FollowupID);
        //                    oTask.Followup = oFollowUp.Description;
        //                    oFollowUp.Dispose();
        //                }


        //                oTask.IsPrivate = Convert.ToBoolean(dtTask.Rows[i]["bIsPrivate"]);
        //                oTask.OwnerID = Convert.ToInt64(dtTask.Rows[i]["nOwnerID"]);
        //                oTask.DateCreated = Convert.ToInt64(dtTask.Rows[i]["nDateCreated"]);
        //                oTask.Notes = dtTask.Rows[i]["sNoteExt"].ToString();
        //                oTask.UserID = Convert.ToInt64(dtTask.Rows[i]["nUserID"]);
        //                oTask.ClinicID = Convert.ToInt64(dtTask.Rows[i]["nClinicID"]);

        //                //Assign Setup
        //                oAssign = new TaskAssign();

        //                oAssign.AssignToID = Convert.ToInt64(dtTask.Rows[i]["nAssignToID"]);
        //                oAssign.AssignFromID = Convert.ToInt64(dtTask.Rows[i]["nAssignFromID"]);
        //                //TODO : AssignedToName & AssignedFromName
        //                oAssign.SelfAssigned = (gloTasksMails.Common.SelfAssigned)Convert.ToInt64(dtTask.Rows[i]["nSelfAssigned"]);
        //                oAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtTask.Rows[i]["nAcceptRejectHold"]);

        //                oTask.Assignment.Add(oAssign);

        //                //Progress Setup
        //                oProgress = new TaskProgress();

        //                //gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
        //                oProgress.StatusID = Convert.ToInt64(dtTask.Rows[i]["nStatusID"]);
        //                gloTasksMails.Common.Status oStatus = new gloTasksMails.Common.Status();
        //                oStatus = oTaskMail.GetStatus(oProgress.StatusID);
        //                oProgress.StatusName = oStatus.Description;
        //                oStatus.Dispose();

        //                oProgress.Complete = Convert.ToDecimal(dtTask.Rows[i]["dComplete"]);
        //                oProgress.Description = dtTask.Rows[i]["sDescription"].ToString();
        //                oProgress.DateTime = Convert.ToDateTime(dtTask.Rows[i]["nDateTime"]);

        //                oTask.Progress = oProgress;

        //                oTasks.Add(oTask);

        //                oProgress.Dispose();
        //                oTask.Dispose();
        //                oTaskMail.Dispose();

        //            }//END - for ( int i =0 ; i <= dtTask.Rows.Count -1 ; i++)

        //            return oTasks;

        //        }//END-if (dtTask != null && dtTask.Rows.Count > 0)

        //        return null;

        //    }//END - try
        //    catch (gloDatabaseLayer.DBException dbErr)
        //    {
        //        //MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        dbErr.ERROR_Log(dbErr.ToString());
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;

        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //        dtTask.Dispose();
        //    }
        //}  //END - GetUserTasks

        public Tasks GetUserTasks(Int64 UserId,string SearchString = " ")
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBps = new gloDatabaseLayer.DBParameters();
            DataTable dtTask = new DataTable();
            Task oTask;
            Tasks oTasks = new Tasks();
            //TaskAssign oAssign;
            TaskProgress oProgress;
            TaskAssigns oAssigns = new TaskAssigns();
            //string strQuery = "";
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);

                //strQuery = "select * from TM_Task_MST where nProviderID= " + ProviderId + " OR nAssignedToID=" + ProviderId + " ";

                /*
                strQuery = "SELECT DISTINCT TM_Task_Assign.nAssignToID, TM_Task_Assign.nAssignFromID, TM_Task_Assign.nSelfAssigned, "
                           + "TM_Task_Assign.nAcceptRejectHold,TM_Task_Progress.nStatusID, TM_Task_Progress.dComplete, "
                           + "TM_Task_Progress.sDescription, TM_Task_Progress.nDateTime,TM_TaskMST.nProviderID, TM_TaskMST.nPatientID, "
                           + "TM_TaskMST.sSubject, TM_TaskMST.nStartDate, TM_TaskMST.nDueDate,TM_TaskMST.nPriorityID, TM_TaskMST.nCategoryID, "
                           + "TM_TaskMST.nFollowUpID, TM_TaskMST.bIsPrivate, TM_TaskMST.nOwnerID,TM_TaskMST.nDateCreated, "
                           + "TM_TaskMST.sNoteExt, TM_TaskMST.nUserID, TM_TaskMST.nClinicID,TM_TaskMST.nTaskID "
                           + "FROM TM_Task_Progress RIGHT OUTER JOIN TM_TaskMST ON TM_Task_Progress.nTaskID = TM_TaskMST.nTaskID "
                           + "LEFT OUTER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID "
                           + "WHERE  TM_TaskMST.nUserID = " + UserId;
                */

        //        strQuery = "SELECT    TM_TaskMST.nTaskID,TM_TaskMST.nProviderID, TM_TaskMST.nPatientID, TM_TaskMST.sSubject, "
        //                 + "TM_TaskMST.nStartDate , TM_TaskMST.nDueDate , TM_TaskMST.nPriorityID , TM_TaskMST.nCategoryID , "
        //                 + "TM_TaskMST.nFollowUpID , TM_TaskMST.bIsPrivate , TM_TaskMST.nOwnerID , TM_TaskMST.nDateCreated , "
        //                 + "TM_TaskMST.sNoteExt , TM_TaskMST.nUserID , TM_TaskMST.nClinicID , TM_Task_Progress.nStatusID, "
        //                 + "TM_Task_Progress.dComplete, TM_Task_Progress.sDescription, TM_Task_Progress.nDateTime, TM_Task_Assign.nAssignToID, "
        //                 + "TM_Task_Assign.nAssignFromID, TM_Task_Assign.nSelfAssigned, TM_Task_Assign.nAcceptRejectHold ,"
                        
        //+ " Case when ISNULL(TM_TaskMST.nTaskGroupID,0)  = 0 then 'Task_Single'" 
        //+ " when ISNULL(TM_Task_Assign.bisownerassigned,0) = 0 and ISNULL(TM_Task_Assign.bisOwner,0) = 0 "
        //+ " then 'Task_NoOwner'"
        //    + " when ISNULL(TM_Task_Assign.bisownerassigned,0) = 1 and ISNULL(TM_Task_Assign.bisOwner,0) = 0 "
        //+ "then 'Task_OtherTaken' "
        //+ " when ISNULL(TM_Task_Assign.bisownerassigned,0) = 1 and ISNULL(TM_Task_Assign.bisOwner,0) = 1"
        //+ " then 'Task_Owner' end as Resp "
        //                 + " FROM TM_TaskMST INNER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID INNER JOIN "
        //                 + "TM_Task_Progress ON TM_TaskMST.nTaskID = TM_Task_Progress.nTaskID "
        //                 + "WHERE (TM_Task_Assign.nSelfAssigned = 1 AND TM_Task_Assign.nAcceptRejectHold <> 4) AND (TM_TaskMST.nUserID = " + UserId + ") Order By TM_TaskMST.nDueDate desc";



                oDBps.Add("@nUserId", UserId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBps.Add("@SearchString", SearchString, ParameterDirection.Input, SqlDbType.VarChar);

                //UpdatePILog("start query");
                oDB.Retrive("GetAllTask", oDBps, out dtTask);
                //UpdatePILog("end  query");
                if (dtTask != null && dtTask.Rows.Count > 0)
                {
                    //UpdatePILog("start Loop");
                    for (int i = 0; i <= dtTask.Rows.Count - 1; i++)
                    {


                        oTask = new Task();

                        oTask.TaskID = Convert.ToInt64(dtTask.Rows[i]["nTaskID"]);
                        //PatientName
                        oTask.PatientCode = dtTask.Rows[i]["PatientCode"].ToString();
                        oTask.ProviderID = Convert.ToInt64(dtTask.Rows[i]["nProviderID"]);
                        //gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                        //if (oTask.ProviderID > 0)
                        //{
                        //oTask.ProviderName = oResource.GetProviderName(oTask.ProviderID);
                        oTask.ProviderName = dtTask.Rows[i]["ProviderName"].ToString();
                        //}
                        //oResource.Dispose();

                        oTask.PatientID = Convert.ToInt64(dtTask.Rows[i]["nPatientID"]);
                        //gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                        //if (oTask.PatientID > 0)
                        //{
                        //oTask.PatientName = ogloPatient.GetPatientName(oTask.PatientID);
                        oTask.PatientName = dtTask.Rows[i]["PatientName"].ToString();
                        //}
                        //ogloPatient.Dispose();


                        oTask.Subject = Convert.ToString(dtTask.Rows[i]["sSubject"]);
                        oTask.StartDate = Convert.ToInt64(dtTask.Rows[i]["nStartDate"]);
                        oTask.DueDate = Convert.ToInt64(dtTask.Rows[i]["nDueDate"]);

                        oTask.PriorityID = Convert.ToInt64(dtTask.Rows[i]["nPriorityID"]);
                        gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
                        //oPriority = oTaskMail.GetPriority(oTask.PriorityID);
                        //oTask.Priority = oPriority.Description;
                        oTask.Priority = dtTask.Rows[i]["Priority"].ToString();
                        oTask.PriorityLevel = Convert.ToInt64(dtTask.Rows[i]["PriorityLevel"]);
                        oPriority.Dispose();

                        oTask.CategoryID = Convert.ToInt64(dtTask.Rows[i]["nCategoryID"]);
                        //  gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
                        //  oCategory = oTaskMail.GetCategory(oTask.CategoryID);
                        //  oTask.Category = oCategory.Description;
                        // oCategory.Dispose();
                        oTask.Category = Convert.ToString(dtTask.Rows[i]["Category"]);

                        oTask.FollowupID = Convert.ToInt64(dtTask.Rows[i]["nFollowupID"]);
                        //gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();
                        //oFollowUp = oTaskMail.GetFollowUp(oTask.FollowupID);
                        //oTask.Followup = oFollowUp.Description;
                        //oFollowUp.Dispose();
                        oTask.Followup = Convert.ToString(dtTask.Rows[i]["Followup"]);

                        oTask.IsPrivate = Convert.ToBoolean(dtTask.Rows[i]["bIsPrivate"]);
                        oTask.OwnerID = Convert.ToInt64(dtTask.Rows[i]["nOwnerID"]);
                        oTask.DateCreated = Convert.ToInt64(dtTask.Rows[i]["nDateCreated"]);
                        oTask.Notes = dtTask.Rows[i]["sNoteExt"].ToString();
                        oTask.UserID = Convert.ToInt64(dtTask.Rows[i]["nUserID"]);
                        oTask.ClinicID = Convert.ToInt64(dtTask.Rows[i]["nClinicID"]);
                        oTask.Resp = Convert.ToString(dtTask.Rows[i]["Resp"]);
                        
                         oTask.Assign_To = Convert.ToString(dtTask.Rows[i]["Assign To"]);

                        //Assign Setup
                        // oAssign = new TaskAssign();

                        //oAssign.AssignToID = Convert.ToInt64(dtTask.Rows[i]["nAssignToID"]);
                        //  oAssign.AssignFromID = Convert.ToInt64(dtTask.Rows[i]["nAssignFromID"]);
                        //TODO : AssignedToName & AssignedFromName
                        //  oAssign.SelfAssigned = (gloTasksMails.Common.SelfAssigned)Convert.ToInt64(dtTask.Rows[i]["nSelfAssigned"]);
                        // oAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtTask.Rows[i]["nAcceptRejectHold"]);

                        //  oTask.Assignment.Add(oAssign);


                        TaskAssign oTaskAssign = new TaskAssign();
                        oTaskAssign.AssignToID = Convert.ToInt64(dtTask.Rows[0]["nAssignToID"]);
                        oTaskAssign.AssignFromID = Convert.ToInt64(dtTask.Rows[0]["nAssignFromID"]);
                        oTaskAssign.SelfAssigned = (gloTasksMails.Common.SelfAssigned)Convert.ToInt64(dtTask.Rows[0]["nSelfAssigned"]);
                        oTaskAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtTask.Rows[0]["nAcceptRejectHold"]);
                        oTaskAssign.ClinicID = Convert.ToInt64(dtTask.Rows[0]["nClinicID"]);
                        oTask.Assignment.Add(oTaskAssign);

                        //Progress Setup
                        oProgress = new TaskProgress();
                        //gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
                        oProgress.StatusID = Convert.ToInt64(dtTask.Rows[i]["nStatusID"]);
                        // gloTasksMails.Common.Status oStatus = new gloTasksMails.Common.Status();
                        //  oStatus = oTaskMail.GetStatus(oProgress.StatusID);
                        //oStatus.Dispose();
                        oProgress.StatusName = dtTask.Rows[i]["status"].ToString();
                        //oProgress.DateTime = Convert.ToDateTime(dtTask.Rows[i]["nDateTime"]);
                        oProgress.Complete = Convert.ToInt64(dtTask.Rows[i]["dcomplete"]);
                       
                        oProgress.Assign_To = dtTask.Rows[i]["Assign To"].ToString();


                        oTask.Progress = oProgress;

                        oTasks.Add(oTask);

                        oProgress.Dispose();
                        oTask.Dispose();
                        oTaskMail.Dispose();

                    }//END - for ( int i =0 ; i <= dtTask.Rows.Count -1 ; i++)
                    //UpdatePILog("end Loop");
                    return oTasks;

                }//END-if (dtTask != null && dtTask.Rows.Count > 0)

                return null;

            }//END - try
            catch (gloDatabaseLayer.DBException dbErr)
            {
                //MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dbErr.ERROR_Log(dbErr.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtTask.Dispose();
            }
        }

        /// <summary>
        /// new function for id collection
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="CurrentDate"></param>
        /// <returns></returns>
        //string strSortOrder = "";
        //int colno = -1;
        //string colName = "";
        public Tasks GetUserTasksNew(string struserids="", string SearchString = " ")
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBps = new gloDatabaseLayer.DBParameters();
            DataTable dtTask = new DataTable();
            DataTable dtallTask = new DataTable();
           
            Task oTask;
            Tasks oTasks = new Tasks();
            //TaskAssign oAssign;
            TaskProgress oProgress;
            TaskAssigns oAssigns = new TaskAssigns();
            //string strQuery = "";
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);
                oDBps.Add("@nUserId", struserids, ParameterDirection.Input, SqlDbType.VarChar);
                //oDBps.Add("@nUserId", UserId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBps.Add("@SearchString", SearchString, ParameterDirection.Input, SqlDbType.VarChar);

                //UpdatePILog("start query");
                oDB.Retrive("GetAllTask", oDBps, out dtallTask);
                //UpdatePILog("end  query");
                if (dtallTask != null && dtallTask.Rows.Count > 0)
                {
              


             /// new code      
                   
                    //dtFilterData = dtTask;
               // string[] arruserids = { "Red", "Green", "Blue" };
               // var filteredRows = dtTaskRequest.AsEnumerable()
               //.Where(row => arruserids.Contains(row.Field<string>("Status")));


                    DataView dv = dtallTask.DefaultView;
                
                    //if (struserids.Trim() != "")  
                    //dv.RowFilter = "nAssignToID in(" + struserids + ")"; 
                    //dv.Sort = colName + " " + strSortOrder;
                    //dtallTask = dv.ToTable();
                    //dv.Dispose();
                    //dv = null;



                    dtTask = dv.ToTable();
                    ///new code end
                    ///


                    //UpdatePILog("start Loop");
                    for (int i = 0; i <= dtTask.Rows.Count - 1; i++)
                    {


                        oTask = new Task();

                        oTask.TaskID = Convert.ToInt64(dtTask.Rows[i]["nTaskID"]);
                        //PatientName
                        oTask.PatientCode = dtTask.Rows[i]["PatientCode"].ToString();
                        oTask.ProviderID = Convert.ToInt64(dtTask.Rows[i]["nProviderID"]);
                        //gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                        //if (oTask.ProviderID > 0)
                        //{
                        //oTask.ProviderName = oResource.GetProviderName(oTask.ProviderID);
                        oTask.ProviderName = dtTask.Rows[i]["ProviderName"].ToString();
                        //}
                        //oResource.Dispose();

                        oTask.PatientID = Convert.ToInt64(dtTask.Rows[i]["nPatientID"]);
                        //gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                        //if (oTask.PatientID > 0)
                        //{
                        //oTask.PatientName = ogloPatient.GetPatientName(oTask.PatientID);
                        oTask.PatientName = dtTask.Rows[i]["PatientName"].ToString();
                        //}
                        //ogloPatient.Dispose();


                        oTask.Subject = Convert.ToString(dtTask.Rows[i]["sSubject"]);
                        oTask.StartDate = Convert.ToInt64(dtTask.Rows[i]["nStartDate"]);
                        oTask.DueDate = Convert.ToInt64(dtTask.Rows[i]["nDueDate"]);

                        oTask.PriorityID = Convert.ToInt64(dtTask.Rows[i]["nPriorityID"]);
                        gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
                        //oPriority = oTaskMail.GetPriority(oTask.PriorityID);
                        //oTask.Priority = oPriority.Description;
                        oTask.Priority = dtTask.Rows[i]["Priority"].ToString();
                        oTask.PriorityLevel = Convert.ToInt64(dtTask.Rows[i]["PriorityLevel"]);
                        oPriority.Dispose();

                        oTask.CategoryID = Convert.ToInt64(dtTask.Rows[i]["nCategoryID"]);
                        //  gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
                        //  oCategory = oTaskMail.GetCategory(oTask.CategoryID);
                        //  oTask.Category = oCategory.Description;
                        // oCategory.Dispose();
                        oTask.Category = Convert.ToString(dtTask.Rows[i]["Category"]);

                        oTask.FollowupID = Convert.ToInt64(dtTask.Rows[i]["nFollowupID"]);
                        //gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();
                        //oFollowUp = oTaskMail.GetFollowUp(oTask.FollowupID);
                        //oTask.Followup = oFollowUp.Description;
                        //oFollowUp.Dispose();
                        oTask.Followup = Convert.ToString(dtTask.Rows[i]["Followup"]);

                        oTask.IsPrivate = Convert.ToBoolean(dtTask.Rows[i]["bIsPrivate"]);
                        oTask.OwnerID = Convert.ToInt64(dtTask.Rows[i]["nOwnerID"]);
                        oTask.DateCreated = Convert.ToInt64(dtTask.Rows[i]["nDateCreated"]);
                        oTask.Notes = dtTask.Rows[i]["sNoteExt"].ToString();
                        oTask.UserID = Convert.ToInt64(dtTask.Rows[i]["nUserID"]);
                        oTask.ClinicID = Convert.ToInt64(dtTask.Rows[i]["nClinicID"]);
                        oTask.Resp = Convert.ToString(dtTask.Rows[i]["Resp"]);

                        oTask.Assign_To = Convert.ToString(dtTask.Rows[i]["Assigned To"]);
                        //Assign Setup
                        // oAssign = new TaskAssign();

                        //oAssign.AssignToID = Convert.ToInt64(dtTask.Rows[i]["nAssignToID"]);
                        //  oAssign.AssignFromID = Convert.ToInt64(dtTask.Rows[i]["nAssignFromID"]);
                        //TODO : AssignedToName & AssignedFromName
                        //  oAssign.SelfAssigned = (gloTasksMails.Common.SelfAssigned)Convert.ToInt64(dtTask.Rows[i]["nSelfAssigned"]);
                        // oAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtTask.Rows[i]["nAcceptRejectHold"]);

                        //  oTask.Assignment.Add(oAssign);


                        //TaskAssign oTaskAssign = new TaskAssign();
                        //oTaskAssign.AssignToID = Convert.ToInt64(dtTask.Rows[0]["nAssignToID"]);
                        //oTaskAssign.AssignFromID = Convert.ToInt64(dtTask.Rows[0]["nAssignFromID"]);
                        //oTaskAssign.SelfAssigned = (gloTasksMails.Common.SelfAssigned)Convert.ToInt64(dtTask.Rows[0]["nSelfAssigned"]);
                        //oTaskAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtTask.Rows[0]["nAcceptRejectHold"]);
                        //oTaskAssign.ClinicID = Convert.ToInt64(dtTask.Rows[0]["nClinicID"]);
                        //oTask.Assignment.Add(oTaskAssign);

                        TaskAssign oTaskAssign = new TaskAssign();
                        oTaskAssign.AssignToID = Convert.ToInt64(dtTask.Rows[i]["nAssignToID"]);
                        oTaskAssign.AssignFromID = Convert.ToInt64(dtTask.Rows[i]["nAssignFromID"]);
                        oTaskAssign.SelfAssigned = (gloTasksMails.Common.SelfAssigned)Convert.ToInt64(dtTask.Rows[i]["nSelfAssigned"]);
                        oTaskAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtTask.Rows[i]["nAcceptRejectHold"]);
                        oTaskAssign.ClinicID = Convert.ToInt64(dtTask.Rows[i]["nClinicID"]);
                        oTask.Assignment.Add(oTaskAssign);
                        //Progress Setup
                        oProgress = new TaskProgress();
                        //gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
                        oProgress.StatusID = Convert.ToInt64(dtTask.Rows[i]["nStatusID"]);
                        // gloTasksMails.Common.Status oStatus = new gloTasksMails.Common.Status();
                        //  oStatus = oTaskMail.GetStatus(oProgress.StatusID);
                        //oStatus.Dispose();
                        oProgress.StatusName = dtTask.Rows[i]["status"].ToString();
                        //oProgress.DateTime = Convert.ToDateTime(dtTask.Rows[i]["nDateTime"]);
                        oProgress.Complete = Convert.ToInt64(dtTask.Rows[i]["dcomplete"]);

                      //  oProgress.Assign_To = dtTask.Rows[i]["Assigned To"].ToString();
                     

                        oTask.Progress = oProgress;

                        oTasks.Add(oTask);

                        oProgress.Dispose();
                        oTask.Dispose();
                        oTaskMail.Dispose();

                    }
                    //END - for ( int i =0 ; i <= dtTask.Rows.Count -1 ; i++)
                    //UpdatePILog("end Loop");
                    
                    return oTasks;

                }//END-if (dtTask != null && dtTask.Rows.Count > 0)

                return null;

            }//END - try
            catch (gloDatabaseLayer.DBException dbErr)
            {
                //MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dbErr.ERROR_Log(dbErr.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtTask.Dispose();
            }
        }



        public Tasks GetCurrentUserTasks(Int64 UserId, DateTime CurrentDate)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBps = new gloDatabaseLayer.DBParameters();
            DataTable dtTask = new DataTable();
            Task oTask;
            Tasks oTasks = new Tasks();
            //TaskAssign oAssign;
            TaskProgress oProgress;
            TaskAssigns oAssigns = new TaskAssigns();
            string strQuery = "";
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);

                //strQuery = "select * from TM_Task_MST where nProviderID= " + ProviderId + " OR nAssignedToID=" + ProviderId + " ";

                /*
                strQuery = "SELECT DISTINCT TM_Task_Assign.nAssignToID, TM_Task_Assign.nAssignFromID, TM_Task_Assign.nSelfAssigned, "
                           + "TM_Task_Assign.nAcceptRejectHold,TM_Task_Progress.nStatusID, TM_Task_Progress.dComplete, "
                           + "TM_Task_Progress.sDescription, TM_Task_Progress.nDateTime,TM_TaskMST.nProviderID, TM_TaskMST.nPatientID, "
                           + "TM_TaskMST.sSubject, TM_TaskMST.nStartDate, TM_TaskMST.nDueDate,TM_TaskMST.nPriorityID, TM_TaskMST.nCategoryID, "
                           + "TM_TaskMST.nFollowUpID, TM_TaskMST.bIsPrivate, TM_TaskMST.nOwnerID,TM_TaskMST.nDateCreated, "
                           + "TM_TaskMST.sNoteExt, TM_TaskMST.nUserID, TM_TaskMST.nClinicID,TM_TaskMST.nTaskID "
                           + "FROM TM_Task_Progress RIGHT OUTER JOIN TM_TaskMST ON TM_Task_Progress.nTaskID = TM_TaskMST.nTaskID "
                           + "LEFT OUTER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID "
                           + "WHERE  TM_TaskMST.nUserID = " + UserId;
                */

                strQuery = "SELECT    TM_TaskMST.nTaskID,TM_TaskMST.nProviderID, TM_TaskMST.nPatientID, TM_TaskMST.sSubject, "
                         + "TM_TaskMST.nStartDate , TM_TaskMST.nDueDate , TM_TaskMST.nPriorityID , TM_TaskMST.nCategoryID , "
                         + "TM_TaskMST.nFollowUpID , TM_TaskMST.bIsPrivate , TM_TaskMST.nOwnerID , TM_TaskMST.nDateCreated , "
                         + "TM_TaskMST.sNoteExt , TM_TaskMST.nUserID , TM_TaskMST.nClinicID , TM_Task_Progress.nStatusID, "
                         + "TM_Task_Progress.dComplete, TM_Task_Progress.sDescription, TM_Task_Progress.nDateTime, TM_Task_Assign.nAssignToID, "
                         + "TM_Task_Assign.nAssignFromID, TM_Task_Assign.nSelfAssigned, TM_Task_Assign.nAcceptRejectHold "
                         + "FROM TM_TaskMST INNER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID INNER JOIN "
                         + "TM_Task_Progress ON TM_TaskMST.nTaskID = TM_Task_Progress.nTaskID "
                         + "WHERE (TM_Task_Assign.nSelfAssigned = 1 AND TM_Task_Assign.nAcceptRejectHold <> 4) AND (TM_TaskMST.nUserID = " + UserId + ") Order By TM_TaskMST.nDueDate desc";



                oDBps.Add("@nUserId", UserId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBps.Add("@CurrentDate", CurrentDate, ParameterDirection.Input, SqlDbType.DateTime);

                //UpdatePILog("start query");
                oDB.Retrive("GetCurrentTask", oDBps, out dtTask);
                //UpdatePILog("end  query");
                if (dtTask != null && dtTask.Rows.Count > 0)
                {
                    //UpdatePILog("start Loop");
                    for (int i = 0; i <= dtTask.Rows.Count - 1; i++)
                    {


                        oTask = new Task();

                        oTask.TaskID = Convert.ToInt64(dtTask.Rows[i]["nTaskID"]);
                        //PatientName
                        oTask.PatientCode = dtTask.Rows[i]["PatientCode"].ToString();
                        oTask.ProviderID = Convert.ToInt64(dtTask.Rows[i]["nProviderID"]);
                        //gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                        //if (oTask.ProviderID > 0)
                        //{
                        //oTask.ProviderName = oResource.GetProviderName(oTask.ProviderID);
                        oTask.ProviderName = dtTask.Rows[i]["ProviderName"].ToString();
                        //}
                        //oResource.Dispose();

                        oTask.PatientID = Convert.ToInt64(dtTask.Rows[i]["nPatientID"]);
                        //gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                        //if (oTask.PatientID > 0)
                        //{
                        //oTask.PatientName = ogloPatient.GetPatientName(oTask.PatientID);
                        oTask.PatientName = dtTask.Rows[i]["PatientName"].ToString();
                        //}
                        //ogloPatient.Dispose();


                        oTask.Subject = Convert.ToString(dtTask.Rows[i]["sSubject"]);
                        oTask.StartDate = Convert.ToInt64(dtTask.Rows[i]["nStartDate"]);
                        oTask.DueDate = Convert.ToInt64(dtTask.Rows[i]["nDueDate"]);

                        oTask.PriorityID = Convert.ToInt64(dtTask.Rows[i]["nPriorityID"]);
                        gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
                        //oPriority = oTaskMail.GetPriority(oTask.PriorityID);
                        //oTask.Priority = oPriority.Description;
                        oTask.Priority = dtTask.Rows[i]["Priority"].ToString();
                        oTask.PriorityLevel = Convert.ToInt64(dtTask.Rows[i]["PriorityLevel"]);
                        oPriority.Dispose();

                        oTask.CategoryID = Convert.ToInt64(dtTask.Rows[i]["nCategoryID"]);
                        //  gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
                        //  oCategory = oTaskMail.GetCategory(oTask.CategoryID);
                        //  oTask.Category = oCategory.Description;
                        // oCategory.Dispose();
                        oTask.Category = Convert.ToString(dtTask.Rows[i]["Category"]);

                        oTask.FollowupID = Convert.ToInt64(dtTask.Rows[i]["nFollowupID"]);
                        //gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();
                        //oFollowUp = oTaskMail.GetFollowUp(oTask.FollowupID);
                        //oTask.Followup = oFollowUp.Description;
                        //oFollowUp.Dispose();
                        oTask.Followup = Convert.ToString(dtTask.Rows[0]["Followup"]);

                        oTask.IsPrivate = Convert.ToBoolean(dtTask.Rows[i]["bIsPrivate"]);
                        oTask.OwnerID = Convert.ToInt64(dtTask.Rows[i]["nOwnerID"]);
                        oTask.DateCreated = Convert.ToInt64(dtTask.Rows[i]["nDateCreated"]);
                        oTask.Notes = dtTask.Rows[i]["sNoteExt"].ToString();
                        oTask.UserID = Convert.ToInt64(dtTask.Rows[i]["nUserID"]);
                        oTask.ClinicID = Convert.ToInt64(dtTask.Rows[i]["nClinicID"]);
                        

                        //Assign Setup
                        // oAssign = new TaskAssign();

                        //oAssign.AssignToID = Convert.ToInt64(dtTask.Rows[i]["nAssignToID"]);
                        //  oAssign.AssignFromID = Convert.ToInt64(dtTask.Rows[i]["nAssignFromID"]);
                        //TODO : AssignedToName & AssignedFromName
                        //  oAssign.SelfAssigned = (gloTasksMails.Common.SelfAssigned)Convert.ToInt64(dtTask.Rows[i]["nSelfAssigned"]);
                        // oAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtTask.Rows[i]["nAcceptRejectHold"]);

                        //  oTask.Assignment.Add(oAssign);


                        TaskAssign oTaskAssign = new TaskAssign();
                        oTaskAssign.AssignToID = Convert.ToInt64(dtTask.Rows[0]["nAssignToID"]);
                        oTaskAssign.AssignFromID = Convert.ToInt64(dtTask.Rows[0]["nAssignFromID"]);
                        oTaskAssign.SelfAssigned = (gloTasksMails.Common.SelfAssigned)Convert.ToInt64(dtTask.Rows[0]["nSelfAssigned"]);
                        oTaskAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtTask.Rows[0]["nAcceptRejectHold"]);
                        oTaskAssign.ClinicID = Convert.ToInt64(dtTask.Rows[0]["nClinicID"]);
                        oTask.Assignment.Add(oTaskAssign);

                        //Progress Setup
                        oProgress = new TaskProgress();
                        //gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
                        oProgress.StatusID = Convert.ToInt64(dtTask.Rows[i]["nStatusID"]);
                        // gloTasksMails.Common.Status oStatus = new gloTasksMails.Common.Status();
                        //  oStatus = oTaskMail.GetStatus(oProgress.StatusID);
                        //oStatus.Dispose();
                        oProgress.StatusName = dtTask.Rows[i]["status"].ToString();
                        //oProgress.DateTime = Convert.ToDateTime(dtTask.Rows[i]["nDateTime"]);
                        oProgress.Complete = Convert.ToInt64(dtTask.Rows[i]["dcomplete"]);


                        oTask.Progress = oProgress;

                        oTasks.Add(oTask);

                        oProgress.Dispose();
                        oTask.Dispose();
                        oTaskMail.Dispose();

                    }//END - for ( int i =0 ; i <= dtTask.Rows.Count -1 ; i++)
                    //UpdatePILog("end Loop");
                    return oTasks;

                }//END-if (dtTask != null && dtTask.Rows.Count > 0)

                return null;

            }//END - try
            catch (gloDatabaseLayer.DBException dbErr)
            {
                //MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dbErr.ERROR_Log(dbErr.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtTask.Dispose();
            }
        }

        //Commented by Rahul Patel on 03-01-2011
        //For Merging the Performance Issuse changes.

        //public Tasks GetUnFinishedUserTasks(Int64 UserId)
        //{

        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    DataTable dtTask = new DataTable();
        //    Task oTask;
        //    Tasks oTasks = new Tasks();
        //    TaskAssign oAssign;
        //    TaskProgress oProgress;
        //    TaskAssigns oAssigns = new TaskAssigns();
        //    string strQuery = "";
        //    gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

        //    try
        //    {
        //        oDB.Connect(false);


        //        //strQuery = "SELECT     TM_TaskMST.nTaskID,TM_TaskMST.nProviderID, TM_TaskMST.nPatientID, TM_TaskMST.sSubject, "
        //        //         + "TM_TaskMST.nStartDate , TM_TaskMST.nDueDate , TM_TaskMST.nPriorityID , TM_TaskMST.nCategoryID , "
        //        //         + "TM_TaskMST.nFollowUpID , TM_TaskMST.bIsPrivate , TM_TaskMST.nOwnerID , TM_TaskMST.nDateCreated , "
        //        //         + "TM_TaskMST.sNoteExt , TM_TaskMST.nUserID , TM_TaskMST.nClinicID , TM_Task_Progress.nStatusID, "
        //        //         + "TM_Task_Progress.dComplete, TM_Task_Progress.sDescription, TM_Task_Progress.nDateTime, TM_Task_Assign.nAssignToID, "
        //        //         + "TM_Task_Assign.nAssignFromID, TM_Task_Assign.nSelfAssigned, TM_Task_Assign.nAcceptRejectHold "
        //        //         + "FROM TM_TaskMST INNER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID INNER JOIN "
        //        //         + "TM_Task_Progress ON TM_TaskMST.nTaskID = TM_Task_Progress.nTaskID "
        //        //         + "WHERE (TM_Task_Assign.nSelfAssigned = 1) AND (TM_TaskMST.nUserID = " + UserId + ") AND ISNULL(TM_Task_Progress.dComplete,0) <> 100 ";



        //        // Added field nTaskType by Abhijeet on date 20100422 in below query
        //        // Respective view is modified to show nTaskType
        //        strQuery = "SELECT nTaskID,nProviderID,nPatientID,sSubject,nStartDate,nDueDate,nPriorityID,nCategoryID,nFollowUpID,bIsPrivate,nOwnerID,nDateCreated,"
        //        + " sNoteExt,nUserID,nClinicID,nStatusID,dComplete,sDescription,nDateTime,nAssignToID,nAssignFromID,nSelfAssigned,nAcceptRejectHold,PatientName,PatientCode,ProviderName,Status,Priority,PriorityLevel,Category,FollowUp,nTaskType FROM VW_IncompleteTasks WHERE nUserID =" + UserId + "";

        //        oDB.Retrive_Query(strQuery, out dtTask);
        //        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Fill Task objects start", gloAuditTrail.ActivityOutCome.Success);
        //        if (dtTask != null && dtTask.Rows.Count > 0)
        //        {

        //            for (int i = 0; i <= dtTask.Rows.Count - 1; i++)
        //            {
        //                oTask = new Task();
        //                oTask.TaskID = Convert.ToInt64(dtTask.Rows[i]["nTaskID"]);
        //                //PatientName
        //                oTask.PatientCode = dtTask.Rows[i]["PatientCode"].ToString();
        //                oTask.ProviderID = Convert.ToInt64(dtTask.Rows[i]["nProviderID"]);
        //                gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
        //                if (oTask.ProviderID > 0)
        //                {
        //                    //oTask.ProviderName = oResource.GetProviderName(oTask.ProviderID);
        //                    oTask.ProviderName = dtTask.Rows[i]["ProviderName"].ToString();
        //                }
        //                //oResource.Dispose();
        //                oTask.PatientID = Convert.ToInt64(dtTask.Rows[i]["nPatientID"]);
        //                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
        //                if (oTask.PatientID > 0)
        //                {
        //                    //oTask.PatientName = ogloPatient.GetPatientName(oTask.PatientID);
        //                    oTask.PatientName = dtTask.Rows[i]["PatientName"].ToString();
        //                }
        //                ogloPatient.Dispose();
        //                oTask.Subject = Convert.ToString(dtTask.Rows[i]["sSubject"]);
        //                oTask.StartDate = Convert.ToInt64(dtTask.Rows[i]["nStartDate"]);
        //                oTask.DueDate = Convert.ToInt64(dtTask.Rows[i]["nDueDate"]);

        //                //oTask.PriorityID = Convert.ToInt64(dtTask.Rows[i]["nPriorityID"]);
        //                //gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
        //                //oPriority = oTaskMail.GetPriority(oTask.PriorityID);
        //                //oTask.Priority = oPriority.Description;
        //                //oTask.PriorityLevel = oPriority.PriorityLevel;
        //                //oPriority.Dispose();


        //                oTask.PriorityID = Convert.ToInt64(dtTask.Rows[i]["nPriorityID"]);
        //                gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
        //                oTask.Priority = dtTask.Rows[i]["Priority"].ToString();
        //                oTask.PriorityLevel = Convert.ToInt64(dtTask.Rows[i]["PriorityLevel"]);
        //                oPriority.Dispose();


        //                //oTask.CategoryID = Convert.ToInt64(dtTask.Rows[i]["nCategoryID"]);
        //                //gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
        //                //oCategory = oTaskMail.GetCategory(oTask.CategoryID);
        //                //oTask.Category = oCategory.Description;
        //                //oCategory.Dispose();
        //                oTask.CategoryID = Convert.ToInt64(dtTask.Rows[i]["nCategoryID"]);
        //                //gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();

        //                oTask.Category = dtTask.Rows[i]["Category"].ToString();
        //                //oCategory.Dispose();

        //                //written code by Abhijeet with Mahesh Sonawane on date 20100422 
        //                //oTask.TaskType = dtTask.Rows[i]["nTaskType"].ToString();
        //                oTask.TaskType = (TaskType)Convert.ToInt32(dtTask.Rows[i]["nTaskType"]);
        //                // End of changes by Abhijeet

        //                //oTask.FollowupID = Convert.ToInt64(dtTask.Rows[i]["nFollowupID"]);
        //                //gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();
        //                //oFollowUp = oTaskMail.GetFollowUp(oTask.FollowupID);
        //                //oTask.Followup = oFollowUp.Description;
        //                //oFollowUp.Dispose();

        //                oTask.Followup = dtTask.Rows[i]["FollowUp"].ToString();
        //                //                     oFollowUp.Dispose();
        //                oTask.IsPrivate = Convert.ToBoolean(dtTask.Rows[i]["bIsPrivate"]);
        //                oTask.OwnerID = Convert.ToInt64(dtTask.Rows[i]["nOwnerID"]);
        //                oTask.DateCreated = Convert.ToInt64(dtTask.Rows[i]["nDateCreated"]);
        //                oTask.Notes = dtTask.Rows[i]["sNoteExt"].ToString();
        //                oTask.UserID = Convert.ToInt64(dtTask.Rows[i]["nUserID"]);
        //                oTask.ClinicID = Convert.ToInt64(dtTask.Rows[i]["nClinicID"]);

        //                //Assign Setup
        //                oAssign = new TaskAssign();

        //                oAssign.AssignToID = Convert.ToInt64(dtTask.Rows[i]["nAssignToID"]);
        //                oAssign.AssignFromID = Convert.ToInt64(dtTask.Rows[i]["nAssignFromID"]);
        //                //TODO : AssignedToName & AssignedFromName
        //                oAssign.SelfAssigned = (gloTasksMails.Common.SelfAssigned)Convert.ToInt64(dtTask.Rows[i]["nSelfAssigned"]);
        //                oAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtTask.Rows[i]["nAcceptRejectHold"]);

        //                oTask.Assignment.Add(oAssign);

        //                //Progress Setup
        //                oProgress = new TaskProgress();

        //                //gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
        //                // oProgress.StatusID = Convert.ToInt64(dtTask.Rows[i]["nStatusID"]);
        //                // gloTasksMails.Common.Status oStatus = new gloTasksMails.Common.Status();
        //                //// oStatus = oTaskMail.GetStatus(oProgress.StatusID);
        //                //oStatus = ;

        //                oProgress.StatusName = dtTask.Rows[i]["Status"].ToString();
        //                //oStatus.Dispose();

        //                oProgress.Complete = Convert.ToDecimal(dtTask.Rows[i]["dComplete"]);
        //                oProgress.Description = dtTask.Rows[i]["sDescription"].ToString();
        //                oProgress.DateTime = Convert.ToDateTime(dtTask.Rows[i]["nDateTime"]);

        //                oTask.Progress = oProgress;

        //                oTasks.Add(oTask);

        //                oProgress.Dispose();
        //                oTask.Dispose();
        //                oTaskMail.Dispose();

        //            }//END - for ( int i =0 ; i <= dtTask.Rows.Count -1 ; i++)
        //            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Fill Task objects End", gloAuditTrail.ActivityOutCome.Success);
        //            return oTasks;

        //        }//END-if (dtTask != null && dtTask.Rows.Count > 0)

        //        return null;

        //    }//END - try
        //    catch (gloDatabaseLayer.DBException dbErr)
        //    {
        //        //MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        dbErr.ERROR_Log(dbErr.ToString());
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;

        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //        dtTask.Dispose();
        //    }
        //}
        //Commented by Rahul Patel on 03-01-2011
        //For Merging the Performance Issuse changes.

        //Modified by Rahul Patel on 03-01-2011
        public Tasks GetUnFinishedUserTasks(Int64 UserId, Int64 nShowTaskRowCount)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBps = new gloDatabaseLayer.DBParameters();
            DataTable dtTask = new DataTable();
            Task oTask;
            Tasks oTasks = new Tasks();
            //TaskAssign oAssign;
            TaskProgress oProgress;
            TaskAssigns oAssigns = new TaskAssigns();
            // string strQuery = "";
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);


                //strQuery = "SELECT     TM_TaskMST.nTaskID,TM_TaskMST.nProviderID, TM_TaskMST.nPatientID, TM_TaskMST.sSubject, "
                //         + "TM_TaskMST.nStartDate , TM_TaskMST.nDueDate , TM_TaskMST.nPriorityID , TM_TaskMST.nCategoryID , "
                //         + "TM_TaskMST.nFollowUpID , TM_TaskMST.bIsPrivate , TM_TaskMST.nOwnerID , TM_TaskMST.nDateCreated , "
                //         + "TM_TaskMST.sNoteExt , TM_TaskMST.nUserID , TM_TaskMST.nClinicID , TM_Task_Progress.nStatusID, "
                //         + "TM_Task_Progress.dComplete, TM_Task_Progress.sDescription, TM_Task_Progress.nDateTime, TM_Task_Assign.nAssignToID, "
                //         + "TM_Task_Assign.nAssignFromID, TM_Task_Assign.nSelfAssigned, TM_Task_Assign.nAcceptRejectHold "
                //         + "FROM TM_TaskMST INNER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID INNER JOIN "
                //         + "TM_Task_Progress ON TM_TaskMST.nTaskID = TM_Task_Progress.nTaskID "
                //         + "WHERE (TM_Task_Assign.nSelfAssigned = 1) AND (TM_TaskMST.nUserID = " + UserId + ") AND ISNULL(TM_Task_Progress.dComplete,0) <> 100 ";



                // Added field nTaskType by Abhijeet on date 20100422 in below query
                // Respective view is modified to show nTaskType
                //oDBps.Add("@nUserId", 1, ParameterDirection.Input, SqlDbType.BigInt);
                //On date of relese 6010 we found out this hard coded value for user id changed to dynamic variable UserId- Date :20110122
                oDBps.Add("@nUserId", UserId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBps.Add("@nRowCount ", nShowTaskRowCount, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("getIncompleteTask", oDBps, out dtTask);

                //Commented by Rahul Patel on 13-12-2010
                //strQuery = "SELECT nTaskID,nProviderID,nPatientID,sSubject,nStartDate,nDueDate,nPriorityID,nCategoryID,nFollowUpID,bIsPrivate,nOwnerID,nDateCreated,"
                //+ " sNoteExt,nUserID,nClinicID,nStatusID,dComplete,sDescription,nDateTime,nAssignToID,nAssignFromID,nSelfAssigned,nAcceptRejectHold,PatientName,PatientCode,ProviderName,Status,Priority,PriorityLevel,Category,FollowUp,nTaskType FROM VW_IncompleteTasks WHERE nUserID =" + UserId + "";
                //oDB.Retrive_Query(strQuery, out dtTask);

                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Fill Task objects start", gloAuditTrail.ActivityOutCome.Success);
                if (dtTask != null && dtTask.Rows.Count > 0)
                {

                    for (int i = 0; i <= dtTask.Rows.Count - 1; i++)
                    {
                        oTask = new Task();
                        oTask.TaskID = Convert.ToInt64(dtTask.Rows[i]["nTaskID"]);
                        //PatientName
                        oTask.PatientCode = dtTask.Rows[i]["PatientCode"].ToString();
                        oTask.ProviderID = Convert.ToInt64(dtTask.Rows[i]["nProviderID"]);
                        gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                        if (oTask.ProviderID > 0)
                        {
                            //oTask.ProviderName = oResource.GetProviderName(oTask.ProviderID);
                            oTask.ProviderName = dtTask.Rows[i]["ProviderName"].ToString();
                        }
                        //oResource.Dispose();
                        oTask.PatientID = Convert.ToInt64(dtTask.Rows[i]["nPatientID"]);
                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                        if (oTask.PatientID > 0)
                        {
                            //oTask.PatientName = ogloPatient.GetPatientName(oTask.PatientID);
                            oTask.PatientName = dtTask.Rows[i]["PatientName"].ToString();
                        }
                        ogloPatient.Dispose();
                        oTask.Subject = Convert.ToString(dtTask.Rows[i]["sSubject"]);
                        //Commented by Rahul Patel on 13-12-2010
                        //Commented Due to Unused 
                        //oTask.StartDate = Convert.ToInt64(dtTask.Rows[i]["nStartDate"]);

                        oTask.DueDate = Convert.ToInt64(dtTask.Rows[i]["nDueDate"]);

                        //oTask.PriorityID = Convert.ToInt64(dtTask.Rows[i]["nPriorityID"]);
                        //gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
                        //oPriority = oTaskMail.GetPriority(oTask.PriorityID);
                        //oTask.Priority = oPriority.Description;
                        //oTask.PriorityLevel = oPriority.PriorityLevel;
                        //oPriority.Dispose();

                        //Commented by Rahul Patel on 13-12-2010
                        //Commented Due to Unused 
                        // oTask.PriorityID = Convert.ToInt64(dtTask.Rows[i]["nPriorityID"]);

                        gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
                        oTask.Priority = dtTask.Rows[i]["Priority"].ToString();

                        //Commented by Rahul Patel on 13-12-2010
                        //Commented Due to Unused 
                        //oTask.PriorityLevel = Convert.ToInt64(dtTask.Rows[i]["PriorityLevel"]);

                        oPriority.Dispose();


                        //oTask.CategoryID = Convert.ToInt64(dtTask.Rows[i]["nCategoryID"]);
                        //gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
                        //oCategory = oTaskMail.GetCategory(oTask.CategoryID);
                        //oTask.Category = oCategory.Description;
                        //oCategory.Dispose();

                        //Commented by Rahul Patel on 13-12-2010
                        //Commented Due to Unused 
                        oTask.CategoryID = Convert.ToInt64(dtTask.Rows[i]["nCategoryID"]);

                        //gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();

                        //Commented by Rahul Patel on 13-12-2010
                        //Commented Due to Unused 
                        //oTask.Category = dtTask.Rows[i]["Category"].ToString();

                        //oCategory.Dispose();

                        //written code by Abhijeet with Mahesh Sonawane on date 20100422 
                        //oTask.TaskType = dtTask.Rows[i]["nTaskType"].ToString();
                        oTask.TaskType = (TaskType)Convert.ToInt32(dtTask.Rows[i]["nTaskType"]);
                        // End of changes by Abhijeet

                        //oTask.FollowupID = Convert.ToInt64(dtTask.Rows[i]["nFollowupID"]);
                        //gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();
                        //oFollowUp = oTaskMail.GetFollowUp(oTask.FollowupID);
                        //oTask.Followup = oFollowUp.Description;
                        //oFollowUp.Dispose();

                        //Commented by Rahul Patel on 13-12-2010
                        //Commented Due to Unused 
                        //oTask.Followup = dtTask.Rows[i]["FollowUp"].ToString();
                        ////                     oFollowUp.Dispose();
                        //oTask.IsPrivate = Convert.ToBoolean(dtTask.Rows[i]["bIsPrivate"]);
                        //oTask.OwnerID = Convert.ToInt64(dtTask.Rows[i]["nOwnerID"]);

                        oTask.DateCreated = Convert.ToInt64(dtTask.Rows[i]["nDateCreated"]);

                        //Commented by Rahul Patel on 13-12-2010
                        //Commented Due to Unused 
                        //oTask.Notes = dtTask.Rows[i]["sNoteExt"].ToString();
                        //oTask.ClinicID = Convert.ToInt64(dtTask.Rows[i]["nClinicID"]);

                        oTask.UserID = Convert.ToInt64(dtTask.Rows[i]["nUserID"]);

                        //Commented by Rahul Patel on 13-12-2010
                        //Commented Due to Unused 
                        //Assign Setup
                        //oAssign = new TaskAssign();

                        //oAssign.AssignToID = Convert.ToInt64(dtTask.Rows[i]["nAssignToID"]);
                        //oAssign.AssignFromID = Convert.ToInt64(dtTask.Rows[i]["nAssignFromID"]);
                        ////TODO : AssignedToName & AssignedFromName
                        //oAssign.SelfAssigned = (gloTasksMails.Common.SelfAssigned)Convert.ToInt64(dtTask.Rows[i]["nSelfAssigned"]);
                        //oAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtTask.Rows[i]["nAcceptRejectHold"]);

                        //oTask.Assignment.Add(oAssign);

                        //Progress Setup
                        oProgress = new TaskProgress();

                        //gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
                        // oProgress.StatusID = Convert.ToInt64(dtTask.Rows[i]["nStatusID"]);
                        // gloTasksMails.Common.Status oStatus = new gloTasksMails.Common.Status();
                        //// oStatus = oTaskMail.GetStatus(oProgress.StatusID);
                        //oStatus = ;

                        oProgress.StatusName = dtTask.Rows[i]["Status"].ToString();
                        oProgress.Assign_To = dtTask.Rows[i]["Assign To"].ToString();
                        //oStatus.Dispose();

                        //Commented by Rahul Patel on 13-12-2010
                        //Commented Due to Unused 
                        //oProgress.Complete = Convert.ToDecimal(dtTask.Rows[i]["dComplete"]);

                        //Commented by Rahul Patel on 13-12-2010
                        //Commented Due to Unused 
                        //oProgress.Description = dtTask.Rows[i]["sDescription"].ToString();
                        //oProgress.DateTime = Convert.ToDateTime(dtTask.Rows[i]["nDateTime"]);

                        oTask.Progress = oProgress;

                        oTasks.Add(oTask);

                        oProgress.Dispose();
                        oTask.Dispose();
                        oTaskMail.Dispose();

                    }//END - for ( int i =0 ; i <= dtTask.Rows.Count -1 ; i++)
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Fill Task objects End", gloAuditTrail.ActivityOutCome.Success);
                    return oTasks;

                }//END-if (dtTask != null && dtTask.Rows.Count > 0)

                return null;

            }//END - try
            catch (gloDatabaseLayer.DBException dbErr)
            {
                //MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dbErr.ERROR_Log(dbErr.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtTask.Dispose();
            }
        }
        //End of code modified by rahul patel on 03-01-2011


        public DataTable GetUnFinishedUserTasksList(Int64 UserId, Int64 nShowTaskRowCount,string SearchString ="")
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBps = new gloDatabaseLayer.DBParameters();
            DataTable dtTask = new DataTable();
            try
            {
                oDB.Connect(false);

                oDBps.Add("@nUserId", UserId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBps.Add("@nRowCount ", nShowTaskRowCount, ParameterDirection.Input, SqlDbType.BigInt);
                oDBps.Add("@SearchString ", SearchString.Trim() , ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("getIncompleteTaskList", oDBps, out dtTask);

                oDB.Disconnect();

                oDB.Dispose();
                oDB = null;

                 return dtTask;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                dtTask.Dispose();
                dtTask = null;
            }
        }

        //Incident #58149: 00019311 
        //Integrated against Bug #58734: 00000559 : multiple tasks for unmatch patient on dashboard
        //Optimize flow for displaying match patient form.
        public DataTable GetUnFinishedUnmatchedTasks(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBps = new gloDatabaseLayer.DBParameters();
            DataTable dtTask = new DataTable();
            try
            {
                oDB.Connect(false);

                oDBps.Add("@nUserId", UserId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_getIncompleteUnmatchedPatientTaskList", oDBps, out dtTask);

                oDB.Disconnect();

                oDB.Dispose();
                oDB = null;

                return dtTask;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                dtTask.Dispose();
                dtTask = null;
            }
        }

        //added in 6050 for patient specific task shown on dashboard in patient detail panel
        public DataTable GetPatientTasksList(Int64 nPatientID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBps = new gloDatabaseLayer.DBParameters();
            DataTable dtTask = new DataTable();
            try
            {
                oDB.Connect(false);


                oDBps.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("getPatientTask", oDBps, out dtTask);

                oDB.Disconnect();

                oDB.Dispose();
                oDB = null;

                return dtTask;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                dtTask.Dispose();
                dtTask = null;
            }
        }


        public long GetUniqueueId()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object oResult = new object();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@ID", "0", ParameterDirection.Output, SqlDbType.BigInt);
                oDB.Execute("gsp_GetUniqueID", oParameters, out oResult);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if ((oDB != null))
                {
                    oDB.Dispose();
                }
                if ((oParameters != null))
                {
                    oParameters.Dispose();
                }
            }
            return Convert.ToInt64(oResult);
        }

        public Tasks GetUserInProgressTasks(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTask = new DataTable();
            Task oTask;
            Tasks oTasks = new Tasks();
            TaskAssign oAssign;
            TaskProgress oProgress;
            TaskAssigns oAssigns = new TaskAssigns();
            string strQuery = "";
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);

                //strQuery = "select * from TM_Task_MST where nProviderID= " + ProviderId + " OR nAssignedToID=" + ProviderId + " ";

                /*
                strQuery = "SELECT DISTINCT TM_Task_Assign.nAssignToID, TM_Task_Assign.nAssignFromID, TM_Task_Assign.nSelfAssigned, "
                           + "TM_Task_Assign.nAcceptRejectHold,TM_Task_Progress.nStatusID, TM_Task_Progress.dComplete, "
                           + "TM_Task_Progress.sDescription, TM_Task_Progress.nDateTime,TM_TaskMST.nProviderID, TM_TaskMST.nPatientID, "
                           + "TM_TaskMST.sSubject, TM_TaskMST.nStartDate, TM_TaskMST.nDueDate,TM_TaskMST.nPriorityID, TM_TaskMST.nCategoryID, "
                           + "TM_TaskMST.nFollowUpID, TM_TaskMST.bIsPrivate, TM_TaskMST.nOwnerID,TM_TaskMST.nDateCreated, "
                           + "TM_TaskMST.sNoteExt, TM_TaskMST.nUserID, TM_TaskMST.nClinicID,TM_TaskMST.nTaskID "
                           + "FROM TM_Task_Progress RIGHT OUTER JOIN TM_TaskMST ON TM_Task_Progress.nTaskID = TM_TaskMST.nTaskID "
                           + "LEFT OUTER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID "
                           + "WHERE  TM_TaskMST.nUserID = " + UserId;
                */

                strQuery = "SELECT     TM_TaskMST.nTaskID,TM_TaskMST.nProviderID, TM_TaskMST.nPatientID, TM_TaskMST.sSubject, "
                         + "TM_TaskMST.nStartDate , TM_TaskMST.nDueDate , TM_TaskMST.nPriorityID , TM_TaskMST.nCategoryID , "
                         + "TM_TaskMST.nFollowUpID , TM_TaskMST.bIsPrivate , TM_TaskMST.nOwnerID , TM_TaskMST.nDateCreated , "
                         + "TM_TaskMST.sNoteExt , TM_TaskMST.nUserID , TM_TaskMST.nClinicID , TM_Task_Progress.nStatusID, "
                         + "TM_Task_Progress.dComplete, TM_Task_Progress.sDescription, TM_Task_Progress.nDateTime, TM_Task_Assign.nAssignToID, "
                         + "TM_Task_Assign.nAssignFromID, TM_Task_Assign.nSelfAssigned, TM_Task_Assign.nAcceptRejectHold "
                         + "FROM TM_TaskMST INNER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID INNER JOIN "
                         + "TM_Task_Progress ON TM_TaskMST.nTaskID = TM_Task_Progress.nTaskID "
                         + "WHERE (TM_Task_Assign.nSelfAssigned = 1) AND (TM_TaskMST.nUserID = " + UserId + ") AND (TM_Task_Progress.dComplete <> 100) ORDER BY TM_TaskMST.nDueDate";



                oDB.Retrive_Query(strQuery, out dtTask);
                if (dtTask != null && dtTask.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtTask.Rows.Count - 1; i++)
                    {
                        oTask = new Task();
                        oTask.TaskID = Convert.ToInt64(dtTask.Rows[i]["nTaskID"]);

                        oTask.ProviderID = Convert.ToInt64(dtTask.Rows[i]["nProviderID"]);
                        gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                        if (oTask.ProviderID > 0)
                        {
                            oTask.ProviderName = oResource.GetProviderName(oTask.ProviderID);
                        }
                        //oResource.Dispose();

                        oTask.PatientID = Convert.ToInt64(dtTask.Rows[i]["nPatientID"]);
                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                        if (oTask.PatientID > 0)
                        {
                            oTask.PatientName = ogloPatient.GetPatientName(oTask.PatientID);
                        }
                        ogloPatient.Dispose();


                        oTask.Subject = Convert.ToString(dtTask.Rows[i]["sSubject"]);
                        oTask.StartDate = Convert.ToInt64(dtTask.Rows[i]["nStartDate"]);
                        oTask.DueDate = Convert.ToInt64(dtTask.Rows[i]["nDueDate"]);

                        oTask.PriorityID = Convert.ToInt64(dtTask.Rows[i]["nPriorityID"]);
                        gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
                        oPriority = oTaskMail.GetPriority(oTask.PriorityID);
                        oTask.Priority = oPriority.Description;
                        oTask.PriorityLevel = oPriority.PriorityLevel;
                        oPriority.Dispose();

                        oTask.CategoryID = Convert.ToInt64(dtTask.Rows[i]["nCategoryID"]);
                        gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
                        oCategory = oTaskMail.GetCategory(oTask.CategoryID);
                        oTask.Category = oCategory.Description;
                        oCategory.Dispose();

                        oTask.FollowupID = Convert.ToInt64(dtTask.Rows[i]["nFollowupID"]);
                        gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();
                        oFollowUp = oTaskMail.GetFollowUp(oTask.FollowupID);
                        oTask.Followup = oFollowUp.Description;
                        oFollowUp.Dispose();

                        oTask.IsPrivate = Convert.ToBoolean(dtTask.Rows[i]["bIsPrivate"]);
                        oTask.OwnerID = Convert.ToInt64(dtTask.Rows[i]["nOwnerID"]);
                        oTask.DateCreated = Convert.ToInt64(dtTask.Rows[i]["nDateCreated"]);
                        oTask.Notes = dtTask.Rows[i]["sNoteExt"].ToString();
                        oTask.UserID = Convert.ToInt64(dtTask.Rows[i]["nUserID"]);
                        oTask.ClinicID = Convert.ToInt64(dtTask.Rows[i]["nClinicID"]);

                        //Assign Setup
                        oAssign = new TaskAssign();

                        oAssign.AssignToID = Convert.ToInt64(dtTask.Rows[i]["nAssignToID"]);
                        oAssign.AssignFromID = Convert.ToInt64(dtTask.Rows[i]["nAssignFromID"]);
                        //TODO : AssignedToName & AssignedFromName
                        oAssign.SelfAssigned = (gloTasksMails.Common.SelfAssigned)Convert.ToInt64(dtTask.Rows[i]["nSelfAssigned"]);
                        oAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtTask.Rows[i]["nAcceptRejectHold"]);
                        oTask.Assignment.Add(oAssign);
                        oAssign.Dispose();

                        //Progress Setup
                        oProgress = new TaskProgress();

                        //gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
                        oProgress.StatusID = Convert.ToInt64(dtTask.Rows[i]["nStatusID"]);
                        gloTasksMails.Common.Status oStatus = new gloTasksMails.Common.Status();
                        oStatus = oTaskMail.GetStatus(oProgress.StatusID);
                        oProgress.StatusName = oStatus.Description;
                        oStatus.Dispose();

                        oProgress.Complete = Convert.ToDecimal(dtTask.Rows[i]["dComplete"]);
                        oProgress.Description = dtTask.Rows[i]["sDescription"].ToString();
                        oProgress.DateTime = Convert.ToDateTime(dtTask.Rows[i]["nDateTime"]);
                        oTask.Assign_To = Convert.ToString(dtTask.Rows[i]["Assign To"]);

                        oTask.Progress = oProgress;

                        oTasks.Add(oTask);

                        oProgress.Dispose();
                        oTask.Dispose();
                        oTaskMail.Dispose();

                    }//END - for ( int i =0 ; i <= dtTask.Rows.Count -1 ; i++)

                    return oTasks;

                }//END-if (dtTask != null && dtTask.Rows.Count > 0)

                return null;

            }//END - try
            catch (gloDatabaseLayer.DBException dbErr)
            {
                //MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dbErr.ERROR_Log(dbErr.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
                oDB.Disconnect();
                dtTask.Dispose();
            }
        }

        //public gloExchange.Common.Task.PmsExchangeTasks GetExchangeUserTasks(Int64 UserId)
        //{

        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    DataTable dtTask = new DataTable();
        //    gloExchange.Common.Task.PmsExchangeTask oTask;
        //    gloExchange.Common.Task.PmsExchangeTasks oTasks = new gloExchange.Common.Task.PmsExchangeTasks();
        //    gloExchange.Common.Task.PmsExchangeTaskAssign oAssign;
        //    gloExchange.Common.Task.PmsExchangeTaskProgress oProgress;
        //    gloExchange.Common.Task.PmsExchangeTaskAssigns oAssigns = new gloExchange.Common.Task.PmsExchangeTaskAssigns();
        //    string strQuery = "";
        //    gloExchange.Common.Task.PmsExchangeTasks oTaskMail = new gloExchange.Common.Task.PmsExchangeTasks();

        //    try
        //    {
        //        oDB.Connect(false);

        //        //strQuery = "select * from TM_Task_MST where nProviderID= " + ProviderId + " OR nAssignedToID=" + ProviderId + " ";

        //        /*
        //        strQuery = "SELECT DISTINCT TM_Task_Assign.nAssignToID, TM_Task_Assign.nAssignFromID, TM_Task_Assign.nSelfAssigned, "
        //                   + "TM_Task_Assign.nAcceptRejectHold,TM_Task_Progress.nStatusID, TM_Task_Progress.dComplete, "
        //                   + "TM_Task_Progress.sDescription, TM_Task_Progress.nDateTime,TM_TaskMST.nProviderID, TM_TaskMST.nPatientID, "
        //                   + "TM_TaskMST.sSubject, TM_TaskMST.nStartDate, TM_TaskMST.nDueDate,TM_TaskMST.nPriorityID, TM_TaskMST.nCategoryID, "
        //                   + "TM_TaskMST.nFollowUpID, TM_TaskMST.bIsPrivate, TM_TaskMST.nOwnerID,TM_TaskMST.nDateCreated, "
        //                   + "TM_TaskMST.sNoteExt, TM_TaskMST.nUserID, TM_TaskMST.nClinicID,TM_TaskMST.nTaskID "
        //                   + "FROM TM_Task_Progress RIGHT OUTER JOIN TM_TaskMST ON TM_Task_Progress.nTaskID = TM_TaskMST.nTaskID "
        //                   + "LEFT OUTER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID "
        //                   + "WHERE  TM_TaskMST.nUserID = " + UserId;
        //        */

        //        strQuery = "SELECT     TM_TaskMST.nTaskID,TM_TaskMST.nProviderID, TM_TaskMST.nPatientID, TM_TaskMST.sSubject, "
        //                 + "TM_TaskMST.nStartDate , TM_TaskMST.nDueDate , TM_TaskMST.nPriorityID , TM_TaskMST.nCategoryID , "
        //                 + "TM_TaskMST.nFollowUpID , TM_TaskMST.bIsPrivate , TM_TaskMST.nOwnerID , TM_TaskMST.nDateCreated , "
        //                 + "TM_TaskMST.sNoteExt , TM_TaskMST.nUserID , TM_TaskMST.nClinicID , TM_Task_Progress.nStatusID, "
        //                 + "TM_Task_Progress.dComplete, TM_Task_Progress.sDescription, TM_Task_Progress.nDateTime, TM_Task_Assign.nAssignToID, "
        //                 + "TM_Task_Assign.nAssignFromID, TM_Task_Assign.nSelfAssigned, TM_Task_Assign.nAcceptRejectHold "
        //                 + "FROM TM_TaskMST INNER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID INNER JOIN "
        //                 + "TM_Task_Progress ON TM_TaskMST.nTaskID = TM_Task_Progress.nTaskID "
        //                 + "WHERE (TM_Task_Assign.nSelfAssigned = 1) AND (TM_TaskMST.nUserID = " + UserId + ") ";



        //        oDB.Retrive_Query(strQuery, out dtTask);
        //        if (dtTask != null && dtTask.Rows.Count > 0)
        //        {

        //            for (int i = 0; i <= dtTask.Rows.Count - 1; i++)
        //            {

        //                oTask = new gloExchange.Common.Task.PmsExchangeTask();

        //                oTask.TaskID = Convert.ToInt64(dtTask.Rows[i]["nTaskID"]);

        //                oTask.ProviderID = Convert.ToInt64(dtTask.Rows[i]["nProviderID"]);
        //                gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
        //                oTask.ProviderName = oResource.GetProviderName(oTask.ProviderID);
        //                //oResource.Dispose();

        //                oTask.PatientID = Convert.ToInt64(dtTask.Rows[i]["nPatientID"]);
        //                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
        //                oTask.PatientName = ogloPatient.GetPatientName(oTask.PatientID);
        //                ogloPatient.Dispose();


        //                oTask.Subject = Convert.ToString(dtTask.Rows[i]["sSubject"]);
        //                oTask.StartDate = Convert.ToInt64(dtTask.Rows[i]["nStartDate"]);
        //                oTask.DueDate = Convert.ToInt64(dtTask.Rows[i]["nDueDate"]);

        //                oTask.PriorityID = Convert.ToInt64(dtTask.Rows[i]["nPriorityID"]);
        //                gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
        //                oPriority = oTaskMail.GetPriority(oTask.PriorityID);
        //                oTask.Priority = oPriority.Description;
        //                oPriority.Dispose();

        //                oTask.CategoryID = Convert.ToInt64(dtTask.Rows[i]["nCategoryID"]);
        //                gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
        //                oCategory = oTaskMail.GetCategory(oTask.CategoryID);
        //                oTask.Category = oCategory.Description;
        //                oCategory.Dispose();

        //                oTask.FollowupID = Convert.ToInt64(dtTask.Rows[i]["nFollowupID"]);
        //                gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();
        //                oFollowUp = oTaskMail.GetFollowUp(oTask.FollowupID);
        //                oTask.Followup = oFollowUp.Description;
        //                oFollowUp.Dispose();

        //                oTask.IsPrivate = Convert.ToBoolean(dtTask.Rows[i]["bIsPrivate"]);
        //                oTask.OwnerID = Convert.ToInt64(dtTask.Rows[i]["nOwnerID"]);
        //                oTask.DateCreated = Convert.ToInt64(dtTask.Rows[i]["nDateCreated"]);
        //                oTask.Notes = dtTask.Rows[i]["sNoteExt"].ToString();
        //                oTask.UserID = Convert.ToInt64(dtTask.Rows[i]["nUserID"]);
        //                oTask.ClinicID = Convert.ToInt64(dtTask.Rows[i]["nClinicID"]);

        //                //Assign Setup
        //                oAssign = new gloExchange.Common.Task.PmsExchangeTaskAssign();

        //                oAssign.AssignToID = Convert.ToInt64(dtTask.Rows[i]["nAssignToID"]);
        //                oAssign.AssignFromID = Convert.ToInt64(dtTask.Rows[i]["nAssignFromID"]);
        //                //TODO : AssignedToName & AssignedFromName
        //                oAssign.SelfAssigned = (gloExchange.Common.Task.SelfAssigned)Convert.ToInt64(dtTask.Rows[i]["nSelfAssigned"]);
        //                oAssign.AcceptRejectHold = (gloExchange.Common.Task.AcceptRejectHold)Convert.ToInt64(dtTask.Rows[i]["nAcceptRejectHold"]);

        //                oTask.Assignment.Add(oAssign);

        //                //Progress Setup
        //                oProgress = new gloExchange.Common.Task.PmsExchangeTaskProgress();

        //                //gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
        //                oProgress.StatusID = Convert.ToInt64(dtTask.Rows[i]["nStatusID"]);
        //                gloTasksMails.Common.Status oStatus = new gloTasksMails.Common.Status();
        //                oStatus = oTaskMail.GetStatus(oProgress.StatusID);
        //                oProgress.StatusName = oStatus.Description;
        //                oStatus.Dispose();

        //                oProgress.Complete = Convert.ToDecimal(dtTask.Rows[i]["dComplete"]);
        //                oProgress.Description = dtTask.Rows[i]["sDescription"].ToString();
        //                oProgress.DateTime = Convert.ToDateTime(dtTask.Rows[i]["nDateTime"]);

        //                oTask.Progress = oProgress;

        //                oTasks.Add(oTask);
        //                oTaskMail.Dispose();

        //            }//END - for ( int i =0 ; i <= dtTask.Rows.Count -1 ; i++)

        //            return oTasks;

        //        }//END-if (dtTask != null && dtTask.Rows.Count > 0)

        //        return null;

        //    }//END - try
        //    catch (gloDatabaseLayer.DBException dbErr)
        //    {
        //        MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;

        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        dtTask.Dispose();
        //    }
        //}

        #region "gloSuite - while integrating this function is comment, if required then we have to add gloExchange project, other wise try to maintain it in gloExchange service"

        //public gloExchange.Common.Task.PmsExchangeTasks ConvertToExchangeTasks(Tasks oTasks)
        //{

        //    gloExchange.Common.Task.PmsExchangeTask oExchangeTask;
        //    gloExchange.Common.Task.PmsExchangeTasks oExchangeTasks = new gloExchange.Common.Task.PmsExchangeTasks();
        //    gloExchange.Common.Task.PmsExchangeTaskAssign oAssign;
        //    gloExchange.Common.Task.PmsExchangeTaskProgress oProgress;
        //    gloExchange.Common.Task.PmsExchangeTaskAssigns oAssigns = new gloExchange.Common.Task.PmsExchangeTaskAssigns();
        //    string strQuery = "";
        //    gloExchange.Common.Task.PmsExchangeTasks oTaskMail = new gloExchange.Common.Task.PmsExchangeTasks();

        //    try
        //    {

        //            for (int i = 0; i <= oTasks.Count - 1; i++)
        //            {

        //                oExchangeTask = new gloExchange.Common.Task.PmsExchangeTask();

        //                oExchangeTask.TaskID = oTasks[i].TaskID;
        //                oExchangeTask.ProviderID = oTasks[i].ProviderID;
        //                oExchangeTask.ProviderName = oTasks[i].ProviderName;
        //                oExchangeTask.PatientID = oTasks[i].PatientID;
        //                oExchangeTask.PatientName = oTasks[i].PatientName;
        //                oExchangeTask.Subject = oTasks[i].Subject;
        //                oExchangeTask.StartDate = oTasks[i].StartDate;
        //                oExchangeTask.DueDate = oTasks[i].DueDate;
        //                oExchangeTask.PriorityID = oTasks[i].PriorityID;
        //                oExchangeTask.Priority = oTasks[i].Priority;
        //                oExchangeTask.CategoryID = oTasks[i].CategoryID;
        //                oExchangeTask.Category = oTasks[i].Category;
        //                oExchangeTask.FollowupID = oTasks[i].FollowupID;
        //                oExchangeTask.Followup = oTasks[i].Followup;
        //                oExchangeTask.IsPrivate = oTasks[i].IsPrivate;
        //                oExchangeTask.OwnerID = oTasks[i].OwnerID;
        //                oExchangeTask.DateCreated = oTasks[i].DateCreated;
        //                oExchangeTask.Notes = oTasks[i].Notes;
        //                oExchangeTask.UserID = oTasks[i].UserID;
        //                oExchangeTask.ClinicID = oTasks[i].ClinicID;

        //                    for (int k=0; k <= oTasks[i].Assignment.Count - 1; k++)
        //                    {
        //                        oAssign = new gloExchange.Common.Task.PmsExchangeTaskAssign();
        //                        oAssign.AcceptRejectHold = (gloExchange.Common.Task.AcceptRejectHold)oTasks[i].Assignment[k].AcceptRejectHold;
        //                        oAssign.AssignFromID = oTasks[i].Assignment[k].AssignFromID;
        //                        oAssign.AssignFromName = oTasks[i].Assignment[k].AssignFromName;
        //                        oAssign.AssignToID = oTasks[i].Assignment[k].AssignToID;
        //                        oAssign.AssignToName = oTasks[i].Assignment[k].AssignToName;
        //                        oAssign.ClinicID = oTasks[i].Assignment[k].ClinicID;
        //                        oAssign.SelfAssigned = (gloExchange.Common.Task.SelfAssigned)oTasks[i].Assignment[k].SelfAssigned;
        //                        oAssign.TaskID = oTasks[i].Assignment[k].TaskID;
        //                        oExchangeTask.Assignment.Add(oAssign);
        //                    }

        //                    oExchangeTask.Progress.ClinicID = oTasks[i].Progress.ClinicID;
        //                    oExchangeTask.Progress.Complete = oTasks[i].Progress.Complete;
        //                    oExchangeTask.Progress.DateTime = oTasks[i].Progress.DateTime;
        //                    oExchangeTask.Progress.Description= oTasks[i].Progress.Description;
        //                    oExchangeTask.Progress.StatusID = oTasks[i].Progress.StatusID;
        //                    oExchangeTask.Progress.StatusName = oTasks[i].Progress.StatusName;
        //                    oExchangeTask.Progress.TaskID = oTasks[i].Progress.TaskID;

        //                oExchangeTasks.Add(oExchangeTask);
        //                oTaskMail.Dispose();

        //            }//END - for ( int i =0 ; i <= oTasks.Count -1 ; i++)

        //            return oExchangeTasks;

        //    }//END - try
        //    catch (gloDatabaseLayer.DBException dbErr)
        //    {
        //        //MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        dbErr.ERROR_Log(dbErr.ToString());
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;

        //    }
        //    finally
        //    {


        //    }
        //}

        #endregion "gloSuite"

        public void ShowTask(System.Windows.Forms.Form oParentWindow)
        {

            //frmViewTask ofrmViewTask = new frmViewTask(_databaseconnectionstring);
            frmViewTask ofrmViewTask = frmViewTask.GetInstance();
            ofrmViewTask.OnViewTask_Change += new frmViewTask.OnViewTaskChange(ofrmViewTask_OnViewTask_Change);
            ofrmViewTask.WindowState = FormWindowState.Maximized;
            ofrmViewTask.MdiParent = oParentWindow;
            ofrmViewTask.Show();

        }

        public void ShowTask(System.Windows.Forms.Form oParentWindow, bool IsEMREnabled, Int64 PatientID, bool gblnviewCompleteOtherUsersTasks=false )
        {
            
            //frmViewTask ofrmViewTask = new frmViewTask(_databaseconnectionstring);
            frmViewTask ofrmViewTask = frmViewTask.GetInstance();
        
            ofrmViewTask.IsEMREnable = IsEMREnabled;
            ofrmViewTask.PatientID = PatientID;
            
            //Developer: Sanjog Dhamke
            //Date:10 Dec 2011
            //Bug ID/PRD Name/Sales force Case: Issue is - Handler is getting added on every button click on same form n Task Button event is fire multiple time 
            //Reason: So now we check that if instance is already created means handler is also added in it so don't add another extra handler for this form
            //ofrmViewTask.OnViewTask_Change -= new frmViewTask.OnViewTaskChange(ofrmViewTask_OnViewTask_Change);
            if (ofrmViewTask._HandlerPresent ==false)
            {
                ofrmViewTask.OnViewTask_Change += new frmViewTask.OnViewTaskChange(ofrmViewTask_OnViewTask_Change);
            }
            ofrmViewTask._HandlerPresent = true;
            ofrmViewTask.WindowState = FormWindowState.Maximized;
            ofrmViewTask.MdiParent = oParentWindow;
            ofrmViewTask.ShowOtherUsersDropdown = gblnviewCompleteOtherUsersTasks;
            ofrmViewTask.Show();

        }

        void ofrmViewTask_OnViewTask_Change(object sender, EventArgs e, TaskChangeEventArg e2,object objfrmtask=null)
        {
            if (OnGloTask_Change != null)
            {
                OnGloTask_Change(sender, e, e2, objfrmtask);
            }
        }

        void gloTask_OnGloTask_Change(object sender, EventArgs e, TaskChangeEventArg e2,object objfrmtask=null)
        {

        }

        public DataTable GetUserTaskRequests(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTaskRequest = new DataTable();
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //strQuery = " SELECT DISTINCT TM_Task_Assign.nAssignToID, "
                //         + " TM_Task_Assign.nAcceptRejectHold, TM_TaskMST.sSubject,TM_TaskMST.nTaskID "
                //         + " FROM TM_TaskMST INNER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID "
                //         + " WHERE TM_TaskMST.nUserID ="+ UserId +" AND TM_Task_Assign.nSelfAssigned = 2 ";
                //
                //strQuery = " SELECT DISTINCT TM_Task_Assign.nAssignToID, "
                //         + " TM_Task_Assign.nAcceptRejectHold, TM_TaskMST.sSubject,TM_TaskMST.nTaskID "
                //         + " FROM TM_TaskMST INNER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID "
                //         + " WHERE TM_TaskMST.nUserID =" + UserId + " AND TM_Task_Assign.bIsBlocked = 'false'";
                //
                //
                //modified by pradeep 20100924 to do changes done in 5061

                strQuery = " SELECT DISTINCT TM_Task_Assign.nAssignToID, "
                       + " TM_Task_Assign.nAcceptRejectHold, TM_TaskMST.sSubject,TM_TaskMST.nTaskID "
                       + " FROM TM_TaskMST INNER JOIN TM_Task_Assign ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID "
                       + " WHERE TM_Task_Assign.nAssignFromID =" + UserId + " AND TM_Task_Assign.bIsBlocked = 'false' AND TM_TaskMST.nOwnerID =" + UserId + " ";

                oDB.Retrive_Query(strQuery, out dtTaskRequest);

                if (dtTaskRequest != null)
                {
                    return dtTaskRequest;

                }
                oDB.Disconnect();
                return null;


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Dispose();
            }
        }
        public DataTable GetUserTrackTaskRequests(Int64 UserId)
        {
            DataTable dtTaskRequest = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand _sqlCommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection(_databaseconnectionstring);
            SqlDataAdapter da;
            try
            {
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.CommandText = "gsp_GetUserTasks";
                _sqlCommand.Connection = oConnection;
                _sqlCommand.CommandTimeout = 0;
                _sqlCommand.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = UserId;

                _sqlCommand.Parameters.Add("@AssignFlag", SqlDbType.Int).Value = 3;
                da = new SqlDataAdapter(_sqlCommand);
                da.Fill(dtTaskRequest);
                da.Dispose();
                _sqlCommand.Parameters.Clear();
                if (dtTaskRequest != null)
                {
                    return dtTaskRequest;
                }

                return null;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                if (_sqlCommand != null)
                {
                    _sqlCommand.Parameters.Clear();   
                    _sqlCommand.Dispose();
                    _sqlCommand = null;
                }

                oConnection.Dispose();
                dtTaskRequest.Dispose();

            }
        }

        public DataTable GetUserTrackTaskRequests_New(Int64 UserId)
        {
            DataTable dtTaskRequest = new DataTable();
            SqlCommand _sqlCommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection(_databaseconnectionstring);
            SqlDataAdapter da;
            try
            {
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.CommandText = "gsp_GetUserTasks_New";
                _sqlCommand.Connection = oConnection;
                _sqlCommand.CommandTimeout = 0;
                _sqlCommand.Parameters.Add("@User_ID", SqlDbType.NVarChar).Value = UserId;

                da = new SqlDataAdapter(_sqlCommand);
                da.Fill(dtTaskRequest);
                da.Dispose();
                _sqlCommand.Parameters.Clear();
                if (dtTaskRequest != null)
                {
                    return dtTaskRequest;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                if (_sqlCommand != null)
                {
                    _sqlCommand.Dispose();
                    _sqlCommand = null;
                }
                if (oConnection != null)
                {
                    oConnection.Dispose();
                    oConnection = null;
                }
                if (dtTaskRequest != null)
                {
                    dtTaskRequest.Dispose();
                    dtTaskRequest = null;
                }
            }
        }

        public TaskAssigns GetAssignedTasks(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            TaskAssign oAssign;
            TaskAssigns oTaskAssigns = new TaskAssigns();
            DataTable dtAssignedTask = new DataTable();


            try
            {
                oDB.Connect(false);

                strQuery = "select distinct  nTaskID,nAssignToID,nAssignFromID,nAcceptRejectHold from TM_Task_Assign "
                         + "where nAssignToID=" + UserId + " AND nAcceptRejectHold= " + Convert.ToInt64(gloTasksMails.Common.AcceptRejectHold.Hold) + " ";

                //Get Assigned task for User which are on Hold
                oDB.Retrive_Query(strQuery, out dtAssignedTask);

                if (dtAssignedTask != null && dtAssignedTask.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAssignedTask.Rows.Count; i++)
                    {

                        oAssign = new TaskAssign();

                        oAssign.TaskID = Convert.ToInt64(dtAssignedTask.Rows[i]["nTaskID"]);
                        oAssign.AssignToID = Convert.ToInt64(dtAssignedTask.Rows[i]["nAssignToID"]);
                        oAssign.AssignFromID = Convert.ToInt64(dtAssignedTask.Rows[i]["nAssignFromID"]);
                       
                        oAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtAssignedTask.Rows[i]["nAcceptRejectHold"]);

                        oTaskAssigns.Add(oAssign);
                        oAssign.Dispose();
                    }

                    return oTaskAssigns;
                }
                return null;


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtAssignedTask.Dispose();
            }

        }
        public TaskAssigns GetUnFinishedAssignedTasks(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            TaskAssign oAssign;
            TaskAssigns oTaskAssigns = new TaskAssigns();
            DataTable dtAssignedTask = new DataTable();


            try
            {
                oDB.Connect(false);

                strQuery = "select TM_Task_Assign.nTaskID,nAssignToID,nAssignFromID,nAcceptRejectHold from TM_Task_Assign LEFT OUTER JOIN TM_Task_Progress "
                         + " ON TM_Task_Assign.nTaskID = TM_Task_Progress.nTaskID "
                         + "where nAssignToID=" + UserId + " AND nAcceptRejectHold= " + Convert.ToInt64(gloTasksMails.Common.AcceptRejectHold.Hold)
                         + " AND ISNULL(TM_Task_Progress.dComplete,0) <> 100";

                //Get Assigned task for User which are on Hold
                oDB.Retrive_Query(strQuery, out dtAssignedTask);

                if (dtAssignedTask != null && dtAssignedTask.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAssignedTask.Rows.Count; i++)
                    {

                        oAssign = new TaskAssign();

                        oAssign.TaskID = Convert.ToInt64(dtAssignedTask.Rows[i]["nTaskID"]);
                        oAssign.AssignToID = Convert.ToInt64(dtAssignedTask.Rows[i]["nAssignToID"]);
                        oAssign.AssignFromID = Convert.ToInt64(dtAssignedTask.Rows[i]["nAssignFromID"]);
                        oAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtAssignedTask.Rows[i]["nAcceptRejectHold"]);

                        oTaskAssigns.Add(oAssign);
                        oAssign.Dispose();
                    }

                    return oTaskAssigns;
                }
                return null;


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtAssignedTask.Dispose();
            }

        }
        public string GetUserLoginName(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtUser = new DataTable();
            string strQuery = "";
            string userName = "";
            try
            {
                oDB.Connect(false);
                strQuery = "select  sFirstName,sMiddleName,sLastName,sLoginName from User_MST where nUserID = " + UserId;
                oDB.Retrive_Query(strQuery, out dtUser);
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    //userName = dtUser.Rows[0][0].ToString() + " " + dtUser.Rows[0][1].ToString() + " " + dtUser.Rows[0][2].ToString();
                    
                    userName = dtUser.Rows[0]["sLoginName"].ToString();

                    return userName;

                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtUser.Dispose();
            }

        }
        public string GetUserName(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtUser = new DataTable();
            string strQuery = "";
            string userName = "";
            try
            {
                oDB.Connect(false);
                strQuery = "select  sFirstName,sMiddleName,sLastName from User_MST where nUserID = " + UserId;
                oDB.Retrive_Query(strQuery, out dtUser);
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    userName = dtUser.Rows[0][0].ToString() + " " + dtUser.Rows[0][1].ToString() + " " + dtUser.Rows[0][2].ToString();
                    // userName = dtUser.Rows[0]["sLoginName"].ToString();

                    return userName;

                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtUser.Dispose();
            }

        }

        public string GetUserEmail(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtUser = new DataTable();
            string strQuery = "";
            string userEmail = "";
            try
            {
                oDB.Connect(false);
                strQuery = "select sEmail from User_MST where nUserID = " + UserId;
                oDB.Retrive_Query(strQuery, out dtUser);
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    userEmail = dtUser.Rows[0][0].ToString();
                    return userEmail;

                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtUser.Dispose();
            }


        }

        public Task GetTask(Int64 TaskId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBps = new gloDatabaseLayer.DBParameters();
            DataTable dtTask = new DataTable();
            Task oTask = new Task();
            string strQuery = "";
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);
                // strQuery = "select *  from TM_TaskMST,TM_Task_Progress where TM_TaskMST.nTaskID=" + TaskId + " AND  TM_Task_Progress.nTaskID=" + TaskId + " ";
                //strQuery = "select *  from TM_TaskMST,TM_Task_Progress,TM_Task_Assign where TM_TaskMST.nTaskID=" + TaskId + " AND  TM_Task_Progress.nTaskID=" + TaskId + " AND TM_Task_Assign.nTaskID=" + TaskId + "";
                //query changed in 5076 
                strQuery = "SELECT    TM_TaskMST.nTaskID,TM_TaskMST.nProviderID, TM_TaskMST.nPatientID, TM_TaskMST.sSubject, "
                        + "TM_TaskMST.nStartDate , TM_TaskMST.nDueDate , TM_TaskMST.nPriorityID , TM_TaskMST.nCategoryID , "
                        + "TM_TaskMST.nFollowUpID , TM_TaskMST.bIsPrivate , TM_TaskMST.nOwnerID , TM_TaskMST.nDateCreated , "
                        + "TM_TaskMST.sNoteExt , TM_TaskMST.nUserID , TM_TaskMST.nClinicID ,TM_TaskMST.nDateCreated,TM_TaskMST.sNoteExt, TM_TaskMST.nUserID,TM_TaskMST.nClinicID, TM_TaskMST.bIsBlocked, "
                        + "TM_TaskMST.nTaskType,TM_TaskMST.sFaxTiffFileName, TM_TaskMST.sMachineName,TM_TaskMST.nReferenceID1,TM_TaskMST.nReferenceID2,TM_TaskMST.nTaskGroupID, "
                        + "TM_Task_Progress.nStatusID,TM_Task_Progress.dComplete, TM_Task_Progress.sDescription, TM_Task_Progress.nDateTime, "
                        + "TM_Task_Assign.nAssignToID, TM_Task_Assign.nAssignFromID, TM_Task_Assign.nSelfAssigned, TM_Task_Assign.nAcceptRejectHold, "
                        + "dbo.GET_NAME(Provider_MST.sFirstName,Provider_MST.sMiddleName,Provider_MST.sLastName) AS  ProviderName , "
                        + "dbo.GET_NAME(Patient.sFirstName,Patient.sMiddleName,Patient.sLastName) AS PatientName "  
                        + "FROM  TM_TaskMST "
                        + "INNER JOIN TM_Task_Assign "
                        + "ON TM_TaskMST.nTaskID = TM_Task_Assign.nTaskID "
                        + "INNER JOIN TM_Task_Progress "
                        + "ON TM_TaskMST.nTaskID = TM_Task_Progress.nTaskID "
                        + "LEFT OUTER JOIN dbo.Patient "
                        + "ON Patient.nPatientID = dbo.TM_TaskMST.nPatientID "
                        + "LEFT OUTER JOIN dbo.Provider_MST "
                        + "on Provider_MST.nProviderID =dbo.TM_TaskMST.nProviderID "
                        + "WHERE "
                        + "	TM_TaskMST.nTaskID= '" + TaskId + "'";
                //oDB.Retrive_Query(strQuery, out dtTask);
                oDBps.Add("@nTASKId", TaskId, ParameterDirection.Input, SqlDbType.BigInt);
                //UpdatePILog("start query");
                oDB.Retrive("GetTask", oDBps, out dtTask);

                if (dtTask != null && dtTask.Rows.Count > 0)
                {
                    oTask.TaskID = Convert.ToInt64(dtTask.Rows[0]["nTaskID"]);

                    oTask.ProviderID = Convert.ToInt64(dtTask.Rows[0]["nProviderID"]);
                    // gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                    //if (oTask.ProviderID > 0)
                    //{
                    //  oTask.ProviderName = oResource.GetProviderName(oTask.ProviderID);
                    oTask.ProviderName = dtTask.Rows[0]["ProviderName"].ToString().Trim();
                    //}
                    //oResource.Dispose();

                    oTask.PatientID = Convert.ToInt64(dtTask.Rows[0]["nPatientID"]);
                    // gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                    //if (oTask.PatientID > 0)
                    //{
                    //oTask.PatientName = ogloPatient.GetPatientName(oTask.PatientID);
                    oTask.PatientName = dtTask.Rows[0]["PatientName"].ToString().Trim();
                    //}
                    //  ogloPatient.Dispose();


                    oTask.Subject = Convert.ToString(dtTask.Rows[0]["sSubject"]);
                    oTask.StartDate = Convert.ToInt64(dtTask.Rows[0]["nStartDate"]);
                    oTask.DueDate = Convert.ToInt64(dtTask.Rows[0]["nDueDate"]);

                    oTask.PriorityID = Convert.ToInt64(dtTask.Rows[0]["nPriorityID"]);
                    //gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
                    //oPriority = oTaskMail.GetPriority(oTask.PriorityID);
                    //oTask.Priority = oPriority.Description;
                    //oTask.PriorityLevel = oPriority.PriorityLevel;
                    //oPriority.Dispose();
                    oTask.Priority = Convert.ToString(dtTask.Rows[0]["Priority"]);
                    oTask.PriorityLevel = Convert.ToInt64(dtTask.Rows[0]["PriorityLevel"]);

                    oTask.CategoryID = Convert.ToInt64(dtTask.Rows[0]["nCategoryID"]);
                    //gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
                    //oCategory = oTaskMail.GetCategory(oTask.CategoryID);
                    //oTask.Category = oCategory.Description;
                    //oCategory.Dispose();
                    oTask.Category = Convert.ToString(dtTask.Rows[0]["Category"]);

                    oTask.FollowupID = Convert.ToInt64(dtTask.Rows[0]["nFollowupID"]);
                    //gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();
                    //oFollowUp = oTaskMail.GetFollowUp(oTask.FollowupID);
                    //oTask.Followup = oFollowUp.Description;
                    //oFollowUp.Dispose();
                    oTask.Followup = Convert.ToString(dtTask.Rows[0]["Followup"]);

                    oTask.IsPrivate = Convert.ToBoolean(dtTask.Rows[0]["bIsPrivate"]);
                    oTask.OwnerID = Convert.ToInt64(dtTask.Rows[0]["nOwnerID"]);
                    oTask.OwnerName = GetUserLoginName(oTask.OwnerID);
                    oTask.DateCreated = Convert.ToInt64(dtTask.Rows[0]["nDateCreated"]);
                    oTask.Notes = dtTask.Rows[0]["sNoteExt"].ToString();
                    oTask.UserID = Convert.ToInt64(dtTask.Rows[0]["nUserID"]);
                    oTask.ClinicID = Convert.ToInt64(dtTask.Rows[0]["nClinicID"]);

                    oTask.MachineName = Convert.ToString(dtTask.Rows[0]["sMachineName"]);
                    oTask.FaxTiffFileName = Convert.ToString(dtTask.Rows[0]["sFaxTiffFileName"]);
                    if (dtTask.Rows[0]["nTaskType"] != DBNull.Value)
                        oTask.TaskType = (TaskType)Convert.ToInt32(dtTask.Rows[0]["nTaskType"]);

                    if (dtTask.Rows[0]["nReferenceID1"] != DBNull.Value)
                        oTask.ReferenceID1 = Convert.ToInt64(dtTask.Rows[0]["nReferenceID1"]);

                    if (dtTask.Rows[0]["nReferenceID2"] != DBNull.Value)
                        oTask.ReferenceID2 = Convert.ToInt64(dtTask.Rows[0]["nReferenceID2"]);

                    if (dtTask.Rows[0]["nTaskGroupID"] != DBNull.Value)
                        oTask.TaskGroupID = Convert.ToInt64(dtTask.Rows[0]["nTaskGroupID"]);
                    //Progress Setup

                    oTask.Progress.TaskID = Convert.ToInt64(dtTask.Rows[0]["nTaskID"]);

                    oTask.Progress.StatusID = Convert.ToInt64(dtTask.Rows[0]["nStatusID"]);
                    //gloTasksMails.Common.Status oStatus = new gloTasksMails.Common.Status();
                    //oStatus = oTaskMail.GetStatus(oTask.Progress.StatusID);
                    //oTask.Progress.StatusName = oStatus.Description;
                    //oStatus.Dispose();
                    oTask.Progress.StatusName = Convert.ToString(dtTask.Rows[0]["Status"]);
                    oTask.Progress.Complete = Convert.ToDecimal(dtTask.Rows[0]["dComplete"]);
                    oTask.Progress.Description = dtTask.Rows[0]["sDescription"].ToString();
                    oTask.Progress.DateTime = Convert.ToDateTime(dtTask.Rows[0]["nDateTime"]);

                    ////Assign Setup
                    ////nAssignToID,nAssignFromID,nSelfAssigned,nAcceptRejectHold,nClinicID
                    TaskAssign oTaskAssign = new TaskAssign();

                    oTaskAssign.AssignToID = Convert.ToInt64(dtTask.Rows[0]["nAssignToID"]);
                    oTaskAssign.AssignFromID = Convert.ToInt64(dtTask.Rows[0]["nAssignFromID"]);
                    oTaskAssign.SelfAssigned = (gloTasksMails.Common.SelfAssigned)Convert.ToInt64(dtTask.Rows[0]["nSelfAssigned"]);
                    oTaskAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtTask.Rows[0]["nAcceptRejectHold"]);
                    oTaskAssign.ClinicID = Convert.ToInt64(dtTask.Rows[0]["nClinicID"]);
                    oTask.Assignment.Add(oTaskAssign);

                    return oTask;

                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtTask.Dispose();
                oTaskMail.Dispose();

            }

        }
        public TaskAssign GetTaskAssign(Int64 TaskId, Int64 UserID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTaskAssign = new DataTable();
            TaskAssign oTaskAssign = new TaskAssign();
            string strQuery = "";
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);
                //modified by pradeep 20100924
                strQuery = " SELECT   TOP 1   ISNULL(TM_Task_Assign.nTaskID,0) AS nTaskID, ISNULL(TM_Task_Assign.nAssignFromID,0) AS nAssignFromID ,ISNULL(TM_Task_Assign.nAssignToID,0) AS nAssignToID, ISNULL(TM_Task_Assign.nSelfAssigned,0) AS nSelfAssigned , " +
               " ISNULL(TM_Task_Assign.nAcceptRejectHold,0) AS nAcceptRejectHold, ISNULL(TM_Task_Assign.nClinicID,0) AS nClinicID,ISNULL(User_MST.sLoginName,'') AS sLoginName " +
               " FROM   TM_Task_Assign INNER JOIN User_MST ON TM_Task_Assign.nAssignFromID = User_MST.nUserID  WHERE TM_Task_Assign.nTaskID =  " + TaskId + " ";
                //AND  ISNULL(TM_Task_Assign.nSelfAssigned,0) <> " + gloTasksMails.Common.SelfAssigned.Self.GetHashCode() + " 
                if (isFromTrackTask == false && isFromReminder == false)
                {
                    strQuery = strQuery + " AND ISNULL(TM_Task_Assign.nAssignToID,0) = " + UserID + "";
                }
                oDB.Retrive_Query(strQuery, out dtTaskAssign);

                if (dtTaskAssign != null && dtTaskAssign.Rows.Count > 0)
                {
                    
                        ////Assign Setup
                        ////nAssignToID,nAssignFromID,nSelfAssigned,nAcceptRejectHold,nClinicID

                        oTaskAssign.AssignToID = Convert.ToInt64(dtTaskAssign.Rows[0]["nAssignToID"]);
                        oTaskAssign.AssignFromID = Convert.ToInt64(dtTaskAssign.Rows[0]["nAssignFromID"]);
                        oTaskAssign.AssignFromName = Convert.ToString(dtTaskAssign.Rows[0]["sLoginName"]);
                        oTaskAssign.SelfAssigned = (gloTasksMails.Common.SelfAssigned)Convert.ToInt64(dtTaskAssign.Rows[0]["nSelfAssigned"]);
                        oTaskAssign.AcceptRejectHold = (gloTasksMails.Common.AcceptRejectHold)Convert.ToInt64(dtTaskAssign.Rows[0]["nAcceptRejectHold"]);
                        oTaskAssign.ClinicID = Convert.ToInt64(dtTaskAssign.Rows[0]["nClinicID"]);

                   
                    return oTaskAssign;

                }
               
                return null;
                


            }

            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtTaskAssign.Dispose();
                oTaskMail.Dispose();

            }

        }

        public bool DeclineTask(Int64 TaskId, Int64 AssignToId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "UPDATE TM_Task_Assign SET nAcceptRejectHold = 2 WHERE nTaskID=" + TaskId + " AND nAssignToID=" + AssignToId + "";
                int result = oDB.Execute_Query(strQuery);

                if (result > 0)
                    return true;
                else
                    return false;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool AcceptTask(Int64 TaskId, Int64 AssignToId)
        {
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //string strQuery = "";

            //try
            //{
            //    oDB.Connect(false);
            //    strQuery = "UPDATE TM_Task_Assign SET nAcceptRejectHold = 1,nnewTaskID=1 WHERE nTaskID=" + TaskId + " AND nAssignToID=" + AssignToId + "";
            //    int result = oDB.Execute_Query(strQuery);

            //    if (result > 0)
            //        return true;
            //    else
            //        return false;

            //}
            //catch (gloDatabaseLayer.DBException dbEx)
            //{
            //    dbEx.ERROR_Log(dbEx.ToString());
            //    return false;
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //    return false;
            //}
            //finally
            //{
            //    oDB.Disconnect();
            //    oDB.Dispose();
            //}






            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {


                oDB.Connect(false);
                oParameters.Add("@nTaskID", TaskId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nAssignToID", AssignToId, ParameterDirection.Input, SqlDbType.BigInt);
                   int _returnresult = oDB.Execute ("gsp_AcceptTask", oParameters);

               
                    return true;
             
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Clear();
                oParameters.Dispose();
                  
            }







        }

        public bool MarkComplete(Int64 TaskId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            int _result = 0;
            try
            {
                oDB.Connect(false);
                strQuery = "UPDATE TM_Task_Progress SET dComplete = 100 WHERE nTaskID = " + TaskId;
                _result = oDB.Execute_Query(strQuery);

                if (_result > 0)
                    return true;
                else
                    return false;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();

            }
        }

        //added in 6050 for complete All task funtionality
        public bool CompleteAll(Int64 TaskId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {


                oDB.Connect(false);
                oParameters.Add("@nTaskID", TaskId, ParameterDirection.Input, SqlDbType.BigInt);
                //oParameters.Add("@nStatusID", 3, ParameterDirection.Input, SqlDbType.BigInt);
                //oParameters.Add("@dComplete", 100, ParameterDirection.Input, SqlDbType.Decimal);
                //oParameters.Add("@sDescription", string.Empty, ParameterDirection.Input, SqlDbType.VarChar);
                //oParameters.Add("@nDateTime",DateTime.Now , ParameterDirection.Input, SqlDbType.DateTime);
                //oParameters.Add("@nClinicID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                //oParameters.Add("@IsCompleteAll", false, ParameterDirection.Input, SqlDbType.Bit);
                int _returnresult = oDB.Execute("gsp_CompleteAll_Task", oParameters);

                if (_returnresult > 0)
                    return true;
                else
                    return false;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
            }


        }

        public bool IsMultipleUserTask(Int64 TaskGroupID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String Query = "SELECT COUNT(*) FROM TM_TaskMST WHERE nTaskGroupID = " + TaskGroupID + "";
            Object val;
            bool result = false;
            try
            {
                oDB.Connect(false);
                val = oDB.ExecuteScalar_Query(Query);
                if ((Int32)val > 0)
                {
                    result = true;
                }

                oDB.Disconnect();
            }
            catch
            {
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return result;
        }

        //added in 7010_phaseII_dev for getting Bill Pay Message Details
        public DataTable GetIntuitBillPayMessageDetails(Int64 nMessageID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBps = new gloDatabaseLayer.DBParameters();
            DataTable dtTask = new DataTable();
            try
            {
                oDB.Connect(false);


                oDBps.Add("@nCommDetailID", nMessageID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_GetIntuitBillPayMessageDetails", oDBps, out dtTask);

                oDB.Disconnect();

                oDB.Dispose();
                oDB = null;

                return dtTask;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                
                dtTask.Dispose();
                dtTask = null;

                if (oDBps != null)
                {
                    oDBps.Dispose();
                    oDBps = null;
                }
                
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        #endregion "Common Task Methods "
    }

    public class Task : IDisposable
    {
        #region " Declarations "

        #endregion " Declarations "

        #region "Constructor & Distructor"

        public Task()
        {
            _TaskAssigns = new TaskAssigns();
            _TaskProgress = new TaskProgress();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _TaskAssigns.Dispose();
                    _TaskProgress.Dispose();
                }
            }
            disposed = true;
        }

        ~Task()
        {
            Dispose(false);
        }

        #endregion

        #region " Task Master Variable Declarations "

        private Int64 _TaskID = 0;
        private Int64 _ProviderID = 0;
        private string _ProviderName = "";
        private Int64 _PatientID = 0;
        private string _PatientName = "";
        private string _PatientCode = "";
        private string _Subject = "";
        private Int64 _StartDate = 0;
        private Int64 _DueDate = 0;
        private string _Priority = "";//
        private Int64 _Prioritylevel = 0;
        private Int64 _PriorityID = 0;
        private string _Category = "";//
        private Int64 _CategoryID = 0;
        private string _Followup = "";//
        private Int64 _FollowupID = 0;
        private bool _IsPrivate = false;
        private string _OwnerName = ""; //provider or user
        private Int64 _OwnerID = 0;
        private Int64 _DateCreated = 0;
        private string _Notes = "";
        private Int64 _UserID = 0;
        //private string _UserName = "";
        private Int64 _ClinicID = 0;
        //private string _ClinicName = "";
        private TaskAssigns _TaskAssigns = null;
        private TaskProgress _TaskProgress = null;


        private string _faxTiffFileName = "";
        private string _machineName = "";
        private string _resp = "";
        private TaskType _taskType;
        private Int64 _nReferenceID1 = 0;
        private Int64 _nReferenceID2 = 0;
        private Int64 _nTaskGroupID = 0;
        private string _Assign_To = "";


        #endregion " Task Master Variable Declarations "

        #region "Property Procedures"

        public Int64 TaskID
        {
            get { return _TaskID; }
            set { _TaskID = value; }
        }
        public Int64 ProviderID
        {
            get { return _ProviderID; }
            set { _ProviderID = value; }
        }
        public string Assign_To
        {
            get { return _Assign_To; }
            set { _Assign_To = value; }

        }
        public string ProviderName
        {
            get { return _ProviderName; }
            set { _ProviderName = value; }
        }
        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        public string PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; }
        }
        public string PatientCode
        {
            get { return _PatientCode; }
            set { _PatientCode = value; }
        }
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        public Int64 StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public Int64 DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }
        public string Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }
        public Int64 PriorityLevel
        {
            get { return _Prioritylevel; }
            set { _Prioritylevel = value; }
        }
        public Int64 PriorityID
        {
            get { return _PriorityID; }
            set { _PriorityID = value; }
        }

        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }
        public Int64 CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        public string Followup
        {
            get { return _Followup; }
            set { _Followup = value; }
        }
        public Int64 FollowupID
        {
            get { return _FollowupID; }
            set { _FollowupID = value; }
        }
        public bool IsPrivate
        {
            get { return _IsPrivate; }
            set { _IsPrivate = value; }
        }
        public string OwnerName
        {
            get { return _OwnerName; }
            set { _OwnerName = value; }
        }
        public Int64 OwnerID
        {
            get { return _OwnerID; }
            set { _OwnerID = value; }
        }

        public Int64 DateCreated
        {
            get { return _DateCreated; }
            set { _DateCreated = value; }
        }
        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }
        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        //public string UserName
        //{
        //    get { return _UserName; }
        //    set { _UserName = value; }
        //}
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public string Resp
        {
            get { return _resp; }
            set { _resp = value; }
        }
        //public Int64 ClinicName
        //{
        //    get { return _ClinicName; }
        //    set { _ClinicName = value; }
        //}
        public TaskAssigns Assignment
        {
            get { return _TaskAssigns; }
            set { _TaskAssigns = value; }
        }

        public TaskProgress Progress
        {
            get { return _TaskProgress; }
            set { _TaskProgress = value; }
        }


        public string FaxTiffFileName
        {
            get { return _faxTiffFileName; }
            set { _faxTiffFileName = value; }
        }

        public string MachineName
        {
            get { return _machineName; }
            set { _machineName = value; }
        }

        public TaskType TaskType
        {
            get { return _taskType; }
            set { _taskType = value; }
        }

        public Int64 ReferenceID1
        {
            get { return _nReferenceID1; }
            set { _nReferenceID1 = value; }
        }

        public Int64 ReferenceID2
        {
            get { return _nReferenceID2; }
            set { _nReferenceID2 = value; }
        }
        public Int64 TaskGroupID
        {
            get { return _nTaskGroupID; }
            set { _nTaskGroupID = value; }
        }
        #endregion "Property Procedures"

    }

    public class Tasks : IDisposable
    {

        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public Tasks()
        {
            _innerlist = new ArrayList();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~Tasks()
        {
            Dispose(false);
        }

        #endregion

        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(Task item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(Task item)
        {
            bool result = false;
            Task obj;

            for (int i = 0; i < _innerlist.Count; i++)
            {
                //store current index being checked
                obj = new Task();
                obj = (Task)_innerlist[i];
                if (obj.TaskID == item.TaskID)
                {
                    _innerlist.RemoveAt(i);
                    result = true;
                    break;
                }
                obj = null;
            }

            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }

        public void Clear()
        {
            _innerlist.Clear();
        }

        public Task this[int index]
        {
            get
            {
                return (Task)_innerlist[index];
            }
        }

        public bool Contains(Task item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(Task item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(Task[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }
    }

    public class TaskAssign : IDisposable
    {
        #region " Declarations "

        private string _databaseconnectionstring = "";

        #endregion " Declarations "

        #region "Constructor & Distructor"


        public TaskAssign(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
        }
        public TaskAssign()
        {

        }
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~TaskAssign()
        {
            Dispose(false);
        }

        #endregion


        #region " TaskAssign Variables Declarations "

        private Int64 _TaskID = 0;
        private Int64 _AssignToID = 0;
        private string _AssignToName = "";
        private string _AssignTo = "";
        private Int64 _AssignFromID = 0;
        private string _AssignFromName = "";
        private gloTasksMails.Common.SelfAssigned _SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
        private gloTasksMails.Common.AcceptRejectHold _AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
        private Int64 _ClinicID = 0;

        #endregion " TaskAssign Variables Declarations "

        #region " Property Procedures "

        public Int64 TaskID
        {
            get { return _TaskID; }
            set { _TaskID = value; }
        }
        public Int64 AssignToID
        {
            get { return _AssignToID; }
            set { _AssignToID = value; }
        }
        public string AssignTo
        {
            get { return _AssignTo; }
            set {_AssignTo= value; }
        }
        public string AssignToName
        {
            get { return _AssignToName; }
            set { _AssignToName = value; }
        }
        public Int64 AssignFromID
        {
            get { return _AssignFromID; }
            set { _AssignFromID = value; }
        }
        public string AssignFromName
        {
            get { return _AssignFromName; }
            set { _AssignFromName = value; }
        }
        public gloTasksMails.Common.SelfAssigned SelfAssigned
        {
            get { return _SelfAssigned; }
            set { _SelfAssigned = value; }
        }
        public gloTasksMails.Common.AcceptRejectHold AcceptRejectHold
        {
            get { return _AcceptRejectHold; }
            set { _AcceptRejectHold = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }


        #endregion " Property Procedures "


    }

    public class TaskAssigns : IDisposable
    {

        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public TaskAssigns()
        {
            _innerlist = new ArrayList();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~TaskAssigns()
        {
            Dispose(false);
        }

        #endregion

        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(TaskAssign item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(TaskAssign item)
        {
            bool result = false;
            TaskAssign obj;

            for (int i = 0; i < _innerlist.Count; i++)
            {
                //store current index being checked
                obj = new TaskAssign();
                obj = (TaskAssign)_innerlist[i];
                if (obj.TaskID == item.TaskID)
                {
                    _innerlist.RemoveAt(i);
                    result = true;
                    break;
                }
                obj = null;
            }

            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }

        public void Clear()
        {
            _innerlist.Clear();
        }

        public TaskAssign this[int index]
        {
            get
            {
                return (TaskAssign)_innerlist[index];
            }
        }

        public bool Contains(TaskAssign item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(TaskAssign item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(TaskAssign[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }
    }

    public class TaskProgress : IDisposable
    {
        #region " Declarations "

        private string _databaseconnectionstring = "";

        #endregion " Declarations "

        #region "Constructor & Distructor"


        public TaskProgress(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
        }
        public TaskProgress()
        {

        }
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~TaskProgress()
        {
            Dispose(false);
        }

        #endregion


        #region " Declartions TaskProgress Variables "
        private Int64 _TaskID = 0;
        private Int64 _StatusID = 0;
        private string _StatusName = "";
        private decimal _Complete = 0;
        private string _Description = "";
        private DateTime _DateTime = DateTime.Now;
        private Int64 _ClinicID = 0;
        #endregion " Declartions TaskProgress Variables "

        #region " Public Properties "

        public Int64 TaskID
        {
            get { return _TaskID; }
            set { _TaskID = value; }
        }
        public Int64 StatusID
        {
            get { return _StatusID; }
            set { _StatusID = value; }
        }
        public string Assign_To
        {
            get { return Assign_To; }
            set {Assign_To = value;}
        }
        public string StatusName
        {
            get { return _StatusName; }
            set { _StatusName = value; }
        }
        public decimal Complete
        {
            get { return _Complete; }
            set { _Complete = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public DateTime DateTime
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }


        #endregion " Public Properties "


    }

    //---New classes after db modifications for Task 


}
