using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloAccountsV2;

namespace gloBilling
{
    public enum enumCheckStatus
    {
        None = 0,
        Ready = 1,
        Posted = 2,
        MarkedDeleted = 3,
        InProcess = 4,
        Hold = 5
    }
    public enum enumPaymentStatus
    {
        Pending = 1,
        Posted = 2
    }

    public partial class frmClearGagePaymentPosting : Form
    {
        private const int COL_CellImage = 0;
        private const int COL_CleargageFileID = 1;
        private const int COL_FileName = 2;
        private const int COL_OriginalFileName = 3;
       
        private const int COL_Importdate = 4;
        private const int COL_Status =5;
        private const int COL_IsBlocked =6;
        private const int COL_COUNT = 7;
        

        #region " Tab Constants "
        private const string TAB_READY = "ReadyToPost";
        private const string TAB_DELETED = "Deleted";
        private const string TAB_POSTED = "Posted";
        private const string TAB_HOLD = "Hold";
        #endregion

        #region c1ReadyToPost Grid
        private const int Col_CleargageFileID = 0;
        private const int Col_PAccountID = 1;
        private const int Col_PatientID=2;
        private const int Col_PatientName=3;
        private const int Col_OriginalFileName=4;
        private const int Col_ImportDate=5;
        private const int Col_Amount=6;
        private const int Col_PaymentMethod = 7;
        private const int Col_Action=8;
        private const int Col_EncounterID = 9;
        private const int Col_Status = 10;
        private const int Col_EncounterCount = 11;
        private const int Col_PendingCount = 12;
        private const int Col_PostedCount = 13;
        private const int Col_ServiceDate = 14;
        private const int Col_TimeStamp = 15;
        private const int Col_PaymentPlanID = 16;
        private const int Col_OriginalTransactionID = 17;
        private const int Col_TransactionID = 18;
        private const int Col_BranchID = 19;
        private const int Col_ClientID = 20;
        private const int Col_Origin = 21;
        private const int Col_TxnType = 22;
        private const int Col_ReferenceNumber = 23;
        private const int Col_User = 24;
        private const int Col_ApprovalCode = 25;
        private const int Col_NationalProviderID = 26;
        private const int Col_EntryMethod = 27;
        private const int Col_AccountType = 28;
        private const int Col_AccountNumber = 29;
        private const int Col_AccountName = 30;
        private const int Col_PatientCode = 31;
        private const int Col_dtSearchDate = 32;
        private const int Col_CreditEventType = 33;
       
        private const int Col_nPaymentTransactionID = 34;
        private const int Col_nNotecount = 36;
        private const int Col_PendingStatus = 35;
       
        private const int Col_Count = 37;
        #endregion


        private static ClsCleargagePaymentPosting oclsCleargagePaymentPosting;
        private static frmClearGagePaymentPosting ofrmCleargagePaymentDistribution;
        private Int64 nPaymentTransactionID = 0;
        public frmClearGagePaymentPosting()
        {
            InitializeComponent();
        }
        public static frmClearGagePaymentPosting GetInstance()
        {
            try
            {
                if (ofrmCleargagePaymentDistribution == null)
                {
                    ofrmCleargagePaymentDistribution = new frmClearGagePaymentPosting();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return ofrmCleargagePaymentDistribution;
        }

        private void frmClearGagePaymentPosting_Load(object sender, EventArgs e)
        {
                   

            try
            {
                tabClearGage.TabPages.Remove(tbpg_Hold);
                tabClearGage.TabPages.Remove(tbpg_Deleted); 
                FillClearGagePaymentPostingInfo();
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Open, "Cleargage Payment form open",0, 0, 0, gloAuditTrail.ActivityOutCome.Success,gloAuditTrail.SoftwareComponent.gloPM,true);
            }
            catch (Exception ex )
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Open, "Exception Occured while Cleargage Payment form open : "+ ex.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                ex = null;
            }
        }
        private void  FillClearGagePaymentPostingInfo()
        {
            DataTable dtPayment = null;
            try
            {
                oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
                dtPayment = oclsCleargagePaymentPosting.GetCleargagePaymentFileList(enumCheckStatus.Ready);
                if (dtPayment != null)
                {
                    //if (dtPayment.Rows.Count > 0)
                    //{
                        c1trvReadyToPost.BeginUpdate();
                        c1trvReadyToPost.DataSource = null;
                        c1trvReadyToPost.DataSource = dtPayment.DefaultView;
                        c1trvReadyToPost.EndUpdate();
                    //}
                        if (dtPayment.Rows.Count <= 0)
                        {
                            FillCleargageFileDetails(0);
                        }
                }
                DesignPaymentPosting(ref c1trvReadyToPost);
                if (txtSearchReadyToPostFile.Text != "")
                {
                    txtSearchReadyToPostFile.Clear();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oclsCleargagePaymentPosting != null)
                {
                    oclsCleargagePaymentPosting.Dispose();
                    oclsCleargagePaymentPosting = null;
                }
            }
        }
        private void DesignPaymentPosting(ref C1.Win.C1FlexGrid.C1FlexGrid C1Payment)
        {
            try
            {
                C1Payment.Cols.Count = COL_COUNT;
                C1Payment.Cols[COL_CleargageFileID].Caption = "CleargageFileID";
                C1Payment.Cols[COL_FileName].Caption = "File Name";
                C1Payment.Cols[COL_OriginalFileName].Caption = "Original File Name";
                C1Payment.Cols[COL_Importdate].Caption = "Import Date";
                C1Payment.Cols[COL_Status].Caption = "Status";
                C1Payment.Cols[COL_IsBlocked].Caption = "Blocked";


                int nWidth = C1Payment.Width - 5;
                C1Payment.Cols[COL_CleargageFileID].Width = 0;
                C1Payment.Cols[COL_FileName].Width = (int)(nWidth * 0.20);
                C1Payment.Cols[COL_OriginalFileName].Width = (int)(nWidth * 0.20);
                C1Payment.Cols[COL_Importdate].Width = (int)(nWidth * 0.31);
                C1Payment.Cols[COL_Status].Width = (int)(nWidth * 0.10);
                C1Payment.Cols[COL_IsBlocked].Width = (int)(Width * 0.10);
                C1Payment.Cols[COL_CellImage].Width = (int)(Width * 0.015);

                C1Payment.Cols[COL_CellImage].Visible = true;
                C1Payment.Cols[COL_CleargageFileID].Visible = false;
                C1Payment.Cols[COL_FileName].Visible = true;
                C1Payment.Cols[COL_OriginalFileName].Visible = false;
                C1Payment.Cols[COL_Importdate].Visible = false;
                C1Payment.Cols[COL_Status].Visible = false;
                C1Payment.Cols[COL_IsBlocked].Visible = false;
                C1Payment.AllowEditing = false;

                C1Payment.Cols[COL_CellImage].DataType = typeof(Image);
                C1Payment.Cols[COL_CellImage].ImageMap = new System.Collections.Hashtable();
                C1Payment.Cols[COL_CellImage].ImageMap.Add(0, null);
                C1Payment.Cols[COL_CellImage].ImageAndText = false;
                C1Payment.Cols[COL_CellImage].AllowResizing = false;
                C1Payment.ExtendLastCol = true;

                //C1Payment.Cols[COL_CellImage].Visible = true;
                //C1Payment.Cols[COL_OriginalFileName].Visible = false;
                //C1Payment.Cols[COL_Importdate].Visible = false;
                //C1Payment.AllowEditing = false;
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                C1Payment.Redraw = true;
            }
         

        }
       

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Close, "Cleargage Payment Closed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            this.Close();
        }

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            RemoveSearchCleargageText();
            RefreshView();
        }

        private void tabClearGage_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveSearchCleargageText();
            if (c1trvReadyToPost.Rows.Count > 0)
            {
                c1trvReadyToPost.Select(0, 0);
            }
            if (c1trvPosted.Rows.Count > 0)
            {
                c1trvPosted.Select(0, 0);
            }
            RefreshView();
        }

        private void RefreshView()
        {
            DataTable dtPaymentInfo = null;
            oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            try
            {
                switch (tabClearGage.SelectedTab.Tag.ToString())
                {
                    case TAB_READY:
                        {
                            dtPaymentInfo = oclsCleargagePaymentPosting.GetCleargagePaymentFileList(enumCheckStatus.Ready);
                            if (dtPaymentInfo != null && dtPaymentInfo.Rows.Count > 0)
                            {
                                c1trvReadyToPost.BeginUpdate();
                                int RowIndex = c1trvReadyToPost.Row;
                                int nNodeCount = c1trvReadyToPost.Rows.Count;
                                c1trvReadyToPost.DataSource = null;
                                c1trvReadyToPost.DataSource = dtPaymentInfo.DefaultView;
                                DesignPaymentPosting(ref c1trvReadyToPost);
                                if (RowIndex < 0) { RowIndex = c1trvReadyToPost.Row; }
                                if (c1trvReadyToPost.Rows.Count != nNodeCount)
                                {
                                    c1trvReadyToPost.Select(0, 0);
                                }
                                else
                                {
                                    c1trvReadyToPost.Select(RowIndex, 0);
                                }
                                //c1trvReadyToPost.Select(RowIndex, 0);
                                c1trvReadyToPost.EndUpdate();
                            }
                            else
                            {
                                c1trvReadyToPost.BeginUpdate();
                                c1trvReadyToPost.DataSource = dtPaymentInfo.DefaultView;
                                DesignPaymentPosting(ref c1trvReadyToPost);
                                c1trvReadyToPost.EndUpdate();
                                FillCleargageFileDetails(0);
                            }
                          
                            ShowHideButtons(TAB_READY);
                        }
                        break;
                    case TAB_POSTED:
                        {
                            dtPaymentInfo = oclsCleargagePaymentPosting.GetCleargagePaymentFileList(enumCheckStatus.Posted);
                            if (dtPaymentInfo != null && dtPaymentInfo.Rows.Count > 0)
                            {
                                c1trvPosted.BeginUpdate();
                                int RowIndex = c1trvPosted.Row;
                                int nNodeCount = c1trvPosted.Rows.Count;
                                c1trvPosted.DataSource = null;
                                c1trvPosted.DataSource = dtPaymentInfo.DefaultView;
                                DesignPaymentPosting(ref c1trvPosted);
                                if (RowIndex < 0) { RowIndex = c1trvPosted.Row; }
                                if (c1trvPosted.Rows.Count != nNodeCount)
                                {
                                    c1trvPosted.Select(0, 0);
                                }
                                else
                                {
                                    c1trvPosted.Select(RowIndex, 0);
                                }
                                c1trvPosted.EndUpdate();
                            }
                            else
                            {
                                c1trvPosted.BeginUpdate();
                                c1trvPosted.DataSource = dtPaymentInfo.DefaultView;
                                DesignPaymentPosting(ref c1trvPosted);
                                c1trvPosted.BeginUpdate();
                                FillPostedFileDetails(0);
                            }
                            ShowHideButtons(TAB_POSTED);
                        }
                        break;
                    case TAB_HOLD:
                        {
                            dtPaymentInfo = oclsCleargagePaymentPosting.GetCleargagePaymentFileList(enumCheckStatus.Hold);
                            if (dtPaymentInfo != null && dtPaymentInfo.Rows.Count > 0)
                            {
                                c1Hold.DataSource = null;
                                c1Hold.DataSource = dtPaymentInfo.DefaultView;
                                DesignPaymentPosting(ref c1Hold);
                            }
                            ShowHideButtons(TAB_HOLD);
                        }
                        break;
                    case TAB_DELETED:
                        {
                            dtPaymentInfo = oclsCleargagePaymentPosting.GetCleargagePaymentFileList(enumCheckStatus.MarkedDeleted);
                            if (dtPaymentInfo != null && dtPaymentInfo.Rows.Count > 0)
                            {
                                c1Deleted.DataSource = null;
                                c1Deleted.DataSource = dtPaymentInfo.DefaultView;
                                DesignPaymentPosting(ref c1Deleted);
                            }
                            ShowHideButtons(TAB_DELETED);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oclsCleargagePaymentPosting != null)
                {
                    oclsCleargagePaymentPosting.Dispose();
                    oclsCleargagePaymentPosting = null;
                }
            }
        }

        private void tsb_PostPayment_Click(object sender, EventArgs e)
        {
            bool bIsFileLock = false;
            Int64 nLockCleargageFileID = 0;
            try
            {
                if (c1ReadyToPost.RowSel>0)
                {
                    nLockCleargageFileID = Convert.ToInt64(c1ReadyToPost.Rows[c1ReadyToPost.RowSel][Col_CleargageFileID]);
                    bIsFileLock = CheckFileIsLock(nLockCleargageFileID);
                    if (bIsFileLock)
                    {
                        return;
                    } 
                }
                bool bIsPaymentPosting = false;

                #region "Create Fee Charge"

                DataTable dtCreateFee = new DataTable();
                Int64 nPatientID = 0;
                Int64 nPAccountID = 0;
                DateTime dtFromDOS = DateTime.MinValue;
                Decimal dAmount = 0;
                string sMessage = string.Empty;
                int nPending = 0;
                try
                {
                    //if (txtSearchReadyToPost.Text != "")
                    //{
                    //    txtSearchReadyToPost.Clear();
                    //}
                    if (c1ReadyToPost != null && c1ReadyToPost.Rows.Count > 1)
                    {
                        for (int i = 1; i < c1ReadyToPost.Rows.Count; i++)
                        {
                            if (Convert.ToString(c1ReadyToPost.Rows[i][Col_Action]).ToUpper() == Convert.ToString(Actions.FEE)) //&& Convert.ToString(c1ReadyToPost.Rows[i][Col_Status]) == "1")
                            {
                                nPending = 1;
                                break;
                            }
                        }
                        if (nPending == 1)
                        {
                            #region "Datatable for Fee Creation"

                            dtCreateFee.Columns.Add("nCleargageFileID");
                            dtCreateFee.Columns.Add("nPatientID");
                            dtCreateFee.Columns.Add("nPAccountID");
                            dtCreateFee.Columns.Add("dtFromDOS");
                            dtCreateFee.Columns.Add("dAmount");
                            dtCreateFee.Columns.Add("sPatientName");
                            dtCreateFee.Columns.Add("sPaymentplanID");
                            dtCreateFee.Columns.Add("sTransactionID");
                            dtCreateFee.Columns.Add("sOriginalTransactionID");
                            dtCreateFee.Columns.Add("sPaymentMethod");
                            dtCreateFee.Columns.Add("sAction");
                            dtCreateFee.Columns.Add("sTimestamp");
                            dtCreateFee.Columns.Add("sEncounterID");
                            dtCreateFee.Columns.Add("sBranchID");
                            dtCreateFee.Columns.Add("sClientID");
                            dtCreateFee.Columns.Add("sOrigin");
                            dtCreateFee.Columns.Add("sTxnType");
                            dtCreateFee.Columns.Add("sReferenceNo");
                            dtCreateFee.Columns.Add("sApprovalCode");
                            dtCreateFee.Columns.Add("sNationalProviderID");
                            dtCreateFee.Columns.Add("sEntryMethod");
                            dtCreateFee.Columns.Add("sAccountType");
                            dtCreateFee.Columns.Add("sAccountNumber");
                            dtCreateFee.Columns.Add("sAccountName");

                            #endregion

                            for (int i = 1; i < c1ReadyToPost.Rows.Count; i++)
                            {
                                if (Convert.ToString(c1ReadyToPost.Rows[i][Col_Action]).ToUpper() == Convert.ToString(Actions.FEE) && Convert.ToInt16(c1ReadyToPost.Rows[i][Col_Status]) == Status.ReadytoPost.GetHashCode())// && Convert.ToString(c1ReadyToPost.Rows[i][Col_Status]) == "1")
                                {
                                    if (Convert.ToString(c1ReadyToPost.Rows[i][Col_PatientID]) != null && Convert.ToString(c1ReadyToPost.Rows[i][Col_PatientID]) != "")
                                    {
                                        nPatientID = Convert.ToInt64(c1ReadyToPost.Rows[i][Col_PatientID]);
                                    }
                                    if (Convert.ToString(c1ReadyToPost.Rows[i][Col_PAccountID]) != null && Convert.ToString(c1ReadyToPost.Rows[i][Col_PAccountID]) != "")
                                    {
                                        nPAccountID = Convert.ToInt64(c1ReadyToPost.Rows[i][Col_PAccountID]);
                                    }
                                    dtFromDOS = Convert.ToDateTime(c1ReadyToPost.Rows[i][Col_ServiceDate]);
                                    string sAmount = Convert.ToString(c1ReadyToPost.Rows[i][Col_Amount]);
                                    dAmount = Convert.ToDecimal(sAmount.Replace('-', ' ').Trim());

                                    #region "Data For Fee Creation"

                                    dtCreateFee.Rows.Add();
                                    int nRow = dtCreateFee.Rows.Count;
                                    dtCreateFee.Rows[nRow - 1]["nPatientID"] = nPatientID;
                                    dtCreateFee.Rows[nRow - 1]["nPAccountID"] = nPAccountID;
                                    dtCreateFee.Rows[nRow - 1]["dtFromDOS"] = dtFromDOS;
                                    dtCreateFee.Rows[nRow - 1]["dAmount"] = dAmount;
                                    dtCreateFee.Rows[nRow - 1]["nCleargageFileID"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_CleargageFileID]);
                                    dtCreateFee.Rows[nRow - 1]["sPatientName"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_PatientName]);
                                    dtCreateFee.Rows[nRow - 1]["sTransactionID"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_TransactionID]);
                                    dtCreateFee.Rows[nRow - 1]["sPaymentplanID"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_PaymentPlanID]);
                                    dtCreateFee.Rows[nRow - 1]["sOriginalTransactionID"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_OriginalTransactionID]);
                                    dtCreateFee.Rows[nRow - 1]["sPaymentMethod"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_PaymentMethod]);
                                    dtCreateFee.Rows[nRow - 1]["sAction"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_Action]);
                                    dtCreateFee.Rows[nRow - 1]["sTimestamp"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_TimeStamp]);
                                    dtCreateFee.Rows[nRow - 1]["sEncounterID"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_EncounterID]);
                                    dtCreateFee.Rows[nRow - 1]["sBranchID"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_BranchID]);
                                    dtCreateFee.Rows[nRow - 1]["sClientID"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_ClientID]);
                                    dtCreateFee.Rows[nRow - 1]["sOrigin"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_Origin]);
                                    dtCreateFee.Rows[nRow - 1]["sTxnType"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_TxnType]);
                                    dtCreateFee.Rows[nRow - 1]["sReferenceNo"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_ReferenceNumber]);
                                    dtCreateFee.Rows[nRow - 1]["sApprovalCode"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_ApprovalCode]);
                                    dtCreateFee.Rows[nRow - 1]["sNationalProviderID"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_NationalProviderID]);
                                    dtCreateFee.Rows[nRow - 1]["sEntryMethod"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_EntryMethod]);
                                    dtCreateFee.Rows[nRow - 1]["sAccountType"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_AccountType]);
                                    dtCreateFee.Rows[nRow - 1]["sAccountNumber"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_AccountNumber]);
                                    dtCreateFee.Rows[nRow - 1]["sAccountName"] = Convert.ToString(c1ReadyToPost.Rows[i][Col_AccountName]);

                                    #endregion

                                }
                            }
                            DataTable dtFinalCreateFee = null;
                            bool bIsFeeCreate = false;
                            oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
                            dtFinalCreateFee = oclsCleargagePaymentPosting.CheckCleargageFeeCharge(dtCreateFee, out bIsFeeCreate);

                            if (bIsFeeCreate == true && (dtFinalCreateFee == null || dtFinalCreateFee.Rows.Count <= 0))
                            {
                                bIsPaymentPosting = true;
                            }
                            else if (bIsFeeCreate == true)
                            {
                                #region "Create Fee"

                                string _message = "System will create offset liability charge for encounters action 'FEE' against payment plan fees" + Environment.NewLine + "Do you want to continue? ";
                                DialogResult dg = MessageBox.Show(_message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (dg == DialogResult.Yes)
                                {
                                    Cursor.Current = Cursors.WaitCursor;
                                    int nErrorPostFee = 0;
                                    string sErrorMessage_Tray = string.Empty;
                                    string sErrorMessage_ID = string.Empty;
                                    string sNotPostedFeePatient = string.Empty;

                                    sMessage = oclsCleargagePaymentPosting.GenerateCleargageFeeCharge(dtFinalCreateFee);

                                    if (sMessage == "" && sMessage != null)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageFee, gloAuditTrail.ActivityType.Generate, "Cleargage Fee Charge Created for CleargageFileID" + Convert.ToString(c1ReadyToPost.Rows[1][Col_CleargageFileID]), nPatientID, Convert.ToInt64(c1ReadyToPost.Rows[1][Col_CleargageFileID]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                    }
                                    else
                                    {
                                        if (sMessage.Contains("Exception Occured :"))
                                        {
                                            nErrorPostFee = 1;
                                            bIsPaymentPosting = false;
                                            MessageBox.Show("No fee created", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        if (sMessage == "Charge Tray or Payment Tray is not available in system." && !sErrorMessage_Tray.Contains("Charge Tray or Payment Tray is not available in system."))
                                        {
                                            nErrorPostFee = 1;
                                            bIsPaymentPosting = false;
                                            MessageBox.Show("Charge Tray is not available in system.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                    }

                                    Cursor.Current = Cursors.Default;
                                    if (nErrorPostFee == 0)
                                    {
                                        bIsPaymentPosting = true;
                                    }
                                    //  FillClearGagePaymentPostingInfo();
                                    //  c1trvReadyToPost.Select(c1trvReadyToPost.Row, COL_FileName);
                                }
                                else
                                {
                                    bIsPaymentPosting = false;
                                }

                                #endregion
                            }
                        }
                        else
                        {
                            bIsPaymentPosting = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageFee, gloAuditTrail.ActivityType.Generate, "Exception occured while Cleargage Fee Charge Created for CleargageFileID" + Convert.ToString(c1ReadyToPost.Rows[1][Col_CleargageFileID]), nPatientID, Convert.ToInt64(c1ReadyToPost.Rows[1][Col_CleargageFileID]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    bIsPaymentPosting = false;
                }
                finally
                {
                    if (oclsCleargagePaymentPosting != null)
                    {
                        oclsCleargagePaymentPosting.Dispose();
                        oclsCleargagePaymentPosting = null;
                    }
                }

                #endregion


                #region "Payment Posting"

                int Pending = 0;
                int IsPreviousPending = 0;
                Int64 nCleargageFileID = 0;
                frmCleargagePaymentDistributionList ofrmCleargagePaymentDistribution = null;
                frmCleargagePaymentVoid ofrmCleargagePaymentVoid = null;
                try
                {
                    //if (bIsPaymentPosting == true)
                    //{
                    //    if (c1ReadyToPost.Rows.Count > 1)
                    //    {
                    //        for (int i = 0; i < c1ReadyToPost.Rows.Count; i++)
                    //        {
                    //            if ((Convert.ToString(c1ReadyToPost.GetData(i, Col_Action)).ToUpper() == Convert.ToString(Actions.PAYMENT) || Convert.ToString(c1ReadyToPost.GetData(i, Col_Action)).ToUpper() == Convert.ToString(Actions.FEE)) && Convert.ToInt16(c1ReadyToPost.Rows[i][Col_Status]) == 1)
                    //            {
                    //                Pending = 1;
                    //                break;
                    //            }
                    //        }
                    //        if (Pending == 1)
                    //        {
                    //            nCleargageFileID = Convert.ToInt64(c1trvReadyToPost.GetData(c1trvReadyToPost.Row, COL_CleargageFileID));
                    //            ofrmCleargagePaymentDistribution = new frmCleargagePaymentDistributionList(nCleargageFileID);
                    //            ofrmCleargagePaymentDistribution.WindowState = FormWindowState.Maximized;
                    //            ofrmCleargagePaymentDistribution.Icon = Properties.Resources.PostPayment;
                    //            ofrmCleargagePaymentDistribution.sAction = Convert.ToString(Actions.PAYMENT); //"PAYMENT";
                    //            ofrmCleargagePaymentDistribution.sPaymentMethod = Convert.ToString(PaymentMethod.CREDIT); //"CREDIT";
                    //            ofrmCleargagePaymentDistribution.ShowDialog();
                    //            ofrmCleargagePaymentDistribution.Dispose();
                    //            ofrmCleargagePaymentDistribution = null;
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("No payment available for posting", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            return;
                    //        }
                    //        int Rowindex = c1trvReadyToPost.Row;
                    //        c1trvReadyToPost.Select(Rowindex, COL_FileName);
                    //    }
                    //    FillClearGagePaymentPostingInfo();
                    //}
                    if (bIsPaymentPosting == true)
                    {
                        if (c1ReadyToPost.Rows.Count > 1)
                        {
                            #region POST PAYMENT
                            for (int i = 0; i < c1ReadyToPost.Rows.Count; i++)
                            {
                                if ((Convert.ToString(c1ReadyToPost.GetData(i, Col_Action)).ToUpper() == Convert.ToString(Actions.PAYMENT) || Convert.ToString(c1ReadyToPost.GetData(i, Col_Action)).ToUpper() == Convert.ToString(Actions.FEE)) && Convert.ToInt16(c1ReadyToPost.Rows[i][Col_Status]) == 1)
                                {
                                    Pending = 1;
                                    break;
                                }

                            }
                            if (Pending == 1)
                            {
                                nCleargageFileID = Convert.ToInt64(c1trvReadyToPost.GetData(c1trvReadyToPost.Row, COL_CleargageFileID));
                                ofrmCleargagePaymentDistribution = new frmCleargagePaymentDistributionList(nCleargageFileID);
                                ofrmCleargagePaymentDistribution.WindowState = FormWindowState.Maximized;
                                ofrmCleargagePaymentDistribution.Icon = Properties.Resources.PostPayment;
                                ofrmCleargagePaymentDistribution.sAction = Convert.ToString(Actions.PAYMENT); //"PAYMENT";
                                ofrmCleargagePaymentDistribution.sPaymentMethod = Convert.ToString(PaymentMethod.CREDIT); //"CREDIT";
                                ofrmCleargagePaymentDistribution.ShowDialog();
                                ofrmCleargagePaymentDistribution.Dispose();
                                ofrmCleargagePaymentDistribution = null;
                                IsPreviousPending = 1;

                            }

                            #endregion
                            Pending = 0;
                            #region POST DISCOUNT
                            for (int i = 0; i < c1ReadyToPost.Rows.Count; i++)
                            {
                                if (Convert.ToString(c1ReadyToPost.GetData(i, Col_Action)).ToUpper() == Convert.ToString(Actions.DISCOUNT) && Convert.ToString(c1ReadyToPost.GetData(i, Col_PaymentMethod)).ToUpper() == "" && Convert.ToInt16(c1ReadyToPost.Rows[i][Col_Status]) == enumPaymentStatus.Pending.GetHashCode())
                                {
                                    Pending = 1;
                                    break;
                                }
                            }

                            if (Pending == 1)
                            {
                                if (IsPreviousPending == 0 || MessageBox.Show("File contains discount.\nDo you want to post discount", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    nCleargageFileID = Convert.ToInt64(c1trvReadyToPost.GetData(c1trvReadyToPost.Row, COL_CleargageFileID));
                                    ofrmCleargagePaymentDistribution = new frmCleargagePaymentDistributionList(nCleargageFileID);
                                    ofrmCleargagePaymentDistribution.WindowState = FormWindowState.Maximized;
                                    ofrmCleargagePaymentDistribution.Icon = Properties.Resources.PostDiscount;
                                    //ofrmCleargagePaymentDistribution.sAction = Convert.ToString(Actions.ADJUSTMENT); //"ADJUSTMENT";
                                    ofrmCleargagePaymentDistribution.sAction = Convert.ToString(Actions.DISCOUNT); //"ADJUSTMENT";
                                    ofrmCleargagePaymentDistribution.ShowDialog();
                                    ofrmCleargagePaymentDistribution.Dispose();
                                    ofrmCleargagePaymentDistribution = null;
                                    IsPreviousPending = 1;

                                }
                            }

                            #endregion
                            Pending = 0;
                            #region POST CREDIT
                            for (int i = 0; i < c1ReadyToPost.Rows.Count; i++)
                            {
                                if ((Convert.ToString(c1ReadyToPost.GetData(i, Col_Action)).ToUpper() == Convert.ToString(Actions.CREDIT) || Convert.ToString(c1ReadyToPost.GetData(i, Col_Action)).ToUpper().Replace(" ", "") == Convert.ToString(Actions.FEECREDIT)) && Convert.ToInt16(c1ReadyToPost.Rows[i][Col_Status]) == enumPaymentStatus.Pending.GetHashCode())
                                {
                                    Pending = 1;
                                    break;
                                }
                            }
                            if (Pending == 1)
                            {
                                if (IsPreviousPending == 0 || MessageBox.Show("File contains refund/void.\nDo you want to post refund/void", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    nCleargageFileID = Convert.ToInt64(c1trvReadyToPost.GetData(c1trvReadyToPost.Row, COL_CleargageFileID));
                                    ofrmCleargagePaymentVoid = new frmCleargagePaymentVoid(nCleargageFileID);
                                    ofrmCleargagePaymentVoid.ShowDialog();
                                    ofrmCleargagePaymentVoid.Dispose();
                                    ofrmCleargagePaymentVoid = null;
                                }
                            }

                            #endregion

                            int Rowindex = c1trvReadyToPost.Row;
                            c1trvReadyToPost.Select(Rowindex, COL_FileName);
                        }
                        FillClearGagePaymentPostingInfo();
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageFee, gloAuditTrail.ActivityType.Load, "Exception occured while Cleargage payment posting for CleargageFileID" + Convert.ToString(c1ReadyToPost.Rows[1][Col_CleargageFileID]), nPatientID, Convert.ToInt64(c1ReadyToPost.Rows[1][Col_CleargageFileID]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    ex = null;
                }

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (bIsFileLock==false)
                {
                    if (c1ReadyToPost.RowSel>0)
                    {
                        ClsCleargagePaymentPosting clsPosting = new ClsCleargagePaymentPosting();
                        clsPosting.InsertDeleteLockedFile(nLockCleargageFileID, gloGlobal.gloPMGlobal.UserID, 1, gloGlobal.gloPMGlobal.UserName, Environment.MachineName);
                        if (clsPosting != null)
                        {
                            clsPosting.Dispose();
                            clsPosting = null;
                        }  
                    }
                }
            }
        }           
        
        private bool CheckFileIsLock(Int64 nCleargageFileID)
        {
            bool bIsFileLocked = false;
            ClsCleargagePaymentPosting clsPosting = null;
            try
            {
                string sMsg = string.Empty;
                clsPosting = new ClsCleargagePaymentPosting();
                bIsFileLocked = clsPosting.IsFileLocked(nCleargageFileID, out sMsg);
                if (bIsFileLocked)
                {
                    MessageBox.Show(sMsg, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return bIsFileLocked;
                }
                else
                {
                    clsPosting.InsertDeleteLockedFile(nCleargageFileID, gloGlobal.gloPMGlobal.UserID, 0, gloGlobal.gloPMGlobal.UserName, Environment.MachineName);
                    return bIsFileLocked;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (clsPosting != null)
                {
                    clsPosting.Dispose();
                    clsPosting = null;
                }
            }
            return bIsFileLocked;
        }

        private void txtSearchAll_TextChanged(object sender, EventArgs e)
        {
            string strSearch = "";
            DataView _dv = new DataView();
            string sFilter = "";
            if (sender != null)
            {
                switch(((TextBox)sender).Tag.ToString())
                {
                    case TAB_READY:
                        {
                            
                                c1trvReadyToPost.RowSel = -1;
                                c1trvReadyToPost.Select(-1, -1, false);
                             //   DataTable dt = (DataTable)c1trvReadyToPost.DataSource;
                               // _dv = dt.DefaultView;
                                _dv = (DataView)c1trvReadyToPost.DataSource;
                                strSearch = txtSearchReadyToPostFile.Text.Trim();
                                strSearch = strSearch.Replace("'", "''").Replace("*", "%").Replace("[", "").Replace("]", "");
                                if (_dv != null)
                                {
                                    if (strSearch != "")
                                    {
                                        sFilter = "(" + _dv.Table.Columns["sFileName"].ColumnName + " like '%" + strSearch + "%')";
                                    }

                                    _dv.RowFilter = sFilter;
                                    if (_dv.ToTable().Rows.Count > 0)
                                    {

                                        //c1trvReadyToPost.DataSource = _dv.ToTable();
                                        //DesignPaymentPosting(ref c1trvReadyToPost);

                                        c1trvReadyToPost.RowSel = 0;
                                        c1trvReadyToPost.AfterSelChange-=new RangeEventHandler(c1trvReadyToPost_AfterSelChange);
                                        c1trvReadyToPost.Select(0, 0, true);
                                        c1trvReadyToPost.AfterSelChange += new RangeEventHandler(c1trvReadyToPost_AfterSelChange);

                                    }
                                    else
                                    {
                                        //c1trvReadyToPost.RowSel = -1;
                                        //c1trvReadyToPost.Select(-1, -1, false);
                                        c1ReadyToPost.DataSource = null;
                                        DesignPaymentDetails(ref c1ReadyToPost);
                                        if (c1ReadyToPost.Rows.Count > 1)
                                        {
                                            c1ReadyToPost.Rows.RemoveRange(1, c1ReadyToPost.Rows.Count - 1);
                                            lblEncounterCount.Text = "0";
                                            lblPostedCount.Text = "0";
                                            lblPendingCount.Text = "0";
                                        }
                                    }
                                }

                            break;
                        }

                    case TAB_POSTED:
                        {
                            c1trvPosted.RowSel = -1;
                            c1trvPosted.Select(-1, -1, false);
                            //DataTable dt = (DataTable)c1Posted.DataSource;
                            _dv = (DataView)c1trvPosted.DataSource;
                            strSearch = txtSearchPostedFile.Text.Trim();
                            strSearch = strSearch.Replace("'", "''").Replace("*", "%").Replace("[", "").Replace("]", "");
                            if (_dv != null)
                            {
                                if (strSearch != "")
                                {
                                    sFilter = "(" + _dv.Table.Columns["sFileName"].ColumnName + " like '%" + strSearch + "%')";
                                }
                                _dv.RowFilter = sFilter;
                                if (_dv.ToTable().Rows.Count > 0)
                                {

                                    //c1trvReadyToPost.DataSource = _dv.ToTable();
                                    //DesignPaymentPosting(ref c1trvReadyToPost);

                                    c1trvPosted.RowSel = 0;
                                    c1trvPosted.Select(0, 0, true);
                                }
                                else
                                {
                                    //c1trvReadyToPost.RowSel = -1;
                                    //c1trvReadyToPost.Select(-1, -1, false);
                                    C1Posted.DataSource = null;
                                    DesignPaymentDetails(ref C1Posted);
                                    if (C1Posted.Rows.Count > 1)
                                    {
                                        C1Posted.Rows.RemoveRange(1, C1Posted.Rows.Count - 1);
                                        lblPostedEncounter.Text = "0";
                                    }
                                }
                            }
                            break;
                        }

                    case TAB_DELETED:
                        {
                            //DataTable dt = (DataTable)c1Deleted.DataSource;
                            _dv = (DataView)c1Deleted.DataSource;
                            strSearch = txtSearchDeleted.Text.Trim();
                            strSearch = strSearch.Replace("'", "''").Replace("*", "%").Replace("[", "").Replace("]", "");
                            if (_dv != null)
                            {
                                if (strSearch != "")
                                {
                                    sFilter = "(" + _dv.Table.Columns["sFileName"].ColumnName + " like '%" + strSearch + "%') or (" + _dv.Table.Columns["sOriginalFileName"].ColumnName + " like '%" + strSearch + "%')";
                                }
                                _dv.RowFilter = sFilter;
                            }
                            break;
                        }

                    case TAB_HOLD:
                        {
                            DataTable dt = (DataTable)c1Hold.DataSource;
                            _dv = dt.DefaultView;
                            strSearch = txtSearchHold.Text.Trim();
                            strSearch = strSearch.Replace("'", "''").Replace("*", "%").Replace("[", "").Replace("]", "");
                            if (strSearch != "")
                            {
                                sFilter = "(" + _dv.Table.Columns["sFileName"].ColumnName + " like '%" + strSearch + "%') or (" + _dv.Table.Columns["sOriginalFileName"].ColumnName + " like '%" + strSearch + "%')";
                            }
                            _dv.RowFilter = sFilter;
                            break;
                        }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                switch (((Button)sender).Tag.ToString())
                {
                    case TAB_READY:
                        {
                            if (txtSearchReadyToPostFile.Text != "")
                            {
                                txtSearchReadyToPostFile.Clear();
                                txtSearchReadyToPostFile.Focus();
                                txtSearchReadyToPost.Clear();
                                txtSearchReadyToPost.Focus();
                            }
                            break;
                        }

                    case TAB_POSTED:
                        {
                            if (txtSearchPostedFile.Text != "")
                            {
                                txtSearchPostedFile.Clear();
                                txtSearchPostedFile.Focus();
                                txtSearchPosted.Clear();
                                txtSearchPosted.Focus();
                            }
                            
                            break;
                        }

                    case TAB_DELETED:
                        {
                            txtSearchDeleted.Clear();
                            txtSearchDeleted.Focus();
                            break;
                        }

                    case TAB_HOLD:
                        {
                            txtSearchHold.Clear();
                            txtSearchHold.Focus();
                            break;
                        }

                }
            }
        }

        private void c1trvReadyToPost_AfterSelChange(object sender, RangeEventArgs e)
        {
            Int64 nCleargageFileID = 0;
            try
            {
                if (((C1FlexGrid)sender).RowSel >= 0)
                {
                    nCleargageFileID = Convert.ToInt64(((C1FlexGrid)sender).GetData(((C1FlexGrid)sender).RowSel, COL_CleargageFileID));
                    FillCleargageFileDetails(nCleargageFileID);
                }
                if (txtSearchReadyToPost.Text != "")
                {
                    txtSearchReadyToPost.Clear();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
               
            }
        }

        private void FillCleargageFileDetails(Int64 nCleargageFileID)
        {
            DataTable dtFileDetails = null;
            oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            try
            {
                dtFileDetails = oclsCleargagePaymentPosting.GetCleargageFileDetails(nCleargageFileID);
                c1ReadyToPost.BeginUpdate();
                c1ReadyToPost.SelChange -= new EventHandler(c1ReadyToPost_SelChange);
                c1ReadyToPost.DataSource = null;
                c1ReadyToPost.DataSource = dtFileDetails.DefaultView;
                c1ReadyToPost.SelChange += new EventHandler(c1ReadyToPost_SelChange);

                if (dtFileDetails != null && dtFileDetails.Rows.Count > 0)
                {
                    lblEncounterCount.Text = Convert.ToString(dtFileDetails.Rows[0]["EncounterCount"]);
                    lblPendingCount.Text = Convert.ToString(dtFileDetails.Rows[0]["Pending"]);
                    lblPostedCount.Text = Convert.ToString(dtFileDetails.Rows[0]["Posted"]);
                    c1ReadyToPost.Select(1, 1);
                }
                else
                {
                    lblEncounterCount.Text = "0";
                    lblPendingCount.Text = "0";
                    lblPostedCount.Text = "0";
                }
                DesignPaymentDetails(ref c1ReadyToPost);
                c1ReadyToPost.EndUpdate();
                //if (c1ReadyToPost.Rows.Count > 1)
                //{
                //    for (int i = 0; i < dtFileDetails.Rows.Count; i++)
                //    {
                //        if (Convert.ToInt16(dtFileDetails.Rows[i][Col_Status]) ==enumPaymentStatus.Pending.GetHashCode())
                //        {
                //            c1ReadyToPost.SetCellImage(i + 1, Col_PendingStatus, global::gloBilling.Properties.Resources.HoldClaim);
                //        }
                //        else
                //        {
                //            c1ReadyToPost.SetCellImage(i + 1, Col_PendingStatus, global::gloBilling.Properties.Resources.StatusNormal);
                //        }
                //    }
                //}
              //  c1ReadyToPost.Select(c1ReadyToPost.Row, Col_PatientName);
                #region Design C1trvReadyToPost
                if (c1trvReadyToPost.DataSource!=null && c1trvReadyToPost.Rows.Count > 0)
                {
                    for (int i = 0; i < c1trvReadyToPost.Rows.Count; i++)
                    {
                        c1trvReadyToPost.SetCellImage(i, COL_CellImage, global::gloBilling.Properties.Resources.Bullet06);
                    }
                }
                #endregion

                if (txtSearchReadyToPost.Text!="")
                {
                    txtSearchReadyToPost.Clear();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oclsCleargagePaymentPosting != null)
                {
                    oclsCleargagePaymentPosting.Dispose();
                    oclsCleargagePaymentPosting = null;
                }
            }
            
        }

        private void DesignPaymentDetails(ref C1.Win.C1FlexGrid.C1FlexGrid C1Cleargage)
        {
            C1Cleargage.Cols.Count = Col_Count;

            C1Cleargage.Cols[Col_CleargageFileID].Caption = "Cleargage ID";
            C1Cleargage.Cols[Col_PAccountID].Caption = "PAccountID";
            C1Cleargage.Cols[Col_PatientID].Caption = "Patient ID";
            C1Cleargage.Cols[Col_PatientName].Caption = "Patient Name";
            C1Cleargage.Cols[Col_OriginalFileName].Caption = "Original File Name";
            C1Cleargage.Cols[Col_ImportDate].Caption = "Import Date";
            C1Cleargage.Cols[Col_Amount].Caption = "Amount";
            C1Cleargage.Cols[Col_PaymentMethod].Caption = "Pay. Method";
            C1Cleargage.Cols[Col_Action].Caption = "Action";
            C1Cleargage.Cols[Col_EncounterID].Caption = "Encounter ID";
            C1Cleargage.Cols[Col_EncounterCount].Caption = "Total Encounter";
            C1Cleargage.Cols[Col_Status].Caption = "";
            C1Cleargage.Cols[Col_PendingCount].Caption = "Pending";
            C1Cleargage.Cols[Col_PostedCount].Caption = "Posted";
            C1Cleargage.Cols[Col_ServiceDate].Caption = "Service Date";            
            C1Cleargage.Cols[Col_TimeStamp].Caption = "TimeStamp";
            C1Cleargage.Cols[Col_PaymentPlanID].Caption = "PaymentPlanID";
            C1Cleargage.Cols[Col_OriginalTransactionID].Caption = "OriginalTransactionID";
            C1Cleargage.Cols[Col_TransactionID].Caption = "TransactionID";
            C1Cleargage.Cols[Col_BranchID].Caption = "BranchID";
            C1Cleargage.Cols[Col_ClientID].Caption = "ClientID";
            C1Cleargage.Cols[Col_Origin].Caption = "Origin";
            C1Cleargage.Cols[Col_TxnType].Caption = "TxnType";
            C1Cleargage.Cols[Col_ReferenceNumber].Caption = "Reference #";
            C1Cleargage.Cols[Col_User].Caption = "User";
            C1Cleargage.Cols[Col_ApprovalCode].Caption = "ApprovalCode";
            C1Cleargage.Cols[Col_NationalProviderID].Caption = "NationalProviderID";
            C1Cleargage.Cols[Col_EntryMethod].Caption = "EntryMethod";
            C1Cleargage.Cols[Col_AccountType].Caption = "AccountType";
            C1Cleargage.Cols[Col_AccountNumber].Caption = "AccountNumber";
            C1Cleargage.Cols[Col_AccountName].Caption = "AccountName";
            C1Cleargage.Cols[Col_PatientCode].Caption = "Patient Code";
            C1Cleargage.Cols[Col_dtSearchDate].Caption = "dtSearchDate";
            C1Cleargage.Cols[Col_CreditEventType].Caption = "CreditEventType";
            C1Cleargage.Cols[Col_PendingStatus].Caption = "";

            C1Cleargage.Cols[Col_nPaymentTransactionID].Caption = "nPaymentTransactionID";
            C1Cleargage.Cols[Col_nNotecount].Caption = "";

            //int nWidth = C1Cleargage.Width - 5;
            C1Cleargage.Cols[Col_CleargageFileID].Width = 0;
            C1Cleargage.Cols[Col_PAccountID].Width = 0;
            C1Cleargage.Cols[Col_PatientID].Width = 0;
            C1Cleargage.Cols[Col_PatientName].Width = 125;
            C1Cleargage.Cols[Col_OriginalFileName].Width =254;
            C1Cleargage.Cols[Col_ImportDate].Width =85;
            C1Cleargage.Cols[Col_Amount].Width =55;
            C1Cleargage.Cols[Col_PaymentMethod].Width = 95;
            C1Cleargage.Cols[Col_Action].Width = 65;
            C1Cleargage.Cols[Col_EncounterID].Width = 156;
            C1Cleargage.Cols[Col_Status].Width =10;
            C1Cleargage.Cols[Col_ServiceDate].Width = 85;
            C1Cleargage.Cols[Col_ReferenceNumber].Width = 85;
            C1Cleargage.Cols[Col_PatientCode].Width = 0;
            C1Cleargage.Cols[Col_dtSearchDate].Width = 0;
            C1Cleargage.Cols[Col_PendingStatus].Width = 30;
            C1Cleargage.Cols[Col_CreditEventType].Width = 0;
            C1Cleargage.Cols[Col_nPaymentTransactionID].Width = 0;          

            C1Cleargage.Cols[Col_CleargageFileID].Visible = false;
            C1Cleargage.Cols[Col_PAccountID].Visible = false;
            C1Cleargage.Cols[Col_PatientID].Visible = false;
            C1Cleargage.Cols[Col_PatientName].Visible = true;
            C1Cleargage.Cols[Col_OriginalFileName].Visible = true;
            C1Cleargage.Cols[Col_ImportDate].Visible = true;
            C1Cleargage.Cols[Col_Amount].Visible = true;
            C1Cleargage.Cols[Col_PaymentMethod].Visible = true;
            C1Cleargage.Cols[Col_Action].Visible = true;
            C1Cleargage.Cols[Col_EncounterID].Visible = true;
            C1Cleargage.Cols[Col_EncounterCount].Visible = false;
            C1Cleargage.Cols[Col_PendingCount].Visible = false;
            C1Cleargage.Cols[Col_PostedCount].Visible = false;
            C1Cleargage.Cols[Col_PostedCount].Visible = false;
            C1Cleargage.Cols[Col_Status].Visible = false;
            C1Cleargage.Cols[Col_ServiceDate].Visible = true;
            C1Cleargage.Cols[Col_dtSearchDate].Visible = false;
            C1Cleargage.Cols[Col_CreditEventType].Visible = false;

            C1Cleargage.Cols[Col_nPaymentTransactionID].Visible = false;
            C1Cleargage.Cols[Col_nNotecount].Visible = true;

            C1Cleargage.Cols[Col_TimeStamp].Visible = false;
            C1Cleargage.Cols[Col_PaymentPlanID].Visible = false;
            C1Cleargage.Cols[Col_OriginalTransactionID].Visible = false;
            C1Cleargage.Cols[Col_TransactionID].Visible = false;
            C1Cleargage.Cols[Col_BranchID].Visible = false;
            C1Cleargage.Cols[Col_ClientID].Visible = false;
            C1Cleargage.Cols[Col_Origin].Visible = false;
            C1Cleargage.Cols[Col_TxnType].Visible = false;
            C1Cleargage.Cols[Col_ReferenceNumber].Visible = true;
            C1Cleargage.Cols[Col_User].Visible = false;
            C1Cleargage.Cols[Col_ApprovalCode].Visible = false;
            C1Cleargage.Cols[Col_NationalProviderID].Visible = false;
            C1Cleargage.Cols[Col_EntryMethod].Visible = false;
            C1Cleargage.Cols[Col_AccountNumber].Visible = false;
            C1Cleargage.Cols[Col_AccountType].Visible = false;
            C1Cleargage.Cols[Col_AccountName].Visible = false;
            C1Cleargage.Cols[Col_PatientCode].Visible = false;
            C1Cleargage.AllowEditing = false;
            //C1Cleargage.Redraw = true;

           
            C1Cleargage.Cols[Col_PAccountID].AllowResizing = false;
            C1Cleargage.Cols[Col_PatientID].AllowResizing = false;
            C1Cleargage.Cols[Col_PatientName].AllowResizing = false;
            C1Cleargage.Cols[Col_OriginalFileName].AllowResizing = true;
            C1Cleargage.Cols[Col_ImportDate].AllowResizing = false;
            C1Cleargage.Cols[Col_Amount].AllowResizing = false;
            C1Cleargage.Cols[Col_PaymentMethod].AllowResizing = false;
            C1Cleargage.Cols[Col_Action].AllowResizing = false;
            C1Cleargage.Cols[Col_EncounterID].AllowResizing = false;
            C1Cleargage.Cols[Col_EncounterCount].AllowResizing = false;
            C1Cleargage.Cols[Col_PendingCount].AllowResizing = false;
            C1Cleargage.Cols[Col_PostedCount].AllowResizing = false;
            C1Cleargage.Cols[Col_PostedCount].AllowResizing = false;
            C1Cleargage.Cols[Col_Status].AllowResizing = false;
            C1Cleargage.Cols[Col_ServiceDate].AllowResizing = false;
            C1Cleargage.Cols[Col_dtSearchDate].AllowResizing = false;
            C1Cleargage.Cols[Col_CreditEventType].AllowResizing = false;
            C1Cleargage.Cols[Col_nPaymentTransactionID].AllowResizing = false;
            C1Cleargage.Cols[Col_nNotecount].AllowResizing = false;
            C1Cleargage.Cols[Col_ReferenceNumber].AllowResizing = false;
            

            C1Cleargage.Cols[Col_PendingStatus].DataType = typeof(Image);
            C1Cleargage.Cols[Col_PendingStatus].ImageMap = new System.Collections.Hashtable();
            C1Cleargage.Cols[Col_PendingStatus].ImageMap.Add(0, null);
            C1Cleargage.Cols[Col_PendingStatus].ImageMap.Add(1, global::gloBilling.Properties.Resources.HoldClaim);
            C1Cleargage.Cols[Col_PendingStatus].ImageMap.Add(2, global::gloBilling.Properties.Resources.StatusNormal);
            C1Cleargage.Cols[Col_PendingStatus].ImageAndText = false;
            C1Cleargage.Cols[Col_PendingStatus].AllowResizing = false;
            C1Cleargage.Cols[Col_nNotecount].Width = (Int32)(24);
            C1Cleargage.ExtendLastCol = true;
            AddCellStyleForGroups(C1Cleargage);

            C1Cleargage.SetCellImage(0, Col_nNotecount, global::gloBilling.Properties.Resources.Notes);
            C1Cleargage.Cols[Col_nNotecount].DataType = typeof(Image);
            C1Cleargage.Cols[Col_nNotecount].ImageMap = new System.Collections.Hashtable();
            C1Cleargage.Cols[Col_nNotecount].ImageMap.Add(0, null);
            C1Cleargage.Cols[Col_nNotecount].ImageMap.Add(1, global::gloBilling.Properties.Resources.Notes);
            C1Cleargage.Cols[Col_nNotecount].ImageAndText = false;
            C1Cleargage.Cols[Col_nNotecount].AllowResizing = false;


            int styleLevel = 1;
            if (C1Cleargage != null && C1Cleargage.Rows.Count > 0)
            {

                string sReferenceNo = "";
                for (int i = 1; i < C1Cleargage.Rows.Count; i++)
                {
                    if ((sReferenceNo == Convert.ToString(C1Cleargage.GetData(i, Col_ReferenceNumber)) && styleLevel == 1) || i == 1)
                    {
                        for (int j = 0; j < Col_Count; j++)
                        {
                            C1Cleargage.SetCellStyle(i, j, "cs1");
                        }
                        styleLevel = 1;
                    }
                    else if (sReferenceNo == Convert.ToString(C1Cleargage.GetData(i, Col_ReferenceNumber)) && styleLevel == 2)
                    {
                        for (int j = 0; j < Col_Count; j++)
                        {
                            C1Cleargage.SetCellStyle(i, j, "cs2");
                        }
                        styleLevel = 2;
                    }
                    else
                    {
                        if (styleLevel == 1)
                        {
                            styleLevel = 2;
                            for (int j = 0; j < Col_Count; j++)
                            {
                                C1Cleargage.SetCellStyle(i, j, "cs2");
                            }
                        }
                        else
                        {
                            styleLevel = 1;
                            for (int j = 0; j < Col_Count; j++)
                            {
                                C1Cleargage.SetCellStyle(i, j, "cs1");
                            }
                        }

                    }
                    sReferenceNo = Convert.ToString(C1Cleargage.GetData(i, Col_ReferenceNumber));
                }
            }
            
        }
        
        private void txtSearchReadyToPost_TextChanged(object sender, EventArgs e)
        {
            string strSearch = "";
            DataView _dv = new DataView();
            string sFilter = "";
            try
            {
                _dv = (DataView)c1ReadyToPost.DataSource;
                if (_dv != null && c1trvReadyToPost.Rows.Count > 0)
                {
                    strSearch = txtSearchReadyToPost.Text.Trim();
                    strSearch = strSearch.Replace("'", "''").Replace("*", "%").Replace("[", "").Replace("]", "");
                    if (strSearch != "")
                    {
                        sFilter = "(" + _dv.Table.Columns["PatientName"].ColumnName + " like '%" + strSearch + "%') or" +
                            "(" + _dv.Table.Columns["sOriginalFileName"].ColumnName + " like '%" + strSearch + "%') or" +
                            "(" + _dv.Table.Columns["PaymentMethod"].ColumnName + " like '%" + strSearch + "%')or " +
                            "(" + _dv.Table.Columns["Action"].ColumnName + " like '%" + strSearch + "%') or " +
                            "(" + _dv.Table.Columns["dtSearchDate"].ColumnName + " like '%" + strSearch + "%') or" +
                            "(" + _dv.Table.Columns["EncounterID"].ColumnName + " like '%" + strSearch + "%')";
                    }
                    _dv.RowFilter = sFilter;
                    if (c1ReadyToPost.Rows.Count > 1 && c1ReadyToPost.Row > 0)
                    {
                        nPaymentTransactionID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_nPaymentTransactionID));
                        if (Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Action)).ToUpper() != Convert.ToString(Actions.DISCOUNT))
                        {
                            if (Convert.ToInt16(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Status)) == enumPaymentStatus.Pending.GetHashCode())
                            {
                                tsbViewPatPmnt.Enabled = false;
                            }
                            else
                            {
                                tsbViewPatPmnt.Enabled = true;
                            }

                            if (Convert.ToInt16(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Status)) == enumPaymentStatus.Pending.GetHashCode() && (Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Action)).ToUpper() == Convert.ToString(Actions.CREDIT) || Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Action)).Replace(" ", "").ToUpper() == Convert.ToString(Actions.FEECREDIT)))
                            {
                                tsb_ViewPatRefund.Enabled = false;
                            }
                            else
                            {
                                tsb_ViewPatRefund.Enabled = true;
                            }
                        }
                        else
                        {
                            tsbViewPatPmnt.Enabled = false;
                            tsb_ViewPatRefund.Enabled = false;
                        }
                        if (Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Action)).ToUpper() == Convert.ToString(Actions.PAYMENT) || Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Action)).ToUpper() == Convert.ToString(Actions.FEE))
                        {
                            if (Convert.ToInt16(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Status)) == enumPaymentStatus.Pending.GetHashCode())
                            {
                                tsb_PaymentPatient.Enabled = true;
                            }
                            else
                            {
                                tsb_PaymentPatient.Enabled = false;
                            }
                        }
                        else
                        {
                            tsb_PaymentPatient.Enabled = false;
                        }

                        if (Convert.ToInt16(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Status)) == enumPaymentStatus.Pending.GetHashCode())
                        {
                            tsb_MarkPosted.Enabled = true;
                        }
                        else
                        {
                            tsb_MarkPosted.Enabled = false;
                        }
                    }
                    DesignPaymentDetails(ref c1ReadyToPost);
                    //if (c1ReadyToPost.DataSource != null && c1ReadyToPost.Rows.Count > 0)
                    //{
                    //    for (int i = 1; i < c1ReadyToPost.Rows.Count; i++)
                    //    {
                    //        if (Convert.ToInt16(c1ReadyToPost.GetData(i, Col_Status)) == enumPaymentStatus.Pending.GetHashCode())
                    //        {
                    //            c1ReadyToPost.SetCellImage(i, Col_PendingStatus, global::gloBilling.Properties.Resources.HoldClaim);
                    //        }
                    //        else
                    //        {
                    //            c1ReadyToPost.SetCellImage(i, Col_PendingStatus, global::gloBilling.Properties.Resources.StatusNormal);
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
           
        }

        private void btnClearReadyToPost_Click(object sender, EventArgs e)
        {
            txtSearchReadyToPost.Clear();
            txtSearchReadyToPost.Focus();
        }
        
        private void tsb_PaymentPatient_Click(object sender, EventArgs e)
        {
            Int64 nPatientID = 0;
            Int64 nPAccountID=0;
            DateTime dtFromDOS=DateTime.MinValue;
            decimal Amount=0;
            string EncounterID = string.Empty;
            Int64 CleargageFileID = 0;
            string PaymentMethodType = string.Empty;
            string CG_TransactionID=string.Empty;
            string CG_OriginalTransactionID=string.Empty;
            string ReferenceNo=string.Empty;
            DataTable dtPaymentDetails = null;
            string PatientName = string.Empty;
            string PlanID = string.Empty;
            DateTime TimeStamp = DateTime.MinValue;
            string PaymentMethod = string.Empty;
            string sAction = string.Empty;
            string AccountType = string.Empty;
            string AccountNo = string.Empty;
            string PatientCode = string.Empty;
            gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2 = null;
            try
            {
                //if (txtSearchReadyToPost.Text != "")
                //{
                //    txtSearchReadyToPost.Clear();
                //}
                if (c1ReadyToPost.Rows.Count > 1)
                {
                    if (Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PatientID)) != 0 && Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PAccountID)) != 0)
                    {
                        nPatientID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PatientID));
                        nPAccountID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PAccountID));
                        CleargageFileID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_CleargageFileID));
                        EncounterID = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_EncounterID));
                        CG_TransactionID = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_TransactionID));
                        CG_OriginalTransactionID = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_OriginalTransactionID));
                        ReferenceNo = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_ReferenceNumber));
                        PatientName = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PatientName));
                        PlanID = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PaymentPlanID));
                        Amount = Convert.ToDecimal(Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Amount)).Substring(1));
                        TimeStamp = Convert.ToDateTime(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_TimeStamp));
                        PaymentMethod = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PaymentMethod));
                        sAction = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Action));
                        AccountType = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_AccountType));
                        AccountNo = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_AccountNumber));
                        PatientCode = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PatientCode));

                        oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
                        dtPaymentDetails = oclsCleargagePaymentPosting.GetPatientPaymentDetails(CleargageFileID, EncounterID, CG_TransactionID, CG_OriginalTransactionID, ReferenceNo);

                        if (nPatientID > 0)
                        {
                            frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(nPatientID, false, 0, 0, 0, 0, EOBPaymentSubType.Other);
                            frmPatientPaymentV2.StartPosition = FormStartPosition.CenterScreen;
                            frmPatientPaymentV2.WindowState = FormWindowState.Maximized;
                            frmPatientPaymentV2.ShowInTaskbar = false;
                            frmPatientPaymentV2.PAccountID = nPAccountID;
                            //frmPatientPaymentV2.CheckAmount = Amount;
                            frmPatientPaymentV2.IsFromCleargagePosting = true;
                            frmPatientPaymentV2.dtCleargagePaymentDetails = dtPaymentDetails;
                            frmPatientPaymentV2.ShowDialog(this);
                            //PatientPaymentID = frmPatientPaymentV2.ReturnPatientPaymentID;

                            int RowIndex = c1trvReadyToPost.Row;
                            c1trvReadyToPost.Select(RowIndex, COL_FileName);
                            FillClearGagePaymentPostingInfo();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Patient not present in system to view patient payment", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageFee, gloAuditTrail.ActivityType.Load, "Exception occured while Manual Cleargage payment loading for CleargageFileID" + Convert.ToString(c1ReadyToPost.Rows[1][Col_CleargageFileID]), nPatientID, Convert.ToInt64(c1ReadyToPost.Rows[1][Col_CleargageFileID]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                ex = null;
            }
            finally
            {
                if (frmPatientPaymentV2 != null)
                {
                    frmPatientPaymentV2.Dispose();
                    frmPatientPaymentV2 = null;
                }
                if (oclsCleargagePaymentPosting != null)
                {
                    oclsCleargagePaymentPosting.Dispose();
                    oclsCleargagePaymentPosting = null;
                }
            }
           
        }

        private void frmClearGagePaymentPosting_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (tabClearGage.SelectedTab.Tag.ToString()==TAB_READY)
            {
                if (c1ReadyToPost.RowSel>0)
                {
                    ClsCleargagePaymentPosting clsPosting = new ClsCleargagePaymentPosting();
                    clsPosting.InsertDeleteLockedFile(Convert.ToInt64(c1ReadyToPost.Rows[c1ReadyToPost.RowSel][Col_CleargageFileID]), gloGlobal.gloPMGlobal.UserID, 1, gloGlobal.gloPMGlobal.UserName, Environment.MachineName);
                    if (clsPosting != null)
                    {
                        clsPosting.Dispose();
                        clsPosting = null;
                    }  
                }
            }
            if (ofrmCleargagePaymentDistribution != null)
            {
                ofrmCleargagePaymentDistribution.Dispose();
                ofrmCleargagePaymentDistribution = null;
            }
        }
        private void RemoveSearchCleargageText()
        {
            try
            {
                if (tabClearGage.SelectedTab.Name == tbpg_ReadyToPost.Name)
                {
                    txtSearchReadyToPostFile.TextChanged -= new EventHandler(txtSearchAll_TextChanged);
                    txtSearchReadyToPostFile.Text = "";

                    txtSearchReadyToPost.TextChanged -= new EventHandler(txtSearchReadyToPost_TextChanged);
                    txtSearchReadyToPost.Text = "";
                }
                else if (tabClearGage.SelectedTab.Name == tbpg_Posted.Name)
                {
                    txtSearchPostedFile.TextChanged -= new EventHandler(txtSearchAll_TextChanged);
                    txtSearchPostedFile.Text = "";

                    txtSearchPosted.TextChanged -= new EventHandler(txtSearchPosted_TextChanged);
                    txtSearchPosted.Text = "";
                }
                else if (tabClearGage.SelectedTab.Name == tbpg_Hold.Name)
                {
                    txtSearchHold.TextChanged -= new EventHandler(txtSearchAll_TextChanged);
                    txtSearchHold.Text = "";
                }
                else if (tabClearGage.SelectedTab.Name == tbpg_Deleted.Name)
                {
                    txtSearchDeleted.TextChanged -= new EventHandler(txtSearchAll_TextChanged);
                    txtSearchDeleted.Text = "";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            } 
            finally
            {
                if (tabClearGage.SelectedTab.Name == tbpg_ReadyToPost.Name)
                {
                    txtSearchReadyToPostFile.TextChanged += new EventHandler(txtSearchAll_TextChanged);
                    txtSearchReadyToPost.TextChanged += new EventHandler(txtSearchReadyToPost_TextChanged);
                }
                else if (tabClearGage.SelectedTab.Name == tbpg_Posted.Name)
                {
                    txtSearchPostedFile.TextChanged += new EventHandler(txtSearchAll_TextChanged);
                    txtSearchPosted.TextChanged +=new EventHandler(txtSearchPosted_TextChanged);
                }
                else if (tabClearGage.SelectedTab.Name == tbpg_Hold.Name)
                {
                    txtSearchHold.TextChanged += new EventHandler(txtSearchAll_TextChanged);
                }
                else if (tabClearGage.SelectedTab.Name == tbpg_Deleted.Name)
                {
                    txtSearchDeleted.TextChanged += new EventHandler(txtSearchAll_TextChanged);
                }
            }
        }

        private void tsb_PostAdjustment_Click(object sender, EventArgs e)
        {
            Int16 Pending = 0;
            Int64 nCleargageFileID = 0;
            frmCleargagePaymentDistributionList ofrmCleargagePaymentDistribution = null;

            try
            {
                if (txtSearchReadyToPost.Text != "")
                {
                    txtSearchReadyToPost.Clear();
                }
                if (c1ReadyToPost.Rows.Count > 1)
                {
                    for (int i = 0; i < c1ReadyToPost.Rows.Count; i++)
                    {
                        //if (Convert.ToString(c1ReadyToPost.GetData(i, Col_Action)) == Convert.ToString(Actions.ADJUSTMENT) && Convert.ToString(c1ReadyToPost.GetData(i, Col_PaymentMethod)) == "" && Convert.ToInt16(c1ReadyToPost.Rows[i][Col_Status]) == 1)
                        //{
                        //    Pending = 1;
                        //    break;
                        //}
                        if (Convert.ToString(c1ReadyToPost.GetData(i, Col_Action)).ToUpper() == Convert.ToString(Actions.DISCOUNT) && Convert.ToString(c1ReadyToPost.GetData(i, Col_PaymentMethod)).ToUpper() == "" && Convert.ToInt16(c1ReadyToPost.Rows[i][Col_Status]) == enumPaymentStatus.Pending.GetHashCode())
                        {
                            Pending = 1;
                            break;
                        }
                    }
                    if (Pending == 1)
                    {
                         nCleargageFileID = Convert.ToInt64(c1trvReadyToPost.GetData(c1trvReadyToPost.Row, COL_CleargageFileID));
                        ofrmCleargagePaymentDistribution = new frmCleargagePaymentDistributionList(nCleargageFileID);
                        ofrmCleargagePaymentDistribution.WindowState = FormWindowState.Maximized;
                        ofrmCleargagePaymentDistribution.Icon = Properties.Resources.PostDiscount;
                        //ofrmCleargagePaymentDistribution.sAction = Convert.ToString(Actions.ADJUSTMENT); //"ADJUSTMENT";
                        ofrmCleargagePaymentDistribution.sAction = Convert.ToString(Actions.DISCOUNT); //"ADJUSTMENT";
                        ofrmCleargagePaymentDistribution.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No discount available for posting",gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    int RowIndex = c1trvReadyToPost.Row;
                    c1trvReadyToPost.Select(RowIndex, COL_FileName);
                }
                FillClearGagePaymentPostingInfo();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (ofrmCleargagePaymentDistribution != null)
                {
                    ofrmCleargagePaymentDistribution.Dispose();
                    ofrmCleargagePaymentDistribution = null;
                }
            }
        }
        
        private void c1ReadyToPost_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (c1ReadyToPost.Rows.Count > 1 && c1ReadyToPost.Row > 0)
                {
                    nPaymentTransactionID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_nPaymentTransactionID));
                    if (Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Action)).ToUpper() != Convert.ToString(Actions.DISCOUNT))
                    {
                        if (Convert.ToInt16(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Status)) ==enumPaymentStatus.Pending.GetHashCode())
                        {
                            tsbViewPatPmnt.Enabled = false;
                        }
                        else
                        {
                            tsbViewPatPmnt.Enabled = true;
                        }

                        if (Convert.ToInt16(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Status)) == enumPaymentStatus.Pending.GetHashCode() && (Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Action)).ToUpper() == Convert.ToString(Actions.CREDIT) || Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Action)).Replace(" ", "").ToUpper() == Convert.ToString(Actions.FEECREDIT)))
                        {                            
                            tsb_ViewPatRefund.Enabled = false;
                        }
                        else
                        {
                            tsb_ViewPatRefund.Enabled = true;
                        }
                    }
                    else
                    {
                        tsbViewPatPmnt.Enabled = false;
                        tsb_ViewPatRefund.Enabled = false;
                    }
                    if (Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Action)).ToUpper() == Convert.ToString(Actions.PAYMENT) || Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Action)).ToUpper() == Convert.ToString(Actions.FEE))
                    {
                        if (Convert.ToInt16(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Status)) == enumPaymentStatus.Pending.GetHashCode())
                        {
                            tsb_PaymentPatient.Enabled = true;
                        }
                        else
                        {
                            tsb_PaymentPatient.Enabled = false;
                        }
                    }
                    else
                    {
                        tsb_PaymentPatient.Enabled = false;
                    }

                    if (Convert.ToInt16(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Status)) == enumPaymentStatus.Pending.GetHashCode())
                    {
                        tsb_MarkPosted.Enabled = true;
                    }
                    else
                    {
                        tsb_MarkPosted.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
           
        }

        private void tsb_PostFee_Click(object sender, EventArgs e)
        {
            Int64 nPatientID = 0;
            Int64 nPAccountID = 0;
            DateTime dtFromDOS = DateTime.MinValue;
            Decimal dAmount = 0;
            string sMessage = string.Empty;
            int Pending = 0;
            try
            {
                if (c1ReadyToPost != null && c1ReadyToPost.Rows.Count > 1)
                {
                    for (int i = 1; i < c1ReadyToPost.Rows.Count; i++)
                    {
                        if (Convert.ToString(c1ReadyToPost.Rows[i][Col_PaymentMethod]).ToUpper() == Convert.ToString(PaymentMethod.CREDIT) && Convert.ToString(c1ReadyToPost.Rows[i][Col_Action]).ToUpper() == Convert.ToString(Actions.FEE) && Convert.ToString(c1ReadyToPost.Rows[i][Col_Status]) ==Convert.ToString(enumPaymentStatus.Pending.GetHashCode()))
                        {
                            Pending = 1;
                            break;
                        }
                    }
                    if (Pending == 1)
                    {                        
                        string _message = "System will create offset liability charge for Encounter Action 'Fee' and will post to match the cleargage balance." + Environment.NewLine + "Do you want to Continue? ";
                        DialogResult dg = MessageBox.Show(_message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dg == DialogResult.Yes)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            int nErrorPostFee = 0;                            
                            string sErrorMessage_Tray = string.Empty;
                            string sErrorMessage_ID = string.Empty;
                            string sNotPostedFeePatient = string.Empty;
                            for (int i = 1; i < c1ReadyToPost.Rows.Count; i++)
                            {
                                if (Convert.ToString(c1ReadyToPost.Rows[i][Col_PaymentMethod]).ToUpper() == Convert.ToString(PaymentMethod.CREDIT) && Convert.ToString(c1ReadyToPost.Rows[i][Col_Action]).ToUpper() == Convert.ToString(Actions.FEE) && Convert.ToString(c1ReadyToPost.Rows[i][Col_Status]) == Convert.ToString(enumPaymentStatus.Pending.GetHashCode()))
                                {
                                    oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
                                    if (Convert.ToString(c1ReadyToPost.Rows[i][Col_PatientID]) != null && Convert.ToString(c1ReadyToPost.Rows[i][Col_PatientID]) != "")
                                    {
                                        nPatientID = Convert.ToInt64(c1ReadyToPost.Rows[i][Col_PatientID]);
                                    }
                                    if (Convert.ToString(c1ReadyToPost.Rows[i][Col_PAccountID]) != null && Convert.ToString(c1ReadyToPost.Rows[i][Col_PAccountID]) != "")
                                    {
                                        nPAccountID = Convert.ToInt64(c1ReadyToPost.Rows[i][Col_PAccountID]);
                                    }
                                    dtFromDOS = Convert.ToDateTime(c1ReadyToPost.Rows[i][Col_ServiceDate]);
                                    string sAmount = Convert.ToString(c1ReadyToPost.Rows[i][Col_Amount]);
                                    dAmount = Convert.ToDecimal(sAmount.Replace('-', ' ').Trim());

                                    // sMessage = oclsCleargagePaymentPosting.GenerateCleargageFeeCharge(nPatientID, nPAccountID, dtFromDOS, dAmount);

                                    if (sMessage == "" && sMessage != null)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageFee, gloAuditTrail.ActivityType.Generate, "Cleargage Fee Charge Created for EncounterIDs : " + Convert.ToString(c1ReadyToPost.Rows[i][Col_EncounterID]), nPatientID, Convert.ToInt64(c1ReadyToPost.Rows[i][Col_CleargageFileID]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                       // oclsCleargagePaymentPosting.UpdateStatusAsPosted(Convert.ToString(c1ReadyToPost.Rows[i][Col_EncounterID]), Convert.ToInt64(c1ReadyToPost.Rows[i][Col_CleargageFileID]), Convert.ToString(c1ReadyToPost.Rows[i][Col_Action]), Convert.ToString(c1ReadyToPost.Rows[i][Col_PaymentMethod]),Convert.ToString(Actions.PAYMENT));
                                        oclsCleargagePaymentPosting.UpdateMasterDetailsStatus(Convert.ToInt64(c1ReadyToPost.Rows[i][Col_CleargageFileID]));
                                    }
                                    else
                                    {
                                        if (sMessage == "Charge Tray or Payment Tray is not available in system." && !sErrorMessage_Tray.Contains("Charge Tray or Payment Tray is not available in system."))
                                        {
                                            sErrorMessage_Tray = sErrorMessage_Tray + "Charge Tray or Payment Tray is not available in system.";
                                            MessageBox.Show("Charge Tray or Payment Tray is not available in system.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        if (sMessage == "Fee encounter data does not match with system record.\nPlease create fee charge manually and post it.")
                                        {
                                            nErrorPostFee = 1;
                                            if (sNotPostedFeePatient == "\n")
                                            {
                                                sNotPostedFeePatient = sNotPostedFeePatient + Convert.ToString(c1ReadyToPost.Rows[i][Col_EncounterID]);
                                            }
                                            else
                                            {
                                                sNotPostedFeePatient = sNotPostedFeePatient + "\n" + Convert.ToString(c1ReadyToPost.Rows[i][Col_EncounterID]);
                                            }

                                            if (!sErrorMessage_ID.Contains("Fee encounter data does not match with system record.\nPlease create fee charge manually and post it."))
                                            {
                                                sErrorMessage_ID = sErrorMessage_ID + "Fee encounter data does not match with system record.\nPlease create fee charge manually and post it.";
                                            }
                                        }                                        
                                    }
                                }
                            }
                            Cursor.Current = Cursors.Default;
                            if (nErrorPostFee > 0)
                            {
                                MessageBox.Show("Cleargage fee posting not done for \n" + sNotPostedFeePatient + "\nDue to " + sErrorMessage_ID, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Cleargage fee posting done successfully.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            FillClearGagePaymentPostingInfo();
                            c1trvReadyToPost.Select(c1trvReadyToPost.Row, COL_FileName);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No fee available for posting. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oclsCleargagePaymentPosting != null)
                {
                    oclsCleargagePaymentPosting.Dispose();
                    oclsCleargagePaymentPosting = null;
                }
            }
        }
        private void ShowHideButtons(string TAB_NAME)
        {
            if (TAB_NAME== TAB_READY)
            {
                tsb_Download.Visible = false;
                tsb_PaymentPatient.Visible = true;
                tsb_PostAdjustment.Visible = false;
                if (c1ReadyToPost.Rows.Count==1)
                {
                    tsbViewPatPmnt.Enabled = true;
                }
                tsb_PostPayment.Visible = true;
                tsb_MarkPosted.Visible = true;
                if (tsb_VoidCleargagePayment.Visible == false) 
                {
                    tsb_VoidCleargagePayment.Visible = false;
                }
            }
            if (TAB_NAME == TAB_POSTED)
            {
                tsb_Download.Visible = false;
                tsb_PaymentPatient.Visible = false;
                tsb_PostAdjustment.Visible = false;
                tsb_PostPayment.Visible = false;
                if (C1Posted.Rows.Count==1)
                {
                    tsbViewPatPmnt.Enabled = true;
                }
                //tsbViewPatPmnt.Enabled = true;
                tsb_MarkPosted.Visible = false;
                if (tsb_VoidCleargagePayment.Visible == true)
                {
                    tsb_VoidCleargagePayment.Visible = false;
                }
            }
            if (TAB_NAME == TAB_HOLD)
            {
                tsb_Download.Visible = false;
                tsb_PaymentPatient.Visible = false;
                tsb_PostAdjustment.Visible = false;
                
                tsb_PostPayment.Visible = false;
                if (tsb_VoidCleargagePayment.Visible == true)
                {
                    tsb_VoidCleargagePayment.Visible = false;
                }
            }
            if (TAB_NAME == TAB_DELETED) 
            {
                if (tsb_VoidCleargagePayment.Visible == true)
                {
                    tsb_VoidCleargagePayment.Visible = false;
                }
            }
        }

        private void c1trvPosted_AfterSelChange(object sender, RangeEventArgs e)
        {
            Int64 nCleargageFileID = 0;
            try
            {

                if (((C1FlexGrid)sender).RowSel >= 0)
                {
                    nCleargageFileID = Convert.ToInt64(((C1FlexGrid)sender).GetData(((C1FlexGrid)sender).RowSel, COL_CleargageFileID));
                    FillPostedFileDetails(nCleargageFileID);
                }
                txtSearchPosted.Clear();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
               
            }
        }

        private void FillPostedFileDetails(Int64 nCleargageFileID)
        {
            DataTable dtFileDetails = null;
            oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            try
            {
                dtFileDetails = oclsCleargagePaymentPosting.GetCleargageFileDetails(nCleargageFileID);
                C1Posted.BeginUpdate();
                C1Posted.DataSource = null;
                C1Posted.DataSource = dtFileDetails.DefaultView;
                if (dtFileDetails != null && dtFileDetails.Rows.Count > 0)
                {
                    lblPostedEncounter.Text = Convert.ToString(dtFileDetails.Rows[0]["Posted"]);
                    C1Posted.Select(1, 1);
                }
                else
                {
                    lblPostedEncounter.Text = "0";
                }
                DesignPaymentDetails(ref C1Posted);
                C1Posted.EndUpdate();
                //for (int i = 0; i < dtFileDetails.Rows.Count; i++)
                //{
                //    if (Convert.ToInt16(dtFileDetails.Rows[i][Col_Status]) == 1)
                //    {
                //        C1Posted.SetCellImage(i + 1, Col_PendingStatus, global::gloBilling.Properties.Resources.HoldClaim);
                //    }
                //    else
                //    {
                //        C1Posted.SetCellImage(i + 1, Col_PendingStatus, global::gloBilling.Properties.Resources.StatusNormal);
                //    }
                //}
              //  C1Posted.Select(C1Posted.Row, Col_PatientName);
                #region Design C1trvPosted
                if (c1trvPosted.DataSource != null && c1trvPosted.Rows.Count > 0)
                {
                    for (int i = 0; i < c1trvPosted.Rows.Count; i++)
                    {
                        c1trvPosted.SetCellImage(i, COL_CellImage, global::gloBilling.Properties.Resources.Bullet06);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oclsCleargagePaymentPosting != null)
                {
                    oclsCleargagePaymentPosting.Dispose();
                    oclsCleargagePaymentPosting = null;
                }
            }
            
        }

        private void txtSearchPosted_TextChanged(object sender, EventArgs e)
        {
            string strSearch = "";
            DataView _dv = new DataView();
            string sFilter = "";
            try
            {
                _dv = (DataView)C1Posted.DataSource;
                if (_dv != null && c1trvPosted.Rows.Count > 0)
                {
                    strSearch = txtSearchPosted.Text.Trim();
                    strSearch = strSearch.Replace("'", "''").Replace("*", "%").Replace("[", "").Replace("]", "");
                    if (strSearch != "")
                    {
                        sFilter = "(" + _dv.Table.Columns["PatientName"].ColumnName + " like '%" + strSearch + "%') or" +
                            "(" + _dv.Table.Columns["sOriginalFileName"].ColumnName + " like '%" + strSearch + "%') or" +
                            "(" + _dv.Table.Columns["PaymentMethod"].ColumnName + " like '%" + strSearch + "%')or" +
                            "(" + _dv.Table.Columns["Action"].ColumnName + " like '%" + strSearch + "%')or " +
                            "(" + _dv.Table.Columns["dtSearchDate"].ColumnName + " like '%" + strSearch + "%')or" +
                            "(" + _dv.Table.Columns["EncounterID"].ColumnName + " like '%" + strSearch + "%')";
                        
                                                    
                    }
                    _dv.RowFilter = sFilter;
                    
                    if (C1Posted.Rows.Count > 1 && C1Posted.Row > 0)
                    {
                        if (Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_Action)).ToUpper() != Convert.ToString(Actions.DISCOUNT))
                        {
                            if (Convert.ToInt16(C1Posted.GetData(C1Posted.Row, Col_Status)) == enumPaymentStatus.Pending.GetHashCode())
                            {
                                tsbViewPatPmnt.Enabled = false;
                            }
                            else
                            {
                                tsbViewPatPmnt.Enabled = true;
                            }

                            if (Convert.ToInt16(C1Posted.GetData(C1Posted.Row, Col_Status)) == enumPaymentStatus.Pending.GetHashCode() && (Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_Action)).ToUpper() == Convert.ToString(Actions.CREDIT) || Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_Action)).Replace(" ", "").ToUpper() == Convert.ToString(Actions.FEECREDIT)))
                            {
                                tsb_ViewPatRefund.Enabled = false;
                            }
                            else
                            {
                                tsb_ViewPatRefund.Enabled = true;
                            }
                        }
                        else
                        {
                            tsbViewPatPmnt.Enabled = false;
                            tsb_ViewPatRefund.Enabled = false;
                        }
                    }
                    DesignPaymentDetails(ref C1Posted);
                    //if (C1Posted.DataSource != null && C1Posted.Rows.Count > 0)
                    //{
                    //    for (int i = 1; i < C1Posted.Rows.Count; i++)
                    //    {
                    //        if (Convert.ToInt16(C1Posted.GetData(i, Col_Status)) == enumPaymentStatus.Pending.GetHashCode())
                    //        {
                    //            C1Posted.SetCellImage(i, Col_PendingStatus, global::gloBilling.Properties.Resources.HoldClaim);
                    //        }
                    //        else
                    //        {
                    //            C1Posted.SetCellImage(i, Col_PendingStatus, global::gloBilling.Properties.Resources.StatusNormal);
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void btnClearPosted_Click(object sender, EventArgs e)
        {
            txtSearchPosted.Clear();
            txtSearchPosted.Focus();
        }

        private void C1Posted_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1ReadyToPost_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void tsbViewPatPmnt_Click(object sender, EventArgs e)
        {
            DataTable dtViewPayment = null;
            string CG_TransactionID = string.Empty;
            string CG_OriginalTransactionID = string.Empty;
            string ReferenceNo = string.Empty;
            string PatientCode = string.Empty;
            string sCreditEventType = string.Empty;
            Int64 eobPaymentId = 0;
            Int64 nPatientID = 0;
            string sEncounterID = "";
            oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            gloAccountsV2.frmViewPatientPaymentV2 ofrmViewPatientPayment = null;
            try
            {
                switch (tabClearGage.SelectedTab.Tag.ToString())
                {

                    case TAB_READY:
                        {
                            if (c1ReadyToPost.Rows.Count > 1)
                            {
                                nPatientID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PatientID));
                                CG_TransactionID = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_TransactionID));
                                CG_OriginalTransactionID = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_OriginalTransactionID));
                                ReferenceNo = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_ReferenceNumber));
                                PatientCode = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PatientCode));
                                sCreditEventType = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_CreditEventType)).ToUpper();
                                sEncounterID = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_EncounterID));

                                dtViewPayment = ClsCleargagePaymentPosting.GetViewPaymentHistory(PatientCode, CG_TransactionID, CG_OriginalTransactionID, ReferenceNo, sEncounterID);

                                if (dtViewPayment != null && dtViewPayment.Rows.Count > 0)
                                {
                                    if (Convert.ToInt64(dtViewPayment.Rows[0]["nCreditRefID"]) != 0)//for Payment correction Event type CREDIT,VOID OR REJECT WE show REFUND details
                                    {
                                        eobPaymentId = Convert.ToInt64(dtViewPayment.Rows[0]["nCreditRefID"]);
                                    }
                                    else
                                    {
                                        eobPaymentId = Convert.ToInt64(dtViewPayment.Rows[0]["nCreditID"]);
                                    }
                                    ofrmViewPatientPayment = new gloAccountsV2.frmViewPatientPaymentV2(gloGlobal.gloPMGlobal.DatabaseConnectionString, nPatientID, gloGlobal.gloPMGlobal.ClinicID, eobPaymentId);
                                    ofrmViewPatientPayment.StartPosition = FormStartPosition.CenterScreen;
                                    ofrmViewPatientPayment.ShowDialog(this);
                                    ofrmViewPatientPayment.Dispose();
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.View, "View Ready to post Cleargage Payment", nPatientID, Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_CleargageFileID)), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                }
                                else if (dtViewPayment.Rows.Count == 0)
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.View, "View Ready to post mark posted record.", nPatientID, Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_CleargageFileID)), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                    MessageBox.Show("Selected row status mark as posted.\nFor more details please view notes.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        break;
                    case TAB_POSTED:
                        {
                            if (C1Posted.Rows.Count > 1)
                            {
                                tsbViewPatPmnt.Enabled = true;
                                nPatientID = Convert.ToInt64(C1Posted.GetData(C1Posted.Row, Col_PatientID));
                                CG_TransactionID = Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_TransactionID));
                                CG_OriginalTransactionID = Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_OriginalTransactionID));
                                ReferenceNo = Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_ReferenceNumber));
                                PatientCode = Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_PatientCode));
                                sCreditEventType = Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_CreditEventType)).ToUpper();
                                sEncounterID = Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_EncounterID));
                                dtViewPayment = ClsCleargagePaymentPosting.GetViewPaymentHistory(PatientCode, CG_TransactionID, CG_OriginalTransactionID, ReferenceNo, sEncounterID);

                                if (dtViewPayment != null && dtViewPayment.Rows.Count > 0)
                                {
                                    if (Convert.ToInt64(dtViewPayment.Rows[0]["nCreditRefID"]) != 0)//for Payment correction Event type CREDIT,VOID OR REJECT WE show REFUND details
                                    {
                                        eobPaymentId = Convert.ToInt64(dtViewPayment.Rows[0]["nCreditRefID"]);
                                    }
                                    else
                                    {
                                        eobPaymentId = Convert.ToInt64(dtViewPayment.Rows[0]["nCreditID"]);
                                    }
                                    ofrmViewPatientPayment = new gloAccountsV2.frmViewPatientPaymentV2(gloGlobal.gloPMGlobal.DatabaseConnectionString, nPatientID, gloGlobal.gloPMGlobal.ClinicID, eobPaymentId);
                                    ofrmViewPatientPayment.StartPosition = FormStartPosition.CenterScreen;
                                    ofrmViewPatientPayment.ShowDialog(this);
                                    ofrmViewPatientPayment.Dispose();
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.View, "View Posted Cleargage Payment", nPatientID, Convert.ToInt64(C1Posted.GetData(C1Posted.Row, Col_CleargageFileID)), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                }
                                else if (dtViewPayment.Rows.Count == 0)
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.View, "View Posted mark posted record.", nPatientID, Convert.ToInt64(C1Posted.GetData(C1Posted.Row, Col_CleargageFileID)), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                    MessageBox.Show("Selected row status mark as posted.\nFor more details please view notes.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.View, "Exception occured while View Cleargage Payment : " + ex.ToString(), nPatientID,0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                ex = null;
            }
            finally
            {
                if (oclsCleargagePaymentPosting != null)
                {
                    oclsCleargagePaymentPosting.Dispose();
                    oclsCleargagePaymentPosting = null;
                }
            }

        }

        private void C1Posted_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (C1Posted.Rows.Count > 1 && C1Posted.Row > 0)
                {
                    if (Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_Action)).ToUpper() != Convert.ToString(Actions.DISCOUNT))
                    {
                        if (Convert.ToInt16(C1Posted.GetData(C1Posted.Row, Col_Status)) == enumPaymentStatus.Pending.GetHashCode())
                        {
                            tsbViewPatPmnt.Enabled = false;
                        }
                        else
                        {
                            tsbViewPatPmnt.Enabled = true;
                        }

                        if (Convert.ToInt16(C1Posted.GetData(C1Posted.Row, Col_Status)) == enumPaymentStatus.Pending.GetHashCode() && (Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_Action)).ToUpper() == Convert.ToString(Actions.CREDIT) || Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_Action)).Replace(" ","").ToUpper() == Convert.ToString(Actions.FEECREDIT)))
                        {
                            tsb_ViewPatRefund.Enabled = false;
                        }
                        else
                        {
                            tsb_ViewPatRefund.Enabled = true;
                        }
                    }
                    else
                    {
                        tsbViewPatPmnt.Enabled = false;
                        tsb_ViewPatRefund.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tsb_VoidCleargagePayment_Click(object sender, EventArgs e)
        {
            Int16 Pending = 0;
            Int64 nCleargageFileID = 0;
            frmCleargagePaymentVoid ofrmCleargagePaymentVoid = null;
            try
            {                
                if (c1ReadyToPost.Rows.Count > 1)
                {
                    for (int i = 0; i < c1ReadyToPost.Rows.Count; i++)
                    {
                        //if ((Convert.ToString(c1ReadyToPost.GetData(i, Col_Action)).ToUpper() == Convert.ToString(Actions.CREDIT) || Convert.ToString(c1ReadyToPost.GetData(i, Col_Action)).Replace(" ", "").ToUpper() == Convert.ToString(Actions.FEECREDIT)) && Convert.ToInt16(c1ReadyToPost.Rows[i][Col_Status]) == enumPaymentStatus.Pending.GetHashCode())
                       if (Convert.ToString(c1ReadyToPost.GetData(i, Col_Action)).ToUpper() == Convert.ToString(Actions.CREDIT) && Convert.ToInt16(c1ReadyToPost.Rows[i][Col_Status]) == enumPaymentStatus.Pending.GetHashCode())
                        {
                            Pending = 1;
                            break;
                        }
                    }
                    if (Pending == 1)
                    {
                        nCleargageFileID = Convert.ToInt64(c1trvReadyToPost.GetData(c1trvReadyToPost.Row, COL_CleargageFileID));
                        ofrmCleargagePaymentVoid = new frmCleargagePaymentVoid(nCleargageFileID); 
                        ofrmCleargagePaymentVoid.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No Credit available for posting", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    int RowIndex = c1trvReadyToPost.Row;
                    c1trvReadyToPost.Select(RowIndex, COL_FileName);
                }
                FillClearGagePaymentPostingInfo();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (ofrmCleargagePaymentVoid != null)
                {
                    ofrmCleargagePaymentVoid.Dispose();
                    ofrmCleargagePaymentVoid = null;
                }
            }

        }
        private void AddCellStyleForGroups(C1.Win.C1FlexGrid.C1FlexGrid C1Cleargage)
        {
      
            C1.Win.C1FlexGrid.CellStyle cs1;
            C1.Win.C1FlexGrid.CellStyle cs2;
            C1.Win.C1FlexGrid.CellStyle cs3;
            C1.Win.C1FlexGrid.CellStyle cs4;
           
            try
            {

                if (C1Cleargage.Styles.Contains("cs1"))
                {
                    cs1 = C1Cleargage.Styles["cs1"];
                }
                else
                {
                    cs1 = C1Cleargage.Styles.Add("cs1");
                    cs1.BackColor = Color.FromArgb(229, 224,236); // Color.FromName("#90ee90");
                    cs1.Border.Color = Color.FromArgb(159,181,221);
                 
                }

                if (C1Cleargage.Styles.Contains("cs2"))
                {
                    cs2 = C1Cleargage.Styles["cs2"];
                }
                else
                {
                    cs2 = C1Cleargage.Styles.Add("cs2");
                    cs2.BackColor = Color.FromArgb(252, 253, 255);//Color.FromName("#ffa07a");
                    cs2.Border.Color = Color.FromArgb(159, 181, 221);
                }

                if (C1Cleargage.Styles.Contains("cs3"))
                {
                    cs3 = C1Cleargage.Styles["cs3"];
                }
                else
                {
                    cs3 = C1Cleargage.Styles.Add("cs3");
                    cs3.BackColor = Color.FromArgb(126, 56, 121);// Color.FromName("#dda0dd");
                    cs3.Border.Color = Color.FromArgb(126, 56, 121);
                }

                if (C1Cleargage.Styles.Contains("cs4"))
                {
                    cs4 = C1Cleargage.Styles["cs4"];
                }
                else
                {
                    cs4 = C1Cleargage.Styles.Add("cs4");
                    cs4.BackColor = Color.FromArgb(77, 130, 184);//Color.FromName("#ffc0cb"); ;
                    cs4.Border.Color = Color.FromArgb(77, 130, 184);
                }

               
            }
            catch
            {
                cs1 = C1Cleargage.Styles.Add("cs1");
                cs2 = C1Cleargage.Styles.Add("cs2");
                cs3 = C1Cleargage.Styles.Add("cs3");
                cs4 = C1Cleargage.Styles.Add("cs4");               
            }

        }

        private void tsb_ViewPatRefund_Click(object sender, EventArgs e)
        {
            Int64 nRefundID = 0;
            Int64 nPatientID = 0;
            String CG_TransactionID = String.Empty;
            String CG_OriginalTransactionID = String.Empty;
            String ReferenceNo = String.Empty;
            String PatientCode = String.Empty;
            string EncounterID = string.Empty;
            DataTable dtViewPayment = null;
            try
            {
                if (c1ReadyToPost.Rows.Count > 1 && c1ReadyToPost.Row > 0)
                {                    
                    nPatientID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.RowSel, Col_PatientID));
                    CG_TransactionID = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_TransactionID));
                    CG_OriginalTransactionID = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_OriginalTransactionID));
                    ReferenceNo = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_ReferenceNumber));
                    PatientCode = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PatientCode));
                    EncounterID = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_EncounterID));
                }
                else if(C1Posted.Rows.Count > 1 && C1Posted.Row > 0)
                {
                    nPatientID = Convert.ToInt64(C1Posted.GetData(C1Posted.RowSel, Col_PatientID));
                    CG_TransactionID = Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_TransactionID));
                    CG_OriginalTransactionID = Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_OriginalTransactionID));
                    ReferenceNo = Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_ReferenceNumber));
                    PatientCode = Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_PatientCode));
                    EncounterID = Convert.ToString(C1Posted.GetData(C1Posted.Row, Col_EncounterID));
                }

                dtViewPayment = ClsCleargagePaymentPosting.GetViewPaymentHistory(PatientCode, CG_TransactionID, CG_OriginalTransactionID, ReferenceNo, EncounterID);
                if (dtViewPayment != null && dtViewPayment.Rows.Count > 0)                 
                {
                    nRefundID = Convert.ToInt64(dtViewPayment.Rows[0]["nRefundID"]);
                }
                if (nRefundID != 0 && nPatientID != 0)
                {
                    frmPatientPayRefundViewV2 ofrmPatientPayRefundView = new frmPatientPayRefundViewV2(gloGlobal.gloPMGlobal.DatabaseConnectionString, nPatientID, Convert.ToInt64(nRefundID));
                    ofrmPatientPayRefundView.ShowDialog(this);
                    ofrmPatientPayRefundView.Dispose();
                }
                else
                {
                    MessageBox.Show("Refund details are not available.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);            
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (dtViewPayment != null)
                { dtViewPayment.Dispose(); dtViewPayment = null; }
            }
        }

        private void tls_btnPatAcct_Click(object sender, EventArgs e)
        {
            Int64 _nPatientID = 0;
            Int64 _nPAccountID = 0;
            frmPatientFinancialViewV2 frm = null;
            try
            {
               
                switch (tabClearGage.SelectedTab.Tag.ToString())
                {

                    case TAB_READY:
                        {
                            if (c1ReadyToPost.Rows.Count > 1)
                            {
                                _nPatientID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PatientID));
                                _nPAccountID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PAccountID));
                                if (_nPatientID != 0)
                                {
                                    frm = new frmPatientFinancialViewV2(_nPatientID);
                                    frm.StartPosition = FormStartPosition.CenterScreen;
                                    frm.WindowState = FormWindowState.Maximized;
                                    frm.ShowInTaskbar = false;
                                    frm.IsCalledFromInsPmt = false;
                                    frm._nSelectAccountId = _nPAccountID;
                                    frm.ShowDialog(this);
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.View, "View account information.", _nPatientID, Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_CleargageFileID)), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                }
                                else
                                {
                                    MessageBox.Show("Patient not present in system to view account information", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        break;
                    case TAB_POSTED:
                        {
                            if (C1Posted.Rows.Count > 1)
                            {
                                _nPatientID = Convert.ToInt64(C1Posted.GetData(C1Posted.Row, Col_PatientID));
                                _nPAccountID = Convert.ToInt64(C1Posted.GetData(C1Posted.Row, Col_PAccountID));
                                if (_nPatientID != 0)
                                {
                                    frm = new frmPatientFinancialViewV2(_nPatientID);
                                    frm.StartPosition = FormStartPosition.CenterScreen;
                                    frm.WindowState = FormWindowState.Maximized;
                                    frm.ShowInTaskbar = false;
                                    frm.IsCalledFromInsPmt = false;
                                    frm._nSelectAccountId = _nPAccountID;
                                    frm.ShowDialog(this);
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.View, "View account information.", _nPatientID, Convert.ToInt64(C1Posted.GetData(C1Posted.Row, Col_CleargageFileID)), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                }
                                else
                                {
                                    MessageBox.Show("Patient not present in system to view account information.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.View, "Exception occured while View account information.", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            }
            finally
            {
                if (frm != null) { frm.Dispose(); }
            }
        }

        private void tsbNotes_Click(object sender, EventArgs e)
        {
            Int64 CleargageFileID = 0;
            switch (tabClearGage.SelectedTab.Tag.ToString())
            {

                case TAB_READY:
                    {
                        if (c1ReadyToPost.Rows.Count > 1)
                        {
                            CleargageFileID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_CleargageFileID));
                            nPaymentTransactionID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_nPaymentTransactionID));
                            ShowNotes();
                            FillCleargageFileDetails(CleargageFileID);
                       }
                    }
                    break;
                case TAB_POSTED:
                    {
                        if (C1Posted.Rows.Count > 1)
                        {
                            CleargageFileID = Convert.ToInt64(C1Posted.GetData(C1Posted.Row, Col_CleargageFileID));
                            nPaymentTransactionID = Convert.ToInt64(C1Posted.GetData(C1Posted.Row, Col_nPaymentTransactionID));
                            ShowNotes();
                            FillPostedFileDetails(CleargageFileID);
                        }
                    }
                    break;
            }

            
           
            
        }

        private void ShowNotes()
        {
            frmCGNotes ofrmCGNotes = new frmCGNotes(nPaymentTransactionID, false);
            ofrmCGNotes.ShowDialog();
        }

        private void tsb_MarkPosted_Click(object sender, EventArgs e)
        {
            Int64 _nCleargageFileID = 0;
            string _sOriginalTransactionID = string.Empty;
            string _sTransactionID = string.Empty;
            string _sReferenceNo = string.Empty;
            string _sEncounterID = string.Empty;
            string _sAction = string.Empty;
            Int64 nNoteID = 0;
            string nPatientID = "0";
            ClsCleargagePaymentPosting oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            try
            {
                if (c1ReadyToPost.Rows.Count > 1)
                {
                    if (MessageBox.Show("Do you want to mark status as posted.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        nPaymentTransactionID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_nPaymentTransactionID));
                        frmCGNotes ofrmCGNotes = new frmCGNotes(nPaymentTransactionID, false);
                        ofrmCGNotes.IsCallFromMarkPosted = true;
                        ofrmCGNotes.ShowDialog();
                        nNoteID = ofrmCGNotes.nReturnNoteID;
                        if (nNoteID > 0)
                        {

                            _nCleargageFileID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_CleargageFileID));
                            _sOriginalTransactionID = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_OriginalTransactionID));
                            _sTransactionID = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_TransactionID));
                            _sReferenceNo = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_ReferenceNumber));
                            _sEncounterID = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_EncounterID));
                            _sAction = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_Action));
                            if (_sAction.ToUpper() == Convert.ToString(Actions.PAYMENT) || _sAction.ToUpper() == Convert.ToString(Actions.FEE))
                            {
                                _sAction = Convert.ToString(Actions.PAYMENT);
                            }

                            bool result = oclsCleargagePaymentPosting.UpdateStatusAsPosted(_sEncounterID, _nCleargageFileID, _sTransactionID, _sOriginalTransactionID, _sReferenceNo, _sAction);
                            oclsCleargagePaymentPosting.UpdateMasterDetailsStatus(_nCleargageFileID);
                            nPatientID = c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PatientID) == DBNull.Value ? "0" : Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_PatientID));
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.View, "Mark As Posted = " + Convert.ToString(result), Convert.ToInt64(nPatientID), _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                            FillClearGagePaymentPostingInfo();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.View, "Exception occured while Mark As Posted", Convert.ToInt64(nPatientID), Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.Row, Col_CleargageFileID)), 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
            }
            finally
            {
                if (oclsCleargagePaymentPosting != null)
                {
                    oclsCleargagePaymentPosting.Dispose();
                    oclsCleargagePaymentPosting = null;
                }
            }
        }

    }
}
