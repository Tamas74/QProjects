using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using gloBilling.Common;
using System.Windows.Forms;
using System.Collections;

namespace gloBilling
{
    class ClsBL_Hold
    {

        #region "Constructor & Distructor"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private string _databaseconnectionstring = "";
        private string _emrdatabaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
      
        public Int64 ClinicID { get; set; }
        public Int64 UserId { get; set; }
        public string UserName { get; set; }
        
       

        public ClsBL_Hold(string DatabaseConnectionString, string EMRDatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            _emrdatabaseconnectionstring = EMRDatabaseConnectionString;

            #region "Retrive Clinic From appsettings"
            
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { ClinicID = 0; }
            }
            else
            { ClinicID = 0; } 

            #endregion

            #region "Retrive Message Box Caption From appSettings"
            
            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; } 

            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    UserId = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                UserId = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                UserName = "";
            }

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

        ~ClsBL_Hold()
        {
            Dispose(false);
        }

        #endregion
        
        #region "Check For Parent Claim"

        public bool IsParentClaim(Int64 nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string sQuery = String.Empty;
            DataTable dtClaim = new DataTable();
            bool bReturn = false;

            try
            {
                oDB.Connect(false);
                sQuery = " SELECT nTransactionMasterID,nTransactionID,nClaimNo,nSubClaimNo FROM BL_TRANSACTION_CLAIM_MST WITH (NOLOCK) "
                          + " WHERE nParentTransactionID = " + nTransactionID;

                oDB.Retrive_Query(sQuery, out dtClaim);

                if (dtClaim.Rows.Count > 0 && dtClaim != null)
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
            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); }
                dtClaim.Dispose();

            }

            return bReturn;
        }

        #endregion

        #region " Hold or Unhold Claim "

        public int HoldUnholdClaim(ClaimHold oClaimHold, Int64 _TransactionMasterID, Int64 _TransactionID)
        {

            int _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                #region "Inserting Hold Information"

                //********************************
                //By Debasish on 21st April 2010
                //Hold Information
                if (oClaimHold != null)
                {
                    if (oClaimHold.HoldReason != "" && oClaimHold.HoldModified == true)
                    {
                        oDBParameters.Clear();
                        oDBParameters.Add("@nTransactionMstID", _TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nTransactionID", _TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@bIsHold", oClaimHold.IsHold, ParameterDirection.Input, SqlDbType.Bit);
                        oDBParameters.Add("@sHoldReason", oClaimHold.HoldReason.Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@dtHoldDateTime", oClaimHold.HoldDateTime, ParameterDirection.Input, SqlDbType.DateTime);
                        oDBParameters.Add("@nHoldUserID", oClaimHold.HoldUserID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nHoldModUserID", UserId, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@dtHoldModDateTime", oClaimHold.HoldModDateTime, ParameterDirection.Input, SqlDbType.DateTime);
                        oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nHoldBillingID", oClaimHold.HoldID, ParameterDirection.Input, SqlDbType.BigInt);

                        //Console.WriteLine(gloDatabaseLayer.DBLayer.getProcedureExeCode("BL_UPDATE_CLAIM_HOLD", oDBParameters));

                        oDB.Connect(false);
                        _result = oDB.Execute("BL_UPDATE_CLAIM_HOLD", oDBParameters);
                        oDB.Disconnect();
                    }
                }
                //*********************************

                #endregion
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                _result = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = 0;
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
            return _result;
        }

        #endregion
        
        #region "Check For Insurance Plan on Hold "

        public bool IsInsurancePlanOnHold(Int64 nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            object _retVal = null;
            bool _isOnHold = false;

            try
            {
                _sqlQuery = " SELECT bIsHold FROM BL_Insurance_PlanHold WITH(NOLOCK) WHERE nContactID = " + nContactID + "";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                { _isOnHold = true; }
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
            return _isOnHold;
        } 
        #endregion

        #region "Release hold Information"
        
        public void ReleaseHoldIfVoided(Int64 TransactionId, Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                #region " Release hold Information "

                oParameters.Clear();
                oParameters.Add("@nTransactionID", TransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nUserID", UserId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                int val = oDB.Execute("BL_UPDATE_HOLD_RELEASE", oParameters);
                oDB.Disconnect();

                #endregion
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }
 
        #endregion

        #region "RemoveClaimsForHold -Delete whole batch if single claim is there"

        //this function will remove claims from Batch and will delete whole batch if single claim is there.
        public void RemoveClaimsForHold(Int64 _TransactionID, TransactionStatus _stauts)
        {

            gloDatabaseLayer.DBLayer oDB = null;
            DataTable dtTrasactionIDs = null;
            string _strQuery = String.Empty;
            Object objCountClaims = null;
            try
            {

                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);

                _strQuery = "SELECT BL_Transaction_Batch.nBatchID, BL_Transaction_Batch.sBatchName, BL_Transaction_Batch.nClaimCounter " +
                                                   " FROM BL_Transaction_Batch_DTL WITH(NOLOCK) INNER JOIN " +
                                                   " BL_Transaction_Batch WITH(NOLOCK) ON BL_Transaction_Batch_DTL.nBatchID = BL_Transaction_Batch.nBatchID " +
                                                   " where BL_Transaction_Batch_DTL.nTransactionID= " + _TransactionID + "";
                DataTable dtBatch = new DataTable();
                oDB.Retrive_Query(_strQuery, out dtBatch);
                if (dtBatch != null && dtBatch.Rows.Count > 0)
                {
                    if (_stauts == TransactionStatus.Batch)
                    {
                        _strQuery = "";
                        _strQuery = "Delete BL_Transaction_Batch_DTL where nTransactionID=" + _TransactionID + "  ";
                        _strQuery = _strQuery + " Update BL_Transaction_Claim_MST set nStatus=2 where nTransactionID=" + _TransactionID + " and nStatus Not IN(2,20,17,16,0)";
                        oDB.Execute_Query(_strQuery);
                    }

                    #region "Count the Number Of Claims in the Batch"

                    _strQuery = "";
                    _strQuery = "SELECT Count(nTransactionID) " +
                                     " FROM BL_Transaction_Batch_DTL WITH(NOLOCK) " +
                                     " where nBatchID=" + Convert.ToInt64(dtBatch.Rows[0]["nBatchID"]) + "";
                    objCountClaims = oDB.ExecuteScalar_Query(_strQuery);

                    #endregion

                    if (objCountClaims != null && Convert.ToInt32(objCountClaims) == 0)
                    {
                        _strQuery = "";
                        _strQuery = "  Delete BL_Transaction_Batch  where nBatchID=" + Convert.ToInt64(dtBatch.Rows[0]["nBatchID"]) + "  ";
                        oDB.Execute_Query(_strQuery);
                    }


                }

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
                if (dtTrasactionIDs != null)
                {
                    dtTrasactionIDs.Dispose();
                }
            }
        } 
        #endregion

        #region "RemovePlanHoldClaimsFromBatch -Delete whole batch if single claim is there in case of Plan Hold"

        //this function will remove claims from Batch and will delete whole batch if single claim is there in case of Plan Hold
        public void RemovePlanHoldClaimsFromBatch(Int64 _nContactID, Int64 _nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            DataTable dtTrasactionIDs = null;
            string _strBatchClaimsCount = String.Empty;
            Object objCountClaims = null;
            try
            {

                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nContactID", Convert.ToInt64(_nContactID), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionID", Convert.ToInt64(_nTransactionID), ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_REMOVE_PLANHOLDCLAIM_FROM_BATCH", oDBParameters, out dtTrasactionIDs);

                if (dtTrasactionIDs != null && dtTrasactionIDs.Rows.Count > 0)
                {
                    for (int nTransID = 0; nTransID < dtTrasactionIDs.Rows.Count; nTransID++)
                    {
                        _strBatchClaimsCount = "SELECT BL_Transaction_Batch.nBatchID, BL_Transaction_Batch.sBatchName, BL_Transaction_Batch.nClaimCounter " +
                                                    " FROM BL_Transaction_Batch_DTL WITH(NOLOCK) INNER JOIN " +
                                                    " BL_Transaction_Batch WITH(NOLOCK) ON BL_Transaction_Batch_DTL.nBatchID = BL_Transaction_Batch.nBatchID " +
                                                    " where BL_Transaction_Batch_DTL.nTransactionID= " + Convert.ToInt64(dtTrasactionIDs.Rows[nTransID]["nTransactionID"]) + "";
                        DataTable dtBatch = new DataTable();
                        oDB.Retrive_Query(_strBatchClaimsCount, out dtBatch);
                        if (dtBatch != null && dtBatch.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtTrasactionIDs.Rows[nTransID]["nStatus"]).Trim() == "3")
                            {
                                _strBatchClaimsCount = "Delete BL_Transaction_Batch_DTL where nTransactionID=" + Convert.ToInt64(dtTrasactionIDs.Rows[nTransID]["nTransactionID"]) + "  ";
                                _strBatchClaimsCount = _strBatchClaimsCount + " Update BL_Transaction_Claim_MST set nStatus=2 where nTransactionID=" + Convert.ToInt64(dtTrasactionIDs.Rows[nTransID]["nTransactionID"]) + " and nStatus Not IN(2,20,17,16,0)";
                                oDB.Execute_Query(_strBatchClaimsCount);
                            }


                            #region "Count the number of claims in the Batch"

                            string _strQuery = "SELECT Count(nTransactionID) FROM BL_Transaction_Batch_DTL WITH(NOLOCK) where nBatchID=" + Convert.ToInt64(dtBatch.Rows[0]["nBatchID"]) + "";
                            objCountClaims = oDB.ExecuteScalar_Query(_strQuery);

                            #endregion

                            if (objCountClaims != null && Convert.ToInt32(objCountClaims) == 0)
                            {
                                _strBatchClaimsCount = "";
                                _strBatchClaimsCount = " Delete BL_Transaction_Batch  where nBatchID=" + Convert.ToInt64(dtBatch.Rows[0]["nBatchID"]) + "  ";
                                oDB.Execute_Query(_strBatchClaimsCount);
                            }


                        }
                        _strBatchClaimsCount = "";
                        objCountClaims = null;

                    }
                }

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
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                }
                if (dtTrasactionIDs != null)
                {
                    dtTrasactionIDs.Dispose();
                }
            }
        } 
        #endregion

    }
}
