using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text;
using System.Data;
using System.Windows.Forms;

namespace gloBilling
{
    class clsStandardReasonCode :IDisposable
    {
        #region " Constructor & Destructor "

        private bool disposed = false;
        public clsStandardReasonCode()
        {
            nCASType = 1;
            _DataBaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            UserID = gloGlobal.gloPMGlobal.UserID;

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM"; ;
                }
            }
            else
            { _messageboxcaption = "gloPM"; ; }

            #endregion        
        }
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
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (oDBPara != null) { oDBPara.Dispose(); oDBPara = null; }

                }
            }
            disposed = true;
        }       

        ~clsStandardReasonCode()
        {
            Dispose(false);
        }
        #endregion

        #region " Variable Declaration "
        private string _DataBaseConnectionString = "";
        private gloDatabaseLayer.DBLayer oDB = null;
        private gloDatabaseLayer.DBParameters oDBPara = null;
        int nCASType;
        int Flag = 1;
        private Int64 ClinicID = 1;
        private Int64 UserID = 0;

        //Tuple<long, string, string, string, string, Boolean> _SelectedReasonCode = null;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _messageboxcaption = "";
        #endregion 

        //GetAll
        public DataTable GetStandardReasonCodeList()
        {
            DataTable _dt = null;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Add("@nCASType", nCASType, ParameterDirection.Input, SqlDbType.Int);
                    oDBPara.Add("@Flag", Flag, ParameterDirection.Input, SqlDbType.Int);
                    oDB.Retrive("BL_SELECT_Standard_ReasonCode", oDBPara, out _dt);
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _dt;
        }

        //CheckIsSystemdefined or Not
        public bool isSystemDefinedReasonCode(Int64 ReasonId)
        {
            bool Result = true;                        
            object value = null;
            try
            {
                if (OpenConnection(true))
                {
                    string SqlQuery = "";
                    SqlQuery = "SELECT ISNULL(IsSystemDefined,'false') AS bIsSystem FROM Insurance_DefaultReasonCodes WITH (NOLOCK)  WHERE [nReasonCodeID]=" + ReasonId;
                    value = oDB.ExecuteScalar_Query(SqlQuery);
                    if (value != null && Convert.ToString(value) != "")
                    {
                        Result = (bool)(value);
                    }
                }
                CloseConnection();
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }            
            return Result;
        }

        //CheckAlredyPresent or Not
        public bool CheckDuplicate(String GroupCode, String ReasonCode)
        {           
            string strQuery = "";
            bool _result = false;
            object _intResult = null;
            try
            {
                if (OpenConnection(true))
                {
                    
                    strQuery = "SELECT COUNT([nReasonCodeID]) FROM Insurance_DefaultReasonCodes WHERE [sGroupCode]='" + GroupCode + "' AND [sReasonCode]='" + ReasonCode + "'";
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
                CloseConnection();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }           
            return _result;
        }        

        #region " Open/Close Database Connection "
        private bool OpenConnection(bool withParameters)
        {
            bool _Result = false;
            try
            {
                if (_DataBaseConnectionString != "")
                {
                    oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                    oDB.Connect(false);
                    if (withParameters)
                        oDBPara = new gloDatabaseLayer.DBParameters();
                    _Result = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _Result;
        }

        private void CloseConnection()
        {
            try
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBPara != null)
                {
                    oDBPara.Dispose();
                    oDBPara = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        #endregion
    }   
}
