using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using gloPMGeneral.gloPriorAuthorization;
using gloSettings;
using gloContacts;

namespace gloPMGeneral
{
    public partial class frmModifyAuthorization : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;      
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private Int64 _CurrentPriorAuthorization;
        private Int64 _ContactID = 0;
        gloPriorAuthorization.clsgloPriorAuthorization objPrior = null;
        private gloListControl.gloListControl oListControl;
        private ComboBox combo;
        bool _tracklimit = false;
        ToolTip tooltip_Rpt = new ToolTip();
        private int  _globaltotalvisits;
        bool _IsTrackAuthLimit = false;
        #endregion " Declarations "
        
        #region " Property Procedures "

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }
     
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion " Property Procedures "

        #region " Constructor "

        public frmModifyAuthorization(string databaseConnectionString, Int64 CurrentPriorAuthorization)
        {
            _databaseconnectionstring = databaseConnectionString;                       
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
             InitializeComponent();

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


             _CurrentPriorAuthorization = CurrentPriorAuthorization;
             cmbReferralProvider.DrawMode = DrawMode.OwnerDrawFixed;
             cmbReferralProvider.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

             cmbInsurance.DrawMode = DrawMode.OwnerDrawFixed;
             cmbInsurance.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

         }

        #endregion " Constructor "

        #region " Form Load "

        private void frmModifyAuthorization_Load(object sender, EventArgs e)
        {
            try
            {
                GetPriorAuthorizations(_CurrentPriorAuthorization);                     
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

         #endregion " Form Load "

        #region " Tool Strip Event "
      
        private void tsb_Close_Click(object sender, EventArgs e)
        {
            //DialogResult dgResult = MessageBox.Show("Are you sure,you want to close?  ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dgResult == DialogResult.Yes)
            //{
            this.Close();
            //}
        }

        private void tsb_Ok_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (SavePriorAuthorization())
                {
                    this.Close();
                }
            }
        }
      
        #endregion " Tool Strip Event "

        #region " Form Control Event "

        private void dtAuthorizationThroughDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void radreferralin_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbreferralin.Checked == true)
            {
                if (panel4.Enabled == false)
                {

                    if (objPrior.AuthorizationType == 1 || objPrior.AuthorizationType == 3)
                    {
                        if (_IsTrackAuthLimit == true)
                        {
                            chklimityes.Checked = true;
                        }
                        else
                        {
                            chklimitno.Checked = true;
                        }
                    }
                    else
                    {
                        chklimitno.Checked = false;
                        chklimityes.Checked = false;

                        mskauthexp.Enabled = false;
                        mskAuthorizationstart.Enabled = false;
                        txtvisitsallow.Enabled = false;
                        txtvisitsallow.Text = "";
                        lblVisitsused.Text = "";
                        mskauthexp.ResetText();
                        mskAuthorizationstart.ResetText();
                    }

                    if (chklimityes.Checked == true)
                    {
                        if (objPrior.VisitsAllowed == null)
                        {
                            txtvisitsallow.Text = "";
                        }
                        else
                        {
                            txtvisitsallow.Text = Convert.ToString(objPrior.VisitsAllowed);
                        }

                        if (objPrior.StartDate > 0)
                        {
                            mskAuthorizationstart.Text = gloDateMaster.gloDate.DateAsDate(objPrior.StartDate).ToString("MM/dd/yyyy");
                           
                        }
                        if (objPrior.ExpDate > 0)
                        {
                            mskauthexp.Text = gloDateMaster.gloDate.DateAsDate(objPrior.ExpDate).ToString("MM/dd/yyyy");
                          
                        }

                    }
                    if (objPrior.InsuranceID > 0)
                    {
                        if (cmbInsurance.Items.Count > 0)
                        {
                            cmbInsurance.SelectedValue = objPrior.InsuranceID;
                        }
                    }
                    else
                    {
                        cmbInsurance.SelectedIndex = 0;
                    }

                    if (objPrior.InsuranceNote != "")
                    {
                        txtInsurancenote.Text = Convert.ToString(objPrior.InsuranceNote);
                    }
                   

                }
                cmbReferralProvider.Visible = true;
                txttoprovider.Visible = false;
                txttoprovider.Location = new Point(335, 30);
                label33.Text = "Referring Provider :";
                panel4.Enabled = true;
                label10.Visible = false;
                btnToProvider.Visible = false;
                btnrefremove.Visible = false;
                btnremove.Visible = false;
                lblVisitsused.Text = (_globaltotalvisits).ToString();
                if (_globaltotalvisits == 0 || (chklimityes.Checked == false && chklimitno.Checked == false) || txtvisitsallow.Enabled == false)
                { 
                    lblVisitsused.Text = "";
                }
               
                txtauth.Focus();
                txtauth.Select();
            }
            else if (rdbreferralout.Checked == true)
            {

                btnToProvider.Visible = false;
                cmbReferralProvider.Visible = false;
                txttoprovider.Location = new Point(335, 30);
                txttoprovider.Visible = true;
                label33.Text = "To Provider :";
                panel4.Enabled = false;
                label10.Visible = false;
                btnToProvider.Visible = false;
                btnrefremove.Visible = true;
                btnremove.Visible = false;
                c1ProirAuthorization.Enabled = false;
                txtInsurancenote.Text = "";
                txtvisitsallow.Text = "";
                mskauthexp.ResetText();
                mskAuthorizationstart.ResetText();
                if (cmbInsurance.SelectedIndex != -1)
                cmbInsurance.SelectedIndex = 0;
                chklimitno.Checked = false;
                chklimityes.Checked = false;
                lblVisitsused.Text = "";
                txtauth.Focus();
                txtauth.Select();
            }
            else if (rdbboth.Checked == true)
            {
                if (panel4.Enabled == false)
                {
                    if (objPrior.AuthorizationType == 1 || objPrior.AuthorizationType == 3)
                    {
                        if (_IsTrackAuthLimit == true)
                        {
                            chklimityes.Checked = true;
                        }
                        else
                        {
                            chklimitno.Checked = true;
                        }
                    }
                    else
                    {
                        chklimitno.Checked = false;
                        chklimityes.Checked = false;

                        mskauthexp.Enabled = false;
                        mskAuthorizationstart.Enabled = false;
                        txtvisitsallow.Enabled = false;
                        txtvisitsallow.Text = "";
                        lblVisitsused.Text = "";
                        mskauthexp.ResetText();
                        mskAuthorizationstart.ResetText();
                    }
                    if (chklimityes.Checked == true)
                    {
                        if (objPrior.VisitsAllowed == null)
                        {
                            txtvisitsallow.Text = "";
                        }
                        else
                        {
                            txtvisitsallow.Text = Convert.ToString(objPrior.VisitsAllowed);
                        }

                        if (objPrior.StartDate > 0)
                        {
                            mskAuthorizationstart.Text = gloDateMaster.gloDate.DateAsDate(objPrior.StartDate).ToString("MM/dd/yyyy");
                           
                        }
                        if (objPrior.ExpDate > 0)
                        {
                            mskauthexp.Text = gloDateMaster.gloDate.DateAsDate(objPrior.ExpDate).ToString("MM/dd/yyyy");
                          
                        }
                    }
                    if (objPrior.InsuranceID > 0)
                    {
                        if (cmbInsurance.Items.Count > 0)
                        {
                            cmbInsurance.SelectedValue = objPrior.InsuranceID;
                        }
                    }
                    else
                    {
                        cmbInsurance.SelectedIndex = 0;
                    }

                    if (objPrior.InsuranceNote != "")
                    {
                        txtInsurancenote.Text = Convert.ToString(objPrior.InsuranceNote);
                    }

                }
                cmbReferralProvider.Visible = true;
                btnToProvider.Visible = true;
                label33.Text = "Referring Provider :";
                panel4.Enabled = true;
                txttoprovider.Location = new Point(335, 54);
                txttoprovider.Visible = true;
                label10.Visible = true;
                btnToProvider.Visible = true;
                btnrefremove.Visible = false;
                btnremove.Visible = true;
                lblVisitsused.Text = (_globaltotalvisits).ToString();
                if (_globaltotalvisits == 0 || (chklimityes.Checked == false && chklimitno.Checked == false) || txtvisitsallow.Enabled == false)
                {
                    lblVisitsused.Text = "";
                }
                txtauth.Focus();
                txtauth.Select();
            }
        }
      
        private void tsb_Activate_Click(object sender, EventArgs e)
        {
            //if (tsb_Activate.Text.ToUpper().Trim() == "ACTIVATE")
            //{
            objPrior.IsActive = true;
            lblActivate.Text = "";
            tsb_Deactivate.Visible = true;
            tsb_Activate.Visible = false;
            panel7.Visible = false;
            //}
            //else
            //{
            //if (DialogResult.Yes != MessageBox.Show("Do you want to deactivate prior authorization . \nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            //{
            //    objPrior.IsActive = false;
            //}
            // }
        }

        private void c1ProirAuthorization_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            string _RecordType = "";
           // bool _HasCharges = false;
            decimal _Units = 0;
            decimal _Charges = 0;
            C1.Win.C1FlexGrid.CellStyle cs1;// = c1ProirAuthorization.Styles.Add("cs_Normal");
            try
            {
                if (c1ProirAuthorization.Styles.Contains("cs_Normal"))
                {
                    cs1 = c1ProirAuthorization.Styles["cs_Normal"];
                }
                else
                {
                    cs1 = c1ProirAuthorization.Styles.Add("cs_Normal");

                }

            }
            catch
            {
                cs1 = c1ProirAuthorization.Styles.Add("cs_Normal");

            }
            C1.Win.C1FlexGrid.CellStyle cs;// = c1ProirAuthorization.Styles.Add("cs_Red");
            try
            {
                if (c1ProirAuthorization.Styles.Contains("cs_Red"))
                {
                    cs = c1ProirAuthorization.Styles["cs_Red"];
                }
                else
                {
                    cs = c1ProirAuthorization.Styles.Add("cs_Red");

                }

            }
            catch
            {
                cs = c1ProirAuthorization.Styles.Add("cs_Red");

            }
            cs.ForeColor = Color.Red;
            for (int i = 1; i < c1ProirAuthorization.Rows.Count; i++)
            {
                //1==Charges , 2=Appointment
                c1ProirAuthorization.SetCellStyle(i, c1ProirAuthorization.Cols["Date"].Index, cs1);
                c1ProirAuthorization.SetCellStyle(i, c1ProirAuthorization.Cols["Description"].Index, cs1);
                c1ProirAuthorization.SetCellStyle(i, c1ProirAuthorization.Cols["sReferralName"].Index, cs1);

                _RecordType = "";
                _RecordType = Convert.ToString(c1ProirAuthorization.GetData(i, 1));
                if (_RecordType == "1")
                {
                    //_HasCharges = true;
                    if (Convert.ToString(_Units) != "")
                    {
                        _Units = _Units + Convert.ToDecimal(c1ProirAuthorization.GetData(i, "Units"));
                    }
                    if (Convert.ToString(_Charges) != "")
                    {
                        _Charges = _Charges + Convert.ToDecimal(c1ProirAuthorization.GetData(i, "charges"));
                    }
                    //_HasCharges = true;
                }
                else if (_RecordType == "2")
                {
                    if (objPrior.IsTrackAuthLimit == true && (objPrior.StartDate > gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1ProirAuthorization.GetData(i, "Date"))) || objPrior.ExpDate < gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1ProirAuthorization.GetData(i, "Date")))))
                    {
                        c1ProirAuthorization.SetCellStyle(i, c1ProirAuthorization.Cols["Date"].Index, cs);
                        c1ProirAuthorization.SetCellStyle(i, c1ProirAuthorization.Cols["Description"].Index, cs);
                        c1ProirAuthorization.SetCellStyle(i, c1ProirAuthorization.Cols["sReferralName"].Index, cs);

                    }
                }
            }
        }

        private void rdblimityes_CheckedChanged(object sender, EventArgs e)
        {
            if (chklimityes.Checked == true)
            {
                if (mskauthexp.Enabled == false)
                {
                    if (objPrior.VisitsAllowed == null)
                    {
                        txtvisitsallow.Text = "";
                    }
                    else
                    {
                        txtvisitsallow.Text = Convert.ToString(objPrior.VisitsAllowed);
                    }

                    if (objPrior.StartDate > 0)
                    {

                        mskAuthorizationstart.Text  = gloDateMaster.gloDate.DateAsDate(objPrior.StartDate).ToString("MM/dd/yyyy");
                    }
                    if (objPrior.ExpDate > 0)
                    {
                        mskauthexp.Text = gloDateMaster.gloDate.DateAsDate(objPrior.ExpDate).ToString("MM/dd/yyyy"); 
                    }
                }
                mskauthexp.Enabled = true;
                mskAuthorizationstart.Enabled = true;
                txtvisitsallow.Enabled = true;
                objPrior.IsTrackAuthLimit = true;
                if (objPrior.IsTrackAuthLimit == true && (lblvisitsremain.Text).Contains("-") == true)
                {
                    lblvisitsremain.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblvisitsremain.ForeColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                mskauthexp.Enabled = false;
                mskAuthorizationstart.Enabled = false;
                txtvisitsallow.Enabled = false;
                txtvisitsallow.Text = "";
                mskauthexp.ResetText();
                mskAuthorizationstart.ResetText();
                objPrior.IsTrackAuthLimit = false;
            }
        }

        private void txtvisitsallow_TextChanged(object sender, EventArgs e)
        {
            if (txtvisitsallow.Text.Trim() != "")
            {
                int _allowed = 0;
                //int _Remained=0;

                if (Convert.ToString(txtvisitsallow.Text).Trim() != "")
                {
                    _allowed = Convert.ToInt32(txtvisitsallow.Text);
                }
                lblvisitsremain.Text = (_allowed - _globaltotalvisits).ToString();

                if (objPrior.IsTrackAuthLimit == true && (lblvisitsremain.Text).Contains("-") == true)
                {
                    lblvisitsremain.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblvisitsremain.ForeColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                lblvisitsremain.Text = "";
            }
        }

        private void txtvisitsallow_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != Convert.ToChar(8) && e.KeyChar != Convert.ToChar(46))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9]*$") == false)
                    {
                        e.Handled = true;
                    }
                }
                else if (e.KeyChar == Convert.ToChar(46))
                {
                    e.Handled = true;
                }


            }
            catch (System.OverflowException ex)
            {
                MessageBox.Show("Visits allow is invalid.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                return;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void btnrefremove_Click(object sender, EventArgs e)
        {
            txttoprovider.Text = "";
            txttoprovider.Tag = 0;
        }

        private void btnremove_Click(object sender, EventArgs e)
        {
            txttoprovider.Text = "";
            txttoprovider.Tag = 0;
        }

        private void tsb_Deactivate_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Do you want to deactivate prior authorization.\nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                objPrior.IsActive = false;
                panel7.Visible = true;
                lblActivate.Text = "Inactive";
                tsb_Deactivate.Visible = false;
                tsb_Activate.Visible = true;
            }
        }

        private void btnAdd_Referral_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.objPrior.PatientID > 0)
                {

                    if (rdbreferralout.Checked == true)
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
                                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick_Both);
                                }
                                catch { }
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
                                try
                                {
                                    oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                                }
                                catch { }
                                try
                                {
                                    oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                                }
                                catch { }
                            }
                            catch
                            {
                            }
                            oListControl.Dispose();
                            oListControl = null;
                        }
                        panel1.Visible = false;
                        panel2.Visible = false;
                        panel3.Visible = false;
                        pnltlsStrip.Visible = false;

                        oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Referrals, false, this.Width);
                        oListControl.ClinicID = _ClinicID;
                        oListControl.ControlHeader = "Referral Provider";

                        oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick_Both);
                        oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                        oListControl.ModifyFormHandlerClick += new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                        //this.Width = 680;
                        //this.Height = 530;
                        this.Controls.Add(oListControl);
                        oListControl.BringToFront();
                        oListControl.Dock = DockStyle.Fill;
                        oListControl.OpenControl();
                        //this.Width = 680;
                        //this.Height = 530;
                    }

                    else
                    {

                        //if (this.objPrior.PatientID > 0)
                        //{

                        //    if (oListControl != null)
                        //    {
                        //        for (int i = this.Controls.Count - 1; i >= 0; i--)
                        //        {
                        //            if (this.Controls[i].Name == oListControl.Name)
                        //            {
                        //                this.Controls.Remove(this.Controls[i]);
                        //                break;
                        //            }
                        //        }
                        //    }
                        //    panel1.Visible = false;
                        //    panel2.Visible = false;
                        //    panel3.Visible = false;
                        //    pnltlsStrip.Visible = false;

                        //    oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Referrals, true, this.Width );
                        //    oListControl.ClinicID = _ClinicID;
                        //    oListControl.ControlHeader = "Referral Provider";

                        //    oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        //    oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        //    oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                        //    //this.Width = 680;
                        //    //this.Height = 530;
                        //    this.Controls.Add(oListControl);

                        //    oListControl.OpenControl();
                        //    oListControl.Dock = DockStyle.Fill;
                        //    oListControl.BringToFront();
                        //this.Width = 680;
                        //this.Height = 530;
                        Int64 _currentPatientId = 0;
                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                        ogloPatient.ShowPatientRegistration(objPrior.PatientID, gloPatient.ModifyPatientDetailType.Referral, out _currentPatientId,this);
                        FillReferralProviders(_currentPatientId);
                        if (ogloPatient != null)
                        {
                            ogloPatient.Dispose();
                            ogloPatient = null;
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

            }
        }

        private void btnToProvider_Click(object sender, EventArgs e)
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
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick_Both);
                    }
                    catch { }
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
                    try
                    {
                        oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                    }
                    catch { }
                    try
                    {
                        oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                    }
                    catch { }
                }
                catch
                {
                }
                oListControl.Dispose();
                oListControl = null;
            }
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            pnltlsStrip.Visible = false;
            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Referrals, false, this.Width);
            oListControl.ClinicID = _ClinicID;
            oListControl.ControlHeader = "Referral Provider";

            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick_Both);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            oListControl.ModifyFormHandlerClick += new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
            //this.Width = 680;
            //this.Height = 530;
            this.Controls.Add(oListControl);
            oListControl.BringToFront();
            oListControl.Dock = DockStyle.Fill;
            oListControl.OpenControl();
            
            //this.Width = 680;
            // this.Height = 530;
        }

        private void oListControl_ModifyFormHandlerClick(object sender, EventArgs e)
        {

            if (oListControl.ControlHeader == "Referral Provider")
            {
                if (oListControl.dgListView.CurrentRow != null)
                {
                    _ContactID = Convert.ToInt64(oListControl.dgListView["nContactID", oListControl.dgListView.CurrentRow.Index].Value);
                }
                if (oListControl.dgListView.Rows.Count != 0)
                {
                    frmSetupPhysician ofrmModifyContact = new frmSetupPhysician(_ContactID, _databaseconnectionstring);
                    ofrmModifyContact.ShowDialog(this);

                    if (ofrmModifyContact.DialogResult == DialogResult.OK)
                    {
                        // _Ismodify = true;
                        oListControl.FillListAsCriteria(ofrmModifyContact.ContactID);

                        // oListControl.FillListAsCriteria1(ofrmModifyContact.ContactID, true);

                    }
                    ofrmModifyContact.Dispose();
                }

            }
        }

        private void cmbReferralProvider_MouseMove(object sender, MouseEventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbReferralProvider.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbReferralProvider.Items[cmbReferralProvider.SelectedIndex])["sReferralName"]), cmbReferralProvider) >= cmbReferralProvider.DropDownWidth - 18)
                    this.toolTip1.Show(Convert.ToString(((DataRowView)cmbReferralProvider.Items[cmbReferralProvider.SelectedIndex])["sReferralName"]), cmbReferralProvider, 0, cmbReferralProvider.Bottom - 40);
                else
                    this.toolTip1.Hide(combo);

            }
        }

        private void cmbInsurance_MouseMove(object sender, MouseEventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbInsurance.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsurance.Items[cmbInsurance.SelectedIndex])["sInsuranceName"]), cmbInsurance) >= cmbInsurance.DropDownWidth - 18)
                    this.toolTip1.Show(Convert.ToString(((DataRowView)cmbInsurance.Items[cmbInsurance.SelectedIndex])["sInsuranceName"]), cmbInsurance, 0, cmbInsurance.Bottom - 90);
                else
                    this.toolTip1.Hide(combo);

            }
        }

        private void cmbInsurance_MouseLeave(object sender, EventArgs e)
        {
            this.toolTip1.Hide(combo);
        }

        private void cmbReferralProvider_MouseLeave(object sender, EventArgs e)
        {
            this.toolTip1.Hide(combo);
        }

        private void mskAuthorizationstart_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                MaskedTextBox mskDate = (MaskedTextBox)sender;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Enter valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Enter valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }



        private void mskauthexp_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                MaskedTextBox mskDate = (MaskedTextBox)sender;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Enter valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Enter valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private void mskAuthorizationstart_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void mskauthexp_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void chklimityes_CheckedChanged(object sender, EventArgs e)
        {
            if (_tracklimit == false)
            {
                _tracklimit = true;
                chklimitno.Checked = false;
            }
            if (chklimityes.Checked == true)
            {
                if (mskauthexp.Enabled == false)
                {
                    if (objPrior.VisitsAllowed == null)
                    {
                        txtvisitsallow.Text = "";
                    }
                    else
                    {
                        txtvisitsallow.Text = Convert.ToString(objPrior.VisitsAllowed);
                    }

                    if (objPrior.StartDate > 0)
                    {

                        mskAuthorizationstart.Text = gloDateMaster.gloDate.DateAsDate(objPrior.StartDate).ToString("MM/dd/yyyy");
                    }
                    if (objPrior.ExpDate > 0)
                    {
                        mskauthexp.Text = gloDateMaster.gloDate.DateAsDate(objPrior.ExpDate).ToString("MM/dd/yyyy");
                    }
                }
                mskauthexp.Enabled = true;
                mskAuthorizationstart.Enabled = true;
                txtvisitsallow.Enabled = true;
                objPrior.IsTrackAuthLimit = true;
                if (objPrior.IsTrackAuthLimit == true && (lblvisitsremain.Text).Contains("-") == true)
                {
                    lblvisitsremain.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblvisitsremain.ForeColor = System.Drawing.Color.Black;
                }

                lblVisitsused.Text = (_globaltotalvisits).ToString();
                if (_globaltotalvisits == 0)
                {
                    lblVisitsused.Text = "";
                }

                _tracklimit = false;
            }
            else
            {
                mskauthexp.Enabled = false;
                mskAuthorizationstart.Enabled = false;
                txtvisitsallow.Enabled = false;
                txtvisitsallow.Text = "";
                lblVisitsused.Text = "";
                mskauthexp.ResetText();
                mskAuthorizationstart.ResetText();
                objPrior.IsTrackAuthLimit = false;
                _tracklimit = false;
            }
        }

        private void chklimitno_CheckedChanged(object sender, EventArgs e)
        {
            if (_tracklimit == false)
            {
                _tracklimit = true;
                chklimityes.Checked = false;
            }
            if (chklimityes.Checked == true)
            {
                if (mskauthexp.Enabled == false)
                {
                    if (objPrior.VisitsAllowed == null)
                    {
                        txtvisitsallow.Text = "";
                    }
                    else
                    {
                        txtvisitsallow.Text = Convert.ToString(objPrior.VisitsAllowed);
                    }

                    if (objPrior.StartDate > 0)
                    {

                        mskAuthorizationstart.Text = gloDateMaster.gloDate.DateAsDate(objPrior.StartDate).ToString("MM/dd/yyyy");
                    }
                    if (objPrior.ExpDate > 0)
                    {
                        mskauthexp.Text = gloDateMaster.gloDate.DateAsDate(objPrior.ExpDate).ToString("MM/dd/yyyy");
                    }
                }
                mskauthexp.Enabled = true;
                mskAuthorizationstart.Enabled = true;
                txtvisitsallow.Enabled = true;
                objPrior.IsTrackAuthLimit = true;
                if (objPrior.IsTrackAuthLimit == true && (lblvisitsremain.Text).Contains("-") == true)
                {
                    lblvisitsremain.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblvisitsremain.ForeColor = System.Drawing.Color.Black;
                }
                lblVisitsused.Text = (_globaltotalvisits).ToString();
                if (_globaltotalvisits == 0)
                {
                    lblVisitsused.Text = "";
                }
                _tracklimit = false;
            }
            else
            {
                mskauthexp.Enabled = false;
                mskAuthorizationstart.Enabled = false;
                txtvisitsallow.Enabled = false;
                txtvisitsallow.Text = "";
                lblVisitsused.Text = "";
                mskauthexp.ResetText();
                mskAuthorizationstart.ResetText();
                objPrior.IsTrackAuthLimit = false;
                _tracklimit = false;
            }
        }

        #endregion

        #region " Private Methods "

        private void DesignGrid()
          {
              try
              {
                  c1ProirAuthorization.Cols["ID"].Visible = false;
                  c1ProirAuthorization.Cols["RecordType"].Visible = false;
                  c1ProirAuthorization.Cols["Date"].Visible = true;
                  c1ProirAuthorization.Cols["sReferralName"].Visible = true;
                  c1ProirAuthorization.Cols["Description"].Visible = true;
                  c1ProirAuthorization.Cols["units"].Visible = false;
                  c1ProirAuthorization.Cols["charges"].Visible = false;
                  c1ProirAuthorization.Cols["StartTime"].Visible = false;
                 
                  #region " Set Header "

                  c1ProirAuthorization.Cols["ID"].Caption = "";
                  c1ProirAuthorization.Cols["RecordType"].Caption = "Auth#";
                  c1ProirAuthorization.Cols["Date"].Caption = "Date";
                  c1ProirAuthorization.Cols["sReferralName"].Caption = "Provider";
                  c1ProirAuthorization.Cols["Description"].Caption = "Description";                

                  #endregion

                  int _nWidth = 0;
                  _nWidth = 826;//Convert.ToInt32( c1QueuedClaims.Width);
                  c1ProirAuthorization.Cols["ID"].Width = 0;
                  c1ProirAuthorization.Cols["RecordType"].Width = 0;
                  c1ProirAuthorization.Cols["StartTime"].Width = 0;
                  c1ProirAuthorization.Cols["Date"].Width = Convert.ToInt32(_nWidth * 0.10);

                  c1ProirAuthorization.Cols["sReferralName"].Width = Convert.ToInt32(_nWidth * 0.30);

                  c1ProirAuthorization.Cols["Description"].Width = Convert.ToInt32(_nWidth * 0.60);

                  c1ProirAuthorization.Cols["Date"].DataType = typeof(System.DateTime);
                  c1ProirAuthorization.Cols["Date"].Format = "MM/dd/yyyy";

                  c1ProirAuthorization.Cols[11].DataType = typeof(System.Decimal);
                  c1ProirAuthorization.Cols[11].Format = "#############0.####";

              }
              catch //(Exception ex)
              {

              }
          }


        void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
        {

            combo = (ComboBox)sender;
            if (combo.Items.Count > 0 && e.Index >= 0)
            {

                e.DrawBackground();
                using (SolidBrush br = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(combo.GetItemText(combo.Items[e.Index]).ToString(), e.Font, br, e.Bounds);
                }

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    if (combo.DroppedDown)
                    {
                        if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth)
                            this.tooltip_Rpt.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 180, e.Bounds.Bottom);
                    }
                    else
                    {
                        tooltip_Rpt.Hide(combo);
                    }
                }
                else
                {
                    tooltip_Rpt.Hide(combo);
                }
                e.DrawFocusRectangle();
            }
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g!=null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            
            return width;
        }


        private void GetPriorAuthorizations(Int64 AuthorizationID)
        {
            lblActivate.Text = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsPrior = new DataSet();
            try
            {
                oDB.Connect(false);
             
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPriorAuthorizationID", _CurrentPriorAuthorization, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("GET_PriorAuthorization_ForModify_Revised", oDBParameters, out dsPrior);

                //oDB.Retrive("GET_PriorAuthorization_ForModify", oDBParameters,out dsPrior);
                //c1ProirAuthorization.DataSource = dtPrior.DefaultView;


                #region "Assign To Object and Grid"

                DataTable dtPrior = dsPrior.Tables[0];
                objPrior = new gloPMGeneral.gloPriorAuthorization.clsgloPriorAuthorization();
                if (dtPrior != null && dtPrior.Rows.Count > 0)
                {
                    objPrior.AuthorizationNote = Convert.ToString(dtPrior.Rows[0]["sAuthorizationNote"]);
                    objPrior.AuthorizationType = Convert.ToInt32(dtPrior.Rows[0]["nAuthorizationType"]);
                    objPrior.StartDate = Convert.ToInt32(dtPrior.Rows[0]["nStartDate"]);
                    objPrior.ExpDate = Convert.ToInt32(dtPrior.Rows[0]["nExpDate"]);
                    objPrior.InsuranceID = Convert.ToInt64(dtPrior.Rows[0]["nInsuranceID"]);
                    objPrior.InsuranceNote = Convert.ToString(dtPrior.Rows[0]["sInsuranceNote"]);
                    objPrior.IsActive = Convert.ToBoolean(dtPrior.Rows[0]["bIsActive"]);
                    objPrior.IsTrackAuthLimit = Convert.ToBoolean(dtPrior.Rows[0]["bIsTrackAuthLimit"]);
                    _IsTrackAuthLimit = objPrior.IsTrackAuthLimit;
                    objPrior.PatientID = Convert.ToInt64(dtPrior.Rows[0]["nPatientID"]);
                    objPrior.PatientName = Convert.ToString(dtPrior.Rows[0]["sPatientName"]);
                    objPrior.PriorAuthorizationID = Convert.ToInt64(dtPrior.Rows[0]["nPriorAuthorizationID"]);
                    objPrior.PriorAuthorizationNo = Convert.ToString(dtPrior.Rows[0]["sPriorAuthorizationNo"]);
                  
                    if (Convert.ToString(dtPrior.Rows[0]["nReferralID"]) != "")
                    {
                        objPrior.ReferralID = Convert.ToInt64(dtPrior.Rows[0]["nReferralID"]);
                    }
                    objPrior.ReferralName = Convert.ToString(dtPrior.Rows[0]["sReferralName"]);

                    if (Convert.ToString(dtPrior.Rows[0]["nToReferralID"]) != "")
                    {
                        objPrior.ToReferralID = Convert.ToInt64(dtPrior.Rows[0]["nToReferralID"]);
                    }
                    objPrior.ToReferralName = Convert.ToString(dtPrior.Rows[0]["sToReferralName"]);
                    if (dtPrior.Rows[0]["nVisitsAllowed"].ToString().Trim() == "")
                    {
                        objPrior.VisitsAllowed = null;
                    }
                    else
                    {
                        objPrior.VisitsAllowed = Convert.ToInt32(dtPrior.Rows[0]["nVisitsAllowed"]);
                    }
                    objPrior.ReferralName = Convert.ToString(dtPrior.Rows[0]["sReferralName"]);
                    objPrior.ToReferralName = Convert.ToString(dtPrior.Rows[0]["sToReferralName"]);

                    objPrior.IsActive = Convert.ToBoolean(dtPrior.Rows[0]["bIsActive"]);
                    if (objPrior.IsActive == true)
                    {
                        panel7.Visible = false;
                        tsb_Activate.Visible = false;
                        tsb_Deactivate.Visible = true; 
                    }
                    else
                    {
                        panel7.Visible = true;
                        tsb_Activate.Visible = true ;
                        tsb_Deactivate.Visible = false; 
                    }


                #endregion

                #region "Set Other Data"

                    //if (objPrior.IsActive == false)
                    //{
                    //    //Icon
                    //    //tsb_Activate.Visible = false;
                    //    tsb_Activate.Text="Activate";
                    //    tsb_Activate.ToolTipText = "Activate";
                    //}
                    //else
                    //{
                    //    //tsb_Activate.Visible = fa;
                    //    tsb_Activate.Text = "&Deactivate";
                    //    tsb_Activate.ToolTipText = "Deactivate";
                    //}
                    lblPatientName1.Text = Convert.ToString(objPrior.PatientName).Trim();
                    FillInsurance(objPrior.PatientID);
                    FillReferralProviders(objPrior.PatientID);
                    if (objPrior.AuthorizationType == 1)
                    {
                        rdbreferralin.Checked = true;
                    }
                    else if (objPrior.AuthorizationType == 2)
                    {
                        rdbreferralout.Checked = true;
                    }
                    else if (objPrior.AuthorizationType == 3)
                    {
                        rdbboth.Checked = true;
                    }
        
                    txtauth.Text = objPrior.PriorAuthorizationNo;
                    //txtRefrovider.Text = objPrior.ReferralName;
                    //txtRefrovider.Tag = objPrior.ReferralID;
                //    if (objPrior.ReferralID != null)
                    {
                        cmbReferralProvider.SelectedValue = Convert.ToInt64(objPrior.ReferralID);
                    }
                    txttoprovider.Text = objPrior.ToReferralName;
                    txttoprovider.Tag = objPrior.ToReferralID;
                    txtAuthorizationNote.Text = objPrior.AuthorizationNote;
                    #region

                    //txtRefrovider.Text = GetPatientReferral(objPrior.ReferralID);

                    //txtToRefProvider.Text = GetPatientToReferral(objPrior.ToReferralID);



                    #endregion
                    #region "Limit Tracking"
                    if (objPrior.AuthorizationType == 2)
                    {
                        chklimityes.Checked = false;
                        chklimitno.Checked = false;
                    }
                    else
                    {
                        if (objPrior.IsTrackAuthLimit == true)
                        {
                            chklimityes.Checked = true;
                        }
                        else
                        {
                            chklimitno.Checked = true;
                        }
                    }
                    #endregion

                    #region "Date"

                    if (objPrior.StartDate > 0)
                    {
                        mskAuthorizationstart.Text = gloDateMaster.gloDate.DateAsDate(objPrior.StartDate).ToString("MM/dd/yyyy");
                    }

                    if (objPrior.ExpDate > 0)
                    {
                        mskauthexp.Text  = gloDateMaster.gloDate.DateAsDate(objPrior.ExpDate).ToString("MM/dd/yyyy");
                    }

                    #endregion

                    #region "Visits Allowed"
                    this.txtvisitsallow.TextChanged -= new System.EventHandler(this.txtvisitsallow_TextChanged);
                    if (objPrior.VisitsAllowed == null)
                    {
                        txtvisitsallow.Text = "";
                    }
                    else
                    {
                        txtvisitsallow.Text = Convert.ToString(objPrior.VisitsAllowed);
                    }

                    #endregion

                    #region Insurance

                  

                    if (objPrior.InsuranceID > 0)
                    {
                        if (cmbInsurance.Items.Count > 0)
                        {
                            cmbInsurance.SelectedValue = objPrior.InsuranceID;
                        }
                    }
                    else
                    {
                        cmbInsurance.SelectedIndex = 0;
                    }

                    if (objPrior.InsuranceNote != "")
                    {
                            txtInsurancenote.Text = Convert.ToString(objPrior.InsuranceNote);   
                    }
                    #endregion


                    #region "Last DOS"
                    string _LastDOS = "";
                    Int64 LastDOS = 0, LastApp = 0;
                    //1 Used in charges
                    DataTable dtLastCharges = dsPrior.Tables[1];
                    //2 Used in Appointment
                    DataTable dtLastAppt = dsPrior.Tables[2];


                    if (dtLastCharges != null && dtLastCharges.Rows.Count > 0)
                        if (Convert.ToInt64(dtLastCharges.Rows[0]["LastDOS"]) > 0)
                            LastDOS = Convert.ToInt64(dtLastCharges.Rows[0]["LastDOS"]);
                    if (dtLastAppt != null && dtLastAppt.Rows.Count > 0)
                        if (Convert.ToInt64(dtLastAppt.Rows[0]["LastDOS"]) > 0)
                            LastApp = Convert.ToInt64(dtLastAppt.Rows[0]["LastDOS"]);

                    if (LastDOS > LastApp)
                        _LastDOS = LastDOS.ToString();
                    else
                        _LastDOS = LastApp.ToString();

                    if (_LastDOS != "" && _LastDOS != "0")
                    {
                        lbllastdos.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_LastDOS)).ToString("MM/dd/yyyy");
                    }

                    #endregion

                    #region "Next DOS"
                    string _NextDOS = "";
                    Int64 NextDOS = 0, NextApp = 0;
                    //3 Used in charges
                    DataTable dtNextCharges = dsPrior.Tables[3];
                    //4 Used in Appointment
                    DataTable dtNextAppt = dsPrior.Tables[4];

                    if (dtNextCharges != null && dtNextCharges.Rows.Count > 0)
                        if (Convert.ToInt64(dtNextCharges.Rows[0]["LastDOS"]) > 0)
                            NextDOS = Convert.ToInt64(dtNextCharges.Rows[0]["LastDOS"]);
                    if (dtNextAppt != null && dtNextAppt.Rows.Count > 0)
                        if (Convert.ToInt64(dtNextAppt.Rows[0]["LastDOS"]) > 0)
                            NextApp = Convert.ToInt64(dtNextAppt.Rows[0]["LastDOS"]);


                    if (NextDOS != 0)
                    {
                        if (NextApp != 0)
                        {
                            if (NextApp > NextDOS)
                            {
                                _NextDOS = NextDOS.ToString();
                            }
                            else
                            {
                                _NextDOS = NextApp.ToString();
                            }
                        }
                        else
                        {
                            _NextDOS = NextDOS.ToString();
                        }
                    }
                    else
                    {
                        _NextDOS = NextApp.ToString();
                    }

                    if (_NextDOS != "" && _NextDOS != "0")
                    {
                        lblnextdos.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_NextDOS)).ToString("MM/dd/yyyy");
                    }


                    #endregion

                    #region  "Visits Used"
                    //5 Actual Visits used in charges
                    //Allowed-Used=Remaining.
                    
                      DataTable dtVisitsCharges = dsPrior.Tables[5];
                      string _chargesVists="";
                    //  if (dtVisitsCharges != null && dtVisitsCharges.Rows.Count > 0)
                    //  {
                    //      _chargesVists = Convert.ToString(dtVisitsCharges.Rows[0]["VisitsUsed"]);
                    //  }

                    ////6 Actual Visits used in Appointment
                    //  DataTable dtVisitsAppt = dsPrior.Tables[6];
                    //  string _ApptVists = "";
                    //  if (dtVisitsAppt != null && dtVisitsAppt.Rows.Count > 0)
                    //  {
                    //      _ApptVists = Convert.ToString(dtVisitsAppt.Rows[0]["VisitsUsed"]);
                    //  }
                      int _ichargesVists = 0;
                      int _iVistsAllowed = 0;
                    //  int _iApptVists = 0;
                    //  if (_chargesVists != "") { _ichargesVists = Convert.ToInt32(_chargesVists); }
                    //  if (_ApptVists != "") { _iApptVists=Convert.ToInt32(_ApptVists); }
                    //  int _TotalVisits;
                    //  _TotalVisits=_ichargesVists + _iApptVists;
                    if (dtVisitsCharges != null && dtVisitsCharges.Rows.Count > 0)
                      {
                          _chargesVists = Convert.ToString(dtVisitsCharges.Rows[0]["VisitsUsed"]);
                          if (Convert.ToString(dtVisitsCharges.Rows[0]["VisitsUsed"]) != "")
                        {
                            _ichargesVists = Convert.ToInt32(dtVisitsCharges.Rows[0]["VisitsUsed"]);
                        }
                      }
                    

                    if (objPrior.VisitsAllowed != null && Convert.ToString(objPrior.VisitsAllowed) != "")
                    {
                        _iVistsAllowed = Convert.ToInt32(objPrior.VisitsAllowed);
                    }
                    
                        _globaltotalvisits = _ichargesVists;
                   
                    //lblVisitsused.Text = _chargesVists.ToString();
                        lblvisitsremain.Text = (_iVistsAllowed - _ichargesVists).ToString();
                      lblVisitsused.Text = (_ichargesVists).ToString();
                      if (_ichargesVists == 0 || objPrior.AuthorizationType == 2 || chklimitno.Checked == true )
                      {
                          lblVisitsused.Text = "";
                      }
                      if (objPrior.IsTrackAuthLimit == true && (_iVistsAllowed - _ichargesVists) < 0)
                      {
                          lblvisitsremain.ForeColor = System.Drawing.Color.Red;
                      }
                      else
                      {
                          lblvisitsremain.ForeColor = System.Drawing.Color.Black;
                      }
                        
                     if(objPrior.VisitsAllowed == null)
                     {
                         lblvisitsremain.Text = "";
                     }

                      this.txtvisitsallow.TextChanged += new System.EventHandler(this.txtvisitsallow_TextChanged);
                    #endregion

                    #region "Grid"


                      c1ProirAuthorization.DataSource = dsPrior.Tables[6];
                      DesignGrid();

                     string _RecordType = "";
                     bool _HasCharges = false;
                     decimal _Units=0;
                     decimal _Charges=0;
                    C1.Win.C1FlexGrid.CellStyle cs;// = c1ProirAuthorization.Styles.Add("cs_Red");
                    try
                    {
                        if (c1ProirAuthorization.Styles.Contains("cs_Red"))
                        {
                            cs = c1ProirAuthorization.Styles["cs_Red"];
                        }
                        else
                        {
                            cs = c1ProirAuthorization.Styles.Add("cs_Red");

                        }

                    }
                    catch
                    {
                        cs = c1ProirAuthorization.Styles.Add("cs_Red");

                    }
                    cs.ForeColor = Color.Red;
                                        
                      for (int i = 1; i < c1ProirAuthorization.Rows.Count; i++)
                      {
                          //1==Charges , 2=Appointment
                         _RecordType="";
                         _RecordType = Convert.ToString(c1ProirAuthorization.GetData(i, 1));
                         if (_RecordType == "1")
                         {
                             _HasCharges = true;
                             if (Convert.ToString(_Units) != "")
                             {
                                 _Units = _Units + Convert.ToDecimal(c1ProirAuthorization.GetData(i, "Units"));
                             }
                             if (Convert.ToString(_Charges) != "")
                             {
                                 _Charges = _Charges + Convert.ToDecimal(c1ProirAuthorization.GetData(i, "charges"));
                             }
                             //_HasCharges = true;
                         }
                         else if (_RecordType=="2")
                         { 
                             if(objPrior.IsTrackAuthLimit==true && (objPrior.StartDate>gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1ProirAuthorization.GetData(i, "Date")))||objPrior.ExpDate<gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1ProirAuthorization.GetData(i, "Date")))))
                             {
                                 c1ProirAuthorization.SetCellStyle(i,c1ProirAuthorization.Cols["Date"].Index ,cs);
                                 c1ProirAuthorization.SetCellStyle(i, c1ProirAuthorization.Cols["Description"].Index, cs);
                                 c1ProirAuthorization.SetCellStyle(i, c1ProirAuthorization.Cols["sReferralName"].Index, cs);
                                 
                             }
                         }
                      }

                      if (_HasCharges == true)
                      {
                          pnlTotal.Visible = true;
                          lblUnits.Text = Convert.ToDecimal(_Units).ToString("#############0.####");
                          lblCharges.Text = "$"+_Charges.ToString();
                      }
                    #endregion

                      if (objPrior.IsActive == false)
                      {
                          lblActivate.Text = "Inactive"; 
                      }
                    //SetData();
                }
                    #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally 
            {
                this.txtvisitsallow.TextChanged += new System.EventHandler(this.txtvisitsallow_TextChanged);
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }
                    
        }

        private void SetData()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { 
            }
        }

        private bool Validate()
        {
          
                try
            {
                if(txtauth.Text.Trim() =="")
                {
                    MessageBox.Show("Enter prior authorization #.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtauth.Focus();
                    return false;                  
                }

                if (rdbreferralout.Checked == false)
                {
                    if (chklimityes.Checked == false && chklimitno.Checked == false)
                    {
                        MessageBox.Show("Select track authorization limits.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chklimitno.Focus();
                        return false;
                    }
                }
               
                if (rdbreferralout.Checked == false)
                {
                    if (chklimityes.Checked == true)
                    {
                        if (mskAuthorizationstart.Text.Replace("/", "").Trim().Length == 0)
                        {
                            MessageBox.Show("Enter start date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskAuthorizationstart.Focus();
                            return false;
                        }
                        if (mskauthexp.Text.Replace("/", "").Trim().Length == 0)
                        {
                            MessageBox.Show("Enter expiration date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskauthexp.Focus();
                            return false;
                        }
                        if (IsValidDate(mskAuthorizationstart.Text.Trim()) == false)
                        {
                            MessageBox.Show("Enter valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskAuthorizationstart.Focus();
                            return false;
                        }
                        if (IsValidDate(mskauthexp.Text.Trim()) == false)
                        {
                            MessageBox.Show("Enter valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskauthexp.Focus();
                            return false;
                        }

                        if (cmbInsurance.Text.Trim() == "" && txtInsurancenote.Text.Trim() == "")
                        {
                           // MessageBox.Show("Insurance information is reruired for expiration tracking authorizations.\nSelect the patient's insurance plan or describe the insurance in the insurance note.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MessageBox.Show("Insurance information is required for track authorization limits.\nSelect the patient's insurance plan or describe the insurance in the insurance note.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                            cmbInsurance.Focus();
                            return false;
                        }
                       
                        if (Convert.ToDateTime(mskAuthorizationstart.Text.Trim()).Date > Convert.ToDateTime(mskauthexp.Text.Trim()).Date)
                        {
                            MessageBox.Show("Expiration date should be equal to or greater than start date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskauthexp.Focus();
                            return false;
                        }
                    }
                }
                if (txtauth.Text.Trim() != objPrior.PriorAuthorizationNo.ToString().Trim())
                {
                    if (IsNumberUsed(txtauth.Text.Trim(), objPrior.PatientID, ClinicID, objPrior.PriorAuthorizationID) == true)
                    {
                        if (DialogResult.Yes != MessageBox.Show("Prior authorization " + txtauth.Text.Trim() + " already exists for patient " + objPrior.PatientName.ToString() + ".\nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                        {
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

        private bool IsNumberUsed(string _AuthNo, Int64 PatientID, Int64 ClinicID,Int64 PriorAuthorizationID)
        {
            string _strquery = "";
            gloDatabaseLayer.DBLayer ODB = null;
            object _retval = null;
            bool _result = false;
            try
            {
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                _strquery = "Select  Count(PriorAuthorization_Mst.nPriorAuthorizationID) from  PriorAuthorization_Mst WITH(NOLOCK) " +
                          "where ISNULL(PriorAuthorization_Mst.sPriorAuthorizationNo,'')='" + _AuthNo.Replace("'", "''") + "' and " +
                          " PriorAuthorization_Mst.nPatientID=" + PatientID + " and PriorAuthorization_Mst.nClinicID=" + ClinicID + " and PriorAuthorization_Mst.nPriorAuthorizationID<>" + PriorAuthorizationID + "";
                _retval = ODB.ExecuteScalar_Query(_strquery);
                if (_retval != null && Convert.ToInt64(_retval) > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Dispose();
                    ODB.Disconnect();
                }
            }
            return _result;
        }

        private void FillInsurance(Int64 PatientId)
        {
            try
            {
                DataTable dt;
                dt = GetPatientInsurance(PatientId);

                if (dt != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["nInsuranceID"] = 0;
                    dr["sInsuranceName"] = "";
                    dt.Rows.InsertAt(dr, 0);

                    if (dt.Rows.Count > 0)
                    {
                        cmbInsurance.DataSource = dt;
                        cmbInsurance.ValueMember = dt.Columns["nInsuranceID"].ColumnName;
                        cmbInsurance.DisplayMember = dt.Columns["sInsuranceName"].ColumnName;
                        cmbInsurance.Refresh();

                        if (cmbInsurance.Items.Count > 1)
                        { cmbInsurance.SelectedIndex = 1; }
                        else
                        { cmbInsurance.SelectedIndex = 0; }
                    }


                }
                dt = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }

        }

        private void FillReferralProviders(Int64 PatientId)
        {
            try
            {
                DataTable dt;
                dt = GetPatientReferral(PatientId);

                if (dt != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["nReferralID"] = 0;
                    dr["sReferralName"] = "";
                    dt.Rows.InsertAt(dr, 0);

                    if (dt.Rows.Count > 0)
                    {
                        cmbReferralProvider.DataSource = dt;
                        cmbReferralProvider.ValueMember = dt.Columns["nReferralID"].ColumnName;
                        cmbReferralProvider.DisplayMember = dt.Columns["sReferralName"].ColumnName;
                        cmbReferralProvider.Refresh();

                        if (cmbReferralProvider.Items.Count > 1)
                        { cmbReferralProvider.SelectedIndex = 0; }
                        else
                        { cmbReferralProvider.SelectedIndex = 0; }
                    }


                }
                dt = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }

        }

        private DataTable GetPatientReferral(Int64 PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtReferrals = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT  ISNULL(Contacts_MST.nContactID,0) as nReferralID, " +
                                " ISNULL(Contacts_MST.sFirstName,'')+ SPACE(1) + ISNULL(Contacts_MST.sMiddleName,'') +SPACE(1)+ISNULL(Contacts_MST.sLastName,'') AS sReferralName  " +
                                " FROM Contacts_MST WITH(NOLOCK) LEFT OUTER JOIN Contacts_Physician_DTL WITH(NOLOCK) ON Contacts_MST.nContactID = Contacts_Physician_DTL.nContactID " +
                                " WHERE(ISNULL(Contacts_MST.bIsBlocked,0) = 0) AND (Contacts_MST.sContactType = 'Physician')  and Contacts_MST.nContactID IN " +
                                " (Select nContactID from Patient_DTL where nPatientID=" + PatientId + " and ISNULL(nContactFlag,0)=3) order by sReferralName";

                oDB.Retrive_Query(_sqlQuery, out dtReferrals);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                //if (dtReferrals != null) { dtReferrals.Dispose(); }
            }

            return dtReferrals;
        }

        private DataTable GetPatientInsurance(Int64 PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtInsurances = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT nInsuranceID,sInsuranceName,CASE ISNULL(nInsuranceFlag, 0) WHEN 0 THEN 4 WHEN 1 THEN 1 WHEN 2 THEN 2 WHEN 3 THEN 3 END As SortOrder FROM PatientInsurance_DTL WITH(NOLOCK) WHERE nPatientID=" + PatientId +" ORDER BY SortOrder";
                oDB.Retrive_Query(_sqlQuery, out dtInsurances);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                //if (dtInsurances != null) { dtInsurances.Dispose(); }
            }

            return dtInsurances;
        }

        //private string GetPatientReferral(Int64 ContactID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string strReferrals="";
        //    DataTable dtReferrals;
        //    string _sqlQuery = "";

        //    try
        //    {
        //        oDB.Connect(false);
        //        _sqlQuery = "SELECT ISNULL(sFirstName,'') +SPACE(1)+ISNULL(sMiddleName,'')+SPACE(1) + ISNULL(sLastName,'') AS sReferralName from contacts_Mst where nContactID=" + ContactID + " AND nClinicID = " + _ClinicID + " " +
        //          " AND sContactType  = 'Physician' ";
        //        oDB.Retrive_Query(_sqlQuery, out dtReferrals);
        //        if (dtReferrals != null && dtReferrals.Rows.Count > 0)
        //        {
        //            strReferrals = Convert.ToString(dtReferrals.Rows[0]["sReferralName"]);
        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //        //if (dtReferrals != null) { dtReferrals.Dispose(); }
        //    }

        //    return strReferrals;
        //}

        private string GetPatientToReferral(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strReferrals="";
            DataTable dtReferrals=null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT ISNULL(sFirstName,'') +SPACE(1)+ISNULL(sMiddleName,'')+SPACE(1) + ISNULL(sLastName,'') AS sReferralName from contacts_Mst WITH(NOLOCK) where nContactID=" + ContactID + " AND nClinicID = " + _ClinicID + " " +
                  " AND sContactType  = 'Physician' ";
                oDB.Retrive_Query(_sqlQuery, out dtReferrals);
                if (dtReferrals != null && dtReferrals.Rows.Count > 0)
                {
                    strReferrals = Convert.ToString(dtReferrals.Rows[0]["sReferralName"]);
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtReferrals != null) { dtReferrals.Dispose(); }
            }

            return strReferrals;
        }

        private bool SavePriorAuthorization()
        {


            try
            {
                //_CurrentPriorAuthorizationNo = txtauth.Text.Replace("'", "''");
                //clsgloPriorAuthorization objgloPriorAuth = new clsgloPriorAuthorization();
                objPrior.PriorAuthorizationNo = txtauth.Text;
                objPrior.PriorAuthorizationID = _CurrentPriorAuthorization;
                //objgloPriorAuth.PatientID = _PatientID;
                if (rdbreferralin.Checked == true)
                {
                    objPrior.ReferralID = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                    objPrior.ToReferralID = 0;
                }
                else if (rdbreferralout.Checked == true)
                {
                    objPrior.ReferralID = 0;
                    objPrior.ToReferralID = Convert.ToInt64(txttoprovider.Tag);
                }
                else if (rdbboth.Checked == true)
                {
                    objPrior.ReferralID = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                    objPrior.ToReferralID = Convert.ToInt64(txttoprovider.Tag);
                }
                objPrior.InsuranceID = Convert.ToInt64(cmbInsurance.SelectedValue);
                if (mskAuthorizationstart.Enabled == false)
                {
                    objPrior.StartDate = 0;
                }
                else
                {
                    if (mskAuthorizationstart.MaskCompleted == true)
                    {
                        mskAuthorizationstart.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        objPrior.StartDate = gloDateMaster.gloDate.DateAsNumber(mskAuthorizationstart.Text);
                    }
                }
                if (mskauthexp.Enabled == false)
                {
                    objPrior.ExpDate = 0;
                }
                else
                {
                    if (mskauthexp.MaskCompleted == true)
                    {
                        mskauthexp.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        objPrior.ExpDate = gloDateMaster.gloDate.DateAsNumber(mskauthexp.Text);
                    }
                }

                if (rdbreferralout.Checked == false)
                {
                    if (chklimityes.Checked == true)
                    {
                        objPrior.IsTrackAuthLimit = true;
                    }
                    else
                    {
                        objPrior.IsTrackAuthLimit = false;
                    }
                }
                else
                {
                    objPrior.IsTrackAuthLimit = false;                
                }
                //objPrior.IsActive = true;
                if (txtvisitsallow.Text.ToString().Trim() == "")
                {
                    objPrior.VisitsAllowed = null;
                }
                else
                {
                    objPrior.VisitsAllowed = Convert.ToInt64(txtvisitsallow.Text.ToString().Trim());
                }
                objPrior.InsuranceNote = txtInsurancenote.Text.Trim();
                if (rdbreferralin.Checked == true)
                {
                    objPrior.AuthorizationType = clsgloPriorAuthorization.AuthorizationTypeEnum.ReferralIn.GetHashCode();
                }
                else if (rdbreferralout.Checked == true)
                {
                    objPrior.AuthorizationType = clsgloPriorAuthorization.AuthorizationTypeEnum.ReferralOut.GetHashCode();
                }
                else if (rdbboth.Checked == true)
                {
                    objPrior.AuthorizationType = clsgloPriorAuthorization.AuthorizationTypeEnum.Both.GetHashCode();
                }

                objPrior.AuthorizationNote = txtAuthorizationNote.Text.ToString().Trim();

                objPrior.Update();
                _CurrentPriorAuthorization = objPrior.PriorAuthorizationID;
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

        void oListControl_AddFormHandlerClick(object sender, EventArgs e)
        {
            if (oListControl.ControlHeader == "Referral Provider")
            {

                gloContacts.frmSetupPhysician ofrmAddContact = new gloContacts.frmSetupPhysician(_databaseconnectionstring);
                ofrmAddContact.ShowDialog(this);
                ofrmAddContact.Dispose();
                oListControl.FillListAsCriteria(ofrmAddContact.ContactID);

            }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            // this.Width = 930;
            //     this.Height = 602;
            panel1.Visible = true;
            panel2.Visible = true;
            pnltlsStrip.Visible = true;
            panel3.Visible = true;

        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            if (oListControl.SelectedItems.Count > 0)
            {
                //if (rdbreferralout.Checked == true)
                //{

                for (int _Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                {
                    txttoprovider.Tag = oListControl.SelectedItems[0].ID;
                    txttoprovider.Text = oListControl.SelectedItems[0].Description;
                }
                //}
                //else
                //{
                //    for (int _Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                //    {


                //        DataTable dt;
                //        dt = GetPatientReferral(objPrior.PatientID);

                //        if (dt != null)
                //        {
                //            DataRow dr = dt.NewRow();
                //            dr["nReferralID"] = 0;
                //            dr["sReferralName"] = "";
                //            dt.Rows.InsertAt(dr, 0);

                //            for (int Counter = 0; Counter <= oListControl.SelectedItems.Count - 1; Counter++)
                //            {
                //                dt.Rows.Add(oListControl.SelectedItems[Counter].ID, oListControl.SelectedItems[Counter].Description);
                //            }

                //            if (dt.Rows.Count > 0)
                //            {
                //                cmbReferralProvider.DataSource = dt.Copy();
                //                cmbReferralProvider.ValueMember = dt.Columns["nReferralID"].ColumnName;
                //                cmbReferralProvider.DisplayMember = dt.Columns["sReferralName"].ColumnName;
                //                cmbReferralProvider.Refresh();

                //                if (cmbReferralProvider.Items.Count > 1)
                //                { cmbReferralProvider.SelectedIndex = 1; }
                //                else
                //                { cmbReferralProvider.SelectedIndex = 0; }
                //            }
                //        }



                //    }

                //}

            }
            //   this.Width = 930;
            //  this.Height = 602;
            panel1.Visible = true;
            panel2.Visible = true;
            pnltlsStrip.Visible = true;
            panel3.Visible = true;

        }

        void oListControl_ItemSelectedClick_Both(object sender, EventArgs e)
        {
            if (oListControl.SelectedItems.Count > 0)
            {

                  gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dtReferrals = null;
                string _sqlQuery = "";
                try
                {
               
                    oDB.Connect(false);
                for (int _Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                {
                    txttoprovider.Tag = oListControl.SelectedItems[0].ID;

                     _sqlQuery = "SELECT ISNULL(Contacts_MST.sFirstName,'')+SPACE(1)+ISNULL(Contacts_MST.sMiddleName,'') +SPACE(1)+ISNULL(Contacts_MST.sLastName,'') AS sReferralName  " +
                                    " FROM Contacts_MST WITH(NOLOCK) where  ISNULL(Contacts_MST.nContactID,0)=" + oListControl.SelectedItems[0].ID;
                        oDB.Retrive_Query(_sqlQuery, out dtReferrals);
                        txttoprovider.Text = dtReferrals.Rows[0][0].ToString().Trim();
           
                }

                oDB.Disconnect();
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); }
                    if (dtReferrals != null) { dtReferrals.Dispose(); }
                }
            }
            //   this.Width = 930;
            //  this.Height = 602;
            panel1.Visible = true;
            panel2.Visible = true;
            pnltlsStrip.Visible = true;
            panel3.Visible = true;

        }

        private bool IsValidDate(object strDate)
        {
            bool Success;
            try
            {
                DateTime validatedDate;
                Success = DateTime.TryParseExact(strDate.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);
                if (validatedDate != null && Success == true)
                {
                    if (validatedDate < DateTime.MaxValue && validatedDate >= Convert.ToDateTime("01/01/1900"))
                    {
                        Success = true;
                    }
                    else
                    {
                        Success = false;
                    }

                }
            }
            catch (FormatException)
            {
                Success = false; // If this line is reached, an exception was thrown

            }
            return Success;
        }

        #endregion " Private Methods "
      
    }
}