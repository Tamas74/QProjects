using System;
using System.Collections.Generic;
using System.Text;

namespace gloAppointmentBook
{
    namespace Books
    {
        /// <summary>
        /// Anil 20070103
        /// </summary>
        public class ClinicDays : IDisposable
        {
            #region " Declarations "

            private string _databaseconnectionstring = "";
            private string _MessageBoxCaption = string.Empty;
            private Int64 _DayID = 0;
            private string _DayName = "";
            private int _DayCategory = 0;
            
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private Int64 _ClinicID = 0;
            
            //

            #endregion " Declarations "

            #region "Constructor & Destructor"

            public ClinicDays(string DatabaseConnectionString)
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

            ~ClinicDays()
            {
                Dispose(false);
            }

            #endregion

            #region "Property Procedures"

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

            public Int64 DayID
            {
                get { return _DayID; }
                set { _DayID = value; }
            }

            public string DayName
            {
                get { return _DayName; }
                set { _DayName = value; }
            }


            public int Category
            {
                get { return _DayCategory; }
                set { _DayCategory = value; }
            }

            #endregion

            #region Public & Private Methods 

            //Get data from database table
            public System.Data.DataTable GetList()
            {
                System.Data.DataTable dt = null;
                gloDatabaseLayer.DBLayer odb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    odb.Connect(false);
                    string _strquery = "";
                    _strquery = "SELECT nDayID, sDayName, nCategory FROM AB_ClinicDay_MST WITH(NOLOCK) ";
                    odb.Retrive_Query(_strquery, out dt);
                    _strquery = null;
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
                    odb.Disconnect();
                    odb.Dispose();
                }
                return dt;
            }

            //Update data in database table
            public int Modify(Int64 ID, string strDay, Int32 DayCategory)
            {
                gloDatabaseLayer.DBLayer odb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    odb.Connect(false);
                    string _strquery = "";
                    _strquery = "UPDATE AB_ClinicDay_MST SET sDayName ='" + strDay + "', nCategory =" + DayCategory + " where nDayID=" + ID + "";
                    odb.Execute_Query(_strquery);
                    _strquery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                    return 0;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return 0;
                }
                finally
                {
                    odb.Disconnect();
                    odb.Dispose();
                }
                return 1;
            } 

            #endregion

        }    

    }
}
