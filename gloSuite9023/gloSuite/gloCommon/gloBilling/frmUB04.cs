using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloCMSEDI;
using System.Collections;
using gloSettings;
namespace gloBilling
{
    public partial class frmUB04 : Form
    {
        #region " Variables "

     //   private ArrayList _arrHCFAData = null;
        private string _databaseconnectionstring = "";

        private ArrayList _arrSelectedTransactions = null;
       // private Transaction oTransaction = null;
        private Int64 _TransactionId = 0;
        private Int64 _MstTransactionId = 0;
        private ArrayList _TransactionIds = new ArrayList();
        private ArrayList _MstTransactionIds = new ArrayList();
        public int _TransactionIdCounter = 0;
        private bool _LoadPatientFromTransaction = false;
     //   string _strPatientStatus = "";
     //   bool IsSecondaryInsurance = false;

        public frmBillingBatch_New objFrmBillingBatch_New;
        public String CallingTab;
        private string _messageboxcaption = " gloPM ";
        private Int64 _ContactID = 0;
        private Int64 _UserID = 0;
        private string _UserName = "";
        //gloPatientStripControl.gloPatientStripControl oPatientStripControl = null;
        gloPatientStripControl.gloClaimSearchControl oPatientStripControl = null;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        gloCMSEDI.UB04 oUB04;
        private bool _bIsFromModifyChr = false;
        public frmBillingModifyCharges frmModifyChr=null;
        private bool IsFormLoad = false;
        private Int64 _PatientID = 0;
        Int64 _ClinicID = 0;
        Boolean _bIsClaimHold = false;
        public Boolean bIsClaimHold
        {
            get { return _bIsClaimHold; }
            set { _bIsClaimHold = value; }
        }
        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        public ArrayList SelectedTransactions
        {
            get { return _arrSelectedTransactions; }
            set { _arrSelectedTransactions = value; }
        }

        public ArrayList TransactionIDs
        {
            get { return _TransactionIds; }
            set { _TransactionIds = value; }
        }
        public ArrayList MstTransactionIds
        {
            get { return _MstTransactionIds; }
            set { _MstTransactionIds = value; }
        }
        public Int64 TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }
        public Int64 MstTransactionId
        {
            get { return _MstTransactionId; }
            set { _MstTransactionId = value; }
        }
        public Int64 LastAccessedTransaction
        {
            get;
            set;
        }
        #endregion

        public frmUB04(string DatabaseConnection, ArrayList TransactionIds, ArrayList MstTransactionIds)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnection;
         
            SelectedTransactions = new ArrayList();
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            this.TransactionIDs = TransactionIds;
            this.MstTransactionIds = MstTransactionIds;

            if (TransactionIds.Count > 0)
            {
                #region " Update Record Status "

                gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
                Int64 _trnId = 0;

                for (int i = 0; i < TransactionIDs.Count; i++)
                {
                    _trnId = 0;                  
                    _trnId = Convert.ToInt64(TransactionIDs[i]);                 
                }
                if (ogloBilling != null) { ogloBilling.Dispose(); }

                #endregion

                _TransactionId = Convert.ToInt64(_TransactionIds[_TransactionIdCounter].ToString());
                _MstTransactionId = Convert.ToInt64(_MstTransactionIds[_TransactionIdCounter].ToString());
            }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion

        }

        public frmUB04(string DatabaseConnection, ArrayList TransactionIds, ArrayList MstTransactionIds, bool bIsFromModifyChr)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnection;

            SelectedTransactions = new ArrayList();
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            _bIsFromModifyChr = bIsFromModifyChr;

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            this.TransactionIDs = TransactionIds;
            this.MstTransactionIds = MstTransactionIds;

            if (TransactionIds.Count > 0)
            {
                #region " Update Record Status "

                gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
                Int64 _trnId = 0;

                for (int i = 0; i < TransactionIDs.Count; i++)
                {
                    _trnId = 0;
                    _trnId = Convert.ToInt64(TransactionIDs[i]);
                }
                if (ogloBilling != null) { ogloBilling.Dispose(); }

                #endregion

                _TransactionId = Convert.ToInt64(_TransactionIds[_TransactionIdCounter].ToString());
                _MstTransactionId = Convert.ToInt64(_MstTransactionIds[_TransactionIdCounter].ToString());
            }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion

        }


        private void Fill_Details()
        {
            

            if (_TransactionId > 0 && _MstTransactionId > 0)
            {
                _LoadPatientFromTransaction = true;
                if (_TransactionIdCounter > -1)
                    lblbottom.Text = (_TransactionIdCounter + 1).ToString() + " of " + _TransactionIds.Count.ToString();
                else
                    lblbottom.Text = "";
                gloBilling oBilling = new gloBilling(_databaseconnectionstring, "");
                _PatientID = oBilling.GetTransactionsPatientID(_MstTransactionId, _ClinicID);
                oBilling.Dispose();
                oPatientStripControl.FillDetails(_PatientID, 0, false);
                if (IsFormLoad == true)
                {
                    panel1.Controls.Remove(oUB04);
                }
                oUB04.FillUB04Form(_TransactionId, _MstTransactionId);
                DataTable _dtResult = null;
                _dtResult = gloBilling.getPatientDetails(_TransactionId);
                if (_dtResult != null & _dtResult.Rows.Count > 0)
                {
                    oPatientStripControl.LblClaimNo = _dtResult.Rows[0]["sClaimNumber"].ToString();
                    _dtResult.Dispose();
                }
                if (IsFormLoad == true)
                {
                    panel1.Controls.Add(oUB04);
                }
                _LoadPatientFromTransaction = false;

                //gloPM6010
                lblMessage.Text ="";
                string InvalidType = string.Empty;
                bool _result = oBilling.ValidateExpandedClaimLimits(_MstTransactionId, _TransactionId, 4, _ClinicID, true, out InvalidType);
                if (_result == false)
                {
                    lblMessage.Text = "Warning – the entire claim cannot be displayed.  " + InvalidType + " are not visible.";
                }
                FetchVoidedInformation();
               
            }
            if (_TransactionIds.Count > 1)
            {
                tls_btnNext.Visible = true;
                if (_TransactionIdCounter != TransactionIDs.Count - 1)
                    tls_btnNext.Enabled = true;
                else
                    tls_btnNext.Enabled = false;
                if (_TransactionIdCounter < 1)
                    tls_btnPrevious.Enabled = false;
                else
                    tls_btnPrevious.Enabled = true;
            }
            else
            {
                tls_btnPrevious.Visible = false;
                tls_btnNext.Visible = false;
            }
            if (_bIsFromModifyChr)
            {
                tls_btnNext.Visible = false;
                tls_btnPrevious.Visible = false;
                tsb_Jump.Visible = false;
                tsb_Modify.Visible = false;

            }
        }

        private void frmUB04_Load(object sender, EventArgs e)
        {
            //gloClaimManager oglo = null;
            try
            {
                gloSettings.DatabaseSetting.DataBaseSetting obSettings = new gloSettings.DatabaseSetting.DataBaseSetting();

                string _sHeightofCMS1500 = obSettings.ReadSettings_XML("BATCH", "HeightofUB04");
                if (_sHeightofCMS1500.Trim() != "")
                {
                    this.Height = Convert.ToInt32(_sHeightofCMS1500);
                }
                // code added for screen resolution issue Sameer 04-06-2014
                Rectangle screen = Screen.PrimaryScreen.WorkingArea;
                int h = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.95);
                if (this.Height > screen.Height)
                {
                    this.Height = h;
                }
                //
                oUB04 = new gloCMSEDI.UB04(_databaseconnectionstring);
                LoadPatientStripControl();
                IsFormLoad = true;
                Fill_Details();
                IsFormLoad = false;
                _ContactID = oUB04.ContactID;
                this.panel1.MouseWheel += new MouseEventHandler(panel1_MouseWheel);
             }
            catch //(Exception ex)
            {
            }
            finally
            {

            }
                              
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0x84: //WM_NCHITTEST
                    var result = (HitTest)m.Result.ToInt32();
                    if (result == HitTest.Left || result == HitTest.Right)
                        m.Result = new IntPtr((int)HitTest.Caption);
                    if (result == HitTest.TopLeft || result == HitTest.TopRight)
                        m.Result = new IntPtr((int)HitTest.Top);
                    if (result == HitTest.BottomLeft || result == HitTest.BottomRight)
                        m.Result = new IntPtr((int)HitTest.Bottom);

                    break;
            }
        }

        enum HitTest
        {
            Caption = 2,
            Transparent = -1,
            Nowhere = 0,
            Client = 1,
            Left = 10,
            Right = 11,
            Top = 12,
            TopLeft = 13,
            TopRight = 14,
            Bottom = 15,
            BottomLeft = 16,
            BottomRight = 17,
            Border = 18
        }
        private void LoadPatientStripControl()
        {
            try
            {
                if (oPatientStripControl != null)
                {
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (this.Controls[i].Name == oPatientStripControl.Name)
                        {
                            this.Controls.RemoveAt(i);
                            break;
                        }
                    }
                }


                if (_bIsFromModifyChr)
                {
                    oPatientStripControl = new gloPatientStripControl.gloClaimSearchControl(_databaseconnectionstring, false, false, true);
                }
                else
                {
                    oPatientStripControl = new gloPatientStripControl.gloClaimSearchControl(_databaseconnectionstring, true, true, true);
                }
               
                oPatientStripControl.OnPatientSearchKeyPress += new gloPatientStripControl.gloClaimSearchControl.PatientSearchKeyPressHandler(oPatientStripControl_OnPatientSearchKeyPress);
                oPatientStripControl.PatientModified += new gloPatientStripControl.gloClaimSearchControl.Patient_Modified(oPatientStripControl_Patient_Modify);
                
                
                oPatientStripControl.FillDetails(_PatientID,  1, false);
                this.Controls.Add(oPatientStripControl);
                oPatientStripControl.Dock = DockStyle.Top;
                oPatientStripControl.SendToBack();
                oPatientStripControl.Padding = new Padding(3, 0, 3, 0);
                panel2.SendToBack();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        void oPatientStripControl_Patient_Modify(object sender, EventArgs e)
        {
            oUB04.ClearForm(null);
            _PatientID = Convert.ToInt64(oPatientStripControl.PatientID);

            if (oUB04 != null)
            {
                oUB04.FillUB04Form(_TransactionId, _MstTransactionId);
            }
        }
        void oPatientStripControl_OnPatientSearchKeyPress(object sender, KeyPressEventArgs e)
        {
            if (_LoadPatientFromTransaction == false)
            {
                if (oPatientStripControl.PatientID > 0 && oPatientStripControl.TransactionID > 0)
                {
                    oUB04.ClearForm(null);
                    _PatientID = Convert.ToInt64(oPatientStripControl.PatientID);
                    _TransactionId = Convert.ToInt64(oPatientStripControl.TransactionID);
                    _MstTransactionId = Convert.ToInt64(oPatientStripControl.TransactionMasterID);
                    
                    if (_TransactionIds.Contains(_TransactionId))
                    {
                        _TransactionIdCounter = _TransactionIds.IndexOf(_TransactionId);
                        if (_TransactionIdCounter == 0)
                        {
                            tls_btnPrevious.Enabled = false;
                            tls_btnNext.Enabled = true;
                        }
                        else if (_TransactionIdCounter == _TransactionIds.Count - 1)
                        {
                            tls_btnNext.Enabled = false;
                            tls_btnPrevious.Enabled = true;
                        }
                        else if (_TransactionIdCounter > 0 && _TransactionIdCounter < _TransactionIds.Count - 1)
                        {
                            tls_btnNext.Enabled = true;
                            tls_btnPrevious.Enabled = true;
                        }
                        lblbottom.Text = (_TransactionIdCounter + 1).ToString() + " of " + _TransactionIds.Count.ToString();
             
                    }
                    else
                    {
                        tls_btnPrevious.Enabled = false;
                        tls_btnNext.Enabled = true;
                        lblbottom.Text = "";
                        _TransactionIdCounter = -1;
                    }

                    if(Convert.ToInt16(BillingType.Institutional) !=  oPatientStripControl.GetBillingType(_TransactionId,_MstTransactionId) )
                    {
                        oPatientStripControl.ClearControl();
                        oUB04.ClearForm(null);
                        MessageBox.Show("Select institutional claim. ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tls_btnPrint.Enabled = false;
                        tls_btnPrintData.Enabled = false;
                        tsb_Modify.Enabled = false;
                        return;
                    }

                   if (oUB04 != null)
                    {
                        oUB04.FillUB04Form(_TransactionId, _MstTransactionId);
                     }
                                      
                    tls_btnPrint.Enabled = true;
                    tls_btnPrintData.Enabled = true;
                    tsb_Modify.Enabled = true;
                    lblMessage.Text = "";
                    FetchVoidedInformation();
                }
                else
                {
                    lblMessage.Text = "";
                    lblbottom.Text = "";
                    oUB04.ClearForm(null);
                    _PatientID = Convert.ToInt64(oPatientStripControl.PatientID);
                    oPatientStripControl.ClearControl();
                    _TransactionIdCounter = -1;
                    oUB04.ClearForm(null);  
                    tls_btnPrint.Enabled = false;
                    tls_btnPrintData.Enabled = false;
                    tsb_Modify.Enabled = false;
                    tls_btnPrevious.Enabled = false;
                    tls_btnNext.Enabled = true;
                    MessageBox.Show("Claim selected is invalid or does not exist", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);        
                  
                }
            }
        }

        

        private void tls_btnPrint_Click(object sender, EventArgs e)
        {
            gloBilling oBilling = new gloBilling(_databaseconnectionstring, "");
            //Hemant
            if (gloCharges.IsGettingBrokenRules(_MstTransactionId, _TransactionId))
            {
                string msgBrokenrule = "Claim Rule Warning:\n\nClaim selected for billing has error(s) or warning(s) present. Continue?";

                switch (MessageBox.Show(msgBrokenrule, _messageboxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    case DialogResult.OK:
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.Validation, gloAuditTrail.ActivityType.OK, msgBrokenrule, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                        break;
                    case DialogResult.Cancel:
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.Validation, gloAuditTrail.ActivityType.Cancle, msgBrokenrule, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                        return;
                    default:
                        break;
                }
                msgBrokenrule = string.Empty;
            }

            //Hemant

            string InvalidType = string.Empty;
            bool _result = oBilling.ValidateExpandedClaimLimits(_MstTransactionId, _TransactionId, 4, _ClinicID, false,out InvalidType);
            DataTable _dtResult = null;
            _dtResult = gloBilling.getPatientDetails(_TransactionId);
            string _sPatientName = String.Empty;
            string _sPatientCode = String.Empty;
            string _sClaimNumber = String.Empty;
            string _sClaimInfo = String.Empty;
            string msg = String.Empty;
            if (_dtResult != null & _dtResult.Rows.Count > 0)
            {
                _sPatientName = Convert.ToString(_dtResult.Rows[0]["sPatientName"]);
                _sPatientCode = Convert.ToString(_dtResult.Rows[0]["sPatientCode"]);
                _sClaimNumber = Convert.ToString(_dtResult.Rows[0]["sClaimNumber"]);
                _sClaimInfo = Convert.ToString(_dtResult.Rows[0]["sClaimNumber"]) + "   " + Convert.ToString(_dtResult.Rows[0]["sPatientCode"]) + "-" + Convert.ToString(_dtResult.Rows[0]["sPatientName"]);
                msg = "ICD9/ICD10 Usage Warning:\n\nClaim Date of Service does not match ICD type  for the following claims:\n\n" + _sClaimInfo.ToString() + "\n\nOk to bill these claims now?";

                _dtResult.Dispose();
            }

            if (_result == false)
            {
                
                string _message =string.Empty;
                MessageBox.Show("Batch could not be printed.\nClaim " + _sClaimNumber + " for patient " + _sPatientCode + " - " + _sPatientName + " has too many "+ InvalidType.Replace("some","") +".", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                TransactionStatus _claimStatus = TransactionStatus.None;
                DialogResult _Result;
                _claimStatus = oBilling.GetTransactionStatus(_MstTransactionId, TransactionId);
                ClaimStatus _Status = ClaimStatus.None;
                _Status = oBilling.GetClaimStatus(_MstTransactionId, TransactionId);


                gloGlobal.gloICD.CodeRevision _codeversion = gloGlobal.gloICD.CodeRevision.ICD9;

                _codeversion = oBilling.GetTransactionICDVersion(_MstTransactionId, TransactionId);

                Int64 _nMinimumDOS = 0;

                _nMinimumDOS = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oBilling.GetClaimMinimumDOS(_MstTransactionId, TransactionId)));

                if (_codeversion != oBilling.GetICDCodeType(_ContactID, _nMinimumDOS))
                {
                    if (MessageBox.Show(msg, _messageboxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                    {
                        return;
                    }
                } 
                
                 _Result = DialogResult.No;
                 if (!bIsClaimHold)
                 {
                     if (_claimStatus == TransactionStatus.Queue && _Status != ClaimStatus.Close && !oBilling.IsClaimorinsuranceonHold(TransactionId, _MstTransactionId, _ContactID))
                     {
                         _Result = MessageBox.Show("Track Print as a new billing to insurance? " + Environment.NewLine + "" + Environment.NewLine + "Yes – New Billing [System will Print, Batch, Reset Aging, Add to Claim History]" + Environment.NewLine + "No – Just Print", _messageboxcaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                     }
                 }
                if (_Result != DialogResult.Cancel)
                {
                    if (_Result == DialogResult.Yes)
                    {
                        if (CheckInsuranceBillingMethod() == false)
                        {
                            return;
                        }
                        oUB04.PrintForm("");
                        oBilling.ClaimNumber = _sClaimNumber;
                        oBilling.SetClaimNumbers();
                        oBilling.AutoBatchPrintedClaims(oBilling.MainClaimNumber, _MstTransactionId, _TransactionId, BatchBillingMethod.UB04, _ContactID, "");
                        oUB04.markPrinted();
                        //AddPrintingnote(_MstTransactionId);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupHCFA1500, gloAuditTrail.ActivityType.Print, "Paper UB04 printed and auto batched for the claim no " + _sClaimNumber, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    }
                    else if (_Result == DialogResult.No)
                    {
                        oUB04.PrintForm("");
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupHCFA1500, gloAuditTrail.ActivityType.Print, "Paper UB04 printed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    }
                }
            }
        }
        private Boolean CheckInsuranceBillingMethod()
        {
            gloBilling oBilling = new gloBilling(_databaseconnectionstring, "");
            bool _result = false;
            try
            {
                if (oBilling.GetInsuranceBillingMethod(_ContactID) == 1)
                {
                    string msgBillingMethod = "Billing Method Warning:\n\nClaim selected for billing has different billing method. Continue?";
                    switch (MessageBox.Show(msgBillingMethod, _messageboxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        case DialogResult.OK:
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Batch, gloAuditTrail.ActivityCategory.Validation, gloAuditTrail.ActivityType.OK, msgBillingMethod, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                            _result = true;
                            break;
                        case DialogResult.Cancel:
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Batch, gloAuditTrail.ActivityCategory.Validation, gloAuditTrail.ActivityType.Cancle, msgBillingMethod, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                            _result = false;
                            break;
                        default: _result = false;
                            break;
                    }
                    msgBillingMethod = string.Empty;
                }
                else
                {
                    _result = true;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (oBilling != null)
                {
                    oBilling.Dispose(); oBilling = null;
                }
            }
            return _result;
        }
      

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupUB04, gloAuditTrail.ActivityType.View, "View Transaction on UB04", gloAuditTrail.ActivityOutCome.Success);
            //Added Rahul on 20101012
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupUB04, gloAuditTrail.ActivityType.View, "View Transaction on UB04", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            //
            this.Close();
        }

        private void tls_btnNext_Click(object sender, EventArgs e)
        {
            if (_TransactionIdCounter < _TransactionIds.Count)
            {
                oUB04.ClearForm(null);
                _LoadPatientFromTransaction = true;
                _TransactionIdCounter = _TransactionIdCounter + 1;
                _TransactionId = Convert.ToInt64(_TransactionIds[_TransactionIdCounter].ToString());
                _MstTransactionId = Convert.ToInt64(_MstTransactionIds[_TransactionIdCounter].ToString());//MaheshB 02152010
                gloBilling oBilling = new gloBilling(_databaseconnectionstring, "");               
                _PatientID = oBilling.GetTransactionsPatientID(_MstTransactionId, _ClinicID);                                                    
                oBilling.Dispose();
                lblbottom.Text = (_TransactionIdCounter + 1).ToString() + " of " + _TransactionIds.Count.ToString();
              
                oPatientStripControl.FillDetails(_PatientID,  0, false);

                oUB04.FillUB04Form(_TransactionId, _MstTransactionId);
                _ContactID = oUB04.ContactID;
                DataTable _dtResult = null;
                _dtResult = gloBilling.getPatientDetails(_TransactionId);
                if (_dtResult != null & _dtResult.Rows.Count > 0)
                {
                    oPatientStripControl.LblClaimNo = _dtResult.Rows[0]["sClaimNumber"].ToString();
                    _dtResult.Dispose();
                }
                tls_btnNext.Enabled = true;

                if (_TransactionIdCounter == _TransactionIds.Count - 1)
                {                    
                    tls_btnNext.Enabled = false;
                }
              
                _LoadPatientFromTransaction = false;
                if (_TransactionIdCounter == 0)
                    tls_btnPrevious.Enabled = false;
                else
                    tls_btnPrevious.Enabled = true;

                tls_btnPrint.Enabled = true;
                tls_btnPrintData.Enabled = true;
                
                tsb_Modify.Enabled = true;
                lblMessage.Text = "";
                string InvalidType = string.Empty;
                bool _result = oBilling.ValidateExpandedClaimLimits(_MstTransactionId, _TransactionId, 4, _ClinicID, true, out InvalidType);
                if (_result == false)
                {
                    lblMessage.Text = "Warning – the entire claim cannot be displayed.  " + InvalidType + " are not visible.";
                }
                FetchVoidedInformation();     
              
            }
        }

        private void tls_btnPrevious_Click(object sender, EventArgs e)
        {
            if (_TransactionIdCounter > 0)
            {
                oUB04.ClearForm(null);
                _LoadPatientFromTransaction = true;
                _TransactionIdCounter = _TransactionIdCounter - 1;
                _TransactionId = Convert.ToInt64(_TransactionIds[_TransactionIdCounter].ToString());
                _MstTransactionId = Convert.ToInt64(_MstTransactionIds[_TransactionIdCounter].ToString());
                gloBilling oBilling = new gloBilling(_databaseconnectionstring, "");
               
                _PatientID = oBilling.GetTransactionsPatientID(_MstTransactionId, _ClinicID);
                             
                oPatientStripControl.FillDetails(_PatientID,  0, false);
                oBilling.Dispose();
                lblbottom.Text = (_TransactionIdCounter + 1).ToString() + " of " + _TransactionIds.Count.ToString();
                DataTable _dtResult = null;
                _dtResult = gloBilling.getPatientDetails(_TransactionId);
                if (_dtResult != null & _dtResult.Rows.Count > 0)
                {
                    oPatientStripControl.LblClaimNo = _dtResult.Rows[0]["sClaimNumber"].ToString();
                    _dtResult.Dispose();
                }
                oUB04.FillUB04Form(_TransactionId, _MstTransactionId);
                _ContactID = oUB04.ContactID;
                if (_TransactionIdCounter == 0)
                {
                    tls_btnPrevious.Enabled = false;                   
                }
                _LoadPatientFromTransaction = false;
                tls_btnNext.Enabled = true;
                
                tls_btnPrint.Enabled = true;
                tls_btnPrintData.Enabled = true;
                tsb_Modify.Enabled = true;
                lblMessage.Text = "";
                string InvalidType = string.Empty;
                bool _result = oBilling.ValidateExpandedClaimLimits(_MstTransactionId, _TransactionId, 4, _ClinicID, true, out InvalidType);
                if (_result == false)
                {
                    lblMessage.Text = "Warning – the entire claim cannot be displayed.  " + InvalidType + " are not visible.";
                }
                FetchVoidedInformation();
             
            }
        }

        private void tls_btnPrintData_Click(object sender, EventArgs e)
        {
            gloBilling oBilling = new gloBilling(_databaseconnectionstring, "");
            //Hemant
            if (gloCharges.IsGettingBrokenRules(_MstTransactionId, _TransactionId))
            {
                string msgBrokenrule = "Claim Rule Warning:\n\nClaim selected for billing has error(s) or warning(s) present. Continue?";

                switch (MessageBox.Show(msgBrokenrule, _messageboxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    case DialogResult.OK:
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.Validation, gloAuditTrail.ActivityType.OK, msgBrokenrule, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                        break;
                    case DialogResult.Cancel:
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.Validation, gloAuditTrail.ActivityType.Cancle, msgBrokenrule, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                        return;
                    default:
                        break;
                }
                msgBrokenrule = string.Empty;
            }

            //Hemant

            string InvalidType = string.Empty;
            bool _result = oBilling.ValidateExpandedClaimLimits(_MstTransactionId, _TransactionId, 4, _ClinicID, false,out InvalidType);
            DataTable _dtResult = null;
            _dtResult = gloBilling.getPatientDetails(_TransactionId);
            string _sPatientName = String.Empty;
            string _sPatientCode = String.Empty;
            string _sClaimNumber = String.Empty;
            string _sClaimInfo = String.Empty;
            string msg = String.Empty;
            if (_dtResult != null & _dtResult.Rows.Count > 0)
            {
                _sPatientName = Convert.ToString(_dtResult.Rows[0]["sPatientName"]);
                _sPatientCode = Convert.ToString(_dtResult.Rows[0]["sPatientCode"]);
                _sClaimNumber = Convert.ToString(_dtResult.Rows[0]["sClaimNumber"]);
                _sClaimInfo = Convert.ToString(_dtResult.Rows[0]["sClaimNumber"]) + "   " + Convert.ToString(_dtResult.Rows[0]["sPatientCode"]) + "-" + Convert.ToString(_dtResult.Rows[0]["sPatientName"]);
                msg = "ICD9/ICD10 Usage Warning:\n\nClaim Date of Service does not match ICD type  for the following claims:\n\n" + _sClaimInfo.ToString() + "\n\nOk to bill these claims now?";

                _dtResult.Dispose();
            }
            if (_result == false)
            {
              

                MessageBox.Show("Batch could not be printed.\nClaim " + _sClaimNumber + " for patient " + _sPatientCode + " - " + _sPatientName + " has too many " + InvalidType.Replace("some", "") + ".", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                TransactionStatus _claimStatus = TransactionStatus.None;
                DialogResult _Result;
                _claimStatus = oBilling.GetTransactionStatus(_MstTransactionId, TransactionId);
                ClaimStatus _Status = ClaimStatus.None;
                _Status = oBilling.GetClaimStatus(_MstTransactionId, TransactionId);

                gloGlobal.gloICD.CodeRevision _codeversion = gloGlobal.gloICD.CodeRevision.ICD9;

                _codeversion = oBilling.GetTransactionICDVersion(_MstTransactionId, TransactionId);

                Int64 _nMinimumDOS = 0;

                _nMinimumDOS = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oBilling.GetClaimMinimumDOS(_MstTransactionId, TransactionId)));

                if (_codeversion != oBilling.GetICDCodeType(_ContactID, _nMinimumDOS))
                {
                    if (MessageBox.Show(msg, _messageboxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                 _Result = DialogResult.No;
                 if (!bIsClaimHold)
                 {
                     if (_claimStatus == TransactionStatus.Queue && _Status != ClaimStatus.Close && !oBilling.IsClaimorinsuranceonHold(TransactionId, _MstTransactionId, _ContactID))
                     {
                         _Result = MessageBox.Show("Track Print as a new billing to insurance? " + Environment.NewLine + "" + Environment.NewLine + "Yes – New Billing [System will Print, Batch, Reset Aging, Add to Claim History]" + Environment.NewLine + "No – Just Print", _messageboxcaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                     }
                 }

                 if (_Result != DialogResult.Cancel)
                 {
                     if (_Result == DialogResult.Yes)
                     {
                         if (CheckInsuranceBillingMethod() == false)
                         {
                             return;
                         }
                         oUB04.PrintData("");
                         oBilling.AutoBatchPrintedClaims(Convert.ToInt64(_sClaimNumber), _MstTransactionId, _TransactionId, BatchBillingMethod.UB04, _ContactID, "");
                         oUB04.markPrinted();
                         //AddPrintingnote(_MstTransactionId);
                         gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupHCFA1500, gloAuditTrail.ActivityType.Print, "Paper UB04 printed and auto batched for the claim no " + _sClaimNumber, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                     }
                     else if (_Result == DialogResult.No)
                     {
                         oUB04.PrintData("");
                         gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupHCFA1500, gloAuditTrail.ActivityType.Print, "Paper UB04 printed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                     }
                 }
            }
        }

        //private void frmUB04_Resize(object sender, EventArgs e)
        //{
        //    this.Width = 1032;
        //}

        private void frmUB04_FormClosing(object sender, FormClosingEventArgs e)
        {
            gloSettings.DatabaseSetting.DataBaseSetting obSettings = new gloSettings.DatabaseSetting.DataBaseSetting();

            obSettings.WriteSettings_XML("BATCH", "HeightofUB04", this.Height.ToString());

            //if (objFrmBillingBatch_New != null)
            //{
            //    objFrmBillingBatch_New.SetView();
                
            //}
            LastAccessedTransaction = _TransactionId;
            if (_bIsFromModifyChr && frmModifyChr!=null)
            {
                frmModifyChr.RefreshModifyCharges();
            }          
        }

        private void tsb_Modify_Click(object sender, EventArgs e)
        {

            try
            {
                if (_TransactionId > 0 && _PatientID > 0)
                {
                    bool _isTransactionOpen = false;
                    string _recordMachineId = "";
                    Int64 _recordUserId = 0;

                    gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
                    _isTransactionOpen = ogloBilling.IsRecordOpen(_TransactionId, out _recordMachineId, out _recordUserId);
                    //ogloBilling.Dispose();

                    if (_isTransactionOpen == false)
                    {
                        ogloBilling.ShowModifyCharges(_PatientID, _TransactionId, false, this);
                        //SetView();
                        //ogloBilling.Dispose();
                      /*  if(objFrmBillingBatch_New.IsClaimOnHold(_TransactionId))
                            objFrmBillingBatch_New.SelectedCharges.Remove(Convert.ToDecimal(_TransactionId));*/
                    }
                    else
                    {
                        DialogResult _dlgRst = DialogResult.None;
                        _dlgRst = MessageBox.Show("Transaction is already opened for modify on machine " + _recordMachineId + " \n Would you like to open this in View mode.", _messageboxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (_dlgRst == DialogResult.OK)
                        {
                            ogloBilling.ShowModifyCharges(_PatientID, _TransactionId, false, this);
                            if (objFrmBillingBatch_New.IsClaimOnHold(_TransactionId))
                                objFrmBillingBatch_New.SelectedCharges.Remove(Convert.ToDecimal( _TransactionId));
                            //ogloBilling.Dispose();
                            // SetView();
                        }
                    }
                    if (ogloBilling != null)
                    {
                        ogloBilling.Dispose();
                        ogloBilling = null;
                    }
                    FetchVoidedInformation();
                    oUB04.ClearForm(null);
                    oUB04.FillUB04Form(_TransactionId, _MstTransactionId);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void tsb_Jump_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList ClaimList = new ArrayList();
                if (CallingTab == "Queue")
                    for (int index = 0; index < objFrmBillingBatch_New.QueuedClaims.Rows.Count; index++)
                    {
                        if (objFrmBillingBatch_New.SelectedCharges.Contains(objFrmBillingBatch_New.QueuedClaims.GetData(index, objFrmBillingBatch_New.QueuedClaims.Cols["TransactionID"].Index)))
                            ClaimList.Add(objFrmBillingBatch_New.QueuedClaims.GetData(index, objFrmBillingBatch_New.QueuedClaims.Cols["Claim"].Index));
                    }
                else if (CallingTab == "Batch")
                    for (int index = 1; index < objFrmBillingBatch_New.BatchGridClaims.Rows.Count; index++)
                        ClaimList.Add(objFrmBillingBatch_New.BatchGridClaims.GetData(index, objFrmBillingBatch_New.BatchGridClaims.Cols["Claim"].Index));
               
                else if (CallingTab == "SentBatch")
                { }

                frmClaimSearch ofrmClaimSearch = new frmClaimSearch(ClaimList);
                ofrmClaimSearch.ShowDialog(this);
                if (ofrmClaimSearch.oDialogResult == true)
                {
                    if (ofrmClaimSearch.SelectedOption == frmClaimSearch.SelectedOptions.ClaimByPosition)
                    {
                        _TransactionIdCounter = Convert.ToInt16(ofrmClaimSearch.SelectedValue) - 1;
                        _TransactionId = Convert.ToInt64(_TransactionIds[_TransactionIdCounter].ToString());
                        _MstTransactionId = Convert.ToInt64(_MstTransactionIds[_TransactionIdCounter].ToString());
                        oUB04.ClearForm(null);
                        Fill_Details();
                    }
                    else if (ofrmClaimSearch.SelectedOption == frmClaimSearch.SelectedOptions.ClaimByClaimNo)
                    {
                        _TransactionIdCounter = Convert.ToInt16(ClaimList.IndexOf(ofrmClaimSearch.SelectedValue));
                        _TransactionId = Convert.ToInt64(_TransactionIds[_TransactionIdCounter].ToString());
                        _MstTransactionId = Convert.ToInt64(_MstTransactionIds[_TransactionIdCounter].ToString());
                        oUB04.ClearForm(null);
                        Fill_Details();
                    }
                    else if (ofrmClaimSearch.SelectedOption == frmClaimSearch.SelectedOptions.HighlightedClaim)
                    {
                        if (CallingTab == "Queue")
                        {
                            _TransactionId = Convert.ToInt64(objFrmBillingBatch_New.QueuedClaims.GetData(objFrmBillingBatch_New.QueuedClaims.RowSel, objFrmBillingBatch_New.QueuedClaims.Cols["TransactionID"].Index));
                            _MstTransactionId = Convert.ToInt64(objFrmBillingBatch_New.QueuedClaims.GetData(objFrmBillingBatch_New.QueuedClaims.RowSel, objFrmBillingBatch_New.QueuedClaims.Cols["TransactionMasterID"].Index));
                            
                                if (objFrmBillingBatch_New.GetBillingType(_TransactionId, _MstTransactionId) != Convert.ToInt16(BillingType.Institutional))
                                {
                                    oUB04.ClearForm(null);
                                    oPatientStripControl.ClearControl();
                                    lblMessage.Text = "";
                                    lblbottom.Text = "";
                                    tls_btnPrevious.Enabled = false;
                                    tls_btnNext.Enabled = true;
                                    _TransactionIdCounter = -1;
                                    tls_btnPrint.Enabled = false;
                                    tls_btnPrintData.Enabled = false;
                                    tsb_Modify.Enabled = false;
                                    MessageBox.Show("Select institutional claim(s). ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (ofrmClaimSearch != null)
                                    {
                                        ofrmClaimSearch.Dispose();
                                        ofrmClaimSearch = null;
                                    }
                                    return;
                                }
                        }
                        else if (CallingTab == "Batch")
                        {
                            _TransactionId = Convert.ToInt64(objFrmBillingBatch_New.BatchGridClaims.GetData(objFrmBillingBatch_New.BatchGridClaims.RowSel, objFrmBillingBatch_New.BatchGridClaims.Cols["TransactionID"].Index));
                            _MstTransactionId = Convert.ToInt64(objFrmBillingBatch_New.BatchGridClaims.GetData(objFrmBillingBatch_New.BatchGridClaims.RowSel, objFrmBillingBatch_New.BatchGridClaims.Cols["TransactionMasterID"].Index));
                        }

                        if (_TransactionIds.Contains(_TransactionId))
                            _TransactionIdCounter = _TransactionIds.IndexOf(_TransactionId);
                        else
                        {
                            tls_btnPrevious.Enabled = false;
                            tls_btnNext.Enabled = true;
                            _TransactionIdCounter = -1;
                        }
                        oUB04.ClearForm(null);
                        Fill_Details();
                    }
                    
                }
                if (ofrmClaimSearch != null)
                {
                    ofrmClaimSearch.Dispose();
                    ofrmClaimSearch = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        private void FetchVoidedInformation()
        {
            #region "Fetch Voided Information"

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                DataTable _dt = null;
                string strQuery = "";
                strQuery = "SELECT  dbo.CONVERT_TO_DATE(BL_Transaction_Claim_MST.nVoidCloseDate) AS nVoidCloseDate, " +
                            " BL_Transaction_Claim_MST.dtVoidDate , BL_Transaction_Claim_MST.nVoidTrayID, isnull(BL_ChargesTray.sDescription,'') as VoidTrayDesc, " +
                            " BL_Transaction_Claim_MST.nVoidUserID, isnull(User_MST.sLoginName,'') as VoidUserName " +
                            " FROM BL_Transaction_Claim_MST WITH(NOLOCK) LEFT  Outer JOIN " +
                            " User_MST ON BL_Transaction_Claim_MST.nVoidUserID = User_MST.nUserID LEFT OUTER JOIN " +
                            " BL_ChargesTray ON BL_Transaction_Claim_MST.nVoidTrayID = BL_ChargesTray.nChargeTrayID " +
                           " WHERE BL_Transaction_Claim_MST.nTransactionMasterID= " + _MstTransactionId + " and ISNULL(BL_Transaction_Claim_MST.bIsVoid,0)=1";
                oDB.Retrive_Query(strQuery, out _dt);
                oDB.Disconnect();

                if (_dt != null && _dt.Rows.Count > 0)
                {
                    lblMessage.Text = "Voided  [" + Convert.ToString(_dt.Rows[0]["VoidUserName"]) + "]  on " + Convert.ToString(_dt.Rows[0]["nVoidCloseDate"]).ToLower() + "";
                }
                else
                {
                    GetHoldMessage();
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {

                oDB.Dispose();
            }

            #endregion
        }
        #region " Other Method "
        private void GetHoldMessage()
        {
            DataTable dtHold = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);
                oDB.Retrive_Query("Select isnull(bIsHold,0) as bIsHold from BL_Transaction_Claim_Mst WITH(NOLOCK) where nTransactionID='" + _TransactionId + "'", out dtHold);
                if (dtHold != null && dtHold.Rows.Count > 0)
                {
                    if (dtHold.Rows[0]["bIsHold"] != DBNull.Value && Convert.ToString(dtHold.Rows[0]["bIsHold"]).Trim() != "" && Convert.ToBoolean(dtHold.Rows[0]["bIsHold"]) == true)
                    {

                        lblMessage.Text = "Claim On Hold";
                    }

                }
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

                dtHold.Dispose();


            }
        }
        #endregion

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {               
                oUB04.Refresh();               
            }
           
        }
        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
                oUB04.Refresh();   
        }

        private void AddPrintingnote(Int64 _MasterTransactionID)
        {

            Common.GeneralNotes oNotes = null;
            Common.GeneralNote oNote = new global::gloBilling.Common.GeneralNote();
            //validation for text box

            try
            {
                oNote = new global::gloBilling.Common.GeneralNote();
                oNote.TransactionID = _MasterTransactionID;
                oNote.TransactionLineId = 0;
                oNote.TransactionDetailID = 0;
                oNote.NoteType = NoteType.Claim_Note;
                oNote.NoteID = 0;
                oNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                oNote.UserID = _UserID;
                oNote.StatementNoteDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                oNote.NoteDescription = @"Claim printed by """ + _UserName + @""" on """ + DateTime.Now.ToString("MM/dd/yyyy hh:mm:tt") + @""" on UB04.";
                oNote.ClinicID = _ClinicID;

                oNote.dtCreatedDatetime = DateTime.Now;

                oNotes = new global::gloBilling.Common.GeneralNotes();
                oNotes.Add(oNote);

                gloCharges.SaveClaimNotes(oNotes);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;

            }
            finally
            {
                oNotes.Clear();
                oNotes.Dispose();
            }


        }
    }


}
