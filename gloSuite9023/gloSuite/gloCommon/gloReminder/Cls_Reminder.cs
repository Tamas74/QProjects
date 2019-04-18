using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Collections;


namespace gloReminder
{
    public enum ReferenceType
    {
        Appointment = 1,
        Task = 2
    }

    public class Reminder
    {

        #region Declarations

        private string _databaseconnectionstring = "";
        private Int64 _clinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        //private string _messageBoxCaption = "gloPM";
        private string _messageBoxCaption = String.Empty;


        private Int64 _reminderID = 0;
        private Int64 _reminderDetailID = 0;
        private string _description = "";
        private string _place = "";
        private DateTime _reminderStartDate;
        private DateTime _reminderStartTime;
        private DateTime _reminderEndDate;
        private DateTime _reminderEndTime;
        private DateTime _reminderDate;
        private DateTime _reminderTime;
        private Int64 _ownerID;
        private bool _isDismissed = false;
        private Int64 _referanceID = 0;
        private ReferenceType _referenceType;
        private Int64 _reminderInterval = 0;

        #region properties

        public Int64 ReminderID
        {
            get { return _reminderID; }
            set { _reminderID = value; }
        }

        public Int64 ReminderDetailID
        {
            get { return _reminderDetailID; }
            set { _reminderDetailID = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Place
        {
            get { return _place; }
            set { _place = value; }
        }

        public DateTime ReminderStartDate
        {
            get { return _reminderStartDate; }
            set { _reminderStartDate = value; }
        }

        public DateTime ReminderStartTime
        {
            get { return _reminderStartTime; }
            set { _reminderStartTime = value; }
        }

        public DateTime ReminderEndDate
        {
            get { return _reminderEndDate; }
            set { _reminderEndDate = value; }
        }

        public DateTime ReminderEndTime
        {
            get { return _reminderEndTime; }
            set { _reminderEndTime = value; }
        }

        public DateTime ReminderDate
        {
            get { return _reminderDate; }
            set { _reminderDate = value; }
        }

        public DateTime ReminderTime
        {
            get { return _reminderTime; }
            set { _reminderTime = value; }
        }

        public Int64 OwnerID
        {
            get { return _ownerID; }
            set { _ownerID = value; }
        }

        public bool IsDismissed
        {
            get { return _isDismissed; }
            set { _isDismissed = value; }
        }

        public Int64 ReferanceID
        {
            get { return _referanceID; }
            set { _referanceID = value; }
        }

        public ReferenceType ReferenceType
        {
            get { return _referenceType; }
            set { _referenceType = value; }
        }

        public Int64 ReminderIntervalMinutes
        {
            get { return _reminderInterval; }
            set { _reminderInterval = value; }
        }

        public Int64 ClinicID
        {
            get { return _clinicID; }
            set { _clinicID = value; }
        }

        #endregion


        #endregion

        #region "Constructor & Distructor"

        public Reminder()
        {
            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _clinicID = Convert.ToInt64(appSettings["ClinicID"]);
                }
                else { _clinicID = 0; }
            }
            else
            { _clinicID = 0; }
            //Sandip Darade  20090428
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }
            #endregion
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

        ~Reminder()
        {
            Dispose(false);
        }

        #endregion

        public void Add(Reminder oReminder)
        {
           // Int64 _result = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object _intresult = 0;

            try
            {

                oDB.Connect(false);

                oDBParameters.Add("@ReminderID", oReminder.ReminderID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@Description", oReminder.Description, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Place", oReminder.Place, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ReminderStartDate", gloDateMaster.gloDate.DateAsNumber(oReminder.ReminderStartDate.ToShortDateString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ReminderStartTime", gloDateMaster.gloTime.TimeAsNumber(oReminder.ReminderStartTime.ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ReminderEndDate", gloDateMaster.gloDate.DateAsNumber(oReminder.ReminderEndDate.ToShortDateString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ReminderEndTime", gloDateMaster.gloTime.TimeAsNumber(oReminder.ReminderEndTime.ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@OwnerID", oReminder.OwnerID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@IsDismissed", oReminder.IsDismissed, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@ReferenceID", oReminder.ReferanceID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@RefrenceType", oReminder.ReferenceType.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ClinicID", _clinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                oDB.Execute("RM_INUP_Reminder_MST", oDBParameters, out _intresult);

                if (Convert.ToInt64(_intresult) > 0)
                {
                    oDBParameters.Clear();
                    oDBParameters.Add("@ReminderID", Convert.ToInt64(_intresult), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@ReminderDate", gloDateMaster.gloDate.DateAsNumber(oReminder.ReminderDate.ToShortDateString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@ReminderTime", gloDateMaster.gloTime.TimeAsNumber(oReminder.ReminderTime.ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@ReminderInterval", oReminder.ReminderIntervalMinutes, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@IsFinished", false, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@ClinicID", _clinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                    oDB.Execute("RM_INUP_Reminder_DTL", oDBParameters);
                }

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                oDB.Disconnect();

                oDB.Dispose();
                oDB = null;

                oDBParameters.Dispose();
                oDBParameters = null;

                _intresult = null;
            }
        }

        public void DismissReminder(long ReminderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            //DataTable dtResult = new DataTable();

            try
            {
                oDB.Connect(false);

                string _sqlQuery = "UPDATE RM_Reminder_MST SET bIsDismissed = 1 WHERE nReminderID = " + ReminderID + " AND nClinicID = " + _clinicID;
                oDB.Execute_Query(_sqlQuery);
                _sqlQuery = null;

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

                oDBParameters.Dispose();
                oDBParameters = null;
            }
        }

        public void SnoozeReminder(long ReminderID, long ReminderDetailID, int Minutes)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            //DataTable dtResult = new DataTable();

            try
            {
                oDB.Connect(false);

                string _sqlQuery = "UPDATE RM_Reminder_DTL SET bIsFinished = 1 WHERE nReminderID = " + ReminderID + " AND nReminderDetailID = " + ReminderDetailID + " AND nClinicID = " + _clinicID;
                oDB.Execute_Query(_sqlQuery);
                _sqlQuery = null;

                DateTime ReminderDate = DateTime.Now;
                DateTime ReminderTime = DateTime.Now.AddMinutes(Minutes);

                oDBParameters.Add("@ReminderID", ReminderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ReminderDate", gloDateMaster.gloDate.DateAsNumber(ReminderTime.ToShortDateString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ReminderTime", gloDateMaster.gloTime.TimeAsNumber(ReminderTime.ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ReminderInterval", Minutes, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@IsFinished", false, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@ClinicID", _clinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                oDB.Execute("RM_INUP_Reminder_DTL", oDBParameters);

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

                oDBParameters.Dispose();
                oDBParameters = null;
            }
        }

        public void MarkAsFinished(long ReminderID, long ReminderDetailID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            //DataTable dtResult = new DataTable();

            try
            {
                oDB.Connect(false);

                string _sqlQuery = "UPDATE RM_Reminder_DTL SET bIsFinished = 1 WHERE nReminderID = " + ReminderID + " AND nReminderDetailID = " + ReminderDetailID + " AND nClinicID = " + _clinicID;
                oDB.Execute_Query(_sqlQuery);
                _sqlQuery = null;

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
        }

        //Activate Task Reminder when user accepts the task, 
        //reminder is inactive when it Task is assigned to user
        public void ActivateTaskReminder(long UserId, long OldTaskID, long NewTaskID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //DataTable dtResult = new DataTable();
            try
            {
                oDB.Connect(false);

                string _sqlQuery = "UPDATE RM_Reminder_MST SET nReferenceID = " + NewTaskID + ", bIsDismissed  = 0 WHERE  nOwnerID = " + UserId + " AND nReferenceID = " + OldTaskID + " AND nClinicID = " + _clinicID;
                oDB.Execute_Query(_sqlQuery);
                _sqlQuery = null;

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
        }

        //If task is modified then modify its Reminder
        public void ModifyTaskReminder(Reminder oReminder)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtResult = new DataTable();
            try
            {
                oDB.Connect(false);

                string _sqlQuery = "SELECT  nReminderID FROM RM_Reminder_MST WHERE  nOwnerID = " + oReminder.OwnerID + "  AND nRefrenceType = " + ReferenceType.Task.GetHashCode() + " AND nReferenceID = " + oReminder.ReferanceID + " AND nClinicID = " + _clinicID;
                oDB.Retrive_Query(_sqlQuery,out dtResult);

                if (dtResult.Rows.Count > 0)
                {
                    _sqlQuery = "DELETE FROM RM_Reminder_MST WHERE nReminderID = " + Convert.ToInt64(dtResult.Rows[0]["nReminderID"]);
                    oDB.Execute_Query(_sqlQuery);

                    _sqlQuery = "DELETE FROM RM_Reminder_DTL WHERE nReminderID = " + Convert.ToInt64(dtResult.Rows[0]["nReminderID"]);
                    oDB.Execute_Query(_sqlQuery);
                }

                _sqlQuery = null;

                this.Add(oReminder);
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

                dtResult.Dispose();
                dtResult = null;
            }
        }

        public Reminder GetTaskReminder(long TaskID,long UserID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Reminder oReminder;
            DataTable dt = new DataTable(); 
            try
            {
                oDB.Connect(false);
                string _SqlQuery = "";

                _SqlQuery = "SELECT  RM_Reminder_MST.nReminderID, RM_Reminder_MST.sDescription, RM_Reminder_MST.sPlace, RM_Reminder_MST.dtReminderStartDate, " +
                            " RM_Reminder_MST.dtReminderStartTime, RM_Reminder_MST.dtReminderEndDate, RM_Reminder_MST.dtReminderEndTime, RM_Reminder_DTL.dtReminderDate, " +
                            " RM_Reminder_DTL.dtReminderTime, RM_Reminder_DTL.dtReminderInterval FROM RM_Reminder_MST INNER JOIN " +
                            " RM_Reminder_DTL ON RM_Reminder_MST.nReminderID = RM_Reminder_DTL.nReminderID " +
                            " WHERE (RM_Reminder_MST.bIsDismissed = 0) AND (RM_Reminder_DTL.bIsFinished = 0) AND (RM_Reminder_MST.nRefrenceType = 2) AND " + 
                            " (RM_Reminder_MST.nOwnerID = " + UserID + ") AND (RM_Reminder_MST.nReferenceID = " + TaskID  + ")"; 
                
                if(_clinicID > 0)
                     _SqlQuery +=  " AND (RM_Reminder_MST.nClinicID = " + _clinicID +")";

                oDB.Retrive_Query(_SqlQuery, out dt);

                _SqlQuery = null;

                if (dt.Rows.Count > 0)
                {
                    oReminder = new Reminder();
                    oReminder.OwnerID = UserID;
                    oReminder.IsDismissed = false;
                    oReminder.ClinicID = _clinicID;
                    oReminder.Description = Convert.ToString(dt.Rows[0]["sDescription"]);
                    oReminder.Place =  Convert.ToString(dt.Rows[0]["sPlace"]);
                    oReminder.ReferenceType = ReferenceType.Task;
                    oReminder.ReminderDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[0]["dtReminderDate"]));    
                    oReminder.ReminderTime = gloDateMaster.gloTime.TimeAsDateTime(oReminder.ReminderDate,Convert.ToInt32(dt.Rows[0]["dtReminderTime"]));      
                    oReminder.ReminderStartDate =  gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[0]["dtReminderStartDate"]));    
                    oReminder.ReminderEndDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[0]["dtReminderEndDate"]));  
                    oReminder.ReminderIntervalMinutes =  Convert.ToInt64(dt.Rows[0]["dtReminderInterval"]);
                     oDB.Disconnect(); 
                    return oReminder; 
                }
                else
                {
                    oDB.Disconnect(); 
                    return null; 
                }
               
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                oDB.Disconnect(); 
                dbex.ERROR_Log(dbex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null)
                { 
                    oDB.Dispose();
                    oDB = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            return null;
        }

        public void DeleteReminder(Reminder oReminder)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtResult = new DataTable();
            try
            {
                oDB.Connect(false);

                string _sqlQuery = "SELECT  nReminderID FROM RM_Reminder_MST WHERE  nOwnerID = " + oReminder.OwnerID + "  AND nRefrenceType = " + ReferenceType.Task.GetHashCode() + " AND nReferenceID = " + oReminder.ReferanceID + " AND nClinicID = " + _clinicID;
                oDB.Retrive_Query(_sqlQuery, out dtResult);

                if (dtResult.Rows.Count > 0)
                {
                    _sqlQuery = "DELETE FROM RM_Reminder_MST WHERE nReminderID = " + Convert.ToInt64(dtResult.Rows[0]["nReminderID"]);
                    oDB.Execute_Query(_sqlQuery);

                    _sqlQuery = "DELETE FROM RM_Reminder_DTL WHERE nReminderID = " + Convert.ToInt64(dtResult.Rows[0]["nReminderID"]);
                    oDB.Execute_Query(_sqlQuery);
                }

                _sqlQuery = null;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

                dtResult.Dispose();
                dtResult = null;
            }
        }
    }

    public class Reminders : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public Reminders()
        {
            _innerlist = new ArrayList();

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


        ~Reminders()
        {
            Dispose(false);
        }
        #endregion


        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(Reminder item)
        {
            _innerlist.Add(item);
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }

        public void Clear()
        {
            _innerlist.Clear();
        }

        public Reminder this[int index]
        {
            get
            { return (Reminder)_innerlist[index]; }
        }

        public bool Contains(Reminder item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(Reminder item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(Reminder[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    public class gloReminder
    {
        private System.Windows.Forms.Timer _timerReminder;
        private string _databaseconnectionstring = "";
        private Int64 _clinicID = 0;
        private Int64 _userID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        //private string _messageBoxCaption = "gloPM";
        private string _messageBoxCaption = String.Empty;
        
        //dhruv Event Argument added
        public class OpenItemClickArgs : EventArgs
        {
            public Int64 TaskID = 0;
        }

        // SUDHIR 20091222 // CUSTOM EVENT TO OPEN TASK, THIS EVENT WILL TRIGER METHOD OF EMR DashBoard //
        public delegate void OnOpenItemClick(object sender, EventArgs e, gloReminder.OpenItemClickArgs e2);
        public event OnOpenItemClick On_OpenItemClick;

        #region "Constructor & Distructor"

        public gloReminder()
        {
            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
            
            
            _timerReminder = new System.Windows.Forms.Timer();
            _timerReminder.Interval = (1000 * 30);
            _timerReminder.Tick += new EventHandler(TimerReminder_Tick);

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _clinicID = Convert.ToInt64(appSettings["ClinicID"]);
                }
                else { _clinicID = 0; }
            }
            else
            { _clinicID = 0; }


            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _userID = Convert.ToInt64(appSettings["UserID"]);
                }
                else { _userID = 0; }
            }
            else
            { _userID = 0; }
            //Sandip Darade  20090428
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }
            #endregion
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
                    //Developer:Pradeep/Date:02/22/2011/Bug ID/PRD Name/Salesforce Case: 21253/Reason:form opening twice
                    if ((this.On_OpenItemClick != null))
                    {
                        this.On_OpenItemClick=null ;
                    }
                    if (_timerReminder != null)
                    {
                        _timerReminder.Tick -= new System.EventHandler(this.TimerReminder_Tick);
                        _timerReminder.Dispose();
                        _timerReminder = null;
                    }

                }
            }
            disposed = true;
        }

        ~gloReminder()
        {
            Dispose(false);
        }

        #endregion

        //Start reminder timer (start reminder process)
        public void Start()
        {
            try
            {
                if (_timerReminder.Enabled == false)
                {
                    _timerReminder.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Stop  reminder timer (stop reminder process)
        public void Stop()
        {
            try
            {
                if (_timerReminder.Enabled == true)
                {
                    _timerReminder.Stop();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void TimerReminder_Tick(object sender, EventArgs e)
        {
            try
            {
                _timerReminder.Enabled = false;
                //Thread t = new Thread(new ThreadStart(GiveReminder));                    
                //t.Start();
                //t.Join();   
                GiveReminder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                _timerReminder.Enabled = true;
            }
        }
      public void GiveReminder()
      {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            gloDatabaseLayer.DBParameters oDBParam = new gloDatabaseLayer.DBParameters();


            DataTable dt = new DataTable();

            try
            {
                //Get currunt login userID 
                if (appSettings["UserID"] != null)
                {
                    if (appSettings["UserID"] != "")
                    {
                        _userID = Convert.ToInt64(appSettings["UserID"]);
                    }
                    else { _userID = 0; }
                }
                else
                { _userID = 0; }

                oDB.Connect(false);

                oDBParam.Add("@UserID", _userID, ParameterDirection.Input , SqlDbType.BigInt);
                oDBParam.Add("@ClinicID", _clinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParam.Add("@ReminderTime", gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToShortTimeString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParam.Add("@ReminderDate", gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("GSP_GetReminder", oDBParam, out dt);
                Reminders oReminders = new Reminders();
                Reminder oRem;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    oRem = new Reminder();
                    oRem.ClinicID = _clinicID;
                    oRem.Description = Convert.ToString(dt.Rows[i]["sDescription"]);
                    oRem.IsDismissed = false;
                    oRem.OwnerID = _userID;
                    oRem.Place = Convert.ToString(dt.Rows[i]["sPlace"]);
                    oRem.ReferanceID = Convert.ToInt64(dt.Rows[i]["nReferenceID"]);
                    oRem.ReferenceType = (ReferenceType)Convert.ToInt32(dt.Rows[i]["nRefrenceType"]);
                    oRem.ReminderDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[i]["dtReminderDate"]));
                    oRem.ReminderEndDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[i]["dtReminderEndDate"]));
                    oRem.ReminderEndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["dtReminderEndTime"]));
                    oRem.ReminderID = Convert.ToInt64(dt.Rows[i]["nReminderID"]);
                    oRem.ReminderDetailID = Convert.ToInt64(dt.Rows[i]["nReminderDetailID"]);
                    oRem.ReminderIntervalMinutes = Convert.ToInt64(dt.Rows[i]["dtReminderInterval"]);
                    oRem.ReminderStartDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[i]["dtReminderStartDate"]));
                    oRem.ReminderStartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["dtReminderStartTime"]));
                    oRem.ReminderTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["dtReminderTime"]));
                    oReminders.Add(oRem);
                    oRem.Dispose();
                    oRem = null;
                }

                if (oReminders.Count > 0)
                {
                    frmReminder ofrm = frmReminder.GetInstance();
                    //Developer:Pradeep/Date:02/22/2011/Bug ID/PRD Name/Salesforce Case: 21253/Reason:form opening twice
                    try
                    {
                        ofrm.On_OpenItemClick -= new frmReminder.OnOpenItemClick(ofrm_On_OpenItemClick);
                    }
                    catch
                    {
                        //bypassing the code if any errror occurs
                    }
                    //dhruv calling  event to open item
                    ofrm.On_OpenItemClick += new frmReminder.OnOpenItemClick(ofrm_On_OpenItemClick);
                    ofrm.Reminders = oReminders;
                    ofrm.StartPosition = FormStartPosition.CenterScreen;
                    ofrm.WindowState = FormWindowState.Normal;
                    ofrm.Show();
                    ofrm.FillReminders();
                }

                oDB.Disconnect();

                oReminders.Dispose();
                oReminders = null;

            }
                
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null) 
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (oDBParam != null) 
                {
                    oDBParam.Dispose();
                    oDBParam = null;
                }

                if (dt != null) 
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }
        //dhruv calling open item
        void ofrm_On_OpenItemClick(object sender, EventArgs e, gloReminder.OpenItemClickArgs e2)
        {
            On_OpenItemClick(sender, e, e2);
        }

        
       
    }
}
