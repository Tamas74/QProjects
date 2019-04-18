using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace gloBilling
{
    class BillingAssociation
    {
        #region "Constructor & Distructor"

            private string _databaseconnectionstring = "";
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private Int64 _ClinicID = 0;
            private string _messageBoxCaption = "";
            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }
            //


            public BillingAssociation(string DatabaseConnectionString)
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
                //

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

            ~BillingAssociation()
            {
                Dispose(false);
            }

        #endregion

        public System.Data.DataTable GetCPTs()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                oDB.Connect(false);
                string _strquery = "";
                //_strquery = "SELECT nCPTID, sCPTCode, sDescription, nSpecialtyID, nCategoryID FROM CPT_MST";
                //
                _strquery = "SELECT nCPTID,sCPTCode,sDescription FROM CPT_MST WITH (NOLOCK) WHERE nClinicID = " + ClinicID + "";
               
                //
                oDB.Retrive_Query(_strquery, out dt);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return dt;
        }

        public System.Data.DataTable GetICD9s()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                oDB.Connect(false);
                string _strquery = "";
                //_strquery = "SELECT nICD9ID, sICD9Code, sDescription, nSpecialtyID FROM ICD9";
                //
                _strquery = "SELECT nICD9ID, sICD9Code, sDescription, nSpecialtyID FROM ICD9 WITH (NOLOCK) WHERE nClinicID = " + this.ClinicID + " ";
                //
                oDB.Retrive_Query(_strquery, out dt);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
               
            }
            return dt;
        }

        public System.Data.DataTable GetModifiers()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                oDB.Connect(false);
                string _strquery = "";
                //_strquery = "SELECT nModifierID, sModifierCode, sDescription FROM Modifier_MST";
                //
                _strquery = "SELECT nModifierID, sModifierCode, sDescription FROM Modifier_MST WITH (NOLOCK) where nClinicID=" + this.ClinicID + " ";
                //
                oDB.Retrive_Query(_strquery, out dt);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                
            }
            return dt;
        }

        public System.Data.DataTable FetchICD9(long ID)
        {
            System.Data.DataTable dt;
           
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            dt = new System.Data.DataTable();
            try
            {
                oDB.Connect(false);
                string _strquery = "";
                //_strquery = "SELECT BL_CPTICD9.nICD9ID , ICD9.sICD9Code + '-' + ICD9.sDescription , '' FROM BL_CPTICD9 INNER JOIN ICD9 ON BL_CPTICD9.nICD9ID = ICD9.nICD9ID WHERE (BL_CPTICD9.nCPTID = " + ID + ")  ";
                //
                _strquery = "SELECT BL_CPTICD9.nICD9ID , ICD9.sICD9Code + '-' + ICD9.sDescription , '' FROM BL_CPTICD9 WITH (NOLOCK) INNER JOIN ICD9 WITH (NOLOCK) ON BL_CPTICD9.nICD9ID = ICD9.nICD9ID WHERE (BL_CPTICD9.nCPTID = " + ID + ")  ";
                //
                oDB.Retrive_Query(_strquery, out dt);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return dt;
        }

        public System.Data.DataTable GetICD9s(string SearchText, int IcdCodeType)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dtList = null;
            try
            {
                _dtList = new System.Data.DataTable();
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@SearchString", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nICDRevision", IcdCodeType.GetHashCode(), ParameterDirection.Input, SqlDbType.SmallInt);
                oDB.Retrive("gsp_Diagnosis_Search", oParameters, out _dtList);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }

            }
            return _dtList;
        }

        public System.Data.DataTable GetICD10(string SearchText)
        {
            gloDatabaseLayer.DBLayer oDB = null;

            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dtList = null;
            try
            {
                _dtList = new System.Data.DataTable();
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@SearchText", SearchText, ParameterDirection.Input, SqlDbType.VarChar);                
                oDB.Retrive("ICD10_GetTreatmentCodes", oParameters, out _dtList);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }

            }
            return _dtList;
        }

        public System.Data.DataTable GetBillableICD10Codes(string ParentCode)
        {
            gloDatabaseLayer.DBLayer oDB = null;

            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dtList = null;
            try
            {
                _dtList = new System.Data.DataTable();
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@ParentCode", ParentCode, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("ICD10_GetBillableCodesUnderParent", oParameters, out _dtList);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }

            }
            return _dtList;
        }
    }
    
}
