using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
//using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.Smo.Agent;
//using SQLDMO;
using System.Windows.Forms;
using System.Collections;

/* Refrences to add to the project
 
References ::
Microsoft.SqlServer.Smo
Microsoft.SqlServer.SmoEnum
Microsoft.SqlServer.SqlEnum
Microsoft.SqlServer.ConnectionInfo
SQLDMO
 
 */



namespace gloSecurity
{
    class clsBackUp
    {

        private string _databaseconnectionstring;
        //private string _sSQLServerName;
        //private string _sSQLDataBaseName;
        //private string _sSQLLoginName;
        //private string _sSQLPassword;
        //private string _sWindowsAuthentication;
        //Server srvSql;
        //ServerConnection srvConn;
        //private string _sBackUpPath;
        //private string  sFilePath;


        //SQLDMO.Application oSQLServerDMOApp = new SQLDMO.Application();
        //SQLDMO.Job SQLJob = new SQLDMO.Job();
        
       
        //public string BackUpPath
        //{
        //    get { return _sBackUpPath;}
        //    set { _sBackUpPath = value;}
        //}

        #region "Constructor & Destructor"

                  
            public clsBackUp(string DatabaseConnectionString)
            {
                _databaseconnectionstring = DatabaseConnectionString;
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

            ~clsBackUp()
            {
                Dispose(false);
            }

     #endregion

 

//        /// <summary>
//        /// Connecting to the server.
//        /// Along with connecting, we're also going to retrieve a list of databases from that server. 
//        /// </summary>
//        /// <returns></returns>
//        public bool chkSettings()
//        { 
//            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
//            _sSQLServerName = appSettings["SQLServerName"];
//            _sSQLDataBaseName = appSettings["DatabaseName"];
//            _sSQLLoginName =appSettings["SQLLoginName"];
//            _sSQLPassword = appSettings["SQLPassword"];
//            _sWindowsAuthentication = appSettings["WindowAuthentication"];

            
            
//           //Connect, we're going to retrieve a list of databases from that server. 

//            if (_sSQLServerName != null)
//            {

//                // Create a new connection to the selected server name

//                srvConn = new ServerConnection(_sSQLServerName);
                

//                // Log in using SQL authentication instead of Windows authentication

//                srvConn.LoginSecure = true;

//                // Give the login username

//                //srvConn.Login = _sSQLLoginName;

//                // Give the login password

//                //srvConn.Password = _sSQLPassword;

//                // Create a new SQL Server object using the connection we created

//                srvSql = new Server(srvConn);

//                // Loop through the databases list

                
//                foreach (Microsoft.SqlServer.Management.Smo.Database dbServer in srvSql.Databases)
//                {
//                    //Check if the current database is valid.
//                    if (dbServer.Name == _sSQLDataBaseName)
//                        return true;

//                }
//                return false;
                
//            }//end if()
//            else
//                return false;
            

            

//        }//end chkSetting


      
//        /// <summary>
//        /// First we check to see if a connection was made, then we prompt the user to choose a path 
//        /// where he wants to save the backup file (BAK extension). 
//        /// </summary>
//        /// <returns></returns>
        
//        public bool dbBackUp(string bckFileName,string sBackUpPath)
//        {
//            _sBackUpPath = sBackUpPath;
//            // If there was a SQL connection created

//            if (srvSql != null)
//            {

//                // If the user has chosen a path where to save the backup file

//                if (BackUpPath != "")
//                {

//                    // Create a new backup operation

//                    Microsoft.SqlServer.Management.Smo.Backup bkpDatabase = new Microsoft.SqlServer.Management.Smo.Backup();
                                        
//                    // Set the backup type to a database backup

//                    bkpDatabase.Action = BackupActionType.Database;
                    
//                    // Set the database that we want to perform a backup on

//                     bkpDatabase.Database = _sSQLDataBaseName; 
                   
                                        
//                    // Set the backup device to a file
                                       
                    
//                    //Setting the extension for the bakup file.
//                    string sFileName = bckFileName + ".bak";
//                     sFilePath = BackUpPath + "\\" + sFileName;

                   
//                    if (File.Exists(sFilePath))
//                    {
//                        //If the file already exits return.
//                        return false;
//                    }

//                    BackupDeviceItem bkpDevice = new BackupDeviceItem(sFilePath,DeviceType.File);
//                    //BackupDeviceItem bkpDevice = new BackupDeviceItem(BackUpPath +"/"+ bckFileName,DeviceType.File);

//                    // Add the backup device to the backup

//                    bkpDatabase.Devices.Add(bkpDevice);

//                    // Perform the backup
                    
//                    bkpDatabase.SqlBackup(srvSql);
                   
//                    return true;
                    
//                }
//                return false;
//            }

//            else
//            {

//                // There was no connection established; probably the Connect button was not clicked
//                return false;
               

//            }


//        }// end method dbBackUp


//        /// <summary>
//        /// Create Job on Server Agent. 
//        /// </summary>
//        /// <param name="oSchedule"></param>
//        /// <param name="sFilePath"></param>
//        /// <returns></returns>

//        public bool CreateJob_Sql(ScheduleInfo oSchedule,string sFilePath)
//       {
//            try
//            {
//                SQLDMO._SQLServer SQLServer = new SQLDMO.SQLServerClass();
//                SQLDMO.JobSchedule SQLSchedule = new SQLDMO.JobSchedule();
                
//                //If login secure is true for windows authentication
//                SQLServer.LoginSecure = Convert.ToBoolean(_sWindowsAuthentication) ;
//                //Connecting to the server
//                SQLServer.Connect(_sSQLServerName, _sSQLLoginName, _sSQLPassword);
//                //Checking if the Sql JobServer is stopped or running(Job Server Agent).
//                switch (SQLServer.JobServer.Status)
//                {
//                    case SQLDMO_SVCSTATUS_TYPE.SQLDMOSvc_Stopped:
//                    SQLServer.JobServer.Start();
//                    SQLServer.JobServer.AutoStart = true;
//                    break;
//                }

//               SQLJob.Name = _sSQLDataBaseName + DateTime.Now.ToString();
//               SQLJob.Description = "Check and Backup  " + _sSQLDataBaseName +DateTime.Now.ToString();
//               //Add job to the job server.
//               SQLServer.JobServer.Jobs.Add(SQLJob);
//               SQLJob.Category = "Database Maintenance";
//               SQLDMO.JobStep aJobStep = new SQLDMO.JobStep();
//               aJobStep.Name = "Step 1: Backup the Database";
//               aJobStep.StepID = 1;
//               aJobStep.DatabaseName = _sSQLDataBaseName;
//               aJobStep.SubSystem = "TSQL";
//               //------>>> If BackUp Folder is Not Found then create BackUp Folder.
//               string   DirectoryName = "D:\\BackUp";
//               if (Directory.Exists(DirectoryName)==false)
//               {
//                System.IO.Directory.CreateDirectory(DirectoryName);
//               }
//               //Making the log of the schedule in file.
//               aJobStep.OutputFileName ="D:\\BackUp\\gloPMS_BackUp_logFile.txt";
               
                                       
//               //------>>>
//               string sExt;
//               //Command to executed for the backup operation.
//               //sExt = "BACKUP DATABASE [gloPMS_Data_SG] TO  DISK = N'D:\\Program Files\\Microsoft SQL Server\\MSSQL.1\\MSSQL\\Backup\\gloPMS_Data_now_code12Jan' WITH NOFORMAT, NOINIT,  NAME = N'gloPMSData_20080108-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
               
//               sExt = "BACKUP DATABASE ["+_sSQLDataBaseName+"] TO  DISK = N'"+sFilePath+"' WITH NOFORMAT, NOINIT,NAME = N' "+_sSQLDataBaseName+"-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
                           
//                aJobStep.Command = sExt;
//                aJobStep.OnSuccessAction = SQLDMO_JOBSTEPACTION_TYPE.SQLDMOJobStepAction_QuitWithSuccess;
//                aJobStep.OnFailAction = SQLDMO_JOBSTEPACTION_TYPE.SQLDMOJobStepAction_QuitWithFailure;
//                SQLJob.JobSteps.Add(aJobStep);
//                SQLJob.ApplyToTargetServer(_sSQLServerName);
//                aJobStep.DoAlter();
//                SQLJob.Refresh();
//                aJobStep.Refresh();

//                return CreateShedule_Sql(SQLJob,oSchedule);
        
//            }
//            catch (Exception Err)
//            {
//                MessageBox.Show(Err.ToString(), "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return false;
//            }
//}

        
//       /// <summary>
//       /// Create Schedule for the Backup.
//       /// </summary>
//       /// <param name="SQLJob"></param>
//       /// <param name="oSchedule"></param>
//       /// <returns></returns>
//       public bool CreateShedule_Sql(SQLDMO.Job SQLJob,ScheduleInfo oSchedule)
//        {
//            try
//            {
                
//                SQLDMO._SQLServer SQLServer = new SQLDMO.SQLServerClass();
//                SQLDMO.JobSchedule SQLSchedule = new SQLDMO.JobSchedule();
               
//                //for window authentication
//                SQLServer.LoginSecure = true;
//                SQLServer.Connect(_sSQLServerName,_sSQLLoginName,_sSQLPassword);
                
//                SQLJob = SQLServer.JobServer.Jobs.Item(SQLJob.Name);
//                // create a new JobSchedule object

//                SQLSchedule.Name = oSchedule.ScheduleName;
                
//                if (oSchedule.ScheduleFrequency == "Daily")
//                {
//                    SQLSchedule.Schedule.FrequencyType = SQLDMO.SQLDMO_FREQUENCY_TYPE.SQLDMOFreq_Daily;
//                    SQLSchedule.Schedule.FrequencyInterval = oSchedule.DailyFrequecy;  

//                }
//                if (oSchedule.ScheduleFrequency == "Weekly")
//                {
//                    SQLSchedule.Schedule.FrequencyType = SQLDMO.SQLDMO_FREQUENCY_TYPE.SQLDMOFreq_Weekly;
                   
//                    //the number of days or weeks between recurrences of the schedule.
//                    SQLSchedule.Schedule.FrequencyRecurrenceFactor = oSchedule.WeekFrequency;
                    
//                    string[] tempAL = oSchedule.WeekDays.Split(',');
//                    for (int i = 0; i < tempAL.Length ; i++)
//                    {
//                        //to set the week day(s) of schedule
//                        switch (tempAL[i])
//                        { 
//                            case "MON" :
//                                SQLSchedule.Schedule.FrequencyInterval = SQLSchedule.Schedule.FrequencyInterval + Convert.ToInt32(SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Monday);
//                                break;
//                            case "TUE" :
//                                SQLSchedule.Schedule.FrequencyInterval = SQLSchedule.Schedule.FrequencyInterval + Convert.ToInt32(SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Tuesday); 
//                                break;
//                            case "WEN" :
//                                SQLSchedule.Schedule.FrequencyInterval = SQLSchedule.Schedule.FrequencyInterval + Convert.ToInt32(SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Wednesday);
//                                break;
//                            case "THU" :
//                                SQLSchedule.Schedule.FrequencyInterval = SQLSchedule.Schedule.FrequencyInterval + Convert.ToInt32(SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Thursday);
//                                break;
//                            case "FRI" :
//                                SQLSchedule.Schedule.FrequencyInterval = SQLSchedule.Schedule.FrequencyInterval + Convert.ToInt32(SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Friday);
//                                break;
//                            case "SAT" :
//                                SQLSchedule.Schedule.FrequencyInterval = SQLSchedule.Schedule.FrequencyInterval + Convert.ToInt32(SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Saturday);
//                                break;
//                            case "SUN" :
//                                SQLSchedule.Schedule.FrequencyInterval = SQLSchedule.Schedule.FrequencyInterval + Convert.ToInt32(SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Sunday);
//                                break;
//                            case "ALL" :
//                                SQLSchedule.Schedule.FrequencyInterval = Convert.ToInt32(SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_EveryDay);
//                                break;
                            
//                        }

//                    }

//                }
//                if (oSchedule.ScheduleFrequency == "Monthly")
//                {
//                    SQLSchedule.Schedule.FrequencyType = SQLDMO.SQLDMO_FREQUENCY_TYPE.SQLDMOFreq_Monthly;
//                    SQLSchedule.Schedule.FrequencyInterval =oSchedule.DayOfMonth;
//                    SQLSchedule.Schedule.FrequencyRecurrenceFactor = oSchedule.MonthFrequency;
//                }

//                SQLSchedule.Schedule.ActiveStartDate = oSchedule.StartDate;
//                SQLSchedule.Schedule.ActiveEndDate = oSchedule.EndDate;
//                SQLSchedule.Schedule.ActiveStartTimeOfDay = oSchedule.ScheduleTime;
//                //Active end time of day set to max value.
//                SQLSchedule.Schedule.ActiveEndTimeOfDay = 235959;
//                //  add the schedule to the Job
//                SQLJob.BeginAlter();
//                SQLJob.JobSchedules.Add(SQLSchedule);
//                SQLJob.DoAlter();
//                return true;
               
//            }
//            catch (Exception err)
//            {
//                MessageBox.Show(err.ToString());
//                return false;
//            }
//        }



    }//end clsBackUp


    /// <summary>
    /// Class used to get the Schedule information from the frmBackUp form
    /// Gives us a Schedule object containing all data needed for the sql schedule as per the 
    /// user request on the form.
    /// </summary>

    public class ScheduleInfo
    {
        private string   _sScheduleName;
        private string   _sScheduleFrequency; 
        private int      _dtStartDate;
        private int      _dtEndDate;
        private int      _dtScheduleTime;
        private string   _asWeekdays;
        private int      _nDayOfMonth;
        private int      _nWeekFrequency;
        private int      _nMonthFrequency;
        private int      _nScheduleActiveEndTime;
        private string   _bBackUpType;
        private int      _nDailyFreqency;
        

        public string ScheduleName
        {
            get { return _sScheduleName;}
            set { _sScheduleName = value;}
        }

        public string ScheduleFrequency
        {
            get { return _sScheduleFrequency; }
            set { _sScheduleFrequency = value; }
        }

       
        public string BackUpType
        {
            get {return _bBackUpType; }
            set {_bBackUpType = value;}
        }
        public int StartDate
        {
            get { return _dtStartDate; }
            set { _dtStartDate = value; }
        }
        public int EndDate
        {
            get { return _dtEndDate; }
            set { _dtEndDate = value; }
        }
        public int ScheduleTime
        {
            get { return _dtScheduleTime; }
            set { _dtScheduleTime = value; }
        }
        public string  WeekDays
        {
            get { return _asWeekdays; }
            set { _asWeekdays = value; }
        }
        public int DayOfMonth
        {
            get { return _nDayOfMonth; }
            set { _nDayOfMonth = value; }
        }
        public int WeekFrequency
        {
            get { return _nWeekFrequency; }
            set { _nWeekFrequency = value; }
        }
        public int MonthFrequency
        {
            get { return _nMonthFrequency; }
            set { _nMonthFrequency = value; }
        }
        public int ScheduleActiveEndTime
        {
            get { return _nScheduleActiveEndTime; }
            set { _nScheduleActiveEndTime = value;}
        }
        public int DailyFrequecy
        {
            get { return _nDailyFreqency; }
            set { _nDailyFreqency = value; }
        }

    } // end class ScheduleInfo

}//end namespace gloSecurity
