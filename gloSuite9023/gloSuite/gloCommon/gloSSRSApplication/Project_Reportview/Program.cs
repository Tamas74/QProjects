using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SSRSApplication;

namespace Project_Reportview
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmLogin());
            Application.Run(new frmLogin());
        }
       
        public static bool gWindowsAuthentication = false;
        public static string gSQLServerName = "";
        public static string gDatabase = "";
        public static string gLoginUser = "";
        public static string gLoginPassword = "";

        public static string gMessageBoxCaption = "gloPM";
        public static Int32 LockOutAttempts = 1;

        public static string GetConnectionString()
        {
            return GetConnectionString(gWindowsAuthentication, gSQLServerName, gDatabase, gLoginUser, gLoginPassword);
        }

        public static string GetConnectionString(string SQLServerName, string Database)
        {
            return GetConnectionString(true, SQLServerName, Database, "", "");
        }

        public static string GetConnectionString(bool WindowsAuthentication, string SQLServerName, string Database, string LoginUser, string LoginPassword)
        {
            string _connstring = "";
            try
            {
                if (WindowsAuthentication == false)//SQL authentication
                {
                    _connstring = "Server=" + SQLServerName + ";Database=" + Database + ";Uid=" + LoginUser + ";Pwd=" + LoginPassword + ";";
                }
                else//windows authentication
                {
                    _connstring = "Server=" + SQLServerName + ";Database=" + Database + ";Trusted_Connection=yes;";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return _connstring;
        }

    }
}
