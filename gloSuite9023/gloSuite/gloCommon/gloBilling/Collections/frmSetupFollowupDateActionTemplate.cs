using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloPatient;
using gloOffice;
using gloWord;
namespace gloBilling.Collections
{
    public partial class frmSetupFollowupDateActionTemplate : Form
    {
        #region "Class Variables"

        private DataTable dtActions = null;
        DataTable dtScheduleActions = null;
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
        public Boolean bIsBatchGenerate { get; set; }
        private PatientDetails objPatientDetails=null;
        private Int64 nTemplateID = 0;
        public DateTime AccountFollowUpTimeStamp { get; set; }
        public DateTime AccountLogTimeStamp { get; set; }
        public DateTime ClaimFollowUpTimeStamp { get; set; }
        public DateTime ClaimLogTimeStamp { get; set; }

        #endregion
       
        #region "Constructors"

        public frmSetupFollowupDateActionTemplate(CollectionEnums.FollowUpType enmType, PatientDetails oPatientDetails, Int64 nAcctScheduleID)
        {
            InitializeComponent();
            FollowUpActionType = enmType;
            objPatientDetails = oPatientDetails;
            ScheduleID = nAcctScheduleID;
        }

        public frmSetupFollowupDateActionTemplate(CollectionEnums.FollowUpType enmType, Int64 nPatientID, Int64 nPAccountID, Int64 nTransactionID, Int64 MstTransactionID)
        {
            InitializeComponent();

            FollowUpActionType = enmType;
            PatientID = nPatientID;
            PAccountID = nPAccountID;
            TransactionID = nTransactionID;
            this.MstTransactionID = MstTransactionID;
        }

        public frmSetupFollowupDateActionTemplate(CollectionEnums.FollowUpType enmType, Int64 nPatientID, Int64 nPAccountID, Int64 nPatientAccountID)
        {
            InitializeComponent();
            FollowUpActionType = enmType;
            PatientID = nPatientID;
            PAccountID = nPAccountID;
            PatientAccountID = nPatientAccountID;
            TransactionID = 0;
        } 

        #endregion

        #region ""Form Load Event"

        private void frmSetupFollowupDateActionTemplate_Load(object sender, EventArgs e)
       {
            try
            {
                dtCurrentDate = CL_FollowUpCode.GetServerDate();
                FillFollowUpInfo();
                FillComboBoxData();
                FillActionAndDefaultNextAction();
                if (bIsBatchGenerate)
                {
                    CreateDataTable();
                }
                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                tom.SetTabOrder(scheme);

                if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                {
                    this.Text = "Generate Claim Template";
                    this.Icon = global::gloBilling.Properties.Resources.Generate_Claim_Template;
                    if (bIsBatchGenerate)
                    {
                        tsb_Generate.Visible = false;
                    }
                    else
                    {
                        tsb_Generate.Visible = true;
                    }
                }
                else if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                {
                    this.Text = "Generate Account Template";
                    this.Icon = global::gloBilling.Properties.Resources.Generate_Account_Template; 
                    if (bIsBatchGenerate)
                    {
                        tsb_Generate.Visible = false;
                    }
                    else
                    {
                        tsb_Generate.Visible = true;
                    }
                }

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

                if (CallingContainer != null && (CallingContainer.Name == "frmBillingModifyCharges" ))
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
                DataTable _dtDefTemplate = new DataTable();
                if (Convert.ToString(cmbLogAction.SelectedValue).Trim() != string.Empty)
                {
                    _dtDefTemplate = CL_FollowUpCode.GetDefaultAssociateTemplate(Convert.ToString(cmbLogAction.SelectedValue), FollowUpActionType);
                    if (_dtDefTemplate != null && _dtDefTemplate.Rows.Count > 0)
                    {
                        lblTemplate.Text = Convert.ToString(_dtDefTemplate.Rows[0]["stemplateNAME"]);
                        nTemplateID = Convert.ToInt64(_dtDefTemplate.Rows[0]["nTemplateID"]);
                        tsb_Generate.Enabled = true;
                        tsb_Print.Enabled = true;
                    }
                    else
                    {
                        lblTemplate.Text = "";
                        nTemplateID = 0;
                        tsb_Generate.Enabled = false;
                        tsb_Print.Enabled = false;
                    }
                }
                else
                {
                    lblTemplate.Text = "";
                    nTemplateID = 0;
                    tsb_Generate.Enabled = false;
                    tsb_Print.Enabled = false;
                }
                var sDefaultAction = (from dt in dtActions.AsEnumerable()
                                      where dt.Field<String>("sFollowUpActionCode") == Convert.ToString(cmbLogAction.SelectedValue)
                                      select dt.Field<String>("sDefNextActionFollowUpCode")).ToArray().SingleOrDefault();

                var nDays = (from dt in dtActions.AsEnumerable()
                             where dt.Field<String>("sFollowUpActionCode") == Convert.ToString(cmbLogAction.SelectedValue)
                             select dt.Field<Int32?>("nFollowUpActionDays")).ToArray().SingleOrDefault();

                Int32 nDaysFinal = 0;
                Int32.TryParse(Convert.ToString(nDays), out nDaysFinal);
                if (sDefaultAction != null)
                {
                    cmbScheduleFollowup.SelectedValue = sDefaultAction;
                    if (sDefaultAction != string.Empty)
                    {
                        mskScheduleFollowupDate.Text = dtCurrentDate.AddDays(nDaysFinal).ToString("MM/dd/yyyy");
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
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oCollection != null) { oCollection.Dispose(); }
                mskScheduleFollowupDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            }
        }

        private void FillComboBoxData()
        {
            CL_FollowUpCode oCollection = new CL_FollowUpCode();
            try
            {
                this.cmbLogAction.SelectedIndexChanged -= new System.EventHandler(this.cmbLogAction_SelectedIndexChanged);
                dtActions = oCollection.getFollowUpActionWithCurrentScheduleTemplate(FollowUpActionType);

                if (dtActions != null)
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

                dtScheduleActions  = oCollection.getFollowUpAction(FollowUpActionType);
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
                if (TransactionID != 0)
                {
                    if (!bIsBatchGenerate)
                    {
                        DataTable dtClaimDetails = CL_FollowUpCode.GetTransDetailsString(TransactionID);
                        if (dtClaimDetails != null && dtClaimDetails.Rows.Count > 0)
                        {
                            sMessage = "Follow-up for Claim # ";
                            sMessage += dtClaimDetails.Rows[0]["ClaimNumber"] == DBNull.Value ? string.Empty : dtClaimDetails.Rows[0]["ClaimNumber"].ToString() + " ";
                            sMessage += dtClaimDetails.Rows[0]["DOS"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dtClaimDetails.Rows[0]["DOS"]).ToString("MM/dd/yyyy") + " ";
                            sMessage += dtClaimDetails.Rows[0]["PatientName"] == DBNull.Value ? string.Empty : dtClaimDetails.Rows[0]["PatientName"].ToString() + " ";
                            lblInfo.Visible = true;
                            lblInfo.Text = sMessage;
                        }
                    }
                    else
                    {
                        lblInfo.Text = "";
                        lblInfo.Visible = false;
                    }
                }
                else
                {
                    if (!bIsBatchGenerate)
                    {
                        DataTable dtAccDetails = gloAccount.GetAccountDetails(PAccountID, PatientID);
                        if (dtAccDetails != null && dtAccDetails.Rows.Count > 0)
                        {
                            sMessage = "Account: ";
                            sMessage += dtAccDetails.Rows[0]["sAccount"] == DBNull.Value ? string.Empty : dtAccDetails.Rows[0]["sAccount"].ToString();
                            sMessage += dtAccDetails.Rows[0]["sGuarantorName"] == DBNull.Value ? string.Empty : Convert.ToString(dtAccDetails.Rows[0]["sGuarantorName"]) == string.Empty ? string.Empty : "  -  " + dtAccDetails.Rows[0]["sGuarantorName"].ToString();
                            sMessage += dtAccDetails.Rows[0]["sAccountDesc"] == DBNull.Value ? string.Empty : Convert.ToString(dtAccDetails.Rows[0]["sAccountDesc"]) == string.Empty ? string.Empty : "  -  " + dtAccDetails.Rows[0]["sAccountDesc"].ToString();
                            lblInfo.Visible = true;
                            lblInfo.Text = sMessage;
                        }
                    }
                    else
                    {
                        lblInfo.Text = "";
                        lblInfo.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillActionAndDefaultNextAction()
        {
            DataTable _dtSchedule = new DataTable();
            DataTable _dtDefTemplate = new DataTable();
            string sMessage = string.Empty;
            try
            {
                if (ScheduleID != 0)
                {
                    _dtSchedule = CL_FollowUpCode.GetScheduledActionDetails(ScheduleID, FollowUpActionType);
                }
                else
                {
                    if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                    {
                        _dtSchedule = CL_FollowUpCode.GetScheduledAction(TransactionID, FollowUpActionType);
                    }
                    else if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                    {
                        _dtSchedule = CL_FollowUpCode.GetScheduledAction(PAccountID, FollowUpActionType);
                    }
                }

                if (_dtSchedule != null && _dtSchedule.Rows.Count > 0)
                {
                    //if (Convert.ToDateTime(_dt.Rows[0]["dtFollowUpDate"]) <= dtCurrentDate)
                    //{
                    cmbLogAction.SelectedValue = _dtSchedule.Rows[0]["sFollowupCode"];
                        //If action not found in combobox
                        if (cmbLogAction.SelectedValue == null)
                        {
                            cmbLogAction.SelectedValue = "";
                        }
                        if (_dtSchedule.Rows[0]["sDefNextActionFollowUpCode"] != System.DBNull.Value && Convert.ToString(_dtSchedule.Rows[0]["sDefNextActionFollowUpCode"]) != string.Empty)
                        {
                            chkScheduleFollowup.Checked = true;
                            cmbScheduleFollowup.SelectedValue = _dtSchedule.Rows[0]["sDefNextActionFollowUpCode"];
                            int nDays = 0;
                            Int32.TryParse(Convert.ToString(_dtSchedule.Rows[0]["nFollowUpActionDays"]), out nDays);
                            mskScheduleFollowupDate.Text = dtCurrentDate.AddDays(nDays).ToString("MM/dd/yyyy");
                        }
                    //}
                    if (Convert.ToString(cmbLogAction.SelectedValue).Trim() != string.Empty)
                    {
                        _dtDefTemplate = CL_FollowUpCode.GetDefaultAssociateTemplate(Convert.ToString(cmbLogAction.SelectedValue), FollowUpActionType);
                        if (_dtDefTemplate != null && _dtDefTemplate.Rows.Count > 0)
                        {
                            lblTemplate.Text = Convert.ToString(_dtDefTemplate.Rows[0]["stemplateNAME"]);
                            nTemplateID = Convert.ToInt64(_dtDefTemplate.Rows[0]["nTemplateID"]);
                            tsb_Generate.Enabled = true;
                            tsb_Print.Enabled = true;
                        }
                        else
                        {
                            lblTemplate.Text = "";
                            nTemplateID = 0;
                            tsb_Generate.Enabled = false;
                            tsb_Print.Enabled = false;
                        }
                    }
                }
                else
                {
                    lblTemplate.Text = "";
                    nTemplateID = 0;
                    tsb_Generate.Enabled = false;
                    tsb_Print.Enabled = false;
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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

        private Boolean GenerateClaimReceipt()
        {
            Boolean _bResult = false;
            gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                if (nTemplateID != 0)
                {
                    if (TransactionID > 0)
                    {
                        ogloTemplate.TemplateID = nTemplateID;
                        ogloTemplate.PrimeryID = nTemplateID;
                        ogloTemplate.PatientID = PatientID;
                        ogloTemplate.TransactionID = TransactionID;
                        ogloTemplate.TransactionMstID = MstTransactionID;
                        ogloTemplate.nPAccountID = PAccountID;
                        ogloTemplate.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
                        gloOffice.frmWd_PatientTemplate frm = new gloOffice.frmWd_PatientTemplate(gloGlobal.gloPMGlobal.DatabaseConnectionString, ogloTemplate);
                        frm._AccountID = PAccountID;
                        //frm.IsView = true;
                        frm.WindowState = FormWindowState.Maximized;
                        frm._isReadOnlyView = true;
                        frm.ShowDialog(this);
                        if (frm.FormResult == DialogResult.OK)
                        {
                            _bResult= true;
                        }
                        frm.Dispose();
                        frm = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return _bResult;
        }

        private Boolean GenerateAccountReceipt()
        {
            Boolean _bResult = false;
            gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                if (PAccountID > 0)
                {
                    if (nTemplateID != 0)
                    {
                        ogloTemplate.TemplateID = nTemplateID;
                        ogloTemplate.PrimeryID = nTemplateID;
                        ogloTemplate.PatientID = PatientID;
                        ogloTemplate.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
                        gloOffice.frmWd_PatientTemplate frm = new gloOffice.frmWd_PatientTemplate(gloGlobal.gloPMGlobal.DatabaseConnectionString, ogloTemplate);
                        frm._AccountID = PAccountID;
                        //frm.IsView = true;
                        frm.WindowState = FormWindowState.Maximized;
                        frm._isReadOnlyView = true;
                        frm.ShowDialog(this);
                        if (frm.FormResult == DialogResult.OK)
                        {
                            _bResult = true;
                        }
                        frm.Dispose();
                        frm = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return _bResult;
        }

       // bool blnBatchPrintinProgress = false;
        private bool CheckBatchPrintProcessRunning()
        {

            try
            {


                foreach (Form oFrm in System.Windows.Forms.Application.OpenForms)
                {

                    if (oFrm.Name == "frmgloPrintSetupDateActionTemplate")
                    {
                        DialogResult dg = MessageBox.Show("Background printing is in progress. Do you want to cancel the printing?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if ((dg == DialogResult.Yes))
                        {
                            oFrm.Close();
                         //   blnBatchPrintinProgress = false;
                            return false;
                          //  break; // TODO: might not be correct. Was : Exit For
                        }
                        else
                        {
                            oFrm.Visible = true;
                            return true;
                           // break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }
                return false;
            }
            catch //(Exception ex)
            {
                //ex = null;
                return false;
            }
        }

        private static System.Drawing.Printing.PrinterSettings myPrinterSetting = new System.Drawing.Printing.PrinterSettings();

        private void PrintReceipt(string templateName)
        {
            if (CheckBatchPrintProcessRunning() == false)
            {
                // if (blnBatchPrintinProgress == false)
                // {
                //    Int64 _TransactionID = 0;
                string OldPrinterName = "";
                if (nTemplateID != 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    //frmWd_PatientTemplate ofrm = null;
                    gloOffice.gloTemplate _gloTemplate = null;
                    //Changes for the case GLO2010-0007587
                    List<gloOffice.gloTemplate> gloTemplates = new List<gloOffice.gloTemplate>();
                    DataTable dtAllPatientWithMultipleAccount = new DataTable();
                    try
                    {

                        //int count = 0;
                        //count = ((objPatientDetails.Count) * (7));
                        //pnl_Prgsbar.Visible = true;
                        //prgBar_Print.Visible = true;
                        //prgBar_Print.Maximum = count;
                        //prgBar_Print.Minimum = 0;
                        //prgBar_Print.Step = 1;
                        //  prgBar_Print.Value = 0;

                        //gloOffice.Supporting.AppointmentID = 0;
                        gloOffice.Supporting.DataBaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
                        gloOffice.Supporting.PatientID = PatientID;
                        if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                        {
                            gloOffice.Supporting.FieldID2 = 0;
                            gloOffice.Supporting.FieldID3 = 0;
                        }
                        else
                        {
                            gloOffice.Supporting.FieldID2 = TransactionID;
                            gloOffice.Supporting.FieldID3 = MstTransactionID;
                        }
                        gloOffice.Supporting.PrimaryID = Convert.ToInt64(nTemplateID);
                        String fileName = gloOffice.Supporting.GenerateDocumentFile();

                        if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                        {
                            if (bIsBatchGenerate)
                            {
                                if (objPatientDetails.Count > 0)
                                {
                                    for (int i = 0; i <= objPatientDetails.Count - 1; i++)
                                    {
                                        _gloTemplate = new gloOffice.gloTemplate(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                        _gloTemplate.AppointmentID = 0;
                                        _gloTemplate.TemplateID = Convert.ToInt64(nTemplateID);  //Convert.ToInt64(oTemplateNode.Tag);
                                        _gloTemplate.TemplateName = templateName;  //oTemplateNode.Text;
                                        _gloTemplate.PrimeryID = nTemplateID;//  Convert.ToInt64(c1ChkInPatients.Rows[i][COL_DTLAPPOINTMENTID]); // Convert.ToInt64(oTemplateNode.Tag);
                                        _gloTemplate.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
                                        _gloTemplate.DocumentCategory = 0;
                                        _gloTemplate.PatientID = objPatientDetails[i].PatientID;
                                        _gloTemplate.nPAccountID = objPatientDetails[i].PatientAccountID;
                                        _gloTemplate.IsTemplateContainsPatientAccountFields = true;//Template.IsContainsPatientAccountFields
                                        _gloTemplate.TemplateFilePath = fileName;
                                        gloTemplates.Add(_gloTemplate);
                                    }
                                }
                            }
                            else
                            {
                                _gloTemplate = new gloOffice.gloTemplate(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                _gloTemplate.AppointmentID = 0;
                                _gloTemplate.TemplateID = Convert.ToInt64(nTemplateID);  //Convert.ToInt64(oTemplateNode.Tag);
                                _gloTemplate.TemplateName = templateName;  //oTemplateNode.Text;
                                _gloTemplate.PrimeryID = nTemplateID;//  Convert.ToInt64(c1ChkInPatients.Rows[i][COL_DTLAPPOINTMENTID]); // Convert.ToInt64(oTemplateNode.Tag);
                                _gloTemplate.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
                                _gloTemplate.DocumentCategory = 0;
                                _gloTemplate.PatientID = PatientID;
                                _gloTemplate.nPAccountID = PAccountID;
                                _gloTemplate.IsTemplateContainsPatientAccountFields = true;//Template.IsContainsPatientAccountFields
                                _gloTemplate.TemplateFilePath = fileName;
                                gloTemplates.Add(_gloTemplate);
                            }
                        }
                        else
                        {
                            if (bIsBatchGenerate)
                            {
                                if (objPatientDetails.Count > 0)
                                {
                                    for (int i = 0; i <= objPatientDetails.Count - 1; i++)
                                    {
                                        _gloTemplate = new gloOffice.gloTemplate(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                        _gloTemplate.AppointmentID = 0;
                                        _gloTemplate.TemplateID = Convert.ToInt64(nTemplateID);  //Convert.ToInt64(oTemplateNode.Tag);
                                        _gloTemplate.TemplateName = templateName;  //oTemplateNode.Text;
                                        _gloTemplate.PrimeryID = nTemplateID;//  Convert.ToInt64(c1ChkInPatients.Rows[i][COL_DTLAPPOINTMENTID]); // Convert.ToInt64(oTemplateNode.Tag);
                                        _gloTemplate.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
                                        _gloTemplate.DocumentCategory = 0;
                                        _gloTemplate.PatientID = objPatientDetails[i].PatientID;
                                        _gloTemplate.nPAccountID = objPatientDetails[i].PatientAccountID;
                                        _gloTemplate.TransactionID = objPatientDetails[i].TransactionID;
                                        _gloTemplate.TransactionMstID = objPatientDetails[i].MstTransactionID;
                                        _gloTemplate.IsTemplateContainsPatientAccountFields = true;//Template.IsContainsPatientAccountFields
                                        _gloTemplate.TemplateFilePath = fileName;
                                        gloTemplates.Add(_gloTemplate);
                                    }
                                }
                            }
                            else
                            {
                                _gloTemplate = new gloOffice.gloTemplate(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                _gloTemplate.AppointmentID = 0;
                                _gloTemplate.TemplateID = Convert.ToInt64(nTemplateID);  //Convert.ToInt64(oTemplateNode.Tag);
                                _gloTemplate.TemplateName = templateName;  //oTemplateNode.Text;
                                _gloTemplate.PrimeryID = nTemplateID;//  Convert.ToInt64(c1ChkInPatients.Rows[i][COL_DTLAPPOINTMENTID]); // Convert.ToInt64(oTemplateNode.Tag);
                                _gloTemplate.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
                                _gloTemplate.DocumentCategory = 0;
                                _gloTemplate.PatientID = PatientID;
                                _gloTemplate.nPAccountID = PAccountID;
                                _gloTemplate.TransactionID = TransactionID;
                                _gloTemplate.TransactionMstID = MstTransactionID;
                                _gloTemplate.IsTemplateContainsPatientAccountFields = true;//Template.IsContainsPatientAccountFields
                                _gloTemplate.TemplateFilePath = fileName;
                                gloTemplates.Add(_gloTemplate);
                            }
                        }


                        //gloWord.LoadAndCloseWord myLoadWord = new gloWord.LoadAndCloseWord();

                        //  blnBatchPrintinProgress = true;
                        if (!gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            try
                            {
                                OldPrinterName = myPrinterSetting.PrinterName;// printDocument1.PrinterSettings.PrinterName;
                            }
                            catch
                            {
                            }
                        }
                        using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog(true))
                        {
                            oDialog.ConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;

                            oDialog.TopMost = true;

                            oDialog.ModuleName = "Printing Batch ReferralLetter";

                            oDialog.RegistryModuleName = "PrintBatchDocuments";

                            if (oDialog != null)
                            {
                                if (!gloGlobal.gloTSPrint.isCopyPrint)
                                {
                                    oDialog.PrinterSettings = myPrinterSetting;//printDocument1.PrinterSettings;
                                    oDialog.AllowSomePages = true;
                                    oDialog.bUseDefaultPrinter = true;
                                    //oDialog.ShowPrinterProfileDialog = true;

                                    oDialog.PrinterSettings.ToPage = 1;
                                    ////maxPage;
                                    oDialog.PrinterSettings.FromPage = 1;
                                    oDialog.PrinterSettings.MaximumPage = 1;
                                    //// maxPage;
                                    oDialog.PrinterSettings.MinimumPage = 1;
                                }
                            }
                            oDialog.AllowSomePages = true;

                            if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {

                                if ((oDialog.bUseDefaultPrinter == true))
                                {
                                    oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                    oDialog.CustomPrinterExtendedSettings.IsShowProgress = true;
                                }



                                frmgloPrintSetupDateActionTemplate ogloPrintProgressController = new frmgloPrintSetupDateActionTemplate(oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, null, this);
                                ogloPrintProgressController.lstgloTemplate = gloTemplates;
                                ogloPrintProgressController.bIsBatchGenerate = bIsBatchGenerate;
                                ogloPrintProgressController.oldPrinterName = OldPrinterName;

                                ogloPrintProgressController._databaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString; ;
                                if (oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint)
                                {

                                    if (oDialog.CustomPrinterExtendedSettings.IsShowProgress)
                                    {


                                        ogloPrintProgressController.Show();

                                    }
                                    else
                                    {
                                        ogloPrintProgressController.Show();
                                    }
                                }
                                else
                                {
                                    ogloPrintProgressController.TopMost = true;
                                    ogloPrintProgressController.ShowInTaskbar = false;

                                    ogloPrintProgressController.ShowDialog();
                                    if (ogloPrintProgressController != null)
                                    {
                                        ogloPrintProgressController.Dispose();
                                    }
                                    ogloPrintProgressController = null;

                                }
                            }
                        }




                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    finally
                    {
                        //this.Cursor = Cursors.Default;
                        //if (_gloTemplate != null) { _gloTemplate.Dispose(); }
                        //if (gloTemplates != null) { gloTemplates = null; }
                        //if (this.Parent != null)
                        //{
                        //    this.Parent.Cursor = Cursors.Default;
                        //}
                        //this.Cursor = Cursors.Default;

                    }
                }
                //}//if end
            }

        }

        private string ValidateFormData()
        {
            DialogResult _dlg = System.Windows.Forms.DialogResult.None;
            string sMessage = string.Empty;
            try
            {

                if (cmbLogAction.Text.Trim() == string.Empty)
                {
                    sMessage = "Please select an Action.";
                    cmbLogAction.Focus();
                    return sMessage;
                }

                mskScheduleFollowupDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

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
                }

                if (sMessage == string.Empty)
                {
                    if (chkScheduleFollowup.Checked && cmbScheduleFollowup.Text == string.Empty && mskScheduleFollowupDate.Text == string.Empty)
                    {
                        sMessage = "Please select an Action.";
                        cmbScheduleFollowup.Focus();
                    }
                }

                if (sMessage == string.Empty)
                {
                    mskScheduleFollowupDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
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
                    }
                }
                if (sMessage == string.Empty && lblTemplate.Text.Trim() == string.Empty)
                {
                    sMessage = "Please associate template with Log Action.";
                    lblTemplate.Focus();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
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
                if (bReturn && !bHasWorked)
                {
                    var sActionLogDesc = (from dt in dtActions.AsEnumerable()
                                            where dt.Field<String>("sFollowUpActionCode") == Convert.ToString(cmbLogAction.SelectedValue)
                                          select dt.Field<String>("sFollowUpActionDescription")).ToArray().SingleOrDefault();
                    DateTime dtScheduledFollowupDate = DateTime.MinValue;

                    if (IsValidDate(mskScheduleFollowupDate.Text.Trim()) == false)
                        dtScheduledFollowupDate = Convert.ToDateTime(DateTime.MinValue.ToString("MM/dd/yyyy"));
                    else
                        dtScheduledFollowupDate = Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim());
                    if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                    {
                        if (!bIsBatchGenerate)
                        {
                            bReturn = oCollection.SaveFollowUpLog(FollowUpActionType, TransactionID, dtCurrentDate, Convert.ToString(cmbLogAction.SelectedValue), sActionLogDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, ClaimLogTimeStamp, ref bHasWorked);
                            //if (bReturn && !bHasWorked)
                            //{
                            //    ClaimFollowUpTimeStamp = DateTime.MinValue;
                            //}
                        }
                        else
                        {
                            if (dtTVP != null && dtTVP.Rows.Count > 0)
                            {
                                bReturn = oCollection.SaveFollowUpLog_Multiple(FollowUpActionType, dtCurrentDate, Convert.ToString(cmbLogAction.SelectedValue), sActionLogDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, dtScheduledFollowupDate, dtTVP, out dtClaimLog, nAuditLogID, "Printing");
                                
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
                        if (!bIsBatchGenerate)
                        {
                            bReturn = oCollection.SaveFollowUpLog(FollowUpActionType, PAccountID, PatientID, dtCurrentDate, Convert.ToString(cmbLogAction.SelectedValue), sActionLogDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, AccountLogTimeStamp, ref bHasWorked);
                            //if (bReturn && !bHasWorked)
                            //{
                            //    AccountFollowUpTimeStamp = DateTime.MinValue;
                            //}
                        }
                        else
                        {
                            if (dtTVP != null && dtTVP.Rows.Count > 0)
                            {
                                //dtTVP = CreateDataTable();
                                bReturn = oCollection.SaveBatchAccountFollowUpLog(FollowUpActionType, dtCurrentDate, Convert.ToString(cmbLogAction.SelectedValue), sActionLogDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, dtTVP);
                                //bReturn = oCollection.SaveFollowUpLog_Multiple(FollowUpActionType, dtCurrentDate, Convert.ToString(cmbLogAction.SelectedValue), sActionLogDesc.ToString(), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, dtScheduledFollowupDate, dtTVP, out dtAccountLog, nAuditLogID, "Printing");
                                //DataRow[] dr = dtAccountLog.Select("HasWorked='No'");
                                //if (dr != null && dr.Length > 0)
                                //{
                                //    dtTVP = DeleteExtraColumns(dr.CopyToDataTable<DataRow>(), FollowUpActionType);
                                //}
                                //else
                                //{
                                //    dtTVP = null;
                                //}
                            }
                        }
                    }
                }

                if (bReturn && chkScheduleFollowup.Checked && !bHasWorked)
                {
                    var sActionScheduleDesc = (from dt in dtScheduleActions.AsEnumerable()
                                                where dt.Field<String>("sFollowUpActionCode") == Convert.ToString(cmbScheduleFollowup.SelectedValue)
                                                select dt.Field<String>("sFollowUpActionDescription")).ToArray().SingleOrDefault();

                    if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                    {
                        if (!bIsBatchGenerate)
                        {
                            bReturn = oCollection.SaveFollowUpScedule(FollowUpActionType, TransactionID, Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim()), Convert.ToString(cmbScheduleFollowup.SelectedValue), Convert.ToString(sActionScheduleDesc), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, ClaimFollowUpTimeStamp, ref bHasWorked);
                        }
                        else
                        {
                            if (dtTVP != null && dtTVP.Rows.Count > 0)
                            {
                                bReturn = oCollection.SaveFollowUpScedule_Multiple(FollowUpActionType, Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim()), Convert.ToString(cmbScheduleFollowup.SelectedValue), Convert.ToString(sActionScheduleDesc), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, dtTVP, out dtClaimSchedule, nAuditLogID, "Printing");
                                DataRow[] dr = dtClaimSchedule.Select("HasWorked='No'");
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
                        if (!bIsBatchGenerate)
                        {
                            bReturn = oCollection.SaveFollowUpScedule(FollowUpActionType, PAccountID, PatientID, Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim()), Convert.ToString(cmbScheduleFollowup.SelectedValue), Convert.ToString(sActionScheduleDesc), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, AccountFollowUpTimeStamp, ref bHasWorked);
                        }
                        else
                        {
                            if (dtTVP != null && dtTVP.Rows.Count > 0)
                            {
                                //dtTVP = CreateDataTable();
                                bReturn = oCollection.SaveBatchAccountFollowUpScedule(FollowUpActionType, Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim()), Convert.ToString(cmbScheduleFollowup.SelectedValue), Convert.ToString(sActionScheduleDesc), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, dtTVP);
                                //bReturn = oCollection.SaveFollowUpScedule_Multiple(FollowUpActionType, Convert.ToDateTime(mskScheduleFollowupDate.Text.Trim()), Convert.ToString(cmbScheduleFollowup.SelectedValue), Convert.ToString(sActionScheduleDesc), gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.Manual, dtTVP, out dtAccountSchedule, nAuditLogID, "Printing");
                                //DataRow[] dr = dtAccountSchedule.Select("HasWorked='No'");
                                //if (dr != null && dr.Length > 0)
                                //{
                                //    dtTVP = DeleteExtraColumns(dr.CopyToDataTable<DataRow>(), FollowUpActionType);
                                //}
                                //else
                                //{
                                //    dtTVP = null;
                                //}
                            }
                        }
                    }
                }

                DataTable dtClaimNote = null;
                DataTable dtAccountNote = null;
                if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
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
                else if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                {
                    //if (dtAccountLog != null && dtAccountLog.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dtAccountSchedule.Rows.Count; i++)
                    //    {
                    //        DataRow[] drr = dtAccountLog.Select("nAccountID='" + Convert.ToString(dtAccountSchedule.Rows[i]["nAccountID"]) + "' And  HasWorked <> '" + Convert.ToString(dtAccountSchedule.Rows[i]["HasWorked"]) + "'");
                    //        if (drr != null)
                    //        {
                    //            for (int j = 0; j < drr.Length; j++)
                    //            {
                    //                drr[j].Delete();
                    //            }

                    //        }
                    //        dtAccountLog.AcceptChanges();
                    //    }
                    //}


                    //dtAccountLog.Merge(dtAccountSchedule);
                    //dtAccountNote = dtAccountLog.DefaultView.ToTable(true);
                }

                if (bHasWorked)
                {
                    MessageBox.Show("Someone else has just worked this " + (FollowUpActionType == CollectionEnums.FollowUpType.Claim?"Claim.":"Account."), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bReturn = false;
                }
                if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                {
                    if (dtClaimNote != null && dtClaimNote.Rows.Count > 0)
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
                else if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                {
                    //if (dtAccountNote != null && dtAccountNote.Rows.Count > 0)
                    //{
                    //    // DataRow[] dr = dtAccountNote.Select("HasWorked='Yes'");
                    //    dtAccountNote = DeleteExtraColumns(dtAccountNote, FollowUpActionType, "Status");
                    //    if (dtAccountNote.Rows.Count > 0)
                    //    {
                    //        frmBatchFollowUpStatus ofrmBatchFollowUpStatus = new frmBatchFollowUpStatus();
                    //        ofrmBatchFollowUpStatus.dtFollowUpStatus = dtAccountNote;
                    //        ofrmBatchFollowUpStatus.FollowUpAction = FollowUpActionType;
                    //        ofrmBatchFollowUpStatus.ShowDialog(this);
                    //        ofrmBatchFollowUpStatus.Dispose();
                    //    }
                    //    else
                    //    {
                    //        return true;
                    //    }
                    //}
                }
              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oCollection != null) { oCollection.Dispose(); }
                if (oNote != null) { oNote.Dispose(); }
                if (oNotes != null) { oNotes.Dispose(); }
            }
            return bReturn;
        }

        private DataTable DeleteExtraColumns(DataTable dtInput, CollectionEnums.FollowUpType FollowUpActionType, string CalledFrom = "")
        {
            DataTable dt = new DataTable();
            dt = dtInput;
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
            if (FollowUpActionType==CollectionEnums.FollowUpType.PatientAccount)
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
                    dtTVP = new DataTable();

                    dtTVP.Columns.Add(dcAccountID);
                    dtTVP.Columns.Add(dcPatientID);

                    for (int i = 0; i <= objPatientDetails.Count - 1; i++)
                    {
                        dtTVP.Rows.Add();
                        dtTVP.Rows[i]["nAccountID"] = objPatientDetails[i].PatientAccountID;
                        dtTVP.Rows[i]["nPatientID"] = objPatientDetails[i].PatientID;

                    }
                    dtTVP.AcceptChanges();
                }
            }
            else if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
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
            }
            return dtTVP;
        }

        #endregion

        #region "Form Controls Event"

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

        private void tsb_Generate_Click(object sender, EventArgs e)
        {
            string sMessage = ValidateFormData();
            if (sMessage != string.Empty)
            {
                if (sMessage != "FutureDate")
                {
                    MessageBox.Show(sMessage, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                //if (!SaveData())
                //{
                //   // MessageBox.Show("Unable to save information!", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //else
                //{
                if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                {
                    if (GenerateClaimReceipt())
                    {
                        if (!SaveData())
                        {
                            MessageBox.Show("Unable to save information!", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }
                else if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                {
                    if (GenerateAccountReceipt())
                    {
                        if (!SaveData())
                        {
                            MessageBox.Show("Unable to save information!", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }
            }
        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            string sMessage = ValidateFormData();
            if (sMessage != string.Empty)
            {
                if (sMessage != "FutureDate")
                {
                    MessageBox.Show(sMessage, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (!SaveData())
                {
                    //MessageBox.Show("Unable to save information!", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    tsb_Generate.Enabled = false;
                    tsb_Print.Enabled = false;
                    tsb_Cancel.Enabled = false;
                    PrintReceipt(lblTemplate.Text.Trim());
                    this.Close();
                }
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

    }
}
