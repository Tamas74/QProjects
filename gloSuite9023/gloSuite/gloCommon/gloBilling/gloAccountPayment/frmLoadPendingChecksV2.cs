using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloDateMaster;
using gloSettings;
using gloBilling;

namespace gloAccountsV2
{
    public partial class frmLoadPendingChecksV2 : Form
    {
        #region  " Variable Declarations "

        public DialogResult _frmDlgRst = DialogResult.None;
        private bool _showForm = true;
      //  private bool _isFormLoading = false;

        #endregion " Variable Declarations "

        #region " Property Procedures "
        
        private string _checkNo = "";
        public string CheckNo
        {
            get { return _checkNo; }
            set { _checkNo = value; }
        }

        private Int64 _checkDate = 0;
        public Int64 CheckDate
        {
            get { return _checkDate; }
            set { _checkDate = value; }
        }

        private decimal _checkAmount = 0;
        public decimal CheckAmount
        {
            get { return _checkAmount; }
            set { _checkAmount = value; }
        }

        private decimal _pendingAmount = 0;
        public decimal PendingAmount
        {
            get { return _pendingAmount; }
            set { _pendingAmount = value; }
        }

        private Int64 _multiTranId = 0;
        public Int64 MultipleTransactionID
        {
            get { return _multiTranId; }
            set { _multiTranId = value; }
        }

        private Int64 _eobPaymentId = 0;
        public Int64 EOBPaymentID
        {
            get { return _eobPaymentId; }
            set { _eobPaymentId = value; }
        }

        private Int64 _InsuranceCompanyId = 0;
        public Int64 InsuranceCompanyID
        {
            get { return _InsuranceCompanyId; }
            set
            {
                _InsuranceCompanyId = value;
                InsuranceCompanyName = string.Empty;
            }
        }

        private string _InsuranceCompanyName = "";
        public string InsuranceCompanyName
        {
            get { return _InsuranceCompanyName; }
            set
            {
                _InsuranceCompanyName = value;
                lblInsCompany.Text = _InsuranceCompanyName;
            }
        }

        public Int64 _selectedUserID = 0;
        public Int64 SelectedUserID
        {
            get
            {
                if (cmbUsers.SelectedIndex > 0)
                { _selectedUserID = Convert.ToInt64(cmbUsers.SelectedValue); }
                else
                { _selectedUserID = 0; }

                return _selectedUserID;
            }
            set { _selectedUserID = value; }
        }

        public Int64 _closeDate = 0;
        public Int64 CloseDate
        {
            get
            {
                if (mskCloseDate.MaskCompleted)
                { _closeDate = gloDate.DateAsNumber(mskCloseDate.Text); }
                else
                { _closeDate = 0; }

                return _closeDate;
            }
            set
            {
                _closeDate = value;

                if (_closeDate != 0)
                { mskCloseDate.Text = gloDate.DateAsDate(_closeDate).Date.ToString("MM/dd/yyyy"); }
                else
                { mskCloseDate.ResetText(); }
            }
        }

        public bool ShowForm
        {
            get { return _showForm; }
            set { _showForm = value; }
        }

        // Added by Pankaj 
        // Will return whether to show Completed Checks in to the list or not
        public bool IsShowCompleted
        {
            get 
            {
                if (tsb_ShowHideZeroCheck.Tag.ToString() == "Show") 
                { return true; }
                else
                { return false; }
            }
        }

        #endregion

        #region " Constructor "

        public frmLoadPendingChecksV2()
        {
            InitializeComponent();
        }

        #endregion " Constructor "

        #region "Columns"

        const int COL_CREDITID = 0;
        const int COL_PAYERID = 1;
        const int COL_USERID = 2;
        const int COL_INSCMP = 3;
        const int COL_CHECKNO = 4;
        const int COL_CHECKDATE = 5;
        const int COL_CLOSEDATE = 6;
        const int COL_CHKAMT  = 7;
        const int COL_CHECKREM = 8;
        const int COL_COUNT = 9;

       
    

        #endregion

        #region " Form Load "

        private void frmBillingCheckDiff_Load(object sender, EventArgs e)
        {
            //_isFormLoading = true;

           

            try
            {
                gloC1FlexStyle.Style(c1PendingCheck, true);
                tsb_ShowHideZeroCheck.Tag = "Show";
                tsb_ShowHideZeroCheck.Text = "&Show Completed";
                tsb_ShowHideZeroCheck.ToolTipText = "Show Completed Check";
                tsb_ShowHideZeroCheck.Image = global::gloBilling.Properties.Resources.Show_Completed;

                pnlPleasewait.Show();  

                // Fill User list & set current user as default one
                FillUserList();
               
            }
            catch (Exception EX)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), true);
            }
            finally
            {
                //_isFormLoading = false;
            }
        }


        private void frmLoadPendingChecksV2_Shown(object sender, EventArgs e)
        {
            
           
            //this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            // Load pending check list as per default filters set
            FillPendingCheck(false);

            //this.Cursor = Cursors.Default;
           
            pnlPleasewait.Hide();  
        }

        #endregion " Form Load "

        #region " Private & Public Methods "

        void FillUserList()
        {
            DataTable _dtUsers = new DataTable();
            _dtUsers = gloGlobal.gloPMMasters.GetUsers();
            if (_dtUsers.Rows.Count != 0)
            {
                DataRow dr = _dtUsers.NewRow();
                dr["sLoginName"] = "";
                _dtUsers.Rows.InsertAt(dr, 0);
                _dtUsers.AcceptChanges();

                cmbUsers.DataSource = _dtUsers.Copy();
                cmbUsers.DisplayMember = "sLoginName";
                cmbUsers.ValueMember = "nUserID";

                if (AppSettings.UserID != 0)
                { cmbUsers.SelectedValue = AppSettings.UserID; }
                else
                { cmbUsers.SelectedValue = 1; }
            }
            else
            {
                DataRow dr = _dtUsers.NewRow();
                dr["sLoginName"] = "";
                _dtUsers.Rows.InsertAt(dr, 0);
                _dtUsers.AcceptChanges();
            }
        }

        private void FillPendingCheck(Boolean bShowHidden)
        {
            if (!ValidateParameters())
            { return; }

            gloAccountsV2.PaymentCollection.gloEOBPaymentInsurance ogloEOBPayIns = new gloAccountsV2.PaymentCollection.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);
            DataTable _dtPendingCheck = null;
            DateTime dtCloseDate = DateTime.Now;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (mskCloseDate.MaskCompleted)
                {
                    dtCloseDate = gloDateMaster.gloDate.DateAsDate(CloseDate);
                }
                else
                {
                    dtCloseDate = DateTime.MinValue;
                }

                _dtPendingCheck = gloInsurancePaymentV2.GetInsurancePendingChecks(InsuranceCompanyID, dtCloseDate, SelectedUserID, IsShowCompleted, bShowHidden);

                //if (_dtPendingCheck != null && _dtPendingCheck.Rows.Count > 0)
                //{
                //    for (int i = _dtPendingCheck.Rows.Count - 1; i >= 0; i--)
                //    {
                //        if (Convert.ToDecimal(_dtPendingCheck.Rows[i]["Remaining"]) == 0)
                //        {
                //            _dtPendingCheck.Rows.RemoveAt(i);
                //        }
                //    }
                //}
                if (_dtPendingCheck != null)
                {
                    c1PendingCheck.DataSource = _dtPendingCheck;
                    CellStyle csCurrency;// = c1PendingCheck.Styles.Add("csCurrencyCell");
                    try
                    {
                        if (c1PendingCheck.Styles.Contains("csCurrencyCell"))
                        {
                            csCurrency = c1PendingCheck.Styles["csCurrencyCell"];
                        }
                        else
                        {
                            csCurrency = c1PendingCheck.Styles.Add("csCurrencyCell");
                            csCurrency.DataType = typeof(System.Decimal);
                            csCurrency.Format = "c";
                            csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
 
                        }

                    }
                    catch
                    {
                        csCurrency = c1PendingCheck.Styles.Add("csCurrencyCell");
                        csCurrency.DataType = typeof(System.Decimal);
                        csCurrency.Format = "c";
                        csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
 
                    }
                    c1PendingCheck.Cols["Check Amount"].Style = csCurrency;
                    c1PendingCheck.Cols["Remaining"].Style = csCurrency;
                    c1PendingCheck.Cols["Remaining"].Caption = "Remaining Amount";
                    c1PendingCheck.Cols["Ins. Company"].Width = 190;
                    c1PendingCheck.Cols["Check Date"].Width = 80;
                    c1PendingCheck.Cols["CloseDate"].Width = 80;
                    c1PendingCheck.Cols["Check Amount"].Width = 127;
                    c1PendingCheck.Cols["Remaining"].Width = 130;
                    c1PendingCheck.Cols["Check #"].Width = 140;

                    c1PendingCheck.Cols["Check Date"].DataType = typeof(System.DateTime);
                    c1PendingCheck.Cols["CloseDate"].DataType = typeof(System.DateTime);
                    c1PendingCheck.Cols["Check Date"].Format = "MM/dd/yyyy";
                    c1PendingCheck.Cols["CloseDate"].Format = "MM/dd/yyyy";


                    c1PendingCheck.Cols["Check Date"].AllowResizing = false;
                    c1PendingCheck.Cols["CloseDate"].AllowResizing = false;
                    c1PendingCheck.Cols["Check Amount"].AllowResizing = false;
                    c1PendingCheck.Cols["Remaining"].AllowResizing = false;
                    c1PendingCheck.Cols["Check #"].AllowResizing = false;


                    c1PendingCheck.Cols["nCreditID"].Visible = false;
                    c1PendingCheck.Cols["nPayerID"].Visible = false;
                    c1PendingCheck.Cols["nUserID"].Visible = false;
                    c1PendingCheck.Cols["CloseDate"].Caption = "Close Date";

                    DesignFooterGrid();

                }
                else
                { _showForm = false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            { this.Cursor = Cursors.Default; }
        }

        private void SelectCheck(int rowIndex)
        {
            try
            {
                if (c1PendingCheck != null && c1PendingCheck.Rows.Count > 1)
                {
                    if (rowIndex > 0)
                    {
                        if (c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check #"].Index) != null
                        && Convert.ToString(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check #"].Index)).Trim() != "")
                        {
                            _checkNo = Convert.ToString(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check #"].Index)).Trim();

                            string _strChkDate = "";
                            _strChkDate = Convert.ToString(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check Date"].Index)).Trim();
                            if (_strChkDate != "")
                            { _checkDate = gloDateMaster.gloDate.DateAsNumber(_strChkDate); }

                            if (c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check Amount"].Index) != null
                                && Convert.ToString(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check Amount"].Index)).Trim() != ""
                                && Convert.ToDecimal(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check Amount"].Index)) > 0)
                            {
                                _checkAmount = Convert.ToDecimal(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check Amount"].Index));
                            }

                            if (c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Remaining"].Index) != null
                                && Convert.ToString(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Remaining"].Index)).Trim() != ""
                                && Convert.ToDecimal(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Remaining"].Index)) > 0)
                            {
                                _pendingAmount = Convert.ToDecimal(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Remaining"].Index));
                            }

                            if (c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["nCreditID"].Index) != null)
                            {
                                _eobPaymentId = Convert.ToInt64(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["nCreditID"].Index));
                            }

                            _frmDlgRst = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private bool IsValidDate(MaskedTextBox mskDate, bool AllowBlank)
        {
            try
            {
                bool _isValid = false;

                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    { _isValid = gloDate.IsValid(mskDate.Text); }
                    else
                    {
                        if (AllowBlank)
                        { _isValid = true; }
                        else
                        { _isValid = false; }
                    }
                }
                return _isValid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private bool ValidateParameters()
        {
            if (!IsValidDate(mskCloseDate, true))
            {
                MessageBox.Show("Enter a valid close date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskCloseDate.Clear();
                mskCloseDate.Focus();
                return false;
            }
            return true;
        }
        #endregion " Private & Public Methods "

        #region " ToolStrip Button Events "

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            if (!IsValidDate(mskCloseDate, true))
            {
                MessageBox.Show("Enter a valid close date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskCloseDate.Clear();
                mskCloseDate.Focus();
            }
            else
            {
                SelectCheck(c1PendingCheck.RowSel);
            }

        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            _frmDlgRst = DialogResult.Cancel;
            this.Close();
        }

        private void tsb_ShowHideZeroCheck_Click(object sender, EventArgs e)
        {
            if (tsb_ShowHideZeroCheck.Tag.ToString() == "Show")
            {
                tsb_ShowHideZeroCheck.Tag = "Hide";
                tsb_ShowHideZeroCheck.Text = "&Hide Completed";
                tsb_ShowHideZeroCheck.ToolTipText = "Hide Completed Check";
                tsb_ShowHideZeroCheck.Image = global::gloBilling.Properties.Resources.Hide_Completed;
                FillPendingCheck(false); //FillPendingCheck(true)
            }
            else
            {
                tsb_ShowHideZeroCheck.Tag = "Show";
                tsb_ShowHideZeroCheck.Text = "&Show Completed";
                tsb_ShowHideZeroCheck.ToolTipText = "Show Completed Check";
                tsb_ShowHideZeroCheck.Image = global::gloBilling.Properties.Resources.Show_Completed;
                FillPendingCheck(false); //FillPendingCheck(false);
            }
        }

        #endregion " ToolStrip Button Events "

        #region " Form Control Events "

        private void btnSearchInsuranceCompany_Click(object sender, EventArgs e)
        {
            try
            {
                frmSearchInsuranceCompany ofrmSearchInsuranceCompany = new frmSearchInsuranceCompany();
                ofrmSearchInsuranceCompany.StartPosition = FormStartPosition.CenterScreen;
                ofrmSearchInsuranceCompany.ShowDialog(this);

                if (ofrmSearchInsuranceCompany.FrmDlgRst == DialogResult.OK)
                {
                    InsuranceCompanyID = ofrmSearchInsuranceCompany.InsuranceCompanyID;
                    InsuranceCompanyName = ofrmSearchInsuranceCompany.InsuranceCompanyName;

                    if (InsuranceCompanyID != 0 && InsuranceCompanyName != "")
                    {
                        toolTip1.SetToolTip(lblInsCompany, InsuranceCompanyName);
                    }
                }
                ofrmSearchInsuranceCompany.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClearInsurance_Click(object sender, EventArgs e)
        {
            InsuranceCompanyID = 0;
        }

        private void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mskCloseDate_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateParameters())
            {
                e.Cancel = true;
            }
        }

        private void DateMouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        #endregion

        #region " C1 Grid Events "

        private void c1PendingCheck_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            C1.Win.C1FlexGrid.HitTestInfo hitInfo;

            try
            {
                if (c1PendingCheck != null && c1PendingCheck.Rows.Count > 1)
                {
                    hitInfo = c1PendingCheck.HitTest(e.X, e.Y);
                    if (hitInfo.Row > 0)
                    {
                        SelectCheck(hitInfo.Row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
      
        #endregion " C1 Grid Events "

        private void tsb_Generate_Click(object sender, EventArgs e)
        {
            FillPendingCheck(false);
        }

        private void DesignFooterGrid()
        {
            try
            {

                c1SinglePaymentTotal.Clear();
                c1SinglePaymentTotal.Rows.Count = 1;
                c1SinglePaymentTotal.Rows.Fixed = 1;
                c1SinglePaymentTotal.Cols.Count = COL_COUNT;
                c1SinglePaymentTotal.Cols.Fixed = 1;
                c1SinglePaymentTotal.Rows[0].Height = 23;
                c1SinglePaymentTotal.AllowSorting = AllowSortingEnum.None;
                c1SinglePaymentTotal.ScrollBars = ScrollBars.None;
                c1SinglePaymentTotal.AllowEditing = false;
                 c1SinglePaymentTotal.Cols.Fixed = 0;


                c1SinglePaymentTotal.Cols[COL_INSCMP].Width = 190;
                c1SinglePaymentTotal.Cols[COL_CHECKDATE].Width = 80;
                c1SinglePaymentTotal.Cols[COL_CLOSEDATE].Width = 80;
                c1SinglePaymentTotal.Cols[COL_CHKAMT].Width = 127;
                c1SinglePaymentTotal.Cols[COL_CHECKREM].Width = 130;
                c1SinglePaymentTotal.Cols[COL_CHECKNO].Width = 140;

                c1SinglePaymentTotal.Cols[COL_CHECKDATE].DataType = typeof(System.DateTime);
                c1SinglePaymentTotal.Cols[COL_CLOSEDATE].DataType = typeof(System.DateTime);
                c1SinglePaymentTotal.Cols[COL_CHECKDATE].Format = "MM/dd/yyyy";
                c1SinglePaymentTotal.Cols[COL_CLOSEDATE].Format = "MM/dd/yyyy";


                c1SinglePaymentTotal.Cols[COL_INSCMP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1SinglePaymentTotal.Cols[COL_CHECKNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1SinglePaymentTotal.Cols[COL_CHECKDATE].AllowResizing = false;
                c1SinglePaymentTotal.Cols[COL_CLOSEDATE].AllowResizing = false;
                c1SinglePaymentTotal.Cols[COL_CHKAMT].AllowResizing = false;
                c1SinglePaymentTotal.Cols[COL_CHECKREM].AllowResizing = false;
                c1SinglePaymentTotal.Cols[COL_CHECKNO].AllowResizing = false;


                c1SinglePaymentTotal.Cols[COL_CREDITID].Visible = false;
                c1SinglePaymentTotal.Cols[COL_PAYERID].Visible = false;
                c1SinglePaymentTotal.Cols[COL_USERID].Visible = false;


                c1SinglePaymentTotal.SetData(0, COL_CREDITID, "");
                c1SinglePaymentTotal.SetData(0, COL_PAYERID, "");
                c1SinglePaymentTotal.SetData(0, COL_USERID, "");
                c1SinglePaymentTotal.SetData(0, COL_INSCMP, "Total Pending Checks : ");
                c1SinglePaymentTotal.SetData(0, COL_CLOSEDATE, "");
                c1SinglePaymentTotal.SetData(0, COL_CHECKDATE, "");
                if (c1PendingCheck.Rows.Count > 1)
                {
                    c1SinglePaymentTotal.SetData(0, COL_CHECKNO, c1PendingCheck.Rows.Count-1);
                }
                
                c1SinglePaymentTotal.SetData(0, COL_CHKAMT, "Total Remaining : ");
                c1SinglePaymentTotal.SetData(0, COL_CHECKREM, CalculateFooterTotal(8));

                CellStyle csTextCaption;
               // csTextCaption = c1SinglePaymentTotal.Styles.Add("SubCaption");
                try
                {
                    if (c1SinglePaymentTotal.Styles.Contains("SubCaption"))
                    {
                        csTextCaption = c1SinglePaymentTotal.Styles["SubCaption"];
                    }
                    else
                    {
                        csTextCaption = c1SinglePaymentTotal.Styles.Add("SubCaption");
                        csTextCaption.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        csTextCaption.TextEffect = TextEffectEnum.Flat;
                        csTextCaption.ForeColor = Color.Maroon;
                        csTextCaption.TextAlign = TextAlignEnum.RightCenter;
                    }

                }
                catch
                {
                    csTextCaption = c1SinglePaymentTotal.Styles.Add("SubCaption");
                    csTextCaption.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csTextCaption.TextEffect = TextEffectEnum.Flat;
                    csTextCaption.ForeColor = Color.Maroon;
                    csTextCaption.TextAlign = TextAlignEnum.RightCenter;
                }
      
                
                
                CellRange subTextCaptionRange;
                subTextCaptionRange = c1SinglePaymentTotal.GetCellRange(0, COL_INSCMP, 0, COL_INSCMP);
                subTextCaptionRange.Style = csTextCaption;
                
                CellRange subTextCaptionRange2;
                subTextCaptionRange2 = c1SinglePaymentTotal.GetCellRange(0, COL_CHKAMT, 0, COL_CHKAMT);
                subTextCaptionRange2.Style = csTextCaption;
                
                CellStyle csNumber;
              //  csNumber = c1SinglePaymentTotal.Styles.Add("csNumber");
                try
                {
                    if (c1SinglePaymentTotal.Styles.Contains("csNumber"))
                    {
                        csNumber = c1SinglePaymentTotal.Styles["csNumber"];
                    }
                    else
                    {
                        csNumber = c1SinglePaymentTotal.Styles.Add("csNumber");
                        csNumber.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        csNumber.TextEffect = TextEffectEnum.Flat;
                        csNumber.ForeColor = Color.Blue;
                        csNumber.TextAlign = TextAlignEnum.LeftCenter;
                    }

                }
                catch
                {
                    csNumber = c1SinglePaymentTotal.Styles.Add("csNumber");
                    csNumber.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csNumber.TextEffect = TextEffectEnum.Flat;
                    csNumber.ForeColor = Color.Blue;
                    csNumber.TextAlign = TextAlignEnum.LeftCenter;
                }
 
                
                CellRange subNumberRange;
                subNumberRange = c1SinglePaymentTotal.GetCellRange(0, COL_CHECKNO, 0, COL_CHECKNO);
                subNumberRange.Style = csNumber;
                
                CellStyle csAmount;
               // csAmount = c1SinglePaymentTotal.Styles.Add("SubTotalRow");
                try
                {
                    if (c1SinglePaymentTotal.Styles.Contains("SubTotalRow"))
                    {
                        csAmount = c1SinglePaymentTotal.Styles["SubTotalRow"];
                    }
                    else
                    {
                        csAmount = c1SinglePaymentTotal.Styles.Add("SubTotalRow");
                        csAmount.Format = "c";
                        csAmount.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        csAmount.TextEffect = TextEffectEnum.Flat;
                        csAmount.ForeColor = Color.Blue;
                        csAmount.TextAlign = TextAlignEnum.RightCenter;
                    }

                }
                catch
                {
                    csAmount = c1SinglePaymentTotal.Styles.Add("SubTotalRow");
                    csAmount.Format = "c";
                    csAmount.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csAmount.TextEffect = TextEffectEnum.Flat;
                    csAmount.ForeColor = Color.Blue;
                    csAmount.TextAlign = TextAlignEnum.RightCenter;
                }
              
                
                CellRange subAmountRange;
                subAmountRange = c1SinglePaymentTotal.GetCellRange(0, COL_CHECKREM, 0, COL_CHECKREM);
                subAmountRange.Style = csAmount;

               
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private decimal CalculateFooterTotal(int ColNumber)
        {
            decimal _result = 0;
            try
            {
                if (c1PendingCheck.Rows.Count > 1)
                {
                    for (int i = 1; i <= c1PendingCheck.Rows.Count - 1; i++)
                    {

                        if (c1PendingCheck.GetData(i, ColNumber) != null && c1PendingCheck.GetData(i, ColNumber).ToString() != null && c1PendingCheck.GetData(i, ColNumber).ToString().Trim().Length > 0)
                        {
                            _result = _result + Convert.ToDecimal(Convert.ToString(c1PendingCheck.GetData(i, ColNumber)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            return _result;

        }

        private void c1PendingCheck_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (c1PendingCheck.HitTest(e.X, e.Y).Row >= 1)
                {
                    Int32 tempRow = 0;
                    tempRow = c1PendingCheck.HitTest(e.X, e.Y).Row;
                    c1PendingCheck.Row = tempRow;
                    if (e.Button == MouseButtons.Right)
                    {
                        contextMenuPendingCheck.Items.Clear();
                        c1PendingCheck.ContextMenuStrip = contextMenuPendingCheck;
                        contextMenuPendingCheck.Items.Add(cmt_Remove); 
                        contextMenuPendingCheck.Items.Add(cmt_Seperator);
                        contextMenuPendingCheck.Items.Add(cmt_ShowHidden); 
                    }
                    else
                    {
                        c1PendingCheck.ContextMenuStrip = contextMenuPendingCheck;
                        contextMenuPendingCheck.Items.Remove(cmt_Seperator); 
                        contextMenuPendingCheck.Items.Remove(cmt_Remove); 
                        //c1PendingCheck.ContextMenuStrip = null;
                    }
                }
                else
                {
                    c1PendingCheck.ContextMenuStrip = contextMenuPendingCheck;
                    contextMenuPendingCheck.Items.Remove(cmt_Seperator); 
                    contextMenuPendingCheck.Items.Remove(cmt_Remove); 
                    //c1PendingCheck.ContextMenuStrip = null;
                }
            }
            catch (Exception ex)
            {
                 gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void cmt_Remove_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dtPendingCheckRemove = null;
                _dtPendingCheckRemove = (DataTable)c1PendingCheck.DataSource;

                if (_dtPendingCheckRemove != null && _dtPendingCheckRemove.Rows.Count > 0)
                {
                    Int64 nCreditID = 0;

                    if (c1PendingCheck.GetData(c1PendingCheck.RowSel, c1PendingCheck.Cols[COL_CREDITID].Index) != null && Convert.ToString(c1PendingCheck.GetData(c1PendingCheck.RowSel, c1PendingCheck.Cols[COL_CREDITID].Index)) != "")
                    {
                        nCreditID = Convert.ToInt64(c1PendingCheck.GetData(c1PendingCheck.RowSel, COL_CREDITID));
                    }

                    if (nCreditID > 0)
                    {
                        if (gloInsurancePaymentV2.HidePendingCheck(nCreditID, true))
                        {
                            _dtPendingCheckRemove.DefaultView.Sort = "nCreditID";
                            _dtPendingCheckRemove.DefaultView.Delete(_dtPendingCheckRemove.DefaultView.Find(nCreditID));
                            _dtPendingCheckRemove.AcceptChanges();

                            c1PendingCheck.DataSource = _dtPendingCheckRemove;
                            DesignFooterGrid();

                        }
                    }
                   
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

           
        }

        private void cmt_ShowHidden_Click(object sender, EventArgs e)
        {
            try
            {
                FillPendingCheck(true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

       
    }
}