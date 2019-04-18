using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace gloAppointmentBook
{
    namespace Books
    {
        public class gloResourceType : IDisposable
        {
            #region " Declarations "

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private string _databaseconnectionstring = "";
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            private Int64 _ClinicID = 0;
            private string _MessageBoxCaption = String.Empty;

            #endregion " Declarations "

            #region "Constructor & Distructor"
           

            //public Location()
            //{
            //}

            public gloResourceType()
            {
                //_databaseconnectionstring = DatabaseConnectionString;
                _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
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

            ~gloResourceType()
            {
                Dispose(false);
            }

            #endregion

            #region Methods

            public bool Block(Int64 ResourceTypeID)
            {
                bool Result = false;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDB.Connect(false);
                    oDB.Execute_Query("UPDATE AB_ResourceType_MST SET bitIsBlocked = '" + true + "' WHERE nResourceTypeID = " + ResourceTypeID + "");
                    oDB.Disconnect();
                    Result = true;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), true);
                }
                finally
                {
                    oDB.Dispose();
                }
                return Result;
            }

            public bool CanDelete(Int64 ResourceTypeID)
            {
                bool Result = false;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dt = null;
                string SqlQuery = "";
                try
                {
                    oDB.Connect(false);

                    SqlQuery = "SELECT nResourceID FROM AB_Resource_MST WITH(NOLOCK) WHERE nResourceTypeID = " + ResourceTypeID;
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
                catch (Exception dbex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(dbex.ToString(), true);
                }
                finally
                {
                    oDB.Dispose();
                    SqlQuery = null;
                    if (dt != null) { dt.Dispose(); dt = null; }
                }
                return Result;
            }

            public bool Delete(Int64 ID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strQuery = "";
                try
                {
                    oDB.Connect(false);
                    strQuery = "delete from AB_ResourceType_MST where nResourceTypeID =" + ID;
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

            public DataTable GetBlockedResourceTypes()
            {
                System.Data.DataTable _result = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    String strQuery = "SELECT nResourceTypeID, sResourceTypeDescription, nResourceType FROM AB_ResourceType_MST  WITH(NOLOCK) WHERE bitIsBlocked = 1 AND nClinicID = " + this._ClinicID + " ";
                    oDB.Retrive_Query(strQuery, out  _result);
                    strQuery = null;
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
                    strQuery = "UPDATE AB_ResourceType_MST SET bitIsBlocked = '" + false + "' WHERE nResourceTypeID = " + ID + " ";
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
                DataTable dt = null;
                int _result=0;
                try
                {
                    oDB.Connect(false);
                    strQuery = " SELECT nResourceTypeID from AB_ResourceType_MST  WITH(NOLOCK) WHERE bitIsBlocked = '" + true + "' AND nClinicID = " + _ClinicID + " ";
                    oDB.Retrive_Query(strQuery, out dt);
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //Check for if we can delete the Resource Type
                            //i.e its is not associated with some other entities or is not in use.
                            if( CanDelete(Convert.ToInt64(dt.Rows[i][0])))
                            {
                                strQuery = "";
                                strQuery = "DELETE FROM AB_ResourceType_MST WHERE nResourceTypeID = " + Convert.ToInt64(dt.Rows[i][0]);
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

            #endregion
        }
    }
}
