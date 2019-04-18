using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;


namespace ChargeRules
{
    public class ClsRuleEngine
    {

        #region "Private Variables"

        private string _databaseconnectionstring = "";
        private static string _MessageBoxCaption = String.Empty;
        private bool disposed = false;

        #endregion

        #region "Constructor and Destructor"

        public ClsRuleEngine()
        {
            _databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _MessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
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

        ~ClsRuleEngine()
        {
            Dispose(false);
        }
        #endregion

        #region "Public Methods "

        public DataTable GetOperators()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dt = null;
            try
            {

                oDB.Connect(false);
                oDB.Retrive("BL_getOperators", out _dt);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _dt;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _dt;
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
            return _dt;
        }

        public DataSet getRuleData(Int64 nRuleID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet _ds = null;
            try
            {

                oDB.Connect(false);
                oParameters.Add("@nRuleID", nRuleID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("ClaimRule_GetRules", oParameters, out _ds);
            }
            catch (gloDatabaseLayer.DBException ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return _ds;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _ds;
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
            return _ds;
        }

        public Int64 SaveRuleMasterData(Int64 nRuleID, string sRuleName, string sRuleDescription, int nEvaluationLogic, String sExpression, String sErrorMessage, int nRuleType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            object RuleID = null;
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nRuleID", nRuleID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sRuleName", sRuleName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sRuleDescription", sRuleDescription, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nEvaluationLogic", nEvaluationLogic, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@sExpression", sExpression, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sRuleErrorMessage", sErrorMessage, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@bIsActive", 0, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nRuleType", nRuleType, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@sVersion", gloGlobal.gloPMGlobal.ApplicationVersion, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Execute("ClaimRule_MasterInsertUpdate", oParameters, out RuleID);

                if (RuleID != null)
                {
                    return (Convert.ToInt64(RuleID));
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx, false);
                MessageBox.Show(dbEx.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }

            return (Convert.ToInt64(RuleID));

        }
        public void SaveRuleDetailsData(DataTable dtRuleConditions)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
          
            try
            {
                oDB.Connect(true);
                oParameters.Add("@TVPRuleConditions", dtRuleConditions, ParameterDirection.Input, SqlDbType.Structured);
                oDB.ExecuteWithTransaction("ClaimRule_RuleConditionsInsertUpdate", oParameters);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                oDB.Rollback();
                RollBackMasterData(Convert.ToInt64(dtRuleConditions.Rows[0]["nRuleID"]));
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx, false);
                MessageBox.Show(dbEx.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
              
            }
            catch (Exception ex)
            {
                oDB.Rollback();
                RollBackMasterData(Convert.ToInt64(dtRuleConditions.Rows[0]["nRuleID"]));
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }

            
        }

        public void  RollBackMasterData(Int64 nRuleID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
           
            try
            {
                oDB.Connect(true);
                oParameters.Add("@nRuleID", nRuleID, ParameterDirection.Input, SqlDbType.Structured);
                oDB.ExecuteWithTransaction("ClaimRule_DeleteRulesFromMaster", oParameters);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                oDB.Rollback();
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx, false);
                MessageBox.Show(dbEx.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            catch (Exception ex)
            {
                oDB.Rollback();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }

            
        }

        public void DeleteRules(Int64 nRuleID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
           
            try
            {
                oDB.Connect(true);
                oParameters.Add("@nRuleID", nRuleID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.ExecuteWithTransaction("ClaimRule_Delete", oParameters);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                oDB.Rollback();
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx, false);
                MessageBox.Show(dbEx.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            catch (Exception ex)
            {
                oDB.Rollback();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }

           

        }

        public void ActiveDeactiveRule(int nActive, Int64 nRuleID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
           
            string Query = "";
            try
            {
                oDB.Connect(false);
                Query = "UPDATE ClaimRule_Master SET bIsActive=" + nActive + " WHERE  nRuleID=" + nRuleID;

                oDB.Execute_Query(Query);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx, false);
                MessageBox.Show(dbEx.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }

            

        }

        public DataSet GetRules(Int64 nRuleID = 0, Boolean bIncludeInactiveRules = false, bool bIsServiceCall = false, string sAUSID = "")
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet _dsRules = null;

            try
            {
                oDB.Connect(false);
                oParameters.Add("@nRuleID", nRuleID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bIncludeInactiveRules", bIncludeInactiveRules, ParameterDirection.Input, SqlDbType.BigInt);
                if (bIsServiceCall)
                {
                    oParameters.Add("@bIsServiceCall", bIsServiceCall, ParameterDirection.Input, SqlDbType.Bit);
                    oParameters.Add("@sAUSID", sAUSID, ParameterDirection.Input, SqlDbType.VarChar); 
                }
                oDB.Retrive("gsp_GetClaimRules",oParameters, out _dsRules);
                oDB.Disconnect();

                if (_dsRules != null && _dsRules.Tables != null && _dsRules.Tables.Count > 0)
                {
                    _dsRules.Tables[0].TableName = "RuleMaster";
                    _dsRules.Tables[1].TableName = "RuleConditions";
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

            return _dsRules;
        }

        public DataSet GetRules(DataTable TVP)
        {
            DataSet ds = null;
            string _MessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataSet _ds = null;
                try
                {
                    oDB.Connect(false);
                    oParameters.Add("@TVPRuleIDs", TVP, ParameterDirection.Input, SqlDbType.Structured);
                    oDB.Retrive("ClaimRule_ExportRules", oParameters, out _ds);
                }
                catch (gloDatabaseLayer.DBException ex)
                {

                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return _ds;

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return _ds;
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
                return _ds;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }

            return ds;
        }

        public DataSet GetRules(string SearchText)
        {
            DataSet ds = null;
            string _MessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataSet _ds = null;
                try
                {
                    oDB.Connect(false);
                    oParameters.Add("@sSearchText", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Retrive("ClaimRule_GetRules_Search", oParameters, out _ds);
                }
                catch (gloDatabaseLayer.DBException ex)
                {

                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return _ds;

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return _ds;
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
                return _ds;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }

            return ds;
        }

        #endregion

        #region For Rule Verification Form
        public static DataTable GetCachedProviders()
        {
            DataTable _dtProviders = null;

            try
            {
                _dtProviders = gloGlobal.gloPMMasters.GetProviders();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            return _dtProviders;
        }
        public static DataTable GetCachedFacilities()
        {
            DataTable _dtFacilities = null;
            try
            {
                _dtFacilities = gloGlobal.gloPMMasters.GetFacilities();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            return _dtFacilities;
        }
        
        public static DataTable GetInsuranceCompanies()
        {
            DataTable dt = null;
            try
            {
                dt = gloGlobal.gloPMMasters.GetInsuranceCompanies();
            }
            catch (Exception ex)
            {


                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return dt;

        }

        public static DataTable GetFieldValues(string sFieldName, string SearchText)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            DataTable _dtList = null;

            try
            {
                               
                    oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                    oDB.Connect(false);
                    oParameters.Clear();
                    oParameters.Add("@sFieldName", sFieldName, ParameterDirection.Input, SqlDbType.NVarChar, 50);
                    oParameters.Add("@SearchString", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Retrive("ClaimRule_MasterValues", oParameters, out _dtList);
                    if (_dtList != null)
                    {
                      return  _dtList;
                    }
                    
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return _dtList;
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
                dbEx.ERROR_Log(dbEx.Message);
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return sInsurancePlanName;
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
                dbEx.ERROR_Log(dbEx.Message);
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
                dbEx.ERROR_Log(dbEx.Message);
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
                dbEx.ERROR_Log(dbEx.Message);
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return sPlanType;
        }

        public static string GetPayerId(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            string sPlanType = string.Empty;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                sPlanType = Convert.ToString(oDB.ExecuteScalar("ClaimRule_GetInsurancePlanPayerID", oParameters));
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
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
                dbEx.ERROR_Log(dbEx.Message);
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return sBillingProviderNPI;
        }
     
        #endregion



        public int CheckRuleWithSameNameExists(long nRuleID)
        {
            int nResult = -1;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dt = null;
            try
            {

                oDB.Connect(false);
                oParameters.Add("@nRuleID", nRuleID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("ClaimRule_CheckRuleExistsWithSameName", oParameters, out _dt);
                if (_dt!=null&&_dt.Rows.Count>0)
                {
                    nResult = Convert.ToInt32(_dt.Rows[0][0]);
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return nResult;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return nResult;
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
            return nResult;
        }
    }

    public class mynode : TreeNode
    {
        public string sOperators
        { get; set; }
    }


    public class gloItem : IDisposable
    {

        #region "Constructor & Distructor"

        public gloItem(Int64 Id, string Description)
        {
            _id = Id;
            _code = "";
            _description = Description;
        }


        public gloItem(Int64 Id, string Code, string Description)
        {
            _id = Id;
            _code = Code;
            _description = Description;

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

        ~gloItem()
        {
            Dispose(false);
        }

        #endregion

        private Int64 _id = 0;
        private string _code = "";
        private string _description = "";

        public Int64 ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


    }

    public class gloItems : IDisposable, IEnumerable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public gloItems()
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


        ~gloItems()
        {
            Dispose(false);
        }
        #endregion


        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(gloItem item)
        {
            _innerlist.Add(item);


        }
        public System.Collections.IEnumerator GetEnumerator()
        {
            return _innerlist.GetEnumerator();
        }

        public int Add(Int64 Id, string Code, string Description)
        {
            gloItem item = new gloItem(Id, Code, Description);
            return _innerlist.Add(item);
        }

        public int Add(Int64 Id, string Description)
        {
            gloItem item = new gloItem(Id, Description);
            return _innerlist.Add(item);
        }

        public void Insert(int index, gloItem item)
        {
            _innerlist.Insert(index, item);
        }

        public bool Remove(gloItem item)
        {
            bool result = false;
            gloItem obj;

            for (int i = 0; i < _innerlist.Count; i++)
            {
                //store current index being checked
                //   obj = new gloItem();
                obj = (gloItem)_innerlist[i];
                if (obj.ID == item.ID && obj.Description == item.Description)
                {
                    _innerlist.RemoveAt(i);
                    result = true;
                    break;
                }
                obj = null;
            }

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

        public gloItem this[int index]
        {
            get
            {
                return (gloItem)_innerlist[index];
            }
        }

        public bool Contains(gloItem item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(gloItem item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(gloItem[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }
    }
}
