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




            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

            //code start by nilesh on 20110228 for case GLO2010-0008612
            CheckAnotherInstance();

            //Check if Application is running on terminal server
            if (GetOSInfo() == false)
            {
                gloSettings.gloRegistrySetting.IsServerOS = false;

                //appSettings["StartupPath"] = CreateNewTempDirectory(false);
                appSettings["StartupPath"] = Application.StartupPath;
               
                    Application.Run(new frmSplash());
               
                }
            else
            {
                gloSettings.gloRegistrySetting.IsServerOS = true;

                appSettings["StartupPath"] = CreateNewTempDirectory(true);
               
                    Application.Run(new frmSplash());
                
            }

            //code end by nilesh on 20110228 for case GLO2010-0008612

            //code comment start by nilesh on 20110228 for case GLO2010-0008612
            ////Check if Application is running on terminal server
            //if (GetOSInfo() == false)
            //{
            //    // added by sandip dhakane 20100722 for registrysetting changes for terminal server
            //    gloSettings.gloRegistrySetting.IsServerOS = false ; 
            //    // end

            //    //Getting the ProcessName by eliminating the extension.
            //    string gloProcessName = System.IO.Path.GetFileNameWithoutExtension(gloMainModuleName);

            //    //Check if application is already running
            //    //if yes then exit.
            //    //if (System.Diagnostics.Process.GetProcessesByName(gloProcessName).Length  > 1) 
            //    if (System.Diagnostics.Process.GetProcessesByName(gloProcessName).Length > 1)
            //    {
            //        MessageBox.Show("Application instance already running.  ", Program.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        Application.Exit();
            //    }
            //    else
            //    {
            //        //appSettings["StartupPath"] = CreateNewTempDirectory(false);
            //        appSettings["StartupPath"] = Application.StartupPath;  
            //        Application.Run(new frmSplash());
            //    }
            //}
            //else
            //{
            //    // added by sandip dhakane 20100722 for registrysetting changes for terminal server
            //    gloSettings.gloRegistrySetting.IsServerOS = true ; 
            //    // end

            //    appSettings["StartupPath"] = CreateNewTempDirectory(true);
            //    Application.Run(new frmSplash());
            //}
            //code comment end by nilesh on 20110228 for case GLO2010-0008612
            #endregion 'Application Instance check'

          
            
            //Application.Run(new Forms.frmMainMenu());
        }

        //code start by nilesh on 20110228 for case GLO2010-0008612
        //check process for another instance as per session
        private static void CheckAnotherInstance()
        {
            try
            {
                int _currentSessionID = System.Diagnostics.Process.GetCurrentProcess().SessionId;
                string _currentProcName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                int _currentProcessID = System.Diagnostics.Process.GetCurrentProcess().Id;

                System.Diagnostics.Process[] _Process = System.Diagnostics.Process.GetProcessesByName(_currentProcName);

                if (_Process.Length > 1)
                {
                    foreach (System.Diagnostics.Process _proc in _Process)
                    {
                        if (_proc.SessionId == _currentSessionID & _proc.Id != _currentProcessID & _proc.ProcessName == _currentProcName)
                        {
                            MessageBox.Show("Another instance of this application is already running.", Program.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Application.Exit();
                            System.Environment.Exit(0);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //code end by nilesh on 20110228 for case GLO2010-0008612

        

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

        private static string gMessageBoxCaption = "gloPM";
        public static string gRestrictedPatientMessage = "The status of the patient is 'Lock Charts'." + Environment.NewLine + "You can not perform any activity on this Patient.";

        public static Boolean gblnShowReportDesigner = false;

        //
        public static string gstrApplicationVersion = "";
        public static string gstrDBVersion = "";
        public static string gstrApplicationDateTime = "";
        //

        public static Int64 gLockScreenTime = 10;
        public static bool gblnAutoLockEnable = false;
        public static Int32 LockOutAttempts = 1;
        public static string gstrHelpProvider = "Client";
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
                        //CHANGE FOR 64 BIT WINDOWS SERVER 2008
                        //if (strOsName[0].Contains("Microsoft Windows Server"))
                        if (strOsName[0].Contains("Microsoft") && strOsName[0].Contains("Windows") && strOsName[0].Contains("Server"))
                        {
                            bResult = true; 
                        }
                    }
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
                bResult = false; 
            }
            return bResult;
        }

        public static string CreateNewTempDirectory(Boolean isTerminalServer)
        {
            string sNewTempDirectory = "";
            try
            {
                
                string _Path = gloSettings.FolderSettings.AppTempFolderPath;

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
                    //Bug #51064: 00000456 : Scheduling
                    //Description: Delete "\\" from following line as "\\" is allready added in _path at gloSettings.FolderSettings.AppTempFolderPath.
                    sNewTempDirectory = _Path + NewDirectoryName;
                    Directory.CreateDirectory(sNewTempDirectory);

                  //  sNewTempDirectory = sNewTempDirectory ;


                    string _PathTemp = sNewTempDirectory + "\\" + "Temp";
                    if (Directory.Exists(_PathTemp) == false)
                    {
                        Directory.CreateDirectory(_PathTemp);
                    }

                }
                else
                {
                    //Bug #51064: 00000456 : Scheduling
                    //Description: remove "\\" from _path because it added "\\\\" when used.
                    sNewTempDirectory = _Path.Remove(_Path.LastIndexOf("\\"));
                }
            }
            catch (IOException)// ioex)
            {
                //ioex.ToString();
                //ioex = null;
                sNewTempDirectory = "";
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                sNewTempDirectory = "";
            }



            return sNewTempDirectory;
        } 

        #endregion

    }
}