using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using gloSettings;
using gloBilling;
using System.Collections;
using gloBilling.Collections;
using gloBilling.Statement;

namespace gloAccountsV2
{
    public class gloInsurancePaymentV2
    {
        #region "Constructor"

        public gloInsurancePaymentV2()
        {
            ClaimRemitRefNo = string.Empty;
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~gloInsurancePaymentV2()
        {
            Dispose(false);
        }
        #endregion "Constructor"

        #region "Public Property"

        public string ClaimRemitRefNo { get; set; }
        public int InsuranceParty { get; set; }

        #endregion "Public Property"

        #region "Save Methods"

        public Int64 SavePaymentTVP(DataSet dsPaymentTVP)
        {
            Int64 nCreditID = 0;
            object _result = null;
            object _error = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string strErrorMessage = string.Empty;
           // bool showErrorMsg = false;
            try
            {
                if (dsPaymentTVP != null && dsPaymentTVP.Tables.Count > 0)
                {
                    oParameters.Clear();
                    oDB.Connect(false);
                    oParameters.Add("@tvpCredits", dsPaymentTVP.Tables["Credits"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpDebits", dsPaymentTVP.Tables["Debits"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpEOB", dsPaymentTVP.Tables["EOB"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpReserves", dsPaymentTVP.Tables["Reserves"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpCredits_DTL", dsPaymentTVP.Tables["CreditsDTL"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpNextAction", dsPaymentTVP.Tables["BL_EOB_NextAction"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpRefunds", dsPaymentTVP.Tables["Refunds"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpReasonCode", dsPaymentTVP.Tables["ReasonCode"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpEOBNotes", dsPaymentTVP.Tables["EOBNotes"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpInsReserveAssociation", dsPaymentTVP.Tables["BL_Reserve_Association"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpHoldSelection", dsPaymentTVP.Tables["HoldSelection"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@ClaimRemitRefNo", ClaimRemitRefNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@SelectedInsuranceParty", InsuranceParty, ParameterDirection.Input, SqlDbType.Int);
                    oParameters.Add("@nCreditsID", nCreditID, ParameterDirection.Output, SqlDbType.BigInt);
                    oParameters.Add("@error_Message", strErrorMessage, ParameterDirection.Output, SqlDbType.VarChar,1000);
                    oParameters.Add("@tvpRemarkCode", dsPaymentTVP.Tables["RemarkCode"], ParameterDirection.Input, SqlDbType.Structured);
                    oDB.Execute("BL_SavePayment_TVP", oParameters, out  _result, out _error);

                    oDB.Disconnect();
                    if (_result != null && Convert.ToString(_result).Trim() != "")
                        nCreditID = Convert.ToInt64(_result);
                    else
                        nCreditID = 0;

                    if (dsPaymentTVP.Tables["BL_EOB_NextAction"] != null && dsPaymentTVP.Tables["BL_EOB_NextAction"].Rows.Count > 0)
                    {
                        CreateFollowupScheduleForPendingInsurance(Convert.ToInt64(dsPaymentTVP.Tables["BL_EOB_NextAction"].Rows[0]["nBillingTransactionID"]), Convert.ToInt64(dsPaymentTVP.Tables["BL_EOB_NextAction"].Rows[0]["nEOBID"]), Convert.ToInt64(dsPaymentTVP.Tables["BL_EOB_NextAction"].Rows[0]["nEOBPaymentID"]));
                    }
                    if (_error != null && Convert.ToString(_error) != "")
                    { strErrorMessage = Convert.ToString(_error); }
                    if (strErrorMessage != "Success")
                    {
                        throw new Exception(strErrorMessage.ToString());
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
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
            return nCreditID;

        }

        private void CreateFollowupScheduleForPendingInsurance(Int64 nTransactionID, Int64 nEOBID, Int64 nCreditID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;

            DataTable dtTrans = null;

            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                // For BL_Transaction_MST Table.
                oDBParameters.Add("@nTransactionID", nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nEOBID", nEOBID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nCreditID", nCreditID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_GetTranIdDetails", oDBParameters, out dtTrans);
                if (dtTrans != null && dtTrans.Rows.Count > 0)
                {
                    for (int _row = 0; _row < dtTrans.Rows.Count; _row++)
                    {
                        CL_FollowUpCode.CreateFollowupSchedule(Convert.ToInt64(dtTrans.Rows[_row]["TransactionMasterId"]), Convert.ToInt64(dtTrans.Rows[_row]["TransactionId"]), Convert.ToInt64(dtTrans.Rows[_row]["ContactId"]));
                    }
                }

            }

            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (dtTrans != null)
                {
                    dtTrans.Dispose();
                    dtTrans = null;
                }

            }

        }


     

        public bool   ERA_SaveCredit( DataSet ds)
        {
            bool _IsSaved = false;
            String Error="";
            object _result = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            SqlConnection _sqlConnection=null;
            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables["Credits"].Rows.Count > 0)
                {
                    oParameters.Clear();
                    //oDB.Connect(false);
                    oParameters.Add("@tvpCredits", ds.Tables["Credits"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpDebits", ds.Tables["Debits"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpEOB", ds.Tables["EOB"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpNextAction", ds.Tables["BL_EOB_NextAction"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpReasonCode", ds.Tables["ReasonCode"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpRemarkCode", ds.Tables["RemarkCode"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@Error", Error, ParameterDirection.Output, SqlDbType.VarChar, 255);
                   // oDB.Execute("SaveCredit_ERA", oParameters, out  _result);
                    _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                    {
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.CommandTimeout = 0;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandText = "SaveCredit_ERA";

                        _result = _sqlCommand.ExecuteNonQuery();

                        if (_sqlCommand.Parameters["@Error"].Value != null)
                        { Error = _sqlCommand.Parameters["@Error"].Value.ToString(); }
                        if (Error == null || Error.Trim().Length !=0)
                        {
                            throw new Exception(Error.ToString());
                        }
                        if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); _sqlCommand.Dispose(); }
                    }
                    
                    _sqlConnection.Close();
                   
                    _IsSaved = true; 
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
               
            }

            return _IsSaved; 
        }

        public Int64 SavePayment(DataSet dsInsurancePayment)
        {
            Int64 PaymentId = 0;
            try
            {
                PaymentId = SavePaymentTVP(dsInsurancePayment);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                
            }

            return PaymentId;
        }

        public Int64 SaveInsuranceRefund(DataSet dsInsurancePaymentRefund)
        {
            Int64 PaymentId = 0;
            try
            {
                PaymentId = SavePaymentTVP(dsInsurancePaymentRefund);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return PaymentId;
        }

        public static bool UpdateClaimRemittanceRefNo(Int64 TransactionID, Int64 ContactInsuranceID, Int64 PatientInsuranceID, int InsuranceParty, string ClaimRemitRefNo, Int64  ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            bool _isUpdated = false;
            object _value = null;
            try
            {

                oParameters.Clear();
                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                oParameters.Add("@nContactID", ContactInsuranceID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                oParameters.Add("@nInsuranceID", PatientInsuranceID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                oParameters.Add("@nResponsibilityNo", InsuranceParty, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                oParameters.Add("@sClaimRemittanceRefNo", ClaimRemitRefNo, ParameterDirection.Input, SqlDbType.VarChar, 50);// numeric(18,2),
                oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),

                int _retVal = 0;

                oDB.Connect(false);
                _retVal = oDB.Execute("BL_INUP_ClaimRemittanceRefNo", oParameters, out _value);

                if (_value != null || _value != DBNull.Value || _value.ToString() != "")
                { _isUpdated = Convert.ToBoolean(_value); }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return _isUpdated;
        }

        public static void SaveEOBNextActionHistory(ref gloBilling.gloAccountPayment.dsPaymentTVP_V2 dsPaymentNextAction, Int64 nEOBPaymentID, Int64 nEOBID, out bool _isDataSaved)
        {
            _isDataSaved = false;

            SqlConnection _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            SqlTransaction _sqlTransaction = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);  
            try
            {
                _sqlConnection.Open();
                if (dsPaymentNextAction != null && dsPaymentNextAction.Tables["BL_EOB_NextAction"].Rows.Count > 0)
                {
                    for (int index = 0; index < dsPaymentNextAction.Tables["BL_EOB_NextAction"].Rows.Count; index++)
                    {

                        using (gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters())
                        {
                            oParameters.Add("@nBillingTransactionID", dsPaymentNextAction.Tables["BL_EOB_NextAction"].Rows[index]["nBillingTransactionID"], ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                            oParameters.Add("@nBillingTransactionDetailID", dsPaymentNextAction.Tables["BL_EOB_NextAction"].Rows[index]["nBillingTransactionDetailID"], ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nNextActionPatientInsID", dsPaymentNextAction.Tables["BL_EOB_NextAction"].Rows[index]["nNextActionPatientInsID"], ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                            oParameters.Add("@nNextActionPartyNumber", dsPaymentNextAction.Tables["BL_EOB_NextAction"].Rows[index]["nNextActionPartyNumber"], ParameterDirection.Input, SqlDbType.Int);//	int,
                            oParameters.Add("@nNextActionContactID", dsPaymentNextAction.Tables["BL_EOB_NextAction"].Rows[index]["nNextActionContactID"], ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@sNextActionCode", dsPaymentNextAction.Tables["BL_EOB_NextAction"].Rows[index]["sNextActionCode"], ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                            oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nNextPartyType", dsPaymentNextAction.Tables["BL_EOB_NextAction"].Rows[index]["nNextPartyType"], ParameterDirection.Input, SqlDbType.Int);//	int,
                            oParameters.Add("@nCloseDate", dsPaymentNextAction.Tables["BL_EOB_NextAction"].Rows[index]["nCloseDate"], ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@sSubClaimNo", dsPaymentNextAction.Tables["BL_EOB_NextAction"].Rows[index]["sSubClaimNo"], ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                            oParameters.Add("@nTrackMstTrnID", dsPaymentNextAction.Tables["BL_EOB_NextAction"].Rows[index]["nTrackMstTrnID"], ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                            oParameters.Add("@nTrackMstTrnDetailID", dsPaymentNextAction.Tables["BL_EOB_NextAction"].Rows[index]["nTrackMstTrnDetailID"], ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                            oParameters.Add("@nHSTID", 0, ParameterDirection.Output, SqlDbType.BigInt);//	numeric(18, 0),

                            using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                            {
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "BL_EOB_NextAction_Add_History_V2";

                                int _result = _sqlCommand.ExecuteNonQuery();

                                if ((_sqlCommand.Parameters["@nHSTID"] != null) && (_sqlCommand.Parameters["@nHSTID"].Value != null) && (_sqlCommand.Parameters["@nHSTID"].Value != DBNull.Value))
                                { //oPaymentInsuranceLineNextActions[index].nHSTID = Convert.ToInt64(_sqlCommand.Parameters["@nHSTID"].Value); 
                                }
                                if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); }
                            }
                        }
                    }
                }
                _isDataSaved = true;
                _sqlConnection.Close();
            }
            catch (Exception ex)
            {
                _isDataSaved = false;
                _sqlConnection.Close();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }


        public static bool HidePendingCheck(Int64 nCreditID,bool bHidePendingCheck)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            try
            {
                string strQuery = "UPDATE Credits_EXT SET bHidePendingCheck= '" + bHidePendingCheck + "' WHERE nCreditID= " + nCreditID + "";
                oDB.Connect(false);
                int _result = oDB.Execute_Query(strQuery);
                oDB.Disconnect();
                return Convert.ToBoolean(_result);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
           
        }

        #endregion "Save Methods"

        #region "Get Methods"

        public static object SetRefundTo(Int64 _nInsuranceID, Int64 _ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            object _ResultGuarantor = null;
            try
            {
                string _strQuery = "";
                _strQuery = "SELECT ISNULL(Patient_OtherContacts.sFirstName,'') + SPACE(1) + ISNULL(Patient_OtherContacts.sMiddleName,'') + SPACE(1)+ ISNULL(Patient_OtherContacts.sLastName,'') AS Guarantor "
                          + " FROM Patient WITH (NOLOCK) LEFT JOIN Patient_OtherContacts WITH (NOLOCK) ON Patient.nPatientID = Patient_OtherContacts.nPatientID "
                          + " WHERE Patient.nPatientID = " + _nInsuranceID + " AND Patient.nClinicID =" + _ClinicID + " AND (Patient_OtherContacts.nPatientContactTypeFlag = 1 OR Patient_OtherContacts.nPatientContactTypeFlag  IS NULL )  ";


                oDB.Connect(false);
                _ResultGuarantor = oDB.ExecuteScalar_Query(_strQuery);

                if (Convert.ToString(_ResultGuarantor).Trim() == "")
                {
                    _strQuery = "SELECT dbo.GET_NAME(sFirstName,sMiddleName,sLastName) FROM Patient WITH (NOLOCK) WHERE nPatientID ='" + _nInsuranceID + "'";
                    _ResultGuarantor = oDB.ExecuteScalar_Query(_strQuery);
                }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_ResultGuarantor != null) { _ResultGuarantor = null; }
            }
            return _ResultGuarantor;
        }
    
        public static gloAccountsV2.PaymentCollection.PaymentInsuranceClaim GetBillingTransaction(Int64 ClaimNumber, string SubClaimNumber, Int64 InsContactID, Int64 InsPlanID)
        {
            DataTable _dtBillingTransactionLines = null;

            gloAccountsV2.PaymentCollection.PaymentInsuranceClaim oPaymentClaim = null;
            gloAccountsV2.PaymentCollection.PaymentInsuranceLine oPaymentClaimLine = null;
            gloBilling.SplitClaimDetails _claimDetails = null;

            try
            {
                _claimDetails = new gloBilling.SplitClaimDetails(ClaimNumber, SubClaimNumber);
                
                if (_claimDetails.TransactionMasterID > 0 && _claimDetails.TransactionID > 0)
                {
                    #region " Set Transaction Master Data "

                    DataRow _drBillingTransaction = GetBillingTransactions(_claimDetails.TransactionMasterID, _claimDetails.TransactionID);
                    if (_drBillingTransaction != null)
                    {
                        oPaymentClaim = new gloAccountsV2.PaymentCollection.PaymentInsuranceClaim();

                        oPaymentClaim.ClaimNo = Convert.ToInt64(_drBillingTransaction["nClaimNo"]);
                        oPaymentClaim.DisplayClaimNo = GetFormattedClaimPaymentNumber(_drBillingTransaction["nClaimNo"].ToString());
                        oPaymentClaim.ClaimNoPrefix = Convert.ToString(_drBillingTransaction["sCaseNoPrefix"]);
                        oPaymentClaim.BillingTransactionID = Convert.ToInt64(_drBillingTransaction["nTransactionID"]);
                        oPaymentClaim.BillingTransactionDate = Convert.ToInt64(_drBillingTransaction["nTransactionDate"]);

                        oPaymentClaim.TrackBillingTrnID = Convert.ToInt64(_drBillingTransaction["TrackingTrnID"]);
                        oPaymentClaim.SubClaimNo = Convert.ToString(_drBillingTransaction["SubClaimNo"]);

                        oPaymentClaim.PatientID = Convert.ToInt64(_drBillingTransaction["nPatientID"]);
                        oPaymentClaim.PatientName = Convert.ToString(_drBillingTransaction["PatientName"]);
                        oPaymentClaim.FacilityType = Convert.ToInt16(_drBillingTransaction["nFacilityType"]);
                    }

                    #endregion " Set Transaction Master Data "

                    #region "Retrieve Billing Transaction Lines Data "

                    _dtBillingTransactionLines = GetBillingTransactionLines(InsContactID, InsPlanID, oPaymentClaim.BillingTransactionID, 0, oPaymentClaim.PatientID, oPaymentClaim.TrackBillingTrnID);
                    if (_dtBillingTransactionLines != null && _dtBillingTransactionLines.Rows.Count > 0)
                    {
                        foreach (DataRow _drBillingTransactionLine in _dtBillingTransactionLines.Rows)
                        {
                            oPaymentClaimLine = new gloAccountsV2.PaymentCollection.PaymentInsuranceLine();

                            oPaymentClaimLine.PatientID = oPaymentClaim.PatientID;

                            oPaymentClaimLine.BLTransactionID = oPaymentClaim.BillingTransactionID;
                            oPaymentClaimLine.BLTransactionDetailID = Convert.ToInt64(_drBillingTransactionLine["nTransactionDetailID"].ToString());
                            oPaymentClaimLine.BLTransactionLineNo = Convert.ToInt64(_drBillingTransactionLine["nTransactionLineNo"].ToString());

                            oPaymentClaimLine.ClaimNumber = Convert.ToInt64(_drBillingTransactionLine["ClaimNumber"].ToString());
                            oPaymentClaimLine.SubClaimNumber = Convert.ToString(_drBillingTransactionLine["SubClaimNo"].ToString());

                            oPaymentClaimLine.TrackBLTransactionID = oPaymentClaim.TrackBillingTrnID;
                            oPaymentClaimLine.TrackBLTransactionDetailID = Convert.ToInt64(_drBillingTransactionLine["TrackTrnDtlID"].ToString());
                            oPaymentClaimLine.TrackBLTransactionLineNo = Convert.ToInt64(_drBillingTransactionLine["TrackTrnLineNo"].ToString());

                            oPaymentClaimLine.DOSFrom = Convert.ToInt64(_drBillingTransactionLine["nFromDate"].ToString());
                            oPaymentClaimLine.DOSTo = Convert.ToInt64(_drBillingTransactionLine["nToDate"].ToString());

                            oPaymentClaimLine.CPTCode = Convert.ToString(_drBillingTransactionLine["sCPTCode"].ToString());
                            oPaymentClaimLine.CPTDescription = Convert.ToString(_drBillingTransactionLine["sCPTDescription"].ToString());
                            oPaymentClaimLine.Modifier = Convert.ToString(_drBillingTransactionLine["Modifier"].ToString());

                            oPaymentClaimLine.CrossWalkCPTCode = Convert.ToString(_drBillingTransactionLine["sCrossWalkCPTCode"]);

                            oPaymentClaimLine.Charges = Convert.ToDecimal(_drBillingTransactionLine["dCharges"]);
                            oPaymentClaimLine.Unit = Convert.ToDecimal(_drBillingTransactionLine["dUnit"]);
                            oPaymentClaimLine.TotalCharges = Convert.ToDecimal(_drBillingTransactionLine["dTotal"]);
                           
                            if (Convert.ToString(_drBillingTransactionLine["dAllowed"]).Trim() != "")
                            {
                                oPaymentClaimLine.Allowed = Convert.ToDecimal(_drBillingTransactionLine["dAllowed"]);
                                oPaymentClaimLine.IsNullAllowed = false;
                            }
                            else
                            {
                                oPaymentClaimLine.IsNullAllowed = true;
                            }

                            oPaymentClaimLine.LinePaidAmount = Convert.ToDecimal(_drBillingTransactionLine["TotalPaidAmount"]);
                            oPaymentClaimLine.LineBalance = Convert.ToDecimal(_drBillingTransactionLine["TotalBalanceAmount"]);

                            if (Convert.ToString(_drBillingTransactionLine["LastInsAllowedAmount"]).Trim() != "")
                            {
                                oPaymentClaimLine.Last_allowed = Convert.ToDecimal(_drBillingTransactionLine["LastInsAllowedAmount"]);
                                oPaymentClaimLine.IsLast_allowedNull = false;
                            }
                            else
                            {
                                oPaymentClaimLine.IsLast_allowedNull = true;
                            }

                            if (Convert.ToString(_drBillingTransactionLine["LastInsPaidAmount"]).Trim() != "")
                            {
                                oPaymentClaimLine.Last_payment = Convert.ToDecimal(_drBillingTransactionLine["LastInsPaidAmount"]);
                                oPaymentClaimLine.IsLast_paymentNull = false;
                            }
                            else
                            {
                                oPaymentClaimLine.IsLast_paymentNull = true;
                            }

                            if (Convert.ToString(_drBillingTransactionLine["LastInsWriteOffAmount"]).Trim() != "")
                            {
                                oPaymentClaimLine.Last_writeoff = Convert.ToDecimal(_drBillingTransactionLine["LastInsWriteOffAmount"]);
                                oPaymentClaimLine.IsLast_writeoffNull = false;
                            }
                            else
                            {
                                oPaymentClaimLine.IsLast_writeoffNull = true;
                            }

                            if (Convert.ToString(_drBillingTransactionLine["LastInsCopayAmount"]).Trim() != "")
                            {
                                oPaymentClaimLine.Last_copay = Convert.ToDecimal(_drBillingTransactionLine["LastInsCopayAmount"]);
                                oPaymentClaimLine.IsLast_copayNull = false;
                            }
                            else
                            {
                                oPaymentClaimLine.IsLast_copayNull = true;
                            }
                            if (Convert.ToString(_drBillingTransactionLine["LastInsDeductibleAmount"]).Trim() != "")
                            {
                                oPaymentClaimLine.Last_deductible = Convert.ToDecimal(_drBillingTransactionLine["LastInsDeductibleAmount"]);
                                oPaymentClaimLine.IsLast_deductibleNull = false;
                            }
                            else
                            {
                                oPaymentClaimLine.IsLast_deductibleNull = true;
                            }

                            if (Convert.ToString(_drBillingTransactionLine["LastInsCoinsuranceAmount"]).Trim() != "")
                            {
                                oPaymentClaimLine.Last_coinsurance = Convert.ToDecimal(_drBillingTransactionLine["LastInsCoinsuranceAmount"]);
                                oPaymentClaimLine.IsLast_coinsuranceNull = false;
                            }
                            else
                            {
                                oPaymentClaimLine.IsLast_coinsuranceNull = true;
                            }

                            if (Convert.ToString(_drBillingTransactionLine["LastInsWithholdAmount"]).Trim() != "")
                            {
                                oPaymentClaimLine.Last_withhold = Convert.ToDecimal(_drBillingTransactionLine["LastInsWithholdAmount"]);
                                oPaymentClaimLine.IsLast_withholdNull = false;
                            }
                            else
                            {
                                oPaymentClaimLine.IsLast_withholdNull = true;
                            }

                            if (Convert.ToString(_drBillingTransactionLine["IsCorrection"]).Trim() != "")
                            { oPaymentClaimLine.Iscorrection = Convert.ToBoolean(_drBillingTransactionLine["IsCorrection"]); }

                            if (Convert.ToString(_drBillingTransactionLine["IsSplitted"]).Trim() != "")
                            { oPaymentClaimLine.IsSplitted = Convert.ToBoolean(_drBillingTransactionLine["IsSplitted"]); }
                            else
                            { oPaymentClaimLine.IsSplitted = false; }

                            if (Convert.ToString(_drBillingTransactionLine["LastEOBID"]).Trim() != "")
                            { oPaymentClaimLine.Last_EOBID = Convert.ToInt64(_drBillingTransactionLine["LastEOBID"]); }


                            if (Convert.ToString(_drBillingTransactionLine["LastEOBPaymentId"]).Trim() != "")
                            { oPaymentClaimLine.LastEOBPaymentId = Convert.ToInt64(_drBillingTransactionLine["LastEOBPaymentId"]); }

                            if (Convert.ToString(_drBillingTransactionLine["TotalPatientPaidAmount"]).Trim() != "")
                            { oPaymentClaimLine.PatientPaidAmount = Convert.ToDecimal(_drBillingTransactionLine["TotalPatientPaidAmount"]); }

                            
                            oPaymentClaim.CliamLines.Add(oPaymentClaimLine);
                            if (oPaymentClaimLine != null) { oPaymentClaimLine.Dispose(); }
                        }
                    }

                    #endregion
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            { ex.ERROR_Log(ex.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {               
                if (oPaymentClaimLine != null) { oPaymentClaimLine.Dispose(); }
                if (_dtBillingTransactionLines != null) { _dtBillingTransactionLines.Dispose(); }
                if (_claimDetails != null) { _claimDetails = null; }
            }

            return oPaymentClaim;
        }

        public static bool IsTakeBack(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            bool _isTakeBack = false;
            int _retVal = 0;
            object _value = null;
            try
            {
                oParameters.Clear();

                oParameters.Add("@nEOBPaymentID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@IsTakeBack", 0, ParameterDirection.InputOutput, SqlDbType.Bit);

                oDB.Connect(false);
                _retVal = oDB.Execute("BL_EOB_TAKEBACK_V2", oParameters, out _value);

                if (_value != null || _value != DBNull.Value || _value.ToString() != "")
                { _isTakeBack = Convert.ToBoolean(_value); }

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
            return _isTakeBack;
        }
        
        public static bool IsPaymentTrayActive(Int64 TrayId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            object _retVal = null;
            bool _isActiveTray = false;

            try
            {
                _sqlQuery = " SELECT ISNULL(bIsActive,1) AS IsActive FROM BL_CloseDayTray " +
                            " WHERE nCloseDayTrayID = " + TrayId + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID;

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                { _isActiveTray = true; }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _isActiveTray;
        }
        
        public static bool IsResponsibilityBilled(Int64 ClaimNumber, string SubClaimNumber, Int64 InsuranceID, int SelectedResponsibility, string SelectedAction)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            object _retVal = null;
            bool _isBilled = false;
            bool IsRebillAction = false;

            try
            {
                if (SelectedAction.StartsWith("R"))
                {
                    IsRebillAction = true;
                }

                DataTable _dtClaimInsurances = null;
                DataRow _drClaimInsurances = null;

                _sqlQuery = " SELECT IsBilled_EOB,IsBilled_NEXT,nResponsibilityNo,IsBilledEOBVoided FROM dbo.view_ClaimInsurances_V2 WHERE nClaimNo = " + ClaimNumber + " AND nInsuranceID = " + InsuranceID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtClaimInsurances);  //_retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_dtClaimInsurances != null && _dtClaimInsurances.Rows.Count > 0)
                {
                    _drClaimInsurances = _dtClaimInsurances.Rows[0];

                    if (_drClaimInsurances != null)
                    {
                        if (IsRebillAction == true)
                        {
                            if ((Convert.ToBoolean(_drClaimInsurances["IsBilled_EOB"])) || (Convert.ToBoolean(_drClaimInsurances["IsBilled_NEXT"])))
                            { _isBilled = true; }
                        }
                        else
                        {
                            if (Convert.ToInt16(_drClaimInsurances["nResponsibilityNo"]) != SelectedResponsibility)
                            {
                                if (
                                    ((Convert.ToBoolean(_drClaimInsurances["IsBilled_EOB"])) || (Convert.ToBoolean(_drClaimInsurances["IsBilled_NEXT"])))
                                    && (Convert.ToInt16(_drClaimInsurances["nResponsibilityNo"]) < SelectedResponsibility)
                                    )
                                {
                                    if (Convert.ToBoolean(_drClaimInsurances["IsBilledEOBVoided"]) == false)
                                    { _isBilled = true; }
                                }
                            }
                            else
                            {
                                if ((Convert.ToBoolean(_drClaimInsurances["IsBilled_EOB"])) || (Convert.ToBoolean(_drClaimInsurances["IsBilled_NEXT"])))
                                {
                                    if (Convert.ToBoolean(_drClaimInsurances["IsBilledEOBVoided"]) == false)
                                    { _isBilled = true; }
                                }
                            }
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _isBilled;
        }

        public static bool IsExistCheck(string receiptNumber, DateTime receiptDate, decimal receiptAmount, PayerTypeV2 payerType, Int32 paymentMode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            Object _retVal = false;
            //string _sqlQuery = "";
            bool _IsExistCheck = false;

            try
            {
                if (Convert.ToString(receiptNumber).Trim() != "")
                {
                    oParameters = new gloDatabaseLayer.DBParameters();
                    oParameters.Add("@sReceiptNo", receiptNumber, ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oParameters.Add("@dtReceiptDate", receiptDate, ParameterDirection.Input, SqlDbType.Date);
                    oParameters.Add("@dReceiptAmount", receiptAmount, ParameterDirection.Input, SqlDbType.Decimal);
                    oParameters.Add("@nPayerType", payerType.GetHashCode(), ParameterDirection.Input, SqlDbType.TinyInt);
                    oParameters.Add("@nPaymentMode", paymentMode, ParameterDirection.Input, SqlDbType.TinyInt);

                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar("gsp_IsReceiptExists", oParameters);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    { _IsExistCheck = Convert.ToBoolean(_retVal); }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oParameters != null) { oParameters.Clear(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _IsExistCheck;
        }

        public static bool IsCheckUpdating(Int64 CreditID, string CheckNumber, DateTime CheckDate, int PaymentMode, decimal CheckAmount)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            object _retVal = null;
            bool _isUpdating = false;

            try
            {
                //bool isExist = IsExistCheck(CheckNumber, CheckDate, CheckAmount, PayerTypeV2.Insurance);
                //if (isExist)
                //{
                    _sqlQuery = " SELECT CASE COUNT(*) WHEN 0 THEN 1 ELSE 0 END AS isCheckUpdating FROM Credits " +
                                 " WHERE nCreditID = " + CreditID +
                                 " AND UPPER(sReceiptNo) = '" + CheckNumber.Trim().ToUpper().Replace("'", "''") + "' " +
                                 " AND dtReceiptDate = '" + CheckDate + "' AND dReceiptAmount = " + CheckAmount + " AND nPaymentMode = " + PaymentMode;


                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                    { _isUpdating = true; }
                //}
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _isUpdating;
        }

        public static bool GetDialyCloseValidationSetting(Int64 nClinicId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _sqlQuery = string.Empty;
            bool _isCheckValid = false;
            Object _retVal = null;

            try
            {
                _sqlQuery = "select sSettingsValue from settings where sSettingsName = 'Complete Payments before Daily Close' and nClinicID = " + nClinicId;


                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal) != "")
                { _isCheckValid = Convert.ToBoolean(_retVal); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _isCheckValid;
        }

        public static bool IsRefunded(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            Object _retVal = false;
            string _sqlQuery = "";
            bool _IsRefunded = false;

            try
            {
                if (CreditID > 0)
                {
                    oDB.Connect(false);
                    _sqlQuery = " SELECT nCreditID " +
                                " FROM Credits_DTL WITH (NOLOCK) " +
                                " WHERE   nEntryType = 5 " +
                                " AND ( bIsPaymentVoid IS NULL " +
                                "  OR bIsPaymentVoid = 0 " +
                                " ) " +
                                " AND nCreditsRef_ID = " + CreditID;
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    { _IsRefunded = Convert.ToBoolean(_retVal); }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _IsRefunded;
        }

        public static bool IsVoidedInsurancePayment(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            bool _isVoided = false;
            int _retVal = 0;
            object _value = null;
            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nEOBPaymentID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@IsVoided", 0, ParameterDirection.InputOutput, SqlDbType.Bit);
                _retVal = oDB.Execute("BL_SELECT_IsVoided_V2", oParameters, out _value);

                if (_value != null || _value != DBNull.Value || _value.ToString() != "")
                { _isVoided = Convert.ToBoolean(_value); }

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
            return _isVoided;
        }

        public static bool IsCheckOverAllocated(Int64 nEOBPaymentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            bool _isOverAllocated = false;

            DataTable _dtEOBPayment = null;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nEOBPaymentID", nEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_OVER_ALLOCATED_CHECK_V2", oParameters, out _dtEOBPayment);
                oDB.Disconnect();

                if (_dtEOBPayment != null && _dtEOBPayment.Rows.Count > 0)
                {
                    _isOverAllocated = Convert.ToBoolean(_dtEOBPayment.Rows[0]["isOverAllocated"]);
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_dtEOBPayment != null) { _dtEOBPayment.Dispose(); }
            }
            return _isOverAllocated;
        }

        public static bool IsMedicarePrimary(Int64 TransactionMasterID, Int64 TransactionID, Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtMedicare = null;
            bool _IsMedicarePrimary = false;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nTransactionMasterID", TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("BL_IsMedicarePrimary", oParameters, out _dtMedicare);
                oDB.Disconnect();

                if (_dtMedicare != null && _dtMedicare.Rows.Count > 0)
                {
                    if (Convert.ToString(_dtMedicare.Rows[0]["sInsuranceTypeCode"]).Trim().ToUpper() == "MB" || Convert.ToString(_dtMedicare.Rows[0]["sInsuranceTypeCode"]).Trim().ToUpper() == "MA")
                    {
                        _IsMedicarePrimary = true;
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
                if (oParameters != null) { oParameters.Dispose(); }
                if (_dtMedicare != null) { _dtMedicare.Dispose(); }
            }

            return _IsMedicarePrimary;
        }

        public static bool IsIncludeMedicareClaimRef(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            Object _retVal = false;
            string _sqlQuery = "";
            bool _bIncludeMedicareClaimRef = false;

            try
            {
                if (ContactID > 0)
                {
                    oDB.Connect(false);
                    _sqlQuery = "SELECT ISNULL(bIncludeMedicareClaimRef,'')  AS bIncludeMedicareClaimRef FROM dbo.Contacts_Insurance_DTL WHERE nContactID = " + ContactID;

                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    { _bIncludeMedicareClaimRef = Convert.ToBoolean(_retVal); }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _bIncludeMedicareClaimRef;
        }

        public static bool IsBusinessCenterAssociated(Int64 _nPAccountID)
        {
            #region "Validation on generating individual statement if no Business Center Associated"

            bool _isBusinessCenterAssociated = false;
            object _result = null;
            string _sqlQuery = string.Empty;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            //DataTable dtTemp = new DataTable();
            oDB.Connect(false);
            try
            {
                _sqlQuery = "SELECT CASE ISNULL(nBusinessCenterID, 0) " +
                            "  WHEN 0 THEN 'False' " +
                            "  ELSE 'True' " +
                            " END AS IsBusinessCenterAssociated FROM PA_Accounts WITH (NOLOCK) WHERE nPAccountID = " + _nPAccountID;

                _result = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_result != null && Convert.ToString(_result) != "")
                {
                    _isBusinessCenterAssociated = Convert.ToBoolean(_result);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
              //  dtTemp.Dispose();
            }

            return _isBusinessCenterAssociated;

            #endregion "Validation on generating individual statement if no Business Center Associated"
        }

        public static decimal GetTotalInsuranceReservesAvailable(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            object _retVal = null;
            decimal _amountToReserve = 0;

            try
            {
                _sqlQuery = "select sum(PrevPaid) AmtToReserve  from view_InsuranceToReserves_v2 where nCreditID = '" + CreditID + "'";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                {
                    _amountToReserve = Convert.ToDecimal(_retVal);
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _amountToReserve;
        }

        public static decimal GetTotalInsuranceReservesUsed(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            decimal _reserveAmount = 0;
            DataTable _dtEOBPayment = null;

            try
            {

                oParameters.Clear();
                oParameters.Add("@nCreditID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_InsuranceReserveUsed_V2", oParameters, out _dtEOBPayment);
                oDB.Disconnect();

                if (_dtEOBPayment != null && _dtEOBPayment.Rows.Count > 0)
                {
                    _reserveAmount = Convert.ToDecimal(_dtEOBPayment.Rows[0]["UsedReserve"]);
                }

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

            return _reserveAmount;
        }

        public static decimal GetCurrentTransactionAllocation(Int64 _nBillingTransactionID, Int64 _nCreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtCurrentTransactionAllocation = null;
            decimal _CurrentTransactionAllocation = 0;
            try
            {
                if (_nCreditID != 0)
                {
                    oParameters.Add("@BillingTransactionID", _nBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@CreditID", _nCreditID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Connect(false);
                    oDB.Retrive("GetCurrentTransactionAllocation_V2", oParameters, out _dtCurrentTransactionAllocation);
                    oDB.Disconnect();

                    if (_dtCurrentTransactionAllocation != null && _dtCurrentTransactionAllocation.Rows.Count > 0)
                    {
                        if (Convert.ToString(_dtCurrentTransactionAllocation.Rows[0]["CurrentTransactionAllocation"]).ToString() != "")
                        {
                            _CurrentTransactionAllocation = Convert.ToDecimal(_dtCurrentTransactionAllocation.Rows[0]["CurrentTransactionAllocation"]);
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
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }

            }
            return _CurrentTransactionAllocation;
        }

        public static Int64 GetCPTCrossWalKID(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _sqlQuery = string.Empty;
            Int64 _crossWalkID = 0;
            Object _retVal = null;

            try
            {
                _sqlQuery = " SELECT ISNULL(Contacts_Insurance_DTL.nCPTMappingID,0) CrosswalkID FROM Contacts_Insurance_DTL " +
                            " JOIN CPT_Mapping_MST ON Contacts_Insurance_DTL.nCPTMappingID = CPT_Mapping_MST.nCPTMappingID " +
                            " WHERE Contacts_Insurance_DTL.nContactID = " + ContactID;

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                { _crossWalkID = Convert.ToInt64(_retVal); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _crossWalkID;
        }

        public static Int64 GetDefaultPaymentTrayID()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _sqlQuery = string.Empty;
            Int64 _defaultTrayId = 0;
            Object _retVal = null;

            try
            {
                _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray " +
                            " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                            " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + gloGlobal.gloPMGlobal.UserID + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID;

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                { _defaultTrayId = Convert.ToInt64(_retVal); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _defaultTrayId;
        }

        public static Int64 GetPatientIDForTransaction(Int64 TransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _sqlQuery = string.Empty;
            Int64 _nPatientID = 0;
            Object _retVal = null;

            try
            {
                _sqlQuery = " select TOP 1 nPatientID " +
                            " From dbo.BL_Transaction_Claim_MST " +
                            " where nTransactionID = " + TransactionID;

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                { _nPatientID = Convert.ToInt64(_retVal); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _nPatientID;
        }
        
        public static Int64 GetClaimInsuranceID(Int64 ClaimNumber, Int64 ResponsibilityNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _sqlQuery = string.Empty;
            Int64 _insuranceID = 0;
            Object _retVal = null;

            try
            {
                _sqlQuery = " SELECT PatientInsurance_DTL.nInsuranceID FROM bl_Transaction_InsPlan " +
                            " INNER JOIN PatientInsurance_DTL ON bl_Transaction_InsPlan.nInsuranceID = PatientInsurance_DTL.nInsuranceID " +
                            " WHERE bl_Transaction_InsPlan.nClaimNo = " + ClaimNumber + " and nResponsibilityNo = " + ResponsibilityNo;

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                { _insuranceID = Convert.ToInt64(_retVal); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _insuranceID;
        }

        public static Int64 GetClaimInsuranceIDRevised(Int64 TransactionMasterID, Int64 TransactionID, Int64 ResponsibilityNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _sqlQuery = string.Empty;
            Int64 _insuranceID = 0;
            Object _retVal = null;

            try
            {
                _sqlQuery = " SELECT PatientInsurance_DTL.nInsuranceID FROM BL_Claim_Insurance WITH ( NOLOCK ) " +
                " INNER JOIN PatientInsurance_DTL WITH ( NOLOCK ) ON BL_Claim_Insurance.nPatientID = PatientInsurance_DTL.nPatientID  " +
                "                                              AND BL_Claim_Insurance.nInsuranceID = PatientInsurance_DTL.nInsuranceID  " +
                "                                              AND BL_Claim_Insurance.nContactID = PatientInsurance_DTL.nContactID  " +
                " WHERE   dbo.BL_Claim_Insurance.nTransactionMasterID = " + TransactionMasterID +
                " AND BL_Claim_Insurance.nTransactionID = " + TransactionID + " AND nResponsibilityNo = " + ResponsibilityNo;

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                { _insuranceID = Convert.ToInt64(_retVal); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _insuranceID;
        }
        
        public static Int64 GetLastUnclosedDate()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            object _retVal = null;
            Int64 _closeDate = 0;

            try
            {
                _sqlQuery = " SELECT ISNULL(MAX(nCloseDayDate),0) as LastCloseDate FROM BL_CloseDays WITH (NOLOCK) ";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                {
                    DateTime dt = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_retVal));
                    dt = dt.AddDays(1);

                    _closeDate = gloDateMaster.gloDate.DateAsNumber(dt.ToString());
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _closeDate;
        }

        public static Int64 GetEOBOriginalPaymentId(Int64 @ContactID, Int64 @InsuranceID, Int64 TransactionMasterID, Int64 TransactionID, string sMainClaimNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _nEOBPaymentID = 0;
            Object _retVal = null;



            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@ContactID", @ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@InsuranceID", @InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionMasterID", TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sMainClaimNo", sMainClaimNo, ParameterDirection.Input, SqlDbType.VarChar);


                oParameters.Add("@nEOBPaymentID", null, ParameterDirection.Output, SqlDbType.BigInt);
                _retVal = oDB.ExecuteScalar("BL_SELECT_Original_Check_V2", oParameters);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                { _nEOBPaymentID = Convert.ToInt64(_retVal); }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_retVal != null) { _retVal = null; }


            }
            return _nEOBPaymentID;
        }        

        public static DataSet FillInsuranceRefund(Int64 nRefundID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet _dsReservesRefund = new DataSet();

            try
            {
                oParameters.Add("@nRefundID", nRefundID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),                
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Insurance_Refund_V2", oParameters, out _dsReservesRefund);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _dsReservesRefund;
        }

        public static DataRow GetBillingTransactions(Int64 TransactionID, Int64 TrackTransactionID)
        {
            DataTable _dtBillingTransaction = null;
            DataRow _drBillingTransaction = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTrackTransactionID", TrackTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("BL_SELECT_PaymentTransaction_MST_Tracking", oParameters, out _dtBillingTransaction);
                oDB.Disconnect();

                if (_dtBillingTransaction != null && _dtBillingTransaction.Rows.Count > 0)
                {
                    _drBillingTransaction = _dtBillingTransaction.Rows[0];
                }

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

            return _drBillingTransaction;
        }

        public static DataRow GetBillingHoldNote(Int64 TransactionMasterID, Int64 TransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;

            DataTable _dtHoldNotes = null;
            DataRow _drHoldNote = null;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nTransactionMasterID", TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("BL_Select_HoldDetails", oParameters, out _dtHoldNotes);
                oDB.Disconnect();

                if (_dtHoldNotes != null && _dtHoldNotes.Rows.Count > 0)
                {
                    _drHoldNote = _dtHoldNotes.Rows[0];
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_dtHoldNotes != null) { _dtHoldNotes.Dispose(); }
            }
            return _drHoldNote;
        }

        public static DataRow GetEOBOriginalPaymentId(string CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            DataTable _dtEOBReservePayment = null;
            DataRow _drEOBReservePayment = null;

            try
            {
                _sqlQuery = " select top(1) Credits.nCreditID As nEOBPaymentID ,sReceiptNo AS sCheckNumber,dtReceiptDate  as CheckDate, dtCloseDate As closedate  " +
                            "  from  Credits  WITH(NOLOCK) inner join Credits_EXT on Credits.nCreditID = Credits_EXT.nCreditID   where " +
                            " isnull(bIsPaymentVoid,0) = 0 AND  Credits.nCreditID in (SELECT ConvertedChar FROM dbo.SplitString('" + CreditID + "',',' ))  AND Credits_EXT.nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "" +
                            " order by dtCloseDate DESC, Credits_EXT.dtCreatedDateTime DESC";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtEOBReservePayment);
                oDB.Disconnect();

                if (_dtEOBReservePayment != null && _dtEOBReservePayment.Rows.Count > 0)
                {
                    _drEOBReservePayment = _dtEOBReservePayment.Rows[0];
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtEOBReservePayment != null) { _dtEOBReservePayment.Dispose(); }
            }

            return _drEOBReservePayment;
        }

        public static DataRow GetInsurancePaymentLogDetails(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            DataTable _dtPaymentLog = null;
            DataRow _drPaymentLog = null;

            try
            {

                oParameters.Clear();
                oParameters.Add("@nCreditID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_ViewInsurancePayment_V2", oParameters, out _dtPaymentLog);
                oDB.Disconnect();

                if (_dtPaymentLog != null && _dtPaymentLog.Rows.Count > 0)
                {
                    _drPaymentLog = _dtPaymentLog.Rows[0];
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _drPaymentLog;
        }

        public static DataRow GetEOBPaymentMST(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;

            DataTable dtCreditMST = null;
            DataRow drCreditMst = null;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nCreditID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_CreditMST_V2", oParameters, out dtCreditMST);
                oDB.Disconnect();

                if (dtCreditMST != null && dtCreditMST.Rows.Count > 0)
                {
                    drCreditMst = dtCreditMST.Rows[0];
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (dtCreditMST != null) { dtCreditMST.Dispose(); }
            }
            return drCreditMst;
        }

        internal static DataTable GetUseReserveCreditEntry(long EOBPaymentID)
        {

            string _sqlQuery = string.Empty;
            DataTable _dtEOBPaymentMST = new DataTable();

            try
            {
                _sqlQuery = "select nCreditsDTL_ID,Dtl.nCreditID,nCreditsRef_ID,nReserveRef_ID,dAmount,nEntryType,sEntryDesc,nInsCompanyID,Reserves.dtCloseDate  from credits_dtl Dtl inner join  Reserves on Dtl.nReserveRef_ID =Reserves.nReserveID where nEntryType=10 AND Dtl.bIsPaymentVoid =0 AND  Dtl.nCreditID=" + EOBPaymentID.ToString();
                SqlDataAdapter SqlDA = new SqlDataAdapter(_sqlQuery, gloGlobal.gloPMGlobal.DatabaseConnectionString);
                SqlDA.Fill(_dtEOBPaymentMST);
                if (SqlDA != null) { SqlDA.Dispose(); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return _dtEOBPaymentMST;

            //throw new NotImplementedException();
        }

        public static DataTable GetCurrentTransactionAllocationInfo(Int64 _nBillingTransactionID, Int64 _nCreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtCurrentTransactionAllocation = null;

            try
            {
                if (_nCreditID != 0)
                {
                    oParameters.Add("@BillingTransactionID", _nBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@CreditID", _nCreditID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Connect(false);
                    oDB.Retrive("GetCurrentTransactionAllocation_V2", oParameters, out _dtCurrentTransactionAllocation);
                    oDB.Disconnect();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }

            }
            return _dtCurrentTransactionAllocation;
        }
        
        public static DataTable GetBillingTransactionLines(Int64 InsContactID, Int64 InsPlanID, Int64 TransactionID, Int64 TransactionDetailID, Int64 PatientID, Int64 TrackingTransactionID)
        {
            DataTable _dtBillingTransactionLines = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();


                oParameters.Add("@nInsContactID", InsContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nInsPlanID", InsPlanID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionDetailID", TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTrackingTransactionID", TrackingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("BL_Select_BillingTransactions_InsurancePayment_V2", oParameters, out _dtBillingTransactionLines);
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

            return _dtBillingTransactionLines;
        }

        public static DataTable GetBillingTransactionLine_ReasonCodes(Int64 TransactionID, Int64 TransactionDetailID, Int64 LastEOBId)
        {
            DataTable _dtBillingTransactionLineReasonCodes = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();


                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionDetailID", TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nLastEOBID", LastEOBId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_Get_LastPaymentLineReasonCodes", oParameters, out _dtBillingTransactionLineReasonCodes);
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

            return _dtBillingTransactionLineReasonCodes;
        }
        
        public static DataTable GetUniqueIDs(Int64 DebitLinesCount)
        {
            DataTable _dtUniqueIDs = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oParameters.Clear();
                oDB.Connect(false);
                oParameters.Add("@IDCount", DebitLinesCount, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_GetUniqueIDS", oParameters, out _dtUniqueIDs);
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

            return _dtUniqueIDs;
        }

        public static DataTable GetCorrectionRefList(decimal CorrectionAmount, Int64 PatientID, Int64 BillingTransactionID, Int64 BillingTransactionDetailID, Int64 PatientInsuranceID, Int64 ContactInsuranceID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            DataTable _dtCorrectionRef = null;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@CorrectionAmount", CorrectionAmount, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                oParameters.Add("@nBillingTransactionID", BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//   numeric(18,0),
                oParameters.Add("@nBillingTransactionDetailID", BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0)
                oParameters.Add("@nInsuranceID", PatientInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),
                oParameters.Add("@nContactID", ContactInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oDB.Retrive("BL_SELECT_EOBInsCorrectionAmountList_V2", oParameters, out _dtCorrectionRef);

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
            return _dtCorrectionRef;
        }

        public static DataTable GetInsurancePendingChecks(Int64 InsuranceCompanyID, DateTime CloseDate, Int64 UserID, bool ShowCompletedOnly, bool ShowHidden)
        {
            DataTable _dtPendingChecks = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oParameters.Clear();

                if (InsuranceCompanyID != 0)
                { oParameters.Add("@InsuranceCompanyID", InsuranceCompanyID, ParameterDirection.Input, SqlDbType.BigInt); }
                else
                { oParameters.Add("@InsuranceCompanyID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt); }

                if (UserID != 0)
                { oParameters.Add("@UserID", UserID, ParameterDirection.Input, SqlDbType.BigInt); }
                else
                { oParameters.Add("@UserID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt); }

                if (CloseDate != DateTime.MinValue)
                { oParameters.Add("@dtCloseDate", CloseDate, ParameterDirection.Input, SqlDbType.DateTime); }
                else
                { oParameters.Add("@dtCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime); }

                oParameters.Add("@ShowCompletedOnly", ShowCompletedOnly, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@ShowHidden", ShowHidden, ParameterDirection.Input, SqlDbType.Bit);

                oDB.Connect(false);
                oDB.Retrive("BL_LoadPendingChecks_V2", oParameters, out _dtPendingChecks);
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

            return _dtPendingChecks;
        }

        public static DataTable GetInsurancePaymentLog(Int64 InsuranceCompanyID, string PaymentTrayIDs, Int64 UserID, string CheckNumber, DateTime PaymentDate, DateTime CloseDate,string CheckAmount)
        {
            DataTable _dtPaymentLog = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oParameters.Clear();

                if (InsuranceCompanyID != 0)
                { oParameters.Add("@InsuranceCompanyID", InsuranceCompanyID, ParameterDirection.Input, SqlDbType.BigInt); }
                else
                { oParameters.Add("@InsuranceCompanyID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt); }

                if (PaymentTrayIDs != "")
                { oParameters.Add("@PaymentTrayIDs", PaymentTrayIDs, ParameterDirection.Input, SqlDbType.VarChar); }
                else
                { oParameters.Add("@PaymentTrayIDs", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar); }

                if (UserID != 0)
                { oParameters.Add("@UserID", UserID, ParameterDirection.Input, SqlDbType.BigInt); }
                else
                { oParameters.Add("@UserID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt); }

                if (CheckNumber != "")
                { oParameters.Add("@CheckNumber", CheckNumber, ParameterDirection.Input, SqlDbType.VarChar); }
                else
                { oParameters.Add("@CheckNumber", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar); }

                if (CloseDate != DateTime.MinValue)
                { oParameters.Add("@CloseDate", CloseDate, ParameterDirection.Input, SqlDbType.DateTime); }
                else
                { oParameters.Add("@CloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime); }

                if (PaymentDate != DateTime.MinValue)
                { oParameters.Add("@PaymentDate", PaymentDate, ParameterDirection.Input, SqlDbType.DateTime); }
                else
                { oParameters.Add("@PaymentDate", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime); }

                decimal checkAmtDecimalValue = 0;
                if (CheckAmount.Trim() != "" && Decimal.TryParse(CheckAmount, out checkAmtDecimalValue) == true)
                { oParameters.Add("@CheckAmount", checkAmtDecimalValue, ParameterDirection.Input, SqlDbType.Decimal); }
                else
                { oParameters.Add("@CheckAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal); }

                if (InsuranceCompanyID != 0 || UserID != 0 || PaymentTrayIDs != "" || CheckNumber != "" || CloseDate != DateTime.MinValue || PaymentDate != DateTime.MinValue)
                {
                    oDB.Connect(false);
                    oDB.Retrive("BL_LoadInsurancePaymentLog_V2", oParameters, out _dtPaymentLog);
                    oDB.Disconnect();
                }
                else
                {
                    oDB.Connect(false);
                    oDB.Retrive("BL_LoadInsurancePaymentLog_V2", oParameters, out _dtPaymentLog);
                    oDB.Disconnect();
                }
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

            return _dtPaymentLog;
        }

        public static DataTable GetInsuranceReservesUsed(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtReserves = null;
            try
            {

                oParameters.Clear();
                oParameters.Add("@nCreditID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_InsuranceIndividualReserveUsed_V2", oParameters, out _dtReserves);
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
            return _dtReserves;
        }

        public static DataTable GetDefaultPaymentTrayDescription()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _strQuery = "";
            DataTable dtDefaultPaymentTrayData = null;
            try
            {
                _strQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID, ISNULL(sDescription,'') AS sDescription FROM BL_CloseDayTray " +
                            " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                            " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + gloGlobal.gloPMGlobal.UserID + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID;
                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dtDefaultPaymentTrayData);
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return dtDefaultPaymentTrayData;
        }

        public static DataTable GetDefaultChargeTrayDescription()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _strQuery = "";
            DataTable dtDefaultChargeTrayData = null;
            try
            {
                _strQuery = " SELECT ISNULL(nChargeTrayID,0) As nChargeTrayID, ISNULL(sDescription,'') AS sDescription FROM BL_ChargesTray " +
                            " WHERE nChargeTrayID IS NOT NULL AND sDescription IS NOT NULL AND nChargeTrayID > 0 " +
                            " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + gloGlobal.gloPMGlobal.UserID + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID;
                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dtDefaultChargeTrayData);
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return dtDefaultChargeTrayData;
        }

        public static DataTable getVoidData(Int64 _nrefundid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _strQuery = "";
            DataTable dtVoidData = null;
            try
            {
                _strQuery = "SELECT bIsPaymentVoid AS bisvoid,dbo.gloGetDate() AS dtVoidDateTime,Refunds.sVoidUserName as sVoidUserName,  sPaymentVoidTrayDesc AS nVoidTrayDescription,dtPaymentVoidCloseDate AS nVoidCloseDate FROM Refunds WITH (NOLOCK) INNER JOIN dbo.Credits WITH (NOLOCK) ON  dbo.Refunds.nCreditID = dbo.Credits.nCreditID  WHERE isnull(sVoidNote ,'') <> '' and nRefundID=" + _nrefundid;
                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dtVoidData);
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return dtVoidData;
        }
        public static DataTable GetInsuranceTakeback(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtTakeBacks = null;
            try
            {

                oParameters.Clear();
                oParameters.Add("@nCreditID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_GetCheckTakeBacks_V2", oParameters, out _dtTakeBacks);
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
            return _dtTakeBacks;
        }

        public static DataTable GetInsuranceReservesAvailable(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtReserves = null;

            try
            {
                string _sqlQuery = "select * from view_InsuranceToReserves_V2 where nCreditID = " + CreditID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtReserves);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _dtReserves;
        }

        public static DataTable GetVoidedInsurancePayment(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtPaymentVoid = null;

            try
            {
                string _sqlQuery = "SELECT sUserName,dbo.CONVERT_TO_DATE(nVoidCloseDate) AS nVoidCloseDate,sNoteDescription FROM BL_EOBPaymentVoid_Notes WITH (NOLOCK) where nEOBPaymentID = " + CreditID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtPaymentVoid);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _dtPaymentVoid;
        }

        public static DataTable GetEOBPaymentSummary(Int64 CreditID)
        {
            DataTable _dtEOBPayment = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nEOBPaymentID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBType", PaymentEntryTypeV2.InsuracePayment.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_EOBSummary_Revised_V2", oParameters, out _dtEOBPayment);
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

            return _dtEOBPayment;
        }

        public static DataTable GetInsurancePaymentRefundLog(Int64 InsuranceCompanyID, string PaymentTrayIDs, Int64 UserID, string CheckNumber, Int64 PaymentDate, Int64 CloseDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtPaymentLog = null;

            try
            {
                string _sqlQuery = "SELECT CloseDate,Tray,Company,CheckNumber,PaymentDate,Amount,sNoteDescription,[User Name],nEOBPaymentID,Status, RefundDateTime,nRefundId,nPayerID "
                                   + " FROM view_InsuranceCompanyReFunds_V2 where nClinicID = " + gloGlobal.gloPMGlobal.ClinicID;

                if (InsuranceCompanyID != 0)
                { _sqlQuery += " AND nPayerID = " + InsuranceCompanyID; }

                if (PaymentTrayIDs != "")
                { _sqlQuery += " AND nPaymentTrayID IN (" + PaymentTrayIDs + ") "; }

                if (UserID != 0)
                { _sqlQuery += " AND nUserID = " + UserID; }

                if (CheckNumber != "")
                { _sqlQuery += " AND CheckNumber = '" + CheckNumber.Replace("'", "''") + "'"; }

                if (PaymentDate != 0)
                { _sqlQuery += " AND PaymentDate = CONVERT(datetime,'" + PaymentDate + "')"; } //{ _sqlQuery += " AND PaymentDate = dbo.CONVERT_TO_DATE(convert(varchar,'" + PaymentDate + "'))"; }

                if (CloseDate != 0)
                { _sqlQuery += " AND CloseDate >= CONVERT(datetime,'" + CloseDate + "')"; } //{ _sqlQuery += " AND CloseDate >=  dbo.CONVERT_TO_DATE(convert(varchar,'" + CloseDate + "'))"; }

                _sqlQuery = _sqlQuery + " order by CloseDate,RefundDateTime desc";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtPaymentLog);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _dtPaymentLog;
        }

        public static DataTable RefundedCheckDetails(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtRefundDetails = null;
            string _sqlQuery = "";
            try
            {
                if (CreditID > 0)
                {
                    oDB.Connect(false);
                    _sqlQuery = " SELECT " +
                                 " ISNULL(sRefundTo,'') AS RefundTo, " +
                                 " ISNULL(nRefundAmount,0) AS RefundAmt, " +
                                 " ISNULL(Credits.sReceiptNo,'') AS RefundCheckNo, " +
                                 " dtReceiptDate AS RefundCheckDate " +
                                 " FROM Refunds WITH (NOLOCK) " +
                                 " INNER JOIN " +
                                 " Credits WITH (NOLOCK) " +
                                 " ON Refunds.nCreditID = Credits.nCreditID " +
                                 " WHERE Refunds.nCreditID " +
                                 " IN  ( " +
                                 "    SELECT nCreditID FROM dbo.Credits_DTL WITH (NOLOCK) " +
                                 "    WHERE nCreditsRef_ID = " + CreditID +
                                 "    AND nEntryType = 5 " +
                                 " ) " +
                                 " AND (Credits.bIsPaymentVoid IS NULL OR Credits.bIsPaymentVoid = 0) ";
                    oDB.Retrive_Query(_sqlQuery, out _dtRefundDetails);
                    oDB.Disconnect();
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dtRefundDetails;
        }

        public static DataTable GetEOBPayment(Int64 CreditID, Int64 EOBID)
        {
            DataTable _dtEOBPayment = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nCreditID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBID", EOBID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.RetriveUsingDataReader("BL_SELECT_EOB_V4", oParameters, out _dtEOBPayment);
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

            return _dtEOBPayment;
        }

        public static DataTable GetEOBPaymentID(Int64 nInsRefundID)
        {
            DataTable _dtEOBPaymentID = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                oDB.Connect(false);
                // string _sqlQuery = " select nEOBPaymentID from BL_EOBInsurance_Refund WITH (NOLOCK) where nrefundID=" + nInsRefundID + " ";
                string _sqlQuery = " select nCreditID from Refunds WITH (NOLOCK) where nrefundID=" + nInsRefundID + " ";
                oDB.Retrive_Query(_sqlQuery, out _dtEOBPaymentID);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dtEOBPaymentID;
        }

        public static DataTable FillInsuranceCompany(Int64 _nInsuranceID, Int64 _ClinicID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtInsCompanies = null;
            string _sqlQuery = "";
            try
            {
                _sqlQuery = "SELECT ISNULL(nID,0) AS nID,ISNULL(sCode,'') AS sCode, " +
                 " ISNULL(sDescription,'') AS sDescription from Contacts_InsuranceCompany_MST WITH (NOLOCK) " +
                 " WHERE nClinicID = " + _ClinicID + " AND nID = " + _nInsuranceID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtInsCompanies);
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _dtInsCompanies;
        }

        public static DataTable getPatientClaimNos(Int64 nPatientID)
        {
            DataTable _dtClaimNo = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            try
            {
                string _strSql = "";
                _strSql = "SELECT CONVERT(VARCHAR,BL_Transaction_Claim_MST.nTransactionMasterID )+ '-' + CONVERT(VARCHAR,BL_Transaction_Claim_MST.nTransactionID) AS ID, "
                         + " dbo.GetSubClaimNumber(BL_Transaction_Claim_MST.nClaimNo,BL_Transaction_Claim_MST.nSubClaimNo ,BL_Transaction_Claim_MST.sMainClaimNo,5) as Claim  "
                         + " FROM BL_Transaction_Claim_MST  WITH (NOLOCK) WHERE (LEFT(nSubClaimNo,1)<> '-')  AND nPatientID = " + nPatientID + " ORDER BY dtCreateDate DESC";

                oDB.Connect(false);
                oDB.Retrive_Query(_strSql, out _dtClaimNo);
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return _dtClaimNo;
        }

        public static DataTable getValidClaimDetails(Int64 MainClaimNumber, string SubClaimNumber)
        {
           
            DataTable dtTransactionID = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                string _strSql = "";

                if (Convert.ToString(SubClaimNumber) == "")
                {
                    _strSql = " SELECT BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                    + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                    + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                    + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient  WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                    + " WHERE nClaimNo = " + MainClaimNumber + "";
                }
                else
                {
                    _strSql = " SELECT BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                    + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                    + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                    + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient  WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                    + " WHERE nClaimNo = " + MainClaimNumber + " AND nSubClaimNo = " + SubClaimNumber;
                }
                oDB.Connect(false);
                oDB.Retrive_Query(_strSql, out dtTransactionID);
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
             finally
             {
                 if (oDB != null)
                 {
                     oDB.Dispose();
                 }
             }
            return dtTransactionID;
        }

        public static DataTable getRefundCloseDate(Int64 _nRefEOBPaymentID)
        {

            DataTable dtRefCloseDate = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                //string strQuery = "SELECT isnull(BL_EOBPayment_DTL.nCloseDate,0) as nCloseDate FROM BL_EOBPayment_DTL WITH (NOLOCK) LEFT OUTER JOIN " +
                //" dbo.BL_EOBPayment_MST WITH (NOLOCK) ON dbo.BL_EOBPayment_DTL.nEOBPaymentID = dbo.BL_EOBPayment_MST.nEOBPaymentID WHERE (BL_EOBPayment_DTL.nPaymentType = 1) " +
                //" AND (BL_EOBPayment_DTL.nPaymentSubType = 9) AND(dbo.BL_EOBPayment_DTL.nPaySign = 2) AND (ISNULL(BL_EOBPayment_MST.nVoidType,0) NOT IN (3, 5, 9, 8)) " +
                //" AND BL_EOBPayment_DTL.nEOBPaymentID = (SELECT TOP 1 BL_EOBPayment_DTL.nRefEOBPaymentID from BL_EOBPayment_DTL WITH (NOLOCK) where nEOBPaymentID=" + _nRefEOBPaymentID + " AND nPaySign = 2 ORDER BY nCloseDate)";
                string strQuery = "SELECT TOP 1 isnull(dbo.Credits.dtCloseDate,'') as nCloseDate FROM Credits WITH (NOLOCK) WHERE Credits.nCreditID  " +
                " IN (SELECT Credits_DTL.nCreditsRef_ID FROM Credits_DTL WHERE nCreditID =" + _nRefEOBPaymentID + ") ORDER BY nclosedate";
                //oDB.Retrive_Query("Select ISNULL(nCloseDate,0) as nclosedate from view_SelectPaymentCloseDate where nEOBPaymentID = (SELECT BL_EOBPayment_DTL.nRefEOBPaymentID from BL_EOBPayment_DTL where nEOBPaymentID=" + _nRefEOBPaymentID + " AND nPaySign = 2 )", out _dtReserves);
                oDB.Connect(false);
                oDB.Retrive_Query(strQuery, out dtRefCloseDate);
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return dtRefCloseDate;
        }

        public static DataTable getInsuranceAvailableReserves(Int64 _InsuranceCompanyID, Int64 _ClinicID, Int64 _nResEOBPaymentDetailID)
        {
            DataTable _dtReserves = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@nInsuranceID", _InsuranceCompanyID, ParameterDirection.Input, SqlDbType.BigInt);//NUMERIC(18,0)
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oParameters.Add("@nEOBPaymentID", _nResEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Reserve_InsuranceDetails_V2", oParameters, out _dtReserves);
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return _dtReserves;
        }

        public static DataTable GetLastPaymentMade(Int64 EOBPaymentID)
        {
            DataTable _dtCheckBalance = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();


            try
            {
                if (EOBPaymentID != 0)
                {
                    //_IsFromSave = true;
                    oParameters.Add("@nPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Connect(false);
                    oDB.Retrive("BL_GetInsurancePaymentCheckBalance_V2", oParameters, out _dtCheckBalance);
                    oDB.Disconnect();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }

            }
            return _dtCheckBalance;
        }

        public static DataTable getValidRefundClaimDetails(Int64 MainClaimNumber, string SubClaimNumber)
        {
            DataTable dtTransactionID = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                string _strSql = "";


                if (SubClaimNumber == "")
                {
                    _strSql = " SELECT top(1) BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                        + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                        + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                        + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                        + " WHERE nClaimNo = " + MainClaimNumber + " order by dtcreateDate asc";
                }
                else
                {
                    _strSql = " SELECT BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                            + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                            + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                            + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                            + " WHERE nClaimNo = " + MainClaimNumber + " AND nSubClaimNo = " + SubClaimNumber;
                }
                oDB.Connect(false);
                oDB.Retrive_Query(_strSql, out dtTransactionID);
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
             finally
             {
                 if (oDB != null)
                 {
                     oDB.Dispose();
                 }
             }
            return dtTransactionID; 
        }

        public static DataTable GetPatientAccountsForInsPmtVoid(Int64 CreditID)
        {
            DataTable dtAccountIDs = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                string _strSql = "";
                _strSql =
                    "SELECT DISTINCT " +
                    "       PA_Accounts_Patients.nPAccountID, BL_Transaction_MST.nPatientID " +
                    " FROM    dbo.Debits " +
                    "        INNER JOIN dbo.BL_Transaction_MST ON dbo.Debits.nBillingTransactionID = dbo.BL_Transaction_MST.nTransactionID " +
                    "        INNER JOIN dbo.Patient ON dbo.BL_Transaction_MST.nPatientID = dbo.Patient.nPatientID " +
                    "        INNER JOIN dbo.PA_Accounts_Patients ON dbo.Patient.nPatientID = dbo.PA_Accounts_Patients.nPatientID " +
                    " WHERE   nCreditID = " + CreditID +
                    "        AND nEntryType IN ( 4, 8, 11 ) ";
                oDB.Connect(false);
                oDB.Retrive_Query(_strSql, out dtAccountIDs);
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return dtAccountIDs;
        }

        public static DataTable GetPatientAccountsForPatPmtVoid(Int64 CreditID)
        {
            DataTable dtAccountIDs = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                string _strSql = "";
                _strSql =
                    " SELECT DISTINCT      " + 
                    " PA_Accounts_Patients.nPAccountID , " +
                    " Credits.nPayerID AS nPatientID " +    
                    " FROM    dbo.Credits " +      
                    " LEFT OUTER JOIN dbo.Patient ON dbo.Credits.nPayerID = dbo.Patient.nPatientID " +       
                    " LEFT OUTER JOIN dbo.PA_Accounts_Patients ON dbo.Patient.nPatientID = dbo.PA_Accounts_Patients.nPatientID " +      
                    " WHERE   nCreditID = " + CreditID +      
                    " AND nEntryType IN ( 6,9 ) ";
                oDB.Connect(false);
                oDB.Retrive_Query(_strSql, out dtAccountIDs);
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return dtAccountIDs;
        }

        public static Int64 GetPatientAccountsForPatRefundVoid(Int64 CreditID)
        {
            Int64 _nPAccountID = 0;
            Object _retVal = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                string _strSql = "";
                _strSql =
                   "SELECT  Credits.nPAccountID , dbo.Refunds.nPayerID " +
                   " FROM    dbo.Refunds " +
                   "         INNER JOIN dbo.Credits ON dbo.Refunds.nCreditID = dbo.Credits.nCreditID " +
                   " WHERE   dbo.Refunds.nCreditID = " + CreditID;
                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_strSql);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                { _nPAccountID = Convert.ToInt64(_retVal); }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return _nPAccountID;
        }

        public static string FillPaymentTray(Int64 _UserId, Int64 _ClinicID)
        {
            //Resolved bug no. 92088::Insurance Payment>>Refund>>Application shows Exception on selected insurance company save
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString); ;
            gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Int64 _lastselectedTrayId = 0;
            Object _retVal = null;
            string sPaymentTray = ""; 

            try
            {
                #region " .... Get the last selected Payment tray ... "
                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                Object _retSettingValue = null;
                oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", _UserId, _ClinicID, out _retSettingValue);
                oSettings.Dispose();

                if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                { _lastselectedTrayId = Convert.ToInt64(_retSettingValue); }

                #endregion " .... Get the last selected Payment tray ... "

                #region " ... Get the default Payment Tray .... "

                _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray WITH (NOLOCK) " +
               " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
               " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + _UserId + " AND nClinicID = " + _ClinicID + "";
                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                { _defaultTrayId = Convert.ToInt64(_retVal); }

                #endregion " ... Get the default Payment Tray .... "

                //...Load the last selected tray if present or else load the default tray
                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oDB.Connect(false);
                _retVal = new object();
                if (_lastselectedTrayId > 0)
                {
                    _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + _lastselectedTrayId + " and ISNULL(bIsActive,0) = 1 AND nClinicID = " + _ClinicID + "");
                    if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                    {
                        sPaymentTray = _retVal.ToString() + "~" + _lastselectedTrayId.ToString();
                    }
                    else
                    {
                        _lastselectedTrayId = 0;
                        sPaymentTray = "";
                    }
                }
                else
                {
                    _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + _ClinicID + "");
                    if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                    {
                        sPaymentTray = _retVal.ToString() + "~" + _defaultTrayId.ToString();
                    }
                }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
                if (ogloValidateUser != null)
                {
                    ogloValidateUser.Dispose();
                    ogloValidateUser = null;
                }
            }
            return sPaymentTray;
        }

        public static string GetReasonDescription(string Code)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            string _description = "";
            Object _retVal = null;

            try
            {
                _sqlQuery = " SELECT DISTINCT ISNULL(sDescription,'') AS Description " +
                " FROM BL_ReasonCodes_MST WITH(NOLOCK)  where UPPER(ISNULL(sGroupCode,''))+UPPER(ISNULL(sCode,'')) = '" + Code.Trim().ToUpper() + "' " +
                " AND (bIsBlock IS NULL OR bIsBlock = 'false') AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + " ";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && _retVal.ToString().Trim() != "")
                { _description = Convert.ToString(_retVal).Trim(); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _description;
        }

        public static string GetNewOpenCloseDate_V2(Int64 nEOBPaymentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _sqlQuery = string.Empty;
            string _CloseDate = "";
            Object _retVal = null;

            try
            {
                _sqlQuery = "select top (1)  dtCloseDate   from Debits where nCreditID = " + nEOBPaymentID + " order by dtCloseDate desc";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_retVal == null || Convert.ToString(_retVal) == string.Empty)
                {
                    _sqlQuery = "select top (1)  dtCloseDate   from Credits where nCreditID = " + nEOBPaymentID + " order by dtCloseDate desc";
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);

                }
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal) != "")
                { _CloseDate = _retVal.ToString(); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _CloseDate;
        }

        public static string GetUserWiseCloseDay(Int64 nUserID, CloseDayType eType)
        {
            DataTable dtCloseDay = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
            String _sqlQuery = "";
            String _result = String.Empty;

            try
            {
                _sqlQuery = "SELECT nCloseDayDate FROM BL_ChargePayment_CloseDays WITH (NOLOCK) where nUserID = " + nUserID
                               + " AND CONVERT(VARCHAR(8),dtCloseDateTime,112) = CONVERT(VARCHAR(8),dbo.gloGetDate(),112)"
                               + " AND	nCloseDayType = " + eType.GetHashCode()
                               + " AND bIsActive = 1";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dtCloseDay);
                oDB.Disconnect();

                if (dtCloseDay != null && dtCloseDay.Rows.Count > 0)
                {
                    _result = Convert.ToString(dtCloseDay.Rows[0][0]);
                }

                if (_result != null && Convert.ToString(_result).Trim() != "")
                {
                    try
                    {
                        _result = gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(_result)); //Convert.ToDateTime(Convert.ToString(_result).Trim()).ToString("MM/dd/yyyy"); 
                    }
                    catch //(Exception ex)
                    {
                        _result = "";
                    }
                }
                else
                { _result = ""; }

                if (_result.Trim() != "")
                {
                    if (ogloBilling.IsDayClosed(Convert.ToDateTime(_result.Trim())) == true)
                    { _result = ""; }
                }

            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
            return _result;
        }

        public static string GetNextActions()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _payActionStatus = "";
            DataTable _dtPayActStatus = null;
            string _sqlQuery = "";
            string _conCodeDesc = "";

            try
            {
                _sqlQuery = " SELECT ISNULL(nID,0) AS ID,ISNULL(sCode,'') AS Code, " +
                " ISNULL(sDescription,'') AS Description, ISNULL(nIsSystem,'false') AS IsSystem, " +
                " ISNULL(nIsBlock,'false') AS nIsBlock, ISNULL(nActionID,0) AS nActionID " +
                " FROM BL_EOBPayment_ActionStatus " +
                " WHERE nID > 0 AND sCode IS NOT NULL AND sDescription IS NOT NULL AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + " " +
                " ORDER BY nID";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtPayActStatus);
                oDB.Disconnect();

                if (_dtPayActStatus != null && _dtPayActStatus.Rows.Count > 0)
                {
                    _payActionStatus = "|";

                    for (int i = 0; i < _dtPayActStatus.Rows.Count; i++)
                    {
                        _conCodeDesc = "";

                        if (Convert.ToString(_dtPayActStatus.Rows[i]["Code"]).Trim() != "" && Convert.ToString(_dtPayActStatus.Rows[i]["Description"]).Trim() != "")
                        {
                            _conCodeDesc = Convert.ToString(_dtPayActStatus.Rows[i]["Code"]).Trim().ToUpper() + "-" + Convert.ToString(_dtPayActStatus.Rows[i]["Description"]).Trim().ToUpper() + "|";
                            _payActionStatus += _conCodeDesc;
                        }
                    }

                    _payActionStatus = _payActionStatus.TrimEnd('|');
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); }
                if (_dtPayActStatus != null) { _dtPayActStatus.Dispose(); }
                if (_sqlQuery != null) { _sqlQuery = null; }
            }

            return _payActionStatus;
        }

        public static string GetInsuranceParties(Int64 TransactionMasterID, Int64 TransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtClaimInsurances = null;
            string _payPartyCode = "";
            string _conCodeDesc = "";

            try
            {
                if (TransactionMasterID > 0)
                {
                    oDB.Connect(false);
                    oParameters.Clear();
                    oParameters.Add("@nTransactionMasterID", TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                    oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                    oDB.Retrive("BL_SELECT_CLAIM_INSURANCES_REVISED_V2", oParameters, out _dtClaimInsurances);
                    oDB.Disconnect();

                    if (_dtClaimInsurances != null && _dtClaimInsurances.Rows.Count > 0)
                    {
                        if (_dtClaimInsurances != null && _dtClaimInsurances.Rows.Count > 0)
                        {
                            _payPartyCode = "|";

                            for (int i = 0; i < _dtClaimInsurances.Rows.Count; i++)
                            {
                                _conCodeDesc = "";

                                if (Convert.ToString(_dtClaimInsurances.Rows[i]["nResponsibilityType"]).Trim() != ""
                                    && Convert.ToInt32(_dtClaimInsurances.Rows[i]["nResponsibilityType"]) == PayerTypeV2.Insurance.GetHashCode())
                                {
                                    _conCodeDesc = Convert.ToString(_dtClaimInsurances.Rows[i]["nResponsibilityNo"]).Trim().ToUpper() + "-" + Convert.ToString(_dtClaimInsurances.Rows[i]["InsuranceName"]).Trim().ToUpper() + "|";
                                    _payPartyCode += _conCodeDesc;
                                }
                                else if (Convert.ToString(_dtClaimInsurances.Rows[i]["nResponsibilityType"]).Trim() != ""
                                    && Convert.ToInt32(_dtClaimInsurances.Rows[i]["nResponsibilityType"]) == PayerTypeV2.Self.GetHashCode())
                                {
                                    _conCodeDesc = Convert.ToString(_dtClaimInsurances.Rows[i]["nResponsibilityNo"]).Trim().ToUpper() + "-" + "Self" + "|";
                                    _payPartyCode += _conCodeDesc;
                                }

                            }
                            //_payPartyCode += "0" + "-" + "Self" + "|";
                            _payPartyCode = _payPartyCode.TrimEnd('|');
                        }
                    }
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_dtClaimInsurances != null) { _dtClaimInsurances.Dispose(); }
            }

            return _payPartyCode;
        }

        public static string GetPaymentTrayDescription(Int64 PaymentTrayID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _description = string.Empty;
            string _sqlQuery = string.Empty;
            Object _retVal = null;

            try
            {
                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WHERE nCloseDayTrayID = " + PaymentTrayID + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "");
                if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                { _description = _retVal.ToString().Trim(); }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _description;
        }

        public static string GetClaimRemittanceRefNo(Int64 TransactionMasterID, Int64 ContactID, Int64 InsuranceID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _claimRemittanceRefNo = string.Empty;
            string _sqlQuery = string.Empty;
            Object _retVal = null;

            try
            {
                //_sqlQuery = "select isnull(sClaimRemittanceRefNo,'') as sClaimRemittanceRefNo from BL_Transaction_ClaimRemittanceRef with(nolock) INNER JOIN dbo.Debits  with(nolock) "+
                //            " ON dbo.BL_Transaction_ClaimRemittanceRef.nContactID = dbo.Debits.nContactID AND dbo.BL_Transaction_ClaimRemittanceRef.nTransactionID = dbo.Debits.nBillingTransactionID "+
                //            " where nTransactionID = " + TransactionMasterID + " and BL_Transaction_ClaimRemittanceRef.nContactID ='" + ContactID + "' and nInsuranceID = '" + InsuranceID + "' and BL_Transaction_ClaimRemittanceRef.nclinicID = '" + gloGlobal.gloPMGlobal.ClinicID + "' AND ISNULL(dbo.Debits.bIsPaymentVoid,0) = 0";
                _sqlQuery = "select isnull(sClaimRemittanceRefNo,'') as sClaimRemittanceRefNo from BL_Transaction_ClaimRemittanceRef with(nolock) where nTransactionID = " + TransactionMasterID + " and nContactID ='" + ContactID + "' and nInsuranceID = '" + InsuranceID + "' and nclinicID = '" + gloGlobal.gloPMGlobal.ClinicID + "'";
                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                { _claimRemittanceRefNo = Convert.ToString(_retVal); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _claimRemittanceRefNo;
        }

        public static bool IsRebilled(Int64 TransactionMasterID, Int64 TransactionID, Int64 ContactID, Int64 InsuranceID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            bool _IsRebilled = false;
            string _sqlQuery = string.Empty;
            Object _retVal = null;
            DataTable _dtClaimFreq = null;
            try
            {
                //_sqlQuery = " SELECT  ISNULL(bIsRebilled,0) AS IsRebilled, ISNULL(bIsReplacementClaim,0) AS bIsReplacementClaim " +
                //            " FROM    dbo.BL_Transaction_Claim_MST WITH ( NOLOCK ) " +
                //            " WHERE   nTransactionMasterID = " + TransactionMasterID +
                //            "        AND nContactID = " + ContactID +
                //            "        AND nInsuranceID = " + InsuranceID + " AND nTransactionID = " + TransactionID;

                _sqlQuery = " SELECT ISNULL(sClaimRemittanceRefNo, '') AS sClaimRemittanceRefNo,ISNULL(bIsRebilled, 0) AS IsRebilled,ISNULL(bIsReplacementClaim,0) AS bIsReplacementClaim  " +
                            "   FROM   BL_Transaction_ClaimRemittanceRef WITH ( NOLOCK )" +
                            "   INNER JOIN" +
                            "       dbo.BL_Transaction_Claim_MST WITH ( NOLOCK )" +
                            "       ON dbo.BL_Transaction_ClaimRemittanceRef.nContactID = dbo.BL_Transaction_Claim_MST.nContactID" +
                            "       AND dbo.BL_Transaction_ClaimRemittanceRef.nInsuranceID = dbo.BL_Transaction_Claim_MST.nInsuranceID" +
                            "       AND dbo.BL_Transaction_ClaimRemittanceRef.nTransactionID = dbo.BL_Transaction_Claim_MST.nTransactionMasterID" +
                            "   WHERE  BL_Transaction_ClaimRemittanceRef.nTransactionID = " + TransactionMasterID +
                            "       AND BL_Transaction_ClaimRemittanceRef.nContactID = " + ContactID +
                            "       AND BL_Transaction_ClaimRemittanceRef.nInsuranceID = " + InsuranceID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtClaimFreq);
                oDB.Disconnect();

                //if (_dtClaimFreq != null && _dtClaimFreq.Rows.Count > 0)
                //{ if (Convert.ToBoolean(_dtClaimFreq.Rows[0]["IsRebilled"]) || Convert.ToBoolean(_dtClaimFreq.Rows[0]["bIsReplacementClaim"])) { _IsRebilled = true; } }

                if (_dtClaimFreq != null && _dtClaimFreq.Rows.Count > 0)
                { if (Convert.ToBoolean(_dtClaimFreq.Rows[0]["IsRebilled"]) || Convert.ToString(_dtClaimFreq.Rows[0]["sClaimRemittanceRefNo"]) != string.Empty) { _IsRebilled = true; } }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _IsRebilled;
        }

        public static string GetFormattedClaimPaymentNumber(string NumberSize)
        {
            int _length = 0;
            _length = NumberSize.Length;
            if (_length == 1)
            {
                NumberSize = "0000" + NumberSize;
            }
            else if (_length == 2)
            {
                NumberSize = "000" + NumberSize;
            }
            else if (_length == 3)
            {
                NumberSize = "00" + NumberSize;
            }
            else if (_length == 4)
            {
                NumberSize = "0" + NumberSize;
            }
            else if (_length == 5)
            {
                //NumberSize = NumberSize;
            }
            return NumberSize;
        }

        #endregion "Get Methods"

        #region "Void Methods"

        public Int64 UpdatePatientPaymentForVoid(string VoidNote, DateTime VoidCloseDate, Int64 VoidTrayID, string VoidTrayName, Int64 TransactionId, Int64 nClinicID, Int64 nProviderID, Int64 _nTrackTrnID, Int64 nUserID, string sUserName, string sMachineName)
        {
            System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            System.Data.SqlClient.SqlCommand _sqlCommand = null;
            System.Data.SqlClient.SqlTransaction _sqlTransaction = null;
            Int64 retVal = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {

                oParameters.Clear();
                oParameters.Add("@nBillingTransactionID", TransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@dtVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@nVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sVoidTrayName", VoidTrayName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nVoidType", VoidType.ClaimVoid.GetHashCode(), ParameterDirection.Input, SqlDbType.TinyInt);
                oParameters.Add("@sVoidNote", VoidNote, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", nClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nProviderID", nProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTrackTrnID", _nTrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sMachineName", sMachineName, ParameterDirection.Input, SqlDbType.VarChar);

                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                _sqlCommand = oDB.GetCmdParameters(oParameters);
                _sqlCommand.Connection = _sqlConnection;
                _sqlCommand.Transaction = _sqlTransaction;
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.CommandText = "BL_PatientPayment_ClaimVoid_Revised_V2";
                _sqlConnection.Open();
                retVal = _sqlCommand.ExecuteNonQuery();
                _sqlConnection.Close();
            }
            catch (gloDatabaseLayer.DBException ex)
            { _sqlTransaction.Rollback(); ex.ERROR_Log(ex.ToString()); }
            catch (Exception ex)
            { _sqlTransaction.Rollback(); gloAuditTrail.gloAuditTrail.ExceptionLog("ERROR: " + ex.Message, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); }
                if (_sqlTransaction != null) { _sqlTransaction.Dispose(); }
            }
            return retVal;
        }

        public Int64 UpdateInsurancePaymentForClaimVoid(string VoidNote, DateTime VoidCloseDate, Int64 VoidTrayID, string VoidTrayName, Int64 TransactionId, Int64 nClinicID, Int64 nProviderID, Int64 _nTrackTrnID, Int64 nUserID, string sUserName, string sMachineName, Int64 nBusinessCenterID)
        {
            System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            System.Data.SqlClient.SqlCommand _sqlCommand = null;
            System.Data.SqlClient.SqlTransaction _sqlTransaction = null;
            Int64 retVal = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {

                oParameters.Clear();
                oParameters.Add("@nBillingTransactionID", TransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@dtVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@nVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sVoidTrayName", VoidTrayName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nVoidType", VoidType.ClaimVoid.GetHashCode(), ParameterDirection.Input, SqlDbType.TinyInt);
                oParameters.Add("@sVoidNote", VoidNote, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", nClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nProviderID", nProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTrackTrnID", _nTrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sMachineName", sMachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nBusinessCenterID", nBusinessCenterID, ParameterDirection.Input, SqlDbType.VarChar);
                
                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                _sqlCommand = oDB.GetCmdParameters(oParameters);
                _sqlCommand.Connection = _sqlConnection;
                _sqlCommand.Transaction = _sqlTransaction;
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.CommandText = "BL_InsurancePayment_ClaimVoid_Revised_V2";
                _sqlConnection.Open();
                retVal = _sqlCommand.ExecuteNonQuery();
                _sqlConnection.Close();
            }
            catch (gloDatabaseLayer.DBException ex)
            { _sqlTransaction.Rollback(); gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            catch (Exception ex)
            { _sqlTransaction.Rollback(); gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); }
                if (_sqlTransaction != null) { _sqlTransaction.Dispose(); }
            }
            return retVal;

        }

        public string VoidInsurancePayment(Int64 CreditID, string VoidNote, DateTime VoidCloseDate, Int64 VoidTrayID, string VoidTrayName)
        {
            System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            System.Data.SqlClient.SqlCommand _sqlCommand = null; 
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string sStatus = string.Empty;

            try
            {               
                if (VoidNote != null)
                {                    
                    oParameters.Clear();
                    oParameters.Add("@nEOBPaymentID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@dtVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Date);
                    oParameters.Add("@nVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sVoidTrayName", VoidTrayName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nVoidType", VoidType.InsurancePaymentVoid.GetHashCode(), ParameterDirection.Input, SqlDbType.TinyInt);                   
                    oParameters.Add("@nID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);                 
                    oParameters.Add("@sNoteDescription", VoidNote.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nVoidNoteType", VoidType.InsurancePaymentVoid.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oParameters.Add("@sUserName", AppSettings.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sVoidTrayDescription", VoidTrayName.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sVoidTrayCode", "", ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@Message", "", ParameterDirection.Output, SqlDbType.VarChar,1000);

                    
                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.CommandTimeout = 0;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = "BL_InsurancePaymentVoid_V2";

                    int _result = 0;
                    _sqlConnection.Open();
                    _result = _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();

                    Hashtable outPut = oDB.GetOutParamResults(_sqlCommand);
                    if (outPut != null)
                    {
                        sStatus = Convert.ToString(outPut["@Message"]);
                    }
                }                
            }
            catch (gloDatabaseLayer.DBException ex)
            { ex.ERROR_Log(ex.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog("ERROR: " + ex.Message, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); }               
            }
            return sStatus;
        }
        public string VoidInsurancePayment_V2(Int64 CreditID, string VoidNote, DateTime VoidCloseDate, Int64 VoidTrayID, string VoidTrayName)
        {
            System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            System.Data.SqlClient.SqlCommand _sqlCommand = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string sStatus = string.Empty;
            //DateTime _dtGetStart;
            //DateTime _dtGetEnd;
            DataSet _ds = null;
            object oResult = null;

            string sErrorMessage = "";
            try
            {
                if (VoidNote != null)
                {
                    oParameters.Clear();
                    oParameters.Add("@nEOBPaymentID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@dtVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Date);
                    oParameters.Add("@nVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sVoidTrayName", VoidTrayName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nVoidType", VoidType.InsurancePaymentVoid.GetHashCode(), ParameterDirection.Input, SqlDbType.TinyInt);
                    oParameters.Add("@nID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sNoteDescription", VoidNote.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nVoidNoteType", VoidType.InsurancePaymentVoid.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oParameters.Add("@sUserName", AppSettings.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sVoidTrayDescription", VoidTrayName.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sVoidTrayCode", "", ParameterDirection.Input, SqlDbType.VarChar);

                    oDB.Connect(false);
                    oDB.Retrive("BL_InsurancePaymentVoid_V2", oParameters, out _ds);
                    oDB.Disconnect();

                    if (_ds != null && _ds.Tables.Count > 0)
                    {
                        //_dt = _dtBillingTransaction.Rows[0];
                        oParameters.Clear();
                        if (_ds.Tables[1] != null && _ds.Tables[1].Rows.Count > 0)
                        {
                            oParameters.Add("@tvpValidTrans", _ds.Tables[1], ParameterDirection.Input, SqlDbType.Structured);
                            oParameters.Add("@sUserName", AppSettings.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@dtVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Date);
                            oParameters.Add("@ErrorMessage", "", ParameterDirection.Output, SqlDbType.VarChar, 5000);
                            oDB.Connect(false);
                            oDB.Execute("BL_SplitTransactionClaim_Void_V3", oParameters, out oResult);
                         
                            if (oResult != null && oResult.ToString() != "")
                            {
                                sErrorMessage = Convert.ToString(oResult);
                            }
                        }
                        else
                        {
                            sErrorMessage = "SUCCESS";
                        }
                       

                    }

                    if (sErrorMessage != "SUCCESS")
                    {
                        if (_ds != null && _ds.Tables.Count > 0)
                        {
                            if (_ds.Tables[0] != null && _ds.Tables[0].Rows.Count > 0)
                            {
                                if (_ds.Tables[0].Rows[0][0] != null)
                                {
                                    RevertVoidPayment(CreditID, Convert.ToInt64(_ds.Tables[0].Rows[0][0]));
                                }
                            }
                        }
                    }
                    //_sqlCommand = new System.Data.SqlClient.SqlCommand();
                    //_sqlCommand = oDB.GetCmdParameters(oParameters);
                    //_sqlCommand.Connection = _sqlConnection;
                    //_sqlCommand.CommandTimeout = 0;
                    //_sqlCommand.CommandType = CommandType.StoredProcedure;
                    //_sqlCommand.CommandText = "BL_InsurancePaymentVoid_V2";

                    //int _result = 0;
                    //_sqlConnection.Open();
                    //_dtGetStart = DateTime.Now;
                    //_result = _sqlCommand.ExecuteNonQuery();
                    //_dtGetEnd = DateTime.Now;
                    //Console.WriteLine("Exec_BL_InsurancePaymentVoid_V2 : " + Convert.ToString((_dtGetEnd - _dtGetStart).Seconds));
                    //_sqlConnection.Close();

                    //Hashtable outPut = oDB.GetOutParamResults(_sqlCommand);
                    //if (outPut != null)
                    //{
                    //    sStatus = Convert.ToString(outPut["@Message"]);
                    //}
                }
                
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                if (_ds != null && _ds.Tables.Count > 0)
                {
                    if (_ds.Tables[0] != null && _ds.Tables[0].Rows.Count > 0)
                    {
                        if (_ds.Tables[0].Rows[0][0] != null)
                        {
                            RevertVoidPayment(CreditID, Convert.ToInt64(_ds.Tables[0].Rows[0][0]));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("ERROR: " + ex.Message, false);
                if (_ds != null && _ds.Tables.Count > 0)
                {
                    if (_ds.Tables[0] != null && _ds.Tables[0].Rows.Count > 0)
                    {
                        if (_ds.Tables[0].Rows[0][0] != null)
                        {
                            RevertVoidPayment(CreditID, Convert.ToInt64(_ds.Tables[0].Rows[0][0]));
                        }
                    }
                }
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); }
            }
            return sErrorMessage;
        }

        public void RevertVoidPayment(Int64 EOBPaymentID, Int64 nID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nID", nID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Execute("BL_RevertPaymentVoid", oParameters);
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog("ERROR: " + ex.Message, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }

            }
        }
        public Int64 VoidInsuranceRefund(Int64 EOBPaymentID, Int64 PatientId, string PatientName, string CloseDate, string VoidNote, DateTime VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName, Int64 refundID, string VoidUserName, Int64 VoidUserID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string sErrorMessage = "";
            bool showErrorMsg = false;
            object _retVal = null;

            try
            {
                //if (EOBInsurancePaymentDetails != null)
                //{

                if (EOBPaymentID > 0)
                {
                    oParameters.Add("@nCreditID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@VoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Date);

                    oParameters.Add("@CreditVoidType", VoidTypeV2.InsurancePaymentRefundVoid.GetHashCode(), ParameterDirection.Input, SqlDbType.TinyInt);
                    oParameters.Add("@CreditVoidTrayDesc", VoidTrayName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@CreditVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);

                    oParameters.Add("@nRefundID", refundID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@RefundVoidNote", VoidNote.Replace("'", "''").Trim(), ParameterDirection.Input, SqlDbType.VarChar);

                    oParameters.Add("@ReserveVoidType", VoidTypeV2.InsurancePaymentRefundVoid.GetHashCode(), ParameterDirection.Input, SqlDbType.TinyInt);
                    oParameters.Add("@VoidUserID", VoidUserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@VoidUserName", VoidUserName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@Credits_DTLnEntryType", PaymentEntryTypeV2.InsuraceRefund.GetHashCode(), ParameterDirection.Input, SqlDbType.TinyInt);
                    oParameters.Add("@error_message", sErrorMessage, ParameterDirection.Output, SqlDbType.VarChar, 1000);

                    oDB.Connect(false);
                    oDB.Execute("BL_VoidRefund_V2", oParameters, out _retVal);
                    oDB.Disconnect();
                    if (_retVal != null)
                    { sErrorMessage = Convert.ToString(_retVal); }

                }
                if (sErrorMessage != "Sucess")
                {
                    showErrorMsg = true;
                }
                else
                {
                    showErrorMsg = false;
                }
            }
            //}
            catch //(gloDatabaseLayer.DBException ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(sErrorMessage, showErrorMsg); }
            //catch (Exception ex)
            //{ gloAuditTrail.gloAuditTrail.ExceptionLog(sErrorMessage, showErrorMsg); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }

            }
            return 0;
        }

        #endregion "Void Methods"
    }

    public class gloPatientPaymentV2
    {
        #region "Constructor"

        public gloPatientPaymentV2()
        {
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~gloPatientPaymentV2()
        {
            Dispose(false);
        }
        #endregion "Constructor"

        #region "Get Methods"

        public gloAccountsV2.PaymentCollection.PaymentPatientClaims GetBillingTransaction_PAF(Int64 PAccountId, Int64 PatientId, bool LoadZeroBalance, bool LoadBadDebtClaims = false)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtBillingTransaction = null;
            DataTable dtBillingTransactionLines = null;
            gloAccountsV2.PaymentCollection.PaymentPatientClaim oPaymentClaim = null;
            gloAccountsV2.PaymentCollection.PaymentInsuranceLine oPaymentLine = null;
            gloAccountsV2.PaymentCollection.PaymentPatientClaims oPaymentClaims = new gloAccountsV2.PaymentCollection.PaymentPatientClaims();

            try
            {
                oParameters.Clear();
                oParameters.Add("@nPatientID", PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", PAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bShowBadDebt", LoadBadDebtClaims, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_PaymentTransaction_Lines_PatPayment_V2", oParameters, out dtBillingTransactionLines);
                oDB.Disconnect();
                oParameters.Clear();

                if (dtBillingTransactionLines != null && dtBillingTransactionLines.Rows.Count > 0)
                {
                    for (int nTrnLineCntr = 0; nTrnLineCntr < dtBillingTransactionLines.Rows.Count; nTrnLineCntr++)
                    {
                        if (oPaymentClaim == null)
                        {
                            oPaymentClaim = new gloAccountsV2.PaymentCollection.PaymentPatientClaim();
                            oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nClaimNo"]);
                            oPaymentClaim.DisplayClaimNo = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString());

                            oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionID"]);
                            oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDate"]);
                            oPaymentClaim.PatientID = PatientId;
                            oPaymentClaim.PatientName = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientName"]);
                            oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                        }
                        else if (oPaymentClaim.DisplayClaimNo.Trim().ToUpper() != Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString()).Trim().ToUpper())
                        {
                            if (oPaymentClaim.CliamLines.Count > 0)
                            {
                                oPaymentClaims.Add(oPaymentClaim);
                            }
                            oPaymentClaim = null;
                            oPaymentClaim = new gloAccountsV2.PaymentCollection.PaymentPatientClaim();
                            oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                            oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nClaimNo"]);
                            oPaymentClaim.DisplayClaimNo = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString());
                            oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionID"]);
                            oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDate"]);
                            oPaymentClaim.PatientID = PatientId;
                            oPaymentClaim.PatientName = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientName"]);
                            oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                        }

                        oPaymentLine = new gloAccountsV2.PaymentCollection.PaymentInsuranceLine();
                        oPaymentLine.PatientID = oPaymentClaim.PatientID;
                        oPaymentLine.BLTransactionID = oPaymentClaim.BillingTransactionID;
                        oPaymentLine.BLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDetailID"].ToString());
                        oPaymentLine.BLTransactionLineNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionLineNo"].ToString());
                        oPaymentLine.ClaimNumber = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["ClaimNumber"].ToString());
                        oPaymentLine.DOSFrom = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nFromDate"].ToString());
                        oPaymentLine.DOSTo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nToDate"].ToString());
                        oPaymentLine.CPTCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTCode"].ToString());
                        oPaymentLine.CPTDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTDescription"].ToString());
                        oPaymentLine.Modifiers = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["Modifier"].ToString());
                        oPaymentLine.Charges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dCharges"]);
                        oPaymentLine.Unit = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dUnit"]);
                        oPaymentLine.TotalCharges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dTotal"]);
                        if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"]) != "")
                        {
                            oPaymentLine.Allowed = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"]);
                            oPaymentLine.IsNullAllowed = false;
                        }
                        else
                            oPaymentLine.IsNullAllowed = true;
                        oPaymentLine.WriteOff = 0;
                        oPaymentLine.NonCovered = 0;
                        oPaymentLine.InsuranceAmount = 0;
                        oPaymentLine.Copay = 0;
                        oPaymentLine.Deductible = 0;
                        oPaymentLine.CoInsurance = 0;
                        oPaymentLine.Withhold = 0;

                        oPaymentLine.LinePrevPatientAdjustment = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PrevPatAdj"]);
                        oPaymentLine.LinePreviousPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousPaid"]);
                        oPaymentLine.LinePreviousAdjuestment = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousAdjuestment"]);
                        oPaymentLine.LineBalance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalBalanceAmount"]);
                        oPaymentLine.LinePatientDue = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientDue"]);
                        oPaymentLine.LineBadDebtDue = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["BadDebtDue"]);
                        oPaymentLine.LinePreviousPatientPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousPatientPaidAmount"]);
                        oPaymentLine.TrackBLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackTransactionDetailID"].ToString());
                        oPaymentLine.TrackBLTransactionID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackTransactionID"].ToString());
                        oPaymentLine.SubClaimNumber = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackSubClaimNo"].ToString());
                        oPaymentLine.ClaimOnHold = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackHoldInfo"].ToString());
                        if (LoadZeroBalance == true)
                        {
                            oPaymentClaim.CliamLines.Add(oPaymentLine);
                        }
                        else
                        {
                            if (oPaymentLine.LineBalance != 0)
                            { oPaymentClaim.CliamLines.Add(oPaymentLine); }
                        }

                        if (nTrnLineCntr == dtBillingTransactionLines.Rows.Count - 1)
                        {
                            if (oPaymentClaim.CliamLines.Count > 0)
                            {
                                oPaymentClaims.Add(oPaymentClaim);
                            }
                            oPaymentClaim = null;
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            { ex.ERROR_Log(ex.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
                if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); }
                if (dtBillingTransactionLines != null) { dtBillingTransactionLines.Dispose(); }
            }

            return oPaymentClaims;
        }

        public gloAccountsV2.PaymentCollection.PaymentPatientClaims GetBillingTransaction(Int64 PatientId, bool LoadZeroBalance, bool LoadBadDebtClaims = false)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtBillingTransaction = null;
            DataTable dtBillingTransactionLines = null;
            gloAccountsV2.PaymentCollection.PaymentPatientClaim oPaymentClaim = null;
            gloAccountsV2.PaymentCollection.PaymentInsuranceLine oPaymentLine = null;
            gloAccountsV2.PaymentCollection.PaymentPatientClaims oPaymentClaims = new gloAccountsV2.PaymentCollection.PaymentPatientClaims();

            try
            {
                oParameters.Clear();
                oParameters.Add("@nPatientID", PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bShowBadDebt", LoadBadDebtClaims, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_PaymentTransaction_Lines_NonAccount_V2", oParameters, out dtBillingTransactionLines);
                oDB.Disconnect();
                oParameters.Clear();

                if (dtBillingTransactionLines != null && dtBillingTransactionLines.Rows.Count > 0)
                {
                    for (int nTrnLineCntr = 0; nTrnLineCntr < dtBillingTransactionLines.Rows.Count; nTrnLineCntr++)
                    {
                        if (oPaymentClaim == null)
                        {
                            oPaymentClaim = new gloAccountsV2.PaymentCollection.PaymentPatientClaim();
                            oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nClaimNo"]);
                            oPaymentClaim.DisplayClaimNo = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString());

                            oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionID"]);
                            oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDate"]);
                            oPaymentClaim.PatientID = PatientId;
                            oPaymentClaim.PatientName = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientName"]);
                            oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                        }
                        else if (oPaymentClaim.DisplayClaimNo.Trim().ToUpper() != Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString()).Trim().ToUpper())
                        {
                            if (oPaymentClaim.CliamLines.Count > 0)
                            {
                                oPaymentClaims.Add(oPaymentClaim);
                            }
                            oPaymentClaim = null;
                            oPaymentClaim = new gloAccountsV2.PaymentCollection.PaymentPatientClaim();
                            oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                            oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nClaimNo"]);
                            oPaymentClaim.DisplayClaimNo = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString());
                            oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionID"]);
                            oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDate"]);
                            oPaymentClaim.PatientID = PatientId;
                            oPaymentClaim.PatientName = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientName"]);
                            oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                        }

                        oPaymentLine = new gloAccountsV2.PaymentCollection.PaymentInsuranceLine();
                        oPaymentLine.PatientID = oPaymentClaim.PatientID;
                        oPaymentLine.BLTransactionID = oPaymentClaim.BillingTransactionID;
                        oPaymentLine.BLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDetailID"].ToString());
                        oPaymentLine.BLTransactionLineNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionLineNo"].ToString());
                        oPaymentLine.ClaimNumber = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["ClaimNumber"].ToString());
                        oPaymentLine.DOSFrom = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nFromDate"].ToString());
                        oPaymentLine.DOSTo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nToDate"].ToString());
                        oPaymentLine.CPTCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTCode"].ToString());
                        oPaymentLine.CPTDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTDescription"].ToString());
                        oPaymentLine.Modifiers = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["Modifier"].ToString());
                        oPaymentLine.Charges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dCharges"]);
                        oPaymentLine.Unit = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dUnit"]);
                        oPaymentLine.TotalCharges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dTotal"]);
                        oPaymentLine.bNonServiceCode = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["bNonServiceCode"].ToString());
                        if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"]) != "")
                        {
                            oPaymentLine.Allowed = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"]);
                            oPaymentLine.IsNullAllowed = false;
                        }
                        else
                            oPaymentLine.IsNullAllowed = true;
                        oPaymentLine.WriteOff = 0;
                        oPaymentLine.NonCovered = 0;
                        oPaymentLine.InsuranceAmount = 0;
                        oPaymentLine.Copay = 0;
                        oPaymentLine.Deductible = 0;
                        oPaymentLine.CoInsurance = 0;
                        oPaymentLine.Withhold = 0;

                        oPaymentLine.LinePrevPatientAdjustment = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PrevPatAdj"]);
                        oPaymentLine.LinePreviousPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousPaid"]);
                        oPaymentLine.LinePreviousAdjuestment = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousAdjuestment"]);
                        oPaymentLine.LineBalance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalBalanceAmount"]);
                        oPaymentLine.LinePatientDue = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientDue"]);
                        oPaymentLine.LineBadDebtDue = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["BadDebtDue"]);
                        oPaymentLine.LinePreviousPatientPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousPatientPaidAmount"]);
                        oPaymentLine.TrackBLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackTransactionDetailID"].ToString());
                        oPaymentLine.TrackBLTransactionID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackTransactionID"].ToString());
                        oPaymentLine.SubClaimNumber = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackSubClaimNo"].ToString());
                        oPaymentLine.ClaimOnHold = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackHoldInfo"].ToString());
                        if (LoadZeroBalance == true)
                        {
                            oPaymentClaim.CliamLines.Add(oPaymentLine);
                        }
                        else
                        {
                            if (oPaymentLine.LineBalance != 0)
                            { oPaymentClaim.CliamLines.Add(oPaymentLine); }
                        }

                        if (nTrnLineCntr == dtBillingTransactionLines.Rows.Count - 1)
                        {
                            if (oPaymentClaim.CliamLines.Count > 0)
                            {
                                oPaymentClaims.Add(oPaymentClaim);
                            }
                            oPaymentClaim = null;
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            { ex.ERROR_Log(ex.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
                if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); }
                if (dtBillingTransactionLines != null) { dtBillingTransactionLines.Dispose(); }
            }

            return oPaymentClaims;
        }

        public gloAccountsV2.PaymentCollection.PaymentPatientClaims GetBillingTransactionAccountPatients_PAF(Int64 PAccountId, bool LoadZeroBalance,bool LoadBadDebtClaims=false)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtBillingTransaction = null;
            DataTable dtBillingTransactionLines = null;
            gloAccountsV2.PaymentCollection.PaymentPatientClaim oPaymentClaim = null;
            gloAccountsV2.PaymentCollection.PaymentInsuranceLine oPaymentLine = null;
            gloAccountsV2.PaymentCollection.PaymentPatientClaims oPaymentClaims = new gloAccountsV2.PaymentCollection.PaymentPatientClaims();

            try
            {
                oParameters.Clear();
                oParameters.Add("@nPAccountID", PAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bShowBadDebt", LoadBadDebtClaims, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_PaymentTransaction_Lines_ForAccountPatients_V2", oParameters, out dtBillingTransactionLines);
                oDB.Disconnect();
                oParameters.Clear();

                if (dtBillingTransactionLines != null && dtBillingTransactionLines.Rows.Count > 0)
                {
                    for (int nTrnLineCntr = 0; nTrnLineCntr < dtBillingTransactionLines.Rows.Count; nTrnLineCntr++)
                    {
                        if (oPaymentClaim == null)
                        {
                            oPaymentClaim = new gloAccountsV2.PaymentCollection.PaymentPatientClaim();
                            oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nClaimNo"]);
                            oPaymentClaim.DisplayClaimNo = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString());

                            oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionID"]);
                            oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDate"]);
                            oPaymentClaim.PatientID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nPatientID"]);
                            oPaymentClaim.PatientName = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientName"]);
                            oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                            oPaymentClaim.FacilityType = Convert.ToInt16(dtBillingTransactionLines.Rows[nTrnLineCntr]["nFacilityType"]);
                        }
                        else if (oPaymentClaim.DisplayClaimNo.Trim().ToUpper() != Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString()).Trim().ToUpper())
                        {
                            if (oPaymentClaim.CliamLines.Count > 0)
                            {
                                oPaymentClaims.Add(oPaymentClaim);
                            }
                            oPaymentClaim = null;
                            oPaymentClaim = new gloAccountsV2.PaymentCollection.PaymentPatientClaim();
                            oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                            oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nClaimNo"]);
                            oPaymentClaim.DisplayClaimNo = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString());
                            oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionID"]);
                            oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDate"]);
                            oPaymentClaim.PatientID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nPatientID"]);
                            oPaymentClaim.PatientName = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientName"]);
                            oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                            oPaymentClaim.FacilityType = Convert.ToInt16(dtBillingTransactionLines.Rows[nTrnLineCntr]["nFacilityType"]);
                        }

                        oPaymentLine = new gloAccountsV2.PaymentCollection.PaymentInsuranceLine();
                        oPaymentLine.PatientID = oPaymentClaim.PatientID;
                        oPaymentLine.BLTransactionID = oPaymentClaim.BillingTransactionID;
                        oPaymentLine.BLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDetailID"].ToString());
                        oPaymentLine.BLTransactionLineNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionLineNo"].ToString());
                        oPaymentLine.ClaimNumber = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["ClaimNumber"].ToString());
                        oPaymentLine.DOSFrom = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nFromDate"].ToString());
                        oPaymentLine.DOSTo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nToDate"].ToString());
                        oPaymentLine.CPTCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTCode"].ToString());
                        oPaymentLine.CPTDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTDescription"].ToString());
                        oPaymentLine.Modifiers = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["Modifier"].ToString());
                        oPaymentLine.Charges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dCharges"]);
                        oPaymentLine.Unit = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dUnit"]);
                        oPaymentLine.TotalCharges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dTotal"]);
                        if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"]) != "")
                        {
                            oPaymentLine.Allowed = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"]);
                            oPaymentLine.IsNullAllowed = false;
                        }
                        else
                            oPaymentLine.IsNullAllowed = true;

                        oPaymentLine.WriteOff = 0;
                        oPaymentLine.NonCovered = 0;
                        oPaymentLine.InsuranceAmount = 0;
                        oPaymentLine.Copay = 0;
                        oPaymentLine.Deductible = 0;
                        oPaymentLine.CoInsurance = 0;
                        oPaymentLine.Withhold = 0;

                        oPaymentLine.LinePrevPatientAdjustment = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PrevPatAdj"]);
                        oPaymentLine.LinePreviousPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousPaid"]);
                        oPaymentLine.LinePreviousAdjuestment = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousAdjuestment"]);
                        oPaymentLine.LineBalance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalBalanceAmount"]);
                        oPaymentLine.LinePatientDue = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientDue"]);
                        oPaymentLine.LineBadDebtDue = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["BadDebtDue"]);
                        oPaymentLine.LinePreviousPatientPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousPatientPaidAmount"]);
                        oPaymentLine.TrackBLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackTransactionDetailID"].ToString());
                        oPaymentLine.TrackBLTransactionID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackTransactionID"].ToString());
                        oPaymentLine.SubClaimNumber = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackSubClaimNo"].ToString());
                        oPaymentLine.ClaimOnHold = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackHoldInfo"].ToString());
                        oPaymentLine.bNonServiceCode = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["bNonServiceCode"].ToString());
                        if (LoadZeroBalance == true)
                        {
                            oPaymentClaim.CliamLines.Add(oPaymentLine);
                        }
                        else
                        {
                            if (oPaymentLine.LineBalance != 0)
                            { oPaymentClaim.CliamLines.Add(oPaymentLine); }
                        }

                        if (nTrnLineCntr == dtBillingTransactionLines.Rows.Count - 1)
                        {
                            if (oPaymentClaim.CliamLines.Count > 0)
                            {
                                oPaymentClaims.Add(oPaymentClaim);
                            }
                            oPaymentClaim = null;
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            { ex.ERROR_Log(ex.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
                if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); }
                if (dtBillingTransactionLines != null) { dtBillingTransactionLines.Dispose(); }
            }

            return oPaymentClaims;
        }

        public DataTable GetBillingTransactionAccountPatients_PAFNew(Int64 PAccountId, bool LoadZeroBalance)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //    DataTable dtBillingTransaction = new DataTable();
            DataTable dtBillingTransactionLines = null;
            try
            {
                oParameters.Clear();
                oParameters.Add("@nPAccountID", PAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_PaymentTransaction_Lines_ForAccountPatients_V3", oParameters, out dtBillingTransactionLines);
                oDB.Disconnect();
                oParameters.Clear();
            }
            catch (Exception) { return null; }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtBillingTransactionLines;
        }
        public string GetAdjustmentCodes()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _payAdjustment = "";
            DataTable _dt = null;
            string _sqlQuery = "";
            string _conCodeDesc = "";

            try
            {
                _sqlQuery = " SELECT ISNULL(sAdjustmentTypeCode,'') AS Code,ISNULL(sAdjustmentTypeDesc,'') AS Description " +
                " FROM BL_AdjustmentType_MST WITH (NOLOCK) " +
                " WHERE " +
                " nClinicID = 1 AND bIsBlocked = '" + false + "'";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dt);
                oDB.Disconnect();

                if (_dt != null && _dt.Rows.Count > 0)
                {
                    _payAdjustment = "|";

                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        _conCodeDesc = "";

                        if (Convert.ToString(_dt.Rows[i]["Code"]).Trim() != "" && Convert.ToString(_dt.Rows[i]["Description"]).Trim() != "")
                        {
                            _conCodeDesc = Convert.ToString(_dt.Rows[i]["Code"]).Trim().ToUpper() + "-" + Convert.ToString(_dt.Rows[i]["Description"]).Trim().ToUpper() + "|";
                            _payAdjustment += _conCodeDesc;
                        }
                    }

                    _payAdjustment = _payAdjustment.TrimEnd('|');
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); }
                if (_dt != null) { _dt.Dispose(); }
                if (_sqlQuery != null) { _sqlQuery = null; }
            }

            return _payAdjustment;
        }

        public string GetFormattedClaimPaymentNumber(string NumberSize)
        {
            int _length = 0;
            _length = NumberSize.Length;
            if (_length == 1)
            {
                NumberSize = "0000" + NumberSize;
            }
            else if (_length == 2)
            {
                NumberSize = "000" + NumberSize;
            }
            else if (_length == 3)
            {
                NumberSize = "00" + NumberSize;
            }
            else if (_length == 4)
            {
                NumberSize = "0" + NumberSize;
            }
            else if (_length == 5)
            {
                //NumberSize = NumberSize;
            }
            return NumberSize;
        }

        public string GetPaymentPrefixNumber()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            Object _retVal = null;
            string _sqlQuery = "";
            Int64 _paymentMaxNo = 0;
            string NumberSize = "";

            try
            {
                oDB.Connect(false);

                _sqlQuery = " SELECT ISNULL(MAX(CONVERT(NUMERIC,sPaymentNo)),0) + 1  " +
                 " FROM Credits ";//WHERE  sPaymentNo = '" + Prefix + "'";

                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && _retVal.ToString().Trim() != "")
                { _paymentMaxNo = Convert.ToInt64(_retVal); }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            finally
            {
                if (_retVal != null) { _retVal = null; }
                if (oDB != null) { oDB.Dispose(); }
            }

            NumberSize = "GPM#" + _paymentMaxNo.ToString();

            return NumberSize;
        }

        public bool IsRefunded(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            Object _retVal = false;
            //string _sqlQuery = "";
            bool _IsRefunded = false;

            try
            {
                if (CreditID > 0)
                {
                    oDB.Connect(false);

                    //Code changes by Sagar Ghodke, v7030 Date:05/08/2013
                    //Code changes done to resolve bug#49855
                    //Moving following query logic to stored procedure "BL_Is_PatientPayment_Refunded"

                    ////_sqlQuery = "SELECT nCreditID FROM Credits_dtl WITH(NOLOCK)" +
                    ////" WHERE nEntryType = 7 " +
                    ////" AND ISNULL(bIsPaymentVoid,0) <> 1 and nCreditsREf_ID= " + CreditID + "";
                    

                    oParameters = new gloDatabaseLayer.DBParameters();
                    oParameters.Add("@nCreditId", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@bIsRefunded", _IsRefunded, ParameterDirection.InputOutput, SqlDbType.Bit);

                    oDB.Execute("BL_Is_PatientPayment_Refunded", oParameters, out _retVal);
                    

                    ////_retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();


                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    { _IsRefunded = Convert.ToBoolean(_retVal); }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oParameters != null) { oParameters.Clear(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _IsRefunded;
        }

        public DataTable GetAccountPatients(Int64 nPAccountID)
        {
            DataTable oDataTable = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string _sqlQuery = "";
            try
            {

                oParameters.Clear();
                oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                _sqlQuery = "PA_Select_AccountPatients";
                oDB.Connect(false);
                oDB.Retrive(_sqlQuery, oParameters, out oDataTable);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
            }
            return oDataTable;
        }

        public static DataTable GetEOBPaymentID(Int64 nInsRefundID, Int64 nPatientID)
        {
            DataTable _dtEOBPaymentID = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                oDB.Connect(false);
                string _sqlQuery = "";
                if (nPatientID > 0)
                {
                    _sqlQuery = " select nCreditID AS nEOBPaymentID from Refunds WITH (NOLOCK) where nrefundID=" + nInsRefundID + " ";
                }
                else
                {
                    _sqlQuery = " select nCreditID AS nEOBPaymentID from Refunds WITH (NOLOCK) where nMasterRefundId=" + nInsRefundID + " ";
                }

                oDB.Retrive_Query(_sqlQuery, out _dtEOBPaymentID);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dtEOBPaymentID;
        }

        public DataTable GetExpectedCopayAmt(Int64 Patientid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtCopayAlert = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);

                _sqlQuery = " SELECT ISNULL(sInsuranceName,'') AS sInsuranceName,ISNULL(nCopay,0) AS nCopay FROM PatientInsurance_DTL WITH (NOLOCK) WHERE nPatientID  = " + Patientid + " AND ISNULL(nInsuranceFlag,0) = 1";

                oDB.Retrive_Query(_sqlQuery, out _dtCopayAlert);
                oDB.Disconnect();


            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dtCopayAlert;
        }

        public DataTable GetLastPatientPmtAmt(Int64 Patientid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtCopayAlert = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);

                _sqlQuery = " SELECT TOP 1      " +
                            "        Credits.dReceiptAmount AS LastPay," +
                            "        Credits.dtClosedate As dtCreatedDateTime" +
                            " FROM    Credits    " +
                            "        INNER JOIN" +
                            "        Credits_EXT" +
                            "        ON dbo.Credits.nCreditID = dbo.Credits_EXT.nCreditID" +
                            " WHERE   Credits.nPayerID = " + Patientid +
                            "        AND nPayerType = 1 AND nEntryType = 6   " +
                            "        AND Credits.dReceiptAmount > 0 " +
                            "        AND ( Credits.bIsPaymentVoid IS NULL " +
                            "            OR Credits.bIsPaymentVoid = 0 " +
                            "            OR Credits.dtPaymentVoidCloseDate > dbo.gloGetDate() " +
                            "        )  " +
                            "        ORDER BY Credits_EXT.dtCreatedDateTime DESC  ";

                oDB.Retrive_Query(_sqlQuery, out _dtCopayAlert);
                oDB.Disconnect();


            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dtCopayAlert;
        }

        public DataRow GetPatientBalances(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;

            DataRow _patientBalance = null;
            DataTable _dtPatientBalance = null;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oDB.Retrive("BL_GetPatientBalance_V2", oParameters, out _dtPatientBalance);
                oDB.Disconnect();

                if (_dtPatientBalance != null && _dtPatientBalance.Rows.Count > 0)
                {
                    _patientBalance = _dtPatientBalance.Rows[0];
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtPatientBalance != null) { _dtPatientBalance.Dispose(); }
            }
            return _patientBalance;
        }

        public DataRow GetPatientAccountBadDept(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;

            DataRow _PatAccBaddept = null;
            DataTable _dtPatAccBaddept = null;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oDB.Retrive("BL_GetPatAccountBadDeptBalance", oParameters, out _dtPatAccBaddept);
                oDB.Disconnect();

                if (_dtPatAccBaddept != null && _dtPatAccBaddept.Rows.Count > 0)
                {
                    _PatAccBaddept = _dtPatAccBaddept.Rows[0];
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtPatAccBaddept != null) { _dtPatAccBaddept.Dispose(); }
            }
            return _PatAccBaddept;
        }

        public static DataSet GetAccountBalances(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet dtSet = new DataSet();
            try
            {

                oParameters.Clear();
                oParameters.Add("@nPAccountID", PAccountID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("PA_GetAccountBalances_V2", oParameters, out dtSet);
                oDB.Disconnect();


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
                if (oParameters != null)
                    oParameters.Dispose();
            }
            return dtSet;
        }

        public static string GetReserveStatus(Int64 CreditID, Int64 ReserveID, Decimal UsedAmount)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oParameters = null;
            string _reserveStatusMessage = "";
            object _value = null;

            try
            {

                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oParameters = new gloDatabaseLayer.DBParameters();
                
                
                oParameters.Add("@ReserveId", ReserveID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@CreditId", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@AmountUsed", UsedAmount, ParameterDirection.Input, SqlDbType.Decimal);
                oParameters.Add("@ErrorMessage", _reserveStatusMessage, ParameterDirection.InputOutput, SqlDbType.VarChar,5000);

                oDB.Connect(false);
                oDB.Execute("gsp_ValidateUsedReserves", oParameters, out _value);
                oDB.Disconnect();

                if (_value != null || _value != DBNull.Value || _value.ToString() != "")
                { _reserveStatusMessage = Convert.ToString(_value); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }

                if (oParameters != null)
                {
                    oParameters.Clear();
                    oParameters.Dispose();
                }
            }

            return _reserveStatusMessage;   
        }

        public static Boolean IsBadDebtPatient(Int64 nPAccountID, Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oParameters = null;
            bool _reserveStatusMessage =false;
            object _value = null;

            try
            {

                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oParameters = new gloDatabaseLayer.DBParameters();


                oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bIsBadDebt", _reserveStatusMessage, ParameterDirection.InputOutput, SqlDbType.Bit);
                

                oDB.Connect(false);
                oDB.Execute("PA_Select_BadDebtPatient", oParameters, out _value);
                oDB.Disconnect();

                if (_value != null || _value != DBNull.Value || _value.ToString() != "")
                { _reserveStatusMessage = Convert.ToBoolean(_value); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }

                if (oParameters != null)
                {
                    oParameters.Clear();
                    oParameters.Dispose();
                }
            }

            return _reserveStatusMessage;
        }

        public static Int64 getCollectionAgencyContactID(Int64 nTrackTransactionID,Int64 nTrackTransactionDetailID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            Object _retVal = false;
            string _sqlQuery = "";
            Int64  _ncontactID = 0;

            try
            {
                
                    oDB.Connect(false);
                    _sqlQuery = "SELECT BL_EOB_NextAction.nNextActionContactID FROM dbo.BL_EOB_NextAction WITH (NOLOCK) WHERE dbo.BL_EOB_NextAction.nTrackMstTrnID=" + nTrackTransactionID + " and dbo.BL_EOB_NextAction.nTrackMstTrnDetailID=" + nTrackTransactionDetailID;
                   
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    { _ncontactID = Convert.ToInt64(_retVal); }
                
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _ncontactID;
        }

        public gloAccountsV2.PaymentCollection.PaymentPatientClaims Cleargage_GetBillingTransaction_PAF(Int64 PAccountId, Int64 PatientId,Int64 CleargageFileID,string EncounterID,bool LoadZeroBalance, bool LoadBadDebtClaims = false)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtBillingTransaction = null;
            DataTable dtBillingTransactionLines = null;
            gloAccountsV2.PaymentCollection.PaymentPatientClaim oPaymentClaim = null;
            gloAccountsV2.PaymentCollection.PaymentInsuranceLine oPaymentLine = null;
            gloAccountsV2.PaymentCollection.PaymentPatientClaims oPaymentClaims = new gloAccountsV2.PaymentCollection.PaymentPatientClaims();

            try
            {
                oParameters.Clear();
                oParameters.Add("@nPatientID", PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", PAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCleargageFileID", CleargageFileID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@EncounterID", EncounterID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@bShowBadDebt", LoadBadDebtClaims, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("Cleargage_SelectPaymentTransaction_Lines_PatPayment_V2", oParameters, out dtBillingTransactionLines);
                oDB.Disconnect();
                oParameters.Clear();

                if (dtBillingTransactionLines != null && dtBillingTransactionLines.Rows.Count > 0)
                {
                    for (int nTrnLineCntr = 0; nTrnLineCntr < dtBillingTransactionLines.Rows.Count; nTrnLineCntr++)
                    {
                        if (oPaymentClaim == null)
                        {
                            oPaymentClaim = new gloAccountsV2.PaymentCollection.PaymentPatientClaim();
                            oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nClaimNo"]);
                            oPaymentClaim.DisplayClaimNo = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString());

                            oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionID"]);
                            oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDate"]);
                            oPaymentClaim.PatientID = PatientId;
                            oPaymentClaim.PatientName = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientName"]);
                            oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                        }
                        else if (oPaymentClaim.DisplayClaimNo.Trim().ToUpper() != Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString()).Trim().ToUpper())
                        {
                            if (oPaymentClaim.CliamLines.Count > 0)
                            {
                                oPaymentClaims.Add(oPaymentClaim);
                            }
                            oPaymentClaim = null;
                            oPaymentClaim = new gloAccountsV2.PaymentCollection.PaymentPatientClaim();
                            oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                            oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nClaimNo"]);
                            oPaymentClaim.DisplayClaimNo = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString());
                            oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionID"]);
                            oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDate"]);
                            oPaymentClaim.PatientID = PatientId;
                            oPaymentClaim.PatientName = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientName"]);
                            oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                        }

                        oPaymentLine = new gloAccountsV2.PaymentCollection.PaymentInsuranceLine();
                        oPaymentLine.PatientID = oPaymentClaim.PatientID;
                        oPaymentLine.BLTransactionID = oPaymentClaim.BillingTransactionID;
                        oPaymentLine.BLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDetailID"].ToString());
                        oPaymentLine.BLTransactionLineNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionLineNo"].ToString());
                        oPaymentLine.ClaimNumber = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["ClaimNumber"].ToString());
                        oPaymentLine.DOSFrom = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nFromDate"].ToString());
                        oPaymentLine.DOSTo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nToDate"].ToString());
                        oPaymentLine.CPTCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTCode"].ToString());
                        oPaymentLine.CPTDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTDescription"].ToString());
                        oPaymentLine.Modifiers = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["Modifier"].ToString());
                        oPaymentLine.Charges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dCharges"]);
                        oPaymentLine.Unit = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dUnit"]);
                        oPaymentLine.TotalCharges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dTotal"]);
                        if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"]) != "")
                        {
                            oPaymentLine.Allowed = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"]);
                            oPaymentLine.IsNullAllowed = false;
                        }
                        else
                            oPaymentLine.IsNullAllowed = true;
                        oPaymentLine.WriteOff = 0;
                        oPaymentLine.NonCovered = 0;
                        oPaymentLine.InsuranceAmount = 0;
                        oPaymentLine.Copay = 0;
                        oPaymentLine.Deductible = 0;
                        oPaymentLine.CoInsurance = 0;
                        oPaymentLine.Withhold = 0;

                        oPaymentLine.LinePrevPatientAdjustment = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PrevPatAdj"]);
                        oPaymentLine.LinePreviousPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousPaid"]);
                        oPaymentLine.LinePreviousAdjuestment = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousAdjuestment"]);
                        oPaymentLine.LineBalance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalBalanceAmount"]);
                        oPaymentLine.LinePatientDue = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientDue"]);
                        oPaymentLine.LineBadDebtDue = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["BadDebtDue"]);
                        oPaymentLine.LinePreviousPatientPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousPatientPaidAmount"]);
                        oPaymentLine.TrackBLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackTransactionDetailID"].ToString());
                        oPaymentLine.TrackBLTransactionID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackTransactionID"].ToString());
                        oPaymentLine.SubClaimNumber = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackSubClaimNo"].ToString());
                        oPaymentLine.ClaimOnHold = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackHoldInfo"].ToString());
                        if (LoadZeroBalance == true)
                        {
                            oPaymentClaim.CliamLines.Add(oPaymentLine);
                        }
                        else
                        {
                            if (oPaymentLine.LineBalance != 0)
                            { oPaymentClaim.CliamLines.Add(oPaymentLine); }
                        }

                        if (nTrnLineCntr == dtBillingTransactionLines.Rows.Count - 1)
                        {
                            if (oPaymentClaim.CliamLines.Count > 0)
                            {
                                oPaymentClaims.Add(oPaymentClaim);
                            }
                            oPaymentClaim = null;
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            { ex.ERROR_Log(ex.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
                if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); }
                if (dtBillingTransactionLines != null) { dtBillingTransactionLines.Dispose(); }
            }

            return oPaymentClaims;
        }
        #endregion "Get Methods"

        #region "Save And Void Method"

        public Int64 SavePatientPayment(DataSet dsPatientPayment)
        {
            Int64 PaymentId = 0;
            try
            {
                PaymentId = SavePaymentTVP(dsPatientPayment);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
            return PaymentId;
        }

        public Int64 SavePaymentTVP(DataSet dsPaymentTVP)
        {
            Int64 nCreditID = 0;
            object _result = null;
            object _error = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string strErrorMessage = string.Empty;
           // bool showErrorMsg = false;
            try
            {
                if (dsPaymentTVP != null && dsPaymentTVP.Tables.Count > 0)
                {
                    oParameters.Clear();
                    oDB.Connect(false);
                    oParameters.Add("@tvpCredits", dsPaymentTVP.Tables["Credits"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpDebits", dsPaymentTVP.Tables["Debits"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpEOB", dsPaymentTVP.Tables["EOB"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpReserves", dsPaymentTVP.Tables["Reserves"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpInsReserveAssociation", dsPaymentTVP.Tables["BL_Reserve_Association"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpCredits_DTL", dsPaymentTVP.Tables["CreditsDTL"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpRefunds", dsPaymentTVP.Tables["Refunds"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpReasonCode", dsPaymentTVP.Tables["ReasonCode"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpEOBNotes", dsPaymentTVP.Tables["EOBNotes"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@nCreditsID", nCreditID, ParameterDirection.Output, SqlDbType.BigInt);
                    oParameters.Add("@error_Message", strErrorMessage, ParameterDirection.Output, SqlDbType.VarChar, 1000);
                    oDB.Execute("BL_SavePatientPayment_TVP", oParameters, out  _result, out _error);

                    oDB.Disconnect();
                    if (_result != null && Convert.ToString(_result).Trim() != "")
                        nCreditID = Convert.ToInt64(_result);
                    else
                        nCreditID = 0;

                    if (_error != null && Convert.ToString(_error) != "")
                    { strErrorMessage = Convert.ToString(_error); }
                    if (strErrorMessage != "Success")
                    {
                        throw new Exception(strErrorMessage.ToString());
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
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
            return nCreditID;

        }

        public Int64 VoidPatientPayment(Int64 CreditID, Int64 PAccountID, Int64 AccountPatientID, string VoidNote, DateTime VoidCloseDate, Int64 VoidTrayID, string VoidTrayName)
        {
            System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            System.Data.SqlClient.SqlCommand _sqlCommand = null;
            System.Data.SqlClient.SqlTransaction _sqlTransaction = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {

                #region "Master Void Payment Note"

                if (VoidNote != null)
                {
                    Object _RcValue = null;
                    _RcValue = null;
                    oParameters.Clear();

                    oParameters.Add("@nID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                    oParameters.Add("@nEOBPaymentID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                    oParameters.Add("@nEOBVoidPaymentID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                    oParameters.Add("@dVoidAmount", 0, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 0),
                    oParameters.Add("@sNoteDescription", VoidNote.ToString(), ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0),
                    oParameters.Add("@nVoidNoteType", VoidType.PatientPaymentVoid.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),	
                    oParameters.Add("@sUserName", AppSettings.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0),
                    oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	varchar(5),
                    oParameters.Add("@nClinicID", 1, ParameterDirection.Input, SqlDbType.BigInt);//	decimal(18, 2),
                    oParameters.Add("@nVoidCloseDate", gloDateMaster.gloDate.DateAsNumber(VoidCloseDate.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                    oParameters.Add("@nVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                    oParameters.Add("@sVoidTrayDescription", VoidTrayName.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sVoidTrayCode", "", ParameterDirection.Input, SqlDbType.VarChar);

                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = "BL_INUP_EOBPaymentVoid_Notes";

                    int _result = 0;
                    _sqlConnection.Open();
                    _result = _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();

                    if (_sqlCommand.Parameters["@nID"].Value != null)
                    { _RcValue = _sqlCommand.Parameters["@nID"].Value; }
                    else
                    { _RcValue = 0; }

                }


                #endregion "Master Void Payment Note"

                oParameters.Clear();
                oParameters.Add("@nEOBPaymentID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@dtVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@nVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sVoidTrayName", VoidTrayName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nVoidType", VoidType.PatientPaymentVoid.GetHashCode(), ParameterDirection.Input, SqlDbType.TinyInt);
                oParameters.Add("@bIsPaymentVoid", 1, ParameterDirection.Input, SqlDbType.Bit);

                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                _sqlCommand = oDB.GetCmdParameters(oParameters);
                _sqlCommand.Connection = _sqlConnection;
                _sqlCommand.Transaction = _sqlTransaction;
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.CommandText = "BL_PatientPaymentVoid_V2";
                _sqlConnection.Open();
                _sqlCommand.ExecuteNonQuery();
                _sqlConnection.Close();
            }
            catch (gloDatabaseLayer.DBException ex)
            { _sqlTransaction.Rollback(); ex.ERROR_Log(ex.ToString()); }
            catch (Exception ex)
            { _sqlTransaction.Rollback(); gloAuditTrail.gloAuditTrail.ExceptionLog("ERROR: " + ex.Message, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); }
                if (_sqlTransaction != null) { _sqlTransaction.Dispose(); }
            }
            return 0;
        }

        public Int64 SavePatientRefund(DataSet dsPatientPaymentRefund)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            Int64 nCreditId = 0;
            try
            {
                if (dsPatientPaymentRefund != null)
                {
                    nCreditId = SavePaymentTVP(dsPatientPaymentRefund);
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return nCreditId;
        }

        #endregion "Save And Void Method"
    }

    public class gloBillingCommonV2
    { 
        #region "Constructor"

        public gloBillingCommonV2()
        {
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~gloBillingCommonV2()
        {
            Dispose(false);
        }
        #endregion "Constructor"

        #region "Get Methods"

        public static int GetFutureCloseDayDateSettings()
        {
            int _addDays = 0;
            Object _retSettingValue = null;
            try
            {
                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oSettings.GetSetting("FUTURECLOSEDATEDAYS", 0, gloGlobal.gloPMGlobal.ClinicID, out _retSettingValue);
                oSettings.Dispose();

                if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                { _addDays = Convert.ToInt32(_retSettingValue); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _addDays;
        }

        public static bool IsInsuranceReserve_ProviderEnable()
        {
            bool IsEnableProvider = false;
            Object _retSettingValue = null;
            try
            {
                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oSettings.GetSetting("INSRSV_ENABLEPROVIDER", 0, gloGlobal.gloPMGlobal.ClinicID, out _retSettingValue);
                oSettings.Dispose();

                if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                { IsEnableProvider = Convert.ToBoolean(_retSettingValue); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return IsEnableProvider;
        }

        public static bool IsPatientReserve_ProviderEnable()
        {
            bool IsEnableProvider = false;
            Object _retSettingValue = null;
            try
            {
                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oSettings.GetSetting("PATRSV_PROVIDERMANDATORY", 0, gloGlobal.gloPMGlobal.ClinicID, out _retSettingValue);
                oSettings.Dispose();

                if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                { IsEnableProvider = Convert.ToBoolean(_retSettingValue); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return IsEnableProvider;
        }

        public static bool IsDefaultPatient_ProviderEnable()
        {
            bool IsDefaultPatientProviderEnable = false;
            Object _retSettingValue = null;
            try
            {
                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oSettings.GetSetting("PATRSV_DEFAULTPROVIDER", 0, gloGlobal.gloPMGlobal.ClinicID, out _retSettingValue);
                oSettings.Dispose();

                if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                { IsDefaultPatientProviderEnable = Convert.ToBoolean(_retSettingValue); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return IsDefaultPatientProviderEnable;
        }

        public static bool IsPaymentTrayActive(Int64 PaymentTrayID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            Object _retVal = false;
            string _sqlQuery = "";
            bool _IsPaymentTrayActive = false;

            try
            {
                if (PaymentTrayID > 0)
                {
                    oDB.Connect(false);
                    _sqlQuery = " SELECT bIsActive FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + PaymentTrayID;// +
                        //" AND (bIsActive = 1 OR bIsDefault = 1) ";
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    { _IsPaymentTrayActive = Convert.ToBoolean(_retVal); }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _IsPaymentTrayActive;
        }

        public static bool IsChargeTrayActive(Int64 ChargeTrayID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            Object _retVal = false;
            string _sqlQuery = "";
            bool _IsChargeTrayActive = false;

            try
            {
                if (ChargeTrayID > 0)
                {
                    oDB.Connect(false);
                    _sqlQuery = " SELECT bIsActive FROM BL_ChargesTray WITH (NOLOCK) WHERE nChargeTrayID = " + ChargeTrayID;// +
                    //" AND (bIsActive = 1 OR bIsDefault = 1) ";
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    { _IsChargeTrayActive = Convert.ToBoolean(_retVal); }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _IsChargeTrayActive;
        }

        public static bool IsPaymentTrayDefault(Int64 PaymentTrayID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            Object _retVal = false;
            string _sqlQuery = "";
            bool _IsPaymentTrayDefault = false;

            try
            {
                if (PaymentTrayID > 0)
                {
                    oDB.Connect(false);
                    _sqlQuery = " SELECT bIsDefault FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + PaymentTrayID;// +
                    //" AND (bIsActive = 1 OR bIsDefault = 1) ";
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    { _IsPaymentTrayDefault = Convert.ToBoolean(_retVal); }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _IsPaymentTrayDefault;
        }

        public static bool IsBusinessCenterAssociated(Int64 _nPAccountID)
        {
            #region "Validation on generating individual statement if no Business Center Associated"

            bool _isBusinessCenterAssociated = false;
            object _result = null;
            string _sqlQuery = string.Empty;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            oDB.Connect(false);
            try
            {
                _sqlQuery = "SELECT CASE ISNULL(nBusinessCenterID, 0) " +
                            "  WHEN 0 THEN 'False' " +
                            "  ELSE 'True' " +
                            " END AS IsBusinessCenterAssociated FROM PA_Accounts WITH (NOLOCK) WHERE nPAccountID = " + _nPAccountID;

                _result = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_result != null && Convert.ToString(_result) != "")
                {
                    _isBusinessCenterAssociated = Convert.ToBoolean(_result);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _isBusinessCenterAssociated;

            #endregion "Validation on generating individual statement if no Business Center Associated"
        }

        public static DateTime GetPaymentCloseDate(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtCloseDate = null;
            DateTime _nCloseDate = DateTime.Now;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);

                _sqlQuery = " SELECT dtCloseDate FROM dbo.Credits WHERE nCreditID = " + CreditID;

                oDB.Retrive_Query(_sqlQuery, out _dtCloseDate);
                oDB.Disconnect();

                if (_dtCloseDate != null && _dtCloseDate.Rows.Count > 0)
                {
                    _nCloseDate = Convert.ToDateTime(_dtCloseDate.Rows[0]["dtCloseDate"].ToString());
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _nCloseDate;
        }

        public static DataTable GetDefaultPatientProvider(Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtDefaultPatientProvider = null;
            try
            {
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

                oDB.Connect(false);
                string strQuery =   " SELECT  bIsblocked,ISNULL(Provider_MST.nProviderID, 0) AS nProviderID,"+
                                    " ( ISNULL(Provider_MST.sFirstName, '') + SPACE(1)  "+
                                    "   + CASE ISNULL(Provider_MST.sMiddleName, '')  "+
                                    "   WHEN '' THEN '' "+  
                                    "   WHEN Provider_MST.sMiddleName "+  
                                    "   THEN Provider_MST.sMiddleName + SPACE(1) "+ 
                                    "   END + ISNULL(Provider_MST.sLastName, '') ) AS ProviderName "+
                                    " FROM Patient  WITH (NOLOCK)               "+
                                    " INNER JOIN dbo.Provider_MST WITH (NOLOCK) ON dbo.Patient.nProviderID = dbo.Provider_MST.nProviderID " +
                                    " WHERE  Patient.nPatientID = "+ nPatientID +
                                    " AND bIsblocked = 0 ";
                oDB.Retrive_Query(strQuery, out _dtDefaultPatientProvider);


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _dtDefaultPatientProvider;
        }

        public static DataTable GetBillingProvider(Int64 nTransactionMasterID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtBillingProvider = null;
            try
            {
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

                oDB.Connect(false);
                string strQuery = " SELECT bIsblocked , BL_Transaction_Claim_MST.nTransactionID, " +
                                   " ISNULL(BL_Transaction_Claim_MST.nTransactionProviderID, 0) AS nProviderID , " +
                                   " ( ISNULL(Provider_MST.sFirstName, '') + SPACE(1) " +
                                   "     + CASE ISNULL(Provider_MST.sMiddleName, '') " +
                                   "         WHEN '' THEN '' " +
                                   "         WHEN Provider_MST.sMiddleName " +
                                   "         THEN Provider_MST.sMiddleName + SPACE(1) " +
                                   "         END + ISNULL(Provider_MST.sLastName, '') ) AS ProviderName " +
                                   " FROM   Patient WITH ( NOLOCK ) " +
                                   " INNER JOIN dbo.Provider_MST WITH ( NOLOCK ) ON dbo.Patient.nProviderID = dbo.Provider_MST.nProviderID " +
                                   " INNER JOIN dbo.BL_Transaction_Claim_MST WITH ( NOLOCK ) ON dbo.Patient.nPatientID = dbo.BL_Transaction_Claim_MST.nPatientID " +
                                   " WHERE  BL_Transaction_Claim_MST.nTransactionMasterID = " + nTransactionMasterID;
                oDB.Retrive_Query(strQuery, out _dtBillingProvider);


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _dtBillingProvider;
        }

        public static DataTable GetGlobalPeriods_ForAlter(Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtGlobalPeriodsList = null;
            try
            {
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

                oDB.Connect(false);
                string strQuery = " select top(1) nid As nId,CONVERT(VARCHAR(20),dtStartDate,101) + ' - ' + CONVERT(VARCHAR(20),dtEndDate,101) AS Dates " +
                                  " from Patient_Global_Periods  where nPatientID =" + nPatientID + " and CONVERT(varchar(10),dbo.gloGetDate(),101) between dtStartDate AND dtEndDate " +
                                  " order by dtEndDate desc ";
                oDB.Retrive_Query(strQuery, out _dtGlobalPeriodsList);


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _dtGlobalPeriodsList;
        }

        public static DataSet GetDefaultAccountFollowUp(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataSet _dsDefaultAccountFollowUp = null;
            try
            {
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

                oParameters.Add("@nPAccountID", PAccountID, ParameterDirection.Input, SqlDbType.BigInt);               
                oDB.Connect(false);
                oDB.Retrive("CL_GetDefaultAccountFollowUp", oParameters, out _dsDefaultAccountFollowUp);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _dsDefaultAccountFollowUp;
        }

        public static decimal GetLastAccountPaidAmount(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            decimal _lastAccountPaidAmount = 0;
            DataTable _dtLastPayment = null;

            try
            {
                oParameters.Clear();
                oParameters.Add("@nPAccountID", PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("CL_LastAccountPaidAmount", oParameters, out _dtLastPayment);
                oDB.Disconnect();

                if (_dtLastPayment != null && _dtLastPayment.Rows.Count > 0)
                {
                    _lastAccountPaidAmount = Convert.ToDecimal(_dtLastPayment.Rows[0]["LastPay"]);
                }
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

            return _lastAccountPaidAmount;
        }

        public static bool IsClaimBillToSelf(DataTable dtEOB)
        {
            bool _result = false;

            try
            {
                //1. Check all charges in datatable for formula evaluation

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { 
            }

            return _result;
        }

        #endregion "Get Methods"

        #region "Intuit Bill Pay"

        public static bool CompleteIntuitBillPayTask(Int64 TaskId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nTaskID", TaskId, ParameterDirection.Input, SqlDbType.BigInt);
                int _returnresult = oDB.Execute("gsp_CompleteAll_Task", oParameters);

                if (_returnresult > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
            }
        }

        public static bool IsIntuitBillPayTaskOfSamePatient(Int64 TaskId, Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool _isIntuitBillPayTaskOfSamePatient = false;

            object _result = null;
            string _sqlQuery = string.Empty;
            
            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT DISTINCT " +
                            "        ISNULL(nPatientID, 0) AS nPatientID " +
                            " FROM    TM_TaskMST " +
                            " WHERE   nTaskID = " + TaskId;

                _result = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_result != null && Convert.ToString(_result) != "")
                {
                    if (Convert.ToInt64(_result) == PatientID)
                    {
                        _isIntuitBillPayTaskOfSamePatient = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
            }
            return _isIntuitBillPayTaskOfSamePatient;
        }

        #endregion "Intuit Bill Pay"

    }
}
