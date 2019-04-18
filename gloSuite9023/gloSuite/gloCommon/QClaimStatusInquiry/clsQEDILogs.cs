using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Win32;
using System.Collections;
using System.Diagnostics;


namespace TriArqEDIRealTimeClaimStatus
{
    public class clsQEDILogs
    {

        private static System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public static string _databaseconnectionstring = "";
        public static bool gblnEnableApplicationLogs = false;
        public static bool gblnEnableExceptionLogs = false;
        //private static Int64 _nAuditTrailID = 0;
        private static DateTime _dtActivityDateTime;
        //private static string _sActivityModule = "";
        //private static string _sActivityCategory = "";
        //private static string _sActivityType = "";
        //private static string _sDescription = "";
        //private static string _sOutcome = "";
        private static string _sMachineName = "";
        //private static Int64 _nTransactionID = 0;
        private static Int64 _nClinicID = 0;
        private static string _MessageBoxCaption = String.Empty;


        private static void GetDefaultParameters()
        {
            try
            {
                // Machine Name
                _sMachineName = System.Windows.Forms.SystemInformation.ComputerName;

                //DatabaseConnection String
                if (appSettings["DataBaseConnectionString"] != null)
                {
                    if (appSettings["DataBaseConnectionString"] != "")
                    { _databaseconnectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]); }
                    else{ _databaseconnectionstring = ""; }
                }
                else
                { _databaseconnectionstring = ""; }

                //Clinic ID
                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _nClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _nClinicID = 0; }
                }
                else
                { _nClinicID = 0; }

                if (_databaseconnectionstring == "")
                {
                    _databaseconnectionstring = @"Server=glosvr02\sql2008r2;Database=gloClaimData;Trusted_Connection=yes;";

                }

            }
            catch (Exception ex)
            {
                ExceptionLog(ex, false);
            }
        }

        public static void ActivityLogFile(string TextToLog)
        {
            System.IO.StreamWriter objFile = null;
            System.Text.StringBuilder strMessage = new System.Text.StringBuilder();
            string _fileName = null;
            string LogPath = null;
            try
            {
                _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";

                LogPath = System.Windows.Forms.Application.StartupPath + "\\Log\\EDIOperationLogs";
                if (CreateDirectoryIfNotExists(LogPath))
                {
                    objFile = new System.IO.StreamWriter(LogPath + "\\" + _fileName, true);
                    strMessage.Append(Environment.NewLine);
                    strMessage.Append(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + ", ");
                    strMessage.Append(TextToLog);
                    objFile.WriteLine(strMessage.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ActivityModule.EDIActivityLog, ActivityType.SaveToFile, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (objFile != null)
                {
                    objFile.Close();
                    objFile.Dispose();
                    objFile = null;
                }
                if (strMessage != null)
                {
                    strMessage.Remove(0, strMessage.Length);
                    strMessage = null;
                }
                _fileName = null;
                LogPath = null;
            }
        }

        public static Int64 CreateQEDIActivityLog(ActivityModule oActivityModule,
                                                  ActivityType oActivityType,
                                                  string Description,
                                                  ActivityOutCome oActivityOutCome,
                                                  string MachineName="",
                                                  ActivityReference oActvityReference = ActivityReference.None,
                                                  string ActivityReferenceValue = "")
        {
            GetDefaultParameters();
            Int64 activityID = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object oReturnID = null;

            if (MachineName == "")
            {
                MachineName = _sMachineName;
            }

            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@ActivityLogId", activityID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@ActivityDateTime", DateTime.Now, ParameterDirection.Input, SqlDbType.Date);
                oDBParameters.Add("@ActivityModule", oActivityModule.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@ActivityType", oActivityType.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@ActivityOutcome", oActivityOutCome.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@Description", Description, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@MachineName", MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@ActivityReference", oActvityReference.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@ActivityReferenceValue", ActivityReferenceValue, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Execute("INUP_EDI_Operation_ActivityLog", oDBParameters, out oReturnID);
                if (oReturnID != null)
                    activityID = Convert.ToInt64(oReturnID);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)// ex)
            {
                ExceptionLog(ActivityModule.EDIActivityLog, ActivityType.SaveToDatabase, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oReturnID != null) oReturnID = null;
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }

                Clear();
            }


            return activityID;
        }

        public static void Clear()
        {
            //_nAuditTrailID = 0;
            _dtActivityDateTime = DateTime.Now;
            //_sActivityModule = "";
            //_sActivityCategory = "";
            //_sActivityType = "";
            //_sDescription = "";
            //_nTransactionID = 0;
            _sMachineName = "";
            //_sOutcome = "";
            _nClinicID = 0;
        }


        public static void ExceptionLog(Exception exceptionOccured, bool ShowMessageBox)
        {
            string strLogMessage = string.Empty;
            string _fileName = string.Empty;
            try
            {

                String exceptionLogPath = Application.StartupPath + "\\Log\\ExceptionLog";
                if (CreateDirectoryIfNotExists(exceptionLogPath))
                {

                    strLogMessage = BuildExceptionLogString(exceptionOccured);
                    _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";
                    File.AppendAllText(exceptionLogPath + "\\" + _fileName, strLogMessage);
                }


                if (ShowMessageBox == true)
                {
                    //frmException frex = new frmException(exceptionOccured.ToString(), strLogMessage);
                    //frex.ShowDialog();
                    //if (frex != null)
                    //{
                    //    frex.Close();
                    //    frex.Dispose();
                    //    frex = null;
                    //}

                }
            }
            catch// (Exception ex)
            {
                //ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                strLogMessage = null;
                _fileName = null;
            }
        }

        public static void ExceptionLog(ActivityModule oActivityModule, ActivityType oActivityType, Exception exceptionOccured, ActivityOutCome oActivityOutCome)
        {
            System.IO.StreamWriter objFile = null;
            System.Text.StringBuilder strMessage = new System.Text.StringBuilder();
            string _fileName = string.Empty;
            string LogPath = string.Empty;
            try
            {
                _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";
                LogPath = System.Windows.Forms.Application.StartupPath + "\\Log\\ExceptionLog";
                if (CreateDirectoryIfNotExists(LogPath))
                {
                    objFile = new System.IO.StreamWriter(LogPath + "\\" + _fileName, true);
                    try
                    {
                        strMessage.Append(Environment.NewLine);
                        //strMessage.Append("-------------------------------------------------------------------------------------------------");
                        //strMessage.Append(Environment.NewLine);
                        strMessage.Append(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + ", ");
                        strMessage.Append("Module: " + oActivityModule.ToString() + ", ");
                        strMessage.Append("Type: " + oActivityType.ToString() + ", ");
                        strMessage.Append("OutCome: " + oActivityOutCome.ToString());
                        strMessage.Append("Description: " + BuildExceptionLogString(exceptionOccured) + ", ");
                        //strMessage.Append("-------------------------------------------------------------------------------------------------");
                        objFile.WriteLine(strMessage.ToString());
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (objFile != null)
                {
                    objFile.Close();
                    objFile.Dispose();
                    objFile = null;
                }
                if (strMessage != null)
                {
                    strMessage.Remove(0, strMessage.Length);
                    strMessage = null;
                }
                _fileName = null;
                LogPath = null; ;
            }
        }


        public static bool CreateDirectoryIfNotExists(string dirName)
        {
            bool exists = false;
            try
            {
                try
                {
                    exists = System.IO.Directory.Exists(dirName);
                    if (exists)
                    {
                        return true;
                    }
                }
                catch
                {
                }
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(dirName);
                }
            }
            catch
            {
            }
            try
            {
                exists = System.IO.Directory.Exists(dirName);
            }
            catch
            {
            }
            return exists;
        }

        private static string BuildExceptionLogString(Exception thrownexception)
        {
            string _exLogString = Environment.NewLine + "" +
                                System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + Environment.NewLine;
            if (thrownexception == null)
            {
                return "\tBlank Exception" + Environment.NewLine;
            }
            try
            {
                _exLogString = _exLogString + "Message: " + thrownexception.Message + Environment.NewLine;
            }
            catch
            {
            }
            try
            {
                _exLogString = _exLogString + "Source: " + thrownexception.Source + Environment.NewLine;
            }
            catch
            {
            }
            try
            {
                _exLogString = _exLogString + "StackTrace: " + thrownexception.StackTrace + Environment.NewLine;
            }
            catch
            {
            }
            try
            {
                _exLogString = _exLogString + "TargetSite: " + thrownexception.TargetSite + Environment.NewLine;
            }
            catch
            {
            }
            try
            {
                if (thrownexception.InnerException != null && thrownexception.InnerException.Message.Trim() != "")
                {
                    try
                    {
                        _exLogString = _exLogString + "     InnerException: " + Environment.NewLine +
                            BuildExceptionLogString(thrownexception.InnerException);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
            return _exLogString;
        }
    }
}
