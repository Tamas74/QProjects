using System;
using System.Collections.Generic;
using System.Text;

namespace gloSettings
{
    public static class AppSettings
    {
        static System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings; 

        public static Int64 ClinicID
        {

            get
            {
                Int64 _clinicID = 1;
                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _clinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _clinicID = 1; }
                }
                else
                { _clinicID = 1; }

                return _clinicID;
            }
        }

        public static Int64 UserID
        {
            get
            {
                Int64 _userID = 0;
                if (appSettings["UserID"] != null)
                {
                    if (appSettings["UserID"] != "")
                    { _userID = Convert.ToInt64(appSettings["UserID"]); }
                }
                else { _userID = 0; }

                return _userID;
            }
        }

        public static string UserName
        {
            get
            {
                string _userName = string.Empty;
                if (appSettings["UserName"] != null)
                {
                    if (appSettings["UserName"] != "")
                    { _userName = Convert.ToString(appSettings["UserName"]); }
                }
                else { _userName = string.Empty; }

                return _userName;
            }
        }

        public static string MessageBoxCaption
        {
            get
            {
                string _messageBoxCaption = string.Empty;
                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    { _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]); }
                    else
                    { _messageBoxCaption = "gloPM"; }
                }
                else
                { _messageBoxCaption = "gloPM"; ; }

                return _messageBoxCaption;
            }
        }

        public static string ConnectionStringPM
        {
            get
            {
                string _connectionString = string.Empty;
                if (appSettings["DataBaseConnectionString"] != null)
                {
                    if (appSettings["DataBaseConnectionString"] != "")
                    { _connectionString = Convert.ToString(appSettings["DataBaseConnectionString"]); }
                    else
                    { _connectionString = string.Empty; }
                }
                else
                { _connectionString = string.Empty; }

                return _connectionString;
            }
        }

        public static string ConnectionStringEMR
        {
            get
            {
                string _connectionString = string.Empty;
                if (appSettings["EMRConnectionString"] != null)
                {
                    if (appSettings["EMRConnectionString"] != "")
                    { _connectionString = Convert.ToString(appSettings["EMRConnectionString"]); }
                    else
                    { _connectionString = string.Empty; }
                }
                else
                { _connectionString = string.Empty; }

                return _connectionString;
            }
        }


        public static Boolean EmergencyAccess
        {
            get
            {
                Boolean _isEmergencyAccess = false;
                if (Convert.ToString(appSettings["BreakTheGlass"]) != "")
                {
                    _isEmergencyAccess = Convert.ToBoolean(appSettings["BreakTheGlass"]); 
                }
                return _isEmergencyAccess;
            }
        }
    }
}
