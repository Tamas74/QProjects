using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloBilling.Payment;
using gloSettings;
using gloDateMaster;
using gloDatabaseLayer;
using gloAuditTrail;
using gloBilling.EOBPayment;
using gloBilling;
using gloBilling.Collections;

namespace gloAccountsV2
{
    public partial class frmViewInsurancePaymentV2 : Form
    {
        #region " Variable Declarations "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
       // string _strCloseDate = "";
        private string _DatabaseConnectionString = "";
     //   private string _emrdatabaseConnectionString = "";
        private Int64 _ClinicID = 0;
        private bool _isInvokedFromInsurancePayment = false;
        #endregion " Variable Declarations "

        #region  " Grid Constants "

        public static class LogDetailsFields
        {
            public const int Notes = 0;
            public const int PatientName = 5;
            public const int ClaimNo = 6;
            public const int DOS = 7;
            public const int Charge = 8;
            public const int Approved = 9;
            public const int Paid = 10;
            public const int WriteOff = 11;
            public const int Copay = 12;
            public const int Deduct = 13;
            public const int CoInsurance = 14;
            public const int WithHold = 15;
            public const int Other = 16;
            public const int EOBPaymentID = 2;
            public const int EOBID = 1;
            public const int TransactionID = 3;
            public const int ContactID = 4;
            public const int EOBDate = 17;
            public const int Count = 18;
        }

        #endregion

        #region " Property Procedures "

        public Int64 _EOBPaymentID = 0;
        public Int64 EOBPaymentID
        {
            get { return _EOBPaymentID; }
            set { _EOBPaymentID = value; }
        }

        public Int64 _insuranceCompanyID = 0;
        public Int64 InsuranceCompanyID
        {
            get { return _insuranceCompanyID; }
            set { _insuranceCompanyID = value; }

        }

        private bool ShowAlertMessage
        {
            get
            { return lblAlertMessage.Visible; }
            set
            {
                lblAlertMessage.Visible = value;

                if (!lblAlertMessage.Visible)
                { lblAlertMessage.Text = string.Empty; }
            }
        }

        decimal _usedReserveAmount = 0;
        private decimal UsedReserveAmount
        {
            get
            {
                if (lblReserveUsed.Text != "")
                { _usedReserveAmount = Convert.ToDecimal(lblReserveUsed.Text); }

                return _usedReserveAmount;
            }
            set
            {
                _usedReserveAmount = value;
                lblReserveUsed.Text = _usedReserveAmount.ToString("#0.00");
            }
        }

        decimal _toReserveAmount = 0;
        private decimal ToReserveAmount
        {
            get
            {
                if (lblAmountAddedToReserve.Text != "")
                { _toReserveAmount = Convert.ToDecimal(lblAmountAddedToReserve.Text); }

                return _toReserveAmount;
            }
            set
            {
                _toReserveAmount = value;
                lblAmountAddedToReserve.Text = _toReserveAmount.ToString("#0.00");
            }
        }

        public bool IsVoidedNow { get; set; }

        #endregion

        #region " Constructors "

        public frmViewInsurancePaymentV2(Int64 nEOBPaymentID, bool IsInsurancePaymentCall = false)
        {
            InitializeComponent();
            _EOBPaymentID = nEOBPaymentID;
            _isInvokedFromInsurancePayment = IsInsurancePaymentCall;
            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion

            #region " Retrive Database Connection String for appSettings "

            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                {
                    _DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
                else
                {
                    _DatabaseConnectionString = "";
                }
            }
            else
            {
                _DatabaseConnectionString = "";
            }

            #endregion

        }

        #endregion

        #region " ToolStrip Button Click Events "

        private void tsb_Void_Click(object sender, EventArgs e)
        {
            DialogResult dlgRst = DialogResult.None;
            gloInsurancePaymentV2 oInsurancePayment = new gloInsurancePaymentV2();
            DateTime iVoidCloseDate = DateTime.Now;
            string strVoidTrayName = "";
            Int64 nVoidTrayId = 0;
            string strVoidTrayCode = "";
            string sMessage =string.Empty;
            string strVoidNotes = "";
            string UserName = "";
            Int64 UserID = 0;
            bool bIsPaymentVoid = false;
            bool bIsTakeBack = false;
            bool bIsRefunded = false;          
            DataTable _dtRefundedDetails = null;
        //    DataTable _dtPAccountIds = null;
            string message = string.Empty;
            try
            {
                IsVoidedNow = false;
                bIsRefunded = gloInsurancePaymentV2.IsRefunded(EOBPaymentID);
               
                if (bIsRefunded)
                {
                    _dtRefundedDetails = gloInsurancePaymentV2.RefundedCheckDetails(EOBPaymentID);
                    if (_dtRefundedDetails != null && _dtRefundedDetails.Rows.Count > 0)
                    {
                        message = "Payment may not be voided because it has been refunded." 
                                   +Environment.NewLine + "To void the payment, void the following refund:" + Environment.NewLine
                                   + Environment.NewLine + "Refund To:\t" + _dtRefundedDetails.Rows[0]["RefundTo"].ToString()
                                   + Environment.NewLine + "Check Date:\t" + String.Format("{0:MM/dd/yyyy}",_dtRefundedDetails.Rows[0]["RefundCheckDate"])
                                   + Environment.NewLine + "Check Number:\t" + _dtRefundedDetails.Rows[0]["RefundCheckNo"].ToString()
                                   + Environment.NewLine + "Refund:\t\t$" + _dtRefundedDetails.Rows[0]["RefundAmt"].ToString();
                        MessageBox.Show(message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                bIsPaymentVoid = gloInsurancePaymentV2.IsVoidedInsurancePayment(EOBPaymentID);
                if (bIsPaymentVoid)
                {
                    MessageBox.Show("Payment is already voided.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                message = "Voiding the insurance payment will:" + Environment.NewLine 
                                   + Environment.NewLine + "1) Mark the insurance payment as Voided"
                                   + Environment.NewLine + "2) Mark each claim remittance as Voided"
                                   + Environment.NewLine + "3) Subtract each payment allocation "
                                   + Environment.NewLine + "4) Subtract each adjustments "
                                   + Environment.NewLine + "5) Reverse charge responsibility"
                                   + Environment.NewLine + "6) Delete any payment charge notes"
                                   + Environment.NewLine + Environment.NewLine + "Do you want to continue?";

                dlgRst = MessageBox.Show(message, AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                try
                {
                    if (dlgRst == DialogResult.Yes)
                    {
                        bIsTakeBack = gloInsurancePaymentV2.IsTakeBack(EOBPaymentID);
                        if (bIsTakeBack)
                        {
                            MessageBox.Show("Claims under this check have subsequent takebacks from other checks, which are preventing the void.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        frmVoidPaymentV2 ofrmVoid = new frmVoidPaymentV2(EOBPaymentID);
                        ofrmVoid.ShowDialog(this);
                        if (ofrmVoid.oDialogResult)
                        {
                            bool _isOverAllocated = gloInsurancePaymentV2.IsCheckOverAllocated(EOBPaymentID);
                            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                            iVoidCloseDate = ofrmVoid.VoidCloseDate;
                            strVoidTrayName = ofrmVoid.VoidTrayName;
                            nVoidTrayId = ofrmVoid.VoidTrayID;
                            strVoidTrayCode = ofrmVoid.VoidTrayCode;
                            strVoidNotes = ofrmVoid.VoidNotes;
                            UserName = ofrmVoid.UserName;
                            UserID = ofrmVoid.UserID;
                           // sMessage = oInsurancePayment.VoidInsurancePayment(EOBPaymentID, strVoidNotes, iVoidCloseDate, nVoidTrayId, strVoidTrayName);
                            sMessage = oInsurancePayment.VoidInsurancePayment_V2(EOBPaymentID, strVoidNotes, iVoidCloseDate, nVoidTrayId, strVoidTrayName);
                            if (sMessage != "SUCCESS")
                            {
                                ofrmVoid.Dispose();
                                throw new Exception(sMessage);
                            }
                            else
                            {
                                IsVoidedNow = true;
                                CL_FollowUpCode.SetAutoAccountFollowUp(EOBPaymentID, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, iVoidCloseDate);
                            }

                            Cls_GlobalSettings.IsPaymentVoided = true;
                            //***
                            // Show Warning Message if check has been overallocated after void
                            
                            if (_isOverAllocated)
                            {
                                string msg = "The voided payment’s reserves had been in use by other insurance payments."
                                    + Environment.NewLine + "These other insurance payments may now be over allocated (with a negative Remaining amount). "
                                    + Environment.NewLine + "Review these payments in the Pending Check dialog or the Insurance Payment Log.";

                                MessageBox.Show(msg, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        ofrmVoid.Dispose();
                        LoadFormData();
                        this.Cursor = System.Windows.Forms.Cursors.Default; 
                    }
                }
                catch (Exception Ex)
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default; 
                    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                    MessageBox.Show("Error while void payment, Void Payment not done", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception EX)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), true);
            }
        }

        private void tsb_ViewRemit_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 EOBId = 0;
                Int64 TransactionID = 0;
                Int64 ContactID = 0;
                if (c1LogDetails != null && c1LogDetails.Rows.Count > 0)
                {
                    if (c1LogDetails.RowSel > 0)
                    {
                        if (c1LogDetails.GetData(c1LogDetails.RowSel, LogDetailsFields.EOBID) != null && Convert.ToString(c1LogDetails.GetData(c1LogDetails.RowSel, LogDetailsFields.EOBID)).Trim() != ""
                            && Convert.ToInt64(c1LogDetails.GetData(c1LogDetails.RowSel, LogDetailsFields.EOBID)) > 0)
                        {
                            EOBId = Convert.ToInt64(c1LogDetails.GetData(c1LogDetails.RowSel, LogDetailsFields.EOBID));
                            TransactionID = Convert.ToInt64(c1LogDetails.GetData(c1LogDetails.RowSel, LogDetailsFields.TransactionID));
                            ContactID = Convert.ToInt64(c1LogDetails.GetData(c1LogDetails.RowSel, LogDetailsFields.ContactID));
                            //gloInsurancePaymentV2.GetContactIDByEOB(EOBId);
                            frmViewClaimRemittanceV2 ofrmViewClaimRemittance = new frmViewClaimRemittanceV2(AppSettings.ConnectionStringPM, 0, AppSettings.ClinicID, EOBPaymentID, EOBId);
                            ofrmViewClaimRemittance.StartPosition = FormStartPosition.CenterScreen;
                            ofrmViewClaimRemittance.CallingContainer = this.Name;
                            ofrmViewClaimRemittance.TransactionID = TransactionID;
                            ofrmViewClaimRemittance.ContactID = ContactID;
                            ofrmViewClaimRemittance.ShowDialog(this);
                            ofrmViewClaimRemittance.Dispose();

                            LoadFormData();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select the EOB to view remit details.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Select the EOB to view remit details.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);   
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            { }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region " Form Events & Methods "

        private void frmViewInsurancePayment_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1LogDetails, false);
            LoadFormData();
            if (_isInvokedFromInsurancePayment)
            {
                tsb_ViewRemit.Enabled = false;
                tsb_Void.Enabled = false;
            }
            
        }

        #endregion

        #region " Form Controls Events "

        private void btnViewReserveRemaining_Click(object sender, EventArgs e)
        {
            if (ToReserveAmount > 0)
            {
                using (frmReservesAvailableV2 ofrmReservesAvailable = new frmReservesAvailableV2(EOBPaymentID))
                {
                    ofrmReservesAvailable.ShowInTaskbar = false;
                    ofrmReservesAvailable.StartPosition = FormStartPosition.CenterScreen;
                    ofrmReservesAvailable.ShowDialog(this);
                }
            }
        }

        private void btnViewUsedReserve_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmInsuranceReserveUsedV2 ofrmInsuranceReserveUsed = new frmInsuranceReserveUsedV2(EOBPaymentID))
                {
                    if (ofrmInsuranceReserveUsed.EOBUsedReserves != null && ofrmInsuranceReserveUsed.EOBUsedReserves.Rows.Count > 0)
                    {
                        ofrmInsuranceReserveUsed.ShowInTaskbar = false;
                        ofrmInsuranceReserveUsed.StartPosition = FormStartPosition.CenterScreen;
                        ofrmInsuranceReserveUsed.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void rbSortByEOB_Click(object sender, EventArgs e)
        {
            rbSortByEOB.Font = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9, FontStyle.Bold);
            rbSortByClaim.Font = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
           
                DataView _dvEOBPayments = (DataView)c1LogDetails.DataSource;
                if (_dvEOBPayments != null)
                {
                    c1LogDetails.DataSource = ApplySorting(_dvEOBPayments.ToTable()).DefaultView;
                    DesignPaymentGrid();
                }
            //LoadPaymentLogDetails();
        }

        private void rbSortByClaim_Click(object sender, EventArgs e)
        {
            rbSortByEOB.Font = gloGlobal.clsgloFont.gFont ;//new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
            rbSortByClaim.Font = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9, FontStyle.Bold);
            DataView _dvEOBPayments = (DataView)c1LogDetails.DataSource;
            if (_dvEOBPayments != null)
            {
                c1LogDetails.DataSource = ApplySorting(_dvEOBPayments.ToTable()).DefaultView;
                DesignPaymentGrid();
            }
          //  LoadPaymentLogDetails();
        }

        #endregion

        #region " Methods and Procedures "

        private void LoadFormData()
        {
            try
            {
                DataRow _drLogDetails = gloInsurancePaymentV2.GetInsurancePaymentLogDetails(EOBPaymentID);
                if (_drLogDetails != null)
                {
                    if (_drLogDetails["CloseDate"] != null)
                    { lblCloseDate.Text = Convert.ToDateTime(_drLogDetails["CloseDate"]).ToString("MM/dd/yyyy"); }

                    if (_drLogDetails["Tray"] != null)
                    {
                        string _trayName = Convert.ToString(_drLogDetails["Tray"]);

                        if (_trayName.Length > 40)
                        { toolTip1.SetToolTip(lblPaymentTray, _trayName); }
                        else
                        { toolTip1.SetToolTip(lblPaymentTray, ""); }

                        lblPaymentTray.Text = _trayName;
                    }

                    if (_drLogDetails["Company"] != null)
                    {
                        string _insCompanyName = Convert.ToString(_drLogDetails["Company"]);

                        if (_insCompanyName.Length > 35)
                        { toolTip1.SetToolTip(lblInsuranceCompany, _insCompanyName); }
                        else
                        { toolTip1.SetToolTip(lblInsuranceCompany, ""); }

                        lblInsuranceCompany.Text = _insCompanyName;
                    }

                    if (_drLogDetails["nPayerID"] != null)
                    { InsuranceCompanyID = Convert.ToInt64(_drLogDetails["nPayerID"]); }

                    if (_drLogDetails["nPaymentMode"] != null)
                    {
                        int _mode = Convert.ToInt16(_drLogDetails["nPaymentMode"]);
                        if (_mode.Equals(PaymentModeV2.Check.GetHashCode()))
                        { lblPaymentType.Text = "Check"; }
                        else if (_mode.Equals(PaymentModeV2.MoneyOrder.GetHashCode()))
                        { lblPaymentType.Text = "MoneyOrder"; }
                        else if (_mode.Equals(PaymentModeV2.EFT.GetHashCode()))
                        { lblPaymentType.Text = "EFT"; }
                        else if (_mode.Equals(PaymentModeV2.Voucher.GetHashCode()))
                        { lblPaymentType.Text = "Voucher"; }
                        else if (_mode.Equals(PaymentModeV2.CreditCard.GetHashCode()))
                        { lblPaymentType.Text = "CreditCard"; }
                    }

                    if (_drLogDetails["CheckNumber"] != null)
                    { lblCheckNo.Text = Convert.ToString(_drLogDetails["CheckNumber"]).Replace("&","&&"); }

                    if (_drLogDetails["Amount"] != null)
                    { lblPaymentAmount.Text = Convert.ToString(_drLogDetails["Amount"]); }

                    if (_drLogDetails["Remaining"] != null)
                    { lblRemainingAmount.Text = Convert.ToString(_drLogDetails["Remaining"]); }

                    if (_drLogDetails["sNoteDescription"] != null)
                    { txtPaymentNote.Text = Convert.ToString(_drLogDetails["sNoteDescription"]); }
                                       
                    ToReserveAmount = gloInsurancePaymentV2.GetTotalInsuranceReservesAvailable(EOBPaymentID);                   
                    UsedReserveAmount = gloInsurancePaymentV2.GetTotalInsuranceReservesUsed(EOBPaymentID);

                    #region " Alert message for voided check "

                    DataTable _dtPaymentVoid = gloInsurancePaymentV2.GetVoidedInsurancePayment(EOBPaymentID);

                    if (_dtPaymentVoid != null && _dtPaymentVoid.Rows.Count > 0)
                    {
                        lblAlertMessage.Visible = true;
                        lblAlertMessage.Text = "Voided [" + Convert.ToString(_dtPaymentVoid.Rows[0]["sUserName"].ToString()) + "] on " + Convert.ToString(_dtPaymentVoid.Rows[0]["nVoidCloseDate"].ToString());

                        pnlVoidNote.Visible = true;
                        lblVoidDesc.Text = Convert.ToString(_dtPaymentVoid.Rows[0]["sNoteDescription"]);
                    }
                    else
                    {
                        pnlVoidNote.Visible = false;
                    }
                    #endregion

                    LoadPaymentLogDetails();
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Unable to view insurance payment.", true);
                    this.Close();
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        public void SetNotesFlag(int rowIndex, bool SetFlag)
        {
            try
            {
                System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Notes;

                if (SetFlag == true)
                { c1LogDetails.SetCellImage(rowIndex, LogDetailsFields.Notes, imgFlag); }
                else
                { c1LogDetails.SetCellImage(rowIndex, LogDetailsFields.Notes, null); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void LoadPaymentLogDetails()
        {

            try
            {
                DataTable _dtEOBPayments = new DataTable();

                
                _dtEOBPayments = gloInsurancePaymentV2.GetEOBPaymentSummary(EOBPaymentID);

                if (_dtEOBPayments != null)
                {
                    if (_dtEOBPayments.Rows.Count == 0)
                    {
                        tsb_ViewRemit.Enabled = false;
                        c1LogDetails.Rows.Count = 1;
                        c1LogDetails.Cols.Count = LogDetailsFields.Count;

                    }
                    else
                    {
                        tsb_ViewRemit.Enabled = true;

                        c1LogDetails.DataSource = ApplySorting(_dtEOBPayments).DefaultView;


                    }
                }
                else
                {
                    c1LogDetails.Rows.Count = 1;
                    c1LogDetails.Cols.Count = LogDetailsFields.Count;
                }
                DesignPaymentGrid();    
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private DataTable ApplySorting(DataTable _dtEOBPayments)
        {
            if (_dtEOBPayments != null)
            {
                DataView dv = _dtEOBPayments.DefaultView;

                if (rbSortByClaim.Checked)
                { dv.Sort = "ClaimNumber ASC"; }
                else
                { dv.Sort = "dtEOBDate ASC"; }

                _dtEOBPayments = dv.ToTable();
            }

            return _dtEOBPayments;
        }

        private void DesignPaymentGrid()
        {
            try
            {
                #region " Grid Settings "

                c1LogDetails.Redraw = false;
                gloC1FlexStyle.Style(c1LogDetails, true);
                //   c1LogDetails.Clear();

                //c1LogDetails.Cols.Count = LogDetailsFields.Count;
                //c1LogDetails.Rows.Count = 1;
                // c1LogDetails.Rows.Fixed = 1;
                //c1LogDetails.Cols.Fixed = 1;

                c1LogDetails.Cols[LogDetailsFields.Notes].Selected = false;
              


                c1LogDetails.AllowEditing = false;
                c1LogDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1LogDetails.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;

                #endregion

                #region " Set Headers "

                c1LogDetails.SetData(0, LogDetailsFields.Notes, "");
                c1LogDetails.SetData(0, LogDetailsFields.EOBPaymentID, "nEOBPaymentID");
                c1LogDetails.SetData(0, LogDetailsFields.PatientName, "Patient");
                c1LogDetails.SetData(0, LogDetailsFields.ClaimNo, "Claim #");
                c1LogDetails.SetData(0, LogDetailsFields.DOS, "DOS");
                c1LogDetails.SetData(0, LogDetailsFields.Charge, "Charge");
                c1LogDetails.SetData(0, LogDetailsFields.Paid, "Payment");
                c1LogDetails.SetData(0, LogDetailsFields.Approved, "Allowed");
                c1LogDetails.SetData(0, LogDetailsFields.WriteOff, "W/O");
                c1LogDetails.SetData(0, LogDetailsFields.Copay, "Copay");
                c1LogDetails.SetData(0, LogDetailsFields.Deduct, "Deduct");
                c1LogDetails.SetData(0, LogDetailsFields.WithHold, "Withhold");
                c1LogDetails.SetData(0, LogDetailsFields.CoInsurance, "Co-ins");
                c1LogDetails.SetData(0, LogDetailsFields.Other, "Other");
                c1LogDetails.SetData(0, LogDetailsFields.EOBID, "nEOBID");

                #endregion

                #region " Show/Hide "

                c1LogDetails.Cols[LogDetailsFields.EOBPaymentID].Visible = false;
                c1LogDetails.Cols[LogDetailsFields.EOBID].Visible = false;
                c1LogDetails.Cols[LogDetailsFields.TransactionID].Visible = false;
                c1LogDetails.Cols[LogDetailsFields.ContactID].Visible = false;
                c1LogDetails.Cols[LogDetailsFields.EOBDate].Visible = false;
                #endregion

                #region " Width "

                c1LogDetails.Cols[LogDetailsFields.Notes].Width = 20;
                c1LogDetails.Cols[LogDetailsFields.PatientName].Width = 175;
                c1LogDetails.Cols[LogDetailsFields.ClaimNo].Width = 75;
                c1LogDetails.Cols[LogDetailsFields.DOS].Width = 85;
                c1LogDetails.Cols[LogDetailsFields.Charge].Width = 65;
                c1LogDetails.Cols[LogDetailsFields.Paid].Width = 65;
                c1LogDetails.Cols[LogDetailsFields.Approved].Width = 65;
                c1LogDetails.Cols[LogDetailsFields.WriteOff].Width = 65;
                c1LogDetails.Cols[LogDetailsFields.Copay].Width = 65;
                c1LogDetails.Cols[LogDetailsFields.Deduct].Width = 65;
                c1LogDetails.Cols[LogDetailsFields.WithHold].Width = 65;
                c1LogDetails.Cols[LogDetailsFields.CoInsurance].Width = 65;
                c1LogDetails.Cols[LogDetailsFields.Other].Width = 65; 

                #endregion

                #region " Data Type "

                c1LogDetails.Cols[LogDetailsFields.EOBPaymentID].DataType = typeof(System.Int64);
                c1LogDetails.Cols[LogDetailsFields.EOBID].DataType = typeof(System.Int64);
                c1LogDetails.Cols[LogDetailsFields.PatientName].DataType = typeof(System.String);
                c1LogDetails.Cols[LogDetailsFields.ClaimNo].DataType = typeof(System.String);
                c1LogDetails.Cols[LogDetailsFields.DOS].DataType = typeof(System.String);
                c1LogDetails.Cols[LogDetailsFields.Charge].DataType = typeof(System.Decimal);
                c1LogDetails.Cols[LogDetailsFields.Paid].DataType = typeof(System.Decimal);
                c1LogDetails.Cols[LogDetailsFields.Approved].DataType = typeof(System.Decimal);
                c1LogDetails.Cols[LogDetailsFields.WriteOff].DataType = typeof(System.Decimal);
                c1LogDetails.Cols[LogDetailsFields.Copay].DataType = typeof(System.Decimal);
                c1LogDetails.Cols[LogDetailsFields.Deduct].DataType = typeof(System.Decimal);
                c1LogDetails.Cols[LogDetailsFields.WithHold].DataType = typeof(System.Decimal);
                c1LogDetails.Cols[LogDetailsFields.CoInsurance].DataType = typeof(System.Decimal);
                c1LogDetails.Cols[LogDetailsFields.Other].DataType = typeof(System.Decimal);

                #endregion

                #region " Alignment "

                c1LogDetails.Cols[LogDetailsFields.ClaimNo].TextAlign = TextAlignEnum.LeftCenter;
                c1LogDetails.Cols[LogDetailsFields.PatientName].TextAlign = TextAlignEnum.LeftCenter;

                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1LogDetails.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1LogDetails.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1LogDetails.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1LogDetails.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                    }

                }
                catch
                {
                    csCurrencyStyle = c1LogDetails.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;


                }
                C1.Win.C1FlexGrid.CellStyle csNoteStyle;
                try
                {

                    if (c1LogDetails.Styles.Contains("csNoteStyle"))
                    {
                        csNoteStyle = c1LogDetails.Styles["csNoteStyle"];
                    }
                    else
                    {
                        csNoteStyle = c1LogDetails.Styles.Add("csNoteStyle");
                        csNoteStyle.DataType = typeof(Image);
                        csNoteStyle.BackColor = System.Drawing.Color.FromArgb(86, 126, 211);

                    }

                }
                catch
                {
                    csNoteStyle = c1LogDetails.Styles.Add("csNoteStyle");
                    csNoteStyle.DataType = typeof(Image);
                    csNoteStyle.BackColor = System.Drawing.Color.FromArgb(86, 126, 211);

                }



                c1LogDetails.Cols[LogDetailsFields.Charge].Style = csCurrencyStyle;
                c1LogDetails.Cols[LogDetailsFields.Paid].Style = csCurrencyStyle;
                c1LogDetails.Cols[LogDetailsFields.Approved].Style = csCurrencyStyle;
                c1LogDetails.Cols[LogDetailsFields.WriteOff].Style = csCurrencyStyle;
                c1LogDetails.Cols[LogDetailsFields.Copay].Style = csCurrencyStyle;
                c1LogDetails.Cols[LogDetailsFields.Deduct].Style = csCurrencyStyle;
                c1LogDetails.Cols[LogDetailsFields.WithHold].Style = csCurrencyStyle;
                c1LogDetails.Cols[LogDetailsFields.CoInsurance].Style = csCurrencyStyle;
                c1LogDetails.Cols[LogDetailsFields.Other].Style = csCurrencyStyle;

                c1LogDetails.SelectionMode = SelectionModeEnum.Row;

                c1LogDetails.FocusRect = FocusRectEnum.Light;
                c1LogDetails.HighLight = HighLightEnum.Always;
                c1LogDetails.ExtendLastCol = true;

             
                #endregion

                #region " Set Note  "
                //c1LogDetails.Cols[LogDetailsFields.Notes].DataType = typeof(Image);
                c1LogDetails.Cols[LogDetailsFields.Notes].Style = csNoteStyle;
                c1LogDetails.Cols[LogDetailsFields.Notes].ImageMap = new System.Collections.Hashtable();
                c1LogDetails.Cols[LogDetailsFields.Notes].ImageMap.Add(0, null);
                c1LogDetails.Cols[LogDetailsFields.Notes].ImageMap.Add(1, global::gloBilling.Properties.Resources.Notes);
                c1LogDetails.Cols[LogDetailsFields.Notes].ImageAndText = false;
                c1LogDetails.Cols[LogDetailsFields.Notes].AllowResizing = false;
                c1LogDetails.Cols[LogDetailsFields.Notes].Selected = false;


                #endregion
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            finally
            { c1LogDetails.Redraw = true; }
        }

        #endregion
      


    }

}
