using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data.SqlClient;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Diagnostics;
using Microsoft.Win32;
using System.Data;

namespace SampleLibrary
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        private System.IO.StreamWriter oWriter = null;
        public Installer1()
        {
            InitializeComponent();

            this.AfterInstall += new InstallEventHandler(Installer1_AfterInstall);
        }
        void Installer1_AfterInstall(object sender, InstallEventArgs e)
        {
            try
            {
                UpdatesAfterInstall();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        public void UpdatesAfterInstall()//Existing MAhine
        {
            // select count(sMachineName) from ClientSettings_MST where  sMachineName = '" + svComputerName + "'

            string strComputerName = System.Environment.MachineName;
            string strConnection = "";
            string strQuery = "";
            object ServerName = GetRegistryValue("sqlserver");
            object strDataBaseName = GetRegistryValue("Database");
            if (ServerName != null && strDataBaseName != null)
            {
                strQuery = "USE [" + strDataBaseName + "] select * from ClientSettings_MST where  sMachineName = '" + strComputerName + "'";
                bool isPresentServer = GetCount(Convert.ToString(strDataBaseName), Convert.ToString(ServerName), strQuery);
               
                if (!isPresentServer)
                {
                    // select isnull(max(nClientID),0)+1 from ClientSettings_MST
                    // INSERT INTO ClientSettings_MST (nClientID, sMachineName, sCurrentProductVersion ,blnIsUpdated) VALUES("+ oRS.Fields(0).value +",'" + svComputerName + "','"+ szCurrentVersion +"','True'                    
                    strQuery = "USE [" + strDataBaseName + "] Insert into ClientSettings_MST (nClientID, sMachineName, nVoiceEnabled, nScanEnabled,sProductCode,sCurrentProductVersion,sLatestProductVersion,blnIsUpdated) "
                                + " select isnull(max(nClientID),0)+1,'" + strComputerName + "','" + Convert.ToBoolean(_typeVoice) + "','True',1,'5.0.1','5.0.1','True' from ClientSettings_MST ";
                    
                }
                else
                {
                    // Update ClientSettings_MST set sCurrentProductVersion ='"+ szCurrentVersion +"',blnIsUpdated= 'True' where sMachineName = '" + svComputerName + "'
                    strQuery = "USE [" + strDataBaseName + "] Update ClientSettings_MST set sCurrentProductVersion ='5.0.1',sLatestProductVersion ='5.0.1',dtUpdateDate = getdate(), blnIsUpdated= 'True' where sMachineName = '" + strComputerName + "'";
                }
                strConnection = "Data Source='" + ServerName + "'; Integrated Security=True";


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
            else
            {
                MessageBox.Show("Registry is missing on this machine.");
            }
        }
        int _typeVoice = 0;
        public override void Install(System.Collections.IDictionary savedState)
        {   
            
            DataBaseInfo form = new DataBaseInfo();
            DataPathInfo frmDatapath = new DataPathInfo();
            string serverName = "";
            string dataBase = "";
            string UserName = "";
            string Password = "";
            string strVoice = "";
            string strAuthenticationMode = "";
            string strgloDataPath = "";
            string strServerPath = "";

            //////////////////////////////////////// Collecting All Parameters required for installation


            string strBatchFilePath = "C:\\InstallEMRPrereq.bat";
            oWriter = File.CreateText(strBatchFilePath);
            oWriter.WriteLine("cd\\");

            if (CheckRegistryExists() && RegistryExists("Serverpath"))//Checking whether gloEMR Registry Key Exists or not
            {
                //frmDatapath.ShowDialog();
                //object obgloDatapath = GetRegistryValue("Serverpath");
                //if (obgloDatapath.ToString().ToLower().EndsWith("prerequisites"))
                //{
                //    strgloDataPath = obgloDatapath.ToString();
                //}
                //else
                //{
                //    while (strgloDataPath.EndsWith("\\"))
                //    { strgloDataPath = strgloDataPath.TrimEnd('\\'); }
                //    strgloDataPath = obgloDatapath + "\\Prerequisites";
                //}
                
               

                
                    frmDatapath.ShowDialog();
                    if (frmDatapath.gloDataPathInfo != null && frmDatapath.gloDataPathInfo.ToString() != "")
                    {
                        strgloDataPath = frmDatapath.gloDataPathInfo.ToString();
                        if (strgloDataPath.ToLower().EndsWith("prerequisites"))
                        {
                            strgloDataPath = frmDatapath.gloDataPathInfo.ToString();
                        }
                        else
                        {
                            strgloDataPath = frmDatapath.gloDataPathInfo.ToString() + "\\Prerequisites";
                        }
                    }
                    while (strgloDataPath.EndsWith("\\"))
                    { strgloDataPath = strgloDataPath.TrimEnd('\\'); }
                    if (frmDatapath.CheckDir(strgloDataPath))
                    {
                        InstallEasyHL7(strgloDataPath);
                        if (frmDatapath.gloNonVoiceInfo != null && frmDatapath.gloNonVoiceInfo.ToString() != "")
                        {
                            strVoice = frmDatapath.gloNonVoiceInfo;
                        }
                        if (frmDatapath.gloVoiceInfo != null && frmDatapath.gloVoiceInfo.ToString() != "")
                        {
                            strVoice = frmDatapath.gloVoiceInfo;
                        }
                        if (strVoice.ToLower() == "true")//Non Voice Installation
                        {

                            InstallgloVoice(strgloDataPath);
                        }
                        else
                        {

                            InstallgloNonVoice(strgloDataPath);
                        }
                        UninstallgloEMRClient(strgloDataPath);
                    }
                    else
                    {
                        MessageBox.Show("Plese copy 'prerequisites' folder to Server Path. i.e \\\\"+strgloDataPath+"glodata\\prerequisites");
                        return;
                    }
                   
               
                
                
      

            }//Fresh Installation
            else
            {
                form.ShowDialog();
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
                if (form.gloDataPath != null && form.gloDataPath.ToString() != "")
                {
                    strgloDataPath = form.gloDataPath.ToString();
                    if (strgloDataPath.ToLower().EndsWith("prerequisites"))
                    {
           
                    }
                    else
                    {
                        strServerPath = strgloDataPath;
                    }
                    if (strgloDataPath.ToLower().EndsWith("prerequisites"))
                    {
                        //strgloDataPath = form.gloDataPath.ToString();
                    }
                    else
                    {
                        //Release extra \ at end

                        while (strgloDataPath.EndsWith("\\"))
                        { strgloDataPath = strgloDataPath.TrimEnd('\\'); }
                        strgloDataPath += "\\Prerequisites";
                    }

                }
                if (form.CheckDir(strgloDataPath))
                {
                    InstallEasyHL7(strgloDataPath);
                    InstallBlackIce(strgloDataPath);
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
                    
                    if (form.gloNonVoice != null && form.gloNonVoice.ToString() != "")
                    {
                        strVoice = form.gloNonVoice;
                    }

                    if (strVoice.ToLower() == "true")//Non Voice Installation
                    {
                        _typeVoice = 0;
                        InstallgloNonVoice(strgloDataPath);
                        //UpdateCleintSettings(dataBase, serverName, 1);//updating Client Setting_MST Table with NonVoice
                    }
                    if (form.gloVoice != null && form.gloVoice.ToString() != "")
                    {
                        strVoice = form.gloVoice;
                    }

                    if (strVoice.ToLower() == "true")//Voice Installation
                    {
                        _typeVoice = 1;
                        InstallgloVoice(strgloDataPath);

                    }
                    SetRegistry(serverName, dataBase, UserName, Password, strAuthenticationMode, strServerPath);
               
                }
                else
                {
                    MessageBox.Show("Plese copy 'prerequisites' folder to Server Path. i.e \\\\Server\\glodata\\prerequisites");
                    return;
                }
               
                
            }
            
            
          //Installing Prerequisites

            InstallFramework(strgloDataPath);
            //InstallFrameworkSP(strgloDataPath);
            
            InstallWordAddin(strgloDataPath);
            InstallgloEDI(strgloDataPath);
            InstallAlphaIICliet(strgloDataPath);
            InstallgloScanner(strgloDataPath);
            oWriter.Flush();
            oWriter.Close();
           
            Process p = new Process();
            p.StartInfo.FileName = "C:\\InstallEMRPrereq.bat";
            p.StartInfo.CreateNoWindow = true;
            //p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.Start();
            
            base.Install(savedState);

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

            procStartInfo.CreateNoWindow = true;

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
        public bool RegistryExists(string strValue)
        {
            bool _success = false;
            RegistryKey oKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\gloEMR", true);

            object value;
            value = oKey.GetValue(strValue);
            if (value != null && value.ToString() != "")
            {
                _success = true;
            }
            else
            {
                _success = false;
            }
            return _success;
        }
       public void InstallFramework(string strPrerequisitePath)
       {
           
           
           
           string strpath = strPrerequisitePath;
           strpath += "\\Framework35\\dotNetFx35setup.exe";
           RegistryKey oKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NetFramework\\AssemblyFolders\\v3.5", true);
           if (oKey != null && oKey.ToString() != "")
           {

           }
           else
           {
               string strFullPathtoMSI = " \"" + strpath + "\" /q /norestart";
               
               oWriter.WriteLine(strFullPathtoMSI);
           }

       }

       public void InstallFrameworkSP(string strPrerequisitePath)
       {
           CopyData(strPrerequisitePath, "Framework35SP1");
           RegistryKey oKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NetFramework\\AssemblyFolders\\v3.5", true);
           if (oKey != null && oKey.ToString() != "")
           {
               string strpath = @"C:\gloData\Framework35SP1\dotnetfx35setup.exe";
               string strFullPathtoMSI = "\"" + strpath + "\" /q /norestart";
               //ExecutePreerequisites(strFullPathtoMSI);

               oWriter.WriteLine(strFullPathtoMSI);
           }
           
       }

        public void UpdateSettingsAfterInstall()//Existing MAhine
        {
            string strComputerName = System.Environment.MachineName;
            string strConnection = "";
            string strQuery = "";
            object ServerName = GetRegistryValue("sqlserver");
            object strDataBaseName = GetRegistryValue("Database");

            strConnection = "Data Source='" + ServerName + "'; Integrated Security=True";

            strQuery = "USE [" + strDataBaseName + "] Update ClientSettings_MST set sCurrentProductVersion ='5.0.1',sLatestProductVersion='5.0.1',blnIsUpdated= 'True' where sMachineName = '" + strComputerName + "'";

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
        public Int64 VoiceCheck()
        {
            Int64 _success = 0;
            string strComputerName = System.Environment.MachineName;
            string strConnection = "";
            string strQuery = "";
            object ServerName = GetRegistryValue("sqlserver");
            object strDataBaseName = GetRegistryValue("Database");

            strConnection = "Data Source='" + ServerName + "'; Integrated Security=True";
            // strQuery = "USE [" + strDataBaseName + "] select blnvoice from  Clientsettings_MST where sMachineName = '" + strComputerName + "' and blnvoice=1";
            strQuery = " USE [" + strDataBaseName + "] SELECT  isVoice = CASE nVoiceEnabled WHEN 1 THEN '2'  WHEN 0 THEN '1' ELSE '3' END " +
                    "FROM ClientSettings_MST " +
                "where sMachineName = '" + strComputerName + "' ";

            SqlConnection MyCon = new SqlConnection(strConnection);
            SqlCommand MyCmd = new SqlCommand(strQuery, MyCon);
            DataTable dt = new DataTable();

            try
            {
                //Open the Connection
                MyCon.Open();
                MyCmd.CommandTimeout = 0;

                SqlDataAdapter da = new SqlDataAdapter(MyCmd);
                da.Fill(dt);
                if (dt != null)
                    if (dt.Rows.Count == 1)
                    {
                        switch (Convert.ToInt16(dt.Rows[0][0]))
                        {
                            case 1: // NonVoice 
                                _success = 1;
                                break;
                            case 2: // Voice 
                                _success = 2;
                                break;
                            case 3: // No records for m/c                                

                                _success = 3;
                                break;
                            default:
                                _success = 3;
                                break;
                        }

                    }
                    else
                    {
                        _success = 3;

                    }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());//Showing Exception to User
            }
            finally
            {
                MyCon.Close(); //Closing the Connection
            }
            return _success;
        }


        public object GetRegistryValue(string strValue)
        {


            RegistryKey oKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\gloEMR", true);

            object value;
            value = oKey.GetValue(strValue);
            return value;
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

        public void UpdateCleintSettings(string strDataBaseName, string ServerName, int _type)//fresh machine......
        {



            string strComputerName = System.Environment.MachineName;
            string strConnection = "";
            string strQuery = "";
            strConnection = "Data Source='" + ServerName + "'; Integrated Security=True";
            string strCheck = "select count(sMachineName) from ClientSettings_MST where  sMachineName = '" + strComputerName + "'";


            //MessageBox.Show(strQuery);



            SqlConnection MyCon = new SqlConnection(strConnection);
            DataTable dt = new DataTable();
            //Define the Command Object
            if (_type == 0)
            {

                strQuery = "USE [" + strDataBaseName + "] Insert into ClientSettings_MST (nClientID, sMachineName, nVoiceEnabled, nScanEnabled,sProductCode,sCurrentProductVersion,sLatestProductVersion) select isnull(max(nClientID),0)+1,'" + strComputerName + "','True','True',1,'5.0.1','5.0.1' from ClientSettings_MST";

            }
            else
            {
                strQuery = "USE [" + strDataBaseName + "] Insert into ClientSettings_MST (nClientID, sMachineName, nVoiceEnabled, nScanEnabled,sProductCode,sCurrentProductVersion,sLatestProductVersion) select isnull(max(nClientID),0)+1,'" + strComputerName + "','False','True',1,'5.0.1','5.0.1' from ClientSettings_MST";

            }

            SqlCommand MyCmd = new SqlCommand(strQuery, MyCon);
            //SqlDataAdapter Myda=new SqlDataAdapter(MyCmd);

            try
            {
                //Open the Connection
                MyCon.Open();
                MyCmd.CommandTimeout = 0;

                if (MyCmd.ExecuteNonQuery() > 0)
                {

                }


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

        
        public void InstallBlackIce(string strPrerequisitePath)
        {
            //SOFTWARE\\Black Ice Software Inc\\MonoNT\\PrinterVariablesValues
            RegistryKey oKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Black Ice Software Inc\\MonoNT\\PrinterVariablesValues", true);
            //if (oKey != null && oKey.ToString() != "")
            //{
            //    MessageBox.Show("BlackIce Exists");
            //}
            //else
            //{
            //    CopyData(strPrerequisitePath, "BlackICESDK");
            //    CopyBatchFile(strPrerequisitePath, "InstallBlackIce");
            //    Process p = new Process();
            //    p.StartInfo.FileName = @"C:\InstallBlackIce.bat";
            //    p.StartInfo.CreateNoWindow = true;
            //    p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //    p.Start();
            //}
            string strFullPath = strPrerequisitePath;
            strFullPath += "\\BlackICESDK\\TiffNT.msi";
            string strCommand = "msiexec /i  \"" + strFullPath + "\" /qb /norestart";
            
            oWriter.WriteLine(strCommand);
        }

        public void InstallAlphaIICliet(string strPrerequisitePath)
        {
            //SOFTWARE\\Black Ice Software Inc\\MonoNT\\PrinterVariablesValues
            RegistryKey oKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\AlphaII\\CodeWizard", true);
            if (oKey != null && oKey.ToString() != "")
            {
                // //   MessageBox.Show("AlphaIICliet Exists");
            }
            else
            {

                string strFullPath = strPrerequisitePath;
                strFullPath += "\\Alpha II CodeWizard Express Redistrib\\Second.msi";
                string strCommand = "msiexec /i  \"" + strFullPath + "\" /qn /norestart";
                oWriter.WriteLine(strCommand);
                oWriter.WriteLine("reg query HKLM\\SOFTWARE\\AlphaII\\CodeWizard");
                oWriter.WriteLine("echo AlphaII Installed Successfully");
            }
        }


        public void InstallgloEDI(string strPrerequisitePath)
        {
            //1.First Check the registry Key
            //2.If Register Key Exists Dont Copy the Folder of gloEDI from the prerequsisites list
            //3.If Regisrty Key does not exist then copy the gloEDI Folder from the prerequisites list 
            //4.Execute the Batch File...
            RegistryKey oKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\EDIdEv\\Setup", true);
            if (oKey != null && oKey.ToString() != "")
            {
                // MessageBox.Show(" gloEDI Exists");
            }
            else
            {
                string strpath = strPrerequisitePath;
                string strtargetDir = Convert.ToString(Context.Parameters["TargetDir"]);
                while (strtargetDir.EndsWith("\\"))
                { strtargetDir = strtargetDir.TrimEnd('\\'); }
                strpath += "\\gloEDI\\Edidev_FREDI_Runtime.exe";
                string strFullPathtoMSI = " \"" + strpath + "\" -sUZTTXJYTFF8F1XOQ9025 -p  \"" + strtargetDir + "\"";
             
                oWriter.WriteLine(strFullPathtoMSI);
                oWriter.WriteLine("reg Query HKLM\\SOFTWARE\\EDIdEv\\Setup");
                oWriter.WriteLine("echo EDI Installed Successfully");

            }
            
        }

        public void InstallgloScanner(string strPrerequisitePath)
        {
            //1.First Check the registry Key
            //2.If Register Key Exists Dont Copy the Folder of gloEDI from the prerequsisites list
            //3.If Regisrty Key does not exist then copy the gloEDI Folder from the prerequisites list 
            //4.Execute the Batch File...
            RegistryKey oKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Management\\ARPCache\\CSSN SDK Version 9.33.0", true);
            if (oKey != null && oKey.ToString() != "")
            {
                // MessageBox.Show(" gloScanner Exists");
            }
            else
            {
                // CopyData(strPrerequisitePath, "gloScanner");
                string strpath = strPrerequisitePath;
                strpath += "\\gloScanner\\sdk_setup.exe";
                string strFullPathtoMSI = " \"" + strpath + "\" /s /v /qb /norestart";
                //ExecutePreerequisites(strFullPathtoMSI);
                ExecuteScriptCmd(strFullPathtoMSI);
                
                //oWriter.WriteLine(strFullPathtoMSI);

            }
        }

        public void InstallEasyHL7(string strPrerequisitePath)
        {
            //1.First Check the registry Key
            //2.If Register Key Exists Dont Copy the Folder of gloEDI from the prerequsisites list
            //3.If Regisrty Key does not exist then copy the gloEDI Folder from the prerequisites list 
            //4.Execute the Batch File...
            RegistryKey oKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Classes\\Installer\\Products\\69B7D771EFF2E2848944E94C062241E1", true);
            //if (oKey != null && oKey.ToString() != "")
            //{
            //  //  MessageBox.Show(" EasyHL7 Exists");
            //}
            //else
            //{
            //    CopyData(strPrerequisitePath, "EasyHL7");
            //    CopyBatchFile(strPrerequisitePath, "InstallEasyHL7");
            //    Process p = new Process();
            //    p.StartInfo.FileName = @"C:\InstallEasyHL7.bat";
            //    p.StartInfo.CreateNoWindow = true;
            //    p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //    p.Start();
            //}
            string strtargetDir = Convert.ToString(Context.Parameters["TargetDir"]);

           
            //Release extra \ at end
            while (strtargetDir.EndsWith("\\"))
            { strtargetDir = strtargetDir.TrimEnd('\\'); }
            string strFullPath = strPrerequisitePath + "\\EasyHL7\\EasyHL7RuntimeDistribution.msi ";

            strFullPath = "msiexec /i \"" + strPrerequisitePath + "\\EasyHL7\\EasyHL7RuntimeDistribution.msi\" /quiet INSTALLDIR=\"" + strtargetDir + "\\EasyHL7\" ALLUSERS=2";
            
            oWriter.WriteLine(strFullPath);

            // Installshield command 
            //"msiexec /i \"" + SRCDIR ^ "Prerequisites\\EasyHL7\\EasyHL7RuntimeDistribution.msi\" /quiet INSTALLDIR=\"" + INSTALLDIR ^ "Prerequisites\\EasyHL7\" ALLUSERS=2"

            //ExecutePreerequisites(strFullPath);
            //oWriter.WriteLine(strFullPath);
        }
        public void InstallWordAddin(string strPrerequisitePath)
        {
            //1.First Check the registry Key
            //2.If Register Key Exists Dont Copy the Folder of gloEDI from the prerequsisites list
            //3.If Regisrty Key does not exist then copy the gloEDI Folder from the prerequisites list 
            //4.Execute the Batch File...
            //CopyData(strPrerequisitePath, "Word Addin for converting to PDF");
            string strpath = strPrerequisitePath;
            strpath += "\\Word Addin for converting to PDF\\SaveAsPDFandXPS.exe";
            string strFullPathtoMSI = " \"" + strpath + "\" /quiet /norestart";
            
           
            oWriter.WriteLine(strFullPathtoMSI);
            oWriter.WriteLine("reg query HKLM\\Software\\Classes\\Installer\\Products\\000021092B0090400000000000F01FEC");
            oWriter.WriteLine("echo Word Addin Installed Successfully");

        }

        
         public void InstallgloNonVoice(string strPrerequisitePath)
        {
            string strVCPath = strPrerequisitePath;
            strVCPath += "\\VCRuntime\\VCRuntime.msi";
            string strVCCommand = "msiexec /i  \"" + strVCPath + "\" /qn /norestart";


            oWriter.WriteLine(strVCCommand);//commented for testing


            
            string strISscriptpath = strPrerequisitePath;
            strISscriptpath += "\\DNSRDK10\\ISScript1050.msi";
            string strISCommand = "msiexec /i  \"" + strISscriptpath + "\" /quiet /norestart ALLUSERS=2";
            
            oWriter.WriteLine(strISCommand);

            string strDnsRDKPath = strPrerequisitePath;

            strDnsRDKPath += "\\DNSRDK10\\dnsrdk.msi";
            string strDNSCommand = "msiexec /i  \"" + strDnsRDKPath + "\" /quiet /norestart";
           
            oWriter.WriteLine(strDNSCommand);
            
        
        }
        public void InstallVCRuntime(string strPrerequisitePath)
        {
            CopyData(strPrerequisitePath, "VCRuntime");
            CopyBatchFile(strPrerequisitePath, "InstallVCRuntime");
            Process p = new Process();
            p.StartInfo.FileName = @"C:\InstallVCRuntime.bat";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.Start();
        }
        
           public void InstallgloVoice(string strPrerequisitePath)
        {


            RegistryKey oKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\UnInstall\\{E7712E53-7A7F-46EB-AA13-70D5987D30F2}", true);
            if (oKey != null && oKey.ToString() != "")
            {

            }
            else
            {
                string strVCPath = strPrerequisitePath;
                strVCPath += "\\VCRuntime\\VCRuntime.msi";
                string strVCCommand = "msiexec /i  \"" + strVCPath + "\" /qn /norestart";
                //ExecutePreerequisites(strCommand);
                oWriter.WriteLine(strVCCommand);//commented for testing

                //CopyData(strPrerequisitePath, "DNSSDK101");
                // Installshield command 
                //msiexec /i "C:\gloData\DNSSDK101\Dragon NaturallySpeaking 10.msi" SERIALNUMBER="A709A-HE4-0010-2300-00"  DEFAULTSINI="C:\gloData\DNSSDK101\nsdefa\ults.ini" INSTALLDIR="C:\gloData" /Liwemo! "C:\gloData\nuance.log" /qn
                //msiexec /i "C:\gloData\DNSSDK101\Dragon NaturallySpeaking 10.msi" SERIALNUMBER="A709A-HE4-0010-2300-00"  DEFAULTSINI="C:\gloData\DNSSDK101\nsdefaults.ini" INSTALLDIR="C:\gloData" /Liwemo! "C:\gloData\nuance.log"  /qn
                //msiexec /i "C:\gloData\DNSSDK101\Dragon NaturallySpeaking 10.msi" SERIALNUMBER="A709A-HE4-0010-2300-00"  DEFAULTSINI="C:\gloData\DNSSDK101\nsdefaults.ini" INSTALLDIR="C:\gloData" /Liwemo! "C:\gloData\nuance.log " /qn
                //*** Installshield command 

                string strtargetDir = Convert.ToString(Context.Parameters["TargetDir"]);

                //Release extra \ at end
                
                while (strtargetDir.EndsWith("\\"))
                { strtargetDir = strtargetDir.TrimEnd('\\');}
               
                string strFullPath = "\"" + strPrerequisitePath + "\\DNSSDK101\\Dragon NaturallySpeaking 10.msi\" SERIALNUMBER=\"A709A-HE4-0010-2300-00\"  DEFAULTSINI=\"" + strPrerequisitePath + "\\DNSSDK101\\nsdefaults.ini\" INSTALLDIR=\"" + strtargetDir + "\\Prerequisites\" /Liwemo! \"" + strtargetDir + "\\nuance.log\"  /qn";
               
                oWriter.WriteLine(strFullPath);
                oWriter.WriteLine("reg query HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\UnInstall\\{E7712E53-7A7F-46EB-AA13-70D5987D30F2}");
                oWriter.WriteLine("echo Dragon Installed Successfully");

            }
        }
        
        
        

        public void UninstallgloEMRClient(string strPrerequisitePath)
        {
            string strCommand = "REG Delete HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\UnInstall\\Installshield_{5AE656CB-F224-4D7F-9EC9-5543210FCCF3} /F";
            oWriter.WriteLine(strCommand);
        }

       
       
        #region RegistrySettings
        public void SetRegistry(string strServerName, string strDataBaseName, string strUserName, string strPassword, string strAuthentication, string ServerPath)
        {
            //Registry Settings Requirement for Client 

            //1.DataBase Name
            //2.Sql Server Name
            //3.ISWINAUTHENTICATION
            //3.ServerPah (Network Path)

            RegistryKey oKey = Registry.LocalMachine;
            CreateSubKey(oKey, "Software\\gloEMR");
            WriteValue(oKey, "Software\\gloEMR", "SQLServer", strServerName);
            WriteValue(oKey, "Software\\gloEMR", "Database", strDataBaseName);
            WriteValue(oKey, "Software\\gloEMR", "ServerPath", ServerPath);
            WriteValue(oKey, "Software\\gloEMR", "ISWINAUTHENTICATION", strAuthentication.ToUpper());

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
        //public void CopyFolder(string sourceFolder, string destFolder)
        //{

        //    if (!Directory.Exists(destFolder))
        //        Directory.CreateDirectory(destFolder);

        //    string[] files = Directory.GetFiles(sourceFolder);

        //    foreach (string file in files)//looping through files
        //    {

        //        string name = Path.GetFileName(file);
        //        string dest = Path.Combine(destFolder, name);
        //        File.Copy(file, dest, true);

        //    }
        //    string[] folders = Directory.GetDirectories(sourceFolder);

        //    foreach (string folder in folders)//looping through folders
        //    {

        //        string name = Path.GetFileName(folder);
        //        string dest = Path.Combine(destFolder, name);
        //        CopyFolder(folder, dest);//recursive function
        //    }
        //}


        public void CopyData(string strPrerequisitePath, string PrerequisiteName)
        {
            string strSource = strPrerequisitePath + "\\" + PrerequisiteName;
           //MessageBox.Show(strSource);
            if (System.IO.Directory.Exists(strSource.ToString()))
            {
                string strDestination = "C:\\gloData";
                CopyFolder(strSource, strDestination, PrerequisiteName);
            }
            

        }
        public void CopyBatchFile(string strPrerequisitePath,string strBatchFileName)
        {
            string strSource = strPrerequisitePath + "\\BatchFiles\\" + strBatchFileName + ".bat";
            
            if (System.IO.File.Exists(strSource))
            {
                string strDestination = "C:\\" + strBatchFileName + ".bat";
                File.Copy(strSource, strDestination, true);
            }
            
        }
        public void CopyFolder(string sourceFolder, string destFolder, string PrerequisiteName)
        {

            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string CreateFolder = destFolder + "\\" + PrerequisiteName;

            if (!Directory.Exists(CreateFolder))
                Directory.CreateDirectory(CreateFolder);

            string[] files = Directory.GetFiles(sourceFolder);

            foreach (string file in files)//looping through files
            {

                string name = Path.GetFileName(file);
                string dest = Path.Combine(CreateFolder, name);
                File.Copy(file, dest, true);

            }
            //string[] folders = Directory.GetDirectories(sourceFolder);

            //foreach (string folder in folders)//looping through folders
            //{

            //    string name = Path.GetFileName(folder);
            //    string dest = Path.Combine(destFolder, name);
            //    CopyFolder(folder, dest);//recursive function
            //}
        }
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

        public void CopyData(string strPath)
        {
            //string strSource = strPath + @"Prereq";            

            string strSource = strPath;
            //MessageBox.Show(strSource);
            string strDestination = "C:\\gloData";
            CopyFolder(strSource, strDestination);

            strSource = "C:\\gloData\\" + "InstallPrerequisites.bat";
            //MessageBox.Show(strSource);
            strDestination = "C:\\InstallPrerequisites.bat";
            File.Copy(strSource, strDestination, true);
            strSource = "C:\\gloData\\" + "gloNonVoice.bat";

            strDestination = "C:\\gloNonVoice.bat";
            File.Copy(strSource, strDestination, true);
            strSource = "C:\\gloData\\" + "gloVoice.bat";

            strDestination = "C:\\gloVoice.bat";
            File.Copy(strSource, strDestination, true);
        }
        #endregion

        #region InstallingPrerquisites
        public void InstallPrequisites()
        {
            Process p = new Process();
            p.StartInfo.FileName = @"C:\InstallPrerequisites.bat";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.Start();
        }
        #endregion


        #region Installing Feature
        //public void InstallgloNonVoice()
        //{
        //    //MessageBox.Show("Voice");
        //    Process p = new Process();
        //    p.StartInfo.FileName = @"C:\gloNonVoice.bat";
        //    p.StartInfo.CreateNoWindow = true;
        //    p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //    p.Start();
        //}
        //public void InstallgloVoice()
        //{
        //    Process p = new Process();
        //    p.StartInfo.FileName = @"C:\gloVoice.bat";
        //    p.StartInfo.CreateNoWindow = true;
        //    p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //    p.Start();
        //}
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