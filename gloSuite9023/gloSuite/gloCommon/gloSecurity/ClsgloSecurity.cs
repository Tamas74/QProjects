using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace gloSecurity
{
    public class gloSecurity
    {
        private string _databaseconnectionstring = "";
        private static string _MessageBoxCaption = String.Empty;
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

         #region "Constructor & Distructor"
           

            public gloSecurity(string DatabaseConnectionString)
            {
                _databaseconnectionstring = DatabaseConnectionString;

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

        ~gloSecurity()
            {
                Dispose(false);
            }

     #endregion

        #region "Show UI"
        public void ShowUserView(System.Windows.Forms.Form oParentWindow)
        {
            frmViewUsers oViewUser = new frmViewUsers(_databaseconnectionstring);
            oViewUser.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            oViewUser.MdiParent = oParentWindow;
            oViewUser.Show();
            oViewUser.BringToFront();
        }
        #endregion

        public Boolean isPatientLock(Int64 PatientID,Boolean ShowRestrictionMessage)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Boolean _isPatientLock = false;
            Boolean _isEmergencyAccess = false;
            //Bug #81090: 00000879: deceased patient status
            appSettings["CurrentPatientStatus"] = "";
            try
            {
                if (Convert.ToString(appSettings["BreakTheGlass"]) != "")
                {
                    _isEmergencyAccess = Convert.ToBoolean(appSettings["BreakTheGlass"]); 
                }

                if (_isEmergencyAccess == false)
                {
                    //Bug #81090: 00000879: deceased patient status
                    string _strQuery = "SELECT sPatientStatus FROM Patient WITH (NOLOCK) WHERE nPatientId = " + PatientID + " AND  (UPPER(sPatientStatus) = 'LOCK CHARTS' or UPPER(sPatientStatus) = 'DECEASED') ";
                    object oLocked;
                    oDB.Connect(false);

                    oLocked = oDB.ExecuteScalar_Query(_strQuery);

                    if (oLocked != null && Convert.ToString(oLocked) != "")
                    {
                        //Bug #81090: 00000879: deceased patient status
                        appSettings["CurrentPatientStatus"] = Convert.ToString(oLocked);
                        _isPatientLock = true;
                    }

                    if (ShowRestrictionMessage == true && _isPatientLock == true)
                    {
                        //Bug #81090: 00000879: deceased patient status
                        System.Windows.Forms.MessageBox.Show("The status of the patient is '" + appSettings["CurrentPatientStatus"] + "'. " + Environment.NewLine + "You can not perform any activity on this patient", _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    }
                    _strQuery = null;   
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose();}
            }
            return _isPatientLock;

        }

        public Boolean isBadDebtPatient(Int64 PatientID, bool AllowEdit = true)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = null;
            Boolean _isPatientLock = false;
            object oLocked = null; 
            appSettings["CurrentPatientStatus"] = "";
            string status = "";
            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bIsBadDebt", 0, ParameterDirection.InputOutput, SqlDbType.Bit);

                oDB.Connect(false);
                oDB.Execute("PA_Select_BadDebtPatient", oParameters, out oLocked);
                oDB.Disconnect();
                    //string _strQuery = "SELECT TOP(1) 'Bad Debt' AS sPatientStatus FROM Patient_BadDebt WITH (NOLOCK) WHERE nPatientId = " + PatientID + "";
                    //oLocked = oDB.ExecuteScalar_Query(_strQuery);
                    if (oLocked != null && Convert.ToBoolean(oLocked) != false)
                    {
                        status = "Bad Debt";
                        appSettings["CurrentPatientStatus"] = status;
                        _isPatientLock = true;
                    }
                    if (_isPatientLock == true && AllowEdit == false)
                    {
                        System.Windows.Forms.MessageBox.Show("The status of the patient is Bad Debt. " + Environment.NewLine + "You can not create Appointment or Charges for this patient", _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    }
                       oLocked = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null)
                {
                    oParameters.Clear();
                    oParameters.Dispose();
                }
            }
            return _isPatientLock;

        }



    }
}
