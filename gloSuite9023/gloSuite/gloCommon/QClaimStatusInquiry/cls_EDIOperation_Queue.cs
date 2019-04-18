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
    public enum QueueStatus
    {
        None = 0,
        Inlined =1,
        RequestFileGeneration= 2,
        ReadyToUpload = 3,
        DownloadComplete = 4,
        ResponseParsing =5,
        Complete=6,
        Failed=7
    }

    public static class cls_EDIOperation_Queue
    {
        private static string _databaseconnectionstring = "";
        private static System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private static void GetDefaultParameters()
        {
            try
            {

                //DatabaseConnection String
                if (appSettings["DataBaseConnectionString"] != null)
                {
                    if (appSettings["DataBaseConnectionString"] != "")
                    { _databaseconnectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]); }
                    else { _databaseconnectionstring = ""; }
                }
                else
                { _databaseconnectionstring = ""; }


                if (_databaseconnectionstring == "")
                {
                    _databaseconnectionstring = @"Server=glosvr02\sql2008r2;Database=gloClaimData;Trusted_Connection=yes;";

                }

            }
            catch (Exception)
            {
              //  ExceptionLog(ex, false);
            }
        }

        public static void InitiateQueue(string ClaimNumber, string AusID)
        {
            GetDefaultParameters();
            Int64 activityID = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object oReturnID = null;
            
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@QueueID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@AUSID", AusID, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@ClaimNumber", ClaimNumber, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@QueueStatus",QueueStatus.Inlined, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@ModifiedDateTime", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDB.Execute("INUP_EDI_Operation_Queue", oDBParameters, out oReturnID);
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
               clsQEDILogs.ExceptionLog(ActivityModule.EDIActivityLog, ActivityType.SaveToDatabase, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oReturnID != null) oReturnID = null;
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }
        }

        public static void GetClaimQueueNumber(string ClaimNumber, string AusID)
        {
        }

        public static void UpdateQueueStatus(string ClaimNumber, string AusID, QueueStatus oQueueStatus, long Queueid = 0)
        {
            Int64 activityID = 0; 
            GetDefaultParameters();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object oReturnID = null;
            
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@QueueID", activityID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@AUSID", AusID, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@ClaimNumber", ClaimNumber, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@QueueStatus", oQueueStatus.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@ModifiedDateTime", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDB.Execute("INUP_EDI_Operation_Queue", oDBParameters, out oReturnID);
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
                clsQEDILogs.ExceptionLog(ActivityModule.EDIActivityLog, ActivityType.SaveToDatabase, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oReturnID != null) oReturnID = null;
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }
        }
    }
}
