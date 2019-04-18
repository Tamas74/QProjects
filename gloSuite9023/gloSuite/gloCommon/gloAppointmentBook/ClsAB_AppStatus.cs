using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace gloAppointmentBook
{
    namespace Books
    {
        public class AppointmentStatus : IDisposable
        {
            #region " Declarations "

            private Int64 _AppointmentStatusID = 0;
            private string _AppointmentStatus = "";
            private bool _IsBlocked = false;
            private bool _IsSystem = false; 
            private Int64 _ClinicID = 0;
            private string _databaseconnectionstring = "";
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

            private string _MessageBoxCaption = string.Empty;

            #endregion " Declarations "


            #region "Constructor & Distructor"


            //public AppointmentStatus()
            //{
            //}

            public AppointmentStatus(string DatabaseConnectionString)
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

            ~AppointmentStatus()
            {
                Dispose(false);
            }

            #endregion


            #region "Property Procedures"
            
            public Int64 AppointmentStatusID
            {
                get { return _AppointmentStatusID; }
                set { _AppointmentStatusID = value; }
            }

            public string AppointmentStatusName
            {
                get { return _AppointmentStatus; }
                set { _AppointmentStatus = value; }
            }

            public bool IsBlocked
            {
                get { return _IsBlocked; }
                set { _IsBlocked = value; }
            }

            public bool IsSystem
            {
                get { return _IsSystem; }
                set { _IsSystem = value; }
            }

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }
            #endregion


            #region Private & Public Methods

            public Int64 Add()
            {
                Int64 _result = 0;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                try
                {
                    object _intresult = 0;
                    //@AppointmentStatusID, @AppointmentStatus, @IsBlocked, @ClinicID
                    oDBParameters.Add("@AppointmentStatusID", _AppointmentStatusID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@AppointmentStatus", _AppointmentStatus, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@IsBlocked", _IsBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@IsSystem", _IsSystem, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                    oDB.Execute("AB_INUP_AppointmentStatus", oDBParameters, out _intresult);

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
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                }
                finally
                {
                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDB.Dispose();
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
                    //@AppointmentStatusID, @AppointmentStatus, @IsBlocked, @ClinicID
                    oDBParameters.Add("@AppointmentStatusID", _AppointmentStatusID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@AppointmentStatus", _AppointmentStatus, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@IsBlocked", _IsBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@IsSystem", _IsSystem, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                    _intresult = oDB.Execute("AB_INUP_AppointmentStatus", oDBParameters);
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
                    // Block The Appointment Status
                    string _sqlQuery = "UPDATE AB_AppointmentStatus SET bIsBlocked = 1 WHERE nAppointmentStatusID = " + _AppointmentStatusID;
                    int _intresult = 0;
                    _intresult = oDB.Execute_Query(_sqlQuery);
                    if (_intresult > 0)
                    {
                        // if Blocked
                        _result = true;
                    }
                    _sqlQuery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }

            public bool Unblock(Int64 ID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                try
                {
                    oDB.Connect(false);
                    strQuery = " UPDATE AB_AppointmentStatus SET bIsBlocked = '" + false + "' WHERE nAppointmentStatusID = " + ID + " ";
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
                    return false;
                }
                finally
                {
                    strQuery = null;
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            public bool DeleteAll()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dt = null;
                string strQuery = "";
                Int64 _ApptStatusId = 0;
                int _result=0;

                try
                {
                    oDB.Connect(false);
                    
                    //Get all blocked Appointment Status Types
                    strQuery = " SELECT nAppointmentStatusID FROM AB_AppointmentStatus  WITH(NOLOCK)  WHERE bIsBlocked = '" + true + "' AND nClinicID = " + this.ClinicID + " ";
                    oDB.Retrive_Query(strQuery, out dt);
                    
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            _ApptStatusId = Convert.ToInt64(dt.Rows[i][0]);
                            strQuery = "";
                            if (CanDelete(_ApptStatusId))
                            {
                                strQuery = " DELETE FROM AB_AppointmentStatus WHERE nAppointmentStatusID = "+ _ApptStatusId +" ";
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
                    if (dt != null) { dt.Dispose(); dt = null; }
                    strQuery = null;
                }
            }

            public bool Delete(Int64 ID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                try
                {
                    oDB.Connect(false);
                    strQuery = "delete from AB_AppointmentStatus where nAppointmentStatusID =" + ID;
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
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;
                }
            }

            public System.Data.DataTable GetList()
            {
                System.Data.DataTable _result = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    //string _sqlQuery = "SELECT nAppointmentStatusID, sAppointmentStatus, nClinicID FROM AB_AppointmentStatus WHERE bIsBlocked =0";
                    string _sqlQuery = "SELECT nAppointmentStatusID, sAppointmentStatus, nClinicID,case ISNULL(bIsSystem,0)  when 0 then 'User' when 1 then 'System' end AS bIsSystem FROM AB_AppointmentStatus  WITH(NOLOCK) WHERE bIsBlocked =0 AND nClinicID = " + this.ClinicID + " ";
                    //
                    oDB.Retrive_Query(_sqlQuery, out  _result);
                    _sqlQuery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }

            public System.Data.DataTable GetBlockedAppStatus()
            {
                System.Data.DataTable _result = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    //string _sqlQuery = "SELECT nAppointmentStatusID, sAppointmentStatus, nClinicID FROM AB_AppointmentStatus WHERE bIsBlocked =0";
                    string _sqlQuery = "SELECT nAppointmentStatusID, sAppointmentStatus, nClinicID FROM AB_AppointmentStatus  WITH(NOLOCK) WHERE bIsBlocked =1 AND nClinicID = " + this.ClinicID + " ";
                    //
                    oDB.Retrive_Query(_sqlQuery, out  _result);
                    _sqlQuery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }

            //nAppointmentStatusID, sAppointmentStatus
            public bool IsExists(Int64 AppointmentStatusID, string AppointmentStatus)
            {
                bool _result = false;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "";
                    if (AppointmentStatusID == 0)
                    // For New AppointmentStatus
                    {
                        //_sqlQuery = "SELECT Count(nAppointmentStatusID) FROM AB_AppointmentStatus WHERE sAppointmentStatus ='" + AppointmentStatus + "'";
                        //
                        _sqlQuery = "SELECT Count(nAppointmentStatusID) FROM AB_AppointmentStatus  WITH(NOLOCK) WHERE sAppointmentStatus ='" + AppointmentStatus.Replace("'", "''") + "' AND nClinicID =" + this.ClinicID + " ";
                        //
                    }
                    else
                    // For AppointmentStatus to UpDate
                    {
                        _sqlQuery = "SELECT Count(nAppointmentStatusID) FROM AB_AppointmentStatus  WITH(NOLOCK) WHERE (sAppointmentStatus ='" + AppointmentStatus.Replace("'", "''") + "' AND nAppointmentStatusID <> " + AppointmentStatusID + ") AND nClinicID =" + this.ClinicID + " ";
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
                    _sqlQuery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }

            //public bool IsBlock(string Description)
            //{
            //    bool _result = false;

            //    return true;

            //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //    oDB.Connect(false);
            //    try
            //    {
            //        string _sqlQuery = "SELECT ID FROM TABLE WHERE FIELD"; //Check For Transaction Table
            //        object _intresult = null;
            //        _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
            //        if (_intresult != null)
            //        {
            //            if (_intresult.ToString().Trim() != "")
            //            {
            //                if (Convert.ToInt64(_intresult) > 0)
            //                {
            //                    _result = true;
            //                }
            //            }
            //        }
            //    }
            //    catch (gloDatabaseLayer.DBException DBErr)
            //    {
            //        DBErr.ERROR_Log(DBErr.ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //    }
            //    finally
            //    {
            //        oDB.Disconnect();
            //        oDB.Dispose();
            //    }
            //    return _result;
            //}

            public bool CanDelete(Int64 AppointmentStatusID)
            {
                bool Result = true;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                //DataTable dt = new DataTable();
                object value = new object(); 
                try
                {
                    oDB.Connect(false);

                    string SqlQuery = "";
                    SqlQuery = "SELECT ISNULL(bIsSystem,'false') AS bIsSystem FROM AB_AppointmentStatus   WITH(NOLOCK) WHERE nAppointmentStatusID = " + AppointmentStatusID;
                    value = oDB.ExecuteScalar_Query(SqlQuery);
                    if (value != null && Convert.ToString(value) != "")
                    {
                        if (Convert.ToBoolean(value) == true)
                        {
                            Result = false; 
                        }
                    }
                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Dispose();
                    value = null;
                }
                return Result;
            } 

            #endregion

            internal DataTable GetAppointmentStatus(long _AppStatusID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dt = null;
                try
                {
                    oDB.Connect(false);
                    String strQuery = "";
                    strQuery = "SELECT ISNULL(sAppointmentStatus,'') as sAppointmentStatus, ISNULL(bIsSystem,'false') AS bIsSystem  FROM AB_AppointmentStatus WHERE nAppointmentStatusID = " + _AppStatusID.ToString();
                    oDB.Retrive_Query(strQuery.ToString(), out  dt);
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;
                }
                catch (gloDatabaseLayer.DBException dbErr)
                {
                    dbErr.ERROR_Log(dbErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return dt;
            }
        }
    }
}
