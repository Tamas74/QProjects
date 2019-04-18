using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace gloAppointmentBook
{
    public enum ResourceType
    {
        Provider = 1,
        Equipment = 2,
        Other = 3,
        General=4
    }

    public enum AppointmentProcedureType
    { 
        AppointmentType = 1,
        Procedure = 2
    }

    public enum AppointmentTypeFlag
    {
        None = 0,
        Followup = 1,
        Other = 2,
        NewPatient = 3
    }

    public enum FollowUpType
    {
        Day = 0,
        Week = 1,
        Month = 2,
        Year = 3
    }

    public class gloAppointmentBook
    {
        //added by kanchan on 20120103
        public delegate void gloAptHandler();   //added delegate for calling gloCommunityViewDataform for AppConfig Download form.
        public event gloAptHandler EvntAptHandler; //added event for calling gloCommunityViewDataform for AppConfig Download form.

        #region " Declarations "
        private string _MessageBoxCaption = string.Empty;
        private string _databaseconnectionstring = "";
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        #endregion " Declarations "

        #region "Constructor & Distructor"

        public gloAppointmentBook(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }


            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
         

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

        ~gloAppointmentBook()
        {
            Dispose(false);
        }

        #endregion

        #region "Show UI"
        public void ShowAppointmentView(System.Windows.Forms.Form oParentWindow)
        {
            //frmViewAppointmentBook oViewResType = new frmViewAppointmentBook();
            frmViewAppointmentBook oViewResType = frmViewAppointmentBook.GetInstance();
            oViewResType.DatabaseConnectionString = _databaseconnectionstring;
            oViewResType.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            oViewResType.MdiParent = oParentWindow;
            //added by kanchan on 20120103 for calling gloCommunityViewDataform for AppConfig Download form
            oViewResType.EvntgloCommunityHandler += getAPTHandler;
            //end
            oViewResType.Show();
        }
        private void getAPTHandler()
        {
            if (EvntAptHandler != null)
                EvntAptHandler();
        }
        #endregion

        #region Methods

        public DataTable getProviders()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            DataTable dt_Data = null;
            string _strSQL = "";
            try
            {
                //_strSQL = "select nProviderID, " +
                //" (ISNULL(sPrefix,'') + space(1) + ISNULL(sFirstName,'') + space(1) + ISNULL(sMiddleName,'') + space(1) + ISNULL(sLastName,'')) As ProviderName, " +
                //" ('(Office):'+ ISNULL(sPhoneNo,'')) As Phone, " +
                //" ('(Mobile):'+ ISNULL(sMobileNo,'')) As Mobile, " +
                //" ('(Email):'+ ISNULL(sEmail,'')) As Email, " +
                //" ('(License):'+ ISNULL(sMedicalLicenseNo,'')) AS MedicalLicenseNo, " +
                //" (ISNULL(sAddress,'') + space(1) + ISNULL(sStreet,'') + space(1) + ISNULL(sCity,'') + space(1) + ISNULL(sState,'') + space(1) + ISNULL(sZIP,'')) AS Address " +
                //" From Provider_MST";

                _strSQL = " select nProviderID,  (ISNULL(sPrefix,'') + space(1) + ISNULL(sFirstName,'') + space(1) + ISNULL(sMiddleName,'') + space(1) + ISNULL(sLastName,'')) As ProviderName, " +
                         " ('(Office):'+ ISNULL(sBusPhoneNo,'')) As Phone,  ('(Mobile):'+ ISNULL(sMobileNo,'')) As Mobile,  ('(Email):'+ ISNULL(sBusEmail,'')) As Email,  ('(License):'+ ISNULL(sMedicalLicenseNo,'')) AS MedicalLicenseNo,  (ISNULL(sBusinessAddressline1,'') + space(1) + ISNULL(sBusinessAddressline2,'') + space(1) + ISNULL(sBusinessCity,'') + space(1) + ISNULL(sBusinessState,'') + space(1) + ISNULL(sBusinessZIP,'')) AS Address  From Provider_MST  WITH(NOLOCK) WHERE  bIsblocked='FALSE' ";

                oDB.Retrive_Query(_strSQL, out dt_Data);

                if (dt_Data != null)
                {
                    return dt_Data;
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _strSQL = null;
            }

        }

        public DataTable getProvider(Int64 ProviderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            DataTable dt_Data = null;
            string _strSQL = "";
            try
            {
                //_strSQL = "select nProviderID, " +
                //" (ISNULL(sPrefix,'') + space(1) + ISNULL(sFirstName,'') + space(1) + ISNULL(sMiddleName,'') + space(1) + ISNULL(sLastName,'')) As ProviderName, " +
                //" ('(Office):'+ ISNULL(sPhoneNo,'')) As Phone, " +
                //" ('(Mobile):'+ ISNULL(sMobileNo,'')) As Mobile, " +
                //" ('(Email):'+ ISNULL(sEmail,'')) As Email, " +
                //" ('(License):'+ ISNULL(sMedicalLicenseNo,'')) AS MedicalLicenseNo, " +
                //" (ISNULL(sAddress,'') + space(1) + ISNULL(sStreet,'') + space(1) + ISNULL(sCity,'') + space(1) + ISNULL(sState,'') + space(1) + ISNULL(sZIP,'')) AS Address " +
                //" From Provider_MST";

                _strSQL = " select nProviderID,  (ISNULL(sPrefix,'') + space(1) + ISNULL(sFirstName,'') + space(1) + ISNULL(sMiddleName,'') + space(1) + ISNULL(sLastName,'')) As ProviderName, " +
                         " ('(Office):'+ ISNULL(sBusPhoneNo,'')) As Phone,  ('(Mobile):'+ ISNULL(sMobileNo,'')) As Mobile,  ('(Email):'+ ISNULL(sBusEmail,'')) As Email,  ('(License):'+ ISNULL(sMedicalLicenseNo,'')) AS MedicalLicenseNo,  (ISNULL(sBusinessAddressline1,'') + space(1) + ISNULL(sBusinessAddressline2,'') + space(1) + ISNULL(sBusinessCity,'') + space(1) + ISNULL(sBusinessState,'') + space(1) + ISNULL(sBusinessZIP,'')) AS Address  From Provider_MST  WITH(NOLOCK) WHERE  nProviderID = " + ProviderID + " AND bIsblocked='FALSE' ";

                oDB.Retrive_Query(_strSQL, out dt_Data);

                if (dt_Data != null)
                {
                    return dt_Data;
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _strSQL = null;
            }

        }

        #endregion

        #region " Follow Up Methods "

        public DataTable GetFollowUps(Int64 FollowUpId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtFollowUp = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                if (FollowUpId > 0)
                {
                    strQuery = " SELECT  nFollowUpID, sFollowUpName,convert(nchar,nDuration) AS nDuration, nCriteria, " +
                               " case nCriteria WHEN  0  then 'Day' "+
				               " when  1 then 'Week' "+
				               " when  2 then 'Month' "+
                               " end AS sCriteria ,nClinicID FROM AB_FollowUp_MST  WITH(NOLOCK) WHERE nFollowUpID=" + FollowUpId + "";
                }
                else
                {
                    strQuery = " SELECT  nFollowUpID, sFollowUpName, convert(nchar,nDuration) AS nDuration, nCriteria, " +
                               " case nCriteria WHEN  0  then 'Day' "+
				               " when  1 then 'Week' "+
				               " when  2 then 'Month' "+
                               " end AS sCriteria ,nClinicID FROM AB_FollowUp_MST  WITH(NOLOCK) ";
                }
                oDB.Retrive_Query(strQuery, out dtFollowUp);
                return dtFollowUp;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtFollowUp.Dispose();
                strQuery = null;
            }
        }

        public Int64 AddModifyFollowUps(Int64 FollowUpId, string sName,Int64 Duration,int Criteria,Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = new object();
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);

                oParameters.Add("@nFollowUpID", FollowUpId, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sFollowUpName", sName, ParameterDirection.Input, SqlDbType.VarChar, 250);
                oParameters.Add("@nDuration", Duration, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCriteria", Criteria, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("AB_INUP_FollowUp", oParameters, out _oResult);

                return Convert.ToInt64(_oResult);

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                _oResult = null;
            }
        }

        public bool IsExistsFollowUp(Int64 FollowUpId, string Name)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            object _intResult = null;

            try
            {
                oDB.Connect(false);
                if (FollowUpId == 0)
                {
                    strQuery = "SELECT  nFollowUpID, sFollowUpName, nDuration, nCriteria, nClinicID FROM AB_FollowUp_MST  WITH(NOLOCK) WHERE sFollowUpName='" + Name.Replace("'", "''") + "'";
                }
                else
                {
                    strQuery = "SELECT  nFollowUpID, sFollowUpName, nDuration, nCriteria, nClinicID FROM AB_FollowUp_MST  WITH(NOLOCK) WHERE nFollowUpID= " + FollowUpId + " AND sFollowUpName='" + Name.Replace("'", "''") + "'";
                }

                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _intResult = null;
                strQuery = null;
            }

            return _result;
        }

        public bool DeleteFollowUp(Int64 FollowUpId)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            object _intResult = null;

            try
            {
                oDB.Connect(false);

                strQuery = "DELETE FROM AB_FollowUp_MST WHERE nFollowUpID=" + FollowUpId + "";
              
              
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                strQuery = null;
                _intResult = null;
            }

            return _result;
        }

        #endregion


    }
    
    
}//namespace gloAppointmentBook
