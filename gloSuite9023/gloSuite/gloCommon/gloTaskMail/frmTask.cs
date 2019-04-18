using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloReminder;
using System.Reflection;
using Microsoft.Win32;
using Microsoft.VisualBasic;
using gloAuditTrail;
using gloCommon;
using System.Collections;
using System.IO;
using System.Linq;
using gloTaskMail.Properties;

namespace gloTaskMail
{
    public partial class frmTask : Form
    {

        #region "Declarations "

        private string _databaseconnectionstring = "";
        //private string _messageBoxCaption = "gloPM";
        private string _messageBoxCaption = String.Empty;

        private gloListControl.gloListControl oListControl;
        private gloListControl.gloListControl oListUsers;
        //Added by Mayuri:Case No:GLO2008-0001606-For Retrieving Patients used PatientListcontrol
        private gloPatient.PatientListControl oPatientListControl;
        private Int64 _taskID = 0;

        private String _sFaxTiffFileName = "";
        private Int64 _nReferenceID1 = 0;
        private Int64 _nReferenceID2 = 0;
        public TaskType _TaskType = TaskType.Task;
        //private String _Notes = "";
        private Int64 _dateCompleted = 0;
        private Int64 _userID = 0;
        private Int64 _defaultuserID = 0;//added in 6060 for provider user task assignment
        private Int64 _ClinicID = 1;
        private bool _IsOpenfromView = false;
        private bool _IsEMREnable = false;
        private bool _IsUpdated = false;
        private bool _IsTaskClose = false;
        private bool _IsReconcile = false;
        gloGeneralItem.gloItems ToList;

        private Int64 _nProviderID = 0;
        private Int64 _nDrugProviderID = 0; //Added Rahul on 20101025
        private Int64 _nIBPAckToken = 0;
        private Int64 _nPatientID = 0;
        private DateTime _dtStartDate = DateTime.Now;
        private DateTime _dtDueDate = DateTime.Now;
        public string _UserName = String.Empty;

        //Added by madan on 20100614
        // This field is added if task need to be of type to show view form after accepting it...
        // This tasks are generated from View form of labs only ...
        private bool _isTaskforPlaceLab = false;
        private bool _isbool = false;
        public string _tskSubjectForExternalOrder = "Place Lab Order";
        private string _LabTestList = string.Empty; //this is used to show test list for lab in description
        public string strPriority = "Low";

        //end madan.. Changes...


        //Added by Abhijeet on 20100625
        // Declare a variable which save an task ID which is created newly
        private Int64 _nTaskInsertedID = 0;
        // End of changes by Abhijeet on 20100625

        //code addeed for track task -pradeep 20100721
        public string _taskuser_id;
        private Boolean _TrackTask;
        public Boolean _SmartTask = false;
        public Boolean _ReminderTask = false;//Developer:Pradeep/Date:01/09/2012/Bug ID:18512/Reason:from textbox was showing blank
        public string _sNotesExt = string.Empty;
        private Int64 _ntaskGroupID = 0;
        public static Boolean _IsCompleteAllTask = false;
        private Boolean _HasRightToCompleteTaskForAllUsers = false;
        public Boolean _IsAcceptClick = false;////Developer:Pradeep/Date:02/22/2011/Bug ID/PRD Name/Salesforce Case: 21248/Reason:creating multiple task 
        private Boolean _IsTaskFromIntuitMessage = false;
        private String _sSubject;
        private String _sMessageBody;
        private Int16 _nAttachments;
        private string _sButtonClick = "";
        private Boolean _isTaskForwardedFromIntuitMessage = false;
        private DataTable dtTask = null;
        private bool blnChangeTask = false;

        public Boolean bIncludeAllUsers = false;


        private int Col_Select = 0;
        private int Col_DueDate = 1;

        private int Col_Subject = 2;
        private int Col_TaskID = 3;
        private int Col_No = 4;
        private int Col_Status = 5;
        private int Col_PercCompleted = 6;
        private int Col_Priority = 7;
        private int Col_TaskDate = 8;
        private int Col_PatientID = 9;
        private int Col_AssignStatus = 10;
        private int Col_PatientCode = 11;
        private int Col_PatientName = 12;
        private int Col_nProviderID = 13;
        private int Col_nCategoryID = 14;
        private int Col_ProviderName = 15;
        private int Col_TotalCount = 16;
        private int Col_TaskGroupID = 17;
        private int Col_SSN = 18;
        private int Col_DateofBirth = 19;
        private  ImageList imgList_Common = new ImageList();
        public  bool blnOpnShowDocs = false;   //added flag to check dms window is opened or not

        private Int16 status = 0;

        #endregion "Declarations "

        #region " Delegates "

        public delegate void OnTaskChange(object sender, EventArgs e, TaskChangeEventArg e2,object objfrmtask =null);//default parameter added for task incident
        public event OnTaskChange OnTask_Change;

        public delegate void PatientPaymentHandler(object sender, EventArgs e, TaskChangeEventArg e2);
        public event PatientPaymentHandler OnPatientPaymentClicked;

        //sanjog 
        public bool OpenEmdeon = false;
        public gloTaskMail.TaskChangeEventArg e2Task = new gloTaskMail.TaskChangeEventArg();








        #endregion

        public enum StatusType
        {
            Completed = 3,
            Deferred = 4,
            InProgress = 2,
            OnHold = 5,
            NotStarted = 1,
            Deleted = 6 ////added by pradeep 20101006 -to show status of task deleted
        }

        #region " Property Procedures "
        long _TaskAssigntoID=0;
        public long TaskAssigntoID
        {
            get{ return _TaskAssigntoID; }
            set{ _TaskAssigntoID=value; }
        }
        //Added by Abhijeet on 20100625
        // Declare properties for accessing variable which save ID of new generated task.
        public long TaskInsertedID
        {
            get { return _nTaskInsertedID; }
            set { _nTaskInsertedID = value; }
        }
        // End of changes by Abhijeet on 20100625
        public long TaskGroupID
        {
            get { return _ntaskGroupID; }
            set { _ntaskGroupID = value; }
        }
        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 TaskID
        {
            get { return _taskID; }
            set { _taskID = value; }
        }
        public string FaxTiffFileName
        {
            get { return _sFaxTiffFileName; }
            set { _sFaxTiffFileName = value; }
        }
        public string sNoteExt
        {
            get { return _sNotesExt; }
            set { _sNotesExt = value; }
        }
        public Int64 ReferenceID
        {
            get { return _nReferenceID1; }
            set { _nReferenceID1 = value; }
        }
        public Int64 DateCompleted
        {
            get { return _dateCompleted; }
            set { _dateCompleted = value; }
        }

        public Int64 UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public bool IsOpenfromView
        {
            get { return _IsOpenfromView; }
            set { _IsOpenfromView = value; }
        }

        public bool IsEMREnable
        {
            get { return _IsEMREnable; }
            set { _IsEMREnable = value; }
        }

        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        public Int64 ProviderID
        {
            get { return _nProviderID; }
            set { _nProviderID = value; }
        }

        public DateTime StartDate
        {
            get { return _dtStartDate; }
            set { _dtStartDate = value; }
        }

        public DateTime DueDate
        {
            get { return _dtDueDate; }
            set { _dtDueDate = value; }
        }

        //Added my madan on 20100622-- for labs
        public string LabTestList
        {
            get { return _LabTestList; }
            set { _LabTestList = value; }
        }
        //end madan

        //Added by Rahul on 20101025
        public Int64 DrugProviderID
        {
            get { return _nDrugProviderID; }
            set { _nDrugProviderID = value; }
        }
        public Int64 IBPAckToken
        {
            get { return _nIBPAckToken; }
            set { _nIBPAckToken = value; }
        }

        private Int64 _DMSUser = 0;
        public Int64 DMSUser
        {
            get { return _DMSUser; }
            set { _DMSUser = value; }
        }

        private String _DMSDescriptiont = "";
        public String DMSDescription
        {
            get { return _DMSDescriptiont; }
            set { _DMSDescriptiont = value; }
        }


        #endregion " Property Procedures "

        #region "Constructor"
        //Uncomment Rahul on 20101025.
        public frmTask()
        {
            InitializeComponent();
        }

        //public frmTask(string DatabaseConnectionString)
        //{
        //    _databaseconnectionstring = DatabaseConnectionString;
        //    InitializeComponent();
        //}

        public frmTask(string DatabaseConnectionString, Int64 TaskID)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            _taskID = TaskID;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _userID = Convert.ToInt64(appSettings["UserID"]);

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
            //Sandip Darade  200100413
            #region " Retrieve UserName from AppSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
                else
                {
                    _UserName = "";
                }
            }
            else
            { _UserName = ""; }

            #endregion
            InitializeComponent();
        }

        // pradeep godse 20100721 for tracktasks

        public frmTask(string DatabaseConnectionString, Int64 TaskID, Boolean track_Task, string AssignTo_ID)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            _taskID = TaskID;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _userID = Convert.ToInt64(appSettings["UserID"]);
            _taskuser_id = AssignTo_ID;
            _TrackTask = track_Task;


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
            //Sandip Darade  200100413
            #region " Retrieve UserName from AppSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
                else
                {
                    _UserName = "";
                }
            }
            else
            { _UserName = ""; }

            #endregion
            InitializeComponent();
        }

        public frmTask(string DatabaseConnectionString, Int64 TaskID, Int64 ReferenceID1, Int64 ReferenceID2, TaskType Task_Type = TaskType.Task)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            _taskID = TaskID;
            _TaskType = Task_Type;

          
            ReferenceID = ReferenceID1;
            _nReferenceID1 = ReferenceID;
            //_nReferenceID1 = ReferenceID1;
            _nReferenceID2 = ReferenceID2;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _userID = Convert.ToInt64(appSettings["UserID"]);
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
            //Sandip Darade  200100413
            #region " Retrieve UserName from AppSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
                else
                {
                    _UserName = "";
                }
            }
            else
            { _UserName = ""; }

            #endregion
            
            InitializeComponent();
            

        }

        //Added by madan-- on 20100614--this constructor is used only in labs....
        // To create tasks for user to place an order.
        //Method modified  by madan on 20100726 by adding referenceid parameter for showing task in exam module.
        //method modified by madan on 20100731 by adding task type parameter to constructtor
        public frmTask(string DatabaseConnectionString, Int64 TaskID, Int64 DefaultUserID, bool IsTaskToPlaceLab, Int64 ReferenceID1, TaskType LabTaskType)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            _taskID = TaskID;
            _TaskType = LabTaskType;
            _isTaskforPlaceLab = IsTaskToPlaceLab;
            _nReferenceID1 = ReferenceID1;

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _userID = Convert.ToInt64(appSettings["UserID"]);
            //code added to fix bug 17415 
            if (DefaultUserID > 0)
            {
                _defaultuserID = DefaultUserID;
            }
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
            //Sandip Darade  200100413
            #region " Retrieve UserName from AppSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
                else
                {
                    _UserName = "";
                }
            }
            else
            { _UserName = ""; }

            #endregion
            InitializeComponent();
        }

        public frmTask(string DatabaseConnectionString, Int64 TaskID, bool Isbool)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            _taskID = TaskID;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _userID = Convert.ToInt64(appSettings["UserID"]);
            _isbool = Isbool;
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
            //Sandip Darade  200100413
            #region " Retrieve UserName from AppSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
                else
                {
                    _UserName = "";
                }
            }
            else
            { _UserName = ""; }

            #endregion
            InitializeComponent();
        }
        //added new constructor for create task from intuit message
        public frmTask(string DatabaseConnectionString, Int64 TaskID, Int64 ReferenceID1, String Subject, String MessageBody, Int16 noOfAttachments, Boolean IsTaskFromIntuitMessage)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            _taskID = TaskID;
            _TaskType = TaskType.IntuitSecureMessageTask;
            _nReferenceID1 = ReferenceID1;
            _sSubject = Subject;
            _sMessageBody = MessageBody;
            _nAttachments = noOfAttachments;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _userID = Convert.ToInt64(appSettings["UserID"]);
            _IsTaskFromIntuitMessage = IsTaskFromIntuitMessage;
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
            //Sandip Darade  200100413
            #region " Retrieve UserName from AppSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
                else
                {
                    _UserName = "";
                }
            }
            else
            { _UserName = ""; }

            #endregion
            InitializeComponent();
        }

        //End madan.///

        #endregion "Constructor"

        private void frmTask_Load(object sender, EventArgs e)
        {
            //gloC1FlexStyle.Style(C1PatTask, false);

            //Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            //Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            //tom.SetTabOrder(scheme);

            try
            {
                if (_taskID <= 0)
                {
                    btnreply.Enabled = false;
                    btnTo.Enabled = true;
                    btn_ToDel.Enabled = true; 
                }
                imgList_Common.Images.Add(Resources.High_PriorityRed);
                 imgList_Common.Images.Add(Resources.Tommorow);
                imgList_Common.Images.Add(Resources.Low_Priority);
                uiPnlTaskDetails.Text = "Selected Task";
                uiPnlPatientTask.Text = "All Patient Tasks";
                tsb_MergeOrder.Visible = false;
                DetachControlEvents();
                C1PatTask.RowColChange -= C1PatTask_RowColChange;
                C1PatTask.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

                if (TaskID == 0 && _TaskType == TaskType.DMS)
                {
                    txtSubject.Text = "DMS DOC Received";
                    rtxtDescription.Text = DMSDescription;
                    if (DMSUser > 0)
                    {
                        _defaultuserID = DMSUser;
                    }
                }

                fill_Status();
                fill_Priority();
                fill_FollowUp();

                fill_Owner();

               
                //Added by madan on 20100614
                if (_isTaskforPlaceLab == true)
                {
                    txtSubject.Text = _tskSubjectForExternalOrder;//Lab Task Assignment
                    cmbPriority.Text = strPriority;
                    //commented by madan on 20100731
                    // _TaskType = TaskType.LabOrder;
                    //end comment.
                    rtxtDescription.Text = _LabTestList;
                }
                if (_IsTaskFromIntuitMessage == true)
                {


                    txtSubject.Text = _sSubject;
                    cmbPriority.Text = "Normal";
                    StringBuilder strDescription = new StringBuilder("Subject: ");
                    strDescription.Append(_sSubject);

                    if (_nAttachments > 0)
                    {
                        strDescription.Append("This message has " + _nAttachments.ToString() + " attachments.  Choose view message to access attachments ");
                    }
                    if (_sMessageBody.Length > 900)
                    {
                        strDescription.Append(Environment.NewLine + _sMessageBody.Substring(0, 900) + "....");
                    }
                    else
                    {
                        strDescription.Append(Environment.NewLine + _sMessageBody);
                    }
                    //strDescription.Append(Environment.NewLine + _sMessageBody);
                    rtxtDescription.Text = strDescription.ToString();
                }

                //End madan changes
                if (_nPatientID > 0)
                {
                    gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                    txtPatient.Text = ogloPatient.GetPatientName(_nPatientID);
                    txtPatient.Tag = _nPatientID;
                    ogloPatient.Dispose();
                    ogloPatient = null;
                }

                if (_nProviderID > 0)
                {
                    gloAppointmentBook.Books.Resource ogloProvider = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                    txtProvider.Text = ogloProvider.GetProviderName(_nProviderID);
                    txtProvider.Tag = _nProviderID;
                    ogloProvider.Dispose();
                    ogloProvider = null;
                }
                dtp_StartDate.Value = _dtStartDate;
                dtp_EndDate.Value = _dtDueDate;
                dtpReminderDate.Value = _dtStartDate;
                dtpReminderTime.Value = _dtStartDate;
                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
                _HasRightToCompleteTaskForAllUsers = oSetting.GetSettingUserSpecificRight("Complete Task for all Users", _userID, _ClinicID);

                oSetting.Dispose();
                oSetting = null;

                ChkCompleteTaskforallUsers.Visible = false;
                _IsCompleteAllTask = false;
                pnl_AssignTo.Visible = true;
                gloTask objglotsk = new gloTask(_databaseconnectionstring);
                if (objglotsk != null)
                {
                    Byte[] oBytesArry = (byte[])objglotsk.LoadDisplaySettings(UserID, System.Environment.MachineName);
                    MemoryStream memStream = null;
                    if (oBytesArry != null)
                    {
                        memStream = new MemoryStream(oBytesArry);

                        uiPanelManager1.LoadLayoutFile(memStream);
                    }
                    if (oBytesArry != null)
                    {
                        oBytesArry = null;
                    }
                    if (memStream != null)
                    {
                        memStream.Close();
                        memStream.Dispose();
                        memStream = null;

                    }

                    objglotsk.Dispose();
                    objglotsk = null;
                }
                HideToolStripButtons();
                if (TaskID > 0)
                {
                    fillUserTask();
                    ShowTask(TaskID);
                    if (dtTask != null)
                    {
                        DataRow[] dr = dtTask.Select("Taskid=" + TaskID + "");
                        if (dr.Length > 0)
                        {
                            int index = dtTask.Rows.IndexOf(dr[0]);
                            // C1PatTask.RowSel = index;
                            C1PatTask.Select((index + 1), 0);
                            if (Convert.ToString(C1PatTask.GetData(index + 1, 5)) == "Not Yet Accepted")
                            {
                                pnlFields.Enabled = false;
                            }
                                Selrowno = index + 1;
                        }
                    }
                }
                else
                {
                    blnChangeTask = true;
                    uiPnlPatientTask.Enabled = false;
                    uiPnlPatientTask.AutoHide = true;

                    //00000926 : Added condition for Assigned Lab and DMS task
                    if (_TaskType == TaskType.Drug || _TaskType == TaskType.Flowsheet || _TaskType == TaskType.LabOrder || _TaskType == TaskType.AssignedLabOrder || _TaskType == TaskType.DMS || _TaskType == TaskType.AssignedDMS  || _TaskType == TaskType.OrderRadiology || _TaskType == TaskType.Exam)
                    {
                        btn_Patient.Enabled = false;
                    }
                    fillUserTask();
                    txtFrom.Text = _UserName;
                    tsbbtn_OnlySave.Visible = false;
                }

                EnableDisablePatientDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            try
            {
                Int64 nPatientID = Convert.ToInt64(txtPatient.Tag);
                gloPatient.gloPatient.GetWindowTitle(this, nPatientID, _databaseconnectionstring, _messageBoxCaption);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }

            finally
            {
                status =Convert.ToInt16(cmbStatus.SelectedValue);
                AttachControlEvents();
                C1PatTask.RowColChange += C1PatTask_RowColChange;
                if ((txtFrom.Text.Trim() == cmb_To.Text.Trim()))
                {
                    btnreply.Enabled = false ;
                    btnTo.Enabled = true;
                    btn_ToDel.Enabled = true;

                }
               
            }
        }


        public void LoadTaskAfterDMS()
        {

            try
            {
               
                uiPnlTaskDetails.Text = "Selected Task";
                uiPnlPatientTask.Text = "All Patient Tasks";
                tsb_MergeOrder.Visible = false;
                DetachControlEvents();
                C1PatTask.RowColChange -= C1PatTask_RowColChange;
                C1PatTask.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

                if (TaskID == 0 && _TaskType == TaskType.DMS)
                {
                    txtSubject.Text = "DMS DOC Received";
                    rtxtDescription.Text = DMSDescription;
                    if (DMSUser > 0)
                    {
                        _defaultuserID = DMSUser;
                    }
                }

                fill_Status();
                fill_Priority();
                fill_FollowUp();

                fill_Owner();


                //Added by madan on 20100614
                if (_isTaskforPlaceLab == true)
                {
                    txtSubject.Text = _tskSubjectForExternalOrder;//Lab Task Assignment
                    cmbPriority.Text = strPriority;
                    //commented by madan on 20100731
                    // _TaskType = TaskType.LabOrder;
                    //end comment.
                    rtxtDescription.Text = _LabTestList;
                }
                if (_IsTaskFromIntuitMessage == true)
                {


                    txtSubject.Text = _sSubject;
                    cmbPriority.Text = "Normal";
                    StringBuilder strDescription = new StringBuilder("Subject: ");
                    strDescription.Append(_sSubject);

                    if (_nAttachments > 0)
                    {
                        strDescription.Append("This message has " + _nAttachments.ToString() + " attachments.  Choose view message to access attachments ");
                    }
                    if (_sMessageBody.Length > 900)
                    {
                        strDescription.Append(Environment.NewLine + _sMessageBody.Substring(0, 900) + "....");
                    }
                    else
                    {
                        strDescription.Append(Environment.NewLine + _sMessageBody);
                    }
                    //strDescription.Append(Environment.NewLine + _sMessageBody);
                    rtxtDescription.Text = strDescription.ToString();
                }

                //End madan changes
                if (_nPatientID > 0)
                {
                    gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                    txtPatient.Text = ogloPatient.GetPatientName(_nPatientID);
                    txtPatient.Tag = _nPatientID;
                    ogloPatient.Dispose();
                    ogloPatient = null;
                }

                if (_nProviderID > 0)
                {
                    gloAppointmentBook.Books.Resource ogloProvider = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                    txtProvider.Text = ogloProvider.GetProviderName(_nProviderID);
                    txtProvider.Tag = _nProviderID;
                    ogloProvider.Dispose();
                    ogloProvider = null;
                }
                dtp_StartDate.Value = _dtStartDate;
                dtp_EndDate.Value = _dtDueDate;
                dtpReminderDate.Value = _dtStartDate;
                dtpReminderTime.Value = _dtStartDate;
                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
                _HasRightToCompleteTaskForAllUsers = oSetting.GetSettingUserSpecificRight("Complete Task for all Users", _userID, _ClinicID);

                oSetting.Dispose();
                oSetting = null;

                ChkCompleteTaskforallUsers.Visible = false;
                _IsCompleteAllTask = false;
                pnl_AssignTo.Visible = true;
                gloTask objglotsk = new gloTask(_databaseconnectionstring);
                if (objglotsk != null)
                {
                    Byte[] oBytesArry = (byte[])objglotsk.LoadDisplaySettings(UserID, System.Environment.MachineName);
                    MemoryStream memStream = null;
                    if (oBytesArry != null)
                    {
                        memStream = new MemoryStream(oBytesArry);

                        uiPanelManager1.LoadLayoutFile(memStream);
                    }
                    if (oBytesArry != null)
                    {
                        oBytesArry = null;
                    }
                    if (memStream != null)
                    {
                        memStream.Close();
                        memStream.Dispose();
                        memStream = null;

                    }

                    objglotsk.Dispose();
                    objglotsk = null;
                }
                HideToolStripButtons();
                if (TaskID > 0)
                {
                    fillUserTask();
                   
                   
                    if (dtTask != null)
                    {
                        DataRow[] dr = dtTask.Select("Taskid=" + TaskID + "");
                        if (dr.Length > 0)
                        {
                            int index = dtTask.Rows.IndexOf(dr[0]);
                            // C1PatTask.RowSel = index;
                            C1PatTask.Select((index + 1), 0);
                            if (Convert.ToString(C1PatTask.GetData(index + 1, 5)) == "Not Yet Accepted")
                            {
                                pnlFields.Enabled = false;
                            }
                            Selrowno = index + 1;
                        }
                        else //added condition for bugid 98688
                        {
                            if (dtTask != null)
                            {
                                if (dtTask.Rows.Count > 0)
                                {
                                    TaskID = Convert.ToInt64(dtTask.Rows[0]["TaskID"]);
                                }
                            }
                        }
                    }
                    ShowTask(TaskID);
                }
                else
                {
                    blnChangeTask = true;
                    uiPnlPatientTask.Enabled = false;
                    uiPnlPatientTask.AutoHide = true;

                    //00000926 : Added condition for Assigned Lab and DMS task
                    if (_TaskType == TaskType.Drug || _TaskType == TaskType.Flowsheet || _TaskType == TaskType.LabOrder || _TaskType == TaskType.AssignedLabOrder || _TaskType == TaskType.DMS || _TaskType == TaskType.AssignedDMS || _TaskType == TaskType.OrderRadiology || _TaskType == TaskType.Exam)
                    {
                        btn_Patient.Enabled = false;
                    }
                    fillUserTask();
                    
                    txtFrom.Text = _UserName;
                    tsbbtn_OnlySave.Visible = false;
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            //try commented for bugid 93056
            //{
            // //   Int64 nPatientID = Convert.ToInt64(txtPatient.Tag);
            //   // gloPatient.gloPatient.GetWindowTitle(this, nPatientID, _databaseconnectionstring, _messageBoxCaption);
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            //}

            finally
            {
                if (dtTask != null)
                {
                    if (dtTask.Rows.Count == 0)
                        this.Close();
                    else
                    {
                        AttachControlEvents();
                        C1PatTask.RowColChange += C1PatTask_RowColChange;
                    }
                }
                else
                {
                    this.Close();
                }
              
            
            }

         }
        //public void fillGridAfterSave()
        //{

        //     dtTask = ShowPatientAllTask(UserID,Convert.ToInt64 ( txtPatient.Tag)  );
        //    if (dtTask != null)
        //    {

        //        DataTable newTable = dtTask;
        //        newTable.Columns.Add(new DataColumn { ColumnName = "Select", DataType = typeof(Boolean), DefaultValue = false });
        //        newTable.Columns["Select"].SetOrdinal(0);
        //        newTable.Merge(dtTask, true, MissingSchemaAction.Add);

        //    }
        //    if (dtTask.Rows.Count <= 1)
        //    {
        //        tsb_OK.Visible = true;
        //        tsbbtn_OnlySave.Visible = false;
        //    }
        //    else
        //    {
        //        tsb_OK.Visible = true;
        //        tsbbtn_OnlySave.Visible = true;
        //    }

        //    //  C1PatTask.Cols.Fixed = 0;
        //    C1PatTask.Rows.Count = 1;
        //    C1PatTask.Cols.Count = dtTask.Columns.Count;

        //    CustomGridStyle();
        //    C1PatTask.DataSource = dtTask;
        //    C1PatTask.Cols[0].DataType = typeof(Boolean);
        //    C1PatTask.Cols[3].Visible = false;
        //    C1PatTask.Cols[4].Visible = false;
        //    C1PatTask.Cols[8].Visible = false;
        //    C1PatTask.Cols[9].Visible = false;
        //    C1PatTask.Cols[12].Visible = false;
        //    C1PatTask.Cols[13].Visible = false;
        //    C1PatTask.Cols[14].Visible = false;
        //    C1PatTask.Cols[15].Visible = false;//patientcode
        //    C1PatTask.Cols[17].Visible = false;
        //    C1PatTask.Cols[18].Visible = false; //ssn
        //    C1PatTask.Cols[19].Visible = false; //DOB

        //    if (dtTask.Rows.Count > 0)
        //    {
        //        TaskID = Convert.ToInt64(dtTask.Rows[0]["TaskId"]);
        //        ShowTask(TaskID);
        //    }

        //}

        public void CustomGridStyle()
        {
            C1PatTask.Cols.Count =20;
            //gloC1FlexStyle.Style();
            //Select,Due Date,Subject,TaskID,No.,Status,% Completed,Priority,TaskDate,PatientID,AssignStatus,PatientCode,Patient Name,nProviderID,nCategoryID,ProviderName,
            //Total Count,TaskGroupID,SSN,Date Of Birth,


            C1PatTask.Cols[Col_Select].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_DueDate].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_Subject].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_TaskID].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_No].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_Status].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_PercCompleted].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_Priority].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_TaskDate].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

            C1PatTask.Cols[Col_PatientID].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_AssignStatus].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_PatientCode].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_PatientName].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_nProviderID].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_nCategoryID].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_ProviderName].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_TotalCount].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_TaskGroupID].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_SSN].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1PatTask.Cols[Col_DateofBirth].StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


            C1PatTask.SetData(0, Col_Select, "Select");
            C1PatTask.SetData(0, Col_DueDate, "Due Date");
            C1PatTask.SetData(0, Col_Subject, "Subject");
            C1PatTask.SetData(0, Col_TaskID, "TaskID");
            C1PatTask.SetData(0, Col_No, "No.");
            C1PatTask.SetData(0, Col_Status, "Status");
            C1PatTask.SetData(0, Col_PercCompleted, "% Completed");
            C1PatTask.SetData(0, Col_Priority, "Priority");
            C1PatTask.SetData(0, Col_TaskDate, "TaskDate");

            C1PatTask.SetData(0, Col_PatientID, "PatientID");
            C1PatTask.SetData(0, Col_AssignStatus, "AssignStatus");
            C1PatTask.SetData(0, Col_PatientCode, "PatientCode");
            C1PatTask.SetData(0, Col_PatientName, "Patient Name");
            C1PatTask.SetData(0, Col_nProviderID, "nProviderID");
            C1PatTask.SetData(0, Col_nCategoryID, "nCategoryID");
            C1PatTask.SetData(0, Col_ProviderName, "ProviderName");
            C1PatTask.SetData(0, Col_TotalCount, "Total Count");
            C1PatTask.SetData(0, Col_TaskGroupID, "TaskGroupID");
            C1PatTask.SetData(0, Col_SSN, "SSN");
            C1PatTask.SetData(0, Col_DateofBirth, "Date Of Birth");

            C1PatTask.Cols[Col_Select].Width = 50;
            C1PatTask.Cols[Col_DueDate].Width = 70;
            C1PatTask.Cols[Col_Status].Width = 70;
            C1PatTask.Cols[Col_Priority].Width = 65;
          //  C1PatTask.Cols["PriorityImage"].Width = 50;




            //C1PatTask.Cols(Col_REPORTINGPERIODSTATUS).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


            for (int i = 0; i < C1PatTask.Rows.Count; i++)
            {
                string imageType = Convert.ToString(C1PatTask.GetData(i, Col_Priority));
                switch (imageType)
                {
                    case "High":
                        C1PatTask.SetCellImage(i, Col_Priority, imgList_Common.Images[0]);
                        break;
                    case "Normal":
                        C1PatTask.SetCellImage(i, Col_Priority, imgList_Common.Images[1]);
                        break;

                    case "Low":
                        C1PatTask.SetCellImage(i, Col_Priority, imgList_Common.Images[2]);
                        break;

                }
            }
        }



        //       Private Sub LoadDisplaySettings()
        //    Dim Con As SqlConnection = Nothing
        //    Dim cmd As SqlCommand = Nothing
        //    Dim da As SqlDataAdapter = Nothing ''slr new not needed 
        //    Dim dt As DataTable = Nothing ''slr new not needed 
        //    Try
        //        'Get Users Display Settings
        //        Con = New SqlConnection(GetConnectionString)
        //        cmd = New SqlCommand("gsp_GET_UserDisplaySettings", Con)
        //        cmd.CommandType = CommandType.StoredProcedure
        //        Dim objParam As SqlParameter

        //        objParam = cmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
        //        objParam.Direction = ParameterDirection.Input
        //        objParam.Value = gnLoginID









        //        objParam = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar)
        //        objParam.Direction = ParameterDirection.Input
        //        objParam.Value = gstrClientMachineName

        //        da = New SqlDataAdapter
        //        dt = New DataTable
        //        da.SelectCommand = cmd
        //        da.Fill(dt)

        //        'Load Display Settings
        //        If IsNothing(dt) = False Then
        //            If dt.Rows.Count > 0 Then
        //                If IsDBNull(dt.Rows(0)("iStyle")) = False Then
        //                    Dim oBytesArry As Byte() = CType(dt.Rows(0)("iStyle"), Byte())
        //                    Dim memStream As MemoryStream = New MemoryStream(oBytesArry)
        //                    uiPanelManager1.LoadLayoutFile(memStream)
        //                    'SLR: clear obytesarray
        //                    If Not oBytesArry Is Nothing Then
        //                        oBytesArry = Nothing
        //                    End If
        //                    memStream.Close()
        //                    memStream.Dispose()
        //                    memStream = Nothing
        //                End If
        //            End If
        //        End If
        //    Catch ex As SqlException
        //        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Load, "LoadDisplaySettings -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        //    Catch ex As Exception
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Load, "LoadDisplaySettings -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        //        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        //    Finally
        //        If Not IsNothing(dt) Then
        //            dt.Dispose()
        //            dt = Nothing
        //        End If
        //        If Not IsNothing(da) Then
        //            da.Dispose()
        //            da = Nothing
        //        End If
        //        If Not IsNothing(cmd) Then
        //            cmd.Parameters.Clear()
        //            cmd.Dispose()
        //            cmd = Nothing
        //        End If
        //        If Not IsNothing(Con) Then
        //            If Con.State = ConnectionState.Open Then
        //                Con.Close()
        //            End If
        //            Con.Dispose()
        //            Con = Nothing
        //        End If
        //    End Try
        //End Sub








        private void HideToolStripButtons()
        {
            foreach (ToolStripButton tbtn in ts_Commands.Items)
            {
                tbtn.Visible = false;
            }
            tsb_Cancel.Visible = true;
            if (C1PatTask.Rows.Count <= 2)
            {
                tsb_OK.Visible = true;
                tsbbtn_OnlySave.Visible = false;
            }
            else
            {
                tsb_OK.Visible = true;
                tsbbtn_OnlySave.Visible = true;
            }

        }
        private void fillUserTask()
        {
            if (dtTask != null)
            {
                dtTask.Dispose();
                dtTask = null;
            }
            dtTask = ShowPatientAllTask(UserID, PatientID);
            if (dtTask != null)
            {

                DataTable newTable = dtTask;
                newTable.Columns.Add(new DataColumn { ColumnName = "Select", DataType = typeof(Boolean), DefaultValue = false });
                newTable.Columns["Select"].SetOrdinal(0);
             
               newTable.Merge(dtTask, true, MissingSchemaAction.Add);

            }
            if (dtTask.Rows.Count <= 1)
            {
                tsb_OK.Visible = true;
                tsbbtn_OnlySave.Visible = false;
            }
            else
            {
                tsb_OK.Visible = true;
                tsbbtn_OnlySave.Visible = true;
            }

            //  C1PatTask.Cols.Fixed = 0;
            C1PatTask.Rows.Count = 1;
            C1PatTask.Cols.Count = dtTask.Columns.Count;
            C1PatTask.DataSource = dtTask;
            CustomGridStyle();


            if (dtTask.Rows.Count == 0)
                pnlselectall.Visible = false;
            else
                pnlselectall.Visible = true;

            SetColumnVisibility();

        }
        private void SetColumnVisibility()
        {
            C1PatTask.Cols[0].DataType = typeof(Boolean);
            C1PatTask.Cols[Col_TaskID].Visible = false;
            C1PatTask.Cols[Col_No].Visible = false;
            C1PatTask.Cols[Col_TaskDate].Visible = false;
            C1PatTask.Cols[Col_PatientID].Visible = false;
            C1PatTask.Cols[Col_PatientName].Visible = false;
            C1PatTask.Cols[Col_nProviderID].Visible = false;
            C1PatTask.Cols[Col_nCategoryID].Visible = false;
            C1PatTask.Cols[Col_ProviderName].Visible = false;//patientcode
            C1PatTask.Cols[Col_TaskGroupID].Visible = false;
            C1PatTask.Cols[Col_SSN].Visible = false; //ssn
            C1PatTask.Cols[Col_DateofBirth].Visible = false; //DOB
            C1PatTask.Cols[Col_PatientCode].Visible = false;
            C1PatTask.Cols[Col_AssignStatus].Visible = false;
            C1PatTask.Cols[Col_TotalCount].Visible = false;
            C1PatTask.Cols[Col_PercCompleted].Visible = false;
            C1PatTask.Cols[Col_Select].AllowEditing = true;
            C1PatTask.Cols[Col_DueDate].AllowEditing = false;
            C1PatTask.Cols[Col_Subject].AllowEditing = false;
            C1PatTask.Cols[Col_TaskID].AllowEditing = false;
            C1PatTask.Cols[Col_TaskDate].AllowEditing = false;
            C1PatTask.Cols[Col_Priority].AllowEditing = false;
            C1PatTask.Cols[Col_No].AllowEditing = false;
            C1PatTask.Cols[Col_Status].AllowEditing = false;
            C1PatTask.Cols[Col_PercCompleted].AllowEditing = false;
            C1PatTask.Cols[Col_AssignStatus].AllowEditing = false;
            C1PatTask.Cols[Col_PatientCode].AllowEditing = false;
            C1PatTask.Cols[Col_PatientName].AllowEditing = false;
            C1PatTask.Cols[Col_nProviderID].AllowEditing = false;
            C1PatTask.Cols[Col_nCategoryID].AllowEditing = false;
            C1PatTask.Cols[Col_ProviderName].AllowEditing = false;
            C1PatTask.Cols[Col_TotalCount].AllowEditing = false;
            C1PatTask.Cols[Col_TaskGroupID].AllowEditing = false;
            C1PatTask.Cols[Col_SSN].AllowEditing = false;
            C1PatTask.Cols[Col_DateofBirth].AllowEditing = false;
            //C1PatTask.Cols["PriorityImage"].Visible = true; //DOB
            //C1PatTask.Cols["PriorityImage"].AllowEditing = false;
           
        }
        private void RefreshTaskList(Int64 AcceptTaskId = 0)
        {
            try
            {

                DetachControlEvents();
                ChkCompleteTaskforallUsers.Checked = false; //added for bugid 76152
                chkReminder.Checked = false;                //added for bugid 76152
                if (dtTask != null)
                {
                    dtTask.Dispose();
                    dtTask = null;

                }
                dtTask = ShowPatientAllTask(UserID, Convert.ToInt64(txtPatient.Tag));
                if (dtTask != null)
                {

                    DataTable newTable = dtTask;
                    newTable.Columns.Add(new DataColumn { ColumnName = "Select", DataType = typeof(Boolean), DefaultValue = false });
                    newTable.Columns["Select"].SetOrdinal(0);
                  
                    newTable.Merge(dtTask, true, MissingSchemaAction.Add);
                  
                }
                // C1PatTask.Cols.Fixed = 0;
                C1PatTask.Rows.Count = 1;
                C1PatTask.Cols.Count = dtTask.Columns.Count;
               
                C1PatTask.RowColChange -= C1PatTask_RowColChange;
                C1PatTask.DataSource = dtTask;
                CustomGridStyle();
                SetColumnVisibility();
                if (dtTask.Rows.Count == 0)
                    pnlselectall.Visible = false;
                else
                    pnlselectall.Visible = true;

                if (dtTask.Rows.Count > 0)
                {
                    if (AcceptTaskId == 0)
                    {
                        //  TaskID = Convert.ToInt64(dtTask.Rows[0]["TaskId"]);
                        DataRow[] dr = dtTask.Select("TaskId=" + TaskID + "");
                        if (dr.Length > 0)
                        {
                            TaskID = Convert.ToInt64(dr[0]["TaskId"]);
                            C1PatTask.Select(dtTask.Rows.IndexOf(dr[0]) + 1, 0);
                        }
                        else
                        {
                            TaskID = Convert.ToInt64(dtTask.Rows[0]["TaskId"]);
                        }

                    }
                    else
                    {
                        DataRow[] dr = dtTask.Select("TaskId=" + AcceptTaskId + "");
                        if (dr.Length > 0)
                        {
                            TaskID = Convert.ToInt64(dr[0]["TaskId"]);
                            C1PatTask.Select(dtTask.Rows.IndexOf(dr[0]) + 1, 0);
                        }
                        else
                        {
                            TaskID = Convert.ToInt64(dtTask.Rows[0]["TaskId"]);
                        }

                    }
                    HideToolStripButtons();
                    ShowTask(TaskID);
                    Selrowno = C1PatTask.RowSel;  
                }
            }
            catch (Exception exp)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Refresh, exp.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                AttachControlEvents();


            }
        }

        private void DetachControlEvents()
        {
            dtpReminderTime.ValueChanged -= dtpReminderTime_ValueChanged;
            dtpReminderDate.ValueChanged -= dtpReminderDate_ValueChanged;
            cmb_FollowUp.SelectedIndexChanged -= cmb_FollowUp_SelectedIndexChanged;
            cmbPriority.SelectedIndexChanged -= cmbPriority_SelectedIndexChanged;
            cmb_To.SelectedIndexChanged -= cmb_To_SelectedIndexChanged;
            cmbStatus.SelectedIndexChanged -= cmbStatus_SelectedIndexChanged;
            dtp_StartDate.ValueChanged -= dtp_StartDate_ValueChanged;
            dtp_EndDate.ValueChanged -= dtp_EndDate_ValueChanged;
            txtPatient.TextChanged -= txtPatient_TextChanged;
            txtProvider.TextChanged -= txtProvider_TextChanged;
            txtFrom.TextChanged -= txtFrom_TextChanged;
            txtSubject.TextChanged -= txtSubject_TextChanged;
            numComplete.ValueChanged -= numComplete_ValueChanged;
            rtxtDescription.TextChanged -= rtxtDescription_TextChanged;
            C1PatTask.RowColChange -= C1PatTask_RowColChange;
        }
        private void AttachControlEvents()
        {
            DetachControlEvents();
            dtpReminderTime.ValueChanged += dtpReminderTime_ValueChanged;
            dtpReminderDate.ValueChanged += dtpReminderDate_ValueChanged;
            cmb_FollowUp.SelectedIndexChanged += cmb_FollowUp_SelectedIndexChanged;
            cmbPriority.SelectedIndexChanged += cmbPriority_SelectedIndexChanged;
            cmb_To.SelectedIndexChanged += cmb_To_SelectedIndexChanged;
            cmbStatus.SelectedIndexChanged += cmbStatus_SelectedIndexChanged;
            dtp_StartDate.ValueChanged += dtp_StartDate_ValueChanged;
            dtp_EndDate.ValueChanged += dtp_EndDate_ValueChanged;
            txtPatient.TextChanged += txtPatient_TextChanged;
            txtProvider.TextChanged += txtProvider_TextChanged;
            txtFrom.TextChanged += txtFrom_TextChanged;
            txtSubject.TextChanged += txtSubject_TextChanged;
            numComplete.ValueChanged += numComplete_ValueChanged;

            rtxtDescription.TextChanged += rtxtDescription_TextChanged;
            C1PatTask.RowColChange += C1PatTask_RowColChange;

        }


        #region " Fill Methods "

        private void fill_Status()
        {
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTasksMails.Common.Statuses oStatuses = new gloTasksMails.Common.Statuses();

            try
            {

                oStatuses = oTaskMail.GetStatuses();

                if (oStatuses != null)
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add("Description");
                    dtTemp.Columns.Add("ID");
                    for (int i = 0; i < oStatuses.Count; i++)
                    {
                        DataRow dr = dtTemp.NewRow();
                        dr["Description"] = oStatuses[i].Description;
                        dr["ID"] = oStatuses[i].ID.ToString();
                        dtTemp.Rows.Add(dr);
                        dr = null;
                    }
                    cmbStatus.DataSource = dtTemp;
                    cmbStatus.DisplayMember = dtTemp.Columns["Description"].ColumnName;
                    cmbStatus.ValueMember = dtTemp.Columns["ID"].ColumnName;
                    cmbStatus.SelectedValue = 1;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                oTaskMail.Dispose();
                oTaskMail = null;

                oStatuses.Dispose();
                oStatuses = null;

            }
        }

        private void fill_Priority()
        {
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTasksMails.Common.Priorities oPriorities = new gloTasksMails.Common.Priorities();

            try
            {
                oPriorities = oTaskMail.GetPriorities();

                if (oPriorities != null)
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add("Description");
                    dtTemp.Columns.Add("ID");
                    for (int i = 0; i < oPriorities.Count; i++)
                    {
                        DataRow dr = dtTemp.NewRow();
                        dr["Description"] = oPriorities[i].Description;
                        dr["ID"] = oPriorities[i].ID;
                        dtTemp.Rows.Add(dr);
                        dr = null;
                    }
                    cmbPriority.DataSource = dtTemp;
                    cmbPriority.DisplayMember = dtTemp.Columns["Description"].ColumnName;
                    cmbPriority.ValueMember = dtTemp.Columns["ID"].ColumnName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                oTaskMail.Dispose();
                oTaskMail = null;

                oPriorities.Dispose();
                oPriorities = null;
            }
        }

        private void fill_FollowUp()
        {
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTasksMails.Common.Followups oFollowUps = new gloTasksMails.Common.Followups();

            try
            {
                oFollowUps = oTaskMail.GetFollowUps();

                if (oFollowUps != null)
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add("Description");
                    dtTemp.Columns.Add("ID");

                    for (int i = 0; i < oFollowUps.Count; i++)
                    {
                        DataRow dr = dtTemp.NewRow();
                        dr["Description"] = oFollowUps[i].Description;
                        dr["ID"] = oFollowUps[i].ID;
                        dtTemp.Rows.Add(dr);
                        dr = null;
                    }
                    cmb_FollowUp.DataSource = dtTemp;
                    cmb_FollowUp.DisplayMember = dtTemp.Columns["Description"].ColumnName;
                    cmb_FollowUp.ValueMember = dtTemp.Columns["ID"].ColumnName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                oTaskMail.Dispose();
                oTaskMail = null;

                oFollowUps.Dispose();
                oFollowUps = null;
            }
        }

        private void CommaSeperatedString(string splitstring)
        {
            try
            {





            }
            catch (Exception ex)
            {
                string _ErrorMessage = ex.ToString();

            }
        }


        DataTable dtUsers = null;
        private void fill_Owner()
        {
            //cmb_To.Items.Clear(); 
            gloAppointmentBook.Books.Resource oUser = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);

            try
            {

             dtUsers = new DataTable();
                dtUsers.Columns.Add("ID");
                dtUsers.Columns.Add("Description");


                //code added for track task edit functionality -pradeep 20100721

                if (_TrackTask == true || _SmartTask == true)
                {
                    string[] str = _taskuser_id.Split(',');
                    ToList = new gloGeneralItem.gloItems();
                    foreach (string temp in str)
                    {
                        DataRow drTemp = dtUsers.NewRow();
                        drTemp["ID"] = Convert.ToInt64(temp);
                        //drTemp["Description"] = oUser.GetUserName(_userID);
                        drTemp["Description"] = oUser.GetLoginName(Convert.ToInt64(temp));


                        gloGeneralItem.gloItem ToItem = new gloGeneralItem.gloItem();

                        ToItem.ID = Convert.ToInt64(temp);
                        ToItem.Description = drTemp["Description"].ToString();



                        ToList.Add(ToItem);
                        dtUsers.Rows.Add(drTemp);

                        ToItem.Dispose();
                        ToItem = null;
                    }
                }
                else

                {
                    //27-Mar-15 Aniket: Changes as per mail 'CDH Feedback from Yaw Sir'
                    //Tasks always defaults to logged in user when new task is created
                    DataRow drTemp=null;
                    //code addded to fix bug 17415
                    if (_defaultuserID > 0)
                    {
                        drTemp = dtUsers.NewRow();
                        drTemp["ID"] = _defaultuserID;
                        drTemp["Description"] = oUser.GetLoginName(_defaultuserID);

                    }
                    
                    else
                    {
                        if (TaskID != 0)
                        {
                            drTemp = dtUsers.NewRow();
                            drTemp["ID"] = _userID;
                            drTemp["Description"] = oUser.GetLoginName(_userID);
                        }

                    }

                    if (drTemp != null)
                    {
                        dtUsers.Rows.Add(drTemp);
                        ToList = new gloGeneralItem.gloItems();
                        gloGeneralItem.gloItem ToItem = new gloGeneralItem.gloItem();

                        ToItem.ID = Convert.ToInt64(drTemp["ID"]);
                        ToItem.Description = drTemp["Description"].ToString();

                        ToList.Add(ToItem);

                        ToItem.Dispose();
                        ToItem = null;
                    }
                }

                cmb_To.DataSource = dtUsers;
                cmb_To.ValueMember = dtUsers.Columns["ID"].ColumnName;
                cmb_To.DisplayMember = dtUsers.Columns["Description"].ColumnName;

               
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oUser != null)
                {
                    oUser.Dispose();
                    oUser = null;
                }
            }

        }

        private void GetPatient()
        {

        }

        #endregion " Fill Methods "

        #region "Tool Strip Events"
        private bool _istaskreplyed = false;
        //private void updatereplyedtask(Int64 TaskId,Int64 fromuserid,Int64 toUserID,string subject)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    //  String Query = "SELECT COUNT(*) FROM TM_Task_Assign INNER JOIN TM_TaskMST ON TM_Task_Assign.nTaskID = TM_TaskMST.nTaskID WHERE (TM_Task_Assign.nAcceptRejectHold = 3 OR TM_Task_Assign.nAcceptRejectHold = 4) AND TM_TaskMST.nTaskID = " + TaskID + "  AND TM_Task_Assign.nAssignToID = " + _userID + "";
        //    gloDatabaseLayer.DBParameters oParamater = new gloDatabaseLayer.DBParameters();

        //    //DataTable _dttask = null;
        //    try
        //    {
        //        oDB.Connect(false);
        //      //  oParamater.Add("@nUserId", UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //      //  oParamater.Add("@nPatientID", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        //oDB.Execute_Query ("getIncompleteTaskList_Patient", oParamater, out _dttask);
        //        //if (_dttask != null)

        //        oDB.Execute_Query("Update tm_taskmst  set ssubject ='" + subject + "' , nUserID=" + toUserID + " where nTaskID =" + TaskId + "");
        //        oDB.Execute_Query("Update TM_Task_Assign  set nAssignToID =" + toUserID + ", nAssignFromID =" + fromuserid + " where nTaskID =" + TaskId + " and nAssignToID= " + fromuserid + " and nAssignFromID =" + toUserID + "");
         
        //        oDB.Disconnect();
        //    }
        //    catch
        //    {
               
        //    }
        //    finally
        //    {
        //        if (oDB != null)
        //        {
        //            oDB.Dispose();
        //        }
        //    }
           
        //}
        //private void TaskReplyed(string callfrom, object sender, ToolStripItemClickedEventArgs e)
        //{
        //    try
        //    {
        //        cmb_To.SelectedIndex = 0;
        //        updatereplyedtask(TaskID, GetUserID(txtFrom.Text), Convert.ToInt64 (cmb_To.SelectedValue) , txtSubject.Text.Trim() );
        //        DataTable dtusers =(DataTable ) cmb_To.DataSource;
        //        if (dtusers != null)
        //        {
        //            if (dtusers.Rows.Count > 0)
        //            {
        //                dtusers.Rows.RemoveAt(0);   
        //            }
        //            cmb_To.DataSource = dtusers; 
        //        }


        //        if (cmb_To.Items.Count > 0)
        //        {
        //            if (callfrom == "Save")
        //            {
        //                if (blnChangeTask == true)
        //                {
        //                    if (blnCreateTask())
        //                    {
        //                        _IsUpdated = true;
        //                        blnChangeTask = false;
        //                        RefreshTaskList();
        //                    }
        //                }

        //            }
        //            if (callfrom == "OK")
        //            {
        //                if (blnChangeTask == true) //added for bugid 76158
        //                {
        //                    if (blnCreateTask())
        //                    {
        //                        _IsTaskClose = true;
        //                        _IsUpdated = true;
        //                        this.Close();
        //                        OnTaskChangeEvent(sender, e);
        //                    }
        //                }
        //                else
        //                {
        //                    _IsTaskClose = true;
        //                    _IsUpdated = true;
        //                    this.Close();
        //                    OnTaskChangeEvent(sender, e);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (callfrom == "Save")
        //            {
        //                _IsUpdated = true;
        //                blnChangeTask = false;
        //                RefreshTaskList();
        //            }
        //            if (callfrom == "OK")
        //            {
        //                _IsTaskClose = true;
        //                _IsUpdated = true;
        //                this.Close();
        //                OnTaskChangeEvent(sender, e);
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        _istaskreplyed = false; 
        //    }

        //}
        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                blnOpnShowDocs = false; 
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                       
                           if (blnChangeTask == true)
                            {
                                if (blnCreateTask())
                                {
                                    _IsUpdated = true;
                                    blnChangeTask = false;
                                    RefreshTaskList();
                                }
                            }
                     
                        break;

                    case "completeall":
                        CompleteAllSelectedTask();
                        break;
                    case "OK":

                      


                            if (blnChangeTask == true) //added for bugid 76158
                            {
                                if (blnCreateTask())
                                {
                                    _IsTaskClose = true;
                                    _IsUpdated = true;
                                    this.Close();
                                    OnTaskChangeEvent(sender, e);
                                }
                            }
                            else
                            {
                                _IsTaskClose = true;
                                _IsUpdated = true;
                                this.Close();
                                OnTaskChangeEvent(sender, e);
                            }
                      
                        break;
                    case "Cancel":

                        if ((blnChangeTask == true) && (tsb_OK.Enabled == true))
                        {
                            DialogResult dg = MessageBox.Show("Do you want to save the changes?", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (dg == DialogResult.Yes)
                            {

                                //CreateTask();

                                //_IsUpdated = true;

                                if (blnCreateTask())
                                {
                                    _IsTaskClose = true;
                                    _IsUpdated = false;
                                    OnTaskChangeEvent(sender, e);
                                    this.Close();
                                }
                            }
                            if (dg == DialogResult.No)
                            {
                                _IsTaskClose = true;
                                _IsUpdated = false;
                                //  OnTaskChangeEvent(sender, e);
                                this.Close();
                            }

                            if (dg == DialogResult.Cancel)
                            {
                                txtSubject.Focus();

                                return;

                            }
                        }
                        else
                        {
                            _IsTaskClose = true;
                            _IsUpdated = false;
                            //  OnTaskChangeEvent(sender, e);
                            this.Close();
                        }
                        break;

                    case "AssignTo":
                        //Code to toggle the AssignedTo combo visibiltiy.
                        if (pnl_AssignTo.Visible == false)
                        {
                            pnl_AssignTo.Visible = true;
                            tsb_AssignTo.Text = "Cancel Assign";
                        }
                        else
                        {
                            pnl_AssignTo.Visible = false;
                            tsb_AssignTo.Text = "Assign To";
                        }
                        break;

                    case "ShowOrder":  //-- USED FOR gloEMR --//
                    //case "ShowLab": 
                    case "MatchPatient": //Added by kanchan on 20100303 for Emdeon,Unmatched Patients
                    case "ViewReport":
                    case "ReviewPatient":
                    case "ShowExam":
                    case "ShowCCD":          //Added by kanchan on 20100605 for CCD
                    case "ShowFlowSheet":    //Added by kanchan on 20100619 for Flowsheet 
                    case "ShowDrugs":        //Added by kanchan on 20100619 for Drugs 
                    case "ShowVitals":
                    case "MergeOrder":
                        //case "LabReport":
                        {

                            _IsTaskClose = false;
                            _IsUpdated = false;
                            OnTaskChangeEvent(sender, e);
                        }
                        break;
                    case "ShowDocs":
                          {
                              blnOpnShowDocs = true;
                            _IsTaskClose = false;
                            _IsUpdated = false;
                            OnTaskChangeEvent(sender, e);
                        }
                        break;

                    case "Reconcile":
                        _IsReconcile = true;
                        _IsTaskClose = false;
                        _IsUpdated = false;
                        OnTaskChangeEvent(sender, e);
                        _IsReconcile = false;
                        break;
                    case "LabReport":
                    case "ShowLab":
                        {
                            CreateTask();
                            _IsTaskClose = true;
                            _IsUpdated = true;
                            OnTaskChangeEvent(sender, e);
                            this.Close();
                            _IsTaskClose = false;
                            _IsUpdated = false;
                            OnTaskChangeEvent(sender, e);
                        }
                        break;
                    case "AcceptTask":
                        if (DialogResult.No == MessageBox.Show("Accept selected task requests ?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            break;
                        }
                        AcceptTask(TaskID);
                        break;
                    case "DeclineTask":
                        if (DialogResult.No == MessageBox.Show("Decline selected task requests ?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            break;
                        }
                        DeclineTask(TaskID);
                        _IsTaskClose = true;
                        _IsUpdated = true;
                        OnTaskChangeEvent(sender, e);
                        this.Close();
                        break;
                    case "LabOrder":
                        OpenEmdeon = true;
                        OnTaskChangeEvent(sender, e);
                        OpenEmdeon = false;
                        break;
                    case "ViewMessage":
                        _IsTaskClose = false;
                        _IsUpdated = false;
                        _sButtonClick = e.ClickedItem.Tag.ToString();
                        OnTaskChangeEvent(sender, e);
                        break;
                    case "ReplyMessage":
                        _IsTaskClose = false;
                        _IsUpdated = false;
                        _sButtonClick = e.ClickedItem.Tag.ToString();
                        OnTaskChangeEvent(sender, e);
                        break;
                    case "ForwardMessage":
                        _isTaskForwardedFromIntuitMessage = true;
                        txtFrom.Text = _UserName;
                        tsb_ViewMessage.Visible = false;
                        tsb_Reply.Visible = false;
                        tsb_Forward.Visible = false;
                      
                        cmb_To.DataSource = null;
                        cmb_To.Items.Clear();
                        ToList = null;
                        break;
                    case "RxMeds":
                        _IsTaskClose = false;
                        _IsUpdated = false;
                        _sButtonClick = e.ClickedItem.Tag.ToString();
                        OnTaskChangeEvent(sender, e);
                        break;
                    case "HL7DocumentTask": //Added code to handle new task type
                        _IsTaskClose = false;
                        _IsUpdated = false;
                        OnTaskChangeEvent(sender, e);
                        break;
                    case "PFPortalTask":
                        blnOpnShowDocs = true;
                        _IsTaskClose = false;
                        _IsUpdated = false;
                        OnTaskChangeEvent(sender, e);
                        break;
                    case "ShowHistory":
                        _IsTaskClose = false;
                        _IsUpdated = false;
                        _sButtonClick = e.ClickedItem.Tag.ToString();
                        OnTaskChangeEvent(sender, e);
                        break;
                    case "ShowROS":
                        _IsTaskClose = false;
                        _IsUpdated = false;
                        _sButtonClick = e.ClickedItem.Tag.ToString();
                        OnTaskChangeEvent(sender, e);
                        break;
                    case "ReviewPHI":
                        _IsTaskClose = false;
                        _IsUpdated = false;
                        _sButtonClick = e.ClickedItem.Tag.ToString();
                        OnTaskChangeEvent(sender, e);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void OnTaskChangeEvent(object sender, EventArgs e)
        {
            TaskChangeEventArg e2 = new TaskChangeEventArg();

            e2.TaskID = _taskID;
            e2.Subject = txtSubject.Text;
            e2.oTaskType = (TaskType)_TaskType;
            e2.PatientID = Convert.ToInt64(txtPatient.Tag);

            if (OpenEmdeon == true)
            {
                e2.oTaskType = TaskType.OpenEmdeonlabOrder;
            }
            if (_IsReconcile == true)
            {
                if (e2.oTaskType == TaskType.Reconcile)
                {
                    e2.oTaskType = TaskType.Reconcile;
                    _IsReconcile = false;
                }
                else if (e2.oTaskType == TaskType.ImReconcile)
                {
                    e2.oTaskType = TaskType.ImReconcile;
                    _IsReconcile = false;
                }
            }
            e2.IsEMREnabled = _IsEMREnable;
            e2.IsUpdated = _IsUpdated;
            e2.IsTaskClose = _IsTaskClose;
            e2.IsOpenFromView = _IsOpenfromView;
            e2.btnTag = _sButtonClick;
            e2.sMessage = rtxtDescription.Text;
            if (_IsEMREnable == true)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                DataTable oDataTable = new DataTable();
                oDB.Retrive_Query("SELECT sFaxTiffFileName,nReferenceID1,nReferenceID2 FROM TM_TaskMST WHERE nTaskID = " + _taskID + " AND nClinicID = " + _ClinicID + "", out oDataTable);
                if (oDataTable != null && oDataTable.Rows.Count > 0)
                {
                    if (oDataTable.Rows[0]["sFaxTiffFileName"].GetType() != typeof(System.DBNull))
                    {
                        e2.FaxTiffFileName = oDataTable.Rows[0]["sFaxTiffFileName"].ToString();
                    }
                    if (oDataTable.Rows[0]["nReferenceID1"].GetType() != typeof(System.DBNull))
                    {
                        if (oDataTable.Rows[0]["nReferenceID1"].ToString() != null && oDataTable.Rows[0]["nReferenceID1"].ToString().Trim() != "")
                        {
                            e2.ReferenceID1 = Convert.ToInt64(oDataTable.Rows[0]["nReferenceID1"].ToString());
                        }
                    }
                    if (oDataTable.Rows[0]["nReferenceID2"].GetType() != typeof(System.DBNull))
                    {
                        if (oDataTable.Rows[0]["nReferenceID2"].ToString() != null && oDataTable.Rows[0]["nReferenceID2"].ToString().Trim() != "")
                        {
                            e2.ReferenceID2 = Convert.ToInt64(oDataTable.Rows[0]["nReferenceID2"].ToString());
                        }
                    }
                }
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); }
            }
                //Bug #82464: 00000909: Task screen not opening respective screen
                e2Task = e2;
                if (blnOpnShowDocs == false)   //if show docs is not opened 
                    this.Close();
                else
                {
                    if (OnTask_Change != null)
                    {
                        OnTask_Change(sender, e, e2,this);
                        e2 = null;
                        e2Task = null;
                      //  LoadTaskAfterDMS();
                    }
                }
            }

        #endregion "Tool Strip Events"

        #region " Task Methods "

        //private void save_Task()
        //{
        //    Task oTask = new Task();
        //    gloTask ogloTask = new gloTask(_databaseconnectionstring);
        //    //if (Convert.ToInt64(txtTo.Tag) > 0 && TaskID > 0)
        //    //{
        //    //    this.Close();
        //    //    return;
        //    //}
        //    try
        //    {
        //        if (txtTo.Visible == true && txtTo.Text.Trim() == "")
        //        {
        //            MessageBox.Show("Assigned Person cant be blank", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            btnTo.Focus();
        //            return;
        //        }

        //        if (txtSubject.Text == "")
        //        {
        //            MessageBox.Show("Please enter the Task Subject", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            txtSubject.Focus();
        //            return;
        //        }
        //        if (txtProvider.Text == "")
        //        {
        //            MessageBox.Show("Please select Provider", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            btn_Providers.Focus();
        //            return;
        //        }
        //        if(txtOwner.Text == "")
        //        {
        //            MessageBox.Show("Please select Owner of Task.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            txtOwner.Focus();
        //            return;
        //        }
        //        if (dtp_EndDate.Value.Date.CompareTo(dtp_StartDate.Value.Date)<0)
        //        {
        //            MessageBox.Show("End Date selected is passed.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            dtp_EndDate.Focus();
        //            return;
        //        }
        //        if (Convert.ToInt64(txtTo.Tag) == Convert.ToInt64(txtProvider.Tag))
        //        {
        //            MessageBox.Show("You cant send Task Request to yourself.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            txtTo.Focus();
        //            return;
        //        }



        //        oTask.TaskID = TaskID;
        //        oTask.ProviderID = Convert.ToInt64(txtProvider.Tag);
        //        oTask.PatientID = Convert.ToInt64(txtPatient.Tag);
        //        oTask.Subject = txtSubject.Text.Trim();
        //        oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(dtp_StartDate.Value.ToString("MM/dd/yyyy"));
        //        oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(dtp_EndDate.Value.ToString("MM/dd/yyyy"));
        //        oTask.StatusID = Convert.ToInt64(cmbStatus.SelectedValue);
        //        oTask.PriorityID =Convert.ToInt64(cmbPriority.SelectedValue);
        //        oTask.Complete = numComplete.Value;
        //        oTask.CategoryID = 1;//1-> Task 
        //        oTask.FollowupID =Convert.ToInt64(cmb_FollowUp.SelectedValue);
        //        oTask.IsPrivate = false;

        //        //check if the Task is Assigned to someone else
        //        //if yes set the assigned persons id & name
        //        //if no set id=0 & name =blank;
        //        if (txtTo.Text != "")
        //        {
        //            oTask.AssignedToID = Convert.ToInt64(txtTo.Tag);
        //            oTask.AssignedToName = txtTo.Text.Trim();
        //        }
        //        else
        //        {
        //            oTask.AssignedToID = 0;
        //            oTask.AssignedToName = "";
        //        }


        //        oTask.OwnerID = Convert.ToInt64(txtOwner.Tag);

        //        oTask.Description = rtxtDescription.Text.Trim();

        //        //Check if the Task is marked complete 
        //        //If yes then set the Task CompleteDate to current Date;
        //        if (numComplete.Value == 100 && DateCompleted == 0)
        //        {
        //            oTask.DateCompleted = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));
        //        }
        //        else if (DateCompleted == 0)
        //        {
        //            oTask.DateCompleted = 0;
        //        }
        //        else
        //        {
        //            oTask.DateCompleted = DateCompleted;
        //        }

        //        oTask.Notes = " ";

        //        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        //        oTask.UserID =Convert.ToInt64(appSettings["UserID"]);
        //        oTask.ClinicID = 0;

        //        Int64 result = 0;

        //        if (TaskID > 0)
        //        {
        //            if (ogloTask.Modify(oTask))
        //            {
        //                //MessageBox.Show("Task Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                this.Close();
        //            }
        //        }
        //        else
        //        {
        //             result = ogloTask.Add(oTask);
        //        }


        //        if (result > 0)
        //        {
        //            //MessageBox.Show("Task Added Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            this.Close();
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        oTask.Dispose();
        //        ogloTask.Dispose();
        //    }

        //}

        //private void modify_Task(Int64 TaskID)
        //{
        //    gloTask ogloTask = new gloTask(_databaseconnectionstring);
        //    Task oTask = new Task();

        //    try
        //    {
        //        oTask = ogloTask.GetTask(TaskID);

        //        if (oTask.OwnerID != oTask.ProviderID)
        //        {
        //            openReadOnly();

        //        }

        //        txtSubject.Text = oTask.Subject;
        //        dtp_StartDate.Value = gloDateMaster.gloDate.DateAsDate(oTask.StartDate);
        //        dtp_EndDate.Value = gloDateMaster.gloDate.DateAsDate(oTask.DueDate);
        //        cmbStatus.SelectedValue = oTask.StatusID;
        //        cmbPriority.SelectedValue = oTask.PriorityID;
        //        cmb_FollowUp.SelectedValue = oTask.FollowupID;
        //        numComplete.Value = oTask.Complete;
        //        txtPatient.Text = oTask.PatientName;
        //        txtPatient.Tag = oTask.PatientID;
        //        txtProvider.Text = oTask.ProviderName;
        //        txtProvider.Tag = oTask.ProviderID;
        //        txtOwner.Text = oTask.OwnerName;
        //        txtOwner.Tag = oTask.OwnerID;
        //        rtxtDescription.Text = oTask.Description;
        //        if (oTask.AssignedToID > 0)
        //        {
        //            txtTo.Tag = oTask.AssignedToID;
        //            txtTo.Text = oTask.AssignedToName;
        //            txtTo.Visible = true;
        //            lblTo.Visible = true;
        //            btnTo.Visible = true;

        //        }
        //        if (oTask.DateCompleted > 0)
        //        {
        //            DateCompleted = oTask.DateCompleted;
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    }
        //}

        #endregion " Task Methods "

        #region " List Control Events "

        //Providers List
        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {

        }
        //Added by Mayuri:20100116-Case No:GLO2008-0001606-Replaced gloListControl by PatientListControl
        private void oPatientListControl_ItemClosedClick(object sender, EventArgs e)
        {
            // this.Width = 723;
            uiPnlTaskDetails.Width = 723;
        }
        private void oPatientListControl_Grid_MouseDown(object sender, EventArgs e)
        {
        }
        private void oPatientListControl_GridRowSelect_Click(object sender, EventArgs e)
        {
        }
        private void oPatientListControl_Grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //this.Width = 723;
                uiPnlTaskDetails.Width = 723;
                txtPatient.Tag = null;
                txtPatient.Text = "";
                txtPatient.Tag = oPatientListControl.SelectedPatientID;

                txtPatient.Text = oPatientListControl.FirstName + " " + oPatientListControl.LastName;
                oPatientListControl.SendToBack();
                oPatientListControl.Visible = false;
                if (PatientID != oPatientListControl.SelectedPatientID)
                {
                    PatientID = oPatientListControl.SelectedPatientID;
                    try
                    {
                        this.Text = "Task ";
                        gloPatient.gloPatient.GetWindowTitle(this, PatientID, _databaseconnectionstring, _messageBoxCaption);
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    }

                    fillUserTask();
                }
                //     this.Controls.Remove(oPatientListControl);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

            }
        }
        //End code Added by Mayuri:20100116
        private void oListControl_ProvidersSelectedClick(object sender, EventArgs e)
        {
            try
            {

                if (oListControl.SelectedItems.Count > 0)
                {

                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {

                        if (oListControl.ControlHeader == "Patient")
                        {
                            txtPatient.Tag = null;
                            txtPatient.Text = "";

                            txtPatient.Tag = oListControl.SelectedItems[i].ID;
                            txtPatient.Text = oListControl.SelectedItems[i].Description;
                        }
                        if (oListControl.ControlHeader == "Provider")
                        {
                            txtProvider.Text = "";
                            txtProvider.Tag = null;

                            txtProvider.Tag = oListControl.SelectedItems[i].ID;
                            txtProvider.Text = oListControl.SelectedItems[i].Description;

                            //txtOwner.Tag = oListControl.SelectedItems[i].ID;
                            //txtOwner.Text = oListControl.SelectedItems[i].Description;


                        }

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

            }
        }

        //Users List
        private void oListUsers_ItemClosedClick(object sender, EventArgs e)
        {


        }

        private void oListUsers_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                //cmb_To.Items.Clear(); 
                DataTable dtUsers = new DataTable();
                DataColumn dcId = new DataColumn("ID");
                DataColumn dcDescription = new DataColumn("Description");

                dtUsers.Columns.Add(dcId);
                dtUsers.Columns.Add(dcDescription);


                ToList = new gloGeneralItem.gloItems();
                gloGeneralItem.gloItem ToItem;

                if (oListUsers.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListUsers.SelectedItems.Count - 1; i++)
                    {
                        DataRow drTemp = dtUsers.NewRow();
                        drTemp["ID"] = oListUsers.SelectedItems[i].ID;
                        drTemp["Description"] = oListUsers.SelectedItems[i].Description;

                        dtUsers.Rows.Add(drTemp);

                        //
                        ToItem = new gloGeneralItem.gloItem();

                        ToItem.ID = oListUsers.SelectedItems[i].ID;
                        ToItem.Description = oListUsers.SelectedItems[i].Description;


                        ToList.Add(ToItem);
                        ToItem.Dispose();
                        ToItem = null;

                        //
                    }
                }





                dtUsers.DefaultView.Sort = "Description";
                dtUsers = dtUsers.DefaultView.ToTable();

                Int64 nDefaultUser = 0;
                if (_defaultuserID > 0)
                {
                    nDefaultUser = _defaultuserID;
                }
                else
                {
                    nDefaultUser = _userID;

                }
                DataRow[] dr = dtUsers.Select("ID ='" + nDefaultUser + "'");
                if (dr.Length > 0)
                {
                    DataRow newRow = dtUsers.NewRow();
                    newRow.ItemArray = dr[0].ItemArray;
                    dtUsers.Rows.Remove(dr[0]);
                    dtUsers.Rows.InsertAt(newRow, 0);

                }
                cmb_To.DataSource = dtUsers;
                cmb_To.ValueMember = dtUsers.Columns["ID"].ColumnName;
                cmb_To.DisplayMember = dtUsers.Columns["Description"].ColumnName;




                oListUsers.IsgloCollectCustomer = false;
                bIncludeAllUsers = oListUsers.bchkIncludeAllUsers;
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }


        #endregion " List Control Events "

        #region " Button Click Events "

        private void btn_Providers_Click(object sender, EventArgs e)
        {


            try
            {

                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ProvidersSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, false, this.Width);
                //oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Provider";

                //_CurrentControlType = gloListControl.gloListControlType.Pharmacy;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ProvidersSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.Dock = DockStyle.Fill;
                this.Controls.Add(oListControl);

                //

                //
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void btn_Patient_Click(object sender, EventArgs e)
        {

            try
            {
                //Commentd by Mayuri:20100216-Case No:GLO2008-0001606
                // onPharmaBrowse_Clicked(sender, e);               
                /// 
                ////if (oListControl != null)
                ////{
                ////    for (int i = this.Controls.Count - 1; i >= 0; i--)
                ////    {
                ////        if (this.Controls[i].Name == oListControl.Name)
                ////        {
                ////            this.Controls.Remove(this.Controls[i]);
                ////            break;
                ////        }
                ////    }
                ////}

                ////oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, false, this.Width);
                //////oListControl.ClinicID = _ClinicID;
                ////oListControl.ControlHeader = "Patient";

                //////_CurrentControlType = gloListControl.gloListControlType.Pharmacy;
                ////oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ProvidersSelectedClick);
                ////oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                ////oListControl.Dock = DockStyle.Fill;
                ////this.Controls.Add(oListControl);

                //

                //
                ////oListControl.OpenControl();
                ////oListControl.Dock = DockStyle.Fill;
                ////oListControl.BringToFront();
                //Added by Mayuri:20100216-To fix case No:GLO2008-0001606
                if (oPatientListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oPatientListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }

                    //for (int i = uiPnlTaskDetails.Controls.Count - 1; i >= 0; i--)
                    //{
                    //    if (uiPnlTaskDetails.Controls[i].Name == oPatientListControl.Name)
                    //    {
                    //        uiPnlTaskDetails.Controls.Remove(this.Controls[i]);
                    //        break;
                    //    }
                    //}
                    try
                    {
                        oPatientListControl.Grid_DoubleClick -= new gloPatient.PatientListControl.GridDoubleClick(oPatientListControl_Grid_DoubleClick);
                        oPatientListControl.GridRowSelect_Click -= new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                        oPatientListControl.Grid_MouseDown -= new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                        oPatientListControl.ItemClosedClick -= new gloPatient.PatientListControl.ItemClosed(oPatientListControl_ItemClosedClick);

                    }
                    catch { }
                    oPatientListControl.Dispose();
                    oPatientListControl = null;
                }
                oPatientListControl = new gloPatient.PatientListControl();
                oPatientListControl.Grid_DoubleClick += new gloPatient.PatientListControl.GridDoubleClick(oPatientListControl_Grid_DoubleClick);
                oPatientListControl.GridRowSelect_Click += new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                oPatientListControl.Grid_MouseDown += new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                oPatientListControl.ItemClosedClick += new gloPatient.PatientListControl.ItemClosed(oPatientListControl_ItemClosedClick);
                oPatientListControl.Dock = DockStyle.Fill;
                // oPatientListControl.Width = uiPnlTaskDetails.Width;
                //  oPatientListControl.Height = uiPnlTaskDetails.Height;    
                //this.Width = 900;
                //  oPatientListControl.ControlHeader = "Patient";
                this.Controls.Add(oPatientListControl);
                // uiPnlTaskDetails.Controls.Add(oPatientListControl);
                oPatientListControl.FillPatients();
                oPatientListControl.ShowOKCancel(true);
                oPatientListControl.ShowHeader(true);
                oPatientListControl.Dock = DockStyle.Fill;
                //  oPatientListControl.Width = uiPnlTaskDetails.Width;
                // oPatientListControl.Height = uiPnlTaskDetails.Height;    
                //    oPatientListControl.OpenControl();
                //  oListControl.Dock = DockStyle.Fill;
                // oListControl.BringToFront();


                oPatientListControl.BringToFront();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void btnTo_Click(object sender, EventArgs e)
        {
            try
            {
                if (oListUsers != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListUsers.Name)
                        {
                           
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        oListUsers.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListUsers_ItemSelectedClick);
                        oListUsers.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListUsers_ItemClosedClick);

                    }
                    catch { }
                    oListUsers.Dispose();
                    oListUsers = null;
                }

                oListUsers = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Users, true, this.Width);

                oListUsers.ControlHeader = "Users";
                oListUsers.IsgloCollectCustomer = true; // Sameer Added For User sorting based on non glocollect and glocollectusers 11/26/2014
                oListUsers.bchkIncludeAllUsers = bIncludeAllUsers;
                oListUsers.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListUsers_ItemSelectedClick);
                oListUsers.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListUsers_ItemClosedClick);
                oListUsers.Dock = DockStyle.Fill;

                // to select already added users
                if (ToList != null)
                {
                    for (int i = 0; i < ToList.Count; i++)
                    {
                        oListUsers.SelectedItems.Add(ToList[i]);
             
            
                    }
                     
                }
                //

                this.Controls.Add(oListUsers);

                //

                //
                oListUsers.OpenControl();
                oListUsers.Dock = DockStyle.Fill;
                oListUsers.BringToFront();


            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btn_ToDel_Click(object sender, EventArgs e)
        {


            Int64 _userId = 0;
            try
            {
                //Remove item from ToList

                _userId = Convert.ToInt64(cmb_To.SelectedValue);
                //SLR: Changed on 4/4/2014
                for (int i = ToList.Count - 1; i >= 0; i--)
                {
                    if (ToList[i].ID == _userId)
                    {
                        ToList.RemoveAt(i);
                    }
                }

                //
                DataTable dtUsers = (DataTable)cmb_To.DataSource;
                dtUsers.Rows.RemoveAt(cmb_To.SelectedIndex);
                dtUsers.AcceptChanges();
                cmb_To.DataSource = dtUsers;
                cmb_To.Refresh();
                //8060 Code Change to avoid exception
                if (dtUsers.Rows.Count > 0)
                {
                    cmb_To.SelectedIndex = 0;
                }

              }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        #endregion " Button Click Events "

        private void numComplete_ValueChanged(object sender, EventArgs e)
        {
            if (numComplete.Value == 0)
            {
                cmbStatus.SelectedValue = Convert.ToInt64(StatusType.NotStarted);
                pnlReminder.Enabled = true;
            }
            else if (numComplete.Value > 0 && numComplete.Value < 100)
            {
                cmbStatus.SelectedValue = Convert.ToInt64(StatusType.InProgress);
                pnlReminder.Enabled = true;
            }
            else if (numComplete.Value == 100)
            {
                cmbStatus.SelectedValue = Convert.ToInt64(StatusType.Completed);
                pnlReminder.Enabled = false;
                chkReminder.Checked = false;
            }
            blnChangeTask = true;
        }

        private void openReadOnly()
        {
            try
            {
                tsb_AssignTo.Visible = false;
                txtSubject.ReadOnly = true;
                dtp_StartDate.Enabled = false;
                dtp_EndDate.Enabled = false;
                cmbStatus.Enabled = false;
                cmbPriority.Enabled = false;
                cmb_FollowUp.Enabled = false;
                numComplete.ReadOnly = true;
                numComplete.Enabled = false;
                txtProvider.ReadOnly = true;
                txtOwner.ReadOnly = true;
                txtPatient.ReadOnly = true;
                btn_Patient.Visible = false;
                btn_Providers.Visible = false;
                btnTo.Visible = false;
                btnTo.Enabled = false;
                pnlRtxtBox.Visible = false;


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #region " Task Methods - New "

        private void CreateTask1()
        {

            Task oTask = new Task();
            TaskAssign oTaskAssign;
            TaskProgress oTaskProgress = new TaskProgress();
            gloTask ogloTask = new gloTask(_databaseconnectionstring);

            try
            {

                if (Validate())
                {

                    #region " Task Master "

                    oTask.TaskID = TaskID;
                    oTask.ProviderID = Convert.ToInt64(txtProvider.Tag);
                    oTask.PatientID = Convert.ToInt64(txtPatient.Tag);
                    oTask.Subject = txtSubject.Text.Trim();
                    oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(dtp_StartDate.Value.ToString("MM/dd/yyyy"));
                    oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(dtp_EndDate.Value.ToString("MM/dd/yyyy"));
                    oTask.PriorityID = Convert.ToInt64(cmbPriority.SelectedValue);
                    oTask.CategoryID = 1;//1-> Task 
                    oTask.FollowupID = Convert.ToInt64(cmb_FollowUp.SelectedValue);
                    oTask.IsPrivate = false;

                    //For Owner of Task 
                    //The user who is logged in will be the owner of the Task which we get from App.Config file
                    oTask.OwnerID = UserID;

                    oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                    oTask.Notes = "";
                    oTask.UserID = UserID;
                    oTask.ClinicID = ClinicID;

                    #endregion " Task Master "

                    #region " Task Assign "

                    if (cmb_To.Visible == true && cmb_To.Items.Count > 0)
                    {
                        for (int i = 0; i < cmb_To.Items.Count; i++)
                        {
                            oTaskAssign = new TaskAssign();
                            cmb_To.SelectedIndex = i;

                            //Set Assign object for Assigned Task
                            oTaskAssign.TaskID = TaskID;
                            oTaskAssign.AssignToID = Convert.ToInt64(cmb_To.SelectedValue);
                            oTaskAssign.AssignFromID = UserID;
                            oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned;
                            oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold;
                            oTaskAssign.ClinicID = ClinicID;

                            oTask.Assignment.Add(oTaskAssign);
                            oTaskAssign.Dispose();
                        }
                    }
                    else
                    {
                        oTaskAssign = new TaskAssign();
                        //Self Task
                        oTaskAssign.TaskID = TaskID;
                        oTaskAssign.AssignToID = 0;
                        oTaskAssign.AssignFromID = 0;
                        oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                        oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                        oTaskAssign.ClinicID = ClinicID;

                        oTask.Assignment.Add(oTaskAssign);
                        oTaskAssign.Dispose();
                    }

                    #endregion " Task Assign "

                    #region " Task Progress "

                    oTask.Progress.TaskID = TaskID;
                    oTask.Progress.StatusID = Convert.ToInt64(cmbStatus.SelectedValue);
                    oTask.Progress.Complete = numComplete.Value;
                    oTask.Progress.Description = rtxtDescription.Text.Trim();
                    oTask.Progress.DateTime = DateTime.Now;
                    oTask.Progress.ClinicID = ClinicID;


                    #endregion " Task Progress "

                    #region " Addding the created Task "

                    Int64 _result = ogloTask.Add(oTask);

                    // Added by Abhijeet on 20100625
                    // Assign new generated task id to Property which save new task id
                    TaskInsertedID = _result;
                    // End of changes by Abhijeet on 20100625

                    if (_result > 0)
                    {
                        MessageBox.Show("Record added successfully.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ERROR : Record not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    #endregion " Addding the created Task "

                }//if(Validate())

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



        }//End : CreateTask 

        private DataTable ShowPatientAllTask(Int64 UserID, Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //  String Query = "SELECT COUNT(*) FROM TM_Task_Assign INNER JOIN TM_TaskMST ON TM_Task_Assign.nTaskID = TM_TaskMST.nTaskID WHERE (TM_Task_Assign.nAcceptRejectHold = 3 OR TM_Task_Assign.nAcceptRejectHold = 4) AND TM_TaskMST.nTaskID = " + TaskID + "  AND TM_Task_Assign.nAssignToID = " + _userID + "";
            gloDatabaseLayer.DBParameters oParamater = new gloDatabaseLayer.DBParameters();

            DataTable _dttask = null;
            try
            {
                oDB.Connect(false);
                oParamater.Add("@nUserId", UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParamater.Add("@nPatientID", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDB.Retrive("getIncompleteTaskList_Patient", oParamater, out _dttask);
                if (_dttask != null)
                {
                    return _dttask;
                }

                oDB.Disconnect();
            }
            catch
            {
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return _dttask;
        }


        private int CompleteAllTask(DataTable dtTaskID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //  String Query = "SELECT COUNT(*) FROM TM_Task_Assign INNER JOIN TM_TaskMST ON TM_Task_Assign.nTaskID = TM_TaskMST.nTaskID WHERE (TM_Task_Assign.nAcceptRejectHold = 3 OR TM_Task_Assign.nAcceptRejectHold = 4) AND TM_TaskMST.nTaskID = " + TaskID + "  AND TM_Task_Assign.nAssignToID = " + _userID + "";
            gloDatabaseLayer.DBParameters oParameter = new gloDatabaseLayer.DBParameters();
            int rowcnt = -1;
            DataTable _dttask = null;
            try
            {
                oDB.Connect(false);
                oParameter.Add("@tvpTaskID", dtTaskID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Structured);
                oParameter.Add("@nDateTime", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                rowcnt = oDB.Execute("gsp_CompleteSelectedTask", oParameter);
                if (rowcnt > 0)
                {
                    for (int i = 0; i < dtTaskID.Rows.Count; i++)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.Task, gloAuditTrail.ActivityType.Modify, "Patient Task Completed", _nPatientID,Convert.ToInt64(dtTaskID.Rows[i][0]),_nProviderID, ActivityOutCome.Success);
                    }
                    
                }
                if (_dttask != null)
                {
                    return rowcnt;
                }

                oDB.Disconnect();
            }
            catch
            {
                return rowcnt;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return rowcnt;
        }



        private void ShowTask(Int64 TaskID)
        {
            gloTask ogloTask = new gloTask(_databaseconnectionstring);
            Task oTask = new Task();
            Reminder oReminder = new Reminder();
            TaskAssign oTaskAssign = new TaskAssign();
            TaskAssigns oTaskAssigns = new TaskAssigns();
            ogloTask.isFromTrackTask = _TrackTask;
            ogloTask.isFromReminder = _ReminderTask;//Developer:Pradeep/Date:01/09/2012/Bug ID:18512/Reason:from textbox was showing blank
            try
            {
                oTask = ogloTask.GetTask(TaskID);
                if ((oTask.UserID != TaskAssigntoID)&&(TaskAssigntoID!=0 ))
                {
                    oTaskAssign = ogloTask.GetTaskAssign(TaskID, TaskAssigntoID );
                }
                else
                {
                    oTaskAssign = ogloTask.GetTaskAssign(TaskID, oTask.UserID);
                }
                   //  oTaskAssign = ogloTask.GetTaskAssign(TaskID, oTask.OwnerID);
                oReminder = oReminder.GetTaskReminder(TaskID, UserID);

                if (oTask != null)
                {
                    txtSubject.Tag = oTask.TaskID;

                    txtProvider.Tag = oTask.ProviderID;
                    txtProvider.Text = oTask.ProviderName;

                    txtPatient.Tag = oTask.PatientID;
                    txtPatient.Text = oTask.PatientName;

                    txtSubject.Text = oTask.Subject;
                    
                    dtp_StartDate.Value = gloDateMaster.gloDate.DateAsDate(oTask.StartDate);
                    dtp_EndDate.Value = gloDateMaster.gloDate.DateAsDate(oTask.DueDate);
                    cmbPriority.SelectedValue = oTask.PriorityID;
                    cmb_FollowUp.SelectedValue = oTask.FollowupID;

                    txtOwner.Text = oTask.OwnerName;
                    txtOwner.Tag = oTask.OwnerID;

                    if (oTaskAssign != null)
                    {
                        //string strusrName = "";
                        //if (oTaskAssign.AssignFromName.Trim() == "")
                        //{
                        //   strusrName = GetUserName(oTaskAssign.AssignToID);
                        //   oTaskAssign.AssignFromName = strusrName; 
                        //}
                        txtFrom.Text = oTaskAssign.AssignFromName;
                    }
                    
                    //Dhruv 20100113
                    ///it was not setting the value as  4 and 5 to the combobox.
                    cmbStatus.SelectedIndexChanged -= new EventHandler(cmbStatus_SelectedIndexChanged);
                    cmbStatus.SelectedValue = oTask.Progress.StatusID.ToString(); //oTask.StatusID;
                    //  cmbStatus.SelectedIndexChanged += new EventHandler(cmbStatus_SelectedIndexChanged);

                    numComplete.ValueChanged -= new EventHandler(numComplete_ValueChanged);
                    numComplete.Value = oTask.Progress.Complete;
                    //  numComplete.ValueChanged +=new EventHandler(numComplete_ValueChanged);
                    //Dhruv
                    rtxtDescription.TextChanged -= rtxtDescription_TextChanged;
                    rtxtDescription.Text = oTask.Progress.Description;
                    // rtxtDescription.TextChanged += rtxtDescription_TextChanged;   

                    _sFaxTiffFileName = oTask.FaxTiffFileName;
                    _nReferenceID1 = oTask.ReferenceID1;
                    _nReferenceID2 = oTask.ReferenceID2;
                    _TaskType = oTask.TaskType;
                    _sNotesExt = oTask.Notes;
                    _ntaskGroupID = oTask.TaskGroupID;
                    // txtFrom.Text = oTaskAssign.AssignFromID;
                    //Sudhir 20090205
                    if (_IsEMREnable == true)
                    {
                        btnreply.Enabled = true;
                        //Enabled = false change made against incident CAS-02366-J7M7G9
                        btnTo.Enabled = false;
                        btn_ToDel.Enabled = false;
                        
                        //Check whether this task is Accepted or not for current UserID
                        tsb_OK.Enabled = true;  //added for bugid 76093
                        tsbbtn_OnlySave.Enabled = true;
                        if (IsTaskOnHold(_taskID) == true)
                        {
                            tsb_AcceptTask.Visible = true;
                            btnreply.Enabled = false;
                            btnTo.Enabled = true;
                            btn_ToDel.Enabled = true; 
                            tsb_DeclineTask.Visible = true;
                            tsb_OK.Enabled = false;
                            tsbbtn_OnlySave.Enabled = false;
                            tsb_MergeOrder.Visible = false;
                        }
                        //00000926 : Added condition for Assigned Lab order task
                        else if (oTask.TaskType == TaskType.LabOrder || oTask.TaskType == TaskType.AssignedLabOrder)
                        {
                            tsb_LabReport.Visible = true;
                        }
                        else if (oTask.TaskType == TaskType.PlaceLabOrder)
                        {
                            tsb_ShowLab.Visible = true;
                            btn_Patient.Enabled = false;
                            tsb_LabOrder.Visible = true;
                        }
                        //00000926 : Added condition for Assigned DMS document task
                        else if (oTask.TaskType == TaskType.DMS || oTask.TaskType == TaskType.AssignedDMS || oTask.TaskType == TaskType.RCMDocs)
                        {

                            tsb_ShowDoc.Visible = true;
                            btn_Patient.Enabled = false; //code added in 6020 to disable patient selection
                        }
                        else if (oTask.TaskType == TaskType.OrderRadiology)
                        {
                            tsb_ShowOrder.Visible = true;
                            btn_Patient.Enabled = false;//code added in 6020 to disable patient selection
                        }
                        else if (oTask.TaskType == TaskType.Exam)
                        {

                            tsb_ShowExam.Visible = true;
                            btn_Patient.Enabled = false;//code added in 6020 to disable patient selection
                        }
                        else if (oTask.TaskType == TaskType.UnmatchedPatient) //Added by kanchan on 20100303 for Emdeon
                        {

                            tsb_MatchPatient.Visible = true; //Added by kanchan on 20100303 for Emdeon
                            btn_Patient.Enabled = false;//code added in 6020 to disable patient selection
                        }
                        else if (oTask.TaskType == TaskType.CCDUnmatchedPatient) //Added by kanchan on 20100605 for CCD
                        {

                            tsb_ShowCCD.Visible = true;                              //Added by kanchan on 20100605 for CCD
                            btn_Patient.Enabled = false;//code added in 6020 to disable patient selection
                        }
                        else if (oTask.TaskType == TaskType.CCD)
                        {
                            tsb_Reconcile.Visible = true;
                            tsb_ShowCCD.Visible = true;                              //Added by kanchan on 20100605 for CCD
                            btn_Patient.Enabled = false;//code added in 6020 to disable patient selection
                        }
                        else if (oTask.TaskType == TaskType.ImReconcile)
                        {
                            tsb_Reconcile.Visible = true;
                        }
                        else if (oTask.TaskType == TaskType.Flowsheet)  //Added by kanchan on 20100619 for Flowsheet
                        {
                            tsb_FlowSheet.Visible = true;
                            btn_Patient.Enabled = false;//code added in 6020 to disable patient selection
                        }
                        else if (oTask.TaskType == TaskType.Drug)       //Added by kanchan on 20100619 for Drug
                        {
                            // Bug #52566: gloEMR-Task - Drug Available >>Application is opening the blank rx meds window for drug available task
                            // Following condition added to resolve the issue.
                            if (oTask.Notes != string.Empty)
                            {
                                tsb_Drugs.Visible = true;
                                btn_Patient.Enabled = false;//code added in 6020 to disable patient selection
                            }
                        }
                        else if (oTask.TaskType == TaskType.ExternalChargesPatientConflict || oTask.TaskType == TaskType.ExternalChargesPatientNotFound) //Added by Abhijeet on 20111004
                        {
                            btn_Patient.Enabled = false;
                        }
                        else if (oTask.TaskType == TaskType.HL7LabInboundFailureNotifyTask) //Added by Abhijeet on 20111123
                        {
                            tsb_ViewReport.Visible = true;
                            btn_Patient.Enabled = false;
                        }
                        else if (oTask.TaskType == TaskType.PatientPortalTask) //Added by Abhijeet on 20111224
                        {
                            tsb_ReviewPatient.Visible = true;
                            btn_Patient.Enabled = false;
                        }
                        else if (oTask.TaskType == TaskType.Vitals) //Added by Abhijeet on 20111224
                        {
                            tsb_Vitals.Visible = true;
                            btn_Patient.Enabled = false;
                        }
                        else if (oTask.TaskType == TaskType.IntuitSecureMessageTask)
                        {
                            tsb_ViewMessage.Visible = true;
                            tsb_Reply.Visible = true;
                            tsb_Forward.Visible = true;
                            btn_Patient.Enabled = false;
                        }
                        else if ((oTask.TaskType == TaskType.IntuitAppointmentTask) || (oTask.TaskType == TaskType.IntuitRxRenewalTask) || (oTask.TaskType == TaskType.IntuitBillPayTask))
                        {
                            if (_messageBoxCaption == "gloEMR")
                            {
                                tsb_Reply.Visible = true;
                                tsb_ViewMessage.Visible = true;
                                if (oTask.TaskType == TaskType.IntuitRxRenewalTask)
                                {
                                    tsb_RxMeds.Visible = true;
                                }

                            }
                            else
                            {
                                if (oTask.TaskType == TaskType.IntuitBillPayTask)
                                {
                                    if (oTask.Progress.Complete == 100)
                                    {
                                        tsb_PatientPayment.Visible = false;
                                    }
                                    else
                                    {
                                        tsb_PatientPayment.Visible = true;
                                    }
                                }
                            }
                            tsb_Forward.Visible = true;
                            btn_Patient.Enabled = false;
                        }
                        else if (oTask.TaskType == TaskType.HL7DocumentTask) // Added By Manoj Jadhav on 20130219 to display DMS document from task
                        {
                            tsb_ShowDoc.Tag = "HL7DocumentTask";
                            tsb_ShowDoc.Visible = true;
                        }
                        else if (oTask.TaskType == TaskType.UnsolicitedTask)
                        {
                            tsb_MergeOrder.Visible = true;
                        }
                        else if (oTask.TaskType == TaskType.PatientPortalPatientFormTask) 
                        {
                            Int16 nShowButton = GetPFTaskDownloadFormat(_nReferenceID2);

                            if (nShowButton > 0)
                            {
                                if (nShowButton == 1)
                                {
                                    tsb_ShowHistory.Visible = true;
                                    tsb_ShowROS.Visible = true;
                                   
                                }
                                else if (nShowButton == 2)
                                {
                                    tsb_ShowDoc.Tag = "PFPortalTask";
                                    tsb_ShowDoc.Visible = true;
                                }
                                else if (nShowButton == 3)
                                {
                                    tsb_ShowDoc.Tag = "PFPortalTask";
                                    tsb_ShowDoc.Visible = true;
                                }
                                else if (nShowButton == 4)
                                {
                                    tsb_ShowHistory.Visible = true;
                                    tsb_ShowROS.Visible = true;
                                    tsb_ShowDoc.Tag = "PFPortalTask";
                                    tsb_ShowDoc.Visible = true;
                                }
                            }
                            else if (nShowButton==0)
                            {
                                //Bug #93993: Static patient form:Show history button is missing
                                //added else condition to resolve the issue for static form.
                                tsb_ShowHistory.Visible = true;
                                tsb_ShowROS.Visible = true;


                            }

                            btn_Patient.Enabled = false;
                        }
                        if (oTask.TaskType == TaskType.PortalPHI)
                        {
                            tsb_ReviewPHI.Visible = true;
                        }

                        if (IsTaskDeleted(_taskID, _userID) == true)
                        {
                            tsb_OK.Enabled = false;
                            tsbbtn_OnlySave.Enabled = false;
                            tsb_MergeOrder.Visible = false;
                        }
                    }
                    else
                    {
                        if (IsTaskDeleted(_taskID, oTaskAssign.AssignToID) == true)
                        {
                            tsb_OK.Enabled = false;
                            tsbbtn_OnlySave.Enabled = false;
                            tsb_MergeOrder.Visible = false;
                        }
                        else //added else condition fro bugid 98977
                        {
                            tsb_OK.Enabled = true;
                            tsbbtn_OnlySave.Enabled = true;
                           
                        }
                    }

                    //if (oTask.AssignedToID > 0)
                    //{
                    //    txtTo.Tag = oTask.AssignedToID;
                    //    txtTo.Text = oTask.AssignedToName;
                    //    txtTo.Visible = true;
                    //    lblTo.Visible = true;
                    //    btnTo.Visible = true;

                    //}
                    //if (oTask.DateCompleted > 0)
                    //{
                    //    DateCompleted = oTask.DateCompleted;
                    //}


                    if (oReminder != null)
                    {
                        chkReminder.Checked = true;
                        dtpReminderDate.Value = oReminder.ReminderDate;
                        dtpReminderTime.Value = oReminder.ReminderTime;
                    }
                    else
                    {
                        chkReminder.Checked = false;
                    }
                    //Added By Me
                    if (Convert.ToString(oTask.Progress.Complete) == "100" && oTask.Progress.StatusID.ToString() == "3")
                    {
                        pnlReminder.Enabled = false;
                        //ChkCompleteTaskforallUsers.Visible=false;
                    }

                    if (oTask.TaskGroupID != 0)
                    {
                        if (_HasRightToCompleteTaskForAllUsers == true)
                        {
                            if (ogloTask.IsMultipleUserTask(oTask.TaskGroupID) == true)
                            {
                                ChkCompleteTaskforallUsers.Visible = true;
                                if (Convert.ToString(oTask.Progress.Complete) == "100" && oTask.Progress.StatusID.ToString() == "3")
                                {
                                    ChkCompleteTaskforallUsers.Enabled = true;
                                }
                                else
                                {
                                    ChkCompleteTaskforallUsers.Enabled = false;
                                }
                            }
                        }
                    }

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            finally
            {
               //changes made for bugid 81069 TO field should not be empty
                if (dtUsers != null)
                {
                    if ((dtUsers.Columns.Contains("ID")) && (dtUsers.Columns.Contains("Description")))
                 {
                     dtUsers.Rows.Clear();
                     DataRow dr = dtUsers.NewRow() ;
                     string strUserName = "";
                        if (oTaskAssign != null)
                     {
                        strUserName = GetUserName(oTaskAssign.AssignToID);
                     }

                      
                        
                        if (strUserName.Trim() != "")
                     {
                         dr["ID"] = oTaskAssign.AssignToID;
                         dr["Description"] = strUserName;
                     }

                         
                        else
                        {
                            dr["ID"] = _userID;
                            dr["Description"] = _UserName;
                            
                        }
                           dtUsers.Rows.Add(dr);   
                    cmb_To.DataSource = dtUsers;
                    cmb_To.ValueMember = dtUsers.Columns["ID"].ColumnName;
                    cmb_To.DisplayMember = dtUsers.Columns["Description"].ColumnName;
                 }
                }
            }
        }

        private Int16 GetPFTaskDownloadFormat(long nPatientFormID)
        {
            Int16 nDownloadFormat = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBps = new gloDatabaseLayer.DBParameters();
            DataTable dtFormDetails = new DataTable();
            try
            {
                oDB.Connect(false);


                oDBps.Add("@nPatientFormID", nPatientFormID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_GetPatientFormDownloadFormat", oDBps, out dtFormDetails);

                oDB.Disconnect();

                oDB.Dispose();
                oDB = null;
                if (dtFormDetails != null && dtFormDetails.Rows.Count > 0)
                {
                    nDownloadFormat= Convert.ToInt16(dtFormDetails.Rows[0]["sDownloadFormat"]);
                }
                return nDownloadFormat;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return nDownloadFormat;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return nDownloadFormat;
            }
            finally
            {

                dtFormDetails.Dispose();
                dtFormDetails = null;

                if (oDBps != null)
                {
                    oDBps.Dispose();
                    oDBps = null;
                }

                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }


        private bool IsTaskOnHold(Int64 TaskID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String Query = "SELECT COUNT(*) FROM TM_Task_Assign INNER JOIN TM_TaskMST ON TM_Task_Assign.nTaskID = TM_TaskMST.nTaskID WHERE (TM_Task_Assign.nAcceptRejectHold = 3 OR TM_Task_Assign.nAcceptRejectHold = 4) AND TM_TaskMST.nTaskID = " + TaskID + "  AND TM_Task_Assign.nAssignToID = " + _userID + "";
            Object val;
            bool result = false;
            try
            {
                oDB.Connect(false);
                val = oDB.ExecuteScalar_Query(Query);
                if ((Int32)val > 0)
                {
                    result = true;
                    pnlFields.Enabled = false;
                }
                else
                {
                    pnlFields.Enabled = true;
                }

                oDB.Disconnect();
            }
            catch
            {
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return result;
        }
        private bool IsTaskDeleted(Int64 TaskID, Int64 nAssignToID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String Query = "SELECT COUNT(*) FROM TM_Task_Assign INNER JOIN TM_TaskMST ON TM_Task_Assign.nTaskID = TM_TaskMST.nTaskID WHERE ( TM_Task_Assign.nAcceptRejectHold = 4) AND TM_TaskMST.nTaskID = " + TaskID + "  AND TM_Task_Assign.nAssignToID = " + nAssignToID + "";
            Object val;
            bool result = false;
            try
            {
                oDB.Connect(false);
                val = oDB.ExecuteScalar_Query(Query);
                if ((Int32)val > 0)
                {
                    result = true;
                }

                oDB.Disconnect();
            }
            catch
            {
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return result;
        }


        private bool Validate()
        {
            if (cmb_To.Visible == true && cmb_To.Items.Count <= 0)
            {
                MessageBox.Show("Assigned person cannot be blank.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnTo.Focus();
                return false;
            }

            if (txtSubject.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the task subject.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSubject.Focus();
                return false;
            }

            if (dtp_EndDate.Value.Date.CompareTo(dtp_StartDate.Value.Date) < 0)
            {
                MessageBox.Show("The due date selected is passed. Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                dtp_EndDate.Focus();

                return false;
            }


            if (chkReminder.Checked == true)
            {
                if (dtpReminderDate.Value.Date.CompareTo(dtp_StartDate.Value.Date) < 0)//develper:Pradeep/date:03/05/2012/bug:21601
                {
                    MessageBox.Show("The reminder date selected is less than start date. Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpReminderDate.Focus();
                    return false;
                }
            }

            return true;
        }




        private void CreateTask()
        {

            Task oTask = new Task();
            TaskAssign oTaskAssign;
            TaskProgress oTaskProgress = new TaskProgress();
            gloTask ogloTask = new gloTask(_databaseconnectionstring);
            ogloTask.isFromTrackTask = _TrackTask;
            ogloTask.isSmartTask = _SmartTask;
            ogloTask.isTaskForwardedFromIntuitMessage = _isTaskForwardedFromIntuitMessage;
            try
            {

                if (Validate())
                {

                    #region " Task Master "


                    oTask.TaskID = TaskID;
                    oTask.ProviderID = Convert.ToInt64(txtProvider.Tag);
                    oTask.PatientID = Convert.ToInt64(txtPatient.Tag);
                    oTask.Subject = txtSubject.Text.Trim();
                    oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(dtp_StartDate.Value.ToString("MM/dd/yyyy"));
                    oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(dtp_EndDate.Value.ToString("MM/dd/yyyy"));
                    oTask.PriorityID = Convert.ToInt64(cmbPriority.SelectedValue);
                    oTask.CategoryID = 1;//1-> Task 
                    oTask.FollowupID = Convert.ToInt64(cmb_FollowUp.SelectedValue);
                    oTask.IsPrivate = false;

                    //For Owner of Task 
                    //The user who is logged in will be the owner of the Task which we get from App.Config file

                    //condition added for editing task from track tasks -pradeep 20100721
                    //if(_TrackTask ==true )
                    //{
                    //    oTask.OwnerID = 1;
                    //}
                    //else
                    //{
                    //Developer:Pradeep 
                    //Date:12/23/2011
                    //Bug ID/PRD Name/Salesforce Case: 17298 and 17349
                    //Reason:residual issue as owneid was going wrong
                    ////if (_TaskType == TaskType.LabOrder)
                    ////{
                    ////    oTask.OwnerID = GetUserID(txtFrom.Text.Trim());
                    ////}
                    ////else
                    ////{
                    oTask.OwnerID = UserID;
                    ////}
                    //}


                    oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                    oTask.Notes = _sNotesExt;
                    //oTask.Notes = _Notes;
                    oTask.UserID = UserID;
                    oTask.ClinicID = ClinicID;

                    oTask.TaskType = _TaskType;
                    oTask.FaxTiffFileName = "";
                    oTask.MachineName = System.Environment.MachineName;
                    oTask.FaxTiffFileName = _sFaxTiffFileName;
                    oTask.ReferenceID1 = _nReferenceID1;
                    oTask.ReferenceID2 = _nReferenceID2;
                    if (TaskID <= 0)
                    {
                        oTask.TaskGroupID = ogloTask.GetUniqueueId();
                    }
                    else
                    {
                        oTask.TaskGroupID = _ntaskGroupID;
                    }
                    #endregion " Task Master "

                    #region " Task Assign "

                    //if (cmb_To.Visible == true && cmb_To.Items.Count > 0)
                    //{
                    //    for (int i = 0; i < cmb_To.Items.Count; i++)
                    //    {
                    //        oTaskAssign = new TaskAssign();
                    //        cmb_To.SelectedIndex = i;

                    //        //Set Assign object for Assigned Task
                    //        oTaskAssign.TaskID = TaskID;
                    //        oTaskAssign.AssignToID = Convert.ToInt64(cmb_To.SelectedValue);
                    //        oTaskAssign.AssignFromID = UserID;
                    //        oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned;
                    //        oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold;
                    //        oTaskAssign.ClinicID = ClinicID;

                    //        oTask.Assignment.Add(oTaskAssign);
                    //        oTaskAssign.Dispose();
                    //    }
                    //}
                    //else
                    //{
                    //    oTaskAssign = new TaskAssign();
                    //    //Self Task
                    //    oTaskAssign.TaskID = TaskID;
                    //    oTaskAssign.AssignToID = 0;
                    //    oTaskAssign.AssignFromID = 0;
                    //    oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                    //    oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                    //    oTaskAssign.ClinicID = ClinicID;

                    //    oTask.Assignment.Add(oTaskAssign);
                    //    oTaskAssign.Dispose();
                    //}


                    if (cmb_To.Visible == true && cmb_To.Items.Count > 0)
                    {
                        for (int i = 0; i < cmb_To.Items.Count; i++)
                        {
                            oTaskAssign = new TaskAssign();
                            cmb_To.SelectedIndex = i;

                            //Set Assign object for Assigned Task
                            oTaskAssign.TaskID = TaskID;
                            oTaskAssign.AssignToID = Convert.ToInt64(cmb_To.SelectedValue);
                            //Developer:Pradeep/Date:02/22/2011/Bug ID/PRD Name/Salesforce Case: 21251/Reason:Wrong user is displayed in "From" field when same task is forwarded
                            if (_TrackTask == false)
                            {
                                if (oTaskAssign.AssignToID != UserID && GetUserID(txtFrom.Text.Trim()) != UserID)
                                {
                                    oTaskAssign.AssignFromID = UserID;
                                }
                                else
                                {
                                    oTaskAssign.AssignFromID = GetUserID(txtFrom.Text.Trim());
                                }
                            }
                            else
                            {
                                oTaskAssign.AssignFromID = GetUserID(txtFrom.Text.Trim());
                            }


                            //Changed by Mayuri:20100624-To fix case No:#0005897
                            //if (oTaskAssign.AssignToID == oTaskAssign.AssignFromID)
                            if (oTaskAssign.AssignToID == UserID)
                            {
                                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                            }
                            else
                            {
                                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned;
                                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold;
                                //Developer:Pradeep 
                                //Date:12/23/2011
                                //Bug ID/PRD Name/Salesforce Case: 17298 and 17349
                                //Reason:residual issue as owneid was going wrong
                                // oTaskAssign.AssignFromID = UserID;
                            }

                            //Sandip Darade 20100405
                            ////if login user is task assign 
                            // if (oTaskAssign.AssignToID == UserID )
                            //{
                            //    oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                            //    oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                            //}
                            oTaskAssign.ClinicID = ClinicID;

                            oTask.Assignment.Add(oTaskAssign);
                            oTaskAssign.Dispose();
                        }
                    }
                    else
                    {

                        oTaskAssign = new TaskAssign();
                        //Self Task
                        oTaskAssign.TaskID = TaskID;
                        oTaskAssign.AssignToID = 0;
                        oTaskAssign.AssignFromID = 0;
                        oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                        oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                        oTaskAssign.ClinicID = ClinicID;

                        oTask.Assignment.Add(oTaskAssign);
                        oTaskAssign.Dispose();
                    }

                    #endregion " Task Assign "

                    #region " Task Progress "

                    oTask.Progress.TaskID = TaskID;
                    oTask.Progress.StatusID = Convert.ToInt64(cmbStatus.SelectedValue);
                    oTask.Progress.Complete = numComplete.Value;
                    oTask.Progress.Description = rtxtDescription.Text.Trim();
                    oTask.Progress.DateTime = DateTime.Now;
                    oTask.Progress.ClinicID = ClinicID;


                    #endregion " Task Progress "

                    if (TaskID > 0)
                    {
                        #region " Modify Task "
                        if (Convert.ToBoolean(ogloTask.Modify(oTask)))
                        {
                            SaveReminder(oTask, true);
                            //MessageBox.Show("Record Modified Successfully.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //if (C1PatTask.Rows.Count <= 1)
                            //{
                            //    this.Close();
                            //}
                        }
                        #endregion " Modify Task "
                    }
                    else
                    {
                        #region " Addding the created Task "

                        Int64 _result = ogloTask.Add(oTask);

                        // Added by Abhijeet on 20100625
                        // Assign new generated task id to Property which save new task id
                        TaskInsertedID = _result;
                        TaskGroupID = oTask.TaskGroupID;
                        // End of changes by Abhijeet on 20100625

                        //check the autoaccept settings

                        //AcceptTask(TaskInsertedID);

                        if (_result > 0)
                        {
                            if (chkReminder.Checked == true)
                            {
                                oTask.TaskID = _result;
                                //For New Reminder second arg --> false 
                                SaveReminder(oTask, false);
                            }
                            _taskID = _result; //Added by Rahul on 20101025 for Smart Order
                            //MessageBox.Show("Record Added Successfully.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //if (C1PatTask.Rows.Count <= 1)
                            //{
                            //    this.Close();
                            //}
                        }
                        else
                        {
                            MessageBox.Show("ERROR : Record not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }

                        #endregion " Addding the created Task "
                    }

                }//if(Validate())

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }

        private bool blnCreateTask()
        {

            Task oTask = new Task();
            TaskAssign oTaskAssign;
            TaskProgress oTaskProgress = new TaskProgress();
            gloTask ogloTask = new gloTask(_databaseconnectionstring);
            Int64 PrevTaskGroupID = 0;
            ogloTask.isFromTrackTask = _TrackTask;
            ogloTask.isSmartTask = _SmartTask;
            ogloTask.isTaskForwardedFromIntuitMessage = _isTaskForwardedFromIntuitMessage;
            bool _localistaskreplyed = _istaskreplyed;
            if (tempusername.Trim() == "")
            {
                tempusername = txtFrom.Text;  
            }
            try
            {

                if ((ChkCompleteTaskforallUsers.Checked == true) && (ChkCompleteTaskforallUsers.Visible == true) && (ChkCompleteTaskforallUsers.Enabled == true))//added for bugid 79096
                {
                    _IsCompleteAllTask = true;
                }
                else
                {
                    _IsCompleteAllTask = false;
                }


                if (Validate())
                {

                    #region " Task Master "


                    oTask.TaskID = TaskID;
                    oTask.ProviderID = Convert.ToInt64(txtProvider.Tag);
                    oTask.PatientID = Convert.ToInt64(txtPatient.Tag);
                    oTask.Subject = txtSubject.Text.Trim();
                    oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(dtp_StartDate.Value.ToString("MM/dd/yyyy"));
                    oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(dtp_EndDate.Value.ToString("MM/dd/yyyy"));
                    oTask.PriorityID = Convert.ToInt64(cmbPriority.SelectedValue);
                    oTask.CategoryID = 1;//1-> Task 
                    oTask.FollowupID = Convert.ToInt64(cmb_FollowUp.SelectedValue);
                    oTask.IsPrivate = false;

                    //For Owner of Task 
                    //The user who is logged in will be the owner of the Task which we get from App.Config file

                    //condition added for editing task from track tasks -pradeep 20100721
                    //if(_TrackTask ==true )
                    //{
                    //    oTask.OwnerID = 1;
                    //}
                    //else
                    //{
                    //Developer:Pradeep 
                    //Date:12/23/2011
                    //Bug ID/PRD Name/Salesforce Case: 17298 and 17349
                    //Reason:residual issue as owneid was going wrong
                    ////if (_TaskType == TaskType.LabOrder)
                    ////{
                    ////    oTask.OwnerID = GetUserID(txtFrom.Text.Trim());
                    ////}
                    ////else
                    ////{
                    oTask.OwnerID = UserID;
                    ////}
                    //}


                    oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                    oTask.Notes = _sNotesExt;
                    //oTask.Notes = _Notes;
                    oTask.UserID = UserID;
                    oTask.ClinicID = ClinicID;

                    oTask.TaskType = _TaskType;
                    oTask.FaxTiffFileName = "";
                    oTask.MachineName = System.Environment.MachineName;
                    oTask.FaxTiffFileName = _sFaxTiffFileName;
                    oTask.ReferenceID1 = _nReferenceID1;
                    oTask.ReferenceID2 = _nReferenceID2;
                    //if (cmb_To.Visible == true && CheckTaskUsers() > 1)
                    if (cmb_To.Visible == true && cmb_To.Items.Count > 1)
                    {
                        if (_ntaskGroupID == 0)
                            oTask.TaskGroupID = ogloTask.GetUniqueueId();
                        else
                            oTask.TaskGroupID = _ntaskGroupID;
                        //added task groupid if reply then change groupid
                        if (_istaskreplyed == true) // task group
                        {
                            PrevTaskGroupID = oTask.TaskGroupID;
                            oTask.TaskGroupID = ogloTask.GetUniqueueId();
                        }
                    }
                    else
                    {
                        if (_istaskreplyed == true) 
                        {
                            PrevTaskGroupID = _ntaskGroupID;
                            oTask.TaskGroupID = 0;
                        }
                        else
                        {
                            oTask.TaskGroupID = _ntaskGroupID;  //if prev task is edited
                        }
                        }
                    #endregion " Task Master "

                    #region " Task Assign "

                    //if (cmb_To.Visible == true && cmb_To.Items.Count > 0)
                    //{
                    //    for (int i = 0; i < cmb_To.Items.Count; i++)
                    //    {
                    //        oTaskAssign = new TaskAssign();
                    //        cmb_To.SelectedIndex = i;

                    //        //Set Assign object for Assigned Task
                    //        oTaskAssign.TaskID = TaskID;
                    //        oTaskAssign.AssignToID = Convert.ToInt64(cmb_To.SelectedValue);
                    //        oTaskAssign.AssignFromID = UserID;
                    //        oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned;
                    //        oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold;
                    //        oTaskAssign.ClinicID = ClinicID;

                    //        oTask.Assignment.Add(oTaskAssign);
                    //        oTaskAssign.Dispose();
                    //    }
                    //}
                    //else
                    //{
                    //    oTaskAssign = new TaskAssign();
                    //    //Self Task
                    //    oTaskAssign.TaskID = TaskID;
                    //    oTaskAssign.AssignToID = 0;
                    //    oTaskAssign.AssignFromID = 0;
                    //    oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                    //    oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                    //    oTaskAssign.ClinicID = ClinicID;

                    //    oTask.Assignment.Add(oTaskAssign);
                    //    oTaskAssign.Dispose();
                    //}



                    if (cmb_To.Visible == true && cmb_To.Items.Count > 0)
                    {
                        bool blnfoundreplyedtaskusers = false ;
                        for (int i = 0; i < cmb_To.Items.Count; i++)
                        {
                            oTaskAssign = new TaskAssign();
                            cmb_To.SelectedIndex = i;



                            //Set Assign object for Assigned Task
                            oTaskAssign.TaskID = TaskID;
                            oTaskAssign.AssignToID = Convert.ToInt64(cmb_To.SelectedValue);
                            //Developer:Pradeep/Date:02/22/2011/Bug ID/PRD Name/Salesforce Case: 21251/Reason:Wrong user is displayed in "From" field when same task is forwarded
                            if (_TrackTask == false)
                            {
                                if (oTaskAssign.AssignToID != UserID && GetUserID(tempusername.Trim()) != UserID)
                                {
                                    oTaskAssign.AssignFromID = UserID;
                                }
                                else
                                {
                                    oTaskAssign.AssignFromID = GetUserID(tempusername.Trim());
                                }
                            }
                            else
                            {
                                oTaskAssign.AssignFromID = GetUserID(tempusername.Trim());
                            }


                            //Changed by Mayuri:20100624-To fix case No:#0005897
                            //if (oTaskAssign.AssignToID == oTaskAssign.AssignFromID)
                            if (oTaskAssign.AssignToID == UserID)
                            {
                                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                            }
                            else
                            {
                                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned;
                                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold;
                                //Developer:Pradeep 
                                //Date:12/23/2011
                                //Bug ID/PRD Name/Salesforce Case: 17298 and 17349
                                //Reason:residual issue as owneid was going wrong
                                // oTaskAssign.AssignFromID = UserID;
                            }

                            //Sandip Darade 20100405
                            ////if login user is task assign 
                            // if (oTaskAssign.AssignToID == UserID )
                            //{
                            //    oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                            //    oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                            //}
                            oTaskAssign.ClinicID = ClinicID;
                            if ((_istaskreplyed == true) && (Convert.ToInt64(cmb_To.SelectedValue) == _replyedtaskSelectedUSerID))
                            {
                                oTaskAssign.AssignFromID = GetUserID(tempusername.Trim());
                                oTaskAssign.AssignToID = Convert.ToInt64(_replyedtaskSelectedUSerID);
                                _istaskreplyed = false;

                                //if (oTaskAssign.AssignToID == Convert.ToInt64(_replyedtaskSelectedUSerID))
                                //{
                                //    oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                                //    oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                                //}
                                //else
                                //{
                                //    oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned;
                                //    oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold;

                                //}
                                blnfoundreplyedtaskusers = true;
                                //oTask.Progress.StatusID =
                                //oTask.Progress.Complete = numComplete.Value;
                                //oTask.Progress.Description = rtxtDescription.Text.Trim();
                                //oTask.Progress.DateTime = DateTime.Now;
                                //oTask.Progress.ClinicID = ClinicID;
                              //while replying task status must be inprogress 
                               UpdateReplytask_assign(oTaskAssign.AssignToID, oTaskAssign.AssignFromID, TaskID, oTask, 2, 25, rtxtDescription.Text.Trim(), DateTime.Now, PrevTaskGroupID);
                               if (_loginuserreplyedtaskUserID != 0)
                               {
                                   oTask.UserID = Convert.ToInt64(_loginuserreplyedtaskUserID);
                                   oTask.OwnerID = Convert.ToInt64(_loginuserreplyedtaskUserID);
                               }
                               else
                               {
                                   oTask.UserID = Convert.ToInt64(_replyedtaskSelectedUSerID);
                                   oTask.OwnerID = Convert.ToInt64(_replyedtaskSelectedUSerID);
                               }
                                   // oTask.Assignment.Add(oTaskAssign);
                              //  oTaskAssign.Dispose();
                            }
                            else
                            {
                                if ((oTask.UserID != TaskAssigntoID) && (TaskAssigntoID != 0) && _istaskreplyed == false && cmb_To.Items.Count ==1  )   //added for bugid 106912
                                {
                                    // oTask.UserID=TaskAssigntoID;
                                    //oTaskAssign.AssignFromID = GetUserID(txtFrom.Text);
                                    //oTaskAssign.AssignToID = GetUserID(cmb_To.Text) ;
                                    //oTask.OwnerID = TaskAssigntoID;
                                
                                    UpdateTaskMST(TaskID, oTask, Convert.ToInt64(cmbStatus.SelectedValue), numComplete.Value, rtxtDescription.Text.Trim(), DateTime.Now, PrevTaskGroupID);
                                    return true;
                                }
                                else
                                {
                                    oTask.Assignment.Add(oTaskAssign);
                                    oTaskAssign.Dispose();
                                }
                                }
                        }
                        
                        if ((blnfoundreplyedtaskusers == false) && (_localistaskreplyed==true)) //if task owner user not found while replying task i.e it is removed from cmb_to then complete that task 
                         {
                             if (PrevTaskGroupID != 0)
                             {
                                 //if condition added for incident CAS-02366-J7M7G9
                                 CompletetaskifOwnerNotFoundwhileReplying(PrevTaskGroupID);
                            
                             }
                          }
                    }

                    else
                    {

                        oTaskAssign = new TaskAssign();
                        //Self Task
                        oTaskAssign.TaskID = TaskID;
                        oTaskAssign.AssignToID = 0;
                        oTaskAssign.AssignFromID = 0;
                        oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                        oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                        oTaskAssign.ClinicID = ClinicID;

                        oTask.Assignment.Add(oTaskAssign);
                        oTaskAssign.Dispose();
                    }


                    #endregion " Task Assign "

                    #region " Task Progress "

                    oTask.Progress.TaskID = TaskID;
                  
                    if (_localistaskreplyed == true)
                    {
                        oTask.Progress.Complete =25; //setting status to inprogress only while replying task 
                        oTask.Progress.StatusID = 2;
                    }
                    else
                    {
                        oTask.Progress.Complete = numComplete.Value;
                        oTask.Progress.StatusID = Convert.ToInt64(cmbStatus.SelectedValue);
                    }
                        oTask.Progress.Description = rtxtDescription.Text.Trim();
                    oTask.Progress.DateTime = DateTime.Now;
                    oTask.Progress.ClinicID = ClinicID;


                    #endregion " Task Progress "

                    if (TaskID > 0)
                    {
                        #region " Modify Task "
                        if (Convert.ToBoolean(ogloTask.Modify(oTask)))
                        {
                            if (status!= Convert.ToInt16(cmbStatus.SelectedValue) && Convert.ToInt16(cmbStatus.SelectedValue) == 3)
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.Task, gloAuditTrail.ActivityType.Modify, "Patient Task Completed", oTask.PatientID, oTask.TaskID, oTask.ProviderID, gloAuditTrail.ActivityOutCome.Success);
                            }
                            else
                            {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.Task, gloAuditTrail.ActivityType.Modify, "Patient Task Modified", oTask.PatientID, oTask.TaskID, oTask.ProviderID, gloAuditTrail.ActivityOutCome.Success);
                            }
                           SaveReminder(oTask, true);
                            //MessageBox.Show("Record Modified Successfully.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //if (C1PatTask.Rows.Count <= 1)
                            //{
                            //    this.Close();
                            //}
                        }
                        #endregion " Modify Task "
                    }
                    else
                    {
                        #region " Addding the created Task "

                        Int64 _result = ogloTask.Add(oTask);

                        // Added by Abhijeet on 20100625
                        // Assign new generated task id to Property which save new task id
                        TaskInsertedID = _result;
                        TaskGroupID = oTask.TaskGroupID;
                        // End of changes by Abhijeet on 20100625

                        //check the autoaccept settings

                        //AcceptTask(TaskInsertedID);

                        if (_result > 0)
                        {
                            if (chkReminder.Checked == true)
                            {
                                oTask.TaskID = _result;
                                //For New Reminder second arg --> false 
                                SaveReminder(oTask, false);
                            }
                            _taskID = _result; //Added by Rahul on 20101025 for Smart Order
                            //MessageBox.Show("Record Added Successfully.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //if (C1PatTask.Rows.Count <= 1)
                            //{
                            //    this.Close();
                            //}
                        }
                        else
                        {
                            MessageBox.Show("ERROR : Record not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }

                        #endregion " Addding the created Task "
                    }

                    return true;
                }//if(Validate())

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

           

            return false;
        }
        private void UpdateReplytask_assign(Int64 nAssignToID, Int64 nAssignFromID, Int64 nTaskID, Task oTask, Int64 StatusID, decimal ProgressComplete, string ProgressDescription, DateTime ProgresDatetime, Int64 TaskPrevGroupID)
        {


            ////oTask.Progress.StatusID =
            //oTask.Progress.Complete = numComplete.Value;
            //oTask.Progress.Description = rtxtDescription.Text.Trim();
            //oTask.Progress.DateTime = DateTime.Now;
            //oTask.Progress.ClinicID = ClinicID;
            
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nAssignToID", nAssignToID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nAssignFromID", nAssignFromID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTaskID", nTaskID, ParameterDirection.Input, SqlDbType.BigInt);


                //task mst
                oParameters.Add("@nProviderID", oTask.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", oTask.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sSubject", oTask.Subject, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nStartDate", oTask.StartDate, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nDueDate", oTask.DueDate, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPriorityID", oTask.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCategoryID", oTask.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nFollowUpID", oTask.FollowupID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bIsPrivate", oTask.IsPrivate, ParameterDirection.Input, SqlDbType.Bit);
              
                oParameters.Add("@nDateCreated", oTask.DateCreated, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sNoteExt", oTask.Notes, ParameterDirection.Input, SqlDbType.VarChar);
             
                oParameters.Add("@nTaskType", oTask.TaskType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@sFaxTiffFileName", oTask.FaxTiffFileName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sMachineName", oTask.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nReferenceID1", oTask.ReferenceID1, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nReferenceID2", oTask.ReferenceID2, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", oTask.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);






                //progress
                //oParameters.Add("@nStatusID", StatusID, ParameterDirection.Input, SqlDbType.BigInt);
                //oParameters.Add("@dComplete", ProgressComplete, ParameterDirection.Input, SqlDbType.Decimal);
                oParameters.Add("@nStatusID", StatusID , ParameterDirection.Input, SqlDbType.BigInt); // fro replying task status must be in progress 
                oParameters.Add("@dComplete", ProgressComplete , ParameterDirection.Input, SqlDbType.Decimal);
           
                oParameters.Add("@sDescription", ProgressDescription  , ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nDateTime", ProgresDatetime, ParameterDirection.Input, SqlDbType.DateTime);

                //added task group
                oParameters.Add("@nTaskGroupID", oTask.TaskGroupID , ParameterDirection.Input, SqlDbType.BigInt );

                oParameters.Add("@nPrevTaskGroupID", TaskPrevGroupID , ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@loginuserreplyedtaskUserID", _loginuserreplyedtaskUserID, ParameterDirection.Input, SqlDbType.BigInt);
                
                oDB.Execute("gsp_UpdateReplytask_assign", oParameters);
            }
            catch
            {
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Clear(); 
                oParameters.Dispose();
            }


        }

        private void UpdateTaskMST( Int64 nTaskID, Task oTask, Int64 StatusID, decimal ProgressComplete, string ProgressDescription, DateTime ProgresDatetime, Int64 TaskPrevGroupID)
        {


            ////oTask.Progress.StatusID =
            //oTask.Progress.Complete = numComplete.Value;
            //oTask.Progress.Description = rtxtDescription.Text.Trim();
            //oTask.Progress.DateTime = DateTime.Now;
            //oTask.Progress.ClinicID = ClinicID;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                  oParameters.Add("@nTaskID", nTaskID, ParameterDirection.Input, SqlDbType.BigInt);
                  oParameters.Add("@nProviderID", oTask.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", oTask.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sSubject", oTask.Subject, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nStartDate", oTask.StartDate, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nDueDate", oTask.DueDate, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPriorityID", oTask.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCategoryID", oTask.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nFollowUpID", oTask.FollowupID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bIsPrivate", oTask.IsPrivate, ParameterDirection.Input, SqlDbType.Bit);

                oParameters.Add("@nDateCreated", oTask.DateCreated, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sNoteExt", oTask.Notes, ParameterDirection.Input, SqlDbType.VarChar);

                oParameters.Add("@nTaskType", oTask.TaskType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@sFaxTiffFileName", oTask.FaxTiffFileName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sMachineName", oTask.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nReferenceID1", oTask.ReferenceID1, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nReferenceID2", oTask.ReferenceID2, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", oTask.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
              
                //progress
                //oParameters.Add("@nStatusID", StatusID, ParameterDirection.Input, SqlDbType.BigInt);
                //oParameters.Add("@dComplete", ProgressComplete, ParameterDirection.Input, SqlDbType.Decimal);
                oParameters.Add("@nStatusID", StatusID, ParameterDirection.Input, SqlDbType.BigInt); // fro replying task status must be in progress 
                oParameters.Add("@dComplete", ProgressComplete, ParameterDirection.Input, SqlDbType.Decimal);

                oParameters.Add("@sDescription", ProgressDescription, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nDateTime", ProgresDatetime, ParameterDirection.Input, SqlDbType.DateTime);

                //added task group
                oParameters.Add("@nTaskGroupID", oTask.TaskGroupID, ParameterDirection.Input, SqlDbType.BigInt);
                 oDB.Execute("gsp_UpdateTaskMst", oParameters);
            }
            catch
            {
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Clear();
                oParameters.Dispose();
            }


        }




        //private void CompletetaskifOwnerNotFoundwhileReplying(Int64 nPrevTaskGroupID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    DataTable dtUser = new DataTable();
        //    string strQuery = "";

        //    try
        //    {
        //        oDB.Connect(false);
        //        strQuery = "Update TM_Task_Progress set nStatusID=3,  dComplete=100 from TM_Task_Progress  inner join tm_taskmst on TM_Task_Progress.nTaskId = tm_taskmst.nTaskId where tm_taskmst.nTaskGroupID = "+ nPrevTaskGroupID +" ";
        //        oDB.Execute_Query (strQuery);
                
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
               
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //        dtUser.Dispose();
        //    }
        //    //return ID;
        //}


        private void CompletetaskifOwnerNotFoundwhileReplying(Int64 nPrevTaskGroupID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameter = new gloDatabaseLayer.DBParameters();


            try
            {
                oDB.Connect(false);
                oParameter.Add("@TaskGroupID", nPrevTaskGroupID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDB.Execute("gsp_UpCompleteTaskGroupWise", oParameter);
                oDB.Disconnect();





            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameter != null) { oParameter.Clear(); oParameter = null; }

            }

        }


        private Int64 GetUserID(string UserName)
        {
            Int64 ID = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtUser = new DataTable();
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                strQuery = "select ISNULL(nUserID,0) AS UserID  from User_MST where sLoginName =  '" + UserName + "' ";
                oDB.Retrive_Query(strQuery, out dtUser);
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    ID = Convert.ToInt64(dtUser.Rows[0][0]);
                    return ID;

                }
                return ID;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return ID;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return ID;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtUser.Dispose();
            }
            //return ID;

        }
        //private string  GetUserName(Int64 UserID)
        //{
        //    string strUserName  = "";

        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
           
        //    string strQuery = "";

        //    try
        //    {
        //        oDB.Connect(false);
        //        strQuery = "select ISNULL(sLoginName,'') AS LoginName  from User_MST where nUserID =  " + UserID + " ";
        //   strUserName=  Convert.ToString (   oDB.ExecuteScalar_Query (strQuery));
        //   return strUserName;
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        return "";
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
              
        //    }
        //    //return ID;

        //}
        //End : CreateTask

        private string GetUserName(Int64 UserID)
        {
            string strUserName = "";

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameter = new gloDatabaseLayer.DBParameters();


            try
            {
                oDB.Connect(false);
                oParameter.Add("@nUSERID", UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                strUserName = Convert.ToString(oDB.ExecuteScalar("gsp_getLoginNamebyUserID", oParameter));
                oDB.Disconnect();
              }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }

                if (oParameter != null) { oParameter.Clear(); oParameter = null; }
            }
            return strUserName;
        }

        #region " Design Events "

        private void btnTo_MouseHover(object sender, EventArgs e)
        {
            btnTo.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Yellow;
            btnTo.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnTo_MouseLeave(object sender, EventArgs e)
        {
            btnTo.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Button;
            btnTo.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_Providers_MouseHover(object sender, EventArgs e)
        {
            btn_Providers.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Yellow;
            btn_Providers.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_Providers_MouseLeave(object sender, EventArgs e)
        {
            btn_Providers.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Button;
            btn_Providers.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_Patient_MouseHover(object sender, EventArgs e)
        {
            btn_Patient.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Yellow;
            btn_Patient.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_Patient_MouseLeave(object sender, EventArgs e)
        {
            btn_Patient.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Button;
            btn_Patient.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_ToDel_MouseHover(object sender, EventArgs e)
        {
            btn_ToDel.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Yellow;
            btn_ToDel.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_ToDel_MouseLeave(object sender, EventArgs e)
        {
            btn_ToDel.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Button;
            btn_ToDel.BackgroundImageLayout = ImageLayout.Stretch;
        }

        #endregion " Design Events "


        //Create reminder for task
        private void SaveReminder(Task oTask, bool IsModified)
        {
            try
            {
                for (int i = 0; i < oTask.Assignment.Count; i++)
                {
                    Reminder oReminder = new Reminder();

                    oReminder.Description = oTask.Subject;
                    oReminder.IsDismissed = false;

                    //GLO2011-0012824 : Task sender receives reminder - Why?
                    // Commneted to for task to be pop up to actual user instead of popping up to invalid user.
                    // and Set the ownerID as assignedTo

                    oReminder.OwnerID = oTask.Assignment[i].AssignToID;
                    //if (oTask.Assignment[i].SelfAssigned == gloTasksMails.Common.SelfAssigned.Self)
                    //{
                    //    oReminder.OwnerID = UserID;
                    //oReminder.IsDismissed = false;
                    //}
                    //else
                    //{
                    //    oReminder.OwnerID = oTask.Assignment[i].AssignToID;
                    //    //Reminder is not active till User [Assigned To] accepts the Task
                    //    //oReminder.IsDismissed = true; // COMMENT BY SUDHIR 20091204 // EMR WANTS TO REMINDER POPUP, EVEN IF USER HAS NOT ACCEPTED THIS TASK //
                    //}


                    DataTable dtLocation = new DataTable();
                    gloAppointmentBook.Books.Location olocation = new gloAppointmentBook.Books.Location();
                    dtLocation = olocation.GetDefaultLocation();
                    if (dtLocation != null && dtLocation.Rows.Count > 0)
                    {
                        oReminder.Place = Convert.ToString(dtLocation.Rows[0]["sLocation"]);
                    }
                    else
                    {
                        oReminder.Place = "";
                    }
                    olocation.Dispose();

                    oReminder.ReferanceID = oTask.TaskID;
                    oReminder.ReferenceType = ReferenceType.Task;

                    oReminder.ReminderStartDate = gloDateMaster.gloDate.DateAsDate(oTask.StartDate);
                    //oReminder.ReminderStartTime = dtpStartTime.Value;
                    oReminder.ReminderEndDate = gloDateMaster.gloDate.DateAsDate(oTask.DueDate);
                    //oReminder.ReminderEndTime = dtpEndTime.Value;

                    oReminder.ReminderDate = dtpReminderDate.Value;
                    oReminder.ReminderTime = dtpReminderTime.Value;

                    //if task is completed then
                    if ((int)oTask.Progress.StatusID == 3)
                    {
                        oReminder.IsDismissed = true;
                    }//if


                    if (chkReminder.Checked == true)
                    {
                        if (IsModified == false)
                            oReminder.Add(oReminder);
                        else
                            oReminder.ModifyTaskReminder(oReminder);
                    }
                    else
                    {
                        oReminder.DeleteReminder(oReminder);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //By Sudhir 20090211 - Using for gloEMR
        private void AcceptTask(Int64 TaskID)
        {
            gloTask ogloTask = new gloTask(DataBaseConnectionString);
            Task oTask = new Task();
            TaskAssign oTaskAssign = new TaskAssign();

            try
            {
                oTask = ogloTask.GetTask(TaskID);

                //If the User Accepts the Task -New Entry for the Task is made 
                //against the user.
                //So we get the assigned Task first & then set its TaskID =0,
                //also change the OwnerID to current userID & some other fields
                //as done below
                //oTaskAssign = ogloTask.GetTaskAssign(TaskID);
                //Int64 _AssignFromID = 0;
                //if (oTaskAssign != null)
                //{
                //    _AssignFromID = oTaskAssign.AssignFromID;
                //}
                if (oTask != null)
                {
                    //Clear the previous assignments for this Task
                    oTask.Assignment.Clear();

                    oTask.TaskID = 0;
                    oTask.UserID = UserID;
                    //Also set the Current User who is accepting this task 
                    //as the owner of the task.
                    oTask.OwnerID = UserID;

                    oTaskAssign.TaskID = 0;
                    oTaskAssign.AssignToID = UserID;
                    //oTaskAssign.AssignFromID = 0;
                    oTaskAssign.AssignFromID = GetUserID(txtFrom.Text.Trim()); //Sandip 

                    oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                    oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                    oTask.Assignment.Add(oTaskAssign);
                }


                Int64 _result = ogloTask.Add(oTask);

                // Added by Abhijeet on 20100625
                // Assign new generated task id to Property which save new task id
                TaskInsertedID = _result;
                // End of changes by Abhijeet on 20100625

                if (_result > 0)
                {
                    if (ogloTask.AcceptTask(TaskID, UserID))
                    {
                        //-- Sudhir --//
                        //To Load Accepted Task Again
                        HideToolStripButtons();
                        // ShowTask(_result);
                        _taskID = _result;

                        tsb_AcceptTask.Visible = false;
                       
                        tsb_DeclineTask.Visible = false;
                        tsb_OK.Enabled = true;
                        tsbbtn_OnlySave.Enabled = true;
                        if (oTask.TaskType == TaskType.UnsolicitedTask)
                        {
                            tsb_MergeOrder.Visible = true;
                        }
                        // -- -- //

                        //--------Saket 
                        //Activate Reminder for this Task if reminder is present 
                        gloReminder.Reminder oReminder = new gloReminder.Reminder();
                        oReminder.ActivateTaskReminder(UserID, TaskID, _result);
                        //-------------
                        _IsAcceptClick = true;//Developer:Pradeep/Date:02/22/2011/Bug ID/PRD Name/Salesforce Case: 21248/Reason:creating multiple task 
                        RefreshTaskList(_taskID);
                    }
                    // MessageBox.Show("Task Accepted Successfully.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ERROR : Record not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            finally
            {
                ogloTask.Dispose();
                oTask.Dispose();
                oTaskAssign.Dispose();

            }

        }

        //By Sudhir 20090211 - Using for gloEMR
        private void DeclineTask(Int64 TaskID)
        {
            gloTask ogloTask = new gloTask(DataBaseConnectionString);
            Int64 tempTaskId = TaskID;

            try
            {
                //Decline Task Request if selected for decline
                if (!ogloTask.DeclineTask(TaskID, UserID))
                {
                    MessageBox.Show("Error : Decline unsuccessful.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }//if (! ogloTask.DeclineTask(tempTaskId, UserId))
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #endregion " Task Methods - New "

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            if (cmbStatus.SelectedValue.ToString() == "3")
            {
                numComplete.Value = 100;
                pnlReminder.Enabled = false;
                ChkCompleteTaskforallUsers.Enabled = true;
            }
            else if (cmbStatus.SelectedValue.ToString() == "1" && numComplete.Value != 0)
            {
                numComplete.Value = 0;
                ChkCompleteTaskforallUsers.Enabled = false;
            }
            else if (cmbStatus.SelectedValue.ToString() == "2") //&& numComplete.Value != 100)
            {
                numComplete.Value = 25;
                ChkCompleteTaskforallUsers.Enabled = false;
            }
            else if (cmbStatus.SelectedValue.ToString() == "5") //&& numComplete.Value != 100)
            {
                // numComplete.Value = 25;
                pnlReminder.Enabled = true;
                ChkCompleteTaskforallUsers.Enabled = false;
                //chkReminder.Checked = false;
            }
            else if (cmbStatus.SelectedValue.ToString() == "4")//&& numComplete.Value != 100)
            {
                // numComplete.Value = 25;
                pnlReminder.Enabled = true;
                ChkCompleteTaskforallUsers.Enabled = false;
            }
            blnChangeTask = true;
           
        }




        private void CompleteAllSelectedTask()
        {
            DataTable dttask = new DataTable();
            Hashtable htable = new Hashtable();
            try
            {

                DetachControlEvents();
                dttask.Columns.Add("nTaskID");

                DataTable dtSelected = (DataTable)C1PatTask.DataSource;
                for (int rowcnt = 0; rowcnt < dtSelected.Rows.Count; rowcnt++)
                {
                    if (Convert.ToBoolean(dtSelected.Rows[rowcnt]["Select"]) && (Convert.ToString(dtSelected.Rows[rowcnt]["Status"]).Trim() != "Not Yet Accepted"))
                    {
                        dttask.Rows.Add(Convert.ToInt64(dtSelected.Rows[rowcnt]["TaskId"]));
                        htable.Add(rowcnt.ToString(), dtSelected.Rows[rowcnt]["TaskId"]);
                    }
                }

                if (dttask.Rows.Count > 0)
                {
                    CompleteAllTask(dttask);
                }
                else //if no record selected or only unaccepted task selected
                {
                    return;
                }
                Int64 SeltaskId = 0;//added for bugid 76075
                int cnt = htable.Count;
                int i = 0;
                int indx = 1;
                foreach (DictionaryEntry di in htable)
                {

                    DataRow[] drr = dtSelected.Select("TaskId=" + di.Value.ToString() + "");
                    i = i + 1;
                    if (i == cnt)
                    {
                        if (drr.Length > 0)
                        {
                            indx = dtSelected.Rows.IndexOf(drr[0]);
                        }
                        //if (drr.Length > 0)
                        //{
                        //   int ind= dtSelected.Rows.IndexOf(drr[0]);
                        //   if ((ind + 1) < dtSelected.Rows.Count)
                        //   {
                        //       SeltaskId = Convert.ToInt64(dtSelected.Rows[ind + 1]["TaskId"]);
                        //   }
                        //   else
                        //   {
                        //       ind = ind - 1;
                        //       if (ind >= 0)
                        //       {
                        //          if((ind+1)<=dtSelected.Rows.Count)   
                        //           SeltaskId = Convert.ToInt64(dtSelected.Rows[ind ]["TaskId"]);
                        //       }
                        //   }
                        //}
                    }
                    if (drr.Length > 0)
                    {
                        C1PatTask.RowColChange -= C1PatTask_RowColChange;
                        dtSelected.Rows.Remove(drr[0]);
                    }
                    // i = i + 1;

                }
                if (dtSelected.Rows.Count > 0)
                {
                    if (indx < dtSelected.Rows.Count)
                    {
                        SeltaskId = Convert.ToInt64(dtSelected.Rows[indx]["TaskId"]);
                    }
                    else
                    {
                        // indx = indx - 1;
                        SeltaskId = Convert.ToInt64(dtSelected.Rows[0]["TaskId"]);
                    }
                }
                if (dtSelected.Rows.Count <= 1)
                {
                    tsb_OK.Visible = true;
                    tsbbtn_OnlySave.Visible = false;
                }
                else
                {
                    tsb_OK.Visible = true;
                    tsbbtn_OnlySave.Visible = true;
                }
                C1PatTask.DataSource = dtSelected;
                SetColumnVisibility();
                if (dtSelected.Rows.Count == 0)
                    pnlselectall.Visible = false;
                else
                {
                    pnlselectall.Visible = true;
                    if (SeltaskId != 0)
                    {
                        DetachControlEvents();
                        HideToolStripButtons();
                        if (dtSelected.Rows.Count > 0)
                        {
                            TaskID = SeltaskId;

                            ShowTask(SeltaskId);
                        }
                        if (indx < (C1PatTask.Rows.Count - 1))
                        {
                            C1PatTask.Select(indx + 1, 0);
                        }
                        else
                        {
                            if (C1PatTask.Rows.Count > 1)
                            {
                                C1PatTask.Select(1, 0);
                            }
                        }

                    }
                }
            }
            finally
            {
                if (dttask != null)
                {
                    dttask.Dispose();
                    dttask = null;
                }
                //   RefreshTaskList();
                AttachControlEvents();
            }
            Selrowno = C1PatTask.RowSel;  
        }
        private void lbl_From_Click(object sender, EventArgs e)
        {

        }

        private void chkCompleteAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCompleteTaskforallUsers.Checked == true)
            {
                _IsCompleteAllTask = true;
            }
            else
            {
                _IsCompleteAllTask = false;
            }
        }

        private void frmTask_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (uiPnlPatientTask.Enabled == true)
            {

                MemoryStream ms = null;
                try
                {
                    C1PatTask.RowColChange -= C1PatTask_RowColChange;
                    gloTask objtask = new gloTask(DataBaseConnectionString);
                    ms = new MemoryStream();
                    uiPanelManager1.SaveLayoutFile(ms);
                    objtask.SaveDisplaySettings(ms, UserID, System.Environment.MachineName.ToString());
                    objtask.Dispose();
                    objtask = null;
                }
                finally
                {
                    if (ms != null)
                    {
                        ms.Dispose();
                        ms = null;
                    }
                }
            }

            if (dtUsers != null)
            {
                dtUsers.Dispose();
                dtUsers = null;
            }
            if (ToList != null)
            {
                ToList.Dispose();
                ToList = null;
            }

            if (imgList_Common != null)
            {
                if (imgList_Common.Images != null)
                {
                    imgList_Common.Images.Clear();
                }
                 imgList_Common.Dispose();
                imgList_Common = null; 
            }
        }

        private void tsb_PatientPayment_Click(object sender, EventArgs e)
        {
            gloTask ogloTask = new gloTask(DataBaseConnectionString);
            TaskChangeEventArg e2;
            DataTable dtBillDetails = null;
            try
            {
                //   Int64 IBPAckToken = 0;
                dtBillDetails = ogloTask.GetIntuitBillPayMessageDetails(ReferenceID);
                e2 = new TaskChangeEventArg();
                if (dtBillDetails.Rows.Count > 0)
                {
                    e2.IsIntuitBillPay = true;

                    e2.IBPAuthNumber = dtBillDetails.Rows[0]["IBPAuthNumber"].ToString();
                    e2.IBPCardType = dtBillDetails.Rows[0]["IBPCardType"].ToString();
                    e2.IBPCheckamount = Convert.ToDecimal(dtBillDetails.Rows[0]["IBPCheckamount"]);
                    //e2.IBPReferenceNumber = dtBillDetails.Rows[0]["IBPReferenceNumber"].ToString();
                    e2.TaskID = TaskID;
                }
                if (OnPatientPaymentClicked != null)
                    OnPatientPaymentClicked(sender, e, e2);
                if (e2.IBPToken == true)
                {
                    numComplete.Value = 100;
                    tsb_PatientPayment.Visible = false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                e2 = null;
                if (ogloTask != null)
                {
                    ogloTask.Dispose();
                    ogloTask = null;
                }

            }
        }

        // added funcation by manoj jadhav for checking is lab result value is hyperlink value or not on 20121205
        private void rtxtDescription_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.LinkText.Trim()) && _TaskType == TaskType.LabOrder)
                {
                    gloGlobal.gloLabGenral.OpenLinkInBrowser(e.LinkText.Trim());
                }
            }
            catch (Exception)
            { }


        }

        private void dtp_StartDate_ValueChanged(object sender, EventArgs e)
        {


            blnChangeTask = true;
        }

        private void cmbPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            blnChangeTask = true;
        }

        private void dtp_EndDate_ValueChanged(object sender, EventArgs e)
        {
            blnChangeTask = true;
        }

        private void cmb_FollowUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            blnChangeTask = true;
        }

        private void dtpReminderDate_ValueChanged(object sender, EventArgs e)
        {
            blnChangeTask = true;
        }

        private void txtPatient_TextChanged(object sender, EventArgs e)
        {
            blnChangeTask = true;
        }

        private void txtProvider_TextChanged(object sender, EventArgs e)
        {
            blnChangeTask = true;
        }

        private void txtSubject_TextChanged(object sender, EventArgs e)
        {
            blnChangeTask = true;
        }

        private void txtFrom_TextChanged(object sender, EventArgs e)
        {
            blnChangeTask = true;
            
        }

        private void cmb_To_SelectedIndexChanged(object sender, EventArgs e)
        {

            blnChangeTask = true;


        }

        private void dtpReminderTime_ValueChanged(object sender, EventArgs e)
        {
            blnChangeTask = true;
        }



        private void rtxtDescription_TextChanged(object sender, EventArgs e)
        {
            blnChangeTask = true;
        }

        private void chkselectall_CheckedChanged(object sender, EventArgs e)
        {
            for (int len = 1; len < C1PatTask.Rows.Count; len++)
            {
                C1PatTask.Rows[len]["Select"] = chkselectall.Checked;
            }
        }

        private void btncomp_Click(object sender, EventArgs e)
        {
            CompleteAllSelectedTask();
            if ((chkselectall.Checked == true) && (C1PatTask.Rows.Count <= 1))
            {
                this.Close();
            }
           //added for bugid 77957
            if (C1PatTask.Rows.Count <= 1) 
            {
                this.Close(); 
            }
        }


        DialogResult dg = DialogResult.Ignore;
        int Selrowno = 0;
        private void C1PatTask_RowColChange(object sender, EventArgs e)
        {
            if (dg == DialogResult.Cancel)
            {
                C1PatTask.RowColChange -= C1PatTask_RowColChange;
                C1PatTask.BeforeSelChange -= C1PatTask_BeforeSelChange;
                C1PatTask.Select(Selrowno, 0);
                C1PatTask.BeforeSelChange += C1PatTask_BeforeSelChange;
                C1PatTask.RowColChange += C1PatTask_RowColChange;
                dg = DialogResult.Ignore;
                return;
            }
            //if (blnChangeTask == false)
            //{
            //    C1PatTask.Select(Selrowno, 0);
            //    return;
            //}

                if ((C1PatTask.RowSel > -1) && (C1PatTask.ColSel > -1))//added condition for bugid 76004
            {

                bool iscancel = false;
                try
                {
                    if (TaskID == 0)  //for new task
                    {
                         dg = MessageBox.Show("Do you want to save the new Task?", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                        if (dg == DialogResult.Yes)
                        {
                            //if (!blnCreateTask())
                            //{



                            if (blnCreateTask())
                            {
                                _IsUpdated = true;
                                blnChangeTask = false;
                                RefreshTaskList();
                                Selrowno = C1PatTask.RowSel;
                                return;
                            }



                            else
                            {
                                txtSubject.Focus();
                                Selrowno = C1PatTask.RowSel;
                                return;
                            }


                        }

                        if (dg == DialogResult.No)
                        {
                            blnChangeTask = false;
                            Selrowno = C1PatTask.RowSel;
                        }
                        if (dg == DialogResult.Cancel)
                        {
                            txtSubject.Focus();
                          
                            return;
                        }
                    }

                    DetachControlEvents();

                    if ((blnChangeTask == true) && (tsb_OK.Enabled == true))
                    {
                         dg = MessageBox.Show("Do you want to save the changes?", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                        if (dg == DialogResult.Yes)
                        {

                            //CreateTask();

                            //_IsUpdated = true;

                            if (blnCreateTask())
                            {
                                _IsUpdated = true;
                                blnChangeTask = false;
                                RefreshTaskList();
                                Selrowno = C1PatTask.RowSel;
                                return;
                            }



                            else
                            {
                                txtSubject.Focus();
                                Selrowno = C1PatTask.RowSel;
                                return;
                            }
                        }
                        if (dg == DialogResult.Cancel)
                        {
                            txtSubject.Focus();
                            iscancel = true;
                           
                            return;
                        }
                        if (dg == DialogResult.No)
                        {
                            Selrowno = C1PatTask.RowSel;
                        }
                    }

                    if (C1PatTask.RowSel > 0)
                    {
                        DataTable dtTask = (DataTable)C1PatTask.DataSource;
                        if (dtTask != null)
                        {
                            if (dtTask.Rows.Count > 0)
                            {
                                TaskID = Convert.ToInt64(dtTask.Rows[C1PatTask.RowSel - 1]["TaskId"]);
                                HideToolStripButtons();
                                ShowTask(TaskID);
                            }
                        }

                    }
                }
                finally
                {
                   
                    AttachControlEvents();
                    if (iscancel == false)
                        blnChangeTask = false;
                    if (dg == DialogResult.Cancel)
                    {
                        C1PatTask.RowColChange -= C1PatTask_RowColChange;
                        C1PatTask.BeforeSelChange -= C1PatTask_BeforeSelChange;
                        C1PatTask.Select(Selrowno, 0);
                        C1PatTask.BeforeSelChange += C1PatTask_BeforeSelChange;
                        C1PatTask.RowColChange += C1PatTask_RowColChange;
                       // dg = DialogResult.Ignore; 
                        
                    }

                    if ((txtFrom.Text.Trim() != cmb_To.Text.Trim()) && (txtFrom.Text.Trim() != _UserName))
                    {
                        btnreply.Enabled = true;
                        //Enabled = false change made against incident CAS-02366-J7M7G9
                        btnTo.Enabled = false;
                        btn_ToDel.Enabled = false;   
                    }
                    else
                    {
                        btnreply.Enabled = false;
                        btnTo.Enabled = true;
                        btn_ToDel.Enabled = true;  
                    }
                    _istaskreplyed = false;
                }
            }
        }

        private void C1PatTask_BeforeSelChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
           if (blnChangeTask==false)  
            Selrowno = C1PatTask.RowSel;
        }
        Int64 _replyedtaskSelectedUSerID = 0;
        Int64 _loginuserreplyedtaskUserID = 0;
        string tempusername = "";
        private void btnreply_Click(object sender, EventArgs e)
        {
            try
            {
                _istaskreplyed = true;
                _replyedtaskSelectedUSerID = 0;
                _loginuserreplyedtaskUserID = 0;
                tempusername = "";
                if (Convert.ToString(cmb_To.Text).Trim() != "")
                {
                    if (Convert.ToString(cmb_To.Text) != txtFrom.Text.Trim())
                    {
                        string tempselectedusername = txtFrom.Text;
                        Int64 tempuserid = GetUserID(txtFrom.Text);
                        txtFrom.Text = cmb_To.Text.ToString();
                       
                        DataTable dt = (DataTable)cmb_To.DataSource;
                        //DataRow drTemp = dt.NewRow();

                        if (dt != null)
                        {


                            
                            if (TaskGroupID != 0)
                            {
                                GetGroupusers(ref dt, TaskGroupID);
                               



                                //DataRow[] dr = dt.Select("Description='" + cmb_To.Text + "'");
                              DataRow[] dr  = dt.Select("Description='" + txtFrom.Text  + "'");
                                if (dr.Length > 0)
                                {
                                    dt.Rows.Remove(dr[0]);
                                }
                            }
                            else
                            {
                                dt.Rows.Clear();  
                                DataRow drr = dt.NewRow();
                                drr["ID"] = tempuserid;
                                drr["Description"] = tempselectedusername;
                                dt.Rows.Add(drr);
                            }

                           
                                cmb_To.DataSource = dt;
                            cmb_To.SelectedValue = tempuserid;
                            _replyedtaskSelectedUSerID = tempuserid;
                            tempusername = txtFrom.Text;  
                            if (txtFrom.Text.Trim() != _UserName.Trim())
                            {
                               
                                txtFrom.Text = _UserName;
                                _loginuserreplyedtaskUserID = GetUserID(_UserName);
                            }
                            addUsertoListControl(dt);
                        }

                    }
                }
            }
            finally
            {
                btnreply.Enabled = false;
                //Enabled = true change made against incident CAS-02366-J7M7G9
                btnTo.Enabled = true;
                btn_ToDel.Enabled = true;   
            }
        }

        private void addUsertoListControl(DataTable dtUsers)
        {
            if (ToList == null)
                ToList = new gloGeneralItem.gloItems();
                ToList.Clear();
            foreach (DataRow dr in dtUsers.Rows)
            {

                gloGeneralItem.gloItem ToItem = new gloGeneralItem.gloItem();

                ToItem.ID = Convert.ToInt64(dr["ID"]);
                ToItem.Description = Convert.ToString(dr["Description"]);

                ToList.Add(ToItem);

                ToItem.Dispose();
                ToItem = null;
            }

        }
        private int CheckTaskUsers() //added to check users count except Fromtask user  if count>1 it means if is group task
        {
        int count =0;
        Int64 _seluserid = GetUserID(txtFrom.Text);   
            if (cmb_To.Items.Count > 1)
        {
            for (int i = 0; i < cmb_To.Items.Count; i++)
            {
                cmb_To.SelectedIndex = i;
                if (_seluserid != Convert.ToInt64(cmb_To.SelectedValue))
                {
                    count++;
                }
            }
            cmb_To.SelectedIndex = 0;
        }
               return count;  
        }
           

        private void GetGroupusers(ref DataTable DtUsers, Int64 TaskGroupID)
        {
        


            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParamater = new gloDatabaseLayer.DBParameters();

        
            try
            {
                oDB.Connect(false);
                oParamater.Add("@nTaskGroupID", TaskGroupID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDB.Retrive("gsp_GetTaskUsersByGroupId", oParamater, out DtUsers);
                oDB.Disconnect();
            }
            catch
            {
               
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
           
        }

        private void C1PatTask_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
           //added for bugid 98982 
            try
            {
                bool resetflag = false;
                chkselectall.CheckedChanged -= chkselectall_CheckedChanged;
                for (int i = 0; i < C1PatTask.Rows.Count; i++)
                {
                    if (Convert.ToString(C1PatTask.Rows[i][0]) == "False")
                    {
                        chkselectall.Checked = false;
                        resetflag = true;
                        break;
                    }
                }
                if ((resetflag == false) && (C1PatTask.Rows.Count > 1))
                {
                    chkselectall.Checked = true;
                }

            }
            finally
            {
                chkselectall.CheckedChanged -= chkselectall_CheckedChanged;
                chkselectall.CheckedChanged += chkselectall_CheckedChanged;
            }
        }

        private void C1PatTask_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, C1PatTask , e.Location);
        }

       
        private void EnableDisablePatientDetails() //if To user is not equal to login user then disable patient detail panel
         {
             if (cmb_To.Text.Trim() != _UserName.Trim())
            {
                uiPnlPatientTask.Enabled = false;
             }
       
       

         }

    }
}
//Completed = 3,
//            Deferred = 4,
//            InProgress = 2,
//            OnHold = 5,
//            NotStarted = 1,


