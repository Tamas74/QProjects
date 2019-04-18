using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;



namespace gloSecurity
{
    public partial class frmBackUp : Form
    {


        private string _databaseconnectionstring = "";
        //public string _MessageBoxCaption = "gloPMS";

        private string _MessageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        #region Constructor


        public frmBackUp(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;

            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        public frmBackUp()
        {
            InitializeComponent();

            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion

        }


        #endregion

       
        //Taking the location of BackUp file storage.
        private void btnBrowse_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    folderBrowserDialog1.Description = "Select Backup folder location";
            //    folderBrowserDialog1.ShowNewFolderButton = true;
            //    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            //    {

            //        txtLocation.Text = folderBrowserDialog1.SelectedPath;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error Locating file", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            //}
            //finally { }


        }//end btnBrowse_Click


        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            //switch (e.ClickedItem.Tag.ToString())
            //{
            //    case "OK":

            //        //Validate the location.
            //        if (ValidateData())
            //        {
            //            clsBackUp oBU = new clsBackUp(_databaseconnectionstring);
            //            if (!oBU.chkSettings())
            //            {
            //                lblStatus.Visible = true;
            //                lblStatus.Text = "Error Connecting to Server....try again.";
            //                lblStatus.ForeColor = Color.Red;
            //                return;
            //            }       
            //            //Check if the backup is immediate or schedule.
            //            if (optSchedule.Checked == true)
            //            {                                                         
            //                ScheduleInfo oScheduleInfo = createSchedule();
            //                string  sFileName = txtBackUpFileName.Text.Trim()+".bak";
            //                string  sFilePath = txtLocation.Text.Trim() + "\\" + sFileName;
            //                if (oBU.CreateJob_Sql(oScheduleInfo, sFilePath))
            //                {
            //                    lblStatus.Visible = true;
            //                    lblStatus.Text = "BackUp Operation Scheduled Successfully.....";
            //                    lblStatus.ForeColor = Color.Green;
            //                }

            //            }
            //            else
            //            {
            //                if (oBU.dbBackUp(txtBackUpFileName.Text.Trim(),txtLocation.Text.Trim()))
            //                {
            //                    //MessageBox.Show("BackUp Operation Successfully completed.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    lblStatus.Text = "Backup Operation Successfully completed.......";
            //                    lblStatus.ForeColor = Color.Green;
            //                    lblStatus.Visible = true;
            //                    txtBackUpFileName.Clear();
            //                    txtLocation.Clear();

            //                }
            //                else
            //                {
            //                    lblStatus.Visible = true;
            //                    MessageBox.Show("BackUp Operation failed OR FileName already exists.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }
            //            }

            //        }
                    
            //        break;


            //    case "Cancel":
                    
            //        this.Close();
            //        break;

            //}

        }


        /// <summary>
        /// This function create the schedule object,containing scheduling information.
        /// </summary>
        /// <returns></returns>
        private ScheduleInfo createSchedule()
        {
            //ScheduleInfo oSchedule = new ScheduleInfo();
            //oSchedule.ScheduleName = txtScheduleName.Text.Trim();
            //oSchedule.StartDate =Convert.ToInt32(DTPStartDate.Text);
            //if (rdBtnNoEndDate.Checked == true)
            //{
            //    //If no end date is specified we set it to max value.
            //    oSchedule.EndDate = 99991231;
            //}
            //else
            //{
            //    oSchedule.EndDate = Convert.ToInt32(DTPEndDate.Text);
            //}
            //oSchedule.ScheduleTime =Convert.ToInt32(tmScheduleTime.Text);
            
            ////oSchedule.ScheduleActiveEndTime = Convert.ToString(DTPActiveEndTime.Value);
            //if (rdBtnWeekly.Checked == true)
            //{
            //    //Get the list of selected days in CSV(Comma Seperated Values) format.
            //    foreach (Control ctr in pnlWeekly.Controls)
            //    {
            //        if (((CheckBox)ctr).Checked)
            //        {
            //            oSchedule.WeekDays += "," + Convert.ToString(((CheckBox)ctr).Tag);
            //        }                
            //    }
            //    //remove first " , " comma from the string.
            //    if (oSchedule.WeekDays.Substring(0, 1) == ",")
            //        oSchedule.WeekDays = oSchedule.WeekDays.Substring(1, oSchedule.WeekDays.Length - 1);

            //    oSchedule.WeekFrequency =Convert.ToInt32(numWeekFreq.Value);
            //    oSchedule.ScheduleFrequency = "Weekly";
            //}
            ////Check if the schedule is daily,weekly or monthly
            //if (rdbtnDaily.Checked)
            //{
            //    oSchedule.ScheduleFrequency = "Daily";
            //    oSchedule.DailyFrequecy = Convert.ToInt32(numWeekFreq.Value);
            //}

            //if (rdBtnMonthly.Checked == true)
            //{ 
            //    oSchedule.DayOfMonth =Convert.ToInt32(numDayOfMonth.Value);
            //    oSchedule.MonthFrequency =Convert.ToInt32(numMonth.Value);
            //    oSchedule.ScheduleFrequency = "Monthly";
            //}

            //if (optComplete.Checked == true)
            //{
            //    oSchedule.BackUpType = "COMPLETE";
            //}
            //else if (optDifferential.Checked == true)
            //{
            //    oSchedule.BackUpType = "DIFFRENTIAL";
            //}

           
            //return oSchedule;
            return null;

        }


       //Here we check whether the given location exists or not.
        private bool ValidateData()
        {
            //if (txtLocation.Text.Trim() == "")
            //{
            //    //MessageBox.Show("Please Select the BackUp Location", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    lblStatus.Visible = true;
            //    lblStatus.Text = "Please Select the BackUp Location";
            //    lblStatus.ForeColor = Color.Blue;
            //    btnBrowse.Focus();
            //    btnBrowse.ForeColor = Color.Purple;
            //    return false;
            //}
            //if (txtBackUpFileName.Text.Trim() == "")
            //{
            //    //MessageBox.Show("Please enter the file name for backup file.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    lblStatus.Visible = true;
            //    lblStatus.Text = "Please enter the file name for backup file.";
            //    lblStatus.ForeColor = Color.Blue;
            //    txtBackUpFileName.Focus();
            //    return false;
            //}
            //if (!Directory.Exists(txtLocation.Text.Trim()))
            //{
            //    MessageBox.Show("Directory Not Found", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtLocation.Focus();
            //    return false;
            //}
            //if (optSchedule.Checked == true)
            //{
            //    if (txtScheduleName.Text == "")
            //    {
            //        lblStatus.Visible = true;
            //        lblStatus.Text = "Please enter the Schedule Name.";
            //        txtScheduleName.Focus();
            //        return false;
            //    }
            //    if (DTPEndDate.Value <= DTPStartDate.Value)
            //    {

            //        MessageBox.Show("End Date should be greater than the start date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        DTPEndDate.Focus();
            //        return false;

            //    }


            //}
            
            return true;

        }


        private void optNow_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void optSchedule_CheckedChanged(object sender, EventArgs e)
        {
            //if (optSchedule.Checked == true)
            //{
            //    pnlScheduling.Visible = true;
            //    this.Size = new Size(559, 540);
            //}
            //else
            //{
            //    pnlScheduling.Visible = false;
            //    this.Size = new Size(559, 255);
            //}
        }


        private void frmBackUp_Load(object sender, EventArgs e)
        {
            this.Size = new Size(559, 255);
        }


        private void rdBtnWeekly_CheckedChanged(object sender, EventArgs e)
        {            
            //if (((RadioButton)sender).Name == "rdbtnDaily")
            //{
            //    numWeekFreq.Visible = true;
            //    lblDaily.Visible = true;
            //    lblDailyEvery.Visible = true;
            //    pnlMonthly.Visible = false;
            //    pnlWeekly.Visible = false;
            //}
            //if (((RadioButton)sender).Name == "rdBtnWeekly")
            //{
            //    lblDaily.Visible = false;
            //    lblDailyEvery.Visible = false;
            //    numWeekFreq.Visible = true;
            //    pnlWeekly.Visible = true;
            //    pnlWeekly.BringToFront();
            //    numWeekFreq.Visible = true; 
            //    pnlMonthly.Visible = false;
            //}
            //if (((RadioButton)sender).Name == "rdBtnMonthly")
            //{
            //    lblDaily.Visible = false;
            //    lblDailyEvery.Visible = false;
            //    numWeekFreq.Visible = false;
            //    pnlMonthly.Visible = true;
            //    pnlMonthly.BringToFront();
            //    pnlWeekly.Visible = false;
            //}

            //if (((RadioButton)sender).Name == "rdBtnNoEndDate")
            //{
            //    DTPEndDate.Enabled = false;
            
            //}
            //if (((RadioButton)sender).Name == "rdBtnDateEnd")
            //{
            //    DTPEndDate.Enabled = true;
            //}



        }

               
    }//end class frmBackUP

}//end namespace gloSecurity