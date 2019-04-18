using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloAccountsV2;
using gloBilling;
using gloGlobal;

namespace gloAccountsV2
{
    public partial class frmPatientPayRefundViewV2 : Form
    {

        #region " Private Variables "

        private Int64 _patientId = 0;
        private string _scloseDate = "";
        private Int64 _nrefundid = 0;
        private Int64 _nCreditID = 0;

        public bool voidstatus=false;
        private bool _isValidate = true;
        private bool _isDayClose = false;

        #endregion " Private Variables "

        #region  " Grid Constants "
        
        const int COL_EOBPAYMENTID = 0;
        const int COL_SOURCE = 1; //Patient or Insurance Name
        const int COL_ORIGINALPAYMENT = 2;//Check Number,Date,Amount
        const int COL_NOTESUBTYP = 3;
        const int COL_NOTE =4;//Note
        const int COL_REFUND = 5;//Current amount to use from avaiable amount
        const int COL_PAYMODE = 6;
        const int COL_REFEOBPAYID = 7;
        const int COL_PAYERID = 8;
        const int COL_CollectionAgencyID = 9;

        

        const int COL_COUNT = 10;

        #endregion 


        #region " Constructors "

        public frmPatientPayRefundViewV2(string DatabaseConnectionString, Int64 Patientid,Int64 RefundId)
        {
            InitializeComponent();
            _patientId = Patientid;
            _nrefundid = RefundId;
        }


        #endregion " Constructors "

        #region "Form Event"
     
        private void frmPaymentUseReserve_Load(object sender, EventArgs e)
        {
            lblvoid.Visible = false;           
            FillCreditCards();
            FillPaymentMode();
            FillPatientRefund();
           // SetNoteType();
            gloBilling.gloBilling globill = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, "");         

            if (IsValidDate(mskCloseDate.Text.Trim()))
            {
                if (globill.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)))
                {
                    _isDayClose = true;
                    DisableControl();
                }
            }
            SetVoidData();
            timer1.Start();
            txtTo.Select();
            txtTo.Focus();
            globill.Dispose();
        }

        private void frmPaymentUseReserve_FormClosed(object sender, FormClosedEventArgs e)
        {      
            c1Refund.FinishEditing();
            if (this.DialogResult != DialogResult.OK) { this.DialogResult = DialogResult.Cancel; }
        }

        #endregion

        #region "Form Control Event"

        private void cmbPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPayMode.SelectedIndex >= 0)
                {

                    cmbCardType.SelectedIndex = -1;
                    txtCardAuthorizationNo.Text = "";
                    mskCreditExpiryDate.Text = "";
                    mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

                   // PaymentModeV2 _EOBPaymentMode = PaymentModeV2.None;

                    if (cmbPayMode.Text.Trim() == PaymentModeV2.Cash.ToString())
                    {
                     //   _EOBPaymentMode = PaymentModeV2.Cash;

                        txtCheckNumber.Text = "";
                        lblCheckDate.Text = "Date :";
                        lblCheckNo.Text = "Ref.# :";
                        lblCheckNo.Enabled = true;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.Enabled = true;
                    }
                    else if (cmbPayMode.Text.Trim() == PaymentModeV2.Check.ToString())
                    {
                       // _EOBPaymentMode = PaymentModeV2.Check;
                        lblCheckDate.Text = "Check Date :";
                        lblCheckNo.Text = "Check# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;

                    }
                    else if (cmbPayMode.Text.Trim() == PaymentModeV2.MoneyOrder.ToString())
                    {
                       // _EOBPaymentMode = PaymentModeV2.MoneyOrder;
                        lblCheckDate.Text = "MO Date :";
                        lblCheckNo.Text = "MO# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (cmbPayMode.Text.Trim() == PaymentModeV2.CreditCard.ToString())
                    {
                      //  _EOBPaymentMode = PaymentModeV2.CreditCard;
                        lblCheckDate.Text = "Date :";
                        lblCheckNo.Text = "Card# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = true;
                        txtCheckNumber.MaxLength = 4;

                    }
                    else if (cmbPayMode.Text.Trim() == PaymentModeV2.EFT.ToString())
                    {
                      //  _EOBPaymentMode = PaymentModeV2.EFT;
                        lblCheckDate.Text = "EFT Date :";
                        lblCheckNo.Text = "EFT# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (cmbPayMode.Text.Trim() == PaymentModeV2.Voucher.ToString())
                    {
                        //  _EOBPaymentMode = PaymentModeV2.EFT;
                        lblCheckDate.Text = "Voucher Date :";
                        lblCheckNo.Text = "Voucher# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }

                }

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (voidstatus == true)
            {
                if (lblvoid.Visible == true)
                {
                    lblvoid.Visible = false;
                }
                else
                {
                    lblvoid.Visible = true;
                }
            }

        }

        private void btnSelectPaymentTray_Click(object sender, EventArgs e)
        {
            try
            {
                gloBilling.frmBillingTraySelection ofrmBillingTraySelection = new gloBilling.frmBillingTraySelection( gloPMGlobal.DatabaseConnectionString);
                ofrmBillingTraySelection.WindowState = FormWindowState.Normal;
                ofrmBillingTraySelection.StartPosition = FormStartPosition.CenterParent;
                ofrmBillingTraySelection.IsChargeTray = false;
                ofrmBillingTraySelection.ShowDialog(this);
                if (ofrmBillingTraySelection.FormResult == DialogResult.OK)
                {
                    if (ofrmBillingTraySelection.SelectedTrayID > 0)
                    {
                        lblPaymentTray.Tag = ofrmBillingTraySelection.SelectedTrayID;
                        lblPaymentTray.Text = ofrmBillingTraySelection.SelectedTrayName;
                    }
                }

                ofrmBillingTraySelection.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mskrefunddate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _isValidate = true;
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
                            MessageBox.Show("Enter valid refund date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isValidate = false;
                            e.Cancel = true;
                        }

                    }
                    else if (((MaskedTextBox)sender).Name == mskCloseDate.Name)
                    {
                        MessageBox.Show("Enter refund date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isValidate = false;
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception)// ex)
            {
                MessageBox.Show("Enter valid refund date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isValidate = false;
                e.Cancel = true;
                //ex.ToString();
                //ex = null;
            }
        }

        private void mskCheckDate_Validating(object sender, CancelEventArgs e)
        {
            string msg = "";
            try
            {
                _isValidate = true;
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
                            msg = "Enter valid " + lblCheckDate.Text.Replace(":", "").Trim().ToLower() + ".";
                            MessageBox.Show(msg, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isValidate = false;
                            e.Cancel = true;
                        }

                    }
                    else if (((MaskedTextBox)sender).Name == mskCloseDate.Name)
                    {
                        msg = "Enter " + lblCheckDate.Text.Replace(":", "").Trim().ToLower() + ".";
                        MessageBox.Show(msg, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isValidate = false;
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception)// ex)
            {
                msg = "Enter valid " + lblCheckDate.Text.Replace(":", "").Trim().ToLower() + ".";
                MessageBox.Show(msg, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isValidate = false;
                e.Cancel = true;
                //ex.ToString();
                //ex = null;
            }
        }

        private void masktext_click(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        private void mskCreditExpiryDate_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }


        #region "Close date validation"

        private void mskCloseDate_Validating_1(object sender, CancelEventArgs e)
        {
            try
            {
                _isValidate = true;
                MaskedTextBox mskDate = (MaskedTextBox)sender;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                if (mskDate != null)
                {
                    if (mskCloseDate.Text.ToString() != _scloseDate)
                    {
                        if (strDate.Length > 0)
                        {
                            if (IsValidDate(mskDate.Text.Trim()) == false)
                            {
                                MessageBox.Show("Enter valid close date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _isValidate = false;
                                e.Cancel = true;

                            }
                            else if (mskCloseDate.MaskCompleted == true && ((MaskedTextBox)sender).Name == mskCloseDate.Name)
                            {
                                //if (mskCloseDate.Text.ToString() < _scloseDate)
                                //{
                                //    MessageBox.Show("Close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_scloseDate).ToShortDateString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    _isValidate = false;
                                //    e.Cancel = true;
                                //}
                            }
                        }
                        else if (((MaskedTextBox)sender).Name == mskCloseDate.Name)
                        {
                            MessageBox.Show("Enter close date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isValidate = false;
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception)// ex)
            {
                MessageBox.Show("Enter valid close date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isValidate = false;
                e.Cancel = true;
                //ex.ToString();
                //ex = null;
            }
        }

        #endregion "Close date validation"

        #endregion

        #region "Tool Strip button events "

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            tsb_OK.Select();
            label1.Focus();
             if(_isValidate == true )
            {
                if (SavePaymentValidation())
                {
                    UpdatePatientRefund();
                    this.DialogResult = DialogResult.OK;
                    this.Close();                                 
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1Refund.RowSel > 0)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you want to void refund ? ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        int _date = 0;

                        if (mskCloseDate.MaskCompleted)
                        {
                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                            _date = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.Trim());
                        }
                        frmVoidPatientRefundV2 ofrm = new frmVoidPatientRefundV2(_date, _nrefundid, _patientId);
                        ofrm.ShowDialog(this);
                        ofrm.Dispose();
                        ofrm = null;
                        SetVoidData();
                    }
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

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
                 
        #endregion

        #region " Design Grid "

        private void DesignReserveRefundGrid()
        {
            try
            {

                c1Refund.Cols.Count = COL_COUNT;
                c1Refund.Rows.Count = 1;
                c1Refund.Rows.Fixed = 1;
                c1Refund.Cols.Fixed = 0;

                #region " Set Header "
                c1Refund.SetData(0, COL_SOURCE, "Source");
                c1Refund.SetData(0, COL_ORIGINALPAYMENT, "Original Payment");
                c1Refund.SetData(0, COL_NOTE, "Note");
                c1Refund.SetData(0, COL_REFUND, "Refund");
                c1Refund.SetData(0, COL_NOTESUBTYP, "Type");
                c1Refund.SetData(0, COL_EOBPAYMENTID, "nEOBPaymentID");
                c1Refund.SetData(0, COL_PAYMODE, "PayMode");
                c1Refund.SetData(0, COL_REFEOBPAYID, "nRefEOBPaymentID");
                c1Refund.SetData(0, COL_PAYERID, "nPayerID");
                #endregion

                int _nWidth = 0;

                c1Refund.Cols[COL_EOBPAYMENTID].Visible = false;
                c1Refund.Cols[COL_PAYERID].Visible = false;
                c1Refund.Cols[COL_PAYMODE].Visible = false;
                c1Refund.Cols[COL_REFEOBPAYID].Visible = false;
                c1Refund.Cols[COL_CollectionAgencyID].Visible = false;



                _nWidth = 930;//Convert.ToInt32( c1QueuedClaims.Width);
                c1Refund.Cols[COL_SOURCE].Width = Convert.ToInt32(_nWidth * 0.15); ;
                c1Refund.Cols[COL_ORIGINALPAYMENT].Width = Convert.ToInt32(_nWidth * 0.25);
                // c1Refund.Cols["nPaymentNoteSubType"].Width = Convert.ToInt32(_nWidth * 0.10);
                c1Refund.Cols[COL_NOTE].Width = Convert.ToInt32(_nWidth * 0.33);
                c1Refund.Cols[COL_REFUND].Width = Convert.ToInt32(_nWidth * 0.15);
                c1Refund.Cols[COL_REFUND].Format = "c";
                c1Refund.ShowCellLabels = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {  c1Refund.Redraw = true; }
        }

        #endregion " Design Grid "

        #region " Grid Events "

        private void c1Refund_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }
                     
        #endregion

        #region " Private & Public Methods "

        private void FillPatientRefund()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet _dtReservesRefund = new DataSet();

            try
            {                           
                oParameters.Add("@nRefundID", _nrefundid , ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),                
                oDB.Connect(false);
                oDB.Retrive("PA_BL_SELECT_Patient_Refund_V2", oParameters, out _dtReservesRefund);
                oDB.Disconnect();
               // c1Refund.DataSource = _dtReservesRefund.Tables[0];
                DesignReserveRefundGrid();

                #region "SET GRID DATA"
                for (int i = 0; i < _dtReservesRefund.Tables[0].Rows.Count; i++)
                {
                    c1Refund.Rows.Add();
                    Int32 Index = c1Refund.Rows.Count - 1;
                    c1Refund.SetData(Index, COL_EOBPAYMENTID, _dtReservesRefund.Tables[0].Rows[i]["nEOBPaymentID"]);
                    c1Refund.SetData(Index, COL_NOTESUBTYP, _dtReservesRefund.Tables[0].Rows[i]["nPaymentNoteSubType"]);
                    c1Refund.SetData(Index, COL_SOURCE, _dtReservesRefund.Tables[0].Rows[i]["PatientName"]);//Pat
                    c1Refund.SetData(Index, COL_ORIGINALPAYMENT, _dtReservesRefund.Tables[0].Rows[i]["OriginalPay"]);
                    c1Refund.SetData(Index, COL_NOTE, _dtReservesRefund.Tables[0].Rows[i]["sNoteDescription"]);//Note
                    c1Refund.SetData(Index, COL_REFUND, _dtReservesRefund.Tables[0].Rows[i]["nAmount"]);//Curr
                    c1Refund.SetData(Index, COL_PAYMODE, _dtReservesRefund.Tables[0].Rows[i]["EOBPaymode"]);
                    c1Refund.SetData(Index, COL_REFEOBPAYID, _dtReservesRefund.Tables[0].Rows[i]["nRefEOBPaymentID"]);
                    c1Refund.SetData(Index, COL_PAYERID, _dtReservesRefund.Tables[0].Rows[i]["nPayerID"]);
                    c1Refund.SetData(Index, COL_CollectionAgencyID, _dtReservesRefund.Tables[0].Rows[i]["nCollectionAgencyContactID"]);
                    
                }
                #endregion

                #region "SET REFUND DATA"
                if (_dtReservesRefund.Tables[0].Rows.Count > 0 && _dtReservesRefund.Tables[1].Rows.Count > 0)
                {
                    txtTo.Text = _dtReservesRefund.Tables[1].Rows[0]["sRefundTo"].ToString();
                    txtNotes.Text = _dtReservesRefund.Tables[1].Rows[0]["sRefundNotes"].ToString();
                    txtRefundAmount.Text = "$" + _dtReservesRefund.Tables[1].Rows[0]["nRefundAmount"].ToString();
                    mskrefunddate.Text = String.Format("{0:MM/dd/yyyy}", _dtReservesRefund.Tables[1].Rows[0]["nRefundDate"]);
                    mskCheckDate.Text = String.Format("{0:MM/dd/yyyy}", _dtReservesRefund.Tables[1].Rows[0]["nCheckDate"]);
                    txtCheckNumber.Text = _dtReservesRefund.Tables[1].Rows[0]["sCheckNumber"].ToString();
                    mskCloseDate.Text = String.Format("{0:MM/dd/yyyy}", _dtReservesRefund.Tables[1].Rows[0]["nCloseDate"]);
                    lblPaymentTray.Text = _dtReservesRefund.Tables[1].Rows[0]["sPaymentTrayDescription"].ToString();
                    lblPaymentTray.Tag = _dtReservesRefund.Tables[1].Rows[0]["nPaymentTrayID"].ToString();
                    lblusername.Text = _dtReservesRefund.Tables[1].Rows[0]["sUserName"].ToString();
                    lbldatetime.Text = string.Format("{0:MM/dd/yyyy hh:mm tt}", _dtReservesRefund.Tables[1].Rows[0]["dtModifiedDateTime"]);
                    _scloseDate = _dtReservesRefund.Tables[1].Rows[0]["nCloseDate"].ToString();
                    _nCreditID =Convert.ToInt64(_dtReservesRefund.Tables[1].Rows[0]["nCreditID"]);
                    
                    //MaheshB
                   this.cmbPayMode.SelectedIndexChanged -= new System.EventHandler(this.cmbPayMode_SelectedIndexChanged);
                    if (PaymentModeV2.Cash.GetHashCode() == Convert.ToInt16(_dtReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = PaymentModeV2.Cash.ToString();
                        //txtCheckNumber.Text = "";
                        lblCheckDate.Text = "Date :";
                        lblCheckNo.Text = "Ref.# :";
                        lblCheckNo.Enabled = true;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.Enabled = true;
                    }
                    else if (PaymentModeV2.Check.GetHashCode() == Convert.ToInt16(_dtReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = PaymentModeV2.Check.ToString();
                        lblCheckDate.Text = "Check Date :";
                        lblCheckNo.Text = "Check# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (PaymentModeV2.CreditCard.GetHashCode() == Convert.ToInt16(_dtReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = PaymentModeV2.CreditCard.ToString();
                        lblCheckDate.Text = "Date :";
                        lblCheckNo.Text = "Card# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = true;
                        txtCheckNumber.MaxLength = 4;
                        cmbCardType.Text = _dtReservesRefund.Tables[1].Rows[0]["sCardType"].ToString().Trim();
                        txtCardAuthorizationNo.Text = _dtReservesRefund.Tables[1].Rows[0]["sAuthorizationNo"].ToString();
                        if (_dtReservesRefund.Tables[1].Rows[0]["nCardExpDate"].ToString().Length == 3)
                        {
                            mskCreditExpiryDate.Text = "0"+_dtReservesRefund.Tables[1].Rows[0]["nCardExpDate"].ToString();
                        }
                        else
                        {
                            if (Convert.ToString(_dtReservesRefund.Tables[1].Rows[0]["nCardExpDate"])!= "0")
                            {
                                mskCreditExpiryDate.Text = _dtReservesRefund.Tables[1].Rows[0]["nCardExpDate"].ToString();
                            }
                        }

                    }
                    else if (PaymentModeV2.EFT.GetHashCode() == Convert.ToInt16(_dtReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = PaymentModeV2.EFT.ToString();
                        lblCheckDate.Text = "EFT Date :";
                        lblCheckNo.Text = "EFT# :";
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (PaymentModeV2.Voucher.GetHashCode() == Convert.ToInt16(_dtReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = PaymentModeV2.Voucher.ToString();
                        lblCheckDate.Text = "Voucher Date :";
                        lblCheckNo.Text = "Voucher# :";
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (PaymentModeV2.MoneyOrder.GetHashCode() == Convert.ToInt16(_dtReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = PaymentModeV2.MoneyOrder.ToString();
                        lblCheckDate.Text = "MO Date :";
                        lblCheckNo.Text = "MO# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    this.cmbPayMode.SelectedIndexChanged += new System.EventHandler(this.cmbPayMode_SelectedIndexChanged);
                    Int64 nCollectionID = 0;
                    if (c1Refund.RowSel > 0)
                    {
                        nCollectionID = Convert.ToInt64(c1Refund.GetData(c1Refund.RowSel, COL_CollectionAgencyID));
                        if (nCollectionID > 0)
                        {
                            tsb_void_refund.Enabled = false;
                            tsb_OK.Enabled = false;
                        }
                    }

                }
                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                if (oDB != null)
                    oDB.Dispose();

            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }
        }
            
        private void FillPaymentMode()
        {
            cmbPayMode.Items.Clear();
            cmbPayMode.Items.Add(PaymentModeV2.Cash.ToString());
            cmbPayMode.Items.Add(PaymentModeV2.Check.ToString());
            cmbPayMode.Items.Add(PaymentModeV2.CreditCard.ToString());
            cmbPayMode.Items.Add(PaymentModeV2.MoneyOrder.ToString());
            cmbPayMode.Items.Add(PaymentModeV2.EFT.ToString());
            cmbPayMode.Items.Add(PaymentModeV2.Voucher.ToString());

            for (int i = 0; i <= cmbPayMode.Items.Count - 1; i++)
            {
                if (cmbPayMode.Items[i].ToString() == PaymentModeV2.Check.ToString())
                {
                    cmbPayMode.SelectedIndex = i;
                    break;
                }
            }

        }

        //For Payment Tray
        private bool IsAdmin(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            DataTable oDataTable = new DataTable();
            bool result = false;
            oDB.Connect(false);
            oDB.Retrive_Query("Select nAdministrator from User_MST WITH (NOLOCK) where nUserID='" + UserId + "' and nAdministrator = 1", out oDataTable);
            if (oDataTable != null)
            {
                if (oDataTable.Rows.Count > 0)
                {
                    result = true;
                }
            }
            oDataTable.Dispose();
            oDB.Dispose();
            return result;


        }
       
        private void FillCreditCards()
        {
            CreditCards oCreditCards = new CreditCards( gloPMGlobal.DatabaseConnectionString);
            DataTable _dtCards = null;

            try
            {
             //   cmbCardType.Items.Clear();
                cmbCardType.DataSource = null;
                cmbCardType.Items.Clear();
                _dtCards = oCreditCards.GetList();

                if (_dtCards != null && _dtCards.Rows.Count > 0)
                {
                    DataRow _dr = _dtCards.NewRow();
                    _dr["nCreditCardID"] = 0;
                    _dr["sCreditCardDesc"] = "";
                    _dtCards.Rows.InsertAt(_dr, 0);
                    _dtCards.AcceptChanges();

                    cmbCardType.DataSource = _dtCards.Copy();
                    cmbCardType.ValueMember = _dtCards.Columns[0].ColumnName;
                    cmbCardType.DisplayMember = _dtCards.Columns[1].ColumnName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oCreditCards != null) { oCreditCards.Dispose(); }
                //if (_dtCards != null) { _dtCards.Dispose(); }
            }
        }

        private bool SavePaymentValidation()
        {
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling( gloPMGlobal.DatabaseConnectionString, "");
            int _addDays = 0;
            _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();
            MaskedTextBox mskDate = mskCloseDate;
            mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string strDate = mskDate.Text;
            mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            try
            {
                if (mskCloseDate.Text.ToString() != _scloseDate)
                {
                    if (strDate.Trim() == "")
                    {
                        MessageBox.Show("Enter close date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCloseDate.Select();
                        mskCloseDate.Focus();
                        return false;
                    }

                    if (IsValidDate(mskDate.Text.Trim()) == false)
                    {
                        MessageBox.Show("Enter valid close date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCloseDate.Select();
                        mskCloseDate.Focus();
                        return false;

                    }

                    //else if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                    //{
                    //    MessageBox.Show("Close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    mskCloseDate.Select();
                    //    mskCloseDate.Focus();
                    //    return false;
                    //}


                    else if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                    {
                        MessageBox.Show("Selected date is already closed. Please select a different close date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCloseDate.Select();
                        mskCloseDate.Focus();
                        return false;
                    }


                    else if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
                    {
                        MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is too far in the future.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mskCloseDate.Focus();
                        mskCloseDate.Select();
                        return false;
                    }
                    else if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date)
                    {
                        DialogResult _dlgCloseDate = DialogResult.None;
                        _dlgCloseDate = MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is in future. Are you sure you want to continue with save?", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (_dlgCloseDate == DialogResult.Cancel)
                        {
                            mskCloseDate.Focus();
                            mskCloseDate.Select();
                            return false;
                        }
                        else
                        {
                            goto outer;
                        }
                    }
                }

                outer:
                
                if (lblPaymentTray.Tag == null || lblPaymentTray.Tag.ToString().Trim() == "" || Convert.ToInt64(lblPaymentTray.Tag) <= 0)
                {
                    MessageBox.Show("Select payment tray.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSelectPaymentTray.Select();
                    btnSelectPaymentTray.Focus();
                    return false;
                }

                if (txtTo.Text.Trim() == "")
                {
                    MessageBox.Show("Enter name.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTo.Focus();
                    txtTo.Select();
                    return false;
                }
                mskDate = mskrefunddate;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                if (strDate.Trim() == "")
                {
                    MessageBox.Show("Enter refund date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskrefunddate.Select();
                    mskrefunddate.Focus();
                    return false;
                }
                if (IsValidDate(mskDate.Text.Trim()) == false)
                {
                    MessageBox.Show("Enter valid refund date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskrefunddate.Select();
                    mskrefunddate.Focus();
                    return false;
                }      

                if (txtRefundAmount.Text.Trim().Length == 0 || Convert.ToDecimal(txtRefundAmount.Text.Remove(0, 1).Trim()) <= 0)
                {
                    MessageBox.Show("Enter refund amount.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (c1Refund.Rows.Count > 1)
                    {
                        c1Refund.Focus();
                        c1Refund.Select(1, COL_REFUND);
                    }
                    return false;
                }

                PaymentModeV2 _EOBPaymentMode = PaymentModeV2.None;
                if (cmbPayMode.Text.Trim() == PaymentModeV2.Cash.ToString())
                { _EOBPaymentMode = PaymentModeV2.Cash; }
                if (cmbPayMode.Text.Trim() == PaymentModeV2.Check.ToString())
                { _EOBPaymentMode = PaymentModeV2.Check; }
                else if (cmbPayMode.Text.Trim() == PaymentModeV2.MoneyOrder.ToString())
                { _EOBPaymentMode = PaymentModeV2.MoneyOrder; }
                else if (cmbPayMode.Text.Trim() == PaymentModeV2.CreditCard.ToString())
                { _EOBPaymentMode = PaymentModeV2.CreditCard; }
                else if (cmbPayMode.Text.Trim() == PaymentModeV2.EFT.ToString())
                { _EOBPaymentMode = PaymentModeV2.EFT; }

                else if (cmbPayMode.Text.Trim() == PaymentModeV2.Voucher.ToString())
                { _EOBPaymentMode = PaymentModeV2.Voucher; }

                if (_EOBPaymentMode == PaymentModeV2.None)
                {
                    MessageBox.Show("Select refund mode.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPayMode.Select();
                    cmbPayMode.Focus();
                    return false;
                }
                else if (_EOBPaymentMode == PaymentModeV2.CreditCard)
                {

                    if (mskCheckDate.MaskCompleted == false)
                    {
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToLower() + " date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCheckDate.Select();
                        mskCheckDate.Focus();
                        return false;
                    }

                    if (cmbCardType == null || cmbCardType.Items.Count <= 0 || cmbCardType.Text.Trim() == "")
                    {
                        MessageBox.Show("Select card type.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbCardType.Select();
                        cmbCardType.Focus();
                        return false;
                    }

                    mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    if (mskCreditExpiryDate.Text != "")
                    {
                        if (mskCreditExpiryDate.MaskFull == false)
                        {
                            MessageBox.Show("Enter valid " + _EOBPaymentMode.ToString().ToLower() + " expiration date (MM/yy).", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCreditExpiryDate.Select();
                            mskCreditExpiryDate.Focus();
                            return false;
                        }
                    }

                }
                else if (_EOBPaymentMode == PaymentModeV2.Check)
                {

                    if (txtCheckNumber.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToLower() + " number.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCheckNumber.Select();
                        txtCheckNumber.Focus();
                        return false;
                    }

                    mskDate = mskCheckDate;
                    mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    strDate = mskDate.Text;
                    mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                    if (strDate.Trim() == "")
                    {
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToLower() + " date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCheckDate.Select();
                        mskCheckDate.Focus();
                        return false;
                    }

                    if (IsValidDate(mskDate.Text.Trim()) == false)
                    {
                        MessageBox.Show("Enter valid " + _EOBPaymentMode.ToString().ToLower() + " date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCheckDate.Select();
                        mskCheckDate.Focus();
                        return false;
                    }

                }
                else if (_EOBPaymentMode == PaymentModeV2.EFT)
                {
                    if (txtCheckNumber.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToUpper() + " number.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCheckNumber.Select();
                        txtCheckNumber.Focus();
                        return false;
                    }
                }
                else if (_EOBPaymentMode == PaymentModeV2.Voucher)
                {
                    if (txtCheckNumber.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToUpper() + " number.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCheckNumber.Select();
                        txtCheckNumber.Focus();
                        return false;
                    }
                }

               
                if (txtNotes.Text.Trim() == "")
                {
                    MessageBox.Show("Enter refund note.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNotes.Select();
                    txtNotes.Focus();
                    return false;
                }

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }

            return true;
        }

        private void SetVoidData()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            string _strQuery = "";
            DataTable dtVoidData = new DataTable();
            try
            {
                _strQuery = "SELECT Refunds.sVoidUserName AS sVoidUserName,dtPaymentVoidCloseDate AS nVoidCloseDate FROM Refunds  WITH (NOLOCK) INNER JOIN Credits  WITH (NOLOCK) ON Refunds.nCreditID  =  Credits.nCreditID "
                           + " WHERE isnull(bIsPaymentVoid ,0) = 1 and Refunds.nRefundID= " + _nrefundid;
              //  _strQuery = "SELECT bisvoid,dtVoidDateTime,sVoidUserName,nVoidTrayDescription,nVoidCloseDate FROM BL_EOBPatient_Refund WITH (NOLOCK) WHERE isnull(bIsvoid ,0) = 1 and nRefundID=" + _nrefundid;
                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dtVoidData);
                if (dtVoidData != null & dtVoidData.Rows.Count > 0)
                {
                    voidstatus = true;
                    lblvoid.Visible = true;
                    DisableControl();
                    lblvoid.Text = "Voided [" + dtVoidData.Rows[0]["sVoidUserName"].ToString() + "] on " + String.Format("{0:MM/dd/yyyy}",dtVoidData.Rows[0]["nVoidCloseDate"]);
                    //lblusername.Text = dtVoidData.Rows[0]["sVoidUserName"].ToString();
                    //lblvoidtray.Text = dtVoidData.Rows[0]["nVoidTrayDescription"].ToString();
                    //lbldatetime.Text = dtVoidData.Rows[0]["dtVoidDateTime"].ToString();
                    tsb_void_refund.Enabled = false;
                    tsb_OK.Enabled = false;
                    txtNotes.Enabled = false;
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
            }

        }

        private void DisableControl()
        {
            mskCheckDate.Enabled = false;
            mskCloseDate.Enabled = false;
            mskrefunddate.Enabled = false;
            txtCardAuthorizationNo.Enabled = false;
            txtCheckNumber.Enabled = false;
            txtNotes.Enabled = false;
            btnSelectPaymentTray.Enabled = false;
            txtTo.Enabled = false;
            tsb_OK.Enabled = false;
            tsb_void_refund.Enabled = false;
            cmbPayMode.Enabled = false;
            cmbCardType.Enabled = false;
            mskCreditExpiryDate.Enabled = false;
            if (_isDayClose == true)
            {
                tsb_void_refund.Enabled = true;
                tsb_OK.Enabled = true;
                txtNotes.Enabled = true;
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
            catch (FormatException e)
            {
                Success = false; // If this line is reached, an exception was thrown
                e.ToString();
                e = null;
            }
            return Success;
        }

        private void UpdatePatientRefund()
        {

            DateTime closedate = DateTime.Now;
            DateTime checkdate = DateTime.Now;
            string checkno = "";
            string cardtype = "";
            long cardid = 0;
            string AuthorizationNo = "";
            long Cardexp = 0;

            PaymentModeV2 _EOBPaymentMode = PaymentModeV2.None;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            gloAccountsV2.PaymentCollection.PatientPaymentRefund oPatientPaymentRefund = new gloAccountsV2.PaymentCollection.PatientPaymentRefund();

            try
            {
                if (_isDayClose == false)
                {

                    #region "Payment Mode"
                    if (cmbPayMode.Text != "")
                    {
                        if (cmbPayMode.Text.Trim() == PaymentModeV2.None.ToString())
                        { _EOBPaymentMode = PaymentModeV2.None; }
                        else if (cmbPayMode.Text.Trim() == PaymentModeV2.Cash.ToString())
                        { _EOBPaymentMode = PaymentModeV2.Cash; }
                        else if (cmbPayMode.Text.Trim() == PaymentModeV2.Check.ToString())
                        { _EOBPaymentMode = PaymentModeV2.Check; }
                        else if (cmbPayMode.Text.Trim() == PaymentModeV2.MoneyOrder.ToString())
                        { _EOBPaymentMode = PaymentModeV2.MoneyOrder; }
                        else if (cmbPayMode.Text.Trim() == PaymentModeV2.CreditCard.ToString())
                        {
                            _EOBPaymentMode = PaymentModeV2.CreditCard;
                            cardtype = cmbCardType.Text;
                            cardid = Convert.ToInt64(cmbCardType.SelectedValue);
                            AuthorizationNo = txtCardAuthorizationNo.Text;
                            if (mskCreditExpiryDate.MaskCompleted)
                            {
                                mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                                Cardexp = Convert.ToInt64(mskCreditExpiryDate.Text);
                            }
                        }
                        else if (cmbPayMode.Text.Trim() == PaymentModeV2.EFT.ToString())
                        { _EOBPaymentMode = PaymentModeV2.EFT; }
                        else if (cmbPayMode.Text.Trim() == PaymentModeV2.Voucher.ToString())
                        { _EOBPaymentMode = PaymentModeV2.Voucher; }
                    }
                    #endregion


                    if (mskrefunddate.MaskCompleted)
                    {
                        mskrefunddate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oPatientPaymentRefund.Refunddate = Convert.ToDateTime(String.Format("{0:MM/dd/yyyy}", mskrefunddate.Text)); 
                    }

                    oPatientPaymentRefund.RefundNotes = txtNotes.Text;
                    oPatientPaymentRefund.RefundTo = txtTo.Text;
                    checkno = txtCheckNumber.Text.ToString();

                   
                    if (mskCloseDate.MaskCompleted)
                    {
                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        closedate = Convert.ToDateTime(String.Format("{0:MM/dd/yyyy}", mskCloseDate.Text)).Date;
                    }
                    if (mskCheckDate.MaskCompleted)
                    {
                        mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        checkdate = Convert.ToDateTime(String.Format("{0:MM/dd/yyyy}", mskCheckDate.Text));
                    }
                    checkno = txtCheckNumber.Text.Trim();

                    oParameters.Add("@Flag", _isDayClose, ParameterDirection.Input, SqlDbType.Bit);
                    oParameters.Add("@nRefundID", _nrefundid, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                    oParameters.Add("@nCreditID", _nCreditID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),                    
                    oParameters.Add("@nClosedate", closedate, ParameterDirection.Input, SqlDbType.Date);
                    oParameters.Add("@nPaymentTrayId", lblPaymentTray.Tag, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sPaymentTrayDesc", lblPaymentTray.Text, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sRefundto", oPatientPaymentRefund.RefundTo, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nRefundDate", oPatientPaymentRefund.Refunddate, ParameterDirection.Input, SqlDbType.Date);
                    oParameters.Add("@sRefundNote", oPatientPaymentRefund.RefundNotes, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nPaymentmode", _EOBPaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sCheckno", checkno, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nCheckdate", checkdate, ParameterDirection.Input, SqlDbType.Date);
                    oParameters.Add("@sCardType", cardtype, ParameterDirection.Input, SqlDbType.VarChar);
                    //oParameters.Add("@nCardID", cardid, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sAuthorizationNo", AuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oParameters.Add("@nCardExpDate", Cardexp, ParameterDirection.Input, SqlDbType.BigInt);
                    //oParameters.Add("@dtModifiedDateTime", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oParameters.Add("@sUserName", gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                   
                }
                else
                {
                    oParameters.Add("@Flag", _isDayClose, ParameterDirection.Input, SqlDbType.Bit);
                    oParameters.Add("@nRefundID", _nrefundid, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),   
                    oParameters.Add("@sRefundNote", txtNotes.Text, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@dtModifiedDateTime", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oParameters.Add("@sUserName", gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                 
                
                }

                oDB.Connect(false);
                oDB.Execute("BL_UPDATE_REFUND_DATA_V2", oParameters);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oPatientPaymentRefund != null) { oPatientPaymentRefund.Dispose(); }

            }

        }

        #endregion " Private & Public Methods "

        private void txtCheckNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void c1Refund_SelChange(object sender, EventArgs e)
        {
            Int64 nCollectionID = 0;
            if (c1Refund.RowSel > 0)
            {
                nCollectionID = Convert.ToInt64(c1Refund.GetData(c1Refund.RowSel, COL_CollectionAgencyID));
                if (nCollectionID > 0)
                {
                    tsb_void_refund.Enabled = false;
                    tsb_OK.Enabled = false; 
                }
            }
        }

           
    }
}