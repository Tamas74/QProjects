using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace gloAppointmentBook
{
    namespace Books
    {
        public class AppointmentBlockType : IDisposable
        {

            #region " Declarations "

            private string _databaseconnectionstring = "";
            private Int64 _AppointmentBlockTypeID = 0;
            private string _AppointmentBlockType = "";
            private bool _IsBlocked = false;
            private Int64 _ClinicID = 0;
            private string _MessageBoxCaption = "";

            #endregion " Declarations "


            #region "Constructor & Distructor"

            
            public AppointmentBlockType()
            {
                System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
                _databaseconnectionstring = appSettings["DatabaseConnectionString"].ToString();
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

            public AppointmentBlockType(string DatabaseConnectionString)
            {
                _databaseconnectionstring = DatabaseConnectionString;
                //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
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

            ~AppointmentBlockType()
            {
                Dispose(false);
            }

            #endregion


            #region "Property Procedures"
            

            public Int64 AppointmentBlockTypeID
            {
                get { return _AppointmentBlockTypeID; }
                set { _AppointmentBlockTypeID = value; }
            }

            public string AppointmentBlockTypeName
            {
                get { return _AppointmentBlockType; }
                set { _AppointmentBlockType = value; }
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
            #endregion


            #region " Private Methods "

            public Int64 Add()
            {
                Int64 _result = 0;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                try
                {
                    object _intresult = 0;
                    //@AppointmentBlockTypeID, @AppointmentBlockType, @IsBlocked, @ClinicID
                      
                    oDBParameters.Add("@AppointmentBlockTypeID", _AppointmentBlockTypeID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@AppointmentBlockType", _AppointmentBlockType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@IsBlocked", _IsBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                    oDB.Execute("AB_INUP_AppointmentBlockType", oDBParameters, out _intresult);

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

            public bool Modify()
            {
                bool _result = false;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                try
                {
                    int _intresult = 0;
                    //@AppointmentBlockTypeID, @AppointmentBlockType, @IsBlocked, @ClinicID
                    oDBParameters.Add("@AppointmentBlockTypeID", _AppointmentBlockTypeID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@AppointmentBlockType", _AppointmentBlockType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@IsBlocked", _IsBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                    _intresult = oDB.Execute("AB_INUP_AppointmentBlockType", oDBParameters);
                    if (_intresult > 0)
                    {
                        _result = true;
                    }
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), true);
                    DBErr = null;
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
                string _sqlQuery = string.Empty;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    _sqlQuery = "UPDATE  AB_AppointmentBlockType SET bIsBlocked = 1 WHERE nAppointmentBlockTypeID = " + _AppointmentBlockTypeID;
                    int _intresult = 0;
                    _intresult = oDB.Execute_Query(_sqlQuery);
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
                    _sqlQuery = null;
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
                    strQuery = "UPDATE  AB_AppointmentBlockType SET bIsBlocked = 0 WHERE nAppointmentBlockTypeID = " + ID +" ";
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

            public bool DeleteAll()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                DataTable dt = null;
                int _result = 0;
                try
                {
                    oDB.Connect(false);
                    strQuery = " SELECT nAppointmentBlockTypeID FROM AB_AppointmentBlockType WITH(NOLOCK) WHERE bIsBlocked = '" + true + "' AND nClinicID = " + this.ClinicID + " ";
                    oDB.Retrive_Query(strQuery, out dt);
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Int64 _AppointmentBlockTypeId = 0;
                            _AppointmentBlockTypeId = Convert.ToInt64(dt.Rows[i][0]);
                            if (CanDelete(_AppointmentBlockTypeId))
                            {
                                strQuery = "";
                                strQuery = " DELETE FROM AB_AppointmentBlockType WHERE nAppointmentBlockTypeID="+ _AppointmentBlockTypeId +" ";
                                _result = oDB.Execute_Query(strQuery);
                            }
                        }
                    }
                    
                    return Convert.ToBoolean(_result);

                }
                catch (gloDatabaseLayer.DBException dbEx)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx.ToString(), true);
                    dbEx = null;
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    dt.Dispose();
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
                    strQuery = "delete from AB_AppointmentBlockType where nAppointmentBlockTypeID =" + ID;
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

            public System.Data.DataTable GetList()
            {
                System.Data.DataTable _result = null;
                string _sqlQuery = string.Empty;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    //string _sqlQuery = "SELECT nAppointmentBlockTypeID, sAppointmentBlockType, nClinicID FROM AB_AppointmentBlockType WHERE bIsBlocked ='" + false + "'";
                    _sqlQuery = "SELECT nAppointmentBlockTypeID, sAppointmentBlockType, nClinicID FROM AB_AppointmentBlockType WITH(NOLOCK)  WHERE bIsBlocked ='" + false + "' AND nClinicID = " + this.ClinicID + " ";
                    
                    oDB.Retrive_Query(_sqlQuery, out _result);
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
                    _sqlQuery = null;
                }
                return _result;
            }

            public System.Data.DataTable GetBlockedAppBlockType()
            {
                System.Data.DataTable _result = null;
                string _sqlQuery = string.Empty;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    //string _sqlQuery = "SELECT nAppointmentBlockTypeID, sAppointmentBlockType, nClinicID FROM AB_AppointmentBlockType WHERE bIsBlocked ='" + false + "'";
                    _sqlQuery = "SELECT nAppointmentBlockTypeID, sAppointmentBlockType, nClinicID FROM AB_AppointmentBlockType WHERE bIsBlocked ='" + true + "' AND nClinicID = " + this.ClinicID + " ";

                    oDB.Retrive_Query(_sqlQuery, out _result);
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
                    _sqlQuery = null;
                }
                return _result;
            }

            public bool IsExists(Int64 appBlockTypeID, string appBlockType)
            {
                bool _result = false;
                string _sqlQuery = string.Empty;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    if (appBlockTypeID == 0)
                    // For New Check For Duplicate
                    {
                        //_sqlQuery = " Select Count(nAppointmentBlockTypeID) FROM AB_AppointmentBlockType  WHERE sAppointmentBlockType = '" + appBlockType + "'";
                        _sqlQuery = " Select Count(nAppointmentBlockTypeID) FROM AB_AppointmentBlockType   WITH(NOLOCK) WHERE (sAppointmentBlockType = '" + appBlockType.Replace("'", "''") + "' AND nClinicID = " + this.ClinicID + " )";
                    }
                    else
                    // For Upadte Check For Duplicate
                    {
                        //_sqlQuery = "Select Count(nAppointmentBlockTypeID) FROM AB_AppointmentBlockType  WHERE sAppointmentBlockType = '" + appBlockType + "' AND nAppointmentBlockTypeID <>" + appBlockTypeID;
                        _sqlQuery = "Select Count(nAppointmentBlockTypeID) FROM AB_AppointmentBlockType   WITH(NOLOCK) WHERE (sAppointmentBlockType = '" + appBlockType.Replace("'", "''") + "' AND nAppointmentBlockTypeID <>" + appBlockTypeID + ") AND nClinicID = " + this.ClinicID + " ";
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
                                // Duplicate Found
                                _result = true;
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
                    ex = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    _sqlQuery = null;
                }
                return _result;
            }

            //public bool IsBlock_NotUsed(string Description)
            //{
            //    bool _result = false;

            //    return true;

            //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //    oDB.Connect(false);
            //    try
            //    {
            //        string _sqlQuery = _sqlQuery = " Select nAppointmentBlockTypeID FROM AB_AppointmentBlockType  WHERE bIsBlocked = '" + true + "'"; ; //Check For Transaction Table
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

            public bool CanDelete(Int64 BlockTypeID)
            {
                bool Result = false;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dt = null;
                string SqlQuery = string.Empty;
                try
                {
                    oDB.Connect(false);

                    SqlQuery = "SELECT nScheduleID  FROM AS_Schedule_DTL  WITH(NOLOCK) WHERE nBlockTypeID = " + BlockTypeID;
                    oDB.Retrive_Query(SqlQuery, out dt);

                    if (dt.Rows.Count == 0)
                    {
                        Result = true;
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
                    ex = null; 
                }
                finally
                {
                    oDB.Dispose();
                    if (dt != null) { dt.Dispose(); dt = null; }
                    SqlQuery = null;
                }
                return Result;
            }

            #endregion " Private Methods "
        }
    }
}
