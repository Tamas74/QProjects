using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;


namespace gloBilling
{
    public partial class frmClaimChargeHistoryV2 : Form
    {

        #region "Variable declaration"

        private string _sqlDatabaseConnectionString = "";
        string _strMessageBoxCaption = string.Empty;
        private Int64 _nPatientID = 0;
        private Int64 _nClinicID = 1;
        private Int64 _nTransactionID = 0;
        private const int COL_TOOLTIP_COL_RANGE_FROM = 9;
        private const int COL_HISTORY = 8;
        private const int COL_TOOLTIP_COL_RANGE_TO = 18;
        private const int COL_TYPE = 15;
        private const int COL_NOTETYPE = 7;
        private const int COL_NOTE_DATA = 10;
        private const int COL_MST_TRAN_ID = 0;
        private const int COL_MST_TRANDETAIL_ID = 1;
        private const int COL_BISVOID = 28;
        private const string InsType = "1I1";
        private const string InsTypeVoid = "1I2";
        private const string PatPmntType = "2P1";
        private const string PatAdjsType = "2P2";
        private const string PatPmntVoidType = "2P3";
        private const string PatPmntCorrType = "2P4";
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private bool _isInvokedFromInsurancePayment = false;
        private bool _isInvokedFromModifyCharge = false;
        #endregion

        #region Constructor

        public frmClaimChargeHistoryV2(string databaseconnectionstring, Int64 patientid, Int64 clinicid, Int64 nTransactionID, bool IsInsurancePaymentCall = false,bool IsModifyChargeCall=false)
        {
            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _strMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _strMessageBoxCaption = "gloPM"; ;
                }
            }
            else
            { _strMessageBoxCaption = "gloPM"; ; }

            #endregion

            _sqlDatabaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _nPatientID = patientid;
            _nClinicID = clinicid;
            _nTransactionID = nTransactionID;
            _isInvokedFromInsurancePayment = IsInsurancePaymentCall;
            _isInvokedFromModifyCharge = IsModifyChargeCall;
        }

        #endregion

        public string CallingContainer { get; set; }

        # region Form Events

        private void frmClaimChargeHistoryV2_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1ClaimCharges, false);
            gloC1FlexStyle.Style(c1ClaimChargeHistory, false);
            c1ClaimCharges.ExtendLastCol = true;
            c1ClaimCharges.AllowDragging =AllowDraggingEnum.None;
            c1ClaimChargeHistory.AllowDragging = AllowDraggingEnum.None;
            c1ClaimChargeHistory.ExtendLastCol = true;
            FillClaimChargeHistory();
            if (_isInvokedFromInsurancePayment == true)
            {
                tls_btnViewPmnt.Enabled = false;
                tsb_Modify.Enabled = false;
            }
            if (_isInvokedFromModifyCharge == true)
            {
                tsb_Modify.Enabled = false;
            }
            SetClaimChargeHistorySorting();
        }

        # endregion

        #region "Functions"

        public void FillClaimChargeHistory()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            DataSet dsClaimChargesummary = null;
            //DataSet dsClaimChargeHistory = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oDBPatameters.Add("@nTransactionID", _nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_Patient_Financial_View_Claim_Charge_History_Summary_V2", oDBPatameters, out dsClaimChargesummary);
                if ((dsClaimChargesummary != null) && (dsClaimChargesummary.Tables[0].Rows.Count > 0))
                {
                    dsClaimChargesummary.Tables[0].TableName = "Claim_Charge_Summary";
                    c1ClaimCharges.DataMember = "Claim_Charge_Summary";
                    this.c1ClaimCharges.RowColChange -= new System.EventHandler(this.c1ClaimCharges_RowColChange);
                    c1ClaimCharges.DataSource = dsClaimChargesummary;
                    this.c1ClaimCharges.RowColChange += new System.EventHandler(this.c1ClaimCharges_RowColChange);
                    if (c1ClaimCharges.Rows.Count > 1)
                    {
                     for (int rowCntr = 1; rowCntr <= c1ClaimCharges.Rows.Count - 1; rowCntr++)
                        {
                        if (c1ClaimCharges.GetData(c1ClaimCharges.Rows[rowCntr].Index, c1ClaimCharges.Cols["lineflag"].Index).ToString().Trim() == "L")
                            c1ClaimCharges.SetData(rowCntr, "ClaimNumber", "");
                        }  
                        FillClaimChargeHistoryDetail(Convert.ToInt64(c1ClaimCharges.GetData(c1ClaimCharges.RowSel, c1ClaimCharges.Cols["nTransactionMSTID"].Index)), Convert.ToInt64((c1ClaimCharges.GetData(c1ClaimCharges.RowSel, c1ClaimCharges.Cols["lineflag"].Index).ToString().Trim() == "M" ? 0 : c1ClaimCharges.GetData(c1ClaimCharges.RowSel, c1ClaimCharges.Cols["nTransactionDetailMSTID"].Index))));
                        tls_btnViewRemit.Enabled = false;
                        tls_btnViewPmnt.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB.Disconnect(); }
            }
        }


        private void FillClaimChargeHistoryDetail(Int64 mst_TransactionID, Int64 mst_TransactionDetailID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            DataSet dsClaimChargeHistory = null;
           // Boolean typeFlag = false;
            List<Int64> oContacts = new List<Int64>();

            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                if (c1ClaimCharges.Rows.Count > 1)
                {
                    oDBPatameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPatameters.Add("@nTransactionMSTID", mst_TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    if (c1ClaimCharges.GetData(c1ClaimCharges.RowSel, c1ClaimCharges.Cols["lineflag"].Index).ToString().Trim() == "M")
                    {
                        oDB.Retrive("PA_Patient_Financial_View_Claim_History_V2", oDBPatameters, out dsClaimChargeHistory);
                    }
                    else
                    {
                        oDBPatameters.Add("@nTransactionMSTDetailID", mst_TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Retrive("PA_Patient_Financial_View_Charge_History_V2", oDBPatameters, out dsClaimChargeHistory);
                    }
                    if ((dsClaimChargeHistory != null) && (dsClaimChargeHistory.Tables[0].Rows.Count > 0))
                    {
                        int chkCntr = 0;
                        for (int i = 0; i <= dsClaimChargeHistory.Tables[0].Rows.Count - 1; i++)
                        {
                            if (Convert.ToString(dsClaimChargeHistory.Tables[0].Rows[i]["sType"]).ToUpper() == "Post".ToUpper())
                            {
                                if (chkCntr == 0)
                                {
                                    dsClaimChargeHistory.Tables[0].Rows[i]["Balance"] = Convert.ToDecimal(dsClaimChargeHistory.Tables[0].Rows[i]["Balance"] == DBNull.Value ? 0 : dsClaimChargeHistory.Tables[0].Rows[i]["Balance"]);
                                }
                                chkCntr++;
                            }
                            else
                            {
                                if (chkCntr != 0)
                                {
                                    if (Convert.ToString(dsClaimChargeHistory.Tables[0].Rows[i]["sHiddenTypeAfterdtCreateDate"]).Trim() == "3C1")
                                    {
                                        dsClaimChargeHistory.Tables[0].Rows[i]["Balance"] = 0;
                                    }
                                    else
                                    {
                                        dsClaimChargeHistory.Tables[0].Rows[i]["Balance"] = Convert.ToDecimal(dsClaimChargeHistory.Tables[0].Rows[i - 1]["Balance"] == DBNull.Value ? 0 : dsClaimChargeHistory.Tables[0].Rows[i - 1]["Balance"]) - (Convert.ToDecimal(dsClaimChargeHistory.Tables[0].Rows[i]["Paid"] == DBNull.Value ? 0 : dsClaimChargeHistory.Tables[0].Rows[i]["Paid"]) + Convert.ToDecimal(dsClaimChargeHistory.Tables[0].Rows[i]["Adjustment"] == DBNull.Value ? 0 : dsClaimChargeHistory.Tables[0].Rows[i]["Adjustment"]));
                                    }
                                }
                            }
                        }
                        DataView dv = dsClaimChargeHistory.Tables[0].DefaultView;
                        DataTable dtUniqueData = dv.ToTable(true, "ClaimNo");
                        DataTable dtFilterData, dtFinalData;
                        dtFilterData = dsClaimChargeHistory.Tables[0].Clone();
                        dtFinalData = dsClaimChargeHistory.Tables[0].Clone();
                        for (int cntr = 0; cntr <= dtUniqueData.Rows.Count - 1; cntr++)
                        {
                            DataRow[] resultRows = null;
                            resultRows = dsClaimChargeHistory.Tables[0].Select("ClaimNo='" + dtUniqueData.Rows[cntr]["ClaimNo"] + "'");
                            if (resultRows.Length > 0)
                            {
                                foreach (DataRow dr in resultRows)
                                {
                                    dtFilterData.ImportRow(dr);
                                }
                            }
                            if (cntr != dtUniqueData.Rows.Count - 1)
                            {
                                dtFilterData.Rows.Add();
                            }
                        }
                        int counter = -1;
                        if (dtFilterData.Rows.Count > 0)
                        {
                            for (int cntr = 0; cntr <= dtFilterData.Rows.Count - 1; cntr++)
                            {                               
                                if (dtFilterData.Rows[cntr]["Description"].ToString() != string.Empty)
                                {
                                    if (dtFilterData.Rows[cntr]["Description"].ToString().Contains("\\n"))
                                    {
                                        dtFinalData.ImportRow(dtFilterData.Rows[cntr]);
                                        if (dtFilterData.Rows[cntr]["bIsCorrection"] != DBNull.Value && Convert.ToBoolean(dtFilterData.Rows[cntr]["bIsCorrection"]))
                                        {
                                            counter = counter + 1;
                                            dtFinalData.Rows[counter]["Description"] = Convert.ToString(dtFilterData.Rows[counter]["Description"]).Replace("\\\\n", Environment.NewLine).Trim(); ;
                                            dtFinalData.Rows[counter]["nEOBPaymentID"] = dtFilterData.Rows[cntr]["nEOBPaymentID"].ToString();
                                            dtFinalData.Rows[counter]["nVoidRefEOBPaymentID"] = (dtFilterData.Rows[cntr]["nVoidRefEOBPaymentID"] == DBNull.Value ? 0 : dtFilterData.Rows[cntr]["nVoidRefEOBPaymentID"]);
                                            dtFinalData.Rows[counter]["nEOBID"] = dtFilterData.Rows[cntr]["nEOBID"].ToString();
                                            dtFinalData.Rows[counter]["sHiddenTypeAfterdtCreateDate"] = dtFilterData.Rows[cntr]["sHiddenTypeAfterdtCreateDate"].ToString();
                                            dtFinalData.Rows[counter]["nTransactionID"] = dtFilterData.Rows[cntr]["nTransactionID"].ToString();
                                        }
                                        else
                                        {
                                            counter = counter + 1;
                                            string[] sDescriptions = Convert.ToString(dtFinalData.Rows[counter]["Description"]).Split(new String[] { "\\\\n" }, StringSplitOptions.RemoveEmptyEntries);
                                            for (int iArrCount = 0; iArrCount <= sDescriptions.Length - 1; iArrCount++)
                                            {
                                                if (iArrCount == 0)
                                                {
                                                    dtFinalData.Rows[counter]["Description"] = sDescriptions[iArrCount];
                                                }
                                                else
                                                {
                                                    if (iArrCount != 1)
                                                    {
                                                        dtFinalData.Rows[counter]["Description"] =  dtFinalData.Rows[counter]["Description"] + "\\\\n" +  sDescriptions[iArrCount].Replace("Total", "");
                                                    }
                                                }
                                            }
                                            dtFinalData.Rows[counter]["Description"] = Convert.ToString(dtFinalData.Rows[counter]["Description"]).Replace("\\\\n", Environment.NewLine).Trim();
                                            dtFinalData.Rows[counter]["nEOBPaymentID"] = dtFilterData.Rows[cntr]["nEOBPaymentID"].ToString();
                                            dtFinalData.Rows[counter]["nVoidRefEOBPaymentID"] = (dtFilterData.Rows[cntr]["nVoidRefEOBPaymentID"] == DBNull.Value ? 0 : dtFilterData.Rows[cntr]["nVoidRefEOBPaymentID"]);
                                            dtFinalData.Rows[counter]["nEOBID"] = dtFilterData.Rows[cntr]["nEOBID"].ToString();
                                            dtFinalData.Rows[counter]["sHiddenTypeAfterdtCreateDate"] = dtFilterData.Rows[cntr]["sHiddenTypeAfterdtCreateDate"].ToString();
                                            dtFinalData.Rows[counter]["nTransactionID"] = dtFilterData.Rows[cntr]["nTransactionID"].ToString();
                                        }

                                    }
                                    else if (dtFilterData.Rows[cntr]["Description"].ToString().Contains("~"))
                                    {
                                        //dtFinalData.Rows.Add();
                                        dtFilterData.Rows[cntr]["Description"] = Convert.ToString(dtFilterData.Rows[cntr]["Description"]).Replace("~", Environment.NewLine).Trim();
                                        dtFilterData.Rows[cntr]["nTransactionMSTID"] = dtFilterData.Rows[cntr]["nTransactionMSTID"].ToString();
                                        dtFinalData.ImportRow(dtFilterData.Rows[cntr]);
                                        counter = counter + 1;
                                    }
                                    else
                                    {
                                        dtFinalData.ImportRow(dtFilterData.Rows[cntr]);
                                        counter = counter + 1;
                                    }
                                }
                                else
                                {
                                    dtFinalData.ImportRow(dtFilterData.Rows[cntr]);
                                    counter = counter + 1;
                                }
                            }
                            dtFinalData.TableName = "Claim_Charge_History";
                            dsClaimChargeHistory.Tables.Clear();
                            dsClaimChargeHistory.Tables.Add(dtFinalData);
                            if (dsClaimChargeHistory.Tables["Claim_Charge_History"].Rows.Count > 0)
                            {
                                    c1ClaimChargeHistory.DataMember = "Claim_Charge_History";
                                    c1ClaimChargeHistory.DataSource = dsClaimChargeHistory;
                                    for (int iRow = 1; iRow <= c1ClaimChargeHistory.Rows.Count - 1; iRow++)
                                    {
                                        c1ClaimChargeHistory.AutoSizeRow(iRow);
                                    }
                                    c1ClaimChargeHistory.Cols["dtDate"].Format = "MM/dd/yyyy";
                                    for (int i = 1; i <= c1ClaimChargeHistory.Rows.Count - 1; i++)
                                    {
                                        if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.Rows[i].Index, c1ClaimChargeHistory.Cols["Description"].Index).ToString() == "")
                                        {
                                            c1ClaimChargeHistory.SetData(i, COL_NOTETYPE, "Split");
                                            if (i != c1ClaimChargeHistory.Rows.Count - 1)
                                                c1ClaimChargeHistory.SetData(i, COL_HISTORY, "New Claim: " + c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.Rows[i + 1].Index, c1ClaimChargeHistory.Cols["ClaimNo"].Index).ToString());
                                            else
                                                c1ClaimChargeHistory.SetData(i, COL_HISTORY, "New Claim: " + c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.Rows[i].Index, c1ClaimChargeHistory.Cols["ClaimNo"].Index).ToString());
                                        }
                                        else
                                        {
                                            if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.Rows[i].Index, c1ClaimChargeHistory.Cols["sType"].Index).ToString() == "Resp")
                                            {
                                                c1ClaimChargeHistory.SetData(i, "Balance", "");
                                            }
                                        }
                                        if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.Rows[i].Index, c1ClaimChargeHistory.Cols["sType"].Index).ToString() == "Bill"
                                            || c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.Rows[i].Index, c1ClaimChargeHistory.Cols["sType"].Index).ToString() == "Chr Note"
                                            || c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.Rows[i].Index, c1ClaimChargeHistory.Cols["sType"].Index).ToString() == "Clm Note"
                                            || c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.Rows[i].Index, c1ClaimChargeHistory.Cols["sType"].Index).ToString() == "Claim FU")
                                        {
                                            c1ClaimChargeHistory.SetData(i, "Balance", "");
                                        }
                                    }
                                }
                        }

                        if (c1ClaimCharges.GetData(c1ClaimCharges.RowSel, c1ClaimCharges.Cols["lineflag"].Index).ToString().Trim() == "M")
                            lblClmCharge.Text = "Claim History";
                        else
                            lblClmCharge.Text = "Charge History";
                    }
                }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); 
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Dispose();
            }
        }

        #endregion

        #region Events

        private void tls_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tls_btnViewRemit_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 nEOBPaymentID = 0;
                Int64 nEOBID = 0;
                Int64 nTransactionID = 0;
                Int64 nContactID = 0;

                Int64 nVoidRefEOBPaymentID = 0;
                bool nIsVoidEOBPayment = false;
           
                if (c1ClaimChargeHistory.Rows.Count > 1)
                {
                    if ((c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == InsType) || (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == InsTypeVoid))
                        {
                            if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index) != null && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index)).Trim() != "")
                            {
                                if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["sHiddenTypeAfterdtCreateDate"].Index) != null
                                   && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["sHiddenTypeAfterdtCreateDate"].Index)).Trim() != ""
                                   && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["sHiddenTypeAfterdtCreateDate"].Index)).Trim().ToUpper() == InsTypeVoid.ToUpper())
                                {
                                    //If Payment is voided then for voided eob the nVoidRefEOBPaymentID will have 
                                    //the nEOBPaymentID for the main entry
                                    nIsVoidEOBPayment = true;
                                    nVoidRefEOBPaymentID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nVoidRefEOBPaymentID"].Index));
                                }
                                
                                nEOBPaymentID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index));
                                nEOBID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBID"].Index));
                                nTransactionID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nTransactionMSTID"].Index));
                                nContactID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nContactID"].Index));
                                gloAccountsV2.frmViewClaimRemittanceV2 ofrmClaimChargeHistory = new gloAccountsV2.frmViewClaimRemittanceV2(_sqlDatabaseConnectionString, _nPatientID, _nClinicID, nEOBPaymentID, nEOBID,_isInvokedFromInsurancePayment);
                                ofrmClaimChargeHistory.StartPosition = FormStartPosition.CenterScreen;
                                ofrmClaimChargeHistory.IsVoidEOBPayment = nIsVoidEOBPayment;
                                ofrmClaimChargeHistory.VoidRefEOBPaymentID = nVoidRefEOBPaymentID;
                                ofrmClaimChargeHistory.CallingContainer = this.Name;
                                ofrmClaimChargeHistory.TransactionID = nTransactionID;
                                ofrmClaimChargeHistory.ContactID = nContactID;
                                ofrmClaimChargeHistory.ShowDialog(this);
                                ofrmClaimChargeHistory.Dispose();
                                FillClaimChargeHistory();
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); 
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void c1ClaimCharges_Click(object sender, EventArgs e)
        {
            try
            { 
               
                if (c1ClaimCharges.Rows.Count >1)
                {
                    Int64 nMstTranID = 0;
                    Int64 nMstTranDetailID = 0;
                    nMstTranID = Convert.ToInt64(c1ClaimCharges.GetData(c1ClaimCharges.RowSel, COL_MST_TRAN_ID));
                    nMstTranDetailID = Convert.ToInt64((c1ClaimCharges.GetData(c1ClaimCharges.RowSel, COL_MST_TRANDETAIL_ID) == DBNull.Value ? 0 : c1ClaimCharges.GetData(c1ClaimCharges.RowSel, COL_MST_TRANDETAIL_ID)));
                    if (nMstTranID > 0)
                    {
                        if (c1ClaimCharges.GetData(c1ClaimCharges.RowSel, c1ClaimCharges.Cols["lineflag"].Index).ToString().Trim() == "M")
                            lblClmCharge.Text = "Claim History";
                        else
                            lblClmCharge.Text = "Charge History";

                            FillClaimChargeHistoryDetail(nMstTranID, nMstTranDetailID);
                    }

                }
               tls_btnViewRemit.Enabled=false;
               tls_btnViewPmnt.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void c1ClaimCharges_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (c1ClaimCharges.HitTest(e.X, e.Y).Column >= COL_TOOLTIP_COL_RANGE_FROM && c1ClaimCharges.HitTest(e.X, e.Y).Column <= COL_TOOLTIP_COL_RANGE_TO)
                {
                    gloC1FlexStyle.ShowToolTip(C1SuperTooltipDx, (C1FlexGrid)sender, e.Location, true);
                }
                else
                {
                    gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltipDx, (C1FlexGrid)sender, e.Location);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void c1ClaimChargeHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            
            try
            {
              
                    Int64 nEOBPaymentID = 0;
                    Int64 nEOBID = 0;
                    Int64 nTransactionID = 0;
                    Int64 nContactID = 0;
                    Int64 nVoidRefEOBPaymentID = 0;
                    bool nIsVoidEOBPayment = false;
                    HitTestInfo hitInfo = this.c1ClaimChargeHistory.HitTest(e.X, e.Y);
                    if (c1ClaimChargeHistory.Rows.Count > 1)
                    {
                        if (hitInfo.Row != 0)
                        {
                            if ((c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == InsType) || (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == InsTypeVoid))
                            {
                                if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index) != null && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index)).Trim() != "")
                                {
                                    if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["sHiddenTypeAfterdtCreateDate"].Index) != null
                                      && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["sHiddenTypeAfterdtCreateDate"].Index)).Trim() != ""
                                      && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["sHiddenTypeAfterdtCreateDate"].Index)).Trim().ToUpper() == InsTypeVoid.ToUpper())
                                    {
                                        //If Payment is voided then for voided eob the nVoidRefEOBPaymentID will have 
                                        //the nEOBPaymentID for the main entry
                                        nIsVoidEOBPayment = true;
                                        nVoidRefEOBPaymentID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nVoidRefEOBPaymentID"].Index));
                                    }
                                    nEOBPaymentID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index));
                                    nEOBID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBID"].Index));
                                    nTransactionID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nTransactionMSTID"].Index));
                                    nContactID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nContactID"].Index));
                                    gloAccountsV2.frmViewClaimRemittanceV2 ofrmClaimChargeHistory = new gloAccountsV2.frmViewClaimRemittanceV2(_sqlDatabaseConnectionString, _nPatientID, _nClinicID, nEOBPaymentID, nEOBID,_isInvokedFromInsurancePayment);
                                    ofrmClaimChargeHistory.StartPosition = FormStartPosition.CenterScreen;
                                    ofrmClaimChargeHistory.IsVoidEOBPayment = nIsVoidEOBPayment;
                                    ofrmClaimChargeHistory.VoidRefEOBPaymentID = nVoidRefEOBPaymentID;
                                    ofrmClaimChargeHistory.TransactionID = nTransactionID;
                                    ofrmClaimChargeHistory.ContactID = nContactID;
                                    ofrmClaimChargeHistory.ShowDialog(this);
                                    ofrmClaimChargeHistory.Dispose();
                                    FillClaimChargeHistory();
                                }
                            }
                            else if ((c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == PatPmntType) || (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == PatPmntVoidType))
                            {
                                Int64 eobPaymentId = 0;
                                Int64 nMainEOBPaymentID = 0;
                                object _mEOBPaymentID = null;
                                if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index) != null && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index)) != "")
                                {
                                    eobPaymentId = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index));
                                    //nMainEOBPaymentID = Convert.ToInt64(oDB.ExecuteScalar_Query("select distinct nRefEOBPaymentID from BL_EOBPayment_Dtl WITH (NOLOCK) where nEOBPaymentID=" + eobPaymentId + " and  nRefEOBPaymentID<>0"));

                                    //Bug #58674: gloPM- Patient Account - View Void payment - Exception on double click
                                    //Comment the line and check for null and "" if true then pass eobPaymentId to nMainEOBPaymentID as done for tls_btnViewPmnt_Click
                                    //nMainEOBPaymentID = Convert.ToInt64(oDB.ExecuteScalar_Query("select distinct nCredit_RefID from Debits WITH (NOLOCK) where nCreditID=" + eobPaymentId + " and  nCredit_RefID <>0"));
                                    _mEOBPaymentID = getEobPaymentID(eobPaymentId);
                                    if (_mEOBPaymentID == null || Convert.ToString(_mEOBPaymentID).Trim() == "")
                                    {
                                        _mEOBPaymentID = eobPaymentId;
                                    }
                                    nMainEOBPaymentID = Convert.ToInt64(_mEOBPaymentID);

                                    gloAccountsV2.frmViewPatientPaymentV2 ofrmViewPatientPayment = new gloAccountsV2.frmViewPatientPaymentV2(_sqlDatabaseConnectionString, _nPatientID, _nClinicID, nMainEOBPaymentID);
                                    ofrmViewPatientPayment.StartPosition = FormStartPosition.CenterScreen;
                                    ofrmViewPatientPayment.ShowDialog(this);
                                    ofrmViewPatientPayment.Dispose();
                                    FillClaimChargeHistory();
                                }
                            }

                        }

                    }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }

        private void c1ClaimChargeHistory_Click(object sender, EventArgs e)
        {
            try
            {
              
                if (c1ClaimChargeHistory.Rows.Count > 1 && _isInvokedFromInsurancePayment == false)
                {
                    if (c1ClaimChargeHistory.RowSel != 0)
                    {
                        if ((c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == InsType) || (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == InsTypeVoid))
                        {
                            tls_btnViewRemit.Enabled = true;
                            tls_btnViewPmnt.Enabled = false;
                        }
                        else if ((c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == PatPmntType) || (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == PatPmntVoidType) )
                        {
                            tls_btnViewRemit.Enabled = false;
                            tls_btnViewPmnt.Enabled = true;
                        }
                    }
                    else
                    {
                        tls_btnViewRemit.Enabled = false;
                        tls_btnViewPmnt.Enabled = false;
                    }

                    
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void c1ClaimChargeHistory_RowColChange(object sender, EventArgs e)
        {
            try
            {
               
                    if (c1ClaimChargeHistory.Rows.Count > 1)
                    {
                        if (c1ClaimChargeHistory.RowSel > 0)
                        {
                            if ((c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == InsType) || (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == InsTypeVoid))
                            {
                                tls_btnViewRemit.Enabled = true;
                                tls_btnViewPmnt.Enabled = false;
                            }
                            else if ((c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == PatPmntType) || (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == PatPmntVoidType))
                            {
                                tls_btnViewRemit.Enabled = false;
                                tls_btnViewPmnt.Enabled = true;
                            }

                            else
                            {
                                tls_btnViewRemit.Enabled = false;
                                tls_btnViewPmnt.Enabled = false;
                            }
                        }


                    }
                }

            
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void c1ClaimChargeHistory_AfterEdit(object sender, RowColEventArgs e)
        {
            //c1ClaimChargeHistory.AutoSizeRow(e.Row);
        }

        private void c1ClaimChargeHistory_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (c1ClaimChargeHistory.HitTest(e.X, e.Y).Column == COL_HISTORY && c1ClaimChargeHistory.HitTest(e.X, e.Y).Column == COL_HISTORY)
                {
                    gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltipDx, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);

                }
                else
                {
                    gloC1FlexStyle.ShowToolTip(C1SuperTooltipDx, (C1FlexGrid)sender, e.Location);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void c1ClaimCharges_RowColChange(object sender, EventArgs e)
        {
            try
            {

                if (c1ClaimCharges.Rows.Count > 1)
                {
                    if (c1ClaimCharges.RowSel > 0)
                    {
                        FillClaimChargeHistoryDetail(Convert.ToInt64(c1ClaimCharges.GetData(c1ClaimCharges.RowSel, c1ClaimCharges.Cols["nTransactionMSTID"].Index)), Convert.ToInt64((c1ClaimCharges.GetData(c1ClaimCharges.RowSel, c1ClaimCharges.Cols["lineflag"].Index).ToString().Trim() == "M" ? 0 : c1ClaimCharges.GetData(c1ClaimCharges.RowSel, c1ClaimCharges.Cols["nTransactionDetailMSTID"].Index))));
                    }

                }
                
            }


            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tls_btnViewPmnt_Click(object sender, EventArgs e)
        {
            Int64 nMainEOBPaymentID = 0;
            decimal amt = 0;
           // object _retVal = null;
           
            
            try
            {
                if (_nPatientID > 0)
                {
                    Int64 eobPaymentId = 0;

                    if (c1ClaimChargeHistory.Rows.Count > 1)
                    {
                        if ((c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == PatPmntType) ||(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == PatPmntVoidType))
                        {
                            if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index) != null && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index)) != "")
                            {
                                
                                eobPaymentId = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index));
                                amt = Convert.ToDecimal(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["Paid"].Index));
                                //string _strQuery = " SELECT TOP 1 " +
                                //           "         CASE " +
                                //           "           WHEN ( SELECT   COUNT(nCreditID)" +
                                //           "                FROM     dbo.Credits_DTL" +
                                //           "                WHERE    nCreditID = " + eobPaymentId +
                                //           "              ) > 0 THEN nCreditsRef_ID " +
                                //           "           ELSE 0 " +
                                //           "         END AS CreditID " +
                                //           " FROM    dbo.Credits_DTL " +
                                //           " WHERE   nCreditID = " + eobPaymentId + " AND dAmount = " + amt;
                                //nMainEOBPaymentID = Convert.ToInt64(oDB.ExecuteScalar_Query("select distinct nCredit_RefID from Debits WITH (NOLOCK) where nCreditID=" + eobPaymentId + " and  nCredit_RefID <>0"));
                                //oDB.Connect(false);
                                //_retVal = oDB.ExecuteScalar_Query(_strQuery);
                                //oDB.Disconnect();

                                //if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                                //{ nMainEOBPaymentID = Convert.ToInt64(_retVal); }
                                object _mEOBPaymentID = null;
                                _mEOBPaymentID = getEobPaymentID(eobPaymentId);
                                if (_mEOBPaymentID == null || Convert.ToString(_mEOBPaymentID).Trim() == "")
                                {
                                    _mEOBPaymentID = eobPaymentId;
                                }
                                nMainEOBPaymentID = Convert.ToInt64(_mEOBPaymentID);

                                if (nMainEOBPaymentID == 0)
                                { nMainEOBPaymentID = eobPaymentId; }
                                gloAccountsV2.frmViewPatientPaymentV2 ofrmViewPatientPayment = new gloAccountsV2.frmViewPatientPaymentV2(_sqlDatabaseConnectionString, _nPatientID, _nClinicID, nMainEOBPaymentID);
                                ofrmViewPatientPayment.StartPosition = FormStartPosition.CenterScreen;
                                ofrmViewPatientPayment.ShowDialog(this);
                                ofrmViewPatientPayment.Dispose();
                                FillClaimChargeHistory();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Payment details are not available. ", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    
                }
                else
                {
                    MessageBox.Show("Please select the patient.", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);                
                ex = null;

            }
            finally
            {
               
            }
        
        }

        private object getEobPaymentID(Int64 eobPaymentId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            object _mEOBPaymentID = null;
            try
            {

                oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
                oDB.Connect(false);
                _mEOBPaymentID = oDB.ExecuteScalar_Query("select distinct nCredit_RefID from Debits WITH (NOLOCK) where nCreditID=" + eobPaymentId + " and  nCredit_RefID <>0");
                oDB.Disconnect();
                return _mEOBPaymentID;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }
        private void tsb_Modify_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64(c1ClaimCharges.GetData(2, 3)) > 0 && _nPatientID > 0)
            {
                gloBilling ogloBilling = new gloBilling(_sqlDatabaseConnectionString, "");
                Int32 iVoidedAndReplacedIndex = 0;
                if (c1ClaimChargeHistory.Rows.Count > 1)
                {
                    iVoidedAndReplacedIndex = c1ClaimChargeHistory.FindRow("Clm Void", 1, c1ClaimChargeHistory.Cols["sType"].Index, true);
                }
                if (c1ClaimChargeHistory.RowSel > 0)
                {
                    if (iVoidedAndReplacedIndex > 0 && (c1ClaimChargeHistory.RowSel == iVoidedAndReplacedIndex))
                    {
                        Int64 nTransactionID = gloCharges.GetParentClaimTransactionID(Convert.ToInt64(c1ClaimChargeHistory.GetData(iVoidedAndReplacedIndex, c1ClaimChargeHistory.Cols["nTransactionMSTID"].Index)));
                        ogloBilling.ShowModifyCharges(_nPatientID, nTransactionID, this.Name, this);
                    }
                    else
                    {
                        ogloBilling.ShowModifyCharges(_nPatientID, Convert.ToInt64(c1ClaimCharges.GetData(2, 3)), this.Name, this);
                    }
                }
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }   
            }

            frmClaimChargeHistoryV2_Load(null, null);
        }

        private void tlb_AddNotes_Click(object sender, EventArgs e)
        {
            frmClaimNotes frmClaimNote = new frmClaimNotes(_nTransactionID);
            frmClaimNote.IsVoidShowNote = Convert.ToBoolean(c1ClaimCharges.GetData(c1ClaimCharges.RowSel, COL_BISVOID));
            frmClaimNote.ShowDialog(this);

            if (frmClaimNote.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Int64 nMstTranID = 0;
                Int64 nMstTranDetailID = 0;
                nMstTranID = Convert.ToInt64(c1ClaimCharges.GetData(c1ClaimCharges.RowSel, COL_MST_TRAN_ID));
                nMstTranDetailID = Convert.ToInt64((c1ClaimCharges.GetData(c1ClaimCharges.RowSel, COL_MST_TRANDETAIL_ID) == DBNull.Value ? 0 : c1ClaimCharges.GetData(c1ClaimCharges.RowSel, COL_MST_TRANDETAIL_ID)));

                FillClaimChargeHistoryDetail(nMstTranID, nMstTranDetailID);
            }
            frmClaimNote.Dispose();
            frmClaimNote = null;
        }

        private void tsb_ViewClaimHistoryReport_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptAgingClaimHistory", "Claim History Report", true, tsb_ViewClaimHistoryReport.Image);
        }

        private void ShowSSRSReport(string ReportName, string ReportTitle, bool blnIsgloStreamReport, Image img)
        {
            Cursor.Current = Cursors.WaitCursor;
            Int64 nMstTranID = 0;
            Int64 nMstTranDetailID = 0;
            nMstTranID = Convert.ToInt64(c1ClaimCharges.GetData(c1ClaimCharges.RowSel, COL_MST_TRAN_ID));
            nMstTranDetailID = Convert.ToInt64((c1ClaimCharges.GetData(c1ClaimCharges.RowSel, COL_MST_TRANDETAIL_ID) == DBNull.Value ? 0 : c1ClaimCharges.GetData(c1ClaimCharges.RowSel, COL_MST_TRANDETAIL_ID)));
            SSRSApplication.frmSSRSViewer frmSSRS = new SSRSApplication.frmSSRSViewer();
            frmSSRS.Conn =gloGlobal.gloPMGlobal.DatabaseConnectionString;
            frmSSRS.reportName = ReportName;
            frmSSRS.reportTitle = ReportTitle;
            frmSSRS.parameterName = "nMasterTransactionID,nTransactionID,nPatientID";
            frmSSRS.ParameterValue =Convert.ToString(nMstTranID)+ "," + Convert.ToString(_nTransactionID) + ","  +  Convert.ToString(_nPatientID); 
            frmSSRS.formIcon = img;
            frmSSRS.IsgloStreamReport = blnIsgloStreamReport;           
            Cursor.Current = Cursors.Default;
            frmSSRS.Show();
         
        }
        private void SetClaimChargeHistorySorting()
        {
            c1ClaimChargeHistory.Cols["dtDate"].AllowSorting = true;
            c1ClaimChargeHistory.Cols["sType"].AllowSorting = false;
            c1ClaimChargeHistory.Cols["Description"].AllowSorting = false;
            c1ClaimChargeHistory.Cols["Paid"].AllowSorting = false;
            c1ClaimChargeHistory.Cols["Adjustment"].AllowSorting = false;
            c1ClaimChargeHistory.Cols["Balance"].AllowSorting = false;
            c1ClaimChargeHistory.Cols["sUser"].AllowSorting = false;
            c1ClaimChargeHistory.Cols["dtCreateDate"].AllowSorting = true;

        }

        private void c1ClaimChargeHistory_AfterSort(object sender, SortColEventArgs e)
        {
            for (int iRow = 1; iRow <= c1ClaimChargeHistory.Rows.Count - 1; iRow++)
            {
                c1ClaimChargeHistory.AutoSizeRow(iRow);
            }
        }

       
    }
}
