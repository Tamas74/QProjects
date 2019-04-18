using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using gloSettings;
using gloGlobal;
using gloBilling.Statement;
using System.Collections;
using System.Windows.Forms;

namespace gloBilling.Collections
{
    #region "Follow Up Code"

    public class CL_FollowUpCode
    {
        #region " Declarations "

        Int64 _ClinicID = 0;
        string _messageBoxCaption = "";
        string _databaseconnectionstring = "";



        #endregion " Declarations "

        #region "Constructor & Destructor"

        public CL_FollowUpCode()
        {
            _databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
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

        ~CL_FollowUpCode()
        {
            Dispose(false);
        }

        #endregion

        #region " Public & Private Methods "

        public static bool IsFollowUpFeatureON()
        {
            GeneralSettings oSettings = null;
            bool SettingsValue = false;
            try
            {
                oSettings = new GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                object oValue = null;
                oSettings.GetSetting("FOLLOWUP_FEATURE", 0, gloPMGlobal.ClinicID, out oValue);
                Boolean.TryParse(Convert.ToString(oValue), out SettingsValue);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSettings != null) { oSettings.Dispose(); }
            }
            return SettingsValue;
        }

        public static DateTime GetServerDate()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            object oCurrentDate = null;
            DateTime currentDate = DateTime.Now;
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = "SELECT CONVERT(DATE,dbo.gloGetDate()) dtCurrentDate";
                oCurrentDate=oDB.ExecuteScalar_Query(strQuery);
                if (oCurrentDate != null)
                {
                    currentDate = Convert.ToDateTime(oCurrentDate);
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
            }
            return currentDate;

        }

        public static DataTable GetAccountFollowUpTimeStamp(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtcurrentDate = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = "SELECT MAX(dtCreatedDateTime) AS dtCreatedDateTime FROM CL_Followupschedule_Acct WHERE nAccountID = " + PAccountID;
                oDB.Retrive_Query(strQuery, out dtcurrentDate);
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
            return dtcurrentDate;

        }

        public static DataTable GetBadDebtAccountFollowUpTimeStamp(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtcurrentDate = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = "SELECT MAX(dtCreatedDateTime) AS dtCreatedDateTime FROM CL_FollowupSchedule_BadDebt WHERE nAccountID = " + PAccountID;
                oDB.Retrive_Query(strQuery, out dtcurrentDate);
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
            return dtcurrentDate;

        }

        public static DataTable GetAccountFollowUpLogTimeStamp(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtcurrentDate = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = "SELECT MAX(dtCreatedDateTime) AS dtCreatedDateTime FROM CL_FollowupLog_Acct WHERE nAccountID = " + PAccountID;
                oDB.Retrive_Query(strQuery, out dtcurrentDate);
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
            return dtcurrentDate;

        }

        public static DataTable GetBadDebtAccountFollowUpLogTimeStamp(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtcurrentDate = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = "SELECT MAX(dtCreatedDateTime) AS dtCreatedDateTime FROM CL_FollowupLog_BadDebt WHERE nAccountID = " + PAccountID;
                oDB.Retrive_Query(strQuery, out dtcurrentDate);
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
            return dtcurrentDate;

        }

        public static DataTable GetClaimFollowUpTimeStamp(Int64 nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtcurrentDate = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = "SELECT MAX(dtCreatedDateTime) AS dtCreatedDateTime FROM CL_FollowupSchedule_Claim WHERE nTransactionID = " + nTransactionID;// +" AND nTransactionMasterID = " + nTransactionMasterID;
                oDB.Retrive_Query(strQuery, out dtcurrentDate);
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
            return dtcurrentDate;
        }

        public static DataTable GetClaimFollowUpLogTimeStamp(Int64 nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtcurrentDate = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = "SELECT MAX(dtCreatedDateTime) AS dtCreatedDateTime FROM CL_FollowupLog_Claim WHERE nTransactionID = " + nTransactionID;// +" AND nTransactionMasterID = " + nTransactionMasterID;
                oDB.Retrive_Query(strQuery, out dtcurrentDate);
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
            return dtcurrentDate;
        }

        public static string GetActionDesc(string sAction,CollectionEnums.FollowUpType enmFollowUpType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dt = null;
            string sAcctionDesc = string.Empty;
            string strQuery = string.Empty;
            try
            {
                oDB.Connect(false);                
                strQuery = "SELECT sFollowUpActionDescription FROM CL_FollowUpAction_Mst WHERE sFollowUpActionCode = '" + sAction + "' AND nFollowUpActionType = " + (int)enmFollowUpType;

                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    sAcctionDesc = Convert.ToString(dt.Rows[0]["sFollowUpActionDescription"]);
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
            }
            return sAcctionDesc;

        }

        public static string GetClaimFollowUpStatus(Int64 transactionMstID,Int64 transactionID, Int64 nContactID)
        {
            string sMessage = string.Empty;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dt = new DataTable();
            string sStatus = string.Empty;
            try
            {
                oDB.Connect(false);
                string _strSqlQuery = " SELECT dbo.GetClaimFollowUpStatus(" + transactionMstID + "," + transactionID + "," + nContactID + ")";
                oDB.Retrive_Query(_strSqlQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    sStatus = Convert.ToString(dt.Rows[0][0]);
                }
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
            return sStatus;
        } 

        public DataTable GetFollowUpCode(Int64 Id, CollectionEnums.FollowUpType nType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtFollowUpCode = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                if (Id > 0)
                {
                    strQuery = "select ISNULL(nFollowUpActionID,0) AS nFollowUpActionID,ISNULL(sFollowUpActionCode,'') AS sFollowUpActionCode,ISNULL(sFollowUpActionDescription,'') AS sFollowUpActionDescription,ISNULL(sDefNextActionFollowUpCode,'') AS sDefNextActionFollowUpCode,ISNULL(sDefNextActionFollowUpDescription,'') AS sDefNextActionFollowUpDescription,nFollowUpActionDays,bIsSystemType,case bIsSystemType when 0 then 'User' else 'System' End as sRecordType, nTemplateID from CL_FollowUpAction_Mst WITH (NOLOCK) where nFollowUpActionID= " + Id + " and nFollowUpActionType=" + nType.GetHashCode() + "  order by sFollowUpActionCode";
                }
                else
                {
                    strQuery = "select ISNULL(nFollowUpActionID,0) AS nFollowUpActionID,ISNULL(sFollowUpActionCode,'') AS sFollowUpActionCode,ISNULL(sFollowUpActionDescription,'') AS sFollowUpActionDescription,ISNULL(sDefNextActionFollowUpCode,'') AS sDefNextActionFollowUpCode,ISNULL(sDefNextActionFollowUpDescription,'') AS sDefNextActionFollowUpDescription,nFollowUpActionDays,bIsSystemType,case bIsSystemType when 0 then 'User' else 'System' End as sRecordType, nTemplateID from CL_FollowUpAction_Mst WITH (NOLOCK) Where nFollowUpActionType=" + nType.GetHashCode() + " order by sFollowUpActionCode";
                }
                oDB.Retrive_Query(strQuery, out dtFollowUpCode);
                oDB.Disconnect();
                return dtFollowUpCode;
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtFollowUpCode != null) { dtFollowUpCode.Dispose(); }
            }
        }

        public DataTable GetTemplateName(Int64 TemplateID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTemplate = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                if (TemplateID > 0)
                {
                    strQuery = " SELECT  ISNULL(sTemplateName, '') AS TemplateName FROM TemplateGallery_MST WITH ( NOLOCK ) WHERE nTemplateID = " + TemplateID;
                }
                oDB.Retrive_Query(strQuery, out dtTemplate);
                oDB.Disconnect();
                return dtTemplate;
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtTemplate != null) { dtTemplate.Dispose(); }
            }
        }

        public static DataTable GetContactAndInsuranceDetails(Int64 nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtcurrentDate = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = "SELECT nContactID , nInsuranceID  FROM BL_Transaction_Claim_MST WITH(NOLOCK) WHERE nTransactionID = " + nTransactionID;// +" AND nTransactionMasterID = " + nTransactionMasterID;
                oDB.Retrive_Query(strQuery, out dtcurrentDate);
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
            return dtcurrentDate;
        }

        public Int64 AddModifyFollowUpCode(Int64 Id, string sCode, string sDesc, string sDefFollowUpCode, string sDefFollowUpDesc, string nFollowUpDays, CollectionEnums.FollowUpType nFollowUpType, Int64 @nTemplateID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = new object();
            try
            {
                oDB.Connect(false);

                oParameters.Add("@nFollowUpActionID", Id, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sFollowUpActionCode", sCode.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sFollowUpActionDescription", sDesc.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sDefFollowUpActionCode", sDefFollowUpCode.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sDefFollowUpActionDescription", sDefFollowUpDesc.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                if (nFollowUpDays != "")
                {
                    oParameters.Add("@nFollowUpActionDays", nFollowUpDays, ParameterDirection.Input, SqlDbType.Int);
                }
                else
                {
                    oParameters.Add("@nFollowUpActionDays", DBNull.Value, ParameterDirection.Input, SqlDbType.Int);
                }
                oParameters.Add("@nFollowUpActionType", nFollowUpType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@bIsSystemType", 0, ParameterDirection.Input, SqlDbType.Int);
                if (nTemplateID > 0)
                {
                    oParameters.Add("@nTemplateID", nTemplateID, ParameterDirection.Input, SqlDbType.BigInt);
                }
                else
                {
                    oParameters.Add("@nTemplateID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                }
                
                oDB.Execute("CL_INUP_FollowUpActionCode", oParameters, out _oResult);
                oDB.Disconnect();
                return Convert.ToInt64(_oResult);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

        public bool VerifyBforDeleteFollowUpCode(CollectionEnums.FollowUpType nType, string sFolowUpCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                //Check if system defined follow-up action for BadDebt
                if (nType == CollectionEnums.FollowUpType.BadDebt)
                {
                    strQuery = "Select ISNULL(bIsSystemType,0) bIsSystemType FROM CL_FollowUpAction_Mst where sFollowUpActionCode ='" + sFolowUpCode.Trim().Replace("'", "''") + "' and nFollowUpActionType = 3";

                    int resultAdmin = Convert.ToInt32(oDB.ExecuteScalar_Query(strQuery));
                    if (resultAdmin > 0)
                    {
                        return true;
                    }
                    strQuery = "";
                }


                strQuery = "Select Count(nFollowUpActionID) from CL_FollowUpAction_Mst where sDefNextActionFollowUpCode ='" + sFolowUpCode.Trim().Replace("'","''") + "' and nFollowUpActionType=" + nType.GetHashCode();
                int result = Convert.ToInt32(oDB.ExecuteScalar_Query(strQuery));
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    strQuery = "";

                    if (nType == CollectionEnums.FollowUpType.Claim)
                    {
                        strQuery = "Select Count(nSettingsID) from settings where sSettingsValue ='" + sFolowUpCode.Trim().Replace("'", "''") + "' and sSettingsName in('CL_INSCLM_START_DEFFUACTION','CL_INSCLM_REBILL_DEFFUACTION')";
                    }
                    else if (nType == CollectionEnums.FollowUpType.PatientAccount)
                    {
                        strQuery = "Select Count(nSettingsID) from settings where sSettingsValue ='" + sFolowUpCode.Trim().Replace("'", "''") + "' and sSettingsName in('CL_PATACCT_START_FUACTION','CL_PMNTPLAN_DEF_FUACTION','EXTERNALCOLLECTIONFUACTION')";
                    }
                    else if (nType == CollectionEnums.FollowUpType.BadDebt)
                    {
                        strQuery = "Select Count(nSettingsID) from settings where sSettingsValue ='" + sFolowUpCode.Trim().Replace("'", "''") + "' and sSettingsName in('CL_BADDEBT_START_FUACTION')";
                    }

                    int resultAdmin = Convert.ToInt32(oDB.ExecuteScalar_Query(strQuery));
                    if (resultAdmin > 0)
                    {
                        return true;
                    }

                    strQuery = "";

                    //Check if Follow up Used in Any Transactions
                    if (nType == CollectionEnums.FollowUpType.Claim)
                    {
                        strQuery = "SELECT  COUNT(sFollowupCode) AS AcctFollowupUsedinTransCnt FROM    CL_FollowupSchedule_Claim WHERE   sFollowupCode = '" + sFolowUpCode.Trim().Replace("'", "''") + "'";
                    }
                    else if(nType == CollectionEnums.FollowUpType.PatientAccount)
                    {
                        strQuery = "SELECT  COUNT(sFollowupCode) AS AcctFollowupUsedinTransCnt FROM    CL_FollowupSchedule_Acct WHERE   sFollowupCode = '" + sFolowUpCode.Trim().Replace("'", "''") + "'";
                    }
                    else if (nType == CollectionEnums.FollowUpType.BadDebt)
                    {
                        strQuery = "SELECT  COUNT(sFollowupCode) AS AcctFollowupUsedinTransCnt FROM    CL_FollowupSchedule_BadDebt WHERE   sFollowupCode = '" + sFolowUpCode.Trim().Replace("'", "''") + "'";
                    }

                    int resultTransactions = Convert.ToInt32(oDB.ExecuteScalar_Query(strQuery));
                    if (resultTransactions > 0)
                    {
                        return true;
                    }

                    strQuery = "";
                    //Check if Follow up Used in Any Log Transactions
                    if (nType == CollectionEnums.FollowUpType.Claim)
                    {
                        strQuery = "SELECT  COUNT(sFollowupCode) AS AcctFollowupUsedinTransCnt FROM    CL_FollowupLog_Claim WHERE   sFollowupCode = '" + sFolowUpCode.Trim().Replace("'", "''") + "'";
                    }
                    else if(nType == CollectionEnums.FollowUpType.PatientAccount)
                    {
                        strQuery = "SELECT  COUNT(sFollowupCode) AS AcctFollowupUsedinTransCnt FROM    CL_FollowupLog_Acct WHERE   sFollowupCode = '" + sFolowUpCode.Trim().Replace("'", "''") + "'";
                    }
                    else if (nType == CollectionEnums.FollowUpType.BadDebt)
                    {
                        strQuery = "SELECT  COUNT(sFollowupCode) AS AcctFollowupUsedinTransCnt FROM    CL_FollowupLog_BadDebt WHERE   sFollowupCode = '" + sFolowUpCode.Trim().Replace("'", "''") + "'";
                    }

                    int resultLogTransactions = Convert.ToInt32(oDB.ExecuteScalar_Query(strQuery));
                    if (resultLogTransactions > 0)
                    {
                        return true;
                    }


                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return false;

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
        }

        public bool VerifyBeforeDeactivateFollowUpCode(string sCode,CollectionEnums.FollowUpType nType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = new object();
            bool result = false;

            try
            {
                oDB.Connect(false);
                oParameters.Add("@sFollowUpActionCode", sCode.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nFollowUpActionType", nType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                _oResult= oDB.ExecuteScalar("CL_VerifyBeforeDeactivateFollowUpCode",oParameters);
                oDB.Disconnect();
                return result= Convert.ToBoolean(_oResult);
            }
            catch(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }

            }
        }
        public DataTable GetFollowUpCodeVBB(Int64 Id, CollectionEnums.FollowUpType nType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtFollowUpCode = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                if (Id > 0)
                {
                    strQuery = "select ISNULL(nFollowUpActionID,0) AS nFollowUpActionID,ISNULL(sFollowUpActionCode,'') AS sFollowUpActionCode,ISNULL(sFollowUpActionDescription,'') AS sFollowUpActionDescription, "+
                    " ISNULL(( SELECT TOP 1 "+
                    " ISNULL(sTemplateName, '') "+
                    " FROM      dbo.TemplateGallery_MST "+
                    " WHERE     TemplateGallery_MST.nTemplateID = CL_FollowUpAction_Mst.nTemplateID " +
                    " ),'') AS sTemplateName, "+
                    " case when ISNULL(sDefNextActionFollowUpCode,'')='' then '' else ISNULL(sDefNextActionFollowUpCode,'') +'-'+ISNULL(sDefNextActionFollowUpDescription,'') END AS sDefNextAction,nFollowUpActionDays "+
                    " ,ISNULL(bIsSystemType,0) bIsSystemType,case when ISNULL(bIsActive,0)=0 then 'Inactive' else 'Active' END AS [Status] " +
                    " from CL_FollowUpAction_Mst WITH (NOLOCK) where nFollowUpActionID= " + Id + " and nFollowUpActionType=" + nType.GetHashCode() + "  order by sFollowUpActionCode";
                }
                else
                {
                    strQuery = "select ISNULL(nFollowUpActionID,0) AS nFollowUpActionID,ISNULL(sFollowUpActionCode,'') AS sFollowUpActionCode,ISNULL(sFollowUpActionDescription,'') AS sFollowUpActionDescription, ISNULL(( SELECT TOP 1 " +
                    " ISNULL(sTemplateName, '') " +
                    " FROM      dbo.TemplateGallery_MST " +
                    " WHERE     TemplateGallery_MST.nTemplateID = CL_FollowUpAction_Mst.nTemplateID " +
                    " ),'') AS sTemplateName, case when ISNULL(sDefNextActionFollowUpCode,'')='' then '' else  ISNULL(sDefNextActionFollowUpCode,'')+'-'+ISNULL(sDefNextActionFollowUpDescription,'') END AS sDefNextAction,nFollowUpActionDays " +
                    " ,ISNULL(bIsSystemType,0) bIsSystemType,case when ISNULL(bIsActive,0)=0 then 'Inactive' else 'Active' END AS [Status] " +
                    "  from CL_FollowUpAction_Mst WITH (NOLOCK) Where nFollowUpActionType=" + nType.GetHashCode() + " order by sFollowUpActionCode";
                }
                oDB.Retrive_Query(strQuery, out dtFollowUpCode);
                oDB.Disconnect();
                return dtFollowUpCode;
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtFollowUpCode != null) { dtFollowUpCode.Dispose(); }
            }
        }

        public bool IsExistsFollowUpCode(Int64 Id, string Code, CollectionEnums.FollowUpType nType)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            Int64 _intResult = 0;
            try
            {
                oDB.Connect(false);
                strQuery = " select count(nFollowUpActionID) from CL_FollowUpAction_Mst WITH (NOLOCK) where sFollowUpActionCode='" + Code.Replace("'", "''") + "' and nFollowUpActionID <> " + Id + " and nFollowUpActionType=" + nType.GetHashCode();
                _intResult = Convert.ToInt64(oDB.ExecuteScalar_Query(strQuery));
                if (_intResult > 0)
                {
                    _result = true;
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
            }
            return _result;

        }

        public DataTable getFollowUpAction(CollectionEnums.FollowUpType nType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtFollowUpCode = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = "select nFollowUpActionID,sFollowUpActionCode,sFollowUpActionDescription,sFollowUpActionCode+'-'+sFollowUpActionDescription as sFollowUpAction,sDefNextActionFollowUpCode,nFollowUpActionDays,nFollowUpActionType from CL_FollowUpAction_Mst WITH (NOLOCK) where ISNULL(bIsActive,0)=1 and nFollowUpActionType=" + nType.GetHashCode() + "  order by sFollowUpAction ";
                oDB.Retrive_Query(strQuery, out dtFollowUpCode);
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
            return dtFollowUpCode;
        }

        public DataTable getFollowUpActionWithCurrentSchedule(CollectionEnums.FollowUpType nType,Int64 nScheduleID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtFollowUpCode = null;
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nScheduleID", nScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@FollowUPType", (int)nType, ParameterDirection.Input, SqlDbType.Int);
                oDB.Retrive("CL_getFollowCodeWithSchedule", oParameters, out dtFollowUpCode);
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
            return dtFollowUpCode;
        }

        public DataTable getFollowUpActionWithCurrentScheduleTemplate(CollectionEnums.FollowUpType nType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtFollowUpCode = null;
            try
            {
                oDB.Connect(false);
                oParameters.Add("@FollowUPType", (int)nType, ParameterDirection.Input, SqlDbType.Int);
                oDB.Retrive("CL_getFollowCodeWithScheduleTemplate", oParameters, out dtFollowUpCode);
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
            return dtFollowUpCode;
        }

        public DataTable getFollowUpActionTemplates(CollectionEnums.FollowUpType nType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtFollowUpCode = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = "select DISTINCT CL_FollowUpAction_Mst.nTemplateID,TemplateGallery_MST.sTemplateName AS stemplateNAME  from CL_FollowUpAction_Mst INNER JOIN dbo.TemplateGallery_MST ON CL_FollowUpAction_Mst.nTemplateID=TemplateGallery_MST.nTemplateID WHERE nFollowUpActionType=" + nType.GetHashCode() + "  order by TemplateGallery_MST.sTemplateName";
                oDB.Retrive_Query(strQuery, out dtFollowUpCode);
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
            return dtFollowUpCode;
        }

        public DataTable fillFollowUpAction(CollectionEnums.FollowUpType nType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtFollowUpCode = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                if (nType == CollectionEnums.FollowUpType.Claim)
                {
                    strQuery = "select sFollowUpActionCode,sFollowUpActionCode+'-'+sFollowUpActionDescription as sFollowUpAction from CL_FollowUpAction_Mst WITH (NOLOCK) where nFollowUpActionType=" + nType.GetHashCode() + "  Union select sFollowUpCode as sFollowUpActionCode,sFollowUpCode+'-'+sFollowUpDescription as sFollowUpAction from CL_FollowupSchedule_Claim order by sFollowUpActionCode";
                }
                else if (nType == CollectionEnums.FollowUpType.PatientAccount)
                {
                    strQuery = "select sFollowUpActionCode,sFollowUpActionCode+'-'+sFollowUpActionDescription as sFollowUpAction from CL_FollowUpAction_Mst WITH (NOLOCK) where nFollowUpActionType=" + nType.GetHashCode() + "  Union select sFollowUpCode as sFollowUpActionCode,sFollowUpCode+'-'+sFollowUpDescription as sFollowUpAction from CL_FollowupSchedule_Acct order by sFollowUpActionCode";
                }
                else if (nType == CollectionEnums.FollowUpType.BadDebt)
                {
                    strQuery = "select sFollowUpActionCode,sFollowUpActionCode+'-'+sFollowUpActionDescription as sFollowUpAction from CL_FollowUpAction_Mst WITH (NOLOCK) where nFollowUpActionType=" + nType.GetHashCode() + "  Union select sFollowUpCode as sFollowUpActionCode,sFollowUpCode+'-'+sFollowUpDescription as sFollowUpAction from CL_FollowupSchedule_BadDebt order by sFollowUpActionCode";
                }

                oDB.Retrive_Query(strQuery, out dtFollowUpCode);
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
            return dtFollowUpCode;
        }

        public bool DeleteFollowUpCode(Int64 ID, CollectionEnums.FollowUpType nType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from CL_FollowUpAction_Mst where nFollowUpActionID =" + ID + " and nFollowUpActionType=" + nType.GetHashCode();
                int result = oDB.Execute_Query(strQuery);
                if (result > 0)
                {
                    oParameters.Add("@ID", ID, System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Execute("gsp_DeleteFollowupFromInsCrosswalk", oParameters);
                    return true;
                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return false;

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
        }

        public bool DeleteFollowUpSchedule(Int64 nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from CL_FollowupSchedule_Claim where nTransactionID =" + nTransactionID;
                int result = oDB.Execute_Query(strQuery);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
        }

        public bool DeleteFollowUpSchedule_Multiple(string sAccount_TransactionIDs,CollectionEnums.FollowUpType emnFollowup)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            string sFollowup = "";
            object sMessage = null;
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nFollowUpType", (int)emnFollowup, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@sAccount_TransactionIDs", sAccount_TransactionIDs, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@Message", sMessage, ParameterDirection.Output, SqlDbType.VarChar, 1000);
                int result = oDB.Execute("CL_RemoveSchedule_Multiple", oDBParameters, out sMessage);

                switch (emnFollowup)
                {
                    case CollectionEnums.FollowUpType.Claim:
                        {
                            sFollowup = "Claim";
                        }
                        break;
                    case CollectionEnums.FollowUpType.PatientAccount:
                        {
                            sFollowup = "Patient Account";
                        }
                        break;
                    case CollectionEnums.FollowUpType.BadDebt:
                        {
                            sFollowup = "Bad Debt";
                        }
                        break;
                }
                if (Convert.ToString(sMessage) == "Success")
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.Delete, sFollowup+" Follow-up deleted." + sAccount_TransactionIDs, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    return true;
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.Delete, sFollowup + "Follow-up not deleted." + Convert.ToString(sMessage)+Environment.NewLine+"Transaction/Account ID: " +Environment.NewLine+ sAccount_TransactionIDs, 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
            }
        }

        public bool RemoveBadDebtStatus(Int64 nPateintID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nPatientID", nPateintID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtCreatedDateTime", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@flag", 2, ParameterDirection.Input, SqlDbType.Int);
                int result = oDB.Execute("PA_IN_Patient_BadDebt", oDBParameters);


                if (result>0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.Delete, "Patient's Bad Debt Status removed.", nPateintID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    return true;
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.Delete, "Patient's Bad Debt Status not removed", nPateintID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
            }
        }

        public decimal GetMultipleCalimBalance(string sTransactionIDs)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            decimal _dTotalBalance = 0;
            DataTable _dtTotalBalance=new DataTable();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@sTransactionIDs", sTransactionIDs, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("CL_RemoveMultipleSchedule_ClaimBalance", oDBParameters,out _dtTotalBalance);

                if (_dtTotalBalance != null && _dtTotalBalance.Rows.Count > 0)
                {
                    for (int i = 0; i < _dtTotalBalance.Rows.Count; i++)
                    {
                        _dTotalBalance = _dTotalBalance + Convert.ToDecimal(_dtTotalBalance.Rows[i]["ClaimBalance"]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                if (_dtTotalBalance != null) { _dtTotalBalance.Dispose(); _dtTotalBalance = null; }
            }
            return _dTotalBalance;
        }
        public static DataTable GetTransDetailsString(Int64 transactionID)
        {
            string sMessage = string.Empty;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dt = new DataTable();
            try
            {
                oDB.Connect(false);
                string _strSqlQuery = " SELECT "
                                       + "sFirstName + ' ' + sMiddleName + ' ' + sLastName AS PatientName, "
                                       + "ClaimToken.DOS, "
                                       + "ClaimToken.ClaimNumber "
                                       + "FROM dbo.Patient INNER JOIN dbo.BL_Transaction_Claim_MST "
                                       + "ON dbo.Patient.nPatientID = dbo.BL_Transaction_Claim_MST.nPatientID "
                                       + "OUTER APPLY "
                                       + "( "
                                       + "  SELECT DOS,ClaimNumber FROM dbo.GetClaimToken_AccountLog(" + transactionID + ") "
                                       + ") AS ClaimToken "
                                       + "WHERE BL_Transaction_Claim_MST.nTransactionID = " + transactionID;

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

        public bool SaveFollowUpLog(CollectionEnums.FollowUpType enmType, Int64 nAccountID, Int64 nPatientID, DateTime dtLogDate, string sCode, string sCodeDesc, Int64 nUserID, string sUserName, CollectionEnums.ScheduleType enmScheduleType, DateTime dtTimeStampOnLoad, ref bool _hasWorked)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool bReturn=false;
            object _message = null;
            try
            {
                oDB.Connect(false);

                oParameters.Add("@nFollowUpType", (int)enmType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nAccountID", nAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@dtLogDate", dtLogDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@sFollowupCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sFollowupDescription", sCodeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nScheduleType", (int)enmScheduleType, ParameterDirection.Input, SqlDbType.Int);
                if (dtTimeStampOnLoad != DateTime.MinValue)
                {
                    oParameters.Add("@dtTimeStampOnLoad", dtTimeStampOnLoad, ParameterDirection.Input, SqlDbType.DateTime);
                }
                else
                {
                    oParameters.Add("@dtTimeStampOnLoad", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                }
                oParameters.Add("@bHasWorked", _message, ParameterDirection.Output, SqlDbType.VarChar, 1000);
                int result = oDB.Execute("CL_INUP_FollowupLog", oParameters, out _message);

                if (result != 0)
                {
                    if (_message != null)
                    {
                        _hasWorked = Convert.ToBoolean((Convert.ToString(_message).Trim() == "" ? "False" : _message));
                    }
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }  
                //if (result != 0)
                //{
                //    if (_message != null)
                //    {
                //        if (Convert.ToBoolean(_message))
                //        {
                //            MessageBox.Show("Someone else has just worked this Account.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            bReturn = false;
                //        }
                //        else
                //        {
                //            bReturn = true;
                //        }
                //    }
                //    else
                //    {
                //        bReturn = true;
                //    }
                //}
                //else
                //{
                //    bReturn = false;
                //}               

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                bReturn = false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect();  oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            return bReturn;
        }


        public static void CreateFollowupSchedule(Int64 TransactionMasterId, Int64 TransactionId, Int64 ContactId)
        {
            GeneralSettings oSettings = null;


            DateTime dtCurrentDate = DateTime.Now;
            object oValue = null;
            CL_FollowUpCode oCollection = null;
            try
            {

                bool SettingsValue = IsFollowUpFeatureON();
                if (SettingsValue)
                {
                    dtCurrentDate = CL_FollowUpCode.GetServerDate();
                    oCollection = new CL_FollowUpCode();
                    oSettings = new GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                }

                #region "Region for Auto Schedule Setup"

                #region " Account Log Feature Enable Disable according to settings."

                if (SettingsValue)
                {

                    if (ContactId > 0 && TransactionMasterId > 0 && TransactionId > 0)
                    {
                        string sStatus = CL_FollowUpCode.GetClaimFollowUpStatus(TransactionMasterId, TransactionId, ContactId);
                        string sAction = string.Empty;
                        string sActionDesc = string.Empty;
                        Int32 nDays = 0;
                        bool bHasWorked = false;
                        switch (sStatus)
                        {
                            case "NewBatch":
                                if (sStatus == "NewBatch")
                                {
                                    oCollection.DeleteFollowUpSchedule(TransactionId);
                                }

                                oSettings.GetSetting("CL_INSCLM_START_DEFFUACTION", 0, gloGlobal.gloPMGlobal.ClinicID, out oValue);
                                sAction = Convert.ToString(oValue);
                                sActionDesc = CL_FollowUpCode.GetActionDesc(sAction, CollectionEnums.FollowUpType.Claim);

                                oSettings.GetSetting("CL_INSCLM_START_DEFFUACTIONDAYS", 0, gloGlobal.gloPMGlobal.ClinicID, out oValue);
                                Int32.TryParse(Convert.ToString(oValue), out nDays);

                                oCollection.SaveFollowUpScedule(CollectionEnums.FollowUpType.Claim, TransactionId, dtCurrentDate.AddDays(nDays), sAction, sActionDesc, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.System, DateTime.MinValue, ref bHasWorked);
                                break;
                            case "Rebill":
                            case "Resend":
                                oSettings.GetSetting("CL_INSCLM_REBILL_DEFFUACTION", 0, gloGlobal.gloPMGlobal.ClinicID, out oValue);
                                sAction = Convert.ToString(oValue);
                                sActionDesc = CL_FollowUpCode.GetActionDesc(sAction, CollectionEnums.FollowUpType.Claim);

                                oSettings.GetSetting("CL_INSCLM_REBILL_DEFFUACTIONDAYS", 0, gloGlobal.gloPMGlobal.ClinicID, out oValue);
                                Int32.TryParse(Convert.ToString(oValue), out nDays);

                                oCollection.SaveFollowUpScedule(CollectionEnums.FollowUpType.Claim, TransactionId, dtCurrentDate.AddDays(nDays), sAction, sActionDesc, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.System, DateTime.MinValue, ref bHasWorked);
                                break;
                        }
                    }
                }

                #endregion

                #endregion
            }
            catch //(Exception ex)
            {
                throw;
            }
            finally
            {
                if (oSettings != null)
                {
                    oSettings.Dispose();
                    oSettings = null;
                }
                if (oCollection != null)
                {
                    oCollection.Dispose();
                    oCollection = null;
                }
            }
        }







        public bool SaveFollowUpLog(CollectionEnums.FollowUpType enmType, Int64 nTransactionID, DateTime dtLogDate, string sCode, string sCodeDesc, Int64 nUserID, string sUserName, CollectionEnums.ScheduleType enmScheduleType, DateTime dtTimeStampOnLoad, ref bool _hasWorked)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            object _message = null;
            bool bReturn = false;
            try
            {
                oDB.Connect(false);

                oParameters.Add("@nFollowUpType", (int)enmType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nAccountID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionID", nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@dtLogDate", dtLogDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@sFollowupCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sFollowupDescription", sCodeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nScheduleType", (int)enmScheduleType, ParameterDirection.Input, SqlDbType.Int);
                if (dtTimeStampOnLoad != DateTime.MinValue)
                {
                    oParameters.Add("@dtTimeStampOnLoad", dtTimeStampOnLoad, ParameterDirection.Input, SqlDbType.DateTime);
                }
                else
                {
                    oParameters.Add("@dtTimeStampOnLoad", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                }
                oParameters.Add("@bHasWorked", _message, ParameterDirection.Output, SqlDbType.VarChar,1000);
                int result = oDB.Execute("CL_INUP_FollowupLog", oParameters, out _message);
                if (result != 0)
                {
                    if (_message != null)
                    {
                        _hasWorked = Convert.ToBoolean((Convert.ToString(_message).Trim() == "" ? "False" : _message));
                    }
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }       
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            return bReturn;
        }

        public bool SaveFollowUpScedule(CollectionEnums.FollowUpType enmType, Int64 nAccountID, Int64 nPatientID, DateTime dtLogDate, string sCode, string sCodeDesc, Int64 nUserID, string sUserName, CollectionEnums.ScheduleType enmScheduleType, DateTime dtTimeStampOnLoad, ref bool _hasWorked)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool bReturn = false;
            object _message = null;
            try
            {
                oDB.Connect(false);

                oParameters.Add("@nFollowUpType", (int)enmType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nAccountID", nAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@dtFollowUpDate", dtLogDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@nTransactionID", null, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sFollowupCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sFollowupDescription", sCodeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nScheduleType", (int)enmScheduleType, ParameterDirection.Input, SqlDbType.Int);
                if (dtTimeStampOnLoad != DateTime.MinValue)
                {
                    oParameters.Add("@dtTimeStampOnLoad", dtTimeStampOnLoad, ParameterDirection.Input, SqlDbType.DateTime);
                }
                else
                {
                    oParameters.Add("@dtTimeStampOnLoad", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                }
                oParameters.Add("@bHasWorked", _message, ParameterDirection.Output, SqlDbType.VarChar, 1000);
                int result = oDB.Execute("CL_INUP_FollowupSchedule", oParameters, out _message);
                if (result != 0)
                {
                    if (_message != null)
                    {
                        _hasWorked = Convert.ToBoolean((Convert.ToString(_message).Trim() == "" ? "False" : _message));
                    }
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }  
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                bReturn = false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            return bReturn;
        }

        public bool SaveFollowUpScedule(CollectionEnums.FollowUpType enmType, Int64 nTransactionID, DateTime dtLogDate, string sCode, string sCodeDesc, Int64 nUserID, string sUserName, CollectionEnums.ScheduleType enmScheduleType, DateTime dtTimeStampOnLoad, ref bool _hasWorked)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool bReturn = false;
            object _message = null;
            try
            {
                oDB.Connect(false);

                oParameters.Add("@nFollowUpType", (int)enmType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nAccountID", null, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", null, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@dtFollowUpDate", dtLogDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@nTransactionID", nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sFollowupCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sFollowupDescription", sCodeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nScheduleType", (int)enmScheduleType, ParameterDirection.Input, SqlDbType.Int);
                if (dtTimeStampOnLoad != DateTime.MinValue)
                {
                    oParameters.Add("@dtTimeStampOnLoad", dtTimeStampOnLoad, ParameterDirection.Input, SqlDbType.DateTime);
                }
                else
                {
                    oParameters.Add("@dtTimeStampOnLoad", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                }
                oParameters.Add("@bHasWorked", _message, ParameterDirection.Output, SqlDbType.VarChar, 1000);
                int result = oDB.Execute("CL_INUP_FollowupSchedule", oParameters,out _message);

                if (result != 0)
                {
                    if (_message != null)
                    {
                        _hasWorked = Convert.ToBoolean((Convert.ToString(_message).Trim() == "" ? "False" : _message));
                    }
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }  
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                bReturn = false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            return bReturn;
        }

        public bool SaveBatchAccountFollowUpLog(CollectionEnums.FollowUpType enmType, DateTime dtLogDate, string sCode, string sCodeDesc, Int64 nUserID, string sUserName, CollectionEnums.ScheduleType enmScheduleType, DataTable dtBatchAccountIds)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool bReturn = false;
            try
            {
                oDB.Connect(false);

                oParameters.Add("@nFollowUpType", (int)enmType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@dtLogDate", dtLogDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@sFollowupCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sFollowupDescription", sCodeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nScheduleType", (int)enmScheduleType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@tvpBatchTemplateLogSchedule", dtBatchAccountIds, ParameterDirection.Input, SqlDbType.Structured);
                int result = oDB.Execute("CL_INUP_BatchTemplateAcctFollowUpLog", oParameters);

                if (result != 0)
                {
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                bReturn = false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            return bReturn;
        }

        public bool SaveBatchAccountFollowUpScedule(CollectionEnums.FollowUpType enmType, DateTime dtLogDate, string sCode, string sCodeDesc, Int64 nUserID, string sUserName, CollectionEnums.ScheduleType enmScheduleType, DataTable dtBatchAccountIds)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool bReturn = false;
            try
            {
                oDB.Connect(false);

                oParameters.Add("@nFollowUpType", (int)enmType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@dtFollowUpDate", dtLogDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@sFollowupCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sFollowupDescription", sCodeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nScheduleType", (int)enmScheduleType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@tvpBatchTemplateLogSchedule", dtBatchAccountIds, ParameterDirection.Input, SqlDbType.Structured);
                int result = oDB.Execute("CL_INUP_BatchTemplateAcctFollowUpSchedule", oParameters);

                if (result != 0)
                {
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                bReturn = false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }

            return bReturn;
        }

        private void BulkActionLog(DataTable dtResult, CollectionEnums.FollowUpType enmType,gloAuditTrail.ActivityOutCome Status, String sMessage1,String sMessage2,Int64 nAuditLogID)
        {
            string TranAccountIDS = "";
                   
            if (enmType == CollectionEnums.FollowUpType.Claim)
            {
                try
                {

                    if (dtResult != null)
                    {

                        TranAccountIDS = dtResult.AsEnumerable()
                            //.Where(r => Convert.ToString(r["HasWorked"]) == Convert.ToString("No"))
                            // .Where(r => r.Field<string>("HasWorked") == "NO")
                                                     .Select(row => row["nTransactionID"].ToString())
                                                     .Aggregate((s1, s2) => String.Concat(s1, "|" + s2));
                        if (TranAccountIDS != "")
                        {
                           // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.Action, sMessage1, 0, 0, 0, Status, gloAuditTrail.SoftwareComponent.gloPM, true);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.Action, sMessage1 + " : " + sMessage2 + " Claim(s) - " + TranAccountIDS, 0,nAuditLogID, 0, Status, gloAuditTrail.SoftwareComponent.gloPM, true);

                        }
                    }
                }
                catch(Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.Action, "AuditTrail failed for bulk Action", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                }

            }
            else
            {
                try
                {
                    if (dtResult != null)
                    {
                        TranAccountIDS = dtResult.AsEnumerable()
                            //  .Where(r => Convert.ToString(r["HasWorked"]) == "No")
                            // .Where(r => r.Field<string>("HasWorked") == "NO")
                                                      .Select(row => row["nAccountID"].ToString())
                                                      .Aggregate((s1, s2) => String.Concat(s1, "|" + s2));
                        if (TranAccountIDS != "")
                        {
                           // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.AccountFollowUpWQ, gloAuditTrail.ActivityType.Action, sMessage1, 0, 0, 0, Status, gloAuditTrail.SoftwareComponent.gloPM, true);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.AccountFollowUpWQ, gloAuditTrail.ActivityType.Action, sMessage1 + " : " + sMessage2 + " Account(s) - " + TranAccountIDS, 0, nAuditLogID, 0, Status, gloAuditTrail.SoftwareComponent.gloPM, true);

                        }
                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.Action, "AuditTrail failed for bulk Action", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                }

            }
        }

        private void BulkActionLog(DataTable dtResult, CollectionEnums.FollowUpType enmType, gloAuditTrail.ActivityOutCome Status, String sMessage1, String sMessage2, Int64 nAuditLogID, gloAuditTrail.ActivityType Action)
        {
            string TranAccountIDS = "";

            if (enmType == CollectionEnums.FollowUpType.Claim)
            {
                try
                {

                    if (dtResult != null)
                    {

                        TranAccountIDS = dtResult.AsEnumerable()
                            //.Where(r => Convert.ToString(r["HasWorked"]) == Convert.ToString("No"))
                            // .Where(r => r.Field<string>("HasWorked") == "NO")
                                                     .Select(row => row["nTransactionID"].ToString())
                                                     .Aggregate((s1, s2) => String.Concat(s1, "|" + s2));
                        if (TranAccountIDS != "")
                        {
                            // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.Action, sMessage1, 0, 0, 0, Status, gloAuditTrail.SoftwareComponent.gloPM, true);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, Action, sMessage1 + " : " + sMessage2 + " Claim(s) - " + TranAccountIDS, 0, nAuditLogID, 0, Status, gloAuditTrail.SoftwareComponent.gloPM, true);

                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, Action, "AuditTrail failed for bulk Action", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                }

            }
            
        }
        public bool SaveFollowUpLog_Multiple(CollectionEnums.FollowUpType enmType, DateTime dtLogDate, string sCode, string sCodeDesc, Int64 nUserID, string sUserName, CollectionEnums.ScheduleType enmScheduleType, DateTime dtFolloUpDate, DataTable dtBatchAccountIds, out DataTable dtLog, Int64 nAuditLogID, string sActionFor = "Schedule Action")
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool bReturn = false;
            dtLog = null;
            DataTable dtResult = null;
            string sMessage = "";
            if (dtBatchAccountIds != null && dtBatchAccountIds.Rows.Count > 0)
            {
                BulkActionLog(dtBatchAccountIds, enmType, gloAuditTrail.ActivityOutCome.Success, sActionFor + " Bulk Log Action Started ", "List of Selected ", nAuditLogID);
            }
            try
            {
                oDB.Connect(false);

                oParameters.Add("@nFollowUpType", (int)enmType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@dtLogDate", dtLogDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@sFollowupCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sFollowupDescription", sCodeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nScheduleType", (int)enmScheduleType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@dtFollowUpDate", dtFolloUpDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@sErrMessage", sMessage, ParameterDirection.InputOutput, SqlDbType.VarChar);
                oParameters.Add("@tvpLogSchedule_Multiple", dtBatchAccountIds, ParameterDirection.Input, SqlDbType.Structured);
               // oDB.Execute("CL_INUP_FollowUpLog_Multiple", oParameters);
               // oDB.Execute("CL_INUP_FollowUpLog_Multiple", oParameters, out abc, out dt);
                oDB.Retrive("CL_INUP_FollowUpLog_Multiple", oParameters, out dtResult, out sMessage);

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    dtLog = dtResult;  
                }
                if (sMessage == "S")
                {
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        DataRow[] dr = dtResult.Select("HasWorked='Yes'");
                        if (dr != null && dr.Length > 0)
                        {
                            BulkActionLog(dr.CopyToDataTable<DataRow>(), enmType, gloAuditTrail.ActivityOutCome.Success, sActionFor + " Bulk Log Action", "List of Skipped", nAuditLogID);
                        }
                        DataRow[] dr1 = dtResult.Select("HasWorked='No'");
                        if (dr1 != null && dr1.Length > 0)
                        {
                            BulkActionLog(dr1.CopyToDataTable<DataRow>(), enmType, gloAuditTrail.ActivityOutCome.Success, sActionFor + " Bulk Log Action", "List of Successed", nAuditLogID);
                        }
                       
                       
                    }
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }

            }
            catch (Exception ex)
            {
                BulkActionLog(dtBatchAccountIds, enmType, gloAuditTrail.ActivityOutCome.Failure, sActionFor + " Bulk Log Action", "List of ", nAuditLogID);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                bReturn = false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (enmType == CollectionEnums.FollowUpType.Claim)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.Action, sActionFor + " Bulk Log Action Completed", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.AccountFollowUpWQ, gloAuditTrail.ActivityType.Action, sActionFor + " Bulk Log Action Completed", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                }
               
            }

            return bReturn;
        }

        public bool SaveFollowUpScedule_Multiple(CollectionEnums.FollowUpType enmType, DateTime dtLogDate, string sCode, string sCodeDesc, Int64 nUserID, string sUserName, CollectionEnums.ScheduleType enmScheduleType, DataTable dtBatchAccountIds, out DataTable dtSchedule, Int64 nAuditLogID, string sActionFor = "Schedule Action")
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool bReturn = false;
            dtSchedule = null;
            DataTable dtResult = null;
            string sMessage = "";
            if (dtBatchAccountIds != null && dtBatchAccountIds.Rows.Count > 0)
            {
                BulkActionLog(dtBatchAccountIds, enmType, gloAuditTrail.ActivityOutCome.Success, sActionFor + " Bulk Schedule Action Started ", "List of Selected ", nAuditLogID);
            }
            try
            {
                oDB.Connect(false);

                oParameters.Add("@nFollowUpType", (int)enmType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@dtFollowUpDate", dtLogDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@sFollowupCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sFollowupDescription", sCodeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nScheduleType", (int)enmScheduleType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@sErrMessage", sMessage, ParameterDirection.InputOutput, SqlDbType.VarChar);
                oParameters.Add("@tvpLogSchedule_Multiple", dtBatchAccountIds, ParameterDirection.Input, SqlDbType.Structured);
               // int result = oDB.Execute("CL_INUP_FollowUpSchedule_Multiple", oParameters);
                oDB.Retrive("CL_INUP_FollowUpSchedule_Multiple", oParameters, out dtResult, out sMessage);

                if (dtResult!=null && dtResult.Rows.Count>0)
                {
                    dtSchedule = dtResult;
                }
                if (sMessage == "S")
                {
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        DataRow[] dr = dtResult.Select("HasWorked='Yes'");
                        if (dr != null && dr.Length > 0)
                        {
                            BulkActionLog(dr.CopyToDataTable<DataRow>(), enmType, gloAuditTrail.ActivityOutCome.Success, sActionFor + " Bulk Schedule Action", "List of Skipped", nAuditLogID);
                        }
                        DataRow[] dr1 = dtResult.Select("HasWorked='No'");
                        if (dr1 != null && dr1.Length > 0)
                        {
                            BulkActionLog(dr1.CopyToDataTable<DataRow>(), enmType, gloAuditTrail.ActivityOutCome.Success, sActionFor + " Bulk Schedule Action", "List of Successed", nAuditLogID);
                        }
                    }
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }

            }
            catch (Exception ex)
            {
                BulkActionLog(dtBatchAccountIds, enmType, gloAuditTrail.ActivityOutCome.Failure, sActionFor + " Bulk Schedule Action", "List of ", nAuditLogID);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                bReturn = false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (enmType == CollectionEnums.FollowUpType.Claim)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.Action, sActionFor + " Bulk Schedule Action Completed", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.AccountFollowUpWQ, gloAuditTrail.ActivityType.Action, sActionFor + " Bulk Schedule Action Completed", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                }
               
            }

            return bReturn;
        }
      
        public Int64  GetUniqueID()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dtLineIds = null;
            Int64 nUniqueID = 0;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@IDCount", 1, ParameterDirection.Input, SqlDbType.Int);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetUniqueIDs", oParameters, out _dtLineIds);
                oDB.Disconnect();

                if (_dtLineIds != null && _dtLineIds.Rows.Count > 0)
                {
                    nUniqueID = Convert.ToInt64(_dtLineIds.Rows[0][1]);
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
            return nUniqueID;
            
        }
        public static DataTable GetScheduledActionDetails(Int64 nScheduleID, CollectionEnums.FollowUpType enmFollowUpType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtFollowUpCode = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                if (enmFollowUpType == CollectionEnums.FollowUpType.PatientAccount)
                {
                    strQuery = "SELECT schedule.sFollowupCode,CONVERT(DATE,schedule.dtAcctFollowUpDate) dtFollowUpDate, mst.sDefNextActionFollowUpCode,mst.nFollowUpActionDays "
                                + "FROM dbo.CL_FollowupSchedule_Acct schedule "
                                + "LEFT OUTER JOIN dbo.CL_FollowUpAction_Mst mst "
                                + "ON mst.sFollowUpActionCode = schedule.sFollowupCode "
                                + "WHERE schedule.nID = " + nScheduleID + " and  ISNULL(mst.nFollowUpActionType,0) IN (0," + CollectionEnums.FollowUpType.PatientAccount.GetHashCode() + ")";
                }
                else if (enmFollowUpType == CollectionEnums.FollowUpType.Claim)
                {
                    strQuery = "SELECT schedule.sFollowupCode,CONVERT(DATE,schedule.dtClaimFollowUpDate) dtFollowUpDate, mst.sDefNextActionFollowUpCode,mst.nFollowUpActionDays "
                                + "FROM dbo.CL_FollowupSchedule_Claim schedule "
                                + "LEFT OUTER JOIN dbo.CL_FollowUpAction_Mst mst "
                                + "ON mst.sFollowUpActionCode = schedule.sFollowupCode "
                                + "WHERE schedule.nID = " + nScheduleID + " and  ISNULL(mst.nFollowUpActionType,0) IN (0," + CollectionEnums.FollowUpType.Claim.GetHashCode() + ")";
                }
                else if (enmFollowUpType == CollectionEnums.FollowUpType.BadDebt)
                {
                    strQuery = "SELECT schedule.sFollowupCode,CONVERT(DATE,schedule.dtAcctFollowUpDate) dtFollowUpDate, mst.sDefNextActionFollowUpCode,mst.nFollowUpActionDays "
                                + "FROM dbo.CL_FollowupSchedule_BadDebt schedule "
                                + "LEFT OUTER JOIN dbo.CL_FollowUpAction_Mst mst "
                                + "ON mst.sFollowUpActionCode = schedule.sFollowupCode "
                                + "WHERE schedule.nID = " + nScheduleID + " and  ISNULL(mst.nFollowUpActionType,0) IN (0," + CollectionEnums.FollowUpType.BadDebt.GetHashCode() + ")";
                }
               
                oDB.Retrive_Query(strQuery, out dtFollowUpCode);
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
            return dtFollowUpCode;
        }

        public static DataTable GetDefaultAssociateTemplate(string sFollowUpCode, CollectionEnums.FollowUpType enmFollowUpType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtDefTemplate = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = "SELECT Isnull(CL_FollowUpAction_Mst.nTemplateID,0) AS nTemplateID,Isnull(TemplateGallery_MST.sTemplateName,'') AS stemplateNAME  from CL_FollowUpAction_Mst INNER JOIN dbo.TemplateGallery_MST ON CL_FollowUpAction_Mst.nTemplateID=TemplateGallery_MST.nTemplateID "
                            + "WHERE CL_FollowUpAction_Mst.sfollowupactioncode = '" + sFollowUpCode + "' and  CL_FollowUpAction_Mst.nFollowUpActionType=" + enmFollowUpType.GetHashCode();

                oDB.Retrive_Query(strQuery, out dtDefTemplate);
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
            return dtDefTemplate;
        }

        public static DataTable GetScheduledAction(Int64 nID, CollectionEnums.FollowUpType enmFollowUpType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtFollowUpCode = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                if (enmFollowUpType == CollectionEnums.FollowUpType.PatientAccount)
                {
                    strQuery = "SELECT schedule.sFollowupCode,CONVERT(DATE,schedule.dtAcctFollowUpDate) dtFollowUpDate, mst.sDefNextActionFollowUpCode,mst.nFollowUpActionDays "
                                + "FROM dbo.CL_FollowupSchedule_Acct schedule "
                                + "LEFT OUTER JOIN dbo.CL_FollowUpAction_Mst mst "
                                + "ON mst.sFollowUpActionCode = schedule.sFollowupCode "
                                + "WHERE schedule.nAccountID = " + nID + " and  mst.nFollowUpActionType=" + CollectionEnums.FollowUpType.PatientAccount.GetHashCode();
                }
                else if (enmFollowUpType == CollectionEnums.FollowUpType.Claim)
                {
                    strQuery = "SELECT schedule.sFollowupCode,CONVERT(DATE,schedule.dtClaimFollowUpDate) dtFollowUpDate, mst.sDefNextActionFollowUpCode,mst.nFollowUpActionDays "
                                + "FROM dbo.CL_FollowupSchedule_Claim schedule "
                                + "LEFT OUTER JOIN dbo.CL_FollowUpAction_Mst mst "
                                + "ON mst.sFollowUpActionCode = schedule.sFollowupCode "
                                + "WHERE schedule.nTransactionID = " + nID + " and  mst.nFollowUpActionType=" + CollectionEnums.FollowUpType.Claim.GetHashCode();
                }
                else if (enmFollowUpType == CollectionEnums.FollowUpType.BadDebt)
                {
                    strQuery = "SELECT schedule.sFollowupCode,CONVERT(DATE,schedule.dtAcctFollowUpDate) dtFollowUpDate, mst.sDefNextActionFollowUpCode,mst.nFollowUpActionDays "
                                + "FROM dbo.CL_FollowupSchedule_BadDebt schedule "
                                + "LEFT OUTER JOIN dbo.CL_FollowUpAction_Mst mst "
                                + "ON mst.sFollowUpActionCode = schedule.sFollowupCode "
                                + "WHERE schedule.nAccountID = " + nID + " and  mst.nFollowUpActionType=" + CollectionEnums.FollowUpType.BadDebt.GetHashCode();
                }

                oDB.Retrive_Query(strQuery, out dtFollowUpCode);
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
            return dtFollowUpCode;
        }

        public static bool ClearInsuranceClaimFollowUp(Int64 nNewTransactionID, Int64 nOldTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool bReturn = false;
            try
            {
                oDB.Connect(false);

                oParameters.Add("@NewTransactionID", nNewTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@OldTransactionID", nOldTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@CarryForward", false, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@InsurancePayment", false, ParameterDirection.Input, SqlDbType.Bit);

                int result = oDB.Execute("CL_PullCollectionsSchedule", oParameters);

                if (result != 0)
                {
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                bReturn = false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return bReturn;
        }

        public static bool ClearInsuranceClaimFollowUp(Int64 nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool bReturn = false;
            try
            {
                oDB.Connect(false);

                oParameters.Add("@NewTransactionID", nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@OldTransactionID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@CarryForward", false, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@InsurancePayment", false, ParameterDirection.Input, SqlDbType.Bit);

                int result = oDB.Execute("CL_PullCollectionsSchedule", oParameters);

                if (result != 0)
                {
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                bReturn = false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return bReturn;
        }

        public static bool CarryForwardClaimFollowupToChild(Int64 nNewTransactionID, Int64 nOldTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool bReturn = false;
            try
            {
                oDB.Connect(false);

                oParameters.Add("@NewTransactionID", nNewTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@OldTransactionID", nOldTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@CarryForward", true, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@InsurancePayment", false, ParameterDirection.Input, SqlDbType.Bit);

                int result = oDB.Execute("CL_PullCollectionsSchedule", oParameters);

                if (result != 0)
                {
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                bReturn = false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return bReturn;
        }

        public static Boolean IsScheduleDateToFarinFuture(string sScheduleDate)
        {
            Boolean _isFuture=false;
            DateTime _dtScheduleDate = Convert.ToDateTime(sScheduleDate);
            try
            {
                DateTime _dtMaxDate = DateTime.Now.AddDays(90);
                if (_dtScheduleDate > _dtMaxDate)
                {
                    _isFuture = true;
                }
            }
            catch (Exception ex)
            {
                _isFuture = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                
            }

            return _isFuture;
        }

        //public static DataTable GetScheduledAction(Int64 nID, CollectionEnums.FollowUpType enmFollowUpType)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //    DataTable dtFollowUpCode = null;
        //    string strQuery = "";
        //    try
        //    {
        //        oDB.Connect(false);
        //        if (enmFollowUpType == CollectionEnums.FollowUpType.PatientAccount)
        //        {
        //            strQuery = "SELECT schedule.sFollowupCode,CONVERT(DATE,schedule.dtAcctFollowUpDate) dtFollowUpDate, mst.sDefNextActionFollowUpCode,mst.nFollowUpActionDays "
        //                        + "FROM dbo.CL_FollowupSchedule_Acct schedule "
        //                        + "LEFT OUTER JOIN dbo.CL_FollowUpAction_Mst mst "
        //                        + "ON mst.sFollowUpActionCode = schedule.sFollowupCode "
        //                        + "WHERE schedule.nAccountID = " + nID;
        //        }
        //        else if (enmFollowUpType == CollectionEnums.FollowUpType.Claim)
        //        {
        //            strQuery = "SELECT schedule.sFollowupCode,CONVERT(DATE,schedule.dtClaimFollowUpDate) dtFollowUpDate, mst.sDefNextActionFollowUpCode,mst.nFollowUpActionDays "
        //                        + "FROM dbo.CL_FollowupSchedule_Claim schedule "
        //                        + "LEFT OUTER JOIN dbo.CL_FollowUpAction_Mst mst "
        //                        + "ON mst.sFollowUpActionCode = schedule.sFollowupCode "
        //                        + "WHERE schedule.nTransactionID = " + nID;
        //        }

        //        oDB.Retrive_Query(strQuery, out dtFollowUpCode);
        //        oDB.Disconnect();
        //    }

        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //    }
        //    return dtFollowUpCode;
        //}

        #region "Account Follow-Up"

        public static bool DeleteAccountFollowUp(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "DELETE FROM CL_FollowupSchedule_Acct WHERE nAccountID =" + PAccountID;
                int result = oDB.Execute_Query(strQuery);
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return false;
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
        }

        public static bool CheckBadDebtAccountFollowUp(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT TOP 1 dbo.CL_FollowupSchedule_BadDebt.nAccountID FROM  CL_FollowupSchedule_BadDebt WHERE dbo.CL_FollowupSchedule_BadDebt.nAccountID=" + PAccountID;
                object result = oDB.ExecuteScalar_Query(strQuery);
                if (result == null || Convert.ToString(result) != string.Empty)
                {
                    return true;
                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return false;
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
        }

        public static void SetAutoAccountFollowUp(Int64 PAccountID, Int64 PatientID, DateTime dtCloseDate)
        {
            DataSet dsAccountBalances = null;
            DataTable dtInsuranceDetails = null;
            DataTable dtReserveDetails = null;
            decimal dTotalBalAmt = 0;
            decimal dTotalInsPending = 0;
            decimal dTotalPatientDue = 0;
            decimal dTotalcopayReserve = 0;
            decimal dTotalAdvancedReserve = 0;
            decimal dTotalOtherReserve = 0;
            CL_FollowUpCode oCLsBL_FollowUpCode = new CL_FollowUpCode();

            string sFollowUpActionCode = string.Empty;
            string sFollowUpActionDescription = string.Empty;

            dsAccountBalances = gloAccountsV2.gloPatientPaymentV2.GetAccountBalances(PAccountID);
            if (dsAccountBalances.Tables.Count > 0)
            {
                dtInsuranceDetails = dsAccountBalances.Tables[0];
                dtReserveDetails = dsAccountBalances.Tables[1];
            }
            if (dtInsuranceDetails != null && dtInsuranceDetails.Rows.Count > 0)
            {

                dTotalInsPending = dtInsuranceDetails.Rows[0]["InsuranceDue"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["InsuranceDue"]);
                dTotalPatientDue = dtInsuranceDetails.Rows[0]["PatientDue"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["PatientDue"]);
                dTotalBalAmt = dTotalInsPending + dTotalPatientDue;
            }
            if (dtReserveDetails != null && dtReserveDetails.Rows.Count > 0)
            {
                foreach (DataRow drReserveDetails in dtReserveDetails.Rows)
                {
                    if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 2)   //For Copay Reserve
                    {
                        dTotalcopayReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                    }

                    if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 3)  //ForAdvanced Reserve
                    {
                        dTotalAdvancedReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                    }
                    if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 4) //For OtherReserve
                    {
                        dTotalOtherReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                    }
                }
            }
            dTotalPatientDue = dTotalPatientDue - (dTotalcopayReserve + dTotalAdvancedReserve + dTotalOtherReserve);

            if (dTotalPatientDue <= 0)
            {
                CL_FollowUpCode.DeleteAccountFollowUp(PAccountID);
            }
            else { SetupPartialPaymentFollowUp(PAccountID, PatientID, dtCloseDate); }

            gloStatment oStatement = new gloStatment();
            oStatement.ResetStatementCount(PAccountID, dtCloseDate, false);
            //Bug #68473: CR00000352 : RCM Queue issue
            if (oStatement.GetStatementCount(PAccountID) == 0 && !(oStatement.GetIsPaymentPlan(PAccountID)))
            {
                CL_FollowUpCode.DeleteAccountFollowUp(PAccountID);
            }

        }

        public static void SetClaimFollowUp(Int64 CreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            
            try
            {
                if (CreditID != 0)
                {
                    oParameters.Clear();
                    oParameters.Add("@nCreditID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Connect(false);
                    oDB.Execute("ERA_ClaimFollowUP", oParameters);
                    oDB.Disconnect();
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Claim Follow-up generation failed.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
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

        }

        public static void SetAutoAccountFollowUp(Int64 CreditID, Int64 UserID, string UserName, DateTime dtLogDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            object _retVal = null;
            try
            {
                if (CreditID != 0)
                {
                    oParameters.Clear();
                    oParameters.Add("@nCreditID", CreditID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sUserName", UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@dtCloseDate", dtLogDate, ParameterDirection.Input, SqlDbType.DateTime);
                    oDB.Connect(false);
                    oDB.Execute("CL_SetAccountFollowUp", oParameters, out _retVal);
                    oDB.Disconnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Account Follow-up generation failed.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
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
        }

        public static void SetupPartialPaymentFollowUp(Int64 PAccountID, Int64 PatientID, DateTime dtCloseDate)
        {
            DataSet dsPaymentPlan = null;
            CL_FollowUpCode oCollection = null;
            DateTime dt_Follow = new DateTime();
            Int64 _NoofDays = 0;
            decimal dPlanAmount = 0;
            decimal dLastPaidAmount = 0;
            string _sDefaultCode = string.Empty;
            string _sDefaultDesc = string.Empty;
            bool IsPaymentPlan = false;
            dt_Follow = DateTime.Now;
            bool bHasWorked = false;
            try
            {
                dsPaymentPlan = CL_PaymentPlan.GetPaymentPlan(PAccountID);
                if (dsPaymentPlan != null)
                {
                    if (dsPaymentPlan.Tables[0] != null)
                    {
                        if (dsPaymentPlan.Tables[0].Rows.Count > 0)
                        {
                            if (dsPaymentPlan.Tables[0].Rows[0]["dPlanAmount"] != DBNull.Value)
                            {
                                dPlanAmount = Convert.ToDecimal(dsPaymentPlan.Tables[0].Rows[0]["dPlanAmount"]);
                                IsPaymentPlan = true;
                            }
                        }
                    }

                    if (dsPaymentPlan.Tables[1] != null)
                    {
                        if (dsPaymentPlan.Tables[1].Rows.Count > 0)
                        {
                            if (dsPaymentPlan.Tables[1].Rows[0]["sPaymentPlanDays"] != DBNull.Value)
                            {
                                _NoofDays = Convert.ToInt64(dsPaymentPlan.Tables[1].Rows[0]["sPaymentPlanDays"]);
                            }
                        }
                    }

                    if (dsPaymentPlan.Tables[3] != null)
                    {
                        if (dsPaymentPlan.Tables[3].Rows.Count > 0)
                        {
                            if (dsPaymentPlan.Tables[3].Rows[0]["sPaymentPlanAction"] != DBNull.Value)
                            {
                                _sDefaultCode = Convert.ToString(dsPaymentPlan.Tables[3].Rows[0]["sPaymentPlanCode"]);
                                _sDefaultDesc = Convert.ToString(dsPaymentPlan.Tables[3].Rows[0]["sPaymentPlanAction"]);
                            }
                        }
                    }
                    dt_Follow = dtCloseDate.AddDays(_NoofDays);
                    dLastPaidAmount = gloAccountsV2.gloBillingCommonV2.GetLastAccountPaidAmount(PAccountID);
                    if (IsPaymentPlan && dLastPaidAmount >= dPlanAmount)
                    {
                        oCollection = new CL_FollowUpCode();
                        CL_FollowUpCode.DeleteAccountFollowUp(PAccountID);
                        oCollection.SaveFollowUpScedule(CollectionEnums.FollowUpType.PatientAccount, PAccountID, PatientID, dt_Follow, _sDefaultCode, _sDefaultDesc, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.System, DateTime.MinValue, ref bHasWorked);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dsPaymentPlan != null)
                { dsPaymentPlan.Dispose(); }
                if (oCollection != null) { oCollection.Dispose(); }
            }

        }

        #endregion "Account Follow-Up"

        // Remove $0.00 bug from insurance claim followup when patient did full patient payment for claim
        //***********************
        public static void ClearZeroBalanceClaimfromInsuranceFollowup(Int64 nTrackTransactionID, Int64 nTransactionMSTID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            string _SQLRetriveClaimBalance = string.Empty;
            string _SQLDeleteClaimfromFollowup = string.Empty;

            try
            {
                oDB.Connect(false);
                _SQLRetriveClaimBalance = "SELECT ClaimBalance = [dbo].[GetSplitClaimBalance]('" + nTrackTransactionID + "','" + nTransactionMSTID + "' )";
                if (Convert.ToDecimal(oDB.ExecuteScalar_Query(_SQLRetriveClaimBalance)) == 0)
                {
                    try
                    {
                        _SQLDeleteClaimfromFollowup = "DELETE FROM CL_FollowupSchedule_Claim WHERE nTransactionMasterID ='" + nTransactionMSTID + "' ";
                        oDB.Execute_Query(_SQLDeleteClaimfromFollowup);
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }
        //************************

        public static DataSet GetClaimFollowUp(Int64 nMasterTransactionID, Int64 nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet dtSet = new DataSet();
            try
            {
                oParameters.Add("@nMasterTransactionID", nMasterTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionID", nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("CL_GetClaimFollowUp", oParameters, out dtSet);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtSet;
        }


        public static String FormatPhoneNo(string sPhoneNo)
        {
            string sFormattedPhone = "";
            try
            {


                if (sPhoneNo != string.Empty)
                {
                    Int64 length = 0;

                    length = sPhoneNo.Length;
                    if (length == 11)
                    {
                        if (sPhoneNo.StartsWith("1"))
                        {
                            sFormattedPhone = "1" + " (" + sPhoneNo.Substring(1, 3) + ") " + sPhoneNo.Substring(4, 3) + "-" + sPhoneNo.Substring(7, 4);
                        }

                    }
                    else if (length == 10)
                    {

                        sFormattedPhone = "(" + sPhoneNo.Substring(0,3) + ") " + sPhoneNo.Substring(3, 3) + "-" + sPhoneNo.Substring(6, 4);

                    }
                    else if (length == 7)
                    {

                        sFormattedPhone = sPhoneNo.Substring(0, 3) + "-" + sPhoneNo.Substring(3, 4);

                    }
                    else
                    {
                        sFormattedPhone = sPhoneNo;
                    }
                }

            }
            catch (Exception)
            {
                sFormattedPhone = "";

            }
            return sFormattedPhone;
        }

        public bool TransferClaimBalanceToExternalCollectionAgency(String nAccountIDs, DateTime dtCloseDate,long nCollectionAgencyContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string _Error = "";
            Object outError = null;
            bool bReturn = false;
            try
            {
                oDB.Connect(false);

                oParameters.Add("@nPAccountID", nAccountIDs, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@dtCloseDate", dtCloseDate, ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@Message", _Error, ParameterDirection.Output, SqlDbType.VarChar,50000);
                oParameters.Add("@PrintFlag", false, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID , ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nCollectionAgencyContactID", nCollectionAgencyContactID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("BL_SplitTransactionClaim_ExtrCollation", oParameters, out outError);

                if (outError != null && Convert.ToString(outError).Trim() != "")  // Display comments to the user.
                { 
                    _Error = Convert.ToString(outError).Trim();
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error while Transfering Claim Balance To External Collection Agency. " + _Error, false);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.CollectionAgency, gloAuditTrail.ActivityType.TransferAccountBalance, "Error while Transfering Claim Balance To External Collection Agency.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.CollectionAgency, gloAuditTrail.ActivityType.TransferAccountBalance, "List of Account - " + nAccountIDs, 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                    bReturn = false;
                }         

                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.CollectionAgency, gloAuditTrail.ActivityType.TransferAccountBalance, "Transfer Account Balance To External Collection Agency.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.CollectionAgency, gloAuditTrail.ActivityType.TransferAccountBalance, "List of Account - " + nAccountIDs, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    bReturn = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error while Transfering Claim Balance To External Collection Agency. " + ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.CollectionAgency, gloAuditTrail.ActivityType.TransferAccountBalance, "Error while Transfering Claim Balance To External Collection Agency.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.CollectionAgency, gloAuditTrail.ActivityType.TransferAccountBalance, "List of Account - " + nAccountIDs, 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                bReturn = false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }
            return bReturn;
        }

        public static DataTable getClaimTFL_DFL_Details(Int64 nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtTFL_DFL_Details  = null;
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nTransactionID", @nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_TFL_DFL", oParameters, out dtTFL_DFL_Details);
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
            return dtTFL_DFL_Details;
        }

        public bool TransferClaimBalanceToSelf(string nBillingTransactionID, string TrackTransactionID, DateTime dtCloseDate, out DataTable dtLog)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string _Error = "";
            dtLog = null;
            string outError = null;
            bool bReturn = false;
            DataTable dtResult = null;
            Int64 nAuditLogID = GetUniqueID();
            if (TrackTransactionID != "")
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.TransferClaimResponsibilityToSelf, "Bulk Transfer to Self Action Started" + " : " + "List of Selected " + " Claim(s) - " + TrackTransactionID, 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            }
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nBillingTransactionID", nBillingTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@TrackTransactionID", TrackTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@dtCloseDate", dtCloseDate, ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@sErrMessage", _Error, ParameterDirection.InputOutput, SqlDbType.VarChar, 50000);
                oParameters.Add("@PrintFlag", false, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sMachineName", Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("BL_SplitTransactionClaim_TransferToSelf", oParameters, out dtResult, out outError);

                oDB.Disconnect();
                if (dtResult != null && dtResult.Rows.Count > 0)
                {


                    if (outError == "Success")
                    {
                        if (dtResult != null && dtResult.Rows.Count > 0)
                        {
                            DataRow[] dr = dtResult.Select("Description <>'Success'");
                            if (dr != null && dr.Length > 0)
                            {
                                BulkActionLog(dr.CopyToDataTable<DataRow>(), CollectionEnums.FollowUpType.Claim, gloAuditTrail.ActivityOutCome.Success, "Bulk Transfer to Self Action", "List of Skipped", nAuditLogID, gloAuditTrail.ActivityType.TransferClaimResponsibilityToSelf);
                            }
                            DataRow[] dr1 = dtResult.Select("Description='Success'");
                            if (dr1 != null && dr1.Length > 0)
                            {
                                BulkActionLog(dr1.CopyToDataTable<DataRow>(), CollectionEnums.FollowUpType.Claim, gloAuditTrail.ActivityOutCome.Success, "Bulk Transfer to Self Action", "List of Successed", nAuditLogID, gloAuditTrail.ActivityType.TransferClaimResponsibilityToSelf);
                            }
                        }
                       
                        dtResult.Columns.Remove("nTransactionID");
                        dtResult.AcceptChanges();
                        dtLog = dtResult;
                        bReturn = true;
                    }
                }
                else
                {
                    bReturn = false;
                }
                //if (dtResult != null && dtResult.Rows.Count > 0)
                //{
                //    dtLog = dtResult;
                //}
                //if (outError != null && Convert.ToString(outError).Trim() != "")  // Display comments to the user.
                //{
                //    _Error = Convert.ToString(outError).Trim();
                //    gloAuditTrail.gloAuditTrail.ExceptionLog("Error while Transferring Claim Responsibility to Self. " + _Error, false);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.TransferClaim, gloAuditTrail.ActivityType.TransferClaimResponsibilityToSelf, "Failure in Transferring Claim Responsibility to Self.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.TransferClaim, gloAuditTrail.ActivityType.TransferClaimResponsibilityToSelf, "List of Master Transaction ID - " + nBillingTransactionID, 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.TransferClaim, gloAuditTrail.ActivityType.TransferClaimResponsibilityToSelf, "List of Transaction ID - " + TrackTransactionID, 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    bReturn = false;
                //}

                //else
                //{
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.TransferClaim, gloAuditTrail.ActivityType.TransferClaimResponsibilityToSelf, "Successfully Transfer Claim Responsibility to Self.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.TransferClaim, gloAuditTrail.ActivityType.TransferClaimResponsibilityToSelf, "List of Master Transaction ID - " + nBillingTransactionID, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.TransferClaim, gloAuditTrail.ActivityType.TransferClaimResponsibilityToSelf, "List of Transaction ID - " + TrackTransactionID, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    bReturn = true;
                //}
            }

            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.TransferClaimResponsibilityToSelf, "Failure Bulk Action Transfer to Self", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                bReturn = false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.TransferClaimResponsibilityToSelf, "Bulk Transfer to Self Action Completed", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
               
            }
            return bReturn;
        }

        public bool ResendMultipleClaims(string nBillingTransactionID, string TrackTransactionID, DateTime dtCloseDate, out DataTable dtLog)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string _Error = "";
            dtLog = null;
            string outError = null;
            bool bReturn = false;
            DataTable dtResult = null;
            Int64 nAuditLogID = GetUniqueID();
            if (TrackTransactionID != "")
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.ResendBatchClaims, "Bulk Resend Action Started" + " : " + "List of Selected " + " Claim(s) - " + TrackTransactionID, 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            }
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nBillingTransactionID", nBillingTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@TrackTransactionID", TrackTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@dtCloseDate", dtCloseDate, ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@sErrMessage", _Error, ParameterDirection.InputOutput, SqlDbType.VarChar, 50000);
                oParameters.Add("@PrintFlag", false, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sMachineName", Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("BL_SplitTransactionClaim_ResendClaims", oParameters, out dtResult, out outError);
                oDB.Disconnect();
                if (dtResult != null && dtResult.Rows.Count > 0)
                {


                    if (outError == "Success")
                    {
                        if (dtResult != null && dtResult.Rows.Count > 0)
                        {
                            DataRow[] dr = dtResult.Select("Description <>'Success'");
                            if (dr != null && dr.Length > 0)
                            {
                                BulkActionLog(dr.CopyToDataTable<DataRow>(), CollectionEnums.FollowUpType.Claim, gloAuditTrail.ActivityOutCome.Success, "Bulk Resend Action", "List of Skipped", nAuditLogID, gloAuditTrail.ActivityType.ResendBatchClaims);
                            }
                            DataRow[] dr1 = dtResult.Select("Description='Success'");
                            if (dr1 != null && dr1.Length > 0)
                            {
                                BulkActionLog(dr1.CopyToDataTable<DataRow>(), CollectionEnums.FollowUpType.Claim, gloAuditTrail.ActivityOutCome.Success, "Bulk Resend Action", "List of Successed", nAuditLogID, gloAuditTrail.ActivityType.ResendBatchClaims);
                            }
                        }

                        dtResult.Columns.Remove("nTransactionID");
                        dtResult.AcceptChanges();
                        dtLog = dtResult;
                        bReturn = true;
                    }
                }
                else
                {
                    bReturn = false;
                }
                //if (dtResult != null && dtResult.Rows.Count > 0)
                //{
                //    dtLog = dtResult;
                //}
                //if (outError != null && Convert.ToString(outError).Trim() != "")  // Display comments to the user.
                //{
                //    _Error = Convert.ToString(outError).Trim();
                //    gloAuditTrail.gloAuditTrail.ExceptionLog("Error while Resending Batched Claim Responsibility to Self. " + _Error, false);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.ResendBatchClaims, gloAuditTrail.ActivityType.ResendBatchClaims, "Failure in Resending Batched Claim.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.ResendBatchClaims, gloAuditTrail.ActivityType.ResendBatchClaims, "List of Master Transaction ID - " + nBillingTransactionID, 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.ResendBatchClaims, gloAuditTrail.ActivityType.ResendBatchClaims, "List of Transaction ID - " + TrackTransactionID, 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    bReturn = false;
                //}

                //else
                //{
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.ResendBatchClaims, gloAuditTrail.ActivityType.ResendBatchClaims, "Successfully Resent Batched Claim.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.ResendBatchClaims, gloAuditTrail.ActivityType.ResendBatchClaims, "List of Master Transaction ID - " + nBillingTransactionID, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.ResendBatchClaims, gloAuditTrail.ActivityType.ResendBatchClaims, "List of Transaction ID - " + TrackTransactionID, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    bReturn = true;
                //}
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.ResendBatchClaims, "Failure Bulk Resend Action", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                bReturn = false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.ResendBatchClaims, "Bulk Resend Action Completed", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            }
            return bReturn;
        }

        public bool RebillMultipleClaims(string nBillingTransactionID, string TrackTransactionID, DateTime dtCloseDate, out DataTable dtLog, DataTable tvpClaimRemitanceInfo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string _Error = "";
            string outError = "";
            bool bReturn = false;
            dtLog = null;
            DataTable dtResult = null;
            Int64 nAuditLogID = GetUniqueID();
            if (TrackTransactionID != "")
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.RebillBatchClaims, "Bulk Rebill Started" + " : " + "List of Selected " + " Claim(s) - " + TrackTransactionID, 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            }
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nBillingTransactionID", nBillingTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@TrackTransactionID", TrackTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@dtCloseDate", dtCloseDate, ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@sErrMessage", _Error, ParameterDirection.InputOutput, SqlDbType.VarChar, 50000);
                oParameters.Add("@PrintFlag", false, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sMachineName", Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@tvpClaimRemitanceInfo", tvpClaimRemitanceInfo, ParameterDirection.Input, SqlDbType.Structured);

                oDB.Retrive("BL_SplitTransactionClaim_Rebill", oParameters, out dtResult, out outError);
                if (dtResult != null && dtResult.Rows.Count > 0)
                {


                    if (outError == "Success")
                    {
                        if (dtResult != null && dtResult.Rows.Count > 0)
                        {
                            DataRow[] dr = dtResult.Select("Description <>'Success'");
                            if (dr != null && dr.Length > 0)
                            {
                                BulkActionLog(dr.CopyToDataTable<DataRow>(), CollectionEnums.FollowUpType.Claim, gloAuditTrail.ActivityOutCome.Success, "Bulk Rebill Action", "List of Skipped", nAuditLogID, gloAuditTrail.ActivityType.RebillBatchClaims);
                            }
                            DataRow[] dr1 = dtResult.Select("Description='Success'");
                            if (dr1 != null && dr1.Length > 0)
                            {
                                BulkActionLog(dr1.CopyToDataTable<DataRow>(), CollectionEnums.FollowUpType.Claim, gloAuditTrail.ActivityOutCome.Success, "Bulk Rebill Action", "List of Successed", nAuditLogID, gloAuditTrail.ActivityType.RebillBatchClaims);
                            }
                        }

                        dtResult.Columns.Remove("nTransactionID");
                        dtResult.AcceptChanges();
                        dtLog = dtResult;
                        bReturn = true;
                    }
                }
                else
                {
                    bReturn = false;
                }
                //if (dtResult != null && dtResult.Rows.Count > 0)
                //{
                //    dtLog = dtResult;
                //}
                //oDB.Disconnect();

                //if (outError != null && Convert.ToString(outError).Trim() != "")  // Display comments to the user.
                //{
                //    _Error = Convert.ToString(outError).Trim();
                //    gloAuditTrail.gloAuditTrail.ExceptionLog("Error while Rebilling Batched Claim Responsibility to Self. " + _Error, false);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.RebillBatchClaims, "Failure in Rebilling Batched Claim.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.RebillBatchClaims, "List of Master Transaction ID - " + nBillingTransactionID, 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.RebillBatchClaims, "List of Transaction ID - " + TrackTransactionID, 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    bReturn = false;
                //}

                //else
                //{
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.RebillBatchClaims, "Successfully Rebilled Batched Claim.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.RebillBatchClaims, "List of Master Transaction ID - " + nBillingTransactionID, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.RebillBatchClaims, "List of Transaction ID - " + TrackTransactionID, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                //    bReturn = true;
                //}
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.RebillBatchClaims, "Failure Bulk Rebill", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                bReturn = false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.InsuranceFollowUpWQ, gloAuditTrail.ActivityType.RebillBatchClaims, "Bulk Rebill Completed", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            }
            return bReturn;
        }

        public DataTable GetFollowUpAction_Merge(CollectionEnums.FollowUpType nType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtFollowUpCode = null;
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nFollowupActionType", nType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oDB.Retrive("gsp_GetScheduledFollowupAction", oParameters, out dtFollowUpCode);
                oDB.Disconnect();
                return dtFollowUpCode;
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (dtFollowUpCode != null) { dtFollowUpCode.Dispose(); }
            }
        }

        public bool MergeScheduledActions(long nMergeInToScheduledActionId, string sMergeFromScheduledActionIds, CollectionEnums.FollowUpType nFollowupType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool result = false;
            string _Error = "";
            string outError = string.Empty;
            DataTable dtResult = null;
            try
            {
                if (oDB != null && oParameters != null)
                {
                    oDB.Connect(false);
                    oParameters.Add("@MergeInToScheduledActionID", nMergeInToScheduledActionId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@MergeFromScheduledActionID", sMergeFromScheduledActionIds, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@FollowUpActionType", nFollowupType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oParameters.Add("@sErrMessage", _Error, ParameterDirection.InputOutput, SqlDbType.VarChar, 50000);
                    oDB.Retrive("gsp_MergeScheduledActions", oParameters, out dtResult, out outError);
                    oDB.Disconnect();
                    if (dtResult!=null&&dtResult.Rows.Count>0)
                    {
                        if (outError == "Success")
                        {
                            string sFromScheduledActions = string.Empty;
                            string sToScheduledAction = "";
                            foreach (DataRow dr in dtResult.Rows)
                            {
                                if (sFromScheduledActions == string.Empty)
                                    sFromScheduledActions = Convert.ToString(dr["OldActionCode"]);
                                else
                                    sFromScheduledActions = sFromScheduledActions + ", " + Convert.ToString(dr["OldActionCode"]);
                                sToScheduledAction = Convert.ToString(dr["NewActionCode"]);
                            }
                            string sLog = string.Format("Follow up scheduled actions \"{0}\" merged into \"{1}\"", sFromScheduledActions, sToScheduledAction);
                            DeletefromCrosswalk(Convert.ToInt64(sMergeFromScheduledActionIds));
                            result = true;
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUpScheduledActions, gloAuditTrail.ActivityType.merge, sLog, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, true);
                        }
                        else
                        {
                            result = false;
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUpScheduledActions, gloAuditTrail.ActivityType.merge, "Error in merging scheduled actions: " + outError, 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR, true);
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                result = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUpScheduledActions, gloAuditTrail.ActivityType.merge, "SQL exception in scheduled actions merging: "+ex.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR, true);
            }
            catch (Exception ex)
            {
                result = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUpScheduledActions, gloAuditTrail.ActivityType.merge, "exception in scheduled actions merging: "+ ex.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }

            }
            return result;
        }
        public bool DeletefromCrosswalk(Int64 sMergeFromScheduledActionIds)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@ID", sMergeFromScheduledActionIds, System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("gsp_DeleteFollowupFromInsCrosswalk", oParameters);
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }

            }
            return true;
        }
        #region"Setting From SettingTable"
        //public static bool ShowClaimStatus()
        //{
        //    DataTable dtSettings = null;
        //    bool bShowClaimStatus = false;
        //    try
        //    {
        //        GeneralSettings oSettings = new GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //        dtSettings = oSettings.GetSetting("EnableRealTimeClaimStatus");

        //        if (dtSettings != null && dtSettings.Rows.Count > 0)
        //        {
        //            bShowClaimStatus = Convert.ToBoolean(dtSettings.Rows[0]["sSettingsValue"]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (dtSettings!=null)
        //        {
        //            dtSettings.Dispose();
        //            dtSettings = null;
        //        }
        //    }
        //    return bShowClaimStatus;
        //}
        #endregion

        public static bool ShowClaimStatus(long ContactId, long ClinicId)
        {
            DataTable dtSettings = null;
            bool bShowClaimStatus = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBPara = new gloDatabaseLayer.DBParameters();
            try
            {
                if (oDB.Connect(false))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@ContactId", ContactId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@ClinicId", ClinicId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("EDI_GetClearingHouse_CSI", oDBPara, out dtSettings);
                    oDB.Disconnect();
                    if (dtSettings != null && dtSettings.Rows.Count > 0)
                    {
                        bShowClaimStatus = Convert.ToBoolean(dtSettings.Rows[0]["bEnableRealTimeClaimStatus"]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dtSettings != null)
                {
                    dtSettings.Dispose();
                    dtSettings = null;
                }
            }
            return bShowClaimStatus;
        }


        #endregion " Public & Private Methods "

    }

    public static class CL_PaymentPlan
    {
        public static Int64 AddModifyPaymentPlan(Int64 PlanID, Int64 PatientID, Int64 AccountID, Int64 AccountPatientID, string PlanAmount)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = new object();
            try
            {
                oDB.Connect(false);

                oParameters.Add("@nPlanID", PlanID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nAccountPatientID", AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                if (PlanAmount != "")
                {
                    oParameters.Add("@dPlanAmount", PlanAmount, ParameterDirection.Input, SqlDbType.Decimal);
                }
                else
                {
                    oParameters.Add("@dPlanAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                }
                oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sMachineName", Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Execute("CL_INUP_PaymentPlan", oParameters, out _oResult);

                oDB.Disconnect();

                return Convert.ToInt64(_oResult);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

        public static DataSet GetPaymentPlan(Int64 nAccounntID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsFollowUpCode = null;
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nPAccountID", nAccounntID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("CL_GETPaymentPlan",oParameters,out dsFollowUpCode);
                oDB.Disconnect();
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
                if (oParameters != null)
                    oParameters.Dispose();
            }
            return dsFollowUpCode;
        }
    }

    public static class CLsCL_RevenueCycle
    {

        public static DataTable getPatAccQueueDetails(string ActionCode, Boolean bIsFutureItems, Int64 nBusinessCenterId)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtPatQueue = null;
            try
            {
                oDB.Connect(false);
                if (ActionCode == "")
                    oParameters.Add("@ActionCodes", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar);
                else
                    oParameters.Add("@ActionCodes", ActionCode, ParameterDirection.Input, SqlDbType.VarChar);

                if (nBusinessCenterId == 0)
                    oParameters.Add("@nBusinessCenterId", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar);
                else
                    oParameters.Add("@nBusinessCenterId", nBusinessCenterId, ParameterDirection.Input, SqlDbType.VarChar);


                oParameters.Add("@bIncludeFutureItems", bIsFutureItems, ParameterDirection.Input, SqlDbType.Bit);

                oDB.Retrive("CL_PatAccTAB_Revenue_Cycle", oParameters, out dtPatQueue);
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
            return dtPatQueue;
        }

        public static DataTable getBadDebtAccQueueDetails(string ActionCode, Boolean bIsFutureItems, Int64 nBusinessCenterId)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtPatQueue = null;
            try
            {
                oDB.Connect(false);
                if (ActionCode == "")
                    oParameters.Add("@ActionCodes", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar);
                else
                    oParameters.Add("@ActionCodes", ActionCode, ParameterDirection.Input, SqlDbType.VarChar);

                if (nBusinessCenterId == 0)
                    oParameters.Add("@nBusinessCenterId", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar);
                else
                    oParameters.Add("@nBusinessCenterId", nBusinessCenterId, ParameterDirection.Input, SqlDbType.VarChar);


                oParameters.Add("@bIncludeFutureItems", bIsFutureItems, ParameterDirection.Input, SqlDbType.Bit);

                oDB.Retrive("CL_BadDebtAccTAB_Revenue_Cycle", oParameters, out dtPatQueue);
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
            return dtPatQueue;
        }

        public static DataTable getInsClaimQueueDetails(string ActionCode, string InsCmpnies, Boolean bIsFutureItems, Int64 nInsBusinessID, Boolean bBeforegloCollect, Boolean bgloCollect,Boolean bIsDisplayTFLDTL)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtInsQueue = null;
            try
            {
                oDB.Connect(false);
                if (ActionCode=="")
                    oParameters.Add("@ActionCodes", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar);
                else
                    oParameters.Add("@ActionCodes", ActionCode, ParameterDirection.Input, SqlDbType.VarChar);
                if (InsCmpnies=="")
                oParameters.Add("@InsuranceCompanies", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar);
                else 
                    oParameters.Add("@InsuranceCompanies", InsCmpnies, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@bIncludeFutureItems", bIsFutureItems, ParameterDirection.Input, SqlDbType.Bit);
                if (nInsBusinessID == 0)
                    oParameters.Add("@nBusinessCenterId", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                else
                    oParameters.Add("@nBusinessCenterId", nInsBusinessID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bBeforegloCollect", bBeforegloCollect, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@bgloCollect", bgloCollect, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@bIncludeBalance", bIsDisplayTFLDTL, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Retrive("CL_InsClaimTAB_Revenue_Cycle", oParameters, out dtInsQueue);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null){oDB.Dispose();}
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return dtInsQueue;
        }

        public static DataSet getInsClaimQueueBannerDetails(Int64 nTransactionID, Int64 nPatientID, Int64 InsuranceID, Int64 ContactID,Int64 AccountID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsInsQueue = null;
            try
            {
                oDB.Connect(false);

                oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionID", nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCurrentPartyContactID",ContactID , ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCurrentPartyInsId", InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("CL_GetClaimDemographics", oParameters, out dsInsQueue);
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
            return dsInsQueue;
        }

        public static DataSet getDashBoardDtl()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet dtDashBoard = null;
            try
            {
                oDB.Connect(false);
                oDB.Retrive("CL_GETRevenueCycle_Summary", oParameters, out dtDashBoard);
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
            return dtDashBoard;
        }
    }

    public class PatientDetail : IDisposable
    {

        #region "Constructor & Destructor"

        public PatientDetail()
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

        ~PatientDetail()
        {
            Dispose(false);
        }

        #endregion "Constructor & Destructor"

        #region "Private variables"

        private Int64 _nPatientID = 0;
        private Int64 _nPatientAccountID = 0;
        private Int64 _nTransactionID = 0;
        private Int64 _nMstTransactionID = 0;
        private DateTime _dtLogTimeStamp = DateTime.MinValue;
        private DateTime _dtFollowUpTimeStamp = DateTime.MinValue;
        private string _TFL_DFL = "";
        private DateTime _dtTFL_DFLTimeStamp = DateTime.MinValue;
        private Int64 _nContactID = 0;
        private Int64 _nInsuranceID = 0;
        private Int64 _nScheduleId = 0;
        private decimal _dAvailableReservesAmt = 0;
        private decimal _dRemainingAccountBalanceWithourNonServiceCode = 0;
        private string _sAccountNo = string.Empty;
        private string _sAccountPatientName = string.Empty;
        private decimal _dRemainingAccountBalanceWithNoServiceCode = 0;
        #endregion "Private variables"

        #region "Property Procedures"

        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        public Int64 PatientAccountID
        {
            get { return _nPatientAccountID; }
            set { _nPatientAccountID = value; }
        }

        public Int64 TransactionID
        {
            get { return _nTransactionID; }
            set { _nTransactionID = value; }
        }

        public Int64 MstTransactionID
        {
            get { return _nMstTransactionID; }
            set { _nMstTransactionID = value; }
        }

        public DateTime dtLogTimeStamp
        {
            get { return _dtLogTimeStamp; }
            set { _dtLogTimeStamp = value; }
        }

        public DateTime dtFollowUpTimeStamp
        {
            get { return _dtFollowUpTimeStamp; }
            set { _dtFollowUpTimeStamp = value; }
        }

        public string TFL_DFL
        {
            get { return _TFL_DFL; }
            set { _TFL_DFL = value; }
        }

        public DateTime dtTFL_DFLTimeStamp
        {
            get { return _dtTFL_DFLTimeStamp; }
            set { _dtTFL_DFLTimeStamp = value; }
        }

        public Int64 ContactID
        {
            get { return _nContactID; }
            set { _nContactID = value; }
        }

        public Int64 InsuranceID
        {
            get { return _nInsuranceID; }
            set { _nInsuranceID = value; }
        }

        public Int64 ScheduleId
        {
            get { return _nScheduleId; }
            set { _nScheduleId = value; }
        }
        public decimal dAvailableReservesAmt
        {
            get { return _dAvailableReservesAmt; }
            set { _dAvailableReservesAmt = value; }
        }
        public decimal dRemainingAccountBalanceWithourNonServiceCode
        {
            get { return _dRemainingAccountBalanceWithourNonServiceCode; }
            set { _dRemainingAccountBalanceWithourNonServiceCode = value; }
        }
        public string sAccountNo
        {
            get { return _sAccountNo; }
            set { _sAccountNo = value; }
        }
        public string sAccountPatientName
        {
            get { return _sAccountPatientName; }
            set { _sAccountPatientName = value; }
        }
        public decimal dRemainingAccountBalanceWithNoServiceCode
        {
            get { return _dRemainingAccountBalanceWithNoServiceCode; }
            set { _dRemainingAccountBalanceWithNoServiceCode = value; }
        }
        #endregion "Property Procedures"

    }

    public class PatientDetails : IDisposable
    {
        protected System.Collections.ArrayList _innerlist;

        #region "Constructor & Destructor"

        public PatientDetails()
        {
            _innerlist = new System.Collections.ArrayList();
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


        ~PatientDetails()
        {
            Dispose(false);
        }
        #endregion


        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(PatientDetail item)
        {
            _innerlist.Add(item);
        }



        public bool Remove(PatientDetail item)
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

        public PatientDetail this[int index]
        {
            get
            { return (PatientDetail)_innerlist[index]; }
        }

        public bool Contains(PatientDetail item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(PatientDetail item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(PatientDetail[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    #endregion


    

   
}
