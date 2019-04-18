using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloSettings;
using System.IO;
using gloPatient;
using gloPatientStripControl;
using System.Text.RegularExpressions;
using System.Linq;

using System.Collections;
using System.Globalization;
using System.Resources;

namespace gloStripControl
{
    //** ucPatientStripControl form and functionality provided by Mahesh Satlapalli(Apollo) **/
    public partial class ucPatientStripControl : UserControl
    {

        #region " Constants "

        // Patient Grid.
        private const int COL_PAT_ID = 0;
        private const int COL_PAT_Code = 1;
        private const int COL_PAT_FirstName = 2;
        private const int COL_PAT_MI = 3;
        private const int COL_PAT_LastName = 4;
        private const int COL_PAT_SSN = 5;
        private const int COL_PAT_Provider = 6;
        private const int COL_PAT_DOB = 7;
        private const int COL_PAT_Phone = 8;
        private const int COL_PAT_Mobile = 9;

        //Insurance Grid.
        const int COL_SELECT = 0;
        const int COL_PARTY = 1;
        const int COL_INSURANCEID = 2;
        const int COL_INSURANCENAME = 3;
        const int COL_INSURANCETYPE = 4;
        const int COL_INSURANCE_STATUS = 5;
        const int COL_INSSELFMODE = 6;
        const int COL_INSURANCECOPAYAMT = 7;
        const int COL_INSURANCEWORKERCOMP = 8;
        const int COL_INSURANCEAUTOCLAIM = 9;
        const int COL_CONTACTID = 10;
        const int COL_COPAY = 11;
        const int COL_SUBSCRIBERID = 12;
        const int COL_GROUP = 13;
        const int COL_INSVIEW_COUNT = 14;
        
        #endregion

        #region " Variables "

        //added by Mahesh Satlapalli(Apollo)
       
        string _DataBaseConnectionString = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        gloDatabaseLayer.DBLayer oDB = null;
        gloDatabaseLayer.DBParameters oDBPara = null;

        private Int64 _ClinicID = 1;

        private bool _IsPatientAccountFeature = false;
        
        string _Age = string.Empty;
        bool _AllowEditingParty = true;
        //DataView dvPatient = new DataView();
        //DataTable dtTemp = new DataTable();
        //DataView dvNext = new DataView();
        Int32 StripHeight = 110; //95;
        
        //Patient Fields
        Int64 _PatientID = 0;
        string _PatientCode = "";
        string _PatientName = "";
        string _ProviderName = "";
        string _PatientMedicalCtegory = "";
        string _NextAppointment = "";
        string _EMRAlert = "";

        private Int64 _ClaimNumber = 0;
        private string _SubClaimNumber = string.Empty;
        
        //PAF Main Ids
        private Int64 _nPAccountID = 0;
        private Int64 _nGuarantorID = 0;
        private Int64 _nAccountPatientID = 0;

        private bool _viewSearchOptionCheckBox = true;
        private bool _HasSecondaryInsOnClaim = false;
        public bool _bIsClaimChange = false;

        private bool _isTransactionOpen = false;
        private string _recordMachineId = "";
        private Int64 _recordUserId = 0;

         #endregion

        #region " Properties "
     

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            {
                Image returnImage = Image.FromStream(ms);
                try
                {
                    ms.Close();
                    ms.Dispose();
                    ms = null;
                }
                catch
                {
                }
                return returnImage;
            }
            

        }

        public string PatientCode
        {
            get { return _PatientCode; }
            set
            {
                _PatientCode = value;
            }
        }

        public string PatientName
        {
            get { return _PatientName; }
            set
            {
                _PatientName = value;
            }
        }
        public string ClaimFieldText
        {
            get { return Convert.ToString(txtPatientSearch.Text); }
            set { txtPatientSearch.Text = value; }
        }
        public string ClaimDisplayNo
        {
            get
            {
                string _claim = string.Empty;
                if (_ClaimNumber > 0)
                {
                    _claim = PatientStripControl.GetFormattedClaimPaymentNumber(Convert.ToString(_ClaimNumber));
                    if (!String.IsNullOrEmpty(_SubClaimNumber))
                    {
                        _claim = string.Concat(_claim, "-", _SubClaimNumber);
                    }
                }
                return _claim;
            }
        }

        public bool AllowEditingParty
        {
            get { return _AllowEditingParty; }
            set { _AllowEditingParty = value; }
        }

        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        public Int64 PAccountID
        {
            get { return _nPAccountID; }
            set { _nPAccountID = value; }
        }

        public Int64 GuarantorID
        {
            get { return _nGuarantorID; }
            set { _nGuarantorID = value; }
        }

        public Int64 AccountPatientID
        {
            get { return _nAccountPatientID; }
            set { _nAccountPatientID = value; }
        }

        public Int64 ClaimNumber
        {
            get { return _ClaimNumber; }
            set { _ClaimNumber = value; }
        }

        public string SubClaimNumber
        {
            get { return _SubClaimNumber; }
            set { _SubClaimNumber = value; }
        }

        Int32 _selectedInsuranceParty = 0;
        public Int32 SelectedInsuranceParty
        {
            get { return _selectedInsuranceParty; }
            set { _selectedInsuranceParty = value; }
        }

        Int32 _LoadselectedInsuranceParty = 0;
        public Int32 LoadselectedInsuranceParty
        {
            get { return _LoadselectedInsuranceParty; }
            set { _LoadselectedInsuranceParty = value; }
        }


        public bool ViewSearchOptionCheckBox
        {
            set
            {
                _viewSearchOptionCheckBox = value;
                chk_ClaimNoSearch.Visible = _viewSearchOptionCheckBox;
                chk_ClaimNoSearch.Checked = true;
            }
        }

        public string PatientStripHeaderText
        {
            get { return lbltxtPatientSearchCaption.Text; }
            set { lbltxtPatientSearchCaption.Text = value; }
        }

        public bool HasSecondaryInsOnClaim
        {
            get { return _HasSecondaryInsOnClaim; }
            set { _HasSecondaryInsOnClaim = value; }
        }

        public bool IsSplitClaimSearchActive
        {
            get { return pnlClaimSearch.Visible; }
            set { pnlClaimSearch.Visible = value; }
        }

        public bool IsRevisedPayment
        {
            set { }
        }
        #endregion

        #region " Delegates "

        public delegate void ClaimNumberEntered(string ClaimText);
        public event ClaimNumberEntered OnClaimNumberEntered;

        //public delegate void InsuranceSelected(Int64 InsuranceID, Int32 InsuraceSelfMode, Int64 ContactID, string InsurancePlanName);

        public delegate void InsuranceSelected(InsuranceSelectedArgs e);
        public event InsuranceSelected OnInsuranceSelected;

        public delegate void PatientModified();
        public event PatientModified OnPatientModified;

        public delegate void ClearPatientDetails();
        public event ClearPatientDetails OnClearPatientDetails;

        public delegate void ViewBenefit(Int64 PatientID,Int64 InsuranceID,string DatabaseConnectionString);
        public event ViewBenefit OnLoadViewBenefit;

        #endregion

        #region " Constructors "

        public ucPatientStripControl()
        {
            InitializeComponent();
            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                { _DataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]); }
            }
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
        }

        #endregion

        #region  " Events "

        private void PatientStripControl_Load(object sender, EventArgs e)
        {
            //TO avoid DB call at design time.
            if (_DataBaseConnectionString != "")
            {
                gloAccount objgloAccount = new gloAccount(_DataBaseConnectionString);
                if (objgloAccount != null)
                    _IsPatientAccountFeature = objgloAccount.GetPatientAccountFeatureSetting();
                pnlAccountDetails.Visible = _IsPatientAccountFeature == true ? true : false;
                objgloAccount.Dispose();  
            }
            SetupControls();
            FillControls(_PatientID);
            if (gloPatient.frmSetupPatient.hshPatData == null)
            {
                gloPatient.frmSetupPatient.hshPatData = new Hashtable(); 
            }
            Object obj = gloPatient.frmSetupPatient.hshPatData[_PatientID];
            if (obj == null)
            {
                gloPatient.frmSetupPatient.hshPatData.Add(_PatientID, gloGlobal.gloPMGlobal.UserID);
                AddrecentPatient(_PatientID, gloGlobal.gloPMGlobal.UserID, ref gloPatient.frmSetupPatient.hshPatData);
            }

        }

        private void PatientStripControl_Paint(object sender, PaintEventArgs e)
        {
            //if (pnl_Main.Width > 0)
            //{
            //    //pnlLeft.Width = pnl_Main.Width / 2;
            //   // pnlRight.Width = pnl_Main.Width - pnlLeft.Width;
            //}
        }

        #endregion

        #region " Patient strip control events "

        private void btn_ModityPatient_Click(object sender, EventArgs e)
        {
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(AppSettings.ConnectionStringPM);
            try
            {
                if (oSecurity.isPatientLock(_PatientID, true) == false && _PatientID > 0)
                {
                    gloPatient.frmSetupPatient ofrmSetupPatient = new gloPatient.frmSetupPatient(_PatientID, AppSettings.ConnectionStringPM);
                    ofrmSetupPatient.ShowDialog(this);

                    //6031
                    //Modified setting name 'GenerateHL7Message' to 'SendPatientDetails' by Abhijeet on 20110926
                    //Also used 'HL7' Setting flag

                    if (ofrmSetupPatient.ReturnIsClose == false)
                    {                                              
                        if (gloGlobal.gloPMGlobal.MessageBoxCaption == "gloEMR")
                        {
                            if (appSettings["HL7SENDOUTBOUNDGLOEMR"] != null)
                            {
                                if (appSettings["HL7SENDOUTBOUNDGLOEMR"] != "")
                                {
                                    if ((Convert.ToBoolean(Convert.ToInt16(appSettings["HL7SENDOUTBOUNDGLOEMR"])) == true))
                                    {
                                        if (appSettings["SendPatientDetails"] != null)
                                        {
                                            if (appSettings["SendPatientDetails"] != "")
                                            {
                                                if ((Convert.ToBoolean(Convert.ToInt16(appSettings["SendPatientDetails"])) == true))
                                                {
                                                    PatientStripControl.gblnAddModPatient = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (gloGlobal.gloPMGlobal.MessageBoxCaption == "gloPM")
                        {
                            if (appSettings["HL7SENDOUTBOUNDGLOPM"] != null)
                            {
                                if (appSettings["HL7SENDOUTBOUNDGLOPM"] != "")
                                {
                                    if ((Convert.ToBoolean(Convert.ToInt16(appSettings["HL7SENDOUTBOUNDGLOPM"])) == true))
                                    {
                                        if (appSettings["SendPatientDetails"] != null)
                                        {
                                            if (appSettings["SendPatientDetails"] != "")
                                            {
                                                if ((Convert.ToBoolean(Convert.ToInt16(appSettings["SendPatientDetails"])) == true))
                                                {
                                                    PatientStripControl.gblnAddModPatient = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (PatientStripControl.gblnAddModPatient == true)
                        {
                            PatientStripControl.InsertInMessageQueue("A08", _PatientID, _PatientID, _DataBaseConnectionString);
                        }
                        
                    }
                    //if (appSettings["GenerateHL7Message"] != null && appSettings["SendPatientDetails"] != null)
                    //{
                    //    if (appSettings["GenerateHL7Message"] != "" && appSettings["SendPatientDetails"] != "")
                    //    {
                    //        if (ofrmSetupPatient.ReturnIsClose == false)
                    //        {
                    //            if ((Convert.ToBoolean(appSettings["GenerateHL7Message"]) == true) && (Convert.ToBoolean(appSettings["SendPatientDetails"]) == true))
                    //            {
                    //                PatientStripControl.InsertInMessageQueue("A08", _PatientID, _PatientID, _DataBaseConnectionString);
                    //            }
                    //        }
                    //    }
                    //}
                    //End of code to Modified setting name 'GenerateHL7Message' to 'SendPatientDetails' by Abhijeet on 20110926

                    ofrmSetupPatient.Dispose();

                    FillControls(PatientID);

                    //FillDetails();

                    if (OnPatientModified != null)
                    { OnPatientModified(); }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSecurity != null)
                    oSecurity.Dispose();
            }
        }

        private void btn_ModityPatient_MouseHover(object sender, EventArgs e)
        {
            btn_ModityPatient.BackgroundImage = global::gloStrips.Properties.Resources.PatientHover;
            btn_ModityPatient.BackgroundImageLayout = ImageLayout.Center;
            if (toolTip1 == null)
            {
                toolTip1 = new ToolTip();
            }
            toolTip1.SetToolTip(btn_ModityPatient, "Modify Patient");
        }

        private void btn_ModityPatient_MouseLeave(object sender, EventArgs e)
        {
            btn_ModityPatient.BackgroundImage = global::gloStrips.Properties.Resources.Patient;
            btn_ModityPatient.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnSearchPatientClaim_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmPatientClaims ofrmPatientClaims = new frmPatientClaims())
                {
                    ofrmPatientClaims.PatientId = _PatientID;
                    ofrmPatientClaims.SelectedClaim = ClaimDisplayNo; //_SubClaimNumber; //_ClaimSubClaimNo;

                    if (ofrmPatientClaims.ShowDialog(this) == DialogResult.Yes)
                    {
                        //_PatientID = ofrmPatientClaims.PatientId; 

                        // Commented code for Remedy Salesforce Problem No : 00000051 which was creating
                        // Patient Id mismatch in Payment Transaction against claim due to searching different patient's claim. This code is not needed 
                        // as _PatientID is populated based on Claim number selected from claim search dialog in Insurance Payment.
                                                
                        txtPatientSearch.Text = ofrmPatientClaims.SelectedClaim;
                        if (ofrmPatientClaims.ClaimNo > 0)
                        {
                            if (IsValidClaimNumber())
                            {
                                _isTransactionOpen = gloStripControl.PatientStripControl.IsRecordOpen(ofrmPatientClaims.ClaimNo.ToString(), out _recordMachineId, out _recordUserId);
                                if (!_isTransactionOpen)
                                {
                                    SetClaimNumbers(false);
                                    gloStripControl.PatientStripControl.Lockclaims(ofrmPatientClaims.ClaimNo.ToString());

                                }
                                else
                                {
                                    DialogResult _dlgRst = DialogResult.None;
                                    _dlgRst = MessageBox.Show("Transaction is already opened for modify on machine " + _recordMachineId + ".", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            
                            
                        }
                        else
                        { txtPatientSearch.Text = ""; }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnSearchPatientClaim_MouseLeave(object sender, EventArgs e)
        {
            btnSearchPatientClaim.BackgroundImage = null;
            btnSearchPatientClaim.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnSearchPatientClaim_MouseHover(object sender, EventArgs e)
        {
            btnSearchPatientClaim.BackgroundImage = global::gloStrips.Properties.Resources.Img_Yellow;
            btnSearchPatientClaim.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void txtPatientSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPatientSearch.Text))
            {
                if (txtPatientSearch.Text.Contains("-"))
                {
                    if (txtPatientSearch.Text.IndexOf("-") == 0 || txtPatientSearch.Text.IndexOf("-") == txtPatientSearch.Text.Length)
                    {
                        txtPatientSearch.Text = "";
                        txtPatientSearch.Focus();
                        return;
                    }
                }

            }

            if ((int)e.KeyChar == 22 && Clipboard.ContainsData(DataFormats.Text))
            {
                if (!string.IsNullOrEmpty(Clipboard.GetText()))
                {
                    Match m = ValidateClaimNo(Clipboard.GetText());
                    if (!m.Success)
                    {
                        txtPatientSearch.Text = "";
                        txtPatientSearch.Focus();
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        txtPatientSearch.Text = Clipboard.GetText();
                    }

                }
            }

                 if (chk_ClaimNoSearch.Checked == true)
                 {
                     if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
                     {
                         if (e.KeyChar == Convert.ToChar(45) && txtPatientSearch.Text.Contains("-") == true)
                         {
                             e.Handled = true;
                         }
                         else if (e.KeyChar == Convert.ToChar(45) && txtPatientSearch.Text.Contains("-") == false)
                         {
                             e.Handled = false;
                         }
                         else
                         {
                             e.Handled = true;
                         }
                     }
                 }
                 if (e.KeyChar == 46)
                 {
                     //e.Handled =false;
                 }
                 if (e.KeyChar == 13)
                 {
                     if (IsValidClaimNumber())
                     {
                         _isTransactionOpen = gloStripControl.PatientStripControl.IsRecordOpen(txtPatientSearch.Text.ToString(), out _recordMachineId, out _recordUserId);
                         if (!_isTransactionOpen)
                         {
                             SetClaimNumbers(true);
                             gloStripControl.PatientStripControl.Lockclaims(txtPatientSearch.Text.ToString());

                         }
                         else
                         {
                             DialogResult _dlgRst = DialogResult.None;
                             _dlgRst = MessageBox.Show("Transaction is already opened for modify on machine " + _recordMachineId + ".", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                     }
                 }

        }

        private bool IsValidClaimNumber()
        {
            if (!String.IsNullOrEmpty(txtPatientSearch.Text))
            {
                string[] _claim = txtPatientSearch.Text.Split('-');
            
                if (_claim.Length.Equals(2))
                {
                    if (Convert.ToString(_claim[1]).Trim() == "")
                    {
                        MessageBox.Show("Claim number is invalid, Please enter a valid claim number.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.SelectSearchBox();
                        return false;
                    }
                }
            }
            return true;
        }

        private void btn_Alerts_Click(object sender, EventArgs e)
        {
            if (_PatientID > 0)
            {
                try
                {
                    frmPatientAlerts ofrmPatientAlerts = new frmPatientAlerts(_DataBaseConnectionString, _PatientID);
                    ofrmPatientAlerts.ShowDialog(this);
                    ofrmPatientAlerts.Dispose();
                    FillAlerts_Notes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No Patient is selected.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ucPatientStripControl_Leave(object sender, EventArgs e)
        {
            IsSplitClaimSearchActive = false;
        }

        private void oFrmEditPatientAccount_SaveButton_Click(object sender, EventArgs e)
        {
            FillControls(_PatientID);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            gloPatient.gloPatient objPatient = new gloPatient.gloPatient(_DataBaseConnectionString);
            Patient oPatient = new Patient();

            try
            {
                if (_nPAccountID > 0)
                {
                    
                    frmEditPatientAccount oFrmEditPatientAccount = new frmEditPatientAccount(_DataBaseConnectionString, _PatientID, _nGuarantorID, _nPAccountID);

                    if (objPatient != null && _PatientID > 0)
                        oPatient = objPatient.GetPatient(_PatientID);

                    if (oPatient != null)
                    {
                        oFrmEditPatientAccount.PatientGuarantors = oPatient.PatientGuarantors;
                        oFrmEditPatientAccount.PatientGuardianDetails = oPatient.GuardianDetail;
                        oFrmEditPatientAccount.PatientDemographicDetails = oPatient.DemographicsDetail;
                        oFrmEditPatientAccount._ownAccount = true;
                        oFrmEditPatientAccount.SaveButton_Click += new frmEditPatientAccount.SaveButtonClick(oFrmEditPatientAccount_SaveButton_Click);
                        oFrmEditPatientAccount.ShowDialog(this);
                        oFrmEditPatientAccount.SaveButton_Click -= new frmEditPatientAccount.SaveButtonClick(oFrmEditPatientAccount_SaveButton_Click);

                    }
                    oFrmEditPatientAccount.Dispose(); 
                }
                else
                {
                    MessageBox.Show("No Account is selected.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (objPatient != null)
                    objPatient.Dispose();
                if (oPatient != null)
                    oPatient.Dispose();
            }
        }

        #endregion

        #region " Insurance Grid Events "

        private void c1Insurance_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1Insurance_AfterEdit(object sender, RowColEventArgs e)
        {
            CheckEnum _Selected = CheckEnum.None;
            InsuranceSelectedArgs args = new InsuranceSelectedArgs();
            Int32 _CurRowIndex = e.Row;
            
            if (c1Insurance.Rows.Count > 0)
            {
                _Selected = c1Insurance.GetCellCheck(_CurRowIndex, COL_SELECT);

                if (_Selected == CheckEnum.Checked)
                {
                    SelectedInsuranceParty = Convert.ToInt32(c1Insurance.GetData(e.Row, COL_PARTY));
                    for (Int32 _Count = 1; _Count <= c1Insurance.Rows.Count - 1; _Count++)
                    {
                        if (_Count != _CurRowIndex)
                        {
                            if (c1Insurance.GetCellCheck(_Count, COL_SELECT) == CheckEnum.Checked)
                            {
                                c1Insurance.SetCellCheck(_Count, COL_SELECT, CheckEnum.Unchecked);
                            }
                        }
                    }
                    args.InsuranceID = Convert.ToInt64(c1Insurance.GetData(_CurRowIndex, COL_INSURANCEID));
                    args.InsuraceSelfMode = Convert.ToInt32(c1Insurance.GetData(_CurRowIndex, COL_INSSELFMODE));
                    args.SelectedInsurancePlan = Convert.ToString(c1Insurance.GetData(_CurRowIndex, COL_INSURANCENAME));
                    args.ContactID = Convert.ToInt64(c1Insurance.GetData(_CurRowIndex, COL_CONTACTID));
                    args.IsSelectedPlanOnHold = PatientStripControl.IsInsurancePlanOnHold(args.ContactID);
                }
                else if (_Selected == CheckEnum.Unchecked)
                { 
                    SelectedInsuranceParty = 0; 
                }

                if (OnInsuranceSelected != null)
                { 
                    OnInsuranceSelected(args); 
                }
            }
        }

        #endregion

        #region " Split Claim Search Grid Events "

        private void c1SplitClaims_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectClaim();
        }

        private void c1SplitClaims_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SelectClaim();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                IsSplitClaimSearchActive = false;
                SelectSearchBox();
            }
        }

        private void c1SplitClaims_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void SelectClaim()
        {
            IsSplitClaimSearchActive = false;
            txtPatientSearch.Text = Convert.ToString(c1SplitClaims.GetData(c1SplitClaims.RowSel, "ClaimDisplay"));
            SetClaimNumbers(false);
        }

        #endregion

        #region " Private Methods "

        //Added by Mahesh Satlapalli(Apollo) : form controls clear.
        public void ClearDetails()
        {
            try
            {
                _PatientID = 0;
                _PatientCode = "";
                _PatientName = "";
                _ProviderName = "";
                if (_bIsClaimChange == false)
                {
                    _ClaimNumber = 0;
                    _SubClaimNumber = "";
                }
                _nPAccountID = 0;
                _nGuarantorID = 0;
                _nAccountPatientID = 0;
                pnlAcctNote.Visible = false;

                FillControls(PatientID);
                DesignInsuranceGrid();
                IsSplitClaimSearchActive = false;
                
                if (OnClearPatientDetails != null)
                { OnClearPatientDetails(); }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void SetupControls()
        {
            pnlInsurace.Visible = true;
            btnSearchPatientClaim.Visible = true;

            btn_ModityPatient.BackgroundImage = global::gloStrips.Properties.Resources.Patient;
            btn_ModityPatient.BackgroundImageLayout = ImageLayout.Center;

            txtPatientSearch.Visible = true;
            chk_ClaimNoSearch.Visible = true;
            chk_ClaimNoSearch.Checked = false;
            chk_ClaimNoSearch.Enabled = true;
            lblSearchonClaimNo.Visible = false;
            c1PatientDetails.Visible = false;
            DesignInsuranceGrid();
           
            StripHeight = StripHeight + 62; 

            this.Height = StripHeight;
            gloC1FlexStyle.Style(c1SplitClaims, false);
        }
        
        public void FillDetails(Int64 TransactionMasterID,Int64 TransactionID)
        {
            try
            {
                    Int64 _patientID = PatientStripControl.GetPatientID(ClaimNumber, SubClaimNumber);
                    FillControls(_patientID);
                    FillInsuranceParties(TransactionMasterID, TransactionID);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillAlerts_Notes()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            DataTable dt = new DataTable();
            DataTable dtNotes = new DataTable();
            string _strSQL = "";
            lblAlerts.Text = "";
            lblNotes.Text = "";
            this.toolTip1.SetToolTip(lblAlerts, "");
            
            try
            {
                oDB.Connect(false);
                _strSQL = " SELECT nAlertID, sAlertName, nAlertType, bAlertStatus, sAlertColor, nPatientID, nClinicID " +
                          " FROM PatientAlerts " +
                          " WHERE (nPatientID = " + PatientID + ") AND (nClinicID = " + _ClinicID + ") " +
                          " AND bAlertStatus = 1 " +
                          " ORDER BY dtCreatedDate desc";
                oDB.Retrive_Query(_strSQL, out dt);
                oDB.Disconnect();

                if (dt != null && dt.Rows.Count > 0)
                {
                    if ((dt.Rows[0]["sAlertName"] == DBNull.Value) && (dt.Rows[0]["sAlertName"].ToString().Length > 35))
                        lblAlerts.Text = dt.Rows[0]["sAlertName"].ToString().Substring(0, 28) + " . . . ";
                    else
                        lblAlerts.Text = dt.Rows[0]["sAlertName"].ToString();

                    this.toolTip1.SetToolTip(lblAlerts, dt.Rows[0]["sAlertName"].ToString());

                    if (dt.Rows.Count > 1)
                        lblAlertsCap.Text = "PM Alerts (" + dt.Rows.Count + ") :";
                    else
                        lblAlertsCap.Text = "PM Alerts :";
                }
                else
                {
                    this.toolTip1.SetToolTip(lblAlerts, "");
                    lblAlertsCap.Text = "PM Alerts :";
                }
                oDB.Connect(false);
                _strSQL = "SELECT sInsuranceNotes AS Note FROM Patient WHERE (nPatientID = " + _PatientID + ") AND (nClinicID = " + _ClinicID + ")";

                oDB.Retrive_Query(_strSQL, out dtNotes);
                oDB.Disconnect();

                if (dtNotes != null && dtNotes.Rows.Count > 0)
                {
                    lblNotes.Text = dtNotes.Rows[0]["Note"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null){oDB.Dispose();}
                if (dt != null) dt.Dispose();
                if (dtNotes != null) dtNotes.Dispose();
            }
        }
        private void AddrecentPatient(Int64 PatID, Int64 UserID, ref Hashtable hashtbl)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oPara = null;
            string _ProcName;
            Int64 deletedpatid = -1;
            try
            {

                // 'Retrieve the Image for Patient
                // _strSQL = "select iphoto,sGender from patient where npatientid=" & PatientID
                _ProcName = "gsp_InsertRecentPatientUserwise";
                oDB.Connect(false);

                oPara = new gloDatabaseLayer.DBParameters();
                oPara.Add("@UserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oPara.Add("@PatientID", PatID, ParameterDirection.Input, SqlDbType.BigInt);



                deletedpatid = Convert.ToInt64(oDB.ExecuteScalar(_ProcName, oPara));

                if ((deletedpatid != -1))
                {
                    if ((hashtbl.Contains(deletedpatid)))
                        hashtbl.Remove(deletedpatid);
                }
            }

            catch
            {
            }
            finally
            {
                if (oDB != null)
                {

                    oDB.Dispose();
                    oDB = null/* TODO Change to default(_) if this is not a reference type */;
                }
                if (oPara != null)
                {
                    oPara.Clear();
                }
            }
        }
        private void FillStatementNote()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            DataTable dt = new DataTable();
            DataTable dtNotes = new DataTable();
            string _sqlQuery = "";
            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT TOP(1)nFromDate, nToDate,sStatementNote FROM Patient_Statement_Notes WITH (NOLOCK) WHERE nPatientID = " + _PatientID + ""
                            + " AND dbo.EDI837_DateAsNumber(CONVERT(VARCHAR(10),dbo.gloGetDate(),101)) <= nToDate "
                            + " AND dbo.EDI837_DateAsNumber(CONVERT(VARCHAR(10),dbo.gloGetDate(),101)) >= nFromDate "
                            + "ORDER BY nToDate DESC ";
                oDB.Retrive_Query(_sqlQuery, out dt);
                oDB.Disconnect();

                if (dt != null && dt.Rows.Count > 0) 
                {
                    if (dt.Rows.Count > 1)
                    {
                        lblDemoNotes.Text = lblDemoNotes.Text = "Patient Notes  (" + dt.Rows.Count + ") :";
                    }
                    else
                    {
                        lblDemoNotes.Text = dt.Rows[0]["sStatementNote"] == DBNull.Value ? string.Empty : dt.Rows[0]["sStatementNote"].ToString();
                    }
                }
                else
                {
                    this.toolTip1.SetToolTip(lblDemoNotes, "");
                    lblDemoNotes.Text = "Patient Notes";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { CloseConnection(); }
                if (dt != null) dt.Dispose();
                if (dtNotes != null) dtNotes.Dispose();
            }
        }


        private void FillInsuranceParties(Int64 TransactionMasterID, Int64 TransactionID)
        {
            DataTable _dtInsuranceParties = null;
            try
            {
                DesignInsuranceGrid();
                c1Insurance.Rows.Count = 1;

                _dtInsuranceParties = PatientStripControl.GetInsuranceParties(TransactionMasterID, TransactionID);
                if (_dtInsuranceParties != null && _dtInsuranceParties.Rows.Count > 0)
                {
                    LoadInsuranceParties(_dtInsuranceParties);
                    LoadInsuranceStatus(TransactionMasterID, TransactionID);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_dtInsuranceParties != null) { _dtInsuranceParties.Dispose(); }
            }
        }

        private void LoadInsuranceParties(DataTable dtInsuranceParties)
        {
            Int32 _ClaimInsCounter = 0;
            if (dtInsuranceParties != null && dtInsuranceParties.Rows.Count > 0)
            {
                Int32 _responsiblityType = 0;
                Int32 _selfPartyNo = dtInsuranceParties.Rows.Count;
               
                CellStyle csNonSelectCell;// = c1Insurance.Styles.Add("cs_NonSelectCell");
                try
                {
                    if (c1Insurance.Styles.Contains("cs_NonSelectCell"))
                    {
                        csNonSelectCell = c1Insurance.Styles["cs_NonSelectCell"];
                    }
                    else
                    {
                        csNonSelectCell = c1Insurance.Styles.Add("cs_NonSelectCell");

                    }

                }
                catch
                {
                    csNonSelectCell = c1Insurance.Styles.Add("cs_NonSelectCell");

                }
                foreach (DataRow row in dtInsuranceParties.Rows)
                {
                    _responsiblityType = Convert.ToInt32(row["nResponsibilityType"]);

                    // ResponsibilityType
                    // 1 : Patient  
                    // 2 : Insurance 
                    if (_responsiblityType.Equals(2))
                    {
                        _ClaimInsCounter = _ClaimInsCounter + 1;

                        c1Insurance.Rows.Add();
                        Int32 rowIndex = c1Insurance.Rows.Count - 1;
                        c1Insurance.SetData(rowIndex, COL_SELECT, false);//Select-CheckBox
                        c1Insurance.SetData(rowIndex, COL_INSURANCENAME, Convert.ToString(row["InsuranceName"])); //
                        c1Insurance.SetData(rowIndex, COL_INSURANCEID, Convert.ToString(row["nInsuranceID"])); //
                        c1Insurance.SetData(rowIndex, COL_INSURANCETYPE, Convert.ToString(row["sInsuranceFlag"]));

                        c1Insurance.SetData(rowIndex, COL_INSURANCECOPAYAMT, Convert.ToDecimal(row["CoPay"]));
                        if (Convert.ToBoolean(row["bWorkersComp"]) == true)
                        { c1Insurance.SetData(rowIndex, COL_INSURANCEWORKERCOMP, true); }
                        if (Convert.ToBoolean(row["bAutoClaim"]) == true)
                        { c1Insurance.SetData(rowIndex, COL_INSURANCEAUTOCLAIM, "Auto Claim"); }
                        c1Insurance.SetData(rowIndex, COL_CONTACTID, Convert.ToString(row["nContactID"])); //
                        c1Insurance.SetData(rowIndex, COL_COPAY, Convert.ToDecimal(Convert.ToString(row["CoPay"])));
                        c1Insurance.SetData(rowIndex, COL_PARTY, Convert.ToInt64(row["nResponsibilityNo"]));
                        c1Insurance.SetData(rowIndex, COL_INSSELFMODE, Convert.ToInt32(row["nResponsibilityType"])); // None = 0,Self = 1,Insurance = 2
                        c1Insurance.SetData(rowIndex, COL_SUBSCRIBERID, Convert.ToString(row["sSubscriberID"])); //Patient Insurance ID
                        c1Insurance.SetData(rowIndex,COL_GROUP,Convert.ToString(row["sGroup"]));

                        if (_ClaimInsCounter == 2) { _HasSecondaryInsOnClaim = true; }
                    }
                    else
                    {
                        c1Insurance.Rows.Add();
                        csNonSelectCell.DataType = typeof(System.String);
                        c1Insurance.SetCellStyle(c1Insurance.Rows.Count - 1, COL_SELECT, csNonSelectCell);
                        c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_SELECT, null);//Select-CheckBox

                        if (Convert.ToInt32(row["nResponsibilityType"]) == 3)
                        {
                            string collectionAgency = "";
                            collectionAgency = PatientStripControl.GetCollectionAgencyname(Convert.ToInt64(row["nContactID"]));
                            c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCENAME, collectionAgency); //
                        }
                        else
                        {
                            c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCENAME, "Self"); //
                        }
                        c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCEID, 0); //
                        c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSSELFMODE, "1"); // None = 0,Self = 1,Insurance = 2
                        c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_PARTY, Convert.ToInt64(row["nResponsibilityNo"]));
                        c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_COPAY, "");
                        c1Insurance.Rows[c1Insurance.Rows.Count - 1].AllowEditing = false;

                        c1Insurance.Cols[COL_INSURANCETYPE].AllowEditing = false;
                        c1Insurance.Cols[COL_INSURANCENAME].AllowEditing = false;
                        c1Insurance.Cols[COL_INSURANCEID].AllowEditing = false;
                        c1Insurance.Cols[COL_INSSELFMODE].AllowEditing = false;
                        c1Insurance.Cols[COL_INSURANCECOPAYAMT].AllowEditing = false;
                    }
                }

                // Add the Row for SELF 
                
               

                SelectInsuranceParty();
            }
        }

        public void LoadInsuranceStatus(Int64 TransactionMasterID, Int64 TransactionID)
        {
            DataTable dtPartyStatus = null;
            DataRow[] resultRows = null;
            int nCurrentResponsibleParty = 0;
            for (int icount = 1; icount <= c1Insurance.Rows.Count - 1; icount++)
            {
                if (c1Insurance.GetCellCheck(icount, COL_SELECT) == CheckEnum.Checked)
                {
                    nCurrentResponsibleParty = icount;
                    break;
                }
            }
            if (nCurrentResponsibleParty > 0)
            {
                dtPartyStatus = GetAllPatientPartyStatus(TransactionMasterID, TransactionID);
                if ((dtPartyStatus != null) && (dtPartyStatus.Rows.Count > 0))
                {
                    bool okInt = false;
                    long contactId = 0;
                    try
                    {
                        object myObj = c1Insurance.GetData(Convert.ToInt16(nCurrentResponsibleParty), COL_CONTACTID);
                        

                        try
                        {
                            contactId = Convert.ToInt64(myObj);
                            okInt = true;
                        }
                        catch
                        {

                        }
                    }
                    catch
                    {
                    }
                    if (okInt)
                    {
                        resultRows = dtPartyStatus.Select("nContactID = " + contactId.ToString());
                        if (resultRows.Length > 0)
                        {
                            foreach (DataRow dr in resultRows)
                            {
                                if (c1Insurance.GetData(Convert.ToInt16(nCurrentResponsibleParty), COL_INSURANCENAME).ToString().ToLower() != "self")
                                {
                                    c1Insurance.SetData(Convert.ToInt16(nCurrentResponsibleParty), COL_INSURANCE_STATUS, Convert.ToString(dr["PartyStatus"]));
                                }
                            }
                        }
                    }
                }
            }
        }

        public DataTable GetAllPatientPartyStatus(Int64 nMstTransactionID, Int64 nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            string _sQuery = String.Empty;
            DataTable dtPartyStatus = new DataTable();
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                oDB.Connect(false);
                //_sQuery = " SELECT nTransactionID, nPatientID, nClaimNo, nInsuranceID, nContactID, nResponsibilityNo, nResponsibilityType,"
                //        + " dbo.GET_ClaimPartyStatus_Revised(" + nTransactionID + ",nTransactionID,nInsuranceID,nContactID)  as PartyStatus "
                //        + " FROM dbo.BL_Transaction_InsPlan WITH (NOLOCK)"
                //        + " WHERE nTransactionID = " + nMstTransactionID;

                _sQuery = "SELECT nTransactionID, nPatientID, nClaimNo, nInsuranceID, nContactID, nResponsibilityNo, nResponsibilityType, "
                        + "dbo.GET_ClaimPartyStatus_Revised(nTransactionID," + nMstTransactionID + ",nInsuranceID,nContactID)  as PartyStatus "
                        + "FROM dbo.BL_Claim_Insurance bci WHERE bci.nTransactionMasterID=" + nMstTransactionID + ""
                        + "AND bci.nTransactionID=" + nTransactionID + "";

                oDB.Retrive_Query(_sQuery, out dtPartyStatus);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); }
                if (dtPartyStatus != null) { dtPartyStatus.Dispose(); }

            }

            return dtPartyStatus;
        }

        private void SelectInsuranceParty()
        {
            // Set Selected Insurance Party
            if (c1Insurance != null && c1Insurance.Rows.Count > 1)
            {
                for (Int32 rIndex = 1; rIndex < c1Insurance.Rows.Count; rIndex++)
                {
                    if (_selectedInsuranceParty != 0)
                    {
                        Int32 _party = Convert.ToInt32(c1Insurance.GetData(rIndex, COL_PARTY));
                
                        if (_selectedInsuranceParty.Equals(_party))
                        {
                            InsuranceSelectedArgs args = new InsuranceSelectedArgs();
                            args.InsuranceID = Convert.ToInt64(c1Insurance.GetData(rIndex, COL_INSURANCEID));
                            args.InsuraceSelfMode = Convert.ToInt32(c1Insurance.GetData(rIndex, COL_INSSELFMODE));
                            args.SelectedInsurancePlan = Convert.ToString(c1Insurance.GetData(rIndex, COL_INSURANCENAME));
                            args.ContactID = Convert.ToInt64(c1Insurance.GetData(rIndex, COL_CONTACTID));
                            args.IsSelectedPlanOnHold = PatientStripControl.IsInsurancePlanOnHold(args.ContactID);

                            if (args.InsuraceSelfMode != 1)
                            { c1Insurance.SetData(rIndex, COL_SELECT, true); }

                            // Trigger the Insurance Selected event
                            OnInsuranceSelected(args);
                            break;
                        }
                    }
                }
            }
        }

        private void SetClaimNumbers(bool AllowSearch)
        {
            ClaimNumber = 0;
            SubClaimNumber = string.Empty;
            IsSplitClaimSearchActive = false;

            if (!String.IsNullOrEmpty(txtPatientSearch.Text))
            {
                string[] _claim = txtPatientSearch.Text.Split('-');
                
                if (_claim.Length.Equals(2))
                {
                    _ClaimNumber = Convert.ToInt64(_claim[0]);
                    _SubClaimNumber = Convert.ToString(_claim[1]);
                    // IF SubClaim Number exist, skip the claim search 
                    AllowSearch = false;
                }
                else if (_claim.Length.Equals(1))
                { _ClaimNumber = Convert.ToInt64(_claim[0]); }

                if (AllowSearch)
                { SearchClaims(); }
            }
            if (!IsSplitClaimSearchActive)
            { OnClaimNumberEntered(txtPatientSearch.Text); }
        }

        private void SearchClaims()
        {
            DataTable _dtSplittedClaims = null;
            try
            {
                _dtSplittedClaims = PatientStripControl.GetSplittedClaims(_ClaimNumber);

                if (_dtSplittedClaims != null)
                {
                    if (_dtSplittedClaims.Rows.Count > 1)
                    {
                        IsSplitClaimSearchActive = true;
                        c1SplitClaims.DataSource = _dtSplittedClaims;
                        SetupClaimSearchGrid();
                        pnlClaimSearch.BringToFront();
                    }
                    else
                    { IsSplitClaimSearchActive = false; }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //if (_dtSplittedClaims != null)
                //    _dtSplittedClaims.Dispose();
            }
        }

        public string GetNextParty()
        {
            string _nextParty = "";

            if (c1Insurance != null && c1Insurance.Rows.Count > 1)
            {
                for (Int32 rIndex = 1; rIndex < c1Insurance.Rows.Count; rIndex++)
                {
                    if (c1Insurance.GetCellCheck(rIndex, COL_SELECT) == CheckEnum.Checked)
                    {
                        if (Convert.ToInt32(c1Insurance.GetData(rIndex, COL_INSSELFMODE)) == 1)
                        {
                            _nextParty = "0" + "-" + "Self" + "|";
                            break;
                        }
                        else if (rIndex + 1 < c1Insurance.Rows.Count)
                        {
                            //_nextParty = Convert.ToString(c1Insurance.GetData(rIndex + 1, COL_PARTY));
                            if (Convert.ToString(c1Insurance.GetData(rIndex + 1, COL_PARTY)).Trim().Split('-')[0] == "0")
                            { _nextParty = "0" + "-" + "Self" + "|"; }
                            else
                            { _nextParty = Convert.ToString(c1Insurance.GetData(rIndex + 1, COL_PARTY)).Trim() + "-" + Convert.ToString(c1Insurance.GetData(rIndex + 1, COL_INSURANCENAME)).Trim() + "|"; }
                            break;
                        }
                    }
                }
            }
            _nextParty = _nextParty.TrimEnd('|');
            return _nextParty;
        }

        public string GetCurrentParty()
        {
            string _nextParty = "";

            if (c1Insurance != null && c1Insurance.Rows.Count > 1)
            {
                for (Int32 rIndex = 1; rIndex < c1Insurance.Rows.Count; rIndex++)
                {
                    if (c1Insurance.GetCellCheck(rIndex, COL_SELECT) == CheckEnum.Checked)
                    {
                        if (Convert.ToInt32(c1Insurance.GetData(rIndex, COL_INSSELFMODE)) == 1)
                        {
                            _nextParty = "0" + "-" + "Self" + "|";
                            break;
                        }
                        else if (rIndex < c1Insurance.Rows.Count)
                        {
                            //_nextParty = Convert.ToString(c1Insurance.GetData(rIndex + 1, COL_PARTY));
                            if (Convert.ToString(c1Insurance.GetData(rIndex, COL_PARTY)).Trim().Split('-')[0] == "0")
                            { _nextParty = "0" + "-" + "Self" + "|"; }
                            else
                            { _nextParty = Convert.ToString(c1Insurance.GetData(rIndex, COL_PARTY)).Trim() + "-" + Convert.ToString(c1Insurance.GetData(rIndex, COL_INSURANCENAME)).Trim() + "|"; }
                            break;
                        }
                    }
                }
            }
            _nextParty = _nextParty.TrimEnd('|');
            return _nextParty;
        }

        public Int32 GetSelfPartyNo()
        {
            Int32 _selfCode = 0;

            if (c1Insurance != null && c1Insurance.Rows.Count > 1)
            {
                for (Int32 rIndex = 1; rIndex < c1Insurance.Rows.Count; rIndex++)
                {
                    if (Convert.ToInt32(c1Insurance.GetData(rIndex, COL_INSSELFMODE)) == 1)
                    {
                        _selfCode = rIndex;
                        break;
                    }
                }
            }
            return _selfCode;
        }

        public Int32 GetSelectedPartyResponsibility()
        {
            Int32 _Party = 0;

            if (c1Insurance != null && c1Insurance.Rows.Count > 1)
            {
                for (Int32 rIndex = 1; rIndex < c1Insurance.Rows.Count; rIndex++)
                {
                    if (c1Insurance.GetCellCheck(rIndex, COL_SELECT) == CheckEnum.Checked)
                    {
                        _Party = rIndex;
                        break;
                    }
                }
            }
            return _Party;
        }

        public void SelectSearchBox()
        {
            this.Select();
            this.Focus();
            txtPatientSearch.Select(); 
            txtPatientSearch.Focus();
            txtPatientSearch.SelectAll();
        }

        private void CloseConnection()
        {
            if (oDB != null)
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
            if (oDBPara != null)
            {
                oDBPara.Dispose();
                oDBPara = null;
            }
        }
        
        private string SplitToolTip(string strOrig)
        {
            try
            {
                string[] strArray;
                string CR = "\r\n";
                string strBuilder = "";
                string strReturn = "";
                strArray = strOrig.Split(' ');
            
                foreach (string strOneWord in strArray)
                {
                    strBuilder = (strBuilder + (strOneWord + ' '));
                    if (strBuilder.Length > 70)
                    {
                        strReturn = (strReturn + (strBuilder + CR));
                        strBuilder = "";
                    }
                }
                if (strBuilder.Length < 8)
                {
                    strReturn = strReturn.Substring(0, (strReturn.Length - 2));
                }
                return (strReturn + strBuilder);
            }
            catch //(Exception ex)
            {
                return strOrig;
            }
        }
        
        public void DisplayBalances()
        {


            decimal _totalBalAmt = 0;
            decimal _totalInsPending = 0;
            decimal _totalPatientDue = 0;
            decimal _totalcopayReserve = 0;
            decimal _totalAdvancedReserve = 0;
            decimal _totalOtherReserve = 0;
            decimal _lastPatPayment = 0;
            decimal dTotalBadDebtDue = 0;
            string sLastPatPaymentDate = "";

            lblBalTotalBalance.Text = "$0.00";
            lblBalInsurancePending.Text = "$0.00";
            lblBalPatientDue.Text = "$0.00";
            lblBalBadDebt.Text = "$0.00";
            lblBalCopayReserve.Text = "$0.00";
            lblBalAdvancedReserve.Text = "$0.00";
            lblBalOtherReserve.Text = "$0.00";

            DataSet dtSet = new DataSet();
            DataTable dtInsuranceDetails = new DataTable();
            DataTable dtReserveDetails = new DataTable();

            dtSet = PatientStripControl.GetPatientBalances(_PatientID,PAccountID);

            if (dtSet.Tables.Count > 0)
            {
                dtInsuranceDetails = dtSet.Tables[0];
                dtReserveDetails = dtSet.Tables[1];
            }

            if (dtInsuranceDetails != null && dtInsuranceDetails.Rows.Count > 0)
            {
                //_totalBalAmt = dtInsuranceDetails.Rows[0]["TotalBalance"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["TotalBalance"]);
                _totalInsPending = dtInsuranceDetails.Rows[0]["InsuranceDue"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["InsuranceDue"]);
                _totalPatientDue = dtInsuranceDetails.Rows[0]["PatientDue"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["PatientDue"]);
                dTotalBadDebtDue = dtInsuranceDetails.Rows[0]["BadDebtDue"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["BadDebtDue"]);
                _lastPatPayment = dtInsuranceDetails.Rows[0]["PatientLastPay"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["PatientLastPay"]);
                sLastPatPaymentDate = dtInsuranceDetails.Rows[0]["LastPayDate"] == DBNull.Value ? "" : Convert.ToDateTime(dtInsuranceDetails.Rows[0]["LastPayDate"]).ToString("MM/dd/yyyy");
                if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                {
                    _totalBalAmt = _totalInsPending + _totalPatientDue + dTotalBadDebtDue;
                    pnlBadDebt.Visible = true;

                    gloSecurity.gloSecurity gloSecurity = new gloSecurity.gloSecurity(_DataBaseConnectionString);
                    if (gloSecurity.isBadDebtPatient(this.PatientID))
                    { lblBadDebtSatusII.Visible = true; }
                    else
                    { lblBadDebtSatusII.Visible = false; }

                    if (gloSecurity != null)
                    {
                        gloSecurity.Dispose();
                        gloSecurity = null;
                    }

                    if (dTotalBadDebtDue > 0)
                    {                        
                        lblBadDebtSatus.Visible = true ;
                    }
                    else
                    {                     
                        lblBadDebtSatus.Visible = false ;
                    }
                }
                else
                {
                    _totalBalAmt = _totalInsPending + _totalPatientDue;
                    pnlBadDebt.Visible = false;
                }
            }
            else
            {
                _totalBalAmt = 0;
                _totalInsPending = 0;
                _totalPatientDue =0;
                _lastPatPayment = 0;
                sLastPatPaymentDate = "";
            }

            //Assign Copay Reserve,AdvancedResere,OtherReserve to Varialbles
            if (dtReserveDetails != null && dtReserveDetails.Rows.Count > 0)
            {
                foreach (DataRow drReserveDetails in dtReserveDetails.Rows)
                {
                    if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 2)   //For Copay Reserve
                    {
                        _totalcopayReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                    }
                    if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 3)  //ForAdvanced Reserve
                    {
                        _totalAdvancedReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                    }
                    if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 4) //For OtherReserve
                    {
                        _totalOtherReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                    }
                }
            }

            _totalBalAmt = _totalBalAmt - (_totalcopayReserve + _totalAdvancedReserve + _totalOtherReserve);
            _totalPatientDue = _totalPatientDue - (_totalcopayReserve + _totalAdvancedReserve + _totalOtherReserve);
            
            lblBalTotalBalance.Text = "$ " + _totalBalAmt.ToString("#0.00");
            lblBalInsurancePending.Text = "$ " + _totalInsPending.ToString("#0.00");
            lblBalPatientDue.Text = "$ " + _totalPatientDue.ToString("#0.00");
            lblBalBadDebt.Text = "$ " + dTotalBadDebtDue.ToString("#0.00");
            lblBalCopayReserve.Text = "$ " + _totalcopayReserve.ToString("#0.00");
            lblBalAdvancedReserve.Text = "$ " + _totalAdvancedReserve.ToString("#0.00");
            lblBalOtherReserve.Text = "$ " + _totalOtherReserve.ToString("#0.00");
            if (sLastPatPaymentDate != "")
            {
                lblDemoLastPatPayment.Text = "$ " + _lastPatPayment.ToString("#0.00") + " [" + sLastPatPaymentDate + "]";
            }
            else { lblDemoLastPatPayment.Text = "$ " + _lastPatPayment.ToString("#0.00"); }
        }
        
        private DataSet GetPatientAccountBalances(Int64 PatientID)
        {
            DataSet dtSet = new DataSet();
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("PA_GetPatientAccountBalances_V2", oDBPara, out dtSet);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
                this.ResumeLayout(false);
            }
            return dtSet;
        }
        
        private DataSet GetAccountBalances(Int64 PAccountID)
        {
            DataSet dtSet = new DataSet();
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@nPAccountID", PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("PA_GetAccountBalances_V2", oDBPara, out dtSet);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
                this.ResumeLayout(false);
            }
            return dtSet;
        }
        
        private bool OpenConnection(bool withParameters)
        {
            bool _Result = false;
            try
            {
                if (_DataBaseConnectionString != "")
                {
                    oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                    oDB.Connect(false);
                    if (withParameters)
                        oDBPara = new gloDatabaseLayer.DBParameters();
                    _Result = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _Result;
        }

        Dictionary<string, string> sortedDict = null;
        public Dictionary<string, string> DishtblMedcatClr;
        private global::System.Globalization.CultureInfo resourceCulture;
        [System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal global::System.Globalization.CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        private void FillMedicalCategoryHashTable()
        {
            if ((DishtblMedcatClr == null))
            {
                DishtblMedcatClr = new Dictionary<string, string>();
                DishtblMedcatClr.Add("MedicalCategoryImages_1_Brown_TopBrown", "MedicalCategoryImages_1_Brown_BottomBrown");
                DishtblMedcatClr.Add("MedicalCategoryImages_2_Blue_TopSkyBlue", "MedicalCategoryImages_2_Blue_BottomSkyBlue");
                DishtblMedcatClr.Add("MedicalCategoryImages_3_Gray_TopGray", "MedicalCategoryImages_3_Gray_BottomGray");
                DishtblMedcatClr.Add("MedicalCategoryImages_4_GreenTopLightGreen", "MedicalCategoryImages_4_Green_BottomLightGreen");
                DishtblMedcatClr.Add("MedicalCategoryImages_5_TopOrange", "MedicalCategoryImages_5_BottomOrange");

                DishtblMedcatClr.Add("MedicalCategoryImages_6_Pink_TopPink", "MedicalCategoryImages_6_Pink_BottomPink");
                DishtblMedcatClr.Add("MedicalCategoryImages_7_Red_TopRed", "MedicalCategoryImages_7_Red_BottomRed");
                DishtblMedcatClr.Add("MedicalCategoryImages_8_Violet_TopViolet", "MedicalCategoryImages_8_Violet_BottomViolet");
                DishtblMedcatClr.Add("MedicalCategoryImages_9_Yellow_TopYellow", "MedicalCategoryImages_9_Yellow_BottomYellow");
                DishtblMedcatClr.Add("MedicalCategoryImages_91_TopDark_Blue", "MedicalCategoryImages_91_BottomDark_Blue");
                //   DishtblMedcatClr.  
                sortedDict = (from entry in DishtblMedcatClr orderby entry.Key ascending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
            }
        }

        private void SetLabelColorForDarkBlue(bool blndrkblue)
        {
            if (blndrkblue == true)
            {

                lblSearchonClaimNo.ForeColor = Color.White;
                lbltxtPatientSearchCaption.ForeColor = Color.White;
                //lblPnlHeaderTop.ForeColor = Color.White;
                //lblPnlHeaderMiddle.ForeColor = Color.White;
                label56.ForeColor = Color.White;
                label55.ForeColor = Color.White;
                label54.ForeColor = Color.White;
                label47.ForeColor = Color.White;
                //lblpnlInsuraceTop.ForeColor = Color.White;
                //lblpnlInsuraceBottom.ForeColor = Color.White;
                //lblpnlInsuraceRight.ForeColor = Color.White;
                //lblpnlInsuraceLeft.ForeColor = Color.White;
                lblBalTotalLine.ForeColor = Color.White;
                lblBalOtherReserveCaption.ForeColor = Color.White;
                lblBalPatientDueCaption.ForeColor = Color.White;
                lblBalAdvancedReserveCaption.ForeColor = Color.White;
                lblBalCopayReserveCaption.ForeColor = Color.White;
                lblBalInsurancePendingCaption.ForeColor = Color.White;
                lblBalTotalBalanceCaption.ForeColor = Color.White;

                lblDemoGuarantor.ForeColor = Color.White;
                lblDemoPatientPayment.ForeColor = Color.White;
                lblDemoGuarantorCaption.ForeColor = Color.White;
                lblDemoLastPatPayment.ForeColor = Color.White;

                lblDemoAccountNoCaption.ForeColor = Color.White;
                lblDemoAccountNo.ForeColor = Color.White;

                lblAccountDesc.ForeColor = Color.White;
                label5.ForeColor = Color.White;
                lblDemoAccountDesc.ForeColor = Color.White;

                lblNotes.ForeColor = Color.White;
                lblAlertsCap.ForeColor = Color.White;

                lblNotesCaption.ForeColor = Color.White;
                lblDemoCopay.ForeColor = Color.White;
                lblDemoProvider.ForeColor = Color.White;
                lblDemoNotes.ForeColor = Color.White;
                lblAlerts.ForeColor = Color.White;
                lblDemoCopayCaption.ForeColor = Color.White;
                lblDemoProviderCaption.ForeColor = Color.White;

                lblDemoPatientCaption.ForeColor = Color.White;
                lblDemoPatient.ForeColor = Color.White;
                if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                {
                    pnlBadDebt.Visible = true;
                }
                else
                {
                    pnlBadDebt.Visible = false;
                }

                //lblucPatientStripRight.ForeColor = Color.White;
                //lblucPatientStripLeft.ForeColor = Color.White;
                //lblucPatientStripBottom.ForeColor = Color.White;

                lblBalTotalBalance.ForeColor = Color.White;
                lblBalOtherReserve.ForeColor = Color.White;
                lblBalAdvancedReserve.ForeColor = Color.White;
                lblBalCopayReserve.ForeColor = Color.White;
                lblBalBadDebt.ForeColor = Color.White;
                lblBalPatientDue.ForeColor = Color.White;
                lblBalInsurancePending.ForeColor = Color.White;
                lblAccountNotes.ForeColor = Color.White;
                lblAccountNotesCaption.ForeColor = Color.White;

                lblPatientCode.ForeColor = Color.White;
                lblPatCodeCaption.ForeColor = Color.White;
                lblGender.ForeColor = Color.White;
                lblGenderCaption.ForeColor = Color.White;
                lblDOB.ForeColor = Color.White;
                lblDobCaption.ForeColor = Color.White;
                lblPatientName.ForeColor = Color.White;
                lblDemoMedCat.ForeColor = Color.White;
                lblDemoMedCatCaption.ForeColor = Color.White;
               
            }
            else
            {
                //lblPatientName.ForeColor = Color.Black;
                lblSearchonClaimNo.ForeColor = Color.Black;
                lbltxtPatientSearchCaption.ForeColor = Color.Black;
                //lblPnlHeaderTop.ForeColor = Color.Black;
                //lblPnlHeaderMiddle.ForeColor = Color.Black;
                label56.ForeColor = Color.Black;
                label55.ForeColor = Color.Black;
                label54.ForeColor = Color.Black;
                label47.ForeColor = Color.Black;
                //lblpnlInsuraceTop.ForeColor = Color.Black;
                //lblpnlInsuraceBottom.ForeColor = Color.Black;
                //lblpnlInsuraceRight.ForeColor = Color.Black;
                //lblpnlInsuraceLeft.ForeColor = Color.Black;
                lblBalTotalLine.ForeColor = Color.Black;
                lblBalOtherReserveCaption.ForeColor = Color.Black;
                lblBalPatientDueCaption.ForeColor = Color.Black;
                lblBalAdvancedReserveCaption.ForeColor = Color.Black;
                lblBalCopayReserveCaption.ForeColor = Color.Black;
                lblBalInsurancePendingCaption.ForeColor = Color.Black;
                lblBalTotalBalanceCaption.ForeColor = Color.Black;

                lblDemoGuarantor.ForeColor = Color.Black;
                lblDemoPatientPayment.ForeColor = Color.Black;
                lblDemoGuarantorCaption.ForeColor = Color.Black;
                lblDemoLastPatPayment.ForeColor = Color.Black;

                lblDemoAccountNoCaption.ForeColor = Color.Black;
                lblDemoAccountNo.ForeColor = Color.Black;

                lblAccountDesc.ForeColor = Color.Black;
                label5.ForeColor = Color.Black;
                lblDemoAccountDesc.ForeColor = Color.Black;

                lblNotes.ForeColor = Color.Black;
                lblAlertsCap.ForeColor = Color.Black;

                lblNotesCaption.ForeColor = Color.Black;
                lblDemoCopay.ForeColor = Color.Black;
                lblDemoProvider.ForeColor = Color.Black;
                lblDemoNotes.ForeColor = Color.Black;
                lblAlerts.ForeColor = Color.Black;
                lblDemoCopayCaption.ForeColor = Color.Black;
                lblDemoProviderCaption.ForeColor = Color.Black;

                lblDemoPatientCaption.ForeColor = Color.Black;
                lblDemoPatient.ForeColor = Color.Black;

                //lblucPatientStripRight.ForeColor = Color.Black;
                //lblucPatientStripLeft.ForeColor = Color.Black;
                //lblucPatientStripBottom.ForeColor = Color.Black;

                lblBalTotalBalance.ForeColor = Color.Black;
                lblBalOtherReserve.ForeColor = Color.Black;
                lblBalAdvancedReserve.ForeColor = Color.Black;
                lblBalBadDebt.ForeColor = Color.Black;
                lblBalCopayReserve.ForeColor = Color.Black;
                lblBalPatientDue.ForeColor = Color.Black;
                lblBalInsurancePending.ForeColor = Color.Black;
                lblAccountNotes.ForeColor = Color.Black;
                lblAccountNotesCaption.ForeColor = Color.Black;

                lblPatientCode.ForeColor = Color.Black;
                lblPatCodeCaption.ForeColor = Color.Black;
                lblGender.ForeColor = Color.Black;
                lblGenderCaption.ForeColor = Color.Black;
                lblDOB.ForeColor = Color.Black;
                lblDobCaption.ForeColor = Color.Black;
                lblPatientName.ForeColor = Color.Black;
                lblDemoMedCat.ForeColor = Color.Black;
                lblDemoMedCatCaption.ForeColor = Color.Black;
                if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                {
                    pnlBadDebt.Visible = true;
                }
                else
                {
                    pnlBadDebt.Visible = false;
                }
            }
        }
        private static ResourceSet resourceSet = gloStrips.Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        private void GetMedicalCategoryImage(DataTable dtMedCat = null)
        {

            DataSet DS = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oPara = null;
            DataTable dtMedColor = null;
            string strcolor = "";
            string strborderColor = string.Empty;
            string strbottompanelcolr = string.Empty;
            try
            {
                var _with1 = oDB;
                oDB.Connect(false);
                oPara = new gloDatabaseLayer.DBParameters();
                oPara.Add("@tvpMedicalCategory", dtMedCat, ParameterDirection.Input, SqlDbType.Structured);
                oPara.Add("@PatientId", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_GetPatientMedicalCategoryColor", oPara, out dtMedColor);
                oDB.Disconnect();
                if ((dtMedColor != null))
                {
                    if ((dtMedColor.Rows.Count > 0))
                    {
                        strcolor = Convert.ToString(dtMedColor.Rows[0]["ImageColor"]);
                        strborderColor = Convert.ToString(dtMedColor.Rows[0]["BorderColor"]);
                        strbottompanelcolr = Convert.ToString(dtMedColor.Rows[0]["BottomPanelColor"]);
                    }
                }


                if ((!string.IsNullOrEmpty(strcolor.Trim())))
                {
                    foreach (KeyValuePair<string, string> di in sortedDict)
                    {
                        if ((di.Key.ToString().Contains(strcolor.Trim().Replace(" ", "_"))))
                        {
                           
                            pnlTop.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Key));
                            panel1.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Key));
                            pnlClaimSearch.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Key));
                            pnlMain.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Value));
                            pnlRight.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Value));
                            pnlLeft.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Value));
                            pnlBalance.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Value));
                            lblucPatientStripLeft.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblucPatientStripRight.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblPnlHeaderTop.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblPnlHeaderMiddle.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblucPatientStripBottom.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }
                else
                {
                    //ResourceSet resourceSet = gloStrips.Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
                    pnlTop.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_TopOrange"));
                    panel1.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_TopOrange"));
                    pnlClaimSearch.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_TopOrange"));
                    pnlMain.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_BottomOrange"));
                    pnlRight.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_BottomOrange"));
                    pnlLeft.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_BottomOrange"));
                    pnlBalance.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_BottomOrange"));
                    lblucPatientStripLeft.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblucPatientStripRight.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblPnlHeaderTop.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblPnlHeaderMiddle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblucPatientStripBottom.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                }

                if ((strcolor.Contains("Pink") || strcolor.Contains("Red") || strcolor.Contains("Violet") || strcolor.Contains("Dark")))
                {
                    lblPatientName.ForeColor = Color.White;
                    lblDobCaption.ForeColor = Color.White;
                    lblDOB.ForeColor = Color.White;
                    lblGenderCaption.ForeColor = Color.White;
                    lblGender.ForeColor = Color.White;
                    lblPatCodeCaption.ForeColor = Color.White;
                    lblPatientCode.ForeColor = Color.White;
                    lblSearchonClaimNo.ForeColor = Color.White;
                    chk_ClaimNoSearch.ForeColor = Color.White;
                    lbltxtPatientSearchCaption.ForeColor = Color.White;
                    btnSearchPatientClaim.ForeColor = Color.White;
                }
                else
                {
                    lblPatientName.ForeColor = Color.Black;
                    lblDobCaption.ForeColor = Color.Black;
                    lblDOB.ForeColor = Color.Black;
                    lblGenderCaption.ForeColor = Color.Black;
                    lblGender.ForeColor = Color.Black;
                    lblPatCodeCaption.ForeColor = Color.Black;
                    lblPatientCode.ForeColor = Color.Black;
                    lblSearchonClaimNo.ForeColor = Color.Black;
                    chk_ClaimNoSearch.ForeColor = Color.Black;
                    lbltxtPatientSearchCaption.ForeColor = Color.Black;
                    btnSearchPatientClaim.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
            finally
            {
                if ((oDB != null))
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if ((dtMedColor != null))
                {
                    dtMedColor.Dispose();
                    dtMedColor = null;
                }
                if ((dtMedCat != null))
                {
                    dtMedCat.Dispose();
                    dtMedCat = null;
                }
                if ((DS != null))
                {
                    DS.Tables.Clear();
                    DS.Dispose();
                    DS = null;
                }
            }


        }
        //fill controls.
        private void FillControls(Int64 PatientID)
        {
            this.SuspendLayout();

            decimal _expectedCopay = 0;
            decimal _lastPatPayment = 0;
            DateTime _DateOfBirth;
            string _BirthTime = "";
            string _Gender = "";

            DataSet dtSet = new DataSet();
            DataTable dtInitialdemographic = new DataTable();
            DataTable dtAlerts = new DataTable();
            DataTable dtPatientNotes = new DataTable();
            DataTable dtAccountNotes = new DataTable();
            DataTable dtStatementNotes = new DataTable();
            //DataTable dtPatientName = new DataTable();
            DataSet dsAccountFollowUp = null;
            bool IsFollowUpEnable = false;

            try
            {

                //Display Selected Patients Details
                if (PatientID > 0)
                {

                    dtSet = PatientStripControl.GetPatientDemographicInformation(PatientID);  //Collect all the information in Data Set
                    //dtPatientName = PatientStripControl.GetPatientName(PatientID);
                    if (dtSet.Tables.Count > 0)
                    {
                        dtInitialdemographic = dtSet.Tables[0];   //Table 0 for patient name ,DOB,patientCode,Provider,Gender
                        dtAlerts = dtSet.Tables[1];   // Table 1 nAlertID, sAlertName
                        dtPatientNotes = dtSet.Tables[2];  // Table 2 Note
                        dtStatementNotes = dtSet.Tables[3];   // Table 3 nFromDate, nToDate,sStatementNote 

                    }
                    
                    //Assign patient name ,DOB,patientCode,Provider,Gender to Variables
                    #region  "Patient Demographics"

                    if (dtInitialdemographic != null && dtInitialdemographic.Rows.Count > 0)
                    {
                        _PatientID = PatientID;
                        _PatientName = dtInitialdemographic.Rows[0]["PatientName"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["PatientName"].ToString();
                        _PatientCode = dtInitialdemographic.Rows[0]["PatientCode"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["PatientCode"].ToString();
                        _DateOfBirth = dtInitialdemographic.Rows[0]["DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtInitialdemographic.Rows[0]["DOB"]);
                        _Gender = dtInitialdemographic.Rows[0]["Gender"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["Gender"].ToString();
                        _PatientCode = dtInitialdemographic.Rows[0]["PatientCode"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["PatientCode"].ToString();
                        _ProviderName = dtInitialdemographic.Rows[0]["Provider"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["Provider"].ToString();
                        _expectedCopay = dtInitialdemographic.Rows[0]["ExpectedCopay"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInitialdemographic.Rows[0]["ExpectedCopay"].ToString());
                        _PatientMedicalCtegory = dtInitialdemographic.Rows[0]["MedicalCategory"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["MedicalCategory"].ToString();
                        _NextAppointment = dtInitialdemographic.Rows[0]["NextAppointment"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["NextAppointment"].ToString();
                        _EMRAlert = dtInitialdemographic.Rows[0]["EMRAlerts"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["EMRAlerts"].ToString();
                        _BirthTime = dtInitialdemographic.Rows[0]["sBirthTime"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["sBirthTime"].ToString();
                    }
                    else
                    {
                        _PatientID = 0;
                        _PatientName = "";
                        _PatientCode = "";
                        _DateOfBirth = DateTime.MinValue;
                        _Gender = "";
                        _PatientCode = "";
                        _ProviderName = "";
                        _expectedCopay = 0;
                        _PatientMedicalCtegory = "";
                        _NextAppointment = "";
                        _EMRAlert = "";
                        _BirthTime = "";
                    }

                    lblPatientName.Visible = true;
                    lblDobCaption.Visible = true;
                    lblDOB.Visible = true;
                    lblGenderCaption.Visible = true;
                    lblGender.Visible = true;
                    lblPatCodeCaption.Visible = true;
                    lblPatientCode.Visible = true;
                    btn_ModityPatient.Visible = true;
                    
                    lblPatientName.Text = _PatientName;
                 //   lblDOB.Text = Convert.ToString(_DateOfBirth.ToString("MM/dd/yyyy")) + "(" + CalculateAge(_DateOfBirth) + "y)";

                    AgeDetail oAgeDetails = new AgeDetail();
                    lblDOB.Text = Convert.ToString(_DateOfBirth.ToString("MM/dd/yyyy")) + "(" + Convert.ToString(oAgeDetails.CalculateAge_New(_DateOfBirth, _BirthTime)) + ")";
                    
                    lblGender.Text = _Gender;
                    lblPatientCode.Text = _PatientCode;


                    if ((!string.IsNullOrEmpty(_PatientMedicalCtegory.Trim())))
                    {
                        lblDemoMedCat.Visible = true;
                        lblDemoMedCatCaption.Visible = true;
                        lblDemoMedCat.Text = _PatientMedicalCtegory;
                        pnlMedCategory.Visible = true;
                        this.toolTip1.SetToolTip(lblDemoMedCat, _PatientMedicalCtegory);
                    }
                    else
                    {
                        lblDemoMedCat.Visible = false;
                        lblDemoMedCatCaption.Visible = false;
                        lblDemoMedCat.Text = "";
                        pnlMedCategory.Visible = false;
                    }

                    if (PatientStripControl.ShowEMRAlertsOnPatientBanner())
                    {
                        if (_EMRAlert != "")
                        {
                            lblDemoEMRAlerts.Visible = true;
                            lblDemogloEMRAlertsCaption.Visible = true;
                            lblDemoEMRAlerts.ForeColor = Color.FromArgb(212, 0, 114);
                        }
                        else
                        {
                            lblDemoEMRAlerts.Visible = false;
                            //lblDemogloEMRAlertsCaption.Visible = false;
                        }
                        //lblDemogloEMRAlertsCaption.ForeColor = Color.FromArgb(212, 0, 114);
                    }
                    else
                    {
                        lblDemoEMRAlerts.Visible = false;
                        lblDemogloEMRAlertsCaption.Visible = false;
                    }

                    lblDemoNextApptCaption.Text = _NextAppointment;
                    lblDemoEMRAlerts.Text = _EMRAlert;
                    this.toolTip1.SetToolTip(lblDemoNextApptCaption, _NextAppointment);
                    this.toolTip1.SetToolTip(lblDemoEMRAlerts, _EMRAlert);
                    
                    this.toolTip1.SetToolTip(lblPatientName, _PatientName);

                    string[] patientName = _PatientName.Split(',');
                    if (patientName.Length > 1)
                    {
                        lblDemoPatient.Text = _PatientCode + "-" + patientName[1].TrimStart() + " " + patientName[0];
                        this.toolTip1.SetToolTip(lblDemoPatient, _PatientCode + "-" + patientName[1].TrimStart() + "" + patientName[0]);
                    }
                    else
                    {
                        lblDemoPatient.Text = _PatientCode + "-" + _PatientName;
                        this.toolTip1.SetToolTip(lblDemoPatient, _PatientCode + "-" + _PatientName);
                    }

                    //if (dtPatientName != null && dtPatientName.Rows.Count > 0)
                    //{
                    //    _PatientName = dtPatientName.Rows[0]["PatientDisplayName"] == DBNull.Value ? string.Empty : dtPatientName.Rows[0]["PatientDisplayName"].ToString();
                    //}

                    lblDemoProvider.Text = _ProviderName;
                    this.toolTip1.SetToolTip(lblDemoProvider, _ProviderName);

                    lblDemoCopay.Text = "$ " + _expectedCopay.ToString("#0.00");
                    lblDemoLastPatPayment.Text = "$ " + _lastPatPayment.ToString("#0.00");
                    FillMedicalCategoryHashTable();
                    GetMedicalCategoryImage();
                    #endregion "Patient Demographics"

                    #region "Alerts"

                    if (dtAlerts != null && dtAlerts.Rows.Count > 0)
                    {
                        if ((dtAlerts.Rows[0]["sAlertName"] != DBNull.Value)
                            && (dtAlerts.Rows[0]["sAlertName"].ToString().Length > 35))
                        {
                            lblAlerts.Text = dtAlerts.Rows[0]["sAlertName"].ToString().Substring(0, 28) + " . . . ";
                        }
                        else
                        {
                            lblAlerts.Text = dtAlerts.Rows[0]["sAlertName"] == DBNull.Value ? string.Empty : dtAlerts.Rows[0]["sAlertName"].ToString();
                        }
                        this.toolTip1.SetToolTip(lblAlerts, dtAlerts.Rows[0]["sAlertName"].ToString());


                        if (dtAlerts.Rows.Count > 1)
                            lblAlertsCap.Text = "PM Alerts (" + dtAlerts.Rows.Count + ") :";
                        else
                            lblAlertsCap.Text = "PM Alerts :";
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(lblAlerts, "");
                        lblAlertsCap.Text = "PM Alerts :";
                    }


                    #endregion

                    #region "Patient Notes"

                    if (dtPatientNotes != null && dtPatientNotes.Rows.Count > 0)
                    {
                        //if ((dtPatientNotes.Rows[0]["Note"] != DBNull.Value)
                        //    && (dtPatientNotes.Rows[0]["Note"].ToString().Length > 35))
                        //{
                        //    lblNotes.Text = dtPatientNotes.Rows[0]["Note"].ToString().Substring(0, 28) + " . . . ";
                        //}
                        //else
                        //{
                        //    lblNotes.Text = dtPatientNotes.Rows[0]["Note"] == DBNull.Value ? string.Empty : dtPatientNotes.Rows[0]["Note"].ToString();
                        //}

                        if ((dtPatientNotes.Rows[0]["Note"] != DBNull.Value)
                            && (dtPatientNotes.Rows[0]["Note"].ToString().Length > 35))
                        {
                            lblNotes.Text = dtPatientNotes.Rows[0]["Note"].ToString();
                        }
                        else
                        {
                            lblNotes.Text = dtPatientNotes.Rows[0]["Note"] == DBNull.Value ? string.Empty : dtPatientNotes.Rows[0]["Note"].ToString();
                        }


                        this.toolTip1.SetToolTip(lblNotes, dtPatientNotes.Rows[0]["Note"].ToString());
                    }


                    #endregion

                    #region "Statement Note"

                    if (dtStatementNotes != null && dtStatementNotes.Rows.Count > 0)
                    {
                        if (dtStatementNotes.Rows.Count > 1)
                        {
                            lblDemoNotes.Text = lblDemoNotes.Text = "Patient Notes  (" + dtStatementNotes.Rows.Count + ") :";
                        }
                        else
                        {
                            lblDemoNotes.Text = dtStatementNotes.Rows[0]["sStatementNote"] == DBNull.Value ? string.Empty : dtStatementNotes.Rows[0]["sStatementNote"].ToString();
                        }
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(lblDemoNotes, "");
                        lblDemoNotes.Text = "Patient Notes";
                    }

                    #endregion

                    #region "Accounts "
                    _nPAccountID = PatientStripControl.GetPatientAccountID(ClaimNumber, SubClaimNumber);
                    
                    DataRow _drAccount = PatientStripControl.GetPatientAccountDetails(_nPAccountID);
                    if (_drAccount != null)
                    {
                        lblDemoAccountNo.Text = Convert.ToString(_drAccount["sAccount"]);
                        this.toolTip1.SetToolTip(lblDemoAccountNo, Convert.ToString(_drAccount["sAccount"]));
                        lblAccountDesc.Text = Convert.ToString(_drAccount["sAccountDesc"]);
                        this.toolTip1.SetToolTip(lblAccountDesc, Convert.ToString(_drAccount["sAccountDesc"]));
                        lblDemoGuarantor.Text = Convert.ToString(_drAccount["sGuarantorName"]);
                        this.toolTip1.SetToolTip(lblDemoGuarantor, Convert.ToString(_drAccount["sGuarantorName"]));
                        _nGuarantorID = Convert.ToInt64(_drAccount["nGuarantorID"]);
                    }
                    else
                    {
                        lblDemoAccountNo.Text = "";
                        this.toolTip1.SetToolTip(lblDemoAccountNo, "");
                        lblAccountDesc.Text = "";
                        this.toolTip1.SetToolTip(lblAccountDesc, "");
                        lblDemoGuarantor.Text = "";
                        this.toolTip1.SetToolTip(lblDemoGuarantor, "");
                        _nGuarantorID = 0;
                    } 
                    #endregion

                    IsFollowUpEnable = PatientStripControl.IsFollowUpEnable();

                    if (IsFollowUpEnable)
                    {
                        dsAccountFollowUp = PatientStripControl.GetAccountFollowUp(_nPAccountID);
                        if (dsAccountFollowUp.Tables.Count > 0)
                        {
                            dtAccountNotes = dsAccountFollowUp.Tables[0];
                        }
                    }

                    #region "Account Notes"

                    if (dtAccountNotes != null && dtAccountNotes.Rows.Count > 0)
                    {
                        if ((dtAccountNotes.Rows[0]["sNoteDescription"] != DBNull.Value)
                            && (dtAccountNotes.Rows[0]["sNoteDescription"].ToString() != ""))
                        {
                            pnlAcctNote.Visible = true;
                            lblAccountNotes.Visible = true;
                            lblAccountNotes.Text = dtAccountNotes.Rows[0]["sNoteDescription"].ToString();
                        }
                        else
                        {
                            pnlAcctNote.Visible = false;
                            lblAccountNotes.Text = dtAccountNotes.Rows[0]["sNoteDescription"] == DBNull.Value ? string.Empty : dtAccountNotes.Rows[0]["sNoteDescription"].ToString();
                        }
                        this.toolTip1.SetToolTip(lblAccountNotes, dtAccountNotes.Rows[0]["sNoteDescription"].ToString());
                    }
                    //Bug #67085: 00000553 : Account Note on patient Banner should change when we enter a new claim
                    else
                    {
                        pnlAcctNote.Visible = false;
                        lblAccountNotes.Visible = false;
                    }

                    #endregion "Account Notes"

                    DisplayBalances();

                }
                else
                {
                    lblDemoAccountNo.Text = "";
                    lblAccountDesc.Text = "";
                    lblDemoGuarantor.Text = "";
                    lblDemoPatient.Text = "";
                    lblDemoProvider.Text = "";
                    lblDemoPatient.Text = "";

                    lblPatientName.Text = "";
                    lblDOB.Text = "";
                    lblGender.Text = "";
                    lblPatientCode.Text = "";

                    lblDemoMedCat.Visible = false;
                    lblDemoMedCatCaption.Visible = false;
                    lblDemoMedCat.Text = "";
                    pnlMedCategory.Visible = false;
                    lblDemoEMRAlerts.Text = "";
                    lblDemoNextApptCaption.Text = "";
                    if (PatientStripControl.ShowEMRAlertsOnPatientBanner())
                    {
                        if (_EMRAlert != "")
                        {
                            lblDemoEMRAlerts.Visible = true;
                            lblDemogloEMRAlertsCaption.Visible = true;
                            lblDemoEMRAlerts.ForeColor = Color.FromArgb(212, 0, 114);
                        }
                        else
                        {
                            lblDemoEMRAlerts.Visible = false;
                        }
                    }
                    else
                    {
                        lblDemoEMRAlerts.Visible = false;
                        lblDemogloEMRAlertsCaption.Visible = false;
                    }

                    this.toolTip1.SetToolTip(lblDemoProvider, "");
                    this.toolTip1.SetToolTip(lblDemoPatient, "");
                    this.toolTip1.SetToolTip(lblDemoAccountNo, "");
                    this.toolTip1.SetToolTip(lblAccountDesc, "");
                    this.toolTip1.SetToolTip(lblDemoGuarantor, "");

                    this.toolTip1.SetToolTip(lblPatientName, "");

                    lblBalTotalBalance.Text = "$0.00";
                    lblBalInsurancePending.Text = "$0.00";
                    lblBalPatientDue.Text = "$0.00";
                    lblBalBadDebt.Text = "$0.00";
                    lblBalCopayReserve.Text = "$0.00";
                    lblBalAdvancedReserve.Text = "$0.00";
                    lblBalOtherReserve.Text = "$0.00";
                    lblDemoCopay.Text = "$ " + _expectedCopay.ToString("#0.00");
                    lblDemoLastPatPayment.Text = "$ " + _lastPatPayment.ToString("#0.00");

                    lblAlertsCap.Text = "PM Alerts :";
                    lblAlerts.Text = "";
                    lblNotes.Text = "";
                    lblDemoNotes.Text = "Patient Notes";
                    this.toolTip1.SetToolTip(lblAlerts, "");
                    this.toolTip1.SetToolTip(lblNotes, "");
                    this.toolTip1.SetToolTip(lblDemoNotes, "");

                    lblPatientName.Visible = false;
                    lblDobCaption.Visible = false;
                    lblDOB.Visible = false;
                    lblGenderCaption.Visible = false;
                    lblGender.Visible = false;
                    lblPatCodeCaption.Visible = false;
                    lblPatientCode.Visible = false;

                    btn_ModityPatient.Visible = false;

                    DesignInsuranceGrid();
                    if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                    {
                        pnlBadDebt.Visible = true;
                    }
                    else
                    {
                        pnlBadDebt.Visible = false;
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {
                dbEX.ERROR_Log(dbEX.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (dtInitialdemographic != null)
                    dtInitialdemographic.Dispose();
                if (dtPatientNotes != null)
                    dtPatientNotes.Dispose();
                if (dtStatementNotes != null)
                    dtStatementNotes.Dispose();
                if (dtAlerts != null)
                    dtAlerts.Dispose();
            }
            this.ResumeLayout(false);
        }
        
        private DataSet GetPatientDemographicInformation(Int64 PatientID)
        {
            DataSet dtSet = new DataSet();
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("PAT_GetDemographics", oDBPara, out dtSet);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
                this.ResumeLayout(false);
            }
            return dtSet;
        }
        
        public string FormatAge(DateTime BirthDate)
                {
                    DateTime _BDate = BirthDate;
                    // Compute the difference between BirthDate 'CODE FROM gloPM : year and end year.
                    bool IsBirthDateLeap = false;
                    Int32 years = DateTime.Now.Year - BirthDate.Year;
                    Int32 months = 0;
                    Int32 days = 0;
                    //Test if BirthDay for LeapYear.
                    if (BirthDate.Day == 29 & BirthDate.Month == 2)
                    {
                        IsBirthDateLeap = true;
                    }
                    // Check if the last year was a full year. 
                    if (DateTime.Now < BirthDate.AddYears(years) && years != 0)
                    {
                        years -= 1;
                    }
                    BirthDate = BirthDate.AddYears(years);
                    
                    // Now we know BirthDate <= end and the diff between them : is < 1 year. 
                    if (BirthDate.Year == DateTime.Now.Year)
                    {
                        months = DateTime.Now.Month - BirthDate.Month;
                    }
                    else
                    {
                        months = (12 - BirthDate.Month) + DateTime.Now.Month;
                    }
                    // Check if the last month was a full month. 
                    if (DateTime.Now < BirthDate.AddMonths(months) && months != 0)
                    {
                        months -= 1;
                    }
                    BirthDate = BirthDate.AddMonths(months);
                    // Now we know that BirthDate < end and is within 1 month  of each other.
                    days = (DateTime.Now - BirthDate).Days;

                    //To Adjust Age if BirthDate is 29th Feb in leap year
                    if (IsBirthDateLeap == true)
                    {
                        //'Sequence of following IF code is too important.. DON'T MODIFY
                        days -= 1;
                        if (DateTime.Now.Day == 29 & DateTime.Now.Month == 2)
                        {
                            days += 1;
                        }
                        else if (DateTime.Now.Year % 4 == 0)
                        {
                            days += 1;
                        }
                        if (days < 0 & DateTime.Now.Year % 4 != 0)
                        {
                            days = 30;
                            months = months - 1;
                            if (months < 0)
                            {
                                months = 11;
                                years = years - 1;
                            }
                        }
                        if (months == 12)
                        {
                            days = 30;
                            months = 11;
                        }
                    }

                    //Return years & " years " & months & " months " & days & " days"
                    //Following code to display age in Numeric and Text
                    //Dim age As New AgeDetail
                    //age.Age = years & " Years " & months & " Months " & days & " Days"
                    //' Cases

                    //'20081119   ''Following Code to Store ExactAge in String
                    string _AgeStr = "";
                    //if (gblShowAgeInDays == true & gblAgeLimit >= DateDiff(DateInterval.Day, (System.DateTime)_BDate, System.DateTime.Now.Date))
                    //{
                    if (years == 0)
                    {
                        if (months == 0)
                        {
                            if (days <= 1)
                            {
                                _AgeStr = days + " Day";
                            }
                            else
                            {
                                _AgeStr = days + " Days";
                            }
                        }
                        else if (months == 1)
                        {
                            if (days == 0)
                            {
                                _AgeStr = months + " Month";
                            }
                            else if (days == 1)
                            {
                                _AgeStr = months + " Month " + days + " Day";
                            }
                            else
                            {
                                _AgeStr = months + " Month " + days + " Days";
                            }
                        }
                        else if (months > 1)
                        {
                            if (days == 0)
                            {
                                _AgeStr = months + " Months";
                            }
                            else if (days == 1)
                            {
                                _AgeStr = months + " Months " + days + " Day";
                            }
                            else
                            {
                                _AgeStr = months + " Months " + days + " Days";
                            }
                        }
                    }
                    else if (years == 1)
                    {
                        if (months == 0)
                        {
                            if (days == 0)
                            {
                                _AgeStr = years + " Year ";
                            }
                            else if (days == 1)
                            {
                                _AgeStr = years + " Year " + days + " Day";
                            }
                            else
                            {
                                _AgeStr = years + " Year " + days + " Days";
                            }
                        }
                        else if (months == 1)
                        {
                            if (days == 0)
                            {
                                _AgeStr = years + " Year " + months + " Month ";
                            }
                            else if (days == 1)
                            {
                                _AgeStr = years + " Year " + months + " Month " + days + " Day";
                            }
                            else
                            {
                                _AgeStr = years + " Year " + months + " Month " + days + " Days";
                            }
                        }
                        else if (months > 1)
                        {
                            if (days == 0)
                            {
                                _AgeStr = years + " Year " + months + " Months ";
                            }
                            else if (days == 1)
                            {
                                _AgeStr = years + " Year " + months + " Months " + days + " Day";
                            }
                            else
                            {
                                _AgeStr = years + " Year " + months + " Months " + days + " Days";
                            }
                        }
                    }
                    else if (years > 1)
                    {
                        if (months == 0)
                        {
                            if (days == 0)
                            {
                                _AgeStr = years + " Years ";
                            }
                            else if (days == 1)
                            {
                                _AgeStr = years + " Years " + days + " Day";
                            }
                            else
                            {
                                _AgeStr = years + " Years " + days + " Days";
                            }
                        }
                        else if (months == 1)
                        {
                            if (days == 0)
                            {
                                _AgeStr = years + " Years " + months + " Month";
                            }
                            else if (days == 1)
                            {
                                _AgeStr = years + " Years " + months + " Month " + days + " Day";
                            }
                            else
                            {
                                _AgeStr = years + " Years " + months + " Month " + days + " Days";
                            }
                        }
                        else if (months > 1)
                        {
                            if (days == 0)
                            {
                                _AgeStr = years + " Years " + months + " Months";
                            }
                            else if (days == 1)
                            {
                                _AgeStr = years + " Years " + months + " Months " + days + " Day";
                            }
                            else
                            {
                                _AgeStr = years + " Years " + months + " Months " + days + " Days";
                            }
                        }
                    }

                    return _AgeStr;
                }


        private Int32 CalculateAge(DateTime birthDate)
        {
            DateTime now = DateTime.Today;
            Int32 years = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                --years;
            return years;
        }
                
        #region " C1 Grid Design Methods "

        private void SetupClaimSearchGrid()
        {
            #region " Hide Columns "

            c1SplitClaims.Cols["ClaimNo"].Visible = false;
            c1SplitClaims.Cols["SubClaimNo"].Visible = false;
            c1SplitClaims.Cols["ClaimNo"].Visible = false;
            c1SplitClaims.Cols["Facility"].Visible = false;
            c1SplitClaims.Cols["FacilityCode"].Visible = false;
            c1SplitClaims.Cols["PatientID"].Visible = false;
            c1SplitClaims.Cols["ProviderFName"].Visible = false;
            c1SplitClaims.Cols["ProviderMName"].Visible = false;
            c1SplitClaims.Cols["ProviderLName"].Visible = false;
            c1SplitClaims.Cols["TransactionMasterID"].Visible = false;
            c1SplitClaims.Cols["TransactionID"].Visible = false;
            c1SplitClaims.Cols["ContactID"].Visible = false;
            c1SplitClaims.Cols["ClinicID"].Visible = false;
            c1SplitClaims.Cols["nResponsibilityNo"].Visible = false;
            c1SplitClaims.Cols["nParentTransactionID"].Visible = false;

            #endregion

            #region " Set Captions "

            c1SplitClaims.Cols["ClaimDisplay"].Caption = "Claim #";
            c1SplitClaims.Cols["PatientCode"].Caption = "Code";
            c1SplitClaims.Cols["PatientFName"].Caption = "Patient FN";
            c1SplitClaims.Cols["PatientMName"].Caption = "Patient MI";
            c1SplitClaims.Cols["PatientLName"].Caption = "Patient LN";
            c1SplitClaims.Cols["ProviderFName"].Caption = "Provider FN";
            c1SplitClaims.Cols["ProviderMName"].Caption = "Provider MI";
            c1SplitClaims.Cols["ProviderLName"].Caption = "Provider LN";
            c1SplitClaims.Cols["InsuranceID"].Caption = "Insurance ID";
            c1SplitClaims.Cols["Insurance"].Caption = "Insurance Plan";

            #endregion

            c1SplitClaims.Focus();
            c1SplitClaims.Select(1, 0);
        }

        private void DesignInsuranceGrid()
        {
            c1Insurance.AllowSorting = AllowSortingEnum.None;
            c1Insurance.Cols.Count = COL_INSVIEW_COUNT;
            c1Insurance.Rows.Count = 1;
            c1Insurance.SetData(0, COL_SELECT, "Sel.");
            c1Insurance.SetData(0, COL_PARTY, "Party");
            c1Insurance.SetData(0, COL_INSURANCEID, "InsuranceID");
            c1Insurance.SetData(0, COL_INSURANCENAME, "Insurance");
            c1Insurance.SetData(0, COL_INSURANCETYPE, "Type");
            c1Insurance.SetData(0, COL_INSURANCE_STATUS  , "Status");
            c1Insurance.SetData(0, COL_INSSELFMODE, "Mode");
            c1Insurance.SetData(0, COL_INSURANCECOPAYAMT, "CopayAmt");
            c1Insurance.SetData(0, COL_INSURANCEWORKERCOMP, "Workers Comp");
            c1Insurance.SetData(0, COL_INSURANCEAUTOCLAIM, "Auto Claim");
            c1Insurance.SetData(0, COL_CONTACTID, "Contact ID");
            c1Insurance.SetData(0, COL_COPAY, "Copay");
            c1Insurance.SetData(0, COL_SUBSCRIBERID, "Insurance ID");
            c1Insurance.SetData(0, COL_GROUP, "Group");

            c1Insurance.Cols[COL_INSURANCEWORKERCOMP].DataType = typeof(System.Boolean);
            c1Insurance.Cols[COL_COPAY].DataType = typeof(System.Decimal);
            c1Insurance.Cols[COL_COPAY].Format = "c";
            c1Insurance.Cols[COL_COPAY].AllowEditing = false;

            c1Insurance.Cols[COL_PARTY].DataType = typeof(System.String);
            c1Insurance.Cols[COL_SUBSCRIBERID].DataType = typeof(System.String);

            Int32 nWidth;
            nWidth = pnlInsurace.Width;

            c1Insurance.Cols[COL_SELECT].DataType = System.Type.GetType("System.Boolean");//Select Column
            c1Insurance.Cols[COL_SELECT].AllowEditing = AllowEditingParty;  //true;
            c1Insurance.Cols[COL_INSURANCEWORKERCOMP].AllowEditing = false;
            c1Insurance.Cols[COL_PARTY].AllowEditing = false;
            c1Insurance.Cols[COL_SUBSCRIBERID].AllowEditing = false;
            c1Insurance.Cols[COL_INSURANCE_STATUS].AllowEditing = false;
            c1Insurance.Cols[COL_GROUP].AllowEditing = false;

            c1Insurance.Cols[COL_INSURANCEID].Visible = false;
            c1Insurance.Cols[COL_INSSELFMODE].Visible = false;
            c1Insurance.Cols[COL_INSURANCECOPAYAMT].Visible = false;
            c1Insurance.Cols[COL_INSURANCEAUTOCLAIM].Visible = false;
            c1Insurance.Cols[COL_INSURANCETYPE].Visible = true;
            c1Insurance.Cols[COL_INSURANCE_STATUS].Visible = true;
            c1Insurance.Cols[COL_INSURANCEWORKERCOMP].Visible = false;
            c1Insurance.Cols[COL_CONTACTID].Visible = false;
            c1Insurance.Cols[COL_GROUP].Visible = true;

            c1Insurance.Cols[COL_SELECT].Width = 35;
            c1Insurance.Cols[COL_PARTY].Width = 40;
            c1Insurance.Cols[COL_INSURANCENAME].Width = Convert.ToInt32(nWidth - 250);
            c1Insurance.Cols[COL_COPAY].Width =50;
            c1Insurance.Cols[COL_SUBSCRIBERID].Width = 90;
            c1Insurance.Cols[COL_INSURANCEID].Width = 0;
            c1Insurance.Cols[COL_INSURANCETYPE].Width = 50;
            c1Insurance.Cols[COL_INSURANCE_STATUS].Width = 50;
            c1Insurance.Cols[COL_INSSELFMODE].Width = 0;
            c1Insurance.Cols[COL_INSURANCEWORKERCOMP].Width = 0;
            c1Insurance.Cols[COL_INSURANCEAUTOCLAIM].Width = 0;
            c1Insurance.Cols[COL_GROUP].Width = 60;
            
            c1Insurance.Cols[COL_INSURANCENAME].TextAlign = TextAlignEnum.LeftCenter;
            c1Insurance.Cols[COL_SUBSCRIBERID].TextAlign = TextAlignEnum.LeftCenter;
            c1Insurance.Cols[COL_GROUP].TextAlign = TextAlignEnum.LeftCenter;
            
            gloC1FlexStyle.Style(c1Insurance, false);

            if (c1Insurance.Cols[COL_SUBSCRIBERID].StyleNew != null && c1Insurance.Cols[COL_SUBSCRIBERID].StyleNew.Trimming == StringTrimming.None)
            { c1Insurance.Cols[COL_SUBSCRIBERID].StyleNew.Trimming = StringTrimming.EllipsisCharacter; }

            if (c1Insurance.Cols[COL_INSURANCENAME].StyleNew != null && c1Insurance.Cols[COL_INSURANCENAME].StyleNew.Trimming == StringTrimming.None)
            { c1Insurance.Cols[COL_INSURANCENAME].StyleNew.Trimming = StringTrimming.EllipsisCharacter; }

        }

        #endregion

        private void txtPatientSearch_MouseUp(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPatientSearch.Text))
            {
                Match m = ValidateClaimNo(txtPatientSearch.Text);
                if (!m.Success)
                {
                    txtPatientSearch.Text = "";
                    txtPatientSearch.Focus();
                    return;
                }

            }
        }

        private Match ValidateClaimNo(string sText)
        {
            string pattern = @"^[-+]?\d*(\-\d+)?$";

            // Instantiate the regular expression object.
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);

            //Match the regular expression pattern against a text string.
            Match m = r.Match(sText);
            return m;
        }

        private void btnMouseHover(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloStrips.Properties.Resources.Img_Orange;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloStrips.Properties.Resources.Img_ButtonHover;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }
        }

        #endregion

        private void c1Insurance_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                CellStyle csSelect;

                for (int i = cmnu_ChangeInsResponsiblity.Items.Count - 1; i >= 0; i--)
                {
                    cmnu_ChangeInsResponsiblity.Items.RemoveAt(i);
                }
                Int64 nCurrentInsuranceID = 0;
                if (c1Insurance.HitTest(e.X, e.Y).Row > 0)
                {
                    nCurrentInsuranceID = Convert.ToInt64(c1Insurance.GetData(c1Insurance.HitTest(e.X, e.Y).Row, COL_INSURANCEID));
                    if (nCurrentInsuranceID > 0)
                        cmnu_ChangeInsResponsiblity.Items.Add(mnuViewBenefit);
                }

                c1Insurance.ContextMenuStrip = cmnu_ChangeInsResponsiblity;
                c1Insurance_BeforeEdit(null, null);

                // csSelect = c1Insurance.Styles.Add("cs_Orange");
                try
                {
                    if (c1Insurance.Styles.Contains("cs_Orange"))
                    {
                        csSelect = c1Insurance.Styles["cs_Orange"];
                    }
                    else
                    {
                        csSelect = c1Insurance.Styles.Add("cs_Orange");

                    }

                }
                catch
                {
                    csSelect = c1Insurance.Styles.Add("cs_Orange");

                }
                csSelect.BackColor = System.Drawing.Color.FromArgb(254, 207, 102);

                // csSelect = c1Insurance.Styles.Add("cs_white");
                try
                {
                    if (c1Insurance.Styles.Contains("cs_white"))
                    {
                        csSelect = c1Insurance.Styles["cs_white"];
                    }
                    else
                    {
                        csSelect = c1Insurance.Styles.Add("cs_white");

                    }

                }
                catch
                {
                    csSelect = c1Insurance.Styles.Add("cs_white");

                }
                csSelect.BackColor = Color.White;

                c1Insurance.Row = c1Insurance.HitTest(e.X, e.Y).Row;

                if (c1Insurance.HitTest(e.X, e.Y).Row > 0)
                {
                    if (c1Insurance.HitTest(e.X, e.Y).Column == COL_INSURANCENAME)
                    {
                        String sSelected = Convert.ToString(c1Insurance.GetData(c1Insurance.HitTest(e.X, e.Y).Row, COL_INSURANCENAME)).Replace("\0", "");
                        if (c1Insurance.GetCellCheck(c1Insurance.HitTest(e.X, e.Y).Row, COL_INSURANCENAME) == CheckEnum.Unchecked && sSelected != "" && sSelected != null)
                        {

                            if (c1Insurance.HitTest(e.X, e.Y).Row == -1)
                            {
                                c1Insurance.ContextMenuStrip = null;
                                return;
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }

        private void c1Insurance_BeforeEdit(object sender, RowColEventArgs e)
        {
            string PartyNo = "";
            Int64 _nResponsibleParty = 0;
            Int64 _nPrimaryParty = 0;

            try
            {
                for (int iCount = 1; iCount <= c1Insurance.Rows.Count - 1; iCount++)
                {
                    if (iCount == 1)
                        _nPrimaryParty = Convert.ToInt64(c1Insurance.GetData(1, COL_CONTACTID));

                    if (c1Insurance.GetCellCheck(iCount, COL_SELECT) == CheckEnum.Checked)
                    {
                        PartyNo = Convert.ToString(c1Insurance.GetData(iCount, COL_INSURANCE_STATUS));
                        _nResponsibleParty = Convert.ToInt64(c1Insurance.GetData(iCount, COL_CONTACTID));
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }

        private void mnuViewBenefit_Click(object sender, EventArgs e)
        {
            Int64 nCurrentInsuranceID = Convert.ToInt64(c1Insurance.GetData(c1Insurance.RowSel, COL_INSURANCEID));
            OnLoadViewBenefit(PatientID, nCurrentInsuranceID, _DataBaseConnectionString);
        }
       
    }
}
