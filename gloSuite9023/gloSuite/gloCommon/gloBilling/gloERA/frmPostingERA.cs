using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections;
using gloBilling.Payment;
using gloDateMaster;
using gloBilling.gloERA;

namespace gloBilling
{

    public partial class frmPostingERA : Form
    {

        #region " Declarations "

        //For Creating the Object of the CrystalReport
        gloERA.rptPostingERA objRptERAReport;
        //gloERA.rptPaperEOB objRptERAReport;


        private string _DataBaseConnectionString = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 1;
        private Int64 _UserID = 0;

        private Int64 _BPRID = 0;
        private Int64 _FileID = 0;
        private Int64 _SelectedTrayID = 0;
        private string _SelectedTrayName = "";

        private bool _bIsSaved = false;
        private gloERA.gloERA oERA;

        Int64 _TempID;
        string strFileName;

        #endregion

        #region " Public Properties "
        public Int64 SelectedPaymentTrayID
        {
            get { return _SelectedTrayID; }
            set
            {
                _SelectedTrayID = value;
                lblPaymentTray.Tag = _SelectedTrayID;

            }
        }
        public string SelectedPaymentTray
        {
            get { return _SelectedTrayName; }
            set
            {
                _SelectedTrayName = value;
                lblPaymentTray.Text = _SelectedTrayName;
            }
        }
        #endregion

        #region " Constructor "

        public frmPostingERA(Int64 nBPRID, Int64 nERAFileID, Int64 nUserID)
        {

            #region " Get DatabaseConnectionString from AppSettings "
            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                { _DataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]); }
            }
            #endregion

            #region " Get MessageBoxCaption from AppSettings "
            if (appSettings["MessageBoxCaption"] != null)
            {
                if (appSettings["MessageBoxCaption"] != "")
                { _MessageBoxCaption = Convert.ToString(appSettings["MessageBoxCaption"]); }
                else
                    _MessageBoxCaption = "gloPM";
            }
            else
                _MessageBoxCaption = "gloPM";
            #endregion

            #region " Get ClinicID from AppSettings "
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            #endregion

            InitializeComponent();

            _BPRID = nBPRID;
            _FileID = nERAFileID;
            _UserID = nUserID;

        }

        #endregion

        #region " Form Events "

        private void frmPostingERA_Load(object sender, EventArgs e)
        {
            try
            {
                //For Addding the ReportViewer User Control in form
                pnlContainer.Controls.Add(crvReportViewer);
                crvReportViewer.Dock = DockStyle.Fill;
                //Property to show the Export Button on Tool Bar
                crvReportViewer.ShowExportButton = false;

                FillERAData();

                LoadClosePaymentTray();

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void frmPostingERA_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!_bIsSaved)
                {
                    gloERA.ClsERAValidation.UpdateCheckStatus(_BPRID, gloERA.enumCheckStatus.Ready.GetHashCode());
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region " Tool Strip Events "

        //private void tsb_btnExportReport_Click(object sender, EventArgs e)
        //{
        //    crvReportViewer.ExportReport();
        //}

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            gloERA.ClsERAValidation.UpdateCheckStatus(_BPRID, gloERA.enumCheckStatus.Ready.GetHashCode());
            this.Close();
        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {

            try
            {
                crvReportViewer.PrintReport();
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
        }

        #endregion

        #region " User Control Events "

        //Event For Generate Report on Click
        private void ogloReports_onGenerateReport_Clicked(object sender, EventArgs e)
        {
            // FillAdjudicationHistory();
        }


        //Event For Closing the Form
        private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region " Private Methods "

        private void FillERAData()
        {
            //Creating the object of the Report
            if (!(object.ReferenceEquals(objRptERAReport, null)))
            {
                objRptERAReport.Close();
                objRptERAReport.Dispose();
                objRptERAReport = null;
            }

            objRptERAReport = new gloERA.rptPostingERA();
            gloERA.drRptERA dsERAReport = new gloERA.drRptERA();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {

                Cursor = Cursors.WaitCursor;
                oConnection.ConnectionString = _DataBaseConnectionString;

                oConnection.Open();
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "ERA_PostingReport";
                _sqlcommand.CommandTimeout = 5000;
                _sqlcommand.Parameters.Add("@nBPRID", SqlDbType.BigInt).Value = _BPRID;//
                _sqlcommand.Parameters.Add("@UserID", SqlDbType.BigInt).Value = _UserID;

                da.SelectCommand = _sqlcommand;
                dsERAReport.EnforceConstraints = false;
                da.Fill(dsERAReport, "dt_PostingReport");


                objRptERAReport.SetDataSource(dsERAReport);

                //Binds the Report to the Report viewer
                crvReportViewer.ReportSource = objRptERAReport;
                strFileName = 'P' + dsERAReport.Tables[0].Rows[0]["PayerID"].ToString().Trim() + 'C' + dsERAReport.Tables[0].Rows[0]["CheckNo"].ToString() + 'D' + dsERAReport.Tables[0].Rows[0]["CheckDate"].ToString().Replace("/", "");
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
                    _sqlcommand.Dispose();
                }
                Cursor = Cursors.Default;
            }
        }

        private void SelectPaymentTray()
        {
            frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(_DataBaseConnectionString);

            try
            {
                //...Show pop-up to select the Tray
                ofrmBillingTraySelection.WindowState = FormWindowState.Normal;
                ofrmBillingTraySelection.StartPosition = FormStartPosition.CenterParent;
                ofrmBillingTraySelection.IsChargeTray = false;
                ofrmBillingTraySelection.ShowDialog();

                // toolTip1.SetToolTip(lbBillingtray, null);

                if (ofrmBillingTraySelection.FormResult == DialogResult.OK)
                {
                    if (ofrmBillingTraySelection.SelectedTrayID > 0)
                    {
                        this.SelectedPaymentTray = ofrmBillingTraySelection.SelectedTrayName;
                        this.SelectedPaymentTrayID = ofrmBillingTraySelection.SelectedTrayID;

                        //  toolTip1.SetToolTip(lbBillingtray, ofrmBillingTraySelection.SelectedTrayName);
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

        private bool IsValidCloseDate()
        {
            if (mskCloseDate.MaskCompleted == false)
            {
                MessageBox.Show("Please enter the close date", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskCloseDate.Focus();
                mskCloseDate.Select();
                return false;
            }

            if (gloDate.IsValid(mskCloseDate.Text) == false)
            {
                CancelEventArgs e = new CancelEventArgs();
                ValidateDate(mskCloseDate, e);
                if (e.Cancel)
                { return false; }
            }
            else
            {
                #region " check for day closed "

                gloBilling ogloBilling = new gloBilling(_DataBaseConnectionString, string.Empty);
                if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                {
                    MessageBox.Show("Selected closed date is being closed.Please select open date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select(); mskCloseDate.Focus();
                    return false;
                }
                ogloBilling.Dispose();

                #endregion " check for day closed  "

                if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(7))
                {
                    MessageBox.Show("Close date " + mskCloseDate.Text.Trim() + " is too far in the future. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning); mskCloseDate.Focus();
                    mskCloseDate.Select();
                    return false;
                }
                else
                {
                    if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date)
                    {
                        DialogResult _dlgCloseDate = DialogResult.None;
                        _dlgCloseDate = MessageBox.Show("Close date " + mskCloseDate.Text.Trim() + " is in future.Are you sure you want to continue with save ?", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (_dlgCloseDate == DialogResult.Cancel)
                        {
                            mskCloseDate.Focus();
                            mskCloseDate.Select();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool IsValidPaymentTray()
        {
            if (SelectedPaymentTrayID == 0)
            {
                MessageBox.Show("Please select the payment tray.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnTraySelection.Select();
                btnTraySelection.Focus();
                return false;
            }
            else if (InsurancePayment.IsPaymentTrayActive(SelectedPaymentTrayID) == false)
            {
                MessageBox.Show("The payment tray selected is inactive. Please select another tray", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnTraySelection.Select();
                btnTraySelection.Focus();
                return false;
            }
            return true;
        }

        private void LoadClosePaymentTray()
        {
            if (!IsCloseDaySet)
            { SetCloseDate(); }

            if (SelectedPaymentTrayID == 0)
            { FillPaymentTray(); }
        }

        private bool IsCloseDaySet
        {
            get { return mskCloseDate.MaskCompleted; }
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
                mskCloseDate.Text = _lastCloseDate;
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

        private void btnTraySelection_Click(object sender, EventArgs e)
        {
            SelectPaymentTray();
        }

        private void ValidateDate(object sender, CancelEventArgs e)
        {
            MaskedTextBox mskDate = (MaskedTextBox)sender;
            mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string strDate = mskDate.Text;
            mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

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
                }
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
                gloERA.ClsERAValidation.UpdateCheckStatus(_BPRID, gloERA.enumCheckStatus.MarkedDeleted.GetHashCode());
                _bIsSaved = true;
                this.Close();
            }
        }

        #endregion

        #region " Toolstrip Button Events "
        private void tsb_Accept_Click(object sender, EventArgs e)
        {
            gloBilling ogloBilling = new gloBilling(_DataBaseConnectionString, "");

            try
            {
                DialogResult _dlgRst = DialogResult.None;
                //_dlgRst = MessageBox.Show("Please review the ERA Posting Report before accepting the post. Continue posting the ERA Check?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                _dlgRst = MessageBox.Show("Accept ERA Postings?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dlgRst == DialogResult.Yes)
                {
                    //Int64 _retValue = 0;
                    string sMsg = string.Empty;


                    if (ClsERAValidation.ValidateReasonCodePayer(_BPRID, out sMsg))
                    {
                        object[] objPlaceHolders = { "\n\n", "\n" };

                        MessageBox.Show(string.Format(sMsg, objPlaceHolders), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        return ;
                    }
                   //_retValue = ClsERAValidation.ValidateCheck(_BPRID, out sMsg);

                   // if (_retValue == 1)
                   // {
                   //     NoClaimProcessed(sMsg);
                   // }
                   // else
                   // {
                        if (IsValidCloseDate() && IsValidPaymentTray())
                        {

                            gloERA.clsERAPosting oERAPosting = null;
                            oERAPosting = new gloERA.clsERAPosting();
                            string sMessage = string.Empty;

                            StopFlag oStopFlag = StopFlag.NotProcessed;

                            #region "Duplicate Check"


                            DataTable dtTempCheckDetails = null;

                            dtTempCheckDetails = oERAPosting.GetTemporaryPostedCheckDetails(_BPRID);
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

                                    if (ClsERAValidation.IsCheckDuplicate(_BPRID, sCheckNumber, dCheckAmount, nSelectedInsuranceCompanyID))  // Returns nContactID/PayerID //1044623:HardCode
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
                                this.Close();
                                return;
                            }

                            Cursor = Cursors.WaitCursor;
                            if (true)//(!oERAPosting.PostERAFile_New(_BPRID, _SelectedTrayID, mskCloseDate.Text, out sMessage, out oStopFlag, null,null))
                            {
                                Cursor = Cursors.Default;
                                if (sMessage != "")
                                    MessageBox.Show(sMessage, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                gloERA.ClsERAValidation.UpdateCheckStatus(_BPRID, gloERA.enumCheckStatus.Ready.GetHashCode());
                                this.Close();
                                return;
                            }
                            else
                            {
                                #region " ACTUAL POSTING HERE "
                                _bIsSaved = true;
                                saveReport();
                                gloERA.ClsERAValidation.UpdateCheckStatus(_BPRID, gloERA.enumCheckStatus.Posted.GetHashCode());
                                ogloBilling.SaveUserWiseCloseDay(mskCloseDate.Text.Trim(), CloseDayType.Payment, _ClinicID);
                                this.Close();
                                #endregion
                            }
                        }

                    }
                //}
                //else
                //{

                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                Cursor = Cursors.Default;
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }

        }

        
        private void saveReport()
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            try
            {
                Stream st = objRptERAReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.RichText);
                byte[] buffer = new byte[st.Length];
                st.Read(buffer, 0, (int)st.Length);
                st.Close();
                oConnection.ConnectionString = _DataBaseConnectionString;
                oConnection.Open();
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.Parameters.Clear();
                _sqlcommand.CommandText = "ERA_InUpReport";
                _sqlcommand.Parameters.Add("@nFileID", SqlDbType.BigInt).Value = _FileID;//
                _sqlcommand.Parameters.Add("@nBPRID", SqlDbType.BigInt).Value = _BPRID;
                _sqlcommand.Parameters.Add("@sReportName", SqlDbType.VarChar).Value = setFileName(strFileName);//
                _sqlcommand.Parameters.Add("@iBinaryFiles", SqlDbType.Image).Value = buffer;
                _sqlcommand.Parameters.Add("@nReportType", SqlDbType.Int).Value = enumReportType.PostingReport;//
                _sqlcommand.Parameters.Add("@nUserID", SqlDbType.BigInt).Value = _UserID;
                _sqlcommand.Parameters.Add("@nClinicID", SqlDbType.BigInt).Value = _ClinicID;//
                
                _sqlcommand.ExecuteNonQuery();
                oConnection.Close();
            }
            
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
                finally
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.Dispose();
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
        
        private void tsb_Reject_Click(object sender, EventArgs e)
        {
            try
            {
                gloERA.ClsERAValidation.UpdateCheckStatus(_BPRID, gloERA.enumCheckStatus.Ready.GetHashCode());
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmERAExceptions oRpt_EOB = new frmERAExceptions(_TempID, 0, _UserID); // ONLY FOR SELECTED CHECK //
            oRpt_EOB.WindowState = FormWindowState.Maximized;
            oRpt_EOB.ShowInTaskbar = false;
            oRpt_EOB.StartPosition = FormStartPosition.CenterParent;
            oRpt_EOB.ShowDialog();
            oRpt_EOB.Dispose();
            oRpt_EOB = null;
        }

        private void tsb_btnExportReport_Click(object sender, EventArgs e)
        {
            crvReportViewer.ExportReport();
        }

        #endregion

    }
}