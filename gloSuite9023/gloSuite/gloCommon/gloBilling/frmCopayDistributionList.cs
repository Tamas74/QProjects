using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using C1.Win.C1FlexGrid;

namespace gloBilling
{
    public partial class frmCopayDistributionList : gloGlobal.Common.TriarqFormWithFocusListner
    {
        #region " Declarations "

        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        //   private bool _IsPatientAccountFeature = false;
        Font Font_CellStyle = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        //   Font Font_Template = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        C1.Win.C1FlexGrid.CellStyle csYellowStyle;
        C1.Win.C1FlexGrid.CellStyle csGreenStyle;
        private static frmCopayDistributionList ofrmCopayDistribution;
        DataTable dtCopayList = null;
        DataTable dtPAccountID = null;
        Int64 nPAccountID = 0;
        #endregion " Declarations "

        #region "Column Declaration"

        private const int COL_CloseDate = 0;
        private const int COL_AccountID = 1;
        private const int COL_AccountNumber = 2;
        private const int COL_PatientID = 3;
        private const int COL_PatientName = 4;
        private const int COL_DOS = 5;
        private const int COL_Claim_Total = 6;
        private const int COL_Copay_Reserves = 7;
        private const int COL_Available = 8;
        private const int COL_UsedReserves = 9;
        private const int COL_MaxCloseDate = 10;
        private const int COL_COUNT = 11;

        #endregion "Column Declarations "
        public bool IsCallingFromByAcc = false;
        #region " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion " Property Procedures "
        public delegate void CloseCopayDistribution();
        public event CloseCopayDistribution OnCloseCopayDistribution;

        public frmCopayDistributionList()
        {
            try
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        #region "Constructor"

        public static frmCopayDistributionList GetInstance()
        {
            try
            {
                if (ofrmCopayDistribution == null)
                {
                    if (!ClsAutoCoapyDistributionList.CheckInstanceForSameUser())
                    {
                        ClsAutoCoapyDistributionList.CreateInsatnceForUser();
                        ofrmCopayDistribution = new frmCopayDistributionList();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            return ofrmCopayDistribution;
        }

        #endregion

        private void frmCopayDistributionList_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsCallingFromByAcc == true)
                {
                    AddGotFocusListener(this);
                    tsb_Next.Visible = false;
                    tsb_AutoDistributeCopay.Visible = false;
                    LoadCopayDistributionList();
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    tsb_CopayList.Visible = false;
                    tsb_Next.Visible = false;
                    LoadAutoCopayDistributionList();
                    OnAutoCopayLoad = false;
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void tsb_close_Click(object sender, EventArgs e)
        {
            try
            {
                OnCloseCopayDistribution();
                ofrmCopayDistribution = null;
                ClsAutoCoapyDistributionList.RemoveInstanceForUser();
               // DisposeReserveDetails(); 
                this.Close();
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {


                C1AutoCopayList.Select();
                Cursor.Current = Cursors.WaitCursor;
              //  DisposeReserveDetails();
                pnlReserveDetails.SendToBack();
                if (pnl_CopayList.Visible == true)
                {
                    if (C1CopayList.Rows.Count > 1)
                    {
                        C1CopayList.Row = 1;
                    }
                    LoadCopayDistributionList();
                }
                else if (pnl_AutoCopayDistributionList.Visible == true)
                {
                    if (C1AutoCopayList.Rows.Count > 1)
                    {
                        C1AutoCopayList.Row = 1;
                    }
                    //ReservelistRow --;
                    if (rb_ShowAll.Checked == true)
                    {
                        LoadAutoCopayDistributionList();
                    }
                    else
                    {
                        dtCopayList = ClsAutoCoapyDistributionList.getAutoCopayListForReserveDOS(dtStartDate.Value.Date, dtEndDate.Value.Date, nPAccountID);
                        DataColumn[] keyColumns = new DataColumn[1];
                        keyColumns[0] = dtPAccountID.Columns["nPAccountID"];
                        dtPAccountID.PrimaryKey = keyColumns;
                        dtPAccountID.Rows.Clear();
                        for (int i = 0; i < dtCopayList.Rows.Count; i++)
                        {
                            if (dtPAccountID.Rows.Count == 0)
                            {
                                dtPAccountID.Rows.Add(Convert.ToInt64(dtCopayList.Rows[i]["nPAccountID"]));
                            }
                            else
                            {
                                if (!dtPAccountID.Rows.Contains(Convert.ToInt64(dtCopayList.Rows[i]["nPAccountID"])))
                                {
                                    dtPAccountID.Rows.Add(Convert.ToInt64(dtCopayList.Rows[i]["nPAccountID"]));
                                }
                            }
                        }
                        LoadAutoCopayDistributionListOneByOne();
                    }
                }
                OnAutoCopayLoad = false;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (dtCopayList != null) { dtCopayList.Dispose(); dtCopayList = null; }
            }
        }

        private void LoadCopayDistributionList()
        {
            #region "Set Visible CopayList"

            pnl_AutoCopayDistributionList.Visible = false;
            pnl_CopayList.Visible = true;

            #endregion

            DataTable dtCopayList = null;
            try
            {
                dtCopayList = ClsCopayDistributionList.getCopayList(dtStartDate.Value.Date, dtEndDate.Value.Date);
                // C1CopayList.Clear();
                C1CopayList.DataSource = null;
                C1CopayList.DataSource = dtCopayList.Copy();
                DesignCopayGrid();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (dtCopayList != null) { dtCopayList.Dispose(); dtCopayList = null; }
            }
        }

        private void DesignCopayGrid()
        {
            try
            {
                int _width;
                gloC1FlexStyle.Style(C1CopayList, true);
                C1CopayList.Rows.Fixed = 1;
                C1CopayList.Cols.Count = COL_COUNT;
                C1CopayList.Cols.Fixed = 0;

                C1CopayList.SetData(0, COL_CloseDate, "Close Date");
                C1CopayList.SetData(0, COL_AccountID, "Account ID");
                C1CopayList.SetData(0, COL_AccountNumber, "Account #");
                C1CopayList.SetData(0, COL_PatientID, "Patient ID");
                C1CopayList.SetData(0, COL_PatientName, "Patient Name");
                C1CopayList.SetData(0, COL_DOS, "DOS");
                C1CopayList.SetData(0, COL_Claim_Total, "Claim Total ");
                C1CopayList.SetData(0, COL_Copay_Reserves, "Copay Reserves");
                C1CopayList.SetData(0, COL_Available, "Available");
                C1CopayList.SetData(0, COL_UsedReserves, "");
                C1CopayList.SetData(0, COL_MaxCloseDate, "Max Close Date");

                C1CopayList.Cols[COL_CloseDate].Visible = true;
                C1CopayList.Cols[COL_AccountID].Visible = false;
                C1CopayList.Cols[COL_AccountNumber].Visible = true;
                C1CopayList.Cols[COL_PatientID].Visible = false;
                C1CopayList.Cols[COL_PatientName].Visible = true;
                C1CopayList.Cols[COL_DOS].Visible = true;
                C1CopayList.Cols[COL_DOS].Format = "MM/dd/yyyy";
                C1CopayList.Cols[COL_Claim_Total].Visible = true;
                C1CopayList.Cols[COL_Copay_Reserves].Visible = true;
                C1CopayList.Cols[COL_Available].Visible = true;
                C1CopayList.Cols[COL_UsedReserves].Visible = true;
                C1CopayList.Cols[COL_MaxCloseDate].Visible = false;
                _width = this.Width - 5;

                C1CopayList.Cols[COL_CloseDate].Width = Convert.ToInt32(_width * 0.13);
                C1CopayList.Cols[COL_AccountNumber].Width = Convert.ToInt32(_width * 0.10);
                C1CopayList.Cols[COL_PatientName].Width = Convert.ToInt32(_width * 0.37);
                C1CopayList.Cols[COL_DOS].Width = Convert.ToInt32(_width * 0.07);
                C1CopayList.Cols[COL_Claim_Total].Width = Convert.ToInt32(_width * 0.10);
                C1CopayList.Cols[COL_Copay_Reserves].Width = Convert.ToInt32(_width * 0.10);
                C1CopayList.Cols[COL_Available].Width = Convert.ToInt32(_width * 0.10);
                C1CopayList.Cols[COL_UsedReserves].Width = Convert.ToInt32(_width * 0.02);

                C1CopayList.Cols[COL_CloseDate].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CopayList.Cols[COL_AccountNumber].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CopayList.Cols[COL_PatientName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CopayList.Cols[COL_DOS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CopayList.Cols[COL_Claim_Total].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CopayList.Cols[COL_Copay_Reserves].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1CopayList.Cols[COL_Available].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                C1CopayList.Cols[COL_UsedReserves].DataType = typeof(Image);
                C1CopayList.Cols[COL_UsedReserves].ImageMap = new System.Collections.Hashtable();
                C1CopayList.Cols[COL_UsedReserves].ImageMap.Add(0, null);
                C1CopayList.Cols[COL_UsedReserves].ImageMap.Add(1, global::gloBilling.Properties.Resources.StatusNormal);
                C1CopayList.Cols[COL_UsedReserves].ImageMap.Add(2, global::gloBilling.Properties.Resources.HoldClaim);
                C1CopayList.Cols[COL_UsedReserves].ImageAndText = false;
                C1CopayList.Cols[COL_UsedReserves].AllowResizing = false;
                C1CopayList.ExtendLastCol = true;

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;
                try
                {
                    if (C1CopayList.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = C1CopayList.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = C1CopayList.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = Font_CellStyle;
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = C1CopayList.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = Font_CellStyle;
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }

                try
                {
                    if (C1CopayList.Styles.Contains("cs_YellowStyle"))
                    {
                        csYellowStyle = C1CopayList.Styles["cs_YellowStyle"];
                    }
                    else
                    {
                        csYellowStyle = C1CopayList.Styles.Add("cs_YellowStyle");
                        csYellowStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 0);
                        csYellowStyle.ForeColor = System.Drawing.Color.Black;
                    }

                }
                catch
                {
                    csYellowStyle = C1CopayList.Styles.Add("cs_YellowStyle");
                    csYellowStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 0);
                    csYellowStyle.ForeColor = System.Drawing.Color.Black;
                }

                try
                {
                    if (C1CopayList.Styles.Contains("cs_GreenStyle"))
                    {
                        csGreenStyle = C1CopayList.Styles["cs_GreenStyle"];
                    }
                    else
                    {
                        csGreenStyle = C1CopayList.Styles.Add("cs_GreenStyle");
                        csGreenStyle.BackColor = System.Drawing.Color.FromArgb(0, 255, 0);
                        csGreenStyle.ForeColor = System.Drawing.Color.Black;
                    }

                }
                catch
                {
                    csGreenStyle = C1CopayList.Styles.Add("cs_GreenStyle");
                    csGreenStyle.BackColor = System.Drawing.Color.FromArgb(0, 255, 0);
                    csGreenStyle.ForeColor = System.Drawing.Color.Black;

                }

                C1CopayList.Cols[COL_Claim_Total].Style = csCurrencyStyle;
                C1CopayList.Cols[COL_Copay_Reserves].Style = csCurrencyStyle;
                C1CopayList.Cols[COL_Available].Style = csCurrencyStyle;

                C1CopayList.AllowEditing = false;
                C1CopayList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;

                //System.Drawing.Font regularFont = gloGlobal.clsgloFont.gFont;
                //System.Drawing.Font boldFont = gloGlobal.clsgloFont.gFont_BOLD;

                //C1CopayList.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                //C1CopayList.ForeColor = Color.Black;
                //C1CopayList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
                //C1CopayList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

                //// Normal Style
                //C1CopayList.Styles.Normal.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                //C1CopayList.Styles.Normal.Border.Color = System.Drawing.Color.FromArgb(99, 99, 99);
                //C1CopayList.Styles.Normal.Font = regularFont;
                //C1CopayList.Styles.Normal.ForeColor = Color.Black;

                //// Fixed Style
                //C1CopayList.Styles.Fixed.BackColor = System.Drawing.Color.FromArgb(86, 126, 211);
                //C1CopayList.Styles.Fixed.Border.Color = System.Drawing.Color.FromArgb(99, 99, 99);
                //C1CopayList.Styles.Fixed.Font = boldFont;
                //C1CopayList.Styles.Fixed.ForeColor = System.Drawing.Color.White;

                // Heighlight Style
                //C1CopayList.Styles.Highlight.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
                //C1CopayList.Styles.Highlight.Border.Color = System.Drawing.Color.FromArgb(99, 99, 99);
                //C1CopayList.Styles.Highlight.Font = regularFont;
                //C1CopayList.Styles.Highlight.ForeColor = System.Drawing.Color.Black;
                //C1CopayList.Styles.Highlight.Clear();
                // Focus Style
                //C1CopayList.Styles.Focus.BackColor = System.Drawing.Color.FromArgb(255, 224, 160);
                //C1CopayList.Styles.Focus.Border.Color = System.Drawing.Color.FromArgb(99, 99, 99);
                //C1CopayList.Styles.Focus.Font = regularFont;
                //C1CopayList.Styles.Focus.ForeColor = System.Drawing.Color.Black;

                //C1CopayList.DrawMode = DrawModeEnum.OwnerDraw;
                // C1CopayList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        //private void C1CopayList_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row > 0 && e.Col == C1CopayList.Cols[COL_Copay_Reserves].Index)
        //        {

        //            if (C1CopayList.GetData(e.Row, COL_Copay_Reserves) != null && Convert.ToInt32(C1CopayList.GetData(e.Row, COL_Copay_Reserves)) > 0)
        //            {
        //                if (C1CopayList.GetData(e.Row, COL_Available) != null && Convert.ToInt32(C1CopayList.GetData(e.Row, COL_Available)) > 0)
        //                {
        //                    C1CopayList.SetCellStyle(e.Row, COL_UsedReserves, csYellowStyle);
        //                }
        //                else
        //                {
        //                    C1CopayList.SetCellStyle(e.Row, COL_UsedReserves, csGreenStyle);
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        ex = null;                
        //    }

        //}

        private void tls_btnPatAcct_Click(object sender, EventArgs e)
        {
            gloAccountsV2.frmPatientFinancialViewV2 frm = null;
            try
            {
                GetControlSelection();
                if (C1CopayList.DataSource != null && C1CopayList.RowSel > 0)
                {
                    Int64 _PatientID = Convert.ToInt64(C1CopayList.GetData(C1CopayList.RowSel, COL_PatientID));
                    frm = new gloAccountsV2.frmPatientFinancialViewV2(_PatientID);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.ShowInTaskbar = false;
                    frm.IsCalledFromInsPmt = true;
                    frm.ShowDialog(this);
                    LoadCopayDistributionList();
                }
                else
                {
                    MessageBox.Show("Please select the claim", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (frm != null) { frm.Dispose(); frm = null; }
                SetControlSelection();
            }
        }

        private void tsb_PaymentPatient_Click(object sender, EventArgs e)
        {
            gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2 = null;
            try
            {
                GetControlSelection();
                if (C1CopayList.DataSource != null && C1CopayList.RowSel > 0)
                {
                    Int64 _PatientID = Convert.ToInt64(C1CopayList.GetData(C1CopayList.RowSel, COL_PatientID));
                    frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(_PatientID, false, 0, 0, 0, 0, EOBPaymentSubType.Other);
                    frmPatientPaymentV2.IsFromCopayReserveList = true;
                    Int64 _ACTID = Convert.ToInt64(C1CopayList.GetData(C1CopayList.RowSel, COL_AccountID));
                    frmPatientPaymentV2.PAccountID = _ACTID;
                    decimal _AvailableAmt = Convert.ToDecimal(C1CopayList.GetData(C1CopayList.RowSel, COL_Available));
                    frmPatientPaymentV2.CheckAmount = _AvailableAmt;
                    if (_AvailableAmt > 0)
                    {
                        frmPatientPaymentV2.SeletedReserveItems = FillUseReserve(_PatientID, _ACTID);
                        if (frmPatientPaymentV2.SeletedReserveItems != null && frmPatientPaymentV2.SeletedReserveItems.Count <= 0)
                        {
                            DialogResult _resDlgRst = System.Windows.Forms.DialogResult.None;

                            _resDlgRst = MessageBox.Show("Reserve(s) selected for allocation is/are used by another user." + Environment.NewLine + "Do you want to refresh the work list for current reserves availability?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (_resDlgRst == System.Windows.Forms.DialogResult.Yes)
                            { LoadCopayDistributionList(); return; }
                        }


                    }





                    if (C1CopayList.GetData(C1CopayList.RowSel, COL_MaxCloseDate) != null && C1CopayList.GetData(C1CopayList.RowSel, COL_MaxCloseDate) != DBNull.Value)
                    {
                        frmPatientPaymentV2.MaxCloseDate = Convert.ToDateTime(C1CopayList.GetData(C1CopayList.RowSel, COL_MaxCloseDate));
                    }

                    frmPatientPaymentV2.StartPosition = FormStartPosition.CenterScreen;
                    frmPatientPaymentV2.WindowState = FormWindowState.Maximized;
                    frmPatientPaymentV2.ShowInTaskbar = false;
                    frmPatientPaymentV2.ShowDialog(this);
                    LoadCopayDistributionList();
                    // }

                }
                else
                {
                    MessageBox.Show("Please select the claim", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
            finally
            {
                if (frmPatientPaymentV2 != null) { frmPatientPaymentV2.Dispose(); frmPatientPaymentV2 = null; }
                SetControlSelection();
            }

        }

        private gloGeneralItem.gloItems FillUseReserve(Int64 PatientID, Int64 PAccountID)
        {
            gloGeneralItem.gloItems oSeletedReserveItems = new gloGeneralItem.gloItems();
            gloGeneralItem.gloItem ogloItem = null;
            decimal _Amount = 0;
            Int64 _selEOBPayId = 0;
            Int64 _selEOBPayDtlId = 0;
            Int64 _selresEOBPayId = 0;
            String _selCloseDate = "";
            DataTable dtReserve = null;
            try
            {

                dtReserve = ClsCopayDistributionList.getCopayUseReserveList(PatientID, PAccountID, dtStartDate.Value.Date, dtEndDate.Value.Date);
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
                            _Amount = Convert.ToDecimal(dtReserve.Rows[i]["AvailableReserve"]);
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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
            }
            return oSeletedReserveItems;
        }

        private void frmCopayDistributionList_Shown(object sender, EventArgs e)
        {
            DesignCopayGrid();
            Cls_GlobalSettings.ControlMover(this.lblExamDxCPT, this.pnlReserveDetails);
            Cls_GlobalSettings.ControlMover(this.imgCPTDX, this.pnlReserveDetails);
            Cls_GlobalSettings.ControlMover(this.pnlRDTop, this.pnlReserveDetails);
        }

        private void frmCopayDistributionList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                RemoveGotFocusListener(this);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_CopayList_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            tsb_Next.Visible = false;
            LoadCopayDistributionList();
            Cursor.Current = Cursors.Default;
        }

        #region "Auto Copay Distribution List"

        #region "Auto Copay Variables"

        bool OnAutoCopayLoad = false;
       // gloAccountsV2.frmPaymentUseCopayReserveV2 ofrmPaymentUseCopayReserveV2 = null;        

        #endregion

        #region "Column Declaration For C1AutoCopayList"

        const int COL_PACCOUNTID = 0;
        const int COL_GUARANTORID = 1;
        const int COL_ACCOUNT_PATIENT_ID = 2;
        const int COL_BILLING_TRANSACTON_ID = 3;
        const int COL_BILLING_TRANSACTON_DETAILID = 4;
        const int COL_BILLING_TRANSACTON_LINENO = 5;
        const int COL_PATIENTID = 6;

        const int COL_ACCOUNTNO = 7;
        const int COL_PATIENTNAME = 8;
        const int COL_TOTALRESERVE = 9;
        const int COL_AVAILABLERESERVE = 10;
        const int COL_ALL_RESERVES_LIST = 11;
        const int COL_RESERVEFORDOS = 12;

        const int COL_CLAIM_NUM = 13;
        const int COL_DOS_FROM = 14;
        const int COL_CPT_CODE = 15;
        const int COL_MODIFIER = 16;
        const int COL_TOTAL_CHARGE = 17;
        const int COL_TOT_BAL_AMT = 18;
        const int COL_PAT_DUE = 19;
        const int COL_DISTRIBUTE_COPAY = 20;


        const int COL_RESERVE_CLOSE_DATE = 21;
        const int COL_DOS_TO = 22;
        const int COL_CLAIM_DATE = 23; //ClaimFromDate
        const int COL_CLAIM_CLOSE_DATE = 24;
        const int COL_FACILITY_TYPE = 25;
        const int COL_CLAIM_NO = 26;
        const int COL_CPT_DESCRIPTON = 27;
        const int COL_CHARGE = 28;
        const int COL_UNIT = 29;
        const int COL_ALLOWED = 30;
        const int COL_PROVIDERID = 31;
        const int COL_TRANSACTION_LINESTATUS = 32;
        const int COL_SENDTOFLAG = 33;
        const int COL_PREV_PAID = 34;
        const int COL_PREV_ADJ = 35;
        const int COL_PREV_PAT_ADJ = 36;
        const int COL_INS_DUE = 37;
        const int COL_BadDebt_DUE = 38;
        const int COL_PREV_PAT_PAID_AMT = 39;
        const int COL_SPLIT_CLAIM_NO = 40;
        const int COL_TRACK_TRN_ID = 41;
        const int COL_TRACK_TRN_DTL_ID = 42;
        const int COL_TRACK_SUB_CLAIM_NO = 43;
        const int COL_TRACK_IS_HOLD = 44;
        const int COL_TRACK_HOLD_INFO = 45;
        const int COL_TRACK_RES_PARTY = 46;
        const int COL_NON_SERVICECODE = 47;
        const int COL_RES_DOS = 48; //Used to Dispaly the Proper Available Amount while AutoDistributing Copay
        const int COL_AVAILABLE_RES = 49; //Used to Calculate the Proper Available Amount when Manually Copay Amount is Distributing

        const int COL_AUTOCOPAYCOUNT = 50;
        int ReservelistRow = 0;
        #endregion

        private void tsb_NewAutoDistributeCopay_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                tsb_Next.Visible = true;
                LoadAutoCopayDistributionList();
                OnAutoCopayLoad = false;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void Btn_Visiblity(object sender, EventArgs e)
        {
            try
            {
                if (pnl_AutoCopayDistributionList.Visible == true)
                {
                    tls_btnPatAcct.Visible = false;
                    tsb_PaymentPatient.Visible = false;
                    tsb_NewAutoDistributeCopay.Visible = false;
                    rb_ShowAll.Font = new Font(rb_ShowAll.Font, FontStyle.Bold);

                    tsb_CopayList.Visible = false;
                    tsb_AutoDistributeCopay.Visible = true;
                    tsb_SaveAutoCopay.Visible = true;

                    rb_ShowAll.Visible = true;
                    rb_ShowOneByOne.Visible = true;
                    lbl_ListName.Text = "Auto Copay Reserves Distribution Work List";
                }

                if (pnl_CopayList.Visible == true)
                {
                    tls_btnPatAcct.Visible = true;
                    tsb_PaymentPatient.Visible = true;
                    tsb_NewAutoDistributeCopay.Visible = false;

                    tsb_CopayList.Visible = false;
                    tsb_AutoDistributeCopay.Visible = false;
                    tsb_SaveAutoCopay.Visible = false;
                    rb_ShowAll.Visible = false;
                    rb_ShowOneByOne.Visible = false;


                    lbl_ListName.Text = " Copay Reserves Distribution Work List";

                  //  DisposeReserveDetails();
                    pnlReserveDetails.SendToBack();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void tsb_AutoDistributeCopay_Click(object sender, EventArgs e)
        {
            try
            {
                C1AutoCopayList.Select();
               // DisposeReserveDetails();
                pnlReserveDetails.SendToBack();
                
                for (int i = 1; i < C1AutoCopayList.Rows.Count; i++)
                {
                    Int64 _PAccountID = 0;
                    decimal dLineamt = 0;
                    decimal dAvailableReserve = 0;
                    string dtReserveForDOS = string.Empty;
                    OnAutoCopayLoad = true;

                    #region "Auto Distribution"

                    if (Convert.ToString(C1AutoCopayList.GetData(i, COL_AVAILABLERESERVE)) != null && Convert.ToString(C1AutoCopayList.GetData(i, COL_AVAILABLERESERVE)) != "")
                    {
                        _PAccountID = Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PACCOUNTID));
                        dAvailableReserve = Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLERESERVE));
                        if (Convert.ToString(C1AutoCopayList.GetData(i, COL_RESERVEFORDOS)) != "")
                        {
                            dtReserveForDOS = Convert.ToDateTime(C1AutoCopayList.GetData(i, COL_RESERVEFORDOS)).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            dtReserveForDOS = "";
                        }

                        for (int j = 1; j < C1AutoCopayList.Rows.Count; j++)
                        {
                            if (_PAccountID == Convert.ToInt64(C1AutoCopayList.GetData(j, COL_PACCOUNTID)) && dtReserveForDOS == Convert.ToString(C1AutoCopayList.GetData(j, COL_DOS_FROM)) && Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_DISTRIBUTE_COPAY)) == 0 && Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_TOTALRESERVE)) == 0)
                            {
                                #region "Distribute Copay for Reserve DOS is Present"

                                if (dAvailableReserve == 0)
                                {
                                    C1AutoCopayList.SetData(j, COL_DISTRIBUTE_COPAY, dAvailableReserve);
                                }
                                if (dAvailableReserve > 0)
                                {
                                    if (Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_PAT_DUE)) > 0)
                                    {
                                        dLineamt = Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_PAT_DUE));
                                    }
                                    else if (Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_TOT_BAL_AMT)) > 0)
                                    {
                                        dLineamt = Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_TOT_BAL_AMT));
                                    }

                                    if (dAvailableReserve > dLineamt)
                                    {
                                        dAvailableReserve = dAvailableReserve - dLineamt;
                                        C1AutoCopayList.SetData(j, COL_DISTRIBUTE_COPAY, dLineamt);
                                    }
                                    else
                                    {
                                        C1AutoCopayList.SetData(j, COL_DISTRIBUTE_COPAY, dAvailableReserve);
                                        dAvailableReserve = 0;
                                    }
                                }
                                //if (Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_TOTALRESERVE)) != 0)
                                //{
                                //    C1AutoCopayList.SetData(j, COL_DISTRIBUTE_COPAY, null);
                                //}

                                #endregion
                            }
                            //else if (_PAccountID == Convert.ToInt64(C1AutoCopayList.GetData(j, COL_PACCOUNTID)) && dtReserveForDOS == Convert.ToString(C1AutoCopayList.GetData(j, COL_RES_DOS)) && Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_DISTRIBUTE_COPAY)) == 0 && Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_TOTALRESERVE)) == 0)
                            {
                                #region "Distribute Copay for Reserve DOS is Not Present"

                                //if (dAvailableReserve == 0)
                                //{
                                //    C1AutoCopayList.SetData(j, COL_DISTRIBUTE_COPAY, dAvailableReserve);
                                //}
                                //if (dAvailableReserve > 0)
                                //{
                                //    if (Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_PAT_DUE)) > 0)
                                //    {
                                //        dLineamt = Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_PAT_DUE)); 
                                //    }
                                //    else if (Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_TOT_BAL_AMT)) > 0)
                                //    {
                                //        dLineamt = Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_TOT_BAL_AMT)); 
                                //    }

                                //    if (dAvailableReserve > dLineamt)
                                //    {
                                //        dAvailableReserve = dAvailableReserve - dLineamt;
                                //        C1AutoCopayList.SetData(j, COL_DISTRIBUTE_COPAY, dLineamt);
                                //    }
                                //    else
                                //    {
                                //        C1AutoCopayList.SetData(j, COL_DISTRIBUTE_COPAY, dAvailableReserve);
                                //        dAvailableReserve = 0;
                                //    }
                                //}                                

                                #endregion
                            }
                        }

                        #region "Set Available Reserve after Distribution"

                        if (_PAccountID == Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PACCOUNTID)))
                        {
                            C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, dAvailableReserve);
                        }

                        #endregion
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            { OnAutoCopayLoad = false; }
        }

        private void tsb_SaveAutoCopay_Click(object sender, EventArgs e)
        {
            try
            {
                pnlReserveDetails.SendToBack();
                C1AutoCopayList.Select();
               // DisposeReserveDetails();
                SaveAutoCopayPayment();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void C1AutoCopayList_CellChanged(object sender, RowColEventArgs e)
        {
            try
            {
                if (OnAutoCopayLoad == false)
                {
                    for (int i = 1; i < C1AutoCopayList.Rows.Count; i++)
                    {
                        if (e.Col == COL_DISTRIBUTE_COPAY && Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PACCOUNTID)) == Convert.ToInt64(C1AutoCopayList.GetData(e.Row, COL_PACCOUNTID)))
                        {
                            if ((Convert.ToString(C1AutoCopayList.GetData(i, COL_RESERVEFORDOS)) == "" && Convert.ToString(C1AutoCopayList.GetData(e.Row, COL_RES_DOS)) == "") || (Convert.ToString(C1AutoCopayList.GetData(e.Row, COL_RES_DOS)) == Convert.ToString(C1AutoCopayList.GetData(i, COL_RESERVEFORDOS))))
                            {
                                decimal dDistributedCopay = 0;//Total of DistributedCopayAmount for corresponding Reserve For DOS
                                decimal dLineamt = 0;
                                decimal dDistributeAmt = Convert.ToDecimal(C1AutoCopayList.GetData(e.Row, COL_DISTRIBUTE_COPAY));

                                if (Convert.ToDecimal(C1AutoCopayList.GetData(e.Row, COL_PAT_DUE)) > 0)
                                {
                                    dLineamt = Convert.ToDecimal(C1AutoCopayList.GetData(e.Row, COL_PAT_DUE));
                                }
                                else if (Convert.ToDecimal(C1AutoCopayList.GetData(e.Row, COL_TOT_BAL_AMT)) > 0)
                                {
                                    dLineamt = Convert.ToDecimal(C1AutoCopayList.GetData(e.Row, COL_TOT_BAL_AMT));
                                }

                                #region "Validating Manually Distributed Copay Amount"

                                if (dDistributeAmt > dLineamt)
                                {
                                    MessageBox.Show("Distributed copay amount exceeds due.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    C1AutoCopayList.SetData(e.Row, COL_DISTRIBUTE_COPAY, Convert.ToDecimal(0));

                                    #region "Calculate Available Copay"

                                    //for (int j = i; j < C1AutoCopayList.Rows.Count; j++)
                                    //{
                                    //    if (Convert.ToInt64(C1AutoCopayList.GetData(j, COL_PACCOUNTID)) == Convert.ToInt64(C1AutoCopayList.GetData(e.Row, COL_PACCOUNTID)))
                                    //    {
                                    //        if (Convert.ToString(C1AutoCopayList.GetData(i, COL_RESERVEFORDOS)) == "" && Convert.ToString(C1AutoCopayList.GetData(j, COL_RES_DOS)) == "1/1/1900 12:00:00 AM")
                                    //        {
                                    //            dAvailableReserve = dAvailableReserve + Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_DISTRIBUTE_COPAY));
                                    //        }
                                    //        else if (Convert.ToString(C1AutoCopayList.GetData(i, COL_RESERVEFORDOS)) == Convert.ToDateTime(C1AutoCopayList.GetData(j, COL_RES_DOS)).ToShortDateString())
                                    //        {
                                    //            dAvailableReserve = dAvailableReserve + Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_DISTRIBUTE_COPAY));
                                    //        }
                                    //    }
                                    //}
                                    dDistributedCopay = CalculateAvailableReserve(sender, e, i);
                                    if (dDistributedCopay > 0)
                                    {
                                        C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)) - dDistributedCopay);
                                    }
                                    else if (dDistributedCopay == 0)
                                    {
                                        C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)));
                                    }


                                    #endregion

                                    return;
                                }
                                else if (dDistributeAmt < 0)
                                {
                                    MessageBox.Show("Distributed copay amount cannot be negative,please correct the allocation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    C1AutoCopayList.SetData(e.Row, COL_DISTRIBUTE_COPAY, Convert.ToDecimal(0));

                                    #region "Calculate Available Copay"

                                    dDistributedCopay = CalculateAvailableReserve(sender, e, i);
                                    if (dDistributedCopay > 0)
                                    {
                                        C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)) - dDistributedCopay);
                                    }
                                    else if (dDistributedCopay == 0)
                                    {
                                        C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)));
                                    }

                                    #endregion

                                    return;
                                }
                                else if (dDistributeAmt == 0)
                                {
                                    #region "Calculate Available Copay"

                                    dDistributedCopay = CalculateAvailableReserve(sender, e, i);
                                    if (dDistributedCopay > 0)
                                    {
                                        C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)) - dDistributedCopay);
                                    }
                                    else if (dDistributedCopay == 0)
                                    {
                                        C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)));
                                    }

                                    #endregion

                                    return;
                                }
                                else
                                {
                                    dDistributedCopay = CalculateAvailableReserve(sender, e, i);
                                    if (dDistributedCopay > Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)))
                                    {
                                        MessageBox.Show("Distributed copay amount exceeds available copay,please correct the allocation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        C1AutoCopayList.SetData(e.Row, COL_DISTRIBUTE_COPAY, Convert.ToDecimal(0));

                                        #region "Calculate Available Copay"

                                        dDistributedCopay = CalculateAvailableReserve(sender, e, i);
                                        if (dDistributedCopay > 0)
                                        {
                                            C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)) - dDistributedCopay);
                                        }
                                        else if (dDistributedCopay == 0)
                                        {
                                            C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)));
                                        }

                                        #endregion

                                        return;
                                    }

                                    #region "Hold"

                                    //if (Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLERESERVE)) > 0)
                                    //{
                                    //    if (dDistributeAmt > Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLERESERVE)))
                                    //    {
                                    //        MessageBox.Show("Distribute copay amount exceeds available copay,please correct the allocation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //        C1AutoCopayList.SetData(e.Row, COL_DISTRIBUTE_COPAY, Convert.ToDecimal(0));

                                    //        #region "Calculate Available Copay"

                                    //        dDistributedCopay = CalculateAvailableReserve(sender, e, i);
                                    //        if (dDistributedCopay > 0)
                                    //        {
                                    //            C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)) - dDistributedCopay);
                                    //        }
                                    //        else if (dDistributedCopay == 0)
                                    //        {
                                    //            C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)));
                                    //        }

                                    //        #endregion

                                    //        return;
                                    //    }
                                    //}
                                    //else if (Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLERESERVE)) == 0)
                                    //{
                                    //    if (dDistributeAmt > Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)))
                                    //    {
                                    //        MessageBox.Show("Distribute copay amount exceeds available copay,please correct the allocation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //        C1AutoCopayList.SetData(e.Row, COL_DISTRIBUTE_COPAY, Convert.ToDecimal(0));

                                    //        #region "Calculate Available Copay"

                                    //        dDistributedCopay = CalculateAvailableReserve(sender, e, i);
                                    //        if (dDistributedCopay > 0)
                                    //        {
                                    //            C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)) - dDistributedCopay);
                                    //        }
                                    //        else if (dDistributedCopay == 0)
                                    //        {
                                    //            C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)));
                                    //        }

                                    //        #endregion

                                    //        return;
                                    //    }
                                    //}

                                    #endregion
                                }

                                #endregion

                                if (Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLERESERVE)) != Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)))
                                {
                                    #region "Calculate Available Copay"

                                    dDistributedCopay = CalculateAvailableReserve(sender, e, i);

                                    if (dDistributedCopay == 0)
                                    {
                                        C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)));
                                        return;
                                    }

                                    #endregion

                                    #region "Comment"

                                    //if (dAvailableReserve > Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)))
                                    //{
                                    //    MessageBox.Show("Distribute copay amount exceeds available available copay amount.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    C1AutoCopayList.SetData(e.Row, COL_DISTRIBUTE_COPAY, Convert.ToDecimal(0));

                                    //    #region "Calculate Available Copay"

                                    //    //for (int j = i; j < C1AutoCopayList.Rows.Count; j++)
                                    //    //{
                                    //    //    if (Convert.ToInt64(C1AutoCopayList.GetData(j, COL_PACCOUNTID)) == Convert.ToInt64(C1AutoCopayList.GetData(e.Row, COL_PACCOUNTID)))
                                    //    //    {
                                    //    //        if (Convert.ToString(C1AutoCopayList.GetData(i, COL_RESERVEFORDOS)) == "" && Convert.ToString(C1AutoCopayList.GetData(j, COL_RES_DOS)) == "1/1/1900 12:00:00 AM")
                                    //    //        {
                                    //    //            dAvailableReserve = dAvailableReserve + Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_DISTRIBUTE_COPAY));
                                    //    //        }
                                    //    //        else if (Convert.ToString(C1AutoCopayList.GetData(i, COL_RESERVEFORDOS)) == Convert.ToDateTime(C1AutoCopayList.GetData(j, COL_RES_DOS)).ToShortDateString())
                                    //    //        {
                                    //    //            dAvailableReserve = dAvailableReserve + Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_DISTRIBUTE_COPAY));
                                    //    //        }
                                    //    //    }
                                    //    //}
                                    //    dAvailableReserve = CalculateAvailableReserve(sender, e, i);
                                    //    if (dAvailableReserve > 0)
                                    //    {
                                    //        C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)) - dAvailableReserve);
                                    //    }
                                    //    else if (dAvailableReserve == 0)
                                    //    {
                                    //        C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)));
                                    //    }

                                    //    #endregion

                                    //    return;
                                    //}
                                    //else 

                                    #endregion

                                    if (dDistributedCopay <= Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)) && dDistributedCopay > 0)
                                    {
                                        C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLE_RES)) - dDistributedCopay);
                                        return;
                                    }
                                }
                                else
                                {
                                    dDistributedCopay = Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLERESERVE));
                                    dDistributedCopay = dDistributedCopay - Convert.ToDecimal(C1AutoCopayList.GetData(e.Row, COL_DISTRIBUTE_COPAY));
                                    C1AutoCopayList.SetData(i, COL_AVAILABLERESERVE, dDistributedCopay);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void C1AutoCopayList_CellButtonClick(object sender, RowColEventArgs e)
        {
            DateTime _dtReserveForDOS = DateTime.MinValue;
            Int64 _nPatientID = 0;
            Int64 _PAccountID = 0;
            //gloAccountsV2.frmPaymentUseCopayReserveV2 ofrmPaymentUseCopayReserveV2 = null;
            try
            {
                if (e.Row > 0)
                {
                    int RowIndex = e.Row;
                    DateTime _dtStartDate = dtStartDate.Value.Date;
                    DateTime _dtEndDate = dtEndDate.Value.Date;
                    _nPatientID = Convert.ToInt64(C1AutoCopayList.GetData(RowIndex, COL_PATIENTID));
                    _PAccountID = Convert.ToInt64(C1AutoCopayList.GetData(RowIndex, COL_PACCOUNTID));

                    if (Convert.ToString(C1AutoCopayList.GetData(RowIndex, COL_RESERVEFORDOS)) != "")
                    {
                        _dtReserveForDOS = Convert.ToDateTime(C1AutoCopayList.GetData(RowIndex, COL_RESERVEFORDOS));
                    }

                    if (_nPatientID != 0 && _PAccountID != 0)
                    {
                       // DisposeReserveDetails();
                        //ofrmPaymentUseCopayReserveV2 = new gloAccountsV2.frmPaymentUseCopayReserveV2(gloGlobal.gloPMGlobal.DatabaseConnectionString, _nPatientID, _PAccountID, _dtReserveForDOS, _dtStartDate, _dtEndDate);
                        //ofrmPaymentUseCopayReserveV2.IsCallingFromAutoCopayDist = true;
                        //ofrmPaymentUseCopayReserveV2.Show();
                        //ofrmPaymentUseCopayReserveV2.Dispose();
                        //ofrmPaymentUseCopayReserveV2 = null;
                        pnlReserveDetails.SendToBack();
                        this.pnlReserveDetails.Height = pictureBox1.Top + 290;
                        this.pnlReserveDetails.Width = 758;
                        this.pnlReserveDetails.Location = new Point(266, 221);
                        FillReserves(_nPatientID, _PAccountID, _dtReserveForDOS, _dtStartDate, _dtEndDate);
                        pnlReserveDetails.BringToFront();
                       
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void C1AutoCopayList_BeforeEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (e.Col == COL_ALL_RESERVES_LIST && C1AutoCopayList.GetData(e.Row, COL_AVAILABLERESERVE) == null)
                {
                    e.Cancel = true;
                }
                if (e.Col == COL_DISTRIBUTE_COPAY && C1AutoCopayList.GetData(e.Row, COL_AVAILABLERESERVE) != null)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void C1AutoCopayList_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (C1AutoCopayList.ColSel == COL_DISTRIBUTE_COPAY)
                    {
                        if (C1AutoCopayList.GetData(C1AutoCopayList.RowSel, C1AutoCopayList.ColSel) != null)
                        {
                            C1AutoCopayList.SetData(C1AutoCopayList.RowSel, C1AutoCopayList.ColSel, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void LoadAutoCopayDistributionList()
        {
            #region "Set Visible AutoCopayDistributionList"

            pnl_CopayList.Visible = false;
            pnl_AutoCopayDistributionList.Visible = true;
            //DataTable dtCopayList = null;
            #endregion

            //DisposeReserveDetails();
            pnlReserveDetails.SendToBack();
            try
            {
                dtCopayList = ClsAutoCoapyDistributionList.getAutoCopayListForReserveDOS(dtStartDate.Value.Date, dtEndDate.Value.Date, nPAccountID);

                #region "Get AccountID from Copay List
                
                dtPAccountID = new DataTable();
                dtPAccountID.Columns.Add("nPAccountID");
                DataColumn[] keyColumns = new DataColumn[1];
                keyColumns[0] = dtPAccountID.Columns["nPAccountID"];
                dtPAccountID.PrimaryKey = keyColumns;
                for (int i = 0; i < dtCopayList.Rows.Count; i++)
                {
                    if (dtPAccountID.Rows.Count == 0)
                    {
                        dtPAccountID.Rows.Add(Convert.ToInt64(dtCopayList.Rows[i]["nPAccountID"]));
                    }
                    else
                    {
                        if (!dtPAccountID.Rows.Contains(Convert.ToInt64(dtCopayList.Rows[i]["nPAccountID"])))
                        {
                            dtPAccountID.Rows.Add(Convert.ToInt64(dtCopayList.Rows[i]["nPAccountID"]));
                        }
                    }
                }

                #endregion
                C1AutoCopayList.DataSource = null;
                DesignAutoCopayGrid();
                OnAutoCopayLoad = true;

                #region "Bind Data"

                var Accounts = new List<KeyValuePair<string, string>>();

                for (int i = 0; i < dtCopayList.Rows.Count; i++)
                {

                    if (!Accounts.Contains(new KeyValuePair<string, string>(Convert.ToString(dtCopayList.Rows[i]["sAccountNo"]), Convert.ToString(dtCopayList.Rows[i]["dtReserveForDOS"]))))
                    {
                        #region "MasterData"

                        C1AutoCopayList.Rows.Add();
                        C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_ACCOUNTNO, Convert.ToString(dtCopayList.Rows[i]["sAccountNo"]));
                        C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PATIENTNAME, Convert.ToString(dtCopayList.Rows[i]["PatientName"]));

                        if (Convert.ToString(dtCopayList.Rows[i]["dtReserveForDOS"]) == "")
                        {
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_RESERVEFORDOS, "");
                        }
                        else
                        {
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_RESERVEFORDOS, Convert.ToDateTime(dtCopayList.Rows[i]["dtReserveForDOS"]).ToString("MM/dd/yyyy"));
                        }
                        C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TOTALRESERVE, Convert.ToString(dtCopayList.Rows[i]["TotalReserveAmount"]));
                        C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_AVAILABLERESERVE, Convert.ToString(dtCopayList.Rows[i]["AvailableReserve"]));

                        C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PACCOUNTID, Convert.ToString(dtCopayList.Rows[i]["nPAccountID"]));
                        C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_GUARANTORID, Convert.ToString(dtCopayList.Rows[i]["nGuarantorID"]));
                        C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_ACCOUNT_PATIENT_ID, Convert.ToString(dtCopayList.Rows[i]["nAccountPatientID"]));
                        C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PATIENTID, Convert.ToString(dtCopayList.Rows[i]["nPatientID"]));
                        C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, Convert.ToString(dtCopayList.Rows[i]["nTransactionMasterID"]));
                        C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_BILLING_TRANSACTON_DETAILID, Convert.ToString(dtCopayList.Rows[i]["nTransactionMasterDetailID"]));
                        C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_BILLING_TRANSACTON_LINENO, Convert.ToString(dtCopayList.Rows[i]["nTransactionLineNo"]));
                        C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_AVAILABLE_RES, Convert.ToString(dtCopayList.Rows[i]["AvailableReserve"]));

                        Accounts.Add(new KeyValuePair<string, string>(Convert.ToString(dtCopayList.Rows[i]["sAccountNo"]), Convert.ToString(dtCopayList.Rows[i]["dtReserveForDOS"])));

                        #endregion

                        #region "Style For Master Data"

                        C1AutoCopayList.SetCellStyle(C1AutoCopayList.Rows.Count - 1, COL_ALL_RESERVES_LIST, "cs_EditableReason");
                        C1AutoCopayList.Rows[C1AutoCopayList.Rows.Count - 1].Style = C1AutoCopayList.Styles["cs_ClaimRowStyle"];
                        C1AutoCopayList.Cols[COL_AVAILABLERESERVE].AllowEditing = false;
                        //C1AutoCopayList.Rows[C1AutoCopayList.Rows.Count - 1].AllowEditing = false;

                        #endregion
                    }

                    #region "Sub Data"

                    C1AutoCopayList.Rows.Add();
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PACCOUNTID, Convert.ToString(dtCopayList.Rows[i]["nPAccountID"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_GUARANTORID, Convert.ToString(dtCopayList.Rows[i]["nGuarantorID"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_ACCOUNT_PATIENT_ID, Convert.ToString(dtCopayList.Rows[i]["nAccountPatientID"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PATIENTID, Convert.ToString(dtCopayList.Rows[i]["nPatientID"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, Convert.ToString(dtCopayList.Rows[i]["nTransactionMasterID"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_BILLING_TRANSACTON_DETAILID, Convert.ToString(dtCopayList.Rows[i]["nTransactionMasterDetailID"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_BILLING_TRANSACTON_LINENO, Convert.ToString(dtCopayList.Rows[i]["nTransactionLineNo"]));

                    //C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CLAIM_NUM, Convert.ToString(dtCopayList.Rows[i]["ClaimNumber"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CLAIM_NUM, Convert.ToString(dtCopayList.Rows[i]["SplitClaimNo"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtCopayList.Rows[i]["nFromDate"])).ToString("MM/dd/yyyy"));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CPT_CODE, Convert.ToString(dtCopayList.Rows[i]["sCPTCode"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_MODIFIER, Convert.ToString(dtCopayList.Rows[i]["Modifier"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TOTAL_CHARGE, Convert.ToString(dtCopayList.Rows[i]["dTotal"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TOT_BAL_AMT, Convert.ToString(dtCopayList.Rows[i]["TotalBalanceAmount"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PAT_DUE, Convert.ToString(dtCopayList.Rows[i]["PatientDue"]));


                    //C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_RESERVE_CLOSE_DATE, Convert.ToString(dtCopayList.Rows[i]["dtCloseDate"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_DOS_TO, Convert.ToString(dtCopayList.Rows[i]["nToDate"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CLAIM_DATE, Convert.ToString(dtCopayList.Rows[i]["ClaimDate"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CLAIM_CLOSE_DATE, Convert.ToString(dtCopayList.Rows[i]["nCloseDate"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_FACILITY_TYPE, Convert.ToString(dtCopayList.Rows[i]["nFacilityType"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CLAIM_NO, Convert.ToString(dtCopayList.Rows[i]["nClaimNo"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CPT_DESCRIPTON, Convert.ToString(dtCopayList.Rows[i]["sCPTDescription"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CHARGE, Convert.ToString(dtCopayList.Rows[i]["dCharges"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_UNIT, Convert.ToString(dtCopayList.Rows[i]["dUnit"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_ALLOWED, Convert.ToString(dtCopayList.Rows[i]["dAllowed"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PROVIDERID, Convert.ToString(dtCopayList.Rows[i]["nProvider"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TRANSACTION_LINESTATUS, Convert.ToString(dtCopayList.Rows[i]["nTransactionLineStatus"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_SENDTOFLAG, Convert.ToString(dtCopayList.Rows[i]["nSendToFlag"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PREV_PAID, Convert.ToString(dtCopayList.Rows[i]["PreviousPaid"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PREV_ADJ, Convert.ToString(dtCopayList.Rows[i]["PreviousAdjuestment"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PREV_PAT_ADJ, Convert.ToString(dtCopayList.Rows[i]["PrevPatAdj"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_INS_DUE, Convert.ToString(dtCopayList.Rows[i]["InsuranceDue"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_BadDebt_DUE, Convert.ToString(dtCopayList.Rows[i]["BadDebtDue"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PREV_PAT_PAID_AMT, Convert.ToString(dtCopayList.Rows[i]["PreviousPatientPaidAmount"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_SPLIT_CLAIM_NO, Convert.ToString(dtCopayList.Rows[i]["SplitClaimNo"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TRACK_TRN_ID, Convert.ToString(dtCopayList.Rows[i]["TrackTransactionID"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TRACK_TRN_DTL_ID, Convert.ToString(dtCopayList.Rows[i]["TrackTransactionDetailID"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TRACK_SUB_CLAIM_NO, Convert.ToString(dtCopayList.Rows[i]["TrackSubClaimNo"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TRACK_IS_HOLD, Convert.ToString(dtCopayList.Rows[i]["TrackIsHold"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TRACK_HOLD_INFO, Convert.ToString(dtCopayList.Rows[i]["TrackHoldInfo"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TRACK_RES_PARTY, Convert.ToString(dtCopayList.Rows[i]["RespParty"]));
                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_NON_SERVICECODE, Convert.ToString(dtCopayList.Rows[i]["bNonServiceCode"]));

                    //Following Columns Only Used to Calculate Proper Avaialble Reserve Amount
                    if (Convert.ToString(dtCopayList.Rows[i]["dtReserveForDOS"]) == "")
                    {
                        C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_RES_DOS, "");
                    }
                    else
                    {
                        C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_RES_DOS, Convert.ToDateTime(dtCopayList.Rows[i]["dtReserveForDOS"]).ToString("MM/dd/yyyy"));
                    }


                    #endregion

                    #region "Style For Sub Data"

                    C1AutoCopayList.Rows[C1AutoCopayList.Rows.Count - 1].Style = C1AutoCopayList.Styles["cs_ClaimServiceRowStyle"];
                    C1AutoCopayList.SetCellStyle(C1AutoCopayList.Rows.Count - 1, COL_DISTRIBUTE_COPAY, "cs_EditableCurrencyStyle");

                    #endregion
                }

                
                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (dtCopayList != null)
                {
                    dtCopayList = null;
                }
            }
        }

        private void LoadAutoCopayDistributionListOneByOne()
        {
            //#region "Set Visible AutoCopayDistributionList"

            //pnl_CopayList.Visible = false;
            //pnl_AutoCopayDistributionList.Visible = true;

            //#endregion
            Int64 nPAccountID = 0;

           // DisposeReserveDetails();
            pnlReserveDetails.SendToBack();
            try
            {
                #region "Bind Data"

                var Accounts = new List<KeyValuePair<string, string>>();
                if (rb_ShowAll.Checked == false)
                {
                    if (dtPAccountID != null && dtPAccountID.Rows.Count > 0)
                    {
                        if (dtPAccountID.Rows.Count > 0 && dtPAccountID.Rows.Count > ReservelistRow)
                        {
                            nPAccountID = Convert.ToInt64(dtPAccountID.Rows[ReservelistRow]["nPAccountID"]);
                        }
                        else
                        {
                            tsb_Next.Enabled = false;
                            if (dtPAccountID.Rows.Count > 0 && tsb_Next.Enabled == false)
                            {
                                MessageBox.Show("You have gone through all the accounts. List will be refreshed for all the accounts.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadAutoCopayDistributionList();
                                rb_ShowAll.Checked = true;
                                rb_ShowOneByOne.Checked = false;
                            }
                            return;
                        }
                    }

                    dtCopayList = ClsAutoCoapyDistributionList.getAutoCopayListForReserveDOS(dtStartDate.Value.Date, dtEndDate.Value.Date, nPAccountID);
                    DesignAutoCopayGrid();
                    OnAutoCopayLoad = true;

                    for (int i = 0; i < dtCopayList.Rows.Count; i++)
                    {
                        if (nPAccountID == Convert.ToInt64(dtCopayList.Rows[i]["nPAccountID"]))
                        {
                          //  ReservelistRow++;
                            if (!Accounts.Contains(new KeyValuePair<string, string>(Convert.ToString(dtCopayList.Rows[i]["sAccountNo"]), Convert.ToString(dtCopayList.Rows[i]["dtReserveForDOS"]))))
                            {
                                #region "MasterData"

                                C1AutoCopayList.Rows.Add();
                                C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_ACCOUNTNO, Convert.ToString(dtCopayList.Rows[i]["sAccountNo"]));
                                C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PATIENTNAME, Convert.ToString(dtCopayList.Rows[i]["PatientName"]));

                                if (Convert.ToString(dtCopayList.Rows[i]["dtReserveForDOS"]) == "")
                                {
                                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_RESERVEFORDOS, "");
                                }
                                else
                                {
                                    C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_RESERVEFORDOS, Convert.ToDateTime(dtCopayList.Rows[i]["dtReserveForDOS"]).ToString("MM/dd/yyyy"));
                                }
                                C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TOTALRESERVE, Convert.ToString(dtCopayList.Rows[i]["TotalReserveAmount"]));
                                C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_AVAILABLERESERVE, Convert.ToString(dtCopayList.Rows[i]["AvailableReserve"]));

                                C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PACCOUNTID, Convert.ToString(dtCopayList.Rows[i]["nPAccountID"]));
                                C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_GUARANTORID, Convert.ToString(dtCopayList.Rows[i]["nGuarantorID"]));
                                C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_ACCOUNT_PATIENT_ID, Convert.ToString(dtCopayList.Rows[i]["nAccountPatientID"]));
                                C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PATIENTID, Convert.ToString(dtCopayList.Rows[i]["nPatientID"]));
                                C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, Convert.ToString(dtCopayList.Rows[i]["nTransactionMasterID"]));
                                C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_BILLING_TRANSACTON_DETAILID, Convert.ToString(dtCopayList.Rows[i]["nTransactionMasterDetailID"]));
                                C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_BILLING_TRANSACTON_LINENO, Convert.ToString(dtCopayList.Rows[i]["nTransactionLineNo"]));
                                C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_AVAILABLE_RES, Convert.ToString(dtCopayList.Rows[i]["AvailableReserve"]));

                                Accounts.Add(new KeyValuePair<string, string>(Convert.ToString(dtCopayList.Rows[i]["sAccountNo"]), Convert.ToString(dtCopayList.Rows[i]["dtReserveForDOS"])));

                                #endregion

                                #region "Style For Master Data"

                                C1AutoCopayList.SetCellStyle(C1AutoCopayList.Rows.Count - 1, COL_ALL_RESERVES_LIST, "cs_EditableReason");
                                C1AutoCopayList.Rows[C1AutoCopayList.Rows.Count - 1].Style = C1AutoCopayList.Styles["cs_ClaimRowStyle"];
                                C1AutoCopayList.Cols[COL_AVAILABLERESERVE].AllowEditing = false;
                                //C1AutoCopayList.Rows[C1AutoCopayList.Rows.Count - 1].AllowEditing = false;

                                #endregion
                            }

                            #region "Sub Data"

                            C1AutoCopayList.Rows.Add();
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PACCOUNTID, Convert.ToString(dtCopayList.Rows[i]["nPAccountID"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_GUARANTORID, Convert.ToString(dtCopayList.Rows[i]["nGuarantorID"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_ACCOUNT_PATIENT_ID, Convert.ToString(dtCopayList.Rows[i]["nAccountPatientID"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PATIENTID, Convert.ToString(dtCopayList.Rows[i]["nPatientID"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, Convert.ToString(dtCopayList.Rows[i]["nTransactionMasterID"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_BILLING_TRANSACTON_DETAILID, Convert.ToString(dtCopayList.Rows[i]["nTransactionMasterDetailID"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_BILLING_TRANSACTON_LINENO, Convert.ToString(dtCopayList.Rows[i]["nTransactionLineNo"]));

                            //C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CLAIM_NUM, Convert.ToString(dtCopayList.Rows[i]["ClaimNumber"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CLAIM_NUM, Convert.ToString(dtCopayList.Rows[i]["SplitClaimNo"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtCopayList.Rows[i]["nFromDate"])).ToString("MM/dd/yyyy"));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CPT_CODE, Convert.ToString(dtCopayList.Rows[i]["sCPTCode"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_MODIFIER, Convert.ToString(dtCopayList.Rows[i]["Modifier"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TOTAL_CHARGE, Convert.ToString(dtCopayList.Rows[i]["dTotal"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TOT_BAL_AMT, Convert.ToString(dtCopayList.Rows[i]["TotalBalanceAmount"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PAT_DUE, Convert.ToString(dtCopayList.Rows[i]["PatientDue"]));


                            //C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_RESERVE_CLOSE_DATE, Convert.ToString(dtCopayList.Rows[i]["dtCloseDate"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_DOS_TO, Convert.ToString(dtCopayList.Rows[i]["nToDate"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CLAIM_DATE, Convert.ToString(dtCopayList.Rows[i]["ClaimDate"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CLAIM_CLOSE_DATE, Convert.ToString(dtCopayList.Rows[i]["nCloseDate"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_FACILITY_TYPE, Convert.ToString(dtCopayList.Rows[i]["nFacilityType"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CLAIM_NO, Convert.ToString(dtCopayList.Rows[i]["nClaimNo"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CPT_DESCRIPTON, Convert.ToString(dtCopayList.Rows[i]["sCPTDescription"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_CHARGE, Convert.ToString(dtCopayList.Rows[i]["dCharges"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_UNIT, Convert.ToString(dtCopayList.Rows[i]["dUnit"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_ALLOWED, Convert.ToString(dtCopayList.Rows[i]["dAllowed"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PROVIDERID, Convert.ToString(dtCopayList.Rows[i]["nProvider"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TRANSACTION_LINESTATUS, Convert.ToString(dtCopayList.Rows[i]["nTransactionLineStatus"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_SENDTOFLAG, Convert.ToString(dtCopayList.Rows[i]["nSendToFlag"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PREV_PAID, Convert.ToString(dtCopayList.Rows[i]["PreviousPaid"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PREV_ADJ, Convert.ToString(dtCopayList.Rows[i]["PreviousAdjuestment"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PREV_PAT_ADJ, Convert.ToString(dtCopayList.Rows[i]["PrevPatAdj"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_INS_DUE, Convert.ToString(dtCopayList.Rows[i]["InsuranceDue"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_BadDebt_DUE, Convert.ToString(dtCopayList.Rows[i]["BadDebtDue"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_PREV_PAT_PAID_AMT, Convert.ToString(dtCopayList.Rows[i]["PreviousPatientPaidAmount"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_SPLIT_CLAIM_NO, Convert.ToString(dtCopayList.Rows[i]["SplitClaimNo"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TRACK_TRN_ID, Convert.ToString(dtCopayList.Rows[i]["TrackTransactionID"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TRACK_TRN_DTL_ID, Convert.ToString(dtCopayList.Rows[i]["TrackTransactionDetailID"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TRACK_SUB_CLAIM_NO, Convert.ToString(dtCopayList.Rows[i]["TrackSubClaimNo"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TRACK_IS_HOLD, Convert.ToString(dtCopayList.Rows[i]["TrackIsHold"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TRACK_HOLD_INFO, Convert.ToString(dtCopayList.Rows[i]["TrackHoldInfo"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_TRACK_RES_PARTY, Convert.ToString(dtCopayList.Rows[i]["RespParty"]));
                            C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_NON_SERVICECODE, Convert.ToString(dtCopayList.Rows[i]["bNonServiceCode"]));

                            //Following Columns Only Used to Calculate Proper Avaialble Reserve Amount
                            if (Convert.ToString(dtCopayList.Rows[i]["dtReserveForDOS"]) == "")
                            {
                                C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_RES_DOS, "");
                            }
                            else
                            {
                                C1AutoCopayList.SetData(C1AutoCopayList.Rows.Count - 1, COL_RES_DOS, Convert.ToDateTime(dtCopayList.Rows[i]["dtReserveForDOS"]).ToString("MM/dd/yyyy"));
                            }


                            #endregion

                            #region "Style For Sub Data"

                            C1AutoCopayList.Rows[C1AutoCopayList.Rows.Count - 1].Style = C1AutoCopayList.Styles["cs_ClaimServiceRowStyle"];
                            C1AutoCopayList.SetCellStyle(C1AutoCopayList.Rows.Count - 1, COL_DISTRIBUTE_COPAY, "cs_EditableCurrencyStyle");

                            #endregion
                        }
                    }
                }

              

                #endregion

                if (dtPAccountID != null)
                {
                    if (dtPAccountID.Rows.Count == ReservelistRow+1)
                    {
                        tsb_Next.Enabled = false;
                        tsb_SaveAutoCopay.Text = "&Save";
                        tsb_SaveAutoCopay.ToolTipText = "Save";
                    }
                    else
                    {
                        tsb_Next.Enabled = true;
                        tsb_SaveAutoCopay.Text = "&Save && Next";
                        tsb_SaveAutoCopay.ToolTipText = "Save & Next";
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (dtCopayList != null)
                {
                    dtCopayList = null;
                }
            }
        }

        private void DesignAutoCopayGrid()
        {
            try
            {
                C1AutoCopayList.Clear();
                gloC1FlexStyle.Style(C1AutoCopayList, true);
                C1AutoCopayList.AllowSorting = AllowSortingEnum.None;
                C1AutoCopayList.SelectionMode = SelectionModeEnum.Cell;

                C1AutoCopayList.Cols.Count = COL_AUTOCOPAYCOUNT;
                C1AutoCopayList.Rows.Count = 1;
                C1AutoCopayList.Rows.Fixed = 1;

                #region "Design"

                #region "Headers"


                C1AutoCopayList.SetData(0, COL_PACCOUNTID, "Pat. AccountID");
                C1AutoCopayList.SetData(0, COL_GUARANTORID, "Pat. GuarantorID");
                C1AutoCopayList.SetData(0, COL_ACCOUNT_PATIENT_ID, "Pat. AccPatID");
                C1AutoCopayList.SetData(0, COL_BILLING_TRANSACTON_ID, "Transacton ID");
                C1AutoCopayList.SetData(0, COL_BILLING_TRANSACTON_DETAILID, "Transacton Detail ID");
                C1AutoCopayList.SetData(0, COL_BILLING_TRANSACTON_LINENO, "Transacton Line No");
                C1AutoCopayList.SetData(0, COL_PATIENTID, "Patient ID");

                C1AutoCopayList.SetData(0, COL_ACCOUNTNO, "Acc. #");
                C1AutoCopayList.SetData(0, COL_PATIENTNAME, "Patient");
                C1AutoCopayList.SetData(0, COL_TOTALRESERVE, "Copay");
                C1AutoCopayList.SetData(0, COL_AVAILABLERESERVE, "Available");
                C1AutoCopayList.SetData(0, COL_ALL_RESERVES_LIST, "R");
                C1AutoCopayList.SetData(0, COL_RESERVEFORDOS, "For DOS");

                C1AutoCopayList.SetData(0, COL_CLAIM_NUM, "Claim #");
                C1AutoCopayList.SetData(0, COL_DOS_FROM, "DOS");
                C1AutoCopayList.SetData(0, COL_CPT_CODE, "CPT");
                C1AutoCopayList.SetData(0, COL_MODIFIER, "Mod");
                C1AutoCopayList.SetData(0, COL_TOTAL_CHARGE, "Charge");
                C1AutoCopayList.SetData(0, COL_TOT_BAL_AMT, "Balance");
                C1AutoCopayList.SetData(0, COL_PAT_DUE, "Pat. Due");
                C1AutoCopayList.SetData(0, COL_DISTRIBUTE_COPAY, "Distribute Copay");


                C1AutoCopayList.SetData(0, COL_RESERVE_CLOSE_DATE, "Res Close Date");
                C1AutoCopayList.SetData(0, COL_DOS_TO, "DOS To");
                C1AutoCopayList.SetData(0, COL_CLAIM_DATE, "Claim Date");
                C1AutoCopayList.SetData(0, COL_CLAIM_CLOSE_DATE, "Claim Close Date");
                C1AutoCopayList.SetData(0, COL_CPT_DESCRIPTON, "CPT Description");
                C1AutoCopayList.SetData(0, COL_FACILITY_TYPE, "Facility Type");
                C1AutoCopayList.SetData(0, COL_CLAIM_NO, "Claim No");
                C1AutoCopayList.SetData(0, COL_UNIT, "Unit");
                C1AutoCopayList.SetData(0, COL_CHARGE, "dCharge");
                C1AutoCopayList.SetData(0, COL_ALLOWED, "Allowed");
                C1AutoCopayList.SetData(0, COL_PROVIDERID, "Provider ID");
                C1AutoCopayList.SetData(0, COL_TRANSACTION_LINESTATUS, "Tran Line Status");
                C1AutoCopayList.SetData(0, COL_SENDTOFLAG, "Send To Flag");
                C1AutoCopayList.SetData(0, COL_PREV_PAID, "Prev Paid");
                C1AutoCopayList.SetData(0, COL_PREV_ADJ, "Prev Adj");
                C1AutoCopayList.SetData(0, COL_PREV_PAT_ADJ, "Prev Pat. Adj");
                C1AutoCopayList.SetData(0, COL_INS_DUE, "Ins. Due");
                C1AutoCopayList.SetData(0, COL_BadDebt_DUE, "Bad Debt Due");
                C1AutoCopayList.SetData(0, COL_PREV_PAT_PAID_AMT, "Prev. PatPaidAmt");
                C1AutoCopayList.SetData(0, COL_SPLIT_CLAIM_NO, "Split Claim No.");
                C1AutoCopayList.SetData(0, COL_TRACK_TRN_ID, "TrackTrnID");
                C1AutoCopayList.SetData(0, COL_TRACK_TRN_DTL_ID, "TrackTrnDtlID");
                C1AutoCopayList.SetData(0, COL_TRACK_SUB_CLAIM_NO, "Claim #");
                C1AutoCopayList.SetData(0, COL_TRACK_IS_HOLD, "Hold");
                C1AutoCopayList.SetData(0, COL_TRACK_HOLD_INFO, "Hold Info");
                C1AutoCopayList.SetData(0, COL_TRACK_RES_PARTY, "Party");
                C1AutoCopayList.SetData(0, COL_NON_SERVICECODE, "Non Service Code");
                C1AutoCopayList.SetData(0, COL_RES_DOS, "Res. DOS");
                C1AutoCopayList.SetData(0, COL_AVAILABLE_RES, "Ava. Res.");

                #endregion

                #region "Visible"


                C1AutoCopayList.Cols[COL_PACCOUNTID].Visible = false;
                C1AutoCopayList.Cols[COL_GUARANTORID].Visible = false;
                C1AutoCopayList.Cols[COL_ACCOUNT_PATIENT_ID].Visible = false;
                C1AutoCopayList.Cols[COL_BILLING_TRANSACTON_ID].Visible = false;
                C1AutoCopayList.Cols[COL_BILLING_TRANSACTON_DETAILID].Visible = false;
                C1AutoCopayList.Cols[COL_BILLING_TRANSACTON_LINENO].Visible = false;
                C1AutoCopayList.Cols[COL_PATIENTID].Visible = false;

                C1AutoCopayList.Cols[COL_ACCOUNTNO].Visible = true;
                C1AutoCopayList.Cols[COL_PATIENTNAME].Visible = true;
                C1AutoCopayList.Cols[COL_TOTALRESERVE].Visible = true;
                C1AutoCopayList.Cols[COL_AVAILABLERESERVE].Visible = true;
                C1AutoCopayList.Cols[COL_ALL_RESERVES_LIST].Visible = true;
                C1AutoCopayList.Cols[COL_RESERVEFORDOS].Visible = true;

                C1AutoCopayList.Cols[COL_CLAIM_NUM].Visible = true;
                C1AutoCopayList.Cols[COL_DOS_FROM].Visible = true;
                C1AutoCopayList.Cols[COL_CPT_CODE].Visible = true;
                C1AutoCopayList.Cols[COL_MODIFIER].Visible = true;
                C1AutoCopayList.Cols[COL_TOTAL_CHARGE].Visible = true;
                C1AutoCopayList.Cols[COL_TOT_BAL_AMT].Visible = true;
                C1AutoCopayList.Cols[COL_PAT_DUE].Visible = true;
                C1AutoCopayList.Cols[COL_DISTRIBUTE_COPAY].Visible = true;


                C1AutoCopayList.Cols[COL_RESERVE_CLOSE_DATE].Visible = false;
                C1AutoCopayList.Cols[COL_DOS_TO].Visible = false;
                C1AutoCopayList.Cols[COL_CLAIM_DATE].Visible = false;
                C1AutoCopayList.Cols[COL_CLAIM_CLOSE_DATE].Visible = false;
                C1AutoCopayList.Cols[COL_CPT_DESCRIPTON].Visible = false;
                C1AutoCopayList.Cols[COL_FACILITY_TYPE].Visible = false;
                C1AutoCopayList.Cols[COL_CLAIM_NO].Visible = false;
                C1AutoCopayList.Cols[COL_UNIT].Visible = false;
                C1AutoCopayList.Cols[COL_CHARGE].Visible = false;
                C1AutoCopayList.Cols[COL_ALLOWED].Visible = false;
                C1AutoCopayList.Cols[COL_PROVIDERID].Visible = false;
                C1AutoCopayList.Cols[COL_TRANSACTION_LINESTATUS].Visible = false;
                C1AutoCopayList.Cols[COL_SENDTOFLAG].Visible = false;
                C1AutoCopayList.Cols[COL_PREV_PAID].Visible = false;
                C1AutoCopayList.Cols[COL_PREV_ADJ].Visible = false;
                C1AutoCopayList.Cols[COL_PREV_PAT_ADJ].Visible = false;
                C1AutoCopayList.Cols[COL_INS_DUE].Visible = false;
                C1AutoCopayList.Cols[COL_BadDebt_DUE].Visible = false;
                C1AutoCopayList.Cols[COL_PREV_PAT_PAID_AMT].Visible = false;
                C1AutoCopayList.Cols[COL_SPLIT_CLAIM_NO].Visible = false;
                C1AutoCopayList.Cols[COL_TRACK_TRN_ID].Visible = false;
                C1AutoCopayList.Cols[COL_TRACK_TRN_DTL_ID].Visible = false;
                C1AutoCopayList.Cols[COL_TRACK_SUB_CLAIM_NO].Visible = false;
                C1AutoCopayList.Cols[COL_TRACK_IS_HOLD].Visible = false;
                C1AutoCopayList.Cols[COL_TRACK_HOLD_INFO].Visible = false;
                C1AutoCopayList.Cols[COL_TRACK_RES_PARTY].Visible = false;
                C1AutoCopayList.Cols[COL_NON_SERVICECODE].Visible = false;
                C1AutoCopayList.Cols[COL_RES_DOS].Visible = false;
                C1AutoCopayList.Cols[COL_AVAILABLE_RES].Visible = false;

                #endregion

                #region "Width"


                C1AutoCopayList.Cols[COL_PACCOUNTID].Width = 0;
                C1AutoCopayList.Cols[COL_GUARANTORID].Width = 0;
                C1AutoCopayList.Cols[COL_ACCOUNT_PATIENT_ID].Width = 0;
                C1AutoCopayList.Cols[COL_BILLING_TRANSACTON_ID].Width = 0;
                C1AutoCopayList.Cols[COL_BILLING_TRANSACTON_DETAILID].Width = 0;
                C1AutoCopayList.Cols[COL_BILLING_TRANSACTON_LINENO].Width = 0;
                C1AutoCopayList.Cols[COL_PATIENTID].Width = 0;

                C1AutoCopayList.Cols[COL_ACCOUNTNO].Width = 70;
                C1AutoCopayList.Cols[COL_PATIENTNAME].Width = 170;
                C1AutoCopayList.Cols[COL_TOTALRESERVE].Width = 100;
                C1AutoCopayList.Cols[COL_AVAILABLERESERVE].Width = 100;
                C1AutoCopayList.Cols[COL_ALL_RESERVES_LIST].Width = 18;
                C1AutoCopayList.Cols[COL_RESERVEFORDOS].Width = 80;

                C1AutoCopayList.Cols[COL_CLAIM_NUM].Width = 70;
                C1AutoCopayList.Cols[COL_DOS_FROM].Width = 80;
                C1AutoCopayList.Cols[COL_CPT_CODE].Width = 70;
                C1AutoCopayList.Cols[COL_MODIFIER].Width = 75;
                C1AutoCopayList.Cols[COL_TOTAL_CHARGE].Width = 100;
                C1AutoCopayList.Cols[COL_TOT_BAL_AMT].Width = 100;
                C1AutoCopayList.Cols[COL_PAT_DUE].Width = 100;
                C1AutoCopayList.Cols[COL_DISTRIBUTE_COPAY].Width = 110;


                C1AutoCopayList.Cols[COL_RESERVE_CLOSE_DATE].Width = 0;
                C1AutoCopayList.Cols[COL_DOS_TO].Width = 0;
                C1AutoCopayList.Cols[COL_CLAIM_DATE].Width = 0;
                C1AutoCopayList.Cols[COL_CLAIM_CLOSE_DATE].Width = 0;
                C1AutoCopayList.Cols[COL_CPT_DESCRIPTON].Width = 0;
                C1AutoCopayList.Cols[COL_FACILITY_TYPE].Width = 0;
                C1AutoCopayList.Cols[COL_CLAIM_NO].Width = 0;
                C1AutoCopayList.Cols[COL_CHARGE].Width = 0;
                C1AutoCopayList.Cols[COL_UNIT].Width = 0;
                C1AutoCopayList.Cols[COL_ALLOWED].Width = 0;
                C1AutoCopayList.Cols[COL_PROVIDERID].Width = 0;
                C1AutoCopayList.Cols[COL_TRANSACTION_LINESTATUS].Width = 0;
                C1AutoCopayList.Cols[COL_SENDTOFLAG].Width = 0;
                C1AutoCopayList.Cols[COL_PREV_PAID].Width = 0;
                C1AutoCopayList.Cols[COL_PREV_ADJ].Width = 0;
                C1AutoCopayList.Cols[COL_PREV_PAT_ADJ].Width = 0;
                C1AutoCopayList.Cols[COL_INS_DUE].Width = 0;
                C1AutoCopayList.Cols[COL_BadDebt_DUE].Width = 0;
                C1AutoCopayList.Cols[COL_PREV_PAT_PAID_AMT].Width = 0;
                C1AutoCopayList.Cols[COL_SPLIT_CLAIM_NO].Width = 0;
                C1AutoCopayList.Cols[COL_TRACK_TRN_ID].Width = 0;
                C1AutoCopayList.Cols[COL_TRACK_TRN_DTL_ID].Width = 0;
                C1AutoCopayList.Cols[COL_TRACK_SUB_CLAIM_NO].Width = 0;
                C1AutoCopayList.Cols[COL_TRACK_IS_HOLD].Width = 0;
                C1AutoCopayList.Cols[COL_TRACK_HOLD_INFO].Width = 0;
                C1AutoCopayList.Cols[COL_TRACK_RES_PARTY].Width = 0;
                C1AutoCopayList.Cols[COL_NON_SERVICECODE].Width = 0;
                C1AutoCopayList.Cols[COL_RES_DOS].Width = 0;
                C1AutoCopayList.Cols[COL_AVAILABLE_RES].Width = 0;

                #endregion

                #region "Alignment"


                C1AutoCopayList.Cols[COL_PACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_GUARANTORID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_ACCOUNT_PATIENT_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_BILLING_TRANSACTON_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_BILLING_TRANSACTON_DETAILID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_BILLING_TRANSACTON_LINENO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                C1AutoCopayList.Cols[COL_ACCOUNTNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_PATIENTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_TOTALRESERVE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1AutoCopayList.Cols[COL_AVAILABLERESERVE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1AutoCopayList.Cols[COL_ALL_RESERVES_LIST].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1AutoCopayList.Cols[COL_RESERVEFORDOS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                C1AutoCopayList.Cols[COL_CLAIM_NUM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_DOS_FROM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_CPT_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_MODIFIER].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_TOTAL_CHARGE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1AutoCopayList.Cols[COL_TOT_BAL_AMT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1AutoCopayList.Cols[COL_PAT_DUE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1AutoCopayList.Cols[COL_DISTRIBUTE_COPAY].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                C1AutoCopayList.Cols[COL_RESERVE_CLOSE_DATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_DOS_TO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_CLAIM_DATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_FACILITY_TYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_CLAIM_NO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_CHARGE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_UNIT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_ALLOWED].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_PROVIDERID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_TRANSACTION_LINESTATUS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_SENDTOFLAG].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_PREV_PAID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_PREV_ADJ].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_PREV_PAT_ADJ].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_INS_DUE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_BadDebt_DUE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_PREV_PAT_PAID_AMT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_SPLIT_CLAIM_NO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_TRACK_TRN_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_TRACK_TRN_DTL_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_TRACK_SUB_CLAIM_NO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_TRACK_IS_HOLD].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_TRACK_HOLD_INFO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_TRACK_RES_PARTY].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_NON_SERVICECODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_RES_DOS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AutoCopayList.Cols[COL_AVAILABLE_RES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                #endregion

                #region " Set Styles "

                #region "Currency Style"

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;
                try
                {
                    if (C1AutoCopayList.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = C1AutoCopayList.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = C1AutoCopayList.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = Font_CellStyle;
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }
                }
                catch
                {
                    csCurrencyStyle = C1AutoCopayList.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = Font_CellStyle;
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                }

                C1AutoCopayList.Cols[COL_TOT_BAL_AMT].Style = csCurrencyStyle;
                C1AutoCopayList.Cols[COL_PAT_DUE].Style = csCurrencyStyle;
                C1AutoCopayList.Cols[COL_TOTALRESERVE].Style = csCurrencyStyle;
                C1AutoCopayList.Cols[COL_AVAILABLERESERVE].Style = csCurrencyStyle;
                C1AutoCopayList.Cols[COL_TOTAL_CHARGE].Style = csCurrencyStyle;

                #endregion

                #region "Editable Currency Style"

                C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle;// = C1AutoCopayList.Styles.Add("cs_EditableCurrencyStyle");
                try
                {
                    if (C1AutoCopayList.Styles.Contains("cs_EditableCurrencyStyle"))
                    {
                        csEditableCurrencyStyle = C1AutoCopayList.Styles["cs_EditableCurrencyStyle"];
                    }
                    else
                    {
                        csEditableCurrencyStyle = C1AutoCopayList.Styles.Add("cs_EditableCurrencyStyle");
                        csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                        csEditableCurrencyStyle.Format = "c";
                        csEditableCurrencyStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableCurrencyStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableCurrencyStyle = C1AutoCopayList.Styles.Add("cs_EditableCurrencyStyle");
                    csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csEditableCurrencyStyle.Format = "c";
                    csEditableCurrencyStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableCurrencyStyle.BackColor = Color.White;

                }

                //C1AutoCopayList.Cols[COL_DISTRIBUTE_COPAY].Style = csEditableCurrencyStyle;

                #endregion

                #region "Claim Rows"

                C1.Win.C1FlexGrid.CellStyle csClaimRowStyle;// = C1AutoCopayList.Styles.Add("cs_ClaimRowStyle");
                try
                {
                    if (C1AutoCopayList.Styles.Contains("cs_ClaimRowStyle"))
                    {
                        csClaimRowStyle = C1AutoCopayList.Styles["cs_ClaimRowStyle"];
                    }
                    else
                    {
                        csClaimRowStyle = C1AutoCopayList.Styles.Add("cs_ClaimRowStyle");
                        csClaimRowStyle.DataType = typeof(System.String);

                        csClaimRowStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                    }
                }
                catch
                {
                    csClaimRowStyle = C1AutoCopayList.Styles.Add("cs_ClaimRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    csClaimRowStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                }

                #endregion

                #region "Claim Service Line Rows"

                C1.Win.C1FlexGrid.CellStyle csClaimServiceLineRowStyle;// = C1AutoCopayList.Styles.Add("cs_ClaimRowStyle");
                try
                {
                    if (C1AutoCopayList.Styles.Contains("cs_ClaimServiceRowStyle"))
                    {
                        csClaimServiceLineRowStyle = C1AutoCopayList.Styles["cs_ClaimServiceRowStyle"];
                    }
                    else
                    {
                        csClaimServiceLineRowStyle = C1AutoCopayList.Styles.Add("cs_ClaimServiceRowStyle");
                        csClaimServiceLineRowStyle.DataType = typeof(System.String);

                        csClaimServiceLineRowStyle.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimServiceLineRowStyle.BackColor = Color.FromArgb(252, 253, 255);
                    }

                }
                catch
                {
                    csClaimServiceLineRowStyle = C1AutoCopayList.Styles.Add("cs_ClaimServiceRowStyle");
                    csClaimServiceLineRowStyle.DataType = typeof(System.String);
                    csClaimServiceLineRowStyle.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csClaimServiceLineRowStyle.BackColor = Color.FromArgb(252, 253, 255);
                }

                #endregion

                #region "Cell Button Style"

                C1.Win.C1FlexGrid.CellStyle csEditableReason;// = c1Payment.Styles.Add("cs_EditableReason");
                try
                {
                    if (C1AutoCopayList.Styles.Contains("cs_EditableReason"))
                    {
                        csEditableReason = C1AutoCopayList.Styles["cs_EditableReason"];
                    }
                    else
                    {
                        csEditableReason = C1AutoCopayList.Styles.Add("cs_EditableReason");
                        csEditableReason.DataType = typeof(C1.Win.C1FlexGrid.C1FlexGrid);
                        csEditableReason.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableReason.BackColor = Color.FromArgb(229, 224, 236);
                        csEditableReason.ComboList = "...";
                    }
                }
                catch
                {
                    csEditableReason = C1AutoCopayList.Styles.Add("cs_EditableReason");
                    csEditableReason.DataType = typeof(C1.Win.C1FlexGrid.C1FlexGrid);
                    csEditableReason.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableReason.BackColor = Color.FromArgb(229, 224, 236);
                    csEditableReason.ComboList = "...";
                }

                #endregion

                #region "Allow Editing"

                C1AutoCopayList.AllowEditing = true;

                C1AutoCopayList.Cols[COL_PACCOUNTID].AllowEditing = false;
                C1AutoCopayList.Cols[COL_BILLING_TRANSACTON_ID].AllowEditing = false;
                C1AutoCopayList.Cols[COL_BILLING_TRANSACTON_DETAILID].AllowEditing = false;
                C1AutoCopayList.Cols[COL_BILLING_TRANSACTON_LINENO].AllowEditing = false;
                C1AutoCopayList.Cols[COL_PATIENTID].AllowEditing = false;

                C1AutoCopayList.Cols[COL_ACCOUNTNO].AllowEditing = false;
                C1AutoCopayList.Cols[COL_PATIENTNAME].AllowEditing = false;
                C1AutoCopayList.Cols[COL_TOTALRESERVE].AllowEditing = false;
                C1AutoCopayList.Cols[COL_AVAILABLERESERVE].AllowEditing = false;
                C1AutoCopayList.Cols[COL_ALL_RESERVES_LIST].AllowEditing = true;
                C1AutoCopayList.Cols[COL_RESERVEFORDOS].AllowEditing = false;

                C1AutoCopayList.Cols[COL_CLAIM_NUM].AllowEditing = false;
                C1AutoCopayList.Cols[COL_DOS_FROM].AllowEditing = false;
                C1AutoCopayList.Cols[COL_CPT_CODE].AllowEditing = false;
                C1AutoCopayList.Cols[COL_MODIFIER].AllowEditing = false;
                C1AutoCopayList.Cols[COL_TOTAL_CHARGE].AllowEditing = false;
                C1AutoCopayList.Cols[COL_TOT_BAL_AMT].AllowEditing = false;
                C1AutoCopayList.Cols[COL_PAT_DUE].AllowEditing = false;
                C1AutoCopayList.Cols[COL_DISTRIBUTE_COPAY].AllowEditing = true;


                C1AutoCopayList.Cols[COL_RESERVE_CLOSE_DATE].AllowEditing = false;
                C1AutoCopayList.Cols[COL_DOS_TO].AllowEditing = false;
                C1AutoCopayList.Cols[COL_CLAIM_DATE].AllowEditing = false;
                C1AutoCopayList.Cols[COL_CLAIM_CLOSE_DATE].AllowEditing = false;
                C1AutoCopayList.Cols[COL_CPT_DESCRIPTON].AllowEditing = false;
                C1AutoCopayList.Cols[COL_FACILITY_TYPE].AllowEditing = false;
                C1AutoCopayList.Cols[COL_CLAIM_NO].AllowEditing = false;
                C1AutoCopayList.Cols[COL_UNIT].AllowEditing = false;
                C1AutoCopayList.Cols[COL_CHARGE].AllowEditing = false;
                C1AutoCopayList.Cols[COL_ALLOWED].AllowEditing = false;
                C1AutoCopayList.Cols[COL_PROVIDERID].AllowEditing = false;
                C1AutoCopayList.Cols[COL_TRANSACTION_LINESTATUS].AllowEditing = false;
                C1AutoCopayList.Cols[COL_SENDTOFLAG].AllowEditing = false;
                C1AutoCopayList.Cols[COL_PREV_PAID].AllowEditing = false;
                C1AutoCopayList.Cols[COL_PREV_ADJ].AllowEditing = false;
                C1AutoCopayList.Cols[COL_PREV_PAT_ADJ].AllowEditing = false;
                C1AutoCopayList.Cols[COL_INS_DUE].AllowEditing = false;
                C1AutoCopayList.Cols[COL_BadDebt_DUE].AllowEditing = false;
                C1AutoCopayList.Cols[COL_PREV_PAT_PAID_AMT].AllowEditing = false;
                C1AutoCopayList.Cols[COL_SPLIT_CLAIM_NO].AllowEditing = false;
                C1AutoCopayList.Cols[COL_TRACK_TRN_ID].AllowEditing = false;
                C1AutoCopayList.Cols[COL_TRACK_TRN_DTL_ID].AllowEditing = false;
                C1AutoCopayList.Cols[COL_TRACK_SUB_CLAIM_NO].AllowEditing = false;
                C1AutoCopayList.Cols[COL_TRACK_IS_HOLD].AllowEditing = false;
                C1AutoCopayList.Cols[COL_TRACK_HOLD_INFO].AllowEditing = false;
                C1AutoCopayList.Cols[COL_TRACK_RES_PARTY].AllowEditing = false;
                C1AutoCopayList.Cols[COL_NON_SERVICECODE].AllowEditing = false;
                C1AutoCopayList.Cols[COL_RES_DOS].AllowEditing = false;
                C1AutoCopayList.Cols[COL_AVAILABLE_RES].AllowEditing = false;

                #endregion

                C1AutoCopayList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                C1AutoCopayList.ExtendLastCol = true;

                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void SaveAutoCopayPayment()
        {
            try
            {
                Int64 _PAccountID = 0;
                Int64 SelectedPaymentTrayID = 0;
                string sSelectedPaymentTray = string.Empty;
                frmPaymentTransferInfo ofrmPaymentTransferInfo = new frmPaymentTransferInfo();
                DateTime dtCloseDate = DateTime.MinValue;
                Int16 _msgCounter = 0;
                StringBuilder _ClaimInfo = new StringBuilder();
              
                //Int16 _msgReserveCloseDateCounter = 0;
                //StringBuilder _ReserveCloseDateInfo = new StringBuilder();

                Int16 _msgReserveCounter = 0;
                StringBuilder _reserveInfo = new StringBuilder();
                gloGeneralItem.gloItem ogloItem = null;
                gloAccountPayment.BulkPaymentOperation bulkWriteOff = null;

                string sAccountID = string.Empty;
                List<string> Acc = new List<string>();

                Int64 nToSkipAccountID = 0;

                #region Check Amount is Distributed or Not

                if (C1AutoCopayList.Rows.Count > 1)
                {
                    bool IsAmountDistribute = false;
                    for (int i = 1; i < C1AutoCopayList.Rows.Count; i++)
                    {
                        if (Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_DISTRIBUTE_COPAY)) != 0)
                        {
                            IsAmountDistribute = true;
                            break;
                        }
                        else
                        {
                            IsAmountDistribute = false;
                        }

                    }
                    if (IsAmountDistribute == false)
                    {
                        MessageBox.Show("No payment has been made to save.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                #endregion

                    #region "Accounts"

                    for (int i = 1; i < C1AutoCopayList.Rows.Count; i++)
                    {
                        if (Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PACCOUNTID)) != 0 && Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_AVAILABLERESERVE)) == 0 && (Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_DISTRIBUTE_COPAY)) != 0))
                        {
                            if (sAccountID != string.Empty && !Acc.Contains(Convert.ToString(C1AutoCopayList.GetData(i, COL_PACCOUNTID))))
                            {
                                sAccountID = sAccountID + "," + Convert.ToString(C1AutoCopayList.GetData(i, COL_PACCOUNTID));
                                Acc.Add(Convert.ToString(C1AutoCopayList.GetData(i, COL_PACCOUNTID)));
                            }
                            else if (sAccountID == string.Empty)
                            {
                                sAccountID = Convert.ToString(C1AutoCopayList.GetData(i, COL_PACCOUNTID));
                                Acc.Add(Convert.ToString(C1AutoCopayList.GetData(i, COL_PACCOUNTID)));
                            }
                        }
                    }

                    #endregion

                    #region "Payment Tray"

                    if (SelectedPaymentTrayID == 0)
                    {
                        ofrmPaymentTransferInfo = new frmPaymentTransferInfo();
                        ofrmPaymentTransferInfo.IsForAutoDistribution = true;
                        ofrmPaymentTransferInfo.AccountIDs = sAccountID;
                        ofrmPaymentTransferInfo.ShowDialog();
                        if (ofrmPaymentTransferInfo.PaymentTrayID > 0)
                        {
                            sSelectedPaymentTray = ofrmPaymentTransferInfo.PaymentTrayName;
                            SelectedPaymentTrayID = ofrmPaymentTransferInfo.PaymentTrayID;
                        }
                        else
                        {
                            sSelectedPaymentTray = string.Empty;
                            SelectedPaymentTrayID = 0;
                        }
                    }
                    if (ofrmPaymentTransferInfo.PaymentTransferCloseDate == string.Empty || SelectedPaymentTrayID == 0)
                    {
                        return;
                    }

                    #endregion

                    DataTable dtCopayReserves = null;
                    foreach (object o in Acc)
                    {
                        gloAccountPayment.PaymentInfoParameter paymentParameter = null;
                        gloGeneralItem.gloItems oSeletedReserveItems = new gloGeneralItem.gloItems();
                        DataTable dtChargeLineDetails = new DataTable();

                        #region "Columns"

                        dtChargeLineDetails.Columns.Add("nTransactionDetailID");
                        dtChargeLineDetails.Columns.Add("nTransactionLineNo");
                        dtChargeLineDetails.Columns.Add("ClaimNumber");
                        dtChargeLineDetails.Columns.Add("nFromDate");
                        dtChargeLineDetails.Columns.Add("nToDate");
                        dtChargeLineDetails.Columns.Add("sCPTCode");
                        dtChargeLineDetails.Columns.Add("ClaimDate");
                        dtChargeLineDetails.Columns.Add("nTransactionID");
                        dtChargeLineDetails.Columns.Add("nTransactionDate");
                        dtChargeLineDetails.Columns.Add("nFacilityType");
                        dtChargeLineDetails.Columns.Add("nClaimNo");
                        dtChargeLineDetails.Columns.Add("nPatientName");
                        dtChargeLineDetails.Columns.Add("nPatientID");
                        dtChargeLineDetails.Columns.Add("Modifier");
                        dtChargeLineDetails.Columns.Add("sCPTDescription");
                        dtChargeLineDetails.Columns.Add("dCharges");
                        dtChargeLineDetails.Columns.Add("dUnit");
                        dtChargeLineDetails.Columns.Add("dTotal");
                        dtChargeLineDetails.Columns.Add("dAllowed");
                        dtChargeLineDetails.Columns.Add("nProviderID");
                        dtChargeLineDetails.Columns.Add("nTransactionLineStatus");
                        dtChargeLineDetails.Columns.Add("nSendToFlag");
                        dtChargeLineDetails.Columns.Add("TotalBalanceAmount");
                        dtChargeLineDetails.Columns.Add("PreviousPaid");
                        dtChargeLineDetails.Columns.Add("PreviousAdjuestment");
                        dtChargeLineDetails.Columns.Add("PrevPatAdj");
                        dtChargeLineDetails.Columns.Add("PatientDue");
                        dtChargeLineDetails.Columns.Add("InsuranceDue");
                        dtChargeLineDetails.Columns.Add("BadDebtDue");
                        dtChargeLineDetails.Columns.Add("PreviousPatientPaidAmount");
                        dtChargeLineDetails.Columns.Add("SplitClaimNo");
                        dtChargeLineDetails.Columns.Add("TrackTransactionID");
                        dtChargeLineDetails.Columns.Add("TrackTransactionDetailID");
                        dtChargeLineDetails.Columns.Add("TrackSubClaimNo");
                        dtChargeLineDetails.Columns.Add("TrackIsHold");
                        dtChargeLineDetails.Columns.Add("TrackHoldInfo");
                        dtChargeLineDetails.Columns.Add("RespParty");
                        dtChargeLineDetails.Columns.Add("bNonServiceCode");
                        dtChargeLineDetails.Columns.Add("dPayment");

                        #endregion

                        _PAccountID = Convert.ToInt64(o);
                        nToSkipAccountID = 0;
                        for (int i = 1; i < C1AutoCopayList.Rows.Count; i++)
                        {
                            if (Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PACCOUNTID)) == _PAccountID && Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PACCOUNTID)) != nToSkipAccountID)
                            {
                              

                                dtCloseDate = Convert.ToDateTime(ofrmPaymentTransferInfo.PaymentTransferCloseDate);
                                if (dtCopayReserves != null && dtCopayReserves.Rows.Count >0 && Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_DISTRIBUTE_COPAY)) != 0 && Convert.ToString(dtCopayReserves.Rows[0]["dtReserveForDOS"]) == Convert.ToDateTime(C1AutoCopayList.GetData(i, COL_DOS_FROM)).ToString("MM/dd/yyyy"))
                                {
                                  

                                    paymentParameter = new gloAccountPayment.PaymentInfoParameter(
                                        Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PATIENTID)),
                                        Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PACCOUNTID)),
                                        Convert.ToInt64(C1AutoCopayList.GetData(i, COL_GUARANTORID)),
                                        Convert.ToInt64(C1AutoCopayList.GetData(i, COL_ACCOUNT_PATIENT_ID)),
                                        dtCloseDate,
                                        SelectedPaymentTrayID,
                                        sSelectedPaymentTray,
                                        "0.00",
                                        "0.00",
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_DISTRIBUTE_COPAY))
                                        );

                                    Decimal dDistributeCopayReserveamt = 0;
                                    Decimal dDistributeamt = Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_DISTRIBUTE_COPAY));
                                    for (int k = 0; k < dtCopayReserves.Rows.Count; k++)
                                    {
                                        if (dDistributeamt > 0 && Convert.ToDecimal(dtCopayReserves.Rows[k]["nAccountID"]) == _PAccountID && Convert.ToDecimal(dtCopayReserves.Rows[k]["AvailableReserve"]) > 0)
                                        {
                                            if (dDistributeamt >= Convert.ToDecimal(dtCopayReserves.Rows[k]["AvailableReserve"]))
                                            {
                                                ogloItem = new gloGeneralItem.gloItem(Convert.ToInt64(dtCopayReserves.Rows[k]["nEOBPaymentID"]), Convert.ToString(dtCopayReserves.Rows[k]["nEOBPaymentDetailID"]), Convert.ToString(dtCopayReserves.Rows[k]["AvailableReserve"]));
                                                dDistributeCopayReserveamt = dDistributeCopayReserveamt + Convert.ToDecimal(dtCopayReserves.Rows[k]["AvailableReserve"]);
                                                dDistributeamt = dDistributeamt - Convert.ToDecimal(dtCopayReserves.Rows[k]["AvailableReserve"]);
                                                dtCopayReserves.Rows[k]["AvailableReserve"] = 0;
                                            }
                                            else
                                            {
                                                ogloItem = new gloGeneralItem.gloItem(Convert.ToInt64(dtCopayReserves.Rows[k]["nEOBPaymentID"]), Convert.ToString(dtCopayReserves.Rows[k]["nEOBPaymentDetailID"]), Convert.ToString(dDistributeamt));
                                                dDistributeCopayReserveamt = dDistributeCopayReserveamt + dDistributeamt;
                                                dtCopayReserves.Rows[k]["AvailableReserve"] = Convert.ToDecimal(dtCopayReserves.Rows[k]["AvailableReserve"]) - dDistributeamt;
                                                dDistributeamt = 0;
                                            }
                                            ogloItem.SubItems.Add(Convert.ToInt64(dtCopayReserves.Rows[k]["nReserveID"]), "0", "0", Convert.ToString(dtCopayReserves.Rows[k]["dtCloseDate"]));
                                            oSeletedReserveItems.Add(ogloItem);
                                        }
                                    }
                                    if (dDistributeCopayReserveamt > 0 && Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_DISTRIBUTE_COPAY)) != dDistributeCopayReserveamt)
                                    {
                                        nToSkipAccountID = Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PACCOUNTID));
                                        _msgCounter++;
                                        if (_msgCounter <= 10)
                                        {
                                            if (_ClaimInfo.ToString() == "")
                                            {
                                                _ClaimInfo.Append("Claim # :\"" + Convert.ToInt32(C1AutoCopayList.GetData(i, COL_CLAIM_NO)) + "\"  CPT Code : \"" + Convert.ToString(C1AutoCopayList.GetData(i, COL_CPT_CODE)) + "\"  DOS : \"" + Convert.ToString(C1AutoCopayList.GetData(i, COL_DOS_FROM)) + "\"");
                                            }
                                            else if (_ClaimInfo.ToString() != "")
                                            {
                                                _ClaimInfo.AppendLine();
                                                _ClaimInfo.Append("Claim # :\"" + Convert.ToInt32(C1AutoCopayList.GetData(i, COL_CLAIM_NO)) + "\"  CPT Code : \"" + Convert.ToString(C1AutoCopayList.GetData(i, COL_CPT_CODE)) + "\"  DOS : \"" + Convert.ToString(C1AutoCopayList.GetData(i, COL_DOS_FROM)) + "\"");
                                            }
                                        }
                                        else if (_msgCounter == 11)
                                        {
                                            _ClaimInfo.AppendLine();
                                            _ClaimInfo.Append("Too many claims to list");
                                        }
                                     
                                        continue;
                                    }
                                    else
                                    {
                                        nToSkipAccountID = 0;
                                    }


                                    #region "DataTable"

                                    dtChargeLineDetails.Rows.Add(Convert.ToString(C1AutoCopayList.GetData(i, COL_BILLING_TRANSACTON_DETAILID)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_BILLING_TRANSACTON_LINENO)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_CLAIM_NUM)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_DOS_FROM)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_DOS_TO)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_CPT_CODE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_CLAIM_DATE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_BILLING_TRANSACTON_ID)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_CLAIM_CLOSE_DATE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_FACILITY_TYPE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_CLAIM_NO)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PATIENTNAME)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PATIENTID)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_MODIFIER)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_CPT_DESCRIPTON)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_CHARGE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_UNIT)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TOTAL_CHARGE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_ALLOWED)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PROVIDERID)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TRANSACTION_LINESTATUS)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_SENDTOFLAG)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TOT_BAL_AMT)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PREV_PAID)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PREV_ADJ)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PREV_PAT_ADJ)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PAT_DUE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_INS_DUE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_BadDebt_DUE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PREV_PAT_PAID_AMT)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_SPLIT_CLAIM_NO)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TRACK_TRN_ID)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TRACK_TRN_DTL_ID)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TRACK_SUB_CLAIM_NO)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TRACK_IS_HOLD)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TRACK_HOLD_INFO)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TRACK_RES_PARTY)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_NON_SERVICECODE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_DISTRIBUTE_COPAY)));

                                    #endregion

                                }
                                else if (dtCopayReserves != null && dtCopayReserves.Rows.Count > 0 && Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_DISTRIBUTE_COPAY)) != 0 && Convert.ToString((C1AutoCopayList.GetData(i, COL_RESERVEFORDOS))) == "")
                                {
                                    dtCloseDate = Convert.ToDateTime(ofrmPaymentTransferInfo.PaymentTransferCloseDate);

                                    paymentParameter = new gloAccountPayment.PaymentInfoParameter(
                                        Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PATIENTID)),
                                        Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PACCOUNTID)),
                                        Convert.ToInt64(C1AutoCopayList.GetData(i, COL_GUARANTORID)),
                                        Convert.ToInt64(C1AutoCopayList.GetData(i, COL_ACCOUNT_PATIENT_ID)),
                                        dtCloseDate,
                                        SelectedPaymentTrayID,
                                        sSelectedPaymentTray,
                                        "0.00",
                                        "0.00",
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_DISTRIBUTE_COPAY))
                                        );

                                    Decimal dDistributeCopayReserveamt = 0;
                                    Decimal dDistributeamt = Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_DISTRIBUTE_COPAY));
                                    for (int k = 0; k < dtCopayReserves.Rows.Count; k++)
                                    {
                                        if (Convert.ToDecimal(dtCopayReserves.Rows[k]["nAccountID"]) != _PAccountID)
                                        {
                                            dtCopayReserves = ClsAutoCoapyDistributionList.getPatientAvailableReserve(Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PATIENTID)), Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PACCOUNTID)), Convert.ToString(C1AutoCopayList.GetData(i, COL_RES_DOS)), dtStartDate.Value.Date, dtEndDate.Value.Date);
                                        }
                                        if (dDistributeamt > 0 && Convert.ToDecimal(dtCopayReserves.Rows[k]["nAccountID"]) == _PAccountID && Convert.ToDecimal(dtCopayReserves.Rows[k]["AvailableReserve"]) > 0)
                                        {
                                            if (dDistributeamt >= Convert.ToDecimal(dtCopayReserves.Rows[k]["AvailableReserve"]))
                                            {
                                                ogloItem = new gloGeneralItem.gloItem(Convert.ToInt64(dtCopayReserves.Rows[k]["nEOBPaymentID"]), Convert.ToString(dtCopayReserves.Rows[k]["nEOBPaymentDetailID"]), Convert.ToString(dtCopayReserves.Rows[k]["AvailableReserve"]));
                                                dDistributeCopayReserveamt = dDistributeCopayReserveamt + Convert.ToDecimal(dtCopayReserves.Rows[k]["AvailableReserve"]);
                                                dDistributeamt = dDistributeamt - Convert.ToDecimal(dtCopayReserves.Rows[k]["AvailableReserve"]);
                                                dtCopayReserves.Rows[k]["AvailableReserve"] = 0;
                                            }
                                            else
                                            {
                                                ogloItem = new gloGeneralItem.gloItem(Convert.ToInt64(dtCopayReserves.Rows[k]["nEOBPaymentID"]), Convert.ToString(dtCopayReserves.Rows[k]["nEOBPaymentDetailID"]), Convert.ToString(dDistributeamt));
                                                dDistributeCopayReserveamt = dDistributeCopayReserveamt + dDistributeamt;
                                                dtCopayReserves.Rows[k]["AvailableReserve"] = Convert.ToDecimal(dtCopayReserves.Rows[k]["AvailableReserve"]) - dDistributeamt;
                                                dDistributeamt = 0;
                                            }
                                            ogloItem.SubItems.Add(Convert.ToInt64(dtCopayReserves.Rows[k]["nReserveID"]), "0", "0", Convert.ToString(dtCopayReserves.Rows[k]["dtCloseDate"]));
                                            oSeletedReserveItems.Add(ogloItem);
                                        }
                                    }
                                    if (dDistributeCopayReserveamt > 0 && Convert.ToDecimal(C1AutoCopayList.GetData(i, COL_DISTRIBUTE_COPAY)) != dDistributeCopayReserveamt)
                                    {
                                        // MessageBox.Show("Reserve allocation for Account # : " + ClsAutoCoapyDistributionList.GetDispalyAccountNo(_PAccountID) + "\nClaim # : " + Convert.ToInt32(C1AutoCopayList.GetData(i, COL_CLAIM_NO)) + " , DOS : " + Convert.ToString(C1AutoCopayList.GetData(i, COL_DOS_FROM)) + " is already used.\nReallocate the amount .", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        nToSkipAccountID = Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PACCOUNTID));
                                        _msgCounter++;
                                        if (_msgCounter <= 10)
                                        {
                                            if (_ClaimInfo.ToString() == "")
                                            {
                                                _ClaimInfo.Append("Claim # :\"" + Convert.ToInt32(C1AutoCopayList.GetData(i, COL_CLAIM_NO)) + "\"  CPT Code : \"" + Convert.ToString(C1AutoCopayList.GetData(i, COL_CPT_CODE)) + "\"  DOS : \"" + Convert.ToString(C1AutoCopayList.GetData(i, COL_DOS_FROM)) + "\"");
                                            }
                                            else if (_ClaimInfo.ToString() != "")
                                            {
                                                _ClaimInfo.AppendLine();
                                                _ClaimInfo.Append("Claim # :\"" + Convert.ToInt32(C1AutoCopayList.GetData(i, COL_CLAIM_NO)) + "\"  CPT Code : \"" + Convert.ToString(C1AutoCopayList.GetData(i, COL_CPT_CODE)) + "\"  DOS : \"" + Convert.ToString(C1AutoCopayList.GetData(i, COL_DOS_FROM)) + "\"");
                                            }
                                        }
                                        else if (_msgCounter == 11)
                                        {
                                            _ClaimInfo.AppendLine();
                                            _ClaimInfo.Append("Too many claims to list");
                                        }

                                        continue;
                                    }
                                    else
                                    {
                                        nToSkipAccountID = 0;
                                    }
                                    #region "DataTable"

                                    dtChargeLineDetails.Rows.Add(Convert.ToString(C1AutoCopayList.GetData(i, COL_BILLING_TRANSACTON_DETAILID)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_BILLING_TRANSACTON_LINENO)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_CLAIM_NUM)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_DOS_FROM)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_DOS_TO)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_CPT_CODE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_CLAIM_DATE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_BILLING_TRANSACTON_ID)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_CLAIM_CLOSE_DATE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_FACILITY_TYPE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_CLAIM_NO)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PATIENTNAME)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PATIENTID)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_MODIFIER)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_CPT_DESCRIPTON)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_CHARGE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_UNIT)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TOTAL_CHARGE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_ALLOWED)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PROVIDERID)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TRANSACTION_LINESTATUS)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_SENDTOFLAG)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TOT_BAL_AMT)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PREV_PAID)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PREV_ADJ)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PREV_PAT_ADJ)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PAT_DUE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_INS_DUE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_BadDebt_DUE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_PREV_PAT_PAID_AMT)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_SPLIT_CLAIM_NO)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TRACK_TRN_ID)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TRACK_TRN_DTL_ID)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TRACK_SUB_CLAIM_NO)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TRACK_IS_HOLD)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TRACK_HOLD_INFO)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_TRACK_RES_PARTY)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_NON_SERVICECODE)),
                                        Convert.ToString(C1AutoCopayList.GetData(i, COL_DISTRIBUTE_COPAY)));

                                    #endregion
                                }
                                else
                                {
                                    if (Convert.ToString(C1AutoCopayList.GetData(i, COL_RESERVEFORDOS)) == "")
                                    {
                                        dtCopayReserves = ClsAutoCoapyDistributionList.getPatientAvailableReserve(Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PATIENTID)), Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PACCOUNTID)), Convert.ToString(C1AutoCopayList.GetData(i, COL_RES_DOS)), dtStartDate.Value.Date, dtEndDate.Value.Date);
                                        #region "Reserve Distribution close date > reserve date handled while accepting close date to maintain consistency for validation"
                                        //if (dtCopayReserves != null)
                                        //{

                                        //    var result = dtCopayReserves.Select("dtClosedate > #" + dtCloseDate.ToShortDateString() + "#"); //dtCopayReserves.AsEnumerable().Where(r => r.Field<DateTime>("dtClosedate") > dtCloseDate);

                                        //    if (result != null && result.Length > 0)
                                        //    {
                                        //        foreach (DataRow dr in result)
                                        //        {

                                        //            _msgReserveCloseDateCounter++;
                                        //            if (_msgReserveCloseDateCounter <= 10)
                                        //            {
                                        //                if (_ReserveCloseDateInfo.ToString() == "")
                                        //                {
                                        //                    _ReserveCloseDateInfo.Append(" Account #  : " + ClsAutoCoapyDistributionList.GetDispalyAccountNo(Convert.ToInt64(dr["nAccountID"])));
                                        //                }
                                        //                else if (_ReserveCloseDateInfo.ToString() != "")
                                        //                {
                                        //                    _ReserveCloseDateInfo.AppendLine();
                                        //                    _ReserveCloseDateInfo.Append(" Account #  : " + ClsAutoCoapyDistributionList.GetDispalyAccountNo(Convert.ToInt64(dr["nAccountID"])));
                                        //                }
                                        //            }
                                        //            else if (_msgReserveCloseDateCounter == 11)
                                        //            {
                                        //                _ReserveCloseDateInfo.AppendLine();
                                        //                _ReserveCloseDateInfo.Append("Too many claims to list");
                                        //            }
                                        //            nToSkipAccountID = Convert.ToInt64(dr["nAccountID"]);
                                        //            //dtCopayReserves = null;
                                        //            break;
                                        //        }
                                        //        continue;
                                        //    }
                                        //    else
                                        //    {
                                        //        nToSkipAccountID = 0;
                                        //    }
                                        //}
                                        #endregion
                                    }
                                    else
                                    {
                                        dtCopayReserves = ClsAutoCoapyDistributionList.getPatientAvailableReserve(Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PATIENTID)), Convert.ToInt64(C1AutoCopayList.GetData(i, COL_PACCOUNTID)), Convert.ToString(C1AutoCopayList.GetData(i, COL_RESERVEFORDOS)), dtStartDate.Value.Date, dtEndDate.Value.Date);
                                        #region "Reserve Distribution close date > reserve date handled while accepting close date to maintain consistency for validation"
                                        //var _dr = from row in dtCopayReserves.AsEnumerable()
                                        //          where row.Field<DateTime>("dtClosedate") > dtCloseDate
                                        //          select row.Field<DataRow[]>("dtCloseDate");
                                        //if (dtCopayReserves != null)
                                        //{

                                        //    var result = dtCopayReserves.Select("dtClosedate > #" + dtCloseDate.ToShortDateString() + "#"); //dtCopayReserves.AsEnumerable().Where(r => r.Field<DateTime>("dtClosedate") > dtCloseDate);

                                        //    if (result != null && result.Length > 0)
                                        //    {
                                        //        foreach (DataRow dr in result)
                                        //        {

                                        //            _msgReserveCloseDateCounter++;
                                        //            if (_msgReserveCloseDateCounter <= 10)
                                        //            {
                                        //                if (_ReserveCloseDateInfo.ToString() == "")
                                        //                {
                                        //                    _ReserveCloseDateInfo.Append(" Account #  : " + ClsAutoCoapyDistributionList.GetDispalyAccountNo(Convert.ToInt64(dr["nAccountID"])));
                                        //                }
                                        //                else if (_ReserveCloseDateInfo.ToString() != "")
                                        //                {
                                        //                    _ReserveCloseDateInfo.AppendLine();
                                        //                    _ReserveCloseDateInfo.Append(" Account #  : " + ClsAutoCoapyDistributionList.GetDispalyAccountNo(Convert.ToInt64(dr["nAccountID"])));
                                        //                }
                                        //            }
                                        //            else if (_msgReserveCloseDateCounter == 11)
                                        //            {
                                        //                _ReserveCloseDateInfo.AppendLine();
                                        //                _ReserveCloseDateInfo.Append("Too many claims to list");
                                        //            }
                                        //            nToSkipAccountID = Convert.ToInt64(dr["nAccountID"]);
                                        //            //dtCopayReserves = null;
                                        //            break;
                                        //        }
                                        //        continue;
                                        //    }
                                        //    else
                                        //    {
                                        //        nToSkipAccountID = 0;
                                        //    }
                                        //}
                                        #endregion
                                    }
                                }
                            }
                        }
                        if (paymentParameter != null && oSeletedReserveItems.Count > 0 && dtChargeLineDetails.Rows.Count > 0 && dtChargeLineDetails != null)
                        {
                            #region "Save in TVP"

                            bulkWriteOff = new gloAccountPayment.BulkPaymentOperation();
                            bulkWriteOff.AutoDistributeCopayReserves(paymentParameter, oSeletedReserveItems, dtChargeLineDetails, paymentParameter.PatientID);                           
                            #endregion
                        }
                        else if ((dtCopayReserves != null && dtCopayReserves.Rows.Count == 0) || (paymentParameter != null && oSeletedReserveItems.Count == 0 && dtChargeLineDetails.Rows.Count > 0 && dtChargeLineDetails != null))
                        {
                            //  MessageBox.Show("There is no remaining copay reserves available to distribute for Account # : " + ClsAutoCoapyDistributionList.GetDispalyAccountNo(_PAccountID), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            nToSkipAccountID = _PAccountID;
                            _msgReserveCounter++;
                            if (_msgReserveCounter <= 10)
                            {
                                if (_reserveInfo.ToString() == "")
                                {
                                    _reserveInfo.Append(" Account #  : " + ClsAutoCoapyDistributionList.GetDispalyAccountNo(_PAccountID));
                                }
                                else if (_reserveInfo.ToString() != "")
                                {
                                    _reserveInfo.AppendLine();
                                    _reserveInfo.Append(" Account #  : " + ClsAutoCoapyDistributionList.GetDispalyAccountNo(_PAccountID));
                                }
                            }
                            else if (_msgReserveCounter == 11)
                            {
                                _reserveInfo.AppendLine();
                                _reserveInfo.Append("Too many claims to list");
                            }

                            continue;
                        }
                        
                           
                        
                    }
                    //if (_ReserveCloseDateInfo.ToString() != "")
                    //{
                    //    MessageBox.Show("The used reserved amount close date is in future than the current payment close date for following account #. Please select a different payment close date. \n\n" + _ReserveCloseDateInfo.ToString() + "", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    if (_ClaimInfo.ToString() != "")
                    {
                       // if ( != 0)
                        {
                            MessageBox.Show("There is no sufficient remaining copay reserve available to distribute for following Claim# \n\n" + _ClaimInfo.ToString() + "", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    if (_reserveInfo.ToString() != "")
                    {
                        MessageBox.Show("Reserves allocated for the following account # is already used by another user.\nReallocate the amount  \n\n" + _reserveInfo.ToString() + "", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (rb_ShowAll.Checked)
                    {
                        LoadAutoCopayDistributionList();
                      //  OnAutoCopayLoad = false;
                    }
                    else
                    {
                        if (_ClaimInfo.ToString() == "" && _reserveInfo.ToString() == "")
                        {
                            ReservelistRow++;
                        }
                        LoadAutoCopayDistributionListOneByOne();
                       // OnAutoCopayLoad = false;
                    }
                    OnAutoCopayLoad = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private decimal CalculateAvailableReserve(object sender, RowColEventArgs e, int i)
        {
            //Only When Copay Reserve Amount is Distributed Manually.
            decimal dDistributedCopayAmt = 0;
            try
            {
                for (int j = i; j < C1AutoCopayList.Rows.Count; j++)
                {
                    if (Convert.ToInt64(C1AutoCopayList.GetData(j, COL_PACCOUNTID)) == Convert.ToInt64(C1AutoCopayList.GetData(e.Row, COL_PACCOUNTID)))
                    {
                        if (Convert.ToString(C1AutoCopayList.GetData(i, COL_RESERVEFORDOS)) == "" && Convert.ToString(C1AutoCopayList.GetData(j, COL_RES_DOS)) == "")
                        {
                            dDistributedCopayAmt = dDistributedCopayAmt + Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_DISTRIBUTE_COPAY));
                        }
                        else if (Convert.ToString(C1AutoCopayList.GetData(i, COL_RESERVEFORDOS)) == Convert.ToString(C1AutoCopayList.GetData(j, COL_RES_DOS)))
                        {
                            dDistributedCopayAmt = dDistributedCopayAmt + Convert.ToDecimal(C1AutoCopayList.GetData(j, COL_DISTRIBUTE_COPAY));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            return dDistributedCopayAmt;
        }

        //private void DisposeReserveDetails()
        //{
        //    try
        //    {
        //        if (ofrmPaymentUseCopayReserveV2 != null)
        //        {
        //            ofrmPaymentUseCopayReserveV2.Dispose();
        //            ofrmPaymentUseCopayReserveV2 = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
        //    }
        //}

        private void frmCopayDistributionList_Deactivate(object sender, EventArgs e)
        {
           // DisposeReserveDetails();
        }

        private void tsb_Next_Click(object sender, EventArgs e)
        {
            C1AutoCopayList.Select();
            ReservelistRow++;
            LoadAutoCopayDistributionListOneByOne();
            OnAutoCopayLoad = false;
        }

        private void rb_ShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_ShowAll.Checked)
            {
                tsb_Next.Visible = false;
                tsb_SaveAutoCopay.Text = "&Save";
                tsb_SaveAutoCopay.ToolTipText = "Save";
                dtStartDate.Enabled = true;
                dtEndDate.Enabled = true;
                rb_ShowAll.Font = new Font(rb_ShowAll.Font, FontStyle.Bold);
                LoadAutoCopayDistributionList();
                OnAutoCopayLoad = false;
            }
            else
            {
                rb_ShowAll.Font = new Font(rb_ShowAll.Font, FontStyle.Regular);
            }
        }

        private void rb_ShowOneByOne_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_ShowOneByOne.Checked)
            {
                ReservelistRow = 0;
                tsb_Next.Visible = true;
                tsb_SaveAutoCopay.Text = "Save && Next";
                tsb_SaveAutoCopay.ToolTipText = "Save & Next";
                dtStartDate.Enabled = false;
                dtEndDate.Enabled = false;
                rb_ShowOneByOne.Font = new Font(rb_ShowOneByOne.Font, FontStyle.Bold);
                LoadData();
                //LoadAutoCopayDistributionListOneByOne();
                OnAutoCopayLoad = false;
            }
            else
            {
                rb_ShowOneByOne.Font = new Font(rb_ShowOneByOne.Font, FontStyle.Regular);
            }
        }

        #endregion

        private void C1AutoCopayList_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar("-"))
            {
                e.Handled = true;
            }
        }
        #region "Reserve Details Panel"

        #region  " Grid Constants "

        const int COL_ResORIGINALPAYMENT = 0;//Check Number,Date,Amount      
        const int COL_ResPROVIDERNAME = 1;
        const int COL_ResTYPE = 2;//Copay,Advance,Other
        const int COL_ResNOTE = 3;//Note
        const int COL_ResTORESERVES = 4;//Amount for reserve
        const int COL_ResAVAILABLE = 5;//Available amount   
        const int COL_ResCOPAYRESERVEFORDOS = 6;
        const int COL_USED_RES = 7;
        const int COL_ResCOUNT = 8;

        #endregion

        #region " Form Controls events "



        private void c1Reserve_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        #endregion


        #region " Design Grid "

        private void DesignPaymentGrid(C1FlexGrid c1Payment)
        {
            try
            {

                c1Payment.Redraw = false;
                c1Payment.AllowSorting = AllowSortingEnum.None;

                c1Payment.Clear();
                c1Payment.Cols.Count = COL_ResCOUNT;
                c1Payment.Rows.Count = 1;
                c1Payment.Rows.Fixed = 1;
                c1Payment.Cols.Fixed = 0;

                #region " Set Headers "

                c1Payment.SetData(0, COL_ResORIGINALPAYMENT, "Original Payment");//Check Number,Date,Amount
                c1Payment.SetData(0, COL_ResTORESERVES, "Copay");//Amount for reserve
                c1Payment.SetData(0, COL_ResTYPE, "Type");//Copay,Advance,Other
                c1Payment.SetData(0, COL_ResNOTE, "Note");//Note
                c1Payment.SetData(0, COL_ResAVAILABLE, "Available");//Available amount                       
                c1Payment.SetData(0, COL_ResPROVIDERNAME, "Provider");
                c1Payment.SetData(0, COL_ResCOPAYRESERVEFORDOS, "Reserve for DOS");
                #endregion

                #region " Width "

                int Width = c1Payment.Width;

                c1Payment.Cols[COL_ResORIGINALPAYMENT].Width = Convert.ToInt32(Width * 0.26);
                c1Payment.Cols[COL_ResPROVIDERNAME].Width = Convert.ToInt32(Width * 0.12);
                c1Payment.Cols[COL_ResTYPE].Width = Convert.ToInt32(Width * 0.07);
                c1Payment.Cols[COL_ResNOTE].Width = Convert.ToInt32(Width * 0.20);
                c1Payment.Cols[COL_ResTORESERVES].Width = Convert.ToInt32(Width * 0.08);
                c1Payment.Cols[COL_ResAVAILABLE].Width = Convert.ToInt32(Width * 0.08);
                c1Payment.Cols[COL_ResCOPAYRESERVEFORDOS].Width = Convert.ToInt32(Width * 0.14);
                c1Payment.Cols[COL_USED_RES].Width = Convert.ToInt32(Width * 0.020);

                #endregion

                #region " Data Type "


                c1Payment.Cols[COL_ResORIGINALPAYMENT].DataType = typeof(System.String);
                c1Payment.Cols[COL_ResTORESERVES].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_ResTYPE].DataType = typeof(System.String);
                c1Payment.Cols[COL_ResNOTE].DataType = typeof(System.String);
                c1Payment.Cols[COL_ResAVAILABLE].DataType = typeof(System.Decimal);
                //c1Payment.Cols[COL_PROVIDERID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ResPROVIDERNAME].DataType = typeof(System.String);
                c1Payment.Cols[COL_ResCOPAYRESERVEFORDOS].Format = "MM/dd/yyyy";
                #endregion

                #region " Alignment "


                c1Payment.Cols[COL_ResORIGINALPAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ResTORESERVES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ResTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ResNOTE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ResAVAILABLE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                c1Payment.Cols[COL_ResPROVIDERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ResCOPAYRESERVEFORDOS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1Payment.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1Payment.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }


                c1Payment.Cols[COL_ResTORESERVES].Style = csCurrencyStyle;
                c1Payment.Cols[COL_ResAVAILABLE].Style = csCurrencyStyle;

                #endregion



                c1Payment.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1Payment.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Allow Editing "

                c1Payment.AllowEditing = false;

                c1Payment.AllowEditing = false;
                c1Payment.Cols[COL_ResORIGINALPAYMENT].AllowEditing = false;//100;
                c1Payment.Cols[COL_ResTORESERVES].AllowEditing = false;//100;
                c1Payment.Cols[COL_ResTYPE].AllowEditing = false;//100;
                c1Payment.Cols[COL_ResNOTE].AllowEditing = false;//100;
                c1Payment.Cols[COL_ResAVAILABLE].AllowEditing = false;//100;

                c1Payment.Cols[COL_ResPROVIDERNAME].AllowEditing = false;
                c1Payment.Cols[COL_ResCOPAYRESERVEFORDOS].AllowEditing = false;
                c1Payment.Cols[COL_USED_RES].AllowEditing = false;

                #endregion

                c1Payment.VisualStyle = VisualStyle.Office2007Blue;
                c1Payment.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                #region "Set Yellow / Green Style"

                c1Payment.Cols[COL_USED_RES].DataType = typeof(Image);
                c1Payment.Cols[COL_USED_RES].ImageMap = new System.Collections.Hashtable();
                c1Payment.Cols[COL_USED_RES].ImageAndText = false;
                c1Payment.Cols[COL_USED_RES].AllowResizing = false;
                c1Payment.Cols[COL_USED_RES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;


                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            { c1Payment.Redraw = true; }
        }

        #endregion " Design Grid "

        #region " Private & Public Methods "

        private void FillReserves(Int64 _patientId, Int64 nPAccountID, DateTime _dtReserveForDOS, DateTime _dtStartDate, DateTime _dtEndDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtReserves = new DataTable();

            try
            {
                DesignPaymentGrid(c1Reserve);

                oParameters.Add("@nPatientID", _patientId, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                if (_dtReserveForDOS == DateTime.MinValue)
                {
                    oParameters.Add("@dtreservefordos", DBNull.Value, ParameterDirection.Input, SqlDbType.Date);
                }
                else
                {
                    oParameters.Add("@dtreservefordos", _dtReserveForDOS, ParameterDirection.Input, SqlDbType.Date);
                }
                oParameters.Add("@dtStartDate", _dtStartDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@dtEndDate", _dtEndDate, ParameterDirection.Input, SqlDbType.Date);
                oDB.Connect(false);
                oDB.Retrive("BL_PatientAvailableCopayReserve", oParameters, out _dtReserves);
                oDB.Disconnect();

                if (_dtReserves != null && _dtReserves.Rows.Count > 0)
                {
                    int _rowIndex = 0;

                    for (int i = 0; i < _dtReserves.Rows.Count; i++)
                    {
                        #region " Set Data "

                        _rowIndex = c1Reserve.Rows.Add().Index;


                        string _originalPayment = "";
                        _originalPayment = ((gloAccountsV2.PaymentModeV2)Convert.ToInt32(_dtReserves.Rows[i]["nPayMode"])).ToString() + "# " + Convert.ToString(_dtReserves.Rows[i]["CheckNumber"]) + " " + Convert.ToString(_dtReserves.Rows[i]["nCheckDate"]) + " $ " + Convert.ToDecimal(_dtReserves.Rows[i]["nCheckAmount"]);
                        c1Reserve.SetData(_rowIndex, COL_ResORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount

                        c1Reserve.SetData(_rowIndex, COL_ResTORESERVES, Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"]));
                        c1Reserve.SetData(_rowIndex, COL_ResTYPE, ((gloAccountsV2.NoteSubTypeV2)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
                        c1Reserve.SetData(_rowIndex, COL_ResNOTE, Convert.ToString(_dtReserves.Rows[i]["sNoteDescription"]));//Note

                        c1Reserve.SetData(_rowIndex, COL_ResAVAILABLE, Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]));//Available amount                      


                        c1Reserve.SetData(_rowIndex, COL_ResPROVIDERNAME, Convert.ToString(_dtReserves.Rows[i]["AssociationProvider"]));
                        c1Reserve.SetData(_rowIndex, COL_ResCOPAYRESERVEFORDOS, Convert.ToString(_dtReserves.Rows[i]["dtReserveForDOS"]));

                        c1Reserve.SetCellImage(_rowIndex, COL_USED_RES, global::gloBilling.Properties.Resources.HoldClaim);

                        #endregion
                    }
                }
            }

            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtReserves != null) { _dtReserves.Dispose(); }

            }
        }

        #endregion " Private & Public Methods "

        private void btnRDClose_Click(object sender, EventArgs e)
        {
            c1Reserve.DataSource = null;
            pnlReserveDetails.SendToBack();
        }

        private void tlb_RDClose_Click(object sender, EventArgs e)
        {
            c1Reserve.DataSource = null;
            pnlReserveDetails.SendToBack();
        }

        #endregion

        private void btnRDClose_Click_1(object sender, EventArgs e)
        {
            c1Reserve.DataSource = null;
            pnlReserveDetails.SendToBack();
        }

        private void tlb_RDClose_Click_1(object sender, EventArgs e)
        {
            c1Reserve.DataSource = null;
            pnlReserveDetails.SendToBack();
        }
        bool allowResize = false;
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
           allowResize = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (allowResize)
            {

                this.pnlReserveDetails.Height = pictureBox1.Top + e.Y;
                this.pnlReserveDetails.Width = pictureBox1.Left + e.X;
            }

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            allowResize = true;
        }

        private void frmCopayDistributionList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ofrmCopayDistribution != null)
            {
                ofrmCopayDistribution.Dispose();
                ofrmCopayDistribution = null;
                ClsAutoCoapyDistributionList.RemoveInstanceForUser();
            }
        }
    }
}
