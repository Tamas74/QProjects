using System;
using System.Data;

namespace gloAppointmentBook
{
    namespace Books
    {
       
        public class AppointmentType : IDisposable
        {

            #region " Declarations "

            private string _databaseconnectionstring = "";
            private string _MessageBoxCaption = string.Empty;
            private Int64 _AppointmentTypeID = 0;
            private string _AppointmentType = "";
            private decimal _Duration = 0;
            private Int32 _ColorCode = 0;
            private AppointmentProcedureType _AppProcType = AppointmentProcedureType.AppointmentType;
            private AppointmentTypeFlag _AppointmentTypeFlag = AppointmentTypeFlag.None;  
            private bool _IsBlocked = false;
            private bool _bIsPriorAuthRequired = false;
            private bool _bIsTurnOffReminders = false;
            private Int64 _ClinicID = 0;
            private gloGeneralItem.gloItems _resources = null;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

            
            #endregion " Declarations "

            #region "Constructor & Distructor"

            public AppointmentType(string DatabaseConnectionString)
            {
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
      
                _databaseconnectionstring = DatabaseConnectionString;
                Resources = new gloGeneralItem.gloItems();
               
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
                        if (Resources != null)
                        {
                            Resources.Clear();
                            Resources.Dispose();
                            Resources = null;
                        }
                    }
                }
                disposed = true;
            }

            ~AppointmentType()
            {
                Dispose(false);
            }

            #endregion

            #region "Property Procedures"

            public Int64 AppointmentTypeID
            {
                get { return _AppointmentTypeID; }
                set { _AppointmentTypeID = value; }
            }

            public string AppointmentTypeName
            {
                get { return _AppointmentType; }
                set { _AppointmentType = value; }
            }

            public decimal Duration
            {
                get { return _Duration; }
                set { _Duration = value; }
            }

            public Int32 ColorCode
            {
                get { return _ColorCode; }
                set { _ColorCode = value; }
            }

            public AppointmentProcedureType AppointmentProcedureType
            {
                get { return _AppProcType; }
                set { _AppProcType = value; }
            }

            public AppointmentTypeFlag AppointmentTypeFlag
            {
                get { return _AppointmentTypeFlag; }
                set { _AppointmentTypeFlag  = value; }
            }

            public bool IsBlocked
            {
                get { return _IsBlocked; }
                set { _IsBlocked = value; }
            }

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

            public gloGeneralItem.gloItems Resources
            {
                get { return _resources; }
                set { _resources = value; }
            }

            public bool IsPriorAuthRequired
            {
                get { return _bIsPriorAuthRequired; }
                set { _bIsPriorAuthRequired = value; }
            }

            public bool IsTurnOffReminders
            {
                get { return _bIsTurnOffReminders; }
                set { _bIsTurnOffReminders = value; }
            }
            #endregion

            #region Private & Public Methods

            public Int64 Add()
            {
                Int64 _result = 0;
                object _intresult = 0;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                try
                {
                    
                    //@AppointmentTypeID, @AppointmentType, @Duration, @ColorCode, @IsBlocked, @ClinicID
                    oDBParameters.Add("@AppointmentTypeID", _AppointmentTypeID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@AppointmentType", _AppointmentType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Duration", _Duration, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                    oDBParameters.Add("@ColorCode", _ColorCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@nAppProcType", _AppProcType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nAppointmentTypeFlag", _AppointmentTypeFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    oDBParameters.Add("@IsBlocked", _IsBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@bIsPriorAuthRequired", _bIsPriorAuthRequired, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@bIsTurnOffReminders", _bIsTurnOffReminders, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);

                    _result = oDB.Execute("AB_INUP_AppointmentType", oDBParameters, out _intresult);

                    if (_intresult != null)
                    {
                        if (_intresult.ToString().Trim() != "")
                        {
                            if (Convert.ToInt64(_intresult) > 0)
                            {
                                _result = Convert.ToInt64(_intresult.ToString());
                            }
                        }
                    }

                    if (_result > 0)
                    {
                        //delete records from the detail table
                        string _strSQL = "delete from AB_AppointmentType_Details where nAppointmentTypeID = " + _result;
                        int _res = 0;

                        _res = oDB.Execute_Query(_strSQL);
                        _strSQL = null;
                        //add records in the detail table dbo.AB_Appointment_ProcedureResources
                        gloGeneralItem.gloItem oItem = null;
                        for (int i = 0; i <= _resources.Count - 1; i++)
                        {
                           // oItem = new gloGeneralItem.gloItem();
                            oItem = _resources[i];
                            AddAppointmentResources(_result, oItem.ID);
                        }//for
                        oItem = null;

                    }//if
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDB.Dispose();
                    _intresult = null;
                }

                return _result;
            }

            public bool Modify()
            {
                bool _result = false;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                try
                {
                    int _intresult = 0;
                    //@AppointmentTypeID, @AppointmentType, @Duration, @ColorCode, @IsBlocked, @ClinicID
                    oDBParameters.Add("@AppointmentTypeID", _AppointmentTypeID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@AppointmentType", _AppointmentType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Duration", _Duration, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                    oDBParameters.Add("@ColorCode", _ColorCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@nAppProcType", _AppProcType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nAppointmentTypeFlag", _AppointmentTypeFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    oDBParameters.Add("@IsBlocked", _IsBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@bIsPriorAuthRequired", _bIsPriorAuthRequired, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@bIsTurnOffReminders", _bIsTurnOffReminders, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);

                    _intresult = oDB.Execute("AB_INUP_AppointmentType", oDBParameters);
                    if (_intresult > 0)
                    {

                        if (_intresult > 0)
                        {
                            //delete records from the detail table
                            string _strSQL = "delete from AB_AppointmentType_Details where nAppointmentTypeID = " + _AppointmentTypeID;
                            int _res = 0;

                            _res = oDB.Execute_Query(_strSQL);
                            _strSQL = null;
                            //add records in the detail table dbo.AB_Appointment_ProcedureResources
                            gloGeneralItem.gloItem oItem = null;
                            for (int i = 0; i <= _resources.Count - 1; i++)
                            {
                                //oItem = new gloGeneralItem.gloItem();
                                oItem = _resources[i];
                                AddAppointmentResources(_AppointmentTypeID, oItem.ID);
                            }//for
                            oItem = null;
                        }//if


                    }
                    _result = true;

                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDB.Dispose();
                }

                return _result;
            }

            public bool Block()
            {
                bool _result = false;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "UPDATE AB_AppointmentType SET bIsBlocked = 1 WHERE nAppointmentTypeID = " + _AppointmentTypeID;
                    int _intresult = 0;
                    _intresult = oDB.Execute_Query(_sqlQuery);
                    _sqlQuery = null;

                    if (_intresult > 0)
                    {
                        _result = true;
                    }
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }

            public bool DeleteAppointmentType(Int64 ID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                try
                {
                    oDB.Connect(false);
                    //First delete all the Resouces associated with the AppointmentType from Detail Table if any.
                    //strQuery = "delete from AB_AppointmentType_Details where nAppointmentTypeID = " + ID + " ";
                    strQuery = "delete from AB_AppointmentType where nAppointmentTypeID = " + ID + " ";//sandip darade 200809010
                    oDB.Execute_Query(strQuery);
                    strQuery = "";

                    //Delete the Master entry for the Appointment Type
                    strQuery = "delete from AB_AppointmentType where nAppointmentTypeID =" + ID +" AND nAppProcType = "+ AppointmentProcedureType.AppointmentType.GetHashCode() +" ";
                    int result = oDB.Execute_Query(strQuery);

                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (gloDatabaseLayer.DBException dbErr)
                {
                    dbErr.ERROR_Log(dbErr.ToString());
                    return false;

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;
                }
            }

            public bool DeleteProcedureType(Int64 ID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                try
                {
                    oDB.Connect(false);

                    //First delete all the Resouces associated with the ProcedureType from Detail Table if any.
                    strQuery = "delete from AB_AppointmentType_Details where nAppointmentTypeID = " + ID + " ";
                    oDB.Execute_Query(strQuery);
                    strQuery = "";

                    //Delete the Procedures master entry
                    strQuery = "delete from AB_AppointmentType where nAppointmentTypeID =" + ID + " AND nAppProcType = " + AppointmentProcedureType.Procedure.GetHashCode() + " ";
                    int result = oDB.Execute_Query(strQuery);
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (gloDatabaseLayer.DBException dbErr)
                {
                    dbErr.ERROR_Log(dbErr.ToString());
                    return false;

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;
                }
            }

            public bool Unblock(Int64 ID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                try
                {
                    oDB.Connect(false);
                    strQuery = " UPDATE AB_AppointmentType SET bIsBlocked = 0 WHERE nAppointmentTypeID = " + ID + " ";
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
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;
                }
            }

            public bool DeleteAllAppointmentTypes()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                DataTable dt = null;
                Int64 _AppointmentTypeId = 0;
                int _result = 0;
                try
                {
                    oDB.Connect(false);

                    //Get all the Blocked Appointment Types.
                    strQuery = " SELECT nAppointmentTypeID FROM AB_AppointmentType  WITH(NOLOCK) WHERE bIsBlocked = 1 AND nAppProcType = " + AppointmentProcedureType.AppointmentType.GetHashCode() + " AND nClinicID =" + this.ClinicID + "  ";
                    oDB.Retrive_Query(strQuery, out dt);

                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            _AppointmentTypeId = Convert.ToInt64(dt.Rows[i][0]);

                            if(CanDeleteAppointmentType(_AppointmentTypeId))
                            {
                                //First delete all the Resouces associated with the ProcedureType from Detail Table if any.
                                strQuery = "delete from AB_AppointmentType_Details where nAppointmentTypeID = " + _AppointmentTypeId + " ";
                                oDB.Execute_Query(strQuery);
                                strQuery = "";

                                //Delete the Procedures master entry
                                strQuery = "delete from AB_AppointmentType where nAppointmentTypeID =" + _AppointmentTypeId + " ";
                                _result = oDB.Execute_Query(strQuery);
                                
                            }
                        }
                    }

                    return Convert.ToBoolean(_result);

                }
                catch (gloDatabaseLayer.DBException dbEx)
                {
                    dbEx.ERROR_Log(dbEx.ToString());
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;
                    if (dt != null) { dt.Dispose(); dt = null; }
                }
            }

            public bool DeleteAllProcedureTypes()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                DataTable dt = null;
                Int64 _ProcedureTypeId = 0;
                int _result = 0;
                try
                {
                    oDB.Connect(false);
                    //Get all the Blocked Appointment Types.
                    strQuery = " SELECT nAppointmentTypeID FROM AB_AppointmentType  WITH(NOLOCK) WHERE bIsBlocked = 1 AND nAppProcType = " + AppointmentProcedureType.Procedure.GetHashCode() + " AND nClinicID =" + this.ClinicID + "  ";
                    oDB.Retrive_Query(strQuery, out dt);

                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            _ProcedureTypeId = Convert.ToInt64(dt.Rows[i][0]);

                            if (CanDeleteProcedureType(_ProcedureTypeId))
                            {
                                //First delete all the Resouces associated with the ProcedureType from Detail Table if any.
                                strQuery = "delete from AB_AppointmentType_Details where nAppointmentTypeID = " + _ProcedureTypeId + " ";
                                oDB.Execute_Query(strQuery);
                                strQuery = "";

                                //Delete the Procedures master entry
                                strQuery = "delete from AB_AppointmentType where nAppointmentTypeID =" + _ProcedureTypeId + " ";
                                int result = oDB.Execute_Query(strQuery);

                            }
                        }
                    }
                    return Convert.ToBoolean(_result);

                }
                catch (gloDatabaseLayer.DBException dbEx)
                {
                    dbEx.ERROR_Log(dbEx.ToString());
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;
                    if (dt != null) { dt.Dispose(); dt = null; }
                }
            }

            public System.Data.DataTable GetList(AppointmentProcedureType oType)
            {
                System.Data.DataTable _result = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    //Bug #77530: 00000215 : Calendar / Scheduling
                    // Order by clause added to resolve the above issue.
                    string _sqlQuery = "SELECT nAppointmentTypeID, sAppointmentType,convert(nchar,nDuration) as nDuration , sColorCode,nAppProcType, nClinicID, bIsPriorAuthRequired FROM AB_AppointmentType  WITH(NOLOCK) WHERE (bIsBlocked = 0 AND nAppProcType = " + oType.GetHashCode() + ") AND nClinicID = " + this.ClinicID + " order by sAppointmentType";
                    
                    oDB.Retrive_Query(_sqlQuery, out _result);
                    _sqlQuery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }

            public System.Data.DataTable GetList(AppointmentProcedureType oType,String sFilter)
            {
                System.Data.DataTable _result = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {

                    String _sqlQuery = "SELECT nAppointmentTypeID, sAppointmentType,convert(nchar,nDuration) as nDuration , sColorCode,nAppProcType, nClinicID, bIsPriorAuthRequired FROM AB_AppointmentType  WITH(NOLOCK) WHERE (bIsBlocked = 0 AND nAppProcType = " + oType.GetHashCode() + ") AND nClinicID = " + this.ClinicID + " ";
                    if (sFilter != "")
                        _sqlQuery += " and " + sFilter;
                    
                    oDB.Retrive_Query(_sqlQuery, out _result);
                    _sqlQuery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }

            public System.Data.DataTable GetBlockedAppType(AppointmentProcedureType oType)
            {
                System.Data.DataTable _result = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    //string _sqlQuery = "SELECT nAppointmentTypeID, sAppointmentType, nDuration, sColorCode,nAppProcType, nClinicID FROM AB_AppointmentType WHERE bIsBlocked = 0 AND nAppProcType = " + oType.GetHashCode() + "";
                    string _sqlQuery = "SELECT nAppointmentTypeID, sAppointmentType, nDuration, sColorCode,nAppProcType, nClinicID FROM AB_AppointmentType  WITH(NOLOCK) WHERE (bIsBlocked = 1 AND nAppProcType = " + oType.GetHashCode() + ") AND nClinicID = " + this.ClinicID + "";
                    //
                    oDB.Retrive_Query(_sqlQuery, out _result);
                    _sqlQuery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }

            public System.Data.DataTable GetAppointmentType(Int64 appTypeID)
            {
                System.Data.DataTable _result = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "SELECT nAppointmentTypeID, sAppointmentType, nDuration, sColorCode, nAppProcType, nAppointmentTypeFlag,bIsBlocked, nClinicID,bIsPriorAuthRequired, bIsTurnOffReminders FROM AB_AppointmentType  WITH(NOLOCK) where nAppointmentTypeID='" + appTypeID + "'";
                    oDB.Retrive_Query(_sqlQuery, out _result);
                    _sqlQuery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }

            public bool IsExists(Int64 appTypeID, string appTypeName, AppointmentProcedureType oType)
            {
                bool _result = false;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    //AB_AppointmentType
                    //nAppointmentTypeID, sAppointmentType, nDuration, sColorCode, bIsBlocked, nClinicID
                    string _sqlQuery = "";
                    if (appTypeID == 0)
                    {
                        //_sqlQuery = "SELECT Count(nAppointmentTypeID) FROM AB_AppointmentType WHERE sAppointmentType ='" + appTypeName + "' AND nAppProcType = " + oType.GetHashCode() + "";
                        _sqlQuery = "SELECT Count(nAppointmentTypeID) FROM AB_AppointmentType  WITH(NOLOCK) WHERE (sAppointmentType ='" + appTypeName.Replace("'", "''") + "' AND nAppProcType = " + oType.GetHashCode() + ") AND nClinicID = " + this.ClinicID + " ";
                        //
                    }
                    else
                    {
                        //_sqlQuery = "SELECT Count(nAppointmentTypeID) FROM AB_AppointmentType WHERE sAppointmentType ='" + appTypeName + "' AND nAppointmentTypeID <> " + appTypeID + " AND nAppProcType = " + oType.GetHashCode() + "";
                        _sqlQuery = "SELECT Count(nAppointmentTypeID) FROM AB_AppointmentType  WITH(NOLOCK) WHERE (sAppointmentType ='" + appTypeName.Replace("'", "''") + "' AND nAppointmentTypeID <> " + appTypeID + " AND nAppProcType = " + oType.GetHashCode() + ") AND nClinicID = " + this.ClinicID + " ";
                        //
                    }

                    object _intresult = null;
                    _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                   
                    if (_intresult != null)
                    {
                        if (_intresult.ToString().Trim() != "")
                        {
                            if (Convert.ToInt64(_intresult) > 0)
                            {
                                _result = true;
                            }
                        }
                    }
                    _intresult = null;
                    _sqlQuery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }

            public bool IsBlock(string Description)
            {
                bool _result = false;

                //return true;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "SELECT ID FROM TABLE WHERE FIELD"; //Check For Transaction Table
                    object _intresult = null;
                    _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (_intresult != null)
                    {
                        if (_intresult.ToString().Trim() != "")
                        {
                            if (Convert.ToInt64(_intresult) > 0)
                            {
                                _result = true;
                            }
                        }
                    }
                    _intresult = null;
                    _sqlQuery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }

            //sarika 24th dec 07
            public int AddAppointmentResources(Int64 AppointmentTypeID, Int64 ResourceID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                int _intresult = 0;
                oDB.Connect(false);

                try
                {
                    oDBParameters.Add("@nAppointmentTypeID", AppointmentTypeID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nResourceID", ResourceID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                    _intresult = oDB.Execute("AB_INUP_AppointmentType_Details", oDBParameters);
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDB.Dispose();
                }
                return _intresult;
            }//AddAppointmentResources

            public System.Data.DataTable GetProblemTypeResources(Int64 AppointmentTypeID)
            {

                System.Data.DataTable dtAppTypeResources = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);

                try
                {
                    string _strSQL = "SELECT isnull(AB_AppointmentType_Details.nAppointmentTypeID,0) as nAppointmentTypeID, isnull(AB_AppointmentType_Details.nResourceID,0) as nResourceID, isnull(AB_Resource_MST.sDescription,'') as sDescription,isnull(AB_Resource_MST.sCode,'') as sCode  FROM  AB_AppointmentType_Details  WITH(NOLOCK) INNER JOIN   AB_Resource_MST  WITH(NOLOCK) ON AB_AppointmentType_Details.nResourceID = AB_Resource_MST.nResourceID where nAppointmentTypeID=" + AppointmentTypeID;

                    oDB.Retrive_Query(_strSQL, out dtAppTypeResources);
                    _strSQL = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

                return dtAppTypeResources;
            }

            public System.Data.DataTable GetAppointmentTypeProcedures(Int64 AppointmentTypeID)
            {

                System.Data.DataTable dtAppTypeResources = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);

                try
                {
                    string _strSQL = "SELECT AB_AppointmentType.nAppointmentTypeID AS nProblemTypeID, "
                    + " AB_AppointmentType.sAppointmentType AS sProblemType "
                    + " FROM  AB_AppointmentType_Details  WITH(NOLOCK) INNER JOIN AB_AppointmentType  WITH(NOLOCK)  "
                    + " ON AB_AppointmentType_Details.nResourceID = AB_AppointmentType.nAppointmentTypeID "
                    + " WHERE AB_AppointmentType_Details.nAppointmentTypeID = " + AppointmentTypeID + " AND nClinicID = " + _ClinicID + " ";

                    oDB.Retrive_Query(_strSQL, out dtAppTypeResources);
                    _strSQL = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

                return dtAppTypeResources;
            }

            public bool CanDeleteAppointmentType(Int64 AppointmentTypeID)
            {
                bool Result = false;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                //DataTable dt = new DataTable();
                try
                {
                    oDB.Connect(false);

                    //string SqlQuery = "";
                    //SqlQuery = "SELECT nAppointmentID FROM AS_Appointment_DTL  WHERE nAppointmentTypeID = " + AppointmentTypeID;
                    //oDB.Retrive_Query(SqlQuery, out dt);

                    //if (dt.Rows.Count == 0)
                    //{
                    //    Result = true;
                    //}

                    //*** Remark - We have already removed can delete logic from setup appintment,
                    // we have to implement same logic in setup appointment template - for modify as well as for new
                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }

                finally
                {
                    oDB.Dispose();
                }
                Result = true;
                return Result;
            }

            public bool CanDeleteProcedureType(Int64 ProcedureTypeID)
            {
                bool Result = false;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dt = null;
                try
                {
                    oDB.Connect(false);

                    string SqlQuery = "";
                    SqlQuery = "SELECT nAppointmentID FROM AS_Appointment_DTL_Procedures  WITH(NOLOCK) WHERE nProcedureID = " + ProcedureTypeID;
                    oDB.Retrive_Query(SqlQuery, out dt);

                    if (dt.Rows.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        Result = true;
                        dt = new DataTable();
                    }

                    SqlQuery = "SELECT nScheduleID FROM AS_Schedule_DTL_Procedures   WITH(NOLOCK) WHERE nProcedureID = " + ProcedureTypeID;
                    oDB.Retrive_Query(SqlQuery, out dt);

                    if (dt.Rows.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        Result = true;
                        dt = new DataTable();
                    }
                    SqlQuery = null;
                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Dispose();
                }
                return Result;
            } 

            #endregion

            public DataTable GetAppointmentTypes(AppointmentTypeFlag appointmentTypeFlag)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dt = null;
                try
                {
                    oDB.Connect(false);
                    string SqlQuery = "SELECT nAppointmentTypeID, sAppointmentType, nDuration FROM  AB_AppointmentType  WITH(NOLOCK)  "
                                    + " WHERE nAppProcType = 1 AND (bIsBlocked IS NULL OR bIsBlocked <> 1) "
                                    + " AND nClinicID =  " + _ClinicID  + " AND nAppointmentTypeFlag = " + appointmentTypeFlag.GetHashCode();
                    oDB.Retrive_Query(SqlQuery, out dt);
                    SqlQuery = null;
                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Dispose();
                }
                return dt;
            }


            public Int64 AddApptmnt_Provider(Int64 ProID,string AppCode,string AppDesc, Int64 AppFlag )
            {
                Int64 _result = 0;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                try
                {
                    if (ProID != 0)
                    {
                        string Sqlquery = "delete from AB_Provider_AppointmentType where nProviderID = " + ProID + "";
                        oDB.Execute_Query(Sqlquery);
                        Sqlquery = null;
                    }

                    object _intresult = 0;
                    //@ProviderID @AppointmentTypeCode @AppointmentTypeDesc @AppointmentTypeFlag @ClinicID
                    oDBParameters.Add("@ProviderID", ProID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@AppointmentTypeCode", AppCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@AppointmentTypeDesc",AppDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@AppointmentTypeFlag", AppFlag, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                    _result = oDB.Execute("AB_INSERT_AppointmentType_Provider", oDBParameters);

                    if (_intresult != null)
                    {
                        if (_intresult.ToString().Trim() != "")
                        {
                            if (Convert.ToInt64(_intresult) > 0)
                            {
                                _result = Convert.ToInt64(_intresult.ToString());
                            }
                        }
                    }

                    _intresult = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDB.Dispose();
                }

                return _result;
            }

            public DataTable GetApptmnt_Providers()
            {
                DataTable _result = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "SELECT AB_Provider_AppointmentType.nProviderID,AB_Provider_AppointmentType.sAppointmentTypeCode," +
                        "ISNULL(Provider_MST.sFirstName,'')+SPACE(1)+ISNULL(Provider_MST.sMiddleName,'')+SPACE(1)+ISNULL(Provider_MST.sLastName,'')AS sname ," +
                    "AB_Provider_AppointmentType.sAppointmentTypeDesc,AB_Provider_AppointmentType.nAppointmentTypeFlag FROM " +
                    "AB_Provider_AppointmentType  WITH(NOLOCK) INNER JOIN Provider_MST  WITH(NOLOCK) ON AB_Provider_AppointmentType.nProviderID = Provider_MST.nProviderID WHERE  AB_Provider_AppointmentType.nClinicID =" + _ClinicID + "";

                    _sqlQuery += " ORDER BY Provider_MST.sFirstName ";

                    oDB.Retrive_Query(_sqlQuery, out _result);
                    _sqlQuery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }

            public DataTable GetApptmnt_Provider( Int64 ProviderID )
            {
                DataTable _result = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "SELECT AB_Provider_AppointmentType.nProviderID,AB_Provider_AppointmentType.sAppointmentTypeCode," +
                        "ISNULL(Provider_MST.sFirstName,'')+SPACE(1)+ISNULL(Provider_MST.sMiddleName,'')+SPACE(1)+ISNULL(Provider_MST.sLastName,'')AS ProviderName ," +
                    "AB_Provider_AppointmentType.sAppointmentTypeDesc,AB_Provider_AppointmentType.nAppointmentTypeFlag FROM " +
                    "AB_Provider_AppointmentType WITH(NOLOCK)  INNER JOIN Provider_MST  WITH(NOLOCK) ON AB_Provider_AppointmentType.nProviderID = Provider_MST.nProviderID WHERE  AB_Provider_AppointmentType.nClinicID =" + _ClinicID + " AND  AB_Provider_AppointmentType.nProviderID=" + ProviderID + "";

                    oDB.Retrive_Query(_sqlQuery, out _result);
                    _sqlQuery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;

            }

            public void DeleteApptmnt_Provider(Int64 ProviderID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                try
                {
                    string Sqlquery = "delete from AB_Provider_AppointmentType where nProviderID = " + ProviderID + "";
                    oDB.Execute_Query(Sqlquery);
                    Sqlquery = null;
                }

               catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDB.Dispose();
                }
            }
                
        }
    }
}
