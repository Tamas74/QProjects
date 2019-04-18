#region "Namespaces"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data;

#endregion

#region "Namespace gloPatient"

namespace gloPatient
{
    //Added by SaiKrishna:2010-12-31(yyyy-mm-dd)
    //This Class is used to hold Common methods related to Account
    #region "gloAccount Class"

    public class gloAccount : IDisposable
    {

        #region"Variable Declaration"

        private string _databaseconnectionstring = string.Empty;
        private Int64 _ClinicID = 1;
        private string _messageBoxCaption = "gloPM";
        private bool disposed = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


        public enum AccountNoteType
        {
            None = 0,
            AccountNote = 1
        }

        #endregion

        #region "Constructor & Destructor"

        public gloAccount(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            #region " Retrieve MessageBoxCaption,ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                    _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                else _ClinicID = 1;
            }
            else
                _ClinicID = 1;

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);

                else
                    _messageBoxCaption = "gloPM";
            }
            else
                _messageBoxCaption = "gloPM";


            #endregion
        }

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

        ~gloAccount()
        {
            Dispose(false);
        }

        #endregion

        #region "Methods"

        /// <summary>
        ///  Method to get account details by accounId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public DataTable GetAccountDetailsById(Int64 accountId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtAccountDetails = new DataTable();

            try
            {
                oDB.Connect(false);

                string _strSqlQuery = "Select nPAccountID, sAccountNo, sAccountDesc, nGuarantorID, sGuarantorCode, dtAccountClosedDate, sFirstName,sMiddleName,";
                _strSqlQuery = _strSqlQuery + " sLastName,nEntityType,sAddressLine1,sAddressLine2,sCity,sState,sZip,sCountry,sCounty,sAreaCode,(Case When bIsExcludeStatement is null then 0 when len(ltrim(rtrim(bIsExcludeStatement))) = 0 then 0 else bIsExcludeStatement end) as bIsExcludeStatement,";
                _strSqlQuery = _strSqlQuery + " bIsSentToCollection,nClinicID,nSiteID,sMachineName,PA_Accounts.nUserID,dtRecordDate,PA_Accounts.bIsActive ,ISNULL(PA_Accounts.nBusinessCenterID,0) AS nBusinessCenterID ,ISNULL(BL_BusinessCenterCodes.sBusinessCenterCode,'') AS sBusinessCenterCode,ISNULL(sBusinessCenterCode + ' - ' + sDescription,'') AS BusinessCenter ";
                _strSqlQuery = _strSqlQuery + " from PA_Accounts WITH (NOLOCK) LEFT OUTER JOIN dbo.BL_BusinessCenterCodes ON dbo.PA_Accounts.nBusinessCenterID = dbo.BL_BusinessCenterCodes.nBusinessCenterID ";
                _strSqlQuery = _strSqlQuery + " where nPAccountID = " + accountId;

                oDB.Retrive_Query(_strSqlQuery, out dtAccountDetails);

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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dtAccountDetails;

        }

        public DataTable GetAccountForStatement(Int64 accountId)
        {
            
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            DataTable dt = null;

            try
            {
                oParameters.Add("@nPAccountID", accountId, ParameterDirection.Input, SqlDbType.BigInt);

                ODB.Connect(false);
                ODB.Retrive("PA_Select_AccountById", oParameters, out dt);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); ODB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }
            return dt;

        }

        public DataTable GetAccountDetailsByIdOnBCFeatureEnabled(Int64 accountId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtAccountDetails = new DataTable();

            try
            {
                oDB.Connect(false);

                string _strSqlQuery = " SELECT  PA_Accounts.nPAccountID , " +
                                " PA_Accounts.sAccountNo , " +
                                "( CASE WHEN ISNULL(PA_Accounts.nBusinessCenterID,0) <> 0  " + 
                                "       THEN ( PA_Accounts_Patients.sAccountNo + '-' " +  
                                "       + ( SELECT    LTRIM(RTRIM(sFirstName + '' + sLastName))  " + 
                                "                  FROM      Patient_OtherContacts   " +
                                "                  WHERE     nPatientContactID = PA_Accounts.nGuarantorId   " +
                                "                ) + '-' + BL_BusinessCenterCodes.sBusinessCenterCode )  " + 
                                "       ELSE ( PA_Accounts_Patients.sAccountNo + '-'   " +
                                "              + ( SELECT    LTRIM(RTRIM(sFirstName + '' + sLastName))   " +
                                "                  FROM      Patient_OtherContacts   " +
                                "                  WHERE     nPatientContactID = PA_Accounts.nGuarantorId   " +
                                "                ) )   " +
                                "  END ) AS sAccount, " +
                                "PA_Accounts.sAccountDesc , " +
                                "PA_Accounts.nGuarantorID , " +
                                "PA_Accounts.sGuarantorCode , " +
                                "PA_Accounts.dtAccountClosedDate , " +
                                "PA_Accounts.sFirstName , " +
                                "PA_Accounts.sMiddleName , " +
                                "PA_Accounts.sLastName , " +
                                "PA_Accounts.nEntityType , " +
                                "PA_Accounts.sAddressLine1 , " +
                                "PA_Accounts.sAddressLine2 , " +
                                "PA_Accounts.sCity , " +
                                "PA_Accounts.sState , " +
                                "PA_Accounts.sZip , " +
                                "PA_Accounts.sCountry , " +
                                "PA_Accounts.sCounty , " +
                                "PA_Accounts.sAreaCode , " +
                                "( CASE WHEN PA_Accounts.bIsExcludeStatement IS NULL THEN 0 " +
                                "       WHEN LEN(LTRIM(RTRIM(PA_Accounts.bIsExcludeStatement))) = 0 THEN 0 " +
                                "       ELSE PA_Accounts.bIsExcludeStatement " +
                                "  END ) AS bIsExcludeStatement , " +
                                "PA_Accounts.bIsSentToCollection , " +
                                "PA_Accounts.nClinicID , " +
                                "PA_Accounts.nSiteID , " +
                                "PA_Accounts.sMachineName , " +
                                "PA_Accounts.nUserID , " +
                                "PA_Accounts.dtRecordDate , " +
                                "PA_Accounts.bIsActive , " +
                                "ISNULL(PA_Accounts.nBusinessCenterID, 0) AS nBusinessCenterID , " +
                                "ISNULL(BL_BusinessCenterCodes.sBusinessCenterCode, '') AS sBusinessCenterCode , " +
                                "ISNULL(sBusinessCenterCode + ' - ' + sDescription, '') AS BusinessCenter " +
                                "From PA_Accounts  WITH (NOLOCK)  Inner Join PA_Accounts_Patients  WITH (NOLOCK)  on PA_Accounts.nPAccountID = PA_Accounts_Patients.nPAccountID         " +
                                " LEFT OUTER JOIN Patient  WITH (NOLOCK)  ON Patient.nPatientID = PA_Accounts_Patients.nPatientID     " +
                                " LEFT OUTER JOIN dbo.BL_BusinessCenterCodes ON PA_Accounts.nBusinessCenterID = BL_BusinessCenterCodes.nBusinessCenterID " +
                                "WHERE   PA_Accounts.nPAccountID = " + accountId;

                oDB.Retrive_Query(_strSqlQuery, out dtAccountDetails);

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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dtAccountDetails;

        }

        /// <summary>
        /// Mehtod to check if the patient is receiving an “existing account” as a new account 
        /// and there are other previous accounts and 
        /// that account has no transactions in it at all and there are no other patients on that account 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public bool CheckTransactionsExistForAccountOrAnyPatientsForAccount(Int64 accountId, Int64 patientId)
        {
            object result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters=new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nPatientID", patientId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", accountId, ParameterDirection.Input, SqlDbType.BigInt);
                result = oDB.ExecuteScalar("PA_AccountToBeRemoved", oParameters); 
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }
            if (Convert.ToInt64(result) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Method to get the guarantor details by guarantorId
        /// </summary>
        /// <param name="_nGuarantorId"></param>
        /// <returns></returns>
        public DataTable GetGuarantorDetailsById(Int64 _nGuarantorId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtGuarantorDetails = new DataTable();
            try
            {
                oDB.Connect(false);

                string _strSqlQuery = "Select nPatientID, nPatientContactID, nLineNumber, nPatientContactType, sFirstName, sMiddleName, sLastName" +
                         ",nDOB, sGender, sRelation, sAddressLine1, sAddressLine2, sCity, sState, sZIP, sPhone, sMobile, sFax, sEmail" +
                         ", bIsActive, nVisitID, nAppointmentID, nGuarantorAsPatientID, nClinicID, nPatientContactTypeFlag, sSSN, sCounty,sCountry,nGuarantorType " +
                         "From Patient_OtherContacts WITH (NOLOCK) " +
                         "Where nPatientContactID = " + _nGuarantorId;

                oDB.Retrive_Query(_strSqlQuery, out dtGuarantorDetails);

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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dtGuarantorDetails;
        }

        /// <summary>
        /// Method to check account exist or not for guarantor.
        /// </summary>
        /// <param name="guarantorContactId"></param>
        /// <returns></returns>
        public bool CheckAccountExistsForGuarantor(Int64 guarantorContactId)
        {
            object result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);

                string _strSqlQuery = "Select count(*) from PA_Accounts WITH (NOLOCK) where nGuarantorId=" + guarantorContactId;
                result = oDB.ExecuteScalar_Query(_strSqlQuery);
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            if (Convert.ToInt64(result) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method to delete account
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="patientId"></param>
        public void DeletePatientAccount(Int64 accountId, Int64 patientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nPatientID", patientId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", accountId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("PA_DeletePatientAccount", oParameters);
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }
           
        }

        public DataTable GetAccountGuarantors(Int64 accountId, Int64 clinicId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtAccountGuarantors = new DataTable();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nPAccountID", accountId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", clinicId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_GetAccountGuarantors", oParameters, out dtAccountGuarantors);
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }
            return dtAccountGuarantors;
        }

        public bool GetPatientAccountFeatureSetting()
        {

            //return gloGlobal.gloPMGlobal.IsAccountsOn;

            object result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                string _strSqlQuery = "SELECT ISNULL(sSettingValue,'') AS sSettingsValue FROM settings_replication WITH (NOLOCK) WHERE sSettingName='Patient Account Feature'";
                result = oDB.ExecuteScalar_Query(_strSqlQuery);

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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            if (result.ToString().Trim().Length == 0)
                result = 0;
            return Convert.ToBoolean(result);
        }

        public bool GetAllowMultipleGuarantorsFeatureSetting()
        {

            object result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                //string _strSqlQuery = "select ISNULL(sSettingsValue,'') AS sSettingsValue from settings where sSettingsName='Allow Multiple Guarantor'";
                string _strSqlQuery = "select ISNULL(sSettingValue,'') AS sSettingsValue from settings_replication WITH (NOLOCK) where sSettingName='Allow Multiple Guarantor'";

                result = oDB.ExecuteScalar_Query(_strSqlQuery);

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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            if (result.ToString().Trim().Length == 0)
                result = 0;
            return Convert.ToBoolean(result);
        }

        public bool GetCopyAccountFeatureSetting()
        {

            object result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                //string _strSqlQuery = "select ISNULL(sSettingsValue,'') AS sSettingsValue from settings where sSettingsName='Copy Account'";
                string _strSqlQuery = "SELECT ISNULL(sSettingValue,'') AS sSettingsValue FROM settings_replication WITH (NOLOCK) WHERE sSettingName='Patient Account Feature'";
                result = oDB.ExecuteScalar_Query(_strSqlQuery);

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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            if (result.ToString().Trim().Length == 0)
                result = 0;
            return Convert.ToBoolean(result);
        }

        public bool GetMergeAccountFeatureSetting()
        {

            object result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                //string _strSqlQuery = "select ISNULL(sSettingsValue,'') AS sSettingsValue from settings where sSettingsName='Merge Account'";
                string _strSqlQuery = "SELECT ISNULL(sSettingValue,'') AS sSettingsValue FROM settings_replication WITH (NOLOCK) WHERE sSettingName='Patient Account Feature'";
                result = oDB.ExecuteScalar_Query(_strSqlQuery);

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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            if (result.ToString().Trim().Length == 0)
                result = 0;
            return Convert.ToBoolean(result);
        }

        public bool CheckTransactionsExistForAccountGuarantor(Int64 accountId, Int64 guarantorId)
        {
            object result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nPAccountID", accountId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nGuarantorId", guarantorId, ParameterDirection.Input, SqlDbType.BigInt);
                result = oDB.ExecuteScalar("PA_CheckTransactionsForAccountGuarantor", oParameters);
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }
            if (Convert.ToInt64(result) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void DeactivateAccountGuarantor(Int64 accountId, Int64 guarantorId)
        {
            
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                string _strSqlQuery = "Update Patient_OtherContacts WITH (READPAST) set bIsActive = 0 where nPAccountID =" + accountId + " and nPatientContactID =" + guarantorId;
                oDB.Execute_Query(_strSqlQuery);
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        public string GetAccountSequenceNumber(Int64 patientId,Int64 clinicId )
        {

            object result = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                string _strSqlQuery = "Select Top 1 PA.sAccountNo From PA_Accounts_Patients PAP WITH (NOLOCK) Inner Join PA_Accounts PA WITH (NOLOCK) "
	                                  +" On PA.nPAccountID = PAP.nPAccountID Where nPatientID ="+ patientId 
                                      +" AND ISNULL(PAP.nClinicID,1)="+ clinicId+" AND bIsOwnAccount = 1 "
                                      +" AND PAP.sPatientCode = (Select sPatientCode from Patient where nPatientID ="+ patientId+")" 
                                      +" Order by dtAccountCreatedDate Desc";
                result = oDB.ExecuteScalar_Query(_strSqlQuery);
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return result.ToString();
        }
        //account address validation
        public bool ChecktAccountAddressAvailable(Int64 _nPAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);         
            oDB.Connect(false);
            bool IsAddressAvialble = false;
            string sqlStr = "";

            try
            {
                if (_nPAccountID > 0)
                    sqlStr = "Select (Case When (len(ltrim(rtrim(sAddressLine1))) <> 0 and LEN(ltrim(rtrim(sCity))) <> 0 and LEN(ltrim(rtrim( sState))) <> 0 and  LEN(ltrim(rtrim(sZip))) <> 0) then 1 else 0 end) as IsAccountAddressAvailable "
                                   + " From PA_Accounts WITH (NOLOCK) where nPAccountID = " + _nPAccountID;

                object result = oDB.ExecuteScalar_Query(sqlStr);

                if (Convert.ToInt64(result) > 0)
                    IsAddressAvialble = true;
                else
                    IsAddressAvialble = false;
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
                }
            }
            return IsAddressAvialble;
        }

        public DataTable GetAccountDataByTransID(Int64 transactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = new DataTable();
            try
            {
                oDB.Connect(false);
                string _strSqlQuery = "Select nPAccountID,sAccountNo,"
                                      +"Case When (len(ltrim(rtrim(sAddressLine1))) <> 0 and LEN(ltrim(rtrim(sCity))) <> 0 and LEN(ltrim(rtrim( sState))) <> 0 and  LEN(ltrim(rtrim(sZip))) <> 0) then 1 else 0 end as IsAccountAddressAvailable"
                                      + " From PA_Accounts WITH (NOLOCK) where nPAccountID = (Select nPAccountID From BL_Transaction_Claim_MST where nTransactionID =" + transactionID + ")";
                oDB.Retrive_Query(_strSqlQuery, out dt);
                
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dt;
           
        }


        public static DataTable GetPatAccountNotes(Int64 nPatientID, Int64 nPatientAccountID, Int64 nPAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable dtPatientAccNotes = new DataTable();

            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nPatientId", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientAccountId", nPatientAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicId", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("BL_Get_PatientAccountNotes", oParameters, out dtPatientAccNotes);
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

            return dtPatientAccNotes;

        }

        public static Boolean SavePatAccNotes(Int64 nNoteID, Int64 nNoteDate, string sNotes, Int64 nPatientID, Int64 nPatientAccountID, Int64 nPAccountID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            int oResult = 0;
            bool _result = false;
            try
            {

                oParameters.Clear();
                oParameters.Add("@nNoteType", AccountNoteType.AccountNote.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nNoteId", nNoteID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nNoteDateTime", nNoteDate, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sNoteDescription", sNotes.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nAccountPatientID", nPatientAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oResult = oDB.Execute("PA_INUPAccountNotes", oParameters);
                oDB.Disconnect();
                ////Warning Removed at the time of Change made to solve memory Leak and word crash issue
                if (oResult == 0)
                {
                    _result = false;
                }
                else
                {
                    _result = true;
                }

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
            
            return _result;
        }

        public static Boolean SavePatAccNotes_Multiple(Int64 nNoteID, Int64 nNoteDate, string sNotes, DataTable dtBatchPatAccountID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            int oResult = 0;
            bool _result = false;
            try
            {

                oParameters.Clear();
                oParameters.Add("@nNoteType", AccountNoteType.AccountNote.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nNoteId", nNoteID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nNoteDateTime", nNoteDate, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sNoteDescription", sNotes.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@tvpLogSchedule_Multiple", dtBatchPatAccountID, ParameterDirection.Input, SqlDbType.Structured);
                oDB.Connect(false);
                oResult = oDB.Execute("PA_INUPAccountNotes_Multiple", oParameters);
                oDB.Disconnect();
                ////Warning Removed at the time of Change made to solve memory Leak and word crash issue
                if (oResult == 0)
                {
                    _result = false;
                }
                else
                {
                    _result = true;
                }

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

            return _result;
        }
        public static DataTable GetAccountDetails(Int64 AccountID, Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtPatients = null;
            try
            {
                if (AccountID > 0 && PatientID > 0)
                {
                    oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Connect(false);
                    oDB.Retrive("PA_Select_PatientsAccounts", oParameters, out dtPatients);
                    oDB.Disconnect();

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
                if (oParameters != null)
                    oParameters.Dispose();
            }
            return dtPatients;
        }


        public static DataTable GetAccountDetails(Int64 AccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtPatients = null;
            try
            {
                if (AccountID > 0)
                {
                    oParameters.Add("@nPatientID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Connect(false);
                    oDB.Retrive("PA_Select_PatientsAccounts", oParameters, out dtPatients);
                    oDB.Disconnect();

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
                if (oParameters != null)
                    oParameters.Dispose();
            }
            return dtPatients;
        }

    #endregion

       
    }

    #endregion "gloAccount Class"

    //This Class is used to hold Account Object
    #region "Account Class"

    public class Account : IDisposable
    {

        #region "Constructor & Distructor"

        private bool disposed = false;

        public Account()
        {
        }

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

        ~Account()
        {
            Dispose(false);
        }

        #endregion

        #region "PrivateVariable Declaration"

        private Int64 _nPAccountID;
        private string _sAccountNo;
        private string _sAccountDesc;
        private Int64 _nGuarantorID;
        private string _sGuarantorCode;
        private DateTime _dtAccountClosedDate;
        private string _sFirstName;
        private string _sMiddleName;
        private string _sLastName;
        private Int64 _nEntityType;
        private string _sAddressLine1;
        private string _sAddressLine2;
        private string _sCity;
        private string _sState;
        private string _sZip;
        private string _sCountry;
        private string _sCounty;
        private string _sAreaCode;
        private bool _bIsExcludeStatement;
        private bool _bIsSentToCollection;
        private Int64 _nClinicID;
        private Int64 _nSiteID;
        private string _sMachineName;
        private Int64 _nUserID;
        private DateTime _dtRecordDate;
        private bool _bIsActive;
        private string _sGuID;
        private bool _bIsAccountFeatureEnabled;
        private bool _bIsExistingAccount;
        private bool _IsDataModified;

        #endregion

        #region "Properties"

        public Int64 PAccountID
        {
            get { return _nPAccountID; }

            set { _nPAccountID = value; }

        }

        public string AccountNo
        {
            get { return _sAccountNo; }

            set { _sAccountNo = value; }

        }

        public string AccountDesc
        {
            get { return _sAccountDesc; }

            set { _sAccountDesc = value; }
        }

        public Int64 GuarantorID
        {
            get { return _nGuarantorID; }

            set { _nGuarantorID = value; }

        }

        public string GuarantorCode
        {
            get { return _sGuarantorCode; }

            set { _sGuarantorCode = value; }

        }

        public DateTime AccountClosedDate
        {
            get { return _dtAccountClosedDate; }

            set { _dtAccountClosedDate = value; }

        }

        public string FirstName
        {
            get { return _sFirstName; }

            set { _sFirstName = value; }

        }

        public string MiddleName
        {
            get { return _sMiddleName; }

            set { _sMiddleName = value; }
        }

        public string LastName
        {
            get { return _sLastName; }

            set { _sLastName = value; }

        }

        public Int64 EntityType
        {
            get { return _nEntityType; }

            set { _nEntityType = value; }

        }

        public string AddressLine1
        {
            get { return _sAddressLine1; }

            set { _sAddressLine1 = value; }

        }

        public string AddressLine2
        {
            get { return _sAddressLine2; }

            set { _sAddressLine2 = value; }

        }

        public string City
        {
            get { return _sCity; }

            set { _sCity = value; }

        }

        public string State
        {
            get { return _sState; }

            set { _sState = value; }

        }

        public string Zip
        {
            get { return _sZip; }

            set { _sZip = value; }

        }

        public string Country
        {
            get { return _sCountry; }

            set { _sCountry = value; }

        }

        public string County
        {
            get { return _sCounty; }

            set { _sCounty = value; }

        }

        public string AreaCode
        {
            get { return _sAreaCode; }

            set { _sAreaCode = value; }

        }

        public bool ExcludeStatement
        {
            get { return _bIsExcludeStatement; }

            set { _bIsExcludeStatement = value; }

        }

        public bool SentToCollection
        {
            get { return _bIsSentToCollection; }

            set { _bIsSentToCollection = value; }

        }

        public Int64 ClinicID
        {
            get { return _nClinicID; }

            set { _nClinicID = value; }

        }

        public Int64 SiteID
        {
            get { return _nSiteID; }

            set { _nSiteID = value; }

        }

        public string MachineName
        {
            get { return _sMachineName; }

            set { _sMachineName = value; }

        }

        public Int64 UserID
        {
            get { return _nUserID; }

            set { _nUserID = value; }

        }

        public DateTime RecordDate
        {
            get { return _dtRecordDate; }

            set { _dtRecordDate = value; }

        }

        public bool Active
        {
            get { return _bIsActive; }

            set { _bIsActive = value; }

        }

        public string GuID
        {
            get { return _sGuID; }
            set { _sGuID = value; }
        }

        public bool IsAccountFeatureEnabled
        {
            get { return _bIsAccountFeatureEnabled; }

            set { _bIsAccountFeatureEnabled = value; }

        }
        public bool IsExistingAccount
        {
            get { return _bIsExistingAccount; }
            set { _bIsExistingAccount = value; }
        }

        public bool IsDataModified
        {
            get { return _IsDataModified; }
            set { _IsDataModified = value; }
        }

        public Int64 nBusinessCenterID
        { get; set; }

        #endregion

    }

    #endregion "Account Class "

    //This Class is used to hold Account Patients Object
    #region "PatientAccount Class"

    public class PatientAccount
    {

        #region "Constructor & Distructor"

        private bool disposed = false;

        public PatientAccount()
        {

        }

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

        ~PatientAccount()
        {
            Dispose(false);
        }

        #endregion

        #region "PrivateVariable Declaration"

        private Int64 _nAccountPatientID;
        private Int64 _nPAccountID;
        private Int64 _nPatientID;
        private string _sAccountNo;
        private string _sPatientCode;
        private DateTime _dtAccountClosedDate;
        private Int64 _nClinicID;
        private Int64 _nSiteID;
        private string _sMachineName;
        private Int64 _nUserID;
        private DateTime _dtRecordDate;
        private bool _bIsActive;
        private bool _bIsOwnAccount;

        #endregion

        #region "Properties"

        public Int64 AccountPatientID
        {
            get { return _nAccountPatientID; }

            set { _nAccountPatientID = value; }

        }

        public Int64 PAccountID
        {
            get { return _nPAccountID; }

            set { _nPAccountID = value; }

        }

        public Int64 PatientID
        {
            get { return _nPatientID; }

            set { _nPatientID = value; }

        }

        public string AccountNo
        {
            get { return _sAccountNo; }

            set { _sAccountNo = value; }

        }

        public string PatientCode
        {
            get { return _sPatientCode; }

            set { _sPatientCode = value; }

        }

        public DateTime AccountClosedDate
        {
            get { return _dtAccountClosedDate; }
            set { _dtAccountClosedDate = value; }
        }

        public Int64 ClinicID
        {
            get { return _nClinicID; }

            set { _nClinicID = value; }

        }

        public Int64 SiteID
        {
            get { return _nSiteID; }

            set { _nSiteID = value; }

        }

        public string MachineName
        {
            get { return _sMachineName; }

            set { _sMachineName = value; }

        }

        public Int64 UserID
        {
            get { return _nUserID; }

            set { _nUserID = value; }

        }

        public DateTime RecordDate
        {
            get { return _dtRecordDate; }

            set { _dtRecordDate = value; }

        }

        public bool Active
        {
            get { return _bIsActive; }

            set { _bIsActive = value; }

        }

        public bool OwnAccount
        {
            get { return _bIsOwnAccount; }
            set { _bIsOwnAccount = value; }
        }

        #endregion

    }

    #endregion "PatientAccount Class"

    //This Class is a collection which holds AccountsPatients Objects
    #region "PatientAccounts Class"

    public class PatientAccounts : IDisposable
    {
        
        #region "Variable Declaration"

        protected ArrayList _innerlist;
        private bool disposed = false;

        #endregion "Variable Declaration"

        #region "Constructor & Distructor"

        public PatientAccounts()
        {
            _innerlist = new ArrayList();
        }

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

        ~PatientAccounts()
        {
            Dispose(false);
        }

        #endregion

        #region "Methods"

        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(PatientAccount item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(PatientAccount item)
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

        public PatientAccount this[int index]
        {
            get
            { return (PatientAccount)_innerlist[index]; }
        }

        public bool Contains(PatientAccount item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(PatientAccount item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(PatientAccount[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion "Methods"

    }

    #endregion "PatientAccounts Class"


    public static class PatientMergeAccounts
    {
        public static void MergePatientAccountsForEMR(Int64 DestinationPatientId, Int64 ClinicId, string DatabaseConnectionString, string DestPatientCode, Int64 UserID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();
            DataTable dtPatientAccounts;
            Int64 fromAccountID = 0;
            Int64 toAccountID = 0;
            bool _IsPatientAccountFeature = false;
            gloAccount objgloAccount = null;

            try
            {
                objgloAccount = new gloAccount(DatabaseConnectionString);
                _IsPatientAccountFeature = objgloAccount.GetPatientAccountFeatureSetting();
                if (objgloAccount != null) { objgloAccount.Dispose(); }
                if (_IsPatientAccountFeature == false)
                {
                    oDB.Connect(false);
                    string _strSqlQuery = "Select nAccountPatientId, nPAccountID, nPatientID," +
                                      " sAccountNo  as sAccountNo, " +
                                      " sPatientCode, dtAccountClosedDate," +
                                      " nClinicID, nSiteID, sMachineName, nUserID, dtRecordDate, bIsActive,bIsOwnAccount" +
                                      " From PA_Accounts_Patients" +
                                      " Where nPatientID = " + Convert.ToInt64(DestinationPatientId) + " AND ISNULL(nClinicID,1) = " + ClinicId;

                    oDB.Retrive_Query(_strSqlQuery, out dtPatientAccounts);

                    if (dtPatientAccounts != null && dtPatientAccounts.Rows.Count > 0)
                    {
                        if (dtPatientAccounts.Rows.Count == 2)
                        {

                            for (int i = 0; i < dtPatientAccounts.Rows.Count; i++)
                            {
                                if (Convert.ToBoolean(dtPatientAccounts.Rows[i]["bIsOwnAccount"].ToString()) == true)
                                {
                                    if (DestPatientCode == Convert.ToString(dtPatientAccounts.Rows[i]["sPatientCode"]))
                                    {
                                        toAccountID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nPAccountID"].ToString());
                                    }
                                    else
                                    {
                                        fromAccountID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nPAccountID"].ToString());
                                    }
                                }
                            }

                            if (fromAccountID > 0 && toAccountID > 0)
                            {
                                string machineName = System.Environment.MachineName;

                                try
                                {
                                    odbParams.Add("@From_nPAccountID", fromAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@To_nPAccountID", toAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@MachineName", machineName, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nClinicID", ClinicId, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDB.Execute("PA_MergeAccount", odbParams);
                                }
                                catch (gloDatabaseLayer.DBException ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                }
                                catch (Exception gex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);
                                }
                                finally
                                {

                                }
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog("Merging of accounts failed in Merge Patient activity for Pat. ID : " + Convert.ToString(DestinationPatientId) + "", false);
                            }
                        }

                    }
                }
            }
            catch //(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Merging of accounts failed in Merge Patient activity for Pat. ID : " + Convert.ToString(DestinationPatientId) + "", false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (odbParams != null) { odbParams.Dispose(); odbParams = null; }
            }

        }
    }
}

#endregion
