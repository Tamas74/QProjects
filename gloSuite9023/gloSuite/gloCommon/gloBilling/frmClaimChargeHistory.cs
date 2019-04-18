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
    public partial class frmClaimChargeHistory : Form
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
        private const string InsType = "1I1";
        private const string InsTypeVoid = "1I2";
        private const string PatPmntType = "2P1";
        private const string PatAdjsType = "2P2";
        private const string PatPmntVoidType = "2P3";
        private const string PatPmntCorrType = "2P4";
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion

        #region Constructor

        public frmClaimChargeHistory(string databaseconnectionstring, Int64 patientid, Int64 clinicid, Int64 nTransactionID)
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

            _sqlDatabaseConnectionString = databaseconnectionstring;
            _nPatientID = patientid;
            _nClinicID = clinicid;
            _nTransactionID = nTransactionID;
        }

        #endregion

        public string CallingContainer { get; set; }

        # region Form Events

        private void frmClaimChargeHistory_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1ClaimCharges, false);
            gloC1FlexStyle.Style(c1ClaimChargeHistory, false);

            c1ClaimCharges.ExtendLastCol = true;
            c1ClaimCharges.AllowDragging =AllowDraggingEnum.None;
            c1ClaimChargeHistory.AllowDragging = AllowDraggingEnum.None;
            c1ClaimChargeHistory.ExtendLastCol = true;

            switch (CallingContainer)
            {
                case "frmBillingModifyCharges":
                    tsb_Modify.Visible = false;
                    break;
                default:
                    tsb_Modify.Visible = true;
                    break;
            }
            
            FillClaimChargeHistory();
            //FillClaimChargeHistoryDetail();
        }

        # endregion

        #region "Functions"

        public void FillClaimChargeHistory()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
          //  CurrencyManager CMMain;

            DataSet dsClaimChargesummary = null;
            //DataSet dsClaimChargeHistory = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oDBPatameters.Add("@nTransactionID", _nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@ClinicID", _nClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("Patient_Financial_View_Claim_Charge_History_Summary", oDBPatameters, out dsClaimChargesummary);
                if ((dsClaimChargesummary != null) && (dsClaimChargesummary.Tables[0].Rows.Count > 0))
                {
                    dsClaimChargesummary.Tables[0].TableName = "Claim_Charge_Summary";
                    c1ClaimCharges.DataMember = "Claim_Charge_Summary";
                    this.c1ClaimCharges.RowColChange -= new System.EventHandler(this.c1ClaimCharges_RowColChange);
                    c1ClaimCharges.DataSource = dsClaimChargesummary;
                    this.c1ClaimCharges.RowColChange += new System.EventHandler(this.c1ClaimCharges_RowColChange);

                  // CMMain= this.BindingContext(dsClaimChargesummary,"" );
                    if (c1ClaimCharges.Rows.Count > 1)
                    {
                     for (int rowCntr = 1; rowCntr <= c1ClaimCharges.Rows.Count - 1; rowCntr++)
                        {
                        if (c1ClaimCharges.GetData(c1ClaimCharges.Rows[rowCntr].Index, c1ClaimCharges.Cols["lineflag"].Index).ToString().Trim() == "L")
                            c1ClaimCharges.SetData(rowCntr, "ClaimNumber", "");
                        // string a = c1ClaimCharges.GetData(c1ClaimCharges.Rows[rowCntr].Index, c1ClaimCharges.Cols["ClaimNumber"].Index).ToString();
                        }  
                        FillClaimChargeHistoryDetail(Convert.ToInt64(c1ClaimCharges.GetData(c1ClaimCharges.RowSel, c1ClaimCharges.Cols["nTransactionMSTID"].Index)), Convert.ToInt64((c1ClaimCharges.GetData(c1ClaimCharges.RowSel, c1ClaimCharges.Cols["lineflag"].Index).ToString().Trim() == "M" ? 0 : c1ClaimCharges.GetData(c1ClaimCharges.RowSel, c1ClaimCharges.Cols["nTransactionDetailMSTID"].Index))));
                        tls_btnViewRemit.Enabled = false;
                        tls_btnViewPmnt.Enabled = false;
                    }

                }
                
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
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
            Boolean typeFlag = false;
            List<Int64> oContacts = new List<Int64>();

            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                if (c1ClaimCharges.Rows.Count > 1)
                {
                    oDBPatameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPatameters.Add("@nClinicID", _nClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPatameters.Add("@nTransactionMSTID", mst_TransactionID, ParameterDirection.Input, SqlDbType.BigInt);

                    if (c1ClaimCharges.GetData(c1ClaimCharges.RowSel, c1ClaimCharges.Cols["lineflag"].Index).ToString().Trim() == "M")
                    {
                        oDB.Retrive("Patient_Financial_View_Claim_History", oDBPatameters, out dsClaimChargeHistory);
                    }
                    else
                    {
                        oDBPatameters.Add("@nTransactionMSTDetailID", mst_TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Retrive("Patient_Financial_View_Charge_History", oDBPatameters, out dsClaimChargeHistory);
                    }
                    if ((dsClaimChargeHistory != null) && (dsClaimChargeHistory.Tables[0].Rows.Count > 0))
                    {

                        DataView dv = dsClaimChargeHistory.Tables[0].DefaultView;
                        DataTable dtUniqueData = dv.ToTable(true, "ClaimNo");
                        DataTable dtFilterData,dtFinalData;
                        dtFilterData = dsClaimChargeHistory.Tables[0].Clone();
                        dtFinalData=dsClaimChargeHistory.Tables[0].Clone();
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
                                //dtFilterData.Rows[cntr]["Description"]
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


                                        //dtFinalData.Rows[counter]["Description"] = dtFilterData.Rows[cntr]["Description"].ToString().Substring(0, dtFilterData.Rows[cntr]["Description"].ToString().IndexOf("\\n") - 1);

                                        //dtFinalData.Rows.Add();
                                        //counter = counter + 1;
                                        //dtFinalData.Rows[counter]["Description"] = dtFilterData.Rows[cntr]["Description"].ToString().Substring(dtFilterData.Rows[cntr]["Description"].ToString().IndexOf("\\n") + 3);
                                        //dtFinalData.Rows[counter]["Description"] = dtFilterData.Rows[cntr]["Description"].ToString();

                                        //dtFinalData.Rows[counter]["nEOBPaymentID"] = dtFilterData.Rows[cntr]["nEOBPaymentID"].ToString();
                                        //dtFinalData.Rows[counter]["nVoidRefEOBPaymentID"] = (dtFilterData.Rows[cntr]["nVoidRefEOBPaymentID"] == DBNull.Value ? 0 : dtFilterData.Rows[cntr]["nVoidRefEOBPaymentID"]);
                                        //dtFinalData.Rows[counter]["nEOBID"] = dtFilterData.Rows[cntr]["nEOBID"].ToString();
                                        ////dtFinalData.Rows[counter]["lineflag"] = dtFilterData.Rows[cntr]["lineflag"].ToString();
                                        //dtFinalData.Rows[counter]["sHiddenType"] = dtFilterData.Rows[cntr]["sHiddenType"].ToString();

                                        //counter = counter + 1;
                                        //string[] sDescriptions = Convert.ToString(dtFinalData.Rows[counter]["Description"]).Split(new String[] { "\\\\n" }, StringSplitOptions.RemoveEmptyEntries);

                                        //for (int iArrCount = 0; iArrCount <= sDescriptions.Length - 1; iArrCount++)
                                        //{

                                        //    if (iArrCount == 0)
                                        //    {
                                        //        dtFinalData.Rows[counter]["Description"] = sDescriptions[iArrCount];
                                        //    }
                                        //    else
                                        //    {
                                        //        dtFinalData.Rows.Add();
                                        //        counter = counter + 1;
                                        //        dtFinalData.Rows[counter]["Description"] = sDescriptions[iArrCount];

                                        //        dtFinalData.Rows[counter]["nEOBPaymentID"] = dtFilterData.Rows[cntr]["nEOBPaymentID"].ToString();
                                        //        dtFinalData.Rows[counter]["nVoidRefEOBPaymentID"] = (dtFilterData.Rows[cntr]["nVoidRefEOBPaymentID"] == DBNull.Value ? 0 : dtFilterData.Rows[cntr]["nVoidRefEOBPaymentID"]);
                                        //        dtFinalData.Rows[counter]["nEOBID"] = dtFilterData.Rows[cntr]["nEOBID"].ToString();
                                        //        dtFinalData.Rows[counter]["sHiddenType"] = dtFilterData.Rows[cntr]["sHiddenType"].ToString();
                                        //        dtFinalData.Rows[counter]["nTransactionID"] = dtFilterData.Rows[cntr]["nTransactionID"].ToString();


                                        //    }
                                        //}

                                        if (dtFilterData.Rows[cntr]["bIsCorrection"] != DBNull.Value && Convert.ToBoolean(dtFilterData.Rows[cntr]["bIsCorrection"]))
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
                                                    dtFinalData.Rows.Add();
                                                    counter = counter + 1;
                                                    dtFinalData.Rows[counter]["Description"] = sDescriptions[iArrCount];

                                                    dtFinalData.Rows[counter]["nEOBPaymentID"] = dtFilterData.Rows[cntr]["nEOBPaymentID"].ToString();
                                                    dtFinalData.Rows[counter]["nVoidRefEOBPaymentID"] = (dtFilterData.Rows[cntr]["nVoidRefEOBPaymentID"] == DBNull.Value ? 0 : dtFilterData.Rows[cntr]["nVoidRefEOBPaymentID"]);
                                                    dtFinalData.Rows[counter]["nEOBID"] = dtFilterData.Rows[cntr]["nEOBID"].ToString();
                                                    dtFinalData.Rows[counter]["sHiddenType"] = dtFilterData.Rows[cntr]["sHiddenType"].ToString();
                                                    dtFinalData.Rows[counter]["nTransactionID"] = dtFilterData.Rows[cntr]["nTransactionID"].ToString();


                                                }
                                            }
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
                                                        dtFinalData.Rows.Add();
                                                        counter = counter + 1;
                                                        dtFinalData.Rows[counter]["Description"] = sDescriptions[iArrCount].Replace("Total","");

                                                        dtFinalData.Rows[counter]["nEOBPaymentID"] = dtFilterData.Rows[cntr]["nEOBPaymentID"].ToString();
                                                        dtFinalData.Rows[counter]["nVoidRefEOBPaymentID"] = (dtFilterData.Rows[cntr]["nVoidRefEOBPaymentID"] == DBNull.Value ? 0 : dtFilterData.Rows[cntr]["nVoidRefEOBPaymentID"]);
                                                        dtFinalData.Rows[counter]["nEOBID"] = dtFilterData.Rows[cntr]["nEOBID"].ToString();
                                                        dtFinalData.Rows[counter]["sHiddenType"] = dtFilterData.Rows[cntr]["sHiddenType"].ToString();
                                                        dtFinalData.Rows[counter]["nTransactionID"] = dtFilterData.Rows[cntr]["nTransactionID"].ToString();
                                                    }

                                                }
                                            }

                                        }

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
                               
                                //for (int cntr = 0; cntr <= dsClaimChargeHistory.Tables["Claim_Charge_History"].Rows.Count - 1; cntr++)
                                //{


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
                                        if (typeFlag == false)
                                        {
                                            if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.Rows[i].Index, c1ClaimChargeHistory.Cols["sType"].Index).ToString() == "Resp")
                                            {
                                                c1ClaimChargeHistory.SetData(i, COL_NOTETYPE, "Post");
                                                typeFlag = true;
                                                //c1ClaimChargeHistory.SetData(i, COL_HISTORY, "New Claim: " + c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.Rows[i + 1].Index, c1ClaimChargeHistory.Cols["ClaimNo"].Index).ToString());
                                            }
                                        }
                                        else
                                        {
                                            if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.Rows[i].Index, c1ClaimChargeHistory.Cols["sType"].Index).ToString() == "Resp")
                                            {
                                                c1ClaimChargeHistory.SetData(i, "Balance", "");
                                            }
                                        }
                                    }

                                }
                        }

                        //dsClaimChargeHistory.Tables[0].TableName = "Claim_Charge_History";
                     
                        //decimal adjTotal = 0;
                        //decimal paidTotal = 0;
                        //decimal balTotal = 0;
                        //for (int cntr = 0; cntr <= dsClaimChargeHistory.Tables["Claim_Charge_History"].Rows.Count - 1; cntr++)
                        //{
                        //    adjTotal += Convert.ToDecimal(dsClaimChargeHistory.Tables["Claim_Charge_History"].Rows[cntr]["Adjustment"] == DBNull.Value ? 0 : dsClaimChargeHistory.Tables["Claim_Charge_History"].Rows[cntr]["Adjustment"]);
                        //    paidTotal += Convert.ToDecimal(dsClaimChargeHistory.Tables["Claim_Charge_History"].Rows[cntr]["Paid"] == DBNull.Value ? 0 : dsClaimChargeHistory.Tables["Claim_Charge_History"].Rows[cntr]["Paid"]);
                        //    balTotal += Convert.ToDecimal(dsClaimChargeHistory.Tables["Claim_Charge_History"].Rows[cntr]["Balance"] == DBNull.Value ? 0 : dsClaimChargeHistory.Tables["Claim_Charge_History"].Rows[cntr]["Balance"]);
                        //    c1ClaimChargeHistory.AutoSizeRow(cntr + 1);
                        //}
                        //c1ClaimChargeHistory.Rows.Add();
                        //c1ClaimChargeHistory.SetData(c1ClaimChargeHistory.Rows.Count - 1, "Description", "Total :");
                        //c1ClaimChargeHistory.SetData(c1ClaimChargeHistory.Rows.Count - 1, "Adjustment", adjTotal);
                        //c1ClaimChargeHistory.SetData(c1ClaimChargeHistory.Rows.Count - 1, "Paid", paidTotal);
                        //c1ClaimChargeHistory.SetData(c1ClaimChargeHistory.Rows.Count - 1, "Balance", balTotal);

                        //CellStyle csSubTotalRow;
                        //csSubTotalRow = c1ClaimChargeHistory.Styles.Add("SubTotalRow");
                        //csSubTotalRow.BackColor = Color.FromArgb(255, 255, 255);
                        //csSubTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        //csSubTotalRow.TextEffect = TextEffectEnum.Flat;
                        //csSubTotalRow.ForeColor = Color.Maroon;

                        //CellRange subTotalRange;
                        //subTotalRange = c1ClaimChargeHistory.GetCellRange(c1ClaimChargeHistory.Rows.Count - 1, 0, c1ClaimChargeHistory.Rows.Count - 1, 8);
                        //subTotalRange.Style = csSubTotalRow;
                        //c1ClaimChargeHistory.Row = c1ClaimChargeHistory.Rows.Fixed;

                        //CellStyle csSubCell;
                        //csSubCell = c1ClaimChargeHistory.Styles.Add("csSubCell");
                        //csSubCell.BackColor = Color.FromArgb(255, 255, 255);
                        //csSubCell.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        //csSubCell.TextEffect = TextEffectEnum.Flat;
                        //csSubCell.ForeColor = Color.Blue;
                        //CellRange subCellRange;
                        //subCellRange = c1ClaimChargeHistory.GetCellRange(c1ClaimChargeHistory.Rows.Count - 1, 9, c1ClaimChargeHistory.Rows.Count - 1, 13);
                        //subCellRange.Style = csSubCell;


                        if (c1ClaimCharges.GetData(c1ClaimCharges.RowSel, c1ClaimCharges.Cols["lineflag"].Index).ToString().Trim() == "M")
                            lblClmCharge.Text = "Claim History";
                        else
                            lblClmCharge.Text = "Charge History";
                    }
                }

                oDB.Disconnect();
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
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

                Int64 nVoidRefEOBPaymentID = 0;
               // Int64 nVoidRefEOBID = 0;
                bool nIsVoidEOBPayment = false;
           
                if (c1ClaimChargeHistory.Rows.Count > 1)
                {

                    if ((c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == InsType) || (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == InsTypeVoid))
                        {
                            if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index) != null && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index)).Trim() != "")
                            {
                                if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["sHiddenType"].Index) != null
                                   && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["sHiddenType"].Index)).Trim() != ""
                                   && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["sHiddenType"].Index)).Trim().ToUpper() == InsTypeVoid.ToUpper())
                                {
                                    //If Payment is voided then for voided eob the nVoidRefEOBPaymentID will have 
                                    //the nEOBPaymentID for the main entry
                                    nIsVoidEOBPayment = true;
                                    nVoidRefEOBPaymentID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nVoidRefEOBPaymentID"].Index));
                                }
                                
                                nEOBPaymentID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index));
                                nEOBID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBID"].Index));
                                frmViewClaimRemittance ofrmClaimChargeHistory = new frmViewClaimRemittance(_sqlDatabaseConnectionString, _nPatientID, _nClinicID, nEOBPaymentID, nEOBID);
                                ofrmClaimChargeHistory.StartPosition = FormStartPosition.CenterScreen;
                                ofrmClaimChargeHistory.IsVoidEOBPayment = nIsVoidEOBPayment;
                                ofrmClaimChargeHistory.VoidRefEOBPaymentID = nVoidRefEOBPaymentID;
                                ofrmClaimChargeHistory.CallingContainer = this.Name;
                                ofrmClaimChargeHistory.ShowDialog(this);
                                ofrmClaimChargeHistory.Dispose();
                                FillClaimChargeHistory();
                        }

                    }

                }

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
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
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
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
                    gloC1FlexStyle.ShowToolTip(C1SuperTooltipDx, (C1FlexGrid)sender, e.Location);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
        }


        private void c1ClaimChargeHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            oDB.Connect(false);

            try
            {
                Int64 nEOBPaymentID = 0;
                Int64 nEOBID = 0;

                Int64 nVoidRefEOBPaymentID = 0;
               // Int64 nVoidRefEOBID = 0;
                bool nIsVoidEOBPayment = false;
                HitTestInfo hitInfo = this.c1ClaimChargeHistory.HitTest(e.X, e.Y);
                if (c1ClaimChargeHistory.Rows.Count > 1)
                {
                    if (hitInfo.Row != 0)
                    {
                        if ((c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == InsType)||(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == InsTypeVoid))
                        {
                            if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index) != null && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index)).Trim() != "")

                            {
                                if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["sHiddenType"].Index) != null
                                  && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["sHiddenType"].Index)).Trim() != ""
                                  && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["sHiddenType"].Index)).Trim().ToUpper() == InsTypeVoid.ToUpper())
                                {
                                    //If Payment is voided then for voided eob the nVoidRefEOBPaymentID will have 
                                    //the nEOBPaymentID for the main entry
                                    nIsVoidEOBPayment = true;
                                    nVoidRefEOBPaymentID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nVoidRefEOBPaymentID"].Index));
                                }
                                nEOBPaymentID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index));
                                nEOBID = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBID"].Index));
                                frmViewClaimRemittance ofrmClaimChargeHistory = new frmViewClaimRemittance(_sqlDatabaseConnectionString, _nPatientID, _nClinicID, nEOBPaymentID, nEOBID);
                                ofrmClaimChargeHistory.StartPosition = FormStartPosition.CenterScreen;
                                ofrmClaimChargeHistory.IsVoidEOBPayment = nIsVoidEOBPayment;
                                ofrmClaimChargeHistory.VoidRefEOBPaymentID = nVoidRefEOBPaymentID;
                                ofrmClaimChargeHistory.ShowDialog(this);
                                ofrmClaimChargeHistory.Dispose();
                                FillClaimChargeHistory();
                            }
                        }
                        else if ((c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == PatPmntType) || (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, COL_TYPE).ToString().Trim() == PatPmntVoidType))
                        {
                            Int64 eobPaymentId = 0;
                            Int64 nMainEOBPaymentID = 0;
                            if (c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index) != null && Convert.ToString(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index)) != "")
                            {
                                eobPaymentId = Convert.ToInt64(c1ClaimChargeHistory.GetData(c1ClaimChargeHistory.RowSel, c1ClaimChargeHistory.Cols["nEOBPaymentID"].Index));
                                nMainEOBPaymentID = Convert.ToInt64(oDB.ExecuteScalar_Query("select distinct nRefEOBPaymentID from BL_EOBPayment_Dtl WITH (NOLOCK) where nEOBPaymentID=" + eobPaymentId + " and  nRefEOBPaymentID<>0"));
                                frmViewPatientPayment ofrmViewPatientPayment = new frmViewPatientPayment(_sqlDatabaseConnectionString, _nPatientID, _nClinicID, nMainEOBPaymentID);
                                ofrmViewPatientPayment.StartPosition = FormStartPosition.CenterScreen;
                                ofrmViewPatientPayment.ShowDialog(this);
                                ofrmViewPatientPayment.Dispose();
                                FillClaimChargeHistory();
                            }
                        }
                      
                    }
           
                }

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
            }
        }

        private void c1ClaimChargeHistory_Click(object sender, EventArgs e)
        {
            try
            {
              
                if (c1ClaimChargeHistory.Rows.Count > 1)
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
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
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

            
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
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
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
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


            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
        }

        private void tls_btnViewPmnt_Click(object sender, EventArgs e)
        {
            Int64 nMainEOBPaymentID = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            oDB.Connect(false);
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
                                nMainEOBPaymentID = Convert.ToInt64(oDB.ExecuteScalar_Query("select distinct nRefEOBPaymentID from BL_EOBPayment_Dtl WITH (NOLOCK) where nEOBPaymentID=" + eobPaymentId + " and  nRefEOBPaymentID<>0"));
                                frmViewPatientPayment ofrmViewPatientPayment = new frmViewPatientPayment(_sqlDatabaseConnectionString, _nPatientID, _nClinicID, nMainEOBPaymentID);
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
            finally
            {
                oDB.Dispose();
            }
        
        }
        
        private void tsb_Modify_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64(c1ClaimCharges.GetData(2, 3)) > 0 && _nPatientID > 0)
            {
                gloBilling ogloBilling = new gloBilling(_sqlDatabaseConnectionString, "");

                ogloBilling.ShowModifyCharges(_nPatientID, Convert.ToInt64(c1ClaimCharges.GetData(2, 3)),this.Name,this);
                ogloBilling.Dispose();
                ogloBilling = null;
            }

            frmClaimChargeHistory_Load(null, null);
        }



       
    }
}
