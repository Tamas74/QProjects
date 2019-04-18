using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Management;
using System.IO;

namespace gloPM
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

            #region 'Application Instance and Terminal server  check'

            System.Diagnostics.Process processTemp = new System.Diagnostics.Process();

            //Getting currently active process.
            processTemp = System.Diagnostics.Process.GetCurrentProcess();

            //Storing the process MainModule's ModuleName.
            string gloMainModuleName = processTemp.MainModule.ModuleName;


            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationSettings.AppSettings;
            //Check if Application is running on terminal server
            if (GetOSInfo() == false)
            {

                //Getting the ProcessName by eliminating the extension.
                string gloProcessName = System.IO.Path.GetFileNameWithoutExtension(gloMainModuleName);

                //Check if application is already running
                //if yes then exit.
                //if (System.Diagnostics.Process.GetProcessesByName(gloProcessName).Length  > 1) 
                if (System.Diagnostics.Process.GetProcessesByName(gloProcessName).Length > 1)
                {
                    MessageBox.Show("Application instance already running.  ", Program.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                else
                {
                    //appSettings["StartupPath"] = CreateNewTempDirectory(false);
                    appSettings["StartupPath"] = Application.StartupPath;  
                    //Application.Run(new frmSplash());
                }
            }
            else
            {
                appSettings["StartupPath"] = CreateNewTempDirectory(true);
                //Application.Run(new frmSplash());
            }
            #endregion 'Application Instance check'

          
            
            //Application.Run(new Forms.frmMainMenu());
        }

        public static bool gWindowsAuthentication = false;
        public static string gSQLServerName = "";
        public static string gDatabase = "";
        public static string gLoginUser = "";
        public static string gLoginPassword = "";


        public static bool gEMRWindowsAuthentication = false;
        public static string gEMRSQLServerName = "";
        public static string gEMRDatabase = "";
        public static string gEMRLoginUser = "";
        public static string gEMRLoginPassword = "";

        public static Int64 gnPatientID = 0;
        public static string gstrPatientCode = "";
        public static string gstrPatientName = "";

        public static string gMessageBoxCaption = "gloPM";

        public static Boolean gblnShowReportDesigner = false;

        //
        public static string gstrApplicationVersion = "";
        public static string gstrDBVersion = "";
        public static string gstrApplicationDateTime = "";
        //

        public static Int64 gLockScreenTime = 10;
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

        #region "Tesminal Server methods"

        public static bool GetOSInfo()
        {
            bool bResult = false; 
            try
            {
                ManagementObjectSearcher objMOS = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                foreach (ManagementObject objMgmt in objMOS.Get())
                {
                    
                    string[] strOsName = Convert.ToString(objMgmt["Name"]).Split('|');
                    if (strOsName.Length > 0)
                    {
                        if (strOsName[0].Contains("Microsoft Windows Server"))
                        {
                            bResult = true; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                ex = null;
                bResult = false; 
            }
            return bResult;
        }

        public static string CreateNewTempDirectory(Boolean isTerminalServer)
        {
            string sNewTempDirectory = "";
            try
            {
                string _Path = Application.StartupPath + "\\Temp";
                if (Directory.Exists(_Path) == false)
                {
                    Directory.CreateDirectory(_Path);
                }

                if (isTerminalServer == true)
                {

                    string NewDirectoryName = "";
                    DateTime _dtCurrentDateTime = System.DateTime.Now;

                    int i = 0;
                    //NewDirectoryName = String.Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss fff");
                    NewDirectoryName = _dtCurrentDateTime.ToString("MM dd yyyy - hh mm ss fff"); 
                    while (Directory.Exists(_Path + "\\" + NewDirectoryName) == true)
                    {
                        i = i + 1;
                        //NewDirectoryName = String.Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss fff") + "_" + i;
                        NewDirectoryName = _dtCurrentDateTime.ToString("MM dd yyyy - hh mm ss fff") + "_" + i; 
                    }

                    sNewTempDirectory = _Path + "\\" + NewDirectoryName;
                    Directory.CreateDirectory(sNewTempDirectory);

                    sNewTempDirectory = sNewTempDirectory ;


                    string _PathTemp = sNewTempDirectory + "\\" + "Temp";
                    if (Directory.Exists(_PathTemp) == false)
                    {
                        Directory.CreateDirectory(_PathTemp);
                    }

                }
                else
                {
                    sNewTempDirectory = _Path + "\\";
                }
            }
            catch (IOException ioex)
            {
                ioex.ToString();
                ioex = null;
                sNewTempDirectory = "";
            }
            catch (Exception ex)
            {
                ex.ToString();
                ex = null;
                sNewTempDirectory = "";
            }



            return sNewTempDirectory;
        } 

        #endregion

    }
}