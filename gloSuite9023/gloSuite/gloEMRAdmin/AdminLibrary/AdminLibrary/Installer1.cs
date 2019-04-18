using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data.SqlClient;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;

using System.Diagnostics;
using Microsoft.Win32;
using System.Data;

namespace AdminLibrary
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        public string strDataBaseMode = "";
        public string strPrerequistePath = "";
        public string serverName = "";
        public string dataBase = "";
        public DataBaseInfo form = new DataBaseInfo();
        public Installer1()
        {
            InitializeComponent();

            this.AfterInstall += new InstallEventHandler(Installer1_AfterInstall);
        }
        void Installer1_AfterInstall(object sender, InstallEventArgs e)
        {
            try
            {
                
                strDataBaseMode = form.ExistingDataBase;
                string strIsreplication = form.IsReplication;
                if (strDataBaseMode.ToLower() == "true" && strIsreplication.ToLower() == "false")
                {
                    RunSqlScripts(strPrerequistePath, serverName, dataBase);
                }
                if (strDataBaseMode.ToLower() == "true")
                {
                    object obArchiveDatabaseName = GetRegistryValue("ArchiveDatabase");
                    if (obArchiveDatabaseName != null && obArchiveDatabaseName.ToString() != "")
                    {
                        
                        RunArchiveDBChange(strPrerequistePath, serverName, Convert.ToString(obArchiveDatabaseName));
                    }
                   

                }
                UpdateSettingsAfterInstall();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        public object GetRegistryValue(string strValue)
        {


            RegistryKey oKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\gloEMR", true);

            object value;
            value = oKey.GetValue(strValue);
            return value;
        }
        public void UpdateSettingsAfterInstall()//Existing MAhine
        {

            // select count(*) from ClinicUpdates_Settings where  sProductCode = '1' and sProductVersion ='"+ svClientVersion +"' and sProductDBVersion ='"+ svCurrentVersion +"'
            // Update ClinicUpdates_Settings set sUpdateStatus ='Install',dtInstallDate= '"+ svDate +"' where  sProductCode = '1' and sProductVersion ='"+ svClientVersion +"' and sProductDBVersion ='"+ svCurrentVersion +"'

            string strComputerName = System.Environment.MachineName;
            string strConnection = "";
            string strQuery = "";
            object ServerName = GetRegistryValue("sqlserver");
            object strDataBaseName = GetRegistryValue("Database");
            if (ServerName != null && strDataBaseName != null)
            {
                strQuery = "USE [" + strDataBaseName + "] select * from ClinicUpdates_Settings where  sProductCode = '1' and sProductVersion ='5.0.1' and sProductDBVersion ='5.0.1'";
                bool isPresentServer = GetCount(Convert.ToString(strDataBaseName), Convert.ToString(ServerName), strQuery);

                if (isPresentServer)
                {
                    strConnection = "Data Source='" + ServerName + "'; Integrated Security=True";
                    // Update ClinicUpdates_Settings set sUpdateStatus ='Install',dtInstallDate= '"+ svDate +"' where  sProductCode = '1' and sProductVersion ='"+ svClientVersion +"' and sProductDBVersion ='"+ svCurrentVersion +"'

                    strQuery = "USE [" + strDataBaseName + "] Update ClinicUpdates_Settings set sUpdateStatus ='Installed',dtInstallDate= getdate() where  sProductCode = '1' and sProductVersion ='5.0.1' and sProductDBVersion ='5.0.1'";

                    SqlConnection MyCon = new SqlConnection(strConnection);

                    //Define the Command Object
                    SqlCommand MyCmd = new SqlCommand(strQuery, MyCon);
                    try
                    {
                        //Open the Connection
                        MyCon.Open();
                        MyCmd.CommandTimeout = 0;
                        if (MyCmd.ExecuteNonQuery() > 0)//Execute The Database Restore Script
                        { }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());//Showing Exception to User
                    }
                    finally
                    {
                        MyCon.Close(); //Closing the Connection
                    }
                }
            }
        }

        public bool GetCount(string strDataBaseName, string ServerName, string strStatement)
        {
            bool _success = false;
            string strComputerName = System.Environment.MachineName;
            string strConnection = "";
            string strQuery = "";

            strQuery = strStatement;
            strConnection = "Data Source='" + ServerName + "'; Integrated Security=True";

            SqlConnection MyCon = new SqlConnection(strConnection);
            DataTable dt = new DataTable();
            SqlCommand MyCmd = new SqlCommand(strQuery, MyCon);


            try
            {
                //Open the Connection
                MyCon.Open();
                SqlDataAdapter Myda = new SqlDataAdapter(MyCmd);
                Myda.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    _success = true;
                }
                else
                {
                    _success = false;
                }

                return _success;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());//Showing Exception to User
                _success = false;

                return _success;
            }
            finally
            {
                MyCon.Close(); //Closing the Connection
            }

        }
        public override void Install(System.Collections.IDictionary savedState)
        {
      
            string UserName = "";
            string Password = "";
            string strgloDataPath = "";
            string strAuthenticationMode = "";
          
            string strDataBaseMode = "";
            string strRegistryPath = "";

            form.ShowDialog();

            //////////////////////////////////////// Collecting All Parameters required for installation
            if (form.ServerName != null && form.ServerName.ToString() != "")
            {
                serverName = form.ServerName;
            }
            if (form.Database != null && form.Database.ToString() != "")
            {
                dataBase = form.Database;

            }
            if (form.UserName != null && form.UserName.ToString() != "")
            {
                UserName = form.UserName;
            }
            if (form.Password != null && form.Password.ToString() != "")
            {
                Password = form.Password;
            }
            if (form.PrerequisitePath != null && form.PrerequisitePath.ToString() != "")
            {
                strPrerequistePath = form.PrerequisitePath.ToString();
                strRegistryPath = form.PrerequisitePath.ToString();

            }
            if (form.gloDataPath != null && form.gloDataPath.ToString() != "")
            {
                strgloDataPath = form.gloDataPath.ToString();
            }
            while (strgloDataPath.EndsWith("\\"))
            { strgloDataPath = strgloDataPath.TrimEnd('\\'); }

            strgloDataPath += "\\gloData";
            if (!Directory.Exists(strgloDataPath))
                Directory.CreateDirectory(strgloDataPath);

            if (strPrerequistePath.ToLower().EndsWith("prerequisites"))
            {
                strPrerequistePath += "\\DataFiles";

            }
            else
            {
                strPrerequistePath += "\\Prerequisites\\DataFiles";
            }
            if (form.CheckDir(strPrerequistePath))
            {
                CopyData(strPrerequistePath, strgloDataPath);//copying prerequisite from the glodatapath

                strDataBaseMode = form.ExistingDataBase;
                if (strDataBaseMode.ToLower() == "true")
                {

                }
                else
                {
                    if (CheckRegistryExists())
                    {
                    }
                    else
                    {
                        strAuthenticationMode = form.Windows;
                        if (strAuthenticationMode.ToLower() == "true")
                        {
                            strAuthenticationMode = "true";
                        }
                        strAuthenticationMode = form.Sql;
                        if (strAuthenticationMode.ToLower() == "true")
                        {
                            strAuthenticationMode = "true";
                        }
                        string strBackFilePath = strgloDataPath + "\\BackUp"; //Database Bak File Path
                        strBackFilePath += "\\RTM2BlankDB" + ".bak";

                        string strDataFilePath = strgloDataPath + "\\Data\\";//MDF Path
                        strDataFilePath += dataBase.ToString();

                        string strLdfPath = strgloDataPath + "\\Data\\"; //LDF Path
                        strLdfPath += dataBase.ToString();
                        DataBaseResotore(dataBase.ToString(), strBackFilePath, serverName.ToString(), UserName.ToString(), Password.ToString(), strDataFilePath.ToString(), strLdfPath.ToString());

                        string strArchiveFileapth = strgloDataPath + "\\BackUp"; //Database Bak File Path
                        strArchiveFileapth += "\\gloEMRRTM2_archive" + ".bak";

                        string strArchiveMDFPath = strgloDataPath + "\\Data\\";//MDF Path
                        strArchiveMDFPath += dataBase.ToString() + "_Archive";

                        string strArchiveLdfPath = strgloDataPath + "\\Data\\"; //LDF Path
                        strArchiveLdfPath += dataBase.ToString() + "_Archive";

                        DataBaseResotore(dataBase.ToString() + "_Archive", strArchiveFileapth, serverName.ToString(), UserName.ToString(), Password.ToString(), strArchiveMDFPath.ToString(), strArchiveLdfPath.ToString());
                        string strgloServicePath = strgloDataPath + "\\BackUp";
                        strgloServicePath += "\\gloservices" + ".bak";
                        string strMDFService = strgloDataPath + "\\Data\\";
                        strMDFService += "gloservices";
                        string strLdfService = strgloDataPath + "\\Data\\";//LDF Path
                        strLdfService += "gloservices";
                        DataBaseResotore("gloservices", strgloServicePath, serverName.ToString(), UserName.ToString(), Password.ToString(), strMDFService.ToString(), strLdfService.ToString());
                        SetRegistry(serverName, dataBase, UserName, Password, strAuthenticationMode, strgloDataPath, strRegistryPath, dataBase.ToString() + "_Archive");

                    }

                }
            }
            else
            {
       
                        MessageBox.Show("Plese copy 'prerequisites' folder to Server Path. i.e \\\\"+strgloDataPath+"");
                        return;
                   
            }
            
           
            UninstallgloEMRAdmin();


            /////////////////////////////////////////////////

            //CopyData(strPrerequistePath,strgloDataPath);//copying prerequisite from the glodatapath

            ///////////////////////////////////////////////// Feature



            ///////////////////////////////////////////////// Restoring CodeWizard DataBase

            string strCodeWizardPath = strgloDataPath + "\\BackUp"; //Database Bak File Path
            strCodeWizardPath += "\\CodeWizard" + ".bak";

            string strDataCode = strgloDataPath + "\\Data\\";//MDF Path
            strDataCode += "CodeWizard";

            string strLdfCodePath = strgloDataPath + "\\Data\\";//LDF Path
            strLdfCodePath += "CodeWizard";

            DataBaseResotore("CodeWizard", strCodeWizardPath, serverName.ToString(), UserName.ToString(), Password.ToString(), strDataCode.ToString(), strLdfCodePath.ToString());

            /////////////////////////////////////////////////

            ///////////////////////////////////////////////// Restoring gloServices DataBase



            
            /////////////////////////////////////////////////

            ////////////////////////////////////////////////// Registry Settings for Fresh Installation.

           

            /////////////////////////////////////////////////

            base.Install(savedState);

        }
        public void RunArchiveDBChange(string strPath, string strServerName, string strDatabase)
        {
            strPath += "\\DBChanges\\gloEMRRTM2 Archive DB.sql";
            string strCommand = "sqlcmd.exe -E -S " + strServerName + " -d " + strDatabase + " -i  \"" + strPath + "\"";
          
            ExecuteScriptCmd(strCommand);
        }
        public void RunLowerScript(string strPath, string strServerName, string strDatabase)
        {

            strPath += "\\DBChanges\\273 To RTM2 Script.sql";
            string strCommand = "sqlcmd.exe -E -S " + strServerName + " -d " + strDatabase + " -i  \"" + strPath + "\"";
            
            ExecuteScriptCmd(strCommand);
        }
        public void RunHotfixScript(string strPath, string strServerName, string strDatabase)
        {
            strPath += "\\DBChanges\\HF3 To RTM2.sql";
            string strCommand = "sqlcmd.exe -E -S " + strServerName + " -d " + strDatabase + " -i  \"" + strPath + "\"";
          
            ExecuteScriptCmd(strCommand);
        }
        public void RunTwoEightOneScript(string strPath, string strServerName, string strDatabase)
        {
            strPath += "\\DBChanges\\281 To RTM2 Script.sql";//change the name
            string strCommand = "sqlcmd.exe -E -S " + strServerName + " -d " + strDatabase + " -i  \"" + strPath + "\"";

            ExecuteScriptCmd(strCommand);
        }
        public void RunSqlScripts(string strPath, string strServerName, string strDatabase)
        {
            if (form.gloDatabaseVersion != null && form.gloDatabaseVersion.ToString() != "")
            {
                string strDBVersion = form.gloDatabaseVersion.ToString();
                if (strDBVersion.ToLower() == "4.2.7.3")
                {
                   
                    RunLowerScript(strPath, strServerName, strDatabase);
                }
                if (strDBVersion.ToLower() == "5.0.0.3")
                {
                  
                    RunHotfixScript(strPath, strServerName, strDatabase);
                }
                if (strDBVersion.ToLower() == "4.2.8.1")
                {
                    RunTwoEightOneScript(strPath, strServerName, strDatabase);
                    
                }
            }
        }
        public void ExecuteScriptCmd(string strSetupPath)
        {
            // create the ProcessStartInfo using "cmd" as the program to be run, and "/c " as the parameters.
            // Incidentally, /c tells cmd that we want it to execute the command that follows, and then exit.

            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + strSetupPath);
            // The following commands are needed to redirect the standard output. 

            //This means that it will be redirected to the Process.StandardOutput StreamReader.
            procStartInfo.RedirectStandardOutput = true;

            procStartInfo.UseShellExecute = false;

            // Do not create the black window.

            procStartInfo.CreateNoWindow = false;

            // Now we create a process, assign its ProcessStartInfo and start it
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            // proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
            proc.StartInfo = procStartInfo;
            proc.Start();
            // Get the output into a string
            string result = proc.StandardOutput.ReadToEnd();

            // Display the command output.

            // Console.WriteLine(result);



        }
        public bool CheckRegistryExists()
        {
            //1.Check Whether gloEMR Registry Key Exists or not
            //2.If it exists  Dont show the Screen
            //3.If it does not exist show the Custom Screen
            bool _success = true;
            RegistryKey oKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\gloEMR", true);
            if (oKey != null && oKey.ToString() != "")
            {
                _success = true;
            }
            else
            {
                _success = false;
            }
            return _success;

        }

        public void UninstallgloEMRAdmin()
        {
            //string strCommand = "REG Delete HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\UnInstall\\Installshield_{5AE656CB-F224-4D7F-9EC9-5543210FCCF3} /F";
            string strCommand="reg delete HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\UnInstall\\Installshield_{AB5CE44B-3768-4F40-9D29-116D11B8C0B9} /f";
            ExecutePrerequisites(strCommand);
            //Process p = new Process();
            //p.StartInfo.FileName = @"C:\UninstallgloEMRAdmin.bat";
            //p.StartInfo.CreateNoWindow = true;
            //p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //p.Start();
        }
        public void ExecutePrerequisites(string strPrerequisites)
        {
            System.Diagnostics.ProcessStartInfo startInfo;
            System.Diagnostics.Process pStart = new System.Diagnostics.Process();
            startInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c" + strPrerequisites);
            pStart.StartInfo = startInfo;

            pStart.StartInfo.CreateNoWindow = true;
            pStart.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            pStart.Start();            
            pStart.WaitForExit();
            pStart.Close();
        }
        

        #region DataBase Restore
        /// <summary>
        /// Function to Restore The DataBase at Clent Machine
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="filePath"></param>
        /// <param name="serverName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="dataFilePath"></param>
        /// <param name="logFilePath"></param>
        public void DataBaseResotore(String databaseName, String filePath, String serverName, String userName, String password, String dataFilePath, String logFilePath)
        {
            // MessageBox.Show(databaseName); 
            //MessageBox.Show(filePath);
            // MessageBox.Show(logFilePath);
            //Connect Timeout=10
            string strConnection = "";
            if (userName != null && userName.ToString() != "" && password != null && password.ToString() != "")
            {
                strConnection = "Data Source='" + serverName + "';User id='" + userName + "';password='" + password + "' ";

            }
            else
            {
                strConnection = "Data Source='" + serverName + "'; Integrated Security=True;";
            }
            //Create the Connection String with Given parameters


            //Query to restore the dataabse at Client machine
            string strQuery = "Use [master] " +
                            "IF (NOT EXISTS (SELECT Name " +
                            "FROM sys.databases where Name=N'" + databaseName + "')) " +
                             "begin " +
                            "CREATE DATABASE " + databaseName + " " +
                            "ON PRIMARY ( NAME = N'" + databaseName + "', FILENAME = N'" + dataFilePath + ".mdf' ,  " +
                           "SIZE = 10000KB ,FILEGROWTH = 10% ) LOG ON  " +
                           "( NAME = N'" + databaseName + "_log', FILENAME = N'" + logFilePath + "_log.ldf'  , SIZE = 10000KB , FILEGROWTH = 10%)  " +
                          "USE [master] " +
                          "RESTORE DATABASE " + databaseName + " FROM DISK = N'" + filePath + "' WITH replace " +
                          "end ";
            //string strQuery = "RESTORE DATABASE [" + databaseName + "] FROM   " +
            //                "DISK = N'" + filePath + "' WITH  FILE = 1,  MOVE N' AIICW _Data' TO N'" + dataFilePath + ".mdf',  " +
            //                 "MOVE N' AIICW _Log' TO N'" + logFilePath + "_log.ldf',  NOUNLOAD,  STATS = 10 ";

            //MessageBox.Show(strQuery);

            //string strQuery = "CREATE DATABASE [" + databaseName + "] ON PRIMARY " +
            //   "( NAME = N'" + databaseName + "', FILENAME = N'" + dataFilePath + ".mdf' , SIZE = 10000KB ,FILEGROWTH = 10% ) " +
            // "LOG ON ( NAME = N'" + databaseName + "_log', FILENAME = N'" + logFilePath + "_log.ldf' , SIZE = 10000KB , FILEGROWTH = 10%) " +
            // "USE [master] " +
            // "RESTORE DATABASE " + databaseName + " FROM DISK = N'" + filePath + "' WITH replace ";
            //Define Connection String Object
            SqlConnection MyCon = new SqlConnection(strConnection);

            //Define the Command Object
            SqlCommand MyCmd = new SqlCommand(strQuery, MyCon);
            try
            {
                //Open the Connection
                MyCon.Open();
                MyCmd.CommandTimeout = 0;
                if (MyCmd.ExecuteNonQuery() > 0)//Execute The Database Restore Script
                { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());//Showing Exception to User
            }
            finally
            {
                MyCon.Close(); //Closing the Connection
            }

        }
        #endregion

        #region RegistrySettings
        public void SetRegistry(string strServerName, string strDataBaseName, string strUserName, string strPassword, string strAuthentication, string glodataPath, string ServerPath,string strArchiveDBName)
        {
            //Registry Settings Requirement for Client 

            //1.DataBase Name
            //2.Sql Server Name
            //3.ISWINAUTHENTICATION
            //3.gloDataPath

            RegistryKey oKey = Registry.LocalMachine;
            CreateSubKey(oKey, "Software\\gloEMR");
            WriteValue(oKey, "Software\\gloEMR", "SQLServer", strServerName);
            WriteValue(oKey, "Software\\gloEMR", "Database", strDataBaseName);
            WriteValue(oKey, "Software\\gloEMR", "gloDataPath", glodataPath);
            WriteValue(oKey, "Software\\gloEMR", "UserName", strUserName);
            WriteValue(oKey, "Software\\gloEMR", "Password", strPassword);
            WriteValue(oKey, "Software\\gloEMR", "ISWINAUTHENTICATION", strAuthentication.ToUpper());
            WriteValue(oKey, "Software\\gloEMR", "ArchiveDatabase",strArchiveDBName);
           //WriteValue(oKey, "Software\\gloEMR", "ServerPath", ServerPath);

        }
        public bool WriteValue(RegistryKey ParentKey, string SubKey, string ValueName, object Value)
        {
            RegistryKey Key = default(RegistryKey);

            try
            {
                //Open the given subkey 
                Key = ParentKey.OpenSubKey(SubKey, true);

                if (Key == null)
                {
                    //when subkey doesn't exist create it 
                    Key = ParentKey.CreateSubKey(SubKey);
                }

                //Once we have a handle to the key, set the value(the data) 
                Key.SetValue(ValueName, Value);

                return true;
            }
            catch (Exception e)
            {
                return false;
                //MessageBox.Show(e.InnerException.ToString());
            }
        }

        public bool CreateSubKey(RegistryKey ParentKey, string SubKey)
        {
            try
            {
                //create the specified subkey 
                ParentKey.CreateSubKey(SubKey);
                return true;
            }
            catch (Exception e)
            {
                return false;
                //MessageBox.Show(e.InnerException.ToString());
            }
        }
        #endregion

        #region CopyPrerequisistes
        public void CopyFolder(string sourceFolder, string destFolder)
        {

            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            string[] files = Directory.GetFiles(sourceFolder);

            foreach (string file in files)//looping through files
            {

                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest, true);

            }
            string[] folders = Directory.GetDirectories(sourceFolder);

            foreach (string folder in folders)//looping through folders
            {

                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);//recursive function
            }
        }
        /// <summary>
        /// Copying Prerequisites and Batch Files to the Required Location.
        /// </summary>
        /// <param name="strPath"></param>
        public void CopyData(string strPath, string strgloDataPath)
        {
            //string strSource = strPath + @"Prereq";            

            string strSource = strPath;

            string strDestination = strgloDataPath;

            CopyFolder(strSource, strDestination);

            strSource = strgloDataPath + "\\BatchFiles\\" + "UnInstallgloEMRAdmin.bat";
            if (System.IO.File.Exists(strSource))
            {
                strDestination = "C:\\UnInstallgloEMRAdmin.bat";
                File.Copy(strSource, strDestination, true);
            }

        }
        #endregion




        #region DB Scripts
        /// <summary>
        /// Function to Read Sql Script
        /// </summary>
        /// <param name="strSQlPath"></param>
        /// <returns></returns>
        public string ReadSqlScript(string strSQlPath)
        {
            strSQlPath += @"DBScripts\RestoreScript.sql";
            string[] strScript = new string[100];
            string strDbUpdates = "";
            if (File.Exists(strSQlPath))
            {
                strScript = File.ReadAllLines(strSQlPath);
            }
            for (int i = 0; i < strScript.Length; i++)
            {
                strDbUpdates += strScript[i].ToString();
            }
            return strDbUpdates.ToString();
        }

        /// <summary>
        /// Function to Execute the .Sql File (DB Scripts File Execution)
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="dataBaseNameToPrepend"></param>
        /// <param name="strSqlPath"></param>
        public void ExecSql(String serverName, String userName, String password, string dataBaseNameToPrepend, string strSqlPath)
        {
            try
            {
                string sql = "";
                string strConnection = "Data Source='" + serverName + "';User id='" + userName + "';password='" + password + "' ";
                sql = ReadSqlScript(strSqlPath);
                sql = sql.Trim();
                string strUse = "";

                if (dataBaseNameToPrepend != null)
                {
                    if (dataBaseNameToPrepend.Trim().Length > 0)
                    {
                        // sql = "USE [" + dataBaseNameToPrepend.Trim() + "]\nGO\n" + sql;
                        // strUse = "Go";
                        //strUse = Environment.NewLine;
                        strUse += "use [" + dataBaseNameToPrepend.Trim() + "]";
                        // strUse+= Environment.NewLine;
                        //  strUse += "GO";
                        strUse += Environment.NewLine;
                        strUse += sql;
                    }
                }
                SqlConnection MyCon = new SqlConnection(strConnection);
                SqlCommand MyCmd = new SqlCommand(strUse, MyCon);
                MyCon.Open();

                if (MyCmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Successful");
                }
                MyCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
        }
        #endregion

    }
    
}