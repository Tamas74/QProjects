using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace gloAppointmentBook
{
    namespace Books
    {
        public class Location : IDisposable
        {
            #region " Declarations "

            private Int64 _LocationID = 0;
            private string _Location = "";
            private string _AddressLine1 = "";
            private string _AddressLine2 = "";
            private string  _City = "";
            private string  _State = "";
            private string  _ZIP = "";
            private string _County = "";
            private string _Country = "";
            private bool _IsBlocked = false;
            private Int64 _ClinicID = 0;
            private bool _IsDefault = false;
            private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private string _databaseconnectionstring = "";
            private string _MessageBoxCaption = string.Empty;
            private bool _bIsTurnOffReminders = false;
            #endregion " Declarations "

            #region "Constructor & Distructor"
            
            //public Location()
            //{
            //}

            public Location()
            {
                //_databaseconnectionstring = DatabaseConnectionString;
                _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
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

            ~Location()
            {
                Dispose(false);
            }

            #endregion

            #region "Property Procedures"

            public Int64 LocationID
            {
                get { return _LocationID; }
                set { _LocationID = value; }
            }

            public string LocationName
            {
                get { return _Location; }
                set { _Location = value; }
            }
            public string AddressLine1
            {
                get { return _AddressLine1; }
                set { _AddressLine1 = value; }
            }
            public string AddressLine2
            {
                get { return _AddressLine2; }
                set { _AddressLine2 = value; }
            }
            public string City
            {
                get { return _City ; }
                set { _City = value; }
            }
            public string State
            {
                get { return _State ; }
                set { _State = value; }
            }
            public string ZIP
            {
                get { return _ZIP; }
                set { _ZIP = value; }
            }
            public string County
            {
                get { return _County; }
                set { _County = value; }
            }
            public string Country
            {
                get { return _Country ; }
                set { _Country = value; }
            }
            public bool IsBlocked
            {
                get { return _IsBlocked; }
                set { _IsBlocked = value; }
            }

            public bool IsDefault
            {
                get { return _IsDefault; }
                set { _IsDefault = value; }
            }

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
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
                    //@AddressLine1,@AddressLine2,@City,@State,@ZIP,@County
                    oDBParameters.Add("@LocationID", _LocationID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@Location", _Location, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@AddressLine1", _AddressLine1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@AddressLine2", _AddressLine2 , System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@City", _City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@State", _State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@ZIP", _ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@County", _County, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@IsBlocked", _IsBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@IsDefault", _IsDefault, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@Country", _Country, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@bIsTurnOffReminders", _bIsTurnOffReminders, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);

                    if (_IsDefault == true)
                    {
                        //string sqlQuery = "UPDATE AB_Location SET bIsDefault = 0";
                        //
                        //
                        string sqlQuery = "UPDATE AB_Location SET bIsDefault = 0 WHERE nClinicID = " + this.ClinicID + "";
                        //
                        oDB.Execute_Query(sqlQuery);
                    }

                     oDB.Execute("AB_INUP_Location", oDBParameters, out  _intresult);

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
                    //AB_INUP_Location
                    //@LocationID, @Location, @IsBlocked, @ClinicID
                    oDBParameters.Add("@LocationID", _LocationID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@Location", _Location, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@AddressLine1", _AddressLine1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@AddressLine2", _AddressLine2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@City", _City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@State", _State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@ZIP", _ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@County", _County, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@IsBlocked", _IsBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@IsDefault", _IsDefault, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@Country", _Country, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@bIsTurnOffReminders", _bIsTurnOffReminders, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);

                    if (_IsDefault == true)
                    {
                        //string sqlQuery = "UPDATE AB_Location SET bIsDefault = 0";
                        //
                        //
                        string sqlQuery = "UPDATE AB_Location SET bIsDefault = 0 where nClinicID = "+this.ClinicID+"";
                        //
                        oDB.Execute_Query(sqlQuery);
                        sqlQuery = null;
                    }

                    _intresult = oDB.Execute("AB_INUP_Location", oDBParameters);
                    SetDefaultLocation();
                    _result = true; 
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

            private void SetDefaultLocation()
            {
                DataTable dtDefault = null;
                try
                {
                    //nLocationID, sLocation, nClinicID, bIsDefault
                    dtDefault = GetDefaultLocation();
                    if (dtDefault != null && dtDefault.Rows.Count > 0)
                    {
                        // Set The Default Location
                        appSettings["DefaultLocationID"] = Convert.ToString(dtDefault.Rows[0]["nLocationID"]);
                        appSettings["DefaultLocation"] = Convert.ToString(dtDefault.Rows[0]["sLocation"]);
                    }
                    else
                    {
                        appSettings["DefaultLocationID"] = "0";
                        appSettings["DefaultLocation"] = "";
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (dtDefault != null) { dtDefault.Dispose(); dtDefault = null; }
                }
            }

            public bool Block()
            {
                bool _result = false;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "UPDATE AB_Location SET bIsBlocked = 1 WHERE nLocationID =" + _LocationID;
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
                    strQuery = " UPDATE AB_Location SET bIsBlocked = '" + false + "' WHERE nLocationID = " + ID + " ";
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
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;
                }
            }

            public bool DeleteAll()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                try
                {
                    oDB.Connect(false);
                    strQuery = " DELETE FROM AB_Location WHERE bIsBlocked = '" + true + "' AND nClinicID = "+this.ClinicID+" ";
                    int _result = oDB.Execute_Query(strQuery);
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
                }
            }

            public bool Delete(Int64 ID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                try
                {
                    oDB.Connect(false);
                    strQuery = "delete from AB_Location where nLocationID =" + ID;
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


            public string GetLocationTransactionCount (Int64 locationId)
            {
                DataTable dt = null;
                string _result = "";
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "";
                    _sqlQuery = "select sum(nLocationID) as nLocationID from ( select count (distinct im_nLocationID) as nLocationID from im_mst  WITH(NOLOCK) where im_nLocationID = " + locationId + " union all select count (distinct nLocationID) as nLocationID from im_trn_dtl  WITH(NOLOCK) where nLocationID = " + locationId + " union all select count (distinct nLocationID)  as nLocationID from as_appointment_dtl  WITH(NOLOCK) where nLocationID = " + locationId + "  union all select count (distinct nLocationID) as nLocationID from  AS_Schedule_DTL  WITH(NOLOCK) where nLocationID = " + locationId + "  ) mytable";

                    oDB.Retrive_Query(_sqlQuery, out  dt);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            _result = Convert.ToString(dt.Rows[0]["nLocationID"]);
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
                    if (dt != null) { dt.Dispose(); dt = null; }
                }
                return _result;
            }

            public DataTable GetLocationList()
            {
                System.Data.DataTable _result = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

                try
                {
                    string _sqlQuery = "SELECT 0 as nLocationID, '<All Locations>' as sLocation  union SELECT nLocationID, sLocation FROM AB_Location  WITH(NOLOCK) WHERE bIsBlocked = 0 AND nClinicID = " + this.ClinicID + "";
                    oDB.Connect(false);
                    oDB.Retrive_Query(_sqlQuery, out  _result);
                    oDB.Disconnect();
                    _sqlQuery = null;
 
                    return _result;

                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return null;
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Dispose();
                        oDB = null;
                    }

                    if (_result != null)
                    {
                        _result.Dispose();
                        _result = null;
                    }
                }
            }


            public DataTable GetList()
            {
                System.Data.DataTable _result = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    //string _sqlQuery = "SELECT nLocationID, sLocation, nClinicID, bIsDefault FROM AB_Location WHERE bIsBlocked = 0 ";
                    string _sqlQuery = "SELECT nLocationID, sLocation,sAddressLine1,sAddressLine2,sCity,sState,sZIP,sCounty,nClinicID, bIsDefault FROM AB_Location  WITH(NOLOCK) WHERE bIsBlocked = 0 AND nClinicID = " + this.ClinicID + "";
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

            public DataTable GetBlockedLocations()
            {
                System.Data.DataTable _result = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    //string _sqlQuery = "SELECT nLocationID, sLocation, nClinicID, bIsDefault FROM AB_Location WHERE bIsBlocked = 0 ";
                    string _sqlQuery = "SELECT nLocationID, sLocation, nClinicID FROM AB_Location  WITH(NOLOCK) WHERE bIsBlocked = 1 AND nClinicID = " + this.ClinicID + "";
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

            public DataTable GetDefaultLocation()
            {
                DataTable _result = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    //string _sqlQuery = "SELECT nLocationID, sLocation, nClinicID, bIsDefault FROM AB_Location WHERE bIsDefault = 1";
                    string _sqlQuery = "SELECT nLocationID, sLocation, nClinicID, bIsDefault FROM AB_Location  WITH(NOLOCK) WHERE bIsDefault = 1 AND nClinicID = " + this.ClinicID + "";
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

            public string GetLocationName(Int64 locationId)
            {
                DataTable dt = null;
                string _result = "";
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "SELECT sLocation FROM AB_Location  WITH(NOLOCK) WHERE nLocationID = " + locationId + "";
                    oDB.Retrive_Query(_sqlQuery, out  dt);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            _result = Convert.ToString(dt.Rows[0]["sLocation"]);
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
                    if (dt != null) { dt.Dispose(); dt = null; }
                }
                return _result;
            }

            public bool IsExists(Int64 LocationID, string sLocation)
            {
                bool _result = false;
                string _sqlQuery = "";
                object _intresult = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    if (LocationID == 0)
                    {
                        //_sqlQuery = "SELECT Count(nLocationID) FROM AB_Location WHERE sLocation = '" + sLocation + "'";
                        _sqlQuery = "SELECT Count(nLocationID) FROM AB_Location  WITH(NOLOCK) WHERE (sLocation = '" + sLocation.Replace("'", "''") + "' AND nClinicID=" + this.ClinicID + ")";
                        //
                    }
                    else
                    {
                        //_sqlQuery = "SELECT Count(nLocationID) FROM AB_Location WHERE sLocation = '" + sLocation + "' AND nLocationID <> " + LocationID;
                        //
                        _sqlQuery = "SELECT Count(nLocationID) FROM AB_Location  WITH(NOLOCK) WHERE (sLocation = '" + sLocation.Replace("'", "''") + "' AND nLocationID <> " + LocationID + ") AND nClinicID =" + this.ClinicID + " ";
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
                    oDB.Disconnect();
                    oDB.Dispose();
                    _sqlQuery = null;
                    _intresult = null;
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

            #endregion
        }
    }
}
