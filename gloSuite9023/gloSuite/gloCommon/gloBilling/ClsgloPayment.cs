using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using gloBilling.Common;
using System.Windows.Forms;

namespace gloBilling
{
    public class gloPayment
    {

        #region " Variable Declarations "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64  _ClinicID = 0;
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "gloPM";
        private Int64  _UserID = 0;
        private string _UserName = "";

        #endregion " Variable Declarations "

        #region " Property Procedures "

        private Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion " Property Procedures "

        #region "Constructor & Distructor"

        public gloPayment(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            #region " Retrive ClinicID from appSetting "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion " Retrive ClinicID from appSetting "

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
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

        ~gloPayment()
        {
            Dispose(false);
        }

        #endregion

        public PatientBalances GetPatientBalaces(Int64 PatientID, Int64 ClinicID)
        {
            PatientBalances oPatientBalances = new PatientBalances();
            PatientBalance oPatientBalance;
            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable oPatientInsurances = new DataTable();

            DataTable oDataBalances = new DataTable();
            Int64 _PatInsID = 0;
            string _PatInsName = "";

            try
            {
                oDBLayer.Connect(false);
                oDBLayer.Retrive_Query("SELECT DISTINCT nInsuranceID,sInsuranceName FROM PatientInsurance_DTL WHERE nPatientID = " + PatientID + " AND nInsuranceID > 0 AND sInsuranceName IS NOT NULL", out oPatientInsurances);

                if (oPatientInsurances != null && oPatientInsurances.Rows.Count > 0)
                {
                    for (int i = 0; i <= oPatientInsurances.Rows.Count - 1; i++)
                    {
                        oDataBalances = new DataTable();

                        oDBParameters.Clear();
                        _PatInsID = Convert.ToInt64(oPatientInsurances.Rows[i]["nInsuranceID"].ToString());
                        _PatInsName = oPatientInsurances.Rows[i]["sInsuranceName"].ToString();

                        oDBParameters.Add("@nPatientID",PatientID,ParameterDirection.Input,SqlDbType.BigInt);
                        oDBParameters.Add("@nInsuranceID",_PatInsID,ParameterDirection.Input,SqlDbType.BigInt);
                        oDBParameters.Add("@nClinicID",ClinicID,ParameterDirection.Input,SqlDbType.BigInt);
                        oDBLayer.Retrive("BL_SELECT_Transaction_PaidNBalance",oDBParameters, out oDataBalances);

                        if (oDataBalances != null && oDataBalances.Rows.Count > 0)
                        { 
                            oPatientBalance = new PatientBalance();
                            decimal _TotCharges = Convert.ToDecimal(oDataBalances.Rows[0]["TotalCharges"].ToString());
                            decimal _TotAllowed = Convert.ToDecimal(oDataBalances.Rows[0]["TotalAllowed"].ToString());
                            decimal _TotPaid = Convert.ToDecimal(oDataBalances.Rows[0]["TotalPaid"].ToString());
                            decimal _TotWriteOff = _TotCharges - _TotAllowed; 

                            oPatientBalance.SelfInsuranceID = _PatInsID;
                            oPatientBalance.SelfInsuranceName = _PatInsName;
                            oPatientBalance.SelfInsuranceType = PayerMode.Insurance;
                            oPatientBalance.SelfInsuranceCharges = _TotCharges;
                            oPatientBalance.SelfInsuranceAllowed = _TotAllowed;
                            oPatientBalance.SelfInsurancePaid = _TotPaid;
                            oPatientBalance.SelfInsuranceBalance = (_TotCharges - (_TotPaid + _TotWriteOff));
                            if (oPatientBalance.SelfInsuranceBalance > 0)
                            {
                                oPatientBalances.Add(oPatientBalance);
                            }
                            oPatientBalance.Dispose();
                        }

                        oDataBalances.Dispose();
                    }
                }

                #region "Patient Balance"
                oDataBalances = new DataTable();

                oDBParameters.Clear();
                _PatInsID = 0;
                _PatInsName = "Self";

                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nInsuranceID", _PatInsID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBLayer.Retrive("BL_SELECT_Transaction_PaidNBalance", oDBParameters, out oDataBalances);

                if (oDataBalances != null && oDataBalances.Rows.Count > 0)
                {
                    oPatientBalance = new PatientBalance();
                    decimal _TotCharges = Convert.ToDecimal(oDataBalances.Rows[0]["TotalCharges"].ToString());
                    decimal _TotAllowed = Convert.ToDecimal(oDataBalances.Rows[0]["TotalAllowed"].ToString());
                    decimal _TotPaid = Convert.ToDecimal(oDataBalances.Rows[0]["TotalPaid"].ToString());
                    decimal _TotWriteOff = _TotCharges - _TotAllowed;

                    oPatientBalance.SelfInsuranceID = _PatInsID;
                    oPatientBalance.SelfInsuranceName = _PatInsName;
                    oPatientBalance.SelfInsuranceType = PayerMode.Self;
                    oPatientBalance.SelfInsuranceCharges = _TotCharges;
                    oPatientBalance.SelfInsuranceAllowed = _TotAllowed;
                    oPatientBalance.SelfInsurancePaid = _TotPaid;
                    oPatientBalance.SelfInsuranceBalance = (_TotCharges - (_TotPaid + _TotWriteOff));
                    if (oPatientBalance.SelfInsuranceBalance > 0)
                    {
                        oPatientBalances.Add(oPatientBalance);
                    }
                    oPatientBalance.Dispose();
                }
                #endregion

                oDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                return null;
            }
            finally
            {
                oDBLayer.Dispose();
                oDBParameters.Dispose();
                oPatientInsurances.Dispose();
            }
            return oPatientBalances;
        }

        public void GetPatientStatement(Int64 PatientID, Int64 ClinicID, out DataTable dtTransaction, out DataTable dtLinesPayment)
        {
            #region " Variable Declarations "

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtTrasaction = new DataTable();
            DataTable _dtLinesPayment = new DataTable();
            DataTable _dtTransactionIds = null;
            Int64 _BillingTransactionID = 0;
         //   Int64 _TransactionPatientId = 0;
            DataTable _dtTempTable = null;
            string _sqlQuery = ""; 

            #endregion

            try
            {
                oDB.Connect(false);

                #region " Get Transaction ID's againts Patient "

                _sqlQuery = "SELECT DISTINCT BL_Transaction_MST.nTransactionID FROM BL_Transaction_MST  " +
                                  "WHERE BL_Transaction_MST.nPatientID = " + PatientID + " AND BL_Transaction_MST.nTransactionID IS NOT NULL AND BL_Transaction_MST.nTransactionID > 0 " +
                                  "ORDER BY BL_Transaction_MST.nTransactionID";
                oDB.Retrive_Query(_sqlQuery, out _dtTransactionIds);

                #endregion " Get Transaction ID's againts Patient "

                if (_dtTransactionIds != null && _dtTransactionIds.Rows.Count > 0)
                {
                    for (int nTrnIDCntr = 0; nTrnIDCntr <= _dtTransactionIds.Rows.Count - 1; nTrnIDCntr++)
                    {
                        #region "Retrive Billing Master Transaction"

                        _BillingTransactionID = Convert.ToInt64(_dtTransactionIds.Rows[nTrnIDCntr]["nTransactionID"].ToString());
                        

                        if (_BillingTransactionID > 0)
                        {
                            oDBParameters.Clear();
                            oDBParameters.Add("@nTransactionID", _BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDB.Retrive("BL_SELECT_Transaction_MST", oDBParameters, out _dtTempTable);
                            if (_dtTempTable != null && _dtTempTable.Rows.Count > 0)
                            {
                                _dtTrasaction.Merge(_dtTempTable, true);
                                _dtTrasaction.AcceptChanges();
                                _dtTempTable = new DataTable();

                                #region " Load TransactionLines & Payments "

                                oDBParameters.Clear();
                                oDBParameters.Add("@nTransactionID", _BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nTransactionLineNo", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                                oDB.Retrive("BL_SELECT_Transaction_Lines_PaidNBalance", oDBParameters, out _dtTempTable);
                                oDBParameters.Clear();

                                if (_dtTempTable != null && _dtTempTable.Rows.Count > 0) 
                                { 
                                    _dtLinesPayment.Merge(_dtTempTable, true);
                                    _dtLinesPayment.AcceptChanges();
                                }
                                _dtTempTable = new DataTable();

                                #endregion " Load TransactionLines & Payments "

                                _BillingTransactionID = 0;
                            }
                        }

                        #endregion
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {dbEx.ERROR_Log(dbEx.Message);}
            catch (Exception ex)
            {gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);}
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtTransactionIds != null) { _dtTransactionIds.Dispose(); }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
            }

            dtTransaction = _dtTrasaction;
            dtLinesPayment = _dtLinesPayment; 
        }

        public PaymentTransaction GetBillingTransactions(Int64 PatientID, Int64 ClinicID, Int64 ClaimNo, bool SearchOnClaimNo, bool IncludeZeroBalanceLine)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtBillingTransactionIDs = new DataTable();
            DataTable dtBillingTransaction = new DataTable();
            DataTable dtBillingTransactionLines = new DataTable();
            PaymentTransaction oResult = null;
            PaymentClaim _PaymentClaim = null;
            PaymentClaimLine _PaymentClaimLine = null;
            int nTrnIDCntr = 0;
            int nTrnCntr = 0;
            int nTrnLineCntr = 0;
            Int64 _patientId = 0;

            Int64 _BillingTransactionID = 0;

            try
            {
                oDB.Connect(false);

                dtBillingTransactionIDs = GetBillingTransactionIDs(PatientID,ClaimNo, SearchOnClaimNo);

                if (dtBillingTransactionIDs != null)
                {
                    
                    if (dtBillingTransactionIDs.Rows.Count > 0)
                    {
                        oResult = new PaymentTransaction();
                        for (nTrnIDCntr = 0; nTrnIDCntr <= dtBillingTransactionIDs.Rows.Count - 1; nTrnIDCntr++)
                        {
                            _BillingTransactionID = Convert.ToInt64(dtBillingTransactionIDs.Rows[nTrnIDCntr]["nTransactionID"].ToString());

                            dtBillingTransaction = new DataTable();

                            #region "Retrive Billing Master Transaction"
                            oDBParameters.Clear();
                            oDBParameters.Add("@nTransactionID", _BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDB.Retrive("BL_SELECT_Transaction_MST", oDBParameters, out dtBillingTransaction);
                            oDBParameters.Clear();
                            #endregion

                            #region "Fill Billing Transaction Information"
                            if (dtBillingTransaction != null)
                            {
                                if (dtBillingTransaction.Rows.Count > 0)
                                {
                                    for (nTrnCntr = 0; nTrnCntr <= dtBillingTransaction.Rows.Count - 1; nTrnCntr++)
                                    {
                                        _PaymentClaim = new PaymentClaim();

                                        _PaymentClaim.TransactionID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionID"]);
                                        _PaymentClaim.TransactionDate = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionDate"]);
                                        _PaymentClaim.ClaimNoPrefix = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["sCaseNoPrefix"]);
                                        _PaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"]);
                                        _PaymentClaim.DisplayClaimNo = GetFormattedClaimPaymentNumber(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"].ToString());
                                        _patientId = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nPatientID"]);

                                        #region "Fill Billing Transaction Service Lines"
                                            dtBillingTransactionLines = new DataTable();

                                            oDBParameters.Clear();

                                            oDBParameters.Add("@nTransactionID", _BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                            oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                            oDBParameters.Add("@nTransactionLineNo", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                            oDBParameters.Add("@nPatientID", _patientId, ParameterDirection.Input, SqlDbType.BigInt);
                                            
                                            oDB.Retrive("BL_SELECT_Transaction_Lines_PaidNBalance", oDBParameters, out dtBillingTransactionLines);
                                            oDBParameters.Clear();

                                            if (dtBillingTransactionLines != null)
                                            {
                                                if (dtBillingTransactionLines.Rows.Count > 0)
                                                {
                                                    for (nTrnLineCntr = 0; nTrnLineCntr <= dtBillingTransactionLines.Rows.Count - 1; nTrnLineCntr++)
                                                    {
                                                        _PaymentClaimLine = new PaymentClaimLine();

                                                        _PaymentClaimLine.PaymentTransactionID = 0;
                                                        _PaymentClaimLine.PaymentTransactionDetailID = 0;
                                                        _PaymentClaimLine.PaymentDate = 0;
                                                        _PaymentClaimLine.PatientID = PatientID;
                                                        _PaymentClaimLine.TransactionID = _BillingTransactionID;
                                                        _PaymentClaimLine.TransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDetailID"].ToString());
                                                        _PaymentClaimLine.TransactionLineNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionLineNo"].ToString());
                                                        _PaymentClaimLine.ClaimNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nClaimNumber"].ToString());
                                                        _PaymentClaimLine.DOSFrom = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nFromDate"].ToString());
                                                        _PaymentClaimLine.DOSTo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nToDate"].ToString());
                                                        _PaymentClaimLine.CPTCode = dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTCode"].ToString();
                                                        _PaymentClaimLine.CPTDescription = dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTDescription"].ToString();
                                                        _PaymentClaimLine.TransactionCharges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dCharges"].ToString());
                                                        _PaymentClaimLine.TransactionUnit = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dUnit"]); //
                                                        _PaymentClaimLine.TransactionTotalCharges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dTotal"]);//
                                                        _PaymentClaimLine.TransactionAllowed = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"].ToString());
                                                        
                                                        //_PaymentClaimLine.TransactionWriteOff = (Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dCharges"].ToString()) - Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"].ToString()));
                                                        _PaymentClaimLine.TransactionWriteOff = _PaymentClaimLine.TransactionTotalCharges - _PaymentClaimLine.TransactionAllowed;

                                                        _PaymentClaimLine.TransactionPaidByInsurance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PaidByInsurance"]); //
                                                        _PaymentClaimLine.TransactionPaidByPatient = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PaidByPatient"]);; //
                                                        _PaymentClaimLine.TransactionPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LinePaid"].ToString());
                                                        _PaymentClaimLine.CurrentPayment = 0;
                                                        _PaymentClaimLine.TransactionBalance = (_PaymentClaimLine.TransactionCharges - (_PaymentClaimLine.TransactionWriteOff + _PaymentClaimLine.TransactionPaid + _PaymentClaimLine.CurrentPayment));
                                                        _PaymentClaimLine.TransactionTypeValue = TransactionType.None;
                                                        _PaymentClaimLine.PaymentModeValue = PaymentMode.None;
                                                        _PaymentClaimLine.PayerModeValue = PayerMode.None;
                                                        if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceID"]).Trim() != "")
                                                        {
                                                            _PaymentClaimLine.PayerModeInsuranceID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceID"].ToString());
                                                        }
                                                        _PaymentClaimLine.PayerModeInsuranceName = dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceName"].ToString();
                                                        _PaymentClaimLine.CardNoAndCheckNoAndMoneyOrderNo = "";
                                                        _PaymentClaimLine.CardExpiryAndCheckDateAndMoneyOrderDate = 0;
                                                        _PaymentClaimLine.SecurityNo = "";
                                                        _PaymentClaimLine.CardType = "";
                                                        _PaymentClaimLine.PaymentLineStatus = TransactionStatus.None;
                                                        _PaymentClaimLine.Notes = null;
                                                        _PaymentClaimLine.ClinicID = ClinicID;

                                                        #region "Extended (Billing) Line Information"

                                                        _PaymentClaimLine.ExtendedLine.POSCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sPOSCode"]); 
                                                        _PaymentClaimLine.ExtendedLine.POSCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sPOSDescription"]);
                                                        _PaymentClaimLine.ExtendedLine.TOSCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sTOSCode"]);
                                                        _PaymentClaimLine.ExtendedLine.TOSDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sTOSDescription"]);
                                                        _PaymentClaimLine.ExtendedLine.CPTCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTCode"]);
                                                        _PaymentClaimLine.ExtendedLine.CPTDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTDescription"]);

                                                        _PaymentClaimLine.ExtendedLine.Dx1Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx1Code"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx1Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx1Description"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx2Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx2Code"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx2Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx2Description"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx3Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx3Code"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx3Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx3Description"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx4Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx4Code"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx4Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx4Description"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx5Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx5Code"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx5Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx5Description"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx6Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx6Code"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx6Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx6Description"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx7Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx7Code"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx7Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx7Description"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx8Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx8Code"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx8Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx8Description"]);

                                                        _PaymentClaimLine.ExtendedLine.Dx1Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx1Pointer"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx2Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx2Pointer"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx3Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx3Pointer"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx4Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx4Pointer"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx5Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx5Pointer"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx6Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx6Pointer"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx7Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx7Pointer"]);
                                                        _PaymentClaimLine.ExtendedLine.Dx8Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx8Pointer"]);

                                                        _PaymentClaimLine.ExtendedLine.Mod1Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod1Code"]);
                                                        _PaymentClaimLine.ExtendedLine.Mod1Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod1Description"]);
                                                        _PaymentClaimLine.ExtendedLine.Mod2Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod2Code"]);
                                                        _PaymentClaimLine.ExtendedLine.Mod2Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod2Description"]);
                                                        _PaymentClaimLine.ExtendedLine.Mod3Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod3Code"]);
                                                        _PaymentClaimLine.ExtendedLine.Mod3Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod3Description"]);
                                                        _PaymentClaimLine.ExtendedLine.Mod4Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod4Code"]);
                                                        _PaymentClaimLine.ExtendedLine.Mod4Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod4Description"]);

                                                        _PaymentClaimLine.ExtendedLine.RefferingProviderId = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["ReneringProviderID"]);
                                                        _PaymentClaimLine.ExtendedLine.RefferingProvider = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["ReneringProviderName"]);

                                                        _PaymentClaimLine.ExtendedLine.BillingProviderId = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["BillingProviderID"]);
                                                        _PaymentClaimLine.ExtendedLine.BillingProvider = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["BillingProviderName"]);

                                                        _PaymentClaimLine.ExtendedLine.FacilityCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sFacilityCode"]);
                                                        _PaymentClaimLine.ExtendedLine.FacilityDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sFacilityDescription"]);

                                                        #endregion

                                                        if (IncludeZeroBalanceLine == true)
                                                        {
                                                            _PaymentClaim.ClaimLines.Add(_PaymentClaimLine);
                                                        }
                                                        else
                                                        {
                                                            //if (_PaymentClaimLine.TransactionBalance > 0)
                                                            if(_PaymentClaimLine.TransactionBalance != 0)
                                                            {
                                                                _PaymentClaim.ClaimLines.Add(_PaymentClaimLine);
                                                            }
                                                        }
                                                        _PaymentClaimLine.Dispose();
                                                    }
                                                }
                                            }
                                            
                                            dtBillingTransactionLines.Dispose();
                                        #endregion

                                            if (_PaymentClaim.ClaimLines.Count > 0)
                                            {
                                                oResult.TransactionPaymentClaims.Add(_PaymentClaim);
                                            }
                                        _PaymentClaim.Dispose();
                                    }
                                }
                            }
                            #endregion

                            dtBillingTransaction.Dispose();
                        }
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                if (dtBillingTransactionIDs != null){dtBillingTransactionIDs.Dispose();}
                if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); }
                if (_PaymentClaim != null) { _PaymentClaim.Dispose(); }
                if (_PaymentClaimLine != null) { _PaymentClaimLine.Dispose(); }
            }
            return oResult;
        }

        public PaymentTransaction GetBilledTransactions(Int64 PatientID, Int64 ClinicID, Int64 ClaimNo, bool SearchOnClaimNo, bool IncludeZeroBalanceLine)
        {
            //....*** Method to load Transactions on Payment form only
            //....*** with status as Batch,Rebatch

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtBillingTransactionIDs = new DataTable();
            DataTable dtBillingTransaction = new DataTable();
            DataTable dtBillingTransactionLines = new DataTable();
            PaymentTransaction oResult = null;
            PaymentClaim _PaymentClaim = null;
            //PaymentClaimLine _PaymentClaimLine = null;
            ClaimLine _PaymentClaimLine = null;
            int nTrnIDCntr = 0;
            int nTrnCntr = 0;
            int nTrnLineCntr = 0;
            Int64 _patientId = 0;

            Int64 _BillingTransactionID = 0;

            try
            {
                oDB.Connect(false);

                //...*** Code changes done on 20090730 by - Sagar Ghodke
                //...*** Code changed to load transaction on payment with status as Batch,ReBatch only
                //...*** For which the GetBillingTransactionIDs method is overloaded with parameter IsForPaymentLoad
                //...*** If true get Transaction with status as Batch & ReBatch
                //...*** Below commented code is existing code to revert to previous logic uncomment the next line
                //...*** and comment the line next to commented line

                dtBillingTransactionIDs = GetBillingTransactionIDs(PatientID, ClaimNo, SearchOnClaimNo);

                //Call this to load transaction with Batch & Rebatch status only
                //dtBillingTransactionIDs = GetBillingTransactionIDs(PatientID, ClaimNo, SearchOnClaimNo, true);

                //...*** End Code changes done on 20090730 by - Sagar Ghodke

                if (dtBillingTransactionIDs != null)
                {

                    if (dtBillingTransactionIDs.Rows.Count > 0)
                    {
                        oResult = new PaymentTransaction();
                        for (nTrnIDCntr = 0; nTrnIDCntr <= dtBillingTransactionIDs.Rows.Count - 1; nTrnIDCntr++)
                        {
                            _BillingTransactionID = Convert.ToInt64(dtBillingTransactionIDs.Rows[nTrnIDCntr]["nTransactionID"].ToString());

                            dtBillingTransaction = new DataTable();

                            #region "Retrive Billing Master Transaction"
                            oDBParameters.Clear();
                            oDBParameters.Add("@nTransactionID", _BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDB.Retrive("BL_SELECT_Transaction_MST", oDBParameters, out dtBillingTransaction);
                            oDBParameters.Clear();
                            #endregion

                            #region "Fill Billing Transaction Information"
                            if (dtBillingTransaction != null)
                            {
                                if (dtBillingTransaction.Rows.Count > 0)
                                {
                                    for (nTrnCntr = 0; nTrnCntr <= dtBillingTransaction.Rows.Count - 1; nTrnCntr++)
                                    {
                                        _PaymentClaim = new PaymentClaim();

                                        _PaymentClaim.TransactionID = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionID"]);
                                        _PaymentClaim.TransactionDate = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nTransactionDate"]);
                                        _PaymentClaim.ClaimNoPrefix = Convert.ToString(dtBillingTransaction.Rows[nTrnCntr]["sCaseNoPrefix"]);
                                        _PaymentClaim.ClaimNo = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"]);
                                        _PaymentClaim.DisplayClaimNo = GetFormattedClaimPaymentNumber(dtBillingTransaction.Rows[nTrnCntr]["nClaimNo"].ToString());
                                        _patientId = Convert.ToInt64(dtBillingTransaction.Rows[nTrnCntr]["nPatientID"]);
                                        _PaymentClaim.ReceivedPaymentCounter = Convert.ToInt32(dtBillingTransaction.Rows[nTrnCntr]["ReceivedPaymentCounter"].ToString());

                                        #region "Fill Billing Transaction Service Lines"
                                        
                                        dtBillingTransactionLines = new DataTable();
                                        oDBParameters.Clear();
                                        oDBParameters.Add("@nTransactionID", _BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                        oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                        oDBParameters.Add("@nTransactionLineNo", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                        oDBParameters.Add("@nPatientID", _patientId, ParameterDirection.Input, SqlDbType.BigInt);
                                        oDB.Retrive("BL_SELECT_Transaction_Lines_PaidNBalance", oDBParameters, out dtBillingTransactionLines);
                                        oDBParameters.Clear();


                                        if (dtBillingTransactionLines != null)
                                        {
                                            if (dtBillingTransactionLines.Rows.Count > 0)
                                            {
                                                for (nTrnLineCntr = 0; nTrnLineCntr <= dtBillingTransactionLines.Rows.Count - 1; nTrnLineCntr++)
                                                {
                                                    _PaymentClaimLine = new ClaimLine();

                                                    _PaymentClaimLine.PaymentTransactionID = 0;
                                                    _PaymentClaimLine.PaymentTransactionDetailID = 0;
                                                    _PaymentClaimLine.PaymentDate = 0;
                                                    _PaymentClaimLine.PatientID = PatientID;
                                                    _PaymentClaimLine.TransactionID = _BillingTransactionID;
                                                    _PaymentClaimLine.TransactionDetailID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionDetailID"].ToString());
                                                    _PaymentClaimLine.TransactionLineNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nTransactionLineNo"].ToString());
                                                    _PaymentClaimLine.ClaimNo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nClaimNumber"].ToString());
                                                    _PaymentClaimLine.DOSFrom = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nFromDate"].ToString());
                                                    _PaymentClaimLine.DOSTo = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["nToDate"].ToString());
                                                    _PaymentClaimLine.CPTCode = dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTCode"].ToString();
                                                    _PaymentClaimLine.CPTDescription = dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTDescription"].ToString();
                                                    _PaymentClaimLine.TransactionCharges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dCharges"].ToString());
                                                    _PaymentClaimLine.TransactionUnit = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dUnit"]); //
                                                    _PaymentClaimLine.TransactionTotalCharges = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dTotal"]);//
                                                    _PaymentClaimLine.TransactionAllowed = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"].ToString());

                                                    //_PaymentClaimLine.TransactionWriteOff = (Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dCharges"].ToString()) - Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["dAllowed"].ToString()));

                                                    _PaymentClaimLine.TransactionWriteOff = _PaymentClaimLine.TransactionTotalCharges - _PaymentClaimLine.TransactionAllowed;

                                                    _PaymentClaimLine.TransactionPaidByInsurance = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PaidByInsurance"]); //
                                                    _PaymentClaimLine.TransactionPaidByPatient = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["PaidByPatient"]); ; //
                                                    _PaymentClaimLine.TransactionPaid = Convert.ToDecimal(dtBillingTransactionLines.Rows[nTrnLineCntr]["LinePaid"].ToString());
                                                    //_PaymentClaimLine.CurrenttotalPayment = 0;
                                                   
                                                    // _PaymentClaimLine.TransactionBalance = (_PaymentClaimLine.TransactionCharges - (_PaymentClaimLine.TransactionWriteOff + _PaymentClaimLine.TransactionPaid )); //+ _PaymentClaimLine.CurrentPayment));
                                                    _PaymentClaimLine.TransactionBalance = (_PaymentClaimLine.TransactionTotalCharges - (_PaymentClaimLine.TransactionWriteOff + _PaymentClaimLine.TransactionPaid));

                                                    //_PaymentClaimLine.TransactionTypeValue = TransactionType.None;
                                                    //_PaymentClaimLine.PaymentModeValue = PaymentMode.None;
                                                    _PaymentClaimLine.PayerModeValue = PayerMode.None;
                                                    if (Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceID"]).Trim() != "")
                                                    {
                                                        _PaymentClaimLine.PayerModeInsuranceID = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceID"].ToString());
                                                    }
                                                    _PaymentClaimLine.PayerModeInsuranceName = dtBillingTransactionLines.Rows[nTrnLineCntr]["LineInsuranceName"].ToString();
                                                    _PaymentClaimLine.PaymentLineStatus = TransactionStatus.None;
                                                    _PaymentClaimLine.ClinicID = ClinicID;

                                                    _PaymentClaimLine.ClaimLineStatusID = Convert.ToInt16(dtBillingTransactionLines.Rows[nTrnLineCntr]["nClaimLineStatusID"]);
                                                    _PaymentClaimLine.ClaimLineStatus = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sClaimLineStatus"]);
                                                    _PaymentClaimLine.SendToFlag = Convert.ToInt16(dtBillingTransactionLines.Rows[nTrnLineCntr]["nSendToFlag"]);

                                                    #region " Extended Billing Line Info - Commented "

                                                    #region "Extended (Billing) Line Information"

                                                    //_PaymentClaimLine.ExtendedLine.POSCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sPOSCode"]);
                                                    //_PaymentClaimLine.ExtendedLine.POSCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sPOSDescription"]);
                                                    //_PaymentClaimLine.ExtendedLine.TOSCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sTOSCode"]);
                                                    //_PaymentClaimLine.ExtendedLine.TOSDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sTOSDescription"]);
                                                    //_PaymentClaimLine.ExtendedLine.CPTCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTCode"]);
                                                    //_PaymentClaimLine.ExtendedLine.CPTDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sCPTDescription"]);

                                                    //_PaymentClaimLine.ExtendedLine.Dx1Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx1Code"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx1Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx1Description"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx2Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx2Code"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx2Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx2Description"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx3Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx3Code"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx3Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx3Description"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx4Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx4Code"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx4Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx4Description"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx5Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx5Code"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx5Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx5Description"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx6Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx6Code"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx6Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx6Description"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx7Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx7Code"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx7Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx7Description"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx8Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx8Code"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx8Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sDx8Description"]);

                                                    //_PaymentClaimLine.ExtendedLine.Dx1Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx1Pointer"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx2Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx2Pointer"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx3Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx3Pointer"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx4Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx4Pointer"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx5Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx5Pointer"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx6Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx6Pointer"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx7Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx7Pointer"]);
                                                    //_PaymentClaimLine.ExtendedLine.Dx8Ptr = Convert.ToBoolean(dtBillingTransactionLines.Rows[nTrnLineCntr]["nDx8Pointer"]);

                                                    //_PaymentClaimLine.ExtendedLine.Mod1Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod1Code"]);
                                                    //_PaymentClaimLine.ExtendedLine.Mod1Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod1Description"]);
                                                    //_PaymentClaimLine.ExtendedLine.Mod2Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod2Code"]);
                                                    //_PaymentClaimLine.ExtendedLine.Mod2Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod2Description"]);
                                                    //_PaymentClaimLine.ExtendedLine.Mod3Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod3Code"]);
                                                    //_PaymentClaimLine.ExtendedLine.Mod3Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod3Description"]);
                                                    //_PaymentClaimLine.ExtendedLine.Mod4Code = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod4Code"]);
                                                    //_PaymentClaimLine.ExtendedLine.Mod4Description = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sMod4Description"]);

                                                    //_PaymentClaimLine.ExtendedLine.RefferingProviderId = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["ReneringProviderID"]);
                                                    //_PaymentClaimLine.ExtendedLine.RefferingProvider = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["ReneringProviderName"]);

                                                    //_PaymentClaimLine.ExtendedLine.BillingProviderId = Convert.ToInt64(dtBillingTransactionLines.Rows[nTrnLineCntr]["BillingProviderID"]);
                                                    //_PaymentClaimLine.ExtendedLine.BillingProvider = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["BillingProviderName"]);

                                                    //_PaymentClaimLine.ExtendedLine.FacilityCode = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sFacilityCode"]);
                                                    //_PaymentClaimLine.ExtendedLine.FacilityDescription = Convert.ToString(dtBillingTransactionLines.Rows[nTrnLineCntr]["sFacilityDescription"]);

                                                    #endregion

                                                    #endregion " Extended Billing Line Info - Commented "

                                                    if (IncludeZeroBalanceLine == true)
                                                    {
                                                        _PaymentClaim.Cliam_Lines.Add(_PaymentClaimLine);
                                                    }
                                                    else
                                                    {
                                                        if (_PaymentClaimLine.TransactionBalance != 0)
                                                        {
                                                            _PaymentClaim.Cliam_Lines.Add(_PaymentClaimLine);
                                                        }
                                                    }
                                                    _PaymentClaimLine.Dispose();
                                                }
                                            }
                                        }

                                        dtBillingTransactionLines.Dispose();
                                        #endregion

                                        if (_PaymentClaim.Cliam_Lines.Count > 0)
                                        {
                                            oResult.TransactionPaymentClaims.Add(_PaymentClaim);
                                        }
                                        _PaymentClaim.Dispose();
                                    }
                                }
                            }
                            #endregion

                            dtBillingTransaction.Dispose();
                        }
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                if (dtBillingTransactionIDs != null) { dtBillingTransactionIDs.Dispose(); }
                if (dtBillingTransaction != null) { dtBillingTransaction.Dispose(); }
                if (_PaymentClaim != null) { _PaymentClaim.Dispose(); }
                if (_PaymentClaimLine != null) { _PaymentClaimLine.Dispose(); }
            }
            return oResult;
        }

        private DataTable GetBillingTransactionIDs(Int64 PatientID, Int64 ClaimNo, bool SearchOnClaimNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = new DataTable();
            try
            {
                oDB.Connect(false);
                string sqlQuery = "";
                sqlQuery = "SELECT DISTINCT BL_Transaction_MST.nTransactionID FROM BL_Transaction_MST  " +
                                  "WHERE BL_Transaction_MST.nPatientID = " + PatientID + " AND BL_Transaction_MST.nTransactionID IS NOT NULL AND BL_Transaction_MST.nTransactionID > 0 " +
                                  "ORDER BY BL_Transaction_MST.nTransactionID";

                if (SearchOnClaimNo == true && ClaimNo > 0)
                {
                    sqlQuery = "SELECT DISTINCT BL_Transaction_MST.nTransactionID FROM BL_Transaction_MST  " +
                                  "WHERE BL_Transaction_MST.nPatientID = " + PatientID + " AND BL_Transaction_MST.nTransactionID IS NOT NULL AND BL_Transaction_MST.nTransactionID > 0 " +
                                  "AND BL_Transaction_MST.nClaimNo = " + ClaimNo + " ORDER BY BL_Transaction_MST.nTransactionID";
                }
                
                oDB.Retrive_Query(sqlQuery, out dt);
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
                oDB.Dispose();
            }
            return dt;
        }

        private DataTable GetBillingTransactionIDs(Int64 PatientID, Int64 ClaimNo, bool SearchOnClaimNo,bool IsForPaymentLoad)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = new DataTable();
            try
            {
                oDB.Connect(false);
                string sqlQuery = "";
                sqlQuery = "SELECT DISTINCT BL_Transaction_MST.nTransactionID FROM BL_Transaction_MST  " +
                                  " WHERE BL_Transaction_MST.nPatientID = " + PatientID + " "+
                                  " AND BL_Transaction_MST.nTransactionID IS NOT NULL "+
                                  " AND BL_Transaction_MST.nTransactionID > 0 " +
                                  " AND nTransactionStatusID IN (" + TransactionStatus.Batch.GetHashCode() + "," + TransactionStatus.ReBatch.GetHashCode() + "," + TransactionStatus.Accepted.GetHashCode() + ") " +
                                  "ORDER BY BL_Transaction_MST.nTransactionID";

                if (SearchOnClaimNo == true && ClaimNo > 0)
                {
                    sqlQuery = "SELECT DISTINCT BL_Transaction_MST.nTransactionID FROM BL_Transaction_MST  " +
                    " WHERE BL_Transaction_MST.nPatientID = " + PatientID + "" +
                    " AND BL_Transaction_MST.nTransactionID IS NOT NULL AND BL_Transaction_MST.nTransactionID > 0 " +
                    " AND BL_Transaction_MST.nClaimNo = " + ClaimNo + " "+
                    " AND BL_Transaction_MST.nTransactionStatusID IN (" + TransactionStatus.Batch.GetHashCode() + "," + TransactionStatus.ReBatch.GetHashCode() + "," + TransactionStatus.Accepted.GetHashCode() + ") " +
                    "ORDER BY BL_Transaction_MST.nTransactionID";
                }

                oDB.Retrive_Query(sqlQuery, out dt);
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
                oDB.Dispose();
            }
            return dt;
        }

        public Int64 GeneratePaymentNumber()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            Object _Result = null;
            Int64 _GeneratedPaymentNumber = 0;

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT ISNULL(MAX(nPaymentNo),0) + 1 FROM BL_Transaction_Payment_MST";
                _Result = oDB.ExecuteScalar_Query(_sqlQuery);

                if (_Result != null)
                {
                    _GeneratedPaymentNumber = Convert.ToInt64(_Result);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _GeneratedPaymentNumber = 0;
            }
            finally
            {
                if (oDB.Connect(false))
                { oDB.Disconnect(); }

                if (oDB != null)
                { oDB.Dispose(); }

                if (_Result != null)
                { _Result = null; }
            }
            return _GeneratedPaymentNumber;
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
              //  NumberSize = NumberSize;
            }
            return NumberSize;
        }

        public Int64 SavePaymentTransactionHistory(Int64 PaymentTransactionId, Int64 UserId, string UserName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object retVal = null;
            Int64 _trnHSTId = 0;

            try
            {

                //@nTrnHSTID numeric(18,0),@nTransactionID numeric(18,0),@nTrnPayHSTDate numeric(18,0),@nTrnPayHSTTime numeric(18,0),
                //@nTrnPayHSTUserID numeric(18,0),@nTrnPayHSTUserName varchar(255),@MachineID numeric(18,0)

                oDBParameters.Add("@nTrnHSTID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//  numeric(18,0) output,
                oDBParameters.Add("@nTransactionID", PaymentTransactionId, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oDBParameters.Add("@nTrnPayHSTDate", gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oDBParameters.Add("@nTrnPayHSTTime", gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToShortTimeString()), ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oDBParameters.Add("@nTrnPayHSTUserID", UserId, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                oDBParameters.Add("@nTrnPayHSTUserName", UserName, ParameterDirection.Input, SqlDbType.VarChar, 255);// varchar(255),
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)

                oDB.Connect(false);
                oDB.Execute("BL_IN_TRN_PAY_HISTORY", oDBParameters, out retVal);
                oDB.Disconnect();
                if (retVal != null)
                { _trnHSTId = Convert.ToInt64(retVal); }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.Message); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
            }

            return _trnHSTId;
        }
       
        #region " Add Payment Transaction - Old Method "

        //public Int64 AddPaymentTransaction(PaymentTransaction oPaymentTransaction, Int64 ClinicID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //    Int64 _PaymentTransactionID = 0;
        //    Int64 _PaymentTransactionDetailID = 0;
        //    object oPaymentTransactionID = null;
        //    object oPaymentTransactionDetailID = null;
        //    try
        //    {
        //        oDB.Connect(false);

        //        oDBParameters.Add("@nPaymentTransactionID", oPaymentTransaction.PaymentTransactionID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //        oDBParameters.Add("@nPaymentDate", oPaymentTransaction.PaymentDate, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@nPaymentTime", oPaymentTransaction.PaymentTime, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@nPaymentNo", oPaymentTransaction.PaymentTransactionNo, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@nTransactionType", oPaymentTransaction.TransactionTypeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //        oDBParameters.Add("@nPaymentMode", oPaymentTransaction.PaymentModeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //        oDBParameters.Add("@dPayerAmount", oPaymentTransaction.PayerAmount, ParameterDirection.Input, SqlDbType.Decimal);
        //        oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", oPaymentTransaction.CardNoAndCheckNoAndMoneyOrderNo, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@nCrdExpChkMnyOrdDate", oPaymentTransaction.CardExpiryAndCheckDateAndMoneyOrderDate, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@sSecurityNo", oPaymentTransaction.SecurityNo, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@sCardType", oPaymentTransaction.CardType, ParameterDirection.Input, SqlDbType.VarChar);

        //        oDBParameters.Add("@sAuthorizationNo", oPaymentTransaction.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@nCardID", oPaymentTransaction.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);

        //        oDBParameters.Add("@nTransactionTypeDetailID", oPaymentTransaction.TransactionTypeDetailValue, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@sTransactionTypeDetailName", oPaymentTransaction.TransactionTypeDetailName, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@nPaymentTransactionStatus", oPaymentTransaction.PaymentTransactionStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //        oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

        //        oDB.Execute("BL_INUP_Transaction_Payment_MST", oDBParameters, out oPaymentTransactionID);

        //        if (oPaymentTransactionID != null && Convert.ToString(oPaymentTransactionID) != "")
        //        {
        //            _PaymentTransactionID = Convert.ToInt64(oPaymentTransactionID);

        //            for (int k = 0; k < oPaymentTransaction.TransactionPaymentClaims.Count; k++)
        //            {
        //                for (int i = 0; i < oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines.Count; i++)
        //                {
        //                    oDBParameters.Clear();

        //                    //private decimal _CurrentPayment = 0;
        //                    //private decimal _CurrentPayment_Insurance = 0;
        //                    //private decimal _CurrentPayment_Patient = 0;
        //                    //private decimal _CurrentPayment_Copay = 0;
        //                    //private decimal _CurrentPayment_Deductable = 0;
        //                    //private decimal _CurrentPayment_Adjustment = 0;
        //                    //private decimal _CurrentPayment_CoInsurance = 0;
        //                    //private decimal _CurrentPayment_Refund = 0;

        //                    for (int l = 0; l <= 8; l++)
        //                    {

        //                        decimal _curSubPayAmount = 0;
        //                        TransactionType _curSubPayTrnType = TransactionType.Payment;
        //                        Int64 _curSubPayCopayID = 0;
        //                        Int64 _curSubPrePayId = 0;

        //                        bool _curSubPayAdd = false;
        //                        bool _curSubPayNoteAdd = false;
        //                        int lnc = 0; //Note Line Number

        //                        PaymentModeDetail _PaymentModeDetail = new PaymentModeDetail();

        //                        #region "Find which payment has to save eg: Payment, Patient, Copay, Advance, etc"
        //                        if (l == 0)
        //                        {
        //                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment != 0)
        //                            {
        //                                _curSubPayAdd = true;
        //                                _curSubPayTrnType = TransactionType.Payment;
        //                                _curSubPayAmount = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment;
        //                                if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details != null
        //                                    && oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details.Count > 0)
        //                                {
        //                                    _PaymentModeDetail = (PaymentModeDetail)oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details[TransactionType.Payment];
        //                                }
        //                                _curSubPayAdd = false;
        //                            }
        //                        }
        //                        else if (l == 1)
        //                        {
        //                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_Insurance != 0)
        //                            {
        //                                _curSubPayAdd = true;
        //                                _curSubPayTrnType = TransactionType.InsuracePayment;
        //                                _curSubPayAmount = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_Insurance;

        //                                if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details != null
        //                                    && oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details.Count > 0)
        //                                {
        //                                    _PaymentModeDetail = (PaymentModeDetail)oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details[TransactionType.InsuracePayment];
        //                                }
        //                            }
        //                        }
        //                        else if (l == 2)
        //                        {
        //                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_Patient != 0)
        //                            {
        //                                _curSubPayAdd = true;
        //                                _curSubPayTrnType = TransactionType.PatientPayment;
        //                                _curSubPayAmount = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_Patient;
        //                                _curSubPrePayId = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PrePayID;
        //                                if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details != null
        //                                    && oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details.Count > 0)
        //                                {
        //                                    _PaymentModeDetail = (PaymentModeDetail)oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details[TransactionType.PatientPayment];
        //                                }
        //                            }
        //                        }
        //                        else if (l == 3)
        //                        {
        //                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_Copay != 0)
        //                            {
        //                                _curSubPayAdd = true;
        //                                _curSubPayTrnType = TransactionType.Copay;
        //                                _curSubPayAmount = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_Copay;
        //                                _curSubPayCopayID = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CopayID;
        //                                if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details != null
        //                                    && oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details.Count > 0)
        //                                {
        //                                    _PaymentModeDetail = (PaymentModeDetail)oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details[TransactionType.Copay];
        //                                }

        //                            }
        //                        }
        //                        else if (l == 4)
        //                        {
        //                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_Deductable != 0)
        //                            {
        //                                _curSubPayAdd = true;
        //                                _curSubPayTrnType = TransactionType.Deductible;
        //                                _curSubPayAmount = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_Deductable;
        //                                if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details != null
        //                                    && oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details.Count > 0)
        //                                {
        //                                    _PaymentModeDetail = (PaymentModeDetail)oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details[TransactionType.Deductible];
        //                                }
        //                            }
        //                        }
        //                        else if (l == 5)
        //                        {
        //                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_Adjustment != 0)
        //                            {
        //                                _curSubPayAdd = true;
        //                                _curSubPayTrnType = TransactionType.Adjustment;
        //                                _curSubPayAmount = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_Adjustment;
        //                                if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details != null
        //                                    && oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details.Count > 0)
        //                                {
        //                                    _PaymentModeDetail = (PaymentModeDetail)oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details[TransactionType.Adjustment];
        //                                }

        //                                ////***..Set Adjustment Type 
        //                                //oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionTypeDetailValue =  
        //                                //oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionTypeDetailName,

        //                            }
        //                        }
        //                        else if (l == 6)
        //                        {
        //                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_CoInsurance != 0)
        //                            {
        //                                _curSubPayAdd = true;
        //                                _curSubPayTrnType = TransactionType.Coinsurance;
        //                                _curSubPayAmount = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_CoInsurance;
        //                                if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details != null
        //                                    && oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details.Count > 0)
        //                                {
        //                                    _PaymentModeDetail = (PaymentModeDetail)oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details[TransactionType.Coinsurance];
        //                                }
        //                            }
        //                        }
        //                        else if (l == 7)
        //                        {
        //                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_Refund != 0)
        //                            {
        //                                _curSubPayAdd = true;
        //                                _curSubPayTrnType = TransactionType.Refund;
        //                                _curSubPayAmount = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_Refund;
        //                                if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details != null
        //                                    && oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details.Count > 0)
        //                                {
        //                                    _PaymentModeDetail = (PaymentModeDetail)oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details[TransactionType.Refund];
        //                                }
        //                            }
        //                        }
        //                        else if (l == 8)
        //                        {
        //                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_Withhold != 0)
        //                            {
        //                                _curSubPayAdd = true;
        //                                _curSubPayTrnType = TransactionType.WithHold;
        //                                _curSubPayAmount = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment_Withhold;
        //                                if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details != null
        //                                    && oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details.Count > 0)
        //                                {
        //                                    _PaymentModeDetail = (PaymentModeDetail)oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentMode_Details[TransactionType.WithHold];
        //                                }
        //                            }
        //                        }

        //                        if (_PaymentModeDetail == null) { _PaymentModeDetail = new PaymentModeDetail(); }

        //                        #region "Find note counter"
        //                        if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes != null)
        //                        {
        //                            for (int nFc = 0; nFc <= oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes.Count - 1; nFc++)
        //                            {
        //                                if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[nFc] == null)
        //                                { continue; }

        //                                if (l == 0)
        //                                {
        //                                    if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[nFc].NoteType == NoteType.GeneralNote)
        //                                    {
        //                                        _curSubPayNoteAdd = true;
        //                                        lnc = nFc;
        //                                        break;
        //                                    }
        //                                }
        //                                else if (l == 1)
        //                                {
        //                                    if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[nFc].NoteType == NoteType.Payment_Insurance)
        //                                    {
        //                                        _curSubPayNoteAdd = true;
        //                                        lnc = nFc;
        //                                        break;
        //                                    }
        //                                }
        //                                else if (l == 2)
        //                                {
        //                                    if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[nFc].NoteType == NoteType.Payment_Patient)
        //                                    {
        //                                        _curSubPayNoteAdd = true;
        //                                        lnc = nFc;
        //                                        break;
        //                                    }
        //                                }
        //                                else if (l == 3)
        //                                {
        //                                    if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[nFc].NoteType == NoteType.Payment_Copay)
        //                                    {
        //                                        _curSubPayNoteAdd = true;
        //                                        lnc = nFc;
        //                                        break;
        //                                    }
        //                                }
        //                                else if (l == 4)
        //                                {
        //                                    if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[nFc].NoteType == NoteType.Payment_Deductable)
        //                                    {
        //                                        _curSubPayNoteAdd = true;
        //                                        lnc = nFc;
        //                                        break;
        //                                    }
        //                                }
        //                                else if (l == 5)
        //                                {
        //                                    if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[nFc].NoteType == NoteType.Payment_Adjustment)
        //                                    {
        //                                        _curSubPayNoteAdd = true;
        //                                        lnc = nFc;
        //                                        break;
        //                                    }
        //                                }
        //                                else if (l == 6)
        //                                {
        //                                    if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[nFc].NoteType == NoteType.Payment_Coinsurance)
        //                                    {
        //                                        _curSubPayNoteAdd = true;
        //                                        lnc = nFc;
        //                                        break;
        //                                    }
        //                                }
        //                                else if (l == 7)
        //                                {
        //                                    if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[nFc].NoteType == NoteType.Payment_Refund)
        //                                    {
        //                                        _curSubPayNoteAdd = true;
        //                                        lnc = nFc;
        //                                        break;
        //                                    }
        //                                }
        //                                else if (l == 8)
        //                                {
        //                                    if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[nFc].NoteType == NoteType.Payment_WithHold)
        //                                    {
        //                                        _curSubPayNoteAdd = true;
        //                                        lnc = nFc;
        //                                        break;
        //                                    }
        //                                }
        //                            }s
        //                        }
        //                        #endregion
        //                        #endregion

        //                        if (_curSubPayAdd == true)
        //                        {
        //                            #region "Line Payment"
        //                            oDBParameters.Add("@nPaymentTransactionID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nPaymentTransactionDetailID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentTransactionDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nPatientID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PatientID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nPaymentDate", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentDate, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nBillingTransactionID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nBillingTransactionDetailID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nBillingTransactionLineNo", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nClaimNo", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@sCPTCode", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CPTCode, ParameterDirection.Input, SqlDbType.VarChar);
        //                            oDBParameters.Add("@sCPTDescription", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);
        //                            oDBParameters.Add("@dAllowedAmt", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionAllowed, ParameterDirection.Input, SqlDbType.Decimal);


        //                            //Old Logic for single payment against one line
        //                            //oDBParameters.Add("@nCurrentPaymentAmtType", _curSubPayTrnModeoPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentModeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                            //oDBParameters.Add("@dCurrentPaymentAmt", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CurrentPayment, ParameterDirection.Input, SqlDbType.Decimal);
        //                            //oDBParameters.Add("@nTransactionType", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionTypeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

        //                            //New logic multiple payments against single line

        //                            oDBParameters.Add("@dCurrentPaymentAmt", _curSubPayAmount, ParameterDirection.Input, SqlDbType.Decimal);
        //                            oDBParameters.Add("@nTransactionType", _curSubPayTrnType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

        //                            if (_curSubPayCopayID > 0)
        //                            { oDBParameters.Add("@nCoPayID", _curSubPayCopayID, ParameterDirection.Input, SqlDbType.BigInt); }
        //                            else if (_curSubPrePayId > 0)
        //                            { oDBParameters.Add("@nCoPayID", _curSubPrePayId, ParameterDirection.Input, SqlDbType.BigInt); }
        //                            else
        //                            { oDBParameters.Add("@nCoPayID", 0, ParameterDirection.Input, SqlDbType.BigInt); }

        //                            //

        //                            //oDBParameters.Add("@nPaymentMode", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentModeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                            //oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CardNoAndCheckNoAndMoneyOrderNo, ParameterDirection.Input, SqlDbType.VarChar);
        //                            //oDBParameters.Add("@nCrdExpChkMnyOrdDate", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CardExpiryAndCheckDateAndMoneyOrderDate, ParameterDirection.Input, SqlDbType.BigInt);
        //                            //oDBParameters.Add("@sSecurityNo", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].SecurityNo, ParameterDirection.Input, SqlDbType.VarChar);
        //                            //oDBParameters.Add("@sCardType", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CardType, ParameterDirection.Input, SqlDbType.VarChar);



        //                            oDBParameters.Add("@nCurrentPaymentAmtType", _PaymentModeDetail.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                            oDBParameters.Add("@nPaymentMode", _PaymentModeDetail.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                            oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", _PaymentModeDetail.CheckMoneyOrderCardEFT_Number, ParameterDirection.Input, SqlDbType.VarChar);
        //                            oDBParameters.Add("@nCrdExpChkMnyOrdDate", _PaymentModeDetail.CheckMoneyOrderCardExpiryEFT_Date, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@sSecurityNo", _PaymentModeDetail.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
        //                            oDBParameters.Add("@sCardType", _PaymentModeDetail.CardType, ParameterDirection.Input, SqlDbType.VarChar);
        //                            oDBParameters.Add("@sAuthorizationNo", _PaymentModeDetail.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
        //                            oDBParameters.Add("@nCardID", _PaymentModeDetail.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);

        //                            oDBParameters.Add("@nPaymentInsuranceID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PayerModeInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nPayerModeID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PayerModeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                            oDBParameters.Add("@nPaymentTransactionLineStatus", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentLineStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

        //                            if (_PaymentModeDetail.TransactionTypeMode == TransactionType.Adjustment)
        //                            {
        //                                oDBParameters.Add("@nTransactionTypeDetailID", _PaymentModeDetail.AdjustmentTypeID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                oDBParameters.Add("@sTransactionTypeDetailName", _PaymentModeDetail.Adjustment_Type.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar);
        //                            }
        //                            else
        //                            {
        //                                oDBParameters.Add("@nTransactionTypeDetailID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionTypeDetailValue, ParameterDirection.Input, SqlDbType.BigInt);
        //                                oDBParameters.Add("@sTransactionTypeDetailName", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionTypeDetailName, ParameterDirection.Input, SqlDbType.VarChar);
        //                            }

        //                            oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);


        //                            #endregion

        //                            oDB.Execute("BL_INUP_Transaction_Payment_DTL", oDBParameters, out oPaymentTransactionDetailID);
        //                            oDBParameters.Clear();

        //                            _PaymentTransactionDetailID = Convert.ToInt64(oPaymentTransactionDetailID);


        //                            #region "Add Notes"
        //                            if (_curSubPayNoteAdd == true)
        //                            {
        //                                if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes.Count > 0)
        //                                {
        //                                    if (_PaymentTransactionID > 0 && _PaymentTransactionDetailID > 0)
        //                                    {
        //                                        if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes.Count > 0)
        //                                        {
        //                                            //_curSubPayTrnType
        //                                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[lnc] != null)
        //                                            {
        //                                                oDBParameters.Clear();

        //                                                oDBParameters.Add("@nPaymentTransactionID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nPaymentTransactionDetailID", _PaymentTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nPaymentNoteId", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nNoteType", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[lnc].NoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                                oDBParameters.Add("@nNoteDateTime", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[lnc].NoteDate, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nUserID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[lnc].UserID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@sNoteDescription", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[lnc].NoteDescription, ParameterDirection.Input, SqlDbType.VarChar);
        //                                                oDBParameters.Add("@nClinicID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Notes[lnc].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

        //                                                oDB.Execute("BL_INUP_Transaction_Payment_Notes", oDBParameters);
        //                                                oDBParameters.Clear();
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            #endregion

        //                            #region " Add Other Payments "

        //                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments != null
        //                                && oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments.Count > 0)
        //                            {
        //                                if (_PaymentTransactionID > 0 && _PaymentTransactionDetailID > 0)
        //                                {
        //                                    if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments.Count > 0)
        //                                    {
        //                                        for (int index = 0; index < oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments.Count; index++)
        //                                        {

        //                                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments[index] != null)
        //                                            {
        //                                                oDBParameters.Clear();

        //                                                TransactionOtherPayment _otherPayment = new TransactionOtherPayment();
        //                                                _otherPayment = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments[index];

        //                                                if (_otherPayment.OtherPayment_Type == _curSubPayTrnType)
        //                                                {
        //                                                    oDBParameters.Add("@nOtherPaymentID", _otherPayment.OtherPayID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nTransactionID", _otherPayment.TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nTransactionDetailID", _otherPayment.TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nTransactionLineNo", _otherPayment.TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nAmountType", _otherPayment.OtherPayment_Type.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                                    oDBParameters.Add("@dAmount", _otherPayment.PaymentAmount, ParameterDirection.Input, SqlDbType.Decimal);
        //                                                    oDBParameters.Add("@nVisitID", _otherPayment.LineVisitID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nAppointmentID", _otherPayment.LineAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@bIsPaid", _otherPayment.IsOtherPaymentPaid, ParameterDirection.Input, SqlDbType.Bit);
        //                                                    oDBParameters.Add("@nPaymentID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nPaymentDetailID", _PaymentTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nReferenceID", _otherPayment.ReferenceID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PatientID), ParameterDirection.Input, SqlDbType.BigInt);

        //                                                    Object _retVal = null;
        //                                                    oDB.Execute("BL_INUP_OtherPayments", oDBParameters, out _retVal);
        //                                                    oDBParameters.Clear();
        //                                                    if (_otherPayment != null) { _otherPayment.Dispose(); }
        //                                                    break;
        //                                                }

        //                                            }

        //                                        }
        //                                    }
        //                                }
        //                            }

        //                            #endregion " Add Other Payments "

        //                            _PaymentModeDetail.Dispose();
        //                        }
        //                    }
        //                }
        //            }


        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException dbex)
        //    {
        //        dbex.ERROR_Log(dbex.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //        if (oDBParameters != null) { oDBParameters.Dispose(); }
        //    }
        //    return _PaymentTransactionID;
        //} 

        #endregion

        #region " New method to save Payment  - Commented on 20090624"

        //public Int64 AddPaymentTransaction(PaymentTransaction oPaymentTransaction, Int64 ClinicID,Int64 PatientID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //    Int64 _PaymentTransactionID = 0;
        //    Int64 _PaymentTransactionDetailID = 0;
        //    object oPaymentTransactionID = null;
        //    object oPaymentTransactionDetailID = null;
        //    try
        //    {
        //        oDB.Connect(false);

        //        oDBParameters.Add("@nPaymentTransactionID", oPaymentTransaction.PaymentTransactionID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //        oDBParameters.Add("@nPaymentDate", oPaymentTransaction.PaymentDate, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@nPaymentTime", oPaymentTransaction.PaymentTime, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@nPaymentNo", oPaymentTransaction.PaymentTransactionNo, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@nTransactionType", oPaymentTransaction.TransactionTypeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //        oDBParameters.Add("@nPaymentMode", oPaymentTransaction.PaymentModeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //        oDBParameters.Add("@dPayerAmount", oPaymentTransaction.PayerAmount, ParameterDirection.Input, SqlDbType.Decimal);
        //        oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", oPaymentTransaction.CardNoAndCheckNoAndMoneyOrderNo, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@nCrdExpChkMnyOrdDate", oPaymentTransaction.CardExpiryAndCheckDateAndMoneyOrderDate, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@sSecurityNo", oPaymentTransaction.SecurityNo, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@sCardType", oPaymentTransaction.CardType, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@sAuthorizationNo", oPaymentTransaction.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@nCardID", oPaymentTransaction.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@nTransactionTypeDetailID", oPaymentTransaction.TransactionTypeDetailValue, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@sTransactionTypeDetailName", oPaymentTransaction.TransactionTypeDetailName, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@nPaymentTransactionStatus", oPaymentTransaction.PaymentTransactionStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //        oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@sUserName",UserName, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@nReplicationID", oDB.GetPrefixTransactionID(PatientID), ParameterDirection.Input, SqlDbType.BigInt);

        //        oDB.Execute("BL_INUP_Transaction_Payment_MST", oDBParameters, out oPaymentTransactionID);

        //        if (oPaymentTransactionID != null && Convert.ToString(oPaymentTransactionID) != "")
        //        {
        //            _PaymentTransactionID = Convert.ToInt64(oPaymentTransactionID);

        //            for (int k = 0; k < oPaymentTransaction.TransactionPaymentClaims.Count; k++)
        //            {
        //                for (int i = 0; i < oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines.Count; i++)
        //                {
        //                    oDBParameters.Clear();
        //                    LinePaymentDetail _linePaymentDtl = null;

        //                    for (int l = 0; l < oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].LinePayment_Details.Count; l++)
        //                    {
        //                        _linePaymentDtl = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].LinePayment_Details[l];

        //                        if (_linePaymentDtl != null 
        //                            && _linePaymentDtl.Transaction_Type != TransactionType.None
        //                            && _linePaymentDtl.Transaction_Type != TransactionType.Payment)
        //                        {
        //                            ////******************************************************************/////
        //                            ////***...Uncomment this condition if zero payment entry not allowed
        //                            if (_linePaymentDtl.LinePaymentAmount <= 0)
        //                            { continue; }
        //                            ////******************************************************************/////

        //                            oDBParameters.Clear();
        //                            oDBParameters.Add("@nPaymentTransactionID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nPaymentTransactionDetailID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentTransactionDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nPatientID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PatientID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nPaymentDate", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentDate, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nBillingTransactionID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nBillingTransactionDetailID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nBillingTransactionLineNo", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nClaimNo", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@sCPTCode", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CPTCode, ParameterDirection.Input, SqlDbType.VarChar);
        //                            oDBParameters.Add("@sCPTDescription", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);
        //                            oDBParameters.Add("@dAllowedAmt", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionAllowed, ParameterDirection.Input, SqlDbType.Decimal);

        //                            oDBParameters.Add("@dCurrentPaymentAmt", _linePaymentDtl.LinePaymentAmount, ParameterDirection.Input, SqlDbType.Decimal);
        //                            oDBParameters.Add("@nTransactionType", _linePaymentDtl.Transaction_Type.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                            oDBParameters.Add("@nCoPayID", 0, ParameterDirection.Input, SqlDbType.BigInt);

        //                            #region " Payment Mode Details "

        //                            if (_linePaymentDtl.PaymentMode_Detail != null)
        //                            {
        //                                oDBParameters.Add("@nCurrentPaymentAmtType", _linePaymentDtl.PaymentMode_Detail.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                oDBParameters.Add("@nPaymentMode", _linePaymentDtl.PaymentMode_Detail.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", _linePaymentDtl.PaymentMode_Detail.CheckMoneyOrderCardEFT_Number, ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@nCrdExpChkMnyOrdDate", _linePaymentDtl.PaymentMode_Detail.CheckMoneyOrderCardExpiryEFT_Date, ParameterDirection.Input, SqlDbType.BigInt);
        //                                oDBParameters.Add("@sSecurityNo", _linePaymentDtl.PaymentMode_Detail.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@sCardType", _linePaymentDtl.PaymentMode_Detail.CardType, ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@sAuthorizationNo", _linePaymentDtl.PaymentMode_Detail.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@nCardID", _linePaymentDtl.PaymentMode_Detail.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);

        //                                if (_linePaymentDtl.PaymentMode_Detail.TransactionTypeMode == TransactionType.InsuracePayment)
        //                                {
        //                                    oDBParameters.Add("@nPaymentInsuranceID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PayerModeInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@nPayerModeID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PayerModeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                }
        //                                else
        //                                {
        //                                    if (_linePaymentDtl.LinePaymentAmount == 0)
        //                                    {
        //                                        oDBParameters.Add("@nPaymentInsuranceID", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nPayerModeID", PayerMode.None.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                    }
        //                                    else
        //                                    {
        //                                        oDBParameters.Add("@nPaymentInsuranceID", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nPayerModeID", PayerMode.Self.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                    }

        //                                }

        //                                if (_linePaymentDtl.PaymentMode_Detail.TransactionTypeMode == TransactionType.Adjustment)
        //                                {
        //                                    oDBParameters.Add("@nTransactionTypeDetailID", _linePaymentDtl.PaymentMode_Detail.AdjustmentTypeID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@sTransactionTypeDetailName", _linePaymentDtl.PaymentMode_Detail.Adjustment_Type.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar);
        //                                }
        //                                else
        //                                {
        //                                    oDBParameters.Add("@nTransactionTypeDetailID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionTypeDetailValue, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@sTransactionTypeDetailName", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionTypeDetailName, ParameterDirection.Input, SqlDbType.VarChar);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                oDBParameters.Add("@nCurrentPaymentAmtType", PaymentMode.None.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                oDBParameters.Add("@nPaymentMode", PaymentMode.None.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", 0, ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@nCrdExpChkMnyOrdDate", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //                                oDBParameters.Add("@sSecurityNo", 0, ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@sCardType", "", ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@sAuthorizationNo", "", ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@nCardID", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //                                oDBParameters.Add("@nPaymentInsuranceID", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //                                oDBParameters.Add("@nPayerModeID", PayerMode.None.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                oDBParameters.Add("@nTransactionTypeDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //                                oDBParameters.Add("@sTransactionTypeDetailName", "", ParameterDirection.Input, SqlDbType.VarChar);
        //                            }

        //                            #endregion " Payment Mode Details "

        //                            oDBParameters.Add("@nPaymentTransactionLineStatus", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentLineStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                            oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@sUserName", UserName, ParameterDirection.Input, SqlDbType.VarChar);
        //                            oDBParameters.Add("@nReplicationID", oDB.GetPrefixTransactionID(oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PatientID), ParameterDirection.Input, SqlDbType.BigInt);

        //                            oDB.Execute("BL_INUP_Transaction_Payment_DTL", oDBParameters, out oPaymentTransactionDetailID);
        //                            oDBParameters.Clear();
        //                            _PaymentTransactionDetailID = Convert.ToInt64(oPaymentTransactionDetailID);

        //                            #region " Add Note "

        //                            if (_linePaymentDtl.Payments_Notes != null && _linePaymentDtl.Payments_Notes.Count > 0)
        //                            {
        //                                if (_linePaymentDtl.Payments_Notes[0] != null)
        //                                {
        //                                    oDBParameters.Clear();
        //                                    oDBParameters.Add("@nPaymentTransactionID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@nPaymentTransactionDetailID", _PaymentTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@nPaymentNoteId", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@nNoteType", _linePaymentDtl.Payments_Notes[0].NoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                    oDBParameters.Add("@nNoteDateTime", _linePaymentDtl.Payments_Notes[0].NoteDate, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@nUserID", _linePaymentDtl.Payments_Notes[0].UserID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@sNoteDescription", _linePaymentDtl.Payments_Notes[0].NoteDescription, ParameterDirection.Input, SqlDbType.VarChar);
        //                                    oDBParameters.Add("@nClinicID", _linePaymentDtl.Payments_Notes[0].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDB.Execute("BL_INUP_Transaction_Payment_Notes", oDBParameters);
        //                                    oDBParameters.Clear();
        //                                }
        //                            }

        //                            #endregion " Add Note "

        //                            #region " Add Other Payments in Transaction in (BL_Transaction_OtherPayments) tables "

        //                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments != null
        //                              && oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments.Count > 0)
        //                            {
        //                                if (_PaymentTransactionID > 0 && _PaymentTransactionDetailID > 0)
        //                                {
        //                                    if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments.Count > 0)
        //                                    {
        //                                        for (int index = 0; index < oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments.Count; index++)
        //                                        {

        //                                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments[index] != null)
        //                                            {
        //                                                oDBParameters.Clear();

        //                                                TransactionOtherPayment _otherPayment = new TransactionOtherPayment();
        //                                                _otherPayment = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments[index];

        //                                                if (_otherPayment.OtherPayment_Type == _linePaymentDtl.Transaction_Type)
        //                                                {
        //                                                    oDBParameters.Add("@nOtherPaymentID", _otherPayment.OtherPayID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nTransactionID", _otherPayment.TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nTransactionDetailID", _otherPayment.TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nTransactionLineNo", _otherPayment.TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nAmountType", _otherPayment.OtherPayment_Type.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                                    oDBParameters.Add("@dAmount", _otherPayment.PaymentAmount, ParameterDirection.Input, SqlDbType.Decimal);
        //                                                    oDBParameters.Add("@nVisitID", _otherPayment.LineVisitID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nAppointmentID", _otherPayment.LineAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@bIsPaid", _otherPayment.IsOtherPaymentPaid, ParameterDirection.Input, SqlDbType.Bit);
        //                                                    oDBParameters.Add("@nPaymentID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nPaymentDetailID", _PaymentTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nReferenceID", _otherPayment.ReferenceID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nReplicationID", oDB.GetPrefixTransactionID(oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PatientID), ParameterDirection.Input, SqlDbType.BigInt);

        //                                                    Object _retVal = null;
        //                                                    oDB.Execute("BL_INUP_OtherPayments", oDBParameters, out _retVal);
        //                                                    oDBParameters.Clear();
        //                                                    if (_otherPayment != null) { _otherPayment.Dispose(); }
        //                                                    break;
        //                                                }

        //                                            }

        //                                        }
        //                                    }
        //                                }
        //                            }

        //                            #endregion " Add Other Payments in Transaction in (BL_Transaction_OtherPayments) tables "

        //                            #region " Add Line Payment Amount Details "

        //                            Object _AmtDetailID = null;
        //                            if (_linePaymentDtl.PayDetails != null && _linePaymentDtl.PayDetails.Count > 0)
        //                            {
        //                                for (int lamtIndex = 0; lamtIndex < _linePaymentDtl.PayDetails.Count; lamtIndex++)
        //                                {
        //                                    if (_linePaymentDtl.PayDetails[lamtIndex] != null)
        //                                    {
                                              
        //                                        //@nPaymentAmountID	numeric(18, 0) output,
        //                                        //@nPaymentTransactionID	numeric(18, 0),
        //                                        //@nPaymentTransactionDetailID	numeric(18, 0),
        //                                        //@nAmountDetailID	numeric(18, 0),
        //                                        //@dAmount	decimal(18, 2),
        //                                        //@nAmountTypeID	int,
        //                                        //@nReplicationID numeric(18,0),
        //                                        //@nBillingTransactionID	numeric(18, 0),
        //                                        //@nBillingTransactionDetailID	numeric(18, 0),
        //                                        //@nBillingTransactionLineNo	numeric(18, 0)
        //                                        oDBParameters.Clear();
        //                                        oDBParameters.Add("@nPaymentAmountID",0,ParameterDirection.InputOutput,SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nPaymentTransactionID",_PaymentTransactionID,ParameterDirection.Input,SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nPaymentTransactionDetailID",_PaymentTransactionDetailID,ParameterDirection.Input,SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nAmountDetailID",_linePaymentDtl.PayDetails[lamtIndex].PaymentID,ParameterDirection.Input,SqlDbType.BigInt);
        //                                        oDBParameters.Add("@dAmount",_linePaymentDtl.PayDetails[lamtIndex].PayAmount,ParameterDirection.Input,SqlDbType.Decimal);
        //                                        oDBParameters.Add("@nAmountTypeID",_linePaymentDtl.PayDetails[lamtIndex].PayType.GetHashCode(),ParameterDirection.Input,SqlDbType.Int);
        //                                        oDBParameters.Add("@nAppliedTypeID",_linePaymentDtl.PayDetails[lamtIndex].AppliedToPayType.GetHashCode(),ParameterDirection.Input,SqlDbType.Int);
        //                                        oDBParameters.Add("@bIsPaid", _linePaymentDtl.PayDetails[lamtIndex].IsPaid, ParameterDirection.Input, SqlDbType.Int);
        //                                        oDBParameters.Add("@nReplicationID", oDB.GetPrefixTransactionID(oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PatientID), ParameterDirection.Input, SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nBillingTransactionID",oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionID,ParameterDirection.Input,SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nBillingTransactionDetailID",oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionDetailID,ParameterDirection.Input,SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nBillingTransactionLineNo", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
        //                                        oDB.Execute("BL_INUP_PaymentAmount_DTL", oDBParameters,out _AmtDetailID);
        //                                        oDBParameters.Clear();
        //                                    }
        //                                }
        //                            }

        //                            #endregion " Add Line Payment Amount Details "

        //                        }
        //                        _linePaymentDtl = null;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException dbex)
        //    {
        //        dbex.ERROR_Log(dbex.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //        if (oDBParameters != null) { oDBParameters.Dispose(); }
        //    }
        //    return _PaymentTransactionID;
        //}

        #endregion " New method to save Payment "

        #region " Save Payment "

        #region " Commented old method "
        //public Int64 AddPaymentTransaction(PaymentTransaction oPaymentTransaction, Int64 ClinicID, Int64 PatientID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //    Int64 _PaymentTransactionID = 0;
        //    Int64 _PaymentTransactionDetailID = 0;
        //    object oPaymentTransactionID = null;
        //    object oPaymentTransactionDetailID = null;
        //    try
        //    {
        //        oDB.Connect(false);

        //        oDBParameters.Add("@nPaymentTransactionID", oPaymentTransaction.PaymentTransactionID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //        oDBParameters.Add("@nPaymentDate", oPaymentTransaction.PaymentDate, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@nPaymentTime", oPaymentTransaction.PaymentTime, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@nPaymentNo", oPaymentTransaction.PaymentTransactionNo, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@nTransactionType", oPaymentTransaction.TransactionTypeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //        oDBParameters.Add("@nPaymentMode", oPaymentTransaction.PaymentModeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //        oDBParameters.Add("@dPayerAmount", oPaymentTransaction.PayerAmount, ParameterDirection.Input, SqlDbType.Decimal);
        //        oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", oPaymentTransaction.CardNoAndCheckNoAndMoneyOrderNo, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@nCrdExpChkMnyOrdDate", oPaymentTransaction.CardExpiryAndCheckDateAndMoneyOrderDate, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@sSecurityNo", oPaymentTransaction.SecurityNo, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@sCardType", oPaymentTransaction.CardType, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@sAuthorizationNo", oPaymentTransaction.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@nCardID", oPaymentTransaction.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@nTransactionTypeDetailID", oPaymentTransaction.TransactionTypeDetailValue, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@sTransactionTypeDetailName", oPaymentTransaction.TransactionTypeDetailName, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@nPaymentTransactionStatus", oPaymentTransaction.PaymentTransactionStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //        oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@sUserName", UserName, ParameterDirection.Input, SqlDbType.VarChar);
        //        //oDBParameters.Add("@nReplicationID", oDB.GetPrefixTransactionID(PatientID), ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(PatientID), ParameterDirection.Input, SqlDbType.BigInt);


        //        oDB.Execute("BL_INUP_Transaction_Payment_MST", oDBParameters, out oPaymentTransactionID);

        //        if (oPaymentTransactionID != null && Convert.ToString(oPaymentTransactionID) != "")
        //        {
        //            _PaymentTransactionID = Convert.ToInt64(oPaymentTransactionID);

        //            for (int k = 0; k < oPaymentTransaction.TransactionPaymentClaims.Count; k++)
        //            {
        //                for (int i = 0; i < oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines.Count; i++)
        //                {
        //                    oDBParameters.Clear();
        //                    LinePaymentDetail _linePaymentDtl = null;

        //                    for (int l = 0; l < oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].LinePayment_Details.Count; l++)
        //                    {
        //                        _linePaymentDtl = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].LinePayment_Details[l];

        //                        if (_linePaymentDtl != null
        //                            && _linePaymentDtl.Transaction_Type != TransactionType.None
        //                            && _linePaymentDtl.Transaction_Type != TransactionType.Payment)
        //                        {
        //                            ////******************************************************************/////
        //                            ////***...Uncomment this condition if zero payment entry not allowed
        //                            if (_linePaymentDtl.LinePaymentAmount <= 0)
        //                            { continue; }
        //                            ////******************************************************************/////

        //                            oDBParameters.Clear();
        //                            oDBParameters.Add("@nPaymentTransactionID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nPaymentTransactionDetailID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentTransactionDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nPatientID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PatientID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nPaymentDate", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentDate, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nBillingTransactionID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nBillingTransactionDetailID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nBillingTransactionLineNo", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nClaimNo", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@sCPTCode", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CPTCode, ParameterDirection.Input, SqlDbType.VarChar);
        //                            oDBParameters.Add("@sCPTDescription", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);
        //                            oDBParameters.Add("@dAllowedAmt", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionAllowed, ParameterDirection.Input, SqlDbType.Decimal);

        //                            oDBParameters.Add("@dCurrentPaymentAmt", _linePaymentDtl.LinePaymentAmount, ParameterDirection.Input, SqlDbType.Decimal);
        //                            oDBParameters.Add("@nTransactionType", _linePaymentDtl.Transaction_Type.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                            oDBParameters.Add("@nCoPayID", 0, ParameterDirection.Input, SqlDbType.BigInt);

        //                            #region " Payment Mode Details "

        //                            if (_linePaymentDtl.PaymentMode_Detail != null)
        //                            {
        //                                oDBParameters.Add("@nCurrentPaymentAmtType", _linePaymentDtl.PaymentMode_Detail.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                oDBParameters.Add("@nPaymentMode", _linePaymentDtl.PaymentMode_Detail.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", _linePaymentDtl.PaymentMode_Detail.CheckMoneyOrderCardEFT_Number, ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@nCrdExpChkMnyOrdDate", _linePaymentDtl.PaymentMode_Detail.CheckMoneyOrderCardExpiryEFT_Date, ParameterDirection.Input, SqlDbType.BigInt);
        //                                oDBParameters.Add("@sSecurityNo", _linePaymentDtl.PaymentMode_Detail.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@sCardType", _linePaymentDtl.PaymentMode_Detail.CardType, ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@sAuthorizationNo", _linePaymentDtl.PaymentMode_Detail.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@nCardID", _linePaymentDtl.PaymentMode_Detail.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);

        //                                if (_linePaymentDtl.PaymentMode_Detail.TransactionTypeMode == TransactionType.InsuracePayment)
        //                                {
        //                                    oDBParameters.Add("@nPaymentInsuranceID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PayerModeInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@nPayerModeID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PayerModeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                }
        //                                else
        //                                {
        //                                    if (_linePaymentDtl.LinePaymentAmount == 0)
        //                                    {
        //                                        oDBParameters.Add("@nPaymentInsuranceID", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nPayerModeID", PayerMode.None.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                    }
        //                                    else
        //                                    {
        //                                        oDBParameters.Add("@nPaymentInsuranceID", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nPayerModeID", PayerMode.Self.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                    }

        //                                }

        //                                if (_linePaymentDtl.PaymentMode_Detail.TransactionTypeMode == TransactionType.Adjustment)
        //                                {
        //                                    oDBParameters.Add("@nTransactionTypeDetailID", _linePaymentDtl.PaymentMode_Detail.AdjustmentTypeID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@sTransactionTypeDetailName", _linePaymentDtl.PaymentMode_Detail.Adjustment_Type.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar);
        //                                }
        //                                else
        //                                {
        //                                    oDBParameters.Add("@nTransactionTypeDetailID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionTypeDetailValue, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@sTransactionTypeDetailName", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionTypeDetailName, ParameterDirection.Input, SqlDbType.VarChar);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                oDBParameters.Add("@nCurrentPaymentAmtType", PaymentMode.None.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                oDBParameters.Add("@nPaymentMode", PaymentMode.None.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", 0, ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@nCrdExpChkMnyOrdDate", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //                                oDBParameters.Add("@sSecurityNo", 0, ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@sCardType", "", ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@sAuthorizationNo", "", ParameterDirection.Input, SqlDbType.VarChar);
        //                                oDBParameters.Add("@nCardID", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //                                oDBParameters.Add("@nPaymentInsuranceID", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //                                oDBParameters.Add("@nPayerModeID", PayerMode.None.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                oDBParameters.Add("@nTransactionTypeDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //                                oDBParameters.Add("@sTransactionTypeDetailName", "", ParameterDirection.Input, SqlDbType.VarChar);
        //                            }

        //                            #endregion " Payment Mode Details "

        //                            oDBParameters.Add("@nPaymentTransactionLineStatus", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PaymentLineStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                            oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@sUserName", UserName, ParameterDirection.Input, SqlDbType.VarChar);
        //                            oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PatientID), ParameterDirection.Input, SqlDbType.BigInt);

        //                            oDB.Execute("BL_INUP_Transaction_Payment_DTL", oDBParameters, out oPaymentTransactionDetailID);
        //                            oDBParameters.Clear();
        //                            _PaymentTransactionDetailID = Convert.ToInt64(oPaymentTransactionDetailID);

        //                            #region " Add Note "

        //                            if (_linePaymentDtl.Payments_Notes != null && _linePaymentDtl.Payments_Notes.Count > 0)
        //                            {
        //                                if (_linePaymentDtl.Payments_Notes[0] != null)
        //                                {
        //                                    oDBParameters.Clear();
        //                                    oDBParameters.Add("@nPaymentTransactionID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@nPaymentTransactionDetailID", _PaymentTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@nPaymentNoteId", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@nNoteType", _linePaymentDtl.Payments_Notes[0].NoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                    oDBParameters.Add("@nNoteDateTime", _linePaymentDtl.Payments_Notes[0].NoteDate, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@nUserID", _linePaymentDtl.Payments_Notes[0].UserID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDBParameters.Add("@sNoteDescription", _linePaymentDtl.Payments_Notes[0].NoteDescription, ParameterDirection.Input, SqlDbType.VarChar);
        //                                    oDBParameters.Add("@nClinicID", _linePaymentDtl.Payments_Notes[0].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                    oDB.Execute("BL_INUP_Transaction_Payment_Notes", oDBParameters);
        //                                    oDBParameters.Clear();
        //                                }
        //                            }

        //                            #endregion " Add Note "

        //                            #region " Add Other Payments in Transaction in (BL_Transaction_OtherPayments) tables "

        //                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments != null
        //                              && oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments.Count > 0)
        //                            {
        //                                if (_PaymentTransactionID > 0 && _PaymentTransactionDetailID > 0)
        //                                {
        //                                    if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments.Count > 0)
        //                                    {
        //                                        for (int index = 0; index < oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments.Count; index++)
        //                                        {

        //                                            if (oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments[index] != null)
        //                                            {
        //                                                oDBParameters.Clear();

        //                                                TransactionOtherPayment _otherPayment = new TransactionOtherPayment();
        //                                                _otherPayment = oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].Transaction_OtherPayments[index];

        //                                                if (_otherPayment.OtherPayment_Type == _linePaymentDtl.Transaction_Type)
        //                                                {
        //                                                    oDBParameters.Add("@nOtherPaymentID", _otherPayment.OtherPayID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nTransactionID", _otherPayment.TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nTransactionDetailID", _otherPayment.TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nTransactionLineNo", _otherPayment.TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nAmountType", _otherPayment.OtherPayment_Type.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                                    oDBParameters.Add("@dAmount", _otherPayment.PaymentAmount, ParameterDirection.Input, SqlDbType.Decimal);
        //                                                    oDBParameters.Add("@nVisitID", _otherPayment.LineVisitID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nAppointmentID", _otherPayment.LineAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@bIsPaid", _otherPayment.IsOtherPaymentPaid, ParameterDirection.Input, SqlDbType.Bit);
        //                                                    oDBParameters.Add("@nPaymentID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nPaymentDetailID", _PaymentTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nReferenceID", _otherPayment.ReferenceID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PatientID), ParameterDirection.Input, SqlDbType.BigInt);

        //                                                    Object _retVal = null;
        //                                                    oDB.Execute("BL_INUP_OtherPayments", oDBParameters, out _retVal);
        //                                                    oDBParameters.Clear();
        //                                                    if (_otherPayment != null) { _otherPayment.Dispose(); }
        //                                                    break;
        //                                                }

        //                                            }

        //                                        }
        //                                    }
        //                                }
        //                            }

        //                            #endregion " Add Other Payments in Transaction in (BL_Transaction_OtherPayments) tables "

        //                            #region " Add Line Payment Amount Details "

        //                            Object _AmtDetailID = null;
        //                            if (_linePaymentDtl.PayDetails != null && _linePaymentDtl.PayDetails.Count > 0)
        //                            {
        //                                for (int lamtIndex = 0; lamtIndex < _linePaymentDtl.PayDetails.Count; lamtIndex++)
        //                                {
        //                                    if (_linePaymentDtl.PayDetails[lamtIndex] != null)
        //                                    {

        //                                        //@nPaymentAmountID	numeric(18, 0) output,
        //                                        //@nPaymentTransactionID	numeric(18, 0),
        //                                        //@nPaymentTransactionDetailID	numeric(18, 0),
        //                                        //@nAmountDetailID	numeric(18, 0),
        //                                        //@dAmount	decimal(18, 2),
        //                                        //@nAmountTypeID	int,
        //                                        //@MachineID numeric(18,0),
        //                                        //@nBillingTransactionID	numeric(18, 0),
        //                                        //@nBillingTransactionDetailID	numeric(18, 0),
        //                                        //@nBillingTransactionLineNo	numeric(18, 0)
        //                                        oDBParameters.Clear();
        //                                        oDBParameters.Add("@nPaymentAmountID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nPaymentTransactionID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nPaymentTransactionDetailID", _PaymentTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nAmountDetailID", _linePaymentDtl.PayDetails[lamtIndex].PaymentID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                        oDBParameters.Add("@dAmount", _linePaymentDtl.PayDetails[lamtIndex].PayAmount, ParameterDirection.Input, SqlDbType.Decimal);
        //                                        oDBParameters.Add("@nAmountTypeID", _linePaymentDtl.PayDetails[lamtIndex].PayType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                        oDBParameters.Add("@nAppliedTypeID", _linePaymentDtl.PayDetails[lamtIndex].AppliedToPayType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                        oDBParameters.Add("@bIsPaid", _linePaymentDtl.PayDetails[lamtIndex].IsPaid, ParameterDirection.Input, SqlDbType.Int);
        //                                        oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PatientID), ParameterDirection.Input, SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nBillingTransactionID",oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionID,ParameterDirection.Input,SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nBillingTransactionDetailID",oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionDetailID,ParameterDirection.Input,SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nReplicationID", oDB.GetPrefixTransactionID(oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].PatientID), ParameterDirection.Input, SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nBillingTransactionID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nBillingTransactionDetailID", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                        oDBParameters.Add("@nBillingTransactionLineNo", oPaymentTransaction.TransactionPaymentClaims[k].ClaimLines[i].TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
        //                                        oDB.Execute("BL_INUP_PaymentAmount_DTL", oDBParameters, out _AmtDetailID);
        //                                        oDBParameters.Clear();
        //                                    }
        //                                }
        //                            }

        //                            #endregion " Add Line Payment Amount Details "

        //                        }
        //                        _linePaymentDtl = null;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException dbex)
        //    {
        //        dbex.ERROR_Log(dbex.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //        if (oDBParameters != null) { oDBParameters.Dispose(); }
        //    }
        //    return _PaymentTransactionID;
        //}
        

        //public Int64 SavePayment(PaymentTransaction oPaymentTransaction, Int64 ClinicID, Int64 PatientID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //    Int64 _PaymentTransactionID = 0;
        //    Int64 _PaymentTransactionDetailID = 0;
        //    object oPaymentTransactionID = null;
        //    object oPaymentTransactionDetailID = null;
        //    bool _isPendingSave = false;

        //    try
        //    {
        //        //..Start connection using sql transaction
        //        oDB.Connect(false);

        //        #region "....Add Payment Master Table Data .... "

        //        //..Addding Payment Master table entry 
        //        //...For now we are not considering the entries for Payment Mode in 
        //        //..payment master
        //        //... If Payer Amount is not present then the entry is of pending amount

        //        if (oPaymentTransaction.PayerAmount > 0 || oPaymentTransaction.PendingPayment > 0)
        //        {
        //            oDBParameters.Add("@nPaymentTransactionID", oPaymentTransaction.PaymentTransactionID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //            oDBParameters.Add("@nPaymentDate", oPaymentTransaction.PaymentDate, ParameterDirection.Input, SqlDbType.BigInt);
        //            oDBParameters.Add("@nPaymentTime", oPaymentTransaction.PaymentTime, ParameterDirection.Input, SqlDbType.BigInt);
        //            oDBParameters.Add("@nPaymentNo", oPaymentTransaction.PaymentTransactionNo, ParameterDirection.Input, SqlDbType.BigInt);
        //            oDBParameters.Add("@nTransactionType", oPaymentTransaction.TransactionTypeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    
        //            if (oPaymentTransaction.PayerAmount > 0)
        //            { oDBParameters.Add("@dPayerAmount", oPaymentTransaction.PayerAmount, ParameterDirection.Input, SqlDbType.Decimal); }
        //            else if (oPaymentTransaction.PendingPayment > 0)
        //            { oDBParameters.Add("@dPayerAmount", oPaymentTransaction.PendingPayment, ParameterDirection.Input, SqlDbType.Decimal); }
        //            else
        //            { oDBParameters.Add("@dPayerAmount", 0, ParameterDirection.Input, SqlDbType.Decimal); }

        //            #region " Master payment mode entry "

        //            if (oPaymentTransaction.PaymentModeDetail != null && oPaymentTransaction.PaymentModeDetail.PaymentMode != PaymentMode.None)
        //            {
        //                oDBParameters.Add("@nPaymentMode", oPaymentTransaction.PaymentModeDetail.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardEFT_Number, ParameterDirection.Input, SqlDbType.VarChar);
        //                oDBParameters.Add("@nCrdExpChkMnyOrdDate", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardExpiryEFT_Date, ParameterDirection.Input, SqlDbType.BigInt);
        //                oDBParameters.Add("@sSecurityNo", oPaymentTransaction.PaymentModeDetail.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
        //                oDBParameters.Add("@sCardType", oPaymentTransaction.PaymentModeDetail.CardType, ParameterDirection.Input, SqlDbType.VarChar);
        //                oDBParameters.Add("@sAuthorizationNo", oPaymentTransaction.PaymentModeDetail.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
        //                oDBParameters.Add("@nCardID", oPaymentTransaction.PaymentModeDetail.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);
        //            }

        //            //oDBParameters.Add("@nPaymentMode", oPaymentTransaction.PaymentModeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //            //oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", oPaymentTransaction.CardNoAndCheckNoAndMoneyOrderNo, ParameterDirection.Input, SqlDbType.VarChar);
        //            //oDBParameters.Add("@nCrdExpChkMnyOrdDate", oPaymentTransaction.CardExpiryAndCheckDateAndMoneyOrderDate, ParameterDirection.Input, SqlDbType.BigInt);
        //            //oDBParameters.Add("@sSecurityNo", oPaymentTransaction.SecurityNo, ParameterDirection.Input, SqlDbType.VarChar);
        //            //oDBParameters.Add("@sCardType", oPaymentTransaction.CardType, ParameterDirection.Input, SqlDbType.VarChar);
        //            //oDBParameters.Add("@sAuthorizationNo", oPaymentTransaction.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
        //            //oDBParameters.Add("@nCardID", oPaymentTransaction.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);

        //            #endregion " Master payment mode entry "

        //            oDBParameters.Add("@nTransactionTypeDetailID", oPaymentTransaction.TransactionTypeDetailValue, ParameterDirection.Input, SqlDbType.BigInt);
        //            oDBParameters.Add("@sTransactionTypeDetailName", oPaymentTransaction.TransactionTypeDetailName, ParameterDirection.Input, SqlDbType.VarChar);
        //            oDBParameters.Add("@nPaymentTransactionStatus", oPaymentTransaction.PaymentTransactionStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //            oDBParameters.Add("@nClinicID", oPaymentTransaction.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //            oDBParameters.Add("@nUserID", oPaymentTransaction.UserID, ParameterDirection.Input, SqlDbType.BigInt);
        //            oDBParameters.Add("@sUserName", oPaymentTransaction.UserName, ParameterDirection.Input, SqlDbType.VarChar);
        //            //oDBParameters.Add("@nReplicationID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
        //            oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

        //            oDB.Execute("BL_INUP_Transaction_Payment_MST", oDBParameters, out oPaymentTransactionID);
        //        }
        //        else
        //        {
        //            _isPendingSave = true;
        //        }

        //        #endregion "....Add Payment Master Table Data .... "

        //        //if (oPaymentTransactionID != null && Convert.ToString(oPaymentTransactionID) != "")
        //        if ((oPaymentTransactionID != null && Convert.ToString(oPaymentTransactionID) != "") || (_isPendingSave == true))
        //        {
        //            _PaymentTransactionID = Convert.ToInt64(oPaymentTransactionID);

        //            if (oPaymentTransaction.TransactionPaymentClaims != null && oPaymentTransaction.TransactionPaymentClaims.Count > 0)
        //            {
        //                //..** Read Payment Claim collection containing Claim Lines 
        //                for (int k = 0; k < oPaymentTransaction.TransactionPaymentClaims.Count; k++)
        //                {
        //                    if (oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines != null && oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines.Count > 0)
        //                    {
        //                        ClaimLines _claimLines = null;
        //                        ClaimLine _claimLine = null;
        //                        _claimLines = oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines;

        //                        for (int l = 0; l < _claimLines.Count; l++)
        //                        {
        //                            _claimLine = _claimLines[l];

        //                            if (_claimLine.ClaimLine_Payments != null && _claimLine.ClaimLine_Payments.Count > 0)
        //                            {
        //                                ClaimLinePayment _claimLinePayment = null;

        //                                for (int m = 0; m < _claimLine.ClaimLine_Payments.Count; m++)
        //                                {
        //                                    _claimLinePayment = _claimLine.ClaimLine_Payments[m];

        //                                    //if (_claimLinePayment != null && _claimLinePayment.PaymentDetails != null && _claimLinePayment.PaymentDetails.Count > 0)
        //                                    if (_claimLinePayment != null && _claimLinePayment.PaymentDetails != null)
        //                                    {
        //                                        ClaimLinePaymentDetail _clmLnPayDtl = null;
        //                                        bool _isChargedTransactionSaved = false;

        //                                        for (int n = 0; n < _claimLinePayment.PaymentDetails.Count; n++)
        //                                        {
        //                                            _clmLnPayDtl = _claimLinePayment.PaymentDetails[n];

        //                                            if (_clmLnPayDtl != null)
        //                                            {
        //                                                #region " Make Payment Detail Table entries "

        //                                                oDBParameters.Clear();

        //                                                //@nPaymentTransactionID numeric(18, 0),@nPaymentTransactionDetailID numeric(18, 0) OUTPUT,  
        //                                                //@nPatientID numeric(18, 0),@nPaymentDate numeric(18, 0),
        //                                                //@nBillingTransactionID numeric(18, 0),@nBillingTransactionDetailID numeric(18, 0),  
        //                                                //@nBillingTransactionLineNo numeric(18, 0),@nClaimNo numeric(18, 0),  
        //                                                //@sCPTCode varchar(255),@sCPTDescription varchar(255),@dAllowedAmt decimal(18, 2),  

        //                                                //@dCurrentPaymentAmt decimal(18, 2),@nCurrentPaymentAmtType int,@nTransactionType int,  
        //                                                //@nPaymentMode int,@sCrdNoMnyOrdNoChkNo varchar(255),@nCrdExpChkMnyOrdDate numeric(18, 0),  
        //                                                //@sSecurityNo varchar(255),@sCardType varchar(255),@nPaymentInsuranceID numeric(18, 0),  
        //                                                //@nPayerModeID int,@nPaymentTransactionLineStatus int,@nClinicID numeric(18, 0),  
        //                                                //@nCoPayID numeric (18,0),@nTransactionTypeDetailID numeric(18,0), 
        //                                                //@sTransactionTypeDetailName varchar(255),@sAuthorizationNo varchar(255),  
        //                                                //@nCardID numeric(18,0),@nUserID numeric(18,0),@sUserName varchar(255),  
        //                                                //@nReplicationID numeric(18,0)  


        //                                                oDBParameters.Add("@nPaymentTransactionID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nPaymentTransactionDetailID", _claimLines[l].PaymentTransactionDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nPatientID", _claimLines[l].PatientID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nPaymentDate", _claimLines[l].PaymentDate, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nBillingTransactionID", _claimLines[l].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nBillingTransactionDetailID", _claimLines[l].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nBillingTransactionLineNo", _claimLines[l].TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nClaimNo", _claimLines[l].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@sCPTCode", _claimLines[l].CPTCode, ParameterDirection.Input, SqlDbType.VarChar);
        //                                                oDBParameters.Add("@sCPTDescription", _claimLines[l].CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);
        //                                                oDBParameters.Add("@dAllowedAmt", _claimLines[l].TransactionAllowed, ParameterDirection.Input, SqlDbType.Decimal);

        //                                                oDBParameters.Add("@nTransactionType", _claimLines[l].ClaimLine_Payments[m].Transaction_Type.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                                oDBParameters.Add("@dCurrentPaymentAmt", _clmLnPayDtl.PayAmount, ParameterDirection.Input, SqlDbType.Decimal);
        //                                                oDBParameters.Add("@nCurrentPaymentAmtType", _clmLnPayDtl.PayAmountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                                //....**** Note
        //                                                //....@nCoPayID field contains the ids of applied copay,advance payment
        //                                                oDBParameters.Add("@nCoPayID", _clmLnPayDtl.PaymentID, ParameterDirection.Input, SqlDbType.BigInt);

        //                                                if (_claimLines[l].ClaimLine_Payments[m].Transaction_Type == TransactionType.InsuracePayment && _clmLnPayDtl.AmountPaymentMode.PaymentMode == PaymentMode.None)
        //                                                {

        //                                                    //..if line payment mode not present enter the master payment mode
        //                                                    oDBParameters.Add("@nPaymentMode", oPaymentTransaction.PaymentModeDetail.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                                    oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardEFT_Number, ParameterDirection.Input, SqlDbType.VarChar);
        //                                                    oDBParameters.Add("@nCrdExpChkMnyOrdDate", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardExpiryEFT_Date, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@sSecurityNo", oPaymentTransaction.PaymentModeDetail.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
        //                                                    oDBParameters.Add("@sCardType", oPaymentTransaction.PaymentModeDetail.CardTypeID, ParameterDirection.Input, SqlDbType.VarChar);
        //                                                    oDBParameters.Add("@sAuthorizationNo", oPaymentTransaction.PaymentModeDetail.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
        //                                                    oDBParameters.Add("@nCardID", oPaymentTransaction.PaymentModeDetail.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                }
        //                                                else
        //                                                {
        //                                                    oDBParameters.Add("@nPaymentMode", _clmLnPayDtl.AmountPaymentMode.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                                    oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", _clmLnPayDtl.AmountPaymentMode.CheckMoneyOrderCardEFT_Number, ParameterDirection.Input, SqlDbType.VarChar);
        //                                                    oDBParameters.Add("@nCrdExpChkMnyOrdDate", _clmLnPayDtl.AmountPaymentMode.CheckMoneyOrderCardExpiryEFT_Date, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@sSecurityNo", _clmLnPayDtl.AmountPaymentMode.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
        //                                                    oDBParameters.Add("@sCardType", _clmLnPayDtl.AmountPaymentMode.CardType, ParameterDirection.Input, SqlDbType.VarChar);
        //                                                    oDBParameters.Add("@sAuthorizationNo", _clmLnPayDtl.AmountPaymentMode.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
        //                                                    oDBParameters.Add("@nCardID", _clmLnPayDtl.AmountPaymentMode.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                }

        //                                                //....**** Note
        //                                                //....** Any kind of adjustment made the adjustment id & the description for 
        //                                                //.... adjustment type goes in this table
        //                                                //....@nTransactionTypeDetailID 
        //                                                //....@sTransactionTypeDetailName
        //                                                oDBParameters.Add("@nTransactionTypeDetailID", _clmLnPayDtl.AmountPaymentMode.AdjustmentTypeID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@sTransactionTypeDetailName", _clmLnPayDtl.AmountPaymentMode.Adjustment_Type.ToString(), ParameterDirection.Input, SqlDbType.VarChar); //

        //                                                oDBParameters.Add("@nUserID", oPaymentTransaction.UserID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@sUserName", oPaymentTransaction.UserName, ParameterDirection.Input, SqlDbType.VarChar);
        //                                                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt); // numeric(18,0)  
        //                                                oDBParameters.Add("@nPaymentTransactionLineStatus", oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines[l].PaymentLineStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

        //                                                //oDBParameters.Add("@nPaymentInsuranceID", _claimLines[l].ClaimLine_Payments[m].InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nPaymentInsuranceID", oPaymentTransaction.InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);

        //                                                //...** Code chages done on 20090702 by - Sagar Ghodke
        //                                                //...

        //                                                //if (_claimLines[l].ClaimLine_Payments[m].InsuranceID > 0)
        //                                                //{ oDBParameters.Add("@nPayerModeID", PayerMode.Insurance.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }
        //                                                //else
        //                                                //{ oDBParameters.Add("@nPayerModeID", PayerMode.Self.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }

        //                                                //...Check if the transaction type is insurance then set 

        //                                                if (_claimLinePayment.Transaction_Type == TransactionType.InsuracePayment)
        //                                                { oDBParameters.Add("@nPayerModeID", PayerMode.Insurance.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }
        //                                                else if (_claimLinePayment.Transaction_Type == TransactionType.Coinsurance
        //                                                    || _claimLinePayment.Transaction_Type == TransactionType.Copay
        //                                                    || _claimLinePayment.Transaction_Type == TransactionType.Deductible
        //                                                    || _claimLinePayment.Transaction_Type == TransactionType.Coverage)
        //                                                { oDBParameters.Add("@nPayerModeID", PayerMode.Self.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }
        //                                                else
        //                                                { oDBParameters.Add("@nPayerModeID", PayerMode.None.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }
                                                    

        //                                                //if (_claimLines[l].ClaimLine_Payments[m].InsuranceID > 0)
        //                                                //{ oDBParameters.Add("@nPayerModeID", PayerMode.Insurance.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }
        //                                                //else
        //                                                //{ oDBParameters.Add("@nPayerModeID", PayerMode.Self.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }

        //                                                //...** End code changes 20090702 - Sagar Ghodke

        //                                                oDBParameters.Add("@nClinicID", _claimLines[l].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

        //                                                oDB.Execute("BL_INUP_Transaction_Payment_DTL", oDBParameters, out oPaymentTransactionDetailID);
        //                                                oDBParameters.Clear();
        //                                                _PaymentTransactionDetailID = Convert.ToInt64(oPaymentTransactionDetailID);

        //                                                #endregion " Make Payment Detail Table entries "

        //                                                #region " Set fully applied copay & advance flag "

        //                                                if (_clmLnPayDtl.PaymentID > 0 && _PaymentTransactionDetailID > 0)
        //                                                {
        //                                                    oDBParameters.Clear();
        //                                                    oDBParameters.Add("@nAdvPayID", _clmLnPayDtl.PaymentID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    oDBParameters.Add("@nClinicID", _claimLines[l].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                    int _isUpdated = 0;
        //                                                    _isUpdated = oDB.Execute("BL_AdvancePayment_SetIsApplied", oDBParameters);
        //                                                    oDBParameters.Clear();
        //                                                } 

        //                                                #endregion
        //                                            }
        //                                        }

        //                                        #region " Make Transaction Entry "

        //                                        if (_claimLinePayment.ChargedAmount > 0)
        //                                        {
        //                                            oDBParameters.Clear();

        //                                            oDBParameters.Add("@nOtherPaymentID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //                                            oDBParameters.Add("@nTransactionID", _claimLines[l].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                            oDBParameters.Add("@nTransactionDetailID", _claimLines[l].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                            oDBParameters.Add("@nTransactionLineNo", _claimLines[l].TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
        //                                            oDBParameters.Add("@nAmountType", _claimLines[l].ClaimLine_Payments[m].Transaction_Type.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                            oDBParameters.Add("@dAmount", _claimLines[l].ClaimLine_Payments[m].ChargedAmount, ParameterDirection.Input, SqlDbType.Decimal);
        //                                            oDBParameters.Add("@nVisitID", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //                                            oDBParameters.Add("@nAppointmentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
        //                                            oDBParameters.Add("@bIsPaid", DBNull.Value, ParameterDirection.Input, SqlDbType.Bit);
        //                                            oDBParameters.Add("@nPaymentID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                            oDBParameters.Add("@nPaymentDetailID", _PaymentTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                                                    
        //                                            //..ReferenceID = InsuranceID against which payment is being made
        //                                            oDBParameters.Add("@nReferenceID", oPaymentTransaction.InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                            oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

        //                                            Object _retVal = null;
        //                                            oDB.Execute("BL_INUP_OtherPayments", oDBParameters, out _retVal);
        //                                            oDBParameters.Clear();
        //                                           // _isChargedTransactionSaved = true;
        //                                        }

        //                                        #endregion " Make Transaction Entry "

        //                                        #region " Add Note "

        //                                        if (_claimLinePayment.Notes != null && _claimLinePayment.Notes.Count > 0)
        //                                        {
        //                                            if (_claimLinePayment.Notes[0] != null)
        //                                            {
        //                                                oDBParameters.Clear();
        //                                                oDBParameters.Add("@nPaymentTransactionID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nPaymentTransactionDetailID", _PaymentTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nPaymentNoteId", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nNoteType", _claimLinePayment.Notes[0].NoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //                                                oDBParameters.Add("@nNoteDateTime", _claimLinePayment.Notes[0].NoteDate, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@nUserID", _claimLinePayment.Notes[0].UserID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@sNoteDescription", _claimLinePayment.Notes[0].NoteDescription, ParameterDirection.Input, SqlDbType.VarChar);
        //                                                oDBParameters.Add("@nClinicID", _claimLinePayment.Notes[0].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
        //                                                oDB.Execute("BL_INUP_Transaction_Payment_Notes", oDBParameters);
        //                                                oDBParameters.Clear();
        //                                            }
        //                                        }

        //                                        #endregion " Add Note "

        //                                    }
        //                                }

        //                            }
        //                        }

        //                    }//END IF .....if (oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines != null && oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines.Count > 0)
        //                    else
        //                    {
        //                        //...** If no lines found Rollback Transaction
        //                        //oDB.Rollback();
        //                    }

        //                }//END FOR...for (int k = 0; k < oPaymentTransaction.TransactionPaymentClaims.Count; k++)

        //            } //END IF....if (oPaymentTransaction.TransactionPaymentClaims != null && oPaymentTransaction.TransactionPaymentClaims.Count > 0)
        //            else
        //            {
        //                //...** If no lines found Rollback Transaction
        //                //oDB.Rollback();
        //            }

        //        }//END IF .....if (oPaymentTransactionID != null && Convert.ToString(oPaymentTransactionID) != "")

        //        oDB.Disconnect();
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
        //    }
        //    return _PaymentTransactionID;
        //}

        #endregion " Commented old method "

        public Int64 SavePaymentTransaction(PaymentTransaction oPaymentTransaction, Int64 ClinicID, Int64 PatientID, bool SaveFlag)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Int64 _MultiplePaymentTransactionID = 0;
            Int64 _PaymentTransactionID = 0;
            Int64 _PaymentTransactionDetailID = 0;
            object oMultiplePaymentTransactionID = null;
            object oPaymentTransactionID = null;
            object oPaymentTransactionDetailID = null;
            object oPaymentTransactionServiceLineID = null;
         //   bool _isPendingSave = false;

            try
            {
                //..Start connection using sql transaction
                oDB.Connect(false);

                #region "....Add Payment Master Multiple Payment Table Data .... "

                //..Addding Payment Master table entry 
                //...For now we are not considering the entries for Payment Mode in 
                //..payment master
                //... If Payer Amount is not present then the entry is of pending amount

                //if (oPaymentTransaction.PayerAmount > 0)
                //{
                    oDBParameters.Add("@nMultiplePaymentTransactionID", oPaymentTransaction.MultiplePaymentTransactionID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@nPaymentDate", oPaymentTransaction.PaymentDate, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nPaymentTime", oPaymentTransaction.PaymentTime, ParameterDirection.Input, SqlDbType.BigInt);

                    if (oPaymentTransaction.PayerAmount > 0)
                    { oDBParameters.Add("@dPayerAmount", oPaymentTransaction.PayerAmount, ParameterDirection.Input, SqlDbType.Decimal); }
                    //else if (oPaymentTransaction.PendingPayment > 0)
                    else if(oPaymentTransaction.PatientPayment > 0)
                    { oDBParameters.Add("@dPayerAmount", oPaymentTransaction.PatientPayment, ParameterDirection.Input, SqlDbType.Decimal); }
                    else
                    { oDBParameters.Add("@dPayerAmount", 0, ParameterDirection.Input, SqlDbType.Decimal); }

                    #region " Master payment mode entry "

                    if (oPaymentTransaction.PaymentModeDetail != null && oPaymentTransaction.PaymentModeDetail.PaymentMode != PaymentMode.None)
                    {
                        oDBParameters.Add("@nPaymentMode", oPaymentTransaction.PaymentModeDetail.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                        oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardEFT_Number, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@nCrdExpChkMnyOrdDate", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardExpiryEFT_Date, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@sSecurityNo", oPaymentTransaction.PaymentModeDetail.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sCardType", oPaymentTransaction.PaymentModeDetail.CardType, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sAuthorizationNo", oPaymentTransaction.PaymentModeDetail.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@nCardID", oPaymentTransaction.PaymentModeDetail.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);
                    }

                    #endregion " Master payment mode entry "

                    oDBParameters.Add("@nClinicID", oPaymentTransaction.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nUserID", oPaymentTransaction.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sUserName", oPaymentTransaction.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                    oDBParameters.Add("@nPaymentInsuranceID",oPaymentTransaction.InsuranceID,ParameterDirection.Input,SqlDbType.BigInt);
	                oDBParameters.Add("@sPaymentInsurance",oPaymentTransaction.InsuranceName,ParameterDirection.Input,SqlDbType.VarChar);

                    oDBParameters.Add("@nMasterTransactionType", oPaymentTransaction.MasterTransactoinType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                    oDB.Execute("BL_INUP_Transaction_Payment_MST_MultiPayment", oDBParameters, out oMultiplePaymentTransactionID);
                //}
                
                #endregion "....Add Payment Master Table Data .... "

                    if ((oMultiplePaymentTransactionID != null && Convert.ToString(oMultiplePaymentTransactionID) != "" && Convert.ToString(oMultiplePaymentTransactionID) != "0"))
                    {
                        _MultiplePaymentTransactionID = Convert.ToInt64(oMultiplePaymentTransactionID);

                        if (oPaymentTransaction.TransactionPaymentClaims != null && oPaymentTransaction.TransactionPaymentClaims.Count > 0)
                        {
                            //..** Read Payment Claim collection containing Claim Lines 
                            for (int k = 0; k < oPaymentTransaction.TransactionPaymentClaims.Count; k++)
                            {
                                #region "Add Payment Transaction Master Table"
                                oDBParameters.Clear();

                                //Code changes done on 20090831 by - Sagar Ghodke
                                //Below commented line is previous line of code

                                oDBParameters.Add("@nPaymentTransactionID", oPaymentTransaction.TransactionPaymentClaims[k].PaymentTransactionID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                //oDBParameters.Add("@nPaymentTransactionID", oPaymentTransaction.TransactionPaymentClaims[k].PaymentTransactionNo, ParameterDirection.InputOutput, SqlDbType.BigInt);

                                //End code changes 20090831,Sagar Ghodke

                                oDBParameters.Add("@nPaymentDate", oPaymentTransaction.TransactionPaymentClaims[k].PaymentDate, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nPaymentTime", oPaymentTransaction.TransactionPaymentClaims[k].PaymentTime, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nPaymentNo", oPaymentTransaction.TransactionPaymentClaims[k].PaymentTransactionNo, ParameterDirection.Input, SqlDbType.BigInt);
                                //oDBParameters.Add("@nTransactionType", TransactionType.Payment.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nTransactionType", oPaymentTransaction.MasterTransactoinType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                                oDBParameters.Add("@dPayerAmount", oPaymentTransaction.TransactionPaymentClaims[k].TotalPayerAmount, ParameterDirection.Input, SqlDbType.Decimal);
                                //oDBParameters.Add("@dPayerAmount", oPaymentTransaction.PayerAmount, ParameterDirection.Input, SqlDbType.Decimal);

                                #region " Master payment mode entry "

                                if (oPaymentTransaction.PaymentModeDetail != null && oPaymentTransaction.PaymentModeDetail.PaymentMode != PaymentMode.None)
                                {
                                    oDBParameters.Add("@nPaymentMode", oPaymentTransaction.PaymentModeDetail.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardEFT_Number, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDBParameters.Add("@nCrdExpChkMnyOrdDate", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardExpiryEFT_Date, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@sSecurityNo", oPaymentTransaction.PaymentModeDetail.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDBParameters.Add("@sCardType", oPaymentTransaction.PaymentModeDetail.CardType, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDBParameters.Add("@sAuthorizationNo", oPaymentTransaction.PaymentModeDetail.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDBParameters.Add("@nCardID", oPaymentTransaction.PaymentModeDetail.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);
                                }

                                #endregion " Master payment mode entry "

                                oDBParameters.Add("@nTransactionTypeDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sTransactionTypeDetailName", "", ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nPaymentTransactionStatus", oPaymentTransaction.TransactionPaymentClaims[k].PaymentTransactionStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nClinicID", oPaymentTransaction.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nUserID", oPaymentTransaction.TransactionPaymentClaims[k].UserID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sUserName", oPaymentTransaction.TransactionPaymentClaims[k].UserName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                                oDBParameters.Add("@nCloseDayTrayID", oPaymentTransaction.TransactionPaymentClaims[k].CloseDayTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sCloseDayCode", oPaymentTransaction.TransactionPaymentClaims[k].CloseDayTrayCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@sCloseDayTrayDescription", oPaymentTransaction.TransactionPaymentClaims[k].CloseDayTrayName, ParameterDirection.Input, SqlDbType.VarChar);

                                oDBParameters.Add("@nMultiPaymentTransactionID", _MultiplePaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nPatientID", oPaymentTransaction.TransactionPaymentClaims[k].PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nClaimNo", oPaymentTransaction.TransactionPaymentClaims[k].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nBillingTransactionID", oPaymentTransaction.TransactionPaymentClaims[k].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);

                                //...Code changes 200090924
                                //oDBParameters.Add("@nPaymentInsuranceID",oPaymentTransaction.InsuranceID,ParameterDirection.Input,SqlDbType.BigInt);
                                //oDBParameters.Add("@sPaymentInsurance",oPaymentTransaction.InsuranceName,ParameterDirection.Input,SqlDbType.VarChar);
                                oDBParameters.Add("@nPaymentInsuranceID", oPaymentTransaction.TransactionPaymentClaims[k].PaymentInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sPaymentInsurance", oPaymentTransaction.TransactionPaymentClaims[k].PaymentInsuranceName, ParameterDirection.Input, SqlDbType.VarChar);
                                //...End code changes 20090924

                                oDBParameters.Add("@nReceivedPaymentCounter", oPaymentTransaction.TransactionPaymentClaims[k].ReceivedPaymentCounter, ParameterDirection.Input, SqlDbType.BigInt);

                                oDBParameters.Add("@nRefundTypeId",oPaymentTransaction.RefundID,ParameterDirection.Input,SqlDbType.BigInt);//  numeric(18,0),
	                            oDBParameters.Add("@sRefundTypeCode",oPaymentTransaction.RefundCode,ParameterDirection.Input,SqlDbType.VarChar);//  varchar(255),
	                            oDBParameters.Add("@sRefundTypeDesc",oPaymentTransaction.RefundDesc,ParameterDirection.Input,SqlDbType.VarChar);//  varchar(255)

                                oDB.Execute("BL_INUP_Transaction_Payment_MST", oDBParameters, out oPaymentTransactionID);

                                #endregion

                                if (oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines != null && oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines.Count > 0)
                                {

                                        #region "Delete Transaction which is open for modify"
                                        //if (SaveFlag == false)
                                        //{
                                            DeletePayment(Convert.ToInt64(oPaymentTransactionID), oPaymentTransaction.TransactionPaymentClaims[k].ClaimNo, oPaymentTransaction.TransactionPaymentClaims[k].TransactionID, oPaymentTransaction.TransactionPaymentClaims[k].PatientID, oPaymentTransaction.ClinicID);
                                        //}
                                        #endregion

                                        if (oPaymentTransactionID != null && Convert.ToString(oPaymentTransactionID) != "" && Convert.ToString(oPaymentTransactionID) != "0")
                                        {
                                            _PaymentTransactionID = Convert.ToInt64(oPaymentTransactionID);

                                            ClaimLines _claimLines = null;
                                            ClaimLine _claimLine = null;
                                            Int64 _paymentTranServiceLineId = 0;
                                            bool _generateTranServiceLineId = false;

                                            _claimLines = oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines;

                                            #region "Individual Service Line Payment against individual claim"
                                            for (int l = 0; l < _claimLines.Count; l++)
                                            {
                                                _claimLine = _claimLines[l];

                                                _generateTranServiceLineId = true;
                                                _paymentTranServiceLineId = 0;
                                                oPaymentTransactionServiceLineID = null;

                                                if (_claimLine.ClaimLine_Payments != null && _claimLine.ClaimLine_Payments.Count > 0)
                                                {
                                                    ClaimLinePayment _claimLinePayment = null;

                                                    for (int m = 0; m < _claimLine.ClaimLine_Payments.Count; m++)
                                                    {
                                                        _claimLinePayment = _claimLine.ClaimLine_Payments[m];

                                                        //if (_claimLinePayment != null && _claimLinePayment.PaymentDetails != null && _claimLinePayment.PaymentDetails.Count > 0)
                                                        if (_claimLinePayment != null && _claimLinePayment.PaymentDetails != null)
                                                        {
                                                            ClaimLinePaymentDetail _clmLnPayDtl = null;
                                                         //   bool _isChargedTransactionSaved = false;

                                                            for (int n = 0; n < _claimLinePayment.PaymentDetails.Count; n++)
                                                            {
                                                                _clmLnPayDtl = _claimLinePayment.PaymentDetails[n];

                                                                if (_clmLnPayDtl != null)
                                                                {
                                                                    #region " Make Payment Detail Table entries "

                                                                    oDBParameters.Clear();

                                                                    //@nPaymentTransactionID numeric(18, 0),@nPaymentTransactionDetailID numeric(18, 0) OUTPUT,  
                                                                    //@nPatientID numeric(18, 0),@nPaymentDate numeric(18, 0),
                                                                    //@nBillingTransactionID numeric(18, 0),@nBillingTransactionDetailID numeric(18, 0),  
                                                                    //@nBillingTransactionLineNo numeric(18, 0),@nClaimNo numeric(18, 0),  
                                                                    //@sCPTCode varchar(255),@sCPTDescription varchar(255),@dAllowedAmt decimal(18, 2),  

                                                                    //@dCurrentPaymentAmt decimal(18, 2),@nCurrentPaymentAmtType int,@nTransactionType int,  
                                                                    //@nPaymentMode int,@sCrdNoMnyOrdNoChkNo varchar(255),@nCrdExpChkMnyOrdDate numeric(18, 0),  
                                                                    //@sSecurityNo varchar(255),@sCardType varchar(255),@nPaymentInsuranceID numeric(18, 0),  
                                                                    //@nPayerModeID int,@nPaymentTransactionLineStatus int,@nClinicID numeric(18, 0),  
                                                                    //@nCoPayID numeric (18,0),@nTransactionTypeDetailID numeric(18,0), 
                                                                    //@sTransactionTypeDetailName varchar(255),@sAuthorizationNo varchar(255),  
                                                                    //@nCardID numeric(18,0),@nUserID numeric(18,0),@sUserName varchar(255),  
                                                                    //@nReplicationID numeric(18,0)  


                                                                    oDBParameters.Add("@nPaymentTransactionID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nPaymentTransactionDetailID", _claimLines[l].PaymentTransactionDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nPatientID", _claimLines[l].PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nPaymentDate", _claimLines[l].PaymentDate, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nBillingTransactionID", _claimLines[l].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nBillingTransactionDetailID", _claimLines[l].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nBillingTransactionLineNo", _claimLines[l].TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nClaimNo", _claimLines[l].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@sCPTCode", _claimLines[l].CPTCode, ParameterDirection.Input, SqlDbType.VarChar);
                                                                    oDBParameters.Add("@sCPTDescription", _claimLines[l].CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                                                    oDBParameters.Add("@dAllowedAmt", _claimLines[l].TransactionAllowed, ParameterDirection.Input, SqlDbType.Decimal);

                                                                    oDBParameters.Add("@nTransactionType", _claimLines[l].ClaimLine_Payments[m].Transaction_Type.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                                                    oDBParameters.Add("@dCurrentPaymentAmt", _clmLnPayDtl.PayAmount, ParameterDirection.Input, SqlDbType.Decimal);
                                                                    oDBParameters.Add("@nCurrentPaymentAmtType", _clmLnPayDtl.PayAmountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                                                    //....**** Note
                                                                    //....@nCoPayID field contains the ids of applied copay,advance payment
                                                                    oDBParameters.Add("@nCoPayID", _clmLnPayDtl.PaymentID, ParameterDirection.Input, SqlDbType.BigInt);

                                                                    if (_claimLines[l].ClaimLine_Payments[m].Transaction_Type == TransactionType.InsuracePayment && _clmLnPayDtl.AmountPaymentMode.PaymentMode == PaymentMode.None)
                                                                    {

                                                                        //..if line payment mode not present enter the master payment mode
                                                                        oDBParameters.Add("@nPaymentMode", oPaymentTransaction.PaymentModeDetail.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                                                        oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardEFT_Number, ParameterDirection.Input, SqlDbType.VarChar);
                                                                        oDBParameters.Add("@nCrdExpChkMnyOrdDate", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardExpiryEFT_Date, ParameterDirection.Input, SqlDbType.BigInt);
                                                                        oDBParameters.Add("@sSecurityNo", oPaymentTransaction.PaymentModeDetail.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
                                                                        oDBParameters.Add("@sCardType", oPaymentTransaction.PaymentModeDetail.CardTypeID, ParameterDirection.Input, SqlDbType.VarChar);
                                                                        oDBParameters.Add("@sAuthorizationNo", oPaymentTransaction.PaymentModeDetail.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
                                                                        oDBParameters.Add("@nCardID", oPaymentTransaction.PaymentModeDetail.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    }
                                                                    else
                                                                    {
                                                                        oDBParameters.Add("@nPaymentMode", _clmLnPayDtl.AmountPaymentMode.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                                                        oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", _clmLnPayDtl.AmountPaymentMode.CheckMoneyOrderCardEFT_Number, ParameterDirection.Input, SqlDbType.VarChar);
                                                                        oDBParameters.Add("@nCrdExpChkMnyOrdDate", _clmLnPayDtl.AmountPaymentMode.CheckMoneyOrderCardExpiryEFT_Date, ParameterDirection.Input, SqlDbType.BigInt);
                                                                        oDBParameters.Add("@sSecurityNo", _clmLnPayDtl.AmountPaymentMode.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
                                                                        oDBParameters.Add("@sCardType", _clmLnPayDtl.AmountPaymentMode.CardType, ParameterDirection.Input, SqlDbType.VarChar);
                                                                        oDBParameters.Add("@sAuthorizationNo", _clmLnPayDtl.AmountPaymentMode.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
                                                                        oDBParameters.Add("@nCardID", _clmLnPayDtl.AmountPaymentMode.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    }

                                                                    //....**** Note
                                                                    //....** Any kind of adjustment made the adjustment id & the description for 
                                                                    //.... adjustment type goes in this table
                                                                    //....@nTransactionTypeDetailID 
                                                                    //....@sTransactionTypeDetailName
                                                                    oDBParameters.Add("@nTransactionTypeDetailID", _clmLnPayDtl.AmountPaymentMode.AdjustmentTypeID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@sTransactionTypeDetailName", _clmLnPayDtl.AmountPaymentMode.Adjustment_Type.ToString(), ParameterDirection.Input, SqlDbType.VarChar); //

                                                                    oDBParameters.Add("@nUserID", oPaymentTransaction.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@sUserName", oPaymentTransaction.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                                                                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt); // numeric(18,0)  
                                                                    oDBParameters.Add("@nPaymentTransactionLineStatus", oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines[l].PaymentLineStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                                                                    //oDBParameters.Add("@nPaymentInsuranceID", _claimLines[l].ClaimLine_Payments[m].InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);


                                                                    //oDBParameters.Add("@nPaymentInsuranceID", oPaymentTransaction.InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    //oDBParameters.Add("@nPaymentInsuranceID", oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines[l].PaymentInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nPaymentInsuranceID", oPaymentTransaction.TransactionPaymentClaims[k].PaymentInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);



                                                                    //...** Code chages done on 20090702 by - Sagar Ghodke
                                                                    //...

                                                                    //if (_claimLines[l].ClaimLine_Payments[m].InsuranceID > 0)
                                                                    //{ oDBParameters.Add("@nPayerModeID", PayerMode.Insurance.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }
                                                                    //else
                                                                    //{ oDBParameters.Add("@nPayerModeID", PayerMode.Self.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }

                                                                    //...Check if the transaction type is insurance then set 

                                                                    if (_claimLinePayment.Transaction_Type == TransactionType.InsuracePayment)
                                                                    { oDBParameters.Add("@nPayerModeID", PayerMode.Insurance.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }
                                                                    else if (_claimLinePayment.Transaction_Type == TransactionType.Coinsurance
                                                                        || _claimLinePayment.Transaction_Type == TransactionType.Copay
                                                                        || _claimLinePayment.Transaction_Type == TransactionType.Deductible
                                                                        || _claimLinePayment.Transaction_Type == TransactionType.Coverage)
                                                                    { oDBParameters.Add("@nPayerModeID", PayerMode.Self.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }
                                                                    else
                                                                    { oDBParameters.Add("@nPayerModeID", PayerMode.None.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }


                                                                    //if (_claimLines[l].ClaimLine_Payments[m].InsuranceID > 0)
                                                                    //{ oDBParameters.Add("@nPayerModeID", PayerMode.Insurance.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }
                                                                    //else
                                                                    //{ oDBParameters.Add("@nPayerModeID", PayerMode.Self.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }

                                                                    //...** End code changes 20090702 - Sagar Ghodke

                                                                    oDBParameters.Add("@nClinicID", _claimLines[l].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                                                                    oDBParameters.Add("@nCloseDayTrayID", _claimLines[l].CloseDayTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@sCloseDayCode", _claimLines[l].CloseDayTrayCode, ParameterDirection.Input, SqlDbType.VarChar);
                                                                    oDBParameters.Add("@sCloseDayTrayDescription", _claimLines[l].CloseDayTrayName, ParameterDirection.Input, SqlDbType.VarChar);

                                                                    oDBParameters.Add("@bGenerateTrnServiceLineID",_generateTranServiceLineId , ParameterDirection.Input, SqlDbType.Bit); 
                                                                    oDBParameters.Add("@nPaymentTransactionServiceLineID",_paymentTranServiceLineId, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                                                    _generateTranServiceLineId = false;

                                                                    //oDBParameters.Add("@nMasterTransactionType",_claimLines[l].MasterTransactoinType.GetHashCode(),ParameterDirection.Input,SqlDbType.Int);//int,
                                                                    //oDBParameters.Add("@nRefundTypeId",_claimLines[l].RefundID,ParameterDirection.Input,SqlDbType.BigInt);// numeric(18,0),
                                                                    //oDBParameters.Add("@sRefundTypeCode",_claimLines[l].RefundCode,ParameterDirection.Input,SqlDbType.VarChar);// varchar(255),
                                                                    //oDBParameters.Add("@sRefundTypeDesc",_claimLines[l].RefundDesc,ParameterDirection.Input,SqlDbType.VarChar);// varchar(255)

                                                                    oDBParameters.Add("@nMasterTransactionType", oPaymentTransaction.MasterTransactoinType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//int,
                                                                    oDBParameters.Add("@nRefundTypeId", oPaymentTransaction.RefundID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                                                    oDBParameters.Add("@sRefundTypeCode", oPaymentTransaction.RefundCode, ParameterDirection.Input, SqlDbType.VarChar);// varchar(255),
                                                                    oDBParameters.Add("@sRefundTypeDesc", oPaymentTransaction.RefundDesc, ParameterDirection.Input, SqlDbType.VarChar);// varchar(255)

                                                                    //****.... Code commented on 20090904 by Sagar Ghodke
                                                                    //****.... Code commented for saving the Remit ID if present for each detail entr

                                                                    //if (_claimLinePayment.Transaction_Type == TransactionType.InsuracePayment)
                                                                    //{ oDBParameters.Add("@nRemitID", _claimLines[l].RemitID, ParameterDirection.Input, SqlDbType.BigInt); }
                                                                    //else
                                                                    //{ oDBParameters.Add("@nRemitID",0, ParameterDirection.Input, SqlDbType.BigInt); }

                                                                    oDBParameters.Add("@nRemitID", _claimLines[l].RemitID, ParameterDirection.Input, SqlDbType.BigInt); 

                                                                    //****.... End code commented on 20090904 by Sagar Ghodke

                                                                    oDBParameters.Add("@nReceivedPaymentCounter", oPaymentTransaction.TransactionPaymentClaims[k].ReceivedPaymentCounter, ParameterDirection.Input, SqlDbType.Int); 

                                                                    oDB.Execute("BL_INUP_Transaction_Payment_DTL", oDBParameters, out oPaymentTransactionDetailID, out oPaymentTransactionServiceLineID);
                                                                    oDBParameters.Clear();
                                                                    _PaymentTransactionDetailID = Convert.ToInt64(oPaymentTransactionDetailID);
                                                                    _paymentTranServiceLineId = Convert.ToInt64(oPaymentTransactionServiceLineID);

                                                                    #endregion " Make Payment Detail Table entries "

                                                                    #region " Set fully applied copay & advance flag "

                                                                    if (_clmLnPayDtl.PaymentID > 0 && _PaymentTransactionDetailID > 0)
                                                                    {
                                                                        oDBParameters.Clear();
                                                                        oDBParameters.Add("@nAdvPayID", _clmLnPayDtl.PaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                        oDBParameters.Add("@nClinicID", _claimLines[l].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                        int _isUpdated = 0;
                                                                        _isUpdated = oDB.Execute("BL_AdvancePayment_SetIsApplied", oDBParameters);
                                                                        oDBParameters.Clear();
                                                                    }

                                                                    #endregion

                                                                    #region " Set Remit Saved Flag "

                                                                    if (_PaymentTransactionDetailID > 0 && _claimLines[l].RemitID > 0)
                                                                    {
                                                                        //gloRemittance ogloRemittance = new gloRemittance(_databaseconnectionstring);
                                                                        //gloPayment ogloPayment = new gloPayment(_databaseconnectionstring);
                                                                        //ogloRemittance.UpdateRemittances(_claimLines[l].RemitID,ogloPayment.GetFormattedClaimPaymentNumber(oPaymentTransaction.TransactionPaymentClaims[k].ClaimNo.ToString()));
                                                                        //ogloPayment.Dispose();
                                                                        //ogloRemittance.Dispose();
                                                                    }

                                                                    #endregion " Set Remit Saved Flag "

                                                                }
                                                            }

                                                            #region " Make Transaction Entry "

                                                            //if (SaveFlag == true)
                                                            //{
                                                                if (_claimLinePayment.ChargedAmount > 0 || oPaymentTransaction.MasterTransactoinType == TransactionType.Refund)
                                                                {
                                                                    oDBParameters.Clear();

                                                                    oDBParameters.Add("@nOtherPaymentID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nTransactionID", _claimLines[l].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nTransactionDetailID", _claimLines[l].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nTransactionLineNo", _claimLines[l].TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nAmountType", _claimLines[l].ClaimLine_Payments[m].Transaction_Type.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                                                    oDBParameters.Add("@dAmount", _claimLines[l].ClaimLine_Payments[m].ChargedAmount, ParameterDirection.Input, SqlDbType.Decimal);
                                                                    oDBParameters.Add("@nVisitID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nAppointmentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@bIsPaid", DBNull.Value, ParameterDirection.Input, SqlDbType.Bit);
                                                                    oDBParameters.Add("@nPaymentID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nPaymentDetailID", _PaymentTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);

                                                                    //..ReferenceID = InsuranceID against which payment is being made previously now take separately
                                                                    oDBParameters.Add("@nReferenceID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                                                                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                                                                    //oDBParameters.Add("@nInsuranceID",oPaymentTransaction.InsuranceID,ParameterDirection.Input,SqlDbType.BigInt);//   numeric(18,0),
                                                                    //oDBParameters.Add("@sInsuranceName", oPaymentTransaction.InsuranceName, ParameterDirection.Input, SqlDbType.VarChar);// varchar(255)

                                                                    oDBParameters.Add("@nInsuranceID", oPaymentTransaction.TransactionPaymentClaims[k].PaymentInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);//   numeric(18,0),
                                                                    oDBParameters.Add("@sInsuranceName", oPaymentTransaction.TransactionPaymentClaims[k].PaymentInsuranceName, ParameterDirection.Input, SqlDbType.VarChar);// varchar(255)


                                                                    oDBParameters.Add("@nReceivedPaymentCounter", oPaymentTransaction.TransactionPaymentClaims[k].ReceivedPaymentCounter, ParameterDirection.Input, SqlDbType.BigInt);

                                                                    //...code added on 20091001 by Sagar Ghodke
                                                                    //....code added to add the MasterTransaction Type (i.e Payment OR Refund)
                                                                    oDBParameters.Add("@nMasterPaymentTypeID", oPaymentTransaction.MasterTransactoinType.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                                                                    //...end code add on 20091001 by Sagar Ghodke


                                                                    Object _retVal = null;
                                                                    oDB.Execute("BL_INUP_OtherPayments", oDBParameters, out _retVal);
                                                                    oDBParameters.Clear();
                                                                    // _isChargedTransactionSaved = true;
                                                                }
                                                            //}

                                                            #endregion " Make Transaction Entry "

                                                            #region " Add Note "

                                                            if (_claimLinePayment.Notes != null && _claimLinePayment.Notes.Count > 0)
                                                            {
                                                                if (_claimLinePayment.Notes[0] != null)
                                                                {
                                                                    oDBParameters.Clear();
                                                                    oDBParameters.Add("@nPaymentTransactionID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nPaymentTransactionDetailID", _PaymentTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nPaymentNoteId", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nNoteType", _claimLinePayment.Notes[0].NoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                                                    oDBParameters.Add("@nNoteDateTime", _claimLinePayment.Notes[0].NoteDate, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@nUserID", _claimLinePayment.Notes[0].UserID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@sNoteDescription", _claimLinePayment.Notes[0].NoteDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                                                    oDBParameters.Add("@nClinicID", _claimLinePayment.Notes[0].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                                                                    oDB.Execute("BL_INUP_Transaction_Payment_Notes", oDBParameters);
                                                                    oDBParameters.Clear();
                                                                }
                                                            }

                                                            #endregion " Add Note "

                                                        }
                                                    }

                                                }
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            //...** If no lines found Rollback Transaction
                                            //oDB.Rollback();
                                        }//if (oPaymentTransactionID != null && Convert.ToString(oPaymentTransactionID) != "" && Convert.ToString(oPaymentTransactionID) != "0")
                                }
                                else
                                {
                                    //...** If no lines found Rollback Transaction
                                    //oDB.Rollback();
                                }//if (oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines != null && oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines.Count > 0)
                            }//END FOR...for (int k = 0; k < oPaymentTransaction.TransactionPaymentClaims.Count; k++)
                        }
                        else
                        {
                            //...** If no lines found Rollback Transaction
                            //oDB.Rollback();
                        }//if (oPaymentTransaction.TransactionPaymentClaims != null && oPaymentTransaction.TransactionPaymentClaims.Count > 0)
                    }
                    else
                    {
                        //...** If no lines found Rollback Transaction
                        //oDB.Rollback();
                    }//if ((oMultiplePaymentTransactionID != null && Convert.ToString(oMultiplePaymentTransactionID) != ""))

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            return _PaymentTransactionID;
        }
        
        public Int64 SavePaymentTransactionBeforeMultiPaymentApply(PaymentTransaction oPaymentTransaction, Int64 ClinicID, Int64 PatientID)
        {
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            //Int64 _MultiplePaymentTransactionID = 0;
            //Int64 _PaymentTransactionID = 0;
            //Int64 _PaymentTransactionDetailID = 0;
            //object oMultiplePaymentTransactionID = null;
            //object oPaymentTransactionID = null;
            //object oPaymentTransactionDetailID = null;
            //bool _isPendingSave = false;

            //try
            //{
            //    //..Start connection using sql transaction
            //    oDB.Connect(false);

            //    #region "....Add Payment Master Multiple Payment Table Data .... "

            //    //..Addding Payment Master table entry 
            //    //...For now we are not considering the entries for Payment Mode in 
            //    //..payment master
            //    //... If Payer Amount is not present then the entry is of pending amount

            //    //if (oPaymentTransaction.PayerAmount > 0)
            //    //{
            //    oDBParameters.Add("@nMultiplePaymentTransactionID", oPaymentTransaction.MultiplePaymentTransactionID, ParameterDirection.InputOutput, SqlDbType.BigInt);
            //    oDBParameters.Add("@nPaymentDate", oPaymentTransaction.PaymentDate, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@nPaymentTime", oPaymentTransaction.PaymentTime, ParameterDirection.Input, SqlDbType.BigInt);

            //    if (oPaymentTransaction.PayerAmount > 0)
            //    { oDBParameters.Add("@dPayerAmount", oPaymentTransaction.PayerAmount, ParameterDirection.Input, SqlDbType.Decimal); }
            //    //else if (oPaymentTransaction.PendingPayment > 0)
            //    else if (oPaymentTransaction.PatientPayment > 0)
            //    { oDBParameters.Add("@dPayerAmount", oPaymentTransaction.PatientPayment, ParameterDirection.Input, SqlDbType.Decimal); }
            //    else
            //    { oDBParameters.Add("@dPayerAmount", 0, ParameterDirection.Input, SqlDbType.Decimal); }

            //    #region " Master payment mode entry "

            //    if (oPaymentTransaction.PaymentModeDetail != null && oPaymentTransaction.PaymentModeDetail.PaymentMode != PaymentMode.None)
            //    {
            //        oDBParameters.Add("@nPaymentMode", oPaymentTransaction.PaymentModeDetail.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
            //        oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardEFT_Number, ParameterDirection.Input, SqlDbType.VarChar);
            //        oDBParameters.Add("@nCrdExpChkMnyOrdDate", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardExpiryEFT_Date, ParameterDirection.Input, SqlDbType.BigInt);
            //        oDBParameters.Add("@sSecurityNo", oPaymentTransaction.PaymentModeDetail.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
            //        oDBParameters.Add("@sCardType", oPaymentTransaction.PaymentModeDetail.CardType, ParameterDirection.Input, SqlDbType.VarChar);
            //        oDBParameters.Add("@sAuthorizationNo", oPaymentTransaction.PaymentModeDetail.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
            //        oDBParameters.Add("@nCardID", oPaymentTransaction.PaymentModeDetail.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);
            //    }

            //    #endregion " Master payment mode entry "

            //    oDBParameters.Add("@nClinicID", oPaymentTransaction.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@nUserID", oPaymentTransaction.UserID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@sUserName", oPaymentTransaction.UserName, ParameterDirection.Input, SqlDbType.VarChar);
            //    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


            //    oDB.Execute("BL_INUP_Transaction_Payment_MST_MultiPayment", oDBParameters, out oMultiplePaymentTransactionID);
            //    //}

            //    #endregion "....Add Payment Master Table Data .... "

            //    #region "....Add Payment Master Table Data .... "

            //    //..Addding Payment Master table entry 
            //    //...For now we are not considering the entries for Payment Mode in 
            //    //..payment master
            //    //... If Payer Amount is not present then the entry is of pending amount

            //    //if (oPaymentTransaction.PayerAmount > 0)
            //    //{
            //    oDBParameters.Add("@nPaymentTransactionID", oPaymentTransaction.PaymentTransactionID, ParameterDirection.InputOutput, SqlDbType.BigInt);
            //    oDBParameters.Add("@nPaymentDate", oPaymentTransaction.PaymentDate, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@nPaymentTime", oPaymentTransaction.PaymentTime, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@nPaymentNo", oPaymentTransaction.PaymentTransactionNo, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@nTransactionType", oPaymentTransaction.TransactionTypeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

            //    if (oPaymentTransaction.PayerAmount > 0)
            //    { oDBParameters.Add("@dPayerAmount", oPaymentTransaction.PayerAmount, ParameterDirection.Input, SqlDbType.Decimal); }
            //    //else if (oPaymentTransaction.PendingPayment > 0)
            //    else if (oPaymentTransaction.PatientPayment > 0)
            //    { oDBParameters.Add("@dPayerAmount", oPaymentTransaction.PatientPayment, ParameterDirection.Input, SqlDbType.Decimal); }
            //    else
            //    { oDBParameters.Add("@dPayerAmount", 0, ParameterDirection.Input, SqlDbType.Decimal); }

            //    #region " Master payment mode entry "

            //    if (oPaymentTransaction.PaymentModeDetail != null && oPaymentTransaction.PaymentModeDetail.PaymentMode != PaymentMode.None)
            //    {
            //        oDBParameters.Add("@nPaymentMode", oPaymentTransaction.PaymentModeDetail.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
            //        oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardEFT_Number, ParameterDirection.Input, SqlDbType.VarChar);
            //        oDBParameters.Add("@nCrdExpChkMnyOrdDate", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardExpiryEFT_Date, ParameterDirection.Input, SqlDbType.BigInt);
            //        oDBParameters.Add("@sSecurityNo", oPaymentTransaction.PaymentModeDetail.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
            //        oDBParameters.Add("@sCardType", oPaymentTransaction.PaymentModeDetail.CardType, ParameterDirection.Input, SqlDbType.VarChar);
            //        oDBParameters.Add("@sAuthorizationNo", oPaymentTransaction.PaymentModeDetail.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
            //        oDBParameters.Add("@nCardID", oPaymentTransaction.PaymentModeDetail.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);
            //    }

            //    //oDBParameters.Add("@nPaymentMode", oPaymentTransaction.PaymentModeValue.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
            //    //oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", oPaymentTransaction.CardNoAndCheckNoAndMoneyOrderNo, ParameterDirection.Input, SqlDbType.VarChar);
            //    //oDBParameters.Add("@nCrdExpChkMnyOrdDate", oPaymentTransaction.CardExpiryAndCheckDateAndMoneyOrderDate, ParameterDirection.Input, SqlDbType.BigInt);
            //    //oDBParameters.Add("@sSecurityNo", oPaymentTransaction.SecurityNo, ParameterDirection.Input, SqlDbType.VarChar);
            //    //oDBParameters.Add("@sCardType", oPaymentTransaction.CardType, ParameterDirection.Input, SqlDbType.VarChar);
            //    //oDBParameters.Add("@sAuthorizationNo", oPaymentTransaction.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
            //    //oDBParameters.Add("@nCardID", oPaymentTransaction.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);

            //    #endregion " Master payment mode entry "

            //    oDBParameters.Add("@nTransactionTypeDetailID", oPaymentTransaction.TransactionTypeDetailValue, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@sTransactionTypeDetailName", oPaymentTransaction.TransactionTypeDetailName, ParameterDirection.Input, SqlDbType.VarChar);
            //    oDBParameters.Add("@nPaymentTransactionStatus", oPaymentTransaction.PaymentTransactionStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
            //    oDBParameters.Add("@nClinicID", oPaymentTransaction.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@nUserID", oPaymentTransaction.UserID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@sUserName", oPaymentTransaction.UserName, ParameterDirection.Input, SqlDbType.VarChar);
            //    //oDBParameters.Add("@nReplicationID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

            //    oDBParameters.Add("@nCloseDayTrayID", oPaymentTransaction.CloseDayTrayID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@sCloseDayCode", oPaymentTransaction.CloseDayTrayCode, ParameterDirection.Input, SqlDbType.VarChar);
            //    oDBParameters.Add("@sCloseDayTrayDescription", oPaymentTransaction.CloseDayTrayName, ParameterDirection.Input, SqlDbType.VarChar);

            //    oDB.Execute("BL_INUP_Transaction_Payment_MST", oDBParameters, out oPaymentTransactionID);
            //    //}

            //    #endregion "....Add Payment Master Table Data .... "

            //    //if (oPaymentTransactionID != null && Convert.ToString(oPaymentTransactionID) != "")
            //    if ((oPaymentTransactionID != null && Convert.ToString(oPaymentTransactionID) != ""))
            //    {
            //        _PaymentTransactionID = Convert.ToInt64(oPaymentTransactionID);

            //        if (oPaymentTransaction.TransactionPaymentClaims != null && oPaymentTransaction.TransactionPaymentClaims.Count > 0)
            //        {
            //            //..** Read Payment Claim collection containing Claim Lines 
            //            for (int k = 0; k < oPaymentTransaction.TransactionPaymentClaims.Count; k++)
            //            {
            //                if (oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines != null && oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines.Count > 0)
            //                {
            //                    ClaimLines _claimLines = null;
            //                    ClaimLine _claimLine = null;
            //                    _claimLines = oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines;

            //                    for (int l = 0; l < _claimLines.Count; l++)
            //                    {
            //                        _claimLine = _claimLines[l];

            //                        if (_claimLine.ClaimLine_Payments != null && _claimLine.ClaimLine_Payments.Count > 0)
            //                        {
            //                            ClaimLinePayment _claimLinePayment = null;

            //                            for (int m = 0; m < _claimLine.ClaimLine_Payments.Count; m++)
            //                            {
            //                                _claimLinePayment = _claimLine.ClaimLine_Payments[m];

            //                                //if (_claimLinePayment != null && _claimLinePayment.PaymentDetails != null && _claimLinePayment.PaymentDetails.Count > 0)
            //                                if (_claimLinePayment != null && _claimLinePayment.PaymentDetails != null)
            //                                {
            //                                    ClaimLinePaymentDetail _clmLnPayDtl = null;
            //                                    bool _isChargedTransactionSaved = false;

            //                                    for (int n = 0; n < _claimLinePayment.PaymentDetails.Count; n++)
            //                                    {
            //                                        _clmLnPayDtl = _claimLinePayment.PaymentDetails[n];

            //                                        if (_clmLnPayDtl != null)
            //                                        {
            //                                            #region " Make Payment Detail Table entries "

            //                                            oDBParameters.Clear();

            //                                            //@nPaymentTransactionID numeric(18, 0),@nPaymentTransactionDetailID numeric(18, 0) OUTPUT,  
            //                                            //@nPatientID numeric(18, 0),@nPaymentDate numeric(18, 0),
            //                                            //@nBillingTransactionID numeric(18, 0),@nBillingTransactionDetailID numeric(18, 0),  
            //                                            //@nBillingTransactionLineNo numeric(18, 0),@nClaimNo numeric(18, 0),  
            //                                            //@sCPTCode varchar(255),@sCPTDescription varchar(255),@dAllowedAmt decimal(18, 2),  

            //                                            //@dCurrentPaymentAmt decimal(18, 2),@nCurrentPaymentAmtType int,@nTransactionType int,  
            //                                            //@nPaymentMode int,@sCrdNoMnyOrdNoChkNo varchar(255),@nCrdExpChkMnyOrdDate numeric(18, 0),  
            //                                            //@sSecurityNo varchar(255),@sCardType varchar(255),@nPaymentInsuranceID numeric(18, 0),  
            //                                            //@nPayerModeID int,@nPaymentTransactionLineStatus int,@nClinicID numeric(18, 0),  
            //                                            //@nCoPayID numeric (18,0),@nTransactionTypeDetailID numeric(18,0), 
            //                                            //@sTransactionTypeDetailName varchar(255),@sAuthorizationNo varchar(255),  
            //                                            //@nCardID numeric(18,0),@nUserID numeric(18,0),@sUserName varchar(255),  
            //                                            //@nReplicationID numeric(18,0)  


            //                                            oDBParameters.Add("@nPaymentTransactionID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@nPaymentTransactionDetailID", _claimLines[l].PaymentTransactionDetailID, ParameterDirection.InputOutput, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@nPatientID", _claimLines[l].PatientID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@nPaymentDate", _claimLines[l].PaymentDate, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@nBillingTransactionID", _claimLines[l].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@nBillingTransactionDetailID", _claimLines[l].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@nBillingTransactionLineNo", _claimLines[l].TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@nClaimNo", _claimLines[l].ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@sCPTCode", _claimLines[l].CPTCode, ParameterDirection.Input, SqlDbType.VarChar);
            //                                            oDBParameters.Add("@sCPTDescription", _claimLines[l].CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);
            //                                            oDBParameters.Add("@dAllowedAmt", _claimLines[l].TransactionAllowed, ParameterDirection.Input, SqlDbType.Decimal);

            //                                            oDBParameters.Add("@nTransactionType", _claimLines[l].ClaimLine_Payments[m].Transaction_Type.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
            //                                            oDBParameters.Add("@dCurrentPaymentAmt", _clmLnPayDtl.PayAmount, ParameterDirection.Input, SqlDbType.Decimal);
            //                                            oDBParameters.Add("@nCurrentPaymentAmtType", _clmLnPayDtl.PayAmountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
            //                                            //....**** Note
            //                                            //....@nCoPayID field contains the ids of applied copay,advance payment
            //                                            oDBParameters.Add("@nCoPayID", _clmLnPayDtl.PaymentID, ParameterDirection.Input, SqlDbType.BigInt);

            //                                            if (_claimLines[l].ClaimLine_Payments[m].Transaction_Type == TransactionType.InsuracePayment && _clmLnPayDtl.AmountPaymentMode.PaymentMode == PaymentMode.None)
            //                                            {

            //                                                //..if line payment mode not present enter the master payment mode
            //                                                oDBParameters.Add("@nPaymentMode", oPaymentTransaction.PaymentModeDetail.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
            //                                                oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardEFT_Number, ParameterDirection.Input, SqlDbType.VarChar);
            //                                                oDBParameters.Add("@nCrdExpChkMnyOrdDate", oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardExpiryEFT_Date, ParameterDirection.Input, SqlDbType.BigInt);
            //                                                oDBParameters.Add("@sSecurityNo", oPaymentTransaction.PaymentModeDetail.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
            //                                                oDBParameters.Add("@sCardType", oPaymentTransaction.PaymentModeDetail.CardTypeID, ParameterDirection.Input, SqlDbType.VarChar);
            //                                                oDBParameters.Add("@sAuthorizationNo", oPaymentTransaction.PaymentModeDetail.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
            //                                                oDBParameters.Add("@nCardID", oPaymentTransaction.PaymentModeDetail.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            }
            //                                            else
            //                                            {
            //                                                oDBParameters.Add("@nPaymentMode", _clmLnPayDtl.AmountPaymentMode.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
            //                                                oDBParameters.Add("@sCrdNoMnyOrdNoChkNo", _clmLnPayDtl.AmountPaymentMode.CheckMoneyOrderCardEFT_Number, ParameterDirection.Input, SqlDbType.VarChar);
            //                                                oDBParameters.Add("@nCrdExpChkMnyOrdDate", _clmLnPayDtl.AmountPaymentMode.CheckMoneyOrderCardExpiryEFT_Date, ParameterDirection.Input, SqlDbType.BigInt);
            //                                                oDBParameters.Add("@sSecurityNo", _clmLnPayDtl.AmountPaymentMode.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
            //                                                oDBParameters.Add("@sCardType", _clmLnPayDtl.AmountPaymentMode.CardType, ParameterDirection.Input, SqlDbType.VarChar);
            //                                                oDBParameters.Add("@sAuthorizationNo", _clmLnPayDtl.AmountPaymentMode.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
            //                                                oDBParameters.Add("@nCardID", _clmLnPayDtl.AmountPaymentMode.CardTypeID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            }

            //                                            //....**** Note
            //                                            //....** Any kind of adjustment made the adjustment id & the description for 
            //                                            //.... adjustment type goes in this table
            //                                            //....@nTransactionTypeDetailID 
            //                                            //....@sTransactionTypeDetailName
            //                                            oDBParameters.Add("@nTransactionTypeDetailID", _clmLnPayDtl.AmountPaymentMode.AdjustmentTypeID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@sTransactionTypeDetailName", _clmLnPayDtl.AmountPaymentMode.Adjustment_Type.ToString(), ParameterDirection.Input, SqlDbType.VarChar); //

            //                                            oDBParameters.Add("@nUserID", oPaymentTransaction.UserID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@sUserName", oPaymentTransaction.UserName, ParameterDirection.Input, SqlDbType.VarChar);
            //                                            oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt); // numeric(18,0)  
            //                                            oDBParameters.Add("@nPaymentTransactionLineStatus", oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines[l].PaymentLineStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

            //                                            //oDBParameters.Add("@nPaymentInsuranceID", _claimLines[l].ClaimLine_Payments[m].InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@nPaymentInsuranceID", oPaymentTransaction.InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);

            //                                            //...** Code chages done on 20090702 by - Sagar Ghodke
            //                                            //...

            //                                            //if (_claimLines[l].ClaimLine_Payments[m].InsuranceID > 0)
            //                                            //{ oDBParameters.Add("@nPayerModeID", PayerMode.Insurance.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }
            //                                            //else
            //                                            //{ oDBParameters.Add("@nPayerModeID", PayerMode.Self.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }

            //                                            //...Check if the transaction type is insurance then set 

            //                                            if (_claimLinePayment.Transaction_Type == TransactionType.InsuracePayment)
            //                                            { oDBParameters.Add("@nPayerModeID", PayerMode.Insurance.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }
            //                                            else if (_claimLinePayment.Transaction_Type == TransactionType.Coinsurance
            //                                                || _claimLinePayment.Transaction_Type == TransactionType.Copay
            //                                                || _claimLinePayment.Transaction_Type == TransactionType.Deductible
            //                                                || _claimLinePayment.Transaction_Type == TransactionType.Coverage)
            //                                            { oDBParameters.Add("@nPayerModeID", PayerMode.Self.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }
            //                                            else
            //                                            { oDBParameters.Add("@nPayerModeID", PayerMode.None.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }


            //                                            //if (_claimLines[l].ClaimLine_Payments[m].InsuranceID > 0)
            //                                            //{ oDBParameters.Add("@nPayerModeID", PayerMode.Insurance.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }
            //                                            //else
            //                                            //{ oDBParameters.Add("@nPayerModeID", PayerMode.Self.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); }

            //                                            //...** End code changes 20090702 - Sagar Ghodke

            //                                            oDBParameters.Add("@nClinicID", _claimLines[l].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

            //                                            oDBParameters.Add("@nCloseDayTrayID", _claimLines[l].CloseDayTrayID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@sCloseDayCode", _claimLines[l].CloseDayTrayCode, ParameterDirection.Input, SqlDbType.VarChar);
            //                                            oDBParameters.Add("@sCloseDayTrayDescription", _claimLines[l].CloseDayTrayName, ParameterDirection.Input, SqlDbType.VarChar);


            //                                            oDB.Execute("BL_INUP_Transaction_Payment_DTL", oDBParameters, out oPaymentTransactionDetailID);
            //                                            oDBParameters.Clear();
            //                                            _PaymentTransactionDetailID = Convert.ToInt64(oPaymentTransactionDetailID);

            //                                            #endregion " Make Payment Detail Table entries "

            //                                            #region " Set fully applied copay & advance flag "

            //                                            if (_clmLnPayDtl.PaymentID > 0 && _PaymentTransactionDetailID > 0)
            //                                            {
            //                                                oDBParameters.Clear();
            //                                                oDBParameters.Add("@nAdvPayID", _clmLnPayDtl.PaymentID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                                oDBParameters.Add("@nClinicID", _claimLines[l].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                                int _isUpdated = 0;
            //                                                _isUpdated = oDB.Execute("BL_AdvancePayment_SetIsApplied", oDBParameters);
            //                                                oDBParameters.Clear();
            //                                            }

            //                                            #endregion
            //                                        }
            //                                    }

            //                                    #region " Make Transaction Entry "

            //                                    if (_claimLinePayment.ChargedAmount > 0)
            //                                    {
            //                                        oDBParameters.Clear();

            //                                        oDBParameters.Add("@nOtherPaymentID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
            //                                        oDBParameters.Add("@nTransactionID", _claimLines[l].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                        oDBParameters.Add("@nTransactionDetailID", _claimLines[l].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                        oDBParameters.Add("@nTransactionLineNo", _claimLines[l].TransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);
            //                                        oDBParameters.Add("@nAmountType", _claimLines[l].ClaimLine_Payments[m].Transaction_Type.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
            //                                        oDBParameters.Add("@dAmount", _claimLines[l].ClaimLine_Payments[m].ChargedAmount, ParameterDirection.Input, SqlDbType.Decimal);
            //                                        oDBParameters.Add("@nVisitID", 0, ParameterDirection.Input, SqlDbType.BigInt);
            //                                        oDBParameters.Add("@nAppointmentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
            //                                        oDBParameters.Add("@bIsPaid", DBNull.Value, ParameterDirection.Input, SqlDbType.Bit);
            //                                        oDBParameters.Add("@nPaymentID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                        oDBParameters.Add("@nPaymentDetailID", _PaymentTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);

            //                                        //..ReferenceID = InsuranceID against which payment is being made
            //                                        oDBParameters.Add("@nReferenceID", oPaymentTransaction.InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                        oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

            //                                        Object _retVal = null;
            //                                        oDB.Execute("BL_INUP_OtherPayments", oDBParameters, out _retVal);
            //                                        oDBParameters.Clear();
            //                                        // _isChargedTransactionSaved = true;
            //                                    }

            //                                    #endregion " Make Transaction Entry "

            //                                    #region " Add Note "

            //                                    if (_claimLinePayment.Notes != null && _claimLinePayment.Notes.Count > 0)
            //                                    {
            //                                        if (_claimLinePayment.Notes[0] != null)
            //                                        {
            //                                            oDBParameters.Clear();
            //                                            oDBParameters.Add("@nPaymentTransactionID", _PaymentTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@nPaymentTransactionDetailID", _PaymentTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@nPaymentNoteId", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@nNoteType", _claimLinePayment.Notes[0].NoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
            //                                            oDBParameters.Add("@nNoteDateTime", _claimLinePayment.Notes[0].NoteDate, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@nUserID", _claimLinePayment.Notes[0].UserID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@sNoteDescription", _claimLinePayment.Notes[0].NoteDescription, ParameterDirection.Input, SqlDbType.VarChar);
            //                                            oDBParameters.Add("@nClinicID", _claimLinePayment.Notes[0].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
            //                                            oDB.Execute("BL_INUP_Transaction_Payment_Notes", oDBParameters);
            //                                            oDBParameters.Clear();
            //                                        }
            //                                    }

            //                                    #endregion " Add Note "

            //                                }
            //                            }

            //                        }
            //                    }

            //                }//END IF .....if (oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines != null && oPaymentTransaction.TransactionPaymentClaims[k].Cliam_Lines.Count > 0)
            //                else
            //                {
            //                    //...** If no lines found Rollback Transaction
            //                    //oDB.Rollback();
            //                }

            //            }//END FOR...for (int k = 0; k < oPaymentTransaction.TransactionPaymentClaims.Count; k++)

            //        } //END IF....if (oPaymentTransaction.TransactionPaymentClaims != null && oPaymentTransaction.TransactionPaymentClaims.Count > 0)
            //        else
            //        {
            //            //...** If no lines found Rollback Transaction
            //            //oDB.Rollback();
            //        }

            //    }//END IF .....if (oPaymentTransactionID != null && Convert.ToString(oPaymentTransactionID) != "")

            //    oDB.Disconnect();
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}
            //finally
            //{
            //    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            //}
            //return _PaymentTransactionID;
            return 0;
        }

        public PaymentTransaction GetMasterPaymentData(Int64 PaymentTransactionId, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtMasterPaymentData = null;
            string _sqlQuery = "";
            PaymentTransaction oPaymentTransaction = null;

            try
            {
                #region " SQL Query to retrive Master Payment Data "
                
                _sqlQuery = " SELECT ISNULL(nPaymentDate,0) AS PaymentDate,ISNULL(nPaymentTime,0) AS PaymentTime, " +
                " ISNULL(nPaymentNo,0) AS PaymentNumber,ISNULL(nTransactionType,0) AS TransactionType, " +
                " ISNULL(nPaymentMode,0) AS PaymentMode,ISNULL(dPayerAmount,0) AS PayerAmount,  " +
                " ISNULL(sCrdNoMnyOrdNoChkNo,'') AS CrdNoMnyOrdNoChkN,ISNULL(nCrdExpChkMnyOrdDate,0) AS CrdExpChkMnyOrdDate, " +
                " ISNULL(sSecurityNo,'') AS SecurityNo,ISNULL(sCardType,'') AS CardType, " +
                " ISNULL(nPaymentTransactionStatus,0) AS PaymentTransactionStatus, " +
                " ISNULL(nTransactionTypeDetailID,0) AS TransactionTypeDetailID, " +
                " ISNULL(sTransactionTypeDetailName,'') AS TransactionTypeDetailName, " +
                " ISNULL(nRefID,0) AS RefID,ISNULL(sRefCode,'') AS RefCode, " +
                " ISNULL(nRefType,0) AS RefType,ISNULL(sAuthorizationNo,'') AS AuthorizationNo, " +
                " ISNULL(nCardID,0) AS CardID,ISNULL(nUserID,0) AS UserID,ISNULL(sUserName,0) AS UserName, " +
                " ISNULL(nCloseDayTrayID,0) AS CloseDayTrayID,ISNULL(sCloseDayCode,'') AS CloseDayCode, " +
                " ISNULL(sCloseDayTrayDescription,'') AS CloseDayTrayDescription, " +
                " ISNULL(nIsTrayClose,0) AS IsTrayClose,ISNULL(nMultiPaymentTransactionID,0) AS MultiPaymentTransactionID, " +
                " ISNULL(nPaymentInsuranceID,0) AS PaymentInsID,ISNULL(sPaymentInsurance,'') AS PaymentInsurance "+
                " FROM BL_Transaction_Payment_MST " +
                " WHERE nPaymentTransactionID = " + PaymentTransactionId + " AND nClinicID = " + ClinicID + " ";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtMasterPaymentData);
                oDB.Disconnect();

                #endregion " SQL Query to retrive Master Payment Data "

                #region " Set Master Data "

                if (_dtMasterPaymentData != null && _dtMasterPaymentData.Rows.Count > 0)
                {
                    oPaymentTransaction = new PaymentTransaction();
                    oPaymentTransaction.PaymentNo = Convert.ToInt64(_dtMasterPaymentData.Rows[0]["PaymentNumber"]);
                    oPaymentTransaction.MultiplePaymentTransactionID = Convert.ToInt64(_dtMasterPaymentData.Rows[0]["MultiPaymentTransactionID"]);
                    oPaymentTransaction.PaymentDate = Convert.ToInt64(_dtMasterPaymentData.Rows[0]["PaymentDate"]);
                    oPaymentTransaction.PaymentTime = Convert.ToInt64(_dtMasterPaymentData.Rows[0]["PaymentTime"]);
                    oPaymentTransaction.PaymentModeValue = (PaymentMode)Convert.ToInt32(_dtMasterPaymentData.Rows[0]["PaymentMode"]);
                    oPaymentTransaction.PayerAmount = Convert.ToInt64(_dtMasterPaymentData.Rows[0]["PayerAmount"]);
                    
                    //oPaymentTransaction.PendingPayment = Convert.ToInt64(_dtMasterPaymentData.Rows[0][""]);
                    //oPaymentTransaction.PatientPayment = Convert.ToInt64(_dtMasterPaymentData.Rows[0][""]);

                    oPaymentTransaction.PaymentModeDetail.PaymentMode = (PaymentMode)Convert.ToInt32(_dtMasterPaymentData.Rows[0]["PaymentMode"]);
                    oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardEFT_Number = Convert.ToString(_dtMasterPaymentData.Rows[0]["CrdNoMnyOrdNoChkN"]);
                    oPaymentTransaction.PaymentModeDetail.CheckMoneyOrderCardExpiryEFT_Date = Convert.ToInt64(_dtMasterPaymentData.Rows[0]["CrdExpChkMnyOrdDate"]);
                    //oPaymentTransaction.PaymentModeDetail.PayerMode = 
                    oPaymentTransaction.PaymentModeDetail.CardSecurityNumber = Convert.ToString(_dtMasterPaymentData.Rows[0]["SecurityNo"]);
                    oPaymentTransaction.PaymentModeDetail.CardType = Convert.ToString(_dtMasterPaymentData.Rows[0]["CardType"]);
                    oPaymentTransaction.PaymentModeDetail.CardTypeID = Convert.ToInt64(_dtMasterPaymentData.Rows[0]["CardID"]);
                    oPaymentTransaction.PaymentModeDetail.CardAuthorizationNo = Convert.ToString(_dtMasterPaymentData.Rows[0]["AuthorizationNo"]);

                    oPaymentTransaction.UserID = Convert.ToInt64(_dtMasterPaymentData.Rows[0]["UserID"]);
                    oPaymentTransaction.UserName = Convert.ToString(_dtMasterPaymentData.Rows[0]["UserName"]);

                    oPaymentTransaction.InsuranceID = Convert.ToInt64(_dtMasterPaymentData.Rows[0]["PaymentInsID"]);
                    oPaymentTransaction.InsuranceName = Convert.ToString(_dtMasterPaymentData.Rows[0]["PaymentInsurance"]);

                    oPaymentTransaction.CloseDayTrayID = Convert.ToInt64(_dtMasterPaymentData.Rows[0]["CloseDayTrayID"]);
                    oPaymentTransaction.CloseDayTrayCode = Convert.ToString(_dtMasterPaymentData.Rows[0]["CloseDayCode"]);
                    oPaymentTransaction.CloseDayTrayName = Convert.ToString(_dtMasterPaymentData.Rows[0]["CloseDayTrayDescription"]);

                }
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtMasterPaymentData != null) { _dtMasterPaymentData.Dispose(); }
            }

            return oPaymentTransaction;
        }

        public ClaimLinePayments GetClaimLinePayments(Int64 PaymentTransactionId,Int64 ClaimNo, Int64 ClinicID, Int64 PatientID,Int64 BillingTranId,Int64 BillingTranDtlId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtPayDtls = null;
            string _sqlQuery = "";
            ClaimLinePayments _clmLnPayments = new ClaimLinePayments();
            ClaimLinePayment _clmLnPayment = null;
            ClaimLinePaymentDetails _clmLnPaymentDtls = new ClaimLinePaymentDetails();
            ClaimLinePaymentDetail _clmLnPaymentDtl = null ;
            PaymentModeDetails _payModeDetls = new PaymentModeDetails();
         //   PaymentModeDetail _payModeDtl = null ;
            TransactionType _trnType = TransactionType.None;

            try
            {
                #region " SQL Query to retrive payment details "

                #region " Commented Code "

                //_sqlQuery = " SELECT  ISNULL(nPaymentTransactionID,0) AS PD_nPaymentTransactionID,   ISNULL(nPaymentTransactionDetailID, 0) AS PD_nPaymentTransactionDetailID, ISNULL(nPaymentDate, 0) AS PD_nPaymentDate, " +
                //" ISNULL(nBillingTransactionID, 0) AS PD_nBillingTransactionID, ISNULL(nBillingTransactionDetailID, 0) AS PD_nBillingTransactionDetailID,  " +
                //" ISNULL(nBillingTransactionLineNo, 0) AS PD_nBillingTransactionLineNo, ISNULL(nClaimNo, 0) AS PD_nClaimNo, ISNULL(sCPTCode, '')  " +
                //" AS PD_sCPTCode, ISNULL(sCPTDescription, '') AS PD_sCPTDescription, ISNULL(dCurrentPaymentAmt, 0) AS PD_dCurrentPaymentAmt,  " +
                //" ISNULL(dAllowedAmt, 0) AS PD_dAllowedAmt, ISNULL(nCurrentPaymentAmtType, 0) AS PD_nCurrentPaymentAmtType, ISNULL(nTransactionType, 0)  " +
                //" AS PD_nTransactionType, ISNULL(nPaymentMode, 0) AS PD_nPaymentMode, ISNULL(sCrdNoMnyOrdNoChkNo, '') AS PD_sCrdNoMnyOrdNoChkNo,  " +
                //" ISNULL(nCrdExpChkMnyOrdDate, 0) AS PD_nCrdExpChkMnyOrdDate, ISNULL(sSecurityNo, '') AS PD_sSecurityNo, ISNULL(sCardType, '')  " +
                //" AS PD_sCardType, ISNULL(nPaymentInsuranceID, 0) AS PD_nPaymentInsuranceID, ISNULL(nPayerModeID, 0) AS PD_nPayerModeID,  " +
                //" ISNULL(nPaymentTransactionLineStatus, 0) AS PD_nPaymentTransactionLineStatus, ISNULL(nClinicID, 0) AS PD_nClinicID,  " +
                //" ISNULL(nCurrentPaymentCopayID, 0) AS PD_nCurrentPaymentCopayID, ISNULL(nCoPayID, 0) AS PD_nCoPayID, ISNULL(nTransactionTypeDetailID, 0)  " +
                //" AS PD_nTransactionTypeDetailID, ISNULL(sTransactionTypeDetailName, '') AS PD_sTransactionTypeDetailName, ISNULL(nRefID, 0) AS PD_nRefID,  " +
                //" ISNULL(sRefCode, '') AS PD_sRefCode, ISNULL(nRefType, 0) AS PD_nRefType, ISNULL(sAuthorizationNo, '') AS PD_sAuthorizationNo, ISNULL(nCardID,0) AS PD_nCardID,  " +
                //" ISNULL(nUserID, 0) AS PD_nUserID, ISNULL(sUserName, '') AS PD_sUserName, ISNULL(nPatientID, 0) AS PD_nPatientID, " +
                //" (SELECT ISNULL(SUM(dCurrentPaymentAmt),0) "+
                //" FROM BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_1 "+
                //" WHERE  "+
                //" BL_Transaction_Payment_DTL_1.nPaymentTransactionID = " + PaymentTransactionId + " " +
                //" AND BL_Transaction_Payment_DTL_1.nClaimNo = " + ClaimNo + " " +
                //" AND BL_Transaction_Payment_DTL_1.nPatientID = " + PatientID + " " +
                //" AND (BL_Transaction_Payment_DTL_1.nClinicID = " + this.ClinicID + ") " +
                //" AND BL_Transaction_Payment_DTL_1.nTransactionType = BL_Transaction_Payment_DTL.nTransactionType)  AS TotalCurrentPayment " +
                //" FROM    " +
                //" BL_Transaction_Payment_DTL " +
                //" WHERE      " +
                //" (nPatientID = " + PatientID + ")  " +
                //" AND (nClaimNo = " + ClaimNo + ") " +
                //" AND (nPaymentTransactionID = " + PaymentTransactionId + ") " +
                //" AND (nClinicID = " + this.ClinicID + ") " +
                //" ORDER BY PD_nTransactionType desc ";

                #endregion

                #region " Commented - 2 "
                // _sqlQuery = " SELECT  ISNULL(nPaymentTransactionID,0) AS PD_nPaymentTransactionID,   ISNULL(nPaymentTransactionDetailID, 0) AS PD_nPaymentTransactionDetailID, ISNULL(nPaymentDate, 0) AS PD_nPaymentDate, " +
                //" ISNULL(nBillingTransactionID, 0) AS PD_nBillingTransactionID, ISNULL(nBillingTransactionDetailID, 0) AS PD_nBillingTransactionDetailID,  " +
                //" ISNULL(nBillingTransactionLineNo, 0) AS PD_nBillingTransactionLineNo, ISNULL(nClaimNo, 0) AS PD_nClaimNo, ISNULL(sCPTCode, '')  " +
                //" AS PD_sCPTCode, ISNULL(sCPTDescription, '') AS PD_sCPTDescription, ISNULL(dCurrentPaymentAmt, 0) AS PD_dCurrentPaymentAmt,  " +
                //" ISNULL(dAllowedAmt, 0) AS PD_dAllowedAmt, ISNULL(nCurrentPaymentAmtType, 0) AS PD_nCurrentPaymentAmtType, ISNULL(nTransactionType, 0)  " +
                //" AS PD_nTransactionType, ISNULL(nPaymentMode, 0) AS PD_nPaymentMode, ISNULL(sCrdNoMnyOrdNoChkNo, '') AS PD_sCrdNoMnyOrdNoChkNo,  " +
                //" ISNULL(nCrdExpChkMnyOrdDate, 0) AS PD_nCrdExpChkMnyOrdDate, ISNULL(sSecurityNo, '') AS PD_sSecurityNo, ISNULL(sCardType, '')  " +
                //" AS PD_sCardType, ISNULL(nPaymentInsuranceID, 0) AS PD_nPaymentInsuranceID, ISNULL(nPayerModeID, 0) AS PD_nPayerModeID,  " +
                //" ISNULL(nPaymentTransactionLineStatus, 0) AS PD_nPaymentTransactionLineStatus, ISNULL(nClinicID, 0) AS PD_nClinicID,  " +
                //" ISNULL(nCurrentPaymentCopayID, 0) AS PD_nCurrentPaymentCopayID, ISNULL(nCoPayID, 0) AS PD_nCoPayID, ISNULL(nTransactionTypeDetailID, 0)  " +
                //" AS PD_nTransactionTypeDetailID, ISNULL(sTransactionTypeDetailName, '') AS PD_sTransactionTypeDetailName, ISNULL(nRefID, 0) AS PD_nRefID,  " +
                //" ISNULL(sRefCode, '') AS PD_sRefCode, ISNULL(nRefType, 0) AS PD_nRefType, ISNULL(sAuthorizationNo, '') AS PD_sAuthorizationNo, ISNULL(nCardID,0) AS PD_nCardID,  " +
                //" ISNULL(nUserID, 0) AS PD_nUserID, ISNULL(sUserName, '') AS PD_sUserName, ISNULL(nPatientID, 0) AS PD_nPatientID, " +
                //" (SELECT ISNULL(SUM(dCurrentPaymentAmt),0) "+
                //" FROM BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_1 "+
                //" WHERE  "+
                //" BL_Transaction_Payment_DTL_1.nPaymentTransactionID = " + PaymentTransactionId + " " +
                //" AND BL_Transaction_Payment_DTL_1.nClaimNo = " + ClaimNo + " " +
                //" AND BL_Transaction_Payment_DTL_1.nPatientID = " + PatientID + " " +
                //" AND (BL_Transaction_Payment_DTL_1.nClinicID = " + this.ClinicID + ") " +
                //" AND BL_Transaction_Payment_DTL_1.nTransactionType = BL_Transaction_Payment_DTL.nTransactionType)  AS TotalCurrentPayment, " +
                //" ISNULL(BL_Transaction_OtherPayments.dAmount,0) AS OP_ChargedAmount "+

                ////" FROM BL_Transaction_Payment_DTL INNER JOIN "+
                ////" BL_Transaction_OtherPayments ON BL_Transaction_Payment_DTL.nBillingTransactionID = BL_Transaction_OtherPayments.nTransactionID AND  "+
                ////" BL_Transaction_Payment_DTL.nBillingTransactionDetailID = BL_Transaction_OtherPayments.nTransactionDetailID AND  "+
                ////" BL_Transaction_Payment_DTL.nBillingTransactionLineNo = BL_Transaction_OtherPayments.nTransactionLineNo AND  "+
                ////" BL_Transaction_Payment_DTL.nTransactionType = BL_Transaction_OtherPayments.nAmountType AND  "+
                ////" BL_Transaction_Payment_DTL.nPaymentTransactionID = BL_Transaction_OtherPayments.nPaymentID AND "+
                ////" BL_Transaction_Payment_DTL.nPaymentTransactionDetailID = BL_Transaction_OtherPayments.nPaymentDetailID " +

                //" FROM BL_Transaction_Payment_DTL LEFT OUTER JOIN "+
                //" BL_Transaction_OtherPayments ON BL_Transaction_Payment_DTL.nBillingTransactionID = BL_Transaction_OtherPayments.nTransactionID AND  "+
                //" BL_Transaction_Payment_DTL.nBillingTransactionDetailID = BL_Transaction_OtherPayments.nTransactionDetailID AND  "+
                //" BL_Transaction_Payment_DTL.nBillingTransactionLineNo = BL_Transaction_OtherPayments.nTransactionLineNo AND  "+
                //" BL_Transaction_Payment_DTL.nTransactionType = BL_Transaction_OtherPayments.nAmountType AND  "+
                //" BL_Transaction_Payment_DTL.nPaymentTransactionID = BL_Transaction_OtherPayments.nPaymentID " +

                //" WHERE      " +
                //" (nPatientID = " + PatientID + ")  " +
                //" AND (nClaimNo = " + ClaimNo + ") " +
                //" AND (nPaymentTransactionID = " + PaymentTransactionId + ") " +
                //" AND (nClinicID = " + this.ClinicID + ") " +
                //" ORDER BY PD_nTransactionType desc "; 
                #endregion

                _sqlQuery = " SELECT  ISNULL(nPaymentTransactionID,0) AS PD_nPaymentTransactionID,   ISNULL(nPaymentTransactionDetailID, 0) AS PD_nPaymentTransactionDetailID, ISNULL(nPaymentDate, 0) AS PD_nPaymentDate, " +
               " ISNULL(nBillingTransactionID, 0) AS PD_nBillingTransactionID, ISNULL(nBillingTransactionDetailID, 0) AS PD_nBillingTransactionDetailID,  " +
               " ISNULL(nBillingTransactionLineNo, 0) AS PD_nBillingTransactionLineNo, ISNULL(nClaimNo, 0) AS PD_nClaimNo, ISNULL(sCPTCode, '')  " +
               " AS PD_sCPTCode, ISNULL(sCPTDescription, '') AS PD_sCPTDescription, ISNULL(dCurrentPaymentAmt, 0) AS PD_dCurrentPaymentAmt,  " +
               " ISNULL(dAllowedAmt, 0) AS PD_dAllowedAmt, ISNULL(nCurrentPaymentAmtType, 0) AS PD_nCurrentPaymentAmtType, ISNULL(nTransactionType, 0)  " +
               " AS PD_nTransactionType, ISNULL(nPaymentMode, 0) AS PD_nPaymentMode, ISNULL(sCrdNoMnyOrdNoChkNo, '') AS PD_sCrdNoMnyOrdNoChkNo,  " +
               " ISNULL(nCrdExpChkMnyOrdDate, 0) AS PD_nCrdExpChkMnyOrdDate, ISNULL(sSecurityNo, '') AS PD_sSecurityNo, ISNULL(sCardType, '')  " +
               " AS PD_sCardType, ISNULL(nPaymentInsuranceID, 0) AS PD_nPaymentInsuranceID, ISNULL(nPayerModeID, 0) AS PD_nPayerModeID,  " +
               " ISNULL(nPaymentTransactionLineStatus, 0) AS PD_nPaymentTransactionLineStatus, ISNULL(nClinicID, 0) AS PD_nClinicID,  " +
               " ISNULL(nCurrentPaymentCopayID, 0) AS PD_nCurrentPaymentCopayID, ISNULL(nCoPayID, 0) AS PD_nCoPayID, ISNULL(nTransactionTypeDetailID, 0)  " +
               " AS PD_nTransactionTypeDetailID, ISNULL(sTransactionTypeDetailName, '') AS PD_sTransactionTypeDetailName, ISNULL(nRefID, 0) AS PD_nRefID,  " +
               " ISNULL(sRefCode, '') AS PD_sRefCode, ISNULL(nRefType, 0) AS PD_nRefType, ISNULL(sAuthorizationNo, '') AS PD_sAuthorizationNo, ISNULL(nCardID,0) AS PD_nCardID,  " +
               " ISNULL(nUserID, 0) AS PD_nUserID, ISNULL(sUserName, '') AS PD_sUserName, ISNULL(nPatientID, 0) AS PD_nPatientID, " +
               " (SELECT ISNULL(SUM(dCurrentPaymentAmt),0) " +
               " FROM BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_1 " +
               " WHERE  " +
               " BL_Transaction_Payment_DTL_1.nPaymentTransactionID = " + PaymentTransactionId + " " +
               " AND BL_Transaction_Payment_DTL_1.nClaimNo = " + ClaimNo + " " +
               " AND BL_Transaction_Payment_DTL_1.nBillingTransactionID = " + BillingTranId + " " +
               " AND BL_Transaction_Payment_DTL_1.nBillingTransactionDetailID = " + BillingTranDtlId + " " +
               " AND BL_Transaction_Payment_DTL_1.nPatientID = " + PatientID + " " +
               " AND (BL_Transaction_Payment_DTL_1.nClinicID = " + this.ClinicID + ") " +
               " AND BL_Transaction_Payment_DTL_1.nTransactionType = BL_Transaction_Payment_DTL.nTransactionType)  AS TotalCurrentPayment, " +
               //" ISNULL(BL_Transaction_OtherPayments.dAmount,0) AS OP_ChargedAmount " +

               " (SELECT ISNULL(SUM(BL_Transaction_OtherPayments.dAmount), 0) "+
               " FROM  BL_Transaction_OtherPayments  "+
               " WHERE  "+
               " (nTransactionID = " + BillingTranId + " ) AND  " +
               " (nTransactionDetailID = " + BillingTranDtlId + ") AND  " +
               " (nAmountType = BL_Transaction_Payment_DTL.nTransactionType)) AS OP_ChargedAmount " +

               //" FROM BL_Transaction_Payment_DTL INNER JOIN "+
                    //" BL_Transaction_OtherPayments ON BL_Transaction_Payment_DTL.nBillingTransactionID = BL_Transaction_OtherPayments.nTransactionID AND  "+
                    //" BL_Transaction_Payment_DTL.nBillingTransactionDetailID = BL_Transaction_OtherPayments.nTransactionDetailID AND  "+
                    //" BL_Transaction_Payment_DTL.nBillingTransactionLineNo = BL_Transaction_OtherPayments.nTransactionLineNo AND  "+
                    //" BL_Transaction_Payment_DTL.nTransactionType = BL_Transaction_OtherPayments.nAmountType AND  "+
                    //" BL_Transaction_Payment_DTL.nPaymentTransactionID = BL_Transaction_OtherPayments.nPaymentID AND "+
                    //" BL_Transaction_Payment_DTL.nPaymentTransactionDetailID = BL_Transaction_OtherPayments.nPaymentDetailID " +

               " FROM BL_Transaction_Payment_DTL LEFT OUTER JOIN " +
               " BL_Transaction_OtherPayments ON BL_Transaction_Payment_DTL.nBillingTransactionID = BL_Transaction_OtherPayments.nTransactionID AND  " +
               " BL_Transaction_Payment_DTL.nBillingTransactionDetailID = BL_Transaction_OtherPayments.nTransactionDetailID AND  " +
               " BL_Transaction_Payment_DTL.nBillingTransactionLineNo = BL_Transaction_OtherPayments.nTransactionLineNo AND  " +
               " BL_Transaction_Payment_DTL.nTransactionType = BL_Transaction_OtherPayments.nAmountType AND  " +
               " BL_Transaction_Payment_DTL.nPaymentTransactionID = BL_Transaction_OtherPayments.nPaymentID " +

               " WHERE      " +
               " (nPatientID = " + PatientID + ")  " +
               " AND (nClaimNo = " + ClaimNo + ") " +
               " AND (nBillingTransactionID = " + BillingTranId + ") " +
               " AND (nBillingTransactionDetailID = " + BillingTranDtlId + ") " +
               " AND (nPaymentTransactionID = " + PaymentTransactionId + ") " +
               " AND (nClinicID = " + this.ClinicID + ") " +
               " ORDER BY PD_nTransactionType desc ";
               
                #endregion

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtPayDtls);
                oDB.Disconnect();

                if (_dtPayDtls != null && _dtPayDtls.Rows.Count > 0)
                {
                    for (int i = 0; i < _dtPayDtls.Rows.Count; i++)
                    {
                        //ChargedAmount//Current_Payment//Paid//Pending_Balance//Notes//InsuranceID//InsuranceName//Transaction_Type
                        //PaymentDetails//TransactionID//TransactionDetailID//TransactionLineNo//DBPendingAmount//DataHasToSave

                        if (_trnType != ((TransactionType)_dtPayDtls.Rows[i]["PD_nTransactionType"]))
                        {
                            _clmLnPayment = new ClaimLinePayment();
                            _clmLnPayment.Transaction_Type = ((TransactionType)_dtPayDtls.Rows[i]["PD_nTransactionType"]);
                            _clmLnPayment.TransactionID = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nBillingTransactionID"]);
                            _clmLnPayment.TransactionDetailID = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nBillingTransactionDetailID"]);
                            _clmLnPayment.TransactionLineNo = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nBillingTransactionLineNo"]);
                            _clmLnPayment.ChargedAmount = Convert.ToInt64(_dtPayDtls.Rows[i]["OP_ChargedAmount"]);
                            _clmLnPayment.Current_Payment = Convert.ToDecimal(_dtPayDtls.Rows[i]["TotalCurrentPayment"]);
                            _clmLnPayment.InsuranceID = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nPaymentInsuranceID"]);
                            _clmLnPayment.InsuranceName = "";
                            _clmLnPayment.Paid = 0;
                            _clmLnPayment.Pending_Balance = _clmLnPayment.ChargedAmount - _clmLnPayment.Current_Payment; //0;
                            _clmLnPayment.DBPendingAmount = 0;
                            _clmLnPayment.Notes = null;

                            _clmLnPayment.PaymentTransactionID = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nPaymentTransactionID"]);
                            _clmLnPayment.PaymentTransactiondetailID = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nPaymentTransactionDetailID"]);
                            _clmLnPayment.DataHasToSave = true;

                            _trnType = _clmLnPayment.Transaction_Type;
                        }

                        #region " Set Amount Details & PayMode Details "

                        _clmLnPaymentDtl = new ClaimLinePaymentDetail();
                        _clmLnPaymentDtl.PayAmount = Convert.ToDecimal(_dtPayDtls.Rows[i]["PD_dCurrentPaymentAmt"]);
                        _clmLnPaymentDtl.PayAmountType = ((PaymentOtherType)Convert.ToInt32(_dtPayDtls.Rows[i]["PD_nCurrentPaymentAmtType"]));
                        _clmLnPaymentDtl.PaymentID = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nCoPayID"]);

                        _clmLnPaymentDtl.AmountPaymentMode.Adjustment_Type = Convert.ToString(_dtPayDtls.Rows[i]["PD_sTransactionTypeDetailName"]);
                        _clmLnPaymentDtl.AmountPaymentMode.AdjustmentTypeID = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nTransactionTypeDetailID"]);
                        _clmLnPaymentDtl.AmountPaymentMode.CardAuthorizationNo = Convert.ToString(_dtPayDtls.Rows[i]["PD_sAuthorizationNo"]);
                        _clmLnPaymentDtl.AmountPaymentMode.CardSecurityNumber = Convert.ToString(_dtPayDtls.Rows[i]["PD_sSecurityNo"]);
                        _clmLnPaymentDtl.AmountPaymentMode.CardType = Convert.ToString(_dtPayDtls.Rows[i]["PD_sCardType"]);
                        _clmLnPaymentDtl.AmountPaymentMode.CardTypeID = Convert.ToInt32(_dtPayDtls.Rows[i]["PD_nCardID"]);
                        _clmLnPaymentDtl.AmountPaymentMode.CheckMoneyOrderCardEFT_Number = Convert.ToString(_dtPayDtls.Rows[i]["PD_sCrdNoMnyOrdNoChkNo"]);
                        _clmLnPaymentDtl.AmountPaymentMode.CheckMoneyOrderCardExpiryEFT_Date = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nCrdExpChkMnyOrdDate"]);
                        _clmLnPaymentDtl.AmountPaymentMode.PayerMode = ((PayerMode)Convert.ToInt32(_dtPayDtls.Rows[i]["PD_nPayerModeID"]));
                        _clmLnPaymentDtl.AmountPaymentMode.PaymentMode = ((PaymentMode)Convert.ToInt32(_dtPayDtls.Rows[i]["PD_nPaymentMode"]));
                        _clmLnPaymentDtl.AmountPaymentMode.TransactionTypeMode = ((TransactionType)Convert.ToInt32(_dtPayDtls.Rows[i]["PD_nTransactionType"]));

                        _clmLnPayment.PaymentDetails.Add(_clmLnPaymentDtl);
                        //_clmLnPaymentDtl = null; 

                        #endregion

                        bool _addClaimLnPayment = true;

                        if (_clmLnPayments != null && _clmLnPayments.Count > 0)
                        {
                            for (int index = 0; index < _clmLnPayments.Count; index++)
                            {
                                if (_clmLnPayments[index].Transaction_Type == _trnType && _clmLnPayments[index].PaymentDetails.Count == _clmLnPayment.PaymentDetails.Count)
                                {
                                    _addClaimLnPayment = false;
                                }
                            }
                        }
                        
                        if(_addClaimLnPayment)
                        {
                            if (_clmLnPaymentDtl != null)
                            {
                                _clmLnPayments.Add(_clmLnPayment);
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return _clmLnPayments;
        }

        public ClaimLinePayments GetClaimLinePayments(Int64 PaymentTransactionId, Int64 ClaimNo, Int64 ClinicID, Int64 PatientID, Int64 BillingTranId, Int64 BillingTranDtlId,Int64 ReceivedPaymentCounter)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtPayDtls = null;
            string _sqlQuery = "";
            ClaimLinePayments _clmLnPayments = new ClaimLinePayments();
            ClaimLinePayment _clmLnPayment = null;
            ClaimLinePaymentDetails _clmLnPaymentDtls = new ClaimLinePaymentDetails();
            ClaimLinePaymentDetail _clmLnPaymentDtl = null;
            PaymentModeDetails _payModeDetls = new PaymentModeDetails();
         //   PaymentModeDetail _payModeDtl = null;
            TransactionType _trnType = TransactionType.None;

            try
            {
                #region " SQL Query to retrive payment details "

                _sqlQuery = " SELECT  ISNULL(nPaymentTransactionID,0) AS PD_nPaymentTransactionID,   ISNULL(nPaymentTransactionDetailID, 0) AS PD_nPaymentTransactionDetailID, ISNULL(nPaymentDate, 0) AS PD_nPaymentDate, " +
               " ISNULL(nBillingTransactionID, 0) AS PD_nBillingTransactionID, ISNULL(nBillingTransactionDetailID, 0) AS PD_nBillingTransactionDetailID,  " +
               " ISNULL(nBillingTransactionLineNo, 0) AS PD_nBillingTransactionLineNo, ISNULL(nClaimNo, 0) AS PD_nClaimNo, ISNULL(sCPTCode, '')  " +
               " AS PD_sCPTCode, ISNULL(sCPTDescription, '') AS PD_sCPTDescription, ISNULL(dCurrentPaymentAmt, 0) AS PD_dCurrentPaymentAmt,  " +
               " ISNULL(dAllowedAmt, 0) AS PD_dAllowedAmt, ISNULL(nCurrentPaymentAmtType, 0) AS PD_nCurrentPaymentAmtType, ISNULL(nTransactionType, 0)  " +
               " AS PD_nTransactionType, ISNULL(nPaymentMode, 0) AS PD_nPaymentMode, ISNULL(sCrdNoMnyOrdNoChkNo, '') AS PD_sCrdNoMnyOrdNoChkNo,  " +
               " ISNULL(nCrdExpChkMnyOrdDate, 0) AS PD_nCrdExpChkMnyOrdDate, ISNULL(sSecurityNo, '') AS PD_sSecurityNo, ISNULL(sCardType, '')  " +
               " AS PD_sCardType, ISNULL(nPaymentInsuranceID, 0) AS PD_nPaymentInsuranceID, ISNULL(nPayerModeID, 0) AS PD_nPayerModeID,  " +
               " ISNULL(nPaymentTransactionLineStatus, 0) AS PD_nPaymentTransactionLineStatus, ISNULL(nClinicID, 0) AS PD_nClinicID,  " +
               " ISNULL(nCurrentPaymentCopayID, 0) AS PD_nCurrentPaymentCopayID, ISNULL(nCoPayID, 0) AS PD_nCoPayID, ISNULL(nTransactionTypeDetailID, 0)  " +
               " AS PD_nTransactionTypeDetailID, ISNULL(sTransactionTypeDetailName, '') AS PD_sTransactionTypeDetailName, ISNULL(nRefID, 0) AS PD_nRefID,  " +
               " ISNULL(sRefCode, '') AS PD_sRefCode, ISNULL(nRefType, 0) AS PD_nRefType, ISNULL(sAuthorizationNo, '') AS PD_sAuthorizationNo, ISNULL(nCardID,0) AS PD_nCardID,  " +
               " ISNULL(nUserID, 0) AS PD_nUserID, ISNULL(sUserName, '') AS PD_sUserName, ISNULL(nPatientID, 0) AS PD_nPatientID, " +
               " (SELECT ISNULL(SUM(dCurrentPaymentAmt),0) " +
               " FROM BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_1 " +
               " WHERE  " +
               " BL_Transaction_Payment_DTL_1.nPaymentTransactionID = " + PaymentTransactionId + " " +
               " AND BL_Transaction_Payment_DTL_1.nClaimNo = " + ClaimNo + " " +
               " AND BL_Transaction_Payment_DTL_1.nBillingTransactionID = " + BillingTranId + " " +
               " AND BL_Transaction_Payment_DTL_1.nBillingTransactionDetailID = " + BillingTranDtlId + " " +
               " AND BL_Transaction_Payment_DTL_1.nPatientID = " + PatientID + " " +
               " AND (BL_Transaction_Payment_DTL_1.nClinicID = " + this.ClinicID + ") " +
               " AND BL_Transaction_Payment_DTL_1.nTransactionType = BL_Transaction_Payment_DTL.nTransactionType)  AS TotalCurrentPayment, " +
                    //" ISNULL(BL_Transaction_OtherPayments.dAmount,0) AS OP_ChargedAmount " +

               " (SELECT ISNULL(SUM(BL_Transaction_OtherPayments.dAmount), 0) " +
               " FROM  BL_Transaction_OtherPayments  " +
               " WHERE  " +
               " (nTransactionID = " + BillingTranId + " ) AND  " +
               " (nTransactionDetailID = " + BillingTranDtlId + ") AND  " +
               " (nReceivedPaymentCounter = "+ReceivedPaymentCounter+") AND "+
               " (nAmountType = BL_Transaction_Payment_DTL.nTransactionType)) AS OP_ChargedAmount " +

               //" FROM BL_Transaction_Payment_DTL INNER JOIN "+
                    //" BL_Transaction_OtherPayments ON BL_Transaction_Payment_DTL.nBillingTransactionID = BL_Transaction_OtherPayments.nTransactionID AND  "+
                    //" BL_Transaction_Payment_DTL.nBillingTransactionDetailID = BL_Transaction_OtherPayments.nTransactionDetailID AND  "+
                    //" BL_Transaction_Payment_DTL.nBillingTransactionLineNo = BL_Transaction_OtherPayments.nTransactionLineNo AND  "+
                    //" BL_Transaction_Payment_DTL.nTransactionType = BL_Transaction_OtherPayments.nAmountType AND  "+
                    //" BL_Transaction_Payment_DTL.nPaymentTransactionID = BL_Transaction_OtherPayments.nPaymentID AND "+
                    //" BL_Transaction_Payment_DTL.nPaymentTransactionDetailID = BL_Transaction_OtherPayments.nPaymentDetailID " +

               " FROM BL_Transaction_Payment_DTL LEFT OUTER JOIN " +
               " BL_Transaction_OtherPayments ON BL_Transaction_Payment_DTL.nBillingTransactionID = BL_Transaction_OtherPayments.nTransactionID AND  " +
               " BL_Transaction_Payment_DTL.nBillingTransactionDetailID = BL_Transaction_OtherPayments.nTransactionDetailID AND  " +
               " BL_Transaction_Payment_DTL.nBillingTransactionLineNo = BL_Transaction_OtherPayments.nTransactionLineNo AND  " +
               " BL_Transaction_Payment_DTL.nTransactionType = BL_Transaction_OtherPayments.nAmountType AND  " +
               " BL_Transaction_Payment_DTL.nPaymentTransactionID = BL_Transaction_OtherPayments.nPaymentID " +

               " WHERE      " +
               " (nPatientID = " + PatientID + ")  " +
               " AND (nClaimNo = " + ClaimNo + ") " +
               " AND (nBillingTransactionID = " + BillingTranId + ") " +
               " AND (nBillingTransactionDetailID = " + BillingTranDtlId + ") " +
               " AND (nPaymentTransactionID = " + PaymentTransactionId + ") " +
               " AND (nClinicID = " + this.ClinicID + ") " +
               " ORDER BY PD_nTransactionType desc ";

                #endregion

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtPayDtls);
                oDB.Disconnect();

                if (_dtPayDtls != null && _dtPayDtls.Rows.Count > 0)
                {
                    for (int i = 0; i < _dtPayDtls.Rows.Count; i++)
                    {
                        //ChargedAmount//Current_Payment//Paid//Pending_Balance//Notes//InsuranceID//InsuranceName//Transaction_Type
                        //PaymentDetails//TransactionID//TransactionDetailID//TransactionLineNo//DBPendingAmount//DataHasToSave

                        if (_trnType != ((TransactionType)_dtPayDtls.Rows[i]["PD_nTransactionType"]))
                        {
                            _clmLnPayment = new ClaimLinePayment();
                            _clmLnPayment.Transaction_Type = ((TransactionType)_dtPayDtls.Rows[i]["PD_nTransactionType"]);
                            _clmLnPayment.TransactionID = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nBillingTransactionID"]);
                            _clmLnPayment.TransactionDetailID = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nBillingTransactionDetailID"]);
                            _clmLnPayment.TransactionLineNo = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nBillingTransactionLineNo"]);
                            _clmLnPayment.ChargedAmount = Convert.ToInt64(_dtPayDtls.Rows[i]["OP_ChargedAmount"]);
                            _clmLnPayment.Current_Payment = Convert.ToDecimal(_dtPayDtls.Rows[i]["TotalCurrentPayment"]);
                            _clmLnPayment.InsuranceID = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nPaymentInsuranceID"]);
                            _clmLnPayment.InsuranceName = "";
                            _clmLnPayment.Paid = 0;
                            _clmLnPayment.Pending_Balance = _clmLnPayment.ChargedAmount - _clmLnPayment.Current_Payment; //0;
                            _clmLnPayment.DBPendingAmount = 0;
                            _clmLnPayment.Notes = null;

                            _clmLnPayment.PaymentTransactionID = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nPaymentTransactionID"]);
                            _clmLnPayment.PaymentTransactiondetailID = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nPaymentTransactionDetailID"]);
                            _clmLnPayment.DataHasToSave = true;

                            _trnType = _clmLnPayment.Transaction_Type;
                        }

                        #region " Set Amount Details & PayMode Details "

                        _clmLnPaymentDtl = new ClaimLinePaymentDetail();
                        _clmLnPaymentDtl.PayAmount = Convert.ToDecimal(_dtPayDtls.Rows[i]["PD_dCurrentPaymentAmt"]);
                        _clmLnPaymentDtl.PayAmountType = ((PaymentOtherType)Convert.ToInt32(_dtPayDtls.Rows[i]["PD_nCurrentPaymentAmtType"]));
                        _clmLnPaymentDtl.PaymentID = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nCoPayID"]);

                        _clmLnPaymentDtl.AmountPaymentMode.Adjustment_Type = Convert.ToString(_dtPayDtls.Rows[i]["PD_sTransactionTypeDetailName"]);
                        _clmLnPaymentDtl.AmountPaymentMode.AdjustmentTypeID = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nTransactionTypeDetailID"]);
                        _clmLnPaymentDtl.AmountPaymentMode.CardAuthorizationNo = Convert.ToString(_dtPayDtls.Rows[i]["PD_sAuthorizationNo"]);
                        _clmLnPaymentDtl.AmountPaymentMode.CardSecurityNumber = Convert.ToString(_dtPayDtls.Rows[i]["PD_sSecurityNo"]);
                        _clmLnPaymentDtl.AmountPaymentMode.CardType = Convert.ToString(_dtPayDtls.Rows[i]["PD_sCardType"]);
                        _clmLnPaymentDtl.AmountPaymentMode.CardTypeID = Convert.ToInt32(_dtPayDtls.Rows[i]["PD_nCardID"]);
                        _clmLnPaymentDtl.AmountPaymentMode.CheckMoneyOrderCardEFT_Number = Convert.ToString(_dtPayDtls.Rows[i]["PD_sCrdNoMnyOrdNoChkNo"]);
                        _clmLnPaymentDtl.AmountPaymentMode.CheckMoneyOrderCardExpiryEFT_Date = Convert.ToInt64(_dtPayDtls.Rows[i]["PD_nCrdExpChkMnyOrdDate"]);
                        _clmLnPaymentDtl.AmountPaymentMode.PayerMode = ((PayerMode)Convert.ToInt32(_dtPayDtls.Rows[i]["PD_nPayerModeID"]));
                        _clmLnPaymentDtl.AmountPaymentMode.PaymentMode = ((PaymentMode)Convert.ToInt32(_dtPayDtls.Rows[i]["PD_nPaymentMode"]));
                        _clmLnPaymentDtl.AmountPaymentMode.TransactionTypeMode = ((TransactionType)Convert.ToInt32(_dtPayDtls.Rows[i]["PD_nTransactionType"]));

                        _clmLnPayment.PaymentDetails.Add(_clmLnPaymentDtl);
                        //_clmLnPaymentDtl = null; 

                        #endregion

                        bool _addClaimLnPayment = true;

                        if (_clmLnPayments != null && _clmLnPayments.Count > 0)
                        {
                            for (int index = 0; index < _clmLnPayments.Count; index++)
                            {
                                if (_clmLnPayments[index].Transaction_Type == _trnType && _clmLnPayments[index].PaymentDetails.Count == _clmLnPayment.PaymentDetails.Count)
                                {
                                    _addClaimLnPayment = false;
                                }
                            }
                        }

                        if (_addClaimLnPayment)
                        {
                            if (_clmLnPaymentDtl != null)
                            {
                                _clmLnPayments.Add(_clmLnPayment);
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return _clmLnPayments;
        }

        public void DeletePayment(Int64 PaymentTransactionId,Int64 ClaimNo,Int64 BillingTransactionId,Int64 PatientId,Int64 ClinicId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "DELETE FROM BL_Transaction_Payment_DTL WHERE nPaymentTransactionID = " + PaymentTransactionId + " AND nClaimNo = " + ClaimNo + " AND nPatientID = " + PatientId + " AND nClinicID = " + ClinicId + " AND nBillingTransactionID = " + BillingTransactionId + "";
                int retVal = oDB.Execute_Query(_sqlQuery);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            finally
            { }
        }

        #endregion " Save Payment "

        public Int64 AddCoPayment(gloCoPayment oCopayment)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Int64 _CoPaymentID = 0;
            object CoPaymentID = null;

            try
            {
                oDB.Connect(false);

                oDBParameters.Add("@nCoPayID", oCopayment.CopayID , ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@nPatientID", oCopayment.PatientID , ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nInsuranceID",oCopayment.InsuranceID , ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nAppointmentID",oCopayment.AppointmentID , ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionDate",oCopayment.TransactionDate , ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nAppointmentDate",oCopayment.AppointmentDate , ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dCoPayAmount",oCopayment.CopayAmount , ParameterDirection.Input, SqlDbType.Decimal);
                if (oCopayment.IsApplied == true)
                {
                    oDBParameters.Add("@bIsApplied", 1, ParameterDirection.Input, SqlDbType.Bit);
                }
                else
                {
                    oDBParameters.Add("@bIsApplied", 0, ParameterDirection.Input, SqlDbType.Bit);
                }
                oDBParameters.Add("@nPaymentMode",oCopayment.PaymentMode.GetHashCode() , ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nCrdExpChkMnyOrdDate",oCopayment.CheckMoneyOrderCardExpiryDate , ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sCrdNoMnyOrdNoChkNo",oCopayment.CheckMoneyOrderCardNumber , ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sSecurityNo",oCopayment.CardSecurityNumber , ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sCardType",oCopayment.CardType , ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@nPayerModeID",oCopayment.PayerMode.GetHashCode() , ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nOtherPaymentType", oCopayment.OtherPaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", oCopayment.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("BL_INUP_Transaction_CoPay_MST", oDBParameters, out CoPaymentID);

                if (CoPaymentID != null && Convert.ToString(CoPaymentID) != "")
                {
                    _CoPaymentID = Convert.ToInt64(CoPaymentID);
                }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                _CoPaymentID = 0;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
            }
            return _CoPaymentID;
        }
        
        public gloCoPayments GetCoPayment(Int64 PatientID, Int64 ClinicID)
        {
            return null;
        }

        public void UpdateClaimLineStatus(Int64 Transactionid, Int64 TransactionDtlId, Int64 TranLineNo, CliamLineUserStatus ClmLineUserStatus,Int64 ClinicID,InsuranceTypeFlag SentToFlag,bool IsSentToFlagUpdate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    Int64 _ClmLineStatusId = 0;
            string _sqlQuery = "";
            
            try
            {

                if (IsSentToFlagUpdate == true)
                {
                    _sqlQuery = " UPDATE BL_Transaction_Lines SET nSendToFlag = " + SentToFlag.GetHashCode() + " " +
                    " WHERE " +
                    " nTransactionID = " + Transactionid + " " +
                    " AND nTransactionDetailID = " + TransactionDtlId + " " +
                    " AND nTransactionLineNo = " + TranLineNo + " " +
                    " AND nClinicID = " + ClinicID + " ";
                }
                else
                {
                    _sqlQuery = " UPDATE BL_Transaction_Lines SET nClaimLineStatusID = " + ClmLineUserStatus.GetHashCode() + ", " +
                    " sClaimLineStatus = '" + ClmLineUserStatus.ToString() + "' " +
                    " WHERE " +
                    " nTransactionID = " + Transactionid + " " +
                    " AND nTransactionDetailID = " + TransactionDtlId + " " +
                    " AND nTransactionLineNo = " + TranLineNo + " " +
                    " AND nClinicID = " + ClinicID + " ";
                }

                oDB.Connect(false);
                oDB.Execute_Query(_sqlQuery);
                oDB.Disconnect();
                 
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            { if (oDB != null) { oDB.Dispose(); } }
        }

        #region "Need to delete - Refered in FillPendingCheck_Old() not in use"
        public DataTable GetCheckAmountDiff(Int64 UserId, Int64 ClinicId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dt = new DataTable();
            string _sqlQuery = "";

            try
            {
                #region " SQL Query "

                _sqlQuery = " SELECT *,(CheckAmount - AppliedAmount) AS PendingAmount FROM " +
                        " ( " +
                        " SELECT DISTINCT " +
                        " ISNULL(BL_Transaction_Payment_MST_MultiPayment.sCrdNoMnyOrdNoChkNo,'') AS CheckNo, " +
                        " CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(BL_Transaction_Payment_MST_MultiPayment.nCrdExpChkMnyOrdDate), 101) AS CheckDate, " +
                        //" ISNULL(BL_Transaction_Payment_MST_MultiPayment.nCrdExpChkMnyOrdDate,0) AS Check Date, " +
                        //" ISNULL(BL_Transaction_Payment_MST_MultiPayment.dPayerAmount,0) AS CheckAmount, " +
                        " ISNULL(BL_Transaction_Payment_MST_MultiPayment.dPayerAmount,0) AS CheckAmount    , " +
                        " ISNULL(BL_Transaction_Payment_MST_MultiPayment.nMultiPaymentTransactionID,0) AS nMultiPaymentTransactionID, " +
                        " (SELECT DISTINCT    ISNULL(SUM(dPayerAmount), 0) " +
                        " FROM          BL_Transaction_Payment_MST " +
                        " WHERE       " +
                        " nUserID = " + UserId + " " +
                        " AND nClinicID = " + ClinicId + " " +
                        " AND BL_Transaction_Payment_MST.nPaymentMode = " + PaymentMode.Check.GetHashCode() + " " +
                        " AND BL_Transaction_Payment_MST.sCrdNoMnyOrdNoChkNo IS NOT NULL  " +
                        " AND BL_Transaction_Payment_MST.sCrdNoMnyOrdNoChkNo <> '' " +
                        " AND BL_Transaction_Payment_MST_MultiPayment.nMasterTransactionType <> " + TransactionType.Refund.GetHashCode() + " " +
                        " AND BL_Transaction_Payment_MST_MultiPayment.sCrdNoMnyOrdNoChkNo = BL_Transaction_Payment_MST.sCrdNoMnyOrdNoChkNo) AS AppliedAmount " +
                        " FROM         BL_Transaction_Payment_MST INNER JOIN " +
                        " BL_Transaction_Payment_MST_MultiPayment ON  " +
                        " BL_Transaction_Payment_MST.nMultiPaymentTransactionID = BL_Transaction_Payment_MST_MultiPayment.nMultiPaymentTransactionID AND  " +
                        " BL_Transaction_Payment_MST.sCrdNoMnyOrdNoChkNo = BL_Transaction_Payment_MST_MultiPayment.sCrdNoMnyOrdNoChkNo " +
                        " WHERE " +
                        " BL_Transaction_Payment_MST_MultiPayment.nPaymentMode = " + PaymentMode.Check.GetHashCode() + " " +
                        " AND BL_Transaction_Payment_MST_MultiPayment.nUserID = " + UserId + " " +
                        " AND BL_Transaction_Payment_MST_MultiPayment.nClinicID = " + ClinicId + " " +
                        " AND BL_Transaction_Payment_MST_MultiPayment.sCrdNoMnyOrdNoChkNo IS NOT NULL  " +
                        " AND BL_Transaction_Payment_MST_MultiPayment.sCrdNoMnyOrdNoChkNo <> '' " +
                        " AND BL_Transaction_Payment_MST_MultiPayment.nMasterTransactionType <> " + TransactionType.Refund.GetHashCode() + " " +
                        //" GROUP BY "+
                        //" BL_Transaction_Payment_MST_MultiPayment.sCrdNoMnyOrdNoChkNo, "+
                        //" BL_Transaction_Payment_MST_MultiPayment.nCrdExpChkMnyOrdDate " +
                        " ) AS TempTable " +
                        " WHERE " +
                        " (CheckAmount - AppliedAmount) > 0 ";

                #endregion

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dt);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            { if (oDB != null) { oDB.Dispose(); } }

            return _dt;
        }
        #endregion

        public DataTable GetMultiplePaymentDetail(Int64 MultipleTrnDtlId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dt = new DataTable();
            string _sqlQuery = "";

            try
            {
                #region " SQL Query "

                _sqlQuery = " SELECT ISNULL(nPaymentDate,0) AS nPaymentDate,ISNULL(nPaymentTime,0) AS nPaymentTime, " +
                        " ISNULL(nPaymentMode,0) AS nPaymentMode,ISNULL(dPayerAmount,0) AS dPayerAmount,  " +
                        " ISNULL(sCrdNoMnyOrdNoChkNo,'') AS sCrdNoMnyOrdNoChkNo,  " +
                        " ISNULL(nCrdExpChkMnyOrdDate,0) AS nCrdExpChkMnyOrdDate, " +
                        " ISNULL(nPaymentInsuranceID,0) AS nPaymentInsuranceID,  " +
                        " ISNULL(sPaymentInsurance,'') AS sPaymentInsurance,  " +
                        " ISNULL(nMasterTransactionType,0) AS nMasterTransactionType " +
                        " FROM " +
                        " BL_Transaction_Payment_MST_MultiPayment " +
                        " WHERE " +
                        " nMultiPaymentTransactionID = " + MultipleTrnDtlId + "  " +
                        " AND nClinicID = " + _ClinicID + " ";

                #endregion

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dt);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            { if (oDB != null) { oDB.Dispose(); } }

            return _dt;
        }

    }

    public class gloCoPayment
    {
        #region "Constructor & Distructor"
        public gloCoPayment()
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

        ~gloCoPayment()
        {
            Dispose(false);
        }

        #endregion

        #region "Variables Declarations"
        public Int64 CopayID = 0;
        public Int64 PatientID = 0;
        public Int64 InsuranceID = 0;
        public Int64 AppointmentID = 0;
        public Int64 TransactionDate = 0;
        public Int64 AppointmentDate = 0;
        public Decimal CopayAmount = 0;
        public Boolean IsApplied = false;
        public PaymentMode PaymentMode = PaymentMode.None;
        public Int64 CheckMoneyOrderCardExpiryDate = 0;
        public String CheckMoneyOrderCardNumber = "";
        public String CardSecurityNumber = "";
        public String CardType = "";
        public PayerMode PayerMode = PayerMode.None;
        public PaymentOtherType OtherPaymentType = PaymentOtherType.None;  
        public Int64 ClinicID = 0;
        #endregion"Variables Declarations"
    }

    public class gloCoPayments
    {

        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public gloCoPayments()
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

                }
            }
            disposed = true;
        }


        ~gloCoPayments()
        {
            Dispose(false);
        }
        #endregion

        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(gloCoPayment item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(gloCoPayment item)
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

        public gloCoPayment this[int index]
        {
            get
            { return (gloCoPayment)_innerlist[index]; }
        }

        public bool Contains(gloCoPayment item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(gloCoPayment item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(gloCoPayment[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    public class AdvancePayment
    {
        #region "Constructor & Distructor"

        public AdvancePayment()
        {
            _CPTCodes = new gloGeneralItem.gloItems();
            _DxCodes = new gloGeneralItem.gloItems();
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
                    if (bDxCodeAssigned)
                    {
                        if (_DxCodes != null)
                        {
                            _DxCodes.Clear();
                            _DxCodes.Dispose();
                            _DxCodes = null;
                        }
                        bDxCodeAssigned = false;
                    }
                    if (bCPTAssigned)
                    {
                        if (_CPTCodes != null)
                        {
                            _CPTCodes.Clear();
                            _CPTCodes.Dispose();
                            _CPTCodes = null;
                        }
                        bCPTAssigned = false;
                    }
                }
            }
            disposed = true;
        }

        ~AdvancePayment()
        {
            Dispose(false);
        }

        #endregion

        #region "Variables Declarations"

        private Int64 _PaymentID = 0;
        private Int64 _PatientID = 0;
        private Int64 _InsuranceID = 0;
        private Int64 _AppointmentID = 0;
        private Int64 _TransactionDate = 0;
        private Int64 _AppointmentDate = 0;
        private Decimal _PaymentAmount = 0;
        private Boolean _IsApplied = false;
        private PaymentMode _PaymentMode = PaymentMode.None;
        private Int64 _CheckMoneyOrderCardExpiryDate = 0;
        private String _CheckMoneyOrderCardNumber = "";
        private String _CardSecurityNumber = "";
        private String _CardType = "";
        private PayerMode _PayerMode = PayerMode.None;
        private PaymentOtherType _OtherPaymentType = PaymentOtherType.None;
        private Int64 _ClinicID = 0;
        private string _CPTCode = "";
        private string _DxCode = "";
        private gloGeneralItem.gloItems _CPTCodes = null;
        private gloGeneralItem.gloItems _DxCodes = null;
        private Int64 _RefrenceId = 0;
        private string _Note = "";
        private string _CardAuthorizationNo = "";
        //Added By MaheshB
        private Int64 _nCloseDayTrayID = 0;
        private string _sCloseDayCode = "";
        private string _sCloseDayTrayDescription = "";
        private string _sReceiptNo = "";
        private string _sPrefix = "";

        #endregion"Variables Declarations"

        #region " Property Procedures "

        public Int64 PaymentID
        {
            get { return _PaymentID; }
            set { _PaymentID = value; }
        }
        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        public Int64 InsuranceID
        {
            get { return _InsuranceID; }
            set { _InsuranceID = value; }
        }
        public Int64 AppointmentID
        {
            get { return _AppointmentID; }
            set { _AppointmentID = value; }
        }
        public Int64 TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        public Int64 AppointmentDate
        {
            get { return _AppointmentDate; }
            set { _AppointmentDate = value; }
        }
        public Decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
        public Boolean IsApplied
        {
            get { return _IsApplied; }
            set { _IsApplied = value; }
        }
        public PaymentMode PaymentMode
        {
            get { return _PaymentMode; }
            set { _PaymentMode = value; }
        }
        public Int64 CheckMoneyOrderCardExpiryDate
        {
            get { return _CheckMoneyOrderCardExpiryDate; }
            set { _CheckMoneyOrderCardExpiryDate = value; }
        }
        public String CheckMoneyOrderCardNumber
        {
            get { return _CheckMoneyOrderCardNumber; }
            set { _CheckMoneyOrderCardNumber = value; }
        }
        public String CardSecurityNumber
        {
            get { return _CardSecurityNumber; }
            set { _CardSecurityNumber = value; }
        }
        public String CardType
        {
            get { return _CardType; }
            set { _CardType = value; }
        }
        public PayerMode PayerMode
        {
            get { return _PayerMode; }
            set { _PayerMode = value; }
        }
        public PaymentOtherType OtherPaymentType
        {
            get { return _OtherPaymentType; }
            set { _OtherPaymentType = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public string CPTCode
        {
            get { return _CPTCode; }
            set { _CPTCode = value; }
        }
        public string DxCode
        {
            get { return _DxCode; }
            set { _DxCode = value; }
        }
        private Boolean bCPTAssigned = true;
        public gloGeneralItem.gloItems CPTCodes
        {
            get { return _CPTCodes; }
            set 
            {
                if (bCPTAssigned)
                {
                    if (_CPTCodes != null)
                    {
                        _CPTCodes.Clear();
                        _CPTCodes.Dispose();
                        _CPTCodes = null;
                    }
                    bCPTAssigned = false;
                }
                _CPTCodes = value; 
            }
        }
        private Boolean bDxCodeAssigned = true;
        public gloGeneralItem.gloItems DxCodes
        {
            get { return _DxCodes; }
            set 
            {
                if (bDxCodeAssigned)
                {
                    if (_DxCodes != null)
                    {
                        _DxCodes.Clear();
                        _DxCodes.Dispose();
                        _DxCodes = null;
                    }
                    bDxCodeAssigned = false;
                }
                _DxCodes = value; 
            }
        }
        public Int64 RefrenceID
        {
            get { return _RefrenceId; }
            set { _RefrenceId = value; }
        }
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        public string CardAuthorizationNo
        {
            get { return _CardAuthorizationNo; }
            set { _CardAuthorizationNo = value; }
        }
        //Added By MaheshB
        public Int64 CloseDayTrayID
        {
            get { return _nCloseDayTrayID; }
            set { _nCloseDayTrayID = value; }
        }
        public string CloseDayCode
        {
            get { return _sCloseDayCode; }
            set { _sCloseDayCode = value; }
        }
        public string CloseDayTrayDescription
        {
            get { return _sCloseDayTrayDescription; }
            set { _sCloseDayTrayDescription = value; }
        }

        public string ReceiptNo
        {
            get { return _sReceiptNo; }
            set { _sReceiptNo = value; }
        }

        public string Prefix
        {
            get { return _sPrefix; }
            set { _sPrefix = value; }
        }

        #endregion

    }

    

    public class AdvancePayments
    {

        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public AdvancePayments()
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

                }
            }
            disposed = true;
        }


        ~AdvancePayments()
        {
            Dispose(false);
        }
        #endregion

        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(AdvancePayment item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(AdvancePayment item)
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

        public AdvancePayment this[int index]
        {
            get
            { return (AdvancePayment)_innerlist[index]; }
        }

        public bool Contains(AdvancePayment item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(AdvancePayment item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(AdvancePayment[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    public class gloAdvancePayment
    {

        #region " Variable Declarations "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "gloPM";
        private Int64 _ClinicID = 0;

        #endregion " Variable Declarations "

        #region " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion " Property Procedures "

        #region "Constructor & Distructor"

        public gloAdvancePayment(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            #region " Retrive ClinicID from App. Config "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion " Retrive ClinicID from App. Config "

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

        ~gloAdvancePayment()
        {
            Dispose(false);
        }

        #endregion

        #region " Public & Private "

        public Int64 SaveAdvancePayment(AdvancePayment oAdvancePayment)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Int64 _PaymentId = 0;
            Object _retVal = null;

            try
            {
                oDBParameters.Add("@nAdvPayID", oAdvancePayment.PaymentID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@nPatientID", oAdvancePayment.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nInsuranceID", oAdvancePayment.InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nAppointmentID", oAdvancePayment.AppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionDate", oAdvancePayment.TransactionDate, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nAppointmentDate", oAdvancePayment.AppointmentDate, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dAdvPayAmount", oAdvancePayment.PaymentAmount, ParameterDirection.Input, SqlDbType.Decimal);
                if (oAdvancePayment.IsApplied == true)
                {oDBParameters.Add("@bIsApplied", 1, ParameterDirection.Input, SqlDbType.Bit);}
                else
                {oDBParameters.Add("@bIsApplied", 0, ParameterDirection.Input, SqlDbType.Bit);}
                oDBParameters.Add("@nPaymentMode", oAdvancePayment.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nCrdExpChkMnyOrdDate", oAdvancePayment.CheckMoneyOrderCardExpiryDate, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sCrdNoMnyOrdNoChkNo",oAdvancePayment.CheckMoneyOrderCardNumber, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sSecurityNo", oAdvancePayment.CardSecurityNumber, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sCardType", oAdvancePayment.CardType, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@nPayerModeID", oAdvancePayment.PayerMode.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nOtherPaymentType", oAdvancePayment.OtherPaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", oAdvancePayment.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sDxCode",oAdvancePayment.DxCode, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sCPTCode", oAdvancePayment.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@nRefrenceID",oAdvancePayment.RefrenceID,ParameterDirection.Input,SqlDbType.BigInt);
                oDBParameters.Add("@sNote",oAdvancePayment.Note, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sAuthorizationNumber", oAdvancePayment.CardAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nCloseDayTrayID", oAdvancePayment.CloseDayTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sCloseDayCode", oAdvancePayment.CloseDayCode, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sCloseDayTrayDescription", oAdvancePayment.CloseDayTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);

                //Added By Pramod Nair For Receipt No
                oDBParameters.Add("@sPrefix", oAdvancePayment.Prefix, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sReceiptNo", oAdvancePayment.ReceiptNo, ParameterDirection.Input, SqlDbType.VarChar);
                
                oDB.Connect(false);
                oDB.Execute("BL_INUP_AdvancePayment", oDBParameters, out _retVal);
                if (_retVal != null && Convert.ToString(_retVal) != "")
                {_PaymentId = Convert.ToInt64(_retVal);}
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {dbEx.ERROR_Log(dbEx.ToString());}
            catch (Exception ex)
            {gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);}
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
            return _PaymentId;
        }

        //public DataTable GetAdvancePayments()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string _sqlQuery = "";
        //    DataTable _dtAdvancePayments = null;

        //    try
        //    {
        //        oDB.Connect(false);

        //        //BL_Transaction_AdvancePayment_MST
        //        //nAdvPayID,nPatientID,nInsuranceID,nAppointmentID,nTransactionDate,nAppointmentDate,dAdvPayAmount,
        //        //bIsApplied,nPaymentMode,nCrdExpChkMnyOrdDate,sCrdNoMnyOrdNoChkNo,sSecurityNo,sCardType,nPayerModeID
        //        //nClinicID,nOtherPaymentMode,sCPTCode,sDxCode,nRefrenceID,sNote

        //        _sqlQuery = " SELECT nAdvPayID,ISNULL(nPatientID,0) AS nPatientID, " +
        //        " ISNULL(nInsuranceID,0) AS nInsuranceID, "+
        //        " ISNULL(nAppointmentID,0) AS nAppointmentID,ISNULL(nTransactionDate,0) AS nTransactionDate,  "+
        //        " ISNULL(nAppointmentDate,0) AS nAppointmentDate,ISNULL(dAdvPayAmount,0) AS dAdvPayAmount, " +
        //        " ISNULL(bIsApplied,0) AS bIsApplied,ISNULL(nPaymentMode,0) AS nPaymentMode,  "+
        //        " ISNULL(nCrdExpChkMnyOrdDate,0) AS nCrdExpChkMnyOrdDate,  "+
        //        " ISNULL(sCrdNoMnyOrdNoChkNo,'') AS sCrdNoMnyOrdNoChkNo,ISNULL(sSecurityNo,'') AS sSecurityNo,  "+
        //        " ISNULL(sCardType,0) AS sCardType,ISNULL(nPayerModeID,0) AS nPayerModeID,  "+
        //        " ISNULL(nClinicID,0) AS nClinicID,ISNULL(nOtherPaymentMode,0) AS nOtherPaymentMode, "+
        //        " ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDxCode,'') AS sDxCode, "+
        //        " ISNULL(nRefrenceID,0) AS nRefrenceID,ISNULL(sNote,'') AS sNote "+
        //        " FROM BL_Transaction_AdvancePayment_MST " +
        //        " WHERE nClinicID = "+this.ClinicID+" ";

        //        oDB.Retrive_Query(_sqlQuery, out _dtAdvancePayments);
        //        oDB.Disconnect();
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {dbEx.ERROR_Log(dbEx.ToString());}
        //    catch (Exception ex)
        //    {gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);}
        //    return _dtAdvancePayments;
        //}

        public DataTable GetAdvancePayments(PaymentOtherType PaymentType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            DataTable _dtAdvancePayments = null;

            try
            {
                oDB.Connect(false);

                //BL_Transaction_AdvancePayment_MST
                //nAdvPayID,nPatientID,nInsuranceID,nAppointmentID,nTransactionDate,nAppointmentDate,dAdvPayAmount,
                //bIsApplied,nPaymentMode,nCrdExpChkMnyOrdDate,sCrdNoMnyOrdNoChkNo,sSecurityNo,sCardType,nPayerModeID
                //nClinicID,nOtherPaymentMode,sCPTCode,sDxCode,nRefrenceID,sNote

                _sqlQuery = " SELECT nAdvPayID,ISNULL(nPatientID,0) AS nPatientID, " +
                " ISNULL(nInsuranceID,0) AS nInsuranceID, " +
                " ISNULL(nAppointmentID,0) AS nAppointmentID,ISNULL(nTransactionDate,0) AS nTransactionDate,  " +
                " ISNULL(nAppointmentDate,0) AS nAppointmentDate,ISNULL(dAdvPayAmount,0) AS dAdvPayAmount, " +
                " ISNULL(bIsApplied,0) AS bIsApplied,ISNULL(nPaymentMode,0) AS nPaymentMode,  " +
                " ISNULL(nCrdExpChkMnyOrdDate,0) AS nCrdExpChkMnyOrdDate,  " +
                " ISNULL(sCrdNoMnyOrdNoChkNo,'') AS sCrdNoMnyOrdNoChkNo,ISNULL(sSecurityNo,'') AS sSecurityNo,  " +
                " ISNULL(sCardType,0) AS sCardType,ISNULL(nPayerModeID,0) AS nPayerModeID,  " +
                " ISNULL(nClinicID,0) AS nClinicID,ISNULL(nOtherPaymentMode,0) AS nOtherPaymentMode, " +
                " ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDxCode,'') AS sDxCode, " +
                " ISNULL(nRefrenceID,0) AS nRefrenceID,ISNULL(sNote,'') AS sNote, " +
                " ISNULL(sAuthorizationNumber,'') AS sAuthorizationNumber "+
                " FROM BL_Transaction_AdvancePayment_MST " +
                " WHERE nOtherPaymentMode = " + PaymentType.GetHashCode() + " AND nClinicID = " + this.ClinicID + " ";

                oDB.Retrive_Query(_sqlQuery, out _dtAdvancePayments);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            return _dtAdvancePayments;
        }

        //public DataTable GetPatientAdvancePayments(Int64 Patientid)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string _sqlQuery = "";
        //    DataTable _dtAdvancePayments = null;

        //    try
        //    {
        //        oDB.Connect(false);

        //        //BL_Transaction_AdvancePayment_MST
        //        //nAdvPayID,nPatientID,nInsuranceID,nAppointmentID,nTransactionDate,nAppointmentDate,dAdvPayAmount,
        //        //bIsApplied,nPaymentMode,nCrdExpChkMnyOrdDate,sCrdNoMnyOrdNoChkNo,sSecurityNo,sCardType,nPayerModeID
        //        //nClinicID,nOtherPaymentMode,sCPTCode,sDxCode,nRefrenceID,sNote

        //        _sqlQuery = " SELECT nAdvPayID,ISNULL(nPatientID,0) AS nPatientID, " +
        //        " ISNULL(nInsuranceID,0) AS nInsuranceID, " +
        //        " ISNULL(nAppointmentID,0) AS nAppointmentID,ISNULL(nTransactionDate,0) AS nTransactionDate,  " +
        //        " ISNULL(nAppointmentDate,0) AS nAppointmentDate,ISNULL(dAdvPayAmount,0) AS dAdvPayAmount, " +
        //        " ISNULL(bIsApplied,0) AS bIsApplied,ISNULL(nPaymentMode,0) AS nPaymentMode,  " +
        //        " ISNULL(nCrdExpChkMnyOrdDate,0) AS nCrdExpChkMnyOrdDate,  " +
        //        " ISNULL(sCrdNoMnyOrdNoChkNo,'') AS sCrdNoMnyOrdNoChkNo,ISNULL(sSecurityNo,'') AS sSecurityNo,  " +
        //        " ISNULL(sCardType,0) AS sCardType,ISNULL(nPayerModeID,0) AS nPayerModeID,  " +
        //        " ISNULL(nClinicID,0) AS nClinicID,ISNULL(nOtherPaymentMode,0) AS nOtherPaymentMode, " +
        //        " ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDxCode,'') AS sDxCode, " +
        //        " ISNULL(nRefrenceID,0) AS nRefrenceID,ISNULL(sNote,'') AS sNote " +
        //        " FROM BL_Transaction_AdvancePayment_MST " +
        //        " WHERE nPatientID = " + Patientid + " AND nClinicID = " + this.ClinicID + " ";

        //        oDB.Retrive_Query(_sqlQuery, out _dtAdvancePayments);
        //        oDB.Disconnect();
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    { dbEx.ERROR_Log(dbEx.ToString()); }
        //    catch (Exception ex)
        //    { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        //    return _dtAdvancePayments;
        //}

        public DataTable GetPatientAdvancePayments(Int64 Patientid,PaymentOtherType PaymentType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            DataTable _dtAdvancePayments = null;

            try
            {
                oDB.Connect(false);

                //BL_Transaction_AdvancePayment_MST
                //nAdvPayID,nPatientID,nInsuranceID,nAppointmentID,nTransactionDate,nAppointmentDate,dAdvPayAmount,
                //bIsApplied,nPaymentMode,nCrdExpChkMnyOrdDate,sCrdNoMnyOrdNoChkNo,sSecurityNo,sCardType,nPayerModeID
                //nClinicID,nOtherPaymentMode,sCPTCode,sDxCode,nRefrenceID,sNote

                _sqlQuery = " SELECT nAdvPayID,ISNULL(nPatientID,0) AS nPatientID, " +
                " ISNULL(nInsuranceID,0) AS nInsuranceID, " +
                " ISNULL(nAppointmentID,0) AS nAppointmentID,ISNULL(nTransactionDate,0) AS nTransactionDate,  " +
                " ISNULL(nAppointmentDate,0) AS nAppointmentDate,ISNULL(dAdvPayAmount,0) AS dAdvPayAmount, " +
                " ISNULL(bIsApplied,0) AS bIsApplied,ISNULL(nPaymentMode,0) AS nPaymentMode,  " +
                " ISNULL(nCrdExpChkMnyOrdDate,0) AS nCrdExpChkMnyOrdDate,  " +
                " ISNULL(sCrdNoMnyOrdNoChkNo,'') AS sCrdNoMnyOrdNoChkNo,ISNULL(sSecurityNo,'') AS sSecurityNo,  " +
                " ISNULL(sCardType,0) AS sCardType,ISNULL(nPayerModeID,0) AS nPayerModeID,  " +
                " ISNULL(nClinicID,0) AS nClinicID,ISNULL(nOtherPaymentMode,0) AS nOtherPaymentMode, " +
                " ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDxCode,'') AS sDxCode, " +
                " ISNULL(nRefrenceID,0) AS nRefrenceID,ISNULL(sNote,'') AS sNote, " +
                " ISNULL(sAuthorizationNumber,'') AS sAuthorizationNumber "+
                " FROM BL_Transaction_AdvancePayment_MST " +
                " WHERE nPatientID = " + Patientid + " AND nOtherPaymentMode = "+PaymentType.GetHashCode()+"  AND nClinicID = " + this.ClinicID + " ";

                oDB.Retrive_Query(_sqlQuery, out _dtAdvancePayments);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            return _dtAdvancePayments;
        }

        public DataTable GetPatientPayments(Int64 Patientid, PaymentOtherType PaymentType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            DataTable _dtAdvancePayments = null;

            try
            {
                oDB.Connect(false);

                //BL_Transaction_AdvancePayment_MST
                //nAdvPayID,nPatientID,nInsuranceID,nAppointmentID,nTransactionDate,nAppointmentDate,dAdvPayAmount,
                //bIsApplied,nPaymentMode,nCrdExpChkMnyOrdDate,sCrdNoMnyOrdNoChkNo,sSecurityNo,sCardType,nPayerModeID
                //nClinicID,nOtherPaymentMode,sCPTCode,sDxCode,nRefrenceID,sNote

                _sqlQuery = " SELECT nAdvPayID,ISNULL(nPatientID,0) AS nPatientID, " +
                " ISNULL(nInsuranceID,0) AS nInsuranceID, " +
                " ISNULL(nAppointmentID,0) AS nAppointmentID,ISNULL(nTransactionDate,0) AS nTransactionDate,  " +
                " ISNULL(nAppointmentDate,0) AS nAppointmentDate,ISNULL(dAdvPayAmount,0) AS dAdvPayAmount, " +
                " ISNULL(bIsApplied,0) AS bIsApplied,ISNULL(nPaymentMode,0) AS nPaymentMode,  " +
                " ISNULL(nCrdExpChkMnyOrdDate,0) AS nCrdExpChkMnyOrdDate,  " +
                " ISNULL(sCrdNoMnyOrdNoChkNo,'') AS sCrdNoMnyOrdNoChkNo,ISNULL(sSecurityNo,'') AS sSecurityNo,  " +
                " ISNULL(sCardType,0) AS sCardType,ISNULL(nPayerModeID,0) AS nPayerModeID,  " +
                " ISNULL(nClinicID,0) AS nClinicID,ISNULL(nOtherPaymentMode,0) AS nOtherPaymentMode, " +
                " ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDxCode,'') AS sDxCode, " +
                " ISNULL(nRefrenceID,0) AS nRefrenceID,ISNULL(sNote,'') AS sNote, " +
                " ISNULL(sAuthorizationNumber,'') AS sAuthorizationNumber, " +
                " ISNULL " +
                          " ((SELECT     SUM(dCurrentPaymentAmt) AS Expr1 " +
                              " FROM         BL_Transaction_Payment_DTL " +
                              " WHERE     (nCoPayID = BL_Transaction_AdvancePayment_MST.nAdvPayID)), 0) AS PaidCopay, ISNULL(BL_Transaction_AdvancePayment_MST.dAdvPayAmount, 0) " +
                      " - ISNULL " +
                          " ((SELECT     SUM(dCurrentPaymentAmt) AS Expr1 " +
                              " FROM         BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_1 " +
                              " WHERE     (nCoPayID = BL_Transaction_AdvancePayment_MST.nAdvPayID)), 0) AS BalancePay " +
                " FROM BL_Transaction_AdvancePayment_MST " +
                " WHERE     (ISNULL(BL_Transaction_AdvancePayment_MST.dAdvPayAmount, 0) - " +
                " ISNULL ((SELECT     SUM(dCurrentPaymentAmt) AS Expr1 " +
                " FROM         BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_1 " +
                " WHERE     (nCoPayID = BL_Transaction_AdvancePayment_MST.nAdvPayID)), 0) > 0) AND (BL_Transaction_AdvancePayment_MST.bIsApplied = 'false') " +
                " AND (BL_Transaction_AdvancePayment_MST.nPatientID = " + Patientid + " ) AND (BL_Transaction_AdvancePayment_MST.nOtherPaymentMode = " + PaymentType.GetHashCode() + ") ";
                

               // " WHERE nPatientID = " + Patientid + " AND nOtherPaymentMode = " + PaymentType.GetHashCode() + "  AND nClinicID = " + this.ClinicID + " ";

                oDB.Retrive_Query(_sqlQuery, out _dtAdvancePayments);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            return _dtAdvancePayments;
        }

        public AdvancePayments GetAdvancePayments(Int64 ClinicId, PaymentOtherType PaymentType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            DataTable _dtAdvancePayments = null;
            AdvancePayment oAdvancePayment = null;
            AdvancePayments oAdvancePayments = new AdvancePayments();

            try
            {
                oDB.Connect(false);

                //BL_Transaction_AdvancePayment_MST
                //nAdvPayID,nPatientID,nInsuranceID,nAppointmentID,nTransactionDate,nAppointmentDate,dAdvPayAmount,
                //bIsApplied,nPaymentMode,nCrdExpChkMnyOrdDate,sCrdNoMnyOrdNoChkNo,sSecurityNo,sCardType,nPayerModeID
                //nClinicID,nOtherPaymentMode,sCPTCode,sDxCode,nRefrenceID,sNote

                _sqlQuery = " SELECT nAdvPayID,ISNULL(nPatientID,0) AS nPatientID, " +
                " ISNULL(nInsuranceID,0) AS nInsuranceID, " +
                " ISNULL(nAppointmentID,0) AS nAppointmentID,ISNULL(nTransactionDate,0) AS nTransactionDate,  " +
                " ISNULL(nAppointmentDate,0) AS nAppointmentDate,ISNULL(dAdvPayAmount,0) AS dAdvPayAmount, " +
                " ISNULL(bIsApplied,0) AS bIsApplied,ISNULL(nPaymentMode,0) AS nPaymentMode,  " +
                " ISNULL(nCrdExpChkMnyOrdDate,0) AS nCrdExpChkMnyOrdDate,  " +
                " ISNULL(sCrdNoMnyOrdNoChkNo,'') AS sCrdNoMnyOrdNoChkNo,ISNULL(sSecurityNo,'') AS sSecurityNo,  " +
                " ISNULL(sCardType,0) AS sCardType,ISNULL(nPayerModeID,0) AS nPayerModeID,  " +
                " ISNULL(nClinicID,0) AS nClinicID,ISNULL(nOtherPaymentMode,0) AS nOtherPaymentMode, " +
                " ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDxCode,'') AS sDxCode, " +
                " ISNULL(nRefrenceID,0) AS nRefrenceID,ISNULL(sNote,'') AS sNote, " +
                " ISNULL(sAuthorizationNumber,'') AS sAuthorizationNumber "+
                " FROM BL_Transaction_AdvancePayment_MST " +
                " WHERE nOtherPaymentMode = " + PaymentType + " AND  nClinicID = " + ClinicId + " ";

                oDB.Retrive_Query(_sqlQuery, out _dtAdvancePayments);
                oDB.Disconnect();

                if (_dtAdvancePayments != null && _dtAdvancePayments.Rows.Count > 0)
                {
                    for (int index = 0; index < _dtAdvancePayments.Rows.Count; index++)
                    {
                        oAdvancePayment = new AdvancePayment();

                        oAdvancePayment.PaymentID = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nAdvPayID"]);
                        oAdvancePayment.PatientID = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nPatientID"]);
                        oAdvancePayment.InsuranceID = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nInsuranceID"]);
                        oAdvancePayment.AppointmentID = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nAppointmentID"]);
                        oAdvancePayment.AppointmentDate = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nAppointmentDate"]);
                        oAdvancePayment.TransactionDate = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nTransactionDate"]);
                        oAdvancePayment.PaymentAmount = Convert.ToDecimal(_dtAdvancePayments.Rows[index]["dAdvPayAmount"]);
                        oAdvancePayment.IsApplied = Convert.ToBoolean(_dtAdvancePayments.Rows[index]["bIsApplied"]);
                        oAdvancePayment.PaymentMode = ((PaymentMode)Convert.ToInt32(_dtAdvancePayments.Rows[index]["nPaymentMode"]));
                        oAdvancePayment.CheckMoneyOrderCardExpiryDate = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nCrdExpChkMnyOrdDate"]);
                        oAdvancePayment.CheckMoneyOrderCardNumber = Convert.ToString(_dtAdvancePayments.Rows[index]["sCrdNoMnyOrdNoChkNo"]);
                        oAdvancePayment.CardSecurityNumber = Convert.ToString(_dtAdvancePayments.Rows[index]["sSecurityNo"]);
                        oAdvancePayment.CardType = Convert.ToString(_dtAdvancePayments.Rows[index]["sCardType"]);
                        oAdvancePayment.PayerMode = ((PayerMode)Convert.ToInt32(_dtAdvancePayments.Rows[index]["nPayerModeID"]));
                        oAdvancePayment.OtherPaymentType = ((PaymentOtherType)Convert.ToInt32(_dtAdvancePayments.Rows[index]["nOtherPaymentMode"]));
                                                
                        oAdvancePayment.CPTCode = Convert.ToString(_dtAdvancePayments.Rows[index]["sCPTCode"]);

                        if (oAdvancePayment.CPTCode.Trim() != "")
                        {
                            string[] _cptCodes = null;
                            gloGeneralItem.gloItem ogloItem = null;
                            _cptCodes = oAdvancePayment.CPTCode.Split(',');
                            if (_cptCodes.Length > 0)
                            {
                                for (int i = 0; i < _cptCodes.Length; i++)
                                {
                                    ogloItem = new gloGeneralItem.gloItem();
                                    ogloItem.ID = 0;
                                    ogloItem.Code = _cptCodes[i].Trim();
                                    ogloItem.Description = "";
                                    oAdvancePayment.CPTCodes.Add(ogloItem);
                                    ogloItem.Dispose();
                                    ogloItem = null;
                                }
                            }
                        }

                        oAdvancePayment.DxCode = Convert.ToString(_dtAdvancePayments.Rows[index]["sDxCode"]);
                        if (oAdvancePayment.DxCode.Trim() != "")
                        {
                            string[] _dxCodes = null;
                            gloGeneralItem.gloItem ogloItem = null;
                            _dxCodes = oAdvancePayment.DxCode.Split(',');
                            if (_dxCodes.Length > 0)
                            {
                                for (int i = 0; i < _dxCodes.Length; i++)
                                {
                                    ogloItem = new gloGeneralItem.gloItem();
                                    ogloItem.ID = 0;
                                    ogloItem.Code = _dxCodes[i].Trim();
                                    ogloItem.Description = "";
                                    oAdvancePayment.DxCodes.Add(ogloItem);
                                    ogloItem.Dispose();
                                    ogloItem = null;
                                }
                            }
                        }

                        oAdvancePayment.ClinicID = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nClinicID"]);
                        oAdvancePayment.RefrenceID = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nRefrenceID"]);
                        oAdvancePayment.Note = Convert.ToString(_dtAdvancePayments.Rows[index]["sNote"]);
                        oAdvancePayment.CardAuthorizationNo = Convert.ToString(_dtAdvancePayments.Rows[index]["sAuthorizationNumber"]);

                        if (oAdvancePayment != null) { oAdvancePayments.Add(oAdvancePayment); }
                        oAdvancePayment = null;

                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }

            return oAdvancePayments;
        }

        public AdvancePayment GetAdvancePayment(Int64 Paymentid,Int64 ClinicId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            DataTable _dtAdvancePayments = null;
            AdvancePayment oAdvancePayment = null;

            try
            {
                oDB.Connect(false);

                //BL_Transaction_AdvancePayment_MST
                //nAdvPayID,nPatientID,nInsuranceID,nAppointmentID,nTransactionDate,nAppointmentDate,dAdvPayAmount,
                //bIsApplied,nPaymentMode,nCrdExpChkMnyOrdDate,sCrdNoMnyOrdNoChkNo,sSecurityNo,sCardType,nPayerModeID
                //nClinicID,nOtherPaymentMode,sCPTCode,sDxCode,nRefrenceID,sNote

                _sqlQuery = "SELECT nAdvPayID,ISNULL(nPatientID,0) AS nPatientID, " +
                " ISNULL(nInsuranceID,0) AS nInsuranceID, " +
                " ISNULL(nAppointmentID,0) AS nAppointmentID,ISNULL(nTransactionDate,0) AS nTransactionDate,  " +
                " ISNULL(nAppointmentDate,0) AS nAppointmentDate,ISNULL(dAdvPayAmount,0) AS dAdvPayAmount, " +
                " ISNULL(bIsApplied,0) AS bIsApplied,ISNULL(nPaymentMode,0) AS nPaymentMode,  " +
                " ISNULL(nCrdExpChkMnyOrdDate,0) AS nCrdExpChkMnyOrdDate,  " +
                " ISNULL(sCrdNoMnyOrdNoChkNo,'') AS sCrdNoMnyOrdNoChkNo,ISNULL(sSecurityNo,'') AS sSecurityNo,  " +
                " ISNULL(sCardType,0) AS sCardType,ISNULL(nPayerModeID,0) AS nPayerModeID,  " +
                " ISNULL(nClinicID,0) AS nClinicID,ISNULL(nOtherPaymentMode,0) AS nOtherPaymentMode, " +
                " ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDxCode,'') AS sDxCode, " +
                " ISNULL(nRefrenceID,0) AS nRefrenceID,ISNULL(sNote,'') AS sNote, " +
                " ISNULL(sAuthorizationNumber,'') AS sAuthorizationNumber,ISNULL(sReceiptNo,'') AS sReceiptNo " +
                " FROM BL_Transaction_AdvancePayment_MST " +
                " WHERE nAdvPayID = " + Paymentid + " AND nClinicID = " + ClinicId + " ";

                oDB.Retrive_Query(_sqlQuery, out _dtAdvancePayments);
                oDB.Disconnect();

                if (_dtAdvancePayments != null && _dtAdvancePayments.Rows.Count > 0)
                {
                        oAdvancePayment = new AdvancePayment();
                        int index = 0;

                        oAdvancePayment.PaymentID = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nAdvPayID"]);
                        oAdvancePayment.PatientID = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nPatientID"]);
                        oAdvancePayment.InsuranceID = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nInsuranceID"]);
                        oAdvancePayment.AppointmentID = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nAppointmentID"]);
                        oAdvancePayment.AppointmentDate = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nAppointmentDate"]);
                        oAdvancePayment.TransactionDate = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nTransactionDate"]);
                        oAdvancePayment.PaymentAmount = Convert.ToDecimal(_dtAdvancePayments.Rows[index]["dAdvPayAmount"]);
                        oAdvancePayment.IsApplied = Convert.ToBoolean(_dtAdvancePayments.Rows[index]["bIsApplied"]);
                        oAdvancePayment.PaymentMode = ((PaymentMode)Convert.ToInt32(_dtAdvancePayments.Rows[index]["nPaymentMode"]));
                        oAdvancePayment.CheckMoneyOrderCardExpiryDate = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nCrdExpChkMnyOrdDate"]);
                        oAdvancePayment.CheckMoneyOrderCardNumber = Convert.ToString(_dtAdvancePayments.Rows[index]["sCrdNoMnyOrdNoChkNo"]);
                        oAdvancePayment.CardSecurityNumber = Convert.ToString(_dtAdvancePayments.Rows[index]["sSecurityNo"]);
                        oAdvancePayment.CardType = Convert.ToString(_dtAdvancePayments.Rows[index]["sCardType"]);
                        oAdvancePayment.PayerMode = ((PayerMode)Convert.ToInt32(_dtAdvancePayments.Rows[index]["nPayerModeID"]));
                        oAdvancePayment.OtherPaymentType = ((PaymentOtherType)Convert.ToInt32(_dtAdvancePayments.Rows[index]["nOtherPaymentMode"]));
                        oAdvancePayment.CPTCode = Convert.ToString(_dtAdvancePayments.Rows[index]["sCPTCode"]);
                        oAdvancePayment.DxCode = Convert.ToString(_dtAdvancePayments.Rows[index]["sDxCode"]);
                        oAdvancePayment.RefrenceID = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nRefrenceID"]);
                        oAdvancePayment.Note = Convert.ToString(_dtAdvancePayments.Rows[index]["sNote"]);
                        oAdvancePayment.ClinicID = Convert.ToInt64(_dtAdvancePayments.Rows[index]["nClinicID"]);
                        oAdvancePayment.CardAuthorizationNo = Convert.ToString(_dtAdvancePayments.Rows[index]["sAuthorizationNumber"]);
                        oAdvancePayment.ReceiptNo = Convert.ToString(_dtAdvancePayments.Rows[index]["sReceiptNo"]);
                        

                        #region " Get CPT Items "

                        if (oAdvancePayment.CPTCode.Trim() != "")
                        {
                            string[] _cptcodes = null;
                            _cptcodes = oAdvancePayment.CPTCode.Split(',');
                            if (_cptcodes != null && _cptcodes.Length > 0)
                            {
                                CPT oCPT = new CPT(_databaseconnectionstring);
                                DataTable _dtcpt = null;
                                gloGeneralItem.gloItem ogloItem = null;

                                for (int cptindex = 0; cptindex < _cptcodes.Length; cptindex++)
                                {
                                    _dtcpt = oCPT.GetCPT(_cptcodes[cptindex].ToString());
                                    if (_dtcpt != null && _dtcpt.Rows.Count > 0)
                                    {
                                        //nCPTID,sDescription
                                        ogloItem = new gloGeneralItem.gloItem();
                                        ogloItem.ID = Convert.ToInt64(_dtcpt.Rows[0]["nCPTID"]);
                                        ogloItem.Code = _cptcodes[cptindex].ToString();
                                        ogloItem.Description = Convert.ToString(_dtcpt.Rows[0]["sDescription"]);
                                        oAdvancePayment.CPTCodes.Add(ogloItem);
                                        ogloItem.Dispose();
                                        ogloItem = null;
                                    }
                                    if (_dtcpt != null)
                                    {
                                        _dtcpt.Dispose();
                                        _dtcpt = null;
                                    }
                                }
                                oCPT.Dispose();
                            }
                        }

                        #endregion " Get CPT Items "

                        #region " Get Dx Items "

                        if (oAdvancePayment.DxCode.Trim() != "")
                        {
                            string[] _dxcodes = null;
                            _dxcodes = oAdvancePayment.DxCode.Split(',');
                            if (_dxcodes != null && _dxcodes.Length > 0)
                            {
                                //CPT oCPT = new CPT(_databaseconnectionstring);
                                ICD9 oICD9 = new ICD9(_databaseconnectionstring);
                                DataTable _dtdx = null;
                                gloGeneralItem.gloItem ogloItem = null;

                                for (int dxindex = 0; dxindex < _dxcodes.Length; dxindex++)
                                {
                                    _dtdx = oICD9.GetICD9(_dxcodes[dxindex].ToString());

                                    if (_dtdx != null && _dtdx.Rows.Count > 0)
                                    {
                                        ogloItem = new gloGeneralItem.gloItem();
                                        //nICD9ID,sDescription 
                                        ogloItem.ID = Convert.ToInt64(_dtdx.Rows[0]["nICD9ID"]);
                                        ogloItem.Code = _dxcodes[dxindex].ToString();
                                        ogloItem.Description = Convert.ToString(_dtdx.Rows[0]["sDescription"]);
                                        oAdvancePayment.DxCodes.Add(ogloItem);
                                        ogloItem.Dispose();
                                        ogloItem = null;
                                    }
                                    if (_dtdx != null)
                                    {
                                        _dtdx.Dispose();
                                        _dtdx = null;
                                    }
                                }
                                oICD9.Dispose();
                                oICD9 = null;
                            }
                        }

                        #endregion " Get Dx Items "


                    }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }

            return oAdvancePayment;
        }

        public DataTable GetCopayAlert(Int64 Patientid, DateTime CopayDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtCopayAlert = null;
            Object retVal = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);

                _sqlQuery = " SELECT Count(AS_Appointment_DTL.dtStartDate) "+
                " FROM  AS_Appointment_MST INNER JOIN AS_Appointment_DTL  "+
                " ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID "+
                " WHERE "+
                " (AS_Appointment_MST.nPatientID = "+Patientid+")  "+
                " AND (AS_Appointment_DTL.dtStartDate = "+gloDateMaster.gloDate.DateAsNumber(CopayDate.ToShortDateString())+")  " +
                " AND (AS_Appointment_DTL.nClinicID = "+this.ClinicID+")";

                retVal = oDB.ExecuteScalar_Query(_sqlQuery);

                if (retVal != null && Convert.ToString(retVal) != "" && Convert.ToInt64(retVal) > 0)
                {
                    _sqlQuery = "";
                    _sqlQuery = " SELECT COUNT(nAdvPayID) FROM BL_Transaction_AdvancePayment_MST "+
                    " WHERE nAppointmentDate = " + gloDateMaster.gloDate.DateAsNumber(CopayDate.ToShortDateString()) + " "+
                    " AND nPatientID = " + Patientid + " AND nClinicID = " + this.ClinicID + "";

                    retVal = oDB.ExecuteScalar_Query(_sqlQuery);

                    if (retVal != null && Convert.ToString(retVal) != "" && Convert.ToInt64(retVal) <= 0)
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
                        " FROM      PatientInsurance_DTL " +
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
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dtCopayAlert;
        }

        public bool IsCopayUnapplied(Int64 Patientid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            Object _retVal = null;
            bool _isCopayUnapplied = false;

            try
            {
                oDB.Connect(false);

                //_sqlQuery = " SELECT COUNT(*) FROM BL_Transaction_Payment_DTL " +
                //" where nCoPayID IN (select nAdvPayID from BL_Transaction_AdvancePayment_MST where nOtherPaymentMode = " + PaymentOtherType.Copay.GetHashCode() +" " +
                //   " and nPatientID = " + Patientid + " AND nClinicID = " + this.ClinicID + ")";

                _sqlQuery = " select COUNT(nAdvPayID) from BL_Transaction_AdvancePayment_MST " +
                " where  " +
                " nAdvPayID NOT IN (select nCoPayID from BL_Transaction_Payment_DTL WHERE nPatientID = " + Patientid + " AND nClinicID = " + this.ClinicID + ") " +
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
            { if (oDB != null) { oDB.Dispose(); } }

            return _isCopayUnapplied;
        }

        public decimal GetUnappliedAmount(Int64 Patientid,PaymentOtherType Payment_OtherType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            Object _retVal = null;
            decimal _totalAmount = 0;

            try
            {
                oDB.Connect(false);

                //_sqlQuery = " SELECT COUNT(*) FROM BL_Transaction_Payment_DTL " +
                //" where nCoPayID IN (select nAdvPayID from BL_Transaction_AdvancePayment_MST where nOtherPaymentMode = " + PaymentOtherType.Copay.GetHashCode() +" " +
                //   " and nPatientID = " + Patientid + " AND nClinicID = " + this.ClinicID + ")";

                _sqlQuery = " select ISNULL(SUM(dAdvPayAmount),0) AS dAdvPayAmount from BL_Transaction_AdvancePayment_MST " +
                " where  " +
                " nAdvPayID NOT IN (select ISNULL(nCoPayID,0) from BL_Transaction_Payment_DTL WHERE nPatientID = " + Patientid + " AND nClinicID = " + this.ClinicID + ") " +
                " AND nOtherPaymentMode = " + Payment_OtherType.GetHashCode() + " and nPatientID = " + Patientid + " " +
                " AND nClinicID = " + this.ClinicID + " ";


                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_retVal != null)
                { _totalAmount = Convert.ToDecimal(_retVal); }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            { if (oDB != null) { oDB.Dispose(); } }

            return _totalAmount;
        }

        public bool DeleteAdvancePayment(Int64 Paymentid, Int64 ClinicId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            bool _isDeleted = false;

            try
            {
                oDB.Connect(false);
                _sqlQuery = "DELETE FROM BL_Transaction_AdvancePayment_MST WHERE nAdvPayID = " + Paymentid + " AND nClinicID = " + ClinicId + " ";
                _isDeleted = Convert.ToBoolean(oDB.Execute_Query(_sqlQuery));
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            { if (oDB != null) { oDB.Dispose(); } }

            return _isDeleted;
        }

        public bool CanDelete_or_Modify_Advance(Int64 Paymentid, Int64 ClinicId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _retVal = null;
            string _sqlQuery = "";
            bool _canDeletePayment = true;

            try
            {
                //...**** Check if the Copay,Advance or Coinsurance selected for delete is 
                //...**** applied (part of it or full amount) against any Payment in Payment Detail table in nCopayID field
                //...**** 

                oDB.Connect(false);
                _sqlQuery = " SELECT ISNULL(COUNT(nCoPayID),0) AS CopayCount FROM  BL_Transaction_Payment_DTL " +
                " WHERE nCoPayID = " + Paymentid + "  AND nClinicID = " + ClinicId + "";
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_retVal != null && Convert.ToInt64(_retVal) > 0)
                { _canDeletePayment = false; }
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            { 
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _canDeletePayment;
        }

        public DataTable GetPatientPendingCoPay(Int64 Patientid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPendingCoPay = null;
            string _strSQL = "";

            try
            {

            _strSQL = "SELECT  BL_Transaction_AdvancePayment_MST.nAdvPayID, BL_Transaction_AdvancePayment_MST.nPatientID, BL_Transaction_AdvancePayment_MST.nInsuranceID, " +
                      " BL_Transaction_AdvancePayment_MST.nAppointmentID, BL_Transaction_AdvancePayment_MST.nTransactionDate, BL_Transaction_AdvancePayment_MST.nAppointmentDate, " +
                      " BL_Transaction_AdvancePayment_MST.bIsApplied, BL_Transaction_AdvancePayment_MST.nPaymentMode, BL_Transaction_AdvancePayment_MST.nCrdExpChkMnyOrdDate, " +
                      " BL_Transaction_AdvancePayment_MST.sCrdNoMnyOrdNoChkNo, BL_Transaction_AdvancePayment_MST.sSecurityNo, BL_Transaction_AdvancePayment_MST.sCardType, " +
                      " BL_Transaction_AdvancePayment_MST.nPayerModeID, BL_Transaction_AdvancePayment_MST.nClinicID, PatientInsurance_DTL.sInsuranceName, " +
                      " ISNULL(BL_Transaction_AdvancePayment_MST.dAdvPayAmount,0) AS TotalCoPay, " +
                      " ISNULL " +
                          " ((SELECT     SUM(dCurrentPaymentAmt) AS Expr1 " +
                              " FROM         BL_Transaction_Payment_DTL " +
                              " WHERE     (nCoPayID = BL_Transaction_AdvancePayment_MST.nAdvPayID)), 0) AS PaidCopay, ISNULL(BL_Transaction_AdvancePayment_MST.dAdvPayAmount, 0) " +
                      " - ISNULL " +
                          " ((SELECT     SUM(dCurrentPaymentAmt) AS Expr1 " +
                              " FROM         BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_1 " +
                              " WHERE     (nCoPayID = BL_Transaction_AdvancePayment_MST.nAdvPayID)), 0) AS BalanceCopay " +
                      " FROM         BL_Transaction_AdvancePayment_MST INNER JOIN PatientInsurance_DTL ON BL_Transaction_AdvancePayment_MST.nInsuranceID = PatientInsurance_DTL.nInsuranceID " +
                      " WHERE     (ISNULL(BL_Transaction_AdvancePayment_MST.dAdvPayAmount, 0) - " +
                        " ISNULL ((SELECT     SUM(dCurrentPaymentAmt) AS Expr1 " +
                        " FROM         BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_1 " +
                        " WHERE     (nCoPayID = BL_Transaction_AdvancePayment_MST.nAdvPayID)), 0) > 0) AND (BL_Transaction_AdvancePayment_MST.bIsApplied = 'false') " +
                        " AND (BL_Transaction_AdvancePayment_MST.nPatientID = " + Patientid + " ) AND (BL_Transaction_AdvancePayment_MST.nOtherPaymentMode = " + PaymentOtherType.Copay.GetHashCode() + ") " +
                        " AND (PatientInsurance_DTL.nPatientID = " + Patientid + ")";

            
                oDB.Connect(false);
                oDB.Retrive_Query(_strSQL, out dtPendingCoPay);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                { oDB.Disconnect(); oDB.Dispose(); }
            }

            return dtPendingCoPay;
        }

        public DataTable GetPatientPendingAdvancePayment(Int64 Patientid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPendingPay = null;
            string _strSQL = "";

            try
            {
                _strSQL = " SELECT     nAdvPayID, nPatientID, nInsuranceID, nAppointmentID, nTransactionDate, " +
                " nAppointmentDate, bIsApplied, nPaymentMode, nCrdExpChkMnyOrdDate,  " +
                " sCrdNoMnyOrdNoChkNo, sSecurityNo, sCardType, nPayerModeID, nClinicID, " +
                " ISNULL(dAdvPayAmount, 0) AS dAdvPayAmount, ISNULL " +
                " ((SELECT     SUM(dCurrentPaymentAmt) AS Expr1 " +
                " FROM         BL_Transaction_Payment_DTL " +
                " WHERE     (nCoPayID = BL_Transaction_AdvancePayment_MST.nAdvPayID)), 0) AS PaidCopay,  " +
                " ISNULL(dAdvPayAmount, 0) - ISNULL " +
                " ((SELECT     SUM(dCurrentPaymentAmt) AS Expr1 " +
                " FROM         BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_1 " +
                " WHERE     (nCoPayID = BL_Transaction_AdvancePayment_MST.nAdvPayID)), 0) AS BalanceCopay " +
                " FROM         BL_Transaction_AdvancePayment_MST " +
                " WHERE     (ISNULL(dAdvPayAmount, 0) - ISNULL " +
                " ((SELECT     SUM(dCurrentPaymentAmt) AS Expr1 " +
                " FROM         BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_1 " +
                " WHERE     (nCoPayID = BL_Transaction_AdvancePayment_MST.nAdvPayID)), 0) > 0) AND (bIsApplied = 'false') AND  " +
                " (nPatientID = " + Patientid + ") AND (nOtherPaymentMode = " + PaymentOtherType.AdvancePayment.GetHashCode() + ") ";

                oDB.Connect(false);
                oDB.Retrive_Query(_strSQL, out dtPendingPay);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                { oDB.Disconnect(); oDB.Dispose(); }
            }
            return dtPendingPay;
        }

        #endregion " Public & Private "

    }

    

}
