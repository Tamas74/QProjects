using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using gloBilling.Statement;
using gloAuditTrail;

namespace gloBilling
{
    public partial class frmSetupPatientStatementFilterCriteria : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        private Int64 _StatementCriteriaID = 0;
        private Int64 _ReturnStatementCriteriaID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        private gloGeneralItem.gloItems ogloItems = null;
        string _FilterCriteria = "Filter";

        #endregion " Declarations "

        #region  " Property Procedures "

        public Int64 StatementCriteriaID
        {
            get { return _StatementCriteriaID; }
            set { _StatementCriteriaID = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public Int64 ReturnStatementCriteriaID
        {
            get { return _ReturnStatementCriteriaID; }
            set { _ReturnStatementCriteriaID = value; }
        }


        #endregion  " Property Procedures "

        #region " Constructor "

        public frmSetupPatientStatementFilterCriteria(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;

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

        public frmSetupPatientStatementFilterCriteria(Int64 StatementCriteriaID, string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            _StatementCriteriaID = StatementCriteriaID;

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

        private void frmSetupPatientStatementFilterCriteria_Load(object sender, EventArgs e)
        {
            try
            {
                //function call to fill patient name criteria A-Z
                FillPatientNameCriteria();

                if (_StatementCriteriaID != 0)
                {
                    PatinetStatementCriteria oPatinetStatementCriteria = new PatinetStatementCriteria(_databaseconnectionstring);
                    if (oPatinetStatementCriteria.GetPatinetStatementCriteria(_StatementCriteriaID))
                    {
                        txtStatementCriteriaName.Text = oPatinetStatementCriteria.StatementCriteriaName.Trim();
                        chkDefault.Checked = oPatinetStatementCriteria.IsDefault;

                        //cmbCreditCard.SelectedIndex = cmbCreditCard.FindStringExact(oPatinetStatementCriteria.CreditCard);

                        #region "Fill Filter "

                        if (oPatinetStatementCriteria.PatStatementCriteriaFilter != null)
                        {
                            DataTable oBindTableCPT = new DataTable();
                            DataTable oBindTableInsurance = new DataTable();
                           // DataRow oRow;

                            oBindTableCPT.Columns.Add("ID");
                            oBindTableCPT.Columns.Add("DispName");

                            oBindTableInsurance.Columns.Add("ID");
                            oBindTableInsurance.Columns.Add("DispName");

                            for (int i = 0; i < oPatinetStatementCriteria.PatStatementCriteriaFilter.Rows.Count; i++)
                            {
                                DataRow dr = oPatinetStatementCriteria.PatStatementCriteriaFilter.Rows[i];

                                //code added by dipak to fill information of PatinetStatementCriteria
                                switch (Convert.ToString(dr[0]))
                                {
                                    case "Balance":
                                        if (Convert.ToString(dr[2]).Trim() != "")
                                        {
                                            txtDueAmount.Text = Convert.ToString(dr[2]);
                                        }
                                        break;
                                    case "Wait Days":
                                        if (Convert.ToString(dr[2]).Trim() != "")
                                        {
                                            //txtWaitFordays .Text = Convert.ToString(dr[2]);
                                            nmWaitFordays.Value = Convert.ToInt16(dr[2]);
                                        }
                                        break;
                                    case "From":
                                        if (Convert.ToString(dr[2]).Trim() != "")
                                        {
                                            cmbNameFrom.Text = Convert.ToString(dr[2]);
                                        }
                                        break;
                                    case "To":
                                        if (Convert.ToString(dr[2]).Trim() != "")
                                        {
                                            cmbNameTo.Text = Convert.ToString(dr[2]);
                                        }
                                        break;

                                }
                            }

                            cmbInsurance.DataSource = oBindTableInsurance;
                            cmbInsurance.DisplayMember = "DispName";
                            cmbInsurance.ValueMember = "ID";

                            cmbCPT.DataSource = oBindTableCPT;
                            cmbCPT.DisplayMember = "DispName";
                            cmbCPT.ValueMember = "ID";
                        }

                        #endregion


                    }
                    oPatinetStatementCriteria.Dispose();
                }


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion " Form Load "

        #region " Tool Strip Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        {
                            if (txtStatementCriteriaName.Text.Trim() == "")
                            {
                                MessageBox.Show("Enter the Patient Statement Filter Settings.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtStatementCriteriaName.Select();
                                break;
                            }

                            if (txtDueAmount.Text.Trim() == "")
                            {
                                txtDueAmount.Text = "0.00";
                            }

                            PatinetStatementCriteria oPatinetStatementCriteria = new PatinetStatementCriteria(_databaseconnectionstring);

                            if (oPatinetStatementCriteria.IsExists(_StatementCriteriaID, txtStatementCriteriaName.Text.Trim(), _FilterCriteria) == true)
                            {
                                MessageBox.Show("Patient Statement Filter Settings with same name already exists, Enter unique Patient Statement Filter Settings name.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtStatementCriteriaName.Select();
                                break;
                            }

                            oPatinetStatementCriteria.StatementCriteriaID = _StatementCriteriaID;
                            oPatinetStatementCriteria.StatementCriteriaName = txtStatementCriteriaName.Text.Trim();
                            oPatinetStatementCriteria.CriteriaType = "Filter";
                            oPatinetStatementCriteria.IsDefault = chkDefault.Checked;



                            #region " Filter"

                            DataTable dt = new DataTable();

                            DataColumn dc;

                            dc = new DataColumn("CritetiaName");
                            dc.DataType = typeof(System.String);
                            dt.Columns.Add(dc);

                            dc = new DataColumn("ValueId");
                            dc.DataType = typeof(System.String);
                            dt.Columns.Add(dc);

                            dc = new DataColumn("ValueCode");
                            dc.DataType = typeof(System.String);
                            dt.Columns.Add(dc);

                            dc = new DataColumn("ValueDesc");
                            dc.DataType = typeof(System.String);
                            dt.Columns.Add(dc);

                            DataRow dr;
                            dr = dt.NewRow();
                            dr["CritetiaName"] = "Balance";

                            dr["ValueId"] = 0;
                            dr["ValueCode"] = txtDueAmount.Text;
                            dr["ValueDesc"] = txtDueAmount.Text;
                            dt.Rows.Add(dr);

                            dr = dt.NewRow();
                            dr["CritetiaName"] = "Wait Days";

                            dr["ValueId"] = 0;
                            dr["ValueCode"] = nmWaitFordays.Value;
                            dr["ValueDesc"] = nmWaitFordays.Value;
                            dt.Rows.Add(dr);

                            dr = dt.NewRow();
                            dr["CritetiaName"] = "From";

                            dr["ValueId"] = 0;
                            dr["ValueCode"] = cmbNameFrom.Text;
                            dr["ValueDesc"] = cmbNameFrom.Text;
                            dt.Rows.Add(dr);

                            dr = dt.NewRow();
                            dr["CritetiaName"] = "To";

                            dr["ValueId"] = 0;
                            dr["ValueCode"] = cmbNameTo.Text;
                            dr["ValueDesc"] = cmbNameTo.Text;
                            dt.Rows.Add(dr);

                            oPatinetStatementCriteria.PatStatementCriteriaFilter = dt;
                            #endregion

                            oPatinetStatementCriteria.ClinicID = ClinicID;
                            if (_StatementCriteriaID == 0)
                            {
                                /* Commented by Rahul Patel on 15-09-2010*/
                                /* For change of showing the default message fuctionality */
                                /*_ReturnStatementCriteriaID = oPatinetStatementCriteria.Add();
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Add, "Add Patient Statement Filter Settings", 0, _ReturnStatementCriteriaID, 0, ActivityOutCome.Success);

                                if (_ReturnStatementCriteriaID < 0)
                                {
                                    // Record is Not Added Successfully
                                    MessageBox.Show("Patient Statement Filter Settings not added, please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtStatementCriteriaName.Select();

                                    break;
                                }*/
                                if (chkDefault.Checked)
                                {
                                    if (oPatinetStatementCriteria.IsDefaultChanged(txtStatementCriteriaName.Text.Trim()))
                                    {
                                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want change default Patient Statement Filter Settings to '" + txtStatementCriteriaName.Text.Trim() + "'?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                                        {
                                            AddPatientStatementCriteria(oPatinetStatementCriteria);
                                        }
                                    }
                                    else
                                        AddPatientStatementCriteria(oPatinetStatementCriteria);
                                }
                                else
                                    AddPatientStatementCriteria(oPatinetStatementCriteria);
                            }
                            else
                            {
                                /* Commented by Rahul Patel on 15-09-2010*/
                                /* For change of showing the default message fuctionality */
                                /*  ReturnStatementCriteriaID = oPatinetStatementCriteria.Add();
                                   gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Modify, "Patient Statement Filter Settings", 0, _ReturnStatementCriteriaID, 0, ActivityOutCome.Success);

                                   if (_ReturnStatementCriteriaID < 0)
                                   {
                                       MessageBox.Show("Patient Statement Filter Settings not modified, please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                       this.Close();
                                       txtStatementCriteriaName.Select();
                                       break;
                                   }*/
                                if (chkDefault.Checked)
                                {
                                    if (oPatinetStatementCriteria.IsDefaultChanged(txtStatementCriteriaName.Text.Trim()))
                                    {
                                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want change default Patient Statement Filter Settings to '" + txtStatementCriteriaName.Text.Trim() + "'?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                                            AddPatientStatementCriteria(oPatinetStatementCriteria);
                                    }
                                    else
                                        AddPatientStatementCriteria(oPatinetStatementCriteria);
                                }
                                else
                                    AddPatientStatementCriteria(oPatinetStatementCriteria);
                            }    
                            txtStatementCriteriaName.Text = "";
                            txtDueAmount.Text = "0.00";
                            //Added by Rahul Patel to unchecked the default after save 
                            chkDefault.Checked = false;
                            txtStatementCriteriaName.Select();
                            //ClearControls();
                            //this.Close();
                        }
                        break;
                    case "Save&Close":
                        {
                            if (txtStatementCriteriaName.Text.Trim() == "")
                            {
                                MessageBox.Show("Enter the Patient Statement Filter Settings name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtStatementCriteriaName.Select();
                                break;
                            }

                            if (txtDueAmount.Text.Trim() == "")
                            {
                                //MessageBox.Show("Please enter balance.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtDueAmount.Text = "0.00";
                            }

                            PatinetStatementCriteria oPatinetStatementCriteria = new PatinetStatementCriteria(_databaseconnectionstring);

                            if (oPatinetStatementCriteria.IsExists(_StatementCriteriaID, txtStatementCriteriaName.Text.Trim(), _FilterCriteria) == true)
                            {
                                MessageBox.Show("Patient Statement Filter Settings with same name already exists, please enter unique Patient Statement Filter Settings name.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtStatementCriteriaName.Select();
                                break;
                            }

                            oPatinetStatementCriteria.StatementCriteriaID = _StatementCriteriaID;
                            oPatinetStatementCriteria.StatementCriteriaName = txtStatementCriteriaName.Text.Trim();
                            oPatinetStatementCriteria.IsDefault = chkDefault.Checked;
                            oPatinetStatementCriteria.CriteriaType = "Filter";

                            #region " Filter"

                            DataTable dt = new DataTable();

                            DataColumn dc;

                            dc = new DataColumn("CritetiaName");
                            dc.DataType = typeof(System.String);
                            dt.Columns.Add(dc);

                            dc = new DataColumn("ValueId");
                            dc.DataType = typeof(System.String);
                            dt.Columns.Add(dc);

                            dc = new DataColumn("ValueCode");
                            dc.DataType = typeof(System.String);
                            dt.Columns.Add(dc);

                            dc = new DataColumn("ValueDesc");
                            dc.DataType = typeof(System.String);
                            dt.Columns.Add(dc);

                            DataRow dr;
                            dr = dt.NewRow();
                            dr["CritetiaName"] = "Balance";

                            dr["ValueId"] = 0;
                            dr["ValueCode"] = txtDueAmount.Text;
                            dr["ValueDesc"] = txtDueAmount.Text;
                            dt.Rows.Add(dr);

                            dr = dt.NewRow();
                            dr["CritetiaName"] = "Wait Days";

                            dr["ValueId"] = 0;
                            dr["ValueCode"] = nmWaitFordays.Value;
                            dr["ValueDesc"] = nmWaitFordays.Value;
                            dt.Rows.Add(dr);

                            dr = dt.NewRow();
                            dr["CritetiaName"] = "From";

                            dr["ValueId"] = 0;
                            dr["ValueCode"] = cmbNameFrom.Text;
                            dr["ValueDesc"] = cmbNameFrom.Text;
                            dt.Rows.Add(dr);

                            dr = dt.NewRow();
                            dr["CritetiaName"] = "To";

                            dr["ValueId"] = 0;
                            dr["ValueCode"] = cmbNameTo.Text;
                            dr["ValueDesc"] = cmbNameTo.Text;
                            dt.Rows.Add(dr);

                            oPatinetStatementCriteria.PatStatementCriteriaFilter = dt;
                            #endregion

                            oPatinetStatementCriteria.ClinicID = ClinicID;

                            if (_StatementCriteriaID == 0)
                            {
                                /*  _ReturnStatementCriteriaID = oPatinetStatementCriteria.Add();
                                  gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Add, "Add Patient Statement Filter Settings", 0, _ReturnStatementCriteriaID, 0, ActivityOutCome.Success);

                                  if (_ReturnStatementCriteriaID < 0)
                                  {
                                      // Record is Not Added Successfully
                                      MessageBox.Show("Patient Statement Filter Settings not added, please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                      txtStatementCriteriaName.Select();

                                      break;
                                  }*/
                                /* Added By Rahul Patel */
                                if (chkDefault.Checked)
                                {
                                    if (oPatinetStatementCriteria.IsDefaultChanged(txtStatementCriteriaName.Text.Trim()))
                                    {
                                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want change default Patient Statement Filter Settings to '" + txtStatementCriteriaName.Text.Trim() + "'?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                                        {
                                            AddPatientStatementCriteria(oPatinetStatementCriteria);
                                        }
                                    }
                                    else
                                        AddPatientStatementCriteria(oPatinetStatementCriteria);
                                }
                                else
                                    AddPatientStatementCriteria(oPatinetStatementCriteria);
                            }
                            else
                            {
                                /*_ReturnStatementCriteriaID = oPatinetStatementCriteria.Add();
                                 gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Modify, "Modify Patient Statement Filter Settings", 0, _ReturnStatementCriteriaID, 0, ActivityOutCome.Success);

                                 if (_ReturnStatementCriteriaID < 0)
                                 {
                                     MessageBox.Show("Patient Statement Filter Settings not modified, please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                     this.Close();
                                     txtStatementCriteriaName.Select();
                                     break;
                                 }*/
                                /* Added By Rahul Patel */
                                if (chkDefault.Checked)
                                {
                                    if (oPatinetStatementCriteria.IsDefaultChanged(txtStatementCriteriaName.Text.Trim()))
                                    {
                                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want change default setting of Patient Statement Filter Settings to '" + txtStatementCriteriaName.Text.Trim() + "'?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                                        {
                                            AddPatientStatementCriteria(oPatinetStatementCriteria);
                                        }
                                    }
                                    else
                                        AddPatientStatementCriteria(oPatinetStatementCriteria);
                                }
                                else
                                    AddPatientStatementCriteria(oPatinetStatementCriteria);
                            }

                            txtStatementCriteriaName.Text = "";
                            txtStatementCriteriaName.Select();
                            //ClearControls();
                            this.Close();
                        }
                        break;
                    case "Close":
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        //Create by Rahul Patel on 16/09/2010
        // For adding Patient Statement criteria detail.
        private void AddPatientStatementCriteria(PatinetStatementCriteria oPatinetStatementCriteria)
        {
            _ReturnStatementCriteriaID = oPatinetStatementCriteria.Add("Filter");
            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Add, "Add Patient Statement Filter Settings", 0, _ReturnStatementCriteriaID, 0, ActivityOutCome.Success);

            if (_ReturnStatementCriteriaID < 0)
            {
                // Record is Not Added Successfully
                MessageBox.Show("Patient Statement Filter Settings not added, please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStatementCriteriaName.Select();
            }
        } // AddPatientStatementCriteria

        #endregion " Tool Strip Event "

        #region "User Control Events"

        private void btnBrowseCPT_Click(object sender, EventArgs e)
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
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch { }

                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch { }

                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.CPT, true, this.Width);

                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " CPT";
                _CurrentControlType = gloListControl.gloListControlType.CPT;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbCPT.DataSource != null)
                {
                    for (int i = 0; i < cmbCPT.Items.Count; i++)
                    {
                        cmbCPT.SelectedIndex = i;
                        cmbCPT.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbCPT.SelectedValue), cmbCPT.Text, "");
                    }
                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnClearCPT_Click(object sender, EventArgs e)
        {
          //  cmbCPT.Items.Clear();
            cmbCPT.DataSource = null;
            cmbCPT.Refresh();
        }

        private void btnClearInsurance_Click(object sender, EventArgs e)
        {

            if (cmbInsurance.Items.Count > 0)
            {

            }
           
            cmbInsurance.DataSource = null;
            cmbInsurance.Items.Clear();
            cmbInsurance.Refresh();


        }

        private void btnBrowseInsurance_Click(object sender, EventArgs e)
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
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch { }

                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch { }

                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Insurance, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Insurances";
                _CurrentControlType = gloListControl.gloListControlType.AllPatientInsurances;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);
                if (cmbInsurance.DataSource != null)
                {
                    for (int i = 0; i < cmbInsurance.Items.Count; i++)
                    {
                        cmbInsurance.SelectedIndex = i;
                        cmbInsurance.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsurance.SelectedValue), cmbInsurance.Text);
                    }
                }

                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;

            switch (_CurrentControlType)
            {

                case gloListControl.gloListControlType.AllPatientInsurances:
                    {
                        
                        cmbInsurance.DataSource = null;
                        cmbInsurance.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");
                            ogloItems = new gloGeneralItem.gloItems();
                            gloGeneralItem.gloItem ogloItem = null;
                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                                ogloItem = new gloGeneralItem.gloItem();
                                ogloItem.ID = oListControl.SelectedItems[_Counter].ID;
                                ogloItem.Description = oListControl.SelectedItems[_Counter].Description;
                                ogloItems.Add(ogloItem);
                                ogloItem.Dispose();
                                ogloItem = null;
                            }

                            cmbInsurance.DataSource = oBindTable;
                            cmbInsurance.DisplayMember = "DispName";
                            cmbInsurance.ValueMember = "ID";
                        }


                    }
                    break;
                case gloListControl.gloListControlType.CPT:
                    {
                       
                        cmbCPT.DataSource = null;
                        cmbCPT.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Code;
                                oBindTable.Rows.Add(oRow);
                            }

                            cmbCPT.DataSource = oBindTable;
                            cmbCPT.DisplayMember = "DispName";
                            cmbCPT.ValueMember = "ID";
                        }


                    }
                    break;
                default:
                    {
                    }
                    break;
            }
        }
        void oListControl_ItemClosedClick(object sender, EventArgs e)
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
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    }
                    catch { }

                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }

                }
                catch { }
                
            }
        }



        #endregion


        #region " Form Control Events"

        private void txtPracZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!(e.KeyChar == Convert.ToChar(8)))
                {
                    if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                    {
                        e.Handled = true;

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        #endregion


        #region "Fill Methods"
        private void FillPatientNameCriteria()
        {
            try
            {
                cmbNameFrom.Items.Add("A");
                cmbNameFrom.Items.Add("B");
                cmbNameFrom.Items.Add("C");
                cmbNameFrom.Items.Add("D");
                cmbNameFrom.Items.Add("E");
                cmbNameFrom.Items.Add("F");
                cmbNameFrom.Items.Add("G");
                cmbNameFrom.Items.Add("H");
                cmbNameFrom.Items.Add("I");
                cmbNameFrom.Items.Add("J");
                cmbNameFrom.Items.Add("K");
                cmbNameFrom.Items.Add("L");
                cmbNameFrom.Items.Add("M");
                cmbNameFrom.Items.Add("N");
                cmbNameFrom.Items.Add("O");
                cmbNameFrom.Items.Add("P");
                cmbNameFrom.Items.Add("Q");
                cmbNameFrom.Items.Add("R");
                cmbNameFrom.Items.Add("S");
                cmbNameFrom.Items.Add("T");
                cmbNameFrom.Items.Add("U");
                cmbNameFrom.Items.Add("V");
                cmbNameFrom.Items.Add("W");
                cmbNameFrom.Items.Add("X");
                cmbNameFrom.Items.Add("Y");
                cmbNameFrom.Items.Add("Z");
                cmbNameFrom.Text = "A";

                cmbNameTo.Items.Add("A");
                cmbNameTo.Items.Add("B");
                cmbNameTo.Items.Add("C");
                cmbNameTo.Items.Add("D");
                cmbNameTo.Items.Add("E");
                cmbNameTo.Items.Add("F");
                cmbNameTo.Items.Add("G");
                cmbNameTo.Items.Add("H");
                cmbNameTo.Items.Add("I");
                cmbNameTo.Items.Add("J");
                cmbNameTo.Items.Add("K");
                cmbNameTo.Items.Add("L");
                cmbNameTo.Items.Add("M");
                cmbNameTo.Items.Add("N");
                cmbNameTo.Items.Add("O");
                cmbNameTo.Items.Add("P");
                cmbNameTo.Items.Add("Q");
                cmbNameTo.Items.Add("R");
                cmbNameTo.Items.Add("S");
                cmbNameTo.Items.Add("T");
                cmbNameTo.Items.Add("U");
                cmbNameTo.Items.Add("V");
                cmbNameTo.Items.Add("W");
                cmbNameTo.Items.Add("X");
                cmbNameTo.Items.Add("Y");
                cmbNameTo.Items.Add("Z");
                cmbNameTo.Text = "Z";

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }
        #endregion

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongYellow;
            ((System.Windows.Forms.Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            ((System.Windows.Forms.Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }



        private void txtDueAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //code added by dipak to validate txtDueAmount for decimal
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            {
                e.Handled = true;
            }
            else
            {
                if (txtDueAmount.SelectionStart > txtDueAmount.Text.IndexOf("."))
                {
                    if (txtDueAmount.Text.Contains(".") == true)
                    {
                        if (txtDueAmount.Text.Substring(txtDueAmount.Text.IndexOf(".") + 1, txtDueAmount.Text.Length - (txtDueAmount.Text.IndexOf(".") + 1)).Length == 2)
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            if (e.KeyChar == Convert.ToChar(46) && txtDueAmount.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void txtWaitFordays_KeyPress(object sender, KeyPressEventArgs e)
        {
            //code added by dipak to validate txtDueAmount for decimal
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(46)) //&& txtOutSideLabCharges.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void tbpFilter_Click(object sender, EventArgs e)
        {

        }
    }
}