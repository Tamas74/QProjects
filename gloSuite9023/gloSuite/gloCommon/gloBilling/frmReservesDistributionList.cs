using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using C1.Win.C1FlexGrid;
using System.Linq;


namespace gloBilling
{
    public partial class frmReservesDistributionList : Form
    {
        #region " Declarations "

        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64   _ClinicID = 0;
        Font Font_CellStyle = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        String SelectedPaymentTray ="";
        Int64 SelectedPaymentTrayID = 0;
        //gloAccountPayment.dsPaymentTVP_V2 dsInsurancePayment_TVP = null;
        //gloAccountsV2.gloPatientPaymentV2 ogloPatientPayment = null;
        C1.Win.C1FlexGrid.CellStyle csYellowStyle;
        C1.Win.C1FlexGrid.CellStyle csGreenStyle;
        #endregion " Declarations "

        #region "Column Declaration"


        private const int COL_Select = 0;
        private const int COL_AccountID = 1;
        private const int COL_PatientID = 2;
        private const int COL_AccountNumber = 3 ;       
        private const int COL_PatientName = 4;        
        private const int COL_AccountBalance = 5;
        private const int COL_PatientDue = 6 ;
        private const int COL_InsuranceDue = 7;
        private const int COL_BadDebtDue = 8;
        private const int COL_TotalReserveAmount = 9;
        private const int COL_Available = 10;
        private const int COL_UsedReserves = 11;
        private const int COL_PatientDueServiceCode = 12; 
        private const int COL_COUNT = 13;
        
        #endregion "Column Declarations "
        
        #region " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion " Property Procedures "

        public frmReservesDistributionList()
        {
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }
            //gloPatient.gloAccount objAccount = new gloPatient.gloAccount(_databaseconnectionstring);
            //_IsPatientAccountFeature = objAccount.GetPatientAccountFeatureSetting();
            #endregion
        }

        private void frmReserveDistributionList_Load(object sender, EventArgs e)
        {
            LoadReserveDistributionList();           
        }


        private void tsb_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            LoadReserveDistributionList();
            
        }

        private void LoadReserveDistributionList()
        {
            DataTable dtCopayList = null;
            ClsReserveDistributionList oClsReserveDistributionList = new ClsReserveDistributionList();            
            try
            {
                dtCopayList = oClsReserveDistributionList.getReserveList(dtStartDate.Value.Date, dtEndDate.Value.Date);
               // C1ReserveList.Clear();
                C1ReserveList.DataSource = null;
                C1ReserveList.DataSource = dtCopayList.Copy();
                DesignCopayGrid();
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
            finally
            {
                if (dtCopayList != null) { dtCopayList.Dispose(); dtCopayList = null; }
                if (oClsReserveDistributionList != null)
                {
                    oClsReserveDistributionList = null;
                }
            }
        }

        private void DesignCopayGrid()
        {
            try
            {
                int _width;
                gloC1FlexStyle.Style(C1ReserveList, true);
                C1ReserveList.Rows.Fixed = 1;
                C1ReserveList.Cols.Count = COL_COUNT;
                C1ReserveList.Cols.Fixed = 0;

                C1ReserveList.Cols[0].DataType = typeof(bool);
                if (C1ReserveList.Rows.Count > 0)
                {
                    C1ReserveList.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    C1ReserveList.SetData(0, 0, "Select All", false);
                    C1ReserveList.Cols[0].Style.TextAlign = TextAlignEnum.CenterCenter;
                    C1ReserveList.Cols[0].AllowEditing = true;
                    C1ReserveList.Cols[0].AllowSorting = false;
                }
                else
                {
                    C1ReserveList.Cols[COL_Select].Visible = false;
                }
                
                C1ReserveList.SetData(0, COL_AccountID, "Account ID");
                C1ReserveList.SetData(0, COL_AccountNumber, "Account #");
                C1ReserveList.Cols[COL_AccountNumber].AllowEditing = false;
                C1ReserveList.SetData(0, COL_PatientID, "Patient ID");
                C1ReserveList.SetData(0, COL_PatientName, "Patient Name");
                C1ReserveList.Cols[COL_PatientName].AllowEditing = false;

                C1ReserveList.SetData(0, COL_AccountBalance, "Acc.Bal");
                C1ReserveList.Cols[COL_AccountBalance].AllowEditing = false;
                
                C1ReserveList.SetData(0, COL_PatientDue, "Pat.Due");
                C1ReserveList.Cols[COL_PatientDue].AllowEditing = false;
                C1ReserveList.SetData(0, COL_InsuranceDue, "Ins.Due");
                C1ReserveList.Cols[COL_InsuranceDue].AllowEditing = false;
                C1ReserveList.SetData(0, COL_BadDebtDue, "BadDebt.Due");
                C1ReserveList.Cols[COL_BadDebtDue].AllowEditing = false;

                C1ReserveList.SetData(0, COL_TotalReserveAmount, "To Reserve");
                C1ReserveList.Cols[COL_TotalReserveAmount].AllowEditing = false;
                C1ReserveList.SetData(0, COL_Available, " Available Reserve");
                C1ReserveList.Cols[COL_Available].AllowEditing = false;

                C1ReserveList.SetData(0, COL_UsedReserves, "");

                C1ReserveList.SetData(0, COL_PatientDueServiceCode, "PatientDueWithoutNonServie");
                C1ReserveList.Cols[COL_PatientDueServiceCode].AllowEditing = false;
                C1ReserveList.Cols[COL_PatientDueServiceCode].Width = 0;
                C1ReserveList.Cols[COL_PatientDueServiceCode].Visible = false;

                C1ReserveList.Cols[COL_AccountID].Visible = false;
                C1ReserveList.Cols[COL_AccountNumber].Visible = true;
                C1ReserveList.Cols[COL_PatientID].Visible = false;
                C1ReserveList.Cols[COL_PatientName].Visible = true;
              
                C1ReserveList.Cols[COL_Available].Visible = true;
                C1ReserveList.Cols[COL_UsedReserves].Visible = false;
                C1ReserveList.Cols[COL_TotalReserveAmount].Visible = false;
                
            
                _width = this.Width-5;


                C1ReserveList.Cols[COL_Select].Width = Convert.ToInt32(_width * 0.07);
                C1ReserveList.Cols[COL_AccountNumber].Width = Convert.ToInt32(_width * 0.13);
                C1ReserveList.Cols[COL_PatientName].Width = Convert.ToInt32(_width * 0.25);
               
                C1ReserveList.Cols[COL_Available].Width = Convert.ToInt32(_width * 0.12);
                C1ReserveList.Cols[COL_UsedReserves].Width = Convert.ToInt32(_width * 0.05);

                C1ReserveList.Cols[COL_AccountNumber].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1ReserveList.Cols[COL_PatientName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;               
                C1ReserveList.Cols[COL_Available].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                C1ReserveList.Cols[COL_AccountBalance].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1ReserveList.Cols[COL_TotalReserveAmount].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter; 

                C1ReserveList.Cols[COL_PatientDue].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1ReserveList.Cols[COL_InsuranceDue].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter; 
                C1ReserveList.Cols[COL_BadDebtDue].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter; 

                C1ReserveList.Cols[COL_UsedReserves].DataType = typeof(Image);
                C1ReserveList.Cols[COL_UsedReserves].ImageMap = new System.Collections.Hashtable();
                C1ReserveList.Cols[COL_UsedReserves].ImageMap.Add(0, null);
                C1ReserveList.Cols[COL_UsedReserves].ImageMap.Add(1, global::gloBilling.Properties.Resources.StatusNormal );
                C1ReserveList.Cols[COL_UsedReserves].ImageMap.Add(2, global::gloBilling.Properties.Resources.HoldClaim);
                C1ReserveList.Cols[COL_UsedReserves].ImageAndText = false;
                C1ReserveList.Cols[COL_UsedReserves].AllowResizing = false;
                C1ReserveList.ExtendLastCol = true;

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;
                try
                {
                    if (C1ReserveList.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = C1ReserveList.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = C1ReserveList.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = Font_CellStyle; 
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = C1ReserveList.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = Font_CellStyle; 
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }

                try
                {
                    if (C1ReserveList.Styles.Contains("cs_YellowStyle"))
                    {
                        csYellowStyle = C1ReserveList.Styles["cs_YellowStyle"];
                    }
                    else
                    {
                        csYellowStyle = C1ReserveList.Styles.Add("cs_YellowStyle");
                        csYellowStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 0);
                        csYellowStyle.ForeColor = System.Drawing.Color.Black;
                    }

                }
                catch
                {
                    csYellowStyle = C1ReserveList.Styles.Add("cs_YellowStyle");
                    csYellowStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 0);
                    csYellowStyle.ForeColor = System.Drawing.Color.Black;
                }

                try
                {
                    if (C1ReserveList.Styles.Contains("cs_GreenStyle"))
                    {
                        csGreenStyle = C1ReserveList.Styles["cs_GreenStyle"];
                    }
                    else
                    {
                        csGreenStyle = C1ReserveList.Styles.Add("cs_GreenStyle");
                        csGreenStyle.BackColor = System.Drawing.Color.FromArgb(0, 255, 0);
                        csGreenStyle.ForeColor = System.Drawing.Color.Black;
                    }

                }
                catch
                {
                    csGreenStyle = C1ReserveList.Styles.Add("cs_GreenStyle");
                    csGreenStyle.BackColor = System.Drawing.Color.FromArgb(0, 255, 0);
                    csGreenStyle.ForeColor = System.Drawing.Color.Black;

                }

                C1ReserveList.Cols[COL_Available].Style = csCurrencyStyle;
                C1ReserveList.Cols[COL_AccountBalance].Style = csCurrencyStyle;
                C1ReserveList.Cols[COL_TotalReserveAmount].Style = csCurrencyStyle;
                
                C1ReserveList.Cols[COL_PatientDue].Style = csCurrencyStyle;
                C1ReserveList.Cols[COL_InsuranceDue].Style = csCurrencyStyle;
                C1ReserveList.Cols[COL_BadDebtDue].Style = csCurrencyStyle;

               
                C1ReserveList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;

                #endregion
                if (C1ReserveList.Rows.Count > 1)
                {
                    C1ReserveList.Row = 1;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
        }


        private void tls_btnPatAcct_Click(object sender, EventArgs e)
        {
           gloAccountsV2.frmPatientFinancialViewV2 frm = null;
            try
            {
                if (C1ReserveList.DataSource != null && C1ReserveList.RowSel > 0)
                {
                    Int64 _PatientID= Convert.ToInt64(C1ReserveList.GetData(C1ReserveList.RowSel,COL_PatientID));
                    frm = new gloAccountsV2.frmPatientFinancialViewV2(_PatientID);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.ShowInTaskbar = false;
                    frm.IsCalledFromInsPmt = true;
                    frm.ShowDialog(this);
                    LoadReserveDistributionList();
                }
                //else
                //{
                //    MessageBox.Show("Please select the claim", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
            finally
            {
                if (frm != null) { frm.Dispose(); frm = null; }
            }
        }

        private void tsb_PaymentPatient_Click(object sender, EventArgs e)
        {
            gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2=null;
               try
                {
                    if (C1ReserveList.DataSource != null && C1ReserveList.RowSel > 0)
                    {
                        Int64 _PatientID= Convert.ToInt64(C1ReserveList.GetData(C1ReserveList.RowSel,COL_PatientID));
                        frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(_PatientID, false, 0, 0, 0, 0, EOBPaymentSubType.Other);
                        frmPatientPaymentV2.IsFromCopayReserveList = true;
                        Int64 _ACTID = Convert.ToInt64(C1ReserveList.GetData(C1ReserveList.RowSel, COL_AccountID));
                        frmPatientPaymentV2.PAccountID = _ACTID;
                        decimal _AvailableAmt = Convert.ToDecimal(C1ReserveList.GetData(C1ReserveList.RowSel, COL_Available));
                        frmPatientPaymentV2.CheckAmount = _AvailableAmt;
                        if(_AvailableAmt>0)
                            frmPatientPaymentV2.SeletedReserveItems = FillUseReserve(_PatientID, _ACTID);

                        if (frmPatientPaymentV2.SeletedReserveItems != null && frmPatientPaymentV2.SeletedReserveItems.Count > 0)
                        {

                            frmPatientPaymentV2.StartPosition = FormStartPosition.CenterScreen;
                            frmPatientPaymentV2.WindowState = FormWindowState.Maximized;
                            frmPatientPaymentV2.ShowInTaskbar = false;
                            frmPatientPaymentV2.ShowDialog(this);
                            LoadReserveDistributionList();
                        }
                        else
                        {
                            DialogResult _resDlgRst = System.Windows.Forms.DialogResult.None;

                            _resDlgRst = MessageBox.Show("Reserve(s) selected for allocation is/are used by another user."+Environment.NewLine+"Do you want to refresh the work list for current reserves availability?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (_resDlgRst == System.Windows.Forms.DialogResult.Yes)
                            { LoadReserveDistributionList(); }
                        }
                    }
                    //else
                    //{
                    //    MessageBox.Show("Please select the claim", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ex = null;
                }
                finally
                {
                    if (frmPatientPaymentV2 != null) { frmPatientPaymentV2.Dispose(); frmPatientPaymentV2 = null; }
                }
            
        }

        private gloGeneralItem.gloItems FillUseReserve(Int64 PatientID, Int64 PAccountID)
        {
            gloGeneralItem.gloItems oSeletedReserveItems = new gloGeneralItem.gloItems();
            gloGeneralItem.gloItem ogloItem = null;
            ClsReserveDistributionList oClsReserveDistributionList = new ClsReserveDistributionList();
            decimal _Amount = 0;
            Int64 _selEOBPayId = 0;
            Int64 _selEOBPayDtlId = 0;
            Int64 _selresEOBPayId = 0;
            String _selCloseDate = "";
            DataTable dtReserve = null;
            try
            {

                dtReserve = oClsReserveDistributionList.getUseReserveList(PAccountID, dtStartDate.Value.Date, dtEndDate.Value.Date);
                if (dtReserve != null)
                {
                    if (dtReserve.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtReserve.Rows.Count - 1; i++)
                        {

                            _selEOBPayId = Convert.ToInt64(dtReserve.Rows[i]["nEOBPaymentID"]);
                            _selEOBPayDtlId = Convert.ToInt64(dtReserve.Rows[i]["nEOBPaymentDetailID"]);
                            _selresEOBPayId = Convert.ToInt64(dtReserve.Rows[i]["nReserveID"]);
                            _selCloseDate = Convert.ToString(dtReserve.Rows[i]["dtCloseDate"]);
                            _Amount = Convert.ToDecimal(dtReserve.Rows[i]["nAmount"]);
                            ogloItem = new gloGeneralItem.gloItem(_selEOBPayId, Convert.ToString(_selEOBPayDtlId), Convert.ToString(_Amount));
                            ogloItem.SubItems.Add(_selresEOBPayId, "0", "0", _selCloseDate);
                            oSeletedReserveItems.Add(ogloItem);
                            //ogloItem.Dispose(); //SLR: It should not be freed since it will dispose subitems?
                            //ogloItem = null;
                            _selEOBPayId = 0;
                            _selEOBPayDtlId = 0;
                            _selEOBPayId = 0;
                            _selEOBPayDtlId = 0;
                            _selCloseDate = "";

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                oSeletedReserveItems = null;
                ex = null;
            }
            finally
            {
               // if (ogloItem != null) { ogloItem.Dispose(); ogloItem = null; }
                _selEOBPayId = 0;
                _selEOBPayDtlId = 0;
                _selEOBPayId = 0;
                _selEOBPayDtlId = 0;
                _selCloseDate = "";
                if (dtReserve != null) { dtReserve.Dispose(); dtReserve = null; }
                if (oClsReserveDistributionList != null)
                {
                    oClsReserveDistributionList = null;
                }
            }
            return oSeletedReserveItems;
        }

        private void frmReserveDistributionList_Shown(object sender, EventArgs e)
        {
            DesignCopayGrid();
        }
        private void SetPaymentTrayPopup()
        {
            try
            {
                Int64 _lastSelectedTrayID = BillingSettings.LastSelectedPaymentTrayID;
                Int64 _defaultTrayID = gloAccountsV2.gloInsurancePaymentV2.GetDefaultPaymentTrayID();

                // Set default payment tray
                SelectedPaymentTray = gloAccountsV2.gloInsurancePaymentV2.GetPaymentTrayDescription(_defaultTrayID);
                SelectedPaymentTrayID = _defaultTrayID;

                if (_lastSelectedTrayID > 0)
                {
                    if (gloAccountsV2.gloInsurancePaymentV2.IsPaymentTrayActive(_lastSelectedTrayID))
                    {
                        if (!_lastSelectedTrayID.Equals(_defaultTrayID))
                        {
                            SelectedPaymentTray = gloAccountsV2.gloInsurancePaymentV2.GetPaymentTrayDescription(_lastSelectedTrayID);
                            SelectedPaymentTrayID = _lastSelectedTrayID;
                            SelectPaymentTray();
                        }
                    }
                    else
                    {
                        SelectPaymentTray();
                    }
                }
                else
                {
                    SelectPaymentTray();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void SelectPaymentTray()
        {
            frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                //...Show pop-up to select the Tray
                ofrmBillingTraySelection.WindowState = FormWindowState.Normal;
                ofrmBillingTraySelection.StartPosition = FormStartPosition.CenterParent;
                ofrmBillingTraySelection.IsChargeTray = false;               
                ofrmBillingTraySelection.ShowDialog(this);

              

                // If payment tray Modified or selected from payment tray dialog then reflect the changes
                if (ofrmBillingTraySelection.FormResult == DialogResult.OK)
                {
                    if (ofrmBillingTraySelection.SelectedTrayID > 0)
                    {
                        this.SelectedPaymentTray = ofrmBillingTraySelection.SelectedTrayName;
                        this.SelectedPaymentTrayID = ofrmBillingTraySelection.SelectedTrayID;
                       
                    }
                    else
                    {
                        this.SelectedPaymentTray = string.Empty;
                        this.SelectedPaymentTrayID = 0;
                    }
                }
                // If Payment Tray dialog closed and payment tray made inactivated then reflect the changes
                else if (ofrmBillingTraySelection.IsOperationPerformed)
                {
                    if (ofrmBillingTraySelection.SelectedTrayID > 0)
                    {
                        this.SelectedPaymentTray = ofrmBillingTraySelection.SelectedTrayName;
                        this.SelectedPaymentTrayID = ofrmBillingTraySelection.SelectedTrayID;
                        
                    }
                    else
                    {
                        this.SelectedPaymentTray = string.Empty;
                        this.SelectedPaymentTrayID = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ofrmBillingTraySelection.Dispose();
            }
        }
        private void tlsAutoDistributeReserve_Click(object sender, EventArgs e)
        {
            #region "AutoDistribution_NEW Code"
          
            Collections.PatientDetails selectedAccountDetails = null;
            Int64 PAccountID = 0;
            decimal _dAvailableReservesAmt=0;
            decimal _dRemainingAccountBalanceWithoutNonService=0;
            decimal _dRemainingAccountBalanceWithNonService = 0;
            string _sAccountNo=string.Empty;
            DateTime dtCloseDate = DateTime.MinValue;           
            string paymentTrayName = string.Empty;
            string adjustmentCode = string.Empty;
            string adjustmentDesc = string.Empty;
            string sCheckAmountToDistribute = "";
            string sAccountNo= "";
            string sAccountPatientName = string.Empty;
            gloGeneralItem.gloItems AvailableReserveListTodistribute = new gloGeneralItem.gloItems();
            ClsReserveDistributionList oClsReserveDistributionList = new ClsReserveDistributionList();
            frmPaymentTransferInfo ofrmPaymentTransferInfo = null;
            StringBuilder _ClaimInfo = new StringBuilder();
            StringBuilder _NonServiceClaimInfo = new StringBuilder();
            Int16 _msgCounter = 0;
            Int16 _accountCounter = 0;
            Int16 _NonServiceClaimInfoCounter = 0;
            StringBuilder _accountInfo = new StringBuilder();
            string sAccountIDs = string.Empty;
            SelectedPaymentTrayID = 0;
            try
            {
                if (C1ReserveList != null && C1ReserveList.Rows.Count > 1)
                {
                    selectedAccountDetails = new Collections.PatientDetails();

                    //int nSelectCount = 0;
                    DataRow[] SelectedRows = null;
                    DataTable dtReserveListSource = null;
                    dtReserveListSource = ((DataTable)C1ReserveList.DataSource);
              
                    //SelectedRows = ((DataTable)C1ReserveList.DataSource).Select("Select All='True'");
                    //if (dtReserveListSource.Columns["Select All"] != null)
                    //{
                    //    dtReserveListSource.Columns["Select All"].ColumnName = "SelectAll";
                    //}
                   // nSelectCount = C1ReserveList.FindRow("1", 1,0, true); // C1ReserveList.FindRow("True", 1, 0, true);
                    SelectedRows = dtReserveListSource.Select("[Select All]='True'");

                    if (SelectedRows != null && SelectedRows.Length > 0)
                    {
                        for (int i = 1; i < C1ReserveList.Rows.Count; i++)
                        {
                            if (C1ReserveList.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                PAccountID = 0;
                                Int64.TryParse(Convert.ToString(C1ReserveList.GetData(C1ReserveList.Rows[i].Index, COL_AccountID)), out PAccountID);
                                if (PAccountID != 0)
                                {
                                    if (sAccountIDs != String.Empty)
                                    {
                                        sAccountIDs = sAccountIDs + "," + Convert.ToString(PAccountID);
                                    }
                                    else
                                    {
                                        sAccountIDs = Convert.ToString(PAccountID);
                                    }
                                }
                                _dAvailableReservesAmt=Convert.ToDecimal(C1ReserveList.GetData(C1ReserveList.Rows[i].Index, COL_Available));
                                //_dRemainingAccountBalance = Convert.ToDecimal(C1ReserveList.GetData(C1ReserveList.Rows[i].Index, COL_PatientDue));// +Convert.ToDecimal(C1ReserveList.GetData(C1ReserveList.Rows[i].Index, COL_BadDebtDue));
                                _dRemainingAccountBalanceWithoutNonService = Convert.ToDecimal(C1ReserveList.GetData(C1ReserveList.Rows[i].Index, COL_PatientDueServiceCode));// Non Service Line Balnces exluded for reserve distribution
                                _dRemainingAccountBalanceWithNonService = Convert.ToDecimal(C1ReserveList.GetData(C1ReserveList.Rows[i].Index, COL_PatientDue));
                                 sAccountNo = Convert.ToString(C1ReserveList.GetData(C1ReserveList.Rows[i].Index, COL_AccountNumber));
                                 sAccountPatientName = Convert.ToString(C1ReserveList.GetData(C1ReserveList.Rows[i].Index, COL_PatientName));
                                if (PAccountID > 0)
                                {
                                    selectedAccountDetails.Add(new Collections.PatientDetail()
                                    {
                                        PatientAccountID = PAccountID,
                                        dAvailableReservesAmt=_dAvailableReservesAmt,
                                        dRemainingAccountBalanceWithourNonServiceCode = _dRemainingAccountBalanceWithoutNonService,
                                        dRemainingAccountBalanceWithNoServiceCode=_dRemainingAccountBalanceWithNonService,
                                        sAccountNo=sAccountNo,
                                        sAccountPatientName=sAccountPatientName

                                        
                                    });
                                }
                            }
                        }
                        
                       
                    }
                    else
                    {
                        if (C1ReserveList.RowSel > 0 && C1ReserveList.ColSel >= 0)
                        {
                            PAccountID = 0;
                            Int64.TryParse(Convert.ToString(C1ReserveList.GetData(C1ReserveList.RowSel, COL_AccountID)), out PAccountID);
                            _dAvailableReservesAmt = Convert.ToDecimal(C1ReserveList.GetData(C1ReserveList.RowSel, COL_Available));
                            //_dRemainingAccountBalance = Convert.ToDecimal(C1ReserveList.GetData(C1ReserveList.RowSel, COL_PatientDue));// +Convert.ToDecimal(C1ReserveList.GetData(C1ReserveList.Rows[i].Index, COL_BadDebtDue));
                            _dRemainingAccountBalanceWithoutNonService = Convert.ToDecimal(C1ReserveList.GetData(C1ReserveList.RowSel, COL_PatientDueServiceCode));// Non Service Line Balnces exluded for reserve distribution   sAccountNo = Convert.ToString(C1ReserveList.GetData(C1ReserveList.RowSel, COL_AccountNumber));
                            _dRemainingAccountBalanceWithNonService = Convert.ToDecimal(C1ReserveList.GetData(C1ReserveList.RowSel, COL_PatientDue));
                            sAccountNo = Convert.ToString(C1ReserveList.GetData(C1ReserveList.RowSel, COL_AccountNumber));
                             sAccountIDs = Convert.ToString(PAccountID);
                             sAccountPatientName = Convert.ToString(C1ReserveList.GetData(C1ReserveList.RowSel, COL_PatientName));
                            if (PAccountID > 0)
                            {
                                selectedAccountDetails.Add(new Collections.PatientDetail()
                                {
                                    PatientAccountID = PAccountID,
                                    dAvailableReservesAmt = _dAvailableReservesAmt,
                                    dRemainingAccountBalanceWithourNonServiceCode = _dRemainingAccountBalanceWithoutNonService,
                                    dRemainingAccountBalanceWithNoServiceCode = _dRemainingAccountBalanceWithNonService,
                                    sAccountNo = sAccountNo,
                                    sAccountPatientName=sAccountPatientName
                                });
                            }
                        }
                    }
                    DesignCopayGrid(); 

                    if (selectedAccountDetails != null && selectedAccountDetails.Count > 0)
                    {

                        //Set values here..
                        dtCloseDate = DateTime.Today;                      
                        paymentTrayName = "";
                        adjustmentCode = "";
                        adjustmentDesc = "";


                        gloAccountPayment.BulkPaymentOperation bulkWriteOff = null;
                        gloAccountPayment.PaymentInfoParameter paymentParameter = null;
                        gloAccountPayment.AccountOwnerInfo currentacctInfo = null;
                        if (SelectedPaymentTrayID == 0)
                        {
                            //  DesignCopayGrid(); 
                            ofrmPaymentTransferInfo = new frmPaymentTransferInfo();
                            ofrmPaymentTransferInfo.IsForAutoDistribution = true;
                            ofrmPaymentTransferInfo.AccountIDs = sAccountIDs;
                            ofrmPaymentTransferInfo.ShowDialog();
                            if (ofrmPaymentTransferInfo.PaymentTrayID > 0)
                            {
                                this.SelectedPaymentTray = ofrmPaymentTransferInfo.PaymentTrayName;
                                this.SelectedPaymentTrayID = ofrmPaymentTransferInfo.PaymentTrayID;

                            }
                            else
                            {
                                this.SelectedPaymentTray = string.Empty;
                                this.SelectedPaymentTrayID = 0;
                            }


                        }
                        if (ofrmPaymentTransferInfo.PaymentTransferCloseDate == string.Empty || this.SelectedPaymentTrayID == 0)
                        {
                            bool IsAllSelect = false;
                            for (int i = 1; i <= C1ReserveList.Rows.Count - 1; i++)
                            {
                                if (C1ReserveList.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                {
                                    IsAllSelect = true;
                                }
                                else
                                {
                                    IsAllSelect = false;
                                    C1ReserveList.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    return;
                                }

                            }
                            if (IsAllSelect)
                            {
                                C1ReserveList.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            }
                            return;

                        }
                        for (int i = 0; i < selectedAccountDetails.Count; i++)
                        {
                            if (selectedAccountDetails[i].PatientAccountID > 0)
                            {
                                currentacctInfo = gloAccountPayment.AccountInfo.GetAccountInfo(selectedAccountDetails[i].PatientAccountID);
                                if (selectedAccountDetails[i].dAvailableReservesAmt > 0)
                                {
                                    AvailableReserveListTodistribute = oClsReserveDistributionList.FillUseReserveforAutoDistribution(currentacctInfo.AccountownerpatientId, currentacctInfo.AccountId, selectedAccountDetails[i].dRemainingAccountBalanceWithourNonServiceCode, selectedAccountDetails[i].dAvailableReservesAmt, dtStartDate.Value.Date, dtEndDate.Value.Date, out sCheckAmountToDistribute);
                                }

                                if (AvailableReserveListTodistribute != null && AvailableReserveListTodistribute.Count > 0 && selectedAccountDetails[i].dRemainingAccountBalanceWithourNonServiceCode >= 0)
                                {
                                    // Select paymnet tray information

                                    dtCloseDate = Convert.ToDateTime(ofrmPaymentTransferInfo.PaymentTransferCloseDate);
                                    //End Select payment tray
                                    paymentParameter = new gloAccountPayment.PaymentInfoParameter(
                                          currentacctInfo.AccountownerpatientId,
                                          currentacctInfo.AccountId,
                                          currentacctInfo.GuarantorId,
                                          currentacctInfo.AccountpatientId,
                                          dtCloseDate.Date,
                                          SelectedPaymentTrayID,
                                          SelectedPaymentTray,
                                          "0.00",
                                          "0.00",
                                          sCheckAmountToDistribute
                                        );

                                    bulkWriteOff = new gloAccountPayment.BulkPaymentOperation();
                                    bulkWriteOff.AutoDistributeReserves(paymentParameter, AvailableReserveListTodistribute);

                                    paymentParameter = null;
                                    currentacctInfo = null;
                                }
                                else if (selectedAccountDetails[i].dRemainingAccountBalanceWithourNonServiceCode <= 0 && (selectedAccountDetails[i].dRemainingAccountBalanceWithourNonServiceCode == selectedAccountDetails[i].dRemainingAccountBalanceWithNoServiceCode))
                                {
                                    // DesignCopayGrid();
                                    //if (selectedAccountDetails.Count == 1)
                                    //{
                                    //    MessageBox.Show("There is no remaining Patient due Or BadDebt due available to distribute selected  Reserve(s) for allocation Account # \"" + sAccountNo + "\".", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //}
                                    //else
                                    //{
                                    //    if (DialogResult.No == MessageBox.Show("There is no remaining Patient due Or BadDebt due available to distribute selected  Reserve(s) for allocation Account # \"" + sAccountNo + "\"." + Environment.NewLine + "do you want to continue reserves distribution for other selected accounts ?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                                    //    {
                                    //        break;
                                    //    }
                                    //}
                                    _msgCounter++;
                                    if (_msgCounter <= 10)
                                    {
                                        if (_ClaimInfo.ToString() == "")
                                        {
                                            _ClaimInfo.Append("Patient Name :\"" + selectedAccountDetails[i].sAccountPatientName + "\"  Account# : \"" + selectedAccountDetails[i].sAccountNo + "\"");
                                        }
                                        else if (_ClaimInfo.ToString() != "")
                                        {
                                            _ClaimInfo.AppendLine();
                                            _ClaimInfo.Append("Patient Name :\"" + selectedAccountDetails[i].sAccountPatientName + "\"  Account# : \"" + selectedAccountDetails[i].sAccountNo + "\"");
                                        }
                                    }
                                    else if (_msgCounter == 11)
                                    {
                                        _ClaimInfo.AppendLine();
                                        _ClaimInfo.Append("Too many claims to list");
                                    }

                                }
                                else if (selectedAccountDetails[i].dAvailableReservesAmt > 0 && (selectedAccountDetails[i].dRemainingAccountBalanceWithourNonServiceCode != selectedAccountDetails[i].dRemainingAccountBalanceWithNoServiceCode))
                                {
                                    _NonServiceClaimInfoCounter++;
                                    if (_NonServiceClaimInfoCounter <= 10)
                                    {
                                        if (_NonServiceClaimInfo.ToString() == "")
                                        {
                                            _NonServiceClaimInfo.Append("Patient Name :\"" + selectedAccountDetails[i].sAccountPatientName + "\"  Account# : \"" + selectedAccountDetails[i].sAccountNo + "\"");
                                        }
                                        else if (_NonServiceClaimInfo.ToString() != "")
                                        {
                                            _NonServiceClaimInfo.AppendLine();
                                            _NonServiceClaimInfo.Append("Patient Name :\"" + selectedAccountDetails[i].sAccountPatientName + "\"  Account# : \"" + selectedAccountDetails[i].sAccountNo + "\"");
                                        }
                                    }
                                    else if (_NonServiceClaimInfoCounter == 11)
                                    {
                                        _NonServiceClaimInfo.AppendLine();
                                        _NonServiceClaimInfo.Append("Too many claims to list");
                                    }
                                }
                                else
                                {
                                    // DesignCopayGrid(); 
                                    if (this.SelectedPaymentTrayID != 0)
                                    {
                                        _accountCounter++;
                                        if (_accountCounter <= 10)
                                        {
                                            if (_accountInfo.ToString() == "")
                                            {
                                                _accountInfo.Append("Patient Name :\"" + selectedAccountDetails[i].sAccountPatientName + "\"  Account# : \"" + selectedAccountDetails[i].sAccountNo + "\"");
                                            }
                                            else if (_accountInfo.ToString() != "")
                                            {
                                                _accountInfo.AppendLine();
                                                _accountInfo.Append("Patient Name :\"" + selectedAccountDetails[i].sAccountPatientName + "\"  Account# : \"" + selectedAccountDetails[i].sAccountNo + "\"");
                                            }
                                        }
                                        else if (_accountCounter == 11)
                                        {
                                            _accountInfo.AppendLine();
                                            _accountInfo.Append("Too many Accounts to list");
                                        }
                                    }
                                }
                            }
                        }
                        if (_ClaimInfo.ToString() != "")
                        {
                            if (this.SelectedPaymentTrayID != 0)
                            {
                                MessageBox.Show("There is no remaining Patient due available to distribute for following Account # \n\n" + _ClaimInfo.ToString() + "", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        if (_accountInfo.ToString() != "")
                        {
                            if (this.SelectedPaymentTrayID != 0)
                            {
                                MessageBox.Show("Reserve(s) selected for allocation is used by another user for following Account # \n\n " + _accountInfo + " .", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        if (_NonServiceClaimInfo.ToString() != "")
                        {
                            if (this.SelectedPaymentTrayID != 0)
                            {
                                MessageBox.Show("Auto reserves distribution for Non-Service communication charges on claim(s) will be skipped. User need to distribute reserves manually from patient payment screen for following Account# \n\n " + _NonServiceClaimInfo + " .", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        LoadReserveDistributionList();
                    }

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured while removing account from bad debt.", true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (selectedAccountDetails != null)
                {
                    selectedAccountDetails.Clear();
                    selectedAccountDetails.Dispose();
                    selectedAccountDetails = null;
                }

                if (AvailableReserveListTodistribute != null)
                {
                    AvailableReserveListTodistribute.Clear();
                    AvailableReserveListTodistribute.Dispose();
                    AvailableReserveListTodistribute = null;
                }
            }
            #endregion

            #region "AutoDistributionReserve_OLD Code"
            //DataTable dtAccountClaims = null;
            //DataTable _dtAccountDetails = null;
            //ClsReserveDistributionList oClsReserveDistributionList = new ClsReserveDistributionList();
            //string sCheckAmount="";
            //gloGeneralItem.gloItems SeletedReserveItems = new gloGeneralItem.gloItems();
            //string _InsTransferCloseDate = string.Empty;
            //try
            //{
            //    if (C1ReserveList.DataSource != null && C1ReserveList.RowSel > 0)
            //    {
                    
            //        Int64 _PatientID = Convert.ToInt64(C1ReserveList.GetData(C1ReserveList.RowSel, COL_PatientID));                   
            //        Int64 _nPAccountID = Convert.ToInt64(C1ReserveList.GetData(C1ReserveList.RowSel, COL_AccountID));
            //        Int64 _nGaurantorID = 0;
            //        Int64 _nAccountPatientID = 0;
            //        decimal _AvailableAmt = Convert.ToDecimal(C1ReserveList.GetData(C1ReserveList.RowSel, COL_Available));
            //        decimal _AccountBalance = Convert.ToDecimal(C1ReserveList.GetData(C1ReserveList.RowSel, COL_PatientDue)) + Convert.ToDecimal(C1ReserveList.GetData(C1ReserveList.RowSel, COL_BadDebtDue));
            //       // SeletedReserveItems.CheckAmount = _AvailableAmt;
            //        if (_AvailableAmt > 0)
            //        {                    
            //            SeletedReserveItems = oClsReserveDistributionList.FillUseReserveforAutoDistribution(_PatientID, _nPAccountID, _AccountBalance, _AvailableAmt, dtStartDate.Value.Date, dtEndDate.Value.Date, out sCheckAmount);
            //        }


            //        if (SeletedReserveItems != null && SeletedReserveItems.Count > 0)
            //        {
                      
            //            _dtAccountDetails = oClsReserveDistributionList.GetPatientAccountDetails(_nPAccountID, _PatientID);
            //            if (_dtAccountDetails != null && _dtAccountDetails.Rows.Count > 0)
            //            {
            //                _nGaurantorID = Convert.ToInt64(_dtAccountDetails.Rows[0]["nGuarantorID"]);
            //                _nAccountPatientID = Convert.ToInt64(_dtAccountDetails.Rows[0]["nAccountPatientID"]);
            //            }
            //            SetPaymentTrayPopup();
            //            dtAccountClaims = oClsReserveDistributionList.GetBillingTransactionAccountPatients_PAF(_nPAccountID, false, true);
            //            dtAccountClaims=  oClsReserveDistributionList.DistubuteAmount(sCheckAmount,  dtAccountClaims);
            //            if (dsInsurancePayment_TVP != null)
            //            {
            //                dsInsurancePayment_TVP = null;
            //            }
            //            frmInsTransCloseDate ofrmInsTransCloseDate = new frmInsTransCloseDate(gloGlobal.gloPMGlobal.DatabaseConnectionString, Convert.ToString(DateTime.Now.Date));
            //            ofrmInsTransCloseDate.ShowDialog(this);
            //            if (ofrmInsTransCloseDate.oDialogResult)
            //            {
            //                _InsTransferCloseDate = ofrmInsTransCloseDate.InsTransferCloseDate;
            //                if (_InsTransferCloseDate != "")
            //                {
            //                    dsInsurancePayment_TVP = new gloAccountPayment.dsPaymentTVP_V2();
            //                    oClsReserveDistributionList.SavePatientUseReservePayment(dsInsurancePayment_TVP, dtAccountClaims, _PatientID, _nPAccountID, _nGaurantorID, _nAccountPatientID, 0, Convert.ToString(sCheckAmount), SelectedPaymentTrayID, SelectedPaymentTray, Convert.ToDateTime(_InsTransferCloseDate), SeletedReserveItems);
            //                }
            //            }
            //            ofrmInsTransCloseDate.Dispose();
            //            LoadReserveDistributionList();
            //        }
            //        else if (_AccountBalance <=0)
            //        {
            //             MessageBox.Show("There is no remaining Patient due Or BadDebt due available to distribute selected  Reserve(s) for allocation ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                       
            //            { LoadReserveDistributionList(); }
            //        }
            //        else
            //        {
            //            DialogResult _resDlgRst = System.Windows.Forms.DialogResult.None;

            //            _resDlgRst = MessageBox.Show("Reserve(s) selected for allocation is/are used by another user." + Environment.NewLine + "Do you want to refresh the work list for current reserves availability?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //            if (_resDlgRst == System.Windows.Forms.DialogResult.Yes)
            //            { LoadReserveDistributionList(); }
            //        }
            //    }
            //    //else
            //    //{
            //    //    MessageBox.Show("Please select the claim", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            //    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    ex = null;
            //}
            //finally
            //{
            //    if (oClsReserveDistributionList != null) { oClsReserveDistributionList = null; }
            //    if (SeletedReserveItems != null)
            //    {
            //        SeletedReserveItems.Dispose();
            //        SeletedReserveItems = null;
            //    }
            //    if (dtAccountClaims != null)
            //    {
            //        dtAccountClaims.Dispose();
            //        dtAccountClaims = null;
            //    }
            //    if (_dtAccountDetails != null)
            //    {
            //        _dtAccountDetails.Dispose();
            //        _dtAccountDetails = null;
            //    }
            //    if (dsInsurancePayment_TVP != null)
            //    {
            //        dsInsurancePayment_TVP.Dispose();
            //        dsInsurancePayment_TVP = null;
            //    }
            //}
            #endregion
        }

        private void C1ReserveList_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                C1ReserveList.Redraw = false;
                if (e.Row == 0 && e.Col == 0)
                {
                    C1ReserveList.FinishEditing();
                    C1ReserveList.Select(0, 0);
                    DataTable dt = (DataTable)C1ReserveList.DataSource;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (C1ReserveList.GetCellCheck(e.Row, e.Col) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            dt.Select().ToList<DataRow>().ForEach(r => r["Select All"] = true);
                            C1ReserveList.DataSource = dt.Copy();
                            DesignCopayGrid();
                            C1ReserveList.Cols[0].DataType = typeof(bool);                          
                            C1ReserveList.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            C1ReserveList.SetData(0, 0, "Select All", true);
                            C1ReserveList.Cols[0].Style.TextAlign = TextAlignEnum.CenterCenter;
                            C1ReserveList.Cols[0].AllowEditing = true;
                            C1ReserveList.Cols[0].AllowSorting = false;                            
                          
                        }
                        else
                        {
                            dt.Select().ToList<DataRow>().ForEach(r => r["Select All"] = false);
                            C1ReserveList.DataSource = dt.Copy();
                            DesignCopayGrid();
                            C1ReserveList.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            C1ReserveList.SetData(0, 0, "Select All", false);
                            C1ReserveList.Cols[0].Style.TextAlign = TextAlignEnum.CenterCenter;
                            C1ReserveList.Cols[0].AllowEditing = true;
                            C1ReserveList.Cols[0].AllowSorting = false;                           
                        }
                      
                        if (dt != null) { dt.Dispose(); dt = null; }
                    }
                }

                C1ReserveList.Redraw = true;
                Cursor.Current = Cursors.Default;
                bool IsAllSelect = false;
                for (int i = 1; i <= C1ReserveList.Rows.Count - 1; i++)
                {
                    if (C1ReserveList.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        IsAllSelect = true;
                    }
                    else
                    {
                        IsAllSelect = false;
                        C1ReserveList.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                        return;
                    }

                }
                if (IsAllSelect)
                {
                    C1ReserveList.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
        }

    }
}
