using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using gloGlobal;
using System.Windows.Forms;
namespace gloBilling
{
    class ClsFeeSchedule
    {

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        string _databaseconnectionstring = "";
        public ClsFeeSchedule(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
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

        ~ClsFeeSchedule()
        {
            Dispose(false);
        }

        public DataTable GetDates(Int64 _StdFeeScheeduleID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable _dtDates = new DataTable();
            try
            {
                string _sqlRetrieveQuery = "SELECT ISNULL(nFromDate,0) AS StartDate ,ISNULL(nToDate,0) AS EndDate From BL_FeeSchedule_Allocation WHERE nFeeScheduleID=" + _StdFeeScheeduleID;
                oDB.Retrive_Query(_sqlRetrieveQuery, out _dtDates);
                return _dtDates;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }
            return _dtDates;

        }

        public bool GetCPTCharges(string cptCode, out decimal Charges, out decimal Units)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtCPTCharges = null;
            string _sqlQuery = "";
            decimal _cptCharges = 0;
            decimal _cptUnits = 1;
            bool _retVal = false;

            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT ISNULL(nCharges,0) AS nCharges,ISNULL(nUnits,1) AS nUnits,ISNULL(bIsUseFromFeeSchedule,0) AS bIsUseFromFeeSchedule " +
                " FROM CPT_MST WITH(NOLOCK) WHERE bInactive = 'false' AND UPPER(sCPTCode) = '" + cptCode.Trim().ToUpper().Replace("'", "''") + "' AND nClinicID = " + gloPMGlobal.ClinicID + "";
                oDB.Retrive_Query(_sqlQuery, out _dtCPTCharges);
                oDB.Disconnect();

                if (_dtCPTCharges != null && _dtCPTCharges.Rows.Count > 0)
                {
                    if (Convert.ToDecimal(_dtCPTCharges.Rows[0]["nCharges"]) > 0)
                    { _cptCharges = Convert.ToDecimal(_dtCPTCharges.Rows[0]["nCharges"]); _retVal = true; }

                    if (Convert.ToDecimal(_dtCPTCharges.Rows[0]["nUnits"]) > 0)
                    { _cptUnits = Convert.ToDecimal(_dtCPTCharges.Rows[0]["nUnits"]); }


                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Charges = _cptCharges;

            Units = _cptUnits;

            return _retVal;
        }
        public void GetCPTFees(string sCPT, string sModifier, Int64 nTransactionDetailId, Int64 nfromDOS, Int64 ntoDOS, Int64 _nChargesFeeScheduleID, Int64 _nPatientProviderID, Int64 _nContactID, int DefaultChargesType, out decimal AllowedCharges, out bool IsAllowedAmount, out decimal chargesAmount, out bool _IsChargeAmount, out Int64 _Fee_ScheduleID)
        {

            sCPT = sCPT.Trim().Replace("'", "''");
            sModifier = sModifier.Trim().Replace("'", "''");
            AllowedCharges = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            chargesAmount = 0;
            _Fee_ScheduleID = 0;
            _IsChargeAmount = false;
            IsAllowedAmount = false;
            try
            {
                if (sCPT.Trim().Length > 0)
                {
                    DataTable _dtFeeSchdeule = null;
                    oDB.Connect(false);
                    if (ntoDOS == 0)
                        ntoDOS = nfromDOS;

                    oParameters.Add("@sCPT", sCPT, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sModifier", sModifier, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@fromDOS", nfromDOS, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@TODOS", ntoDOS, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@ChargesFeeScheduleID", _nChargesFeeScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@BillingProviderID", _nPatientProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@FacilityType", DefaultChargesType, ParameterDirection.Input, SqlDbType.Int);
                    oDB.Retrive("GetChargeAmount", oParameters, out _dtFeeSchdeule);
                    if (_dtFeeSchdeule != null && _dtFeeSchdeule.Rows.Count > 0)
                    {
                        _IsChargeAmount = Convert.ToBoolean(_dtFeeSchdeule.Rows[0]["IsChargeAmount"]);
                        if (_IsChargeAmount)
                        {
                            chargesAmount = Convert.ToDecimal(_dtFeeSchdeule.Rows[0]["ChargeAmount"].ToString());
                            _Fee_ScheduleID = Convert.ToInt64(_dtFeeSchdeule.Rows[0]["FeeScheduleID"].ToString());
                        }
                    }
                    oParameters.Clear();
                    _dtFeeSchdeule = null;
                    oParameters.Add("@sCPT", sCPT, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sModifier", sModifier, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@fromDOS", nfromDOS, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@TODOS", ntoDOS, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@FacilityType", DefaultChargesType, ParameterDirection.Input, SqlDbType.Int);
                    oParameters.Add("@nContactID", _nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@TransactionDetailId", nTransactionDetailId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("GetAllowedAmountForCharges", oParameters, out _dtFeeSchdeule);
                    if (_dtFeeSchdeule != null && _dtFeeSchdeule.Rows.Count > 0)
                    {
                        IsAllowedAmount = Convert.ToBoolean(_dtFeeSchdeule.Rows[0]["IsAllowedAmount"]);
                        if (IsAllowedAmount)
                            AllowedCharges = Convert.ToDecimal(_dtFeeSchdeule.Rows[0]["AllowedAmount"].ToString());
                    }
                    oDB.Disconnect();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;

                }
            }
        }

        public string GetDefaultCPT_CLIAno(string sCPT)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            string _sqlQuery = "";
            Object _Result;
            string _DefaultCPT_CLIAno = "";
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                _sqlQuery = "SELECT ISNULL(sCLIANo,'') AS sCLIANo FROM dbo.CPT_MST WHERE sCPTCode='" + sCPT + "' ";
                _Result = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();
                if (_Result != null)
                {
                    if (Convert.ToString(_Result) != "")
                    {
                        _DefaultCPT_CLIAno = Convert.ToString(_Result);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return "";
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }
            }

            return _DefaultCPT_CLIAno;
        }

        public bool GetDefaultSelf(string sCPT)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            string _sqlQuery = "";
            Object _Result;
            bool _bDefaultSelf = false;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                _sqlQuery = "SELECT ISNULL(bDefaultSelf,0) AS bDefaultSelf FROM dbo.CPT_MST WHERE sCPTCode='" + sCPT + "' ";
                _Result = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();
                if (_Result != null)
                {
                    if (Convert.ToString(_Result) != "")
                    {
                        _bDefaultSelf = Convert.ToBoolean(_Result);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return false;
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }
            }

            return _bDefaultSelf;
        }
        public DataTable GetCharges(Int64 _StdFeeScheeduleID, Boolean _IsCopyFeeScheduleID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable _dtCharges = new DataTable();
            try
            {
                string _sqlRetrieveQuery = "";

                if (_IsCopyFeeScheduleID)
                {
                    _sqlRetrieveQuery = "SELECT  0 AS nFeeScheduleID, " +
                                      "ISNULL(sHCPCS, '') AS sHCPCS, ISNULL(sModifier, '') AS sModifier,'' as sModDesc, ISNULL(nNonFacilityFeeScheduleAmount, 0) " +
                                      " AS nNonFacilityFeeScheduleAmount, ISNULL(nFacilityFeeScheduleAmount, 0) AS nFacilityFeeScheduleAmount, " +
                                      " ISNULL(sSpecialtyID, '') AS sSpecialtyID,'' as sSpecialityDesc, ISNULL(nClinicCharges, 0) AS nClinicCharges,  " +
                                      " ISNULL(BL_FeeSchedule_DTL.nChargePercentage, 0) AS nChargePercentage, ISNULL(BL_FeeSchedule_DTL.nVariantAmount, 0) AS nVariantAmount," +
                                      " ISNULL(nLimitCharges, 0) AS nLimitCharges, ISNULL(nAllowedCharges, 0) AS nAllowedCharges, ISNULL(nnonfacilityChargeAmount,0) As nnonfacilityChargeAmount,ISNULL(nfacilityChargeAmount,0) As nfacilityChargeAmount " +
                                      " FROM BL_FeeSchedule_DTL WITH(NOLOCK) WHERE BL_FeeSchedule_DTL.nFeeScheduleID= " + _StdFeeScheeduleID + " ";
                }
                else
                {
                    _sqlRetrieveQuery = "SELECT  ISNULL(nFeeScheduleID, 0) AS nFeeScheduleID, " +
                                        "ISNULL(sHCPCS, '') AS sHCPCS, ISNULL(sModifier, '') AS sModifier,'' as sModDesc, ISNULL(nNonFacilityFeeScheduleAmount, 0) " +
                                        " AS nNonFacilityFeeScheduleAmount, ISNULL(nFacilityFeeScheduleAmount, 0) AS nFacilityFeeScheduleAmount, " +
                                        " ISNULL(sSpecialtyID, '') AS sSpecialtyID,'' as sSpecialityDesc, ISNULL(nClinicCharges, 0) AS nClinicCharges,  " +
                                        " ISNULL(BL_FeeSchedule_DTL.nChargePercentage, 0) AS nChargePercentage, ISNULL(BL_FeeSchedule_DTL.nVariantAmount, 0) AS nVariantAmount," +
                                        " ISNULL(nLimitCharges, 0) AS nLimitCharges, ISNULL(nAllowedCharges, 0) AS nAllowedCharges, ISNULL(nnonfacilityChargeAmount,0) As nnonfacilityChargeAmount,ISNULL(nfacilityChargeAmount,0) As nfacilityChargeAmount " +
                                        " FROM BL_FeeSchedule_DTL WITH(NOLOCK) WHERE BL_FeeSchedule_DTL.nFeeScheduleID= " + _StdFeeScheeduleID + " ";
                }
                oDB.Retrive_Query(_sqlRetrieveQuery, out _dtCharges);
                return _dtCharges;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }
            return _dtCharges;

        }

        public Decimal GetAllowedAmountForCharges(Int64 _nContactId, Int64 _nTransactionID, string sCPT, string sModifier, int nFacilityType, ref Boolean bHasAllowedAmt, Int64 fromDOS)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            oDB.Connect(false);
            try
            {
                object _sqlresult = new object();
                DataTable _sqlDataTable = new DataTable();
                Decimal _AllowedCharges = 0;
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable _dtFeeSchdeule = null;
                oParameters.Add("@sCPT", sCPT, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sModifier", sModifier, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@fromDOS", fromDOS, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@TODOS", fromDOS, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@FacilityType", nFacilityType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nContactID", _nContactId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@TransactionDetailId", _nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("GetAllowedAmountForCharges", oParameters, out _dtFeeSchdeule);
                if (_dtFeeSchdeule != null && _dtFeeSchdeule.Rows.Count > 0)
                {
                    bHasAllowedAmt = Convert.ToBoolean(_dtFeeSchdeule.Rows[0]["IsAllowedAmount"]);
                    if (bHasAllowedAmt)
                        _AllowedCharges = Convert.ToDecimal(_dtFeeSchdeule.Rows[0]["AllowedAmount"].ToString());
                }
                oDB.Disconnect();
                return _AllowedCharges;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
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

        public Decimal GetAllowedAmount(Int64 nContactID, string sCPT, string sModifier, int nFacilityType, ref Boolean bHasAllowedAmt, Int64 nfromDOS)
        {
            Decimal _AllowedAmt = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);

            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {


                DataTable _dtFeeSchdeule = null;
                oDB.Connect(false);
                oParameters.Add("@sCPT", sCPT, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sModifier", sModifier, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@fromDOS", nfromDOS, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@TODOS", nfromDOS, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@FacilityType", nFacilityType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nTransactionMasterID", nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("GetAllowedAmount", oParameters, out _dtFeeSchdeule);
                if (_dtFeeSchdeule != null && _dtFeeSchdeule.Rows.Count > 0)
                {
                    bHasAllowedAmt = Convert.ToBoolean(_dtFeeSchdeule.Rows[0]["IsAllowedAmount"]);
                    if (bHasAllowedAmt)
                        _AllowedAmt = Convert.ToDecimal(_dtFeeSchdeule.Rows[0]["AllowedAmount"].ToString());
                }
                oDB.Disconnect();
                return _AllowedAmt;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;

                }

            }
        }

        public bool ValidateFeeSchedule(Int64 _nFeeScheduleID, Int64 nFromDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            oDB.Connect(false);
            try
            {
                string _sqlquery = "";
                object _sqlresult = new object();
                _sqlquery = "select ISNULL(nFeeScheduleID,'') AS nFeeScheduleID from BL_FeeSchedule_Allocation WITH(NOLOCK) where nFeeScheduleID = " + _nFeeScheduleID + " " +
                        " and nFromDate <= " + nFromDate + " and nToDate >= " + nFromDate + " and nClinicID = " + gloPMGlobal.ClinicID + "";

                _sqlresult = new object();
                _sqlresult = oDB.ExecuteScalar_Query(_sqlquery);
                if (_sqlresult != null)
                {
                    if (Convert.ToString(_sqlresult).Trim() != "")
                    {
                        try
                        {
                            if (Convert.ToInt64(_sqlresult.ToString()) > 0)
                                return true;
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            ex = null;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
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

        public Boolean RemoveCharge(DataTable _RemovedCharged)
        {
            string _sqlQuery = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                for (int i = 0; i < _RemovedCharged.Rows.Count; i++)
                {
                    _sqlQuery = "Delete From BL_FeeSchedule_DTL Where nFeeScheduleID=" + _RemovedCharged.Rows[i][1].ToString() + " And sHCPCS='" + _RemovedCharged.Rows[i][0].ToString().Replace("'", "''") + "' AND ISNULL(sModifier,'')='" + _RemovedCharged.Rows[i][2].ToString() + "'";
                    Int64 _result = oDB.Execute_Query(_sqlQuery);

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return true;
        }

        public bool _IsDuplicate(string sFeeScheduleName, Int64 _StdFeeScheeduleID)
        {
            string _sqlQuery = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable _dtDuplicateCPT = new DataTable();
            try
            {
                //_sqlQuery = "SELECT BL_FeeSchedule_MST.sFeeScheduleName,BL_FeeSchedule_Allocation.nFromDate,BL_FeeSchedule_Allocation.nToDate  FROM   BL_FeeSchedule_MST " +
                //                               "INNER JOIN dbo.BL_FeeSchedule_Allocation ON   BL_FeeSchedule_MST.nFeeScheduleID=BL_FeeSchedule_Allocation.nFeeScheduleID " +
                //                                "AND (" + gloDateMaster.gloDate.DateAsNumber(mskStartDate) + " BETWEEN  BL_FeeSchedule_Allocation.nFromDate AND BL_FeeSchedule_Allocation.nToDate " +
                //                                "OR " + gloDateMaster.gloDate.DateAsNumber(mskEndDate) + " BETWEEN  BL_FeeSchedule_Allocation.nFromDate AND BL_FeeSchedule_Allocation.nToDate )" +
                //                                "AND (BL_FeeSchedule_MST.nClinicID = " + _ClinicID + ")" +
                //                                "WHERE  (sFeeScheduleName = '" + sFeeScheduleName.ToString().Replace("'", "''") + "')  AND (BL_FeeSchedule_MST.nFeeScheduleID <> " + _StdFeeScheeduleID + ") ";

                _sqlQuery = "SELECT BL_FeeSchedule_MST.sFeeScheduleName   FROM   BL_FeeSchedule_MST " +
                                                "WHERE  (sFeeScheduleName = '" + sFeeScheduleName.ToString().Replace("'", "''") + "')  AND (BL_FeeSchedule_MST.nFeeScheduleID <> " + _StdFeeScheeduleID + ") ";
                oDB.Retrive_Query(_sqlQuery, out _dtDuplicateCPT);
                if (_dtDuplicateCPT.Rows.Count > 0)
                    return true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }
            return false;
        }

        public bool UpdateFeeAllocationAndMaster(string mskStartDate, string mskEndDate, Int64 _ClinicID, string sFeeScheduleName, Int64 _StdFeeScheeduleID)
        {
            string _sqlQuery = "";
            Int64 _result = 0;
            object Result = null;
            bool IsUpdate = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                _sqlQuery = "Select Count(nFeeScheduleID) FROM BL_FeeSchedule_Allocation WHERE nFeeScheduleID=" + _StdFeeScheeduleID;
                Result = oDB.ExecuteScalar_Query(_sqlQuery);

                if (Result != null)
                {
                    if (Convert.ToInt16(Result) > 0)
                        _sqlQuery = " UPDATE BL_FeeSchedule_Allocation SET  nFromDate=" + gloDateMaster.gloDate.DateAsNumber(mskStartDate) + ",nToDate=" + gloDateMaster.gloDate.DateAsNumber(mskEndDate) + " where nFeeScheduleID=" + _StdFeeScheeduleID;
                    else
                        _sqlQuery = " INSERT INTO BL_FeeSchedule_Allocation (nFeeScheduleID , nFromDate,nToDate,nClinicID) " +
                                      " VALUES (" + _StdFeeScheeduleID + "," + gloDateMaster.gloDate.DateAsNumber(mskStartDate) + "," + gloDateMaster.gloDate.DateAsNumber(mskEndDate) + "," + _ClinicID + ")";

                    //******************************* Ends Here ***********************************

                    _result = oDB.Execute_Query(_sqlQuery);
                    if (_result != 0)
                    {
                        IsUpdate = true;
                    }
                }
                _sqlQuery = " UPDATE BL_FeeSchedule_MST SET  sFeeScheduleName = '" + sFeeScheduleName.Trim().Replace("'", "''") + "'" +
                               " WHERE  nFeeScheduleID= " + _StdFeeScheeduleID + "";
                _result = oDB.Execute_Query(_sqlQuery);
                if (_result != 0)
                {
                    IsUpdate = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                IsUpdate = false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }
            return IsUpdate;
        }
        public bool SaveFeeSchedultDTL(DataTable dtCharges, decimal nCahrgePercentage, ref Int64 StdFeeScheduleID)
        {

            Hashtable oParam = new Hashtable();
            Boolean bResult = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string strErrorMessage = string.Empty;
            object _result = 0;
            try
            {
                if (dtCharges != null && dtCharges.Rows.Count > 0)
                {
                    oParameters.Clear();
                    oDB.Connect(false);
                    oParameters.Add("@tvpFeeScheduleDTL", dtCharges, ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@nChargePercentage", nCahrgePercentage, ParameterDirection.Input, SqlDbType.Decimal);
                    oParameters.Add("@nFeeScheduleID", StdFeeScheduleID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDB.Execute("FeeScheduleDTL_Save_TVP", oParameters, out _result);
                    if (_result != null && Convert.ToString(_result).Trim() != "")
                    {
                        StdFeeScheduleID = Convert.ToInt64(_result);
                        bResult = true;
                    }
                    else
                        StdFeeScheduleID = 0;
                    oDB.Disconnect();
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                bResult = false;
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                bResult = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            return bResult;

        }

        public bool IsValidCPT(string CPTCode)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            bool _returnvalue = false;
            string sqlgetQuery = " SELECT sCPTCode " +
                                 " FROM CPT_MST WHERE sCPTCode in ('" + (CPTCode).Replace("'", "''") + "')";
            try
            {
                ODB.Connect(false);
                ODB.Retrive_Query(sqlgetQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    _returnvalue = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                ODB.Disconnect();
                if (ODB != null) { ODB.Dispose(); }

                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }

            }
            return _returnvalue;

        }

        public string GetFacilityMammogramNumber(string sCPT, Int64 nFacilityID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            string _sqlQuery = "";
            Object _Result;
            string sMammogramCertNo = "";
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                _sqlQuery = string.Format("select [dbo].[BL_GetFacilityMammogramNumber]('{0}',{1})", sCPT, nFacilityID);
                _Result = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();
                if (_Result != null)
                {
                    if (Convert.ToString(_Result) != "")
                    {
                        sMammogramCertNo = Convert.ToString(_Result);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return "";
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return sMammogramCertNo;
        }

        


        public bool InsertMasterDataAndAllocationForNewSchedule(string mskStartDate, string mskEndDate, Int64 _ClinicID, string sFeeScheduleName, Int64 _StdFeeScheeduleID)
        {

            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _strMSTQuery = " INSERT INTO BL_FeeSchedule_MST (nFeeScheduleID, nFeeScheduleType, sFeeScheduleName, nClinicID) " +
                                             " VALUES (" + _StdFeeScheeduleID + ", 0, '" + sFeeScheduleName.Replace("'", "''") + "', " + _ClinicID + ")";

                //******************************* Ends Here ***********************************
                _result = oDB.Execute_Query(_strMSTQuery);

                _strMSTQuery = " INSERT INTO BL_FeeSchedule_Allocation (nFeeScheduleID , nFromDate,nToDate,nClinicID) " +
                                      " VALUES (" + _StdFeeScheeduleID + "," + gloDateMaster.gloDate.DateAsNumber(mskStartDate) + "," + gloDateMaster.gloDate.DateAsNumber(mskEndDate) + "," + _ClinicID + ")";

                //******************************* Ends Here ***********************************
                _result = oDB.Execute_Query(_strMSTQuery);
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
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

        public DataTable GetShowCPTList(string FromCPT, string ToCPT, Int64 StdFeeScheduleID, Boolean IsCopyFeeSchedule)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtCPT = new DataTable();
            oDB.Connect(false);
            try
            {
                oDBParameters.Add("@FromCPTCode", FromCPT, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@ToCPTCode", ToCPT, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@nFeeScheduleID", StdFeeScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@IsCopyFeeSchedule", IsCopyFeeSchedule, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_GetCPTCodes", oDBParameters, out dtCPT);
                return dtCPT;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }
            return dtCPT;
        }

        public Int64 GetDefaultSelfPayFeeSchedule() 
        {            
            Int64 nFeeScheduleID = 0;            
            string sQuery = "SELECT sSettingsValue FROM settings WHERE sSettingsName = 'DefaultSelfPayFeeSchedule'";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);           
            oDB.Connect(false);
            try
            {
                nFeeScheduleID = Convert.ToInt64(oDB.ExecuteScalar_Query(sQuery));
            }
            catch (Exception ex) 
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
              
            }
            return nFeeScheduleID;
        }

    }
}
