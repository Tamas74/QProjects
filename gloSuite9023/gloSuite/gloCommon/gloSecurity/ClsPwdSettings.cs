using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace gloSecurity
{
    public class ClsPwdSettings : IDisposable
    {
        #region Declarations
        
        private string _databaseConnectionString = "";        
        private int _capLetters;
        private int _letters;
        private int _digits;
        private int _specialChar;
        private int _minLength;
        private int _days;

        //Code added on 11/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //


        #endregion
        
        #region Properties
        public int CapLetters
        {
            get { return _capLetters; }
            set { _capLetters = value; }
        }

        public int Letters
        {
            get { return _letters; }
            set { _letters = value; }
        }

        public int Digits
        {
            get { return _digits; }
            set { _digits = value; }
        }

        public int SpecialChar
        {
            get { return _specialChar; }
            set { _specialChar = value; }
        }

        public int MinLength
        {
            get { return _minLength; }
            set { _minLength = value; }
        }

        public int NoOfDays
        {
            get { return _days; }
            set { _days = value; }
        }
        #endregion

        #region Contructor & Destructor

        public ClsPwdSettings()
            {
                
            }

            public ClsPwdSettings(string datbaseConnectionString)
            {
                _databaseConnectionString = datbaseConnectionString;
                
                
                //Code added on 11/04/2008 -by Sagar Ghodke for implementing ClinicID;
                _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                //

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

         ~ClsPwdSettings()
            {
                Dispose(false);
            }
        
        #endregion

        /// <summary>
        /// This method gets a password setting from the database
        /// </summary>
        /// <returns></returns>
        internal DataTable getSettings()
            {
                DataTable dtSetting = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    // Changes for ClinicID implementation
                    //
                    //String strQuery = "SELECT * FROM PwdSettings";
                      //String strQuery = "SELECT * FROM PwdSettings where nClinicID = "+ ClinicID +" ";
                    String strQuery = "SELECT ExpCapitalLetters, ExpNoOfLetters, ExpNoOfDigits, ExpNoOfSpecChars, ExpPwdLength, ExpTimeFrameinDays, nClinicID FROM PwdSettings where nClinicID = " + ClinicID + " ";

                    //
                    oDB.Retrive_Query(strQuery, out dtSetting);
                    strQuery = null;
                    return dtSetting;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                    DBErr = null;
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;
                    return null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

        /// <summary>
        /// this method save modified password settings to database
        /// </summary>
        /// <returns></returns>
            internal bool SaveSettings()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                try
                {
                    oDB.Connect(false);

                    oDBParameters.Add("@CapLetters", this.CapLetters, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    oDBParameters.Add("@Letters", this.Letters, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    oDBParameters.Add("@Digits", this.Digits, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    oDBParameters.Add("@SpecialChar", this.SpecialChar, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    oDBParameters.Add("@MinLength", this.MinLength, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    oDBParameters.Add("@NoOfDays", this.NoOfDays, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    //
                    //
                    oDBParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    //
                                        
                    oDB.Execute("gsp_InUpPwdSettings", oDBParameters);
                    return true;

                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                    DBErr = null;
                    return false;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;
                    return false;
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
