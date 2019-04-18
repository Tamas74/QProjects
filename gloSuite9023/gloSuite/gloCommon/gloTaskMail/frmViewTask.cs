using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using System.Data.SqlClient;

namespace gloTaskMail
{

    public partial class frmViewTask : gloGlobal.Common.TriarqFormWithFocusListner
    {
        string struserids = "";

        #region " Declarations "

        private string _databaseconnnectionstring = "";
        private Int64 _taskID = 0;
        //private string _messageBoxCaption = "gloPM";
        DateTime _CurrentTime;
        //Added By Pramod For Message Box
        private string _messageBoxCaption = String.Empty;
        private Int64 _SelPatientID = 0;
        private Int64 _PatientID = 0;
        //private Int64 _providerID = 0;
        private Int64 _userId = 0;
        private string _userName = string.Empty;
        public bool _IsEMREnable = false;
        //Developer: Sanjog Dhamke
        //Date:10 Dec 2011
        //Bug ID/PRD Name/Sales force Case: Issue is - Handler is getting added on every button click on same form n Task Button event is fire multiple time 
        //Reason: So now we check that if instance is already created means handler is also added in it so don't add another extra handler for this form
        public bool _HandlerPresent = false;
        //private Int32 _AutoAcceptTask = 0;
        public delegate void OnViewTaskChange(object sender, EventArgs e, TaskChangeEventArg e2, object objfrmtask = null);
        public event OnViewTaskChange OnViewTask_Change;

        public delegate void ViewTaskModifiedClicked(object sender, EventArgs e, TaskChangeEventArg e2);
        public event ViewTaskModifiedClicked OnViewTaskModifiedClicked;
        public String SearchString = "";
        public bool _ReadviewCompletetask = false;
        #region "Flex Columns"

        const int COL_TASKICON = 0;
        const int COL_PRIORITYICON = 1;
        const int COL_TASKID = 2;
        const int COL_SELECT = 3;
        const int COL_PROVIDERID = 4;
        const int COL_PROVIDERNAME = 5;
        const int COL_PATIENTID = 6;
        const int COL_PATIENTNAME = 8;
        const int COL_SUBJECT = 7;
        const int COL_STARTDATE = 9;
        const int COL_DUEDATE = 10;
        const int COL_STATUSID = 11;
        const int COL_STATUSNAME = 12;
        const int COL_PRIORITYID = 13;
        const int COL_PRIORITYNAME = 14;
        const int COL_COMPLETE = 15;
        const int COL_CATEGORYID = 16;
        const int COL_CATEGORYNAME = 17;
        const int COL_FOLLOWUPID = 18;
        const int COL_FOLLOWUPNAME = 19;
        const int COL_ISPRIVATE = 20;
        const int COL_OWNERID = 21;
        const int COL_OWNERNAME = 22;
        const int COL_ASSIGNEDTOID = 23;
        const int COL_ASSIGNEDTONAME = 24;
        const int COL_DESCRIPTION = 25;
        const int COL_DATECOMPLETED = 26;
        const int COL_NOTES = 27;
        const int COL_USERID = 28;
        const int COL_CLINICID = 29;
        const int COL_FOLLOWUPICON = 30;
        const int COL_PRIORITYLEVEL = 31;
        const int COL_FROMID = 32;
        const int COL_Resp = 33;
        const int COL_ASSIGNEDTO = 34;
        const int COL_COUNT = 35;



        #endregion "Flex Columns"


        #endregion " Declarations "

        #region " Properties "

        public bool ShowOtherUsersDropdown
        {
            get { return _ReadviewCompletetask; }
            set { _ReadviewCompletetask = value; }
        }
        public Int64 UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public string userName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public string DatabaseConnnectionString
        {
            get { return _databaseconnnectionstring; }
            set { _databaseconnnectionstring = value; }

        }

        public Int64 TaskID
        {
            get { return _taskID; }
            set { _taskID = value; }

        }

        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        public bool IsEMREnable
        {
            get { return _IsEMREnable; }
            set { _IsEMREnable = value; }
        }

        #endregion " Properties "

        #region " Constructor "

        //int GridSelect_RowCount=0;
        private frmViewTask()
        {

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

            _databaseconnnectionstring = appSettings["DataBaseConnectionString"].ToString();

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _userId = Convert.ToInt64(appSettings["UserID"]); }
                else { _userId = 0; }
            }
            else
            { _userId = 0; }

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _userName = Convert.ToString(appSettings["UserName"]); }
                else { _userName = ""; }
            }
            else
            { _userName = ""; }
            //Added By Pramod Nair For Messagebox Caption 
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

            InitializeComponent();


            DataTable dtUsers = (DataTable)cmbToUsers.DataSource;
            if (dtUsers == null)
            {
                dtUsers = new DataTable();
                DataColumn dcId = new DataColumn("ID");
                DataColumn dcDescription = new DataColumn("Description");

                dtUsers.Columns.Add(dcId);
                dtUsers.Columns.Add(dcDescription);
            }

            //if (ToList == null)
            //    ToList = new gloGeneralItem.gloItems();
            //ToList.Clear();

            //gloGeneralItem.gloItem ToItem = new gloGeneralItem.gloItem();
            DataRow dr = dtUsers.NewRow();
            dr["ID"] = UserId;
            dr["Description"] = userName;
            dtUsers.Rows.Add(dr);
            dtUsers.AcceptChanges();
            cmbToUsers.DataSource = dtUsers;
            cmbToUsers.DisplayMember = "Description";
            cmbToUsers.ValueMember = "ID";
            cmbToUsers.SelectedValue = 0;
            cmbToUsers.SelectedIndex = 0;

            //ToList.Add(ToItem);
            //dtUsers.Rows.Add(dr);


        }

        private static frmViewTask _frm = null;
        public static frmViewTask GetInstance()
        {

            if (_frm != null)
            {
                return _frm;
            }
            else
            {
                _frm = new frmViewTask();
                return _frm;
            }

        }

        bool blnDisposed;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (blnDisposed == false)
            {
                if (disposing && (components != null))
                {
                    try
                    {
                        components.Dispose();
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }

                    try
                    {
                        if (cm_Task != null)
                        {
                            System.Windows.Forms.ContextMenuStrip[] cntmnuControls = { cm_Task };
                            System.Windows.Forms.Control[] cntControls = { cm_Task };
                            if (cntmnuControls != null)
                            {
                                if (cntmnuControls.Length > 0)
                                {
                                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntmnuControls);

                                }
                            }
                            if (cntControls != null)
                            {
                                if (cntControls.Length > 0)
                                {
                                    gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                                }
                            }

                        }
                    }
                    catch
                    {
                    }


                }
                base.Dispose(disposing);
            }
            _frm = null;
            blnDisposed = true;
        }

        private void Disposer()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        #endregion " Constructor "

        #region " Form Load Event "

        private void frmViewTask_Load(object sender, EventArgs e)
        {
            //Bug #48803: gloEMR - Tasks - Application is selecting only cell instead of row.
            gloC1FlexStyle.Style(c1ViewTask, false);



            try
            {

                DataTable dtUsers = new DataTable();
                DataRow drTemp = dtUsers.NewRow();
                if (ToList == null)
                    ToList = new gloGeneralItem.gloItems();
                ToList.Clear();


                gloGeneralItem.gloItem ToItem = new gloGeneralItem.gloItem();
                DataColumn dcId = new DataColumn("ID");
                DataColumn dcDescription = new DataColumn("Description");

                dtUsers.Columns.Add(dcId);
                dtUsers.Columns.Add(dcDescription);
                drTemp["ID"] = UserId;
                drTemp["Description"] = userName;
                ToItem.ID = Convert.ToInt64(drTemp["ID"]);
                ToItem.Description = Convert.ToString(drTemp["Description"]);


                ToList.Add(ToItem);
                dtUsers.Rows.Add(drTemp);


                ToItem.Dispose();
                ToItem = null;




                AddGotFocusListener(this);
                tsb_AcceptTask.Visible = false;
                tsb_DeclineTask.Visible = false;
                tsb_Delete.Visible = true;

                struserids = Convert.ToString(_userId);
                Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);
                //dtUsers.Dispose();
                btnDown.Visible = true;
                btnDown.BackgroundImage = global::gloTaskMail.Properties.Resources.Down;
                btnDown.BackgroundImageLayout = ImageLayout.Center;
                oTimer.Tick += new EventHandler(oTimer_Tick);

                if (c1ViewTask.Rows.Count > 1)
                {
                    c1ViewTask.Focus();
                    c1ViewTask.Select(1, 0);
                }

                if (ShowOtherUsersDropdown == true)
                {
                    cmbToUsers.Visible = true;
                    btnToBrowse.Visible = true;
                    btnToDelete.Visible = true;
                    lbluser.Visible = true;
                }
                else
                {
                    cmbToUsers.Visible = false;
                    btnToBrowse.Visible = false;
                    btnToDelete.Visible = false;
                    lbluser.Visible = false;
                }
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void frmViewTask_SizeChanged(object sender, EventArgs e)
        {
        }

        #endregion " Form Load Event "

        #region " ToolStrip Button Click Strip Event "

        private void tsb_ADD_Click(object sender, EventArgs e)
        {
            try
            {
                frmTask ofrmTask = new frmTask(_databaseconnnectionstring, 0);
                ofrmTask.OnTask_Change += new frmTask.OnTaskChange(ofrmTask_OnTask_Change);
                ofrmTask.IsEMREnable = _IsEMREnable;
                ofrmTask.PatientID = _PatientID;
                ofrmTask.ShowDialog(this);
                ofrmTask.Dispose();
                Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void tsb_Modify_Click(object sender, EventArgs e)
        {
            try
            {
                GetControlSelection();
                if (c1ViewTask != null && c1ViewTask.Rows.Count > 0)
                {
                    ModifyTask();
                    Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                SetControlSelection();
            }
        }

        private void tsb_AcceptTask_Click(object sender, EventArgs e)
        {
            AcceptTask();
        }

        private void tsb_DeclineTask_Click(object sender, EventArgs e)
        {
            DeclineTask();
        }

        private void tsb_Delete_Click(object sender, EventArgs e)
        {
            //Int64 id = 0;
            gloTask ogloTask = new gloTask(_databaseconnnectionstring);
            DataTable dt = new DataTable();
            try
            {
                //Added By MaheshB 
                if (c1ViewTask.Rows.Count <= 1)
                {
                    return;
                }
                DialogResult dr = MessageBox.Show("Are you sure you want to delete task ?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    Int64 tempTaskId = 0;
                    // Int64 assignToId = 0;

                    if (c1ViewTask != null && c1ViewTask.Rows.Count > 0)
                    {
                        if (c1ViewTask.RowSel > 0)
                        {
                            tempTaskId = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_TASKID));
                            Int64 _taskuserID = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_ASSIGNEDTOID));
                            if (ogloTask.CanDeleteTask(tempTaskId) == true)
                            {
                                if (ogloTask.DeleteRequestedTask(tempTaskId, _taskuserID) == true)
                                {

                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.Task, gloAuditTrail.ActivityType.Delete,"Patient Task Deleted", Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_PATIENTID)), tempTaskId, Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_PROVIDERID)), gloAuditTrail.ActivityOutCome.Success);
                                    
                                }
                                
                                dt = ogloTask.get_multiTask(tempTaskId);
                                if (dt.Rows.Count == 0)
                                {
                                    ogloTask.DeleteTask(tempTaskId);
                                    // designC1TaskRequest();

                                }


                                //gloGeneralItem.gloItem ToItem = new gloGeneralItem.gloItem();
                                //gloGeneralItem.gloItems ToList;
                                //DataTable dtUsers = new DataTable();
                                //DataRow drTemp = dtUsers.NewRow();
                                //DataColumn dcId = new DataColumn("ID");
                                //DataColumn dcDescription = new DataColumn("Description");
                                //ToList = new gloGeneralItem.gloItems();
                                //dtUsers.Columns.Add(dcId);
                                //dtUsers.Columns.Add(dcDescription);



                                Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);

                                //drTemp["ID"] = struserids;
                                //drTemp["Description"] = struserids;
                                //ToItem.ID = Convert.ToInt64(drTemp["ID"]);
                                //ToItem.Description = Convert.ToString(drTemp["Description"]);


                                //ToList.Add(ToItem);
                                //dtUsers.Rows.Add(drTemp);


                                //ToItem.Dispose();
                                //ToItem = null;
                            }
                            else
                            {
                                MessageBox.Show("Cannot delete message . The message is assigned and is on hold", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {

            Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);

            txtSearch.ResetText();
            txtSearch.Focus();

            c1ViewTask_SelChange(null, null);
            if (c1ViewTask.Rows.Count > 1)
            {
                c1ViewTask.Focus();
                c1ViewTask.Select(1, 0);
            }
            else
            {
                c1ViewTask.Focus();
            }
        }

        #region "gloSuite - while integrating this function is comment, if required then we have to add gloExchange project, other wise try to maintain it in gloExchange service"

        private void tsb_ReceiveFromExchange_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    gloExchange.frmDateRange ofrm = new gloExchange.frmDateRange();
            //    ofrm.ShowDialog(this);
            //    gloExchange.gloExchange oReceiveExchange = new gloExchange.gloExchange(_databaseconnnectionstring);
            //    oReceiveExchange.GetExchangeSettings();
            //    gloExchange.Common.Task.PmsExchangeTasks oExchangeTasks = oReceiveExchange.GetTasks(UserId, ofrm.FromDate, ofrm.ToDate);
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            //}
        }

        private void tsb_SendToExchange_Click(object sender, EventArgs e)
        {
            //gloTask ogloTask = new gloTask(_databaseconnnectionstring);
            //try
            //{
            //    gloExchange.gloExchange oSendExchange = new gloExchange.gloExchange(_databaseconnnectionstring);
            //    oSendExchange.GetExchangeSettings();
            //    gloExchange.Common.Task.PmsExchangeTasks oTasksToSend = new gloExchange.Common.Task.PmsExchangeTasks();
            //    Tasks oTasks = new Tasks();
            //    oTasks = ogloTask.GetUserTasks(UserId);
            //    oTasksToSend = ogloTask.ConvertToExchangeTasks(oTasks);
            //    if (oTasksToSend != null)
            //    {
            //        for (int i = 0; i < oTasksToSend.Count; i++)
            //        {
            //            oSendExchange.CreateTask(oTasksToSend[i]);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            //}
        }

        #endregion
        private void tsb_Close_Click(object sender, EventArgs e)
        {
            OnTaskChangeEvent(sender, e);
            this.Close();
        }

        private void tsb_Help_Click(object sender, EventArgs e)
        {

        }

        private void tls_btnTaskAssigned_Click(object sender, EventArgs e)
        {
            //code commented for optimization
            //pnlGridAssign.Visible = true;
            //pnlSmallToolStrip.Visible = false;
            //pnlRequestTask.Visible = false;
            //pnlTaskRequestHeader.Visible = false;
            //pnlc1AssignedTask.Visible = true;
            //pnlAssigmedTaskHeader.Visible = true;
            //pnlc1AssignedTask.Dock = DockStyle.Fill;


        }

        private void tls_btnTaskRequest_Click(object sender, EventArgs e)
        {
            //code commented for optimization
            //pnlGridAssign.Visible = true;
            //pnlSmallToolStrip.Visible = false;
            //pnlc1AssignedTask.Visible = false;
            //pnlAssigmedTaskHeader.Visible = false;
            //pnlRequestTask.Visible = true;
            //pnlTaskRequestHeader.Visible = true;
            //pnlTaskRequest.Dock = DockStyle.Fill;
        }

        #endregion " ToolStrip Button Click Strip Event "

        #region " Flex Grid Design & Fill methods "
        //DataTable dtTaskRequest = null; //SLR: 'New is not needed
        private void design_c1Task()
        {

            try
            {

                c1ViewTask.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                //Set the column Names.                              
                c1ViewTask.Cols.Count = COL_COUNT;
                c1ViewTask.Rows.Count = 1;
                c1ViewTask.Cols.Fixed = 0;

                c1ViewTask.DrawMode = DrawModeEnum.OwnerDraw;
                c1ViewTask.FocusRect = FocusRectEnum.Light;
                c1ViewTask.SelectionMode = SelectionModeEnum.Row;
                c1ViewTask.AllowEditing = false;
                c1ViewTask.BringToFront();

                c1ViewTask.ShowCellLabels = false;

                c1ViewTask.SetData(0, COL_TASKICON, "");
                c1ViewTask.SetData(0, COL_PRIORITYICON, "");
                c1ViewTask.SetData(0, COL_FOLLOWUPICON, "");
                c1ViewTask.SetData(0, COL_TASKID, "TASKID");
                c1ViewTask.SetData(0, COL_SELECT, "");
                c1ViewTask.SetData(0, COL_PROVIDERID, "PROVIDERID");
                c1ViewTask.SetData(0, COL_PROVIDERNAME, "PROVIDERNAME");
                c1ViewTask.SetData(0, COL_PATIENTID, "PATIENTID");
                c1ViewTask.SetData(0, COL_PATIENTNAME, "Patient Name");
                //c1ViewTask.SetData(0, COL_SUBJECT, "SUBJECT");
                c1ViewTask.SetData(0, COL_SUBJECT, "Subject");
                c1ViewTask.SetData(0, COL_STARTDATE, "STARTDATE");
                //c1ViewTask.SetData(0, COL_DUEDATE, "DUEDATE");
                c1ViewTask.SetData(0, COL_DUEDATE, "Due Date");
                c1ViewTask.SetData(0, COL_STATUSID, "STATUSID");
                //c1ViewTask.SetData(0, COL_STATUSNAME, "STATUS");
                c1ViewTask.SetData(0, COL_STATUSNAME, "Status");
                c1ViewTask.SetData(0, COL_PRIORITYID, "PRIORITYID");
                c1ViewTask.SetData(0, COL_PRIORITYNAME, "Priority");
                c1ViewTask.SetData(0, COL_COMPLETE, "% Complete");
                c1ViewTask.SetData(0, COL_CATEGORYID, "CATEGORYID");
                c1ViewTask.SetData(0, COL_CATEGORYNAME, "Category Name");
                c1ViewTask.SetData(0, COL_FOLLOWUPID, "FOLLOWUPID");
                c1ViewTask.SetData(0, COL_FOLLOWUPNAME, "Followup Name");
                c1ViewTask.SetData(0, COL_ISPRIVATE, "ISPRIVATE");
                c1ViewTask.SetData(0, COL_OWNERID, "OWNERID");
                c1ViewTask.SetData(0, COL_OWNERNAME, "Owner Name");
                c1ViewTask.SetData(0, COL_ASSIGNEDTOID, "ASSIGNEDTOID");
                c1ViewTask.SetData(0, COL_ASSIGNEDTONAME, "Assigned To Name");
                c1ViewTask.SetData(0, COL_DESCRIPTION, "Description");
                c1ViewTask.SetData(0, COL_DATECOMPLETED, "Date Completed");
                c1ViewTask.SetData(0, COL_NOTES, "Notes");
                c1ViewTask.SetData(0, COL_USERID, "USERID");
                c1ViewTask.SetData(0, COL_CLINICID, "CLINICID");
                c1ViewTask.SetData(0, COL_FROMID, "FROMID");
                c1ViewTask.SetData(0, COL_Resp, "Resp");
                c1ViewTask.SetData(0, COL_ASSIGNEDTO, "Assigned To");
                int nWidth;
                nWidth = pnl_Grid.Width - 2;
                //nWidth = panel4.Width;

                c1ViewTask.Cols[COL_TASKICON].Width = 20; //Convert.ToInt32(nWidth * 0.03);
                c1ViewTask.Cols[COL_PRIORITYICON].Width = 20;
                c1ViewTask.Cols[COL_PRIORITYICON].Visible = true;
                c1ViewTask.Cols[COL_FOLLOWUPICON].Width = 20; //Convert.ToInt32(nWidth * 0.02);

                c1ViewTask.Cols[COL_TASKID].Width = 0;

                c1ViewTask.Cols[COL_SELECT].DataType = System.Type.GetType("System.Boolean");//Select Column
                c1ViewTask.Cols[COL_SELECT].AllowEditing = true;
                //c1ViewTask.Cols[COL_SELECT].Width = Convert.ToInt32(nWidth * 0.05);
                c1ViewTask.Cols[COL_SELECT].Visible = false;
                c1ViewTask.Cols[COL_PRIORITYNAME].Visible = false;

                c1ViewTask.Cols[COL_PROVIDERID].Width = 0;
                c1ViewTask.Cols[COL_PROVIDERNAME].Width = 0;

                c1ViewTask.Cols[COL_PATIENTID].Width = 0;
                //Bug #48802: gloEMR - Tasks - Application is hiding patient name column when user tries to complete task.
                c1ViewTask.Cols[COL_PATIENTNAME].Width = 171;

                c1ViewTask.Cols[COL_SUBJECT].Width = Convert.ToInt32(nWidth * 0.40);

                c1ViewTask.Cols[COL_STARTDATE].Width = 0;
                c1ViewTask.Cols[COL_DUEDATE].Width = Convert.ToInt32(nWidth * 0.10);

                c1ViewTask.Cols[COL_DUEDATE].DataType = typeof(System.DateTime);
                c1ViewTask.Cols[COL_DUEDATE].Format = "MM/dd/yyyy";

                c1ViewTask.Cols[COL_STATUSID].Width = 0;
                c1ViewTask.Cols[COL_STATUSNAME].Width = Convert.ToInt32(nWidth * 0.08);

                c1ViewTask.Cols[COL_PRIORITYID].Width = 0;
                c1ViewTask.Cols[COL_PRIORITYNAME].Width = Convert.ToInt32(nWidth * 0.10);

                c1ViewTask.Cols[COL_COMPLETE].Width = Convert.ToInt32(nWidth * 0.06);
                c1ViewTask.Cols[COL_COMPLETE].TextAlign = TextAlignEnum.LeftCenter;

                c1ViewTask.Cols[COL_CATEGORYID].Width = 0;
                c1ViewTask.Cols[COL_CATEGORYNAME].Width = 0;

                c1ViewTask.Cols[COL_FOLLOWUPID].Width = 0;
                c1ViewTask.Cols[COL_FOLLOWUPNAME].Width = 0;

                c1ViewTask.Cols[COL_ISPRIVATE].Width = 0;

                c1ViewTask.Cols[COL_OWNERID].Width = 0;
                c1ViewTask.Cols[COL_OWNERNAME].Width = 0;

                c1ViewTask.Cols[COL_ASSIGNEDTOID].Width = 0;
                c1ViewTask.Cols[COL_ASSIGNEDTONAME].Width = 0;

                c1ViewTask.Cols[COL_DESCRIPTION].Width = 0;
                c1ViewTask.Cols[COL_DATECOMPLETED].Width = 0;
                c1ViewTask.Cols[COL_NOTES].Width = 0;
                c1ViewTask.Cols[COL_USERID].Width = 0;
                c1ViewTask.Cols[COL_CLINICID].Width = 0;
                c1ViewTask.Cols[COL_PRIORITYLEVEL].Visible = false;
                c1ViewTask.Cols[COL_FROMID].Width = 0;
                c1ViewTask.Cols[COL_ASSIGNEDTO].Width = Convert.ToInt32(nWidth * 0.08);


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void ReIntializeTaskGrid()
        {
            int nWidth;
            nWidth = pnl_Grid.Width - 2;
            c1ViewTask.Cols[COL_TASKICON].Width = Convert.ToInt32(nWidth * 0.03);
            c1ViewTask.Cols[COL_PRIORITYICON].Width = 20;
            c1ViewTask.Cols[COL_FOLLOWUPICON].Width = Convert.ToInt32(nWidth * 0.02);
            c1ViewTask.Cols[COL_TASKID].Width = 0;
            c1ViewTask.Cols[COL_PROVIDERID].Width = 0;
            c1ViewTask.Cols[COL_PROVIDERNAME].Width = 0;
            c1ViewTask.Cols[COL_PATIENTID].Width = 0;
            c1ViewTask.Cols[COL_PATIENTNAME].Width = 0;
            c1ViewTask.Cols[COL_SUBJECT].Width = Convert.ToInt32(nWidth * 0.40);
            c1ViewTask.Cols[COL_STARTDATE].Width = 0;
            c1ViewTask.Cols[COL_DUEDATE].Width = Convert.ToInt32(nWidth * 0.10);
            c1ViewTask.Cols[COL_STATUSID].Width = 0;
            c1ViewTask.Cols[COL_STATUSNAME].Width = Convert.ToInt32(nWidth * 0.08);
            c1ViewTask.Cols[COL_PRIORITYID].Width = 0;
            c1ViewTask.Cols[COL_PRIORITYNAME].Width = Convert.ToInt32(nWidth * 0.11);
            c1ViewTask.Cols[COL_COMPLETE].Width = Convert.ToInt32(nWidth * 0.08);
            c1ViewTask.Cols[COL_CATEGORYID].Width = 0;
            c1ViewTask.Cols[COL_CATEGORYNAME].Width = 0;
            c1ViewTask.Cols[COL_FOLLOWUPID].Width = 0;
            c1ViewTask.Cols[COL_FOLLOWUPNAME].Width = 0;
            c1ViewTask.Cols[COL_ISPRIVATE].Width = 0;
            c1ViewTask.Cols[COL_OWNERID].Width = 0;
            c1ViewTask.Cols[COL_OWNERNAME].Width = 0;
            c1ViewTask.Cols[COL_ASSIGNEDTOID].Width = 0;
            c1ViewTask.Cols[COL_ASSIGNEDTONAME].Width = 0;
            c1ViewTask.Cols[COL_DESCRIPTION].Width = 0;
            c1ViewTask.Cols[COL_DATECOMPLETED].Width = 0;
            c1ViewTask.Cols[COL_NOTES].Width = 0;
            c1ViewTask.Cols[COL_USERID].Width = 0;
            c1ViewTask.Cols[COL_CLINICID].Width = 0;
            c1ViewTask.Cols[COL_FROMID].Width = 0;
            c1ViewTask.Cols[COL_ASSIGNEDTO].Width = Convert.ToInt32(nWidth * 0.08);
        }


        private void fill_c1Task()
        {
            gloTask ogloTask = new gloTask(_databaseconnnectionstring);
            TaskAssigns oTaskAssigns = new TaskAssigns();
            Tasks oTasks = new Tasks();
            int count, count1;
            int RowNo = 0;

            c1ViewTask.BeginUpdate();

            C1.Win.C1FlexGrid.CellStyle csSubject;// = c1ViewTask.Styles.Add("cs_Subject");
            try
            {
                if (c1ViewTask.Styles.Contains("cs_Subject"))
                {
                    csSubject = c1ViewTask.Styles["cs_Subject"];
                }
                else
                {
                    csSubject = c1ViewTask.Styles.Add("cs_Subject");
                    csSubject.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT;//new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                }

            }
            catch
            {
                csSubject = c1ViewTask.Styles.Add("cs_Subject");
                csSubject.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT;//new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            }

            C1.Win.C1FlexGrid.CellStyle csTaskIcon;// = c1ViewTask.Styles.Add("csTaskIcon");
            try
            {
                if (c1ViewTask.Styles.Contains("csTaskIcon"))
                {
                    csTaskIcon = c1ViewTask.Styles["csTaskIcon"];
                }
                else
                {
                    csTaskIcon = c1ViewTask.Styles.Add("csTaskIcon");
                    csTaskIcon.ImageAlign = ImageAlignEnum.CenterCenter;
                    csTaskIcon.TextAlign = TextAlignEnum.LeftTop;
                    csTaskIcon.BackgroundImageLayout = ImageAlignEnum.CenterCenter;
                }

            }
            catch
            {
                csTaskIcon = c1ViewTask.Styles.Add("csTaskIcon");
                csTaskIcon.ImageAlign = ImageAlignEnum.CenterCenter;
                csTaskIcon.TextAlign = TextAlignEnum.LeftTop;
                csTaskIcon.BackgroundImageLayout = ImageAlignEnum.CenterCenter;

            }


            //System.Drawing.Image img = global::gloTaskMail.Properties.Resources.Task;
            //System.Drawing.Image img1 = global::gloTaskMail.Properties.Resources.Task;
            //System.Drawing.Image img2 = global::gloTaskMail.Properties.Resources.Task;
            //System.Drawing.Image img3 = global::gloTaskMail.Properties.Resources.Task;
            //System.Drawing.Image img4 = global::gloTaskMail.Properties.Resources.Task;
            //System.Drawing.Image img5 = global::gloTaskMail.Properties.Resources.Task;

            System.Drawing.Image img = global::gloTaskMail.Properties.Resources.High_PriorityRed;
            System.Drawing.Image img1 = global::gloTaskMail.Properties.Resources.Low_Priority;
            System.Drawing.Image img2 = global::gloTaskMail.Properties.Resources.Today;
            System.Drawing.Image img3 = global::gloTaskMail.Properties.Resources.Tommorow;
            System.Drawing.Image img4 = global::gloTaskMail.Properties.Resources.No_Date;
            System.Drawing.Image img5 = global::gloTaskMail.Properties.Resources.Flag_Yellow;
            System.Drawing.Image img_Single = global::gloTaskMail.Properties.Resources.Task_Single.ToBitmap();
            System.Drawing.Image img_NoOwner = global::gloTaskMail.Properties.Resources.Task_NoOwner.ToBitmap();
            System.Drawing.Image img_OtherTaken = global::gloTaskMail.Properties.Resources.Task_OtherTaken.ToBitmap();
            System.Drawing.Image img_Owner = global::gloTaskMail.Properties.Resources.Task_Owner.ToBitmap();
            c1ViewTask.Cols[COL_Resp].ImageAlign = ImageAlignEnum.CenterCenter;
            //  System.Drawing.Image img4 = global::gloTaskMail.Properties.Resources.No_Date;
            //   System.Drawing.Image img5 = global::gloTaskMail.Properties.Resources.Flag_Yellow;
            try
            {
                string struserids = Convert.ToString(UserId);
                // Retrives All Accepted Tasks
                //oTasks = ogloTask.GetUserTasks(UserId);
                oTasks = ogloTask.GetUserTasksNew(struserids);


                count = FillAssginedTask();
             

                if (oTasks != null && oTasks.Count > 0)
                {
                    count1 = oTasks.Count + count;  //added for bugid 106870
                    for (int i = count, j = 0; i <= count1 - 1; i++, j++)
                    {
                        if (c1ViewTask.Rows.Count - 1 <= i)
                        {
                            c1ViewTask.Rows.Add();
                        }

                        c1ViewTask.SetData(i + 1, COL_TASKID, oTasks[j].TaskID);
                        c1ViewTask.SetData(i + 1, COL_PROVIDERID, oTasks[j].ProviderID);
                        c1ViewTask.SetData(i + 1, COL_PROVIDERNAME, oTasks[j].ProviderName);

                        c1ViewTask.SetData(i + 1, COL_PATIENTID, oTasks[j].PatientID);
                        c1ViewTask.SetData(i + 1, COL_PATIENTNAME, oTasks[j].PatientName);

                        c1ViewTask.SetData(i + 1, COL_SUBJECT, oTasks[j].Subject);

                        c1ViewTask.SetData(i + 1, COL_STARTDATE, gloDateMaster.gloDate.DateAsDate(oTasks[j].StartDate));
                        c1ViewTask.SetData(i + 1, COL_DUEDATE, gloDateMaster.gloDate.DateAsDate(oTasks[j].DueDate));


                        c1ViewTask.SetData(i + 1, COL_PRIORITYID, oTasks[j].PriorityID);
                        c1ViewTask.SetData(i + 1, COL_PRIORITYNAME, oTasks[j].Priority);


                        if (oTasks[j].PriorityLevel == 1)
                        {
                            c1ViewTask.SetCellImage(i + 1, COL_PRIORITYICON, img);

                            c1ViewTask.SetData(i + 1, COL_PRIORITYICON, "1");
                        }
                        else if (oTasks[j].PriorityLevel == 2)
                        {
                        }
                        else if (oTasks[j].PriorityLevel == 3)
                        {
                            c1ViewTask.SetCellImage(i + 1, COL_PRIORITYICON, img1);
                            c1ViewTask.SetData(i + 1, COL_PRIORITYICON, "3");
                        }

                        c1ViewTask.SetData(i + 1, COL_CATEGORYID, oTasks[j].CategoryID);
                        c1ViewTask.SetData(i + 1, COL_CATEGORYNAME, oTasks[j].Category);

                        c1ViewTask.SetData(i + 1, COL_FOLLOWUPID, oTasks[j].FollowupID);
                        c1ViewTask.SetData(i + 1, COL_FOLLOWUPNAME, oTasks[j].Followup);
                        if (oTasks[j].Followup.ToUpper() == "TODAY")
                        {
                            // img = global::gloTaskMail.Properties.Resources.Today;
                            c1ViewTask.SetCellImage(i + 1, COL_FOLLOWUPICON, img2);
                            c1ViewTask.SetCellStyle(i + 1, COL_FOLLOWUPICON, csTaskIcon);
                        }
                        else if (oTasks[j].Followup.ToUpper() == "Tomorrow")
                        {
                            // img = global::gloTaskMail.Properties.Resources.Tomorrow;
                            c1ViewTask.SetCellImage(i + 1, COL_FOLLOWUPICON, img3);
                            c1ViewTask.SetCellStyle(i + 1, COL_FOLLOWUPICON, csTaskIcon);
                        }
                        else if (oTasks[j].Followup.ToUpper() == "NO DATE")
                        {
                            // img = global::gloTaskMail.Properties.Resources.No_Date;
                            c1ViewTask.SetCellImage(i + 1, COL_FOLLOWUPICON, img4);
                            c1ViewTask.SetCellStyle(i + 1, COL_FOLLOWUPICON, csTaskIcon);
                        }
                        else
                        {
                            // img = global::gloTaskMail.Properties.Resources.Flag_Yellow;
                            c1ViewTask.SetCellImage(i + 1, COL_FOLLOWUPICON, img5);
                            c1ViewTask.SetCellStyle(i + 1, COL_FOLLOWUPICON, csTaskIcon);
                        }
                        if (oTasks[j].Resp.ToUpper().Trim() == "TASK_SINGLE") // added for bugid 99357 
                        {
                            c1ViewTask.SetCellImage(i + 1, COL_Resp, img_Single);
                        }
                        if (oTasks[j].Resp.ToUpper().Trim() == "TASK_NOOWNER")
                        {
                            c1ViewTask.SetCellImage(i + 1, COL_Resp, img_NoOwner);
                        }
                        if (oTasks[j].Resp.ToUpper().Trim() == "TASK_OWNER")
                        {
                            c1ViewTask.SetCellImage(i + 1, COL_Resp, img_Owner);
                        }
                        if (oTasks[j].Resp.ToUpper().Trim() == "TASK_OTHERTAKEN")
                        {
                            c1ViewTask.SetCellImage(i + 1, COL_Resp, img_OtherTaken);
                        }
                        c1ViewTask.SetData(i + 1, COL_ISPRIVATE, oTasks[j].IsPrivate);

                        c1ViewTask.SetData(i + 1, COL_OWNERID, oTasks[j].OwnerID);
                        c1ViewTask.SetData(i + 1, COL_OWNERNAME, oTasks[j].OwnerName);

                        c1ViewTask.SetData(i + 1, COL_ASSIGNEDTOID, oTasks[j].Assignment[0].AssignToID);
                        c1ViewTask.SetData(i + 1, COL_ASSIGNEDTONAME, oTasks[j].Assignment[0].AssignToName);

                        c1ViewTask.SetData(i + 1, COL_DESCRIPTION, oTasks[j].Progress.Description);
                        c1ViewTask.SetData(i + 1, COL_DATECOMPLETED, oTasks[j].Progress.Complete);

                        c1ViewTask.SetData(i + 1, COL_STATUSID, oTasks[j].Progress.StatusID);
                        c1ViewTask.SetData(i + 1, COL_STATUSNAME, oTasks[j].Progress.StatusName);

                        c1ViewTask.SetData(i + 1, COL_COMPLETE, oTasks[j].Progress.Complete);

                        c1ViewTask.SetData(i + 1, COL_NOTES, oTasks[j].Notes);
                        c1ViewTask.SetData(i + 1, COL_USERID, oTasks[j].UserID);
                        c1ViewTask.SetData(i + 1, COL_CLINICID, oTasks[j].ClinicID);
                        c1ViewTask.SetData(i + 1, COL_PRIORITYLEVEL, oTasks[j].PriorityLevel);
                        c1ViewTask.SetData(i + 1, COL_FROMID, oTasks[j].Assignment[0].AssignFromID);

                        c1ViewTask.SetData(i + 1, COL_ASSIGNEDTO, oTasks[j].Assign_To);

                        if (oTasks[j].Progress.Complete == 100)
                        {
                            this.c1ViewTask.Rows[i + 1].Style = csSubject;
                        }

                        //Bug #47913: Taks -- > Setting the Priority Wipes the Taks Details
                        //get the row which is previously selected.
                        if (TaskID == oTasks[j].TaskID)
                        {
                            RowNo = c1ViewTask.Rows.Count - 1;
                        }
                    }
                }

                //Bug #47913: Taks -- > Setting the Priority Wipes the Taks Details                
                if (RowNo != 0)
                {
                    c1ViewTask.Select(RowNo, COL_SUBJECT);
                }
                else
                {
                    c1ViewTask.Select(1, COL_SUBJECT);
                }
                c1ViewTask.EndUpdate();

            }//try
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        private int FillAssginedTask()
        {
            gloTask ogloTask = new gloTask(_databaseconnnectionstring);
            TaskAssigns oTaskAssigns = new TaskAssigns();
            Task oTask;

            C1.Win.C1FlexGrid.CellStyle csSubject;// = c1ViewTask.Styles.Add("cs_Subject");
            try
            {
                if (c1ViewTask.Styles.Contains("cs_Subject"))
                {
                    csSubject = c1ViewTask.Styles["cs_Subject"];
                }
                else
                {
                    csSubject = c1ViewTask.Styles.Add("cs_Subject");


                    csSubject.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT;//new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                }

            }
            catch
            {
                csSubject = c1ViewTask.Styles.Add("cs_Subject");


                csSubject.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT;//new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            }
            C1.Win.C1FlexGrid.CellStyle asgTask;// = c1ViewTask.Styles.Add("asgTask");
            try
            {
                if (c1ViewTask.Styles.Contains("asgTask"))
                {
                    asgTask = c1ViewTask.Styles["asgTask"];
                }
                else
                {
                    asgTask = c1ViewTask.Styles.Add("asgTask");


                    asgTask.Font = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                }

            }
            catch
            {
                asgTask = c1ViewTask.Styles.Add("asgTask");


                asgTask.Font = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            }

            C1.Win.C1FlexGrid.CellStyle csTaskIcon; //= c1ViewTask.Styles.Add("csTaskIcon");
            try
            {
                if (c1ViewTask.Styles.Contains("csTaskIcon"))
                {
                    csTaskIcon = c1ViewTask.Styles["csTaskIcon"];
                }
                else
                {
                    csTaskIcon = c1ViewTask.Styles.Add("csTaskIcon");


                    csTaskIcon.ImageAlign = ImageAlignEnum.CenterCenter;
                    csTaskIcon.TextAlign = TextAlignEnum.LeftTop;
                    csTaskIcon.BackgroundImageLayout = ImageAlignEnum.CenterCenter;
                }

            }
            catch
            {
                csTaskIcon = c1ViewTask.Styles.Add("csTaskIcon");


                csTaskIcon.ImageAlign = ImageAlignEnum.CenterCenter;
                csTaskIcon.TextAlign = TextAlignEnum.LeftTop;
                csTaskIcon.BackgroundImageLayout = ImageAlignEnum.CenterCenter;

            }

            System.Drawing.Image img = global::gloTaskMail.Properties.Resources.Task;
            try
            {
                oTaskAssigns = ogloTask.GetAssignedTasks(UserId);

                if (oTaskAssigns != null && oTaskAssigns.Count > 0)
                {

                    for (int i = 0; i < oTaskAssigns.Count; i++)
                    {

                        if (c1ViewTask.Rows.Count - 1 <= i)
                        {
                            c1ViewTask.Rows.Add();
                        }

                        oTask = new Task();
                        oTask = ogloTask.GetTask(oTaskAssigns[i].TaskID);
                        img = global::gloTaskMail.Properties.Resources.Task;
                        c1ViewTask.SetCellImage(i + 1, COL_TASKICON, img);
                        c1ViewTask.SetCellStyle(i + 1, COL_TASKICON, csTaskIcon);

                        c1ViewTask.SetData(i + 1, COL_TASKID, oTask.TaskID);
                        //c1ViewTask.SetData(i + 1, COL_SELECT, );
                        c1ViewTask.SetData(i + 1, COL_PROVIDERID, oTask.ProviderID);
                        c1ViewTask.SetData(i + 1, COL_PROVIDERNAME, oTask.ProviderName);

                        c1ViewTask.SetData(i + 1, COL_PATIENTID, oTask.PatientID);
                        c1ViewTask.SetData(i + 1, COL_PATIENTNAME, oTask.PatientName);

                        c1ViewTask.SetData(i + 1, COL_SUBJECT, oTask.Subject);

                        c1ViewTask.SetData(i + 1, COL_STARTDATE, gloDateMaster.gloDate.DateAsDate(oTask.StartDate));
                        c1ViewTask.SetData(i + 1, COL_DUEDATE, gloDateMaster.gloDate.DateAsDate(oTask.DueDate));


                        c1ViewTask.SetData(i + 1, COL_PRIORITYID, oTask.PriorityID);
                        c1ViewTask.SetData(i + 1, COL_PRIORITYNAME, oTask.Priority);


                        c1ViewTask.SetData(i + 1, COL_ASSIGNEDTO, oTask.Assign_To);
                        if (oTask.PriorityLevel == 1)
                        {
                            img = global::gloTaskMail.Properties.Resources.High_PriorityRed;
                            c1ViewTask.SetCellImage(i + 1, COL_PRIORITYICON, img);

                            //c1ViewTask.SetCellStyle(i + 1, COL_PRIORITYICON, csTaskIcon);
                            c1ViewTask.SetData(i + 1, COL_PRIORITYICON, "1");
                        }
                        else if (oTask.PriorityLevel == 2)
                        {
                        }
                        else if (oTask.PriorityLevel == 3)
                        {
                            img = global::gloTaskMail.Properties.Resources.Low_Priority;
                            c1ViewTask.SetCellImage(i + 1, COL_PRIORITYICON, img);
                            //c1ViewTask.SetCellStyle(i + 1, COL_PRIORITYICON, csTaskIcon);
                            c1ViewTask.SetData(i + 1, COL_PRIORITYICON, "3");
                        }

                        c1ViewTask.SetData(i + 1, COL_CATEGORYID, oTask.CategoryID);
                        c1ViewTask.SetData(i + 1, COL_CATEGORYNAME, oTask.Category);

                        c1ViewTask.SetData(i + 1, COL_FOLLOWUPID, oTask.FollowupID);
                        c1ViewTask.SetData(i + 1, COL_FOLLOWUPNAME, oTask.Followup);
                        if (oTask.Followup.ToUpper() == "TODAY")
                        {
                            img = global::gloTaskMail.Properties.Resources.Today;
                            c1ViewTask.SetCellImage(i + 1, COL_FOLLOWUPICON, img);
                            c1ViewTask.SetCellStyle(i + 1, COL_FOLLOWUPICON, csTaskIcon);
                        }
                        else if (oTask.Followup.ToUpper() == "Tomorrow")
                        {
                            img = global::gloTaskMail.Properties.Resources.Tommorow;
                            c1ViewTask.SetCellImage(i + 1, COL_FOLLOWUPICON, img);
                            c1ViewTask.SetCellStyle(i + 1, COL_FOLLOWUPICON, csTaskIcon);
                        }
                        else if (oTask.Followup.ToUpper() == "NO DATE")
                        {
                            img = global::gloTaskMail.Properties.Resources.No_Date;
                            c1ViewTask.SetCellImage(i + 1, COL_FOLLOWUPICON, img);
                            c1ViewTask.SetCellStyle(i + 1, COL_FOLLOWUPICON, csTaskIcon);
                        }
                        else
                        {
                            img = global::gloTaskMail.Properties.Resources.Flag_Yellow;
                            c1ViewTask.SetCellImage(i + 1, COL_FOLLOWUPICON, img);
                            c1ViewTask.SetCellStyle(i + 1, COL_FOLLOWUPICON, csTaskIcon);
                        }

                        c1ViewTask.SetData(i + 1, COL_ISPRIVATE, oTask.IsPrivate);

                        c1ViewTask.SetData(i + 1, COL_OWNERID, oTask.OwnerID);
                        c1ViewTask.SetData(i + 1, COL_OWNERNAME, oTask.OwnerName);

                        c1ViewTask.SetData(i + 1, COL_DESCRIPTION, oTask.Progress.Description);
                        c1ViewTask.SetData(i + 1, COL_DATECOMPLETED, oTask.Progress.Complete);

                        c1ViewTask.SetData(i + 1, COL_STATUSID, oTask.Progress.StatusID);
                        c1ViewTask.SetData(i + 1, COL_STATUSNAME, oTask.Progress.StatusName);

                        c1ViewTask.SetData(i + 1, COL_COMPLETE, oTask.Progress.Complete);

                        img = global::gloTaskMail.Properties.Resources.Task;
                        c1ViewTask.SetCellImage(i + 1, COL_TASKICON, img);
                        c1ViewTask.SetCellStyle(i + 1, COL_TASKICON, csTaskIcon);

                        c1ViewTask.SetData(i + 1, COL_NOTES, oTask.Notes);
                        c1ViewTask.SetData(i + 1, COL_USERID, oTask.UserID);
                        c1ViewTask.SetData(i + 1, COL_CLINICID, oTask.ClinicID);
                        c1ViewTask.SetData(i + 1, COL_PRIORITYLEVEL, oTask.PriorityLevel);
                        c1ViewTask.SetData(i + 1, COL_FROMID, oTask.Assignment[0].AssignFromID);

                        c1ViewTask.SetData(i + 1, COL_ASSIGNEDTO, oTask.Assign_To);

                        if (oTask.Progress.Complete == 100 && oTask.Progress.StatusID == 3)
                        {
                            c1ViewTask.SetCellStyle(i + 1, COL_SUBJECT, csSubject);
                            c1ViewTask.SetCellStyle(i + 1, COL_DUEDATE, csSubject);
                            c1ViewTask.SetCellStyle(i + 1, COL_STATUSNAME, csSubject);
                            c1ViewTask.SetCellStyle(i + 1, COL_PRIORITYNAME, csSubject);
                            c1ViewTask.SetCellStyle(i + 1, COL_COMPLETE, csSubject);
                            c1ViewTask.SetCellStyle(i + 1, COL_ASSIGNEDTO, csSubject);
                            //Bug #48802: gloEMR - Tasks - Application is hiding patient name column when user tries to complete task.
                            c1ViewTask.SetCellStyle(i + 1, COL_PATIENTNAME, csSubject);
                        }
                        c1ViewTask.SetCellStyle(i + 1, COL_SUBJECT, asgTask);
                        c1ViewTask.SetCellStyle(i + 1, COL_DUEDATE, asgTask);
                        c1ViewTask.SetCellStyle(i + 1, COL_STATUSNAME, asgTask);
                        c1ViewTask.SetCellStyle(i + 1, COL_PRIORITYNAME, asgTask);
                        c1ViewTask.SetCellStyle(i + 1, COL_COMPLETE, asgTask);
                        //Bug #48802: gloEMR - Tasks - Application is hiding patient name column when user tries to complete task.
                        c1ViewTask.SetCellStyle(i + 1, COL_PATIENTNAME, asgTask);
                        c1ViewTask.SetCellStyle(i + 1, COL_ASSIGNEDTO, asgTask);
                    }

                    return oTaskAssigns.Count;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return 0;
            }
            finally
            {
                ogloTask.Dispose();

            }

        }

        #endregion " Flex Grid Design & Fill methods "

        #region " Public & Private Methods "

        //16-Nov-15 Aniket: 8070 Refresh Tasks/Messages when different user logs in after lock screen

        public void Fill_Users_All_Tasks(string struserids = "", bool IsUserTaskdropdownVisible = false)
        {

            if (IsUserTaskdropdownVisible == false)
            {
                cmbToUsers.Visible = false;
                btnToBrowse.Visible = false;
                btnToDelete.Visible = false;
                lbluser.Visible = false;
            }



            if (struserids.Trim() == "")
            {
                struserids = Convert.ToString(_userId);


                olist_Ids();
            }

       




            gloTask ogloTask = new gloTask(_databaseconnnectionstring);
            Tasks oTasks = new Tasks();
            int RowNo = 0;
            try
            {


                //c1ViewTask.Select(0,0);
                SearchString = txtSearch.Text.Trim();
                this.Cursor = Cursors.WaitCursor;
                // c1ViewTask.Clear();
                c1ViewTask.DataSource = null;
                c1ViewTask.Cols.Count = COL_COUNT;
                c1ViewTask.Cols.Fixed = 0;
                c1ViewTask.Rows.Count = 1;
                C1.Win.C1FlexGrid.CellStyle csSubiect;// = c1ViewTask.Styles.Add("cs_Subiect");
                try
                {
                    if (c1ViewTask.Styles.Contains("cs_Subiect"))
                    {
                        csSubiect = c1ViewTask.Styles["cs_Subiect"];
                    }
                    else
                    {
                        csSubiect = c1ViewTask.Styles.Add("cs_Subiect");


                        csSubiect.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT;// new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    }

                }
                catch
                {
                    csSubiect = c1ViewTask.Styles.Add("cs_Subiect");


                    csSubiect.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT;//new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                }

                C1.Win.C1FlexGrid.CellStyle asgTask;// = c1ViewTask.Styles.Add("asgTask");
                try
                {
                    if (c1ViewTask.Styles.Contains("asgTask"))
                    {
                        asgTask = c1ViewTask.Styles["asgTask"];
                    }
                    else
                    {
                        asgTask = c1ViewTask.Styles.Add("asgTask");


                        asgTask.Font = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    }

                }
                catch
                {
                    asgTask = c1ViewTask.Styles.Add("asgTask");


                    asgTask.Font = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                }

                C1.Win.C1FlexGrid.CellStyle csTaskIcon;//= c1ViewTask.Styles.Add("csTaskIcon");
                try
                {
                    if (c1ViewTask.Styles.Contains("csTaskIcon"))
                    {
                        csTaskIcon = c1ViewTask.Styles["csTaskIcon"];
                    }
                    else
                    {
                        csTaskIcon = c1ViewTask.Styles.Add("csTaskIcon");


                        csTaskIcon.ImageAlign = ImageAlignEnum.CenterCenter;
                        csTaskIcon.TextAlign = TextAlignEnum.LeftTop;
                        csTaskIcon.BackgroundImageLayout = ImageAlignEnum.CenterCenter;
                    }

                }
                catch
                {
                    csTaskIcon = c1ViewTask.Styles.Add("csTaskIcon");


                    csTaskIcon.ImageAlign = ImageAlignEnum.CenterCenter;
                    csTaskIcon.TextAlign = TextAlignEnum.LeftTop;
                    csTaskIcon.BackgroundImageLayout = ImageAlignEnum.CenterCenter;

                }

                System.Drawing.Image img = global::gloTaskMail.Properties.Resources.Task; ;


                //System.Drawing.Image img1 = global::gloTaskMail.Properties.Resources.Task;
                //System.Drawing.Image img2 = global::gloTaskMail.Properties.Resources.Task;
                //System.Drawing.Image img3 = global::gloTaskMail.Properties.Resources.Task;
                //System.Drawing.Image img4 = global::gloTaskMail.Properties.Resources.Task;
                //System.Drawing.Image img5 = global::gloTaskMail.Properties.Resources.Task;
                //System.Drawing.Image img6 = global::gloTaskMail.Properties.Resources.Task;

                System.Drawing.Image img1 = global::gloTaskMail.Properties.Resources.Low_Priority;
                System.Drawing.Image img2 = global::gloTaskMail.Properties.Resources.Today;
                System.Drawing.Image img3 = global::gloTaskMail.Properties.Resources.Tommorow;
                System.Drawing.Image img4 = global::gloTaskMail.Properties.Resources.No_Date;
                System.Drawing.Image img5 = global::gloTaskMail.Properties.Resources.Flag_Yellow;
                System.Drawing.Image img6 = global::gloTaskMail.Properties.Resources.High_PriorityRed;
                System.Drawing.Image img_Single = global::gloTaskMail.Properties.Resources.Task_Single.ToBitmap();
                System.Drawing.Image img_NoOwner = global::gloTaskMail.Properties.Resources.Task_NoOwner.ToBitmap();
                System.Drawing.Image img_OtherTaken = global::gloTaskMail.Properties.Resources.Task_OtherTaken.ToBitmap();
                System.Drawing.Image img_Owner = global::gloTaskMail.Properties.Resources.Task_Owner.ToBitmap();


                c1ViewTask.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
                c1ViewTask.SelectionMode = SelectionModeEnum.Row;
                c1ViewTask.ShowCellLabels = false;

                c1ViewTask.SetData(0, COL_TASKICON, "");
                c1ViewTask.SetData(0, COL_PRIORITYICON, "");
                c1ViewTask.SetData(0, COL_FOLLOWUPICON, "");
                c1ViewTask.SetData(0, COL_TASKID, "TASKID");
                c1ViewTask.SetData(0, COL_SELECT, "");
                c1ViewTask.SetData(0, COL_PROVIDERID, "PROVIDERID");
                c1ViewTask.SetData(0, COL_PROVIDERNAME, "PROVIDERNAME");
                c1ViewTask.SetData(0, COL_PATIENTID, "PATIENTID");
                c1ViewTask.SetData(0, COL_PATIENTNAME, "Patient Name");
                c1ViewTask.SetData(0, COL_SUBJECT, "Subject");
                c1ViewTask.SetData(0, COL_STARTDATE, "STARTDATE");
                c1ViewTask.SetData(0, COL_DUEDATE, "Due Date");
                c1ViewTask.SetData(0, COL_STATUSID, "STATUSID");
                c1ViewTask.SetData(0, COL_STATUSNAME, "Status");
                c1ViewTask.SetData(0, COL_PRIORITYID, "PRIORITYID");
                c1ViewTask.SetData(0, COL_PRIORITYNAME, "Priority");
                c1ViewTask.SetData(0, COL_COMPLETE, "% Complete");
                c1ViewTask.SetData(0, COL_CATEGORYID, "CATEGORYID");
                c1ViewTask.SetData(0, COL_CATEGORYNAME, "Category Name");
                c1ViewTask.SetData(0, COL_FOLLOWUPID, "FOLLOWUPID");
                c1ViewTask.SetData(0, COL_FOLLOWUPNAME, "Followup Name");
                c1ViewTask.SetData(0, COL_ISPRIVATE, "ISPRIVATE");
                c1ViewTask.SetData(0, COL_OWNERID, "OWNERID");
                c1ViewTask.SetData(0, COL_OWNERNAME, "Owner Name");
                c1ViewTask.SetData(0, COL_ASSIGNEDTOID, "ASSIGNEDTOID");
                c1ViewTask.SetData(0, COL_ASSIGNEDTONAME, "Assigned To Name");
                c1ViewTask.SetData(0, COL_DESCRIPTION, "Description");
                c1ViewTask.SetData(0, COL_DATECOMPLETED, "Date Completed");
                c1ViewTask.SetData(0, COL_NOTES, "Notes");
                c1ViewTask.SetData(0, COL_USERID, "USERID");
                c1ViewTask.SetData(0, COL_CLINICID, "CLINICID");
                c1ViewTask.SetData(0, COL_FROMID, "FROMID");
                c1ViewTask.SetData(0, COL_Resp, "Resp");
                c1ViewTask.SetData(0, COL_ASSIGNEDTO, "AssignedTo");

                int nWidth;
                nWidth = pnl_Grid.Width - 2;
                //nWidth = panel4.Width;

                c1ViewTask.Cols[COL_TASKICON].Width = 20; //Convert.ToInt32(nWidth * 0.03);
                c1ViewTask.Cols[COL_PRIORITYICON].Width = 20;
                c1ViewTask.Cols[COL_PRIORITYICON].Visible = true;
                c1ViewTask.Cols[COL_FOLLOWUPICON].Width = 20; //Convert.ToInt32(nWidth * 0.02);

                c1ViewTask.Cols[COL_TASKID].Width = 0;

                c1ViewTask.Cols[COL_SELECT].DataType = System.Type.GetType("System.Boolean");//Select Column
                c1ViewTask.Cols[COL_SELECT].AllowEditing = true;
                c1ViewTask.Cols[COL_SELECT].Visible = false;
                c1ViewTask.Cols[COL_PRIORITYNAME].Visible = false;

                c1ViewTask.Cols[COL_PROVIDERID].Width = 0;
                c1ViewTask.Cols[COL_PROVIDERNAME].Width = 0;

                c1ViewTask.Cols[COL_PATIENTID].Width = 0;
                c1ViewTask.Cols[COL_PATIENTNAME].Width = 171;

                c1ViewTask.Cols[COL_SUBJECT].Width = Convert.ToInt32(nWidth * 0.40);

                c1ViewTask.Cols[COL_STARTDATE].Width = 0;
                c1ViewTask.Cols[COL_DUEDATE].Width = Convert.ToInt32(nWidth * 0.08);
                c1ViewTask.Cols[COL_DUEDATE].DataType = typeof(System.DateTime);
                c1ViewTask.Cols[COL_DUEDATE].Format = "MM/dd/yyyy";


                c1ViewTask.Cols[COL_STATUSID].Width = 0;
                c1ViewTask.Cols[COL_STATUSNAME].Width = Convert.ToInt32(nWidth * 0.08);

                c1ViewTask.Cols[COL_PRIORITYID].Width = 0;
                c1ViewTask.Cols[COL_PRIORITYNAME].Width = Convert.ToInt32(nWidth * 0.10);

                c1ViewTask.Cols[COL_COMPLETE].Width = Convert.ToInt32(nWidth * 0.08);
                c1ViewTask.Cols[COL_COMPLETE].TextAlign = TextAlignEnum.LeftCenter;

                c1ViewTask.Cols[COL_CATEGORYID].Width = 0;
                c1ViewTask.Cols[COL_CATEGORYNAME].Width = 0;

                c1ViewTask.Cols[COL_FOLLOWUPID].Width = 0;
                c1ViewTask.Cols[COL_FOLLOWUPNAME].Width = 0;

                c1ViewTask.Cols[COL_ISPRIVATE].Width = 0;

                c1ViewTask.Cols[COL_OWNERID].Width = 0;
                c1ViewTask.Cols[COL_OWNERNAME].Width = 0;

                c1ViewTask.Cols[COL_ASSIGNEDTOID].Width = 0;
                c1ViewTask.Cols[COL_ASSIGNEDTONAME].Width = 0;

                c1ViewTask.Cols[COL_DESCRIPTION].Width = 0;
                c1ViewTask.Cols[COL_DATECOMPLETED].Width = 0;
                c1ViewTask.Cols[COL_NOTES].Width = 0;
                c1ViewTask.Cols[COL_USERID].Width = 0;
                c1ViewTask.Cols[COL_CLINICID].Width = 0;
                c1ViewTask.Cols[COL_PRIORITYLEVEL].Visible = false;
                c1ViewTask.Cols[COL_FROMID].Width = 0;
                c1ViewTask.Cols[COL_Resp].ImageAlign = ImageAlignEnum.CenterCenter;

                c1ViewTask.Cols[COL_ASSIGNEDTO].Width = Convert.ToInt32(nWidth * 0.08);

                // Retrives All Tasks
                //string struserids = Convert.ToString(UserId);
                ////oTasks = ogloTask.GetUserTasks(UserId , SearchString);
                oTasks = ogloTask.GetUserTasksNew(struserids, SearchString);

                if (oTasks != null && oTasks.Count > 0)
                {

                    //c1ViewTask.Redraw = false;
                    c1ViewTask.BeginUpdate();
                    for (int i = 0; i < oTasks.Count; i++)
                    {
                        c1ViewTask.Rows.Add();
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_TASKID, oTasks[i].TaskID);
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_PROVIDERID, oTasks[i].ProviderID);
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_PROVIDERNAME, oTasks[i].ProviderName);

                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_PATIENTID, oTasks[i].PatientID);
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_PATIENTNAME, oTasks[i].PatientName);

                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_SUBJECT, oTasks[i].Subject);

                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_STARTDATE, gloDateMaster.gloDate.DateAsDate(oTasks[i].StartDate));
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_DUEDATE, gloDateMaster.gloDate.DateAsDate(oTasks[i].DueDate));

                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_PRIORITYID, oTasks[i].PriorityID);
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_PRIORITYNAME, oTasks[i].Priority);


                        if (oTasks[i].PriorityLevel == 1)
                        {
                            c1ViewTask.SetCellImage(c1ViewTask.Rows.Count - 1, COL_PRIORITYICON, img6);
                            c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_PRIORITYICON, "1");
                        }
                        else if (oTasks[i].PriorityLevel == 2)
                        {
                        }
                        else if (oTasks[i].PriorityLevel == 3)
                        {
                            c1ViewTask.SetCellImage(c1ViewTask.Rows.Count - 1, COL_PRIORITYICON, img1);
                            c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_PRIORITYICON, "3");
                        }

                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_CATEGORYID, oTasks[i].CategoryID);
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_CATEGORYNAME, oTasks[i].Category);

                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_FOLLOWUPID, oTasks[i].FollowupID);
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_FOLLOWUPNAME, oTasks[i].Followup);
                        if (oTasks[i].Followup.ToUpper() == "TODAY")
                        {
                            c1ViewTask.SetCellImage(c1ViewTask.Rows.Count - 1, COL_FOLLOWUPICON, img2);
                            c1ViewTask.SetCellStyle(c1ViewTask.Rows.Count - 1, COL_FOLLOWUPICON, csTaskIcon);
                        }
                        else if (oTasks[i].Followup.ToUpper() == "Tomorrow")
                        {
                            c1ViewTask.SetCellImage(c1ViewTask.Rows.Count - 1, COL_FOLLOWUPICON, img3);
                            c1ViewTask.SetCellStyle(c1ViewTask.Rows.Count - 1, COL_FOLLOWUPICON, csTaskIcon);
                        }

                        //29-Jan-13 Aniket: Resolving Bug #62667 
                        else if (oTasks[i].Followup.ToUpper() == "NO DATE" || oTasks[i].Followup.ToUpper() == "")
                        {
                            c1ViewTask.SetCellImage(c1ViewTask.Rows.Count - 1, COL_FOLLOWUPICON, img4);
                            c1ViewTask.SetCellStyle(c1ViewTask.Rows.Count - 1, COL_FOLLOWUPICON, csTaskIcon);
                        }
                        else
                        {
                            c1ViewTask.SetCellImage(c1ViewTask.Rows.Count - 1, COL_FOLLOWUPICON, img5);
                            c1ViewTask.SetCellStyle(c1ViewTask.Rows.Count - 1, COL_FOLLOWUPICON, csTaskIcon);
                        }

                        if (oTasks[i].Resp.ToUpper().Trim() == "TASK_SINGLE")
                        {
                            c1ViewTask.SetCellImage(c1ViewTask.Rows.Count - 1, COL_Resp, img_Single);
                        }
                        if (oTasks[i].Resp.ToUpper().Trim() == "TASK_NOOWNER")
                        {
                            c1ViewTask.SetCellImage(c1ViewTask.Rows.Count - 1, COL_Resp, img_NoOwner);
                        }
                        if (oTasks[i].Resp.ToUpper().Trim() == "TASK_OWNER")
                        {
                            c1ViewTask.SetCellImage(c1ViewTask.Rows.Count - 1, COL_Resp, img_Owner);
                        }
                        if (oTasks[i].Resp.ToUpper().Trim() == "TASK_OTHERTAKEN")
                        {
                            c1ViewTask.SetCellImage(c1ViewTask.Rows.Count - 1, COL_Resp, img_OtherTaken);
                        }

                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_ISPRIVATE, oTasks[i].IsPrivate);

                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_OWNERID, oTasks[i].OwnerID);
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_OWNERNAME, oTasks[i].OwnerName);

                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_ASSIGNEDTOID, oTasks[i].Assignment[0].AssignToID);
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_ASSIGNEDTONAME, oTasks[i].Assignment[0].AssignToName);

                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_DESCRIPTION, oTasks[i].Progress.Description);
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_DATECOMPLETED, oTasks[i].Progress.Complete);

                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_STATUSID, oTasks[i].Progress.StatusID);
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_STATUSNAME, oTasks[i].Progress.StatusName);

                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_COMPLETE, oTasks[i].Progress.Complete);

                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_NOTES, oTasks[i].Notes);
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_USERID, oTasks[i].UserID);
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_CLINICID, oTasks[i].ClinicID);
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_PRIORITYLEVEL, oTasks[i].PriorityLevel);
                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_FROMID, oTasks[i].Assignment[0].AssignFromID);

                        c1ViewTask.SetData(c1ViewTask.Rows.Count - 1, COL_ASSIGNEDTO, oTasks[i].Assign_To);

                        if (oTasks[i].Progress.Complete == 100)
                        {
                            this.c1ViewTask.Rows[c1ViewTask.Rows.Count - 1].Style = csSubiect;
                        }
                        else if (oTasks[i].UserID != UserId)
                        {
                            this.c1ViewTask.Rows[c1ViewTask.Rows.Count - 1].Style = asgTask;
                            c1ViewTask.SetCellImage(i + 1, COL_TASKICON, img);
                            c1ViewTask.SetCellStyle(i + 1, COL_TASKICON, csTaskIcon);
                        }
                        //Bug #47913: Taks -- > Setting the Priority Wipes the Taks Details
                        if (TaskID == oTasks[i].TaskID)
                        {
                            RowNo = c1ViewTask.Rows.Count - 1;
                        }
                    }
                }

                c1ViewTask.Tree.Column = 10;
                c1ViewTask.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None;
                c1ViewTask.Tree.Indent = 20;
                c1ViewTask.Tree.LineColor = Color.Transparent;

                c1ViewTask.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Clear);


                // c1ViewTask.Redraw = true;

                //Bug #47913: Taks -- > Setting the Priority Wipes the Taks Details
                if (RowNo != 0)
                {
                    c1ViewTask.Select(RowNo, COL_SUBJECT);
                }
                else
                {
                    if (c1ViewTask.RowSel == 1)
                    {
                        c1ViewTask.Row = -1;
                    }
                    //if (c1ViewTask.RowSel > 1)
                    //{
                    //    c1ViewTask.Select(1, COL_SUBJECT);
                    //}
                    c1ViewTask.Select(1, COL_SUBJECT);
                }

                c1ViewTask.EndUpdate();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {

                this.Cursor = Cursors.Default;
                if (ogloTask != null) { ogloTask.Dispose(); }
                if (oTasks != null) { oTasks.Dispose(); }
            }

        }

        private void FillAll()
        {
            try
            {
                design_c1Task();
                fill_c1Task();

                FillFolowupMenu();
                FillPriorityMenu();

                gloTask ogloTask = new gloTask(_databaseconnnectionstring);
                lblHelloUser.Text = "";
                lblHelloUser.Text = "  " + ogloTask.GetUserLoginName(UserId);
                if (ogloTask != null) { ogloTask.Dispose(); }

                if (pnlTaskDetails.Visible == false)
                {
                    pnlTaskDetails.Visible = false;
                    btnUP.Visible = true;
                    btnDown.Visible = false;
                }
                else
                {
                    pnlTaskDetails.Visible = true;
                    btnUP.Visible = false;
                    btnDown.Visible = true;
                }

                btnUP.BackgroundImage = global::gloTaskMail.Properties.Resources.UP;
                btnUP.BackgroundImageLayout = ImageLayout.Center;

                lblShowSubject.Text = "";
                lblShowStatus.Text = "";
                lblShowStartDate.Text = "";
                lblShowProvider.Text = "";
                lblShowPriority.Text = "";
                lblShowPatient.Text = "";
                lblShowOwner.Text = "";
                lblShowFollowUp.Text = "";
                lblShowDueDate.Text = "";
                lblShowDescp.Text = "";
                lblShowComplete.Text = "";


                if (c1ViewTask.Rows.Count > 1)
                {
                    c1ViewTask.RowSel = 1;
                    c1ViewTask_SelChange(c1ViewTask, null);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        /// <summary>
        /// By Pradeep 20100720
        ///   //changes done for requirement of showing assigned task and his task together
        /// </summary>
        private void AcceptTask()
        {
            if (c1ViewTask.Rows.Count <= 1)
            {
                return;
            }
            if (DialogResult.No == MessageBox.Show("Accept selected task requests?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }//If yes then proceed.


            gloTask ogloTask = new gloTask(_databaseconnnectionstring);
            Int64 tempTaskId = 0;
            Task oTask = new Task();
            TaskAssign oTaskAssign = new TaskAssign();

            try
            {
                tempTaskId = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_TASKID));//TaskID
                oTask = ogloTask.GetTask(tempTaskId);
                if (oTask != null)
                {
                    //Clear the previous assignments for this Task
                    oTask.Assignment.Clear();

                    oTask.TaskID = 0;
                    oTask.UserID = UserId;
                    oTask.OwnerID = UserId;

                    oTaskAssign.TaskID = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_TASKID));//TaskID;
                    oTaskAssign.AssignToID = UserId;//AssignTo ID;
                    oTaskAssign.AssignFromID = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_FROMID));//AssignFrom  ID;
                    oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                    oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                    tsb_AcceptTask.Visible = false;
                    tsb_DeclineTask.Visible = false;
                    tsb_Delete.Visible = true;

                    oTask.Assignment.Add(oTaskAssign);

                }

                Int64 _result = ogloTask.Add(oTask);

                if (_result > 0)
                {
                    if (ogloTask.AcceptTask(tempTaskId, UserId))
                    {
                        //--------Saket 
                        //Activate Reminder for this Task if reminder is present 
                        gloReminder.Reminder oReminder = new gloReminder.Reminder();
                        oReminder.ActivateTaskReminder(UserId, tempTaskId, _result);
                        //-------------
                    }
                }

                else
                {
                    MessageBox.Show("ERROR : Record not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);
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

        //changes done by pradeep 20100720
        //changes done for requirement of showing assigned task and his task together

        private void DeclineTask()
        {
            //Added By MaheshB
            if (c1ViewTask.Rows.Count <= 1)
            {
                return;
            }
            if (DialogResult.No == MessageBox.Show("Decline selected task requests?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            gloTask ogloTask = new gloTask(_databaseconnnectionstring);
            Int64 tempTaskId = 0;

            try
            {
                //Traverse through the rows
                tempTaskId = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_TASKID));//TaskID
                //Decline Task Request if selected for decline
                if (!ogloTask.DeclineTask(tempTaskId, UserId))
                {
                    MessageBox.Show("Error : Decline unsuccessful.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                design_c1Task();
                fill_c1Task();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void ModifyTask()
        {
            //string formType="ViewTask";
            Int64 tempTaskId = 0;
            //Int64 tempAssignToId = 0;
            gloTask ogloTask = new gloTask(_databaseconnnectionstring);
            Task otempTask = new Task();
            try
            {


                if (c1ViewTask.Rows.Count <= 1)
                {
                    return;
                }



                tempTaskId = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_TASKID));
                gloTaskMail.frmTask ofrmTask = new gloTaskMail.frmTask(_databaseconnnectionstring, tempTaskId);
                ofrmTask.TaskAssigntoID = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_ASSIGNEDTOID));
                ofrmTask.PatientID = _SelPatientID;
                ofrmTask.OnTask_Change += new frmTask.OnTaskChange(ofrmTask_OnTask_Change);
                if (!_IsEMREnable)
                {
                    ofrmTask.OnPatientPaymentClicked += new frmTask.PatientPaymentHandler(oFrmModifyTaskClicked);
                }
                ofrmTask.IsEMREnable = true;
                ofrmTask.IsOpenfromView = true;
                ofrmTask.ShowDialog(this);

                //Bug #82464: 00000909: Task screen not opening respective screen
                ofrmTask_OnTask_Change(null, null, ofrmTask.e2Task);

                if (ofrmTask.IBPAckToken != 0)
                {
                    ogloTask.MarkComplete(tempTaskId);
                }
                ofrmTask.Dispose();

                fill_c1Task();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR :" + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {

                ogloTask.Dispose();
                otempTask.Dispose();
            }

        }

        void ofrmTask_OnTask_Change(object sender, EventArgs e, TaskChangeEventArg e2, object objfrmtsk = null)
        {
            if (OnViewTask_Change != null)
            {

                OnViewTask_Change(sender, e, e2, objfrmtsk);
            }
        }

        void oFrmModifyTaskClicked(object sender, EventArgs e, TaskChangeEventArg e2)
        {
            if (OnViewTaskModifiedClicked != null)
            { OnViewTaskModifiedClicked(sender, e, e2); }
        }

        private void OnTaskChangeEvent(object sender, EventArgs e)
        {
            TaskChangeEventArg e2 = new TaskChangeEventArg();

            e2.TaskID = 0;
            e2.Subject = "";
            e2.oTaskType = TaskType.None;
            e2.IsEMREnabled = _IsEMREnable;
            e2.IsUpdated = true;
            e2.IsTaskClose = true;
            //e2.IsOpenFromView = false;
            e2.IsOpenFromView = true;
            e2.FaxTiffFileName = "";
            e2.ReferenceID1 = 0;
            e2.ReferenceID2 = 0;

            if (OnViewTask_Change != null)
            {
                OnViewTask_Change(sender, e, e2);
            }
        }


        #endregion " Public & Private Methods "

        #region " C1 Grid Events "

        private void c1ViewTask_DoubleClick(object sender, EventArgs e)
        {
            GetControlSelection();

            ModifyTask();
            Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);
            SetControlSelection();

        }

        //private void c1ViewTask_Click(object sender, EventArgs e)
        //{
        //    //Added By MaheshB,to not show the details of Assigned task.


        //    string strname=((C1.Win.C1FlexGrid.C1FlexGrid)sender).Name;//Added By MaheshB.To disable delete Button.
        //    if (strname.ToString() == "c1AssignedTask" || strname.ToString() == "c1TaskRequest")
        //    {
        //        tsb_Delete.Enabled = false;
        //    }
        //    else 
        //    {
        //        tsb_Delete.Enabled = true;
        //    }
        //    if (strname.ToString() == "c1ViewTask" || strname.ToString() == "c1AssignedTask")
        //    {
        //        Int64 tempTaskId = 0;
        //        gloTask ogloTask = new gloTask(_databaseconnnectionstring);
        //        Task oTask = new Task();
        //        try
        //        {

        //            if (c1ViewTask.Focused && c1ViewTask.Rows.Count > 1)
        //                tempTaskId = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_TASKID));//TaskID

        //            else if (c1AssignedTask.Focused && c1AssignedTask.Rows.Count > 1)
        //                tempTaskId = Convert.ToInt64(c1AssignedTask.GetData(c1AssignedTask.RowSel, 0));//TaskID

        //            //else if (c1TaskRequest.Focused && c1TaskRequest.Rows.Count > 1)
        //            //    tempTaskId = Convert.ToInt64(c1TaskRequest.GetData(c1TaskRequest.RowSel, 0));//TaskID
        //            else
        //                return;


        //            oTask = ogloTask.GetTask(tempTaskId);
        //            if (oTask != null)
        //            {
        //                lblShowSubject.Text = oTask.Subject;
        //                lblShowStartDate.Text = gloDateMaster.gloDate.DateAsDate(oTask.StartDate).Date.ToString("MM-dd-yyyy");
        //                lblShowDueDate.Text = gloDateMaster.gloDate.DateAsDate(oTask.DueDate).ToString("MM-dd-yyyy");
        //                lblShowPriority.Text = oTask.Priority;

        //                lblShowStatus.Text = oTask.Progress.StatusName;
        //                lblShowComplete.Text = oTask.Progress.Complete.ToString();
        //                lblShowFollowUp.Text = oTask.Followup;

        //                if (oTask.ProviderName == "")
        //                {
        //                    lblShowProvider.Text = "N/A";

        //                }
        //                else
        //                {
        //                    lblShowProvider.Text = oTask.ProviderName;


        //                }

        //                if (oTask.PatientName == "")
        //                {
        //                    lblShowPatient.Text = "N/A";

        //                }
        //                else
        //                {
        //                    lblShowPatient.Text = oTask.PatientName;

        //                }

        //                lblShowDescp.Text = oTask.Progress.Description;
        //                lblShowOwner.Text = oTask.OwnerName;
        //             //   lblShowNotes.Text = oTask.Notes;
        //                TaskAssign oTaskAssign = new TaskAssign();
        //                oTaskAssign = ogloTask.GetTaskAssign(tempTaskId);
        //                Int32 reqstatus=Convert.ToInt32(oTaskAssign.AcceptRejectHold); 
        //                if(reqstatus==3)
        //                {
        //                    tsb_AcceptTask.Visible = true;
        //                    tsb_DeclineTask.Visible = true;
        //                    tsb_Delete.Visible = false;
        //                }
        //                else
        //                {
        //                    tsb_AcceptTask.Visible = false;
        //                    tsb_DeclineTask.Visible = false;
        //                    tsb_Delete.Visible = true;
        //                }


        //                //if (oTask.Assignment.Count > 0)
        //                //{
        //                //    for (int i = 0; i < oTask.Assignment.Count; i++)
        //                //    {
        //                //        lblShowAssignedTo.Text = oTask.Assignment[i].AssignToName ;
        //                //        lblShowAssginedFrom.Text = oTask.Assignment[i].AssignFromName; 

        //                //    }
        //                //}

        //            }


        //        }
        //        catch (gloDatabaseLayer.DBException dbEx)
        //        {
        //            dbEx.ERROR_Log(dbEx.ToString());
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //        finally
        //        {
        //            ogloTask.Dispose();
        //        }
        //    }
        //    else
        //    {

        //        lblShowSubject.Text = "";
        //        lblShowStartDate.Text = "";
        //        lblShowDueDate.Text = "";
        //        lblShowPriority.Text = "";

        //        lblShowStatus.Text = "";
        //        lblShowComplete.Text = "";
        //        lblShowFollowUp.Text = "";
        //        lblShowProvider.Text = "";
        //        lblShowPatient.Text = "";
        //        lblShowDescp.Text = "";
        //        lblShowOwner.Text = "";
        //        //lblShowNotes.Text = "";
        //    }

        //}

        #endregion " C1 Grid Events "

        #region " Designer Code "

        private void btnDecline_MouseHover(object sender, EventArgs e)
        {
            //btnDecline.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_ButtonHover;
            //btnDecline.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnDecline_MouseLeave(object sender, EventArgs e)
        {
            //btnDecline.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Button;
            //btnDecline.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnAccept_MouseHover(object sender, EventArgs e)
        {
            //btnAccept.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_ButtonHover;
            //btnAccept.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnAccept_MouseLeave(object sender, EventArgs e)
        {
            //btnAccept.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Button;
            //btnAccept.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnUP_MouseHover(object sender, EventArgs e)
        {
            btnUP.BackgroundImage = global::gloTaskMail.Properties.Resources.UPHover;
            btnUP.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnUP_MouseLeave(object sender, EventArgs e)
        {
            btnUP.BackgroundImage = global::gloTaskMail.Properties.Resources.UP;
            btnUP.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown_MouseHover(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloTaskMail.Properties.Resources.DownHover;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown_MouseLeave(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloTaskMail.Properties.Resources.Down;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        #endregion " Designer Code "

        #region " Form Controls Events "

        private void btnDown_Click(object sender, EventArgs e)
        {
            pnlTaskDetails.Visible = false;
            btnUP.Visible = true;
            btnDown.Visible = false;
            btnUP.BackgroundImage = global::gloTaskMail.Properties.Resources.UP;
            btnUP.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnUP_Click(object sender, EventArgs e)
        {
            pnlTaskDetails.Visible = true;
            btnUP.Visible = false;
            btnDown.Visible = true;
            btnDown.BackgroundImage = global::gloTaskMail.Properties.Resources.Down;
            btnDown.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnHideAssignTask_Click(object sender, EventArgs e)
        {
            //code commented for optimization
            //pnlGridAssign.Visible = false;
            //pnlSmallToolStrip.Visible = true;
            //btnShowAssignTask.Visible = true;
            //btnShowAssignTask.BackgroundImage = global::gloTaskMail.Properties.Resources.Forward;
            //btnShowAssignTask.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnShowAssignTask_Click(object sender, EventArgs e)
        {
            //code commented for optimization
            //pnlGridAssign.Visible = true;
            //pnlSmallToolStrip.Visible = false;


            //pnlTaskRequestHeader.Visible = true;
            //pnlRequestTask.Visible = true;

            //pnlAssigmedTaskHeader.Visible = true;
            //pnlc1AssignedTask.Visible = true;
            //pnlc1AssignedTask.Dock = DockStyle.Top;

        }

        private void btnHideAssignTask_MouseHover(object sender, EventArgs e)
        {
            //code commented for optimization
            //btnHideAssignTask.BackgroundImage = global::gloTaskMail.Properties.Resources.RewindHover;
            //btnHideAssignTask.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnHideAssignTask_MouseLeave(object sender, EventArgs e)
        {
            //code commented for optimization
            // btnHideAssignTask.BackgroundImage = global::gloTaskMail.Properties.Resources.Rewind;
            //btnHideAssignTask.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnShowAssignTask_MouseHover(object sender, EventArgs e)
        {
            //code commented for optimization
            //btnShowAssignTask.BackgroundImage = global::gloTaskMail.Properties.Resources.ForwardHover;
            //btnShowAssignTask.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnShowAssignTask_MouseLeave(object sender, EventArgs e)
        {
            //code commented for optimization
            //btnShowAssignTask.BackgroundImage = global::gloTaskMail.Properties.Resources.Forward;
            //btnShowAssignTask.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnHideTaskRequest_Click(object sender, EventArgs e)
        {
            //code commented for optimization
            //pnlGridAssign.Visible = false;
            //pnlSmallToolStrip.Visible = true;
            //btnShowAssignTask.Visible = true;
            //btnShowAssignTask.BackgroundImage = global::gloTaskMail.Properties.Resources.Forward;
            //btnShowAssignTask.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnHideTaskRequest_MouseHover(object sender, EventArgs e)
        {
            //code commented for optimization
            //btnHideTaskRequest.BackgroundImage = global::gloTaskMail.Properties.Resources.RewindHover;
            //btnHideTaskRequest.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnHideTaskRequest_MouseLeave(object sender, EventArgs e)
        {
            //code commented for optimization
            //btnHideTaskRequest.BackgroundImage = global::gloTaskMail.Properties.Resources.Rewind;
            //btnHideTaskRequest.BackgroundImageLayout = ImageLayout.Center;
        }

        //private void pnl_Grid_Resize(object sender, EventArgs e)
        //{
        //    ReIntializeTaskGrid();
        //}

        //private void pnl_Grid_SizeChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int nWidth;
        //        nWidth = pnl_Grid.Width;
        //        //nWidth = panel4.Width;


        //        c1ViewTask.Cols[COL_TASKID].Width = 0;

        //        c1ViewTask.Cols[COL_SELECT].Width = 0;//Convert.ToInt32(nWidth * 0.05);



        //        c1ViewTask.Cols[COL_PROVIDERID].Width = 0;
        //        c1ViewTask.Cols[COL_PROVIDERNAME].Width = 0;

        //        c1ViewTask.Cols[COL_PATIENTID].Width = 0;
        //        c1ViewTask.Cols[COL_PATIENTNAME].Width = 175;

        //        c1ViewTask.Cols[COL_SUBJECT].Width = Convert.ToInt32(nWidth * 0.45);

        //        c1ViewTask.Cols[COL_STARTDATE].Width = 0;
        //        c1ViewTask.Cols[COL_DUEDATE].Width = Convert.ToInt32(nWidth * 0.15);

        //        c1ViewTask.Cols[COL_STATUSID].Width = 0;
        //        c1ViewTask.Cols[COL_STATUSNAME].Width = Convert.ToInt32(nWidth * 0.15);

        //        c1ViewTask.Cols[COL_PRIORITYID].Width = 0;
        //        c1ViewTask.Cols[COL_PRIORITYNAME].Width = Convert.ToInt32(nWidth * 0.1);

        //        c1ViewTask.Cols[COL_COMPLETE].Width = Convert.ToInt32(nWidth * 0.15);
        //        c1ViewTask.Cols[COL_COMPLETE].TextAlign = TextAlignEnum.LeftCenter;

        //        c1ViewTask.Cols[COL_CATEGORYID].Width = 0;
        //        c1ViewTask.Cols[COL_CATEGORYNAME].Width = 0;

        //        c1ViewTask.Cols[COL_FOLLOWUPID].Width = 0;
        //        c1ViewTask.Cols[COL_FOLLOWUPNAME].Width = 0;

        //        c1ViewTask.Cols[COL_ISPRIVATE].Width = 0;

        //        c1ViewTask.Cols[COL_OWNERID].Width = 0;
        //        c1ViewTask.Cols[COL_OWNERNAME].Width = 0;

        //        c1ViewTask.Cols[COL_ASSIGNEDTOID].Width = 0;
        //        c1ViewTask.Cols[COL_ASSIGNEDTONAME].Width = 0;

        //        c1ViewTask.Cols[COL_DESCRIPTION].Width = 0;
        //        c1ViewTask.Cols[COL_DATECOMPLETED].Width = 0;
        //        c1ViewTask.Cols[COL_NOTES].Width = 0;
        //        c1ViewTask.Cols[COL_USERID].Width = 0;
        //        c1ViewTask.Cols[COL_CLINICID].Width = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }

        //}

        #endregion " Form Controls Events "

        #region " Context Menu Code "

        private void c1ViewTask_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Int64 tempTaskId = 0;
                gloTask ogloTask = new gloTask(_databaseconnnectionstring);
                TaskAssign oTaskAssign = new TaskAssign();
                String strTaskStatus;

                if (c1ViewTask.HitTest(e.X, e.Y).Row >= 1)
                {
                    Int32 tempRow = 0;

                    tempRow = c1ViewTask.HitTest(e.X, e.Y).Row;
                    c1ViewTask.Row = tempRow;
                    if (e.Button == MouseButtons.Right)
                    {
                        tempTaskId = Convert.ToInt64(c1ViewTask.GetData(tempRow, COL_TASKID));
                        if (tempTaskId > 0)
                        {

                            //10-Apr-13 Aniket: Fixing Bug 47899
                            strTaskStatus = Convert.ToString(c1ViewTask.GetData(tempRow, 12));

                            if (strTaskStatus == "Completed")
                            {
                                cmu_MarkCompleted.Visible = false;
                            }
                            else
                            {
                                cmu_MarkCompleted.Visible = true;
                            }

                            TaskID = tempTaskId;
                            oTaskAssign = ogloTask.GetTaskAssign(tempTaskId, UserId);
                            if (oTaskAssign != null)
                            {
                                Int32 reqstatus = Convert.ToInt32(oTaskAssign.AcceptRejectHold);
                                if (reqstatus == 3)
                                {
                                    c1ViewTask.ContextMenuStrip = null;
                                }
                                else
                                {
                                    c1ViewTask.ContextMenuStrip = cm_Task;
                                    FillFolowupMenu();
                                    FillPriorityMenu();
                                }
                            }

                        }
                        else
                        {
                            c1ViewTask.ContextMenuStrip = null;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillFolowupMenu()
        {
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnnectionstring);
            gloTasksMails.Common.Followups oFollowUps = new gloTasksMails.Common.Followups();
            System.Drawing.Image img = null;

            try
            {
                cmu_FollowUp.DropDownItems.Clear();

                oFollowUps = oTaskMail.GetFollowUps();

                if (oFollowUps != null && oFollowUps.Count > 0)
                {
                    for (int i = 0; i < oFollowUps.Count; i++)
                    {
                        ToolStripItem oFolloupSubMenuItem = new ToolStripMenuItem();
                        oFolloupSubMenuItem.Text = oFollowUps[i].Description;
                        oFolloupSubMenuItem.Name = oFollowUps[i].Description;
                        oFolloupSubMenuItem.ForeColor = Color.FromArgb(31, 73, 125);
                        oFolloupSubMenuItem.Font = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        oFolloupSubMenuItem.Tag = oFollowUps[i].ID;
                        if (oFollowUps[i].Description.ToUpper() == "TODAY")
                        {
                            img = global::gloTaskMail.Properties.Resources.Today;
                            oFolloupSubMenuItem.Image = img;
                        }
                        else if (oFollowUps[i].Description.ToUpper() == "Tomorrow")
                        {
                            img = global::gloTaskMail.Properties.Resources.Tommorow;
                            oFolloupSubMenuItem.Image = img;
                        }
                        else if (oFollowUps[i].Description.ToUpper() == "NO DATE")
                        {
                            img = global::gloTaskMail.Properties.Resources.No_Date;
                            oFolloupSubMenuItem.Image = img;
                        }
                        else
                        {
                            img = global::gloTaskMail.Properties.Resources.Flag_Yellow;
                            oFolloupSubMenuItem.Image = img;
                        }
                        oFolloupSubMenuItem.Click += new EventHandler(oFolloupSubMenuItem_Click);

                        cmu_FollowUp.DropDownItems.Add(oFolloupSubMenuItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oTaskMail != null) { oTaskMail.Dispose(); }
                if (oFollowUps != null) { oFollowUps.Dispose(); }
            }
        }

        private void FillPriorityMenu()
        {
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnnectionstring);
            gloTasksMails.Common.Priorities oPriorities = new gloTasksMails.Common.Priorities();
            System.Drawing.Image img = null;

            try
            {
                cmu_Priority.DropDownItems.Clear();
                oPriorities = oTaskMail.GetPriorities();
                if (oPriorities != null && oPriorities.Count > 0)
                {
                    for (int i = 0; i < oPriorities.Count; i++)
                    {
                        ToolStripItem oPrioritySubMenuItem = new ToolStripMenuItem();
                        oPrioritySubMenuItem.Text = oPriorities[i].Description;
                        oPrioritySubMenuItem.Name = oPriorities[i].Description;
                        oPrioritySubMenuItem.ForeColor = Color.FromArgb(31, 73, 125);
                        oPrioritySubMenuItem.Font = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        oPrioritySubMenuItem.Tag = oPriorities[i].ID;
                        if (oPriorities[i].PriorityLevel == 1)
                        {
                            img = global::gloTaskMail.Properties.Resources.High_PriorityRed;
                            oPrioritySubMenuItem.Image = img;
                            oPrioritySubMenuItem.ImageAlign = ContentAlignment.MiddleCenter;
                        }
                        else if (oPriorities[i].PriorityLevel == 2)
                        {

                        }
                        else if (oPriorities[i].PriorityLevel == 3)
                        {
                            img = global::gloTaskMail.Properties.Resources.Low_Priority;
                            oPrioritySubMenuItem.Image = img;
                            oPrioritySubMenuItem.ImageAlign = ContentAlignment.MiddleCenter;
                        }
                        oPrioritySubMenuItem.Click += new EventHandler(oPrioritySubMenuItem_Click);
                        cmu_Priority.DropDownItems.Add(oPrioritySubMenuItem);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {

            }
        }

        void oPrioritySubMenuItem_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnnectionstring);
            Int64 _priorityId = 0;
            Int64 _taskId = 0;
            string _sqlQuery = "";

            try
            {
                if (((ToolStripMenuItem)sender).Tag != null && ((ToolStripMenuItem)sender).Tag.ToString() != "")
                {
                    _priorityId = Convert.ToInt64(((ToolStripMenuItem)sender).Tag.ToString());
                    if (_priorityId > 0)
                    {
                        if (c1ViewTask != null && c1ViewTask.Rows.Count > 0)
                        {
                            if (c1ViewTask.RowSel > 0)
                            {
                                _taskId = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_TASKID));
                                if (_taskId > 0)
                                {
                                    oDB.Connect(false);
                                    _sqlQuery = "UPDATE TM_TaskMST SET nPriorityID = " + _priorityId + " WHERE nTaskID = " + _taskId + "";
                                    oDB.Execute_Query(_sqlQuery);
                                    oDB.Disconnect();
                                    Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);

                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        void oFolloupSubMenuItem_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnnectionstring);
            Int64 _followupId = 0;
            Int64 _taskId = 0;
            string _sqlQuery = "";

            try
            {
                if (((ToolStripMenuItem)sender).Tag != null && ((ToolStripMenuItem)sender).Tag.ToString() != "")
                {
                    _followupId = Convert.ToInt64(((ToolStripMenuItem)sender).Tag.ToString());
                    if (_followupId > 0)
                    {
                        if (c1ViewTask != null && c1ViewTask.Rows.Count > 0)
                        {
                            if (c1ViewTask.RowSel > 0)
                            {
                                _taskId = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_TASKID));
                                if (_taskId > 0)
                                {
                                    oDB.Connect(false);
                                    _sqlQuery = "UPDATE TM_TaskMST SET nFollowUpID = " + _followupId + " WHERE nTaskID = " + _taskId + " ";
                                    oDB.Execute_Query(_sqlQuery);
                                    oDB.Disconnect();
                                    Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private void cmu_NewTask_Click(object sender, EventArgs e)
        {
            tsb_ADD_Click(null, null);
        }
        //Open == Edit Task
        private void cmu_OpenTask_Click(object sender, EventArgs e)
        {
            c1ViewTask_DoubleClick(null, null);
        }

        private void cmu_MarkCompleted_Click(object sender, EventArgs e)
        {
            gloTask ogloTask = new gloTask(_databaseconnnectionstring);
            Int64 _taskId = 0;
            try
            {
                if (c1ViewTask != null && c1ViewTask.Rows.Count > 0)
                {
                    if (c1ViewTask.RowSel > 0)
                    {
                        _taskId = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_TASKID));
                        if (_taskId > 0)
                        {
                            ogloTask.ModifyTaskComplete(_taskId, 100);
                            Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            { }
        }

        private void cmu_Delete_Click(object sender, EventArgs e)
        {
            tsb_Delete_Click(null, null);
        }



        private void smn_Zero_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnnectionstring);
            string _sqlQuery = "";
            Int64 _complete = -1;
            Int64 _taskId = 0;
            Int64 _statusId = 0;

            try
            {
                if (((ToolStripMenuItem)sender).Tag != null && ((ToolStripMenuItem)sender).Tag.ToString() != "")
                {
                    _complete = Convert.ToInt64(((ToolStripMenuItem)sender).Tag.ToString());
                    if (_complete != -1)
                    {
                        if (c1ViewTask != null && c1ViewTask.Rows.Count > 0)
                        {
                            if (c1ViewTask.RowSel > 0)
                            {
                                _taskId = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_TASKID));
                                if (_taskId > 0)
                                {
                                    if (_complete == 0)
                                    {
                                        _statusId = Convert.ToInt64(gloTaskMail.frmTask.StatusType.NotStarted.GetHashCode());
                                    }
                                    else if (_complete > 0 && _complete < 100)
                                    {
                                        _statusId = Convert.ToInt64(gloTaskMail.frmTask.StatusType.InProgress.GetHashCode());
                                    }
                                    else if (_complete == 100)
                                    {
                                        _statusId = Convert.ToInt64(gloTaskMail.frmTask.StatusType.Completed.GetHashCode());
                                    }

                                    oDB.Connect(false);
                                    _sqlQuery = "UPDATE TM_Task_Progress SET dComplete = " + _complete + ", nStatusID = " + _statusId + " where nTaskID = " + _taskId + "";
                                    oDB.Execute_Query(_sqlQuery);
                                    design_c1Task();
                                    fill_c1Task();
                                    oDB.Disconnect();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        #endregion " Context Menu Code "

        private void c1AssignedTask_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1TaskRequest_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1ViewTask_BeforeSort(object sender, SortColEventArgs e)
        {

            if (e.Col == 1)
            {
                if ((SortFlags)e.Order == SortFlags.Ascending)
                {
                    c1ViewTask.Sort(SortFlags.Descending, COL_PRIORITYLEVEL);
                }
                else if ((SortFlags)e.Order == SortFlags.Descending)
                {
                    c1ViewTask.Sort(SortFlags.Ascending, COL_PRIORITYLEVEL);
                }
            }
            else if (e.Col == 30)
            {
                if ((SortFlags)e.Order == SortFlags.Ascending)
                {
                    c1ViewTask.Sort(SortFlags.Descending, COL_FOLLOWUPID);
                }
                else if ((SortFlags)e.Order == SortFlags.Descending)
                {
                    c1ViewTask.Sort(SortFlags.Ascending, COL_FOLLOWUPID);
                }
            }

        }



        private void c1ViewTask_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }


        private void tsb_TrackTask_Click(object sender, EventArgs e)
        {

            frmTrackTask ofrmTrackTask = new frmTrackTask();
            ofrmTrackTask.ShowDialog(this);
            ofrmTrackTask.Dispose();
            ofrmTrackTask = null;
        }

        private void c1ViewTask_SelChange(object sender, EventArgs e)
        {
            Int64 tempTaskId = 0;
            gloTask ogloTask = new gloTask(_databaseconnnectionstring);
            Task oTask = new Task();
            try
            {

                if (c1ViewTask.Rows.Count > 1)
                {
                    if (c1ViewTask.RowSel < 1)
                    {
                        return;
                    }
                    if (Convert.ToString(c1ViewTask.GetData(c1ViewTask.RowSel, COL_TASKID)) != "")
                    {
                        tempTaskId = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_TASKID));//TaskID
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

                if (Convert.ToString(c1ViewTask.GetData(c1ViewTask.RowSel, COL_TASKID)) != "")
                {
                    tempTaskId = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_TASKID));//TaskID
                }
                _SelPatientID = Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_PATIENTID));
                oTask = ogloTask.GetTask(tempTaskId);
                if (oTask != null)
                {
                    lblShowSubject.Text = oTask.Subject;
                    lblShowStartDate.Text = gloDateMaster.gloDate.DateAsDate(oTask.StartDate).Date.ToString("MM-dd-yyyy");
                    lblShowDueDate.Text = gloDateMaster.gloDate.DateAsDate(oTask.DueDate).ToString("MM-dd-yyyy");
                    lblShowPriority.Text = oTask.Priority;

                    lblShowStatus.Text = oTask.Progress.StatusName;
                    lblShowComplete.Text = oTask.Progress.Complete.ToString();
                    lblShowFollowUp.Text = oTask.Followup;

                    if (oTask.ProviderName == "")
                    {
                        lblShowProvider.Text = "N/A";

                    }
                    else
                    {
                        lblShowProvider.Text = oTask.ProviderName;


                    }

                    if (oTask.PatientName == "")
                    {
                        lblShowPatient.Text = "N/A";

                    }
                    else
                    {
                        lblShowPatient.Text = oTask.PatientName;

                    }

                    lblShowDescp.Text = oTask.Progress.Description;
                    lblShowOwner.Text = oTask.OwnerName;
                    //   lblShowNotes.Text = oTask.Notes;

                    TaskAssign oTaskAssign = new TaskAssign();
                   // oTaskAssign = ogloTask.GetTaskAssign(tempTaskId, Convert.ToInt64(c1ViewTask.GetData(c1ViewTask.RowSel, COL_ASSIGNEDTOID)));
                    oTaskAssign = ogloTask.GetTaskAssign(tempTaskId, UserId );
                    if (oTaskAssign != null)
                    {
                        Int32 reqstatus = Convert.ToInt32(oTaskAssign.AcceptRejectHold);
                        if (reqstatus == 3)
                        {
                            tsb_AcceptTask.Visible = true;
                            tsb_DeclineTask.Visible = true;
                            tsb_Delete.Visible = false;
                        }
                        else
                        {
                            tsb_AcceptTask.Visible = false;
                            tsb_DeclineTask.Visible = false;
                            tsb_Delete.Visible = true;
                        }
                    }
                    else
                    {
                        tsb_AcceptTask.Visible = false;
                        tsb_DeclineTask.Visible = false;
                        tsb_Delete.Visible = true;
                    }
                }
                else
                {

                    lblShowSubject.Text = "";
                    lblShowStartDate.Text = "";
                    lblShowDueDate.Text = "";
                    lblShowPriority.Text = "";

                    lblShowStatus.Text = "";
                    lblShowComplete.Text = "";
                    lblShowFollowUp.Text = "";
                    lblShowProvider.Text = "";
                    lblShowPatient.Text = "";
                    lblShowDescp.Text = "";
                    lblShowOwner.Text = "";
                }


            }
            catch (gloDatabaseLayer.DBException dbEx)
                {
                    dbEx.ERROR_Log(dbEx.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    
                    ogloTask.Dispose();
                }
        }

        void oTimer_Tick(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() != "")
            {
                // IF LAST KEY PRESS TIME DIFFERENCE IS 100 MILLISECONDS THEN SEARCHING WILL BE START //
                if (DateTime.Now.Subtract(_CurrentTime).Milliseconds > 100)
                {
                    oTimer.Stop();
                    Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);
                }
            }
            else
            {
                oTimer.Stop();
                Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (oTimer.Enabled == false)
                {
                    oTimer.Stop();
                    oTimer.Enabled = true;
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    return;
                }
                _CurrentTime = DateTime.Now;
                oTimer.Stop();
                oTimer.Interval = 700;
                oTimer.Enabled = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.ResetText();
            txtSearch.Focus();
        }

        private void frmViewTask_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveGotFocusListener(this);
        }
        private void oListUsers_ItemClosedClick(object sender, EventArgs e)
        {

            //added for bugid 98909   
            ts_Commands.Enabled = true;

            //panel6.Enabled = true;

        }
        Boolean bIncludeAllUsers = false;
        private void oListUsers_ItemSelectedClick(object sender, EventArgs e)
        {

            try
            {
                struserids = "";
                //cmb_To.Items.Clear(); 
                DataTable dtUsers = new DataTable();
                DataColumn dcId = new DataColumn("ID");
                DataColumn dcDescription = new DataColumn("Description");
                Int64[] UserIDs = new Int64[2];
                dtUsers.Columns.Add(dcId);
                dtUsers.Columns.Add(dcDescription);


                ToList = new gloGeneralItem.gloItems();
                gloGeneralItem.gloItem ToItem;
                Array.Resize(ref UserIDs, oListUsers.SelectedItems.Count);
                if (oListUsers.SelectedItems.Count > 0)
                {

                    for (Int16 i = 0; i <= oListUsers.SelectedItems.Count - 1; i++)
                    {
                        DataRow drTemp = dtUsers.NewRow();
                        drTemp["ID"] = oListUsers.SelectedItems[i].ID;
                        drTemp["Description"] = oListUsers.SelectedItems[i].Description;

                        dtUsers.Rows.Add(drTemp);
                        UserIDs[i] = Convert.ToInt64(drTemp["ID"]);
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

                //Int64 nDefaultUser = 0;
                //if (_defaultuserID > 0)
                //{
                //    nDefaultUser = _defaultuserID;
                //}
                //else
                //{
                //    nDefaultUser = _userID;

                //}
                //DataRow[] dr = dtUsers.Select("ID ='" + nDefaultUser + "'");
                //if (dr.Length > 0)
                //{
                //    DataRow newRow = dtUsers.NewRow();
                //    newRow.ItemArray = dr[0].ItemArray;
                //    dtUsers.Rows.Remove(dr[0]);
                //    dtUsers.Rows.InsertAt(newRow, 0);

                //}

                cmbToUsers.DataSource = dtUsers;
                cmbToUsers.ValueMember = dtUsers.Columns["ID"].ColumnName;
                cmbToUsers.DisplayMember = dtUsers.Columns["Description"].ColumnName;



                // oListUsers 
                oListUsers.IsgloCollectCustomer = false;
                bIncludeAllUsers = oListUsers.bchkIncludeAllUsers;

             


                for (int i = 0; i < UserIDs.Length; i++)
                {
                    struserids += UserIDs[i].ToString() + ",";
                }
                if (struserids.Length > 0)
                    struserids = struserids.Substring(0, struserids.Length - 1);


                Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                //added for bugid 98909   
                ts_Commands.Enabled = true;

                //panel6.Enabled = true;
            }

        }
        private string getSelectedUserIDs()
        {
            string userids = "";

            DataTable dtusers = (DataTable)cmbToUsers.DataSource;
            if (dtusers != null)
            {
                foreach (DataRow dr in dtusers.Rows)
                {
                    if ((Convert.ToString(dr["ID"])) != "-1")
                        userids += dr["ID"].ToString() + ",";
                }
            }
            if (userids.Length > 0)
            {
                userids = userids.Substring(0, userids.Length - 1);
            }
            return userids;
        }
        private void AdddefaultUser()
        {
            DataTable dtUsers = (DataTable)cmbToUsers.DataSource;
            if (dtUsers == null)
            {
                dtUsers = new DataTable();
                DataColumn dcId = new DataColumn("ID");
                DataColumn dcDescription = new DataColumn("Description");

                dtUsers.Columns.Add(dcId);
                dtUsers.Columns.Add(dcDescription);
            }
            DataRow dr = dtUsers.NewRow();
            dr["ID"] = UserId;
            dr["Description"] = userName;
            dtUsers.Rows.Add(dr);
            dtUsers.AcceptChanges();
            cmbToUsers.DataSource = dtUsers;
            cmbToUsers.DisplayMember = "Description";
            cmbToUsers.ValueMember = "ID";
            cmbToUsers.SelectedValue = UserId;
        }
        private void btnToDelete_Click(object sender, EventArgs e)
        {
            Int64 _deluserid = 0;
            try
            {
                //Remove item from ToList

                _deluserid = Convert.ToInt64(cmbToUsers.SelectedValue);
                //SLR: Changed on 4/4/2014
                for (int i = ToList.Count - 1; i >= 0; i--)
                {
                    if (ToList[i].ID == _deluserid)
                    {
                        ToList.RemoveAt(i);

                    }

                }

                //
                if (_deluserid > 0)
                {
                    DataTable dtUsers = (DataTable)cmbToUsers.DataSource;
                    dtUsers.Rows.RemoveAt(cmbToUsers.SelectedIndex);
                   
                    dtUsers.AcceptChanges();
                    cmbToUsers.DataSource = dtUsers;
                    cmbToUsers.Refresh();
                    //8060 Code Change to avoid exception
                    if (dtUsers.Rows.Count == 0)
                    {
                        AdddefaultUser();
                        //DataRow dr = dtUsers.NewRow();
                        //dr["ID"] = -1;
                        //dr["Description"] = "All";
                        //dtUsers.Rows.Add(dr);
                        //dtUsers.AcceptChanges();
                        //cmbToUsers.SelectedValue = -1;
                    }
                    if (dtUsers.Rows.Count > 0)
                    {
                        cmbToUsers.SelectedIndex = 0;
                        struserids = "";
                        foreach (DataRow dr in dtUsers.Rows)
                        {
                            if (struserids.Trim().Length > 0)
                            {
                                struserids = struserids + "," + dr[0].ToString();
                            }
                            else
                            {
                               
                                struserids = dr[0].ToString();
                            }
                        }

                    }

                    
                }
                else
                {
                    DataTable dtUsers = (DataTable)cmbToUsers.DataSource;
                    dtUsers.Rows.Clear();
                    AdddefaultUser();
                    //DataRow dr=  dtUsers.NewRow();
                    //dr["ID"] = -1;
                    //dr["Description"] = "All";
                    //dtUsers.Rows.Add(dr);  
                    //  dtUsers.AcceptChanges();
                    // // cmbToUsers.Items.Add("All");
                    // // cmbToUsers.SelectedValue = 0;
                    //  cmbToUsers.SelectedValue = -1;  
                    Fill_Users_All_Tasks(UserId.ToString(), ShowOtherUsersDropdown);
                   

                }





                if (ToList == null)
                {
                    ToList = new gloGeneralItem.gloItems();
                    ToList.Clear();
                }
                    DataTable dtUsers1 = new DataTable();
                    DataRow drTemp = dtUsers1.NewRow();
                   
                     ////ToList.Clear();

                    if (ToList.Count == 0)
                    {
                        gloGeneralItem.gloItem ToItem = new gloGeneralItem.gloItem();
                        DataColumn dcId = new DataColumn("ID");
                        DataColumn dcDescription = new DataColumn("Description");

                        dtUsers1.Columns.Add(dcId);
                        dtUsers1.Columns.Add(dcDescription);

                        drTemp["ID"] = UserId;

                        drTemp["Description"] = userName;
                        ToItem.ID = UserId;
                        ToItem.Description = userName;

                        ToList.Add(ToItem);
                        dtUsers1.Rows.Add(drTemp);

                        ToItem.Dispose();
                        ToItem = null;

                    }
               
               
                if (c1ViewTask.Rows.Count > 1)
                {
                    c1ViewTask.Focus();
                    c1ViewTask.Select(1, 0);
                }
                else
                {
                    c1ViewTask.Focus();
                }
                Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);
            }
                  
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }
        private gloListControl.gloListControl oListUsers;

        gloGeneralItem.gloItems ToList;

        private void btnToBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                ts_Commands.Enabled = false;
                //   panel6.Enabled = false;
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

                oListUsers = new gloListControl.gloListControl(_databaseconnnectionstring, gloListControl.gloListControlType.Users, true, this.Width);

                oListUsers.ControlHeader = "Users";
                oListUsers.IsgloCollectCustomer = true; // Sameer Added For User sorting based on non glocollect and glocollectusers 11/26/2014
                oListUsers.bchkIncludeAllUsers = false;
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

                oListUsers.tsb_UserGroups.Visible = false;
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
            //finally
            //{
            //    ts_Commands.Enabled = true;
            //}
        }
        //private void oListUsers_ItemSelectedClick1()
        //{
        //    try
        //    {
        //        struserids = "";
        //        //cmb_To.Items.Clear(); 
        //        DataTable dtUsers = new DataTable();
        //        DataColumn dcId = new DataColumn("ID");
        //        DataColumn dcDescription = new DataColumn("Description");
        //        Int64[] UserIDs = new Int64[2];
        //        dtUsers.Columns.Add(dcId);
        //        dtUsers.Columns.Add(dcDescription);


        //        ToList = new gloGeneralItem.gloItems();
        //        gloGeneralItem.gloItem ToItem;
        //        oListUsers = new gloListControl.gloListControl(_databaseconnnectionstring, gloListControl.gloListControlType.Users, true, this.Width);

        //        oListUsers.ControlHeader = "Users";
        //        oListUsers.IsgloCollectCustomer = true; // Sameer Added For User sorting based on non glocollect and glocollectusers 11/26/2014
        //        oListUsers.bchkIncludeAllUsers = false;
        //        Array.Resize(ref UserIDs, oListUsers.SelectedItems.Count);
        //        if (oListUsers.SelectedItems.Count > 0)
        //        {

        //            for (Int16 i = 0; i <= oListUsers.SelectedItems.Count - 1; i++)
        //            {
        //                DataRow drTemp = dtUsers.NewRow();
        //                drTemp["ID"] = oListUsers.SelectedItems[i].ID;
        //                drTemp["Description"] = oListUsers.SelectedItems[i].Description;

        //                dtUsers.Rows.Add(drTemp);
        //                UserIDs[i] = Convert.ToInt64(drTemp["ID"]);
        //                //
        //                ToItem = new gloGeneralItem.gloItem();

        //                ToItem.ID = oListUsers.SelectedItems[i].ID;
        //                ToItem.Description = oListUsers.SelectedItems[i].Description;


        //                ToList.Add(ToItem);
        //                ToItem.Dispose();
        //                ToItem = null;

        //                //
        //            }
        //        }




        //        dtUsers.DefaultView.Sort = "Description";
        //        dtUsers = dtUsers.DefaultView.ToTable();



        //        cmbToUsers.DataSource = dtUsers;
        //        cmbToUsers.ValueMember = dtUsers.Columns["ID"].ColumnName;
        //        cmbToUsers.DisplayMember = dtUsers.Columns["Description"].ColumnName;



        //        // oListUsers 
        //        oListUsers.IsgloCollectCustomer = false;
        //        bIncludeAllUsers = oListUsers.bchkIncludeAllUsers;




        //        for (int i = 0; i < UserIDs.Length; i++)
        //        {
        //            struserids += UserIDs[i].ToString() + ",";
        //        }
        //        if (struserids.Length > 0)
        //            struserids = struserids.Substring(0, struserids.Length - 1);


        //        Fill_Users_All_Tasks(struserids);
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        //    }
        //    finally
        //    {
        //        //added for bugid 98909   
        //        ts_Commands.Enabled = true;

        //        panel6.Enabled = true;
        //    }

        //}
        private void olist_Ids()
        {
            DataTable dtUsers = new DataTable();
            DataRow drTemp = dtUsers.NewRow();
            if (ToList == null)
                ToList = new gloGeneralItem.gloItems();
            ToList.Clear();


            gloGeneralItem.gloItem ToItem = new gloGeneralItem.gloItem();
            DataColumn dcId = new DataColumn("ID");
            DataColumn dcDescription = new DataColumn("Description");

            dtUsers.Columns.Add(dcId);
            dtUsers.Columns.Add(dcDescription);
            drTemp["ID"] = UserId;
            drTemp["Description"] = userName;
            ToItem.ID = Convert.ToInt64(drTemp["ID"]);
            ToItem.Description = Convert.ToString(drTemp["Description"]);


            ToList.Add(ToItem);
            dtUsers.Rows.Add(drTemp);


            ToItem.Dispose();
            ToItem = null;

            cmbToUsers.DataSource = dtUsers;
            if (dtUsers.Rows.Count >= 1)
                cmbToUsers.SelectedIndex = 0;
           
           
        }
        private void cmbToUsers_SelectedIndexChanged(object sender, EventArgs e)
        {


            cmbToUsers.SelectedIndexChanged -= cmbToUsers_SelectedIndexChanged;
            if (cmbToUsers.Text.Trim() != "")
            {
                Int64 cmbuserId = Convert.ToInt64(cmbToUsers.SelectedValue);
                if (cmbuserId <= 0)
                {
                    //if( oListUsers.tsb_UserGroups.Selected)
                    // {

                    // }

                    Fill_Users_All_Tasks(struserids, ShowOtherUsersDropdown);
                }
                //else
                //{
                //    FilterBySelectedUsers(getSelectedUserIDs() );
                //}
            }
            cmbToUsers.SelectedIndexChanged += cmbToUsers_SelectedIndexChanged;
        }


    



    }
}
