using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Windows.Forms;

namespace gloSecurity
{

    public enum eActivityType
    {
        ADD,
        MODIFY,
        DELETE,
        UPDATE,
        Login,
        Logout,
        Other,
        RecordViewed,
        UserBlocked,
        UserUnBlocked,
        SecurityAdmin,
        ResetPassword,
        NodeAuthenticationFailure,
        Query,
        ChangePassword,
        PatientRecordAdded,
        PatientRecordModified,
        PatientRecordDeleted,
        PatientRecordViewed,
        SignatureCreated,
        SignatureValidated,

    }

    public enum enmLogOutCome
    {
        Success,
        Failure
    }

    public partial class gloAuditTrail
    {
        #region "Declaration"
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationSettings.AppSettings;
        private string _messageBoxCaption = String.Empty;

        string _databaseConnectionString;

        //
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //

        #endregion "Declaration"

        #region "Constructor & Destructor"

        public gloAuditTrail()
        {
            _databaseConnectionString = appSettings["DataBaseConnectionString"];

            //Code added on 11/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"].ToString() != "")
                {
                    _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                }
                else
                {
                    _ClinicID = 1;
                }
            }
            else 
            {
                _ClinicID = 0;
            }
            //Remark Vinayak Check this for load or constructor


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

        }

        public string DataBaseConnectionString
        {
            get { return _databaseConnectionString; }
            set { _databaseConnectionString = value; }

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

        ~gloAuditTrail()
        {
            Dispose(false);
        }

        #endregion

        ///// <summary>
        ///// CreateLog stores the information of the changes made to 
        ///// the Records such as Add,Delete,Modify etc to the Audit Trail
        ///// /// </summary>
        ///// <param name="enmLogActivity"></param>
        ///// <param name="strDescription"></param>
        ///// <param name="nPatientID"></param>
        ///// <param name="sLoginName"></param>
        ///// <param name="eLogOutCome"></param>
        ///// <returns></returns>
        //public bool CreateLog(eActivityType enmLogActivity, string strDescription, long nPatientID,string sLoginName,enmLogOutCome eLogOutCome)
        //{ 
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);    
        //    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //    try
        //    {

        //        oDB.Connect(false);
        //        string tempActivityType = "";

        //        //Checking what activity is done by the user.
        //        switch (enmLogActivity)
        //        {
        //            case eActivityType.ADD:
        //                tempActivityType = "Record Added";
        //                break;
        //            case eActivityType.DELETE:
        //                tempActivityType = "Record Deleted";
        //                break;
        //            case eActivityType.MODIFY:
        //                tempActivityType = "Record Modified";
        //                break;
        //            case eActivityType.ChangePassword:
        //                tempActivityType ="Password Change";
        //                break;
        //            case eActivityType.Login:
        //                tempActivityType = "User Login";
        //                break;
        //            case eActivityType.Logout:
        //                tempActivityType = "User Logout";
        //                break;
        //            case eActivityType.Other:
        //                tempActivityType = "Other";
        //                break;
        //        }

        //        oParameters.Add("@ActivityCategory", tempActivityType, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@Description", strDescription, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@PatientID", nPatientID, ParameterDirection.Input, SqlDbType.Int);

        //        oParameters.Add("@UserName", sLoginName, ParameterDirection.Input, SqlDbType.VarChar);

        //        //Getting the ComputerName which is running the application.
        //        string tempComputerName = System.Windows.Forms.SystemInformation.ComputerName;
        //        oParameters.Add("@MachineName", tempComputerName, ParameterDirection.Input, SqlDbType.VarChar);

        //        string tempOutCome = "";
        //        //Checking the OutCome of Activity.
        //        switch (eLogOutCome)
        //        {
        //            case enmLogOutCome.Success:
        //                tempOutCome = "Success";
        //                break;
        //            case enmLogOutCome.Failure:
        //                tempOutCome = "Failure";
        //                break;
        //        }
        //        oParameters.Add("@sOutcome", tempOutCome, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@sSoftwareComponent", "gloPMS", ParameterDirection.Input, SqlDbType.VarChar);
        //        oDB.Execute("sp_InsertAuditTrail", oParameters);
        //        return true;

        //    }//try
        //    catch(gloDatabaseLayer.DBException ex)
        //    {
        //        MessageBox.Show("Audit Log failed.", "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oParameters.Dispose();
        //        oDB.Dispose();

        //    }

        //}//create Log

        /// <summary>
        /// Method to display the Audit entries made by a particular user.
        /// 
        /// </summary>
        /// <param name="nUserID"></param>
        /// 


        public DataTable ViewLog(int nUserID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtResult;


            try
            {
                oDB.Connect(false);
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.Int);
                oDB.Retrive("sp_GetUserAudit", oParameters, out dtResult);
                return dtResult;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                MessageBox.Show("Error Retriving Audit -" + dbEx.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error - " + e.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
                oDB.Disconnect();
                oParameters.Dispose();
                oDB.Dispose();
            }


        }

        //Method to make entry of LOG in AuditTable
        public bool CreateAuditLog(eActivityType enmLogActivity, string strDescription, long nPatientID, enmLogOutCome eLogOutCome)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {

                oDB.Connect(false);
                string tempActivityType = "";

                //Checking what activity is done by the user.
                switch (enmLogActivity)
                {
                    case eActivityType.ADD:
                        tempActivityType = "Record Added";
                        break;
                    case eActivityType.DELETE:
                        tempActivityType = "Record Deleted";
                        break;
                    case eActivityType.MODIFY:
                        tempActivityType = "Record Modified";
                        break;
                    case eActivityType.ChangePassword:
                        tempActivityType = "Password Change";
                        break;
                    case eActivityType.Login:
                        tempActivityType = "User Login";
                        break;
                    case eActivityType.Logout:
                        tempActivityType = "User Logout";
                        break;
                    case eActivityType.Other:
                        tempActivityType = "Other";
                        break;
                }

                oParameters.Add("@ActivityCategory", tempActivityType, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@Description", strDescription, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@PatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);


                System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationSettings.AppSettings;
                string sLoginName = appSettings["UserName"];


                oParameters.Add("@UserName", sLoginName, ParameterDirection.Input, SqlDbType.VarChar);

                //Getting the ComputerName which is running the application.
                string tempComputerName = System.Windows.Forms.SystemInformation.ComputerName;
                oParameters.Add("@MachineName", tempComputerName, ParameterDirection.Input, SqlDbType.VarChar);

                //
                //
                oParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                //

                string tempOutCome = "";
                //Checking the OutCome of Activity.
                switch (eLogOutCome)
                {
                    case enmLogOutCome.Success:
                        tempOutCome = "Success";
                        break;
                    case enmLogOutCome.Failure:
                        tempOutCome = "Failure";
                        break;
                }
                oParameters.Add("@sOutcome", tempOutCome, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sSoftwareComponent", _messageBoxCaption, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Execute("sp_InsertAuditTrail", oParameters);
                return true;

            }//try
            catch (gloDatabaseLayer.DBException ex)
            {
                MessageBox.Show("Audit Log failed.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex.ToString();
                ex = null;
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oParameters.Dispose();
                oDB.Dispose();

            }

                
        
        }



    }//class gloAudit Trail
    

}//namespace gloSecurity
