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
    internal partial class frmSetupAppointmentStatus : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        private Int64 _AppStatusID = 0;
        private Int64 _ReturnAppStatusID = 0;
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        #endregion " Declarations "

        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 ReturnAppStatusID
        {
            get { return _ReturnAppStatusID; }
            set { _ReturnAppStatusID = value; }
        }


        #endregion  " Property Procedures "

        #region " Constructor "

        public frmSetupAppointmentStatus(string  databaseconnectionstring)
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

        public frmSetupAppointmentStatus(Int64 AppStatusID, string databaseconnectionstring)
        {
            InitializeComponent();
            _AppStatusID = AppStatusID;
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

        #endregion " Constructor "

        #region " Form Load "

        private void frmSetupAppointmentStatus_Load(object sender, EventArgs e)
        {
            try
            {
                if (_AppStatusID != 0)
                {

                    FillAppointmentStatus();

                    
                }
                txtAppStatus.Select();
            }//try
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillAppointmentStatus()
        {
            Books.AppointmentStatus oAppStatus = new Books.AppointmentStatus(_databaseconnectionstring);
            try
            {
                DataTable dt = oAppStatus.GetAppointmentStatus(_AppStatusID);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtAppStatus.Text = dt.Rows[0]["sAppointmentStatus"].ToString(); // Resourse Description
                        chkIsSystem.Checked = Convert.ToBoolean(dt.Rows[0]["bIsSystem"]);
                    }
                }
                dt.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oAppStatus != null) { oAppStatus.Dispose(); oAppStatus = null; }
            }
        }

        #endregion " Form Load "

        #region " Tool Strip Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Books.AppointmentStatus oAppStatus = null;
            Books.AppointmentStatus oAppStatus1 = null;
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        if (txtAppStatus.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter the Appointment Status.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAppStatus.Select();
                            break;
                        }

                        oAppStatus = new Books.AppointmentStatus(_databaseconnectionstring);

                        if (oAppStatus.IsExists(_AppStatusID, txtAppStatus.Text.Trim()) == true)
                        {
                            MessageBox.Show("Appointment Status with same name already exists, Enter different Appointment Status name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAppStatus.Select();
                            break;
                        }

                        oAppStatus.AppointmentStatusID = _AppStatusID;
                        oAppStatus.AppointmentStatusName = txtAppStatus.Text.Trim();
                        oAppStatus.IsSystem = chkIsSystem.Checked;
                        oAppStatus.IsBlocked = false;
                        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                        //oAppStatus.ClinicID = 1;
                        oAppStatus.ClinicID = ClinicID;
                        //

                        if (_AppStatusID == 0)
                        {
                            _ReturnAppStatusID = oAppStatus.Add();

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentStatus, ActivityType.Add, "Add Appointment Status", 0, _ReturnAppStatusID, 0, ActivityOutCome.Success);

                            if (_ReturnAppStatusID <= 0)
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentStatus, ActivityType.Add, "Add Appointment Status", 0, _ReturnAppStatusID, 0, ActivityOutCome.Failure);
                                // Record is Not Added Successfully
                                MessageBox.Show("Appointment Status not added.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtAppStatus.Select();
                                break;

                            }
                        }
                        else
                        {
                            if (oAppStatus.Modify() == false)
                            {
                                MessageBox.Show("Appointment Status not modified.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtAppStatus.Select();
                                break;
                            }
                        }

                        // Record is Added Successfully
                        //frmViewResourceType oRefResTypes;
                        //oRefResTypes = (frmViewResourceType)Owner.ActiveMdiChild;
                        //oRefResTypes.FillResourceTypes();
                        txtAppStatus.Text = "";
                        txtAppStatus.Select();
                        //'oRefResTypes = null;
                        this.Close();
                        if (_AppStatusID != 0)
                        {
                            this.Close();
                        }
                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    case "Save":
                        if (txtAppStatus.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter the Appointment Status.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAppStatus.Select();
                            break;
                        }

                        oAppStatus1 = new Books.AppointmentStatus(_databaseconnectionstring);

                        if (oAppStatus1.IsExists(_AppStatusID, txtAppStatus.Text.Trim()) == true)
                        {
                            MessageBox.Show("Appointment Status with same name already exists, Enter different Appointment Status name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAppStatus.Select();
                            break;
                        }

                        oAppStatus1.AppointmentStatusID = _AppStatusID;
                        oAppStatus1.AppointmentStatusName = txtAppStatus.Text.Trim();
                        oAppStatus1.IsSystem = chkIsSystem.Checked;
                        oAppStatus1.IsBlocked = false;
                        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                        //oAppStatus1.ClinicID = 1;
                        oAppStatus1.ClinicID = ClinicID;
                        //

                        if (_AppStatusID == 0)
                        {
                            _ReturnAppStatusID = oAppStatus1.Add();

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentStatus, ActivityType.Add, "Add Appointment Status", 0, _ReturnAppStatusID, 0, ActivityOutCome.Success);

                            if (_ReturnAppStatusID <= 0)
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentStatus, ActivityType.Add, "Add Appointment Status", 0, _ReturnAppStatusID, 0, ActivityOutCome.Failure);
                                // Record is Not Added Successfully
                                MessageBox.Show("Appointment Status not added.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtAppStatus.Select();
                                break;

                            }
                        }
                        else
                        {
                            if (oAppStatus1.Modify() == false)
                            {
                                MessageBox.Show("Appointment Status not modified.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtAppStatus.Select();
                                break;
                            }
                        }

                        // Record is Added Successfully
                        //frmViewResourceType oRefResTypes;
                        //oRefResTypes = (frmViewResourceType)Owner.ActiveMdiChild;
                        //oRefResTypes.FillResourceTypes();
                        txtAppStatus.Text = "";
                        txtAppStatus.Select();
                        //'oRefResTypes = null;

                        if (_AppStatusID != 0)
                        {
                            _AppStatusID = 0;
                            txtAppStatus.Text = "";
                            chkIsSystem.Checked = false;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oAppStatus != null) { oAppStatus.Dispose(); oAppStatus = null; }
                if (oAppStatus1 != null) { oAppStatus1.Dispose(); oAppStatus1 = null; }
            }
        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {

        }

        private void txtAppStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "~")
            {
                e.Handled = true;
            }
        }
        //function

        #endregion " Tool Strip Event "

    }
}