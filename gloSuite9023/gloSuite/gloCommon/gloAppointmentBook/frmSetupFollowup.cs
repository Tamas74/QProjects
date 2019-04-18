using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloAppointmentBook
{
    public partial class frmSetupFollowup : Form
    {
        #region " Contructors "

        public frmSetupFollowup(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        public frmSetupFollowup(string databaseconnectionstring, Int64 FollowUpID)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            _FollowUpID = FollowUpID;

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

        #endregion " Contructors "

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private Int64 _FollowUpID = 0;
        //private int _FollowUpType = 0;

        #endregion " Declarations "

        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "

        #region " Form Load "

        private void frmSetupFollowup_Load(object sender, EventArgs e)
        {
            
            try
            {
                FillFollowUps(_FollowUpID);
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion " Form Load "

        #region " Button Click Events "

        private void tlsp_btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 _tempresult = 0;
                gloAppointmentBook ogloAppointmentBook = new gloAppointmentBook(_databaseconnectionstring);
                if (ValidateData())
                {

                    //Modify
                    if (_FollowUpID > 0)
                    {
                        if (rdbDays.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(Convert.ToInt64(txtFollowupName.Tag), txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Day.GetHashCode(), _ClinicID);
                        }
                        else if (rdbMonth.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(Convert.ToInt64(txtFollowupName.Tag), txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Month.GetHashCode(), _ClinicID);
                        }
                        else if (rdbWeek.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(Convert.ToInt64(txtFollowupName.Tag), txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Week.GetHashCode(), _ClinicID);
                        }
                        else if (rdbYear.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(Convert.ToInt64(txtFollowupName.Tag), txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Year.GetHashCode(), _ClinicID);
                        }
                        if (_tempresult > 0)
                        {
                            ogloAppointmentBook.Dispose();
                        }
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.FollowUp, ActivityType.Add, "Add FollowUp", 0, _tempresult, 0, ActivityOutCome.Success);                

                    }
                    else
                    {
                        //Check if TOS already exists if Yes do not add
                        if ((ogloAppointmentBook.IsExistsFollowUp(Convert.ToInt64(txtFollowupName.Tag), txtFollowupName.Text.Trim())))
                        {
                            MessageBox.Show("Follow up already exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        //Add
                        if (rdbDays.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(0, txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Day.GetHashCode(), _ClinicID);
                        }
                        else if (rdbMonth.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(0, txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Month.GetHashCode(), _ClinicID);
                        }
                        else if (rdbWeek.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(0, txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Week.GetHashCode(), _ClinicID);
                        }
                        else if (rdbYear.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(0, txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Year.GetHashCode(), _ClinicID);
                        }
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.FollowUp, ActivityType.Add, "Add FollowUp", 0, _tempresult, 0, ActivityOutCome.Success);                

                        if (_tempresult > 0)
                        {
                            ogloAppointmentBook.Dispose();
                        }

                    }

                    this.Close();
                }   
            }
            catch (Exception ex)
            {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {

            try
            {
                Int64 _tempresult = 0;
                gloAppointmentBook ogloAppointmentBook = new gloAppointmentBook(_databaseconnectionstring);
                if (ValidateData())
                {

                    //Modify
                    if (_FollowUpID > 0)
                    {
                        if (rdbDays.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(Convert.ToInt64(txtFollowupName.Tag), txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Day.GetHashCode(), _ClinicID);
                        }
                        else if (rdbMonth.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(Convert.ToInt64(txtFollowupName.Tag), txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Month.GetHashCode(), _ClinicID);
                        }
                        else if (rdbWeek.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(Convert.ToInt64(txtFollowupName.Tag), txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Week.GetHashCode(), _ClinicID);
                        }
                        else if (rdbYear.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(Convert.ToInt64(txtFollowupName.Tag), txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Year.GetHashCode(), _ClinicID);
                        }
                        if (_tempresult > 0)
                        {
                            ogloAppointmentBook.Dispose();
                        }
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.FollowUp, ActivityType.Add, "Add FollowUp", 0, _tempresult, 0, ActivityOutCome.Success);

                    }
                    else
                    {
                        //Check if TOS already exists if Yes do not add
                        if ((ogloAppointmentBook.IsExistsFollowUp(Convert.ToInt64(txtFollowupName.Tag), txtFollowupName.Text.Trim())))
                        {
                            MessageBox.Show("Follow up already exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        //Add
                        if (rdbDays.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(0, txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Day.GetHashCode(), _ClinicID);
                        }
                        else if (rdbMonth.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(0, txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Month.GetHashCode(), _ClinicID);
                        }
                        else if (rdbWeek.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(0, txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Week.GetHashCode(), _ClinicID);
                        }
                        else if (rdbYear.Checked == true)
                        {
                            _tempresult = ogloAppointmentBook.AddModifyFollowUps(0, txtFollowupName.Text.Trim(), Convert.ToInt64(numDuration.Value), FollowUpType.Year.GetHashCode(), _ClinicID);
                        }
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.FollowUp, ActivityType.Add, "Add FollowUp", 0, _tempresult, 0, ActivityOutCome.Success);

                        if (_tempresult > 0)
                        {
                            ogloAppointmentBook.Dispose();
                        }

                    }
                    _FollowUpID = 0;
                    txtFollowupName.Text="";
                    numDuration.Value=numDuration.Minimum;
                    rdbDays.Checked = true;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tlsp_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion " Button Click Events "

        #region " Private Methods "

        private void FillFollowUps(Int64 FollowUpId)
        {
            gloAppointmentBook ogloAppointmentBook = new gloAppointmentBook(_databaseconnectionstring);
            DataTable dtFollowUp = null;

            try
            {
                if (FollowUpId > 0)
                {
                    dtFollowUp = ogloAppointmentBook.GetFollowUps(FollowUpId);
                    if (dtFollowUp != null && dtFollowUp.Rows.Count > 0)
                    {
                        txtFollowupName.Text = Convert.ToString(dtFollowUp.Rows[0]["sFollowUpName"]);
                        txtFollowupName.Tag = Convert.ToInt64(dtFollowUp.Rows[0]["nFollowUpID"]);
                        //numDuration.Value = Convert.ToDecimal(dtFollowUp.Rows[0]["nDuration"]);

                        if (Convert.ToString(dtFollowUp.Rows[0]["nCriteria"]) != "")
                        {
                            if (Convert.ToInt32(dtFollowUp.Rows[0]["nCriteria"]) == 0)
                            {
                                rdbDays.Checked = true;
                                rdbMonth.Checked = false;
                                rdbWeek.Checked = false;
                                rdbYear.Checked = false;
                                rdbDays_CheckedChanged(null, null);
                            }
                            else if (Convert.ToInt32(dtFollowUp.Rows[0]["nCriteria"]) == 2)
                            {
                                rdbDays.Checked = false;
                                rdbMonth.Checked = true;
                                rdbWeek.Checked = false;
                                rdbYear.Checked = false;
                                rdbMonth_CheckedChanged(null, null);
                            }
                            else if (Convert.ToInt32(dtFollowUp.Rows[0]["nCriteria"]) == 1)
                            {
                                rdbDays.Checked = false;
                                rdbMonth.Checked = false;
                                rdbWeek.Checked = true;
                                rdbYear.Checked = false;
                                rdbWeek_CheckedChanged(null, null);
                            }
                            else if (Convert.ToInt32(dtFollowUp.Rows[0]["nCriteria"]) == 3)
                            {
                                rdbDays.Checked = false;
                                rdbMonth.Checked = false;
                                rdbWeek.Checked = false;
                                rdbYear.Checked = true;
                                rdbYear_CheckedChanged(null, null);
                            }
                        }
                        //Sandip Darade 13 th Feb 09 
                        //To check if the duration is Within the range of  min & max  values for Numeric Updown 
                        if (Convert.ToDecimal(dtFollowUp.Rows[0]["nDuration"]) > Convert.ToDecimal(numDuration.Maximum) || Convert.ToDecimal(dtFollowUp.Rows[0]["nDuration"]) < Convert.ToDecimal(numDuration.Minimum))
                        {
                            numDuration.Value = numDuration.Minimum;
                        }
                        else
                        {
                            numDuration.Value = Convert.ToDecimal(dtFollowUp.Rows[0]["nDuration"]);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (ogloAppointmentBook != null) { ogloAppointmentBook.Dispose(); ogloAppointmentBook = null; }
                if (dtFollowUp != null) { dtFollowUp.Dispose(); dtFollowUp = null; }

            }
        }

        private bool ValidateData()
        {
            try
            {
                if (txtFollowupName.Text.Trim() == "")
                {
                    MessageBox.Show("Enter the Follow Up name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFollowupName.Focus();
                    return false;
                }

                if (rdbDays.Checked == false)
                {
                    if (rdbWeek.Checked == false)
                    {
                        if (rdbMonth.Checked == false)
                        {
                            MessageBox.Show("Select the Follow Up type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                }
               
                return true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {

            }
        }

        #endregion " Private Methods "

        #region " Radio Button Events "

        private void rdbDays_CheckedChanged(object sender, EventArgs e)
        {
           
             if (rdbDays.Checked == true)
            {
                rdbDays.Font = gloGlobal.clsgloFont.gFont_BOLD; // new Font("Tahoma", 9, FontStyle.Bold);
                //Added By Pramod For Not Reseting the Duration Value
                if (numDuration.Value > 365)
                {
                    numDuration.Value = 1;
                }
                numDuration.Minimum = 1;
                //numDuration.Maximum = 7;
                numDuration.Maximum = 365;
            }
            else
            {

                rdbDays.Font = gloGlobal.clsgloFont.gFont; // new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rdbWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbWeek.Checked == true)
            {
                rdbWeek.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);

                //Added By Pramod For Not Reseting the Duration Value
                if (numDuration.Value > 48)
                {
                    numDuration.Value = 1;
                    //numDuration.Maximum = 4;
                   
                }
                numDuration.Minimum = 1;
                numDuration.Maximum = 48;
            }
            else
            {

                rdbWeek.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rdbMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMonth.Checked == true)
            {
                rdbMonth.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                
                //Added By Pramod For Not Reseting the Duration Value
                if (numDuration.Value > 18)
                {
                    numDuration.Value = 1;
                }

                numDuration.Minimum = 1;
                numDuration.Maximum = 18;
            }
            else
            {

                rdbMonth.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rdbYear_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbYear.Checked == true)
            {
                rdbYear.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                numDuration.Minimum = 1;
                numDuration.Maximum = 25;
            }
            else
            {

                rdbYear.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        #endregion " Radio Button Events "

        private void txtFollowupName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "~")
            {
                e.Handled = true;
            }
        }


       
    }
}