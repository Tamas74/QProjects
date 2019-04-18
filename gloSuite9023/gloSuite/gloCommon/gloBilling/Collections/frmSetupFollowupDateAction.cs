using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloPatient;

namespace gloBilling.Collections
{
    public partial class frmSetupFollowupDateAction : Form
    {
        #region "Class Variables"

        private DataTable dtActions = null;
        private DateTime dtCurrentDate;
        DataTable dtTVP = null;
        #endregion

        #region "Properties"

        public CollectionEnums.FollowUpType FollowUpActionType { get; set; }
        public Int64 PatientID { get; set; }
        public Int64 PAccountID { get; set; }
        public Int64 PatientAccountID { get; set; }
        public Int64 TransactionID { get; set; }
        public Int64 MstTransactionID { get; set; }
        public Int64 ScheduleID { get; set; }
        public Control CallingContainer { get; set; }
        public DateTime AccountFollowUpTimeStamp { get; set; }
        public DateTime AccountLogTimeStamp { get; set; }
        public DateTime ClaimFollowUpTimeStamp { get; set; }
        public DateTime ClaimLogTimeStamp { get; set; }
        public DateTime TFL_DFLDate { get; set; } // TFL and DFL Changes
        public String flgTFL_DFL { get; set; }// TFL and DFL Changes
        public String ClaimNumber { get; set; }
        public bool ShowClaimStatus { get; set; }
        private PatientDetails objPatientDetails = null;
        public bool bIsMultipleSelect = false;
        #endregion
       
        #region "Constructors"

        public frmSetupFollowupDateAction(CollectionEnums.FollowUpType enmType, Int64 nPatientID, Int64 nPAccountID, Int64 nTransactionID, Int64 MstTransactionID)
        {
            InitializeComponent();

            FollowUpActionType = enmType;
            PatientID = nPatientID;
            PAccountID = nPAccountID;
            TransactionID = nTransactionID;
            this.MstTransactionID = MstTransactionID;
        }

        public frmSetupFollowupDateAction(CollectionEnums.FollowUpType enmType, Int64 nPatientID, Int64 nPAccountID, Int64 nPatientAccountID)
        {
            InitializeComponent();

            FollowUpActionType = enmType;
            PatientID = nPatientID;
            PAccountID = nPAccountID;
            PatientAccountID = nPatientAccountID;
            TransactionID = 0;
        }

        public frmSetupFollowupDateAction(CollectionEnums.FollowUpType enmType, PatientDetails oPatientDetails, Int64 nAcctScheduleID)
        {
            InitializeComponent();
            FollowUpActionType = enmType;
            objPatientDetails = oPatientDetails;
            ScheduleID = nAcctScheduleID;
        }

        #endregion

        #region ""Form Load Event"

        private void frmSetupFollowupDateAction_Load(object sender, EventArgs e)
        {
            try
            {
                if (ClaimNumber == null)
                {
                    tsb_ClaimStatus.Visible = false;
                }
                tsb_ClaimStatus.Visible = ShowClaimStatus;
                dtCurrentDate = CL_FollowUpCode.GetServerDate();
                FillFollowUpInfo();
                FillComboBoxData();
                FillActionAndDefaultNextAction();
                if (bIsMultipleSelect)
                {
                    CreateDataTable();
                }

                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                tom.SetTabOrder(scheme);
                if (!bIsMultipleSelect)
                {
                    if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                    {
                        lblNoteCaption.Text = "Claim Note";
                        this.Icon = global::gloBilling.Properties.Resources.Insurance_Claim_Follow_up_date;

                        DataTable dtClaimFollowUpTimeStamp = null;
                        dtClaimFollowUpTimeStamp = CL_FollowUpCode.GetClaimFollowUpTimeStamp(TransactionID);
                        if (dtClaimFollowUpTimeStamp != null && dtClaimFollowUpTimeStamp.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtClaimFollowUpTimeStamp.Rows[0]["dtCreatedDateTime"]).Trim() != "")
                            {
                                ClaimFollowUpTimeStamp = Convert.ToDateTime(dtClaimFollowUpTimeStamp.Rows[0]["dtCreatedDateTime"]);
                            }
                            else
                            {
                                ClaimFollowUpTimeStamp = DateTime.MinValue;
                            }
                        }
                        DataTable dtClaimLogTimeStamp = null;
                        dtClaimLogTimeStamp = CL_FollowUpCode.GetClaimFollowUpLogTimeStamp(TransactionID);
                        if (dtClaimLogTimeStamp != null && dtClaimLogTimeStamp.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtClaimLogTimeStamp.Rows[0]["dtCreatedDateTime"]).Trim() != "")
                            {
                                ClaimLogTimeStamp = Convert.ToDateTime(dtClaimLogTimeStamp.Rows[0]["dtCreatedDateTime"]);
                            }
                            else
                            {
                                ClaimLogTimeStamp = DateTime.MinValue;
                            }
                        }

                        btnBrowseReasonCode.Visible = true;
                        btnBrowseRemarkCode.Visible = true;

                    }
                    else if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                    {
                        lblNoteCaption.Text = "Acct Note";
                        this.Icon = global::gloBilling.Properties.Resources.Patient_Follow_up_date;

                        DataTable dtAccountFollowUpTimeStamp = null;
                        dtAccountFollowUpTimeStamp = CL_FollowUpCode.GetAccountFollowUpTimeStamp(PAccountID);
                        if (dtAccountFollowUpTimeStamp != null && dtAccountFollowUpTimeStamp.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtAccountFollowUpTimeStamp.Rows[0]["dtCreatedDateTime"]).Trim() != "")
                            {
                                AccountFollowUpTimeStamp = Convert.ToDateTime(dtAccountFollowUpTimeStamp.Rows[0]["dtCreatedDateTime"]);
                            }
                            else
                            {
                                AccountFollowUpTimeStamp = DateTime.MinValue;
                            }
                        }
                        DataTable dtAccountLogTimeStamp = null;
                        dtAccountLogTimeStamp = CL_FollowUpCode.GetAccountFollowUpLogTimeStamp(PAccountID);
                        if (dtAccountLogTimeStamp != null && dtAccountLogTimeStamp.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtAccountLogTimeStamp.Rows[0]["dtCreatedDateTime"]).Trim() != "")
                            {
                                AccountLogTimeStamp = Convert.ToDateTime(dtAccountLogTimeStamp.Rows[0]["dtCreatedDateTime"]);
                            }
                            else
                            {
                                AccountLogTimeStamp = DateTime.MinValue;
                            }
                        }
                        btnBrowseReasonCode.Visible = false;
                        btnBrowseRemarkCode.Visible = false;
                    }
                    else if (FollowUpActionType == CollectionEnums.FollowUpType.BadDebt)
                    {
                        lblNoteCaption.Text = "Acct Note";
                        this.Icon = global::gloBilling.Properties.Resources.Patient_Follow_up_date;

                        DataTable dtAccountFollowUpTimeStamp = null;
                        dtAccountFollowUpTimeStamp = CL_FollowUpCode.GetBadDebtAccountFollowUpTimeStamp(PAccountID);
                        if (dtAccountFollowUpTimeStamp != null && dtAccountFollowUpTimeStamp.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtAccountFollowUpTimeStamp.Rows[0]["dtCreatedDateTime"]).Trim() != "")
                            {
                                AccountFollowUpTimeStamp = Convert.ToDateTime(dtAccountFollowUpTimeStamp.Rows[0]["dtCreatedDateTime"]);
                            }
                            else
                            {
                                AccountFollowUpTimeStamp = DateTime.MinValue;
                            }
                        }
                        DataTable dtAccountLogTimeStamp = null;
                        dtAccountLogTimeStamp = CL_FollowUpCode.GetBadDebtAccountFollowUpLogTimeStamp(PAccountID);
                        if (dtAccountLogTimeStamp != null && dtAccountLogTimeStamp.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtAccountLogTimeStamp.Rows[0]["dtCreatedDateTime"]).Trim() != "")
                            {
                                AccountLogTimeStamp = Convert.ToDateTime(dtAccountLogTimeStamp.Rows[0]["dtCreatedDateTime"]);
                            }
                            else
                            {
                                AccountLogTimeStamp = DateTime.MinValue;
                            }
                        }
                        btnBrowseReasonCode.Visible = false;
                        btnBrowseRemarkCode.Visible = false;
                    }
                }
                else
                { 
                    if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                    {
                        lblNoteCaption.Text = "Claim Note";
                        this.Icon = global::gloBilling.Properties.Resources.Insurance_Claim_Follow_up_date;

                        btnBrowseReasonCode.Visible = true;
                        btnBrowseRemarkCode.Visible = true;

                    }
                    else if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                    {
                        lblNoteCaption.Text = "Acct Note";
                        this.Icon = global::gloBilling.Properties.Resources.Patient_Follow_up_date;
                        
                        btnBrowseReasonCode.Visible = false;
                        btnBrowseRemarkCode.Visible = false;
                    }
                    else if (FollowUpActionType == CollectionEnums.FollowUpType.BadDebt)
                    {
                        lblNoteCaption.Text = "Acct Note";
                        this.Icon = global::gloBilling.Properties.Resources.Patient_Follow_up_date;

                        btnBrowseReasonCode.Visible = false;
                        btnBrowseRemarkCode.Visible = false;
                    }
                }
                if (CallingContainer != null && (CallingContainer.Name == "frmBillingModifyCharges" || CallingContainer.Name == "frmPatientFinancialViewV2" || CallingContainer.Name == "frmInsurancePaymentV2"))
                {
                    if (cmbLogAction.SelectedIndex <= 0)
                    {
                        chkScheduleFollowup.Checked = true;
                        cmbScheduleFollowup.Focus();
                        cmbScheduleFollowup.Select();
                    }
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }           
        } 

        #endregion

        #region "Public & Private Method"

        private void DefaultScheduleOnControldataChenged()
        {
            CL_FollowUpCode oCollection = new CL_FollowUpCode();

            try
            {
                var sDefaultAction = (from dt in dtActions.AsEnumerable()
                                      where dt.Field<String>("sFollowUpActionCode") == Convert.ToString(cmbLogAction.SelectedValue)
                                      select dt.Field<String>("sDefNextActionFollowUpCode")).ToArray().Single();

                var nDays = (from dt in dtActions.AsEnumerable()
                             where dt.Field<String>("sFollowUpActionCode") == Convert.ToString(cmbLogAction.SelectedValue)
                             select dt.Field<Int32?>("nFollowUpActionDays")).ToArray().Single();

                Int32 nDaysFinal = 0;
                Int32.TryParse(Convert.ToString(nDays), out nDaysFinal);

                cmbScheduleFollowup.SelectedValue = sDefaultAction;
                if (sDefaultAction != string.Empty)
                {
                    string sBaseDate = string.Empty;
                    mskLogActionDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                    if (mskLogActionDate.Text == string.Empty)
                    { sBaseDate = dtCurrentDate.ToString("MM/dd/yyyy"); }
                    else
                    {
                        mskLogActionDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        sBaseDate = mskLogActionDate.Text;
                    }

                    mskScheduleFollowupDate.Text = Convert.ToDateTime(sBaseDate).AddDays(nDaysFinal).ToString("MM/dd/yyyy");
                }
                else
                {
                    mskScheduleFollowupDate.Text = string.Empty;
                }

                mskScheduleFollowupDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskScheduleFollowupDate.Text == string.Empty && cmbScheduleFollowup.Text == string.Empty)
                {
                    chkScheduleFollowup.Checked = false;
                }
                else
                {
                    chkScheduleFollowup.Checked = true;
                }
                mskScheduleFollowupDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oCollection != null) { oCollection.Dispose(); }
                mskLogActionDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                mskScheduleFollowupDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            }
        }

        private void DefaultScheduleDateOnLogDateChanged()
        {
            try
            {
                if (Convert.ToString(cmbLogAction.SelectedValue) != string.Empty)
                {
                    var nDays = (from dt in dtActions.AsEnumerable()
                                 where dt.Field<String>("sFollowUpActionCode") == Convert.ToString(cmbLogAction.SelectedValue)
                                 select dt.Field<Int32?>("nFollowUpActionDays")).ToArray().Single();

                    Int32 nDaysFinal = 0;
                    Int32.TryParse(Convert.ToString(nDays), out nDaysFinal);


                    string sBaseDate = string.Empty;
                    mskLogActionDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                    if (mskLogActionDate.Text == string.Empty)
                    { sBaseDate = dtCurrentDate.ToString("MM/dd/yyyy"); }
                    else
                    {
                        mskLogActionDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        sBaseDate = mskLogActionDate.Text;
                    }

                    mskScheduleFollowupDate.Text = Convert.ToDateTime(sBaseDate).AddDays(nDaysFinal).ToString("MM/dd/yyyy");
                }
                else
                {
                    mskScheduleFollowupDate.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {              
                mskLogActionDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                mskScheduleFollowupDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            }
        }

        private void FillComboBoxData()
        {
            CL_FollowUpCode oCollection = new CL_FollowUpCode();

            try
            {
                this.cmbLogAction.SelectedIndexChanged -= new System.EventHandler(this.cmbLogAction_SelectedIndexChanged);
                if (ScheduleID != 0)
                {
                    dtActions = oCollection.getFollowUpActionWithCurrentSchedule(FollowUpActionType, ScheduleID);
                }
                else
                {
                    dtActions = oCollection.getFollowUpActionWithCurrentSchedule(FollowUpActionType,0);
                }

                if (dtActions != null && dtActions.Rows.Count > 0)
                {
                    DataRow row = dtActions.NewRow();
                    row["sFollowUpActionCode"] = "";
                    row["sFollowUpAction"] = "";
                    row["nFollowUpActionID"] = 0;
                    dtActions.Rows.InsertAt(row, 0);

                    cmbLogAction.DataSource = dtActions.Copy();
                    cmbLogAction.ValueMember = "sFollowUpActionCode";
                    cmbLogAction.DisplayMember = "sFollowUpAction";
                    //cmbLogAction.SelectedIndex = -1;
                }

                DataTable dtScheduleActions = oCollection.getFollowUpAction(FollowUpActionType);
                if (dtScheduleActions != null && dtScheduleActions.Rows.Count > 0)
                {
                    DataRow row = dtScheduleActions.NewRow();
                    row["sFollowUpActionCode"] = "";
                    row["sFollowUpAction"] = "";
                    row["nFollowUpActionID"] = 0;
                    dtScheduleActions.Rows.InsertAt(row, 0);

                    cmbScheduleFollowup.DataSource = dtScheduleActions;
                    cmbScheduleFollowup.ValueMember = "sFollowUpActionCode";
                    cmbScheduleFollowup.DisplayMember = "sFollowUpAction";
                    //cmbScheduleFollowup.SelectedIndex = -1;
                }

                chkLogAction_CheckedChanged(null, null);
                chkScheduleFollowup_CheckedChanged(null, null);

                cmbLogAction.DrawMode = DrawMode.OwnerDrawFixed;
                cmbLogAction.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
                cmbScheduleFollowup.DrawMode = DrawMode.OwnerDrawFixed;
                cmbScheduleFollowup.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oCollection != null) { oCollection.Dispose(); }
                this.cmbLogAction.SelectedIndexChanged += new System.EventHandler(this.cmbLogAction_SelectedIndexChanged);
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

        private void FillFollowUpInfo()
        {
            string sMessage = string.Empty;
            try
            {
                if (!bIsMultipleSelect)
                {
                    if (TransactionID != 0)
                    {
                        DataTable dtClaimDetails = CL_FollowUpCode.GetTransDetailsString(TransactionID);
                        if (dtClaimDetails != null && dtClaimDetails.Rows.Count > 0)
                        {
                            sMessage = "Follow-up for Claim # ";
                            sMessage += dtClaimDetails.Rows[0]["ClaimNumber"] == DBNull.Value ? string.Empty : dtClaimDetails.Rows[0]["ClaimNumber"].ToString() + " ";
                            sMessage += dtClaimDetails.Rows[0]["DOS"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dtClaimDetails.Rows[0]["DOS"]).ToString("MM/dd/yyyy") + " ";
                            sMessage += dtClaimDetails.Rows[0]["PatientName"] == DBNull.Value ? string.Empty : dtClaimDetails.Rows[0]["PatientName"].ToString() + " ";

                        }
                    }
                    else
                    {
                        DataTable dtAccDetails = gloAccount.GetAccountDetails(PAccountID, PatientID);
                        if (dtAccDetails != null && dtAccDetails.Rows.Count > 0)
                        {
                            sMessage = "Account: ";
                            sMessage += dtAccDetails.Rows[0]["sAccount"] == DBNull.Value ? string.Empty : dtAccDetails.Rows[0]["sAccount"].ToString();
                            sMessage += dtAccDetails.Rows[0]["sGuarantorName"] == DBNull.Value ? string.Empty : Convert.ToString(dtAccDetails.Rows[0]["sGuarantorName"]) == string.Empty ? string.Empty : "  -  " + dtAccDetails.Rows[0]["sGuarantorName"].ToString();
                            sMessage += dtAccDetails.Rows[0]["sAccountDesc"] == DBNull.Value ? string.Empty : Convert.ToString(dtAccDetails.Rows[0]["sAccountDesc"]) == string.Empty ? string.Empty : "  -  " + dtAccDetails.Rows[0]["sAccountDesc"].ToString();
                        }
                    }
                    lblInfo.Visible = true;
                    lblInfo.Text = sMessage; 
                }
                else
                {
                    lblInfo.Text = "";
                    lblInfo.Visible = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void FillActionAndDefaultNextAction()
        {
            DataTable _dt = new DataTable();
            string sMessage = string.Empty;
            try
            {
                if (ScheduleID != 0)
                {
                    _dt = CL_FollowUpCode.GetScheduledActionDetails(ScheduleID, FollowUpActionType);
                }
                else
                {
                    if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                    {
                        _dt = CL_FollowUpCode.GetScheduledAction(TransactionID, FollowUpActionType);
                    }
                    else if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                    {
                        _dt = CL_FollowUpCode.GetScheduledAction(PAccountID, FollowUpActionType);
                    }
                    else if (FollowUpActionType == CollectionEnums.FollowUpType.BadDebt)
                    {
                        _dt = CL_FollowUpCode.GetScheduledAction(PAccountID, FollowUpActionType);
                    }
                }

                if (_dt != null && _dt.Rows.Count > 0)
                {                   
                    if (Convert.ToDateTime(_dt.Rows[0]["dtFollowUpDate"]) <= dtCurrentDate)
                    {
                        chkLogAction.Checked = true;
                        cmbLogAction.SelectedValue = _dt.Rows[0]["sFollowupCode"];
                        mskLogActionDate.Text = dtCurrentDate.ToString("MM/dd/yyyy");

                        if (_dt.Rows[0]["sDefNextActionFollowUpCode"] != System.DBNull.Value && Convert.ToString(_dt.Rows[0]["sDefNextActionFollowUpCode"]) != string.Empty)
                        {
                            chkScheduleFollowup.Checked = true;
                            cmbScheduleFollowup.SelectedValue = _dt.Rows[0]["sDefNextActionFollowUpCode"];
                            int nDays = 0;
                            Int32.TryParse(Convert.ToString(_dt.Rows[0]["nFollowUpActionDays"]), out nDays);
                            mskScheduleFollowupDate.Text = dtCurrentDate.AddDays(nDays).ToString("MM/dd/yyyy");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }            
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
                Success = false;
            }
            return Success;
        }

        void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
        {

            ComboBox combo = (ComboBox)sender;
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

        private string ValidateFormData()
        {
            DialogResult _dlg = System.Windows.Forms.DialogResult.None;
            string sMessage = string.Empty;
            try
            {
                mskLogActionDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskScheduleFollowupDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                if (mskLogActionDate.Text.Trim() != string.Empty)
                {
                    mskLogActionDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    if (IsValidDate(mskLogActionDate.Text.Trim()) == false)
                    {
                        sMessage = "Please enter a valid date.  ";
                        mskLogActionDate.Focus();
                    }
                    else if (CL_FollowUpCode.IsScheduleDateToFarinFuture(mskLogActionDate.Text))
                    {
                        _dlg = MessageBox.Show("Are you sure you want to schedule this action that far in the future: " + mskLogActionDate.Text + " ?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (_dlg == DialogResult.Cancel)
                        {
                            sMessage = "FutureDate";
                            mskLogActionDate.Focus();
                            mskLogActionDate.Select();
                        } 
                    }
                }

                if (mskScheduleFollowupDate.Text.Trim() != string.Empty)
                {
                    mskScheduleFollowupDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    if (IsValidDate(mskScheduleFollowupDate.Text.Trim()) == false)
                    {
                        sMessage = "Please enter a valid date.  ";
                        mskScheduleFollowupDate.Focus();
                    }
                    else if (CL_FollowUpCode.IsScheduleDateToFarinFuture(mskScheduleFollowupDate.Text))
                    {
                        _dlg = MessageBox.Show("Are you sure you want to schedule this action that far in the future: " + mskScheduleFollowupDate.Text + " ?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                     
                        if (_dlg == DialogResult.Cancel)
                        {
                            sMessage = "FutureDate";
                            mskScheduleFollowupDate.Focus();
                            mskScheduleFollowupDate.Select();
                        }
                    }
                    // TFL and DFL Changes
                    if (sMessage != "FutureDate")
                    {

                        if (!bIsMultipleSelect)
                        {
                            DataTable dtTFL_DFL = null;
                            dtTFL_DFL = CL_FollowUpCode.getClaimTFL_DFL_Details(TransactionID);
                            if (dtTFL_DFL != null)
                            {
                                if (dtTFL_DFL.Rows.Count > 0)
                                {
                                    if (Convert.ToString(dtTFL_DFL.Rows[0]["TFLDays"]) != "")
                                    {
                                        TFL_DFLDate = Convert.ToDateTime(dtTFL_DFL.Rows[0]["TFL_DFL_DATE"]);
                                        flgTFL_DFL = "TFL";
                                    }
                                    else if (Convert.ToString(dtTFL_DFL.Rows[0]["DFLDays"]) != "")
                                    {
                                        TFL_DFLDate = Convert.ToDateTime(dtTFL_DFL.Rows[0]["TFL_DFL_DATE"]);
                                        flgTFL_DFL = "DFL";
                                    }
                                    else
                                    {
                                        flgTFL_DFL = "";
                                    }
                                }
                            }
                            if (dtTFL_DFL != null) { dtTFL_DFL.Dispose(); dtTFL_DFL = null; }
                        }
                    }
                    if (IsValidDate(mskScheduleFollowupDate.Text) && sMessage != "FutureDate")
                    {
                        if (!bIsMultipleSelect)
                        {
                            if (TFL_DFLDate < Convert.ToDateTime(mskScheduleFollowupDate.Text) && flgTFL_DFL == "TFL")
                            {
                                _dlg = MessageBox.Show("The scheduled follow up date is AFTER Claim Filing Limit.  Are you sure you want to defer this claim past its filing limit deadline?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                                if (_dlg == DialogResult.Cancel)
                                {
                                    sMessage = "The scheduled follow up date is AFTER Claim Filing Limit";
                                }
                            }
                            else if (TFL_DFLDate < Convert.ToDateTime(mskScheduleFollowupDate.Text) && flgTFL_DFL == "DFL")
                            {
                                _dlg = MessageBox.Show("The scheduled follow up date is AFTER Denial Filing Limit.  Are you sure you want to defer this claim past its denial filing limit deadline?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                                if (_dlg == DialogResult.Cancel)
                                {
                                    sMessage = "The scheduled follow up date is AFTER Denial Filing Limit";
                                }
                            }
                        }
                    }
                }

                if (sMessage == string.Empty)
                {
                    if (!chkLogAction.Checked && !chkScheduleFollowup.Checked && txtNotes.Text.Trim() == string.Empty)
                    {
                        sMessage = "Select an option.";
                    }
                    else if (chkLogAction.Checked && cmbLogAction.Text == string.Empty && mskLogActionDate.Text == string.Empty)
                    {
                        sMessage = "Please select an Action.";
                        cmbLogAction.Focus();
                    }
                    else if (chkScheduleFollowup.Checked && cmbScheduleFollowup.Text == string.Empty && mskScheduleFollowupDate.Text == string.Empty)
                    {
                        sMessage = "Please select an Action.";
                        cmbScheduleFollowup.Focus();
                    }
                }

                if (sMessage == string.Empty)
                {
                    mskLogActionDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    mskScheduleFollowupDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                    if (chkLogAction.Checked && (cmbLogAction.Text != string.Empty || mskLogActionDate.Text != string.Empty))
                    {
                        if (cmbLogAction.Text == string.Empty)
                        {
                            sMessage = "Please select an Action.";
                            cmbLogAction.Focus();
                        }
                        else if (mskLogActionDate.Text == string.Empty)
                        {
                            sMessage = "Please enter a Date.";
                            mskLogActionDate.Focus();
                        }
                    }

                    if (sMessage == string.Empty && chkScheduleFollowup.Checked && (cmbScheduleFollowup.Text != string.Empty || mskScheduleFollowupDate.Text != string.Empty))
                    {
                        if (cmbScheduleFollowup.Text == string.Empty)
                        {
                            sMessage = "Please select an Action.";
                            cmbScheduleFollowup.Focus();
                        }
                        else if (mskScheduleFollowupDate.Text == string.Empty)
                        {
                            sMessage = "Please enter a Date.";
                            mskScheduleFollowupDate.Focus();
                        }
                        //else if (TransactionID == 0 && PAccountID > 0 )
                        //{
                        //    if (CL_FollowUpCode.CheckBadDebtAccountFollowUp(PAccountID))
                        //    {
                        //        sMessage = "Account is in Bad Debt Follow-up Queue,you can not schedule Follow-up for this account.";
                        //    }
                        //}
                    }
                }
             
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                mskLogActionDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                mskScheduleFollowupDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            }

            return sMessage;
        }

        private bool SaveData()
        {
            bool bReturn = true;
            CL_FollowUpCode oCollection = new CL_FollowUpCode();
            Common.GeneralNote oNote = null;
            Common.GeneralNotes oNotes = null;
            DataTable dtAccountLog = new DataTable();
            DataTable dtAccountSchedule = new DataTable();
            DataTable dtClaimLog = new DataTable();
            DataTable dtClaimSchedule = new DataTable();
            Int64 nAuditLogID = 0;
            try
            {
                bool bHasWorked = false;
                if (bReturn && chkLogAction.Checked)
                {
                    var sActionLogDesc = (from dt in dtActions.AsEnumerable()
                                          where dt.Field<String>("sFollowUpActionCode") == Convert.ToString(cmbLogAction.SelectedValue)
                                          select dt.Field<String>("sFollowUpActionDescription")).ToArray().Single();
                    nAuditLogID = oCollection.GetUniqueID();
                    
                    DateTime dtScheduledFollowupDate=DateTime.MinValue;
                    if (IsValidDate(mskScheduleFollowupDate.Text.Trim()) == false)
                        dtScheduledFollowupDate = Convert.ToDateTime(DateTime.MinValue.ToString("MM/dd/yyyy"));
                    else
                        dtScheduledFollowupDate = Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim());

                    if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                    {
                        //bReturn = oCollection.SaveFollowUpLog(FollowUpActionType, TransactionID, Convert.ToDateTime(mskLogActionDate.Text.Trim()), Convert.ToString(cmbLogAction.SelectedValue), sActionLogDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, ClaimLogTimeStamp, ref bHasWorked);
                        //if (bReturn && !bHasWorked)
                        //{
                        //    ClaimFollowUpTimeStamp = DateTime.MinValue;
                        //}
                        if (!bIsMultipleSelect)
                        {
                            bReturn = oCollection.SaveFollowUpLog(FollowUpActionType, TransactionID, Convert.ToDateTime(mskLogActionDate.Text.Trim()), Convert.ToString(cmbLogAction.SelectedValue), sActionLogDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, ClaimLogTimeStamp, ref bHasWorked);
                        }
                        else
                        {
                            if (dtTVP!=null&&dtTVP.Rows.Count>0)
                            {
                                bReturn = oCollection.SaveFollowUpLog_Multiple(FollowUpActionType, Convert.ToDateTime(mskLogActionDate.Text.Trim()), Convert.ToString(cmbLogAction.SelectedValue), sActionLogDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, dtScheduledFollowupDate, dtTVP, out dtClaimLog, nAuditLogID);
                                
                                DataRow[] dr = dtClaimLog.Select("HasWorked='No'");
                                if (dr != null && dr.Length > 0)
                                {
                                    dtTVP = DeleteExtraColumns(dr.CopyToDataTable<DataRow>(), FollowUpActionType);
                                }
                                else
                                {
                                    dtTVP = null;
                                }
                            }
                        }
                    }
                    else if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                    {
                        if (!bIsMultipleSelect)
                        {
                            bReturn = oCollection.SaveFollowUpLog(FollowUpActionType, PAccountID, PatientID, Convert.ToDateTime(mskLogActionDate.Text.Trim()), Convert.ToString(cmbLogAction.SelectedValue), sActionLogDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, AccountLogTimeStamp, ref bHasWorked);
                        }
                        else
                        {
                            if (dtTVP!=null&&dtTVP.Rows.Count>0)
                            {
                                bReturn = oCollection.SaveFollowUpLog_Multiple(FollowUpActionType, Convert.ToDateTime(mskLogActionDate.Text.Trim()), Convert.ToString(cmbLogAction.SelectedValue), sActionLogDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, dtScheduledFollowupDate, dtTVP, out dtAccountLog, nAuditLogID);

                                DataRow[] dr = dtAccountLog.Select("HasWorked='No'");
                                if (dr != null && dr.Length > 0)
                                {
                                    dtTVP = DeleteExtraColumns(dr.CopyToDataTable<DataRow>(), FollowUpActionType);
                                }
                                else
                                {
                                    dtTVP = null;
                                }
                            }
                        }
                        //if (bReturn && !bHasWorked)
                        //{
                        //    AccountFollowUpTimeStamp = DateTime.MinValue;
                        //}
                    }
                    else if (FollowUpActionType == CollectionEnums.FollowUpType.BadDebt)
                    {
                        if (!bIsMultipleSelect)
                        {
                            bReturn = oCollection.SaveFollowUpLog(FollowUpActionType, PAccountID, PatientID, Convert.ToDateTime(mskLogActionDate.Text.Trim()), Convert.ToString(cmbLogAction.SelectedValue), sActionLogDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, AccountLogTimeStamp, ref bHasWorked);
                        }
                        else
                        {
                            if (dtTVP != null && dtTVP.Rows.Count > 0)
                            {
                                bReturn = oCollection.SaveFollowUpLog_Multiple(FollowUpActionType, Convert.ToDateTime(mskLogActionDate.Text.Trim()), Convert.ToString(cmbLogAction.SelectedValue), sActionLogDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim()), dtTVP, out dtAccountLog, nAuditLogID);

                                DataRow[] dr = dtAccountLog.Select("HasWorked='No'");
                                if (dr != null && dr.Length > 0)
                                {
                                    dtTVP = DeleteExtraColumns(dr.CopyToDataTable<DataRow>(), FollowUpActionType);
                                }
                                else
                                {
                                    dtTVP = null;
                                }
                            }
                        }
                    }
                }

                if (bReturn && chkScheduleFollowup.Checked && !bHasWorked)
                {
                    var sActionScheduleDesc = (from dt in dtActions.AsEnumerable()
                                               where dt.Field<String>("sFollowUpActionCode") == Convert.ToString(cmbScheduleFollowup.SelectedValue)
                                               select dt.Field<String>("sFollowUpActionDescription")).ToArray().Single();

                    if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                    {
                        //bReturn = oCollection.SaveFollowUpScedule(FollowUpActionType, TransactionID, Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim()), Convert.ToString(cmbScheduleFollowup.SelectedValue), sActionScheduleDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, ClaimFollowUpTimeStamp, ref bHasWorked);
                        if (!bIsMultipleSelect)
                        {
                            bReturn = oCollection.SaveFollowUpScedule(FollowUpActionType, TransactionID, Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim()), Convert.ToString(cmbScheduleFollowup.SelectedValue), sActionScheduleDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, ClaimFollowUpTimeStamp, ref bHasWorked);
                        }
                        else
                        {
                            if (dtTVP != null && dtTVP.Rows.Count > 0)
                            {
                                bReturn = oCollection.SaveFollowUpScedule_Multiple(FollowUpActionType, Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim()), Convert.ToString(cmbScheduleFollowup.SelectedValue), sActionScheduleDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, dtTVP, out dtClaimSchedule, nAuditLogID);

                                DataRow[] dr = dtClaimSchedule.Select("HasWorked='No'");

                                if (dr != null && dr.Length > 0)
                                {
                                    dtTVP = DeleteExtraColumns(dr.CopyToDataTable<DataRow>(),FollowUpActionType);
                                    
                                } 
                            }
                        }
                    }
                    else if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                    {
                        if (!bIsMultipleSelect)
                        {
                            bReturn = oCollection.SaveFollowUpScedule(FollowUpActionType, PAccountID, PatientID, Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim()), Convert.ToString(cmbScheduleFollowup.SelectedValue), sActionScheduleDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, AccountFollowUpTimeStamp, ref bHasWorked);
                        }
                        else
                        {
                            if (dtTVP!=null&&dtTVP.Rows.Count>0)
                            {
                                bReturn = oCollection.SaveFollowUpScedule_Multiple(FollowUpActionType, Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim()), Convert.ToString(cmbScheduleFollowup.SelectedValue), sActionScheduleDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, dtTVP, out dtAccountSchedule, nAuditLogID);
                                
                                DataRow[] dr = dtAccountSchedule.Select("HasWorked='No'");
                                if (dr != null && dr.Length > 0)
                                {
                                    dtTVP = DeleteExtraColumns(dr.CopyToDataTable<DataRow>(), FollowUpActionType);
                                }
                            }
                        }
                    }
                    else if (FollowUpActionType == CollectionEnums.FollowUpType.BadDebt)
                    {
                        if (!bIsMultipleSelect)
                        {
                            bReturn = oCollection.SaveFollowUpScedule(FollowUpActionType, PAccountID, PatientID, Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim()), Convert.ToString(cmbScheduleFollowup.SelectedValue), sActionScheduleDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, AccountFollowUpTimeStamp, ref bHasWorked);
                        }
                        else
                        {
                            if (dtTVP != null && dtTVP.Rows.Count > 0)
                            {
                                bReturn = oCollection.SaveFollowUpScedule_Multiple(FollowUpActionType, Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim()), Convert.ToString(cmbScheduleFollowup.SelectedValue), sActionScheduleDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, dtTVP, out dtAccountSchedule, nAuditLogID);

                                DataRow[] dr = dtAccountSchedule.Select("HasWorked='No'");
                                if (dr != null && dr.Length > 0)
                                {
                                    dtTVP = DeleteExtraColumns(dr.CopyToDataTable<DataRow>(), FollowUpActionType);
                                }
                            }
                        }
                    }
                }

                DataTable dtClaimNote = null;
                DataTable dtAccountNote = null;
                if (FollowUpActionType==CollectionEnums.FollowUpType.Claim)
                {
                    if (dtClaimLog != null && dtClaimLog.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtClaimSchedule.Rows.Count; i++)
                        {
                            DataRow[] drr = dtClaimLog.Select("nTransactionID='" + Convert.ToString(dtClaimSchedule.Rows[i]["nTransactionID"]) + "' And  HasWorked <> '" + Convert.ToString(dtClaimSchedule.Rows[i]["HasWorked"]) + "'");
                            if (drr != null)
                            {
                                for (int j = 0; j < drr.Length; j++)
                                {
                                    drr[j].Delete();
                                }

                            }
                            dtClaimLog.AcceptChanges();
                        }
                    }
                    dtClaimLog.Merge(dtClaimSchedule);
                    dtClaimNote = dtClaimLog.DefaultView.ToTable(true);
                }
                else if (FollowUpActionType==CollectionEnums.FollowUpType.PatientAccount)
                {
                    if (dtAccountLog != null && dtAccountLog.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtAccountSchedule.Rows.Count; i++)
                        {
                            DataRow[] drr = dtAccountLog.Select("nAccountID='" + Convert.ToString(dtAccountSchedule.Rows[i]["nAccountID"]) + "' And  HasWorked <> '" + Convert.ToString(dtAccountSchedule.Rows[i]["HasWorked"]) + "'");
                            if (drr != null)
                            {
                                for (int j = 0; j < drr.Length; j++)
                                {
                                    drr[j].Delete();
                                }

                            }
                            dtAccountLog.AcceptChanges();
                        }
                    }
                   

                    dtAccountLog.Merge(dtAccountSchedule);
                    dtAccountNote = dtAccountLog.DefaultView.ToTable(true);
                }
                else if (FollowUpActionType == CollectionEnums.FollowUpType.BadDebt)
                {
                    if (dtAccountLog != null && dtAccountLog.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtAccountSchedule.Rows.Count; i++)
                        {
                            DataRow[] drr = dtAccountLog.Select("nAccountID='" + Convert.ToString(dtAccountSchedule.Rows[i]["nAccountID"]) + "' And  HasWorked <> '" + Convert.ToString(dtAccountSchedule.Rows[i]["HasWorked"]) + "'");
                            if (drr != null)
                            {
                                for (int j = 0; j < drr.Length; j++)
                                {
                                    drr[j].Delete();
                                }

                            }
                            dtAccountLog.AcceptChanges();
                        }
                    }

                    dtAccountLog.Merge(dtAccountSchedule);
                    dtAccountNote = dtAccountLog.DefaultView.ToTable(true);
                }

                if (bReturn && txtNotes.Text.Trim() != string.Empty && !bHasWorked)
                {
                    if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                    {
                       
                        oNote = new global::gloBilling.Common.GeneralNote();
                        oNote.TransactionID = MstTransactionID;
                        oNote.NoteType = NoteType.Claim_Note;
                        oNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(dtpNoteDate.Value.ToShortDateString());
                        oNote.UserID = gloGlobal.gloPMGlobal.UserID;
                        oNote.StatementNoteDate = gloDateMaster.gloDate.DateAsNumber(dtpNoteDate.Value.ToShortDateString());
                        oNote.NoteDescription = txtNotes.Text.Trim();
                        oNote.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
                        oNotes = new global::gloBilling.Common.GeneralNotes();
                        oNotes.Add(oNote);

                        if (!bIsMultipleSelect)
                        {
                            bReturn = gloCharges.SaveClaimNotes(oNotes);
                        }
                        else
                        {
                            if (dtTVP != null && dtTVP.Rows.Count > 0)
                            { 
                                bReturn = gloCharges.SaveClaimNotes_Multiple(oNotes, dtTVP); 
                            }
                        }
                    }
                    else if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                    {
                        if (!bIsMultipleSelect)
                        {
                            gloAccount.SavePatAccNotes(0, gloDateMaster.gloDate.DateAsNumber(dtpNoteDate.Text.Trim()), txtNotes.Text.Trim(), PatientID, PatientAccountID, PAccountID);
                        }
                        else
                        {
                            if (dtTVP!=null&&dtTVP.Rows.Count>0)
                            {
                                gloAccount.SavePatAccNotes_Multiple(0, gloDateMaster.gloDate.DateAsNumber(dtpNoteDate.Text.Trim()), txtNotes.Text.Trim(), dtTVP);
                            }
                        }
                    }
                    else if (FollowUpActionType == CollectionEnums.FollowUpType.BadDebt)
                    {
                        if (!bIsMultipleSelect)
                        {
                            gloAccount.SavePatAccNotes(0, gloDateMaster.gloDate.DateAsNumber(dtpNoteDate.Text.Trim()), txtNotes.Text.Trim(), PatientID, PatientAccountID, PAccountID);
                        }
                        else
                        {
                            if (dtTVP != null && dtTVP.Rows.Count > 0)
                            {
                                gloAccount.SavePatAccNotes_Multiple(0, gloDateMaster.gloDate.DateAsNumber(dtpNoteDate.Text.Trim()), txtNotes.Text.Trim(), dtTVP);
                            }
                        }
                    }
                }
                if (bHasWorked)
                {
                    MessageBox.Show("Someone else has just worked this " + (FollowUpActionType == CollectionEnums.FollowUpType.Claim ? "Claim." : "Account."), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bReturn = false;
                }
                if (FollowUpActionType==CollectionEnums.FollowUpType.Claim)
                {
                    if (dtClaimNote!=null&&dtClaimNote.Rows.Count>0)
                    {
                        //DataRow[] dr = dtClaimNote.Select("HasWorked='Yes'");
                        //dtClaimNote = DeleteExtraColumns(dr.CopyToDataTable(), FollowUpActionType, "Status");
                        //DataRow[] dr = dtClaimNote.Select("HasWorked='Yes'");
                        dtClaimNote = DeleteExtraColumns(dtClaimNote, FollowUpActionType, "Status");
                        if (dtClaimNote.Rows.Count > 0)
                        {
                            frmBatchFollowUpStatus ofrmBatchFollowUpStatus = new frmBatchFollowUpStatus();
                            ofrmBatchFollowUpStatus.dtFollowUpStatus = dtClaimNote;
                            ofrmBatchFollowUpStatus.FollowUpAction = FollowUpActionType;
                            ofrmBatchFollowUpStatus.ShowDialog(this);
                            ofrmBatchFollowUpStatus.Dispose();
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else if (FollowUpActionType==CollectionEnums.FollowUpType.PatientAccount)
                {
                    if (dtAccountNote != null && dtAccountNote.Rows.Count > 0)
                    {
                       // DataRow[] dr = dtAccountNote.Select("HasWorked='Yes'");
                        dtAccountNote = DeleteExtraColumns(dtAccountNote, FollowUpActionType, "Status");
                        if (dtAccountNote.Rows.Count > 0)
                        {
                            frmBatchFollowUpStatus ofrmBatchFollowUpStatus = new frmBatchFollowUpStatus();
                            ofrmBatchFollowUpStatus.dtFollowUpStatus = dtAccountNote;
                            ofrmBatchFollowUpStatus.FollowUpAction = FollowUpActionType;
                            ofrmBatchFollowUpStatus.ShowDialog(this);
                            ofrmBatchFollowUpStatus.Dispose();
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else if (FollowUpActionType == CollectionEnums.FollowUpType.BadDebt)
                {
                    if (dtAccountNote != null && dtAccountNote.Rows.Count > 0)
                    {
                        // DataRow[] dr = dtAccountNote.Select("HasWorked='Yes'");
                        dtAccountNote = DeleteExtraColumns(dtAccountNote, FollowUpActionType, "Status");
                        if (dtAccountNote.Rows.Count > 0)
                        {
                            frmBatchFollowUpStatus ofrmBatchFollowUpStatus = new frmBatchFollowUpStatus();
                            ofrmBatchFollowUpStatus.dtFollowUpStatus = dtAccountNote;
                            ofrmBatchFollowUpStatus.FollowUpAction = FollowUpActionType;
                            ofrmBatchFollowUpStatus.ShowDialog(this);
                            ofrmBatchFollowUpStatus.Dispose();
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oCollection != null) { oCollection.Dispose(); }
                if (oNote != null) { oNote.Dispose(); }
                if (oNotes != null) { oNotes.Dispose(); }
            }
            return bReturn;
        }

        private DataTable DeleteExtraColumns(DataTable dtInput, CollectionEnums.FollowUpType FollowUpActionType,string CalledFrom="")
        {
            DataTable dt = new DataTable();
            dt=dtInput;
            if (CalledFrom != "" && CalledFrom == "Status")
            {
                dt.Columns.Remove("HasWorked");
                dt.Columns.Remove("nInsuranceID");
                dt.Columns.Remove("nContactID");
                dt.Columns.Remove("bTFL_DFL");
                dt.Columns.Remove("dtTFL_DFLTimeStamp");
                dt.Columns.Remove("dtFollowUpTimeStamp");
                dt.Columns.Remove("dtLogTimeStamp");
                dt.Columns.Remove("nMstTransactionID");
                dt.Columns.Remove("nTransactionID");
                dt.Columns.Remove("nPatientID");
                dt.Columns.Remove("nAccountID");
            }
            else
            {
                if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                {
                    dt.Columns.Remove("HasWorked");
                    dt.Columns.Remove("Description");
                    dt.Columns.Remove("Action");
                    dt.Columns.Remove("Scheduled");
                    dt.Columns.Remove("Claim#");
                    dt.Columns.Remove("Patient");
                    dt.Columns.Remove("Insurance Plan");
                }
                else if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                {
                    dt.Columns.Remove("HasWorked");
                    dt.Columns.Remove("Description");
                    dt.Columns.Remove("Action");
                    dt.Columns.Remove("Scheduled");
                    dt.Columns.Remove("Patient");
                    dt.Columns.Remove("Guarantor");
                    dt.Columns.Remove("Acct#");
                }
                else if (FollowUpActionType == CollectionEnums.FollowUpType.BadDebt)
                {
                    dt.Columns.Remove("HasWorked");
                    dt.Columns.Remove("Description");
                    dt.Columns.Remove("Action");
                    dt.Columns.Remove("Scheduled");
                    dt.Columns.Remove("Patient");
                    dt.Columns.Remove("Guarantor");
                    dt.Columns.Remove("Acct#");
                }
            }
            return dt;
        }

        
        private DataTable CreateDataTable()
        {
            if (objPatientDetails != null && objPatientDetails.Count > 0)
            {
                DataColumn dcAccountID = new DataColumn();
                dcAccountID.DataType = System.Type.GetType("System.Int64");
                dcAccountID.ColumnName = "nAccountID";
                dcAccountID.Caption = "nAccountID";

                DataColumn dcPatientID = new DataColumn();
                dcPatientID.DataType = System.Type.GetType("System.Int64");
                dcPatientID.ColumnName = "nPatientID";
                dcPatientID.Caption = "nPatientID";

                DataColumn dcTransactionID = new DataColumn();
                dcTransactionID.DataType = System.Type.GetType("System.Int64");
                dcTransactionID.ColumnName = "nTransactionID";
                dcTransactionID.Caption = "nTransactionID";

                DataColumn dcMstTransactionID = new DataColumn();
                dcMstTransactionID.DataType = System.Type.GetType("System.Int64");
                dcMstTransactionID.ColumnName = "nMstTransactionID";
                dcMstTransactionID.Caption = "nMstTransactionID";

                DataColumn dcLogTimeStamp = new DataColumn();
                dcLogTimeStamp.DataType = System.Type.GetType("System.DateTime");
                dcLogTimeStamp.ColumnName = "dtLogTimeStamp";
                dcLogTimeStamp.Caption = "dtLogTimeStamp";

                DataColumn dcFollowUpTimeStamp = new DataColumn();
                dcFollowUpTimeStamp.DataType = System.Type.GetType("System.DateTime");
                dcFollowUpTimeStamp.ColumnName = "dtFollowUpTimeStamp";
                dcFollowUpTimeStamp.Caption = "dtFollowUpTimeStamp";

                DataColumn dcdtTFL_DFLTimeStamp = new DataColumn();
                dcdtTFL_DFLTimeStamp.DataType = System.Type.GetType("System.DateTime");
                dcdtTFL_DFLTimeStamp.ColumnName = "dtTFL_DFLTimeStamp";
                dcdtTFL_DFLTimeStamp.Caption = "dtTFL_DFLTimeStamp";

                DataColumn dcTFL_DFL = new DataColumn();
                dcTFL_DFL.DataType = System.Type.GetType("System.String");
                dcTFL_DFL.ColumnName = "TFL_DFL";
                dcTFL_DFL.Caption = "TFL_DFL";

                DataColumn dcContactID = new DataColumn();
                dcContactID.DataType = System.Type.GetType("System.Int64");
                dcContactID.ColumnName = "nContactID";
                dcContactID.Caption = "nContactID";

                DataColumn dcInsuranceID = new DataColumn();
                dcInsuranceID.DataType = System.Type.GetType("System.Int64");
                dcInsuranceID.ColumnName = "nInsuranceID";
                dcInsuranceID.Caption = "nInsuranceID";

                dtTVP = new DataTable();
                dtTVP.Columns.Add(dcAccountID);
                dtTVP.Columns.Add(dcPatientID);
                dtTVP.Columns.Add(dcTransactionID);
                dtTVP.Columns.Add(dcMstTransactionID);
                dtTVP.Columns.Add(dcLogTimeStamp);
                dtTVP.Columns.Add(dcFollowUpTimeStamp);
                dtTVP.Columns.Add(dcdtTFL_DFLTimeStamp);
                dtTVP.Columns.Add(dcTFL_DFL);
                dtTVP.Columns.Add(dcContactID);
                dtTVP.Columns.Add(dcInsuranceID);

                for (int i = 0; i <= objPatientDetails.Count - 1; i++)
                {
                    dtTVP.Rows.Add();
                    dtTVP.Rows[i]["nAccountID"] = objPatientDetails[i].PatientAccountID;
                    dtTVP.Rows[i]["nPatientID"] = objPatientDetails[i].PatientID;
                    dtTVP.Rows[i]["nTransactionID"] = objPatientDetails[i].TransactionID;
                    dtTVP.Rows[i]["nMstTransactionID"] = objPatientDetails[i].MstTransactionID;
                    if (objPatientDetails[i].dtLogTimeStamp == DateTime.MinValue)
                        dtTVP.Rows[i]["dtLogTimeStamp"] = DBNull.Value;
                    else
                        dtTVP.Rows[i]["dtLogTimeStamp"] = Convert.ToDateTime(objPatientDetails[i].dtLogTimeStamp);

                    if (objPatientDetails[i].dtFollowUpTimeStamp == DateTime.MinValue)
                        dtTVP.Rows[i]["dtFollowUpTimeStamp"] = DBNull.Value;
                    else
                        dtTVP.Rows[i]["dtFollowUpTimeStamp"] = Convert.ToDateTime(objPatientDetails[i].dtFollowUpTimeStamp);

                    if (objPatientDetails[i].dtTFL_DFLTimeStamp == DateTime.MinValue)
                        dtTVP.Rows[i]["dtTFL_DFLTimeStamp"] = DBNull.Value;
                    else
                        dtTVP.Rows[i]["dtTFL_DFLTimeStamp"] = Convert.ToDateTime(objPatientDetails[i].dtTFL_DFLTimeStamp);

                    dtTVP.Rows[i]["TFL_DFL"] = Convert.ToString(objPatientDetails[i].TFL_DFL);
                    dtTVP.Rows[i]["nContactID"] = objPatientDetails[i].ContactID;
                    dtTVP.Rows[i]["nInsuranceID"] = objPatientDetails[i].InsuranceID;
                }
                dtTVP.AcceptChanges();
            }
            return dtTVP;
        }
        #endregion

        #region "Form Controls Event"

        private void mskLogActionDate_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            if (((MaskedTextBox)sender).Text.Trim() != "")
            {
                ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                if (IsValidDate(((MaskedTextBox)sender).Text.Trim()) == false)
                {
                    MessageBox.Show("Please enter a valid date.  ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
                else
                {
                    if (((MaskedTextBox)sender).Name == "mskLogActionDate") { DefaultScheduleDateOnLogDateChanged(); }   
                }
            }
            else
            {
                if (((MaskedTextBox)sender).Name == "mskLogActionDate") { DefaultScheduleDateOnLogDateChanged(); }           
                e.Cancel = false;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void chkLogAction_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLogAction.Checked)
            {
                cmbLogAction.Enabled = true;
                mskLogActionDate.Enabled = true;
            }
            else
            {
                cmbLogAction.Enabled = false;
                mskLogActionDate.Enabled = false;
            }
        }

        private void chkScheduleFollowup_CheckedChanged(object sender, EventArgs e)
        {
            if (chkScheduleFollowup.Checked)
            {
                cmbScheduleFollowup.Enabled = true;
                mskScheduleFollowupDate.Enabled = true;
            }
            else
            {
                cmbScheduleFollowup.Enabled = false;
                mskScheduleFollowupDate.Enabled = false;
            }
        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            string sMessage = ValidateFormData();
            if (sMessage != string.Empty)
            {
                // TFL and DFL Changes
                if (sMessage != "FutureDate" && sMessage != "The scheduled follow up date is AFTER Claim Filing Limit" && sMessage != "The scheduled follow up date is AFTER Denial Filing Limit")
                {
                    MessageBox.Show(sMessage, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                SaveData();
                this.Close();
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbScheduleFollowup_MouseEnter(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            if (combo.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)combo.Items[combo.SelectedIndex])["sFollowUpAction"]), combo) >= combo.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(combo, Convert.ToString(((DataRowView)combo.Items[combo.SelectedIndex])["sFollowUpAction"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(combo, "");
                }
            }
        }

        private void cmbLogAction_SelectedIndexChanged(object sender, EventArgs e)
        {          
            DefaultScheduleOnControldataChenged();           
        }

        private void mskLogActionDate_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        private void cmbLogAction_MouseEnter(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            if (combo.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)combo.Items[combo.SelectedIndex])["sFollowUpAction"]), combo) >= combo.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(combo, Convert.ToString(((DataRowView)combo.Items[combo.SelectedIndex])["sFollowUpAction"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(combo, "");
                }
            }
        } 

        #endregion          

        #region "Browse Quick Notes"
        private void btnBrowseNotes_Click(object sender, EventArgs e)
        {
            gloPatient.frmQuickNotes ofrmQuickNotes = null;
            try
            {
                if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                    ofrmQuickNotes = new gloPatient.frmQuickNotes(QuickNoteType.ClaimInternal.GetHashCode());
                else if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                    ofrmQuickNotes = new gloPatient.frmQuickNotes(QuickNoteType.AccountInternal.GetHashCode());

                if (ofrmQuickNotes != null)
                {
                    ofrmQuickNotes.ShowDialog(this);
                    if (txtNotes.Text != "")
                        txtNotes.Text = txtNotes.Text + " " + ofrmQuickNotes.Note;
                    else
                        txtNotes.Text = ofrmQuickNotes.Note;

                    const int MaxChars = 255;
                    if (txtNotes.Text.Length > MaxChars)
                        txtNotes.Text = txtNotes.Text.Substring(0, MaxChars);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (ofrmQuickNotes != null)
                {
                    ofrmQuickNotes.Dispose(); ofrmQuickNotes = null;
                }
            }

        }

        private void btnMouseHover(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongYellow;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

        private void btnBrowseReasonCode_Click(object sender, EventArgs e)
        {
            gloPatient.frmQuickNotes ofrmQuickNotes = null;
            try
            {

                ofrmQuickNotes = new gloPatient.frmQuickNotes(QuickNoteType.StatementReasonCode.GetHashCode());

                if (ofrmQuickNotes != null)
                {
                    ofrmQuickNotes.ShowDialog(this);
                    if (txtNotes.Text != "")
                        txtNotes.Text = txtNotes.Text + " " + ofrmQuickNotes.Note;
                    else
                        txtNotes.Text = ofrmQuickNotes.Note;

                    const int MaxChars = 255;
                    if (txtNotes.Text.Length > MaxChars)
                        txtNotes.Text = txtNotes.Text.Substring(0, MaxChars);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (ofrmQuickNotes != null)
                {
                    ofrmQuickNotes.Dispose(); ofrmQuickNotes = null;
                }
            }

        }

        private void btnBrowseRemarkCode_Click(object sender, EventArgs e)
        {
            gloPatient.frmQuickNotes ofrmQuickNotes = null;
            try
            {
                ofrmQuickNotes = new gloPatient.frmQuickNotes(QuickNoteType.StatementRemarkCode.GetHashCode());

                if (ofrmQuickNotes != null)
                {
                    ofrmQuickNotes.ShowDialog(this);
                    if (txtNotes.Text != "")
                        txtNotes.Text = txtNotes.Text + " " + ofrmQuickNotes.Note;
                    else
                        txtNotes.Text = ofrmQuickNotes.Note;

                    const int MaxChars = 255;
                    if (txtNotes.Text.Length > MaxChars)
                        txtNotes.Text = txtNotes.Text.Substring(0, MaxChars);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (ofrmQuickNotes != null)
                {
                    ofrmQuickNotes.Dispose(); ofrmQuickNotes = null;
                }
            }

        }

        private void tsb_ClaimStatus_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GetClaimStatus();
            this.Cursor = Cursors.Default;
        }

        private void GetContactInsuranceID(long nTransactionID, out long nContactID, out long nInsuranceID)
        {
            nContactID = 0; nInsuranceID = 0;
            DataTable dtContInsID = null;
            try
            {
                dtContInsID = CL_FollowUpCode.GetContactAndInsuranceDetails(nTransactionID);
                if (dtContInsID != null && dtContInsID.Rows.Count > 0)
                {
                    nContactID = Convert.ToInt64(dtContInsID.Rows[0]["nContactID"]);
                    nInsuranceID = Convert.ToInt64(dtContInsID.Rows[0]["nInsuranceID"]);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (dtContInsID != null) { dtContInsID.Dispose(); dtContInsID = null; }
            }

        }

        private void GetClaimStatus()
        {
            string ClaimNumber = "";
            long TrnMasterId = 0;
            string ResponseError = "";
            string StatusCategoryCode = "";
            string StatusCategoryCodeDesc = "";
            string StatusCode = "";
            string StatusCodeDesc = "";
            string StatusMessge = "";
            string PayerId = "";
            string PayerName = "";
            string RequestFilePath = "";
            string ResponseFilePath = "";

            List<gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo> claimStatusInfo = null;

            Int64 nContactID, nInsuranceID;
            GetContactInsuranceID(TransactionID, out nContactID, out nInsuranceID);

            TriArqEDIRealTimeClaimStatus.clsRealTimeClaimStatus oRTCS = new TriArqEDIRealTimeClaimStatus.clsRealTimeClaimStatus();
            var ClaimStatus = oRTCS.DoRealTimeCSI(ClaimNumber, gloGlobal.gloPMGlobal.DatabaseConnectionString,nContactID , gloGlobal.gloPMGlobal.ClinicID, TrnMasterId);
            if (ClaimStatus != null)
            {
                claimStatusInfo = new List<gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo>();
                ResponseError = ClaimStatus.ResponseError;
                StatusMessge = ClaimStatus.StatusMessge;
                StatusCategoryCode = ClaimStatus.StatusCategoryCode;
                StatusCategoryCodeDesc = ClaimStatus.StatusCategoryCodeDesc;
                StatusCode = ClaimStatus.StatusCode;
                StatusCodeDesc = ClaimStatus.StatusCodeDesc;
                PayerId = ClaimStatus.PayerId;
                PayerName = ClaimStatus.PayerName;
                RequestFilePath = ClaimStatus.RequestFilePath;
                ResponseFilePath = ClaimStatus.ResponseFilePath;
               
                try
                {
                    if (System.IO.File.Exists(RequestFilePath))
                    {
                        System.IO.File.Delete(RequestFilePath);
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                try
                {
                    if (System.IO.File.Exists(ResponseFilePath))
                    {
                        System.IO.File.Delete(ResponseFilePath);
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }

                if (ResponseError != "")
                {
                    claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, ResponseError, gloUIControlLibrary.Classes.ClaimStatus.MessageType.Error));
                    claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, "No Information.", gloUIControlLibrary.Classes.ClaimStatus.MessageType.StatusCategory));
                    claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, "No Information.", gloUIControlLibrary.Classes.ClaimStatus.MessageType.Status));
                }
                else
                {
                    bool noResult = false;
                    if (StatusMessge == "" && StatusCode == "")
                    {
                        if (StatusCategoryCode == "STC0" || StatusCategoryCode == "")
                        {
                            noResult = true;
                        }
                    }

                    if (noResult)
                    {
                        ResponseError = "Cannot process your request due to internal issues, please contact Gateway EDI Customer Service at 1-800-556-2231.";
                        claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, ResponseError, gloUIControlLibrary.Classes.ClaimStatus.MessageType.Error));
                        claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, "No Information.", gloUIControlLibrary.Classes.ClaimStatus.MessageType.StatusCategory));
                        claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, "No Information.", gloUIControlLibrary.Classes.ClaimStatus.MessageType.Status));
                    }
                    else
                    {
                        claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, StatusMessge, gloUIControlLibrary.Classes.ClaimStatus.MessageType.Message));
                        claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, StatusCategoryCode + " : " + StatusCategoryCodeDesc, gloUIControlLibrary.Classes.ClaimStatus.MessageType.StatusCategory));
                        claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, StatusCode + " : " + StatusCodeDesc, gloUIControlLibrary.Classes.ClaimStatus.MessageType.Status));
                    }
                }
                
                Boolean dlgResult = true;
                if (claimStatusInfo.Count > 0)
                {
                    gloUIControlLibrary.WPFForms.frmClaimStatus frmClaimStatus = new gloUIControlLibrary.WPFForms.frmClaimStatus(claimStatusInfo);
                    System.Windows.Interop.WindowInteropHelper _interophelper = new System.Windows.Interop.WindowInteropHelper(frmClaimStatus);
                    _interophelper.Owner = this.Handle;
                    frmClaimStatus.ShowDialog();
                    dlgResult = Convert.ToBoolean(frmClaimStatus.DialogResult);
                }
            }
        }




        //private void GetClaimStatus()
        //{
        //    string RequestString = "";
        //    string ResponseString = "";
        //    string ResponseError = "";
        //    string StatusCategoryCode = "";
        //    string StatusCategoryCodeDesc = "";
        //    string StatusCode = "";
        //    string StatusCodeDesc = "";
        //    string StatusEffectiveDate = null;
        //    string StatusMessge = "";
        //    string PayerId = "";
        //    string PayerName = "";
        //    List<gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo> claimStatusInfo = null;

        //    TriArqEDIRealTimeClaimStatus.clsRealTimeClaimStatus oRTCS = new TriArqEDIRealTimeClaimStatus.clsRealTimeClaimStatus();
        //    var ClaimStatus = oRTCS.DoRealTimeClaimStatusByClaim_RCM(ClaimNumber, gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //    if (ClaimStatus != null)
        //    {
        //        claimStatusInfo = new List<gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo>();

        //        RequestString = ClaimStatus.RequestString;
        //        ResponseString = ClaimStatus.ResponseString;
        //        ResponseError = ClaimStatus.ResponseError;
        //        StatusCategoryCode = ClaimStatus.StatusCategoryCode;
        //        StatusCategoryCodeDesc = ClaimStatus.StatusCategoryCodeDesc;
        //        StatusCode = ClaimStatus.StatusCode;
        //        StatusCodeDesc = ClaimStatus.StatusCodeDesc;
        //        StatusEffectiveDate = ClaimStatus.StatusEffectiveDate;
        //        StatusMessge = ClaimStatus.StatusMessge;
        //        PayerId = ClaimStatus.PayerId;
        //        PayerName = ClaimStatus.PayerName;


        //        if (ResponseError != "")
        //        {
        //            claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, ResponseError, gloUIControlLibrary.Classes.ClaimStatus.MessageType.Error));
        //        }
        //        else
        //        {
        //            bool noResult = false;
        //            if (StatusMessge == "" && StatusCode == "")
        //            {
        //                if (StatusCategoryCode == "STC0" || StatusCategoryCode == "")
        //                {
        //                    noResult = true;
        //                }
        //            }

        //            if (noResult)
        //            {
        //                ResponseError = "Cannot process your request due to internal issues, please contact Gateway EDI Customer Service at 1-800-556-2231.";
        //                claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, ResponseError, gloUIControlLibrary.Classes.ClaimStatus.MessageType.Error));
        //            }
        //            else
        //            {
        //                claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, StatusMessge, gloUIControlLibrary.Classes.ClaimStatus.MessageType.Message));
        //                claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, StatusCategoryCode + " : " + StatusCategoryCodeDesc, gloUIControlLibrary.Classes.ClaimStatus.MessageType.StatusCategory));
        //                claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, StatusCode + " : " + StatusCodeDesc, gloUIControlLibrary.Classes.ClaimStatus.MessageType.StatusCategory));
        //            }
        //        }



        //        //claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, ResponseError, gloUIControlLibrary.Classes.ClaimStatus.MessageType.Error));
        //        //StatusMessge = "72010 HCPCS Procedure Code is invalid in Professional Service. Value of sub-element SV101-02 is incorrect. Expected value is from external code list - HCPCS Code (130) when SV101-01='HC'. (72010) Rejected. Syntax error noted for this claim/service/inquiry. See Functional or Implementation Acknowledgement for details. (Note: Only for use to reject claims or status requests in transactions that were 'accepted with errors' on a 997 or 999 Acknowledgement.)";
        //        //StatusCategoryCode = "F3F";
        //        //StatusCategoryCodeDesc = "Finalized/Forwarded-The claim/encounter processing has been completed. Any applicable payment has been made and the claim/encounter has been forwarded to a subsequent entity as identified on the original claim or in this payer's records.";
        //        //StatusCode = "684";
        //        //StatusCodeDesc = "Rejected. Syntax error noted for this claim/service/inquiry. See Functional or Implementation Acknowledgement for details. (Note: Only for use to reject claims or status requests in transactions that were 'accepted with errors' on a 997 or 999 Acknowledgement.) Rejected. Syntax error noted for this claim/service/inquiry. See Functional or Implementation Acknowledgement for details. (Note: Only for use to reject claims or status requests in transactions that were 'accepted with errors' on a 997 or 999 Acknowledgement.) ";
        //        //claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, StatusMessge, gloUIControlLibrary.Classes.ClaimStatus.MessageType.Message));
        //        //claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, StatusCategoryCode + " : " + StatusCategoryCodeDesc, gloUIControlLibrary.Classes.ClaimStatus.MessageType.StatusCategory));
        //        //claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, StatusCode + " : " + StatusCodeDesc, gloUIControlLibrary.Classes.ClaimStatus.MessageType.Status));

        //        Boolean dlgResult = true;
        //        if (claimStatusInfo.Count > 0)
        //        {
        //            gloUIControlLibrary.WPFForms.frmClaimStatus frmClaimStatus = new gloUIControlLibrary.WPFForms.frmClaimStatus(claimStatusInfo);
        //            System.Windows.Interop.WindowInteropHelper _interophelper = new System.Windows.Interop.WindowInteropHelper(frmClaimStatus);
        //            _interophelper.Owner = this.Handle;
        //            frmClaimStatus.ShowDialog();
        //            dlgResult = Convert.ToBoolean(frmClaimStatus.DialogResult);
        //        }

        //        if (dlgResult)
        //        {
        //            AddRealTimeClaimStatus(MstTransactionID, ClaimNumber, RequestString, ResponseString, ResponseError, StatusCategoryCode, StatusCategoryCodeDesc, StatusCode, StatusCodeDesc, StatusEffectiveDate);
        //        }
        //    }
        //}

        public void AddRealTimeClaimStatus(Int64 TrnMasterID, string ClaimNumber, string RequestString, string ResponseString, string ResponseError,
                                           string StatusCategoryCode, string StatusCategoryCodeDesc, string StatusCode, string StatusCodeDesc, string StatusEffectiveDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = new object();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@StatusDate", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@TransactionMasterId", TrnMasterID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@ClaimNumber", ClaimNumber, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@StatusRequestString", RequestString, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@StatusResponseString", ResponseString, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@StatusError", ResponseError, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@ClaimStatusCategoryCode", StatusCategoryCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@ClaimStatusCatgoryDescription", StatusCategoryCodeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@ClaimStatusCode", StatusCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@ClaimStatusDescription", StatusCodeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@ClaimStatusEffectiveDate", StatusEffectiveDate, ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@LoginUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("BL_IN_RealTimeClaimStatus", oParameters, out _oResult);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

    }
}
