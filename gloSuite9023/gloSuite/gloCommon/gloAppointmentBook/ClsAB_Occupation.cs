using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace gloAppointmentBook
{
    namespace Books
    {
        public class Occupation : IDisposable
        {
            #region " Declarations "

            private Int64 _OccupationID = 0;
            private string _Occupation = "";
            private string _Employer = "";
            private string _PlaceofEmployment = "";
            private string _AddressLine1 = "";
            private string _AddressLine2 = "";
            private string  _City = "";
            private string  _State = "";
            private string  _ZIP = "";
            private string _County = "";
            private string _Phone = "";
            private string _Mobile = "";
            private string _Fax = "";
            private string _Email = "";
            private Int64 _ClinicID = 0;
            private string _Country = "";
            //private string _OccupationName = 0;

            private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private string _databaseconnectionstring = "";
            private string _MessageBoxCaption = string.Empty;
            #endregion " Declarations "

            #region "Constructor & Distructor"
            
            //public Occupation()
            //{
            //}

            public Occupation()
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

            ~Occupation()
            {
                Dispose(false);
            }

            #endregion

            #region "Property Procedures"

            public Int64 OccupationID
            {
                get { return _OccupationID; }
                set { _OccupationID = value; }
            }

            public string OccupationName
            {
                get { return _Occupation; }
                set { _Occupation = value; }
            }

            public string EmployerName
            {
                get { return _Employer; }
                set { _Employer = value; }
            }

            public string PlaceofEmployment
            {
                get { return _PlaceofEmployment; }
                set { _PlaceofEmployment = value; }
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
            public string Mobile
            {
                get { return _Mobile; }
                set { _Mobile = value; }
            }
            public string Phone
            {
                get { return _Phone; }
                set { _Phone = value; }
            }
            public string Email
            {
                get { return _Email; }
                set { _Email = value; }
            }
            public string Fax
            {
                get { return _Fax; }
                set { _Fax = value; }
            }
            
            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

            public string Country
            {
                get { return _Country; }
                set { _Country = value; }
            }
            //public string OccupationName
            //{
            //    get { return _OccupationName; }
            //    set { _OccupationName = value; }
            //}

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
                    oDBParameters.Add("@nOccupationID", _OccupationID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@sOccupation", _Occupation, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sEmployerName", _Employer, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sPlaceofEmployment", _PlaceofEmployment, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkAddress1", _AddressLine1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkAddress2", _AddressLine2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkCity", _City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkState", _State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkZip", _ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkCountry", _County, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkPhone", _Phone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkMobile", _Mobile, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkFax", _Fax, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkEmail", _Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sCountry", _Country, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    
                    oDB.Execute("AB_INUP_Occupation_MST", oDBParameters, out  _intresult);

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
                    //AB_INUP_Occupation
                    //@OccupationID, @Occupation, @IsBlocked, @ClinicID
                    oDBParameters.Add("@nOccupationID", _OccupationID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@sOccupation", _Occupation, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sEmployerName", _Employer, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sPlaceofEmployment", _PlaceofEmployment, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkAddress1", _AddressLine1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkAddress2", _AddressLine2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkCity", _City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkState", _State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkZip", _ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkCountry", _County, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkPhone", _Phone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkMobile", _Mobile, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkFax", _Fax, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sWorkEmail", _Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sCountry", _Country, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    _intresult = oDB.Execute("AB_INUP_Occupation_MST", oDBParameters);                    
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


            public bool DeleteAll()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                try
                {
                    oDB.Connect(false);
                    strQuery = " DELETE FROM AB_Occupation_MST WHERE nClinicID = " + this.ClinicID + " ";
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
                    strQuery = "delete from AB_Occupation_MST  where nOccupationID =" + ID + " and nClinicID = " + this.ClinicID + " ";
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

            public DataTable GetList()
            {
                System.Data.DataTable _result = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    //string _sqlQuery = "SELECT nOccupationID, sOccupation, nClinicID, bIsDefault FROM AB_Occupation WHERE bIsBlocked = 0 ";
                    string _sqlQuery = "SELECT nOccupationID,isnull(sEmployerName,'') as sEmployerName,isnull(sOccupation,'') as sOccupation,isnull(sPlaceofEmployment,'') as sPlaceofEmployment,isnull(sWorkAddress1,'') as sWorkAddress1,isnull(sWorkAddress2,'') as sWorkAddress2,isnull(sWorkCity,'') as sWorkCity,isnull(sWorkState,'') as sWorkState,isnull(sWorkZip,'') as sWorkZip,isnull(sWorkCountry,'') as sWorkCountry,isnull(sWorkPhone,'') as sWorkPhone,isnull(sWorkMobile,'') as sWorkMobile,isnull(sWorkFax,'') as sWorkFax,isnull(sWorkEmail,'') as sWorkEmail FROM AB_Occupation_MST WITH(NOLOCK) WHERE nClinicID = " + this.ClinicID + "";
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

            public string GetOccupationName(Int64 OccupationId)
            {
                DataTable dt = null;
                string _result = "";
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "SELECT sOccupation FROM AB_Occupation_MST  WITH(NOLOCK) WHERE nOccupationID = " + OccupationId + "";
                    oDB.Retrive_Query(_sqlQuery, out  dt);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            _result = Convert.ToString(dt.Rows[0]["sOccupation"]);
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

            //public Int64 GetOccupationID(string OccupationName)
            //{
            //    DataTable dt = new DataTable();
            //    string _result = "";
            //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //    oDB.Connect(false);
            //    try
            //    {
            //        string _sqlQuery = "SELECT nOccupationID FROM AB_Occupation_MST WHERE sOccupation = " + OccupationName + "";
            //        oDB.Retrive_Query(_sqlQuery, out  dt);
            //        if (dt != null)
            //        {
            //            if (dt.Rows.Count > 0)
            //            {
            //                _result = Convert.ToInt64(dt.Rows[0]["nOccupationID"]);
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

            public bool IsExists_Old(Int64 OccupationID, string sOccupation)
            {
                bool _result = false;
                string _sqlQuery = "";
                object _intresult = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    if (OccupationID == 0)
                    {
                        //_sqlQuery = "SELECT Count(nOccupationID) FROM AB_Occupation WHERE sOccupation = '" + sOccupation + "'";
                        _sqlQuery = "SELECT Count(nOccupationID) FROM AB_Occupation_MST  WITH(NOLOCK) WHERE (sOccupation = '" + sOccupation.Replace("'", "''") + "' AND nClinicID=" + this.ClinicID + ")";
                        //
                    }
                    else
                    {
                        //_sqlQuery = "SELECT Count(nOccupationID) FROM AB_Occupation WHERE sOccupation = '" + sOccupation + "' AND nOccupationID <> " + OccupationID;
                        //
                        _sqlQuery = "SELECT Count(nOccupationID) FROM AB_Occupation_MST  WITH(NOLOCK) WHERE (sOccupation = '" + sOccupation.Replace("'", "''") + "' AND nOccupationID <> " + OccupationID + ") AND nClinicID =" + this.ClinicID + " ";
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

            //MaheshB
            public bool IsExists(Int64 OccupationID, string sOccupation, string sEmployername)
            {
                bool _result = false;
                object _intresult = null;
                string _sqlQuery = "";

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    if (OccupationID == 0)
                    {
                        //_sqlQuery = "SELECT Count(nOccupationID) FROM AB_Occupation WHERE sOccupation = '" + sOccupation + "'";
                        _sqlQuery = "SELECT Count(nOccupationID) FROM AB_Occupation_MST  WITH(NOLOCK) WHERE (sOccupation = '" + sOccupation.Replace("'", "''") + "' AND nClinicID=" + this.ClinicID + " and sEmployername='" + sEmployername + "')";
                        //
                    }
                    else
                    {
                        //_sqlQuery = "SELECT Count(nOccupationID) FROM AB_Occupation WHERE sOccupation = '" + sOccupation + "' AND nOccupationID <> " + OccupationID;
                        //
                        _sqlQuery = "SELECT Count(nOccupationID) FROM AB_Occupation_MST  WITH(NOLOCK) WHERE (sOccupation = '" + sOccupation.Replace("'", "''") + "' AND nOccupationID <> " + OccupationID + ") AND nClinicID =" + this.ClinicID + " and sEmployername='" + sEmployername + "'";
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
                    _intresult = null;
                    _sqlQuery = null;
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
