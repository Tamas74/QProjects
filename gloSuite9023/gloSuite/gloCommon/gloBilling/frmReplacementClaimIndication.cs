using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using gloBilling.Common;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace gloBilling
{
    public partial class frmReplacementClaimIndication : Form
    {

        #region "Variable Declarations"


        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        
        private Int64 _TransactionID = 0;
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;

        private string _UserName = "";
        private string _strClaimRefNo = "";
        private string _messageBoxCaption = "";
        private string _databaseConnectionString = "";

        public bool oDialogResult = false;
        public Boolean _bIsVoid = false;
        private bool _IsOpenforModify_Flag = false;
        private Boolean _bIsReplacementClaim = false;

        public int width;
        ComboBox combo;

        public EOBPaymentSubType _NoteTypeEnum = EOBPaymentSubType.Charges_BillingNote;
                   
        public Common.ClaimBox19Note _oBox19Note = new global::gloBilling.Common.ClaimBox19Note();
        public Common.ClaimBox19Notes _oBox19Notes = null;

        private Int64 _IllnessDate;
        //private bool _IsValidDate = true;

        private string _strPWKReportTypeCode = string.Empty;
        private string _strPWKReportTransmissionCode = string.Empty;
        private string _strPWKAttachmentControlNumber = string.Empty;
        private string _strPWKMammogramCertNumber = string.Empty;
        private string _strIDENo = string.Empty;

        #endregion

        #region properties

        public Boolean IsOpenforModify_Flag
        {
            get { return _IsOpenforModify_Flag; }
            set { _IsOpenforModify_Flag = value; }
        }

        public String sClaimRefNo
        {
            get { return _strClaimRefNo; }
            set { _strClaimRefNo = value; }
        }

        public Boolean IsbCliamReplacement
        {
            get { return _bIsReplacementClaim; }
            set { _bIsReplacementClaim = value; }
        }

        public Boolean bIsVoid
        {
            get { return _bIsVoid; }
            set { _bIsVoid = value; }
        }

        public Int64 IllnessDate
        {
            get { return _IllnessDate; }
            set { _IllnessDate = value; }
        }

        public String sDelayReasonCode
        { get; set; }

        public String sServiceAuthExceptionCode
        { get; set; }

        public Boolean bIsRefProAsSupervisor
        { get; set; }

        public Boolean bEnableSupervisorOption
        { get; set; }

        public Boolean bIsUB
        { get; set; }

        public String sMedicaidRebumissionCode
        { get; set; }

        public Boolean bIsRebilled
        { get; set; }

        public string ClaimBox10dNote
        { get; set; }

        public Int64 PAccountID
        { get; set; }

        public Int64 AccountPatientID
        { get; set; }

        public Int64 PatientID
        { get; set; }

        public Int64 nReplacementByTransMasterID
        { get; set; }

        public Int64 nReplacingTransMasterID
        { get; set; }

        public string sReplacementByClaimNo
        { get; set; }

        public string sReplacingClaimNo
        { get; set; }

        public Boolean bShowReplacementClaim
        { get; set; }
        
        public Int64 LastSeenDate
        { get; set; }

        public Int64 MoreClaimData_UnableToWorkFrom
        { get; set; }

        public Int64 MoreClaimData_UnableToWorkTill
        { get; set; }

        public string sPWKReportTypeCode
        {
            get { return _strPWKReportTypeCode; }
            set { _strPWKReportTypeCode = value; }
        }

        public string sPWKReportTransmissionCode
        {
            get { return _strPWKReportTransmissionCode; }
            set { _strPWKReportTransmissionCode = value; }
        }

        public string sPWKAttachmentControlNumber
        {
            get { return _strPWKAttachmentControlNumber; }
            set { _strPWKAttachmentControlNumber = value; }
        }
        public string sMammogramCertNumber
        {
            get { return _strPWKMammogramCertNumber; }
            set { _strPWKMammogramCertNumber = value; }
        }
        public string sIDENo
        {
            get { return _strIDENo; }
            set { _strIDENo = value; }
        }


        // To show the supervising for existing claims having the option checked on more claim data If user Un-check the admin setting
        [System.ComponentModel.DefaultValue(false)]
        public Boolean bIsSupProvSavedForExClaims 
        { get; set; }
        
        #endregion

        #region Constructors

        public frmReplacementClaimIndication(String DatabaseConnectionString, Int64 ClinicID, Int64 TransactionID, Common.ClaimBox19Notes oBox19Notes, String strClaimRefNo, Boolean bIsReplacementClaim, Int64 IllnessDate)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
            _TransactionID = TransactionID;
            _ClinicID = ClinicID;
            _oBox19Notes = oBox19Notes;
            sClaimRefNo=strClaimRefNo;
            IsbCliamReplacement = bIsReplacementClaim;
            _IllnessDate = IllnessDate;
            
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

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion

            
            cmbDelayReasonCode.DrawMode = DrawMode.OwnerDrawFixed;
            cmbDelayReasonCode.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            cmbServiceAuthExcepCode.DrawMode = DrawMode.OwnerDrawFixed;
            cmbServiceAuthExcepCode.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            cmbMedicaidResubmissionCode.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMedicaidResubmissionCode.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
        }

        #endregion

        # region "Form Load"

        private void frmReplacementClaimIndication_Load(object sender, EventArgs e)
        {
            Cls_TabIndexSettings tabSettings = null;
            try
            {
                tabSettings = new Cls_TabIndexSettings(this);
                tabSettings.SetTabOrder(Cls_TabIndexSettings.TabScheme.AcrossFirst);
                if (_bIsVoid == true)
                    tsb_OK.Enabled = false;

                if (bEnableSupervisorOption || bIsSupProvSavedForExClaims)
                {
                    chkRefProvAsSupervisor.Visible = true;  

                }
                else
                {
                    chkRefProvAsSupervisor.Visible = false;  
                }

                FillNotes();
                FillDelayReasonCodes();
                FillServiceAuthExceptionCodes();
                FillMedicaidResibmissionCode();
                FillReportTypeAndTransmission();
                txtAttachmentControlNumber.Text = sPWKAttachmentControlNumber;
                txtFDAMammogramNo.Text = sMammogramCertNumber;
                txtIDE.Text = sIDENo;
                if (bIsUB == true)
                {
                    pnlUB.Visible = true;
                    pnlCMS.Visible = false;
                    //label1.TextAlign = ContentAlignment.BottomRight;
                    //label1.Text = "UB04 FL80 :";
                    //label1.TextAlign = ContentAlignment.BottomRight;
                }
                else
                {
                    pnlUB.Visible = false;
                    pnlCMS.Visible = true;
                }

                if (bIsVoid)
                {
                    GetClaimsOfPatient(cmbReplacedByClaim);
                    GetClaimsOfPatient(cmbReplacingClaim);
                    pnlReplacementClaim.Visible = true;
                    pnlReplacementClaim.Enabled = false;
                    this.Height = 655;
                }
                else if (bShowReplacementClaim)
                {
                    GetClaimsOfPatient(cmbReplacedByClaim);
                    GetClaimsOfPatient(cmbReplacingClaim);
                    pnlReplacementClaim.Visible = true;
                    this.Height = 655;
                }
                else
                {
                    pnlReplacementClaim.Visible = false;
                    this.Height = 590;
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
            }
            finally
            {
                if (tabSettings != null) { tabSettings = null; }
            }
        }

        

        #endregion

        #region " Tool Strip Events"

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            oDialogResult = false;
            this.Close();
        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean claimReplacement = false;
                Int64 nIllnessDate = 0;
                Int64 nLastSeenDate = 0;
                Int64 _nUnableToWorkFromDate = 0;
                Int64 _nUnableToWorkTillDate = 0;


                if (chkCorrRplcmntClaim.Checked)
                    claimReplacement = true;

                bIsRefProAsSupervisor = chkRefProvAsSupervisor.Checked;


                if (chkCorrRplcmntClaim.Checked && txtClaimRefNo.Text.Trim() == string.Empty)
                {
                    DialogResult _dialogResult;
                    _dialogResult = MessageBox.Show("Claim is marked as Corrected / Replacement Claim but has not Claim Remittance Ref #.\nStop to Review?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    if (_dialogResult == DialogResult.Yes)
                    {
                        return;
                    }
                }
                if (chkCorrRplcmntClaim.Checked && txtBox19ClaimNote.Text.Trim() == string.Empty && cmbMedicaidResubmissionCode.Text.Trim() == string.Empty)
                {
                    DialogResult _dialogResult;
                    if (pnlUB.Visible == false)
                    {
                        _dialogResult = MessageBox.Show("Claim is marked as Corrected / Replacement Claim but Box 19 and Medicaid resubmission code not updated.\nStop to Review?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        _dialogResult = MessageBox.Show("Claim is marked as Corrected / Replacement Claim but UB04 FL80 and Medicaid resubmission code not updated.\nStop to Review?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }

                    if (_dialogResult == DialogResult.Yes)
                    {
                        return;
                    }
                }
                if (chkCorrRplcmntClaim.Checked && txtBox19ClaimNote.Text.Trim() == string.Empty)
                {
                    DialogResult _dialogResult;
                    if (pnlUB.Visible == false)
                    {
                        _dialogResult = MessageBox.Show("Claim is marked as Corrected / Replacement Claim but Box 19 not updated.\nStop to Review?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        _dialogResult = MessageBox.Show("Claim is marked as Corrected / Replacement Claim but UB04 FL80 not updated.\nStop to Review?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }

                    if (_dialogResult == DialogResult.Yes)
                    {
                        return;
                    }
                }
                if (chkCorrRplcmntClaim.Checked && cmbMedicaidResubmissionCode.Text.Trim() == string.Empty)
                {
                    DialogResult _dialogResult;
                    _dialogResult = MessageBox.Show("Claim is marked as Corrected / Replacement Claim but Medicaid resubmission code not updated.\nStop to Review?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    if (_dialogResult == DialogResult.Yes)
                    {
                        return;
                    }
                }
                //if (!bIsRebilled && !chkCorrRplcmntClaim.Checked && txtClaimRefNo.Text.Trim() != string.Empty)
                //{
                //    DialogResult _dialogResult;
                //    _dialogResult = MessageBox.Show("Claim has Claim Remittance Ref # but has not been marked as a corrected claim.\nStop to Review?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                //    if (_dialogResult == DialogResult.Yes)
                //    {
                //        return;
                //    }
                //}

                //Illness Date validation
                mskIllnessDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskIllnessDate.Text != "")
                {
                    mskIllnessDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    //if ((gloDateMaster.gloDate.IsValidDate(mskIllnessDate.Text)) == false)
                    if ((gloDateMaster.gloDate.IsValid(mskIllnessDate.Text)) == false)
                    {
                        MessageBox.Show("Enter a valid Onset Same/Similar Illness Date. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        nIllnessDate = gloDateMaster.gloDate.DateAsNumber(mskIllnessDate.Text);
                    }
                }

                //--------x------

                //Last Seen Date validation
                mskLastSeenDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskLastSeenDate.Text != "")
                {
                    mskLastSeenDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    if ((gloDateMaster.gloDate.IsValid(mskLastSeenDate.Text)) == false)
                    {
                        MessageBox.Show("Enter a valid Last Seen Date. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        nLastSeenDate = gloDateMaster.gloDate.DateAsNumber(mskLastSeenDate.Text);
                    }
                }

                //--------x------


                //--------x------

                //Unable To Work From Date
                mskUnableFromDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskUnableFromDate.Text != "")
                {
                    mskUnableFromDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    if ((gloDateMaster.gloDate.IsValid(mskUnableFromDate.Text)) == false)
                    {
                        MessageBox.Show("Enter a valid Unable To Work From Date. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        _nUnableToWorkFromDate = gloDateMaster.gloDate.DateAsNumber(mskUnableFromDate.Text);
                    }
                }

                //--------x------

                //--------x------

                //Unable To Work Till Date
                mskUnableTillDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskUnableTillDate.Text != "")
                {
                    mskUnableTillDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    if ((gloDateMaster.gloDate.IsValid(mskUnableTillDate.Text)) == false)
                    {
                        MessageBox.Show("Enter a valid Unable To Work Till Date. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        _nUnableToWorkTillDate = gloDateMaster.gloDate.DateAsNumber(mskUnableTillDate.Text);
                    }
                }

                //--------x------

                if (_nUnableToWorkFromDate > 0 && _nUnableToWorkTillDate > 0)
                {
                    if (gloDateMaster.gloDate.DateAsDate(_nUnableToWorkFromDate) > gloDateMaster.gloDate.DateAsDate(_nUnableToWorkTillDate))
                    {
                        MessageBox.Show("Unable to Work From Date cannot be greater than Unable to Work Till Date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskUnableFromDate.Select();
                        mskUnableFromDate.Focus();
                        return;

                    }
                }

                #region "Validation for PWK Segment Element"
                bool bIsReportTypePresent = false, bIsReportTransmissionCodePresent = false, bIsAttachmentControlNoPresent = false;
                
                if (cmbReportTypeCode.SelectedValue != null && Convert.ToString(cmbReportTypeCode.SelectedValue) != "0")
                {
                    bIsReportTypePresent = true;
                }
                if (cmbReportTransmissionCode.SelectedValue != null && Convert.ToString(cmbReportTransmissionCode.SelectedValue) != "0")
                {
                    bIsReportTransmissionCodePresent = true;
                }
                if (txtAttachmentControlNumber.Text != "")
                {
                    bIsAttachmentControlNoPresent = true;
                }

                


                if (bIsReportTypePresent || bIsReportTransmissionCodePresent || bIsAttachmentControlNoPresent)
                {
                    if (bIsReportTypePresent == false)
                    {
                        MessageBox.Show("Select Report Type Code.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbReportTypeCode.Focus();
                        return;
                    }
                    else if (bIsReportTransmissionCodePresent == false)
                    {
                        MessageBox.Show("Select Report Transmission Code.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbReportTransmissionCode.Focus();
                        return;
                    }
                    else if (bIsAttachmentControlNoPresent == false)
                    {
                        MessageBox.Show("Enter Attachment Control Number.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAttachmentControlNumber.Focus();
                        return;
                    }
                    
                } 
                #endregion

                IsbCliamReplacement = claimReplacement;
                sClaimRefNo = txtClaimRefNo.Text.Trim();

                if (cmbReplacedByClaim.SelectedValue != null)
                {
                    nReplacementByTransMasterID = Convert.ToInt64(cmbReplacedByClaim.SelectedValue);
                    sReplacementByClaimNo = Convert.ToString(cmbReplacedByClaim.Text);
                }
                else
                {
                    nReplacementByTransMasterID = 0;
                    sReplacementByClaimNo = "";
                }

                if (cmbReplacingClaim.SelectedValue != null)
                {
                    nReplacingTransMasterID = Convert.ToInt64(cmbReplacingClaim.SelectedValue);
                    sReplacingClaimNo = Convert.ToString(cmbReplacingClaim.Text);
                }
                else
                {
                    nReplacingTransMasterID = 0;
                    sReplacingClaimNo = "";
                }

                AssignDataToObject(txtBox19ClaimNote.Text.Trim(), claimReplacement, nIllnessDate);
                ClaimBox10dNote = txt10dReservedForLocalUse.Text.Trim();

                _IllnessDate = nIllnessDate;
                LastSeenDate = nLastSeenDate;

                if (cmbDelayReasonCode.SelectedValue != null)
                {
                    if (Convert.ToString(cmbDelayReasonCode.SelectedValue) != "0")
                    {
                        sDelayReasonCode = Convert.ToString(cmbDelayReasonCode.SelectedValue);
                    }
                    else
                    {
                        sDelayReasonCode = "";
                    }
                }
                if (cmbServiceAuthExcepCode.SelectedValue != null)
                {
                    if (Convert.ToString(cmbServiceAuthExcepCode.SelectedValue) != "0")
                    {
                        sServiceAuthExceptionCode = Convert.ToString(cmbServiceAuthExcepCode.SelectedValue);
                    }
                    else
                    {
                        sServiceAuthExceptionCode = "";
                    }
                }
                if (cmbMedicaidResubmissionCode.SelectedValue != null)
                {
                    if (Convert.ToString(cmbMedicaidResubmissionCode.SelectedValue) != "0")
                    {
                        sMedicaidRebumissionCode = Convert.ToString(cmbMedicaidResubmissionCode.SelectedValue);
                    }
                    else
                    {
                        sMedicaidRebumissionCode = "";
                    }
                }

                MoreClaimData_UnableToWorkFrom = _nUnableToWorkFromDate;
                MoreClaimData_UnableToWorkTill = _nUnableToWorkTillDate;

                #region "PWK Segment Element"
                if (cmbReportTypeCode.SelectedValue != null)
                {
                    if (Convert.ToString(cmbReportTypeCode.SelectedValue) != "0")
                    {
                        sPWKReportTypeCode = Convert.ToString(cmbReportTypeCode.SelectedValue);
                    }
                    else
                    {
                        sPWKReportTypeCode = "";
                    }
                }

                if (cmbReportTransmissionCode.SelectedValue != null)
                {
                    if (Convert.ToString(cmbReportTransmissionCode.SelectedValue) != "0")
                    {
                        sPWKReportTransmissionCode = Convert.ToString(cmbReportTransmissionCode.SelectedValue);
                    }
                    else
                    {
                        sPWKReportTransmissionCode = "";
                    }
                }

                sPWKAttachmentControlNumber = Convert.ToString(txtAttachmentControlNumber.Text.Trim());
                sMammogramCertNumber=Convert.ToString(txtFDAMammogramNo.Text.Trim());
                sIDENo = Convert.ToString(txtIDE.Text.Trim());
                #endregion

                oDialogResult = true;
                this.Close();
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
            }
            finally
            {
                //this.mskUnableFromDate.Validating += new CancelEventHandler(mskDate_Validating);
                //this.mskUnableTillDate.Validating += new CancelEventHandler(mskDate_Validating);
            }
        }

        # endregion

        # region "Private And Public Methods"

        private void FillNotes()
        {
            
            string _NoteID = "0";
            try
            {
                #region " Fetch the Charge Notes form the Object and Bind it to the Grid "

                if (!_IsOpenforModify_Flag)
                {
                    if (Convert.ToString(_IllnessDate) != "" && _IllnessDate > 0)
                    {
                        mskIllnessDate.Text = Convert.ToString(gloDateMaster.gloDate.DateAsDateString(_IllnessDate));
                    }
                    if (Convert.ToString(LastSeenDate) != "" && LastSeenDate > 0)
                    {
                        mskLastSeenDate.Text = Convert.ToString(gloDateMaster.gloDate.DateAsDateString(LastSeenDate));
                    }
                    if (Convert.ToString(MoreClaimData_UnableToWorkFrom) != "" && MoreClaimData_UnableToWorkFrom > 0)
                    {
                        mskUnableFromDate.Text = Convert.ToString(gloDateMaster.gloDate.DateAsDateString(MoreClaimData_UnableToWorkFrom));
                    }
                    if (Convert.ToString(MoreClaimData_UnableToWorkTill) != "" && MoreClaimData_UnableToWorkTill > 0)
                    {
                        mskUnableTillDate.Text = Convert.ToString(gloDateMaster.gloDate.DateAsDateString(MoreClaimData_UnableToWorkTill));
                    }
                    if (_oBox19Notes != null && _oBox19Notes.Count > 0)
                    {

                        for (int i = _oBox19Notes.Count - 1; i >= 0; i--)
                        {
                            if (_oBox19Notes[i].BillingNoteType == EOBPaymentSubType.Claim_Box19Note)
                            {
                                _NoteID = _NoteID + "," + _oBox19Notes[i].NoteID;
                                _oBox19Note.TransactionID = _oBox19Notes[i].TransactionID; // Convert.ToInt64(dt.Rows[i]["nTransactionID"]);
                                _oBox19Note.NoteType = _oBox19Notes[i].NoteType; //NoteType.GeneralNote;
                                _oBox19Note.NoteID = _oBox19Notes[i].NoteID; //Convert.ToInt64(dt.Rows[i]["nNoteId"]);
                                _oBox19Note.NoteDate = _oBox19Notes[i].NoteDate; //Convert.ToInt64(dt.Rows[i]["nNoteDateTime"]);
                                _oBox19Note.UserID = _oBox19Notes[i].UserID; //Convert.ToInt64(dt.Rows[i]["nUserID"]);
                                _oBox19Note.Box19NoteDescription = _oBox19Notes[i].Box19NoteDescription; //Convert.ToString(dt.Rows[i]["sNoteDescription"])
                                _oBox19Note.ClinicID = _ClinicID;
                                _oBox19Note.BillingNoteType = _oBox19Notes[i].BillingNoteType;
                                //oBox19Notes.RemoveAt(i);
                                txtBox19ClaimNote.Text = _oBox19Note.Box19NoteDescription;
                            }
                      
                        }

                    }
                    _NoteID = "0";
                    txt10dReservedForLocalUse.Text = ClaimBox10dNote; 
                    txtClaimRefNo.Text = _strClaimRefNo;
                    chkCorrRplcmntClaim.Checked = _bIsReplacementClaim;
                    chkRefProvAsSupervisor.Checked = bIsRefProAsSupervisor;  
                }

                # endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
               
            }
               
        }

        //Method used to assign/Update the _oClaimHold object
        private void AssignDataToObject(string sReason,Boolean IsClaimReplacement,Int64 _IllnessDate)
        {
            try
            {
                
                _oBox19Note.TransactionID = _TransactionID; // Convert.ToInt64(dt.Rows[i]["nTransactionID"]);
                _oBox19Note.NoteType = NoteType.Box19_Note; //NoteType.GeneralNote;
                _oBox19Note.NoteDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString()); //Convert.ToInt64(dt.Rows[i]["nNoteDateTime"]);
                _oBox19Note.UserID = _UserID; //Convert.ToInt64(dt.Rows[i]["nUserID"]);
                _oBox19Note.Box19NoteDescription = sReason; //Convert.ToString(dt.Rows[i]["sNoteDescription"]);
                _oBox19Note.ClinicID = _ClinicID;
                _oBox19Note.UserName=_UserName;
                _oBox19Note.NoteRowID=1;
                _oBox19Note.BillingNoteType=EOBPaymentSubType.Claim_Box19Note;
                 _oBox19Note.NoteDateTime=DateTime.Now;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private bool IsValidDate(string DOB)
        {
            Int32 year = 0; Int32 month = 0; Int32 day = 0;
            string[] _Date = DOB.Split('/');
            if (_Date.Length == 3)
            {
                for (int i = 0; i < _Date.Length; i++)
                {
                    if (_Date[i].Trim() != "")
                    {
                        if (i == 0)
                        {
                            month = Convert.ToInt32(_Date[i]);
                        }
                        if (i == 1)
                        {
                            day = Convert.ToInt32(_Date[i]);
                        }
                        if (i == 2)
                        {
                            if (_Date[i].Trim().Length == 4)
                                year = Convert.ToInt32(_Date[i]);
                            else
                                return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }

                if (month > 12)
                {
                    return false;
                }
                if (day == 29)
                {
                    if (month == 2)
                    {
                        if (year % 4 == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }


            }
            else
            {
                return false;
            }
        }

        private void FillDelayReasonCodes()
        {
            try
            {
                DataTable dt;

                ReasonCodes oReasonCodes = new ReasonCodes(_databaseConnectionString);
                dt = oReasonCodes.GetDelayReasonCodes();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["sDelayReasonCode"] = 0;
                    dr["sDelayCodeDesc"] = "";
                    dt.Rows.InsertAt(dr, 0);
                    dt.AcceptChanges();

                    cmbDelayReasonCode.DataSource = dt;
                    cmbDelayReasonCode.ValueMember = dt.Columns["sDelayReasonCode"].ColumnName;
                    cmbDelayReasonCode.DisplayMember = dt.Columns["sDelayCodeDesc"].ColumnName;
                    cmbDelayReasonCode.Refresh();
                    if (sDelayReasonCode != "")
                    {
                        cmbDelayReasonCode.SelectedValue = sDelayReasonCode;
                    }
                    else
                    {
                        cmbDelayReasonCode.SelectedIndex = 0;
                    }
                 
                }

                dt = null;
                oReasonCodes.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void GetClaimsOfPatient(ComboBox oCombo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                DataTable dtReplaceClaim;
                oDB.Connect(false);

                string strQuery ="";

                if (oCombo.Name.Trim() == "cmbReplacedByClaim")
                {
                    strQuery = "SELECT DISTINCT nTransactionID, dbo.GetClaimNo(ISNULL(CONVERT(VARCHAR,nClaimNo),0),5) AS nClaimNo   FROM dbo.BL_Transaction_MST WHERE nPatientID= " + PatientID + " AND nTransactionID >0 ORDER BY nClaimNo";
                }
                else
                {
                    strQuery = "SELECT DISTINCT nTransactionID, dbo.GetClaimNo(ISNULL(CONVERT(VARCHAR,nClaimNo),0),5) AS nClaimNo   FROM BL_Transaction_MST WHERE ISNULL(bIsVoid,0) =1  AND nPatientID= " + PatientID + " AND nTransactionID >0 ORDER BY nClaimNo";
                }
                oDB.Retrive_Query(strQuery, out dtReplaceClaim);
                oDB.Disconnect();
             
                if (dtReplaceClaim != null && dtReplaceClaim.Rows.Count > 0)
                {
                    DataRow dr = dtReplaceClaim.NewRow();
                    dr["nTransactionID"] = 0;
                    dr["nClaimNo"] = "";
                    dtReplaceClaim.Rows.InsertAt(dr, 0);
                    dtReplaceClaim.AcceptChanges();

                    oCombo.DataSource = dtReplaceClaim;
                    oCombo.ValueMember = dtReplaceClaim.Columns["nTransactionID"].ColumnName;
                    oCombo.DisplayMember = dtReplaceClaim.Columns["nClaimNo"].ColumnName;
                    oCombo.Refresh();
                    if (oCombo.Name.Trim() == "cmbReplacedByClaim")
                    {
                        if (nReplacementByTransMasterID != 0)
                        {
                            oCombo.SelectedValue = nReplacementByTransMasterID;
                        }
                        else
                        {
                            oCombo.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        if (nReplacingTransMasterID != 0)
                        {
                            oCombo.SelectedValue = nReplacingTransMasterID;
                        }
                        else
                        {
                            oCombo.SelectedIndex = 0;
                        }
                    }
                }

                dtReplaceClaim = null;
                oDB.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private void FillMedicaidResibmissionCode()
        {
            try
            {
                DataTable dt=new DataTable();
                MedicaidResubmissionCode oMedicaidReasonCode = new MedicaidResubmissionCode(_databaseConnectionString);
                //ReasonCodes oReasonCodes = new ReasonCodes(_databaseConnectionString);
                dt = oMedicaidReasonCode.GetMedicaidReasonCodes();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["sResubmissionCode"] = 0;
                    dr["sMedicaidResubmissionCodeDesc"] = "";
                    dt.Rows.InsertAt(dr, 0);
                    dt.AcceptChanges();

                   // cmbDelayReasonCode.DataSource = dt;
                    cmbMedicaidResubmissionCode.DataSource = dt;
                    cmbMedicaidResubmissionCode.ValueMember = dt.Columns["sResubmissionCode"].ColumnName;
                    cmbMedicaidResubmissionCode.DisplayMember = dt.Columns["sMedicaidResubmissionCodeDesc"].ColumnName;
                    cmbMedicaidResubmissionCode.Refresh();
                    if (sMedicaidRebumissionCode != "")
                    {
                        cmbMedicaidResubmissionCode.SelectedValue = sMedicaidRebumissionCode;
                    }
                    else
                    {
                        cmbMedicaidResubmissionCode.SelectedIndex = 0;
                    }

                }

                dt = null;
                oMedicaidReasonCode.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void FillServiceAuthExceptionCodes()
        {
            gloBilling ogloBilling = new gloBilling(_databaseConnectionString, "");
            try
            {
                DataTable dt;
                dt = ogloBilling.GetServiceAuthExceptionCodes();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["sServiceAuthExceCode"] = 0;
                    dr["sServiceAuthExcepDesc"] = "";
                    dt.Rows.InsertAt(dr, 0);
                    dt.AcceptChanges();

                    cmbServiceAuthExcepCode.DataSource = dt;
                    cmbServiceAuthExcepCode.ValueMember = dt.Columns["sServiceAuthExceCode"].ColumnName;
                    cmbServiceAuthExcepCode.DisplayMember = dt.Columns["sServiceAuthExcepDesc"].ColumnName;
                    cmbServiceAuthExcepCode.Refresh();
                    if (sServiceAuthExceptionCode!=null && sServiceAuthExceptionCode.Trim() != "")
                    {
                        cmbServiceAuthExcepCode.SelectedValue = sServiceAuthExceptionCode;
                    }
                    else
                    {
                        cmbServiceAuthExcepCode.SelectedIndex = 0;
                    }

                }

                dt = null;
                ogloBilling.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                if(ogloBilling!=null)
                    ogloBilling.Dispose();
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
                        string txt = combo.GetItemText(combo.Items[e.Index]).ToString();


                        if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth - 20)
                        {
                            if (toolTip1.GetToolTip(combo) != txt)
                            {
                                this.toolTip1.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 180, e.Bounds.Bottom);
                            }
                        }
                        else
                        {
                            this.toolTip1.SetToolTip(combo, "");
                        }
                    }
                    else
                    {
                        this.toolTip1.Hide(combo);
                    }
                }
                else
                {
                    //this.tooltip_Billing.SetToolTip(combo,"");
                }
                e.DrawFocusRectangle();
            }
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
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

        private void FillReportTypeAndTransmission()
        {
            gloBilling ogloBilling = new gloBilling(_databaseConnectionString, "");
            try
            {
                DataSet ds;
                ds = ogloBilling.GetPWKReportType_TransmissionCodes();

                if (ds!=null && ds.Tables.Count>0)
                {
                    DataTable dtReportType = ds.Tables[0];
                    DataTable dtReportTransmission = ds.Tables[1];

                    if (dtReportType != null && dtReportType.Rows.Count > 0)
                    {
                        DataRow dr = dtReportType.NewRow();
                        dr["sCode"] = 0;
                        dr["sDefination"] = "";
                        dtReportType.Rows.InsertAt(dr, 0);
                        dtReportType.AcceptChanges();

                        cmbReportTypeCode.DataSource = dtReportType;
                        cmbReportTypeCode.ValueMember = dtReportType.Columns["sCode"].ColumnName;
                        cmbReportTypeCode.DisplayMember = dtReportType.Columns["sDefination"].ColumnName;
                        cmbReportTypeCode.Refresh();
                        if (sPWKReportTypeCode != null && sPWKReportTypeCode.Trim() != "")
                        {
                            cmbReportTypeCode.SelectedValue = sPWKReportTypeCode;
                        }
                        else
                        {
                            cmbReportTypeCode.SelectedIndex = 0;
                        }

                    }

                    dtReportType = null;

                    if (dtReportTransmission != null && dtReportTransmission.Rows.Count > 0)
                    {
                        DataRow dr = dtReportTransmission.NewRow();
                        dr["sCode"] = 0;
                        dr["sDefination"] = "";
                        dtReportTransmission.Rows.InsertAt(dr, 0);
                        dtReportTransmission.AcceptChanges();

                        cmbReportTransmissionCode.DataSource = dtReportTransmission;
                        cmbReportTransmissionCode.ValueMember = dtReportTransmission.Columns["sCode"].ColumnName;
                        cmbReportTransmissionCode.DisplayMember = dtReportTransmission.Columns["sDefination"].ColumnName;
                        cmbReportTransmissionCode.Refresh();
                        if (sPWKReportTransmissionCode != null && sPWKReportTransmissionCode.Trim() != "")
                        {
                            cmbReportTransmissionCode.SelectedValue = sPWKReportTransmissionCode;
                        }
                        else
                        {
                            cmbReportTransmissionCode.SelectedIndex = 0;
                        }

                    }

                    dtReportTransmission = null;
                    ogloBilling.Dispose();
                }
                ogloBilling.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                if (ogloBilling != null)
                    ogloBilling.Dispose();
            }
        }

        # endregion

        #region " Form Control Events "
        
        private void mskIllnessDate_MouseClick(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void mskIllnessDate_Validating(object sender, CancelEventArgs e)
        {

            try
            {
                mskIllnessDate.ValidatingType = typeof(System.DateTime);
                mskIllnessDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskIllnessDate.Text.Length > 0 && mskIllnessDate.MaskCompleted == false)
                {
                    MessageBox.Show("Enter a valid Onset Same/Similar Illness Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
                else
                {
                    if (mskIllnessDate.MaskCompleted == true)
                    {
                        try
                        {
                            mskIllnessDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                            //if (gloDateMaster.gloDate.IsValidDate(mskIllnessDate.Text))
                            if (gloDateMaster.gloDate.IsValid(mskIllnessDate.Text))
                            {
                                //if (Convert.ToDateTime(mskIllnessDate.Text).Date > DateTime.Now.Date)
                                //{
                                //    MessageBox.Show("Enter a valid Onset Same/Similar Illness Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    e.Cancel = true;
                                //}
                            }
                            else
                            {
                                MessageBox.Show("Enter a valid Onset Same/Similar Illness Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                            MessageBox.Show("Enter a valid Onset Same/Similar Illness Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void cmbDelayReasonCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDelayReasonCode.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbDelayReasonCode.Items[cmbDelayReasonCode.SelectedIndex])["sDelayCodeDesc"]), cmbDelayReasonCode) >= cmbDelayReasonCode.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbDelayReasonCode, Convert.ToString(((DataRowView)cmbDelayReasonCode.Items[cmbDelayReasonCode.SelectedIndex])["sDelayCodeDesc"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbDelayReasonCode, "");
                }
            }
        }

        private void cmbDelayReasonCode_MouseEnter(object sender, EventArgs e)
        {
            if (cmbDelayReasonCode.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbDelayReasonCode.Items[cmbDelayReasonCode.SelectedIndex])["sDelayCodeDesc"]), cmbDelayReasonCode) >= cmbDelayReasonCode.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbDelayReasonCode, Convert.ToString(((DataRowView)cmbDelayReasonCode.Items[cmbDelayReasonCode.SelectedIndex])["sDelayCodeDesc"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbDelayReasonCode, "");
                }
            }

        }

        private void mskLastSeenDate_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void mskLastSeenDate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                mskLastSeenDate.ValidatingType = typeof(System.DateTime);
                mskLastSeenDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskLastSeenDate.Text.Length > 0 && mskLastSeenDate.MaskCompleted == false)
                {
                    MessageBox.Show("Enter a valid Last Seen Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
                else
                {
                    if (mskLastSeenDate.MaskCompleted == true)
                    {
                        try
                        {
                            mskLastSeenDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                            if (gloDateMaster.gloDate.IsValid(mskLastSeenDate.Text))
                            {
                            }
                            else
                            {
                                MessageBox.Show("Enter a valid Last Seen Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                            MessageBox.Show("Enter a valid Last Seen Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        
        private void cmbServiceAuthExcepCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServiceAuthExcepCode.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbServiceAuthExcepCode.Items[cmbServiceAuthExcepCode.SelectedIndex])["sServiceAuthExcepDesc"]), cmbServiceAuthExcepCode) >= cmbServiceAuthExcepCode.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbServiceAuthExcepCode, Convert.ToString(((DataRowView)cmbServiceAuthExcepCode.Items[cmbServiceAuthExcepCode.SelectedIndex])["sServiceAuthExcepDesc"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbServiceAuthExcepCode, "");
                }
            }
        }

        private void cmbServiceAuthExcepCode_MouseEnter(object sender, EventArgs e)
        {
            if (cmbServiceAuthExcepCode.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbServiceAuthExcepCode.Items[cmbServiceAuthExcepCode.SelectedIndex])["sServiceAuthExcepDesc"]), cmbServiceAuthExcepCode) >= cmbServiceAuthExcepCode.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbServiceAuthExcepCode, Convert.ToString(((DataRowView)cmbServiceAuthExcepCode.Items[cmbServiceAuthExcepCode.SelectedIndex])["sServiceAuthExcepDesc"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbServiceAuthExcepCode, "");
                }
            }
        }

        private void cmbMedicaidResubmissionCode_MouseEnter(object sender, EventArgs e)
        {
            if (cmbMedicaidResubmissionCode.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbMedicaidResubmissionCode.Items[cmbMedicaidResubmissionCode.SelectedIndex])["sMedicaidResubmissionCodeDesc"]), cmbMedicaidResubmissionCode) >= cmbMedicaidResubmissionCode.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbMedicaidResubmissionCode, Convert.ToString(((DataRowView)cmbMedicaidResubmissionCode.Items[cmbMedicaidResubmissionCode.SelectedIndex])["sMedicaidResubmissionCodeDesc"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbMedicaidResubmissionCode, "");
                }
            }
        }

        private void txtClaimRefNo_TextChanged(object sender, EventArgs e)
        {
            if (txtClaimRefNo.Text.Trim() != string.Empty && !bIsRebilled)
            { chkCorrRplcmntClaim.Checked = true; }
            else if (bIsRebilled && chkCorrRplcmntClaim.Checked)
            { chkCorrRplcmntClaim.Checked = true; }
            else
            { chkCorrRplcmntClaim.Checked = false; }
        }

        private void mskUnableFromDate_MouseClick(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void mskUnableFromDate_Validating(object sender, CancelEventArgs e)
        {

            try
            {
                mskUnableFromDate.ValidatingType = typeof(System.DateTime);
                mskUnableFromDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskUnableFromDate.Text.Length > 0 && mskUnableFromDate.MaskCompleted == false)
                {
                    MessageBox.Show("Enter a valid Unable To Work From Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
                else
                {
                    if (mskUnableFromDate.MaskCompleted == true)
                    {
                        try
                        {
                            mskUnableFromDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                            //if (gloDateMaster.gloDate.IsValidDate(mskUnableFromDate.Text))
                            if (gloDateMaster.gloDate.IsValid(mskUnableFromDate.Text))
                            {
                                //if (Convert.ToDateTime(mskUnableFromDate.Text).Date > DateTime.Now.Date)
                                //{
                                //    MessageBox.Show("Enter a valid Onset Same/Similar Illness Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    e.Cancel = true;
                                //}
                            }
                            else
                            {
                                MessageBox.Show("Enter a valid Unable To Work From Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                            MessageBox.Show("Enter a valid Unable To Work From Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void mskUnableTillDate_MouseClick(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void mskUnableTillDate_Validating(object sender, CancelEventArgs e)
        {

            try
            {
                mskUnableTillDate.ValidatingType = typeof(System.DateTime);
                mskUnableTillDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskUnableTillDate.Text.Length > 0 && mskUnableTillDate.MaskCompleted == false)
                {
                    MessageBox.Show("Enter a valid Unable To Work Till Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
                else
                {
                    if (mskUnableTillDate.MaskCompleted == true)
                    {
                        try
                        {
                            mskUnableTillDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                            //if (gloDateMaster.gloDate.IsValidDate(mskUnableTillDate.Text))
                            if (gloDateMaster.gloDate.IsValid(mskUnableTillDate.Text))
                            {
                                //if (Convert.ToDateTime(mskUnableTillDate.Text).Date > DateTime.Now.Date)
                                //{
                                //    MessageBox.Show("Enter a valid Onset Same/Similar Illness Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    e.Cancel = true;
                                //}
                            }
                            else
                            {
                                MessageBox.Show("Enter a valid Unable To Work Till Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                            MessageBox.Show("Enter a valid Unable To Work Till Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void txtFDAMammogramNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
          private void txtIDE_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        #endregion
    }
}
