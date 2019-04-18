using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace gloICDAnalysis.ClassLib
{
    class clsInstallDBScripts
    {
        public static bool ScriptsNonReplication(string strPathtoScript, string NonReplicationLogName, string servername, string databasename, string username, string password, string strissql, string strOutputPath)
        {
            bool Issql = false;
            if (strissql.ToLower() == "true")
            {
                Issql = true;
            }
            string Message = "Query Executing on Database " + databasename + "";

            if (CreateFolder(strOutputPath))
            {
                strOutputPath = strOutputPath + "\\" + NonReplicationLogName + ".txt";
            }
            bool success = false;
            try
            {
                if (File.Exists(strPathtoScript))//check whether .sql file exists
                {
                    if (RunScript(strPathtoScript, servername, databasename, strOutputPath, Issql, username, password))//run the scripts 
                    {
                        Application.DoEvents();
                        if (File.Exists(strOutputPath))//check the output file exists or not
                        {
                            WriteToTextFile(strOutputPath, Message);
                            if (ReadLogFile(strOutputPath))//check whether script executed successfully or not
                            {
                                WriteToTextFile(strOutputPath, "Query Executed Successfully");
                                success = true;
                                Application.DoEvents();
                            }
                            else
                            {
                                WriteToTextFile(strOutputPath, "Query Execution completed with errors");
                               // WriteLogEntry("Please check log file at following location" + strOutputPath + "");
                            }
                        }
                    }

                }

            }

            catch (Exception ex)
            {
                gloServerLog("Exception while executing Non replication script " + ex.Message.ToString() + "");
            }
            Application.DoEvents();
            return success;
        }

        public static bool RunScript(string strPath, string strServerName, string strDatabase, string strOutputpath, bool IsSQlAuthentication, string UserName, string Password)
        {
            Application.DoEvents();
            bool _success = false;
            if (!strOutputpath.ToLower().EndsWith("gloICDAnalysisDB.txt"))
            {
              //  pgInfo.SetmyStatusText("Executing Database script on Server :" + strServerName + " and Database :" + strDatabase + "");
            }


            string strCommand = string.Empty;
            try
            {

                string strpath = "sqlcmd";


                if (!String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Password)) //check for credentials
                {
                    strCommand = "-U " + UserName + " -P " + Password + " -W -S " + strServerName + " -d " + strDatabase + " -i\"" + strPath + "\"  -o \"" + strOutputpath + "\"";
                }
                else //windows authentication execute scripts with windows authentication
                {
                    strCommand = "-E -W -S " + strServerName + " -d " + strDatabase + " -i\"" + strPath + "\"  -o \"" + strOutputpath + "\"";
                }

                gloServerLog(strCommand);

                if (RunExe(strpath, strCommand))
                {
                    _success = true;
                   // WriteLogEntry("Executed Script successfully on Server : " + strServerName + " Database : " + strDatabase + "");
                    Application.DoEvents();
                }
                else
                {
                    //WriteLogEntry("Execution of script failed on Server : " + strServerName + " Database : " + strDatabase + "");
                }

            }
            catch //(Exception ex)
            {

                //WriteLogEntry("Exception while Executing script  failed on Server : " + strServerName + " Database : " + strDatabase + "");
                //WriteLogEntry("Exception in RunScript " + ex.Message.ToString() + "");
            }
            finally
            {

            }
            Application.DoEvents();
            return _success;
        }

        public static void WriteToTextFile(string pathtofile, string text)
        {
            TextWriter tw = null;
            try
            {
                tw = new StreamWriter(pathtofile, true);
                // write a line of text (present date/time) to the file  
                tw.WriteLine(DateTime.Now);
                // write the rest of the text lines  
                tw.Write(text);
                // close the stream   
                tw.Close();

            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), false);
            }
            finally
            {
                if (tw != null)
                {
                    tw.Dispose();
                }

            }
        }

        public static bool ReadLogFile(string strFilepath)
        {
            StreamReader objStreamReader = null;
            bool _success = false;
            try
            {
                string FILENAME = strFilepath;
                int count = 0;
                string[] arInfo;
                string line;
                //Get a StreamReader class that can be used to read the file
                objStreamReader = File.OpenText(FILENAME);
                while ((line = objStreamReader.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        // define which character is seperating fields
                        char[] textdelimiter = { ',' };
                        arInfo = line.Split(textdelimiter);
                        if (arInfo.Length > 4)
                        {
                            _success = false;
                            count = 1;
                            break;

                        }
                        else
                        {
                            if (count == 0)
                            {
                                _success = true;
                            }
                            else
                            {
                                _success = false;
                            }
                        }
                    }
                    else
                    {
                        _success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), false);
            }
            finally
            {
                if (objStreamReader != null)
                {
                    objStreamReader.Close();
                    objStreamReader.Dispose();
                    objStreamReader = null;
                }
            }
            Application.DoEvents();
            return _success;
        }
        
        public static bool ImportTextFileData(string TblName,string Filepath,string DBConnection)
        {
            bool _result = false;
            DataTable dtTable = null;

            if (clsDBSettings.IsRecordExist(TblName) == true)
            {
                return true;
            }
            dtTable = new DataTable();
            if (TblName == "ICD10CM_order")
            {
               
                dtTable.Columns.Add("nICD10OrderFilesID");
                dtTable.Columns.Add("sICD10Code");
                dtTable.Columns.Add("nCodeType");
                dtTable.Columns.Add("sShortDescription");
                dtTable.Columns.Add("sLongDescription");
            }
            else if (TblName == "ICD10_GEM_File")
            {
                dtTable.Columns.Add("nICD9ToICD10MappingID");
                dtTable.Columns.Add("sICD9Code");
                dtTable.Columns.Add("sICD10Code");
                dtTable.Columns.Add("sFlag");               
            }

            try
            {
                string line = null;

                string strICD10OrderCodeFilePath = Filepath; //Application.StartupPath + @"\CodeFiles\icd10cm_order.txt";

                using (System.IO.StreamReader sr = System.IO.File.OpenText(strICD10OrderCodeFilePath))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] data = line.Split('\t');
                        if (data.Length > 0)
                        {
                            DataRow row = dtTable.NewRow();
                            row.ItemArray = data;
                            dtTable.Rows.Add(row);

                        }
                    }
                    sr.Close();
                }



                using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(DBConnection))
                {
                    cn.Open();
                    using (System.Data.SqlClient.SqlBulkCopy copy = new System.Data.SqlClient.SqlBulkCopy(cn))
                    {
                        copy.BulkCopyTimeout = 0;
                        copy.ColumnMappings.Add(0, 0);
                        copy.ColumnMappings.Add(1, 1);
                        copy.ColumnMappings.Add(2, 2);
                        copy.ColumnMappings.Add(3, 3);
                        if (TblName == "ICD10CM_order")
                        {
                            copy.ColumnMappings.Add(4, 4);
                            copy.DestinationTableName = "ICD10OrderFiles";
                        }
                        else if (TblName == "ICD10_GEM_File")
                        {
                            copy.DestinationTableName = "ICD9ToICD10Mapping";
                        }

                        if (dtTable.Rows.Count > 0)
                        {
                            copy.WriteToServer(dtTable);
                            _result = true;
                        }

                    }
                    cn.Close();
                }


            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
            finally
            {
                if (dtTable != null)
                {
                    dtTable.Dispose();
                    dtTable = null;
                }
            }
            return _result; 
            
        }

        public static System.IO.StreamWriter logFileStreamWriter = null;

        public static void gloServerLog(string Message)
        {
            string logFileName = string.Empty;
            string strgloServerLogPath = string.Empty;
            if (null == logFileStreamWriter)
            {
                try
                {
                    if (!String.IsNullOrEmpty(Application.StartupPath))
                    {
                        strgloServerLogPath = Application.StartupPath + "\\InstallationLogs";
                    }
                    logFileName = "\\gloServerEditionlog.txt";

                    if (CreateFolder(strgloServerLogPath))
                    {
                    }
                    strgloServerLogPath = strgloServerLogPath + logFileName;
                    // Always create and overwrite the old log file.
                    logFileStreamWriter = new System.IO.StreamWriter(
                        new System.IO.FileStream(strgloServerLogPath,
                        System.IO.FileMode.Append | System.IO.FileMode.OpenOrCreate));
                }
                catch (Exception)
                {
                    // MessageBox.Show(ex.Message.ToString());
                }
            }
            WriteMessagetoLog(Message, logFileStreamWriter);
        }

        public static void WriteMessagetoLog(string message, StreamWriter logFileStreamWriter)
        {
           // StackFrame stackFrame = new StackFrame(1, true);
            //string logMessage = DateTime.Now.ToLocalTime().ToString() +
            //    ":  " + message +
            //    "  {" + stackFrame.GetFileName() + ":Line " +
            //    stackFrame.GetFileLineNumber() + ": [" +
            //    stackFrame.GetMethod() + "]}";
            string logMessage = DateTime.Now.ToLocalTime().ToString() +
               ":  " + message + "";

            // Write to a file.
            if (null != logFileStreamWriter)
            {
                logFileStreamWriter.WriteLine(logMessage);
                // Seek to the end of the file
                logFileStreamWriter.BaseStream.Seek(0, System.IO.SeekOrigin.End);
                // Flush out the data.
                logFileStreamWriter.Flush();
            }
        }

        public static bool CreateFolder(string strPath)
        {
            bool success = false;
            try
            {
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);

                    success = Directory.Exists(strPath);
                }
                else
                {
                    success = true;
                }
            }
            catch (System.AccessViolationException)
            {

            }
            catch (System.ArgumentException)
            {

            }
            catch (System.Exception)
            {

            }
            return success;
        }

        public static bool ExecuteQueryWithTranscation(string strQuery, string strUserName, string strPassword, string strSqlServer, string strDatabase)
        {
            string strConnection = "";
            bool _success = false;
            SqlConnection Mycon = null;
            SqlCommand Mycmd = null;
            SqlTransaction objTransaction = null;
            try
            {


                if (!String.IsNullOrEmpty(strUserName) && !String.IsNullOrEmpty(strPassword))
                {
                    strConnection = "Data Source='" + strSqlServer + "';User id='" + strUserName + "';password='" + strPassword + "'; Database='" + strDatabase + "' ";
                }
                else
                {
                    strConnection = "Data Source='" + strSqlServer + "'; Integrated Security=True; Database='" + strDatabase + "'";
                }
                using (Mycon = new SqlConnection(strConnection))
                {
                    Mycon.Open();
                    objTransaction = Mycon.BeginTransaction();
                    using (Mycmd = new SqlCommand(strQuery, Mycon, objTransaction))
                    {
                        Mycmd.CommandTimeout = 0;
                        if (Mycmd.ExecuteNonQuery() >= 0)
                        {
                            _success = true;
                            objTransaction.Commit();
                        }
                    }
                    Mycon.Close();
                }
            }
            catch //(Exception ex)
            {
                // gloServerLog("Exception : while Executing Query " + strQuery + " " + ex.Message.ToString() + "");
                objTransaction.Rollback();
            }
            finally
            {
                if (objTransaction != null)
                {
                    objTransaction.Dispose();
                    objTransaction = null;
                }

                if (Mycmd != null)
                {
                    Mycmd.Dispose();
                    Mycmd = null;
                }
                if (Mycon != null)
                {
                    if (Mycon.State == ConnectionState.Open)
                        Mycon.Close();

                    Mycon.Dispose();
                    Mycon = null;
                }
            }
            return _success;
        } //ExecuteQueryWithTranscation

        public static bool RunExe(string strPath, string strCommand)
        {
            bool Success = false;
            string strsetup = string.Empty;
            string strWinpath = Environment.GetEnvironmentVariable("WINDIR").ToString();//gets the WIndir path
            System.Diagnostics.Process oProcess = new Process();
            if (strPath == "")
            {
                strsetup = ("" + strWinpath + "\\System32\\msiexec.exe");
            }
            else
            {
                strsetup = strPath;
            }
            try
            {
                if (oProcess != null)
                {
                    oProcess.StartInfo.FileName = strsetup;                   
                    oProcess.StartInfo.Arguments = strCommand;
                    oProcess.StartInfo.UseShellExecute = false;
                    oProcess.StartInfo.CreateNoWindow = true;                   
                    oProcess.Start();
                    do
                    {                       
                        oProcess.Refresh();
                        Application.DoEvents();
                    }
                    while (!oProcess.HasExited);
                    Application.DoEvents();
                    if (oProcess.ExitCode == 0 || oProcess.ExitCode == 3010)
                    {
                        Success = true;
                    }
                    else
                    {
                        gloServerLog("Msi not installed successfully for file :" + strsetup + " and cmd :" + strPath + " , Exit code :" + oProcess.ExitCode);
                    }
                }
            }
            catch (Exception ex)
            {
                gloServerLog("Exception in RunExe " + ex.Message.ToString() + "");
            }
            finally
            {
                if (oProcess != null)
                {
                    oProcess.Close();
                    oProcess.Dispose();
                }
            }
            return Success;
        }

    }
}
