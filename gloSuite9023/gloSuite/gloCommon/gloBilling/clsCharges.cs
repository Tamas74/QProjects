using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using gloGlobal;
using System.Collections;
using gloBilling.Common;
using gloSettings;


namespace gloBilling
{
    public static class gloCharges
    {


        #region " Form Data Retrieval Methods"

        public static DataSet GetChargesFormData(Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataSet dsChargesData = new DataSet();

            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientId", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicId", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bIsPatientChange", 0, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("BL_GET_CHARGESFORMDATA_V2", oParameters, out dsChargesData);
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

            return dsChargesData;
        }

        public static DataSet GetPatientChangeData(Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParamerters = null;
            DataSet dsChargesData = new DataSet();

            try
            {
                oParamerters = new gloDatabaseLayer.DBParameters();
                oParamerters.Add("@nUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParamerters.Add("@nPatientId", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParamerters.Add("@nClinicId", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParamerters.Add("@bIsPatientChange", 1, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("BL_GET_CHARGESFORMDATA_V2", oParamerters, out dsChargesData);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParamerters != null) { oParamerters.Dispose(); }
            }

            return dsChargesData;
        }

        public static DataSet GetLoginUserChangeData()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParamerters = null;
            DataSet dsUserData = new DataSet();

            try
            {
                oParamerters = new gloDatabaseLayer.DBParameters();
                oParamerters.Add("@nUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_GET_CHARGESFORM_USERCHANGEDATA_V2", oParamerters, out dsUserData);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParamerters != null) { oParamerters.Dispose(); }
            }

            return dsUserData;
        }

        public static DataSet GetPatientModifiedData(Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParamerters = null;
            DataSet dsChargesData = new DataSet();

            try
            {
                oParamerters = new gloDatabaseLayer.DBParameters();
                oParamerters.Add("@nUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParamerters.Add("@nPatientId", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParamerters.Add("@nClinicId", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_GET_PATIENT_MODIFIED_DATA", oParamerters, out dsChargesData);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParamerters != null) { oParamerters.Dispose(); }
            }

            return dsChargesData;
        }

        public static DataTable GetReloadPatientRefferalsData(Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParamerters = null;
            DataTable dtPatientRefferals = new DataTable();

            try
            {
                oParamerters = new gloDatabaseLayer.DBParameters();
                oParamerters.Add("@nPatientId", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_GETCharges_PatientRefferals", oParamerters, out dtPatientRefferals);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParamerters != null) { oParamerters.Dispose(); }
            }

            return dtPatientRefferals;
        }

        public static DataTable GetProviderSettings(Int64 nProviderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtProviderSettings = new DataTable();
            String _strQuery = "";
            try
            {
                _strQuery = "SELECT  sName, sValue, nProviderID FROM ProviderSettings WITH (NOLOCK) WHERE  nProviderID = " + nProviderID + " AND nClinicID =" + gloGlobal.gloPMGlobal.ClinicID + " ";
                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dtProviderSettings);
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

            return dtProviderSettings;
        }

        public static DataTable GetChargeBusinessCenterSettings()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtChargeBusinessCenter = null;
            try
            {
                oDB.Connect(false);
                oDB.Retrive("gsp_getBusinessNewModifyCharge_Setting", out _dtChargeBusinessCenter);
                oDB.Disconnect();
                return _dtChargeBusinessCenter;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

        }

        public static DataTable GetPatientAccountBusinessCenter(Int64 nAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            String _strQuery = "";
            DataTable _dtAccBusinessCenter = null;
            try
            {
                _strQuery = "SELECT PA_Accounts.nBusinessCenterID,BL_BusinessCenterCodes.sBusinessCenterCode FROM PA_Accounts INNER JOIN BL_BusinessCenterCodes ON PA_Accounts.nBusinessCenterID = dbo.BL_BusinessCenterCodes.nBusinessCenterID  where PA_Accounts.nPAccountID =" + nAccountID;
                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out _dtAccBusinessCenter);
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
            return _dtAccBusinessCenter;
        }

        public static Int64 GetClinicFeeScheduleID()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            String _strQuery = "";
            Int64 _defaultfeescheduleID = 0;
            object _defaultClinicFeeScheduleID = null;
            try
            {
                _strQuery = "SELECT  sSettingsValue AS DefaultFeeID  FROM    Settings WITH ( NOLOCK )  WHERE   sSettingsName = 'CLINICFEESCHEDULE'  ";
                oDB.Connect(false);
                _defaultClinicFeeScheduleID = oDB.ExecuteScalar_Query(_strQuery);
                if (_defaultClinicFeeScheduleID != null && Convert.ToString(_defaultClinicFeeScheduleID) != "")
                {
                    _defaultfeescheduleID = Convert.ToInt64(_defaultClinicFeeScheduleID);
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
                if (_defaultClinicFeeScheduleID != null) { _defaultClinicFeeScheduleID = null; }
            }
            return _defaultfeescheduleID;
        }
        public static Int64 GetICDRevision(string ICDCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            String _strQuery = "";
            Int64 _ICDRevision = 0;
            object _ObjICDRevision = null;
            try
            {
                _strQuery = "SELECT ISNULL(nICDRevision,9) AS nICDRevision FROM dbo.ICD9 WHERE sICD9Code='" + ICDCode + "'";
                oDB.Connect(false);
                _ObjICDRevision = oDB.ExecuteScalar_Query(_strQuery);
                if (_ObjICDRevision != null && Convert.ToString(_ObjICDRevision) != "")
                {
                    _ICDRevision = Convert.ToInt16(_ObjICDRevision);
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
                if (_ObjICDRevision != null) { _ObjICDRevision = null; }
            }
            return _ICDRevision;
        }
        public static Int64 GetProviderFeeScheduleID(Int64 nProviderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            String _strQuery = "";
            Int64 _defaultfeescheduleID = 0;
            object _defaultProviderFeeScheduleID = null;
            try
            {
                _strQuery = "SELECT CONVERT(NUMERIC, CASE WHEN sValue ='' THEN '0' ELSE ISNULL(sValue, '0') END) FROM ProviderSettings WITH ( NOLOCK ) WHERE nProviderID =" + nProviderID.ToString() + " AND sName ='Fee Schedule'";
                oDB.Connect(false);
                _defaultProviderFeeScheduleID = oDB.ExecuteScalar_Query(_strQuery);
                if (_defaultProviderFeeScheduleID != null && Convert.ToString(_defaultProviderFeeScheduleID) != "")
                {
                    _defaultfeescheduleID = Convert.ToInt64(_defaultProviderFeeScheduleID);
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
                if (_defaultProviderFeeScheduleID != null) { _defaultProviderFeeScheduleID = null; }
            }
            return _defaultfeescheduleID;
        }

        public static DataTable GetBusinessCenterByRules(Int64 nBillingProviderID, Int64 nFacilityID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParamerters = null;
            DataTable _dtBusinessCenter = null;
            try
            {
                oParamerters = new gloDatabaseLayer.DBParameters();
                oParamerters.Add("@nProviderId", nBillingProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oParamerters.Add("@nFacilityId", nFacilityID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BC_GetBusinessCenter_ByRules", oParamerters, out _dtBusinessCenter);
                oDB.Disconnect();
                return _dtBusinessCenter;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParamerters != null) { oParamerters.Dispose(); }
            }

        }

        #endregion

        #region "Other Methods"

        public static Int64 GenerateClaimNumber()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";

            Int64 _GeneratedClaimNumber = 0;
            DataTable _dtClaimNumber = null;
            try
            {
                _sqlQuery = "GenerateClaimNumber";
                oDB.Connect(false);
                oDB.Retrive(_sqlQuery, out _dtClaimNumber);
                oDB.Disconnect();
                if (_dtClaimNumber != null && _dtClaimNumber.Rows.Count > 0)
                {
                    _GeneratedClaimNumber = Convert.ToInt64(_dtClaimNumber.Rows[0][0]);

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _GeneratedClaimNumber = 0;
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); }
                if (_dtClaimNumber != null)
                { _dtClaimNumber.Dispose(); }

            }
            return _GeneratedClaimNumber;
        }

        public static Int64 GetMaxSerialNoForDx()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            Object _Result = null;
            Int64 _MaxserialNo = 0;

            try
            {
                _sqlQuery = "select ISNULL(MAX(nSerialNo),0) from BL_Transaction_Diagnosis WITH (NOLOCK) where nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "";
                oDB.Connect(false);
                _Result = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();
                if (_Result != null)
                {
                    _MaxserialNo = Convert.ToInt64(_Result);
                }
            }
            catch (SqlException sqlex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(sqlex.ToString(), true);
                _MaxserialNo = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _MaxserialNo = 0;
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); }

                if (_Result != null)
                { _Result = null; }
            }
            return _MaxserialNo;
        }

        public static string FormattedClaimNumberGeneration(string NumberSize)
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



        public static DataSet ClaimRulesGetRequiredInformation(string InsuranceID,
            Int64 BillingProviderID,
            Int64 ReferringProviderID,
            Int64 PatientID
            )
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;

            DataSet ds = null;

            try
            {
                ds = new DataSet();
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nInsuranceID", InsuranceID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nBillingProviderID", BillingProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nReferringProviderID", ReferringProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("ClaimRule_GetRequiredDetails", oParameters, out ds);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return ds;
        }

        public static string GetInsurancePlanName(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            string sPatientRelationshipToSubscriber = string.Empty;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                sPatientRelationshipToSubscriber = Convert.ToString(oDB.ExecuteScalar("ClaimRule_GetInsurancePlanName", oParameters));
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return sPatientRelationshipToSubscriber;
        }
        public static string GetInsurancePlanPayerID(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            string PayerID = "";
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                PayerID = Convert.ToString(oDB.ExecuteScalar("ClaimRule_GetInsurancePlanPayerID", oParameters));
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return PayerID;
        }
        public static string GetPatientRelationshipToSubscriber(Int64 InsuranceID, Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            string sPatientRelationshipToSubscriber = string.Empty;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nInsuranceID", InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                sPatientRelationshipToSubscriber = Convert.ToString(oDB.ExecuteScalar("ClaimRule_GetPatientRelationshipToSubscriber", oParameters));
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return sPatientRelationshipToSubscriber;
        }

        public static string GetReportingCategory(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            string sReportingCategory = string.Empty;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                sReportingCategory = Convert.ToString(oDB.ExecuteScalar("ClaimRule_GetReportingCategory", oParameters));
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return sReportingCategory;
        }

        public static string GetInsCompanyReportingCategory(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            string sReportingCategory = string.Empty;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                sReportingCategory = Convert.ToString(oDB.ExecuteScalar("ClaimRule_GetInsCompanyReportingCategory", oParameters));
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return sReportingCategory;
        }

        public static string GetPlanType(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            string sPlanType = string.Empty;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                sPlanType = Convert.ToString(oDB.ExecuteScalar("ClaimRule_GetPlanType", oParameters));
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return sPlanType;
        }

        public static string GetBillingProviderNPI(Int64 ProviderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            string sBillingProviderNPI = string.Empty;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                sBillingProviderNPI = Convert.ToString(oDB.ExecuteScalar("ClaimRule_GetBillingProviderNPI", oParameters));
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return sBillingProviderNPI;
        }

        public static DataTable GetBrokenRules(Int64 TransactionMasterID, Int64 TransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable dtBrokenRules = new DataTable();
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nTransactionMasterID", TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("ClaimRule_GetBrokenRules", oParameters, out dtBrokenRules);
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return dtBrokenRules;
        }

        public static string GetProviderNPI(Int64 ProviderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            string sInsurancePlanName = string.Empty;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                sInsurancePlanName = Convert.ToString(oDB.ExecuteScalar("ClaimRule_GetReferringProviderNPI", oParameters));
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return sInsurancePlanName;
        }

        public static string GetInsuranceCompanyName(Int64 InsuranceID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            string sInsurancePlanName = string.Empty;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nInsuranceID", InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                sInsurancePlanName = Convert.ToString(oDB.ExecuteScalar("ClaimRule_GetInsuranceCompanyName", oParameters));
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return sInsurancePlanName;
        }

        public static DataTable GetUniqueIDsForLines(int claimLinesCount)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dtLineIds = null;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@IDCount", claimLinesCount, ParameterDirection.Input, SqlDbType.Int);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetUniqueIDs", oParameters, out _dtLineIds);
                oDB.Disconnect();

                if (_dtLineIds != null && _dtLineIds.Rows.Count > 0)
                {
                    _dtLineIds.Columns.Add("ChargeLineNo");
                    _dtLineIds.AcceptChanges();
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dtLineIds;
        }

        public static Boolean CheckCptIcd9Exists(Int64 ExamId, gloSettings.ExternalChargesType nEMRTreatmentType, String sCptCode, Int64 nLineNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dtLineIds = null;
            Boolean _bResult = false;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nExamID", ExamId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTreatmentType", nEMRTreatmentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@sBilledCPTS", sCptCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sUsedLineNos", nLineNo, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("BL_CheckExistsInEMRTreatment", oParameters, out _dtLineIds);
                oDB.Disconnect();

                if (_dtLineIds != null && _dtLineIds.Rows.Count > 0)
                {
                    _bResult = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return false;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return _bResult;
        }

        public static DataTable GetRevenueCodeForCPT(CPTCollection CPTCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dtRevenueCodes = null;
            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@tvpCPTCollection", CPTCode, ParameterDirection.Input, SqlDbType.Structured);
                oDB.Connect(false);
                oDB.Retrive("BL_GetRevenueCodesForCPT", oParameters, out _dtRevenueCodes);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dtRevenueCodes;

        }

        public static DataTable GetTransactionDxList(Int64 TransactionId, Int64 VisitId, Int64 ClaimNo, Int64 PatientId, Int64 ClinicId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtDxList = new DataTable();
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = " select ISNULL(sDx1Code,'') AS sDx1Code,ISNULL(sDx1Description,'') AS sDx1Description,ISNULL(bIsClaimDx,0) AS bIsClaimDx ,ISNULL(bIsPrimaryDx,0) AS bIsPrimaryDx from BL_Transaction_Diagnosis WITH (NOLOCK) " +
                " where nTransactionID = " + TransactionId + " AND nClaimNo = " + ClaimNo + " " +
                " AND nClinicID = " + ClinicId + "";
                oDB.Retrive_Query(_sqlQuery, out dtDxList);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return dtDxList;
        }

        public static Boolean GetDefaultNDCForCPT(String sCPTCode, ref string sNDCCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtCPTDrug = null;
            string _sqlQuery = "";
            bool _bReturn = false;
            try
            {
                _sqlQuery = "SELECT ISNULL(bIsCPTDrug,0) AS bIsCPTDrug,ISNULL(sNDCCode,'') AS sNDCCode FROM CPT_MST WITH (NOLOCK) WHERE sCPTCode = '" + sCPTCode.Replace("'", "''") + "'";
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtCPTDrug);
                oDB.Disconnect();

                if (_dtCPTDrug.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(_dtCPTDrug.Rows[0]["bIsCPTDrug"]))
                    {
                        _bReturn = true;
                        sNDCCode = Convert.ToString(_dtCPTDrug.Rows[0]["sNDCCode"]);
                    }
                    else
                    {
                        sNDCCode = Convert.ToString(_dtCPTDrug.Rows[0]["sNDCCode"]);
                    }

                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _bReturn;
        }

        public static Boolean GetPromptForEPSDT(String sCPTCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtCPTDrug = null;
            string _sqlQuery = "";
            bool _bReturn = false;
            try
            {
                _sqlQuery = "SELECT ISNULL(bIsPromptforEpsdtFamPlan,0) AS bIsPromptforEpsdtFamPlan FROM CPT_MST WITH (NOLOCK) WHERE sCPTCode = '" + sCPTCode.Replace("'", "''") + "'";
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtCPTDrug);
                oDB.Disconnect();

                if (_dtCPTDrug.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(_dtCPTDrug.Rows[0]["bIsPromptforEpsdtFamPlan"]))
                    {
                        _bReturn = true;

                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _bReturn;
        }

        public static Boolean GetPromptForAnesthesia(String sCPTCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtCPTAnesthesia = null;
            string _sqlQuery = "";
            bool _bReturn = false;
            try
            {
                _sqlQuery = "SELECT ISNULL(bIsAnesthesia,0) AS bIsAnesthesia FROM CPT_MST WITH (NOLOCK) WHERE sCPTCode = '" + sCPTCode.Replace("'", "''") + "'";
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtCPTAnesthesia);
                oDB.Disconnect();

                if (_dtCPTAnesthesia.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(_dtCPTAnesthesia.Rows[0]["bIsAnesthesia"]))
                    {
                        _bReturn = true;

                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _bReturn;
        }

        public static Boolean GetPromptForEPSDT(Int64 nPatientID, string sInsuranceIds)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtEPSDT = null;
            string _sqlQuery = "";
            bool _bReturn = false;
            try
            {
                _sqlQuery = "SELECT PD.nContactID,ISNULL(CID.bBillEPSDTorFamilyPlanning,'FALSE') AS bBillEPSDTorFamilyPlanning FROM dbo.PatientInsurance_DTL PD "
                            + "INNER JOIN dbo.Contacts_Insurance_DTL CID "
                            + "ON PD.nContactID = CID.nContactID "
                            + "WHERE pd.nInsuranceID IN (" + sInsuranceIds + ") "
                            + "AND PD.nPatientID = " + nPatientID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtEPSDT);
                oDB.Disconnect();

                if (_dtEPSDT != null && _dtEPSDT.Rows.Count > 0)
                {
                    var result = (from dt in _dtEPSDT.AsEnumerable()
                                  where dt.Field<Boolean>("bBillEPSDTorFamilyPlanning") == true
                                  select dt).Count();

                    if (result > 0)
                    {
                        _bReturn = true;
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _bReturn;
        }
        public static gloICD.CodeRevision getICDRevisionbyClaimID(Int64 nTransactionMasterID, Int64 nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtICDRevision = null;
            string _sqlQuery = "";
            gloICD.CodeRevision _ICDRevision = gloICD.CodeRevision.ICD10;
            try
            {
                _sqlQuery = "  SELECT TOP 1 ISNULL(btcm.nICDRevision,0) AS nICDRevision FROM dbo.BL_Transaction_Claim_MST btcm  WHERE btcm.nTransactionMasterID=" + nTransactionMasterID + " AND btcm.nTransactionID=" + nTransactionID;
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtICDRevision);
                oDB.Disconnect();

                if (_dtICDRevision.Rows.Count > 0)
                {
                    _ICDRevision = (gloICD.CodeRevision)Convert.ToInt16(_dtICDRevision.Rows[0]["nICDRevision"]);

                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _ICDRevision;
        }
        public static Boolean GetPromptForEPSDT(Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtEPSDT = null;
            string _sqlQuery = "";
            bool _bReturn = false;
            try
            {
                //_sqlQuery = "SELECT ISNULL(bBillEPSDTorFamilyPlanning,0) AS bBillEPSDTorFamilyPlanning FROM Contacts_Insurance_DTL WITH (NOLOCK) WHERE nContactID = " + nContactID;
                //oDB.Connect(false);
                //oDB.Retrive_Query(_sqlQuery, out _dtEPSDT);
                //oDB.Disconnect();

                //if (_dtEPSDT.Rows.Count > 0)
                //{
                //    if (Convert.ToBoolean(_dtEPSDT.Rows[0]["bBillEPSDTorFamilyPlanning"]))
                //    {
                //        _bReturn = true;

                //    }
                //}

                _sqlQuery = "SELECT PD.nContactID,ISNULL(CID.bBillEPSDTorFamilyPlanning,'FALSE') AS bBillEPSDTorFamilyPlanning FROM dbo.PatientInsurance_DTL PD "
                            + "INNER JOIN dbo.Contacts_Insurance_DTL CID "
                            + "ON PD.nContactID = CID.nContactID "
                            + "WHERE pd.nInsuranceFlag != 0 "
                            + "AND PD.nPatientID = " + nPatientID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtEPSDT);
                oDB.Disconnect();

                if (_dtEPSDT != null && _dtEPSDT.Rows.Count > 0)
                {
                    var result = (from dt in _dtEPSDT.AsEnumerable()
                                  where dt.Field<Boolean>("bBillEPSDTorFamilyPlanning") == true
                                  select dt).Count();

                    if (result > 0)
                    {
                        _bReturn = true;
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _bReturn;
        }

        public static void GetMasterTransactionID(Int64 nTransactionID, out Int64 MasterTransactionID, out Int64 ClaimNo, out DateTime ModifyDate)
        {
            string _sqlQuery = "";
            gloDatabaseLayer.DBLayer ODB = null;
            DataTable _dt = new DataTable();
            Int64 _LocalTransMasterID = 0;
            Int64 _nClaimNo = 0;
            DateTime _ModifyDate = DateTime.Now;
            try
            {
                _sqlQuery = "SELECT ISNULL(nTransactionMasterID,0) AS nTransactionMasterID,ISNULL(nClaimNo,0) AS nClaimNo, dtModifyDate as ModifyDate FROM BL_Transaction_Claim_MST WITH (NOLOCK) WHERE nTransactionID = " + nTransactionID;
                ODB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                ODB.Connect(false);
                ODB.Retrive_Query(_sqlQuery, out _dt);
                ODB.Disconnect();
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    _LocalTransMasterID = Convert.ToInt64(_dt.Rows[0]["nTransactionMasterID"]);
                    _nClaimNo = Convert.ToInt64(_dt.Rows[0]["nClaimNo"]);
                    _ModifyDate = Convert.ToDateTime(_dt.Rows[0]["ModifyDate"]);
                }

                MasterTransactionID = _LocalTransMasterID;
                ClaimNo = _nClaimNo;
                ModifyDate = _ModifyDate;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                MasterTransactionID = _LocalTransMasterID;
                ClaimNo = _nClaimNo;
                ModifyDate = _ModifyDate;
            }
            finally
            {
                if (ODB != null) { ODB.Dispose(); }

            }

        }

        public static Int64 GetDefaultFeeSchedule()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            Object _Result = null;
            Int64 _clinicFeeSettingValue = 0;

            try
            {
                _sqlQuery = "SELECT ISNULL(sSettingsValue,0) FROM Settings WITH (NOLOCK) WHERE UPPER(sSettingsName) = 'CLINICFEESCHEDULE' AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "";
                oDB.Connect(false);
                _Result = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();
                if (_Result != null && Convert.ToString(_Result) != "")
                {
                    _clinicFeeSettingValue = Convert.ToInt64(_Result);
                }
            }
            catch (SqlException sqlex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(sqlex.ToString(), true);
                _clinicFeeSettingValue = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _clinicFeeSettingValue = 0;
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); }

                if (_Result != null)
                { _Result = null; }
            }
            return _clinicFeeSettingValue;
        }

        public static DataTable GetUsers()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtUsers = null;
            try
            {
                string strQuery = "";
                strQuery = " select nUserID,ISNULL(sLoginName,'') As sUserName from User_MST WITH(NOLOCK)";
                oDB.Connect(false);
                oDB.Retrive_Query(strQuery, out _dtUsers);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                throw;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _dtUsers;

        }
        public static Boolean GetInsDescCheckStaus(string sCPTCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object isAutoCheck = new object();
            try
            {
                oDBParameters.Add("@RetFlag", false, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@CPTCode", sCPTCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Execute("gsp_IsNocCPT", oDBParameters, out isAutoCheck);
                if (isAutoCheck != null)
                {
                    return Convert.ToBoolean(isAutoCheck);
                }
                else
                { return false; }



            }

            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (oDBParameters != null)
                    oDBParameters.Dispose();

            }

        }
        public static Boolean ReleaseLockStatus(Int64 nMasterTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Boolean isReleased = false;
            try
            {
                oDBParameters.Add("@nTransactionMasterID", nMasterTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sMachineName", Environment.MachineName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Execute("BL_UnLockClaims", oDBParameters);
                oDB.Disconnect();
                isReleased = true;
            }

            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                isReleased = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                isReleased = false;
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
                if (oDBParameters != null)
                    oDBParameters.Dispose();

            }
            return isReleased;
        }

        public static Boolean getPriorAuthorizationStatus(Int64 _PaID)
        {
            Boolean _isPriorAuthExists = false;
            gloDatabaseLayer.DBLayer oDB = null;
            try
            {
                object count = null;
                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                string sqlstring = "SELECT count(*) FROM  PriorAuthorization_Mst WITH (NOLOCK) WHERE nPriorAuthorizationID='" + _PaID + "' AND bIsActive=1";
                oDB.Connect(false);
                count = oDB.ExecuteScalar_Query(sqlstring);
                oDB.Disconnect();
                if (Convert.ToInt64(count) > 0)
                {
                    _isPriorAuthExists = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return _isPriorAuthExists;

        }

        public static Boolean CheckPriorAuthorization(Int64 nPatientID)
        {
            Boolean _isPriorAuthExists = false;
            gloDatabaseLayer.DBLayer oDB = null;
            try
            {
                object count = null;

                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                //string sqlstring = "SELECT count(*) FROM  PriorAuthorization_Mst WITH (NOLOCK) WHERE nPatientID='" + nPatientID + "' AND bIsActive=1";
                string sqlstring = "SELECT count(*) FROM  PriorAuthorization_Mst WITH (NOLOCK) WHERE nPatientID='" + nPatientID + "'";
                oDB.Connect(false);
                count = oDB.ExecuteScalar_Query(sqlstring);
                oDB.Disconnect();
                if (Convert.ToInt64(count) > 0)
                {
                    _isPriorAuthExists = true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return _isPriorAuthExists;
        }

        public static Boolean CheckForPriorAppointments(Int64 nPatientID)
        {
            Boolean _isPriorAuthExists = false;
            gloDatabaseLayer.DBLayer oDB = null;
            try
            {
                object count = null;

                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                string sqlstring = "SELECT count(*) FROM  PriorAuthorization_Mst WITH (NOLOCK) WHERE nPatientID='" + nPatientID + "' AND bIsActive=1";
                oDB.Connect(false);
                count = oDB.ExecuteScalar_Query(sqlstring);
                oDB.Disconnect();
                if (Convert.ToInt64(count) > 0)
                {
                    _isPriorAuthExists = true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return _isPriorAuthExists;
        }

        public static Boolean CheckForActivePriorAuth(Int64 nPatientID)
        {
            Boolean _isPriorAuthExists = false;
            gloDatabaseLayer.DBLayer oDB = null;
            try
            {
                object count = null;

                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                string sqlstring = "SELECT count(*) FROM  PriorAuthorization_Mst WITH (NOLOCK) WHERE nPatientID='" + nPatientID + "' AND bIsActive=1";
                oDB.Connect(false);
                count = oDB.ExecuteScalar_Query(sqlstring);
                oDB.Disconnect();
                if (Convert.ToInt64(count) > 0)
                {
                    _isPriorAuthExists = true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return _isPriorAuthExists;
        }

        public static String CheckRefNumber(Int64 nTransactionID, Int64 nContactID, Int64 nInsuranceID, Int64 nClinicid)
        {

            gloDatabaseLayer.DBLayer oDB = null;
            DataTable dtClaimRefNo = null;
            string _strClaimRefNo = "";
            try
            {

                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                string sqlstring = " SELECT  ISNULL(sClaimRemittanceRefNo,'') AS sClaimRemittanceRefNo " +
                                    " FROM BL_Transaction_ClaimRemittanceRef WITH (NOLOCK) " +
                                    " WHERE (nTransactionID = " + nTransactionID + " AND nContactID = " + nContactID + " " +
                                    " AND nInsuranceID = " + nInsuranceID + " AND nClinicID= " + gloGlobal.gloPMGlobal.ClinicID + ")";
                oDB.Connect(false);
                oDB.Retrive_Query(sqlstring, out dtClaimRefNo);
                oDB.Disconnect();
                if (dtClaimRefNo != null)
                {
                    if (dtClaimRefNo.Rows.Count > 0)
                    {
                        _strClaimRefNo = Convert.ToString(dtClaimRefNo.Rows[0]["sClaimRemittanceRefNo"]); ;

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return _strClaimRefNo;
        }

        public static DataTable GetVoidedInformation(Int64 nMasterTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtVoidInfo = null;
            string strVoidInfoQuery = "";
            try
            {
                strVoidInfoQuery = " SELECT  dbo.CONVERT_TO_DATE(BL_Transaction_Claim_MST.nVoidCloseDate) AS nVoidCloseDate, " +
                                " BL_Transaction_Claim_MST.dtVoidDate , BL_Transaction_Claim_MST.nVoidTrayID, ISNULL(BL_ChargesTray.sDescription,'') AS VoidTrayDesc, " +
                                " BL_Transaction_Claim_MST.nVoidUserID, ISNULL(User_MST.sLoginName,'') AS VoidUserName " +
                                " FROM BL_Transaction_Claim_MST WITH (NOLOCK) LEFT OUTER JOIN " +
                                " User_MST WITH (NOLOCK) ON BL_Transaction_Claim_MST.nVoidUserID = User_MST.nUserID LEFT OUTER JOIN " +
                                " BL_ChargesTray WITH (NOLOCK) ON BL_Transaction_Claim_MST.nVoidTrayID = BL_ChargesTray.nChargeTrayID " +
                                " WHERE BL_Transaction_Claim_MST.nTransactionMasterID= " + nMasterTransactionID + "";

                oDB.Connect(false);
                oDB.Retrive_Query(strVoidInfoQuery, out _dtVoidInfo);
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dtVoidInfo;
        }


        public static void RemoveUnusedDx(TransactionLines _oLineTransactions, ref StringBuilder sbDx)
        {
            //StringBuilder sbDx = new StringBuilder();
            try
            {

                for (int i = 0; i <= _oLineTransactions.Count - 1; i++)
                {

                    if (_oLineTransactions[i].Dx1Code != String.Empty)
                    {
                        if (sbDx.Length == 0)
                            sbDx.Append(_oLineTransactions[i].Dx1Code.ToString());
                        else
                        {
                            if (!sbDx.ToString().Contains(_oLineTransactions[i].Dx1Code.ToString()))
                                sbDx.Append("," + _oLineTransactions[i].Dx1Code.ToString());
                        }
                    }
                    if (_oLineTransactions[i].Dx2Code != String.Empty)
                    {
                        if (sbDx.Length == 0)
                            sbDx.Append(_oLineTransactions[i].Dx2Code.ToString());
                        else
                        {
                            if (!sbDx.ToString().Contains(_oLineTransactions[i].Dx2Code.ToString()))
                                sbDx.Append("," + _oLineTransactions[i].Dx2Code.ToString());
                        }
                    }
                    if (_oLineTransactions[i].Dx3Code != String.Empty)
                    {
                        if (sbDx.Length == 0)
                            sbDx.Append(_oLineTransactions[i].Dx3Code.ToString());
                        else
                        {
                            if (!sbDx.ToString().Contains(_oLineTransactions[i].Dx3Code.ToString()))
                                sbDx.Append("," + _oLineTransactions[i].Dx3Code.ToString());
                        }
                    }
                    if (_oLineTransactions[i].Dx4Code != String.Empty)
                    {
                        if (sbDx.Length == 0)
                            sbDx.Append(_oLineTransactions[i].Dx4Code.ToString());
                        else
                        {
                            if (!sbDx.ToString().Contains(_oLineTransactions[i].Dx4Code.ToString()))
                                sbDx.Append("," + _oLineTransactions[i].Dx4Code.ToString());
                        }
                    }
                    if (_oLineTransactions[i].Dx5Code != String.Empty)
                    {
                        if (sbDx.Length == 0)
                            sbDx.Append(_oLineTransactions[i].Dx5Code.ToString());
                        else
                        {
                            if (!sbDx.ToString().Contains(_oLineTransactions[i].Dx5Code.ToString()))
                                sbDx.Append("," + _oLineTransactions[i].Dx5Code.ToString());
                        }
                    }
                    if (_oLineTransactions[i].Dx6Code != String.Empty)
                    {
                        if (sbDx.Length == 0)
                            sbDx.Append(_oLineTransactions[i].Dx6Code.ToString());
                        else
                        {
                            if (!sbDx.ToString().Contains(_oLineTransactions[i].Dx6Code.ToString()))
                                sbDx.Append("," + _oLineTransactions[i].Dx6Code.ToString());
                        }
                    }
                    if (_oLineTransactions[i].Dx7Code != String.Empty)
                    {
                        if (sbDx.Length == 0)
                            sbDx.Append(_oLineTransactions[i].Dx7Code.ToString());
                        else
                        {
                            if (!sbDx.ToString().Contains(_oLineTransactions[i].Dx7Code.ToString()))
                                sbDx.Append("," + _oLineTransactions[i].Dx7Code.ToString());
                        }
                    }
                    if (_oLineTransactions[i].Dx8Code != String.Empty)
                    {
                        if (sbDx.Length == 0)
                            sbDx.Append(_oLineTransactions[i].Dx8Code.ToString());
                        else
                        {
                            if (!sbDx.ToString().Contains(_oLineTransactions[i].Dx8Code.ToString()))
                                sbDx.Append("," + _oLineTransactions[i].Dx8Code.ToString());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        public static DateTime GetMinDOS(Int64 nTransactionMasterID)
        {
            DateTime _dtMinDOS = DateTime.Now;
            Object _Result = null;
            gloDatabaseLayer.DBLayer oDB = null;

            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                string sqlstring = "SELECT min(nfromDate) as Dos FROM  BL_Transaction_Claim_Lines WHERE nTransactionMasterID='" + nTransactionMasterID + "'";
                oDB.Connect(false);
                _Result = oDB.ExecuteScalar_Query(sqlstring);
                oDB.Disconnect();
                if (_Result != null && Convert.ToString(_Result) != "")
                {
                    _dtMinDOS = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_Result));
                }

            }
            catch (SqlException sqlex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(sqlex.Message, true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return _dtMinDOS;
        }

        public static decimal FormatNumber(decimal Number)
        {
            Decimal _result = Number;
            try
            {
                String[] no = _result.ToString().Split('.');
                if (no.GetUpperBound(0) > 0)
                {
                    if (no[1].ToString().Length > 4)
                    {
                        no[1] = no[1].Substring(0, 4);
                    }
                    _result = Convert.ToDecimal(no[0] + "." + no[1]);
                }
                _result = Convert.ToDecimal(_result.ToString("0.####"));
                if (_result == 0)
                {
                    _result = 1;
                }
            }
            catch
            {
                _result = Number;
            }
            return _result;
        }

        public static bool IsClaimVoided(Int64 TransactionMasterId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            //  DataTable _dtVoidInfo = null;
            string strVoidInfoQuery = "";
            Object retValue = null;
            bool isVoided = false;

            try
            {
                strVoidInfoQuery =
                                " SELECT  ISNULL(bIsVoid,0) AS IsVoid " +
                                " FROM BL_Transaction_MST WITH (NOLOCK) " +
                                " WHERE BL_Transaction_MST.nTransactionID = " + TransactionMasterId + "";

                oDB.Connect(false);
                retValue = oDB.ExecuteScalar_Query(strVoidInfoQuery);
                oDB.Disconnect();

                if (retValue != null && Convert.ToString(retValue).Trim() != "")
                {
                    Boolean.TryParse(Convert.ToString(retValue), out isVoided);
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return isVoided;
        }

        public static bool IsClaimSplitted(Int64 TransactionMasterId, Int64 TransactionId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            // DataTable _dtVoidInfo = null;
            string strVoidInfoQuery = "";
            Object retValue = null;
            bool isSplitChildPresent = false;

            try
            {
                strVoidInfoQuery =
                                " SELECT COUNT(Claim.nTransactionID) AS ChildCount " +
                                " FROM BL_Transaction_Claim_MST Claim WITH (NOLOCK)  " +
                                " WHERE Claim.nTransactionMasterID = " + TransactionMasterId + " AND Claim.nParentTransactionID = " + TransactionId + " ";

                oDB.Connect(false);
                retValue = oDB.ExecuteScalar_Query(strVoidInfoQuery);
                oDB.Disconnect();

                if (retValue != null && Convert.ToString(retValue).Trim() != "")
                {
                    Boolean.TryParse(Convert.ToString(retValue), out isSplitChildPresent);
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return isSplitChildPresent;
        }

        public static bool IsClaimBilled(Int64 TransactionMasterId, Int64 InsuranceId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strIsClaimBilledQuery = "";
            Object retValue = null;
            bool isBilled = false;

            try
            {
                strIsClaimBilledQuery =
                                    " SELECT 1 AS IsBilled " +
                                    " FROM  dbo.BL_EOB_NextAction_HST WITH(NOLOCK) " +
                                    " WHERE  " +
                                    " sNextActionCode IN ('T', 'B')  " +
                                    " AND nBillingTransactionID = " + TransactionMasterId + "  " +
                                    " AND nNextActionPatientInsID = " + InsuranceId + "   ";

                oDB.Connect(false);
                retValue = oDB.ExecuteScalar_Query(strIsClaimBilledQuery);
                oDB.Disconnect();

                if (retValue != null && Convert.ToString(retValue).Trim() != "")
                {
                    bool isConversionSucessfull = Boolean.TryParse(retValue.ToString() == "1" ? "true" : "false", out isBilled);
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return isBilled;
        }

        #endregion

        #region " Notes "

        public static bool SaveClaimNotes(global::gloBilling.Common.GeneralNotes Notes)
        {



            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            int oResult = 0;
            bool _result = false;
            try
            {

                #region "Add Reserve Association Entry"

                oParameters.Clear();
                oParameters.Add("@nTransactionID", Notes[0].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nLineNo", Notes[0].TransactionLineId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionDetailID", Notes[0].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nNoteType", Notes[0].NoteType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nNoteId", Notes[0].NoteID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nNoteDateTime", Notes[0].NoteDate, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sNoteDescription", Notes[0].NoteDescription, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", Notes[0].UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", Notes[0].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nBillingNoteType", DBNull.Value, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nStatementNoteDate", Notes[0].StatementNoteDate, ParameterDirection.Input, SqlDbType.BigInt);
                if (Notes[0].NoteID != 0)
                {
                    oParameters.Add("@dtCreatedDateTime", Notes[0].dtCreatedDatetime, ParameterDirection.Input, SqlDbType.DateTime);
                }
                oDB.Connect(false);
                oResult = oDB.Execute("SaveClaimNotes", oParameters);
               
                oDB.Disconnect();

                #endregion

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            //   if (oResult != null)
            {
                if (oResult == 0)
                {
                    _result = false;
                }
                else
                {
                    _result = true;
                }
            }
            return _result;
        }

        public static bool SaveClaimNotes_Delete_Multiple(global::gloBilling.Common.GeneralNotes Notes, string sTransactionIDs)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            int oResult = 0;
            bool _result = false;
            try
            {

                #region "Add Reserve Association Entry"

                oParameters.Clear();
                oParameters.Add("@nTransactionID", Notes[0].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nLineNo", Notes[0].TransactionLineId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionDetailID", Notes[0].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nNoteType", Notes[0].NoteType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nNoteId", Notes[0].NoteID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nNoteDateTime", Notes[0].NoteDate, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sNoteDescription", Notes[0].NoteDescription, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", Notes[0].UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", Notes[0].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nBillingNoteType", DBNull.Value, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nStatementNoteDate", Notes[0].StatementNoteDate, ParameterDirection.Input, SqlDbType.BigInt);
                if (Notes[0].NoteID != 0)
                {
                    oParameters.Add("@dtCreatedDateTime", Notes[0].dtCreatedDatetime, ParameterDirection.Input, SqlDbType.DateTime);
                }
                oParameters.Add("@sTransactionIDs", sTransactionIDs, ParameterDirection.Input, SqlDbType.VarChar);

                oDB.Connect(false);
                oResult = oDB.Execute("SaveClaimNotes_Delete_Multiple", oParameters);
                oDB.Disconnect();

                #endregion

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            //   if (oResult != null)
            {
                if (oResult == 0)
                {
                    _result = false;
                }
                else
                {
                    _result = true;
                }
            }
            return _result;
        }

        public static bool SaveClaimNotes_Multiple(global::gloBilling.Common.GeneralNotes Notes, DataTable dtBatchPatAccountID)
        {



            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            int oResult = 0;
            bool _result = false;
            try
            {

                #region "Add Reserve Association Entry"

                oParameters.Clear();
                oParameters.Add("@nTransactionID", Notes[0].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nLineNo", Notes[0].TransactionLineId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionDetailID", Notes[0].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nNoteType", Notes[0].NoteType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nNoteId", Notes[0].NoteID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nNoteDateTime", Notes[0].NoteDate, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sNoteDescription", Notes[0].NoteDescription, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", Notes[0].UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", Notes[0].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCloseDate", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nBillingNoteType", DBNull.Value, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nStatementNoteDate", Notes[0].StatementNoteDate, ParameterDirection.Input, SqlDbType.BigInt);
                if (Notes[0].NoteID != 0)
                {
                    oParameters.Add("@dtCreatedDateTime", Notes[0].dtCreatedDatetime, ParameterDirection.Input, SqlDbType.DateTime);
                }
                oParameters.Add("@tvpLogSchedule_Multiple", dtBatchPatAccountID, ParameterDirection.Input, SqlDbType.Structured);
                oDB.Connect(false);
                oResult = oDB.Execute("SaveClaimNotes_Multiple", oParameters);
                oDB.Disconnect();

                #endregion

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            //   if (oResult != null)
            {
                if (oResult == 0)
                {
                    _result = false;
                }
                else
                {
                    _result = true;
                }
            }
            return _result;
        }

        public static DataTable GetClaimNotes(Int64 nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtPayNotes = null;

            try
            {
                #region"Commented code"
                //strQueryFinNotes = " SELECT ISNULL(BL_Transaction_Lines_Notes.nTransactionID,0) AS  nTransactionID, " +
                //              " ISNULL(BL_Transaction_Lines_Notes.nClinicID,0) AS  nClinicID,  " +
                //              " ISNULL(BL_Transaction_Lines_Notes.nNoteId,0) AS  nNoteId, " +
                //              " BL_Transaction_Lines_Notes.nNoteType AS NoteType, " +
                //              " dbo.CONVERT_TO_DATE(convert(varchar(50),BL_Transaction_Lines_Notes.nNoteDateTime)) AS  nNoteDateTime, " +
                //              " ISNULL(BL_Transaction_Lines_Notes.nUserID,0) AS  nUserID, " +
                //              " ISNULL(User_MST.sLoginName,'')  As sUserName, " +
                //              " ISNULL(BL_Transaction_Lines_Notes.sNoteDescription,'') AS  sNoteDescription ," +
                //              " nCloseDate,nStatementNoteDate,dtCreatedDateTime AS dtCreatedDateTime " +
                //              " FROM BL_Transaction_Lines_Notes WITH(NOLOCK)  " +
                //              " LEFT OUTER JOIN User_MST WITH(NOLOCK) on BL_Transaction_Lines_Notes.nUserID = User_MST.nUserID  " +
                //              " where (BL_Transaction_Lines_Notes.nNoteType = 12  OR BL_Transaction_Lines_Notes.nNoteType = 10 )AND BL_Transaction_Lines_Notes.nTransactionID = " + nTransactionID + " AND " +
                //              " BL_Transaction_Lines_Notes.nClinicID =  " + gloGlobal.gloPMGlobal.ClinicID;

                //oDB.Connect(false);
                //oDB.Retrive_Query(strQueryFinNotes, out _dtPayNotes);
                //oDB.Disconnect();
                #endregion

                oParameters.Clear();
                oParameters.Add("@nTransactionID", nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("nNotesType", 0, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("BL_Get_ClaimAndPaymentNotes", oParameters, out _dtPayNotes);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            return _dtPayNotes;
        }
        public static DataTable GetPaymentNotes(Int64 nMasterTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtPayNotes = null;
            try
            {
                #region"Commented code"
                //string strPayNotesQuery = " SELECT nEOBPaymentID, CASE ISNULL(BL_EOB_Notes.nPaymentNoteSubType,0) WHEN 5 THEN 15 WHEN 6 THEN 16 END AS  nNoteType, " +
                //                            " ISNULL(BL_EOB_Notes.nBillingTransactionID,0) AS  nTransactionID," +
                //                            " ISNULL(BL_EOB_Notes.nBillingTransactionDetailID,0) AS nTransactionDetailID, " +
                //                            " ISNULL(0,0) AS  nLineNo,         " +
                //                            " ISNULL(BL_EOB_Notes.nClinicID,0) AS  nClinicID, " +
                //                            " ISNULL(BL_EOB_Notes.nID,0)  AS  nNoteId," +
                //                            " ISNULL(BL_EOB_Notes.nDateTime,dbo.gloGetDate()) AS  nNoteDateTime, dbo.Convert_TO_DATE(BL_EOB_Notes.nCloseDate) AS  nCloseDate," +
                //                            " ISNULL(BL_EOB_Notes.nUserID,0) AS  nUserID,    " +
                //                            " ISNULL(User_MST.sLoginName,'') As sUserName, " +
                //                            " ISNULL(sNoteDescription,'') AS  sNoteDescription, ISNULL(sNoteCode,'') AS sNoteCode FROM " +
                //                            " BL_EOB_Notes  WITH (NOLOCK) LEFT OUTER JOIN User_MST  WITH (NOLOCK) ON BL_EOB_Notes.nUserID = User_MST.nUserID      " +
                //                            " WHERE   BL_EOB_Notes.nBillingTransactionID = " + nMasterTransactionID + "   AND  nPaymentNoteType =1 AND BL_EOB_Notes.nBillingTransactionID <> 0 AND ISNULL(BL_EOB_Notes.bIsVoid,0) = 0 AND nPaymentNoteSubType IN (5,6) ORDER BY BL_EOB_Notes.nID DESC";


                //oDB.Connect(false);
                //oDB.Retrive_Query(strPayNotesQuery, out _dtPayNotes);
                //oDB.Disconnect();
                #endregion

                oParameters.Clear();
                oParameters.Add("@nTransactionID", nMasterTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nNotesType", 1, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("BL_Get_ClaimAndPaymentNotes", oParameters, out _dtPayNotes);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            return _dtPayNotes;
        }
        public static DataTable GetPaymentNotes(Int64 nMasterTransactionID, StringBuilder sbTrasMStDtlID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtPayNotes = null;
            try
            {
                #region"Commented code"
                //string strPayNotesQuery = " SELECT nEOBPaymentID, CASE ISNULL(BL_EOB_Notes.nPaymentNoteSubType,0) WHEN 5 THEN 15 WHEN 6 THEN 16 END AS  nNoteType, " +
                //                            " ISNULL(BL_EOB_Notes.nBillingTransactionID,0) AS  nTransactionID," +
                //                            " ISNULL(BL_EOB_Notes.nBillingTransactionDetailID,0) AS nTransactionDetailID, " +
                //                            " ISNULL(0,0) AS  nLineNo,         " +
                //                            " ISNULL(BL_EOB_Notes.nClinicID,0) AS  nClinicID, " +
                //                            " ISNULL(BL_EOB_Notes.nID,0)  AS  nNoteId," +
                //                            " ISNULL(BL_EOB_Notes.nDateTime,dbo.gloGetDate()) AS  nNoteDateTime, dbo.Convert_TO_DATE(BL_EOB_Notes.nCloseDate) AS  nCloseDate," +
                //                            " ISNULL(BL_EOB_Notes.nUserID,0) AS  nUserID,    " +
                //                            " ISNULL(User_MST.sLoginName,'') As sUserName, " +
                //                            " ISNULL(sNoteDescription,'') AS  sNoteDescription, ISNULL(sNoteCode,'') AS sNoteCode FROM " +
                //                            " BL_EOB_Notes  WITH (NOLOCK) LEFT OUTER JOIN User_MST  WITH (NOLOCK) ON BL_EOB_Notes.nUserID = User_MST.nUserID      " +
                //                            " WHERE   BL_EOB_Notes.nBillingTransactionID = " + nMasterTransactionID + " AND BL_EOB_Notes.nBillingTransactionDetailID IN (" + sbTrasMStDtlID.ToString() + ") AND  nPaymentNoteType =1 AND BL_EOB_Notes.nBillingTransactionID <> 0 AND ISNULL(BL_EOB_Notes.bIsVoid,0) = 0 AND nPaymentNoteSubType IN (5,6) ORDER BY BL_EOB_Notes.nID DESC";


                //oDB.Connect(false);
                //oDB.Retrive_Query(strPayNotesQuery, out _dtPayNotes);
                //oDB.Disconnect();
                #endregion

                oParameters.Clear();
                oParameters.Add("@nTransactionID", nMasterTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nNotesType", 1, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sTransactionDetailID", sbTrasMStDtlID.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("BL_Get_ClaimAndPaymentNotes", oParameters, out _dtPayNotes);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            return _dtPayNotes;
        }

        public static DataTable GetVoidNotes(Int64 nTransactionID, Int64 nTransactionMasterDtlID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtPayNotes = null;
            try
            {


                #region "Commented Code"

                //string strVoidNotesQuery = " SELECT 0 as nEOBPaymentID  ,   " +
                //              " ISNULL(BL_Transaction_Lines_Notes.nBillingNoteType,0) AS  nNoteType," +
                //              " ISNULL(BL_Transaction_Lines_Notes.nTransactionID,0) AS  nTransactionID, " +
                //              " ISNULL(BL_Transaction_Lines_Notes.nTransactionDetailID,0) AS nTransactionDetailID," +
                //              " ISNULL(BL_Transaction_Lines_Notes.nLineNo,0) AS  nLineNo, " +
                //              " ISNULL(BL_Transaction_Lines_Notes.nClinicID,0) AS  nClinicID,  " +
                //              " ISNULL(BL_Transaction_Lines_Notes.nNoteId,0) AS  nNoteId, " +
                //              " dbo.CONVERT_TO_DATE(convert(varchar(50),BL_Transaction_Lines_Notes.nNoteDateTime)) AS  nNoteDateTime, " +
                //              " ISNULL(BL_Transaction_Lines_Notes.nUserID,0) AS  nUserID, " +
                //              " ISNULL(User_MST.sLoginName,'')  As sUserName, " +
                //              " ISNULL(BL_Transaction_Lines_Notes.sNoteDescription,'') AS  sNoteDescription, " +
                //              " BL_Transaction_Lines_Notes.nStatementNoteDate AS  nStatementNoteDate " +
                //              " FROM BL_Transaction_Lines_Notes WITH(NOLOCK)  " +
                //              " LEFT OUTER JOIN User_MST WITH(NOLOCK) on BL_Transaction_Lines_Notes.nUserID = User_MST.nUserID  " +
                //              " where ( BL_Transaction_Lines_Notes.nTransactionID = " + nTransactionID + " ) AND (BL_Transaction_Lines_Notes.nTransactionID <> 0 OR " +
                //              " BL_Transaction_Lines_Notes.nTransactionDetailID = " + nTransactionMasterDtlID + ") AND " +
                //              " BL_Transaction_Lines_Notes.nClinicID =  " + gloGlobal.gloPMGlobal.ClinicID + " AND nStatementNoteDate is Not Null  " +
                //              " AND BL_Transaction_Lines_Notes.nNoteType <> 12 " +
                //              " UNION " +
                //              " select nEOBPaymentID, CASE ISNULL(BL_EOB_Notes.nPaymentNoteSubType,0) WHEN 5 THEN 15 WHEN 6 THEN 16 END AS  nNoteType, " +
                //              " ISNULL(BL_EOB_Notes.nBillingTransactionID,0) AS  nTransactionID," +
                //              " ISNULL(BL_EOB_Notes.nBillingTransactionDetailID,0) AS nTransactionDetailID, " +
                //              " ISNULL(0,0) AS  nLineNo,         " +
                //              " ISNULL(BL_EOB_Notes.nClinicID,0) AS  nClinicID, " +
                //              " ISNULL(BL_EOB_Notes.nID,0)  AS  nNoteId," +
                //              " ISNULL(dbo.CONVERT_TO_DATE(BL_EOB_Notes.nCloseDate),CONVERT(VARCHAR,dbo.gloGetDate(),101)) AS  nNoteDateTime, " +
                //              " ISNULL(BL_EOB_Notes.nUserID,0) AS  nUserID,    " +
                //              " ISNULL(User_MST.sLoginName,'') As sUserName, " +
                //              " ISNULL(sNoteDescription,'') AS  sNoteDescription,BL_EOB_Notes.nCloseDate AS nStatementNoteDate  from BL_EOB_Notes WITH(NOLOCK)  LEFT OUTER JOIN User_MST WITH(NOLOCK) on BL_EOB_Notes.nUserID = User_MST.nUserID      " +
                //              " where   BL_EOB_Notes.nBillingTransactionID = " + nTransactionID + " AND  nPaymentNoteType =1 and BL_EOB_Notes.nBillingTransactionID <> 0";

                #endregion

                oParameters.Clear();
                oParameters.Add("@nTransactionID", nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionDetailID", nTransactionMasterDtlID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("BL_GetVoidNotes", oParameters, out _dtPayNotes);
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            return _dtPayNotes;
        }

        public static DataTable GetFiniancialViewNotes(Int64 nTransactionID, Int64 nTransactionMasterDtlID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtPayNotes = null;
            string strQueryFinNotes = "";
            try
            {

                strQueryFinNotes = " SELECT 0 as nEOBPaymentID  ,   " +
                              " ISNULL(BL_Transaction_Lines_Notes.nBillingNoteType,0) AS  nNoteType," +
                              " ISNULL(BL_Transaction_Lines_Notes.nTransactionID,0) AS  nTransactionID, " +
                              " ISNULL(BL_Transaction_Lines_Notes.nTransactionDetailID,0) AS nTransactionDetailID," +
                              " ISNULL(BL_Transaction_Lines_Notes.nLineNo,0) AS  nLineNo, " +
                              " ISNULL(BL_Transaction_Lines_Notes.nClinicID,0) AS  nClinicID,  " +
                              " ISNULL(BL_Transaction_Lines_Notes.nNoteId,0) AS  nNoteId, " +
                              " dbo.CONVERT_TO_DATE(convert(varchar(50),BL_Transaction_Lines_Notes.nNoteDateTime)) AS  nNoteDateTime, " +
                              " ISNULL(BL_Transaction_Lines_Notes.nUserID,0) AS  nUserID, " +
                              " ISNULL(User_MST.sLoginName,'')  As sUserName, " +
                              " ISNULL(BL_Transaction_Lines_Notes.sNoteDescription,'') AS  sNoteDescription " +
                              " FROM BL_Transaction_Lines_Notes WITH(NOLOCK)  " +
                              " LEFT OUTER JOIN User_MST WITH(NOLOCK) on BL_Transaction_Lines_Notes.nUserID = User_MST.nUserID  " +
                              " where BL_Transaction_Lines_Notes.nTransactionID = " + nTransactionID + " or " +
                              " BL_Transaction_Lines_Notes.nTransactionDetailID = " + nTransactionMasterDtlID + " AND " +
                              " BL_Transaction_Lines_Notes.nClinicID =  " + gloGlobal.gloPMGlobal.ClinicID;

                oDB.Connect(false);
                oDB.Retrive_Query(strQueryFinNotes, out _dtPayNotes);
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dtPayNotes;
        }


        #endregion

        #region " Get cached data Methods "

        public static DataTable GetCachedFacilities()
        {
            DataTable _dtFacilities = null;
            try
            {
                _dtFacilities = gloGlobal.gloPMMasters.GetFacilities();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return _dtFacilities;
        }

        public static DataTable GetCachedChargeTrays()
        {
            DataTable _dtChargeTrays = null;

            try
            {
                _dtChargeTrays = gloGlobal.gloPMMasters.GetChargesTrays();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return _dtChargeTrays;
        }

        public static DataTable GetCachedProviders()
        {
            DataTable _dtProviders = null;

            try
            {
                _dtProviders = gloGlobal.gloPMMasters.GetProviders();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return _dtProviders;
        }

        public static DataTable GetCachedAllProviders()
        {
            DataTable _dtProviders = null;

            try
            {
                _dtProviders = gloGlobal.gloPMMasters.GetAllProviders();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return _dtProviders;
        }

        #endregion

        #region "EMR Treatment Methods"

        public static gloSettings.ExternalChargesType GetEMRTreatmentSourceSetting()
        {
            gloSettings.ExternalChargesType _Result = gloSettings.ExternalChargesType.gloEMRTreatment;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                string _Query = "SELECT isnull(sSettingsValue,1) AS sSettingsValue FROM Settings WITH (NOLOCK) WHERE sSettingsName='EMRTreatmentSource'";
                Object oResult;
                oDB.Connect(false);
                oResult = oDB.ExecuteScalar_Query(_Query);
                oDB.Disconnect();
                if (oResult != null && Convert.ToString(oResult) != "")

                    _Result = (gloSettings.ExternalChargesType)Convert.ToInt32(oResult);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }
            }
            return _Result;

        }

        public static DataTable GetEMRExams(gloSettings.ExternalChargesType enumChargeType)
        {
            DataTable _dtPatientExams = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDBParameters.Add("@nTreatmentType", enumChargeType.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_PatientExams", oDBParameters, out _dtPatientExams);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
            return _dtPatientExams;
        }

        public static string ValidateEMRExam(Int64 nEMRExamID, Boolean IsICD9Driven, Int32 nMaxDiagnosis, Int32 nMaxServiceLines, gloSettings.ExternalChargesType nEMRTreatmentType)
        {
            String _Message = String.Empty;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataSet dsChargesData = new DataSet();
            DataTable _dt = null;

            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nExamID", nEMRExamID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTreatmentType", nEMRTreatmentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oDB.Connect(false);
                oDB.Retrive("BL_Get_ValidateEMRExam_Data", oParameters, out dsChargesData);
                oDB.Disconnect();

                if (nEMRTreatmentType == gloSettings.ExternalChargesType.gloEMRTreatment)
                {
                    if (IsICD9Driven)
                    {
                        _dt = dsChargesData.Tables[0];
                        if (_dt.Rows.Count > nMaxServiceLines)
                        {
                            _Message = Environment.NewLine + "More than " + nMaxServiceLines + " CPTs per Claim.";
                        }
                    }
                    else // For CPT Driven Don't use Distinct else it will skip one service line
                    {
                        _dt = dsChargesData.Tables[1];

                        if (_dt.Rows.Count > 0 && _dt.Rows[0]["Line_Count"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt16(_dt.Rows[0]["Line_Count"]) > nMaxServiceLines)
                            {
                                _Message = Environment.NewLine + "More than " + nMaxServiceLines + " CPTs per Claim.";
                            }
                        }

                    }
                }
                else if (nEMRTreatmentType == gloSettings.ExternalChargesType.HL7InboundCharges)
                {
                    _dt = dsChargesData.Tables[1];
                    if (_dt.Rows.Count > 0 && _dt.Rows[0]["Line_Count"] != System.DBNull.Value)
                    {
                        if (Convert.ToInt16(_dt.Rows[0]["Line_Count"]) > nMaxServiceLines)
                        {
                            _Message = Environment.NewLine + "More than " + nMaxServiceLines + " CPTs per Claim.";
                        }
                    }
                }

                #region "More than 4 Diagnoses per Claim"

                _dt = null;
                //More than 4 Diagnoses per Claim
                _dt = dsChargesData.Tables[2];
                if (_dt.Rows.Count > nMaxDiagnosis)
                {
                    _Message += Environment.NewLine + "More than " + nMaxDiagnosis + " Diagnosis per Claim.";
                }

                #endregion


                #region "More than 4 Diagnoses per Charge CPT with no Diagnosis"

                _dt = null;
                //More than 4 Diagnoses per Charge
                //CPT with no Diagnosis
                _dt = dsChargesData.Tables[3];

                for (int iCount = 0; iCount <= _dt.Rows.Count - 1; iCount++)
                {
                    if (Convert.ToInt16(_dt.Rows[iCount]["DxCount"]) > nMaxDiagnosis)
                    {
                        _Message += Environment.NewLine + "More than " + nMaxDiagnosis + " Diagnosis per Charge.";
                        break;
                    }
                }

                for (int iCount = 0; iCount <= _dt.Rows.Count - 1; iCount++)
                {
                    if (Convert.ToInt16(_dt.Rows[iCount]["DxCount"]) == 0)
                    {
                        _Message += Environment.NewLine + "CPT with no Diagnosis.";
                        break;
                    }
                }

                #endregion "More than 4 Diagnoses per Charge CPT with no Diagnosis"


                #region "More than 2 Modifiers per Charge"

                _dt = null;
                //More than 2 Modifiers per Charge
                _dt = dsChargesData.Tables[4];

                for (int iCount = 0; iCount <= _dt.Rows.Count - 1; iCount++)
                {
                    if (Convert.ToInt16(_dt.Rows[iCount]["ModCount"]) > 4)
                    {
                        _Message += Environment.NewLine + "More than 4 Modifiers per Charge.";
                        break;
                    }
                }
                #endregion "More than 2 Modifiers per Charge"


                #region "Diagnosis with no CPT"

                _dt = null;
                //Diagnosis with no CPT
                _dt = dsChargesData.Tables[5];

                for (int iCount = 0; iCount <= _dt.Rows.Count - 1; iCount++)
                {
                    if (Convert.ToInt16(_dt.Rows[iCount]["CPTCount"]) == 0)
                    {
                        _Message += Environment.NewLine + "Diagnosis with no CPT.";
                        break;
                    }
                }

                #endregion "Diagnosis with no CPT"


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return _Message;
        }

        public static string ValidateEMRExamOnDXCPTWindow(TransactionLines oPreviouslyBilledCPTS, TransactionLines oEMRTransactionBilledCPTS, Int64 nEMRExamID, Boolean IsICD9Driven, Int32 nMaxDiagnosis, Int32 nMaxServiceLines, gloSettings.ExternalChargesType nEMRTreatmentType, DataTable dtLoadedCPTS, TransactionLines oLinesAlreadyLoaded)
        {
            DataTable _dtDistinctRecords = null;
            DataTable _dtUnique = null;
            int InitialRows = oEMRTransactionBilledCPTS.Count;
            String _Message = String.Empty;
            try
            {
                if (oPreviouslyBilledCPTS != null && oPreviouslyBilledCPTS.Count > 0)
                {
                    for (int i = 0; i < oPreviouslyBilledCPTS.Count; i++)
                    {
                        oEMRTransactionBilledCPTS.Add(oPreviouslyBilledCPTS[i]);
                    }
                }
                if (oEMRTransactionBilledCPTS.Count <= 0)
                {
                    oEMRTransactionBilledCPTS = oLinesAlreadyLoaded;
                }
                DataTable dtActual = null;
                DataRow[] drFilter;
                dtActual = new DataTable();
                dtActual.Columns.Add("DxCode");
                dtActual.Columns.Add("MOD");
                dtActual.Columns.Add("nExamLineNo");
                dtActual.Columns.Add("sCPTCode");

                for (int i = 0; i <= oEMRTransactionBilledCPTS.Count - 1; i++)
                {

                    #region " Diagnosis"

                    if (Convert.ToString(oEMRTransactionBilledCPTS[i].Dx1Code) != "")
                    {
                        DataRow _drInsert = dtActual.NewRow();
                        _drInsert["DxCode"] = oEMRTransactionBilledCPTS[i].Dx1Code;
                        _drInsert["nExamLineNo"] = oEMRTransactionBilledCPTS[i].EMRTreatmentLineNo;
                        _drInsert["MOD"] = "";
                        _drInsert["sCPTCode"] = oEMRTransactionBilledCPTS[i].CPTCode;
                        dtActual.Rows.Add(_drInsert);
                        dtActual.AcceptChanges();
                    }
                    if (Convert.ToString(oEMRTransactionBilledCPTS[i].Dx2Code) != "")
                    {
                        DataRow _dr = dtActual.NewRow();
                        _dr["DxCode"] = oEMRTransactionBilledCPTS[i].Dx2Code;
                        _dr["nExamLineNo"] = oEMRTransactionBilledCPTS[i].EMRTreatmentLineNo;
                        _dr["MOD"] = "";
                        _dr["sCPTCode"] = oEMRTransactionBilledCPTS[i].CPTCode;
                        dtActual.Rows.Add(_dr);
                        dtActual.AcceptChanges();
                    }
                    if (Convert.ToString(oEMRTransactionBilledCPTS[i].Dx3Code) != "")
                    {

                        DataRow _dr = dtActual.NewRow();
                        _dr["DxCode"] = oEMRTransactionBilledCPTS[i].Dx3Code;
                        _dr["nExamLineNo"] = oEMRTransactionBilledCPTS[i].EMRTreatmentLineNo;
                        _dr["MOD"] = "";
                        _dr["sCPTCode"] = oEMRTransactionBilledCPTS[i].CPTCode;
                        dtActual.Rows.Add(_dr);
                        dtActual.AcceptChanges();

                    }
                    if (Convert.ToString(oEMRTransactionBilledCPTS[i].Dx4Code) != "")
                    {
                        DataRow _dr = dtActual.NewRow();
                        _dr["DxCode"] = oEMRTransactionBilledCPTS[i].Dx4Code;
                        _dr["nExamLineNo"] = oEMRTransactionBilledCPTS[i].EMRTreatmentLineNo;
                        _dr["MOD"] = "";
                        _dr["sCPTCode"] = oEMRTransactionBilledCPTS[i].CPTCode;
                        dtActual.Rows.Add(_dr);
                        dtActual.AcceptChanges();
                    }
                    if (Convert.ToString(oEMRTransactionBilledCPTS[i].Dx5Code) != "")
                    {
                        DataRow _dr = dtActual.NewRow();
                        _dr["DxCode"] = oEMRTransactionBilledCPTS[i].Dx5Code;
                        _dr["nExamLineNo"] = oEMRTransactionBilledCPTS[i].EMRTreatmentLineNo;
                        _dr["MOD"] = "";
                        _dr["sCPTCode"] = oEMRTransactionBilledCPTS[i].CPTCode;
                        dtActual.Rows.Add(_dr);
                        dtActual.AcceptChanges();
                    }
                    if (Convert.ToString(oEMRTransactionBilledCPTS[i].Dx6Code) != "")
                    {
                        DataRow _dr = dtActual.NewRow();
                        _dr["DxCode"] = oEMRTransactionBilledCPTS[i].Dx6Code;
                        _dr["nExamLineNo"] = oEMRTransactionBilledCPTS[i].EMRTreatmentLineNo;
                        _dr["MOD"] = "";
                        _dr["sCPTCode"] = oEMRTransactionBilledCPTS[i].CPTCode;
                        dtActual.Rows.Add(_dr);
                        dtActual.AcceptChanges();


                    }
                    if (Convert.ToString(oEMRTransactionBilledCPTS[i].Dx7Code) != "")
                    {
                        DataRow _dr = dtActual.NewRow();
                        _dr["DxCode"] = oEMRTransactionBilledCPTS[i].Dx7Code;
                        _dr["nExamLineNo"] = oEMRTransactionBilledCPTS[i].EMRTreatmentLineNo;
                        _dr["MOD"] = "";
                        _dr["sCPTCode"] = oEMRTransactionBilledCPTS[i].CPTCode;
                        dtActual.Rows.Add(_dr);
                        dtActual.AcceptChanges();

                    }
                    if (Convert.ToString(oEMRTransactionBilledCPTS[i].Dx8Code) != "")
                    {
                        DataRow _dr = dtActual.NewRow();
                        _dr["DxCode"] = oEMRTransactionBilledCPTS[i].Dx8Code;
                        _dr["nExamLineNo"] = oEMRTransactionBilledCPTS[i].EMRTreatmentLineNo;
                        _dr["MOD"] = "";
                        _dr["sCPTCode"] = oEMRTransactionBilledCPTS[i].CPTCode;
                        dtActual.Rows.Add(_dr);
                        dtActual.AcceptChanges();

                    }

                    #endregion

                    #region "Modifiers"

                    if (Convert.ToString(oEMRTransactionBilledCPTS[i].Mod1Code) != "")
                    {

                        DataRow _dr = dtActual.NewRow();
                        _dr["DxCode"] = "";
                        _dr["MOD"] = oEMRTransactionBilledCPTS[i].Mod1Code;
                        _dr["nExamLineNo"] = oEMRTransactionBilledCPTS[i].EMRTreatmentLineNo;
                        dtActual.Rows.Add(_dr);
                        dtActual.AcceptChanges();


                    }
                    if (Convert.ToString(oEMRTransactionBilledCPTS[i].Mod2Code) != "")
                    {


                        DataRow _dr = dtActual.NewRow();
                        _dr["DxCode"] = "";
                        _dr["MOD"] = oEMRTransactionBilledCPTS[i].Mod2Code;
                        _dr["nExamLineNo"] = oEMRTransactionBilledCPTS[i].EMRTreatmentLineNo;
                        dtActual.Rows.Add(_dr);
                        dtActual.AcceptChanges();


                    }
                    if (Convert.ToString(oEMRTransactionBilledCPTS[i].Mod3Code) != "")
                    {


                        DataRow _dr = dtActual.NewRow();
                        _dr["DxCode"] = "";
                        _dr["MOD"] = oEMRTransactionBilledCPTS[i].Mod3Code;
                        _dr["nExamLineNo"] = oEMRTransactionBilledCPTS[i].EMRTreatmentLineNo;
                        dtActual.Rows.Add(_dr);
                        dtActual.AcceptChanges();


                    }
                    if (Convert.ToString(oEMRTransactionBilledCPTS[i].Mod4Code) != "")
                    {

                        DataRow _dr = dtActual.NewRow();
                        _dr["DxCode"] = "";
                        _dr["MOD"] = oEMRTransactionBilledCPTS[i].Mod4Code;
                        _dr["nExamLineNo"] = oEMRTransactionBilledCPTS[i].EMRTreatmentLineNo;
                        dtActual.Rows.Add(_dr);
                        dtActual.AcceptChanges();


                    }

                    #endregion

                }


                if (oEMRTransactionBilledCPTS.Count > nMaxServiceLines)
                {
                    _Message = Environment.NewLine + "More than " + nMaxServiceLines + " CPTs per Claim.";

                }

                if (oPreviouslyBilledCPTS != null && oPreviouslyBilledCPTS.Count > 0)
                {
                    for (int i = oEMRTransactionBilledCPTS.Count - 1; i >= InitialRows; i--)
                    {
                        oEMRTransactionBilledCPTS.RemoveAt(i);
                    }
                }

                #region "More than 4 Diagnoses per Claim"

                _dtDistinctRecords = null;
                _dtDistinctRecords = dtActual.DefaultView.ToTable(true, "DxCode");
                if (_dtDistinctRecords != null && _dtDistinctRecords.Rows.Count > 0)
                {
                    drFilter = _dtDistinctRecords.Select("DxCode <> ''");
                    if (drFilter != null && drFilter.Length > 0)
                    {
                        _dtUnique = _dtDistinctRecords.Clone();
                        foreach (DataRow drFilterRow in drFilter)
                        {
                            _dtUnique.ImportRow(drFilterRow);

                        }
                        _dtUnique.AcceptChanges();
                    }

                    //More than 4 Diagnoses per Claim


                    if (_dtUnique != null && _dtUnique.Rows.Count > nMaxDiagnosis)
                    {
                        _Message += Environment.NewLine + "More than " + nMaxDiagnosis + " Diagnosis per Claim.";
                    }
                }

                #endregion


                #region "More than 4 Diagnoses per Charge CPT with no Diagnosis"

                DataTable dtlineNo = dtActual.DefaultView.ToTable(true, "nExamLineNo");

                _dtDistinctRecords = null;
                drFilter = null;
                _dtUnique = null;
                //More than 4 Diagnoses per Charge
                //CPT with no Diagnosis

                for (int iCount = 0; iCount <= dtlineNo.Rows.Count - 1; iCount++)
                {
                    _dtUnique = new DataTable();
                    _dtUnique = dtActual.Clone();
                    drFilter = dtActual.Select("DxCode <> '' and nExamLineNo=" + dtlineNo.Rows[iCount]["nExamLineNo"]);
                    if (drFilter != null && drFilter.Length > 0)
                    {
                        foreach (DataRow drFilterRow in drFilter)
                        {
                            _dtUnique.ImportRow(drFilterRow);
                        }
                    }
                    _dtUnique.AcceptChanges();
                    if (_dtUnique != null && _dtUnique.Rows.Count > 0)
                    {
                        _dtDistinctRecords = _dtUnique.DefaultView.ToTable(true, "DxCode");
                        if (_dtDistinctRecords.Rows.Count > nMaxDiagnosis)
                        {
                            _Message += Environment.NewLine + "More than " + nMaxDiagnosis + " Diagnosis per Charge.";
                            break;
                        }
                        if (_dtDistinctRecords.Rows.Count == 0)
                        {
                            _Message += Environment.NewLine + "CPT with no Diagnosis.";
                            break;
                        }
                    }
                    _dtUnique = null;
                }



                #endregion "More than 4 Diagnoses per Charge CPT with no Diagnosis"


                #region "More than 4 Modifiers per Charge"

                _dtDistinctRecords = null;
                _dtUnique = null;
                drFilter = null;
                //More than 2 Modifiers per Charge


                for (int iCount = 0; iCount <= dtlineNo.Rows.Count - 1; iCount++)
                {
                    _dtUnique = new DataTable();
                    _dtUnique = dtActual.Clone();
                    drFilter = dtActual.Select("MOD <> '' and nExamLineNo=" + dtlineNo.Rows[iCount]["nExamLineNo"]);
                    if (drFilter != null && drFilter.Length > 0)
                    {
                        foreach (DataRow datarow in drFilter)
                        {
                            _dtUnique.ImportRow(datarow);
                        }
                    }
                    _dtUnique.AcceptChanges();
                    if (_dtUnique != null && _dtUnique.Rows.Count > 0)
                    {
                        _dtDistinctRecords = _dtUnique.DefaultView.ToTable(true, "DxCode");
                        if (_dtDistinctRecords.Rows.Count > 4)
                        {
                            _Message += Environment.NewLine + "More than 4 Modifiers per Charge.";
                            break;
                        }
                    }
                    _dtUnique = null;
                }
                #endregion "More than 2 Modifiers per Charge"

                #region "Diagnosis with no CPT"


                //Diagnosis with no CPT
                drFilter = dtActual.Select("sCPTCode = ''");
                if (drFilter != null && drFilter.Length > 0)
                {
                    _Message += Environment.NewLine + "Diagnosis with no CPT.";
                }

                #endregion "Diagnosis with no CPT"

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {

            }
            return _Message;
        }

        public static bool IsValidCPT(string CPTCode)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dt = new DataTable();
            bool _returnvalue = false;
            Object count = 0;
            string sqlQuery = " SELECT COUNT(nCPTID) FROM CPT_MST WHERE sCPTCode = '" + CPTCode.Replace("'", "''") + "'";
            try
            {
                ODB.Connect(false);
                count = ODB.ExecuteScalar_Query(sqlQuery);
                ODB.Disconnect();
                if (Convert.ToInt64(count) > 0)
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
                ODB.Dispose();

            }
            return _returnvalue;

        }

        public static bool IsValidDxCodeForBilling(String sDxCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool _returnvalue = false;
            try
            {
                oParameters.Add("@sICD9Code", sDxCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@bIsValidIcd", _returnvalue, ParameterDirection.Output, SqlDbType.Bit);

                oDB.Connect(false);
                //oDB.Execute("SaveCharges_TVP", oParameters, out  _oTransactionMstID, out _oNewClaimNo);
                Hashtable oOUT = oDB.Execute("BL_IsValidICDForBilling", oParameters, true);
                if (oOUT != null)
                {
                    if (oOUT["@bIsValidIcd"] != DBNull.Value)
                        _returnvalue = Convert.ToBoolean(oOUT["@bIsValidIcd"]);
                }
                oDB.Disconnect();
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
            return _returnvalue;

        }

        public static bool IsValidDX(string sDxCode)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dt = new DataTable();
            bool _returnvalue = false;
            Object count = 0;

            try
            {
                string sqlQuery = " SELECT COUNT(nICD9ID) FROM ICD9 WHERE sICD9Code =  '" + sDxCode.Replace("'", "''") + "'";
                ODB.Connect(false);
                count = ODB.ExecuteScalar_Query(sqlQuery);
                ODB.Disconnect();
                if (Convert.ToInt64(count) > 0)
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
                ODB.Dispose();

            }
            return _returnvalue;

        }

        public static bool IsValidModifier(string sMod)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dt = new DataTable();
            bool _returnvalue = false;
            Object count = 0;
            string sqlQuery = " SELECT COUNT(nModifierID) FROM Modifier_MST WHERE sModifierCode = '" + sMod.Replace("'", "''") + "'";
            try
            {
                ODB.Connect(false);
                count = ODB.ExecuteScalar_Query(sqlQuery);
                ODB.Disconnect();
                if (Convert.ToInt64(count) > 0)
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
                ODB.Dispose();

            }
            return _returnvalue;

        }

        public static bool VoidExternalCharges(Int64 BillingTransactionID, Int64 EMRExamID, Int64 EMRVisitID, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool _result = false;

            try
            {
                oDB.Connect(false);
                oParameters.Add("@EMRExamID", EMRExamID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEMRVisitID", EMRVisitID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@EMRTreatmentType", ExternalChargesType.HL7InboundCharges.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                int Val = oDB.Execute("gsp_VoidExternalEMRTreatmentCharges", oParameters);
                _result = true;
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                _result = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _result = false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return _result;
        }


        #endregion

        #region "GetCurrentPartyNumber"

        public static DataTable GetCurrentPartyNumber(Int64 _MasterTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            DataTable dtPartyNo = null;
            try
            {
                oDB.Connect(false);
                strQuery = "select ISNULL(max(nNextActionPartyNumber),0) AS PartyNo,isnull(sNextActionCode,'') as sNextActionCode  from BL_EOB_NextAction WITH (NOLOCK) where nBillingTransactionID = " + _MasterTransactionID + " group by nNextActionPartyNumber,sNextActionCode order by nNextActionPartyNumber desc";
                oDB.Retrive_Query(strQuery, out dtPartyNo);

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); }
            }

            return dtPartyNo;

        }

        public static DataTable GetCurrentPartyNumber(Int64 TrnID, Int64 _MasterTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            //  string strQuery = "";
            DataTable dtPartyNo = null;
            try
            {
                //...Start - Changes done by Sagar Ghodke
                //...Changed done for "Claim Line Missing Scenario Fix: Change the fetch logic and moved to stored procedure"
                //...If claim is splitted and opened then current resp. is not found in BL_EOB_NExtAction so we looked for the same in history


                //strQuery = "SELECT ISNULL(BL_Transaction_Claim_MST.nResponsibilityNo, 1) AS PartyNo, ISNULL(BL_EOB_NextAction.sNextActionCode, 'T') AS sNextActionCode, " +
                //               " BL_EOB_NextAction.nBillingTransactionDetailID " +
                //               " FROM BL_Transaction_Claim_MST WITH (NOLOCK) LEFT OUTER JOIN " +
                //               " BL_EOB_NextAction WITH (NOLOCK) ON BL_Transaction_Claim_MST.nTransactionMasterID = BL_EOB_NextAction.nBillingTransactionID AND  " +
                //               " BL_Transaction_Claim_MST.nResponsibilityNo = BL_EOB_NextAction.nNextActionPartyNumber " +
                //                " where BL_Transaction_Claim_MST.nTransactionMasterID=" + _MasterTransactionID +
                //                " and BL_Transaction_Claim_MST.nTransactionID=" + TrnID +
                //                " order by BL_EOB_NextAction.nBillingTransactionDetailID ";

                //oDB.Connect(false);
                //oDB.Retrive_Query(strQuery, out dtPartyNo);

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@TransactionMasterID", _MasterTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@TransactionID", TrnID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("BL_GET_CurrentPartyNumber", oParameters, out dtPartyNo);
                //...End - Changes done by Sagar Ghodke
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }

            return dtPartyNo;

        }

        public static DataTable GetFollowUpSchedule(Int64 TrnID, Int64 _MasterTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            DataTable dtSchedule = null;
            try
            {
                strQuery = "SELECT nID as nScheduleId,nAccountID, nPatientID,dtClaimFollowUpDate from CL_FollowupSchedule_Claim  " +
                                " where CL_FollowupSchedule_Claim.nTransactionMasterID=" + _MasterTransactionID +
                                " and nTransactionID=" + TrnID;

                oDB.Connect(false);
                oDB.Retrive_Query(strQuery, out dtSchedule);

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); }
            }

            return dtSchedule;

        }

        public static DataTable GetNextAction(Int64 _MasterTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            DataTable dtNextAction = null;
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT DISTINCT sNextActionCode,ISNULL(nNextPartyType,0) AS nNextPartyType FROM BL_EOB_NextAction WITH (NOLOCK) where nBillingTransactionID= " + _MasterTransactionID + " ";
                oDB.Retrive_Query(strQuery, out dtNextAction);

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); }
            }

            return dtNextAction;
        }

        public static DataTable IsPartyChanged(Int64 _nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            DataTable dtNextAction = null;
            try
            {
                oDB.Connect(false);
                strQuery = "Select nNextActionPatientInsName,nNextActionPartyNumber,nNextActionContactID,sNextActionCode , nNextActionPatientInsID  from BL_EOB_NextAction WITH (NOLOCK) where nTrackMstTrnID = " + _nTransactionID;
                oDB.Retrive_Query(strQuery, out dtNextAction);

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); }
            }

            return dtNextAction;
        }

        public static DataTable IsMasterClaimPartyChanged(Int64 _nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            DataTable dtContact = null;
            try
            {
                oDB.Connect(false);
                strQuery = "Select nContactID from BL_Transaction_Claim_MST WITH (NOLOCK) where nTransactionID = " + _nTransactionID;
                oDB.Retrive_Query(strQuery, out dtContact);

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); }
            }

            return dtContact;
        }

        public static Boolean IsSamePlan(Int64 nCurrentInsuranceID, Int64 nExistingInsuranceID)
        {
            Boolean _IsSamePlan = false;
            gloDatabaseLayer.DBLayer oDB = null;
            DataTable dtPlans = null;
            try
            {

                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                string sqlstring = "SELECT DISTINCT sSubscriberID,sSubscriberName,dtDOB,sGroup, ISNULL(nInsuranceFlag,0) AS nInsuranceFlag FROM PatientInsurance_DTL WITH (NOLOCK) WHERE nInsuranceID IN(" + nCurrentInsuranceID + "," + nExistingInsuranceID + ")";
                oDB.Connect(false);
                oDB.Retrive_Query(sqlstring, out dtPlans);
                oDB.Disconnect();
                if (dtPlans!=null && dtPlans.Rows.Count == 1 && Convert.ToInt16(dtPlans.Rows[0]["nInsuranceFlag"])!=0)
                {
                    _IsSamePlan = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return _IsSamePlan;
        }



        #endregion

        #region "Save Charges TVP Methods "
        public static DataTable getCPTToDistributeCopay(dsChargesTVP dsChargesTVP, decimal dTotalAvailableCopayReserve)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dt = null;

            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@tvpClaim_Lines", dsChargesTVP.Tables["BL_Transaction_Claim_Lines"], ParameterDirection.Input, SqlDbType.Structured);
                oParameters.Add("@dTotalAvailableCopayReserve", dTotalAvailableCopayReserve, ParameterDirection.Input, SqlDbType.Decimal);
                oDB.Connect(false);
                oDB.Retrive("BL_getCPTToDistributeCopay", oParameters, out _dt);
                return _dt;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            //return _dt;
        }

        public static DataSet AutoDistributeCopayDOSWise(Int64 nPatientID, Int64 nPAccountID, dsChargesTVP dsChargesTVP)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataSet _ds = null;

            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@tvpClaim_Lines", dsChargesTVP.Tables["BL_Transaction_Claim_Lines"], ParameterDirection.Input, SqlDbType.Structured);
                oDB.Connect(false);
                oDB.Retrive("BL_AutoDistributeCopayDos", oParameters, out _ds);
                return _ds;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            //return _dt;
        }



        public static decimal getPatientTotalCoPayReserve(Int64 nPatientID, Int64 nPAccountID, DateTime dtCloseDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dt = null;
            decimal _Amount = 0;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@dtStartDate", dtCloseDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@dtEndDate", dtCloseDate, ParameterDirection.Input, SqlDbType.Date);
                oDB.Connect(false);
                oDB.Retrive("BL_getPatientTotalCoPayReserve", oParameters, out _dt);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    _Amount = Convert.ToDecimal(_dt.Rows[0]["AvailableReserve"]);
                }
                return _Amount;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            // return _Amount;
        }
        public static Int64 SaveClaim(dsChargesTVP dsChargesTVP, Int64 nEMRExamID, Int64 nEMRVisitID, out Int64 _nextClaimNo, out Int64 _nTransactionID, String sEMRTreatmentLineNos, String sPostedCPTS, String sPostedLineNo, DataTable _dtNoPostCharges, bool _bIsSaveClose, DataTable _dtAppointmentLink)
        {
            Int64 TransactionMasterID = 0;
            Int64 TransactionID = 0;
            _nextClaimNo = 0;
            _nTransactionID = 0;
            string sTransResult = "";
            object _oTransactionMstID = null;
            object _oNewClaimNo = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                if (dsChargesTVP.BL_Transaction_Claim_MST != null && dsChargesTVP.BL_Transaction_Claim_MST.Rows.Count > 0)
                {

                    oParameters.Add("@tvpClaimMST", dsChargesTVP.Tables["BL_Transaction_Claim_MST"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpClaim_Lines", dsChargesTVP.Tables["BL_Transaction_Claim_Lines"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpClaimDx", dsChargesTVP.Tables["BL_Transaction_Diagnosis"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpClaim_Lines_Notes", dsChargesTVP.Tables["BL_Transaction_Lines_Notes"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpClaim_InsurancePlans", dsChargesTVP.Tables["BL_Transaction_InsPlan"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpNextAction", dsChargesTVP.Tables["BL_EOB_NextAction"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpUB04Data", dsChargesTVP.Tables["UB04Data"], ParameterDirection.Input, SqlDbType.Structured);
                    if (nEMRExamID > 0)
                    {
                        oParameters.Add("@EMRExamID", nEMRExamID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@EMRVisitID", nEMRVisitID, ParameterDirection.Input, SqlDbType.BigInt);
                    }

                    oParameters.Add("@nTransactionMasterID", TransactionMasterID, ParameterDirection.Output, SqlDbType.BigInt);
                    oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Output, SqlDbType.BigInt);
                    oParameters.Add("@sTranResult", sTransResult, ParameterDirection.Output, SqlDbType.VarChar, 1000);
                    oParameters.Add("@sEMRTreatmentLineNos", sEMRTreatmentLineNos, ParameterDirection.Input, SqlDbType.VarChar, 255);


                    oParameters.Add("@sPostedCPTS", sPostedCPTS, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sPostedLineNos", sPostedLineNo, ParameterDirection.Input, SqlDbType.VarChar);

                    if (_dtNoPostCharges == null || _dtNoPostCharges.Columns.Count == 0)
                    {
                        _dtNoPostCharges = new DataTable();
                        _dtNoPostCharges.Columns.Add("nLineNo");
                        _dtNoPostCharges.Columns.Add("sCPTCodes");

                    }

                    oParameters.Add("@tvpEMRSPLITS", _dtNoPostCharges, ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@bIsSaveClose", _bIsSaveClose, ParameterDirection.Input, SqlDbType.Bit);
                    oParameters.Add("@AppointmentTVP", _dtAppointmentLink, ParameterDirection.Input, SqlDbType.Structured);

                    oDB.Connect(false);
                    //oDB.Execute("SaveCharges_TVP", oParameters, out  _oTransactionMstID, out _oNewClaimNo);
                    Hashtable oOUT = oDB.Execute("SaveCharges_TVP", oParameters, true);
                    oDB.Disconnect();

                    if (oOUT != null)
                    {
                        _oTransactionMstID = oOUT["@nTransactionMasterID"];
                        _oNewClaimNo = oOUT["@sTranResult"];
                        if (oOUT["@nTransactionID"] != DBNull.Value)
                            _nTransactionID = Convert.ToInt64(oOUT["@nTransactionID"]);
                    }

                    if (_oTransactionMstID != null)
                        TransactionMasterID = Convert.ToInt64(_oTransactionMstID);
                    else
                        TransactionMasterID = 0;

                    if (_oNewClaimNo != null)
                    { sTransResult = Convert.ToString(_oNewClaimNo); }
                    else
                    { sTransResult = ""; }

                    if (TransactionMasterID != 0)
                    {
                        _nextClaimNo = Convert.ToInt64(sTransResult);
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(sTransResult, true);
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

            return TransactionMasterID;

        }


        public static Int64 AutoDistributeCopay(Int64 nTransactionMSTID, Int64 nTransactionMSTDetailID, Int64 nTransactionDetailID, Int64 nPatientID, Int64 nPAccountID, Int64 nGuarantorID, Int64 nAccountPatientID, decimal Copay, DateTime dtClosedate, Int64 nUserID, string sUserName, DateTime dtDOS)
        {
            Int64 TransactionMasterID = 0;
            Int64 nCreaditID = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {

                oParameters.Add("@nTransactionMSTId", nTransactionMSTID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionMSTDetaild", nTransactionMSTDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionDetaild", nTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nGuarantorID", nGuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nAccountPatientID", nAccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@CopayReservetoDistribute", Copay, ParameterDirection.Input, SqlDbType.Decimal);
                oParameters.Add("@dtCloseDate", dtClosedate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sMachineName", Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@dtDOS", dtDOS, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@nCreaditID", 0, ParameterDirection.Output, SqlDbType.BigInt);

                oDB.Connect(false);
                //oDB.Execute("SaveCharges_TVP", oParameters, out  _oTransactionMstID, out _oNewClaimNo);
                Hashtable oOUT = oDB.Execute("BL_AutoDistributeCopay", oParameters, true);
                oDB.Disconnect();

                if (oOUT != null)
                {

                    if (oOUT["@nCreaditID"] != DBNull.Value)
                        nCreaditID = Convert.ToInt64(oOUT["@nCreaditID"]);
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

            return TransactionMasterID;

        }

        #endregion

        #region " Cases "

        public static void getSingleValidCase(DateTime dtClosedate, Int64 _PatientID, out DataTable dtCases, out  DataTable dtCasesDiag, out DataTable dtCasesIns)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            dtCases = new DataTable();
            dtCasesDiag = new DataTable();
            dtCasesIns = new DataTable();
            try
            {
                oDB.Connect(false);
                strQuery = " SELECT nCaseID, nPatientId, sCaseName, bUpdateDiagnose," +
                " dtHopitalizationDateTo, dtHopitalizationDateFrom, nInsuranceid," +
                " (Select nPriorAuthorizationID FROM PriorAuthorization_Mst WHERE nPriorAuthorizationID =Patient_Cases_MST.nAuthorizationID AND bIsActive=1) AS nAuthorizationID, " +
                "(Select sPriorAuthorizationNo FROM PriorAuthorization_Mst WHERE nPriorAuthorizationID =Patient_Cases_MST.nAuthorizationID AND bIsActive=1) AS sAuthorizationNumber, dtStartdate, dtEnddate, nFacilityID, nReferralID,nAccidenttype, " +
                " dtInjuryDate, sWCnumber, dtunbaleWorkfrom, dtunbaleWorkto, sState, nReportCategoryID," +
                " sNote,dtCreatedDateTime, dtModifiedDateTime, nCreatedUserID, nModifyUserID ,ISNULL(nICDRevision,9 ) AS nICDRevision , sClaimDateQualifier,dtOtherClaimDate,sOtherClaimDateQualifier FROM  Patient_Cases_MST" +
               "  WHERE (dtEnddate>= '" + dtClosedate + "' OR  dtEnddate IS NULL) " + " and  nPatientId = " + _PatientID;
                oDB.Retrive_Query(strQuery, out dtCases);
                if (dtCases != null && dtCases.Rows.Count == 1)
                {
                    Int64 nCaseID = 0;
                    nCaseID = Convert.ToInt64(dtCases.Rows[0]["nCaseID"]);
                    strQuery = " SELECT sdxCode,sDxDescription FROM Patient_Cases_Diag where nCaseID= " + nCaseID + " Order By nIndex";
                    oDB.Retrive_Query(strQuery, out dtCasesDiag);
                    strQuery = "select nID, nCaseID, nInsuranceId ,sInsuranceName , nResponsibilityNumber from Patient_Cases_InsPlan where nCaseID= " + nCaseID + "ORDER BY nResponsibilityNumber";
                    oDB.Retrive_Query(strQuery, out dtCasesIns);
                }


            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();

            }
        }

        public static bool getCaseUpdatelastDiag(Int64 nCaseId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            bool result = false;
            try
            {
                Object oResult;
                oDB.Connect(false);
                strQuery = " SELECT ISNULL(bUpdateDiagnose,0) FROM Patient_Cases_Mst where nCaseId= " + nCaseId;
                oResult = oDB.ExecuteScalar_Query(strQuery);
                oDB.Disconnect();
                if (oResult != null && Convert.ToString(oResult) != "")
                    result = Convert.ToBoolean(oResult);

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

            }
            return result;
        }

        public static bool UpdateCaseDiagnoses(Int64 nCaseID, int nICDRevision)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Boolean isUpdated = false;
            try
            {
                oDBParameters.Add("@CaseID", nCaseID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nICDRevision", nICDRevision, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDB.Connect(false);
                oDB.Execute("Patient_CaseDiag_Update", oDBParameters);

                isUpdated = true;
            }

            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                isUpdated = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                isUpdated = false;
            }
            finally
            {
                if (oDB != null)
                { oDB.Disconnect(); oDB.Dispose(); }

                if (oDBParameters != null)
                    oDBParameters.Dispose();

            }
            return isUpdated;
        }

        public static Boolean CheckPatientCases(Int64 nPatientID)
        {
            Boolean _isPatientCasesExists = false;
            gloDatabaseLayer.DBLayer oDB = null;
            DataTable _dtResult = null;

            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                string sqlstring = "SELECT 1 FROM Patient_Cases_Mst WHERE  nPatientId = " + nPatientID;
                oDB.Connect(false);
                oDB.Retrive_Query(sqlstring, out _dtResult);

                if (_dtResult != null && _dtResult.Rows.Count > 0)
                {
                    _isPatientCasesExists = true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return _isPatientCasesExists;
        }

        #endregion

        #region "Insurance Coverage Date"
        public static void GetInsuranceCovrageDates(Int64 _PatientID, Int64 _nCurrentResponsibleContactID, Int64 _nCurrentInsuranceID, out DataTable dtInsuranceCovrageDates)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            dtInsuranceCovrageDates = new DataTable();

            try
            {
                oDB.Connect(false);
                strQuery = "SELECT dtStartDate,dtEndDate  FROM dbo.PatientInsurance_DTL where nPatientID=" + _PatientID + " AND nInsuranceID=" + _nCurrentInsuranceID + " AND nContactID=" + _nCurrentResponsibleContactID;
                oDB.Retrive_Query(strQuery, out dtInsuranceCovrageDates);

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();

            }
        }
        #endregion

        #region " Global Periods "

        public static DataTable GetLastPatientGlobalPeriod(Int64 nPatientID, bool bIncludeToday)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            DataTable _dt = null;

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT  * FROM [dbo].[Get_Last_PatientGlobalPeriod](" + nPatientID + ",'" + bIncludeToday + "')";
                oDB.Retrive_Query(_sqlQuery, out _dt);
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
            return _dt;
        }

        public static DataTable CheckForGlobalPeriods(DataTable _dtCPT, Int64 nInsuranceID, Int64 nProviderID, Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dt = null;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@tvpCPTCollection", _dtCPT, ParameterDirection.Input, SqlDbType.Structured);
                oParameters.Add("@nInsuranceID", nInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nProviderID", nProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("Check_for_Global_Periods", oParameters, out _dt);

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dt;
        }

        public static DataTable CheckPatientGlobalPeriods(DataTable _dtCPT, Int64 nInsuranceID, Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dt = null;

            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@tvpCPTCollection", _dtCPT, ParameterDirection.Input, SqlDbType.Structured);
                oParameters.Add("@nInsuranceID", nInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("Check_Patient_Global_Periods", oParameters, out _dt);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dt;
        }


        #endregion

        #region " Split Claims"

        public static bool CheckForSplitClaim(Int64 nTransactionID)
        {
            bool _bReturn = false;
            Hashtable oOut = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDBParameters.Add("@nTransactionID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                //oDBParameters.Add("@nContactID", nContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@bIsExists", false, System.Data.ParameterDirection.Output, System.Data.SqlDbType.Bit);
                oDB.Connect(false);
                oOut = oDB.Execute("Get_Matched_Insurance", oDBParameters, true);

                _bReturn = Convert.ToBoolean(oOut["@bIsExists"]);
            }

            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                _bReturn = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                _bReturn = false;
            }
            finally
            {
                if (oDB != null)
                { oDB.Disconnect(); oDB.Dispose(); }

                if (oDBParameters != null)
                    oDBParameters.Dispose();
                oOut = null;
            }
            return _bReturn;
        }

        public static bool UpdateSplitClaimIns(Int64 nTransactionID, bool bTransferResp)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Boolean isUpdated = false;
            try
            {
                oDBParameters.Add("@nTransactionID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@bTransferResp", bTransferResp, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Execute("CLAIM_INSURANCE_MODIFY", oDBParameters);

                isUpdated = true;
            }

            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                isUpdated = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                isUpdated = false;
            }
            finally
            {
                if (oDB != null)
                { oDB.Disconnect(); oDB.Dispose(); }

                if (oDBParameters != null)
                    oDBParameters.Dispose();

            }
            return isUpdated;
        }

        public static bool PerformSplitOperation(Int64 nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            Object _Result = null;
            bool _Return = false;

            try
            {
                _sqlQuery = "SELECT  [dbo].[PERFORM_SPLIT_CLAIM](" + nContactID + ")";
                oDB.Connect(false);
                _Result = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();
                if (_Result != null)
                {
                    _Return = Convert.ToBoolean(_Result);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _Return = false;
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); }

                if (_Result != null)
                { _Result = null; }
            }
            return _Return;
        }

        public static Int64 GetParentClaimTransactionID(Int64 nTransactionMasterID)
        {
            Int64 nTransactionID = 0;
            DataTable _dtParentClaimTransactionID = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            try
            {
                string _strQuery = " SELECT nTransactionID,nClaimNo FROM dbo.BL_Transaction_claim_MST  WHERE nTransactionMasterID=" + nTransactionMasterID +
                                    " AND ISNULL(nParentTransactionID,0)=0";
                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out _dtParentClaimTransactionID);
                oDB.Disconnect();

                if (_dtParentClaimTransactionID != null && _dtParentClaimTransactionID.Rows.Count > 0)
                {
                    nTransactionID = Convert.ToInt64(_dtParentClaimTransactionID.Rows[0]["nTransactionID"]);
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());

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
            return nTransactionID;
        }

        #endregion

        #region "UB04"

        public static Int16 GetBillingType(Int64 TransactionId, Int64 MstTransactionId)
        {
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                object BillingType;
                oParameters.Add("@nTransactionId", TransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionMstId", MstTransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                BillingType = oDB.ExecuteScalar("BL_Get_BillingType", oParameters);
                oDB.Disconnect();
                if (Convert.ToString(BillingType) != "")
                {
                    return Convert.ToInt16(BillingType);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return 0;
            }


        }
        public static Int16 GetBillingType(Int64 TransactionId, Int64 MstTransactionId, Boolean bIsCopyClaim)
        {
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                object BillingType;
                oParameters.Add("@nTransactionId", TransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionMstId", MstTransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bIsCopyClaim", bIsCopyClaim, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                BillingType = oDB.ExecuteScalar("BL_Get_BillingType", oParameters);
                oDB.Disconnect();
                if (Convert.ToString(BillingType) != "")
                {
                    return Convert.ToInt16(BillingType);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return 0;
            }


        }

        public static DataSet FillUB04Data(Int64 TransactionMasterID, Int64 TransactionID, ref UB04Data oUB)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsUB04Data = null;
            try
            {
                oDBParameters.Add("@nTransactionMasterID", TransactionMasterID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionID", TransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_ADDITIONAL_UB_CLAIM_DATA", oDBParameters, out dsUB04Data);
                oDB.Disconnect();
                dsUB04Data.Tables[0].TableName = "ChargeUBData";
                dsUB04Data.Tables[1].TableName = "ConditionCode";
                dsUB04Data.Tables[2].TableName = "ValueCode";
                dsUB04Data.Tables[3].TableName = "OccurrenceCode";
                dsUB04Data.Tables[4].TableName = "OccurrenceSpanCode";
                if (dsUB04Data.Tables["ChargeUBData"] != null)
                {
                    if (dsUB04Data.Tables["ChargeUBData"].Rows.Count > 0)
                    {
                        oUB.sTypeofbill = Convert.ToString(dsUB04Data.Tables["ChargeUBData"].Rows[0]["sTypeOfBill"]);
                        if (oUB != null && Convert.ToString(oUB.sTypeofbill) != "")
                        {
                            oUB.IsModify = true;
                        }
                        oUB.sAdmissionType = Convert.ToString(dsUB04Data.Tables["ChargeUBData"].Rows[0]["sAdmissionTypeCode"]);
                        if (oUB != null && Convert.ToString(oUB.sAdmissionType) != "")
                        {
                            oUB.IsModify = true;
                        }

                        oUB.sAdmitDate = Convert.ToString(dsUB04Data.Tables["ChargeUBData"].Rows[0]["AdmitDate"]);
                        if (oUB != null && Convert.ToString(oUB.sAdmitDate) != "")
                        {
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        oUB.sAdmitHour = Convert.ToString(dsUB04Data.Tables["ChargeUBData"].Rows[0]["Admithour"]);
                        if (oUB != null && Convert.ToString(oUB.sAdmitHour) != "")
                        {
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        oUB.sDischargeHour = Convert.ToString(dsUB04Data.Tables["ChargeUBData"].Rows[0]["Dischargehour"]);
                        if (oUB != null && Convert.ToString(oUB.sDischargeHour) != "")
                        {
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        oUB.sDischargeStatus = Convert.ToString(dsUB04Data.Tables["ChargeUBData"].Rows[0]["sDischargeStatusCode"]);
                        if (oUB != null && Convert.ToString(oUB.sDischargeStatus) != "")
                        {
                            oUB.IsModify = true;
                        }
                    }
                }


                if (dsUB04Data.Tables["ConditionCode"] != null)
                {
                    if (dsUB04Data.Tables["ConditionCode"].Rows.Count > 0)
                    {
                        if (Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode01"]) != "")
                        {
                            oUB.sConditionCode01 = Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode01"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode02"]) != "")
                        {
                            oUB.sConditionCode02 = Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode02"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode03"]) != "")
                        {
                            oUB.sConditionCode03 = Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode03"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode04"]) != "")
                        {
                            oUB.sConditionCode04 = Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode04"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode05"]) != "")
                        {
                            oUB.sConditionCode05 = Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode05"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode06"]) != "")
                        {
                            oUB.sConditionCode06 = Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode06"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode07"]) != "")
                        {
                            oUB.sConditionCode07 = Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode07"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode08"]) != "")
                        {
                            oUB.sConditionCode08 = Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode08"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode09"]) != "")
                        {
                            oUB.sConditionCode09 = Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode09"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode10"]) != "")
                        {
                            oUB.sConditionCode10 = Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode10"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode11"]) != "")
                        {
                            oUB.sConditionCode11 = Convert.ToString(dsUB04Data.Tables["ConditionCode"].Rows[0]["sConditionCode11"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }


                    }
                }


                if (dsUB04Data.Tables["ValueCode"] != null)
                {
                    if (dsUB04Data.Tables["ValueCode"].Rows.Count > 0)
                    {
                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode01"]) != "")
                        {
                            oUB.sValueCode01 = dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode01"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount01"]) != "")
                        {
                            oUB.nValueAmount01 = Convert.ToDecimal(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount01"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode02"]) != "")
                        {
                            oUB.sValueCode02 = dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode02"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount02"]) != "")
                        {
                            oUB.nValueAmount02 = Convert.ToDecimal(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount02"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode03"]) != "")
                        {
                            oUB.sValueCode03 = dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode03"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount03"]) != "")
                        {
                            oUB.nValueAmount03 = Convert.ToDecimal(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount03"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode04"]) != "")
                        {
                            oUB.sValueCode04 = dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode04"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount04"]) != "")
                        {
                            oUB.nValueAmount04 = Convert.ToDecimal(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount04"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode05"]) != "")
                        {
                            oUB.sValueCode05 = dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode05"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount05"]) != "")
                        {
                            oUB.nValueAmount05 = Convert.ToDecimal(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount05"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode06"]) != "")
                        {
                            oUB.sValueCode06 = dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode06"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount06"]) != "")
                        {
                            oUB.nValueAmount06 = Convert.ToDecimal(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount06"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode07"]) != "")
                        {
                            oUB.sValueCode07 = dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode07"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount07"]) != "")
                        {
                            oUB.nValueAmount07 = Convert.ToDecimal(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount07"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }


                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode08"]) != "")
                        {
                            oUB.sValueCode08 = dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode08"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount08"]) != "")
                        {
                            oUB.nValueAmount08 = Convert.ToDecimal(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount08"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode09"]) != "")
                        {
                            oUB.sValueCode09 = dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode09"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount09"]) != "")
                        {
                            oUB.nValueAmount09 = Convert.ToDecimal(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount09"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode10"]) != "")
                        {
                            oUB.sValueCode10 = dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode10"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount10"]) != "")
                        {
                            oUB.nValueAmount10 = Convert.ToDecimal(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount10"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode11"]) != "")
                        {
                            oUB.sValueCode11 = dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode11"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount11"]) != "")
                        {
                            oUB.nValueAmount11 = Convert.ToDecimal(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount11"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode12"]) != "")
                        {
                            oUB.sValueCode12 = dsUB04Data.Tables["ValueCode"].Rows[0]["sValueCode12"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount12"]) != "")
                        {
                            oUB.nValueAmount12 = Convert.ToDecimal(dsUB04Data.Tables["ValueCode"].Rows[0]["nValueAmount12"]);
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                    }
                }


                if (dsUB04Data.Tables["OccurrenceCode"] != null)
                {
                    if (dsUB04Data.Tables["OccurrenceCode"].Rows.Count > 0)
                    {
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode01"]) != "")
                        {
                            oUB.sOccurrenceCode01 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode01"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate01"]) != "")
                        {
                            oUB.sOccurrenceDate01 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate01"].ToString();
                            oUB.IsModify = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode02"]) != "")
                        {
                            oUB.sOccurrenceCode02 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode02"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate02"]) != "")
                        {
                            oUB.sOccurrenceDate02 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate02"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode03"]) != "")
                        {
                            oUB.sOccurrenceCode03 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode03"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate03"]) != "")
                        {
                            oUB.sOccurrenceDate03 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate03"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode04"]) != "")
                        {
                            oUB.sOccurrenceCode04 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode04"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate04"]) != "")
                        {
                            oUB.sOccurrenceDate04 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate04"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode05"]) != "")
                        {
                            oUB.sOccurrenceCode05 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode05"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate05"]) != "")
                        {
                            oUB.sOccurrenceDate05 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate05"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode06"]) != "")
                        {
                            oUB.sOccurrenceCode06 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode06"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate06"]) != "")
                        {
                            oUB.sOccurrenceDate06 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate06"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode07"]) != "")
                        {
                            oUB.sOccurrenceCode07 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode07"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate07"]) != "")
                        {
                            oUB.sOccurrenceDate07 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate07"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode08"]) != "")
                        {
                            oUB.sOccurrenceCode08 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["sOccurrenceCode08"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate08"]) != "")
                        {
                            oUB.sOccurrenceDate08 = dsUB04Data.Tables["OccurrenceCode"].Rows[0]["dtOccurrenceDate08"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                    }
                }


                if (dsUB04Data.Tables["OccurrenceSpanCode"] != null)
                {
                    if (dsUB04Data.Tables["OccurrenceSpanCode"].Rows.Count > 0)
                    {
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["sOccurrenceSpanCode01"]) != "")
                        {
                            oUB.sOccurrenceSpanCode01 = dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["sOccurrenceSpanCode01"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceFromspanDate01"]) != "")
                        {
                            oUB.sOccurrenceFromSpanDate01 = dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceFromspanDate01"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceTospanDate01"]) != "")
                        {
                            oUB.sOccurrenceTOSpanDate01 = dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceTospanDate01"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }


                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["sOccurrenceSpanCode02"]) != "")
                        {
                            oUB.sOccurrenceSpanCode02 = dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["sOccurrenceSpanCode02"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceFromspanDate02"]) != "")
                        {
                            oUB.sOccurrenceFromSpanDate02 = dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceFromspanDate02"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceTospanDate02"]) != "")
                        {
                            oUB.sOccurrenceToSpanDate02 = dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceTospanDate02"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["sOccurrenceSpanCode03"]) != "")
                        {
                            oUB.sOccurrenceSpanCode03 = dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["sOccurrenceSpanCode03"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceFromspanDate03"]) != "")
                        {
                            oUB.sOccurrenceFromSpanDate03 = dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceFromspanDate03"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceTospanDate03"]) != "")
                        {
                            oUB.sOccurrenceToSpanDate03 = dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceTospanDate03"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }

                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["sOccurrenceSpanCode04"]) != "")
                        {
                            oUB.sOccurrenceSpanCode04 = dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["sOccurrenceSpanCode04"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceFromspanDate04"]) != "")
                        {
                            oUB.sOccurrenceFromSpanDate04 = dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceFromspanDate04"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                        if (Convert.ToString(dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceTospanDate04"]) != "")
                        {
                            oUB.sOccurrenceToSpanDate04 = dsUB04Data.Tables["OccurrenceSpanCode"].Rows[0]["dtOccurrenceTospanDate04"].ToString();
                            oUB.IsModify = true;
                            oUB.HasOtherData = true;
                        }
                    }
                }





            }
            catch (gloDatabaseLayer.DBException ex)
            {
                //ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //dsHeaderEDI.Dispose();
                oDBParameters.Dispose();
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return dsUB04Data;
        }

        #endregion

        #region " Void and ReplaceClaim"

        public static bool VoidAndReplaceClaim(Int64 nReplacedByTransMasterID, Int64 nReplacingTransMasterID, Int64 nTransactionMasterID, Boolean bIsCopiedClaim)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            int oResult = 0;
            bool _result = false;
            string strQuery = "";
            Int64 _AssociatednReplacedByTransMasterID = 0;
            Int64 _AssociatednReplacingTransMasterID = 0;
            try
            {
                if (bIsCopiedClaim)  //New Copy Claim
                {
                    oDB.Connect(false);

                    //Updated Current Claim For New Claim
                    strQuery = "UPDATE BL_Transaction_MST SET  nReplacingTransMasterID = " + nReplacingTransMasterID + ", nReplacedByTransMasterID =0 WHERE nTransactionID= " + nTransactionMasterID + "";
                    oResult = oDB.Execute_Query(strQuery);

                    //Updated Voided Claim (Prev Claim)
                    strQuery = "";
                    strQuery = "UPDATE BL_Transaction_MST SET nReplacedByTransMasterID =" + nTransactionMasterID + " WHERE nTransactionID= " + nReplacingTransMasterID + "";
                    oResult = oDB.Execute_Query(strQuery);


                    //Updated Previos replacing claim if any
                    strQuery = "UPDATE BL_Transaction_MST SET  nReplacingTransMasterID = 0, nReplacedByTransMasterID =0 WHERE nReplacingTransMasterID = " + nReplacingTransMasterID + " AND nTransactionID <> " + nTransactionMasterID + "";
                    oResult = oDB.Execute_Query(strQuery);
                }
                else //Modify Claim
                {

                    //-----------------------------Update Claim-----------------------------------------------

                    oDB.Connect(false);

                    if (nReplacingTransMasterID == 0)
                    {
                        //To Remove All the Links from other associated Claims
                        DataSet _dtReplacementClaimDtls = gloCharges.RetreiveReplacementClaim(nTransactionMasterID);
                        if (_dtReplacementClaimDtls != null && _dtReplacementClaimDtls.Tables.Count > 0)
                        {
                            _AssociatednReplacedByTransMasterID = Convert.ToInt64(_dtReplacementClaimDtls.Tables[0].Rows[0]["nReplacedByTransMasterID"]);
                            _AssociatednReplacingTransMasterID = Convert.ToInt64(_dtReplacementClaimDtls.Tables[0].Rows[0]["nReplacingTransMasterID"]);
                        }


                        if (nReplacingTransMasterID == 0 && _AssociatednReplacingTransMasterID > 0)
                        {
                            strQuery = "";
                            strQuery = "UPDATE BL_Transaction_MST SET nReplacedByTransMasterID =0 WHERE nTransactionID= " + _AssociatednReplacingTransMasterID + "";
                            oResult = oDB.Execute_Query(strQuery);
                        }
                    }


                    //Updated Current Claim replacement IDs 

                    strQuery = "UPDATE BL_Transaction_MST SET nReplacedByTransMasterID =" + nReplacedByTransMasterID + ", "
                               + "nReplacingTransMasterID = " + nReplacingTransMasterID + " "
                               + "WHERE nTransactionID= " + nTransactionMasterID + "";
                    oResult = oDB.Execute_Query(strQuery);



                    if (nReplacingTransMasterID > 0)
                    {
                        //Updated Voided Claim (Prev Claim)
                        strQuery = "UPDATE BL_Transaction_MST SET nReplacedByTransMasterID =" + nTransactionMasterID + " WHERE nTransactionID= " + nReplacingTransMasterID + "";
                        oResult = oDB.Execute_Query(strQuery);

                        //Remove Replacing Link from Other Claim To Avoid multiple links
                        strQuery = "UPDATE BL_Transaction_MST SET nReplacingTransMasterID = 0 WHERE nReplacingTransMasterID = " + nReplacingTransMasterID + " AND nTransactionID <> " + nTransactionMasterID + "";
                        oResult = oDB.Execute_Query(strQuery);

                        //Remove ReplacedBy Link if Other Claim  To Avoid multiple links
                        strQuery = "UPDATE BL_Transaction_MST SET nReplacedByTransMasterID = 0 WHERE nReplacedByTransMasterID =" + nTransactionMasterID + " AND nTransactionID <> " + nReplacingTransMasterID + "";
                        oResult = oDB.Execute_Query(strQuery);
                    }

                    //----------------------------------------------------------------------------

                }
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }

            }

            //   if (oResult != null)
            {
                if (oResult == 0)
                {
                    _result = false;
                }
                else
                {
                    _result = true;
                }
            }
            return _result;
        }

        public static DataSet RetreiveReplacementClaim(Int64 nTransactionMasterID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            int oResult = 0;
            DataSet _dtresult = null;
            try
            {

                oParameters.Clear();
                oParameters.Add("@nTransactionMasterID", nTransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_RetreiveReplacementClaim", oParameters, out _dtresult);
                oDB.Disconnect();

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

            //   if (oResult != null)
            {
                if (oResult == 0)
                {

                }
                else
                {

                }
            }
            return _dtresult;
        }

        #endregion

        #region ClaimRules

        public static void SaveTriggeredRules(DataTable dtRule, Boolean DeleteRules)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(true);
                oParameters.Add("@TVPRule", dtRule, ParameterDirection.Input, SqlDbType.Structured);
                oParameters.Add("@IsForDelete", DeleteRules, ParameterDirection.Input, SqlDbType.Bit);
                oDB.ExecuteWithTransaction("ClaimRule_RulesInsertUpdate", oParameters);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                oDB.Rollback();
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx, false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx.ToString(), true);

            }
            catch (Exception ex)
            {
                oDB.Rollback();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }



        }
        public static void SaveGlobalTriggeredRules(DataTable dtRule)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(true);
                oParameters.Add("@TVPGlobalRule", dtRule, ParameterDirection.Input, SqlDbType.Structured);
                oDB.ExecuteWithTransaction("ClaimRule_GlobalRulesInsertUpdate", oParameters);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                oDB.Rollback();
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx, false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx.ToString(), true);

            }
            catch (Exception ex)
            {
                oDB.Rollback();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }



        }

        public static void ClearRulesCached()
        {
            try
            {
                ChargeRules.RulesRepository.ClearRulesCache();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }

        #endregion


        #region Checking befor printing data and forms

        public static Boolean IsGettingBrokenRules(Int64 TransactionMasterID, Int64 TransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable dtBrokenRules = new DataTable();

            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nTransactionMasterID", TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("ClaimRule_GetErrorsAndWarnnings", oParameters, out dtBrokenRules);
                oDB.Disconnect();
                if (dtBrokenRules != null)
                {
                    if (dtBrokenRules.Rows.Count > 0 && gloGlobal.gloPMGlobal.IsClaimRulesEnabled())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                oDB.Rollback();
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx, false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx.ToString(), true);
                return false;
            }
            catch (Exception ex)
            {
                oDB.Rollback();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }



        }
        #endregion

        #region "Appointments Charges Linking"
        public static void DeleteLinking(long TransactionMasterID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nChargeID", TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Execute("AptChrgs_DeleteLinking", oParameters);
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        public static void SaveAppointmentsChargesLink(long TransactionMasterID, DataTable AppointmentsData)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nTransactionMasterID", TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@TVP", AppointmentsData, ParameterDirection.Input, SqlDbType.Structured);
                oDB.Connect(false);
                oDB.Execute("AptChrgs_SaveAppointments", oParameters);
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        public static DataTable GetLinkedPatientAppointments(Int64 PatientID, Int64 ChargeID)
        {
            DataTable dtAppointmentsData = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {

                oDB.Connect(false);
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nChargeID", ChargeID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("AptChrgs_GetLinkedPatientAppointments", oDBParameters, out dtAppointmentsData);

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }

            return dtAppointmentsData.Copy();
        }

        public static DataTable GetAppointments(Int64 PatientID, Int64 FromDate, Int64 ToDate, DataTable TVP)
        {
            DataTable dtAppointments = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nPatientId", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtStartDateOfService", FromDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtEndDateOfService", ToDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@TVP", TVP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Structured);
                oDB.Retrive("AptChrgs_GetPatientAppointments", oDBParameters, out dtAppointments);
                return dtAppointments;
            }

            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                dtAppointments.Dispose();
            }
        }

         public static DataSet GetMissingChargeAppointments(Int64 PatientID,int AppointmentStatus,Int64 AppointmentTypeID)
        {
            DataSet dtLatestAppointments = new DataSet();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nPatientId", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@AppointmentStatus", AppointmentStatus, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nAppointmentTypeID", AppointmentTypeID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDB.Retrive("AptChrgs_GetPatientLatestAppointments", oDBParameters, out dtLatestAppointments);
                return dtLatestAppointments;
            }

            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                dtLatestAppointments.Dispose();
            }
        }
        
        public static DataTable GetLinkedAppointments(Int64 PatientID, Int64 ChargeID, Int64 FromDate, Int64 ToDate, DataTable TVP)
        {
            DataTable dtAppointmet = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nPatientId", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nChargeID", ChargeID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtStartDateOfService", FromDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtEndDateOfService", ToDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@TVP", TVP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Structured);
                oDB.Retrive("AptChrgs_GetAssociatedPatientAppointments", oDBParameters, out dtAppointmet);
                return dtAppointmet;
            }

            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                dtAppointmet.Dispose();
            }
        }

        public static DataTable GetTVPAppointments(DataTable TVP)
        {
            DataTable dtAppointmet = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@TVP", TVP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Structured);
                oDB.Retrive("AptChrgs_GetTVPAppointments", oDBParameters, out dtAppointmet);
                return dtAppointmet;
            }

            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                dtAppointmet.Dispose();
            }
        }
        #endregion
        public static bool IsMammogramCPTPresentOnClaim(DataTable TVP)
        {
            Boolean bIsMammogramCPTPresent = false;
            DataTable dt_result = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@TVP", TVP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Structured);
                oDB.Retrive("gsp_IsMammogramCPTPresentonClaim", oDBParameters, out dt_result);
                if (dt_result != null && dt_result.Rows.Count > 0)
                {
                    bIsMammogramCPTPresent = Convert.ToBoolean(dt_result.Rows[0]["bIsMammogramCPTPresent"]);

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();

            }
            return bIsMammogramCPTPresent;
        }
        public static bool IsMammogramCPTPresentOnClaim(string sCPT)
        {
            Boolean bIsMammogramCPTPresent = false;
            // DataTable dt_result=null;
            string _sqlQuery = "";
            Object _Result;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
                oDB.Connect(false);
                _sqlQuery = String.Format("select [dbo].[BL_IsMammogramCPTPresentonClaim]('{0}')", sCPT);
                _Result = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();
                if (_Result != null)
                {
                    if (Convert.ToString(_Result) != "")
                    {
                        bIsMammogramCPTPresent = Convert.ToBoolean(_Result);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                //return "";
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return bIsMammogramCPTPresent;
        }
        public static DataTable GetOnlineChargesList()
        {
            DataTable _dtOnlineCharge = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {

                oDB.Connect(false);
                oDB.Retrive("gsp_GetOCPClaimList", oDBParameters, out _dtOnlineCharge);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
            return _dtOnlineCharge;
        }


        public static bool UpdateOCPClaimDetails(long _PortalClaimID, long nPostedAsTransactionID, Int64 UpdateType)
        {
            bool bIsUpdated = false;
            Object _result;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@PortalClaimID", _PortalClaimID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@TransactionID", nPostedAsTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@UpdateType", UpdateType, ParameterDirection.Input, SqlDbType.BigInt);
                _result = oDB.Execute("gsp_UpdateOCPClaimData", oDBParameters);
                if (_result != null)
                {
                    bIsUpdated = Convert.ToBoolean(_result);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return bIsUpdated;
        }
        public static DataTable GetModifiyTransaction(Int64 MasterTransactionID)
        {
            DataTable _dtModify = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                oDBParameters.Add("@nTransactionMasterID", MasterTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_GetModifiedTransaction", oDBParameters, out _dtModify);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _dtModify;

        }

        public static Int64 InsertNoteBeforeDelete(Int64 NoteID,string NoteType,string sourceTable)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object obj=new object();
            Int64 NoteHistoryID = 0;
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nNoteHistoryID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@nNoteID", NoteID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sNoteType", NoteType, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sNoteSourceTable", sourceTable, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Execute("gsp_InsertNoteHistory", oDBParameters,out obj);

                if (obj != null)
                {
                    NoteHistoryID = Convert.ToInt64(obj);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return NoteHistoryID;

        }

        public static Int64 DeleteNoteInHistory(string NoteIDs)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object obj = new object();
            Int64 NoteHistoryID = 0;
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@sNoteIDs", NoteIDs, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Execute("gsp_DeleteNoteInHistory", oDBParameters, out obj);

                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return NoteHistoryID;

        }
        public static string CheckPatientStatus(Int64 PatientID, string ClaimCPTCode)
        {
            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            string sPatientStatus = string.Empty;

            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sCurrentClaimCPTs", ClaimCPTCode, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("gsp_IsNewPatientOrEstablishedPatient", oDBParameters, out dt);


                if (dt != null && dt.Rows.Count > 0)
                {
                    sPatientStatus = Convert.ToString(dt.Rows[0][0]);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return sPatientStatus;
        }
    }
}



    


