using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloPatient;
using gloBilling;
using gloBilling.Payment;
using gloGlobal;
using gloBilling.Collections;
using C1.Win.C1FlexGrid;

namespace gloAccountsV2
{
    public partial class frmCollectionAgencyPreviousRefund : Form
    {

        private Int64 _CollectionAgencyId = 0;
        private Int64 _MasterRefundId = 0;
        private bool voidstatus = false;

        private int Col_CloseDate = 0;
        private int Col_PaymentTrayDesc = 1;
        private int Col_RefundTo = 2;
        private int Col_RefundDate = 3;
        private int Col_RefundAmount = 4;
        private int Col_RefundNotes = 5;
        private int Col_UserName = 6;
        private int Col_Status = 7;
        private int Col_ReceiptNo = 8;
        private int Col_PaymentMode = 9;
        private int Col_ReceiptDate = 10;
        private int Col_PaymentTrayId = 11;
        private int Col_CreditCardType = 12;
        private int Col_AuthorizationNo = 13;
        private int Col_MasterRefundId = 14;

        private DataTable _dtPreviousRefund ;
        public DataTable dtPreviousRefund
        {
            get { return _dtPreviousRefund; }
            set {_dtPreviousRefund=value; }
        }

      
        public frmCollectionAgencyPreviousRefund()
        {
            InitializeComponent();
        }

        public frmCollectionAgencyPreviousRefund(Int64 CollectionAgencyID, string CollectionAgencyName)
        {
            InitializeComponent();
            _CollectionAgencyId = CollectionAgencyID;
            lblCollectionAgency.Text = CollectionAgencyName;
        }


        private void frmCollectionAgencyPreviousRefund_Load(object sender, EventArgs e)
        {
            FillCollectionAgencyRefunds();
            if (c1Refund.Rows.Count > 1)
            {
                ViewPatientRefund();
                FillPatientRefunds();
            }
            timer1.Start();
        }

        private void tsb_Void_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 nmasterrefundid = 0;
                Int64 nPatientID = 0;
                if (c1Refund.Rows.Count > 1)
                {
                    if (Convert.ToString(c1Refund.GetData(c1Refund.RowSel, this.Col_MasterRefundId)) != "")
                    {
                        nmasterrefundid = Convert.ToInt64(c1Refund.GetData(c1Refund.RowSel, Col_MasterRefundId));
                        if (DialogResult.Yes == MessageBox.Show("Do you want to void refund? ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                        {
                            int _date = 0;

                            if (mskCloseDate.MaskCompleted)
                            {
                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                _date = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.Trim());
                            }
                            frmVoidPatientRefundV2 ofrm = new frmVoidPatientRefundV2(_date, nmasterrefundid, nPatientID);
                            ofrm.ShowDialog(this);
                            ofrm.Dispose();
                            ofrm = null;
                            SetVoidData(nmasterrefundid);
                            _dtPreviousRefund = GetRefund();
                            FillCollectionAgencyRefunds();
                        }
                    }
                    else
                    {
                        lblvoid.Text = "";
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

        private void c1Refund_Click(object sender, EventArgs e)
        {
            if (c1Refund.Rows.Count > 1)
            {
                ViewPatientRefund();
                FillPatientRefunds();
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


        public DataTable GetRefund()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            DataTable dtPatientRefund = new DataTable();
            try
            {
                _sqlQuery = " SELECT Credits.dtCloseDate as nCloseDate, Credits.sPaymentTrayDesc,sRefundTo,dtRefundDate,sum(nRefundAmount) as RefundAmount ,sRefundNotes, "
                          + "Credits.sUserName,CASE Credits.bIsPaymentVoid WHEN '1' THEN 'Voided' ELSE '' END AS Status,Credits.sReceiptNo,Credits.nPaymentMode, "
                          + "Credits.dtReceiptDate,Credits.nPaymentTrayId, "
                          + "Credits_EXT.sCreditCardType,Credits_EXT.sAuthorizationNo,Refunds.nMasterRefundId "
                          + "FROM Refunds  INNER JOIN  Credits  WITH (NOLOCK) ON Credits.nCreditID = Refunds.nCreditID  "
                          + "INNER JOIN Credits_EXT WITH (NOLOCK) ON Credits_EXT.nCreditID = Refunds.nCreditID  "
                          + "GROUP BY nMasterRefundid ,Refunds.nCollectionAgencyContactId,sRefundTo,sRefundNotes,dtRefundDate,Credits.dtCloseDate,Credits.sPaymentTrayDesc, "
                          + "Credits.sUserName,Credits.bIsPaymentVoid,Credits.sReceiptNo,Credits.nPaymentMode,Credits.dtReceiptDate,Credits.nPaymentTrayId, "
                          + "Credits.sPaymentTrayDesc,Credits_EXT.sCreditCardType,Credits_EXT.sAuthorizationNo,Refunds.nMasterRefundId  "
                          + "HAVING Refunds.nCollectionAgencyContactId = " + _CollectionAgencyId;
                _sqlQuery += " ORDER BY Credits.dtCloseDate desc";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dtPatientRefund);
                oDB.Disconnect();
                return dtPatientRefund;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (oDB != null)
                    oDB.Dispose();
                return null;

            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();

                _sqlQuery = string.Empty;
            }
        }

        private void FillCollectionAgencyRefunds()
        {
            object _TotalRefund = null;
            decimal _totalrefund = 0;
            gloPatientFinancialViewV2 objPatFinacialView = null;
            DataTable dtPatientRefund = new DataTable();
            try
            {
                #region "Refund"
                dtPatientRefund = _dtPreviousRefund; // GetRefund();
                _TotalRefund = dtPatientRefund.Compute("SUM(RefundAmount)", "Status <> 'Voided' AND Isnull(RefundAmount,0) <> 0");
                if (_TotalRefund != null && _TotalRefund.ToString() != "")
                    _totalrefund = Convert.ToDecimal(_TotalRefund);
                c1Refund.Cols.Add();
                c1Refund.DataSource = dtPatientRefund.DefaultView;
                DesignRefundgrid(_totalrefund);
                setGridStyle(c1RefundTotal, c1RefundTotal.Rows.Count - 1, 1, 6);
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (_TotalRefund != null)
                    _TotalRefund = null;
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }
        }

        private void setGridStyle(C1FlexGrid C1Flex, Int32 iRowNumber, Int32 iColNumber, Int32 iColCount)
        {
            CellStyle csSubTotalRow;
            CellStyle csSubCol;
            // csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
            try
            {
                if (C1Flex.Styles.Contains("SubTotalRow"))
                {
                    csSubTotalRow = C1Flex.Styles["SubTotalRow"];
                }
                else
                {
                    csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
                    //csSubTotalRow.DataType = typeof(System.Decimal);
                    csSubTotalRow.Format = "c";
                    csSubTotalRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                    csSubTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csSubTotalRow.TextEffect = TextEffectEnum.Flat;
                    csSubTotalRow.ForeColor = Color.Blue;
                    csSubTotalRow.TextAlign = TextAlignEnum.RightCenter;
                }

            }
            catch
            {
                csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
                //csSubTotalRow.DataType = typeof(System.Decimal);
                csSubTotalRow.Format = "c";
                csSubTotalRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                csSubTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csSubTotalRow.TextEffect = TextEffectEnum.Flat;
                csSubTotalRow.ForeColor = Color.Blue;
                csSubTotalRow.TextAlign = TextAlignEnum.RightCenter;
            }

            //  csSubCol = C1Flex.Styles.Add("SubCol");
            try
            {
                if (C1Flex.Styles.Contains("SubCol"))
                {
                    csSubCol = C1Flex.Styles["SubCol"];
                }
                else
                {
                    csSubCol = C1Flex.Styles.Add("SubCol");
                    csSubCol.TextAlign = TextAlignEnum.LeftCenter;
                    csSubCol.BackColor = Color.FromArgb(255, 255, 255);
                    csSubCol.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csSubCol.TextEffect = TextEffectEnum.Flat;
                    csSubCol.ForeColor = Color.Maroon;
                }

            }
            catch
            {
                csSubCol = C1Flex.Styles.Add("SubCol");
                csSubCol.TextAlign = TextAlignEnum.LeftCenter;
                csSubCol.BackColor = Color.FromArgb(255, 255, 255);
                csSubCol.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csSubCol.TextEffect = TextEffectEnum.Flat;
                csSubCol.ForeColor = Color.Maroon;
            }


            CellRange subTotalRange;
            subTotalRange = C1Flex.GetCellRange(iRowNumber, 0, iRowNumber, iColCount);
            subTotalRange.Style = csSubTotalRow;
            CellRange subCol;
            subCol = C1Flex.GetCellRange(iRowNumber, iColNumber, iRowNumber, iColNumber);
            subCol.Style = csSubCol;
        }

        private void DesignRefundgrid(decimal TotalRefund)
        {
            try
            {
                // gloC1FlexStyle.Style(c1PatientRefund, true );
                c1Refund.ShowCellLabels = false;
                #region " Set Header "
                c1Refund.Cols[Col_CloseDate].Caption = "Close Date";
                c1Refund.Cols[Col_PaymentTrayDesc].Caption = "Tray";
                c1Refund.Cols[Col_RefundTo].Caption = "To";
                c1Refund.Cols[Col_RefundDate].Caption = "Refund Date";
                c1Refund.Cols[Col_RefundAmount].Caption = "Amount";
                c1Refund.Cols[Col_RefundNotes].Caption = "Note";
                c1Refund.Cols[Col_UserName].Caption = "User";
                c1Refund.Cols[Col_Status].Caption = "Status";
                c1Refund.Cols[Col_ReceiptNo].Caption = "Date/Time";
                c1Refund.Cols[Col_PaymentMode].Caption = "Payment Mode";
                c1Refund.Cols[Col_ReceiptDate].Caption = "Receipt Date";
                c1Refund.Cols[Col_PaymentTrayId].Caption = "Pay Tray Id";
                c1Refund.Cols[Col_CreditCardType].Caption = "CC Type";
                c1Refund.Cols[Col_AuthorizationNo].Caption = "Authorization";
                c1Refund.Cols[Col_MasterRefundId].Caption = "Master Refund Id";

                #endregion

                int _nWidth = 0;
                _nWidth = 976;//Convert.ToInt32( c1QueuedClaims.Width);

                c1Refund.Cols[Col_CloseDate].Width = Convert.ToInt32(_nWidth * 0.10);
                c1Refund.Cols[Col_PaymentTrayDesc].Width = Convert.ToInt32(_nWidth * 0.10);
                c1Refund.Cols[Col_RefundTo].Width = Convert.ToInt32(_nWidth * 0.14);
                c1Refund.Cols[Col_RefundDate].Width = Convert.ToInt32(_nWidth * 0.10);
                c1Refund.Cols[Col_RefundAmount].Width = Convert.ToInt32(_nWidth * 0.10);
                c1Refund.Cols[Col_RefundNotes].Width = Convert.ToInt32(_nWidth * 0.25);
                c1Refund.Cols[Col_UserName].Width = Convert.ToInt32(_nWidth * 0.11);
                c1Refund.Cols[Col_Status].Width = Convert.ToInt32(_nWidth * 0.07);
                c1Refund.Cols[Col_ReceiptNo].Width = 0;
                c1Refund.Cols[Col_PaymentMode].Width = 0;
                c1Refund.Cols[Col_ReceiptDate].Width = 0;
                c1Refund.Cols[Col_PaymentTrayId].Width = 0;
                c1Refund.Cols[Col_CreditCardType].Width = 0;
                c1Refund.Cols[Col_AuthorizationNo].Width = 0;
                c1Refund.Cols[Col_MasterRefundId].Width = 0;

                c1Refund.Cols[Col_ReceiptNo].Visible = false;
                c1Refund.Cols[Col_PaymentMode].Visible = false;
                c1Refund.Cols[Col_ReceiptDate].Visible = false;
                c1Refund.Cols[Col_PaymentTrayId].Visible = false;
                c1Refund.Cols[Col_CreditCardType].Visible = false;
                c1Refund.Cols[Col_AuthorizationNo].Visible = false;
                c1Refund.Cols[Col_MasterRefundId].Visible = false;

                c1Refund.Cols[Col_RefundAmount].Format = "c";
                c1Refund.Cols[Col_CloseDate].DataType = typeof(System.DateTime);
                c1Refund.Cols[Col_CloseDate].Format = "MM/dd/yyyy";
                c1Refund.Cols[Col_RefundDate].DataType = typeof(System.DateTime);
                c1Refund.Cols[Col_RefundDate].Format = "MM/dd/yyyy";


                if (c1Refund.Rows.Count > 1)
                {
                    c1RefundTotal.Cols[0].Width = Convert.ToInt32(_nWidth * 0.10);
                    c1RefundTotal.Cols[1].Width = Convert.ToInt32(_nWidth * 0.10);
                    c1RefundTotal.Cols[2].Width = Convert.ToInt32(_nWidth * 0.14);
                    c1RefundTotal.Cols[3].Width = Convert.ToInt32(_nWidth * 0.10);
                    c1RefundTotal.Cols[4].Width = Convert.ToInt32(_nWidth * 0.10);
                    c1RefundTotal.Cols[5].Width = Convert.ToInt32(_nWidth * 0.25);
                    c1RefundTotal.Cols[6].Width = Convert.ToInt32(_nWidth * 0.11);
                    c1RefundTotal.Cols[7].Width = Convert.ToInt32(_nWidth * 0.07);
                    c1RefundTotal.Cols[8].Width = Convert.ToInt32(_nWidth * 0);
                    c1RefundTotal.Cols[9].Width = Convert.ToInt32(_nWidth * 0);

                    c1RefundTotal.Cols[1].Caption = "Total :";
                    c1RefundTotal.Cols[4].Caption = "$" + Convert.ToString(TotalRefund);
                }
                else
                {
                    c1RefundTotal.Cols[0].Width = 1300;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }

        }

        private void ViewPatientRefund()
        {
            try
            {
                #region "SET REFUND DATA"
                if (c1Refund.Rows.Count > 0 )
                {
                    mskCloseDate.Text = String.Format("{0:MM/dd/yyyy}", c1Refund.GetData(c1Refund.RowSel, Col_CloseDate));
                    txtTray.Text = Convert.ToString(c1Refund.GetData(c1Refund.RowSel, this.Col_PaymentTrayDesc));
                    txtTo.Text = Convert.ToString(c1Refund.GetData(c1Refund.RowSel, this.Col_RefundTo));
                    txtNotes.Text = Convert.ToString(c1Refund.GetData(c1Refund.RowSel, this.Col_RefundNotes));
                    txtRefundAmount.Text = "$" + Convert.ToString(c1Refund.GetData(c1Refund.RowSel, this.Col_RefundAmount));
                    mskrefunddate.Text = String.Format("{0:MM/dd/yyyy}", c1Refund.GetData(c1Refund.RowSel, this.Col_RefundDate));
                    mskCheckDate.Text = String.Format("{0:MM/dd/yyyy}", c1Refund.GetData(c1Refund.RowSel, this.Col_ReceiptDate));
                    txtCheckNumber.Text = Convert.ToString(c1Refund.GetData(c1Refund.RowSel, this.Col_ReceiptNo));
                    lblusername.Text = Convert.ToString(c1Refund.GetData(c1Refund.RowSel, this.Col_UserName));
                    _MasterRefundId = Convert.ToInt64(c1Refund.GetData(c1Refund.RowSel, this.Col_MasterRefundId));


                    if (PaymentModeV2.Cash.GetHashCode() == Convert.ToInt16(c1Refund.GetData(c1Refund.RowSel, this.Col_PaymentMode)))
                    {
                        txtPayMode.Text = "Cash";
                        txtCheckNumber.Text = "";
                        lblCheckDate.Text = "Date :";
                        lblCheckNo.Text = "Ref.# :";
                        lblCheckNo.Enabled = true;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.Enabled = true;
                    }
                    else if (PaymentModeV2.Check.GetHashCode() == Convert.ToInt16(c1Refund.GetData(c1Refund.RowSel, this.Col_PaymentMode)))
                    {
                        txtPayMode.Text = "Check";
                        lblCheckDate.Text = "Check Date :";
                        lblCheckNo.Text = "Check# :";
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (PaymentModeV2.CreditCard.GetHashCode() == Convert.ToInt16(c1Refund.GetData(c1Refund.RowSel, this.Col_PaymentMode)))
                    {
                        txtPayMode.Text = "CreditCard";
                        lblCheckDate.Text = "Date :";
                        lblCheckNo.Text = "Card# :";
                        pnlCredit.Enabled = true;
                        txtCheckNumber.MaxLength = 4;
                        txtCardType.Text = Convert.ToString(c1Refund.GetData(c1Refund.RowSel, this.Col_CreditCardType)).Trim();
                        txtCardAuthorizationNo.Text = Convert.ToString(c1Refund.GetData(c1Refund.RowSel, this.Col_AuthorizationNo)).Trim();
                        mskCreditExpiryDate.Text = "";
                    }
                    else if (PaymentModeV2.EFT.GetHashCode() == Convert.ToInt16(c1Refund.GetData(c1Refund.RowSel, this.Col_PaymentMode)))
                    {
                        txtPayMode.Text = "EFT";
                        lblCheckDate.Text = "EFT Date :";
                        lblCheckNo.Text = "EFT# :";
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (PaymentModeV2.Voucher.GetHashCode() == Convert.ToInt16(c1Refund.GetData(c1Refund.RowSel, this.Col_PaymentMode)))
                    {
                        txtPayMode.Text = PaymentModeV2.Voucher.ToString();
                        lblCheckDate.Text = "Voucher Date :";
                        lblCheckNo.Text = "Voucher# :";
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (PaymentModeV2.MoneyOrder.GetHashCode() == Convert.ToInt16(c1Refund.GetData(c1Refund.RowSel, this.Col_PaymentMode)))
                    {
                        txtPayMode.Text = "MoneyOrder";
                        lblCheckDate.Text = "MO Date :";
                        lblCheckNo.Text = "MO# :";
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    if (Convert.ToString(c1Refund.GetData(c1Refund.RowSel, this.Col_MasterRefundId)) != "")
                    {
                        SetVoidData(Convert.ToInt64(c1Refund.GetData(c1Refund.RowSel, this.Col_MasterRefundId)));
                    }
                    else
                    {
                        lblvoid.Text = "";
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
              
            }
        }

        private void SetVoidData(long nMasterrefundid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            string _strQuery = "";
            DataTable dtVoidData = new DataTable();
            try
            {
                _strQuery = "SELECT Refunds.sVoidUserName AS sVoidUserName,dtPaymentVoidCloseDate AS nVoidCloseDate " +
                            " FROM Refunds  WITH (NOLOCK) INNER JOIN Credits  WITH (NOLOCK) ON Refunds.nCreditID  =  Credits.nCreditID " +
                            " WHERE isnull(bIsPaymentVoid ,0) = 1 and Refunds.nMasterRefundId= " + nMasterrefundid;
                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dtVoidData);
                if (dtVoidData != null & dtVoidData.Rows.Count > 0)
                {
                    voidstatus = true;
                    lblvoid.Visible = true;
                    lblvoid.Text = "Voided [" + dtVoidData.Rows[0]["sVoidUserName"].ToString() + "] on " + String.Format("{0:MM/dd/yyyy}", dtVoidData.Rows[0]["nVoidCloseDate"]);
                    tsb_Void.Enabled = false;
                }
                else
                {
                    voidstatus = false;
                    lblvoid.Visible = false;
                    tsb_Void.Enabled = true;
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
                _strQuery = string.Empty;
                if (dtVoidData != null) { dtVoidData.Dispose(); dtVoidData = null; }
            }

        }

        public DataTable GetRefundPatients()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            DataTable dtPatientRefund = new DataTable();
            try
            {
                _sqlQuery = " SELECT ISNULL(nRefundID,0) as nRefundID, "
                         + " ISNULL(Patient.sFirstName,'')+SPACE(1)+ISNULL(Patient.sLastName,'') AS sPatientName, "
                         + " Credits.dtCloseDate as nCloseDate,Credits.sPaymentTrayDesc ,sRefundTo,dtRefundDate AS nRefundDate,nRefundAmount , "
                         + " sRefundNotes ,Credits.sUserName,Credits_EXT.dtModifiedDateTime AS dtCreatedDateTime , "
                         + " CASE Credits.bIsPaymentVoid WHEN '1' THEN 'Voided' ELSE '' END AS Status,  "
                         + " Refunds.nPatientID AS nPatientID ,Contacts_mst.sName as [Collection Agency] "
                         + " From Refunds  WITH (NOLOCK) INNER JOIN Patient WITH (NOLOCK) ON Refunds.nPayerID =Patient.nPatientID  INNER JOIN "
                         + " Credits  WITH (NOLOCK) ON Credits.nCreditID = Refunds.nCreditID INNER JOIN Credits_EXT WITH (NOLOCK) ON Credits.nCreditID = Credits_Ext.nCreditID  "
                         + " LEFT OUTER JOIN Contacts_mst WITH (NOLOCK) ON Contacts_mst.nContactId=Refunds.nCollectionAgencyContactId "
                         + " Where Refunds.nMasterRefundId = " + _MasterRefundId ;
                _sqlQuery += "  order by nCloseDate desc,dtCreatedDateTime desc";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dtPatientRefund);
                oDB.Disconnect();
                return dtPatientRefund;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (oDB != null)
                    oDB.Dispose();
                return null;

            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }
        }

        private void FillPatientRefunds()
        {
            object _TotalRefund = null;
            decimal _totalrefund = 0;
            DataTable dtPatientRefund=null;
            try
            {
                #region "Refund"
                dtPatientRefund = GetRefundPatients();
                _TotalRefund = dtPatientRefund.Compute("SUM(nRefundAmount)", "Status <> 'Voided' AND Isnull(nRefundAmount,0) <> 0");
                if (_TotalRefund != null && _TotalRefund.ToString() != "")
                    _totalrefund = Convert.ToDecimal(_TotalRefund);
                c1PatientRefund.Cols.Add();
                c1PatientRefund.DataSource = dtPatientRefund.DefaultView;
                DesignPatientRefundgrid(_totalrefund);
                setGridStyle(c1PatientRefundTotal, c1RefundTotal.Rows.Count - 1, 1, 6);
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (_TotalRefund != null)
                    _TotalRefund = null;

                if (dtPatientRefund != null)
                {
                    dtPatientRefund.Dispose();
                    dtPatientRefund = null;
                }
            }
        }

        private void DesignPatientRefundgrid(decimal TotalRefund)
        {
            try
            {
                c1PatientRefund.ShowCellLabels = false;
                #region " Set Header "
                c1PatientRefund.Cols["nRefundID"].Caption = "RefundID";
                c1PatientRefund.Cols["sRefundTo"].Caption = "To";
                c1PatientRefund.Cols["nCloseDate"].Caption = "Close Date";
                c1PatientRefund.Cols["sPaymentTrayDesc"].Caption = "Tray";
                c1PatientRefund.Cols["nRefundDate"].Caption = "Refund Date";
                c1PatientRefund.Cols["nRefundAmount"].Caption = "Amount";
                c1PatientRefund.Cols["sRefundNotes"].Caption = "Note";
                c1PatientRefund.Cols["sUserName"].Caption = "User";
                c1PatientRefund.Cols["dtCreatedDateTime"].Caption = "Date/Time";
                c1PatientRefund.Cols["Status"].Caption = "Status";
                c1PatientRefund.Cols["sPatientName"].Caption = "Patient";
                c1PatientRefund.Cols["nPatientID"].Caption = "nPatientID";
                c1PatientRefund.Cols["Collection Agency"].Caption = "Collection Agency";

                #endregion


                c1PatientRefund.Cols["nRefundID"].AllowResizing = false;
                c1PatientRefund.Cols["sRefundTo"].AllowResizing = false;
                c1PatientRefund.Cols["nCloseDate"].AllowResizing = false;
                c1PatientRefund.Cols["sPaymentTrayDesc"].AllowResizing = false;
                c1PatientRefund.Cols["nRefundDate"].AllowResizing = false;
                c1PatientRefund.Cols["nRefundAmount"].AllowResizing = false;
                c1PatientRefund.Cols["sRefundNotes"].AllowResizing = false;
                c1PatientRefund.Cols["sUserName"].AllowResizing = false;
                c1PatientRefund.Cols["dtCreatedDateTime"].AllowResizing = false;
                c1PatientRefund.Cols["Status"].AllowResizing = false;
                c1PatientRefund.Cols["sPatientName"].AllowResizing = false;
                c1PatientRefund.Cols["nPatientID"].AllowResizing = false;
                c1PatientRefund.Cols["Collection Agency"].AllowResizing = false;

                c1PatientRefund.Cols["nRefundID"].AllowSorting = false;
                c1PatientRefund.Cols["sRefundTo"].AllowSorting = false;
                c1PatientRefund.Cols["nCloseDate"].AllowSorting = false;
                c1PatientRefund.Cols["sPaymentTrayDesc"].AllowSorting = false;
                c1PatientRefund.Cols["nRefundDate"].AllowSorting = false;
                c1PatientRefund.Cols["nRefundAmount"].AllowSorting = false;
                c1PatientRefund.Cols["sRefundNotes"].AllowSorting = false;
                c1PatientRefund.Cols["sUserName"].AllowSorting = false;
                c1PatientRefund.Cols["dtCreatedDateTime"].AllowSorting = false;
                c1PatientRefund.Cols["Status"].AllowSorting = false;
                c1PatientRefund.Cols["sPatientName"].AllowSorting = false;
                c1PatientRefund.Cols["nPatientID"].AllowSorting = false;
                c1PatientRefund.Cols["Collection Agency"].AllowSorting = false;


                int _nWidth = 0;
                _nWidth = 976;//Convert.ToInt32( c1QueuedClaims.Width);
                c1PatientRefund.Cols["nRefundID"].Width = 0;
                c1PatientRefund.Cols["nRefundID"].Visible = false;
                c1PatientRefund.Cols["sPatientName"].Width = Convert.ToInt32(_nWidth * 0.30);
                c1PatientRefund.Cols["nCloseDate"].Width = Convert.ToInt32(_nWidth * 0.12);
                c1PatientRefund.Cols["sPaymentTrayDesc"].Width = Convert.ToInt32(_nWidth * 0);
                c1PatientRefund.Cols["sRefundTo"].Width = Convert.ToInt32(_nWidth * 0.20);
                c1PatientRefund.Cols["nRefundDate"].Width = Convert.ToInt32(_nWidth * 0);
                c1PatientRefund.Cols["nRefundAmount"].Width = Convert.ToInt32(_nWidth * 0.15);
                c1PatientRefund.Cols["sRefundNotes"].Width = Convert.ToInt32(_nWidth * 0);
                c1PatientRefund.Cols["sUserName"].Width = Convert.ToInt32(_nWidth * 0);
                c1PatientRefund.Cols["dtCreatedDateTime"].Width = Convert.ToInt32(_nWidth * 0.15);
                c1PatientRefund.Cols["dtCreatedDateTime"].Format = "MM/dd/yyyy hh:mm tt";
                c1PatientRefund.Cols["Status"].Width = Convert.ToInt32(_nWidth * 0);
                c1PatientRefund.Cols["nPatientID"].Width = 0;
                c1PatientRefund.Cols["Collection Agency"].Width = 0;

               

                c1PatientRefund.Cols["nRefundAmount"].Format = "c";

                c1PatientRefund.Cols["nCloseDate"].DataType = typeof(System.DateTime);
                c1PatientRefund.Cols["nCloseDate"].Format = "MM/dd/yyyy";
                c1PatientRefund.Cols["nRefundDate"].DataType = typeof(System.DateTime);
                c1PatientRefund.Cols["nRefundDate"].Format = "MM/dd/yyyy";


                if (c1PatientRefund.Rows.Count > 1)
                {
                    c1PatientRefundTotal.Cols[0].Width = 0;
                    c1PatientRefundTotal.Cols[1].Width = Convert.ToInt32(_nWidth * 0.30);
                    c1PatientRefundTotal.Cols[2].Width = Convert.ToInt32(_nWidth * 0.12);
                    c1PatientRefundTotal.Cols[3].Width = Convert.ToInt32(_nWidth * 0.00);
                    c1PatientRefundTotal.Cols[4].Width = Convert.ToInt32(_nWidth * 0.20);
                    c1PatientRefundTotal.Cols[5].Width = Convert.ToInt32(_nWidth * 0.0);
                    c1PatientRefundTotal.Cols[6].Width = Convert.ToInt32(_nWidth * 0.15);
                    c1PatientRefundTotal.Cols[7].Width = Convert.ToInt32(_nWidth * 0.0);
                    c1PatientRefundTotal.Cols[8].Width = Convert.ToInt32(_nWidth * 0.0);
                    c1PatientRefundTotal.Cols[9].Width = Convert.ToInt32(_nWidth * 0.15);
                    

                    c1PatientRefundTotal.Cols[1].Caption = "Total :";
                    c1PatientRefundTotal.Cols[6].Caption = "$" + Convert.ToString(TotalRefund);
                }
                else
                {
                    c1RefundTotal.Cols[0].Width = 1300;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }
    }
}
