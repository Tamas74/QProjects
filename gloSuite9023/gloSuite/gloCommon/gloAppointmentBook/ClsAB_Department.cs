using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace gloAppointmentBook
{
    namespace Books
    {
        public class Department : IDisposable
        {
            #region " Declarations "

            private string _databaseconnectionstring = "";
            private Int64 _DepartmentID = 0;
            private string _Department = "";
            private Int64 _LocationID = 0;
            private string _Location = "";
            private bool _IsBlocked = false;
            private Int64 _ClinicID = 0;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private string _MessageBoxCaption = string.Empty;

            #endregion " Declarations "

            #region "Constructor & Distructor"

            //public Department()
            //{
            //}

            public Department(string DatabaseConnectionString)
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

            ~Department()
            {
                Dispose(false);
            }

            #endregion

            #region "Property Procedures"

            public Int64 DepartmentID
            {
                get { return _DepartmentID; }
                set { _DepartmentID = value; }
            }

            public string DepartmentName
            {
                get { return _Department; }
                set { _Department = value; }
            }

            public Int64 LocationID
            {
                get { return _LocationID; }
                set { _LocationID = value; }
            }

            public string Location
            {
                get { return _Location; }
                set { _Location = value; }
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
                    //@DepartmentID, @Department, @LocationID, @IsBlocked, @ClinicID
                    oDBParameters.Add("@DepartmentID", _DepartmentID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@Department", _Department, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@LocationID", _LocationID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@IsBlocked", _IsBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                     oDB.Execute("AB_INUP_Department", oDBParameters, out _intresult);

                    if (_intresult != null)
                    {
                        if (_intresult.ToString().Trim() != "")
                        {
                            if (Convert.ToInt64(_intresult) > 0)
                            {
                                _result = Convert.ToInt64(_intresult);
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
                    //@DepartmentID, @Department, @LocationID, @IsBlocked, @ClinicID
                    oDBParameters.Add("@DepartmentID", _DepartmentID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@Department", _Department, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@LocationID", _LocationID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@IsBlocked", _IsBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                    _intresult = oDB.Execute("AB_INUP_Department", oDBParameters);
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
                    string _sqlQuery = "UPDATE AB_Department SET bIsBlocked = 1 WHERE nDepartmentID =" + _DepartmentID;
                    int _intresult = 0;
                    _intresult = oDB.Execute_Query(_sqlQuery);
                    if (_intresult > 0)
                    {
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
                    strQuery = " UPDATE AB_Department SET bIsBlocked = '" + false + "' WHERE nDepartmentID = " + ID + " ";
                    int _result = oDB.Execute_Query(strQuery);
                    strQuery = null;
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
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            public bool DeleteAll()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                try
                {
                    oDB.Connect(false);
                    strQuery = " DELETE FROM AB_Department WHERE bIsBlocked = '" + true + "' AND nClinicID = "+this.ClinicID+" ";
                    int _result = oDB.Execute_Query(strQuery);
                    strQuery = null;
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
                }
            }

            public bool Delete(Int64 ID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                try
                {
                    oDB.Connect(false);
                    strQuery = "delete from AB_Department where nDepartmentID =" + ID;
                    int result = oDB.Execute_Query(strQuery);
                    strQuery = null;
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
                }
            }

            public System.Data.DataTable GetList()
            {
                System.Data.DataTable _result = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    //string _sqlQuery = "SELECT AB_Department.nDepartmentID AS nDepartmentID, AB_Department.sDepartment AS sDepartment , AB_Department.nLocationID AS nLocationID , AB_Location.sLocation AS sLocation , AB_Department.nClinicID AS nClinicID  " +
                    //" FROM AB_Department INNER JOIN AB_Location ON AB_Department.nLocationID = AB_Location.nLocationID WHERE (AB_Department.bIsBlocked = 0)";

                    string _sqlQuery = "SELECT AB_Department.nDepartmentID AS nDepartmentID, AB_Department.sDepartment AS sDepartment , AB_Department.nLocationID AS nLocationID , AB_Location.sLocation AS sLocation , AB_Department.nClinicID AS nClinicID  " +
                    " FROM AB_Department WITH(NOLOCK) INNER JOIN AB_Location WITH(NOLOCK) ON AB_Department.nLocationID = AB_Location.nLocationID WHERE (AB_Department.bIsBlocked = 0) AND AB_Department.nClinicID = " + this.ClinicID + "";

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

            public System.Data.DataTable GetBlockedDepartments()
            {
                System.Data.DataTable _result = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    //string _sqlQuery = "SELECT AB_Department.nDepartmentID AS nDepartmentID, AB_Department.sDepartment AS sDepartment , AB_Department.nLocationID AS nLocationID , AB_Location.sLocation AS sLocation , AB_Department.nClinicID AS nClinicID  " +
                    //" FROM AB_Department INNER JOIN AB_Location ON AB_Department.nLocationID = AB_Location.nLocationID WHERE (AB_Department.bIsBlocked = 0)";

                    string _sqlQuery = "SELECT AB_Department.nDepartmentID AS nDepartmentID, AB_Department.sDepartment AS sDepartment , AB_Department.nLocationID AS nLocationID , AB_Location.sLocation AS sLocation , AB_Department.nClinicID AS nClinicID  " +
                    " FROM AB_Department  WITH(NOLOCK) INNER JOIN AB_Location  WITH(NOLOCK) ON AB_Department.nLocationID = AB_Location.nLocationID WHERE (AB_Department.bIsBlocked = 1) AND AB_Department.nClinicID = " + this.ClinicID + "";

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

            //nDepartmentID, sDepartment
            public bool IsExists(Int64 DeptID, string DepartmentName)
            {
                bool _result = false;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _sqlQuery = "";
                object _intresult = null;
                try
                {
                    if (DeptID == 0)
                    {
                        //_sqlQuery = "SELECT Count(nDepartmentID) FROM AB_Department WHERE sDepartment='" + DepartmentName + "'";
                        //
                        _sqlQuery = "SELECT Count(nDepartmentID) FROM AB_Department  WITH(NOLOCK) WHERE sDepartment='" + DepartmentName.Replace("'", "''") + "' AND (nClinicID=" + this.ClinicID + " AND nLocationID=" + _LocationID + ")";
                        //
                    }
                    else
                    {
                        //_sqlQuery = "SELECT Count(nDepartmentID) FROM AB_Department WHERE sDepartment='" + DepartmentName + "' AND nDepartmentID<>" + DeptID;
                        //
                        _sqlQuery = "SELECT Count(nDepartmentID) FROM AB_Department  WITH(NOLOCK) WHERE (sDepartment='" + DepartmentName.Replace("'", "''") + "' AND nDepartmentID<>" + DeptID + ") AND (nClinicID = " + this.ClinicID + " AND nLocationID=" + _LocationID + ") ";
                        //
                    }


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
                    _sqlQuery = null;
                    _intresult = null;
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

            //sarika 26th dec 07
            public System.Data.DataTable GetDeptsofLocation(Int64 LocationID)
            {
                System.Data.DataTable _result = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "SELECT AB_Department.nDepartmentID AS nDepartmentID, AB_Department.sDepartment AS sDepartment , AB_Department.nLocationID AS nLocationID , AB_Location.sLocation AS sLocation , AB_Department.nClinicID AS nClinicID  " +
                    " FROM AB_Department WITH(NOLOCK) INNER JOIN AB_Location  WITH(NOLOCK) ON AB_Department.nLocationID = AB_Location.nLocationID WHERE (AB_Department.bIsBlocked = 0) and AB_Department.nLocationID = " + LocationID + "";

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
            //--- 
            #endregion

        }
    }
}
