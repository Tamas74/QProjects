using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloBilling.EOBPayment.Common;
using C1.Win.C1FlexGrid;
using gloBilling.Payment;
using System.Linq;

namespace gloBilling
{
    public partial class frmEOBPaymentReasonCode : Form
    {
        #region " Varible Declarations "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseConnection = "";
        private Int64 _ClaimNo = 0;
        private Int64 _BillingTransactionID = 0;
        private Int64 _BillingTransactionDetailID = 0;

        private string _SubClaimNo = "";
        private Int64 _TrackBillingTransactionID = 0;
        private Int64 _TrackBillingTransactionDetailID = 0;

        private Int64 _UserID = 0;
        private string _UserName = "";
        private Int64 _ClinicID = 0;
        private string _messageboxcaption = "";

        private C1.Win.C1FlexGrid.C1FlexGrid _ReasonGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
        private DialogResult _frmDlgRst = DialogResult.None;

        private string _StatementNote = "";
        private bool _IncludeStatmentNoteOnPrint = false;

        private string _InternalNote = "";
        private bool _IncludeInternalNoteOnPrint = false;
        //  private bool _isFormLoading = false;

        private string _ReasonCodeSetup;
        private string _ReasonCodeType;
        private decimal _Payment;
        private decimal PendingPayment = 0;
        private DataTable _dtERAPayerSetup;
        private string _AmountMissmatch = "";

        public gloGridListControl ogloGridListControl = null;


        #endregion " Varible Declarations "

        #region " Property Procedures "

        public Int64 ClaimNo
        { get { return _ClaimNo; } set { _ClaimNo = value; } }
        public Int64 BillingTransactionID
        { get { return _BillingTransactionID; } set { _BillingTransactionID = value; } }
        public Int64 BillingTransactionDetailID
        { get { return _BillingTransactionDetailID; } set { _BillingTransactionDetailID = value; } }

        public DialogResult FrmDlgRst
        { get { return _frmDlgRst; } set { _frmDlgRst = value; } }

        public string StatementNote
        { get { return txtStatementNotes.Text.Trim(); } set { txtStatementNotes.Text = value; } }

        public bool IncludeStatmentNoteOnPrint
        { get { return chkStatementNotes.Checked; } set { chkStatementNotes.Checked = value; } }

        public string InternalNote
        { get { return txtInternalNotes.Text.Trim(); } set { txtInternalNotes.Text = value; } }

        public bool IncludeInternalNoteOnPrint
        { get { return chkInternalNotes.Checked; } set { chkInternalNotes.Checked = value; } }

        public string SubClaimNo
        { get { return _SubClaimNo; } set { _SubClaimNo = value; } }
        public Int64 TrackBillingTransactionID
        { get { return _TrackBillingTransactionID; } set { _TrackBillingTransactionID = value; } }
        public Int64 TrackBillingTransactionDetailID
        { get { return _TrackBillingTransactionDetailID; } set { _TrackBillingTransactionDetailID = value; } }

        public string ReasonCodeSetup
        { get { return _ReasonCodeSetup; } set { _ReasonCodeSetup = value; } }

        public string ReasonCodeType
        { get { return _ReasonCodeType; } set { _ReasonCodeType = value; } }

        public decimal Payment
        { get { return _Payment; } set { _Payment = value; } }


        public string AmountMissmatch
        { get { return _AmountMissmatch; } set { _AmountMissmatch = value; } }

        public DataTable dtERAPayerSetup
        {
            get { return _dtERAPayerSetup; }
            set { _dtERAPayerSetup = value; }
        }

        #endregion " Property Procedures "

        #region " Column Constants "

        const int COL_ID = 0;
        const int COL_CLAIMNO = 1;
        const int COL_EOBPAYMENTID = 2;
        const int COL_EOBID = 3;
        const int COL_EOBPAYMENTDETAILID = 4;
        const int COL_BILLINGTRANSACTIONID = 5;
        const int COL_BILLINGTRANSACTIONDETAILID = 6;
        const int COL_REASONCODE_TYPE = 7;
        const int COL_REASONCODE = 8;
        const int COL_REASONDESCRIPTION = 9;
        const int COL_REASONAMOUNT = 10;
        const int COL_CLINICID = 11;
        const int COL_SUBCLAIMNO = 12;
        const int COL_TRACK_BILLINGTRANSACTIONID = 13;
        const int COL_TRACK_BILLINGTRANSACTIONDETAILID = 14;
        const int COL_REMARK_REASON_CODES = 15;

        //const int COL_STATEMENTNOTES = 11;
        //const int COL_ISSTATEMENTNOTESONPRINT = 12;

        //const int COL_INTERNALNOTES = 13;
        //const int COL_ISINTERNALNOTESONPRINT = 14;

        const int COL_COUNT = 16;

        #endregion " Column Constants "

        #region " Constructor "

        public frmEOBPaymentReasonCode(string DatabaseConnectionString, Int64 ClaimNo, Int64 BillingTransactionID, Int64 BillingTransactionDetailID, C1.Win.C1FlexGrid.C1FlexGrid oGrid, string _ReasonCodeType="Other")
        {
            InitializeComponent();

            #region " Retrive ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            #endregion " Retrive ClinicID from AppSettings "

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM"; ;
                }
            }
            else
            { _messageboxcaption = "gloPM"; ; }

            #endregion

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

            _databaseConnection = DatabaseConnectionString;
            _ClaimNo = ClaimNo;
            _BillingTransactionID = BillingTransactionID;
            _BillingTransactionDetailID = BillingTransactionDetailID;
            ReasonCodeType = _ReasonCodeType;
            if (ReasonCodeType != "Other" && ReasonCodeType != null)
            {
                pnltls_Notes.TabStop = false;
                ReasonGrid.Select();
                ReasonGrid.Focus();
            }
            else
            {
                pnltls_Notes.TabStop = true;
                //pnltls_Notes.Select();
                pnltls_Notes.Focus();
                //tls_btnAddLine.Select();
                
            }
            DesignTotalsGrid();
            //ReasonGrid = oGrid;
        }

        #endregion " Constructor "

        #region " Form Load Event "

        private void frmEOBPaymentReasonCode_Load(object sender, EventArgs e)
        {
            //Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            //Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            //tom.SetTabOrder(scheme);
            int ReasonCodeCount_Bytype = 0;
            //DesignGrid(ReasonGrid); 
            SetComboBoxCellStyle();
            if (ReasonCodeSetup == "Standard" || ReasonCodeSetup == "PayerSetup" || ReasonCodeSetup == "Manual")
            {
                pnlAmountHeader.Visible = true;
                lblAmount.Text = _Payment.ToString("C");
                lblReasoncodeType.Text = ReasonCodeType;
                //lblAmountHeader.Text = ReasonCodeType + " Amount";
                if (ReasonCodeSetup == "Standard" || ReasonCodeSetup == "PayerSetup")
                {
                    if (dtERAPayerSetup != null)
                    {
                        if (ReasonGrid.Rows.Count > 1)
                        {
                            for (int i = 1; i < ReasonGrid.Rows.Count; i++)
                            {
                                if (Convert.ToString(ReasonGrid.GetData(i, COL_REASONCODE_TYPE)) == ReasonCodeType)
                                {
                                    ReasonCodeCount_Bytype++;
                                }
                            }
                        }

                        for (int i = 0; i < dtERAPayerSetup.Rows.Count; i++)
                        {
                            if (ReasonGrid != null)
                            {
                                int rCodePresent = ReasonGrid.FindRow(Convert.ToString(dtERAPayerSetup.Rows[i]["ReasonCode"]), 0, ReasonGrid.Cols["Code"].Index, true);
                                if (rCodePresent != -1)
                                {
                                    if (Convert.ToString(ReasonGrid.GetData(rCodePresent, "ReasonCodeType")) != ReasonCodeType)
                                    {
                                        rCodePresent = -1;
                                    }
                                }

                                if (rCodePresent == -1)
                                {
                                    ReasonGrid.Rows.Add();
                                    ReasonGrid.SetCellStyle(ReasonGrid.Rows.Count - 1, COL_REASONCODE_TYPE, ReasonGrid.Styles["csGroupCodes"]);

                                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_REASONCODE, Convert.ToString(dtERAPayerSetup.Rows[i]["ReasonCode"]));
                                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_REASONDESCRIPTION, Convert.ToString(dtERAPayerSetup.Rows[i]["sDescription"]));
                                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_REASONCODE_TYPE, ReasonCodeType);
                                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_CLAIMNO, ClaimNo);
                                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_BILLINGTRANSACTIONID, BillingTransactionID);
                                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_BILLINGTRANSACTIONDETAILID, BillingTransactionDetailID);
                                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_SUBCLAIMNO, SubClaimNo);
                                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_TRACK_BILLINGTRANSACTIONID, TrackBillingTransactionID);
                                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_TRACK_BILLINGTRANSACTIONDETAILID, TrackBillingTransactionDetailID);
                                    //   //DataRow drPropertyDetails = (from myRow in dtProperties.AsEnumerable()
                                    //   //               where myRow.Field<string>("sPropertyDisplayName") == selectednode.Tag.ToString()
                                    //   //               select myRow).FirstOrDefault();

                                    //IEnumerable< C1.Win.C1FlexGrid.Row> isMultipleReasoncode = from C1.Win.C1FlexGrid.Row datagridRow in ReasonGrid.Rows 
                                    //                            where datagridRow[COL_REASONCODE_TYPE] == ReasonCodeType
                                    //                            select datagridRow;

                                    if (dtERAPayerSetup.Rows.Count == 1)
                                    {
                                        if (ReasonCodeSetup == "Standard")
                                        {
                                            if (ReasonCodeCount_Bytype <= 1)
                                            {
                                                ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_REASONAMOUNT, _Payment);
                                            }
                                            else
                                            {
                                                ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_REASONAMOUNT, null);
                                            }
                                        }
                                        else
                                        {
                                            ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_REASONAMOUNT, _Payment);
                                        }

                                    }
                                    ReasonGrid.Focus();
                                }

                                ReasonGrid.Select(1, COL_REASONAMOUNT, true);
                            }
                        }
                    }
                }
            }
            else
            {
                lblAmount.Text = "0";
                lblAmount.Visible = false;
                lblReasoncodeType.Text = ReasonCodeType;
                //lblAmountHeader.Text = ReasonCodeType + " Amount";
                pnlAmountHeader.Visible = false;
            }




            if (ReasonCodeSetup != "Other")//Do Not Show Reasoncode of unmatched type
            {
                for (int i = 1; i < ReasonGrid.Rows.Count; i++)
                {
                    string Code = Convert.ToString(ReasonGrid.GetData(i, COL_REASONCODE));
                    string amt = Convert.ToString(ReasonGrid.GetData(i, COL_REASONAMOUNT));

                    if (Convert.ToString(ReasonGrid.GetData(i, COL_REASONCODE_TYPE)) != ReasonCodeType)
                    {
                        ReasonGrid.Rows[i].Visible = false;
                    }
                    else
                    {
                        ReasonGrid.Rows[i].Visible = true;
                    }
                }

                for (int i = 1; i < RemarkGrid.Rows.Count; i++)
                {
                    string RemarkCode = Convert.ToString(RemarkGrid.GetData(i, COL_REASONCODE));
                    string RemarkReasonCode = Convert.ToString(RemarkGrid.GetData(i, COL_REMARK_REASON_CODES));
                    for (int j = 1; j < ReasonGrid.Rows.Count; j++)
                    {
                        string Code = Convert.ToString(ReasonGrid.GetData(j, COL_REASONCODE));
                        string codeType = Convert.ToString(ReasonGrid.GetData(j, COL_REASONCODE_TYPE));

                        if (Code == RemarkReasonCode && codeType != ReasonCodeType)
                        {
                            RemarkGrid.Rows[i].Visible = false;
                        }
                    }
                }


                if (ReasonGrid.Rows.Count > 1)
                {
                    SetFocusonAmountCell();
                }

                pnlInternalNotes.Visible = false;
                pnlStatementNotes.Visible = false;
                pnlRemarkCode.Visible = false;
                pnlRemarkCodeHdr.Visible = false;
                this.Height = 370;
                lblCntrlF.Visible = true;
                lblFillAmount.Visible = true;
                CalculatePayment();
            }
            else if (ReasonCodeSetup == "Other")
            {
                lblCntrlF.Visible = false;
                lblFillAmount.Visible = false;
                pnltls_Notes.Focus();
                tls_btnAddLine.Select();
                //HighlightMissmatchAmount();
            }


           
        }

        private void HighlightMissmatchAmount()
        {
            C1.Win.C1FlexGrid.CellStyle CustomStyle = ReasonGrid.Styles.Add("CustomStyle");
            CustomStyle.ForeColor = Color.Brown;
            if (AmountMissmatch != "")
            {
                for (int i = 1; i < ReasonGrid.Rows.Count; i++)
                {
                    if (AmountMissmatch.Contains(Convert.ToString(ReasonGrid.GetData(i, COL_REASONCODE_TYPE))))
                    {
                        ReasonGrid.Cols[COL_REASONAMOUNT].Style = CustomStyle;
                    }
                }
            }
        }
        
        private void frmEOBPaymentReasonCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                if (ReasonCodeType != "Other")
                {
                    if (ReasonGrid.Rows.Count > 1)
                    {
                        if (lblPendingAmount.Text != "")
                        {
                            if (ReasonGrid.Focused == false)
                            {
                                for (int i = 1; i < ReasonGrid.Rows.Count; i++)
                                {
                                    if (Convert.ToString(ReasonGrid.GetData(i, COL_REASONCODE_TYPE)) == ReasonCodeType &&
                                        ReasonGrid.Rows[i].Visible == true)
                                    {
                                        decimal SelectedcellAmount = Convert.ToDecimal(ReasonGrid.GetData(i, COL_REASONAMOUNT));
                                        SelectedcellAmount = SelectedcellAmount + PendingPayment;
                                        ReasonGrid.SetData(i, COL_REASONAMOUNT, SelectedcellAmount);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (ReasonGrid.RowSel > 0)
                                {
                                    decimal SelectedcellAmount = Convert.ToDecimal(ReasonGrid.GetData(ReasonGrid.RowSel, COL_REASONAMOUNT));
                                    SelectedcellAmount = SelectedcellAmount + PendingPayment;
                                    ReasonGrid.SetData(ReasonGrid.RowSel, COL_REASONAMOUNT, SelectedcellAmount);
                                }
                            }
                            CalculatePayment();
                        }
                    }
                }
            }
        }

        #endregion " Form Load Event "

        private void txtStatementNotes_Enter(object sender, EventArgs e)
        {
            CloseInternalControl();
        }

        private void txtInternalNotes_Enter(object sender, EventArgs e)
        {
            CloseInternalControl();
        }

        #region " ToolStrip Button Click Event "

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            this.Close();
        }

        private void tls_btnAddLine_Click(object sender, EventArgs e)
        {

            if (RemarkGrid != null && RemarkGrid.Focused)
            {

                RemarkGrid.Rows.Add();
                RemarkGrid.SetData(RemarkGrid.Rows.Count - 1, COL_CLAIMNO, ClaimNo);
                RemarkGrid.SetData(RemarkGrid.Rows.Count - 1, COL_BILLINGTRANSACTIONID, BillingTransactionID);
                RemarkGrid.SetData(RemarkGrid.Rows.Count - 1, COL_BILLINGTRANSACTIONDETAILID, BillingTransactionDetailID);

                RemarkGrid.SetData(RemarkGrid.Rows.Count - 1, COL_SUBCLAIMNO, SubClaimNo);
                RemarkGrid.SetData(RemarkGrid.Rows.Count - 1, COL_TRACK_BILLINGTRANSACTIONID, TrackBillingTransactionID);
                RemarkGrid.SetData(RemarkGrid.Rows.Count - 1, COL_TRACK_BILLINGTRANSACTIONDETAILID, TrackBillingTransactionDetailID);


                RemarkGrid.Focus();
                RemarkGrid.Select(RemarkGrid.Rows.Count - 1, COL_REASONCODE, true);

            }
            else
            {
                if (ReasonGrid != null)
                {

                    ReasonGrid.Rows.Add();
                    ReasonGrid.SetCellStyle(ReasonGrid.Rows.Count - 1, COL_REASONCODE_TYPE, ReasonGrid.Styles["csGroupCodes"]);
                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_CLAIMNO, ClaimNo);
                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_BILLINGTRANSACTIONID, BillingTransactionID);
                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_BILLINGTRANSACTIONDETAILID, BillingTransactionDetailID);
                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_SUBCLAIMNO, SubClaimNo);
                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_TRACK_BILLINGTRANSACTIONID, TrackBillingTransactionID);
                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_TRACK_BILLINGTRANSACTIONDETAILID, TrackBillingTransactionDetailID);
                    ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_REASONCODE_TYPE, ReasonCodeType);
                    ReasonGrid.Focus();

                    ReasonGrid.Select(ReasonGrid.Rows.Count - 1, COL_REASONCODE, true);

                }
            }


            CloseInternalControl();
        }

        private void tls_btnRemoveLine_Click(object sender, EventArgs e)
        {
            if (RemarkGrid != null && RemarkGrid.Focused)
            {
                if (RemarkGrid.RowSel > 0)
                { RemarkGrid.Rows.Remove(RemarkGrid.RowSel); }
            }
            else
            {
                if (ReasonGrid != null)
                {
                    if (ReasonGrid.RowSel > 0)
                    {
                        //if (Convert.ToString(ReasonGrid.GetData(ReasonGrid.RowSel, COL_REASONCODE_TYPE)) == ReasonCodeType)//Allow remove line  for matched reason code type only
                        //{
                        ReasonGrid.Rows.Remove(ReasonGrid.RowSel); SetReasonCodes();
                        //}
                    }
                }
            }
            CloseInternalControl();
        }

        private void tlb_SavenClose_Click(object sender, EventArgs e)
        {
            //Code to Validate Flex Grid Cell
            ValidateEditEventArgs oArg = null;
            oArg = new ValidateEditEventArgs(ReasonGrid.RowSel, ReasonGrid.ColSel, CheckEnum.Checked);
            ReasonGrid_ValidateEdit(sender, oArg);
            //            
            if (ValidateSave())
            { SaveCodes(); this.Close(); }
        }

        #endregion " ToolStrip Button Click Event "

        #region " Short Cut Menu Events "

        private void mnuBilling_AddLine_Click(object sender, EventArgs e)
        {
            tls_btnAddLine_Click(null, null);
        }

        private void mnuBilling_RemoveLine_Click(object sender, EventArgs e)
        {
            tls_btnRemoveLine_Click(null, null);
        }

        private void mnuBilling_Save_Click(Object sender, EventArgs e)
        {
            tlb_SavenClose_Click(null, null);
        }

        #endregion " Short Cut Menu Events "

        #region " C1 Grid Events "

        private void ReasonGrid_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            //if (_isFormLoading == false)
            //{
            //    if (e.Col == COL_REASONCODE)
            //    {
            //        if (ReasonGrid.GetData(e.Row, COL_REASONCODE) != null && Convert.ToString(ReasonGrid.GetData(e.Row, COL_REASONCODE)).Trim() != "")
            //        {
            //            try
            //            {
            //                this.ReasonGrid.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.ReasonGrid_CellChanged);

            //                string _str = "";
            //                _str = Convert.ToString(ReasonGrid.GetData(e.Row, COL_REASONCODE)).Trim().Replace('-', ' ').Replace(',', ' ').Replace('(', ' ').Replace(')', ' ').Trim();
            //                //CellStyle cs = ReasonGrid.GetCellStyle(e.Row, COL_REASONCODE);

            //                CellStyle cs = ReasonGrid.Cols[COL_REASONCODE].Style;

            //                bool _isValidCode = false;

            //                if (_str != "")
            //                {
            //                    _str = Convert.ToString(ReasonGrid.GetData(e.Row, COL_REASONCODE)).Trim();
            //                    _isValidCode = cs.ComboList.Contains(_str);
            //                }

            //                if (_isValidCode == false)
            //                {
            //                    MessageBox.Show("Please select a valid code.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    ReasonGrid.SetData(e.Row, COL_REASONCODE, null);
            //                    ReasonGrid.SetData(e.Row, COL_REASONDESCRIPTION, null);

            //                }
            //                else if (_isValidCode == true)
            //                {
            //                    EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new global::gloBilling.EOBPayment.gloEOBPaymentInsurance(_databaseConnection);
            //                    ReasonGrid.SetData(e.Row, COL_REASONDESCRIPTION, ogloEOBPaymentInsurance.GetReasonDescription(_str));
            //                    ogloEOBPaymentInsurance.Dispose();
            //                }
            //            }
            //            catch (Exception ex)
            //            { }
            //            finally
            //            { this.ReasonGrid.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.ReasonGrid_CellChanged); }
            //        }
            //    }
            //}



        }

        private void ReasonGrid_StartEdit(object sender, RowColEventArgs e)
        {
            //if (e.Row > 0)
            //{
            //    CellNote _cellNote = null;
            //    CellRange _cellRange = ReasonGrid.GetCellRange(e.Row, e.Col);
            //    _cellRange.UserData = _cellNote;
            //}
            switch (e.Col)
            {

                case COL_REASONCODE:
                    {
                        OpenInternalControl(gloGridListControlType.ReasonCodes, "Reason Code", false, e.Row, e.Col, "");
                        string _SearchText = "";
                        if (ReasonGrid != null && ReasonGrid.Rows.Count > 0)
                        {

                            //_SearchText = Convert.ToString(ReasonGrid.GetData(COL_REASONCODE, e.Col));
                            _SearchText = Convert.ToString(ReasonGrid.GetData(e.Row, COL_REASONCODE));

                            //_SearchText = Convert.ToString("CR  8");
                            if (_SearchText != "" && ogloGridListControl != null)
                            {
                                ogloGridListControl.AdvanceSearch(_SearchText);
                            }
                        }
                    }
                    break;
                case COL_REASONDESCRIPTION:
                    {
                        OpenInternalControl(gloGridListControlType.ReasonCodes, "Reason Code", false, e.Row, e.Col, "");
                        string _SearchText = "";
                        if (ReasonGrid != null && ReasonGrid.Rows.Count > 0)
                        {

                            //_SearchText = Convert.ToString(ReasonGrid.GetData(COL_REASONCODE, e.Col));
                            _SearchText = Convert.ToString(ReasonGrid.GetData(e.Row, COL_REASONDESCRIPTION));

                            //_SearchText = Convert.ToString("CR  8");
                            if (_SearchText != "" && ogloGridListControl != null)
                            {
                                ogloGridListControl.AdvanceSearch(_SearchText);
                            }
                        }
                    }
                    break;
            }
        }

        private void ReasonGrid_ChangeEdit(object sender, EventArgs e)
        {
            string _strSearchString = "";
            try
            {
                _strSearchString = ReasonGrid.Editor.Text;

                if (ogloGridListControl != null)
                {

                    if (ReasonGrid.Col == COL_REASONCODE || ReasonGrid.Col == COL_REASONDESCRIPTION)
                    {
                        string _COL_REASONCODE = "";
                        string _COL_REASONDESC = "";

                        if (ReasonGrid != null && ReasonGrid.Rows.Count > 0)
                        {
                            _COL_REASONCODE = Convert.ToString(ReasonGrid.GetData(ReasonGrid.Row, COL_REASONCODE));
                            _COL_REASONDESC = Convert.ToString(ReasonGrid.GetData(ReasonGrid.Row, COL_REASONDESCRIPTION));
                            ogloGridListControl.SelectedReasonCode = _strSearchString;

                        }

                        if (ReasonGrid.Col != COL_REASONCODE || ReasonGrid.Col != COL_REASONDESCRIPTION)
                        {
                            //if (_strSearchString.Length == 4)
                            //{
                            //    if (_strSearchString.EndsWith(".") == false)
                            //    { _strSearchString = _strSearchString.Insert(_strSearchString.Length - 1, "."); }
                            //}
                            //else if (_strSearchString.Length > 3)
                            //{
                            //    if (_strSearchString.Substring(3, 1).ToString() != ".")
                            //    {
                            //        string _PeriodSearch = _strSearchString.Substring(0, 3) + "." + _strSearchString.Substring(3, _strSearchString.Length - 3);
                            //        _strSearchString = _PeriodSearch;
                            //    }

                            //}
                        }

                        if (_strSearchString != "" && ogloGridListControl != null)
                        {
                            ogloGridListControl.AdvanceSearch(_strSearchString);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                ex.ToString();
            }
            finally
            {
            }
        }

        private void ReasonGrid_KeyUp(object sender, KeyEventArgs e)
        {

            //int _id = 0;
            string _code = "";
            string _description = "";
            bool _isdeleted = true;

            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            RowColEventArgs e1 = null;

            try
            {

                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    #region "Enter Key"

                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                            if (_IsItemSelected)
                            {

                            }
                        }
                    }


                    #endregion
                }
                else if (e.KeyCode == Keys.Down)
                {
                    e.SuppressKeyPress = true;
                    #region "Down Key"
                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            ogloGridListControl.Focus();
                        }
                    }
                    #endregion
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    #region "Escape Key"
                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            CloseInternalControl();

                            if (ReasonGrid.RowSel > 0)
                            {

                            }
                        }
                    }
                    #endregion
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    //CellNote oCellNotes = null;


                    //if (ReasonGrid.GetData(ReasonGrid.RowSel, ReasonGrid.ColSel) != null)
                    //{
                    //    _code = ReasonGrid.GetData(ReasonGrid.RowSel, ReasonGrid.ColSel).ToString();
                    //}
                    //if (ReasonGrid.GetData(ReasonGrid.RowSel, ReasonGrid.ColSel + 1) != null)
                    //{
                    //    _description = ReasonGrid.GetData(ReasonGrid.RowSel, ReasonGrid.ColSel + 1).ToString();
                    //}


                    if (ReasonGrid.GetData(ReasonGrid.RowSel, COL_REASONCODE) != null)
                    {
                        _code = ReasonGrid.GetData(ReasonGrid.RowSel, COL_REASONCODE).ToString();
                    }
                    if (ReasonGrid.GetData(ReasonGrid.RowSel, COL_REASONDESCRIPTION) != null)
                    {
                        _description = ReasonGrid.GetData(ReasonGrid.RowSel, COL_REASONDESCRIPTION).ToString();
                    }

                    e2.oType = TransactionLineColumnType.None;

                    e.SuppressKeyPress = true;

                    #region "Delete Key"
                    switch (ReasonGrid.ColSel)
                    {

                        case COL_REASONCODE:
                            {

                                ReasonGrid.SetData(ReasonGrid.RowSel, ReasonGrid.ColSel, "");
                                ReasonGrid.SetData(ReasonGrid.RowSel, ReasonGrid.ColSel + 1, "");
                                ReasonGrid.SetData(ReasonGrid.RowSel, ReasonGrid.ColSel, null);
                                //CellRange rg = ReasonGrid.GetCellRange(ReasonGrid.RowSel, ReasonGrid.ColSel);
                                //rg.UserData = oCellNotes;
                                e2.oType = TransactionLineColumnType.ReasonCode;

                            }
                            break;
                        case COL_REASONDESCRIPTION:
                            {

                                //ReasonGrid.SetData(ReasonGrid.RowSel, ReasonGrid.ColSel, "");
                                //ReasonGrid.SetData(ReasonGrid.RowSel, ReasonGrid.ColSel + 1, "");

                                ReasonGrid.SetData(ReasonGrid.RowSel, COL_REASONCODE, "");
                                ReasonGrid.SetData(ReasonGrid.RowSel, COL_REASONDESCRIPTION, "");
                                ReasonGrid.SetData(ReasonGrid.RowSel, ReasonGrid.ColSel, null);
                                //CellRange rg = ReasonGrid.GetCellRange(ReasonGrid.RowSel, ReasonGrid.ColSel);
                                //rg.UserData = oCellNotes;
                                e2.oType = TransactionLineColumnType.ReasonCode;

                            }
                            break;
                        case COL_REASONAMOUNT:
                            {
                                ReasonGrid.SetData(ReasonGrid.RowSel, ReasonGrid.ColSel, null);
                                e2.oType = TransactionLineColumnType.ReasonCode;
                            }
                            break;
                    }
                    _code = "";
                    e1 = new RowColEventArgs(ReasonGrid.RowSel, ReasonGrid.ColSel);
                    e2.code = _code;
                    e2.description = _description;
                    e2.isdeleted = true;


                    e2.code = _code;
                    e2.description = _description;
                    e2.isdeleted = _isdeleted;


                    #endregion

                    CalculatePayment();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                ex.ToString();
            }
            finally
            {

            }

        }

        private void ReasonGrid_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void ReasonGrid_SelChange(object sender, EventArgs e)
        {
            //Commented in 6030 
            //CloseInternalControl();
        }

        // solving issue # mantis id-1289
        private void ReasonGrid_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (e.Col == COL_REASONAMOUNT)   //Check for max value create it zero
                {
                    if (ReasonGrid.GetData(ReasonGrid.RowSel, COL_REASONAMOUNT) != null)
                    {
                        if (Convert.ToDecimal(ReasonGrid.GetData(ReasonGrid.RowSel, COL_REASONAMOUNT)) == 79228162514264300000000000000M)
                        {
                            ReasonGrid.SetData(ReasonGrid.RowSel, COL_REASONAMOUNT, 0);
                        }
                    }
                    if (ReasonCodeType != "" && ReasonCodeType != "Other")
                    {
                        CalculatePayment();
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                ex.ToString();
            }
        }

        void ReasonGrid_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            if (ReasonGrid.Cols[e.Col].Name == "Amount")
            {
                if (ReasonGrid.Editor != null)
                {
                    ReasonGrid.SetData(e.Row, e.Col, ReasonGrid.Editor.Text);
                }
                return;
            }
        }

        private void ReasonGrid_Enter(object sender, EventArgs e)
        {
            pnlRemarkCodemain.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            pnlRemarkCodemain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pnlRemarkCodemain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));

            pnlReasonCodemain.BackgroundImage = global::gloBilling.Properties.Resources.Img_Rx_MxGreen;
            pnlReasonCodemain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            lblReasonCodemain.ForeColor = System.Drawing.Color.White;
        }

        private void RemarkGrid_Enter(object sender, EventArgs e)
        {


            pnlReasonCodemain.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            pnlReasonCodemain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            lblReasonCodemain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));

            pnlRemarkCodemain.BackgroundImage = global::gloBilling.Properties.Resources.Img_Rx_MxGreen;
            pnlRemarkCodemain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pnlRemarkCodemain.ForeColor = System.Drawing.Color.White;

        }

        private void RemarkGrid_ChangeEdit(object sender, EventArgs e)
        {
            string _strSearchString = "";
            try
            {
                _strSearchString = RemarkGrid.Editor.Text;

                if (ogloGridListControl != null)
                {

                    if (RemarkGrid.Col == COL_REASONCODE || RemarkGrid.Col == COL_REASONDESCRIPTION)
                    {
                        string _COL_REASONCODE = "";
                        string _COL_REASONDESC = "";

                        if (RemarkGrid != null && RemarkGrid.Rows.Count > 0)
                        {
                            _COL_REASONCODE = Convert.ToString(RemarkGrid.GetData(RemarkGrid.Row, COL_REASONCODE));
                            _COL_REASONDESC = Convert.ToString(RemarkGrid.GetData(RemarkGrid.Row, COL_REASONDESCRIPTION));
                            ogloGridListControl.SelectedReasonCode = _strSearchString;

                        }
                        if (_strSearchString != "" && ogloGridListControl != null)
                        {
                            ogloGridListControl.AdvanceSearch(_strSearchString);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                ex.ToString();
            }
            finally
            {
                _strSearchString = null;
            }
        }

        private void RemarkGrid_KeyUp(object sender, KeyEventArgs e)
        {
            //int _id = 0;
            string _code = "";
            string _description = "";
            bool _isdeleted = true;

            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            RowColEventArgs e1 = null;

            try
            {

                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    #region "Enter Key"

                    if (pnlInternalControlRmk.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                            if (_IsItemSelected)
                            {

                            }
                        }
                    }


                    #endregion
                }
                else if (e.KeyCode == Keys.Down)
                {
                    e.SuppressKeyPress = true;
                    #region "Down Key"
                    if (pnlInternalControlRmk.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            ogloGridListControl.Focus();
                        }
                    }
                    #endregion
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;

                    #region "Escape Key"

                    if (pnlInternalControlRmk.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            CloseInternalControl();

                            if (RemarkGrid.RowSel > 0)
                            {

                            }
                        }
                    }

                    #endregion
                }
                else if (e.KeyCode == Keys.Delete)
                {


                    if (RemarkGrid.GetData(RemarkGrid.RowSel, COL_REASONCODE) != null)
                    {
                        _code = RemarkGrid.GetData(RemarkGrid.RowSel, COL_REASONCODE).ToString();
                    }
                    if (RemarkGrid.GetData(RemarkGrid.RowSel, COL_REASONDESCRIPTION) != null)
                    {
                        _description = RemarkGrid.GetData(RemarkGrid.RowSel, COL_REASONDESCRIPTION).ToString();
                    }

                    e2.oType = TransactionLineColumnType.None;

                    e.SuppressKeyPress = true;

                    #region "Delete Key"
                    switch (RemarkGrid.ColSel)
                    {

                        case COL_REASONCODE:
                            {

                                RemarkGrid.SetData(RemarkGrid.RowSel, RemarkGrid.ColSel, "");
                                RemarkGrid.SetData(RemarkGrid.RowSel, RemarkGrid.ColSel + 1, "");
                                e2.oType = TransactionLineColumnType.RemarkCode;

                            }
                            break;
                        case COL_REASONDESCRIPTION:
                            {

                                RemarkGrid.SetData(RemarkGrid.RowSel, COL_REASONCODE, "");
                                RemarkGrid.SetData(RemarkGrid.RowSel, COL_REASONDESCRIPTION, "");
                                e2.oType = TransactionLineColumnType.RemarkCode;

                            }
                            break;
                        case COL_REMARK_REASON_CODES:
                            {


                                RemarkGrid.SetData(RemarkGrid.RowSel, COL_REMARK_REASON_CODES, "");
                                e2.oType = TransactionLineColumnType.RemarkCode;

                            }
                            break;

                    }
                    _code = "";
                    e1 = new RowColEventArgs(RemarkGrid.RowSel, RemarkGrid.ColSel);
                    e2.code = _code;
                    e2.description = _description;
                    e2.isdeleted = true;


                    e2.code = _code;
                    e2.description = _description;
                    e2.isdeleted = _isdeleted;


                    #endregion
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                ex.ToString();
            }
            finally
            {
                e1 = null;
                e2 = null;
            }
        }

        private void RemarkGrid_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                ex.ToString();
            }
        }

        private void RemarkGrid_StartEdit(object sender, RowColEventArgs e)
        {

            try
            {
                switch (e.Col)
                {

                    case COL_REASONCODE:
                        {
                            OpenInternalControl(gloGridListControlType.RemarkCodes, "Remark Code", false, e.Row, e.Col, "");
                            string _SearchText = "";
                            if (RemarkGrid != null && RemarkGrid.Rows.Count > 0)
                            {
                                _SearchText = Convert.ToString(RemarkGrid.GetData(e.Row, COL_REASONCODE));

                                if (_SearchText != "" && ogloGridListControl != null)
                                {
                                    ogloGridListControl.AdvanceSearch(_SearchText);
                                }
                            }
                        }
                        break;
                    case COL_REASONDESCRIPTION:
                        {
                            OpenInternalControl(gloGridListControlType.RemarkCodes, "Remark Code", false, e.Row, e.Col, "");
                            string _SearchText = "";
                            if (RemarkGrid != null && RemarkGrid.Rows.Count > 0)
                            {

                                _SearchText = Convert.ToString(RemarkGrid.GetData(e.Row, COL_REASONDESCRIPTION));


                                if (_SearchText != "" && ogloGridListControl != null)
                                {
                                    ogloGridListControl.AdvanceSearch(_SearchText);
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }

        }

        #endregion

        #region "Grid List Control"

        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {

            bool allowToOpen = false;
            for (int i = 1; i < ReasonGrid.Rows.Count; i++)
            {
                if (ReasonGrid.Rows[i].Visible == true)
                {
                    allowToOpen = true;
                    break;
                }
            }




            bool _result = false;
            if (allowToOpen)
            {

                try
                {

                    if (ogloGridListControl != null)
                    {
                        CloseInternalControl();
                    }
                    if (ControlType == gloGridListControlType.RemarkCodes)
                    {
                        ogloGridListControl = new gloGridListControl(ControlType, false, pnlInternalControlRmk.Width, RowIndex, ColIndex);
                    }
                    else
                    {
                        ogloGridListControl = new gloGridListControl(ControlType, false, pnlInternalControl.Width, RowIndex, ColIndex);
                    }
                    ogloGridListControl.ItemSelected += new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                    ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                    ogloGridListControl.ControlHeader = ControlHeader;

                    ogloGridListControl.Dock = DockStyle.Fill;
                    if (SearchText != "")
                    {
                        ogloGridListControl.Search(SearchText, SearchColumn.Code);
                    }
                    ogloGridListControl.Show();


                    int _x = 0;
                    int _y = 0;
                    int _width = 0;
                    int _height = 0;
                    int _parentleft = 0;
                    int _parentwidth = 0;
                    int _diffFactor = 0;


                    if (ControlType == gloGridListControlType.RemarkCodes)
                    {
                        pnlInternalControlRmk.Controls.Add(ogloGridListControl);

                        _x = RemarkGrid.Cols[ColIndex].Left + 40;
                        _y = RemarkGrid.Rows[RowIndex].Bottom;
                        _width = pnlInternalControlRmk.Width;
                        _height = pnlInternalControlRmk.Height;



                        _parentleft = pnlInternalControlRmk.Parent.Bounds.Left;
                        _parentwidth = pnlInternalControlRmk.Parent.Bounds.Width;
                        _diffFactor = _parentwidth - _x;

                        if (_diffFactor < _width)
                        {
                            _x = pnlInternalControlRmk.Parent.Bounds.Width + (_diffFactor);
                            pnlInternalControlRmk.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                        }
                        else
                        {
                            pnlInternalControlRmk.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                        }
                        pnlInternalControlRmk.Visible = true;
                        pnlInternalControlRmk.BringToFront();
                    }
                    else
                    {
                        pnlInternalControl.Controls.Add(ogloGridListControl);
                        _x = ReasonGrid.Cols[ColIndex].Left;
                        _y = ReasonGrid.Rows[RowIndex].Bottom;
                        _width = pnlInternalControl.Width;
                        _height = pnlInternalControl.Height;



                        _parentleft = pnlInternalControl.Parent.Bounds.Left;
                        _parentwidth = pnlInternalControl.Parent.Bounds.Width;
                        _diffFactor = _parentwidth - _x;

                        if (_diffFactor < _width)
                        {
                            _x = pnlInternalControl.Parent.Bounds.Width + (_diffFactor);
                            pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                        }
                        else
                        {
                            pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                        }
                        pnlInternalControl.Visible = true;
                        pnlInternalControl.BringToFront();
                    }





                    //int _x = 0, _y = 0;
                    //if (ControlType == gloGridListControlType.RemarkCodes)
                    //{
                    //    pnlInternalControlRmk.Controls.Add(ogloGridListControl);
                    //    _x = RemarkGrid.Cols[ColIndex].Left;
                    //    _y = RemarkGrid.Rows[RowIndex].Bottom;
                    //    int _width = pnlInternalControlRmk.Width;
                    //    int _height = pnlInternalControlRmk.Height;
                    //    pnlInternalControlRmk.SetBounds(RemarkGrid.Cols[ColIndex].Left, RemarkGrid.Rows[RowIndex].Bottom, 0, 0, BoundsSpecified.Location);
                    //    pnlInternalControlRmk.Visible = true;
                    //    pnlInternalControlRmk.BringToFront();
                    //}
                    //else
                    //{
                    //    pnlInternalControl.Controls.Add(ogloGridListControl);
                    //    _x = ReasonGrid.Cols[ColIndex].Left;
                    //    _y = ReasonGrid.Rows[RowIndex].Bottom;
                    //    int _width = pnlInternalControl.Width;
                    //    int _height = pnlInternalControl.Height;
                    //    pnlInternalControl.SetBounds(ReasonGrid.Cols[ColIndex].Left, ReasonGrid.Rows[RowIndex].Bottom, 0, 0, BoundsSpecified.Location);
                    //    pnlInternalControl.Visible = true;
                    //    pnlInternalControl.BringToFront();
                    //}

                    ogloGridListControl.Focus();
                    _result = true;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                    _result = false;
                }
                finally
                {
                    RePositionInternalControl(ControlType);
                }
            }
            return _result;
        }

        public void SetReasonCodes()
        {
            String sReasonCodes = "";
            string[] ReasonCodes;
            try
            {

                if (ReasonGrid != null && ReasonGrid.Rows.Count > 1)
                {
                    for (int rIndex = 1; rIndex < ReasonGrid.Rows.Count; rIndex++)
                    {
                        if (ReasonGrid.GetData(rIndex, COL_REASONCODE) != null && Convert.ToString(ReasonGrid.GetData(rIndex, COL_REASONCODE)).Trim() != "" && Convert.ToString(ReasonGrid.GetData(rIndex, COL_REASONCODE)).Trim() != "")
                        {
                            sReasonCodes = sReasonCodes + ReasonGrid.GetData(rIndex, COL_REASONCODE) + "|";
                        }

                    }
                }
                C1.Win.C1FlexGrid.CellStyle csReasonCodes;
                try
                {
                    if (RemarkGrid.Styles.Contains("csReasonCodes"))
                    {
                        csReasonCodes = RemarkGrid.Styles["csReasonCodes"];
                    }
                    else
                    {
                        csReasonCodes = RemarkGrid.Styles.Add("csReasonCodes");
                        csReasonCodes.DataType = typeof(System.String);
                    }

                    RemarkGrid.Cols[COL_REMARK_REASON_CODES].Style = csReasonCodes;
                    csReasonCodes.ComboList = sReasonCodes;

                }
                catch
                {
                    csReasonCodes = RemarkGrid.Styles.Add("csReasonCodes");
                    csReasonCodes.DataType = typeof(System.String);
                }
                ReasonCodes = sReasonCodes.Split('|');
                if (RemarkGrid != null && RemarkGrid.Rows.Count > 1)
                {
                    for (int rIndex = 1; rIndex < RemarkGrid.Rows.Count; rIndex++)
                    {
                        if (RemarkGrid.GetData(rIndex, COL_REMARK_REASON_CODES) != null)
                        {
                            String sAssociatedReasonCode = Convert.ToString(RemarkGrid.GetData(rIndex, COL_REMARK_REASON_CODES));
                            if (!ReasonCodes.Contains(sAssociatedReasonCode))
                            {
                                RemarkGrid.SetData(rIndex, COL_REMARK_REASON_CODES, "");
                            }
                            if (ReasonCodes.Length == 2)
                            {
                                RemarkGrid.SetData(rIndex, COL_REMARK_REASON_CODES, ReasonCodes[0]);
                            }


                        }
                    }
                }

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);

            }
            finally
            {
                ReasonCodes = null;
            }
        }

        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {
                //SLR: Changed on 2/4/2014

                //if (RemarkGrid.Focused)
                // {
                for (int i = pnlInternalControlRmk.Controls.Count - 1; i >= 0; i--)
                {
                    pnlInternalControlRmk.Controls.RemoveAt(i);
                }
                pnlInternalControlRmk.Visible = false;
                pnlInternalControlRmk.SendToBack();
                //  }
                // else
                {
                    for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                    {
                        pnlInternalControl.Controls.RemoveAt(i);
                    }
                    pnlInternalControl.Visible = false;
                    pnlInternalControl.SendToBack();
                }
                if (ogloGridListControl != null)
                {
                    try
                    {
                        ogloGridListControl.ItemSelected -= new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                        ogloGridListControl.InternalGridKeyDown -= new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);

                    }
                    catch { }
                    ogloGridListControl.Dispose();
                    ogloGridListControl = null;
                }

                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                _result = false;
            }
            finally
            { }
            return _result;
        }

        private void RePositionInternalControl(gloGridListControlType ControlType)
        {
            try
            {
                if (ControlType == gloGridListControlType.RemarkCodes)
                {
                    if (RemarkGrid.Parent.Bottom - RemarkGrid.Rows[RemarkGrid.RowSel].Bottom - pnlInternalControlRmk.Height - 80 < pnlRemarkCode.Height)
                    {
                        pnlInternalControlRmk.SetBounds((RemarkGrid.Cols[RemarkGrid.ColSel].Left + RemarkGrid.ScrollPosition.X), (RemarkGrid.Rows[RemarkGrid.RowSel].Top - pnlInternalControlRmk.Height) + RemarkGrid.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                        pnlInternalControlRmk.Visible = true;
                        pnlInternalControlRmk.BringToFront();
                    }
                    else
                    {
                        pnlInternalControlRmk.SetBounds(RemarkGrid.Cols[RemarkGrid.ColSel].Left, RemarkGrid.Rows[RemarkGrid.RowSel].Bottom + RemarkGrid.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                    }
                }
                if (ControlType == gloGridListControlType.ReasonCodes)
                {
                    if (ReasonGrid.Parent.Bottom - ReasonGrid.Rows[ReasonGrid.RowSel].Bottom - pnlInternalControl.Height + 160 < pnlReason.Height)
                    {
                        pnlInternalControl.SetBounds((ReasonGrid.Cols[ReasonGrid.ColSel].Left + ReasonGrid.ScrollPosition.X), (ReasonGrid.Rows[ReasonGrid.RowSel].Top - pnlInternalControl.Height) + ReasonGrid.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                        pnlInternalControl.Visible = true;
                        pnlInternalControl.BringToFront();
                    }
                    else
                    {
                        pnlInternalControl.SetBounds(ReasonGrid.Cols[ReasonGrid.ColSel].Left, ReasonGrid.Rows[ReasonGrid.RowSel].Bottom + ReasonGrid.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }

        #endregion

        #region " Delegates "

        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        {
            #region "Custom Event"
            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            #endregion

            try
            {
                if (ogloGridListControl._ControlType == gloGridListControlType.ReasonCodes)
                {


                    int _rowIndex = 0;
                    if (ogloGridListControl.SelectedItems != null && ogloGridListControl.SelectedItems.Count > 0)
                    {
                        //...Check if code exists
                        bool _isExistsCode = false;

                        if (ReasonGrid != null && ReasonGrid.Rows.Count > 1)
                        {
                            for (int rIndex = 1; rIndex < ReasonGrid.Rows.Count; rIndex++)
                            {
                                if (rIndex != ogloGridListControl.ParentRowIndex)
                                {
                                    if (ReasonGrid.GetData(rIndex, COL_REASONCODE) != null && Convert.ToString(ReasonGrid.GetData(rIndex, COL_REASONCODE)).Trim() != ""
                                        && Convert.ToString(ReasonGrid.GetData(rIndex, COL_REASONCODE)).Trim().ToUpper() == ogloGridListControl.SelectedItems[0].Code.Trim().ToUpper())
                                    {
                                        _isExistsCode = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (_isExistsCode == false)
                        {
                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            ReasonGrid.SetData(_rowIndex, COL_REASONCODE, ogloGridListControl.SelectedItems[0].Code.Trim());
                            ReasonGrid.SetData(_rowIndex, COL_REASONDESCRIPTION, ogloGridListControl.SelectedItems[0].Description.Trim());
                            ReasonGrid.Focus();
                            ReasonGrid.Select(_rowIndex, COL_REASONAMOUNT, true);
                        }
                        else
                        {
                            MessageBox.Show("Adjustment code already exists.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            ReasonGrid.SetData(_rowIndex, COL_REASONCODE, null);
                            ReasonGrid.SetData(_rowIndex, COL_REASONDESCRIPTION, null);
                            ReasonGrid.Select(_rowIndex, COL_REASONCODE, true);
                        }
                    }
                    else
                    {
                        _rowIndex = ogloGridListControl.ParentRowIndex;
                        string code = Convert.ToString(ReasonGrid.GetData(ReasonGrid.RowSel, COL_REASONCODE));
                        ReasonGrid.SetData(_rowIndex, COL_REASONCODE, code);
                        //ReasonGrid.SetData(_rowIndex, COL_REASONDESCRIPTION, null);
                        ReasonGrid.Focus();
                        ReasonGrid.Select(_rowIndex, COL_REASONCODE, true);
                    }

                }
                if (ogloGridListControl._ControlType == gloGridListControlType.RemarkCodes)
                {
                    int _rowIndex = 0;
                    if (ogloGridListControl.SelectedItems != null && ogloGridListControl.SelectedItems.Count > 0)
                    {
                        //...Check if code exists
                        bool _isExistsCode = false;

                        if (RemarkGrid != null && RemarkGrid.Rows.Count > 1)
                        {
                            for (int rIndex = 1; rIndex < RemarkGrid.Rows.Count; rIndex++)
                            {
                                if (rIndex != ogloGridListControl.ParentRowIndex)
                                {
                                    if (RemarkGrid.GetData(rIndex, COL_REASONCODE) != null && Convert.ToString(RemarkGrid.GetData(rIndex, COL_REASONCODE)).Trim() != ""
                                        && Convert.ToString(RemarkGrid.GetData(rIndex, COL_REASONCODE)).Trim().ToUpper() == ogloGridListControl.SelectedItems[0].Code.Trim().ToUpper())
                                    {
                                        _isExistsCode = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (_isExistsCode == false)
                        {
                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            RemarkGrid.SetData(_rowIndex, COL_REASONCODE, ogloGridListControl.SelectedItems[0].Code.Trim());
                            RemarkGrid.SetData(_rowIndex, COL_REASONDESCRIPTION, ogloGridListControl.SelectedItems[0].Description.Trim());
                            RemarkGrid.Focus();

                        }
                        else
                        {
                            MessageBox.Show("Remark code already exists.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            RemarkGrid.SetData(_rowIndex, COL_REASONCODE, null);
                            RemarkGrid.SetData(_rowIndex, COL_REASONDESCRIPTION, null);
                        }
                    }
                    else
                    {
                        _rowIndex = ogloGridListControl.ParentRowIndex;
                        string code = Convert.ToString(RemarkGrid.GetData(RemarkGrid.RowSel, COL_REASONCODE));
                        RemarkGrid.SetData(_rowIndex, COL_REASONCODE, code);
                        RemarkGrid.Focus();

                    }
                }
                SetReasonCodes();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);

            }
            finally
            {
                CloseInternalControl();
            }
        }

        void ogloGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {
                CloseInternalControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
            finally
            { }
        }

        #endregion

        #region " Public & Private Methods "

        private bool SaveCodes()
        {
            bool _retVal = true;

            try
            {
                if (ReasonGrid != null && ReasonGrid.Rows.Count > 0)
                {
                    FrmDlgRst = DialogResult.OK;
                }

                if (txtStatementNotes.Text.Trim() != "")
                {
                    _StatementNote = txtStatementNotes.Text.Trim();
                    _IncludeStatmentNoteOnPrint = chkStatementNotes.Checked;
                    FrmDlgRst = DialogResult.OK;
                }

                if (txtInternalNotes.Text.Trim() != "")
                {
                    _InternalNote = txtInternalNotes.Text.Trim();
                    _IncludeInternalNoteOnPrint = chkInternalNotes.Checked;
                    FrmDlgRst = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                _retVal = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
            }

            return _retVal;
        }

        //private bool ValidateSave()
        //{
        //    bool _retVal = true;

        //    try
        //    {
        //        if (ReasonGrid != null && ReasonGrid.Rows.Count > 0)
        //        {
        //            for (int rowIndex = 1; rowIndex < ReasonGrid.Rows.Count; rowIndex++)
        //            {
        //                if (!IsValidCode(rowIndex))
        //                {
        //                    ReasonGrid.SetData(rowIndex, COL_REASONCODE, "");
        //                    _retVal = false;
        //                    ReasonGrid.Select(rowIndex, COL_REASONCODE, true);
        //                    break;
        //                }
        //                ////if (ReasonGrid.GetData(rowIndex, COL_REASONCODE) == null || Convert.ToString(ReasonGrid.GetData(rowIndex, COL_REASONCODE)).Trim() == "")
        //                ////{
        //                ////    MessageBox.Show("Please enter reason code for line number " + rowIndex.ToString() + "", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                ////    ReasonGrid.Focus();
        //                ////    pnlInternalControl.Visible = false;
        //                ////    pnlInternalControl.SendToBack();
        //                ////    ReasonGrid.SetData(rowIndex, COL_REASONCODE, "");
        //                ////    _retVal = false;
        //                ////     ReasonGrid.Select(rowIndex, COL_REASONCODE, true);
        //                ////    break;
        //                ////}
        //            }
        //        }
        //        if (_retVal != false)
        //        {
        //            if (RemarkGrid != null && RemarkGrid.Rows.Count > 0)
        //            {
        //                for (int rowIndex = 1; rowIndex < RemarkGrid.Rows.Count; rowIndex++)
        //                {
        //                    if (!IsValidRemarkCode(rowIndex))
        //                    {
        //                        RemarkGrid.SetData(rowIndex, COL_REASONCODE, "");
        //                        _retVal = false;
        //                        RemarkGrid.Select(rowIndex, COL_REASONCODE, true);
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //        if (_retVal != false)
        //        {
        //            if (ReasonGrid != null && ReasonGrid.Rows.Count > 1)
        //            {
        //                for (int rowIndex = 1; rowIndex < RemarkGrid.Rows.Count; rowIndex++)
        //                {
        //                    if (!IsValidRemarkReasonCode(rowIndex))
        //                    {
        //                        _retVal = false;
        //                        RemarkGrid.Select(rowIndex, COL_REMARK_REASON_CODES, true);
        //                        break;
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _retVal = false;
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
        //        ex.ToString();
        //    }

        //    return _retVal;
        //}

        private bool ValidateSave()
        {
            bool _retVal = true;

            try
            {
                if (ReasonGrid != null && ReasonGrid.Rows.Count > 0)
                {
                    for (int rowIndex = 1; rowIndex < ReasonGrid.Rows.Count; rowIndex++)
                    {
                        string rCodeType = Convert.ToString(ReasonGrid.GetData(rowIndex, COL_REASONCODE_TYPE));
                        //if (rCodeType == "Other")
                        //{
                        if (!IsValidCode(rowIndex))
                        {
                            ReasonGrid.SetData(rowIndex, COL_REASONCODE, "");
                            _retVal = false;
                            ReasonGrid.Select(rowIndex, COL_REASONCODE, true);
                            break;
                        }
                        
                        //}                       
                    }

                    if (ReasonCodeType != "" && ReasonCodeSetup != "Other")
                    {
                        if (_retVal != false)
                        {
                            if (CalculatePayment() != true)
                            {
                                MessageBox.Show(ReasonCodeType + " Amount distribution is not correct.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                SetFocusonAmountCell();
                                _retVal = false;
                            }
                        }

                    }
                }

                if (_retVal != false)
                {
                    if (ReasonCodeSetup == "PayerSetup")
                    {
                        return true;
                    }
                    if (RemarkGrid != null && RemarkGrid.Rows.Count > 0)
                    {
                        for (int rowIndex = 1; rowIndex < RemarkGrid.Rows.Count; rowIndex++)
                        {
                            if (!IsValidRemarkCode(rowIndex))
                            {
                                RemarkGrid.SetData(rowIndex, COL_REASONCODE, "");
                                _retVal = false;
                                RemarkGrid.Select(rowIndex, COL_REASONCODE, true);
                                break;
                            }
                        }
                    }
                }
                if (_retVal != false)
                {
                    if (ReasonGrid != null && ReasonGrid.Rows.Count > 1)
                    {
                        for (int rowIndex = 1; rowIndex < RemarkGrid.Rows.Count; rowIndex++)
                        {
                            if (!IsValidRemarkReasonCode(rowIndex))
                            {
                                _retVal = false;
                                RemarkGrid.Select(rowIndex, COL_REMARK_REASON_CODES, true);
                                break;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _retVal = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                ex.ToString();
            }

            return _retVal;
        }

        private bool IsValidCode(int rowIndex)
        {
            try
            {
                CloseInternalControl();

                CellStyle cs = ReasonGrid.Cols[COL_REASONCODE].Style;

                if ((ReasonGrid.GetData(rowIndex, COL_REASONCODE) != null) || (Convert.ToString(ReasonGrid.GetData(rowIndex, COL_REASONCODE)).Trim() == ""))
                {
                    string _str = Convert.ToString(ReasonGrid.GetData(rowIndex, COL_REASONCODE)).Trim().Replace('-', ' ').Replace(',', ' ').Replace('(', ' ').Replace(')', ' ').Trim();
                    string _strType = Convert.ToString(ReasonGrid.GetData(rowIndex, COL_REASONCODE_TYPE));
                    bool _isValidCode = false;

                    if (_str != "")
                    {
                        _str = Convert.ToString(ReasonGrid.GetData(rowIndex, COL_REASONCODE)).Trim();
                        if (ReasonGrid.Editor != null && ReasonGrid.IsCellCursor(rowIndex,COL_REASONCODE)) 
                            _str = ReasonGrid.Editor.Text;
                        _isValidCode = InsurancePayment.IsValidReasonCode(_str);
                                               
                        if (_isValidCode == false)
                        {
                            //if (ReasonCodeSetup == "PayerSetup")
                            //{
                            //    return true;
                            //}
                            if (ReasonCodeType != _strType)
                            {
                                return true;
                            }
                            MessageBox.Show("Please select a valid code.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (ReasonGrid.Editor != null)
                                ReasonGrid.Editor.Text = "";
                            ReasonGrid.SetData(rowIndex, COL_REASONCODE, null);
                            ReasonGrid.SetData(rowIndex, COL_REASONDESCRIPTION, null);
                            ReasonGrid.Select(rowIndex, COL_REASONCODE, true);

                            return false;
                        }
                        if (ReasonGrid.Editor != null && ReasonGrid.Editor.Text != "" && ReasonGrid.IsCellCursor(rowIndex,COL_REASONCODE))
                            ReasonGrid.Editor.Text = Convert.ToString(ReasonGrid.GetData(ReasonGrid.Row, COL_REASONCODE));
                    }
                    else
                    {
                        MessageBox.Show("Please enter reason code.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ReasonGrid.SetData(rowIndex, COL_REASONCODE, null);
                        ReasonGrid.SetData(rowIndex, COL_REASONDESCRIPTION, null);
                        ReasonGrid.Select(rowIndex, COL_REASONCODE, true);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                return false;
            }
        }

        private bool IsValidRemarkCode(int rowIndex)
        {
            try
            {
                CloseInternalControl();

                CellStyle cs = RemarkGrid.Cols[COL_REASONCODE].Style;

                if ((RemarkGrid.GetData(rowIndex, COL_REASONCODE) != null) || (Convert.ToString(RemarkGrid.GetData(rowIndex, COL_REASONCODE)).Trim() == ""))
                {
                    string _str = Convert.ToString(RemarkGrid.GetData(rowIndex, COL_REASONCODE)).Trim().Replace('-', ' ').Replace(',', ' ').Replace('(', ' ').Replace(')', ' ').Trim();
                    bool _isValidCode = false;

                    if (_str != "")
                    {
                        _str = Convert.ToString(RemarkGrid.GetData(rowIndex, COL_REASONCODE)).Trim();
                        if (RemarkGrid.Editor != null)
                            _str = RemarkGrid.Editor.Text;
                        _isValidCode = InsurancePayment.IsValidRemarkCode(_str);

                        if (_isValidCode == false)
                        {
                            MessageBox.Show("Please select a valid code.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (RemarkGrid.Editor != null)
                                RemarkGrid.Editor.Text = "";
                            RemarkGrid.SetData(rowIndex, COL_REASONCODE, null);
                            RemarkGrid.SetData(rowIndex, COL_REASONDESCRIPTION, null);
                            RemarkGrid.Select(rowIndex, COL_REASONCODE, true);

                            return false;
                        }
                        if (RemarkGrid.Editor != null && RemarkGrid.Editor.Text != "")
                            RemarkGrid.Editor.Text = Convert.ToString(RemarkGrid.GetData(rowIndex, COL_REASONCODE));
                    }
                    else
                    {
                        MessageBox.Show("Please enter remark code for line number " + rowIndex.ToString() + "", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        RemarkGrid.SetData(rowIndex, COL_REASONCODE, null);
                        RemarkGrid.SetData(rowIndex, COL_REASONDESCRIPTION, null);
                        RemarkGrid.Select(rowIndex, COL_REASONCODE, true);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                return false;
            }
        }

        private bool IsValidRemarkReasonCode(int rowIndex)
        {

            try
            {

                CloseInternalControl();

                //CellStyle cs = RemarkGrid.Cols[COL_REMARK_REASON_CODES].Style;

                if ((RemarkGrid.GetData(rowIndex, COL_REMARK_REASON_CODES) != null) || (Convert.ToString(RemarkGrid.GetData(rowIndex, COL_REMARK_REASON_CODES)).Trim() == ""))
                {
                    string _str = Convert.ToString(RemarkGrid.GetData(rowIndex, COL_REMARK_REASON_CODES)).Trim().Replace('-', ' ').Replace(',', ' ').Replace('(', ' ').Replace(')', ' ').Trim();
                    //  bool _isValidCode = false;

                    if (_str == "")
                    {
                        MessageBox.Show("Please enter reason code.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //RemarkGrid.SetData(rowIndex, COL_REASONCODE, null);
                        //RemarkGrid.SetData(rowIndex, COL_REASONDESCRIPTION, null);
                        RemarkGrid.Select(rowIndex, COL_REMARK_REASON_CODES, true);
                        return false;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                return false;
            }

        }

        private bool CalculatePayment()
        {
            decimal CalculatedPayment = 0;
            for (int i = 1; i < ReasonGrid.Rows.Count; i++)
            {
                if (ReasonCodeType != "Other")
                {
                    if (Convert.ToString(ReasonGrid.GetData(i, COL_REASONCODE_TYPE)) == ReasonCodeType)
                    {
                        CalculatedPayment = CalculatedPayment + Convert.ToDecimal(ReasonGrid.GetData(i, COL_REASONAMOUNT));
                    }
                }
                else
                {
                    CalculatedPayment = CalculatedPayment + Convert.ToDecimal(ReasonGrid.GetData(i, COL_REASONAMOUNT));
                }
            }

            c1ReasonAmountTotal.SetData(c1ReasonAmountTotal.Rows.Count - 1, COL_REASONCODE_TYPE, "");
            c1ReasonAmountTotal.SetData(c1ReasonAmountTotal.Rows.Count - 1, COL_REASONCODE, "");
            c1ReasonAmountTotal.SetData(c1ReasonAmountTotal.Rows.Count - 1, COL_REASONDESCRIPTION, "Total :");
            c1ReasonAmountTotal.SetData(c1ReasonAmountTotal.Rows.Count - 1, COL_REASONAMOUNT, CalculatedPayment);
            PendingPayment = _Payment - CalculatedPayment;
            //lblPendingAmount.Text = "$" + "" + Convert.ToString(PendingPayment);
            lblPendingAmount.Text = "$" + "" + ((decimal)(PendingPayment)).ToString("N2");


            if (_Payment == Convert.ToDecimal(CalculatedPayment))
            {
                lblAmount.ForeColor = Color.Green;
                return true;
            }
            else
            {
                lblAmount.ForeColor = Color.Maroon;
                return false;
            }
        }

        private void CalculateReasonamountTotal()
        {
            decimal CalculatedPayment = 0;
            for (int i = 1; i < ReasonGrid.Rows.Count; i++)
            {

                CalculatedPayment = CalculatedPayment + Convert.ToDecimal(ReasonGrid.GetData(i, COL_REASONAMOUNT));
            }

            c1ReasonAmountTotal.SetData(c1ReasonAmountTotal.Rows.Count - 1, COL_REASONCODE_TYPE, "");
            c1ReasonAmountTotal.SetData(c1ReasonAmountTotal.Rows.Count - 1, COL_REASONCODE, "");
            c1ReasonAmountTotal.SetData(c1ReasonAmountTotal.Rows.Count - 1, COL_REASONDESCRIPTION, "Total :");
            c1ReasonAmountTotal.SetData(c1ReasonAmountTotal.Rows.Count - 1, COL_REASONAMOUNT, CalculatedPayment);

        }

        private void SetFocusonAmountCell()
        {
            ReasonGrid.Focus();
            for (int i = 1; i < ReasonGrid.Rows.Count; i++)
            {
                if (ReasonGrid.Rows[i].Visible)
                {
                    ReasonGrid.Select(i, COL_REASONAMOUNT, true);
                    break;
                }
            }
        }

        #endregion " Public & Private Methods "

        #region " C1 Grid Design Method "

        private void SetComboBoxCellStyle()
        {
            CellStyle csStdGroupCode;
            if (!ReasonGrid.Styles.Contains("csGroupCodes"))
            {
                csStdGroupCode = ReasonGrid.Styles.Add("csGroupCodes");
                csStdGroupCode.ComboList = "W/O|Copay|Deduct|Co-ins|Withhold|Other";
            }
        }

        public void DesignTotalsGrid()
        {
            c1ReasonAmountTotal.Cols.Count = COL_COUNT;
            c1ReasonAmountTotal.Rows.Count = 1;
            c1ReasonAmountTotal.Cols.Fixed = 0;


            c1ReasonAmountTotal.Rows.Fixed = 0;
            c1ReasonAmountTotal.ScrollBars = ScrollBars.None;

            c1ReasonAmountTotal.Cols[COL_ID].Visible = false;
            c1ReasonAmountTotal.Cols[COL_CLAIMNO].Visible = false;
            c1ReasonAmountTotal.Cols[COL_EOBPAYMENTID].Visible = false;
            c1ReasonAmountTotal.Cols[COL_EOBID].Visible = false;
            c1ReasonAmountTotal.Cols[COL_EOBPAYMENTDETAILID].Visible = false;
            c1ReasonAmountTotal.Cols[COL_BILLINGTRANSACTIONID].Visible = false;
            c1ReasonAmountTotal.Cols[COL_BILLINGTRANSACTIONDETAILID].Visible = false;
            c1ReasonAmountTotal.Cols[COL_CLINICID].Visible = false;
            c1ReasonAmountTotal.Cols[COL_SUBCLAIMNO].Visible = false;
            c1ReasonAmountTotal.Cols[COL_TRACK_BILLINGTRANSACTIONID].Visible = false;
            c1ReasonAmountTotal.Cols[COL_TRACK_BILLINGTRANSACTIONDETAILID].Visible = false;
            c1ReasonAmountTotal.Cols[COL_REMARK_REASON_CODES].Visible = false;
            c1ReasonAmountTotal.Cols[COL_REASONCODE_TYPE].Visible = true;
            c1ReasonAmountTotal.Cols[COL_REASONCODE].Visible = true;
            c1ReasonAmountTotal.Cols[COL_REASONDESCRIPTION].Visible = true;
            c1ReasonAmountTotal.Cols[COL_REASONAMOUNT].Visible = true;


            c1ReasonAmountTotal.Cols[COL_ID].Width = 0;
            c1ReasonAmountTotal.Cols[COL_CLAIMNO].Width = 0;
            c1ReasonAmountTotal.Cols[COL_EOBPAYMENTID].Width = 0;
            c1ReasonAmountTotal.Cols[COL_EOBID].Width = 0;
            c1ReasonAmountTotal.Cols[COL_EOBPAYMENTDETAILID].Width = 0;
            c1ReasonAmountTotal.Cols[COL_BILLINGTRANSACTIONID].Width = 0;
            c1ReasonAmountTotal.Cols[COL_BILLINGTRANSACTIONDETAILID].Width = 0;
            c1ReasonAmountTotal.Cols[COL_CLINICID].Width = 0;
            c1ReasonAmountTotal.Cols[COL_SUBCLAIMNO].Width = 0;
            c1ReasonAmountTotal.Cols[COL_TRACK_BILLINGTRANSACTIONID].Width = 0;
            c1ReasonAmountTotal.Cols[COL_TRACK_BILLINGTRANSACTIONDETAILID].Width = 0;
            c1ReasonAmountTotal.Cols[COL_REASONCODE_TYPE].Width = 80;
            c1ReasonAmountTotal.Cols[COL_REASONCODE].Width = 80;
            c1ReasonAmountTotal.Cols[COL_REASONDESCRIPTION].Width = 350;
            c1ReasonAmountTotal.Cols[COL_REASONAMOUNT].Width = 80;



            c1ReasonAmountTotal.Cols[COL_ID].AllowEditing = false;
            c1ReasonAmountTotal.Cols[COL_CLAIMNO].AllowEditing = false;
            c1ReasonAmountTotal.Cols[COL_EOBPAYMENTID].AllowEditing = false;
            c1ReasonAmountTotal.Cols[COL_EOBID].AllowEditing = false;
            c1ReasonAmountTotal.Cols[COL_EOBPAYMENTDETAILID].AllowEditing = false;
            c1ReasonAmountTotal.Cols[COL_BILLINGTRANSACTIONID].AllowEditing = false;
            c1ReasonAmountTotal.Cols[COL_BILLINGTRANSACTIONDETAILID].AllowEditing = false;
            c1ReasonAmountTotal.Cols[COL_REASONCODE_TYPE].AllowEditing = false;
            c1ReasonAmountTotal.Cols[COL_REASONCODE].AllowEditing = false;
            c1ReasonAmountTotal.Cols[COL_REASONDESCRIPTION].AllowEditing = false;
            c1ReasonAmountTotal.Cols[COL_REASONAMOUNT].AllowEditing = false;
            c1ReasonAmountTotal.Cols[COL_CLINICID].AllowEditing = false;
            c1ReasonAmountTotal.Cols[COL_SUBCLAIMNO].AllowEditing = false;
            c1ReasonAmountTotal.Cols[COL_TRACK_BILLINGTRANSACTIONID].AllowEditing = false;
            c1ReasonAmountTotal.Cols[COL_TRACK_BILLINGTRANSACTIONDETAILID].AllowEditing = false;


            c1ReasonAmountTotal.Cols[COL_REASONCODE_TYPE].TextAlign = TextAlignEnum.RightCenter;
            c1ReasonAmountTotal.Cols[COL_REASONCODE].TextAlign = TextAlignEnum.RightCenter;
            c1ReasonAmountTotal.Cols[COL_REASONDESCRIPTION].TextAlign = TextAlignEnum.RightCenter;
            c1ReasonAmountTotal.Cols[COL_REASONAMOUNT].TextAlign = TextAlignEnum.RightCenter;

            C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = FlexGrid.Styles.Add("cs_CurrencyStyle");
            try
            {
                if (c1ReasonAmountTotal.Styles.Contains("cs_CurrencyStyle"))
                {
                    csCurrencyStyle = c1ReasonAmountTotal.Styles["cs_CurrencyStyle"];
                }
                else
                {
                    csCurrencyStyle = c1ReasonAmountTotal.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.ForeColor = Color.Maroon;
                }

            }
            catch
            {
                csCurrencyStyle = c1ReasonAmountTotal.Styles.Add("cs_CurrencyStyle");
                csCurrencyStyle.DataType = typeof(System.Decimal);
                csCurrencyStyle.Format = "c";
                csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                csCurrencyStyle.ForeColor = Color.Maroon;
            }
            c1ReasonAmountTotal.Cols[COL_REASONDESCRIPTION].Style = csCurrencyStyle;
            c1ReasonAmountTotal.Cols[COL_REASONAMOUNT].Style = csCurrencyStyle;
            c1ReasonAmountTotal.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            c1ReasonAmountTotal.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
            c1ReasonAmountTotal.Styles[C1.Win.C1FlexGrid.CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
            c1ReasonAmountTotal.Styles[C1.Win.C1FlexGrid.CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
            c1ReasonAmountTotal.Styles[C1.Win.C1FlexGrid.CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);


        }

        public void DesignGrid(C1.Win.C1FlexGrid.C1FlexGrid FlexGrid)
        {
            try
            {
                //_isFormLoading = true;
                string sGridType = Convert.ToString(((System.Windows.Forms.Control)(FlexGrid)).Name);
                FlexGrid.Redraw = true;
                FlexGrid.ScrollBars = ScrollBars.None;
                FlexGrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;

                FlexGrid.Clear();
                FlexGrid.Cols.Count = COL_COUNT;
                FlexGrid.Rows.Count = 1;
                FlexGrid.Rows.Fixed = 1;
                FlexGrid.Cols.Fixed = 0;
                FlexGrid.ScrollBars = ScrollBars.None;
                FlexGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;



                #region " Set Headers "

                FlexGrid.SetData(0, COL_ID, "Id");
                FlexGrid.SetData(0, COL_CLAIMNO, "ClaimNo");
                FlexGrid.SetData(0, COL_EOBPAYMENTID, "EOBPaymentID");
                FlexGrid.SetData(0, COL_EOBID, "EOBID");
                FlexGrid.SetData(0, COL_EOBPAYMENTDETAILID, "EOBPayDtlID");
                FlexGrid.SetData(0, COL_BILLINGTRANSACTIONID, "BLTransactionID");
                FlexGrid.SetData(0, COL_BILLINGTRANSACTIONDETAILID, "BLTransactionDtlID");
                FlexGrid.SetData(0, COL_REASONCODE, "Code");
                FlexGrid.SetData(0, COL_REASONDESCRIPTION, "Description");
                FlexGrid.SetData(0, COL_REASONAMOUNT, "Amount");
                FlexGrid.SetData(0, COL_CLINICID, "ClinicId");
                FlexGrid.SetData(0, COL_SUBCLAIMNO, "SubClaimNo");
                FlexGrid.SetData(0, COL_TRACK_BILLINGTRANSACTIONID, "TrackBLTransactionID");
                FlexGrid.SetData(0, COL_TRACK_BILLINGTRANSACTIONDETAILID, "TrackBLTransactionDtlID");

                FlexGrid.Cols[COL_ID].Name = "Id";
                FlexGrid.Cols[COL_CLAIMNO].Name = "ClaimNo";
                FlexGrid.Cols[COL_EOBPAYMENTID].Name = "EOBPaymentID";
                FlexGrid.Cols[COL_EOBID].Name = "EOBID";
                FlexGrid.Cols[COL_EOBPAYMENTDETAILID].Name = "EOBPayDtlID";
                FlexGrid.Cols[COL_BILLINGTRANSACTIONID].Name = "BLTransactionID";
                FlexGrid.Cols[COL_BILLINGTRANSACTIONDETAILID].Name = "BLTransactionDtlID";
                FlexGrid.Cols[COL_REASONCODE].Name = "Code";
                FlexGrid.Cols[COL_REASONDESCRIPTION].Name = "Description";
                FlexGrid.Cols[COL_REASONAMOUNT].Name = "Amount";
                FlexGrid.Cols[COL_CLINICID].Name = "ClinicId";

                FlexGrid.Cols[COL_SUBCLAIMNO].Name = "SubClaimNo";
                FlexGrid.Cols[COL_TRACK_BILLINGTRANSACTIONID].Name = "TrackBLTransactionID";
                FlexGrid.Cols[COL_TRACK_BILLINGTRANSACTIONDETAILID].Name = "TrackBLTransactionDtlID";

                FlexGrid.Cols[COL_REMARK_REASON_CODES].Name = "RemarkReasonCodes";
                FlexGrid.Cols[COL_REMARK_REASON_CODES].Visible = true;
                FlexGrid.Cols[COL_REMARK_REASON_CODES].Width = 100;
                FlexGrid.Cols[COL_REMARK_REASON_CODES].DataType = typeof(System.String);
                FlexGrid.SetData(0, COL_REMARK_REASON_CODES, "Reason Codes");

                FlexGrid.SetData(0, COL_REASONCODE_TYPE, "Type");
                FlexGrid.Cols[COL_REASONCODE_TYPE].Name = "ReasonCodeType";
                FlexGrid.Cols[COL_REASONCODE_TYPE].DataType = typeof(System.String);
                //FlexGrid.Cols[COL_REASONCODE_TYPE].AllowEditing = false;



                if (sGridType == "RemarkGrid")
                {
                    FlexGrid.Cols[COL_REMARK_REASON_CODES].Visible = true;
                    FlexGrid.Cols[COL_REASONCODE_TYPE].Visible = false;
                }
                else
                {
                    FlexGrid.Cols[COL_REMARK_REASON_CODES].Visible = false;
                    FlexGrid.Cols[COL_REASONCODE_TYPE].Visible = true;
                }

                //FlexGrid.Cols[COL_STATEMENTNOTES].Name = "StatementNotes";
                //FlexGrid.Cols[COL_ISSTATEMENTNOTESONPRINT].Name = "IsStatmentNoteOnPrint";
                //FlexGrid.Cols[COL_INTERNALNOTES].Name = "InternalNotes";
                //FlexGrid.Cols[COL_ISINTERNALNOTESONPRINT].Name = "IsInternalNoteOnPrint";

                #endregion " Set Headers "

                #region " Show/Hide "

                FlexGrid.Cols[COL_ID].Visible = false;
                FlexGrid.Cols[COL_CLAIMNO].Visible = false;
                FlexGrid.Cols[COL_EOBPAYMENTID].Visible = false;
                FlexGrid.Cols[COL_EOBID].Visible = false;
                FlexGrid.Cols[COL_EOBPAYMENTDETAILID].Visible = false;
                FlexGrid.Cols[COL_BILLINGTRANSACTIONID].Visible = false;
                FlexGrid.Cols[COL_BILLINGTRANSACTIONDETAILID].Visible = false;
                FlexGrid.Cols[COL_REASONCODE].Visible = true;
                FlexGrid.Cols[COL_REASONDESCRIPTION].Visible = true;
                FlexGrid.Cols[COL_REASONAMOUNT].Visible = true;
                FlexGrid.Cols[COL_CLINICID].Visible = false;

                FlexGrid.Cols[COL_SUBCLAIMNO].Visible = false;
                FlexGrid.Cols[COL_TRACK_BILLINGTRANSACTIONID].Visible = false;
                FlexGrid.Cols[COL_TRACK_BILLINGTRANSACTIONDETAILID].Visible = false;

                //FlexGrid.Cols[COL_STATEMENTNOTES].Visible = false;
                //FlexGrid.Cols[COL_ISSTATEMENTNOTESONPRINT].Visible = false;
                //FlexGrid.Cols[COL_INTERNALNOTES].Visible = false;
                //FlexGrid.Cols[COL_ISINTERNALNOTESONPRINT].Visible = false;

                #endregion " Show/Hide "

                #region " Width "

                FlexGrid.Cols[COL_ID].Width = 0;
                FlexGrid.Cols[COL_CLAIMNO].Width = 0;
                FlexGrid.Cols[COL_EOBPAYMENTID].Width = 0;
                FlexGrid.Cols[COL_EOBID].Width = 0;
                FlexGrid.Cols[COL_EOBPAYMENTDETAILID].Width = 0;
                FlexGrid.Cols[COL_BILLINGTRANSACTIONID].Width = 0;
                FlexGrid.Cols[COL_BILLINGTRANSACTIONDETAILID].Width = 0;
                if (sGridType == "RemarkGrid")
                {
                    FlexGrid.Cols[COL_REASONCODE_TYPE].Width = 0;
                    FlexGrid.Cols[COL_REASONCODE].Width = 100;
                    FlexGrid.Cols[COL_REASONDESCRIPTION].Width = 370;
                    FlexGrid.Cols[COL_REASONAMOUNT].Width = 100;
                }
                else
                {
                    FlexGrid.Cols[COL_REASONCODE_TYPE].Width = 80;
                    FlexGrid.Cols[COL_REASONCODE].Width = 80;
                    FlexGrid.Cols[COL_REASONDESCRIPTION].Width = 350;
                    FlexGrid.Cols[COL_REASONAMOUNT].Width = 80;
                }
                FlexGrid.Cols[COL_CLINICID].Width = 0;
                FlexGrid.Cols[COL_SUBCLAIMNO].Width = 0;
                FlexGrid.Cols[COL_TRACK_BILLINGTRANSACTIONID].Width = 0;
                FlexGrid.Cols[COL_TRACK_BILLINGTRANSACTIONDETAILID].Width = 0;

                //FlexGrid.Cols[COL_STATEMENTNOTES].Width = 0;
                //FlexGrid.Cols[COL_ISSTATEMENTNOTESONPRINT].Width = 0;
                //FlexGrid.Cols[COL_INTERNALNOTES].Width = 0;
                //FlexGrid.Cols[COL_ISINTERNALNOTESONPRINT].Width = 0;

                #endregion " Width "

                #region " DataType "

                FlexGrid.Cols[COL_ID].DataType = typeof(System.Int64);
                FlexGrid.Cols[COL_CLAIMNO].DataType = typeof(System.Int64);
                FlexGrid.Cols[COL_EOBPAYMENTID].DataType = typeof(System.Int64);
                FlexGrid.Cols[COL_EOBID].DataType = typeof(System.Int64);
                FlexGrid.Cols[COL_EOBPAYMENTDETAILID].DataType = typeof(System.Int64);
                FlexGrid.Cols[COL_BILLINGTRANSACTIONID].DataType = typeof(System.Int64);
                FlexGrid.Cols[COL_BILLINGTRANSACTIONDETAILID].DataType = typeof(System.Int64);
                FlexGrid.Cols[COL_REASONCODE].DataType = typeof(System.String);
                FlexGrid.Cols[COL_REASONDESCRIPTION].DataType = typeof(System.String);
                FlexGrid.Cols[COL_REASONAMOUNT].DataType = typeof(System.Decimal);
                FlexGrid.Cols[COL_CLINICID].DataType = typeof(System.Int64);
                FlexGrid.Cols[COL_SUBCLAIMNO].DataType = typeof(System.String);
                FlexGrid.Cols[COL_TRACK_BILLINGTRANSACTIONID].DataType = typeof(System.Int64);
                FlexGrid.Cols[COL_TRACK_BILLINGTRANSACTIONDETAILID].DataType = typeof(System.Int64);

                //FlexGrid.Cols[COL_STATEMENTNOTES].DataType = typeof(System.Int64);
                //FlexGrid.Cols[COL_ISSTATEMENTNOTESONPRINT].DataType = typeof(System.Int64);
                //FlexGrid.Cols[COL_INTERNALNOTES].DataType = typeof(System.Int64);
                //FlexGrid.Cols[COL_ISINTERNALNOTESONPRINT].DataType = typeof(System.Int64);

                #endregion " DataType "

                #region " Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = FlexGrid.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (FlexGrid.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = FlexGrid.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = FlexGrid.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

                    }

                }
                catch
                {
                    csCurrencyStyle = FlexGrid.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

                }


                FlexGrid.Cols[COL_REASONAMOUNT].Style = csCurrencyStyle;

                C1.Win.C1FlexGrid.CellStyle csEditableActionStatus;// = FlexGrid.Styles.Add("cs_ReasonCodes");
                try
                {
                    if (FlexGrid.Styles.Contains("cs_ReasonCodes"))
                    {
                        csEditableActionStatus = FlexGrid.Styles["cs_ReasonCodes"];
                    }
                    else
                    {
                        csEditableActionStatus = FlexGrid.Styles.Add("cs_ReasonCodes");
                        csEditableActionStatus.DataType = typeof(System.String);
                        csEditableActionStatus.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableActionStatus.BackColor = Color.White;

                    }

                }
                catch
                {
                    csEditableActionStatus = FlexGrid.Styles.Add("cs_ReasonCodes");
                    csEditableActionStatus.DataType = typeof(System.String);
                    csEditableActionStatus.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableActionStatus.BackColor = Color.White;

                }

                string _comboList = "";
                EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new EOBPayment.gloEOBPaymentInsurance(_databaseConnection);

                if (sGridType == "ReasonGrid")
                {
                    _comboList = ogloEOBPaymentInsurance.GetReasonCodes();
                }

                if (sGridType == "RemarkGrid")
                {
                    _comboList = ogloEOBPaymentInsurance.GetRemarkCodes();
                    FlexGrid.Cols[COL_REASONAMOUNT].Visible = false;

                    C1.Win.C1FlexGrid.CellStyle csReasonCodes;// = FlexGrid.Styles.Add("cs_ReasonCodes");
                    try
                    {
                        if (FlexGrid.Styles.Contains("csReasonCodes"))
                        {
                            csReasonCodes = FlexGrid.Styles["csReasonCodes"];
                        }
                        else
                        {
                            csReasonCodes = FlexGrid.Styles.Add("csReasonCodes");
                            csReasonCodes.DataType = typeof(System.String);
                            //csReasonCodes.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                            //csReasonCodes.BackColor = Color.White;

                        }

                        FlexGrid.Cols[COL_REMARK_REASON_CODES].Style = csReasonCodes;

                    }
                    catch
                    {
                        csReasonCodes = FlexGrid.Styles.Add("csReasonCodes");
                        csReasonCodes.DataType = typeof(System.String);
                        //csReasonCodes.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        //csReasonCodes.BackColor = Color.White;

                    }
                    FlexGrid.Cols[COL_REASONDESCRIPTION].Width = 400;
                }

                ogloEOBPaymentInsurance.Dispose();
                csEditableActionStatus.ComboList = _comboList;

                //FlexGrid.Cols[COL_REASONCODE].Style = csEditableActionStatus;

                //FlexGrid.Cols[COL_REASONDESCRIPTION].AllowEditing = false;
                FlexGrid.Cols[COL_REASONDESCRIPTION].AllowEditing = false;
                FlexGrid.Cols[COL_REASONCODE].AllowEditing = true;
                //FlexGrid.Cols[COL_REASONAMOUNT].AllowEditing = true;
                //FlexGrid.Cols[COL_REASONDESCRIPTION].AllowEditing = false;
                //FlexGrid.Cols[COL_REASONCODE].AllowEditing = true;
                //FlexGrid.Cols[COL_REASONAMOUNT].AllowEditing = true;


                FlexGrid.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
                FlexGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
                FlexGrid.Styles[C1.Win.C1FlexGrid.CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                FlexGrid.Styles[C1.Win.C1FlexGrid.CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                FlexGrid.Styles[C1.Win.C1FlexGrid.CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                #endregion " Styles "

                FlexGrid.ScrollBars = ScrollBars.Vertical;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                ex.ToString();
            }
            finally
            {
                FlexGrid.Redraw = true;
                //_isFormLoading = false;
            }
        }

        #endregion " C1 Grid Design Method "

    }
}
