using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using gloSettings;

namespace gloBilling
{
    namespace EOBPayment
    {
        public class gloEOBPaymentPatient
        {
            #region "Private Variables"

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private string _databaseConnectionString = "";
            private Int64 _clinicId = 0;
            private Int64 _userId = 0;
            private string _userName = "";
            private string _messageBoxCaption = "";
       //     private string _paymentPrefix = "GPM#";


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

            public gloEOBPaymentPatient(string Databaseconnectionstring)
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

            ~gloEOBPaymentPatient()
            {
                Dispose(false);
            }

            #endregion

            #region " Private & Public Methods "

            public EOBPayment.Common.PaymentPatientClaims GetBillingTransaction_Old(Int64 ClaimNo, Int64 PatientId, bool IsClaimSearch, bool LoadZeroBalance)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtBillingTransaction = null;
                DataTable dtBillingTransactionLines = null;
                EOBPayment.Common.PaymentPatientClaim oPaymentClaim = null;
                EOBPayment.Common.PaymentPatientLine oPaymentLine = null;
                Int64 _claimTranId = 0;
                ArrayList _claimTranIds = new ArrayList();
                EOBPayment.Common.PaymentPatientClaims oPaymentClaims = new global::gloBilling.EOBPayment.Common.PaymentPatientClaims();

                try
                {
                    if (IsClaimSearch == true)
                    {
                        _claimTranId = GetBillingTransactionID(ClaimNo);
                        _claimTranIds.Add(_claimTranId);
                    }
                    else
                    {
                        //_claimTranIds = GetBillingTransactionIDs(PatientId); 
                        _claimTranIds = GetBillingTransactionIDs(PatientId, false);
                    }

                    if (_claimTranIds != null && _claimTranIds.Count > 0)
                    {
                        for (int x = 0; x < _claimTranIds.Count; x++)
                        {
                            _claimTranId = 0;
                            _claimTranId = Convert.ToInt64(_claimTranIds[x]);

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

                                    oPaymentClaim = new global::gloBilling.EOBPayment.Common.PaymentPatientClaim();
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
                                    oDB.Retrive("BL_SELECT_PaymentTransaction_Lines_PatPayment", oParameters, out dtBillingTransactionLines);
                                    oDB.Disconnect();
                                    oParameters.Clear();

                                    if (dtBillingTransactionLines != null && dtBillingTransactionLines.Rows.Count > 0)
                                    {
                                        for (int nTrnLineCntr = 0; nTrnLineCntr < dtBillingTransactionLines.Rows.Count; nTrnLineCntr++)
                                        {
                                            oPaymentLine = new global::gloBilling.EOBPayment.Common.PaymentPatientLine();

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


                                            oPaymentLine.LinePreviousPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousPaid"]);
                                            oPaymentLine.LinePreviousAdjuestment = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousAdjuestment"]);
                                            oPaymentLine.LineBalance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalBalanceAmount"]);
                                            oPaymentLine.LinePatientDue = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientDue"]);

                                            if (LoadZeroBalance == true)
                                            {
                                                oPaymentClaim.CliamLines.Add(oPaymentLine);
                                            }
                                            else
                                            {
                                                if (oPaymentLine.LineBalance != 0)
                                                { oPaymentClaim.CliamLines.Add(oPaymentLine); }
                                            }
                                        }
                                    }
                                    if (dtBillingTransactionLines != null)
                                    {
                                        dtBillingTransactionLines.Dispose();
                                        dtBillingTransactionLines = null;
                                    }

                                    #endregion

                                    if (oPaymentClaim.CliamLines.Count > 0)
                                    {
                                        oPaymentClaims.Add(oPaymentClaim);
                                    }
                                    oPaymentClaim = null;
                                }
                                if (dtBillingTransaction != null)
                                {
                                    dtBillingTransaction.Dispose();
                                    dtBillingTransaction = null;
                                }
                                #endregion " Set Transaction Master Data "
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
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); dtBillingTransaction = null; }
                    if (dtBillingTransactionLines != null) { dtBillingTransactionLines.Dispose(); dtBillingTransactionLines = null; }
                    if (_claimTranIds != null)
                    {
                        _claimTranIds.Clear();
                        _claimTranIds = null;
                    }
                }

                return oPaymentClaims;
            }

            //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for   getting billing transaction with effect of PAF(new Patient Account Feature)
            public EOBPayment.Common.PaymentPatientClaims GetBillingTransaction_PAF(Int64 ClaimNo, Int64 PAccountId, Int64 PatientId, bool IsClaimSearch, bool LoadZeroBalance)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtBillingTransaction = null;
                DataTable dtBillingTransactionLines = null;
                EOBPayment.Common.PaymentPatientClaim oPaymentClaim = null;
                EOBPayment.Common.PaymentPatientLine oPaymentLine = null;
                Int64 _claimTranId = 0;
                ArrayList _claimTranIds = new ArrayList();
                EOBPayment.Common.PaymentPatientClaims oPaymentClaims = new global::gloBilling.EOBPayment.Common.PaymentPatientClaims();

                try
                {
                    if (IsClaimSearch == true)
                    {
                        _claimTranId = GetBillingTransactionID(ClaimNo);
                        _claimTranIds.Add(_claimTranId);
                    }
                    else
                    {
                        //_claimTranIds = GetBillingTransactionIDs(PatientId); 

                        //Code Commented By subashish for PAF
                        // _claimTranIds = GetBillingTransactionIDs(PatientId, false);
                        //end PAF
                        _claimTranIds = GetBillingTransactionIDs_PAF(PAccountId, PatientId, false);

                    }

                    if (_claimTranIds != null && _claimTranIds.Count > 0)
                    {
                        for (int x = 0; x < _claimTranIds.Count; x++)
                        {
                            _claimTranId = 0;
                            _claimTranId = Convert.ToInt64(_claimTranIds[x]);

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



                                    #region "Retrive Billing Transaction Lines Data "

                                    oParameters.Clear();
                                    oParameters.Add("@nTransactionID", Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionID"]), ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nTransactionDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nPatientID", Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nPatientID"]), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDB.Connect(false);
                                    oDB.Retrive("BL_SELECT_PaymentTransaction_Lines_PatPayment", oParameters, out dtBillingTransactionLines);
                                    oDB.Disconnect();
                                    oParameters.Clear();

                                    if (dtBillingTransactionLines != null && dtBillingTransactionLines.Rows.Count > 0)
                                    {
                                        for (int nTrnLineCntr = 0; nTrnLineCntr < dtBillingTransactionLines.Rows.Count; nTrnLineCntr++)
                                        {
                                            if (oPaymentClaim == null)
                                            {
                                                oPaymentClaim = new global::gloBilling.EOBPayment.Common.PaymentPatientClaim();
                                                oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"]);
                                                oPaymentClaim.DisplayClaimNo = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString());
                                                oPaymentClaim.ClaimNoPrefix = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["sCaseNoPrefix"]);
                                                oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionID"]);
                                                oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionDate"]);
                                                oPaymentClaim.PatientID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nPatientID"]);
                                                oPaymentClaim.PatientName = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["PatientName"]);
                                                oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                                                // oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespPartyNumber"]) + "-" + Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]); 
                                            }
                                            else if (oPaymentClaim.DisplayClaimNo.Trim().ToUpper() != Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString()).Trim().ToUpper())
                                            {

                                                if (oPaymentClaim.CliamLines.Count > 0)
                                                {
                                                    oPaymentClaims.Add(oPaymentClaim);
                                                }
                                                oPaymentClaim = null;

                                                oPaymentClaim = new global::gloBilling.EOBPayment.Common.PaymentPatientClaim();

                                                oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);

                                                oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"]);
                                                oPaymentClaim.DisplayClaimNo = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString());
                                                oPaymentClaim.ClaimNoPrefix = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["sCaseNoPrefix"]);
                                                oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionID"]);
                                                oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionDate"]);
                                                oPaymentClaim.PatientID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nPatientID"]);
                                                oPaymentClaim.PatientName = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["PatientName"]);

                                                oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);

                                                //oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespPartyNumber"]) + "-" + Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]); 

                                            }

                                            oPaymentLine = new global::gloBilling.EOBPayment.Common.PaymentPatientLine();
                                            oPaymentLine.PatientID = oPaymentClaim.PatientID;
                                            oPaymentLine.BLTransactionID = oPaymentClaim.BillingTransactionID;
                                            oPaymentLine.BLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDetailID"].ToString());
                                            oPaymentLine.BLTransactionLineNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionLineNo"].ToString());
                                            oPaymentLine.ClaimNumber = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["ClaimNumber"].ToString());
                                            //oPaymentLine.ClaimNumber = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString());
                                            oPaymentLine.DOSFrom = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nFromDate"].ToString());
                                            oPaymentLine.DOSTo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nToDate"].ToString());
                                            oPaymentLine.CPTCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTCode"].ToString());
                                            oPaymentLine.CPTDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTDescription"].ToString());
                                            oPaymentLine.Modifiers = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["Modifier"].ToString());
                                            oPaymentLine.BLInsuranceID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceID"].ToString());
                                            oPaymentLine.BLInsuranceName = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceName"].ToString());
                                            oPaymentLine.BLInsuranceFlag = ((InsuranceTypeFlag)Convert.ToInt32(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceFlag"]));
                                            //Code Added By subashish for Responsible Party
                                            //oPaymentLine.RespParty = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["RespParty"]);
                                            //End Code
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


                                            oPaymentLine.LinePreviousPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousPaid"]);
                                            oPaymentLine.LinePreviousAdjuestment = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousAdjuestment"]);
                                            oPaymentLine.LineBalance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalBalanceAmount"]);
                                            oPaymentLine.LinePatientDue = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientDue"]);
                                            oPaymentLine.LinePreviousPatientPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousPatientPaidAmount"]);




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
                                    if (dtBillingTransactionLines != null)
                                    {
                                        dtBillingTransactionLines.Dispose();
                                        dtBillingTransactionLines = null;
                                    }
                                    #endregion

                                    //if (oPaymentClaim.CliamLines.Count > 0)
                                    //{
                                    //    oPaymentClaims.Add(oPaymentClaim);
                                    //}
                                    //oPaymentClaim = null;
                                }
                                if (dtBillingTransaction != null)
                                {
                                    dtBillingTransaction.Dispose();
                                    dtBillingTransaction = null;
                                }
                                #endregion " Set Transaction Master Data "
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
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); dtBillingTransaction = null; }
                    if (dtBillingTransactionLines != null) { dtBillingTransactionLines.Dispose(); dtBillingTransactionLines = null; }
                    if (_claimTranIds != null)
                    {
                        _claimTranIds.Clear();
                        _claimTranIds = null;
                    }
                }

                return oPaymentClaims;
            }
            //End
            

            public EOBPayment.Common.PaymentPatientClaims GetBillingTransaction(Int64 ClaimNo, Int64 PatientId, bool IsClaimSearch, bool LoadZeroBalance)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtBillingTransaction = null;
                DataTable dtBillingTransactionLines = null;
                EOBPayment.Common.PaymentPatientClaim oPaymentClaim = null;
                EOBPayment.Common.PaymentPatientLine oPaymentLine = null;
                Int64 _claimTranId = 0;
                ArrayList _claimTranIds = new ArrayList();
                EOBPayment.Common.PaymentPatientClaims oPaymentClaims = new global::gloBilling.EOBPayment.Common.PaymentPatientClaims();

                try
                {
                    if (IsClaimSearch == true)
                    {
                        _claimTranId = GetBillingTransactionID(ClaimNo);
                        _claimTranIds.Add(_claimTranId);
                    }
                    else
                    {
                        //_claimTranIds = GetBillingTransactionIDs(PatientId); 
                        _claimTranIds = GetBillingTransactionIDs(PatientId, false);
                    }

                    if (_claimTranIds != null && _claimTranIds.Count > 0)
                    {
                        for (int x = 0; x < _claimTranIds.Count; x++)
                        {
                            _claimTranId = 0;
                            _claimTranId = Convert.ToInt64(_claimTranIds[x]);

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



                                    #region "Retrive Billing Transaction Lines Data "

                                    oParameters.Clear();
                                    oParameters.Add("@nTransactionID", Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionID"]), ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nTransactionDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nPatientID", Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nPatientID"]), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDB.Connect(false);
                                    oDB.Retrive("BL_SELECT_PaymentTransaction_Lines_PatPayment", oParameters, out dtBillingTransactionLines);
                                    oDB.Disconnect();
                                    oParameters.Clear();

                                    if (dtBillingTransactionLines != null && dtBillingTransactionLines.Rows.Count > 0)
                                    {
                                        for (int nTrnLineCntr = 0; nTrnLineCntr < dtBillingTransactionLines.Rows.Count; nTrnLineCntr++)
                                        {
                                            if (oPaymentClaim == null)
                                            {
                                                oPaymentClaim = new global::gloBilling.EOBPayment.Common.PaymentPatientClaim();
                                                oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"]);
                                                oPaymentClaim.DisplayClaimNo = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString());
                                                oPaymentClaim.ClaimNoPrefix = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["sCaseNoPrefix"]);
                                                oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionID"]);
                                                oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionDate"]);
                                                oPaymentClaim.PatientID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nPatientID"]);
                                                oPaymentClaim.PatientName = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["PatientName"]);
                                                //Added by Subashish_b on 22/Mar /2011 (integration made on date-10/May/2011) for  to get the responsible party value        
                                                oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                                                //End
                                            }
                                            else if (oPaymentClaim.DisplayClaimNo.Trim().ToUpper() != Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString()).Trim().ToUpper())
                                            {

                                                if (oPaymentClaim.CliamLines.Count > 0)
                                                {
                                                    oPaymentClaims.Add(oPaymentClaim);
                                                }
                                                oPaymentClaim = null;

                                                oPaymentClaim = new global::gloBilling.EOBPayment.Common.PaymentPatientClaim();
                                                //Added by Subashish_b on 22/Mar /2011 (integration made on date-10/May/2011) for  to get the responsible party value    
                                                oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                                                //End
                                                oPaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"]);
                                                oPaymentClaim.DisplayClaimNo = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString());
                                                oPaymentClaim.ClaimNoPrefix = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["sCaseNoPrefix"]);
                                                oPaymentClaim.BillingTransactionID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionID"]);
                                                oPaymentClaim.BillingTransactionDate = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionDate"]);
                                                oPaymentClaim.PatientID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nPatientID"]);
                                                oPaymentClaim.PatientName = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["PatientName"]);
                                                //Added by Subashish_b on 22/Mar /2011 (integration made on date-10/May/2011) for  to get the responsible party value    
                                                oPaymentClaim.RespParty = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["RespParty"]);
                                                //End
                                            }

                                            oPaymentLine = new global::gloBilling.EOBPayment.Common.PaymentPatientLine();
                                            oPaymentLine.PatientID = oPaymentClaim.PatientID;
                                            oPaymentLine.BLTransactionID = oPaymentClaim.BillingTransactionID;
                                            oPaymentLine.BLTransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDetailID"].ToString());
                                            oPaymentLine.BLTransactionLineNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionLineNo"].ToString());
                                            oPaymentLine.ClaimNumber = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["ClaimNumber"].ToString());
                                            //oPaymentLine.ClaimNumber = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["SplitClaimNo"].ToString());
                                            oPaymentLine.DOSFrom = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nFromDate"].ToString());
                                            oPaymentLine.DOSTo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nToDate"].ToString());
                                            oPaymentLine.CPTCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTCode"].ToString());
                                            oPaymentLine.CPTDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTDescription"].ToString());
                                            oPaymentLine.Modifiers = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["Modifier"].ToString());
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


                                            oPaymentLine.LinePreviousPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousPaid"]);
                                            oPaymentLine.LinePreviousAdjuestment = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousAdjuestment"]);
                                            oPaymentLine.LineBalance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalBalanceAmount"]);
                                            oPaymentLine.LinePatientDue = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PatientDue"]);
                                            oPaymentLine.LinePreviousPatientPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousPatientPaidAmount"]);

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
                                    if (dtBillingTransactionLines != null)
                                    {
                                        dtBillingTransactionLines.Dispose();
                                        dtBillingTransactionLines = null;
                                    }
                                    #endregion

                                    //if (oPaymentClaim.CliamLines.Count > 0)
                                    //{
                                    //    oPaymentClaims.Add(oPaymentClaim);
                                    //}
                                    //oPaymentClaim = null;
                                }
                                if (dtBillingTransaction != null)
                                {
                                    dtBillingTransaction.Dispose();
                                    dtBillingTransaction = null;
                                }
                                #endregion " Set Transaction Master Data "
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
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); dtBillingTransaction = null; }
                    if (dtBillingTransactionLines != null) { dtBillingTransactionLines.Dispose(); dtBillingTransactionLines = null; }
                    if (_claimTranIds != null)
                    {
                        _claimTranIds.Clear();
                        _claimTranIds = null;
                    }
                }

                return oPaymentClaims;
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
                            if (dtBillingTransactionLines != null)
                            {
                                dtBillingTransactionLines.Dispose();
                                dtBillingTransactionLines = null;
                            }
                            #endregion

                        }
                        if (dtBillingTransaction != null)
                        {
                            dtBillingTransaction.Dispose();
                            dtBillingTransaction = null;
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

            public EOBPayment.Common.PaymentPatientClaims GetBillingExistingTransaction(Int64 PatientId, Int64 PaymentID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtBillingTransaction = null;
                DataTable dtBillingTransactionLines = null;
                EOBPayment.Common.PaymentPatientClaim oPaymentClaim = null;
                EOBPayment.Common.PaymentPatientLine oPaymentLine = null;
                Int64 _claimTranId = 0;
                gloGeneralItem.gloItems _claimTranIds = null;
                EOBPayment.Common.PaymentPatientClaims oPaymentClaims = new global::gloBilling.EOBPayment.Common.PaymentPatientClaims();

                try
                {
                    _claimTranIds = GetBillingExistingTransactionIDs(PatientId, PaymentID);

                    if (_claimTranIds != null && _claimTranIds.Count > 0)
                    {
                        for (int x = 0; x < _claimTranIds.Count; x++)
                        {
                            _claimTranId = 0;
                            _claimTranId = Convert.ToInt64(_claimTranIds[x].ID);

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

                                    oPaymentClaim = new global::gloBilling.EOBPayment.Common.PaymentPatientClaim();
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
                                    oDB.Retrive("BL_SELECT_PaymentTransaction_Lines_PatPayment", oParameters, out dtBillingTransactionLines);
                                    oDB.Disconnect();
                                    oParameters.Clear();

                                    if (dtBillingTransactionLines != null && dtBillingTransactionLines.Rows.Count > 0)
                                    {
                                        for (int nTrnLineCntr = 0; nTrnLineCntr < dtBillingTransactionLines.Rows.Count; nTrnLineCntr++)
                                        {
                                            oPaymentLine = new global::gloBilling.EOBPayment.Common.PaymentPatientLine();

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


                                            oPaymentLine.LinePreviousPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousPaid"]);
                                            oPaymentLine.LinePreviousAdjuestment = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PreviousAdjuestment"]);
                                            oPaymentLine.LineBalance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["TotalBalanceAmount"]);

                                            if (_claimTranIds[x].Code == "FILL")
                                            {
                                                oPaymentClaim.CliamLines.Add(oPaymentLine);
                                            }
                                            else
                                            {
                                                if (oPaymentLine.LineBalance != 0)
                                                { oPaymentClaim.CliamLines.Add(oPaymentLine); }
                                            }
                                        }
                                    }
                                    if (dtBillingTransactionLines != null)
                                    {
                                        dtBillingTransactionLines.Dispose();
                                        dtBillingTransactionLines = null;
                                    }
                                    #endregion

                                    if (oPaymentClaim.CliamLines.Count > 0)
                                    {
                                        oPaymentClaims.Add(oPaymentClaim);
                                    }
                                    oPaymentClaim = null;
                                }
                                if (dtBillingTransaction != null)
                                {
                                    dtBillingTransaction.Dispose();
                                    dtBillingTransaction = null;
                                }
                                #endregion " Set Transaction Master Data "
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
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); dtBillingTransaction = null; }
                    if (dtBillingTransactionLines != null) { dtBillingTransactionLines.Dispose(); dtBillingTransactionLines = null; }
                    if (_claimTranIds != null)
                    {
                        _claimTranIds.Clear();
                        _claimTranIds.Dispose();
                        _claimTranIds = null;
                    }
                }

                return oPaymentClaims;
            }

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

            public ArrayList GetBillingTransactionIDs(Int64 PatientID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                DataTable _dt = null;
                string _sqlQuery = "";
                ArrayList _TransactionMstIDs = new ArrayList();

                try
                {
                    if (PatientID > 0)
                    {
                        //_sqlQuery = " SELECT DISTINCT ISNULL(BL_Transaction_MST.nTransactionID,0) AS nTransactionID,ISNULL(BL_Transaction_Lines.nFromDate,0) AS nFromDate " +
                        //            " FROM BL_Transaction_MST INNER JOIN BL_Transaction_Lines ON BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID  " +
                        //            " WHERE     (BL_Transaction_MST.nTransactionID IS NOT NULL) AND " +
                        //            " (BL_Transaction_MST.nTransactionID > 0) AND " +
                        //            " (BL_Transaction_MST.nPatientID = " + PatientID + ") AND " +
                        //            " (BL_Transaction_MST.nClinicID = " + _clinicId + ")" +
                        //            " AND ISNULL(BL_Transaction_MST.bIsVoid,0) = 0 " +
                        //            " ORDER BY nFromDate desc";

                        _sqlQuery = " SELECT DISTINCT ISNULL(BL_Transaction_MST.nTransactionID,0) AS nTransactionID, " +
                        " (SELECT MAX(nFromDate) FROM BL_Transaction_Lines AS FrmTable WITH (NOLOCK)" +
                        " WHERE FrmTable.nTransactionID = BL_Transaction_MST.nTransactionID)AS nFromDate " +
                        " FROM BL_Transaction_MST WITH (NOLOCK) INNER JOIN BL_Transaction_Lines WITH (NOLOCK) ON BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID  " +
                        " WHERE     (BL_Transaction_MST.nTransactionID IS NOT NULL) AND " +
                        " (BL_Transaction_MST.nTransactionID > 0) AND " +
                        " (BL_Transaction_MST.nPatientID = " + PatientID + ") AND " +
                        " (BL_Transaction_MST.nClinicID = " + _clinicId + ")" +
                        " AND ISNULL(BL_Transaction_MST.bIsVoid,0) = 0 " +
                        " ORDER BY nFromDate desc";

                        oDB.Connect(false);
                        oDB.Retrive_Query(_sqlQuery, out _dt);
                        oDB.Disconnect();

                        if (_dt != null && _dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < _dt.Rows.Count; i++)
                            {
                                _TransactionMstIDs.Add(Convert.ToInt64(_dt.Rows[i]["nTransactionID"]));
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
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (_dt != null) { _dt.Dispose(); _dt = null; }
                }

                return _TransactionMstIDs;
            }

            //Added by Subashish_b on 22/Mar /2011 (integration made on date-10/May/2011) for  getting billing transaction copy of previous declared method with little change for PAF
            public ArrayList GetBillingTransactionIDs_PAF(Int64 PAccountID, Int64 PatientID, bool LoadVoid)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                DataTable _dt = null;
                string _sqlQuery = "";
                ArrayList _TransactionMstIDs = new ArrayList();

                try
                {
                    _sqlQuery = " SELECT DISTINCT ISNULL(BL_Transaction_MST.nTransactionID,0) AS nTransactionID, " +
                        " (SELECT MAX(nFromDate) FROM BL_Transaction_Lines AS FrmTable " +
                        " WHERE FrmTable.nTransactionID = BL_Transaction_MST.nTransactionID)AS nFromDate " +
                        " FROM BL_Transaction_MST INNER JOIN BL_Transaction_Lines ON BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID  " +
                        " WHERE BL_Transaction_MST.nTransactionID IS NOT NULL " +
                        " AND BL_Transaction_MST.nTransactionID > 0 " +
                        " AND BL_Transaction_MST.nPAccountID = " + PAccountID +
                        " AND BL_Transaction_MST.nClinicID = " + _clinicId +
                        " AND ISNULL(BL_Transaction_MST.bIsVoid,0) = '" + LoadVoid + "' ";

                    if (PatientID > 0)
                        _sqlQuery += " AND BL_Transaction_MST.nPatientID = " + PatientID;

                    _sqlQuery += " ORDER BY nFromDate desc";

                    oDB.Connect(false);
                    oDB.Retrive_Query(_sqlQuery, out _dt);
                    oDB.Disconnect();

                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dt.Rows.Count; i++)
                        {
                            _TransactionMstIDs.Add(Convert.ToInt64(_dt.Rows[i]["nTransactionID"]));
                        }
                    }

                }
                catch (gloDatabaseLayer.DBException ex)
                { ex.ERROR_Log(ex.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (_dt != null) { _dt.Dispose(); _dt = null; }
                }

                return _TransactionMstIDs;
            }
            //End


            public ArrayList GetBillingTransactionIDs(Int64 PatientID, bool LoadVoid)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                DataTable _dt = null;
                string _sqlQuery = "";
                ArrayList _TransactionMstIDs = new ArrayList();

                try
                {
                    if (PatientID > 0)
                    {
                        _sqlQuery = " SELECT DISTINCT ISNULL(BL_Transaction_MST.nTransactionID,0) AS nTransactionID, " +
                        " (SELECT MAX(nFromDate) FROM BL_Transaction_Lines AS FrmTable WITH (NOLOCK)" +
                        " WHERE FrmTable.nTransactionID = BL_Transaction_MST.nTransactionID)AS nFromDate " +
                        " FROM BL_Transaction_MST WITH (NOLOCK) INNER JOIN BL_Transaction_Lines WITH (NOLOCK) ON BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID  " +
                        " WHERE     (BL_Transaction_MST.nTransactionID IS NOT NULL) AND " +
                        " (BL_Transaction_MST.nTransactionID > 0) AND " +
                        " (BL_Transaction_MST.nPatientID = " + PatientID + ") AND " +
                        " (BL_Transaction_MST.nClinicID = " + _clinicId + ")" +
                        " AND ISNULL(BL_Transaction_MST.bIsVoid,0) = '" + LoadVoid + "' " +
                        " ORDER BY nFromDate desc";

                        oDB.Connect(false);
                        oDB.Retrive_Query(_sqlQuery, out _dt);
                        oDB.Disconnect();

                        if (_dt != null && _dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < _dt.Rows.Count; i++)
                            {
                                _TransactionMstIDs.Add(Convert.ToInt64(_dt.Rows[i]["nTransactionID"]));
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
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (_dt != null) { _dt.Dispose(); _dt = null; }
                }

                return _TransactionMstIDs;
            }

            public gloGeneralItem.gloItems GetBillingExistingTransactionIDs(Int64 PatientID, Int64 PaymentID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                DataTable _dt = null;
                DataTable _dtPayments = null;
                string _sqlQuery = "";
                gloGeneralItem.gloItems _TransactionMstIDs = new gloGeneralItem.gloItems();

                try
                {
                    if (PaymentID > 0 && PatientID > 0)
                    {
                        oDB.Connect(false);

                        #region "Get All Transactions"
                        _sqlQuery = " SELECT DISTINCT ISNULL(BL_Transaction_MST.nTransactionID,0) AS nTransactionID,ISNULL(BL_Transaction_Lines.nFromDate,0) AS nFromDate " +
                                   " FROM BL_Transaction_MST WITH (NOLOCK) INNER JOIN BL_Transaction_Lines WITH (NOLOCK) ON BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID  " +
                                   " WHERE     (BL_Transaction_MST.nTransactionID IS NOT NULL) AND " +
                                   " (BL_Transaction_MST.nTransactionID > 0) AND " +
                                   " (BL_Transaction_MST.nPatientID = " + PatientID + ") AND " +
                                   " (BL_Transaction_MST.nClinicID = " + _clinicId + ")" +
                                   " ORDER BY nFromDate desc";
                        oDB.Retrive_Query(_sqlQuery, out _dt);
                        #endregion

                        #region "Get Payment Transactions"
                        _sqlQuery = "SELECT DISTINCT ISNULL(BL_Transaction_MST.nTransactionID, 0) AS nTransactionID, ISNULL(BL_Transaction_Lines.nFromDate, 0) AS nFromDate " +
                        " FROM BL_Transaction_MST WITH (NOLOCK) INNER JOIN " +
                        " BL_Transaction_Lines WITH (NOLOCK) ON BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID INNER JOIN " +
                        " BL_EOBPayment_DTL WITH (NOLOCK) ON BL_Transaction_Lines.nTransactionID = BL_EOBPayment_DTL.nBillingTransactionID AND  " +
                        " BL_Transaction_Lines.nTransactionDetailID = BL_EOBPayment_DTL.nBillingTransactionDetailID " +
                        " WHERE (BL_Transaction_MST.nTransactionID IS NOT NULL) AND (BL_Transaction_MST.nTransactionID > 0) AND (BL_Transaction_MST.nPatientID = " + PatientID + ") AND  " +
                        " (BL_Transaction_MST.nClinicID = " + _clinicId + ") AND (BL_EOBPayment_DTL.nEOBPaymentID = " + PaymentID + ") ";
                        oDB.Retrive_Query(_sqlQuery, out _dtPayments);
                        #endregion

                        oDB.Disconnect();

                        #region "Filter out payment transaction from normal transactions"
                        if (_dt != null && _dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < _dt.Rows.Count; i++)
                            {
                                gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();
                                oItem.ID = Convert.ToInt64(_dt.Rows[i]["nTransactionID"]);
                                if (_dtPayments != null && _dtPayments.Rows.Count > 0)
                                {
                                    DataRow[] oList = _dtPayments.Select("nTransactionID = " + oItem.ID + "");
                                    if (oList != null && oList.Length > 0)
                                    {
                                        oItem.Code = "FILL";
                                    }
                                    oList = null;
                                }
                                _TransactionMstIDs.Add(oItem);
                               oItem.Dispose();
                               oItem = null;
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
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (_dt != null) { _dt.Dispose(); _dt = null; }
                    if (_dtPayments != null)
                    {
                        _dtPayments.Dispose();
                        _dtPayments = null;
                    }
                }

                return _TransactionMstIDs;
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

            public Int64 SavePatientPayment(EOBPayment.Common.PaymentPatient EOBPaymentPatient, bool IsSaveForVoid, out EOBPayment.Common.EOBPatientPaymentDetails EOBPatientPaymentMasterLines)
            {
                System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(_databaseConnectionString);
                System.Data.SqlClient.SqlCommand _sqlCommand = null;
                System.Data.SqlClient.SqlTransaction _sqlTransaction = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);    
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                object _retVal = null;
                object _valRet = null;
                Int64 _EOBPayId = 0;
                Int64 _EOBId = 0;
                Int64 _EOBDtlId = 0;
                Int64 _EOBPayDtlId = 0;
                Int64 _EOBPayCreditDtlId = 0;
          //       Int64 _EOBPayResId = 0;

                Int64 _EOBVoidPayId = 0;

                bool _UseExtEOBID = false;
                EOBPayment.Common.PaymentPatientLine PaymentPatClaimLine = null;
                EOBPayment.Common.PaymentPatientClaim PaymentPatClaim = null;
                EOBPayment.Common.EOBPatientPaymentDetail EOBPatPayDtl = null;
                EOBPatientPaymentMasterLines = null;

                try
                {

                    if (EOBPaymentPatient != null)
                    {
                        _sqlConnection.Open();
                        _sqlTransaction = _sqlConnection.BeginTransaction();

                        #region " Master Data Save "

                        //nEOBPaymentID,nEOBRefNO,sPayerName,nPayerID,nPayerType,nPaymentMode,sCheckNumber,nCheckAmount,nCheckDate
                        //nMSTAccountID,nMSTAccountType,nPaymentTrayID,sPaymentTrayName,nCloseDate,sCardType,sCardSecurityNo
                        //nCardID,nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                        _EOBPayId = 0;
                        oParameters.Clear();

                        oParameters.Add("@sPaymentNo", EOBPaymentPatient.PaymentNumber, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sPaymentNoPrefix", EOBPaymentPatient.PaymentNumberPefix, ParameterDirection.Input, SqlDbType.VarChar);

                        oParameters.Add("@nEOBPaymentID", EOBPaymentPatient.EOBPaymentID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nEOBRefNO", EOBPaymentPatient.EOBRefNO, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sPayerName", EOBPaymentPatient.PayerName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Unchecked
                        oParameters.Add("@nPayerID", EOBPaymentPatient.PayerID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nPayerType", EOBPaymentPatient.PayerType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nPaymentMode", EOBPaymentPatient.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@sCheckNumber", EOBPaymentPatient.CheckNumber, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Unchecked
                        oParameters.Add("@nCheckAmount", EOBPaymentPatient.CheckAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nCheckDate", EOBPaymentPatient.CheckDate, ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nMSTAccountID", EOBPaymentPatient.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nMSTAccountType", EOBPaymentPatient.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked

                        oParameters.Add("@nPaymentTrayID", EOBPaymentPatient.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sPaymentTrayCode", EOBPaymentPatient.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                        oParameters.Add("@sPaymentTrayDescription", EOBPaymentPatient.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255

                        oParameters.Add("@nCloseDate", EOBPaymentPatient.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sCardType", EOBPaymentPatient.CardType, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sCardSecurityNo", EOBPaymentPatient.CardSecurityNo, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@nCardID", EOBPaymentPatient.CardID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked

                        oParameters.Add("@sAuthorizationNo", EOBPaymentPatient.AuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                        oParameters.Add("@nCardExpDate", EOBPaymentPatient.CardExpiryDate, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),

                        oParameters.Add("@nUserID", EOBPaymentPatient.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sUserName", EOBPaymentPatient.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtCreatedDateTime", EOBPaymentPatient.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtModifiedDateTime", EOBPaymentPatient.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@bIsVoid", EOBPaymentPatient.IsVoid, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nVoidCloseDate", EOBPaymentPatient.VoidCloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nVoidTrayID", EOBPaymentPatient.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nVoidType", EOBPaymentPatient.VoidType, ParameterDirection.Input, SqlDbType.Int);
                        oParameters.Add("@nVoidRefPaymentID", EOBPaymentPatient.VoidRefPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", EOBPaymentPatient.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked

                        oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);

                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                        //Added by Subashish_b on 25/01 /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values while saving
                        oParameters.Add("@nPAccountID", EOBPaymentPatient.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nAccountPatientID", EOBPaymentPatient.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nGuarantorID", EOBPaymentPatient.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                        //End

                        //oDB.Connect(false);
                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandText = "BL_INUP_EOBPayment_MST_PatPayment";

                        int _result = _sqlCommand.ExecuteNonQuery();


                        if (_sqlCommand.Parameters["@nEOBPaymentID"].Value != null)
                        { _retVal = _sqlCommand.Parameters["@nEOBPaymentID"].Value; }
                        else
                        { _retVal = 0; }

                        //oDB.Execute("BL_INUP_EOBPayment_MST_PatPayment", oParameters, out _retVal);
                        //oDB.Disconnect();

                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                        { _EOBPayId = Convert.ToInt64(_retVal); }

                        if (IsSaveForVoid == true)
                        {
                            _EOBVoidPayId = Convert.ToInt64(_retVal);
                        }

                        if (_sqlCommand != null)
                        {
                            _sqlCommand.Parameters.Clear();
                            _sqlCommand.Dispose();
                            _sqlCommand = null;
                        }
                        #region "Master Payment Note"

                        if (EOBPaymentPatient.Notes != null && EOBPaymentPatient.Notes.Count > 0)
                        {
                            Object _RcValue = null;

                            for (int rcInd = 0; rcInd < EOBPaymentPatient.Notes.Count; rcInd++)
                            {
                                _RcValue = null;
                                oParameters.Clear();

                                oParameters.Add("@nID", EOBPaymentPatient.Notes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                oParameters.Add("@nClaimNo", EOBPaymentPatient.Notes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBID", EOBPaymentPatient.Notes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentDetailID", EOBPaymentPatient.Notes[rcInd].EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nBillingTransactionID", EOBPaymentPatient.Notes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                oParameters.Add("@nBillingTransactionDetailID", EOBPaymentPatient.Notes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@sNoteCode", EOBPaymentPatient.Notes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                oParameters.Add("@sNoteDescription", EOBPaymentPatient.Notes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                oParameters.Add("@dNoteAmount", EOBPaymentPatient.Notes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                oParameters.Add("@nPaymentNoteType", EOBPaymentPatient.Notes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                oParameters.Add("@nPaymentNoteSubType", EOBPaymentPatient.Notes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                oParameters.Add("@nIncludeNoteOnPrint", EOBPaymentPatient.Notes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
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

                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                { _RcValue = _sqlCommand.Parameters["@nID"].Value; }
                                else
                                { _RcValue = 0; }
                                if (_sqlCommand != null)
                                {
                                    _sqlCommand.Parameters.Clear();
                                    _sqlCommand.Dispose();
                                    _sqlCommand = null;
                                }
                            }
                        }


                        #endregion

                        #endregion " Master Data Save "

                        #region "Payment Line Master (Credit) Entry, Total Check Amount Entry, but it is in same table "
                        if (EOBPaymentPatient.EOBPatientPaymentLineDetails != null && EOBPaymentPatient.EOBPatientPaymentLineDetails.Count > 0)
                        {
                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentPatient.EOBPatientPaymentLineDetails.Count; clmInsPayLnIndex++)
                            {
                                if (EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex] != null)
                                {
                                    _EOBPayCreditDtlId = 0;
                                    //This credit line detail id we have to setup into debit line where debit line dont have paydtlid
                                    //Suppose we collect new check as well as multiple reserve then
                                    //for reserve amount debit line will get id from form and who dont have id
                                    //we will setup this new id to them
                                    EOBPatPayDtl = EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex];

                                    oParameters.Clear();
                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBID", EOBPatPayDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBDtlID", EOBPatPayDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBPaymentDetailID", EOBPatPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionLineNo", EOBPatPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nPatientID", EOBPatPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nDOSFrom", EOBPatPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nDOSTo", EOBPatPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@sCPTCode", EOBPatPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sCPTDescription", EOBPatPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                    if (EOBPatPayDtl.IsNullAmount == false)
                                    {
                                        oParameters.Add("@nAmount", EOBPatPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    oParameters.Add("@nPaymentType", EOBPatPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentSubType", EOBPatPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaySign", EOBPatPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPayMode", EOBPatPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nAccountID", EOBPatPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nAccountType", EOBPatPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nMSTAccountID", EOBPatPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nMSTAccountType", EOBPatPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentTrayID", EOBPatPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sPaymentTrayCode", EOBPatPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sPaymentTrayDescription", EOBPatPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@nUserID", EOBPatPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sUserName", EOBPatPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@dtCreatedDateTime", EOBPatPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@dtModifiedDateTime", EOBPatPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@nClinicID", EOBPatPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    oParameters.Add("@nRefEOBPaymentID", EOBPatPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBPatPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //..ResEOBPaymentID,ResEOBPaymentDetailID has the reference id's for the reserve amount
                                    oParameters.Add("@nResEOBPaymentID", EOBPatPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nResEOBPaymentDetailID", EOBPatPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);

                                    oParameters.Add("@nContactInsID", EOBPatPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nCreditLineID", EOBPatPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    oParameters.Add("@nCloseDate", EOBPatPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    // Newly added parameters by pankaj
                                    oParameters.Add("@nTrackTrnID", EOBPatPayDtl.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nTrackTrnDtlID", EOBPatPayDtl.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@sSubClaimNo", EOBPatPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),
                                    oParameters.Add("@bIsVoid", EOBPatPayDtl.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                    oParameters.Add("@nVoidCloseDate", EOBPatPayDtl.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                    oParameters.Add("@nVoidTrayID", EOBPatPayDtl.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                    oParameters.Add("@nVoidType", EOBPatPayDtl.VoidType, ParameterDirection.Input, SqlDbType.Int);

                                    oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                    oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);

                                    oParameters.Add("@nOldResEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nOldResEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    //Added by Subashish_b on 25/01 /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values while saving
                                    oParameters.Add("@nPAccountID", EOBPatPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nAccountPatientID", EOBPatPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nGuarantorID", EOBPatPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
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

                                    if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                    { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                    else
                                    { _retVal = 0; }

                                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                    { _EOBPayCreditDtlId = Convert.ToInt64(_retVal); }
                                    if (_sqlCommand != null)
                                    {
                                        _sqlCommand.Parameters.Clear();
                                        _sqlCommand.Dispose();
                                        _sqlCommand = null;
                                    }
                                    #region " Add Line Notes "

                                    if (EOBPatPayDtl.LineNotes != null && EOBPatPayDtl.LineNotes.Count > 0)
                                    {
                                        Object _RcValue = null;

                                        for (int rcInd = 0; rcInd < EOBPatPayDtl.LineNotes.Count; rcInd++)
                                        {
                                            _RcValue = null;
                                            oParameters.Clear();

                                            oParameters.Add("@nID", EOBPatPayDtl.LineNotes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                            oParameters.Add("@nClaimNo", EOBPatPayDtl.LineNotes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBID", EOBPatPayDtl.LineNotes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentDetailID", _EOBPayCreditDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.LineNotes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                            oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.LineNotes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@sNoteCode", EOBPatPayDtl.LineNotes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                            oParameters.Add("@sNoteDescription", EOBPatPayDtl.LineNotes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                            oParameters.Add("@dNoteAmount", EOBPatPayDtl.LineNotes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                            oParameters.Add("@nPaymentNoteType", EOBPatPayDtl.LineNotes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nPaymentNoteSubType", EOBPatPayDtl.LineNotes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nIncludeNoteOnPrint", EOBPatPayDtl.LineNotes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
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
                                            if (_sqlCommand != null)
                                            {
                                                _sqlCommand.Parameters.Clear();
                                                _sqlCommand.Dispose();
                                                _sqlCommand = null;
                                            }
                                        }
                                    }

                                    #endregion " Add Line Notes "


                                    EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex].EOBPaymentID = _EOBPayId;
                                    EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex].EOBPaymentDetailID = Convert.ToInt64(_retVal.ToString());


                                    #region "Assign Credit Line Reference and Finance Line Reference to debit line wherever applicable"
                                    if (EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex].IsMainCreditLine == true)
                                    {
                                        //All Remaining Credit Lines
                                        for (int nAsgn = 0; nAsgn <= EOBPaymentPatient.EOBPatientPaymentLineDetails.Count - 1; nAsgn++)
                                        {
                                            if (EOBPaymentPatient.EOBPatientPaymentLineDetails[nAsgn].IsMainCreditLine == false)
                                            {
                                                EOBPaymentPatient.EOBPatientPaymentLineDetails[nAsgn].MainCreditLineID = _EOBPayCreditDtlId;
                                            }
                                        }
                                        //All Debit Lines
                                        for (int nAsgnClaim = 0; nAsgnClaim <= EOBPaymentPatient.PatientClaims.Count - 1; nAsgnClaim++)
                                        {
                                            for (int nAsgnClmLine = 0; nAsgnClmLine <= EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines.Count - 1; nAsgnClmLine++)
                                            {
                                                for (int nAsgn = 0; nAsgn <= EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails.Count - 1; nAsgn++)
                                                {
                                                    if (EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].IsMainCreditLine == false)
                                                    {
                                                        EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].MainCreditLineID = _EOBPayCreditDtlId;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //Assign Reference Number to debit lines
                                    for (int nAsgnClaim = 0; nAsgnClaim <= EOBPaymentPatient.PatientClaims.Count - 1; nAsgnClaim++)
                                    {
                                        for (int nAsgnClmLine = 0; nAsgnClmLine <= EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines.Count - 1; nAsgnClmLine++)
                                        {
                                            for (int nAsgn = 0; nAsgn <= EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails.Count - 1; nAsgn++)
                                            {
                                                if (EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].RefFinanceLieNo == EOBPatPayDtl.FinanceLieNo)
                                                {
                                                    if (EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].UseRefFinanceLieNo == true)
                                                    {
                                                        EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].RefEOBPaymentID = _EOBPayId;
                                                        EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].RefEOBPaymentDetailID = _EOBPayCreditDtlId;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    //Assign Main Credit Line ID to Direct Transaction Reserve Lines
                                    for (int nResClmLine = 0; nResClmLine <= EOBPaymentPatient.EOBPatientPaymentReserveLineDetail.Count - 1; nResClmLine++)
                                    {
                                        EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].MainCreditLineID = _EOBPayCreditDtlId;
                                        if (EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].UseRefFinanceLieNo == true)
                                        {
                                            //here checking id is 0, bcz its patient payment directlly send to use reserve, other wise as per insurace payment no need
                                            if (EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].RefEOBPaymentID == 0 && EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].RefEOBPaymentDetailID == 0)
                                            {
                                                EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].RefEOBPaymentID = _EOBPayId;
                                                EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].RefEOBPaymentDetailID = _EOBPayCreditDtlId;
                                            }
                                        }
                                    }

                                    #endregion
                                   
                                        EOBPatPayDtl = null;
                                    
                                }
                            }
                        }
                        #endregion

                        #region "Payment Line Master Reserve (Debit) Entry"
                        if (EOBPaymentPatient.EOBPatientPaymentReserveLineDetail != null && EOBPaymentPatient.EOBPatientPaymentReserveLineDetail.Count > 0)
                        {
                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentPatient.EOBPatientPaymentReserveLineDetail.Count; clmInsPayLnIndex++)
                            {
                                if (EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[clmInsPayLnIndex] != null)
                                {
                                    _EOBPayDtlId = 0;
                                    EOBPatPayDtl = EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[clmInsPayLnIndex];

                                    oParameters.Clear();
                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBID", EOBPatPayDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBDtlID", EOBPatPayDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBPaymentDetailID", EOBPatPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionLineNo", EOBPatPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nPatientID", EOBPatPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nDOSFrom", EOBPatPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nDOSTo", EOBPatPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@sCPTCode", EOBPatPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sCPTDescription", EOBPatPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                    if (EOBPatPayDtl.IsNullAmount == false)
                                    {
                                        oParameters.Add("@nAmount", EOBPatPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    oParameters.Add("@nPaymentType", EOBPatPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentSubType", EOBPatPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaySign", EOBPatPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPayMode", EOBPatPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nAccountID", EOBPatPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nAccountType", EOBPatPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nMSTAccountID", EOBPatPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nMSTAccountType", EOBPatPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentTrayID", EOBPatPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sPaymentTrayCode", EOBPatPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sPaymentTrayDescription", EOBPatPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@nUserID", EOBPatPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sUserName", EOBPatPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@dtCreatedDateTime", EOBPatPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@dtModifiedDateTime", EOBPatPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@nClinicID", EOBPatPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    oParameters.Add("@nRefEOBPaymentID", EOBPatPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBPatPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //..ResEOBPaymentID,ResEOBPaymentDetailID has the reference id's for the reserve amount
                                    oParameters.Add("@nResEOBPaymentID", EOBPatPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nResEOBPaymentDetailID", EOBPatPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);

                                    oParameters.Add("@nContactInsID", EOBPatPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nCreditLineID", EOBPatPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    oParameters.Add("@nCloseDate", EOBPatPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    // Newly added parameters by pankaj
                                    oParameters.Add("@nTrackTrnID", EOBPatPayDtl.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nTrackTrnDtlID", EOBPatPayDtl.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@sSubClaimNo", EOBPatPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),
                                    oParameters.Add("@bIsVoid", EOBPatPayDtl.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                    oParameters.Add("@nVoidCloseDate", EOBPatPayDtl.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                    oParameters.Add("@nVoidTrayID", EOBPatPayDtl.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                    oParameters.Add("@nVoidType", EOBPatPayDtl.VoidType, ParameterDirection.Input, SqlDbType.Int);

                                    oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                    oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nOldResEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nOldResEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    //Added by Subashish_b on 25/01 /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values while saving
                                    oParameters.Add("@nPAccountID", EOBPatPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nAccountPatientID", EOBPatPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nGuarantorID", EOBPatPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
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

                                    _result = _sqlCommand.ExecuteNonQuery();

                                    if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                    { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                    else
                                    { _retVal = 0; }


                                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                    { _EOBPayDtlId = Convert.ToInt64(_retVal); }
                                    if (_sqlCommand != null)
                                    {
                                        _sqlCommand.Parameters.Clear();
                                        _sqlCommand.Dispose();
                                        _sqlCommand = null;
                                    }
                                    #region " Add Line Notes "

                                    if (EOBPatPayDtl.LineNotes != null && EOBPatPayDtl.LineNotes.Count > 0)
                                    {
                                        Object _RcValue = null;

                                        for (int rcInd = 0; rcInd < EOBPatPayDtl.LineNotes.Count; rcInd++)
                                        {
                                            _RcValue = null;
                                            oParameters.Clear();

                                            oParameters.Add("@nID", EOBPatPayDtl.LineNotes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                            oParameters.Add("@nClaimNo", EOBPatPayDtl.LineNotes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBID", EOBPatPayDtl.LineNotes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentDetailID", _EOBPayDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.LineNotes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                            oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.LineNotes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@sNoteCode", EOBPatPayDtl.LineNotes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                            oParameters.Add("@sNoteDescription", EOBPatPayDtl.LineNotes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                            oParameters.Add("@dNoteAmount", EOBPatPayDtl.LineNotes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                            oParameters.Add("@nPaymentNoteType", EOBPatPayDtl.LineNotes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nPaymentNoteSubType", EOBPatPayDtl.LineNotes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nIncludeNoteOnPrint", EOBPatPayDtl.LineNotes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
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
                                            if (_sqlCommand != null)
                                            {
                                                _sqlCommand.Parameters.Clear();
                                                _sqlCommand.Dispose();
                                                _sqlCommand = null;
                                            }
                                        }
                                    }

                                    #endregion " Add Line Notes "


                                   
                                        EOBPatPayDtl = null;
                                    
                                }
                            }
                        }
                        #endregion

                        #region " EOB Data Save "

                        if (_EOBPayId > 0 && EOBPaymentPatient.PatientClaims != null && EOBPaymentPatient.PatientClaims.Count > 0)
                        {
                            for (int clmIndex = 0; clmIndex < EOBPaymentPatient.PatientClaims.Count; clmIndex++)
                            {
                                PaymentPatClaim = EOBPaymentPatient.PatientClaims[clmIndex];
                                for (int clmLnIndex = 0; clmLnIndex < PaymentPatClaim.CliamLines.Count; clmLnIndex++)
                                {
                                    if (PaymentPatClaim.CliamLines[clmLnIndex] != null)
                                    {
                                        _EOBDtlId = 0;
                                        PaymentPatClaimLine = PaymentPatClaim.CliamLines[clmLnIndex];

                                        //nEOBPaymentID,nEOBID,nEOBPaymentDetailID,nBillingTransactionID,nBillingTransactionDetailID
                                        //nBillingTransactionLineNo,nPatientID,nDOSFrom,nDOSTo,sCPTCode,sCPTDescription,nAmount,
                                        //nPaymentType,nPaymentSubType,nPaySign,nRefEOBPaymentID,nRefEOBPaymentDetailID,nAccountID
                                        //nAccountType,nMSTAccountID,nMSTAccountType,nPaymentTrayID,nPaymentTrayCode,nPaymentTrayDescription
                                        //nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                                        oParameters.Clear();
                                        #region "EOB Service Line"
                                        if (_UseExtEOBID == true) { PaymentPatClaimLine.mEOBID = _EOBId; }
                                        oParameters.Add("@nEOBID", PaymentPatClaimLine.mEOBID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                        oParameters.Add("@nEOBDtlID", PaymentPatClaimLine.mEOBDtlID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                        oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0)
                                        //oParameters.Add("@nClaimNo", PaymentPatClaimLine.ClaimNumber, ParameterDirection.Input, SqlDbType.Int);//	int
                                        oParameters.Add("@nClaimNo", PaymentPatClaimLine.ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                        oParameters.Add("@nDOSFrom", PaymentPatClaimLine.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                        oParameters.Add("@nDOSTo", PaymentPatClaimLine.DOSTo, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                        oParameters.Add("@sCPTCode", PaymentPatClaimLine.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                        oParameters.Add("@sCPTDescription", PaymentPatClaimLine.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                        if (PaymentPatClaimLine.IsNullCharges == false)
                                        {
                                            oParameters.Add("@dCharges", PaymentPatClaimLine.Charges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullUnit == false)
                                        {
                                            oParameters.Add("@dUnit", PaymentPatClaimLine.Unit, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dUnit", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                        }

                                        if (PaymentPatClaimLine.IsNullTotalCharges == false)
                                        {
                                            oParameters.Add("@dTotalCharges", PaymentPatClaimLine.TotalCharges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dTotalCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullAllowed == false)
                                        {
                                            oParameters.Add("@dAllowed", PaymentPatClaimLine.Allowed, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dAllowed", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullWriteOff == false)
                                        {
                                            oParameters.Add("@dWriteOff", PaymentPatClaimLine.WriteOff, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dWriteOff", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullNonCovered == false)
                                        {
                                            oParameters.Add("@dNotCovered", PaymentPatClaimLine.NonCovered, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dNotCovered", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullInsurance == false)
                                        {
                                            oParameters.Add("@dPayment", PaymentPatClaimLine.InsuranceAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dPayment", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullCopay == false)
                                        {
                                            oParameters.Add("@dCopay", PaymentPatClaimLine.Copay, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dCopay", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullDeductible == false)
                                        {
                                            oParameters.Add("@dDeductible", PaymentPatClaimLine.Deductible, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dDeductible", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullCoInsurance == false)
                                        {
                                            oParameters.Add("@dCoInsurance", PaymentPatClaimLine.CoInsurance, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                        }
                                        else
                                        {
                                            oParameters.Add("@dCoInsurance", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                        }

                                        if (PaymentPatClaimLine.IsNullWithhold == false)
                                        {
                                            oParameters.Add("@dWithhold", PaymentPatClaimLine.Withhold, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dWithhold", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        oParameters.Add("@nPaymentTrayID", PaymentPatClaimLine.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                        oParameters.Add("@sPaymentTrayCode", PaymentPatClaimLine.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                                        oParameters.Add("@sPaymentTrayDescription", PaymentPatClaimLine.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked

                                        oParameters.Add("@nUserID", PaymentPatClaimLine.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                        oParameters.Add("@sUserName", PaymentPatClaimLine.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked
                                        oParameters.Add("@dtCreatedDateTime", PaymentPatClaimLine.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked
                                        oParameters.Add("@dtModifiedDateTime", PaymentPatClaimLine.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked

                                        oParameters.Add("@nPatientID", PaymentPatClaimLine.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                        oParameters.Add("@nInsuraceID", PaymentPatClaimLine.PatInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                        oParameters.Add("@nContactID", PaymentPatClaimLine.InsContactID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked

                                        oParameters.Add("@nClinicID", PaymentPatClaimLine.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                        oParameters.Add("@nEOBType", PaymentPatClaimLine.EOBType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);// int,
                                        oParameters.Add("@nBillingTransactionID", PaymentPatClaimLine.BLTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                        oParameters.Add("@nBillingTransactionDetailID", PaymentPatClaimLine.BLTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                        oParameters.Add("@nBillingTransactionLineNo", PaymentPatClaimLine.BLTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)

                                        oParameters.Add("@bUseExtEOBID", _UseExtEOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                        oParameters.Add("@nCloseDate", PaymentPatClaimLine.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                        oParameters.Add("@nTrackTrnID", PaymentPatClaimLine.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                        oParameters.Add("@nTrackTrnDtlID", PaymentPatClaimLine.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                        oParameters.Add("@sSubClaimNo", PaymentPatClaimLine.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),

                                        oParameters.Add("@bIsVoid", PaymentPatClaimLine.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                        oParameters.Add("@nVoidCloseDate", PaymentPatClaimLine.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                        oParameters.Add("@nVoidTrayID", PaymentPatClaimLine.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                        oParameters.Add("@nVoidType", PaymentPatClaimLine.VoidType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                                        oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                        oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                        oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                        //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values while saving
                                        oParameters.Add("@nPAccountID", EOBPaymentPatient.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                        oParameters.Add("@nAccountPatientID", EOBPaymentPatient.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                        oParameters.Add("@nGuarantorID", EOBPaymentPatient.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                        //End
                                        if (_UseExtEOBID == false) { _UseExtEOBID = true; }
                                        _retVal = null;
                                        _valRet = null;

                                        //oDB.Connect(false);
                                        ////oDB.Execute("BL_INSERT_EOBPayment_EOB", oParameters, out _retVal);
                                        //oDB.Execute("BL_INSERT_EOBPayment_EOB_PatPayment", oParameters, out _retVal, out _valRet);
                                        //oDB.Disconnect();

                                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                                        _sqlCommand.Connection = _sqlConnection;
                                        _sqlCommand.Transaction = _sqlTransaction;
                                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                                        _sqlCommand.CommandText = "BL_INSERT_EOBPayment_EOB_PatPayment";

                                        _result = _sqlCommand.ExecuteNonQuery();

                                        if (_sqlCommand.Parameters["@nEOBID"].Value != null)
                                        { _retVal = _sqlCommand.Parameters["@nEOBID"].Value; }
                                        else
                                        { _retVal = 0; }

                                        if (_sqlCommand.Parameters["@nEOBDtlID"].Value != null)
                                        { _valRet = _sqlCommand.Parameters["@nEOBDtlID"].Value; }
                                        else
                                        { _valRet = 0; }
                                        if (_sqlCommand != null)
                                        {
                                            _sqlCommand.Parameters.Clear();
                                            _sqlCommand.Dispose();
                                            _sqlCommand = null;
                                        }
                                        #endregion

                                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                        { _EOBId = Convert.ToInt64(_retVal); }

                                        if (_valRet != null && Convert.ToString(_valRet).Trim() != "")
                                        { _EOBDtlId = Convert.ToInt64(_valRet); }

                                        #region " Add Line Adjustment Codes "

                                        if (PaymentPatClaimLine.LineAdjestmentCodes != null && PaymentPatClaimLine.LineAdjestmentCodes.Count > 0)
                                        {
                                            Object _RcValue = null;

                                            for (int rcInd = 0; rcInd < PaymentPatClaimLine.LineAdjestmentCodes.Count; rcInd++)
                                            {
                                                _RcValue = null;
                                                oParameters.Clear();

                                                oParameters.Add("@nID", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                                oParameters.Add("@nClaimNo", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentDetailID", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nBillingTransactionID", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                oParameters.Add("@nBillingTransactionDetailID", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sReasonCode", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                oParameters.Add("@sReasonDescription", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),

                                                if (PaymentPatClaimLine.LineAdjestmentCodes[rcInd].IsNullAmount == false)
                                                {
                                                    oParameters.Add("@dReasonAmount", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dReasonAmount", null, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                }

                                                oParameters.Add("@nType", EOBCommentType.Adjustment.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
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
                                                { _RcValue = _sqlCommand.Parameters["@nID"].Value; }
                                                else
                                                { _RcValue = 0; }
                                                if (_sqlCommand != null)
                                                {
                                                    _sqlCommand.Parameters.Clear();
                                                    _sqlCommand.Dispose();
                                                    _sqlCommand = null;
                                                }
                                            }
                                        }

                                        #endregion " Add Line Adjustment Codes "

                                        #region " EOB Financial Service Line Save "

                                        if (_EOBPayId > 0 && PaymentPatClaimLine.EOBPatientPaymentLineDetails != null && PaymentPatClaimLine.EOBPatientPaymentLineDetails.Count > 0)
                                        {
                                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < PaymentPatClaimLine.EOBPatientPaymentLineDetails.Count; clmInsPayLnIndex++)
                                            {
                                                if (PaymentPatClaimLine.EOBPatientPaymentLineDetails[clmInsPayLnIndex] != null)
                                                {
                                                    _EOBPayDtlId = 0;
                                                    EOBPatPayDtl = PaymentPatClaimLine.EOBPatientPaymentLineDetails[clmInsPayLnIndex];

                                                    oParameters.Clear();
                                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nEOBDtlID", _EOBDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nEOBPaymentDetailID", EOBPatPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nBillingTransactionLineNo", EOBPatPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nPatientID", EOBPatPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nDOSFrom", EOBPatPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nDOSTo", EOBPatPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@sCPTCode", EOBPatPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                    oParameters.Add("@sCPTDescription", EOBPatPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                                    if (EOBPatPayDtl.IsNullAmount == false)
                                                    {
                                                        oParameters.Add("@nAmount", EOBPatPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                    }
                                                    else
                                                    {
                                                        oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                    }

                                                    oParameters.Add("@nPaymentType", EOBPatPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPaymentSubType", EOBPatPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPaySign", EOBPatPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPayMode", EOBPatPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nAccountID", EOBPatPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nAccountType", EOBPatPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nMSTAccountID", EOBPatPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nMSTAccountType", EOBPatPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPaymentTrayID", EOBPatPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@sPaymentTrayCode", EOBPatPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                    oParameters.Add("@sPaymentTrayDescription", EOBPatPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                    oParameters.Add("@nUserID", EOBPatPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@sUserName", EOBPatPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                    oParameters.Add("@dtCreatedDateTime", EOBPatPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                    oParameters.Add("@dtModifiedDateTime", EOBPatPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                    oParameters.Add("@nClinicID", EOBPatPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                                    //..RefEOBPaymentID & RefEOBPaymentDetailID identifies from where (which payment source or check) this payment
                                                    //..is coming from
                                                    if (EOBPatPayDtl.RefEOBPaymentID == 0) { EOBPatPayDtl.RefEOBPaymentID = _EOBPayId; }
                                                    if (EOBPatPayDtl.RefEOBPaymentDetailID == 0) { EOBPatPayDtl.RefEOBPaymentDetailID = _EOBPayCreditDtlId; }

                                                    oParameters.Add("@nRefEOBPaymentID", EOBPatPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBPatPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                                    //..ResEOBPaymentID,ResEOBPaymentDetailID has the reference id's for the reserve amount
                                                    oParameters.Add("@nResEOBPaymentID", EOBPatPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nResEOBPaymentDetailID", EOBPatPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);

                                                    oParameters.Add("@nContactInsID", EOBPatPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@nCreditLineID", EOBPatPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                                    oParameters.Add("@nCloseDate", EOBPatPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                                    // Newly added parameters by pankaj
                                                    oParameters.Add("@nTrackTrnID", EOBPatPayDtl.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@nTrackTrnDtlID", EOBPatPayDtl.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@sSubClaimNo", EOBPatPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),
                                                    oParameters.Add("@bIsVoid", EOBPatPayDtl.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                                    oParameters.Add("@nVoidCloseDate", EOBPatPayDtl.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                                    oParameters.Add("@nVoidTrayID", EOBPatPayDtl.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                                    oParameters.Add("@nVoidType", EOBPatPayDtl.VoidType, ParameterDirection.Input, SqlDbType.Int);

                                                    oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                                    oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nOldResEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nOldResEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                                    //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                                                    oParameters.Add("@nPAccountID", EOBPaymentPatient.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nAccountPatientID", EOBPaymentPatient.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nGuarantorID", EOBPaymentPatient.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
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

                                                    _result = _sqlCommand.ExecuteNonQuery();

                                                    if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                                    { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                                    else
                                                    { _retVal = 0; }

                                                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                                    { _EOBPayDtlId = Convert.ToInt64(_retVal); }
                                                    if (_sqlCommand != null)
                                                    {
                                                        _sqlCommand.Parameters.Clear();
                                                        _sqlCommand.Dispose();
                                                        _sqlCommand = null;
                                                    }
                                                     
                                                        EOBPatPayDtl = null;
                                                    

                                                }
                                            }
                                        }


                                        #endregion " EOB Financial Service Line Save "

                                        PaymentPatClaimLine = null;
                                    }
                                }
                                PaymentPatClaim = null;
                            }
                        }

                        #endregion " EOB Data Save "

                        EOBPatientPaymentMasterLines = EOBPaymentPatient.EOBPatientPaymentLineDetails;

                        _sqlTransaction.Commit();
                        _sqlConnection.Close();

                        #region " Save last selected Close date "

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseConnectionString);
                        oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(gloDateMaster.gloDate.DateAsDate(EOBPaymentPatient.CloseDate)).ToString("MM/dd/yyyy"), _clinicId, EOBPaymentPatient.UserID, gloSettings.SettingFlag.User);
                        oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", EOBPaymentPatient.PaymentTrayID.ToString(), _clinicId, EOBPaymentPatient.UserID, gloSettings.SettingFlag.User);
                        oSettings.Dispose();
                        oSettings = null;

                        //start Abhisekh  3 sept 2010

                        gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, AppSettings.ConnectionStringEMR);
                        ogloBilling.SaveUserWiseCloseDay(gloDateMaster.gloDate.DateAsDate(EOBPaymentPatient.CloseDate).ToString(), CloseDayType.Payment, _clinicId);
                        ogloBilling.Dispose();
                        ogloBilling = null;

                        //end 

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

                    if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                    if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); _sqlCommand = null; }
                    if (_sqlTransaction != null) { _sqlTransaction.Dispose(); _sqlTransaction = null; }

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                    }
                }

                return _EOBPayId;
            }

            public Int64 SavePatientCorrectionPayment(EOBPayment.Common.PaymentPatient EOBPaymentPatient, bool IsSaveForVoid, out EOBPayment.Common.EOBPatientPaymentDetails EOBPatientPaymentMasterLines)
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
                Int64 _EOBPayCreditDtlId = 0;
            //    Int64 _EOBPayResId = 0;

                Int64 _EOBVoidPayId = 0;

                bool _UseExtEOBID = false;
                EOBPayment.Common.PaymentPatientLine PaymentPatClaimLine = null;
                EOBPayment.Common.PaymentPatientClaim PaymentPatClaim = null;
                EOBPayment.Common.EOBPatientPaymentDetail EOBPatPayDtl = null;
                EOBPatientPaymentMasterLines = null;
                int _result = 0;

                try
                {
                    if (EOBPaymentPatient != null)
                    {
                        _sqlConnection.Open();
                        _sqlTransaction = _sqlConnection.BeginTransaction();

                        #region " Master Data Save "

                        //nEOBPaymentID,nEOBRefNO,sPayerName,nPayerID,nPayerType,nPaymentMode,sCheckNumber,nCheckAmount,nCheckDate
                        //nMSTAccountID,nMSTAccountType,nPaymentTrayID,sPaymentTrayName,nCloseDate,sCardType,sCardSecurityNo
                        //nCardID,nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                        _EOBPayId = 0;
                        oParameters.Clear();

                        oParameters.Add("@sPaymentNo", EOBPaymentPatient.PaymentNumber, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sPaymentNoPrefix", EOBPaymentPatient.PaymentNumberPefix, ParameterDirection.Input, SqlDbType.VarChar);

                        oParameters.Add("@nEOBPaymentID", EOBPaymentPatient.EOBPaymentID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nEOBRefNO", EOBPaymentPatient.EOBRefNO, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sPayerName", EOBPaymentPatient.PayerName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Unchecked
                        oParameters.Add("@nPayerID", EOBPaymentPatient.PayerID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nPayerType", EOBPaymentPatient.PayerType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nPaymentMode", EOBPaymentPatient.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@sCheckNumber", EOBPaymentPatient.CheckNumber, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Unchecked
                        oParameters.Add("@nCheckAmount", EOBPaymentPatient.CheckAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nCheckDate", EOBPaymentPatient.CheckDate, ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nMSTAccountID", EOBPaymentPatient.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nMSTAccountType", EOBPaymentPatient.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked

                        oParameters.Add("@nPaymentTrayID", EOBPaymentPatient.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sPaymentTrayCode", EOBPaymentPatient.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                        oParameters.Add("@sPaymentTrayDescription", EOBPaymentPatient.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255

                        oParameters.Add("@nCloseDate", EOBPaymentPatient.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sCardType", EOBPaymentPatient.CardType, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sCardSecurityNo", EOBPaymentPatient.CardSecurityNo, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@nCardID", EOBPaymentPatient.CardID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked

                        oParameters.Add("@sAuthorizationNo", EOBPaymentPatient.AuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                        oParameters.Add("@nCardExpDate", EOBPaymentPatient.CardExpiryDate, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),

                        oParameters.Add("@nUserID", EOBPaymentPatient.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sUserName", EOBPaymentPatient.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtCreatedDateTime", EOBPaymentPatient.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtModifiedDateTime", EOBPaymentPatient.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@bIsVoid", EOBPaymentPatient.IsVoid, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nVoidCloseDate", EOBPaymentPatient.VoidCloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nVoidTrayID", EOBPaymentPatient.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nVoidType", EOBPaymentPatient.VoidType, ParameterDirection.Input, SqlDbType.Int);
                        oParameters.Add("@nVoidRefPaymentID", EOBPaymentPatient.VoidRefPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", EOBPaymentPatient.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked

                        oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);

                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                        //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                        oParameters.Add("@nPAccountID", EOBPaymentPatient.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nAccountPatientID", EOBPaymentPatient.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nGuarantorID", EOBPaymentPatient.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                        //End
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
                        { _retVal = _sqlCommand.Parameters["@nEOBPaymentID"].Value; }
                        else
                        { _retVal = 0; }

                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                        { _EOBPayId = Convert.ToInt64(_retVal); }

                        if (IsSaveForVoid == true)
                        {
                            _EOBVoidPayId = Convert.ToInt64(_retVal);
                        }
                        if (_sqlCommand != null)
                        {
                            _sqlCommand.Parameters.Clear();
                            _sqlCommand.Dispose();
                            _sqlCommand = null;
                        }
                        #region "Master Payment Note"

                        if (EOBPaymentPatient.Notes != null && EOBPaymentPatient.Notes.Count > 0)
                        {
                            //Object _RcValue = null;

                            for (int rcInd = 0; rcInd < EOBPaymentPatient.Notes.Count; rcInd++)
                            {
                               // _RcValue = null;
                                oParameters.Clear();

                                oParameters.Add("@nID", EOBPaymentPatient.Notes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                oParameters.Add("@nClaimNo", EOBPaymentPatient.Notes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBID", EOBPaymentPatient.Notes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentDetailID", EOBPaymentPatient.Notes[rcInd].EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nBillingTransactionID", EOBPaymentPatient.Notes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                oParameters.Add("@nBillingTransactionDetailID", EOBPaymentPatient.Notes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@sNoteCode", EOBPaymentPatient.Notes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                oParameters.Add("@sNoteDescription", EOBPaymentPatient.Notes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                oParameters.Add("@dNoteAmount", EOBPaymentPatient.Notes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                oParameters.Add("@nPaymentNoteType", EOBPaymentPatient.Notes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                oParameters.Add("@nPaymentNoteSubType", EOBPaymentPatient.Notes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                oParameters.Add("@nIncludeNoteOnPrint", EOBPaymentPatient.Notes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
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

                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                else
                                { _retVal = 0; }
                                if (_sqlCommand != null)
                                {
                                    _sqlCommand.Parameters.Clear();
                                    _sqlCommand.Dispose();
                                    _sqlCommand = null;
                                }
                            }
                        }


                        #endregion

                        #endregion " Master Data Save "

                        #region "Payment Line Master (Credit) Entry, Total Check Amount Entry, but it is in same table "
                        if (EOBPaymentPatient.EOBPatientPaymentLineDetails != null && EOBPaymentPatient.EOBPatientPaymentLineDetails.Count > 0)
                        {
                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentPatient.EOBPatientPaymentLineDetails.Count; clmInsPayLnIndex++)
                            {
                                if (EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex] != null)
                                {
                                    _EOBPayCreditDtlId = 0;
                                    //This credit line detail id we have to setup into debit line where debit line dont have paydtlid
                                    //Suppose we collect new check as well as multiple reserve then
                                    //for reserve amount debit line will get id from form and who dont have id
                                    //we will setup this new id to them
                                    EOBPatPayDtl = EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex];

                                    oParameters.Clear();
                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBID", EOBPatPayDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBDtlID", EOBPatPayDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBPaymentDetailID", EOBPatPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionLineNo", EOBPatPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nPatientID", EOBPatPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nDOSFrom", EOBPatPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nDOSTo", EOBPatPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@sCPTCode", EOBPatPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sCPTDescription", EOBPatPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                    if (EOBPatPayDtl.IsNullAmount == false)
                                    {
                                        oParameters.Add("@nAmount", EOBPatPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    oParameters.Add("@nPaymentType", EOBPatPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentSubType", EOBPatPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaySign", EOBPatPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPayMode", EOBPatPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nAccountID", EOBPatPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nAccountType", EOBPatPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nMSTAccountID", EOBPatPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nMSTAccountType", EOBPatPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentTrayID", EOBPatPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sPaymentTrayCode", EOBPatPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sPaymentTrayDescription", EOBPatPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@nUserID", EOBPatPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sUserName", EOBPatPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@dtCreatedDateTime", EOBPatPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@dtModifiedDateTime", EOBPatPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@nClinicID", EOBPatPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    oParameters.Add("@nRefEOBPaymentID", EOBPatPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBPatPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //..ResEOBPaymentID,ResEOBPaymentDetailID has the reference id's for the reserve amount
                                    oParameters.Add("@nResEOBPaymentID", EOBPatPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nResEOBPaymentDetailID", EOBPatPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);


                                    oParameters.Add("@nContactInsID", EOBPatPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nCreditLineID", EOBPatPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    oParameters.Add("@nCloseDate", EOBPatPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    // Newly added parameters by pankaj
                                    oParameters.Add("@nTrackTrnID", EOBPatPayDtl.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nTrackTrnDtlID", EOBPatPayDtl.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@sSubClaimNo", EOBPatPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),
                                    oParameters.Add("@bIsVoid", EOBPatPayDtl.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                    oParameters.Add("@nVoidCloseDate", EOBPatPayDtl.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                    oParameters.Add("@nVoidTrayID", EOBPatPayDtl.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                    oParameters.Add("@nVoidType", EOBPatPayDtl.VoidType, ParameterDirection.Input, SqlDbType.Int);

                                    oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                    oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);

                                    oParameters.Add("@nOldResEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nOldResEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                                    oParameters.Add("@nPAccountID", EOBPaymentPatient.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nAccountPatientID", EOBPaymentPatient.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nGuarantorID", EOBPaymentPatient.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
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

                                    if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                    { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                    else
                                    { _retVal = 0; }


                                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                    { _EOBPayCreditDtlId = Convert.ToInt64(_retVal); }
                                    if (_sqlCommand != null)
                                    {
                                        _sqlCommand.Parameters.Clear();
                                        _sqlCommand.Dispose();
                                        _sqlCommand = null;
                                    }
                                    #region " Add Line Notes "

                                    if (EOBPatPayDtl.LineNotes != null && EOBPatPayDtl.LineNotes.Count > 0)
                                    {
//                                        Object _RcValue = null;

                                        for (int rcInd = 0; rcInd < EOBPatPayDtl.LineNotes.Count; rcInd++)
                                        {
  //                                          _RcValue = null;
                                            oParameters.Clear();

                                            oParameters.Add("@nID", EOBPatPayDtl.LineNotes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                            oParameters.Add("@nClaimNo", EOBPatPayDtl.LineNotes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBID", EOBPatPayDtl.LineNotes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentDetailID", _EOBPayCreditDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.LineNotes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                            oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.LineNotes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@sNoteCode", EOBPatPayDtl.LineNotes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                            oParameters.Add("@sNoteDescription", EOBPatPayDtl.LineNotes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                            oParameters.Add("@dNoteAmount", EOBPatPayDtl.LineNotes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                            oParameters.Add("@nPaymentNoteType", EOBPatPayDtl.LineNotes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nPaymentNoteSubType", EOBPatPayDtl.LineNotes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nIncludeNoteOnPrint", EOBPatPayDtl.LineNotes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
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

                                            if (_sqlCommand.Parameters["@nID"].Value != null)
                                            { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                            else
                                            { _retVal = 0; }

                                            if (_sqlCommand != null)
                                            {
                                                _sqlCommand.Parameters.Clear();
                                                _sqlCommand.Dispose();
                                                _sqlCommand = null;
                                            }
                                        }
                                    }

                                    #endregion " Add Line Notes "


                                    EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex].EOBPaymentID = _EOBPayId;
                                    EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex].EOBPaymentDetailID = Convert.ToInt64(_retVal.ToString());

                                    #region "Assign Credit Line Reference and Finance Line Reference to debit line wherever applicable"
                                    if (EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex].IsMainCreditLine == true)
                                    {
                                        //All Remaining Credit Lines
                                        for (int nAsgn = 0; nAsgn <= EOBPaymentPatient.EOBPatientPaymentLineDetails.Count - 1; nAsgn++)
                                        {
                                            if (EOBPaymentPatient.EOBPatientPaymentLineDetails[nAsgn].IsMainCreditLine == false)
                                            {
                                                EOBPaymentPatient.EOBPatientPaymentLineDetails[nAsgn].MainCreditLineID = _EOBPayCreditDtlId;
                                            }
                                        }
                                        //All Debit Lines
                                        for (int nAsgnClaim = 0; nAsgnClaim <= EOBPaymentPatient.PatientClaims.Count - 1; nAsgnClaim++)
                                        {
                                            for (int nAsgnClmLine = 0; nAsgnClmLine <= EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines.Count - 1; nAsgnClmLine++)
                                            {
                                                for (int nAsgn = 0; nAsgn <= EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails.Count - 1; nAsgn++)
                                                {
                                                    if (EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].IsMainCreditLine == false)
                                                    {
                                                        EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].MainCreditLineID = _EOBPayCreditDtlId;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //Assign Reference Number to debit lines
                                    for (int nAsgnClaim = 0; nAsgnClaim <= EOBPaymentPatient.PatientClaims.Count - 1; nAsgnClaim++)
                                    {
                                        for (int nAsgnClmLine = 0; nAsgnClmLine <= EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines.Count - 1; nAsgnClmLine++)
                                        {
                                            for (int nAsgn = 0; nAsgn <= EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails.Count - 1; nAsgn++)
                                            {
                                                if (EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].RefFinanceLieNo == EOBPatPayDtl.FinanceLieNo)
                                                {
                                                    if (EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].UseRefFinanceLieNo == true)
                                                    {
                                                        EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].RefEOBPaymentID = _EOBPayId;
                                                        EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].RefEOBPaymentDetailID = _EOBPayCreditDtlId;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //Assign Main Credit Line ID to Direct Transaction Reserve Lines
                                    for (int nResClmLine = 0; nResClmLine <= EOBPaymentPatient.EOBPatientPaymentReserveLineDetail.Count - 1; nResClmLine++)
                                    {
                                        EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].MainCreditLineID = _EOBPayCreditDtlId;
                                        if (EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].UseRefFinanceLieNo == true)
                                        {
                                            //here checking id is 0, bcz its patient payment directlly send to use reserve, other wise as per insurace payment no need
                                            if (EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].RefEOBPaymentID == 0 && EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].RefEOBPaymentDetailID == 0)
                                            {
                                                EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].RefEOBPaymentID = _EOBPayId;
                                                EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].RefEOBPaymentDetailID = _EOBPayCreditDtlId;
                                            }
                                        }
                                    }
                                    #endregion


                                   
                                        EOBPatPayDtl = null;
                                    
                                }
                            }
                        }
                        #endregion

                        #region "Payment Line Master Reserve (Debit) Entry"
                        if (EOBPaymentPatient.EOBPatientPaymentReserveLineDetail != null && EOBPaymentPatient.EOBPatientPaymentReserveLineDetail.Count > 0)
                        {
                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentPatient.EOBPatientPaymentReserveLineDetail.Count; clmInsPayLnIndex++)
                            {
                                if (EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[clmInsPayLnIndex] != null)
                                {
                                    _EOBPayDtlId = 0;
                                    EOBPatPayDtl = EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[clmInsPayLnIndex];

                                    oParameters.Clear();
                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBID", EOBPatPayDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBDtlID", EOBPatPayDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBPaymentDetailID", EOBPatPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionLineNo", EOBPatPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nPatientID", EOBPatPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nDOSFrom", EOBPatPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nDOSTo", EOBPatPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@sCPTCode", EOBPatPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sCPTDescription", EOBPatPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                    if (EOBPatPayDtl.IsNullAmount == false)
                                    {
                                        oParameters.Add("@nAmount", EOBPatPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    oParameters.Add("@nPaymentType", EOBPatPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentSubType", EOBPatPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaySign", EOBPatPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPayMode", EOBPatPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nAccountID", EOBPatPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nAccountType", EOBPatPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nMSTAccountID", EOBPatPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nMSTAccountType", EOBPatPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentTrayID", EOBPatPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sPaymentTrayCode", EOBPatPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sPaymentTrayDescription", EOBPatPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@nUserID", EOBPatPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sUserName", EOBPatPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@dtCreatedDateTime", EOBPatPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@dtModifiedDateTime", EOBPatPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@nClinicID", EOBPatPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    oParameters.Add("@nRefEOBPaymentID", EOBPatPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBPatPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //..ResEOBPaymentID,ResEOBPaymentDetailID has the reference id's for the reserve amount
                                    oParameters.Add("@nResEOBPaymentID", EOBPatPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nResEOBPaymentDetailID", EOBPatPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);


                                    oParameters.Add("@nContactInsID", EOBPatPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nCreditLineID", EOBPatPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    oParameters.Add("@nCloseDate", EOBPatPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    // Newly added parameters by pankaj
                                    oParameters.Add("@nTrackTrnID", EOBPatPayDtl.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nTrackTrnDtlID", EOBPatPayDtl.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@sSubClaimNo", EOBPatPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),
                                    oParameters.Add("@bIsVoid", EOBPatPayDtl.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                    oParameters.Add("@nVoidCloseDate", EOBPatPayDtl.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                    oParameters.Add("@nVoidTrayID", EOBPatPayDtl.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                    oParameters.Add("@nVoidType", EOBPatPayDtl.VoidType, ParameterDirection.Input, SqlDbType.Int);

                                    oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                    oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);

                                    oParameters.Add("@nOldResEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nOldResEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                                    //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                                    oParameters.Add("@nPAccountID", EOBPatPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nAccountPatientID", EOBPatPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nGuarantorID", EOBPatPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
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

                                    if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                    { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                    else
                                    { _retVal = 0; }

                                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                    { _EOBPayDtlId = Convert.ToInt64(_retVal); }
                                    if (_sqlCommand != null)
                                    {
                                        _sqlCommand.Parameters.Clear();
                                        _sqlCommand.Dispose();
                                        _sqlCommand = null;
                                    }
                                    #region " Add Line Notes "

                                    if (EOBPatPayDtl.LineNotes != null && EOBPatPayDtl.LineNotes.Count > 0)
                                    {
                                       // Object _RcValue = null;

                                        for (int rcInd = 0; rcInd < EOBPatPayDtl.LineNotes.Count; rcInd++)
                                        {
                                         //   _RcValue = null;
                                            oParameters.Clear();

                                            oParameters.Add("@nID", EOBPatPayDtl.LineNotes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                            oParameters.Add("@nClaimNo", EOBPatPayDtl.LineNotes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBID", EOBPatPayDtl.LineNotes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentDetailID", _EOBPayDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.LineNotes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                            oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.LineNotes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@sNoteCode", EOBPatPayDtl.LineNotes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                            oParameters.Add("@sNoteDescription", EOBPatPayDtl.LineNotes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                            oParameters.Add("@dNoteAmount", EOBPatPayDtl.LineNotes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                            oParameters.Add("@nPaymentNoteType", EOBPatPayDtl.LineNotes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nPaymentNoteSubType", EOBPatPayDtl.LineNotes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nIncludeNoteOnPrint", EOBPatPayDtl.LineNotes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
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

                                            if (_sqlCommand.Parameters["@nID"].Value != null)
                                            { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                            else
                                            { _retVal = 0; }
                                            if (_sqlCommand != null)
                                            {
                                                _sqlCommand.Parameters.Clear();
                                                _sqlCommand.Dispose();
                                                _sqlCommand = null;
                                            }
                                        }
                                    }

                                    #endregion " Add Line Notes "


                                    
                                        EOBPatPayDtl = null;
                                     
                                }
                            }
                        }
                        #endregion

                        #region " EOB Data Save "

                        if (_EOBPayId > 0 && EOBPaymentPatient.PatientClaims != null && EOBPaymentPatient.PatientClaims.Count > 0)
                        {
                            for (int clmIndex = 0; clmIndex < EOBPaymentPatient.PatientClaims.Count; clmIndex++)
                            {
                                PaymentPatClaim = EOBPaymentPatient.PatientClaims[clmIndex];
                                for (int clmLnIndex = 0; clmLnIndex < PaymentPatClaim.CliamLines.Count; clmLnIndex++)
                                {
                                    if (PaymentPatClaim.CliamLines[clmLnIndex] != null)
                                    {
                                        _EOBDtlId = 0;
                                        PaymentPatClaimLine = PaymentPatClaim.CliamLines[clmLnIndex];

                                        //nEOBPaymentID,nEOBID,nEOBPaymentDetailID,nBillingTransactionID,nBillingTransactionDetailID
                                        //nBillingTransactionLineNo,nPatientID,nDOSFrom,nDOSTo,sCPTCode,sCPTDescription,nAmount,
                                        //nPaymentType,nPaymentSubType,nPaySign,nRefEOBPaymentID,nRefEOBPaymentDetailID,nAccountID
                                        //nAccountType,nMSTAccountID,nMSTAccountType,nPaymentTrayID,nPaymentTrayCode,nPaymentTrayDescription
                                        //nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                                        oParameters.Clear();
                                        #region "EOB Service Line"
                                        if (_UseExtEOBID == true) { PaymentPatClaimLine.mEOBID = _EOBId; }
                                        oParameters.Add("@nEOBID", PaymentPatClaimLine.mEOBID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                        oParameters.Add("@nEOBDtlID", PaymentPatClaimLine.mEOBDtlID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                        oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0)
                                        //oParameters.Add("@nClaimNo", PaymentPatClaimLine.ClaimNumber, ParameterDirection.Input, SqlDbType.Int);//	int
                                        oParameters.Add("@nClaimNo", PaymentPatClaimLine.ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                        oParameters.Add("@nDOSFrom", PaymentPatClaimLine.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                        oParameters.Add("@nDOSTo", PaymentPatClaimLine.DOSTo, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                        oParameters.Add("@sCPTCode", PaymentPatClaimLine.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                        oParameters.Add("@sCPTDescription", PaymentPatClaimLine.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                        if (PaymentPatClaimLine.IsNullCharges == false)
                                        {
                                            oParameters.Add("@dCharges", PaymentPatClaimLine.Charges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullUnit == false)
                                        {
                                            oParameters.Add("@dUnit", PaymentPatClaimLine.Unit, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dUnit", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                        }

                                        if (PaymentPatClaimLine.IsNullTotalCharges == false)
                                        {
                                            oParameters.Add("@dTotalCharges", PaymentPatClaimLine.TotalCharges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dTotalCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullAllowed == false)
                                        {
                                            oParameters.Add("@dAllowed", PaymentPatClaimLine.Allowed, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dAllowed", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullWriteOff == false)
                                        {
                                            oParameters.Add("@dWriteOff", PaymentPatClaimLine.WriteOff, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dWriteOff", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullNonCovered == false)
                                        {
                                            oParameters.Add("@dNotCovered", PaymentPatClaimLine.NonCovered, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dNotCovered", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullInsurance == false)
                                        {
                                            oParameters.Add("@dPayment", PaymentPatClaimLine.InsuranceAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dPayment", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullCopay == false)
                                        {
                                            oParameters.Add("@dCopay", PaymentPatClaimLine.Copay, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dCopay", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullDeductible == false)
                                        {
                                            oParameters.Add("@dDeductible", PaymentPatClaimLine.Deductible, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dDeductible", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullCoInsurance == false)
                                        {
                                            oParameters.Add("@dCoInsurance", PaymentPatClaimLine.CoInsurance, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                        }
                                        else
                                        {
                                            oParameters.Add("@dCoInsurance", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                        }

                                        if (PaymentPatClaimLine.IsNullWithhold == false)
                                        {
                                            oParameters.Add("@dWithhold", PaymentPatClaimLine.Withhold, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dWithhold", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        oParameters.Add("@nPaymentTrayID", PaymentPatClaimLine.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                        oParameters.Add("@sPaymentTrayCode", PaymentPatClaimLine.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                                        oParameters.Add("@sPaymentTrayDescription", PaymentPatClaimLine.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked

                                        oParameters.Add("@nUserID", PaymentPatClaimLine.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                        oParameters.Add("@sUserName", PaymentPatClaimLine.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked
                                        oParameters.Add("@dtCreatedDateTime", PaymentPatClaimLine.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked
                                        oParameters.Add("@dtModifiedDateTime", PaymentPatClaimLine.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked

                                        oParameters.Add("@nPatientID", PaymentPatClaimLine.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                        oParameters.Add("@nInsuraceID", PaymentPatClaimLine.PatInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                        oParameters.Add("@nContactID", PaymentPatClaimLine.InsContactID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked

                                        oParameters.Add("@nClinicID", PaymentPatClaimLine.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                        oParameters.Add("@nEOBType", PaymentPatClaimLine.EOBType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);// int,
                                        oParameters.Add("@nBillingTransactionID", PaymentPatClaimLine.BLTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                        oParameters.Add("@nBillingTransactionDetailID", PaymentPatClaimLine.BLTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                        oParameters.Add("@nBillingTransactionLineNo", PaymentPatClaimLine.BLTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)

                                        oParameters.Add("@bUseExtEOBID", _UseExtEOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                        oParameters.Add("@nTrackTrnID", PaymentPatClaimLine.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                        oParameters.Add("@nTrackTrnDtlID", PaymentPatClaimLine.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                        oParameters.Add("@sSubClaimNo", PaymentPatClaimLine.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),

                                        oParameters.Add("@nCloseDate", PaymentPatClaimLine.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                        oParameters.Add("@bIsVoid", PaymentPatClaimLine.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                        oParameters.Add("@nVoidCloseDate", PaymentPatClaimLine.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                        oParameters.Add("@nVoidTrayID", PaymentPatClaimLine.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                        oParameters.Add("@nVoidType", PaymentPatClaimLine.VoidType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                        //oParameters.Add("@nContactInsID", PaymentPatClaimLine.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                        //oParameters.Add("@nCreditLineID", PaymentPatClaimLine.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                        oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                        oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                        oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);

                                        //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                                        oParameters.Add("@nPAccountID", EOBPaymentPatient.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                        oParameters.Add("@nAccountPatientID", EOBPaymentPatient.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                        oParameters.Add("@nGuarantorID", EOBPaymentPatient.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                        //End


                                        if (_UseExtEOBID == false) { _UseExtEOBID = true; }
                                        _retVal = null;
                                        _valRet = null;

                                        //oDB.Connect(false);
                                        //oDB.Execute("BL_INSERT_EOBPayment_EOB_PatPayment", oParameters, out _retVal, out _valRet);
                                        //oDB.Disconnect();

                                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                                        _sqlCommand.Connection = _sqlConnection;
                                        _sqlCommand.Transaction = _sqlTransaction;
                                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                                        _sqlCommand.CommandText = "BL_INSERT_EOBPayment_EOB_PatPayment";

                                        _result = 0;
                                        _result = _sqlCommand.ExecuteNonQuery();

                                        if (_sqlCommand.Parameters["@nEOBID"].Value != null)
                                        { _retVal = _sqlCommand.Parameters["@nEOBID"].Value; }
                                        else
                                        { _retVal = 0; }

                                        if (_sqlCommand.Parameters["@nEOBDtlID"].Value != null)
                                        { _valRet = _sqlCommand.Parameters["@nEOBDtlID"].Value; }
                                        else
                                        { _valRet = 0; }
                                        if (_sqlCommand != null)
                                        {
                                            _sqlCommand.Parameters.Clear();
                                            _sqlCommand.Dispose();
                                            _sqlCommand = null;
                                        }
                                        #endregion

                                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                        { _EOBId = Convert.ToInt64(_retVal); }

                                        if (_valRet != null && Convert.ToString(_valRet).Trim() != "")
                                        { _EOBDtlId = Convert.ToInt64(_valRet); }

                                        #region " Add Line Adjustment Codes "

                                        if (PaymentPatClaimLine.LineAdjestmentCodes != null && PaymentPatClaimLine.LineAdjestmentCodes.Count > 0)
                                        {
                                         //   Object _RcValue = null;

                                            for (int rcInd = 0; rcInd < PaymentPatClaimLine.LineAdjestmentCodes.Count; rcInd++)
                                            {
                                           //     _RcValue = null;
                                                oParameters.Clear();

                                                oParameters.Add("@nID", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                                oParameters.Add("@nClaimNo", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentDetailID", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nBillingTransactionID", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                oParameters.Add("@nBillingTransactionDetailID", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sReasonCode", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                oParameters.Add("@sReasonDescription", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                if (PaymentPatClaimLine.LineAdjestmentCodes[rcInd].IsNullAmount == false)
                                                {
                                                    oParameters.Add("@dReasonAmount", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dReasonAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                }

                                                oParameters.Add("@nType", EOBCommentType.Adjustment.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
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

                                                _result = 0;
                                                _result = _sqlCommand.ExecuteNonQuery();

                                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                                { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                                else
                                                { _retVal = 0; }
                                                if (_sqlCommand != null)
                                                {
                                                    _sqlCommand.Parameters.Clear();
                                                    _sqlCommand.Dispose();
                                                    _sqlCommand = null;
                                                }
                                            }
                                        }

                                        #endregion " Add Line Adjustment Codes "

                                        #region " EOB Financial Service Line Save "

                                        if (_EOBPayId > 0 && PaymentPatClaimLine.EOBPatientPaymentLineDetails != null && PaymentPatClaimLine.EOBPatientPaymentLineDetails.Count > 0)
                                        {
                                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < PaymentPatClaimLine.EOBPatientPaymentLineDetails.Count; clmInsPayLnIndex++)
                                            {
                                                if (PaymentPatClaimLine.EOBPatientPaymentLineDetails[clmInsPayLnIndex] != null)
                                                {
                                                    _EOBPayDtlId = 0;
                                                    EOBPatPayDtl = PaymentPatClaimLine.EOBPatientPaymentLineDetails[clmInsPayLnIndex];

                                                    oParameters.Clear();
                                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nEOBDtlID", _EOBDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nEOBPaymentDetailID", EOBPatPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nBillingTransactionLineNo", EOBPatPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nPatientID", EOBPatPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nDOSFrom", EOBPatPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nDOSTo", EOBPatPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@sCPTCode", EOBPatPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                    oParameters.Add("@sCPTDescription", EOBPatPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                                    if (EOBPatPayDtl.IsNullAmount == false)
                                                    {
                                                        oParameters.Add("@nAmount", EOBPatPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                    }
                                                    else
                                                    {
                                                        oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                    }

                                                    oParameters.Add("@nPaymentType", EOBPatPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPaymentSubType", EOBPatPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPaySign", EOBPatPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPayMode", EOBPatPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nAccountID", EOBPatPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nAccountType", EOBPatPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nMSTAccountID", EOBPatPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nMSTAccountType", EOBPatPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPaymentTrayID", EOBPatPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@sPaymentTrayCode", EOBPatPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                    oParameters.Add("@sPaymentTrayDescription", EOBPatPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                    oParameters.Add("@nUserID", EOBPatPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@sUserName", EOBPatPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                    oParameters.Add("@dtCreatedDateTime", EOBPatPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                    oParameters.Add("@dtModifiedDateTime", EOBPatPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                    oParameters.Add("@nClinicID", EOBPatPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                                    //..RefEOBPaymentID & RefEOBPaymentDetailID identifies from where (which payment source or check) this payment
                                                    //..is coming from
                                                    if (EOBPatPayDtl.RefEOBPaymentID == 0) { EOBPatPayDtl.RefEOBPaymentID = _EOBPayId; }
                                                    if (EOBPatPayDtl.RefEOBPaymentDetailID == 0) { EOBPatPayDtl.RefEOBPaymentDetailID = _EOBPayCreditDtlId; }

                                                    oParameters.Add("@nRefEOBPaymentID", EOBPatPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBPatPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                                    //..Code chages done by Sagar Ghodke on 20100626
                                                    //Below commented code is previous code
                                                    ////..ResEOBPaymentID,ResEOBPaymentDetailID has the reference id's for the reserve amount

                                                    //oParameters.Add("@nResEOBPaymentID", EOBPatPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    //oParameters.Add("@nResEOBPaymentDetailID", EOBPatPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);

                                                    oParameters.Add("@nResEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nResEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                                                    oParameters.Add("@nOldResEOBPaymentID", EOBPatPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nOldResEOBPaymentDetailID", EOBPatPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);

                                                    //..End code chages for 20100626 by Sagar Ghodke

                                                    oParameters.Add("@nContactInsID", EOBPatPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@nCreditLineID", EOBPatPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                                    oParameters.Add("@nCloseDate", EOBPatPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                                    // Newly added parameters by pankaj
                                                    oParameters.Add("@nTrackTrnID", EOBPatPayDtl.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@nTrackTrnDtlID", EOBPatPayDtl.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@sSubClaimNo", EOBPatPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),
                                                    oParameters.Add("@bIsVoid", EOBPatPayDtl.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                                    oParameters.Add("@nVoidCloseDate", EOBPatPayDtl.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                                    oParameters.Add("@nVoidTrayID", EOBPatPayDtl.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                                    oParameters.Add("@nVoidType", EOBPatPayDtl.VoidType, ParameterDirection.Input, SqlDbType.Int);

                                                    oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                                    oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                                    
                                                    //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                                                    oParameters.Add("@nPAccountID", EOBPatPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nAccountPatientID", EOBPatPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nGuarantorID", EOBPatPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
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

                                                    if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                                    { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                                    else
                                                    { _retVal = 0; }

                                                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                                    { _EOBPayDtlId = Convert.ToInt64(_retVal); }
                                                    if (_sqlCommand != null)
                                                    {
                                                        _sqlCommand.Parameters.Clear();
                                                        _sqlCommand.Dispose();
                                                        _sqlCommand = null;
                                                    }
                                                    
                                                        EOBPatPayDtl = null;
                                                     

                                                }
                                            }
                                        }


                                        #endregion " EOB Financial Service Line Save "

                                        PaymentPatClaimLine = null;
                                    }
                                }
                                PaymentPatClaim = null;
                            }
                        }

                        #endregion " EOB Data Save "

                        EOBPatientPaymentMasterLines = EOBPaymentPatient.EOBPatientPaymentLineDetails;

                        _sqlTransaction.Commit();
                        _sqlConnection.Close();

                        #region " Save last selected Close date "

                        if (IsSaveForVoid == false)
                        {
                            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseConnectionString);
                            oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(gloDateMaster.gloDate.DateAsDate(EOBPaymentPatient.CloseDate)).ToString("MM/dd/yyyy"), _clinicId, EOBPaymentPatient.UserID, gloSettings.SettingFlag.User);
                            oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", EOBPaymentPatient.PaymentTrayID.ToString(), _clinicId, EOBPaymentPatient.UserID, gloSettings.SettingFlag.User);
                            oSettings.Dispose();
                            oSettings = null;
                        }

                        //start Abhisekh  3 sept 2010

                        gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, AppSettings.ConnectionStringEMR);
                        ogloBilling.SaveUserWiseCloseDay(gloDateMaster.gloDate.DateAsDate(EOBPaymentPatient.CloseDate).ToString(), CloseDayType.Payment, _clinicId);
                        ogloBilling.Dispose();
                        ogloBilling = null;
                        //end 

                        #endregion " Save last selected Close date "
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                { _sqlTransaction.Rollback(); ex.ERROR_Log(ex.ToString()); }
                catch (Exception ex)
                { _sqlTransaction.Rollback(); gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
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
                    }
                }

                return _EOBPayId;
            }

            public Int64 SavePatientRefund(EOBPayment.Common.PaymentPatient EOBPaymentPatient, EOBPayment.Common.PatientPaymentReturn PatientPaymentReturn, bool IsSaveForVoid)
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
                Int64 _EOBPayCreditDtlId = 0;
             //   Int64 _EOBPayResId = 0;

                Int64 _EOBVoidPayId = 0;

                bool _UseExtEOBID = false;
                EOBPayment.Common.PaymentPatientLine PaymentPatClaimLine = null;
                EOBPayment.Common.PaymentPatientClaim PaymentPatClaim = null;
                EOBPayment.Common.EOBPatientPaymentDetail EOBPatPayDtl = null;
                EOBPayment.Common.EOBPatientPaymentDetails EOBPatientPaymentMasterLines = null;

                int _result = 0;

                try
                {
                    if (EOBPaymentPatient != null)
                    {
                        _sqlConnection.Open();
                        _sqlTransaction = _sqlConnection.BeginTransaction();

                        #region " Master Data Save "

                        //nEOBPaymentID,nEOBRefNO,sPayerName,nPayerID,nPayerType,nPaymentMode,sCheckNumber,nCheckAmount,nCheckDate
                        //nMSTAccountID,nMSTAccountType,nPaymentTrayID,sPaymentTrayName,nCloseDate,sCardType,sCardSecurityNo
                        //nCardID,nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                        _EOBPayId = 0;
                        oParameters.Clear();

                        oParameters.Add("@sPaymentNo", EOBPaymentPatient.PaymentNumber, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sPaymentNoPrefix", EOBPaymentPatient.PaymentNumberPefix, ParameterDirection.Input, SqlDbType.VarChar);

                        oParameters.Add("@nEOBPaymentID", EOBPaymentPatient.EOBPaymentID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nEOBRefNO", EOBPaymentPatient.EOBRefNO, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sPayerName", EOBPaymentPatient.PayerName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Unchecked
                        oParameters.Add("@nPayerID", EOBPaymentPatient.PayerID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nPayerType", EOBPaymentPatient.PayerType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nPaymentMode", EOBPaymentPatient.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@sCheckNumber", EOBPaymentPatient.CheckNumber, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Unchecked
                        oParameters.Add("@nCheckAmount", EOBPaymentPatient.CheckAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nCheckDate", EOBPaymentPatient.CheckDate, ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nMSTAccountID", EOBPaymentPatient.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nMSTAccountType", EOBPaymentPatient.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked

                        oParameters.Add("@nPaymentTrayID", EOBPaymentPatient.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sPaymentTrayCode", EOBPaymentPatient.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                        oParameters.Add("@sPaymentTrayDescription", EOBPaymentPatient.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255

                        oParameters.Add("@nCloseDate", EOBPaymentPatient.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sCardType", EOBPaymentPatient.CardType, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sCardSecurityNo", EOBPaymentPatient.CardSecurityNo, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@nCardID", EOBPaymentPatient.CardID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked

                        oParameters.Add("@sAuthorizationNo", EOBPaymentPatient.AuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                        oParameters.Add("@nCardExpDate", EOBPaymentPatient.CardExpiryDate, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),

                        oParameters.Add("@nUserID", EOBPaymentPatient.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sUserName", EOBPaymentPatient.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtCreatedDateTime", EOBPaymentPatient.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtModifiedDateTime", EOBPaymentPatient.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@bIsVoid", EOBPaymentPatient.IsVoid, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nVoidCloseDate", EOBPaymentPatient.VoidCloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nVoidTrayID", EOBPaymentPatient.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nVoidType", EOBPaymentPatient.VoidType, ParameterDirection.Input, SqlDbType.Int);
                        oParameters.Add("@nVoidRefPaymentID", EOBPaymentPatient.VoidRefPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", EOBPaymentPatient.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked

                        oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);

                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                        //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                        oParameters.Add("@nPAccountID", EOBPaymentPatient.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nAccountPatientID", EOBPaymentPatient.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nGuarantorID", EOBPaymentPatient.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                        //End

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
                            EOBPaymentPatient.EOBPaymentID = Convert.ToInt64(_sqlCommand.Parameters["@nEOBPaymentID"].Value);
                        }
                        else
                        {
                            EOBPaymentPatient.EOBPaymentID = 0;
                        }

                        if (_sqlCommand != null)
                        {
                            _sqlCommand.Parameters.Clear();
                            _sqlCommand.Dispose();
                            _sqlCommand = null;
                        }

                        oParameters.Clear();

                        oParameters.Add("@nEOBPaymentID", EOBPaymentPatient.EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nPayerID", EOBPaymentPatient.PayerID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nPaymentMode", EOBPaymentPatient.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@sCheckNumber", EOBPaymentPatient.CheckNumber, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Unchecked
                        oParameters.Add("@nCheckAmount", EOBPaymentPatient.CheckAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nCheckDate", EOBPaymentPatient.CheckDate, ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked


                        oParameters.Add("@nPaymentTrayID", EOBPaymentPatient.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sPaymentTrayCode", EOBPaymentPatient.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                        oParameters.Add("@sPaymentTrayDescription", EOBPaymentPatient.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255

                        oParameters.Add("@nCloseDate", EOBPaymentPatient.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sCardType", EOBPaymentPatient.CardType, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sCardSecurityNo", EOBPaymentPatient.CardSecurityNo, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@nCardID", EOBPaymentPatient.CardID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked

                        oParameters.Add("@sAuthorizationNo", EOBPaymentPatient.AuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                        oParameters.Add("@nCardExpDate", EOBPaymentPatient.CardExpiryDate, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),

                        oParameters.Add("@nUserID", EOBPaymentPatient.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sUserName", EOBPaymentPatient.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtCreatedDateTime", EOBPaymentPatient.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtModifiedDateTime", EOBPaymentPatient.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@bIsVoid", EOBPaymentPatient.IsVoid, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nVoidCloseDate", EOBPaymentPatient.VoidCloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nVoidTrayID", EOBPaymentPatient.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", EOBPaymentPatient.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked                           

                        oParameters.Add("@nRefundID", PatientPaymentReturn.RefundID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@sRefundTo", PatientPaymentReturn.RefundTo, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sRefundNote", PatientPaymentReturn.RefundNotes, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@dtRefundDate", PatientPaymentReturn.Refunddate, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nRefundAmount", PatientPaymentReturn.RefundAmount, ParameterDirection.Input, SqlDbType.Decimal);
                        
                        //Added by SaiKrishna on 03/Feb /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                        oParameters.Add("@nPAccountID", EOBPaymentPatient.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nAccountPatientID", EOBPaymentPatient.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nGuarantorID", EOBPaymentPatient.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                        //End


                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        // _sqlCommand.CommandText = "BL_INUP_EOBPayment_MST_PatRefund";
                        _sqlCommand.CommandText = "PA_BL_INUP_EOBPayment_MST_PatRefund";
                        _result = _sqlCommand.ExecuteNonQuery();



                        if (_sqlCommand.Parameters["@nEOBPaymentID"].Value != null && Convert.ToString(_sqlCommand.Parameters["@nEOBPaymentID"].Value).Trim() != "")
                        { _retVal = Convert.ToInt64(_sqlCommand.Parameters["@nEOBPaymentID"].Value); }

                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                        { _EOBPayId = Convert.ToInt64(_retVal); }

                        if (IsSaveForVoid == true)
                        {
                            _EOBVoidPayId = Convert.ToInt64(_retVal);
                        }
                        if (_sqlCommand != null)
                        {
                            _sqlCommand.Parameters.Clear();
                            _sqlCommand.Dispose();
                            _sqlCommand = null;
                        }
                        #region "Master Payment Note"

                        if (EOBPaymentPatient.Notes != null && EOBPaymentPatient.Notes.Count > 0)
                        {
                          //  Object _RcValue = null;

                            for (int rcInd = 0; rcInd < EOBPaymentPatient.Notes.Count; rcInd++)
                            {
                               // _RcValue = null;
                                oParameters.Clear();

                                oParameters.Add("@nID", EOBPaymentPatient.Notes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                oParameters.Add("@nClaimNo", EOBPaymentPatient.Notes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBID", EOBPaymentPatient.Notes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentDetailID", EOBPaymentPatient.Notes[rcInd].EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nBillingTransactionID", EOBPaymentPatient.Notes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                oParameters.Add("@nBillingTransactionDetailID", EOBPaymentPatient.Notes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@sNoteCode", EOBPaymentPatient.Notes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                oParameters.Add("@sNoteDescription", EOBPaymentPatient.Notes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                oParameters.Add("@dNoteAmount", EOBPaymentPatient.Notes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                oParameters.Add("@nPaymentNoteType", EOBPaymentPatient.Notes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                oParameters.Add("@nPaymentNoteSubType", EOBPaymentPatient.Notes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                oParameters.Add("@nIncludeNoteOnPrint", EOBPaymentPatient.Notes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
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
                                if (_sqlCommand != null)
                                {
                                    _sqlCommand.Parameters.Clear();
                                    _sqlCommand.Dispose();
                                    _sqlCommand = null;
                                }
                            }
                        }


                        #endregion

                        #endregion " Master Data Save "

                        #region "Payment Line Master (Credit) Entry, Total Check Amount Entry, but it is in same table "
                        if (EOBPaymentPatient.EOBPatientPaymentLineDetails != null && EOBPaymentPatient.EOBPatientPaymentLineDetails.Count > 0)
                        {
                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentPatient.EOBPatientPaymentLineDetails.Count; clmInsPayLnIndex++)
                            {
                                if (EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex] != null)
                                {
                                    _EOBPayCreditDtlId = 0;
                                    //This credit line detail id we have to setup into debit line where debit line dont have paydtlid
                                    //Suppose we collect new check as well as multiple reserve then
                                    //for reserve amount debit line will get id from form and who dont have id
                                    //we will setup this new id to them
                                    EOBPatPayDtl = EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex];

                                    oParameters.Clear();
                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBID", EOBPatPayDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBDtlID", EOBPatPayDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBPaymentDetailID", EOBPatPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionLineNo", EOBPatPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nPatientID", EOBPatPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nDOSFrom", EOBPatPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nDOSTo", EOBPatPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@sCPTCode", EOBPatPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sCPTDescription", EOBPatPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                    if (EOBPatPayDtl.IsNullAmount == false)
                                    {
                                        oParameters.Add("@nAmount", EOBPatPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    oParameters.Add("@nPaymentType", EOBPatPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentSubType", EOBPatPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaySign", EOBPatPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPayMode", EOBPatPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nAccountID", EOBPatPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nAccountType", EOBPatPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nMSTAccountID", EOBPatPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nMSTAccountType", EOBPatPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentTrayID", EOBPatPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sPaymentTrayCode", EOBPatPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sPaymentTrayDescription", EOBPatPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@nUserID", EOBPatPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sUserName", EOBPatPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@dtCreatedDateTime", EOBPatPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@dtModifiedDateTime", EOBPatPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@nClinicID", EOBPatPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    oParameters.Add("@nRefEOBPaymentID", EOBPatPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBPatPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //..ResEOBPaymentID,ResEOBPaymentDetailID has the reference id's for the reserve amount
                                    oParameters.Add("@nResEOBPaymentID", EOBPatPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nResEOBPaymentDetailID", EOBPatPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);

                                    oParameters.Add("@nOldResEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nOldResEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                                    oParameters.Add("@nContactInsID", EOBPatPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nCreditLineID", EOBPatPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    oParameters.Add("@nCloseDate", EOBPatPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    // Newly added parameters by pankaj
                                    oParameters.Add("@nTrackTrnID", EOBPatPayDtl.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nTrackTrnDtlID", EOBPatPayDtl.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@sSubClaimNo", EOBPatPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),
                                    oParameters.Add("@bIsVoid", EOBPatPayDtl.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                    oParameters.Add("@nVoidCloseDate", EOBPatPayDtl.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                    oParameters.Add("@nVoidTrayID", EOBPatPayDtl.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                    oParameters.Add("@nVoidType", EOBPatPayDtl.VoidType, ParameterDirection.Input, SqlDbType.Int);

                                    oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                    oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                    //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                                    oParameters.Add("@nPAccountID", EOBPatPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nAccountPatientID", EOBPatPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nGuarantorID", EOBPatPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
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
                                    if (_sqlCommand != null)
                                    {
                                        _sqlCommand.Parameters.Clear();
                                        _sqlCommand.Dispose();
                                        _sqlCommand = null;
                                    }
                                    #region " Add Line Notes "

                                    if (EOBPatPayDtl.LineNotes != null && EOBPatPayDtl.LineNotes.Count > 0)
                                    {
                                     //   Object _RcValue = null;

                                        for (int rcInd = 0; rcInd < EOBPatPayDtl.LineNotes.Count; rcInd++)
                                        {
                                       //     _RcValue = null;
                                            oParameters.Clear();

                                            oParameters.Add("@nID", EOBPatPayDtl.LineNotes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                            oParameters.Add("@nClaimNo", EOBPatPayDtl.LineNotes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBID", EOBPatPayDtl.LineNotes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentDetailID", _EOBPayCreditDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.LineNotes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                            oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.LineNotes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@sNoteCode", EOBPatPayDtl.LineNotes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                            oParameters.Add("@sNoteDescription", EOBPatPayDtl.LineNotes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                            oParameters.Add("@dNoteAmount", EOBPatPayDtl.LineNotes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                            oParameters.Add("@nPaymentNoteType", EOBPatPayDtl.LineNotes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nPaymentNoteSubType", EOBPatPayDtl.LineNotes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nIncludeNoteOnPrint", EOBPatPayDtl.LineNotes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
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

                                            if (_sqlCommand != null)
                                            {
                                                _sqlCommand.Parameters.Clear();
                                                _sqlCommand.Dispose();
                                                _sqlCommand = null;
                                            }
                                        }
                                    }

                                    #endregion " Add Line Notes "


                                    EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex].EOBPaymentID = _EOBPayId;
                                    EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex].EOBPaymentDetailID = Convert.ToInt64(_retVal.ToString());

                                    #region "Assign Credit Line Reference and Finance Line Reference to debit line wherever applicable"
                                    if (EOBPaymentPatient.EOBPatientPaymentLineDetails[clmInsPayLnIndex].IsMainCreditLine == true)
                                    {
                                        //All Remaining Credit Lines
                                        for (int nAsgn = 0; nAsgn <= EOBPaymentPatient.EOBPatientPaymentLineDetails.Count - 1; nAsgn++)
                                        {
                                            if (EOBPaymentPatient.EOBPatientPaymentLineDetails[nAsgn].IsMainCreditLine == false)
                                            {
                                                EOBPaymentPatient.EOBPatientPaymentLineDetails[nAsgn].MainCreditLineID = _EOBPayCreditDtlId;
                                            }
                                        }
                                        //All Debit Lines
                                        for (int nAsgnClaim = 0; nAsgnClaim <= EOBPaymentPatient.PatientClaims.Count - 1; nAsgnClaim++)
                                        {
                                            for (int nAsgnClmLine = 0; nAsgnClmLine <= EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines.Count - 1; nAsgnClmLine++)
                                            {
                                                for (int nAsgn = 0; nAsgn <= EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails.Count - 1; nAsgn++)
                                                {
                                                    if (EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].IsMainCreditLine == false)
                                                    {
                                                        EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].MainCreditLineID = _EOBPayCreditDtlId;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //Assign Reference Number to debit lines
                                    for (int nAsgnClaim = 0; nAsgnClaim <= EOBPaymentPatient.PatientClaims.Count - 1; nAsgnClaim++)
                                    {
                                        for (int nAsgnClmLine = 0; nAsgnClmLine <= EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines.Count - 1; nAsgnClmLine++)
                                        {
                                            for (int nAsgn = 0; nAsgn <= EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails.Count - 1; nAsgn++)
                                            {
                                                if (EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].RefFinanceLieNo == EOBPatPayDtl.FinanceLieNo)
                                                {
                                                    if (EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].UseRefFinanceLieNo == true)
                                                    {
                                                        EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].RefEOBPaymentID = _EOBPayId;
                                                        EOBPaymentPatient.PatientClaims[nAsgnClaim].CliamLines[nAsgnClmLine].EOBPatientPaymentLineDetails[nAsgn].RefEOBPaymentDetailID = _EOBPayCreditDtlId;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //Assign Main Credit Line ID to Direct Transaction Reserve Lines
                                    for (int nResClmLine = 0; nResClmLine <= EOBPaymentPatient.EOBPatientPaymentReserveLineDetail.Count - 1; nResClmLine++)
                                    {
                                        EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].MainCreditLineID = _EOBPayCreditDtlId;
                                        if (EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].UseRefFinanceLieNo == true)
                                        {
                                            //here checking id is 0, bcz its patient payment directlly send to use reserve, other wise as per insurace payment no need
                                            if (EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].RefEOBPaymentID == 0 && EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].RefEOBPaymentDetailID == 0)
                                            {
                                                EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].RefEOBPaymentID = _EOBPayId;
                                                EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[nResClmLine].RefEOBPaymentDetailID = _EOBPayCreditDtlId;
                                            }
                                        }
                                    }
                                    #endregion

                                     
                                        EOBPatPayDtl = null;
                                   
                                }
                            }
                        }
                        #endregion

                        #region "Payment Line Master Reserve (Debit) Entry"
                        if (EOBPaymentPatient.EOBPatientPaymentReserveLineDetail != null && EOBPaymentPatient.EOBPatientPaymentReserveLineDetail.Count > 0)
                        {
                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentPatient.EOBPatientPaymentReserveLineDetail.Count; clmInsPayLnIndex++)
                            {
                                if (EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[clmInsPayLnIndex] != null)
                                {
                                    _EOBPayDtlId = 0;
                                    EOBPatPayDtl = EOBPaymentPatient.EOBPatientPaymentReserveLineDetail[clmInsPayLnIndex];

                                    oParameters.Clear();
                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBID", EOBPatPayDtl.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBDtlID", EOBPatPayDtl.EOBDtlID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBPaymentDetailID", EOBPatPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionLineNo", EOBPatPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nPatientID", EOBPatPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nDOSFrom", EOBPatPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nDOSTo", EOBPatPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@sCPTCode", EOBPatPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sCPTDescription", EOBPatPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                    if (EOBPatPayDtl.IsNullAmount == false)
                                    {
                                        oParameters.Add("@nAmount", EOBPatPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    oParameters.Add("@nPaymentType", EOBPatPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentSubType", EOBPatPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaySign", EOBPatPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPayMode", EOBPatPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nAccountID", EOBPatPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nAccountType", EOBPatPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nMSTAccountID", EOBPatPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nMSTAccountType", EOBPatPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentTrayID", EOBPatPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sPaymentTrayCode", EOBPatPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sPaymentTrayDescription", EOBPatPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@nUserID", EOBPatPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sUserName", EOBPatPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@dtCreatedDateTime", EOBPatPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@dtModifiedDateTime", EOBPatPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@nClinicID", EOBPatPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    //..RefEOBPaymentID & RefEOBPaymentDetailID identifies from where (which payment source or check) this payment
                                    //..is coming from
                                    if (EOBPatPayDtl.RefEOBPaymentID == 0) { EOBPatPayDtl.RefEOBPaymentID = _EOBPayId; }
                                    if (EOBPatPayDtl.RefEOBPaymentDetailID == 0) { EOBPatPayDtl.RefEOBPaymentDetailID = _EOBPayCreditDtlId; }

                                    oParameters.Add("@nRefEOBPaymentID", EOBPatPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBPatPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    //..ResEOBPaymentID,ResEOBPaymentDetailID has the reference id's for the reserve amount
                                    oParameters.Add("@nResEOBPaymentID", EOBPatPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nResEOBPaymentDetailID", EOBPatPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);


                                    oParameters.Add("@nContactInsID", EOBPatPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nCreditLineID", EOBPatPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    oParameters.Add("@nCloseDate", EOBPatPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    // Newly added parameters by pankaj
                                    oParameters.Add("@nTrackTrnID", EOBPatPayDtl.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nTrackTrnDtlID", EOBPatPayDtl.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@sSubClaimNo", EOBPatPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),
                                    oParameters.Add("@bIsVoid", EOBPatPayDtl.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                    oParameters.Add("@nVoidCloseDate", EOBPatPayDtl.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                    oParameters.Add("@nVoidTrayID", EOBPatPayDtl.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                    oParameters.Add("@nVoidType", EOBPatPayDtl.VoidType, ParameterDirection.Input, SqlDbType.Int);

                                    oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                    oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);

                                    oParameters.Add("@nOldResEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nOldResEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                                    oParameters.Add("@nPAccountID", EOBPatPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nAccountPatientID", EOBPatPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nGuarantorID", EOBPatPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
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
                                    if (_sqlCommand != null)
                                    {
                                        _sqlCommand.Parameters.Clear();
                                        _sqlCommand.Dispose();
                                        _sqlCommand = null;
                                    }
                                    #region " Add Line Notes "

                                    if (EOBPatPayDtl.LineNotes != null && EOBPatPayDtl.LineNotes.Count > 0)
                                    {
                                     //   Object _RcValue = null;

                                        for (int rcInd = 0; rcInd < EOBPatPayDtl.LineNotes.Count; rcInd++)
                                        {
                                          //  _RcValue = null;
                                            oParameters.Clear();

                                            oParameters.Add("@nID", EOBPatPayDtl.LineNotes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                            oParameters.Add("@nClaimNo", EOBPatPayDtl.LineNotes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBID", EOBPatPayDtl.LineNotes[rcInd].EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentDetailID", _EOBPayDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.LineNotes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                            oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.LineNotes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@sNoteCode", EOBPatPayDtl.LineNotes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                            oParameters.Add("@sNoteDescription", EOBPatPayDtl.LineNotes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                            oParameters.Add("@dNoteAmount", EOBPatPayDtl.LineNotes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                            oParameters.Add("@nPaymentNoteType", EOBPatPayDtl.LineNotes[rcInd].PaymentNoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nPaymentNoteSubType", EOBPatPayDtl.LineNotes[rcInd].PaymentNoteSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                                            oParameters.Add("@nIncludeNoteOnPrint", EOBPatPayDtl.LineNotes[rcInd].IncludeOnPrint, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
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
                                            if (_sqlCommand != null)
                                            {
                                                _sqlCommand.Parameters.Clear();
                                                _sqlCommand.Dispose();
                                                _sqlCommand = null;
                                            }
                                        }
                                    }

                                    #endregion " Add Line Notes "


                                     
                                        EOBPatPayDtl = null;
                                     
                                }
                            }
                        }
                        #endregion

                        #region " EOB Data Save "

                        if (_EOBPayId > 0 && EOBPaymentPatient.PatientClaims != null && EOBPaymentPatient.PatientClaims.Count > 0)
                        {
                            for (int clmIndex = 0; clmIndex < EOBPaymentPatient.PatientClaims.Count; clmIndex++)
                            {
                                PaymentPatClaim = EOBPaymentPatient.PatientClaims[clmIndex];
                                for (int clmLnIndex = 0; clmLnIndex < PaymentPatClaim.CliamLines.Count; clmLnIndex++)
                                {
                                    if (PaymentPatClaim.CliamLines[clmLnIndex] != null)
                                    {
                                        _EOBDtlId = 0;
                                        PaymentPatClaimLine = PaymentPatClaim.CliamLines[clmLnIndex];

                                        //nEOBPaymentID,nEOBID,nEOBPaymentDetailID,nBillingTransactionID,nBillingTransactionDetailID
                                        //nBillingTransactionLineNo,nPatientID,nDOSFrom,nDOSTo,sCPTCode,sCPTDescription,nAmount,
                                        //nPaymentType,nPaymentSubType,nPaySign,nRefEOBPaymentID,nRefEOBPaymentDetailID,nAccountID
                                        //nAccountType,nMSTAccountID,nMSTAccountType,nPaymentTrayID,nPaymentTrayCode,nPaymentTrayDescription
                                        //nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                                        oParameters.Clear();
                                        #region "EOB Service Line"
                                        if (_UseExtEOBID == true) { PaymentPatClaimLine.mEOBID = _EOBId; }
                                        oParameters.Add("@nEOBID", PaymentPatClaimLine.mEOBID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                        oParameters.Add("@nEOBDtlID", PaymentPatClaimLine.mEOBDtlID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                        oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0)
                                        //oParameters.Add("@nClaimNo", PaymentPatClaimLine.ClaimNumber, ParameterDirection.Input, SqlDbType.Int);//	int
                                        oParameters.Add("@nClaimNo", PaymentPatClaimLine.ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                        oParameters.Add("@nDOSFrom", PaymentPatClaimLine.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                        oParameters.Add("@nDOSTo", PaymentPatClaimLine.DOSTo, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                        oParameters.Add("@sCPTCode", PaymentPatClaimLine.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                        oParameters.Add("@sCPTDescription", PaymentPatClaimLine.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                        //oParameters.Add("@dCharges", PaymentPatClaimLine.Charges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        //oParameters.Add("@dUnit", PaymentPatClaimLine.Unit, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                        //oParameters.Add("@dTotalCharges", PaymentPatClaimLine.TotalCharges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        //oParameters.Add("@dAllowed", PaymentPatClaimLine.Allowed, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        //oParameters.Add("@dWriteOff", PaymentPatClaimLine.WriteOff, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        //oParameters.Add("@dNotCovered", PaymentPatClaimLine.NonCovered, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        //oParameters.Add("@dPayment", PaymentPatClaimLine.InsuranceAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        //oParameters.Add("@dCopay", PaymentPatClaimLine.Copay, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        //oParameters.Add("@dDeductible", PaymentPatClaimLine.Deductible, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        //oParameters.Add("@dCoInsurance", PaymentPatClaimLine.CoInsurance, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                        //oParameters.Add("@dWithhold", PaymentPatClaimLine.Withhold, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)

                                        if (PaymentPatClaimLine.IsNullCharges == false)
                                        {
                                            oParameters.Add("@dCharges", PaymentPatClaimLine.Charges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullUnit == false)
                                        {
                                            oParameters.Add("@dUnit", PaymentPatClaimLine.Unit, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dUnit", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                        }

                                        if (PaymentPatClaimLine.IsNullTotalCharges == false)
                                        {
                                            oParameters.Add("@dTotalCharges", PaymentPatClaimLine.TotalCharges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dTotalCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullAllowed == false)
                                        {
                                            oParameters.Add("@dAllowed", PaymentPatClaimLine.Allowed, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dAllowed", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullWriteOff == false)
                                        {
                                            oParameters.Add("@dWriteOff", PaymentPatClaimLine.WriteOff, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dWriteOff", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullNonCovered == false)
                                        {
                                            oParameters.Add("@dNotCovered", PaymentPatClaimLine.NonCovered, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dNotCovered", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullInsurance == false)
                                        {
                                            oParameters.Add("@dPayment", PaymentPatClaimLine.InsuranceAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dPayment", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullCopay == false)
                                        {
                                            oParameters.Add("@dCopay", PaymentPatClaimLine.Copay, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dCopay", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullDeductible == false)
                                        {
                                            oParameters.Add("@dDeductible", PaymentPatClaimLine.Deductible, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dDeductible", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        if (PaymentPatClaimLine.IsNullCoInsurance == false)
                                        {
                                            oParameters.Add("@dCoInsurance", PaymentPatClaimLine.CoInsurance, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                        }
                                        else
                                        {
                                            oParameters.Add("@dCoInsurance", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                        }

                                        if (PaymentPatClaimLine.IsNullWithhold == false)
                                        {
                                            oParameters.Add("@dWithhold", PaymentPatClaimLine.Withhold, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }
                                        else
                                        {
                                            oParameters.Add("@dWithhold", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                        }

                                        oParameters.Add("@nPaymentTrayID", PaymentPatClaimLine.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                        oParameters.Add("@sPaymentTrayCode", PaymentPatClaimLine.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                                        oParameters.Add("@sPaymentTrayDescription", PaymentPatClaimLine.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked

                                        oParameters.Add("@nUserID", PaymentPatClaimLine.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                        oParameters.Add("@sUserName", PaymentPatClaimLine.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked
                                        oParameters.Add("@dtCreatedDateTime", PaymentPatClaimLine.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked
                                        oParameters.Add("@dtModifiedDateTime", PaymentPatClaimLine.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked

                                        oParameters.Add("@nPatientID", PaymentPatClaimLine.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                        oParameters.Add("@nInsuraceID", PaymentPatClaimLine.PatInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                        oParameters.Add("@nContactID", PaymentPatClaimLine.InsContactID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked

                                        oParameters.Add("@nClinicID", PaymentPatClaimLine.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                        oParameters.Add("@nEOBType", PaymentPatClaimLine.EOBType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);// int,
                                        oParameters.Add("@nBillingTransactionID", PaymentPatClaimLine.BLTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                        oParameters.Add("@nBillingTransactionDetailID", PaymentPatClaimLine.BLTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                        oParameters.Add("@nBillingTransactionLineNo", PaymentPatClaimLine.BLTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)

                                        oParameters.Add("@bUseExtEOBID", _UseExtEOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                        oParameters.Add("@nTrackTrnID", PaymentPatClaimLine.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                        oParameters.Add("@nTrackTrnDtlID", PaymentPatClaimLine.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                        oParameters.Add("@sSubClaimNo", PaymentPatClaimLine.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),

                                        oParameters.Add("@nCloseDate", PaymentPatClaimLine.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                        oParameters.Add("@bIsVoid", PaymentPatClaimLine.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                        oParameters.Add("@nVoidCloseDate", PaymentPatClaimLine.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                        oParameters.Add("@nVoidTrayID", PaymentPatClaimLine.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                        oParameters.Add("@nVoidType", PaymentPatClaimLine.VoidType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                                        oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                        oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                        oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);

                                        //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                                        oParameters.Add("@nPAccountID", EOBPaymentPatient.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                        oParameters.Add("@nAccountPatientID", EOBPaymentPatient.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                        oParameters.Add("@nGuarantorID", EOBPaymentPatient.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                        //End

                                        if (_UseExtEOBID == false) { _UseExtEOBID = true; }
                                        _retVal = null;
                                        _valRet = null;

                                        //oDB.Connect(false);
                                        //oDB.Execute("BL_INSERT_EOBPayment_EOB_PatPayment", oParameters, out _retVal, out _valRet);
                                        //oDB.Disconnect();

                                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                                        _sqlCommand.Connection = _sqlConnection;
                                        _sqlCommand.Transaction = _sqlTransaction;
                                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                                        _sqlCommand.CommandText = "BL_INSERT_EOBPayment_EOB_PatPayment";

                                        _result = 0;
                                        _result = _sqlCommand.ExecuteNonQuery();

                                        if (_sqlCommand.Parameters["@nEOBID"].Value != null && Convert.ToString(_sqlCommand.Parameters["@nEOBID"].Value).Trim() != "")
                                        { _retVal = Convert.ToInt64(_sqlCommand.Parameters["@nEOBID"].Value); }

                                        if (_sqlCommand.Parameters["@nEOBDtlID"].Value != null && Convert.ToString(_sqlCommand.Parameters["@nEOBDtlID"].Value).Trim() != "")
                                        { _valRet = Convert.ToInt64(_sqlCommand.Parameters["@nEOBDtlID"].Value); }
                                        if (_sqlCommand != null)
                                        {
                                            _sqlCommand.Parameters.Clear();
                                            _sqlCommand.Dispose();
                                            _sqlCommand = null;
                                        }
                                        #endregion

                                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                        { _EOBId = Convert.ToInt64(_retVal); }

                                        if (_valRet != null && Convert.ToString(_valRet).Trim() != "")
                                        { _EOBDtlId = Convert.ToInt64(_valRet); }

                                        #region " Add Line Adjustment Codes "

                                        if (PaymentPatClaimLine.LineAdjestmentCodes != null && PaymentPatClaimLine.LineAdjestmentCodes.Count > 0)
                                        {
                                           // Object _RcValue = null;

                                            for (int rcInd = 0; rcInd < PaymentPatClaimLine.LineAdjestmentCodes.Count; rcInd++)
                                            {
                                             //   _RcValue = null;
                                                oParameters.Clear();

                                                oParameters.Add("@nID", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                                oParameters.Add("@nClaimNo", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nEOBPaymentDetailID", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@nBillingTransactionID", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                                oParameters.Add("@nBillingTransactionDetailID", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                                oParameters.Add("@sReasonCode", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                                oParameters.Add("@sReasonDescription", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                                if (PaymentPatClaimLine.LineAdjestmentCodes[rcInd].IsNullAmount == false)
                                                {
                                                    oParameters.Add("@dReasonAmount", PaymentPatClaimLine.LineAdjestmentCodes[rcInd].Amount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                }
                                                else
                                                {
                                                    oParameters.Add("@dReasonAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                                }

                                                oParameters.Add("@nType", EOBCommentType.Adjustment.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
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

                                                _result = 0;
                                                _result = _sqlCommand.ExecuteNonQuery();

                                                if (_sqlCommand.Parameters["@nID"].Value != null && Convert.ToString(_sqlCommand.Parameters["@nID"].Value).Trim() != "")
                                                { _retVal = Convert.ToInt64(_sqlCommand.Parameters["@nID"].Value); }
                                                if (_sqlCommand != null)
                                                {
                                                    _sqlCommand.Parameters.Clear();
                                                    _sqlCommand.Dispose();
                                                    _sqlCommand = null;
                                                }
                                            }
                                        }

                                        #endregion " Add Line Adjustment Codes "

                                        #region " EOB Financial Service Line Save "

                                        if (_EOBPayId > 0 && PaymentPatClaimLine.EOBPatientPaymentLineDetails != null && PaymentPatClaimLine.EOBPatientPaymentLineDetails.Count > 0)
                                        {
                                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < PaymentPatClaimLine.EOBPatientPaymentLineDetails.Count; clmInsPayLnIndex++)
                                            {
                                                if (PaymentPatClaimLine.EOBPatientPaymentLineDetails[clmInsPayLnIndex] != null)
                                                {
                                                    _EOBPayDtlId = 0;
                                                    EOBPatPayDtl = PaymentPatClaimLine.EOBPatientPaymentLineDetails[clmInsPayLnIndex];

                                                    oParameters.Clear();
                                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nEOBDtlID", _EOBDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nEOBPaymentDetailID", EOBPatPayDtl.EOBPaymentDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nBillingTransactionLineNo", EOBPatPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nPatientID", EOBPatPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nDOSFrom", EOBPatPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nDOSTo", EOBPatPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@sCPTCode", EOBPatPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                    oParameters.Add("@sCPTDescription", EOBPatPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                                    if (EOBPatPayDtl.IsNullAmount == false)
                                                    {
                                                        oParameters.Add("@nAmount", EOBPatPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                    }
                                                    else
                                                    {
                                                        oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                                    }

                                                    oParameters.Add("@nPaymentType", EOBPatPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPaymentSubType", EOBPatPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPaySign", EOBPatPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPayMode", EOBPatPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nAccountID", EOBPatPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nAccountType", EOBPatPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nMSTAccountID", EOBPatPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nMSTAccountType", EOBPatPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                                    oParameters.Add("@nPaymentTrayID", EOBPatPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@sPaymentTrayCode", EOBPatPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                                    oParameters.Add("@sPaymentTrayDescription", EOBPatPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                    oParameters.Add("@nUserID", EOBPatPayDtl.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@sUserName", EOBPatPayDtl.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                                    oParameters.Add("@dtCreatedDateTime", EOBPatPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                    oParameters.Add("@dtModifiedDateTime", EOBPatPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                                    oParameters.Add("@nClinicID", EOBPatPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                                    //..RefEOBPaymentID & RefEOBPaymentDetailID identifies from where (which payment source or check) this payment
                                                    //..is coming from
                                                    if (EOBPatPayDtl.RefEOBPaymentID == 0) { EOBPatPayDtl.RefEOBPaymentID = _EOBPayId; }
                                                    if (EOBPatPayDtl.RefEOBPaymentDetailID == 0) { EOBPatPayDtl.RefEOBPaymentDetailID = _EOBPayCreditDtlId; }

                                                    oParameters.Add("@nRefEOBPaymentID", EOBPatPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBPatPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                                    //..ResEOBPaymentID,ResEOBPaymentDetailID has the reference id's for the reserve amount
                                                    oParameters.Add("@nResEOBPaymentID", EOBPatPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nResEOBPaymentDetailID", EOBPatPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);

                                                    oParameters.Add("@nContactInsID", EOBPatPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@nCreditLineID", EOBPatPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@nEOBVoidPaymentID", _EOBVoidPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                                    oParameters.Add("@nCloseDate", EOBPatPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                                    // Newly added parameters by pankaj
                                                    oParameters.Add("@nTrackTrnID", EOBPatPayDtl.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@nTrackTrnDtlID", EOBPatPayDtl.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                                    oParameters.Add("@sSubClaimNo", EOBPatPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),
                                                    oParameters.Add("@bIsVoid", EOBPatPayDtl.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                                    oParameters.Add("@nVoidCloseDate", EOBPatPayDtl.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                                    oParameters.Add("@nVoidTrayID", EOBPatPayDtl.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                                    oParameters.Add("@nVoidType", EOBPatPayDtl.VoidType, ParameterDirection.Input, SqlDbType.Int);

                                                    oParameters.Add("@bIsPaymentVoid", false, ParameterDirection.Input, SqlDbType.Bit);
                                                    oParameters.Add("@nPaymentVoidCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nPaymentVoidTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                                                   
                                                    oParameters.Add("@nOldResEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nOldResEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                                                    //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                                                    oParameters.Add("@nPAccountID", EOBPatPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nAccountPatientID", EOBPatPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    oParameters.Add("@nGuarantorID", EOBPatPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
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
                                                    if (_sqlCommand != null)
                                                    {
                                                        _sqlCommand.Parameters.Clear();
                                                        _sqlCommand.Dispose();
                                                        _sqlCommand = null;
                                                    }
                                                   
                                                        EOBPatPayDtl = null;
                                                    


                                                }
                                            }
                                        }


                                        #endregion " EOB Financial Service Line Save "

                                        PaymentPatClaimLine = null;
                                    }
                                }
                                PaymentPatClaim = null;
                            }
                        }

                        #endregion " EOB Data Save "

                        EOBPatientPaymentMasterLines = EOBPaymentPatient.EOBPatientPaymentLineDetails;

                        _sqlTransaction.Commit();
                        _sqlConnection.Close();

                        #region " Save last selected Close date "

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseConnectionString);
                        oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(gloDateMaster.gloDate.DateAsDate(EOBPaymentPatient.CloseDate)).ToString("MM/dd/yyyy"), _clinicId, EOBPaymentPatient.UserID, gloSettings.SettingFlag.User);
                        oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", EOBPaymentPatient.PaymentTrayID.ToString(), _clinicId, EOBPaymentPatient.UserID, gloSettings.SettingFlag.User);
                        oSettings.Dispose();
                        oSettings = null;
                        //start Abhisekh  3 sept 2010

                        gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, AppSettings.ConnectionStringEMR);
                        ogloBilling.SaveUserWiseCloseDay(gloDateMaster.gloDate.DateAsDate(EOBPaymentPatient.CloseDate).ToString(), CloseDayType.Payment, _clinicId);
                        ogloBilling.Dispose();
                        ogloBilling = null;
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

            public DataTable GetPendingChecks()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                DataTable _dtPendingChecks = null;

                try
                {
                    oDB.Connect(false);
                    oDB.Retrive("BL_SELECT_EOBPENDINGCHECKS", out _dtPendingChecks);
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
                   // if (_dtPendingChecks != null) { _dtPendingChecks.Dispose(); }
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
                    " FROM BL_EOBPayment_ActionStatus WITH (NOLOCK)" +
                    " WHERE nID > 0 AND sCode IS NOT NULL AND sDescription IS NOT NULL AND nClinicID = " + _clinicId + " ";

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

                            if (Convert.ToString(_dtPayPartyCode.Rows[i]["nInsuranceFlagNo"]).Trim() != "" && Convert.ToString(_dtPayPartyCode.Rows[i]["InsuranceName"]).Trim() != "")
                            {
                                _conCodeDesc = Convert.ToString(_dtPayPartyCode.Rows[i]["nInsuranceFlagNo"]).Trim().ToUpper() + "-" + Convert.ToString(_dtPayPartyCode.Rows[i]["InsuranceName"]).Trim().ToUpper() + "|";
                                _payPartyCode += _conCodeDesc;
                            }
                        }

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

            public DataTable GetExpectedCopayAmt(Int64 Patientid)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
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
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                }

                return _dtCopayAlert;
            }

            public DataTable GetLastPatientPmtAmt(Int64 Patientid)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                DataTable _dtCopayAlert = null;
                string _sqlQuery = "";

                try
                {
                    oDB.Connect(false);

                    /*_sqlQuery = "  SELECT top(1) BL_EOBPayment_MST.nCloseDate ,"  
                                     +" BL_EOBPayment_MST.nCheckAmount,  "
                                     + " BL_EOBPayment_MST.sCheckNumber,  "
                                     +" BL_EOBPayment_MST.nMSTAccountID,  "
                                     +" dtCreatedDateTime  " 
                                     +" FROM BL_EOBPayment_MST  "
                                     +" WHERE " 
                                     +" BL_EOBPayment_MST.nClinicID = 1 "
                                     +" AND BL_EOBPayment_MST.nMSTAccountID ="  + Patientid   
                                     +" AND BL_EOBPayment_MST.nMSTAccountType =  1"   
                                     +" AND nPayerType = 1"
                                     + " AND nPayerId =" + Patientid
                                     +" AND("   
                                     +"(BL_EOBPayment_MST.nPaymentMode = 1 AND BL_EOBPayment_MST.nCheckAmount <> 0)"
                                     +"OR (BL_EOBPayment_MST.nPaymentMode = 2 AND BL_EOBPayment_MST.sCheckNumber <> '' AND BL_EOBPayment_MST.nCheckAmount <> 0)" 
                                     +"OR (BL_EOBPayment_MST.nPaymentMode = 3 AND BL_EOBPayment_MST.nCheckAmount <> 0)" 
                                     +"OR (BL_EOBPayment_MST.nPaymentMode = 4 AND BL_EOBPayment_MST.nCheckAmount <> 0)" 
                                     +"OR (BL_EOBPayment_MST.nPaymentMode = 5 AND BL_EOBPayment_MST.sCheckNumber <> '' AND BL_EOBPayment_MST.nCheckAmount <> 0)" 
                                     +")"
                                     + "AND ((ISNULL(bIsPaymentVoid,0) = 0 AND ISNULL(nVoidType,0) = 0))"
                                     + "ORDER BY dtCreatedDateTime Desc"; */

                    _sqlQuery = " SELECT  BL_EOBPayment_MST.nCloseDate , BL_EOBPayment_MST.nCheckAmount,"
                                + " BL_EOBPayment_MST.sCheckNumber,   BL_EOBPayment_MST.nMSTAccountID,"  
                                + " dtCreatedDateTime,BL_EOBPayment_MST.nVoidType"   
                                + " FROM BL_EOBPayment_MST WITH (NOLOCK)  WHERE  BL_EOBPayment_MST.nClinicID = " + _clinicId
                                + " AND BL_EOBPayment_MST.nMSTAccountID = " + Patientid
                                + " AND BL_EOBPayment_MST.nMSTAccountType = 1"
                                + " AND nPayerType = 1 AND nPayerId = " + Patientid
                                + " AND((BL_EOBPayment_MST.nPaymentMode = 1 AND BL_EOBPayment_MST.nCheckAmount <> 0)"
                                + " OR (BL_EOBPayment_MST.nPaymentMode = 2 AND BL_EOBPayment_MST.sCheckNumber <> ''"
                                + " AND BL_EOBPayment_MST.nCheckAmount <> 0)OR (BL_EOBPayment_MST.nPaymentMode = 3"
                                + " AND BL_EOBPayment_MST.nCheckAmount <> 0)OR (BL_EOBPayment_MST.nPaymentMode = 4"
                                + " AND BL_EOBPayment_MST.nCheckAmount <> 0)OR (BL_EOBPayment_MST.nPaymentMode = 5"
                                + " AND BL_EOBPayment_MST.sCheckNumber <> '' AND BL_EOBPayment_MST.nCheckAmount <> 0))"
                        //+ " AND  (ISNULL(bIsPaymentVoid,0) = 0 AND ISNULL(nVoidType,0) = 0)"
                                + " AND ( "
                                + " (ISNULL(bIsPaymentVoid,0) = 1 AND ISNULL(nVoidType,0) = 2 AND CONVERT(DATETIME,(dbo.CONVERT_TO_DATE(BL_EOBPayment_MST.nPaymentVoidCloseDate))) > dbo.gloGetDate()) "
                                + " OR "
                                + "	(ISNULL(bIsPaymentVoid,0) = 0 AND ISNULL(nVoidType,0) = 0))"
                                + " and (BL_EOBPayment_MST.nEOBPaymentID not in ("
                                + " select nEOBPaymentID from BL_EOBPayment_DTL WITH (NOLOCK) where nPaymentType = 7 "
                                + " and nPaymentSubType = 14 and nPatientId =  " + Patientid + "))"
                                + " ORDER BY dtCreatedDateTime Desc";

                    oDB.Retrive_Query(_sqlQuery, out _dtCopayAlert);
                    oDB.Disconnect();


                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                }

                return _dtCopayAlert;
            }

            public DataRow GetPatientBalances(Int64 PatientID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

                string _sqlQuery = string.Empty;

                DataRow _patientBalance = null;
                DataTable _dtPatientBalance = null;

                try
                {
                    oDB.Connect(false);
                    oParameters.Clear();
                    oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                    oDB.Retrive("BL_GET_PATIENT_BALANCE_V2", oParameters, out _dtPatientBalance);
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
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (_dtPatientBalance != null) { _dtPatientBalance.Dispose(); _dtPatientBalance = null; }
                }
                return _patientBalance;
            }

            public string GetAdjustmentCodes()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _payAdjustment = "";
                DataTable _dt = null;
                string _sqlQuery = "";
                string _conCodeDesc = "";

                try
                {
                    _sqlQuery = " SELECT ISNULL(sAdjustmentTypeCode,'') AS Code,ISNULL(sAdjustmentTypeDesc,'') AS Description " +
                    " FROM BL_AdjustmentType_MST WITH (NOLOCK) " +
                    " WHERE " +
                    " nClinicID = " + _clinicId + " AND bIsBlocked = '" + false + "'";

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
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                    if (_dt != null) { _dt.Dispose(); _dt = null; }
                    if (_sqlQuery != null) { _sqlQuery = null; }
                }

                return _payAdjustment;
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
                     " FROM BL_EOBPayment_MST WITH(NOLOCK) where  sPaymentNoPrefix = '" + Prefix + "' AND nClinicID = " + _clinicId + " ";

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

            public bool IsExistCheck(string CheckNumber)
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
                        _sqlQuery = "SELECT COUNT(*) FROM BL_EOBPayment_MST WITH (NOLOCK) WHERE UPPER(sCheckNumber) = '" + CheckNumber.Trim().ToUpper() + "'";
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

            public DataTable GetCopayAlert(Int64 Patientid, DateTime CopayDate)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                DataTable _dtCopayAlert = null;
                Object retVal = null;
                string _sqlQuery = "";

                try
                {
                    oDB.Connect(false);

                    _sqlQuery = " SELECT Count(AS_Appointment_DTL.dtStartDate) " +
                    " FROM  AS_Appointment_MST WITH (NOLOCK) INNER JOIN AS_Appointment_DTL WITH (NOLOCK)  " +
                    " ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID " +
                    " WHERE " +
                    " (AS_Appointment_MST.nPatientID = " + Patientid + ")  " +
                    " AND (AS_Appointment_DTL.dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(CopayDate.ToShortDateString()) + ")  " +
                    " AND (AS_Appointment_DTL.nClinicID = " + this.ClinicID + ")";

                    retVal = oDB.ExecuteScalar_Query(_sqlQuery);

                    if (retVal != null && Convert.ToString(retVal) != "" && Convert.ToInt64(retVal) > 0)
                    {
                        _sqlQuery = "";
                        decimal _Pending = 0;
                        #region "Pending Copay, Advance and Reserve"
                        _sqlQuery = "select nPaymentNoteSubType,sum(AvailableReserve) as AvailableReserve from " +
                        " ( " +
                            " SELECT  " +
                            " ISNULL(BL_EOB_Notes.nPaymentNoteSubType,'') AS nPaymentNoteSubType, " +
                            " ISNULL(( " +
                            " (BL_EOBPayment_DTL.nAmount) -  " +
                            " ISNULL((SELECT SUM(ISNULL(nAmount,0)) FROM BL_EOBPayment_DTL AS BL_EOBPayment_DTL_UseRes  WITH (NOLOCK) " +
                            " WHERE BL_EOBPayment_DTL_UseRes.nResEOBPaymentID = BL_EOBPayment_DTL.nEOBPaymentID AND  " +
                            " BL_EOBPayment_DTL_UseRes.nResEOBPaymentDetailID = BL_EOBPayment_DTL.nEOBPaymentDetailID   " +
                            " AND BL_EOBPayment_DTL_UseRes.npaymentsubtype <> 13 " +
                            " ),0) " +
                            " ),0) AS AvailableReserve " +
                            " FROM         BL_EOBPayment_DTL WITH (NOLOCK) INNER JOIN " +
                            " BL_EOBPayment_MST WITH (NOLOCK) ON BL_EOBPayment_DTL.nEOBPaymentID = BL_EOBPayment_MST.nEOBPaymentID INNER JOIN " +
                            " Patient WITH (NOLOCK) ON BL_EOBPayment_DTL.nPatientID = Patient.nPatientID INNER JOIN " +
                            " BL_EOB_Notes WITH (NOLOCK) ON BL_EOBPayment_DTL.nEOBPaymentID = BL_EOB_Notes.nEOBPaymentID AND  " +
                            " BL_EOBPayment_DTL.nEOBPaymentDetailID = BL_EOB_Notes.nEOBPaymentDetailID " +
                            " WHERE " +
                            " BL_EOBPayment_DTL.npaymenttype = 2 AND BL_EOBPayment_DTL.npaymentsubtype = 9 AND BL_EOBPayment_DTL.npaysign = 2 " +
                            " AND BL_EOBPayment_DTL.npatientid = " + Patientid + " AND BL_EOBPayment_DTL.nClinicID = " + this.ClinicID + " " +
                            " AND BL_EOBPayment_MST.nCloseDate = " + gloDateMaster.gloDate.DateAsNumber(CopayDate.ToShortDateString()) + " " +
                        " ) " +
                        " as Final	GROUP BY nPaymentNoteSubType ";

                        DataTable oCARData = null;
                        oDB.Retrive_Query(_sqlQuery, out oCARData);
                        if (oCARData != null && oCARData.Rows.Count > 0)
                        {
                            for (int i = 0; i <= oCARData.Rows.Count - 1; i++)
                            {
                                if (oCARData.Rows[i]["nPaymentNoteSubType"] != null && oCARData.Rows[i]["nPaymentNoteSubType"].ToString().Trim() != "")
                                {

                                    if (oCARData.Rows[i]["AvailableReserve"] != null && oCARData.Rows[i]["AvailableReserve"].ToString().Trim() != "")
                                    {
                                        _Pending = Convert.ToDecimal(Convert.ToString(oCARData.Rows[i]["AvailableReserve"]));
                                    }
                                    //public enum EOBPaymentSubType
                                    //{
                                    //    None = 0, Insurace = 1, Copay = 2, Advance = 3, Coinsurace = 4, Dedcutiable = 5, WriteOff = 6, WithHold = 7, Patient = 8, Reserved = 9, Other = 10, TakeBack = 11, Adjuestment = 12
                                    //}
                                    //PLEASE REFER THIS ENUM FROM BILLING FOR ANY MODIFICATION, 
                                    //CURRENTLY WE ARE USING HARD CODED VALUE TO AVOID BILLING REF IN PATIENT STRIP
                                    if (_Pending > 0)
                                    {
                                        //It is applicable to only copay, but for provision we have take here reserve and other type also, 
                                        //for work with copay only we mark amount 0 in and reserve and other
                                        if (Convert.ToInt32(oCARData.Rows[i]["nPaymentNoteSubType"]) == 2)
                                        {
                                           // _Pending = _Pending;
                                        }
                                        else if (Convert.ToInt32(oCARData.Rows[i]["nPaymentNoteSubType"]) == 3)
                                        {
                                            _Pending = 0;
                                        }
                                        else if (Convert.ToInt32(oCARData.Rows[i]["nPaymentNoteSubType"]) == 10)
                                        {
                                            _Pending = 0;
                                        }
                                    }
                                }
                            }
                        }
                        if (oCARData != null)
                        {
                            oCARData.Dispose();
                            oCARData = null;
                        }

                        #endregion

                        if (Convert.ToString(_Pending) != "" && Convert.ToInt64(_Pending) <= 0)
                        {
                            _sqlQuery = "";
                            _sqlQuery = " SELECT  distinct   nPatientID, ISNULL(nCoPay,0) AS nCoPay, nInsuranceID, sInsuranceName,nInsuranceFlag, " +
                            " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0)  " +
                            " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'   " +
                            " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'  " +
                            " ELSE '' END  AS SortOrder,   " +
                            " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0)     " +
                            " WHEN 0 THEN 4   " +
                            " ELSE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0)  END  AS SortIndex  " +
                            " FROM      PatientInsurance_DTL WITH (NOLOCK) " +
                            " WHERE     (nPatientID = " + Patientid + ") AND ISNULL(nCoPay,0) > 0 " +
                            " ORDER BY SortIndex";

                            oDB.Retrive_Query(_sqlQuery, out _dtCopayAlert);
                        }
                    }

                    oDB.Disconnect();

                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                }

                return _dtCopayAlert;
            }

            public bool IsCopayUnapplied(Int64 Patientid)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _sqlQuery = "";
                Object _retVal = null;
                bool _isCopayUnapplied = false;

                try
                {
                    oDB.Connect(false);

                    //_sqlQuery = " SELECT COUNT(*) FROM BL_Transaction_Payment_DTL " +
                    //" where nCoPayID IN (select nAdvPayID from BL_Transaction_AdvancePayment_MST where nOtherPaymentMode = " + PaymentOtherType.Copay.GetHashCode() +" " +
                    //   " and nPatientID = " + Patientid + " AND nClinicID = " + this.ClinicID + ")";

                    _sqlQuery = " select COUNT(nAdvPayID) from BL_Transaction_AdvancePayment_MST WITH (NOLOCK) " +
                    " where  " +
                    " nAdvPayID NOT IN (select nCoPayID from BL_Transaction_Payment_DTL WITH (NOLOCK) WHERE nPatientID = " + Patientid + " AND nClinicID = " + this.ClinicID + ") " +
                    " AND nOtherPaymentMode = " + PaymentOtherType.Copay.GetHashCode() + " and nPatientID = " + Patientid + " " +
                    " AND nClinicID = " + this.ClinicID + " ";


                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (_retVal != null)
                    { _isCopayUnapplied = Convert.ToBoolean(_retVal); }
                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
                finally
                { if (oDB != null) { oDB.Dispose(); oDB = null; } }

                return _isCopayUnapplied;
            }

            public DataTable GetTrackingInformation(Int64 nClaimNo, Int64 nTransactionMasterID, Int64 nTransactionMasterDetailID)
            {
                DataTable dtTrackInfo = null;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                oDB.Connect(false);

                //string sQuery = " SELECT TOP 1 BL_Transaction_Claim_MST.nTransactionID, BL_Transaction_Claim_Lines.nTransactionDetailID," +
                //                " BL_Transaction_Claim_MST.nClaimNo, BL_Transaction_Claim_MST.nSubClaimNo, nTransactionLineNo" +
                //                " FROM dbo.BL_Transaction_Claim_Lines" +
                //                " INNER JOIN dbo.BL_Transaction_Claim_MST ON BL_Transaction_Claim_MST.nTransactionMasterID = BL_Transaction_Claim_Lines.nTransactionMasterID" +
                //                " AND BL_Transaction_Claim_MST.nTransactionID = BL_Transaction_Claim_Lines.nTransactionID" +
                //                " WHERE nClaimNo = '" + Convert.ToString(nClaimNo) + "'" +
                //                " AND BL_Transaction_Claim_MST.nTransactionMasterID = '" + Convert.ToString(nTransactionMasterID) + "'" +
                //                " AND BL_Transaction_Claim_Lines.nTransactionMasterDetailID = '" + Convert.ToString(nTransactionMasterDetailID) + "'" +
                //                " ORDER BY BL_Transaction_Claim_MST.nSubClaimNo DESC";

                string sQuery = " SELECT TOP 1 BL_Transaction_Claim_MST.nTransactionID, BL_Transaction_Claim_Lines.nTransactionDetailID," +
                                " BL_Transaction_Claim_MST.nClaimNo, BL_Transaction_Claim_MST.nSubClaimNo, nTransactionLineNo,ISNULL(dbo.BL_Transaction_Claim_MST.bIsHold,0) as bIsHold, " +
                                " dbo.GET_HoldInformation(dbo.BL_Transaction_Claim_MST.bIsHold,BL_Transaction_Claim_MST.nContactID,BL_Transaction_Claim_MST.nTransactionID) as HoldInfo " +
                                " FROM dbo.BL_Transaction_Claim_Lines WITH (NOLOCK)" +
                                " INNER JOIN dbo.BL_Transaction_Claim_MST WITH (NOLOCK) ON BL_Transaction_Claim_MST.nTransactionMasterID = BL_Transaction_Claim_Lines.nTransactionMasterID" +
                                " AND BL_Transaction_Claim_MST.nTransactionID = BL_Transaction_Claim_Lines.nTransactionID" +
                                " WHERE BL_Transaction_Claim_MST.nTransactionMasterID = '" + Convert.ToString(nTransactionMasterID) + "'" +
                                " AND BL_Transaction_Claim_Lines.nTransactionMasterDetailID = '" + Convert.ToString(nTransactionMasterDetailID) + "' " +
                                " ORDER BY BL_Transaction_Claim_MST.dtCreateDate DESC,BL_Transaction_Claim_MST.nTransactionID DESC";


                try
                {
                    oDB.Retrive_Query(sQuery, out dtTrackInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while getting Tracking Information..");
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                }
                finally
                {
                    oDB.Dispose(); oDB = null;
                }
                return dtTrackInfo;
            }

            public Int64 VoidPatientPayment(Int64 EOBPaymentID, Int64 PatientId, string PatientName, string CloseDate, string VoidNote, int VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName)
            {
                System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(_databaseConnectionString);
                System.Data.SqlClient.SqlCommand _sqlCommand = null;
                System.Data.SqlClient.SqlTransaction _sqlTransaction = null;
                EOBPayment.Common.EOBPatientPaymentDetails EOBPatientPaymentDtls = new EOBPayment.Common.EOBPatientPaymentDetails();
                EOBPayment.Common.EOBPatientPaymentDetail EOBPatPayDtl = null;
                EOBPayment.Common.PaymentPatientLines PaymentPatientEOBLines = new global::gloBilling.EOBPayment.Common.PaymentPatientLines();
                EOBPayment.Common.PaymentPatientLine PaymentPatientEOBLine = null;
                EOBPayment.Common.PaymentPatient PaymentPatientMST = new global::gloBilling.EOBPayment.Common.PaymentPatient();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                object _retVal = null;
                object _valRet = null;
                Int64 _EOBPayId = 0;
                Int64 _EOBPayDtlId = 0;
                Int64 EOBId = 0;
                Int64 EOBDtlId = 0;
                string _sqlQuery = "";
             //   bool _UseExtEOBID = false;
                try
                {
                    if (EOBPatientPaymentDtls != null)
                    {
                        _sqlConnection.Open();
                        _sqlTransaction = _sqlConnection.BeginTransaction();

                        PaymentPatientMST = GetMasterDetailsForPatientPaymentVoid(EOBPaymentID, ClinicID, VoidCloseDate, VoidTrayID, VoidTrayCode, VoidTrayName);
                        #region " Master Data Save "
                        if (PaymentPatientMST != null)
                        {
                            //nEOBPaymentID,nEOBRefNO,sPayerName,nPayerID,nPayerType,nPaymentMode,sCheckNumber,nCheckAmount,nCheckDate
                            //nMSTAccountID,nMSTAccountType,nPaymentTrayID,sPaymentTrayName,nCloseDate,sCardType,sCardSecurityNo
                            //nCardID,nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                            oParameters.Clear();

                            oParameters.Add("@sPaymentNo", PaymentPatientMST.PaymentNumber, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@sPaymentNoPrefix", PaymentPatientMST.PaymentNumberPefix, ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                            oParameters.Add("@nEOBRefNO", PaymentPatientMST.EOBRefNO, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                            oParameters.Add("@sPayerName", PaymentPatientMST.PayerName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Unchecked
                            oParameters.Add("@nPayerID", PaymentPatientMST.PayerID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                            oParameters.Add("@nPayerType", PaymentPatientMST.PayerType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                            oParameters.Add("@nPaymentMode", PaymentPatientMST.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                            oParameters.Add("@sCheckNumber", PaymentPatientMST.CheckNumber, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Unchecked
                            oParameters.Add("@nCheckAmount", PaymentPatientMST.CheckAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 0)	Unchecked
                            oParameters.Add("@nCheckDate", PaymentPatientMST.CheckDate, ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                            oParameters.Add("@nMSTAccountID", PaymentPatientMST.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                            oParameters.Add("@nMSTAccountType", PaymentPatientMST.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                            oParameters.Add("@nPaymentTrayID", PaymentPatientMST.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                            oParameters.Add("@sPaymentTrayCode", PaymentPatientMST.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                            oParameters.Add("@sPaymentTrayDescription", PaymentPatientMST.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255
                            oParameters.Add("@nCloseDate", PaymentPatientMST.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                            oParameters.Add("@sCardType", PaymentPatientMST.CardType, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                            oParameters.Add("@sCardSecurityNo", PaymentPatientMST.CardSecurityNo, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                            oParameters.Add("@nCardID", PaymentPatientMST.CardID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                            oParameters.Add("@sAuthorizationNo", PaymentPatientMST.AuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                            oParameters.Add("@nCardExpDate", PaymentPatientMST.CardExpiryDate, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                            oParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                            oParameters.Add("@sUserName", UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0)	Checked
                            oParameters.Add("@dtCreatedDateTime", PaymentPatientMST.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                            oParameters.Add("@dtModifiedDateTime", PaymentPatientMST.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                            oParameters.Add("@nClinicID", PaymentPatientMST.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                            oParameters.Add("@bIsVoid", PaymentPatientMST.IsVoid, ParameterDirection.Input, SqlDbType.Bit);
                            oParameters.Add("@nVoidCloseDate", PaymentPatientMST.VoidCloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nVoidTrayID", PaymentPatientMST.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);

                            oParameters.Add("@bIsPaymentVoid", true, ParameterDirection.Input, SqlDbType.Bit);
                            oParameters.Add("@nPaymentVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nPaymentVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);

                            oParameters.Add("@nVoidType", PaymentPatientMST.VoidType, ParameterDirection.Input, SqlDbType.Int);
                            oParameters.Add("@nVoidRefPaymentID", PaymentPatientMST.VoidRefPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                            //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                            oParameters.Add("@nPAccountID", PaymentPatientMST.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nAccountPatientID", PaymentPatientMST.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nGuarantorID", PaymentPatientMST.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                            //End

                            _sqlCommand = new System.Data.SqlClient.SqlCommand();
                            _sqlCommand = oDB.GetCmdParameters(oParameters);
                            _sqlCommand.Connection = _sqlConnection;
                            _sqlCommand.Transaction = _sqlTransaction;
                            _sqlCommand.CommandType = CommandType.StoredProcedure;
                            _sqlCommand.CommandText = "BL_INUP_EOBPayment_MST_PatPayment";

                            int _result = 0;
                            _result = _sqlCommand.ExecuteNonQuery();

                            if (_sqlCommand.Parameters["@nEOBPaymentID"].Value != null)
                            { _retVal = _sqlCommand.Parameters["@nEOBPaymentID"].Value; }
                            else
                            { _retVal = 0; }

                            if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                            { _EOBPayId = Convert.ToInt64(_retVal); }
                            if (_sqlCommand != null)
                            {
                                _sqlCommand.Parameters.Clear();
                                _sqlCommand.Dispose();
                                _sqlCommand = null;
                            }

                        }
                        #endregion "Master Data Save"

                        #region "Master Void Payment Note"

                        if (VoidNote != null)
                        {
                            Object _RcValue = null;
                            _RcValue = null;
                            oParameters.Clear();

                            oParameters.Add("@nID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                            oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nEOBVoidPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@dVoidAmount", PaymentPatientMST.CheckAmount * -1, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 0),
                            oParameters.Add("@sNoteDescription", VoidNote.ToString(), ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0),
                            oParameters.Add("@nVoidNoteType", PaymentPatientMST.VoidType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),	
                            oParameters.Add("@sUserName", UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0),
                            oParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);//	varchar(5),
                            oParameters.Add("@nClinicID", PaymentPatientMST.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	decimal(18, 2),
                            oParameters.Add("@nVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@sVoidTrayDescription", VoidTrayName.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                            oParameters.Add("@sVoidTrayCode", VoidTrayCode.ToString(), ParameterDirection.Input, SqlDbType.VarChar);

                            _sqlCommand = new System.Data.SqlClient.SqlCommand();
                            _sqlCommand = oDB.GetCmdParameters(oParameters);
                            _sqlCommand.Connection = _sqlConnection;
                            _sqlCommand.Transaction = _sqlTransaction;
                            _sqlCommand.CommandType = CommandType.StoredProcedure;
                            _sqlCommand.CommandText = "BL_INUP_EOBPaymentVoid_Notes";

                            int _result = 0;
                            _result = _sqlCommand.ExecuteNonQuery();

                            if (_sqlCommand.Parameters["@nID"].Value != null)
                            { _RcValue = _sqlCommand.Parameters["@nID"].Value; }
                            else
                            { _RcValue = 0; }
                            if (_sqlCommand != null)
                            {
                                _sqlCommand.Parameters.Clear();
                                _sqlCommand.Dispose();
                                _sqlCommand = null;
                            }
                        }


                        #endregion "Master Void Payment Note"

                        #region "EOB Data save for voiding patient payment "

                        PaymentPatientEOBLines = GetEOBLinesDetailForPatientPaymentVoid(EOBPaymentID, PatientId, VoidCloseDate, VoidTrayID, VoidTrayCode, VoidTrayName);

                        if (EOBPaymentID > 0 && PaymentPatientEOBLines != null && PaymentPatientEOBLines.Count > 0)
                        {
                            for (int _payVoidEOBLineIndex = 0; _payVoidEOBLineIndex < PaymentPatientEOBLines.Count; _payVoidEOBLineIndex++)
                            {
                                if (PaymentPatientEOBLines[_payVoidEOBLineIndex] != null)
                                {
                                    PaymentPatientEOBLine = PaymentPatientEOBLines[_payVoidEOBLineIndex];
                                    oParameters.Clear();

                                    oParameters.Add("@nEOBID", EOBId, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                    oParameters.Add("@nEOBDtlID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//

                                    //Code commented by Sagar Ghodke on 20100622
                                    //oParameters.Add("@nEOBPaymentID", PaymentPatientEOBLine.mEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0)
                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);
                                    //End code changes Sagar Ghodke,20100622

                                    //oParameters.Add("@nClaimNo", PaymentPatientEOBLine.ClaimNumber, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nClaimNo", PaymentPatientEOBLine.ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                    oParameters.Add("@nDOSFrom", PaymentPatientEOBLine.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nDOSTo", PaymentPatientEOBLine.DOSTo, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                    oParameters.Add("@sCPTCode", PaymentPatientEOBLine.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sCPTDescription", PaymentPatientEOBLine.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                    if (PaymentPatientEOBLine.IsNullCharges == false)
                                    {
                                        oParameters.Add("@dCharges", PaymentPatientEOBLine.Charges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@dCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    if (PaymentPatientEOBLine.IsNullUnit == false)
                                    {
                                        oParameters.Add("@dUnit", PaymentPatientEOBLine.Unit, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                    }
                                    else
                                    {
                                        oParameters.Add("@dUnit", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                    }

                                    if (PaymentPatientEOBLine.IsNullTotalCharges == false)
                                    {
                                        oParameters.Add("@dTotalCharges", PaymentPatientEOBLine.TotalCharges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@dTotalCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    if (PaymentPatientEOBLine.IsNullAllowed == false)
                                    {
                                        oParameters.Add("@dAllowed", PaymentPatientEOBLine.Allowed, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@dAllowed", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    if (PaymentPatientEOBLine.IsNullWriteOff == false)
                                    {
                                        oParameters.Add("@dWriteOff", PaymentPatientEOBLine.WriteOff, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@dWriteOff", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    if (PaymentPatientEOBLine.IsNullNonCovered == false)
                                    {
                                        oParameters.Add("@dNotCovered", PaymentPatientEOBLine.NonCovered, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@dNotCovered", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    if (PaymentPatientEOBLine.IsNullInsurance == false)
                                    {
                                        oParameters.Add("@dPayment", PaymentPatientEOBLine.InsuranceAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@dPayment", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    if (PaymentPatientEOBLine.IsNullCopay == false)
                                    {
                                        oParameters.Add("@dCopay", PaymentPatientEOBLine.Copay, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@dCopay", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    if (PaymentPatientEOBLine.IsNullDeductible == false)
                                    {
                                        oParameters.Add("@dDeductible", PaymentPatientEOBLine.Deductible, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@dDeductible", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    if (PaymentPatientEOBLine.IsNullCoInsurance == false)
                                    {
                                        oParameters.Add("@dCoInsurance", PaymentPatientEOBLine.CoInsurance, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                    }
                                    else
                                    {
                                        oParameters.Add("@dCoInsurance", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                    }

                                    if (PaymentPatientEOBLine.IsNullWithhold == false)
                                    {
                                        oParameters.Add("@dWithhold", PaymentPatientEOBLine.Withhold, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@dWithhold", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    oParameters.Add("@nPaymentTrayID", PaymentPatientEOBLine.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                    oParameters.Add("@sPaymentTrayCode", PaymentPatientEOBLine.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                                    oParameters.Add("@sPaymentTrayDescription", PaymentPatientEOBLine.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked
                                    oParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                    oParameters.Add("@sUserName", UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked
                                    oParameters.Add("@dtCreatedDateTime", PaymentPatientEOBLine.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked
                                    oParameters.Add("@dtModifiedDateTime", PaymentPatientEOBLine.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked
                                    oParameters.Add("@nPatientID", PaymentPatientEOBLine.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                    oParameters.Add("@nInsuraceID", PaymentPatientEOBLine.PatInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                    oParameters.Add("@nContactID", PaymentPatientEOBLine.InsContactID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                    oParameters.Add("@nClinicID", PaymentPatientEOBLine.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBType", PaymentPatientEOBLine.EOBType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);// int,
                                    oParameters.Add("@nBillingTransactionID", PaymentPatientEOBLine.BLTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                    oParameters.Add("@nBillingTransactionDetailID", PaymentPatientEOBLine.BLTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                    oParameters.Add("@nBillingTransactionLineNo", PaymentPatientEOBLine.BLTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
                                    oParameters.Add("@bUseExtEOBID", EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nCloseDate", PaymentPatientEOBLine.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                    oParameters.Add("@nTrackTrnID", PaymentPatientEOBLine.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nTrackTrnDtlID", PaymentPatientEOBLine.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@sSubClaimNo", PaymentPatientEOBLine.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),

                                    oParameters.Add("@bIsVoid", PaymentPatientEOBLine.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                    oParameters.Add("@nVoidCloseDate", PaymentPatientEOBLine.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                    oParameters.Add("@nVoidTrayID", PaymentPatientEOBLine.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),

                                    oParameters.Add("@bIsPaymentVoid", true, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                    oParameters.Add("@nPaymentVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                    oParameters.Add("@nPaymentVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),

                                    oParameters.Add("@nVoidType", PaymentPatientEOBLine.VoidType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                                    oParameters.Add("@nVoidRefEOBID", PaymentPatientEOBLine.mEOBID, ParameterDirection.Input, SqlDbType.BigInt); //numeric(18, 0),            
                                    oParameters.Add("@nVoidRefEOBDtlID", PaymentPatientEOBLine.mEOBDtlID, ParameterDirection.Input, SqlDbType.BigInt); //numeric(18,0),            
                                    oParameters.Add("@nVoidRefEOBPaymentID", PaymentPatientEOBLine.mEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0)

                                    //Added by Subashish_b on 16/May /2011 for  adding 3 more parameter for adding PAF values 
                                    oParameters.Add("@nPAccountID", PaymentPatientEOBLine.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nAccountPatientID", PaymentPatientEOBLine.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nGuarantorID", PaymentPatientEOBLine.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                    //End

                                    _retVal = null;
                                    _valRet = null;

                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                    _sqlCommand.Connection = _sqlConnection;
                                    _sqlCommand.Transaction = _sqlTransaction;
                                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                                    _sqlCommand.CommandText = "BL_INSERT_EOB_PatPayment_Void";

                                    int _result = 0;
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
                                    { EOBId = Convert.ToInt64(_retVal); }

                                    if (_valRet != null && Convert.ToString(_valRet).Trim() != "")
                                    { EOBDtlId = Convert.ToInt64(_valRet); }
                                    if (_sqlCommand != null)
                                    {
                                        _sqlCommand.Parameters.Clear();
                                        _sqlCommand.Dispose();
                                        _sqlCommand = null;
                                    }
                                }
                            }
                        }
                        if (PaymentPatientEOBLines != null)
                        {
                            PaymentPatientEOBLines.Dispose();
                            PaymentPatientEOBLines = null;
                        }
                        #endregion " EOB Data save for voiding patient payment


                        EOBPatientPaymentDtls = GetPaymentForVoid(EOBPaymentID, PatientId, VoidCloseDate, VoidTrayID, VoidTrayCode, VoidTrayName);
                        #region " Payment Line Details save for voiding patient payment "

                        if (EOBPaymentID > 0 && EOBPatientPaymentDtls != null && EOBPatientPaymentDtls.Count > 0)
                        {
                            for (int _payVoidLineIndex = 0; _payVoidLineIndex < EOBPatientPaymentDtls.Count; _payVoidLineIndex++)
                            {
                                if (EOBPatientPaymentDtls[_payVoidLineIndex] != null)
                                {
                                    EOBPatPayDtl = EOBPatientPaymentDtls[_payVoidLineIndex];
                                    oParameters.Clear();
                                    oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBID", EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBDtlID", EOBDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nEOBPaymentDetailID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nBillingTransactionLineNo", EOBPatPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nPatientID", EOBPatPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nDOSFrom", EOBPatPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nDOSTo", EOBPatPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@sCPTCode", EOBPatPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sCPTDescription", EOBPatPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                    if (EOBPatPayDtl.IsNullAmount == false)
                                    {
                                        oParameters.Add("@nAmount", EOBPatPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }
                                    else
                                    {
                                        oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                    }

                                    oParameters.Add("@nPaymentType", EOBPatPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentSubType", EOBPatPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaySign", EOBPatPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPayMode", EOBPatPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nAccountID", EOBPatPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nAccountType", EOBPatPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nMSTAccountID", EOBPatPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nMSTAccountType", EOBPatPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                    oParameters.Add("@nPaymentTrayID", EOBPatPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sPaymentTrayCode", EOBPatPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                    oParameters.Add("@sPaymentTrayDescription", EOBPatPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@sUserName", UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                    oParameters.Add("@dtCreatedDateTime", EOBPatPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@dtModifiedDateTime", EOBPatPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                    oParameters.Add("@nClinicID", EOBPatPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                    if (EOBPatPayDtl.RefEOBPaymentID == 0) { EOBPatPayDtl.RefEOBPaymentID = 0; }
                                    if (EOBPatPayDtl.RefEOBPaymentDetailID == 0) { EOBPatPayDtl.RefEOBPaymentDetailID = 0; }

                                    oParameters.Add("@nRefEOBPaymentID", EOBPatPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nRefEOBPaymentDetailID", EOBPatPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                    oParameters.Add("@nResEOBPaymentID", EOBPatPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nResEOBPaymentDetailID", EOBPatPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nContactInsID", EOBPatPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nCreditLineID", EOBPatPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nEOBVoidPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nCloseDate", EOBPatPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nTrackTrnID", EOBPatPayDtl.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@nTrackTrnDtlID", EOBPatPayDtl.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                    oParameters.Add("@sSubClaimNo", EOBPatPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),

                                    oParameters.Add("@bIsVoid", EOBPatPayDtl.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                    oParameters.Add("@nVoidCloseDate", EOBPatPayDtl.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                    oParameters.Add("@nVoidTrayID", EOBPatPayDtl.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                    oParameters.Add("@nVoidType", EOBPatPayDtl.VoidType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                                    oParameters.Add("@bIsPaymentVoid", true, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                    oParameters.Add("@nPaymentVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                    oParameters.Add("@nPaymentVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),

                                    oParameters.Add("@nOldResEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nOldResEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                    //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                                    oParameters.Add("@nPAccountID", EOBPatPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nAccountPatientID", EOBPatPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oParameters.Add("@nGuarantorID", EOBPatPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                    //End
                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                    _sqlCommand.Connection = _sqlConnection;
                                    _sqlCommand.Transaction = _sqlTransaction;
                                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                                    _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL_PatPayment";

                                    int _result = _sqlCommand.ExecuteNonQuery();

                                    if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                    { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                    else
                                    { _retVal = 0; }

                                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                    { _EOBPayDtlId = Convert.ToInt64(_retVal); }
                                    if (_sqlCommand != null)
                                    {
                                        _sqlCommand.Parameters.Clear();
                                        _sqlCommand.Dispose();
                                        _sqlCommand = null;
                                    }
                                    EOBPatPayDtl = null;
                                }
                            }
                        }
                        if (EOBPatientPaymentDtls != null)
                        {
                            EOBPatientPaymentDtls.Dispose();
                            EOBPatientPaymentDtls = null;
                        }
                        #endregion " Payment Line Details save for voiding patient payment "

                        if (_EOBPayId > 0)
                        {

                            _sqlQuery = " UPDATE BL_EOBPayment_MST WITH(READPAST) SET bIsPaymentVoid = 'true', nPaymentVoidCloseDate = " + VoidCloseDate + ", nPaymentVoidTrayID = " + VoidTrayID + ", nVoidType = " + VoidType.PatientPaymentVoid.GetHashCode() + " WHERE ( nEOBPaymentID = " + EOBPaymentID + " ) AND ISNULL(nVoidType,0) NOT IN (" + VoidType.PatientPaymentVoidEntry.GetHashCode() + "," + VoidType.PatientPaymentRefundVoid.GetHashCode() + "," + VoidType.PatientPaymentRefundVoidEntry.GetHashCode() + ")  ";// OR nResEOBPaymentID = " + EOBPaymentID + " OR nRefEOBPaymentID =  "+ EOBPaymentID + " ";
                            _sqlCommand = new System.Data.SqlClient.SqlCommand();
                            _sqlCommand.Connection = _sqlConnection;
                            _sqlCommand.Transaction = _sqlTransaction;
                            _sqlCommand.CommandType = CommandType.Text;
                            _sqlCommand.CommandText = _sqlQuery;
                            _sqlCommand.ExecuteNonQuery();
                            if (_sqlCommand != null)
                            {
                                _sqlCommand.Parameters.Clear();
                                _sqlCommand.Dispose();
                                _sqlCommand = null;
                            }
                            _sqlQuery = "";
                            _sqlQuery = " UPDATE BL_EOBPayment_EOB WITH(READPAST) SET bIsPaymentVoid = 'true', nPaymentVoidCloseDate = " + VoidCloseDate + ", nPaymentVoidTrayID = " + VoidTrayID + ", nVoidType = " + VoidType.PatientPaymentVoid.GetHashCode() + " WHERE ( nEOBPaymentID = " + EOBPaymentID + " ) AND ISNULL(nVoidType,0) NOT IN (" + VoidType.PatientPaymentVoidEntry.GetHashCode() + "," + VoidType.PatientPaymentRefundVoid.GetHashCode() + "," + VoidType.PatientPaymentRefundVoidEntry.GetHashCode() + ")";//" OR nResEOBPaymentID = " + EOBPaymentID + " OR nRefEOBPaymentID =  " + EOBPaymentID + " ";
                            _sqlCommand = new System.Data.SqlClient.SqlCommand();
                            _sqlCommand.Connection = _sqlConnection;
                            _sqlCommand.Transaction = _sqlTransaction;
                            _sqlCommand.CommandType = CommandType.Text;
                            _sqlCommand.CommandText = _sqlQuery;
                            _sqlCommand.ExecuteNonQuery();
                            if (_sqlCommand != null)
                            {
                                _sqlCommand.Parameters.Clear();
                                _sqlCommand.Dispose();
                                _sqlCommand = null;
                            }
                            _sqlQuery = "";
                            _sqlQuery = " UPDATE BL_EOBPayment_DTL WITH(READPAST) SET bIsPaymentVoid = 'true', nPaymentVoidCloseDate = " + VoidCloseDate + ", nPaymentVoidTrayID = " + VoidTrayID + ", nVoidType = " + VoidType.PatientPaymentVoid.GetHashCode() + " WHERE ( nEOBPaymentID = " + EOBPaymentID + " OR nResEOBPaymentID = " + EOBPaymentID + " OR nRefEOBPaymentID =  " + EOBPaymentID + " ) AND ISNULL(nVoidType,0) NOT IN (" + VoidType.PatientPaymentVoidEntry.GetHashCode() + "," + VoidType.PatientPaymentRefundVoid.GetHashCode() + "," + VoidType.PatientPaymentRefundVoidEntry.GetHashCode() + ") AND (nPaymentType <> " + EOBPaymentType.PatientPayment.GetHashCode() + " OR nPaymentSubType <> " + EOBPaymentSubType.Adjuestment.GetHashCode() + ") ";
                            _sqlCommand = new System.Data.SqlClient.SqlCommand();
                            _sqlCommand.Connection = _sqlConnection;
                            _sqlCommand.Transaction = _sqlTransaction;
                            _sqlCommand.CommandType = CommandType.Text;
                            _sqlCommand.CommandText = _sqlQuery;
                            _sqlCommand.ExecuteNonQuery();
                            if (_sqlCommand != null)
                            {
                                _sqlCommand.Parameters.Clear();
                                _sqlCommand.Dispose();
                                _sqlCommand = null;
                            }
                        }
                        _sqlTransaction.Commit();
                        _sqlConnection.Close();

                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                { _sqlTransaction.Rollback(); ex.ERROR_Log(ex.ToString()); }
                catch (Exception ex)
                { _sqlTransaction.Rollback(); gloAuditTrail.gloAuditTrail.ExceptionLog("ERROR: " + ex.Message, true); }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (_retVal != null) { _retVal = null; }
                    if (_sqlConnection != null) { _sqlConnection.Dispose(); _sqlConnection = null; }
                    if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); _sqlCommand = null; }
                    if (_sqlTransaction != null) { _sqlTransaction.Dispose(); _sqlTransaction = null; }
                 //   if (EOBPatPayDtl != null) { EOBPatPayDtl.Dispose(); };
                    if (EOBPatientPaymentDtls != null) { EOBPatientPaymentDtls.Dispose(); EOBPatientPaymentDtls = null; }
                    if (PaymentPatientMST != null) { PaymentPatientMST.Dispose(); PaymentPatientMST = null; }
                }
                return 0;
            }

            public EOBPayment.Common.EOBPatientPaymentDetails GetPaymentForVoid(Int64 EOBPaymentID, Int64 PatientId, int VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtEOBPatientPayment = null;
                EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentDetail = null;
                EOBPayment.Common.EOBPatientPaymentDetails oEOBPatientPaymentDetails = null;
                EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;
                int _PaymentMode = 0;
                EOBPaymentSign _EOBPaymentSign = EOBPaymentSign.None;
                int _PaymentSign = 0;
                EOBPaymentType _EOBPaymentType = EOBPaymentType.None;
                int _PaymentType = 0;
                EOBPaymentSubType _EOBPaymentSubType = EOBPaymentSubType.None;
                int _PaymentSubType = 0;
                Int64 _voidTrayID = 0;
                int _voidCloseDate = 0;
                try
                {
                    if (EOBPaymentID > 0)
                    {
                        _voidTrayID = VoidTrayID;
                        _voidCloseDate = VoidCloseDate;
                        #region "Retrieve Patient Payment Details For Void"

                        oParameters.Clear();
                        oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Connect(false);
                        oDB.Retrive("BL_SELECT_PatientPaymentDetailsForVoid", oParameters, out dtEOBPatientPayment);
                        oDB.Disconnect();
                        oParameters.Clear();

                        #endregion "Retrieve Patient Payment Details For Void"

                        #region " Set Payment Detail Data "

                        //nEOBPaymentID,nEOBID,nEOBDtlID,nEOBPaymentDetailID,nBillingTransactionID,nBillingTransactionDetailID,nBillingTransactionLineNo,
                        //nPatientID,nDOSFrom,nDOSTo,sCPTCode,sCPTDescription,nAmount,nPaymentType,nPaymentSubType,nPaySign,nPayMode,nRefEOBPaymentID,
                        //nRefEOBPaymentDetailID,nAccountID,nAccountType,nMSTAccountID,nMSTAccountType,nPaymentTrayID,sPaymentTrayCode,sPaymentTrayDescription,nUserID,
                        //sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID,nResEOBPaymentID,nResEOBPaymentDetailID,nCreditLineID,nContactInsID,
                        //dtDayClosedOn,nDayCloseUserID,sDayCloseUserName,bIsUpdated,bIsDayClosed,bIsVoid,nEOBVoidPaymentID,nCloseDate,nVoidCloseDate,
                        //nVoidTrayID,nTrackTrnID,nTrackTrnDtlID,nTrackTrnLineNo,sSubClaimNo,nOldRefEOBPaymentID,nOldRefEOBPaymentDetailID,nOldResEOBPaymentID,
                        //nOldResEOBPaymentDetailID,sVersion,nVoidType

                        if (dtEOBPatientPayment != null && dtEOBPatientPayment.Rows.Count > 0)
                        {
                            oEOBPatientPaymentDetails = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetails();
                            for (int _nPayDtlCounter = 0; _nPayDtlCounter < dtEOBPatientPayment.Rows.Count; _nPayDtlCounter++)
                            {
                                oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();

                                #region "Get the debit allocation entry for voiding payment"
                                _PaymentSign = Convert.ToInt32(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nPaySign"].ToString());
                                _PaymentType = Convert.ToInt32(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nPaymentType"].ToString());
                                _PaymentSubType = Convert.ToInt32(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nPaymentSubType"].ToString());
                                if (_PaymentSign == 1) { _EOBPaymentSign = EOBPaymentSign.Payment_Credit; }
                                else if (_PaymentSign == 2) { _EOBPaymentSign = EOBPaymentSign.Receipt_Debit; }
                                if (_PaymentType == 2) { _EOBPaymentType = EOBPaymentType.PatientReserved; }
                                else if (_PaymentType == 6) { _EOBPaymentType = EOBPaymentType.PatientPayment; }
                                if (_PaymentSubType == 13) { _EOBPaymentSubType = EOBPaymentSubType.Correction; }
                                else if (_PaymentSubType == 8) { _EOBPaymentSubType = EOBPaymentSubType.Patient; }


                                if (_EOBPaymentSign == EOBPaymentSign.Receipt_Debit || _EOBPaymentSubType == EOBPaymentSubType.Correction)
                                {
                                    _PaymentMode = Convert.ToInt32(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nPayMode"].ToString());
                                    if (_PaymentMode == 0)
                                    { _EOBPaymentMode = EOBPaymentMode.None; }
                                    else if (_PaymentMode == 1)
                                    { _EOBPaymentMode = EOBPaymentMode.Cash; }
                                    else if (_PaymentMode == 2)
                                    { _EOBPaymentMode = EOBPaymentMode.Check; }
                                    else if (_PaymentMode == 3)
                                    { _EOBPaymentMode = EOBPaymentMode.MoneyOrder; }
                                    else if (_PaymentMode == 4)
                                    { _EOBPaymentMode = EOBPaymentMode.CreditCard; }
                                    else if (_PaymentMode == 5)
                                    { _EOBPaymentMode = EOBPaymentMode.EFT; }


                                    oEOBPatientPaymentDetail.EOBPaymentID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nEOBPaymentID"].ToString());
                                    oEOBPatientPaymentDetail.EOBID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nEOBID"].ToString());
                                    oEOBPatientPaymentDetail.EOBDtlID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nEOBDtlID"].ToString());
                                    oEOBPatientPaymentDetail.EOBPaymentDetailID = GetPrefixTransactionID();

                                    oEOBPatientPaymentDetail.BillingTransactionID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nBillingTransactionID"].ToString());
                                    oEOBPatientPaymentDetail.BillingTransactionDetailID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nBillingTransactionDetailID"].ToString());
                                    oEOBPatientPaymentDetail.BillingTransactionLineNo = Convert.ToInt32(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nBillingTransactionLineNo"].ToString());
                                    oEOBPatientPaymentDetail.DOSFrom = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nDOSFrom"].ToString());
                                    oEOBPatientPaymentDetail.DOSTo = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nDOSTo"].ToString());
                                    oEOBPatientPaymentDetail.CPTCode = Convert.ToString(dtEOBPatientPayment.Rows[_nPayDtlCounter]["sCPTCode"].ToString());
                                    oEOBPatientPaymentDetail.CPTDescription = Convert.ToString(dtEOBPatientPayment.Rows[_nPayDtlCounter]["sCPTDescription"].ToString());

                                    oEOBPatientPaymentDetail.IsNullAmount = false;
                                    //solving sales force case - GLO2011-0011127   
                                    decimal _fillPayAmt = 0;
                                    if (dtEOBPatientPayment.Rows[_nPayDtlCounter]["nAmount"] != DBNull.Value && dtEOBPatientPayment.Rows[_nPayDtlCounter]["nAmount"].ToString() != "")
                                    {
                                        _fillPayAmt = Convert.ToDecimal(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nAmount"].ToString());
                                    }
                                    else
                                    {
                                        _fillPayAmt = 0;
                                        oEOBPatientPaymentDetail.IsNullAmount = true; 
                                    }
                                    // end
                                    if (_EOBPaymentType == EOBPaymentType.PatientPayment || (_EOBPaymentType == EOBPaymentType.PatientPayment && _EOBPaymentSubType == EOBPaymentSubType.Correction) && _EOBPaymentSign == EOBPaymentSign.Receipt_Debit)
                                    {
                                        oEOBPatientPaymentDetail.Amount = (_fillPayAmt * -1);
                                        oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;
                                    }
                                    else
                                    {
                                        oEOBPatientPaymentDetail.Amount = _fillPayAmt;
                                        oEOBPatientPaymentDetail.PayMode = EOBPaymentMode.PaymentVoidReserved;
                                    }
                                    oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientPayment;
                                    oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Patient;
                                    oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;

                                    oEOBPatientPaymentDetail.RefEOBPaymentID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nRefEOBPaymentID"].ToString());
                                    oEOBPatientPaymentDetail.RefEOBPaymentDetailID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nRefEOBPaymentDetailID"].ToString());
                                    if (_EOBPaymentType != EOBPaymentType.PatientReserved)
                                    {
                                        oEOBPatientPaymentDetail.ReserveEOBPaymentID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nResEOBPaymentID"].ToString());
                                        oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nResEOBPaymentDetailID"].ToString());
                                    }
                                    else
                                    {
                                        oEOBPatientPaymentDetail.ReserveEOBPaymentID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nEOBPaymentID"].ToString());
                                        oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nEOBPaymentDetailID"].ToString());
                                    }

                                    //2010626 Sagar Ghodke
                                    if (_EOBPaymentType == EOBPaymentType.PatientPayment && _EOBPaymentSubType == EOBPaymentSubType.Patient && _EOBPaymentSign == EOBPaymentSign.Receipt_Debit
                                        && Convert.ToString(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nOldResEOBPaymentID"]).Trim() != ""
                                        && Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nOldResEOBPaymentID"]) > 0
                                        && Convert.ToString(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nOldResEOBPaymentDetailID"]).Trim() != ""
                                        && Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nOldResEOBPaymentDetailID"]) > 0)
                                    {
                                        oEOBPatientPaymentDetail.ReserveEOBPaymentID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nOldResEOBPaymentID"].ToString());
                                        oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nOldResEOBPaymentDetailID"].ToString());
                                    }
                                    //2010626 Sagar Ghodke

                                    oEOBPatientPaymentDetail.AccountID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nAccountID"].ToString());
                                    oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Patient;
                                    oEOBPatientPaymentDetail.MSTAccountID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nMSTAccountID"].ToString());
                                    oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                                    oEOBPatientPaymentDetail.PatientID = PatientId;
                                    oEOBPatientPaymentDetail.PaymentTrayID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nPaymentTrayID"].ToString());
                                    oEOBPatientPaymentDetail.PaymentTrayCode = Convert.ToString(dtEOBPatientPayment.Rows[_nPayDtlCounter]["sPaymentTrayCode"].ToString());
                                    oEOBPatientPaymentDetail.PaymentTrayDescription = Convert.ToString(dtEOBPatientPayment.Rows[_nPayDtlCounter]["sPaymentTrayDescription"].ToString());
                                    oEOBPatientPaymentDetail.UserID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nUserID"].ToString());
                                    oEOBPatientPaymentDetail.UserName = Convert.ToString(dtEOBPatientPayment.Rows[_nPayDtlCounter]["sUserName"].ToString());
                                    oEOBPatientPaymentDetail.ClinicID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nClinicID"].ToString());
                                    oEOBPatientPaymentDetail.CloseDate = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nCloseDate"].ToString());
                                    oEOBPatientPaymentDetail.FinanceLieNo = 0;
                                    oEOBPatientPaymentDetail.MainCreditLineID = 0;
                                    oEOBPatientPaymentDetail.IsMainCreditLine = false;
                                    oEOBPatientPaymentDetail.IsReserveCreditLine = false;
                                    oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
                                    oEOBPatientPaymentDetail.RefFinanceLieNo = 1;
                                    oEOBPatientPaymentDetail.UseRefFinanceLieNo = false;
                                    oEOBPatientPaymentDetail.TrackTrnID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nTrackTrnID"].ToString());
                                    oEOBPatientPaymentDetail.TrackTrnDtlID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nTrackTrnDtlID"].ToString());
                                    oEOBPatientPaymentDetail.SubClaimNo = "";


                                    oEOBPatientPaymentDetail.VoidCloseDate = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nVoidCloseDate"].ToString());
                                    oEOBPatientPaymentDetail.VoidTrayID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nVoidTrayID"].ToString());
                                    oEOBPatientPaymentDetail.IsVoid = Convert.ToBoolean(dtEOBPatientPayment.Rows[_nPayDtlCounter]["bIsVoid"].ToString());
                                   
                                    //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  adding 3 more parameter for adding PAF values 
                                    oEOBPatientPaymentDetail.PAccountID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nPAccountID"].ToString());
                                    oEOBPatientPaymentDetail.AccountPatientID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nAccountPatientID"].ToString());
                                    oEOBPatientPaymentDetail.GuarantorID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nGuarantorID"].ToString());

                                    //End

                                    oEOBPatientPaymentDetail.VoidType = VoidType.PatientPaymentVoidEntry;
                                    oEOBPatientPaymentDetails.Add(oEOBPatientPaymentDetail);
                                }
                                #endregion "Get the debit allocation entry for voiding payment"
                            }
                        }
                        #endregion " Set Payment Detail Data "
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                { ex.ERROR_Log(ex.ToString()); throw; }
                catch //(Exception ex)
                { throw; }
                finally
                {
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                 //   if (oEOBPatientPaymentDetail != null) { oEOBPatientPaymentDetail.Dispose(); oEOBPatientPaymentDetail = null; }
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (dtEOBPatientPayment != null) { dtEOBPatientPayment.Dispose(); dtEOBPatientPayment = null; }
                }

                return oEOBPatientPaymentDetails;
            }

            public DataTable GetExistingPaymentDetails(Int64 PatientID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable _dtPendingChecks = null;
                try
                {
                    oParameters.Add("@nPayerID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Connect(false);
                    oDB.Retrive("BL_SELECT_ExistingPatientPayment", oParameters, out _dtPendingChecks);
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
                    //if (_dtPendingChecks != null) { _dtPendingChecks.Dispose(); }
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                }

                return _dtPendingChecks;
            }

            public EOBPayment.Common.PaymentPatient GetMasterDetailsForPatientPaymentVoid(Int64 EOBPaymentID, Int64 ClinicID, Int64 VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtEOBPaymentMST = null;
                EOBPayment.Common.PaymentPatient oPatientPayment = null;
                int _PaymentMode = 0;
                EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;
                try
                {
                    if (EOBPaymentID > 0)
                    {
                        #region "Retrieve Patient Payment Master Details For Void"

                        oParameters.Clear();
                        oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Connect(false);
                        oDB.Retrive("BL_SELECT_PatientPaymentMasterDetailsForVoid", oParameters, out dtEOBPaymentMST);
                        oDB.Disconnect();
                        oParameters.Clear();

                        #endregion "Retrieve Patient Payment Master Details For Void"

                        if (dtEOBPaymentMST != null && dtEOBPaymentMST.Rows.Count > 0)
                        {
                            oPatientPayment = new global::gloBilling.EOBPayment.Common.PaymentPatient();
                            _PaymentMode = Convert.ToInt32(dtEOBPaymentMST.Rows[0]["nPaymentMode"].ToString());
                            if (_PaymentMode == 0)
                            { _EOBPaymentMode = EOBPaymentMode.None; }
                            else if (_PaymentMode == 1)
                            { _EOBPaymentMode = EOBPaymentMode.Cash; }
                            else if (_PaymentMode == 2)
                            { _EOBPaymentMode = EOBPaymentMode.Check; }
                            else if (_PaymentMode == 3)
                            { _EOBPaymentMode = EOBPaymentMode.MoneyOrder; }
                            else if (_PaymentMode == 4)
                            { _EOBPaymentMode = EOBPaymentMode.CreditCard; }
                            else if (_PaymentMode == 5)
                            { _EOBPaymentMode = EOBPaymentMode.EFT; }

                            oPatientPayment.PaymentNumber = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentNo"].ToString());
                            oPatientPayment.PaymentNumberPefix = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentNoPrefix"].ToString());
                            oPatientPayment.EOBPaymentID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nEOBPaymentID"].ToString());
                            oPatientPayment.EOBRefNO = Convert.ToString(dtEOBPaymentMST.Rows[0]["nEOBRefNO"].ToString());
                            oPatientPayment.PayerName = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPayerName"].ToString());
                            oPatientPayment.PayerID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nPayerID"].ToString());
                            oPatientPayment.PayerType = EOBPaymentAccountType.Patient;
                            oPatientPayment.PaymentMode = _EOBPaymentMode;
                            oPatientPayment.CheckNumber = Convert.ToString(dtEOBPaymentMST.Rows[0]["sCheckNumber"].ToString());
                            oPatientPayment.CheckAmount = Convert.ToDecimal(dtEOBPaymentMST.Rows[0]["nCheckAmount"].ToString()) * -1;
                            oPatientPayment.CheckDate = Convert.ToInt32(dtEOBPaymentMST.Rows[0]["nCheckDate"].ToString());
                            oPatientPayment.CardType = Convert.ToString(dtEOBPaymentMST.Rows[0]["sCardType"].ToString());
                            oPatientPayment.AuthorizationNo = Convert.ToString(dtEOBPaymentMST.Rows[0]["sAuthorizationNo"].ToString());
                            oPatientPayment.CardExpiryDate = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nCardExpDate"].ToString());
                            oPatientPayment.CardID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nCardID"].ToString());
                            oPatientPayment.MSTAccountID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nMSTAccountID"].ToString());
                            oPatientPayment.MSTAccountType = EOBPaymentAccountType.Patient;
                            oPatientPayment.ClinicID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nClinicID"].ToString());
                            oPatientPayment.CreatedDateTime = DateTime.Now;
                            oPatientPayment.ModifiedDateTime = DateTime.Now;
                            oPatientPayment.PaymentTrayID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nPaymentTrayID"].ToString()); ;
                            oPatientPayment.PaymentTrayCode = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentTrayCode"].ToString()); ;
                            oPatientPayment.PaymentTrayDesc = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentTrayDescription"].ToString()); ;
                            oPatientPayment.CloseDate = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nCloseDate"].ToString());
                            oPatientPayment.UserID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nUserID"].ToString());
                            oPatientPayment.UserName = Convert.ToString(dtEOBPaymentMST.Rows[0]["sUserName"].ToString());

                            oPatientPayment.IsVoid = Convert.ToBoolean(dtEOBPaymentMST.Rows[0]["bIsVoid"].ToString());
                            oPatientPayment.VoidCloseDate = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nVoidCloseDate"].ToString()); ;
                            oPatientPayment.VoidRefPaymentID = EOBPaymentID;
                            oPatientPayment.VoidTrayID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nVoidTrayID"].ToString()); ;
                            oPatientPayment.VoidType = VoidType.PatientPaymentVoidEntry;
                            oPatientPayment.PAccountID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nPAccountID"].ToString()); ;
                            oPatientPayment.GuarantorID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nGuarantorID"].ToString()); ;
                            oPatientPayment.AccountPatientID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nAccountPatientID"].ToString()); ;


                        }
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                { ex.ERROR_Log(ex.ToString()); throw; }
                catch //(Exception ex)
                { throw; }
                finally
                {
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (dtEOBPaymentMST != null) { dtEOBPaymentMST.Dispose(); dtEOBPaymentMST = null; }
                }

                return oPatientPayment;

            }

            public EOBPayment.Common.PaymentPatientLines GetEOBLinesDetailForPatientPaymentVoid(Int64 EOBPaymentID, Int64 PatientId, Int64 VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dtEOBPaymentEOB = null;
                EOBPayment.Common.PaymentPatientLines PaymentPatientEOBLinesDtls = new global::gloBilling.EOBPayment.Common.PaymentPatientLines();
                EOBPayment.Common.PaymentPatientLine oPaymentPatientLine = null;

                try
                {
                    if (EOBPaymentID > 0)
                    {
                        #region "Retrieve Patient Payment Details EOB For Void"
                        oParameters.Clear();
                        oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Connect(false);
                        oDB.Retrive("BL_SELECT_PatientEOBDetailsForVoid", oParameters, out dtEOBPaymentEOB);
                        oDB.Disconnect();
                        oParameters.Clear();
                        #endregion "Retrieve Patient Payment EOB Details For Void"

                        #region " Set EOB Data"
                        if (dtEOBPaymentEOB != null && dtEOBPaymentEOB.Rows.Count > 0)
                        {
                            for (int _nEOBDtlCounter = 0; _nEOBDtlCounter < dtEOBPaymentEOB.Rows.Count; _nEOBDtlCounter++)
                            {
                                oPaymentPatientLine = new global::gloBilling.EOBPayment.Common.PaymentPatientLine();
                                oPaymentPatientLine.mEOBPaymentID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nEOBPaymentID"].ToString());
                                oPaymentPatientLine.mEOBID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nEOBID"].ToString());
                                oPaymentPatientLine.mEOBDtlID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nEOBDtlID"].ToString());
                                oPaymentPatientLine.PatientID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nPatientID"].ToString());
                                oPaymentPatientLine.PatInsuranceID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nInsuraceID"].ToString());
                                oPaymentPatientLine.InsContactID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nContactID"].ToString());
                                oPaymentPatientLine.BLTransactionID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nBillingTransactionID"].ToString());
                                oPaymentPatientLine.BLTransactionDetailID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nBillingTransactionDetailID"].ToString());
                                oPaymentPatientLine.BLTransactionLineNo = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nBillingTransactionLineNo"].ToString());
                                oPaymentPatientLine.ClaimNumber = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nClaimNo"].ToString());

                                oPaymentPatientLine.DOSFrom = Convert.ToInt32(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nDOSFrom"].ToString());
                                oPaymentPatientLine.DOSTo = Convert.ToInt32(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nDOSTo"].ToString());

                                oPaymentPatientLine.CPTCode = Convert.ToString(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["sCPTCode"].ToString());
                                oPaymentPatientLine.CPTDescription = Convert.ToString(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["sCPTDescription"].ToString());

                                oPaymentPatientLine.BLInsuranceID = 0;
                                oPaymentPatientLine.BLInsuranceName = "";
                                oPaymentPatientLine.BLInsuranceFlag = InsuranceTypeFlag.None;
                                if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCharges"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCharges"].ToString() != "")
                                {
                                    oPaymentPatientLine.Charges = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCharges"].ToString());
                                    oPaymentPatientLine.IsNullCharges = false;
                                }

                                if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dUnit"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dUnit"].ToString() != "")
                                {
                                    oPaymentPatientLine.Unit = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dUnit"].ToString());
                                    oPaymentPatientLine.IsNullUnit = false;
                                }
                                if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dTotalCharges"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dTotalCharges"].ToString() != "")
                                {
                                    oPaymentPatientLine.TotalCharges = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dTotalCharges"].ToString());
                                    oPaymentPatientLine.IsNullTotalCharges = false;
                                }
                                if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dAllowed"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dAllowed"].ToString() != "")
                                {
                                    oPaymentPatientLine.Allowed = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dAllowed"].ToString());
                                    oPaymentPatientLine.IsNullAllowed = false;
                                }

                                if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dWriteOff"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dWriteOff"].ToString() != "")
                                {
                                    oPaymentPatientLine.WriteOff = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dWriteOff"].ToString());
                                    oPaymentPatientLine.IsNullWriteOff = false;
                                }

                                if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dNotCovered"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dNotCovered"].ToString() != "")
                                {
                                    oPaymentPatientLine.NonCovered = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dNotCovered"].ToString());
                                    oPaymentPatientLine.IsNullNonCovered = false;
                                }

                                if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dPayment"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dPayment"].ToString() != "")
                                {
                                    oPaymentPatientLine.InsuranceAmount = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dPayment"].ToString());
                                    oPaymentPatientLine.IsNullInsurance = false;
                                }

                                if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCopay"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCopay"].ToString() != "")
                                {
                                    oPaymentPatientLine.Copay = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCopay"].ToString());
                                    oPaymentPatientLine.IsNullCopay = false;
                                }

                                if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dDeductible"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dDeductible"].ToString() != "")
                                {
                                    oPaymentPatientLine.Deductible = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dDeductible"].ToString());
                                    oPaymentPatientLine.IsNullDeductible = false;
                                }

                                if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCoInsurance"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCoInsurance"].ToString() != "")
                                {
                                    oPaymentPatientLine.CoInsurance = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCoInsurance"].ToString());
                                    oPaymentPatientLine.IsNullInsurance = false;
                                }

                                if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dWithhold"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dWithhold"].ToString() != "")
                                {
                                    oPaymentPatientLine.Withhold = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dWithhold"].ToString());
                                    oPaymentPatientLine.IsNullWithhold = false;
                                }

                                oPaymentPatientLine.PaymentTrayID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nPaymentTrayID"].ToString());
                                oPaymentPatientLine.PaymentTrayCode = Convert.ToString(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["sPaymentTrayCode"].ToString());
                                oPaymentPatientLine.PaymentTrayDesc = Convert.ToString(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["sPaymentTrayDescription"].ToString());

                                oPaymentPatientLine.TrackTrnID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nTrackTrnID"].ToString());
                                oPaymentPatientLine.TrackTrnDtlID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nTrackTrnDtlID"].ToString());
                                oPaymentPatientLine.SubClaimNo = Convert.ToString(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["sSubClaimNo"].ToString());
                                //oPaymentPatientLine.InsContactID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nInsuranceCompanyID"].ToString());

                                oPaymentPatientLine.UserID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nUserID"].ToString());
                                oPaymentPatientLine.UserName = Convert.ToString(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["sUserName"].ToString());
                                oPaymentPatientLine.ClinicID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nClinicID"].ToString());

                                oPaymentPatientLine.CloseDate = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nCloseDate"].ToString());

                                oPaymentPatientLine.EOBType = EOBPaymentType.PatientPayment;
                                oPaymentPatientLine.IsVoid = Convert.ToBoolean(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["bIsVoid"].ToString());
                                oPaymentPatientLine.VoidType = VoidType.PatientPaymentVoidEntry;
                                oPaymentPatientLine.VoidTrayID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nVoidTrayID"].ToString());
                                oPaymentPatientLine.VoidCloseDate = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nVoidCloseDate"].ToString());
                             

                                //Added by Subashish_b on 16/May/2011 for  add 3 more parameter to save PAF values  while saving
                                oPaymentPatientLine.PAccountID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nPAccountID"].ToString());
                                oPaymentPatientLine.AccountPatientID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nAccountPatientID"].ToString());
                                oPaymentPatientLine.GuarantorID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nGuarantorID"].ToString());
                                //End
                                PaymentPatientEOBLinesDtls.Add(oPaymentPatientLine);
                            }
                        }
                        #endregion " Set EOB Data"
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                { ex.ERROR_Log(ex.ToString()); throw; }
                catch //(Exception ex)
                { throw; }
                finally
                {
                    if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (dtEOBPaymentEOB != null) { dtEOBPaymentEOB.Dispose(); dtEOBPaymentEOB = null; }
                    //if (oPaymentPatientLine != null) { oPaymentPatientLine.Dispose(); oPaymentPatientLine = null; }
                }
                return PaymentPatientEOBLinesDtls;
            }

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
                        _sqlQuery = "SELECT COUNT(*) FROM BL_EOBPayment_MST WITH (NOLOCK)" +
                        " WHERE UPPER(sCheckNumber) = '" + CheckNumber.Trim().ToUpper().Replace("'", "''") + "' " +
                        " AND nCheckDate = " + CheckDate + " AND nCheckAmount = " + CheckAmt + " " +
                        " AND sCheckNumber IS NOT NULL AND nCheckDate IS NOT NULL AND nCheckAmount IS NOT NULL AND ISNULL(bIsVoid,0) = 0 " +
                        " AND ISNULL(nVoidType,0) = 0 AND nPayerType = " + EOBPaymentAccountType.Patient.GetHashCode() + " ";
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

            public bool IsRefunded(Int64 EOBPaymentID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                Object _retVal = false;
                string _sqlQuery = "";
                bool _IsRefunded = false;

                try
                {
                    if (EOBPaymentID > 0)
                    {
                        oDB.Connect(false);
                        _sqlQuery = "SELECT nEOBPaymentID FROM BL_EOBPayment_DTL WITH(NOLOCK)" +
                        " WHERE nPaymentType = 7 " +
                        " AND nPaymentSubType = 14 " +
                        " AND nPaySign = 2 " +
                        " AND nRefEOBPaymentID = " + EOBPaymentID +
                        " AND ISNULL(bIsPaymentVoid,0) <> 1";
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
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (_retVal != null) { _retVal = null; }
                }

                return _IsRefunded;
            }

            public static DataTable GetPatientPaymentRefundLog(Int64 nPatientID, Int64 nClinicID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
                DataTable _dtPaymentLog = null;

                try
                {
                    string _sqlQuery = "select CloseDate,Tray,Company,CheckNumber,PaymentDate,Amount,sNoteDescription,[User Name], "
                                     + " nEOBPaymentID,Status,RefundDateTime,nRefundId,nPayerID,Claim,Patient,Amount,nClinicID,nPatientID "
                                     + " FROM view_PatientInsurancePaymentReFunds where nClinicID = " + AppSettings.ClinicID;

                    if (nPatientID != 0)
                    { _sqlQuery += " AND nPatientID = " + nPatientID; }


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
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                }
                return _dtPaymentLog;
            }

            #endregion " Private & Public Methods "

            #region " Get Replication ID "

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
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;
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

        }


        namespace Common
        {
            public class PaymentPatient
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
                public Int64 CardID = 0;
                public string CardType = "";
                public string AuthorizationNo = "";
                public Int64 CardExpiryDate = 0;
                public Int64 MSTAccountID = 0;
                public EOBPaymentAccountType MSTAccountType = EOBPaymentAccountType.None;
                public Int64 PaymentTrayID = 0;
                public string PaymentTrayCode = "";
                public string PaymentTrayDesc = "";
                public Int64 CloseDate = 0;
                public string CardSecurityNo = "";
                public Int64 UserID = 0;
                public string UserName = "";
                public DateTime CreatedDateTime = DateTime.Now;
                public DateTime ModifiedDateTime = DateTime.Now;
                public Int64 ClinicID = 0;
                public Int64 VoidCloseDate = 0;
                public Int64 VoidTrayID = 0;
                public bool IsVoid = false;
                public VoidType VoidType = VoidType.None;
                public Int64 VoidRefPaymentID = 0;


                private PaymentPatientClaims _PaymentPatientClaims = null;
                private EOBPatientPaymentDetails _EOBPatientPaymentLineDetails = null;
                private EOBPatientPaymentDetails _EOBPatientPaymentReserveLineDetail = null;
                private PaymentPatientLineNotes _Notes = null;

                private bool paymentAssigned = true;
                private bool lineAssigned = true;
                private bool reserveAssigned = true;
                private bool notesAssigned = true;

                //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for declaring Property Variable 
                private Int64 _nPAccountID = 0;
                private Int64 _nAccountPatientID = 0;
                private Int64 _nGuarantorID = 0;
                //End

                #endregion

                #region "Constructor & Destructor"


                public PaymentPatient()
                {
                    _PaymentPatientClaims = new PaymentPatientClaims();
                    _EOBPatientPaymentLineDetails = new EOBPatientPaymentDetails();
                    _EOBPatientPaymentReserveLineDetail = new EOBPatientPaymentDetails();
                    _Notes = new PaymentPatientLineNotes();
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
                                if (_PaymentPatientClaims != null)
                                {
                                    _PaymentPatientClaims.Dispose();
                                    _PaymentPatientClaims = null;
                                }
                            }
                            if (lineAssigned)
                            {
                                if (_EOBPatientPaymentLineDetails != null)
                                {
                                    _EOBPatientPaymentLineDetails.Dispose();
                                    _EOBPatientPaymentLineDetails = null;
                                }
                            }
                            if (reserveAssigned)
                            {
                                if (_EOBPatientPaymentReserveLineDetail != null)
                                {
                                    _EOBPatientPaymentReserveLineDetail.Dispose();
                                    _EOBPatientPaymentReserveLineDetail = null;
                                }
                                
                            }
                            if (notesAssigned)
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

                ~PaymentPatient()
                {
                    Dispose(false);
                }

                #endregion

                public PaymentPatientClaims PatientClaims
                {
                    get { return _PaymentPatientClaims; }
                    set {
                        if (paymentAssigned)
                        {
                            if (_PaymentPatientClaims != null)
                            {
                                _PaymentPatientClaims.Dispose();
                                _PaymentPatientClaims = null;
                            }
                        }
                         
                        _PaymentPatientClaims = value;
                        paymentAssigned = false;

                    }
                }
                public EOBPatientPaymentDetails EOBPatientPaymentLineDetails
                {
                    get { return _EOBPatientPaymentLineDetails; }
                    set {
                        
                        if (lineAssigned)
                        {
                            if (_EOBPatientPaymentLineDetails != null)
                            {
                                _EOBPatientPaymentLineDetails.Dispose();
                                _EOBPatientPaymentLineDetails = null;
                            }
                        }
                       
                        _EOBPatientPaymentLineDetails = value;
                        lineAssigned = false;
                    }
                }
                public EOBPatientPaymentDetails EOBPatientPaymentReserveLineDetail
                {
                    get { return _EOBPatientPaymentReserveLineDetail; }
                    set {
                       
                        if (reserveAssigned)
                        {
                            if (_EOBPatientPaymentReserveLineDetail != null)
                            {
                                _EOBPatientPaymentReserveLineDetail.Dispose();
                                _EOBPatientPaymentReserveLineDetail = null;
                            }

                        }
                       
                        _EOBPatientPaymentReserveLineDetail = value;
                        reserveAssigned = false;
                    }
                }
                public PaymentPatientLineNotes Notes
                {
                    get { return _Notes; }
                    set {
                       
                        if (notesAssigned)
                        {
                            if (_Notes != null)
                            {
                                _Notes.Dispose();
                                _Notes = null;
                            }
                        }
                        _Notes = value;
                        notesAssigned = false;
                    }
                }
                //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  creating properties 
                public Int64 PAccountID
                { get { return _nPAccountID; } set { _nPAccountID = value; } }

                public Int64 AccountPatientID
                { get { return _nAccountPatientID; } set { _nAccountPatientID = value; } }

                public Int64 GuarantorID
                { get { return _nGuarantorID; } set { _nGuarantorID = value; } }
                //End
              
            }

            public class PaymentPatientClaim
            {
                #region "Private Variables"

                private Int64 _ClaimNo = 0;
                private string _DisplayClaimNo = "";
                private string _ClaimNoPrefix = "";
                private Int64 _BillingTransactionID = 0;
                private Int64 _BillingTransactionDate = 0;
                private Int64 _PatientID = 0;
                private string _PatientName = "";
                private PaymentPatientLines _CliamLines = null;
                private PaymentPatientLineNotes _CliamLineNotes = null;
                private bool linesAssigned = true;
                private bool notesAssigned = true;
                private Int64 _ClinicId = 0;
                //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for declaring Property Variable 
                private string _sRespParty = "";
                //End
                #endregion

                #region "Constructor & Distructor"


                public PaymentPatientClaim()
                {
                    _CliamLines = new PaymentPatientLines();
                    _CliamLineNotes = new PaymentPatientLineNotes();
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
                            if (linesAssigned)
                            {
                                if (_CliamLines != null)
                                {
                                    _CliamLines.Dispose();
                                    _CliamLines = null;
                                }
                            }
                            if (notesAssigned)
                            {
                                if (_CliamLineNotes != null)
                                {
                                    _CliamLineNotes.Dispose();
                                    _CliamLineNotes = null;
                                }
                            }
                        }
                    }
                    disposed = true;
                }

                ~PaymentPatientClaim()
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
                public PaymentPatientLines CliamLines
                {
                    get { return _CliamLines; }
                    set {
                        if (linesAssigned)
                        {
                            if (_CliamLines != null)
                            {
                                _CliamLines.Dispose();
                                _CliamLines = null;
                            }
                        }
                     
                        _CliamLines = value;
                        linesAssigned = false;
                    }
                }

                public Int64 ClinicID
                {
                    get { return _ClinicId; }
                    set { _ClinicId = value; }
                }
                public PaymentPatientLineNotes CliamLineNotes
                {
                    get { return _CliamLineNotes; }
                    set {
   
                        if (notesAssigned)
                        {
                            if (_CliamLineNotes != null)
                            {
                                _CliamLineNotes.Dispose();
                                _CliamLineNotes = null;
                            }
                        }
                        _CliamLineNotes = value;
                        notesAssigned = false;
                    }
                }

                //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  creating Property
                public string RespParty
                {
                    get { return _sRespParty; }
                    set { _sRespParty = value; }
                }
                //End

                #endregion " Property Procedures "
            }

            public class PaymentPatientClaims
            {

                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public PaymentPatientClaims()
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


                ~PaymentPatientClaims()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(PaymentPatientClaim item)
                {
                    _innerlist.Add(item);
                }

                public bool Remove(PaymentPatientClaim item)
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

                public PaymentPatientClaim this[int index]
                {
                    get
                    { return (PaymentPatientClaim)_innerlist[index]; }
                }

                public bool Contains(PaymentPatientClaim item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(PaymentPatientClaim item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(PaymentPatientClaim[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }

            public class PaymentPatientLine
            {
                #region "Constructor & Destructor"

                public PaymentPatientLine()
                {
                    _LineAdjestmentCodes = new PaymentPatientLineAdjustmentCodes();
                    _EOBPatientPaymentLineDetails = new EOBPatientPaymentDetails();
                    _LineNotes = new PaymentPatientLineNotes();
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
                            if (adjustmentAssigned)
                            {
                                if (_LineAdjestmentCodes != null)
                                {
                                    _LineAdjestmentCodes.Dispose();
                                    _LineAdjestmentCodes = null;
                                }
                            }
                            if (paymentAssigned)
                            {
                                if (_EOBPatientPaymentLineDetails != null)
                                {
                                    _EOBPatientPaymentLineDetails.Dispose();
                                    _EOBPatientPaymentLineDetails = null;
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

                ~PaymentPatientLine()
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
                private Int64 _DOSFrom = 0;
                private Int64 _DOSTo = 0;
                private string _CPTCode = "";
                private string _CPTDescription = "";
                private string _sModifiers = "";
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

                private decimal _LinePreviousAdjuestment = 0;
                private decimal _LinePreviousPaid = 0;
                private decimal _LinePreviousPatientPaid = 0;
                private decimal _LineBalance = 0;
                private decimal _LinePatientDue = 0;

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
                private Int64 _nVoidCloseDate = 0;
                private Int64 _nVoidTrayID = 0;
                private bool _bIsVoid = false;
                private VoidType _VoidType = VoidType.None;
                //....Field to identify whether the eob entry is for patient payment or insurance
                private EOBPaymentType _eobType = EOBPaymentType.PatientPayment;

                private EOBPatientPaymentDetails _EOBPatientPaymentLineDetails = null;
                private PaymentPatientLineAdjustmentCodes _LineAdjestmentCodes = null;
                private PaymentPatientLineNotes _LineNotes = null;

                private bool paymentAssigned = true;
                private bool adjustmentAssigned = true;
                private bool notesAssigned = true;

                Int64 _TrackTrnID = 0;
                Int64 _TrackTrnDtlID = 0;
                string _SubClaimNo = string.Empty;

                private Int64 _nCloseDate = 0;

                //Added by Subashish_b on 21/Jan /2011 (integration made on date-10/May/2011) for declaring property pariable 
                private Int64 _nPAccountID = 0;
                private Int64 _nAccountPatientID = 0;
                private Int64 _nGuarantorID = 0;
                private String _sRespParty = "";
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
                public Int64 DOSFrom
                { get { return _DOSFrom; } set { _DOSFrom = value; } }
                public Int64 DOSTo
                { get { return _DOSTo; } set { _DOSTo = value; } }
                public string CPTCode
                { get { return _CPTCode; } set { _CPTCode = value; } }
                public string CPTDescription
                { get { return _CPTDescription; } set { _CPTDescription = value; } }

                public string Modifiers
                { get { return _sModifiers; } set { _sModifiers = value; } }

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

                public decimal LinePreviousPaid
                { get { return _LinePreviousPaid; } set { _LinePreviousPaid = value; } }
                public decimal LinePreviousPatientPaid
                { get { return _LinePreviousPatientPaid; } set { _LinePreviousPatientPaid = value; } }
                public decimal LinePreviousAdjuestment
                { get { return _LinePreviousAdjuestment; } set { _LinePreviousAdjuestment = value; } }

                public decimal LineBalance
                { get { return _LineBalance; } set { _LineBalance = value; } }

                public decimal LinePatientDue
                { get { return _LinePatientDue; } set { _LinePatientDue = value; } }

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

                public Int64 UserID
                { get { return _userId; } set { _userId = value; } }
                public string UserName
                { get { return _userName; } set { _userName = value; } }

                public Int64 ClinicID
                { get { return _clinicId; } set { _clinicId = value; } }

                //....Field to identify whether the eob entry is for patient payment or insurance
                public EOBPaymentType EOBType
                { get { return _eobType; } set { _eobType = value; } }


                public EOBPatientPaymentDetails EOBPatientPaymentLineDetails
                {
                    get { return _EOBPatientPaymentLineDetails; }
                    set {
                        
                        if (paymentAssigned)
                        {
                            if (_EOBPatientPaymentLineDetails != null)
                            {
                                _EOBPatientPaymentLineDetails.Dispose();
                                _EOBPatientPaymentLineDetails = null;
                            }
                        }
                        
                        _EOBPatientPaymentLineDetails = value;
                        paymentAssigned = false;
                    }
                }
                public PaymentPatientLineAdjustmentCodes LineAdjestmentCodes
                {
                    get { return _LineAdjestmentCodes; }
                    set {
                        if (adjustmentAssigned)
                        {
                            if (_LineAdjestmentCodes != null)
                            {
                                _LineAdjestmentCodes.Dispose();
                                _LineAdjestmentCodes = null;
                            }
                        }
                        
                        _LineAdjestmentCodes = value;
                        adjustmentAssigned = false;
                    }
                }
                public PaymentPatientLineNotes LineNotes
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

                public Int64 CloseDate
                { get { return _nCloseDate; } set { _nCloseDate = value; } }

                public Int64 TrackTrnID
                { get { return _TrackTrnID; } set { _TrackTrnID = value; } }
                public Int64 TrackTrnDtlID
                { get { return _TrackTrnDtlID; } set { _TrackTrnDtlID = value; } }
                public string SubClaimNo
                { get { return _SubClaimNo; } set { _SubClaimNo = value; } }

                public Int64 VoidCloseDate
                { get { return _nVoidCloseDate; } set { _nVoidCloseDate = value; } }


                public Int64 VoidTrayID
                { get { return _nVoidTrayID; } set { _nVoidTrayID = value; } }

                public bool IsVoid
                { get { return _bIsVoid; } set { _bIsVoid = value; } }

                public VoidType VoidType
                { get { return _VoidType; } set { _VoidType = value; } }

                //Added by Subashish_b on 21/Jan /2011 (integration made on date-10/May/2011) for  creating properties to store PAF Values

                public Int64 PAccountID
                { get { return _nPAccountID; } set { _nPAccountID = value; } }

                public Int64 AccountPatientID
                { get { return _nAccountPatientID; } set { _nAccountPatientID = value; } }

                public Int64 GuarantorID
                { get { return _nGuarantorID; } set { _nGuarantorID = value; } }

                public String RespParty
                { get { return _sRespParty; } set { _sRespParty = value; } }

                //End

                #endregion " Property Procedures "
            }

            public class PaymentPatientLines
            {

                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public PaymentPatientLines()
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


                ~PaymentPatientLines()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(PaymentPatientLine item)
                {
                    _innerlist.Add(item);
                }

                public bool Remove(PaymentPatientLine item)
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

                public PaymentPatientLine this[int index]
                {
                    get
                    { return (PaymentPatientLine)_innerlist[index]; }
                }

                public bool Contains(PaymentPatientLine item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(PaymentPatientLine item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(PaymentPatientLine[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }

            public class EOBPatientPaymentDetail
            {
                #region "Constructor & Distructor"

                public EOBPatientPaymentDetail()
                {
                    _LineNotes = new PaymentPatientLineNotes();
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

                ~EOBPatientPaymentDetail()
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
                private PaymentPatientLineNotes _LineNotes = null;
                private bool notesAssigned = true;

                private Int64 _nReserveEOBPaymentID = 0;
                private Int64 _nReserveEOBPaymentDetailID = 0;

                private Int32 _nFinanceLieNo = 0;
                private Int64 _nMainCreditLineID = 0;
                private bool _IsMainCreditLine = false;
                private bool _IsReserveCreditLine = false;
                private bool _IsCorrectionCreditLine = false;
                private Int32 _nRefFinanceLieNo = 0;
                private bool _UseRefFinanceLieNo = false;
                private Int64 _nContactInsID = 0;
                private Int64 _nVoidCloseDate = 0;
                private Int64 _nVoidTrayID = 0;
                private bool _bIsVoid = false;
                private VoidType _VoidType = VoidType.None;
                private Int64 _nCloseDate = 0;
                Int64 _TrackTrnID = 0;
                Int64 _TrackTrnDtlID = 0;
                string _SubClaimNo = string.Empty;

                //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for declaring property variable 
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
                public PaymentPatientLineNotes LineNotes
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

                public Int64 TrackTrnID
                { get { return _TrackTrnID; } set { _TrackTrnID = value; } }
                public Int64 TrackTrnDtlID
                { get { return _TrackTrnDtlID; } set { _TrackTrnDtlID = value; } }
                public string SubClaimNo
                { get { return _SubClaimNo; } set { _SubClaimNo = value; } }

                public Int64 VoidCloseDate
                { get { return _nVoidCloseDate; } set { _nVoidCloseDate = value; } }

                public Int64 VoidTrayID
                { get { return _nVoidTrayID; } set { _nVoidTrayID = value; } }

                public bool IsVoid
                { get { return _bIsVoid; } set { _bIsVoid = value; } }

                public VoidType VoidType
                { get { return _VoidType; } set { _VoidType = value; } }

                //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  creating properties to store the PAF values
                public Int64 PAccountID
                { get { return _nPAccountID; } set { _nPAccountID = value; } }

                public Int64 AccountPatientID
                { get { return _nAccountPatientID; } set { _nAccountPatientID = value; } }

                public Int64 GuarantorID
                { get { return _nGuarantorID; } set { _nGuarantorID = value; } }
                //End

                #endregion

            }

            public class EOBPatientPaymentDetails
            {

                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public EOBPatientPaymentDetails()
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


                ~EOBPatientPaymentDetails()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(EOBPatientPaymentDetail item)
                {
                    _innerlist.Add(item);
                }

                public bool Remove(EOBPatientPaymentDetail item)
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

                public EOBPatientPaymentDetail this[int index]
                {
                    get
                    { return (EOBPatientPaymentDetail)_innerlist[index]; }
                }

                public bool Contains(EOBPatientPaymentDetail item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(EOBPatientPaymentDetail item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(EOBPatientPaymentDetail[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }

            public class PaymentPatientLineAdjustmentCode
            {
                #region "Constructor & Distructor"

                public PaymentPatientLineAdjustmentCode()
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

                ~PaymentPatientLineAdjustmentCode()
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
                private string _sReasonCode = "";
                private string _sReasonDescription = "";
                private decimal _dReasonAmount = 0;
                private bool _isNullReasonAmount = true;
                private Int64 _nClinicID = 0;

                private bool _HasData = false;

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
                public bool IsNullAmount
                { get { return _isNullReasonAmount; } set { _isNullReasonAmount = value; } }
                public Int64 ClinicID
                { get { return _nClinicID; } set { _nClinicID = value; } }
                public bool HasData
                { get { return _HasData; } set { _HasData = value; } }
                #endregion

            }

            public class PaymentPatientLineAdjustmentCodes
            {

                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public PaymentPatientLineAdjustmentCodes()
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


                ~PaymentPatientLineAdjustmentCodes()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(PaymentPatientLineAdjustmentCode item)
                {
                    _innerlist.Add(item);
                }

                public bool Remove(PaymentPatientLineAdjustmentCode item)
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

                public PaymentPatientLineAdjustmentCode this[int index]
                {
                    get
                    { return (PaymentPatientLineAdjustmentCode)_innerlist[index]; }
                }

                public bool Contains(PaymentPatientLineAdjustmentCode item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(PaymentPatientLineAdjustmentCode item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(PaymentPatientLineAdjustmentCode[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }

            public class PaymentPatientLineNote
            {
                #region "Constructor & Destructor"

                public PaymentPatientLineNote()
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

                ~PaymentPatientLineNote()
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
                private string _sReasonCode = "";
                private string _sReasonDescription = "";
                private decimal _dReasonAmount = 0;
                private bool _bIncludeOnPrint = false;

                private Int64 _nClinicID = 0;

                private EOBPaymentType _nPaymentNoteType = EOBPaymentType.None;
                private EOBPaymentSubType _nPaymentNoteSubType = EOBPaymentSubType.None;


                private bool _HasData = false;

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

                #endregion

            }

            //Implement the Patient Refund
            public class PatientPaymentReturn
            {
                #region "Constructor & Destructor"

                public PatientPaymentReturn()
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

                ~PatientPaymentReturn()
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

                #endregion

            }


            public class PaymentPatientLineNotes
            {

                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public PaymentPatientLineNotes()
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


                ~PaymentPatientLineNotes()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(PaymentPatientLineNote item)
                {
                    _innerlist.Add(item);
                }

                public bool Remove(PaymentPatientLineNote item)
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

                public PaymentPatientLineNote this[int index]
                {
                    get
                    { return (PaymentPatientLineNote)_innerlist[index]; }
                }

                public bool Contains(PaymentPatientLineNote item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(PaymentPatientLineNote item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(PaymentPatientLineNote[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }
        }
    }
}

