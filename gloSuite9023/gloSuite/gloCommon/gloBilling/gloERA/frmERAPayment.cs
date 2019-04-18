using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using gloBilling.Payment;
using gloDateMaster;
using System.ComponentModel;
using C1.Win.C1FlexGrid;
using Microsoft.Reporting.WinForms;

namespace gloBilling.gloERA
{
    public partial class frmERAPayment : Form
    {

        #region " Variable Declarations "

        private ToolTip oToolTip = null;
        private Font oFontRegular = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
        private Font oFontBold = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
        Timer oTimer = new Timer();
        DateTime _CurrentTime;
        private string _DataBaseConnectionString = "";
        private string _MessageBoxCaption = "";
        private Int64 _UserID = 0;
        private Int64 _ClinicID = 1;
        private String _practice;
        private String _ClaimProcessedCountComment;
        private String _BalError;
        private String _UserName;
        private bool _bIsSaved = false;
        private Int64 _SelectedTrayID = 0;
        string _SearchText;
        private string _SelectedTrayName = "";
        private DateTime _PostedDateRange= DateTime.Now.AddDays(-7).Date;
        private bool _IsFormLoading = false;

        SSRSApplication.frmSSRSViewer frmSSRS = null;
        String strFileName = "";
        StopFlag sStopFlag;
        private enum enum_ReportType
        {
            None = 0,
            EOB = 1,
            Posting = 2,
            Exception = 3
        }

        private gloERA oERA = null;

        private gloDatabaseLayer.DBLayer oDB = null;
        private gloDatabaseLayer.DBParameters oDBPara = null;

        string _ServerPath = "";
        string _835FolderPath = "";
        Int32 _Width;
        string _TempStr = "";
        Int64 _TempID = 0;
        Int64 _nOperationID = 0;
        Int64 _FileId = 0;
        Int64 nSelectedRowID = 0;
        Int64 nSelectedRowReadyID = 0;
        Int64 nSelectedRowPostedID = 0;
        Int64 nSelectedRowHoldID = 0;
        Int64 nSelectedRowDeleteID = 0;
        int nSortColumn = 0;
        int IRow = 1;
        C1.Win.C1FlexGrid.SortFlags eSortOrder = C1.Win.C1FlexGrid.SortFlags.None;

        #region " C1 Constants "

        private const int COL_ERAFileID = 0;
        private const int COL_ISAID = 1;
        private const int COL_BPRID = 2;
        private const int COL_UpdateDate = 3;
        private const int COL_ImportDate = 4;
        private const int COL_FileName = 5;
       // private const int COL_OrigFileName = 6;
        private const int COL_ProdDate = 6;
        private const int COL_PayerID = 7;
        private const int COL_PayerName = 8;
        private const int COL_PayerContact = 9;
        private const int COL_PayMethod = 10;
        private const int COL_CheckNo = 11;
        private const int COL_CheckDate = 12;
        private const int COL_CheckDateNumeric = 13;
        private const int COL_CheckAmount = 14;
        private const int COL_CheckAmountHidden = 15;
        private const int COL_OrigFileName = 16;
        private const int COL_TotalClaimPaid = 17;
        private const int COL_TotalPLBAmount = 18;
        private const int COL_IsFullyPosted = 19;
        private const int COL_NotesCount = 20;
        private const int COL_CloseDate = 21;
        private const int COL_PaymentTray = 22;
        private const int COL_PostUser = 23;
        private const int COL_DateDiffinDays = 26;

        #endregion

        #region " Tab Constants "
        private const string TAB_READY = "ReadyToPost";
        private const string TAB_DELETED = "Deleted";
        private const string TAB_POSTED = "Posted";
        private const string TAB_HOLD = "Hold";
        #endregion

        #region " Server Path Constants "
        private const string _BASE_FOLDER = "Claim Management";
        private const string _INBOX_FOLDER = "InBox";        private const string _CLAIM_FOLDER = "835 Remittance Advice";
        
        #endregion


        #endregion

        #region " Public Properties "
        public Int64 SelectedPaymentTrayID
        {
            get { return _SelectedTrayID; }
            set
            {
                _SelectedTrayID = value;
                frmSSRS.lblPaymentTray.Tag = _SelectedTrayID;

            }
        }
        public string SelectedPaymentTray
        {
            get { return _SelectedTrayName; }
            set
            {
                _SelectedTrayName = value;

                //                SSRSApplication.frmSSRSViewer frmSSRS = new SSRSApplication.frmSSRSViewer();
                frmSSRS.lblPaymentTray.Text = _SelectedTrayName;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        #endregion

        #region " Constructor "
        public frmERAPayment()
        {

            #region " Get MessageBoxCaption,Clinic ID ,UserID,Databaseconnection Stringfrom AppSettings "
            _MessageBoxCaption = Convert.ToString(gloGlobal.gloPMGlobal.MessageBoxCaption);
            _DataBaseConnectionString = Convert.ToString(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            _ClinicID = Convert.ToInt64(gloGlobal.gloPMGlobal.ClinicID);
            _UserID = Convert.ToInt64(gloGlobal.gloPMGlobal.UserID);
            #endregion
            InitializeComponent();
        }
        #endregion

        #region " Form Events "

        private void frmERAPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (oToolTip != null)
            {
                oToolTip.RemoveAll();
                oToolTip.Dispose();
                oToolTip = null;
            }

            //if (oFontRegular != null) { oFontRegular.Dispose(); oFontRegular = null; }
            //if (oFontBold != null) { oFontBold.Dispose(); oFontBold = null; }
            if (oERA != null) { oERA.Dispose(); oERA = null; }
            if (oDB != null) { oDB.Dispose(); oDB = null; }
            if (oDBPara != null) { oDBPara.Dispose(); oDBPara = null; }
        }

        private void frmERAPayment_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }


        void oTimer_Tick(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox txtSearch = null;

            if (tabERA.SelectedTab.Tag.ToString() == TAB_READY)
            {
                txtSearch = txtSearchReadyToPost;
            }
            if (tabERA.SelectedTab.Tag.ToString() == TAB_POSTED)
            {
                txtSearch = txtSearchPosted;
            }
            if (tabERA.SelectedTab.Tag.ToString() == TAB_HOLD)
            {
                txtSearch = txtSearchHold;
            }
            if (tabERA.SelectedTab.Tag.ToString() == TAB_DELETED)
            {
                txtSearch = txtSearchDeleted;
            }
            if (txtSearch != null)
            {
                _SearchText = txtSearch.Text;
                if (txtSearch.Text.Trim() != "")
                {
                    // IF LAST KEY PRESS TIME DIFFERENCE IS 100 Seconds THEN SEARCHING WILL BE START //
                    if (DateTime.Now.Subtract(_CurrentTime).Milliseconds > 100)
                    {
                        oTimer.Stop();
                        searchERACheck();
                    }
                }
                else
                {
                    oTimer.Stop();
                    searchERACheck();
                }
            }

        }

        private void frmERAPayment_Load(object sender, EventArgs e)
        {
            _IsFormLoading = true;
            try
            {

                #region " Set Tool Tips "
                oToolTip = new ToolTip();
                oToolTip.SetToolTip(btnClearSearchReadyToPost, "Clear Search");
                oToolTip.SetToolTip(btnClearSearchDeleted, "Clear Search");
                oToolTip.SetToolTip(btnClearSearchPosted, "Clear Search");
                oToolTip.SetToolTip(btnClearSearchHold, "Clear Search");
                oToolTip.SetToolTip(btnUP, "Show Additional Information");
                oToolTip.SetToolTip(btnDown, "Hide Additional Information");
                oToolTip.SetToolTip(btnShowChecksPosted, "Show Checks");
                oToolTip.SetToolTip(btnShowChecksDeleted, "Show Checks");
                oToolTip.SetToolTip(btnShowChecksHold, "Show Checks");
                #endregion

                lblProgress.Visible = false;
                prgProgress.Visible = false;
                btnUP.Visible = false;
                mskFromDatePosted.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskFromDateDeletd.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskFromDateHold.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskToDatePosted.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskToDateDeleted.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskToDateHold.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                FillClearingHouseMenus(tsb_Download);
                _FillPostedCombo();
                oTimer.Tick += new EventHandler(oTimer_Tick);
                ShowMessageLogs("");

                tsb_DetailPaperEOB.Visible = GetDetailsReportSetting("Show Detail Paper EOB on ERA Posting");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _IsFormLoading = false;
            }
        }

        private void frmERAPayment_Shown(object sender, EventArgs e)
        {
            RefreshView();
        }
        #endregion

        #region " Tool Strip Button Events "
        private void tsb_Import_Click(object sender, EventArgs e)
        {
            try
            {
                #region " Import ERA Files "
                _ServerPath = GetServerPath();
                OpenFileDialog oDialog = new OpenFileDialog();
                oDialog.Title = "Import";
                oDialog.Filter = "All Remittance Files (*.RMT;*.txt)|*.RMT;*.txt|Remittance Files (*.RMT)|*.RMT|Text Documents (*.txt)|*.txt";

                _835FolderPath = _ServerPath + "\\" + _BASE_FOLDER + "\\" + _INBOX_FOLDER + "\\" + _CLAIM_FOLDER;
                if (_ServerPath != "")
                    oDialog.InitialDirectory = _835FolderPath;
                oDialog.ValidateNames = true;
                oDialog.CheckFileExists = true;
                oDialog.CheckPathExists = true;
                oDialog.Multiselect = true;
                if (oDialog.ShowDialog(this) == DialogResult.OK)
                {
                    ImportERAFiles(oDialog.FileNames);
                }
                oDialog.Dispose();
                oDialog = null;

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_ERAFiles_Click(object sender, EventArgs e)
        {
            try
            {
                frmERAFiles oFrm = new frmERAFiles();
                oFrm.WindowState = FormWindowState.Maximized;
                oFrm.ShowDialog(this);
                //RefreshView();
                ShowMessageLogs(oFrm.MessageLog);
                oFrm.Dispose();
                oFrm = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                RefreshView();
            }
        }

        private void tsb_Trial_Click(object sender, EventArgs e)
        {

        }

        private void tsb_Post_Click(object sender, EventArgs e)
        {
            clsERAPostingV2 oERAPosting = null;
            tls_Main.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                // TEMPORARY COMMENT //
                //frmCloseDate ofrmlosedate = new frmCloseDate();
                //ofrmlosedate.WindowState = FormWindowState.Normal;
                //ofrmlosedate.StartPosition = FormStartPosition.CenterParent;
                //ofrmlosedate.ShowDialog();
                //return; 

                _TempID = 0;
                _bIsSaved = false;
                if (tabERA.SelectedTab.Tag.ToString() == TAB_READY && c1ReadyToPost.RowSel >= 0)
                {
                    _TempID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.RowSel, COL_BPRID));
                    if (_TempID <= 0) return;
                    //    SetCheckStatus(_TempID, enumCheckStatus.Posted);
                    #region " POSTING "
                    oERAPosting = new clsERAPostingV2();
                    string sMessage = string.Empty;
                    _FileId = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.RowSel, COL_ERAFileID));
                  
                    DataTable _dtUniqueOperationID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                    if (_dtUniqueOperationID != null && _dtUniqueOperationID.Rows.Count > 0)
                    {
                        _nOperationID = Convert.ToInt64(_dtUniqueOperationID.Rows[0]["ID"].ToString());

                    }

                    if (!oERAPosting.PostERAFile_Temp(_TempID, _nOperationID, out sMessage, out sStopFlag, ref prgProgress, ref lblProgress))
                    {
                        //If any Error

                        if (sStopFlag == StopFlag.NoClaimProcessed)
                        {

                            DialogResult _dlgRst = DialogResult.None;
                            string sMsg = string.Empty;
                            sMsg = string.Format(sMessage + "\n\nContinue to view ERA Posting Report?");
                            _dlgRst = MessageBox.Show(sMsg, _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_dlgRst == DialogResult.Yes)
                            {
                                if (c1ReadyToPost.GetData(c1ReadyToPost.RowSel, COL_NotesCount).ToString() != "0")
                                    using (frmNotes ofrm = new frmNotes(_DataBaseConnectionString, _ClinicID, enumNoteType.Check, _TempID, false))
                                    {
                                        ofrm.ShowDialog(this);
                                    }

                                ShowSSRSReport(enum_ReportType.Posting);

                            }
                            //{
                            //    //frmPostingERA oRpt_EOB = new frmPostingERA(_TempID, _FileId, _UserID); // ONLY FOR SELECTED CHECK //
                            //    //oRpt_EOB.WindowState = FormWindowState.Maximized;
                            //    //oRpt_EOB.ShowInTaskbar = false;
                            //    //oRpt_EOB.StartPosition = FormStartPosition.CenterParent;
                            //    //oRpt_EOB.ShowDialog();
                            //    //oRpt_EOB.Dispose();
                            //    //oRpt_EOB = null;

                                //}

                            else
                                ClsERAValidation.UpdateCheckStatus(_TempID, enumCheckStatus.Ready.GetHashCode());

                        }
                        else
                        {
                            MessageBox.Show(sMessage, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            if (sStopFlag != StopFlag.CheckOpened)
                                ClsERAValidation.UpdateCheckStatus(_TempID, enumCheckStatus.Ready.GetHashCode());
                        }
                    }
                    else
                    {

                        //frmPostingERA oRpt_EOB = new frmPostingERA(_TempID, _FileId, _UserID); // ONLY FOR SELECTED CHECK //
                        //oRpt_EOB.WindowState = FormWindowState.Maximized;
                        //oRpt_EOB.ShowInTaskbar = false;
                        //oRpt_EOB.StartPosition = FormStartPosition.CenterParent;
                        //oRpt_EOB.ShowDialog();
                        //oRpt_EOB.Dispose();
                        //oRpt_EOB = null;
                        if (c1ReadyToPost.GetData(c1ReadyToPost.RowSel, COL_NotesCount).ToString() != "0")
                            using (frmNotes ofrm = new frmNotes(_DataBaseConnectionString, _ClinicID, enumNoteType.Check, _TempID, false))
                            {
                                ofrm.ShowDialog(this);
                            }
                        ShowSSRSReport(enum_ReportType.Posting);
                    }

                    #endregion



                    // IF ALL CHECKS ARE POSTED FROM CURRENT FILE THEN MARK FILE STATUS AS FINISHED //
                    if (IsAllChecksPosted(_FileId))
                    {
                        oERA = new gloERA();
                        oERA.SetFileStatus(_FileId, enumERAFileStatus.Finished);
                        oERA.Dispose();
                        oERA = null;
                    }


                }



                //RefreshView();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
             
                RefreshView();
                this.Cursor = Cursors.Default;
                tls_Main.Enabled = true;
                prgProgress.Visible = false;
                lblProgress.Visible = false;
                InsurancePayment.UnlockCheckClaims(_TempID);
            }
        }

        private void tsb_PaperEOB_Click(object sender, EventArgs e)
        {
            try
            {
                _TempID = 0;
                if (tabERA.SelectedTab.Tag.ToString() == TAB_READY)
                {
                    if (c1ReadyToPost.RowSel >= 0)
                        _TempID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.RowSel, COL_BPRID));
                }
                else if (tabERA.SelectedTab.Tag.ToString() == TAB_POSTED)
                {
                    if (c1Posted.RowSel >= 0)
                        _TempID = Convert.ToInt64(c1Posted.GetData(c1Posted.RowSel, COL_BPRID));
                }
                else if (tabERA.SelectedTab.Tag.ToString() == TAB_DELETED)
                {
                    if (c1Deleted.RowSel >= 0)
                        _TempID = Convert.ToInt64(c1Deleted.GetData(c1Deleted.RowSel, COL_BPRID));
                }
                else if (tabERA.SelectedTab.Tag.ToString() == TAB_HOLD)
                {
                    if (c1Hold.RowSel >= 0)
                        _TempID = Convert.ToInt64(c1Hold.GetData(c1Hold.RowSel, COL_BPRID));
                }

                if (_TempID > 0)
                {
                    //frmRpt_PaperEOB oRpt_EOB = new frmRpt_PaperEOB(_TempID, 0, _UserID); // ONLY FOR SELECTED CHECK //
                    //oRpt_EOB.WindowState = FormWindowState.Maximized;
                    //oRpt_EOB.ShowInTaskbar = false;
                    //oRpt_EOB.StartPosition = FormStartPosition.CenterParent;
                    //oRpt_EOB.ShowDialog();
                    //oRpt_EOB.Dispose();
                    //oRpt_EOB = null;
                    ShowSSRSReport(enum_ReportType.EOB);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }


        }

        private void tsb_View_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _RowSel = -1;
                _TempID = 0;
                _TempStr = tabERA.SelectedTab.Tag.ToString();


                if (_TempStr == TAB_READY)
                    _RowSel = c1ReadyToPost.RowSel;
                else if (_TempStr == TAB_DELETED)
                    _RowSel = c1Deleted.RowSel;
                else if (_TempStr == TAB_POSTED)
                    _RowSel = c1Posted.RowSel;
                else if (_TempStr == TAB_HOLD)
                    _RowSel = c1Hold.RowSel;

                if (_RowSel >= 0)
                {
                    if (_TempStr == TAB_READY)
                    {
                        _TempID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.RowSel, COL_ERAFileID));
                        _TempStr = Convert.ToString(c1ReadyToPost.GetData(c1ReadyToPost.RowSel, COL_OrigFileName));
                    }
                    else if (_TempStr == TAB_DELETED)
                    {
                        _TempID = Convert.ToInt64(c1Deleted.GetData(c1Deleted.RowSel, COL_ERAFileID));
                        _TempStr = Convert.ToString(c1Deleted.GetData(c1Deleted.RowSel, COL_OrigFileName));
                    }
                    else if (_TempStr == TAB_POSTED)
                    {
                        _TempID = Convert.ToInt64(c1Posted.GetData(c1Posted.RowSel, COL_ERAFileID));
                        _TempStr = Convert.ToString(c1Posted.GetData(c1Posted.RowSel, COL_OrigFileName));
                    }
                    else if (_TempStr == TAB_HOLD)
                    {
                        _TempID = Convert.ToInt64(c1Hold.GetData(c1Hold.RowSel, COL_ERAFileID));
                        _TempStr = Convert.ToString(c1Hold.GetData(c1Hold.RowSel, COL_OrigFileName));
                    }

                    oERA = new gloERA();
                    DataTable dtFile;
                    dtFile = oERA.GetERAFile(_TempID);
                    if (dtFile != null && dtFile.Rows.Count > 0)
                    {
                        _TempStr = Supporting.ConvertBinaryToFile(dtFile.Rows[0]["iBinaryFile"], _TempStr);
                        if (File.Exists(_TempStr))
                            System.Diagnostics.Process.Start(_TempStr);
                    }

                    if (dtFile != null)
                    {
                        dtFile.Dispose();
                        dtFile = null;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oERA != null)
                {
                    oERA.Dispose();
                    oERA = null;
                }
            }
        }

        private void tsb_MarkDeleted_Click(object sender, EventArgs e)
        {
            try
            {
                _TempID = 0;
                if (tabERA.SelectedTab.Tag.ToString() == TAB_READY && c1ReadyToPost.RowSel >= 0)
                {
                    if (MessageBox.Show("ERA Check will be marked Deleted. " + Environment.NewLine + "Continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _TempID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.RowSel, COL_BPRID));
                        SetCheckStatus(_TempID, enumCheckStatus.MarkedDeleted);
                        using (frmNotes ofrm = new frmNotes(_DataBaseConnectionString, _ClinicID, enumNoteType.Check, _TempID, true))
                        {
                            ofrm.ShowDialog(this);
                        }
                        // RefreshView();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                RefreshView();
            }
        }

        private void tsb_UndoDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _TempID = 0;
                if (tabERA.SelectedTab.Tag.ToString() == TAB_DELETED && c1Deleted.RowSel >= 0)
                {
                    if (MessageBox.Show("ERA Check will no longer be Deleted. ERA Check will be set Ready to Post. " + Environment.NewLine + "Continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _TempID = Convert.ToInt64(c1Deleted.GetData(c1Deleted.RowSel, COL_BPRID));
                        SetCheckStatus(_TempID, enumCheckStatus.Ready);
                        RefreshView();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
                    
            if (Cursor == Cursors.Default)
                this.Close();
        }

        private void tsb_ExportToExcel_Click(object sender, EventArgs e)
        {
            string _FilePath = "";

            try
            {

                FileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel File(.xls)|*.xls";
                saveFileDialog.DefaultExt = ".xls";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                {
                    saveFileDialog.Dispose();
                    saveFileDialog = null;
                    return;
                }
                _FilePath = saveFileDialog.FileName;
                saveFileDialog.Dispose();
                saveFileDialog = null;
                string tabName = tabERA.SelectedTab.Text;
                switch (tabName)
                {
                    case "Ready to Post":
                        c1ReadyToPost.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);
                        break;
                    case "Posted":
                        c1Posted.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);
                        break;
                    case "Hold":
                        c1Hold.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);
                        break;
                    case "Deleted":
                        c1Deleted.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);
                        break;
                }
                MessageBox.Show("File saved successfully.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        
            }
            catch (IOException)// ioEx)
            {

                MessageBox.Show("File in use. Fail to export report.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception ex)
            {
                MessageBox.Show("File in use. Fail to export report.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
              
            }

           
        }

     
        private void tsb_ExceptionReport_Click(object sender, EventArgs e)
        {
            bool _IsFullyPosted = true;
            _TempID = 0;
            if (tabERA.SelectedTab.Tag.ToString() == TAB_READY)
            {
                if (c1ReadyToPost.RowSel >= 0)
                    _TempID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.RowSel, COL_BPRID));
            }
            else if (tabERA.SelectedTab.Tag.ToString() == TAB_POSTED)
            {
                if (c1Posted.RowSel >= 0)
                {
                    _TempID = Convert.ToInt64(c1Posted.GetData(c1Posted.RowSel, COL_BPRID));

                    if (c1Posted.GetData(c1Posted.RowSel, COL_IsFullyPosted) != null && c1Posted.GetData(c1Posted.RowSel, COL_IsFullyPosted).ToString() != "")
                        _IsFullyPosted = Convert.ToBoolean(c1Posted.GetData(c1Posted.RowSel, COL_IsFullyPosted));
                }
            }
            else if (tabERA.SelectedTab.Tag.ToString() == TAB_DELETED)
            {
                if (c1Deleted.RowSel >= 0)
                    _TempID = Convert.ToInt64(c1Deleted.GetData(c1Deleted.RowSel, COL_BPRID));
            }

            if (_TempID > 0)
            {
                if (_IsFullyPosted)
                {
                    MessageBox.Show("No Exceptions present.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //frmERAExceptions oRpt = new frmERAExceptions(_TempID, 0, _UserID); // ONLY FOR SELECTED CHECK //
                //oRpt.WindowState = FormWindowState.Maximized;
                //oRpt.ShowInTaskbar = false;
                //oRpt.StartPosition = FormStartPosition.CenterParent;
                //oRpt.ShowDialog();
                //oRpt.Dispose();
                //oRpt = null;
                ShowSSRSReport(enum_ReportType.Exception);
            }
        }

        private void oClearingHouseMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem cmnuClearingHouseItem = new ToolStripMenuItem();
            cmnuClearingHouseItem = (ToolStripMenuItem)sender;
            string _ClearingHouse_Name = cmnuClearingHouseItem.Text;
            Int64 _ClearingHouse_ID = Convert.ToInt64(cmnuClearingHouseItem.Tag.ToString());
            gloPMClaimService.frmDownloadClaim ofrmDownloadClaim = null;
            try
            {
                _ServerPath = GetServerPath();

                if (_ServerPath.Trim() != "")
                {
                    _835FolderPath = _ServerPath + "\\" + _BASE_FOLDER + "\\" + _INBOX_FOLDER + "\\" + _CLAIM_FOLDER;

                    if (System.IO.Directory.Exists(_835FolderPath) == false)
                    {
                        System.IO.Directory.CreateDirectory(_835FolderPath);
                    }

                    ofrmDownloadClaim = new gloPMClaimService.frmDownloadClaim(_ClearingHouse_ID, _ClearingHouse_Name, _DataBaseConnectionString, _835FolderPath);
                    ofrmDownloadClaim.ShowDialog(this);
                    #region " Import Downloaded files if Download & Import Button is clicked "
                    if (ofrmDownloadClaim.DownloadedFiles != null && ofrmDownloadClaim.DownloadedFiles.Length > 0)
                        ImportERAFiles(ofrmDownloadClaim.DownloadedFiles);
                    #endregion


                }
                else
                    MessageBox.Show("Server Path cannot be empty. Set Server Path from gloPMAdmin settings.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (ofrmDownloadClaim != null)
                {
                    ofrmDownloadClaim.Dispose();
                    ofrmDownloadClaim = null;
                }

            }

        }

        private void tsb_PostingReport_Click(object sender, EventArgs e)
        {
            _TempID = 0;
            if (c1Posted.RowSel >= 0)
            {
                _TempID = Convert.ToInt64(c1Posted.GetData(c1Posted.RowSel, COL_BPRID));
                if (getReport(_TempID) == false)
                {
                    MessageBox.Show("No Report present.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void tsb_ViewPayment_Click(object sender, EventArgs e)
        {
            try
            {
                _TempID = 0;
                if (tabERA.SelectedTab.Tag.ToString() == TAB_POSTED)
                {
                    if (c1Posted.RowSel >= 0)
                    {
                        _TempID = Convert.ToInt64(c1Posted.GetData(c1Posted.RowSel, COL_BPRID));
                        if (_TempID > 0)
                        {
                            if (OpenConnection(false))
                            {
                                object oResult = null;
                                _TempStr = "SELECT ISNULL(nCreditID,0) FROM Credits_EXT WITH(NOLOCK) WHERE nBPRID = " + _TempID;
                                oResult = oDB.ExecuteScalar_Query(_TempStr);
                                CloseConnection();
                                if (oResult != null && oResult.ToString() != "")
                                {
                                    _TempID = Convert.ToInt64(oResult);
                                    using (gloAccountsV2.frmViewInsurancePaymentV2 ofrmViewInsurancePayment = new gloAccountsV2.frmViewInsurancePaymentV2(_TempID))
                                    {
                                        ofrmViewInsurancePayment.ShowInTaskbar = false;
                                        ofrmViewInsurancePayment.StartPosition = FormStartPosition.CenterScreen;
                                        ofrmViewInsurancePayment.ShowDialog(this);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void tsb_HoldCheck_Click(object sender, EventArgs e)
        {
            try
            {
                _TempID = 0;
                if (tabERA.SelectedTab.Tag.ToString() == TAB_READY && c1ReadyToPost.RowSel >= 0)
                {
                    if (MessageBox.Show("ERA Check will be placed on Hold. " + Environment.NewLine + "Continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _TempID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.RowSel, COL_BPRID));
                        SetCheckStatus(_TempID, enumCheckStatus.Hold);
                        using (frmNotes ofrm = new frmNotes(_DataBaseConnectionString, _ClinicID, enumNoteType.Check, _TempID, true))
                        {
                            ofrm.ShowDialog(this);
                        }
                        RefreshView();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_UnholdCheck_Click(object sender, EventArgs e)
        {
            try
            {
                _TempID = 0;
                if (tabERA.SelectedTab.Tag.ToString() == TAB_HOLD && c1Hold.RowSel >= 0)
                {
                    if (MessageBox.Show("ERA Check will be released from Hold. " + Environment.NewLine + "Continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _TempID = Convert.ToInt64(c1Hold.GetData(c1Hold.RowSel, COL_BPRID));
                        SetCheckStatus(_TempID, enumCheckStatus.Ready);
                        RefreshView();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_Notes_Click(object sender, EventArgs e)
        {
            try
            {
                _TempID = 0;
                if (tabERA.SelectedTab.Tag.ToString() == TAB_READY)
                {
                    if (c1ReadyToPost.RowSel >= 0)
                        _TempID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.RowSel, COL_BPRID));
                }
                else if (tabERA.SelectedTab.Tag.ToString() == TAB_POSTED)
                {
                    if (c1Posted.RowSel >= 0)
                        _TempID = Convert.ToInt64(c1Posted.GetData(c1Posted.RowSel, COL_BPRID));
                }
                else if (tabERA.SelectedTab.Tag.ToString() == TAB_DELETED)
                {
                    if (c1Deleted.RowSel >= 0)
                        _TempID = Convert.ToInt64(c1Deleted.GetData(c1Deleted.RowSel, COL_BPRID));
                }
                else if (tabERA.SelectedTab.Tag.ToString() == TAB_HOLD)
                {
                    if (c1Hold.RowSel >= 0)
                        _TempID = Convert.ToInt64(c1Hold.GetData(c1Hold.RowSel, COL_BPRID));
                }

                if (_TempID > 0)
                {
                    using (frmNotes ofrm = new frmNotes(_DataBaseConnectionString, _ClinicID, enumNoteType.Check, _TempID, false))
                    {
                        ofrm.ShowDialog(this);
                        RefreshView();
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region " Private Methods "

        #region " Open/Close Database Connection "

        private bool OpenConnection(bool withParameters)
        {
            bool _Result = false;
            try
            {
                if (_DataBaseConnectionString != "")
                {
                    oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                    oDB.Connect(false);
                    if (withParameters)
                        oDBPara = new gloDatabaseLayer.DBParameters();
                    _Result = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _Result;
        }

        private void CloseConnection()
        {
            if (oDB != null)
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
            if (oDBPara != null)
            {
                oDBPara.Dispose();
                oDBPara = null;
            }
        }

        #endregion

        private void FillClearingHouseMenus(ToolStripDropDownButton tsbParents)
        {
            DataTable dtClearingHouse = new DataTable();
            string _SQLQuery = "";
            tsbParents.DropDownItems.Clear();
            try
            {
                if (OpenConnection(false))
                {
                    _SQLQuery = "SELECT nClearingHouseID, sClearingHouseCode FROM BL_ClearingHouse_MST WITH(NOLOCK) WHERE ISNULL(nClearingHouseID,0) <> 0 AND ISNULL(sClearingHouseCode,'') <> '' order by ISNULL(bIsDefault,0) Desc";
                    oDB.Retrive_Query(_SQLQuery, out dtClearingHouse);

                    if (dtClearingHouse != null && dtClearingHouse.Rows.Count > 0)
                    {
                        ToolStripMenuItem oClearingHouseMenuItem;

                        for (int i = 0; i < dtClearingHouse.Rows.Count; i++)
                        {
                            oClearingHouseMenuItem = new ToolStripMenuItem();
                            oClearingHouseMenuItem.Text = dtClearingHouse.Rows[i]["sClearingHouseCode"].ToString();
                            oClearingHouseMenuItem.Tag = dtClearingHouse.Rows[i]["nClearingHouseID"].ToString();
                            oClearingHouseMenuItem.ForeColor = Color.FromArgb(31, 73, 125);
                            oClearingHouseMenuItem.Font = oFontRegular;
                            oClearingHouseMenuItem.Image = imgERA.Images[3];
                            oClearingHouseMenuItem.ImageAlign = ContentAlignment.MiddleCenter;
                            oClearingHouseMenuItem.ImageScaling = ToolStripItemImageScaling.None;
                            oClearingHouseMenuItem.Click += new EventHandler(oClearingHouseMenuItem_Click);
                            tsbParents.DropDownItems.Add(oClearingHouseMenuItem);
                            oClearingHouseMenuItem = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return;
            }
            finally
            {
                if (dtClearingHouse != null) { dtClearingHouse.Dispose(); dtClearingHouse = null; }
                CloseConnection();
            }
        }

        private void ImportERAFiles(string[] _ImportedFiles)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Refresh();
                lblProgress.Visible = true;
                prgProgress.Visible = true;

                oERA = new gloERA();
                string[] _FileNames = _ImportedFiles;
                gloGeneralItem.gloItems _SplitedFiles;
                string _InvalidFiles = "";
                string _MessageLog = "";

                #region " CHECK FOR FILE WITH .TXT & .RMT ONLY "
                for (int i = 0; i < _FileNames.Length; i++)
                {
                    if (Path.GetExtension(_FileNames[i]).ToUpper() != ".TXT" && Path.GetExtension(_FileNames[i]).ToUpper() != ".RMT")
                    {
                        _InvalidFiles = _InvalidFiles + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "   Invalid ERA File selected " + Path.GetFileName(_FileNames[i]) + "." + Environment.NewLine;
                        _FileNames[i] = "";
                    }
                }
                #endregion

                _SplitedFiles = GetSplitedFiles(_FileNames);

                if (_SplitedFiles != null)
                    _MessageLog = oERA.ImportERAFiles(_SplitedFiles, 0, "", ref prgProgress, ref lblProgress);

                ShowMessageLogs(_InvalidFiles + _MessageLog);

                if (oERA != null) { oERA.Dispose(); oERA = null; }
                if (_SplitedFiles != null) { _SplitedFiles.Dispose(); _SplitedFiles = null; }

                RefreshView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Cursor = Cursors.Default;
                prgProgress.Visible = false;
                lblProgress.Visible = false;
            }
        }

        private gloGeneralItem.gloItems GetSplitedFiles(string[] sFileNames)
        {
            gloGeneralItem.gloItems oFiles = new gloGeneralItem.gloItems();
            ArrayList _SplittedFiles;
            try
            {
                for (int i = 0; i < sFileNames.Length; i++)
                {
                    if (sFileNames[i] != "")
                    {
                        _SplittedFiles = SplitFile(sFileNames[i]);
                        if (_SplittedFiles != null && _SplittedFiles.Count > 0)
                        {
                            for (int j = 0; j < _SplittedFiles.Count; j++)
                            {
                                if (_SplittedFiles.Count > 1)
                                    oFiles.Add(j + 1, sFileNames[i].ToString(), _SplittedFiles[j].ToString());
                                else
                                    oFiles.Add(0, sFileNames[i].ToString(), _SplittedFiles[j].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
            return oFiles;
        }

        private ArrayList SplitFile(string sFileName)
        {
            ArrayList _ArrSplitedFiles = new ArrayList();
            FileStream oFileRead = null;
            FileStream oFileSplit = null;
            StreamReader oReader = null;
            StreamWriter oWriter = null;
            try
            {
                File.SetAttributes(sFileName, FileAttributes.Normal);
                oFileRead = new FileStream(sFileName, FileMode.Open);
                oReader = new StreamReader(oFileRead);
                string _FileData = oReader.ReadToEnd();
                string[] _SplitString = { "ISA*0" };
                string[] _SplitData = _FileData.Split(_SplitString, StringSplitOptions.RemoveEmptyEntries);

                if (_SplitData.Length == 1)
                    _ArrSplitedFiles.Add(sFileName);
                else
                {
                    #region " CREATE NEW FILES PER ISA "

                    for (int i = 0; i < _SplitData.Length; i++)
                    {
                        _TempStr = Supporting.GenerateTempFileName("");
                        oFileSplit = new FileStream(_TempStr, FileMode.CreateNew);
                        oWriter = new StreamWriter(oFileSplit);
                        oWriter.Write("ISA*0" + _SplitData[i]);
                        _ArrSplitedFiles.Add(_TempStr);
                        oWriter.Close();
                        oFileSplit.Close();
                    }

                    #endregion
                }


                oReader.Close();
                oFileRead.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
            finally
            {
                if (oFileRead != null) { oFileRead.Dispose(); oFileRead = null; }
                if (oFileSplit != null) { oFileSplit.Dispose(); oFileSplit = null; }
                if (oReader != null) { oReader.Dispose(); oReader = null; }
                if (oWriter != null) { oWriter.Dispose(); oWriter = null; }
            }
            return _ArrSplitedFiles;
        }

        private void DesignGrid(ref C1.Win.C1FlexGrid.C1FlexGrid _C1, string sSelectedTab, Int64 _SelectedRowID)
        {
            bool IsSelect = false;
            _C1.Redraw = false;
            try
            {
                _C1.Redraw = false;
                gloC1FlexStyle.Style(_C1, true);

                _C1.Rows.Fixed = 1;
                _C1.AllowEditing = false;

                if (sSelectedTab == TAB_READY) { _C1.Cols[COL_UpdateDate].Visible = false; }
                else if (sSelectedTab == TAB_POSTED) { _C1.Cols[COL_UpdateDate].Caption = "Post Date"; }
                else if (sSelectedTab == TAB_DELETED) { _C1.Cols[COL_UpdateDate].Caption = "Delete Date"; }
                else if (sSelectedTab == TAB_HOLD) { _C1.Cols[COL_UpdateDate].Caption = "Hold Date"; }

                _C1.Cols[COL_ImportDate].Caption = "Import Date";
                _C1.Cols[COL_FileName].Caption = "File Name";
                _C1.Cols[COL_PayerID].Caption = "Payer ID";
                _C1.Cols[COL_PayerName].Caption = "Payer Name";
                _C1.Cols[COL_PayMethod].Caption = "Method";
                _C1.Cols[COL_CheckNo].Caption = "Check Number";
                _C1.Cols[COL_CheckDate].Caption = "Check Date";
                _C1.Cols[COL_CheckAmount].Caption = "Amount";
                _C1.Cols[COL_OrigFileName].Caption = "Original File Name";
                _C1.Cols[COL_NotesCount].Caption = "";

                _C1.Cols[COL_ERAFileID].Visible = false;
                _C1.Cols[COL_ISAID].Visible = false;
                _C1.Cols[COL_BPRID].Visible = false;
                //_C1.Cols[COL_OrigFileName].Visible = false;
                _C1.Cols[COL_ProdDate].Visible = false;
                _C1.Cols[COL_PayerContact].Visible = false;
                _C1.Cols[COL_CheckDateNumeric].Visible = false;
                _C1.Cols[COL_CheckAmountHidden].Visible = false;
                _C1.Cols[COL_TotalClaimPaid].Visible = false;
                _C1.Cols[COL_TotalPLBAmount].Visible = false;
                _C1.Cols[COL_IsFullyPosted].Visible = false;
                _C1.Cols[COL_CloseDate].Visible = false;
                _C1.Cols[COL_PaymentTray].Visible = false;
                _C1.Cols[COL_PostUser].Visible = false;
                _C1.Cols["SearchImportDate"].Visible = false;
                _C1.Cols["SearchCheckDate"].Visible = false;
                _C1.Cols[COL_DateDiffinDays].Visible = false;
                _Width = _C1.Width;

                _C1.Cols[COL_UpdateDate].Width = (Int32)(_Width * 0.07);
                _C1.Cols[COL_ImportDate].Width = (Int32)(_Width * 0.07);
                _C1.Cols[COL_FileName].Width = (Int32)(_Width * 0.16);
                _C1.Cols[COL_PayerID].Width = (Int32)(_Width * 0.07);
                if (sSelectedTab == TAB_READY) _C1.Cols[COL_PayerName].Width = (Int32)(_Width * 0.12);
                else _C1.Cols[COL_PayerName].Width = (Int32)(_Width * 0.18);
                _C1.Cols[COL_PayMethod].Width = (Int32)(_Width * 0.05);
                _C1.Cols[COL_CheckNo].Width = (Int32)(_Width * 0.12);
                _C1.Cols[COL_CheckDate].Width = (Int32)(_Width * 0.07);
                _C1.Cols[COL_CheckAmount].Width = (Int32)(_Width * 0.07);
                _C1.Cols[COL_OrigFileName].Width = (Int32)(_Width * 0.24);
                _C1.Cols[COL_NotesCount].Width = (Int32)(24);

                _C1.Cols[COL_CheckAmount].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                //_C1.Cols[COL_ImportDate].DataType = typeof(System.String);
                // Fomat Date  Mahesh Nawal
                _C1.Cols[COL_ImportDate].DataType = typeof(System.DateTime);
                _C1.Cols[COL_ImportDate].Format = "MM/dd/yyyy";

                _C1.Cols[COL_CheckDate].DataType = typeof(System.DateTime);
                _C1.Cols[COL_CheckDate].Format = "MM/dd/yyyy";

                _C1.Cols[COL_UpdateDate].DataType = typeof(System.DateTime);
                _C1.Cols[COL_UpdateDate].Format = "MM/dd/yyyy";

                if (nSortColumn != 0 && eSortOrder != C1.Win.C1FlexGrid.SortFlags.None)
                    _C1.Sort(eSortOrder, nSortColumn);

                _C1.SetCellImage(0, COL_NotesCount, global::gloBilling.Properties.Resources.Notes);
                _C1.Cols[COL_NotesCount].DataType = typeof(Image);
                _C1.Cols[COL_NotesCount].ImageMap = new System.Collections.Hashtable();
                _C1.Cols[COL_NotesCount].ImageMap.Add(0, null);
                _C1.Cols[COL_NotesCount].ImageMap.Add(1, global::gloBilling.Properties.Resources.Notes);
                _C1.Cols[COL_NotesCount].ImageAndText = false;
                _C1.Cols[COL_NotesCount].AllowResizing = false;
                //_C1.Dock = DockStyle.Fill;
                //_C1.Cols[COL_NotesCount].Visible = false;
                //for (int iRow = 1; iRow < _C1.Rows.Count; iRow++)
                //{
                //    if (_C1.GetData(iRow, COL_NotesCount).ToString() != "" && Convert.ToInt32(_C1.GetData(iRow, COL_NotesCount).ToString()) > 0)
                //        _C1.SetCellImage(iRow, COL_NotesCount, global::gloBilling.Properties.Resources.Notes);
                //    else
                //        _C1.SetData(iRow, COL_NotesCount, "");

                //    if (Convert.ToInt64(_C1.GetData(iRow, COL_BPRID).ToString()) == _SelectedRowID)
                //    {
                //        _C1.Select(iRow, COL_BPRID);
                //        IsSelect = true;
                //    }

                //}
                if (!IsSelect && _C1.Rows.Count > 1)
                {
                    _C1.Select(IRow, COL_BPRID);

                }

                //if (_C1.RowSel > 0)
                //{

                //    if (_C1.GetData(_C1.RowSel, COL_NotesCount).ToString() != "" && Convert.ToInt32(_C1.GetData(_C1.RowSel, COL_NotesCount).ToString()) > 0)
                //    {
                //        this.tsb_Notes.Image = global::gloBilling.Properties.Resources.Edit_Note;
                //    }

                //    else
                //    {
                //        this.tsb_Notes.Image = global::gloBilling.Properties.Resources.Add_Note;
                //    }
                //}
                //else
                //{
                //    this.tsb_Notes.Image = global::gloBilling.Properties.Resources.Add_Note;
                //}


                _C1.Redraw = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                _C1.Redraw = true;
            }
        }

        private void DesignFooterGrid(C1.Win.C1FlexGrid.C1FlexGrid _C1Footer, ref C1.Win.C1FlexGrid.C1FlexGrid _C1, string sSelectedTab, DataTable dtChecks)
        {
            try
            {

                _C1Footer.Clear();
                _C1Footer.Rows.Count = _C1.Rows.Count;
                _C1Footer.Rows.Fixed = 1;
                _C1Footer.Cols.Count = _C1.Cols.Count;
                _C1Footer.Cols.Fixed = 1;
                _C1Footer.Rows[0].Height = 23;
                _C1Footer.AllowSorting = AllowSortingEnum.None;
                _C1Footer.ScrollBars = ScrollBars.None;
                _C1Footer.AllowEditing = false;
                _C1Footer.Cols.Fixed = 0;

                for (int i = 0; i < _C1.Cols.Count; i++)
                {
                    _C1Footer.Cols[i].Visible = _C1.Cols[i].Visible;
                    _C1Footer.Cols[i].Width = _C1.Cols[i].Width;
                    _C1Footer.Cols[i].TextAlign = _C1.Cols[i].TextAlign;
                    _C1Footer.Cols[i].AllowResizing = _C1.Cols[i].AllowResizing;
                }
                string sDateDiff = "";
                if (_C1.Rows.Count > 1)
                {
                    if (sSelectedTab == TAB_READY)
                    {

                        _C1Footer.SetData(0, COL_ImportDate, "Total Checks:");
                        _C1Footer.SetData(0, COL_FileName, dtChecks.Compute("Count(ERAFileID)", ""));

                        _C1Footer.SetData(0, COL_CheckAmount - 1, "Total Amount:");
                        _C1Footer.SetData(0, COL_CheckAmount, dtChecks.Compute("Sum(CheckAmountHidden)", ""));
                        _C1Footer.SetData(0, COL_CheckDate - 1, "Average Days Pending:");
                        sDateDiff = dtChecks.Compute("AVG(DATEDIFFinDays)", "").ToString();
                        if (sDateDiff != "")
                        {
                            _C1Footer.SetData(0, COL_CheckDate, sDateDiff + " Days");
                        }
                        else
                        {
                            _C1Footer.SetData(0, COL_CheckDate, sDateDiff);
                        }

                    }
                    else if (sSelectedTab == TAB_POSTED)
                    {

                        _C1Footer.SetData(0, COL_CheckDate - 1, "Average Posting Lag:");
                        sDateDiff = dtChecks.Compute("AVG(DATEDIFFinDays)", "").ToString();
                        if (sDateDiff != "")
                        {
                            _C1Footer.SetData(0, COL_CheckDate, sDateDiff + " Days");
                        }
                        else
                        {
                            _C1Footer.SetData(0, COL_CheckDate, sDateDiff);
                        }
                    }
                }

                CellStyle csTextCaption;
               // csTextCaption = _C1Footer.Styles.Add("SubCaption");
                try
                {
                    if (_C1Footer.Styles.Contains("SubCaption"))
                    {
                        csTextCaption = _C1Footer.Styles["SubCaption"];
                    }
                    else
                    {
                        csTextCaption = _C1Footer.Styles.Add("SubCaption");
                        csTextCaption.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        csTextCaption.TextEffect = TextEffectEnum.Flat;
                        csTextCaption.ForeColor = Color.Maroon;
                        csTextCaption.TextAlign = TextAlignEnum.RightCenter;
                    }

                }
                catch
                {
                    csTextCaption = _C1Footer.Styles.Add("SubCaption");
                    csTextCaption.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csTextCaption.TextEffect = TextEffectEnum.Flat;
                    csTextCaption.ForeColor = Color.Maroon;
                    csTextCaption.TextAlign = TextAlignEnum.RightCenter;

                }
  

                CellStyle csTextCaptionblue;
               // csTextCaptionblue = _C1Footer.Styles.Add("SubCaption1");
                try
                {
                    if (_C1Footer.Styles.Contains("SubCaption1"))
                    {
                        csTextCaptionblue = _C1Footer.Styles["SubCaption1"];
                    }
                    else
                    {
                        csTextCaptionblue = _C1Footer.Styles.Add("SubCaption1");
                        csTextCaptionblue.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        csTextCaptionblue.TextEffect = TextEffectEnum.Flat;
                        csTextCaptionblue.ForeColor = Color.Blue;
                        csTextCaptionblue.TextAlign = TextAlignEnum.LeftCenter;
                    }

                }
                catch
                {
                    csTextCaptionblue = _C1Footer.Styles.Add("SubCaption1");
                    csTextCaptionblue.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csTextCaptionblue.TextEffect = TextEffectEnum.Flat;
                    csTextCaptionblue.ForeColor = Color.Blue;
                    csTextCaptionblue.TextAlign = TextAlignEnum.LeftCenter;

                }
     

                CellRange subTextCaptionRange;
                subTextCaptionRange = _C1Footer.GetCellRange(0, COL_ImportDate, 0, COL_ImportDate);
                subTextCaptionRange.Style = csTextCaption;


                CellRange subFileName;
                subFileName = _C1Footer.GetCellRange(0, COL_FileName, 0, COL_PayMethod);
                subFileName.Style = csTextCaptionblue;


                CellStyle csTextCaption1;
               // csTextCaption1 = _C1Footer.Styles.Add("SubCaption2");
                try
                {
                    if (_C1Footer.Styles.Contains("SubCaption2"))
                    {
                        csTextCaption1 = _C1Footer.Styles["SubCaption2"];
                    }
                    else
                    {
                        csTextCaption1 = _C1Footer.Styles.Add("SubCaption2");
                        csTextCaption1.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        csTextCaption1.TextEffect = TextEffectEnum.Flat;
                        csTextCaption1.ForeColor = Color.Maroon;
                        csTextCaption1.TextAlign = TextAlignEnum.RightCenter;
                    }

                }
                catch
                {
                    csTextCaption1 = _C1Footer.Styles.Add("SubCaption2");
                    csTextCaption1.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csTextCaption1.TextEffect = TextEffectEnum.Flat;
                    csTextCaption1.ForeColor = Color.Maroon;
                    csTextCaption1.TextAlign = TextAlignEnum.RightCenter;

                }
           


                CellRange subTextCaptionRange2;
                subTextCaptionRange2 = _C1Footer.GetCellRange(0, COL_CheckNo, 0, COL_CheckNo);
                subTextCaptionRange2.Style = csTextCaption1;

                CellRange subTextCaptionRange1;
                subTextCaptionRange1 = _C1Footer.GetCellRange(0, COL_CheckDate, 0, COL_CheckDate);
                subTextCaptionRange1.Style = csTextCaptionblue;




                CellStyle csAmount;
              //  csAmount = _C1Footer.Styles.Add("SubTotalRow");
                try
                {
                    if (_C1Footer.Styles.Contains("SubTotalRow"))
                    {
                        csAmount = _C1Footer.Styles["SubTotalRow"];
                    }
                    else
                    {
                        csAmount = _C1Footer.Styles.Add("SubTotalRow");
                        csAmount.Format = "c";
                        csAmount.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        csAmount.TextEffect = TextEffectEnum.Flat;
                        csAmount.ForeColor = Color.Blue;
                        csAmount.TextAlign = TextAlignEnum.RightCenter;
                    }

                }
                catch
                {
                    csAmount = _C1Footer.Styles.Add("SubTotalRow");
                    csAmount.Format = "c";
                    csAmount.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csAmount.TextEffect = TextEffectEnum.Flat;
                    csAmount.ForeColor = Color.Blue;
                    csAmount.TextAlign = TextAlignEnum.RightCenter;

                }
           
      

                CellRange subAmountRange;
                subAmountRange = _C1Footer.GetCellRange(0, COL_CheckAmount, 0, COL_CheckAmount);
                subAmountRange.Style = csAmount;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }
        private void RefreshView()
        {
            DataTable dtChecks;
            Cursor = Cursors.WaitCursor;
            mskFromDateDeletd.Clear();
            mskToDateDeleted.Clear();
            mskFromDatePosted.Clear();
            mskToDatePosted.Clear();
            mskFromDateHold.Clear();
            mskToDateHold.Clear();

            try
            {
                switch (tabERA.SelectedTab.Tag.ToString())
                {
                    #region " View Ready To Post Checks "
                    case TAB_READY:
                        {
                            tsb_UndoDelete.Visible = false;
                            tsb_MarkDeleted.Visible = true;
                            tsb_Trial.Visible = false;
                            tsb_Post.Visible = true;
                            tsb_ExceptionReport.Visible = false;
                            tsb_DetailExceptionReport.Visible = false;
                            tsb_PostingReport.Visible = false;
                            tsb_ViewPayment.Visible = false;
                            tsb_HoldCheck.Visible = true;
                            tsb_UnholdCheck.Visible = false;

                            dtChecks = GetChecks(enumCheckStatus.Ready);

                            if (dtChecks != null)
                            {
                                this.c1ReadyToPost.AfterRowColChange -= new C1.Win.C1FlexGrid.RangeEventHandler(this.C1All_AfterRowColChange);
                                c1ReadyToPost.DataSource = dtChecks.DefaultView;
                                this.c1ReadyToPost.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.C1All_AfterRowColChange);
                                DesignGrid(ref c1ReadyToPost, TAB_READY, nSelectedRowReadyID);
                                DesignFooterGrid(c1ReadyToPostTotal, ref c1ReadyToPost, TAB_READY, dtChecks);                                
                            }

                            if (txtSearchReadyToPost.Text.Trim() != "")
                            {
                                txtSearchReadyToPost.TextChanged -= new EventHandler(txtSearchAll_TextChanged);
                                txtSearchReadyToPost.Clear();
                                txtSearchReadyToPost.TextChanged += new EventHandler(txtSearchAll_TextChanged);
                            }
                            break;
                        }
                    #endregion

                    #region " View Posted Checks "
                    case TAB_POSTED:
                        {
                            
                            tsb_UndoDelete.Visible = false;
                            tsb_MarkDeleted.Visible = false;
                            tsb_Trial.Visible = false;
                            tsb_Post.Visible = false;
                            tsb_ExceptionReport.Visible = true;
                            tsb_DetailExceptionReport.Visible = GetDetailsReportSetting("Show Detail ERA Exception Report on ERA Posting");
                            tsb_PostingReport.Visible = true;
                            tsb_ViewPayment.Visible = true;
                            tsb_HoldCheck.Visible = false;
                            tsb_UnholdCheck.Visible = false;

                            dtChecks = GetChecks(enumCheckStatus.Posted);
                            if (dtChecks != null)
                            {
                                this.c1Posted.AfterRowColChange -= new C1.Win.C1FlexGrid.RangeEventHandler(this.C1All_AfterRowColChange);
                                c1Posted.DataSource = dtChecks.DefaultView;
                                this.c1Posted.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.C1All_AfterRowColChange);
                                DesignGrid(ref c1Posted, TAB_POSTED, nSelectedRowPostedID);
                                DesignFooterGrid(c1PostedTotal, ref c1Posted, TAB_POSTED, dtChecks);

                                c1Posted.Cols[COL_FileName].Width = (Int32)(_Width * 0.14);
                                c1Posted.Cols[COL_PayerName].Width = (Int32)(_Width * 0.13);
                                c1Posted.Cols[COL_CheckNo].Width = (Int32)(_Width * 0.09);
                                c1Posted.Cols[COL_OrigFileName].Width = (Int32)(_Width * 0.20);                                
                            }

                            if (txtSearchPosted.Text.Trim() != "")
                            {
                                txtSearchPosted.TextChanged -= new EventHandler(txtSearchAll_TextChanged);
                                txtSearchPosted.Clear();
                                txtSearchPosted.TextChanged += new EventHandler(txtSearchAll_TextChanged);
                            }
                            break;
                        }
                    #endregion

                    #region " View Deleted Checks "
                    case TAB_DELETED:
                        {
                            tsb_UndoDelete.Visible = true;
                            tsb_MarkDeleted.Visible = false;
                            tsb_Trial.Visible = false;
                            tsb_Post.Visible = false;
                            tsb_ExceptionReport.Visible = false;
                            tsb_DetailExceptionReport.Visible = false;
                            tsb_PostingReport.Visible = false;
                            tsb_ViewPayment.Visible = false;
                            tsb_HoldCheck.Visible = false;
                            tsb_UnholdCheck.Visible = false;

                            dtChecks = GetChecks(enumCheckStatus.MarkedDeleted);
                            if (dtChecks != null)
                            {
                                this.c1Deleted.AfterRowColChange -= new C1.Win.C1FlexGrid.RangeEventHandler(this.C1All_AfterRowColChange);
                                c1Deleted.DataSource = dtChecks.DefaultView;
                                this.c1Deleted.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.C1All_AfterRowColChange);
                                DesignGrid(ref c1Deleted, TAB_DELETED, nSelectedRowDeleteID);

                                c1Deleted.Cols[COL_FileName].Width = (Int32)(_Width * 0.14);
                                c1Deleted.Cols[COL_PayerName].Width = (Int32)(_Width * 0.13);
                                c1Deleted.Cols[COL_CheckNo].Width = (Int32)(_Width * 0.09);
                                c1Deleted.Cols[COL_OrigFileName].Width = (Int32)(_Width * 0.20);
                            }

                            if (txtSearchDeleted.Text.Trim() != "")
                            {
                                txtSearchDeleted.TextChanged -= new EventHandler(txtSearchAll_TextChanged);
                                txtSearchDeleted.Clear();
                                txtSearchDeleted.TextChanged += new EventHandler(txtSearchAll_TextChanged);
                            }
                            break;
                        }
                    #endregion

                    #region " View Hold Checks "
                    case TAB_HOLD:
                        {
                            tsb_UndoDelete.Visible = false;
                            tsb_MarkDeleted.Visible = false;
                            tsb_Trial.Visible = false;
                            tsb_Post.Visible = false;
                            tsb_ExceptionReport.Visible = false;
                            tsb_DetailExceptionReport.Visible = false;
                            tsb_PostingReport.Visible = false;
                            tsb_ViewPayment.Visible = false;
                            tsb_HoldCheck.Visible = false;
                            tsb_UnholdCheck.Visible = true;

                            dtChecks = GetChecks(enumCheckStatus.Hold);
                            if (dtChecks != null)
                            {
                                this.c1Hold.AfterRowColChange -= new C1.Win.C1FlexGrid.RangeEventHandler(this.C1All_AfterRowColChange);
                                c1Hold.DataSource = dtChecks.DefaultView;
                                this.c1Hold.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.C1All_AfterRowColChange);
                                DesignGrid(ref c1Hold, TAB_HOLD, nSelectedRowHoldID);

                                c1Hold.Cols[COL_FileName].Width = (Int32)(_Width * 0.14);
                                c1Hold.Cols[COL_PayerName].Width = (Int32)(_Width * 0.13);
                                c1Hold.Cols[COL_CheckNo].Width = (Int32)(_Width * 0.09);
                                c1Hold.Cols[COL_OrigFileName].Width = (Int32)(_Width * 0.20);
                            }

                            if (txtSearchHold.Text.Trim() != "")
                            {
                                txtSearchHold.TextChanged -= new EventHandler(txtSearchAll_TextChanged);
                                txtSearchHold.Clear();
                                txtSearchHold.TextChanged += new EventHandler(txtSearchAll_TextChanged);
                            }
                            break;
                        }
                    #endregion
                }

                C1All_AfterRowColChange(null, null);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool IsAllChecksPosted(Int64 nFileID)
        {
            bool _Result = false;
            Object oResult = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "SELECT COUNT(nBPRID) FROM ERA_BPR WITH(NOLOCK) WHERE nCheckStatus <> 2 AND nERAFileID = " + nFileID;
                    oResult = oDB.ExecuteScalar_Query(_TempStr);
                    if (oResult != null && oResult.ToString() != "")
                        if (Convert.ToInt32(oResult) == 0)
                            _Result = true;
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _Result;
        }

        private DataTable GetChecks(enumCheckStatus eCheckStatus)
        {
            DataTable _dtChecks = null;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@CheckStatus", eCheckStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDBPara.Add("@UserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@PostedDateRange", _PostedDateRange, ParameterDirection.Input, SqlDbType.Date);
                    oDB.Retrive("ERA_GetChecks", oDBPara, out _dtChecks);
                    CloseConnection();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _dtChecks;
        }

        private void tabERA_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                switch (((Button)sender).Tag.ToString())
                {
                    case TAB_READY:
                        {
                            txtSearchReadyToPost.ResetText();
                            txtSearchReadyToPost.Focus();
                            break;
                        }

                    case TAB_POSTED:
                        {
                            txtSearchPosted.ResetText();
                            txtSearchPosted.Focus();
                            break;
                        }

                    case TAB_DELETED:
                        {
                            txtSearchDeleted.ResetText();
                            txtSearchDeleted.Focus();
                            break;
                        }

                    case TAB_HOLD:
                        {
                            txtSearchHold.ResetText();
                            txtSearchHold.Focus();
                            break;
                        }

                }
            }
        }

        private void SetCheckStatus(Int64 nBPRID, enumCheckStatus eStatus)
        {
            try
            {
                if (OpenConnection(false))
                {
                    string _Query = "UPDATE ERA_BPR SET nCheckStatus = " + eStatus.GetHashCode() + ", dUpdateDate = dbo.gloGetDate() WHERE nBPRID = " + nBPRID + "";
                    oDB.Execute_Query(_Query);
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private DataTable GetUnProcessedFiles()
        {
            DataTable dtFiles = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "SELECT ERA_Files.nERAFileID, DBO.ERA_GetFileName(ERA_Files.nERAFileID) AS sFileName, ISNULL(sOriginalFileName,'') AS sOriginalFileName, dtImportDate " +
                        " FROM ERA_Files WITH(NOLOCK) WHERE nStatus = 1 AND nUserID = " + _UserID + " AND nClinicID = " + _ClinicID + " ORDER BY dtImportDate";
                    oDB.Retrive_Query(_TempStr, out dtFiles);
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return dtFiles;
        }

        private void ShowMessageLogs(string _OtherMessage)
        {
            try
            {
                DataTable _dtFiles = GetUnProcessedFiles();
                string _MessageLog = _OtherMessage;
                if (_dtFiles != null && _dtFiles.Rows.Count > 0)
                {
                    for (int i = 0; i < _dtFiles.Rows.Count; i++)
                    {
                        _MessageLog = _MessageLog + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "   ERA File : " + _dtFiles.Rows[i]["sFileName"].ToString() + " is not processed." + Environment.NewLine;
                    }
                }
                txtMessage.Text = _MessageLog;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void C1All_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                //if (pnlAdditionalCheckInfo.Visible == false)
                //    return;

                // RESET ALL LABEL TEXT //
                lblOriginalFileName.Text = "";
                lblProductionDate.Text = "";
                lblPayerContact.Text = "";
                lblTotalClaimPaid.Text = "";
                lblTotalPLBAmount.Text = "";
                lblPaymentBalanced.Text = "";
                pnlPostInfo.Visible = false;
                //nSelectedRowID = 0;

                Int32 _RowSel = -1;
                decimal _TotalClaimPaid = 0;
                decimal _TotalPLBAmount = 0;
                decimal _CheckAmount = 0;


                if (e != null)
                {
                    _RowSel = e.NewRange.r1;
                    IRow = _RowSel;
                }
                else
                {
                    if (tabERA.SelectedTab.Tag.ToString() == TAB_READY)
                        _RowSel = c1ReadyToPost.RowSel;
                    else if (tabERA.SelectedTab.Tag.ToString() == TAB_DELETED)
                        _RowSel = c1Deleted.RowSel;
                    else if (tabERA.SelectedTab.Tag.ToString() == TAB_POSTED)
                        _RowSel = c1Posted.RowSel;
                    else if (tabERA.SelectedTab.Tag.ToString() == TAB_HOLD)
                        _RowSel = c1Hold.RowSel;
                }

                if (_RowSel >= 0)
                {
                    switch (tabERA.SelectedTab.Tag.ToString())
                    {
                        case TAB_READY:
                            {
                                lblOriginalFileName.Text = c1ReadyToPost.GetData(_RowSel, COL_OrigFileName).ToString();
                                if (c1ReadyToPost.GetData(_RowSel, COL_ProdDate).ToString() != "")
                                    lblProductionDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(c1ReadyToPost.GetData(_RowSel, COL_ProdDate).ToString())).ToString("MM/dd/yyyy");
                                else
                                    lblProductionDate.Text = "";
                                lblPayerContact.Text = c1ReadyToPost.GetData(_RowSel, COL_PayerContact).ToString();
                                try
                                {
                                    _TotalClaimPaid = Convert.ToDecimal(c1ReadyToPost.GetData(_RowSel, COL_TotalClaimPaid));
                                }
                                catch
                                {
                                }
                                try
                                {
                                    _TotalPLBAmount = Convert.ToDecimal(c1ReadyToPost.GetData(_RowSel, COL_TotalPLBAmount));
                                }
                                catch
                                {
                                }
                                try
                                {
                                    _CheckAmount = Convert.ToDecimal(c1ReadyToPost.GetData(_RowSel, COL_CheckAmountHidden));
                                }
                                catch
                                {
                                }
                                lblTotalClaimPaid.Text = "$ " + Convert.ToString(c1ReadyToPost.GetData(_RowSel, COL_TotalClaimPaid));
                                lblTotalPLBAmount.Text = "$ " + Convert.ToString(c1ReadyToPost.GetData(_RowSel, COL_TotalPLBAmount));

                                if (_CheckAmount == (_TotalClaimPaid - _TotalPLBAmount))
                                {
                                    lblPaymentBalanced.Text = "Yes";
                                    lblPaymentBalanced.ForeColor = Color.FromArgb(31, 73, 125);
                                }
                                else
                                {
                                    lblPaymentBalanced.Text = "No";
                                    lblPaymentBalanced.ForeColor = Color.Red;
                                }
                                try
                                {
                                    nSelectedRowReadyID = Convert.ToInt64(c1ReadyToPost.GetData(_RowSel, COL_BPRID));
                                }
                                catch
                                {
                                }
                                try
                                {
                                    nSelectedRowID = Convert.ToInt64(c1ReadyToPost.GetData(_RowSel, COL_BPRID));
                                }
                                catch
                                {
                                }

                                break;
                            }
                        case TAB_DELETED:
                            {
                                lblOriginalFileName.Text = c1Deleted.GetData(_RowSel, COL_OrigFileName).ToString();
                                if (c1Deleted.GetData(_RowSel, COL_ProdDate).ToString() != "")
                                    lblProductionDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(c1Deleted.GetData(_RowSel, COL_ProdDate).ToString())).ToString("MM/dd/yyyy");
                                else
                                    lblProductionDate.Text = "";
                                lblPayerContact.Text = c1Deleted.GetData(_RowSel, COL_PayerContact).ToString();
                                try
                                {
                                    _TotalClaimPaid = Convert.ToDecimal(c1Deleted.GetData(_RowSel, COL_TotalClaimPaid));
                                }
                                catch
                                {
                                }
                                try
                                {
                                    _TotalPLBAmount = Convert.ToDecimal(c1Deleted.GetData(_RowSel, COL_TotalPLBAmount));
                                }
                                catch
                                {
                                }
                                try
                                {
                                    _CheckAmount = Convert.ToDecimal(c1Deleted.GetData(_RowSel, COL_CheckAmountHidden));
                                }
                                catch
                                {
                                }
                                lblTotalClaimPaid.Text = "$ " + Convert.ToString(c1Deleted.GetData(_RowSel, COL_TotalClaimPaid));
                                lblTotalPLBAmount.Text = "$ " + Convert.ToString(c1Deleted.GetData(_RowSel, COL_TotalPLBAmount));
                                if (_CheckAmount == (_TotalClaimPaid - _TotalPLBAmount))
                                {
                                    lblPaymentBalanced.Text = "Yes";
                                    lblPaymentBalanced.ForeColor = Color.FromArgb(31, 73, 125);
                                }
                                else
                                {
                                    lblPaymentBalanced.Text = "No";
                                    lblPaymentBalanced.ForeColor = Color.Red;
                                }
                                try
                                {
                                    nSelectedRowDeleteID = Convert.ToInt64(c1Deleted.GetData(_RowSel, COL_BPRID));
                                }
                                catch
                                {
                                }
                                try
                                {
                                    nSelectedRowID = Convert.ToInt64(c1Deleted.GetData(_RowSel, COL_BPRID));
                                }
                                catch
                                {
                                }
                                break;
                            }
                        case TAB_POSTED:
                            {
                                lblOriginalFileName.Text = c1Posted.GetData(_RowSel, COL_OrigFileName).ToString();
                                if (c1Posted.GetData(_RowSel, COL_ProdDate).ToString() != "")
                                    lblProductionDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(c1Posted.GetData(_RowSel, COL_ProdDate).ToString())).ToString("MM/dd/yyyy");
                                else
                                    lblProductionDate.Text = "";
                                lblPayerContact.Text = c1Posted.GetData(_RowSel, COL_PayerContact).ToString();
                                try
                                {
                                    _TotalClaimPaid = Convert.ToDecimal(c1Posted.GetData(_RowSel, COL_TotalClaimPaid));
                                }
                                catch
                                {
                                }
                                try
                                {
                                    _TotalPLBAmount = Convert.ToDecimal(c1Posted.GetData(_RowSel, COL_TotalPLBAmount));
                                }
                                catch
                                {
                                }
                                try
                                {
                                    _CheckAmount = Convert.ToDecimal(c1Posted.GetData(_RowSel, COL_CheckAmountHidden));
                                }
                                catch
                                {
                                }

                                lblTotalClaimPaid.Text = "$ " + Convert.ToString(c1Posted.GetData(_RowSel, COL_TotalClaimPaid));
                                lblTotalPLBAmount.Text = "$ " + Convert.ToString(c1Posted.GetData(_RowSel, COL_TotalPLBAmount));
                                if (_CheckAmount == (_TotalClaimPaid - _TotalPLBAmount))
                                {
                                    lblPaymentBalanced.Text = "Yes";
                                    lblPaymentBalanced.ForeColor = Color.FromArgb(31, 73, 125);
                                }
                                else
                                {
                                    lblPaymentBalanced.Text = "No";
                                    lblPaymentBalanced.ForeColor = Color.Red;
                                }
                                pnlPostInfo.Visible = true;
                                lblCloseDate.Text = Convert.ToString(c1Posted.GetData(_RowSel, COL_CloseDate));
                                lblPayTray.Text = Convert.ToString(c1Posted.GetData(_RowSel, COL_PaymentTray));
                                lblUser.Text = Convert.ToString(c1Posted.GetData(_RowSel, COL_PostUser));

                                try
                                {
                                    nSelectedRowPostedID = Convert.ToInt64(c1Posted.GetData(_RowSel, COL_BPRID));
                                }
                                catch
                                {
                                }
                                try
                                {
                                    nSelectedRowID = Convert.ToInt64(c1Posted.GetData(_RowSel, COL_BPRID));
                                }
                                catch
                                {
                                }

                                break;
                            }
                        case TAB_HOLD:
                            {
                                lblOriginalFileName.Text = c1Hold.GetData(_RowSel, COL_OrigFileName).ToString();
                                if (c1Hold.GetData(_RowSel, COL_ProdDate).ToString() != "")
                                    lblProductionDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(c1Hold.GetData(_RowSel, COL_ProdDate).ToString())).ToString("MM/dd/yyyy");
                                else
                                    lblProductionDate.Text = "";
                                lblPayerContact.Text = c1Hold.GetData(_RowSel, COL_PayerContact).ToString();

                                try
                                {
                                    _TotalClaimPaid = Convert.ToDecimal(c1Hold.GetData(_RowSel, COL_TotalClaimPaid));
                                }
                                catch
                                {
                                }
                                try
                                {
                                    _TotalPLBAmount = Convert.ToDecimal(c1Hold.GetData(_RowSel, COL_TotalPLBAmount));
                                }
                                catch
                                {
                                }
                                try
                                {
                                    _CheckAmount = Convert.ToDecimal(c1Hold.GetData(_RowSel, COL_CheckAmountHidden));
                                }
                                catch
                                {
                                }

                                lblTotalClaimPaid.Text = "$ " + Convert.ToString(c1Hold.GetData(_RowSel, COL_TotalClaimPaid));
                                lblTotalPLBAmount.Text = "$ " + Convert.ToString(c1Hold.GetData(_RowSel, COL_TotalPLBAmount));
                                if (_CheckAmount == (_TotalClaimPaid - _TotalPLBAmount))
                                {
                                    lblPaymentBalanced.Text = "Yes";
                                    lblPaymentBalanced.ForeColor = Color.FromArgb(31, 73, 125);
                                }
                                else
                                {
                                    lblPaymentBalanced.Text = "No";
                                    lblPaymentBalanced.ForeColor = Color.Red;
                                }

                                try
                                {
                                    nSelectedRowHoldID = Convert.ToInt64(c1Hold.GetData(_RowSel, COL_BPRID));
                                }
                                catch
                                {
                                }
                                try
                                {
                                    nSelectedRowID = Convert.ToInt64(c1Hold.GetData(_RowSel, COL_BPRID));
                                }
                                catch
                                {
                                }

                                break;
                            }
                    }

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void C1All_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            try
            {

                C1.Win.C1FlexGrid.C1FlexGrid _C1 = (C1.Win.C1FlexGrid.C1FlexGrid)sender;
                _C1.Redraw = false;
                if (e.Col != COL_CheckAmount)
                    nSortColumn = e.Col;
                else
                    nSortColumn = COL_CheckAmountHidden;
                eSortOrder = e.Order;
                _C1.Sort(eSortOrder, nSortColumn);
                //for (int iRow = 1; iRow < _C1.Rows.Count; iRow++)
                //{
                //    if (_C1.GetData(iRow, COL_NotesCount).ToString() != "")
                //        _C1.SetCellImage(iRow, COL_NotesCount, global::gloBilling.Properties.Resources.Notes);
                //    else
                //        _C1.SetCellImage(iRow, COL_NotesCount, null);

                //    if (Convert.ToInt64(_C1.GetData(iRow, COL_BPRID).ToString()) == nSelectedRowID)
                //        _C1.Select(iRow, COL_BPRID);
                //}
                _C1.Redraw = true;
            }
            catch //(Exception ex)
            { }
        }

        private void txtSearchAll_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // THIS CONDITION WILL OCCURE IF TEXT IS PASTED IN SEARCH BOX //
                if (oTimer.Enabled == false)
                {
                    oTimer.Stop();
                    oTimer.Enabled = true;

                }

                this.Cursor = Cursors.Default;
            }//try
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
            }
            finally
            {
            }

        }


        private void searchERACheck()
        {
            DataView _dv = null;
            C1.Win.C1FlexGrid.C1FlexGrid _C1 = null;
            try
            {
                _CurrentTime = DateTime.Now;

                string _Filter = "";

                _SearchText = _SearchText.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]").Replace("*", "%");

                #region " Get DV & C1 "
                if (tabERA.SelectedTab.Tag.ToString() == TAB_READY)
                {
                    _dv = (DataView)c1ReadyToPost.DataSource;
                    _C1 = c1ReadyToPost;
                }
                else if (tabERA.SelectedTab.Tag.ToString() == TAB_DELETED)
                {
                    _dv = (DataView)c1Deleted.DataSource;
                    _C1 = c1Deleted;
                }
                else if (tabERA.SelectedTab.Tag.ToString() == TAB_POSTED)
                {
                    _dv = (DataView)c1Posted.DataSource;
                    _C1 = c1Posted;
                }
                else if (tabERA.SelectedTab.Tag.ToString() == TAB_HOLD)
                {
                    _dv = (DataView)c1Hold.DataSource;
                    _C1 = c1Hold;
                }
                _C1.Redraw = false;
                #endregion
                if (_dv != null)
                {
                    #region " SEARCH "
                    if (_SearchText == "")
                    {
                        _dv.RowFilter = "";
                        //for (int iRow = 1; iRow < _C1.Rows.Count; iRow++)
                        //{
                        //    if (_C1.GetData(iRow, COL_NotesCount).ToString() != "" && Convert.ToInt32(_C1.GetData(iRow, COL_NotesCount).ToString()) > 0)
                        //        _C1.SetCellImage(iRow, COL_NotesCount, global::gloBilling.Properties.Resources.Notes);
                        //    else
                        //        _C1.SetData(iRow, COL_NotesCount, "");
                        //}
                    }
                    else
                    {
                        if (_SearchText.Contains(",") == false)
                        {
                            #region " Simple Search "

                            _Filter = _C1.Cols["SearchImportDate"].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_FileName].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_OrigFileName].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_PayerID].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_PayerName].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_CheckNo].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols["SearchCheckDate"].Name + " LIKE '" + _SearchText + "%' OR " +
                              " SUBSTRING (" + _C1.Cols[COL_CheckAmount].Name + ",2,len(" + _C1.Cols[COL_CheckAmount].Name + ")) LIKE '" + _SearchText + "%' ";

                            #endregion
                        }
                        else
                        {
                            #region " Comma Separated Search "

                            string[] _SplitSearch = _SearchText.Split(',');
                            string _SplitString;

                            for (int i = 0; i < _SplitSearch.Length; i++)
                            {

                                _SplitString = _SplitSearch[i].Trim();

                                if (_SplitString != "")
                                {
                                    if (_Filter != "")
                                        _Filter = _Filter + " AND ";

                                    _Filter = _Filter + " ( " +
                                        _C1.Cols["SearchImportDate"].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_FileName].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_OrigFileName].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_PayerID].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_PayerName].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_CheckNo].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols["SearchCheckDate"].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_CheckAmount].Name + " LIKE '" + _SplitString + "%'" +
                                        " ) ";
                                }
                            }

                            #endregion
                        }

                        _dv.RowFilter = _Filter;
                        //for (int iRow = 1; iRow < _C1.Rows.Count; iRow++)
                        //{
                        //    if (_C1.GetData(iRow, COL_NotesCount).ToString() != "" && Convert.ToInt32(_C1.GetData(iRow, COL_NotesCount).ToString()) > 0)
                        //        _C1.SetCellImage(iRow, COL_NotesCount, global::gloBilling.Properties.Resources.Notes);
                        //    else
                        //        _C1.SetData(iRow, COL_NotesCount, "");
                        //}



                    }


                    string sDateDiff = "";
                    if (tabERA.SelectedTab.Tag.ToString() == TAB_READY)
                    {

                        c1ReadyToPostTotal.SetData(0, COL_FileName, _dv.ToTable().Compute("Count(ERAFileID)", string.Empty));
                        c1ReadyToPostTotal.SetData(0, COL_CheckAmount, _dv.ToTable().Compute("Sum(CheckAmountHidden)", ""));
                        sDateDiff = _dv.ToTable().Compute("AVG(DATEDIFFinDays)", "").ToString();
                        if (sDateDiff != "")
                        {
                            c1ReadyToPostTotal.SetData(0, COL_CheckDate, sDateDiff + " Days");
                        }
                        else
                        {
                            c1ReadyToPostTotal.SetData(0, COL_CheckDate, sDateDiff);
                        }
                    }
                    else if (tabERA.SelectedTab.Tag.ToString() == TAB_POSTED)
                    {
                        sDateDiff = _dv.ToTable().Compute("AVG(DATEDIFFinDays)", "").ToString();
                        if (sDateDiff != "")
                        {
                            c1PostedTotal.SetData(0, COL_CheckDate, sDateDiff + " Days");
                        }
                        else
                        {
                            c1PostedTotal.SetData(0, COL_CheckDate, sDateDiff);
                        }
                    }

                    #endregion
                }
                _C1.Redraw = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                _dv = null;
                _C1 = null;
            }
        }
        private void btnShowChecks_Click(object sender, EventArgs e)
        {
            DataView _dv = null;
            try
            {
                if (tabERA.SelectedTab.Tag.ToString() == TAB_DELETED)
                {

                    #region " Date Validations "

                    if (mskFromDateDeletd.Text.ToString() == "" && mskToDateDeleted.Text.ToString() == "")
                    {
                        RefreshView();
                        return;
                    }
                    if (mskFromDateDeletd.Text != "" && mskFromDateDeletd.MaskFull == false)
                    {
                        MessageBox.Show("Enter valid date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskFromDateDeletd.Focus();
                        return;
                    }
                    if (mskToDateDeleted.Text != "" && mskToDateDeleted.MaskFull == false)
                    {
                        MessageBox.Show("Enter valid date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskToDateDeleted.Focus();
                        return;
                    }

                    if (mskToDateDeleted.Text == "") mskToDateDeleted.Text = mskFromDateDeletd.Text;
                    if (mskFromDateDeletd.Text == "") mskFromDateDeletd.Text = mskToDateDeleted.Text;

                    mskFromDateDeletd.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    mskToDateDeleted.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

                    if (!gloDateMaster.gloDate.IsValidDate(mskFromDateDeletd.Text))
                    {
                        MessageBox.Show("Enter valid date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskFromDateDeletd.Focus();
                        return;
                    }
                    if (!gloDateMaster.gloDate.IsValidDate(mskToDateDeleted.Text))
                    {
                        MessageBox.Show("Enter valid date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskToDateDeleted.Focus();
                        return;
                    }
                    #endregion

                    _dv = (DataView)c1Deleted.DataSource;
                    if (_dv != null)
                    {
                        _dv.RowFilter = "CheckDateNumeric >= " + gloDateMaster.gloDate.DateAsNumber(mskFromDateDeletd.Text) + " AND CheckDateNumeric <= " + gloDateMaster.gloDate.DateAsNumber(mskToDateDeleted.Text);
                        //for (int iRow = 1; iRow < c1Deleted.Rows.Count; iRow++)
                        //{
                        //    if (c1Deleted.GetData(iRow, COL_NotesCount).ToString() != "" && Convert.ToInt32(c1Deleted.GetData(iRow, COL_NotesCount).ToString()) > 0)
                        //        c1Deleted.SetCellImage(iRow, COL_NotesCount, global::gloBilling.Properties.Resources.Notes);
                        //    else
                        //        c1Deleted.SetData(iRow, COL_NotesCount, "");

                        //    if (Convert.ToInt64(c1Deleted.GetData(iRow, COL_BPRID).ToString()) == nSelectedRowDeleteID)
                        //        c1Deleted.Select(iRow, COL_BPRID);

                        //}
                    }
                }
                else if (tabERA.SelectedTab.Tag.ToString() == TAB_POSTED)
                {

                    #region " Date Validations "

                    if (mskFromDatePosted.Text.ToString() == "" && mskToDatePosted.Text.ToString() == "")
                    {
                        RefreshView();
                        return;
                    }

                    if (mskFromDatePosted.Text != "" && mskFromDatePosted.MaskFull == false)
                    {
                        MessageBox.Show("Enter valid date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskFromDatePosted.Focus();
                        return;
                    }
                    if (mskToDatePosted.Text != "" && mskToDatePosted.MaskFull == false)
                    {
                        MessageBox.Show("Enter valid date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskToDatePosted.Focus();
                        return;
                    }

                    if (mskToDatePosted.Text == "") mskToDatePosted.Text = mskFromDatePosted.Text;
                    if (mskFromDatePosted.Text == "") mskFromDatePosted.Text = mskToDatePosted.Text;

                    mskFromDatePosted.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    mskToDatePosted.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

                    if (!gloDateMaster.gloDate.IsValidDate(mskFromDatePosted.Text))
                    {
                        MessageBox.Show("Enter valid date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskFromDatePosted.Focus();
                        return;
                    }
                    if (!gloDateMaster.gloDate.IsValidDate(mskToDatePosted.Text))
                    {
                        MessageBox.Show("Enter valid date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskToDatePosted.Focus();
                        return;
                    }

                    #endregion

                    _dv = (DataView)c1Posted.DataSource;
                    if (_dv != null)
                    {
                        _dv.RowFilter = "CheckDateNumeric >= " + gloDateMaster.gloDate.DateAsNumber(mskFromDatePosted.Text) + " AND CheckDateNumeric <= " + gloDateMaster.gloDate.DateAsNumber(mskToDatePosted.Text);
                        //for (int iRow = 1; iRow < c1Posted.Rows.Count; iRow++)
                        //{
                        //    if (c1Posted.GetData(iRow, COL_NotesCount).ToString() != "" && Convert.ToInt32(c1Posted.GetData(iRow, COL_NotesCount).ToString()) > 0)
                        //        c1Posted.SetCellImage(iRow, COL_NotesCount, global::gloBilling.Properties.Resources.Notes);
                        //    else
                        //        c1Posted.SetData(iRow, COL_NotesCount, "");

                        //    if (Convert.ToInt64(c1Posted.GetData(iRow, COL_BPRID).ToString()) == nSelectedRowPostedID)
                        //        c1Posted.Select(iRow, COL_BPRID);

                        //}
                        string sDateDiff = "";
                        sDateDiff = _dv.ToTable().Compute("AVG(DATEDIFFinDays)", "").ToString();
                        if (sDateDiff != "")
                        {
                            c1PostedTotal.SetData(0, COL_CheckDate, sDateDiff + " Days");
                        }
                        else
                        {
                            c1PostedTotal.SetData(0, COL_CheckDate, sDateDiff);
                        }
                    }
                }
                else if (tabERA.SelectedTab.Tag.ToString() == TAB_HOLD)
                {

                    #region " Date Validations "

                    if (mskFromDateHold.Text.ToString() == "" && mskToDateHold.Text.ToString() == "")
                    {
                        RefreshView();
                        return;
                    }

                    if (mskFromDateHold.Text != "" && mskFromDateHold.MaskFull == false)
                    {
                        MessageBox.Show("Enter valid date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskFromDateHold.Focus();
                        return;
                    }
                    if (mskToDateHold.Text != "" && mskToDateHold.MaskFull == false)
                    {
                        MessageBox.Show("Enter valid date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskToDateHold.Focus();
                        return;
                    }

                    if (mskToDateHold.Text == "") mskToDateHold.Text = mskFromDateHold.Text;
                    if (mskFromDateHold.Text == "") mskFromDateHold.Text = mskToDateHold.Text;

                    mskFromDateHold.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    mskToDateHold.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

                    if (!gloDateMaster.gloDate.IsValidDate(mskFromDateHold.Text))
                    {
                        MessageBox.Show("Enter valid date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskFromDateHold.Focus();
                        return;
                    }
                    if (!gloDateMaster.gloDate.IsValidDate(mskToDateHold.Text))
                    {
                        MessageBox.Show("Enter valid date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskToDateHold.Focus();
                        return;
                    }

                    #endregion

                    _dv = (DataView)c1Hold.DataSource;
                    if (_dv != null)
                    {
                        _dv.RowFilter = "CheckDateNumeric >= " + gloDateMaster.gloDate.DateAsNumber(mskFromDateHold.Text) + " AND CheckDateNumeric <= " + gloDateMaster.gloDate.DateAsNumber(mskToDateHold.Text);
                        //for (int iRow = 1; iRow < c1Hold.Rows.Count; iRow++)
                        //{
                        //    if (c1Hold.GetData(iRow, COL_NotesCount).ToString() != "" && Convert.ToInt32(c1Hold.GetData(iRow, COL_NotesCount).ToString()) > 0)
                        //        c1Hold.SetCellImage(iRow, COL_NotesCount, global::gloBilling.Properties.Resources.Notes);
                        //    else
                        //        c1Hold.SetData(iRow, COL_NotesCount, "");

                        //    if (Convert.ToInt64(c1Hold.GetData(iRow, COL_BPRID).ToString()) == nSelectedRowHoldID)
                        //        c1Hold.Select(iRow, COL_BPRID);

                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _dv = null;
                mskFromDateDeletd.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskToDateDeleted.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskFromDatePosted.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskToDatePosted.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskFromDateHold.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskToDateHold.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            }
        }

        private string GetServerPath()
        {

            Object retVal = null;
            string _isValidPath = "";

            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "SELECT sSettingsValue FROM Settings WITH(NOLOCK) WHERE UPPER(sSettingsName) = 'SERVERPATH'";
                    retVal = oDB.ExecuteScalar_Query(_TempStr);
                    CloseConnection();

                    if (retVal != null && retVal != DBNull.Value)
                    {
                        _isValidPath = Convert.ToString(retVal);
                        if (System.IO.Directory.Exists(_isValidPath) == false)
                        { _isValidPath = ""; }
                    }
                    else
                    { _isValidPath = ""; }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                retVal = null;
            }
            return _isValidPath;
        }

        #region " Addition Check Info Buttons "
        private void btnDown_Click(object sender, EventArgs e)
        {
            pnlAdditionalCheckInfo.Visible = false;
            btnDown.Visible = false;
            btnUP.Visible = true;
            btnUP.Focus();
        }

        private void btnUP_Click(object sender, EventArgs e)
        {
            pnlAdditionalCheckInfo.Visible = true;
            btnUP.Visible = false;
            btnDown.Visible = true;
            btnDown.Focus();
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            try
            {
                if (((Button)sender).Name.ToString() == "btnUP")
                    ((Button)sender).Image = global::gloBilling.Properties.Resources.UPHover;
                else
                    ((Button)sender).Image = global::gloBilling.Properties.Resources.DownHover;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (((Button)sender).Name.ToString() == "btnUP")
                    ((Button)sender).Image = global::gloBilling.Properties.Resources.UP;
                else
                    ((Button)sender).Image = global::gloBilling.Properties.Resources.Down;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
        }
        #endregion

        #region " Mask Box Events "

        private void All_MaskBox_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        #endregion

        private Boolean getReport(Int64 nBPRId)
        {

            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            Boolean bExists = false;

            try
            {
                oConnection.ConnectionString = _DataBaseConnectionString;
                oConnection.Open();
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandText = "select iBinaryFiles,sReportName,nFileType from ERA_Reports Where nBPRID=" + nBPRId;

                da.SelectCommand = _sqlcommand;
                da.Fill(ds, "dt");
                string sFileFormat = "";
                int iFileType;

                if (ds.Tables[0].Rows.Count > 0)
                {

                    iFileType = Convert.ToInt32(ds.Tables[0].Rows[0]["nFileType"].ToString());
                    switch (iFileType)
                    {
                        case 1:
                            sFileFormat = ".RTF";
                            break;
                        case 2:
                            sFileFormat = ".PDF";
                            break;
                    }


                    string sFileName = ds.Tables[0].Rows[0]["sReportName"].ToString() + sFileFormat;


                    string strPath = gloSettings.FolderSettings.AppTempFolderPath + sFileName;

                    Int32 iFile = 1;
                    while (File.Exists(strPath))
                    {
                        strPath = gloSettings.FolderSettings.AppTempFolderPath + "(" + iFile + ")" + sFileName;
                        iFile++;
                    }


                    byte[] buffer = (byte[])ds.Tables[0].Rows[0]["iBinaryFiles"];
                    FileStream fs = new FileStream(strPath, FileMode.Create);
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Close();
                    try
                    {
                        System.Diagnostics.Process.Start(strPath);
                    }
                    catch (Exception ex)
                    {
                        //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message + Environment.NewLine + Environment.NewLine + "Install PDF Viewer.", true);
                        MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + "Install PDF Viewer.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    bExists = true;
                }
                oConnection.Close();
                return bExists;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return bExists;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }

            }
        }



        private void SSFillERAData()
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {

                Cursor = Cursors.WaitCursor;
                oConnection.ConnectionString = _DataBaseConnectionString;

                oConnection.Open();
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "ERA_SSRSPostingData";
                _sqlcommand.Parameters.Add("@nBPRID", SqlDbType.BigInt).Value = _TempID;//
                _sqlcommand.Parameters.Add("@UserID", SqlDbType.BigInt).Value = _UserID;

                da.SelectCommand = _sqlcommand;
                da.Fill(ds, "dt");


                decimal PmntNotBalanced;
                PmntNotBalanced = System.Convert.ToDecimal(ds.Tables["dt"].Rows[0]["CheckAmount"]) - (System.Convert.ToDecimal(ds.Tables["dt"].Rows[0]["TotalClaimPaid"].ToString()) - System.Convert.ToDecimal(ds.Tables["dt"].Rows[0]["TotalPLBAmount"]));
                string BalError = "";


                if (PmntNotBalanced != 0)
                {
                    BalError = "Payment Not Posted : Balance Error";

                }
                // else if (System.Convert.ToDecimal(ds.Tables["dt"].Rows[0]["ERAPostedAmount"]) == 0)
                else if (sStopFlag == StopFlag.NoClaimProcessed)
                {
                    BalError = "Payment Not Posted";
                }

                _practice = ds.Tables["dt"].Rows[0]["PracticeName"].ToString();
                _ClaimProcessedCountComment = ds.Tables["dt"].Rows[0]["ClaimProcessedCountComment"].ToString();
                _BalError = BalError;
                _UserName = ds.Tables["dt"].Rows[0]["UserName"].ToString();
                strFileName = 'P' + ds.Tables["dt"].Rows[0]["PayerID"].ToString().Trim() + 'C' + ds.Tables["dt"].Rows[0]["CheckNo"].ToString() + 'D' + ds.Tables["dt"].Rows[0]["CheckDate"].ToString().Replace("/", "");

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                Cursor = Cursors.Default;
                if (oConnection != null) //connection close done
                {
                    if (oConnection.State == ConnectionState.Open)
                    {
                        oConnection.Close();
                        oConnection.Dispose();
                    }
                }
            }

        }



        private void SSFillERAData_EOB()
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                Cursor = Cursors.WaitCursor;
                oConnection.ConnectionString = _DataBaseConnectionString;

                oConnection.Open();
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "ERA_SSRSEOBData";
                _sqlcommand.Parameters.Add("@BPRID", SqlDbType.BigInt).Value = _TempID;//
                _sqlcommand.Parameters.Add("@UserID", SqlDbType.BigInt).Value = _UserID;

                da.SelectCommand = _sqlcommand;
                da.Fill(ds, "dt");
                _practice = ds.Tables["dt"].Rows[0]["PracticeName"].ToString();
                _UserName = ds.Tables["dt"].Rows[0]["UserName"].ToString();

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                Cursor = Cursors.Default;
                if (oConnection != null) //connection close done
                {
                    if (oConnection.State == ConnectionState.Open)
                    {
                        oConnection.Close();
                        oConnection.Dispose();
                    }
                }
            }

        }


        private void SSFillERAData_Exceptions()
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {

                Cursor = Cursors.WaitCursor;
                oConnection.ConnectionString = _DataBaseConnectionString;

                oConnection.Open();
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "ERA_SSRSExceptionData";
                _sqlcommand.Parameters.Add("@nBPRID", SqlDbType.BigInt).Value = _TempID;//
                _sqlcommand.Parameters.Add("@UserID", SqlDbType.BigInt).Value = _UserID;

                da.SelectCommand = _sqlcommand;
                da.Fill(ds, "dt");



                string BalError = "";

                decimal ERAAmt;
                decimal CheckAmt;

                ERAAmt = Convert.ToDecimal(ds.Tables["dt"].Rows[0]["ERAPostedAmount"].ToString());
                CheckAmt = Convert.ToDecimal(ds.Tables["dt"].Rows[0]["CheckAmount"].ToString());


                if (ERAAmt > CheckAmt)
                {
                    BalError = "Payment is Over-Allocated";

                }

                _practice = ds.Tables["dt"].Rows[0]["PracticeName"].ToString();
                _ClaimProcessedCountComment = ds.Tables["dt"].Rows[0]["ClaimProcessedCountComment"].ToString();
                _BalError = BalError;
                _UserName = ds.Tables["dt"].Rows[0]["UserName"].ToString();

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                Cursor = Cursors.Default;

                if (oConnection != null) //connection close done
                {
                    if (oConnection.State == ConnectionState.Open)
                    {
                        oConnection.Close();
                        oConnection.Dispose();
                    }
                }
            }

        }


        private void ShowSSRSReport(enum_ReportType _ReportType)
        {
            using (frmSSRS = new SSRSApplication.frmSSRSViewer())
            {

                switch (_ReportType)
                {
                    case enum_ReportType.EOB:
                        {
                            SSFillERAData_EOB();
                            frmSSRS.parameterName = "practice,UserName,BPRID,UserID";
                            frmSSRS.ParameterValue = _practice + "," + _UserName + "," + _TempID + "," + _UserID;
                            frmSSRS.reportName = "rptERAEOB";
                            frmSSRS.formIcon = null;
                            frmSSRS.reportTitle = "EOB Report";
                            break;
                        }
                    case enum_ReportType.Posting:
                        {
                            lblProgress.Text = "Preparing Report ";
                            Application.DoEvents();
                            frmSSRS.Close_Click += new SSRSApplication.frmSSRSViewer.CloseClick(frmSSRS_Close_Click);
                            SSFillERAData();
                            frmSSRS.parameterName = "practice,ClaimProcessedCountComment,BalError,UserName,nBPRID,UserID";
                            frmSSRS.ParameterValue = _practice + "," + _ClaimProcessedCountComment + "," + _BalError + "," + _UserName + "," + _TempID + "," + _UserID;
                            frmSSRS.reportName = "rptERAPosting";
                            frmSSRS.formIcon = null;
                            frmSSRS.reportTitle = "Posting Report";
                            frmSSRS.Tray_Load += new SSRSApplication.frmSSRSViewer.TrayLoad(frmSSRS_Tray_Load);
                            frmSSRS.Accept_Click += new SSRSApplication.frmSSRSViewer.AcceptClick(frmSSRS_Accept_Click);
                            frmSSRS.Tray_Click += new SSRSApplication.frmSSRSViewer.TrayClick(frmSSRS_Tray_Click);
                            break;
                        }
                    case enum_ReportType.Exception:
                        {
                            SSFillERAData_Exceptions();
                            frmSSRS.parameterName = "practice,ClaimProcessedCountComment,BalError,UserName,nBPRID,UserID";
                            frmSSRS.ParameterValue = _practice + "," + _ClaimProcessedCountComment + "," + _BalError + "," + _UserName + "," + _TempID + "," + _UserID;
                            frmSSRS.reportName = "rptERAExceptions";
                            frmSSRS.formIcon = null;
                            frmSSRS.reportTitle = "Exceptions Report";
                            break;
                        }
                }
                frmSSRS.Conn = _DataBaseConnectionString;
                frmSSRS.IsgloStreamReport = true;
                frmSSRS.ShowDialog(this);
                frmSSRS.Dispose();
                frmSSRS = null;
            }
        }

        private void NoClaimProcessed(string sMsg)
        {
            DialogResult _dlgRst = DialogResult.None;
            string sDisplayMsg = String.Format(sMsg + " ERA Check will not be posted.\n\nMark ERA check as 'Deleted'?");
            _dlgRst = DialogResult.None;
            _dlgRst = MessageBox.Show(sDisplayMsg, _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (_dlgRst == DialogResult.Yes)
            {
                ClsERAValidation.UpdateCheckStatus(_TempID, enumCheckStatus.MarkedDeleted.GetHashCode());
                _bIsSaved = true;
                //this.Close();

            }
        }

        void frmSSRS_Tray_Click(object Sender, EventArgs e)
        {

            SelectPaymentTray();

        }
        //start
        void frmSSRS_Tray_Load(object Sender, EventArgs e)
        {

            LoadClosePaymentTray();

        }

        void frmSSRS_Close_Click(object Sender, EventArgs e)
        {

            try
            {
                if (!_bIsSaved)
                {
                    InsurancePayment.UnlockCheckClaims(_TempID);
                    ClsERAValidation.UpdateCheckStatus(_TempID, enumCheckStatus.Ready.GetHashCode());
                    ClsERAValidation.DeleteClaimMatchDetailsonClose(_nOperationID , 0);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }


        }

        private void LoadClosePaymentTray()
        {
            if (!IsCloseDaySet)
            {
                SetCloseDate();
            }

            if (SelectedPaymentTrayID == 0)
            { FillPaymentTray(); }
            else
            {
                Int64 _lastSelectedTrayID = BillingSettings.LastSelectedPaymentTrayID;
                Int64 _defaultTrayID = InsurancePayment.GetDefaultPaymentTrayID();

                frmSSRS.PaymentTray = InsurancePayment.GetPaymentTrayDescription(_defaultTrayID);
                frmSSRS.TrayID = _defaultTrayID;

                if (_lastSelectedTrayID > 0)
                {
                    if (InsurancePayment.IsPaymentTrayActive(_lastSelectedTrayID))
                    {
                        if (!_lastSelectedTrayID.Equals(_defaultTrayID))
                        {
                            frmSSRS.PaymentTray = InsurancePayment.GetPaymentTrayDescription(_lastSelectedTrayID);
                            frmSSRS.TrayID = _lastSelectedTrayID;
                            SelectPaymentTray();
                        }
                    }
                    else
                    {
                        SelectPaymentTray();
                    }
                }

            }
        }

        private bool IsCloseDaySet
        {
            get { return frmSSRS.mskCloseDate.MaskCompleted; }
        }

        private void SetCloseDate()
        {
            gloBilling ogloBilling = new gloBilling(_DataBaseConnectionString, string.Empty);

            try
            {
                //...Load last selected close date
                //string _lastCloseDate = BillingSettings.LastSelectedCloseDate;
                string _lastCloseDate = gloBilling.GetUserWiseCloseDay(_UserID, CloseDayType.Payment);

                //...If the last selected close date is being closed then make the close date empty.
                if (!_lastCloseDate.Equals(string.Empty))
                {
                    if (ogloBilling.IsDayClosed(Convert.ToDateTime(_lastCloseDate)) == true)
                    {
                        _lastCloseDate = string.Empty;
                    }
                }
                //mskCloseDate = _lastCloseDate;

                frmSSRS.mskCloseDate.Text = _lastCloseDate;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloBilling.Dispose();
            }
        }

        private void FillPaymentTray()
        {
            Int64 _lastSelectedTrayID = BillingSettings.LastSelectedPaymentTrayID;
            Int64 _defaultTrayID = InsurancePayment.GetDefaultPaymentTrayID();

            // Set default payment tray
            SelectedPaymentTray = InsurancePayment.GetPaymentTrayDescription(_defaultTrayID);
            SelectedPaymentTrayID = _defaultTrayID;

            if (_lastSelectedTrayID > 0)
            {
                if (InsurancePayment.IsPaymentTrayActive(_lastSelectedTrayID))
                {
                    if (!_lastSelectedTrayID.Equals(_defaultTrayID))
                    {
                        SelectedPaymentTray = InsurancePayment.GetPaymentTrayDescription(_lastSelectedTrayID);
                        SelectedPaymentTrayID = _lastSelectedTrayID;
                        SelectPaymentTray();
                    }
                }
                else
                {
                    SelectPaymentTray();
                }
            }
        }
        //End
        private void SelectPaymentTray()
        {
            frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(_DataBaseConnectionString);

            try
            {
                //...Show pop-up to select the Tray
                ofrmBillingTraySelection.WindowState = FormWindowState.Normal;
                ofrmBillingTraySelection.StartPosition = FormStartPosition.CenterParent;
                ofrmBillingTraySelection.IsChargeTray = false;
                ofrmBillingTraySelection.ShowDialog(this);

                // toolTip1.SetToolTip(lbBillingtray, null);

                if (ofrmBillingTraySelection.FormResult == DialogResult.OK)
                {
                    if (ofrmBillingTraySelection.SelectedTrayID > 0 && frmSSRS != null)
                    {
                        frmSSRS.PaymentTray = ofrmBillingTraySelection.SelectedTrayName;
                        frmSSRS.TrayID = ofrmBillingTraySelection.SelectedTrayID;
                        SelectedPaymentTray = ofrmBillingTraySelection.SelectedTrayName;
                        SelectedPaymentTrayID = ofrmBillingTraySelection.SelectedTrayID;
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


        private void ValidateDate(object sender, CancelEventArgs e)
        {
            int _addDays = 0;
            MaskedTextBox mskDate = (MaskedTextBox)sender;
            mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string strDate = mskDate.Text;
            mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();
            bool _isValid = false;

            if (mskDate != null)
            {
                if (strDate.Length > 0)
                {
                    _isValid = gloDate.IsValid(mskDate.Text);

                    if (!_isValid)
                    {
                        MessageBox.Show("Please enter a valid date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskDate.Clear();
                        mskDate.Focus();
                        e.Cancel = true;
                    }
                    if (_isValid && Convert.ToDateTime(frmSSRS.mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
                    {
                        MessageBox.Show("Close date " + frmSSRS.mskCloseDate.Text.Trim() + " is too far in the future. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning); frmSSRS.mskCloseDate.Focus();
                        frmSSRS.mskCloseDate.Select();
                        e.Cancel = true;
                    }
                }
            }



        }

        private bool IsValidCloseDate()
        {
            int _addDays = 0;
            _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();
            if (frmSSRS.mskCloseDate.MaskCompleted == false)
            {
                MessageBox.Show("Please enter the close date", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmSSRS.mskCloseDate.Focus();
                frmSSRS.mskCloseDate.Select();
                return false;
            }


            if (gloDate.IsValid(frmSSRS.mskCloseDate.Text) == false)
            {
                CancelEventArgs e = new CancelEventArgs();
                ValidateDate(frmSSRS.mskCloseDate, e);
                if (e.Cancel)
                { return false; }
            }
            //else
            //{
                #region " check for day closed "

                gloBilling ogloBilling = new gloBilling(_DataBaseConnectionString, string.Empty);
                if (ogloBilling.IsDayClosed(Convert.ToDateTime(frmSSRS.mskCloseDate.Text)) == true)
                {
                    MessageBox.Show("Selected date is already closed. Please select a different close date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmSSRS.mskCloseDate.Select(); frmSSRS.mskCloseDate.Focus();
                    if (ogloBilling != null)
                    {
                        ogloBilling.Dispose();
                        ogloBilling = null;
                    }
                    return false;
                }
                ogloBilling.Dispose();

                #endregion " check for day closed  "




                if (Convert.ToDateTime(frmSSRS.mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
                {
                    MessageBox.Show("Close date " + frmSSRS.mskCloseDate.Text.Trim() + " is too far in the future. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning); frmSSRS.mskCloseDate.Focus();
                    frmSSRS.mskCloseDate.Select();
                    return false;
                }
                else
                {
                    if (Convert.ToDateTime(frmSSRS.mskCloseDate.Text.Trim()).Date > DateTime.Now.Date)
                    {
                        DialogResult _dlgCloseDate = DialogResult.None;
                        _dlgCloseDate = MessageBox.Show("Close date " + frmSSRS.mskCloseDate.Text.Trim() + " is in future.Are you sure you want to continue with save ?", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (_dlgCloseDate == DialogResult.Cancel)
                        {
                            frmSSRS.mskCloseDate.Focus();
                            frmSSRS.mskCloseDate.Select();
                            return false;
                        }
                    }
                }
            //}

            if (!IsValidClaimCloseDate())
            {
                MessageBox.Show("Cannot save payment – payment close date precedes charge’s close date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning); frmSSRS.mskCloseDate.Focus();
                frmSSRS.mskCloseDate.Select();
                return false;
            }
            return true;
        }



        private bool IsValidPaymentTray()
        {
            if (SelectedPaymentTrayID == 0)
            {
                MessageBox.Show("Please select the payment tray.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmSSRS.btnTraySelection.Select();
                frmSSRS.btnTraySelection.Focus();
                return false;
            }
            else if (InsurancePayment.IsPaymentTrayActive(SelectedPaymentTrayID) == false)
            {
                MessageBox.Show("The payment tray selected is inactive. Please select another tray", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmSSRS.btnTraySelection.Select();
                frmSSRS.btnTraySelection.Focus();
                return false;
            }
            return true;
        }

        private bool IsValidClaimCloseDate()
        {
            gloDatabaseLayer.DBParameters oDBPara = new gloDatabaseLayer.DBParameters();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            SqlConnection _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                Object _result;
                Int64 nCloseDate = 0;

                oDBPara.Add("@nBPRID", _TempID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPara.Add("@nTransactionDate", 0, ParameterDirection.Output, SqlDbType.BigInt);


                _sqlConnection.Open();
                using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oDBPara))
                {
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = "ERA_GETCLAIMCLOSEDATE";
                    _result = _sqlCommand.ExecuteNonQuery();
                    if (_sqlCommand.Parameters["@nTransactionDate"].Value != DBNull.Value)
                    { nCloseDate = Convert.ToInt64(_sqlCommand.Parameters["@nTransactionDate"].Value); }

                }

                if (nCloseDate > gloDateMaster.gloDate.DateAsNumber(frmSSRS.mskCloseDate.Text.ToString()))
                    return false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                _sqlConnection.Close();
                if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                if (oDBPara != null) { oDBPara.Dispose(); }



            }
            return true;
        }


        private void saveReport()
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            try
            {


                

                
                byte[] buffer = frmSSRS.SSRSViewer.ServerReport.Render("PDF");
                oConnection.ConnectionString = _DataBaseConnectionString;
                oConnection.Open();
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.Parameters.Clear();
                _sqlcommand.CommandText = "ERA_InUpReport";
                _sqlcommand.Parameters.Add("@nFileID", SqlDbType.BigInt).Value = _FileId;//
                _sqlcommand.Parameters.Add("@nBPRID", SqlDbType.BigInt).Value = _TempID;
                _sqlcommand.Parameters.Add("@sReportName", SqlDbType.VarChar).Value = setFileName(strFileName);//
                _sqlcommand.Parameters.Add("@iBinaryFiles", SqlDbType.Image).Value = buffer;
                _sqlcommand.Parameters.Add("@nReportType", SqlDbType.Int).Value = enumReportType.PostingReport;//
                _sqlcommand.Parameters.Add("@nFileType", SqlDbType.Int).Value = enumReportFormat.PDF;//
                _sqlcommand.Parameters.Add("@nUserID", SqlDbType.BigInt).Value = _UserID;
                _sqlcommand.Parameters.Add("@nClinicID", SqlDbType.BigInt).Value = _ClinicID;//

                _sqlcommand.ExecuteNonQuery();
                oConnection.Close();
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
            }


        }

        private string setFileName(string strFileName)
        {
            try
            {
                strFileName = System.Text.RegularExpressions.Regex.Replace(strFileName, @"[:,/,\\,;,*,?,<,>,|,""]", String.Empty);

                //strFileName = strFileName.Replace(@"\", "");
                //strFileName = strFileName.Replace(@"/", "");
                //strFileName = strFileName.Replace(";", "");
                //strFileName = strFileName.Replace("*", "");
                //strFileName = strFileName.Replace("?", "");
                //strFileName = strFileName.Replace("<", "");
                //strFileName = strFileName.Replace(">", "");
                //strFileName = strFileName.Replace("|", "");
                //strFileName = strFileName.Replace(@"""", "");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }

            return strFileName;


        }


        void frmSSRS_Accept_Click(object Sender, EventArgs e, SSRSApplication.AcceptClickArgs eAccept, ref ProgressBar oProgress, ref Label oLabel)
        {
            gloBilling ogloBilling = new gloBilling(_DataBaseConnectionString, "");
            clsERAPostingV2 oERAPosting = null;
           try
            {

               
                DialogResult _dlgRst = DialogResult.None;
                _dlgRst = MessageBox.Show("Accept ERA Postings?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dlgRst == DialogResult.Yes)
                {
                    //Int64 _retValue = 0;                  
                    string sMsg = string.Empty;
                    if (ClsERAValidation.ValidateReasonCodePayer(_TempID, out sMsg))
                    {
                        object[] objPlaceHolders = { "\n\n", "\n" };
                        MessageBox.Show(string.Format(sMsg, objPlaceHolders), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                         return;
                    }



                    if (IsValidCloseDate() && IsValidPaymentTray())
                    {

                        oERAPosting = new clsERAPostingV2();
                        string sMessage = string.Empty;

                        StopFlag oStopFlag = StopFlag.NotProcessed;

                        #region "Duplicate Check"


                        DataTable dtTempCheckDetails = null;

                        dtTempCheckDetails = oERAPosting.GetTemporaryPostedCheckDetails(_TempID);
                        bool bFlag = false;

                        if (dtTempCheckDetails != null)
                        {
                            if (dtTempCheckDetails.Rows.Count > 0)
                            {
                                string sCheckNumber = string.Empty;
                                string sSelectedInsuranceCompany = string.Empty;
                                decimal dCheckAmount = 0;
                                Int64 nSelectedInsuranceCompanyID = 0;

                                sCheckNumber = dtTempCheckDetails.Rows[0]["sCheckNo"].ToString();
                                dCheckAmount = Convert.ToDecimal(dtTempCheckDetails.Rows[0]["CheckAmount"].ToString());
                                nSelectedInsuranceCompanyID = Convert.ToInt64(dtTempCheckDetails.Rows[0]["sPayerID"].ToString());
                                sSelectedInsuranceCompany = dtTempCheckDetails.Rows[0]["sPayerName"].ToString();

                                if (ClsERAValidation.IsCheckDuplicate(_TempID, sCheckNumber, dCheckAmount, nSelectedInsuranceCompanyID))  // Returns nContactID/PayerID //1044623:HardCode
                                {
                                    // Logs are written using Stored Procedure.
                                    // Stop Posting if nContactID is 0.

                                    _dlgRst = DialogResult.None;
                                    string sDisplayMsg = string.Empty;
                                    sDisplayMsg = string.Format("ERA check already posted.\n\n     Payer: {0} \n     Check Number: {1} \n     Check Amount: {2}\n\nContinue posting ERA check? ", sSelectedInsuranceCompany, sCheckNumber, dCheckAmount);
                                    _dlgRst = MessageBox.Show(sDisplayMsg, _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_dlgRst == DialogResult.No)
                                    {
                                        sMessage = "ERA check already posted.";
                                        return;
                                    }
                                    //continue;
                                }
                            }
                            else
                            {
                                sMsg = "No Claims Processed.";
                                bFlag = true;
                            }
                        }
                        else
                        {
                            sMsg = "No Claims Processed.";
                            bFlag = true;
                        }
                        dtTempCheckDetails = null;

                        #endregion

                        if (bFlag)
                        {
                            NoClaimProcessed(sMsg);
                            //this.Close();
                            InsurancePayment.UnlockCheckClaims(_TempID);
                            frmSSRS.Close();
                            return;
                        }
                        frmSSRS.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                        frmSSRS.Close_Btn = false;
                        frmSSRS.Accept_Btn = false;
                        oProgress.Value = 0;
                        oProgress.Maximum = 80;
                        oProgress.Minimum = 0;
                        oProgress.Width = 410;
                        oProgress.Visible = true;
                       
                        if (!oERAPosting.PostERAFile_New(_TempID, SelectedPaymentTrayID, frmSSRS.mskCloseDate.Text, SelectedPaymentTrayID, out sMessage, out oStopFlag, ref oProgress, ref oLabel, ref frmSSRS))
                        {
                            frmSSRS.Cursor = System.Windows.Forms.Cursors.Default;
                            frmSSRS.Close_Btn = true;
                            frmSSRS.Accept_Btn = true;
                            if (sMessage != "")
                                MessageBox.Show(sMessage, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            ClsERAValidation.UpdateCheckStatus(_TempID, enumCheckStatus.Ready.GetHashCode());
                            //this.Close();
                            InsurancePayment.UnlockCheckClaims(_TempID);
                            frmSSRS.Close();
                            return;
                        }
                        else
                        {
                            #region " ACTUAL POSTING HERE "
                            _bIsSaved = true;
                            
                            saveReport();
                            
                            ClsERAValidation.UpdateCheckStatus(_TempID, enumCheckStatus.Posted.GetHashCode());
                            //Saving Close Date Against User
                            ogloBilling.SaveUserWiseCloseDay(frmSSRS.mskCloseDate.Text.Trim(), CloseDayType.Payment, _ClinicID);
                            //this.Close();
                            InsurancePayment.UpdateDenialQueue(_UserID, _UserName, _ClinicID, _TempID);
                            InsurancePayment.UnlockCheckClaims(_TempID);                            
                            frmSSRS.Close();
                            #endregion
                        }
                    }
                    else
                    {
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                InsurancePayment.UnlockCheckClaims(_TempID);
            }
            finally
            {
                Cursor = Cursors.Default;
                frmSSRS.Close_Btn = true;
                frmSSRS.Accept_Btn = true;
                
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
          
            
        }

        #endregion

        private void txtSearchAll_KeyDown(object sender, KeyEventArgs e)
        {
            // SEARCH IS IMPLEMENTED ON TIMER TICK //
            _CurrentTime = DateTime.Now;
            oTimer.Stop();
            oTimer.Interval = 700;
            oTimer.Enabled = true;
        }

        private void c1Posted_AfterScroll(object sender, RangeEventArgs e)
        {
            try
            {
                c1PostedTotal.ScrollPosition = c1Posted.ScrollPosition;
            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1ReadyToPost_AfterScroll(object sender, RangeEventArgs e)
        {
            try
            {
                c1ReadyToPostTotal.ScrollPosition = c1ReadyToPost.ScrollPosition;
            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1Posted_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            try
            {
                c1PostedTotal.Cols[e.Col].Width = c1Posted.Cols[e.Col].Width;

            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1ReadyToPost_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            try
            {
                c1ReadyToPostTotal.Cols[e.Col].Width = c1ReadyToPost.Cols[e.Col].Width;

            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1FlexGrid_MouseClick(object sender, MouseEventArgs e)
        {
             
         
              //try
              //{
              //    HitTestInfo hitInfo = ((C1.Win.C1FlexGrid.C1FlexGrid)sender).HitTest(e.X, e.Y);

              //    if (((C1.Win.C1FlexGrid.C1FlexGrid)sender).Rows.Count > 0)
              //    {
              //        if (hitInfo.Row > 0)
              //        {
              //            if (((C1.Win.C1FlexGrid.C1FlexGrid)sender).GetData(hitInfo.Row, COL_NotesCount).ToString() != "" && Convert.ToInt32(((C1.Win.C1FlexGrid.C1FlexGrid)sender).GetData(hitInfo.Row, COL_NotesCount).ToString()) > 0)
              //            {
              //                this.tsb_Notes.Image = global::gloBilling.Properties.Resources.Edit_Note;//((System.Drawing.Image)(global::gloBilling.frmNotes.resources.GetObject("tlb_Notes.Image"))); 
              //            }
              //            else
              //            {
              //                this.tsb_Notes.Image = global::gloBilling.Properties.Resources.Add_Note;
              //            }

              //        }
              //    }
                                   

              //}
              //catch (Exception ex)
              //{
              //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
              //}

        }

        private void cmbPosted_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cmbPosted.SelectedItem != null)
            {
                if (cmbPosted.SelectedIndex == 0)
                    _PostedDateRange = DateTime.Now.AddDays(-7).Date;
                else if (cmbPosted.SelectedIndex == 1)
                    _PostedDateRange = DateTime.Now.AddMonths(-1).Date;
                else if (cmbPosted.SelectedIndex == 2)
                    _PostedDateRange = DateTime.Now.AddYears(-1).Date;
                else
                    _PostedDateRange = DateTime.MinValue.Date;
                if (!_IsFormLoading)
                {
                    RefreshView();
                }
            }
        }

        private void _FillPostedCombo()
        {
            cmbPosted.Items.Clear();
            cmbPosted.Items.Add(Enumerations.GetEnumDescription(ERAPostedDuration.LastWeek).ToString());
            cmbPosted.Items.Add(Enumerations.GetEnumDescription(ERAPostedDuration.LastMonth).ToString());
            cmbPosted.Items.Add(Enumerations.GetEnumDescription(ERAPostedDuration.LastYear).ToString());
            cmbPosted.Items.Add(Enumerations.GetEnumDescription(ERAPostedDuration.All).ToString());
            cmbPosted.SelectedIndex = 0;
        }

        private void tsb_DetailPaperEOB_Click(object sender, EventArgs e)
        {
            try
            {
                _TempID = 0;
                if (tabERA.SelectedTab.Tag.ToString() == TAB_READY)
                {
                    if (c1ReadyToPost.RowSel >= 0)
                        _TempID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.RowSel, COL_BPRID));
                }
                else if (tabERA.SelectedTab.Tag.ToString() == TAB_POSTED)
                {
                    if (c1Posted.RowSel >= 0)
                        _TempID = Convert.ToInt64(c1Posted.GetData(c1Posted.RowSel, COL_BPRID));
                }
                else if (tabERA.SelectedTab.Tag.ToString() == TAB_DELETED)
                {
                    if (c1Deleted.RowSel >= 0)
                        _TempID = Convert.ToInt64(c1Deleted.GetData(c1Deleted.RowSel, COL_BPRID));
                }
                else if (tabERA.SelectedTab.Tag.ToString() == TAB_HOLD)
                {
                    if (c1Hold.RowSel >= 0)
                        _TempID = Convert.ToInt64(c1Hold.GetData(c1Hold.RowSel, COL_BPRID));
                }

                if (_TempID > 0)
                {
                    // ShowSSRSReport(enum_ReportType.EOB);

                    using (frmSSRS = new SSRSApplication.frmSSRSViewer())
                    {
                        SSFillERAData_EOB();
                        frmSSRS.parameterName = "practice,UserName,BPRID,UserID";
                        frmSSRS.ParameterValue = _practice + "," + _UserName + "," + _TempID + "," + _UserID;
                        frmSSRS.reportName = "rptERAEOB_V2";
                        frmSSRS.formIcon = null;
                        frmSSRS.reportTitle = "EOB Report";
                        frmSSRS.Conn = _DataBaseConnectionString;
                        frmSSRS.IsgloStreamReport = true;
                        frmSSRS.ShowDialog(this);
                        frmSSRS.Dispose();
                        frmSSRS = null;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool GetDetailsReportSetting(string forReport)
        {

            Object retVal = null;
            bool _isValidPath = false;

            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "SELECT sSettingsValue FROM Settings WITH(NOLOCK) WHERE UPPER(sSettingsName) ='" + forReport + "'";// 'Show Detail Paper EOB on ERA Posting'";
                    retVal = oDB.ExecuteScalar_Query(_TempStr);
                    CloseConnection();

                    if (retVal != null && retVal != DBNull.Value)
                    {
                        if (Convert.ToString(retVal) == "1")
                        {
                            _isValidPath = true;
                        }


                    }
                    else
                    { _isValidPath = false; }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                retVal = null;
            }
            return _isValidPath;
        }

        private void tsb_DetailExceptionReport_Click(object sender, EventArgs e)
        {
            bool _IsFullyPosted = true;
            _TempID = 0;
            if (tabERA.SelectedTab.Tag.ToString() == TAB_READY)
            {
                if (c1ReadyToPost.RowSel >= 0)
                    _TempID = Convert.ToInt64(c1ReadyToPost.GetData(c1ReadyToPost.RowSel, COL_BPRID));
            }
            else if (tabERA.SelectedTab.Tag.ToString() == TAB_POSTED)
            {
                if (c1Posted.RowSel >= 0)
                {
                    _TempID = Convert.ToInt64(c1Posted.GetData(c1Posted.RowSel, COL_BPRID));

                    if (c1Posted.GetData(c1Posted.RowSel, COL_IsFullyPosted) != null && c1Posted.GetData(c1Posted.RowSel, COL_IsFullyPosted).ToString() != "")
                        _IsFullyPosted = Convert.ToBoolean(c1Posted.GetData(c1Posted.RowSel, COL_IsFullyPosted));
                }
            }
            else if (tabERA.SelectedTab.Tag.ToString() == TAB_DELETED)
            {
                if (c1Deleted.RowSel >= 0)
                    _TempID = Convert.ToInt64(c1Deleted.GetData(c1Deleted.RowSel, COL_BPRID));
            }


            if (_TempID > 0)
            {
                if (_IsFullyPosted)
                {
                    MessageBox.Show("No Exceptions present.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                using (frmSSRS = new SSRSApplication.frmSSRSViewer())
                {
                    SSFillERAData_Exceptions();
                    frmSSRS.parameterName = "practice,ClaimProcessedCountComment,BalError,UserName,nBPRID,UserID";
                    frmSSRS.ParameterValue = _practice + "," + _ClaimProcessedCountComment + "," + _BalError + "," + _UserName + "," + _TempID + "," + _UserID;
                    frmSSRS.reportName = "rptERAExceptions_V2";
                    frmSSRS.formIcon = null;
                    frmSSRS.reportTitle = "Detail Exceptions Report";
                    frmSSRS.Conn = _DataBaseConnectionString;
                    frmSSRS.IsgloStreamReport = true;
                    frmSSRS.ShowDialog(this);
                    frmSSRS.Dispose();
                    frmSSRS = null;
                }

          
                //ShowSSRSReport(enum_ReportType.Exception);
            }



        }

     

    

        
    }
}
