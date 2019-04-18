using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloBilling.Common;
using gloAppointmentBook.Books;
using gloContacts;
using gloSettings;

namespace gloBilling
{
    //public enum ProviderType
    //{
    //    BillingProvider = 1,
    //    PayToProvider = 2,
    //    RefferingProvider = 3,
    //    RenderingProvider = 4
    //}

    //public enum AddressType
    //{
    //    None = 0,
    //    ProviderAddress = 1,
    //    FacilityAddress = 2,
    //    ClinicAddress = 3
    //}

    //public enum NPIType
    //{
    //    None = 0,
    //    BillingProviderNPI = 1,
    //    FacilityNPI = 2,
    //    ClinicNPI = 3,
    //}


    public partial class frmHCFA1500New : Form
    {

        #region " Variables "

        private ArrayList _arrHCFAData = null;
        private string _databaseconnectionstring = "";
      //  private string _emrdatabaseConnectionString = "";
        private ArrayList _arrSelectedTransactions = null;
       // private Transaction oTransaction = null;
        private Int64 _TransactionId = 0;
        private Int64 _MstTransactionId = 0;//MaheshB 02152010
        private ArrayList _TransactionIds = new ArrayList();
        private ArrayList _MstTransactionIds = new ArrayList();//MaheshB 02152010
        public int _TransactionIdCounter = 0;
        private bool _LoadPatientFromTransaction = false;
   //     string _strPatientStatus = "";
     //   bool IsSecondaryInsurance = false;
        private bool UB04Setting = false;
        // added by sandip dhakane
        // private string _messageboxcaption = "";
        private string _messageboxcaption = " gloPM ";

        Boolean _bIsModify = false;

        private string _BusinessCenterCode = "";

        string _PatientAccountCode = "Claim Number";

        private Int64 _UserID = 0;
        private string _UserName = "";

        public frmBillingBatch_New objFrmBillingBatch_New;
        public String CallingTab;
        gloPatientStripControl.gloClaimSearchControl oPatientStripControl = null;
        //gloPatientStripControl.gloPatientStripControl oPatientStripControl = null;
    //    private gloListControl.gloListControl oListControl;
   //     private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        
        gloCMSEDI.HCFA1500New oHcfA1500;
        private Int64 _PatientID = 0;
        Boolean _bIsClaimHold = false;
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

        public ArrayList HCFAData
        {
            get { return _arrHCFAData; }
            set { _arrHCFAData = value; }
        }

        public Int64 LastAccessedTransaction
        {
            get ; set; 
        }


        private string _DefaultTOSCode = "";
        private string _DefaultTOSDesc = "";
        private string _DefaultPOSCode = "";
        private string _DefaultPOSDesc = "";

        private Int64 _ContactID = 0;
        Int64 _ClinicID = 0;
        private bool _IsValidData = true;
        private bool _bIsFromModifyChr = false;
        public frmBillingModifyCharges frmModifyChr=null;


        //Signature on File
   //     private bool _IsSignatureOnFile = false;
        private DateTime _dtSignatureOnFileDate = DateTime.Now;

        //BOX13
     //   private string _InsuredPersonSign = "";
        private bool IsFormLoad = false;

        #endregion " Variables And Procedures "


        #region " Property Procedures "

        public string DefaultTOSCode
        {
            get { return _DefaultTOSCode; }
            set { _DefaultTOSCode = value; }
        }
        public string DefaultTOSDesc
        {
            get { return _DefaultTOSDesc; }
            set { _DefaultTOSDesc = value; }
        }
        public string DefaultPOSCode
        {
            get { return _DefaultPOSCode; }
            set { _DefaultPOSCode = value; }
        }
        public string DefaultPOSDesc
        {
            get { return _DefaultPOSDesc; }
            set { _DefaultPOSDesc = value; }
        }

        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        //Added By PramodNair For Disabling the Save& Close On another Tabs
        public Boolean bIsModify
        {
            get { return _bIsModify; }
            set { _bIsModify = value; }
        }

        public Boolean bIsClaimHold
        {
            get { return _bIsClaimHold; }
            set { _bIsClaimHold = value; }
        }

        public Int64 TransactionId
        {
            get { return _TransactionId; }
            set { this._TransactionId = value; }
        }
        public Int64 MstTransactionId
        {
            get { return _MstTransactionId; }
            set { this._MstTransactionId = value; }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //cp.ExStyle |= 0x02000000;
                //cp.ExStyle |= 0x00000020;//WS_EX_TRANSPARENT
                return cp;
            }
        }

        #endregion

        #region " Constructor "

        public frmHCFA1500New(string DatabaseConnection, ArrayList TransactionIds, ArrayList MstTransactionIds,  string BusinessCenterCode = "")
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnection;
            HCFAData = new ArrayList();
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
            //    string _machName = "";
             //   Int64 _userId = 0;
             //   bool _IsOpen = false;

                for (int i = 0; i < TransactionIDs.Count; i++)
                {
                    _trnId = 0;
               //     _machName = "";
               //     _userId = 0;
                //    _IsOpen = false;

                    _trnId = Convert.ToInt64(TransactionIDs[i]);
                    //_IsOpen = ogloBilling.IsRecordOpen(_trnId, out _machName, out _userId);
                    //if (_IsOpen == false)
                    //{ogloBilling.UpdateRecordStatus(_trnId, this.UserID, true);}
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

            _BusinessCenterCode= BusinessCenterCode ;
        }


        public frmHCFA1500New(string DatabaseConnection, ArrayList TransactionIds, ArrayList MstTransactionIds, bool bIsFromModifyChr, string BusinessCenterCode="" )
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnection;

            HCFAData = new ArrayList();
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
               // string _machName = "";
               // Int64 _userId = 0;
               // bool _IsOpen = false;

                for (int i = 0; i < TransactionIDs.Count; i++)
                {
                    _trnId = 0;
                 //   _machName = "";
                 //   _userId = 0;
                 //   _IsOpen = false;

                    _trnId = Convert.ToInt64(TransactionIDs[i]);
                    //_IsOpen = ogloBilling.IsRecordOpen(_trnId, out _machName, out _userId);
                    //if (_IsOpen == false)
                    //{ogloBilling.UpdateRecordStatus(_trnId, this.UserID, true);}
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
            _BusinessCenterCode = BusinessCenterCode;
        }

        #endregion " Constructor "

        #region " Form Load "


        private void Fill_Details()
        {
            if (_TransactionId > 0 && _MstTransactionId > 0)
            {
                _LoadPatientFromTransaction = true;

                gloBilling oBilling = new gloBilling(_databaseconnectionstring, "");
                _PatientID = oBilling.GetTransactionsPatientID(_MstTransactionId, _ClinicID);
                oBilling.Dispose();
                //oPatientStripControl.FillDetails(_PatientID, gloPatientStripControl.FormName.Billing, 0, false);
                //LoadPatientStripControl();
                if (IsFormLoad == true)
                {
                    panel1.Controls.Remove(oHcfA1500);
                }         
                oHcfA1500.FillTransactionOnForm(_TransactionId, _MstTransactionId);
                _ContactID = oHcfA1500.ContactID;
                oPatientStripControl.FillDetails(_PatientID, 0, false);
                //FillTransactionOnForm(_TransactionId);
                if (IsFormLoad == true)
                {
                    panel1.Controls.Add(oHcfA1500);
                }
                _LoadPatientFromTransaction = false;
                UB04Setting = oBilling.IsenableUB04(_ClinicID);
                DataTable _dtResult = null;
                _dtResult = gloBilling.getPatientDetails(_TransactionId);
                if (_dtResult != null & _dtResult.Rows.Count > 0)
                {
                    oPatientStripControl.LblClaimNo = _dtResult.Rows[0]["sClaimNumber"].ToString();
                    _dtResult.Dispose();
                }

                //string _RecordMachineName = "";
                //Int64 _RecordUserId = 0;

                //if (oBilling.IsRecordOpen(_TransactionId, out _RecordMachineName, out _RecordUserId))
                //{ SetViewMode(false); }

                if (bIsModify)
                {
                    tsb_OK.Visible = true;
                }
                else
                {
                    tsb_OK.Visible = false;
                }

                //gloPM6010
                lblbottom.Text = "";
                if (_TransactionIdCounter > -1)
                    lblbottom.Text = (_TransactionIdCounter + 1).ToString() + " of " + _TransactionIds.Count.ToString();
                else
                    lblbottom.Text = "";
                string InvalidType = string.Empty;
                bool _resultClaim = oBilling.ValidateExpandedClaimLimits(_MstTransactionId, _TransactionId, 5, _ClinicID, true, out InvalidType);
                if (_resultClaim == false)
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

            tsb_OK.Visible = false;

            if (_bIsFromModifyChr)
            {
                tls_btnNext.Visible = false;
                tls_btnPrevious.Visible = false;
                tsb_Jump.Visible = false;
                tsb_Modify.Visible = false;

            }



        }

        private void frmHCFA1500_Load(object sender, EventArgs e)
        {
            gloSettings.DatabaseSetting.DataBaseSetting obSettings = new gloSettings.DatabaseSetting.DataBaseSetting();

            string _sHeightofCMS1500 = obSettings.ReadSettings_XML("BATCH", "HeightofNewCMS1500");
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
            GetDefaultTOSPOS();
            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object _result = null;
            oSettings.GetSetting("PatientAccountCode", 0, _ClinicID, out _result);
            if (_result != null)
            {
                _PatientAccountCode = Convert.ToString(_result);
            }
            _result = null;
            oSettings.Dispose();
            oHcfA1500 = new gloCMSEDI.HCFA1500New(_databaseconnectionstring);
            _ContactID = oHcfA1500.ContactID;
            LoadPatientStripControl();
            IsFormLoad = true;
            Fill_Details();
            IsFormLoad = false;
            panel1.VerticalScroll.Value = 15;
            this.panel1.MouseWheel += new MouseEventHandler(panel1_MouseWheel);
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
        #endregion Form Load

        private void SetViewMode(bool Value)
        {
            try
            {
                tsb_OK.Visible = Value;
                panel3.Enabled = Value;
                oPatientStripControl.Enabled = Value;


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }

        }


        private void GetDefaultTOSPOS()
        {
            CLsBL_TOSPOS oTOSPOS = new CLsBL_TOSPOS(_databaseconnectionstring);
            DataTable dt = null;

            //temp setting
            Int64 _DefaultTOSID = 0;
            string _POSName = "Office";
            Int64 _DefaultPOSID = 0;
            //

            try
            {
                dt = oTOSPOS.GetTOS(_DefaultTOSID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    _DefaultTOSCode = Convert.ToString(dt.Rows[0]["sTOSCode"]);
                    _DefaultTOSDesc = Convert.ToString(dt.Rows[0]["sDescription"]);
                }
                else
                {
                    _DefaultTOSCode = "";
                    _DefaultTOSDesc = "";
                }
                dt = null;

                dt = oTOSPOS.GetPOS(_POSName);

                if (dt == null || dt.Rows.Count == 0)
                {
                    dt = oTOSPOS.GetPOS(_DefaultPOSID);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    _DefaultPOSCode = Convert.ToString(dt.Rows[0]["sPOSCode"]);
                    _DefaultPOSDesc = Convert.ToString(dt.Rows[0]["sPOSName"]);
                }
                else
                {
                    _DefaultPOSCode = "";
                    _DefaultPOSDesc = "";
                }
                dt = null;
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
                if (oTOSPOS != null) { oTOSPOS.Dispose(); }
                if (dt != null) { dt.Dispose(); }
            }
        }



        #region " Tool Strip Click Events "

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupHCFA1500, gloAuditTrail.ActivityType.View, "View Transaction on HCFA1500", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);



            this.Close();
        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            ts_Commands.Focus();
            if (_IsValidData == true)
            {
                //GetData();
            }
            else
            {
                _IsValidData = true;
                return;
            }
        }

        private void tls_btnPrint_Click(object sender, EventArgs e)
        {
            DataTable _dtResult = null;
           
            try
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


                oHcfA1500.IsPrintForm = true;
                string InvalidType = string.Empty;
                string _sClaimNumber = String.Empty;
                _dtResult = gloBilling.getPatientDetails(_TransactionId);
                string _sPatientName = String.Empty;
                string _sPatientCode = String.Empty;
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
                bool _result = oBilling.ValidateExpandedClaimLimits(_MstTransactionId, _TransactionId, 5, _ClinicID, false, out InvalidType);
                if (_result == false)
                {
                   
                    MessageBox.Show("Claim could not be printed.\nClaim " + _sClaimNumber + " for patient " + _sPatientCode + " – " + _sPatientName + " has too many" + InvalidType.Replace("Some", "").Replace("some", "") + ".", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

                    Int64 _nMinimumDOS=0;

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
                                oHcfA1500.PrintForm("");
                                oBilling.ClaimNumber = _sClaimNumber;
                                oBilling.SetClaimNumbers();
                                oBilling.AutoBatchPrintedClaims(oBilling.MainClaimNumber, _MstTransactionId, _TransactionId, BatchBillingMethod.CMS1500New, _ContactID, _BusinessCenterCode);
                                oHcfA1500.markPrinted();
                                // AddPrintingnote(_MstTransactionId);
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupHCFA1500, gloAuditTrail.ActivityType.Print, "Paper CMS 02/12 printed and auto batched for the claim no " + _sClaimNumber, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
          
                            }
                            else if (_Result == DialogResult.No)
                            {
                                oHcfA1500.PrintForm("");
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupHCFA1500, gloAuditTrail.ActivityType.Print, "Paper CMS 02/12 printed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                            }
                        }
                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupHCFA1500, gloAuditTrail.ActivityType.Print, "Paper CMS 02/12 printed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
            }
            finally
            {
                if (_dtResult != null)
                {
                    _dtResult.Dispose();
                }
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

            oHcfA1500.IsPrintForm = false;
            gloCMSEDI.frmPrintCMS1500 ofrmPrintCMS1500 = new gloCMSEDI.frmPrintCMS1500(_databaseconnectionstring);
            if (ofrmPrintCMS1500.GetDefaultCoordinateUpdateSetting())
            {
                bool _IsAllowPrint = true;
                ofrmPrintCMS1500.ShowDialog(this);
                _IsAllowPrint = ofrmPrintCMS1500.IsAllowPrinting;
                if (ofrmPrintCMS1500 != null)
                    ofrmPrintCMS1500.Dispose();
                if (!_IsAllowPrint)
                    return;
            }

            string InvalidType = string.Empty;
            bool _result = oBilling.ValidateExpandedClaimLimits(_MstTransactionId, _TransactionId, 5, _ClinicID, false, out InvalidType);
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
                MessageBox.Show("Claim could not be printed.\nClaim " + _sClaimNumber + " for patient " + _sPatientCode + " - " + _sPatientName + " has too many" + InvalidType.Replace("Some", "").Replace("some", "") + ".", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        oHcfA1500.PrintData("");
                        oBilling.ClaimNumber = _sClaimNumber;
                        oBilling.SetClaimNumbers();
                        oBilling.AutoBatchPrintedClaims(oBilling.MainClaimNumber, _MstTransactionId, _TransactionId, BatchBillingMethod.CMS1500New, _ContactID, _BusinessCenterCode);
                        oHcfA1500.markPrinted();
                        //AddPrintingnote(_MstTransactionId);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupHCFA1500, gloAuditTrail.ActivityType.Print, "Paper CMS 02/12 printed and auto batched for the claim no " + _sClaimNumber, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    }

                    else if (_Result == DialogResult.No)
                    {
                        oHcfA1500.PrintData("");
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupHCFA1500, gloAuditTrail.ActivityType.Print, "Paper CMS 02/12 printed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
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
                if (oBilling!=null)
                {
                    oBilling.Dispose(); oBilling = null;
                }
            }
            return _result;
        }
        private void tsb_btnPrevious_Click(object sender, EventArgs e)
        {

        }

        private void tsb_btnNext_Click(object sender, EventArgs e)
        {

        }

        private void printdoc_HCFA1500_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void printdoc_HCFA1500_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
        }

        private void printdoc_HCFA1500_QueryPageSettings(object sender, System.Drawing.Printing.QueryPageSettingsEventArgs e)
        {

        }

        private void printdoc_HCFA1500_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }


        #endregion




        #region " Patient Strip "

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
                    try
                    {
                        oPatientStripControl.ControlSize_Changed -= new gloPatientStripControl.gloClaimSearchControl.ControlSizeChanged(oPatientStripControl_ControlSize_Changed);
                        oPatientStripControl.OnPatientSearchKeyPress -= new gloPatientStripControl.gloClaimSearchControl.PatientSearchKeyPressHandler(oPatientStripControl_OnPatientSearchKeyPress);
                        oPatientStripControl.PatientModified -= new gloPatientStripControl.gloClaimSearchControl.Patient_Modified(oPatientStripControl_Patient_Modify);

                    }
                    catch { }
                    oPatientStripControl.Dispose();
                    oPatientStripControl = null;
                }

                if (_bIsFromModifyChr)
                {
                    oPatientStripControl = new gloPatientStripControl.gloClaimSearchControl(_databaseconnectionstring, false, false, true);
                }
                else
                {
                    oPatientStripControl = new gloPatientStripControl.gloClaimSearchControl(_databaseconnectionstring, true, true, true);
                }

                oPatientStripControl.ControlSize_Changed += new gloPatientStripControl.gloClaimSearchControl.ControlSizeChanged(oPatientStripControl_ControlSize_Changed);
                oPatientStripControl.OnPatientSearchKeyPress += new gloPatientStripControl.gloClaimSearchControl.PatientSearchKeyPressHandler(oPatientStripControl_OnPatientSearchKeyPress);
                oPatientStripControl.PatientModified += new gloPatientStripControl.gloClaimSearchControl.Patient_Modified(oPatientStripControl_Patient_Modify);
                //oPatientStripControl.btnDownEnable = true;
                //oPatientStripControl.btnUpEnable = true;
                //oPatientStripControl.DTP.Visible = false;


                oPatientStripControl.FillDetails(_PatientID, 1, false);
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
            oHcfA1500.ClearForm(null);
            _PatientID = Convert.ToInt64(oPatientStripControl.PatientID);

            if (oHcfA1500 != null)
            {
                oHcfA1500.FillTransactionOnForm(_TransactionId, _MstTransactionId);
            }
        }
        void oPatientStripControl_OnPatientSearchKeyPress(object sender, KeyPressEventArgs e)
        {
            lblMessage.Text = "";
            if (_LoadPatientFromTransaction == false)
            {
                if (oPatientStripControl.PatientID > 0 && oPatientStripControl.TransactionID > 0)
                {
                    _PatientID = Convert.ToInt64(oPatientStripControl.PatientID);
                    _TransactionId = Convert.ToInt64(oPatientStripControl.TransactionID);
                    _MstTransactionId = Convert.ToInt64(oPatientStripControl.TransactionMasterID);
                    gloBilling oBilling = new gloBilling(_databaseconnectionstring, "");

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
                    if (UB04Setting == true)
                    {
                        if (Convert.ToInt16(BillingType.Professional) != oPatientStripControl.GetBillingType(_TransactionId, _MstTransactionId))
                        {
                            oPatientStripControl.ClearControl();
                            oHcfA1500.ClearForm(null);
                            MessageBox.Show("Select professional claim. ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tls_btnPrint.Enabled = false;
                            tls_btnPrintData.Enabled = false;
                            tsb_Modify.Enabled = false;
                            return;
                        }
                    }
                    if (oHcfA1500 != null)
                    {
                        oHcfA1500.FillTransactionOnForm(_TransactionId, _MstTransactionId);
                    }
                    // sandip dhakane 20100811
                    tls_btnPrint.Enabled = true;
                    tls_btnPrintData.Enabled = true;
                    tsb_Modify.Enabled = true;
                    lblMessage.Text = "";


                    string InvalidType = string.Empty;
                    bool _result = oBilling.ValidateExpandedClaimLimits(_MstTransactionId, _TransactionId, 5, _ClinicID, true, out InvalidType);
                    if (_result == false)
                    {
                        lblMessage.Text = "Warning – the entire claim cannot be displayed.  " + InvalidType + " are not visible.";
                    }
                    FetchVoidedInformation();

                    oBilling.Dispose();
                    // end
                }
                else
                {
                    //MaheshB 02162010
                    lblMessage.Text = "";
                    lblbottom.Text = "";
                    _PatientID = Convert.ToInt64(oPatientStripControl.PatientID);
                    oPatientStripControl.ClearControl();
                    oHcfA1500.ClearForm(null);

                    tls_btnPrevious.Enabled = false;
                    tls_btnNext.Enabled = true;
                    _TransactionIdCounter = -1;
                    //added by sandip dhakane for proper validation message for wrong claim
                    tls_btnPrint.Enabled = false;
                    tls_btnPrintData.Enabled = false;
                    tsb_Modify.Enabled = false;
                    tls_btnPrevious.Enabled = false;
                    tls_btnNext.Enabled = true;
                    MessageBox.Show("Claim selected is invalid or does not exist", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //end
                }
            }
        }

        void oPatientStripControl_ControlSize_Changed(object sender, EventArgs e)
        {

        }

        #endregion " Patient Strip "

        #region " Patient Settings "

        //Get Patient Setting
        private string GetPatientSetting(Int64 _PatientID, string _SettingName)
        {
            string _result = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = new DataTable();
            string _sqlQuery = "";
            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT ISNULL(sValue,'') as sValue FROM PatientSettings  WITH(NOLOCK) " +
                                        " WHERE nPatientID=" + _PatientID + " and sName='" + _SettingName + "'";
                oDB.Retrive_Query(_sqlQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    _result = Convert.ToString(dt.Rows[0]["sValue"]);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                dt = null;
                oDB.Dispose();
            }
            return _result;
        }

        #endregion

        #region " Form Button Click Events "
        private void tls_btnNext_Click(object sender, EventArgs e)
        {
            if (_TransactionIdCounter < _TransactionIds.Count)
            {
                _LoadPatientFromTransaction = true;
                _TransactionIdCounter = _TransactionIdCounter + 1;
                _TransactionId = Convert.ToInt64(_TransactionIds[_TransactionIdCounter].ToString());
                _MstTransactionId = Convert.ToInt64(_MstTransactionIds[_TransactionIdCounter].ToString());//MaheshB 02152010
                gloBilling oBilling = new gloBilling(_databaseconnectionstring, "");
                lblbottom.Text = (_TransactionIdCounter + 1).ToString() + " of " + _TransactionIds.Count.ToString();
                //20100316  Mahesh Nawal send the proper value on the GetTransactionsPatientID the function
                _PatientID = oBilling.GetTransactionsPatientID(_MstTransactionId, _ClinicID);

                //..Check if the Record is open if yes then dont allow save for 
                //transaction

                tsb_OK.Visible = false;
              //  bool _isTransactionOpen = false;
              //  string _recordMachineId = "";
              //  Int64 _recordUserId = 0;
                //_isTransactionOpen = oBilling.IsRecordOpen(_TransactionId, out _recordMachineId, out _recordUserId);
                //if (_isTransactionOpen) { SetViewMode(false); }
                //else { 
                SetViewMode(true);
                //}
                oPatientStripControl.FillDetails(_PatientID, 0, false);
                DataTable _dtResult = null;
                _dtResult = gloBilling.getPatientDetails(_TransactionId);
                if (_dtResult != null & _dtResult.Rows.Count > 0)
                {
                    oPatientStripControl.LblClaimNo = _dtResult.Rows[0]["sClaimNumber"].ToString();
                    _dtResult.Dispose();
                }
                /*if (objFrmBillingBatch_New != null)
                {
                    if (CallingTab.Equals("Queue"))
                    {
                        for (int rowindex = 0; rowindex < objFrmBillingBatch_New.QueuedClaims.Rows.Count; rowindex++)
                            if (objFrmBillingBatch_New.QueuedClaims.GetData(rowindex, objFrmBillingBatch_New.QueuedClaims.Cols["TransactionID"].Index).ToString().Equals(Convert.ToString(_TransactionId)))
                                objFrmBillingBatch_New.QueuedClaims.Row = rowindex;
                    }
                    else if (CallingTab.Equals("Batch"))
                    {
                        objFrmBillingBatch_New.BatchGridClaims.Row = objFrmBillingBatch_New.BatchGridClaims.Row + 1;
                    }

                } */

                oBilling.Dispose();
                //ClearForm();
                //ClearVariablesAndObjects();
                //oHcfA1500 = new gloCMSEDI.HCFA1500(_databaseconnectionstring);
                oHcfA1500.FillTransactionOnForm(_TransactionId, _MstTransactionId);
                _ContactID = oHcfA1500.ContactID;
                tls_btnNext.Enabled = true;

                if (_TransactionIdCounter == _TransactionIds.Count - 1)
                {
                    tls_btnPrevious.Enabled = true;
                    tls_btnNext.Enabled = false;
                }
                _LoadPatientFromTransaction = false;

                tsb_OK.Visible = false;
                if (_TransactionIdCounter == 0)
                    tls_btnPrevious.Enabled = false;
                else
                    tls_btnPrevious.Enabled = true;

                // sandip dhakane 20100811
                tls_btnPrint.Enabled = true;
                tls_btnPrintData.Enabled = true;
                tsb_Modify.Enabled = true;
                // end

                lblMessage.Text = "";

                string InvalidType = string.Empty;
                bool _result = oBilling.ValidateExpandedClaimLimits(_MstTransactionId, _TransactionId, 5, _ClinicID, true, out InvalidType);
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
                _LoadPatientFromTransaction = true;
                _TransactionIdCounter = _TransactionIdCounter - 1;
                _TransactionId = Convert.ToInt64(_TransactionIds[_TransactionIdCounter].ToString());
                _MstTransactionId = Convert.ToInt64(_MstTransactionIds[_TransactionIdCounter].ToString());
                gloBilling oBilling = new gloBilling(_databaseconnectionstring, "");
                //20100316  Mahesh Nawal send the proper value on the GetTransactionsPatientID the function
                _PatientID = oBilling.GetTransactionsPatientID(_MstTransactionId, _ClinicID);
                lblbottom.Text = (_TransactionIdCounter + 1).ToString() + " of " + _TransactionIds.Count.ToString();
                ////..Check if the Record is open if yes then dont allow save for 
                ////transaction

             //   bool _isTransactionOpen = false;
              //  string _recordMachineId = "";
              //  Int64 _recordUserId = 0;
                //_isTransactionOpen = oBilling.IsRecordOpen(_TransactionId, out _recordMachineId, out _recordUserId);
                //if (_isTransactionOpen)
                //{SetViewMode(false);}
                //else { 
                SetViewMode(true);
                //}
                /* if (objFrmBillingBatch_New != null)
                 {
                     if (CallingTab.Equals("Queue"))
                     {
                         for (int rowindex = 0; rowindex < objFrmBillingBatch_New.QueuedClaims.Rows.Count; rowindex++)
                             if (objFrmBillingBatch_New.QueuedClaims.GetData(rowindex, objFrmBillingBatch_New.QueuedClaims.Cols["TransactionID"].Index).ToString().Equals(Convert.ToString(_TransactionId)))
                                 objFrmBillingBatch_New.QueuedClaims.Row = rowindex;
                     }
                     else if (CallingTab.Equals("Batch"))
                     {
                         objFrmBillingBatch_New.BatchGridClaims.Row = objFrmBillingBatch_New.BatchGridClaims.Row - 1;
                     }

                 }*/
                //..end

                oPatientStripControl.FillDetails(_PatientID, 0, false);
                DataTable _dtResult = null;
                _dtResult = gloBilling.getPatientDetails(_TransactionId);
                if (_dtResult != null & _dtResult.Rows.Count > 0)
                {
                    oPatientStripControl.LblClaimNo = _dtResult.Rows[0]["sClaimNumber"].ToString();
                    _dtResult.Dispose();
                }
                oBilling.Dispose();
                //ClearForm();
                //ClearVariablesAndObjects();
                //oHcfA1500 = new gloCMSEDI.HCFA1500(_databaseconnectionstring);
                oHcfA1500.FillTransactionOnForm(_TransactionId, _MstTransactionId);
                _ContactID = oHcfA1500.ContactID;
                if (_TransactionIdCounter == 0)
                {
                    tls_btnPrevious.Enabled = false;
                    tls_btnNext.Enabled = true;
                }
                _LoadPatientFromTransaction = false;
                tls_btnNext.Enabled = true;
                tsb_OK.Visible = false;

                // sandip dhakane 20100811
                tls_btnPrint.Enabled = true;
                tls_btnPrintData.Enabled = true;
                tsb_Modify.Enabled = true;
                // end
                lblMessage.Text = "";

                string InvalidType = string.Empty;
                bool _result = oBilling.ValidateExpandedClaimLimits(_MstTransactionId, _TransactionId, 5, _ClinicID, true, out InvalidType);
                if (_result == false)
                {
                    lblMessage.Text = "Warning – the entire claim cannot be displayed.  " + InvalidType + " are not visible.";
                }
                FetchVoidedInformation();
            }
        }
      
        private void frmHCFA1500_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                #region " Update the Record Status "

                gloSettings.DatabaseSetting.DataBaseSetting obSettings = new gloSettings.DatabaseSetting.DataBaseSetting();

                obSettings.WriteSettings_XML("BATCH", "HeightofNewCMS1500", this.Height.ToString());

                //if (TransactionIDs != null && TransactionIDs.Count > 0)
                //{
                //    gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
                //    Int64 _trnId = 0;
                //    string _machName = "";
                //    Int64 _userId = 0;
                //    bool _IsOpen = false;

                //    for (int i = 0; i < TransactionIDs.Count; i++)
                //    {
                //        _trnId = 0;
                //        _trnId = Convert.ToInt64(TransactionIDs[i]);
                //        ogloBilling.UpdateRecordStatus(_trnId, 0, false);
                //    }
                //    if (ogloBilling != null) { ogloBilling.Dispose(); }
                //}


                //if (objFrmBillingBatch_New != null)
                //{
                   

                //    objFrmBillingBatch_New.SetView();
                //    if (CallingTab == "Queue")
                //        for (int index = 1; index < objFrmBillingBatch_New.QueuedClaims.Rows.Count; index++)
                //        {
                //            if (Convert.ToInt64(objFrmBillingBatch_New.QueuedClaims.GetData(index, objFrmBillingBatch_New.QueuedClaims.Cols["TransactionID"].Index)) == _TransactionId)
                //            {
                //                objFrmBillingBatch_New.QueuedClaims.Row = index;
                //                break;
                //            }
                //        }
                //    else if (CallingTab == "Batch")
                //        for (int index = 1; index < objFrmBillingBatch_New.BatchGridClaims.Rows.Count; index++)
                //        {
                //            if (Convert.ToInt64(objFrmBillingBatch_New.BatchGridClaims.GetData(index, objFrmBillingBatch_New.BatchGridClaims.Cols["TransactionID"].Index)) == _TransactionId)
                //            {
                //                objFrmBillingBatch_New.BatchGridClaims.Row = index;
                //                break;
                //            }
                //        }

                    
                //}


                LastAccessedTransaction = _TransactionId;
                #endregion

                if (_bIsFromModifyChr && frmModifyChr!=null)
                {
                    frmModifyChr.RefreshModifyCharges();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        //private void frmHCFA1500_Resize(object sender, EventArgs e)
        //{
        //    this.Width = 940;
        //}

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
                        ogloBilling.ShowModifyCharges(_PatientID, _TransactionId, this);
                        /* if (objFrmBillingBatch_New.IsClaimOnHold(_TransactionId))
                             objFrmBillingBatch_New.SelectedCharges.Remove(Convert.ToDecimal( _TransactionId));*/
                        //SetView();
                        //ogloBilling.Dispose();
                    }
                    else
                    {
                        DialogResult _dlgRst = DialogResult.None;
                        _dlgRst = MessageBox.Show("Transaction is already opened for modify on machine " + _recordMachineId + " \n Would you like to open this in View mode.", _messageboxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (_dlgRst == DialogResult.OK)
                        {
                            ogloBilling.ShowModifyCharges(_PatientID, _TransactionId, this);
                            if (objFrmBillingBatch_New.IsClaimOnHold(_TransactionId))
                                objFrmBillingBatch_New.SelectedCharges.Remove(Convert.ToDecimal(_TransactionId));
                 //           ogloBilling.Dispose();
                            // SetView();
                        }
                    }
                    if (ogloBilling != null)
                    {
                        ogloBilling.Dispose();
                        ogloBilling = null;
                    }
                    lblMessage.Text = "";


                    string InvalidType = string.Empty;
                    bool _result = ogloBilling.ValidateExpandedClaimLimits(_MstTransactionId, _TransactionId, 5, _ClinicID, true, out InvalidType);
                    if (_result == false)
                    {
                        lblMessage.Text = "Warning – the entire claim cannot be displayed.  " + InvalidType + " are not visible.";
                    }

                    FetchVoidedInformation();
                    oHcfA1500.FillTransactionOnForm(_TransactionId, _MstTransactionId);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
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
                            " FROM BL_Transaction_Claim_MST WITH(NOLOCK) LEFT   Outer JOIN " +
                            " User_MST WITH(NOLOCK) ON BL_Transaction_Claim_MST.nVoidUserID = User_MST.nUserID LEFT OUTER JOIN " +
                            " BL_ChargesTray  WITH(NOLOCK) ON BL_Transaction_Claim_MST.nVoidTrayID = BL_ChargesTray.nChargeTrayID " +
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

        private void tsb_Jump_Click(object sender, EventArgs e)
        {
            try
            {
                panel1.VerticalScroll.Visible = true;
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
                tls_btnPrint.Enabled = true;
                tls_btnPrintData.Enabled = true;
                tsb_Modify.Enabled = true;

                frmClaimSearch ofrmClaimSearch = new frmClaimSearch(ClaimList);
                ofrmClaimSearch.ShowDialog(this);
                if (ofrmClaimSearch.oDialogResult == true)
                {
                    lblMessage.Text = "";
                    if (ofrmClaimSearch.SelectedOption == frmClaimSearch.SelectedOptions.ClaimByPosition)
                    {
                        _TransactionIdCounter = Convert.ToInt16(ofrmClaimSearch.SelectedValue) - 1;
                        _TransactionId = Convert.ToInt64(_TransactionIds[_TransactionIdCounter].ToString());
                        _MstTransactionId = Convert.ToInt64(_MstTransactionIds[_TransactionIdCounter].ToString());
                        //frmHCFA1500_Load(sender, e);
                        Fill_Details();
                    }
                    else if (ofrmClaimSearch.SelectedOption == frmClaimSearch.SelectedOptions.ClaimByClaimNo)
                    {
                        _TransactionIdCounter = Convert.ToInt16(ClaimList.IndexOf(ofrmClaimSearch.SelectedValue));
                        _TransactionId = Convert.ToInt64(_TransactionIds[_TransactionIdCounter].ToString());
                        _MstTransactionId = Convert.ToInt64(_MstTransactionIds[_TransactionIdCounter].ToString());
                        //frmHCFA1500_Load(sender, e);
                        Fill_Details();
                    }
                    else if (ofrmClaimSearch.SelectedOption == frmClaimSearch.SelectedOptions.HighlightedClaim)
                    {
                        if (CallingTab == "Queue")
                        {
                            _TransactionId = Convert.ToInt64(objFrmBillingBatch_New.QueuedClaims.GetData(objFrmBillingBatch_New.QueuedClaims.RowSel, objFrmBillingBatch_New.QueuedClaims.Cols["TransactionID"].Index));
                            _MstTransactionId = Convert.ToInt64(objFrmBillingBatch_New.QueuedClaims.GetData(objFrmBillingBatch_New.QueuedClaims.RowSel, objFrmBillingBatch_New.QueuedClaims.Cols["TransactionMasterID"].Index));
                            if (UB04Setting == true)
                            {
                                if (objFrmBillingBatch_New.GetBillingType(_TransactionId, _MstTransactionId) != Convert.ToInt16(BillingType.Professional))
                                {
                                    lblMessage.Text = "";
                                    lblbottom.Text = "";
                                    oHcfA1500.ClearForm(null);
                                    oPatientStripControl.ClearControl();
                                    tls_btnPrevious.Enabled = false;
                                    tls_btnNext.Enabled = true;
                                    _TransactionIdCounter = -1;
                                    tls_btnPrint.Enabled = false;
                                    tls_btnPrintData.Enabled = false;
                                    tsb_Modify.Enabled = false;
                                    MessageBox.Show("Select professional claim(s). ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (ofrmClaimSearch != null)
                                    {
                                        ofrmClaimSearch.Dispose();
                                        ofrmClaimSearch = null;
                                    }
                                    return;
                                }
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
                        //frmHCFA1500_Load(sender, e);
                        Fill_Details();
                    }

                    FetchVoidedInformation();

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

        #endregion

        #region " Other Method "
        private void GetHoldMessage()
        {
            DataTable dtHold = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);
                oDB.Retrive_Query("Select isnull(bIsHold,0) as bIsHold from BL_Transaction_Claim_Mst  WITH(NOLOCK) where nTransactionID='" + _TransactionId + "' ", out dtHold);
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
                oHcfA1500.Refresh();
            }
        }
        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        { 
            //SLR : Is it indented for dummy statement? on 4/22/2014
            oHcfA1500.Refresh();
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
                        oNote.NoteDescription = @"Claim printed by """  + this.UserName + @""" on """ + DateTime.Now.ToString("MM/dd/yyyy hh:mm:tt")+ @""" on CMS1500 02/12.";
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





