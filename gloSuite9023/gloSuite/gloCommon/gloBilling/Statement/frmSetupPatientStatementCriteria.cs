using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using gloAuditTrail;
using gloBilling.Statement;

namespace gloBilling
{
    public partial class frmSetupPatientStatementCriteria : Form
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
        
        public frmSetupPatientStatementCriteria(string databaseconnectionstring )
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

        public frmSetupPatientStatementCriteria(Int64 StatementCriteriaID, string databaseconnectionstring)
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

        private void frmSetupPatientStatementCriteria_Load(object sender, EventArgs e)
        {
            try
            {
                FillFacilities();
                FillChargesTray();
                FillCreditCards();
                FillPaymentTray();
                FillStates("");
                //function call to fill patient name criteria A-Z
                FillPatientNameCriteria();
                
                if (_StatementCriteriaID != 0)
                {
                    PatinetStatementCriteria oPatinetStatementCriteria = new PatinetStatementCriteria(_databaseconnectionstring);
                    if (oPatinetStatementCriteria.GetPatinetStatementCriteria(_StatementCriteriaID))
                    {
                        txtStatementCriteriaName.Text = oPatinetStatementCriteria.StatementCriteriaName.Trim();
                        chkDefault.Checked = oPatinetStatementCriteria.IsDefault;
                        txtPracAddress1.Text = oPatinetStatementCriteria.PracAddress1.Trim();
                        txtPracAddress2.Text = oPatinetStatementCriteria.PracAddress2.Trim();
                        txtPracCity.Text = oPatinetStatementCriteria.PracCity.Trim();
                        
                        cmbPracState.SelectedIndex =cmbPracState.FindStringExact(oPatinetStatementCriteria.PracState);
                        
                        txtPracZip.Text = oPatinetStatementCriteria.PracZip.Trim();

                        //cmbCreditCard.SelectedIndex = cmbCreditCard.FindStringExact(oPatinetStatementCriteria.CreditCard);

                        for (int i = 0; i < trvCreditCard.Nodes.Count; i++)
                        {
                            if (oPatinetStatementCriteria.CreditCard.Contains(trvCreditCard.Nodes[i].Text) == true)
                            {
                                trvCreditCard.Nodes[i].Checked = true;
                            }
                        }

                        txtBillingContactName.Text = oPatinetStatementCriteria.BillingContactName.Trim();
                        txtBillingContactPhone.Text = oPatinetStatementCriteria.BillingContactPhone.Trim();

                        dtpOfficeStartTime.Value =gloDateMaster.gloTime.TimeAsDateTime(dtpOfficeStartTime.Value,Convert.ToInt32(oPatinetStatementCriteria.OfficeStartTime));
                        dtpOfficeEndTime.Value = gloDateMaster.gloTime.TimeAsDateTime(dtpOfficeEndTime.Value, Convert.ToInt32(oPatinetStatementCriteria.OfficeEndTime));

                        txtPracticeTaxID.Text = oPatinetStatementCriteria.PracticeTaxID.Trim();
                        txtRemitName.Text = oPatinetStatementCriteria.RemitName.Trim();
                        txtRemitAddress1.Text = oPatinetStatementCriteria.RemitAddress1.Trim();
                        txtRemitAddress2.Text = oPatinetStatementCriteria.RemitAddress2.Trim();
                        txtRemitCity.Text = oPatinetStatementCriteria.RemitCity.Trim();
                        
                        cmbRemitState.SelectedIndex=cmbRemitState.FindStringExact(oPatinetStatementCriteria.RemitState);

                        txtRemitZip.Text = oPatinetStatementCriteria.RemitZip.Trim();

                        chkPendingInsurance.Checked = oPatinetStatementCriteria.IsPendingInsurance;
                        txtClinicMessage1.Text = oPatinetStatementCriteria.ClinicMessage1.Trim();
                        txtClinicMessage2.Text = oPatinetStatementCriteria.ClinicMessage2.Trim();
                        chkGuarantorIndicator.Checked = oPatinetStatementCriteria.IsGuarantorIndicator;

                        #region "Fill Filter "

                        if (oPatinetStatementCriteria.PatStatementCriteriaFilter != null)
                        {
                            DataTable oBindTableCPT = new DataTable();
                            DataTable oBindTableInsurance = new DataTable();
                            //DataRow oRow;

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
                                            nmWaitFordays.Value  = Convert.ToInt16(dr[2]);
                                        }
                                        break;
                                    case "From":
                                        if (Convert.ToString(dr[2]).Trim() != "")
                                        {
                                            cmbNameFrom .Text = Convert.ToString(dr[2]);
                                        }
                                        break;
                                    case "To":
                                        if (Convert.ToString(dr[2]).Trim() != "")
                                        {
                                            cmbNameTo .Text = Convert.ToString(dr[2]);
                                        }
                                        break;
                                    
                                }
                                //code commented by dipak as code is no linger used

                                //switch (Convert.ToString(dr[0]))
                                //{
                                //    case "Due Amount Grater Then":
                                //        if (Convert.ToInt32(dr[1]) == 1)
                                //        {
                                //            rbGreaterthen.Checked = true;
                                //            txtDueAmount.Text = Convert.ToString(dr[2]);
                                //        }
                                //        break;
                                //    case "Due Amount Less Then":
                                //        if (Convert.ToInt32(dr[1]) == 1)
                                //        {
                                //            rbLessThen.Checked = true;
                                //            txtDueAmount.Text = Convert.ToString(dr[2]);
                                //        }
                                //        break;
                                //    case "CPT":
                                //        if (Convert.ToString(dr[2]).Trim() != "")
                                //        {
                                //            oRow = oBindTableCPT.NewRow();
                                //            oRow[0] = Convert.ToString(dr[2]).Trim();
                                //            oRow[1] = Convert.ToString(dr[3]).Trim();
                                //            oBindTableCPT.Rows.Add(oRow);
                                //        }
                                //        break;
                                //    case "Insurance":
                                //        if (Convert.ToString(dr[1]).Trim() != "")
                                //        {
                                //            oRow = oBindTableInsurance.NewRow();
                                //            oRow[0] = Convert.ToString(dr[1]).Trim();
                                //            oRow[1] = Convert.ToString(dr[3]).Trim();
                                //            oBindTableInsurance.Rows.Add(oRow);
                                //        }
                                //        break;                                    
                                //    case "Charge Tray":
                                //        if (Convert.ToString(dr[1]).Trim() != "")
                                //        {
                                //            cmbChargesTray.SelectedIndex = cmbChargesTray.FindStringExact(Convert.ToString(dr[3]));
                                //        }
                                //        break;
                                //    case "Payment Tray":
                                //        if (Convert.ToString(dr[1]).Trim() != "")
                                //        {
                                //            cmbPaymentTray.SelectedIndex = cmbPaymentTray.FindStringExact(Convert.ToString(dr[3]));
                                //        }
                                //        break;
                                //    case "Facility":
                                //        if (Convert.ToString(dr[1]).Trim() != "")
                                //        {
                                //            cmbFacility.SelectedIndex = cmbFacility.FindStringExact(Convert.ToString(dr[3]));
                                //        }
                                //        break;                                    
                                //    case "Zip Code":
                                //        if (Convert.ToString(dr[2]).Trim() != "")
                                //        {
                                //            txtZipCode.Text = Convert.ToString(dr[2]).Trim();
                                //        }
                                //        break;
                                //}
                                //end code commented by dipak 20091208

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
                            MessageBox.Show("Please enter the patient statement criteria name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtStatementCriteriaName.Select();
                            break;
                        }

                        PatinetStatementCriteria oPatinetStatementCriteria = new PatinetStatementCriteria(_databaseconnectionstring);

                        if (oPatinetStatementCriteria.IsExists(_StatementCriteriaID, txtStatementCriteriaName.Text.Trim()) == true)
                        {
                            MessageBox.Show("Patient Statement Criteria with same name already exists, please enter unique patient statement criteria name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtStatementCriteriaName.Select();
                            break;
                        }

                        oPatinetStatementCriteria.StatementCriteriaID = _StatementCriteriaID;
                        oPatinetStatementCriteria.StatementCriteriaName = txtStatementCriteriaName.Text.Trim();
                        oPatinetStatementCriteria.IsDefault = chkDefault.Checked;

                        oPatinetStatementCriteria.PracAddress1 = txtPracAddress1.Text.Trim();
                        oPatinetStatementCriteria.PracAddress2 = txtPracAddress2.Text.Trim();
                        oPatinetStatementCriteria.PracCity = txtPracCity.Text.Trim();
                        if (cmbPracState.SelectedIndex != 0)
                        {
                            oPatinetStatementCriteria.PracState = cmbPracState.Text;
                        }
                        oPatinetStatementCriteria.PracZip = txtPracZip.Text.Trim();
                        
                        //oPatinetStatementCriteria.CreditCard = cmbCreditCard.Text.Trim();

                        String _sCreditCard = "";
                        for (int i = 0; i < trvCreditCard.Nodes.Count; i++)
                        {
                            if (trvCreditCard.Nodes[i].Checked == true)
                            {
                                if (_sCreditCard == "")
                                {
                                    _sCreditCard = trvCreditCard.Nodes[i].Text.ToString();
                                }
                                else
                                {
                                    _sCreditCard += "," + trvCreditCard.Nodes[i].Text.ToString();
                                }
                            }
                        }
                        oPatinetStatementCriteria.CreditCard = _sCreditCard;

                        
                        oPatinetStatementCriteria.BillingContactName = txtBillingContactName.Text.Trim();
                        oPatinetStatementCriteria.BillingContactPhone = txtBillingContactPhone.Text.Trim();

                        oPatinetStatementCriteria.OfficeStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpOfficeStartTime.Value.ToString("hh:mm tt")); 
                        oPatinetStatementCriteria.OfficeEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpOfficeEndTime.Value.ToString("hh:mm tt")); 

                        oPatinetStatementCriteria.PracticeTaxID = txtPracticeTaxID.Text.Trim();
                        oPatinetStatementCriteria.RemitName = txtRemitName.Text.Trim();
                        oPatinetStatementCriteria.RemitAddress1 = txtRemitAddress1.Text.Trim();
                        oPatinetStatementCriteria.RemitAddress2 = txtRemitAddress2.Text.Trim();
                        oPatinetStatementCriteria.RemitCity = txtRemitCity.Text.Trim();
                        if (cmbPracState.SelectedIndex != 0)
                        {
                            oPatinetStatementCriteria.RemitState = cmbRemitState.Text;
                        }
                        oPatinetStatementCriteria.RemitZip = txtRemitZip.Text.Trim();

                        oPatinetStatementCriteria.IsPendingInsurance = chkPendingInsurance.Checked;
                        oPatinetStatementCriteria.ClinicMessage1 = txtClinicMessage1.Text.Trim();
                        oPatinetStatementCriteria.ClinicMessage2 = txtClinicMessage2.Text.Trim();
                        oPatinetStatementCriteria.IsGuarantorIndicator = chkGuarantorIndicator.Checked;

                        #region " Filter"
                        
                        DataTable dt = new DataTable();

                        DataColumn dc;

                        dc = new DataColumn("CritetiaName");
                        dc.DataType=typeof(System.String);
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
                          //code commented by dipak 20091208 as recordes not used  
                        //dr = dt.NewRow();
                        //dr["CritetiaName"] = "Due Amount Grater Then";
                        //if (rbGreaterthen.Checked == true)
                        //{
                        //    dr["ValueId"] = 1;
                        //    dr["ValueCode"] = txtDueAmount.Text;
                        //    dr["ValueDesc"] = txtDueAmount.Text;
                        //}
                        //else
                        //{
                        //    dr["ValueId"] = 0;
                        //    dr["ValueCode"] = "";
                        //    dr["ValueCode"] = "";
                        //}
                        //dt.Rows.Add(dr);

                        //dr = dt.NewRow();
                        //dr["CritetiaName"] = "Due Amount Less Then";                        
                        //if (rbLessThen.Checked == true)
                        //{
                        //    dr["ValueId"] = 1;
                        //    dr["ValueCode"] = txtDueAmount.Text;
                        //    dr["ValueDesc"] = txtDueAmount.Text;
                        //}
                        //else
                        //{
                        //    dr["ValueId"] = 0;
                        //    dr["ValueCode"] ="";
                        //    dr["ValueCode"] = "";
                        //}
                        //dt.Rows.Add(dr);

                        //for (int i = 0; i < cmbCPT.Items.Count; i++)
                        //{
                        //    cmbCPT.SelectedIndex = i;
                        //    cmbCPT.Refresh();
                        //    //oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsurance.SelectedValue), cmbInsurance.Text);

                        //    dr = dt.NewRow();
                        //    dr["CritetiaName"] = "CPT";
                        //    dr["ValueId"] = "";
                        //    dr["ValueCode"] = cmbCPT.Text;
                        //    dr["ValueDesc"] = cmbCPT.Text;
                        //    dt.Rows.Add(dr);
                        //}

                        //for (int i = 0; i < cmbInsurance.Items.Count; i++)
                        //{
                        //    cmbInsurance.SelectedIndex = i;
                        //    cmbInsurance.Refresh();
                        //    //oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsurance.SelectedValue), cmbInsurance.Text);
                        
                        //    dr = dt.NewRow();
                        //    dr["CritetiaName"] = "Insurance";
                        //    dr["ValueId"] = cmbChargesTray.SelectedValue;
                        //    dr["ValueCode"] = "";
                        //    dr["ValueDesc"] = cmbInsurance.Text;
                        //    dt.Rows.Add(dr);
                        //}

                        //if (cmbChargesTray.SelectedIndex>0)
                        //{
                        //dr = dt.NewRow();
                        //dr["CritetiaName"] = "Charge Tray";
                        //dr["ValueId"] = cmbChargesTray.SelectedValue;                     
                        //dr["ValueCode"] = "";
                        //dr["ValueDesc"] = cmbChargesTray.Text;                                                
                        //dt.Rows.Add(dr);
                        //}

                        //if (cmbPaymentTray.SelectedIndex>0)
                        //{
                        //dr = dt.NewRow();
                        //dr["CritetiaName"] = "Payment Tray";
                        //dr["ValueId"] = cmbPaymentTray.SelectedValue;
                        //dr["ValueCode"] = "";
                        //dr["ValueDesc"] = cmbPaymentTray.Text;                                                
                        //dt.Rows.Add(dr);
                        //}

                        //if (cmbFacility.SelectedIndex>0)
                        //{
                        //    dr = dt.NewRow();
                        //    dr["CritetiaName"] = "Facility";
                        //    dr["ValueId"] = cmbFacility.SelectedValue;
                        //    dr["ValueCode"] = "";
                        //    dr["ValueDesc"] = cmbFacility.Text;
                        //    dt.Rows.Add(dr);
                        //}                        

                        //if (txtZipCode.Text.Trim() != "")
                        //{
                        //    dr = dt.NewRow();
                        //    dr["CritetiaName"] = "Zip Code";
                        //    dr["ValueId"] = "";
                        //    dr["ValueCode"] = txtZipCode.Text.Trim();
                        //    dr["ValueDesc"] = "";
                        //    dt.Rows.Add(dr);
                        //}
                          //end code commented by dipak 120091208

                          //code added by dipak 20091208 
                        dr = dt.NewRow();
                        dr["CritetiaName"] = "Balance";

                        dr["ValueId"] = 0;
                        dr["ValueCode"] = txtDueAmount.Text;
                        dr["ValueDesc"] = txtDueAmount.Text;

                        dr = dt.NewRow();
                        dr["CritetiaName"] = "Wait Days";

                        dr["ValueId"] = 0;
                        //dr["ValueCode"] = txtWaitFordays.Text;
                        //dr["ValueDesc"] = txtWaitFordays.Text;
                        dr["ValueCode"] = nmWaitFordays.Value;
                        dr["ValueDesc"] = nmWaitFordays.Value;  

                        dr = dt.NewRow();
                        dr["CritetiaName"] = "From";

                        dr["ValueId"] = 0;
                        dr["ValueCode"] = cmbNameFrom .Text;
                        dr["ValueDesc"] = cmbNameFrom .Text;
  
                         dr = dt.NewRow();
                        dr["CritetiaName"] = "To";

                        dr["ValueId"] = 0;
                        dr["ValueCode"] = cmbNameTo .Text;
                        dr["ValueDesc"] = cmbNameTo .Text;

                        //end code added by dipak 20091208

                        oPatinetStatementCriteria.PatStatementCriteriaFilter = dt;
                        #endregion

                        oPatinetStatementCriteria.ClinicID = ClinicID;
                        //

                        if (_StatementCriteriaID == 0)
                        {
                            _ReturnStatementCriteriaID = oPatinetStatementCriteria.Add("");
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Add, "Add Patinet Statement Criteria", 0, _ReturnStatementCriteriaID, 0, ActivityOutCome.Success);

                            if (_ReturnStatementCriteriaID < 0)
                            {
                                // Record is Not Added Successfully
                                MessageBox.Show("Patinet Statement Criteria not added, please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtStatementCriteriaName.Select();
                                break;
                            }
                            else
                            {
                                _StatementCriteriaID = _ReturnStatementCriteriaID;
                            }
                        }
                        else
                        {
                            _ReturnStatementCriteriaID = oPatinetStatementCriteria.Add("");
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Modify, "Modify Patinet Statement Criteria", 0, _ReturnStatementCriteriaID, 0, ActivityOutCome.Success);

                            if (_ReturnStatementCriteriaID < 0)
                            {
                                MessageBox.Show("Patinet Statement Criteria not modified, please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                                txtStatementCriteriaName.Select();
                                break;
                            }
                            else
                            {
                                _StatementCriteriaID = _ReturnStatementCriteriaID;
                            }
                        }

                }
                        break;
                    case "Save&Close":
                        {
                            if (txtStatementCriteriaName.Text.Trim() == "")
                            {
                                MessageBox.Show("Please enter the patient statement criteria name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtStatementCriteriaName.Select();
                                break;
                            }

                            PatinetStatementCriteria oPatinetStatementCriteria = new PatinetStatementCriteria(_databaseconnectionstring);

                            if (oPatinetStatementCriteria.IsExists(_StatementCriteriaID, txtStatementCriteriaName.Text.Trim()) == true)
                            {
                                MessageBox.Show("Patient Statement Criteria with same name already exists, please enter unique patient statement criteria name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtStatementCriteriaName.Select();
                                break;
                            }

                            oPatinetStatementCriteria.StatementCriteriaID = _StatementCriteriaID;
                            oPatinetStatementCriteria.StatementCriteriaName = txtStatementCriteriaName.Text.Trim();
                            oPatinetStatementCriteria.IsDefault = chkDefault.Checked;

                            oPatinetStatementCriteria.PracAddress1 = txtPracAddress1.Text.Trim();
                            oPatinetStatementCriteria.PracAddress2 = txtPracAddress2.Text.Trim();
                            oPatinetStatementCriteria.PracCity = txtPracCity.Text.Trim();
                            if (cmbPracState.SelectedIndex != 0)
                            {
                                oPatinetStatementCriteria.PracState = cmbPracState.Text;
                            }
                            oPatinetStatementCriteria.PracZip = txtPracZip.Text.Trim();

                            //oPatinetStatementCriteria.CreditCard = cmbCreditCard.Text.Trim();

                            String _sCreditCard = "";
                            for (int i = 0; i < trvCreditCard.Nodes.Count; i++)
                            {
                                if (trvCreditCard.Nodes[i].Checked == true)
                                {
                                    if (_sCreditCard == "")
                                    {
                                        _sCreditCard = trvCreditCard.Nodes[i].Text.ToString();
                                    }
                                    else
                                    {
                                        _sCreditCard += "," + trvCreditCard.Nodes[i].Text.ToString();
                                    }
                                }
                            }
                            oPatinetStatementCriteria.CreditCard = _sCreditCard;
                            

                            oPatinetStatementCriteria.BillingContactName = txtBillingContactName.Text.Trim();
                            oPatinetStatementCriteria.BillingContactPhone = txtBillingContactPhone.Text.Trim();

                            oPatinetStatementCriteria.OfficeStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpOfficeStartTime.Value.ToString("hh:mm tt"));
                            oPatinetStatementCriteria.OfficeEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpOfficeEndTime.Value.ToString("hh:mm tt"));

                            oPatinetStatementCriteria.PracticeTaxID = txtPracticeTaxID.Text.Trim();
                            oPatinetStatementCriteria.RemitName = txtRemitName.Text.Trim();
                            oPatinetStatementCriteria.RemitAddress1 = txtRemitAddress1.Text.Trim();
                            oPatinetStatementCriteria.RemitAddress2 = txtRemitAddress2.Text.Trim();
                            oPatinetStatementCriteria.RemitCity = txtRemitCity.Text.Trim();
                            if (cmbPracState.SelectedIndex != 0)
                            {
                                oPatinetStatementCriteria.RemitState = cmbRemitState.Text;
                            }
                            oPatinetStatementCriteria.RemitZip = txtRemitZip.Text.Trim();

                            oPatinetStatementCriteria.IsPendingInsurance = chkPendingInsurance.Checked;
                            oPatinetStatementCriteria.ClinicMessage1 = txtClinicMessage1.Text.Trim();
                            oPatinetStatementCriteria.ClinicMessage2 = txtClinicMessage2.Text.Trim();
                            oPatinetStatementCriteria.IsGuarantorIndicator = chkGuarantorIndicator.Checked;

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

                            //code commented by dipak 20091208 as recordes not used  

                            //dr = dt.NewRow();
                            //dr["CritetiaName"] = "Due Amount Grater Then";
                            //if (rbGreaterthen.Checked == true)
                            //{
                            //    dr["ValueId"] = 1;
                            //    dr["ValueCode"] = txtDueAmount.Text;
                            //    dr["ValueDesc"] = txtDueAmount.Text;
                            //}
                            //else
                            //{
                            //    dr["ValueId"] = 0;
                            //    dr["ValueCode"] = "";
                            //    dr["ValueCode"] = "";
                            //}
                            //dt.Rows.Add(dr);

                            //dr = dt.NewRow();
                            //dr["CritetiaName"] = "Due Amount Less Then";
                            //if (rbLessThen.Checked == true)
                            //{
                            //    dr["ValueId"] = 1;
                            //    dr["ValueCode"] = txtDueAmount.Text;
                            //    dr["ValueDesc"] = txtDueAmount.Text;
                            //}
                            //else
                            //{
                            //    dr["ValueId"] = 0;
                            //    dr["ValueCode"] = "";
                            //    dr["ValueCode"] = "";
                            //}
                            //dt.Rows.Add(dr);

                            //for (int i = 0; i < cmbCPT.Items.Count; i++)
                            //{
                            //    cmbCPT.SelectedIndex = i;
                            //    cmbCPT.Refresh();
                            //    //oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsurance.SelectedValue), cmbInsurance.Text);

                            //    dr = dt.NewRow();
                            //    dr["CritetiaName"] = "CPT";
                            //    dr["ValueId"] = "";
                            //    dr["ValueCode"] = cmbCPT.Text;
                            //    dr["ValueDesc"] = cmbCPT.Text;
                            //    dt.Rows.Add(dr);
                            //}

                            //for (int i = 0; i < cmbInsurance.Items.Count; i++)
                            //{
                            //    cmbInsurance.SelectedIndex = i;
                            //    cmbInsurance.Refresh();
                            //    //oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsurance.SelectedValue), cmbInsurance.Text);

                            //    dr = dt.NewRow();
                            //    dr["CritetiaName"] = "Insurance";
                            //    dr["ValueId"] = cmbChargesTray.SelectedValue;
                            //    dr["ValueCode"] = "";
                            //    dr["ValueDesc"] = cmbInsurance.Text;
                            //    dt.Rows.Add(dr);
                            //}

                            //if (cmbChargesTray.SelectedIndex != -1)
                            //{
                            //    dr = dt.NewRow();
                            //    dr["CritetiaName"] = "Charge Tray";
                            //    dr["ValueId"] = cmbChargesTray.SelectedValue;
                            //    dr["ValueCode"] = "";
                            //    dr["ValueDesc"] = cmbChargesTray.Text;
                            //    dt.Rows.Add(dr);
                            //}

                            //if (cmbPaymentTray.SelectedIndex != -1)
                            //{
                            //    dr = dt.NewRow();
                            //    dr["CritetiaName"] = "Payment Tray";
                            //    dr["ValueId"] = cmbPaymentTray.SelectedValue;
                            //    dr["ValueCode"] = "";
                            //    dr["ValueDesc"] = cmbPaymentTray.Text;
                            //    dt.Rows.Add(dr);
                            //}

                            //if (cmbFacility.SelectedIndex != -1)
                            //{
                            //    dr = dt.NewRow();
                            //    dr["CritetiaName"] = "Facility";
                            //    dr["ValueId"] = cmbFacility.SelectedValue;
                            //    dr["ValueCode"] = "";
                            //    dr["ValueDesc"] = cmbFacility.Text;
                            //    dt.Rows.Add(dr);
                            //}
                            

                            //if (txtZipCode.Text.Trim() != "")
                            //{
                            //    dr = dt.NewRow();
                            //    dr["CritetiaName"] = "Zip Code";
                            //    dr["ValueId"] = "";
                            //    dr["ValueCode"] = txtZipCode.Text.Trim();
                            //    dr["ValueDesc"] = "";
                            //    dt.Rows.Add(dr);
                            //}

                            //code added by dipak 20091208 
                            dr = dt.NewRow();
                            dr["CritetiaName"] = "Balance";

                            dr["ValueId"] = 0;
                            dr["ValueCode"] = txtDueAmount.Text;
                            dr["ValueDesc"] = txtDueAmount.Text;
                            dt.Rows.Add(dr);

                            dr = dt.NewRow();
                            dr["CritetiaName"] = "Wait Days";

                            dr["ValueId"] = 0;
                            //dr["ValueCode"] = txtWaitFordays.Text;
                            //dr["ValueDesc"] = txtWaitFordays.Text;
                            dr["ValueCode"] = nmWaitFordays.Value  ;
                            dr["ValueDesc"] = nmWaitFordays.Value  ;
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
                            //end code added by dipak 20091208

                            oPatinetStatementCriteria.PatStatementCriteriaFilter = dt;
                            #endregion

                            oPatinetStatementCriteria.ClinicID = ClinicID;
                            //

                            if (_StatementCriteriaID == 0)
                            {
                                _ReturnStatementCriteriaID = oPatinetStatementCriteria.Add("");
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Add, "Add Patinet Statement Criteria", 0, _ReturnStatementCriteriaID, 0, ActivityOutCome.Success);

                                if (_ReturnStatementCriteriaID < 0)
                                {
                                    // Record is Not Added Successfully
                                    MessageBox.Show("Patinet Statement Criteria not added, please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtStatementCriteriaName.Select();

                                    break;
                                }
                            }
                            else
                            {
                                _ReturnStatementCriteriaID = oPatinetStatementCriteria.Add("");
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Modify, "Modify Patinet Statement Criteria", 0, _ReturnStatementCriteriaID, 0, ActivityOutCome.Success);

                                if (_ReturnStatementCriteriaID < 0)
                                {
                                    MessageBox.Show("Patinet Statement Criteria not modified, please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                    txtStatementCriteriaName.Select();
                                    break;
                                }
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
            
            cmbCPT.DataSource = null;
            cmbCPT.Items.Clear();
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
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        //To fill the City,State,County according to zip Code
        private void txtPracZip_Leave(object sender, EventArgs e)
        {
            if (txtPracZip.Text.Trim() != "" && Regex.IsMatch(txtPracZip.Text.Trim(), @"^[0-9]+$") == true)
            {
                DataTable dt = new System.Data.DataTable();
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    string qry = "SELECT City,ST,County FROM CSZ_MST WITH (NOLOCK) where ZIP = " + txtPracZip.Text.Trim() + "";
                    oDb.Retrive_Query(qry, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cmbPracState.Text = Convert.ToString(dt.Rows[0]["ST"]);

                        if (txtPracCity.Text.Trim() == "")
                            txtPracCity.Text = Convert.ToString(dt.Rows[0]["City"]);

                        //txtPACounty.Text = Convert.ToString(dt.Rows[0]["County"]);
                        //cmbPACountry.Text = "US";
                    }
                    else
                    {
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dt.Dispose();
                    if (oDb != null)
                    {
                        oDb.Disconnect();
                        oDb.Dispose();
                    }
                }
            }
        }


        private void txtRemitZip_KeyPress(object sender, KeyPressEventArgs e)
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
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        //To fill the City,State,County according to zip Code
        private void txtRemitZip_Leave(object sender, EventArgs e)
        {
            if (txtRemitZip.Text.Trim() != "" && Regex.IsMatch(txtPracZip.Text.Trim(), @"^[0-9]+$") == true)
            {
                DataTable dt = new System.Data.DataTable();
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    string qry = "SELECT City,ST,County FROM CSZ_MST WITH (NOLOCK) where ZIP = " + txtRemitZip.Text.Trim() + "";
                    oDb.Retrive_Query(qry, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cmbRemitState.Text = Convert.ToString(dt.Rows[0]["ST"]);

                        if (txtRemitCity.Text.Trim() == "")
                            txtRemitCity.Text = Convert.ToString(dt.Rows[0]["City"]);

                        //txtPACounty.Text = Convert.ToString(dt.Rows[0]["County"]);
                        //cmbPACountry.Text = "US";
                    }
                    else
                    {
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dt.Dispose();
                    if (oDb != null)
                    {
                        oDb.Disconnect();
                        oDb.Dispose();
                    }
                }
            }
        }

        private void trvCreditCard_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                #region  selectAll/DeselectAll
                if (e.Node.Checked == true)
                {

                    int CountNode = 0;
                    for (int i = 0; i < trvCreditCard.Nodes.Count; i++)
                    {
                        if (trvCreditCard.Nodes[i].Checked == true)
                        {
                            CountNode++;
                        }
                    }
                    if (trvCreditCard.Nodes.Count == CountNode)
                    {
                        btnDeSelectCreditCard.Visible = true;
                        btnSelectCreditCard.Visible = false;
                    }


                }
                else
                {

                    int CountNode = 0;
                    for (int i = 0; i < trvCreditCard.Nodes.Count; i++)
                    {
                        if (trvCreditCard.Nodes[i].Checked == false)
                        {
                            CountNode++;
                        }
                    }
                    if (trvCreditCard.Nodes.Count == CountNode)
                    {
                        btnDeSelectCreditCard.Visible = false;
                        btnSelectCreditCard.Visible = true;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnSelectCreditCard_Click(object sender, EventArgs e)
        {
            this.trvCreditCard.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvCreditCard_AfterCheck);
            try
            {
                if (trvCreditCard.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvCreditCard.Nodes.Count; i++)
                    {
                        trvCreditCard.Nodes[i].Checked = true;
                    }
                }
                btnDeSelectCreditCard.Visible = true;
                btnSelectCreditCard.Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.trvCreditCard.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvCreditCard_AfterCheck);                
            }
        }

        private void btnDeSelectCreditCard_Click(object sender, EventArgs e)
        {
            this.trvCreditCard.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvCreditCard_AfterCheck);
            try
            {
                if (trvCreditCard.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvCreditCard.Nodes.Count; i++)
                    {
                        trvCreditCard.Nodes[i].Checked = false;
                    }
                }
                btnDeSelectCreditCard.Visible = false;
                btnSelectCreditCard.Visible = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.trvCreditCard.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvCreditCard_AfterCheck);                
            }
        }


        #endregion


        #region "Fill Methods"
        private void FillStates(String _States)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                DataTable dtStates = new DataTable();
                string _sqlQuery = "SELECT distinct ST FROM CSZ_MST WITH (NOLOCK) order by ST";
                oDB.Retrive_Query(_sqlQuery, out dtStates);
                oDB.Disconnect();

                if (dtStates != null)
                {
                    DataRow dr = dtStates.NewRow();
                    dr["ST"] = "";
                    dtStates.Rows.InsertAt(dr, 0);
                    dtStates.AcceptChanges();

                    cmbPracState.DataSource = dtStates;
                    cmbPracState.DisplayMember = "ST";

                     
                    cmbRemitState.DataSource = dtStates.Copy();
                    cmbRemitState.DisplayMember = "ST";

                }

                

                
                if (_States != "")
                {
                    cmbPracState.Text = _States;
                }



            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

        }

        // For Filling the charges Tray 
        private void FillChargesTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtChargesTray;
            DataTable dtChargesTray = new DataTable();
            try
            {
               // cmbChargesTray.Items.Clear();
                cmbChargesTray.DataSource = null;
                cmbChargesTray.Items.Clear();
                oDB.Connect(false);

                oDB.Retrive_Query(" select nChargeTrayID,sDescription from BL_ChargesTray WITH (NOLOCK) ", out  _dtChargesTray);

                if (_dtChargesTray != null && _dtChargesTray.Rows.Count > 0)
                {
                    dtChargesTray.Columns.Add("nChargeTrayID");
                    dtChargesTray.Columns.Add("sDescription");

                    dtChargesTray.Clear();
                    dtChargesTray.Rows.Add(0, "");

                    for (int i = 0; i < _dtChargesTray.Rows.Count; i++)
                    {
                        dtChargesTray.Rows.Add(_dtChargesTray.Rows[i]["nChargeTrayID"], _dtChargesTray.Rows[i]["sDescription"]);
                    }

                    cmbChargesTray.DataSource = dtChargesTray;
                    cmbChargesTray.DisplayMember = "sDescription";
                    cmbChargesTray.ValueMember = "nChargeTrayID";
                }

                dtChargesTray = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        // For Filling the Payment Tray 
        private void FillPaymentTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtPaymentTray;
            DataTable dtPaymentTray = new DataTable();
            try
            {
              //  cmbPaymentTray.Items.Clear();
                cmbPaymentTray.DataSource = null;
                cmbPaymentTray.Items.Clear();
                oDB.Connect(false);

                oDB.Retrive_Query(" select nCloseDayTrayID,sDescription from BL_CloseDayTray WITH (NOLOCK) ", out  _dtPaymentTray);

                if (_dtPaymentTray != null && _dtPaymentTray.Rows.Count > 0)
                {
                    dtPaymentTray.Columns.Add("nCloseDayTrayID");
                    dtPaymentTray.Columns.Add("sDescription");

                    dtPaymentTray.Clear();
                    dtPaymentTray.Rows.Add(0, "");

                    for (int i = 0; i < _dtPaymentTray.Rows.Count; i++)
                    {
                        dtPaymentTray.Rows.Add(_dtPaymentTray.Rows[i]["nCloseDayTrayID"], _dtPaymentTray.Rows[i]["sDescription"]);
                    }

                    cmbPaymentTray.DataSource = dtPaymentTray;
                    cmbPaymentTray.DisplayMember = "sDescription";
                    cmbPaymentTray.ValueMember = "nCloseDayTrayID";
                }

                dtPaymentTray = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally 
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

         //For Filling The Facilities Combo
        private void FillFacilities()
        {
            try
            {
                DataTable _dtLocations = new DataTable();
                gloFacility ogloFacilities = new gloFacility(_databaseconnectionstring);                
                _dtLocations = ogloFacilities.GetFacilities();

                DataTable dtLocations;
                if (_dtLocations != null && _dtLocations.Rows.Count > 0)
                {
                    dtLocations = new DataTable();
                    dtLocations.Columns.Add("sFacilityCode");
                    dtLocations.Columns.Add("sFacilityName");

                    dtLocations.Clear();
                    dtLocations.Rows.Add(0, "");

                    for (int i = 0; i < _dtLocations.Rows.Count; i++)
                    {
                        dtLocations.Rows.Add(_dtLocations.Rows[i]["sFacilityCode"], _dtLocations.Rows[i]["sFacilityName"]);
                    }

                    cmbFacility.DataSource = dtLocations;
                    cmbFacility.DisplayMember = "sFacilityName";
                    cmbFacility.ValueMember = "sFacilityCode";
                }

                _dtLocations = null;
                ogloFacilities.Dispose();


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //For Filling The CreditCard Type combo
        private void FillCreditCards()
        {
            CreditCards oCreditCards = new CreditCards(_databaseconnectionstring);
            DataTable _dtCards = null;

            try
            {
                //cmbCreditCard.DataSource = null;
                //cmbCreditCard.Items.Clear();
                _dtCards = oCreditCards.GetList();

                //if (_dtCards != null && _dtCards.Rows.Count > 0)
                //{
                //    DataRow _dr = _dtCards.NewRow();
                //    _dr["nCreditCardID"] = 0;
                //    _dr["sCreditCardDesc"] = "";
                //    _dtCards.Rows.InsertAt(_dr, 0);
                //    _dtCards.AcceptChanges();

                //    cmbCreditCard.DataSource = _dtCards.Copy();
                //    cmbCreditCard.ValueMember = _dtCards.Columns[0].ColumnName;
                //    cmbCreditCard.DisplayMember = _dtCards.Columns[1].ColumnName;
                //}
                if (_dtCards != null && _dtCards.Rows.Count > 0)
                {
                    for (int i = 0; i <= _dtCards.Rows.Count - 1; i++)
                    {
                        DataRow dr = _dtCards.Rows[i];
                        TreeNode oNode = new TreeNode();
                        oNode.Tag = Convert.ToString(dr[0]); 
                        oNode.Text = Convert.ToString(dr[1]); ;
                        trvCreditCard.Nodes.Add(oNode);
                        oNode = null;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oCreditCards != null) { oCreditCards.Dispose(); }
                if (_dtCards != null) { _dtCards.Dispose(); }
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

        private void rbGreaterthen_CheckedChanged(object sender, EventArgs e)
        {
            // code commented by dipak 20091208 as no longwer used

            //if (rbGreaterthen.Checked == true)
            //    rbGreaterthen.Font = new Font("Tahoma", 9, FontStyle.Bold);
            //else
            //    rbGreaterthen.Font = new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void rbLessThen_CheckedChanged(object sender, EventArgs e)
        {
            // code commented by dipak 20091208 as no longwer used
            //if (rbLessThen.Checked == true)
            //    rbLessThen.Font = new Font("Tahoma", 9, FontStyle.Bold);
            //else
            //    rbLessThen.Font = new Font("Tahoma", 9, FontStyle.Regular);
        }

        /// <summary>
        /// function fills combo boxes 1)cmbNameFrom 2)cmbNameTo with values A-Z
        /// </summary>
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
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
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