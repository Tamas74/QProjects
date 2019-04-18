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
    internal partial class frmSetupAppointmentType : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        private AppointmentProcedureType _Type;
        private Int64 _appID = 0;
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;


        #endregion " Declarations "

        #region  " Property Procedures "

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 AppTypeId
        {
            get { return _appID; }
            set { _appID = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "
        
        #region " Constructor "

        public frmSetupAppointmentType(string DatabaseConnectionString, AppointmentProcedureType oType)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;

            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            //
            _Type = oType;
            if (oType == AppointmentProcedureType.AppointmentType)
            {
                lblAppColor.Visible = true;
                txtAppColor.Visible = true;
                btnBrowseAppColor.Visible = true;

                lblAppType.Text = "Appointment Type :";
                this.Text = "Appointment Type";
                this.Icon = global ::gloAppointmentBook.Properties.Resources.Appointment_type_01;
                lblProblemtype.Text = "Problem Type :";
                lblSelectedProblemtype.Text = "Selected Problem Type :";
            }
            else
            {
                lblAppColor.Visible = false;
                txtAppColor.Visible = false;
                btnBrowseAppColor.Visible = false;

                lblAppType.Text = "Problem Type :";

                lblProblemtype.Text = "Resources :";
                lblSelectedProblemtype.Text = "Selected Resources :";
                this.Icon = global::gloAppointmentBook.Properties.Resources.Procedure;
                this.Text = "Problem Type";
                this.Height = 377;
            }

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

        public frmSetupAppointmentType(Int64 appID, string DatabaseConnectionString, AppointmentProcedureType oType)
        {
            InitializeComponent();
            _appID = appID;
            _Type = oType;
            _databaseconnectionstring = DatabaseConnectionString;

            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            //

            if (oType == AppointmentProcedureType.AppointmentType)
            {
                lblAppColor.Visible = true;
                txtAppColor.Visible = true;
                btnBrowseAppColor.Visible = true;
               
                lblAppType.Text = "Appointment Type :";
                lblProblemtype.Text = "Problem Type :";
                lblSelectedProblemtype.Text = "Selected Problem Type :"; //Ojeswini
                this.Icon = global ::gloAppointmentBook.Properties.Resources.Appointment_type_01;
                 this.Text = "Appointment Type";
            }
            else if (oType == AppointmentProcedureType.Procedure)
            {
                lblAppColor.Visible = false;
                txtAppColor.Visible = false;
                btnBrowseAppColor.Visible = false;

                lblAppType.Text = "Problem Type :";
                lblProblemtype.Text = "Resources :";
                lblSelectedProblemtype.Text = "Selected Resources :";
                this.Icon = global::gloAppointmentBook.Properties.Resources.Procedure;

                this.Text = "Problem Type";
                this.Height = 377;
                
            }

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

        private void frmSetupAppointmentType_Load(object sender, EventArgs e)
        {

            if (_Type == AppointmentProcedureType.AppointmentType)
            {
                FillProblemTypes();
                Fill_Typecombo();
                cmbAppointmentType.Visible = true;
                pnlAppointmentTypeDetails.Visible = true;  
            }
            else if (_Type ==AppointmentProcedureType.Procedure)
            {
                FillResources();
                cmbAppointmentType.Visible = false;
                pnlAppointmentTypeDetails.Visible = false;
            }
            
            

            if (_appID > 0)
            {
                Books.AppointmentType oApp = new Books.AppointmentType(_databaseconnectionstring);
                DataTable dtAppType = null;
                dtAppType = oApp.GetAppointmentType(_appID);

                if (dtAppType != null && dtAppType.Rows.Count > 0)
                {
                    txtAppType.Text = dtAppType.Rows[0]["sAppointmentType"].ToString();
                    if (Convert.ToString(dtAppType.Rows[0]["bIsPriorAuthRequired"]).ToLower() == "true")
                        chkPriorAuthorizationRequired.Checked = true;
                    else
                        chkPriorAuthorizationRequired.Checked = false;

                    if (Convert.ToString(dtAppType.Rows[0]["bIsTurnOffReminders"]).ToLower() == "true")
                        chkTurnOffReminders.Checked = true;
                    else
                        chkTurnOffReminders.Checked = false;

                    if (_Type == AppointmentProcedureType.AppointmentType)
                    {
                        #region "Fill Appointment Type Details"
                        if (dtAppType.Rows[0]["sColorCode"].ToString() != "")
                        {
                            Int32 _Color = Convert.ToInt32(dtAppType.Rows[0]["sColorCode"]);
                            txtAppColor.BackColor = Color.FromArgb(_Color);
                        }

                        TimeSpan ts = new TimeSpan(0, Convert.ToInt32(dtAppType.Rows[0]["nDuration"]), 0);
                        numAppDurationHour.Value = ts.Hours;
                        numAppDurationMin.Value = ts.Minutes;

                        if (dtAppType.Rows[0]["nAppointmentTypeFlag"] != DBNull.Value)
                        {
                            {
                                cmbAppointmentType.Text = ((AppointmentTypeFlag)Convert.ToInt32(dtAppType.Rows[0]["nAppointmentTypeFlag"])).ToString();
                            }
                        } 
                        #endregion

                        #region "Fill Associated problem Types"

                        DataTable dtProblemTypes = null;
                        dtProblemTypes = oApp.GetAppointmentTypeProcedures(_appID);
                        oApp.Dispose();
                        TreeNode myNode;

                        if (dtProblemTypes != null)
                        {
                            for (int i = 0; i <= dtProblemTypes.Rows.Count - 1; i++)
                            {
                                myNode = new TreeNode();
                                myNode.Text = Convert.ToString(dtProblemTypes.Rows[i]["sProblemType"]);
                                myNode.Tag = dtProblemTypes.Rows[i]["nProblemTypeID"];

                                trvSelectedResources.Nodes.Add(myNode);

                                myNode = null;
                            }//for

                            //now remove the nodes which are present in trvselected resources from trvresources treeview
                            TreeNode objNode = new TreeNode();

                            for (int i = 0; i <= trvSelectedResources.Nodes.Count - 1; i++)
                            {
                                myNode = new TreeNode();

                                myNode = trvSelectedResources.Nodes[i];
                                for (int j = 0; j <= trvResources.Nodes.Count - 1; j++)
                                {

                                    TreeNode masterNode = new TreeNode();
                                    masterNode = trvResources.Nodes[j];
                                    if (masterNode.Tag.ToString() == myNode.Tag.ToString())
                                    {
                                        masterNode.Remove();
                                        break;
                                    }
                                }

                            }//for
                        }//if
                        if (dtProblemTypes != null) { dtProblemTypes.Dispose(); dtProblemTypes = null; }
                        myNode = null;
                        #endregion
                    }
                    else if (_Type == AppointmentProcedureType.Procedure)
                    {
                        #region "Fill Problem Type Resorces"

                        DataTable dtAppTypeResources = null;
                        dtAppTypeResources = oApp.GetProblemTypeResources(_appID);
                        oApp.Dispose();
                        TreeNode myNode;

                        if (dtAppTypeResources != null)
                        {
                            
                            for (int i = 0; i <= dtAppTypeResources.Rows.Count - 1; i++)
                            {
                                myNode = new TreeNode();
                                myNode.Text = dtAppTypeResources.Rows[i]["sDescription"].ToString();
                                myNode.Tag = dtAppTypeResources.Rows[i]["nResourceID"];

                                trvSelectedResources.Nodes.Add(myNode);

                                myNode = null;
                            }//for

                            //now remove the nodes which are present in trvselected resources from trvresources treeview
                            TreeNode objNode = new TreeNode();

                            for (int i = 0; i <= trvSelectedResources.Nodes.Count - 1; i++)
                            {
                                myNode = new TreeNode();

                                myNode = trvSelectedResources.Nodes[i];
                                for (int j = 0; j <= trvResources.Nodes.Count - 1; j++)
                                {

                                    TreeNode masterNode = new TreeNode();
                                    masterNode = trvResources.Nodes[j];
                                    if (masterNode.Tag.ToString() == myNode.Tag.ToString())
                                    {
                                        masterNode.Remove();
                                        break;
                                    }
                                }

                            }//for

                        }//if
                        if (dtAppTypeResources != null) { dtAppTypeResources.Dispose(); dtAppTypeResources = null; }
                        myNode = null;
                        #endregion
                    }
                }
            }


        }

       

        #endregion " Form Load "
        
        #region " Tool Strip Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                gloAppointmentBook oAppTrans = new gloAppointmentBook(_databaseconnectionstring);
                Books.AppointmentType oApp = new Books.AppointmentType(_databaseconnectionstring);
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        oApp.AppointmentTypeName = txtAppType.Text.Trim();
                        oApp.ColorCode = txtAppColor.BackColor.ToArgb();
                        TimeSpan ts = new TimeSpan((Int32)numAppDurationHour.Value, (Int32)numAppDurationMin.Value, 0);
                        oApp.Duration = Convert.ToDecimal(ts.TotalMinutes);
                        oApp.AppointmentProcedureType = _Type;
                        oApp.IsPriorAuthRequired = chkPriorAuthorizationRequired.Checked;
                        oApp.IsTurnOffReminders = chkTurnOffReminders.Checked;

                        if ( cmbAppointmentType.SelectedItem !=null && cmbAppointmentType.SelectedItem.ToString()== "")
                        {
                            oApp.AppointmentTypeFlag = AppointmentTypeFlag.None;
                        }
                        if (cmbAppointmentType.SelectedItem != null && cmbAppointmentType.SelectedItem.ToString() == AppointmentTypeFlag.Followup.ToString())
                        {
                            oApp.AppointmentTypeFlag = AppointmentTypeFlag.Followup;
                        }
                        if (cmbAppointmentType.SelectedItem != null && cmbAppointmentType.SelectedItem.ToString() == AppointmentTypeFlag.NewPatient.ToString())
                        {
                            oApp.AppointmentTypeFlag = AppointmentTypeFlag.NewPatient;
                        }

                        oApp.ClinicID = ClinicID;

                        //Validation for Appointmaent type not Empty

                        if (txtAppType.Text.Trim() == "")
                        {
                            //MessageBox.Show(this, "Please Enter the Appointment Type.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (_Type == AppointmentProcedureType.AppointmentType)
                            {
                                MessageBox.Show(this, "Enter the Appointment Type.  ", _MessageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(this, "Enter the Problem Type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            txtAppType.Focus();
                            return;

                        }
                        if (ts.TotalMinutes == Convert.ToDouble(0) && _Type == AppointmentProcedureType.AppointmentType)
                        {
                            MessageBox.Show(this, "Enter a valid duration for Appointment Type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            numAppDurationMin.Focus();
                            return;

                        }

                        //sarika 25th dec 07
                        //fill the Appointment resources from the treeview selected resources
                        // gloGeneralItem.gloItems oResources = new gloGeneralItem.gloItems();
                        gloGeneralItem.gloItem oResourceItem;

                        if (trvSelectedResources.Nodes.Count > 0)
                        {
                            for (int i = 0; i <= trvSelectedResources.Nodes.Count - 1; i++)
                            {

                                oResourceItem = new gloGeneralItem.gloItem(Convert.ToInt64(trvSelectedResources.Nodes[i].Tag), trvSelectedResources.Nodes[i].Text);
                                //oResources.Add(oResourceItem);
                                oApp.Resources.Add(oResourceItem);
                                oResourceItem.Dispose();
                                oResourceItem = null;
                            }//for
                        }//if

                        //oApp.Resources = oResources;
                        //----

                        if (AppTypeId == 0)
                        {
                            if (oApp.IsExists(0, oApp.AppointmentTypeName, _Type) == true)
                            {
                                if (_Type == AppointmentProcedureType.AppointmentType)
                                {
                                    MessageBox.Show(this, "The Appointment Type already exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show(this, "The Problem Type already exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                return;
                            }
                            _appID =oApp.Add();
                            if (_appID == 0)
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentType, ActivityType.Add, "Add Appointment Type", 0, _appID, 0, ActivityOutCome.Failure);                

                                // Record is Not Added Successfully
                                if (_Type == AppointmentProcedureType.AppointmentType)
                                {
                                    MessageBox.Show("Appointment Type not added.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Problem Type not added.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                txtAppType.Select();
                                break;
                            }
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentType, ActivityType.Add, "Add Appointment Type", 0, _appID, 0, ActivityOutCome.Failure);                

                            if (oApp.IsExists(_appID, oApp.AppointmentTypeName, _Type) == true)
                            {
                                // MessageBox.Show(this, "The Appointment type already Exist", _messageBoxCaption);
                                if (_Type == AppointmentProcedureType.AppointmentType)
                                {
                                    MessageBox.Show("The Appointment Type already exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Problem Type already exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                return;
                            }

                            oApp.AppointmentTypeID = _appID;
                            if (oApp.Modify() == false)
                            {
                                // Record is Not Added Successfully
                                if (_Type == AppointmentProcedureType.AppointmentType)
                                {
                                    MessageBox.Show("Appointment Type not added.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Problem Type not added.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                txtAppType.Select();
                                break;
                            }
                        }
                        this.Close();
                        oApp.Dispose();
                        oAppTrans.Dispose();

                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    default:
                        break;
                    case "Save":
                        {
                            #region save
                            oApp.AppointmentTypeName = txtAppType.Text.Trim();
                            oApp.ColorCode = txtAppColor.BackColor.ToArgb();
                            TimeSpan ts1 = new TimeSpan((Int32)numAppDurationHour.Value, (Int32)numAppDurationMin.Value, 0);
                            oApp.Duration = Convert.ToDecimal(ts1.TotalMinutes);
                            oApp.AppointmentProcedureType = _Type;
                            oApp.IsPriorAuthRequired = chkPriorAuthorizationRequired.Checked;
                            oApp.IsTurnOffReminders = chkTurnOffReminders.Checked;

                            if (cmbAppointmentType.SelectedItem != null && cmbAppointmentType.SelectedItem.ToString() == "")
                            {
                                oApp.AppointmentTypeFlag = AppointmentTypeFlag.None;
                            }
                            if (cmbAppointmentType.SelectedItem != null && cmbAppointmentType.SelectedItem.ToString() == AppointmentTypeFlag.Followup.ToString())
                            {
                                oApp.AppointmentTypeFlag = AppointmentTypeFlag.Followup;
                            }
                            if (cmbAppointmentType.SelectedItem != null && cmbAppointmentType.SelectedItem.ToString() == AppointmentTypeFlag.NewPatient.ToString())
                            {
                                oApp.AppointmentTypeFlag = AppointmentTypeFlag.NewPatient;
                            }

                            oApp.ClinicID = ClinicID;

                            //Validation for Appointmaent type not Empty

                            if (txtAppType.Text.Trim() == "")
                            {
                                //MessageBox.Show(this, "Please Enter the Appointment Type.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (_Type == AppointmentProcedureType.AppointmentType)
                                {
                                    MessageBox.Show(this, "Please enter the appointment type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show(this, "Please enter the problem type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                txtAppType.Focus();
                                return;

                            }
                            if (ts1.TotalMinutes == Convert.ToDouble(0) && _Type == AppointmentProcedureType.AppointmentType)
                            {
                                MessageBox.Show(this, "Please enter a valid duration for appointment type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                numAppDurationMin.Focus();
                                return;

                            }

                            //sarika 25th dec 07
                            //fill the Appointment resources from the treeview selected resources
                            // gloGeneralItem.gloItems oResources = new gloGeneralItem.gloItems();
                            gloGeneralItem.gloItem oResourceItem1;

                            if (trvSelectedResources.Nodes.Count > 0)
                            {
                                for (int i = 0; i <= trvSelectedResources.Nodes.Count - 1; i++)
                                {

                                    oResourceItem1 = new gloGeneralItem.gloItem(Convert.ToInt64(trvSelectedResources.Nodes[i].Tag), trvSelectedResources.Nodes[i].Text);
                                    //oResources.Add(oResourceItem1);
                                    oApp.Resources.Add(oResourceItem1);
                                    oResourceItem1.Dispose();
                                    oResourceItem1 = null;
                                }//for
                            }//if

                            //oApp.Resources = oResources;
                            //----

                            if (AppTypeId == 0)
                            {
                                if (oApp.IsExists(0, oApp.AppointmentTypeName, _Type) == true)
                                {
                                    if (_Type == AppointmentProcedureType.AppointmentType)
                                    {
                                        MessageBox.Show(this, "The appointment type already exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "The Problem type already exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    return;
                                }
                                _appID = oApp.Add();
                                if (_appID == 0)
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentType, ActivityType.Add, "Add Appointment Type", 0, _appID, 0, ActivityOutCome.Failure);

                                    // Record is Not Added Successfully
                                    if (_Type == AppointmentProcedureType.AppointmentType)
                                    {
                                        MessageBox.Show("Appointment type not added.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Problem type not added.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    txtAppType.Select();
                                    break;
                                }
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentType, ActivityType.Add, "Add Appointment Type", 0, _appID, 0, ActivityOutCome.Failure);

                                if (oApp.IsExists(_appID, oApp.AppointmentTypeName, _Type) == true)
                                {
                                    // MessageBox.Show(this, "The Appointment type already Exist", _messageBoxCaption);
                                    if (_Type == AppointmentProcedureType.AppointmentType)
                                    {
                                        MessageBox.Show("The Appointment type already exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Problem type already exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    return;
                                }

                                oApp.AppointmentTypeID = _appID;
                                if (oApp.Modify() == false)
                                {
                                    // Record is Not Added Successfully
                                    if (_Type == AppointmentProcedureType.AppointmentType)
                                    {
                                        MessageBox.Show("Appointment type not added.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Problem type not added.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    txtAppType.Select();
                                    break;
                                }
                            }
                            
                            oApp.Dispose();
                            oAppTrans.Dispose();
                            #endregion
                            //_Type = AppointmentProcedureType.AppointmentType;
                            _appID = 0;
                            txtAppType.Text="";
                            //numAppDurationHour.Value=numAppDurationHour.Minimum;
                            //numAppDurationMin.Value = numAppDurationMin.Minimum;
                            cmbAppointmentType.SelectedIndex=-1;
                            txtAppColor.BackColor = Color.Empty;
                            trvSelectedResources.Nodes.Clear();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                this.Close();
            }
        }

        #endregion " Tool Strip Event "

        #region " Button Click Events "

        private void btnBrowseAppColor_Click(object sender, EventArgs e)
        {
            clDlg.Color = txtAppColor.BackColor;
            try
            {
                clDlg.CustomColors = gloGlobal.gloCustomColor.customColor;
            }
            catch
            {
            }
            if (clDlg.ShowDialog(this) == DialogResult.OK)
            {
                txtAppColor.BackColor = clDlg.Color;
                try
                {
                    gloGlobal.gloCustomColor.customColor = clDlg.CustomColors;
                }
                catch
                {
                }
            }
            
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TreeNode myNode;

            try
            {
                if (trvResources.Nodes.Count > 0)
                {
                    if (trvResources.SelectedNode != null)
                    {
                        myNode = new TreeNode();

                        myNode.Text = trvResources.SelectedNode.Text;
                        myNode.Tag = trvResources.SelectedNode.Tag;

                        trvSelectedResources.Nodes.Add(myNode);
                        trvResources.SelectedNode.Remove();

                    }//if
                }//if
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                myNode = null;
            }
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            TreeNode myNode;

            try
            {
                if (trvResources.Nodes.Count > 0)
                {
                    for (int i = 0; i <= trvResources.Nodes.Count - 1; i++)
                    {
                        myNode = new TreeNode();
                        myNode.Text = trvResources.Nodes[i].Text;
                        myNode.Tag = trvResources.Nodes[i].Tag;

                        trvSelectedResources.Nodes.Add(myNode);

                        myNode = null;
                    }//for


                    trvResources.Nodes.Clear();
                }//if
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            TreeNode myNode;

            try
            {
                if (trvSelectedResources.Nodes.Count > 0)
                {
                    if (trvSelectedResources.SelectedNode != null)
                    {
                        myNode = new TreeNode();

                        myNode.Text = trvSelectedResources.SelectedNode.Text;
                        myNode.Tag = trvSelectedResources.SelectedNode.Tag;

                        trvResources.Nodes.Add(myNode);
                        trvSelectedResources.Nodes.Remove(trvSelectedResources.SelectedNode);

                        myNode = null;
                    }
                }

            }//try
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 

            }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            TreeNode myNode;

            try
            {
                if (trvSelectedResources.Nodes.Count > 0)
                {
                    for (int i = 0; i <= trvSelectedResources.Nodes.Count - 1; i++)
                    {
                        myNode = new TreeNode();
                        myNode.Text = trvSelectedResources.Nodes[i].Text;
                        myNode.Tag = trvSelectedResources.Nodes[i].Tag;

                        trvResources.Nodes.Add(myNode);

                        myNode = null;
                    }//for


                    trvSelectedResources.Nodes.Clear();
                }//if
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        
        #endregion " Button Click Events "

        #region  " Private Methods "

        private void FillResources()
        {
            Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
            DataTable dtResources = null;
            TreeNode myNode;

            try
            {
                dtResources = oResource.GetResources();

                //now fill the treeview with resources except Provider
                if (dtResources != null)
                {
                    if (dtResources.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtResources.Rows.Count - 1; i++)
                        {
                            myNode = new TreeNode();

                            myNode.Text = dtResources.Rows[i]["sDescription"].ToString();
                            myNode.Tag = dtResources.Rows[i]["nResourceID"];

                            trvResources.Nodes.Add(myNode);

                            myNode = null;
                        }//for
                        
                    }//if
                }//if

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oResource != null) { oResource.Dispose(); oResource = null; }
                if (dtResources != null) { dtResources.Dispose(); dtResources = null; }
            }
        }//FillResources()

        private void FillProblemTypes()
        {
            Books.AppointmentType oAppointmentType = new Books.AppointmentType(_databaseconnectionstring);
            TreeNode myNode;
            DataTable dtProblemType = null;
            try
            {
                dtProblemType = oAppointmentType.GetList(AppointmentProcedureType.Procedure);

                if (dtProblemType != null && dtProblemType.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtProblemType.Rows.Count - 1; i++)
                    {
                        myNode = new TreeNode();

                        myNode.Text = Convert.ToString(dtProblemType.Rows[i]["sAppointmentType"]);
                        myNode.Tag = dtProblemType.Rows[i]["nAppointmentTypeID"];

                        trvResources.Nodes.Add(myNode);

                        myNode = null;
                    }//for
                }

            }
            catch (Exception ex)
            { 
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oAppointmentType != null) { oAppointmentType.Dispose(); oAppointmentType = null; }
                if (dtProblemType != null) { dtProblemType.Dispose(); dtProblemType = null; }
            }
        }

        private void Fill_Typecombo()
        {
            cmbAppointmentType.Items.Clear();
            cmbAppointmentType.Items.Add("");
            cmbAppointmentType.Items.Add(AppointmentTypeFlag.Followup.ToString());
            cmbAppointmentType.Items.Add(AppointmentTypeFlag.NewPatient.ToString());
        }

        #endregion  " Private Methods "

        #region Designer MouseHover & Mouser Leave Events

        //This code is added by shilpa on 21st Jan 2008 for Buttons MouseHover & MouseLeave Events
        private void btnBrowseAppColor_MouseHover(object sender, EventArgs e)
        {
            btnBrowseAppColor.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Yellow;
            btnBrowseAppColor.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnBrowseAppColor_MouseLeave(object sender, EventArgs e)
        {
            btnBrowseAppColor.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Button;
            btnBrowseAppColor.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnAdd_MouseHover(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Yellow;
            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Button;
            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnAddAll_MouseHover(object sender, EventArgs e)
        {
            btnAddAll.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Yellow;
            btnAddAll.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnAddAll_MouseLeave(object sender, EventArgs e)
        {
            btnAddAll.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Button;
            btnAddAll.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnRemove_MouseHover(object sender, EventArgs e)
        {
            btnRemove.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Yellow;
            btnRemove.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnRemove_MouseLeave(object sender, EventArgs e)
        {
            btnRemove.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Button;
            btnRemove.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnRemoveAll_MouseHover(object sender, EventArgs e)
        {
            btnRemoveAll.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Yellow;
            btnRemoveAll.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnRemoveAll_MouseLeave(object sender, EventArgs e)
        {
            btnRemoveAll.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Button;
            btnRemoveAll.BackgroundImageLayout = ImageLayout.Stretch;
        }

        


        #endregion

        private void numAppDurationHour_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (numAppDurationHour.Value < 12)
                {
                    numAppDurationMin.Enabled = true;
                }
                else if ((numAppDurationHour.Value == 12) && (numAppDurationMin.Value != 0))
                {
                    numAppDurationMin.ValueChanged -= new EventHandler(numAppDurationMin_ValueChanged);
                    numAppDurationMin.Value = 0;
                    numAppDurationMin.ValueChanged += new EventHandler(numAppDurationMin_ValueChanged);

                    numAppDurationMin.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void numAppDurationMin_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ((numAppDurationMin.Value != 0) && (numAppDurationHour.Value == 12))
                {
                    numAppDurationMin.ValueChanged -= new EventHandler(numAppDurationMin_ValueChanged);
                    numAppDurationMin.Value = 0;
                    numAppDurationMin.ValueChanged += new EventHandler(numAppDurationMin_ValueChanged);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void txtAppType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "~")
            {
                e.Handled = true;
            }
        }
       
      
    }
}