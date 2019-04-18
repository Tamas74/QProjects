using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using gloSettings;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace gloBilling
{

    namespace EOBPayment
    {
        public class gloEOBPaymentInsurance
        {

            #region "Private Variables"

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private string _databaseConnectionString = "";
            private Int64 _clinicId = 0;
            private Int64 _userId = 0;
            private string _userName = "";
            private string _messageBoxCaption = "";

            #endregion

            #region " Property Procedures "

            public string DatabaseConnectionString
            {
                get { return _databaseConnectionString; }
                set { _databaseConnectionString = value; }
            }
            public Int64 ClinicID
            {
                get { return _clinicId; }
                set { _clinicId = value; }
            }
            public Int64 UserID
            {
                get { return _userId; }
                set { _userId = value; }
            }
            public string UserName
            {
                get { return _userName; }
                set { _userName = value; }
            }

            #endregion " Property Procedures "

            #region "Constructor & Distructor"

            public gloEOBPaymentInsurance(string Databaseconnectionstring)
            {
                _databaseConnectionString = Databaseconnectionstring;

                #region " Retrive ClinicID from appSetting "

                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _clinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _clinicId = 0; }
                }
                else
                { _clinicId = 0; }

                #endregion " Retrive ClinicID from appSetting "

                #region " Retrive UserID from appSettings "

                if (appSettings["UserID"] != null)
                {
                    if (appSettings["UserID"] != "")
                    {
                        _userId = Convert.ToInt64(appSettings["UserID"]);
                    }
                }
                else
                {
                    _userId = 0;
                }

                #endregion

                #region " Retrive UserName from appSettings "

                if (appSettings["UserName"] != null)
                {
                    if (appSettings["UserName"] != "")
                    {
                        _userName = Convert.ToString(appSettings["UserName"]);
                    }
                }
                else
                {
                    _userName = "";
                }

                #endregion

                #region " Retrieve MessageBoxCaption from AppSettings "

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _messageBoxCaption = "";
                    }
                }
                else
                { _messageBoxCaption = ""; }

                #endregion
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

            ~gloEOBPaymentInsurance()
            {
                Dispose(false);
            }

            #endregion

            #region " Private & Public Methods "

            public EOBPayment.Common.PaymentInsuranceClaim GetBillingTransaction(Int64 ClaimNo)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtBillingTransaction = null;
                DataTable dtBillingTransactionLines = null;
                EOBPayment.Common.PaymentInsuranceClaim oPaymentClaim = null;
                EOBPayment.Common.PaymentInsuranceLine oPaymentLine = null;
                Int64 _claimTranId = 0;

                try
                {
                    _claimTranId = GetBillingTransactionID(ClaimNo);

                    if (_claimTranId > 0)
                    {
                        #region "Retrive Billing Master Transaction"

                        oParameters.Clear();
                        oParameters.Add("@nTransactionID", _claimTranId, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Connect(false);
                        oDB.Retrive("BL_SELECT_PaymentTransaction_MST", oParameters, out dtBillingTransaction);
                        oDB.Disconnect();
                        oParameters.Clear();

                        #endregion

                        #region " Set Transaction Master Data "

                        if (dtBillingTransaction != null && dtBillingTransaction.Rows.Count > 0)
                        {
                            int nTrnCntr = 0;

                            oPaymentClaim = new global::gloBilling.EOBPayment.Common.PaymentInsuranceClaim();
                            oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"]);
                            oPaymentClaim.DisplayClaimNo = GetFormattedClaimPaymentNumber(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"].ToString());
                            oPaymentClaim.ClaimNoPrefix = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["sCaseNoPrefix"]);
                            oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionID"]);
                            oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionDate"]);
                            oPaymentClaim.PatientID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nPatientID"]);
                            oPaymentClaim.PatientName = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["PatientName"]);

                            #region "Retrive Billing Transaction Lines Data "

                            oParameters.Clear();
                            oParameters.Add("@nTransactionID", oPaymentClaim.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nTransactionDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nPatientID", oPaymentClaim.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDB.Connect(false);
                            oDB.Retrive("BL_SELECT_PaymentTransaction_Lines", oParameters, out dtBillingTransactionLines);
                            oDB.Disconnect();
                            oParameters.Clear();

                            if (dtBillingTransactionLines != null && dtBillingTransactionLines.Rows.Count > 0)
                            {
                                for (int nTrnLineCntr = 0; nTrnLineCntr < dtBillingTransactionLines.Rows.Count; nTrnLineCntr++)
                                {
                                    oPaymentLine = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLine();

                                    oPaymentLine.PatientID = oPaymentClaim.PatientID;
                                    oPaymentLine.BLTransactionID = oPaymentClaim.BillingTransactionID;
                                    oPaymentLine.BLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDetailID"].ToString());
                                    oPaymentLine.BLTransactionLineNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionLineNo"].ToString());
                                    oPaymentLine.ClaimNumber = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["ClaimNumber"].ToString());
                                    oPaymentLine.DOSFrom = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nFromDate"].ToString());
                                    oPaymentLine.DOSTo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nToDate"].ToString());
                                    oPaymentLine.CPTCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTCode"].ToString());
                                    oPaymentLine.CPTDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTDescription"].ToString());

                                    oPaymentLine.BLInsuranceID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceID"].ToString());
                                    oPaymentLine.BLInsuranceName = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceName"].ToString());
                                    oPaymentLine.BLInsuranceFlag = ((InsuranceTypeFlag)Convert.ToInt32(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceFlag"]));

                                    oPaymentLine.Charges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dCharges"]);
                                    oPaymentLine.Unit = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dUnit"]);
                                    oPaymentLine.TotalCharges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dTotal"]);
                                    oPaymentLine.Allowed = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"]);
                                    oPaymentLine.WriteOff = 0;
                                    oPaymentLine.NonCovered = 0;
                                    oPaymentLine.InsuranceAmount = 0;
                                    oPaymentLine.Copay = 0;
                                    oPaymentLine.Deductible = 0;
                                    oPaymentLine.CoInsurance = 0;
                                    oPaymentLine.Withhold = 0;


                                    oPaymentLine.LinePaidAmount = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalPaidAmount"]);
                                    oPaymentLine.LinePaidByPatient = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalPatientPaidAmount"]);
                                    oPaymentLine.LinePaidByInsurance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalInsurancePaidAmount"]);
                                    oPaymentLine.LinePaidWriteOff = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalWriteOff"]);
                                    oPaymentLine.LinePaidWithHold = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalWithHold"]);
                                    oPaymentLine.LineBalance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalBalanceAmount"]);

                                    oPaymentClaim.CliamLines.Add(oPaymentLine);
                                }
                            }

                            #endregion

                        }

                        #endregion " Set Transaction Master Data "

                    }

                }
                catch (gloDatabaseLayer.DBException ex)
                { ex.ERROR_Log(ex.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
                finally
                {
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); dtBillingTransaction = null; }
                    if (dtBillingTransactionLines != null) { dtBillingTransactionLines.Dispose(); dtBillingTransactionLines = null; }
                }

                return oPaymentClaim;
            }

            public EOBPayment.Common.PaymentInsuranceClaim GetBillingTransaction(Int64 ClaimNo, Int64 InsuranceCompanyID, Int64 InsContactID, Int64 InsPlanID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtBillingTransaction = null;
                DataTable dtBillingTransactionLines = null;
                EOBPayment.Common.PaymentInsuranceClaim oPaymentClaim = null;
                EOBPayment.Common.PaymentInsuranceLine oPaymentLine = null;
                Int64 _claimTranId = 0;

                try
                {
                    _claimTranId = GetBillingTransactionID(ClaimNo);

                    if (_claimTranId > 0)
                    {
                        #region "Retrive Billing Master Transaction"

                        oParameters.Clear();
                        oParameters.Add("@nTransactionID", _claimTranId, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Connect(false);
                        oDB.Retrive("BL_SELECT_PaymentTransaction_MST", oParameters, out dtBillingTransaction);
                        oDB.Disconnect();
                        oParameters.Clear();

                        #endregion

                        #region " Set Transaction Master Data "

                        if (dtBillingTransaction != null && dtBillingTransaction.Rows.Count > 0)
                        {
                            int nTrnCntr = 0;

                            oPaymentClaim = new global::gloBilling.EOBPayment.Common.PaymentInsuranceClaim();
                            oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"]);
                            oPaymentClaim.DisplayClaimNo = GetFormattedClaimPaymentNumber(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"].ToString());
                            oPaymentClaim.ClaimNoPrefix = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["sCaseNoPrefix"]);
                            oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionID"]);
                            oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionDate"]);
                            oPaymentClaim.PatientID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nPatientID"]);
                            oPaymentClaim.PatientName = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["PatientName"]);

                            #region "Retrive Billing Transaction Lines Data "

                            oParameters.Clear();
                            oParameters.Add("@nInsCompanyID", InsuranceCompanyID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nInsContactID", InsContactID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                            oParameters.Add("@nInsPlanID", InsPlanID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                            oParameters.Add("@nTransactionID", oPaymentClaim.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nTransactionDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nPatientID", oPaymentClaim.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDB.Connect(false);

                            oDB.Retrive("BL_SELECT_PaymentTransaction_Lines_InsCompany", oParameters, out dtBillingTransactionLines);

                            oDB.Disconnect();
                            oParameters.Clear();

                            if (dtBillingTransactionLines != null && dtBillingTransactionLines.Rows.Count > 0)
                            {
                                for (int nTrnLineCntr = 0; nTrnLineCntr < dtBillingTransactionLines.Rows.Count; nTrnLineCntr++)
                                {
                                    oPaymentLine = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLine();

                                    oPaymentLine.PatientID = oPaymentClaim.PatientID;
                                    oPaymentLine.BLTransactionID = oPaymentClaim.BillingTransactionID;
                                    oPaymentLine.BLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDetailID"].ToString());
                                    oPaymentLine.BLTransactionLineNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionLineNo"].ToString());
                                    oPaymentLine.ClaimNumber = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["ClaimNumber"].ToString());
                                    oPaymentLine.DOSFrom = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nFromDate"].ToString());
                                    oPaymentLine.DOSTo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nToDate"].ToString());
                                    oPaymentLine.CPTCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTCode"].ToString());
                                    oPaymentLine.CPTDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTDescription"].ToString());

                                    oPaymentLine.BLInsuranceID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceID"].ToString());
                                    oPaymentLine.BLInsuranceName = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceName"].ToString());
                                    oPaymentLine.BLInsuranceFlag = ((InsuranceTypeFlag)Convert.ToInt32(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceFlag"]));

                                    oPaymentLine.Charges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dCharges"]);
                                    oPaymentLine.Unit = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dUnit"]);
                                    oPaymentLine.TotalCharges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dTotal"]);
                                    oPaymentLine.Allowed = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"]);
                                    oPaymentLine.WriteOff = 0;
                                    oPaymentLine.NonCovered = 0;
                                    oPaymentLine.InsuranceAmount = 0;
                                    oPaymentLine.Copay = 0;
                                    oPaymentLine.Deductible = 0;
                                    oPaymentLine.CoInsurance = 0;
                                    oPaymentLine.Withhold = 0;

                                    oPaymentLine.LinePaidAmount = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalPaidAmount"]);
                                    oPaymentLine.LinePaidByPatient = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalPatientPaidAmount"]);
                                    oPaymentLine.LinePaidByInsurance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalInsurancePaidAmount"]);
                                    oPaymentLine.LinePaidWriteOff = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalWriteOff"]);
                                    oPaymentLine.LinePaidWithHold = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalWithHold"]);
                                    oPaymentLine.LineBalance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalBalanceAmount"]);

                                    //LastInsPaidAmount,LastInsWriteOffAmount,LastInsWithholdAmount,LastInsAllowedAmount,
                                    //LastInsCopayAmount,LastInsDeductibleAmount,LastInsCoinsuranceAmount

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsAllowedAmount"]).Trim() != "")
                                    { oPaymentLine.Last_allowed = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsAllowedAmount"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsPaidAmount"]).Trim() != "")
                                    { oPaymentLine.Last_payment = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsPaidAmount"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsWriteOffAmount"]).Trim() != "")
                                    { oPaymentLine.Last_writeoff = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsWriteOffAmount"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsCopayAmount"]).Trim() != "")
                                    { oPaymentLine.Last_copay = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsCopayAmount"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsDeductibleAmount"]).Trim() != "")
                                    { oPaymentLine.Last_deductible = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsDeductibleAmount"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsCoinsuranceAmount"]).Trim() != "")
                                    { oPaymentLine.Last_coinsurance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsCoinsuranceAmount"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsWithholdAmount"]).Trim() != "")
                                    { oPaymentLine.Last_withhold = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsWithholdAmount"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["IsCorrection"]).Trim() != "")
                                    { oPaymentLine.Iscorrection = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["IsCorrection"]); }

                                    oPaymentClaim.CliamLines.Add(oPaymentLine);
                                }
                            }

                            #endregion

                        }

                        #endregion " Set Transaction Master Data "

                    }

                }
                catch (gloDatabaseLayer.DBException ex)
                { ex.ERROR_Log(ex.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
                finally
                {
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); dtBillingTransaction = null; }
                    if (dtBillingTransactionLines != null) { dtBillingTransactionLines.Dispose(); dtBillingTransactionLines = null; }
   
                }

                return oPaymentClaim;
            }

            public EOBPayment.Common.PaymentInsuranceClaim GetPaymentTransaction(Int64 TransactionID, Int64 EOBPaymentID, Int64 EOBID, Int64 PatientID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtBillingTransaction = null;
                DataTable dtBillingTransactionLines = null;
                EOBPayment.Common.PaymentInsuranceClaim oPaymentClaim = null;
                EOBPayment.Common.PaymentInsuranceLine oPaymentLine = null;

                try
                {

                    if (TransactionID > 0)
                    {
                        #region "Retrive Billing Master Transaction"

                        oParameters.Clear();
                        oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Connect(false);
                        oDB.Retrive("BL_SELECT_PaymentTransaction_MST", oParameters, out dtBillingTransaction);
                        oDB.Disconnect();
                        oParameters.Clear();

                        #endregion

                        #region " Set Transaction Master Data "

                        if (dtBillingTransaction != null && dtBillingTransaction.Rows.Count > 0)
                        {
                            int nTrnCntr = 0;

                            oPaymentClaim = new global::gloBilling.EOBPayment.Common.PaymentInsuranceClaim();
                            oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"]);
                            oPaymentClaim.DisplayClaimNo = GetFormattedClaimPaymentNumber(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"].ToString());
                            oPaymentClaim.ClaimNoPrefix = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["sCaseNoPrefix"]);
                            oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionID"]);
                            oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionDate"]);
                            oPaymentClaim.PatientID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nPatientID"]);
                            oPaymentClaim.PatientName = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["PatientName"]);

                            #region "Retrive Billing Transaction Lines Data "

                            oParameters.Clear();
                            oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nEOBID", EOBID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                            oDB.Connect(false);
                            oDB.Retrive("BL_SELECT_PaymentTransaction_Lines_Modify", oParameters, out dtBillingTransactionLines);
                            oDB.Disconnect();
                            oParameters.Clear();

                            if (dtBillingTransactionLines != null && dtBillingTransactionLines.Rows.Count > 0)
                            {
                                for (int nTrnLineCntr = 0; nTrnLineCntr < dtBillingTransactionLines.Rows.Count; nTrnLineCntr++)
                                {
                                    oPaymentLine = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLine();

                                    oPaymentLine.PatientID = oPaymentClaim.PatientID;
                                    oPaymentLine.BLTransactionID = oPaymentClaim.BillingTransactionID;
                                    oPaymentLine.BLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDetailID"].ToString());
                                    oPaymentLine.BLTransactionLineNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionLineNo"].ToString());
                                    oPaymentLine.ClaimNumber = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["ClaimNumber"].ToString());
                                    oPaymentLine.DOSFrom = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nFromDate"].ToString());
                                    oPaymentLine.DOSTo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nToDate"].ToString());
                                    oPaymentLine.CPTCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTCode"].ToString());
                                    oPaymentLine.CPTDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTDescription"].ToString());

                                    //oPaymentLine.BLInsuranceID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceID"].ToString());
                                    //oPaymentLine.BLInsuranceName = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceName"].ToString());
                                    //oPaymentLine.BLInsuranceFlag = ((InsuranceTypeFlag)Convert.ToInt32(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceFlag"]));

                                    oPaymentLine.Charges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dCharges"]);
                                    oPaymentLine.Unit = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dUnit"]);
                                    oPaymentLine.TotalCharges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dTotal"]);
                                    oPaymentLine.Allowed = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"]);

                                    oPaymentLine.WriteOff = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dWriteOff"]);
                                    oPaymentLine.NonCovered = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dNotCovered"]);
                                    oPaymentLine.InsuranceAmount = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dPayment"]);
                                    oPaymentLine.Copay = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dCopay"]);
                                    oPaymentLine.Deductible = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dDeductible"]);
                                    oPaymentLine.CoInsurance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dCoInsurance"]);
                                    oPaymentLine.Withhold = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dWithhold"]);


                                    //oPaymentLine.LinePaidAmount = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalPaidAmount"]);
                                    //oPaymentLine.LinePaidByPatient = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalPatientPaidAmount"]);
                                    //oPaymentLine.LinePaidByInsurance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalInsurancePaidAmount"]);
                                    //oPaymentLine.LinePaidWriteOff = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalWriteOff"]);
                                    //oPaymentLine.LinePaidWithHold = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalWithHold"]);
                                    //oPaymentLine.LineBalance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalBalanceAmount"]);

                                    oPaymentClaim.CliamLines.Add(oPaymentLine);
                                }
                            }

                            #endregion

                        }

                        #endregion " Set Transaction Master Data "

                    }

                }
                catch (gloDatabaseLayer.DBException ex)
                { ex.ERROR_Log(ex.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
                finally
                {
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); dtBillingTransaction = null; }
                    if (dtBillingTransactionLines != null) { dtBillingTransactionLines.Dispose(); dtBillingTransactionLines = null; }
   
                }

                return oPaymentClaim;
            }

            #region " Split Methods "

            public EOBPayment.Common.PaymentInsuranceClaim GetBillingTransaction(string Claim_SubClaimNo, Int64 InsuranceCompanyID, Int64 InsContactID, Int64 InsPlanID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtBillingTransaction = null;
                DataTable dtBillingTransactionLines = null;
                EOBPayment.Common.PaymentInsuranceClaim oPaymentClaim = null;
                EOBPayment.Common.PaymentInsuranceLine oPaymentLine = null;
                Int64 _claimTranId = 0;
                Int64 _claimTranTrackingId = 0;
                DataTable _dtTranID = null;

                try
                {
                    //_claimTranId = GetBillingTransactionID(ClaimNo);
                    //Claim_SubClaimNo = "2-3";
                    _dtTranID = GetSplitBillingTransactionID(Claim_SubClaimNo);

                    if (_dtTranID != null && _dtTranID.Rows.Count > 0)
                    {
                        _claimTranId = Convert.ToInt64(_dtTranID.Rows[0]["nTransactionMasterID"]);
                        _claimTranTrackingId = Convert.ToInt64(_dtTranID.Rows[0]["nTransactionID"]);
                    }

                    if (_claimTranId > 0 && _claimTranTrackingId > 0)
                    {
                        #region "Retrive Billing Master Transaction"

                        oParameters.Clear();
                        oParameters.Add("@nTransactionID", _claimTranId, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nTrackTransactionID", _claimTranTrackingId, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Connect(false);
                        oDB.Retrive("BL_SELECT_PaymentTransaction_MST_Tracking", oParameters, out dtBillingTransaction);
                        oDB.Disconnect();
                        oParameters.Clear();

                        #endregion

                        #region " Set Transaction Master Data "

                        if (dtBillingTransaction != null && dtBillingTransaction.Rows.Count > 0)
                        {
                            int nTrnCntr = 0;

                            oPaymentClaim = new global::gloBilling.EOBPayment.Common.PaymentInsuranceClaim();

                            oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"]);
                            oPaymentClaim.DisplayClaimNo = GetFormattedClaimPaymentNumber(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"].ToString());
                            oPaymentClaim.ClaimNoPrefix = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["sCaseNoPrefix"]);
                            oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionID"]);
                            oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionDate"]);

                            oPaymentClaim.TrackBillingTrnID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["TrackingTrnID"]);
                            oPaymentClaim.SubClaimNo = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["SubClaimNo"]);

                            oPaymentClaim.PatientID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nPatientID"]);
                            oPaymentClaim.PatientName = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["PatientName"]);

                            #region "Retrive Billing Transaction Lines Data "

                            oParameters.Clear();
                            oParameters.Add("@nInsCompanyID", InsuranceCompanyID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nInsContactID", InsContactID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                            oParameters.Add("@nInsPlanID", InsPlanID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                            oParameters.Add("@nTransactionID", oPaymentClaim.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nTransactionDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nPatientID", oPaymentClaim.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nTrackingTransactionID", oPaymentClaim.TrackBillingTrnID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDB.Connect(false);

                            oDB.Retrive("BL_SELECT_PaymentTransaction_Lines_InsCompany_Tracking", oParameters, out dtBillingTransactionLines);

                            oDB.Disconnect();
                            oParameters.Clear();

                            if (dtBillingTransactionLines != null && dtBillingTransactionLines.Rows.Count > 0)
                            {
                                for (int nTrnLineCntr = 0; nTrnLineCntr < dtBillingTransactionLines.Rows.Count; nTrnLineCntr++)
                                {
                                    oPaymentLine = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLine();

                                    oPaymentLine.PatientID = oPaymentClaim.PatientID;

                                    oPaymentLine.BLTransactionID = oPaymentClaim.BillingTransactionID;
                                    oPaymentLine.BLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDetailID"].ToString());
                                    oPaymentLine.BLTransactionLineNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionLineNo"].ToString());
                                    oPaymentLine.ClaimNumber = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["ClaimNumber"].ToString());

                                    oPaymentLine.TrackBLTransactionID = oPaymentClaim.TrackBillingTrnID;
                                    oPaymentLine.TrackBLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackTrnDtlID"].ToString());
                                    oPaymentLine.TrackBLTransactionLineNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["TrackTrnLineNo"].ToString());
                                    oPaymentLine.SubClaimNumber = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SubClaimNo"].ToString());


                                    oPaymentLine.DOSFrom = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nFromDate"].ToString());
                                    oPaymentLine.DOSTo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nToDate"].ToString());
                                    oPaymentLine.CPTCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTCode"].ToString());
                                    oPaymentLine.CPTDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTDescription"].ToString());

                                    oPaymentLine.BLInsuranceID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceID"].ToString());
                                    oPaymentLine.BLInsuranceName = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceName"].ToString());
                                    oPaymentLine.BLInsuranceFlag = ((InsuranceTypeFlag)Convert.ToInt32(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceFlag"]));

                                    oPaymentLine.Charges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dCharges"]);
                                    oPaymentLine.Unit = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dUnit"]);
                                    oPaymentLine.TotalCharges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dTotal"]);
                                    oPaymentLine.Allowed = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"]);
                                    oPaymentLine.WriteOff = 0;
                                    oPaymentLine.NonCovered = 0;
                                    oPaymentLine.InsuranceAmount = 0;
                                    oPaymentLine.Copay = 0;
                                    oPaymentLine.Deductible = 0;
                                    oPaymentLine.CoInsurance = 0;
                                    oPaymentLine.Withhold = 0;

                                    oPaymentLine.LinePaidAmount = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalPaidAmount"]);
                                    oPaymentLine.LinePaidByPatient = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalPatientPaidAmount"]);
                                    oPaymentLine.LinePaidByInsurance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalInsurancePaidAmount"]);
                                    oPaymentLine.LinePaidWriteOff = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalWriteOff"]);
                                    oPaymentLine.LinePaidWithHold = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalWithHold"]);
                                    oPaymentLine.LineBalance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalBalanceAmount"]);

                                    //LastInsPaidAmount,LastInsWriteOffAmount,LastInsWithholdAmount,LastInsAllowedAmount,
                                    //LastInsCopayAmount,LastInsDeductibleAmount,LastInsCoinsuranceAmount

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsAllowedAmount"]).Trim() != "")
                                    { oPaymentLine.Last_allowed = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsAllowedAmount"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsPaidAmount"]).Trim() != "")
                                    { oPaymentLine.Last_payment = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsPaidAmount"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsWriteOffAmount"]).Trim() != "")
                                    { oPaymentLine.Last_writeoff = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsWriteOffAmount"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsCopayAmount"]).Trim() != "")
                                    { oPaymentLine.Last_copay = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsCopayAmount"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsDeductibleAmount"]).Trim() != "")
                                    { oPaymentLine.Last_deductible = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsDeductibleAmount"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsCoinsuranceAmount"]).Trim() != "")
                                    { oPaymentLine.Last_coinsurance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsCoinsuranceAmount"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsWithholdAmount"]).Trim() != "")
                                    { oPaymentLine.Last_withhold = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LastInsWithholdAmount"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["IsCorrection"]).Trim() != "")
                                    { oPaymentLine.Iscorrection = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["IsCorrection"]); }

                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["IsSplitted"]).Trim() != "")
                                    { oPaymentLine.IsSplitted = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["IsSplitted"]); }
                                    else
                                    { oPaymentLine.IsSplitted = false; }

                                    oPaymentClaim.CliamLines.Add(oPaymentLine);
                                }
                            }

                            #endregion

                        }

                        #endregion " Set Transaction Master Data "

                    }

                }
                catch (gloDatabaseLayer.DBException ex)
                { ex.ERROR_Log(ex.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
                finally
                {
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); dtBillingTransaction = null; }
                    if (dtBillingTransactionLines != null) { dtBillingTransactionLines.Dispose(); dtBillingTransactionLines = null; }
   
                }

                return oPaymentClaim;
            }

            #endregion " Split Methods "

            public Int64 GetBillingTransactionID(Int64 ClaimNo)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                Object _retBillingTranId = null;
                string _sqlQuery = "";
                Int64 _TransactionMstID = 0;

                try
                {
                    if (ClaimNo > 0)
                    {
                        _sqlQuery = " SELECT DISTINCT BL_Transaction_MST.nTransactionID FROM BL_Transaction_MST WITH (NOLOCK) " +
                                   " WHERE " +
                                   " BL_Transaction_MST.nTransactionID IS NOT NULL AND BL_Transaction_MST.nTransactionID > 0 " +
                                   " AND BL_Transaction_MST.nClaimNo = " + ClaimNo + " " +
                                   " AND ISNULL(BL_Transaction_MST.bIsVoid,0) = 0 " +
                                   " AND BL_Transaction_MST.nClinicID = " + _clinicId + " ";

                        oDB.Connect(false);
                        _retBillingTranId = oDB.ExecuteScalar_Query(_sqlQuery);
                        oDB.Disconnect();

                        if (_retBillingTranId != null && Convert.ToString(_retBillingTranId).Trim() != "")
                        { _TransactionMstID = Convert.ToInt64(_retBillingTranId); }
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                { ex.ERROR_Log(ex.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (_retBillingTranId != null) { _retBillingTranId = null; }
                }

                return _TransactionMstID;
            }

            public DataTable GetSplitBillingTransactionID(string ClaimSubClaimNo)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
               // string _sqlQuery = "";
                string[] oList = null;
                Int64 _claimNo = 0;
                string _subClaimNo = "";
                DataTable _dt = null;

                try
                {
                    if (ClaimSubClaimNo.Trim() != "")
                    {
                        oList = ClaimSubClaimNo.Trim().Split('-');

                        if (oList.Length == 2)
                        {
                            _claimNo = Convert.ToInt64(oList[0]);
                            _subClaimNo = Convert.ToString(oList[1]);
                        }
                        else if (oList.Length == 1)
                        {
                            _claimNo = Convert.ToInt64(oList[0]);
                        }

                        //if (_subClaimNo.Trim() != "")
                        //{
                        //    _sqlQuery = " SELECT DISTINCT nTransactionMasterID, nTransactionID " +
                        //    " FROM BL_Transaction_Claim_MST " +
                        //    " WHERE      " +
                        //    " (nClaimNo = " + _claimNo + ")  " +
                        //    " AND (nSubClaimNo = '" + _subClaimNo + "')  " +
                        //    " AND ISNULL(nClaimStatus,0) = "+ClaimStatus.Open.GetHashCode()+" "+
                        //    " AND ISNULL(bIsVoid,0) = 0  " +
                        //    " AND (nClinicID = " + _clinicId + ") ";
                        //}
                        //else
                        //{
                        //    _sqlQuery = " SELECT DISTINCT nTransactionMasterID, nTransactionID " +
                        //    " FROM BL_Transaction_Claim_MST " +
                        //    " WHERE      " +
                        //    " (nClaimNo = " + _claimNo + ")  " +
                        //    " AND ISNULL(nClaimStatus,0) = " + ClaimStatus.Open.GetHashCode() + " " +
                        //    " AND ISNULL(bIsVoid,0) = 0  " +
                        //    " AND (nClinicID = " + _clinicId + ") ";
                        //}

                        oParameters.Add("@nClaimno", _claimNo, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@sSubClaimno", _subClaimNo, ParameterDirection.Input, SqlDbType.VarChar, 50);
                        oDB.Connect(false);
                        oDB.Retrive("BL_Select_SplitClaims", oParameters, out _dt);
                        oDB.Disconnect();
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                { ex.ERROR_Log(ex.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                }

                return _dt;
            }


            /// <summary>
            /// Function shifted to gloBilling\Payment\InsurancePayment.cs 
            /// </summary>
            /// <param name="NumberSize"></param>
            /// <returns></returns>
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

            public Int64 SaveEOB(EOBPayment.Common.PaymentInsurance EOBPaymentInsurance, bool IsSaveForVoid)
            {
                System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(_databaseConnectionString);
                System.Data.SqlClient.SqlCommand _sqlCommand = null;
                System.Data.SqlClient.SqlTransaction _sqlTransaction = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);

                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                object _retVal = null;
                object _valRet = null;
                Int64 _EOBPayId = 0;
                Int64 _EOBId = 0;
                Int64 _EOBDtlId = 0;
                Int64 _EOBPayDtlId = 0;
                Int64 _EOBVoidPayId = 0;

                bool _UseExtEOBID = false;
                EOBPayment.Common.PaymentInsuranceLine PaymentInsClaimLine = null;
                EOBPayment.Common.EOBInsurancePaymentDetail EOBInsPayDtl = null;
                EOBPayment.Common.EOBInsuranceReserveDetail EOBInsPayResDtl = null;
                int _result = 0;

                try
                {
                    if (EOBPaymentInsurance != null)
                    {
                        _sqlConnection.Open();
                        _sqlTransaction = _sqlConnection.BeginTransaction();

                        #region " Master Data Save "

                        //nEOBPaymentID,nEOBRefNO,sPayerName,nPayerID,nPayerType,nPaymentMode,sCheckNumber,nCheckAmount,nCheckDate
                        //nMSTAccountID,nMSTAccountType,nPaymentTrayID,sPaymentTrayName,nCloseDate,sCardType,sCardSecurityNo
                        //nCardID,nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                        _EOBPayId = 0;
                        oParameters.Clear();

                        oParameters.Add("@sPaymentNo", EOBPaymentInsurance.PaymentNumber, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sPaymentNoPrefix", EOBPaymentInsurance.PaymentNumberPefix, ParameterDirection.Input, SqlDbType.VarChar);

                        oParameters.Add("@nEOBPaymentID", EOBPaymentInsurance.EOBPaymentID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nEOBRefNO", EOBPaymentInsurance.EOBRefNO, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sPayerName", EOBPaymentInsurance.PayerName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Unchecked
                        oParameters.Add("@nPayerID", EOBPaymentInsurance.PayerID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nPayerType", EOBPaymentInsurance.PayerType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nPaymentMode", EOBPaymentInsurance.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked

                        oParameters.Add("@sCheckNumber", EOBPaymentInsurance.CheckNumber, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Unchecked
                        oParameters.Add("@nCheckAmount", EOBPaymentInsurance.CheckAmount, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nCheckDate", EOBPaymentInsurance.CheckDate, ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nMSTAccountID", EOBPaymentInsurance.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nMSTAccountType", EOBPaymentInsurance.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked

                        oParameters.Add("@nPaymentTrayID", EOBPaymentInsurance.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sPaymentTrayCode", EOBPaymentInsurance.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                        oParameters.Add("@sPaymentTrayDescription", EOBPaymentInsurance.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255

                        oParameters.Add("@nCloseDate", EOBPaymentInsurance.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sCardType", EOBPaymentInsurance.CardType, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sCardSecurityNo", EOBPaymentInsurance.CardSecurityNo, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@nCardID", EOBPaymentInsurance.CardID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked

                        oParameters.Add("@sAuthorizationNo", EOBPaymentInsurance.AuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                        oParameters.Add("@nCardExpDate", EOBPaymentInsurance.CardExpiryDate, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),

                        oParameters.Add("@nUserID", EOBPaymentInsurance.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sUserName", EOBPaymentInsurance.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtCreatedDateTime", EOBPaymentInsurance.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtModifiedDateTime", EOBPaymentInsurance.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked

                        oParameters.Add("@nClinicID", EOBPaymentInsurance.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                        //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                        oParameters.Add("@nPAccountID", EOBPaymentInsurance.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nAccountPatientID", EOBPaymentInsurance.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nGuarantorID", EOBPaymentInsurance.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                        //End

                        //oDB.Connect(false);
                        //oDB.Execute("BL_INUP_EOBPayment_MST", oParameters, out _retVal);
                        //oDB.Disconnect();

                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandText = "BL_INUP_EOBPayment_MST";
                         
                        _result = _sqlCommand.ExecuteNonQuery();

                        if (_sqlCommand.Parameters["@nEOBPaymentID"].Value != null)
                        { _retVal = _sqlCommand.Parameters["@nEOBPaymentID"].Value; }
                        else
                        { _retVal = 0; }

                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                        { _EOBPayId = Convert.ToInt64(_retVal); }

                        if (IsSaveForVoid == true) { _EOBVoidPayId = Convert.ToInt64(_retVal); }
                        _sqlCommand.Parameters.Clear();
                        _sqlCommand.Dispose();
                        _sqlCommand = null;
                        #region "Master Payment Note"

                        if (EOBPaymentInsurance.Notes != null && EOBPaymentInsurance.Notes.Count > 0)
                        {
                           // Object _RcValue = null;

                            for (int rcInd = 0; rcInd < EOBPaymentInsurance.Notes.Count; rcInd++)
                            {
                                //_RcValue = null;
                                oParameters.Clear();

                                oParameters.Add("@nID", EOBPaymentInsurance.Notes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                oParameters.Add("@nClaimNo", EOBPaymentInsurance.Notes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBID", EOBPaymentInsurance.Notes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentDetailID", EOBPaymentInsurance.Notes[rcInd].EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nBillingTransactionID", EOBPaymentInsurance.Notes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                oParameters.Add("@nBillingTransactionDetailID", EOBPaymentInsurance.Notes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@sNoteCode", EOBPaymentInsurance.Notes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                oParameters.Add("@sNoteDescription", EOBPaymentInsurance.Notes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                oParameters.Add("@dNoteAmount", EOBPaymentInsurance.Notes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                oParameters.Add("@nPaymentNoteType", EOBPaymentInsurance.Notes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                oParameters.Add("@nPaymentNoteSubType", EOBPaymentInsurance.Notes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                oParameters.Add("@nIncludeNoteOnPrint", EOBPaymentInsurance.Notes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
                                oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                //oDB.Connect(false);
                                //oDB.Execute("BL_INUP_EOBNotes", oParameters, out _RcValue);
                                //oDB.Disconnect();

                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "BL_INUP_EOBNotes";

                                _result = _sqlCommand.ExecuteNonQuery();

                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                else
                                { _retVal = 0; }
                                _sqlCommand.Parameters.Clear();
                                _sqlCommand.Dispose();
                                _sqlCommand = null;
                            }
                        }


                        #endregion

                        #endregion " Master Data Save "

                        if (EOBPaymentInsurance.EOBInsuranceReserveDetails != null && EOBPaymentInsurance.EOBInsuranceReserveDetails.Count > 0)
                        {

                            #region "Reserve Amount Entry, but it is in same table "

                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentInsurance.EOBInsuranceReserveDetails.Count; clmInsPayLnIndex++)
                            {
                                if (EOBPaymentInsurance.EOBInsuranceReserveDetails[clmInsPayLnIndex] != null)
                                {
                                    _EOBPayDtlId = 0;
                                    EOBInsPayResDtl = EOBPaymentInsurance.EOBInsuranceReserveDetails[clmInsPayLnIndex];
                                    //EOBInsPayDtl.EOBPaymentDetailID = 0;
                                    oParameters.Clear();
                                    //if (EOBInsPayResDtl.PaymentType == EOBPaymentType.InsuraceReserverd && EOBInsPayResDtl.PaymentSubType == EOBPaymentSubType.Reserved && EOBInsPayResDtl.PaySign == EOBPaymentSign.Receipt_Debit)
                                    //{
                                    //    oParameters.Add("@nEOBPaymentID", EOBInsPayResDtl.EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //}
                                    //else
                                    //{
                                    //    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //}
                                    //oParameters.Add("@nEOBID", EOBInsPayResDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nEOBDtlID", EOBInsPayResDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nEOBPaymentDetailID", EOBInsPayResDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nBillingTransactionID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nBillingTransactionDetailID", EOBInsPayResDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nBillingTransactionLineNo", EOBInsPayResDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nPatientID", EOBInsPayResDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nDOSFrom", EOBInsPayResDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                    //oParameters.Add("@nDOSTo", EOBInsPayResDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                    //oParameters.Add("@sCPTCode", EOBInsPayResDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    //oParameters.Add("@sCPTDescription", EOBInsPayResDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    //oParameters.Add("@nAmount", EOBInsPayResDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    //oParameters.Add("@nPaymentType", EOBInsPayResDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    //oParameters.Add("@nPaymentSubType", EOBInsPayResDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    //oParameters.Add("@nPaySign", EOBInsPayResDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    //oParameters.Add("@nPayMode", EOBInsPayResDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    //oParameters.Add("@nRefEOBPaymentID", EOBInsPayResDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayResDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nAccountID", EOBInsPayResDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nAccountType", EOBInsPayResDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    //oParameters.Add("@nMSTAccountID", EOBInsPayResDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nMSTAccountType", EOBInsPayResDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    //oParameters.Add("@nPaymentTrayID", EOBInsPayResDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@sPaymentTrayCode", EOBInsPayResDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    //oParameters.Add("@sPaymentTrayDescription", EOBInsPayResDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    //oParameters.Add("@nUserID", EOBInsPayResDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@sUserName", EOBInsPayResDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    //oParameters.Add("@dtCreatedDateTime", EOBInsPayResDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    //oParameters.Add("@dtModifiedDateTime", EOBInsPayResDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    //oParameters.Add("@nClinicID", EOBInsPayResDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nResEOBPaymentID", EOBInsPayResDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                    //oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayResDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                    //oParameters.Add("@nContactInsID", EOBInsPayResDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    //oParameters.Add("@nCreditLineID", EOBInsPayResDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    //oParameters.Add("@nEOBVoidPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    //oParameters.Add("@nCloseDate", EOBInsPayResDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    //oParameters.Add("@nOldRefEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    //oParameters.Add("@nOldRefEOBPaymentDetailID", EOBInsPayResDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    if (EOBInsPayResDtl.PaymentType == EOBPaymentType.InsuraceReserverd && EOBInsPayResDtl.PaymentSubType == EOBPaymentSubType.Reserved && EOBInsPayResDtl.PaySign == EOBPaymentSign.Receipt_Debit)
                                    {
                                        oParameters.Add("@nEOBPaymentID", EOBInsPayResDtl.EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    }
                                    else
                                    {
                                        oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    }
                                    oParameters.Add("@nEOBID", EOBInsPayResDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBDtlID", EOBInsPayResDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBPaymentDetailID", EOBInsPayResDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)

                                    oParameters.Add("@nBillingTransactionID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionDetailID", EOBInsPayResDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionLineNo", EOBInsPayResDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    oParameters.Add("@sSubClaimNo", EOBInsPayResDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);//	int
                                    oParameters.Add("@nTrackTrnID", 0, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                    oParameters.Add("@nTrackTrnDtlID", EOBInsPayResDtl.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                    oParameters.Add("@nTrackTrnLineNo", EOBInsPayResDtl.TrackBillingTransactionLineNo, ParameterDirection.Input, SqlDbType.Int);// numeric(18,0)


                                    oParameters.Add("@nPatientID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nDOSFrom", EOBInsPayResDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nDOSTo", EOBInsPayResDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@sCPTCode", EOBInsPayResDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sCPTDescription", EOBInsPayResDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@nAmount", EOBInsPayResDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    oParameters.Add("@nPaymentType", EOBInsPayResDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentSubType", EOBInsPayResDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaySign", EOBInsPayResDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPayMode", EOBInsPayResDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nAccountID", EOBInsPayResDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nAccountType", EOBInsPayResDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nMSTAccountID", EOBInsPayResDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nMSTAccountType", EOBInsPayResDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentTrayID", EOBInsPayResDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sPaymentTrayCode", EOBInsPayResDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sPaymentTrayDescription", EOBInsPayResDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@nUserID", EOBInsPayResDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sUserName", EOBInsPayResDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@dtCreatedDateTime", EOBInsPayResDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@dtModifiedDateTime", EOBInsPayResDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@nClinicID", EOBInsPayResDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    oParameters.Add("@nRefEOBPaymentID", EOBInsPayResDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayResDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //..ResEOBPaymentID,ResEOBPaymentDetailID has the reference id's for the reserve amount
                                    oParameters.Add("@nResEOBPaymentID", EOBInsPayResDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayResDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);

                                    oParameters.Add("@nContactInsID", EOBInsPayResDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nCreditLineID", EOBInsPayResDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nEOBVoidPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    oParameters.Add("@nCloseDate", EOBInsPayResDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    oParameters.Add("@nOldRefEOBPaymentID", EOBInsPayResDtl.OldRefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nOldRefEOBPaymentDetailID", EOBInsPayResDtl.OldRefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nOldResEOBPaymentID", EOBInsPayResDtl.OldReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nOldResEOBPaymentDetailID", EOBInsPayResDtl.OldReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0)

                                    //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                                    oParameters.Add("@nPAccountID", EOBInsPayResDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nAccountPatientID", EOBInsPayResDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nGuarantorID", EOBInsPayResDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                    //End

                                    _retVal = null;

                                    //oDB.Connect(false);
                                    //oDB.Execute("BL_INUP_EOBPayment_DTL", oParameters, out _retVal);
                                    //oDB.Disconnect();
                                     
                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                    _sqlCommand.Connection = _sqlConnection;
                                    _sqlCommand.Transaction = _sqlTransaction;
                                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                                    _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL_Tracking";

                                    _result = _sqlCommand.ExecuteNonQuery();

                                    if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                    { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                    else
                                    { _retVal = 0; }

                                    if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                                    {
                                        _EOBPayDtlId = Convert.ToInt64(_retVal.ToString());
                                    }

                                    //#region Patient Reserve Association

                                    //oParameters.Clear();
                                    //oParameters.Add("@nEOBPaymentID", EOBInsPayResDtl.EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nEOBPaymentDetailID", _EOBPayDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nTransactionID", EOBInsPayResDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nTrackTrnID", EOBInsPayResDtl.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //oParameters.Add("@nPatientID ", EOBInsPayResDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    //_sqlCommand = new System.Data.SqlClient.SqlCommand();
                                    //_sqlCommand = GetCmdParameters(oParameters);
                                    //_sqlCommand.Connection = _sqlConnection;
                                    //_sqlCommand.Transaction = _sqlTransaction;
                                    //_sqlCommand.CommandType = CommandType.StoredProcedure;
                                    //_sqlCommand.CommandText = "BL_INUP_Reserve_Association";

                                    //_result = _sqlCommand.ExecuteNonQuery();

                                    //#endregion Patient Reserve Association

                                    //#region Patient Reserve Association
                                    _sqlCommand.Parameters.Clear();
                                    _sqlCommand.Dispose();
                                    _sqlCommand = null;
                                    oParameters.Clear();

                                    oParameters.Add("@nID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                    oParameters.Add("@nClaimNo", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                    oParameters.Add("@nEOBPaymentID", EOBInsPayResDtl.EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                    oParameters.Add("@nEOBID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                    oParameters.Add("@nEOBPaymentDetailID", _EOBPayDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                    oParameters.Add("@nBillingTransactionID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                    oParameters.Add("@nBillingTransactionDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                    oParameters.Add("@sNoteCode", "", ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                    oParameters.Add("@sNoteDescription", EOBPaymentInsurance.sClaimVoidNote, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                    oParameters.Add("@dNoteAmount", EOBInsPayResDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                    oParameters.Add("@nPaymentNoteType", EOBPaymentType.InsuraceReserverd.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                    oParameters.Add("@nPaymentNoteSubType", EOBPaymentSubType.Reserved.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                    oParameters.Add("@nIncludeNoteOnPrint", false, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
                                    oParameters.Add("@nClinicID", EOBInsPayResDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                    _sqlCommand.Connection = _sqlConnection;
                                    _sqlCommand.Transaction = _sqlTransaction;
                                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                                    _sqlCommand.CommandText = "BL_INUP_EOBNotes";

                                    _result = _sqlCommand.ExecuteNonQuery();
                                    _sqlCommand.Parameters.Clear();
                                    _sqlCommand.Dispose();
                                    _sqlCommand = null;
                                    //#endregion Patient Reserve Association

                                }
                            }
                            #endregion
                        }

                        #region " EOB Data Save "

                        if (EOBPaymentInsurance.InsuranceClaims != null && EOBPaymentInsurance.InsuranceClaims.Count > 0)
                        {

                            #region "Payment Line Master (Credit) Entry, Total Check Amount Entry, but it is in same table "
                            if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails != null && EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count > 0)
                            {
                                for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count; clmInsPayLnIndex++)
                                {
                                    if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex] != null)
                                    {
                                        _EOBPayDtlId = 0;
                                        EOBInsPayDtl = EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex];
                                        //EOBInsPayDtl.EOBPaymentDetailID = 0;
                                        oParameters.Clear();
                                        oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@nEOBID", EOBInsPayDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@nEOBDtlID", EOBInsPayDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@nEOBPaymentDetailID", EOBInsPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@nBillingTransactionLineNo", EOBInsPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@nPatientID", EOBInsPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@nDOSFrom", EOBInsPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                        oParameters.Add("@nDOSTo", EOBInsPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                        oParameters.Add("@sCPTCode", EOBInsPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                        oParameters.Add("@sCPTDescription", EOBInsPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                        oParameters.Add("@nAmount", EOBInsPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        oParameters.Add("@nPaymentType", EOBInsPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                        oParameters.Add("@nPaymentSubType", EOBInsPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                        oParameters.Add("@nPaySign", EOBInsPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                        oParameters.Add("@nPayMode", EOBInsPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                        oParameters.Add("@nRefEOBPaymentID", EOBInsPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@nAccountID", EOBInsPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@nAccountType", EOBInsPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                        oParameters.Add("@nMSTAccountID", EOBInsPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@nMSTAccountType", EOBInsPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                        oParameters.Add("@nPaymentTrayID", EOBInsPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@sPaymentTrayCode", EOBInsPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                        oParameters.Add("@sPaymentTrayDescription", EOBInsPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                        oParameters.Add("@nUserID", EOBInsPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@sUserName", EOBInsPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                        oParameters.Add("@dtCreatedDateTime", EOBInsPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                        oParameters.Add("@dtModifiedDateTime", EOBInsPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                        oParameters.Add("@nClinicID", EOBInsPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@nResEOBPaymentID", EOBInsPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                        oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                        oParameters.Add("@nContactInsID", EOBInsPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                        oParameters.Add("@nCreditLineID", EOBInsPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                        oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                        oParameters.Add("@nCloseDate", EOBInsPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                        //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                                        oParameters.Add("@nPAccountID", EOBInsPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                        oParameters.Add("@nAccountPatientID", EOBInsPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                        oParameters.Add("@nGuarantorID", EOBInsPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                        //End
                                        _retVal = null;

                                        //oDB.Connect(false);
                                        //oDB.Execute("BL_INUP_EOBPayment_DTL", oParameters, out _retVal);
                                        //oDB.Disconnect();

                                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                                        _sqlCommand.Connection = _sqlConnection;
                                        _sqlCommand.Transaction = _sqlTransaction;
                                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                                        _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL";

                                        _result = _sqlCommand.ExecuteNonQuery();

                                        if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                        { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                        else
                                        { _retVal = 0; }

                                        if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                                        {
                                            _EOBPayDtlId = Convert.ToInt64(_retVal.ToString());
                                        }

                                        _sqlCommand.Parameters.Clear();
                                        _sqlCommand.Dispose();
                                        _sqlCommand = null;
                                        EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentID = _EOBPayId;
                                        EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentDetailID = _EOBPayDtlId;

                                        #region "Assign Credit Line Reference and Finance Line Reference to debit line wherever applicable"
                                        if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].IsMainCreditLine == true)
                                        {
                                            //All Remaining Credit Lines
                                            for (int nAsgn = 0; nAsgn <= EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count - 1; nAsgn++)
                                            {
                                                if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[nAsgn].IsMainCreditLine == false)
                                                {
                                                    EOBPaymentInsurance.EOBInsurancePaymentLineDetails[nAsgn].MainCreditLineID = _EOBPayDtlId;
                                                }
                                            }
                                            //All Debit Lines
                                            for (int nAsgnClaim = 0; nAsgnClaim <= EOBPaymentInsurance.InsuranceClaims.Count - 1; nAsgnClaim++)
                                            {
                                                for (int nAsgnClmLine = 0; nAsgnClmLine <= EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines.Count - 1; nAsgnClmLine++)
                                                {
                                                    for (int nAsgn = 0; nAsgn <= EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails.Count - 1; nAsgn++)
                                                    {
                                                        if (EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].IsMainCreditLine == false)
                                                        {
                                                            EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].MainCreditLineID = _EOBPayDtlId;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //Assign Reference Number to debit lines
                                        for (int nAsgnClaim = 0; nAsgnClaim <= EOBPaymentInsurance.InsuranceClaims.Count - 1; nAsgnClaim++)
                                        {
                                            for (int nAsgnClmLine = 0; nAsgnClmLine <= EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines.Count - 1; nAsgnClmLine++)
                                            {
                                                for (int nAsgn = 0; nAsgn <= EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails.Count - 1; nAsgn++)
                                                {
                                                    if (EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].RefFinanceLieNo == EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].FinanceLieNo)
                                                    {
                                                        if (EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].UseRefFinanceLieNo == true)
                                                        {
                                                            EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].RefEOBPaymentID = _EOBPayId;
                                                            EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].RefEOBPaymentDetailID = _EOBPayDtlId;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        #endregion

                                        #region " Add Line Notes "

                                        if (EOBInsPayDtl.LineNotes != null && EOBInsPayDtl.LineNotes.Count > 0)
                                        {
                                            Object _RcValue = null;

                                            for (int rcInd = 0; rcInd < EOBInsPayDtl.LineNotes.Count; rcInd++)
                                            {
                                                _RcValue = null;
                                                oParameters.Clear();

                                                oParameters.Add("@nID", EOBInsPayDtl.LineNotes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                                oParameters.Add("@nClaimNo", EOBInsPayDtl.LineNotes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBID", EOBInsPayDtl.LineNotes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentDetailID", _EOBPayDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.LineNotes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.LineNotes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sNoteCode", EOBInsPayDtl.LineNotes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                oParameters.Add("@sNoteDescription", EOBInsPayDtl.LineNotes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                oParameters.Add("@dNoteAmount", EOBInsPayDtl.LineNotes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                oParameters.Add("@nPaymentNoteType", EOBInsPayDtl.LineNotes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                                oParameters.Add("@nPaymentNoteSubType", EOBInsPayDtl.LineNotes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                                oParameters.Add("@nIncludeNoteOnPrint", EOBInsPayDtl.LineNotes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
                                                oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                                //oDB.Connect(false);
                                                //oDB.Execute("BL_INUP_EOBNotes", oParameters, out _RcValue);
                                                //oDB.Disconnect();

                                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                _sqlCommand.Connection = _sqlConnection;
                                                _sqlCommand.Transaction = _sqlTransaction;
                                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                _sqlCommand.CommandText = "BL_INUP_EOBNotes";

                                                _result = _sqlCommand.ExecuteNonQuery();

                                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                                { _RcValue = _sqlCommand.Parameters["@nID"].Value; }
                                                else
                                                { _RcValue = 0; }
                                                _sqlCommand.Parameters.Clear();
                                                _sqlCommand.Dispose();
                                                _sqlCommand = null;
                                            }
                                        }

                                        #endregion " Add Line Notes "

                                        EOBInsPayDtl = null;
                                    }
                                }
                            }
                            #endregion

                            #region "Payment EOB and Finance DEbit Line Entries"

                            for (int clmIndex = 0; clmIndex < EOBPaymentInsurance.InsuranceClaims.Count; clmIndex++)
                            {
                                if (_EOBPayId > 0 && EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines != null && EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines.Count > 0)
                                {
                                    for (int clmLnIndex = 0; clmLnIndex < EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines.Count; clmLnIndex++)
                                    {

                                        if (EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex] != null)
                                        {
                                            _EOBDtlId = 0;
                                            PaymentInsClaimLine = EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex];

                                            //nEOBPaymentID,nEOBID,nEOBPaymentDetailID,nBillingTransactionID,nBillingTransactionDetailID
                                            //nBillingTransactionLineNo,nPatientID,nDOSFrom,nDOSTo,sCPTCode,sCPTDescription,nAmount,
                                            //nPaymentType,nPaymentSubType,nPaySign,nRefEOBPaymentID,nRefEOBPaymentDetailID,nAccountID
                                            //nAccountType,nMSTAccountID,nMSTAccountType,nPaymentTrayID,nPaymentTrayCode,nPaymentTrayDescription
                                            //nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                                            oParameters.Clear();

                                            #region "EOB Service Line"
                                            if (_UseExtEOBID == true) { PaymentInsClaimLine.mEOBID = _EOBId; }
                                            oParameters.Add("@nEOBID", PaymentInsClaimLine.mEOBID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                            oParameters.Add("@nEOBDtlID", PaymentInsClaimLine.mEOBDtlID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0)
                                            //oParameters.Add("@nClaimNo", PaymentInsClaimLine.ClaimNumber, ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nClaimNo", PaymentInsClaimLine.ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                            oParameters.Add("@nDOSFrom", PaymentInsClaimLine.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nDOSTo", PaymentInsClaimLine.DOSTo, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                            oParameters.Add("@sCPTCode", PaymentInsClaimLine.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                            oParameters.Add("@sCPTDescription", PaymentInsClaimLine.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                            if (PaymentInsClaimLine.IsNullCharges == false)
                                            {
                                                oParameters.Add("@dCharges", PaymentInsClaimLine.Charges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }
                                            else
                                            {
                                                oParameters.Add("@dCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }

                                            if (PaymentInsClaimLine.IsNullUnit == false)
                                            {
                                                oParameters.Add("@dUnit", PaymentInsClaimLine.Unit, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                            }
                                            else
                                            {
                                                oParameters.Add("@dUnit", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                            }

                                            if (PaymentInsClaimLine.IsNullTotalCharges == false)
                                            {
                                                oParameters.Add("@dTotalCharges", PaymentInsClaimLine.TotalCharges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }
                                            else
                                            {
                                                oParameters.Add("@dTotalCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }

                                            if (PaymentInsClaimLine.IsNullAllowed == false)
                                            {
                                                oParameters.Add("@dAllowed", PaymentInsClaimLine.Allowed, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }
                                            else
                                            {
                                                oParameters.Add("@dAllowed", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }

                                            if (PaymentInsClaimLine.IsNullWriteOff == false)
                                            {
                                                oParameters.Add("@dWriteOff", PaymentInsClaimLine.WriteOff, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }
                                            else
                                            {
                                                oParameters.Add("@dWriteOff", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }

                                            if (PaymentInsClaimLine.IsNullNonCovered == false)
                                            {
                                                oParameters.Add("@dNotCovered", PaymentInsClaimLine.NonCovered, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }
                                            else
                                            {
                                                oParameters.Add("@dNotCovered", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }

                                            if (PaymentInsClaimLine.IsNullInsurance == false)
                                            {
                                                oParameters.Add("@dPayment", PaymentInsClaimLine.InsuranceAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }
                                            else
                                            {
                                                oParameters.Add("@dPayment", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }

                                            if (PaymentInsClaimLine.IsNullCopay == false)
                                            {
                                                oParameters.Add("@dCopay", PaymentInsClaimLine.Copay, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }
                                            else
                                            {
                                                oParameters.Add("@dCopay", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }

                                            if (PaymentInsClaimLine.IsNullDeductible == false)
                                            {
                                                oParameters.Add("@dDeductible", PaymentInsClaimLine.Deductible, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }
                                            else
                                            {
                                                oParameters.Add("@dDeductible", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }

                                            if (PaymentInsClaimLine.IsNullCoInsurance == false)
                                            {
                                                oParameters.Add("@dCoInsurance", PaymentInsClaimLine.CoInsurance, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                            }
                                            else
                                            {
                                                oParameters.Add("@dCoInsurance", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                            }

                                            if (PaymentInsClaimLine.IsNullWithhold == false)
                                            {
                                                oParameters.Add("@dWithhold", PaymentInsClaimLine.Withhold, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }
                                            else
                                            {
                                                oParameters.Add("@dWithhold", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            }

                                            oParameters.Add("@nPaymentTrayID", PaymentInsClaimLine.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                            oParameters.Add("@sPaymentTrayCode", PaymentInsClaimLine.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                                            oParameters.Add("@sPaymentTrayDescription", PaymentInsClaimLine.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked

                                            oParameters.Add("@nUserID", PaymentInsClaimLine.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                            oParameters.Add("@sUserName", PaymentInsClaimLine.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked
                                            oParameters.Add("@dtCreatedDateTime", PaymentInsClaimLine.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked
                                            oParameters.Add("@dtModifiedDateTime", PaymentInsClaimLine.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked

                                            oParameters.Add("@nPatientID", PaymentInsClaimLine.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                            oParameters.Add("@nInsuraceID", PaymentInsClaimLine.PatInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                            oParameters.Add("@nContactID", PaymentInsClaimLine.InsContactID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked

                                            oParameters.Add("@nClinicID", PaymentInsClaimLine.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@bUseExtEOBID", _UseExtEOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                            oParameters.Add("@nEOBType", PaymentInsClaimLine.EOBType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);// int,
                                            oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.BLTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                            oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.BLTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                            oParameters.Add("@nBillingTransactionLineNo", PaymentInsClaimLine.BLTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)

                                            oParameters.Add("@nInsuranceCompanyID", PaymentInsClaimLine.InsCompanyID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
                                            oParameters.Add("@nCloseDate", PaymentInsClaimLine.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)

                                            if (_UseExtEOBID == false) { _UseExtEOBID = true; }
                                            _retVal = null;
                                            _valRet = null;

                                            _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                            _sqlCommand = oDB.GetCmdParameters(oParameters);
                                            _sqlCommand.Connection = _sqlConnection;
                                            _sqlCommand.Transaction = _sqlTransaction;
                                            _sqlCommand.CommandType = CommandType.StoredProcedure;
                                            _sqlCommand.CommandText = "BL_INSERT_EOBPayment_EOB";

                                            _result = _sqlCommand.ExecuteNonQuery();

                                            if (_sqlCommand.Parameters["@nEOBID"].Value != null)
                                            { _retVal = _sqlCommand.Parameters["@nEOBID"].Value; }
                                            else
                                            { _retVal = 0; }

                                            if (_sqlCommand.Parameters["@nEOBDtlID"].Value != null)
                                            { _valRet = _sqlCommand.Parameters["@nEOBDtlID"].Value; }
                                            else
                                            { _valRet = 0; }

                                            if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                            { _EOBId = Convert.ToInt64(_retVal); }

                                            if (_valRet != null && Convert.ToString(_valRet).Trim() != "")
                                            { _EOBDtlId = Convert.ToInt64(_valRet); }
                                            _sqlCommand.Parameters.Clear();
                                            _sqlCommand.Dispose();
                                            _sqlCommand = null;

                                            #region " Add Line NextAction  "

                                            if (PaymentInsClaimLine.LineNextAction != null && PaymentInsClaimLine.LineNextAction.HasData == true && PaymentInsClaimLine.LineNextAction.HasActionData)
                                            {
                                               // Object _nextActRetVal = null;

                                                oParameters.Clear();

                                                oParameters.Add("@nID", PaymentInsClaimLine.LineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                                oParameters.Add("@nClaimNo", PaymentInsClaimLine.LineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentDetailID", PaymentInsClaimLine.LineNextAction.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.LineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.LineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                                oParameters.Add("@nNextActionPatientInsID", PaymentInsClaimLine.LineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                                                oParameters.Add("@nNextActionPatientInsName", PaymentInsClaimLine.LineNextAction.NextActionPatientInsName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                oParameters.Add("@nNextActionPartyNumber", PaymentInsClaimLine.LineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                                                oParameters.Add("@nNextPartyType", PaymentInsClaimLine.LineNextAction.NextPartyType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                                                oParameters.Add("@nNextActionContactID", PaymentInsClaimLine.LineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sNextActionCode", PaymentInsClaimLine.LineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                oParameters.Add("@sNextActionDescription", PaymentInsClaimLine.LineNextAction.NextActionDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                if (PaymentInsClaimLine.LineNextAction.IsNullNextActionAmount == false)
                                                {
                                                    oParameters.Add("@dNextActionAmount", PaymentInsClaimLine.LineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dNextActionAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                }

                                                oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                                // ----------------------------------------
                                                // Parameters added - Pankaj bedse 29012010
                                                oParameters.Add("@nCloseDate", PaymentInsClaimLine.LineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nUserID", PaymentInsClaimLine.LineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sUserName", PaymentInsClaimLine.LineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),
                                                // ----------------------------------------

                                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                _sqlCommand.Connection = _sqlConnection;
                                                _sqlCommand.Transaction = _sqlTransaction;
                                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                _sqlCommand.CommandText = "BL_UP_EOBNextAction";

                                                _result = _sqlCommand.ExecuteNonQuery();

                                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                                { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                                else
                                                { _retVal = 0; }
                                                _sqlCommand.Parameters.Clear();
                                                _sqlCommand.Dispose();
                                                _sqlCommand = null;
                                            }

                                            #endregion " Add Line NextAction "

                                            #region " Add Line Next Party "

                                            if (PaymentInsClaimLine.LineNextAction != null && PaymentInsClaimLine.LineNextAction.HasData == true && PaymentInsClaimLine.LineNextAction.HasActionData)
                                            {
                                                //Object _nextActRetVal = null;

                                                oParameters.Clear();

                                                oParameters.Add("@nID", PaymentInsClaimLine.LineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                                oParameters.Add("@nClaimNo", PaymentInsClaimLine.LineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentDetailID", PaymentInsClaimLine.LineNextAction.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.LineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.LineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                                oParameters.Add("@nNextActionPatientInsID", PaymentInsClaimLine.LineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                                                oParameters.Add("@nNextActionPatientInsName", PaymentInsClaimLine.LineNextAction.NextActionPatientInsName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                oParameters.Add("@nNextActionPartyNumber", PaymentInsClaimLine.LineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                                                oParameters.Add("@nNextPartyType", PaymentInsClaimLine.LineNextAction.NextPartyType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                                                oParameters.Add("@nNextActionContactID", PaymentInsClaimLine.LineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sNextActionCode", PaymentInsClaimLine.LineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                oParameters.Add("@sNextActionDescription", PaymentInsClaimLine.LineNextAction.NextActionDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                if (PaymentInsClaimLine.LineNextAction.IsNullNextActionAmount == false)
                                                {
                                                    oParameters.Add("@dNextActionAmount", PaymentInsClaimLine.LineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dNextActionAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                }

                                                oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                                // ----------------------------------------
                                                // Parameters added - Pankaj bedse 29012010
                                                oParameters.Add("@nCloseDate", PaymentInsClaimLine.LineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nUserID", PaymentInsClaimLine.LineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sUserName", PaymentInsClaimLine.LineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),
                                                // ----------------------------------------



                                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                _sqlCommand.Connection = _sqlConnection;
                                                _sqlCommand.Transaction = _sqlTransaction;
                                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                _sqlCommand.CommandText = "BL_UP_EOBParty";

                                                _result = _sqlCommand.ExecuteNonQuery();

                                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                                { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                                else
                                                { _retVal = 0; }
                                                _sqlCommand.Parameters.Clear();
                                                _sqlCommand.Dispose();
                                                _sqlCommand = null;
                                            }

                                            #endregion " Add Line Next Party "

                                            #region " Add Line Reason Codes "

                                            if (PaymentInsClaimLine.LineResonCodes != null && PaymentInsClaimLine.LineResonCodes.Count > 0)
                                            {
                                               // Object _RcValue = null;

                                                for (int rcInd = 0; rcInd < PaymentInsClaimLine.LineResonCodes.Count; rcInd++)
                                                {
                                                   // _RcValue = null;
                                                    oParameters.Clear();

                                                    oParameters.Add("@nID", PaymentInsClaimLine.LineResonCodes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                                    oParameters.Add("@nClaimNo", PaymentInsClaimLine.LineResonCodes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nEOBPaymentDetailID", PaymentInsClaimLine.LineResonCodes[rcInd].EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.LineResonCodes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                    oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.LineResonCodes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@sReasonCode", PaymentInsClaimLine.LineResonCodes[rcInd].ReasonCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                    oParameters.Add("@sReasonDescription", PaymentInsClaimLine.LineResonCodes[rcInd].ReasonDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                    if (PaymentInsClaimLine.LineResonCodes[rcInd].IsNullReasonAmount == false)
                                                    {
                                                        oParameters.Add("@dReasonAmount", PaymentInsClaimLine.LineResonCodes[rcInd].ReasonAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                    }
                                                    else
                                                    {
                                                        oParameters.Add("@dReasonAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                    }

                                                    oParameters.Add("@nType", EOBCommentType.Reason.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                                    oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                                    //oDB.Connect(false);
                                                    //oDB.Execute("BL_INUP_EOBReasonCode", oParameters, out _RcValue);
                                                    //oDB.Disconnect();

                                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                    _sqlCommand.Connection = _sqlConnection;
                                                    _sqlCommand.Transaction = _sqlTransaction;
                                                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                    _sqlCommand.CommandText = "BL_INUP_EOBReasonCode";

                                                    _result = _sqlCommand.ExecuteNonQuery();

                                                    if (_sqlCommand.Parameters["@nID"].Value != null)
                                                    { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                                    else
                                                    { _retVal = 0; }
                                                    _sqlCommand.Parameters.Clear();
                                                    _sqlCommand.Dispose();
                                                    _sqlCommand = null;
                                                }
                                            }

                                            #endregion " Add Line Reason Codes "


                                            #endregion

                                            #region " EOB Financial Service Line Save "

                                            if (_EOBPayId > 0 && PaymentInsClaimLine.EOBInsurancePaymentLineDetails != null && PaymentInsClaimLine.EOBInsurancePaymentLineDetails.Count > 0)
                                            {
                                                for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < PaymentInsClaimLine.EOBInsurancePaymentLineDetails.Count; clmInsPayLnIndex++)
                                                {
                                                    if (PaymentInsClaimLine.EOBInsurancePaymentLineDetails[clmInsPayLnIndex] != null)
                                                    {
                                                        _EOBPayDtlId = 0;
                                                        EOBInsPayDtl = PaymentInsClaimLine.EOBInsurancePaymentLineDetails[clmInsPayLnIndex];

                                                        oParameters.Clear();
                                                        oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nEOBDtlID", _EOBDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nEOBPaymentDetailID", EOBInsPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nBillingTransactionLineNo", EOBInsPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nPatientID", EOBInsPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nDOSFrom", EOBInsPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nDOSTo", EOBInsPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@sCPTCode", EOBInsPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                        oParameters.Add("@sCPTDescription", EOBInsPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                        oParameters.Add("@nAmount", EOBInsPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                        oParameters.Add("@nPaymentType", EOBInsPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPaymentSubType", EOBInsPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPaySign", EOBInsPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPayMode", EOBInsPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nRefEOBPaymentID", EOBInsPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nAccountID", EOBInsPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nAccountType", EOBInsPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nMSTAccountID", EOBInsPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nMSTAccountType", EOBInsPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPaymentTrayID", EOBInsPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@sPaymentTrayCode", EOBInsPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                        oParameters.Add("@sPaymentTrayDescription", EOBInsPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                        oParameters.Add("@nUserID", EOBInsPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@sUserName", EOBInsPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                        oParameters.Add("@dtCreatedDateTime", EOBInsPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                        oParameters.Add("@dtModifiedDateTime", EOBInsPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                        oParameters.Add("@nClinicID", EOBInsPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nResEOBPaymentID", EOBInsPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                        oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                        oParameters.Add("@nContactInsID", EOBInsPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                        oParameters.Add("@nCreditLineID", EOBInsPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                        oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                        oParameters.Add("@nCloseDate", EOBInsPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                        _retVal = null;

                                                        //oDB.Connect(false);
                                                        //oDB.Execute("BL_INUP_EOBPayment_DTL", oParameters, out _retVal);
                                                        //oDB.Disconnect();

                                                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                        _sqlCommand.Connection = _sqlConnection;
                                                        _sqlCommand.Transaction = _sqlTransaction;
                                                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                        _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL";

                                                        _result = _sqlCommand.ExecuteNonQuery();

                                                        if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                                        { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                                        else
                                                        { _retVal = 0; }

                                                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                                        { _EOBPayDtlId = Convert.ToInt64(_retVal); }


                                                        _sqlCommand.Parameters.Clear();
                                                        _sqlCommand.Dispose();
                                                        _sqlCommand = null;
                                                        EOBInsPayDtl = null;

                                                    }
                                                }
                                            }


                                            #endregion " EOB Financial Service Line Save "

                                            PaymentInsClaimLine = null;
                                        }
                                    }
                                }
                            }//20091020 (for)

                            #endregion

                        }//20091020 (If)

                        #endregion " EOB Data Save "

                        _sqlTransaction.Commit();
                        _sqlConnection.Close();

                        #region " Save last selected Close date "
                        if (IsSaveForVoid == false)
                        {
                            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseConnectionString);
                            oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(gloDateMaster.gloDate.DateAsDate(EOBPaymentInsurance.CloseDate)).ToString("MM/dd/yyyy"), _clinicId, EOBPaymentInsurance.UserID, gloSettings.SettingFlag.User);
                            oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", EOBPaymentInsurance.PaymentTrayID.ToString(), _clinicId, EOBPaymentInsurance.UserID, gloSettings.SettingFlag.User);
                            oSettings.Dispose();
                        }

                        #endregion " Save last selected Close date "
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                { _sqlTransaction.Rollback(); _sqlConnection.Close(); ex.ERROR_Log(ex.ToString()); }
                catch (Exception ex)
                { _sqlTransaction.Rollback(); _sqlConnection.Close(); gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
                finally
                {
                    //if (oDB != null) { oDB.Dispose(); }
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (_retVal != null) { _retVal = null; }

                    if (_sqlConnection != null) { _sqlConnection.Dispose(); _sqlConnection = null; }
                    if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); }
                    if (_sqlTransaction != null) { _sqlTransaction.Dispose(); _sqlTransaction = null; }

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }

                return _EOBPayId;
            }

            public Int64 SaveSplitEOB(EOBPayment.Common.PaymentInsurance EOBPaymentInsurance, bool IsSaveForVoid, out Int64 EOBId)
            {
                System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(_databaseConnectionString);
                System.Data.SqlClient.SqlCommand _sqlCommand = null;
                System.Data.SqlClient.SqlTransaction _sqlTransaction = null;
                EOBId = 0;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);

                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                object _retVal = null;
                object _valRet = null;
                Int64 _EOBPayId = 0;
                Int64 _EOBId = 0;
                Int64 _EOBDtlId = 0;
                Int64 _EOBPayDtlId = 0;
                Int64 _EOBVoidPayId = 0;

                bool _UseExtEOBID = false;
                EOBPayment.Common.PaymentInsuranceLine PaymentInsClaimLine = null;
                EOBPayment.Common.EOBInsurancePaymentDetail EOBInsPayDtl = null;
                int _result = 0;

                try
                {
                    if (EOBPaymentInsurance != null)
                    {
                        _sqlConnection.Open();
                        _sqlTransaction = _sqlConnection.BeginTransaction();

                        #region " Master Data Save "

                        //nEOBPaymentID,nEOBRefNO,sPayerName,nPayerID,nPayerType,nPaymentMode,sCheckNumber,nCheckAmount,nCheckDate
                        //nMSTAccountID,nMSTAccountType,nPaymentTrayID,sPaymentTrayName,nCloseDate,sCardType,sCardSecurityNo
                        //nCardID,nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                        _EOBPayId = 0;
                        oParameters.Clear();

                        oParameters.Add("@sPaymentNo", EOBPaymentInsurance.PaymentNumber, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sPaymentNoPrefix", EOBPaymentInsurance.PaymentNumberPefix, ParameterDirection.Input, SqlDbType.VarChar);

                        oParameters.Add("@nEOBPaymentID", EOBPaymentInsurance.EOBPaymentID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nEOBRefNO", EOBPaymentInsurance.EOBRefNO, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sPayerName", EOBPaymentInsurance.PayerName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Unchecked
                        oParameters.Add("@nPayerID", EOBPaymentInsurance.PayerID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nPayerType", EOBPaymentInsurance.PayerType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nPaymentMode", EOBPaymentInsurance.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked

                        oParameters.Add("@sCheckNumber", EOBPaymentInsurance.CheckNumber, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Unchecked
                        oParameters.Add("@nCheckAmount", EOBPaymentInsurance.CheckAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nCheckDate", EOBPaymentInsurance.CheckDate, ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nMSTAccountID", EOBPaymentInsurance.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nMSTAccountType", EOBPaymentInsurance.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked

                        oParameters.Add("@nPaymentTrayID", EOBPaymentInsurance.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sPaymentTrayCode", EOBPaymentInsurance.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                        oParameters.Add("@sPaymentTrayDescription", EOBPaymentInsurance.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255

                        oParameters.Add("@nCloseDate", EOBPaymentInsurance.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sCardType", EOBPaymentInsurance.CardType, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sCardSecurityNo", EOBPaymentInsurance.CardSecurityNo, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@nCardID", EOBPaymentInsurance.CardID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked

                        oParameters.Add("@sAuthorizationNo", EOBPaymentInsurance.AuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                        oParameters.Add("@nCardExpDate", EOBPaymentInsurance.CardExpiryDate, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),

                        oParameters.Add("@nUserID", EOBPaymentInsurance.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sUserName", EOBPaymentInsurance.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtCreatedDateTime", EOBPaymentInsurance.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtModifiedDateTime", EOBPaymentInsurance.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked

                        oParameters.Add("@nClinicID", EOBPaymentInsurance.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                        //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding the PAF values 
                        oParameters.Add("@nPAccountID", EOBPaymentInsurance.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nAccountPatientID", EOBPaymentInsurance.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nGuarantorID", EOBPaymentInsurance.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                        //End

                        //oDB.Connect(false);
                        //oDB.Execute("BL_INUP_EOBPayment_MST", oParameters, out _retVal);
                        //oDB.Disconnect();

                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandText = "BL_INUP_EOBPayment_MST";

                        _result = _sqlCommand.ExecuteNonQuery();

                        if (_sqlCommand.Parameters["@nEOBPaymentID"].Value != null)
                        { _retVal = _sqlCommand.Parameters["@nEOBPaymentID"].Value; }
                        else
                        { _retVal = 0; }

                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                        { _EOBPayId = Convert.ToInt64(_retVal); }

                        if (IsSaveForVoid == true) { _EOBVoidPayId = Convert.ToInt64(_retVal); }
                        _sqlCommand.Parameters.Clear();
                        _sqlCommand.Dispose();
                        _sqlCommand = null;
                        #region "Master Payment Note"

                        if (EOBPaymentInsurance.Notes != null && EOBPaymentInsurance.Notes.Count > 0)
                        {
                            //Object _RcValue = null;

                            for (int rcInd = 0; rcInd < EOBPaymentInsurance.Notes.Count; rcInd++)
                            {
                              //  _RcValue = null;
                                oParameters.Clear();

                                oParameters.Add("@nID", EOBPaymentInsurance.Notes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                oParameters.Add("@nClaimNo", EOBPaymentInsurance.Notes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBID", EOBPaymentInsurance.Notes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentDetailID", EOBPaymentInsurance.Notes[rcInd].EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                oParameters.Add("@nBillingTransactionID", EOBPaymentInsurance.Notes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                oParameters.Add("@nBillingTransactionDetailID", EOBPaymentInsurance.Notes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                oParameters.Add("@nTrackTrnID", EOBPaymentInsurance.Notes[rcInd].TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                oParameters.Add("@nTrackTrnDtlID", EOBPaymentInsurance.Notes[rcInd].TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nTrackTrnLineNo", EOBPaymentInsurance.Notes[rcInd].TrackBillingTransactionLineNo, ParameterDirection.Input, SqlDbType.Int);
                                oParameters.Add("@sSubClaimNo", EOBPaymentInsurance.Notes[rcInd].SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);

                                oParameters.Add("@sNoteCode", EOBPaymentInsurance.Notes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                oParameters.Add("@sNoteDescription", EOBPaymentInsurance.Notes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                oParameters.Add("@dNoteAmount", EOBPaymentInsurance.Notes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                oParameters.Add("@nPaymentNoteType", EOBPaymentInsurance.Notes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                oParameters.Add("@nPaymentNoteSubType", EOBPaymentInsurance.Notes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                oParameters.Add("@nIncludeNoteOnPrint", EOBPaymentInsurance.Notes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
                                oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 
                                oParameters.Add("@nCloseDate", EOBPaymentInsurance.Notes[rcInd].CloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nUserID", EOBPaymentInsurance.Notes[rcInd].UserId, ParameterDirection.Input, SqlDbType.BigInt);
                                //oDB.Connect(false);
                                //oDB.Execute("BL_INUP_EOBNotes", oParameters, out _RcValue);
                                //oDB.Disconnect();

                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "BL_INUP_EOBNotes_Tracking";

                                _result = _sqlCommand.ExecuteNonQuery();

                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                else
                                { _retVal = 0; }
                                _sqlCommand.Parameters.Clear();
                                _sqlCommand.Dispose();
                                _sqlCommand = null;
                            }
                        }


                        #endregion

                        #endregion " Master Data Save "

                        #region "Payment Line Master (Credit) Entry, Total Check Amount Entry, but it is in same table "
                        if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails != null && EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count > 0)
                        {
                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count; clmInsPayLnIndex++)
                            {
                                if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex] != null)
                                {
                                    _EOBPayDtlId = 0;
                                    EOBInsPayDtl = EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex];
                                    //EOBInsPayDtl.EOBPaymentDetailID = 0;
                                    oParameters.Clear();
                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBID", EOBInsPayDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBDtlID", EOBInsPayDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBPaymentDetailID", EOBInsPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)

                                    oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionLineNo", EOBInsPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    oParameters.Add("@nTrackTrnID", EOBInsPayDtl.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nTrackTrnDtlID", EOBInsPayDtl.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nTrackTrnLineNo", EOBInsPayDtl.TrackBillingTransactionLineNo, ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0)
                                    oParameters.Add("@sSubClaimNo", EOBInsPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0)

                                    oParameters.Add("@nPatientID", EOBInsPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nDOSFrom", EOBInsPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nDOSTo", EOBInsPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@sCPTCode", EOBInsPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sCPTDescription", EOBInsPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@nAmount", EOBInsPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    oParameters.Add("@nPaymentType", EOBInsPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentSubType", EOBInsPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaySign", EOBInsPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPayMode", EOBInsPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nRefEOBPaymentID", EOBInsPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nAccountID", EOBInsPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nAccountType", EOBInsPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nMSTAccountID", EOBInsPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nMSTAccountType", EOBInsPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentTrayID", EOBInsPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sPaymentTrayCode", EOBInsPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sPaymentTrayDescription", EOBInsPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@nUserID", EOBInsPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sUserName", EOBInsPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@dtCreatedDateTime", EOBInsPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@dtModifiedDateTime", EOBInsPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@nClinicID", EOBInsPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nResEOBPaymentID", EOBInsPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                    oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                    oParameters.Add("@nContactInsID", EOBInsPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nCreditLineID", EOBInsPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nCloseDate", EOBInsPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    oParameters.Add("@nOldRefEOBPaymentID", EOBInsPayDtl.OldRefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nOldRefEOBPaymentDetailID", EOBInsPayDtl.OldRefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nOldResEOBPaymentID", EOBInsPayDtl.OldReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nOldResEOBPaymentDetailID", EOBInsPayDtl.OldReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0)

                                    //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding the PAF values 
                                    oParameters.Add("@nPAccountID", EOBInsPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nAccountPatientID", EOBInsPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nGuarantorID", EOBInsPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                    //End

                                    _retVal = null;

                                    //oDB.Connect(false);
                                    //oDB.Execute("BL_INUP_EOBPayment_DTL", oParameters, out _retVal);
                                    //oDB.Disconnect();

                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                    _sqlCommand.Connection = _sqlConnection;
                                    _sqlCommand.Transaction = _sqlTransaction;
                                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                                    _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL_Tracking";

                                    _result = _sqlCommand.ExecuteNonQuery();

                                    if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                    { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                    else
                                    { _retVal = 0; }

                                    if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                                    {
                                        _EOBPayDtlId = Convert.ToInt64(_retVal.ToString());
                                    }

                                    _sqlCommand.Parameters.Clear();
                                    _sqlCommand.Dispose();
                                    _sqlCommand = null;
                                    EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentID = _EOBPayId;
                                    EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentDetailID = _EOBPayDtlId;

                                    #region "Assign Credit Line Reference and Finance Line Reference to debit line wherever applicable"

                                    if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails != null && EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count > 0)
                                    {
                                        if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].IsMainCreditLine == true)
                                        {
                                            //All Remaining Credit Lines
                                            for (int nAsgn = 0; nAsgn <= EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count - 1; nAsgn++)
                                            {
                                                if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[nAsgn].IsMainCreditLine == false)
                                                {
                                                    EOBPaymentInsurance.EOBInsurancePaymentLineDetails[nAsgn].MainCreditLineID = _EOBPayDtlId;
                                                }
                                            }

                                            ////Assign current check ref. to use reserve credit line - 20100220
                                            //for (int nAsgn = 0; nAsgn <= EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count - 1; nAsgn++)
                                            //{
                                            //    if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[nAsgn].IsReserveCreditLine == true)
                                            //    {
                                            //        EOBPaymentInsurance.EOBInsurancePaymentLineDetails[nAsgn].RefEOBPaymentID = _EOBPayId;
                                            //        EOBPaymentInsurance.EOBInsurancePaymentLineDetails[nAsgn].RefEOBPaymentDetailID = _EOBPayDtlId;

                                            //    }
                                            //}

                                            //All Debit Lines
                                            if (EOBPaymentInsurance.InsuranceClaims != null && EOBPaymentInsurance.InsuranceClaims.Count > 0)
                                            {
                                                for (int nAsgnClaim = 0; nAsgnClaim <= EOBPaymentInsurance.InsuranceClaims.Count - 1; nAsgnClaim++)
                                                {
                                                    if (EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines != null && EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines.Count > 0)
                                                    {
                                                        for (int nAsgnClmLine = 0; nAsgnClmLine <= EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines.Count - 1; nAsgnClmLine++)
                                                        {
                                                            if (EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails != null && EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails.Count > 0)
                                                            {
                                                                for (int nAsgn = 0; nAsgn <= EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails.Count - 1; nAsgn++)
                                                                {
                                                                    if (EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].IsMainCreditLine == false)
                                                                    {
                                                                        EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].MainCreditLineID = _EOBPayDtlId;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //Assign Reference Number to debit lines
                                    if (EOBPaymentInsurance.InsuranceClaims != null && EOBPaymentInsurance.InsuranceClaims.Count > 0)
                                    {
                                        for (int nAsgnClaim = 0; nAsgnClaim <= EOBPaymentInsurance.InsuranceClaims.Count - 1; nAsgnClaim++)
                                        {
                                            if (EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines != null && EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines.Count > 0)
                                            {
                                                for (int nAsgnClmLine = 0; nAsgnClmLine <= EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines.Count - 1; nAsgnClmLine++)
                                                {
                                                    if (EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails != null && EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails.Count > 0)
                                                    {
                                                        for (int nAsgn = 0; nAsgn <= EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails.Count - 1; nAsgn++)
                                                        {
                                                            if (EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].RefFinanceLieNo == EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].FinanceLieNo)
                                                            {
                                                                if (EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].UseRefFinanceLieNo == true)
                                                                {
                                                                    EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].RefEOBPaymentID = _EOBPayId;
                                                                    EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].RefEOBPaymentDetailID = _EOBPayDtlId;
                                                                    EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].OldRefEOBPaymentID = _EOBPayId;
                                                                    EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].OldRefEOBPaymentDetailID = _EOBPayDtlId;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    //Assign Main Credit Line ID to Direct Transaction Reserve Lines
                                    if (EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail != null)
                                    {
                                        for (int nResClmLine = 0; nResClmLine <= EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail.Count - 1; nResClmLine++)
                                        {
                                            EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[nResClmLine].MainCreditLineID = _EOBPayDtlId;
                                            if (EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[nResClmLine].UseRefFinanceLieNo == true)
                                            {
                                                if (EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[nResClmLine].RefEOBPaymentID == 0 && EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[nResClmLine].RefEOBPaymentDetailID == 0)
                                                {
                                                    EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[nResClmLine].RefEOBPaymentID = _EOBPayId;
                                                    EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[nResClmLine].RefEOBPaymentDetailID = _EOBPayDtlId;
                                                    EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[nResClmLine].OldRefEOBPaymentID = _EOBPayId;
                                                    EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[nResClmLine].OldRefEOBPaymentDetailID = _EOBPayDtlId;
                                                }
                                            }
                                        }
                                    }

                                    #endregion

                                    EOBInsPayDtl = null;
                                }
                            }
                        }
                        #endregion

                        #region "Payment Line Master Reserve (Credit) Entry"

                        if (EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail != null && EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail.Count > 0)
                        {
                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail.Count; clmInsPayLnIndex++)
                            {
                                if (EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[clmInsPayLnIndex] != null)
                                {
                                    _EOBPayDtlId = 0;
                                    EOBInsPayDtl = EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[clmInsPayLnIndex];

                                    oParameters.Clear();
                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBID", EOBInsPayDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBDtlID", EOBInsPayDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBPaymentDetailID", EOBInsPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)

                                    oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionLineNo", EOBInsPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    oParameters.Add("@sSubClaimNo", EOBInsPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);//	int
                                    oParameters.Add("@nTrackTrnID", EOBInsPayDtl.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                    oParameters.Add("@nTrackTrnDtlID", EOBInsPayDtl.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                    oParameters.Add("@nTrackTrnLineNo", EOBInsPayDtl.TrackBillingTransactionLineNo, ParameterDirection.Input, SqlDbType.Int);// numeric(18,0)


                                    oParameters.Add("@nPatientID", EOBInsPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nDOSFrom", EOBInsPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nDOSTo", EOBInsPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@sCPTCode", EOBInsPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sCPTDescription", EOBInsPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                    if (EOBInsPayDtl.IsNullAmount == false)
                                    {
                                        oParameters.Add("@nAmount", EOBInsPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    oParameters.Add("@nPaymentType", EOBInsPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentSubType", EOBInsPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaySign", EOBInsPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPayMode", EOBInsPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nAccountID", EOBInsPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nAccountType", EOBInsPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nMSTAccountID", EOBInsPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nMSTAccountType", EOBInsPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentTrayID", EOBInsPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sPaymentTrayCode", EOBInsPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sPaymentTrayDescription", EOBInsPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@nUserID", EOBInsPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sUserName", EOBInsPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@dtCreatedDateTime", EOBInsPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@dtModifiedDateTime", EOBInsPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@nClinicID", EOBInsPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    oParameters.Add("@nRefEOBPaymentID", EOBInsPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //..ResEOBPaymentID,ResEOBPaymentDetailID has the reference id's for the reserve amount
                                    oParameters.Add("@nResEOBPaymentID", EOBInsPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);

                                    oParameters.Add("@nContactInsID", EOBInsPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nCreditLineID", EOBInsPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    oParameters.Add("@nCloseDate", EOBInsPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    oParameters.Add("@nOldRefEOBPaymentID", EOBInsPayDtl.OldRefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nOldRefEOBPaymentDetailID", EOBInsPayDtl.OldRefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nOldResEOBPaymentID", EOBInsPayDtl.OldReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nOldResEOBPaymentDetailID", EOBInsPayDtl.OldReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0)

                                    //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding the PAF values 
                                    oParameters.Add("@nPAccountID", EOBInsPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nAccountPatientID", EOBInsPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nGuarantorID", EOBInsPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                    //End

                                    _retVal = null;

                                    //oDB.Connect(false);
                                    //oDB.Execute("BL_INUP_EOBPayment_DTL_PatPayment", oParameters, out _retVal);
                                    //oDB.Disconnect();

                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                    _sqlCommand.Connection = _sqlConnection;
                                    _sqlCommand.Transaction = _sqlTransaction;
                                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                                    _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL_Tracking";

                                    _result = _sqlCommand.ExecuteNonQuery();

                                    if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                    { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                    else
                                    { _retVal = 0; }


                                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                    { _EOBPayDtlId = Convert.ToInt64(_retVal); }
                                    _sqlCommand.Parameters.Clear();
                                    _sqlCommand.Dispose();
                                    _sqlCommand = null;
                                    #region " Add Line Notes "

                                    if (EOBInsPayDtl.LineNotes != null && EOBInsPayDtl.LineNotes.Count > 0)
                                    {
                                        Object _RcValue = null;

                                        for (int rcInd = 0; rcInd < EOBInsPayDtl.LineNotes.Count; rcInd++)
                                        {
                                            _RcValue = null;
                                            oParameters.Clear();

                                            oParameters.Add("@nID", EOBInsPayDtl.LineNotes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBID", EOBInsPayDtl.LineNotes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentDetailID", _EOBPayDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                            oParameters.Add("@nClaimNo", EOBInsPayDtl.LineNotes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.LineNotes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                            oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.LineNotes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                            oParameters.Add("@nTrackTrnID", EOBInsPayDtl.LineNotes[rcInd].TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                            oParameters.Add("@nTrackTrnDtlID", EOBInsPayDtl.LineNotes[rcInd].TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nTrackTrnLineNo", EOBInsPayDtl.LineNotes[rcInd].TrackBillingTransactionLineNo, ParameterDirection.Input, SqlDbType.Int);
                                            oParameters.Add("@sSubClaimNo", EOBInsPayDtl.LineNotes[rcInd].SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);


                                            oParameters.Add("@sNoteCode", EOBInsPayDtl.LineNotes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                            oParameters.Add("@sNoteDescription", EOBInsPayDtl.LineNotes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                            oParameters.Add("@dNoteAmount", EOBInsPayDtl.LineNotes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                            oParameters.Add("@nPaymentNoteType", EOBInsPayDtl.LineNotes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nPaymentNoteSubType", EOBInsPayDtl.LineNotes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nIncludeNoteOnPrint", EOBInsPayDtl.LineNotes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
                                            oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 
                                            oParameters.Add("@nCloseDate", EOBInsPayDtl.LineNotes[rcInd].CloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                                            oParameters.Add("@nUserID", EOBInsPayDtl.LineNotes[rcInd].UserId, ParameterDirection.Input, SqlDbType.BigInt);
                                            //oDB.Connect(false);
                                            //oDB.Execute("BL_INUP_EOBNotes", oParameters, out _RcValue);
                                            //oDB.Disconnect();

                                            _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                            _sqlCommand = oDB.GetCmdParameters(oParameters);
                                            _sqlCommand.Connection = _sqlConnection;
                                            _sqlCommand.Transaction = _sqlTransaction;
                                            _sqlCommand.CommandType = CommandType.StoredProcedure;
                                            _sqlCommand.CommandText = "BL_INUP_EOBNotes_Tracking";

                                            _result = _sqlCommand.ExecuteNonQuery();

                                            if (_sqlCommand.Parameters["@nID"].Value != null)
                                            { _RcValue = _sqlCommand.Parameters["@nID"].Value; }
                                            else
                                            { _RcValue = 0; }
                                            _sqlCommand.Parameters.Clear();
                                            _sqlCommand.Dispose();
                                            _sqlCommand = null;
                                        }
                                    }

                                    #endregion " Add Line Notes "


                                    EOBInsPayDtl = null;
                                }
                            }
                        }
                        #endregion

                        #region " EOB Data Save "

                        //if (EOBPaymentInsurance.InsuranceClaims != null && EOBPaymentInsurance.InsuranceClaims.Count > 0)
                        //{

                        //..Payment Line Master(Credit) Entry,Total Check Amount Entry,but it is in same table region
                        //..move out of the the above commented condition

                        if (EOBPaymentInsurance.InsuranceClaims != null && EOBPaymentInsurance.InsuranceClaims.Count > 0)
                        {
                            #region "Payment EOB and Finance DEbit Line Entries"

                            for (int clmIndex = 0; clmIndex < EOBPaymentInsurance.InsuranceClaims.Count; clmIndex++)
                            {
                                if (_EOBPayId > 0 && EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines != null && EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines.Count > 0)
                                {
                                    for (int clmLnIndex = 0; clmLnIndex < EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines.Count; clmLnIndex++)
                                    {

                                        if (EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex] != null)
                                        {
                                            _EOBDtlId = 0;
                                            PaymentInsClaimLine = EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex];

                                            oParameters.Clear();

                                            // If any claim line has no EOB Payment details then Skip the EOB Service Line Region
                                            // Added by Pankaj Bedse on 15032010
                                            //if (PaymentInsClaimLine.EOBInsurancePaymentLineDetails.Count != 0)
                                            {
                                                #region "EOB Service Line"

                                                //nEOBPaymentID,nEOBID,nEOBPaymentDetailID,nBillingTransactionID,nBillingTransactionDetailID
                                                //nBillingTransactionLineNo,nPatientID,nDOSFrom,nDOSTo,sCPTCode,sCPTDescription,nAmount,
                                                //nPaymentType,nPaymentSubType,nPaySign,nRefEOBPaymentID,nRefEOBPaymentDetailID,nAccountID
                                                //nAccountType,nMSTAccountID,nMSTAccountType,nPaymentTrayID,nPaymentTrayCode,nPaymentTrayDescription
                                                //nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                                                if (_UseExtEOBID == true) { PaymentInsClaimLine.mEOBID = _EOBId; }
                                                oParameters.Add("@nEOBID", PaymentInsClaimLine.mEOBID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                                oParameters.Add("@nEOBDtlID", PaymentInsClaimLine.mEOBDtlID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0)
                                                //oParameters.Add("@nClaimNo", PaymentInsClaimLine.ClaimNumber, ParameterDirection.Input, SqlDbType.Int);//	int
                                                oParameters.Add("@nClaimNo", PaymentInsClaimLine.ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                                oParameters.Add("@sSubClaimNo", PaymentInsClaimLine.SubClaimNumber, ParameterDirection.Input, SqlDbType.VarChar);//	int
                                                oParameters.Add("@nDOSFrom", PaymentInsClaimLine.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                                oParameters.Add("@nDOSTo", PaymentInsClaimLine.DOSTo, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                                oParameters.Add("@sCPTCode", PaymentInsClaimLine.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                oParameters.Add("@sCPTDescription", PaymentInsClaimLine.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                                if (PaymentInsClaimLine.IsNullCharges == false)
                                                {
                                                    oParameters.Add("@dCharges", PaymentInsClaimLine.Charges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }

                                                if (PaymentInsClaimLine.IsNullUnit == false)
                                                {
                                                    oParameters.Add("@dUnit", PaymentInsClaimLine.Unit, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dUnit", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                                }

                                                if (PaymentInsClaimLine.IsNullTotalCharges == false)
                                                {
                                                    oParameters.Add("@dTotalCharges", PaymentInsClaimLine.TotalCharges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dTotalCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }

                                                if (PaymentInsClaimLine.IsNullAllowed == false)
                                                {
                                                    oParameters.Add("@dAllowed", PaymentInsClaimLine.Allowed, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dAllowed", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }

                                                if (PaymentInsClaimLine.IsNullWriteOff == false)
                                                {
                                                    oParameters.Add("@dWriteOff", PaymentInsClaimLine.WriteOff, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dWriteOff", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }

                                                if (PaymentInsClaimLine.IsNullNonCovered == false)
                                                {
                                                    oParameters.Add("@dNotCovered", PaymentInsClaimLine.NonCovered, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dNotCovered", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }

                                                if (PaymentInsClaimLine.IsNullInsurance == false)
                                                {
                                                    oParameters.Add("@dPayment", PaymentInsClaimLine.InsuranceAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dPayment", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }

                                                if (PaymentInsClaimLine.IsNullCopay == false)
                                                {
                                                    oParameters.Add("@dCopay", PaymentInsClaimLine.Copay, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dCopay", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }

                                                if (PaymentInsClaimLine.IsNullDeductible == false)
                                                {
                                                    oParameters.Add("@dDeductible", PaymentInsClaimLine.Deductible, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dDeductible", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }

                                                if (PaymentInsClaimLine.IsNullCoInsurance == false)
                                                {
                                                    oParameters.Add("@dCoInsurance", PaymentInsClaimLine.CoInsurance, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dCoInsurance", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                                }

                                                if (PaymentInsClaimLine.IsNullWithhold == false)
                                                {
                                                    oParameters.Add("@dWithhold", PaymentInsClaimLine.Withhold, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dWithhold", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                }

                                                oParameters.Add("@nPaymentTrayID", PaymentInsClaimLine.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                                oParameters.Add("@sPaymentTrayCode", PaymentInsClaimLine.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                                                oParameters.Add("@sPaymentTrayDescription", PaymentInsClaimLine.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked

                                                oParameters.Add("@nUserID", PaymentInsClaimLine.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                                oParameters.Add("@sUserName", PaymentInsClaimLine.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked
                                                oParameters.Add("@dtCreatedDateTime", PaymentInsClaimLine.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked
                                                oParameters.Add("@dtModifiedDateTime", PaymentInsClaimLine.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked

                                                oParameters.Add("@nPatientID", PaymentInsClaimLine.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                                oParameters.Add("@nInsuraceID", PaymentInsClaimLine.PatInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                                oParameters.Add("@nContactID", PaymentInsClaimLine.InsContactID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked

                                                oParameters.Add("@nClinicID", PaymentInsClaimLine.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                oParameters.Add("@bUseExtEOBID", _UseExtEOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                                oParameters.Add("@nEOBType", PaymentInsClaimLine.EOBType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);// int,

                                                oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.BLTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                                oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.BLTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                                oParameters.Add("@nBillingTransactionLineNo", PaymentInsClaimLine.BLTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)

                                                oParameters.Add("@nTrackTrnID", PaymentInsClaimLine.TrackBLTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                                oParameters.Add("@nTrackTrnDtlID", PaymentInsClaimLine.TrackBLTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                                oParameters.Add("@nTrackTrnLineNo", PaymentInsClaimLine.TrackBLTransactionLineNo, ParameterDirection.Input, SqlDbType.Int);// numeric(18,0)

                                                oParameters.Add("@nInsuranceCompanyID", PaymentInsClaimLine.InsCompanyID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
                                                oParameters.Add("@nCloseDate", PaymentInsClaimLine.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)

                                                //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding the PAF values 
                                                oParameters.Add("@nPAccountID", PaymentInsClaimLine.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                                oParameters.Add("@nAccountPatientID", PaymentInsClaimLine.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                                oParameters.Add("@nGuarantorID", PaymentInsClaimLine.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                                //End

                                                if (_UseExtEOBID == false) { _UseExtEOBID = true; }
                                                _retVal = null;
                                                _valRet = null;

                                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                _sqlCommand.Connection = _sqlConnection;
                                                _sqlCommand.Transaction = _sqlTransaction;
                                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                _sqlCommand.CommandText = "BL_INSERT_EOBPayment_EOB_Tracking";

                                                _result = _sqlCommand.ExecuteNonQuery();

                                                if (_sqlCommand.Parameters["@nEOBID"].Value != null)
                                                { _retVal = _sqlCommand.Parameters["@nEOBID"].Value; }
                                                else
                                                { _retVal = 0; }

                                                if (_sqlCommand.Parameters["@nEOBDtlID"].Value != null)
                                                { _valRet = _sqlCommand.Parameters["@nEOBDtlID"].Value; }
                                                else
                                                { _valRet = 0; }

                                                if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                                { _EOBId = Convert.ToInt64(_retVal); EOBId = _EOBId; }

                                                if (_valRet != null && Convert.ToString(_valRet).Trim() != "")
                                                { _EOBDtlId = Convert.ToInt64(_valRet); }
                                                _sqlCommand.Parameters.Clear();
                                                _sqlCommand.Dispose();
                                                _sqlCommand = null;
                                                #endregion
                                            }

                                            #region " EOB Service Line Details (Next Action, Next Party, Reason Codes, Notes etc. "

                                            #region " Add Line NextAction  "

                                            // Following Condition has been removed 
                                            // " PaymentInsClaimLine.LineNextAction.HasActionData "
                                            // As Next Action details were not updated for NONE & Pending Action 
                                            // Commemted by Pankaj on 13032010
                                            //if (PaymentInsClaimLine.LineNextAction != null && PaymentInsClaimLine.LineNextAction.HasData == true && PaymentInsClaimLine.LineNextAction.HasActionData)

                                            if (PaymentInsClaimLine.LineNextAction != null && PaymentInsClaimLine.LineNextAction.HasData == true)
                                            {
                                                //Object _nextActRetVal = null;

                                                oParameters.Clear();

                                                oParameters.Add("@nID", PaymentInsClaimLine.LineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                                oParameters.Add("@nClaimNo", PaymentInsClaimLine.LineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentDetailID", PaymentInsClaimLine.LineNextAction.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                                oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.LineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.LineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                                //oParameters.Add("@nTrackTrnID", PaymentInsClaimLine.LineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                                oParameters.Add("@nTrackTrnID", PaymentInsClaimLine.LineNextAction.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),

                                                oParameters.Add("@nTrackTrnDtlID", PaymentInsClaimLine.LineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                                oParameters.Add("@sSubClaimNo", PaymentInsClaimLine.LineNextAction.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                                                oParameters.Add("@nTrackTrnLineNo", 0, ParameterDirection.Input, SqlDbType.Int);// int  

                                                oParameters.Add("@nNextActionPatientInsID", PaymentInsClaimLine.LineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                                                oParameters.Add("@nNextActionPatientInsName", PaymentInsClaimLine.LineNextAction.NextActionPatientInsName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                oParameters.Add("@nNextActionPartyNumber", PaymentInsClaimLine.LineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                                                oParameters.Add("@nNextPartyType", PaymentInsClaimLine.LineNextAction.NextPartyType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                                                oParameters.Add("@nNextActionContactID", PaymentInsClaimLine.LineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sNextActionCode", PaymentInsClaimLine.LineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                oParameters.Add("@sNextActionDescription", PaymentInsClaimLine.LineNextAction.NextActionDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                if (PaymentInsClaimLine.LineNextAction.IsNullNextActionAmount == false)
                                                {
                                                    oParameters.Add("@dNextActionAmount", PaymentInsClaimLine.LineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dNextActionAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                }

                                                oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                                // ----------------------------------------
                                                // Parameters added - Pankaj bedse 29012010
                                                oParameters.Add("@nCloseDate", PaymentInsClaimLine.LineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nUserID", PaymentInsClaimLine.LineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sUserName", PaymentInsClaimLine.LineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),
                                                // ----------------------------------------

                                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                _sqlCommand.Connection = _sqlConnection;
                                                _sqlCommand.Transaction = _sqlTransaction;
                                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                _sqlCommand.CommandText = "BL_UP_EOBNextAction_Tracking";

                                                _result = _sqlCommand.ExecuteNonQuery();

                                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                                { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                                else
                                                { _retVal = 0; }
                                                _sqlCommand.Parameters.Clear();
                                                _sqlCommand.Dispose();
                                                _sqlCommand = null;
                                            }

                                            #endregion " Add Line NextAction "

                                            #region " Add Line Next Party "

                                            if (PaymentInsClaimLine.LineNextAction != null && PaymentInsClaimLine.LineNextAction.HasData == true && PaymentInsClaimLine.LineNextAction.HasActionData)
                                            {
                                               // Object _nextActRetVal = null;

                                                oParameters.Clear();

                                                oParameters.Add("@nID", PaymentInsClaimLine.LineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                                oParameters.Add("@nClaimNo", PaymentInsClaimLine.LineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentDetailID", PaymentInsClaimLine.LineNextAction.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                                oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.LineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.LineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                                oParameters.Add("@nTrackMstTrnID", PaymentInsClaimLine.LineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                                oParameters.Add("@nTrackMstTrnDetailID", PaymentInsClaimLine.LineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                                oParameters.Add("@sSubClaimNo", PaymentInsClaimLine.LineNextAction.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                                                //oParameters.Add("@nTrackTrnLineNo", 0, ParameterDirection.Input, SqlDbType.Int);// int  

                                                oParameters.Add("@nNextActionPatientInsID", PaymentInsClaimLine.LineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                                                oParameters.Add("@nNextActionPatientInsName", PaymentInsClaimLine.LineNextAction.NextActionPatientInsName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                oParameters.Add("@nNextActionPartyNumber", PaymentInsClaimLine.LineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                                                oParameters.Add("@nNextPartyType", PaymentInsClaimLine.LineNextAction.NextPartyType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                                                oParameters.Add("@nNextActionContactID", PaymentInsClaimLine.LineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sNextActionCode", PaymentInsClaimLine.LineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                oParameters.Add("@sNextActionDescription", PaymentInsClaimLine.LineNextAction.NextActionDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                if (PaymentInsClaimLine.LineNextAction.IsNullNextActionAmount == false)
                                                {
                                                    oParameters.Add("@dNextActionAmount", PaymentInsClaimLine.LineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dNextActionAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                }

                                                oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                                // ----------------------------------------
                                                // Parameters added - Pankaj bedse 29012010
                                                oParameters.Add("@nCloseDate", PaymentInsClaimLine.LineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nUserID", PaymentInsClaimLine.LineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sUserName", PaymentInsClaimLine.LineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),
                                                // ----------------------------------------



                                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                _sqlCommand.Connection = _sqlConnection;
                                                _sqlCommand.Transaction = _sqlTransaction;
                                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                _sqlCommand.CommandText = "BL_UP_EOBParty";

                                                _result = _sqlCommand.ExecuteNonQuery();

                                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                                { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                                else
                                                { _retVal = 0; }
                                                _sqlCommand.Parameters.Clear();
                                                _sqlCommand.Dispose();
                                                _sqlCommand = null;
                                            }

                                            #endregion " Add Line Next Party "

                                            #region " Add Line Reason Codes "

                                            if (PaymentInsClaimLine.LineResonCodes != null && PaymentInsClaimLine.LineResonCodes.Count > 0)
                                            {
                                                //Object _RcValue = null;

                                                for (int rcInd = 0; rcInd < PaymentInsClaimLine.LineResonCodes.Count; rcInd++)
                                                {
                                                  //  _RcValue = null;
                                                    oParameters.Clear();

                                                    oParameters.Add("@nID", PaymentInsClaimLine.LineResonCodes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,

                                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nEOBPaymentDetailID", PaymentInsClaimLine.LineResonCodes[rcInd].EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                                    oParameters.Add("@nClaimNo", PaymentInsClaimLine.LineResonCodes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.LineResonCodes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                    oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.LineResonCodes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                                    //oParameters.Add("@nTrackTrnID", PaymentInsClaimLine.LineResonCodes[rcInd].TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                                    oParameters.Add("@nTrackTrnID", PaymentInsClaimLine.LineResonCodes[rcInd].TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),

                                                    oParameters.Add("@nTrackTrnDtlID", PaymentInsClaimLine.LineResonCodes[rcInd].TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                                    oParameters.Add("@sSubClaimNo", PaymentInsClaimLine.LineResonCodes[rcInd].SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                                                    oParameters.Add("@nTrackTrnLineNo", 0, ParameterDirection.Input, SqlDbType.Int);// int  

                                                    oParameters.Add("@sReasonCode", PaymentInsClaimLine.LineResonCodes[rcInd].ReasonCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                    oParameters.Add("@sReasonDescription", PaymentInsClaimLine.LineResonCodes[rcInd].ReasonDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                    if (PaymentInsClaimLine.LineResonCodes[rcInd].IsNullReasonAmount == false)
                                                    {
                                                        oParameters.Add("@dReasonAmount", PaymentInsClaimLine.LineResonCodes[rcInd].ReasonAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                    }
                                                    else
                                                    {
                                                        oParameters.Add("@dReasonAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                    }

                                                    oParameters.Add("@nType", PaymentInsClaimLine.LineResonCodes[rcInd].CommentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                                    oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 
                                                    oParameters.Add("@nCloseDate", PaymentInsClaimLine.LineResonCodes[rcInd].CloseDate, ParameterDirection.Input, SqlDbType.BigInt);

                                                    //oDB.Connect(false);
                                                    //oDB.Execute("BL_INUP_EOBReasonCode", oParameters, out _RcValue);
                                                    //oDB.Disconnect();

                                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                    _sqlCommand.Connection = _sqlConnection;
                                                    _sqlCommand.Transaction = _sqlTransaction;
                                                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                    _sqlCommand.CommandText = "BL_INUP_EOBReasonCode_Tracking";

                                                    _result = _sqlCommand.ExecuteNonQuery();

                                                    if (_sqlCommand.Parameters["@nID"].Value != null)
                                                    { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                                    else
                                                    { _retVal = 0; }
                                                    _sqlCommand.Parameters.Clear();
                                                    _sqlCommand.Dispose();
                                                    _sqlCommand = null;
                                                }
                                            }

                                            #endregion " Add Line Reason Codes "

                                            #region " Add Line Notes "

                                            if (PaymentInsClaimLine.LineNotes != null && PaymentInsClaimLine.LineNotes.Count > 0)
                                            {
                                                Object _RcValue = null;

                                                for (int rcInd = 0; rcInd < PaymentInsClaimLine.LineNotes.Count; rcInd++)
                                                {
                                                    _RcValue = null;
                                                    oParameters.Clear();

                                                    oParameters.Add("@nID", PaymentInsClaimLine.LineNotes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nEOBID", PaymentInsClaimLine.LineNotes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nEOBPaymentDetailID", _EOBPayDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                                    oParameters.Add("@nClaimNo", PaymentInsClaimLine.LineNotes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.LineNotes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                    oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.LineNotes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                                    oParameters.Add("@nTrackTrnID", PaymentInsClaimLine.LineNotes[rcInd].TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                    oParameters.Add("@nTrackTrnDtlID", PaymentInsClaimLine.LineNotes[rcInd].TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nTrackTrnLineNo", PaymentInsClaimLine.LineNotes[rcInd].TrackBillingTransactionLineNo, ParameterDirection.Input, SqlDbType.Int);
                                                    oParameters.Add("@sSubClaimNo", PaymentInsClaimLine.LineNotes[rcInd].SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);


                                                    oParameters.Add("@sNoteCode", PaymentInsClaimLine.LineNotes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                    oParameters.Add("@sNoteDescription", PaymentInsClaimLine.LineNotes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                    oParameters.Add("@dNoteAmount", PaymentInsClaimLine.LineNotes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                    oParameters.Add("@nPaymentNoteType", PaymentInsClaimLine.LineNotes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                                    oParameters.Add("@nPaymentNoteSubType", PaymentInsClaimLine.LineNotes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                                    oParameters.Add("@nIncludeNoteOnPrint", PaymentInsClaimLine.LineNotes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
                                                    oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 
                                                    oParameters.Add("@nCloseDate", PaymentInsClaimLine.LineNotes[rcInd].CloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nUserID", PaymentInsClaimLine.LineNotes[rcInd].UserId, ParameterDirection.Input, SqlDbType.BigInt);

                                                    //oDB.Connect(false);
                                                    //oDB.Execute("BL_INUP_EOBNotes", oParameters, out _RcValue);
                                                    //oDB.Disconnect();

                                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                    _sqlCommand.Connection = _sqlConnection;
                                                    _sqlCommand.Transaction = _sqlTransaction;
                                                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                    _sqlCommand.CommandText = "BL_INUP_EOBNotes_Tracking";

                                                    _result = _sqlCommand.ExecuteNonQuery();

                                                    if (_sqlCommand.Parameters["@nID"].Value != null)
                                                    { _RcValue = _sqlCommand.Parameters["@nID"].Value; }
                                                    else
                                                    { _RcValue = 0; }
                                                    _sqlCommand.Parameters.Clear();
                                                    _sqlCommand.Dispose();
                                                    _sqlCommand = null;
                                                }
                                            }

                                            #endregion " Add Line Notes "

                                            #endregion

                                            #region " EOB Financial Service Line Save "

                                            if (_EOBPayId > 0 && PaymentInsClaimLine.EOBInsurancePaymentLineDetails != null && PaymentInsClaimLine.EOBInsurancePaymentLineDetails.Count > 0)
                                            {
                                                for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < PaymentInsClaimLine.EOBInsurancePaymentLineDetails.Count; clmInsPayLnIndex++)
                                                {
                                                    if (PaymentInsClaimLine.EOBInsurancePaymentLineDetails[clmInsPayLnIndex] != null)
                                                    {
                                                        _EOBPayDtlId = 0;
                                                        EOBInsPayDtl = PaymentInsClaimLine.EOBInsurancePaymentLineDetails[clmInsPayLnIndex];

                                                        oParameters.Clear();
                                                        oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nEOBDtlID", _EOBDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nEOBPaymentDetailID", EOBInsPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)

                                                        oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nBillingTransactionLineNo", EOBInsPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                                        oParameters.Add("@nTrackTrnID", EOBInsPayDtl.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nTrackTrnDtlID", EOBInsPayDtl.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nTrackTrnLineNo", EOBInsPayDtl.TrackBillingTransactionLineNo, ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0)
                                                        oParameters.Add("@sSubClaimNo", EOBInsPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0)


                                                        oParameters.Add("@nPatientID", EOBInsPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nDOSFrom", EOBInsPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nDOSTo", EOBInsPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@sCPTCode", EOBInsPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                        oParameters.Add("@sCPTDescription", EOBInsPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                        oParameters.Add("@nAmount", EOBInsPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                        oParameters.Add("@nPaymentType", EOBInsPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPaymentSubType", EOBInsPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPaySign", EOBInsPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPayMode", EOBInsPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nRefEOBPaymentID", EOBInsPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nAccountID", EOBInsPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nAccountType", EOBInsPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nMSTAccountID", EOBInsPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nMSTAccountType", EOBInsPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPaymentTrayID", EOBInsPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@sPaymentTrayCode", EOBInsPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                        oParameters.Add("@sPaymentTrayDescription", EOBInsPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                        oParameters.Add("@nUserID", EOBInsPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@sUserName", EOBInsPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                        oParameters.Add("@dtCreatedDateTime", EOBInsPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                        oParameters.Add("@dtModifiedDateTime", EOBInsPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                        oParameters.Add("@nClinicID", EOBInsPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nResEOBPaymentID", EOBInsPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                        oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                        oParameters.Add("@nContactInsID", EOBInsPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                        oParameters.Add("@nCreditLineID", EOBInsPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                        oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                        oParameters.Add("@nCloseDate", EOBInsPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                                        oParameters.Add("@nOldRefEOBPaymentID", EOBInsPayDtl.OldRefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                        oParameters.Add("@nOldRefEOBPaymentDetailID", EOBInsPayDtl.OldRefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                        oParameters.Add("@nOldResEOBPaymentID", EOBInsPayDtl.OldReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                        oParameters.Add("@nOldResEOBPaymentDetailID", EOBInsPayDtl.OldReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0)
                                                        
                                                        //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                                                        oParameters.Add("@nPAccountID", EOBInsPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                                        oParameters.Add("@nAccountPatientID", EOBInsPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                                        oParameters.Add("@nGuarantorID", EOBInsPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                                        //End

                                                        _retVal = null;

                                                        //oDB.Connect(false);
                                                        //oDB.Execute("BL_INUP_EOBPayment_DTL", oParameters, out _retVal);
                                                        //oDB.Disconnect();

                                                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                        _sqlCommand.Connection = _sqlConnection;
                                                        _sqlCommand.Transaction = _sqlTransaction;
                                                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                        _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL_Tracking";

                                                        _result = _sqlCommand.ExecuteNonQuery();

                                                        if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                                        { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                                        else
                                                        { _retVal = 0; }

                                                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                                        { _EOBPayDtlId = Convert.ToInt64(_retVal); }

                                                        EOBInsPayDtl = null;
                                                        _sqlCommand.Parameters.Clear();
                                                        _sqlCommand.Dispose();
                                                        _sqlCommand = null;
                                                    }
                                                }
                                            }


                                            #endregion " EOB Financial Service Line Save "

                                            PaymentInsClaimLine = null;
                                        }
                                    }
                                }
                            }//20091020 (for)

                            #endregion

                        }//20091020 (If)

                        #endregion " EOB Data Save "

                        _sqlTransaction.Commit();
                        _sqlConnection.Close();

                        #region " Save last selected Close date "

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseConnectionString);
                        oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(gloDateMaster.gloDate.DateAsDate(EOBPaymentInsurance.CloseDate)).ToString("MM/dd/yyyy"), _clinicId, EOBPaymentInsurance.UserID, gloSettings.SettingFlag.User);
                        oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", EOBPaymentInsurance.PaymentTrayID.ToString(), _clinicId, EOBPaymentInsurance.UserID, gloSettings.SettingFlag.User);
                        oSettings.Dispose();

                        #endregion " Save last selected Close date "
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                { _sqlTransaction.Rollback(); _sqlConnection.Close(); ex.ERROR_Log(ex.ToString()); }
                catch (Exception ex)
                { _sqlTransaction.Rollback(); _sqlConnection.Close(); gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
                finally
                {
                    //if (oDB != null) { oDB.Dispose(); }
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (_retVal != null) { _retVal = null; }

                    if (_sqlConnection != null) { _sqlConnection.Dispose(); _sqlConnection = null; }
                    if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); _sqlCommand = null; }
                    if (_sqlTransaction != null) { _sqlTransaction.Dispose(); _sqlTransaction = null; }

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }

                return _EOBPayId;
            }

            public void UpdateNextAction(EOBPayment.Common.PaymentInsuranceLineNextActions oPaymentInsuranceLineNextActions)
            {
                System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(_databaseConnectionString);
                System.Data.SqlClient.SqlCommand _sqlCommand = null;
                System.Data.SqlClient.SqlTransaction _sqlTransaction = null;
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);    

                EOBPayment.Common.PaymentInsuranceLineNextAction oLineNextAction = null;
                object _retVal = null;
                int _result = 0;

                try
                {
                    if (oPaymentInsuranceLineNextActions != null && oPaymentInsuranceLineNextActions.Count > 0)
                    {
                        _sqlConnection.Open();
                        _sqlTransaction = _sqlConnection.BeginTransaction();

                        for (int index = 0; index < oPaymentInsuranceLineNextActions.Count; index++)
                        {
                          //  oLineNextAction = new EOBPayment.Common.PaymentInsuranceLineNextAction();
                            oLineNextAction = oPaymentInsuranceLineNextActions[index];

                            if (oLineNextAction != null && oLineNextAction.HasData == true)
                            {
                                oParameters.Clear();
                                oParameters.Add("@nID", oLineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                oParameters.Add("@nClaimNo", oLineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentID", oLineNextAction.EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBID", oLineNextAction.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentDetailID", oLineNextAction.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nBillingTransactionID", oLineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                oParameters.Add("@nBillingTransactionDetailID", oLineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nNextActionPatientInsID", oLineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                                oParameters.Add("@nNextActionPatientInsName", oLineNextAction.NextActionPatientInsName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                oParameters.Add("@nNextActionPartyNumber", oLineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                                oParameters.Add("@nNextPartyType", oLineNextAction.NextPartyType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                                oParameters.Add("@nNextActionContactID", oLineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@sNextActionCode", oLineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                oParameters.Add("@sNextActionDescription", oLineNextAction.NextActionDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                if (oLineNextAction.IsNullNextActionAmount == false)
                                {
                                    oParameters.Add("@dNextActionAmount", oLineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                }
                                else
                                {
                                    oParameters.Add("@dNextActionAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                }

                                oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                //oParameters.Add("@nTrackTrnID", oLineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                oParameters.Add("@nTrackTrnID", oLineNextAction.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),

                                oParameters.Add("@nTrackTrnDtlID", oLineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                oParameters.Add("@sSubClaimNo", oLineNextAction.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                                oParameters.Add("@nTrackTrnLineNo", 0, ParameterDirection.Input, SqlDbType.Int);// int  

                                // ----------------------------------------
                                // Parameters added - Pankaj bedse 29012010
                                oParameters.Add("@nCloseDate", oLineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nUserID", oLineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@sUserName", oLineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),
                                // ----------------------------------------

                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                //_sqlCommand.CommandText = "BL_UP_EOBNextAction";
                                _sqlCommand.CommandText = "BL_UP_EOBNextAction_Tracking";

                                _result = _sqlCommand.ExecuteNonQuery();

                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                else
                                { _retVal = 0; }

                                if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); }

                            }


                            //if (oLineNextAction != null) { oLineNextAction.Dispose(); }
                        }

                        _sqlTransaction.Commit();
                        _sqlConnection.Close();
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    _sqlTransaction.Rollback();
                    _sqlConnection.Close(); ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    _sqlTransaction.Rollback();
                    _sqlConnection.Close(); gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (_retVal != null) { _retVal = null; }

                    if (_sqlConnection != null) { _sqlConnection.Dispose(); _sqlConnection = null; }
                    if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); _sqlCommand = null; }
                    if (_sqlTransaction != null) { _sqlTransaction.Dispose(); _sqlTransaction = null; }

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }
            }

            public void UpdateParty(EOBPayment.Common.PaymentInsuranceLineNextActions oPaymentInsuranceLineNextActions)
            {
                System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(_databaseConnectionString);
                System.Data.SqlClient.SqlCommand _sqlCommand = null;
                System.Data.SqlClient.SqlTransaction _sqlTransaction = null;
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);    
                EOBPayment.Common.PaymentInsuranceLineNextAction oLineNextAction = null;
                object _retVal = null;
                int _result = 0;

                try
                {
                    if (oPaymentInsuranceLineNextActions != null && oPaymentInsuranceLineNextActions.Count > 0)
                    {
                        _sqlConnection.Open();
                        _sqlTransaction = _sqlConnection.BeginTransaction();

                        for (int index = 0; index < oPaymentInsuranceLineNextActions.Count; index++)
                        {
                           // oLineNextAction = new EOBPayment.Common.PaymentInsuranceLineNextAction();
                            oLineNextAction = oPaymentInsuranceLineNextActions[index];

                            if (oLineNextAction != null && oLineNextAction.HasData == true)
                            {
                                oParameters.Clear();
                                oParameters.Add("@nID", oLineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                oParameters.Add("@nClaimNo", oLineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentID", oLineNextAction.EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBID", oLineNextAction.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentDetailID", oLineNextAction.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nBillingTransactionID", oLineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                oParameters.Add("@nBillingTransactionDetailID", oLineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nNextActionPatientInsID", oLineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                                oParameters.Add("@nNextActionPatientInsName", oLineNextAction.NextActionPatientInsName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                oParameters.Add("@nNextActionPartyNumber", oLineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                                oParameters.Add("@nNextPartyType", oLineNextAction.NextPartyType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                                oParameters.Add("@nNextActionContactID", oLineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@sNextActionCode", oLineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                oParameters.Add("@sNextActionDescription", oLineNextAction.NextActionDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                if (oLineNextAction.IsNullNextActionAmount == false)
                                {
                                    oParameters.Add("@dNextActionAmount", oLineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                }
                                else
                                {
                                    oParameters.Add("@dNextActionAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                }

                                oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                // ----------------------------------------
                                // Parameters added - Pankaj bedse 29012010
                                oParameters.Add("@nCloseDate", oLineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nUserID", oLineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@sUserName", oLineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),
                                // ----------------------------------------

                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "BL_UP_EOBParty";

                                _result = _sqlCommand.ExecuteNonQuery();

                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                else
                                { _retVal = 0; }

                                if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); }
                                
                            }


                            //if (oLineNextAction != null) { oLineNextAction.Dispose(); }
                        }

                        _sqlTransaction.Commit();
                        _sqlConnection.Close();
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    _sqlTransaction.Rollback();
                    _sqlConnection.Close(); ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    _sqlTransaction.Rollback();
                    _sqlConnection.Close(); gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (_retVal != null) { _retVal = null; }

                    if (_sqlConnection != null) { _sqlConnection.Dispose(); _sqlConnection = null; }
                    if (_sqlCommand != null) { _sqlCommand.Parameters.Clear(); _sqlCommand.Dispose(); _sqlCommand = null; }
                    if (_sqlTransaction != null) { _sqlTransaction.Dispose(); _sqlTransaction = null; }

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }
            }

            public Int64 UpdateEOB(EOBPayment.Common.PaymentInsurance EOBPaymentInsurance, out EOBPayment.Common.EOBInsurancePaymentDetails EOBInsurancePaymentMasterLines)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                object _retVal = null;
                object _valRet = null;
                Int64 _EOBPayId = 0;
                Int64 _EOBId = 0;
                Int64 _EOBDtlId = 0;
                Int64 _EOBPayDtlId = 0;
                bool _UseExtEOBID = false;
                EOBPayment.Common.PaymentInsuranceLine PaymentInsClaimLine = null;
                EOBPayment.Common.EOBInsurancePaymentDetail EOBInsPayDtl = null;
                EOBInsurancePaymentMasterLines = null;

                try
                {
                    if (EOBPaymentInsurance != null)
                    {
                        #region " Master Data Save "

                        //nEOBPaymentID,nEOBRefNO,sPayerName,nPayerID,nPayerType,nPaymentMode,sCheckNumber,nCheckAmount,nCheckDate
                        //nMSTAccountID,nMSTAccountType,nPaymentTrayID,sPaymentTrayName,nCloseDate,sCardType,sCardSecurityNo
                        //nCardID,nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                        _EOBPayId = 0;
                        oParameters.Clear();
                        oParameters.Add("@nEOBPaymentID", EOBPaymentInsurance.EOBPaymentID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nEOBRefNO", EOBPaymentInsurance.EOBRefNO, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sPayerName", EOBPaymentInsurance.PayerName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Unchecked
                        oParameters.Add("@nPayerID", EOBPaymentInsurance.PayerID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nPayerType", EOBPaymentInsurance.PayerType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nPaymentMode", EOBPaymentInsurance.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@sCheckNumber", EOBPaymentInsurance.CheckNumber, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Unchecked
                        oParameters.Add("@nCheckAmount", EOBPaymentInsurance.CheckAmount, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nCheckDate", EOBPaymentInsurance.CheckDate, ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nMSTAccountID", EOBPaymentInsurance.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nMSTAccountType", EOBPaymentInsurance.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked

                        oParameters.Add("@nPaymentTrayID", EOBPaymentInsurance.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sPaymentTrayCode", EOBPaymentInsurance.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                        oParameters.Add("@sPaymentTrayDescription", EOBPaymentInsurance.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255

                        oParameters.Add("@nCloseDate", EOBPaymentInsurance.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sCardType", EOBPaymentInsurance.CardType, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sCardSecurityNo", EOBPaymentInsurance.CardSecurityNo, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@nCardID", EOBPaymentInsurance.CardID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked

                        oParameters.Add("@nUserID", EOBPaymentInsurance.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sUserName", EOBPaymentInsurance.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtCreatedDateTime", EOBPaymentInsurance.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtModifiedDateTime", EOBPaymentInsurance.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked

                        oParameters.Add("@nClinicID", EOBPaymentInsurance.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                        //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding the PAF values 
                        oParameters.Add("@nPAccountID", EOBPaymentInsurance.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nAccountPatientID", EOBPaymentInsurance.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nGuarantorID", EOBPaymentInsurance.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                        //End

                        oDB.Connect(false);
                        oDB.Execute("BL_INUP_EOBPayment_MST", oParameters, out _retVal);
                        oDB.Disconnect();

                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                        { _EOBPayId = Convert.ToInt64(_retVal); }

                        #endregion " Master Data Save "

                        #region " EOB Data Save "

                        if (EOBPaymentInsurance.InsuranceClaims != null && EOBPaymentInsurance.InsuranceClaims.Count > 0)
                        {

                            for (int clmIndex = 0; clmIndex < EOBPaymentInsurance.InsuranceClaims.Count; clmIndex++)
                            {

                                if (_EOBPayId > 0 && EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines != null && EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines.Count > 0)
                                {
                                    for (int clmLnIndex = 0; clmLnIndex < EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines.Count; clmLnIndex++)
                                    {
                                        if (EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex] != null)
                                        {
                                            _EOBDtlId = 0;
                                            PaymentInsClaimLine = EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex];

                                            string _sqlQuery = "";

                                            if (clmLnIndex == 0)
                                            {
                                                #region " Delete existing entry for modify "

                                                _sqlQuery =
                                                " DELETE FROM BL_EOBPayment_DTL WITH (READPAST) " +
                                                " WHERE nEOBPaymentID = " + EOBPaymentInsurance.EOBPaymentID + " " +
                                                    //" AND nBillingTransactionID = " + PaymentInsClaimLine.BLTransactionID + " " +
                                                " AND nEOBID = " + PaymentInsClaimLine.mEOBID + " " +
                                                " AND nClinicID = " + _clinicId + " ";

                                                oDB.Connect(false);
                                                oDB.Execute_Query(_sqlQuery);
                                                oDB.Disconnect();

                                                #endregion " Delete existing entry for modify "
                                            }

                                            //nEOBPaymentID,nEOBID,nEOBPaymentDetailID,nBillingTransactionID,nBillingTransactionDetailID
                                            //nBillingTransactionLineNo,nPatientID,nDOSFrom,nDOSTo,sCPTCode,sCPTDescription,nAmount,
                                            //nPaymentType,nPaymentSubType,nPaySign,nRefEOBPaymentID,nRefEOBPaymentDetailID,nAccountID
                                            //nAccountType,nMSTAccountID,nMSTAccountType,nPaymentTrayID,nPaymentTrayCode,nPaymentTrayDescription
                                            //nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                                            oParameters.Clear();

                                            #region " Delete existing entry for modify "

                                            _sqlQuery = "";

                                            _sqlQuery = "DELETE FROM  BL_EOBPayment_EOB WITH (READPAST) " +
                                            " WHERE nEOBPaymentID = " + EOBPaymentInsurance.EOBPaymentID + " AND nEOBID = " + PaymentInsClaimLine.mEOBID + " " +
                                            " AND nClaimNo = " + PaymentInsClaimLine.ClaimNumber + " AND nPatientID = " + PaymentInsClaimLine.PatientID + " " +
                                            " AND nClinicID = " + _clinicId + "";

                                            oDB.Connect(false);
                                            oDB.Execute_Query(_sqlQuery);
                                            oDB.Disconnect();

                                            #endregion " Delete existing entry for modify "


                                            #region "EOB Service Line"
                                            if (_UseExtEOBID == true) { PaymentInsClaimLine.mEOBID = _EOBId; }
                                            oParameters.Add("@nEOBID", PaymentInsClaimLine.mEOBID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                            oParameters.Add("@nEOBDtlID", PaymentInsClaimLine.mEOBDtlID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0)
                                            //oParameters.Add("@nClaimNo", PaymentInsClaimLine.ClaimNumber, ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nClaimNo", PaymentInsClaimLine.ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                            oParameters.Add("@nDOSFrom", PaymentInsClaimLine.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nDOSTo", PaymentInsClaimLine.DOSTo, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                            oParameters.Add("@sCPTCode", PaymentInsClaimLine.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                            oParameters.Add("@sCPTDescription", PaymentInsClaimLine.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                            oParameters.Add("@dCharges", PaymentInsClaimLine.Charges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dUnit", PaymentInsClaimLine.Unit, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                            oParameters.Add("@dTotalCharges", PaymentInsClaimLine.TotalCharges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dAllowed", PaymentInsClaimLine.Allowed, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dWriteOff", PaymentInsClaimLine.WriteOff, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dNotCovered", PaymentInsClaimLine.NonCovered, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dPayment", PaymentInsClaimLine.InsuranceAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dCopay", PaymentInsClaimLine.Copay, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dDeductible", PaymentInsClaimLine.Deductible, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dCoInsurance", PaymentInsClaimLine.CoInsurance, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                            oParameters.Add("@dWithhold", PaymentInsClaimLine.Withhold, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)

                                            oParameters.Add("@nPaymentTrayID", PaymentInsClaimLine.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                            oParameters.Add("@sPaymentTrayCode", PaymentInsClaimLine.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                                            oParameters.Add("@sPaymentTrayDescription", PaymentInsClaimLine.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked

                                            oParameters.Add("@nUserID", PaymentInsClaimLine.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                            oParameters.Add("@sUserName", PaymentInsClaimLine.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked
                                            oParameters.Add("@dtCreatedDateTime", PaymentInsClaimLine.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked
                                            oParameters.Add("@dtModifiedDateTime", PaymentInsClaimLine.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked

                                            oParameters.Add("@nPatientID", PaymentInsClaimLine.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                            oParameters.Add("@nInsuraceID", PaymentInsClaimLine.PatInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                            oParameters.Add("@nContactID", PaymentInsClaimLine.InsContactID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked

                                            oParameters.Add("@nClinicID", PaymentInsClaimLine.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@bUseExtEOBID", _UseExtEOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                            oParameters.Add("@nEOBType", PaymentInsClaimLine.EOBType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);// int,
                                            oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.BLTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                            oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.BLTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                            oParameters.Add("@nBillingTransactionLineNo", PaymentInsClaimLine.BLTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)

                                            if (_UseExtEOBID == false) { _UseExtEOBID = true; }
                                            _retVal = null;
                                            _valRet = null;

                                            oDB.Connect(false);
                                            //oDB.Execute("BL_INSERT_EOBPayment_EOB", oParameters, out _retVal);
                                            oDB.Execute("BL_INSERT_EOBPayment_EOB", oParameters, out _retVal, out _valRet);
                                            oDB.Disconnect();

                                            if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                            { _EOBId = Convert.ToInt64(_retVal); }

                                            if (_valRet != null && Convert.ToString(_valRet).Trim() != "")
                                            { _EOBDtlId = Convert.ToInt64(_valRet); }

                                            #region " Add Line NextAction & Party Info "

                                            #region " Delete existing entry for modify "

                                            _sqlQuery = "";

                                            _sqlQuery =
                                            " DELETE FROM BL_EOB_NextAction WITH (READPAST) " +
                                            " WHERE nEOBPaymentID = " + EOBPaymentInsurance.EOBPaymentID + " AND nEOBID = " + PaymentInsClaimLine.mEOBID + " " +
                                            " AND nClaimNo = " + PaymentInsClaimLine.ClaimNumber + " AND nBillingTransactionID = " + PaymentInsClaimLine.BLTransactionID + " " +
                                            " AND nBillingTransactionDetailID = " + PaymentInsClaimLine.BLTransactionDetailID + " " +
                                            " AND nClinicID = " + _clinicId + " ";

                                            oDB.Connect(false);
                                            oDB.Execute_Query(_sqlQuery);
                                            oDB.Disconnect();

                                            #endregion " Delete existing entry for modify "


                                            if (PaymentInsClaimLine.LineNextAction != null && PaymentInsClaimLine.LineNextAction.HasData == true)
                                            {
                                                Object _nextActRetVal = null;

                                                oParameters.Clear();

                                                oParameters.Add("@nID", PaymentInsClaimLine.LineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                                oParameters.Add("@nClaimNo", PaymentInsClaimLine.LineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentDetailID", PaymentInsClaimLine.LineNextAction.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.LineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.LineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                                oParameters.Add("@nNextActionPatientInsID", PaymentInsClaimLine.LineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                                                oParameters.Add("@nNextActionPatientInsName", PaymentInsClaimLine.LineNextAction.NextActionPatientInsName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                oParameters.Add("@nNextActionPartyNumber", PaymentInsClaimLine.LineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                                                oParameters.Add("@nNextActionContactID", PaymentInsClaimLine.LineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sNextActionCode", PaymentInsClaimLine.LineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                oParameters.Add("@sNextActionDescription", PaymentInsClaimLine.LineNextAction.NextActionDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                oParameters.Add("@dNextActionAmount", PaymentInsClaimLine.LineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),

                                                oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                                // ----------------------------------------
                                                // Parameters added - Pankaj bedse 29012010
                                                oParameters.Add("@nCloseDate", PaymentInsClaimLine.LineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nUserID", PaymentInsClaimLine.LineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sUserName", PaymentInsClaimLine.LineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),
                                                // ----------------------------------------

                                                oDB.Connect(false);
                                                oDB.Execute("BL_INUP_EOBNextActionParty", oParameters, out _nextActRetVal);
                                                oDB.Disconnect();

                                            }

                                            #endregion " Add Line NextAction & Party Info "

                                            #region " Add Line Reason Codes "

                                            #region " Delete existing entry for modify "

                                            _sqlQuery = "";

                                            _sqlQuery =
                                            " DELETE FROM BL_EOB_ReasonCodes WITH (READPAST) " +
                                            " WHERE nEOBPaymentID = " + EOBPaymentInsurance.EOBPaymentID + " AND nEOBID = " + PaymentInsClaimLine.mEOBID + " " +
                                            " AND nClaimNo = " + PaymentInsClaimLine.ClaimNumber + " AND nBillingTransactionID = " + PaymentInsClaimLine.BLTransactionID + " " +
                                            " AND nBillingTransactionDetailID = " + PaymentInsClaimLine.BLTransactionDetailID + " " +
                                            " AND nClinicID = " + _clinicId + " ";

                                            oDB.Connect(false);
                                            oDB.Execute_Query(_sqlQuery);
                                            oDB.Disconnect();

                                            #endregion " Delete existing entry for modify "

                                            if (PaymentInsClaimLine.LineResonCodes != null && PaymentInsClaimLine.LineResonCodes.Count > 0)
                                            {
                                                Object _RcValue = null;

                                                for (int rcInd = 0; rcInd < PaymentInsClaimLine.LineResonCodes.Count; rcInd++)
                                                {
                                                    _RcValue = null;
                                                    oParameters.Clear();

                                                    oParameters.Add("@nID", PaymentInsClaimLine.LineResonCodes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                                    oParameters.Add("@nClaimNo", PaymentInsClaimLine.LineResonCodes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nEOBPaymentDetailID", PaymentInsClaimLine.LineResonCodes[rcInd].EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.LineResonCodes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                    oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.LineResonCodes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@sReasonCode", PaymentInsClaimLine.LineResonCodes[rcInd].ReasonCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                    oParameters.Add("@sReasonDescription", PaymentInsClaimLine.LineResonCodes[rcInd].ReasonDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                    oParameters.Add("@dReasonAmount", PaymentInsClaimLine.LineResonCodes[rcInd].ReasonAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                    oParameters.Add("@nType", EOBCommentType.Reason.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                                    oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                                    oDB.Connect(false);
                                                    oDB.Execute("BL_INUP_EOBReasonCode", oParameters, out _RcValue);
                                                    oDB.Disconnect();
                                                }
                                            }

                                            #endregion " Add Line Reason Codes "


                                            #endregion


                                            #region " EOB Financial Service Line Save "



                                            if (_EOBPayId > 0 && PaymentInsClaimLine.EOBInsurancePaymentLineDetails != null && PaymentInsClaimLine.EOBInsurancePaymentLineDetails.Count > 0)
                                            {
                                                for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < PaymentInsClaimLine.EOBInsurancePaymentLineDetails.Count; clmInsPayLnIndex++)
                                                {
                                                    if (PaymentInsClaimLine.EOBInsurancePaymentLineDetails[clmInsPayLnIndex] != null)
                                                    {
                                                        _EOBPayDtlId = 0;
                                                        EOBInsPayDtl = PaymentInsClaimLine.EOBInsurancePaymentLineDetails[clmInsPayLnIndex];

                                                        oParameters.Clear();
                                                        oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nEOBDtlID", _EOBDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nEOBPaymentDetailID", EOBInsPayDtl.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nBillingTransactionLineNo", EOBInsPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nPatientID", EOBInsPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nDOSFrom", EOBInsPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nDOSTo", EOBInsPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@sCPTCode", EOBInsPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                        oParameters.Add("@sCPTDescription", EOBInsPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                        oParameters.Add("@nAmount", EOBInsPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                        oParameters.Add("@nPaymentType", EOBInsPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPaymentSubType", EOBInsPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPaySign", EOBInsPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPayMode", EOBInsPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nRefEOBPaymentID", EOBInsPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nAccountID", EOBInsPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nAccountType", EOBInsPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nMSTAccountID", EOBInsPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nMSTAccountType", EOBInsPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPaymentTrayID", EOBInsPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@sPaymentTrayCode", EOBInsPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                        oParameters.Add("@sPaymentTrayDescription", EOBInsPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                        oParameters.Add("@nUserID", EOBInsPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@sUserName", EOBInsPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                        oParameters.Add("@dtCreatedDateTime", EOBInsPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                        oParameters.Add("@dtModifiedDateTime", EOBInsPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                        oParameters.Add("@nClinicID", EOBInsPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nResEOBPaymentID", EOBInsPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                        oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                                        _retVal = null;

                                                        oDB.Connect(false);
                                                        //oDB.Execute("BL_INSERT_EOBPayment_DTL", oParameters, out _retVal);
                                                        oDB.Execute("BL_INUP_EOBPayment_DTL", oParameters, out _retVal);
                                                        oDB.Disconnect();

                                                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                                        { _EOBPayDtlId = Convert.ToInt64(_retVal); }

                                                        EOBInsPayDtl = null;

                                                    }
                                                }
                                            }


                                            #endregion " EOB Financial Service Line Save "

                                            PaymentInsClaimLine = null;
                                        }
                                    }
                                }

                                #region "Payment Line Master (Debit) Entry, Total Check Amount Entry, but it is in same table "
                                if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails != null && EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count > 0)
                                {
                                    for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count; clmInsPayLnIndex++)
                                    {
                                        if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex] != null)
                                        {
                                            _EOBPayDtlId = 0;
                                            EOBInsPayDtl = EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex];

                                            oParameters.Clear();
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nEOBID", EOBInsPayDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nEOBDtlID", EOBInsPayDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nEOBPaymentDetailID", EOBInsPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nBillingTransactionLineNo", EOBInsPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nPatientID", EOBInsPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nDOSFrom", EOBInsPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nDOSTo", EOBInsPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@sCPTCode", EOBInsPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                            oParameters.Add("@sCPTDescription", EOBInsPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                            oParameters.Add("@nAmount", EOBInsPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@nPaymentType", EOBInsPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nPaymentSubType", EOBInsPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nPaySign", EOBInsPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nPayMode", EOBInsPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nRefEOBPaymentID", EOBInsPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nAccountID", EOBInsPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nAccountType", EOBInsPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nMSTAccountID", EOBInsPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nMSTAccountType", EOBInsPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nPaymentTrayID", EOBInsPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@sPaymentTrayCode", EOBInsPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                            oParameters.Add("@sPaymentTrayDescription", EOBInsPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                            oParameters.Add("@nUserID", EOBInsPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@sUserName", EOBInsPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                            oParameters.Add("@dtCreatedDateTime", EOBInsPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                            oParameters.Add("@dtModifiedDateTime", EOBInsPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                            oParameters.Add("@nClinicID", EOBInsPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nResEOBPaymentID", EOBInsPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            _retVal = null;

                                            oDB.Connect(false);
                                            oDB.Execute("BL_INUP_EOBPayment_DTL", oParameters, out _retVal);
                                            oDB.Disconnect();

                                            EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentID = _EOBPayId;
                                            EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentDetailID = Convert.ToInt64(_retVal.ToString());
                                            EOBInsPayDtl = null;
                                        }
                                    }
                                }
                                #endregion

                                EOBInsurancePaymentMasterLines = EOBPaymentInsurance.EOBInsurancePaymentLineDetails;

                            }//20091020 (for)

                        }//20091020 (If)

                        #endregion " EOB Data Save "
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                { ex.ERROR_Log(ex.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (_retVal != null) { _retVal = null; }
                }

                return _EOBPayId;
            }

            public Int64 SaveRemitEOB(EOBPayment.Common.PaymentInsurance EOBPaymentInsurance, out EOBPayment.Common.EOBInsurancePaymentDetails EOBInsurancePaymentMasterLines)
            {
                System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(_databaseConnectionString);
                System.Data.SqlClient.SqlCommand _sqlCommand = null;
                System.Data.SqlClient.SqlTransaction _sqlTransaction = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

                object _retVal = null;
                object _valRet = null;
                Int64 _EOBPayId = 0;
                Int64 _EOBId = 0;
                Int64 _EOBDtlId = 0;
                Int64 _EOBPayDtlId = 0;
                bool _UseExtEOBID = false;
                EOBPayment.Common.PaymentInsuranceLine PaymentInsClaimLine = null;
                EOBPayment.Common.EOBInsurancePaymentDetail EOBInsPayDtl = null;
                EOBInsurancePaymentMasterLines = null;
                int _result = 0;

                try
                {
                    if (EOBPaymentInsurance != null)
                    {
                        _sqlConnection.Open();
                        _sqlTransaction = _sqlConnection.BeginTransaction();

                        #region " Master Data Save "

                        //nEOBPaymentID,nEOBRefNO,sPayerName,nPayerID,nPayerType,nPaymentMode,sCheckNumber,nCheckAmount,nCheckDate
                        //nMSTAccountID,nMSTAccountType,nPaymentTrayID,sPaymentTrayName,nCloseDate,sCardType,sCardSecurityNo
                        //nCardID,nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                        _EOBPayId = 0;
                        oParameters.Clear();
                        oParameters.Add("@nEOBPaymentID", EOBPaymentInsurance.EOBPaymentID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nEOBRefNO", EOBPaymentInsurance.EOBRefNO, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sPayerName", EOBPaymentInsurance.PayerName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Unchecked
                        oParameters.Add("@nPayerID", EOBPaymentInsurance.PayerID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nPayerType", EOBPaymentInsurance.PayerType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nPaymentMode", EOBPaymentInsurance.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@sCheckNumber", EOBPaymentInsurance.CheckNumber, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Unchecked
                        oParameters.Add("@nCheckAmount", EOBPaymentInsurance.CheckAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nCheckDate", EOBPaymentInsurance.CheckDate, ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nMSTAccountID", EOBPaymentInsurance.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nMSTAccountType", EOBPaymentInsurance.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked

                        oParameters.Add("@nPaymentTrayID", EOBPaymentInsurance.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sPaymentTrayCode", EOBPaymentInsurance.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                        oParameters.Add("@sPaymentTrayDescription", EOBPaymentInsurance.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255

                        oParameters.Add("@nCloseDate", EOBPaymentInsurance.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sCardType", EOBPaymentInsurance.CardType, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sCardSecurityNo", EOBPaymentInsurance.CardSecurityNo, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@nCardID", EOBPaymentInsurance.CardID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked

                        oParameters.Add("@nUserID", EOBPaymentInsurance.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sUserName", EOBPaymentInsurance.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtCreatedDateTime", EOBPaymentInsurance.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtModifiedDateTime", EOBPaymentInsurance.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked

                        oParameters.Add("@nClinicID", EOBPaymentInsurance.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                        
                        //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding the PAF values 
                        oParameters.Add("@nPAccountID", EOBPaymentInsurance.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nAccountPatientID", EOBPaymentInsurance.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nGuarantorID", EOBPaymentInsurance.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                        //End

                        //oDB.Connect(false);
                        //oDB.Execute("BL_INUP_EOBPayment_MST", oParameters, out _retVal);
                        //oDB.Disconnect();

                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandText = "BL_INUP_EOBPayment_MST";

                        _result = _sqlCommand.ExecuteNonQuery();

                        if (_sqlCommand.Parameters["@nEOBPaymentID"].Value != null)
                        { _retVal = _sqlCommand.Parameters["@nEOBPaymentID"].Value; }
                        else
                        { _retVal = 0; }

                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                        { _EOBPayId = Convert.ToInt64(_retVal); }
                        _sqlCommand.Parameters.Clear();
                        _sqlCommand.Dispose();
                        _sqlCommand = null;
                        #endregion " Master Data Save "

                        #region " EOB Data Save "

                        if (EOBPaymentInsurance.InsuranceClaims != null && EOBPaymentInsurance.InsuranceClaims.Count > 0)
                        {

                            for (int clmIndex = 0; clmIndex < EOBPaymentInsurance.InsuranceClaims.Count; clmIndex++)
                            {
                                if (_EOBPayId > 0 && EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines != null && EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines.Count > 0)
                                {
                                    for (int clmLnIndex = 0; clmLnIndex < EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines.Count; clmLnIndex++)
                                    {

                                        if (EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex] != null)
                                        {
                                            _EOBDtlId = 0;
                                            PaymentInsClaimLine = EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex];

                                            //nEOBPaymentID,nEOBID,nEOBPaymentDetailID,nBillingTransactionID,nBillingTransactionDetailID
                                            //nBillingTransactionLineNo,nPatientID,nDOSFrom,nDOSTo,sCPTCode,sCPTDescription,nAmount,
                                            //nPaymentType,nPaymentSubType,nPaySign,nRefEOBPaymentID,nRefEOBPaymentDetailID,nAccountID
                                            //nAccountType,nMSTAccountID,nMSTAccountType,nPaymentTrayID,nPaymentTrayCode,nPaymentTrayDescription
                                            //nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                                            oParameters.Clear();

                                            #region "EOB Service Line"
                                            if (_UseExtEOBID == true) { PaymentInsClaimLine.mEOBID = _EOBId; }
                                            oParameters.Add("@nEOBID", PaymentInsClaimLine.mEOBID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                            oParameters.Add("@nEOBDtlID", PaymentInsClaimLine.mEOBDtlID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0)
                                            //oParameters.Add("@nClaimNo", PaymentInsClaimLine.ClaimNumber, ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nClaimNo", PaymentInsClaimLine.ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                            oParameters.Add("@nDOSFrom", PaymentInsClaimLine.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nDOSTo", PaymentInsClaimLine.DOSTo, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                            oParameters.Add("@sCPTCode", PaymentInsClaimLine.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                            oParameters.Add("@sCPTDescription", PaymentInsClaimLine.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                            oParameters.Add("@dCharges", PaymentInsClaimLine.Charges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dUnit", PaymentInsClaimLine.Unit, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                            oParameters.Add("@dTotalCharges", PaymentInsClaimLine.TotalCharges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dAllowed", PaymentInsClaimLine.Allowed, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dWriteOff", PaymentInsClaimLine.WriteOff, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dNotCovered", PaymentInsClaimLine.NonCovered, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dPayment", PaymentInsClaimLine.InsuranceAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dCopay", PaymentInsClaimLine.Copay, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dDeductible", PaymentInsClaimLine.Deductible, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@dCoInsurance", PaymentInsClaimLine.CoInsurance, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                            oParameters.Add("@dWithhold", PaymentInsClaimLine.Withhold, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)

                                            oParameters.Add("@nPaymentTrayID", PaymentInsClaimLine.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                            oParameters.Add("@sPaymentTrayCode", PaymentInsClaimLine.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                                            oParameters.Add("@sPaymentTrayDescription", PaymentInsClaimLine.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked

                                            oParameters.Add("@nUserID", PaymentInsClaimLine.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                            oParameters.Add("@sUserName", PaymentInsClaimLine.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked
                                            oParameters.Add("@dtCreatedDateTime", PaymentInsClaimLine.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked
                                            oParameters.Add("@dtModifiedDateTime", PaymentInsClaimLine.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked

                                            oParameters.Add("@nPatientID", PaymentInsClaimLine.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                            oParameters.Add("@nInsuraceID", PaymentInsClaimLine.PatInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                            oParameters.Add("@nContactID", PaymentInsClaimLine.InsContactID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked

                                            oParameters.Add("@nClinicID", PaymentInsClaimLine.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@bUseExtEOBID", _UseExtEOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                            oParameters.Add("@nEOBType", PaymentInsClaimLine.EOBType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);// int,
                                            oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.BLTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                            oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.BLTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                            oParameters.Add("@nBillingTransactionLineNo", PaymentInsClaimLine.BLTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)

                                            if (_UseExtEOBID == false) { _UseExtEOBID = true; }
                                            _retVal = null;
                                            _valRet = null;

                                            //oDB.Connect(false);
                                            //oDB.Execute("BL_INSERT_EOBPayment_EOB", oParameters, out _retVal, out _valRet);
                                            //oDB.Disconnect();

                                            _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                            _sqlCommand = oDB.GetCmdParameters(oParameters);
                                            _sqlCommand.Connection = _sqlConnection;
                                            _sqlCommand.Transaction = _sqlTransaction;
                                            _sqlCommand.CommandType = CommandType.StoredProcedure;
                                            _sqlCommand.CommandText = "BL_INSERT_EOBPayment_EOB";

                                            _result = _sqlCommand.ExecuteNonQuery();

                                            if (_sqlCommand.Parameters["@nEOBID"].Value != null)
                                            { _retVal = _sqlCommand.Parameters["@nEOBID"].Value; }
                                            else
                                            { _retVal = 0; }

                                            if (_sqlCommand.Parameters["@nEOBDtlID"].Value != null)
                                            { _valRet = _sqlCommand.Parameters["@nEOBDtlID"].Value; }
                                            else
                                            { _valRet = 0; }

                                            if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                            { _EOBId = Convert.ToInt64(_retVal); }

                                            if (_valRet != null && Convert.ToString(_valRet).Trim() != "")
                                            { _EOBDtlId = Convert.ToInt64(_valRet); }
                                            _sqlCommand.Parameters.Clear();
                                            _sqlCommand.Dispose();
                                            _sqlCommand = null;
                                            #region " Add Line NextAction & Party Info "

                                            if (PaymentInsClaimLine.LineNextAction != null && PaymentInsClaimLine.LineNextAction.HasData == true)
                                            {
                                               // Object _nextActRetVal = null;

                                                oParameters.Clear();

                                                oParameters.Add("@nID", PaymentInsClaimLine.LineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                                oParameters.Add("@nClaimNo", PaymentInsClaimLine.LineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentDetailID", PaymentInsClaimLine.LineNextAction.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.LineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.LineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                                oParameters.Add("@nNextActionPatientInsID", PaymentInsClaimLine.LineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                                                oParameters.Add("@nNextActionPatientInsName", PaymentInsClaimLine.LineNextAction.NextActionPatientInsName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                oParameters.Add("@nNextActionPartyNumber", PaymentInsClaimLine.LineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                                                oParameters.Add("@nNextActionContactID", PaymentInsClaimLine.LineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sNextActionCode", PaymentInsClaimLine.LineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                oParameters.Add("@sNextActionDescription", PaymentInsClaimLine.LineNextAction.NextActionDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                oParameters.Add("@dNextActionAmount", PaymentInsClaimLine.LineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),

                                                oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                                // ----------------------------------------
                                                // Parameters added - Pankaj bedse 29012010
                                                oParameters.Add("@nCloseDate", PaymentInsClaimLine.LineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nUserID", PaymentInsClaimLine.LineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sUserName", PaymentInsClaimLine.LineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),
                                                // ----------------------------------------

                                                //oDB.Connect(false);
                                                //oDB.Execute("BL_INUP_EOBNextActionParty", oParameters, out _nextActRetVal);
                                                //oDB.Disconnect();

                                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                _sqlCommand.Connection = _sqlConnection;
                                                _sqlCommand.Transaction = _sqlTransaction;
                                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                _sqlCommand.CommandText = "BL_INUP_EOBNextActionParty";

                                                _result = _sqlCommand.ExecuteNonQuery();

                                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                                { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                                else
                                                { _retVal = 0; }
                                                _sqlCommand.Parameters.Clear();
                                                _sqlCommand.Dispose();
                                                _sqlCommand = null;

                                            }

                                            #endregion " Add Line NextAction & Party Info "

                                            #region " Add Line Reason Codes "

                                            if (PaymentInsClaimLine.LineResonCodes != null && PaymentInsClaimLine.LineResonCodes.Count > 0)
                                            {
                                               // Object _RcValue = null;

                                                for (int rcInd = 0; rcInd < PaymentInsClaimLine.LineResonCodes.Count; rcInd++)
                                                {
                                                  //  _RcValue = null;
                                                    oParameters.Clear();

                                                    oParameters.Add("@nID", PaymentInsClaimLine.LineResonCodes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                                    oParameters.Add("@nClaimNo", PaymentInsClaimLine.LineResonCodes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nEOBPaymentDetailID", PaymentInsClaimLine.LineResonCodes[rcInd].EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.LineResonCodes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                    oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.LineResonCodes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@sReasonCode", PaymentInsClaimLine.LineResonCodes[rcInd].ReasonCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                    oParameters.Add("@sReasonDescription", PaymentInsClaimLine.LineResonCodes[rcInd].ReasonDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                    oParameters.Add("@dReasonAmount", PaymentInsClaimLine.LineResonCodes[rcInd].ReasonAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                    oParameters.Add("@nType", EOBCommentType.Reason.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                                    oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                                    //oDB.Connect(false);
                                                    //oDB.Execute("BL_INUP_EOBReasonCode", oParameters, out _RcValue);
                                                    //oDB.Disconnect();

                                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                    _sqlCommand.Connection = _sqlConnection;
                                                    _sqlCommand.Transaction = _sqlTransaction;
                                                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                    _sqlCommand.CommandText = "BL_INUP_EOBReasonCode";

                                                    _result = _sqlCommand.ExecuteNonQuery();

                                                    if (_sqlCommand.Parameters["@nID"].Value != null)
                                                    { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                                    else
                                                    { _retVal = 0; }
                                                    _sqlCommand.Parameters.Clear();
                                                    _sqlCommand.Dispose();
                                                    _sqlCommand = null;
                                                }
                                            }

                                            #endregion " Add Line Reason Codes "


                                            #endregion


                                            #region " EOB Financial Service Line Save "

                                            if (_EOBPayId > 0 && PaymentInsClaimLine.EOBInsurancePaymentLineDetails != null && PaymentInsClaimLine.EOBInsurancePaymentLineDetails.Count > 0)
                                            {
                                                for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < PaymentInsClaimLine.EOBInsurancePaymentLineDetails.Count; clmInsPayLnIndex++)
                                                {
                                                    if (PaymentInsClaimLine.EOBInsurancePaymentLineDetails[clmInsPayLnIndex] != null)
                                                    {
                                                        _EOBPayDtlId = 0;
                                                        EOBInsPayDtl = PaymentInsClaimLine.EOBInsurancePaymentLineDetails[clmInsPayLnIndex];

                                                        oParameters.Clear();
                                                        oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nEOBDtlID", _EOBDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nEOBPaymentDetailID", EOBInsPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nBillingTransactionLineNo", EOBInsPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nPatientID", EOBInsPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nDOSFrom", EOBInsPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nDOSTo", EOBInsPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@sCPTCode", EOBInsPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                        oParameters.Add("@sCPTDescription", EOBInsPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                        oParameters.Add("@nAmount", EOBInsPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                        oParameters.Add("@nPaymentType", EOBInsPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPaymentSubType", EOBInsPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPaySign", EOBInsPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPayMode", EOBInsPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nRefEOBPaymentID", EOBInsPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nAccountID", EOBInsPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nAccountType", EOBInsPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nMSTAccountID", EOBInsPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nMSTAccountType", EOBInsPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                        oParameters.Add("@nPaymentTrayID", EOBInsPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@sPaymentTrayCode", EOBInsPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                        oParameters.Add("@sPaymentTrayDescription", EOBInsPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                        oParameters.Add("@nUserID", EOBInsPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@sUserName", EOBInsPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                        oParameters.Add("@dtCreatedDateTime", EOBInsPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                        oParameters.Add("@dtModifiedDateTime", EOBInsPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                        oParameters.Add("@nClinicID", EOBInsPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                        oParameters.Add("@nResEOBPaymentID", EOBInsPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                        oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                        //oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                        //this above parameter need to implment or check when implement void with remittance

                                                        //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding the PAF values 
                                                        oParameters.Add("@nPAccountID", EOBInsPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                                        oParameters.Add("@nAccountPatientID", EOBInsPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                                        oParameters.Add("@nGuarantorID", EOBInsPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                                        //End

                                                        _retVal = null;

                                                        //oDB.Connect(false);
                                                        ////oDB.Execute("BL_INSERT_EOBPayment_DTL", oParameters, out _retVal);
                                                        //oDB.Execute("BL_INUP_EOBPayment_DTL", oParameters, out _retVal);
                                                        //oDB.Disconnect();

                                                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                        _sqlCommand.Connection = _sqlConnection;
                                                        _sqlCommand.Transaction = _sqlTransaction;
                                                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                        _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL";

                                                        _result = _sqlCommand.ExecuteNonQuery();

                                                        if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                                        { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                                        else
                                                        { _retVal = 0; }

                                                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                                        { _EOBPayDtlId = Convert.ToInt64(_retVal); }

                                                        EOBInsPayDtl = null;
                                                        _sqlCommand.Parameters.Clear();
                                                        _sqlCommand.Dispose();
                                                        _sqlCommand = null;
                                                    }
                                                }
                                            }


                                            #endregion " EOB Financial Service Line Save "

                                            PaymentInsClaimLine = null;
                                        }
                                    }
                                }

                                #region "Payment Line Master (Debit) Entry, Total Check Amount Entry, but it is in same table "
                                if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails != null && EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count > 0)
                                {
                                    for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count; clmInsPayLnIndex++)
                                    {
                                        if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex] != null)
                                        {
                                            _EOBPayDtlId = 0;
                                            EOBInsPayDtl = EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex];

                                            oParameters.Clear();
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nEOBID", EOBInsPayDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nEOBDtlID", EOBInsPayDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nEOBPaymentDetailID", EOBInsPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nBillingTransactionLineNo", EOBInsPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nPatientID", EOBInsPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nDOSFrom", EOBInsPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nDOSTo", EOBInsPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@sCPTCode", EOBInsPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                            oParameters.Add("@sCPTDescription", EOBInsPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                            oParameters.Add("@nAmount", EOBInsPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                            oParameters.Add("@nPaymentType", EOBInsPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nPaymentSubType", EOBInsPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nPaySign", EOBInsPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nPayMode", EOBInsPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nRefEOBPaymentID", EOBInsPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nAccountID", EOBInsPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nAccountType", EOBInsPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nMSTAccountID", EOBInsPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nMSTAccountType", EOBInsPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                            oParameters.Add("@nPaymentTrayID", EOBInsPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@sPaymentTrayCode", EOBInsPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                            oParameters.Add("@sPaymentTrayDescription", EOBInsPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                            oParameters.Add("@nUserID", EOBInsPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@sUserName", EOBInsPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                            oParameters.Add("@dtCreatedDateTime", EOBInsPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                            oParameters.Add("@dtModifiedDateTime", EOBInsPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                            oParameters.Add("@nClinicID", EOBInsPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nResEOBPaymentID", EOBInsPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            
                                            //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding the PAF values 
                                            oParameters.Add("@nPAccountID", EOBInsPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                            oParameters.Add("@nAccountPatientID", EOBInsPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                            oParameters.Add("@nGuarantorID", EOBInsPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                            //End

                                            //oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                            //check when implement remittance with void
                                            _retVal = null;

                                            //oDB.Connect(false);
                                            //oDB.Execute("BL_INUP_EOBPayment_DTL", oParameters, out _retVal);
                                            //oDB.Disconnect();

                                            _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                            _sqlCommand = oDB.GetCmdParameters(oParameters);
                                            _sqlCommand.Connection = _sqlConnection;
                                            _sqlCommand.Transaction = _sqlTransaction;
                                            _sqlCommand.CommandType = CommandType.StoredProcedure;
                                            _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL";

                                            _result = _sqlCommand.ExecuteNonQuery();

                                            if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                            { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                            else
                                            { _retVal = 0; }
                                            _sqlCommand.Parameters.Clear();
                                            _sqlCommand.Dispose();
                                            _sqlCommand = null;
                                            EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentID = _EOBPayId;
                                            EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentDetailID = Convert.ToInt64(_retVal.ToString());
                                            EOBInsPayDtl = null;
                                        }
                                    }
                                }
                                #endregion

                                EOBInsurancePaymentMasterLines = EOBPaymentInsurance.EOBInsurancePaymentLineDetails;
                                gloEOBPaymentInsurance ogloEOBPaymentInsurance = new gloEOBPaymentInsurance(_databaseConnectionString);

                                //oDB.Connect(false);

                                string _sqlQuery = "UPDATE BL_Transaction_Remittance_Claims WITH (READPAST) SET bIsProcessedForPayment = 1 WHERE sClaimNumber = '" + ogloEOBPaymentInsurance.GetFormattedClaimPaymentNumber(EOBPaymentInsurance.InsuranceClaims[clmIndex].ClaimNo.ToString()) + "'";
                                ogloEOBPaymentInsurance.Dispose();
                                ogloEOBPaymentInsurance = null;
                                //oDB.Execute_Query(_sqlQuery);
                                //oDB.Disconnect();
                                //oDB.Dispose();

                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.Text;
                                _sqlCommand.CommandText = _sqlQuery;

                                _result = _sqlCommand.ExecuteNonQuery();
                                _sqlCommand.Parameters.Clear();
                                _sqlCommand.Dispose();
                                _sqlCommand = null;

                            }//20091020 (for) Claim

                        }//20091020 (If)

                        #endregion " EOB Data Save "

                        _sqlTransaction.Commit();
                        _sqlConnection.Close();
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                { ex.ERROR_Log(ex.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
                finally
                {
                    //if (oDB != null) { oDB.Dispose(); }
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (_retVal != null) { _retVal = null; }

                    if (_sqlConnection != null) { _sqlConnection.Dispose(); _sqlConnection = null; }
                    if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); _sqlCommand = null; }
                    if (_sqlTransaction != null) { _sqlTransaction.Dispose(); _sqlTransaction = null; }

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }

                return _EOBPayId;
            }

            public DataTable GetPendingChecks(bool ShowCompleted)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable _dtPendingChecks = null;

                try
                {
                    oDB.Connect(false);
                    oParameters.Add("@ShowCompleted", ShowCompleted, ParameterDirection.Input, SqlDbType.Bit);
                    oDB.Retrive("BL_SELECT_EOBPENDINGCHECKS", oParameters, out _dtPendingChecks);
                    //oDB.Retrive("BL_EOBPENDINGCHECKS", oParameters, out _dtPendingChecks);
                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    
                }

                return _dtPendingChecks;
            }

            public DataTable SplitGetPendingChecks_OLD(bool ShowCompleted)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable _dtPendingChecks = null;

                try
                {
                    oDB.Connect(false);
                    oParameters.Add("@ShowCompleted", ShowCompleted, ParameterDirection.Input, SqlDbType.Bit);
                    oDB.Retrive("BL_SELECT_EOBPENDINGCHECKS_RENEWED", oParameters, out _dtPendingChecks);
                    //oDB.Retrive("BL_SELECT_EOBPENDINGCHECKS_Split", oParameters, out _dtPendingChecks);

                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                   
                }

                return _dtPendingChecks;
            }

            public DataTable SplitGetPendingChecks(Int64 userId, Int64 CloseDate)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable _dtPendingChecks = null;

                try
                {
                    oDB.Connect(false);
                    oParameters.Add("@nUserID", userId, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                    oParameters.Add("@nCloseDate", CloseDate, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                    //oParameters.Add("@nPayerID", PayerId, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)

                    oDB.Retrive("BL_SELECT_EOBPENDINGCHECKS_RENEWED", oParameters, out _dtPendingChecks);
                    //oDB.Retrive("BL_SELECT_EOBPENDINGCHECKS_Split", oParameters, out _dtPendingChecks);

                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                     
                }

                return _dtPendingChecks;
            }

            public DataTable SplitGetPendingChecks()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable _dtPendingChecks = null;

                try
                {
                    oDB.Connect(false);
                    oDB.Retrive("BL_SELECT_PAYMENT_INS_PROTOTYPE", oParameters, out _dtPendingChecks);
                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    
                }

                return _dtPendingChecks;
            }

            public DataTable GetEOBPaymentDetails(Int64 EOBPaymentID, EOBPaymentSign EOBPaySign)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable _dtEOBPaymentDetails = null;

                try
                {
                    //.....*** Pass EOBPaymentSign zero to get all transactions
                    oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPaySign", EOBPaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Connect(false);
                    oDB.Retrive("BL_SELECT_EOBPayment_DTL", oParameters, out _dtEOBPaymentDetails);
                    oDB.Disconnect();

                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                }

                return _dtEOBPaymentDetails;
            }

            public bool GetEOBMasterCreditLineAndAllocationLines(Int64 PaymentID, Int64 ClinicID, out EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines EOBInsurancePaymentMasterLines, out EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines EOBInsurancePaymentMasterAllocationLines)
            {
                gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                DataTable oDataTable = null;
                bool _Result = true;

                EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines _EOBInsurancePaymentMasterLines = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines();
                EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines _EOBInsurancePaymentMasterAllocationLines = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines();

                try
                {
                    oDBParameters.Add("@nEOBPaymentID", PaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBLayer.Connect(false);
                    oDBLayer.Retrive("BL_SELECT_EOBInsCreditLineList_Tracking", oDBParameters, out oDataTable);
                    oDBLayer.Disconnect();

                    if (oDataTable != null && oDataTable.Rows.Count > 0)
                    {
                        for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                        {

                            //nEOBPaymentID, nEOBID, nEOBDtlID, nEOBPaymentDetailID, 
                            //nBillingTransactionID, nBillingTransactionDetailID, nBillingTransactionLineNo, 
                            //nPatientID, nDOSFrom, nDOSTo, sCPTCode, sCPTDescription, 
                            //nAmount, 
                            //nPaymentType, nPaymentSubType, nPaySign, nPayMode, 
                            //nRefEOBPaymentID, nRefEOBPaymentDetailID, 
                            //nAccountID, nAccountType, nMSTAccountID, nMSTAccountType, 
                            //nPaymentTrayID, sPaymentTrayCode, sPaymentTrayDescription, nUserID, sUserName, dtCreatedDateTime, dtModifiedDateTime, nClinicID, 
                            //nResEOBPaymentID, nResEOBPaymentDetailID, 
                            //dtDayClosedOn, nDayCloseUserID, sDayCloseUserName, bIsUpdated, bIsDayClosed, 
                            //nCreditLineID, nContactInsID

                            EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine oEOBInsPaymentCrLine = new EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine();
                            EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine oEOBInsPaymentCrAllocationLine = new EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine();

                            #region "Credit Line"
                            oEOBInsPaymentCrLine.EOBPaymentID = Convert.ToInt64(oDataTable.Rows[i]["nEOBPaymentID"]);
                            oEOBInsPaymentCrLine.EOBID = Convert.ToInt64(oDataTable.Rows[i]["nEOBID"]);
                            oEOBInsPaymentCrLine.EOBDtlID = Convert.ToInt64(oDataTable.Rows[i]["nEOBDtlID"]);
                            oEOBInsPaymentCrLine.EOBPaymentDetailID = Convert.ToInt64(oDataTable.Rows[i]["nEOBPaymentDetailID"]);
                            oEOBInsPaymentCrLine.RefEOBPaymentID = Convert.ToInt64(oDataTable.Rows[i]["nRefEOBPaymentID"]);
                            oEOBInsPaymentCrLine.RefEOBPaymentDetailID = Convert.ToInt64(oDataTable.Rows[i]["nRefEOBPaymentDetailID"]);
                            oEOBInsPaymentCrLine.ReserveEOBPaymentID = Convert.ToInt64(oDataTable.Rows[i]["nResEOBPaymentID"]);
                            oEOBInsPaymentCrLine.ReserveEOBPaymentDetailID = Convert.ToInt64(oDataTable.Rows[i]["nResEOBPaymentDetailID"]);


                            oEOBInsPaymentCrLine.BillingTransactionID = Convert.ToInt64(oDataTable.Rows[i]["nBillingTransactionID"]);
                            oEOBInsPaymentCrLine.BillingTransactionDetailID = Convert.ToInt64(oDataTable.Rows[i]["nBillingTransactionDetailID"]);
                            oEOBInsPaymentCrLine.BillingTransactionLineNo = Convert.ToInt32(oDataTable.Rows[i]["nBillingTransactionLineNo"]);
                            oEOBInsPaymentCrLine.DOSFrom = Convert.ToInt64(oDataTable.Rows[i]["nDOSFrom"]);
                            oEOBInsPaymentCrLine.DOSTo = Convert.ToInt64(oDataTable.Rows[i]["nDOSTo"]);

                            oEOBInsPaymentCrLine.CPTCode = Convert.ToString(oDataTable.Rows[i]["sCPTCode"]);
                            oEOBInsPaymentCrLine.CPTDescription = Convert.ToString(oDataTable.Rows[i]["sCPTDescription"]);

                            oEOBInsPaymentCrLine.Amount = Convert.ToDecimal(oDataTable.Rows[i]["nAmount"]);
                            //Credit line we take all amount, but if we take remaining amt then we can change check amt,
                            //but lets check is it affecting anywhere or not
                            //oEOBInsPaymentCrLine.Amount = Convert.ToDecimal(oDataTable.Rows[i]["BalAmt"]);

                            oEOBInsPaymentCrLine.PaymentType = ((EOBPaymentType)Convert.ToInt32(oDataTable.Rows[i]["nPaymentType"]));
                            oEOBInsPaymentCrLine.PaymentSubType = ((EOBPaymentSubType)Convert.ToInt32(oDataTable.Rows[i]["nPaymentSubType"]));
                            oEOBInsPaymentCrLine.PaySign = ((EOBPaymentSign)Convert.ToInt32(oDataTable.Rows[i]["nPaySign"]));
                            oEOBInsPaymentCrLine.PayMode = ((EOBPaymentMode)Convert.ToInt32(oDataTable.Rows[i]["nPayMode"]));

                            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                            oEOBInsPaymentCrLine.AccountID = Convert.ToInt64(oDataTable.Rows[i]["nAccountID"]);
                            oEOBInsPaymentCrLine.AccountType = ((EOBPaymentAccountType)Convert.ToInt32(oDataTable.Rows[i]["nAccountType"]));
                            oEOBInsPaymentCrLine.MSTAccountID = Convert.ToInt64(oDataTable.Rows[i]["nMSTAccountID"]);
                            oEOBInsPaymentCrLine.MSTAccountType = ((EOBPaymentAccountType)Convert.ToInt32(oDataTable.Rows[i]["nMSTAccountType"]));
                            oEOBInsPaymentCrLine.ContactInsID = Convert.ToInt64(oDataTable.Rows[i]["nContactInsID"]);

                            oEOBInsPaymentCrLine.PatientID = Convert.ToInt64(oDataTable.Rows[i]["nPatientID"]);

                            oEOBInsPaymentCrLine.PaymentTrayID = Convert.ToInt64(oDataTable.Rows[i]["nPaymentTrayID"]);
                            oEOBInsPaymentCrLine.PaymentTrayCode = Convert.ToString(oDataTable.Rows[i]["sPaymentTrayCode"]);
                            oEOBInsPaymentCrLine.PaymentTrayDescription = Convert.ToString(oDataTable.Rows[i]["sPaymentTrayDescription"]);

                            oEOBInsPaymentCrLine.UserID = Convert.ToInt64(oDataTable.Rows[i]["nUserID"]);
                            oEOBInsPaymentCrLine.UserName = Convert.ToString(oDataTable.Rows[i]["sUserName"]);
                            oEOBInsPaymentCrLine.ClinicID = ClinicID;

                            oEOBInsPaymentCrLine.FinanceLieNo = i + 1;
                            oEOBInsPaymentCrLine.MainCreditLineID = Convert.ToInt64(oDataTable.Rows[i]["nCreditLineID"]);

                            oEOBInsPaymentCrLine.IsMainCreditLine = false;
                            oEOBInsPaymentCrLine.IsReserveCreditLine = false;
                            oEOBInsPaymentCrLine.IsCorrectionCreditLine = false;
                            if (Convert.ToInt32(oDataTable.Rows[i]["CreditLineType"]) == 1)
                            {
                                oEOBInsPaymentCrLine.IsMainCreditLine = true;
                            }
                            else if (Convert.ToInt32(oDataTable.Rows[i]["CreditLineType"]) == 2)
                            {
                                //oEOBInsPaymentCrLine.IsReserveCreditLine = true;
                                oEOBInsPaymentCrLine.IsCorrectionCreditLine = true;
                            }
                            else if (Convert.ToInt32(oDataTable.Rows[i]["CreditLineType"]) == 3)
                            {
                                //oEOBInsPaymentCrLine.IsCorrectionCreditLine = true;
                                oEOBInsPaymentCrLine.IsReserveCreditLine = true;
                                oEOBInsPaymentCrLine.DBReserveAmount = Convert.ToDecimal(oDataTable.Rows[i]["nAmount"]);
                            }

                            oEOBInsPaymentCrLine.RefFinanceLieNo = 0;
                            oEOBInsPaymentCrLine.UseRefFinanceLieNo = false;

                            #endregion

                            #region "Allocation Line"
                            oEOBInsPaymentCrAllocationLine.EOBPaymentID = Convert.ToInt64(oDataTable.Rows[i]["nEOBPaymentID"]);
                            oEOBInsPaymentCrAllocationLine.EOBID = Convert.ToInt64(oDataTable.Rows[i]["nEOBID"]);
                            oEOBInsPaymentCrAllocationLine.EOBDtlID = Convert.ToInt64(oDataTable.Rows[i]["nEOBDtlID"]);
                            oEOBInsPaymentCrAllocationLine.EOBPaymentDetailID = Convert.ToInt64(oDataTable.Rows[i]["nEOBPaymentDetailID"]);
                            oEOBInsPaymentCrAllocationLine.RefEOBPaymentID = Convert.ToInt64(oDataTable.Rows[i]["nRefEOBPaymentID"]);
                            oEOBInsPaymentCrAllocationLine.RefEOBPaymentDetailID = Convert.ToInt64(oDataTable.Rows[i]["nRefEOBPaymentDetailID"]);
                            oEOBInsPaymentCrAllocationLine.ReserveEOBPaymentID = Convert.ToInt64(oDataTable.Rows[i]["nResEOBPaymentID"]);
                            oEOBInsPaymentCrAllocationLine.ReserveEOBPaymentDetailID = Convert.ToInt64(oDataTable.Rows[i]["nResEOBPaymentDetailID"]);


                            oEOBInsPaymentCrAllocationLine.BillingTransactionID = Convert.ToInt64(oDataTable.Rows[i]["nBillingTransactionID"]);
                            oEOBInsPaymentCrAllocationLine.BillingTransactionDetailID = Convert.ToInt64(oDataTable.Rows[i]["nBillingTransactionDetailID"]);
                            oEOBInsPaymentCrAllocationLine.BillingTransactionLineNo = Convert.ToInt32(oDataTable.Rows[i]["nBillingTransactionLineNo"]);
                            oEOBInsPaymentCrAllocationLine.DOSFrom = Convert.ToInt64(oDataTable.Rows[i]["nDOSFrom"]);
                            oEOBInsPaymentCrAllocationLine.DOSTo = Convert.ToInt64(oDataTable.Rows[i]["nDOSTo"]);

                            oEOBInsPaymentCrAllocationLine.CPTCode = Convert.ToString(oDataTable.Rows[i]["sCPTCode"]);
                            oEOBInsPaymentCrAllocationLine.CPTDescription = Convert.ToString(oDataTable.Rows[i]["sCPTDescription"]);
                            oEOBInsPaymentCrAllocationLine.Amount = Convert.ToDecimal(oDataTable.Rows[i]["BalAmt"]);
                            oEOBInsPaymentCrAllocationLine.PaymentType = ((EOBPaymentType)Convert.ToInt32(oDataTable.Rows[i]["nPaymentType"]));
                            oEOBInsPaymentCrAllocationLine.PaymentSubType = ((EOBPaymentSubType)Convert.ToInt32(oDataTable.Rows[i]["nPaymentSubType"]));
                            oEOBInsPaymentCrAllocationLine.PaySign = ((EOBPaymentSign)Convert.ToInt32(oDataTable.Rows[i]["nPaySign"]));
                            oEOBInsPaymentCrAllocationLine.PayMode = ((EOBPaymentMode)Convert.ToInt32(oDataTable.Rows[i]["nPayMode"]));

                            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                            oEOBInsPaymentCrAllocationLine.AccountID = Convert.ToInt64(oDataTable.Rows[i]["nAccountID"]);
                            oEOBInsPaymentCrAllocationLine.AccountType = ((EOBPaymentAccountType)Convert.ToInt32(oDataTable.Rows[i]["nAccountType"]));
                            oEOBInsPaymentCrAllocationLine.MSTAccountID = Convert.ToInt64(oDataTable.Rows[i]["nMSTAccountID"]);
                            oEOBInsPaymentCrAllocationLine.MSTAccountType = ((EOBPaymentAccountType)Convert.ToInt32(oDataTable.Rows[i]["nMSTAccountType"]));
                            oEOBInsPaymentCrAllocationLine.ContactInsID = Convert.ToInt64(oDataTable.Rows[i]["nContactInsID"]);

                            oEOBInsPaymentCrAllocationLine.PatientID = Convert.ToInt64(oDataTable.Rows[i]["nPatientID"]);

                            oEOBInsPaymentCrAllocationLine.PaymentTrayID = Convert.ToInt64(oDataTable.Rows[i]["nPaymentTrayID"]);
                            oEOBInsPaymentCrAllocationLine.PaymentTrayCode = Convert.ToString(oDataTable.Rows[i]["sPaymentTrayCode"]);
                            oEOBInsPaymentCrAllocationLine.PaymentTrayDescription = Convert.ToString(oDataTable.Rows[i]["sPaymentTrayDescription"]);

                            oEOBInsPaymentCrAllocationLine.UserID = Convert.ToInt64(oDataTable.Rows[i]["nUserID"]);
                            oEOBInsPaymentCrAllocationLine.UserName = Convert.ToString(oDataTable.Rows[i]["sUserName"]);
                            oEOBInsPaymentCrAllocationLine.ClinicID = ClinicID;

                            oEOBInsPaymentCrAllocationLine.FinanceLieNo = i + 1;
                            oEOBInsPaymentCrAllocationLine.MainCreditLineID = Convert.ToInt64(oDataTable.Rows[i]["nCreditLineID"]);

                            oEOBInsPaymentCrAllocationLine.IsMainCreditLine = false;
                            oEOBInsPaymentCrAllocationLine.IsReserveCreditLine = false;
                            oEOBInsPaymentCrAllocationLine.IsCorrectionCreditLine = false;
                            if (Convert.ToInt32(oDataTable.Rows[i]["CreditLineType"]) == 1)
                            {
                                oEOBInsPaymentCrAllocationLine.IsMainCreditLine = true;
                            }
                            else if (Convert.ToInt32(oDataTable.Rows[i]["CreditLineType"]) == 2)
                            {
                                oEOBInsPaymentCrAllocationLine.IsReserveCreditLine = true;
                                oEOBInsPaymentCrAllocationLine.DBReserveAmount = Convert.ToDecimal(oDataTable.Rows[i]["BalAmt"]);
                            }
                            else if (Convert.ToInt32(oDataTable.Rows[i]["CreditLineType"]) == 3)
                            {
                                oEOBInsPaymentCrAllocationLine.IsCorrectionCreditLine = true;
                            }

                            oEOBInsPaymentCrAllocationLine.RefFinanceLieNo = 0;
                            oEOBInsPaymentCrAllocationLine.UseRefFinanceLieNo = false;

                            #endregion

                            _EOBInsurancePaymentMasterLines.Add(oEOBInsPaymentCrLine);
                            _EOBInsurancePaymentMasterAllocationLines.Add(oEOBInsPaymentCrAllocationLine);

                            //oEOBInsPaymentCrLine.Dispose();
                            //oEOBInsPaymentCrAllocationLine.Dispose();
                        }
                    }
                }
                catch //(Exception ex)
                {
                    _EOBInsurancePaymentMasterLines.Clear();
                    _EOBInsurancePaymentMasterAllocationLines.Clear();
                    _Result = false;
                }
                finally
                {
                    if (oDBLayer != null)
                    {
                        oDBLayer.Dispose();
                        oDBLayer = null;
                    }
                    if (oDBParameters != null)
                    {
                        oDBParameters.Dispose();
                        oDBParameters = null;
                    }
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                }


                EOBInsurancePaymentMasterLines = _EOBInsurancePaymentMasterLines;
                EOBInsurancePaymentMasterAllocationLines = _EOBInsurancePaymentMasterAllocationLines;
                return _Result;
            }

            public string GetPaymentActionStatus()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _payActionStatus = "";
                DataTable _dtPayActStatus = null;
                string _sqlQuery = "";
                string _conCodeDesc = "";

                try
                {
                    _sqlQuery = " SELECT ISNULL(nID,0) AS ID,ISNULL(sCode,'') AS Code, " +
                    " ISNULL(sDescription,'') AS Description, ISNULL(nIsSystem,'false') AS IsSystem, " +
                    " ISNULL(nIsBlock,'false') AS nIsBlock, ISNULL(nActionID,0) AS nActionID " +
                    " FROM BL_EOBPayment_ActionStatus WITH(NOLOCK) " +
                    " WHERE nID > 0 AND sCode IS NOT NULL AND sDescription IS NOT NULL AND nClinicID = " + _clinicId + " " +
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
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                    if (_dtPayActStatus != null) { _dtPayActStatus.Dispose(); _dtPayActStatus = null; }
                    if (_sqlQuery != null) { _sqlQuery = null; }
                }

                return _payActionStatus;
            }

            public string GetReasonCodes()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _payActionStatus = "";
                DataTable _dtPayActStatus = null;
                string _sqlQuery = "";
                string _conCodeDesc = "";

                try
                {
                    //_sqlQuery = " SELECT ISNULL(nID,0) AS ID,ISNULL(sCode,'') AS Code, " +
                    //" ISNULL(sDescription,'') AS Description, ISNULL(nIsSystem,'false') AS IsSystem, " +
                    //" ISNULL(nIsBlock,'false') AS nIsBlock, ISNULL(nActionID,0) AS nActionID " +
                    //" FROM BL_EOBPayment_ActionStatus " +
                    //" WHERE nID > 0 AND sCode IS NOT NULL AND sDescription IS NOT NULL AND nClinicID = " + _clinicId + " ";

                    _sqlQuery = " SELECT ISNULL(nReasonID,0) AS nReasonID, ISNULL(sCode,'') AS Code,ISNULL(sDescription,'') AS Description " +
                    " FROM  BL_ReasonCodes_MST  WITH(NOLOCK) WHERE ISNULL(bIsBlock,0) = 0 AND nClinicID = " + _clinicId + " ";


                    oDB.Connect(false);
                    oDB.Retrive_Query(_sqlQuery, out _dtPayActStatus);
                    oDB.Disconnect();

                    if (_dtPayActStatus != null && _dtPayActStatus.Rows.Count > 0)
                    {
                        _payActionStatus = "|";

                        for (int i = 0; i < _dtPayActStatus.Rows.Count; i++)
                        {
                            _conCodeDesc = "";

                            if (Convert.ToString(_dtPayActStatus.Rows[i]["Code"]).Trim() != "")
                            {
                                _conCodeDesc = Convert.ToString(_dtPayActStatus.Rows[i]["Code"]).Trim().ToUpper() + "|";
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
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                    if (_dtPayActStatus != null) { _dtPayActStatus.Dispose(); _dtPayActStatus = null; }
                    if (_sqlQuery != null) { _sqlQuery = null; }
                }

                return _payActionStatus;
            }
            // Code added for Remark code association - 8060
            public string GetRemarkCodes()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _RemarkCodes = "";
                DataTable _dtRemarkCodes = null;
                string _sqlQuery = "";
                string _conCodeDesc = "";

                try
                {
                    _sqlQuery = " SELECT nRemarkID, sRemarkCode, sRemarkDescription FROM BL_RemarkCodes_MST  WITH(NOLOCK) WHERE ISNULL(bIsBlock,0) = 0 AND nClinicID = " + _clinicId + " ";
                    
                    oDB.Connect(false);

                    oDB.Retrive_Query(_sqlQuery, out _dtRemarkCodes);
                    oDB.Disconnect();

                    if (_RemarkCodes != null && _dtRemarkCodes.Rows.Count > 0)
                    {
                        _RemarkCodes = "|";

                        for (int i = 0; i < _dtRemarkCodes.Rows.Count; i++)
                        {
                            _conCodeDesc = "";

                            if (Convert.ToString(_dtRemarkCodes.Rows[i]["sRemarkCode"]).Trim() != "")
                            {
                                _conCodeDesc = Convert.ToString(_dtRemarkCodes.Rows[i]["sRemarkCode"]).Trim().ToUpper() + "|";
                                _RemarkCodes += _conCodeDesc;
                            }
                        }

                        _RemarkCodes = _RemarkCodes.TrimEnd('|');
                    }

                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
                finally
                {
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                    if (_dtRemarkCodes != null) { _dtRemarkCodes.Dispose(); _dtRemarkCodes = null; }
                    if (_sqlQuery != null) { _sqlQuery = null; }
                }

                return _RemarkCodes;
            }

            public string GetReasonDescription(string Code)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                DataTable _dtPayActStatus = null;
                string _sqlQuery = "";
                string _conCodeDesc = "";

                try
                {

                    _sqlQuery = " SELECT ISNULL(sDescription,'') AS Description from  BL_ReasonCodes_MST WITH(NOLOCK) " +
                                    "  WHERE ISNULL(bIsBlock,0) = 0 " +
                                    "  AND nClinicID = " + _clinicId + " AND UPPER(sCode) = '" + Code.Trim().ToUpper() + "'";


                    oDB.Connect(false);
                    oDB.Retrive_Query(_sqlQuery, out _dtPayActStatus);
                    oDB.Disconnect();

                    if (_dtPayActStatus != null && _dtPayActStatus.Rows.Count > 0)
                    {
                        _conCodeDesc = Convert.ToString(_dtPayActStatus.Rows[0]["Description"]);
                    }

                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
                finally
                {
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                    if (_dtPayActStatus != null) { _dtPayActStatus.Dispose(); _dtPayActStatus = null; }
                    if (_sqlQuery != null) { _sqlQuery = null; }
                }

                return _conCodeDesc;
            }

            public string GetPaymentActionStatus(string Code)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _payActionStatus = "";
                DataTable _dtPayActStatus = null;
                string _sqlQuery = "";
                string _conCodeDesc = "";

                try
                {
                    _sqlQuery = " SELECT ISNULL(nID,0) AS ID,ISNULL(sCode,'') AS Code, " +
                    " ISNULL(sDescription,'') AS Description, ISNULL(nIsSystem,'false') AS IsSystem, " +
                    " ISNULL(nIsBlock,'false') AS nIsBlock, ISNULL(nActionID,0) AS nActionID " +
                    " FROM BL_EOBPayment_ActionStatus WITH(NOLOCK) " +
                    " WHERE nID > 0 AND sCode IS NOT NULL AND sDescription IS NOT NULL AND sCode = '" + Code + "'  AND nClinicID = " + _clinicId + " ";

                    oDB.Connect(false);
                    oDB.Retrive_Query(_sqlQuery, out _dtPayActStatus);
                    oDB.Disconnect();

                    if (_dtPayActStatus != null && _dtPayActStatus.Rows.Count > 0)
                    {

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
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                    if (_dtPayActStatus != null) { _dtPayActStatus.Dispose(); _dtPayActStatus = null; }
                    if (_sqlQuery != null) { _sqlQuery = null; }
                }

                return _payActionStatus;
            }

            public string GetPaymentParty(Int64 PatientId)
            {
                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseConnectionString);
                string _payPartyCode = "";
                DataTable _dtPayPartyCode = null;
                string _conCodeDesc = "";

                try
                {

                    _dtPayPartyCode = ogloPatient.getPatientInsurances(PatientId);

                    if (_dtPayPartyCode != null && _dtPayPartyCode.Rows.Count > 0)
                    {
                        _payPartyCode = "|";

                        for (int i = 0; i < _dtPayPartyCode.Rows.Count; i++)
                        {
                            _conCodeDesc = "";

                            if (Convert.ToString(_dtPayPartyCode.Rows[i]["nInsuranceFlag"]).Trim() != "" && Convert.ToString(_dtPayPartyCode.Rows[i]["InsuranceName"]).Trim() != "")
                            {
                                _conCodeDesc = Convert.ToString(_dtPayPartyCode.Rows[i]["nInsuranceFlag"]).Trim().ToUpper() + "-" + Convert.ToString(_dtPayPartyCode.Rows[i]["InsuranceName"]).Trim().ToUpper() + "|";
                                _payPartyCode += _conCodeDesc;
                            }
                        }
                        _payPartyCode += "0" + "-" + "Self" + "|";
                        _payPartyCode = _payPartyCode.TrimEnd('|');
                    }

                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
                finally
                {
                    if (ogloPatient != null) { ogloPatient.Dispose(); ogloPatient = null; }
                    if (_dtPayPartyCode != null) { _dtPayPartyCode.Dispose(); _dtPayPartyCode = null; }
                }

                return _payPartyCode;
            }

            public string GetClaimParties(Int64 ClaimNo, Int64 PatientId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable _dtClaimInsurances = null;
                string _payPartyCode = "";
                string _conCodeDesc = "";

                try
                {
                    if (ClaimNo > 0 && PatientId > 0)
                    {
                        oDB.Connect(false);
                        oParameters.Clear();
                        oParameters.Add("@nPatientID", PatientId, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                        oParameters.Add("@nClaimNo", ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                        oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
                        oDB.Retrive("BL_SELECT_CLAIM_INSURANCES", oParameters, out _dtClaimInsurances);
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
                                        && Convert.ToInt32(_dtClaimInsurances.Rows[i]["nResponsibilityType"]) == PayerMode.Insurance.GetHashCode())
                                    {
                                        _conCodeDesc = Convert.ToString(_dtClaimInsurances.Rows[i]["nResponsibilityNo"]).Trim().ToUpper() + "-" + Convert.ToString(_dtClaimInsurances.Rows[i]["InsuranceName"]).Trim().ToUpper() + "|";
                                        _payPartyCode += _conCodeDesc;
                                    }
                                    else if (Convert.ToString(_dtClaimInsurances.Rows[i]["nResponsibilityType"]).Trim() != ""
                                        && Convert.ToInt32(_dtClaimInsurances.Rows[i]["nResponsibilityType"]) == PayerMode.Self.GetHashCode())
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
                {
                    MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (_dtClaimInsurances != null) { _dtClaimInsurances.Dispose(); _dtClaimInsurances = null; }
                }

                return _payPartyCode;
            }

            public DataTable GetClaimInsurances(Int64 ClaimNo, Int64 PatientId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable _dtClaimInsurances = null;

                try
                {
                    if (ClaimNo > 0 && PatientId > 0)
                    {
                        oDB.Connect(false);
                        oParameters.Clear();
                        oParameters.Add("@nPatientID", PatientId, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                        oParameters.Add("@nClaimNo", ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                        oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
                        oDB.Retrive("BL_SELECT_CLAIM_INSURANCES", oParameters, out _dtClaimInsurances);
                        oDB.Disconnect();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                }

                return _dtClaimInsurances;
            }

            public DataTable GetPaymentParty(Int64 EOBPaymentId, Int64 EOBId, Int64 ClaimNo, Int64 BillingTranId, Int64 BillingTranDtlId, out string LineParty, out string LineAction)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _sqlQuery = "";
                DataTable _dt = null;
                string _lineParty = "";
                string _lineAction = "";

                try
                {
                    _sqlQuery =
                    " SELECT nNextActionPatientInsID, ISNULL(nNextActionPatientInsName,0) AS nNextActionPatientInsName,ISNULL(nNextActionPartyNumber,0) AS nNextActionPartyNumber, " +
                    " nNextActionContactID,ISNULL(sNextActionCode,'') AS sNextActionCode,ISNULL(sNextActionDescription,'') AS sNextActionDescription, dNextActionAmount " +
                    " FROM BL_EOB_NextAction WITH(NOLOCK) " +
                    " WHERE nClaimNo = " + ClaimNo + " AND nEOBPaymentID = " + EOBPaymentId + " AND nEOBID = " + EOBId + " AND " +
                    " nBillingTransactionID = " + BillingTranId + " AND nBillingTransactionDetailID = " + BillingTranDtlId + "" +
                    " AND nClinicID = " + _clinicId + " ";

                    oDB.Connect(false);
                    oDB.Retrive_Query(_sqlQuery, out _dt);
                    oDB.Disconnect();

                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        _lineParty = Convert.ToString(_dt.Rows[0]["nNextActionPartyNumber"]).Trim().ToUpper() + "-" + Convert.ToString(_dt.Rows[0]["nNextActionPatientInsName"]).Trim().ToUpper();
                        _lineAction = Convert.ToString(_dt.Rows[0]["sNextActionCode"]).Trim().ToUpper() + "-" + Convert.ToString(_dt.Rows[0]["sNextActionDescription"]).Trim().ToUpper();
                    }
                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                   
                }

                LineParty = _lineParty;
                LineAction = _lineAction;
                return _dt;

            }

            public string GetPaymentPrefixNumber(string Prefix)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                Object _retVal = null;
                string _sqlQuery = "";
                Int64 _paymentMaxNo = 0;
                string NumberSize = "";

                try
                {
                    oDB.Connect(false);

                    _sqlQuery = " SELECT isnull(max(CONVERT(NUMERIC,sPaymentNo)),0)+ 1  " +
                     " FROM BL_EOBPayment_MST WITH (NOLOCK) where  sPaymentNoPrefix = '" + Prefix + "' AND nClinicID = " + _clinicId + " ";

                    //_sqlQuery = " SELECT ISNULL(MAX(CONVERT(numeric,ISNULL(sPaymentNo,0)) + 1),1) AS PaymentNo  FROM BL_EOBPayment_MST " +
                    //" where sPaymentNoPrefix = '" + Prefix + "' AND nClinicID = " + _clinicId + " ";

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
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                }

                NumberSize = Prefix + _paymentMaxNo.ToString();

                return NumberSize;
            }

            // Not in use, replaced by a new function in the Insurance Payment Class with same name
            public bool IsExistCheck(string CheckNumber, Int64 CheckDate, decimal CheckAmt)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                Object _retVal = false;
                string _sqlQuery = "";
                bool _IsExistCheck = false;

                try
                {
                    if (CheckNumber.ToString().Trim() != "")
                    {
                        oDB.Connect(false);
                        _sqlQuery = "SELECT COUNT(*) FROM BL_EOBPayment_MST WITH(NOLOCK) " +
                        " WHERE UPPER(sCheckNumber) = '" + CheckNumber.Trim().ToUpper().Replace("'", "''") + "' " +
                        " AND nCheckDate = " + CheckDate + " AND nCheckAmount = " + CheckAmt + " " +
                        " AND sCheckNumber IS NOT NULL AND nCheckDate IS NOT NULL AND nCheckAmount IS NOT NULL";
                        _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
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
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (_retVal != null) { _retVal = null; }
                }

                return _IsExistCheck;
            }

            public Int64 GetContactCompanyId(Int64 ContactId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _sqlQuery = "";
                object _retVal = null;
                Int64 _companyId = 0;

                try
                {
                    _sqlQuery = "SELECT ISNULL(nCompanyId,0) AS nCompanyId FROM Contact_InsurancePlan_Association WITH(NOLOCK) " +
                    " WHERE  nContactId = " + ContactId + " AND nClinicId = " + _clinicId + " ";

                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    {
                        _companyId = Convert.ToInt64(_retVal);
                    }

                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (_retVal != null) { _retVal = null; }
                }

                return _companyId;
            }

            public string GetContactCompanyName(Int64 ContactId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _sqlQuery = "";
                object _retVal = null;
                string _companyName = "";

                try
                {
                    //_sqlQuery = " SELECT ISNULL(sDescription,0) AS sCompanyName FROM Contact_InsurancePlan_Association " +
                    //" WHERE  nContactId = " + ContactId + " AND nClinicId = " + _clinicId + " ";

                    _sqlQuery = " SELECT TOP 1 " +
                    " ISNULL(Contacts_InsuranceCompany_MST.sDescription, 0) AS sCompanyName  " +
                    " FROM          " +
                    " Contact_InsurancePlan_Association WITH(NOLOCK) INNER JOIN " +
                    " Contacts_InsuranceCompany_MST WITH(NOLOCK) ON Contact_InsurancePlan_Association.nCompanyId = Contacts_InsuranceCompany_MST.nID " +
                    " WHERE      " +
                    " (Contact_InsurancePlan_Association.nContactId = " + ContactId + ")  " +
                    " AND (Contact_InsurancePlan_Association.nClinicId = " + _clinicId + ") ";


                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    {
                        _companyName = Convert.ToString(_retVal);
                    }

                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (_retVal != null) { _retVal = null; }
                }

                return _companyName;
            }

            public bool IsContactCompanyAssociated(Int64 ContactId, Int64 CompanyId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _sqlQuery = "";
                object _retVal = null;
                bool _IsExists = false;

                try
                {
                    _sqlQuery = " SELECT ISNULL(COUNT(*),0) FROM Contact_InsurancePlan_Association WITH(NOLOCK) " +
                    " WHERE  nContactId = " + ContactId + " AND nCompanyId = " + CompanyId + " AND nClinicId = " + _clinicId + " ";

                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    {
                        _IsExists = Convert.ToBoolean(_retVal);
                    }

                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (_retVal != null) { _retVal = null; }
                }

                return _IsExists;
            }


            #region " Get Replication ID "

            /// <summary>
            /// Function shifted to gloBilling\Payment\InsurancePayment.cs 
            /// </summary>
            /// <returns></returns>
            private Int64 GetPrefixTransactionID()
            {
                Int64 _Result = 0;
                string _result = "";
                DateTime _PatientDOB = DateTime.Now;
                DateTime _CurrentDate = DateTime.Now;
                DateTime _BaseDate = Convert.ToDateTime("1/1/1900");

                string strID1 = "";
                string strID2 = "";
                string strID3 = "";

                TimeSpan oTS;

                try
                {
                    _result = "";

                    oTS = new TimeSpan();
                    oTS = _CurrentDate.Subtract(_BaseDate);
                    strID1 = oTS.Days.ToString().Replace("-", "");

                    oTS = new TimeSpan();
                    oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                    strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                    oTS = new TimeSpan();
                    oTS = _PatientDOB.Subtract(_BaseDate);
                    strID3 = oTS.Days.ToString().Replace("-", "");

                    _result = strID1 + strID2 + strID3;

                    _Result = Convert.ToInt64(_result);

                }
                catch //(Exception ex)
                {
                    //returns random number if exception occures
                    Random oRan = new Random();
                    return oRan.Next(1, Int32.MaxValue);
                }
                finally
                {

                }
                return _Result;
            }

            #endregion " Get Replication ID "

            public Int64 SaveInsuranceRefund(EOBPayment.Common.PaymentInsurance EOBPaymentInsurance, EOBPayment.Common.InsurancePaymentRefund InsurancePaymentRefund, bool IsSaveForVoid)
            {
                System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(_databaseConnectionString);
                System.Data.SqlClient.SqlCommand _sqlCommand = null;
                System.Data.SqlClient.SqlTransaction _sqlTransaction = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                object _retVal = null;
                Int64 _EOBPayId = 0;
                Int64 _EOBId = 0;
                Int64 _EOBDtlId = 0;
                Int64 _EOBPayDtlId = 0;
                Int64 _EOBPayCreditDtlId = 0;
                Int64 _EOBVoidPayId = 0;

                EOBPayment.Common.PaymentInsuranceLine PaymentInsuranceClaimLine = null;
                EOBPayment.Common.PaymentInsuranceClaim PaymentInsClaim = null;
                EOBPayment.Common.EOBInsurancePaymentDetail EOBInsurancePayDtl = null;
                EOBPayment.Common.EOBInsurancePaymentDetails EOBInsurancePaymentMasterLines = null;

                int _result = 0;

                try
                {
                    if (EOBPaymentInsurance != null)
                    {
                        _sqlConnection.Open();
                        _sqlTransaction = _sqlConnection.BeginTransaction();

                        #region " Master Data Save "

                        //nEOBPaymentID,nEOBRefNO,sPayerName,nPayerID,nPayerType,nPaymentMode,sCheckNumber,nCheckAmount,nCheckDate
                        //nMSTAccountID,nMSTAccountType,nPaymentTrayID,sPaymentTrayName,nCloseDate,sCardType,sCardSecurityNo
                        //nCardID,nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                        _EOBPayId = 0;
                        oParameters.Clear();

                        oParameters.Add("@sPaymentNo", EOBPaymentInsurance.PaymentNumber, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sPaymentNoPrefix", EOBPaymentInsurance.PaymentNumberPefix, ParameterDirection.Input, SqlDbType.VarChar);

                        oParameters.Add("@nEOBPaymentID", EOBPaymentInsurance.EOBPaymentID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nEOBRefNO", EOBPaymentInsurance.EOBRefNO, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sPayerName", EOBPaymentInsurance.PayerName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Unchecked
                        oParameters.Add("@nPayerID", EOBPaymentInsurance.PayerID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nPayerType", EOBPaymentInsurance.PayerType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nPaymentMode", EOBPaymentInsurance.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@sCheckNumber", EOBPaymentInsurance.CheckNumber, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Unchecked
                        oParameters.Add("@nCheckAmount", EOBPaymentInsurance.CheckAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nCheckDate", EOBPaymentInsurance.CheckDate, ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nMSTAccountID", EOBPaymentInsurance.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nMSTAccountType", EOBPaymentInsurance.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked

                        oParameters.Add("@nPaymentTrayID", EOBPaymentInsurance.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sPaymentTrayCode", EOBPaymentInsurance.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                        oParameters.Add("@sPaymentTrayDescription", EOBPaymentInsurance.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255

                        oParameters.Add("@nCloseDate", EOBPaymentInsurance.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sCardType", EOBPaymentInsurance.CardType, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sCardSecurityNo", EOBPaymentInsurance.CardSecurityNo, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@nCardID", EOBPaymentInsurance.CardID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked

                        oParameters.Add("@sAuthorizationNo", EOBPaymentInsurance.AuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                        oParameters.Add("@nCardExpDate", EOBPaymentInsurance.CardExpiryDate, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),

                        oParameters.Add("@nUserID", EOBPaymentInsurance.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sUserName", EOBPaymentInsurance.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtCreatedDateTime", EOBPaymentInsurance.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtModifiedDateTime", EOBPaymentInsurance.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@bIsVoid", EOBPaymentInsurance.IsVoid, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nVoidCloseDate", EOBPaymentInsurance.VoidCloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nVoidTrayID", EOBPaymentInsurance.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nVoidType", EOBPaymentInsurance.VoidType, ParameterDirection.Input, SqlDbType.Int);
                        oParameters.Add("@nVoidRefPaymentID", EOBPaymentInsurance.VoidRefPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", EOBPaymentInsurance.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked

                        oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);

                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                        //oDB.Connect(false);
                        //oDB.Execute("BL_INUP_EOBPayment_MST_PatPayment", oParameters, out _retVal);
                        //oDB.Disconnect();

                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandText = "BL_INUP_EOBPayment_MST_PatPayment";

                        _result = 0;
                        _result = _sqlCommand.ExecuteNonQuery();

                        if (_sqlCommand.Parameters["@nEOBPaymentID"].Value != null)
                        {
                            EOBPaymentInsurance.EOBPaymentID = Convert.ToInt64(_sqlCommand.Parameters["@nEOBPaymentID"].Value);
                        }
                        else
                        {
                            EOBPaymentInsurance.EOBPaymentID = 0;
                        }

                        _sqlCommand.Parameters.Clear();
                        _sqlCommand.Dispose();
                        _sqlCommand = null;

                        oParameters.Clear();

                        oParameters.Add("@nEOBPaymentID", EOBPaymentInsurance.EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nPayerID", EOBPaymentInsurance.PayerID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nPaymentMode", EOBPaymentInsurance.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@sCheckNumber", EOBPaymentInsurance.CheckNumber, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Unchecked
                        oParameters.Add("@nCheckAmount", EOBPaymentInsurance.CheckAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nCheckDate", EOBPaymentInsurance.CheckDate, ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked


                        oParameters.Add("@nPaymentTrayID", EOBPaymentInsurance.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sPaymentTrayCode", EOBPaymentInsurance.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                        oParameters.Add("@sPaymentTrayDescription", EOBPaymentInsurance.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255

                        oParameters.Add("@nCloseDate", EOBPaymentInsurance.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sCardType", EOBPaymentInsurance.CardType, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sCardSecurityNo", EOBPaymentInsurance.CardSecurityNo, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@nCardID", EOBPaymentInsurance.CardID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked

                        oParameters.Add("@sAuthorizationNo", EOBPaymentInsurance.AuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                        oParameters.Add("@nCardExpDate", EOBPaymentInsurance.CardExpiryDate, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),

                        oParameters.Add("@nUserID", EOBPaymentInsurance.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sUserName", EOBPaymentInsurance.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtCreatedDateTime", EOBPaymentInsurance.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtModifiedDateTime", EOBPaymentInsurance.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@bIsVoid", EOBPaymentInsurance.IsVoid, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nVoidCloseDate", EOBPaymentInsurance.VoidCloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nVoidTrayID", EOBPaymentInsurance.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", EOBPaymentInsurance.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked                           

                        oParameters.Add("@nRefundID", InsurancePaymentRefund.RefundID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@sRefundTo", InsurancePaymentRefund.RefundTo, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sRefundNote", InsurancePaymentRefund.RefundNotes, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@dtRefundDate", InsurancePaymentRefund.Refunddate, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nRefundAmount", InsurancePaymentRefund.RefundAmount, ParameterDirection.Input, SqlDbType.Decimal);

                        //Add insurance Refund 
                        if (InsurancePaymentRefund.TransactionID != 0 && InsurancePaymentRefund.MasterTransactionID != 0 && InsurancePaymentRefund.ClaimNo != "")
                        {
                            oParameters.Add("@nTransactionID", InsurancePaymentRefund.TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nClaimNo", InsurancePaymentRefund.ClaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@nTransactionMasterID", InsurancePaymentRefund.MasterTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                        }
                        else
                        {
                            oParameters.Add("@nTransactionID",DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nClaimNo", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@nTransactionMasterID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                        }
                        if (InsurancePaymentRefund.PatientID != 0)
                        {
                            oParameters.Add("@nPatientID", InsurancePaymentRefund.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        }
                        else 
                        {
                            oParameters.Add("@nPatientID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                        }
                       

                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandText = "BL_INSERT_EOBInsurance_Refund";
                        _result = _sqlCommand.ExecuteNonQuery();



                        if (_sqlCommand.Parameters["@nEOBPaymentID"].Value != null && Convert.ToString(_sqlCommand.Parameters["@nEOBPaymentID"].Value).Trim() != "")
                        { _retVal = Convert.ToInt64(_sqlCommand.Parameters["@nEOBPaymentID"].Value); }

                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                        { _EOBPayId = Convert.ToInt64(_retVal); }

                        if (IsSaveForVoid == true)
                        {
                            _EOBVoidPayId = Convert.ToInt64(_retVal);
                        }
                        _sqlCommand.Parameters.Clear();
                        _sqlCommand.Dispose();
                        _sqlCommand = null;
                        #endregion " Master Data Save "

                        #region "Payment Line Master (Credit) Entry, Total Check Amount Entry, but it is in same table "
                        if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails != null && EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count > 0)
                        {
                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count; clmInsPayLnIndex++)
                            {
                                if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex] != null)
                                {
                                    _EOBPayCreditDtlId = 0;
                                    //This credit line detail id we have to setup into debit line where debit line dont have paydtlid
                                    //Suppose we collect new check as well as multiple reserve then
                                    //for reserve amount debit line will get id from form and who dont have id
                                    //we will setup this new id to them
                                    EOBInsurancePayDtl = EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex];

                                    oParameters.Clear();
                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBID", EOBInsurancePayDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBDtlID", EOBInsurancePayDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBPaymentDetailID", EOBInsurancePayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionID", EOBInsurancePayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionDetailID", EOBInsurancePayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionLineNo", EOBInsurancePayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nPatientID", EOBInsurancePayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nDOSFrom", EOBInsurancePayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nDOSTo", EOBInsurancePayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@sCPTCode", EOBInsurancePayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sCPTDescription", EOBInsurancePayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                    if (EOBInsurancePayDtl.IsNullAmount == false)
                                    {
                                        oParameters.Add("@nAmount", EOBInsurancePayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    oParameters.Add("@nPaymentType", EOBInsurancePayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentSubType", EOBInsurancePayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaySign", EOBInsurancePayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPayMode", EOBInsurancePayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nAccountID", EOBInsurancePayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nAccountType", EOBInsurancePayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nMSTAccountID", EOBInsurancePayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nMSTAccountType", EOBInsurancePayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentTrayID", EOBInsurancePayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sPaymentTrayCode", EOBInsurancePayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sPaymentTrayDescription", EOBInsurancePayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@nUserID", EOBInsurancePayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sUserName", EOBInsurancePayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@dtCreatedDateTime", EOBInsurancePayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@dtModifiedDateTime", EOBInsurancePayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@nClinicID", EOBInsurancePayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    oParameters.Add("@nRefEOBPaymentID", EOBInsurancePayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBInsurancePayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //..ResEOBPaymentID,ResEOBPaymentDetailID has the reference id's for the reserve amount
                                    oParameters.Add("@nResEOBPaymentID", EOBInsurancePayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nResEOBPaymentDetailID", EOBInsurancePayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);

                                    oParameters.Add("@nOldResEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nOldResEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                                    oParameters.Add("@nContactInsID", EOBInsurancePayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nCreditLineID", EOBInsurancePayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    oParameters.Add("@nCloseDate", EOBInsurancePayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    // Newly added parameters by pankaj
                                    oParameters.Add("@nTrackTrnID", EOBInsurancePayDtl.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nTrackTrnDtlID", EOBInsurancePayDtl.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@sSubClaimNo", EOBInsurancePayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),
                                    oParameters.Add("@bIsVoid", EOBInsurancePayDtl.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                    oParameters.Add("@nVoidCloseDate", EOBInsurancePayDtl.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                    oParameters.Add("@nVoidTrayID", EOBInsurancePayDtl.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                    oParameters.Add("@nVoidType", EOBInsurancePayDtl.VoidType, ParameterDirection.Input, SqlDbType.Int);

                                    oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                    oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);

                                    //Added by Subashish_b on 10/05 /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding the PAF values 
                                    oParameters.Add("@nPAccountID", EOBInsurancePayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nAccountPatientID", EOBInsurancePayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nGuarantorID", EOBInsurancePayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                    //End

                                    _retVal = null;

                                    //oDB.Connect(false);
                                    //oDB.Execute("BL_INUP_EOBPayment_DTL_PatPayment", oParameters, out _retVal);
                                    //oDB.Disconnect();

                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                    _sqlCommand.Connection = _sqlConnection;
                                    _sqlCommand.Transaction = _sqlTransaction;
                                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                                    _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL_PatPayment";

                                    _result = 0;
                                    _result = _sqlCommand.ExecuteNonQuery();

                                    if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null && Convert.ToString(_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value).Trim() != "")
                                    { _retVal = Convert.ToInt64(_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value); }

                                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                    { _EOBPayCreditDtlId = Convert.ToInt64(_retVal); }
                                    _sqlCommand.Parameters.Clear();
                                    _sqlCommand.Dispose();
                                    _sqlCommand = null;
                                    #region " Add Line Notes "

                                    if (EOBInsurancePayDtl.LineNotes != null && EOBInsurancePayDtl.LineNotes.Count > 0)
                                    {
                                       // Object _RcValue = null;

                                        for (int rcInd = 0; rcInd < EOBInsurancePayDtl.LineNotes.Count; rcInd++)
                                        {
                                            //_RcValue = null;
                                            oParameters.Clear();

                                            oParameters.Add("@nID", EOBInsurancePayDtl.LineNotes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                            oParameters.Add("@nClaimNo", EOBInsurancePayDtl.LineNotes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBID", EOBInsurancePayDtl.LineNotes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentDetailID", _EOBPayCreditDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nBillingTransactionID", EOBInsurancePayDtl.LineNotes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                            oParameters.Add("@nBillingTransactionDetailID", EOBInsurancePayDtl.LineNotes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@sNoteCode", EOBInsurancePayDtl.LineNotes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                            oParameters.Add("@sNoteDescription", EOBInsurancePayDtl.LineNotes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                            oParameters.Add("@dNoteAmount", EOBInsurancePayDtl.LineNotes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                            oParameters.Add("@nPaymentNoteType", EOBInsurancePayDtl.LineNotes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nPaymentNoteSubType", EOBInsurancePayDtl.LineNotes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nIncludeNoteOnPrint", EOBInsurancePayDtl.LineNotes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
                                            oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                            //oDB.Connect(false);
                                            //oDB.Execute("BL_INUP_EOBNotes", oParameters, out _RcValue);
                                            //oDB.Disconnect();

                                            _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                            _sqlCommand = oDB.GetCmdParameters(oParameters);
                                            _sqlCommand.Connection = _sqlConnection;
                                            _sqlCommand.Transaction = _sqlTransaction;
                                            _sqlCommand.CommandType = CommandType.StoredProcedure;
                                            _sqlCommand.CommandText = "BL_INUP_EOBNotes";

                                            _result = 0;
                                            _result = _sqlCommand.ExecuteNonQuery();

                                            if (_sqlCommand.Parameters["@nID"].Value != null && Convert.ToString(_sqlCommand.Parameters["@nID"].Value).Trim() != "")
                                            { _retVal = Convert.ToInt64(_sqlCommand.Parameters["@nID"].Value); }

                                            _sqlCommand.Parameters.Clear();
                                            _sqlCommand.Dispose();
                                            _sqlCommand = null;
                                        }
                                    }

                                    #endregion " Add Line Notes "


                                    EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentID = _EOBPayId;
                                    EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentDetailID = Convert.ToInt64(_retVal.ToString());

                                    #region "Assign Credit Line Reference and Finance Line Reference to debit line wherever applicable"
                                    if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].IsMainCreditLine == true)
                                    {
                                        //All Remaining Credit Lines
                                        for (int nAsgn = 0; nAsgn <= EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count - 1; nAsgn++)
                                        {
                                            if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[nAsgn].IsMainCreditLine == false)
                                            {
                                                EOBPaymentInsurance.EOBInsurancePaymentLineDetails[nAsgn].MainCreditLineID = _EOBPayCreditDtlId;
                                            }
                                        }
                                        //All Debit Lines
                                        for (int nAsgnClaim = 0; nAsgnClaim <= EOBPaymentInsurance.InsuranceClaims.Count - 1; nAsgnClaim++)
                                        {
                                            for (int nAsgnClmLine = 0; nAsgnClmLine <= EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines.Count - 1; nAsgnClmLine++)
                                            {
                                                for (int nAsgn = 0; nAsgn <= EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails.Count - 1; nAsgn++)
                                                {
                                                    if (EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].IsMainCreditLine == false)
                                                    {
                                                        EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].MainCreditLineID = _EOBPayCreditDtlId;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //Assign Reference Number to debit lines
                                    for (int nAsgnClaim = 0; nAsgnClaim <= EOBPaymentInsurance.InsuranceClaims.Count - 1; nAsgnClaim++)
                                    {
                                        for (int nAsgnClmLine = 0; nAsgnClmLine <= EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines.Count - 1; nAsgnClmLine++)
                                        {
                                            for (int nAsgn = 0; nAsgn <= EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails.Count - 1; nAsgn++)
                                            {
                                                if (EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].RefFinanceLieNo == EOBInsurancePayDtl.FinanceLieNo)
                                                {
                                                    if (EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].UseRefFinanceLieNo == true)
                                                    {
                                                        EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].RefEOBPaymentID = _EOBPayId;
                                                        EOBPaymentInsurance.InsuranceClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBInsurancePaymentLineDetails[nAsgn].RefEOBPaymentDetailID = _EOBPayCreditDtlId;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //Assign Main Credit Line ID to Direct Transaction Reserve Lines
                                    for (int nResClmLine = 0; nResClmLine <= EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail.Count - 1; nResClmLine++)
                                    {
                                        EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[nResClmLine].MainCreditLineID = _EOBPayCreditDtlId;
                                        if (EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[nResClmLine].UseRefFinanceLieNo == true)
                                        {
                                            //here checking id is 0, bcz its patient payment directlly send to use reserve, other wise as per insurace payment no need
                                            if (EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[nResClmLine].RefEOBPaymentID == 0 && EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[nResClmLine].RefEOBPaymentDetailID == 0)
                                            {
                                                EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[nResClmLine].RefEOBPaymentID = _EOBPayId;
                                                EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[nResClmLine].RefEOBPaymentDetailID = _EOBPayCreditDtlId;
                                            }
                                        }
                                    }
                                    #endregion

                                    EOBInsurancePayDtl = null;
                                }
                            }
                        }
                        #endregion

                        #region "Payment Line Master Reserve (Debit) Entry"
                        if (EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail != null && EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail.Count > 0)
                        {
                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail.Count; clmInsPayLnIndex++)
                            {
                                if (EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[clmInsPayLnIndex] != null)
                                {
                                    _EOBPayDtlId = 0;
                                    EOBInsurancePayDtl = EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[clmInsPayLnIndex];

                                    oParameters.Clear();
                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBID", EOBInsurancePayDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBDtlID", EOBInsurancePayDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBPaymentDetailID", EOBInsurancePayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionID", EOBInsurancePayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionDetailID", EOBInsurancePayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionLineNo", EOBInsurancePayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nPatientID", EOBInsurancePayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nDOSFrom", EOBInsurancePayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nDOSTo", EOBInsurancePayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@sCPTCode", EOBInsurancePayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sCPTDescription", EOBInsurancePayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                    if (EOBInsurancePayDtl.IsNullAmount == false)
                                    {
                                        oParameters.Add("@nAmount", EOBInsurancePayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    oParameters.Add("@nPaymentType", EOBInsurancePayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentSubType", EOBInsurancePayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaySign", EOBInsurancePayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPayMode", EOBInsurancePayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nAccountID", EOBInsurancePayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nAccountType", EOBInsurancePayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nMSTAccountID", EOBInsurancePayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nMSTAccountType", EOBInsurancePayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentTrayID", EOBInsurancePayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sPaymentTrayCode", EOBInsurancePayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sPaymentTrayDescription", EOBInsurancePayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@nUserID", EOBInsurancePayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sUserName", EOBInsurancePayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@dtCreatedDateTime", EOBInsurancePayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@dtModifiedDateTime", EOBInsurancePayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@nClinicID", EOBInsurancePayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    //..RefEOBPaymentID & RefEOBPaymentDetailID identifies from where (which payment source or check) this payment
                                    //..is coming from
                                    if (EOBInsurancePayDtl.RefEOBPaymentID == 0) { EOBInsurancePayDtl.RefEOBPaymentID = _EOBPayId; }
                                    if (EOBInsurancePayDtl.RefEOBPaymentDetailID == 0) { EOBInsurancePayDtl.RefEOBPaymentDetailID = _EOBPayCreditDtlId; }

                                    oParameters.Add("@nRefEOBPaymentID", EOBInsurancePayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBInsurancePayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //..ResEOBPaymentID,ResEOBPaymentDetailID has the reference id's for the reserve amount
                                    oParameters.Add("@nResEOBPaymentID", EOBInsurancePayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nResEOBPaymentDetailID", EOBInsurancePayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);


                                    oParameters.Add("@nContactInsID", EOBInsurancePayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nCreditLineID", EOBInsurancePayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    oParameters.Add("@nCloseDate", EOBInsurancePayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    // Newly added parameters by pankaj
                                    oParameters.Add("@nTrackTrnID", EOBInsurancePayDtl.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nTrackTrnDtlID", EOBInsurancePayDtl.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@sSubClaimNo", EOBInsurancePayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),
                                    oParameters.Add("@bIsVoid", EOBInsurancePayDtl.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                    oParameters.Add("@nVoidCloseDate", EOBInsurancePayDtl.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                    oParameters.Add("@nVoidTrayID", EOBInsurancePayDtl.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                    oParameters.Add("@nVoidType", EOBInsurancePayDtl.VoidType, ParameterDirection.Input, SqlDbType.Int);

                                    oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                    oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);

                                    oParameters.Add("@nOldResEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nOldResEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                                    //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding the PAF values 
                                    oParameters.Add("@nPAccountID", EOBInsurancePayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nAccountPatientID", EOBInsurancePayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nGuarantorID", EOBInsurancePayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                    //End

                                    _retVal = null;

                                    //oDB.Connect(false);
                                    //oDB.Execute("BL_INUP_EOBPayment_DTL_PatPayment", oParameters, out _retVal);
                                    //oDB.Disconnect();

                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                    _sqlCommand.Connection = _sqlConnection;
                                    _sqlCommand.Transaction = _sqlTransaction;
                                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                                    _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL_PatPayment";

                                    _result = 0;
                                    _result = _sqlCommand.ExecuteNonQuery();

                                    if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null && Convert.ToString(_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value).Trim() != "")
                                    { _retVal = Convert.ToInt64(_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value); }



                                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                    { _EOBPayDtlId = Convert.ToInt64(_retVal); }
                                    _sqlCommand.Parameters.Clear();
                                    _sqlCommand.Dispose();
                                    _sqlCommand = null;
                                    #region " Add Line Notes "

                                    if (EOBInsurancePayDtl.LineNotes != null && EOBInsurancePayDtl.LineNotes.Count > 0)
                                    {
                                      //  Object _RcValue = null;

                                        for (int rcInd = 0; rcInd < EOBInsurancePayDtl.LineNotes.Count; rcInd++)
                                        {
                                           // _RcValue = null;
                                            oParameters.Clear();

                                            oParameters.Add("@nID", EOBInsurancePayDtl.LineNotes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                            oParameters.Add("@nClaimNo", EOBInsurancePayDtl.LineNotes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBID", EOBInsurancePayDtl.LineNotes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentDetailID", _EOBPayDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nBillingTransactionID", EOBInsurancePayDtl.LineNotes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                            oParameters.Add("@nBillingTransactionDetailID", EOBInsurancePayDtl.LineNotes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@sNoteCode", EOBInsurancePayDtl.LineNotes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                            oParameters.Add("@sNoteDescription", EOBInsurancePayDtl.LineNotes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                            oParameters.Add("@dNoteAmount", EOBInsurancePayDtl.LineNotes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                            oParameters.Add("@nPaymentNoteType", EOBInsurancePayDtl.LineNotes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nPaymentNoteSubType", EOBInsurancePayDtl.LineNotes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nIncludeNoteOnPrint", EOBInsurancePayDtl.LineNotes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
                                            oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                            //oDB.Connect(false);
                                            //oDB.Execute("BL_INUP_EOBNotes", oParameters, out _RcValue);
                                            //oDB.Disconnect();

                                            _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                            _sqlCommand = oDB.GetCmdParameters(oParameters);
                                            _sqlCommand.Connection = _sqlConnection;
                                            _sqlCommand.Transaction = _sqlTransaction;
                                            _sqlCommand.CommandType = CommandType.StoredProcedure;
                                            _sqlCommand.CommandText = "BL_INUP_EOBNotes";

                                            _result = 0;
                                            _result = _sqlCommand.ExecuteNonQuery();

                                            if (_sqlCommand.Parameters["@nID"].Value != null && Convert.ToString(_sqlCommand.Parameters["@nID"].Value).Trim() != "")
                                            { _retVal = Convert.ToInt64(_sqlCommand.Parameters["@nID"].Value); }
                                            _sqlCommand.Parameters.Clear();
                                            _sqlCommand.Dispose();
                                            _sqlCommand = null;
                                        }
                                    }

                                    #endregion " Add Line Notes "


                                    EOBInsurancePayDtl = null;
                                }
                            }
                        }
                        #endregion

                        #region " EOB Financial Service Line Save "
                        if (_EOBPayId > 0 && EOBPaymentInsurance.InsuranceClaims != null && EOBPaymentInsurance.InsuranceClaims.Count > 0)
                        {
                            for (int clmIndex = 0; clmIndex < EOBPaymentInsurance.InsuranceClaims.Count; clmIndex++)
                            {
                                PaymentInsClaim = EOBPaymentInsurance.InsuranceClaims[clmIndex];
                                for (int clmLnIndex = 0; clmLnIndex < PaymentInsClaim.CliamLines.Count; clmLnIndex++)
                                {
                                    if (PaymentInsClaim.CliamLines[clmLnIndex] != null)
                                    {
                                        _EOBDtlId = 0;
                                        PaymentInsuranceClaimLine = PaymentInsClaim.CliamLines[clmLnIndex];
                                        if (_EOBPayId > 0 && PaymentInsuranceClaimLine.EOBInsurancePaymentLineDetails != null && PaymentInsuranceClaimLine.EOBInsurancePaymentLineDetails.Count > 0)
                                        {
                                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < PaymentInsuranceClaimLine.EOBInsurancePaymentLineDetails.Count; clmInsPayLnIndex++)
                                            {
                                                if (PaymentInsuranceClaimLine.EOBInsurancePaymentLineDetails[clmInsPayLnIndex] != null)
                                                {
                                                    _EOBPayDtlId = 0;
                                                    EOBInsurancePayDtl = PaymentInsuranceClaimLine.EOBInsurancePaymentLineDetails[clmInsPayLnIndex];

                                                    oParameters.Clear();
                                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nEOBDtlID", _EOBDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nEOBPaymentDetailID", EOBInsurancePayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nBillingTransactionID", EOBInsurancePayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nBillingTransactionDetailID", EOBInsurancePayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nBillingTransactionLineNo", EOBInsurancePayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nPatientID", EOBInsurancePayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nDOSFrom", EOBInsurancePayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nDOSTo", EOBInsurancePayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@sCPTCode", EOBInsurancePayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                    oParameters.Add("@sCPTDescription", EOBInsurancePayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                                    if (EOBInsurancePayDtl.IsNullAmount == false)
                                                    {
                                                        oParameters.Add("@nAmount", EOBInsurancePayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                    }
                                                    else
                                                    {
                                                        oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                    }

                                                    oParameters.Add("@nPaymentType", EOBInsurancePayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPaymentSubType", EOBInsurancePayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPaySign", EOBInsurancePayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPayMode", EOBInsurancePayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nAccountID", EOBInsurancePayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nAccountType", EOBInsurancePayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nMSTAccountID", EOBInsurancePayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nMSTAccountType", EOBInsurancePayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPaymentTrayID", EOBInsurancePayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@sPaymentTrayCode", EOBInsurancePayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                    oParameters.Add("@sPaymentTrayDescription", EOBInsurancePayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                    oParameters.Add("@nUserID", EOBInsurancePayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@sUserName", EOBInsurancePayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                    oParameters.Add("@dtCreatedDateTime", EOBInsurancePayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                    oParameters.Add("@dtModifiedDateTime", EOBInsurancePayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                    oParameters.Add("@nClinicID", EOBInsurancePayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                                    //..RefEOBPaymentID & RefEOBPaymentDetailID identifies from where (which payment source or check) this payment
                                                    //..is coming from
                                                    if (EOBInsurancePayDtl.RefEOBPaymentID == 0) { EOBInsurancePayDtl.RefEOBPaymentID = _EOBPayId; }
                                                    if (EOBInsurancePayDtl.RefEOBPaymentDetailID == 0) { EOBInsurancePayDtl.RefEOBPaymentDetailID = _EOBPayCreditDtlId; }

                                                    oParameters.Add("@nRefEOBPaymentID", EOBInsurancePayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBInsurancePayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                                    //..ResEOBPaymentID,ResEOBPaymentDetailID has the reference id's for the reserve amount
                                                    oParameters.Add("@nResEOBPaymentID", EOBInsurancePayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nResEOBPaymentDetailID", EOBInsurancePayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);

                                                    oParameters.Add("@nContactInsID", EOBInsurancePayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@nCreditLineID", EOBInsurancePayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                                    oParameters.Add("@nCloseDate", EOBInsurancePayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                                    // Newly added parameters by pankaj
                                                    oParameters.Add("@nTrackTrnID", EOBInsurancePayDtl.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@nTrackTrnDtlID", EOBInsurancePayDtl.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@sSubClaimNo", EOBInsurancePayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),
                                                    oParameters.Add("@bIsVoid", EOBInsurancePayDtl.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                                    oParameters.Add("@nVoidCloseDate", EOBInsurancePayDtl.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                                    oParameters.Add("@nVoidTrayID", EOBInsurancePayDtl.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                                    oParameters.Add("@nVoidType", EOBInsurancePayDtl.VoidType, ParameterDirection.Input, SqlDbType.Int);

                                                    oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                                    oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);

                                                    oParameters.Add("@nOldResEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nOldResEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                                                    //Added by Subashish_b on 10/05 /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding the PAF values 
                                                    oParameters.Add("@nPAccountID", EOBInsurancePayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nAccountPatientID", EOBInsurancePayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nGuarantorID", EOBInsurancePayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    //End
                                                    _retVal = null;

                                                    //oDB.Connect(false);
                                                    //oDB.Execute("BL_INUP_EOBPayment_DTL_PatPayment", oParameters, out _retVal);
                                                    //oDB.Disconnect();

                                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                    _sqlCommand.Connection = _sqlConnection;
                                                    _sqlCommand.Transaction = _sqlTransaction;
                                                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                                                    _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL_PatPayment";

                                                    _result = 0;
                                                    _result = _sqlCommand.ExecuteNonQuery();

                                                    if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null && Convert.ToString(_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value).Trim() != "")
                                                    { _retVal = Convert.ToInt64(_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value); }

                                                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                                    { _EOBPayDtlId = Convert.ToInt64(_retVal); }

                                                    EOBInsurancePayDtl = null;
                                                    _sqlCommand.Parameters.Clear();
                                                    _sqlCommand.Dispose();
                                                    _sqlCommand = null;

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        #endregion " EOB Financial Service Line Save "

                        EOBInsurancePaymentMasterLines = EOBPaymentInsurance.EOBInsurancePaymentLineDetails;

                        _sqlTransaction.Commit();
                        _sqlConnection.Close();

                        #region " Save last selected Close date "

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseConnectionString);
                        oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(gloDateMaster.gloDate.DateAsDate(EOBPaymentInsurance.CloseDate)).ToString("MM/dd/yyyy"), _clinicId, EOBPaymentInsurance.UserID, gloSettings.SettingFlag.User);
                        oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", EOBPaymentInsurance.PaymentTrayID.ToString(), _clinicId, EOBPaymentInsurance.UserID, gloSettings.SettingFlag.User);
                        oSettings.Dispose();

                        //start Abhisekh  3 sept 2010

                        //gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, AppSettings.ConnectionStringEMR);
                        //ogloBilling.SaveUserWiseCloseDay(gloDateMaster.gloDate.DateAsDate(EOBPaymentInsurance.CloseDate).ToString(), CloseDayType.Payment, _clinicId);
                        //ogloBilling.Dispose();

                        //end 

                        #endregion " Save last selected Close date "
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    _sqlTransaction.Rollback();
                    _sqlConnection.Close();
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    _sqlTransaction.Rollback();
                    _sqlConnection.Close();
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (_retVal != null) { _retVal = null; }

                    if (_sqlCommand != null)
                    {
                         if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); }
                        _sqlCommand.Dispose();
                        _sqlCommand = null;
                    }
                }

                return _EOBPayId;
            }

            #endregion " Private & Public Methods "

        }

        namespace Common
        {
            public class PaymentInsurance
            {

                #region "Private Variables"

                public string PaymentNumber = "";
                public string PaymentNumberPefix = "";
                public Int64 EOBPaymentID = 0;
                public string EOBRefNO = "";
                public string PayerName = "";
                public Int64 PayerID = 0;
                public EOBPaymentAccountType PayerType = EOBPaymentAccountType.None;
                public EOBPaymentMode PaymentMode = EOBPaymentMode.None;
                public string CheckNumber = "";
                public decimal CheckAmount = 0;
                public Int64 CheckDate = 0;
                public Int64 MSTAccountID = 0;
                public EOBPaymentAccountType MSTAccountType = EOBPaymentAccountType.None;
                public Int64 PaymentTrayID = 0;
                public string PaymentTrayCode = "";
                public string PaymentTrayDesc = "";
                public Int64 CloseDate = 0;
                public string CardType = "";
                public string CardSecurityNo = "";
                public Int64 CardID = 0;
                public string AuthorizationNo = "";
                public Int64 CardExpiryDate = 0;
                public Int64 BPRID = 0; // Added on 28-June-2010 by Qutub.

                //Insurance Refund 
                public Int64 TransactionId = 0;
                public Int64 MasterTransactionId = 0;
                public Int64 PatientID = 0;
                public string ClaimNo = "";
                //


                public Int64 UserID = 0;
                public string UserName = "";
                public DateTime CreatedDateTime = DateTime.Now;
                public DateTime ModifiedDateTime = DateTime.Now;
                public Int64 ClinicID = 0;

                private PaymentInsuranceClaims _PaymentInsuranceClaims = null;
                private EOBInsurancePaymentDetails _EOBInsurancePaymentLineDetails = null;
                private EOBInsurancePaymentDetails _EOBInsurancePaymentReserveLineDetail = null;
                private PaymentInsuranceLineNotes _Notes = null;
                private EOBInsuranceReserveDetails _EOBInsuranceReserveDetail = null;
                public bool bIsERAPayment = false;
                private Int64 _nVoidCloseDate = 0;
                private Int64 _nVoidTrayID = 0;
                private bool _bIsVoid = false;
                private VoidType _VoidType = VoidType.None;
                public Int64 VoidRefPaymentID = 0;
                private String _sClaimVoidNote = "";

                //Added by Subashish_b on 10/May /2011 (integration made on date-10/May/2011) for declaring variable to be used in property for PAF
                private Int64 _nPAccountID = 0;
                private Int64 _nAccountPatientID = 0;
                private Int64 _nGuarantorID = 0;
                //End

                private bool paymentAssigned = true;
                private bool lineAssigned = true;
                private bool reserveAssigned = true;
                private bool noteAssigned = true;
                #endregion

                #region "Constructor & Distructor"


                public PaymentInsurance()
                {
                    _PaymentInsuranceClaims = new PaymentInsuranceClaims();
                    _EOBInsurancePaymentLineDetails = new EOBInsurancePaymentDetails();
                    _EOBInsurancePaymentReserveLineDetail = new EOBInsurancePaymentDetails();
                    _Notes = new PaymentInsuranceLineNotes();
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
                            if (paymentAssigned)
                            {
                                if (_PaymentInsuranceClaims != null)
                                {
                                    _PaymentInsuranceClaims.Dispose();
                                    _PaymentInsuranceClaims = null;
                                }
                            }
                            if (lineAssigned)
                            {

                                if (_EOBInsurancePaymentLineDetails != null)
                                {
                                    _EOBInsurancePaymentLineDetails.Dispose();
                                    _EOBInsurancePaymentLineDetails = null;
                                }
                            }
                            if (reserveAssigned)
                            {
                                if (_EOBInsurancePaymentReserveLineDetail != null)
                                {
                                    _EOBInsurancePaymentReserveLineDetail.Dispose();
                                    _EOBInsurancePaymentReserveLineDetail = null;
                                }
                            }
                            if (noteAssigned)
                            {
                                if (_Notes != null)
                                {
                                    _Notes.Dispose();
                                    _Notes = null;
                                }
                            }
                        }
                    }
                    disposed = true;
                }

                ~PaymentInsurance()
                {
                    Dispose(false);
                }

                #endregion

                public PaymentInsuranceClaims InsuranceClaims
                {
                    get { return _PaymentInsuranceClaims; }
                    set {
                        if (paymentAssigned)
                        {
                            if (_PaymentInsuranceClaims != null)
                            {
                                _PaymentInsuranceClaims.Dispose();
                                _PaymentInsuranceClaims = null;
                            }
                        }
                        
                        _PaymentInsuranceClaims = value;
                        paymentAssigned = false;
                    }
                }
                public EOBInsurancePaymentDetails EOBInsurancePaymentLineDetails
                {
                    get { return _EOBInsurancePaymentLineDetails; }
                    set {
                        
                        if (lineAssigned)
                        {

                            if (_EOBInsurancePaymentLineDetails != null)
                            {
                                _EOBInsurancePaymentLineDetails.Dispose();
                                _EOBInsurancePaymentLineDetails = null;
                            }
                        }
                        
                        _EOBInsurancePaymentLineDetails = value;
                        lineAssigned = false;
                    }
                }
                public EOBInsurancePaymentDetails EOBInsurancePaymentReserveLineDetail
                {
                    get { return _EOBInsurancePaymentReserveLineDetail; }
                    set {
                       
                        if (reserveAssigned)
                        {
                            if (_EOBInsurancePaymentReserveLineDetail != null)
                            {
                                _EOBInsurancePaymentReserveLineDetail.Dispose();
                                _EOBInsurancePaymentReserveLineDetail = null;
                            }
                        }
                       
                        _EOBInsurancePaymentReserveLineDetail = value;
                        reserveAssigned = false;
                    }
                }
                public EOBInsuranceReserveDetails EOBInsuranceReserveDetails
                {
                    get { return _EOBInsuranceReserveDetail; }
                    set { _EOBInsuranceReserveDetail = value; }
                }
                public PaymentInsuranceLineNotes Notes
                { get { return _Notes; } set {
                    
                    if (noteAssigned)
                    {
                        if (_Notes != null)
                        {
                            _Notes.Dispose();
                            _Notes = null;
                        }
                    }
                    _Notes = value;
                    noteAssigned = false;
                } }
                public Int64 VoidCloseDate
                { get { return _nVoidCloseDate; } set { _nVoidCloseDate = value; } }

                public Int64 VoidTrayID
                { get { return _nVoidTrayID; } set { _nVoidTrayID = value; } }

                public bool IsVoid
                { get { return _bIsVoid; } set { _bIsVoid = value; } }

                public VoidType VoidType
                { get { return _VoidType; } set { _VoidType = value; } }

                public String sClaimVoidNote
                {
                    get
                    {
                        return _sClaimVoidNote;
                    }
                    set
                    {
                        _sClaimVoidNote = value;
                    }
                }

                //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  creating properties for storing the PAF Values
                
                public Int64 PAccountID
                { get { return _nPAccountID; } set { _nPAccountID = value; } }

                public Int64 AccountPatientID
                { get { return _nAccountPatientID; } set { _nAccountPatientID = value; } }

                public Int64 GuarantorID
                { get { return _nGuarantorID; } set { _nGuarantorID = value; } }

                //End

            }

            public class PaymentInsuranceClaim
            {
                #region "Private Variables"

                private Int64 _ClaimNo = 0;
                private string _DisplayClaimNo = "";
                private string _ClaimNoPrefix = "";
                private Int64 _BillingTransactionID = 0;
                private Int64 _BillingTransactionDate = 0;
                private Int64 _PatientID = 0;
                private string _PatientName = "";
                private PaymentInsuranceLines _CliamLines = null;
                private Int64 _ClinicId = 0;

                private string _SubClaimNo = "";
                private Int64 _TrackBillingTrnID = 0;
                private bool claimAssigned = true;

                #endregion

                #region "Constructor & Distructor"


                public PaymentInsuranceClaim()
                {
                    _CliamLines = new PaymentInsuranceLines();
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
                            if (claimAssigned)
                            {
                                if (_CliamLines != null)
                                {
                                    _CliamLines.Dispose();
                                    _CliamLines = null;
                                }
                            }
                        }
                    }
                    disposed = true;
                }

                ~PaymentInsuranceClaim()
                {
                    Dispose(false);
                }

                #endregion

                #region " Property Procedures "

                public Int64 ClaimNo
                {
                    get { return _ClaimNo; }
                    set { _ClaimNo = value; }
                }
                public string DisplayClaimNo
                {
                    get { return _DisplayClaimNo; }
                    set { _DisplayClaimNo = value; }
                }
                public string ClaimNoPrefix
                {
                    get { return _ClaimNoPrefix; }
                    set { _ClaimNoPrefix = value; }
                }
                public Int64 BillingTransactionID
                {
                    get { return _BillingTransactionID; }
                    set { _BillingTransactionID = value; }
                }
                public Int64 BillingTransactionDate
                {
                    get { return _BillingTransactionDate; }
                    set { _BillingTransactionDate = value; }
                }
                public Int64 PatientID
                {
                    get { return _PatientID; }
                    set { _PatientID = value; }
                }
                public string PatientName
                {
                    get { return _PatientName; }
                    set { _PatientName = value; }
                }
                public PaymentInsuranceLines CliamLines
                {
                    get { return _CliamLines; }
                    set {
                        if (claimAssigned)
                        {
                            if (_CliamLines != null)
                            {
                                _CliamLines.Dispose();
                                _CliamLines = null;
                            }
                        }
                        _CliamLines = value;
                        claimAssigned = false;
                    }
                }

                public Int64 ClinicID
                {
                    get { return _ClinicId; }
                    set { _ClinicId = value; }
                }

                public string SubClaimNo
                { get { return _SubClaimNo; } set { _SubClaimNo = value; } }

                public Int64 TrackBillingTrnID
                { get { return _TrackBillingTrnID; } set { _TrackBillingTrnID = value; } }

                #endregion " Property Procedures "
            }

            public class PaymentInsuranceClaims
            {

                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public PaymentInsuranceClaims()
                {
                    _innerlist = new ArrayList();
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
                            if (_innerlist != null)
                            {
                                _innerlist.Clear();
                            }
                        }
                    }
                    disposed = true;
                }


                ~PaymentInsuranceClaims()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(PaymentInsuranceClaim item)
                {
                    _innerlist.Add(item);
                }

                public bool Remove(PaymentInsuranceClaim item)
                //Remark - Work Remining for comparision
                {
                    bool result = false;


                    return result;
                }

                public bool RemoveAt(int index)
                {
                    bool result = false;
                    _innerlist.RemoveAt(index);
                    result = true;
                    return result;
                }

                public void Clear()
                {
                    _innerlist.Clear();
                }

                public PaymentInsuranceClaim this[int index]
                {
                    get
                    { return (PaymentInsuranceClaim)_innerlist[index]; }
                }

                public bool Contains(PaymentInsuranceClaim item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(PaymentInsuranceClaim item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(PaymentInsuranceClaim[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }

            public class PaymentInsuranceLine
            {
                #region "Constructor & Distructor"

                public PaymentInsuranceLine()
                {
                    _EOBInsurancePaymentLineDetails = new EOBInsurancePaymentDetails();
                    _LineResonCodes = new PaymentInsuranceLineResonCodes();
                    _LineNextAction = new PaymentInsuranceLineNextAction();
                    _LineNotes = new PaymentInsuranceLineNotes();

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
                            if (paymentAssigned)
                            {
                                if (_EOBInsurancePaymentLineDetails != null)
                                {
                                    _EOBInsurancePaymentLineDetails.Dispose();
                                    _EOBInsurancePaymentLineDetails = null;
                                }
                            }

                            if (reasonAssigned)
                            {
                                if (_LineResonCodes != null)
                                {
                                    _LineResonCodes.Dispose();
                                    _LineResonCodes = null;
                                }
                            }
                            if (actionAssigned)
                            {
                                if (_LineNextAction != null)
                                {
                                    _LineNextAction.Dispose();
                                    _LineNextAction = null;
                                }
                            }
                            if (notesAssigned)
                            {
                                if (_LineNotes != null)
                                {
                                    _LineNotes.Dispose();
                                    _LineNotes = null;
                                }
                            }
                        }
                    }
                    disposed = true;
                }

                ~PaymentInsuranceLine()
                {
                    Dispose(false);
                }

                #endregion

                #region " Private & Public Variables "

                private Int64 _PatientID = 0;

                private Int64 _BLTransactionID = 0;
                private Int64 _BLTransactionDetailID = 0;
                private Int64 _BLTransactionLineNo = 0;
                private Int64 _ClaimNumber = 0;

                private Int64 _TrackBLTransactionID = 0;
                private Int64 _TrackBLTransactionDetailID = 0;
                private Int64 _TrackBLTransactionLineNo = 0;
                private string _SubClaimNumber = "";
                private string _sModifier = "";

                private Int64 _DOSFrom = 0;
                private Int64 _DOSTo = 0;
                private string _CPTCode = "";
                private string _CPTDescription = "";

                private Int64 _BLInsuranceId = 0;
                private string _BLInsuranceName = "";
                private InsuranceTypeFlag _BLInsuranceFlag = InsuranceTypeFlag.None;

                private decimal _Charges = 0;
                private decimal _Unit = 0;
                private decimal _TotalCharges = 0;
                private decimal _Allowed = 0;
                private decimal _WriteOff = 0;
                private decimal _NonCovered = 0;
                private decimal _Insurance = 0;
                private decimal _Copay = 0;
                private decimal _Deductible = 0;
                private decimal _CoInsurance = 0;
                private decimal _Withhold = 0;

                private bool _IsNullCharges = true;
                private bool _IsNullUnit = true;
                private bool _IsNullTotalCharges = true;
                private bool _IsNullAllowed = true;
                private bool _IsNullWriteOff = true;
                private bool _IsNullNonCovered = true;
                private bool _IsNullInsurance = true;
                private bool _IsNullCopay = true;
                private bool _IsNullDeductible = true;
                private bool _IsNullCoInsurance = true;
                private bool _IsNullWithhold = true;


                private decimal _LinePaid = 0;
                private decimal _LinePaidByPatient = 0;
                private decimal _LinePaidByInsurance = 0;
                private decimal _LinePaidWriteOff = 0;
                private decimal _LinePaidWithHold = 0;
                private decimal _LineBalance = 0;

                //Fields used for modify 
                private Int64 _mEOBId = 0;
                private Int64 _mEOBDtlID = 0;
                private Int64 _mEOBPaymentId = 0;

                private Int64 _paymentTrayId = 0;
                private string _paymentTrayCode = "";
                private string _paymentTrayDesc = "";
                private DateTime _createdDateTime = DateTime.Now;
                private DateTime _modifiedDateTime = DateTime.Now;

                private Int64 _PatInsuranceId = 0;
                private Int64 _InsContactId = 0;

                private Int64 _userId = 0;
                private string _userName = "";

                private Int64 _clinicId = 0;

                //....Field to identify whether the eob entry is for patient payment or insurance
                private EOBPaymentType _eobType = EOBPaymentType.InsuracePayment;

                private EOBInsurancePaymentDetails _EOBInsurancePaymentLineDetails = null;

                private PaymentInsuranceLineResonCodes _LineResonCodes = null;
                private PaymentInsuranceLineNextAction _LineNextAction = null;
                private PaymentInsuranceLineNotes _LineNotes = null;

                private bool paymentAssigned = true;
                private bool reasonAssigned = true;
                private bool actionAssigned = true;
                private bool notesAssigned = true;

                private decimal _last_allowed = 0;
                private decimal _last_payment = 0;
                private decimal _last_writeoff = 0;
                private decimal _last_copay = 0;
                private decimal _last_deductible = 0;
                private decimal _last_coinsurance = 0;
                private decimal _last_withhold = 0;
                private bool _iscorrection = false;

                private Int64 _InsCompanyid = 0;

                private Int64 _closeDate = 0;

                private bool _IsSplitted = false;

                private bool _bIsERAPayment = false;

                //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for declaring property variable for PAF

                private Int64 _nPAccountID = 0;
                private Int64 _nAccountPatientID = 0;
                private Int64 _nGuarantorID = 0;

                
                //End

                #endregion " Private & Public Variables "

                #region " Property Procedures "

                public Int64 PatientID
                { get { return _PatientID; } set { _PatientID = value; } }

                public Int64 BLTransactionID
                { get { return _BLTransactionID; } set { _BLTransactionID = value; } }
                public Int64 BLTransactionDetailID
                { get { return _BLTransactionDetailID; } set { _BLTransactionDetailID = value; } }
                public Int64 BLTransactionLineNo
                { get { return _BLTransactionLineNo; } set { _BLTransactionLineNo = value; } }
                public Int64 ClaimNumber
                { get { return _ClaimNumber; } set { _ClaimNumber = value; } }

                public Int64 TrackBLTransactionID
                { get { return _TrackBLTransactionID; } set { _TrackBLTransactionID = value; } }
                public Int64 TrackBLTransactionDetailID
                { get { return _TrackBLTransactionDetailID; } set { _TrackBLTransactionDetailID = value; } }
                public Int64 TrackBLTransactionLineNo
                { get { return _TrackBLTransactionLineNo; } set { _TrackBLTransactionLineNo = value; } }
                public string SubClaimNumber
                { get { return _SubClaimNumber; } set { _SubClaimNumber = value; } }

                public Int64 DOSFrom
                { get { return _DOSFrom; } set { _DOSFrom = value; } }
                public Int64 DOSTo
                { get { return _DOSTo; } set { _DOSTo = value; } }

                public string CPTCode
                { get { return _CPTCode; } set { _CPTCode = value; } }
                public string CPTDescription
                { get { return _CPTDescription; } set { _CPTDescription = value; } }

                string _CrossWalkCPTCode = "";
                string _CrossWalkCPTDescription = "";

                public string CrossWalkCPTCode
                { get { return _CrossWalkCPTCode; } set { _CrossWalkCPTCode = value; } }
                public string CrossWalkCPTDescription
                { get { return _CrossWalkCPTDescription; } set { _CrossWalkCPTDescription = value; } }

                public string Modifier
                { get { return _sModifier; } set { _sModifier = value; } }

                public Int64 BLInsuranceID
                { get { return _BLInsuranceId; } set { _BLInsuranceId = value; } }
                public string BLInsuranceName
                { get { return _BLInsuranceName; } set { _BLInsuranceName = value; } }
                public InsuranceTypeFlag BLInsuranceFlag
                { get { return _BLInsuranceFlag; } set { _BLInsuranceFlag = value; } }

                public decimal Charges
                { get { return _Charges; } set { _Charges = value; } }
                public decimal Unit
                { get { return _Unit; } set { _Unit = value; } }
                public decimal TotalCharges
                { get { return _TotalCharges; } set { _TotalCharges = value; } }
                public decimal Allowed
                { get { return _Allowed; } set { _Allowed = value; } }
                public decimal WriteOff
                { get { return _WriteOff; } set { _WriteOff = value; } }
                public decimal NonCovered
                { get { return _NonCovered; } set { _NonCovered = value; } }
                public decimal InsuranceAmount
                { get { return _Insurance; } set { _Insurance = value; } }
                public decimal Copay
                { get { return _Copay; } set { _Copay = value; } }
                public decimal Deductible
                { get { return _Deductible; } set { _Deductible = value; } }
                public decimal CoInsurance
                { get { return _CoInsurance; } set { _CoInsurance = value; } }
                public decimal Withhold
                { get { return _Withhold; } set { _Withhold = value; } }


                public bool IsNullCharges
                { get { return _IsNullCharges; } set { _IsNullCharges = value; } }
                public bool IsNullUnit
                { get { return _IsNullUnit; } set { _IsNullUnit = value; } }
                public bool IsNullTotalCharges
                { get { return _IsNullTotalCharges; } set { _IsNullTotalCharges = value; } }
                public bool IsNullAllowed
                { get { return _IsNullAllowed; } set { _IsNullAllowed = value; } }
                public bool IsNullWriteOff
                { get { return _IsNullWriteOff; } set { _IsNullWriteOff = value; } }
                public bool IsNullNonCovered
                { get { return _IsNullNonCovered; } set { _IsNullNonCovered = value; } }
                public bool IsNullInsurance
                { get { return _IsNullInsurance; } set { _IsNullInsurance = value; } }
                public bool IsNullCopay
                { get { return _IsNullCopay; } set { _IsNullCopay = value; } }
                public bool IsNullDeductible
                { get { return _IsNullDeductible; } set { _IsNullDeductible = value; } }
                public bool IsNullCoInsurance
                { get { return _IsNullCoInsurance; } set { _IsNullCoInsurance = value; } }
                public bool IsNullWithhold
                { get { return _IsNullWithhold; } set { _IsNullWithhold = value; } }

                public decimal LinePaidAmount
                { get { return _LinePaid; } set { _LinePaid = value; } }
                public decimal LinePaidByPatient
                { get { return _LinePaidByPatient; } set { _LinePaidByPatient = value; } }
                public decimal LinePaidByInsurance
                { get { return _LinePaidByInsurance; } set { _LinePaidByInsurance = value; } }

                public decimal LinePaidWriteOff
                { get { return _LinePaidWriteOff; } set { _LinePaidWriteOff = value; } }
                public decimal LinePaidWithHold
                { get { return _LinePaidWithHold; } set { _LinePaidWithHold = value; } }

                public decimal LineBalance
                { get { return _LineBalance; } set { _LineBalance = value; } }

                //Fields used for modify 
                public Int64 mEOBID
                { get { return _mEOBId; } set { _mEOBId = value; } }
                public Int64 mEOBDtlID
                { get { return _mEOBDtlID; } set { _mEOBDtlID = value; } }
                public Int64 mEOBPaymentID
                { get { return _mEOBPaymentId; } set { _mEOBPaymentId = value; } }

                public Int64 PaymentTrayID
                { get { return _paymentTrayId; } set { _paymentTrayId = value; } }
                public string PaymentTrayCode
                { get { return _paymentTrayCode; } set { _paymentTrayCode = value; } }
                public string PaymentTrayDesc
                { get { return _paymentTrayDesc; } set { _paymentTrayDesc = value; } }
                public DateTime CreatedDateTime
                { get { return _createdDateTime; } set { _createdDateTime = value; } }
                public DateTime ModifiedDateTime
                { get { return _modifiedDateTime; } set { _modifiedDateTime = value; } }

                public Int64 PatInsuranceID
                { get { return _PatInsuranceId; } set { _PatInsuranceId = value; } }
                public Int64 InsContactID
                { get { return _InsContactId; } set { _InsContactId = value; } }
                public Int64 InsCompanyID
                { get { return _InsCompanyid; } set { _InsCompanyid = value; } }

                public Int64 UserID
                { get { return _userId; } set { _userId = value; } }
                public string UserName
                { get { return _userName; } set { _userName = value; } }

                public Int64 ClinicID
                { get { return _clinicId; } set { _clinicId = value; } }
                private Int64 _nVoidCloseDate = 0;
                private Int64 _nVoidTrayID = 0;
                private bool _bIsVoid = false;
                private VoidType _VoidType = VoidType.None;

                //....Field to identify whether the eob entry is for patient payment or insurance
                public EOBPaymentType EOBType
                { get { return _eobType; } set { _eobType = value; } }

                public EOBInsurancePaymentDetails EOBInsurancePaymentLineDetails
                {
                    get { return _EOBInsurancePaymentLineDetails; }
                    set {
                        if (paymentAssigned)
                        {
                            if (_EOBInsurancePaymentLineDetails != null)
                            {
                                _EOBInsurancePaymentLineDetails.Dispose();
                                _EOBInsurancePaymentLineDetails = null;
                            }
                        }

                       
                        _EOBInsurancePaymentLineDetails = value;
                        paymentAssigned = false;
                    }
                }

                public PaymentInsuranceLineResonCodes LineResonCodes
                { get { return _LineResonCodes; } set {
                     
                    if (reasonAssigned)
                    {
                        if (_LineResonCodes != null)
                        {
                            _LineResonCodes.Dispose();
                            _LineResonCodes = null;
                        }
                    }
                    
                    _LineResonCodes = value;
                    reasonAssigned = false;
                } }

                public PaymentInsuranceLineNextAction LineNextAction
                { get { return _LineNextAction; } set {
                   
                    if (actionAssigned)
                    {
                        if (_LineNextAction != null)
                        {
                            _LineNextAction.Dispose();
                            _LineNextAction = null;
                        }
                    }
                    
                    _LineNextAction = value;
                    actionAssigned = false;
                } }

                public decimal Last_allowed
                { get { return _last_allowed; } set { _last_allowed = value; } }
                public decimal Last_payment
                { get { return _last_payment; } set { _last_payment = value; } }
                public decimal Last_writeoff
                { get { return _last_writeoff; } set { _last_writeoff = value; } }
                public decimal Last_copay
                { get { return _last_copay; } set { _last_copay = value; } }
                public decimal Last_deductible
                { get { return _last_deductible; } set { _last_deductible = value; } }
                public decimal Last_coinsurance
                { get { return _last_coinsurance; } set { _last_coinsurance = value; } }
                public decimal Last_withhold
                { get { return _last_withhold; } set { _last_withhold = value; } }

                public bool IsLast_allowedNull { get; set; }
                public bool IsLast_paymentNull { get; set; }
                public bool IsLast_writeoffNull { get; set; }
                public bool IsLast_copayNull { get; set; }
                public bool IsLast_deductibleNull { get; set; }
                public bool IsLast_coinsuranceNull { get; set; }
                public bool IsLast_withholdNull { get; set; }

                public bool Iscorrection
                { get { return _iscorrection; } set { _iscorrection = value; } }

                public Int64 CloseDate
                { get { return _closeDate; } set { _closeDate = value; } }

                public PaymentInsuranceLineNotes LineNotes
                { get { return _LineNotes; } set {
                    
                    if (notesAssigned)
                    {
                        if (_LineNotes != null)
                        {
                            _LineNotes.Dispose();
                            _LineNotes = null;
                        }
                    }
                    _LineNotes = value;
                    notesAssigned = false;
                } }

                public bool IsSplitted
                { get { return _IsSplitted; } set { _IsSplitted = value; } }

                // Properties add on 28-June-2010 by Qutub
                private Int64 _SVCID = 0;
                private Int64 _CLPID = 0;

                private bool _bIsStopCharge = false;

                public Int64 SVCID
                { get { return _SVCID; } set { _SVCID = value; } }


                public Int64 CLPID
                { get { return _CLPID; } set { _CLPID = value; } }

                public bool IsStopCharge
                { get { return _bIsStopCharge; } set { _bIsStopCharge = value; } }

                public bool bIsERAPayment
                { get { return _bIsERAPayment; } set { _bIsERAPayment = value; } }

                public Int64 VoidCloseDate
                { get { return _nVoidCloseDate; } set { _nVoidCloseDate = value; } }

                public Int64 VoidTrayID
                { get { return _nVoidTrayID; } set { _nVoidTrayID = value; } }

                public bool IsVoid
                { get { return _bIsVoid; } set { _bIsVoid = value; } }

                public VoidType VoidType
                { get { return _VoidType; } set { _VoidType = value; } }

                //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  creating properties for storing the PAF Values
                
                public Int64 PAccountID
                { get { return _nPAccountID; } set { _nPAccountID = value; } }

                public Int64 AccountPatientID
                { get { return _nAccountPatientID; } set { _nAccountPatientID = value; } }

                public Int64 GuarantorID
                { get { return _nGuarantorID; } set { _nGuarantorID = value; } }

                //End

                #endregion " Property Procedures "
            }

            public class PaymentInsuranceLines
            {

                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public PaymentInsuranceLines()
                {
                    _innerlist = new ArrayList();
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
                            if (_innerlist != null)
                            {
                                _innerlist.Clear();

                            }
                        }
                    }
                    disposed = true;
                }


                ~PaymentInsuranceLines()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(PaymentInsuranceLine item)
                {
                    _innerlist.Add(item);
                }

                public bool Remove(PaymentInsuranceLine item)
                //Remark - Work Remining for comparision
                {
                    bool result = false;


                    return result;
                }

                public bool RemoveAt(int index)
                {
                    bool result = false;
                    _innerlist.RemoveAt(index);
                    result = true;
                    return result;
                }

                public void Clear()
                {
                    _innerlist.Clear();
                }

                public PaymentInsuranceLine this[int index]
                {
                    get
                    { return (PaymentInsuranceLine)_innerlist[index]; }
                }

                public bool Contains(PaymentInsuranceLine item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(PaymentInsuranceLine item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(PaymentInsuranceLine[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }

            public class EOBInsurancePaymentDetail
            {
                #region "Constructor & Distructor"

                public EOBInsurancePaymentDetail()
                {
                    _LineNotes = new PaymentInsuranceLineNotes();
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
                           
                            if (notesAssigned)
                            {
                                if (_LineNotes != null)
                                {
                                    _LineNotes.Dispose();
                                    _LineNotes = null;
                                }
                            }
                        }
                    }
                    disposed = true;
                }

                ~EOBInsurancePaymentDetail()
                {
                    Dispose(false);
                }

                #endregion

                #region "Variables Declarations"
                private bool notesAssigned = true;
                private Int64 _nEOBPaymentID = 0;
                private Int64 _nEOBID = 0;
                private Int64 _nEOBDtlID = 0;
                private Int64 _nEOBPaymentDetailID = 0;

                private Int64 _nBillingTransactionID = 0;
                private Int64 _nBillingTransactionDetailID = 0;
                private int _nBillingTransactionLineNo = 0;

                private Int64 _nTrackBillingTransactionID = 0;
                private Int64 _nTrackBillingTransactionDetailID = 0;
                private int _nTrackBillingTransactionLineNo = 0;
                private string _sSubClaimNo = "";


                private Int64 _nPatientId = 0;
                private Int64 _DOSFrom = 0;
                private Int64 _DOSTo = 0;
                private string _sCPTCode = "";
                private string _sCPTDescription = "";
                private decimal _dAmount = 0;
                private bool _isNullAmount = true;
                private EOBPaymentType _nPaymentType = EOBPaymentType.None;
                private EOBPaymentSubType _nPaymentSubType = EOBPaymentSubType.None;
                private EOBPaymentSign _nPaySign = EOBPaymentSign.None;
                private EOBPaymentMode _nPayMode = EOBPaymentMode.None;
                private Int64 _nRefEOBPaymentID = 0;
                private Int64 _nRefEOBPaymentDetailID = 0;
                private Int64 _nAccountID = 0;
                private EOBPaymentAccountType _nAccountType = EOBPaymentAccountType.None;
                private Int64 _nMSTAccountID = 0;
                private EOBPaymentAccountType _nMSTAccountType = EOBPaymentAccountType.None;
                private Int64 _nPaymentTrayID = 0;
                private string _sPaymentTrayCode = "";
                private string _sPaymentTrayDescription = "";
                private Int64 _nUserID = 0;
                private string _sUserName = "";
                private DateTime _CreatedDateTime = DateTime.Now;
                private DateTime _ModifiedDateTime = DateTime.Now;
                private Int64 _nClinicID = 0;

                private Int64 _nReserveEOBPaymentID = 0;
                private Int64 _nReserveEOBPaymentDetailID = 0;

                private Int32 _nFinanceLieNo = 0; //Continues line number
                private Int64 _nMainCreditLineID = 0; //Identify main credit line, the line of actual check/cash/etc payment, there may be other credit line of reserve or correction but those are not main credit line
                private bool _IsMainCreditLine = false; //Mark that which one is main credit line
                private bool _IsReserveCreditLine = false;
                private bool _IsCorrectionCreditLine = false;
                private Int32 _nRefFinanceLieNo = 0; //in correction we have to set ref and res id runtime, and to identify which credit line number we have to use, we will use this ref no to identify
                private bool _UseRefFinanceLieNo = false; //it will help above field to identify do we have to use this ref line no or not

                private Int64 _nContactInsID = 0;

                private Int64 _nCloseDate = 0;

                private PaymentInsuranceLineNotes _LineNotes = null;

                private Int64 _nOldRefEOBPaymentID = 0;
                private Int64 _nOldRefEOBPaymentDetailID = 0;
                private Int64 _nOldReserveEOBPaymentID = 0;
                private Int64 _nOldReserveEOBPaymentDetailID = 0;

                private Int64 _nVoidCloseDate = 0;
                private Int64 _nVoidTrayID = 0;
                private bool _bIsVoid = false;
                private VoidType _VoidType = VoidType.None;

                private Int64 _nReserveMSTTransactionID = 0;
                private Int64 _nReserveTransactionID = 0;
                private Int64 _nReservePatientID = 0;

                //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for declaring property variable for PAF

                private Int64 _nPAccountID = 0;
                private Int64 _nAccountPatientID = 0;
                private Int64 _nGuarantorID = 0;

                //End

                #endregion"Variables Declarations"

                #region " Property Procedures "

                public Int64 EOBPaymentID
                { get { return _nEOBPaymentID; } set { _nEOBPaymentID = value; } }
                public Int64 EOBID
                { get { return _nEOBID; } set { _nEOBID = value; } }
                public Int64 EOBDtlID
                { get { return _nEOBDtlID; } set { _nEOBDtlID = value; } }
                public Int64 EOBPaymentDetailID
                { get { return _nEOBPaymentDetailID; } set { _nEOBPaymentDetailID = value; } }

                public Int64 BillingTransactionID
                { get { return _nBillingTransactionID; } set { _nBillingTransactionID = value; } }
                public Int64 BillingTransactionDetailID
                { get { return _nBillingTransactionDetailID; } set { _nBillingTransactionDetailID = value; } }
                public int BillingTransactionLineNo
                { get { return _nBillingTransactionLineNo; } set { _nBillingTransactionLineNo = value; } }

                public Int64 TrackBillingTransactionID
                { get { return _nTrackBillingTransactionID; } set { _nTrackBillingTransactionID = value; } }
                public Int64 TrackBillingTransactionDetailID
                { get { return _nTrackBillingTransactionDetailID; } set { _nTrackBillingTransactionDetailID = value; } }
                public int TrackBillingTransactionLineNo
                { get { return _nTrackBillingTransactionLineNo; } set { _nTrackBillingTransactionLineNo = value; } }
                public string SubClaimNo
                { get { return _sSubClaimNo; } set { _sSubClaimNo = value; } }

                public Int64 PatientID
                { get { return _nPatientId; } set { _nPatientId = value; } }
                public Int64 DOSFrom
                { get { return _DOSFrom; } set { _DOSFrom = value; } }
                public Int64 DOSTo
                { get { return _DOSTo; } set { _DOSTo = value; } }
                public string CPTCode
                { get { return _sCPTCode; } set { _sCPTCode = value; } }
                public string CPTDescription
                { get { return _sCPTDescription; } set { _sCPTDescription = value; } }
                public decimal Amount
                { get { return _dAmount; } set { _dAmount = value; } }
                public bool IsNullAmount
                { get { return _isNullAmount; } set { _isNullAmount = value; } }
                public EOBPaymentType PaymentType
                { get { return _nPaymentType; } set { _nPaymentType = value; } }
                public EOBPaymentSubType PaymentSubType
                { get { return _nPaymentSubType; } set { _nPaymentSubType = value; } }
                public EOBPaymentSign PaySign
                { get { return _nPaySign; } set { _nPaySign = value; } }
                public EOBPaymentMode PayMode
                { get { return _nPayMode; } set { _nPayMode = value; } }
                public Int64 RefEOBPaymentID
                { get { return _nRefEOBPaymentID; } set { _nRefEOBPaymentID = value; } }
                public Int64 RefEOBPaymentDetailID
                { get { return _nRefEOBPaymentDetailID; } set { _nRefEOBPaymentDetailID = value; } }
                public Int64 AccountID
                { get { return _nAccountID; } set { _nAccountID = value; } }
                public EOBPaymentAccountType AccountType
                { get { return _nAccountType; } set { _nAccountType = value; } }
                public Int64 MSTAccountID
                { get { return _nMSTAccountID; } set { _nMSTAccountID = value; } }
                public EOBPaymentAccountType MSTAccountType
                { get { return _nMSTAccountType; } set { _nMSTAccountType = value; } }
                public Int64 PaymentTrayID
                { get { return _nPaymentTrayID; } set { _nPaymentTrayID = value; } }
                public string PaymentTrayCode
                { get { return _sPaymentTrayCode; } set { _sPaymentTrayCode = value; } }
                public string PaymentTrayDescription
                { get { return _sPaymentTrayDescription; } set { _sPaymentTrayDescription = value; } }
                public Int64 UserID
                { get { return _nUserID; } set { _nUserID = value; } }
                public string UserName
                { get { return _sUserName; } set { _sUserName = value; } }
                public DateTime CreatedDateTime
                { get { return _CreatedDateTime; } set { _CreatedDateTime = value; } }
                public DateTime ModifiedDateTime
                { get { return _ModifiedDateTime; } set { _ModifiedDateTime = value; } }
                public Int64 ClinicID
                { get { return _nClinicID; } set { _nClinicID = value; } }
                public Int64 ReserveEOBPaymentID
                { get { return _nReserveEOBPaymentID; } set { _nReserveEOBPaymentID = value; } }
                public Int64 ReserveEOBPaymentDetailID
                { get { return _nReserveEOBPaymentDetailID; } set { _nReserveEOBPaymentDetailID = value; } }

                public Int32 FinanceLieNo
                { get { return _nFinanceLieNo; } set { _nFinanceLieNo = value; } }
                public Int64 MainCreditLineID
                { get { return _nMainCreditLineID; } set { _nMainCreditLineID = value; } }
                public bool IsMainCreditLine
                { get { return _IsMainCreditLine; } set { _IsMainCreditLine = value; } }

                public bool IsReserveCreditLine
                { get { return _IsReserveCreditLine; } set { _IsReserveCreditLine = value; } }
                public bool IsCorrectionCreditLine
                { get { return _IsCorrectionCreditLine; } set { _IsCorrectionCreditLine = value; } }

                public Int32 RefFinanceLieNo
                { get { return _nRefFinanceLieNo; } set { _nRefFinanceLieNo = value; } }
                public bool UseRefFinanceLieNo
                { get { return _UseRefFinanceLieNo; } set { _UseRefFinanceLieNo = value; } }

                public Int64 ContactInsID
                { get { return _nContactInsID; } set { _nContactInsID = value; } }

                public Int64 CloseDate
                { get { return _nCloseDate; } set { _nCloseDate = value; } }

                public PaymentInsuranceLineNotes LineNotes
                { get { return _LineNotes; } set {
                    
                    if (notesAssigned)
                    {
                        if (_LineNotes != null)
                        {
                            _LineNotes.Dispose();
                            _LineNotes = null;
                        }
                    }
                    _LineNotes = value;
                    notesAssigned = false;
                } }

                public Int64 OldRefEOBPaymentID
                { get { return _nOldRefEOBPaymentID; } set { _nOldRefEOBPaymentID = value; } }
                public Int64 OldRefEOBPaymentDetailID
                { get { return _nOldRefEOBPaymentDetailID; } set { _nOldRefEOBPaymentDetailID = value; } }
                public Int64 OldReserveEOBPaymentID
                { get { return _nOldReserveEOBPaymentID; } set { _nOldReserveEOBPaymentID = value; } }
                public Int64 OldReserveEOBPaymentDetailID
                { get { return _nOldReserveEOBPaymentDetailID; } set { _nOldReserveEOBPaymentDetailID = value; } }
                public Int64 VoidCloseDate
                { get { return _nVoidCloseDate; } set { _nVoidCloseDate = value; } }

                public Int64 VoidTrayID
                { get { return _nVoidTrayID; } set { _nVoidTrayID = value; } }

                public bool IsVoid
                { get { return _bIsVoid; } set { _bIsVoid = value; } }

                public VoidType VoidType
                { get { return _VoidType; } set { _VoidType = value; } }

                public Int64 ReserveAssociationMSTTransactionID
                { get { return _nReserveMSTTransactionID; } set { _nReserveMSTTransactionID = value; } }

                public Int64 ReserveAssociationTransactionID
                { get { return _nReserveTransactionID; } set { _nReserveTransactionID = value; } }

                public Int64 ReserveAssociationPatientID
                { get { return _nReservePatientID; } set { _nReservePatientID = value; } }

                //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  creating properties for storing the PAF Values
                public Int64 PAccountID
                { get { return _nPAccountID; } set { _nPAccountID = value; } }

                public Int64 AccountPatientID
                { get { return _nAccountPatientID; } set { _nAccountPatientID = value; } }

                public Int64 GuarantorID
                { get { return _nGuarantorID; } set { _nGuarantorID = value; } }

                //End

                #endregion

            }

            public class EOBInsurancePaymentDetails
            {

                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public EOBInsurancePaymentDetails()
                {
                    _innerlist = new ArrayList();
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
                            if (_innerlist != null)
                            {
                                _innerlist.Clear();
                            }
                        }
                    }
                    disposed = true;
                }


                ~EOBInsurancePaymentDetails()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(EOBInsurancePaymentDetail item)
                {
                    _innerlist.Add(item);
                }

                public bool Remove(EOBInsurancePaymentDetail item)
                //Remark - Work Remining for comparision
                {
                    bool result = false;


                    return result;
                }

                public bool RemoveAt(int index)
                {
                    bool result = false;
                    _innerlist.RemoveAt(index);
                    result = true;
                    return result;
                }

                public void Clear()
                {
                    _innerlist.Clear();
                }

                public EOBInsurancePaymentDetail this[int index]
                {
                    get
                    { return (EOBInsurancePaymentDetail)_innerlist[index]; }
                }

                public bool Contains(EOBInsurancePaymentDetail item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(EOBInsurancePaymentDetail item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(EOBInsurancePaymentDetail[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }

            public class EOBInsuranceReserveDetail
            {
                #region "Constructor & Distructor"

                public EOBInsuranceReserveDetail()
                {
                    _LineNotes = new PaymentInsuranceLineNotes();
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
                           
                            if (notesAssigned)
                            {
                                if (_LineNotes != null)
                                {
                                    _LineNotes.Dispose();
                                    _LineNotes = null;
                                }
                            }
                        }
                    }
                    disposed = true;
                }

                ~EOBInsuranceReserveDetail()
                {
                    Dispose(false);
                }

                #endregion
                

                #region "Variables Declarations"

                private bool notesAssigned = true;
                private Int64 _nEOBPaymentID = 0;
                private Int64 _nEOBID = 0;
                private Int64 _nEOBDtlID = 0;
                private Int64 _nEOBPaymentDetailID = 0;

                private Int64 _nBillingTransactionID = 0;
                private Int64 _nBillingTransactionDetailID = 0;
                private int _nBillingTransactionLineNo = 0;

                private Int64 _nTrackBillingTransactionID = 0;
                private Int64 _nTrackBillingTransactionDetailID = 0;
                private int _nTrackBillingTransactionLineNo = 0;
                private string _sSubClaimNo = "";


                private Int64 _nPatientId = 0;
                private Int64 _DOSFrom = 0;
                private Int64 _DOSTo = 0;
                private string _sCPTCode = "";
                private string _sCPTDescription = "";
                private decimal _dAmount = 0;
                private bool _isNullAmount = true;
                private EOBPaymentType _nPaymentType = EOBPaymentType.None;
                private EOBPaymentSubType _nPaymentSubType = EOBPaymentSubType.None;
                private EOBPaymentSign _nPaySign = EOBPaymentSign.None;
                private EOBPaymentMode _nPayMode = EOBPaymentMode.None;
                private Int64 _nRefEOBPaymentID = 0;
                private Int64 _nRefEOBPaymentDetailID = 0;
                private Int64 _nAccountID = 0;
                private EOBPaymentAccountType _nAccountType = EOBPaymentAccountType.None;
                private Int64 _nMSTAccountID = 0;
                private EOBPaymentAccountType _nMSTAccountType = EOBPaymentAccountType.None;
                private Int64 _nPaymentTrayID = 0;
                private string _sPaymentTrayCode = "";
                private string _sPaymentTrayDescription = "";
                private Int64 _nUserID = 0;
                private string _sUserName = "";
                private DateTime _CreatedDateTime = DateTime.Now;
                private DateTime _ModifiedDateTime = DateTime.Now;
                private Int64 _nClinicID = 0;

                private Int64 _nReserveEOBPaymentID = 0;
                private Int64 _nReserveEOBPaymentDetailID = 0;

                private Int32 _nFinanceLieNo = 0; //Continues line number
                private Int64 _nMainCreditLineID = 0; //Identify main credit line, the line of actual check/cash/etc payment, there may be other credit line of reserve or correction but those are not main credit line
                private bool _IsMainCreditLine = false; //Mark that which one is main credit line
                private bool _IsReserveCreditLine = false;
                private bool _IsCorrectionCreditLine = false;
                private Int32 _nRefFinanceLieNo = 0; //in correction we have to set ref and res id runtime, and to identify which credit line number we have to use, we will use this ref no to identify
                private bool _UseRefFinanceLieNo = false; //it will help above field to identify do we have to use this ref line no or not

                private Int64 _nContactInsID = 0;

                private Int64 _nCloseDate = 0;

                private PaymentInsuranceLineNotes _LineNotes = null;

                private Int64 _nOldRefEOBPaymentID = 0;
                private Int64 _nOldRefEOBPaymentDetailID = 0;
                private Int64 _nOldReserveEOBPaymentID = 0;
                private Int64 _nOldReserveEOBPaymentDetailID = 0;

                private Int64 _nVoidCloseDate = 0;
                private Int64 _nVoidTrayID = 0;
                private bool _bIsVoid = false;
                private VoidType _VoidType = VoidType.None;

                private Int64 _nReserveMSTTransactionID = 0;
                private Int64 _nReserveTransactionID = 0;
                private Int64 _nReservePatientID = 0;

                //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for declaring property variable for PAF

                private Int64 _nPAccountID = 0;
                private Int64 _nAccountPatientID = 0;
                private Int64 _nGuarantorID = 0;

                //End

                #endregion"Variables Declarations"

                #region " Property Procedures "

                public Int64 EOBPaymentID
                { get { return _nEOBPaymentID; } set { _nEOBPaymentID = value; } }
                public Int64 EOBID
                { get { return _nEOBID; } set { _nEOBID = value; } }
                public Int64 EOBDtlID
                { get { return _nEOBDtlID; } set { _nEOBDtlID = value; } }
                public Int64 EOBPaymentDetailID
                { get { return _nEOBPaymentDetailID; } set { _nEOBPaymentDetailID = value; } }

                public Int64 BillingTransactionID
                { get { return _nBillingTransactionID; } set { _nBillingTransactionID = value; } }
                public Int64 BillingTransactionDetailID
                { get { return _nBillingTransactionDetailID; } set { _nBillingTransactionDetailID = value; } }
                public int BillingTransactionLineNo
                { get { return _nBillingTransactionLineNo; } set { _nBillingTransactionLineNo = value; } }

                public Int64 TrackBillingTransactionID
                { get { return _nTrackBillingTransactionID; } set { _nTrackBillingTransactionID = value; } }
                public Int64 TrackBillingTransactionDetailID
                { get { return _nTrackBillingTransactionDetailID; } set { _nTrackBillingTransactionDetailID = value; } }
                public int TrackBillingTransactionLineNo
                { get { return _nTrackBillingTransactionLineNo; } set { _nTrackBillingTransactionLineNo = value; } }
                public string SubClaimNo
                { get { return _sSubClaimNo; } set { _sSubClaimNo = value; } }

                public Int64 PatientID
                { get { return _nPatientId; } set { _nPatientId = value; } }
                public Int64 DOSFrom
                { get { return _DOSFrom; } set { _DOSFrom = value; } }
                public Int64 DOSTo
                { get { return _DOSTo; } set { _DOSTo = value; } }
                public string CPTCode
                { get { return _sCPTCode; } set { _sCPTCode = value; } }
                public string CPTDescription
                { get { return _sCPTDescription; } set { _sCPTDescription = value; } }
                public decimal Amount
                { get { return _dAmount; } set { _dAmount = value; } }
                public bool IsNullAmount
                { get { return _isNullAmount; } set { _isNullAmount = value; } }
                public EOBPaymentType PaymentType
                { get { return _nPaymentType; } set { _nPaymentType = value; } }
                public EOBPaymentSubType PaymentSubType
                { get { return _nPaymentSubType; } set { _nPaymentSubType = value; } }
                public EOBPaymentSign PaySign
                { get { return _nPaySign; } set { _nPaySign = value; } }
                public EOBPaymentMode PayMode
                { get { return _nPayMode; } set { _nPayMode = value; } }
                public Int64 RefEOBPaymentID
                { get { return _nRefEOBPaymentID; } set { _nRefEOBPaymentID = value; } }
                public Int64 RefEOBPaymentDetailID
                { get { return _nRefEOBPaymentDetailID; } set { _nRefEOBPaymentDetailID = value; } }
                public Int64 AccountID
                { get { return _nAccountID; } set { _nAccountID = value; } }
                public EOBPaymentAccountType AccountType
                { get { return _nAccountType; } set { _nAccountType = value; } }
                public Int64 MSTAccountID
                { get { return _nMSTAccountID; } set { _nMSTAccountID = value; } }
                public EOBPaymentAccountType MSTAccountType
                { get { return _nMSTAccountType; } set { _nMSTAccountType = value; } }
                public Int64 PaymentTrayID
                { get { return _nPaymentTrayID; } set { _nPaymentTrayID = value; } }
                public string PaymentTrayCode
                { get { return _sPaymentTrayCode; } set { _sPaymentTrayCode = value; } }
                public string PaymentTrayDescription
                { get { return _sPaymentTrayDescription; } set { _sPaymentTrayDescription = value; } }
                public Int64 UserID
                { get { return _nUserID; } set { _nUserID = value; } }
                public string UserName
                { get { return _sUserName; } set { _sUserName = value; } }
                public DateTime CreatedDateTime
                { get { return _CreatedDateTime; } set { _CreatedDateTime = value; } }
                public DateTime ModifiedDateTime
                { get { return _ModifiedDateTime; } set { _ModifiedDateTime = value; } }
                public Int64 ClinicID
                { get { return _nClinicID; } set { _nClinicID = value; } }
                public Int64 ReserveEOBPaymentID
                { get { return _nReserveEOBPaymentID; } set { _nReserveEOBPaymentID = value; } }
                public Int64 ReserveEOBPaymentDetailID
                { get { return _nReserveEOBPaymentDetailID; } set { _nReserveEOBPaymentDetailID = value; } }

                public Int32 FinanceLieNo
                { get { return _nFinanceLieNo; } set { _nFinanceLieNo = value; } }
                public Int64 MainCreditLineID
                { get { return _nMainCreditLineID; } set { _nMainCreditLineID = value; } }
                public bool IsMainCreditLine
                { get { return _IsMainCreditLine; } set { _IsMainCreditLine = value; } }

                public bool IsReserveCreditLine
                { get { return _IsReserveCreditLine; } set { _IsReserveCreditLine = value; } }
                public bool IsCorrectionCreditLine
                { get { return _IsCorrectionCreditLine; } set { _IsCorrectionCreditLine = value; } }

                public Int32 RefFinanceLieNo
                { get { return _nRefFinanceLieNo; } set { _nRefFinanceLieNo = value; } }
                public bool UseRefFinanceLieNo
                { get { return _UseRefFinanceLieNo; } set { _UseRefFinanceLieNo = value; } }

                public Int64 ContactInsID
                { get { return _nContactInsID; } set { _nContactInsID = value; } }

                public Int64 CloseDate
                { get { return _nCloseDate; } set { _nCloseDate = value; } }

                public PaymentInsuranceLineNotes LineNotes
                { get { return _LineNotes; } set {
                   
                    if (notesAssigned)
                    {
                        if (_LineNotes != null)
                        {
                            _LineNotes.Dispose();
                            _LineNotes = null;
                        }
                    }
                    _LineNotes = value;
                    notesAssigned = false;
                } 
                }

                public Int64 OldRefEOBPaymentID
                { get { return _nOldRefEOBPaymentID; } set { _nOldRefEOBPaymentID = value; } }
                public Int64 OldRefEOBPaymentDetailID
                { get { return _nOldRefEOBPaymentDetailID; } set { _nOldRefEOBPaymentDetailID = value; } }
                public Int64 OldReserveEOBPaymentID
                { get { return _nOldReserveEOBPaymentID; } set { _nOldReserveEOBPaymentID = value; } }
                public Int64 OldReserveEOBPaymentDetailID
                { get { return _nOldReserveEOBPaymentDetailID; } set { _nOldReserveEOBPaymentDetailID = value; } }
                public Int64 VoidCloseDate
                { get { return _nVoidCloseDate; } set { _nVoidCloseDate = value; } }

                public Int64 VoidTrayID
                { get { return _nVoidTrayID; } set { _nVoidTrayID = value; } }

                public bool IsVoid
                { get { return _bIsVoid; } set { _bIsVoid = value; } }

                public VoidType VoidType
                { get { return _VoidType; } set { _VoidType = value; } }

                public Int64 ReserveAssociationMSTTransactionID
                { get { return _nReserveMSTTransactionID; } set { _nReserveMSTTransactionID = value; } }

                public Int64 ReserveAssociationTransactionID
                { get { return _nReserveTransactionID; } set { _nReserveTransactionID = value; } }

                public Int64 ReserveAssociationPatientID
                { get { return _nReservePatientID; } set { _nReservePatientID = value; } }

                //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  creating properties for storing the PAF Values
                public Int64 PAccountID
                { get { return _nPAccountID; } set { _nPAccountID = value; } }

                public Int64 AccountPatientID
                { get { return _nAccountPatientID; } set { _nAccountPatientID = value; } }

                public Int64 GuarantorID
                { get { return _nGuarantorID; } set { _nGuarantorID = value; } }

                //End

                #endregion

            }

            public class EOBInsuranceReserveDetails
            {

                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public EOBInsuranceReserveDetails()
                {
                    _innerlist = new ArrayList();
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
                            if (_innerlist != null)
                            {
                                _innerlist.Clear();
                            }
                        }
                    }
                    disposed = true;
                }


                ~EOBInsuranceReserveDetails()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(EOBInsuranceReserveDetail item)
                {
                    _innerlist.Add(item);
                }

                public bool Remove(EOBInsuranceReserveDetail item)
                //Remark - Work Remining for comparision
                {
                    bool result = false;


                    return result;
                }

                public bool RemoveAt(int index)
                {
                    bool result = false;
                    _innerlist.RemoveAt(index);
                    result = true;
                    return result;
                }

                public void Clear()
                {
                    _innerlist.Clear();
                }

                public EOBInsuranceReserveDetail this[int index]
                {
                    get
                    { return (EOBInsuranceReserveDetail)_innerlist[index]; }
                }

                public bool Contains(EOBInsuranceReserveDetail item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(EOBInsuranceReserveDetail item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(EOBInsuranceReserveDetail[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }

            public class PaymentInsuranceLineResonCode
            {
                #region "Constructor & Distructor"

                public PaymentInsuranceLineResonCode()
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

                ~PaymentInsuranceLineResonCode()
                {
                    Dispose(false);
                }

                #endregion

                #region "Variables Declarations"

                private Int64 _nID = 0;
                private Int64 _nClaimNo = 0;
                private Int64 _nEOBPaymentID = 0;
                private Int64 _nEOBID = 0;
                private Int64 _nEOBPaymentDetailID = 0;

                private Int64 _nBillingTransactionID = 0;
                private Int64 _nBillingTransactionDetailID = 0;

                private Int64 _nTrackBillingTransactionID = 0;
                private Int64 _nTrackBillingTransactionDetailID = 0;
                private string _sSubClaimNo = "";


                private string _sReasonCode = "";
                private string _sReasonDescription = "";
                private decimal _dReasonAmount = 0;
                private bool _isNullReasonAmount = true;
                private Int64 _nClinicID = 0;

                private bool _HasData = false;
                private Int64 _nCloseDate = 0;
                private EOBCommentType _EOBCommentType = EOBCommentType.None;

                private Int32 _ReasonCodeSubType = 0;

                #endregion"Variables Declarations"

                #region " Property Procedures "

                public Int64 ID
                { get { return _nID; } set { _nID = value; } }
                public Int64 ClaimNo
                { get { return _nClaimNo; } set { _nClaimNo = value; } }
                public Int64 EOBPaymentID
                { get { return _nEOBPaymentID; } set { _nEOBPaymentID = value; } }
                public Int64 EOBID
                { get { return _nEOBID; } set { _nEOBID = value; } }
                public Int64 EOBPaymentDetailID
                { get { return _nEOBPaymentDetailID; } set { _nEOBPaymentDetailID = value; } }

                public Int64 BillingTransactionID
                { get { return _nBillingTransactionID; } set { _nBillingTransactionID = value; } }
                public Int64 BillingTransactionDetailID
                { get { return _nBillingTransactionDetailID; } set { _nBillingTransactionDetailID = value; } }

                public Int64 TrackBillingTransactionID
                { get { return _nTrackBillingTransactionID; } set { _nTrackBillingTransactionID = value; } }
                public Int64 TrackBillingTransactionDetailID
                { get { return _nTrackBillingTransactionDetailID; } set { _nTrackBillingTransactionDetailID = value; } }
                public string SubClaimNo
                { get { return _sSubClaimNo; } set { _sSubClaimNo = value; } }

                public string ReasonCode
                { get { return _sReasonCode; } set { _sReasonCode = value; } }
                public string ReasonDescription
                { get { return _sReasonDescription; } set { _sReasonDescription = value; } }
                public decimal ReasonAmount
                { get { return _dReasonAmount; } set { _dReasonAmount = value; } }
                public bool IsNullReasonAmount
                { get { return _isNullReasonAmount; } set { _isNullReasonAmount = value; } }
                public Int64 ClinicID
                { get { return _nClinicID; } set { _nClinicID = value; } }
                public bool HasData
                { get { return _HasData; } set { _HasData = value; } }
                public Int64 CloseDate
                { get { return _nCloseDate; } set { _nCloseDate = value; } }
                public EOBCommentType CommentType
                { get { return _EOBCommentType; } set { _EOBCommentType = value; } }
                // Property add on 28-June-2010 by Qutub
                public Int32 ReasonCodeSubType
                { get { return _ReasonCodeSubType; } set { _ReasonCodeSubType = value; } }

                #endregion

            }

            public class PaymentInsuranceLineResonCodes
            {

                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public PaymentInsuranceLineResonCodes()
                {
                    _innerlist = new ArrayList();
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
                            if (_innerlist != null)
                            {
                                _innerlist.Clear();
                                _innerlist = null;
                            }
                        }
                    }
                    disposed = true;
                }


                ~PaymentInsuranceLineResonCodes()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(PaymentInsuranceLineResonCode item)
                {
                    _innerlist.Add(item);
                }

                public bool Remove(PaymentInsuranceLineResonCode item)
                //Remark - Work Remining for comparision
                {
                    bool result = false;


                    return result;
                }

                public bool RemoveAt(int index)
                {
                    bool result = false;
                    _innerlist.RemoveAt(index);
                    result = true;
                    return result;
                }

                public void Clear()
                {
                    _innerlist.Clear();
                }

                public PaymentInsuranceLineResonCode this[int index]
                {
                    get
                    { return (PaymentInsuranceLineResonCode)_innerlist[index]; }
                }

                public bool Contains(PaymentInsuranceLineResonCode item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(PaymentInsuranceLineResonCode item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(PaymentInsuranceLineResonCode[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }

            public class PaymentInsuranceLineNextAction
            {
                #region "Constructor & Distructor"

                public PaymentInsuranceLineNextAction()
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

                ~PaymentInsuranceLineNextAction()
                {
                    Dispose(false);
                }

                #endregion

                #region "Variables Declarations"

                private Int64 _nID = 0;

                private Int64 _nEOBPaymentID = 0;
                private Int64 _nEOBID = 0;
                private Int64 _nEOBPaymentDetailID = 0;

                private Int64 _nClaimNo = 0;
                private Int64 _nBillingTransactionID = 0;
                private Int64 _nBillingTransactionDetailID = 0;

                private string _sSubClaimNo = "";
                private Int64 _nTrackBillingTransactionID = 0;
                private Int64 _nTrackBillingTransactionDetailID = 0;

                private Int64 _nNextActionPatientInsID = 0;
                private string _nNextActionPatientInsName = "";
                private int _nNextActionPartyNumber = 0;
                private Int64 _nNextActionContactID = 0;

                private string _sNextActionCode = "";
                private string _sNextActionDescription = "";
                private decimal _dNextActionAmount = 0;
                private bool _IsNullNextActionAmount = true;
                private Int64 _nClinicID = 0;
                private bool _HasData = false;
                private PayerMode _nextPartyType = PayerMode.None;

                private bool _HasNextData = false;
                private bool _HasActionData = false;

                private Int64 _nCloseDate = 0;
                private Int64 _nUserID = 0;
                private string _sUserName = "";
                private DateTime _dtDate;

                #endregion"Variables Declarations"

                #region " Property Procedures "

                public Int64 ID
                { get { return _nID; } set { _nID = value; } }
                public Int64 ClaimNo
                { get { return _nClaimNo; } set { _nClaimNo = value; } }
                public Int64 EOBPaymentID
                { get { return _nEOBPaymentID; } set { _nEOBPaymentID = value; } }
                public Int64 EOBID
                { get { return _nEOBID; } set { _nEOBID = value; } }
                public Int64 EOBPaymentDetailID
                { get { return _nEOBPaymentDetailID; } set { _nEOBPaymentDetailID = value; } }
                public Int64 BillingTransactionID
                { get { return _nBillingTransactionID; } set { _nBillingTransactionID = value; } }
                public Int64 BillingTransactionDetailID
                { get { return _nBillingTransactionDetailID; } set { _nBillingTransactionDetailID = value; } }
                public Int64 NextActionPatientInsID
                { get { return _nNextActionPatientInsID; } set { _nNextActionPatientInsID = value; } }
                public string NextActionPatientInsName
                { get { return _nNextActionPatientInsName; } set { _nNextActionPatientInsName = value; } }
                public int NextActionPartyNumber
                { get { return _nNextActionPartyNumber; } set { _nNextActionPartyNumber = value; } }
                public Int64 NextActionContactID
                { get { return _nNextActionContactID; } set { _nNextActionContactID = value; } }
                public string NextActionCode
                { get { return _sNextActionCode; } set { _sNextActionCode = value; } }
                public string NextActionDescription
                { get { return _sNextActionDescription; } set { _sNextActionDescription = value; } }
                public decimal NextActionAmount
                { get { return _dNextActionAmount; } set { _dNextActionAmount = value; } }
                public bool IsNullNextActionAmount
                { get { return _IsNullNextActionAmount; } set { _IsNullNextActionAmount = value; } }
                public Int64 ClinicID
                { get { return _nClinicID; } set { _nClinicID = value; } }
                public bool HasData
                { get { return _HasData; } set { _HasData = value; } }
                public PayerMode NextPartyType
                { get { return _nextPartyType; } set { _nextPartyType = value; } }

                public bool HasNextData
                { get { return _HasNextData; } set { _HasNextData = value; } }
                public bool HasActionData
                { get { return _HasActionData; } set { _HasActionData = value; } }


                // Properties added by Pankaj Bedse on 29012010
                public Int64 CloseDate
                { get { return _nCloseDate; } set { _nCloseDate = value; } }
                public Int64 UserID
                { get { return _nUserID; } set { _nUserID = value; } }
                public string UserName
                { get { return _sUserName; } set { _sUserName = value; } }
                public DateTime LastUpdated
                { get { return _dtDate; } set { _dtDate = value; } }

                public string SubClaimNo
                { get { return _sSubClaimNo; } set { _sSubClaimNo = value; } }
                public Int64 TrackBillingTransactionID
                { get { return _nTrackBillingTransactionID; } set { _nTrackBillingTransactionID = value; } }
                public Int64 TrackBillingTransactionDetailID
                { get { return _nTrackBillingTransactionDetailID; } set { _nTrackBillingTransactionDetailID = value; } }

                Int64 _nHSTID = 0;
                public Int64 nHSTID
                { get { return _nHSTID; } set { _nHSTID = value; } }

                #endregion

            }

            public class PaymentInsuranceLineNextActions
            {

                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public PaymentInsuranceLineNextActions()
                {
                    _innerlist = new ArrayList();
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
                            if (_innerlist != null)
                            {
                                _innerlist.Clear();
                            }
                        }
                    }
                    disposed = true;
                }


                ~PaymentInsuranceLineNextActions()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(PaymentInsuranceLineNextAction item)
                {
                    _innerlist.Add(item);
                }

                public bool Remove(PaymentInsuranceLineNextAction item)
                //Remark - Work Remining for comparision
                {
                    bool result = false;


                    return result;
                }

                public bool RemoveAt(int index)
                {
                    bool result = false;
                    _innerlist.RemoveAt(index);
                    result = true;
                    return result;
                }

                public void Clear()
                {
                    _innerlist.Clear();
                }

                public PaymentInsuranceLineNextAction this[int index]
                {
                    get
                    { return (PaymentInsuranceLineNextAction)_innerlist[index]; }
                }

                public bool Contains(PaymentInsuranceLineNextAction item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(PaymentInsuranceLineNextAction item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(PaymentInsuranceLineNextAction[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }

            public class PaymentInsuranceLineNote
            {
                #region "Constructor & Distructor"

                public PaymentInsuranceLineNote()
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

                ~PaymentInsuranceLineNote()
                {
                    Dispose(false);
                }

                #endregion

                #region "Variables Declarations"

                private Int64 _nID = 0;
                private Int64 _nEOBPaymentID = 0;
                private Int64 _nEOBID = 0;
                private Int64 _nEOBPaymentDetailID = 0;

                private Int64 _nClaimNo = 0;
                private Int64 _nBillingTransactionID = 0;
                private Int64 _nBillingTransactionDetailID = 0;

                private string _sSubClaimNo = "";
                private Int64 _nTrackBillingTransactionID = 0;
                private Int64 _nTrackBillingTransactionDetailID = 0;
                private int _nTrackBillingTransactionLineNo = 0;

                private string _sReasonCode = "";
                private string _sReasonDescription = "";
                private decimal _dReasonAmount = 0;
                private bool _bIncludeOnPrint = false;

                private Int64 _nClinicID = 0;

                private EOBPaymentType _nPaymentNoteType = EOBPaymentType.None;
                private EOBPaymentSubType _nPaymentNoteSubType = EOBPaymentSubType.None;
                private bool _HasData = false;
                private Int64 _nCloseDate = 0;
                private Int64 _nUserId = 0;

                #endregion"Variables Declarations"

                #region " Property Procedures "

                public Int64 ID
                { get { return _nID; } set { _nID = value; } }
                public Int64 ClaimNo
                { get { return _nClaimNo; } set { _nClaimNo = value; } }
                public Int64 EOBPaymentID
                { get { return _nEOBPaymentID; } set { _nEOBPaymentID = value; } }
                public Int64 EOBID
                { get { return _nEOBID; } set { _nEOBID = value; } }
                public Int64 EOBPaymentDetailID
                { get { return _nEOBPaymentDetailID; } set { _nEOBPaymentDetailID = value; } }
                public Int64 BillingTransactionID
                { get { return _nBillingTransactionID; } set { _nBillingTransactionID = value; } }
                public Int64 BillingTransactionDetailID
                { get { return _nBillingTransactionDetailID; } set { _nBillingTransactionDetailID = value; } }
                public string Code
                { get { return _sReasonCode; } set { _sReasonCode = value; } }
                public string Description
                { get { return _sReasonDescription; } set { _sReasonDescription = value; } }
                public decimal Amount
                { get { return _dReasonAmount; } set { _dReasonAmount = value; } }
                public Int64 ClinicID
                { get { return _nClinicID; } set { _nClinicID = value; } }
                public EOBPaymentType PaymentNoteType
                { get { return _nPaymentNoteType; } set { _nPaymentNoteType = value; } }
                public EOBPaymentSubType PaymentNoteSubType
                { get { return _nPaymentNoteSubType; } set { _nPaymentNoteSubType = value; } }
                public bool HasData
                { get { return _HasData; } set { _HasData = value; } }
                public bool IncludeOnPrint
                { get { return _bIncludeOnPrint; } set { _bIncludeOnPrint = value; } }

                public string SubClaimNo
                { get { return _sSubClaimNo; } set { _sSubClaimNo = value; } }
                public Int64 TrackBillingTransactionID
                { get { return _nTrackBillingTransactionID; } set { _nTrackBillingTransactionID = value; } }
                public Int64 TrackBillingTransactionDetailID
                { get { return _nTrackBillingTransactionDetailID; } set { _nTrackBillingTransactionDetailID = value; } }
                public int TrackBillingTransactionLineNo
                { get { return _nTrackBillingTransactionLineNo; } set { _nTrackBillingTransactionLineNo = value; } }
                public Int64 CloseDate
                { get { return _nCloseDate; } set { _nCloseDate = value; } }
                public Int64 UserId
                { get { return _nUserId; } set { _nUserId = value; } }


                #endregion

            }

            public class PaymentInsuranceLineNotes
            {

                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public PaymentInsuranceLineNotes()
                {
                    _innerlist = new ArrayList();
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
                            if (_innerlist != null)
                            {
                                _innerlist.Clear();
                            }
                        }
                    }
                    disposed = true;
                }


                ~PaymentInsuranceLineNotes()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(PaymentInsuranceLineNote item)
                {
                    _innerlist.Add(item);
                }

                public bool Remove(PaymentInsuranceLineNote item)
                //Remark - Work Remining for comparision
                {
                    bool result = false;


                    return result;
                }

                public bool RemoveAt(int index)
                {
                    bool result = false;
                    _innerlist.RemoveAt(index);
                    result = true;
                    return result;
                }

                public void Clear()
                {
                    _innerlist.Clear();
                }

                public PaymentInsuranceLineNote this[int index]
                {
                    get
                    { return (PaymentInsuranceLineNote)_innerlist[index]; }
                }

                public bool Contains(PaymentInsuranceLineNote item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(PaymentInsuranceLineNote item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(PaymentInsuranceLineNote[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }


            public class EOBInsurancePaymentMasterAllocationLine
            {
                #region "Constructor & Distructor"

                public EOBInsurancePaymentMasterAllocationLine()
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

                ~EOBInsurancePaymentMasterAllocationLine()
                {
                    Dispose(false);
                }

                #endregion

                #region "Variables Declarations"

                private Int64 _nEOBPaymentID = 0;
                private Int64 _nEOBID = 0;
                private Int64 _nEOBDtlID = 0;
                private Int64 _nEOBPaymentDetailID = 0;

                private Int64 _nBillingTransactionID = 0;
                private Int64 _nBillingTransactionDetailID = 0;
                private int _nBillingTransactionLineNo = 0;


                private Int64 _nTrackBillingTransactionID = 0;
                private Int64 _nTrackBillingTransactionDetailID = 0;
                private int _nTrackBillingTransactionLineNo = 0;

                private Int64 _nPatientId = 0;
                private Int64 _DOSFrom = 0;
                private Int64 _DOSTo = 0;
                private string _sCPTCode = "";
                private string _sCPTDescription = "";
                private decimal _dAmount = 0;
                private decimal _dRemainingAmount = 0;
                private bool _isNullAmount = true;
                private EOBPaymentType _nPaymentType = EOBPaymentType.None;
                private EOBPaymentSubType _nPaymentSubType = EOBPaymentSubType.None;
                private EOBPaymentSign _nPaySign = EOBPaymentSign.None;
                private EOBPaymentMode _nPayMode = EOBPaymentMode.None;
                private Int64 _nRefEOBPaymentID = 0;
                private Int64 _nRefEOBPaymentDetailID = 0;
                private Int64 _nAccountID = 0;
                private EOBPaymentAccountType _nAccountType = EOBPaymentAccountType.None;
                private Int64 _nMSTAccountID = 0;
                private EOBPaymentAccountType _nMSTAccountType = EOBPaymentAccountType.None;
                private Int64 _nPaymentTrayID = 0;
                private string _sPaymentTrayCode = "";
                private string _sPaymentTrayDescription = "";
                private Int64 _nUserID = 0;
                private string _sUserName = "";
                private DateTime _CreatedDateTime = DateTime.Now;
                private DateTime _ModifiedDateTime = DateTime.Now;
                private Int64 _nClinicID = 0;

                private Int64 _nReserveEOBPaymentID = 0;
                private Int64 _nReserveEOBPaymentDetailID = 0;

                private Int32 _nFinanceLieNo = 0; //Continues line number
                private Int64 _nMainCreditLineID = 0; //Identify main credit line, the line of actual check/cash/etc payment, there may be other credit line of reserve or correction but those are not main credit line
                private bool _IsMainCreditLine = false; //Mark that which one is main credit line
                private bool _IsReserveCreditLine = false;
                private bool _IsCorrectionCreditLine = false;
                private Int32 _nRefFinanceLieNo = 0; //in correction we have to set ref and res id runtime, and to identify which credit line number we have to use, we will use this ref no to identify
                private bool _UseRefFinanceLieNo = false; //it will help above field to identify do we have to use this ref line no or not

                private Int64 _nContactInsID = 0;

                private Int64 _nOldRefEOBPaymentID = 0;
                private Int64 _nOldRefEOBPaymentDetailID = 0;
                private Int64 _nOldReserveEOBPaymentID = 0;
                private Int64 _nOldReserveEOBPaymentDetailID = 0;
                private Int64 _InsuranceCompanyID = 0;

                //Added by Subashish_b on 22/Feb /2011 (integration made on date-10/May/2011) for declaring variable for storing PAF Value
                private Int64 _nPAccountID = 0;
                private Int64 _nGuarantorID = 0;
                private Int64 _nAccountPatientID = 0;
                //End

                #endregion"Variables Declarations"

                #region " Property Procedures "

                public Int64 EOBPaymentID
                { get { return _nEOBPaymentID; } set { _nEOBPaymentID = value; } }
                public Int64 EOBID
                { get { return _nEOBID; } set { _nEOBID = value; } }
                public Int64 EOBDtlID
                { get { return _nEOBDtlID; } set { _nEOBDtlID = value; } }
                public Int64 EOBPaymentDetailID
                { get { return _nEOBPaymentDetailID; } set { _nEOBPaymentDetailID = value; } }
                public Int64 BillingTransactionID
                { get { return _nBillingTransactionID; } set { _nBillingTransactionID = value; } }
                public Int64 BillingTransactionDetailID
                { get { return _nBillingTransactionDetailID; } set { _nBillingTransactionDetailID = value; } }
                public int BillingTransactionLineNo
                { get { return _nBillingTransactionLineNo; } set { _nBillingTransactionLineNo = value; } }
                public Int64 PatientID
                { get { return _nPatientId; } set { _nPatientId = value; } }
                public Int64 DOSFrom
                { get { return _DOSFrom; } set { _DOSFrom = value; } }
                public Int64 DOSTo
                { get { return _DOSTo; } set { _DOSTo = value; } }
                public string CPTCode
                { get { return _sCPTCode; } set { _sCPTCode = value; } }
                public string CPTDescription
                { get { return _sCPTDescription; } set { _sCPTDescription = value; } }
                
                public decimal Amount
                { get { return _dAmount; } set { _dAmount = value; } }


                public decimal RemainingAmount
                { get { return _dRemainingAmount; } set { _dRemainingAmount = value; } }

                

                public bool IsNullAmount
                { get { return _isNullAmount; } set { _isNullAmount = value; } }

                private bool _isNull_dDBReserveAmount = true;
                private decimal _dDBReserveAmount = 0;
                public decimal DBReserveAmount
                { get { return _dDBReserveAmount; } set { _dDBReserveAmount = value; } }
                public bool IsNull_dDBReserveAmount
                { get { return _isNull_dDBReserveAmount; } set { _isNull_dDBReserveAmount = value; } }

                public EOBPaymentType PaymentType
                { get { return _nPaymentType; } set { _nPaymentType = value; } }
                public EOBPaymentSubType PaymentSubType
                { get { return _nPaymentSubType; } set { _nPaymentSubType = value; } }
                public EOBPaymentSign PaySign
                { get { return _nPaySign; } set { _nPaySign = value; } }
                public EOBPaymentMode PayMode
                { get { return _nPayMode; } set { _nPayMode = value; } }
                public Int64 RefEOBPaymentID
                { get { return _nRefEOBPaymentID; } set { _nRefEOBPaymentID = value; } }
                public Int64 RefEOBPaymentDetailID
                { get { return _nRefEOBPaymentDetailID; } set { _nRefEOBPaymentDetailID = value; } }
                public Int64 AccountID
                { get { return _nAccountID; } set { _nAccountID = value; } }
                public EOBPaymentAccountType AccountType
                { get { return _nAccountType; } set { _nAccountType = value; } }
                public Int64 MSTAccountID
                { get { return _nMSTAccountID; } set { _nMSTAccountID = value; } }
                public EOBPaymentAccountType MSTAccountType
                { get { return _nMSTAccountType; } set { _nMSTAccountType = value; } }
                public Int64 PaymentTrayID
                { get { return _nPaymentTrayID; } set { _nPaymentTrayID = value; } }
                public string PaymentTrayCode
                { get { return _sPaymentTrayCode; } set { _sPaymentTrayCode = value; } }
                public string PaymentTrayDescription
                { get { return _sPaymentTrayDescription; } set { _sPaymentTrayDescription = value; } }
                public Int64 UserID
                { get { return _nUserID; } set { _nUserID = value; } }
                public string UserName
                { get { return _sUserName; } set { _sUserName = value; } }
                public DateTime CreatedDateTime
                { get { return _CreatedDateTime; } set { _CreatedDateTime = value; } }
                public DateTime ModifiedDateTime
                { get { return _ModifiedDateTime; } set { _ModifiedDateTime = value; } }
                public Int64 ClinicID
                { get { return _nClinicID; } set { _nClinicID = value; } }
                public Int64 ReserveEOBPaymentID
                { get { return _nReserveEOBPaymentID; } set { _nReserveEOBPaymentID = value; } }
                public Int64 ReserveEOBPaymentDetailID
                { get { return _nReserveEOBPaymentDetailID; } set { _nReserveEOBPaymentDetailID = value; } }

                public Int32 FinanceLieNo
                { get { return _nFinanceLieNo; } set { _nFinanceLieNo = value; } }
                public Int64 MainCreditLineID
                { get { return _nMainCreditLineID; } set { _nMainCreditLineID = value; } }
                public bool IsMainCreditLine
                { get { return _IsMainCreditLine; } set { _IsMainCreditLine = value; } }

                public bool IsReserveCreditLine
                { get { return _IsReserveCreditLine; } set { _IsReserveCreditLine = value; } }
                public bool IsCorrectionCreditLine
                { get { return _IsCorrectionCreditLine; } set { _IsCorrectionCreditLine = value; } }

                public Int32 RefFinanceLieNo
                { get { return _nRefFinanceLieNo; } set { _nRefFinanceLieNo = value; } }
                public bool UseRefFinanceLieNo
                { get { return _UseRefFinanceLieNo; } set { _UseRefFinanceLieNo = value; } }

                public Int64 ContactInsID
                { get { return _nContactInsID; } set { _nContactInsID = value; } }

                public Int64 TrackBillingTransactionID
                { get { return _nTrackBillingTransactionID; } set { _nTrackBillingTransactionID = value; } }
                public Int64 TrackBillingTransactionDetailID
                { get { return _nTrackBillingTransactionDetailID; } set { _nTrackBillingTransactionDetailID = value; } }
                public int TrackBillingTransactionLineNo
                { get { return _nTrackBillingTransactionLineNo; } set { _nTrackBillingTransactionLineNo = value; } }

                public Int64 OldRefEOBPaymentID
                { get { return _nOldRefEOBPaymentID; } set { _nOldRefEOBPaymentID = value; } }
                public Int64 OldRefEOBPaymentDetailID
                { get { return _nOldRefEOBPaymentDetailID; } set { _nOldRefEOBPaymentDetailID = value; } }
                public Int64 OldReserveEOBPaymentID
                { get { return _nOldReserveEOBPaymentID; } set { _nOldReserveEOBPaymentID = value; } }
                public Int64 OldReserveEOBPaymentDetailID
                { get { return _nOldReserveEOBPaymentDetailID; } set { _nOldReserveEOBPaymentDetailID = value; } }

                public Int64 InsuranceCompanyID
                {get { return _InsuranceCompanyID; } set { _InsuranceCompanyID = value; }}

                //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  creating properties for storing the PAF Values
                public Int64 PAccountID
                { get { return _nPAccountID; } set { _nPAccountID = value; } }
                public Int64 GuarantorID
                { get { return _nGuarantorID; } set { _nGuarantorID = value; } }
                public Int64 AccountPatientID
                { get { return _nAccountPatientID; } set { _nAccountPatientID = value; } }
                //End

                #endregion

            }

            public class EOBInsurancePaymentMasterAllocationLines
            {

                private ArrayList _innerlist;

                #region "Constructor & Destructor"

                public EOBInsurancePaymentMasterAllocationLines()
                {
                    this._innerlist = new ArrayList();
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
                            if (_innerlist != null)
                            {
                                _innerlist.Clear();
                            }
                        }
                    }
                    disposed = true;
                }


                ~EOBInsurancePaymentMasterAllocationLines()
                {
                    Dispose(false);
                }
                #endregion

                

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return this._innerlist.Count; }
                }

                public void Add(EOBInsurancePaymentMasterAllocationLine item)
                {
                    this._innerlist.Add(item);
                   
                }


                public EOBInsurancePaymentMasterAllocationLines Copy()
                {
                    EOBInsurancePaymentMasterAllocationLines newEOBInsurancePaymentMasterAllocationLines = new EOBInsurancePaymentMasterAllocationLines();
                    EOBInsurancePaymentMasterAllocationLine oObj = null;

                    for (int i = 0; i <= _innerlist.Count - 1; i++)
                    {
                        #region "Set Object"
                        oObj = new EOBInsurancePaymentMasterAllocationLine();
                        oObj.AccountID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).AccountID;
                        oObj.AccountType = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).AccountType;
                        oObj.Amount = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).Amount;
                        oObj.BillingTransactionID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).BillingTransactionID;
                        oObj.BillingTransactionDetailID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).BillingTransactionDetailID;
                        oObj.BillingTransactionLineNo = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).BillingTransactionLineNo;
                        oObj.ClinicID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).ClinicID;
                        oObj.ContactInsID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).ContactInsID;
                        oObj.CPTCode = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).CPTCode;
                        oObj.CPTDescription = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).CPTDescription;
                        oObj.CreatedDateTime = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).CreatedDateTime;
                        oObj.DBReserveAmount = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).DBReserveAmount;
                        oObj.DOSFrom = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).DOSFrom;
                        oObj.DOSTo = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).DOSTo;
                        oObj.EOBID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).EOBID;
                        oObj.EOBDtlID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).EOBDtlID;
                        oObj.EOBPaymentID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).EOBPaymentID;
                        oObj.EOBPaymentDetailID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).EOBPaymentDetailID;
                        oObj.FinanceLieNo = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).FinanceLieNo;
                        oObj.IsCorrectionCreditLine = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).IsCorrectionCreditLine;
                        oObj.IsMainCreditLine = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).IsMainCreditLine;
                        oObj.IsNull_dDBReserveAmount = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).IsNull_dDBReserveAmount;
                        oObj.IsNullAmount = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).IsNullAmount;
                        oObj.IsReserveCreditLine = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).IsReserveCreditLine;
                        oObj.MainCreditLineID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).MainCreditLineID;
                        oObj.ModifiedDateTime = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).ModifiedDateTime;
                        oObj.MSTAccountID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).MSTAccountID;
                        oObj.MSTAccountType = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).MSTAccountType;
                        oObj.OldRefEOBPaymentID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).OldRefEOBPaymentID;
                        oObj.OldRefEOBPaymentDetailID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).OldRefEOBPaymentDetailID;
                        oObj.OldReserveEOBPaymentID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).OldReserveEOBPaymentID;
                        oObj.OldReserveEOBPaymentDetailID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).OldReserveEOBPaymentDetailID;
                        oObj.PatientID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).PatientID;
                        oObj.PaymentType = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).PaymentType;
                        oObj.PaymentSubType = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).PaymentSubType;
                        oObj.PayMode = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).PayMode;
                        oObj.PaymentTrayCode = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).PaymentTrayCode;
                        oObj.PaymentTrayDescription = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).PaymentTrayDescription;
                        oObj.PaymentTrayID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).PaymentTrayID;
                        oObj.PaySign = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).PaySign;
                        oObj.RefEOBPaymentID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).RefEOBPaymentID;
                        oObj.RefEOBPaymentDetailID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).RefEOBPaymentDetailID;
                        oObj.RefFinanceLieNo = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).RefFinanceLieNo;
                        oObj.ReserveEOBPaymentID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).ReserveEOBPaymentID;
                        oObj.ReserveEOBPaymentDetailID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).ReserveEOBPaymentDetailID;
                        oObj.TrackBillingTransactionID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).TrackBillingTransactionID;
                        oObj.TrackBillingTransactionDetailID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).TrackBillingTransactionDetailID;
                        oObj.TrackBillingTransactionLineNo = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).TrackBillingTransactionLineNo;
                        oObj.UseRefFinanceLieNo = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).UseRefFinanceLieNo;
                        oObj.UserID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).UserID;
                        oObj.UserName = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).UserName;
                        oObj.InsuranceCompanyID = ((EOBInsurancePaymentMasterAllocationLine)(_innerlist[i])).InsuranceCompanyID;

                        newEOBInsurancePaymentMasterAllocationLines.Add(oObj);
                        oObj = null;
                        #endregion
                    }
                    return newEOBInsurancePaymentMasterAllocationLines;
                }

                public bool Remove(EOBInsurancePaymentMasterAllocationLine item)
                //Remark - Work Remining for comparision
                {
                    bool result = false;


                    return result;
                }

                public bool RemoveAt(int index)
                {
                    bool result = false;
                    this._innerlist.RemoveAt(index);
                    result = true;
                    return result;
                }

                public void Clear()
                {
                    this._innerlist.Clear();
                }

                public EOBInsurancePaymentMasterAllocationLine this[int index]
                {
                    get
                    { return (EOBInsurancePaymentMasterAllocationLine)this._innerlist[index]; }
                }

                public bool Contains(EOBInsurancePaymentMasterAllocationLine item)
                {
                    return this._innerlist.Contains(item);
                }

                public int IndexOf(EOBInsurancePaymentMasterAllocationLine item)
                {
                    return this._innerlist.IndexOf(item);
                }

                public void CopyTo(EOBInsurancePaymentMasterAllocationLine[] array, int index)
                {
                    this._innerlist.CopyTo(array, index);
                }

            }


            #region " Collection class added on 20110329 "

            public class EOBInsurancePaymentSelectedCreditLines
            {

                private ArrayList _innerlist;

                #region "Constructor & Destructor"

                public EOBInsurancePaymentSelectedCreditLines()
                {
                    this._innerlist = new ArrayList();
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
                            if (_innerlist != null)
                            {
                                _innerlist.Clear();
                            }
                        }
                    }
                    disposed = true;
                }


                ~EOBInsurancePaymentSelectedCreditLines()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return this._innerlist.Count; }
                }

                public void Add(EOBInsurancePaymentMasterAllocationLine item)
                {
                    this._innerlist.Add(item);
                }

                public bool Remove(EOBInsurancePaymentMasterAllocationLine item)
                //Remark - Work Remining for comparision
                {
                    bool result = false;


                    return result;
                }

                public bool RemoveAt(int index)
                {
                    bool result = false;
                    this._innerlist.RemoveAt(index);
                    result = true;
                    return result;
                }

                public void Clear()
                {
                    this._innerlist.Clear();
                }

                public EOBInsurancePaymentMasterAllocationLine this[int index]
                {
                    get
                    { return (EOBInsurancePaymentMasterAllocationLine)this._innerlist[index]; }
                }

                public bool Contains(EOBInsurancePaymentMasterAllocationLine item)
                {
                    return this._innerlist.Contains(item);
                }

                public int IndexOf(EOBInsurancePaymentMasterAllocationLine item)
                {
                    return this._innerlist.IndexOf(item);
                }

                public void CopyTo(EOBInsurancePaymentMasterAllocationLine[] array, int index)
                {
                    this._innerlist.CopyTo(array, index);
                }

            }

            #endregion

            //Implement the Patient Refund
            public class InsurancePaymentRefund
            {
                #region "Constructor & Destructor"

                public InsurancePaymentRefund()
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

                ~InsurancePaymentRefund()
                {
                    Dispose(false);
                }

                #endregion

                #region "Variables Declarations"

                private Int64 _nRefundID = 0;
                private string _sRefundTo = "";
                private string _sRefundNotes = "";
                private Int64 _dtRefunddate = 0;
                private decimal _dRefundAmount = 0;
                private bool _bIsVoid = false;
                private Int64 _nVoidCloseDate = 0;
                private Int64 _nVoidTrayID = 0;
                private string _nVoidTrayDescription = "";


                #endregion"Variables Declarations"

                #region " Property Procedures "

                public Int64 RefundID
                { get { return _nRefundID; } set { _nRefundID = value; } }
                public string RefundTo
                { get { return _sRefundTo; } set { _sRefundTo = value; } }
                public string RefundNotes
                { get { return _sRefundNotes; } set { _sRefundNotes = value; } }
                public Int64 Refunddate
                { get { return _dtRefunddate; } set { _dtRefunddate = value; } }
                public decimal RefundAmount
                { get { return _dRefundAmount; } set { _dRefundAmount = value; } }
                public bool IsVoid
                { get { return _bIsVoid; } set { _bIsVoid = value; } }
                public Int64 VoidCloseDate
                { get { return _nVoidCloseDate; } set { _nVoidCloseDate = value; } }
                public Int64 VoidTrayID
                { get { return _nVoidTrayID; } set { _nVoidTrayID = value; } }
                public string VoidTrayDescription
                { get { return _nVoidTrayDescription; } set { _nVoidTrayDescription = value; } }

                public Int64 TransactionID { get; set; }
                public Int64 MasterTransactionID { get; set; }
                public Int64 PatientID { get; set; }
                public string ClaimNo { get; set; }


                #endregion

            }

            public class InsuranceReserveRemainingDetails
            {
                #region "Constructor & Destructor"
                public InsuranceReserveRemainingDetails()
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

                ~InsuranceReserveRemainingDetails()
                {
                    Dispose(false);
                }

                #endregion

                #region "Variable declaration"
                string _InsuranceCompany = "";
                Int64 _InsuranceCompanyID = 0;
                Int64 _PatientID = 0;
                string _PatientName = "";
                string _ClaimNo = "";  
                Int64 _nMSTTransactionID = 0;
                Int64 _nTransactionID = 0;
                decimal _AmountToReserve = 0;
                string _ReserveNote = "";

                #endregion

                #region " Property Procedures "
               
                public Int64 PatientID
                {
                    get { return _PatientID; }
                    set { _PatientID = value; }
                }
                               
                public string PatientName
                {
                    get { return _PatientName; }
                    set { _PatientName = value; }
                }
               
                public string ClaimNo
                {
                    get { return _ClaimNo; }
                    set { _ClaimNo = value; }
                }

                public Int64 MSTTransactionID
                {
                    get { return _nMSTTransactionID; }
                    set { _nMSTTransactionID = value; }
                }
            
                public Int64 TransactionID
                {
                    get { return _nTransactionID; }
                    set { _nTransactionID = value; }
                }

                public string InsuranceCompany
                {
                    get { return _InsuranceCompany; }
                    set { _InsuranceCompany = value; }
                }
                               
                public Int64 InsuranceCompanyID
                {
                    get { return _InsuranceCompanyID; }
                    set { _InsuranceCompanyID = value; }
                }

                public decimal AmountToReserve
                {
                    get { return _AmountToReserve; }
                    set { _AmountToReserve = value; }
                }
                
                public string ReserveNote
                {
                    get { return _ReserveNote; }
                    set { _ReserveNote = value; }
                }
              
                #endregion
            }
        }
    }
}
