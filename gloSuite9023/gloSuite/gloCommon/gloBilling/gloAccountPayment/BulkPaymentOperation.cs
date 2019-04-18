using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloDatabaseLayer;
using System.Data;
using System.ComponentModel;
using gloAuditTrail;

namespace gloBilling.gloAccountPayment
{
    public class PaymentInfoParameter: IDisposable
    {
        private readonly Int64 accountId = 0;
        private readonly Int64 guarantorID = 0;
        private readonly Int64 accountPatientID = 0;
        private readonly Int64 patientID = 0;
        private readonly Int64 paymentTrayID = 0;
        private readonly string paymentTrayName = string.Empty;
        private readonly DateTime dtcloseDate = DateTime.MinValue;
        private readonly Int64 ncloseDate = 0;
        private readonly string adjustmentreasoncode = string.Empty;
        private readonly string adjustmentreasondescription = string.Empty;
        private readonly string checkAmountToDistribute = string.Empty;

        [ReadOnly(true)]
        [Description("Read only property AccountID")]
        public Int64 AccountID { get { return this.accountId; } }

        [ReadOnly(true)]
        [Description("Read only property GuarantorID")]
        public Int64 GuarantorID { get { return this.guarantorID; } }

        [ReadOnly(true)]
        [Description("Read only property AccountPatientID")]
        public Int64 AccountPatientID { get { return this.accountPatientID; } }

        [ReadOnly(true)]
        [Description("Read only property PatientID")]
        public Int64 PatientID { get { return this.patientID; } }

        [ReadOnly(true)]
        [Description("Read only property PaymentTrayID")]
        public Int64 PaymentTrayID { get { return this.paymentTrayID; } }

        [ReadOnly(true)]
        [Description("Read only property PaymentTrayName")]
        public string PaymentTrayName { get { return this.paymentTrayName; } }

        [ReadOnly(true)]
        [Description("Read only property CloseDate")]
        public DateTime CloseDate { get { return this.dtcloseDate; } }

        [ReadOnly(true)]
        [Description("Read only property CloseDateAsNumber")]
        public Int64 CloseDateAsNumber { get { return ncloseDate; } }

        [ReadOnly(true)]
        [Description("Read only property AdjustmentReasonCode")]
        public string AdjustmentReasonCode { get { return this.adjustmentreasoncode; } }

        [ReadOnly(true)]
        [Description("Read only property AdjustmentReasonCodeDescription")]
        public string AdjustmentReasonCodeDescription { get { return this.adjustmentreasondescription; } }

        [ReadOnly(true)]
        [Description("Read only property sCheckAmountToDistribute")]
        public string CheckAmountToDistribute { get { return this.checkAmountToDistribute; } }

        public PaymentInfoParameter(Int64 patientId, Int64 accountId, Int64 guarantorId, Int64 accountpatientId, 
            DateTime closedate, Int64 paymenttrayId, string paymenttrayName,
            string reasonCode, string reasonDescription, string CheckAmount= "")
        {
            this.patientID = 0;
            this.accountId = 0;
            this.guarantorID = 0;
            this.accountPatientID = 0;
            this.dtcloseDate = DateTime.MinValue;
            this.ncloseDate = 0; 
            this.paymentTrayID = 0;
            this.paymentTrayName = string.Empty;
            this.adjustmentreasoncode = string.Empty;
            this.adjustmentreasondescription = string.Empty;

            //...........................................

            this.patientID = patientId;
            this.accountId = accountId;
            this.guarantorID = guarantorId;
            this.accountPatientID = accountpatientId;
            this.dtcloseDate = closedate;

            if (this.dtcloseDate != null)
            { this.ncloseDate = gloDateMaster.gloDate.DateAsNumber(this.dtcloseDate.ToShortDateString()); }
            else
            { this.ncloseDate = 0; }

            this.paymentTrayID = paymenttrayId;
            this.paymentTrayName = paymenttrayName;

            this.adjustmentreasoncode = reasonCode;
            this.adjustmentreasondescription = reasonDescription;
            this.checkAmountToDistribute = CheckAmount;

            ValidateParameters();
        }

        private void ValidateParameters()
        {
            try
            {
                if (  this.AccountID == 0 || this.AccountID < 0)
                { throw new Exception("Invalid value for parameter AccountID"); }

                if (  this.PatientID == 0 || this.PatientID < 0)
                { throw new Exception("Invalid value for parameter PatientID"); }

                if (  this.GuarantorID == 0 || this.GuarantorID < 0)
                { throw new Exception("Invalid value for parameter GuarantorID"); }

                if (  this.AccountPatientID == 0 || this.AccountPatientID < 0)
                { throw new Exception("Invalid value for parameter AccountPatientID"); }

                if (this.CloseDate == null || this.CloseDate == DateTime.MinValue)
                { throw new Exception("Invalid value for parameter CloseDate"); }

                if ( this.CloseDateAsNumber == 0 || this.CloseDateAsNumber < 0)
                { throw new Exception("Invalid value for parameter CloseDateAsNumber"); }

                if (  this.PaymentTrayID == 0 || this.PaymentTrayID < 0)
                { throw new Exception("Invalid value for parameter PaymentTrayID"); }

                if (this.PaymentTrayName == null || this.PaymentTrayName == string.Empty)
                { throw new Exception("Invalid value for parameter PaymentTrayName"); }

                if (this.AdjustmentReasonCode == null || this.AdjustmentReasonCode == string.Empty)
                { throw new Exception("Invalid value for parameter AdjustmentReasonCode"); }

                if (this.AdjustmentReasonCodeDescription == null || this.AdjustmentReasonCodeDescription == string.Empty)
                { throw new Exception("Invalid value for parameter AdjustmentReasonCodeDescription"); }               
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        { }
        
    }

    public class AccountOwnerInfo : IDisposable
    {
        private readonly Int64 accountId = 0;
        private readonly string accountNo = string.Empty;
        private readonly Int64 guarantorId = 0;
        private readonly Int64 accountpatientId = 0;
        private readonly Int64 accountownerpatientId = 0;

        public Int64 AccountId
        { get { return accountId; } }

        public string AccountNo
        { get { return accountNo; } }

        public Int64 GuarantorId
        { get { return guarantorId; } }

        public Int64 AccountpatientId
        { get { return accountpatientId; } }

        public Int64 AccountownerpatientId
        { get { return accountownerpatientId; } }

        public AccountOwnerInfo(Int64 accountid, string accountno, Int64 gurantorid, Int64 accountpatientid, Int64 patientid)
        {
            this.accountId = 0;
            this.accountNo = string.Empty;
            this.guarantorId = 0;
            this.accountpatientId = 0;
            this.accountownerpatientId = 0;

            this.accountId = accountid;
            this.accountNo = accountno;
            this.guarantorId = gurantorid;
            this.accountpatientId = accountpatientid;
            this.accountownerpatientId = patientid;
        }

        public void Dispose()
        {
            
        }
    }

    public class BulkPaymentOperation: IDisposable
    {
        private dsPaymentTVP_V2 dsBulkPatientPayment_TVP = null;
        //private gloAccountsV2.PaymentModeV2 paymentMode = gloAccountsV2.PaymentModeV2.None;
        
        public BulkPaymentOperation()
        {
            dsBulkPatientPayment_TVP = new dsPaymentTVP_V2();
            //paymentMode = gloAccountsV2.PaymentModeV2.None;
        }

        public void Dispose()
        {
            if (dsBulkPatientPayment_TVP != null)
            { dsBulkPatientPayment_TVP.Dispose(); dsBulkPatientPayment_TVP = null; }
        }

        public Boolean WriteOffAccountDue(PaymentInfoParameter paymentInfoParameter)
        {
            DataTable dtPatientBadDebtDueCharges = null;
            DataTable dtUniqueID = null;
            Int64 paymentCreditId = 0;
            Int64 paymentEOBId = 0;
            Int64 AccountOwnerPatientId = 0;
            bool bIsWriteOffAccountDue = false;
            gloAccountsV2.gloPatientPaymentV2 objPatientPayment = null;

            try
            {
                if (paymentInfoParameter != null)
                {
                    if (this.dsBulkPatientPayment_TVP != null)
                    {
                        this.dsBulkPatientPayment_TVP = null;
                        this.dsBulkPatientPayment_TVP = new dsPaymentTVP_V2();
                    }

                    AccountOwnerPatientId = GetAccountOwnerPatientId(paymentInfoParameter.AccountID);

                    //TODO: Verify close date 
                    
                    if (AccountOwnerPatientId > 0)
                    {
                        dtPatientBadDebtDueCharges = GetAccountTransaction(paymentInfoParameter.AccountID);

                        if (dtPatientBadDebtDueCharges != null && dtPatientBadDebtDueCharges.Rows.Count > 0)
                        {
                            dtUniqueID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                            if (dtUniqueID != null && dtUniqueID.Rows.Count > 0)
                            {
                                paymentCreditId = Convert.ToInt64(dtUniqueID.Rows[0]["ID"].ToString());
                                paymentEOBId = Convert.ToInt64(dtUniqueID.Rows[0]["ID2"].ToString());
                            }

                            //Write Credit and Credit_EXT info to TVP
                            GenerateCreditEntry(paymentCreditId, paymentInfoParameter);
                            GenerateMasterNoteEntry(paymentCreditId, paymentInfoParameter);
                            GeneratePaymentDetailsEntry(dtPatientBadDebtDueCharges, paymentCreditId, paymentEOBId, paymentInfoParameter);

                            if (this.dsBulkPatientPayment_TVP != null)
                            {
                                Int64 returnPaymentId = 0;
                                objPatientPayment = new gloAccountsV2.gloPatientPaymentV2();
                                returnPaymentId = objPatientPayment.SavePatientPayment(this.dsBulkPatientPayment_TVP);
                                bIsWriteOffAccountDue = true;  
                            }
                        }
                    }
                    else
                    { bIsWriteOffAccountDue = false; throw new Exception("Error at WriteOffPatientDue method, AccountOwnerPatientId is zero or null"); }
                }
                else
                { bIsWriteOffAccountDue = false; throw new Exception("Error at WriteOffPatientDue method, paymentInfoParameter is null"); }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                bIsWriteOffAccountDue = false;
            }
            finally
            {
                if (objPatientPayment != null) { objPatientPayment.Dispose(); objPatientPayment = null; }
                if (dtPatientBadDebtDueCharges != null) { dtPatientBadDebtDueCharges.Dispose(); dtPatientBadDebtDueCharges = null; }
            }
            return bIsWriteOffAccountDue;
        }

        private Int64 GetAccountOwnerPatientId(Int64 AccountId)
        {
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oParameters = null;
            Int64 accountownerpatientId = 0;
            string sqlQueryString = string.Empty;
            Object retValue = null;

            try
            {
                if (AccountId > 0)
                {
                    sqlQueryString = "SELECT nPatientID FROM PA_Accounts_Patients WHERE nPAccountID = " + AccountId + " AND ISNULL(bIsOwnAccount,0) = 1";

                    oDBLayer = new DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    oDBLayer.Connect(false);

                    retValue = oDBLayer.ExecuteScalar_Query(sqlQueryString);

                    oDBLayer.Disconnect();

                    if (retValue != null && Convert.ToString(retValue).Trim() != "")
                    { Int64.TryParse(Convert.ToString(retValue), out accountownerpatientId); }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (oParameters != null) { oParameters.Clear(); oParameters.Dispose(); oParameters = null; }
                if (oDBLayer != null) { oDBLayer.Dispose(); oDBLayer = null; }
            }

            return accountownerpatientId;
        }

        private DataTable GetAccountTransaction(Int64 Accountid)
        {
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable dtTransaction = null;

            try
            {
                if (Accountid > 0)
                {
                    oDBLayer = new DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    oDBLayer.Connect(false);

                    oParameters = new DBParameters();
                    oParameters.Add(new DBParameter
                    {
                        ParameterName = "@nPAccountID",
                        Value = Accountid,
                        DataType = System.Data.SqlDbType.BigInt,
                        ParameterDirection = System.Data.ParameterDirection.Input
                    });

                    oDBLayer.Retrive("BL_SELECT_Transaction_Lines_ForAccount", oParameters, out dtTransaction);

                    oDBLayer.Disconnect();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (oParameters != null) { oParameters.Clear(); oParameters.Dispose(); oParameters = null; }
                if (oDBLayer != null) { oDBLayer.Dispose(); oDBLayer = null; }
            }

            return dtTransaction;
        }

        private void GenerateCreditEntry(Int64 creditId, PaymentInfoParameter paymentInfoParameter)
        {
            Int32 currentRowIndex = 0;
            string creditTableName = "Credits";
            //DataTable _dtUniquePaymentPrfixNumber = null;

            try
            {
                if (creditId > 0 && paymentInfoParameter.PatientID > 0 && paymentInfoParameter.AccountID > 0 && paymentInfoParameter.PaymentTrayID > 0
                    && paymentInfoParameter.CloseDate != DateTime.MinValue)
                {
                    if (this.dsBulkPatientPayment_TVP != null)
                    {
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows.Add();
                        currentRowIndex = this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows.Count - 1;

                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nCreditID"] = creditId;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sReceiptNo"] = "BULKWRITEOFF";
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["dReceiptAmount"] = Convert.ToDecimal("0.00");
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["dtReceiptDate"] = paymentInfoParameter.CloseDate;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nPayerType"] = gloAccountsV2.PayerTypeV2.Self.GetHashCode();
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nPayerID"] = paymentInfoParameter.PatientID;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sPayerName"] = "";
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sPaymentNo"] = Convert.ToString(creditId);
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nPaymentMode"] = gloAccountsV2.PaymentModeV2.Voucher;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["dtCloseDate"] = paymentInfoParameter.CloseDate;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nPaymentTrayID"] = paymentInfoParameter.PaymentTrayID;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sPaymentTrayDesc"] = paymentInfoParameter.PaymentTrayName;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sUserName"] = gloGlobal.gloPMGlobal.UserName;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["bIsPaymentVoid"] = 0;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nVoidType"] = 0;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["dtPaymentVoidCloseDate"] = DBNull.Value;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nPAccountID"] = paymentInfoParameter.AccountID;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nAccountPatientID"] = paymentInfoParameter.AccountPatientID;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nGuarantorID"] = paymentInfoParameter.GuarantorID;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sPaymentNote"] = "Bad Debt Bulk Write-off";
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nPaymentVoidTrayID"] = DBNull.Value;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sPaymentVoidTrayDesc"] = "";
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.PatientPayment.GetHashCode();
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["Credits_EXTID"] = 0;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["ClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["PaymentVoidDateTime"] = DBNull.Value;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["CreatedDateTime"] = DateTime.Now;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["ModifiedDateTime"] = DateTime.Now;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sMachineName"] = Environment.MachineName;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["SiteID"] = "";
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["IsFinished"] = false;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["IsERAPost"] = false;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["BPRID"] = DBNull.Value;

                        this.dsBulkPatientPayment_TVP.Tables["Credits"].AcceptChanges();

                    }
                    else
                    { throw new Exception("Error at GenerateCreditEntry method, dsBulkPatientPayment_TVP is null"); }
                }
                else
                { throw new Exception("Error at GenerateCreditEntry method, either of CreditID/PayerID/AccountID/TrayId/CloseDate parameter is invalid"); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        private void GenerateCreditEntryforReserveDistribution(Int64 creditId, PaymentInfoParameter paymentInfoParameter, string sPaymentNote)
        {
            Int32 currentRowIndex = 0;
            string creditTableName = "Credits";
          
            try
            {
                if (creditId > 0 && paymentInfoParameter.PatientID > 0 && paymentInfoParameter.AccountID > 0 && paymentInfoParameter.PaymentTrayID > 0
                    && paymentInfoParameter.CloseDate != DateTime.MinValue)
                {
                    if (this.dsBulkPatientPayment_TVP != null)
                    {
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows.Add();
                        currentRowIndex = this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows.Count - 1;

                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nCreditID"] = creditId;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sReceiptNo"] = "";
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["dReceiptAmount"] = Convert.ToDecimal(paymentInfoParameter.CheckAmountToDistribute);
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["dtReceiptDate"] = paymentInfoParameter.CloseDate;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nPayerType"] = gloAccountsV2.PayerTypeV2.Self.GetHashCode();
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nPayerID"] = paymentInfoParameter.PatientID;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sPayerName"] = "";
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sPaymentNo"] = Convert.ToString(creditId);
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nPaymentMode"] = gloAccountsV2.PaymentModeV2.Check;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["dtCloseDate"] = paymentInfoParameter.CloseDate;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nPaymentTrayID"] = paymentInfoParameter.PaymentTrayID;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sPaymentTrayDesc"] = paymentInfoParameter.PaymentTrayName;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sUserName"] = gloGlobal.gloPMGlobal.UserName;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["bIsPaymentVoid"] = 0;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nVoidType"] = 0;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["dtPaymentVoidCloseDate"] = DBNull.Value;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nPAccountID"] = paymentInfoParameter.AccountID;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nAccountPatientID"] = paymentInfoParameter.AccountPatientID;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nGuarantorID"] = paymentInfoParameter.GuarantorID;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sPaymentNote"] = sPaymentNote;//"Auto Reserve Distribution";
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nPaymentVoidTrayID"] = DBNull.Value;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sPaymentVoidTrayDesc"] = "";
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.UseReserve.GetHashCode();
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["Credits_EXTID"] = 0;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["ClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["PaymentVoidDateTime"] = DBNull.Value;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["CreatedDateTime"] = DateTime.Now;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["ModifiedDateTime"] = DateTime.Now;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["sMachineName"] = Environment.MachineName;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["SiteID"] = "";
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["IsFinished"] = false;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["IsERAPost"] = false;
                        this.dsBulkPatientPayment_TVP.Tables[creditTableName].Rows[currentRowIndex]["BPRID"] = DBNull.Value;

                        this.dsBulkPatientPayment_TVP.Tables["Credits"].AcceptChanges();

                    }
                    else
                    { throw new Exception("Error at GenerateCreditEntry method, dsBulkPatientPayment_TVP is null"); }
                }
                else
                { throw new Exception("Error at GenerateCreditEntry method, either of CreditID/PayerID/AccountID/TrayId/CloseDate parameter is invalid"); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        private void GenerateCredit_DTLEntryforReserveDistribution(Int64 creditId, PaymentInfoParameter paymentInfoParameter, gloGeneralItem.gloItems ReserveDetails)
        {
            Int32 currentRowIndex = 0;
            string credit_DTLTableName = "CreditsDTL";

            try
            {
                if (creditId > 0 && paymentInfoParameter.PatientID > 0 && paymentInfoParameter.AccountID > 0 && paymentInfoParameter.PaymentTrayID > 0
                    && paymentInfoParameter.CloseDate != DateTime.MinValue && ReserveDetails != null)
                {
                    if (this.dsBulkPatientPayment_TVP != null)
                    {
                        
                        if (ReserveDetails != null)
                        {

                            if (ReserveDetails == null)
                            {
                                ReserveDetails = new gloGeneralItem.gloItems();
                            }
                            for (int crPay = 0; crPay <= ReserveDetails.Count - 1; crPay++)
                            {
                                this.dsBulkPatientPayment_TVP.Tables[credit_DTLTableName].Rows.Add();
                                currentRowIndex = this.dsBulkPatientPayment_TVP.Tables[credit_DTLTableName].Rows.Count - 1;
                                this.dsBulkPatientPayment_TVP.Tables[credit_DTLTableName].Rows[currentRowIndex]["nCreditsDTL_ID"] = 0;
                                this.dsBulkPatientPayment_TVP.Tables[credit_DTLTableName].Rows[currentRowIndex]["nCreditID"] = creditId;
                                this.dsBulkPatientPayment_TVP.Tables[credit_DTLTableName].Rows[currentRowIndex]["nCreditsRef_ID"] = Convert.ToInt64(ReserveDetails[crPay].ID);
                                for (int crSub = 0; crSub <= ReserveDetails[crPay].SubItems.Count - 1; crSub++)
                                {
                                    this.dsBulkPatientPayment_TVP.Tables[credit_DTLTableName].Rows[currentRowIndex]["nReserveRef_ID"] = Convert.ToInt64(ReserveDetails[crPay].SubItems[crSub].ID);
                                }
                                this.dsBulkPatientPayment_TVP.Tables[credit_DTLTableName].Rows[currentRowIndex]["dAmount"] = Convert.ToDecimal(ReserveDetails[crPay].Description);
                                this.dsBulkPatientPayment_TVP.Tables[credit_DTLTableName].Rows[currentRowIndex]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.UseReserve.GetHashCode();
                                this.dsBulkPatientPayment_TVP.Tables[credit_DTLTableName].Rows[currentRowIndex]["sEntryDesc"] = "UR";


                              //  for (int crSub = 0; crSub <= ReserveDetails[crPay].SubItems.Count - 1; crSub++)
                              //  {
                                   // if (Convert.ToDateTime(paymentInfoParameter.CloseDate) < Convert.ToDateTime(ReserveDetails[crPay].SubItems[crSub].CloseDate))
                                   // {
                                    //    MessageBox.Show("The used reserved amount close date is in future than the current payment close date. Please select a different payment close date.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    return 0;
                                  //  }

                                this.dsBulkPatientPayment_TVP.Tables[credit_DTLTableName].Rows[currentRowIndex]["dtCloseDate"] = paymentInfoParameter.CloseDate;
                               // }
                                this.dsBulkPatientPayment_TVP.Tables[credit_DTLTableName].Rows[currentRowIndex]["dtCreatedDateTime"] = DateTime.Now;
                                this.dsBulkPatientPayment_TVP.Tables[credit_DTLTableName].Rows[currentRowIndex]["dtModifiedDateTime"] = DateTime.Now;
                                this.dsBulkPatientPayment_TVP.Tables[credit_DTLTableName].Rows[currentRowIndex]["bIsPaymentVoid"] = false;

                                this.dsBulkPatientPayment_TVP.Tables[credit_DTLTableName].AcceptChanges();
                            }
                        }
                        

                    }
                    else
                    { throw new Exception("Error at GenerateCredit_DTLEntry method, dsBulkPatientPayment_TVP is null"); }
                }
                else
                { throw new Exception("Error at GenerateCredit_DTLEntry method, either of CreditID/PayerID/AccountID/TrayId/CloseDate/ReserveDetails parameter is invalid"); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        private void GenerateEob_DebitsEntryforReserveDistribution(Int64 creditId, Int64 EodId, PaymentInfoParameter paymentInfoParameter, gloGeneralItem.gloItems ReserveDetails, DataTable dtPatientAndBadDebtDueCharges)
        {
            Int32 currentRowIndex = 0;
            string EOBTableName = "EOB";
            string DebitsableName = "Debits";
            Int64 _nTransactionPatientID = 0;
            DataTable _dtUniqueIDs = null;
            int row_num = 0;
            int row_index = 0;
            try
            {
                if (creditId > 0 && paymentInfoParameter.PatientID > 0 && paymentInfoParameter.AccountID > 0 && paymentInfoParameter.PaymentTrayID > 0
                    && paymentInfoParameter.CloseDate != DateTime.MinValue && ReserveDetails != null && dtPatientAndBadDebtDueCharges !=null)
                {
                    if (this.dsBulkPatientPayment_TVP != null)
                    {
                        
                        #region "EOB and Debit Entry"
                        if (dtPatientAndBadDebtDueCharges != null && dtPatientAndBadDebtDueCharges.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtPatientAndBadDebtDueCharges.Rows.Count; i++)
                            {
                                #region "Claim wise EOB and Finance Line"
                                _nTransactionPatientID = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["nPatientID"]);                              
                                    if (dtPatientAndBadDebtDueCharges.Rows[i]["dPayment"] != null && Convert.ToString(dtPatientAndBadDebtDueCharges.Rows[i]["dPayment"]).Trim() != "" && Convert.ToDecimal(dtPatientAndBadDebtDueCharges.Rows[i]["dPayment"]) > 0)
                                    {
                                        #region "EOB Service Lines"
                                        decimal _fillPayAmt = 0; 
                                        //decimal _fillAdjAmt = 0;
                                        this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows.Add();
                                        currentRowIndex = this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows.Count - 1;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[currentRowIndex]["nCreditID"] = creditId;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[currentRowIndex]["nEOBID"] = EodId;
                                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                        {
                                             this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nEOBDetailID"] = 0;
                                        }
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nPatientID"] = _nTransactionPatientID;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nPatientInsuranceID"] = 0;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nContactID"] = 0;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nInsCompanyID"] = 0;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nBillingTransactionID"] = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["nTransactionID"]);
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nBillingTransactionDetailID"] = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["nTransactionDetailID"]);
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[currentRowIndex]["nTrackTransactionDetailID"] = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["TrackTransactionDetailID"]);
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[currentRowIndex]["nTrackTransactionID"] = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["TrackTransactionID"]); 
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["sEntryDesc"] = "PatientPayment";
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["sCPTCode"] = Convert.ToString(dtPatientAndBadDebtDueCharges.Rows[i]["sCPTCode"]);
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["dtCloseDate"] = paymentInfoParameter.CloseDate;

                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[currentRowIndex]["nPaymentTrayID"] = paymentInfoParameter.PaymentTrayID;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[currentRowIndex]["sPaymentTrayDesc"] = paymentInfoParameter.PaymentTrayName;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nPaymentVoidTrayID"] = DBNull.Value;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["bIsPaymentVoid"] = false;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nVoidType"] = 0;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[currentRowIndex]["nPAccountID"] = paymentInfoParameter.AccountID;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[currentRowIndex]["nGuarantorID"] = paymentInfoParameter.GuarantorID;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[currentRowIndex]["nAccountPatientID"] = paymentInfoParameter.AccountPatientID;

                                        if (dtPatientAndBadDebtDueCharges.Rows[i]["dTotal"] != null && Convert.ToString(dtPatientAndBadDebtDueCharges.Rows[i]["dTotal"]).Trim() != "")
                                        {
                                             this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["dTotalChargeAmount"] = Convert.ToDecimal(dtPatientAndBadDebtDueCharges.Rows[i]["dTotal"]);
                                        }

                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nEXTID"] = 0;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[currentRowIndex]["nCreditIDEXT"] = creditId;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[currentRowIndex]["nEOBIDEXT"] = EodId;
                                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                        {
                                             this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nEOBDetailIDEXT"] = 0;
                                        }

                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["sNextActionEXT"] = "";
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["sNextPartyEXT"] = "";
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nNextPartyIDEXT"] = 0;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["dtCreatedDateTimeEXT"] = DateTime.Now;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["dtModifiedDateTimeEXT"] = DateTime.Now;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["dtPaymentVoidDateTimeEXT"] = DBNull.Value;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nUserIDEXT"] = gloGlobal.gloPMGlobal.UserID;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["sUserNameEXT"] = gloGlobal.gloPMGlobal.UserName;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["bIsERAPostEXT"] = false;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nSVCIDEXT"] = DBNull.Value;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["sMachineNameEXT"] = Environment.MachineName;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["sSiteIDEXT"] = DBNull.Value;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["sVersionEXT"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nClinicIDEXT"] = gloGlobal.gloPMGlobal.ClinicID;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nCLPIdEXT"] = 0;
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nBillingTransactionEXT"] = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["nTransactionID"]);
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[ currentRowIndex]["nBillingTransactionDetailEXT"] = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["nTransactionDetailID"]);
                                        if (dtPatientAndBadDebtDueCharges.Rows[i]["dPayment"] != null && Convert.ToString(dtPatientAndBadDebtDueCharges.Rows[i]["dPayment"]).Trim() != "")
                                        {
                                            _fillPayAmt = Convert.ToDecimal(Convert.ToString(dtPatientAndBadDebtDueCharges.Rows[i]["dPayment"]));
                                        }
                                         this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[currentRowIndex]["dPaymentAmount"] = _fillPayAmt;

                                        #endregion "EOB Service Lines"

                                        #region "Debit Service Line - patient "

                                        gloGeneralItem.gloItems oItems = null;
                                        if (ReserveDetails != null)
                                        {
                                            oItems = (gloGeneralItem.gloItems)ReserveDetails;
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

                                        if (dtPatientAndBadDebtDueCharges.Rows[i]["dPayment"] != null && Convert.ToString(dtPatientAndBadDebtDueCharges.Rows[i]["dPayment"]).Trim() != "")
                                        {
                                            _fillPayAmt = Convert.ToDecimal(Convert.ToString(dtPatientAndBadDebtDueCharges.Rows[i]["dPayment"]));                                           
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
                                            this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows.Add();
                                            currentRowIndex = this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows.Count - 1;
                                            if (_fillResPayID == 0)
                                            {
                                                _fillResPayID = creditId;
                                            }
                                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                            {
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nEOBDetailID"] = 0;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nDebitID"] = 0;
                                            }
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nCreditID"] = creditId;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nCredit_RefID"] = _fillResPayID;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nEOBID"] = EodId;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nPatientInsuranceID"] = 0;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nContactID"] = 0;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nInsCompanyID"] = 0;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nPatientID"] = _nTransactionPatientID;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nGuarantorID"] = paymentInfoParameter.GuarantorID;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[currentRowIndex]["nPAccountID"] = paymentInfoParameter.AccountID;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[currentRowIndex]["nAccountPatientID"] = paymentInfoParameter.AccountPatientID;                                         
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nBillingTransactionID"] = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["nTransactionID"]);
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nBillingTransactionDetailID"] = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["nTransactionDetailID"]);
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nTrackTransactionID"] = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["TrackTransactionID"]);
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nTrackTransactionDetailID"] = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["TrackTransactionDetailID"]);
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sEntryDesc"] = "PatientPayment";
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sCPTCode"] = Convert.ToString(dtPatientAndBadDebtDueCharges.Rows[i]["sCPTCode"]);
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sCPTDesc"] = Convert.ToString(dtPatientAndBadDebtDueCharges.Rows[i]["sCPTDescription"]); ;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dPaymentAmount"] = _fillPayAmt;                                            

                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dWriteoffAmount"] = DBNull.Value;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dWithholdAmount"] = DBNull.Value;

                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[currentRowIndex]["nPaymentTrayID"] = paymentInfoParameter.PaymentTrayID;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[currentRowIndex]["sPaymentTrayDesc"] = paymentInfoParameter.PaymentTrayName;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nPaymentVoidTrayID"] = DBNull.Value;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sUserName"] = gloGlobal.gloPMGlobal.UserName;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[currentRowIndex]["dtCloseDate"] = paymentInfoParameter.CloseDate;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["bIsPaymentVoid"] = false;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nVoidType"] = 0;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dtPaymentVoidDateTime"] = DBNull.Value;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dtClaimVoidCloseDate"] = DBNull.Value;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dtClaimVoidDateTime"] = DBNull.Value;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["bIsERAPost"] = false;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dtCreatedDateTime"] = DateTime.Now;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dtModifiedDateTime"] = DateTime.Now;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sMachineName"] = Environment.MachineName;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                             this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sSiteID"] = DBNull.Value;
                                            if (_fillrPayIndx != -1)
                                            {
                                                oItems[_fillrPayIndx].Description = Convert.ToString(_fillResAmt - _fillPayAmt);
                                                ReserveDetails = oItems;
                                            }
                                            #endregion
                                            row_index = row_index + 1;
                                        }
                                        else
                                        {
                                            #region "Set More Amount Multiple object"

                                            decimal _fillPayMulAmt = _fillPayAmt;
                                            int loopIndex = 0; 
                                            do
                                            {
                                                
                                                loopIndex++;
                                                if (Convert.ToDecimal(oItems[_fillrPayIndx].Description) > 0)
                                                {
                                                    _fillResAmt = Convert.ToDecimal(oItems[_fillrPayIndx].Description);
                                                    _fillResPayID = Convert.ToInt64(oItems[_fillrPayIndx].ID);
                                                    _fillResPayDtlID = Convert.ToInt64(oItems[_fillrPayIndx].Code);
                                                    _fillRefFinanceLieNo = 0;
                                                 

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
                                                        
                                                        _fillRefFinanceLieNo = _fillrPayIndx + 2;
                                                    }
                                                }
                                              
                                                if (_fillResPayID == 0)
                                                {
                                                    _fillResPayID = creditId;
                                                }
                                                #region "Set object"
                                                this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows.Add();
                                                currentRowIndex = this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows.Count - 1;
                                                if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                {
                                                     this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nEOBDetailID"] = 0;
                                                     this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nDebitID"] = 0;
                                                }
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nCreditID"] = creditId;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nCredit_RefID"] = _fillResPayID;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nEOBID"] = EodId;

                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nPatientInsuranceID"] = 0;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nContactID"] = 0;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nInsCompanyID"] = 0;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nPatientID"] = _nTransactionPatientID;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[currentRowIndex]["nGuarantorID"] = paymentInfoParameter.GuarantorID;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[currentRowIndex]["nPAccountID"] = paymentInfoParameter.AccountID;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[currentRowIndex]["nAccountPatientID"] = paymentInfoParameter.AccountPatientID;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nBillingTransactionID"] = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["nTransactionID"]);
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nBillingTransactionDetailID"] = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["nTransactionDetailID"]);
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nTrackTransactionID"] = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["TrackTransactionID"]);
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nTrackTransactionDetailID"] = Convert.ToInt64(dtPatientAndBadDebtDueCharges.Rows[i]["TrackTransactionDetailID"]);
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sEntryDesc"] = "PatientPayment";
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sCPTCode"] = Convert.ToString(dtPatientAndBadDebtDueCharges.Rows[i]["sCPTCode"]);
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sCPTDesc"] = Convert.ToString(dtPatientAndBadDebtDueCharges.Rows[i]["sCPTDescription"]);
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dPaymentAmount"] = _fillPayAmt;                           


                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dWriteoffAmount"] = DBNull.Value;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dWithholdAmount"] = DBNull.Value;

                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[currentRowIndex]["nPaymentTrayID"] = paymentInfoParameter.PaymentTrayID;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[currentRowIndex]["sPaymentTrayDesc"] = paymentInfoParameter.PaymentTrayName;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nPaymentVoidTrayID"] = DBNull.Value;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sUserName"] = gloGlobal.gloPMGlobal.UserName;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[currentRowIndex]["dtCloseDate"] = paymentInfoParameter.CloseDate;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["bIsPaymentVoid"] = false;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nVoidType"] = 0;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dtPaymentVoidDateTime"] = DBNull.Value;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dtClaimVoidCloseDate"] = DBNull.Value;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dtClaimVoidDateTime"] = DBNull.Value;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["bIsERAPost"] = false;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dtCreatedDateTime"] = DateTime.Now;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["dtModifiedDateTime"] = DateTime.Now;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sMachineName"] = Environment.MachineName;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[ currentRowIndex]["sSiteID"] = DBNull.Value;
                                                #endregion

                                                if (_fillrPayIndx != -1)
                                                {
                                                    oItems[_fillrPayIndx].Description = Convert.ToString(_fillResAmt - _fillPayAmt);
                                                    ReserveDetails = oItems;
                                                    _fillrPayIndx = _fillrPayIndx + 1;
                                                    if (_fillrPayIndx >= oItems.Count) { break; }
                                                }

                                            }
                                            while (_fillPayMulAmt > 0);

                                            #endregion
                                        }

                                        #endregion
                                    }
                                     this.dsBulkPatientPayment_TVP.Tables[EOBTableName].AcceptChanges();
                                     this.dsBulkPatientPayment_TVP.Tables[DebitsableName].AcceptChanges();

                                    row_num = row_num + 1;
                               
                                #endregion "Claim wise EOB and Finance Line"
                            }
                        }


                        #endregion "EOB and Debit Entry"

                        if ( this.dsBulkPatientPayment_TVP.Tables[DebitsableName] != null && Convert.ToInt64( this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows.Count) > 0)
                        {
                            _dtUniqueIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs( this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows.Count);

                            for (int i = 0; i <= dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows.Count - 1; i++)
                            {
                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                                 this.dsBulkPatientPayment_TVP.Tables[DebitsableName].Rows[i]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID2"].ToString());
                            }

                            for (int i = 0; i <= dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows.Count - 1; i++)
                            {
                                 this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                                 this.dsBulkPatientPayment_TVP.Tables[EOBTableName].Rows[i]["nEOBDetailIDEXT"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                            }
                        }


                    }
                    else
                    { throw new Exception("Error at GenerateCredit_DTLEntry method, dsBulkPatientPayment_TVP is null"); }
                }
                else
                { throw new Exception("Error at GenerateCredit_DTLEntry method, either of CreditID/PayerID/AccountID/TrayId/CloseDate/ReserveDetails parameter is invalid"); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        
        private void GenerateMasterNoteEntry(Int64 creditId, PaymentInfoParameter paymentInfoParameter)
        {
            Int32 currentRowIndex = 0;
            string eobNotesTableName = "EOBNotes";

            try
            {
                if (creditId > 0)
                {
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows.Add();
                    currentRowIndex = this.dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows.Count - 1;

                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nID"] = 0;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nClaimNo"] = 0;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nEOBPaymentID"] = creditId;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nEOBID"] = 0;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nEOBPaymentDetailID"] = 0;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nBillingTransactionID"] = 0;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nBillingTransactionDetailID"] = 0;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["sNoteCode"] = "";
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["sNoteDescription"] = "Bad debt due write-off";
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nIncludeNoteOnPrint"] = false;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["dNoteAmount"] = Convert.ToDecimal("0.00");
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nCloseDate"] = paymentInfoParameter.CloseDateAsNumber;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nPaymentNoteType"] = gloAccountsV2.NoteTypeV2.Payment_Patient.GetHashCode();
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nPaymentNoteSubType"] = gloAccountsV2.NoteSubTypeV2.MasterPayment.GetHashCode();
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["bIsVoid"] = 0;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nBillingNoteType"] = gloAccountsV2.BillingNoteTypeV2.None.GetHashCode();
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nDateTime"] = DBNull.Value;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nTrackTrnID"] = 0;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nTrackTrnDtlID"] = 0;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["nTrackTrnLineNo"] = 0;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["sSubClaimNo"] = "";
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].Rows[currentRowIndex]["dtModifiedDateTime"] = DBNull.Value;
                    dsBulkPatientPayment_TVP.Tables[eobNotesTableName].AcceptChanges();
                }
                else
                { throw new Exception("Error at GenerateMasterNoteEntry method, creditId is null or invalid"); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GeneratePaymentDetailsEntry(DataTable dtCharges, Int64 creditId, Int64 eobId, PaymentInfoParameter paymentInfoParameter)
        {
            string filterExpression = string.Empty;
            Int64 currentMasterTransactionId = 0;
            Int64 currentTransactionId = 0;
            DataTable dtUniqueIDs = null;

            Int64 currentEobDetailId = 0;
            Int64 currentDebitId = 0;
            decimal currentAdjustmentAmount = 0;

            Int32 currentRowIndexEOB = 0;
            Int32 currentRowIndexReasonCode = 0;
            Int32 currentRowIndexDebits = 0;

            //int row_num = 0;
            //int row_index = 0;

            string eobTableName = "EOB";
            string reasonCodeTableName = "ReasonCode";
            string debitsTableName = "Debits";

            try
            {
                if (this.dsBulkPatientPayment_TVP != null)
                {
                    if (dtCharges != null && dtCharges.Rows.Count > 0)
                    {
                        if (creditId > 0 && eobId > 0)
                        {
                            if (paymentInfoParameter != null)
                            {
                                dtUniqueIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(dtCharges.Rows.Count);

                                for (int rowIndex = 0; rowIndex < dtCharges.Rows.Count; rowIndex++)
                                {
                                    currentMasterTransactionId = Convert.ToInt64(dtCharges.Rows[rowIndex]["nTransactionID"]);
                                    currentTransactionId = Convert.ToInt64(dtCharges.Rows[rowIndex]["TrackTransactionID"]);

                                    #region "Write EOB Table"

                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows.Add();
                                    currentRowIndexEOB = this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows.Count - 1;

                                    currentAdjustmentAmount = Convert.ToDecimal(dtCharges.Rows[rowIndex]["BadDebtDue"]) + Convert.ToDecimal(dtCharges.Rows[rowIndex]["PatientDue"]);
                                    currentEobDetailId = Convert.ToInt64(Convert.ToString(dtUniqueIDs.Rows[currentRowIndexEOB]["ID"]));

                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nCreditID"] = creditId;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nEOBID"] = eobId;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nEOBDetailID"] = currentEobDetailId;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nPatientID"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["nPatientID"]);
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nPatientInsuranceID"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nContactID"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nInsCompanyID"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nBillingTransactionID"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["nTransactionID"]);
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nBillingTransactionDetailID"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["nTransactionDetailID"]);
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nTrackTransactionID"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["TrackTransactionID"]);
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nTrackTransactionDetailID"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["TrackTransactionDetailID"]); ;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["sEntryDesc"] = "PatientPayment";
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["sCPTCode"] = Convert.ToString(dtCharges.Rows[rowIndex]["sCPTCode"]);
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["dtCloseDate"] = paymentInfoParameter.CloseDate;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["dWriteoffAmount"] = currentAdjustmentAmount;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["dPaymentAmount"] = Convert.ToDecimal("0.00");
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nPaymentTrayID"] = paymentInfoParameter.PaymentTrayID;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["sPaymentTrayDesc"] = paymentInfoParameter.PaymentTrayName;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nPaymentVoidTrayID"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["bIsPaymentVoid"] = false;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nVoidType"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nPAccountID"] = paymentInfoParameter.AccountID;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nGuarantorID"] = paymentInfoParameter.GuarantorID;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nAccountPatientID"] = paymentInfoParameter.AccountPatientID;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["dTotalChargeAmount"] = Convert.ToDecimal(dtCharges.Rows[rowIndex]["dTotal"]);
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nEXTID"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nCreditIDEXT"] = creditId;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nEOBIDEXT"] = eobId;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nEOBDetailIDEXT"] = currentEobDetailId;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["sNextActionEXT"] = "";
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["sNextPartyEXT"] = "";
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nNextPartyIDEXT"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["dtCreatedDateTimeEXT"] = DateTime.Now;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["dtModifiedDateTimeEXT"] = DateTime.Now;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["dtPaymentVoidDateTimeEXT"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nUserIDEXT"] = gloGlobal.gloPMGlobal.UserID;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["sUserNameEXT"] = gloGlobal.gloPMGlobal.UserName;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["bIsERAPostEXT"] = false;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nSVCIDEXT"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["sMachineNameEXT"] = Environment.MachineName;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["sSiteIDEXT"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["sVersionEXT"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nClinicIDEXT"] = gloGlobal.gloPMGlobal.ClinicID;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nCLPIdEXT"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nBillingTransactionEXT"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["nTransactionID"]);
                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].Rows[currentRowIndexEOB]["nBillingTransactionDetailEXT"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["nTransactionDetailID"]);

                                    this.dsBulkPatientPayment_TVP.Tables[eobTableName].AcceptChanges();

                                    #endregion "Write EOB Table"

                                    #region "Write Reason Code Table"

                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows.Add();
                                    currentRowIndexReasonCode = this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows.Count - 1;
                                    

                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nID"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nClaimNo"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["nClaimNo"]);
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nEOBPaymentDetailID"] = currentEobDetailId;
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nEOBPaymentID"] = creditId;
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nEOBID"] = eobId;
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nBillingTransactionID"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["nTransactionID"]);
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nBillingTransactionDetailID"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["nTransactionDetailID"]);
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["sReasonCode"] = paymentInfoParameter.AdjustmentReasonCode;
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["sReasonDescription"] = paymentInfoParameter.AdjustmentReasonCodeDescription;
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["dReasonAmount"] = currentAdjustmentAmount; 
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nType"] = gloAccountsV2.EOBCommentTypeV2.SystemReasonCode.GetHashCode();
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["bIsVoid"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nTrackTrnID"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["TrackTransactionID"]);
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nTrackTrnDtlID"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["TrackTransactionDetailID"]);
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nTrackTrnLineNo"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["sSubClaimNo"] = Convert.ToString(dtCharges.Rows[rowIndex]["TrackSubClaimNo"]);
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nCloseDate"] = paymentInfoParameter.CloseDateAsNumber;
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nVoidType"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["bIsPaymentVoid"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nPaymentVoidCloseDate"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nPaymentVoidTrayID"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].Rows[currentRowIndexReasonCode]["nSubType"] = DBNull.Value;
                                    
                                    this.dsBulkPatientPayment_TVP.Tables[reasonCodeTableName].AcceptChanges();

                                    #endregion "Write Reason Code Table"

                                    #region "Write Debits Table"

                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows.Add();
                                    currentRowIndexDebits = this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows.Count - 1;
                                    currentDebitId = Convert.ToInt64(Convert.ToString(dtUniqueIDs.Rows[currentRowIndexDebits]["ID2"]));

                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nCreditID"] = creditId;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nCredit_RefID"] = creditId;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nEOBID"] = eobId;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nEOBDetailID"] = currentEobDetailId;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nDebitID"] = currentDebitId;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nPatientInsuranceID"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nContactID"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nInsCompanyID"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nPatientID"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["nPatientID"]);
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nGuarantorID"] = paymentInfoParameter.GuarantorID;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nPAccountID"] = paymentInfoParameter.AccountID;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nAccountPatientID"] = paymentInfoParameter.AccountPatientID;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nBillingTransactionID"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["nTransactionID"]);
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nBillingTransactionDetailID"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["nTransactionDetailID"]);
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nTrackTransactionID"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["TrackTransactionID"]);
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nTrackTransactionDetailID"] = Convert.ToInt64(dtCharges.Rows[rowIndex]["TrackTransactionDetailID"]);
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["sEntryDesc"] = "PatientPayment";
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["sCPTCode"] = Convert.ToString(dtCharges.Rows[rowIndex]["sCPTCode"]);
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["sCPTDesc"] = Convert.ToString(dtCharges.Rows[rowIndex]["sCPTDescription"]);
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["dPaymentAmount"] = Convert.ToDecimal("0.00");
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["dOtherAdjustmentAmount"] = currentAdjustmentAmount;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["dWriteoffAmount"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["dWithholdAmount"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nPaymentTrayID"] = paymentInfoParameter.PaymentTrayID;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["sPaymentTrayDesc"] = paymentInfoParameter.PaymentTrayName;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nPaymentVoidTrayID"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["sUserName"] = gloGlobal.gloPMGlobal.UserName;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["dtCloseDate"] = paymentInfoParameter.CloseDate;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["bIsPaymentVoid"] = false;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nVoidType"] = 0;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["dtPaymentVoidDateTime"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["dtClaimVoidCloseDate"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["dtClaimVoidDateTime"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["bIsERAPost"] = false;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["dtCreatedDateTime"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["dtModifiedDateTime"] = DBNull.Value;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["sMachineName"] = Environment.MachineName;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].Rows[currentRowIndexDebits]["sSiteID"] = DBNull.Value;

                                    this.dsBulkPatientPayment_TVP.Tables[debitsTableName].AcceptChanges();

                                    #endregion "Write Debits Table"
                                }
                            }
                            else
                            { throw new Exception("Error at GeneratePaymentDetailsEntry method, paymentInfoParameter is null or invalid"); }
                        }
                        else
                        { throw new Exception("Error at GeneratePaymentDetailsEntry method, either credit/eobid is null or invalid"); }
                    }
                    else
                    { throw new Exception("Error at GeneratePaymentDetailsEntry method, dtCharges is null or empty"); }
                }
                else
                { throw new Exception("Error at GeneratePaymentDetailsEntry method, dsBulkPatientPayment_TVP is null"); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutoDistributeReserves(PaymentInfoParameter paymentInfoParameter, gloGeneralItem.gloItems ReserveDetails)
        {
            DataTable dtPatientAndBadDebtDueCharges = null;
            DataTable dtUniqueID = null;
            Int64 paymentCreditId = 0;
            Int64 paymentEOBId = 0;
            Int64 AccountOwnerPatientId = 0;
            gloAccountsV2.gloPatientPaymentV2 objPatientPayment = null;
            ClsReserveDistributionList oClsReserveDistributionList = new ClsReserveDistributionList();
            string sPaymentNote = "Auto Reserve Distribution";

            try
            {
                if (paymentInfoParameter != null)
                {
                    if (this.dsBulkPatientPayment_TVP != null)
                    {
                        this.dsBulkPatientPayment_TVP = null;
                        this.dsBulkPatientPayment_TVP = new dsPaymentTVP_V2();
                    }

                    AccountOwnerPatientId = GetAccountOwnerPatientId(paymentInfoParameter.AccountID);

                    //TODO: Verify close date 

                    if (AccountOwnerPatientId > 0)
                    {
                       // dtPatientAndBadDebtDueCharges = GetAccountTransaction(paymentInfoParameter.AccountID);
                        dtPatientAndBadDebtDueCharges = oClsReserveDistributionList.getPatientAndBadDebtDueCharges(paymentInfoParameter.AccountID, false, true);
                        dtPatientAndBadDebtDueCharges = oClsReserveDistributionList.DistubuteAmount(paymentInfoParameter.CheckAmountToDistribute, dtPatientAndBadDebtDueCharges);

                        if (dtPatientAndBadDebtDueCharges != null && dtPatientAndBadDebtDueCharges.Rows.Count > 0 && paymentInfoParameter.CheckAmountToDistribute != string.Empty)
                        {
                            dtUniqueID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                            if (dtUniqueID != null && dtUniqueID.Rows.Count > 0)
                            {
                                paymentCreditId = Convert.ToInt64(dtUniqueID.Rows[0]["ID"].ToString());
                                paymentEOBId = Convert.ToInt64(dtUniqueID.Rows[0]["ID2"].ToString());
                            }

                            //Write Credit and Credit_EXT info to TVP
                            GenerateCreditEntryforReserveDistribution(paymentCreditId, paymentInfoParameter, sPaymentNote);
                            //Write Credit_DTl Used Reserve ingo to TVP
                            GenerateCredit_DTLEntryforReserveDistribution(paymentCreditId, paymentInfoParameter, ReserveDetails);
                            //Write EOB and Debits Entry ingo to TVP
                            GenerateEob_DebitsEntryforReserveDistribution(paymentCreditId,paymentEOBId, paymentInfoParameter, ReserveDetails,dtPatientAndBadDebtDueCharges);

                            string _auditNoChargeMessage = "Reserves Distribution: Successful for account #: " + paymentInfoParameter.AccountID + " ";
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Reserves, ActivityType.ReservesDistribution, _auditNoChargeMessage, 0, 0, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                      
                            if (this.dsBulkPatientPayment_TVP != null)
                            {
                                Int64 returnPaymentId = 0;
                                objPatientPayment = new gloAccountsV2.gloPatientPaymentV2();
                                returnPaymentId = objPatientPayment.SavePatientPayment(this.dsBulkPatientPayment_TVP);
                            }

                        }
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Reserves, ActivityType.ReservesDistribution, "Reserves Distribution: Failed for account #: " + paymentInfoParameter.AccountID + "", 0, 0, 0, ActivityOutCome.Failure, SoftwareComponent.gloPM, true); 
                        throw new Exception("Error at WriteOffPatientDue method, AccountOwnerPatientId is zero or null"); }
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Reserves, ActivityType.ReservesDistribution, "Reserves Distribution: Failed for account #: " + paymentInfoParameter.AccountID + "", 0, 0, 0, ActivityOutCome.Failure, SoftwareComponent.gloPM, true); 
                    throw new Exception("Error at WriteOffPatientDue method, paymentInfoParameter is null"); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (objPatientPayment != null) { objPatientPayment.Dispose(); objPatientPayment = null; }
                if (dtPatientAndBadDebtDueCharges != null) { dtPatientAndBadDebtDueCharges.Dispose(); dtPatientAndBadDebtDueCharges = null; }
                if (oClsReserveDistributionList != null) {  oClsReserveDistributionList = null; }
                
            }
        }

        public void AutoDistributeCopayReserves(PaymentInfoParameter paymentInfoParameter, gloGeneralItem.gloItems ReserveDetails,DataTable dtChargeLineDetail,Int64 nPatientID)
        {            
            DataTable dtUniqueID = null;
            Int64 paymentCreditId = 0;
            Int64 paymentEOBId = 0;
            Int64 AccountOwnerPatientId = 0;
            gloAccountsV2.gloPatientPaymentV2 objPatientPayment = null;
            ClsReserveDistributionList oClsReserveDistributionList = new ClsReserveDistributionList();
            string sPaymentNote = "Auto Copay Reserve Distribution";

            try
            {
                if (paymentInfoParameter != null)
                {
                    if (this.dsBulkPatientPayment_TVP != null)
                    {
                        this.dsBulkPatientPayment_TVP = null;
                        this.dsBulkPatientPayment_TVP = new dsPaymentTVP_V2();
                    }

                    AccountOwnerPatientId = nPatientID;

                    //TODO: Verify close date 

                    if (AccountOwnerPatientId > 0)
                    {
                        if (dtChargeLineDetail != null && dtChargeLineDetail.Rows.Count > 0 && paymentInfoParameter.CheckAmountToDistribute != string.Empty)
                        {
                            dtUniqueID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                            if (dtUniqueID != null && dtUniqueID.Rows.Count > 0)
                            {
                                paymentCreditId = Convert.ToInt64(dtUniqueID.Rows[0]["ID"].ToString());
                                paymentEOBId = Convert.ToInt64(dtUniqueID.Rows[0]["ID2"].ToString());
                            }

                            //Write Credit and Credit_EXT info to TVP
                            GenerateCreditEntryforReserveDistribution(paymentCreditId, paymentInfoParameter, sPaymentNote);
                            //Write Credit_DTl Used Reserve ingo to TVP
                            GenerateCredit_DTLEntryforReserveDistribution(paymentCreditId, paymentInfoParameter, ReserveDetails);
                            //Write EOB and Debits Entry ingo to TVP
                            GenerateEob_DebitsEntryforReserveDistribution(paymentCreditId, paymentEOBId, paymentInfoParameter, ReserveDetails, dtChargeLineDetail);

                            string _auditNoChargeMessage = "Copay Reserves Distribution: Successful for account #: " + paymentInfoParameter.AccountID + " ";
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Reserves, ActivityType.ReservesDistribution, _auditNoChargeMessage, 0, 0, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);

                            if (this.dsBulkPatientPayment_TVP != null)
                            {
                                Int64 returnPaymentId = 0;
                                objPatientPayment = new gloAccountsV2.gloPatientPaymentV2();
                                returnPaymentId = objPatientPayment.SavePatientPayment(this.dsBulkPatientPayment_TVP);
                            }
                        }
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Reserves, ActivityType.ReservesDistribution, "Reserves Distribution: Failed for account #: " + paymentInfoParameter.AccountID + "", 0, 0, 0, ActivityOutCome.Failure, SoftwareComponent.gloPM, true);
                        throw new Exception("Error at WriteOffPatientDue method, AccountOwnerPatientId is zero or null");
                    }
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Reserves, ActivityType.ReservesDistribution, "Reserves Distribution: Failed for account #: " + paymentInfoParameter.AccountID + "", 0, 0, 0, ActivityOutCome.Failure, SoftwareComponent.gloPM, true);
                    throw new Exception("Error at WriteOffPatientDue method, paymentInfoParameter is null");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (objPatientPayment != null) { objPatientPayment.Dispose(); objPatientPayment = null; }
                if (dtChargeLineDetail != null) { dtChargeLineDetail.Dispose(); dtChargeLineDetail = null; }
                if (oClsReserveDistributionList != null) { oClsReserveDistributionList = null; }
            }
        }
        
        public Int64 AutoCleargagePaymentDistribution(PaymentInfoParameter paymentInfoParameter, DataTable dtChargeLineDetail, Int64 nPatientID,string ReserveDetails)
        {
            DataTable _dtUniqueCreditID = null;//SLR: New is not needed
            Int64 _nEOBID = 0;
            Int64 _nCreditID = 0;
            string sPaymentNote = "Cleargage Payment Note";
            gloAccountsV2.PaymentModeV2 _EOBPaymentMode = gloAccountsV2.PaymentModeV2.None;
            gloAccountsV2.gloPatientPaymentV2 objPatientPayment = null;
            DataTable _dtUniqueIDs = null;
            Int64 returnPaymentId = 0;
            DataTable _dtUniqueReserveIDs = null;
            try
            {
                //if (Convert.ToString(dtChargeLineDetail.Rows[0]["Action"]) == Convert.ToString(Actions.PAYMENT) || Convert.ToString(dtChargeLineDetail.Rows[0]["Action"]) == Convert.ToString(Actions.ADJUSTMENT))
                if (Convert.ToString(dtChargeLineDetail.Rows[0]["Action"]).ToUpper() == Convert.ToString(Actions.PAYMENT) || Convert.ToString(dtChargeLineDetail.Rows[0]["Action"]).ToUpper() == Convert.ToString(Actions.DISCOUNT) || Convert.ToString(dtChargeLineDetail.Rows[0]["Action"]).ToUpper()==Convert.ToString(Actions.FEE))
                {
                    if (Convert.ToString(dtChargeLineDetail.Rows[0]["Action"]).ToUpper() == Convert.ToString(Actions.PAYMENT) || Convert.ToString(dtChargeLineDetail.Rows[0]["Action"]).ToUpper() == Convert.ToString(Actions.FEE))
                    {
                        sPaymentNote = "Auto Cleargage Payment/Fee Distribution";
                    }
                    else
                    {
                        sPaymentNote = "Auto Cleargage Discount/Adjustment Distribution";
                    }
                    if (paymentInfoParameter != null)
                    {
                        if (this.dsBulkPatientPayment_TVP != null)
                        {
                            this.dsBulkPatientPayment_TVP = null;
                            this.dsBulkPatientPayment_TVP = new dsPaymentTVP_V2();
                        }
                    }

                    _dtUniqueCreditID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                    {
                        _nCreditID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID"].ToString());
                        _nEOBID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                    }
                    if (_nCreditID > 0 && paymentInfoParameter.PatientID > 0 && paymentInfoParameter.AccountID > 0 && paymentInfoParameter.PaymentTrayID > 0
                       && paymentInfoParameter.CloseDate != DateTime.MinValue && dtChargeLineDetail != null)
                    {

                        #region Credit Table Entry
                        #region "Payment Mode"
                        //if (cmbPayMode.Text != "")
                        //{
                        //    if (cmbPayMode.Text.Trim() == gloAccountsV2.PaymentModeV2.None.ToString())
                        //    { _EOBPaymentMode = gloAccountsV2.PaymentModeV2.None; }
                        //    else if (cmbPayMode.Text.Trim() == gloAccountsV2.PaymentModeV2.Cash.ToString())
                        //    { _EOBPaymentMode = gloAccountsV2.PaymentModeV2.Cash; }
                        //    else if (cmbPayMode.Text.Trim() == PaymentModeV2.Check.ToString())
                        //    { _EOBPaymentMode = PaymentModeV2.Check; }
                        //    else if (cmbPayMode.Text.Trim() == PaymentModeV2.MoneyOrder.ToString())
                        //    { _EOBPaymentMode = PaymentModeV2.MoneyOrder; }
                        //    else if (cmbPayMode.Text.Trim() == PaymentModeV2.CreditCard.ToString())
                        //    { _EOBPaymentMode = PaymentModeV2.CreditCard; }
                        //    else if (cmbPayMode.Text.Trim() == PaymentModeV2.EFT.ToString())
                        //    { _EOBPaymentMode = PaymentModeV2.EFT; }
                        //    else if (cmbPayMode.Text.Trim() == PaymentModeV2.Voucher.ToString())
                        //    { _EOBPaymentMode = PaymentModeV2.Voucher; }
                        //}
                        #endregion

                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Add();

                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                        if (Convert.ToString(dtChargeLineDetail.Rows[0]["AdjustmentAmount"]) != null && Convert.ToString(dtChargeLineDetail.Rows[0]["AdjustmentAmount"]).Trim() != "")
                        {
                            dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["sReceiptNo"] = "";
                            dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["dReceiptAmount"] = Convert.ToDecimal(0);
                        }
                        else
                        {
                            if (Convert.ToString(dtChargeLineDetail.Rows[0]["PaymentMethod"]) != null && Convert.ToString(dtChargeLineDetail.Rows[0]["PaymentMethod"]).Trim() != "")
                            {
                                if (Convert.ToString(dtChargeLineDetail.Rows[0]["PaymentMethod"]) == Convert.ToString(PaymentMethod.CREDIT))
                                {
                                    if (Convert.ToString(dtChargeLineDetail.Rows[0]["AccountNo"]) != null && Convert.ToString(dtChargeLineDetail.Rows[0]["AccountNo"]).Trim() != "")
                                    {
                                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["sReceiptNo"] = Convert.ToString(dtChargeLineDetail.Rows[0]["AccountNo"]); //"CGPAY_" + Convert.ToString(paymentInfoParameter.CloseDateAsNumber);
                                    }
                                    else
                                    {
                                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["sReceiptNo"] = "";
                                    }
                                }
                                else if (Convert.ToString(dtChargeLineDetail.Rows[0]["PaymentMethod"]) == Convert.ToString(PaymentMethod.CASH))
                                {
                                    dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["sReceiptNo"] = "CG_CASH_" + Convert.ToString(dtChargeLineDetail.Rows[0]["ReferenceNo"]); //"CGPAY_" + Convert.ToString(paymentInfoParameter.CloseDateAsNumber);

                                }
                                else if (Convert.ToString(dtChargeLineDetail.Rows[0]["PaymentMethod"]) == Convert.ToString(PaymentMethod.ACH))
                                {
                                    dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["sReceiptNo"] = "CG_ACH_" + Convert.ToString(dtChargeLineDetail.Rows[0]["ReferenceNo"]); //"CGPAY_" + Convert.ToString(paymentInfoParameter.CloseDateAsNumber);
                                }
                            }
                            
                            
                            dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["dReceiptAmount"] =Convert.ToDecimal(paymentInfoParameter.CheckAmountToDistribute);
                        }
                        //if (mskCheckDate.MaskCompleted)
                        //{
                        //  mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["dtReceiptDate"] = Convert.ToDateTime(dtChargeLineDetail.Rows[0]["TimeStamp"]);
                        //}
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nPayerType"] = gloAccountsV2.PayerTypeV2.Self.GetHashCode();
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nPayerID"] = paymentInfoParameter.PatientID;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["sPayerName"] = "";
                        // if (lblPaymetNo.Text != "")
                        {
                            dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentNo"] = Convert.ToString(_nCreditID);
                        }
                        //////None = 0,
                        //////Cash = 1,
                        //////Check = 2,
                        //////MoneyOrder = 3,
                        //////CreditCard = 4,
                        //////EFT = 5

                        if (Convert.ToString(dtChargeLineDetail.Rows[0]["PaymentMethod"]) != null && Convert.ToString(dtChargeLineDetail.Rows[0]["PaymentMethod"]).Trim() != "")
                        {
                            if (Convert.ToString(dtChargeLineDetail.Rows[0]["PaymentMethod"]) == Convert.ToString(PaymentMethod.CREDIT))
                            {
                                _EOBPaymentMode = gloAccountsV2.PaymentModeV2.CreditCard;
                            }
                            else if (Convert.ToString(dtChargeLineDetail.Rows[0]["PaymentMethod"]) == Convert.ToString(PaymentMethod.CASH))
                            {
                                _EOBPaymentMode = gloAccountsV2.PaymentModeV2.Cash;
                            }
                            else if (Convert.ToString(dtChargeLineDetail.Rows[0]["PaymentMethod"]) == Convert.ToString(PaymentMethod.ACH))
                            {
                                _EOBPaymentMode = gloAccountsV2.PaymentModeV2.Check;
                            }
                        }
                        if (_EOBPaymentMode == gloAccountsV2.PaymentModeV2.Check)
                        { dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 2; }
                        else if (_EOBPaymentMode == gloAccountsV2.PaymentModeV2.Cash)
                        { dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 1; }
                        else if (_EOBPaymentMode == gloAccountsV2.PaymentModeV2.EFT)
                        { dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 5; }
                        else if (_EOBPaymentMode == gloAccountsV2.PaymentModeV2.Voucher)
                        { dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 6; }
                        else if (_EOBPaymentMode == gloAccountsV2.PaymentModeV2.MoneyOrder)
                        { dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 3; }
                        else if (_EOBPaymentMode == gloAccountsV2.PaymentModeV2.CreditCard)
                        { dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 4; }
                        else
                        { dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 0; }

                        if (Convert.ToString(dtChargeLineDetail.Rows[0]["AdjustmentAmount"]) != null && Convert.ToString(dtChargeLineDetail.Rows[0]["AdjustmentAmount"]).Trim() != "")
                        {
                            dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 2;
                        }

                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(paymentInfoParameter.CloseDate);
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentTrayID"] = paymentInfoParameter.PaymentTrayID;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentTrayDesc"] = paymentInfoParameter.PaymentTrayName;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["sUserName"] = gloGlobal.gloPMGlobal.UserName;

                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nVoidType"] = 0;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nPAccountID"] = paymentInfoParameter.AccountID;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nAccountPatientID"] = paymentInfoParameter.AccountPatientID;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nGuarantorID"] = paymentInfoParameter.GuarantorID;

                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentNote"] = sPaymentNote;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = "Blank Tray";


                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.PatientPayment.GetHashCode();
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["Credits_EXTID"] = 0;
                        if (_EOBPaymentMode == gloAccountsV2.PaymentModeV2.CreditCard)
                        {
                            if (Convert.ToString(dtChargeLineDetail.Rows[0]["AccountType"]) != null && Convert.ToString(dtChargeLineDetail.Rows[0]["AccountType"]).Trim() != "")
                            {
                                switch (Convert.ToString(dtChargeLineDetail.Rows[0]["AccountType"]).ToUpper())
                                {
                                    case "VI":
                                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["CreditCardType"] = "Visa";
                                        break;
                                    case "MC":
                                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["CreditCardType"] = "Master Card";
                                        break;
                                    case "DI":
                                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["CreditCardType"] = "DISCOVER";
                                        break;
                                    case "AX":
                                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["CreditCardType"] = "American Express";
                                        break;
                                }
                            }
                            if (Convert.ToString(dtChargeLineDetail.Rows[0]["ReferenceNo"]) != null && Convert.ToString(dtChargeLineDetail.Rows[0]["ReferenceNo"]).Trim() != "")
                            {
                                dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["AuthorizationNo"] = Convert.ToString(dtChargeLineDetail.Rows[0]["ReferenceNo"]);
                            }
                            else
                            {
                                dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["AuthorizationNo"] = "";

                            }
                        
                        }
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["ClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["PaymentVoidDateTime"] = DBNull.Value;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["CreatedDateTime"] = DateTime.Now;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["ModifiedDateTime"] = DateTime.Now;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["SiteID"] = "";
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["IsFinished"] = false;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["IsERAPost"] = false;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["BPRID"] = DBNull.Value;
                        dsBulkPatientPayment_TVP.Tables["Credits"].Rows[dsBulkPatientPayment_TVP.Tables["Credits"].Rows.Count - 1]["CollectionAgencyContactID"] = 0;



                        dsBulkPatientPayment_TVP.Tables["Credits"].AcceptChanges();
                        #endregion
                        #region EOB Notes
                        #endregion
                        #region EOB  and Debits Table Entry

                       

                            if (dtChargeLineDetail != null && dtChargeLineDetail.Rows.Count > 0)
                           {
                                for (int i = 0; i < dtChargeLineDetail.Rows.Count; i++)
                                {
                                    if ((Convert.ToString(dtChargeLineDetail.Rows[i]["TxnType"]).ToUpper() != Convert.ToString(CGTransactionType.COPAY) && Convert.ToString(dtChargeLineDetail.Rows[i]["TxnType"]).ToUpper() != Convert.ToString(CGTransactionType.OTHER) && Convert.ToString(dtChargeLineDetail.Rows[i]["TxnType"]).ToUpper() != Convert.ToString(CGTransactionType.DECLINE) && Convert.ToString(dtChargeLineDetail.Rows[i]["TxnType"]).ToUpper() != Convert.ToString(CGTransactionType.LATEFEE) && Convert.ToString(dtChargeLineDetail.Rows[i]["TxnType"]).ToUpper() != Convert.ToString(CGTransactionType.REJECTFEE)))
                                    {
                                        //if (dtChargeLineDetail.Rows[i]["dPayment"] != null && Convert.ToString(dtChargeLineDetail.Rows[i]["dPayment"]).Trim() != "" && Convert.ToDecimal(dtChargeLineDetail.Rows[i]["dPayment"]) > 0)
                                        //{
                                        #region "EOB  Entry"
                                        decimal _fillPayAmt = 0;


                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Add();
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                        {
                                            dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailID"] = 0;
                                        }
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nPatientID"] = paymentInfoParameter.PatientID;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nContactID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["nTransactionID"]);
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["nTransactionDetailID"]);
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["TrackTransactionDetailID"]);
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["TrackTransactionID"]);
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["sEntryDesc"] = "PatientPayment";
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(dtChargeLineDetail.Rows[i]["sCPTCode"]);
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(paymentInfoParameter.CloseDate);

                                        if (Convert.ToString(dtChargeLineDetail.Rows[i]["dPayment"]) != null && Convert.ToString(dtChargeLineDetail.Rows[i]["dPayment"]) != "")
                                        {
                                            _fillPayAmt = Convert.ToDecimal(Convert.ToString(dtChargeLineDetail.Rows[i]["dPayment"]));
                                        }
                                        if (Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentAmount"]) != null && Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentAmount"]) != "")
                                        {
                                            dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["dWriteoffAmount"] = Convert.ToDecimal(dtChargeLineDetail.Rows[i]["AdjustmentAmount"]);
                                        }


                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nPaymentTrayID"] = paymentInfoParameter.PaymentTrayID;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["sPaymentTrayDesc"] = paymentInfoParameter.PaymentTrayName;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nVoidType"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nPAccountID"] = paymentInfoParameter.AccountID;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nGuarantorID"] = paymentInfoParameter.GuarantorID;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nAccountPatientID"] = paymentInfoParameter.AccountPatientID;

                                        if (Convert.ToString(dtChargeLineDetail.Rows[i]["dTotal"]) != null && Convert.ToString(Convert.ToString(dtChargeLineDetail.Rows[i]["dTotal"])).Trim() != "")
                                        {
                                            dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["dTotalChargeAmount"] = Convert.ToDecimal(Convert.ToString(dtChargeLineDetail.Rows[i]["dTotal"]));
                                        }

                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nEXTID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nCreditIDEXT"] = _nCreditID;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBIDEXT"] = _nEOBID;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailIDEXT"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextActionEXT"] = "";
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextPartyEXT"] = "";
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nNextPartyIDEXT"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["dtCreatedDateTimeEXT"] = DateTime.Now;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["dtModifiedDateTimeEXT"] = DateTime.Now;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["dtPaymentVoidDateTimeEXT"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nUserIDEXT"] = gloGlobal.gloPMGlobal.UserID;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["sUserNameEXT"] = gloGlobal.gloPMGlobal.UserName;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["bIsERAPostEXT"] = false;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nSVCIDEXT"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["sMachineNameEXT"] = Environment.MachineName;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["sSiteIDEXT"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["sVersionEXT"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nClinicIDEXT"] = gloGlobal.gloPMGlobal.ClinicID;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nCLPIdEXT"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionEXT"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["nTransactionID"]);
                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionDetailEXT"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["nTransactionDetailID"]);

                                        dsBulkPatientPayment_TVP.Tables["EOB"].Rows[dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1]["nCollectionAgencyContactID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOB"].AcceptChanges();

                                        #endregion
                                        #region " Set Line Reason Codes for Adjustment "

                                        if (Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentAmount"]) != null && Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentAmount"]) != "")
                                        {
                                            if (Convert.ToDecimal(Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentAmount"])) != 0)
                                            {
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Add();
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nID"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["nClaimNo"]);
                                                if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                {
                                                    dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentDetailID"] = 0;
                                                }

                                                if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                                {
                                                    dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsBulkPatientPayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                                    dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                                }
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["nTransactionID"]);
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["nTransactionDetailID"]);


                                                if (Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentCode"]) != null && Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentCode"]).Trim() != "")
                                                {
                                                    dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonCode"] = Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentCode"]);
                                                }
                                                if (Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentDesc"]) != null && Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentDesc"]).Trim() != "")
                                                {
                                                    dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonDescription"] = Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentDesc"]);
                                                }

                                                if (Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentAmount"]) != null && Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentAmount"]).Trim() != "")
                                                {
                                                    dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["dReasonAmount"] = Convert.ToDecimal(dtChargeLineDetail.Rows[i]["AdjustmentAmount"]);
                                                }

                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nType"] = gloAccountsV2.EOBCommentTypeV2.SystemReasonCode.GetHashCode();
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsVoid"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnID"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["TrackTransactionID"]);
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnDtlID"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["TrackTransactionDetailID"]);
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnLineNo"] = DBNull.Value;
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(dtChargeLineDetail.Rows[i]["TrackSubClaimNo"]);
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nCloseDate"] = paymentInfoParameter.CloseDateAsNumber;
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nVoidType"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidCloseDate"] = DBNull.Value;
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nSubType"] = DBNull.Value;
                                                dsBulkPatientPayment_TVP.Tables["ReasonCode"].AcceptChanges();
                                            }
                                        }

                                        #endregion " Set Line Reason Codes "
                                        #region "Debits"

                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Add();
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = _nCreditID;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                        {
                                            dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = 0;
                                            dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = 0;
                                        }
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nContactID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientID"] = paymentInfoParameter.PatientID;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nGuarantorID"] = paymentInfoParameter.GuarantorID;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nPAccountID"] = paymentInfoParameter.AccountID;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nAccountPatientID"] = paymentInfoParameter.AccountPatientID;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["nTransactionID"]);
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["nTransactionDetailID"]);
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["TrackTransactionID"]);
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(dtChargeLineDetail.Rows[i]["TrackTransactionDetailID"]);
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = gloAccountsV2.PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "PatientPayment";
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(dtChargeLineDetail.Rows[i]["sCPTCode"]);
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTDesc"] = Convert.ToString(dtChargeLineDetail.Rows[i]["sCPTDescription"]);

                                        if (Convert.ToString(dtChargeLineDetail.Rows[i]["dPayment"]) != null && Convert.ToString(dtChargeLineDetail.Rows[i]["dPayment"]) != "")
                                        {
                                            _fillPayAmt = Convert.ToDecimal(Convert.ToString(dtChargeLineDetail.Rows[i]["dPayment"]));
                                        }

                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;
                                        if (Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentAmount"]) != null && Convert.ToString(dtChargeLineDetail.Rows[i]["AdjustmentAmount"]) != "")
                                        {
                                            dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = Convert.ToDecimal(dtChargeLineDetail.Rows[i]["AdjustmentAmount"]);
                                        }
                                        // dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = 0;


                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = DBNull.Value;

                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentTrayID"] = paymentInfoParameter.PaymentTrayID;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentTrayDesc"] = paymentInfoParameter.PaymentTrayName;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["sUserName"] = gloGlobal.gloPMGlobal.UserName;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(paymentInfoParameter.CloseDate);
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nVoidType"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidCloseDate"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidDateTime"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsERAPost"] = false;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["sSiteID"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].Rows[dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["Debits"].AcceptChanges();
                                        #endregion "Debits"
                                        // }
                                    }
                                }
                                if (this.dsBulkPatientPayment_TVP.Tables["Debits"] != null && Convert.ToInt64(this.dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count) > 0)
                                {
                                    _dtUniqueIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(this.dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count);

                                    for (int i = 0; i <= dsBulkPatientPayment_TVP.Tables["Debits"].Rows.Count - 1; i++)
                                    {
                                        this.dsBulkPatientPayment_TVP.Tables["Debits"].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                                        this.dsBulkPatientPayment_TVP.Tables["Debits"].Rows[i]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID2"].ToString());
                                    }

                                    for (int i = 0; i <= dsBulkPatientPayment_TVP.Tables["EOB"].Rows.Count - 1; i++)
                                    {
                                        this.dsBulkPatientPayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                                        this.dsBulkPatientPayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailIDEXT"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                                    }
                                    for (int i = 0; i <= dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows.Count - 1; i++)
                                    {
                                        this.dsBulkPatientPayment_TVP.Tables["ReasonCode"].Rows[i]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID2"].ToString()); 
                                    }
                                }
                           }
                        #endregion
                        #region Reserve Entry
                            if (ReserveDetails != null && ReserveDetails.Trim().Length > 0)
                            {

                                string[] oList = null;
                                if (ReserveDetails != null)
                                {
                                    oList = ReserveDetails.Split('~');
                                }
                                if (oList != null && oList.Length == 9)
                                {
                                    //...Condition added to avoid zero reserve entry in debit table
                                    if (oList[0] != null && Convert.ToString(oList[0]).Trim() != "" && Convert.ToDecimal(oList[0]) > 0)
                                    {


                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Add();
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["nReserveID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["nCredits_RefID"] = _nCreditID;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["dReserveAmount"] = Convert.ToDecimal(oList[0]);
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["nReserveType"] = gloAccountsV2.ReserveEntryTypeV2.PatientReserve.GetHashCode();
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["nPatientID"] = paymentInfoParameter.PatientID;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["sUserName"] = gloGlobal.gloPMGlobal.UserName;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtCloseDate"] = paymentInfoParameter.CloseDate;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["nVoidType"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["nPAccountID"] =paymentInfoParameter.AccountID;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["nGuarantorID"] =paymentInfoParameter.GuarantorID;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["nAccountPatientID"] =paymentInfoParameter.AccountPatientID;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["sReserveNote"] = "Reserved";
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                                        dsBulkPatientPayment_TVP.Tables["Reserves"].AcceptChanges();

                                        dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows.Add();
                                        dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows[dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                                        dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows[dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nEOBPaymentDetailID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows[dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nTransactionID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows[dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nTrackTrnID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows[dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nPatientID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows[dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nProviderID"] = Convert.ToInt64(oList[7]); 
                                        dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows[dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nCollectionAgencycontactID"] = 0; 


                                        if (Convert.ToDateTime(oList[8]) != DateTime.MinValue) //dtReserveForDOS
                                        {
                                            dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows[dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["dtReserveForDOS"] =Convert.ToDateTime(oList[8]);
                                        }
                                        else
                                        {
                                            dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows[dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["dtReserveForDOS"] = DBNull.Value;
                                        }

                                        dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].AcceptChanges();

                                        #region "General Note"
                                        //   int rowNum = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Add();
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                        {
                                            dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["ID2"].ToString());
                                        }

                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = Convert.ToString(oList[1]).Trim();

                                        if (oList[3] != null && oList[3].ToString().Trim() != "")
                                        {
                                            dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = Convert.ToBoolean(oList[3]);
                                        }
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = Convert.ToDecimal(oList[0]);
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = paymentInfoParameter.CloseDateAsNumber;
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = gloAccountsV2.NoteTypeV2.Payment_PatientReserved.GetHashCode();
                                        if (oList[2] != null && oList[2].ToString().Trim() != "")
                                        {
                                            dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = Convert.ToInt32(oList[2]);
                                        }

                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;

                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = gloAccountsV2.BillingNoteTypeV2.None.GetHashCode();
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;

                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = paymentInfoParameter.CloseDateAsNumber; 
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].AcceptChanges();
                                        #endregion

                                        if ((EOBPaymentSubType)Convert.ToInt32(oList[2]) == EOBPaymentSubType.Advance)
                                        {
                                            #region "CPT Note"

                                            if (oList[4] != null && oList[4].ToString().TrimEnd() != "")
                                            {

                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Add();
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                                if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                {
                                                    dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = 0; 
                                                }
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "CPT";
                                                if (oList[4] != null && oList[4].ToString().TrimEnd() != "")
                                                {
                                                    dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = Convert.ToString(oList[4]).Replace('^', '~');
                                                }
                                                if (oList[3] != null && oList[3].ToString().Trim() != "")
                                                {
                                                    dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = Convert.ToBoolean(oList[3]);
                                                }
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = Convert.ToDecimal(oList[0]);

                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = paymentInfoParameter.CloseDate;

                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = gloAccountsV2.NoteTypeV2.Payment_PatientReserved.GetHashCode();
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = gloAccountsV2.NoteSubTypeV2.Advance.GetHashCode();
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;

                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = false;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = gloAccountsV2.BillingNoteTypeV2.CPT.GetHashCode();
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;

                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = paymentInfoParameter.CloseDate;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].AcceptChanges();
                                            }
                                            #endregion

                                            #region "ICD9 Note"

                                            if (oList[5] != null && oList[5].Trim() != "")
                                            {

                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Add();
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                                if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                {
                                                    dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = 0;
                                                }
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "ICD9";
                                                if (oList[5] != null && oList[5].Trim() != "")
                                                { dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = Convert.ToString(oList[5]).Replace('^', '~'); }

                                                if (oList[3] != null && oList[3].ToString().Trim() != "")
                                                {
                                                    dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = Convert.ToBoolean(oList[3]);
                                                }
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = Convert.ToDecimal(oList[0]);

                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = gloAccountsV2.NoteTypeV2.Payment_PatientReserved.GetHashCode();

                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = gloAccountsV2.NoteSubTypeV2.Advance.GetHashCode();
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;

                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = false;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = gloAccountsV2.BillingNoteTypeV2.ICD9.GetHashCode();
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;

                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DateTime.Now;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = paymentInfoParameter.CloseDate;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DateTime.Now;
                                                dsBulkPatientPayment_TVP.Tables["EOBNotes"].AcceptChanges();
                                            }
                                           
                                            #endregion
                                        }
                                        if (dsBulkPatientPayment_TVP.Tables["Reserves"] != null && Convert.ToInt64(dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count) > 0)
                                        {
                                            _dtUniqueReserveIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count);
                                            for (int i = 0; i <= dsBulkPatientPayment_TVP.Tables["Reserves"].Rows.Count - 1; i++)
                                            {
                                                dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[i]["nReserveID"] = Convert.ToInt64(_dtUniqueReserveIDs.Rows[i]["ID"].ToString());
                                            }

                                            for (int i = 0; i <= dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1; i++)
                                            {
                                                dsBulkPatientPayment_TVP.Tables["BL_Reserve_Association"].Rows[i]["nEOBPaymentDetailID"] = Convert.ToInt64(dsBulkPatientPayment_TVP.Tables["Reserves"].Rows[i]["nReserveID"].ToString());
                                            }

                                            DataView dv = dsBulkPatientPayment_TVP.Tables["EOBNotes"].DefaultView;
                                            DataTable dt = dv.ToTable(true, "nEOBPaymentID");
                                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                            {
                                                Int64 resID = Convert.ToInt64(_dtUniqueReserveIDs.Rows[i]["ID"].ToString());
                                                for (int iVar = 0; iVar <= dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows.Count - 1; iVar++)
                                                {
                                                    if (dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[iVar]["nEOBPaymentID"].ToString().Trim() == dt.Rows[i]["nEOBPaymentID"].ToString().Trim())
                                                        dsBulkPatientPayment_TVP.Tables["EOBNotes"].Rows[iVar]["nEOBPaymentDetailID"] = resID;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            if (this.dsBulkPatientPayment_TVP != null)
                            {

                                objPatientPayment = new gloAccountsV2.gloPatientPaymentV2();
                                returnPaymentId = objPatientPayment.SavePatientPayment(this.dsBulkPatientPayment_TVP);
                            }

                    }
                }
                       
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (objPatientPayment != null)
                {
                    objPatientPayment.Dispose();
                    objPatientPayment = null;
                }
            }
            return returnPaymentId;
        }
    }

    public static class AccountInfo
    {
        public static AccountOwnerInfo GetAccountInfo(Int64 accountId)
        {
            gloDatabaseLayer.DBLayer odbLayer = null;
            string sqlQueryText = string.Empty;
            DataTable dtAccountInfo = null;
            AccountOwnerInfo accountOwnerInfo = null;

            try
            {
                sqlQueryText =
                    " SELECT " +
                    " AC.nPAccountID, AC.sAccountNo, AC.nGuarantorID, AP.nAccountPatientID, AP.nPatientID " +
                    " FROM PA_Accounts AC WITH(NOLOCK) INNER JOIN PA_Accounts_Patients AP WITH(NOLOCK) " +
                    " ON AC.nPAccountID = AP.nPAccountID " +
                    " INNER JOIN Patient P WITH(NOLOCK) ON P.nPatientID = AP.nPatientID  " +
                    " WHERE AC.nPAccountID = " + accountId + " AND ISNULL(bIsOwnAccount,0) = 1";

                odbLayer = new DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                odbLayer.Connect(false);
                odbLayer.Retrive_Query(sqlQueryText, out dtAccountInfo);
                odbLayer.Disconnect();

                if (dtAccountInfo != null && dtAccountInfo.Rows.Count > 0)
                {
                    accountOwnerInfo = new AccountOwnerInfo(
                        Convert.ToInt64(dtAccountInfo.Rows[0]["nPAccountID"]),
                        Convert.ToString(dtAccountInfo.Rows[0]["sAccountNo"]),
                        Convert.ToInt64(dtAccountInfo.Rows[0]["nGuarantorID"]),
                        Convert.ToInt64(dtAccountInfo.Rows[0]["nAccountPatientID"]),
                        Convert.ToInt64(dtAccountInfo.Rows[0]["nPatientID"])
                       );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (odbLayer != null) { odbLayer.Dispose(); odbLayer = null; }
                if (dtAccountInfo != null) { dtAccountInfo.Clear(); dtAccountInfo.Dispose(); dtAccountInfo = null; }
            }

            return accountOwnerInfo;
        }
    }
}
