using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using gloAuditTrail;
using gloAddress;
using System.Data.SqlClient;
using gloBilling.Statement;
using gloSettings;

namespace gloBilling
{
    public partial class frmSetupPatientStatementDisplaySettings : Form
    {

        #region " Declarations "
        
        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        private Int64 _StatementCriteriaID = 0;
        private Int64 _ReturnStatementCriteriaID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

       // gloListControl.gloListControl oListControl = null;
       // gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
       // private gloGeneralItem.gloItems ogloItems = null;
        string _DisplayCriteria = "Display";

        private string _ZipCodeType = "";

        private bool _AddressModified = false;
        public bool AddressModified
        {
            get { return _AddressModified; }
            set { _AddressModified = value; }
        }

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


        public bool IsNewStatementDisplaySettings { get; set; }

        #endregion  " Property Procedures "

        #region " Constructor "
        
        public frmSetupPatientStatementDisplaySettings(string databaseconnectionstring )
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

        public frmSetupPatientStatementDisplaySettings(Int64 StatementCriteriaID, string databaseconnectionstring)
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

        private void frmSetupPatientStatementDisplaySettings_Load(object sender, EventArgs e)
        {
            isFormLoading = true;
            Cls_TabIndexSettings tabSettings = null;

            gloC1FlexStyle.Style(c1StatementMessage, false);

            c1StatementMessage.Cols[1].Style.BackColor = System.Drawing.Color.White;
            c1StatementMessage.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
            c1StatementMessage.Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            c1StatementMessage.Cols[1].Style.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);

            c1StatementMessage.SetData(1, 0, "Statement 1 Clinic Message");
            c1StatementMessage.SetData(2, 0, "Statement 2 Clinic Message");
            c1StatementMessage.SetData(3, 0, "Statement 3 Clinic Message");
            c1StatementMessage.SetData(4, 0, "Statement 4 Clinic Message");
            c1StatementMessage.SetData(5, 0, "Statement 5 Clinic Message");

            try
            {
                if (IsNewStatementDisplaySettings)
                { this.Height = 519; }
                FillCreditCards();
                FillStates("");
                //function call to fill patient name criteria A-Z
                PatinetStatementCriteria oPatinetStatementCriteria = new PatinetStatementCriteria(_databaseconnectionstring);
                if (_StatementCriteriaID != 0)
                {
                    if (oPatinetStatementCriteria.GetPatinetStatementCriteria(_StatementCriteriaID))
                    {
                        txtStatementDisplaySettingsName.Text = oPatinetStatementCriteria.StatementCriteriaName.Trim();
                        chkDefault1.Checked = oPatinetStatementCriteria.IsDefault;
                        txtPracAddress1.Text = oPatinetStatementCriteria.PracAddress1.Trim();
                        txtPracAddress2.Text = oPatinetStatementCriteria.PracAddress2.Trim();
                        txtPracCity.Text = oPatinetStatementCriteria.PracCity.Trim();

                        cmbPracState.SelectedIndex = cmbPracState.FindStringExact(oPatinetStatementCriteria.PracState);

                        txtPracZip.Text = oPatinetStatementCriteria.PracZip.Trim();

                        for (int i = 0; i < trvCreditCard.Nodes.Count; i++)
                        {
                            if (oPatinetStatementCriteria.CreditCard.Contains(trvCreditCard.Nodes[i].Text) == true)
                            {
                                trvCreditCard.Nodes[i].Checked = true;
                            }
                        }

                        txtBillingContactName.Text = oPatinetStatementCriteria.BillingContactName.Trim();
                        txtBillingContactPhone.Text = oPatinetStatementCriteria.BillingContactPhone.Trim();
                        txtBillingURL.Text = oPatinetStatementCriteria.BillingURL.Trim();
                        txtBillingEmail.Text = oPatinetStatementCriteria.BillingEmail.Trim();
                        dtpOfficeStartTime.Value = gloDateMaster.gloTime.TimeAsDateTime(dtpOfficeStartTime.Value, Convert.ToInt32(oPatinetStatementCriteria.OfficeStartTime));
                        dtpOfficeEndTime.Value = gloDateMaster.gloTime.TimeAsDateTime(dtpOfficeEndTime.Value, Convert.ToInt32(oPatinetStatementCriteria.OfficeEndTime));

                        if (oPatinetStatementCriteria.PayableTo == (Int64)PaymentAddressType.OtherAddress)
                        {
                            rbOtherAddress.Checked = true;
                            pnlOther.Visible = true;
                           // this.Height = this.Height + 150;
                        }
                        else if (oPatinetStatementCriteria.PayableTo == (Int64)PaymentAddressType.RemitAddress)
                        {
                            rbRemitAddress.Checked = true;
                            rbRemitAddress.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                            this.Height = this.Height - 138;
                        }
                        else
                        {
                            rbBillingProvider.Checked = true;
                            this.Height = this.Height - 138;
                        }

                        if (oPatinetStatementCriteria.RemitTo == (Int64)RemitAddressType.OtherAddress)
                        {
                            rbRemitOtherAddress.Checked = true;
                            pnlRemitAddress.Visible = true;
                            // this.Height = this.Height + 150;
                        }
                        else if (oPatinetStatementCriteria.RemitTo == (Int64)RemitAddressType.PateintProviderAddress)
                        {
                            rbRemitBillingProvider.Checked = true;
                            rbRemitBillingProvider.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                            this.Height = this.Height - 141;
                        }
                        else
                        {
                            rbRemitBillingProvider.Checked = true;
                            this.Height = this.Height - 141;
                        }

                        txtPracticeTaxID.Text = oPatinetStatementCriteria.PracticeTaxID.Trim();
                        txtRemitName.Text = oPatinetStatementCriteria.RemitName.Trim();
                        txtRemitAddress1.Text = oPatinetStatementCriteria.RemitAddress1.Trim();
                        txtRemitAddress2.Text = oPatinetStatementCriteria.RemitAddress2.Trim();
                        txtRemitCity.Text = oPatinetStatementCriteria.RemitCity.Trim();

                        //cmbRemitState.SelectedIndex = cmbRemitState.FindStringExact(oPatinetStatementCriteria.RemitState);
                        cmbRemitState.Text = oPatinetStatementCriteria.RemitState;

                        txtRemitZip.Text = oPatinetStatementCriteria.RemitZip.Trim();

                        txtOtherName.Text = oPatinetStatementCriteria.OtherName.Trim();
                        txtOtherAddress1.Text = oPatinetStatementCriteria.OtherAddress1.Trim();
                        txtOtherAddress2.Text = oPatinetStatementCriteria.OtherAddress2.Trim();
                        txtOtherCity.Text = oPatinetStatementCriteria.OtherCity.Trim();

                       // cmbOtherState.SelectedIndex = cmbOtherState.FindStringExact(oPatinetStatementCriteria.OtherState);
                        cmbOtherState.Text = oPatinetStatementCriteria.OtherState;

                        txtOtherZip.Text = oPatinetStatementCriteria.OtherZip.Trim();

                        chkPendingInsurance.Checked = oPatinetStatementCriteria.IsPendingInsurance;
                        txtClinicMessage1.Text = oPatinetStatementCriteria.ClinicMessage1.Trim();
                        txtClinicMessage2.Text = oPatinetStatementCriteria.ClinicMessage2.Trim();
                        chkGuarantorIndicator.Checked = oPatinetStatementCriteria.IsGuarantorIndicator;
                        chkPaymentRemit.Checked = oPatinetStatementCriteria.IsIncludeInsuranceRemit;
                        chkIncludeClaim.Checked = oPatinetStatementCriteria.IsIncludeClaim;
                        txtDetachInstructions.Text = oPatinetStatementCriteria.DetachandReturnInstructions.Trim();
                        chkIncludeonEachStatement.Checked = oPatinetStatementCriteria.bIsIncludeOnEveryStatement;

                        #region "Set value on statement message grid"

                        c1StatementMessage.SetData(1, 1, oPatinetStatementCriteria.StatementClinicMsg1);
                        c1StatementMessage.SetData(2, 1, oPatinetStatementCriteria.StatementClinicMsg2);
                        c1StatementMessage.SetData(3, 1, oPatinetStatementCriteria.StatementClinicMsg3);
                        c1StatementMessage.SetData(4, 1, oPatinetStatementCriteria.StatementClinicMsg4);
                        c1StatementMessage.SetData(5, 1, oPatinetStatementCriteria.StatementClinicMsg5);

                        #endregion
                    }
                    oPatinetStatementCriteria.Dispose();
                }

                tabSettings = new Cls_TabIndexSettings(this);
                tabSettings.SetTabOrder(Cls_TabIndexSettings.TabScheme.AcrossFirst);


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
            finally
            {
                isFormLoading = false;
                if (tabSettings != null) { tabSettings = null; }
            }
           

        }
       

        #endregion " Form Load "

        
        #region "User Control Events"
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


        //private void txtRemitZip_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //    try
        //    {
        //        //_ZipTextType = enumZipTextType.PatientZip;
        //        if (e.KeyChar == Convert.ToChar(13))
        //        {
        //            //' HITS ENTER BUTTON ''
        //            if (pnlInternalControl.Visible)
        //            {

        //                oZipcontrol_ItemSelected(null, null);
        //            }
        //        }
        //        else if (e.KeyChar == Convert.ToChar(27))
        //        {
        //            //' HITS ESCAPE ''

        //            if (txtRemitCity.Text == "" && txtRemitZip.Text == "")
        //            //if ( txtPAZip.Text == "")
        //            {
        //                _TempZipText = txtRemitZip.Text;

        //            }
        //            txtRemitCity.Focus();
        //        }

        //        if (!(e.KeyChar == Convert.ToChar(8)))
        //        {
        //            if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
        //            {
        //                e.Handled = true;
        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }

        //}

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

        //private void txtOtherZip_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //    try
        //    {
        //        //_ZipTextType = enumZipTextType.PatientZip;
        //        if (e.KeyChar == Convert.ToChar(13))
        //        {
        //            //' HITS ENTER BUTTON ''
        //            if (pnlInternalZipControl.Visible)
        //            {

        //                oZipcontrol_ItemSelected(null, null);
        //            }
        //        }
        //        else if (e.KeyChar == Convert.ToChar(27))
        //        {
        //            //' HITS ESCAPE ''

        //            if (txtOtherCity.Text == "" && txtOtherZip.Text == "")
        //            //if ( txtPAZip.Text == "")
        //            {
        //                _TempOtherZipText = txtOtherZip.Text;

        //            }
        //            txtOtherCity.Focus();
        //        }

        //        if (!(e.KeyChar == Convert.ToChar(8)))
        //        {
        //            if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
        //            {
        //                e.Handled = true;
        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }

        //}

        //To fill the City,State,County according to zip Code
        private void txtOtherZip_Leave(object sender, EventArgs e)
        {
            if (txtOtherZip.Text.Trim() != "" && Regex.IsMatch(txtOtherZip.Text.Trim(), @"^[0-9]+$") == true)
            {
                DataTable dt = new System.Data.DataTable();
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    string qry = "SELECT City,ST,County FROM CSZ_MST WITH (NOLOCK) where ZIP = " + txtOtherZip.Text.Trim() + "";
                    oDb.Retrive_Query(qry, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cmbOtherState.Text = Convert.ToString(dt.Rows[0]["ST"]);

                        if (txtOtherCity.Text.Trim() == "")
                            txtOtherCity.Text = Convert.ToString(dt.Rows[0]["City"]);

                       
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


                    cmbRemitState.DataSource = dtStates.Copy(); ;
                    cmbRemitState.DisplayMember = "ST";

                    //***********

                    cmbOtherState.DataSource = dtStates.Copy(); ;
                    cmbOtherState.DisplayMember = "ST";

                    //************
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

        #region " Tool Strip Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        {
                            c1StatementMessage.FinishEditing();
                            //Validate Phone No
                            if (txtBillingContactPhone.Text.Length != 10 && txtBillingContactPhone.Text.Length != 0)
                            {
                                MessageBox.Show("Phone details are incomplete.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtBillingContactPhone.Select();
                                break;
                            }

                            if (txtStatementDisplaySettingsName.Text.Trim() == "")
                            {
                                MessageBox.Show("Please enter the Patient Statement Display Settings.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtStatementDisplaySettingsName.Select();
                                break;
                            }
                            if ((!string.IsNullOrEmpty(txtBillingURL.Text.Trim())))
                            {
                                //If Regex.IsMatch(txtURL.Text.Trim(), "^(((ht|f){1}((tp|tps):[/][/]){1})|((www.){1}))[-a-zA-Z0-9@:%_\+.~#?&//=]+$") = False Then
                                if ((GeneralSettings.ValidateURLAddress(txtBillingURL.Text.Trim()) == false))
                                {
                                    MessageBox.Show("Please enter a valid url.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtBillingURL.Focus();
                                    break;
                                }
                            }

                            if (CheckEmailAddress(txtBillingEmail.Text) == false)
                            {
                                MessageBox.Show("Enter a valid email id.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtBillingEmail.Focus();
                                break;
                            }

                            PatinetStatementCriteria oPatinetStatementCriteria = new PatinetStatementCriteria(_databaseconnectionstring);

                            if (oPatinetStatementCriteria.IsExists(_StatementCriteriaID, txtStatementDisplaySettingsName.Text.Trim(), _DisplayCriteria) == true)
                            {
                                MessageBox.Show("Patient Statement Display Settings with same name already exists, please enter unique Patient Statement Display Settings.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtStatementDisplaySettingsName.Select();
                                break;
                            }


                            oPatinetStatementCriteria.StatementCriteriaID = _StatementCriteriaID;
                            oPatinetStatementCriteria.StatementCriteriaName = txtStatementDisplaySettingsName.Text.Trim();
                            oPatinetStatementCriteria.IsDefault = chkDefault1.Checked;
                            oPatinetStatementCriteria.CriteriaType = "Display";

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


                            if (txtRemitZip.Focused == true)
                            {
                                txtStatementDisplaySettingsName.Select();
                            }

                            oPatinetStatementCriteria.CreditCard = _sCreditCard;


                            oPatinetStatementCriteria.BillingContactName = txtBillingContactName.Text.Trim();
                            oPatinetStatementCriteria.BillingContactPhone = txtBillingContactPhone.Text.Trim();
                            oPatinetStatementCriteria.BillingURL = txtBillingURL.Text.Trim();
                            oPatinetStatementCriteria.BillingEmail = txtBillingEmail.Text.Trim();
                            oPatinetStatementCriteria.OfficeStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpOfficeStartTime.Value.ToString("hh:mm tt"));
                            oPatinetStatementCriteria.OfficeEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpOfficeEndTime.Value.ToString("hh:mm tt"));

                            oPatinetStatementCriteria.PracticeTaxID = txtPracticeTaxID.Text.Trim();
                            oPatinetStatementCriteria.RemitName = txtRemitName.Text.Trim();
                            oPatinetStatementCriteria.RemitAddress1 = txtRemitAddress1.Text.Trim();
                            oPatinetStatementCriteria.RemitAddress2 = txtRemitAddress2.Text.Trim();
                            oPatinetStatementCriteria.RemitCity = txtRemitCity.Text.Trim();
                            if (cmbRemitState.SelectedIndex != 0)
                            {
                                oPatinetStatementCriteria.RemitState = cmbRemitState.Text;
                            }
                            else if (Convert.ToString(cmbRemitState.Text) != "" && cmbRemitState.SelectedIndex > 0)
                            {
                                oPatinetStatementCriteria.RemitState = cmbRemitState.Text;
                            }
                            oPatinetStatementCriteria.RemitZip = txtRemitZip.Text.Trim();



                            if (txtOtherZip.Focused == true)
                            {
                                txtStatementDisplaySettingsName.Select();
                            }


                            oPatinetStatementCriteria.OtherName = txtOtherName.Text.Trim();
                            oPatinetStatementCriteria.OtherAddress1 = txtOtherAddress1.Text.Trim();
                            oPatinetStatementCriteria.OtherAddress2 = txtOtherAddress2.Text.Trim();
                            oPatinetStatementCriteria.OtherCity = txtOtherCity.Text.Trim();
                           
                            if (cmbOtherState.SelectedIndex != 0)
                            {
                                oPatinetStatementCriteria.OtherState = cmbOtherState.Text;
                            }

                            else if (Convert.ToString(cmbOtherState.Text) != "" && cmbOtherState.SelectedIndex > 0)
                            {
                                oPatinetStatementCriteria.OtherState = cmbOtherState.Text;
                            }

                            oPatinetStatementCriteria.OtherZip = txtOtherZip.Text.Trim();

                            if (rbBillingProvider.Checked)
                            {
                                oPatinetStatementCriteria.PayableTo =(Int64)PaymentAddressType.PateintProviderAddress;
                            }
                            else if (rbOtherAddress.Checked)
                            {
                                oPatinetStatementCriteria.PayableTo = (Int64)PaymentAddressType.OtherAddress;
                            }
                            else if (rbRemitAddress.Checked)
                            {
                                oPatinetStatementCriteria.PayableTo = (Int64)PaymentAddressType.RemitAddress ;
                            }

                            if (rbRemitBillingProvider.Checked)
                            {
                                oPatinetStatementCriteria.RemitTo = (Int32)RemitAddressType.PateintProviderAddress;
                            }
                            else if (rbRemitOtherAddress.Checked)
                            {
                                oPatinetStatementCriteria.RemitTo = (Int32)RemitAddressType.OtherAddress;
                            }
                           



                            oPatinetStatementCriteria.IsPendingInsurance = chkPendingInsurance.Checked;
                            oPatinetStatementCriteria.ClinicMessage1 = txtClinicMessage1.Text.Trim();
                            oPatinetStatementCriteria.ClinicMessage2 = txtClinicMessage2.Text.Trim();
                            oPatinetStatementCriteria.IsGuarantorIndicator = chkGuarantorIndicator.Checked;


							if (c1StatementMessage.Rows.Count > 0)
                            {
                                if (c1StatementMessage.GetData(1, 1) != null && c1StatementMessage.GetData(1, 1).ToString() != "")
                                {
                                    oPatinetStatementCriteria.StatementClinicMsg1 = c1StatementMessage.GetData(1, 1).ToString().Trim();
                                }
                                if (c1StatementMessage.GetData(2, 1) != null && c1StatementMessage.GetData(2, 1).ToString() != "")
                                {
                                    oPatinetStatementCriteria.StatementClinicMsg2 = c1StatementMessage.GetData(2, 1).ToString().Trim();
                                }
                                if (c1StatementMessage.GetData(3, 1) != null && c1StatementMessage.GetData(3, 1).ToString() != "")
                                {
                                    oPatinetStatementCriteria.StatementClinicMsg3 = c1StatementMessage.GetData(3, 1).ToString().Trim();
                                }
                                if (c1StatementMessage.GetData(4, 1) != null && c1StatementMessage.GetData(4, 1).ToString() != "")
                                {
                                    oPatinetStatementCriteria.StatementClinicMsg4 = c1StatementMessage.GetData(4, 1).ToString().Trim();
                                }
                                if (c1StatementMessage.GetData(5, 1) != null && c1StatementMessage.GetData(5, 1).ToString() != "")
                                {
                                    oPatinetStatementCriteria.StatementClinicMsg5 = c1StatementMessage.GetData(5, 1).ToString().Trim();
                                }
                            }

                            oPatinetStatementCriteria.DetachandReturnInstructions = txtDetachInstructions.Text.Trim();
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

                            oPatinetStatementCriteria.PatStatementCriteriaFilter = dt;
                            #endregion

                            oPatinetStatementCriteria.ClinicID = ClinicID;
                            //

                            if (_StatementCriteriaID == 0)
                            {
                                _ReturnStatementCriteriaID = oPatinetStatementCriteria.Add("Display");
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Add, "Add Patient Statement Display Settings", 0, _ReturnStatementCriteriaID, 0, ActivityOutCome.Success);

                                if (_ReturnStatementCriteriaID < 0)
                                {
                                    MessageBox.Show("Patient Statement Display Settings not added, please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtStatementDisplaySettingsName.Select();

                                    break;
                                }
                            }
                            else
                            {
                                _ReturnStatementCriteriaID = oPatinetStatementCriteria.Add("Display");
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Modify, "Modify Patient Statement Display Settings", 0, _ReturnStatementCriteriaID, 0, ActivityOutCome.Success);

                                if (_ReturnStatementCriteriaID < 0)
                                {
                                    MessageBox.Show("Patient Statement Display Settings not modified, please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                    txtStatementDisplaySettingsName.Select();
                                    break;
                                }
                            }

                            txtStatementDisplaySettingsName.Text = "";
                            txtStatementDisplaySettingsName.Select();

                        }
                        break;
                    case "Save&Close":
                        {
                            c1StatementMessage.FinishEditing();
                            //Validate Phone No
                            if (txtBillingContactPhone.Text.Length != 10 && txtBillingContactPhone.Text.Length != 0)
                            {
                                MessageBox.Show("Phone details are incomplete.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtBillingContactPhone.Select();
                                break;
                            }

                            if (txtStatementDisplaySettingsName.Text.Trim() == "")
                            {
                                MessageBox.Show("Please enter the Patient Statement Display Settings.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtStatementDisplaySettingsName.Select();
                                break;
                            }

                            if ((!string.IsNullOrEmpty(txtBillingURL.Text.Trim())))
                            {
                                //If Regex.IsMatch(txtURL.Text.Trim(), "^(((ht|f){1}((tp|tps):[/][/]){1})|((www.){1}))[-a-zA-Z0-9@:%_\+.~#?&//=]+$") = False Then
                                if ((GeneralSettings.ValidateURLAddress(txtBillingURL.Text.Trim()) == false))
                                {
                                    MessageBox.Show("Please enter a valid url.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtBillingURL.Focus();
                                    break;
                                }
                            }

                            if (CheckEmailAddress(txtBillingEmail.Text) == false)
                            {
                                MessageBox.Show("Enter a valid email id.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtBillingEmail.Focus();
                                break;
                            }

                            PatinetStatementCriteria oPatinetStatementCriteria = new PatinetStatementCriteria(_databaseconnectionstring);

                            if (oPatinetStatementCriteria.IsExists(_StatementCriteriaID, txtStatementDisplaySettingsName.Text.Trim(), _DisplayCriteria) == true)
                            {
                                MessageBox.Show("Patient Statement Display Settings with same name already exists, please enter unique Patient Statement Display Settings name.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtStatementDisplaySettingsName.Select();
                                break;
                            }


                           oPatinetStatementCriteria.StatementCriteriaID = _StatementCriteriaID;
                            oPatinetStatementCriteria.StatementCriteriaName = txtStatementDisplaySettingsName.Text.Trim();
                            oPatinetStatementCriteria.IsDefault = chkDefault1.Checked;
                            oPatinetStatementCriteria.CriteriaType = "Display";

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


                            if (txtRemitZip.Focused == true)
                            {
                                txtStatementDisplaySettingsName.Select();
                            }

                            oPatinetStatementCriteria.CreditCard = _sCreditCard;


                            oPatinetStatementCriteria.BillingContactName = txtBillingContactName.Text.Trim();
                            oPatinetStatementCriteria.BillingContactPhone = txtBillingContactPhone.Text.Trim();
                            oPatinetStatementCriteria.BillingURL = txtBillingURL.Text.Trim();
                            oPatinetStatementCriteria.BillingEmail = txtBillingEmail.Text.Trim();
                            oPatinetStatementCriteria.OfficeStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpOfficeStartTime.Value.ToString("hh:mm tt"));
                            oPatinetStatementCriteria.OfficeEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpOfficeEndTime.Value.ToString("hh:mm tt"));

                            oPatinetStatementCriteria.PracticeTaxID = txtPracticeTaxID.Text.Trim();
                            oPatinetStatementCriteria.RemitName = txtRemitName.Text.Trim();
                            oPatinetStatementCriteria.RemitAddress1 = txtRemitAddress1.Text.Trim();
                            oPatinetStatementCriteria.RemitAddress2 = txtRemitAddress2.Text.Trim();
                            oPatinetStatementCriteria.RemitCity = txtRemitCity.Text.Trim();

                            if (cmbRemitState.SelectedIndex != 0)
                            {
                                oPatinetStatementCriteria.RemitState = cmbRemitState.Text;
                            }
                            else if (Convert.ToString(cmbRemitState.Text) != "" && cmbRemitState.SelectedIndex > 0)
                            {
                                oPatinetStatementCriteria.RemitState = cmbRemitState.Text;
                            }

                            oPatinetStatementCriteria.RemitZip = txtRemitZip.Text.Trim();


                            if (txtOtherZip.Focused == true)
                            {
                                txtStatementDisplaySettingsName.Select();
                            }


                            oPatinetStatementCriteria.OtherName = txtOtherName.Text.Trim();
                            oPatinetStatementCriteria.OtherAddress1 = txtOtherAddress1.Text.Trim();
                            oPatinetStatementCriteria.OtherAddress2 = txtOtherAddress2.Text.Trim();
                            oPatinetStatementCriteria.OtherCity = txtOtherCity.Text.Trim();
                            if (cmbOtherState.SelectedIndex != 0)
                            {
                                oPatinetStatementCriteria.OtherState = cmbOtherState.Text;
                            }

                            else if (Convert.ToString(cmbOtherState.Text) != "" && cmbOtherState.SelectedIndex > 0)
                            {
                                oPatinetStatementCriteria.OtherState = cmbOtherState.Text;
                            }

                            oPatinetStatementCriteria.OtherZip = txtOtherZip.Text.Trim();

                            if (rbBillingProvider.Checked)
                            {
                                oPatinetStatementCriteria.PayableTo = (Int64)PaymentAddressType.PateintProviderAddress;
                            }
                            else if (rbOtherAddress.Checked)
                            {
                                oPatinetStatementCriteria.PayableTo = (Int64)PaymentAddressType.OtherAddress;
                            }
                            else if (rbRemitAddress.Checked)
                            {
                                oPatinetStatementCriteria.PayableTo = (Int64)PaymentAddressType.RemitAddress;
                            }

                            if (rbRemitBillingProvider.Checked)
                            {
                                oPatinetStatementCriteria.RemitTo = (Int32)RemitAddressType.PateintProviderAddress;
                            }
                            else if (rbRemitOtherAddress.Checked)
                            {
                                oPatinetStatementCriteria.RemitTo = (Int32)RemitAddressType.OtherAddress;
                            }

                            oPatinetStatementCriteria.IsPendingInsurance = chkPendingInsurance.Checked;
                            oPatinetStatementCriteria.ClinicMessage1 = txtClinicMessage1.Text.Trim();
                            oPatinetStatementCriteria.ClinicMessage2 = txtClinicMessage2.Text.Trim();
                            oPatinetStatementCriteria.bIsIncludeOnEveryStatement = txtClinicMessage1.Text.Trim() !="" ? chkIncludeonEachStatement.Checked :false ;
							if (c1StatementMessage.Rows.Count > 0)
                            {
                                if (c1StatementMessage.GetData(1, 1) != null && c1StatementMessage.GetData(1, 1).ToString() != "")
                                {
                                    oPatinetStatementCriteria.StatementClinicMsg1 = c1StatementMessage.GetData(1, 1).ToString().Trim();
                                }
                                if (c1StatementMessage.GetData(2, 1) != null && c1StatementMessage.GetData(2, 1).ToString() != "")
                                {
                                    oPatinetStatementCriteria.StatementClinicMsg2 = c1StatementMessage.GetData(2, 1).ToString().Trim();
                                }
                                if (c1StatementMessage.GetData(3, 1) != null && c1StatementMessage.GetData(3, 1).ToString() != "")
                                {
                                    oPatinetStatementCriteria.StatementClinicMsg3 = c1StatementMessage.GetData(3, 1).ToString().Trim();
                                }
                                if (c1StatementMessage.GetData(4, 1) != null && c1StatementMessage.GetData(4, 1).ToString() != "")
                                {
                                    oPatinetStatementCriteria.StatementClinicMsg4 = c1StatementMessage.GetData(4, 1).ToString().Trim();
                                }
                                if (c1StatementMessage.GetData(5, 1) != null && c1StatementMessage.GetData(5, 1).ToString() != "")
                                {
                                    oPatinetStatementCriteria.StatementClinicMsg5 = c1StatementMessage.GetData(5, 1).ToString().Trim();
                                }
                            }

                            oPatinetStatementCriteria.DetachandReturnInstructions = txtDetachInstructions.Text.Trim();
                            oPatinetStatementCriteria.IsGuarantorIndicator = chkGuarantorIndicator.Checked;
                            oPatinetStatementCriteria.IsIncludeInsuranceRemit = chkPaymentRemit.Checked;
                            oPatinetStatementCriteria.IsIncludeClaim = chkIncludeClaim.Checked;
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

                            oPatinetStatementCriteria.PatStatementCriteriaFilter = dt;
                            #endregion

                            oPatinetStatementCriteria.ClinicID = ClinicID;
                            //

                            if (_StatementCriteriaID == 0)
                            {
                                _ReturnStatementCriteriaID = oPatinetStatementCriteria.Add("Display");
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Add, "Add Patient Statement Display Settings", 0, _ReturnStatementCriteriaID, 0, ActivityOutCome.Success);

                                if (_ReturnStatementCriteriaID < 0)
                                {
                                    MessageBox.Show("Patient Statement Display Settings not added, please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtStatementDisplaySettingsName.Select();

                                    break;
                                }
                            }
                            else
                            {
                                _ReturnStatementCriteriaID = oPatinetStatementCriteria.Add("Display");
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Modify, "Modify Patient Statement Display Settings", 0, _ReturnStatementCriteriaID, 0, ActivityOutCome.Success);

                                if (_ReturnStatementCriteriaID < 0)
                                {
                                    MessageBox.Show("Patient Statement Display Settings with same name already exists, please enter unique Patient Statement Display Settings name.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                    txtStatementDisplaySettingsName.Select();
                                    break;
                                }
                            }

                            txtStatementDisplaySettingsName.Text = "";
                            txtStatementDisplaySettingsName.Select();
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

        #region " ZIP Control Implemented "

        bool isFormLoading = false;

        private gloZipcontrol oZipcontrol;
      //  private bool isSearchControlOpen = false;
        //enumZipTextType _ZipTextType;
        private string _TempZipText;
        private string _TempOtherZipText;
        private bool _isZipItemSelected = false;
        private bool _isTextBoxLoading = false;
        private ToolTip oToolTip = new ToolTip();

        private void txtRemitZip_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //***********************
                {
                    string _strRegex = "";
                    //'Allow digits only if country is US 
                    //if ((_Country == "US"))
                    //{
                    //    _strRegex = "[0-9]";
                    //}
                    //else
                    //{
                        //'allow alphanumerics if country is Canada 
                        _strRegex = "^([0-9a-zA-Z])";
               //     }
                    string strZipcode = txtRemitZip.Text;
                    foreach (char c in strZipcode)
                    {
                        if (Regex.IsMatch(c.ToString(), _strRegex) == false)
                        {


                            strZipcode = strZipcode.Replace(c.ToString(), "");
                        }
                    }
                    txtRemitZip.Text = strZipcode;
                }

                //**************************

                _ZipCodeType = PaymentAddressType.RemitAddress.GetHashCode().ToString();

                pnlInternalControl.BringToFront();

                if (isFormLoading == false & _isTextBoxLoading == false)
                {
                    if (_ZipCodeType == PaymentAddressType.RemitAddress.GetHashCode().ToString())
                    {

                        if (pnlInternalControl.Visible == false)
                        {
                            pnlInternalControl.Visible = true;
                            OpenInternalControl(gloAddress.gloGridListControlType.ZIP, "Zip", false, 0, 0, "");
                            oZipcontrol.FillControl(Convert.ToString(txtRemitZip.Text.Trim()));
                        }
                        else
                        {
                            oZipcontrol.FillControl(Convert.ToString(txtRemitZip.Text.Trim()));
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                //_TempZipText = txtPAZip.Text;
                //if (isFormLoading == false)
                //    _AddressModified = true;
            }
        }

        public bool OpenInternalControl(gloAddress.gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            _isZipItemSelected = false;
            try
            {

                if (oZipcontrol != null)
                {
                    CloseInternalControl();
                }
                oZipcontrol = new gloZipcontrol(ControlType, false, 0, 0, 0, _databaseconnectionstring);
                oZipcontrol.ItemSelectedclick += oZipcontrol_ItemSelected;
                //oZipcontrol.InternalGridKeyDownclick += oZipcontrol_InternalGridKeyDown;
                
                oZipcontrol.ControlHeader = ControlHeader;
                oZipcontrol.ShowHeader = false;

                oZipcontrol.Dock = DockStyle.Fill;

                if (_ZipCodeType == PaymentAddressType.RemitAddress.GetHashCode().ToString())
                {
                    pnlInternalControl.BringToFront();
                    pnlInternalControl.Visible = true;
                    pnlInternalControl.Controls.Add(oZipcontrol);
                }
                else if (_ZipCodeType == PaymentAddressType.OtherAddress.GetHashCode().ToString())
                {
                    pnlInternalZipControl.BringToFront();
                    pnlInternalZipControl.Visible = true;
                    pnlInternalZipControl.Controls.Add(oZipcontrol);

                }
                if (!string.IsNullOrEmpty(SearchText))
                {
                    oZipcontrol.Search(SearchText, gloAddress.SearchColumn.Code);
                }
                oZipcontrol.Show();
                _result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = false;
            }
            finally
            {

            }

            //isSearchControlOpen = true;
            return _result;
        }

        private bool CloseInternalControl()
        {
            if (oZipcontrol != null)
            {

                _isTextBoxLoading = true;
                //SLR: Changed on 4/4/2014
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0;  i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }

                if (oZipcontrol != null)
                {
                    try
                    {
                        oZipcontrol.ItemSelectedclick -= oZipcontrol_ItemSelected;
                    }
                    catch { }

                    oZipcontrol.Dispose();
                    oZipcontrol = null;
                }


                _isTextBoxLoading = false;

            }
            return _isTextBoxLoading;
        }

        private void oZipcontrol_ItemSelected(object sender, EventArgs e)
        {
            try
            {
                if (oZipcontrol.C1GridList.Row < 0)
                {
                    return;
                }
                string _Zip = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 0).ToString();
                string _City = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 1).ToString();
                string _ID = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 2).ToString();
                //string _County = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 3).ToString();
                string _State = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 4).ToString();
                string _AreaCode = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 5).ToString();

                _isTextBoxLoading = true;

                if (_ZipCodeType == PaymentAddressType.RemitAddress.GetHashCode().ToString())
                {
                    txtRemitZip.Text = _Zip;
                    txtRemitZip.Tag = _ID;
                    txtRemitCity.Text = _City;
                    txtRemitCity.Tag = _AreaCode;

                    cmbRemitState.Text = _State;

                    if (pnlInternalControl.Visible == true)
                    {
                        pnlInternalControl.Visible = false;
                        txtRemitCity.Focus();

                      
                    }

                }
                else if (_ZipCodeType == PaymentAddressType.OtherAddress.GetHashCode().ToString())
                {

                    ///***********
                    txtOtherZip.Text = _Zip;
                    txtOtherZip.Tag = _ID;
                    txtOtherCity.Text = _City;
                    txtOtherCity.Tag = _AreaCode;

                    cmbOtherState.Text = _State;

                    if (pnlInternalZipControl.Visible == true)
                    {
                        pnlInternalZipControl.Visible = false;
                        txtOtherCity.Focus();
 
                    }
                }
                ///***********

                _isTextBoxLoading = false;
                _isZipItemSelected = true;

                

                //isSearchControlOpen = false;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void oZipcontrol_InternalGridKeyDown(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CloseInternalControl();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //    }
        //}

        //private void txtZip_LostFocus(object sender, System.EventArgs e)
        //{
        //    if (oZipcontrol != null)
        //    {
        //        if (_isZipItemSelected == false & oZipcontrol.C1GridList.Focused == false & oZipcontrol.Focused == false)
        //        {
        //            _isTextBoxLoading = true;
        //            //txtRemitZip.Text = _TempZipText;
        //            if (txtRemitCity.Text == "" && txtRemitZip.Text == "")
        //            {
        //                _TempZipText = txtRemitZip.Text;

        //            }
        //            pnlInternalControl.Visible = false;
        //            _isTextBoxLoading = false;
        //        }
        //    }
        //}


        private void txtOtherZip_LostFocus(object sender, System.EventArgs e)
        {
            // bool _result;
            if (oZipcontrol != null)
            {
                if (_isZipItemSelected == false & oZipcontrol.C1GridList.Focused == false & oZipcontrol.Focused == false)
                {
                    _isTextBoxLoading = true;
                    // txtZip.Text = _TempZipText;
                    if (txtOtherCity.Text == "" && txtOtherZip.Text == "")
                    {
                        //_TempZipText = txtZip.Text;
                        txtOtherZip.Text = _TempOtherZipText;

                    }
                    else
                    {
                        //_TempZipText = txtZip.Text;
                        if (txtOtherZip.Text == "")
                        {
                            pnlInternalZipControl.Visible = false;
                            _isTextBoxLoading = false;
                            return;
                        }
                        // txtZip.Text = ZipLeadingWithZero(txtZip);
                        int len = 6;

                        if (_isMessageshown == true)
                        {
                            return;
                        }
                        if (txtOtherZip.Text.Length <= len)
                        //if (txtZip.Text.Length <= 5)
                        {
                            //(checkZip(txtZip.Text) == false)
                            if (checkZip(txtOtherZip.Text) == false)
                            {

                                if (MessageBox.Show("It is not a known zip code. Do you want to continue with this information?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    txtOtherZip.Text = _TempOtherZipText;
                                    pnlInternalZipControl.Visible = false;
                                    _isTextBoxLoading = false;
                                    _isMessageshown = false;
                                    //return;
                                }
                            }

                        }

                    }
                    pnlInternalZipControl.Visible = false;
                    _isTextBoxLoading = false;
                }
            }
            
            
            
            
            //if (oZipcontrol != null)
            //{
            //    if (_isZipItemSelected == false & oZipcontrol.C1GridList.Focused == false & oZipcontrol.Focused == false)
            //    {
            //        _isTextBoxLoading = true;
 
            //        ////********
            //     //   txtOtherZip.Text = _TempOtherZipText;
            //        if (txtOtherCity.Text == "" && txtOtherZip.Text == "")
            //        {
            //            _TempOtherZipText = txtOtherZip.Text;

            //        }
            //        else
            //        {
            //            //_TempZipText = txtZip.Text;
            //            if (txtOtherZip.Text == "")
            //            {
            //                pnlInternalZipControl.Visible = false;
            //                _isTextBoxLoading = false;
            //                return;
            //            }
            //            // txtZip.Text = ZipLeadingWithZero(txtZip);
            //            int len;
            //                len = 6;
                        




            //            //if (_isMessageshown == true)
            //            //{
            //            //    return;
            //            //}
            //            if (txtOtherZip.Text.Length <= len)
            //            //if (txtZip.Text.Length <= 5)
            //            {
            //                //(checkZip(txtZip.Text) == false)
            //                if (checkZip(txtOtherZip.Text) == false)
            //                {

            //                    if (MessageBox.Show("It is not a known zip code. Do you want to continue with this information?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //                    {
            //                        txtOtherZip.Text = _TempZipText;
            //                        pnlInternalControl.Visible = false;
            //                        _isTextBoxLoading = false;
            //                        //_isMessageshown = false;
            //                        //return;
            //                    }
            //                }

            //            }

            //        }


            //        ////***********

            //        pnlInternalZipControl.Visible = false;
            //        _isTextBoxLoading = false;
            //    }
            //}
        }


        public bool checkZip(string zip)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                DataTable dtStates = new DataTable();
                string _sqlQuery = "SELECT count(*) FROM  CSZ_MST  WITH (NOLOCK) WHERE ZIP = '" + zip + "' ";
                Object NoOfRec = oDB.ExecuteScalar_Query(_sqlQuery);
                if (Convert.ToInt64(NoOfRec) > 0)
                {
                    _result = true;
                }
                oDB.Disconnect();
                oDB.Dispose();
                return _result;

            }
            catch (Exception ) //ex)
            {
                //ex.ToString();
                //ex = null;
                _result = false;
                return _result;

            }
        }

        //private void txtZip_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        //{
        //    try
        //    {
        //        //_ZipTextType = enumZipTextType.PatientZip;
        //        if (e.KeyChar == Convert.ToChar(13))
        //        {
        //            //' HITS ENTER BUTTON ''
        //            if (pnlInternalControl.Visible)
        //            {

        //                oZipcontrol_ItemSelected(null, null);
        //            }
        //        }
        //        else if (e.KeyChar == Convert.ToChar(27))
        //        {
        //            //' HITS ESCAPE ''

        //            if (txtRemitCity.Text == "" && txtRemitZip.Text == "")
        //            //if ( txtPAZip.Text == "")
        //            {
        //                _TempZipText = txtRemitZip.Text;

        //            }
        //            txtRemitCity.Focus();

        //            //*********
        //            if (txtOtherCity.Text == "" && txtOtherZip.Text == "")
        //            //if ( txtPAZip.Text == "")
        //            {
        //                _TempOtherZipText = txtOtherZip.Text;

        //            }
        //            txtOtherCity.Focus();
        //            //************
        //        }

        //        if (!(e.KeyChar == Convert.ToChar(8)))
        //        {
        //            if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
        //            {
        //                e.Handled = true;
        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}

       

        private void txtZip_GotFocus(object sender, EventArgs e)
        {
            try
            {
                _TempZipText = txtRemitZip.Text.Trim();
              
            }
            catch
            {

            }
        }
        private void txtOtherZip_GotFocus(object sender, EventArgs e)
        {
            try
            {
                
                _TempOtherZipText = txtOtherZip.Text.Trim();
            }
            catch
            {

            }
        }


        private void txtOtherZip_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down | e.KeyCode == Keys.Up)
                {
                    //' HITS UP / DOWN ''
                    if (pnlInternalZipControl.Visible)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        oZipcontrol.C1GridList.Focus();
                        oZipcontrol.C1GridList.Select(oZipcontrol.C1GridList.RowSel, 0);
                    }
                }
            }
            catch
            {
            }
        }

        private void oZipcontrol_AddBtnClick(object sender, System.EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(txtRemitCity.Text))
                {
                    AddCity(txtRemitCity.Text.Trim(), cmbRemitState.SelectedText.Trim(), txtRemitZip.Text.Trim(), "0", "0");
                    pnlInternalControl.Visible = false;
                }
                else
                {
                    MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRemitCity.Focus();
                }
                //isSearchControlOpen = false;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


       
        //private void oZipcontrol_CloseBtnClick1(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        if (this.pnlInternalControl.Visible == true)
        //        {
        //            this.pnlInternalControl.Visible = false;
        //        }
        //        isSearchControlOpen = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    }
        //}
        
        private void oZipcontrol_ModifyBtnClick(object sender, System.EventArgs e)
        {

            try
            {
                Int64 nZipID = default(Int64);
                nZipID = Convert.ToInt64(oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 2));
                if (!string.IsNullOrEmpty(txtRemitCity.Text))
                {
                    UpdateCity(txtRemitCity.Text.Trim(), txtRemitZip.Text.Trim(), nZipID);
                    pnlInternalControl.Visible = false;
                }
                else
                {
                    MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRemitCity.Focus();
                }

              
                //isSearchControlOpen = false;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        


        private void AddCity(string sCity, string sState, string sZip, string sAreaCode, string sCounty)
        {
            try
            {
                AddCity1(sCity, sState, sZip, Convert.ToInt64(sAreaCode), sCounty);

                if (!string.IsNullOrEmpty(txtRemitCity.Text.Trim()))
                {
                    if (txtRemitZip.Text.Trim() == sZip)
                    {
                        txtRemitCity.Text = sCity;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private void OtherAddCity(string sCity, string sState, string sZip, string sAreaCode, string sCounty)
        {
            try
            {
                AddCity1(sCity, sState, sZip, Convert.ToInt64(sAreaCode), sCounty);

                //***************

                if (!string.IsNullOrEmpty(txtOtherCity.Text.Trim()))
                {
                    if (txtOtherZip.Text.Trim() == sZip)
                    {
                        txtOtherCity.Text = sCity;
                    }
                }
                //******************
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public void AddCity1(string sCity, string sState, string sZip, Int64 sAreaCode, string sCounty)
        {

            SqlConnection conn = new SqlConnection(_databaseconnectionstring);
            SqlCommand oCmd = default(SqlCommand);
            string _strSQL = "";
            object _result = null;

            try
            {
                _strSQL = "SELECT MAX(ISNULL(nID,0)) +1 From csz_mst WITH (NOLOCK)";

                oCmd = new SqlCommand();

                oCmd.Connection = conn;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = _strSQL;
                conn.Open();
                _result = oCmd.ExecuteScalar();


                _strSQL = "";
                _strSQL = "Insert into csz_mst (City,ST,Zip,Areacode,county,nID) values ('" + sCity.Replace("'", "''") + "','" + sState.Replace("'", "''") + "','" + sZip.Replace("'", "''") + "'," + sAreaCode + ",'" + sCounty.Replace("'", "''") + "'," + Convert.ToInt64(_result) + ")";
                // where zip = '" & sZip & "'"

                oCmd = new SqlCommand();
                oCmd.Connection = conn;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = _strSQL;

                oCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((conn != null))
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                        conn = null;

                        if ((oCmd != null))
                        {
                            oCmd.Dispose();
                            oCmd = null;
                        }
                    }
                }
            }
        }

        private void UpdateCity(string sCity, string sZip, Int64 ID)
        {

            try
            {
                UpdateCity1(sCity, sZip, ID);

                if (!string.IsNullOrEmpty(txtRemitCity.Text.Trim()))
                {
                    if (txtRemitZip.Text.Trim() == sZip)
                    {
                        txtRemitCity.Text = sCity;
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void OtherUpdateCity(string sCity, string sZip, Int64 ID)
        {

            try
            {
                UpdateCity1(sCity, sZip, ID);

                

                if (!string.IsNullOrEmpty(txtOtherCity.Text.Trim()))
                {
                    if (txtOtherZip.Text.Trim() == sZip)
                    {
                        txtOtherCity.Text = sCity;
                    }
                }
             

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCityAgainstZip(string sCity, string sZip)
        {
            SqlConnection conn = new SqlConnection(_databaseconnectionstring);
            SqlCommand oCmd = default(SqlCommand);
            string _strSQL = "";


            try
            {

                _strSQL = "update csz_mst WITH (READPAST) set city = '" + sCity.Replace("'", "''") + "' where zip = '" + sZip + "'";

                oCmd = new System.Data.SqlClient.SqlCommand();

                oCmd.Connection = conn;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = _strSQL;

                conn.Open();
                oCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((conn != null))
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                        conn = null;

                        if ((oCmd != null))
                        {
                            oCmd.Dispose();
                            oCmd = null;
                        }
                    }
                }
            }
        }
        public void UpdateCity1(string sCity, string sZip, Int64 ID)
        {
            SqlConnection conn = new SqlConnection(_databaseconnectionstring);
            SqlCommand oCmd = default(SqlCommand);
            string _strSQL = "";

            try
            {

                _strSQL = "update csz_mst WITH (READPAST) set city = '" + sCity.Replace("'", "''") + "' where zip = '" + sZip + "' AND nID = " + ID + "";

                oCmd = new SqlCommand();

                oCmd.Connection = conn;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = _strSQL;

                conn.Open();
                oCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((conn != null))
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                        conn = null;

                        if ((oCmd != null))
                        {
                            oCmd.Dispose();
                            oCmd = null;
                        }
                    }
                }
            }

        }
        private void txtOtherZip_TextChanged(object sender, EventArgs e)
        {
            try
            {

                //***********************
                {
                    string _strRegex = "";
                    //'Allow digits only if country is US 
                    //if ((_Country == "US"))
                    //{
                    //    _strRegex = "[0-9]";
                    //}
                    //else
                    //{
                    //'allow alphanumerics if country is Canada 
                    _strRegex = "^([0-9a-zA-Z])";
                    //     }
                    string strOtherZipcode = txtOtherZip.Text;
                    foreach (char c in strOtherZipcode)
                    {
                        if (Regex.IsMatch(c.ToString(), _strRegex) == false)
                        {


                            strOtherZipcode = strOtherZipcode.Replace(c.ToString(), "");
                        }
                    }
                    txtOtherZip.Text = strOtherZipcode;
                }

                //**************************



                _ZipCodeType = PaymentAddressType.OtherAddress.GetHashCode().ToString();

                //_ZipTextType = enumZipTextType.PatientZip;
                pnlInternalZipControl.BringToFront();

                if (isFormLoading == false & _isTextBoxLoading == false)
                {
                    if (pnlInternalZipControl.Visible == false)
                    {
                        pnlInternalZipControl.Visible = true;
                        OpenInternalControl(gloAddress.gloGridListControlType.ZIP, "Zip", false, 0, 0, "");
                     
                        //**********
                        oZipcontrol.FillControl(Convert.ToString(txtOtherZip.Text.Trim()));
                        //*********
                    }
                    else
                    {
                       
                        //************
                        oZipcontrol.FillControl(Convert.ToString(txtOtherZip.Text.Trim()));
                        //*************
                    }
                }
            }
            catch
            {
            }
            finally
            {
                //_TempOtherZipText = txtPAZip.Text;
            }
        }


        private void txtRemitZip_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down | e.KeyCode == Keys.Up)
                {
                    //' HITS UP / DOWN ''
                    if (pnlInternalControl.Visible)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        oZipcontrol.C1GridList.Focus();
                        oZipcontrol.C1GridList.Select(oZipcontrol.C1GridList.RowSel, 0);
                    }
                }
            }
            catch
            {
            }
        }

        private void txtRemitZip_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                _isMessageshown = false;
                //_ZipTextType = enumZipTextType.PatientZip;
                if (e.KeyChar == Convert.ToChar(13))
                {

                    //' HITS ENTER BUTTON ''
                    if (pnlInternalControl.Visible)
                    {
                        int len=5;

                        //if (_Country == "US")
                        //{
                        //    len = 5;
                        //}
                      //  else
                      //  {
                        //    len = 6;
                      //  }
                        if (txtRemitZip.Text.Trim().Length == len && oZipcontrol.C1GridList.Row != -1)
                        {
                            oZipcontrol_ItemSelected(null, null);
                        }
                        if (txtRemitZip.Text.Trim() != "")
                        {
                            _isTextBoxLoading = false;
                            _isZipItemSelected = false;
                            //isSearchControlOpen = false;
                            if (pnlInternalControl.Visible == true)
                            {
                                pnlInternalControl.Visible = false;
                                txtRemitCity.Focus();
                            }
                            _isMessageshown = true;
                            _isTextBoxLoading = false;
                            _isZipItemSelected = false;
                            _isTextBoxLoading = false;
                            _isZipItemSelected = true;



                        }

                    }
                }
                else if (e.KeyChar == Convert.ToChar(27))
                {

                    //' HITS ESCAPE ''

                     txtRemitZip.Text = _TempZipText;
                    txtRemitCity.Focus();
                }
                
                {
                    string _strRegex = "";
                    //'Allow digits only if country is US 
                 //   if ((_Country == "US"))
                  //  {
                       // _strRegex = "^([0-9]*)$";
                 //   }
                  //  else
                  //  {
                        //'allow alphanumerics if country is Canada 
                        _strRegex = "^([0-9a-zA-Z]*)$";
                   // }
                    if (!(e.KeyChar == Convert.ToChar(8)))
                    {

                        if (Regex.IsMatch(e.KeyChar.ToString(), _strRegex) == false)
                        {
                            e.Handled = true;
                        }
                        if ((e.KeyChar == Convert.ToChar(32)))
                        {
                            //Allow space 
                            if (!string.IsNullOrEmpty(txtRemitZip.Text))
                            {

                                e.Handled = false;
                            }
                        }
                    }
                }


            }
            catch
            {

            }
        }

        private void txtOtherZip_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                _isMessageshown = false;
                //_ZipTextType = enumZipTextType.PatientZip;
                if (e.KeyChar == Convert.ToChar(13))
                {

                    //' HITS ENTER BUTTON ''
                    if (pnlInternalZipControl.Visible)
                    {
                        int len = 5;

                        //if (_Country == "US")
                        //{
                        //    len = 5;
                        //}
                        //  else
                        //  {
                        //    len = 6;
                        //  }
                        if (txtOtherZip.Text.Trim().Length == len && oZipcontrol.C1GridList.Row != -1)
                        {
                            oZipcontrol_ItemSelected(null, null);
                        }
                        if (txtOtherZip.Text.Trim() != "")
                        {
                            _isTextBoxLoading = false;
                            _isZipItemSelected = false;
                            //isSearchControlOpen = false;
                            if (pnlInternalZipControl.Visible == true)
                            {
                                pnlInternalZipControl.Visible = false;
                                txtOtherCity.Focus();
                            }
                            _isMessageshown = true;
                            _isTextBoxLoading = false;
                            _isZipItemSelected = false;
                            _isTextBoxLoading = false;
                            _isZipItemSelected = true;



                        }

                    }
                }
                else if (e.KeyChar == Convert.ToChar(27))
                {

                    //' HITS ESCAPE ''

                    txtOtherZip.Text = _TempOtherZipText;
                    txtOtherCity.Focus();
                }

                {
                    string _strRegex = "";
                    //'Allow digits only if country is US 
                    //   if ((_Country == "US"))
                    //  {
                    // _strRegex = "^([0-9]*)$";
                    //   }
                    //  else
                    //  {
                    //'allow alphanumerics if country is Canada 
                    _strRegex = "^([0-9a-zA-Z]*)$";
                    // }
                    if (!(e.KeyChar == Convert.ToChar(8)))
                    {

                        if (Regex.IsMatch(e.KeyChar.ToString(), _strRegex) == false)
                        {
                            e.Handled = true;
                        }
                        if ((e.KeyChar == Convert.ToChar(32)))
                        {
                            //Allow space 
                            if (!string.IsNullOrEmpty(txtRemitZip.Text))
                            {

                                e.Handled = false;
                            }
                        }
                    }
                }


            }
            catch
            {

            }
        }



        
        bool _isMessageshown = false;
        public void txtZip_LostFocus(object sender, System.EventArgs e)
        {
           // bool _result;
            if (oZipcontrol != null)
            {
                if (_isZipItemSelected == false & oZipcontrol.C1GridList.Focused == false & oZipcontrol.Focused == false)
                {
                    _isTextBoxLoading = true;
                    // txtZip.Text = _TempZipText;
                    if (txtRemitCity.Text == ""  && txtRemitZip.Text == "")
                    {
                        //_TempZipText = txtZip.Text;
                        txtRemitZip.Text = _TempZipText;

                    }
                    else
                    {
                        //_TempZipText = txtZip.Text;
                        if (txtRemitZip.Text == "")
                        {
                            pnlInternalControl.Visible = false;
                            _isTextBoxLoading = false;
                            return;
                        }
                        // txtZip.Text = ZipLeadingWithZero(txtZip);
                        int len =6;
                        



                       
                       if (_isMessageshown == true)
                        {
                            return;
                        } 
                       if (txtRemitZip.Text.Length <= len)
                       //if (txtZip.Text.Length <= 5)
                        {
                            //(checkZip(txtZip.Text) == false)
                            if (checkZip(txtRemitZip.Text) == false)
                            {

                                if (MessageBox.Show("It is not a known zip code. Do you want to continue with this information?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    txtRemitZip.Text = _TempZipText;
                                    pnlInternalControl.Visible = false;
                                    _isTextBoxLoading = false;
                                    _isMessageshown = false;
                                    //return;
                                }
                            }

                        }

                    }
                    pnlInternalControl.Visible = false;
                    _isTextBoxLoading = false;
                }
            }

        }

    


        //private void txtRemitZip_TextChanged(object sender, System.EventArgs e)
        //{
        //    if (isFormLoading == true)
        //    {
        //        return;
        //    }
        //    try
        //    {
        //        ///Sandip Darade 20100320
        //        //'Remove Special character 

        //        {
        //            string _strRegex = "";
        //            //'Allow digits only if country is US 
        //            if ((_Country == "US"))
        //            {
        //                _strRegex = "[0-9]";
        //            }
        //            else
        //            {
        //                //'allow alphanumerics if country is Canada 
        //                _strRegex = "^([0-9a-zA-Z])";
        //            }
        //            string strZipcode = txtRemitZip.Text;
        //            foreach (char c in strZipcode)
        //            {
        //                if (Regex.IsMatch(c.ToString(), _strRegex) == false)
        //                {


        //                    strZipcode = strZipcode.Replace(c.ToString(), "");
        //                }
        //            }
        //            txtRemitZip.Text = strZipcode;
        //        }



        //        //_ZipTextType = enumZipTextType.PatientZip;
        //        pnlInternalControl.BringToFront();

        //        if (isFormLoading == false & _isTextBoxLoading == false)
        //        {
        //            if (pnlInternalControl.Visible == false)
        //            {
        //                pnlInternalControl.Visible = true;
        //                OpenInternalControl(gloGridListControlType.ZIP, "Zip", false, 0, 0, "");
        //                oZipcontrol.FillControl(Convert.ToString(txtRemitZip.Text.Trim()));
        //            }
        //            else
        //            {
        //                oZipcontrol.FillControl(Convert.ToString(txtRemitZip.Text.Trim()));
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        //_TempZipText = txtZip.Text;
        //        if (isFormLoading == false)
        //            _AddressModified = true;
        //    }
        //}


        //private void oZipcontrol_ItemSelected(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        if (oZipcontrol.C1GridList.Row < 0)
        //        {
        //            return;
        //        }
        //        string _Zip = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 0).ToString();
        //        string _City = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 1).ToString();
        //        string _ID = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 2).ToString();
        //        string _County = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 3).ToString();
        //        string _State = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 4).ToString();
        //        string _AreaCode = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 5).ToString();

        //        _isTextBoxLoading = true;

        //        txtRemitZip.Text = _Zip;
        //        txtRemitZip.Tag = _ID;
        //        txtRemitCity.Text = _City;
        //        txtRemitCity.Tag = _AreaCode;
        //      //  txtCounty.Text = _County;
        //        cmbRemitState.Text = _State;

        //        _isTextBoxLoading = false;
        //        _isZipItemSelected = true;
        //        if (pnlInternalControl.Visible == true)
        //        {
        //            pnlInternalControl.Visible = false;
        //            txtRemitCity.Focus();
        //        }

        //        //isSearchControlOpen = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}





        private void oZipcontrol_LostFocusEvent(object sender, EventArgs e)
        {
            if (txtRemitZip.Focused == false)
            {
                //txtZip_LostFocus(null, null);
                pnlInternalControl.Visible = false;
            }
        }


        private void oZipcontrol_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {
                CloseInternalControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        private void oZipcontrol_CloseBtnClick1(object sender, System.EventArgs e)
        {
            try
            {
                if (this.pnlInternalControl.Visible == true)
                {
                    this.pnlInternalControl.Visible = false;
                }

                //isSearchControlOpen = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        //public bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        //{
        //    bool _result = false;
        //    _isZipItemSelected = false;
        //    try
        //    {

        //        if (oZipcontrol != null)
        //        {
        //            CloseInternalControl();
        //        }
        //        oZipcontrol = new gloZipcontrol(ControlType, false, 0, 0, 0, _databaseconnectionstring);
        //        oZipcontrol.ItemSelectedclick += oZipcontrol_ItemSelected;
        //        oZipcontrol.InternalGridKeyDownclick += oZipcontrol_InternalGridKeyDown;
        //        oZipcontrol.LostFocusEvent += new gloZipcontrol.LostFocus(oZipcontrol_LostFocusEvent);
        //        oZipcontrol.ControlHeader = ControlHeader;
        //        oZipcontrol.ShowHeader = false;

        //        oZipcontrol.Dock = DockStyle.Fill;
        //        pnlInternalControl.BringToFront();
        //        pnlInternalControl.Visible = true;
        //        pnlInternalControl.Controls.Add(oZipcontrol);



        //        if (!string.IsNullOrEmpty(SearchText))
        //        {
        //            oZipcontrol.Search(SearchText, SearchColumn.Code);
        //        }
        //        oZipcontrol.Show();
        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        _result = false;
        //    }
        //    finally
        //    {

        //    }

            //isSearchControlOpen = true;
         //   return _result;
  //      }

       



    

        #endregion " ZIP Control Implemented "

        private void rbOtherAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOtherAddress.Checked)
            {
                pnlOtherAddress.Visible = true;
                if (rbRemitOtherAddress.Checked == false)
                {
                    if (this.Height != 798)
                        this.Height = this.Height + 138;
                }
                else
                {
                    if (this.Height == 660 || this.Height == 519)
                        this.Height = this.Height + 138;
                }
               
            }
            else
            {
                pnlOtherAddress.Visible = false;
                if (rbRemitOtherAddress.Checked == false)
                {
                    if (this.Height != 798)
                        this.Height = this.Height - 138;
                }
                else
                {
                    if (this.Height == 798)
                        this.Height = this.Height - 138;
                }
            }

            if (rbOtherAddress.Checked == true)
            {
                rbOtherAddress.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbOtherAddress.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
           

        }

        private void rbRemitAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRemitAddress.Checked == true)
            {
                rbRemitAddress.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbRemitAddress.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbBillingProvider_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBillingProvider.Checked == true)
            {
                rbBillingProvider.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbBillingProvider.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void txtRemitCity_TextChanged(object sender, EventArgs e)
        {
            if (isFormLoading == false)
                _AddressModified = true;
        }

       
        private void txtOtherCity_TextChanged(object sender, EventArgs e)
        {
            if (isFormLoading == false)
                _AddressModified = true;
        }

     
        private void cmbOtherState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFormLoading == false)
                _AddressModified = true;
        }

        private void cmbRemitState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFormLoading == false)
                _AddressModified = true;
        }

        public bool CheckEmailAddress(string input)
        {
            bool response = false;
            if (Regex.IsMatch(input, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*") || input.Trim() == "")
            {
                response = true;
            }
            else
            {
                response = false;
            }
            return response;

        }

        private void txtBillingEmail_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txtBillingEmail.Text) == false)
            {
                MessageBox.Show("Enter a valid email id.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private void c1StatementMessage_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1StatementMessage_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            c1StatementMessage.Focus();
            c1StatementMessage.Select(e.Row, 1);
        }

        private void c1StatementMessage_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            //string tempstring = "";
            //c1StatementMessage.FinishEditing();
            //tempstring = c1StatementMessage.GetData(e.Row, 1).ToString().Trim();
            //if (c1StatementMessage.GetData(e.Row, 1).ToString().Trim().Length > 255)
            //{
            //    e.Handled = true;
            //}

            //c1StatementMessage.SetData(e.Row, 1, tempstring + e.KeyChar.ToString());
            //c1StatementMessage.StartEditing(e.Row, 1);
            

        }

        private void c1StatementMessage_SetupEditor(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (e.Col == 1)
            {
                ((TextBox)c1StatementMessage.Editor).MaxLength = 250;
            }
        }

        private void c1StatementMessage_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            c1StatementMessage.Editor = (TextBox)c1StatementMessage.Editor;
        }

        private void rbRemitOtherAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRemitOtherAddress.Checked)
            {
                pnlRemitAddress.Visible = true;
                if (rbOtherAddress.Checked == true)
                {
                    if (this.Height != 798)
                        this.Height = this.Height + 141;
                }
                else
                {
                    if (this.Height == 798)
                        this.Height = this.Height - 138;
                    else if (this.Height == 519)
                        this.Height = this.Height + 141;
                }
            }
            else
            {
                pnlRemitAddress.Visible = false;
                if (rbOtherAddress.Checked == true)
                {
                    if (this.Height == 798)
                        this.Height = this.Height - 141;
                }
                else
                {
                    if (this.Height == 660)
                        this.Height = this.Height - 141;
                }
            }

            if (rbRemitOtherAddress.Checked == true)
            {
                rbRemitOtherAddress.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbRemitOtherAddress.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
            System.Drawing.Point m = new Point(0, -26);
            c1StatementMessage.ScrollPosition = m;
        }

        private void rbRemitBillingProvider_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRemitBillingProvider.Checked == true)
            {
                rbRemitBillingProvider.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbRemitBillingProvider.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void txtDetachInstructions_MouseHover(object sender, EventArgs e)
        {
            if (txtDetachInstructions.Text.Length > 70 && txtDetachInstructions.Text.Trim() != string.Empty)
            {
                C1SuperTooltip1.SetToolTip(this.txtDetachInstructions, Convert.ToString(txtDetachInstructions.Text));
            }
            else
            {
                C1SuperTooltip1.SetToolTip(this.txtDetachInstructions, "");
            }
        }

    }
}