using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloBilling;
using System.Collections;
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Globalization;
using System.Resources;


namespace gloBilling.Collections
{
    public partial class frmRevenueCycle : Form
    {


        #region " Declarations "

        gloListControl.gloListControl oListControl = null;
        ComboBox combo;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        private static frmRevenueCycle ofrmRevenueCycle;
        private Int64 _PatientID = 0;
        private Int64 nPAccountID = 0;
        private Int64 nGuarantorID = 0;
        private Int64 nAccountPatientID = 0;
        private bool _blnDisposed;

        //string filterBadDebtQueueByAction = string.Empty;
        //bool filterBadDebtQueueIncludesFutureFollowup = false;
        //Int64 filterBadDebtQueueByBusinessCenterId = 0;

        private int iAccountSelRow = 1;
       // private int iBadDebtAccountSelRow = 1;
        private int iClaimSelRow = 1;
        private int _AccountSelRow = 0;
       // private int _BadDebtAccountSelRow = 0;
        string sActonCode = "";
        private Int64 nBusinessId = 0;
        private Int64 nInsBusinessId = 0;
        Boolean bIsFutureItemAllow = false;
        Boolean bBeforegloCollect = false;
        Boolean bgloCollect = false;
        private BackgroundWorker worker = new BackgroundWorker();
        private BackgroundWorker workerClaimIns = new BackgroundWorker();
       // private BackgroundWorker workerBadDebtAccounts = new BackgroundWorker();
        private string sInsCmpny = "";
        decimal _dBalance = 0;
        int nInsSortedColumn = -1;
        SortFlags oInsSortFlags;
        private int iClaimNextSelRow = 1;

        int nActSortedColumn = -1;
        SortFlags oActSortFlags;
        private int iAccountNextSelRow = 1;

        //int nBadDebtActSortedColumn = -1;
        //SortFlags oBadDebtActSortFlags;
        //private int iBadDebtAccountNextSelRow = 1;

        public bool bIsShowClaimStatus = false;
        public Tuple<Int64, string, string> SyncPatientId
        {
            get
            {
                if (tbPatientFinancial.SelectedTab.Text == "Patient Account Follow-up Queue")
                {
                    return new Tuple<Int64, string, string>(_PatientID, oPatientControl.PatientCode, oPatientControl.PatientName);
                }
                else if (tbPatientFinancial.SelectedTab.Text == "Insurance Claim Follow-up Queue")
                {
                    return new Tuple<Int64, string, string>(Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index)), lblPatientCode.Text, lblPatientName.Text);
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region " Constructor "

        public frmRevenueCycle(Int64 PatientID)
        {
            InitializeComponent();
            _PatientID = PatientID;
            cmbAcctFollowUpAction.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAcctFollowUpAction.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            
            cmbClaimFollowupAction.DrawMode = DrawMode.OwnerDrawFixed;
            cmbClaimFollowupAction.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            
            cmbInsuranceCompany.DrawMode = DrawMode.OwnerDrawFixed;
            cmbInsuranceCompany.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            //cmbBadDebtScheduleAction.DrawMode = DrawMode.OwnerDrawFixed;
            //cmbBadDebtScheduleAction.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            if (gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_FollowupQueue"))
            {
                cmbBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
                cmbBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
                cmbInsBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
                cmbInsBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
                //cmbBadDebtBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
                //cmbBadDebtBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

                FillBusinessCenter(cmbBusinessCenter);
                FillBusinessCenter(cmbInsBusinessCenter);
               // FillBusinessCenter(cmbBadDebtBusinessCenter);

                cmbBusinessCenter.SelectedValue = gloGlobal.gloPMGlobal.GetDefaultBusinessCenterForUser(gloGlobal.gloPMGlobal.UserID);
                cmbInsBusinessCenter.SelectedValue = gloGlobal.gloPMGlobal.GetDefaultBusinessCenterForUser(gloGlobal.gloPMGlobal.UserID);
               // cmbBadDebtBusinessCenter.SelectedValue = gloGlobal.gloPMGlobal.GetDefaultBusinessCenterForUser(gloGlobal.gloPMGlobal.UserID);
            }
            else
            {
                pnlBusinessCenter.Visible = false;
                pnlInsBusinessCenter.Visible = false;
               // pnlBadDebtBusinessCenter.Visible = false;
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (!(this._blnDisposed))
            {

                if ((disposing))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    if ((components != null))
                    {
                        components.Dispose();
                    }
                }
            }
            ofrmRevenueCycle = null;
            this._blnDisposed = true;
            base.Dispose(disposing);
        }

        public void Disposer()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        ~frmRevenueCycle()
        {
            Dispose(false);
        }

        public static frmRevenueCycle GetInstance(Int64 PatientID)
        {
            try
            {
                if (ofrmRevenueCycle == null)
                {
                    ofrmRevenueCycle = new frmRevenueCycle(PatientID);
                }
            }
            finally
            {

            }
            return ofrmRevenueCycle;
        }

        #endregion

        #region " Private & Public Methods"

        private void LoadPatientStrip(Int64 PatientId, Int64 AccountID, bool SearchEnable)
        {
            oPatientControl.FillDetails(PatientId, AccountID, gloStripControl.FormName.ModifyCharges);
            oPatientControl.ShowAccountPatientSearch = true;
            _PatientID = oPatientControl.PatientID;
            this.nPAccountID = oPatientControl.PAccountID;
            this.nGuarantorID = oPatientControl.GuarantorID;
            this.nAccountPatientID = oPatientControl.AccountPatientID;
        }

        //private void LoadBadDebtPatientStrip(Int64 PatientId, Int64 AccountID, bool SearchEnable)
        //{
        //    oPatientControlBadDebt.FillDetails(PatientId, AccountID, gloStripControl.FormName.ModifyCharges);
        //    oPatientControlBadDebt.ShowAccountPatientSearch = true;
        //    _PatientID = oPatientControlBadDebt.PatientID;
        //    this.nPAccountID = oPatientControlBadDebt.PAccountID;
        //    this.nGuarantorID = oPatientControlBadDebt.GuarantorID;
        //    this.nAccountPatientID = oPatientControlBadDebt.AccountPatientID;
        //}

        private void LoadInsuranceClaimQueue()
        {
            try
            {
                pnlProgressClaimIndication.BringToFront();
                sInsCmpny = "";
                sActonCode = "";
                nInsBusinessId = 0;
                bIsFutureItemAllow = false;
                if (cmbInsuranceCompany.Items.Count > 0)
                {
                    for (int cntrIns = 0; cntrIns <= cmbInsuranceCompany.Items.Count - 1; cntrIns++)
                    {
                        if (Convert.ToString(cmbInsuranceCompany.Text).Trim() != string.Empty)
                        {
                            if (sInsCmpny == string.Empty)
                                sInsCmpny = (Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cntrIns])["ID"]));
                            else
                                sInsCmpny = sInsCmpny + "," + (Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cntrIns])["ID"]));
                        }
                    }
                }
                else
                {
                    sInsCmpny = "";
                }
                if (cmbClaimFollowupAction.Items.Count > 0)
                {
                    sActonCode = Convert.ToString(cmbClaimFollowupAction.SelectedValue).Trim();
                }
                else
                {
                    sActonCode = "";
                }

                if (cmbInsBusinessCenter.Items.Count > 0 && pnlInsBusinessCenter.Visible == true)
                {
                    nInsBusinessId = Convert.ToInt64(cmbInsBusinessCenter.SelectedValue);
                }
                else
                {
                    nInsBusinessId = 0;
                }

                if (chkInsClmIncludeFutureDtl.Checked)
                {
                    bIsFutureItemAllow = true;
                }
                else
                {
                    bIsFutureItemAllow = false;
                }

                bBeforegloCollect = chkBeforegloCollect.Checked;

                bgloCollect = chkAftergloCollect.Checked;

                if (!workerClaimIns.IsBusy)
                {
                    workerClaimIns.RunWorkerAsync();
                }

                //FillInsuranceClaimTab();

                //if (c1InsClaimSchedule.Rows.Count > 1)
                //{
                //    if (c1InsClaimSchedule.RowSel > 0)
                //    {
                //        if (iClaimSelRow != c1InsClaimSchedule.RowSel || iClaimSelRow == 1)
                //        {
                //            if (
                //                    (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)) != "") &&
                //                    (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index)) != "") &&
                //                    (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index)) != "") &&
                //                    (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index)) != "")
                //                )
                //            {
                //                FillInsuranceClaimTabBanner(Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index)));
                //                pnlInsuranceClaimBanner.Visible = true;
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    pnlInsuranceClaimBanner.Visible = false;
                //    ClearClaimBanner();
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                SetToolStripButtonForInsuranceQueue();
            }
        }

        private void ClearClaimBanner()
        {
            lblPatientName.Text = "";
            lblDOB.Text = "";
            lblGender.Text = "";
            lblPatientCode.Text = "";
            lblPatAlert.Text = "";
            lblPatNote.Text = "";
            lblInsPlan.Text = "";
            lblSubscriberName.Text = "";
            lblClaimNo.Text = "";
            lblDOSdt.Text = "";
            lblClaimNote.Text = "";
            lblProc.Text = "";
            lblDx.Text = "";
            lblCharges.Text = "";
            lblBalance.Text = "";
            lblFollowUpDt.Text = "";
            lblActNo.Text = "";
            lblActNote.Text = "";
            lblLastFiling.Text = "";
            lblLastRemit.Text = "";
            lblInsuranceID.Text = "";
            lblGroupNo.Text = "";
            lblTFLDFLDate.Text = ""; // TFL and DFL Changes
            lblDemoEMRAlerts.Text = "";
            lblDemoNextAppt.Text = "";

        }

        private void LoadPatientAccountQueue()
        {
            try
            {

                pnlProgressIndication.BringToFront();

                if (cmbAcctFollowUpAction.Items.Count > 0)
                {
                    sActonCode = Convert.ToString(cmbAcctFollowUpAction.SelectedValue).Trim();
                    if (cmbAcctFollowUpAction.SelectedIndex > 0)
                    {
                        if (!chkPatAccIncludeFutureDetail.Checked)
                        {
                            if (Convert.ToString(cmbAcctFollowUpAction.SelectedValue).Trim() != string.Empty)
                            {
                                DataTable _dtDefTemplate = new DataTable();
                                _dtDefTemplate = CL_FollowUpCode.GetDefaultAssociateTemplate(Convert.ToString(cmbAcctFollowUpAction.SelectedValue), CollectionEnums.FollowUpType.PatientAccount);
                                if (_dtDefTemplate != null && _dtDefTemplate.Rows.Count > 0)
                                {
                                    chkBatchTemplate.Visible = true;
                                }
                                else
                                {
                                    chkBatchTemplate.Checked = false;
                                    chkBatchTemplate.Visible = false;
                                }
                                chkBatchTemplate.Checked = false;
                                chkBatchTemplate.Visible = false;
                            }
                            else
                            {
                                chkBatchTemplate.Checked = false;
                                chkBatchTemplate.Visible = false;
                            }
                        }
                        else
                        {
                            chkBatchTemplate.Checked = false;
                            chkBatchTemplate.Visible = false;
                        }
                    }
                    else
                    {
                        chkBatchTemplate.Checked = false;
                        chkBatchTemplate.Visible = false;
                    }
                }
                else
                {
                    sActonCode = "";
                }
                if (cmbBusinessCenter.Items.Count > 0 && pnlBusinessCenter.Visible == true)
                {
                    nBusinessId = Convert.ToInt64(cmbBusinessCenter.SelectedValue);
                }
                else
                {
                    nBusinessId = 0;
                }
                if (chkPatAccIncludeFutureDetail.Checked)
                {
                    bIsFutureItemAllow = true;
                }
                else
                {
                    bIsFutureItemAllow = false;
                }
                if (!worker.IsBusy)
                {
                    worker.RunWorkerAsync();
                }

                //FillPatientQueueTab();

                //if (c1PALogView.Rows.Count > 1)
                //{
                //    if (iAccountSelRow != c1PALogView.RowSel || iAccountSelRow == 1)
                //    {
                //        if (c1PALogView.RowSel > 0)
                //        {
                //            if (
                //                    c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index) != null && Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)) != 0
                //                   && c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index) != null && Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)) != 0
                //               )
                //            {
                //                LoadPatientStrip(Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)), Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)), false);
                //                oPatientControl.Visible = true;
                //            }
                //        }
                //    }

                //}
                //else
                //{
                //    oPatientControl.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        //private void LoadBadDebtAccountQueue()
        //{
        //    try
        //    {
        //        filterBadDebtQueueByAction = "";
        //        filterBadDebtQueueIncludesFutureFollowup = false;
        //        filterBadDebtQueueByBusinessCenterId = 0;


        //        pnlBadDebtProgressIndication.BringToFront();

        //        if (cmbBadDebtScheduleAction.Items.Count > 0)
        //        {
        //            filterBadDebtQueueByAction = Convert.ToString(cmbBadDebtScheduleAction.SelectedValue).Trim();
        //        }
        //        else
        //        {
        //            filterBadDebtQueueByAction = "";
        //        }
        //        if (cmbBadDebtBusinessCenter.Items.Count > 0 && pnlBadDebtBusinessCenter.Visible == true)
        //        {
        //            filterBadDebtQueueByBusinessCenterId = Convert.ToInt64(cmbBadDebtBusinessCenter.SelectedValue);
        //        }
        //        else
        //        {
        //            filterBadDebtQueueByBusinessCenterId = 0;
        //        }
        //        if (chkBadDebtAccIncludeFutureDetail.Checked)
        //        {
        //            filterBadDebtQueueIncludesFutureFollowup = true;
        //        }
        //        else
        //        {
        //            filterBadDebtQueueIncludesFutureFollowup = false;
        //        }
        //        if (!workerBadDebtAccounts.IsBusy)
        //        {
        //            workerBadDebtAccounts.RunWorkerAsync();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }
        //    finally
        //    {
        //        ShowHideBadDebtToolstripButtons();
        //    }
        //}

        private void FillRevenueSummary()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {

                DataSet _dtRevenueSummary = CLsCL_RevenueCycle.getDashBoardDtl();

                if (_dtRevenueSummary != null)
                {
                    if (_dtRevenueSummary.Tables[0] != null)
                    {
                        if (_dtRevenueSummary.Tables[0].Rows.Count > 0)
                        {
                            int cnt = 0;
                            for (cnt = 0; cnt <= _dtRevenueSummary.Tables[0].Rows.Count - 1; cnt++)
                            {
                                if (Convert.ToString(_dtRevenueSummary.Tables[0].Rows[cnt][0]).Trim().ToLower() == "Number of Accounts in Follow-up".ToLower())
                                {
                                    lblPatAccNoOfAccInFollowUp.Text = Convert.ToString(_dtRevenueSummary.Tables[0].Rows[cnt][1]);
                                }
                                else if (Convert.ToString(_dtRevenueSummary.Tables[0].Rows[cnt][0]).Trim().ToLower() == "Actions Completed Today".ToLower())
                                {
                                    lblPatAccActionCompToday.Text = Convert.ToString(_dtRevenueSummary.Tables[0].Rows[cnt][1]);
                                }
                                else if (Convert.ToString(_dtRevenueSummary.Tables[0].Rows[cnt][0]).Trim().ToLower() == "Number of Claims in Follow-up".ToLower())
                                {
                                    lblInsClmNoOfAccInFollowUp.Text = Convert.ToString(_dtRevenueSummary.Tables[0].Rows[cnt][1]);
                                }
                                else if (Convert.ToString(_dtRevenueSummary.Tables[0].Rows[cnt][0]).Trim().ToLower() == "Actions Completed Today Claim".ToLower())
                                {
                                    lblInsClmActionCompToday.Text = Convert.ToString(_dtRevenueSummary.Tables[0].Rows[cnt][1]);
                                }
                                //else if (Convert.ToString(_dtRevenueSummary.Tables[0].Rows[cnt][0]).Trim().ToLower() == "Number of BadDebt Accounts in Follow-up".ToLower())
                                //{
                                //    lblBadDebtAccNoOfAccInFollowUp.Text = Convert.ToString(_dtRevenueSummary.Tables[0].Rows[cnt][1]);
                                //}
                                //else if (Convert.ToString(_dtRevenueSummary.Tables[0].Rows[cnt][0]).Trim().ToLower() == "Actions Completed Today BadDebt".ToLower())
                                //{
                                //    lblBadDebtAccActionCompToday.Text = Convert.ToString(_dtRevenueSummary.Tables[0].Rows[cnt][1]);
                                //}
                            }
                        }
                    }

                    if (_dtRevenueSummary.Tables[1] != null)
                    {
                        c1ActOverDue.DataSource = _dtRevenueSummary.Tables[1];
                        c1ActOverDue.FocusRect = FocusRectEnum.None;
                    }

                    if (_dtRevenueSummary.Tables[2] != null)
                    {
                        c1ClaimOverDue.DataSource = _dtRevenueSummary.Tables[2];
                        c1ClaimOverDue.FocusRect = FocusRectEnum.None;
                    }

                    //if (_dtRevenueSummary.Tables[3] != null)
                    //{
                    //    c1BadDebtActOverDue.DataSource = _dtRevenueSummary.Tables[3];
                    //    c1BadDebtActOverDue.FocusRect = FocusRectEnum.None;
                    //}
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

        }
        private void FillBusinessCenter(ComboBox cmbBussCenter)
        {
            DataTable _dtBusinessCenter = new DataTable();
            _dtBusinessCenter = gloGlobal.gloPMMasters.GetBusinessCenter();

            if (_dtBusinessCenter != null && _dtBusinessCenter.Rows.Count > 0)
            {
                DataRow dr = _dtBusinessCenter.NewRow();
                dr["BusinessCenter"] = "";
                dr["nBusinessCenterId"] = 0;

                _dtBusinessCenter.Rows.InsertAt(dr, 0);

                cmbBussCenter.DataSource = _dtBusinessCenter;
                cmbBussCenter.DisplayMember = "BusinessCenter";
                cmbBussCenter.ValueMember = "nBusinessCenterId";


            }
            // throw new NotImplementedException();
        }
        private void FillFollowUpActionsAcc()
        {
            try
            {
                CL_FollowUpCode clFollow = new CL_FollowUpCode();
                DataTable dtFollowUpActions = clFollow.fillFollowUpAction(CollectionEnums.FollowUpType.PatientAccount);
                if (dtFollowUpActions != null && dtFollowUpActions.Rows.Count > 0)
                {
                    DataRow dr = dtFollowUpActions.NewRow();
                    dr["sFollowUpActionCode"] = "";
                    dr["sFollowUpAction"] = "";
                    dtFollowUpActions.Rows.InsertAt(dr, 0);

                    cmbAcctFollowUpAction.BeginUpdate();
                    cmbAcctFollowUpAction.DataSource = dtFollowUpActions;
                    cmbAcctFollowUpAction.DisplayMember = dtFollowUpActions.Columns["sFollowUpAction"].ColumnName;
                    cmbAcctFollowUpAction.ValueMember = dtFollowUpActions.Columns["sFollowUpActionCode"].ColumnName;
                    cmbAcctFollowUpAction.EndUpdate();
                    //cmbAcctFollowUpAction.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        //private void FillFollowUpActionsBadDebtAccount()
        //{
        //    try
        //    {
        //        CL_FollowUpCode clFollow = new CL_FollowUpCode();
        //        DataTable dtFollowUpActions = clFollow.fillFollowUpAction(CollectionEnums.FollowUpType.BadDebt);
        //        if (dtFollowUpActions != null && dtFollowUpActions.Rows.Count > 0)
        //        {
        //            DataRow dr = dtFollowUpActions.NewRow();
        //            dr["sFollowUpActionCode"] = "";
        //            dr["sFollowUpAction"] = "";
        //            dtFollowUpActions.Rows.InsertAt(dr, 0);

        //            cmbBadDebtScheduleAction.BeginUpdate();
        //            cmbBadDebtScheduleAction.DataSource = dtFollowUpActions;
        //            cmbBadDebtScheduleAction.DisplayMember = dtFollowUpActions.Columns["sFollowUpAction"].ColumnName;
        //            cmbBadDebtScheduleAction.ValueMember = dtFollowUpActions.Columns["sFollowUpActionCode"].ColumnName;
        //            cmbBadDebtScheduleAction.EndUpdate();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }
        //}

        private void FillFollowUpActionsInsClaim()
        {
            try
            {
                CL_FollowUpCode clFollow = new CL_FollowUpCode();
                DataTable dtFollowUpActions = clFollow.fillFollowUpAction(CollectionEnums.FollowUpType.Claim);
                if (dtFollowUpActions != null && dtFollowUpActions.Rows.Count > 0)
                {
                    DataRow dr = dtFollowUpActions.NewRow();
                    dr["sFollowUpActionCode"] = "";
                    dr["sFollowUpAction"] = "";
                    dtFollowUpActions.Rows.InsertAt(dr, 0);

                    cmbClaimFollowupAction.BeginUpdate();
                    cmbClaimFollowupAction.DataSource = dtFollowUpActions;
                    cmbClaimFollowupAction.DisplayMember = dtFollowUpActions.Columns["sFollowUpAction"].ColumnName;
                    cmbClaimFollowupAction.ValueMember = dtFollowUpActions.Columns["sFollowUpActionCode"].ColumnName;
                    cmbClaimFollowupAction.EndUpdate();
                    //cmbClaimFollowupAction.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void FillPatientQueueTab()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            this.c1PALogView.EnterCell -= new System.EventHandler(this.c1PALogView_EnterCell);
            try
            {
                string sActonCode = "";
                if (cmbAcctFollowUpAction.Items.Count > 0)
                {
                    sActonCode = Convert.ToString(cmbAcctFollowUpAction.SelectedValue).Trim();
                }
                nBusinessId = 0;
                if (cmbBusinessCenter.Items.Count > 0 && pnlBusinessCenter.Visible == true)
                {
                    nBusinessId = Convert.ToInt64(cmbBusinessCenter.SelectedValue);
                }
                else
                {
                    nBusinessId = 0;
                }
                Boolean bIsFutureItemAllow = false;
                if (chkPatAccIncludeFutureDetail.Checked)
                {
                    bIsFutureItemAllow = true;
                }
                DataTable _dtAccountLogQueue = CLsCL_RevenueCycle.getPatAccQueueDetails(sActonCode, bIsFutureItemAllow, nBusinessId);
                if (_dtAccountLogQueue != null)
                {
                    c1PALogView.DataSource = _dtAccountLogQueue;
                    if (_dtAccountLogQueue.Rows.Count > 0)
                    {
                        if (_dtAccountLogQueue.Rows.Count >= iAccountSelRow)
                            c1PALogView.Row = iAccountSelRow;
                        else if (c1PALogView.Rows.Count > 1)
                        {
                            c1PALogView.Row = c1PALogView.Rows.Count - 1;
                            iAccountSelRow = c1PALogView.Rows.Count - 1;
                        }

                        LoadPatientStrip(Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)), Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)), false);
                        //oPatientControl.Visible = true;  
                    }
                    else
                    {
                        //oPatientControl.Visible = false;
                    }

                }



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                this.c1PALogView.EnterCell += new System.EventHandler(this.c1PALogView_EnterCell);
                //if (c1PALogView.Rows.Count > 1)
                //{ c1PALogView.Select(1, 0); }
            }

        }

        public Int16 GetBillingType(Int64 TransactionId, Int64 MstTransactionId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                object BillingType;
                oParameters.Add("@nTransactionId", TransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionMstId", MstTransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                BillingType = oDB.ExecuteScalar("BL_Get_BillingType", oParameters);
                oDB.Disconnect();
                return Convert.ToInt16(BillingType);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

        }

        private void showClaimHistory()
        {
            Int64 nPatientID = 0;
            Int64 ParamTransactionId = 0;
            try
            {
                if (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)) != "")
                {
                    ParamTransactionId = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index));
                    gloBilling ogloBilling = new gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                    Int64 mainTransactionID = 0;
                    if (ParamTransactionId != 0)
                        mainTransactionID = ogloBilling.GetLastTransactionID(ParamTransactionId);

                    nPatientID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index));
                    frmClaimChargeHistoryV2 ofrmClaimChargeHistory = new frmClaimChargeHistoryV2(gloGlobal.gloPMGlobal.DatabaseConnectionString, nPatientID, gloGlobal.gloPMGlobal.ClinicID, mainTransactionID);
                    ofrmClaimChargeHistory.StartPosition = FormStartPosition.CenterScreen;
                    ofrmClaimChargeHistory.ShowDialog(this);
                    ofrmClaimChargeHistory.Dispose();
                    ogloBilling.Dispose();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
        }

        private void FillInsuranceClaimTab()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                sInsCmpny = "";
                sActonCode = "";
                bIsFutureItemAllow = false;
                if (cmbInsuranceCompany.Items.Count > 0)
                {
                    for (int cntrIns = 0; cntrIns <= cmbInsuranceCompany.Items.Count - 1; cntrIns++)
                    {
                        if (cmbInsuranceCompany.SelectedIndex >= 0)
                        {
                            if (sInsCmpny == string.Empty)
                                sInsCmpny = (Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cntrIns])["ID"]));
                            else
                                sInsCmpny = sInsCmpny + "," + (Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cntrIns])["ID"]));
                        }
                    }
                }

                if (cmbClaimFollowupAction.Items.Count > 0)
                {
                    sActonCode = Convert.ToString(cmbClaimFollowupAction.SelectedValue).Trim();
                }
                nInsBusinessId = 0;
                if (cmbInsBusinessCenter.Items.Count > 0 && pnlInsBusinessCenter.Visible == true)
                {
                    nInsBusinessId = Convert.ToInt64(cmbInsBusinessCenter.SelectedValue);
                }
                else
                {
                    nInsBusinessId = 0;
                }
                if (chkInsClmIncludeFutureDtl.Checked)
                {
                    bIsFutureItemAllow = true;
                }

                if (chkBeforegloCollect.Checked)
                {
                    bBeforegloCollect = true;
                }
                if (chkAftergloCollect.Checked)
                {
                    bgloCollect = true;
                }
                DataTable _dtInsuranceLogQueue = CLsCL_RevenueCycle.getInsClaimQueueDetails(sActonCode, sInsCmpny, bIsFutureItemAllow, nInsBusinessId, bBeforegloCollect, bgloCollect,ChkIncludeBalances.Checked);
                if (_dtInsuranceLogQueue != null)
                {
                    gloGlobal.gloPMGlobal.SplitClaimColumn(_dtInsuranceLogQueue, _dtInsuranceLogQueue.Columns.IndexOf("sClaimNo"));
                    c1InsClaimSchedule.DataSource = _dtInsuranceLogQueue;
                    if (_dtInsuranceLogQueue.Rows.Count >= iClaimSelRow)
                        c1InsClaimSchedule.Row = iClaimSelRow;
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

        }

        Dictionary<string, string> sortedDict = null;
        public Dictionary<string, string> DishtblMedcatClr;
        private global::System.Globalization.CultureInfo resourceCulture;
        [System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal global::System.Globalization.CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        private void FillMedicalCategoryHashTable()
        {
            if ((DishtblMedcatClr == null))
            {
                DishtblMedcatClr = new Dictionary<string, string>();
                DishtblMedcatClr.Add("MedicalCategoryImages_1_Brown_TopBrown", "MedicalCategoryImages_1_Brown_BottomBrown");
                DishtblMedcatClr.Add("MedicalCategoryImages_2_Blue_TopSkyBlue", "MedicalCategoryImages_2_Blue_BottomSkyBlue");
                DishtblMedcatClr.Add("MedicalCategoryImages_3_Gray_TopGray", "MedicalCategoryImages_3_Gray_BottomGray");
                DishtblMedcatClr.Add("MedicalCategoryImages_4_GreenTopLightGreen", "MedicalCategoryImages_4_Green_BottomLightGreen");
                DishtblMedcatClr.Add("MedicalCategoryImages_5_TopOrange", "MedicalCategoryImages_5_BottomOrange");

                DishtblMedcatClr.Add("MedicalCategoryImages_6_Pink_TopPink", "MedicalCategoryImages_6_Pink_BottomPink");
                DishtblMedcatClr.Add("MedicalCategoryImages_7_Red_TopRed", "MedicalCategoryImages_7_Red_BottomRed");
                DishtblMedcatClr.Add("MedicalCategoryImages_8_Violet_TopViolet", "MedicalCategoryImages_8_Violet_BottomViolet");
                DishtblMedcatClr.Add("MedicalCategoryImages_9_Yellow_TopYellow", "MedicalCategoryImages_9_Yellow_BottomYellow");
                DishtblMedcatClr.Add("MedicalCategoryImages_91_TopDark_Blue", "MedicalCategoryImages_91_BottomDark_Blue");
                //   DishtblMedcatClr.  
                sortedDict = (from entry in DishtblMedcatClr orderby entry.Key ascending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
            }
        }
        private static ResourceSet resourceSet = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

        private void GetMedicalCategoryImage(long nPatientId=0,DataTable dtMedCat = null)
        {

            DataSet DS = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oPara = null;
            DataTable dtMedColor = null;
            string strcolor = "";
            string strborderColor = string.Empty;
            string strbottompanelcolr = string.Empty;
            try
            {
                var _with1 = oDB;
                oDB.Connect(false);
                oPara = new gloDatabaseLayer.DBParameters();
                oPara.Add("@tvpMedicalCategory", dtMedCat, ParameterDirection.Input, SqlDbType.Structured);
                oPara.Add("@PatientId", nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_GetPatientMedicalCategoryColor", oPara, out dtMedColor);
                oDB.Disconnect();
                if ((dtMedColor != null))
                {
                    if ((dtMedColor.Rows.Count > 0))
                    {
                        strcolor = Convert.ToString(dtMedColor.Rows[0]["ImageColor"]);
                        strborderColor = Convert.ToString(dtMedColor.Rows[0]["BorderColor"]);
                        strbottompanelcolr = Convert.ToString(dtMedColor.Rows[0]["BottomPanelColor"]);
                    }
                }


                if ((!string.IsNullOrEmpty(strcolor.Trim())))
                {
                    foreach (KeyValuePair<string, string> di in sortedDict)
                    {
                        if ((di.Key.ToString().Contains(strcolor.Trim().Replace(" ", "_"))))
                        {
                            pnlHeader.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Key));
                            pnlInsClaimBannerMain.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Value));

                            lblInsuranceClaimRight.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblInsuranceClaimLeft.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblInsuranceClaimTop.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblInsuranceClaimBottom.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblInsuranceclaimHdrBottom.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }
                else
                {
                    //ResourceSet resourceSet = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
                    pnlHeader.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_TopOrange"));
                    pnlInsClaimBannerMain.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_BottomOrange"));
                    lblInsuranceClaimRight.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblInsuranceClaimLeft.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblInsuranceClaimTop.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblInsuranceClaimBottom.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblInsuranceclaimHdrBottom.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                }

                if ((strcolor.Contains("Pink") || strcolor.Contains("Red") || strcolor.Contains("Violet") || strcolor.Contains("Dark")))
                {
                    lblPatientCode.ForeColor = Color.White;
                    lblPatientCodeCaption.ForeColor = Color.White;
                    lblGender.ForeColor = Color.White;
                    lblGenderCaption.ForeColor = Color.White;
                    lblDOB.ForeColor = Color.White;
                    lblDOBCaption.ForeColor = Color.White;
                    lblPatientName.ForeColor = Color.White;
                }
                else
                {
                    lblPatientCode.ForeColor = Color.Black;
                    lblPatientCodeCaption.ForeColor = Color.Black;
                    lblGender.ForeColor = Color.Black;
                    lblGenderCaption.ForeColor = Color.Black;
                    lblDOB.ForeColor = Color.Black;
                    lblDOBCaption.ForeColor = Color.Black;
                    lblPatientName.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if ((oDB != null))
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if ((dtMedColor != null))
                {
                    dtMedColor.Dispose();
                    dtMedColor = null;
                }
                if ((dtMedCat != null))
                {
                    dtMedCat.Dispose();
                    dtMedCat = null;
                }
                if ((DS != null))
                {
                    DS.Tables.Clear();
                    DS.Dispose();
                    DS = null;
                }
            }


        }

        private void FillInsuranceClaimTabBanner(Int64 TransactionID, Int64 PatientID, Int64 nNextActionPatientInsID, Int64 nNextActionContactID, Int64 nAccountID)
        {

            //Patient Fields
            string _PatientCode = "";
            string _PatientName = "";
            string _Gender = "";
            decimal _dcharges = 0;
            Decimal dTotalBalAmt = 0;
            DateTime _DateOfBirth;
            lblClaimNo.Text = "";
            DataSet _dsInsuranceLogQueueBanner = null;
            lblTflDfl.Visible = false;
            string _NextAppointment = "";
            string _EMRAlerts = "";

            try
            {
                _dsInsuranceLogQueueBanner = CLsCL_RevenueCycle.getInsClaimQueueBannerDetails(TransactionID, PatientID, nNextActionPatientInsID, nNextActionContactID, nAccountID);
                if (_dsInsuranceLogQueueBanner != null && _dsInsuranceLogQueueBanner.Tables.Count > 0)
                {
                    if (_dsInsuranceLogQueueBanner.Tables[0] != null && _dsInsuranceLogQueueBanner.Tables[0].Rows.Count > 0)
                    {
                        _PatientName = _dsInsuranceLogQueueBanner.Tables[0].Rows[0]["PatientName"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[0].Rows[0]["PatientName"].ToString();
                        _PatientCode = _dsInsuranceLogQueueBanner.Tables[0].Rows[0]["PatientCode"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[0].Rows[0]["PatientCode"].ToString();
                        _DateOfBirth = _dsInsuranceLogQueueBanner.Tables[0].Rows[0]["DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(_dsInsuranceLogQueueBanner.Tables[0].Rows[0]["DOB"]);
                        _Gender = _dsInsuranceLogQueueBanner.Tables[0].Rows[0]["Gender"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[0].Rows[0]["Gender"].ToString();
                        _NextAppointment = _dsInsuranceLogQueueBanner.Tables[0].Rows[0]["NextAppointment"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[0].Rows[0]["NextAppointment"].ToString(); ;
                        _EMRAlerts = _dsInsuranceLogQueueBanner.Tables[0].Rows[0]["EMRAlerts"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[0].Rows[0]["EMRAlerts"].ToString(); ;
                    }
                    else
                    {
                        _PatientName = "";
                        _PatientCode = "";
                        _DateOfBirth = DateTime.MinValue;
                        _Gender = "";
                        _NextAppointment = "";
                        _EMRAlerts = "";

                    }

                    lblPatientName.Text = _PatientName;
                    lblDOB.Text = Convert.ToString(_DateOfBirth.ToString("MM/dd/yyyy")) + "(" + CalculateAge(_DateOfBirth) + "y)";
                    lblGender.Text = _Gender;
                    lblPatientCode.Text = _PatientCode;

                    if (gloStripControl.PatientStripControl.ShowEMRAlertsOnPatientBanner())
                    {
                        if (_EMRAlerts != "")
                        {
                            lblDemoEMRAlerts.Visible = true;
                            lblDemogloEMRAlertsCaption.Visible = true;
                            lblDemoEMRAlerts.ForeColor = Color.FromArgb(212, 0, 114);
                        }
                        else
                        {
                            lblDemoEMRAlerts.Visible = false;
                            //lblDemogloEMRAlertsCaption.Visible = false;
                        }
                        //lblDemogloEMRAlertsCaption.ForeColor = Color.FromArgb(212, 0, 114);
                    }
                    else
                    {
                        lblDemoEMRAlerts.Visible = false;
                        lblDemogloEMRAlertsCaption.Visible = false;
                    }

                    lblDemoNextAppt.Text = _NextAppointment;
                    lblDemoEMRAlerts.Text = _EMRAlerts;
                    this.toolTip1.SetToolTip(lblDemoNextAppt, _NextAppointment);
                    this.toolTip1.SetToolTip(lblDemoEMRAlerts, _EMRAlerts);

                    FillMedicalCategoryHashTable();
                    GetMedicalCategoryImage(PatientID);
                    if (_dsInsuranceLogQueueBanner.Tables[1] != null && _dsInsuranceLogQueueBanner.Tables[1].Rows.Count > 0)
                    {
                        lblPatAlert.Text = Convert.ToString(_dsInsuranceLogQueueBanner.Tables[1].Rows[0]["sAlertName"]);
                    }
                    else
                    {
                        lblPatAlert.Text = "";
                    }
                    if (_dsInsuranceLogQueueBanner.Tables[2] != null && _dsInsuranceLogQueueBanner.Tables[2].Rows.Count > 0)
                    {
                        lblPatNote.Text = Convert.ToString(_dsInsuranceLogQueueBanner.Tables[2].Rows[0]["Note"]);
                        this.toolTip1.SetToolTip(lblPatNote, Convert.ToString(_dsInsuranceLogQueueBanner.Tables[2].Rows[0]["Note"]));
                    }
                    else
                    {
                        lblPatNote.Text = "";
                        this.toolTip1.SetToolTip(lblPatNote, "");
                    }

                    if (_dsInsuranceLogQueueBanner.Tables[3] != null && _dsInsuranceLogQueueBanner.Tables[3].Rows.Count > 0)
                    {
                        _dcharges = Convert.ToDecimal(_dsInsuranceLogQueueBanner.Tables[3].Rows[0]["dTotalCharges"]);
                        lblInsPlan.Text = Convert.ToString(_dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sInsuranceName"]) + "     " + Convert.ToString(_dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sInsurancePhone"]);
                        _DateOfBirth = _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["SubscriberDOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(_dsInsuranceLogQueueBanner.Tables[3].Rows[0]["SubscriberDOB"]);
                        lblSubscriberName.Text = Convert.ToString(_dsInsuranceLogQueueBanner.Tables[3].Rows[0]["SubscriberName"]) + "     " + _DateOfBirth.ToString("MM/dd/yyyy");
                        //Convert.ToString(_dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sInsuranceID"]) + "     " + Convert.ToString(_dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sGroupNo"]);
                        lblInsuranceID.Text = Convert.ToString(_dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sInsuranceID"]);
                        lblGroupNo.Text = Convert.ToString(_dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sGroupNo"]);
                        lblClaimNo.Text = _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sClaimNo"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sClaimNo"].ToString().Trim();
                        lblDOSdt.Text = _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["DOS"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["DOS"].ToString().Trim();
                        lblClaimNote.Text = _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sClaimNote"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sClaimNote"].ToString();
                        lblProc.Text = Convert.ToString(_dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sCPTCode"]) + "    " + Convert.ToString(_dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sMod1Code"]);
                        lblDx.Text = _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sDx1Code"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sDx1Code"].ToString();
                        lblCharges.Text = "$ " + _dcharges.ToString("#0.00");
                        lblBalance.Text = "$ " + dTotalBalAmt.ToString("#0.00");
                        // TFL and DFL Changes
                        if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "")
                        {
                            lblTFLDFLDate.Text = _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["TFLDate"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["TFLDate"].ToString().Trim();
                            lblTflDfl.Text = "TFL Date :";
                            lblTflDfl.Visible = true;

                        }
                        else if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)) != "")
                        {
                            lblTFLDFLDate.Text = _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["DFLDate"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["DFLDate"].ToString().Trim();
                            lblTflDfl.Text = "DFL Date :";
                            lblTflDfl.Visible = true;

                        }
                        else
                        {
                           
                            lblTFLDFLDate.Text = "";

                        }
                        if (Convert.ToString(_dsInsuranceLogQueueBanner.Tables[3].Rows[0]["ClaimFollowUpDate"]).Trim() != "")
                        {
                            lblFollowUpDt.Text = Convert.ToString(_dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sFollowupCode"]) + "    " + _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["ClaimFollowUpDate"];
                            if (Convert.ToDateTime(_dsInsuranceLogQueueBanner.Tables[3].Rows[0]["ClaimFollowUpDate"].ToString()) <= DateTime.Now)
                            {
                                lblFollowUpDt.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblFollowUpDt.Font, FontStyle.Bold);
                                lblFollowUpDt.ForeColor = System.Drawing.Color.Maroon;
                            }

                            else
                            {
                                lblFollowUpDt.ForeColor = System.Drawing.Color.Black;
                            }
                        }

                        this.toolTip1.SetToolTip(lblClaimNote, _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sClaimNote"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[3].Rows[0]["sClaimNote"].ToString());
                       
                    }
                    else
                    {
                        lblInsPlan.Text = "";
                        _DateOfBirth = DateTime.MinValue;
                        lblSubscriberName.Text = "";
                        lblClaimNo.Text = "";
                        lblDOSdt.Text = "";
                        lblClaimNote.Text = "";
                        lblProc.Text = "";
                        lblDx.Text = "";
                        lblCharges.Text = "$ 0.00";
                        lblBalance.Text = "$ 0.00";
                        lblFollowUpDt.Text = "";
                        lblInsuranceID.Text = "";
                        lblGroupNo.Text = "";
                        this.toolTip1.SetToolTip(lblClaimNote, "");

                    }
                    if (_dsInsuranceLogQueueBanner.Tables[4] != null && _dsInsuranceLogQueueBanner.Tables[4].Rows.Count > 0)
                    {
                        lblActNo.Text = _dsInsuranceLogQueueBanner.Tables[4].Rows[0]["sAccount"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[4].Rows[0]["sAccount"].ToString();
                        lblActNote.Text = _dsInsuranceLogQueueBanner.Tables[4].Rows[0]["sAccountNote"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[4].Rows[0]["sAccountNote"].ToString();
                        this.toolTip1.SetToolTip(lblActNote, Convert.ToString(_dsInsuranceLogQueueBanner.Tables[4].Rows[0]["sAccountNote"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[4].Rows[0]["sAccountNote"]));
                    }
                    else
                    {
                        lblActNo.Text = "";
                        lblActNote.Text = "";
                        this.toolTip1.SetToolTip(lblActNote, "");

                    }
                    if (_dsInsuranceLogQueueBanner.Tables[5] != null && _dsInsuranceLogQueueBanner.Tables[5].Rows.Count > 0)
                    {
                        lblLastFiling.Text = (Convert.ToString(_dsInsuranceLogQueueBanner.Tables[5].Rows[0]["dtLastBilled"]).Trim() == "" ? string.Empty : String.Format("{0:MM/dd/yyyy}", _dsInsuranceLogQueueBanner.Tables[5].Rows[0]["dtLastBilled"]));

                    }
                    else
                    {
                        lblLastFiling.Text = "";
                    }
                    if (_dsInsuranceLogQueueBanner.Tables[6] != null && _dsInsuranceLogQueueBanner.Tables[6].Rows.Count > 0)
                    {
                        lblLastRemit.Text = _dsInsuranceLogQueueBanner.Tables[6].Rows[0]["dtLastRemitteded"] == DBNull.Value ? string.Empty : _dsInsuranceLogQueueBanner.Tables[6].Rows[0]["dtLastRemitteded"].ToString();
                    }
                    else
                    {
                        lblLastRemit.Text = "";
                    }

                    if (_dsInsuranceLogQueueBanner.Tables[7] != null && _dsInsuranceLogQueueBanner.Tables[7].Rows.Count > 0)
                    {

                        _dBalance = Convert.ToDecimal(_dsInsuranceLogQueueBanner.Tables[7].Rows[0]["TotalBalanceAmount"]);
                        lblBalance.Text = "$ " + _dBalance.ToString("#0.00");
                    }
                    else
                    {
                        lblBalance.Text = "";
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (_dsInsuranceLogQueueBanner != null) { _dsInsuranceLogQueueBanner.Dispose(); }
            }
        }

        private Int32 CalculateAge(DateTime birthDate)
        {
            DateTime now = DateTime.Today;
            Int32 years = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                --years;
            return years;
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g != null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }

            return width;
        }

        void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private Boolean ModifyCharge()
        {
            Boolean _IsModified = false;

            try
            {
                gloAccountsV2.gloPatientFinancialViewV2 objPatFinacialView = null;
                if (oPatientControl.IsAllAccPatSelected)
                {
                    objPatFinacialView = new gloAccountsV2.gloPatientFinancialViewV2(0);
                }

                Int64 ParamTransactionId = 0;
                Int64 PatientID = 0;
                gloBilling ogloBilling = new gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                if (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)) != "")
                {

                    ParamTransactionId = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index));
                    if (Convert.ToInt32(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["bIsVoid"].Index)) == 1)
                    {

                        PatientID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index));
                        _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, true, true, this);
                    }
                    else
                    {
                        ParamTransactionId = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index));
                        PatientID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index));
                        if (ParamTransactionId != 0)
                            _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, false, true, this);
                    }

                }

                ogloBilling.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
            return _IsModified;
        }

        #endregion

        #region " Form Events "

        private void frmRevenueCycle_Load(object sender, EventArgs e)
        {

            try
            {
                PatientDetail oSelectedPatientDetail = new PatientDetail();                
                bIsShowClaimStatus = CL_FollowUpCode.ShowClaimStatus(oSelectedPatientDetail.ContactID, gloGlobal.gloPMGlobal.ClinicID);
                tsb_ClaimStatus.Visible = bIsShowClaimStatus;

                tbPatientFinancial.TabPages.Remove(tbpgBadDebtAcctQueue);
                btn_ModifyPatient.BackgroundImage = global::gloBilling.Properties.Resources.Patient;
                btn_ModifyPatient.BackgroundImageLayout = ImageLayout.Center;
                worker.WorkerSupportsCancellation = true;
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

                workerClaimIns.WorkerSupportsCancellation = true;
                workerClaimIns.DoWork += new DoWorkEventHandler(workerClaimIns_DoWork);
                workerClaimIns.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerClaimIns_RunWorkerCompleted);

                //workerBadDebtAccounts.WorkerSupportsCancellation = true;
                //workerBadDebtAccounts.DoWork += new DoWorkEventHandler(workerBadDebtAccounts_DoWork);
                //workerBadDebtAccounts.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerBadDebtAccounts_RunWorkerCompleted);

                FillFollowUpActionsAcc();
                FillFollowUpActionsInsClaim();
               // FillFollowUpActionsBadDebtAccount();

                FillRevenueSummary();
                //FillRevenueSummary();
                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                tom.SetTabOrder(scheme);
                if (IsgloCollectEnable())
                    SetgloCollectUserDefaulting();

                tspTransferClaimBalance.Visible = false;
                // TFL and DFL Changes
                C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle;
                C1.Win.C1FlexGrid.CellStyle csTFLBeforeDUE;
                try
                {
                    if (c1InsClaimSchedule.Styles.Contains("cs_EditableCurrencyStyle"))
                    {
                        csEditableCurrencyStyle = c1InsClaimSchedule.Styles["cs_EditableCurrencyStyle"];
                    }
                    else
                    {
                        csEditableCurrencyStyle = c1InsClaimSchedule.Styles.Add("cs_EditableCurrencyStyle");
                        csEditableCurrencyStyle.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(148)))));
                        csEditableCurrencyStyle.ForeColor = Color.White;
                        csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;
                    }

                    if (c1InsClaimSchedule.Styles.Contains("csTFLBeforeDUE"))
                    {
                        csTFLBeforeDUE = c1InsClaimSchedule.Styles["csTFLBeforeDUE"];
                    }
                    else
                    {
                        csTFLBeforeDUE = c1InsClaimSchedule.Styles.Add("csTFLBeforeDUE");
                        csTFLBeforeDUE.BackColor = Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
                        csTFLBeforeDUE.ForeColor = Color.Black;
                        csTFLBeforeDUE.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;
                    }
                }
                catch
                {
                    csEditableCurrencyStyle = c1InsClaimSchedule.Styles.Add("cs_EditableCurrencyStyle");
                    csTFLBeforeDUE = c1InsClaimSchedule.Styles.Add("csTFLBeforeDUE");
                }


            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                //if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                //{
                //    panel10.Visible = true;
                //}
                //else
                //{
                //    tbPatientFinancial.TabPages.Remove(tbpgBadDebtAcctQueue);
                //    panel10.Visible = false;
                //}
            }
        }

        #region "Bad Debt Worker"

        //void workerBadDebtAccounts_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    try
        //    {
        //        this.c1BadDebtLogView.EnterCell -= new System.EventHandler(this.c1BadDebtLogView_EnterCell);

        //        c1BadDebtLogView.SuspendLayout();
        //        c1BadDebtLogView.AutoResize = false;
        //        c1BadDebtLogView.Redraw = false;
        //        DataTable _dtBadDebtLogQueue = (DataTable)e.Result;
        //        if (e.Result != null)
        //        {
        //            c1BadDebtLogView.DataSource = _dtBadDebtLogQueue;

        //            if (nBadDebtActSortedColumn > -1)
        //                c1BadDebtLogView.Sort(oBadDebtActSortFlags, nBadDebtActSortedColumn);

        //            c1BadDebtLogView.Cols[0].DataType = typeof(bool);
        //            c1BadDebtLogView.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
        //            c1BadDebtLogView.SetData(0, 0, "Select All", false);
        //            c1BadDebtLogView.Cols[0].Style.TextAlign = TextAlignEnum.CenterCenter;
        //            c1BadDebtLogView.Cols[0].AllowEditing = true;
        //            c1BadDebtLogView.Cols[0].AllowSorting = false;

        //            if (!gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_FollowupQueue"))
        //                c1PALogView.Cols["BusinessCenter"].Visible = false;
        //            else
        //                c1PALogView.Cols["BusinessCenter"].Visible = true;

        //            //code start-Added by kanchan on 20130611 to maintain selection
        //            if (nBadDebtActSortedColumn > -1)
        //            {
        //                //Resolved bug no. 92109::Patient account follow-up queue>>transfer to agency>>Application shows Exception after clicking on save and close button of transfer close date pop-up
        //                if (c1BadDebtLogView.Selection.r1 > -1 && c1BadDebtLogView.Selection.r2 > -1)
        //                {
        //                    c1BadDebtLogView.Selection.Clear(ClearFlags.Style);
        //                }
        //                //

        //                if (iBadDebtAccountNextSelRow < c1BadDebtLogView.Rows.Count)
        //                {
        //                    CellRange cr;
        //                    cr = c1BadDebtLogView.GetCellRange(iBadDebtAccountNextSelRow, 1);
        //                    c1BadDebtLogView.Select(cr, true);
        //                }
        //            }
        //            else if (_BadDebtAccountSelRow > -1)
        //            {
        //                foreach (C1.Win.C1FlexGrid.Row rw in c1BadDebtLogView.Rows)
        //                {
        //                    CurrencyManager cm = (CurrencyManager)BindingContext[this.c1BadDebtLogView.DataSource];
        //                    DataRowView dr = rw.DataSource as DataRowView;
        //                    if (dr != null)
        //                    {
        //                        int currIndex = dr.Row.Table.Rows.IndexOf(dr.Row);
        //                        if (currIndex == _BadDebtAccountSelRow)
        //                        {
        //                            CellRange cr = c1BadDebtLogView.GetCellRange(rw.Index, 1);
        //                            // to scroll the selected row in the visible area
        //                            c1BadDebtLogView.Select(cr, true);
        //                            cr = c1BadDebtLogView.GetCellRange(rw.Index, 0, rw.Index, c1BadDebtLogView.Cols.Count - 1);
        //                            c1BadDebtLogView.Select(cr, false);
        //                            iBadDebtAccountSelRow = rw.Index;

        //                            break;
        //                        }
        //                    }
        //                }
        //            }

        //            if (c1BadDebtLogView.Rows.Count > 1)
        //            {
        //                if (iBadDebtAccountSelRow != c1BadDebtLogView.RowSel || iBadDebtAccountSelRow == 1)
        //                {
        //                    if (c1BadDebtLogView.RowSel > 0)
        //                    {
        //                        if (
        //                                c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index) != null && Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index)) != 0
        //                               && c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nAccountID"].Index) != null && Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nAccountID"].Index)) != 0
        //                           )
        //                        {
        //                            LoadBadDebtPatientStrip(Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index)), Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1PALogView.Cols["nAccountID"].Index)), false);
        //                            oPatientControlBadDebt.Visible = true;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    LoadBadDebtPatientStrip(Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index)), Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nAccountID"].Index)), false);
        //                    oPatientControlBadDebt.Visible = true;
        //                }

        //            }
        //            else
        //            {
        //                oPatientControlBadDebt.Visible = false;
        //            }

        //        }
        //        if (cmbBadDebtScheduleAction.SelectedValue.ToString() != "" && c1BadDebtLogView.Rows.Count > 1)
        //            c1BadDebtLogView.Cols[0].Visible = true;
        //        else
        //            c1BadDebtLogView.Cols[0].Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        pnlBadDebtProgressIndication.SendToBack();
        //        c1BadDebtLogView.ResumeLayout();
        //        c1BadDebtLogView.AutoResize = true;
        //        c1BadDebtLogView.Redraw = true;
        //        this.c1BadDebtLogView.EnterCell += new System.EventHandler(this.c1BadDebtLogView_EnterCell);
        //        ShowHideBadDebtToolstripButtons();
        //    }
        //}

        //void workerBadDebtAccounts_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //    DataTable _dtBadDebtAccountLogQueue = null;
        //    string filterByAction = string.Empty;
        //    //bool filterIncludesFutureFollowup = false;
        //    //Int64 filterByBusinessCenterId = 0;

        //    try
        //    {
        //        //if (cmbBadDebtScheduleAction != null && cmbBadDebtScheduleAction.Items.Count > 0)
        //        //{ filterByAction = Convert.ToString(cmbBadDebtScheduleAction.SelectedValue).Trim(); }

        //        //if (cmbBadDebtBusinessCenter.Items.Count > 0 && pnlBadDebtBusinessCenter.Visible == true)
        //        //{ filterByBusinessCenterId = Convert.ToInt64(cmbBadDebtBusinessCenter.SelectedValue); }
        //        //else
        //        //{ filterByBusinessCenterId = 0; }


        //        //if (chkBadDebtAccIncludeFutureDetail.Checked)
        //        //{ filterIncludesFutureFollowup = true; }

        //        _dtBadDebtAccountLogQueue = CLsCL_RevenueCycle.getBadDebtAccQueueDetails(filterBadDebtQueueByAction, filterBadDebtQueueIncludesFutureFollowup, filterBadDebtQueueByBusinessCenterId);
        //        e.Result = _dtBadDebtAccountLogQueue.Copy();
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (_dtBadDebtAccountLogQueue != null) { _dtBadDebtAccountLogQueue.Dispose(); _dtBadDebtAccountLogQueue = null; }
        //        if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
        //    }
        //}

        #endregion "Bad Debt Worker"

        void workerClaimIns_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                this.c1InsClaimSchedule.EnterCell -= new System.EventHandler(this.c1InsClaimSchedule_EnterCell);
                DataTable _dtClaimLogQueue = (DataTable)e.Result;
                if (e.Result != null)
                {
                    if (_dtClaimLogQueue != null)
                    {
                        gloGlobal.gloPMGlobal.SplitClaimColumn(_dtClaimLogQueue, _dtClaimLogQueue.Columns.IndexOf("sClaimNo"));
                    }
                    c1InsClaimSchedule.DataSource = _dtClaimLogQueue;

                    if (c1InsClaimSchedule.DataSource != null)
                    {
                        c1InsClaimSchedule.Cols["sClaimNo"].Sort = SortFlags.Ascending;
                    }

                    if(nInsSortedColumn > -1)
                        c1InsClaimSchedule.Sort(oInsSortFlags, nInsSortedColumn);

                    c1InsClaimSchedule.Cols[0].DataType = typeof(bool);
                    c1InsClaimSchedule.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    c1InsClaimSchedule.SetData(0, 0, "Select All", false);
                    c1InsClaimSchedule.Cols[0].Style.TextAlign = TextAlignEnum.CenterCenter;
                    c1InsClaimSchedule.Cols[0].AllowEditing = true;
                    c1InsClaimSchedule.Cols[0].AllowSorting = false;

                    if (!gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_FollowupQueue"))
                        c1InsClaimSchedule.Cols["BusinessCenter"].Visible = false;
                    else
                        c1InsClaimSchedule.Cols["BusinessCenter"].Visible = true;
                    //code start-Added by kanchan on 20130611 to maintain selection
                    if (nInsSortedColumn > -1)
                    {
                        c1InsClaimSchedule.Selection.Clear(ClearFlags.Style);
                        if (iClaimNextSelRow < c1InsClaimSchedule.Rows.Count)
                        {
                            CellRange cr;
                            cr = c1InsClaimSchedule.GetCellRange(iClaimNextSelRow, 1);
                            c1InsClaimSchedule.Select(cr, true);
                        }
                    }
                    else if (iClaimSelRow > -1)
                    {
                        foreach (C1.Win.C1FlexGrid.Row rw in c1InsClaimSchedule.Rows)
                        {
                            CurrencyManager cm = (CurrencyManager)BindingContext[this.c1InsClaimSchedule.DataSource];
                            DataRowView dr = rw.DataSource as DataRowView;
                            if (dr != null)
                            {
                                int currIndex = dr.Row.Table.Rows.IndexOf(dr.Row);
                                if (currIndex == iClaimSelRow)
                                {
                                    CellRange cr = c1InsClaimSchedule.GetCellRange(rw.Index, 1);
                                    // to scroll the selected row in the visible area
                                    c1InsClaimSchedule.Select(cr, true);
                                    cr = c1InsClaimSchedule.GetCellRange(rw.Index, 0, rw.Index, c1InsClaimSchedule.Cols.Count - 1);
                                    c1InsClaimSchedule.Select(cr, false);
                                    break;
                                }
                            }
                            
                        }
                    }
                    //code end-Added by kanchan on 20130611 to maintain selection
                    if (c1InsClaimSchedule.Rows.Count > 1)
                    {
                        if (c1InsClaimSchedule.RowSel > 0)
                        {
                            //if (iClaimSelRow != c1InsClaimSchedule.RowSel || iClaimSelRow == 1)
                            //{
                            if (
                                    (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)) != "") &&
                                    (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index)) != "") &&
                                    (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index)) != "") &&
                                    (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index)) != "")
                                )
                            {
                                FillInsuranceClaimTabBanner(Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index)));
                                pnlInsuranceClaimBanner.Visible = true;

                                // TFL and DFL Changes
                                if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "")
                                {
                                    lblTFLDFLDate.Text = Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["Tfl_Dfl_Date"].Index));
                                    lblTflDfl.Text = "TFL Date :";
                                    lblTflDfl.Visible = true;

                                }
                                else if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)) != "")
                                {
                                    lblTFLDFLDate.Text = Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["Tfl_Dfl_Date"].Index));
                                    lblTflDfl.Text = "DFL Date :";
                                    lblTflDfl.Visible = true;

                                }
                                else
                                {

                                    lblTFLDFLDate.Text = "";
                                    lblTflDfl.Visible = false;

                                }
      
                                    // TFL and DFL Changes
                                    if ((Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "" || Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)) != ""))
                                    {
                                        Int64 TFLDays = 0;
                                        if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "")
                                        {
                                            TFLDays = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index));
                                        }
                                        else if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)) != "")
                                        { TFLDays = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)); }


                                        if (TFLDays > 0 && TFLDays <= 30)
                                        {
                                            lblTFLDFLDate.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblFollowUpDt.Font, FontStyle.Bold);
                                            lblTFLDFLDate.ForeColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
                                        }
                                        else if (TFLDays < 0)
                                        {
                                            lblTFLDFLDate.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblFollowUpDt.Font, FontStyle.Bold);
                                            lblTFLDFLDate.ForeColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(148)))));
                                        }
                                        else
                                        {
                                            lblTFLDFLDate.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblFollowUpDt.Font, FontStyle.Bold);
                                            lblTFLDFLDate.ForeColor = Color.Black;
                                        }
                                    }
                                    else
                                    {
                                        lblTflDfl.Visible = false;
                                        lblTFLDFLDate.Visible = false;
                                    }
                                }
                            

                            
                                foreach (C1.Win.C1FlexGrid.Row rw in c1InsClaimSchedule.Rows)
                                {

                                    if (rw.Index > 0 && rw["TFLDays"] != DBNull.Value)
                                    {

                                        Int64 TFLDays = Convert.ToInt64(c1InsClaimSchedule.GetData(rw.Index, c1InsClaimSchedule.Cols["TFLDays"].Index));

                                        if (TFLDays > 0 && TFLDays <= 30)
                                        {
                                            c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["TFLDays"].Index, "csTFLBeforeDUE");
                                        }
                                        else if (TFLDays < 0)
                                        {
                                            c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["TFLDays"].Index, "cs_EditableCurrencyStyle");
                                        }
                                        else
                                        {
                                            c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["TFLDays"].Index, "");
                                        }
                                    }

                                    if (rw.Index > 0 && rw["DFLDays"] != DBNull.Value)
                                    {

                                        Int64 DFLDays = Convert.ToInt64(c1InsClaimSchedule.GetData(rw.Index, c1InsClaimSchedule.Cols["DFLDays"].Index));

                                        if (DFLDays > 0 && DFLDays <= 30)
                                        {
                                            c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["DFLDays"].Index, "csTFLBeforeDUE");
                                        }
                                        else if (DFLDays < 0)
                                        {
                                            c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["DFLDays"].Index, "cs_EditableCurrencyStyle");
                                        }
                                        else
                                        {
                                            c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["DFLDays"].Index, "");
                                        }
                                    }
                                }

                                if (ChkIncludeBalances.Checked)
                                {
                                    c1InsClaimSchedule.Cols["TotalBalanceAmount"].Visible = true;
                                }
                                else
                                {
                                    c1InsClaimSchedule.Cols["TotalBalanceAmount"].Visible = false;
                                }
                            //}
                            //else
                            //{
                            //    pnlInsuranceClaimBanner.Visible = true;
                            //}
                        }
                    }
                    else
                    {
                        pnlInsuranceClaimBanner.Visible = false;
                        //ClearClaimBanner();
                    }

                }
                if (cmbClaimFollowupAction.SelectedValue.ToString() != "" && c1InsClaimSchedule.Rows.Count > 1)
                    c1InsClaimSchedule.Cols[0].Visible = true;
                else
                    c1InsClaimSchedule.Cols[0].Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.c1InsClaimSchedule.EnterCell += new System.EventHandler(this.c1InsClaimSchedule_EnterCell);
                pnlProgressClaimIndication.SendToBack();
                SetToolStripButtonForInsuranceQueue();
            }
        }

        void workerClaimIns_DoWork(object sender, DoWorkEventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            try
            {
                DataTable _dtInsuranceLogQueue = CLsCL_RevenueCycle.getInsClaimQueueDetails(sActonCode, sInsCmpny, bIsFutureItemAllow, nInsBusinessId, bBeforegloCollect, bgloCollect,ChkIncludeBalances.Checked);
                e.Result = _dtInsuranceLogQueue;


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                this.c1PALogView.EnterCell -= new System.EventHandler(this.c1PALogView_EnterCell);
                c1PALogView.SuspendLayout();
                c1PALogView.AutoResize = false;
                c1PALogView.Redraw = false;
                DataTable _dtClaimLogQueue = (DataTable)e.Result;
                if (e.Result != null)
                {
                    c1PALogView.DataSource = _dtClaimLogQueue;

                    if (nActSortedColumn > -1)
                        c1PALogView.Sort(oActSortFlags, nActSortedColumn);

                    c1PALogView.Cols[0].DataType = typeof(bool);
                    c1PALogView.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    c1PALogView.SetData(0, 0, "Select All", false);
                    c1PALogView.Cols[0].Style.TextAlign = TextAlignEnum.CenterCenter;
                    c1PALogView.Cols[0].AllowEditing = true;
                    c1PALogView.Cols[0].AllowSorting = false;

                    if (!gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_FollowupQueue"))
                        c1PALogView.Cols["BusinessCenter"].Visible = false;
                    else
                        c1PALogView.Cols["BusinessCenter"].Visible = true;

                    //code start-Added by kanchan on 20130611 to maintain selection
                    if (nActSortedColumn > -1)
                    {
                        //Resolved bug no. 92109::Patient account follow-up queue>>transfer to agency>>Application shows Exception after clicking on save and close button of transfer close date pop-up
                        if (c1PALogView.Selection.r1 > -1 && c1PALogView.Selection.r2 > -1)
                        {
                            c1PALogView.Selection.Clear(ClearFlags.Style);
                        }
                        //

                        if (iAccountNextSelRow < c1PALogView.Rows.Count)
                        {
                            CellRange cr;
                            cr = c1PALogView.GetCellRange(iAccountNextSelRow, 1);
                            c1PALogView.Select(cr, true);
                        }
                    }
                    else if (_AccountSelRow > -1)
                    {
                        foreach (C1.Win.C1FlexGrid.Row rw in c1PALogView.Rows)
                        {
                            CurrencyManager cm = (CurrencyManager)BindingContext[this.c1PALogView.DataSource];
                            DataRowView dr = rw.DataSource as DataRowView;
                            if (dr != null)
                            {
                                int currIndex = dr.Row.Table.Rows.IndexOf(dr.Row);
                                if (currIndex == _AccountSelRow)
                                {
                                    CellRange cr = c1PALogView.GetCellRange(rw.Index, 1);
                                    // to scroll the selected row in the visible area
                                    c1PALogView.Select(cr, true);
                                    cr = c1PALogView.GetCellRange(rw.Index, 0, rw.Index, c1PALogView.Cols.Count - 1);
                                    c1PALogView.Select(cr, false);
                                    iAccountSelRow = rw.Index;
                                    
                                    break;
                                }
                            }
                        }
                    }
                    //if (c1PALogView.Rows.Count - 1 > 0)
                    //{
                    //    if (c1PALogView.Rows.Count - 1 >= iAccountSelRow)
                    //        c1PALogView.Row = iAccountSelRow;
                    //    else if (c1PALogView.Rows.Count > 1)
                    //    {
                    //        c1PALogView.Row = c1PALogView.Rows.Count - 1;
                    //        iAccountSelRow = c1PALogView.Rows.Count - 1;
                    //    }
                    //}
                    //code end-Added by kanchan on 20130611 to maintain selection
                    if (c1PALogView.Rows.Count > 1)
                    {
                        if (iAccountSelRow != c1PALogView.RowSel || iAccountSelRow == 1)
                        {
                            if (c1PALogView.RowSel > 0)
                            {
                                if (
                                        c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index) != null && Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)) != 0
                                       && c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index) != null && Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)) != 0
                                   )
                                {
                                    LoadPatientStrip(Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)), Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)), false);
                                    oPatientControl.Visible = true;
                                }
                            }
                        }
                        else
                        {
                            LoadPatientStrip(Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)), Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)), false);
                            oPatientControl.Visible = true;
                        }

                    }
                    else
                    {
                        oPatientControl.Visible = false;
                        chkBatchTemplate.Checked = false;
                        chkBatchTemplate.Visible = false;
                    }

                }
                if (cmbAcctFollowUpAction.SelectedValue.ToString() != "" && c1PALogView.Rows.Count > 1)
                    c1PALogView.Cols[0].Visible = true;
                else
                    c1PALogView.Cols[0].Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                pnlProgressIndication.SendToBack();
                c1PALogView.ResumeLayout();
                c1PALogView.AutoResize = true;
                c1PALogView.Redraw = true;
                this.c1PALogView.EnterCell += new System.EventHandler(this.c1PALogView_EnterCell);
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            EFCollection context = null;
            SqlConnectionStringBuilder sb = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            try
            {
                DataTable _dtAccountLogQueue = CLsCL_RevenueCycle.getPatAccQueueDetails(sActonCode, bIsFutureItemAllow, nBusinessId);
                e.Result = _dtAccountLogQueue;

                #region "Commented Code"

                //context = new EFCollection();
                //sb = new SqlConnectionStringBuilder(((EntityConnection)context.Connection).StoreConnection.ConnectionString);
                //sb.ConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
                //((EntityConnection)context.Connection).StoreConnection.ConnectionString = sb.ConnectionString;


                //if (bIsFutureItemAllow)
                //{
                //    if (sActonCode != string.Empty)
                //    {
                //        var result = (from schedule in context.CL_FollowupSchedule_Acct
                //                      join acctmst in context.PA_Accounts
                //                       on schedule.nAccountID equals acctmst.nPAccountID
                //                      join pat in context.Patients
                //                       on schedule.nPatientID equals pat.nPatientID
                //                      join provider in context.Provider_MST
                //                       on pat.nProviderID equals provider.nProviderID
                //                      join otherPat in context.Patient_OtherContacts
                //                       on acctmst.nGuarantorID equals otherPat.nPatientContactID
                //                      where schedule.sFollowupCode == sActonCode
                //                      orderby schedule.dtAcctFollowUpDate
                //                      select new
                //                      {
                //                          sProviderName = provider.sFirstName.Trim() + " " + provider.sMiddleName.Trim() + " " + provider.sLastName.Trim(),
                //                          sGurantorName = otherPat.sFirstName.Trim() + " " + otherPat.sMiddleName.Trim() + " " + otherPat.sLastName.Trim(),
                //                          nAccountID = acctmst.nPAccountID,
                //                          sAccountNo = acctmst.sAccountNo,
                //                          sGuarantorPhone = otherPat.sPhone,
                //                          nPatientID = pat.nPatientID,
                //                          sPatientName = pat.sFirstName.Trim() + " " + pat.sMiddleName.Trim() + " " + pat.sLastName.Trim(),
                //                          dtNextScheduledate = schedule.dtAcctFollowUpDate,
                //                          nAccScheduleID = schedule.nID,
                //                          sNextScheduleDescription = schedule.sFollowupCode + " - " + schedule.sFollowupDescription,
                //                          dtCreatedDateTime = schedule.dtCreatedDateTime,
                //                          bIsTemplate = "true"
                //                      }).ToList();

                //        var finalresult = (from res in result.AsEnumerable()
                //                           select new
                //                           {
                //                               sProviderName = res.sProviderName,
                //                               sGurantorName = res.sGurantorName,
                //                               nAccountID = res.nAccountID,
                //                               sAccountNo = res.sAccountNo,
                //                               sGuarantorPhone = CL_FollowUpCode.FormatPhoneNo(res.sGuarantorPhone),
                //                               nPatientID = res.nPatientID,
                //                               sPatientName = res.sPatientName,
                //                               dtNextScheduledate = res.dtNextScheduledate,
                //                               nAccScheduleID = res.nAccScheduleID,
                //                               sNextScheduleDescription = res.sNextScheduleDescription,
                //                               dtCreatedDateTime = res.dtCreatedDateTime,
                //                               bIsTemplate = "true"
                //                           }).ToList();

                //        e.Result = finalresult;
                //    }
                //    else
                //    {
                //        var result = (from schedule in context.CL_FollowupSchedule_Acct
                //                      join acctmst in context.PA_Accounts
                //                       on schedule.nAccountID equals acctmst.nPAccountID
                //                      join pat in context.Patients
                //                       on schedule.nPatientID equals pat.nPatientID
                //                      join provider in context.Provider_MST
                //                       on pat.nProviderID equals provider.nProviderID
                //                      join otherPat in context.Patient_OtherContacts
                //                       on acctmst.nGuarantorID equals otherPat.nPatientContactID
                //                      orderby schedule.dtAcctFollowUpDate
                //                      select new
                //                      {
                //                          sProviderName = provider.sFirstName.Trim() + " " + provider.sMiddleName.Trim() + " " + provider.sLastName.Trim(),
                //                          sGurantorName = otherPat.sFirstName.Trim() + " " + otherPat.sMiddleName.Trim() + " " + otherPat.sLastName.Trim(),
                //                          nAccountID = acctmst.nPAccountID,
                //                          sAccountNo = acctmst.sAccountNo,
                //                          sGuarantorPhone = otherPat.sPhone,
                //                          nPatientID = pat.nPatientID,
                //                          sPatientName = pat.sFirstName.Trim() + " " + pat.sMiddleName.Trim() + " " + pat.sLastName.Trim(),
                //                          dtNextScheduledate = schedule.dtAcctFollowUpDate,
                //                          nAccScheduleID = schedule.nID,
                //                          sNextScheduleDescription = schedule.sFollowupCode + " - " + schedule.sFollowupDescription,
                //                          dtCreatedDateTime = schedule.dtCreatedDateTime,
                //                          bIsTemplate = "true"
                //                      }).ToList();

                //        var finalresult = (from res in result.AsEnumerable()
                //                           select new
                //                           {
                //                               sProviderName = res.sProviderName,
                //                               sGurantorName = res.sGurantorName,
                //                               nAccountID = res.nAccountID,
                //                               sAccountNo = res.sAccountNo,
                //                               sGuarantorPhone = CL_FollowUpCode.FormatPhoneNo(res.sGuarantorPhone),
                //                               nPatientID = res.nPatientID,
                //                               sPatientName = res.sPatientName,
                //                               dtNextScheduledate = res.dtNextScheduledate,
                //                               nAccScheduleID = res.nAccScheduleID,
                //                               sNextScheduleDescription = res.sNextScheduleDescription,
                //                               dtCreatedDateTime = res.dtCreatedDateTime,
                //                               bIsTemplate = "true"
                //                           }).ToList();

                //        e.Result = finalresult;
                //    }
                //}
                //else
                //{
                //    if (sActonCode != string.Empty)
                //    {
                //        var result = (from schedule in context.CL_FollowupSchedule_Acct
                //                      join acctmst in context.PA_Accounts
                //                       on schedule.nAccountID equals acctmst.nPAccountID
                //                      join pat in context.Patients
                //                       on schedule.nPatientID equals pat.nPatientID
                //                      join provider in context.Provider_MST
                //                       on pat.nProviderID equals provider.nProviderID
                //                      join otherPat in context.Patient_OtherContacts
                //                       on acctmst.nGuarantorID equals otherPat.nPatientContactID
                //                      where schedule.sFollowupCode == sActonCode
                //                      && schedule.dtAcctFollowUpDate <= DateTime.Now
                //                      orderby schedule.dtAcctFollowUpDate
                //                      select new
                //                      {
                //                          sProviderName = provider.sFirstName.Trim() + " " + provider.sMiddleName.Trim() + " " + provider.sLastName.Trim(),
                //                          sGurantorName = otherPat.sFirstName.Trim() + " " + otherPat.sMiddleName.Trim() + " " + otherPat.sLastName.Trim(),
                //                          nAccountID = acctmst.nPAccountID,
                //                          sAccountNo = acctmst.sAccountNo,
                //                          sGuarantorPhone = otherPat.sPhone,
                //                          nPatientID = pat.nPatientID,
                //                          sPatientName = pat.sFirstName.Trim() + " " + pat.sMiddleName.Trim() + " " + pat.sLastName.Trim(),
                //                          dtNextScheduledate = schedule.dtAcctFollowUpDate,
                //                          nAccScheduleID = schedule.nID,
                //                          sNextScheduleDescription = schedule.sFollowupCode + " - " + schedule.sFollowupDescription,
                //                          dtCreatedDateTime = schedule.dtCreatedDateTime,
                //                          bIsTemplate = "true"
                //                      }).ToList();


                //        var finalresult = (from res in result.AsEnumerable()
                //                           select new
                //                           {
                //                               sProviderName = res.sProviderName,
                //                               sGurantorName = res.sGurantorName,
                //                               nAccountID = res.nAccountID,
                //                               sAccountNo = res.sAccountNo,
                //                               sGuarantorPhone = CL_FollowUpCode.FormatPhoneNo(res.sGuarantorPhone),
                //                               nPatientID = res.nPatientID,
                //                               sPatientName = res.sPatientName,
                //                               dtNextScheduledate = res.dtNextScheduledate,
                //                               nAccScheduleID = res.nAccScheduleID,
                //                               sNextScheduleDescription = res.sNextScheduleDescription,
                //                               dtCreatedDateTime = res.dtCreatedDateTime,
                //                               bIsTemplate = "true"
                //                           }).ToList();

                //        e.Result = finalresult;
                //    }
                //    else
                //    {
                //        var result = (from schedule in context.CL_FollowupSchedule_Acct
                //                      join acctmst in context.PA_Accounts
                //                       on schedule.nAccountID equals acctmst.nPAccountID
                //                      join pat in context.Patients
                //                       on schedule.nPatientID equals pat.nPatientID
                //                      join provider in context.Provider_MST
                //                       on pat.nProviderID equals provider.nProviderID
                //                      join otherPat in context.Patient_OtherContacts
                //                       on acctmst.nGuarantorID equals otherPat.nPatientContactID
                //                     where schedule.dtAcctFollowUpDate <= DateTime.Now
                //                     orderby schedule.dtAcctFollowUpDate
                //                     select new
                //                     {
                //                         sProviderName = provider.sFirstName.Trim() + " " + provider.sMiddleName.Trim() + " " + provider.sLastName.Trim(),
                //                         sGurantorName = otherPat.sFirstName.Trim() + " " + otherPat.sMiddleName.Trim() + " " + otherPat.sLastName.Trim(),
                //                         nAccountID = acctmst.nPAccountID,
                //                         sAccountNo = acctmst.sAccountNo,
                //                         sGuarantorPhone = otherPat.sPhone,
                //                         nPatientID = pat.nPatientID,
                //                         sPatientName = pat.sFirstName.Trim() + " " + pat.sMiddleName.Trim() + " " + pat.sLastName.Trim(),
                //                         dtNextScheduledate = schedule.dtAcctFollowUpDate,
                //                         nAccScheduleID = schedule.nID,
                //                         sNextScheduleDescription = schedule.sFollowupCode + " - " + schedule.sFollowupDescription,
                //                         dtCreatedDateTime = schedule.dtCreatedDateTime,
                //                         bIsTemplate = "true"
                //                     }).ToList();


                //        var finalresult = (from res in result.AsEnumerable()
                //                           select new
                //                           {
                //                               sProviderName = res.sProviderName,
                //                               sGurantorName = res.sGurantorName,
                //                               nAccountID = res.nAccountID,
                //                               sAccountNo = res.sAccountNo,
                //                               sGuarantorPhone = CL_FollowUpCode.FormatPhoneNo(res.sGuarantorPhone),
                //                               nPatientID = res.nPatientID,
                //                               sPatientName = res.sPatientName,
                //                               dtNextScheduledate = res.dtNextScheduledate,
                //                               nAccScheduleID = res.nAccScheduleID,
                //                               sNextScheduleDescription = res.sNextScheduleDescription,
                //                               dtCreatedDateTime = res.dtCreatedDateTime,
                //                               bIsTemplate = "true"
                //                           }).ToList();

                //e.Result = finalresult;

                //    }
                //}

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (context != null) { context.Dispose(); }
                if (sb != null) { sb = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }

        private void frmRevenueCycle_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void frmRevenueCycle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (worker.IsBusy)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
            if (workerClaimIns.IsBusy)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }

            //if (workerBadDebtAccounts.IsBusy)
            //{
            //    e.Cancel = true;
            //}
            //else
            //{
            //    e.Cancel = false;
            //}
        }

        #endregion

        #region "Form Control Events "

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {

            try
            {
                int _Counter = 0;
                switch (_CurrentControlType)
                {
                    case gloListControl.gloListControlType.InsuranceCompany:
                        {
                            
                            cmbInsuranceCompany.DataSource = null;
                            cmbInsuranceCompany.Items.Clear();
                            if (oListControl != null)
                            {
                                if (oListControl.SelectedItems.Count > 0)
                                {
                                    DataTable oBindTable = new DataTable();

                                    oBindTable.Columns.Add("ID");
                                    oBindTable.Columns.Add("DispName");

                                    for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                                    {
                                        DataRow oRow;
                                        oRow = oBindTable.NewRow();
                                        oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                        oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                        oBindTable.Rows.Add(oRow);
                                    }

                                    cmbInsuranceCompany.DataSource = oBindTable;
                                    cmbInsuranceCompany.DisplayMember = "DispName";
                                    cmbInsuranceCompany.ValueMember = "ID";
                                }

                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            try
            {
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch
                        {
                        }
                        

                    }
                    catch
                    {
                    }
                   
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbAcctFollowUpAction_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                combo = cmbAcctFollowUpAction;
                if (cmbAcctFollowUpAction.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbAcctFollowUpAction.Items[cmbAcctFollowUpAction.SelectedIndex])["sFollowUpAction"]), cmbAcctFollowUpAction) >= cmbAcctFollowUpAction.DropDownWidth - 20)
                    {
                        this.toolTip1.SetToolTip(cmbAcctFollowUpAction, Convert.ToString(((DataRowView)cmbAcctFollowUpAction.Items[cmbAcctFollowUpAction.SelectedIndex])["sFollowUpAction"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbAcctFollowUpAction, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbAcctFollowUpAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                combo = cmbAcctFollowUpAction;
                if (cmbAcctFollowUpAction.SelectedItem != null)
                {
                    //if (getWidthofListItems(Convert.ToString(((DataRowView)cmbAcctFollowUpAction.Items[cmbAcctFollowUpAction.SelectedIndex])["sFollowUpAction"]), cmbAcctFollowUpAction) >= cmbAcctFollowUpAction.DropDownWidth - 20)
                    //{
                    //    this.toolTip1.SetToolTip(cmbAcctFollowUpAction, Convert.ToString(((DataRowView)cmbAcctFollowUpAction.Items[cmbAcctFollowUpAction.SelectedIndex])["sFollowUpAction"]));
                    //}
                    //else
                    //{
                    //    this.toolTip1.SetToolTip(cmbAcctFollowUpAction, "");
                    //}

                    if (!(cmbAcctFollowUpAction.SelectedValue.ToString() != "" && cmbAcctFollowUpAction.SelectedValue.ToString() == getExternalCollectionFUAction()))
                    {
                        tspTransferClaimBalance.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbClaimFollowupAction_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                combo = cmbClaimFollowupAction;
                if (cmbClaimFollowupAction.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbClaimFollowupAction.Items[cmbClaimFollowupAction.SelectedIndex])["sFollowUpAction"]), cmbClaimFollowupAction) >= cmbClaimFollowupAction.DropDownWidth - 20)
                    {
                        this.toolTip1.SetToolTip(cmbClaimFollowupAction, Convert.ToString(((DataRowView)cmbClaimFollowupAction.Items[cmbClaimFollowupAction.SelectedIndex])["sFollowUpAction"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbClaimFollowupAction, "");
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbClaimFollowupAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                combo = cmbClaimFollowupAction;
                if (cmbClaimFollowupAction.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbClaimFollowupAction.Items[cmbClaimFollowupAction.SelectedIndex])["sFollowUpAction"]), cmbClaimFollowupAction) >= cmbClaimFollowupAction.DropDownWidth - 20)
                    {
                        this.toolTip1.SetToolTip(cmbClaimFollowupAction, Convert.ToString(((DataRowView)cmbClaimFollowupAction.Items[cmbClaimFollowupAction.SelectedIndex])["sFollowUpAction"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbClaimFollowupAction, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbInsuranceCompany_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                combo = cmbInsuranceCompany;
                if (cmbInsuranceCompany.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["DispName"]), cmbInsuranceCompany) >= cmbInsuranceCompany.DropDownWidth - 20)
                    {
                        this.toolTip1.SetToolTip(cmbInsuranceCompany, Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["DispName"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbInsuranceCompany, "");
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbInsuranceCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                combo = cmbInsuranceCompany;
                if (cmbInsuranceCompany.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["DispName"]), cmbInsuranceCompany) >= cmbInsuranceCompany.DropDownWidth - 20)
                    {
                        this.toolTip1.SetToolTip(cmbInsuranceCompany, Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["DispName"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbInsuranceCompany, "");
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tbPatientFinancial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                iAccountSelRow = 1;
                iClaimSelRow = 1;
                switch (this.tbPatientFinancial.SelectedTab.Name)
                {
                    case "tbPgDashBoard":
                        FillRevenueSummary();
                        break;

                    case "tbpgPatAcctQueue":
                        //LoadPatientAccountQueue();
                        pnlProgressIndication.BringToFront();

                        if (cmbAcctFollowUpAction.Items.Count > 0)
                        {
                            if (cmbAcctFollowUpAction.SelectedIndex > 0)
                            {
                                if (!chkPatAccIncludeFutureDetail.Checked)
                                {
                                    if (Convert.ToString(cmbAcctFollowUpAction.SelectedValue).Trim() != string.Empty)
                                    {
                                        DataTable _dtDefTemplate = new DataTable();
                                        _dtDefTemplate = CL_FollowUpCode.GetDefaultAssociateTemplate(Convert.ToString(cmbAcctFollowUpAction.SelectedValue), CollectionEnums.FollowUpType.PatientAccount);
                                        if (_dtDefTemplate != null && _dtDefTemplate.Rows.Count > 0)
                                        {
                                            chkBatchTemplate.Visible = true;
                                        }
                                        else
                                        {
                                            chkBatchTemplate.Checked = false;
                                            chkBatchTemplate.Visible = false;
                                        }
                                        chkBatchTemplate.Checked = false;
                                        chkBatchTemplate.Visible = false;
                                    }
                                    else
                                    {
                                        chkBatchTemplate.Checked = false;
                                        chkBatchTemplate.Visible = false;
                                    }
                                }
                                else
                                {
                                    chkBatchTemplate.Checked = false;
                                    chkBatchTemplate.Visible = false;
                                }
                            }
                            else
                            {
                                chkBatchTemplate.Checked = false;
                                chkBatchTemplate.Visible = false;
                            }
                            sActonCode = Convert.ToString(cmbAcctFollowUpAction.SelectedValue).Trim();
                        }
                        if (cmbBusinessCenter.Items.Count > 0 && pnlBusinessCenter.Visible == true)
                        {
                            nBusinessId = Convert.ToInt64(cmbBusinessCenter.SelectedValue);
                        }
                        else
                        {
                            nBusinessId = 0;
                        }
                        if (chkPatAccIncludeFutureDetail.Checked)
                        {
                            bIsFutureItemAllow = true;
                        }
                        //if (chkBeforegloCollect.Checked)
                        //{
                        //    bBeforegloCollect = true;
                        //}
                        //if (chkAftergloCollect.Checked)
                        //{
                        //    bgloCollect = true;
                        //}
                        if (!worker.IsBusy)
                        {
                            worker.RunWorkerAsync();
                        }
                        else
                        {
                            worker.CancelAsync();
                        }
                        break;

                    case "tbpgClaimsQueue":
                        pnlProgressClaimIndication.BringToFront();                                              

                        sInsCmpny = "";
                        sActonCode = "";

                        bIsFutureItemAllow = false;
                        if (cmbInsBusinessCenter.Items.Count > 0 && pnlInsBusinessCenter.Visible == true)
                        {
                            nInsBusinessId = Convert.ToInt64(cmbInsBusinessCenter.SelectedValue);
                        }
                        else
                        {
                            nInsBusinessId = 0;
                        }

                        if (cmbInsuranceCompany.Items.Count > 0)
                        {
                            for (int cntrIns = 0; cntrIns <= cmbInsuranceCompany.Items.Count - 1; cntrIns++)
                            {
                                if (cmbInsuranceCompany.SelectedIndex >= 0)
                                {
                                    if (sInsCmpny == string.Empty)
                                        sInsCmpny = (Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cntrIns])["ID"]));
                                    else
                                        sInsCmpny = sInsCmpny + "," + (Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cntrIns])["ID"]));
                                }
                            }
                        }
                        if (cmbClaimFollowupAction.Items.Count > 0)
                        {
                            sActonCode = Convert.ToString(cmbClaimFollowupAction.SelectedValue).Trim();
                        }



                        if (chkInsClmIncludeFutureDtl.Checked)
                        {
                            bIsFutureItemAllow = true;
                        }

                        bBeforegloCollect = chkBeforegloCollect.Checked;

                        bgloCollect = chkAftergloCollect.Checked;

                        if (!workerClaimIns.IsBusy)
                        {
                            workerClaimIns.RunWorkerAsync();
                        }
                        else
                        {
                            workerClaimIns.CancelAsync();
                        }
                        break;
                    //case "tbpgBadDebtAcctQueue":

                    //    pnlBadDebtProgressIndication.BringToFront();

                    //    if (cmbBadDebtScheduleAction.Items.Count > 0)
                    //    {
                    //        filterBadDebtQueueByAction = Convert.ToString(cmbBadDebtScheduleAction.SelectedValue).Trim();
                    //    }
                    //    if (cmbBadDebtBusinessCenter.Items.Count > 0 && pnlBadDebtBusinessCenter.Visible == true)
                    //    {
                    //        filterBadDebtQueueByBusinessCenterId = Convert.ToInt64(cmbBadDebtBusinessCenter.SelectedValue);
                    //    }
                    //    else
                    //    {
                    //        filterBadDebtQueueByBusinessCenterId = 0;
                    //    }

                    //    if (chkBadDebtAccIncludeFutureDetail.Checked)
                    //    {
                    //        filterBadDebtQueueIncludesFutureFollowup = true;
                    //    }

                    //    if (!workerBadDebtAccounts.IsBusy)
                    //    {
                    //        workerBadDebtAccounts.RunWorkerAsync();
                    //    }
                    //    else
                    //    {
                    //        workerBadDebtAccounts.CancelAsync();
                    //    }
                    //    break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnBrowseInsurance_Click(object sender, EventArgs e)
        {
            try
            {
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch
                        {
                        }
                       

                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloListControl.gloListControlType.InsuranceCompany, true, this.Width);
                oListControl.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
                oListControl.ControlHeader = "Insurance Company";
                _CurrentControlType = gloListControl.gloListControlType.InsuranceCompany;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbInsuranceCompany.DataSource != null)
                {
                    for (int i = 0; i < cmbInsuranceCompany.Items.Count; i++)
                    {
                        cmbInsuranceCompany.SelectedIndex = i;
                        cmbInsuranceCompany.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsuranceCompany.SelectedValue), cmbInsuranceCompany.SelectedText);
                    }

                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();

            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void btnClearInsurance_Click(object sender, EventArgs e)
        {
            try
            {
               // cmbInsuranceCompany.Items.Clear();
                cmbInsuranceCompany.DataSource = null;
                cmbInsuranceCompany.Items.Clear();
                cmbInsuranceCompany.Refresh();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void chkBatchTemplate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkBatchTemplate.Checked == true)
                {
                    c1PALogView.Cols[0].Visible = true;
                    c1PALogView.SetCellCheck(0, 0, CheckEnum.Checked);
                    this.Cursor = Cursors.WaitCursor;
                    for (int i = 1; i <= c1PALogView.Rows.Count - 1; i++)
                    {
                        c1PALogView.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    }
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    c1PALogView.Cols[0].Visible = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbBadDebtScheduleAction_MouseEnter(object sender, EventArgs e)
        {
            //try
            //{
            //    combo = cmbBadDebtScheduleAction;
            //    if (cmbBadDebtScheduleAction.SelectedItem != null)
            //    {
            //        if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBadDebtScheduleAction.Items[cmbBadDebtScheduleAction.SelectedIndex])["sFollowUpAction"]), cmbBadDebtScheduleAction) >= cmbBadDebtScheduleAction.DropDownWidth - 20)
            //        {
            //            this.toolTip1.SetToolTip(cmbBadDebtScheduleAction, Convert.ToString(((DataRowView)cmbBadDebtScheduleAction.Items[cmbBadDebtScheduleAction.SelectedIndex])["sFollowUpAction"]));
            //        }
            //        else
            //        {
            //            this.toolTip1.SetToolTip(cmbBadDebtScheduleAction, "");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}
        }

        private void cmbBadDebtScheduleAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    combo = cmbBadDebtScheduleAction;
            //    if (cmbBadDebtScheduleAction.SelectedItem != null)
            //    {
            //        if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBadDebtScheduleAction.Items[cmbBadDebtScheduleAction.SelectedIndex])["sFollowUpAction"]), cmbBadDebtScheduleAction) >= cmbBadDebtScheduleAction.DropDownWidth - 20)
            //        {
            //            this.toolTip1.SetToolTip(cmbBadDebtScheduleAction, Convert.ToString(((DataRowView)cmbBadDebtScheduleAction.Items[cmbBadDebtScheduleAction.SelectedIndex])["sFollowUpAction"]));
            //        }
            //        else
            //        {
            //            this.toolTip1.SetToolTip(cmbBadDebtScheduleAction, "");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}
        }

        #endregion

        #region "Drill Down on Grid"

        private void PAScheduleDrilldown()
        {
            frmSetupFollowupDateAction objfrm = null;
            try
            {
                if (c1PALogView.RowSel > 0 && c1PALogView.ColSel >= 0)
                {
                    Int64 nScheduleID = 0;
                    Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccScheduleID"].Index)), out nScheduleID);

                    Int64 PatientID = 0;
                    Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)), out PatientID);

                    Int64 PAccountID = 0;
                    Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)), out PAccountID);

                    Int64 AccountPatientID = 0;
                    Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)), out AccountPatientID);

                    objfrm = new frmSetupFollowupDateAction(CollectionEnums.FollowUpType.PatientAccount, PatientID, PAccountID, AccountPatientID)
                    {
                        ScheduleID = nScheduleID
                    };
                    objfrm.ShowDialog(this);
                    objfrm.Dispose();
                    LoadPatientAccountQueue();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (objfrm != null) { objfrm.Dispose(); }
            }
        }
        private void PAScheduleDrilldown_Multiple(PatientDetails oPatientDetails, Int64 nAccScheduledID)
        {
            frmSetupFollowupDateAction objfrm = null;
            try
            {
                
                if (c1PALogView.Rows.Count>1)
                {
                    objfrm = new frmSetupFollowupDateAction(CollectionEnums.FollowUpType.PatientAccount, oPatientDetails, nAccScheduledID);
                    objfrm.bIsMultipleSelect = true;
                    objfrm.ShowDialog(this);
                    objfrm.Dispose();
                    LoadPatientAccountQueue();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (objfrm != null) { objfrm.Dispose(); }
            }
        }
        private void PAScheduleTemplateDrilldown()
        {
            frmSetupFollowupDateActionTemplate objfrm = null;
            try
            {
                if (c1PALogView.RowSel > 0 && c1PALogView.ColSel >= 0)
                {
                    Int64 nScheduleID = 0;
                    Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccScheduleID"].Index)), out nScheduleID);

                    Int64 PatientID = 0;
                    Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)), out PatientID);

                    Int64 PAccountID = 0;
                    Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)), out PAccountID);

                    Int64 AccountPatientID = 0;
                    Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)), out AccountPatientID);

                    DateTime dtScheduleDate = DateTime.Now.Date;
                    DateTime.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["dtNextScheduledate"].Index)), out dtScheduleDate);

                    //if (dtScheduleDate <= CL_FollowUpCode.GetServerDate())
                    //{
                    objfrm = new frmSetupFollowupDateActionTemplate(CollectionEnums.FollowUpType.PatientAccount, PatientID, PAccountID, AccountPatientID)
                    {
                        ScheduleID = nScheduleID
                    };
                    objfrm.ShowDialog(this);
                    objfrm.Dispose();
                    LoadPatientAccountQueue();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Cannot Generate Template for future Scheduled date. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (objfrm != null) { objfrm.Dispose(); }
            }
        }

        //private void BadDebtScheduleDrilldown(PatientDetails oPatientDetails, bool isMultiSelected)
        //{
        //    frmSetupFollowupDateAction objfrm = null;
        //    try
        //    {

        //        if (c1BadDebtLogView.Rows.Count > 1)
        //        {
        //            if (isMultiSelected == true)
        //            {
        //                objfrm = new frmSetupFollowupDateAction(CollectionEnums.FollowUpType.BadDebt, oPatientDetails, oPatientDetails[0].ScheduleId);
        //                objfrm.bIsMultipleSelect = true;
        //            }
        //            else
        //            {
        //                objfrm = new frmSetupFollowupDateAction(CollectionEnums.FollowUpType.BadDebt, oPatientDetails, oPatientDetails[0].ScheduleId);
        //                objfrm.bIsMultipleSelect = true;
        //            }

        //            objfrm.ShowDialog(this);
        //            objfrm.Dispose();
        //            LoadBadDebtAccountQueue();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //    finally
        //    {
        //        if (objfrm != null) { objfrm.Dispose(); }
        //    }
        //}
        
        private void PAScheduleBatchTemplateDrilldown(PatientDetails oPatientDetails, Int64 nAcctScheduleID)
        {
            frmSetupFollowupDateActionTemplate objfrm = null;
            try
            {
                if (c1PALogView.Rows.Count > 1)
                {
                    objfrm = new frmSetupFollowupDateActionTemplate(CollectionEnums.FollowUpType.PatientAccount, oPatientDetails, nAcctScheduleID);
                    objfrm.bIsBatchGenerate = true;
                    objfrm.ShowDialog(this);
                    objfrm.Dispose();
                    objfrm = null;
                    LoadPatientAccountQueue();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (objfrm != null) { objfrm.Dispose(); }
            }
        }

        private void PAAccountDrilldown(CollectionEnums.FollowUpType currentFollowUpType = CollectionEnums.FollowUpType.PatientAccount)
        {
            gloAccountsV2.frmPatientFinancialViewV2 objfrm = null;
            Int64 selectedPatientId = 0;
            Int64 selectedAccountId = 0;

            try
            {
                if (currentFollowUpType == CollectionEnums.FollowUpType.PatientAccount)
                {
                    if (c1PALogView != null && c1PALogView.RowSel > 0 && c1PALogView.ColSel >= 0)
                    {
                        Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)), out selectedPatientId);
                        selectedAccountId = oPatientControl.PAccountID;
                    }
                }
                else if (currentFollowUpType == CollectionEnums.FollowUpType.BadDebt)
                {
                    if (c1BadDebtLogView != null && c1BadDebtLogView.RowSel > 0 && c1BadDebtLogView.ColSel >= 0)
                    {
                        Int64.TryParse(Convert.ToString(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index)), out selectedPatientId);
                        selectedAccountId = oPatientControlBadDebt.PAccountID;
                    }
                }

                if (selectedPatientId > 0 && selectedAccountId > 0)
                {
                    objfrm = new gloAccountsV2.frmPatientFinancialViewV2(selectedPatientId)
                    {

                    };
                    objfrm._nSelectAccountId = selectedAccountId;
                    objfrm.StartPosition = FormStartPosition.CenterScreen;
                    objfrm.WindowState = FormWindowState.Maximized;
                    objfrm.ShowDialog(this);
                    objfrm.Dispose();
                    objfrm.Dispose();
                    objfrm = null;

                    switch (currentFollowUpType)
                    {
                        case CollectionEnums.FollowUpType.Claim:
                            break;
                        case CollectionEnums.FollowUpType.PatientAccount:
                            LoadPatientAccountQueue();
                            break;
                        //case CollectionEnums.FollowUpType.BadDebt:
                        //    LoadBadDebtAccountQueue();
                        //    break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (objfrm != null) { objfrm.Dispose(); }
            }
        }

        private void LoadPatientPaymentScreen(CollectionEnums.FollowUpType currentFollowUpType)
        {
            Int64 selectedPatientId = 0;
            try
            {
                switch (currentFollowUpType)
                {
                    case CollectionEnums.FollowUpType.Claim:
                        break;
                    case CollectionEnums.FollowUpType.PatientAccount:
                        {
                            if (c1PALogView != null && c1PALogView.Rows.Count > 0 && c1PALogView.RowSel > 0)
                            {
                                Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)), out selectedPatientId);
                            }
                        }
                        break;
                    case CollectionEnums.FollowUpType.BadDebt:
                        {
                            if (c1BadDebtLogView != null && c1BadDebtLogView.Rows.Count > 0 && c1BadDebtLogView.RowSel > 0)
                            {
                                Int64.TryParse(Convert.ToString(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index)), out selectedPatientId);
                            }
                        }
                        break;
                    default:
                        break;
                }

                if (selectedPatientId > 0)
                {
                    gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(selectedPatientId, false, 0, 0, 0, 0, EOBPaymentSubType.Other);
                    frmPatientPaymentV2.StartPosition = FormStartPosition.CenterScreen;
                    frmPatientPaymentV2.WindowState = FormWindowState.Maximized;
                    frmPatientPaymentV2.ShowInTaskbar = false;
                    frmPatientPaymentV2.ShowDialog(this);
                    frmPatientPaymentV2.Dispose();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show("Error loading patient payment screen.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PAInsClmDrilldown()
        {
            gloAccountsV2.frmPatientFinancialViewV2 objfrm = null;
            try
            {
                if (c1InsClaimSchedule.RowSel > 0 && c1InsClaimSchedule.ColSel >= 0)
                {
                    Int64 PatientID = 0;
                    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index)), out PatientID);


                    objfrm = new gloAccountsV2.frmPatientFinancialViewV2(PatientID)
                    {

                    };

                    objfrm.StartPosition = FormStartPosition.CenterScreen;
                    objfrm.WindowState = FormWindowState.Maximized;
                    objfrm.ShowDialog(this);
                    objfrm.Dispose();
                    objfrm = null;
                    LoadInsuranceClaimQueue();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (objfrm != null) { objfrm.Dispose(); }
            }
        }

        private void PAPaymentPlanDrilldown()
        {
            frmSetupPaymentPlan objfrm = null;
            try
            {
                if (c1PALogView.RowSel > 0 && c1PALogView.ColSel >= 0)
                {
                    objfrm = new frmSetupPaymentPlan();
                    objfrm.nPAccountID = oPatientControl.PAccountID;
                    objfrm.nPatientAccountID = oPatientControl.AccountPatientID;
                    objfrm.nPatientID = oPatientControl.PatientID;
                    objfrm.ShowDialog(this);
                    objfrm.Dispose();
                    objfrm = null;
                    LoadPatientAccountQueue();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (objfrm != null) { objfrm.Dispose(); }
            }
        }

        private void InsClaimScheduleDrilldown()
        {
            frmSetupFollowupDateAction objfrm = null;
            try
            {
                if (c1InsClaimSchedule.RowSel > 0 && c1InsClaimSchedule.ColSel >= 0)
                {
                    Int64 nScheduleID = 0;
                    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextScheduleID"].Index)), out nScheduleID);

                    Int64 nTransactionID = 0;
                    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index)), out nTransactionID);

                    Int64 nMstTransactionID = 0;
                    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)), out nMstTransactionID);

                    objfrm = new frmSetupFollowupDateAction(CollectionEnums.FollowUpType.Claim, _PatientID, nPAccountID, nTransactionID, nMstTransactionID)
                    {
                        ScheduleID = nScheduleID
                    };                    

                    // TFL and DFL Changes
                    if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "")
                    {
                        objfrm.TFL_DFLDate = Convert.ToDateTime(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["Tfl_Dfl_Date"].Index)));
                        objfrm.flgTFL_DFL = "TFL";
                    }
                    else if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)) != "")
                    {
                        objfrm.TFL_DFLDate = Convert.ToDateTime(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["Tfl_Dfl_Date"].Index)));
                        objfrm.flgTFL_DFL = "DFL";
                    }
                    else
                    {
                        objfrm.flgTFL_DFL = "";
                    }

                    objfrm.ClaimNumber = Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["sClaimNo"].Index)); ;
                    objfrm.ShowClaimStatus = bIsShowClaimStatus;
                    objfrm.ShowDialog(this);
                    objfrm.Dispose();
                    objfrm = null;
                    LoadInsuranceClaimQueue();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (objfrm != null) { objfrm.Dispose(); }
            }
        }

        private void InsClaimScheduleDrilldown_Multiple(PatientDetails oPatientDetails, Int64 nAccScheduledID)
        {
            frmSetupFollowupDateAction objfrm = null;
            try
            {
                //if (c1InsClaimSchedule.RowSel > 0 && c1InsClaimSchedule.ColSel >= 0)
                //{
                //    Int64 nScheduleID = 0;
                //    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextScheduleID"].Index)), out nScheduleID);

                //    Int64 nTransactionID = 0;
                //    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index)), out nTransactionID);

                //    Int64 nMstTransactionID = 0;
                //    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)), out nMstTransactionID);

                //    objfrm = new frmSetupFollowupDateAction(CollectionEnums.FollowUpType.Claim, _PatientID, nPAccountID, nTransactionID, nMstTransactionID)
                //    {
                //        ScheduleID = nScheduleID
                //    };

                //    // TFL and DFL Changes
                //    if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "")
                //    {
                //        objfrm.TFL_DFLDate = Convert.ToDateTime(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["Tfl_Dfl_Date"].Index)));
                //        objfrm.flgTFL_DFL = "TFL";
                //    }
                //    else if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)) != "")
                //    {
                //        objfrm.TFL_DFLDate = Convert.ToDateTime(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["Tfl_Dfl_Date"].Index)));
                //        objfrm.flgTFL_DFL = "DFL";
                //    }
                //    else
                //    {
                //        objfrm.flgTFL_DFL = "";
                //    }

                //    objfrm.ShowDialog(this);
                //    objfrm.Dispose();
                //    objfrm = null;
                //    LoadInsuranceClaimQueue();
                //}
                if (c1InsClaimSchedule.Rows.Count>0)
                {
                    objfrm = new frmSetupFollowupDateAction(CollectionEnums.FollowUpType.Claim, oPatientDetails, nAccScheduledID);
                    objfrm.bIsMultipleSelect = true;
                    objfrm.ShowDialog(this);
                    objfrm.Dispose();
                    objfrm = null;
                    LoadInsuranceClaimQueue();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (objfrm != null) { objfrm.Dispose(); }
            }
        }
        private void InsClaimScheduleTemplateDrilldown()
        {
            frmSetupFollowupDateActionTemplate objfrm = null;
            try
            {
                if (c1InsClaimSchedule.RowSel > 0 && c1InsClaimSchedule.ColSel >= 0)
                {
                    Int64 nScheduleID = 0;
                    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextScheduleID"].Index)), out nScheduleID);

                    Int64 nTransactionID = 0;
                    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index)), out nTransactionID);

                    Int64 nMstTransactionID = 0;
                    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)), out nMstTransactionID);

                    Int64 nPatientID = 0;
                    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index)), out nPatientID);

                    Int64 nAccountID = 0;
                    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index)), out nAccountID);

                    DateTime dtScheduleDate = DateTime.Now.Date;
                    DateTime.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["dtClaimFollowUpDate"].Index)), out dtScheduleDate);

                    //if (dtScheduleDate<= CL_FollowUpCode.GetServerDate())
                    //{
                    objfrm = new frmSetupFollowupDateActionTemplate(CollectionEnums.FollowUpType.Claim, nPatientID, nAccountID, nTransactionID, nMstTransactionID)
                    {
                        ScheduleID = nScheduleID
                    };
                    objfrm.ShowDialog(this);
                    objfrm.Dispose();
                    objfrm = null;
                    LoadInsuranceClaimQueue();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Cannot Generate Template for future Scheduled date. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (objfrm != null) { objfrm.Dispose(); }
            }
        }

        private void InsClaimScheduleTemplateDrilldown_Batch(PatientDetails oPatientDetails, Int64 nAcctScheduledID)
        {
            frmSetupFollowupDateActionTemplate objfrm = null;
            try
            {
                //if (c1InsClaimSchedule.RowSel > 0 && c1InsClaimSchedule.ColSel >= 0)
                //{
                //    Int64 nScheduleID = 0;
                //    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextScheduleID"].Index)), out nScheduleID);

                //    Int64 nTransactionID = 0;
                //    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index)), out nTransactionID);

                //    Int64 nMstTransactionID = 0;
                //    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)), out nMstTransactionID);

                //    Int64 nPatientID = 0;
                //    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index)), out nPatientID);

                //    Int64 nAccountID = 0;
                //    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index)), out nAccountID);

                //    DateTime dtScheduleDate = DateTime.Now.Date;
                //    DateTime.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["dtClaimFollowUpDate"].Index)), out dtScheduleDate);

                //    //if (dtScheduleDate<= CL_FollowUpCode.GetServerDate())
                //    //{
                //    objfrm = new frmSetupFollowupDateActionTemplate(CollectionEnums.FollowUpType.Claim, nPatientID, nAccountID, nTransactionID, nMstTransactionID)
                //    {
                //        ScheduleID = nScheduleID
                //    };
                //    objfrm.ShowDialog(this);
                //    objfrm.Dispose();
                //    objfrm = null;
                //    LoadInsuranceClaimQueue();
                //    //}
                //    //else
                //    //{
                //    //    MessageBox.Show("Cannot Generate Template for future Scheduled date. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    //}
                //}
                if (c1InsClaimSchedule.Rows.Count > 1)
                {
                    objfrm = new frmSetupFollowupDateActionTemplate(CollectionEnums.FollowUpType.Claim, oPatientDetails, nAcctScheduledID);
                    objfrm.bIsBatchGenerate = true;
                    objfrm.ShowDialog(this);
                    objfrm.Dispose();
                    objfrm = null;
                    LoadInsuranceClaimQueue();

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (objfrm != null) { objfrm.Dispose(); }
            }
        }

        //7022 Items: Remove claim from queue manually
        //Description: Function added to selected account follow-up from patient account follow-up queue.
        private void PAAccountScheduleDelete()
        {
            try
            {
                if (c1PALogView.RowSel > 0 && c1PALogView.ColSel >= 0)
                {
                    Int64 PAccountID = 0;
                    Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)), out PAccountID);

                    DialogResult res = MessageBox.Show("Warning: Account will be removed from the follow-up queue." + Environment.NewLine + "Continue?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (res == DialogResult.OK)
                    {
                        if (CL_FollowUpCode.DeleteAccountFollowUp(PAccountID))
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.Delete, "Account Follow-up deleted.", 0, PAccountID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM,true);
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.Delete, "Account Follow-up not delete.", 0, PAccountID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                        }
                    }
                    LoadPatientAccountQueue();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.Delete, "Exception: Account Follow-up delete " + ex.Message + " .", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        //7022 Items: Remove claim from queue manually
        //Description: Function added to selected Claim follow-up from Insurance follow-up queue.
        private void InsClaimScheduleDelete()
        {
            try
            {
                if (c1InsClaimSchedule.RowSel > 0 && c1InsClaimSchedule.ColSel >= 0)
                {
                    Int64 nTransactionID = 0;
                    Int64.TryParse(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index)), out nTransactionID);
                    DialogResult res = System.Windows.Forms.DialogResult.No;

                    if (_dBalance > 0)
                        res = MessageBox.Show("Warning:" + Environment.NewLine + "You are about to remove a Claim from Follow-Up that still has a remaining balance." + Environment.NewLine + Environment.NewLine + "Normally, there is no need to perform this function and you should not proceed." + Environment.NewLine + Environment.NewLine + "Claim Balances that are removed from Follow-Up are more difficult to track and may get lost. " + Environment.NewLine + Environment.NewLine + "Instead, you should take other steps, like rebill the claim, move it to patient responsibility, write off the balance, or schedule it for an additional follow-up action." + Environment.NewLine + Environment.NewLine + "Remove this Claim from Follow-Up?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    else
                        res = DialogResult.Yes;


                    if (res == DialogResult.Yes)
                    {
                        CL_FollowUpCode oCollection = new CL_FollowUpCode();

                        if (oCollection.DeleteFollowUpSchedule(nTransactionID))
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.Delete, "Claim Follow-up deleted.", 0, nTransactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

                            if (_dBalance > 0)
                                InsertNotesForDeletedFollowUpClaim();
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.Delete, "Claim Follow-up not delete.", 0, nTransactionID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                        }
                        oCollection.Dispose();
                        LoadInsuranceClaimQueue();
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.Delete, "Exception: Claim Follow-up delete " + ex.Message + " .", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void InsertNotesForDeletedFollowUpClaim()
        {
            global::gloBilling.Common.GeneralNote oNote = null;
            global::gloBilling.Common.GeneralNotes oNotes = null;
            try
            {
                Int64 nTransactionID = 0;
                Int64.TryParse(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index).ToString(), out nTransactionID);
                if (nTransactionID > 0)
                {
                    oNote = new Common.GeneralNote();
                    oNotes = new Common.GeneralNotes();
                    oNote.TransactionID = nTransactionID;
                    oNote.TransactionLineId = 0;
                    oNote.TransactionDetailID = 0;
                    oNote.NoteType = NoteType.Claim_Note;
                    oNote.NoteID = 0;
                    oNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                    oNote.UserID = gloGlobal.gloPMGlobal.UserID;
                    oNote.StatementNoteDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                    oNote.NoteDescription = "System Notice: This claim (" + lblClaimNo.Text + ") was manually removed from Follow-up tracking while it still had a balance of $ " + _dBalance;
                    oNote.ClinicID = gloGlobal.gloPMGlobal.ClinicID; ;
                    oNotes = new global::gloBilling.Common.GeneralNotes();
                    oNotes.Add(oNote);

                    gloCharges.SaveClaimNotes(oNotes);
                }
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                oNote = null;
                oNotes = null;
            }

        }

        private void InsertNotesForDeletedFollowUpClaim_Multiple(string sTransactionIDs)
        {
            global::gloBilling.Common.GeneralNote oNote = null;
            global::gloBilling.Common.GeneralNotes oNotes = null;
            try
            {
                if (sTransactionIDs !="")
                {
                    oNote = new Common.GeneralNote();
                    oNotes = new Common.GeneralNotes();
                    oNote.TransactionID = 0;
                    oNote.TransactionLineId = 0;
                    oNote.TransactionDetailID = 0;
                    oNote.NoteType = NoteType.Claim_Note;
                    oNote.NoteID = 0;
                    oNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                    oNote.UserID = gloGlobal.gloPMGlobal.UserID;
                    oNote.StatementNoteDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                    oNote.NoteDescription = "";
                    oNote.ClinicID = gloGlobal.gloPMGlobal.ClinicID; ;
                    oNotes = new global::gloBilling.Common.GeneralNotes();
                    oNotes.Add(oNote);

                    gloCharges.SaveClaimNotes_Delete_Multiple(oNotes,sTransactionIDs);
                }
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                oNote = null;
                oNotes = null;
            }

        }
        #endregion

        #region "Tool Strip Events "

        #region "DashBoard"

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            FillRevenueSummary();
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_FollowUp_Click(object sender, EventArgs e)
        {
            //frmAutoFollowupUtility ofrmAutoUtility = new frmAutoFollowupUtility();
            //ofrmAutoUtility.ShowDialog(this);
        }

        #endregion

        #region "Patient Account Follow Up Queue"

        private void tsb_PatAcctRefresh_Click(object sender, EventArgs e)
        {
            nActSortedColumn = -1;
            if (gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_FollowupQueue") && pnlBusinessCenter.Visible == false)
            {

                if (cmbBusinessCenter.DataSource == null)
                {
                    cmbBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
                    cmbBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
                    FillBusinessCenter(cmbBusinessCenter);
                }
                pnlBusinessCenter.Visible = true;
                cmbBusinessCenter.SelectedValue = gloGlobal.gloPMGlobal.GetDefaultBusinessCenterForUser(gloGlobal.gloPMGlobal.UserID);
            }
            else if (!gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_FollowupQueue"))
                pnlBusinessCenter.Visible = false;
            LoadPatientAccountQueue();
            if (cmbAcctFollowUpAction.SelectedItem != null)
            {

                if (cmbAcctFollowUpAction.SelectedValue.ToString() != "" && cmbAcctFollowUpAction.SelectedValue.ToString() == getExternalCollectionFUAction() && gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                {
                    tspTransferClaimBalance.Visible = true;
                }
                else
                {
                    tspTransferClaimBalance.Visible = false;
                }
            }
            else
            {
                tspTransferClaimBalance.Visible = false;
            }
        }

        private void tsb_PatAcctPaymentPlan_Click(object sender, EventArgs e)
        {
            PAPaymentPlanDrilldown();
        }

        private void tsb_PatAcctAccount_Click(object sender, EventArgs e)
        {

            PAAccountDrilldown();

        }

        private void tsb_PatAcctFollowup_Click(object sender, EventArgs e)
        {
            try
            {
                //PAScheduleDrilldown();
                if (c1PALogView.Rows.Count > 1)
                {
                    int nSelectCount = 0;
                    nSelectCount = c1PALogView.FindRow("True", 1, 0, true);
                    
                    if (nSelectCount <=0)
                    {
                        PAScheduleDrilldown();
                    }
                    else
                    {
                        Int64 nScheduleID = 0;
                        PatientDetails oAllSelectedPatientDetails = new PatientDetails();
                        for (int i = 1; i < c1PALogView.Rows.Count; i++)
                        {
                            PatientDetail oSelectedPatientDetail = new PatientDetail();
                            oSelectedPatientDetail.PatientID = 0;
                            oSelectedPatientDetail.PatientAccountID = 0;
                            oSelectedPatientDetail.TransactionID = 0;
                            oSelectedPatientDetail.MstTransactionID = 0;
                            oSelectedPatientDetail.ContactID = 0;
                            oSelectedPatientDetail.InsuranceID = 0;
                            if (c1PALogView.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                if (nScheduleID == 0)
                                {
                                    nScheduleID = Convert.ToInt64(c1PALogView.GetData(c1PALogView.Rows[i].Index, c1PALogView.Cols["nAccScheduleID"].Index));
                                }
                                oSelectedPatientDetail.PatientID = Convert.ToInt64(c1PALogView.GetData(c1PALogView.Rows[i].Index, c1PALogView.Cols["nPatientID"].Index));
                                oSelectedPatientDetail.PatientAccountID = Convert.ToInt64(c1PALogView.GetData(c1PALogView.Rows[i].Index, c1PALogView.Cols["nAccountID"].Index));
                                oSelectedPatientDetail.TransactionID = 0;
                                oSelectedPatientDetail.MstTransactionID = 0;
                                DateTime dtLogTimeStamp, dtFollowUpTimeStamp;
                                GetLogAndScheduleDateTimeStamp(CollectionEnums.FollowUpType.PatientAccount, oSelectedPatientDetail.PatientAccountID, out dtLogTimeStamp, out dtFollowUpTimeStamp);
                                oSelectedPatientDetail.dtLogTimeStamp = dtLogTimeStamp;
                                oSelectedPatientDetail.dtFollowUpTimeStamp = dtFollowUpTimeStamp;
                                oSelectedPatientDetail.ContactID = 0;
                                oSelectedPatientDetail.InsuranceID = 0;
                                oSelectedPatientDetail.dtTFL_DFLTimeStamp = DateTime.MinValue;
                                oSelectedPatientDetail.TFL_DFL = "";
                                oAllSelectedPatientDetails.Add(oSelectedPatientDetail);
                            }
                            oSelectedPatientDetail.Dispose();
                            oSelectedPatientDetail = null;
                        }
                        if (oAllSelectedPatientDetails != null && oAllSelectedPatientDetails.Count > 0)
                        {
                            //call method for batch action
                            PAScheduleDrilldown_Multiple(oAllSelectedPatientDetails, nScheduleID);
                        }
                        if (oAllSelectedPatientDetails != null)
                        {
                            oAllSelectedPatientDetails.Dispose();
                            oAllSelectedPatientDetails = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
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
                if (dtContInsID != null){ dtContInsID.Dispose(); dtContInsID = null; }
            }

        }

        private void GetLogAndScheduleDateTimeStamp(CollectionEnums.FollowUpType FollowUpActionType, Int64 nID, out DateTime dtLogTimeStamp, out DateTime dtFollowUpTimeStamp)
        {
            DateTime AccountFollowUpTimeStamp = DateTime.MinValue, AccountLogTimeStamp = DateTime.MinValue;
            DateTime ClaimFollowUpTimeStamp = DateTime.MinValue, ClaimLogTimeStamp = DateTime.MinValue;
            dtLogTimeStamp = DateTime.MinValue;
            dtFollowUpTimeStamp = DateTime.MinValue;
            try
            {
                if (FollowUpActionType == CollectionEnums.FollowUpType.Claim)
                {
                    DataTable dtClaimFollowUpTimeStamp = null;
                    dtClaimFollowUpTimeStamp = CL_FollowUpCode.GetClaimFollowUpTimeStamp(nID);
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
                    dtClaimLogTimeStamp = CL_FollowUpCode.GetClaimFollowUpLogTimeStamp(nID);
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

                    dtLogTimeStamp = ClaimLogTimeStamp;
                    dtFollowUpTimeStamp = ClaimFollowUpTimeStamp;
                }
                else if (FollowUpActionType == CollectionEnums.FollowUpType.PatientAccount)
                {
                    DataTable dtAccountFollowUpTimeStamp = null;
                    dtAccountFollowUpTimeStamp = CL_FollowUpCode.GetAccountFollowUpTimeStamp(nID);
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
                    dtAccountLogTimeStamp = CL_FollowUpCode.GetAccountFollowUpLogTimeStamp(nID);
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

                    dtLogTimeStamp = AccountLogTimeStamp;
                    dtFollowUpTimeStamp = AccountFollowUpTimeStamp;
                }
                else if (FollowUpActionType == CollectionEnums.FollowUpType.BadDebt)
                {
                    DataTable dtAccountFollowUpTimeStamp = null;
                    dtAccountFollowUpTimeStamp = CL_FollowUpCode.GetBadDebtAccountFollowUpTimeStamp(nID);
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
                    dtAccountLogTimeStamp = CL_FollowUpCode.GetBadDebtAccountFollowUpLogTimeStamp(nID);
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

                    dtLogTimeStamp = AccountLogTimeStamp;
                    dtFollowUpTimeStamp = AccountFollowUpTimeStamp;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        

        private void tsb_PatAcctClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void tsbAcctGenerateTemp_Click(object sender, EventArgs e)
        {
            try 
            {
                DialogResult _dlg = System.Windows.Forms.DialogResult.None;
                string sMessage = string.Empty;
                if (c1PALogView.Rows.Count > 1 && c1PALogView.RowSel > 0)
                {
                    int nSelectCount = 0;
                    nSelectCount = c1PALogView.FindRow("True", 1, 0, true);  
                    if (nSelectCount <= 0)
                    {
                        PAScheduleTemplateDrilldown();
                    }
                    else
                    {
                       DataTable _dtDefTemplate = new DataTable();
                                _dtDefTemplate = CL_FollowUpCode.GetDefaultAssociateTemplate(Convert.ToString(cmbAcctFollowUpAction.SelectedValue), CollectionEnums.FollowUpType.PatientAccount);
                           
                        if (_dtDefTemplate != null && _dtDefTemplate.Rows.Count <=0)
                             {
                                   _dlg = MessageBox.Show("Selected schedule action \""+ Convert.ToString(cmbAcctFollowUpAction.Text) + "\" \ndoes not have associated template.\n\nContinue ?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                                    if (_dlg == DialogResult.Cancel)
                                     {
                                         return;
                                     }      
  
                              }
                              if (_dtDefTemplate == null)
                              {
                                  _dlg = MessageBox.Show("Selected schedule action \"" + Convert.ToString(cmbAcctFollowUpAction.Text) + "\" \ndoes not have associated template.\n\nContinue ?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                                  if (_dlg == DialogResult.Cancel)
                                  {
                                      return;
                                  }

                              }
                        Int64 _nscheduleID = 0;
                        PatientDetails oAllSelectedPatientDetails = new PatientDetails();
                        for (int i = 1; i < c1PALogView.Rows.Count; i++)
                        {
                            PatientDetail SelectedPatientDetail = new PatientDetail();
                            SelectedPatientDetail.PatientID = 0;
                            SelectedPatientDetail.PatientAccountID = 0;
                            if (c1PALogView.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                //DateTime dtScheduleDate = DateTime.Now.Date;
                                //DateTime.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["dtNextScheduledate"].Index)), out dtScheduleDate);

                                //if (dtScheduleDate > CL_FollowUpCode.GetServerDate())
                                //{
                                //    MessageBox.Show("Cannot Generate Template for future Schedule date. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    c1PALogView.RowSel = c1PALogView.Rows[i].Index;
                                //    return;
                                //}
                                if (_nscheduleID == 0)
                                {
                                    _nscheduleID = Convert.ToInt64(c1PALogView.GetData(c1PALogView.Rows[i].Index, c1PALogView.Cols["nAccScheduleID"].Index));
                                }
                                SelectedPatientDetail.PatientID = Convert.ToInt64(c1PALogView.GetData(c1PALogView.Rows[i].Index, c1PALogView.Cols["nPatientID"].Index));
                                SelectedPatientDetail.PatientAccountID = Convert.ToInt64(c1PALogView.GetData(c1PALogView.Rows[i].Index, c1PALogView.Cols["nAccountID"].Index));
                                oAllSelectedPatientDetails.Add(SelectedPatientDetail);
                            }
                            SelectedPatientDetail.Dispose();
                            SelectedPatientDetail = null;
                        }
                        if (oAllSelectedPatientDetails != null && oAllSelectedPatientDetails.Count > 0)
                        {
                            PAScheduleBatchTemplateDrilldown(oAllSelectedPatientDetails, _nscheduleID);
                        }
                        if (oAllSelectedPatientDetails != null)
                        {
                            oAllSelectedPatientDetails.Dispose();
                            oAllSelectedPatientDetails = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tsb_PatAcctReport_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                frmSSRSViewer frmSSRS = new frmSSRSViewer();
                frmSSRS.Conn = gloGlobal.gloPMGlobal.DatabaseConnectionString;
                frmSSRS.reportName = "rptAccountFollow-up";
                frmSSRS.reportTitle = "Account Follow-up Report";
                frmSSRS.IsgloStreamReport = true;
                Cursor.Current = Cursors.Default;
                frmSSRS.ShowDialog(this);
                frmSSRS.Dispose();
                frmSSRS = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //7022 Items: Remove claim from queue manually
        //Description: New button for Remove patient account follow-up (named as "Clear follow-up") is added with click event.
        private void tsb_PatAcctDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int nSelectedCount = 0;
                nSelectedCount = c1PALogView.FindRow("True", 1, 0, true);
                if (nSelectedCount <= 0)
                {
                    PAAccountScheduleDelete();
                }
                else
                {
                    //if (c1PALogView.RowSel > 0 && c1PALogView.ColSel >= 0)
                    //{
                        string sAccountIDs = string.Empty;

                        DataTable dt = (DataTable)c1PALogView.DataSource;
                        sAccountIDs = dt.AsEnumerable()
                                              .Where(r => Convert.ToBoolean(r["bIsTemplate"]) == true)
                                              .Select(row => row["nAccountID"].ToString())
                                              .Aggregate((s1, s2) => String.Concat(s1, "," + s2));

                        if (sAccountIDs != "")
                        {
                            DeleteMultipleFollowUpScheduled(sAccountIDs, CollectionEnums.FollowUpType.PatientAccount);
                        }
                        LoadPatientAccountQueue();
                    //}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.Delete, "Exception: Claim Follow-up delete " + ex.Message + " .", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                throw;
            }
            
        }

        #endregion

        #region "Insurance Claim Follow Up Queue"

        private void tsb_ClaimRefresh_Click(object sender, EventArgs e)
        {
            nInsSortedColumn = -1;
            if (gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_FollowupQueue") && pnlInsBusinessCenter.Visible == false)
            {
                if (cmbBusinessCenter.DataSource == null)
                {
                    cmbInsBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
                    cmbInsBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
                    FillBusinessCenter(cmbInsBusinessCenter);
                }
                pnlInsBusinessCenter.Visible = true;
                cmbInsBusinessCenter.SelectedValue = gloGlobal.gloPMGlobal.GetDefaultBusinessCenterForUser(gloGlobal.gloPMGlobal.UserID);
            }
            else if (!gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_FollowupQueue"))
            {
                pnlInsBusinessCenter.Visible = false;
            }
            LoadInsuranceClaimQueue();
        }

        private void tsb_ClaimModifyChrg_Click(object sender, EventArgs e)
        {
            Boolean _IsModified = false;
            try
            {
                if (c1InsClaimSchedule.Rows.Count > 1)
                {
                    _IsModified = ModifyCharge();
                    if (_IsModified)
                    {
                        LoadInsuranceClaimQueue();
                    }
                }
                else
                {
                    MessageBox.Show("Claim not available. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void tsb_ClaimHistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1InsClaimSchedule.Rows.Count > 1)
                {
                    showClaimHistory();
                    LoadInsuranceClaimQueue();
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_ClaimPatAcct_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1InsClaimSchedule.Rows.Count > 1)
                {
                    PAInsClmDrilldown();
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_ClaimFollowup_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1InsClaimSchedule.Rows.Count > 1)
                {
                    //InsClaimScheduleDrilldown();
                    //if (c1InsClaimSchedule.RowSel>0)
                    {
                        int nSelectedCount = 0;
                        nSelectedCount = c1InsClaimSchedule.FindRow("True", 1, 0, true);

                        if (nSelectedCount <= 0)
                        {
                            InsClaimScheduleDrilldown();
                        }
                        else
                        {
                            Int64 nScheduleID = 0;
                            PatientDetails oAllSelectedPatientDetails = new PatientDetails();
                            for (int i = 1; i < c1InsClaimSchedule.Rows.Count; i++)
                            {
                                PatientDetail oSelectedPatientDetail = new PatientDetail();
                                oSelectedPatientDetail.PatientID = 0;
                                oSelectedPatientDetail.PatientAccountID = 0;
                                oSelectedPatientDetail.TransactionID = 0;
                                oSelectedPatientDetail.MstTransactionID = 0;
                                oSelectedPatientDetail.ContactID = 0;
                                oSelectedPatientDetail.InsuranceID = 0;
                                if (c1InsClaimSchedule.GetCellCheck(i,0)==C1.Win.C1FlexGrid.CheckEnum.Checked)
                                {
                                    if (nScheduleID==0)
                                    {
                                        nScheduleID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["nNextScheduleID"].Index));
                                    }
                                    oSelectedPatientDetail.PatientID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["nPatientID"].Index));
                                    oSelectedPatientDetail.PatientAccountID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["nAccountID"].Index));
                                    oSelectedPatientDetail.TransactionID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["nTransactionID"].Index)); ;
                                    oSelectedPatientDetail.MstTransactionID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)); ;
                                    DateTime dtLogTimeStamp, dtFollowUpTimeStamp;
                                    GetLogAndScheduleDateTimeStamp(CollectionEnums.FollowUpType.Claim, oSelectedPatientDetail.TransactionID, out dtLogTimeStamp, out dtFollowUpTimeStamp);
                                    oSelectedPatientDetail.dtLogTimeStamp = dtLogTimeStamp;
                                    oSelectedPatientDetail.dtFollowUpTimeStamp = dtFollowUpTimeStamp;
                                    Int64 nContactID, nInsuranceID;
                                    GetContactInsuranceID(oSelectedPatientDetail.TransactionID, out nContactID, out nInsuranceID);
                                    oSelectedPatientDetail.ContactID = nContactID;
                                    oSelectedPatientDetail.InsuranceID = nInsuranceID;
                                    DateTime dtTFL_DFLTimeStamp; string sTFL_DFL;
                                    if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "")
                                    {
                                        dtTFL_DFLTimeStamp = Convert.ToDateTime(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["Tfl_Dfl_Date"].Index)));
                                        sTFL_DFL = "TFL";
                                    }
                                    else if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["DFLDays"].Index)) != "")
                                    {
                                        dtTFL_DFLTimeStamp = Convert.ToDateTime(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["Tfl_Dfl_Date"].Index)));
                                        sTFL_DFL = "DFL";
                                    }
                                    else
                                    {
                                        dtTFL_DFLTimeStamp = DateTime.MinValue;
                                        sTFL_DFL = "";
                                    }
                                    oSelectedPatientDetail.dtTFL_DFLTimeStamp = dtTFL_DFLTimeStamp;
                                    oSelectedPatientDetail.TFL_DFL = sTFL_DFL;
                                    oAllSelectedPatientDetails.Add(oSelectedPatientDetail);
                                }
                                oSelectedPatientDetail.Dispose();
                                oSelectedPatientDetail = null;
                            }
                            if (oAllSelectedPatientDetails!=null&&oAllSelectedPatientDetails.Count>0)
                            {
                                InsClaimScheduleDrilldown_Multiple(oAllSelectedPatientDetails,nScheduleID);
                            }
                            if (oAllSelectedPatientDetails!=null)
                            {
                                oAllSelectedPatientDetails.Dispose();
                                oAllSelectedPatientDetails = null;
                            }
                        }
                    }
                    //LoadInsuranceClaimQueue();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void GetTFL_DFL_Details(long nTransactionID, out DateTime dtTFL_DFLTimeStamp, out string sTFL_DFL)
        {
            dtTFL_DFLTimeStamp = DateTime.MinValue; sTFL_DFL = "";
            DataTable dtTFL_DFL = null;
            try
            {
                dtTFL_DFL = CL_FollowUpCode.getClaimTFL_DFL_Details(nTransactionID);
                if (dtTFL_DFL != null && dtTFL_DFL.Rows.Count > 0)
                {
                    if (Convert.ToString(dtTFL_DFL.Rows[0]["TFLDays"]) != "")
                    {
                        dtTFL_DFLTimeStamp = Convert.ToDateTime(dtTFL_DFL.Rows[0]["TFL_DFL_DATE"]);
                        sTFL_DFL = "TFL";
                    }
                    else if (Convert.ToString(dtTFL_DFL.Rows[0]["DFLDays"]) != "")
                    {
                        dtTFL_DFLTimeStamp = Convert.ToDateTime(dtTFL_DFL.Rows[0]["TFL_DFL_DATE"]);
                        sTFL_DFL = "DFL";
                    }
                    else
                    {
                        sTFL_DFL = "";
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (dtTFL_DFL != null) { dtTFL_DFL.Dispose(); dtTFL_DFL = null; }
            }
        }

        private void tsb_ClaimResend_Click(object sender, EventArgs e)
        {
            string nTrackTransactionID = string.Empty;
            string nBillingTransactionID = string.Empty;
            string _InsTransferCloseDate = string.Empty;
            CL_FollowUpCode oCollection = null;
            try
            {
                Int64 _transactionId = 0;
                Int64 _MastertrnId = 0;
                Int64 _ContactID = 0;
                Int64 _InsuranceID = 0;
                TransactionStatus Transaction_Status = TransactionStatus.None;
                string claimnumber = "";
                DataTable dtNonResendClaims = new DataTable();

                if (c1InsClaimSchedule.Rows.Count > 1)
                {

                    {
                        int nSelectCount = 0;
                        nSelectCount = c1InsClaimSchedule.FindRow("True", 1, 0, true);
                        if (nSelectCount > 0)
                        {
                            DataTable dt = (DataTable)c1InsClaimSchedule.DataSource;

                            List<DataRow> dRow = (from myrow in dt.AsEnumerable()
                                                  where Convert.ToBoolean(myrow.Field<dynamic>("bIsTemplate")) == true
                                                  && (TransactionStatus)myrow.Field<dynamic>("nClaimStatus") != TransactionStatus.SendToClaimManager
                                                  && (TransactionStatus)myrow.Field<dynamic>("nClaimStatus") != TransactionStatus.SendToClearingHouse
                                                  select myrow).ToList<DataRow>(); ;


                            if (dRow != null && dRow.Count > 0)
                            {
                                dtNonResendClaims = dRow.CopyToDataTable();

                                DataColumn dcDescription = new DataColumn();
                                dcDescription.DataType = System.Type.GetType("System.String");
                                dcDescription.ColumnName = "Description";
                                dcDescription.Caption = "Description";


                                dtNonResendClaims.Columns.Add(dcDescription);
                                foreach (DataRow dr in dtNonResendClaims.Rows)
                                {
                                    string sDescription = string.Empty;
                                    TransactionStatus claimStatus = (TransactionStatus)dr["nClaimStatus"];
                                    if (claimStatus == TransactionStatus.Queue || claimStatus == TransactionStatus.Transacted || claimStatus == TransactionStatus.Batch)
                                        sDescription = "Claim is ready for billing.  No need to Resend.";
                                    else if (claimStatus == TransactionStatus.Pending)
                                        sDescription = "Bill Pending, claim cannot be resent";
                                    else
                                        sDescription = "The claim cannot be resent.";
                                    dr["Description"] = "Skipped : " + sDescription;
                                }
                                dtNonResendClaims.AcceptChanges();
                            }
                            List<DataRow> dRow1 = (from myrow in dt.AsEnumerable()
                                                   where Convert.ToBoolean(myrow.Field<dynamic>("bIsTemplate")) == true
                                                   && ((TransactionStatus)myrow.Field<dynamic>("nClaimStatus") == TransactionStatus.SendToClaimManager
                                                   || (TransactionStatus)myrow.Field<dynamic>("nClaimStatus") == TransactionStatus.SendToClearingHouse)
                                                   select myrow).ToList<DataRow>(); ;

                            if (dRow1 != null && dRow1.Count > 0)
                            {
                                nTrackTransactionID = dt.AsEnumerable()
                                                  .Where(r => Convert.ToBoolean(r["bIsTemplate"]) == true && ((TransactionStatus)(Convert.ToInt16(r["nClaimStatus"])) == TransactionStatus.SendToClaimManager || (TransactionStatus)(Convert.ToInt16(r["nClaimStatus"])) == TransactionStatus.SendToClearingHouse))
                                                  .Select(row => row["nTransactionID"].ToString())
                                                  .Aggregate((s1, s2) => String.Concat(s1, "," + s2));

                                nBillingTransactionID = dt.AsEnumerable()
                                                       .Where(r => Convert.ToBoolean(r["bIsTemplate"]) == true && ((TransactionStatus)(Convert.ToInt16(r["nClaimStatus"])) == TransactionStatus.SendToClaimManager || (TransactionStatus)(Convert.ToInt16(r["nClaimStatus"])) == TransactionStatus.SendToClearingHouse))
                                                       .Select(row => row["nTransactionMasterID"].ToString())
                                                       .Aggregate((s1, s2) => String.Concat(s1, "," + s2));

                            }

                            DataTable dtResult = new DataTable();
                            DataTable dtStatus = new DataTable();
                            frmInsTransCloseDate ofrmInsTransCloseDate = null;
                            if (nBillingTransactionID != "" && nTrackTransactionID != "")
                            {
                                ofrmInsTransCloseDate = new frmInsTransCloseDate(gloGlobal.gloPMGlobal.DatabaseConnectionString, nTrackTransactionID, nBillingTransactionID, Convert.ToString(DateTime.Now.Date));
                                ofrmInsTransCloseDate.ShowDialog(this);
                                if (ofrmInsTransCloseDate.oDialogResult)
                                {
                                    _InsTransferCloseDate = ofrmInsTransCloseDate.InsTransferCloseDate;
                                    ofrmInsTransCloseDate.Dispose();
                                    if (_InsTransferCloseDate != "")
                                    {
                                        oCollection = new CL_FollowUpCode();
                                        oCollection.ResendMultipleClaims(nBillingTransactionID, nTrackTransactionID, Convert.ToDateTime(_InsTransferCloseDate), out dtResult);
                                    }
                                }
                                ofrmInsTransCloseDate.Dispose();
                            }
                            //added condition to not show status form if Resend of claims is not perform.
                            if (_InsTransferCloseDate != "" || ofrmInsTransCloseDate == null)
                            {
                                if (dtNonResendClaims != null && dtNonResendClaims.Rows.Count > 0)
                                {
                                    dtStatus = DeleteExtraColumns(dtNonResendClaims);
                                }

                                if (dtStatus != null && dtStatus.Rows.Count > 0)
                                {
                                    if (dtResult != null && dtResult.Rows.Count > 0)
                                        dtStatus.Merge(dtResult);
                                }
                                else
                                {

                                    dtStatus = dtResult;
                                }

                                if (dtStatus != null && dtStatus.Rows.Count > 0)
                                {
                                    if (dtStatus.Rows.Count > 0)
                                    {
                                        frmBatchFollowUpStatus ofrmBatchFollowUpStatus = new frmBatchFollowUpStatus();
                                        ofrmBatchFollowUpStatus.dtFollowUpStatus = dtStatus;
                                        ofrmBatchFollowUpStatus.CalledFrom = "Resend";
                                        ofrmBatchFollowUpStatus.ShowDialog(this);
                                        ofrmBatchFollowUpStatus.Dispose();
                                    }
                                }
                            }
                            LoadInsuranceClaimQueue();
                        }
                        else if (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)) != "")
                        {
                            _transactionId = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index));
                            _MastertrnId = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index));
                            Transaction_Status = (TransactionStatus)(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nClaimStatus"].Index));
                            _ContactID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index));
                            _InsuranceID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index));
                            claimnumber = Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["sClaimNo"].Index)); ;

                            if (Transaction_Status == TransactionStatus.SendToClaimManager || Transaction_Status == TransactionStatus.SendToClearingHouse)
                            {
                                if (DialogResult.Yes == MessageBox.Show("Claim will be resent. \nContinue? ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                                {

                                    String strClaimRemitRefNo = "";
                                    strClaimRemitRefNo = gloCharges.CheckRefNumber(_MastertrnId, _ContactID, _InsuranceID, gloGlobal.gloPMGlobal.ClinicID);

                                    clsgloResend oClsgloResend = new clsgloResend();
                                    if (oClsgloResend.ResendClaim(_transactionId, _MastertrnId, Transaction_Status, strClaimRemitRefNo))
                                    {
                                        LoadInsuranceClaimQueue();
                                    }
                                    oClsgloResend.Dispose();
                                }
                            }
                            else if (Transaction_Status == TransactionStatus.Queue || Transaction_Status == TransactionStatus.Transacted || Transaction_Status == TransactionStatus.Batch)
                            {
                                MessageBox.Show("Claim is ready for billing.  No need to Resend.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                MessageBox.Show("The claim cannot be resent. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oCollection != null)
                {
                    oCollection.Dispose();
                    oCollection = null;
                }
                _InsTransferCloseDate = string.Empty;
                nTrackTransactionID = string.Empty;
                nBillingTransactionID = string.Empty;
                this.Cursor = Cursors.Default;
            }
        }

        private void tsb_BillPendingClaim_Click(object sender, EventArgs e)
        {
            if (c1InsClaimSchedule.Rows.Count > 1 && c1InsClaimSchedule.RowSel > 0)
            {
                int nSelectCount = 0;
                nSelectCount = c1InsClaimSchedule.FindRow("True", 1, 0, true);
                if (nSelectCount > 0)
                {
                    string claimnumber = "";
                    claimnumber = Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["sClaimNo"].Index));
                    if (DialogResult.Cancel == StopBulckAction("Bill Pending", claimnumber))
                    {
                        return;
                    }
                }

            }
            BillPendingNoBillClaim();
        }

        private void tsb_ClaimCMS1500_Click(object sender, EventArgs e)
        {
            ArrayList _CurTrnIDs = new ArrayList();
            ArrayList _MasTrnIDs = new ArrayList();

            try
            {
                Int64 _transactionId = 0;
                Int64 _MastertrnId = 0;
                Int64 _patientId = 0;
                Int64 _nContactID = 0;
                if (c1InsClaimSchedule.Rows.Count > 1)
                {
                    _transactionId = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index));
                    _MastertrnId = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index));
                    _patientId = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index));
                    _nContactID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index));
                    if (_transactionId > 0) { _CurTrnIDs.Add(_transactionId); }
                    if (_MastertrnId > 0) { _MasTrnIDs.Add(_MastertrnId); }


                    if (GetBillingType(_transactionId, _MastertrnId) == Convert.ToInt16(BillingType.Professional))
                    {
                        if (_CurTrnIDs.Count > 0)
                        {

                            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                            gloDatabaseLayer.DBParameters oDBPara = new gloDatabaseLayer.DBParameters();

                            oDBPara.Add("@nContactID", _nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDB.Connect(false);
                            Int16 nResult = Convert.ToInt16(oDB.ExecuteScalar("gsp_CheckPaperVersion", oDBPara));
                            oDB.Disconnect();
                            oDB.Dispose();
                            oDBPara.Clear();
                            oDBPara.Dispose();
                            oDB = null;
                            oDBPara = null;
                            switch ((gloSettings.PaperFormVersion)nResult)
                            {
                                case gloSettings.PaperFormVersion.CMS1500:
                                    {
                                        if (gloCharges.getICDRevisionbyClaimID(_MastertrnId, _transactionId) == gloGlobal.gloICD.CodeRevision.ICD10)//_InitialTransaction.nICDRevision == gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode())
                                        {
                                            if (MessageBox.Show("Selected claim(s) contains ICD-10 codes, billing ICD-10 on CMS1500 08/05 may cause billing rejection." + Environment.NewLine + Environment.NewLine + "Continue?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                            {
                                                frmHCFA1500 ofrmHCFA1500 = new frmHCFA1500(gloGlobal.gloPMGlobal.DatabaseConnectionString, _CurTrnIDs, _MasTrnIDs, true);
                                                ofrmHCFA1500.CallingTab = Convert.ToString("Queue");
                                                ofrmHCFA1500.ShowDialog(this);
                                                ofrmHCFA1500.Dispose();
                                                ofrmHCFA1500 = null;
                                            }
                                        }
                                        else
                                        {
                                            frmHCFA1500 ofrmHCFA1500 = new frmHCFA1500(gloGlobal.gloPMGlobal.DatabaseConnectionString, _CurTrnIDs, _MasTrnIDs, true);
                                            ofrmHCFA1500.CallingTab = Convert.ToString("Queue");
                                            ofrmHCFA1500.ShowDialog(this);
                                            ofrmHCFA1500.Dispose();
                                            ofrmHCFA1500 = null;

                                        }
                                        break;
                                    };

                                case gloSettings.PaperFormVersion.CMS1500New:
                                    {
                                        frmHCFA1500New ofrmHCFA1500New = new frmHCFA1500New(gloGlobal.gloPMGlobal.DatabaseConnectionString, _CurTrnIDs, _MasTrnIDs, true);
                                        ofrmHCFA1500New.CallingTab = Convert.ToString("Queue");
                                        ofrmHCFA1500New.ShowDialog(this);
                                        ofrmHCFA1500New.Dispose();
                                        ofrmHCFA1500New = null;
                                        break;

                                    };
                            }
                        }
                        else
                        {
                            MessageBox.Show("Select transaction. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        if (_CurTrnIDs.Count > 0)
                        {
                            frmUB04 ofrmUB04 = new frmUB04(gloGlobal.gloPMGlobal.DatabaseConnectionString, _CurTrnIDs, _MasTrnIDs, true);
                            ofrmUB04.CallingTab = Convert.ToString("Queue");
                            ofrmUB04.ShowDialog(this);
                            ofrmUB04.Dispose();
                            ofrmUB04 = null;
                        }
                        else
                        {
                            MessageBox.Show("Select transaction.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tsb_ClaimInsPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1InsClaimSchedule.Rows.Count > 1)
                {
                    gloAccountsV2.frmInsurancePaymentV2 oPaymentInsurace = new gloAccountsV2.frmInsurancePaymentV2(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    oPaymentInsurace.StartPosition = FormStartPosition.CenterScreen;
                    oPaymentInsurace.WindowState = FormWindowState.Maximized;
                    if (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)) != "")
                    {
                        if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["sClaimNo"].Index)).Trim() != string.Empty)
                        {
                            oPaymentInsurace.RevCycleClaimNo = Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["sClaimNo"].Index));
                        }

                    }
                    oPaymentInsurace.IsOpenFromRevenueCycle = true;
                    //oPaymentInsurace.PatientControl_OnClaimNumberEntered(Convert.ToString(PatientControl.ClaimNumber));
                    oPaymentInsurace.ShowInTaskbar = false;
                    oPaymentInsurace.ShowDialog(this);
                    oPaymentInsurace.Dispose();
                    oPaymentInsurace = null;

                    LoadInsuranceClaimQueue();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DialogResult StopBulckAction(string sMessage, string ClaimAccountNo)
        {
            DialogResult dr = DialogResult.None;
            dr = MessageBox.Show("Cannot perform Batch " + sMessage + ".\nContinue " + sMessage + " action for Claim # \"" + ClaimAccountNo + "\" ?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
             return dr;
             
        }
        private void tsbClaimGenerateTemp_Click(object sender, EventArgs e)
        {
         //   DialogResult _dlg = System.Windows.Forms.DialogResult.None;
            //try
            //{

            //    if (c1InsClaimSchedule.Rows.Count > 1)
            //    {
            //        int nSelectCount = 0;
            //        nSelectCount = c1InsClaimSchedule.FindRow("True", 1, 0, true);
            //        if (nSelectCount > 0)
            //        {
            //            string claimnumber = "";
            //            claimnumber =Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["sClaimNo"].Index));
            //            if (DialogResult.Cancel == StopBulckAction("Template", claimnumber))
            //            {
            //                return;
            //            }
            //        }

            //    }

            //          InsClaimScheduleTemplateDrilldown();            
                  
            //}
            //catch (Exception ex)
            //{

            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}
            DialogResult dlg = DialogResult.None;
            string message = string.Empty;
            if (c1InsClaimSchedule.Rows.Count > 1 && c1InsClaimSchedule.RowSel > 0)
            {
                int nSelectCount = 0;
                nSelectCount = c1InsClaimSchedule.FindRow("True", 1, 0, true);
                if (nSelectCount <= 0)
                {
                    InsClaimScheduleTemplateDrilldown();
                }
                else
                {
                    DataTable dtDefTemplate = new DataTable();
                    dtDefTemplate = CL_FollowUpCode.GetDefaultAssociateTemplate(Convert.ToString(cmbClaimFollowupAction.SelectedValue), CollectionEnums.FollowUpType.Claim);
                    if (dtDefTemplate != null && dtDefTemplate.Rows.Count <= 0)
                    {
                        //selected action not having template associated.
                        dlg = MessageBox.Show("Selected schedule action \"" + Convert.ToString(cmbClaimFollowupAction.Text) + "\" \ndoes not have associated template.\n\nContinue ?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (dlg == DialogResult.Cancel)
                        {
                            return;
                        }

                    }
                    if (dtDefTemplate == null)
                    {
                        //selected action not having template associated.
                        dlg = MessageBox.Show("Selected schedule action \"" + Convert.ToString(cmbClaimFollowupAction.Text) + "\" \ndoes not have associated template.\n\nContinue ?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (dlg == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                    Int64 nScheduledID = 0;
                    PatientDetails oAllSelectedPatientDetails = new PatientDetails();
                    for (int i = 1; i < c1InsClaimSchedule.Rows.Count; i++)
                    {
                        PatientDetail oSelectedPatientDetail = new PatientDetail();
                        oSelectedPatientDetail.PatientID = 0;
                        oSelectedPatientDetail.PatientAccountID = 0;
                        oSelectedPatientDetail.TransactionID = 0;
                        oSelectedPatientDetail.MstTransactionID = 0;
                        oSelectedPatientDetail.ContactID = 0;
                        oSelectedPatientDetail.InsuranceID = 0;
                        if (c1InsClaimSchedule.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            if (nScheduledID == 0)
                            {
                                nScheduledID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["nNextScheduleID"].Index));
                            }
                            oSelectedPatientDetail.PatientID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["nPatientID"].Index));
                            oSelectedPatientDetail.PatientAccountID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["nAccountID"].Index));
                            oSelectedPatientDetail.TransactionID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["nTransactionID"].Index));
                            oSelectedPatientDetail.MstTransactionID = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index));

                            DateTime dtLogTimeStamp, dtFolloUpTimeStamp;
                            GetLogAndScheduleDateTimeStamp(CollectionEnums.FollowUpType.Claim, oSelectedPatientDetail.TransactionID, out dtLogTimeStamp, out dtFolloUpTimeStamp);

                            oSelectedPatientDetail.dtLogTimeStamp = dtLogTimeStamp;
                            oSelectedPatientDetail.dtFollowUpTimeStamp = dtFolloUpTimeStamp;

                            Int64 nContactID, nInsuranceID;
                            GetContactInsuranceID(oSelectedPatientDetail.TransactionID, out nContactID, out nInsuranceID);
                            oSelectedPatientDetail.ContactID = nContactID;
                            oSelectedPatientDetail.InsuranceID = nInsuranceID;

                            DateTime dtTFL_DFLTimeStamp; string sTFL_DFL;
                            if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "")
                            {
                                dtTFL_DFLTimeStamp = Convert.ToDateTime(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["Tfl_Dfl_Date"].Index)));
                                sTFL_DFL = "TFL";
                            }
                            else if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["DFLDays"].Index)) != "")
                            {
                                dtTFL_DFLTimeStamp = Convert.ToDateTime(Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.Rows[i].Index, c1InsClaimSchedule.Cols["Tfl_Dfl_Date"].Index)));
                                sTFL_DFL = "DFL";
                            }
                            else
                            {
                                dtTFL_DFLTimeStamp = DateTime.MinValue;
                                sTFL_DFL = "";
                            }
                            oSelectedPatientDetail.dtTFL_DFLTimeStamp = dtTFL_DFLTimeStamp;
                            oSelectedPatientDetail.TFL_DFL = sTFL_DFL;
                            oAllSelectedPatientDetails.Add(oSelectedPatientDetail);
                        }
                        oSelectedPatientDetail.Dispose();
                        oSelectedPatientDetail = null;
                    }

                    if (oAllSelectedPatientDetails != null && oAllSelectedPatientDetails.Count > 0)
                    {
                        InsClaimScheduleTemplateDrilldown_Batch(oAllSelectedPatientDetails, nScheduledID);
                    }
                    if (oAllSelectedPatientDetails != null)
                    {
                        oAllSelectedPatientDetails.Dispose();
                        oAllSelectedPatientDetails = null;
                    }
                }
            }
        }

        private void tsb_ClaimClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            btnDown.Visible = true;
            btnUp.Visible = false;
            pnlInsClaimBannerMain.Visible = false;
            pnlInsuranceClaimBanner.Height = pnlHeader.Height;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            btnDown.Visible = false;
            btnUp.Visible = true;
            pnlInsClaimBannerMain.Visible = true;
            pnlInsuranceClaimBanner.Height = pnlHeader.Height + 203;
        }

        //7022 Items: Remove claim from queue manually
        //Description: New button for Remove Insurance claim follow-up (named as "Clear follow-up") is added with click event.
        private void tsb_ClaimDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int nSelectedCount = 0;
                nSelectedCount = c1InsClaimSchedule.FindRow("True", 1, 0, true);
                if (nSelectedCount <= 0)
                {
                    InsClaimScheduleDelete();
                }
                else
                {
                    //if (c1InsClaimSchedule.RowSel > 0 && c1InsClaimSchedule.ColSel >= 0)
                    //{
                        string sTransactionIDs = string.Empty;

                        DataTable dt = (DataTable)c1InsClaimSchedule.DataSource;
                        sTransactionIDs = dt.AsEnumerable()
                                              .Where(r => Convert.ToBoolean(r["bIsTemplate"]) == true)
                                              .Select(row => row["nTransactionID"].ToString())
                                              .Aggregate((s1, s2) => String.Concat(s1, "," + s2));

                        if (sTransactionIDs != "")
                        {
                            DeleteMultipleFollowUpScheduled(sTransactionIDs, CollectionEnums.FollowUpType.Claim);
                        }
                        LoadInsuranceClaimQueue();
                    //}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.Delete, "Exception: Claim Follow-up delete " + ex.Message + " .", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                throw;
            }
        }

        private void DeleteMultipleFollowUpScheduled(string sTransactionIDs,CollectionEnums.FollowUpType FollowUpType)
        {
            DialogResult res = System.Windows.Forms.DialogResult.No;
            CL_FollowUpCode oCollection = new CL_FollowUpCode();
            decimal dTotalBalance = 0;
            bool bIsDeleted = false;
            string sFollowup = string.Empty;
            try
            {
                switch (FollowUpType)
                {
                    case CollectionEnums.FollowUpType.Claim:
                        {
                            dTotalBalance = oCollection.GetMultipleCalimBalance(sTransactionIDs);
                            if (dTotalBalance > 0)
                                res = MessageBox.Show("Warning:" + Environment.NewLine + "You are about to remove Claim(s) from Follow-Up, one of the claim still has a remaining balance." + Environment.NewLine + Environment.NewLine + "Normally, there is no need to perform this function and you should not proceed." + Environment.NewLine + Environment.NewLine + "Claim Balances that are removed from Follow-Up are more difficult to track and may get lost. " + Environment.NewLine + Environment.NewLine + "Instead, you should take other steps, like rebill the claim, move it to patient responsibility, write off the balance, or schedule it for an additional follow-up action." + Environment.NewLine + Environment.NewLine + "Remove this Claim from Follow-Up?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            else
                                res = DialogResult.Yes;
                            sFollowup = "Claim";
                        }
                        break;
                    case CollectionEnums.FollowUpType.PatientAccount:
                        {
                            res = MessageBox.Show("Warning: Account will be removed from the follow-up queue." + Environment.NewLine + "Continue?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            if (res == DialogResult.OK)
                            {
                                res = DialogResult.Yes;
                            }
                            sFollowup = "Patient Account";
                        }
                        break;
                    case CollectionEnums.FollowUpType.BadDebt:
                        {
                            res = MessageBox.Show("Warning: Account will be removed from the follow-up queue." + Environment.NewLine + "Continue?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            if (res == DialogResult.OK)
                            {
                                res = DialogResult.Yes;
                            }
                            sFollowup = "Bad Debt";
                        }
                        break;
                }

                if (res == DialogResult.Yes)
                {
                    bIsDeleted = oCollection.DeleteFollowUpSchedule_Multiple(sTransactionIDs, FollowUpType);
                    if (bIsDeleted)
                    {
                        if (FollowUpType == CollectionEnums.FollowUpType.Claim)
                        {
                            if (dTotalBalance > 0)
                                InsertNotesForDeletedFollowUpClaim_Multiple(sTransactionIDs);
                        }
                    }
                }
                oCollection.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.Delete, "Exception in DeleteMultipleFollowUpScheduled method for Transaction/Account ID: "+sTransactionIDs +Environment.NewLine+"Exception details: " + ex.Message + " .", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oCollection!=null)
                {
                    oCollection.Dispose();
                    oCollection = null;
                }
            }
        }

        #endregion

        #region " Bad Debt Account Follow Up Queue"

        private void tsb_BadDebt_PatientPayment_Click(object sender, EventArgs e)
        {
           // LoadPatientPaymentScreen(CollectionEnums.FollowUpType.BadDebt);
        }

        private void tsb_BadDebt_PatAcct_Click(object sender, EventArgs e)
        {
           // PAAccountDrilldown(CollectionEnums.FollowUpType.BadDebt);
        }

        private void tsb_BadDebt_AcctFUReport_Click(object sender, EventArgs e)
        {
          //  MessageBox.Show("Work in-progress...", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void tsb_BadDebt_Action_Click(object sender, EventArgs e)
        {
            //Int64 nScheduleID = 0;
            //PatientDetails selectedFollowUpDetails = null;
            //PatientDetail oSelectedPatientDetail = null;
            //DateTime dtLogTimeStamp, dtFollowUpTimeStamp;

            //try
            //{
            //    if (c1BadDebtLogView != null && c1BadDebtLogView.Rows.Count > 1)
            //    {
            //        selectedFollowUpDetails = new PatientDetails();

            //        int nSelectCount = 0;
            //        nSelectCount = c1BadDebtLogView.FindRow("True", 1, 0, true);

            //        if (nSelectCount <= 0)
            //        {
            //            #region "Single selection with out check-box "
            //            if (c1BadDebtLogView.RowSel > 0 && c1BadDebtLogView.ColSel >= 0)
            //            {
            //                Int64.TryParse(Convert.ToString(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nAccScheduleID"].Index)), out nScheduleID);

            //                Int64 PatientID = 0;
            //                Int64.TryParse(Convert.ToString(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index)), out PatientID);

            //                Int64 PAccountID = 0;
            //                Int64.TryParse(Convert.ToString(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nAccountID"].Index)), out PAccountID);

            //                Int64 CollectionAgencyContactId = 0;
            //                //Int64.TryParse(Convert.ToString(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nContactID"].Index)), out CollectionAgencyContactId);

            //                Int64 AccountPatientID = 0;
            //                Int64.TryParse(Convert.ToString(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nAccountID"].Index)), out AccountPatientID);

            //                GetLogAndScheduleDateTimeStamp(CollectionEnums.FollowUpType.BadDebt, PAccountID, out dtLogTimeStamp, out dtFollowUpTimeStamp);

            //                selectedFollowUpDetails.Add(new PatientDetail()
            //                {
            //                    PatientID = PatientID,
            //                    PatientAccountID = PAccountID,
            //                    TransactionID = 0,
            //                    MstTransactionID = 0,
            //                    dtLogTimeStamp = dtLogTimeStamp,
            //                    dtFollowUpTimeStamp = dtFollowUpTimeStamp,
            //                    ContactID = CollectionAgencyContactId,
            //                    InsuranceID = 0,
            //                    dtTFL_DFLTimeStamp = DateTime.MinValue,
            //                    TFL_DFL = "",
            //                    ScheduleId = nScheduleID
            //                });

            //                if (selectedFollowUpDetails != null && selectedFollowUpDetails.Count > 0)
            //                { BadDebtScheduleDrilldown(selectedFollowUpDetails, false); }
            //            }

            //            #endregion "Single selection with out check-box "
            //        }
            //        else
            //        {
            //            for (int i = 1; i < c1BadDebtLogView.Rows.Count; i++)
            //            {

            //                oSelectedPatientDetail = new PatientDetail();
            //                oSelectedPatientDetail.PatientID = 0;
            //                oSelectedPatientDetail.PatientAccountID = 0;
            //                oSelectedPatientDetail.TransactionID = 0;
            //                oSelectedPatientDetail.MstTransactionID = 0;
            //                oSelectedPatientDetail.ContactID = 0;
            //                oSelectedPatientDetail.InsuranceID = 0;
            //                if (c1BadDebtLogView.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
            //                {
            //                    if (nScheduleID == 0)
            //                    {
            //                        nScheduleID = Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.Rows[i].Index, c1BadDebtLogView.Cols["nAccScheduleID"].Index));
            //                    }
            //                    oSelectedPatientDetail.PatientID = Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.Rows[i].Index, c1BadDebtLogView.Cols["nPatientID"].Index));
            //                    oSelectedPatientDetail.PatientAccountID = Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.Rows[i].Index, c1BadDebtLogView.Cols["nAccountID"].Index));
            //                    oSelectedPatientDetail.TransactionID = 0;
            //                    oSelectedPatientDetail.MstTransactionID = 0;
            //                    GetLogAndScheduleDateTimeStamp(CollectionEnums.FollowUpType.BadDebt, oSelectedPatientDetail.PatientAccountID, out dtLogTimeStamp, out dtFollowUpTimeStamp);
            //                    oSelectedPatientDetail.dtLogTimeStamp = dtLogTimeStamp;
            //                    oSelectedPatientDetail.dtFollowUpTimeStamp = dtFollowUpTimeStamp;
            //                    //oSelectedPatientDetail.ContactID = Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.Rows[i].Index, c1BadDebtLogView.Cols["nContactID"].Index)); ;
            //                    oSelectedPatientDetail.InsuranceID = 0;
            //                    oSelectedPatientDetail.dtTFL_DFLTimeStamp = DateTime.MinValue;
            //                    oSelectedPatientDetail.TFL_DFL = "";
            //                    selectedFollowUpDetails.Add(oSelectedPatientDetail);
            //                }
            //                oSelectedPatientDetail.Dispose();
            //                oSelectedPatientDetail = null;
            //            }
            //            if (selectedFollowUpDetails != null && selectedFollowUpDetails.Count > 0)
            //            {
            //                BadDebtScheduleDrilldown(selectedFollowUpDetails, true);
            //            }
            //            if (selectedFollowUpDetails != null)
            //            {
            //                selectedFollowUpDetails.Dispose();
            //                selectedFollowUpDetails = null;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            //}
        }

        private void tsb_BadDebt_RemoveAcct_Click(object sender, EventArgs e)
        {
            //PatientDetails selectedFollowUpDetails = null;
            //Int64 PAccountID = 0;

            //try
            //{
            //    if (c1BadDebtLogView != null && c1BadDebtLogView.Rows.Count > 1)
            //    {
            //        selectedFollowUpDetails = new PatientDetails();

            //        int nSelectCount = 0;
            //        nSelectCount = c1BadDebtLogView.FindRow("True", 1, 0, true);

            //        if (nSelectCount <= 0)
            //        {
            //            if (c1BadDebtLogView.RowSel > 0 && c1BadDebtLogView.ColSel >= 0)
            //            {
            //                PAccountID = 0;
            //                Int64.TryParse(Convert.ToString(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nAccountID"].Index)), out PAccountID);

            //                if (PAccountID > 0)
            //                {
            //                    selectedFollowUpDetails.Add(new PatientDetail()
            //                    {
            //                        PatientAccountID = PAccountID
            //                    });
            //                }
            //            }
            //        }
            //        else
            //        {
            //            for (int i = 1; i < c1BadDebtLogView.Rows.Count; i++)
            //            {
            //                if (c1BadDebtLogView.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
            //                {
            //                    PAccountID = 0;
            //                    Int64.TryParse(Convert.ToString(c1BadDebtLogView.GetData(c1BadDebtLogView.Rows[i].Index, c1BadDebtLogView.Cols["nAccountID"].Index)), out PAccountID);

            //                    if (PAccountID > 0)
            //                    {
            //                        selectedFollowUpDetails.Add(new PatientDetail()
            //                        {
            //                            PatientAccountID = PAccountID
            //                        });
            //                    }
            //                }
            //            }
            //        }


            //        if (selectedFollowUpDetails != null && selectedFollowUpDetails.Count > 0)
            //        {
            //            //Send list of accounts to remove from bad debt
            //            string sAccountIDs = string.Empty;
            //            for (int i = 0; i < selectedFollowUpDetails.Count; i++)
            //            {
            //                if (sAccountIDs == "")
            //                    sAccountIDs = Convert.ToString(selectedFollowUpDetails[i].PatientAccountID);
            //                else
            //                    sAccountIDs = sAccountIDs + ',' + Convert.ToString(selectedFollowUpDetails[i].PatientAccountID);
            //            }

            //            if (sAccountIDs != "")
            //            {
            //                DeleteMultipleFollowUpScheduled(sAccountIDs, CollectionEnums.FollowUpType.BadDebt);
            //            }
            //            LoadBadDebtAccountQueue();
            //        }

            //    }

            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured while removing account from bad debt.", true);
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            //}
            //finally
            //{
            //    if (selectedFollowUpDetails != null)
            //    {
            //        selectedFollowUpDetails.Clear();
            //        selectedFollowUpDetails.Dispose();
            //        selectedFollowUpDetails = null;
            //    }
            //}
        }

        private void tsb_BadDebt_WriteOff_Click(object sender, EventArgs e)
        {
            //PatientDetails selectedFollowUpDetails = null;
            //Int64 PAccountID = 0;
            //Int64 PatientID = 0;
            //DateTime dtCloseDate = DateTime.MinValue;
            //Int64 paymentTrayId = 0;
            //string paymentTrayName = string.Empty;
            //string adjustmentCode = string.Empty;
            //string adjustmentDesc = string.Empty;
            //bool removeBadDebtFollowup = false;
            //bool removeBadDebtStatus = false;
            //CL_FollowUpCode oCollection = null;
            //frmPaymentTransferInfo ofrmPaymentTransferInfo = null;
            //try
            //{
            //    if (c1BadDebtLogView != null && c1BadDebtLogView.Rows.Count > 1)
            //    {
            //        selectedFollowUpDetails = new PatientDetails();

            //        int nSelectCount = 0;
            //        nSelectCount = c1BadDebtLogView.FindRow("True", 1, 0, true);

            //        if (nSelectCount <= 0)
            //        {
            //            if (c1BadDebtLogView.RowSel > 0 && c1BadDebtLogView.ColSel >= 0)
            //            {
            //                PAccountID = 0;
            //                Int64.TryParse(Convert.ToString(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nAccountID"].Index)), out PAccountID);
            //                PatientID = 0;
            //                Int64.TryParse(Convert.ToString(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index)), out PatientID);

            //                if (PAccountID > 0)
            //                {
            //                    selectedFollowUpDetails.Add(new PatientDetail()
            //                    {
            //                        PatientAccountID = PAccountID,
            //                        PatientID = PatientID
            //                    });
            //                }
            //            }
            //        }
            //        else
            //        {
            //            for (int i = 1; i < c1BadDebtLogView.Rows.Count; i++)
            //            {
            //                if (c1BadDebtLogView.GetCellCheck(i, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
            //                {
            //                    PAccountID = 0;
            //                    Int64.TryParse(Convert.ToString(c1BadDebtLogView.GetData(c1BadDebtLogView.Rows[i].Index, c1BadDebtLogView.Cols["nAccountID"].Index)), out PAccountID);
            //                    PatientID = 0;
            //                    Int64.TryParse(Convert.ToString(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index)), out PatientID);

            //                    if (PAccountID > 0)
            //                    {
            //                        selectedFollowUpDetails.Add(new PatientDetail()
            //                        {
            //                            PatientAccountID = PAccountID,
            //                            PatientID = PatientID
            //                        });
            //                    }
            //                }
            //            }
            //        }


            //        if (selectedFollowUpDetails != null && selectedFollowUpDetails.Count > 0)
            //        {
            //            string sAccountIDs = string.Empty;
            //            for (int i = 0; i < selectedFollowUpDetails.Count; i++)
            //            {
            //                if (selectedFollowUpDetails[i].PatientAccountID > 0)
            //                {
            //                    if (sAccountIDs == "")
            //                        sAccountIDs = Convert.ToString(selectedFollowUpDetails[i].PatientAccountID);
            //                    else
            //                        sAccountIDs = sAccountIDs + ',' + Convert.ToString(selectedFollowUpDetails[i].PatientAccountID);
            //                }
            //            }
            //            ofrmPaymentTransferInfo = new frmPaymentTransferInfo();
            //            ofrmPaymentTransferInfo.AccountIDs = sAccountIDs;
            //            ofrmPaymentTransferInfo.ShowDialog();

            //            //Set values here..
            //            //if (ofrmPaymentTransferInfo.PaymentTrayID != null && ofrmPaymentTransferInfo.PaymentTrayName != null && ofrmPaymentTransferInfo.AdjustmentCode != null && ofrmPaymentTransferInfo.AdjustmentDescription != null)
            //            if (ofrmPaymentTransferInfo.PaymentTrayName != null && ofrmPaymentTransferInfo.AdjustmentCode != null && ofrmPaymentTransferInfo.AdjustmentDescription != null)
            //            {
            //                dtCloseDate = Convert.ToDateTime(ofrmPaymentTransferInfo.PaymentTransferCloseDate);
            //                paymentTrayId = ofrmPaymentTransferInfo.PaymentTrayID;
            //                paymentTrayName = ofrmPaymentTransferInfo.PaymentTrayName;
            //                adjustmentCode = ofrmPaymentTransferInfo.AdjustmentCode;
            //                adjustmentDesc = ofrmPaymentTransferInfo.AdjustmentDescription;
            //                removeBadDebtFollowup = ofrmPaymentTransferInfo.RemoveBadDebtFollowup;
            //                removeBadDebtStatus = ofrmPaymentTransferInfo.RemoveBadDebtStatus;
            //            }
            //            else
            //            {
            //                return;
            //            }
            //            ofrmPaymentTransferInfo.Dispose();
            //            ofrmPaymentTransferInfo = null;

            //            gloAccountPayment.BulkPaymentOperation bulkWriteOff = null;
            //            gloAccountPayment.PaymentInfoParameter paymentParameter = null;
            //            gloAccountPayment.AccountOwnerInfo currentacctInfo = null;

            //            for (int i = 0; i < selectedFollowUpDetails.Count; i++)
            //            {
            //                if (selectedFollowUpDetails[i].PatientAccountID > 0)
            //                {
            //                    currentacctInfo = gloAccountPayment.AccountInfo.GetAccountInfo(selectedFollowUpDetails[i].PatientAccountID);

            //                    paymentParameter = new gloAccountPayment.PaymentInfoParameter(
            //                          currentacctInfo.AccountownerpatientId,
            //                          currentacctInfo.AccountId,
            //                          currentacctInfo.GuarantorId,
            //                          currentacctInfo.AccountpatientId,
            //                          dtCloseDate,
            //                          paymentTrayId,
            //                          paymentTrayName,
            //                          adjustmentCode,
            //                          adjustmentDesc
            //                        );

            //                    bulkWriteOff = new gloAccountPayment.BulkPaymentOperation();
            //                    if (bulkWriteOff.WriteOffAccountDue(paymentParameter))
            //                    {
            //                        oCollection = new CL_FollowUpCode();
            //                        if (removeBadDebtFollowup)
            //                        {
            //                            oCollection.DeleteFollowUpSchedule_Multiple(Convert.ToString(selectedFollowUpDetails[i].PatientAccountID), CollectionEnums.FollowUpType.BadDebt);
            //                            if (removeBadDebtStatus)
            //                            {
            //                                oCollection.RemoveBadDebtStatus(selectedFollowUpDetails[i].PatientID);

            //                            }
            //                        }
            //                    }

            //                    paymentParameter = null;
            //                    currentacctInfo = null;
            //                }
            //            }

            //            LoadBadDebtAccountQueue();
            //        }

            //    }
            //    oCollection.Dispose();
            //    oCollection = null;
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured while removing account from bad debt.", true);
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            //}
            //finally
            //{
            //    if (selectedFollowUpDetails != null)
            //    {
            //        selectedFollowUpDetails.Clear();
            //        selectedFollowUpDetails.Dispose();
            //        selectedFollowUpDetails = null;
            //    }
            //    if (ofrmPaymentTransferInfo != null)
            //    {
            //        ofrmPaymentTransferInfo.Dispose(); ofrmPaymentTransferInfo = null;
            //    }
            //    if (oCollection != null)
            //    {
            //        oCollection.Dispose(); oCollection = null;
            //    }
            //}
        }

        private void tsb_BadDebt_Refresh_Click(object sender, EventArgs e)
        {

            //nBadDebtActSortedColumn = -1;
            //if (gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_FollowupQueue") && pnlBadDebtBusinessCenter.Visible == false)
            //{

            //    if (cmbBadDebtBusinessCenter.DataSource == null)
            //    {
            //        cmbBadDebtBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
            //        cmbBadDebtBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            //        FillBusinessCenter(cmbBadDebtBusinessCenter);
            //    }
            //    pnlBadDebtBusinessCenter.Visible = true;
            //    cmbBadDebtBusinessCenter.SelectedValue = gloGlobal.gloPMGlobal.GetDefaultBusinessCenterForUser(gloGlobal.gloPMGlobal.UserID);
            //}
            //else if (!gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_FollowupQueue"))
            //    pnlBadDebtBusinessCenter.Visible = false;

            //LoadBadDebtAccountQueue();
            //ShowHideBadDebtToolstripButtons();
        }

        private void ShowHideBadDebtToolstripButtons()
        {
            //if (cmbBadDebtScheduleAction.Text != "Write-Off BadDebt-Write-Off BadDebt due")
            //{
            //    tsb_BadDebt_WriteOff.Enabled = false;
            //}
            //else if (cmbBadDebtScheduleAction.Text == "Write-Off BadDebt-Write-Off BadDebt due")
            //{
            //    tsb_BadDebt_WriteOff.Enabled = true;
            //}
        }

        private void tsb_BadDebt_Close_Click(object sender, EventArgs e)
        {
           // this.Close();
        }

        #endregion " Bad Debt Account Follow Up Queue"

        #endregion "Tool Strip Events "

        #region "C1 Grid Events "

        #region "Account Queue "

        private void c1PALogView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (c1PALogView.MouseRow > 0 && c1PALogView.MouseCol >= 0)
                {
                    PAScheduleDrilldown();
                    //FillPatientQueueTab();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        private void c1PALogView_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
        }

        private void c1PALogView_EnterCell(object sender, EventArgs e)
        {
            try
            {
                if (iAccountSelRow != c1PALogView.RowSel)
                {
                    if (c1PALogView.Rows.Count > 1)
                    {
                        iAccountSelRow = c1PALogView.RowSel;

                        if (c1PALogView.RowSel > 0)
                        {
                            if (
                                    c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index) != null && Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)) != 0
                                   && c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index) != null && Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)) != 0
                               )
                            {
                                LoadPatientStrip(Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)), Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)), false);
                                oPatientControl.Visible = true;
                            }
                        }


                    }
                    else
                    {
                        oPatientControl.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }

        }

        private void c1PALogView_Click(object sender, EventArgs e)
        {
            //if (c1PALogView.Rows.Count > 1 && c1PALogView.Rows.Count > 0)
            //iAccountSelRow = c1PALogView.RowSel;
            //code start-Added by kanchan on 20130611 to maintain selection
            if (c1PALogView != null)
            {
                if (c1PALogView.Rows.Count > 1) 
	            {
                CurrencyManager cm = (CurrencyManager)BindingContext[this.c1PALogView.DataSource];
                DataRowView dr = cm.Current as DataRowView;
                _AccountSelRow = dr.Row.Table.Rows.IndexOf(dr.Row);

                if (nActSortedColumn > -1)
                    iAccountNextSelRow = c1PALogView.RowSel;
	            }
            }
            //code end-Added by kanchan on 20130611 to maintain selection
        }

        private void c1PALogView_AfterSort(object sender, SortColEventArgs e)
        {
            try
            {
                if (c1PALogView.Rows.Count > 1)
                {
                    if (c1PALogView.RowSel > 0)
                    {
                        if (
                                c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index) != null && Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)) != 0
                               && c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index) != null && Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)) != 0
                           )
                        {
                            LoadPatientStrip(Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)), Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nAccountID"].Index)), false);
                            oPatientControl.Visible = true;
                        }
                    }
                    //code start-Added by kanchan on 20130611 to maintain selection
                    if (_AccountSelRow > -1)
                    {
                        foreach (C1.Win.C1FlexGrid.Row rw in c1PALogView.Rows)
                        {
                            CurrencyManager cm = (CurrencyManager)BindingContext[this.c1PALogView.DataSource];
                            DataRowView dr = rw.DataSource as DataRowView;
                            if (dr != null)
                            {
                                int currIndex = dr.Row.Table.Rows.IndexOf(dr.Row);
                                if (currIndex == _AccountSelRow)
                                {
                                    CellRange cr = c1PALogView.GetCellRange(rw.Index, 1);
                                    // to scroll the selected row in the visible area
                                    c1PALogView.Select(cr, true);
                                    cr = c1PALogView.GetCellRange(rw.Index, 0, rw.Index, c1PALogView.Cols.Count - 1);
                                    c1PALogView.Select(cr, false);
                                    break;
                                }
                            }
                        }
                    }
                    //code end-Added by kanchan on 20130611 to maintain selection
                    nActSortedColumn = e.Col;
                    oActSortFlags = e.Order;
                    if (nActSortedColumn > -1)
                        iAccountNextSelRow = c1PALogView.RowSel;
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1PALogView_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                c1PALogView.Redraw = false;
                if (e.Row == 0 && e.Col == 0)
                {
                    c1PALogView.FinishEditing();
                    c1PALogView.Select(0, 0);  
                    DataTable dt = (DataTable)c1PALogView.DataSource;
                    if (dt != null && dt.Rows.Count >0)
                    {
                        if (c1PALogView.GetCellCheck(e.Row, e.Col) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            dt.Select().ToList<DataRow>().ForEach(r => r["bIsTemplate"] = true);
                        }
                        else
                        {
                            dt.Select().ToList<DataRow>().ForEach(r => r["bIsTemplate"] = false);
                        }
                        c1PALogView.DataSource = dt.Copy();
                        //c1PALogView.Select();
                        if (dt != null) { dt.Dispose(); dt = null; }
                        //c1PALogView.EnterCell -= new System.EventHandler(this.c1PALogView_EnterCell);
                        //int i = 0;
                        

                            //for (i = 1; i <= c1PALogView.Rows.Count - 1; i++)
                            //{
                            //    c1PALogView.SetData(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            //}

                        
                            //for (i = 1; i <= c1PALogView.Rows.Count - 1; i++)
                            //{
                            //    c1PALogView.SetData(i, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            //}
                       
                        //c1PALogView.EnterCell += new System.EventHandler(this.c1PALogView_EnterCell);
                    }
                }
                //else
                //{
                //    if (c1PALogView.Rows.Count > 1)
                //    {
                //        int i = 0;
                //        i = c1PALogView.FindRow("False", 1, 0, false);
                //        if (i > 0)
                //        {
                //            c1PALogView.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                //        }
                //        else
                //        {
                //            c1PALogView.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                //        }
                //    }
                //}

                c1PALogView.Redraw = true;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
        }
        #endregion

        #region "Insurance Claim "

        private void c1InsClaimSchedule_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (c1InsClaimSchedule.MouseRow > 0 && c1InsClaimSchedule.MouseCol >= 0)
                {
                    InsClaimScheduleDrilldown();
                    //FillInsuranceClaimTab();
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1InsClaimSchedule_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1ActOverDue_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1ClaimOverDue_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1InsClaimSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                //code start-Added by kanchan on 20130611 to maintain selection
                CurrencyManager cm = (CurrencyManager)BindingContext[this.c1InsClaimSchedule.DataSource];
                DataRowView dr = cm.Current as DataRowView;
                iClaimSelRow = dr.Row.Table.Rows.IndexOf(dr.Row);
                //code end-Added by kanchan on 20130611 to maintain selection

                if (nInsSortedColumn > -1)
                { iClaimNextSelRow = c1InsClaimSchedule.RowSel; }

                SetToolStripButtonForInsuranceQueue();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1InsClaimSchedule_EnterCell(object sender, EventArgs e)
        {
            try
            {
                if (c1InsClaimSchedule.Rows.Count > 1)
                {
                    if (c1InsClaimSchedule.RowSel > 0)
                    {
                        //if (iClaimSelRow != c1InsClaimSchedule.RowSel || iClaimSelRow == 1)
                       // {
                            if (
                                    (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)) != "") &&
                                    (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index)) != "") &&
                                    (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index)) != "") &&
                                    (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index)) != "")
                                )
                            {
                               
                                FillInsuranceClaimTabBanner(Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index)));
                                pnlInsuranceClaimBanner.Visible = true;
                            }
                       // }

                        // TFL and DFL Changes
                        if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "")
                        {
                            lblTFLDFLDate.Text = Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["Tfl_Dfl_Date"].Index));
                            lblTflDfl.Text = "TFL Date :";
                            lblTflDfl.Visible = true;

                        }
                        else if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)) != "")
                        {
                            lblTFLDFLDate.Text = Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["Tfl_Dfl_Date"].Index));
                            lblTflDfl.Text = "DFL Date :";
                            lblTflDfl.Visible = true;

                        }
                        else
                        {

                            lblTFLDFLDate.Text = "";
                            lblTflDfl.Visible = false;

                        }

                        // TFL and DFL Changes
                        if ((Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "" || Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)) != ""))
                        {
                            Int64 TFLDays = 0;
                            if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "")
                            {
                                TFLDays = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index));
                            }
                            else if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)) != "")
                            { TFLDays = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)); }


                            if (TFLDays > 0 && TFLDays <= 30)
                            {
                                lblTFLDFLDate.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblFollowUpDt.Font, FontStyle.Bold);
                                lblTFLDFLDate.ForeColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
                            }
                            else if (TFLDays < 0)
                            {
                                lblTFLDFLDate.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblFollowUpDt.Font, FontStyle.Bold);
                                lblTFLDFLDate.ForeColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(148)))));
                            }
                            else
                            {
                                lblTFLDFLDate.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblFollowUpDt.Font, FontStyle.Bold);
                                lblTFLDFLDate.ForeColor = Color.Black;
                            }
                        }
                        else
                        {
                            lblTflDfl.Visible = false;
                            lblTFLDFLDate.Visible = false;
                        }
                        if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index)) == "" && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)) == "")
                        {
                            lblTFLDFLDate.Visible = false;
                            lblTflDfl.Visible = false;
                        }
                        else
                        {
                            lblTFLDFLDate.Visible = true;
                            lblTflDfl.Visible = true;
                        }


                    }

                    SetToolStripButtonForInsuranceQueue();
                }
                else
                {
                    pnlInsuranceClaimBanner.Visible = false;
                }

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1InsClaimSchedule_AfterSort(object sender, SortColEventArgs e)
        {
            try
            {
                if (c1InsClaimSchedule.Rows.Count > 1)
                {
                    if (c1InsClaimSchedule.RowSel > 0)
                    {
                        if (e.Col == c1InsClaimSchedule.Cols["sClaimNo"].Index)
                        {
                            c1InsClaimSchedule.Cols["SortClaim"].Sort = e.Order;
                            c1InsClaimSchedule.Cols["SortSubClaim"].Sort = SortFlags.Ascending;
                            c1InsClaimSchedule.Sort(SortFlags.UseColSort, c1InsClaimSchedule.Cols["SortClaim"].Index, c1InsClaimSchedule.Cols["SortSubClaim"].Index);
                        }

                        
                        if (
                                (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)) != "") &&
                                (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index)) != "") &&
                                (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index)) != "") &&
                                (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index)) != "")
                            )
                        {
                            if ((Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "" || Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)) != ""))
                            {
                                Int64 TFLDays = 0;
                                if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "")
                                {
                                    TFLDays = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index));
                                }
                                else if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)) != "")
                                { TFLDays = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)); }


                                if (TFLDays > 0 && TFLDays <= 30)
                                {
                                    lblTFLDFLDate.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblFollowUpDt.Font, FontStyle.Bold);
                                    lblTFLDFLDate.ForeColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
                                }
                                else if (TFLDays < 0)
                                {
                                    lblTFLDFLDate.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblFollowUpDt.Font, FontStyle.Bold);
                                    lblTFLDFLDate.ForeColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(148)))));
                                }
                                else
                                {
                                    lblTFLDFLDate.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblFollowUpDt.Font, FontStyle.Bold);
                                    lblTFLDFLDate.ForeColor = Color.Black;
                                }
                            }
                            else
                            {
                                lblTflDfl.Visible = false;
                                lblTFLDFLDate.Visible = false;
                            }
                            FillInsuranceClaimTabBanner(Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index)));
                        }
                        if (iClaimSelRow > -1)
                        {
                            foreach (C1.Win.C1FlexGrid.Row rw in c1InsClaimSchedule.Rows)
                            {
                                CurrencyManager cm = (CurrencyManager)BindingContext[this.c1InsClaimSchedule.DataSource];
                                DataRowView dr = rw.DataSource as DataRowView;
                                if (dr != null)
                                {
                                    int currIndex = dr.Row.Table.Rows.IndexOf(dr.Row);
                                    if (currIndex == iClaimSelRow)
                                    {
                                        CellRange cr = c1InsClaimSchedule.GetCellRange(rw.Index, 1);
                                        // to scroll the selected row in the visible area
                                        c1InsClaimSchedule.Select(cr, true);
                                        cr = c1InsClaimSchedule.GetCellRange(rw.Index, 0, rw.Index, c1InsClaimSchedule.Cols.Count - 1);
                                        c1InsClaimSchedule.Select(cr, false);
                                        break;
                                    }
                                }
                            }
                        }
                        if (e.Col == c1InsClaimSchedule.Cols["sClaimNo"].Index)
                        {
                            nInsSortedColumn = c1InsClaimSchedule.Cols["SortClaim"].Index;
                        }
                        else
                        {
                            nInsSortedColumn = e.Col;
                        }
                        oInsSortFlags = e.Order;
                        if (nInsSortedColumn > -1)
                            iClaimNextSelRow = c1InsClaimSchedule.RowSel;
                    }
                    // TFL and DFL Changes
                    foreach (C1.Win.C1FlexGrid.Row rw in c1InsClaimSchedule.Rows)
                    {

                        if (rw.Index > 0 && rw["TFLDays"] != DBNull.Value)
                        {

                            Int64 TFLDays = Convert.ToInt64(c1InsClaimSchedule.GetData(rw.Index, c1InsClaimSchedule.Cols["TFLDays"].Index));

                            if (TFLDays > 0 && TFLDays <= 30)
                            {
                                c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["TFLDays"].Index, "csTFLBeforeDUE");
                            }
                            else if (TFLDays < 0)
                            {
                                c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["TFLDays"].Index, "cs_EditableCurrencyStyle");
                            }
                            else
                            {
                                c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["TFLDays"].Index, "");
                            }
                        }
                        else
                        {
                            c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["TFLDays"].Index, "");
                        }

                        if (rw.Index > 0 && rw["DFLDays"] != DBNull.Value)
                        {

                            Int64 DFLDays = Convert.ToInt64(c1InsClaimSchedule.GetData(rw.Index, c1InsClaimSchedule.Cols["DFLDays"].Index));

                            if (DFLDays > 0 && DFLDays <= 30)
                            {
                                c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["DFLDays"].Index, "csTFLBeforeDUE");
                            }
                            else if (DFLDays < 0)
                            {
                                c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["DFLDays"].Index, "cs_EditableCurrencyStyle");
                            }
                            else
                            {
                                c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["DFLDays"].Index, "");
                            }
                        }
                        else
                        {
                            c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["DFLDays"].Index, "");
                        }
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
                SetToolStripButtonForInsuranceQueue();
            }
        }

        #endregion

        #region "BadDebt Account Queue "

        private void c1BadDebtLogView_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    if (c1BadDebtLogView.MouseRow > 0 && c1BadDebtLogView.MouseCol >= 0)
            //    {
            //        //PAScheduleDrilldown();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            //}

        }

        private void c1BadDebtLogView_MouseMove(object sender, MouseEventArgs e)
        {
            //gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
        }

        private void c1BadDebtLogView_EnterCell(object sender, EventArgs e)
        {
            //try
            //{
            //    if (iBadDebtAccountSelRow != c1BadDebtLogView.RowSel)
            //    {
            //        if (c1BadDebtLogView.Rows.Count > 1)
            //        {
            //            iBadDebtAccountSelRow = c1BadDebtLogView.RowSel;

            //            if (c1BadDebtLogView.RowSel > 0)
            //            {
            //                if (
            //                        c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index) != null && Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index)) != 0
            //                       && c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nAccountID"].Index) != null && Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nAccountID"].Index)) != 0
            //                   )
            //                {
            //                    LoadBadDebtPatientStrip(Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index)), Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nAccountID"].Index)), false);
            //                    oPatientControlBadDebt.Visible = true;
            //                }
            //            }


            //        }
            //        else
            //        {
            //            oPatientControlBadDebt.Visible = false;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //    ex = null;
            //}

        }

        private void c1BadDebtLogView_Click(object sender, EventArgs e)
        {
            //if (c1BadDebtLogView != null)
            //{
            //    if (c1BadDebtLogView.Rows.Count > 1)
            //    {
            //        CurrencyManager cm = (CurrencyManager)BindingContext[this.c1BadDebtLogView.DataSource];
            //        DataRowView dr = cm.Current as DataRowView;
            //        _BadDebtAccountSelRow = dr.Row.Table.Rows.IndexOf(dr.Row);

            //        if (nBadDebtActSortedColumn > -1)
            //            iBadDebtAccountNextSelRow = c1BadDebtLogView.RowSel;
            //    }
            //}
        }

        private void c1BadDebtLogView_AfterSort(object sender, SortColEventArgs e)
        {
            //try
            //{
            //    if (c1BadDebtLogView.Rows.Count > 1)
            //    {
            //        if (c1BadDebtLogView.RowSel > 0)
            //        {
            //            if (
            //                    c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index) != null && Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index)) != 0
            //                   && c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nAccountID"].Index) != null && Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nAccountID"].Index)) != 0
            //               )
            //            {
            //                LoadBadDebtPatientStrip(Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nPatientID"].Index)), Convert.ToInt64(c1BadDebtLogView.GetData(c1BadDebtLogView.RowSel, c1BadDebtLogView.Cols["nAccountID"].Index)), false);
            //                oPatientControlBadDebt.Visible = true;
            //            }
            //        }
            //        //code start-Added by kanchan on 20130611 to maintain selection
            //        if (_BadDebtAccountSelRow > -1)
            //        {
            //            foreach (C1.Win.C1FlexGrid.Row rw in c1BadDebtLogView.Rows)
            //            {
            //                CurrencyManager cm = (CurrencyManager)BindingContext[this.c1BadDebtLogView.DataSource];
            //                DataRowView dr = rw.DataSource as DataRowView;
            //                if (dr != null)
            //                {
            //                    int currIndex = dr.Row.Table.Rows.IndexOf(dr.Row);
            //                    if (currIndex == _BadDebtAccountSelRow)
            //                    {
            //                        CellRange cr = c1BadDebtLogView.GetCellRange(rw.Index, 1);
            //                        // to scroll the selected row in the visible area
            //                        c1BadDebtLogView.Select(cr, true);
            //                        cr = c1BadDebtLogView.GetCellRange(rw.Index, 0, rw.Index, c1BadDebtLogView.Cols.Count - 1);
            //                        c1BadDebtLogView.Select(cr, false);
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //        //code end-Added by kanchan on 20130611 to maintain selection
            //        nBadDebtActSortedColumn = e.Col;
            //        oBadDebtActSortFlags = e.Order;
            //        if (nBadDebtActSortedColumn > -1)
            //            iBadDebtAccountNextSelRow = c1BadDebtLogView.RowSel;
            //    }
            //}
            //catch (Exception ex)
            //{

            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //    ex = null;
            //}
        }

        private void c1BadDebtLogView_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            //try
            //{
            //    Cursor.Current = Cursors.WaitCursor;
            //    c1BadDebtLogView.Redraw = false;
            //    //  c1BadDebtLogView.ResumeLayout(true);
            //    if (e.Row == 0 && e.Col == 0)
            //    {
            //        c1BadDebtLogView.FinishEditing();
            //        c1BadDebtLogView.Select(0, 0);
            //        DataTable dt = (DataTable)c1BadDebtLogView.DataSource;
            //        if (dt != null && dt.Rows.Count > 0)
            //        {
            //            if (c1BadDebtLogView.GetCellCheck(e.Row, e.Col) == C1.Win.C1FlexGrid.CheckEnum.Checked)
            //            {
            //                dt.Select().ToList<DataRow>().ForEach(r => r["bIsTemplate"] = true);
            //            }
            //            else
            //            {
            //                dt.Select().ToList<DataRow>().ForEach(r => r["bIsTemplate"] = false);
            //            }
            //            c1BadDebtLogView.SetDataBinding(dt.Copy(), "", true);
            //            if (dt != null) { dt.Dispose(); dt = null; }

            //        }
            //    }

            //    c1BadDebtLogView.Redraw = true;
            //    Cursor.Current = Cursors.Default;
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //    ex = null;

            //}
        }

        #endregion "BadDebt Account Queue "

        private void btn_ModifyPatient_Click(object sender, EventArgs e)
        {
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {

                if (c1InsClaimSchedule.Rows.Count > 1)
                {
                    if (c1InsClaimSchedule.RowSel > 0)
                    {
                        if (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index) != null && Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index)) != 0)
                        {
                            if (oSecurity.isPatientLock(Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index)), true) == false && Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index)) > 0)
                            {
                                gloPatient.frmSetupPatient ofrmSetupPatient = new gloPatient.frmSetupPatient(Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index)), gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                ofrmSetupPatient.ShowDialog(this);
                                ofrmSetupPatient.Dispose();
                                ofrmSetupPatient = null;
                            }

                            if (c1InsClaimSchedule.RowSel > 0)
                            {

                                if (
                                        (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)) != "") &&
                                        (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index)) != "") &&
                                        (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index)) != "") &&
                                        (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index)) != "")
                                    )
                                {
                                    if ((Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "" || Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)) != ""))
                                    {
                                        Int64 TFLDays = 0;
                                        if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index)) != "")
                                        {
                                            TFLDays = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["TFLDays"].Index));
                                        }
                                        else if (Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)) != "")
                                        { TFLDays = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["DFLDays"].Index)); }


                                        if (TFLDays > 0 && TFLDays <= 30)
                                        {
                                            lblTFLDFLDate.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblFollowUpDt.Font, FontStyle.Bold);
                                            lblTFLDFLDate.ForeColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
                                        }
                                        else if (TFLDays < 0)
                                        {
                                            lblTFLDFLDate.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblFollowUpDt.Font, FontStyle.Bold);
                                            lblTFLDFLDate.ForeColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(148)))));
                                        }
                                        else
                                        {
                                            lblTFLDFLDate.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblFollowUpDt.Font, FontStyle.Bold);
                                            lblTFLDFLDate.ForeColor = Color.Black;
                                        }
                                    }
                                    else
                                    {
                                        lblTflDfl.Visible = false;
                                        lblTFLDFLDate.Visible = false;
                                    }
                                    FillInsuranceClaimTabBanner(Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nPatientID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionContactID"].Index)), Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nAccountID"].Index)));
                                    pnlInsuranceClaimBanner.Visible = true;
                                }

                            }
                        }
                    }
                }
                else
                {
                    pnlInsuranceClaimBanner.Visible = false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSecurity != null)
                    oSecurity.Dispose();
            }
        }

        #endregion

        private void cmbBusinessCenter_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                combo = cmbBusinessCenter;
                if (cmbBusinessCenter.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]), cmbBusinessCenter) >= cmbBusinessCenter.DropDownWidth - 20)
                    {
                        this.toolTip1.SetToolTip(cmbBusinessCenter, Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbBusinessCenter, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbBusinessCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBusinessCenter.Items.Count > 0 && pnlBusinessCenter.Visible == true)
                {
                    nBusinessId = Convert.ToInt64(cmbBusinessCenter.SelectedValue);
                }
                else
                {
                    nBusinessId = 0;
                }
                combo = cmbBusinessCenter;
                if (cmbBusinessCenter.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]), cmbBusinessCenter) >= cmbBusinessCenter.DropDownWidth - 20)
                    {
                        this.toolTip1.SetToolTip(cmbBusinessCenter, Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbBusinessCenter, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbBadDebtBusinessCenter_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                combo = cmbBadDebtBusinessCenter;
                if (cmbBadDebtBusinessCenter.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBadDebtBusinessCenter.Items[cmbBadDebtBusinessCenter.SelectedIndex])["BusinessCenter"]), cmbBadDebtBusinessCenter) >= cmbBadDebtBusinessCenter.DropDownWidth - 20)
                    {
                        this.toolTip1.SetToolTip(cmbBadDebtBusinessCenter, Convert.ToString(((DataRowView)cmbBadDebtBusinessCenter.Items[cmbBadDebtBusinessCenter.SelectedIndex])["BusinessCenter"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbBadDebtBusinessCenter, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbBadDebtBusinessCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (cmbBadDebtBusinessCenter.Items.Count > 0 && pnlBadDebtBusinessCenter.Visible == true)
                //{
                //    nBusinessId = Convert.ToInt64(cmbBusinessCenter.SelectedValue);
                //}
                //else
                //{
                //    nBusinessId = 0;
                //}

                combo = cmbBadDebtBusinessCenter;
                if (cmbBadDebtBusinessCenter.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBadDebtBusinessCenter.Items[cmbBadDebtBusinessCenter.SelectedIndex])["BusinessCenter"]), cmbBadDebtBusinessCenter) >= cmbBadDebtBusinessCenter.DropDownWidth - 20)
                    {
                        this.toolTip1.SetToolTip(cmbBadDebtBusinessCenter, Convert.ToString(((DataRowView)cmbBadDebtBusinessCenter.Items[cmbBadDebtBusinessCenter.SelectedIndex])["BusinessCenter"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbBadDebtBusinessCenter, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbInsBusinessCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbInsBusinessCenter.Items.Count > 0 && pnlInsBusinessCenter.Visible == true)
                {
                    nInsBusinessId = Convert.ToInt64(cmbInsBusinessCenter.SelectedValue);
                }
                else
                {
                    nInsBusinessId = 0;
                }
                combo = cmbInsBusinessCenter;
                if (cmbInsBusinessCenter.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsBusinessCenter.Items[cmbInsBusinessCenter.SelectedIndex])["BusinessCenter"]), cmbInsBusinessCenter) >= cmbInsBusinessCenter.DropDownWidth - 20)
                    {
                        this.toolTip1.SetToolTip(cmbInsBusinessCenter, Convert.ToString(((DataRowView)cmbInsBusinessCenter.Items[cmbInsBusinessCenter.SelectedIndex])["BusinessCenter"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbInsBusinessCenter, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbInsBusinessCenter_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                combo = cmbInsBusinessCenter;
                if (cmbInsBusinessCenter.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsBusinessCenter.Items[cmbInsBusinessCenter.SelectedIndex])["BusinessCenter"]), cmbInsBusinessCenter) >= cmbInsBusinessCenter.DropDownWidth - 20)
                    {
                        this.toolTip1.SetToolTip(cmbInsBusinessCenter, Convert.ToString(((DataRowView)cmbInsBusinessCenter.Items[cmbInsBusinessCenter.SelectedIndex])["BusinessCenter"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbInsBusinessCenter, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_ClaimInsReport_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                frmInsurance_Claim_Followup oInsClmFollowup = new frmInsurance_Claim_Followup();
                oInsClmFollowup.Conn = gloGlobal.gloPMGlobal.DatabaseConnectionString;
                oInsClmFollowup.reportName = "rpt_InsClaimFollowupQueue";
                oInsClmFollowup.reportTitle = "Insurance Claim Follow-up Report";
                oInsClmFollowup.IsgloStreamReport = true;
                Cursor.Current = Cursors.Default;
                oInsClmFollowup.ShowDialog(this);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void SetgloCollectUserDefaulting()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            bool _result = false;
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT ISNULL(bIsGloCollect,'false') FROM dbo.User_MST WHERE nUserID=" + gloGlobal.gloPMGlobal.UserID;
                object result = oDB.ExecuteScalar_Query(strQuery);
                if (result != null)
                {
                    bool.TryParse(result.ToString(), out _result);
                    if (_result == true)
                    {
                        chkAftergloCollect.Checked = true;
                        chkBeforegloCollect.Checked = false;
                    }
                    else
                    {
                        chkBeforegloCollect.Checked = true;
                        chkAftergloCollect.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        private bool IsgloCollectEnable()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            bool _result = false;
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT ISNULL(sSettingsValue,'') FROM dbo.settings WHERE sSettingsName='gloCollectResponsibilityDOS'";
                object result = oDB.ExecuteScalar_Query(strQuery);
                if (result != null)
                {
                    if (result.ToString() != "")
                    {
                        chkAftergloCollect.Visible = true;
                        chkBeforegloCollect.Visible = true;
                        _result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        private void tspTransferClaimBalance_Click(object sender, System.EventArgs e)
        {
            CL_FollowUpCode objCL_FollowUpCode = null;
            String AccountIDS=String.Empty ;
            this.Cursor = Cursors.WaitCursor;
            string _InsTransferCloseDate = "";
            long CollectionAgency = 0;
            try
            {
                 DialogResult oresult=  DialogResult.Cancel;
                if (c1PALogView.Rows.Count >1)
                {
                    if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                    {

                        frmSelectCollectionAgency oCollection = new frmSelectCollectionAgency(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                        if (oCollection.dtCollectionAgency != null && oCollection.dtCollectionAgency.Rows.Count > 1)
                        {
                            oCollection.ShowDialog(this);
                            oresult = oCollection.DialogResult;
                            CollectionAgency = oCollection.ContactId_Collection;
                            oCollection.Dispose();
                            oCollection = null;
                        }
                        else if (oCollection.dtCollectionAgency != null && oCollection.dtCollectionAgency.Rows.Count == 1 )
                        {
                            CollectionAgency = Convert.ToInt64(oCollection.dtCollectionAgency.Rows[0]["nContactId"]);
                            oresult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Setup collection agency to transfer.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                       
                    }
                   

                    if (oresult == DialogResult.OK)
                    {
                        frmInsTransCloseDate ofrmInsTransCloseDate = new frmInsTransCloseDate(gloGlobal.gloPMGlobal.DatabaseConnectionString, Convert.ToString(DateTime.Now.Date));
                        ofrmInsTransCloseDate.CollectionAgency = CollectionAgency;
                        ofrmInsTransCloseDate.ShowDialog(this);
                        if (ofrmInsTransCloseDate.oDialogResult)
                        {
                            _InsTransferCloseDate = ofrmInsTransCloseDate.InsTransferCloseDate;
                            ofrmInsTransCloseDate.Dispose();
                            if (_InsTransferCloseDate != "")
                            {
                                objCL_FollowUpCode = new CL_FollowUpCode();
                                int nSelectCount = 0;
                                nSelectCount = c1PALogView.FindRow("True", 1, 0, true);

                                if (nSelectCount >= 1)
                                {
                                    DataTable dt = (DataTable)c1PALogView.DataSource;

                                    if (dt != null && dt.Rows.Count > 0)
                                    {
                                        AccountIDS = dt.AsEnumerable()
                                              .Where(r => Convert.ToBoolean(r["bIsTemplate"]) == true)
                                              .Select(row => row["nAccountID"].ToString())
                                              .Aggregate((s1, s2) => String.Concat(s1, "," + s2));

                                        if (AccountIDS != "" && AccountIDS.Length > 0)
                                            objCL_FollowUpCode.TransferClaimBalanceToExternalCollectionAgency(AccountIDS, Convert.ToDateTime(_InsTransferCloseDate), CollectionAgency);
                                    }
                                }
                                else
                                {
                                    if (c1PALogView.RowSel > 0)
                                    {
                                        AccountIDS = Convert.ToString(c1PALogView.Rows[c1PALogView.RowSel]["nAccountID"]);
                                        objCL_FollowUpCode.TransferClaimBalanceToExternalCollectionAgency(AccountIDS, Convert.ToDateTime(_InsTransferCloseDate), CollectionAgency);
                                    }

                                }
                                LoadPatientAccountQueue();
                            }
                                                  }
                        else
                        {
                            ofrmInsTransCloseDate.Dispose();

                        }
                        
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (objCL_FollowUpCode != null)
                {
                    objCL_FollowUpCode.Dispose(); objCL_FollowUpCode = null;
                }
                AccountIDS = String.Empty;
                this.Cursor = Cursors.Default;
            }
            
        }

        private string getExternalCollectionFUAction()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            string _result = String.Empty;
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT ISNULL(sSettingsValue,'') FROM dbo.settings WHERE sSettingsName='ExternalCollectionFUAction' AND "
                    + "(SELECT Convert(Bit,ISNULL(sSettingsValue,'')) FROM dbo.settings WHERE sSettingsName='ExternalCollectionfeature') = 1 ";
                object result = oDB.ExecuteScalar_Query(strQuery);
                if (result != null)
                {
                    if (result.ToString() != "")
                    {
                        _result = Convert.ToString(result).Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = String.Empty;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }

        private void BillPendingNoBillClaim()
        {
            Int64 _transactionId = 0;
            Int64 _mastertrnId = 0;
            TransactionStatus _transactionStatus = TransactionStatus.None;
            Int64 _recordUserId = 0;
            string _claimNo = "";
            string _recordMachineId = "";
            string _claimResponsibilityChangeCloseDate = "";
            Int64 _patientInsuranceId = 0;

            try
            {
                if (c1InsClaimSchedule != null && c1InsClaimSchedule.Rows.Count > 1)
                {
                    if (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)) != "")
                    {
                        _transactionId = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index));
                        _mastertrnId = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index));
                        _transactionStatus = (TransactionStatus)(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nClaimStatus"].Index));
                        _claimNo = Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["sClaimNo"].Index));
                        _patientInsuranceId = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nNextActionPatientInsID"].Index));

                        if (_mastertrnId > 0 && _transactionId > 0 && _transactionStatus == TransactionStatus.Pending && _patientInsuranceId > 0)
                        {
                            //Check if claim was already billed for selected/pending insurance party
                            if (gloCharges.IsClaimBilled(_mastertrnId, _patientInsuranceId) == false)
                            {
                                if (DialogResult.Yes == MessageBox.Show("Claim will be billed for pending insurance. \nContinue? ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                                {
                                    gloBilling ogloBilling = new gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloGlobal.gloPMGlobal.DatabaseConnectionString);

                                    //Check claim lock status, if locked for modify by another user
                                    if (ogloBilling.IsRecordOpen(_mastertrnId, out _recordMachineId, out _recordUserId) == false)
                                    {
                                        //lock for current user and update
                                        ogloBilling.UpdateRecordStatus(_mastertrnId, 0, true);

                                        //Re-verify if claim is voided may be by another user, another machine
                                        if (gloCharges.IsClaimVoided(_mastertrnId) == false)
                                        {
                                            //Re-verify if claim is updated may be by another user, another machine
                                            if (gloCharges.IsClaimSplitted(_mastertrnId, _transactionId) == false)
                                            {
                                                //get login user last close date
                                                _claimResponsibilityChangeCloseDate = GetLoginUserLastCloseDate();

                                                frmInsTransCloseDate ofrmInsTransCloseDate = new frmInsTransCloseDate(gloGlobal.gloPMGlobal.DatabaseConnectionString, _transactionId, _mastertrnId, _claimResponsibilityChangeCloseDate);
                                                ofrmInsTransCloseDate.ShowDialog(this);

                                                if (ofrmInsTransCloseDate.oDialogResult)
                                                {
                                                    string _claimRespTransferDate = "";
                                                    _claimRespTransferDate = ofrmInsTransCloseDate.InsTransferCloseDate;
                                                    ofrmInsTransCloseDate.Dispose();

                                                    using (gloPendingClaimBilling oPendingClaimBilling = new gloPendingClaimBilling())
                                                    {
                                                        oPendingClaimBilling.BillPendingClaim(_transactionId, _mastertrnId, gloDateMaster.gloDate.DateAsNumber(_claimRespTransferDate));
                                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.BillPendingClaim, "Pending Claim#" + _claimNo + " was billed again", 0, _transactionId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                                        LoadInsuranceClaimQueue();
                                                    }

                                                    _claimRespTransferDate = null;
                                                }
                                                else
                                                { ofrmInsTransCloseDate.Dispose(); }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Claim selected for billing is updated by another user. Refresh follow-up queue for current claim status.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Claim is already marked voided. Refresh follow-up queue for current claim status.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("Transaction is already opened for modify on machine " + _recordMachineId + "", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    if (ogloBilling != null)
                                    {
                                        ogloBilling.Dispose();
                                        ogloBilling = null;
                                    }
                                }
                            }//END - If claim is billed already
                            else
                            {
                                MessageBox.Show("Claim has already been billed to pending insurance.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                gloCharges.ReleaseLockStatus(_mastertrnId);
            }
        }

        private void SetToolStripButtonForInsuranceQueue()
        {
            try
            {
                if (cmbClaimFollowupAction.SelectedValue.ToString() == "" && c1InsClaimSchedule.Rows.Count > 1)
                {
                    if (c1InsClaimSchedule != null && c1InsClaimSchedule.Rows.Count > 1 && c1InsClaimSchedule.RowSel > 0)
                    {
                        if (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index) != null && Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)) != "")
                        {
                            if (((TransactionStatus)(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nClaimStatus"].Index))) == TransactionStatus.Pending)
                            {
                                tsb_ClaimResend.Enabled = false;
                                tsb_BillPendingClaim.Enabled = true;
                            }
                            else
                            {
                                tsb_ClaimResend.Enabled = true;
                                tsb_BillPendingClaim.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        tsb_ClaimResend.Enabled = false;
                        tsb_BillPendingClaim.Enabled = false;
                    } 
                }
                else
                {
                    tsb_ClaimResend.Enabled = true;
                    tsb_BillPendingClaim.Enabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private string GetLoginUserLastCloseDate()
        {
            string _lastCloseDate = "";

            try
            {
                DataSet _dsUserData = null;
                _dsUserData = gloCharges.GetLoginUserChangeData();

                if (_dsUserData != null && _dsUserData.Tables != null && _dsUserData.Tables.Count > 0)
                {
                    if (_dsUserData.Tables[1] != null && _dsUserData.Tables[1].Rows.Count > 0)
                    {
                        if (_dsUserData.Tables[1].Rows[0]["nCloseDayDate"] != DBNull.Value && Convert.ToString(_dsUserData.Tables[1].Rows[0]["nCloseDayDate"]).Trim() != ""
                            && Convert.ToInt64(_dsUserData.Tables[1].Rows[0]["nCloseDayDate"]) > 0)
                        {
                            _lastCloseDate = gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(_dsUserData.Tables[1].Rows[0]["nCloseDayDate"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                _lastCloseDate = "";
            }

            return _lastCloseDate;
        }


        private void btn_ModityPatient_MouseHover(object sender, EventArgs e)
        {
            btn_ModifyPatient.BackgroundImage = global::gloBilling.Properties.Resources.PatientHover;
            btn_ModifyPatient.BackgroundImageLayout = ImageLayout.Center; 
            if (toolTip1 == null)
            {
                toolTip1 = new ToolTip();
            }
            toolTip1.SetToolTip(btn_ModifyPatient , "Modify Patient");
        }

        private void btn_ModityPatient_MouseLeave(object sender, EventArgs e)
        {
            btn_ModifyPatient.BackgroundImage = global::gloBilling.Properties.Resources.Patient;
            btn_ModifyPatient.BackgroundImageLayout = ImageLayout.Center;
        }

        private void tspPatientPayment_Click(object sender, EventArgs e)
        {
            LoadPatientPaymentScreen(CollectionEnums.FollowUpType.PatientAccount);

            //if (c1PALogView.Rows.Count >1)
            //{
            //    //Code to open patient Paymeny from Dashboard
            //    gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(Convert.ToInt64(c1PALogView.GetData(c1PALogView.RowSel, c1PALogView.Cols["nPatientID"].Index)), false, 0, 0, 0, 0, EOBPaymentSubType.Other);
            //    frmPatientPaymentV2.StartPosition = FormStartPosition.CenterScreen;
            //    frmPatientPaymentV2.WindowState = FormWindowState.Maximized;
            //    frmPatientPaymentV2.ShowInTaskbar = false;
            //    frmPatientPaymentV2.ShowDialog(this);
            //    frmPatientPaymentV2.Dispose();
            //}
        }

        private void c1InsClaimSchedule_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                c1InsClaimSchedule.Redraw = false;
                if (e.Row == 0 && e.Col == 0)
                {
                    c1InsClaimSchedule.FinishEditing();
                    c1InsClaimSchedule.Select(0, 0);
                    DataTable dt = (DataTable)c1InsClaimSchedule.DataSource;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (c1InsClaimSchedule.GetCellCheck(e.Row, e.Col) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            dt.Select().ToList<DataRow>().ForEach(r => r["bIsTemplate"] = true);
                        }
                        else
                        {
                            dt.Select().ToList<DataRow>().ForEach(r => r["bIsTemplate"] = false);
                        }
                        c1InsClaimSchedule.DataSource = dt.Copy();
                        if (dt != null) { dt.Dispose(); dt = null; }
                    }
                }

                foreach (C1.Win.C1FlexGrid.Row rw in c1InsClaimSchedule.Rows)
                {

                    if (rw.Index > 0 && rw["TFLDays"] != DBNull.Value)
                    {

                        Int64 TFLDays = Convert.ToInt64(c1InsClaimSchedule.GetData(rw.Index, c1InsClaimSchedule.Cols["TFLDays"].Index));

                        if (TFLDays > 0 && TFLDays <= 30)
                        {
                            c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["TFLDays"].Index, "csTFLBeforeDUE");
                        }
                        else if (TFLDays < 0)
                        {
                            c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["TFLDays"].Index, "cs_EditableCurrencyStyle");
                        }
                        else
                        {
                            c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["TFLDays"].Index, "");
                        }
                    }

                    if (rw.Index > 0 && rw["DFLDays"] != DBNull.Value)
                    {

                        Int64 DFLDays = Convert.ToInt64(c1InsClaimSchedule.GetData(rw.Index, c1InsClaimSchedule.Cols["DFLDays"].Index));

                        if (DFLDays > 0 && DFLDays <= 30)
                        {
                            c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["DFLDays"].Index, "csTFLBeforeDUE");
                        }
                        else if (DFLDays < 0)
                        {
                            c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["DFLDays"].Index, "cs_EditableCurrencyStyle");
                        }
                        else
                        {
                            c1InsClaimSchedule.SetCellStyle(rw.Index, c1InsClaimSchedule.Cols["DFLDays"].Index, "");
                        }
                    }
                }
                c1InsClaimSchedule.Redraw = true;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
        }

        private void c1PALogView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (c1PALogView.Rows.Count >= 1)
                {
                    Int32 tempRow = 0;
                    tempRow = c1PALogView.HitTest(e.X, e.Y).Row;
                    if (tempRow == -1)
                    {
                        c1PALogView.ContextMenuStrip = null;
                        return;
                    }
                    c1PALogView.Row = tempRow;
                    if (e.Button == MouseButtons.Right)
                    {
                        cmnuAccountMenu_Action.Click -= new EventHandler(cmnuAccountMenu_Action_Click);
                        c1PALogView.Row = tempRow;
                        c1PALogView.ContextMenuStrip = cmnuAccountMenu;

                        cmnuAccountMenu_Action.Click += new EventHandler(cmnuAccountMenu_Action_Click);
                    }
                    else
                    {
                        c1PALogView.ContextMenuStrip = null;
                    }
                }
                else
                {
                    c1PALogView.ContextMenuStrip = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        void cmnuAccountMenu_Action_Click(object sender, EventArgs e)
        {
            PAScheduleDrilldown();
        }

        private void c1InsClaimSchedule_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (c1InsClaimSchedule.Rows.Count >= 1)
                {
                    Int32 tempRow = 0;
                    tempRow = c1InsClaimSchedule.HitTest(e.X, e.Y).Row;
                    if (tempRow == -1)
                    {
                        c1InsClaimSchedule.ContextMenuStrip = null;
                        return;
                    }
                    c1InsClaimSchedule.Row = tempRow;
                    if (e.Button == MouseButtons.Right)
                    {
                        cmnuClaimMenu_Action.Click -= new EventHandler(cmnuClaimMenu_Action_Click);
                        c1InsClaimSchedule.Row = tempRow;
                        c1InsClaimSchedule.ContextMenuStrip = cmnuClaimMenu;

                        cmnuClaimMenu_Action.Click += new EventHandler(cmnuClaimMenu_Action_Click);
                    }
                    else
                    {
                        c1InsClaimSchedule.ContextMenuStrip = null;
                    }
                }
                else
                {
                    c1InsClaimSchedule.ContextMenuStrip = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        void cmnuClaimMenu_Action_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1InsClaimSchedule.Rows.Count > 1)
                {
                    InsClaimScheduleDrilldown();
                    LoadInsuranceClaimQueue();
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_ClaimTransferToSelf_Click(object sender, EventArgs e)
        {
            CL_FollowUpCode oCollection = null;
            string _InsTransferCloseDate = string.Empty;
            string nTrackTransactionID = string.Empty;
            string nBillingTransactionID = string.Empty;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (c1InsClaimSchedule.Rows.Count > 1)
                {
                   // if (c1InsClaimSchedule.RowSel > 0)
                    {
                        int nSelectedCount = 0;
                        nSelectedCount = c1InsClaimSchedule.FindRow("True", 1, 0, true);
                        if (nSelectedCount <= 0)
                        {
                            nTrackTransactionID = Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index));
                            nBillingTransactionID = Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index));
                        }
                        else
                        {
                            DataTable dt = (DataTable)c1InsClaimSchedule.DataSource;
                            nTrackTransactionID = dt.AsEnumerable()
                                                  .Where(r => Convert.ToBoolean(r["bIsTemplate"]) == true)
                                                  .Select(row => row["nTransactionID"].ToString())
                                                  .Aggregate((s1, s2) => String.Concat(s1, "," + s2));
                            nBillingTransactionID = dt.AsEnumerable()
                                                    .Where(r => Convert.ToBoolean(r["bIsTemplate"]) == true)
                                                  .Select(row => row["nTransactionMasterID"].ToString())
                                                  .Aggregate((s1, s2) => String.Concat(s1, "," + s2));
                        }
                        DataTable dtStatus = new DataTable();
                        if (nBillingTransactionID != "" && nTrackTransactionID != "")
                        {
                            frmInsTransCloseDate ofrmInsTransCloseDate = new frmInsTransCloseDate(gloGlobal.gloPMGlobal.DatabaseConnectionString, nTrackTransactionID, nBillingTransactionID, Convert.ToString(DateTime.Now.Date));
                            ofrmInsTransCloseDate.ShowDialog(this);
                            if (ofrmInsTransCloseDate.oDialogResult)
                            {
                                _InsTransferCloseDate = ofrmInsTransCloseDate.InsTransferCloseDate;
                                if (_InsTransferCloseDate != "")
                                {
                                    oCollection = new CL_FollowUpCode();
                                    oCollection.TransferClaimBalanceToSelf(nBillingTransactionID, nTrackTransactionID, Convert.ToDateTime(_InsTransferCloseDate), out dtStatus);
                                }
                            }
                            ofrmInsTransCloseDate.Dispose();
                        }
                        //added condition to not show status from if "Transfer to self" of claims is not perform.
                        if (_InsTransferCloseDate != "")
                        {
                            if (dtStatus != null && dtStatus.Rows.Count > 0)
                            {
                                if (dtStatus.Rows.Count > 0)
                                {
                                    frmBatchFollowUpStatus ofrmBatchFollowUpStatus = new frmBatchFollowUpStatus();
                                    ofrmBatchFollowUpStatus.dtFollowUpStatus = dtStatus;
                                    ofrmBatchFollowUpStatus.CalledFrom = "TransferToSelf";
                                    ofrmBatchFollowUpStatus.ShowDialog(this);
                                    ofrmBatchFollowUpStatus.Dispose();
                                }
                            }
                        }
                        LoadInsuranceClaimQueue();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oCollection!=null)
                {
                    oCollection.Dispose();
                    oCollection = null;
                }
                _InsTransferCloseDate = string.Empty;
                nTrackTransactionID = string.Empty;
                nBillingTransactionID = string.Empty;
                this.Cursor = Cursors.Default;
            }
        }

        private void tsb_ClaimRebill_Click(object sender, EventArgs e)
        {
            CL_FollowUpCode oCollection = null;
            string _InsTransferCloseDate = string.Empty;
            string nTrackTransactionID = string.Empty;
            string nBillingTransactionID = string.Empty;
            this.Cursor = Cursors.WaitCursor;
            DataTable dtClaimRemitanceRefNo = null;
            try
            {
                if (c1InsClaimSchedule.Rows.Count > 1)
                {
                    //if (c1InsClaimSchedule.RowSel > 0)
                    {
                        int nSelectedCount = 0;
                        nSelectedCount = c1InsClaimSchedule.FindRow("True", 1, 0, true);
                        if (nSelectedCount <= 0)
                        {
                            nTrackTransactionID = Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index));
                            nBillingTransactionID = Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index));
                        }
                        else
                        {
                            DataTable dt = (DataTable)c1InsClaimSchedule.DataSource;
                            nTrackTransactionID = dt.AsEnumerable()
                                                  .Where(r => Convert.ToBoolean(r["bIsTemplate"]) == true)
                                                  .Select(row => row["nTransactionID"].ToString())
                                                  .Aggregate((s1, s2) => String.Concat(s1, "," + s2));
                            nBillingTransactionID = dt.AsEnumerable()
                                                    .Where(r => Convert.ToBoolean(r["bIsTemplate"]) == true)
                                                  .Select(row => row["nTransactionMasterID"].ToString())
                                                  .Aggregate((s1, s2) => String.Concat(s1, "," + s2));
                        }
                        if (nSelectedCount > 0)
                        {
                            DataTable dt = (DataTable)c1InsClaimSchedule.DataSource;

                            List<DataRow> dRow = (from myrow in dt.AsEnumerable()
                                                  where Convert.ToBoolean(myrow.Field<dynamic>("bIsTemplate")) == true
                                                  select myrow).ToList<DataRow>();


                            if (dRow != null && dRow.Count > 0)
                            {
                                dtClaimRemitanceRefNo = dRow.CopyToDataTable().DefaultView.ToTable(false, "nTransactionMasterID", "nTransactionID", "nNextActionPatientInsID", "nNextActionContactID", "sInsuranceName", "sPatientName", "sClaimNo", "ClaimRemittanceRefNo");
                                if (dtClaimRemitanceRefNo != null && dtClaimRemitanceRefNo.Rows.Count > 0)
                                {
                                    if (dtClaimRemitanceRefNo.Rows.Count > 0)
                                    {

                                        frmBatchFollowUpStatus ofrmBatchFollowUpStatus = new frmBatchFollowUpStatus();

                                        dtClaimRemitanceRefNo.Columns["sInsuranceName"].ColumnName = "Insurance Plan";
                                        dtClaimRemitanceRefNo.Columns["sPatientName"].ColumnName = "Patient";
                                        dtClaimRemitanceRefNo.Columns["sClaimNo"].ColumnName = "Claim#";
                                        dtClaimRemitanceRefNo.Columns["ClaimRemittanceRefNo"].ColumnName = "Claim Remittance Ref #";
                                        dtClaimRemitanceRefNo.DefaultView.Sort = "Claim Remittance Ref #"; 

                                        ofrmBatchFollowUpStatus.dtFollowUpStatus = dtClaimRemitanceRefNo.DefaultView.ToTable(true);
                                        DialogResult _dialogResult;
                                        string msgBrokenrule = "Rebilling normally requires a Claim Remittance Ref #, but none has been entered.\nEnter a Claim Remittance Ref # now?";

                                        _dialogResult = MessageBox.Show(msgBrokenrule, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                        switch (_dialogResult)
                                        {
                                            case DialogResult.Yes:
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.Yes, msgBrokenrule, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                                break;
                                            case DialogResult.No:
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.No, msgBrokenrule, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                                break;
                                            case DialogResult.Cancel:
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.Cancle, msgBrokenrule, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                                break;
                                            default:
                                                break;
                                        }
                                        if (_dialogResult == DialogResult.Yes)
                                        {
                                            ofrmBatchFollowUpStatus.dtClaimRemittanceInfo = dtClaimRemitanceRefNo;
                                            ofrmBatchFollowUpStatus.CalledFrom = "RebillRemitanceNo";
                                            ofrmBatchFollowUpStatus.ShowDialog(this);
                                            dtClaimRemitanceRefNo = ofrmBatchFollowUpStatus.dtClaimRemittanceInfo;
                                        }

                                        if (dtClaimRemitanceRefNo != null && dtClaimRemitanceRefNo.Rows.Count > 0)
                                        {
                                            dtClaimRemitanceRefNo.Columns.Remove("Insurance Plan");
                                            dtClaimRemitanceRefNo.Columns.Remove("Patient");
                                            dtClaimRemitanceRefNo.Columns.Remove("Claim#");
                                        }
                                        ofrmBatchFollowUpStatus.Dispose();
                                    }
                                }
                            }
                        }
                        else
                        {
                            DataTable dt = (DataTable)c1InsClaimSchedule.DataSource;

                            List<DataRow> dRow = (from myrow in dt.AsEnumerable()
                                                  where Convert.ToInt64(myrow.Field<dynamic>("nTransactionID")) == Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, "nTransactionID"))
                                                  select myrow).ToList<DataRow>();


                            if (dRow != null && dRow.Count > 0)
                            {
                                dtClaimRemitanceRefNo = dRow.CopyToDataTable().DefaultView.ToTable(false, "nTransactionMasterID", "nTransactionID", "nNextActionPatientInsID", "nNextActionContactID", "sInsuranceName", "sPatientName", "sClaimNo", "ClaimRemittanceRefNo");
                                if (dtClaimRemitanceRefNo != null && dtClaimRemitanceRefNo.Rows.Count > 0)
                                {
                                    if (dtClaimRemitanceRefNo.Rows.Count > 0)
                                    {

                                        frmBatchFollowUpStatus ofrmBatchFollowUpStatus = new frmBatchFollowUpStatus();

                                        dtClaimRemitanceRefNo.Columns["sInsuranceName"].ColumnName = "Insurance Plan";
                                        dtClaimRemitanceRefNo.Columns["sPatientName"].ColumnName = "Patient";
                                        dtClaimRemitanceRefNo.Columns["sClaimNo"].ColumnName = "Claim#";
                                        dtClaimRemitanceRefNo.Columns["ClaimRemittanceRefNo"].ColumnName = "Claim Remittance Ref#";
                                        dtClaimRemitanceRefNo.DefaultView.Sort = "Claim Remittance Ref#";

                                        ofrmBatchFollowUpStatus.dtFollowUpStatus = dtClaimRemitanceRefNo.DefaultView.ToTable(true);
                                        DialogResult _dialogResult;
                                        string msgBrokenrule = "Rebilling normally requires a Claim Remittance Ref #, but none has been entered.\nEnter a Claim Remittance Ref # now?";

                                        _dialogResult = MessageBox.Show(msgBrokenrule, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                        switch (_dialogResult)
                                        {
                                            case DialogResult.Yes:
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.Yes, msgBrokenrule, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                                break;
                                            case DialogResult.No:
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.No, msgBrokenrule, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                                break;
                                            case DialogResult.Cancel:
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.Cancle, msgBrokenrule, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                                break;
                                            default:
                                                break;
                                        }
                                        if (_dialogResult == DialogResult.Yes)
                                        {
                                            ofrmBatchFollowUpStatus.dtClaimRemittanceInfo = dtClaimRemitanceRefNo;
                                            ofrmBatchFollowUpStatus.CalledFrom = "RebillRemitanceNo";
                                            ofrmBatchFollowUpStatus.ShowDialog(this);
                                            dtClaimRemitanceRefNo = ofrmBatchFollowUpStatus.dtClaimRemittanceInfo;
                                        }

                                        if (dtClaimRemitanceRefNo != null && dtClaimRemitanceRefNo.Rows.Count > 0)
                                        {
                                            dtClaimRemitanceRefNo.Columns.Remove("Insurance Plan");
                                            dtClaimRemitanceRefNo.Columns.Remove("Patient");
                                            dtClaimRemitanceRefNo.Columns.Remove("Claim#");
                                        }
                                        ofrmBatchFollowUpStatus.Dispose();
                                    }
                                }
                            }
                        }
                        DataTable dtStatus = new DataTable();
                        if (nBillingTransactionID != "" && nTrackTransactionID != "")
                        {
                            frmInsTransCloseDate ofrmInsTransCloseDate = new frmInsTransCloseDate(gloGlobal.gloPMGlobal.DatabaseConnectionString, nTrackTransactionID, nBillingTransactionID, Convert.ToString(DateTime.Now.Date));
                            ofrmInsTransCloseDate.ShowDialog(this);
                            if (ofrmInsTransCloseDate.oDialogResult)
                            {
                                _InsTransferCloseDate = ofrmInsTransCloseDate.InsTransferCloseDate;
                                if (_InsTransferCloseDate != "")
                                {
                                    oCollection = new CL_FollowUpCode();
                                    oCollection.RebillMultipleClaims(nBillingTransactionID, nTrackTransactionID, Convert.ToDateTime(_InsTransferCloseDate), out dtStatus, dtClaimRemitanceRefNo);
                                }
                            }
                            ofrmInsTransCloseDate.Dispose();
                        }
                        //added condition to not show status form if Rebill of claims is not perform.
                        if (_InsTransferCloseDate != "")
                        {
                            if (dtStatus != null && dtStatus.Rows.Count > 0)
                            {
                                if (dtStatus.Rows.Count > 0)
                                {
                                    frmBatchFollowUpStatus ofrmBatchFollowUpStatus = new frmBatchFollowUpStatus();
                                    ofrmBatchFollowUpStatus.dtFollowUpStatus = dtStatus;
                                    ofrmBatchFollowUpStatus.CalledFrom = "Rebill";
                                    ofrmBatchFollowUpStatus.ShowDialog(this);
                                    ofrmBatchFollowUpStatus.Dispose();
                                }
                            }
                        }
                        LoadInsuranceClaimQueue();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oCollection!=null)
                {
                    oCollection.Dispose();
                    oCollection = null;
                }
                _InsTransferCloseDate = string.Empty;
                nTrackTransactionID = string.Empty;
                nBillingTransactionID = string.Empty;
                this.Cursor = Cursors.Default;
            }
        }

        private DataTable DeleteExtraColumns(DataTable dtInput)
        {
            DataTable dt = new DataTable();
            dt = dtInput;
            if (dt!=null)
            {
                dt.Columns.Remove("Tfl_Dfl_Date");
                dt.Columns.Remove("TotalBalanceAmount");
                dt.Columns.Remove("DFLDays");
                dt.Columns.Remove("TFLDays");
                dt.Columns.Remove("BusinessCenter");
                dt.Columns.Remove("bIsTemplate");
                dt.Columns.Remove("dtCreatedDateTime");
                dt.Columns.Remove("ClaimDOS");
                dt.Columns.Remove("sInsuranceCompany");
                dt.Columns.Remove("sProviderName");
                dt.Columns.Remove("SortClaim");
                dt.Columns.Remove("SortSubClaim");
                dt.Columns.Remove("nClaimStatus");
                dt.Columns.Remove("bIsVoid");
                dt.Columns.Remove("nNextActionPatientInsID");
                dt.Columns.Remove("nNextActionContactID");
                dt.Columns.Remove("nPatientID");
                dt.Columns.Remove("nCompanyId");
                dt.Columns.Remove("nTransactionMasterID");
                dt.Columns.Remove("nTransactionID");
                dt.Columns.Remove("nAccountID");
                dt.Columns.Remove("nNextScheduleID");
                dt.Columns.Remove("dtClaimFollowUpDate");
                dt.Columns.Remove("sNextScheduleDescription");
                dt.Columns.Remove("ClaimRemittanceRefNo");

                dt.Columns["sInsuranceName"].SetOrdinal(0);
                dt.Columns["sPatientName"].SetOrdinal(1);
                dt.Columns["sClaimNo"].SetOrdinal(2);

                dt.Columns["sInsuranceName"].ColumnName = "Insurance Plan";
                dt.Columns["sPatientName"].ColumnName = "Patient";
                dt.Columns["sClaimNo"].ColumnName = "Claim#";
            }
            
            return dt;
        }

        private void c1PALogView_BeforeSelChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (c1PALogView != null)
                {
                    if (e.NewRange.TopRow == 0)
                    { e.Cancel = true; }
                }
            }
            catch (Exception)
            {
                //No need to log the exception
            }
        }

        private void c1InsClaimSchedule_BeforeSelChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (c1InsClaimSchedule != null)
                {
                    if (e.NewRange.TopRow == 0)
                    { e.Cancel = true; }
                }
            }
            catch (Exception)
            {
                //No need to log the exception
            }
        }

        private void c1BadDebtLogView_BeforeSelChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (c1BadDebtLogView != null)
                {
                    if (e.NewRange.TopRow == 0)
                    { e.Cancel = true; }
                }
            }
            catch (Exception)
            {
                //No need to log the exception
            }
        }

        private void tsb_ClaimStatus_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GetClaimStatus();
            this.Cursor = Cursors.Default;
        }



        private void GetClaimStatus()
        {
            string ClaimNumber = "";
            long TrnMasterId = 0;
            long TransactionId = 0;
            //string RequestString = "";
            //string ResponseString = "";
            string ResponseError = "";
            string StatusCategoryCode = "";
            string StatusCategoryCodeDesc = "";
            string StatusCode = "";
            string StatusCodeDesc = "";
            //string StatusEffectiveDate = null;
            string StatusMessge = "";
            string PayerId = "";
            string PayerName = "";
            //long RequestFileId = 0;
            //long ResponseFileId = 0;
            //long RequestId = 0;
            //long ResponseId = 0;
            string RequestFilePath = "";
            string ResponseFilePath = "";

            List<gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo> claimStatusInfo = null;

            if (c1InsClaimSchedule.RowSel >= 0)
            {
                if (c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index) != null &&
                    Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index)) != "")
                {
                    TrnMasterId = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionMasterID"].Index));
                    TransactionId = Convert.ToInt64(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["nTransactionID"].Index));
                    ClaimNumber = Convert.ToString(c1InsClaimSchedule.GetData(c1InsClaimSchedule.RowSel, c1InsClaimSchedule.Cols["sClaimNo"].Index));
                    Int64 nContactID, nInsuranceID;
                    GetContactInsuranceID(TransactionId, out nContactID, out nInsuranceID);
                  
                    TriArqEDIRealTimeClaimStatus.clsRealTimeClaimStatus oRTCS = new TriArqEDIRealTimeClaimStatus.clsRealTimeClaimStatus();
                    var ClaimStatus = oRTCS.DoRealTimeCSI(ClaimNumber, gloGlobal.gloPMGlobal.DatabaseConnectionString, TransactionId, gloGlobal.gloPMGlobal.ClinicID, TrnMasterId);
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
                        //RequestId = ClaimStatus.RequestId;
                        //ResponseId = ClaimStatus.ResponseId;
                        //RequestFileId = ClaimStatus.RequestFileId;
                        //ResponseFileId = ClaimStatus.ResponseFileId;
                        //RequestString = ClaimStatus.RequestString;
                        //ResponseString = ClaimStatus.ResponseString;
                        //StatusEffectiveDate = ClaimStatus.StatusEffectiveDate;
                        
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

                        ////claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, ResponseError, gloUIControlLibrary.Classes.ClaimStatus.MessageType.Error));
                        //StatusMessge = "72010 HCPCS Procedure Code is invalid in Professional Service. Value of sub-element SV101-02 is incorrect. Expected value is from external code list - HCPCS Code (130) when SV101-01='HC'. (72010) Rejected. Syntax error noted for this claim/service/inquiry. See Functional or Implementation Acknowledgement for details. (Note: Only for use to reject claims or status requests in transactions that were 'accepted with errors' on a 997 or 999 Acknowledgement.)";
                        //StatusCategoryCode = "A7";
                        //StatusCategoryCodeDesc = "Acknowledgement/Rejected for Invalid Information - The claim/encounter has invalid information as specified in the Status details and has been rejected.";
                        //StatusCode = "684";
                        //StatusCodeDesc = "Rejected. Syntax error noted for this claim/service/inquiry. See Functional or Implementation Acknowledgement for details. (Note: Only for use to reject claims or status requests in transactions that were 'accepted with errors' on a 997 or 999 Acknowledgement.) Rejected. Syntax error noted for this claim/service/inquiry. See Functional or Implementation Acknowledgement for details. (Note: Only for use to reject claims or status requests in transactions that were 'accepted with errors' on a 997 or 999 Acknowledgement.) ";
                        //claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, StatusMessge, gloUIControlLibrary.Classes.ClaimStatus.MessageType.Message));
                        //claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, StatusCategoryCode + " : " + StatusCategoryCodeDesc, gloUIControlLibrary.Classes.ClaimStatus.MessageType.StatusCategory));
                        //claimStatusInfo.Add(new gloUIControlLibrary.Classes.ClaimStatus.ClaimStatusInfo(PayerId, PayerName, ClaimNumber, StatusCode + " : " + StatusCodeDesc, gloUIControlLibrary.Classes.ClaimStatus.MessageType.Status));

                        Boolean dlgResult = true;
                        if (claimStatusInfo.Count > 0)
                        {
                            gloUIControlLibrary.WPFForms.frmClaimStatus frmClaimStatus = new gloUIControlLibrary.WPFForms.frmClaimStatus(claimStatusInfo);
                            System.Windows.Interop.WindowInteropHelper _interophelper = new System.Windows.Interop.WindowInteropHelper(frmClaimStatus);
                            _interophelper.Owner = this.Handle;
                            frmClaimStatus.ShowDialog();
                            dlgResult = Convert.ToBoolean(frmClaimStatus.DialogResult);
                        }

                        //if (dlgResult)
                        //{
                        //    AddRealTimeClaimStatus(TrnMasterId, ClaimNumber, RequestString, ResponseString, ResponseError, StatusCategoryCode, StatusCategoryCodeDesc, StatusCode, StatusCodeDesc, StatusEffectiveDate);
                        //}
                    }
                }
            }
        }

      

     
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
