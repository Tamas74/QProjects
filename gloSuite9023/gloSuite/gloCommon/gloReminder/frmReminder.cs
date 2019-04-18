using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;


namespace gloReminder
{
    public partial class frmReminder : Form
    {
        private string _databaseconnectionstring = "";
        //private string _messageBoxCaption = "gloPM";
        private string _messageBoxCaption = String.Empty;

        private DataTable _dtReminders = new DataTable();
        private Reminders _reminders = new Reminders();
      //  private Int64 _nTaskID = 0;
        public int ReminderCount=0;
        public int _MaxMin=720;
        public const int _MinMin=1;
        public const int _MaxHours=12;
        public const int _MinHours=1;
        public const int _MaxDay=4;
        public const int _MinDay=1;

        // SUDHIR 20091222 // CUSTOM EVENT TO OPEN TASK, THIS EVENT WILL TRIGER METHOD OF gloReminder.gloReminder //
        public delegate void OnOpenItemClick(object sender, EventArgs e, gloReminder.OpenItemClickArgs e2);
        public event OnOpenItemClick On_OpenItemClick;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public Reminders Reminders
        {
            get { return _reminders; }
            set { _reminders = value; }
        }

        private frmReminder()
        {
            InitializeComponent();
            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
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
            //_dtReminders = dtReminders;
        }

        private void frmReminder_Load(object sender, EventArgs e)
        {
            DesignListView();
            //FillReminders();
        }

        public void FillReminders()
        {
            try
            {
                if (_reminders.Count > 0)
                {
                    Reminder oReminder = new Reminder();
                    for (int i = 0; i < _reminders.Count; i++)
                    {
                        ListViewItem oListItem = new ListViewItem();                
                     
                        oListItem.Text = "";

                        //ReminderID -- 1
                        oListItem.SubItems.Add(_reminders[i].ReminderID.ToString());
                        //ReminderDetailID -- 2
                        oListItem.SubItems.Add(_reminders[i].ReminderDetailID.ToString());                        
                        //Description -- 3
                        oListItem.SubItems.Add(_reminders[i].Description);
                        //Start date -- 4
                        oListItem.SubItems.Add(_reminders[i].ReminderStartDate.ToShortDateString());
                        //Start Time -- 5
                        oListItem.SubItems.Add(_reminders[i].ReminderStartTime.ToShortTimeString());
                        //Place -- 6
                        oListItem.SubItems.Add(_reminders[i].Place);

                        //Due Time -- 7
                        if (_reminders[i].ReferenceType != ReferenceType.Task)
                        {
                            TimeSpan DueIn = DateTime.Now.Subtract(_reminders[i].ReminderStartTime);

                            Int32 hours = Convert.ToInt32(DueIn.TotalMinutes / 60);
                            Int32 Minutes = Convert.ToInt32(DueIn.TotalMinutes % 60);

                            if (DueIn.TotalMinutes < 0)
                                oListItem.SubItems.Add(Convert.ToInt32(DueIn.TotalMinutes) + " Min");
                            else
                            {
                                if (hours > 0)
                                    oListItem.SubItems.Add(hours + " Hours " + Minutes + " Min Overdue");
                                else
                                    oListItem.SubItems.Add(Minutes + " Min Overdue");
                            }
                        }
                        else //if Reminder is for Task  do not show Due Time
                        {
                            oListItem.SubItems.Add(" ");
                        }

                        //Reference Type -- 8
                        oListItem.SubItems.Add(_reminders[i].ReferenceType.GetHashCode().ToString());
                        oListItem.SubItems.Add(_reminders[i].ReferanceID.ToString());

                        lv_Reminders.Items.Add(oListItem);
                        //lv_Reminders.Items[i].ImageIndex = 0;

                        if (i == 0)
                        {
                            lv_Reminders.Items[0].Selected = true;
                            lblDescription.Text = _reminders[i].Description;
                            lblLocation.Text = _reminders[i].Place;
                            //show due date in place of start date
                            //lblStartDate.Text = _reminders[i].ReminderStartDate.ToShortDateString();
                            lblStartDate.Text = _reminders[i].ReminderEndDate.ToShortDateString();
                            if (_reminders[i].ReferenceType != ReferenceType.Task)
                                lblStrartTime.Text = _reminders[i].ReminderStartTime.ToShortTimeString();
                            picBoxReminder.Image = imgListReminder.Images[0];


                        }
                        for (int Count = 0; Count < lv_Reminders.Items.Count; Count++)
                        {
                            lv_Reminders.Items[Count].ImageIndex = 0;
                        }

                            oListItem = null;

                        oReminder.MarkAsFinished(_reminders[i].ReminderID, _reminders[i].ReminderDetailID);
                    }                   
                }
                Application.DoEvents();  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public void FillReminders_Old()
        {
            try
            {
                if (_reminders.Count > 0)
                {
                    ReminderCount = _reminders.Count;
                    DesignListView();
                    lv_Reminders.Items.Clear();
                    Reminder oReminder = new Reminder();
                    for (int i = 0; i < _reminders.Count; i++)
                    {
                        ListViewItem oListItem = new ListViewItem();

                        oListItem.Text = "";

                        //ReminderID -- 1
                        oListItem.SubItems.Add(_reminders[i].ReminderID.ToString());
                        //ReminderDetailID -- 2
                        oListItem.SubItems.Add(_reminders[i].ReminderDetailID.ToString());
                        //Description -- 3
                        oListItem.SubItems.Add(_reminders[i].Description);
                        //Start date -- 4
                        oListItem.SubItems.Add(_reminders[i].ReminderStartDate.ToShortDateString());
                        //Start Time -- 5
                        oListItem.SubItems.Add(_reminders[i].ReminderStartTime.ToShortTimeString());
                        //Place -- 6
                        oListItem.SubItems.Add(_reminders[i].Place);

                        //Due Time -- 7
                        if (_reminders[i].ReferenceType != ReferenceType.Task)
                        {
                            TimeSpan DueIn = DateTime.Now.Subtract(_reminders[i].ReminderStartTime);

                            Int32 hours = Convert.ToInt32(DueIn.TotalMinutes / 60);
                            Int32 Minutes = Convert.ToInt32(DueIn.TotalMinutes % 60);

                            if (DueIn.TotalMinutes < 0)
                                oListItem.SubItems.Add(Convert.ToInt32(DueIn.TotalMinutes) + " Min");
                            else
                            {
                                if (hours > 0)
                                    oListItem.SubItems.Add(hours + " Hours " + Minutes + " Min Overdue");
                                else
                                    oListItem.SubItems.Add(Minutes + " Min Overdue");
                            }
                        }
                        else //if Reminder is for Task  do not show Due Time
                        {
                            oListItem.SubItems.Add(" ");
                        }

                        //Reference Type -- 8
                        oListItem.SubItems.Add(_reminders[i].ReferenceType.GetHashCode().ToString());
                        oListItem.SubItems.Add(_reminders[i].ReferanceID.ToString());

                        lv_Reminders.Items.Add(oListItem);
                        lv_Reminders.Items[i].ImageIndex = 0;

                        if (i == 0)
                        {
                            lv_Reminders.Items[0].Selected = true;
                            lblDescription.Text = _reminders[i].Description;
                            lblLocation.Text = _reminders[i].Place;
                            //show due date in place of start date
                            //lblStartDate.Text = _reminders[i].ReminderStartDate.ToShortDateString();
                            lblStartDate.Text = _reminders[i].ReminderEndDate.ToShortDateString();
                            if (_reminders[i].ReferenceType != ReferenceType.Task)
                                lblStrartTime.Text = _reminders[i].ReminderStartTime.ToShortTimeString();
                            picBoxReminder.Image = imgListReminder.Images[0];


                        }

                        oListItem = null;

                        //oReminder.MarkAsFinished(_reminders[i].ReminderID, _reminders[i].ReminderDetailID);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region Dismiss reminder

        //dismiss selected reminder 
        private void btnDismiss_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < lv_Reminders.Items.Count; i++)
                {
                    if (lv_Reminders.Items[i].Selected == true)
                    {
                        Reminder oReminder = new Reminder();
                        Int64 ReminderID = Convert.ToInt64(lv_Reminders.Items[i].SubItems[1].Text);
                        oReminder.DismissReminder(ReminderID);
                        lv_Reminders.Items[i].Remove();
                        break;
                    }
                }
                if (lv_Reminders.Items.Count == 0)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //dismiss all reminders
        private void btnDismissAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < lv_Reminders.Items.Count; i++)
                {
                    Reminder oReminder = new Reminder();
                    Int64 ReminderID = Convert.ToInt64(lv_Reminders.Items[i].SubItems[1].Text);
                    oReminder.DismissReminder(ReminderID);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        } 

        #endregion
       
        #region Snooze
        
        private void btnSnooze_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSnoozeTime.SelectedIndex == -1)
                {
                    //return;
                }
                Int32 SnoozeTime = 0;
                //switch (cmbSnoozeTime.SelectedItem.ToString())
                //{
                //    case "5 Minutes":
                //        SnoozeTime = 5;
                //        break;
                //    case "10 Minutes":
                //        SnoozeTime = 10;
                //        break;
                //    case "15 Minutes":
                //        SnoozeTime = 15;
                //        break;
                //    case "30 Minutes":
                //        SnoozeTime = 30;
                //        break;
                //    default:
                //        SnoozeTime = 5;
                //        break;
                //}
                //Added By MaheshB
                string strsnoozetime = cmbSnoozeTime.Text.ToString();
                string []arrsnooze=null;
                double Num;
                char[] splitchar = {' '};
                arrsnooze = strsnoozetime.Split(splitchar);
                if(arrsnooze.Length == 2) //Checks Only two entries For e.g.:- 2 Minutes.
                {
                    bool isNum = double.TryParse(arrsnooze[0], out Num);
                    if (isNum==true)
                    {
                        SnoozeTime= CalculateSnoozeTime(arrsnooze);
                        //SnoozeTime = Convert.ToInt32(arrsnooze[0]);
                        if (SnoozeTime > 0)
                        {
                            for (int i = 0; i < lv_Reminders.Items.Count; i++)
                            {
                                if (lv_Reminders.Items[i].Selected == true)
                                {
                                    Reminder oReminder = new Reminder();
                                    Int64 ReminderID = Convert.ToInt64(lv_Reminders.Items[i].SubItems[1].Text);
                                    Int64 ReminderDetailID = Convert.ToInt64(lv_Reminders.Items[i].SubItems[2].Text);
                                    oReminder.SnoozeReminder(ReminderID, ReminderDetailID, SnoozeTime);
                                    lv_Reminders.Items[i].Remove();
                                    break;
                                }
                            }
                            if (lv_Reminders.Items.Count == 0)
                            {
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid snooze time. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid snooze time. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid snooze time. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int CalculateSnoozeTime(string []snoozetime)
        {
            try
            {
                int Time = Convert.ToInt32(snoozetime[0]);
                if (snoozetime[1].ToString().ToUpper() == "MINUTES" || snoozetime[1].ToString().ToUpper() == "MINUTE")
                {
                    if (Convert.ToInt32(snoozetime[0]) < _MinMin || Convert.ToInt32(snoozetime[0]) > _MaxMin)
                    {
                        return 0;
                    }
                    else
                    {
                        return Time;
                    }
                }

                else if (snoozetime[1].ToString().ToUpper() == "HOURS" || snoozetime[1].ToString().ToUpper() == "HOUR")
                {
                    if (Convert.ToInt32(snoozetime[0]) < _MinHours || Convert.ToInt32(snoozetime[0]) > _MaxHours)
                    {
                        return 0;
                    }
                    else
                    {
                        return (Time * 60);
                    }
                }
                else if (snoozetime[1].ToString().ToUpper() == "DAYS" || snoozetime[1].ToString().ToUpper() == "DAY")
                {
                    if (Convert.ToInt32(snoozetime[0]) < _MinDay || Convert.ToInt32(snoozetime[0]) > _MaxDay)
                    {
                        return 0;
                    }
                    else
                    {
                        return (Time * 24 * 60);
                    }
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
                return 0; 
            }
            return 0;
        }

        //Snooze reminder for 5 Min by default if reminder in closed 
        private void frmReminder_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Int32 SnoozeTime = 5;

                for (int i = lv_Reminders.Items.Count - 1; i >= 0; i--)
                {
                    Reminder oReminder = new Reminder();
                    Int64 ReminderID = Convert.ToInt64(lv_Reminders.Items[i].SubItems[1].Text);
                    Int64 ReminderDetailID = Convert.ToInt64(lv_Reminders.Items[i].SubItems[2].Text);
                    oReminder.SnoozeReminder(ReminderID, ReminderDetailID, SnoozeTime);
                    lv_Reminders.Items[i].Remove();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        private void lv_Reminders_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < lv_Reminders.Items.Count; i++)
                {
                    if (lv_Reminders.Items[i].Selected == true)
                    {
                        lblDescription.Text = lv_Reminders.Items[i].SubItems[3].Text;                       
                        lblStartDate.Text = lv_Reminders.Items[i].SubItems[4].Text;
                        if ((ReferenceType)Convert.ToInt64(lv_Reminders.Items[i].SubItems[8].Text) != ReferenceType.Task)
                        {
                            lblStrartTime.Text = lv_Reminders.Items[i].SubItems[5].Text;
                            lblLocation.Text = lv_Reminders.Items[i].SubItems[6].Text;
                        }
                        else
                        {
                            lblStrartTime.Text = "";
                            lblLocation.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void DesignListView()
        {
            try
            {
                lv_Reminders.Columns.Clear();

                //nReminderID, nReminderDetailID,sDescription, dtReminderStartDate,dtReminderStartTime,dtReminderInterval,sPlace 
                lv_Reminders.Columns.Add(" ");
                lv_Reminders.Columns.Add("ReminderID");
                lv_Reminders.Columns.Add("ReminderDetailID");
               // lv_Reminders.Columns.Add("Description");
                lv_Reminders.Columns.Add("Subject");
                lv_Reminders.Columns.Add("ReminderStartDate");
                lv_Reminders.Columns.Add("ReminderStartTime");
                lv_Reminders.Columns.Add("Location");
                lv_Reminders.Columns.Add("Due In");
                lv_Reminders.Columns.Add("ReferenceType");
                lv_Reminders.Columns.Add("ReferenceID");

                Int32 Width = pnlReminders.Width - 5;


                lv_Reminders.Columns[0].Width = Convert.ToInt32(Width * 0.06);
                lv_Reminders.Columns[1].Width = 0;
                lv_Reminders.Columns[2].Width = 0;
                lv_Reminders.Columns[3].Width = Convert.ToInt32(Width * 0.6);
                lv_Reminders.Columns[4].Width = 0;
                lv_Reminders.Columns[5].Width = 0;
                lv_Reminders.Columns[6].Width = 0;
                lv_Reminders.Columns[7].Width = Convert.ToInt32(Width * 0.34);
                lv_Reminders.Columns[8].Width = 0;
                lv_Reminders.Columns[9].Width = 0;

                lv_Reminders.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloReminder.Properties.Resources.Img_Yellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloReminder.Properties.Resources.Img_Button;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }
        //Open the task  reminded
        private void btnOpen_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (lv_Reminders.Items.Count != 0)
            //    {
            //        for (int i = 0; i < lv_Reminders.Items.Count; i++)
            //        {
            //            if (lv_Reminders.Items[i].Selected == true)
            //            {
            //                _nTaskID = Convert.ToInt64(lv_Reminders.Items[i].SubItems[9].Text);
            //                gloTaskMail.frmTask ofrmTask = new gloTaskMail.frmTask(_databaseconnectionstring, _nTaskID);
            //                ofrmTask.ShowDialog();
            //                ofrmTask.Dispose();
            //                break;
            //            }
            //        }

            //    }
            //    if (lv_Reminders.Items.Count == 0)
            //    {
            //        this.Close();
            //    }


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            OnOpenItemClickEvent(sender, e);
        }
        //Developer:Pradeep/Date:02/22/2011/Bug ID/PRD Name/Salesforce Case: 21248/Reason:creating multiple task 
        public void UpdateReminderListView()
        {
            try
            {
                int index = this.lv_Reminders.SelectedIndices[0];
                this.lv_Reminders.Items.RemoveAt(index);
                lv_Reminders.Update();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        private void frmReminder_FormClosed(object sender, FormClosedEventArgs e)
        {
           this.Dispose();
        }

        private void OnOpenItemClickEvent(object sender, EventArgs e)
        {
            if (_messageBoxCaption == "gloPM")
            { return; }

            try
            {
                if (lv_Reminders.Items.Count != 0)
                {
                    gloReminder.OpenItemClickArgs e2;
                    for (int i = 0; i < lv_Reminders.Items.Count; i++)
                    {
                        if (lv_Reminders.Items[i].Selected == true)
                        {
                            e2 = new gloReminder.OpenItemClickArgs();
                            e2.TaskID = Convert.ToInt64(lv_Reminders.Items[i].SubItems[9].Text);
                            On_OpenItemClick(sender, e, e2);
                            //this.On_OpenItemClick -= new frmReminder.OnOpenItemClick(On_OpenItemClick);//Developer:Pradeep/Date:02/20/2012/Bug ID/PRD Name/Salesforce Case: 21253
                            break;
                        }
                    }
                }
                if (lv_Reminders.Items.Count == 0)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}