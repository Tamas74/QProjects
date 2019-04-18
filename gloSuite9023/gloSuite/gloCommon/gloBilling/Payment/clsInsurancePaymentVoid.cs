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

namespace gloBilling.Payment
{
    partial class InsurancePayment
    {
        public static decimal GetTotalInsuranceReservesUsed(Int64 EOBPaymentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            object _retVal = null;
            decimal _reserveAmount = 0;

            try
            {
                _sqlQuery = "select sum(UsedAmount) UsedReserve  from view_InsuranceReserveUsed where ISNULL(VoidType,0) <> 3 and nEOBPaymentID = '" + EOBPaymentID + "'";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                {
                    _reserveAmount = Convert.ToDecimal(_retVal);
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
            return _reserveAmount;
        }

        public static DataTable GetInsuranceReservesUsed(Int64 EOBPaymentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            DataTable _dtReserves = new DataTable();

            try
            {
                string _sqlQuery = "select * from view_InsuranceReserveUsed where nEOBPaymentID = '" + EOBPaymentID + "'";

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

        public static decimal GetTotalInsuranceReservesAvailable(Int64 EOBPaymentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            object _retVal = null;
            decimal _amountToReserve = 0;

            try
            {
                _sqlQuery = "select sum(PrevPaid) AmtToReserve  from view_InsuranceToReserves where nEOBPaymentID = '" + EOBPaymentID + "'";

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

        public static DataTable GetInsuranceReservesAvailable(Int64 EOBPaymentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            DataTable _dtReserves = new DataTable();

            try
            {
                string _sqlQuery = "select * from view_InsuranceToReserves where nEOBPaymentID = '" + EOBPaymentID + "'";

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

        public static bool HasNotes(Int64 nEOBPaymentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            object _retVal = null;
            bool _hasNotes = false;

            try
            {
                _sqlQuery = " SELECT COUNT(NID) NotesCount FROM BL_EOB_NOTES WITH (NOLOCK) WHERE nEOBPaymentID = '" + nEOBPaymentID +"'" +
                            " and nPaymentNoteType = 4 and nPaymentNoteSubType = 16 ";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                { _hasNotes = true; }
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
            return _hasNotes;
        }

        public static DataRow GetInsurancePaymentLogDetails(Int64 EOBPaymentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);

            DataTable _dtPaymentLog = new DataTable();
            DataRow _drPaymentLog = null;

            try
            {
                string _sqlQuery = "SELECT * FROM view_InsurancePaymentLog where nEOBPaymentID = " + EOBPaymentID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtPaymentLog);
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

        //public static DataTable GetInsurancePaymentLog(Int64 InsuranceCompanyID, Int64 PaymentTrayID, Int64 UserID, string CheckNumber, Int64 PaymentDate, Int64 CloseDate)
        public static DataTable GetInsurancePaymentLog(Int64 InsuranceCompanyID, string PaymentTrayIDs, Int64 UserID, string CheckNumber, Int64 PaymentDate, Int64 CloseDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            DataTable _dtPaymentLog = new DataTable();

            try
            {
                string _sqlQuery = "SELECT *,CONVERT(datetime,closedate) as orderdate FROM view_InsurancePaymentLog where nClinicID = " + AppSettings.ClinicID;

                if (InsuranceCompanyID != 0)
                { _sqlQuery += " AND nPayerID = " + InsuranceCompanyID; }

                //if (PaymentTrayID != 0)
                //{ _sqlQuery += " AND nPaymentTrayID = " + PaymentTrayID; }

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

                _sqlQuery += " order by orderdate ";

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

        public static Int64 GetLastUnclosedDate()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
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

        public static bool IsCheckOverAllocated(Int64 nEOBPaymentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            bool _isOverAllocated = false;

            DataTable _dtEOBPayment = new DataTable();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nEOBPaymentID", nEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_OVER_ALLOCATED_CHECK", oParameters, out _dtEOBPayment);
                oDB.Disconnect();

                if (_dtEOBPayment != null && _dtEOBPayment.Rows.Count > 0)
                {  _isOverAllocated = true; }
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

        #region "Void Insurance Payment"

        public Int64 VoidInsurancePayment(Int64 EOBPaymentID, Int64 PatientId, string PatientName, string CloseDate, string VoidNote, int VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName, Int64 VoidUserID, string VoidUserName)
        {
            System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(AppSettings.ConnectionStringPM);
            System.Data.SqlClient.SqlCommand _sqlCommand = null;
            System.Data.SqlClient.SqlTransaction _sqlTransaction = null;
            EOBPayment.Common.EOBInsurancePaymentDetails EOBInsurancePaymentDtls = new EOBPayment.Common.EOBInsurancePaymentDetails();
            EOBPayment.Common.EOBInsurancePaymentDetail EOBInsPayDtl = null;
            EOBPayment.Common.PaymentInsurance EOBPaymentInsurance = new global::gloBilling.EOBPayment.Common.PaymentInsurance();
            EOBPayment.Common.PaymentInsuranceLines PaymentInsuranceEOBLines = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLines();
            EOBPayment.Common.PaymentInsuranceLine PaymentInsuranceEOBLine = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            object _retVal = null;
            object _valRet = null;
            Int64 _EOBPayId = 0;
            Int64 _EOBPayDtlId = 0;
            Int64 EOBId = 0;
            Int64 EOBDtlId = 0;
            Int64 _currentEOBId = 0;
            string _sqlQuery = "";
            try
            {
                if (EOBInsurancePaymentDtls != null)
                {
                    _sqlConnection.Open();
                    _sqlTransaction = _sqlConnection.BeginTransaction();

                    EOBPaymentInsurance = GetMasterDetailsForInsurancePaymentVoid(EOBPaymentID, AppSettings.ClinicID, VoidCloseDate, VoidTrayID, VoidTrayCode, VoidTrayName, VoidUserID, VoidUserName);
                    #region " Master Data Save "
                    if (EOBPaymentInsurance != null)
                    {
                        //nEOBPaymentID,nEOBRefNO,sPayerName,nPayerID,nPayerType,nPaymentMode,sCheckNumber,nCheckAmount,nCheckDate
                        //nMSTAccountID,nMSTAccountType,nPaymentTrayID,sPaymentTrayName,nCloseDate,sCardType,sCardSecurityNo
                        //nCardID,nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                        oParameters.Clear();

                        oParameters.Add("@sPaymentNo", EOBPaymentInsurance.PaymentNumber, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sPaymentNoPrefix", EOBPaymentInsurance.PaymentNumberPefix, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
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

                        oParameters.Add("@bIsPaymentVoid", true, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nPaymentVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nPaymentVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nVoidType", VoidType.InsurancePaymentVoidEntry, ParameterDirection.Input, SqlDbType.Int);
                        oParameters.Add("@nVoidRefPaymentID", EOBPaymentID,ParameterDirection.Input,SqlDbType.BigInt);
                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandText = "BL_INUP_EOBPayment_MST_InsVoid";

                        int _result = 0;
                        _result = _sqlCommand.ExecuteNonQuery();

                        if (_sqlCommand.Parameters["@nEOBPaymentID"].Value != null)
                        { _retVal = _sqlCommand.Parameters["@nEOBPaymentID"].Value; }
                        else
                        { _retVal = 0; }

                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                        { _EOBPayId = Convert.ToInt64(_retVal); }

                    }
                    #endregion "Master Data Save"

                    PaymentInsuranceEOBLines = GetEOBLinesDetailForInsurancePaymentVoid(EOBPaymentID, PatientId, VoidCloseDate, VoidTrayID, VoidTrayCode, VoidTrayName, VoidUserID, VoidUserName);

                    #region "EOB Data save for voiding Insurance payment "

                    if (EOBPaymentID > 0 && PaymentInsuranceEOBLines != null && PaymentInsuranceEOBLines.Count > 0)
                    {
                        for (int _payVoidEOBLineIndex = 0; _payVoidEOBLineIndex < PaymentInsuranceEOBLines.Count; _payVoidEOBLineIndex++)
                        {
                            if (PaymentInsuranceEOBLines[_payVoidEOBLineIndex] != null)
                            {
                                PaymentInsuranceEOBLine = PaymentInsuranceEOBLines[_payVoidEOBLineIndex];

                                if (PaymentInsuranceEOBLines[_payVoidEOBLineIndex].mEOBID != _currentEOBId)
                                {
                                    EOBId = 0;

                                }
                                #region "EOB Lines Save "

                                oParameters.Clear();
                                oParameters.Add("@nEOBID", EOBId, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                oParameters.Add("@nEOBDtlID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0)
                                //oParameters.Add("@nClaimNo", PaymentInsuranceEOBLine.ClaimNumber, ParameterDirection.Input, SqlDbType.Int);//	int
                                oParameters.Add("@nClaimNo", PaymentInsuranceEOBLine.ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nDOSFrom", PaymentInsuranceEOBLine.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                oParameters.Add("@nDOSTo", PaymentInsuranceEOBLine.DOSTo, ParameterDirection.Input, SqlDbType.BigInt);//	int
                                oParameters.Add("@sCPTCode", PaymentInsuranceEOBLine.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                oParameters.Add("@sCPTDescription", PaymentInsuranceEOBLine.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                if (PaymentInsuranceEOBLine.IsNullCharges == false)
                                {
                                    oParameters.Add("@dCharges", PaymentInsuranceEOBLine.Charges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }
                                else
                                {
                                    oParameters.Add("@dCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }

                                if (PaymentInsuranceEOBLine.IsNullUnit == false)
                                {
                                    oParameters.Add("@dUnit", PaymentInsuranceEOBLine.Unit, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                }
                                else
                                {
                                    oParameters.Add("@dUnit", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                                }

                                if (PaymentInsuranceEOBLine.IsNullTotalCharges == false)
                                {
                                    oParameters.Add("@dTotalCharges", PaymentInsuranceEOBLine.TotalCharges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }
                                else
                                {
                                    oParameters.Add("@dTotalCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }

                                if (PaymentInsuranceEOBLine.IsNullAllowed == false)
                                {
                                    oParameters.Add("@dAllowed", PaymentInsuranceEOBLine.Allowed, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }
                                else
                                {
                                    oParameters.Add("@dAllowed", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }

                                if (PaymentInsuranceEOBLine.IsNullWriteOff == false)
                                {
                                    oParameters.Add("@dWriteOff", PaymentInsuranceEOBLine.WriteOff, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }
                                else
                                {
                                    oParameters.Add("@dWriteOff", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }

                                if (PaymentInsuranceEOBLine.IsNullNonCovered == false)
                                {
                                    oParameters.Add("@dNotCovered", PaymentInsuranceEOBLine.NonCovered, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }
                                else
                                {
                                    oParameters.Add("@dNotCovered", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }

                                if (PaymentInsuranceEOBLine.IsNullInsurance == false)
                                {
                                    oParameters.Add("@dPayment", PaymentInsuranceEOBLine.InsuranceAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }
                                else
                                {
                                    oParameters.Add("@dPayment", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }

                                if (PaymentInsuranceEOBLine.IsNullCopay == false)
                                {
                                    oParameters.Add("@dCopay", PaymentInsuranceEOBLine.Copay, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }
                                else
                                {
                                    oParameters.Add("@dCopay", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }

                                if (PaymentInsuranceEOBLine.IsNullDeductible == false)
                                {
                                    oParameters.Add("@dDeductible", PaymentInsuranceEOBLine.Deductible, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }
                                else
                                {
                                    oParameters.Add("@dDeductible", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }

                                if (PaymentInsuranceEOBLine.IsNullCoInsurance == false)
                                {
                                    oParameters.Add("@dCoInsurance", PaymentInsuranceEOBLine.CoInsurance, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                }
                                else
                                {
                                    oParameters.Add("@dCoInsurance", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                                }

                                if (PaymentInsuranceEOBLine.IsNullWithhold == false)
                                {
                                    oParameters.Add("@dWithhold", PaymentInsuranceEOBLine.Withhold, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }
                                else
                                {
                                    oParameters.Add("@dWithhold", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }

                                oParameters.Add("@nPaymentTrayID", PaymentInsuranceEOBLine.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                oParameters.Add("@sPaymentTrayCode", PaymentInsuranceEOBLine.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                                oParameters.Add("@sPaymentTrayDescription", PaymentInsuranceEOBLine.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked
                                oParameters.Add("@nUserID", AppSettings.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                                oParameters.Add("@sUserName", AppSettings.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked
                                oParameters.Add("@dtCreatedDateTime", PaymentInsuranceEOBLine.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked
                                oParameters.Add("@dtModifiedDateTime", PaymentInsuranceEOBLine.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked
                                oParameters.Add("@nPatientID", PaymentInsuranceEOBLine.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                oParameters.Add("@nInsuraceID", PaymentInsuranceEOBLine.PatInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                oParameters.Add("@nContactID", PaymentInsuranceEOBLine.InsContactID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                                oParameters.Add("@nClinicID", PaymentInsuranceEOBLine.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nEOBType", PaymentInsuranceEOBLine.EOBType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);// int,
                                oParameters.Add("@nBillingTransactionID", PaymentInsuranceEOBLine.BLTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                oParameters.Add("@nBillingTransactionDetailID", PaymentInsuranceEOBLine.BLTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                oParameters.Add("@nBillingTransactionLineNo", PaymentInsuranceEOBLine.BLTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
                                oParameters.Add("@bUseExtEOBID", EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nCloseDate", PaymentInsuranceEOBLine.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                oParameters.Add("@nVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),

                                oParameters.Add("@nTrackTrnID", PaymentInsuranceEOBLine.TrackBLTransactionID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nTrackTrnDtlID", PaymentInsuranceEOBLine.TrackBLTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nInsuranceCompanyID", PaymentInsuranceEOBLine.InsCompanyID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nTrackTrnLineNo", PaymentInsuranceEOBLine.TrackBLTransactionLineNo, ParameterDirection.Input, SqlDbType.Int);  // numeric(18,0),
                                oParameters.Add("@sSubClaimNo", PaymentInsuranceEOBLine.SubClaimNumber, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),


                                oParameters.Add("@bIsPaymentVoid", true, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                oParameters.Add("@nPaymentVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                oParameters.Add("@nPaymentVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                oParameters.Add("@nVoidType", VoidType.InsurancePaymentVoidEntry.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                                oParameters.Add("@nVoidRefEOBID", PaymentInsuranceEOBLine.mEOBID, ParameterDirection.Input, SqlDbType.BigInt); //numeric(18, 0),            
                                oParameters.Add("@nVoidRefEOBDtlID", PaymentInsuranceEOBLine.mEOBDtlID, ParameterDirection.Input, SqlDbType.BigInt); //numeric(18,0),            
                                oParameters.Add("@nVoidRefEOBPaymentID", PaymentInsuranceEOBLine.mEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0)

                                _retVal = null;
                                _valRet = null;

                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "BL_INSERT_EOBPayment_EOB_InsVoid";

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
                                { EOBId = Convert.ToInt64(_retVal); _currentEOBId = PaymentInsuranceEOBLine.mEOBID; }

                                if (_valRet != null && Convert.ToString(_valRet).Trim() != "")
                                { EOBDtlId = Convert.ToInt64(_valRet); }

                                #endregion "EOB Lines Save "

                                EOBInsurancePaymentDtls = GetInsurancePaymentForVoid(EOBPaymentID, PaymentInsuranceEOBLine.mEOBID, PaymentInsuranceEOBLine.mEOBDtlID, PatientId, VoidCloseDate, VoidTrayID, VoidTrayCode, VoidTrayName, VoidUserID, VoidUserName,false,false,false);

                                #region " Payment Line Details save for voiding Insurance payment "

                                if (EOBPaymentID > 0 && EOBInsurancePaymentDtls != null && EOBInsurancePaymentDtls.Count > 0)
                                {
                                    for (int _payVoidLineIndex = 0; _payVoidLineIndex < EOBInsurancePaymentDtls.Count; _payVoidLineIndex++)
                                    {
                                        if (EOBInsurancePaymentDtls[_payVoidLineIndex] != null)
                                        {

                                            EOBInsPayDtl = EOBInsurancePaymentDtls[_payVoidLineIndex];
                                            oParameters.Clear();
                                            oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nEOBID", EOBId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nEOBDtlID", EOBDtlId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nEOBPaymentDetailID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nBillingTransactionLineNo", EOBInsPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
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
                                            oParameters.Add("@nUserID", AppSettings.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@sUserName", AppSettings.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                            oParameters.Add("@dtCreatedDateTime", EOBInsPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                            oParameters.Add("@dtModifiedDateTime", EOBInsPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                            oParameters.Add("@nClinicID", EOBInsPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                            if (EOBInsPayDtl.RefEOBPaymentID == 0) { EOBInsPayDtl.RefEOBPaymentID = 0; }
                                            if (EOBInsPayDtl.RefEOBPaymentDetailID == 0) { EOBInsPayDtl.RefEOBPaymentDetailID = 0; }

                                            oParameters.Add("@nRefEOBPaymentID", EOBInsPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                            oParameters.Add("@nResEOBPaymentID", EOBInsPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                            oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                                            oParameters.Add("@nContactInsID", EOBInsPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                            oParameters.Add("@nCreditLineID", EOBInsPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                            oParameters.Add("@nEOBVoidPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                            oParameters.Add("@nCloseDate", EOBInsPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                            oParameters.Add("@nOldRefEOBPaymentID", EOBInsPayDtl.OldRefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                            oParameters.Add("@nOldRefEOBPaymentDetailID", EOBInsPayDtl.OldRefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                            oParameters.Add("@nOldResEOBPaymentID", EOBInsPayDtl.OldReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                            oParameters.Add("@nOldResEOBPaymentDetailID", EOBInsPayDtl.OldReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                            oParameters.Add("@nTrackTrnID", EOBInsPayDtl.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                            oParameters.Add("@nTrackTrnDtlID", EOBInsPayDtl.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                            oParameters.Add("@nTrackTrnLineNo", EOBInsPayDtl.TrackBillingTransactionLineNo, ParameterDirection.Input, SqlDbType.Int);  // numeric(18,0),
                                            oParameters.Add("@sSubClaimNo", EOBInsPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // VarChar
                                            

                                            oParameters.Add("@nVoidType", EOBInsPayDtl.VoidType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                            oParameters.Add("@bIsPaymentVoid", true, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                            oParameters.Add("@nPaymentVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                            oParameters.Add("@nPaymentVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),


                                            _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                            _sqlCommand = oDB.GetCmdParameters(oParameters);
                                            _sqlCommand.Connection = _sqlConnection;
                                            _sqlCommand.Transaction = _sqlTransaction;
                                            _sqlCommand.CommandType = CommandType.StoredProcedure;
                                            _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL_InsVoid";

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
                                #endregion " Payment Line Details save for voiding Insurance payment "

                                EOBDtlId = 0;
                            }
                        }
                    }

                    #endregion " EOB Data save for voiding Insurance payment

                    #region " For Reserve Credit entries "

                    EOBInsurancePaymentDtls = GetInsurancePaymentForVoid(EOBPaymentID, 0, 0, PatientId, VoidCloseDate, VoidTrayID, VoidTrayCode, VoidTrayName, VoidUserID, VoidUserName,true,false,false);
                    
                    #region " Payment Line Details save for voiding Insurance payment "

                    if (EOBPaymentID > 0 && EOBInsurancePaymentDtls != null && EOBInsurancePaymentDtls.Count > 0)
                    {
                        for (int _payVoidLineIndex = 0; _payVoidLineIndex < EOBInsurancePaymentDtls.Count; _payVoidLineIndex++)
                        {
                            if (EOBInsurancePaymentDtls[_payVoidLineIndex] != null)
                            {

                                EOBInsPayDtl = EOBInsurancePaymentDtls[_payVoidLineIndex];
                                oParameters.Clear();
                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nEOBID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nEOBDtlID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nEOBPaymentDetailID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nBillingTransactionLineNo", EOBInsPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
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
                                oParameters.Add("@nUserID", AppSettings.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@sUserName", AppSettings.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                oParameters.Add("@dtCreatedDateTime", EOBInsPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                oParameters.Add("@dtModifiedDateTime", EOBInsPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                oParameters.Add("@nClinicID", EOBInsPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                if (EOBInsPayDtl.RefEOBPaymentID == 0) { EOBInsPayDtl.RefEOBPaymentID = 0; }
                                if (EOBInsPayDtl.RefEOBPaymentDetailID == 0) { EOBInsPayDtl.RefEOBPaymentDetailID = 0; }

                                oParameters.Add("@nRefEOBPaymentID", EOBInsPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nResEOBPaymentID", EOBInsPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nContactInsID", EOBInsPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nCreditLineID", EOBInsPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nEOBVoidPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nCloseDate", EOBInsPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                oParameters.Add("@nOldRefEOBPaymentID", EOBInsPayDtl.OldRefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nOldRefEOBPaymentDetailID", EOBInsPayDtl.OldRefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nOldResEOBPaymentID", EOBInsPayDtl.OldReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nOldResEOBPaymentDetailID", EOBInsPayDtl.OldReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                oParameters.Add("@nTrackTrnID", EOBInsPayDtl.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nTrackTrnDtlID", EOBInsPayDtl.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nTrackTrnLineNo", EOBInsPayDtl.TrackBillingTransactionLineNo, ParameterDirection.Input, SqlDbType.Int);  // numeric(18,0),
                                oParameters.Add("@sSubClaimNo", EOBInsPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // VarChar

                                oParameters.Add("@nVoidType", EOBInsPayDtl.VoidType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oParameters.Add("@bIsPaymentVoid", true, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                oParameters.Add("@nPaymentVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                oParameters.Add("@nPaymentVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),


                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL_InsVoid";

                                int _result = _sqlCommand.ExecuteNonQuery();

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
                    #endregion " Payment Line Details save for voiding Insurance payment "

                    #endregion " For Reserve Credit entries "

                    #region " For Correction Credit entries "

                    EOBInsurancePaymentDtls = GetInsurancePaymentForVoid(EOBPaymentID, 0, 0, PatientId, VoidCloseDate, VoidTrayID, VoidTrayCode, VoidTrayName, VoidUserID, VoidUserName, false, true, false);

                    #region " Payment Line Details save for voiding Insurance payment "

                    if (EOBPaymentID > 0 && EOBInsurancePaymentDtls != null && EOBInsurancePaymentDtls.Count > 0)
                    {
                        for (int _payVoidLineIndex = 0; _payVoidLineIndex < EOBInsurancePaymentDtls.Count; _payVoidLineIndex++)
                        {
                            if (EOBInsurancePaymentDtls[_payVoidLineIndex] != null)
                            {

                                EOBInsPayDtl = EOBInsurancePaymentDtls[_payVoidLineIndex];
                                oParameters.Clear();
                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nEOBID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nEOBDtlID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nEOBPaymentDetailID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nBillingTransactionLineNo", EOBInsPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
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
                                oParameters.Add("@nUserID", AppSettings.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@sUserName", AppSettings.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                oParameters.Add("@dtCreatedDateTime", EOBInsPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                oParameters.Add("@dtModifiedDateTime", EOBInsPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                oParameters.Add("@nClinicID", EOBInsPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                if (EOBInsPayDtl.RefEOBPaymentID == 0) { EOBInsPayDtl.RefEOBPaymentID = 0; }
                                if (EOBInsPayDtl.RefEOBPaymentDetailID == 0) { EOBInsPayDtl.RefEOBPaymentDetailID = 0; }

                                oParameters.Add("@nRefEOBPaymentID", EOBInsPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nResEOBPaymentID", EOBInsPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nContactInsID", EOBInsPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nCreditLineID", EOBInsPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nEOBVoidPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nCloseDate", EOBInsPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                oParameters.Add("@nOldRefEOBPaymentID", EOBInsPayDtl.OldRefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nOldRefEOBPaymentDetailID", EOBInsPayDtl.OldRefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nOldResEOBPaymentID", EOBInsPayDtl.OldReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nOldResEOBPaymentDetailID", EOBInsPayDtl.OldReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                oParameters.Add("@nTrackTrnID", EOBInsPayDtl.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nTrackTrnDtlID", EOBInsPayDtl.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nTrackTrnLineNo", EOBInsPayDtl.TrackBillingTransactionLineNo, ParameterDirection.Input, SqlDbType.Int);  // numeric(18,0),
                                oParameters.Add("@sSubClaimNo", EOBInsPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // VarChar

                                oParameters.Add("@nVoidType", EOBInsPayDtl.VoidType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oParameters.Add("@bIsPaymentVoid", true, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                oParameters.Add("@nPaymentVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                oParameters.Add("@nPaymentVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),


                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL_InsVoid";

                                int _result = _sqlCommand.ExecuteNonQuery();

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
                    #endregion " Payment Line Details save for voiding Insurance payment "

                    #endregion " For Correction Credit entries "

                    #region " For Main Credit entries "

                    EOBInsurancePaymentDtls = GetInsurancePaymentForVoid(EOBPaymentID, 0, 0, PatientId, VoidCloseDate, VoidTrayID, VoidTrayCode, VoidTrayName, VoidUserID, VoidUserName, false, false, true);

                    #region " Payment Line Details save for voiding Insurance payment "

                    if (EOBPaymentID > 0 && EOBInsurancePaymentDtls != null && EOBInsurancePaymentDtls.Count > 0)
                    {
                        for (int _payVoidLineIndex = 0; _payVoidLineIndex < EOBInsurancePaymentDtls.Count; _payVoidLineIndex++)
                        {
                            if (EOBInsurancePaymentDtls[_payVoidLineIndex] != null)
                            {

                                EOBInsPayDtl = EOBInsurancePaymentDtls[_payVoidLineIndex];
                                oParameters.Clear();
                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nEOBID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nEOBDtlID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nEOBPaymentDetailID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nBillingTransactionID", EOBInsPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nBillingTransactionDetailID", EOBInsPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nBillingTransactionLineNo", EOBInsPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
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
                                oParameters.Add("@nUserID", AppSettings.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@sUserName", AppSettings.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                oParameters.Add("@dtCreatedDateTime", EOBInsPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                oParameters.Add("@dtModifiedDateTime", EOBInsPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                oParameters.Add("@nClinicID", EOBInsPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                if (EOBInsPayDtl.RefEOBPaymentID == 0) { EOBInsPayDtl.RefEOBPaymentID = 0; }
                                if (EOBInsPayDtl.RefEOBPaymentDetailID == 0) { EOBInsPayDtl.RefEOBPaymentDetailID = 0; }

                                oParameters.Add("@nRefEOBPaymentID", EOBInsPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nRefEOBPaymentDetailID", EOBInsPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nResEOBPaymentID", EOBInsPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nResEOBPaymentDetailID", EOBInsPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nContactInsID", EOBInsPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nCreditLineID", EOBInsPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nEOBVoidPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nCloseDate", EOBInsPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                oParameters.Add("@nOldRefEOBPaymentID", EOBInsPayDtl.OldRefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nOldRefEOBPaymentDetailID", EOBInsPayDtl.OldRefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nOldResEOBPaymentID", EOBInsPayDtl.OldReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nOldResEOBPaymentDetailID", EOBInsPayDtl.OldReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),

                                oParameters.Add("@nTrackTrnID", EOBInsPayDtl.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nTrackTrnDtlID", EOBInsPayDtl.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nTrackTrnLineNo", EOBInsPayDtl.TrackBillingTransactionLineNo, ParameterDirection.Input, SqlDbType.Int);  // numeric(18,0),
                                oParameters.Add("@sSubClaimNo", EOBInsPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // VarChar

                                oParameters.Add("@nVoidType", EOBInsPayDtl.VoidType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oParameters.Add("@bIsPaymentVoid", true, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                oParameters.Add("@nPaymentVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                oParameters.Add("@nPaymentVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),


                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL_InsVoid";

                                int _result = _sqlCommand.ExecuteNonQuery();

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
                    #endregion " Payment Line Details save for voiding Insurance payment "

                    #endregion " For Main Credit entries "

                    #region "Master Void Payment Note"

                    if (VoidNote != null)
                    {
                        Object _RcValue = null;
                        _RcValue = null;
                        oParameters.Clear();

                        oParameters.Add("@nID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                        oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nEOBVoidPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@dVoidAmount", EOBPaymentInsurance.CheckAmount * -1, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 0),
                        oParameters.Add("@sNoteDescription", VoidNote.ToString(), ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0),
                        oParameters.Add("@nVoidNoteType", VoidType.InsurancePaymentVoid.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),	
                        oParameters.Add("@sUserName", AppSettings.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0),
                        oParameters.Add("@nUserID", AppSettings.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	varchar(5),
                        oParameters.Add("@nClinicID", EOBPaymentInsurance.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	decimal(18, 2),
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

                    }


                    #endregion "Master Void Payment Note"

                    if (_EOBPayId > 0)
                    {

                        _sqlQuery = " UPDATE BL_EOBPayment_MST WITH (READPAST) SET bIsPaymentVoid = 'true', nPaymentVoidCloseDate = " + VoidCloseDate + ", nPaymentVoidTrayID = " + VoidTrayID + ", nVoidType = " + VoidType.InsurancePaymentVoid.GetHashCode() + " WHERE ( nEOBPaymentID = " + EOBPaymentID + " ) AND ISNULL(nVoidType,0) <> " + VoidType.InsurancePaymentVoidEntry.GetHashCode() + " ";// OR nResEOBPaymentID = " + EOBPaymentID + " OR nRefEOBPaymentID =  "+ EOBPaymentID + " ";
                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = _sqlQuery;
                        _sqlCommand.ExecuteNonQuery();

                        _sqlQuery = "";
                        //_sqlQuery = " UPDATE BL_EOBPayment_DTL SET bIsPaymentVoid = 'true', nPaymentVoidCloseDate = " + VoidCloseDate + ", nPaymentVoidTrayID = " + VoidTrayID + ", nVoidType = " + VoidType.InsurancePaymentVoid.GetHashCode() + " WHERE ( nEOBPaymentID = " + EOBPaymentID + " OR nResEOBPaymentID = " + EOBPaymentID + " OR nRefEOBPaymentID =  " + EOBPaymentID + " ) AND ISNULL(nVoidType,0) <> " + VoidType.InsurancePaymentVoidEntry.GetHashCode() + " AND (ISNULL(nPaymentType,0) <> " + EOBPaymentType.InsuracePayment.GetHashCode() + " OR nPaymentSubType <> " + EOBPaymentSubType.Adjuestment.GetHashCode() + ") ";
                        _sqlQuery = " UPDATE BL_EOBPayment_DTL WITH (READPAST) SET bIsPaymentVoid = 'true', nPaymentVoidCloseDate = " + VoidCloseDate + ", nPaymentVoidTrayID = " + VoidTrayID + ", nVoidType = " + VoidType.InsurancePaymentVoid.GetHashCode() + " WHERE ( nEOBPaymentID = " + EOBPaymentID + " OR nResEOBPaymentID = " + EOBPaymentID + " OR nRefEOBPaymentID =  " + EOBPaymentID + " ) AND ISNULL(nVoidType,0) <> " + VoidType.InsurancePaymentVoidEntry.GetHashCode();//" AND (ISNULL(nPaymentType,0) <> " + EOBPaymentType.InsuracePayment.GetHashCode() + " OR nPaymentSubType <> " + EOBPaymentSubType.Adjuestment.GetHashCode() + ") ";
                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = _sqlQuery;
                        _sqlCommand.ExecuteNonQuery();

                        //_sqlQuery = "";
                        //_sqlQuery = " UPDATE BL_EOBPayment_DTL SET bIsPaymentVoid = 'true', nPaymentVoidCloseDate = " + VoidCloseDate + ", nPaymentVoidTrayID = " + VoidTrayID + ", nVoidType = " + VoidType.InsurancePaymentVoid.GetHashCode() + " WHERE ( ISNULL(nVoidType,0) <> " + VoidType.InsurancePaymentVoidEntry.GetHashCode() + 
                        //            " AND nEOBPaymentID = (SELECT nEOBPaymentID FROM BL_EOBPayment_DTL WHERE nRefEOBPaymentID = " + EOBPaymentID + " AND ISNULL(bIsVoid,0) = 1))";
                        //_sqlCommand = new System.Data.SqlClient.SqlCommand();
                        //_sqlCommand.Connection = _sqlConnection;
                        //_sqlCommand.Transaction = _sqlTransaction;
                        //_sqlCommand.CommandType = CommandType.Text;
                        //_sqlCommand.CommandText = _sqlQuery;
                        //_sqlCommand.ExecuteNonQuery();

                        _sqlQuery = "";
                        _sqlQuery = " UPDATE BL_EOBPayment_EOB WITH (READPAST) SET bIsPaymentVoid = 'true', nPaymentVoidCloseDate = " + VoidCloseDate + ", nPaymentVoidTrayID = " + VoidTrayID + ", nVoidType = " + VoidType.InsurancePaymentVoid.GetHashCode() + " WHERE ( nEOBPaymentID = " + EOBPaymentID + " ) AND ISNULL(nVoidType,0) <> " + VoidType.InsurancePaymentVoidEntry.GetHashCode() + " ";
                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = _sqlQuery;
                        _sqlCommand.ExecuteNonQuery();

                        _sqlQuery = "";
                        _sqlQuery = " UPDATE BL_EOB_ReasonCodes WITH (READPAST) SET bIsPaymentVoid = 'true', nPaymentVoidCloseDate = " + VoidCloseDate + ", nPaymentVoidTrayID = " + VoidTrayID + ", nVoidType = " + VoidType.InsurancePaymentVoid.GetHashCode() + " WHERE nEOBPaymentID = " + EOBPaymentID + " ";
                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = _sqlQuery;
                        _sqlCommand.ExecuteNonQuery();

                        //***************************************
                        //Added By Debasish on 19th July Aug 2010
                        //5070 Requirement
                        //***************************************
                        _sqlQuery = "";
                        _sqlQuery = " UPDATE BL_EOB_Notes WITH (READPAST) SET bIsVoid = 'true' WHERE nEOBPaymentID = " + EOBPaymentID;
                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = _sqlQuery;
                        _sqlCommand.ExecuteNonQuery();
                        //***************************************

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
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_retVal != null) { _retVal = null; }
                if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                if (_sqlCommand != null) { _sqlCommand.Dispose(); }
                if (_sqlTransaction != null) { _sqlTransaction.Dispose(); }
                if (EOBInsPayDtl != null) { EOBInsPayDtl.Dispose(); };
                if (EOBInsurancePaymentDtls != null) { EOBInsurancePaymentDtls.Dispose(); }
            }
            return 0;
        }

        public EOBPayment.Common.EOBInsurancePaymentDetails GetInsurancePaymentForVoid(Int64 EOBPaymentID, Int64 EOBId, Int64 EOBDtlId, Int64 PatientId, int VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName, Int64 VoidUserId, string VoidUserName, bool IsReserve,bool IsCorrectionCredit, bool IsMainCreditLine)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtInsPayment = new DataTable();
            EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentDetail = null;
            EOBPayment.Common.EOBInsurancePaymentDetails oEOBInsPaymentDetails = null;
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
            Int64 _creditLineid = 0;
            try
            {
                if (EOBPaymentID > 0)
                {
                    _voidTrayID = VoidTrayID;
                    _voidCloseDate = VoidCloseDate;
                    #region "Retrieve Patient Payment Details For Void"

                    if (!IsCorrectionCredit && IsReserve && !IsMainCreditLine) 
                    {
                        oParameters.Clear();
                        oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nEOBID", EOBId, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nEOBDtlID", EOBDtlId, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@IsReserve", IsReserve, ParameterDirection.Input, SqlDbType.Bit);
                        oDB.Connect(false);
                        oDB.Retrive("BL_SELECT_InsurancePaymentDetailsForVoid", oParameters, out dtInsPayment);
                        oDB.Disconnect();
                        oParameters.Clear();
                    }
                    else if (IsCorrectionCredit && !IsReserve && !IsMainCreditLine)
                    {
                        oParameters.Clear();
                        oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Connect(false);
                        oDB.Retrive("BL_SELECT_InsurancePaymentDetailsForCreditVoid", oParameters, out dtInsPayment);
                        oDB.Disconnect();
                        oParameters.Clear();
                    }
                    else if (!IsCorrectionCredit && !IsReserve && IsMainCreditLine)
                    {
                        oParameters.Clear();
                        oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Connect(false);
                        oDB.Retrive("BL_SELECT_InsurancePaymentDetailsForMainCreditVoid", oParameters, out dtInsPayment);
                        oDB.Disconnect();
                        oParameters.Clear();
                    }
                    else if (!IsCorrectionCredit && !IsReserve && !IsMainCreditLine)
                    {
                        oParameters.Clear();
                        oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nEOBID", EOBId, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nEOBDtlID", EOBDtlId, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@IsReserve", IsReserve, ParameterDirection.Input, SqlDbType.Bit);
                        oDB.Connect(false);
                        oDB.Retrive("BL_SELECT_InsurancePaymentDetailsForVoid", oParameters, out dtInsPayment);
                        oDB.Disconnect();
                        oParameters.Clear();
                    }
                    


                    #endregion "Retrieve Patient Payment Details For Void"

                    #region " Set Payment Detail Data "

                    if (dtInsPayment != null && dtInsPayment.Rows.Count > 0)
                    {
                        oEOBInsPaymentDetails = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetails();
                        for (int _nPayDtlCounter = 0; _nPayDtlCounter < dtInsPayment.Rows.Count; _nPayDtlCounter++)
                        {
                            oEOBInsPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();

                            #region "Get the debit allocation entry for voiding payment"
                            _PaymentSign = Convert.ToInt32(dtInsPayment.Rows[_nPayDtlCounter]["nPaySign"].ToString());
                            _PaymentType = Convert.ToInt32(dtInsPayment.Rows[_nPayDtlCounter]["nPaymentType"].ToString());
                            _PaymentSubType = Convert.ToInt32(dtInsPayment.Rows[_nPayDtlCounter]["nPaymentSubType"].ToString());
                            _creditLineid = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nCreditLineID"].ToString());
                            if (_PaymentSign == 1) { _EOBPaymentSign = EOBPaymentSign.Payment_Credit; }
                            else if (_PaymentSign == 2) { _EOBPaymentSign = EOBPaymentSign.Receipt_Debit; }
                            if (_PaymentType == 1) { _EOBPaymentType = EOBPaymentType.InsuraceReserverd; }
                            else if (_PaymentType == 4) { _EOBPaymentType = EOBPaymentType.InsuracePayment; }
                            else if (_PaymentType == 8) { _EOBPaymentType = EOBPaymentType.InsuranceCorrection; }
                            if (_PaymentSubType == 13) { _EOBPaymentSubType = EOBPaymentSubType.Correction; }
                            else if (_PaymentSubType == 1) { _EOBPaymentSubType = EOBPaymentSubType.Insurace; }
                            else if (_PaymentSubType == 6) { _EOBPaymentSubType = EOBPaymentSubType.WriteOff; }
                            else if (_PaymentSubType == 7) { _EOBPaymentSubType = EOBPaymentSubType.WithHold; }

                            //if (_EOBPaymentSign == EOBPaymentSign.Receipt_Debit || _EOBPaymentSubType == EOBPaymentSubType.Correction || (_EOBPaymentSign == EOBPaymentSign.Payment_Credit && _EOBPaymentType == EOBPaymentType.InsuraceReserverd))
                            //if ((_EOBPaymentType != EOBPaymentType.InsuracePayment && _EOBPaymentSubType != EOBPaymentSubType.Insurace && _EOBPaymentSign != EOBPaymentSign.Payment_Credit) ||
                            //     _EOBPaymentSign == EOBPaymentSign.Receipt_Debit || _EOBPaymentSubType == EOBPaymentSubType.Correction || (_EOBPaymentSign == EOBPaymentSign.Payment_Credit && _EOBPaymentType == EOBPaymentType.InsuraceReserverd) )

                            //Skip the main credit line entry
                            //if(!(_creditLineid == 0 && _EOBPaymentType == EOBPaymentType.InsuracePayment && _EOBPaymentSubType == EOBPaymentSubType.Insurace && _EOBPaymentSign == EOBPaymentSign.Payment_Credit))
                            //{
                                _PaymentMode = Convert.ToInt32(dtInsPayment.Rows[_nPayDtlCounter]["nPayMode"].ToString());
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


                                oEOBInsPaymentDetail.EOBPaymentID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nEOBPaymentID"].ToString());
                                oEOBInsPaymentDetail.EOBID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nEOBID"].ToString());
                                oEOBInsPaymentDetail.EOBDtlID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nEOBDtlID"].ToString());
                                oEOBInsPaymentDetail.EOBPaymentDetailID = GetPrefixTransactionID();

                                oEOBInsPaymentDetail.BillingTransactionID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nBillingTransactionID"].ToString());
                                oEOBInsPaymentDetail.BillingTransactionDetailID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nBillingTransactionDetailID"].ToString());
                                oEOBInsPaymentDetail.BillingTransactionLineNo = Convert.ToInt32(dtInsPayment.Rows[_nPayDtlCounter]["nBillingTransactionLineNo"].ToString());
                                oEOBInsPaymentDetail.DOSFrom = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nDOSFrom"].ToString());
                                oEOBInsPaymentDetail.DOSTo = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nDOSTo"].ToString());
                                oEOBInsPaymentDetail.CPTCode = Convert.ToString(dtInsPayment.Rows[_nPayDtlCounter]["sCPTCode"].ToString());
                                oEOBInsPaymentDetail.CPTDescription = Convert.ToString(dtInsPayment.Rows[_nPayDtlCounter]["sCPTDescription"].ToString());
                                oEOBInsPaymentDetail.ReserveEOBPaymentID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nResEOBPaymentID"].ToString());
                                oEOBInsPaymentDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nResEOBPaymentDetailID"].ToString());
                                oEOBInsPaymentDetail.OldRefEOBPaymentDetailID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nOldRefEOBPaymentDetailID"].ToString());
                                oEOBInsPaymentDetail.OldRefEOBPaymentID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nOldRefEOBPaymentID"].ToString());
                                oEOBInsPaymentDetail.OldReserveEOBPaymentDetailID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nOldResEOBPaymentDetailID"].ToString());
                                oEOBInsPaymentDetail.OldReserveEOBPaymentID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nOldResEOBPaymentID"].ToString());

                                oEOBInsPaymentDetail.IsNullAmount = false;
                                decimal _fillPayAmt = Convert.ToDecimal(dtInsPayment.Rows[_nPayDtlCounter]["nAmount"].ToString());

                                if (_EOBPaymentType == EOBPaymentType.InsuraceReserverd && _EOBPaymentSign == EOBPaymentSign.Payment_Credit)
                                {
                                    oEOBInsPaymentDetail.PaymentType = EOBPaymentType.InsuraceReserverd;
                                    oEOBInsPaymentDetail.PaymentSubType = EOBPaymentSubType.Reserved;
                                    oEOBInsPaymentDetail.PaySign = EOBPaymentSign.Payment_Credit;
                                    oEOBInsPaymentDetail.Amount = (_fillPayAmt * -1);
                                    oEOBInsPaymentDetail.PayMode = _EOBPaymentMode;
                                }
                                else if (_EOBPaymentType == EOBPaymentType.InsuracePayment && _EOBPaymentSubType == EOBPaymentSubType.WriteOff && _EOBPaymentSign == EOBPaymentSign.Receipt_Debit)
                                {
                                    oEOBInsPaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                    oEOBInsPaymentDetail.PaymentSubType = EOBPaymentSubType.WriteOff;
                                    oEOBInsPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                    oEOBInsPaymentDetail.Amount = (_fillPayAmt * -1);
                                    oEOBInsPaymentDetail.PayMode = _EOBPaymentMode;
                                }
                                else if (_EOBPaymentType == EOBPaymentType.InsuracePayment && _EOBPaymentSubType == EOBPaymentSubType.WithHold && _EOBPaymentSign == EOBPaymentSign.Receipt_Debit)
                                {
                                    oEOBInsPaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                    oEOBInsPaymentDetail.PaymentSubType = EOBPaymentSubType.WithHold;
                                    oEOBInsPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                    oEOBInsPaymentDetail.Amount = (_fillPayAmt * -1);
                                    oEOBInsPaymentDetail.PayMode = _EOBPaymentMode;
                                }
                                else if (_EOBPaymentType == EOBPaymentType.InsuracePayment && _EOBPaymentSubType == EOBPaymentSubType.Insurace && _EOBPaymentSign == EOBPaymentSign.Receipt_Debit)
                                {
                                    oEOBInsPaymentDetail.Amount = (_fillPayAmt * -1);
                                    oEOBInsPaymentDetail.PayMode = _EOBPaymentMode;
                                    oEOBInsPaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                    oEOBInsPaymentDetail.PaymentSubType = EOBPaymentSubType.Insurace;
                                    oEOBInsPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                }
                                else if (_EOBPaymentType == EOBPaymentType.InsuracePayment && _EOBPaymentSubType == EOBPaymentSubType.Insurace && _EOBPaymentSign == EOBPaymentSign.Payment_Credit)
                                {
                                    oEOBInsPaymentDetail.Amount = (_fillPayAmt * -1);
                                    oEOBInsPaymentDetail.PayMode = _EOBPaymentMode;
                                    oEOBInsPaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                    oEOBInsPaymentDetail.PaymentSubType = EOBPaymentSubType.Insurace;
                                    oEOBInsPaymentDetail.PaySign = EOBPaymentSign.Payment_Credit;
                                }
                                else if (_EOBPaymentType == EOBPaymentType.InsuranceCorrection && _EOBPaymentSubType == EOBPaymentSubType.Correction && _EOBPaymentSign == EOBPaymentSign.Receipt_Debit)
                                {
                                    oEOBInsPaymentDetail.Amount = (_fillPayAmt * -1);
                                    oEOBInsPaymentDetail.PayMode = _EOBPaymentMode;
                                    oEOBInsPaymentDetail.PaymentType = EOBPaymentType.InsuranceCorrection;
                                    oEOBInsPaymentDetail.PaymentSubType = EOBPaymentSubType.Correction;
                                    oEOBInsPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                }
                                else if (_EOBPaymentType == EOBPaymentType.InsuracePayment && _EOBPaymentSubType == EOBPaymentSubType.Correction && _EOBPaymentSign == EOBPaymentSign.Payment_Credit)
                                {
                                    oEOBInsPaymentDetail.Amount = (_fillPayAmt * -1);
                                    oEOBInsPaymentDetail.PayMode = _EOBPaymentMode;
                                    oEOBInsPaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                    oEOBInsPaymentDetail.PaymentSubType = EOBPaymentSubType.Correction;
                                    oEOBInsPaymentDetail.PaySign = EOBPaymentSign.Payment_Credit;
                                }
                                else if(_EOBPaymentType == EOBPaymentType.InsuranceCorrection && _EOBPaymentSubType == EOBPaymentSubType.Correction && _EOBPaymentSign == EOBPaymentSign.Payment_Credit)
                                {
                                    oEOBInsPaymentDetail.Amount = (_fillPayAmt * -1);
                                    oEOBInsPaymentDetail.PayMode = _EOBPaymentMode;
                                    oEOBInsPaymentDetail.PaymentType = EOBPaymentType.InsuranceCorrection;
                                    oEOBInsPaymentDetail.PaymentSubType = EOBPaymentSubType.Correction;
                                    oEOBInsPaymentDetail.PaySign = EOBPaymentSign.Payment_Credit;
                                }
                                else if (_EOBPaymentType != EOBPaymentType.InsuraceReserverd && _EOBPaymentSign != EOBPaymentSign.Payment_Credit)
                                {
                                    oEOBInsPaymentDetail.Amount = _fillPayAmt;
                                    oEOBInsPaymentDetail.PayMode = EOBPaymentMode.PaymentVoidReserved;
                                    oEOBInsPaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                    oEOBInsPaymentDetail.PaymentSubType = EOBPaymentSubType.Insurace;
                                    oEOBInsPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                }
                                else if (_EOBPaymentType == EOBPaymentType.InsuraceReserverd && _EOBPaymentSign == EOBPaymentSign.Receipt_Debit)
                                {
                                    oEOBInsPaymentDetail.Amount = (_fillPayAmt * -1);
                                    oEOBInsPaymentDetail.PayMode = EOBPaymentMode.PaymentVoidReserved;
                                    oEOBInsPaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                    oEOBInsPaymentDetail.PaymentSubType = EOBPaymentSubType.Insurace;
                                    oEOBInsPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                    oEOBInsPaymentDetail.ReserveEOBPaymentID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nEOBPaymentID"].ToString());
                                    oEOBInsPaymentDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nEOBPaymentDetailID"].ToString());
                                    oEOBInsPaymentDetail.OldReserveEOBPaymentDetailID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nEOBPaymentDetailID"].ToString());
                                    oEOBInsPaymentDetail.OldReserveEOBPaymentID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nEOBPaymentID"].ToString());
                                }
                                oEOBInsPaymentDetail.RefEOBPaymentID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nRefEOBPaymentID"].ToString());
                                oEOBInsPaymentDetail.RefEOBPaymentDetailID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nRefEOBPaymentDetailID"].ToString());
                                oEOBInsPaymentDetail.AccountID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nAccountID"].ToString());
                                oEOBInsPaymentDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                                oEOBInsPaymentDetail.MSTAccountID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nMSTAccountID"].ToString());
                                oEOBInsPaymentDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                oEOBInsPaymentDetail.PatientID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nPatientID"].ToString()); ;
                                oEOBInsPaymentDetail.PaymentTrayID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nPaymentTrayID"].ToString());
                                oEOBInsPaymentDetail.PaymentTrayCode = Convert.ToString(dtInsPayment.Rows[_nPayDtlCounter]["sPaymentTrayCode"].ToString());
                                oEOBInsPaymentDetail.PaymentTrayDescription = Convert.ToString(dtInsPayment.Rows[_nPayDtlCounter]["sPaymentTrayDescription"].ToString());
                                oEOBInsPaymentDetail.UserID = VoidUserId;
                                oEOBInsPaymentDetail.UserName = VoidUserName;
                                oEOBInsPaymentDetail.ClinicID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nClinicID"].ToString());
                                oEOBInsPaymentDetail.CloseDate = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nCloseDate"].ToString());
                                oEOBInsPaymentDetail.ContactInsID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nContactInsID"].ToString());

                                oEOBInsPaymentDetail.TrackBillingTransactionID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nTrackTrnID"].ToString());
                                oEOBInsPaymentDetail.TrackBillingTransactionDetailID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nTrackTrnDtlID"].ToString());
                                oEOBInsPaymentDetail.TrackBillingTransactionLineNo = Convert.ToInt32(dtInsPayment.Rows[_nPayDtlCounter]["nTrackTrnLineNo"].ToString());   

                                oEOBInsPaymentDetail.FinanceLieNo = 0;
                                if (Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nCreditLineID"].ToString()) == 0)
                                { oEOBInsPaymentDetail.MainCreditLineID = 0; }
                                else
                                { oEOBInsPaymentDetail.MainCreditLineID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nCreditLineID"].ToString()); }
                                oEOBInsPaymentDetail.IsMainCreditLine = false;
                                oEOBInsPaymentDetail.IsReserveCreditLine = false;
                                oEOBInsPaymentDetail.IsCorrectionCreditLine = false;
                                oEOBInsPaymentDetail.RefFinanceLieNo = 1;
                                oEOBInsPaymentDetail.UseRefFinanceLieNo = false;
                                oEOBInsPaymentDetail.SubClaimNo = Convert.ToString(dtInsPayment.Rows[_nPayDtlCounter]["sSubClaimNo"].ToString());
                                oEOBInsPaymentDetail.VoidCloseDate = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nVoidCloseDate"].ToString());
                                oEOBInsPaymentDetail.VoidTrayID = Convert.ToInt64(dtInsPayment.Rows[_nPayDtlCounter]["nVoidTrayID"].ToString());
                                oEOBInsPaymentDetail.IsVoid = Convert.ToBoolean(dtInsPayment.Rows[_nPayDtlCounter]["bIsVoid"].ToString());

                                oEOBInsPaymentDetail.VoidType = VoidType.InsurancePaymentVoidEntry;
                                oEOBInsPaymentDetail.VoidCloseDate = VoidCloseDate;
                                oEOBInsPaymentDetail.VoidTrayID = VoidTrayID;
                                oEOBInsPaymentDetails.Add(oEOBInsPaymentDetail);
                            //}
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
                if (oParameters != null) { oParameters.Dispose(); }
                if (oEOBInsPaymentDetail != null) { oEOBInsPaymentDetail.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
                if (dtInsPayment != null) { dtInsPayment.Dispose(); }
            }

            return oEOBInsPaymentDetails;
        }

        public EOBPayment.Common.PaymentInsurance GetMasterDetailsForInsurancePaymentVoid(Int64 EOBPaymentID, Int64 ClinicID, Int64 VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName, Int64 VoidUserId, string VoidUserName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtEOBPaymentMST = new DataTable();
            EOBPayment.Common.PaymentInsurance oInsurancePayment = null;
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
                    oDB.Retrive("BL_SELECT_InsurancePaymentMasterDetailsForVoid", oParameters, out dtEOBPaymentMST);
                    oDB.Disconnect();
                    oParameters.Clear();

                    #endregion "Retrieve Patient Payment Master Details For Void"

                    if (dtEOBPaymentMST != null && dtEOBPaymentMST.Rows.Count > 0)
                    {
                        oInsurancePayment = new global::gloBilling.EOBPayment.Common.PaymentInsurance();
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

                        oInsurancePayment.PaymentNumber = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentNo"].ToString());
                        oInsurancePayment.PaymentNumberPefix = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentNoPrefix"].ToString());
                        oInsurancePayment.EOBPaymentID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nEOBPaymentID"].ToString());
                        oInsurancePayment.EOBRefNO = Convert.ToString(dtEOBPaymentMST.Rows[0]["nEOBRefNO"].ToString());
                        oInsurancePayment.PayerName = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPayerName"].ToString());
                        oInsurancePayment.PayerID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nPayerID"].ToString());
                        oInsurancePayment.PayerType = EOBPaymentAccountType.InsuranceCompany;
                        oInsurancePayment.PaymentMode = _EOBPaymentMode;
                        oInsurancePayment.CheckNumber = Convert.ToString(dtEOBPaymentMST.Rows[0]["sCheckNumber"].ToString());
                        oInsurancePayment.CheckAmount = Convert.ToDecimal(dtEOBPaymentMST.Rows[0]["nCheckAmount"].ToString()) * -1;
                        oInsurancePayment.CheckDate = Convert.ToInt32(dtEOBPaymentMST.Rows[0]["nCheckDate"].ToString());
                        oInsurancePayment.CardType = Convert.ToString(dtEOBPaymentMST.Rows[0]["sCardType"].ToString());
                        oInsurancePayment.AuthorizationNo = Convert.ToString(dtEOBPaymentMST.Rows[0]["sAuthorizationNo"].ToString());
                        oInsurancePayment.CardExpiryDate = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nCardExpDate"].ToString());
                        oInsurancePayment.CardID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nCardID"].ToString());
                        oInsurancePayment.MSTAccountID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nMSTAccountID"].ToString());
                        oInsurancePayment.MSTAccountType = EOBPaymentAccountType.InsuranceCompany;
                        oInsurancePayment.ClinicID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nClinicID"].ToString());
                        oInsurancePayment.CreatedDateTime = DateTime.Now;
                        oInsurancePayment.ModifiedDateTime = DateTime.Now;
                        oInsurancePayment.PaymentTrayID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nPaymentTrayID"].ToString()); ;
                        oInsurancePayment.PaymentTrayCode = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentTrayCode"].ToString()); ;
                        oInsurancePayment.PaymentTrayDesc = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentTrayDescription"].ToString()); ;
                        oInsurancePayment.CloseDate = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nCloseDate"].ToString());
                        oInsurancePayment.UserID = VoidUserId;
                        oInsurancePayment.UserName = VoidUserName;
                        
                    }
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            { ex.ERROR_Log(ex.ToString()); throw; }
            catch //(Exception ex)
            { throw; }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
                if (dtEOBPaymentMST != null) { dtEOBPaymentMST.Dispose(); }
            }

            return oInsurancePayment;

        }

        public EOBPayment.Common.PaymentInsuranceLines GetEOBLinesDetailForInsurancePaymentVoid(Int64 EOBPaymentID, Int64 PatientId, Int64 VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName, Int64 VoidUserId, string VoidUserName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtEOBPaymentEOB = new DataTable();
            EOBPayment.Common.PaymentInsuranceLines PaymentInsuranceEOBLinesDtls = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLines();
            EOBPayment.Common.PaymentInsuranceLine oPaymentInsuranceLine = null;

            try
            {
                if (EOBPaymentID > 0)
                {
                    #region "Retrieve Patient Payment Details EOB For Void"
                    oParameters.Clear();
                    oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Connect(false);
                    oDB.Retrive("BL_SELECT_InsuranceEOBDetailsForVoid", oParameters, out dtEOBPaymentEOB);
                    oDB.Disconnect();
                    oParameters.Clear();
                    #endregion "Retrieve Patient Payment EOB Details For Void"

                    #region " Set EOB Data"
                    if (dtEOBPaymentEOB != null && dtEOBPaymentEOB.Rows.Count > 0)
                    {
                        for (int _nEOBDtlCounter = 0; _nEOBDtlCounter < dtEOBPaymentEOB.Rows.Count; _nEOBDtlCounter++)
                        {
                            oPaymentInsuranceLine = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLine();
                            oPaymentInsuranceLine.mEOBPaymentID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nEOBPaymentID"].ToString());
                            oPaymentInsuranceLine.mEOBID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nEOBID"].ToString());
                            oPaymentInsuranceLine.mEOBDtlID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nEOBDtlID"].ToString());
                            oPaymentInsuranceLine.PatientID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nPatientID"].ToString());
                            oPaymentInsuranceLine.PatInsuranceID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nInsuraceID"].ToString());
                            oPaymentInsuranceLine.InsContactID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nContactID"].ToString());
                            oPaymentInsuranceLine.BLTransactionID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nBillingTransactionID"].ToString());
                            oPaymentInsuranceLine.BLTransactionDetailID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nBillingTransactionDetailID"].ToString());
                            oPaymentInsuranceLine.BLTransactionLineNo = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nBillingTransactionLineNo"].ToString());
                            oPaymentInsuranceLine.ClaimNumber = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nClaimNo"].ToString());

                            oPaymentInsuranceLine.DOSFrom = Convert.ToInt32(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nDOSFrom"].ToString());
                            oPaymentInsuranceLine.DOSTo = Convert.ToInt32(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nDOSTo"].ToString());

                            oPaymentInsuranceLine.CPTCode = Convert.ToString(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["sCPTCode"].ToString());
                            oPaymentInsuranceLine.CPTDescription = Convert.ToString(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["sCPTDescription"].ToString());

                            oPaymentInsuranceLine.BLInsuranceID = 0;
                            oPaymentInsuranceLine.BLInsuranceName = "";
                            oPaymentInsuranceLine.BLInsuranceFlag = InsuranceTypeFlag.None;
                            if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCharges"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCharges"].ToString() != "")
                            {
                                oPaymentInsuranceLine.Charges = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCharges"].ToString());
                                oPaymentInsuranceLine.IsNullCharges = false;
                            }

                            if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dUnit"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dUnit"].ToString() != "")
                            {
                                oPaymentInsuranceLine.Unit = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dUnit"].ToString());
                                oPaymentInsuranceLine.IsNullUnit = false;
                            }
                            if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dTotalCharges"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dTotalCharges"].ToString() != "")
                            {
                                oPaymentInsuranceLine.TotalCharges = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dTotalCharges"].ToString());
                                oPaymentInsuranceLine.IsNullTotalCharges = false;
                            }
                            if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dAllowed"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dAllowed"].ToString() != "")
                            {
                                oPaymentInsuranceLine.Allowed = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dAllowed"].ToString());
                                oPaymentInsuranceLine.IsNullAllowed = false;
                            }

                            if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dWriteOff"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dWriteOff"].ToString() != "")
                            {
                                oPaymentInsuranceLine.WriteOff = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dWriteOff"].ToString()) * -1;
                                oPaymentInsuranceLine.IsNullWriteOff = false;
                            }

                            if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dNotCovered"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dNotCovered"].ToString() != "")
                            {
                                oPaymentInsuranceLine.NonCovered = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dNotCovered"].ToString()) * -1;
                                oPaymentInsuranceLine.IsNullNonCovered = false;
                            }

                            if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dPayment"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dPayment"].ToString() != "")
                            {
                                oPaymentInsuranceLine.InsuranceAmount = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dPayment"].ToString()) * -1;
                                oPaymentInsuranceLine.IsNullInsurance = false;
                            }

                            if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCopay"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCopay"].ToString() != "")
                            {
                                oPaymentInsuranceLine.Copay = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCopay"].ToString()) * -1;
                                oPaymentInsuranceLine.IsNullCopay = false;
                            }

                            if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dDeductible"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dDeductible"].ToString() != "")
                            {
                                oPaymentInsuranceLine.Deductible = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dDeductible"].ToString()) * -1;
                                oPaymentInsuranceLine.IsNullDeductible = false;
                            }

                            if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCoInsurance"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCoInsurance"].ToString() != "")
                            {
                                oPaymentInsuranceLine.CoInsurance = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dCoInsurance"].ToString()) * -1;
                                oPaymentInsuranceLine.IsNullCoInsurance = false;
                            }

                            if (dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dWithhold"].ToString() != null && dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dWithhold"].ToString() != "")
                            {
                                oPaymentInsuranceLine.Withhold = Convert.ToDecimal(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["dWithhold"].ToString()) * -1;
                                oPaymentInsuranceLine.IsNullWithhold = false;
                            }
                            oPaymentInsuranceLine.TrackBLTransactionID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nTrackTrnID"].ToString());
                            oPaymentInsuranceLine.TrackBLTransactionDetailID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nTrackTrnDtlID"].ToString());
                            oPaymentInsuranceLine.TrackBLTransactionLineNo = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nTrackTrnLineNo"].ToString());
                            oPaymentInsuranceLine.SubClaimNumber = Convert.ToString(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["sSubClaimNo"].ToString());
                            oPaymentInsuranceLine.InsCompanyID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nInsuranceCompanyID"].ToString());


                            oPaymentInsuranceLine.PaymentTrayID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nPaymentTrayID"].ToString());
                            oPaymentInsuranceLine.PaymentTrayCode = Convert.ToString(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["sPaymentTrayCode"].ToString());
                            oPaymentInsuranceLine.PaymentTrayDesc = Convert.ToString(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["sPaymentTrayDescription"].ToString());
                            oPaymentInsuranceLine.UserID = VoidUserId;
                            oPaymentInsuranceLine.UserName = VoidUserName;
                            oPaymentInsuranceLine.ClinicID = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nClinicID"].ToString());
                            oPaymentInsuranceLine.CloseDate = Convert.ToInt64(dtEOBPaymentEOB.Rows[_nEOBDtlCounter]["nCloseDate"].ToString());
                            oPaymentInsuranceLine.EOBType = EOBPaymentType.InsuracePayment;
                            PaymentInsuranceEOBLinesDtls.Add(oPaymentInsuranceLine);
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
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
                if (dtEOBPaymentEOB != null) { dtEOBPaymentEOB.Dispose(); }
                if (oPaymentInsuranceLine != null) { oPaymentInsuranceLine.Dispose(); }
            }
            return PaymentInsuranceEOBLinesDtls;
        }

        #endregion "Void Insurance Payment"

        public static bool IsTakeBack(Int64 EobPaymentId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            bool _isTakeBack = false;
            int _retVal = 0;
            object _value = null;
            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nEOBPaymentID", EobPaymentId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@IsTakeBack", 0, ParameterDirection.InputOutput, SqlDbType.Bit);
                _retVal = oDB.Execute("BL_EOB_TAKEBACK", oParameters, out _value);
                 
                if (_value != null || _value != DBNull.Value || _value.ToString() != "")
                { _isTakeBack = Convert.ToBoolean(_value); }

                //if (!_isTakeBack)
                //{
                //    _value = null;
                //    oParameters.Clear();
                //    oParameters.Add("@nEOBPayID", EobPaymentId, ParameterDirection.Input, SqlDbType.BigInt);
                //    oParameters.Add("@nIsResTakeBack", 0, ParameterDirection.InputOutput, SqlDbType.Bit);
                //    _retVal = oDB.Execute("BL_EOB_RESERVE_TAKEBACK", oParameters, out _value);
                //}

                //if (_value != null || _value != DBNull.Value || _value.ToString() != "")
                //{ _isTakeBack = Convert.ToBoolean(_value); }

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

        public static bool IsVoidedInsurancePayment(Int64 EobPaymentId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            bool _isVoided = false;
            int _retVal = 0;
            object _value = null;
            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nEOBPaymentID", EobPaymentId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@IsVoided", 0, ParameterDirection.InputOutput, SqlDbType.Bit);
                _retVal = oDB.Execute("BL_SELECT_IsVoided", oParameters, out _value);

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

        public static DataTable GetVoidedInsurancePayment(Int64 EobPaymentId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            DataTable _dtPaymentVoid = new DataTable();

            try
            {
                string _sqlQuery = "SELECT sUserName,dbo.CONVERT_TO_DATE(nVoidCloseDate) AS nVoidCloseDate,sNoteDescription FROM BL_EOBPaymentVoid_Notes WITH (NOLOCK) where nEOBPaymentID = " + EobPaymentId;
              
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
    }
}
