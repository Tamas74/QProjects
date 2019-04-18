using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using gloBilling.gloAccountPayment;
using gloGlobal;

namespace gloBilling
{

    public enum Actions
    {
        PAYMENT = 1,// Payments made by the patient that apply towards the plan balance.
        FEE = 2, //Fees paid by the patient that do not go against the plan balance.
        CREDIT = 3, //Credit/void (refund) or Reject of a previous PAYMENT.
        FEECREDIT = 4, //Credit (refund) or Reject of a previous FEE.
        DISCOUNT = 5, //Discount applied to a plan balance.
        ADJUSTMENT = 6, //Adjustment to a plan balance.
        PREFUNDDISCOUNT = 7, //Plan balance discount for prefunding.
        PREFUNDBUYOUT = 8 //Prefund payment of plan balance.        
    }
    public enum Status
    { 
        ReadytoPost=1,
        Posted=2

    }
    public enum PaymentMethod
    {
        CREDIT = 1,// Payment was made with a credit card or debit account.
        ACH = 2,//Payment was made with a checking or savings account.
        CASH = 3 //Payment was made with cash
    }
    public enum CPTCode
    {
        CGFEE=1
    }
    public enum CreditEventType
    {
        CREDIT = 1,
        VOID = 2,
        REJECT = 3
    }
    class ClsCleargagePaymentPosting
    {
       
            #region "Constructor"

            dsPaymentTVP_V2 dsAutoCleargagePayment_TVP = null;

          

            public ClsCleargagePaymentPosting()
            {
                dsAutoCleargagePayment_TVP = new dsPaymentTVP_V2();
            }

            #endregion

            #region "Dispose"

            public void Dispose()
            {
                if (dsAutoCleargagePayment_TVP != null)
                {
                    dsAutoCleargagePayment_TVP.Dispose();
                    dsAutoCleargagePayment_TVP = null;
                }
            }

            #endregion

            #region "Method"

            public static DataTable GetCleargagePaymentList(Int64 nCleargageFileID, Int64 nPAccountID,string sAction,string sPaymentMethod,int ActionType)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtResult = new DataTable();
                try
                {
                    oParameters.Clear();
                    //oParameters.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.Date);
                    // oParameters.Add("@dtEndDate", dtEndDate, ParameterDirection.Input, SqlDbType.Date);
                    oParameters.Add("@nCleargageFileID", nCleargageFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@Action", sAction, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@PaymentMethod", sPaymentMethod, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nActionType", ActionType, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Connect(false);
                    oDB.Retrive("Cleargage_GetPatientListDownloadedFile", oParameters, out dtResult);
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
            public bool UpdateStatusAsPosted(string nEncounterIDs, Int64 nCleargageFileID,string sTransactionID="",string sOriginalTransactionID="",string sReferenceNo="",string sAction="")
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                int bResult = 0;
                try
                {
                    oParameters.Clear();

                    oParameters.Add("@nEncounterID", nEncounterIDs, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nCleargageFileID", nCleargageFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sTransactionID", sTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sOriginalTransactionID", sOriginalTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sReferenceNo", sReferenceNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sAction",sAction, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Connect(false);
                    bResult = oDB.Execute("Cleargage_UpdateStatus", oParameters);
                    oDB.Disconnect();
                    if (bResult > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;

                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                }
                return false;
            }
            public bool UpdateMasterDetailsStatus(Int64 nCleargageFileID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                int bResult = 0;
                try
                {
                    oParameters.Clear();


                    oParameters.Add("@nCleargageFileID", nCleargageFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Connect(false);
                    bResult = oDB.Execute("Cleargage_UpdateMasterDetailsStatus", oParameters);
                    oDB.Disconnect();
                    if (bResult > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;
                    return false;
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                }
                return false;
            }
            public DataTable GetCleargageFileDetails(Int64 nCleargageFileID)
            {
                DataTable dtPaymentInfo = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBPara = new gloDatabaseLayer.DBParameters();
                try
                {
                    oDB.Connect(false);
                    oDBPara.Add("@nCleargageFileID", nCleargageFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("Cleargage_GetResponseFileDetails", oDBPara, out dtPaymentInfo);

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Dispose();
                        oDB = null;
                    }

                }
                return dtPaymentInfo;
            }
            public DataTable GetCleargagePaymentFileList(enumCheckStatus eCheckStatus)
            {
                DataTable dtPaymentInfo = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBPara = new gloDatabaseLayer.DBParameters();
                try
                {
                    oDB.Connect(false);
                    oDBPara.Add("@CheckStatus", eCheckStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDB.Retrive("Cleargage_ListDownloadedFile", oDBPara, out dtPaymentInfo);

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Dispose();
                        oDB = null;
                    }

                }
                return dtPaymentInfo;
            }
            public string GenerateCleargageFeeCharge(DataTable dtCreateFee)
            {
                string sError = "";
                Object outError = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                try
                {
                    if (oDB != null)
                    {
                        oDB.Connect(false);                        
                        oDBParameters.Add("@sMachineName", Convert.ToString(Environment.MachineName), ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@tvpCleargageFeeCharge", dtCreateFee, ParameterDirection.Input, SqlDbType.Structured);
                        oDBParameters.Add("@Message", sError, ParameterDirection.Output, SqlDbType.VarChar, 50000);
                        oDB.Execute("Cleargage_CleargageFeeCharge", oDBParameters, out outError);

                        sError = Convert.ToString(outError);
                    }
                }
                catch (Exception ex)
                {
                    sError = "Exception Occured : " + ex.Message;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                }
                finally
                {
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                    if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                }
                return sError;
            }
            public Int64 SavePaymentPostingDetails(Int64 nCGTransactionID, string PatientID, string PatientName, string PlanID, string TransactionID, string OriginalTransactionID, Decimal Amount, string PaymentMethod, string Action, DateTime TimeStamp, string TrackingID, string PaymentProfileID, string AccountType, string AccountNo, string ReferenceNo, Int64 nPaymentCreditID, string sEncounterID, Int64 nCleargageFileID, Int64 nCreditRefID = 0, Int64 nRefundID = 0)
            {
                Int64 CGTransactionID = 0;
                object objCGTransactionID = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                try
                {

                    oDB.Connect(false);
                    oDBParameters.Add("@nCGCreditID", nCGTransactionID, ParameterDirection.Output, SqlDbType.BigInt);
                    oDBParameters.Add("@sPatientID", PatientID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sPatientName", PatientName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sPlanID", PlanID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sOriginalTransactionID", OriginalTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@dAmount", Amount, ParameterDirection.Input, SqlDbType.Decimal);
                    oDBParameters.Add("@sPaymentMethod", PaymentMethod, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sAction", Action, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@dtTimeStamp", TimeStamp, ParameterDirection.Input, SqlDbType.DateTime);
                    oDBParameters.Add("@sTrackingID", TrackingID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sPaymentProfileID", PaymentProfileID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sAccountType", AccountType, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sAccountNumber", AccountNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sReference", ReferenceNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@nCreditID", nPaymentCreditID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sEncounterID", sEncounterID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@nCleargageFileID", nCleargageFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@bIsOneTimePayment", 0, ParameterDirection.Input, SqlDbType.Bit);
                    oDBParameters.Add("@nCreditRefID", nCreditRefID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nRefundID", nRefundID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Execute("Cleargage_INUP_CreditsTransaction", oDBParameters, out objCGTransactionID);
                    if (objCGTransactionID != null)
                    {
                        CGTransactionID = Convert.ToInt64(objCGTransactionID);
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                }
                finally
                {
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                    if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                }
                return CGTransactionID;
            }

            public DataTable CheckCleargageFeeCharge(DataTable dtCheckFee, out bool bIsFeeCreated)
            {
                 bIsFeeCreated = false;
                DataTable dtFees = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                try
                {
                    if (oDB != null)
                    {
                        oDB.Connect(false);                                               
                        oDBParameters.Add("@tvpCleargageFeeCharge", dtCheckFee, ParameterDirection.Input, SqlDbType.Structured);                        
                        oDB.Retrive("Cleargage_CheckFeeCharge", oDBParameters, out dtFees);
                        bIsFeeCreated = true;
                    }
                }
                catch (Exception ex)
                {
                    bIsFeeCreated = false;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                }
                finally
                {
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                    if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                }
                return dtFees;
            }
            public DataTable GetPatientPaymentDetails(Int64 nCleargageFileID, string nEncounterID, string sTransactionID = "", string sOriginalTransactionID = "", string sReferenceNo = "")
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
               DataTable dtResult=null;
                try
                {
                    oParameters.Clear();

                    oParameters.Add("@nCleargageFileID", nCleargageFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sTransactionID", sTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sOriginalTransactionID", sOriginalTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sReferenceNumber", sReferenceNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sEncounterID", nEncounterID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Connect(false);
                    oDB.Retrive("Cleargage_GetPatientPaymentDetails", oParameters, out dtResult);
                    oDB.Disconnect();
                    
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;

                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                }
                return dtResult;
            }

            #region "Iframe Transaction"
            public DataTable GetCleargageTransaction(Int64 nOTPTransactionID)
            {
                gloDatabaseLayer.DBLayer _DBLayer = null;
                gloDatabaseLayer.DBParameters _DBParameters = null;
                DataTable dtOTPTransaction = null;
                try
                {
                    _DBLayer = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    _DBParameters = new gloDatabaseLayer.DBParameters();
                    _DBParameters.Clear();
                    _DBParameters.Add("@nCGCreditID", nOTPTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBLayer.Connect(false);
                    _DBLayer.Retrive("Cleargage_GetCreditTransaction", _DBParameters, out dtOTPTransaction);
                    _DBLayer.Disconnect();

                }
                catch (Exception ex)
                {
                    _DBLayer.Disconnect();
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (_DBLayer != null) { _DBLayer.Dispose(); _DBLayer = null; }
                    if (_DBParameters != null) { _DBParameters.Dispose(); _DBParameters = null; }
                }
                return dtOTPTransaction;
            }
            public Int64 UpdateCleargageTransaction(Int64 nCGTransactionID, Int64 nCreditID, ClearGage.SSO.Response.Transaction objTransaction = null, bool bIsForVoid = false)
            {
                gloDatabaseLayer.DBLayer _DBLayer = null;
                gloDatabaseLayer.DBParameters _DBParameters = null;
                object _objCGTransactionID = null;
                try
                {
                    _DBLayer = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    _DBParameters = new gloDatabaseLayer.DBParameters();
                    _DBParameters.Clear();
                    _DBParameters.Add("@nCGCreditID", nCGTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@nCreditID", nCreditID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@bIsVoid", bIsForVoid == false ? 0 : 1, ParameterDirection.Input, SqlDbType.Bit);
                    _DBParameters.Add("@sVoidTransactionID", bIsForVoid == false ? DBNull.Value.ToString() : objTransaction.TransactionId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sVoidOriginalTransactionID", bIsForVoid == false ? DBNull.Value.ToString() : objTransaction.OriginalTransactionId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@dtVoidTimeStamp", bIsForVoid == false ? DBNull.Value.ToString() : objTransaction.Timestamp.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sVoidReference", bIsForVoid == false ? DBNull.Value.ToString() : objTransaction.Reference, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBLayer.Connect(false);
                    _DBLayer.Execute("Cleargage_Update_CreditsTransaction", _DBParameters, out _objCGTransactionID);
                    if (_objCGTransactionID != null || _objCGTransactionID != DBNull.Value || _objCGTransactionID.ToString() != "")
                    {
                        nCGTransactionID = Convert.ToInt64(_objCGTransactionID);
                    }
                    _DBLayer.Disconnect();
                }
                catch (Exception ex)
                {
                    _DBLayer.Disconnect();
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (_DBLayer != null) { _DBLayer.Dispose(); _DBLayer = null; }
                    if (_DBParameters != null) { _DBParameters.Dispose(); _DBParameters = null; }
                }
                return nCGTransactionID;
            }

            public Int64 SaveCleargageTransaction(ClearGage.SSO.Response.Transaction objTransaction)
            {
                gloDatabaseLayer.DBLayer _DBLayer = null;
                gloDatabaseLayer.DBParameters _DBParameters = null;
                object _objCGTransactionID = null;
                Int64 nCGTransactionID = 0;
                try
                {
                    _DBLayer = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    _DBParameters = new gloDatabaseLayer.DBParameters();
                    _DBParameters.Clear();
                    _DBParameters.Add("@nCGCreditID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    _DBParameters.Add("@sPatientID", objTransaction.PatientId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sPatientName", objTransaction.PatientName, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sPlanID", objTransaction.PlanId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sTransactionID", objTransaction.TransactionId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sOriginalTransactionID", objTransaction.OriginalTransactionId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@dAmount", objTransaction.Amount, ParameterDirection.Input, SqlDbType.Decimal);
                    _DBParameters.Add("@sPaymentMethod", objTransaction.PayMethod, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sAction", objTransaction.Action, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@dtTimeStamp", objTransaction.Timestamp, ParameterDirection.Input, SqlDbType.DateTime);
                    _DBParameters.Add("@sTrackingID", objTransaction.TrackingId == null ? DBNull.Value.ToString() : objTransaction.TrackingId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sPaymentProfileID", objTransaction.PaymentProfileId == null ? DBNull.Value.ToString() : objTransaction.PaymentProfileId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sAccountType", objTransaction.AccountType == null ? DBNull.Value.ToString() : objTransaction.AccountType, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sAccountNumber", objTransaction.AccountNumber == null ? DBNull.Value.ToString() : objTransaction.AccountNumber, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sReference", objTransaction.Reference, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@bIsOneTimePayment", 1, ParameterDirection.Input, SqlDbType.Bit);
                    _DBLayer.Connect(false);
                    _DBLayer.Execute("Cleargage_INUP_CreditsTransaction", _DBParameters, out _objCGTransactionID);
                    if (_objCGTransactionID != null || _objCGTransactionID != DBNull.Value || _objCGTransactionID.ToString() != "")
                    {
                        nCGTransactionID = Convert.ToInt64(_objCGTransactionID);
                    }
                    _DBLayer.Disconnect();

                }
                catch (Exception ex)
                {
                    _DBLayer.Disconnect();
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (_DBLayer != null) { _DBLayer.Dispose(); _DBLayer = null; }
                    if (_DBParameters != null) { _DBParameters.Dispose(); _DBParameters = null; }
                }
                return nCGTransactionID;
            }

            public Int64 SaveCleargageFEECREDITTransaction(ClearGage.SSO.Response.Transaction objTransaction,Int64 nCGCreditID,Int64 nCreditID,Int64 nCGFileID,string sCGEncounterID, Int64 nCGVoidPaymentTrayID)
            {
                gloDatabaseLayer.DBLayer _DBLayer = null;
                gloDatabaseLayer.DBParameters _DBParameters = null;
                object _objCGTransactionID = null;
                Int64 nCGTransactionID = 0;
                try
                {
                    _DBLayer = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    _DBParameters = new gloDatabaseLayer.DBParameters();
                    _DBParameters.Clear();
                    _DBParameters.Add("@nCGFeeCreditID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    _DBParameters.Add("@nCleargageFileID", nCGFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@nCGCreditID", nCGCreditID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@nCreditID", nCreditID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@sEncounterID", sCGEncounterID, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sPatientID", objTransaction.PatientId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sPatientName", objTransaction.PatientName, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sPlanID", objTransaction.PlanId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sTransactionID", objTransaction.TransactionId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sOriginalTransactionID", objTransaction.OriginalTransactionId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@dAmount", objTransaction.Amount, ParameterDirection.Input, SqlDbType.Decimal);
                    _DBParameters.Add("@sPaymentMethod", objTransaction.PayMethod, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sAction", objTransaction.Action, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@dtTimeStamp", objTransaction.Timestamp, ParameterDirection.Input, SqlDbType.DateTime);
                    _DBParameters.Add("@sTrackingID", objTransaction.TrackingId == null ? DBNull.Value.ToString() : objTransaction.TrackingId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sPaymentProfileID", objTransaction.PaymentProfileId == null ? DBNull.Value.ToString() : objTransaction.PaymentProfileId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sAccountType", objTransaction.AccountType == null ? DBNull.Value.ToString() : objTransaction.AccountType, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sAccountNumber", objTransaction.AccountNumber == null ? DBNull.Value.ToString() : objTransaction.AccountNumber, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sReference", objTransaction.Reference, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@nCGVoidPaymentTrayID", nCGVoidPaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    
                    _DBLayer.Connect(false);
                    _DBLayer.Execute("Cleargage_INUP_FEECREDIT_Transaction", _DBParameters, out _objCGTransactionID);
                    if (_objCGTransactionID != null || _objCGTransactionID != DBNull.Value || _objCGTransactionID.ToString() != "")
                    {
                        nCGTransactionID = Convert.ToInt64(_objCGTransactionID);
                    }
                    _DBLayer.Disconnect();

                }
                catch (Exception ex)
                {
                    _DBLayer.Disconnect();
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (_DBLayer != null) { _DBLayer.Dispose(); _DBLayer = null; }
                    if (_DBParameters != null) { _DBParameters.Dispose(); _DBParameters = null; }
                }
                return nCGTransactionID;
            }

            public bool CheckCleargagePayment(long nCreditID, out DataTable dtCleargageDetails)
            {
                gloDatabaseLayer.DBLayer oDB = null;
                gloDatabaseLayer.DBParameters oParameters = null;
                bool bIsCleargagePayment = false;
                dtCleargageDetails = null;
                try
                {
                    oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    oParameters = new gloDatabaseLayer.DBParameters();

                    oParameters.Add("@nCreditID", nCreditID, ParameterDirection.Input, SqlDbType.BigInt);//NUMERIC(18,0)
                    oDB.Connect(false);
                    oDB.Retrive("Cleargage_GetClearggageCreditTransactionByCreditID", oParameters, out dtCleargageDetails);
                    oDB.Disconnect();

                    if (dtCleargageDetails != null && dtCleargageDetails.Rows.Count > 0)
                    {
                        bIsCleargagePayment = true;
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
                    if (oDB != null)
                        oDB.Dispose();
                    if (oParameters != null)
                        oParameters.Dispose();
                }
                return bIsCleargagePayment;
            }

            public DataTable GetPatientEncounterDetails(Int64 nPatientID, Int64 nPAccountID)
            {
                gloDatabaseLayer.DBLayer _DBLayer = null;
                gloDatabaseLayer.DBParameters _DBParameters = null;
                DataTable dtOTPTransaction = null;
                try
                {
                    _DBLayer = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    _DBParameters = new gloDatabaseLayer.DBParameters();
                    _DBParameters.Clear();
                    _DBParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBLayer.Connect(false);
                    _DBLayer.Retrive("gsp_CG_GetPatientDueInformationToAddPaymentPlan", _DBParameters, out dtOTPTransaction);
                    _DBLayer.Disconnect();

                }
                catch (Exception ex)
                {
                    _DBLayer.Disconnect();
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (_DBLayer != null) { _DBLayer.Dispose(); _DBLayer = null; }
                    if (_DBParameters != null) { _DBParameters.Dispose(); _DBParameters = null; }
                }
                return dtOTPTransaction;
            }
            #endregion

            #region "Cleargage File History"

            public DataTable GetCleargageUploadedFiles()
            {
                DataTable dtGetAllUploadedFiles = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                try
                {
                    if (oDB != null)
                    {
                        oDB.Connect(false);
                        oDB.Retrive("Cleargage_GetUploadedFiles", oDBParameters, out dtGetAllUploadedFiles);
                    }
                }
                catch (Exception ex)
                {
                    dtGetAllUploadedFiles = null;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                }
                finally
                {
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                    if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                }
                return dtGetAllUploadedFiles;
            }

            public DataTable GetCleargageUploadedFileDetails(Int64 nProcessID)
            {
                DataTable dtGetUploadedFileDetails = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                try
                {
                    if (oDB != null)
                    {
                        oDB.Connect(false);
                        oDBParameters.Add("@nProcessID", nProcessID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Retrive("Cleargage_GetUploadedFileDetails", oDBParameters, out dtGetUploadedFileDetails);
                    }
                }
                catch (Exception ex)
                {
                    dtGetUploadedFileDetails = null;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                }
                finally
                {
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                    if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                }
                return dtGetUploadedFileDetails;
            }

            public DataTable GetAcknowledgementBinaryFile(Int64 nUploadedFileID)
            {
                DataTable dtGetAcknowledgementBinaryFile = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                try
                {
                    if (oDB != null)
                    {
                        oDB.Connect(false);
                        oDBParameters.Add("@nUploadedFileID", nUploadedFileID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Retrive("Cleargage_GetAcknowledgementBinaryFile", oDBParameters, out dtGetAcknowledgementBinaryFile);
                    }
                }
                catch (Exception ex)
                {
                    dtGetAcknowledgementBinaryFile = null;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                }
                finally
                {
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                    if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                }
                return dtGetAcknowledgementBinaryFile;
            }

            #endregion

            #region "Correction"

            public DataTable GetCleargagePaymentList_Correction(Int64 nCleargageFileID, Int64 nPAccountID, string sAction)//,Int64 nPAccountID,string sPaymentMethod,int ActionType)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtResult = new DataTable();
                try
                {
                    oParameters.Clear();
                    //oParameters.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.Date);
                    // oParameters.Add("@dtEndDate", dtEndDate, ParameterDirection.Input, SqlDbType.Date);
                    oParameters.Add("@nCleargageFileID", nCleargageFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@Action", sAction, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Connect(false);
                    oDB.Retrive("Cleargage_GetPatientListDownloadedFile_Correction", oParameters, out dtResult);
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

            public string SaveCleargagePaymentList_Correction(Int64 nPatientID, Int64 nPAccountID, Int64 nGuarantorID, Int64 nAccountPatientID, Int64 nCreditRefID, Int64 nExistingReserveCreditID, Int64 nExistingReserveID, Decimal dReserveRefundAmount, Decimal dCorrectionAmount, DateTime dtCloseDate, Int64 nPaymentTrayID, String sPaymentTrayDesc, DateTime dtReceiptDate, String sAccountNo, Int32 nPaymentMode, String sCreditCardType, String sCGReferenceNo, String sCGOriginalTransactionID, out Int64 nOUTRefundCreditID, out Int64 nOUTRefundID)
            {
                string sOutput = "";
                nOUTRefundCreditID = 0;
                nOUTRefundID = 0;
                Object outmsg = null;
                Object objOUTRefundCreditID = null;
                Object objOUTRefundID = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                try
                {
                    oDB.Connect(false);
                    oDBParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nPAccountID",nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nGuarantorID", nGuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nAccountPatientID", nAccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nCreditRefID", nCreditRefID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nExistingReserveCreditID", nExistingReserveCreditID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nExistingReserveID", nExistingReserveID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@dReserveRefundAmount", dReserveRefundAmount, ParameterDirection.Input, SqlDbType.Decimal);
                    oDBParameters.Add("@dCorrectionAmount", dCorrectionAmount, ParameterDirection.Input, SqlDbType.Decimal);
                    oDBParameters.Add("@dtCloseDate", dtCloseDate, ParameterDirection.Input, SqlDbType.DateTime);
                    oDBParameters.Add("@nPaymentTrayID", nPaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sPaymentTrayDesc", sPaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@dtReceiptDate", dtReceiptDate, ParameterDirection.Input, SqlDbType.DateTime);
                    oDBParameters.Add("@sAccountNo", sAccountNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@nPaymentMode", nPaymentMode, ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@sCreditCardType", sCreditCardType, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sCGReferenceNo", sCGReferenceNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@sMachineName", Convert.ToString(Environment.MachineName), ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@bShowBadDebt", 0, ParameterDirection.Input, SqlDbType.Bit);
                    oDBParameters.Add("@nOutRefundCreditID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@nOutRefundID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@sOutput", sOutput, ParameterDirection.Output, SqlDbType.VarChar, 8000);
                    oDBParameters.Add("@sCGOriginalTransactionID", sCGOriginalTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Execute("Cleargage_PaymentCorrection", oDBParameters, out objOUTRefundCreditID, out objOUTRefundID, out outmsg);

                    sOutput = Convert.ToString(outmsg);
                    nOUTRefundCreditID = Convert.ToInt64(objOUTRefundCreditID);
                    nOUTRefundID = Convert.ToInt64(objOUTRefundID);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                }
                finally
                {
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                    if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                }
                return sOutput;
            }

            public Int64 SaveCleargageFEECREDITTransaction(Int64 nCGCreditID, Int64 nCreditID, Int64 nCGFileID, string sCGEncounterID, Int64 PatientId, string PatientName, string PlanId, string TransactionId, string OriginalTransactionId, decimal Amount, string PayMethod, string Action, DateTime dtTimestamp, string AccountType, string AccountNumber, string Reference, Int64 nCGVoidPaymentTrayID, bool bIsJsonFeeCredit)
            {
                gloDatabaseLayer.DBLayer _DBLayer = null;
                gloDatabaseLayer.DBParameters _DBParameters = null;
                object _objCGTransactionID = null;
                Int64 nCGTransactionID = 0;
                try
                {
                    _DBLayer = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    _DBParameters = new gloDatabaseLayer.DBParameters();
                    _DBParameters.Clear();
                    _DBParameters.Add("@nCGFeeCreditID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    _DBParameters.Add("@nCleargageFileID", nCGFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@nCGCreditID", nCGCreditID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@nCreditID", nCreditID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@sEncounterID", sCGEncounterID, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sPatientID", PatientId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sPatientName", PatientName, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sPlanID", PlanId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sTransactionID", TransactionId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sOriginalTransactionID", OriginalTransactionId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@dAmount", Amount, ParameterDirection.Input, SqlDbType.Decimal);
                    _DBParameters.Add("@sPaymentMethod", PayMethod, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sAction", Action, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@dtTimeStamp", dtTimestamp, ParameterDirection.Input, SqlDbType.DateTime);
                    _DBParameters.Add("@sTrackingID", "", ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sPaymentProfileID", "", ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sAccountType", AccountType, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sAccountNumber", AccountNumber, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sReference", Reference, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@nCGVoidPaymentTrayID", nCGVoidPaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@bIsJsonFeeCredit", bIsJsonFeeCredit, ParameterDirection.Input, SqlDbType.Bit);
                    _DBLayer.Connect(false);
                    _DBLayer.Execute("Cleargage_INUP_FEECREDIT_Transaction", _DBParameters, out _objCGTransactionID);
                    if (_objCGTransactionID != null || _objCGTransactionID != DBNull.Value || _objCGTransactionID.ToString() != "")
                    {
                        nCGTransactionID = Convert.ToInt64(_objCGTransactionID);
                    }
                    _DBLayer.Disconnect();

                }
                catch (Exception ex)
                {
                    _DBLayer.Disconnect();
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (_DBLayer != null) { _DBLayer.Dispose(); _DBLayer = null; }
                    if (_DBParameters != null) { _DBParameters.Dispose(); _DBParameters = null; }
                }
                return nCGTransactionID;
            }

            public Int64 UpdateCleargageTransaction(Int64 nCGTransactionID, Int64 nCreditID, string TransactionId, string OriginalTransactionId, string Timestamp, string Reference, bool bIsForVoid = false)
            {
                gloDatabaseLayer.DBLayer _DBLayer = null;
                gloDatabaseLayer.DBParameters _DBParameters = null;
                object _objCGTransactionID = null;
                try
                {
                    _DBLayer = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    _DBParameters = new gloDatabaseLayer.DBParameters();
                    _DBParameters.Clear();
                    _DBParameters.Add("@nCGCreditID", nCGTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@nCreditID", nCreditID, ParameterDirection.Input, SqlDbType.BigInt);
                    _DBParameters.Add("@bIsVoid", bIsForVoid == false ? 0 : 1, ParameterDirection.Input, SqlDbType.Bit);
                    _DBParameters.Add("@sVoidTransactionID", bIsForVoid == false ? DBNull.Value.ToString() : TransactionId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sVoidOriginalTransactionID", bIsForVoid == false ? DBNull.Value.ToString() : OriginalTransactionId, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@dtVoidTimeStamp", bIsForVoid == false ? DBNull.Value.ToString() : Timestamp.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                    _DBParameters.Add("@sVoidReference", bIsForVoid == false ? DBNull.Value.ToString() : Reference, ParameterDirection.Input, SqlDbType.VarChar);
                    _DBLayer.Connect(false);
                    _DBLayer.Execute("Cleargage_Update_CreditsTransaction", _DBParameters, out _objCGTransactionID);
                    if (_objCGTransactionID != null || _objCGTransactionID != DBNull.Value || _objCGTransactionID.ToString() != "")
                    {
                        nCGTransactionID = Convert.ToInt64(_objCGTransactionID);
                    }
                    _DBLayer.Disconnect();
                }
                catch (Exception ex)
                {
                    _DBLayer.Disconnect();
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (_DBLayer != null) { _DBLayer.Dispose(); _DBLayer = null; }
                    if (_DBParameters != null) { _DBParameters.Dispose(); _DBParameters = null; }
                }
                return nCGTransactionID;
            }

            #endregion

            public static DataTable GetViewPaymentHistory(string sPatientCode, string sTransactionID, string sOriginalTransactionID, string sReferenceNo,string sEncounterID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtResult = new DataTable();
                try
                {
                    oParameters.Clear();
                    oParameters.Add("@PatientCode", sPatientCode, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sTransactionID", sTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sOriginalTransactionID", sOriginalTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sReferenceNo", sReferenceNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sEncounterID", sEncounterID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Connect(false);
                    oDB.Retrive("Cleargage_ViewPaymentHistory", oParameters, out dtResult);
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

        #region "Notes"
            public Int64 SaveCGNote(Int64 nNoteID, Int64 nReferenceID, string sNoteDescription, DateTime _NoteDate)
            {
                Object oResult = null;
                Int64 _Result = 0;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                try
                {
                    if (oDB.Connect(false))
                    {
                        oParameters.Clear();
                        oParameters.Add("@nNoteID", nNoteID, ParameterDirection.InputOutput, SqlDbType.BigInt);                       
                        oParameters.Add("@nPaymentTransactionID", nReferenceID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@sNote", sNoteDescription, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@nNoteDate", gloDateMaster.gloDate.DateAsNumber(_NoteDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Execute("CG_INUP_NOTES", oParameters, out oResult);                     
                        if (oResult != null && oResult.ToString() != "")
                        {
                            _Result = Convert.ToInt64(oResult);
                            if (nNoteID == 0)
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, "Payment Transaction Note Added ", 0, nReferenceID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                            else
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, "Payment Transaction Note Modified ", 0, nReferenceID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                        }
                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                return _Result;
            }

         public DataTable GetCGNotes(Int64 nPaymentTransactionID)
            {
                DataTable _dt = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                try
                {
                    if (oDB.Connect(false))
                    {
                        oParameters.Clear();
                        oParameters.Add("@nPaymentTransactionID", nPaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Retrive("CG_GetNotes", oParameters, out _dt);

                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                }
                return _dt;
            }
        #endregion

        #region "File Lock"
         public bool IsFileLocked(Int64 nCleargageFileID, out string sMessage)
         {
             bool bIsLocked = false;
             gloDatabaseLayer.DBLayer oDB = null; 
             gloDatabaseLayer.DBParameters oParameters = null; 
             DataTable dt=new DataTable();
             sMessage = string.Empty;
             try
             {
                 oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                 oParameters = new gloDatabaseLayer.DBParameters();
                 oParameters.Clear();

                 oParameters.Add("@nCleargageFileID", nCleargageFileID, ParameterDirection.Input, SqlDbType.BigInt);
                 oParameters.Add("@sMachineName", Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                 oDB.Connect(false);
                 oDB.Retrive("Cleargage_CheckFileIsLocked", oParameters, out dt);
                 oDB.Disconnect();
                 if (dt!=null&&dt.Rows.Count>0)
                 {
                     bIsLocked = true;
                     sMessage = string.Format("File is already open for posting by user {0} on machine {1}", Convert.ToString(dt.Rows[0]["sUserName"]), Convert.ToString(dt.Rows[0]["sMachineName"]));
                 }
             }
             catch (Exception ex)
             {
                 gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                 ex = null;
                 return false;
             }
             finally
             {
                 if (oDB != null) { oDB.Dispose(); oDB = null; }
                 if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
             }
             return bIsLocked;
         }
         public Int64 InsertDeleteLockedFile(Int64 nCleargageFileID, Int64 nUserID, int nFlag, string sUserName, string sMachineName)
         {
             Int64 nFileLockID = 0;
             gloDatabaseLayer.DBLayer oDB = null;
             gloDatabaseLayer.DBParameters oParameters = null;
             object objFileID=null;
             try
             {
                 oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                 oParameters = new gloDatabaseLayer.DBParameters();
                 oParameters.Clear();

                 oParameters.Add("@nFileLockedID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                 oParameters.Add("@nCleargageFileID", nCleargageFileID, ParameterDirection.Input, SqlDbType.BigInt);
                 oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                 oParameters.Add("@nFlag", nFlag, ParameterDirection.Input, SqlDbType.Int);
                 oParameters.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                 oParameters.Add("@sMachineName", sMachineName, ParameterDirection.Input, SqlDbType.VarChar);

                 oDB.Connect(false);
                 oDB.Execute("Cleargage_InsertDeleteLockedFile", oParameters,out objFileID);
                 oDB.Disconnect();
                 if (objFileID != null && Convert.ToInt64(objFileID)!=0)
                 {
                     nFileLockID = Convert.ToInt64(objFileID);
                 }
             }
             catch (Exception ex)
             {
                 gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                 ex = null;
                 return 0;
             }
             finally
             {
                 if (oDB != null) { oDB.Dispose(); oDB = null; }
                 if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
             }
               

             return nFileLockID;
         }
        #endregion

    }   
   
}
