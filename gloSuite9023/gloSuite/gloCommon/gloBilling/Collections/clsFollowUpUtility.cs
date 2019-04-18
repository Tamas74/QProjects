using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 
using System.Data.SqlClient; 

namespace gloBilling.Collections
{
    public class clsFollowUpUtility
    {
        string _databaseconnectionstring = "";        
        

        public clsFollowUpUtility()
        {
            _databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString;           
        }

        public DataTable GetAllAccounts()
        {
            DataTable dtAccounts = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {               
                oDB.Connect(false);
                oDB.Retrive_Query("select nPAccountID , sAccountNo  from PA_Accounts", out  dtAccounts);
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

            return dtAccounts;
        }
        public DataTable GetAllClaims()
        {
            DataTable dtAccounts = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                oDB.Retrive_Query("select distinct nTransactionMasterID ,nTransactionID ,isnull(bIsRebilled,0) as bIsRebilled  from BL_Transaction_Claim_MST where nClaimStatus =1  and isnull(bIsVoid,0) =0 and nResponsibilityType = 2", out  dtAccounts);
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

            return dtAccounts;
        }
        public DataTable  GetDefaultAccFollowupSetting()
        {
          
            DataTable dtAccounts = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                oDB.Retrive("CL_Get_AccountFollowUp_Default", out dtAccounts);
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

            return dtAccounts;
        }



        public DataTable GetDefaultClaimsFollowupSetting()
        {

            DataTable dtAccounts = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                oDB.Retrive("CL_Get_ClaimFollowUp_Default", out dtAccounts);
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

            return dtAccounts;
        }

        public void ProcessAccount(Int64 nPAccountID, int StmtCountSet, int days, string action, string actionDescription)
        {
            DataTable dtAccounts = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);                
                oParameters.Add("@StmtCountSet", StmtCountSet, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@days", days, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@action", action, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@actionDescription", actionDescription, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@userid",  gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@username", gloGlobal.gloPMGlobal.UserName , ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Execute("CL_Create_Account_FollowUp", oParameters);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }
        public void ProcessClaim(Int64 nTransactionMstId, Int64 nTransactionId, Boolean bIsrebilled, int billdays, string billaction, string billactionDescription, int rebilldays, string rebillaction, string rebillactionDescription)
        {
            DataTable dtAccounts = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nTransactionMstId", nTransactionMstId, ParameterDirection.Input, SqlDbType.BigInt);

                oParameters.Add("@nTransactionId", nTransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bisrebilled", bIsrebilled, ParameterDirection.Input, SqlDbType.Bit);

                oParameters.Add("@billdays", billdays, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@billaction", billaction, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@billactionDescription", billactionDescription, ParameterDirection.Input, SqlDbType.VarChar);

                oParameters.Add("@rebilldays", rebilldays, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@rebillaction", rebillaction, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@rebillactionDescription", rebillactionDescription, ParameterDirection.Input, SqlDbType.VarChar);


                oParameters.Add("@userid", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@username", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);


                oDB.Execute("CL_Create_Claim_FollowUp", oParameters);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }


    }
}
