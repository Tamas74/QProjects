using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using gloBilling.gloAccountPayment;

namespace gloBilling
{
    class ClsCopayDistributionList
    {
        public ClsCopayDistributionList()
        {
            
        }    
        #region "Method"

       public static DataTable getCopayList(DateTime dtStartDate, DateTime dtEndDate)
       {
           gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
           gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
           DataTable dtResult = new DataTable();
           try
           {

               oParameters.Clear();
               oParameters.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.Date);
               oParameters.Add("@dtEndDate", dtEndDate, ParameterDirection.Input, SqlDbType.Date);
               oDB.Connect(false);
               oDB.Retrive("BL_getCopayforDistribution", oParameters, out dtResult);
               oDB.Disconnect();

           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
               ex = null;
               dtResult = null;
           }
           finally
           {
               if (oDB != null) { oDB.Dispose(); oDB = null; }
               if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                  
           }
           return dtResult;
       }

       public static DataTable getCopayUseReserveList(Int64 PatientID, Int64 PAccountID, DateTime dtStartDate, DateTime dtEndDate)
       {
           gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
           gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
           DataTable dtResult = new DataTable();
           try
           {

               oParameters.Clear();
               oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
               oParameters.Add("@nPAccountID", PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
               oParameters.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.Date);
               oParameters.Add("@dtEndDate", dtEndDate, ParameterDirection.Input, SqlDbType.Date);
               oDB.Connect(false);
               oDB.Retrive("BL_getPatientAvailableCoPayReserve", oParameters, out dtResult);
               oDB.Disconnect();

           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
               ex = null;
               dtResult = null;
           }
           finally
           {
               if (oDB != null) { oDB.Dispose(); oDB = null; }
               if (oParameters != null) { oParameters.Dispose(); oParameters = null; }

           }
           return dtResult;
       }
        #endregion 
    }
  
    class ClsReserveDistributionList
    {
       
        Int64 _CloseDayTrayID = 0;
        string _CloseDayTrayName = "";
        DateTime _dtCloseDate = DateTime.Now.Date;
      
        public ClsReserveDistributionList()
        {
           
        }
        #region "Method"

        public  DataTable getReserveList(DateTime dtStartDate, DateTime dtEndDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtResult = new DataTable();
            try
            {

                oParameters.Clear();
                oParameters.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@dtEndDate", dtEndDate, ParameterDirection.Input, SqlDbType.Date);
                oDB.Connect(false);
                oDB.Retrive("BL_GetReserveforDistribution", oParameters, out dtResult);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
                dtResult = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }

            }
            return dtResult;
        }

        public  DataTable getUseReserveList( Int64 PAccountID, DateTime dtStartDate, DateTime dtEndDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtResult = new DataTable();
            try
            {

                oParameters.Clear();
                
                oParameters.Add("@nPAccountID", PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@dtEndDate", dtEndDate, ParameterDirection.Input, SqlDbType.Date);
                oDB.Connect(false);
                oDB.Retrive("BL_getAvailableReservefromAccount", oParameters, out dtResult);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
                dtResult = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }

            }
            return dtResult;
        }

        public DataTable getPatientAndBadDebtDueCharges(Int64 PAccountId, bool LoadZeroBalance, bool LoadBadDebtClaims = false)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtBillingTransaction = null;
            DataTable dtBillingTransactionLines = null;
            try
            {
                oParameters.Clear();
                oParameters.Add("@nPAccountID", PAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bShowBadDebt", false, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_PaymentTransaction_Lines_ForAccountPatients_V2", oParameters, out dtBillingTransactionLines);
                oDB.Disconnect();
                oParameters.Clear();
                if (dtBillingTransactionLines != null && dtBillingTransactionLines.Rows.Count > 0)
                {
                    dtBillingTransactionLines.Columns.Add("dPayment");
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            { 
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
                if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); }
                if (dtBillingTransactionLines != null) { dtBillingTransactionLines.Dispose(); }
            }

            return dtBillingTransactionLines;
        }
        public DataTable GetPatientAccountDetails(Int64 AccountID , Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _sqlQuery = string.Empty;        
            DataTable dtAccount = null;

            try
            {
                _sqlQuery = "SELECT nGuarantorID,nAccountPatientID FROM dbo.PA_Accounts " +
                             " INNER JOIN dbo.PA_Accounts_Patients ON dbo.PA_Accounts.nPAccountID = dbo.PA_Accounts_Patients.nPAccountID" +
                             " WHERE PA_Accounts.nPAccountID=" + AccountID + "" +
                             " AND dbo.PA_Accounts_Patients.nPatientID=" + nPatientID + "";

                oDB.Connect(false);               
                oDB.Retrive_Query(_sqlQuery, out dtAccount);
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtAccount;
        }
        public gloGeneralItem.gloItems FillUseReserveforAutoDistribution(Int64 PatientID, Int64 PAccountID, decimal AccountBalance,decimal AvaliableReserve, DateTime dtStartDate,DateTime dtEndDate ,out String sCheckAmount)
        {
            gloGeneralItem.gloItems oSeletedReserveItems = new gloGeneralItem.gloItems();
            gloGeneralItem.gloItem ogloItem = null;
            decimal _Amount = 0;
            Int64 _selEOBPayId = 0;
            Int64 _selEOBPayDtlId = 0;
            Int64 _selresEOBPayId = 0;
            String _selCloseDate = "";
            DataTable dtReserve = null;
            decimal RemainingAccountBalance = AccountBalance;
            decimal dCheckAmount = 0;
            sCheckAmount = "";
            try
            {

                dtReserve = getUseReserveList(PAccountID, dtStartDate.Date, dtEndDate.Date);
                if (dtReserve != null)
                {
                    if (dtReserve.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtReserve.Rows.Count - 1; i++)
                        {

                            if (RemainingAccountBalance != 0)
                            {
                                _selEOBPayId = Convert.ToInt64(dtReserve.Rows[i]["nEOBPaymentID"]);
                                _selEOBPayDtlId = Convert.ToInt64(dtReserve.Rows[i]["nEOBPaymentDetailID"]);
                                _selresEOBPayId = Convert.ToInt64(dtReserve.Rows[i]["nReserveID"]);
                                _selCloseDate = Convert.ToString(dtReserve.Rows[i]["dtCloseDate"]);
                                _Amount = Convert.ToDecimal(dtReserve.Rows[i]["nAmount"]);
                                if (RemainingAccountBalance >= _Amount)
                                {
                                    ogloItem = new gloGeneralItem.gloItem(_selEOBPayId, Convert.ToString(_selEOBPayDtlId), Convert.ToString(_Amount));
                                    RemainingAccountBalance = RemainingAccountBalance - _Amount;
                                    dCheckAmount = dCheckAmount + _Amount;
                                }
                                else
                                {
                                    ogloItem = new gloGeneralItem.gloItem(_selEOBPayId, Convert.ToString(_selEOBPayDtlId), Convert.ToString(RemainingAccountBalance));
                                    dCheckAmount = dCheckAmount + RemainingAccountBalance;
                                    RemainingAccountBalance = 0;
                                }
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

                        sCheckAmount = Convert.ToString(dCheckAmount);
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public DataTable DistubuteAmount(string sCheckAmount, DataTable  dtBillingLines)
        {
            
            try
            {
                //this.c1SinglePayment.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);

                if (dtBillingLines != null && dtBillingLines.Rows.Count > 0)
                {
                    decimal _PatTotAmount = 0;
                    decimal _PatBalAmount = 0;
                    decimal _PatLineAmount = 0;
                    decimal _PatLineDistAmount = 0;
                    decimal _PatLineBalAmount = 0;
                    int _SelectFirstIndex = 0;
                    if (sCheckAmount.Trim() != null && sCheckAmount.Trim().Length > 0 && sCheckAmount.Trim() != "0.00" && sCheckAmount.Trim() != "0")
                    {
                        _PatTotAmount = Convert.ToDecimal(sCheckAmount.Trim());
                        _PatLineAmount = 0;
                        _PatBalAmount = _PatTotAmount - _PatLineAmount;
                    }

                    if (_PatTotAmount > 0 && dtBillingLines.Rows.Count > 0)
                    {
                        //for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                        for (int i = dtBillingLines.Rows.Count ; i > 0; i--)
                        {
                            _PatLineAmount = 0;

                            if (_PatBalAmount > 0)
                            {
                                if (Convert.ToBoolean(dtBillingLines.Rows[i-1]["bNonServiceCode"]) == false)
                                {

                                    if (_SelectFirstIndex <= 0) { _SelectFirstIndex = i - 1; }

                                    if (Convert.ToString(dtBillingLines.Rows[i - 1]["PatientDue"]) != null && Convert.ToString(dtBillingLines.Rows[i - 1]["PatientDue"]).Trim() != "")
                                        {
                                            _PatLineBalAmount = Convert.ToDecimal(Convert.ToString(dtBillingLines.Rows[i - 1]["PatientDue"]));
                                        }

                                        //if (c1SinglePayment.Cols[COL_BadDebt_DUE].Visible)
                                        //{
                                            //if (_PatLineBalAmount <= 0)
                                            //{
                                            //    if (Convert.ToString(dtBillingLines.Rows[i - 1]["BadDebtDue"]) != null && Convert.ToString(dtBillingLines.Rows[i - 1]["BadDebtDue"]).Trim() != "")
                                            //    {
                                            //        _PatLineBalAmount = Convert.ToDecimal(Convert.ToString(dtBillingLines.Rows[i - 1]["BadDebtDue"]));
                                            //    }
                                            //}
                                       // }
                                            if (Convert.ToString(dtBillingLines.Rows[i - 1]["dPayment"]) != null && Convert.ToString(dtBillingLines.Rows[i - 1]["dPayment"]).Trim() != "")
                                        {
                                            _PatLineAmount = Convert.ToDecimal(Convert.ToString(dtBillingLines.Rows[i - 1]["dPayment"]));
                                        }

                                        if (_PatLineBalAmount > 0)
                                        {
                                            if (_PatLineAmount <= 0)
                                            {
                                                if (_PatBalAmount < _PatLineBalAmount)
                                                {
                                                    _PatLineDistAmount = _PatBalAmount;
                                                }
                                                else
                                                {
                                                    _PatLineDistAmount = _PatLineBalAmount;
                                                }

                                                dtBillingLines.Rows[i - 1]["dPayment"] = _PatLineDistAmount;
                                                dtBillingLines.AcceptChanges();
                                                _PatBalAmount = _PatBalAmount - _PatLineDistAmount;
                                                
                                            }
                                        }
                                    }
                                
                            }
                            else
                            {
                                break;
                            }
                        }

                        
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return null;
            }
            finally
            {
                //this.c1SinglePayment.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);
            }
            return dtBillingLines;
        }
        private void SetCreditsDetails(dsPaymentTVP_V2 dsInsurancePayment_TVP, Int64 nPatientID,Int64 nPAccountID, Int64 nGauranatorID, Int64 nAccountPatientID,  Int64 _nCreditID, string sCheckAmount)
        {
            DataTable _dtUniqueCreditID = null;
            Int64 nPaymentNoUniqueID = 0;
            try
            {
                _dtUniqueCreditID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                {
                    nPaymentNoUniqueID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID"].ToString());
                    
                }

                dsInsurancePayment_TVP.Tables["Credits"].Rows.Add();
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sReceiptNo"] = "";
                if (sCheckAmount.Trim() != "") { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dReceiptAmount"] = Convert.ToDecimal(sCheckAmount); }
                {
                    dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dtReceiptDate"] = DateTime.Now.Date; //Convert.ToDateTime(mskCheckDate.Text);
                }
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPayerType"] = gloAccountsV2.PayerTypeV2.Self.GetHashCode();
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPayerID"] = nPatientID;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPayerName"] = "";
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentNo"] = nPaymentNoUniqueID.ToString();//lblPaymetNo.Text.Trim().Split('#')[1];                     
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 2;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(_dtCloseDate);
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sUserName"] = gloGlobal.gloPMGlobal.UserName;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nVoidType"] = 0;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPAccountID"] = nPAccountID;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nAccountPatientID"] = nAccountPatientID;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nGuarantorID"] = nGauranatorID;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentNote"] = "Payment Note";
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = "Blank Tray";
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.UseReserve.GetHashCode();
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["Credits_EXTID"] = 0;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["ClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["PaymentVoidDateTime"] = DBNull.Value;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["CreatedDateTime"] = DateTime.Now;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["ModifiedDateTime"] = DateTime.Now;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["SiteID"] = "";
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["IsFinished"] = false;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["IsERAPost"] = false;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["BPRID"] = DBNull.Value;
                dsInsurancePayment_TVP.Tables["Credits"].AcceptChanges();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);               
                ex = null;
                
            }
            finally
            {
                //this.c1SinglePayment.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);
            }
            
        }
        public Int64 SavePatientUseReservePayment(dsPaymentTVP_V2 dsInsurancePayment_TVP, DataTable dtAccountBalances, Int64 nPatientID, Int64 nPAccountID, Int64 nGauranatorID, Int64 nAccountPatientID, Int64 _nCreditID, string sCheckAmount,Int64 nPayTrayID, string sPayTrayDesc,DateTime _dtCloseDate, gloGeneralItem.gloItems ocrItems)
        {
            
            Int64 _retPayId = 0;
            int row_num = 0;
            int row_index = 0;
            DataTable _dtUniqueIDs = null;//SLR: New is not needed
            DataTable _dtUniqueCreditID = null;//SLR: New is not needed
            Int64 _nEOBID = 0;                    
            Int64 _nTransactionPatientID = 0;
            Int64 _EOBPaymentID=0;
            _CloseDayTrayID = nPayTrayID;
            _CloseDayTrayName = sPayTrayDesc;
            this._dtCloseDate = _dtCloseDate;
            try
            {
               

                if ((sCheckAmount.Trim().Length > 0 && Convert.ToDecimal(sCheckAmount) > 0))
                {
                    
                 

                    #region " Master Data "

                    _dtUniqueCreditID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                    {
                        _nCreditID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID"].ToString());
                        _nEOBID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                    }
                    SetCreditsDetails(dsInsurancePayment_TVP, nPatientID, nPAccountID, nGauranatorID, nAccountPatientID, _nCreditID, sCheckAmount);

                    #endregion

                    #region "Credit Details in case of TB/UR"

                    if (ocrItems != null)
                    {

                        if (ocrItems == null)
                        {
                            ocrItems = new gloGeneralItem.gloItems();
                        }
                        for (int crPay = 0; crPay <= ocrItems.Count - 1; crPay++)
                        {
                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Add();
                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditsDTL_ID"] = 0;
                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditsRef_ID"] = Convert.ToInt64(ocrItems[crPay].ID);
                            for (int crSub = 0; crSub <= ocrItems[crPay].SubItems.Count - 1; crSub++)
                            {
                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nReserveRef_ID"] = Convert.ToInt64(ocrItems[crPay].SubItems[crSub].ID);
                            }
                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"] = Convert.ToDecimal(ocrItems[crPay].Description);
                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.UseReserve.GetHashCode();
                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["sEntryDesc"] = "UR";


                            for (int crSub = 0; crSub <= ocrItems[crPay].SubItems.Count - 1; crSub++)
                            {
                                if (Convert.ToDateTime(_dtCloseDate) < Convert.ToDateTime(ocrItems[crPay].SubItems[crSub].CloseDate))
                                {
                                    MessageBox.Show("The used reserved amount close date is in future than the current payment close date. Please select a different payment close date.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);                                   
                                    return 0;
                                }

                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(_dtCloseDate);  
                            }
                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtCreatedDateTime"] = DateTime.Now;
                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtModifiedDateTime"] = DateTime.Now;
                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["bIsPaymentVoid"] = false;

                            dsInsurancePayment_TVP.Tables["CreditsDTL"].AcceptChanges();
                        }
                    }


                    #endregion "Credit Details in case of TB/UR"

                    #region "EOB and Debit Entry"
                    if (dtAccountBalances != null && dtAccountBalances.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtAccountBalances.Rows.Count; i++)
                        {
                                #region "Claim wise EOB and Finance Line"
                                    _nTransactionPatientID = Convert.ToInt64(dtAccountBalances.Rows[i]["nPatientID"]);
                                    //if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                                    {
                                        if (dtAccountBalances.Rows[i]["dPayment"] != null && Convert.ToString(dtAccountBalances.Rows[i]["dPayment"]).Trim() != "" && Convert.ToDecimal(dtAccountBalances.Rows[i]["dPayment"]) > 0 )
                                        {
                                            #region "EOB Service Lines"
                                            decimal _fillPayAmt = 0; //decimal _fillAdjAmt = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows.Add();
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailID"] = 0;
                                            }
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPatientID"] = _nTransactionPatientID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nContactID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(dtAccountBalances.Rows[i]["nTransactionID"]);
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(dtAccountBalances.Rows[i]["nTransactionDetailID"]);
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(dtAccountBalances.Rows[i]["TrackTransactionID"]);
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(dtAccountBalances.Rows[i]["TrackTransactionDetailID"]);
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sEntryDesc"] = "PatientPayment";
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(dtAccountBalances.Rows[i]["sCPTCode"]);
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(_dtCloseDate);  
                                          
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nVoidType"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPAccountID"] = nPAccountID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nGuarantorID"] = nGauranatorID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nAccountPatientID"] = nAccountPatientID;

                                            if (dtAccountBalances.Rows[i]["dTotal"] != null && Convert.ToString(dtAccountBalances.Rows[i]["dTotal"]).Trim() != "")
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dTotalChargeAmount"] = Convert.ToDecimal(dtAccountBalances.Rows[i]["dTotal"]);
                                            }

                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEXTID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCreditIDEXT"] = _nCreditID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBIDEXT"] = _nEOBID;
                                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailIDEXT"] = 0;
                                            }

                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextActionEXT"] = "";
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextPartyEXT"] = "";
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nNextPartyIDEXT"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtCreatedDateTimeEXT"] = DateTime.Now;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtModifiedDateTimeEXT"] = DateTime.Now;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtPaymentVoidDateTimeEXT"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nUserIDEXT"] = gloGlobal.gloPMGlobal.UserID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sUserNameEXT"] = gloGlobal.gloPMGlobal.UserName;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["bIsERAPostEXT"] = false;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nSVCIDEXT"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sMachineNameEXT"] = Environment.MachineName;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sSiteIDEXT"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sVersionEXT"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nClinicIDEXT"] = gloGlobal.gloPMGlobal.ClinicID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCLPIdEXT"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionEXT"] = Convert.ToInt64(dtAccountBalances.Rows[i]["nTransactionID"]);
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionDetailEXT"] = Convert.ToInt64(dtAccountBalances.Rows[i]["nTransactionDetailID"]);
                                            if (dtAccountBalances.Rows[i]["dPayment"] != null && Convert.ToString(dtAccountBalances.Rows[i]["dPayment"]).Trim() != "")
                                            {
                                                _fillPayAmt = Convert.ToDecimal(Convert.ToString(dtAccountBalances.Rows[i]["dPayment"]));
                                            }
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;

                                            #endregion "EOB Service Lines"

                                            #region "Debit Service Line - patient "

                                            gloGeneralItem.gloItems oItems = null;
                                            if (ocrItems != null)
                                            {
                                                oItems = (gloGeneralItem.gloItems)ocrItems;
                                            }
                                            if (oItems == null)
                                            {
                                                oItems = new gloGeneralItem.gloItems();
                                            }

                                            decimal _fillResAmt = 0;
                                            Int64 _fillResPayID = 0; Int64 _fillResPayDtlID = 0;
                                            Int64 _fillRefPayID = 0; Int64 _fillRefPayDtlID = 0;
                                            int _fillrPayIndx = -1;
                                            int _fillRefFinanceLieNo = 0;
                                            //bool _fillUseRefFinanceLieNo = false;
                                            //bool _isNullfillPayAmt = true;
                                            //bool _isNullfillAdjAmt = true;

                                            if (dtAccountBalances.Rows[i]["dPayment"] != null && Convert.ToString(dtAccountBalances.Rows[i]["dPayment"]).Trim() != "")
                                            {
                                                _fillPayAmt = Convert.ToDecimal(Convert.ToString(dtAccountBalances.Rows[i]["dPayment"] ));
                                                //    _isNullfillPayAmt = false; 
                                            }
                                            for (int rPay = 0; rPay <= oItems.Count - 1; rPay++)
                                            {
                                                if (Convert.ToDecimal(oItems[rPay].Description) > 0)
                                                {
                                                    _fillResAmt = Convert.ToDecimal(oItems[rPay].Description);
                                                    _fillResPayID = Convert.ToInt64(oItems[rPay].ID);
                                                    _fillResPayDtlID = Convert.ToInt64(oItems[rPay].Code);

                                                    if (oItems[rPay].SubItems != null && oItems[rPay].SubItems.Count > 0)
                                                    {
                                                        _fillRefPayID = Convert.ToInt64(oItems[rPay].SubItems[0].ID);
                                                        _fillRefPayDtlID = Convert.ToInt64(oItems[rPay].SubItems[0].Description);
                                                    }

                                                    //This logic is temporary depend upon the gloItems
                                                    //when we implement partial payment it should be depend upon the "EOBInsurancePaymentMasterAllocationLines" this object
                                                    if (_fillRefPayID == 0 && _fillRefPayDtlID == 0)
                                                    {
                                                        //_fillUseRefFinanceLieNo = true;
                                                        _fillRefFinanceLieNo = rPay + 2;
                                                    }

                                                    _fillrPayIndx = rPay;
                                                    break;
                                                }
                                            }

                                            if (_fillPayAmt <= _fillResAmt)
                                            {
                                                #region "Set Less Amount Single object"
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows.Add();
                                                //if (row_index < 0)
                                                //    row_index = row_index + 1;
                                                //Bug : 00000840: Patient Accounts. Patient Used reserve and made only adjustment for some lines then CreditRefId is saving zero.
                                                if (_fillResPayID == 0)
                                                {
                                                    _fillResPayID = _nCreditID;
                                                }
                                                if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                {
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = 0;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = 0;
                                                }
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = _fillResPayID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nContactID"] = 0;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientID"] = _nTransactionPatientID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nGuarantorID"] = nGauranatorID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPAccountID"] = nPAccountID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nAccountPatientID"] = nAccountPatientID;
                                                //nPAccountID,nGuarantorID,nAccountPatientID
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(dtAccountBalances.Rows[i]["nTransactionID"]);
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(dtAccountBalances.Rows[i]["nTransactionDetailID"]);
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(dtAccountBalances.Rows[i]["TrackTransactionID"]);
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(dtAccountBalances.Rows[i]["TrackTransactionDetailID"]);
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "PatientPayment";
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(dtAccountBalances.Rows[i]["sCPTCode"]);
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTDesc"] = Convert.ToString(dtAccountBalances.Rows[i]["sCPTDescription"]);;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;

                                                //if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                //{ _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); }

                                                //if (_fillAdjAmt > 0)
                                                //{
                                                //    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = _fillAdjAmt;
                                                //}
                                                //else
                                                //{
                                                //    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = 0;
                                                //}


                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = DBNull.Value;

                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sUserName"] = gloGlobal.gloPMGlobal.UserName;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(_dtCloseDate);  
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nVoidType"] = 0;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidCloseDate"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidDateTime"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsERAPost"] = false;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCreatedDateTime"] = DateTime.Now;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtModifiedDateTime"] = DateTime.Now;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sSiteID"] = DBNull.Value;
                                                if (_fillrPayIndx != -1)
                                                {
                                                    oItems[_fillrPayIndx].Description = Convert.ToString(_fillResAmt - _fillPayAmt);
                                                   ocrItems = oItems;
                                                }
                                                #endregion
                                                row_index = row_index + 1;
                                            }
                                            else
                                            {
                                                #region "Set More Amount Multiple object"

                                                decimal _fillPayMulAmt = _fillPayAmt;
                                                int loopIndex = 0; //Bug : 00000840: Patient Accounts. 
                                                do
                                                {
                                                    //Bug : 00000840: Patient Accounts. 
                                                    loopIndex++;
                                                    if (Convert.ToDecimal(oItems[_fillrPayIndx].Description) > 0)
                                                    {
                                                        _fillResAmt = Convert.ToDecimal(oItems[_fillrPayIndx].Description);
                                                        _fillResPayID = Convert.ToInt64(oItems[_fillrPayIndx].ID);
                                                        _fillResPayDtlID = Convert.ToInt64(oItems[_fillrPayIndx].Code);
                                                        _fillRefFinanceLieNo = 0;
                                                        //_fillUseRefFinanceLieNo = false;

                                                        if (oItems[_fillrPayIndx].SubItems != null && oItems[_fillrPayIndx].SubItems.Count > 0)
                                                        {
                                                            _fillRefPayID = Convert.ToInt64(oItems[_fillrPayIndx].SubItems[0].ID);
                                                            _fillRefPayDtlID = Convert.ToInt64(oItems[_fillrPayIndx].SubItems[0].Description);
                                                        }

                                                        if (_fillPayMulAmt >= _fillResAmt)
                                                        { _fillPayAmt = _fillResAmt; _fillPayMulAmt = _fillPayMulAmt - _fillPayAmt; }
                                                        else
                                                        { _fillPayAmt = _fillPayMulAmt; _fillPayMulAmt = _fillPayMulAmt - _fillPayMulAmt; }

                                                        //This logic is temporary depend upon the gloItems
                                                        //when we implement partial payment it should be depend upon the "EOBInsurancePaymentMasterAllocationLines" this object
                                                        if (_fillRefPayID == 0 && _fillRefPayDtlID == 0)
                                                        {
                                                            //_fillUseRefFinanceLieNo = true;
                                                            _fillRefFinanceLieNo = _fillrPayIndx + 2;
                                                        }
                                                    }
                                                    // Bug : 00000840: Patient Accounts. Patient Used reserve and made only adjustment for some lines then CreditRefId is saving zero.
                                                    if (_fillResPayID == 0)
                                                    {
                                                        _fillResPayID = _nCreditID;
                                                    }
                                                    #region "Set object"
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows.Add();
                                                    if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = 0;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = 0;
                                                    }
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = _fillResPayID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBID"] = _nEOBID;

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nContactID"] = 0;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientID"] = _nTransactionPatientID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nGuarantorID"] = nGauranatorID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPAccountID"] = nPAccountID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nAccountPatientID"] = nAccountPatientID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(dtAccountBalances.Rows[i]["nTransactionID"]);
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(dtAccountBalances.Rows[i]["nTransactionDetailID"]);
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(dtAccountBalances.Rows[i]["TrackTransactionID"]);
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(dtAccountBalances.Rows[i]["TrackTransactionDetailID"]);
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] =gloAccountsV2.PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "PatientPayment";
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(dtAccountBalances.Rows[i]["sCPTCode"]);
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTDesc"] = Convert.ToString(dtAccountBalances.Rows[i]["sCPTDescription"]);
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;

                                                    //if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                    //{ _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); }

                                                    ////Bug : 00000840: Patient Accounts. Adjustment was saving multiple time for same line.
                                                    //if (_fillAdjAmt > 0 && loopIndex == 1)
                                                    //{
                                                    //    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = _fillAdjAmt;
                                                    //}
                                                    //else
                                                    //{
                                                    //    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = 0;
                                                    //}


                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = DBNull.Value;

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sUserName"] = gloGlobal.gloPMGlobal.UserName;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(_dtCloseDate);  
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nVoidType"] = 0;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidCloseDate"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidDateTime"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsERAPost"] = false;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCreatedDateTime"] = DateTime.Now;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtModifiedDateTime"] = DateTime.Now;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sSiteID"] = DBNull.Value;
                                                    #endregion

                                                    if (_fillrPayIndx != -1)
                                                    {
                                                        oItems[_fillrPayIndx].Description = Convert.ToString(_fillResAmt - _fillPayAmt);
                                                        ocrItems = oItems;
                                                        _fillrPayIndx = _fillrPayIndx + 1;
                                                        if (_fillrPayIndx >= oItems.Count) { break; }
                                                    }

                                                }
                                                while (_fillPayMulAmt > 0);

                                                #endregion
                                            }

                                            #endregion
                                        }
                                        dsInsurancePayment_TVP.Tables["EOB"].AcceptChanges();
                                        dsInsurancePayment_TVP.Tables["Debits"].AcceptChanges();

                                        row_num = row_num + 1;
                                    
                                }

                                #endregion "Claim wise EOB and Finance Line"
                            }
                        }
                    

                    #endregion "EOB and Debit Entry"

                    if (dsInsurancePayment_TVP.Tables["Debits"] != null && Convert.ToInt64(dsInsurancePayment_TVP.Tables["Debits"].Rows.Count) > 0)
                    {
                        _dtUniqueIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(dsInsurancePayment_TVP.Tables["Debits"].Rows.Count);

                        for (int i = 0; i <= dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1; i++)
                        {
                            dsInsurancePayment_TVP.Tables["Debits"].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                            dsInsurancePayment_TVP.Tables["Debits"].Rows[i]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID2"].ToString());
                        }

                        for (int i = 0; i <= dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1; i++)
                        {
                            dsInsurancePayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                            dsInsurancePayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailIDEXT"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                        }
                    }

                    gloAccountsV2.gloPatientPaymentV2 objclsAccPayment = new gloAccountsV2.gloPatientPaymentV2();
                    _EOBPaymentID = objclsAccPayment.SavePatientPayment(dsInsurancePayment_TVP);
                    _retPayId = _EOBPaymentID;

                    if (_retPayId > 0)
                    {
                        Collections.CL_FollowUpCode.SetAutoAccountFollowUp(nPAccountID, 0, DateTime.Now.Date);//Convert.ToDateTime(mskCloseDate.Text));
                        //if (gloAccountsV2.gloBillingCommonV2.IsIntuitBillPayTaskOfSamePatient(IBPTaskID, oPatientControl.PatientID))
                        //{
                        //    if (IsIntuitBillPay)
                        //    {
                        //        gloAccountsV2.gloBillingCommonV2.CompleteIntuitBillPayTask(IBPTaskID);
                        //    }
                        //}
                        //IsIntuitBillPay = false;
                    }
                    _EOBPaymentID = 0;
                   
                    //SLR: Free obClsaccpayment
                    if (objclsAccPayment != null)
                    {
                        objclsAccPayment.Dispose();
                    }
                }
               
                if (_retPayId > 0)
                {
                    gloBilling ogloBilling = new gloBilling (gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                    ogloBilling.SaveUserWiseCloseDay(Convert.ToString(_dtCloseDate), CloseDayType.Payment, gloGlobal.gloPMGlobal.ClinicID);
                    gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);

                    oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(_dtCloseDate).ToString("MM/dd/yyyy"), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                    oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", _CloseDayTrayID.ToString(), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                    oSettings.Dispose();
                    ogloBilling.Dispose();
                }
               
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dsInsurancePayment_TVP != null)
                { dsInsurancePayment_TVP.Clear(); }
                // _useReserves = false;
            }
            return _retPayId;
        }
        #endregion
    }

    public class ClsAutoCoapyDistributionList
    {
        #region "Constructor"

        dsPaymentTVP_V2 dsAutoCopayPatientPayment_TVP = null;

        public ClsAutoCoapyDistributionList()
        {
            dsAutoCopayPatientPayment_TVP = new dsPaymentTVP_V2();
        }

        #endregion

        #region "Dispose"

        public void Dispose()
        {
            if (dsAutoCopayPatientPayment_TVP != null)
            {
                dsAutoCopayPatientPayment_TVP.Dispose();
                dsAutoCopayPatientPayment_TVP = null;
            }
        }

        #endregion

        #region "Method"

        public static DataTable getAutoCopayListForReserveDOS(DateTime dtStartDate, DateTime dtEndDate, Int64 nPAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtResult = new DataTable();
            try
            {
                oParameters.Clear();
                oParameters.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@dtEndDate", dtEndDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetAutoCopayDistributionList", oParameters, out dtResult);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                dtResult = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }
            return dtResult;
        }

        public static DataTable getPatientAvailableReserve(Int64 PatientID, Int64 PAccountID, string dtReserveForDOS, DateTime dtStartDate, DateTime dtEndDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtResult = new DataTable();
            try
            {
                oParameters.Clear();
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                if (dtReserveForDOS == "")
                {
                    oParameters.Add("@dtReserveForDOS", DBNull.Value, ParameterDirection.Input, SqlDbType.Date);
                }
                else
                {
                    oParameters.Add("@dtReserveForDOS", dtReserveForDOS, ParameterDirection.Input, SqlDbType.Date);
                }

                oParameters.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@dtEndDate", dtEndDate, ParameterDirection.Input, SqlDbType.Date);
                oDB.Connect(false);
                oDB.Retrive("BL_PatientAvailableCopayReserve", oParameters, out dtResult);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                dtResult = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }
            return dtResult;
        }

        public static void CreateInsatnceForUser()
        {            
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Clear();
                oParameters.Add("@bIsCopaydistOpen", 1, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@sMachinename", Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserid", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUsername", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Execute("IsAutoCopayDistribution", oParameters); 
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }            
        }

        public static bool CheckInstanceForSameUser()
        {
            bool bIsOpen = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                DataTable dt;
                string sQuery = "SELECT * FROM dbo.CopayReserve_Setting";
                oDB.Connect(false);
                oDB.Retrive_Query(sQuery, out dt);
                if (dt.Rows.Count > 0) 
                {
                    bIsOpen = true;
                    MessageBox.Show("Copay Disribution List is open at Machine : " + Convert.ToString(dt.Rows[0]["sMachineName"]), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    bIsOpen = false;
                oDB.Disconnect();
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }                
            }
            return bIsOpen;
        }

        public static void RemoveInstanceForUser()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                //string sQuery = "DELETE FROM CopayReserve_Setting WHERE sMachinename = '" + Environment.MachineName + "' AND nUserid = " + gloGlobal.gloPMGlobal.UserID + " AND sUsername = '" + gloGlobal.gloPMGlobal.UserName + "'";
                string sQuery = "TRUNCATE TABLE dbo.CopayReserve_Setting";
                oDB.Connect(false);
                object _result = oDB.ExecuteScalar_Query(sQuery);                
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }            
        }
        
        public static void CheckInstanceForloginSameUser()
        {
          
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                oDB.Connect(false);

                string sQuery = "SELECT bIsCopaydistOpen FROM CopayReserve_Setting WHERE sMachinename = '" + Environment.MachineName + "' AND nUserid = " + gloGlobal.gloPMGlobal.UserID + " AND sUsername = '" + gloGlobal.gloPMGlobal.UserName + "'";
                string dQuery = "DELETE FROM CopayReserve_Setting WHERE sMachinename = '" + Environment.MachineName + "' AND nUserid = " + gloGlobal.gloPMGlobal.UserID + " AND sUsername = '" + gloGlobal.gloPMGlobal.UserName + "'";
                object _result = oDB.ExecuteScalar_Query(sQuery);
               
                if (Convert.ToString(_result) != "" && Convert.ToString(_result) == "True" || Convert.ToString(_result) == "true")
                {
                    object result = oDB.Execute_Query(dQuery);                    
                }               
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            //return bIsOpen;
        }

        public static object GetDispalyAccountNo(long nPAccountID = 0)
        {
            object _result = null;
            string sQuery = "SELECT sAccountNo FROM PA_Accounts WHERE nPAccountID = " + nPAccountID;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                oDB.Connect(false);
                _result = oDB.ExecuteScalar_Query(sQuery);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        #endregion        
    }    
}
