using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using gloPatient;
using gloSettings;
using gloBilling.Common;

using System.Data.SqlClient;
using gloBilling.EOBPayment.Common;
using gloAuditTrail;

namespace gloBilling.Payment
{
    
    

    partial class InsurancePayment : PaymentBase
    {

        public static Int64 TempEOBPaymentDetailId = 0;  // added on 13_July_2010 by Dev66

        public static Int64 GetCPTCrossWalKID(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);

            string _sqlQuery = string.Empty;
            Int64 _crossWalkID = 0;
            Object _retVal = null;

            try
            {
                _sqlQuery = " SELECT ISNULL(Contacts_Insurance_DTL.nCPTMappingID,0) CrosswalkID FROM Contacts_Insurance_DTL WITH(NOLOCK) " +
                            " JOIN CPT_Mapping_MST WITH(NOLOCK) ON Contacts_Insurance_DTL.nCPTMappingID = CPT_Mapping_MST.nCPTMappingID " +
                            " WHERE Contacts_Insurance_DTL.nContactID = " + ContactID;

                //_sqlQuery = " SELECT ISNULL(nCPTMappingID,0) CrosswalkID FROM dbo.Contacts_Insurance_DTL WHERE nContactID = " + ContactID;

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


        public static bool GetDialyCloseValidationSetting(Int64 nClinicId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);

            string _sqlQuery = string.Empty;
            bool _isCheckValid = false;
            Object _retVal = null;

            try
            {
                _sqlQuery = "select sSettingsValue from settings where sSettingsName = 'Complete Payments before Daily Close' and nClinicID = " + nClinicId ;


                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal)!="")
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


        //For payments with closed close dates but are NOT completed. 
        //Function will return last close date for EOBPaymentID
        public static string  GetNewOpenCloseDate(Int64 nEOBPaymentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);

            string _sqlQuery = string.Empty;
            string _CloseDate = "";
            Object _retVal = null;

            try
            {
                _sqlQuery = "select top (1)  dbo.CONVERT_TO_DATE(nCloseDate)   from BL_EOBPayment_DTL where nEOBPaymentID = " + nEOBPaymentID  +" order by nCloseDate desc" ;


                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
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


        public static string GetLastCloseDate()
        {
            gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, string.Empty);

            //...Load last selected close date
            string _lastCloseDate = BillingSettings.LastSelectedCloseDate;
            try
            {
                //...If the last selected close date is being closed then make the close date empty.
                if (!_lastCloseDate.Equals(string.Empty))
                {
                    if (ogloBilling.IsDayClosed(Convert.ToDateTime(_lastCloseDate)) == true)
                    {
                        _lastCloseDate = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloBilling.Dispose();
            }
            return _lastCloseDate;
        }

        public static Int64 GetDefaultPaymentTrayID()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);

            string _sqlQuery = string.Empty;
            Int64 _defaultTrayId = 0;
            Object _retVal = null;

            try
            {
                _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray WITH(NOLOCK) " +
                            " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                            " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + AppSettings.UserID + " AND nClinicID = " + AppSettings.ClinicID + "";

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

        public static string GetPaymentTrayDescription(Int64 PaymentTrayID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);

            string _description = string.Empty;
            string _sqlQuery = string.Empty;
            Object _retVal = null;

            try
            {
                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH(NOLOCK) WHERE nCloseDayTrayID = " + PaymentTrayID + " AND nClinicID = " + AppSettings.ClinicID + "");
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

        public static string GetNextActions()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
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
                " WHERE nID > 0 AND sCode IS NOT NULL AND sDescription IS NOT NULL AND nClinicID = " + AppSettings.ClinicID + " " +
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

        public static string GetInsuranceParties(Int64 ClaimNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtClaimInsurances = null;
            string _payPartyCode = "";
            string _conCodeDesc = "";

            try
            {
                if (ClaimNo > 0)
                {
                    oDB.Connect(false);
                    oParameters.Clear();
                    oParameters.Add("@nClaimNo", ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                    oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
                    oDB.Retrive("BL_SELECT_CLAIM_INSURANCES_REVISED", oParameters, out _dtClaimInsurances);
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
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_dtClaimInsurances != null) { _dtClaimInsurances.Dispose(); }
            }

            return _payPartyCode;
        }

        public static string GetReasonDescription(string Code)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            string _description = "";
            Object _retVal = null;

            try
            {
                _sqlQuery = " SELECT DISTINCT SUBSTRING(ISNULL(sDescription,''),0,255) AS Description " +
                " FROM BL_ReasonCodes_MST WITH(NOLOCK)  where UPPER(ISNULL(sGroupCode,''))+UPPER(ISNULL(sCode,'')) = '" + Code.Trim().ToUpper() + "' " +
                " AND (bIsBlock IS NULL OR bIsBlock = 'false') AND nClinicID = " + AppSettings.ClinicID + " ";

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


        public static string GetRemarkDescription(string Code)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            string _description = "";
            Object _retVal = null;

            try
            {
                _sqlQuery = " SELECT DISTINCT SUBSTRING(ISNULL(sRemarkDescription,''),0,500) AS Description " +
                " FROM BL_RemarkCodes_MST WITH(NOLOCK)  where UPPER(ISNULL(sRemarkCode,'')) = '" + Code.Trim().ToUpper() + "' " +
                " AND (bIsBlock IS NULL OR bIsBlock = 'false') AND nClinicID = " + AppSettings.ClinicID + " ";

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


        public static DataRow GetEOBPaymentMST(Int64 EOBPaymentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;

            DataTable _dtEOBPaymentMST = new DataTable();
            DataRow _EOBPaymentMST = null;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCliniID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt); 
                oDB.Retrive("BL_SELECT_EOBPaymentMST", oParameters, out _dtEOBPaymentMST);
                oDB.Disconnect();

                if (_dtEOBPaymentMST != null && _dtEOBPaymentMST.Rows.Count > 0)
                {
                    _EOBPaymentMST = _dtEOBPaymentMST.Rows[0];
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
                if (_dtEOBPaymentMST != null) { _dtEOBPaymentMST.Dispose(); }
            }
            return _EOBPaymentMST;
        }

        public static Int64 GetEOBOriginalPaymentId(Int64 PayerId,Int64 TransactionMasterID, Int64 TransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _nEOBPaymentID = 0;
            Object _retVal = null;         

           

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nPayerID", PayerId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionMasterID", TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCliniID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBPaymentID",null ,ParameterDirection.Output, SqlDbType.BigInt);
                _retVal=oDB.ExecuteScalar("BL_SELECT_Original_EOBPaymentMST", oParameters);
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

        public static DataRow GetEOBOriginalPaymentId(string EOBPaymentId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            DataTable _dtEOBReservePayment = new DataTable();
            DataRow _drEOBReservePayment = null;            

            try
            {
                _sqlQuery = " select top(1) nEOBPaymentID ,sCheckNumber,dbo.CONVERT_TO_DATE(nCheckDate) as CheckDate,dbo.CONVERT_TO_DATE(nCloseDate) As closedate  from  BL_EOBPayment_MST WITH(NOLOCK) where " +
                            "isnull(bIsPaymentVoid,0) = 0 AND  nEOBPaymentID in (SELECT ConvertedChar FROM dbo.SplitString('" + EOBPaymentId + "',',' ))  AND nClinicID = " + AppSettings.ClinicID + "" +
                            "order by nCloseDate,dtCreatedDateTime DESC";

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

        public static DataTable GetEOBPayment(Int64 EOBPaymentID, Int64 EOBID)
        {
            DataTable _dtEOBPayment = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBType", EOBPaymentType.InsuracePayment.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBID", EOBID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_EOB", oParameters, out _dtEOBPayment);
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
                //if (_dtEOBPayment != null) { _dtEOBPayment.Dispose(); }
            }

            return _dtEOBPayment;
        }

        public static DataTable GetEOBPaymentSummary(Int64 EOBPaymentID)
        {
            DataTable _dtEOBPayment = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBType", EOBPaymentType.InsuracePayment.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                //oDB.Retrive("BL_SELECT_EOBSummary", oParameters, out _dtEOBPayment);
                oDB.Retrive("BL_SELECT_EOBSummary_Revised", oParameters, out _dtEOBPayment);
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
                //if (_dtEOBPayment != null) { _dtEOBPayment.Dispose(); }
            }

            return _dtEOBPayment;
        }

        public static DataRow GetBillingTransactions(Int64 TransactionID, Int64 TrackTransactionID)
        {
            DataTable _dtBillingTransaction = new DataTable();
            DataRow _drBillingTransaction = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTrackTransactionID", TrackTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

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
                //if (_dtBillingTransaction != null) { _dtBillingTransaction.Dispose(); }
            }

            return _drBillingTransaction;
        }

        public static DataTable GetBillingTransactionLines(Int64 InsContactID, Int64 InsPlanID, Int64 TransactionID, Int64 TransactionDetailID, Int64 PatientID, Int64 TrackingTransactionID)
        {
            DataTable _dtBillingTransactionLines = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                //oParameters.Add("@nInsCompanyID", InsCompanyID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nInsContactID", InsContactID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oParameters.Add("@nInsPlanID", InsPlanID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionDetailID", TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", PatientID , ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTrackingTransactionID", TrackingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("BL_SELECT_PaymentTransaction_Lines_InsCompany_Tracking_REVISED", oParameters, out _dtBillingTransactionLines);
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
                //if (_dtBillingTransactionLines != null) { _dtBillingTransactionLines.Dispose(); }
            }

            return _dtBillingTransactionLines;
        }

        public static PaymentInsuranceClaim GetBillingTransaction(Int64 ClaimNumber, string SubClaimNumber, Int64 InsContactID, Int64 InsPlanID)
        {
            DataTable _dtBillingTransactionLines = new DataTable();
            
            PaymentInsuranceClaim oPaymentClaim = null;
            PaymentInsuranceLine  oPaymentClaimLine = null;

            try
            {
                SplitClaimDetails _claimDetails = new SplitClaimDetails(ClaimNumber, SubClaimNumber);

                if (_claimDetails.TransactionMasterID > 0 && _claimDetails.TransactionID > 0)
                {
                    #region " Set Transaction Master Data "
                    
                    DataRow _drBillingTransaction = GetBillingTransactions(_claimDetails.TransactionMasterID, _claimDetails.TransactionID);
                    if (_drBillingTransaction != null)
                    {
                        oPaymentClaim = new global::gloBilling.EOBPayment.Common.PaymentInsuranceClaim();

                        oPaymentClaim.ClaimNo = Convert.ToInt64(_drBillingTransaction["nClaimNo"]);
                        oPaymentClaim.DisplayClaimNo = GetFormattedClaimPaymentNumber(_drBillingTransaction["nClaimNo"].ToString());
                        oPaymentClaim.ClaimNoPrefix = Convert.ToString(_drBillingTransaction["sCaseNoPrefix"]);
                        oPaymentClaim.BillingTransactionID = Convert.ToInt64(_drBillingTransaction["nTransactionID"]);
                        oPaymentClaim.BillingTransactionDate = Convert.ToInt64(_drBillingTransaction["nTransactionDate"]);

                        oPaymentClaim.TrackBillingTrnID = Convert.ToInt64(_drBillingTransaction["TrackingTrnID"]);
                        oPaymentClaim.SubClaimNo = Convert.ToString(_drBillingTransaction["SubClaimNo"]);

                        oPaymentClaim.PatientID = Convert.ToInt64(_drBillingTransaction["nPatientID"]);
                        oPaymentClaim.PatientName = Convert.ToString(_drBillingTransaction["PatientName"]);
                    }

                    #endregion " Set Transaction Master Data "

                    #region "Retrieve Billing Transaction Lines Data "

                    _dtBillingTransactionLines = GetBillingTransactionLines(InsContactID, InsPlanID, oPaymentClaim.BillingTransactionID, 0, oPaymentClaim.PatientID, oPaymentClaim.TrackBillingTrnID);
                    if (_dtBillingTransactionLines != null && _dtBillingTransactionLines.Rows.Count > 0)
                    {
                        foreach (DataRow _drBillingTransactionLine in _dtBillingTransactionLines.Rows)
                        {
                            oPaymentClaimLine = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLine();

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

                            oPaymentClaimLine.BLInsuranceID = Convert.ToInt64(_drBillingTransactionLine["LineInsuranceID"].ToString());
                            oPaymentClaimLine.BLInsuranceName = Convert.ToString(_drBillingTransactionLine["LineInsuranceName"].ToString());
                            oPaymentClaimLine.BLInsuranceFlag = ((InsuranceTypeFlag)Convert.ToInt32(_drBillingTransactionLine["LineInsuranceFlag"]));

                            oPaymentClaimLine.Charges = Convert.ToDecimal(_drBillingTransactionLine["dCharges"]);
                            oPaymentClaimLine.Unit = Convert.ToDecimal(_drBillingTransactionLine["dUnit"]);
                            oPaymentClaimLine.TotalCharges = Convert.ToDecimal(_drBillingTransactionLine["dTotal"]);
                            oPaymentClaimLine.Allowed = Convert.ToDecimal(_drBillingTransactionLine["dAllowed"]);

                            oPaymentClaimLine.LinePaidAmount = Convert.ToDecimal(_drBillingTransactionLine["TotalPaidAmount"]);
                            oPaymentClaimLine.LinePaidByPatient = Convert.ToDecimal(_drBillingTransactionLine["TotalPatientPaidAmount"]);
                            oPaymentClaimLine.LinePaidByInsurance = Convert.ToDecimal(_drBillingTransactionLine["TotalInsurancePaidAmount"]);
                            oPaymentClaimLine.LinePaidWriteOff = Convert.ToDecimal(_drBillingTransactionLine["TotalWriteOff"]);
                            oPaymentClaimLine.LinePaidWithHold = Convert.ToDecimal(_drBillingTransactionLine["TotalWithHold"]);
                            oPaymentClaimLine.LineBalance = Convert.ToDecimal(_drBillingTransactionLine["TotalBalanceAmount"]);

                            if (_drBillingTransactionLine["LastInsAllowedAmount"] == DBNull.Value)
                            {
                                oPaymentClaimLine.IsLast_allowedNull = true;
                            }
                            else
                            {
                                oPaymentClaimLine.IsLast_allowedNull = false;
                            }
                            if (Convert.ToString(_drBillingTransactionLine["LastInsAllowedAmount"]).Trim() != "")
                            { oPaymentClaimLine.Last_allowed = Convert.ToDecimal(_drBillingTransactionLine["LastInsAllowedAmount"]); }

                            if (_drBillingTransactionLine["LastInsPaidAmount"] == DBNull.Value)
                            {
                                oPaymentClaimLine.IsLast_paymentNull = true;
                            }
                            else
                            {
                                oPaymentClaimLine.IsLast_paymentNull = false;
                            }
                            if (Convert.ToString(_drBillingTransactionLine["LastInsPaidAmount"]).Trim() != "")
                            { oPaymentClaimLine.Last_payment = Convert.ToDecimal(_drBillingTransactionLine["LastInsPaidAmount"]); }

                            if (_drBillingTransactionLine["LastInsWriteOffAmount"] == DBNull.Value)
                            {
                                oPaymentClaimLine.IsLast_writeoffNull = true;
                            }
                            else
                            {
                                oPaymentClaimLine.IsLast_writeoffNull = false;
                            }
                            if (Convert.ToString(_drBillingTransactionLine["LastInsWriteOffAmount"]).Trim() != "")
                            { oPaymentClaimLine.Last_writeoff = Convert.ToDecimal(_drBillingTransactionLine["LastInsWriteOffAmount"]); }

                            if (_drBillingTransactionLine["LastInsCopayAmount"] == DBNull.Value)
                            {
                                oPaymentClaimLine.IsLast_copayNull = true;
                            }
                            else
                            {
                                oPaymentClaimLine.IsLast_copayNull = false;
                            }
                            if (Convert.ToString(_drBillingTransactionLine["LastInsCopayAmount"]).Trim() != "")
                            { oPaymentClaimLine.Last_copay = Convert.ToDecimal(_drBillingTransactionLine["LastInsCopayAmount"]); }

                            if (_drBillingTransactionLine["LastInsDeductibleAmount"] == DBNull.Value)
                            {
                                oPaymentClaimLine.IsLast_deductibleNull = true;
                            }
                            else
                            {
                                oPaymentClaimLine.IsLast_deductibleNull = false;
                            }
                            if (Convert.ToString(_drBillingTransactionLine["LastInsDeductibleAmount"]).Trim() != "")
                            { oPaymentClaimLine.Last_deductible = Convert.ToDecimal(_drBillingTransactionLine["LastInsDeductibleAmount"]); }

                            if (_drBillingTransactionLine["LastInsCoinsuranceAmount"] == DBNull.Value)
                            {
                                oPaymentClaimLine.IsLast_coinsuranceNull = true;
                            }
                            else
                            {
                                oPaymentClaimLine.IsLast_coinsuranceNull = false;
                            }
                            if (Convert.ToString(_drBillingTransactionLine["LastInsCoinsuranceAmount"]).Trim() != "")
                            { oPaymentClaimLine.Last_coinsurance = Convert.ToDecimal(_drBillingTransactionLine["LastInsCoinsuranceAmount"]); }

                            if (_drBillingTransactionLine["LastInsWithholdAmount"] == DBNull.Value)
                            {
                                oPaymentClaimLine.IsLast_withholdNull = true;
                            }
                            else
                            {
                                oPaymentClaimLine.IsLast_withholdNull = false;
                            }
                            if (Convert.ToString(_drBillingTransactionLine["LastInsWithholdAmount"]).Trim() != "")
                            { oPaymentClaimLine.Last_withhold = Convert.ToDecimal(_drBillingTransactionLine["LastInsWithholdAmount"]); }

                            if (Convert.ToString(_drBillingTransactionLine["IsCorrection"]).Trim() != "")
                            { oPaymentClaimLine.Iscorrection = Convert.ToBoolean(_drBillingTransactionLine["IsCorrection"]); }

                            if (Convert.ToString(_drBillingTransactionLine["IsSplitted"]).Trim() != "")
                            { oPaymentClaimLine.IsSplitted = Convert.ToBoolean(_drBillingTransactionLine["IsSplitted"]); }
                            else
                            { oPaymentClaimLine.IsSplitted = false; }

                            oPaymentClaim.CliamLines.Add(oPaymentClaimLine);
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
                //if (oPaymentClaim != null) { oPaymentClaim.Dispose(); }
                if (oPaymentClaimLine != null) { oPaymentClaimLine.Dispose(); }
                if (_dtBillingTransactionLines != null) { _dtBillingTransactionLines.Dispose(); }
            }

            return oPaymentClaim;
        }

        public static DataRow GetClaimDetails(Int64 ClaimNumber, string SubClaimNumber)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;
            string _subClaimNumber = string.Empty;

            DataTable _dtClaimDetails = null;
            DataRow _claimDetails = null;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nClaimno", ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sSubClaimno", SubClaimNumber, ParameterDirection.Input, SqlDbType.VarChar, 50);

                oDB.Retrive("BL_Select_SplitClaims", oParameters, out _dtClaimDetails);
                oDB.Disconnect();

                if (_dtClaimDetails != null && _dtClaimDetails.Rows.Count > 0)
                {
                    _claimDetails = _dtClaimDetails.Rows[0];
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
                if (_dtClaimDetails != null) { _dtClaimDetails.Dispose(); }
            }
            return _claimDetails;
        }

        public static DataTable GetCorrectionRefList(decimal CorrectionAmount, Int64 PatientID, Int64 BillingTransactionID, Int64 BillingTransactionDetailID, Int64 PatientInsuranceID, Int64 ContactInsuranceID) // Parameter removed , Int64 InsuranceCompanyID
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            DataTable _dtCorrectionRef = new DataTable();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@CorrectionAmount", CorrectionAmount, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),
                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),
                oParameters.Add("@nBillingTransactionID", BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//   numeric(18,0),
                oParameters.Add("@nBillingTransactionDetailID", BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0)
                oParameters.Add("@nInsuranceID", PatientInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),
                oParameters.Add("@nContactID", ContactInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                //oParameters.Add("@nInsuranceCompanyID", InsuranceCompanyID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0)
                oDB.Retrive("BL_SELECT_EOBInsCorrectionAmountList_REVISED", oParameters, out _dtCorrectionRef);

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

        public static DataTable GetUserList()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            DataTable _dtUsers = new DataTable();

            try
            {
                string _sqlQuery = "Select nUserID,nProviderID,sLoginName,sFirstName,sMiddleName,sLastName,sEmail,sExchangeLogin from User_MST WITH(NOLOCK)" +
                                    " where nClinicID = 1 AND ISNULL(nBlockStatus,'false') <> 'true'" +
                                    " AND ISNULL(nBlockStatus,'false') <> 'true' order by sLoginName";
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtUsers);
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
                if (_dtUsers != null) { _dtUsers.Dispose(); }
            }
            return _dtUsers;
        }

        public static string GetClaimRemittanceRefNo(Int64 TransactionMasterID, Int64 ContactID, Int64 InsuranceID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);

            string _claimRemittanceRefNo = string.Empty;
            string _sqlQuery = string.Empty;
            Object _retVal = null;

            try
            {
                _sqlQuery = "select isnull(sClaimRemittanceRefNo,'') as sClaimRemittanceRefNo from BL_Transaction_ClaimRemittanceRef WITH(NOLOCK) where nTransactionID = " + TransactionMasterID + " and nContactID ='" + ContactID + "' and nInsuranceID = '" + InsuranceID + "' and nclinicID = '" + AppSettings.ClinicID + "'";

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

        public static DataRow GetBillingHoldNote(Int64 TransactionMasterID, Int64 TransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;

            DataTable _dtHoldNotes = new DataTable();
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

        public static bool IsClaimHasChilds(Int64 nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            object _retVal = null;
            bool _isSplitted = false;

            try
            {
                _sqlQuery = " select count(nTransactionID) as SplitedClaimCount from BL_Transaction_Claim_MST WITH (NOLOCK)" +
                            " where substring(nSubClaimno,1,1) <> '-' and nParentTransactionID = " + nTransactionID + " ";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                { _isSplitted = true; }
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
            return _isSplitted;
        }

        public static bool IsPaymentTrayActive(Int64 TrayId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            object _retVal = null;
            bool _isActiveTray = false;

            try
            {
                _sqlQuery = " SELECT ISNULL(bIsActive,1) AS IsActive FROM BL_CloseDayTray WITH(NOLOCK) " +
                            " WHERE nCloseDayTrayID = " + TrayId + " AND nClinicID = " + AppSettings.ClinicID + "";

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

        //public static bool IsCheckUpdating(Int64 EOBPaymentID, string CheckNumber, Int64 CheckDate, int PaymentMode, decimal CheckAmount)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
        //    string _sqlQuery = "";
        //    object _retVal = null;
        //    bool _isUpdating = false;

        //    try
        //    {
        //        bool isExist = IsExistCheck(CheckNumber, CheckDate, CheckAmount, EOBPaymentAccountType.InsuranceCompany);
        //        if (isExist)
        //        {
        //            _sqlQuery = " SELECT CASE COUNT(*) WHEN 0 THEN 1 ELSE 0 END AS isCheckUpdating FROM BL_EOBPayment_MST WITH(NOLOCK) " +
        //                        " WHERE nEOBPaymentID = " + EOBPaymentID +
        //                        " AND UPPER(sCheckNumber) = '" + CheckNumber.Trim().ToUpper().Replace("'", "''") + "' " +
        //                        " AND nCheckDate = " + CheckDate + " AND nCheckAmount = " + CheckAmount + " AND nPaymentMode = " + PaymentMode;

        //            oDB.Connect(false);
        //            _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
        //            oDB.Disconnect();

        //            if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
        //            { _isUpdating = true; }
        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //        if (_retVal != null) { _retVal = null; }
        //    }
        //    return _isUpdating;
        //}

        public static bool IsCheckUpdating(Int64 EOBPaymentID, string CheckNumber, Int64 CheckDate, int PaymentMode, decimal CheckAmount)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            object _retVal = null;
            bool _isUpdating = false;

            try
            {
             
                    _sqlQuery = " SELECT CASE COUNT(*) WHEN 0 THEN 1 ELSE 0 END AS isCheckUpdating FROM BL_EOBPayment_MST WITH(NOLOCK) " +
                                " WHERE nEOBPaymentID = " + EOBPaymentID +
                                " AND UPPER(sCheckNumber) = '" + CheckNumber.Trim().ToUpper().Replace("'", "''") + "' " +
                                " AND nCheckDate = " + CheckDate + " AND nCheckAmount = " + CheckAmount + " AND nPaymentMode = " + PaymentMode;

                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                    { _isUpdating = true; }
               
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

        public static bool IsResponsibilityBilled(Int64 ClaimNumber, string SubClaimNumber, Int64 InsuranceID, int SelectedResponsibility, string SelectedAction)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
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

                DataTable _dtClaimInsurances = new DataTable();
                DataRow _drClaimInsurances = null;

                _sqlQuery = " SELECT IsBilled_EOB,IsBilled_NEXT,nResponsibilityNo,IsBilledEOBVoided FROM dbo.view_ClaimInsurances WHERE nClaimNo = " + ClaimNumber + " AND nInsuranceID = " + InsuranceID;

                //_sqlQuery = " SELECT COUNT(DISTINCT(nEOBPaymentID)) AS IsBilled FROM BL_EOBPayment_EOB as EOB " +
                //            " WHERE EOB.nClaimNo = " + ClaimNumber + " AND bIsPaymentVoid != 1 AND nVoidType != 5" + //" AND EOB.sSubClaimNo = '" + SubClaimNumber + "'" +
                //            " AND EOB.nInsuraceID IN (SELECT PatientInsurance_DTL.nInsuranceID FROM bl_Transaction_InsPlan " +
                //            " INNER JOIN PatientInsurance_DTL ON bl_Transaction_InsPlan.nInsuranceID = PatientInsurance_DTL.nInsuranceID " +
                //            " WHERE bl_Transaction_InsPlan.nClaimNo = " + ClaimNumber + " AND bl_Transaction_InsPlan.nInsuranceID = " + InsuranceID + ")";

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

                //if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                //{
                //    _isBilled = Convert.ToBoolean(_retVal);

                //    if (!_isBilled)
                //    { _isBilled = IsResponsibilityTransactedOrBilled(ClaimNumber, SubClaimNumber, InsuranceID); }
                //    else
                //    { _isBilled = true; }
                //}

                //if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                //{ _isBilled = true; }
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

        static bool IsResponsibilityTransactedOrBilled(Int64 ClaimNumber, string SubClaimNumber, Int64 InsuranceID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            object _retVal = null;
            bool _isBilled = false;

            try
            {
                _sqlQuery = " SELECT COUNT(DISTINCT(nNextActionPatientInsID)) IsBilled FROM dbo.BL_EOB_NextAction_HST WITH(NOLOCK) " +
                            " WHERE sNextActionCode IN ('T','B') AND nClaimNo = " + ClaimNumber + " AND nNextActionPatientInsID = " + InsuranceID;
                            //" WHERE sNextActionCode IN ('T','B') AND nClaimNo = " + ClaimNumber + " AND nNextActionPartyNumber = " + ResponsibilityNo;

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                { _isBilled = true; }
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

        public static Int64 GetClaimInsuranceID(Int64 ClaimNumber, Int64 ResponsibilityNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);

            string _sqlQuery = string.Empty;
            Int64 _insuranceID = 0;
            Object _retVal = null;

            try
            {
                _sqlQuery = " SELECT PatientInsurance_DTL.nInsuranceID FROM bl_Transaction_InsPlan WITH(NOLOCK) " +
                            " INNER JOIN PatientInsurance_DTL WITH(NOLOCK) ON bl_Transaction_InsPlan.nInsuranceID = PatientInsurance_DTL.nInsuranceID " +
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

        public static DataTable GetInsurancePendingChecks(Int64 InsuranceCompanyID, Int64 CloseDate, Int64 UserID, bool ShowCompletedOnly)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            DataTable _dtPaymentLog = new DataTable();

            try
            {
                string _sqlQuery = "SELECT nEOBPaymentID,nPayerID,nUserID,bIsDayClosed,[Ins. Company],[Check #],[Check Date],"
                                  + " CloseDate,[Check Amount],DebitAmount,Remaining,nClinicID "
                                  + " FROM view_InsurancePendingChecks_Revised where nClinicID = " + AppSettings.ClinicID;

                if (InsuranceCompanyID != 0)
                { _sqlQuery += " AND nPayerID = " + InsuranceCompanyID; }

                if (UserID != 0)
                { _sqlQuery += " AND nUserID = " + UserID; }

                if (CloseDate != 0)
                { _sqlQuery += " AND CloseDate =  dbo.CONVERT_TO_DATE(convert(varchar,'" + CloseDate + "'))"; }

                if (ShowCompletedOnly)
                { _sqlQuery += " AND Remaining <> 0 "; }


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

        public static bool IsValidReasonCode(string ReasonCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            object _retVal = null;

            DataTable _dtReasonCodes = new DataTable();

            bool _isValid = false;

            try
            {
                //_sqlQuery = " select nReasonID,isnull(sGroupCode,'') + isnull(sCode,'') as ReasonCode ,sDescription from BL_ReasonCodes_MST WITH(NOLOCK) " + 
                //            " where (bIsBlock IS NULL OR bIsBlock = '" + false + "') AND nClinicID = " + AppSettings.ClinicID + " ORDER BY ReasonCode,nReasonID";

                _sqlQuery = " (SELECT nReasonID,isnull(sGroupCode,'') + isnull(sCode,'') as ReasonCode ,sDescription FROM BL_ReasonCodes_MST WITH(NOLOCK)  " +
                            " WHERE (bIsBlock IS NULL OR bIsBlock = '" + false + "') AND nClinicID = 1 ) " +
                            " UNION " +
                            " (SELECT nReasonCodeID,isnull(sGroupCode,'') + isnull(sReasonCode,'') as ReasonCode ,'' as sDescription FROM ERA_PayerReasonCodes WITH(NOLOCK)  " +
                            " WHERE  nClinicID = " + AppSettings.ClinicID + " ) " +
                            " UNION " +
                            " (SELECT nReasonCodeID,isnull(sGroupCode,'') + isnull(sReasonCode,'') as ReasonCode ,'' as sDescription FROM Insurance_DefaultReasonCodes WITH(NOLOCK)  " +
                            " WHERE  nClinicID = " + AppSettings.ClinicID + " ) " +
                            " ORDER BY ReasonCode,nReasonID ";




                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtReasonCodes);
                oDB.Disconnect();

                if (_dtReasonCodes != null && _dtReasonCodes.Rows.Count > 0)
                {
                    DataView dvReasonCodes = new DataView(_dtReasonCodes, "", "ReasonCode", DataViewRowState.CurrentRows);

                    if (!dvReasonCodes.FindRows(ReasonCode).Length.Equals(0))
                    { _isValid = true; }
                    else
                    { _isValid = false; }
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
            return _isValid;
        }
        // Code added for Remark code association - 8060
        public static bool IsValidRemarkCode(string RemarkCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            object _retVal = null;

            DataTable _dtRemarkCodes = new DataTable();

            bool _isValid = false;

            try
            {
                _sqlQuery = " select nRemarkID ,sRemarkCode,sRemarkDescription from BL_RemarkCodes_MST WITH(NOLOCK) " +
                            " where (bIsBlock IS NULL OR bIsBlock = '" + false + "') AND nClinicID = " + AppSettings.ClinicID + " AND sRemarkCode = '" + RemarkCode + "' ";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtRemarkCodes);
                oDB.Disconnect();

                if (_dtRemarkCodes != null && _dtRemarkCodes.Rows.Count > 0)
                {
                    _isValid = true;
                }
                else { _isValid = false; }
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
            return _isValid;
        }

        public static DataTable GetInsurancePaymentRefundLog(Int64 InsuranceCompanyID, string PaymentTrayIDs, Int64 UserID, string CheckNumber, Int64 PaymentDate, Int64 CloseDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            DataTable _dtPaymentLog = new DataTable();

            try
            {
                string _sqlQuery = "SELECT * FROM view_InsuranceCompanyReFunds where nClinicID = " + AppSettings.ClinicID;

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

        public static bool IsRefunded(Int64 EOBPaymentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            Object _retVal = false;
            string _sqlQuery = "";
            bool _IsRefunded = false;

            try
            {
                if (EOBPaymentID > 0)
                {
                    oDB.Connect(false);
                    _sqlQuery = "SELECT nEOBPaymentID FROM BL_EOBPayment_DTL WITH (NOLOCK) " +
                    " WHERE nPaymentType = 5 " +
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
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _IsRefunded;
        }

        public static DataTable RefundedCheckDetails(Int64 EOBPaymentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            DataTable _dtRefundDetails = new DataTable();
            string _sqlQuery = "";
            try
            {
                if (EOBPaymentID > 0)
                {
                    oDB.Connect(false);
                    _sqlQuery = " SELECT ISNULL(nEOBPaymentID,0) AS nEOBPaymentID,ISNULL(sRefundTo,'') AS RefundTo," +
                                " ISNULL(nRefundAmount,0) AS RefundAmt," +
                                " ISNULL(sCheckNumber,'') AS RefundCheckNo," +
                                " dbo.Convert_To_Date(ISNULL(nCheckDate,0)) AS RefundCheckDate" +
                                " FROM BL_EOBInsurance_Refund WITH (NOLOCK) " +
                                " WHERE nEOBPaymentID IN " +
                                " (SELECT nEOBPaymentID FROM BL_EOBPayment_DTL WITH (NOLOCK) " +
                                " WHERE nRefEOBPaymentID = " + EOBPaymentID +
                                " AND (nPaymentType = 5 AND nPaymentSubType = 14 AND nPaySign = 2)) AND ISNULL(bIsVoid,0) <> 1";
                    oDB.Retrive_Query(_sqlQuery,out _dtRefundDetails);
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

        #region " Functions taken as it is from ClsBL_PaymentInsurance.cs "

        public static Int64 GetPrefixTransactionID()
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
                //returns random number if exception occures
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }
            return _Result;
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

        #endregion

        #region " Save EOB Methods "

        public static Int64 SaveEOBPayment(SqlConnection _sqlConnection, SqlTransaction _sqlTransaction, EOBPayment.Common.PaymentInsurance EOBPaymentInsurance, bool IsSaveForVoid, out Int64 EOBId, out bool _isDataSaved, bool _IsCheckSaved, Int64 _CheckSavedEOBID)
        {
            // Reset the out parameter
            _isDataSaved = false;
            EOBId = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);    

            #region " Variables declaration "

            System.Data.SqlClient.SqlCommand _sqlCommand = null;
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

            #endregion

            try
            {
                if (EOBPaymentInsurance != null)
                {
                    if (_IsCheckSaved == true)
                    {
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
                        oParameters.Add("@nBPRID", EOBPaymentInsurance.BPRID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                        //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  add 3 more parameter to save PAF values  while saving to BL_INUP_EOBPayment_MST
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

                        #region "Master Payment Note"

                        if (EOBPaymentInsurance.Notes != null && EOBPaymentInsurance.Notes.Count > 0)
                        {
                           // Object _RcValue = null;

                            for (int rcInd = 0; rcInd < EOBPaymentInsurance.Notes.Count; rcInd++)
                            {
                               // _RcValue = null;
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
                                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
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
                            }
                        }


                        #endregion

                        #endregion " Master Data Save "
                    }
                    else
                    {
                        _EOBPayId = _CheckSavedEOBID;
                    }

                    #region "Payment Line Master (Credit) Entry, Total Check Amount Entry, but it is in same table "
                    if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails != null && EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count > 0)
                    {
                        for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count; clmInsPayLnIndex++)
                        {
                            if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex] != null)
                            {
                                if (_IsCheckSaved == true)
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
                                    //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  add 3 more parameter to save PAF values  while saving
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

                                    EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentID = _EOBPayId;
                                    EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentDetailID = _EOBPayDtlId;
                                    TempEOBPaymentDetailId = _EOBPayDtlId;
                                }
                                else
                                {
                                    _EOBPayDtlId = TempEOBPaymentDetailId;
                                    EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentID = _EOBPayId;
                                    EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentDetailID = TempEOBPaymentDetailId;
                                }

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
                                //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  add 3 more parameter to save PAF values  while saving
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
                                        oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
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
                                    }
                                }

                                #endregion " Add Line Notes "

                                #region "Add Reserve Association Entry"
                            
                                oParameters.Clear();
                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nEOBPaymentDetailID", _EOBPayDtlId, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nTransactionID", EOBInsPayDtl.ReserveAssociationMSTTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nTrackTrnID", EOBInsPayDtl.ReserveAssociationTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nPatientID", EOBInsPayDtl.ReserveAssociationPatientID, ParameterDirection.Input, SqlDbType.BigInt);

                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "BL_INUP_Reserve_Association";

                                _result = _sqlCommand.ExecuteNonQuery();
  

                                #endregion
                                EOBInsPayDtl = null;
                            }
                        }
                    }
                    #endregion

                    #region " EOB Data Save "

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
                                        oParameters.Clear();
                                        PaymentInsClaimLine = EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex];

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
                                        
                                        //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  add 3 more parameter to save PAF values  while saving
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

                                        #region " Update correction credit lines for CPT with nEOBID & nEOBDtlID

                                        if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails != null && EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count > 0)
                                        {
                                            string _sqlQuery = "";
                                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count; clmInsPayLnIndex++)
                                            {
                                                if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex] != null
                                                 && EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].PaymentType == EOBPaymentType.InsuracePayment
                                                 && EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].PaymentSubType == EOBPaymentSubType.Correction
                                                 && EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].PaySign == EOBPaymentSign.Payment_Credit
                                                 && EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].BillingTransactionID == PaymentInsClaimLine.BLTransactionID
                                                 && EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].BillingTransactionDetailID == PaymentInsClaimLine.BLTransactionDetailID)
                                                {
                                                    _result = 0;
                                                    _sqlQuery = "";

                                                    _sqlQuery = " UPDATE " +
                                                                " BL_EOBPayment_DTL WITH (READPAST) " +
                                                                " SET nEOBID = " + _EOBId + ", " +
                                                                " nEOBDtlID = " + _EOBDtlId + "  " +
                                                                " WHERE nEOBPaymentDetailID = " + EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentDetailID + " " +
                                                                " AND nEOBPaymentID = " + EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentID + " " +
                                                                " AND ISNULL(BL_EOBPayment_DTL.nEOBID,0) = 0  AND ISNULL(BL_EOBPayment_DTL.nEOBDtlID,0) = 0 " +
                                                                " AND (ISNULL(BL_EOBPayment_DTL.nPaymentType,0) = 4  AND ISNULL(BL_EOBPayment_DTL.nPaymentSubType,0) = 13 AND ISNULL(BL_EOBPayment_DTL.nPaySign,0) = 1) ";

                                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                    _sqlCommand.Connection = _sqlConnection;
                                                    _sqlCommand.Transaction = _sqlTransaction;
                                                    _sqlCommand.CommandType = CommandType.Text;
                                                    _sqlCommand.CommandText = _sqlQuery;
                                                    _result = _sqlCommand.ExecuteNonQuery();
                                                }
                                            }
                                        }


                                        #endregion " Update correction credit lines for CPT with nEOBID & nEOBDtlID

                                        #endregion

                                        #region " EOB Service Line Details (Next Action, Next Party, Reason Codes, Notes etc. "

                                        #region " Add Line NextAction  "

                                        if (PaymentInsClaimLine.LineNextAction != null && PaymentInsClaimLine.LineNextAction.HasData == true)
                                        {
                                            //Object _nextActRetVal = null;
                                            //oParameters.Clear();

                                            //oParameters.Add("@nID", PaymentInsClaimLine.LineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                            //oParameters.Add("@nClaimNo", PaymentInsClaimLine.LineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            //oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            //oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            //oParameters.Add("@nEOBPaymentDetailID", PaymentInsClaimLine.LineNextAction.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                            //oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.LineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                            //oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.LineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                            //oParameters.Add("@nTrackTrnID", PaymentInsClaimLine.LineNextAction.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                            //oParameters.Add("@nTrackTrnDtlID", PaymentInsClaimLine.LineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                            //oParameters.Add("@sSubClaimNo", PaymentInsClaimLine.LineNextAction.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                                            //oParameters.Add("@nTrackTrnLineNo", 0, ParameterDirection.Input, SqlDbType.Int);// int  

                                            //oParameters.Add("@nNextActionPatientInsID", PaymentInsClaimLine.LineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                                            //oParameters.Add("@nNextActionPatientInsName", PaymentInsClaimLine.LineNextAction.NextActionPatientInsName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                            //oParameters.Add("@nNextActionPartyNumber", PaymentInsClaimLine.LineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                                            //oParameters.Add("@nNextPartyType", PaymentInsClaimLine.LineNextAction.NextPartyType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                                            //oParameters.Add("@nNextActionContactID", PaymentInsClaimLine.LineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            //oParameters.Add("@sNextActionCode", PaymentInsClaimLine.LineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                            //oParameters.Add("@sNextActionDescription", PaymentInsClaimLine.LineNextAction.NextActionDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                            //if (PaymentInsClaimLine.LineNextAction.IsNullNextActionAmount == false)
                                            //{
                                            //    oParameters.Add("@dNextActionAmount", PaymentInsClaimLine.LineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                            //}
                                            //else
                                            //{
                                            //    oParameters.Add("@dNextActionAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                            //}

                                            //oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            //oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                            //// ----------------------------------------
                                            //// Parameters added - Pankaj bedse 29012010
                                            //oParameters.Add("@nCloseDate", PaymentInsClaimLine.LineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            //oParameters.Add("@nUserID", PaymentInsClaimLine.LineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            //oParameters.Add("@sUserName", PaymentInsClaimLine.LineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),
                                            //// ----------------------------------------

                                            //_sqlCommand = new System.Data.SqlClient.SqlCommand();
                                            //_sqlCommand = GetCmdParameters(oParameters);
                                            //_sqlCommand.Connection = _sqlConnection;
                                            //_sqlCommand.Transaction = _sqlTransaction;
                                            //_sqlCommand.CommandType = CommandType.StoredProcedure;
                                            //_sqlCommand.CommandText = "BL_UP_EOBNextAction_Tracking";

                                            //_result = _sqlCommand.ExecuteNonQuery();

                                            //if (_sqlCommand.Parameters["@nID"].Value != null)
                                            //{ _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                            //else
                                            //{ _retVal = 0; }
                                        }

                                        #endregion " Add Line NextAction "

                                        #region " Add Line Next Party "

                                        if (PaymentInsClaimLine.LineNextAction != null && PaymentInsClaimLine.LineNextAction.HasData == true && PaymentInsClaimLine.LineNextAction.HasActionData)
                                        {
                                            //Object _nextActRetVal = null;

                                            //oParameters.Clear();

                                            //oParameters.Add("@nID", PaymentInsClaimLine.LineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                            //oParameters.Add("@nClaimNo", PaymentInsClaimLine.LineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            //oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            //oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            //oParameters.Add("@nEOBPaymentDetailID", PaymentInsClaimLine.LineNextAction.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                            //oParameters.Add("@nBillingTransactionID", PaymentInsClaimLine.LineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                            //oParameters.Add("@nBillingTransactionDetailID", PaymentInsClaimLine.LineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                            //oParameters.Add("@nTrackMstTrnID", PaymentInsClaimLine.LineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                            //oParameters.Add("@nTrackMstTrnDetailID", PaymentInsClaimLine.LineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                            //oParameters.Add("@sSubClaimNo", PaymentInsClaimLine.LineNextAction.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                                            ////oParameters.Add("@nTrackTrnLineNo", 0, ParameterDirection.Input, SqlDbType.Int);// int  

                                            //oParameters.Add("@nNextActionPatientInsID", PaymentInsClaimLine.LineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                                            //oParameters.Add("@nNextActionPatientInsName", PaymentInsClaimLine.LineNextAction.NextActionPatientInsName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                            //oParameters.Add("@nNextActionPartyNumber", PaymentInsClaimLine.LineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                                            //oParameters.Add("@nNextPartyType", PaymentInsClaimLine.LineNextAction.NextPartyType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                                            //oParameters.Add("@nNextActionContactID", PaymentInsClaimLine.LineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            //oParameters.Add("@sNextActionCode", PaymentInsClaimLine.LineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                            //oParameters.Add("@sNextActionDescription", PaymentInsClaimLine.LineNextAction.NextActionDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                            //if (PaymentInsClaimLine.LineNextAction.IsNullNextActionAmount == false)
                                            //{
                                            //    oParameters.Add("@dNextActionAmount", PaymentInsClaimLine.LineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                            //}
                                            //else
                                            //{
                                            //    oParameters.Add("@dNextActionAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                            //}

                                            //oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            //oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                            //// ----------------------------------------
                                            //// Parameters added - Pankaj bedse 29012010
                                            //oParameters.Add("@nCloseDate", PaymentInsClaimLine.LineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            //oParameters.Add("@nUserID", PaymentInsClaimLine.LineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            //oParameters.Add("@sUserName", PaymentInsClaimLine.LineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),
                                            //// ----------------------------------------

                                            //_sqlCommand = new System.Data.SqlClient.SqlCommand();
                                            //_sqlCommand = GetCmdParameters(oParameters);
                                            //_sqlCommand.Connection = _sqlConnection;
                                            //_sqlCommand.Transaction = _sqlTransaction;
                                            //_sqlCommand.CommandType = CommandType.StoredProcedure;
                                            //_sqlCommand.CommandText = "BL_UP_EOBParty";

                                            //_result = _sqlCommand.ExecuteNonQuery();

                                            //if (_sqlCommand.Parameters["@nID"].Value != null)
                                            //{ _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                            //else
                                            //{ _retVal = 0; }
                                        }

                                        #endregion " Add Line Next Party "

                                        #region " Add Line Reason Codes "

                                        if (PaymentInsClaimLine.LineResonCodes != null && PaymentInsClaimLine.LineResonCodes.Count > 0)
                                        {
                                           // Object _RcValue = null;

                                            for (int rcInd = 0; rcInd < PaymentInsClaimLine.LineResonCodes.Count; rcInd++)
                                            {
                                                //_RcValue = null;
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
                                                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
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
                                                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
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

                                                    //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  add 3 more parameter to save PAF values  while saving
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

                                                }
                                            }
                                        }


                                        #endregion " EOB Financial Service Line Save "

                                        PaymentInsClaimLine = null;
                                    }
                                }
                            }
                        }

                        #endregion
                    }

                    #endregion " EOB Data Save "

                    #region " Save last selected Close date "

                    gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
                    oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(gloDateMaster.gloDate.DateAsDate(EOBPaymentInsurance.CloseDate)).ToString("MM/dd/yyyy"), AppSettings.ClinicID, EOBPaymentInsurance.UserID, gloSettings.SettingFlag.User);
                    oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", EOBPaymentInsurance.PaymentTrayID.ToString(), AppSettings.ClinicID, EOBPaymentInsurance.UserID, gloSettings.SettingFlag.User);
                    oSettings.Dispose();

                    //start Abhisekh  2 sept 2010

                    gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, AppSettings.ConnectionStringEMR);
                    ogloBilling.SaveUserWiseCloseDay(gloDateMaster.gloDate.DateAsDate(EOBPaymentInsurance.CloseDate).ToString(), CloseDayType.Payment, EOBPaymentInsurance.ClinicID);
                    ogloBilling.Dispose();

                    //end 

                    #endregion " Save last selected Close date "

                    _isDataSaved = true;
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                _isDataSaved = false;
                ex.ERROR_Log(ex.ToString()); }
            catch (Exception ex)
            {
                _isDataSaved = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (_sqlCommand != null) { _sqlCommand.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_retVal != null) { _retVal = null; }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return _EOBPayId;
        }

        public static void SaveEOBNextAction(SqlConnection _sqlConnection, SqlTransaction _sqlTransaction, ref PaymentInsuranceLineNextActions oPaymentInsuranceLineNextActions, Int64 nEOBPaymentID, Int64 nEOBID, out bool _isDataSaved)
        {
            //Reset the out parameter
            _isDataSaved = false;

            //SqlConnection _sqlConnection = new SqlConnection(AppSettings.ConnectionStringPM);
            //SqlTransaction _sqlTransaction = null;

            PaymentInsuranceLineNextAction oLineNextAction = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);    
            try
            {
                //_sqlConnection.Open();
                //_sqlTransaction = _sqlConnection.BeginTransaction();
                //object _retVal = null;

                if (oPaymentInsuranceLineNextActions != null && oPaymentInsuranceLineNextActions.Count > 0)
                {
                    for (int index = 0; index < oPaymentInsuranceLineNextActions.Count; index++)
                    {
                        //_retVal = null;
                        oLineNextAction = oPaymentInsuranceLineNextActions[index];

                        //UpdateNextParty(oLineNextAction, _sqlConnection, _sqlTransaction, nEOBPaymentID, nEOBID);
                        //UpdateNextAction(oLineNextAction, _sqlConnection, _sqlTransaction, nEOBPaymentID, nEOBID);

                        using (gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters())
                        {
                            //oParameters.Add("@nID", oLineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                            //oParameters.Add("@nClaimNo", oLineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                            oParameters.Add("@nEOBID", nEOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nEOBPaymentID", nEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nEOBPaymentDetailID", oLineNextAction.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                            oParameters.Add("@nNextActionPatientInsID", oLineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                            oParameters.Add("@nNextActionPatientInsName", oLineNextAction.NextActionPatientInsName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                            oParameters.Add("@nNextActionPartyNumber", oLineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                            oParameters.Add("@nNextActionContactID", oLineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nNextPartyType", oLineNextAction.NextPartyType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                            
                            oParameters.Add("@sNextActionCode", oLineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                            oParameters.Add("@sNextActionDescription", oLineNextAction.NextActionDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),

                            if (oLineNextAction.IsNullNextActionAmount == false)
                            { oParameters.Add("@dNextActionAmount", oLineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal); }
                            else
                            { oParameters.Add("@dNextActionAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal); }

                            //oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                            oParameters.Add("@nCloseDate", oLineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nUserID", oLineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@sUserName", oLineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),
                            oParameters.Add("@sSubClaimNo", oLineNextAction.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),

                            oParameters.Add("@nTrackMstTrnID", oLineNextAction.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                            oParameters.Add("@nTrackMstTrnDetailID", oLineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                            oParameters.Add("@nTrackTrnLineNo", 0, ParameterDirection.Input, SqlDbType.Int);// int  

                            oParameters.Add("@nBillingTransactionID", oLineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                            oParameters.Add("@nBillingTransactionDetailID", oLineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nHSTID", 0, ParameterDirection.Output, SqlDbType.BigInt);//	numeric(18, 0),

                            using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                            {
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "BL_UP_EOBNextAction_Revised";

                                int _result = _sqlCommand.ExecuteNonQuery();

                                if ((_sqlCommand.Parameters["@nHSTID"] != null) && (_sqlCommand.Parameters["@nHSTID"].Value != null) && (_sqlCommand.Parameters["@nHSTID"].Value != DBNull.Value))
                                { oPaymentInsuranceLineNextActions[index].nHSTID = Convert.ToInt64(_sqlCommand.Parameters["@nHSTID"].Value); }
                                //else
                                //{ _retVal = 0; }
                            }
                        }
                        oLineNextAction.Dispose();
                    }
                }
                _isDataSaved = true;
                //_sqlTransaction.Commit();
                //_sqlConnection.Close();
            }
            catch (Exception ex)
            {
                _isDataSaved = false;
                //_sqlTransaction.Rollback();
                //_sqlConnection.Close();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                //if (_sqlTransaction != null) { _sqlTransaction.Dispose(); }
                if (oLineNextAction != null) { oLineNextAction.Dispose(); }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        public static void SaveEOBNextActionHistory(ref PaymentInsuranceLineNextActions oPaymentInsuranceLineNextActions, Int64 nEOBPaymentID, Int64 nEOBID, out bool _isDataSaved)
        {
            //Reset the out parameter
            _isDataSaved = false;

            SqlConnection _sqlConnection = new SqlConnection(AppSettings.ConnectionStringPM);
            SqlTransaction _sqlTransaction = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);    
            PaymentInsuranceLineNextAction oLineNextAction = null;

            try
            {
                _sqlConnection.Open();
                //_sqlTransaction = _sqlConnection.BeginTransaction();
                //object _retVal = null;

                if (oPaymentInsuranceLineNextActions != null && oPaymentInsuranceLineNextActions.Count > 0)
                {
                    for (int index = 0; index < oPaymentInsuranceLineNextActions.Count; index++)
                    {
                        //_retVal = null;
                        oLineNextAction = oPaymentInsuranceLineNextActions[index];

                        using (gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters())
                        {
                            oParameters.Add("@nBillingTransactionID", oLineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                            oParameters.Add("@nBillingTransactionDetailID", oLineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nNextActionPatientInsID", oLineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                            oParameters.Add("@nNextActionPartyNumber", oLineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                            oParameters.Add("@nNextActionContactID", oLineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@sNextActionCode", oLineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                            oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nNextPartyType", oLineNextAction.NextPartyType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                            oParameters.Add("@nCloseDate", oLineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nUserID", oLineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@sSubClaimNo", oLineNextAction.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                            oParameters.Add("@nTrackMstTrnID", oLineNextAction.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                            oParameters.Add("@nTrackMstTrnDetailID", oLineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                            oParameters.Add("@nHSTID", 0, ParameterDirection.Output, SqlDbType.BigInt);//	numeric(18, 0),

                            using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                            {
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "BL_EOB_NextAction_Add_History";

                                int _result = _sqlCommand.ExecuteNonQuery();

                                if ((_sqlCommand.Parameters["@nHSTID"] != null) && (_sqlCommand.Parameters["@nHSTID"].Value != null) && (_sqlCommand.Parameters["@nHSTID"].Value != DBNull.Value))
                                { oPaymentInsuranceLineNextActions[index].nHSTID = Convert.ToInt64(_sqlCommand.Parameters["@nHSTID"].Value); }
                                //else
                                //{ _retVal = 0; }
                            }
                        }
                        oLineNextAction.Dispose();
                    }
                }
                _isDataSaved = true;
                //_sqlTransaction.Commit();
                _sqlConnection.Close();
            }
            catch (Exception ex)
            {
                _isDataSaved = false;
                //_sqlTransaction.Rollback();
                _sqlConnection.Close();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                //if (_sqlTransaction != null) { _sqlTransaction.Dispose(); }
                if (oLineNextAction != null) { oLineNextAction.Dispose(); }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        public static void SaveEOBNextActionHistory(ref PaymentInsuranceLineNextActions oPaymentInsuranceLineNextActions, Int64 nEOBPaymentID, Int64 nEOBID, SqlConnection sqlConnection,SqlTransaction sqlTransaction)
        {
            SqlConnection _sqlConnection = null;
            SqlTransaction _sqlTransaction = null;
            PaymentInsuranceLineNextAction oLineNextAction = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);    
            try
            {
                _sqlConnection = sqlConnection;
                _sqlTransaction = sqlTransaction;

                if (oPaymentInsuranceLineNextActions != null && oPaymentInsuranceLineNextActions.Count > 0)
                {
                    for (int index = 0; index < oPaymentInsuranceLineNextActions.Count; index++)
                    {
                        //_retVal = null;
                        oLineNextAction = oPaymentInsuranceLineNextActions[index];

                        using (gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters())
                        {
                            oParameters.Add("@nBillingTransactionID", oLineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                            oParameters.Add("@nBillingTransactionDetailID", oLineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nNextActionPatientInsID", oLineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                            oParameters.Add("@nNextActionPartyNumber", oLineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                            oParameters.Add("@nNextActionContactID", oLineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@sNextActionCode", oLineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                            oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nNextPartyType", oLineNextAction.NextPartyType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                            oParameters.Add("@nCloseDate", oLineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@nUserID", oLineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oParameters.Add("@sSubClaimNo", oLineNextAction.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                            oParameters.Add("@nTrackMstTrnID", oLineNextAction.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                            oParameters.Add("@nTrackMstTrnDetailID", oLineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                            oParameters.Add("@nHSTID", 0, ParameterDirection.Output, SqlDbType.BigInt);//	numeric(18, 0),

                            using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                            {
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "BL_EOB_NextAction_Add_History";

                                int _result = _sqlCommand.ExecuteNonQuery();

                                if ((_sqlCommand.Parameters["@nHSTID"] != null) && (_sqlCommand.Parameters["@nHSTID"].Value != null) && (_sqlCommand.Parameters["@nHSTID"].Value != DBNull.Value))
                                { oPaymentInsuranceLineNextActions[index].nHSTID = Convert.ToInt64(_sqlCommand.Parameters["@nHSTID"].Value); }
                            }
                        }
                        oLineNextAction.Dispose();
                    }
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
            finally
            {
                if (oLineNextAction != null) { oLineNextAction.Dispose(); }
                
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }
        }

        public static bool UpdateClaimRemittanceRefNo(ref SplitClaimDetails oSplitClaimDetails, Transaction oTransaction)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            bool _isUpdated = false;

            try
            {
                
                
                oParameters.Clear();
                oParameters.Add("@nTransactionID", oTransaction.TransactionMasterID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                oParameters.Add("@nContactID", oTransaction.ContactID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                oParameters.Add("@nInsuranceID", oTransaction.InsuranceID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                oParameters.Add("@nResponsibilityNo", oTransaction.ResponsibilityNo, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                oParameters.Add("@sClaimRemittanceRefNo", oSplitClaimDetails.ClaimRemittanceReferenceNo, ParameterDirection.Input, SqlDbType.VarChar, 50);// numeric(18,2),
                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),

                //int _retVal = oDB.Execute("BL_UP_ClaimRemittanceRefNo", oParameters);

                int _retVal = 0;
                if (oSplitClaimDetails.UseExtSqlConnection == false)
                {
                    oDB.Connect(false);
                    _retVal = oDB.Execute("BL_INUP_ClaimRemittanceRefNo", oParameters);
                }
                else
                {
                    using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                    {
                        _sqlCommand.Connection = oSplitClaimDetails.ExtSqlConnection;
                        _sqlCommand.Transaction = oSplitClaimDetails.ExtSqlTransaction;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandText = "BL_INUP_ClaimRemittanceRefNo";
                        _retVal = _sqlCommand.ExecuteNonQuery();
                    }
                }

                if (_retVal == 0)
                { _isUpdated = false; }
                else
                { _isUpdated = true; }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                oSplitClaimDetails.ExtTransactionErrorValue = true;
                oSplitClaimDetails.ExtTransactionErrorMsg = ex.ToString();
                throw ex;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return _isUpdated;
        }



        #endregion

        #region " Methods not in use "

        static void UpdateNextParty(PaymentInsuranceLineNextAction oLineNextAction, SqlConnection _sqlConnection, SqlTransaction _sqlTransaction, Int64 nEOBPaymentID, Int64 nEOBID)
        {
            object _retVal = null;
            int _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);

            try
            {

                if (oLineNextAction != null && oLineNextAction.HasData == true)
                {
                    using (gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters())
                    {
                        oParameters.Add("@nID", oLineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                        oParameters.Add("@nClaimNo", oLineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        //oParameters.Add("@nEOBPaymentID", oLineNextAction.EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        //oParameters.Add("@nEOBID", oLineNextAction.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nEOBPaymentID", nEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nEOBID", nEOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
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
                        { oParameters.Add("@dNextActionAmount", oLineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal); }
                        else
                        { oParameters.Add("@dNextActionAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal); }

                        oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 
                        oParameters.Add("@nCloseDate", oLineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nUserID", oLineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@sUserName", oLineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),

                        using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                        {
                            _sqlCommand.Connection = _sqlConnection;
                            _sqlCommand.Transaction = _sqlTransaction;
                            _sqlCommand.CommandType = CommandType.StoredProcedure;
                            _sqlCommand.CommandText = "BL_UP_EOBParty";

                            _result = _sqlCommand.ExecuteNonQuery();

                            if (_sqlCommand.Parameters["@nID"].Value != null)
                            { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                            else
                            { _retVal = 0; }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        static void UpdateNextAction(PaymentInsuranceLineNextAction oLineNextAction, SqlConnection _sqlConnection, SqlTransaction _sqlTransaction, Int64 nEOBPaymentID, Int64 nEOBID)
        {
            object _retVal = null;
            int _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);

            try
            {

                if (oLineNextAction != null && oLineNextAction.HasData == true)
                {
                    using (gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters())
                    {
                        oParameters.Add("@nID", oLineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                        oParameters.Add("@nClaimNo", oLineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        //oParameters.Add("@nEOBPaymentID", oLineNextAction.EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        //oParameters.Add("@nEOBID", oLineNextAction.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nEOBPaymentID", nEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nEOBID", nEOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
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
                        { oParameters.Add("@dNextActionAmount", oLineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal); }
                        else
                        { oParameters.Add("@dNextActionAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal); }

                        oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                        //oParameters.Add("@nTrackTrnID", oLineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                        oParameters.Add("@nTrackTrnID", oLineNextAction.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                        oParameters.Add("@nTrackTrnDtlID", oLineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                        oParameters.Add("@sSubClaimNo", oLineNextAction.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                        oParameters.Add("@nTrackTrnLineNo", 0, ParameterDirection.Input, SqlDbType.Int);// int  

                        oParameters.Add("@nCloseDate", oLineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nUserID", oLineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@sUserName", oLineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),

                        using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                        {
                            _sqlCommand.Connection = _sqlConnection;
                            _sqlCommand.Transaction = _sqlTransaction;
                            _sqlCommand.CommandType = CommandType.StoredProcedure;
                            _sqlCommand.CommandText = "BL_UP_EOBNextAction_Tracking";

                            _result = _sqlCommand.ExecuteNonQuery();

                            if (_sqlCommand.Parameters["@nID"].Value != null)
                            { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                            else
                            { _retVal = 0; }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

        }

        #endregion

        #region " Save Insurance Payment Method for ERA Posting "
        private static void RefreshProgress(ref System.Windows.Forms.ProgressBar oProgress, ref System.Windows.Forms.Label oLabel, String sProgressText)
        {
            oProgress.Increment(1);
            oLabel.Text = sProgressText;
            System.Windows.Forms.Application.DoEvents();
            //oProgress.Refresh();
            //oLabel.Refresh();
            //oProgress.Parent.Refresh();
            //oProgress.Parent.Invalidate();
            //oProgress.Parent.Update();

            //foreach (Form oForm in Application.OpenForms)
            //{
            //    if (oForm.Name == "frmDashBoardMain" || oForm.Name == "frmERAPayment" || oForm.Name == "frmERAFiles")
            //    {
            //        oForm.Invalidate();
            //        oForm.Refresh();
            //        oForm.Update();
            //    }
            //}
        }
        public static Int64 SaveERAEOBPayment(EOBPayment.Common.PaymentInsurance EOBPaymentInsurance, bool IsSaveForVoid, ref System.Windows.Forms.ProgressBar oProgress, ref System.Windows.Forms.Label oLabel, Int64 nBPRID)
        {
            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
            ActivityType.SaveOperationStarts, "Start-Save of ERA check", ActivityOutCome.Success);

            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
            ActivityType.Initialize, "Start-Initialize save variables", ActivityOutCome.Success);

            #region " Variables declaration "

            SqlConnection _sqlConnection = null;
            SqlTransaction _sqlTransaction = null;
            System.Data.SqlClient.SqlCommand _sqlCommand = null;
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            object _retVal = null;
            object _valRet = null;

            Int64 _EOBPayId = 0;//Main ID from Master entry
            Int64 _EOBId = 0; //Each claim EOB ID from EOB Table per claim
            Int64 _EOBDtlId = 0; //Each claim line EOB Detail ID from EOB Table per claim per line
            Int64 _EOBPayDtlId = 0;//Each claim line finance entry from DTL table
            Int64 _EOBVoidPayId = 0;
            Int64 _MainCreditLineID = 0; //The eobpaymentdtlid of the Main Credit Line entry from DTL table


            bool _UseExtEOBID = false;
            EOBPayment.Common.PaymentInsuranceLine PaymentInsClaimLine = null;
            EOBPayment.Common.EOBInsurancePaymentDetail EOBInsPayDtl = null;
            SplitClaimDetails oSplitClaimDetails = null;
            SplitClaimDetails _claimDetails = null;
            SplitClaimLine oSplitLine = null;
            gloSplitClaim ogloSplitClaim = new gloSplitClaim(AppSettings.ConnectionStringPM);
            bool _splitFlag = false;
            int _result = 0;
            Exception _customException = new Exception("Error saving payment");
            PaymentInsuranceLineNextActions _oNextActions = null;

            #endregion

            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
            ActivityType.Initialize, "Finish-Initialize save variables", ActivityOutCome.Success);
gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);    

            try
            {
                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                ActivityType.Initialize, "Start-Initialize database connection & transaction", ActivityOutCome.Success);

                _sqlConnection = new System.Data.SqlClient.SqlConnection(AppSettings.ConnectionStringPM);
                _sqlConnection.Open();
                _sqlTransaction = _sqlConnection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                ActivityType.Initialize, "Finish-Initialize database connection & transaction", ActivityOutCome.Success);

                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                ActivityType.PreSaveValidation, "Start-Payment object read", ActivityOutCome.Success);

                if (EOBPaymentInsurance != null)
                {
                    #region " Master Data Save "

                    //nEOBPaymentID,nEOBRefNO,sPayerName,nPayerID,nPayerType,nPaymentMode,sCheckNumber,nCheckAmount,nCheckDate
                    //nMSTAccountID,nMSTAccountType,nPaymentTrayID,sPaymentTrayName,nCloseDate,sCardType,sCardSecurityNo
                    //nCardID,nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                    ActivityType.SaveMasterDetails, "Start-Save check details (Master Details)", ActivityOutCome.Success);

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
                    oParameters.Add("@bIsERAPayment", true, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0)

                    oParameters.Add("@nBPRID", EOBPaymentInsurance.BPRID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                    //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  add 3 more parameter to save PAF values  while saving
                    oParameters.Add("@nPAccountID", EOBPaymentInsurance.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nAccountPatientID", EOBPaymentInsurance.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nGuarantorID", EOBPaymentInsurance.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                    //End

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

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                    ActivityType.SaveMasterDetails, "Finish-Save Check details (Master Details)", ActivityOutCome.Success);

                    #region "Master Payment Note"


                    if (EOBPaymentInsurance.Notes != null && EOBPaymentInsurance.Notes.Count > 0)
                    {
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                        ActivityType.SaveNotes, "Start-Save payment master note", ActivityOutCome.Success);

                       // Object _RcValue = null;

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
                            oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
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
                        }

                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                        ActivityType.SaveNotes, "Finish-Save payment master note", ActivityOutCome.Success);
                    }


                    #endregion

                    #endregion " Master Data Save "

                    #region "Payment Line Master (Credit) Entry, Total Check Amount Entry, but it is in same table "

                    if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails != null && EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count > 0)
                    {
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                        ActivityType.SaveCreditLine, "Start Iteration - Save payment main credit line entries", ActivityOutCome.Success);

                        for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count; clmInsPayLnIndex++)
                        {
                            if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex] != null)
                            {
                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                ActivityType.SaveCreditLine, "Start - Save payment main credit line entries ( " + clmInsPayLnIndex.ToString() + " ) ", ActivityOutCome.Success);

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
                                oParameters.Add("@bIsERAPayment", true, ParameterDirection.Input, SqlDbType.Bit);
                                //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  add 3 more parameter to save PAF values  while saving
                                oParameters.Add("@nPAccountID", EOBInsPayDtl.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nAccountPatientID", EOBInsPayDtl.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nGuarantorID", EOBInsPayDtl.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                                //End

                                _retVal = null;
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
                                    _MainCreditLineID = _EOBPayDtlId;
                                }

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                ActivityType.AssignCreditLineReferences, "Start-Assign credit line references to financial lines", ActivityOutCome.Success);

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

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                ActivityType.AssignCreditLineReferences, "Finish-Assign credit line references to financial lines", ActivityOutCome.Success);

                                EOBInsPayDtl = null;

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                ActivityType.SaveCreditLine, "Finish - Save payment main credit line entries ( " + clmInsPayLnIndex.ToString() + " ) ", ActivityOutCome.Success);
                            }
                        }

                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                        ActivityType.SaveCreditLine, "Finish Iteration -Save payment main credit line entries", ActivityOutCome.Success);
                    }

                    #endregion

                    #region "Payment Line Master Reserve (Credit) Entry"

                    if (EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail != null && EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail.Count > 0)
                    {
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                        ActivityType.SaveReserveCreditLine, "Start Iteration - Save payment reserve credit line entries", ActivityOutCome.Success);

                        for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail.Count; clmInsPayLnIndex++)
                        {
                            if (EOBPaymentInsurance.EOBInsurancePaymentReserveLineDetail[clmInsPayLnIndex] != null)
                            {
                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                ActivityType.SaveReserveCreditLine, "Start - Save payment reserve credit line entries ( " + clmInsPayLnIndex.ToString() + " ) ", ActivityOutCome.Success);

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

                                //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  add 3 more parameter to save PAF values  while saving
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

                                #region " Add Line Notes "

                                if (EOBInsPayDtl.LineNotes != null && EOBInsPayDtl.LineNotes.Count > 0)
                                {
                                    Object _RcValue = null;

                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                    ActivityType.SaveNotes, "Start - Save payment reserve note ( " + clmInsPayLnIndex.ToString() + " ) ", ActivityOutCome.Success);

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
                                        oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
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
                                    }

                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                    ActivityType.SaveNotes, "Finish - Save payment reserve note ( " + clmInsPayLnIndex.ToString() + " ) ", ActivityOutCome.Success);
                                }

                                #endregion " Add Line Notes "


                                EOBInsPayDtl = null;

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                ActivityType.SaveReserveCreditLine, "Finish - Save payment reserve credit line entries ( " + clmInsPayLnIndex.ToString() + " ) ", ActivityOutCome.Success);
                            }
                        }

                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                        ActivityType.SaveCreditLine, "Finish Iteration -Save payment reserve credit line entries", ActivityOutCome.Success);
                    }
                    #endregion

                    #region " EOB Data Save "

                    if (EOBPaymentInsurance.InsuranceClaims != null && EOBPaymentInsurance.InsuranceClaims.Count > 0)
                    {
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                        ActivityType.SaveEOBDetails, "Start Iteration -Save payment EOB (cliam) entries", ActivityOutCome.Success);

                        #region "Payment EOB and Finance DEbit Line Entries"

                        oProgress.Value = 0;
                        oProgress.Maximum = EOBPaymentInsurance.InsuranceClaims.Count;
                        oProgress.Minimum = 0;
                        oProgress.Visible = true;
                        oLabel.Visible = true;
                        oLabel.BringToFront();
                        for (int clmIndex = 0; clmIndex < EOBPaymentInsurance.InsuranceClaims.Count; clmIndex++)
                        {
                            RefreshProgress(ref oProgress, ref oLabel, "Posting Claim " + EOBPaymentInsurance.InsuranceClaims[clmIndex].ClaimNo.ToString().PadLeft(5, '0') + " ");

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                            ActivityType.SaveEOBDetails, "Start - Save payment EOB (cliam) [CLM#:" + EOBPaymentInsurance.InsuranceClaims[clmIndex].DisplayClaimNo.ToString() + "]", ActivityOutCome.Success);

                            _EOBId = 0;
                            _UseExtEOBID = false;
                            _splitFlag = false;
                            _oNextActions = new PaymentInsuranceLineNextActions();

                            // Create a new instance for split claim logic for each claim in the collection
                            //Set master data of SplitClaim here 
                            oSplitClaimDetails = new SplitClaimDetails();
                            oSplitClaimDetails.TransactionMasterID = EOBPaymentInsurance.InsuranceClaims[clmIndex].BillingTransactionID;
                            oSplitClaimDetails.TransactionID = EOBPaymentInsurance.InsuranceClaims[clmIndex].TrackBillingTrnID;
                            oSplitClaimDetails.ClaimNo = EOBPaymentInsurance.InsuranceClaims[clmIndex].ClaimNo;
                            oSplitClaimDetails.SubClaimNo = EOBPaymentInsurance.InsuranceClaims[clmIndex].SubClaimNo;
                            oSplitClaimDetails.ClinicID = AppSettings.ClinicID;

                            if (_EOBPayId > 0 && EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines != null && EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines.Count > 0)
                            {
                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                ActivityType.SaveEOBLine, "Start Iteration - Save payment EOB Lines (service line) entries [CLM#:" + EOBPaymentInsurance.InsuranceClaims[clmIndex].DisplayClaimNo.ToString() + "]", ActivityOutCome.Success);

                                for (int clmLnIndex = 0; clmLnIndex < EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines.Count; clmLnIndex++)
                                {
                                    if (EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex] != null)
                                    {

                                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                        ActivityType.SaveEOBLine, "Start - Save payment EOB Lines (service line) entries [Line#: " + EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].TrackBLTransactionLineNo.ToString() + "", ActivityOutCome.Success);

                                        _EOBDtlId = 0;
                                        oParameters.Clear();
                                        PaymentInsClaimLine = EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex];

                                        #region "EOB Service Line"

                                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                        ActivityType.SaveEOBLine, "Start - Save EOB Line data for EOB table [Line#: " + EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].TrackBLTransactionLineNo.ToString() + "", ActivityOutCome.Success);

                                        //nEOBPaymentID,nEOBID,nEOBPaymentDetailID,nBillingTransactionID,nBillingTransactionDetailID
                                        //nBillingTransactionLineNo,nPatientID,nDOSFrom,nDOSTo,sCPTCode,sCPTDescription,nAmount,
                                        //nPaymentType,nPaymentSubType,nPaySign,nRefEOBPaymentID,nRefEOBPaymentDetailID,nAccountID
                                        //nAccountType,nMSTAccountID,nMSTAccountType,nPaymentTrayID,nPaymentTrayCode,nPaymentTrayDescription
                                        //nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                                        if (_UseExtEOBID == true) { PaymentInsClaimLine.mEOBID = _EOBId; }
                                        oParameters.Add("@nEOBID", PaymentInsClaimLine.mEOBID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                        oParameters.Add("@nEOBDtlID", PaymentInsClaimLine.mEOBDtlID, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                        oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0)
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
                                        oParameters.Add("@bIsERAPayment", true, ParameterDirection.Input, SqlDbType.Bit);// numeric(18,0)
                                        oParameters.Add("@nSVCId", PaymentInsClaimLine.SVCID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)

                                        oParameters.Add("@nCloseDate", PaymentInsClaimLine.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)

                                        //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  add 3 more parameter to save PAF values  while saving
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
                                        { _EOBId = Convert.ToInt64(_retVal); }

                                        if (_valRet != null && Convert.ToString(_valRet).Trim() != "")
                                        { _EOBDtlId = Convert.ToInt64(_valRet); }

                                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                        ActivityType.SaveEOBLine, "Finish - Save EOB Line data for EOB table [Line#: " + EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].TrackBLTransactionLineNo.ToString() + "", ActivityOutCome.Success);

                                        #region " Update correction credit lines for CPT with nEOBID & nEOBDtlID

                                        if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails != null && EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count > 0)
                                        {
                                            string _sqlQuery = "";
                                            for (int clmInsPayLnIndex = 0; clmInsPayLnIndex < EOBPaymentInsurance.EOBInsurancePaymentLineDetails.Count; clmInsPayLnIndex++)
                                            {
                                                if (EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex] != null
                                                 && EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].PaymentType == EOBPaymentType.InsuracePayment
                                                 && EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].PaymentSubType == EOBPaymentSubType.Correction
                                                 && EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].PaySign == EOBPaymentSign.Payment_Credit
                                                 && EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].BillingTransactionID == PaymentInsClaimLine.BLTransactionID
                                                 && EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].BillingTransactionDetailID == PaymentInsClaimLine.BLTransactionDetailID)
                                                {
                                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                                    ActivityType.SaveEOBLine, "Start - Updating correction credit line with EOBID & EOBDtlID for the current eob correction line [Line#: " + EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].TrackBLTransactionLineNo.ToString() + "", ActivityOutCome.Success);

                                                    _result = 0;
                                                    _sqlQuery = "";

                                                    _sqlQuery = " UPDATE " +
                                                                " BL_EOBPayment_DTL WITH (READPAST) " +
                                                                " SET nEOBID = " + _EOBId + ", " +
                                                                " nEOBDtlID = " + _EOBDtlId + "  " +
                                                                " WHERE nEOBPaymentDetailID = " + EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentDetailID + " " +
                                                                " AND nEOBPaymentID = " + EOBPaymentInsurance.EOBInsurancePaymentLineDetails[clmInsPayLnIndex].EOBPaymentID + " " +
                                                                " AND ISNULL(BL_EOBPayment_DTL.nEOBID,0) = 0  AND ISNULL(BL_EOBPayment_DTL.nEOBDtlID,0) = 0 " +
                                                                " AND (ISNULL(BL_EOBPayment_DTL.nPaymentType,0) = 4  AND ISNULL(BL_EOBPayment_DTL.nPaymentSubType,0) = 13 AND ISNULL(BL_EOBPayment_DTL.nPaySign,0) = 1) ";

                                                    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                                                    _sqlCommand.Connection = _sqlConnection;
                                                    _sqlCommand.Transaction = _sqlTransaction;
                                                    _sqlCommand.CommandType = CommandType.Text;
                                                    _sqlCommand.CommandText = _sqlQuery;
                                                    _result = _sqlCommand.ExecuteNonQuery();

                                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                                     ActivityType.SaveEOBLine, "Finish - Updating correction credit line with EOBID & EOBDtlID for the current eob correction line [Line#: " + EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].TrackBLTransactionLineNo.ToString() + "", ActivityOutCome.Success);
                                                }
                                            }
                                        }


                                        #endregion " Update correction credit lines for CPT with nEOBID & nEOBDtlID

                                        #endregion

                                        #region " New Next Action Party "

                                        PaymentInsuranceLineNextAction oLineNextAction = null;
                                        oLineNextAction = EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].LineNextAction;

                                        if (oLineNextAction != null)
                                        {
                                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                            ActivityType.SaveNextAction, "Start - Update NextAction & Party current eob line [Line#: " + EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].TrackBLTransactionLineNo.ToString() + "", ActivityOutCome.Success);

                                            oParameters = new gloDatabaseLayer.DBParameters();
                                            oParameters.Add("@nEOBID", _EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nEOBPaymentDetailID", oLineNextAction.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),

                                            oParameters.Add("@nNextActionPatientInsID", oLineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                                            oParameters.Add("@nNextActionPatientInsName", oLineNextAction.NextActionPatientInsName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                            oParameters.Add("@nNextActionPartyNumber", oLineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                                            oParameters.Add("@nNextActionContactID", oLineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nNextPartyType", oLineNextAction.NextPartyType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,

                                            oParameters.Add("@sNextActionCode", oLineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                            oParameters.Add("@sNextActionDescription", oLineNextAction.NextActionDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),

                                            if (oLineNextAction.IsNullNextActionAmount == false)
                                            { oParameters.Add("@dNextActionAmount", oLineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal); }
                                            else
                                            { oParameters.Add("@dNextActionAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal); }

                                            //oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                            oParameters.Add("@nCloseDate", oLineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nUserID", oLineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@sUserName", oLineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),
                                            oParameters.Add("@sSubClaimNo", oLineNextAction.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),

                                            oParameters.Add("@nTrackMstTrnID", oLineNextAction.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                            oParameters.Add("@nTrackMstTrnDetailID", oLineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                            oParameters.Add("@nTrackTrnLineNo", 0, ParameterDirection.Input, SqlDbType.Int);// int  

                                            oParameters.Add("@nBillingTransactionID", oLineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                            oParameters.Add("@nBillingTransactionDetailID", oLineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                            oParameters.Add("@nHSTID", 0, ParameterDirection.Output, SqlDbType.BigInt);//	numeric(18, 0),

                                            _sqlCommand = new SqlCommand();
                                            _sqlCommand = oDB.GetCmdParameters(oParameters);
                                            _sqlCommand.Connection = _sqlConnection;
                                            _sqlCommand.Transaction = _sqlTransaction;
                                            _sqlCommand.CommandType = CommandType.StoredProcedure;
                                            _sqlCommand.CommandText = "BL_UP_EOBNextAction_Revised";

                                            _result = _sqlCommand.ExecuteNonQuery();

                                            //History id needed for split claim
                                            if ((_sqlCommand.Parameters["@nHSTID"] != null) && (_sqlCommand.Parameters["@nHSTID"].Value != null) && (_sqlCommand.Parameters["@nHSTID"].Value != DBNull.Value))
                                            { EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].LineNextAction.nHSTID = Convert.ToInt64(_sqlCommand.Parameters["@nHSTID"].Value); }

                                            #region " Set the split line data "

                                            oSplitLine = new SplitClaimLine();
                                            oSplitLine.TransactionDetailID = oLineNextAction.TrackBillingTransactionDetailID;
                                            oSplitLine.TransactionMasterDetailID = oLineNextAction.BillingTransactionDetailID;
                                            oSplitLine.NextActionCode = oLineNextAction.NextActionCode;
                                            oSplitLine.InsuranceId = oLineNextAction.NextActionPatientInsID;
                                            oSplitLine.ContactID = oLineNextAction.NextActionContactID;
                                            oSplitLine.ResponsibilityNo = oLineNextAction.NextActionPartyNumber;
                                            oSplitLine.Party = oLineNextAction.NextActionPatientInsName;

                                            oSplitClaimDetails.Lines.Add(oSplitLine);
                                            oSplitLine = null;

                                            #endregion " Set the split line data "

                                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                            ActivityType.SaveNextAction, "Finish - Update NextAction & Party current eob line [Line#: " + EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].TrackBLTransactionLineNo.ToString() + "", ActivityOutCome.Success);

                                            _oNextActions.Add(oLineNextAction);
                                        }

                                        if (oLineNextAction != null) { oLineNextAction.Dispose(); }

                                        #endregion

                                        #region " Add Line Reason Codes "

                                        if (PaymentInsClaimLine.LineResonCodes != null && PaymentInsClaimLine.LineResonCodes.Count > 0)
                                        {
                                            //Object _RcValue = null;

                                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                            ActivityType.SaveReasonCodes, "Start Iteration - Save Reasoncode [Line#: " + EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].TrackBLTransactionLineNo.ToString() + "", ActivityOutCome.Success);

                                            for (int rcInd = 0; rcInd < PaymentInsClaimLine.LineResonCodes.Count; rcInd++)
                                            {
                                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                                ActivityType.SaveReasonCodes, "Start - Save Reasoncode [Line#: " + EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].TrackBLTransactionLineNo.ToString() + "", ActivityOutCome.Success);

                                             //   _RcValue = null;
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
                                                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
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

                                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                                ActivityType.SaveReasonCodes, "Finish - Save Reasoncode [Line#: " + EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].TrackBLTransactionLineNo.ToString() + "", ActivityOutCome.Success);
                                            }

                                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                            ActivityType.SaveReasonCodes, "Finish Iteration - Save Reasoncode [Line#: " + EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].TrackBLTransactionLineNo.ToString() + "", ActivityOutCome.Success);
                                        }

                                        #endregion " Add Line Reason Codes "

                                        #region " Add Line Notes "

                                        if (PaymentInsClaimLine.LineNotes != null && PaymentInsClaimLine.LineNotes.Count > 0)
                                        {
                                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                            ActivityType.SaveNotes, "Start Iteration - Save line notes [Line#: " + EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].TrackBLTransactionLineNo.ToString() + "", ActivityOutCome.Success);

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
                                                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
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
                                            }

                                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                            ActivityType.SaveNotes, "Finish Iteration - Save line notes [Line#: " + EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].TrackBLTransactionLineNo.ToString() + "", ActivityOutCome.Success);
                                        }

                                        #endregion " Add Line Notes "

                                        #region " EOB Financial Service Line Save "


                                        if (_EOBPayId > 0 && PaymentInsClaimLine.EOBInsurancePaymentLineDetails != null && PaymentInsClaimLine.EOBInsurancePaymentLineDetails.Count > 0)
                                        {
                                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                        ActivityType.SaveEOBLine, "Start Iteration - Save EOB financial Lines (service line) entries [CLM#:" + EOBPaymentInsurance.InsuranceClaims[clmIndex].DisplayClaimNo.ToString() + "]", ActivityOutCome.Success);

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

                                                    //Added by Subashish_b on 13/Jan /2011 (integration made on date-10/May/2011) for  add 3 more parameter to save PAF values  while saving
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

                                                }
                                            }

                                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                        ActivityType.SaveEOBLine, "Finish Iteration - Save EOB financial Lines (service line) entries [CLM#:" + EOBPaymentInsurance.InsuranceClaims[clmIndex].DisplayClaimNo.ToString() + "]", ActivityOutCome.Success);
                                        }


                                        #endregion " EOB Financial Service Line Save "

                                        PaymentInsClaimLine = null;

                                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                        ActivityType.SaveEOBLine, "Finish - Save payment EOB Lines (service line) entries [Line#: " + EOBPaymentInsurance.InsuranceClaims[clmIndex].CliamLines[clmLnIndex].TrackBLTransactionLineNo.ToString() + "", ActivityOutCome.Success);

                                    }
                                }

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                ActivityType.SaveEOBLine, "Finish Iteration -Save payment EOB Lines (service line) entries [CLM#:" + EOBPaymentInsurance.InsuranceClaims[clmIndex].DisplayClaimNo.ToString() + "] ", ActivityOutCome.Success);
                            }

                            oSplitClaimDetails.EOBPaymentID = _EOBPayId;
                            oSplitClaimDetails.EOBID = _EOBId;

                            //**********************************************************************
                            //Hold logic to be written
                            DataTable _dtHoldInfo = new DataTable();
                            DataRow _drParentClaimHoldNote = null;
                            _claimDetails = new SplitClaimDetails(EOBPaymentInsurance.InsuranceClaims[clmIndex].ClaimNo, EOBPaymentInsurance.InsuranceClaims[clmIndex].SubClaimNo);
                            if (_claimDetails.IsClaimOnHold)
                            {
                                _drParentClaimHoldNote = InsurancePayment.GetBillingHoldNote(oSplitClaimDetails.TransactionMasterID, oSplitClaimDetails.TransactionID);
                                _dtHoldInfo = ogloSplitClaim.GetSubClaims(oSplitClaimDetails);
                                if (_dtHoldInfo != null)
                                {
                                    if (_dtHoldInfo.Rows.Count > 1)
                                    {
                                        //...When claims will be splitted then need to implement logic here
                                    }
                                }
                            }

                            //**********************************************************************

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                        ActivityType.SaveEOBLine, "Start - Split Claim Claim#:" + oSplitClaimDetails.ClaimDisplayNo + " ", ActivityOutCome.Success);

                            //Call split function to proceed for current claim
                            oSplitClaimDetails.UseExtSqlConnection = true;
                            oSplitClaimDetails.ExtSqlConnection = _sqlConnection;
                            oSplitClaimDetails.ExtSqlTransaction = _sqlTransaction;

                            _splitFlag = ogloSplitClaim.SplitTransactionClaim_ERA(ref oSplitClaimDetails, _dtHoldInfo);


                            if (oSplitClaimDetails.ExtTransactionErrorValue == true)
                            { throw _customException; }


                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                           ActivityType.SaveEOBLine, "Finish - Split Claim Claim#:" + oSplitClaimDetails.ClaimDisplayNo + " ", ActivityOutCome.Success);


                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                        ActivityType.SaveNextAction, "Start - Save Next Action History Claim#:" + oSplitClaimDetails.ClaimDisplayNo + " ", ActivityOutCome.Success);

                            //Call NextAction History after spilt
                           // bool isEOBSaved = false;
                            InsurancePayment.SaveEOBNextActionHistory(ref _oNextActions, _EOBPayId, _EOBId, _sqlConnection, _sqlTransaction);
                            _oNextActions = null;

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                                        ActivityType.SaveNextAction, "Finish - Save Next Action History Claim#:" + oSplitClaimDetails.ClaimDisplayNo + " ", ActivityOutCome.Success);

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                            ActivityType.SaveEOBDetails, "Finish - Save payment EOB (cliam) [CLM#:" + EOBPaymentInsurance.InsuranceClaims[clmIndex].DisplayClaimNo.ToString() + "]", ActivityOutCome.Success);

                            if (oSplitClaimDetails != null) { oSplitClaimDetails = null; }

                        }

                        #endregion

                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                        ActivityType.SaveEOBDetails, "Finish Iteration -Save payment EOB (cliam) entries", ActivityOutCome.Success);
                    }

                    #endregion " EOB Data Save "

                    #region " Save last selected Close date "

                    gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
                    oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(gloDateMaster.gloDate.DateAsDate(EOBPaymentInsurance.CloseDate)).ToString("MM/dd/yyyy"), AppSettings.ClinicID, EOBPaymentInsurance.UserID, gloSettings.SettingFlag.User);
                    oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", EOBPaymentInsurance.PaymentTrayID.ToString(), AppSettings.ClinicID, EOBPaymentInsurance.UserID, gloSettings.SettingFlag.User);
                    oSettings.Dispose();

                    #endregion " Save last selected Close date "

                    _sqlTransaction.Commit();

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                    ActivityType.SaveOperationEnds, "Finish-Payment object read", ActivityOutCome.Success);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                    ActivityType.PreSaveValidation, "Finish-Payment object read (null object found).", ActivityOutCome.Failure);
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                _sqlTransaction.Rollback();
                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                    ActivityType.SaveOperationAborted, "Abort - Save Payment", ActivityOutCome.Failure);
                _EOBPayId = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _sqlTransaction.Rollback();
                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Billing, ActivityCategory.ERAInsurancePaymentSave,
                    ActivityType.SaveOperationAborted, "Abort - Save Payment", ActivityOutCome.Failure);
                _EOBPayId = 0;
            }
            finally
            {
                if (_sqlCommand != null) { _sqlCommand.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_retVal != null) { _retVal = null; }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                UnlockCheckClaims(nBPRID);
            }
            return _EOBPayId;
        }

        public static void UnlockCheckClaims(Int64 nBPRID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oDBPara = new gloDatabaseLayer.DBParameters();
             
            try
            {
                if (nBPRID == 0)
                    return;
                 oDB.Connect(false);
                //string _Query = "UPDATE BL_Transaction_MST with(readpast) SET bIsOpened = " + 0 + ", sMachineID = '' " +
                //        " WHERE nClaimNo IN (SELECT CONVERT(NUMERIC(18,0),sCLP01_ClaimSubmitterID) FROM ERA_CLP WHERE nBPRID = " + nBPRID.ToString() + ")" +
                //        " AND ISNULL(bIsOpened,0) = 1 AND nClinicID = " + AppSettings.ClinicID;
                oDBPara.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPara.Add("@MachineName", Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Execute("ERA_UnlockClaims", oDBPara);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBPara != null)
                {
                    oDBPara.Dispose();
                    oDBPara = null;
                }

            }

        }

        public static void UpdateDenialQueue(Int64 nUserID, String sUser, Int64 nClinicID, Int64 nBPRID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oDBPara = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oDBPara.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPara.Add("@sUser", sUser, ParameterDirection.Input, SqlDbType.Text);
                oDBPara.Add("@nClinicID", nClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPara.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("ERA_UpdateDenialQueue", oDBPara);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBPara != null)
                {
                    oDBPara.Dispose();
                    oDBPara = null;
                }

            }

        }
        #endregion
    }
}
