using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using gloSettings;

namespace gloBilling.gloERA
{

    #region " Enumerations "

    #endregion

    public class ClsERAValidation
    {

        #region " Constructor & Destructor "

        private bool disposed = false;

        public ClsERAValidation()
        {
            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            _DataBaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _UserID = gloGlobal.gloPMGlobal.UserID;
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
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (oDBPara != null) { oDBPara.Dispose(); oDBPara = null; }

                  

                }
            }
            disposed = true;
        }

        ~ClsERAValidation()
        {
            Dispose(false);
        }

        #endregion

        #region " Variable Declarations "

        
        private string _DataBaseConnectionString;
        private Int64 _ClinicID = 1;
        private Int64 _UserID;
        private string _MessageBoxCaption;

        private gloDatabaseLayer.DBLayer oDB;
        private gloDatabaseLayer.DBParameters oDBPara;
       // string _TempStr;

       
        #endregion

        #region " Private & Public Methods for ERA Validation. "

        public static bool CheckValidation(Int64 nBPRId, out Int32 ErrorLevel, out string sMessage)
        {

            //SP: ERA_MaintainCheckStatus; @nBPRId		Numeric(18,0)
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            SqlConnection _sqlConnection = null;
            bool blnFlag = false;
            int _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            sMessage = string.Empty;
            ErrorLevel = 0;
            try
            {
                oParameters.Clear();
                oParameters.Add("@nBPRId", nBPRId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sMachineName", Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sUserName", gloSettings.AppSettings.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", Convert.ToInt64(gloSettings.AppSettings.UserID), ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nErrorLevel", ErrorLevel, ParameterDirection.Output, SqlDbType.Int);
                oParameters.Add("@sMessage", sMessage, ParameterDirection.Output, SqlDbType.VarChar, 255);

                _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                _sqlConnection.Open();
                using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                {
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.CommandTimeout = 0;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = "ERA_CheckLevelValidation";

                    _result = _sqlCommand.ExecuteNonQuery();

                    if (Convert.ToInt32(_sqlCommand.Parameters["@nErrorLevel"].Value.ToString()) != 0)  // Display comments to the user.
                    { ErrorLevel = Convert.ToInt32(_sqlCommand.Parameters["@nErrorLevel"].Value); blnFlag = true; }

                    if (_sqlCommand.Parameters["@sMessage"].Value.ToString().Trim() != "")  // Display comments to the user.
                    { sMessage = _sqlCommand.Parameters["@sMessage"].Value.ToString(); blnFlag = true; }


                    if (_sqlCommand.Parameters != null)
                    {
                        _sqlCommand.Parameters.Clear();

                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _sqlConnection.Close();
                if (_sqlConnection != null) { _sqlConnection.Close(); }
                if (oParameters != null) { oParameters.Dispose(); }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return blnFlag;
        }

        public static bool ClaimsValidations(Int64 nBPRID, string ERAClaimNo, Int64 nCLPId, string ERAPayerID, Int64 nOperationID, out Int64 nContactID, out Int64 nInsuranceID, out int nResponsibilityNo, out bool nIsStopPosting)
        {
            //Stored Procedure name : "ERA_ClaimMatch"; Parameters : @ClaimNo NUMERIC(18,0)   

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            SqlConnection _sqlConnection = null;
            string _sqlQuery = "";
            bool _blnFlag = false;
            Object _retVal = null;
            int _result = 0;
            nContactID = 0;
            nInsuranceID = 0;
            nResponsibilityNo = 0;
            nIsStopPosting = false;
            try
            {
                _sqlQuery = "ERA_ClaimsValidations";

                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sERAClaimNo", ERAClaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nCLPId", nCLPId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sERAPayerID", ERAPayerID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nOperationID", nOperationID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nContactId", nContactID, ParameterDirection.Output, SqlDbType.BigInt);
                oParameters.Add("@nInsuranceId", nInsuranceID, ParameterDirection.Output, SqlDbType.BigInt);
                oParameters.Add("@nResPonsibilityNumber", nResponsibilityNo, ParameterDirection.Output, SqlDbType.Int);
                oParameters.Add("@nIsStopPosting", nIsStopPosting, ParameterDirection.Output, SqlDbType.Bit);
               
                _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                _sqlConnection.Open();
                using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                {
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.CommandTimeout = 0;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = _sqlQuery;

                    _result = _sqlCommand.ExecuteNonQuery();

                    if (_sqlCommand.Parameters["@nContactId"].Value != null)
                    { nContactID = (Int64)_sqlCommand.Parameters["@nContactId"].Value; }

                    if (_sqlCommand.Parameters["@nInsuranceID"].Value != null)
                    { nInsuranceID = (Int64)_sqlCommand.Parameters["@nInsuranceID"].Value; }
                    if (_sqlCommand.Parameters["@nResPonsibilityNumber"].Value != null)
                    { nResponsibilityNo = (int)_sqlCommand.Parameters["@nResPonsibilityNumber"].Value; }
                    if (_sqlCommand.Parameters["@nResPonsibilityNumber"].Value != null)
                    { nIsStopPosting = (bool)_sqlCommand.Parameters["@nIsStopPosting"].Value; }

                    if (_sqlCommand.Parameters != null)
                    {
                        _sqlCommand.Parameters.Clear();

                    }
                }

                if (nContactID > 0)
                    _blnFlag = true;
                if (nIsStopPosting == true)
                    _blnFlag = false;


                //_retVal = oDB.ExecuteScalar(_sqlQuery, oParameters);

                //if (_retVal != null && _retVal.ToString().Trim() != "")
                //{ 
                //    if(Convert.ToInt32(_retVal) >0)
                //        _blnFlag = Convert.ToBoolean(_retVal); 

                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (_retVal != null) { _retVal = null; }
                if (_sqlConnection != null) //connection close done
                {
                    if (_sqlConnection.State == ConnectionState.Open)
                        _sqlConnection.Close();
                    _sqlConnection.Dispose(); 
                }
            }
            return _blnFlag;
        }


        public static bool IsClaimMatch(Int64 nBPRID, string ERAClaimNo, Int64 nCLPId) 
        {
            //Stored Procedure name : "ERA_ClaimMatch"; Parameters : @ClaimNo NUMERIC(18,0)   

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string _sqlQuery = "";
            bool _blnFlag = false;
            Object _retVal = null;

            try
            {
                _sqlQuery = "ERA_ClaimMatch";

                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sERAClaimNo", ERAClaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nCLPId", nCLPId, ParameterDirection.Input, SqlDbType.BigInt);

                _retVal = oDB.ExecuteScalar(_sqlQuery, oParameters);

                if (_retVal != null && _retVal.ToString().Trim() != "")
                { 
                    if(Convert.ToInt32(_retVal) >0)
                        _blnFlag = Convert.ToBoolean(_retVal); 
                
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (_retVal != null) { _retVal = null; }
            }
            return _blnFlag;
        }

        public static bool DeterminePayer(Int64 nBPRId, string ERAClaimNo, Int64 nCLPID, out Int64 nContactID, out Int64 nInsuranceID, out int nResponsibilityNo) 
        {
            //nContactID = 1044623; //HardCode

            bool _blnFlag = false;

            //SP: ERA_DeterminePayer; 
            // @nBPRId
            // @nClaimNo
            // @nContactId out

           gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
           gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);    
            SqlConnection _sqlConnection = null;
            string _sqlQuery = "";
            int _result = 0;
            nContactID = 0;
            nInsuranceID = 0;
            nResponsibilityNo = 0;
            try
            {
                _sqlQuery = "ERA_DeterminePayer";
                oParameters.Clear();
                oParameters.Add("@nBPRId", nBPRId, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sERAClaimNo", ERAClaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nCLPID", nCLPID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nContactId", nContactID, ParameterDirection.Output, SqlDbType.BigInt);
                oParameters.Add("@nInsuranceId", nInsuranceID, ParameterDirection.Output, SqlDbType.BigInt);
                oParameters.Add("@nResPonsibilityNumber", nResponsibilityNo, ParameterDirection.Output, SqlDbType.Int);
                 _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                _sqlConnection.Open();
                using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                {
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = _sqlQuery;

                    _result = _sqlCommand.ExecuteNonQuery();

                    if (_sqlCommand.Parameters["@nContactId"].Value != null)
                    { nContactID = (Int64)_sqlCommand.Parameters["@nContactId"].Value; }

                    if (_sqlCommand.Parameters["@nInsuranceID"].Value != null)
                    { nInsuranceID = (Int64)_sqlCommand.Parameters["@nInsuranceID"].Value; }
                    if (_sqlCommand.Parameters["@nResPonsibilityNumber"].Value != null)
                    { nResponsibilityNo = (int)_sqlCommand.Parameters["@nResPonsibilityNumber"].Value; }

                    if (_sqlCommand.Parameters != null)
                    {
                        _sqlCommand.Parameters.Clear();

                    }
                }

                if (nContactID >0)
                    _blnFlag = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _sqlConnection.Close();
                if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();  
                }
            }

            return _blnFlag; 
        }



        public static bool IsCheckDuplicate(Int64 nBPRId, string sCheckNo,decimal nCheckAmount,Int64 nPayerId)
        {
            bool _bDuplicate = false;

            
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            SqlConnection _sqlConnection = null;
            string _sqlQuery = "";
            int _result = 0;
            try
            {
                _sqlQuery = "ERA_DuplicateCheck";
                oParameters.Clear();
                oParameters.Add("@nBPRId", nBPRId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sCheckNo", sCheckNo, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nCheckAmount", nCheckAmount, ParameterDirection.Input, SqlDbType.Decimal);
                oParameters.Add("@nPayerId", nPayerId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bIsDuplicated", _bDuplicate, ParameterDirection.Output, SqlDbType.Bit);

                _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                _sqlConnection.Open();
                using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                {
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = _sqlQuery;

                    _result = _sqlCommand.ExecuteNonQuery();

                    if (_sqlCommand.Parameters["@bIsDuplicated"].Value != null)
                    { _bDuplicate = (bool)_sqlCommand.Parameters["@bIsDuplicated"].Value; }

                    if (_sqlCommand.Parameters != null)
                    {
                        _sqlCommand.Parameters.Clear();

                    }
                }

                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _sqlConnection.Close();
                if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return _bDuplicate;
        }




        public static bool IsChargeMatchedAgaintClaim(Int64 nBPRID, string ERAClaimNo, Int64 nContactId, Int64 nCLPID,int nResponsibiltyNo) 
        {
            //Stored Procedure name : "ERA_GETCLAIMCHARGEMATCH"; Parameters : @CLAIMNO NUMERIC(18,0),@nContactId  NUMERIC(18,0)   

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string _sqlQuery = "";
            bool _blnFlag = false;
            Object _retVal = null;

            try
            {
                _sqlQuery = "ERA_CLAIMCHARGEMATCH";

                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sERAClaimNo", ERAClaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nContactId", nContactId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCLPID", nCLPID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nResponsibiltyNo", nResponsibiltyNo, ParameterDirection.Input, SqlDbType.Int);
                _retVal = oDB.ExecuteScalar(_sqlQuery, oParameters);

                if (_retVal != null && _retVal.ToString().Trim() != "")
                { _blnFlag = Convert.ToBoolean(_retVal); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (_retVal != null) { _retVal = null; }
            }
            return _blnFlag;
        }

        public static bool DetermineNextActionParty(Int64 nBPRId, Int64 nCLPId, string ERAClaimNo, Int64 nClaimStatus, Int64 nContactId, Int64 nInsuranceId, out System.Collections.Hashtable ohtTab,string sPayerID) 
        {
            bool _blnFlag = false;

            //SP: ERA_DetermineNextActionParty; 
            // @nBPRId
            // @nCLPId
            // @nClaimNo
            // @nClaimStatus
            // @nContactId

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            SqlDataReader dr = null;
            System.Collections.Hashtable htTab = null;
            string _sqlQuery = "";

            try
            {
                _sqlQuery = "ERA_DetermineNextActionParty";
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nBPRId", nBPRId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCLPId", nCLPId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sERAClaimNo", ERAClaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClaimStatus", nClaimStatus, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nContactId", nContactId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nInsuranceId", nInsuranceId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sERAPayerId", sPayerID, ParameterDirection.Input, SqlDbType.VarChar);
                //dr = new SqlDataReader();
                oDB.Retrive(_sqlQuery, oParameters, out dr);

                htTab = new System.Collections.Hashtable();
                
                // Create Hashtable Key/Value pairs
                htTab.Add("NextAction", "");
                htTab.Add("NextContactId", "");
                htTab.Add("NextParty", "");
                htTab.Add("nNextActionPatientInsID", "");
                htTab.Add("IsStopPosting", "");
                htTab.Add("bIsTranfer", "");


                if(dr.HasRows)
                {
                    dr.Read();
                    htTab["NextAction"] = dr["NextAction"].ToString();
                    htTab["NextContactId"] = dr["NextContactId"].ToString();
                    htTab["NextParty"] = dr["NextParty"].ToString();
                    htTab["nNextActionPatientInsID"] = dr["nNextActionPatientInsID"].ToString();
                    htTab["IsStopPosting"] = dr["IsStopPosting"].ToString();
                    htTab["bIsTranfer"] = dr["bIsTranfer"].ToString();

                    if (Convert.ToBoolean(dr["IsStopPosting"]))
                        _blnFlag = true;

                    dr.Close();
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
                if (oParameters != null) { oParameters.Dispose(); }
                if (dr != null) { dr.Dispose(); }
            }

            ohtTab = htTab;

            return _blnFlag; 
        }

        public static Int64 ValidateCheck(Int64 nBPRId,out string sMessage)
        {

            //SP: ERA_CheckValidations; @nBPRId		Numeric(18,0),@nCheckAmount	Numeric(15,2) out,@bIsDuplicateCheck	bit out
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            SqlConnection _sqlConnection = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);    

            int _result = 0;
            int _returnValue = 0;

            sMessage = string.Empty;
            try
            {
                oParameters.Clear();
                oParameters.Add("@nBPRId", nBPRId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sMessage", sMessage, ParameterDirection.Output, SqlDbType.VarChar, 255);

                _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                _sqlConnection.Open();
                using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                {
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 5000;
                    _sqlCommand.CommandText = "ERA_CheckValidations";
                    _result = _sqlCommand.ExecuteNonQuery();

                    if (_sqlCommand.Parameters["@sMessage"].Value.ToString().Trim() != "")  // Display comments to the user.
                    { sMessage = _sqlCommand.Parameters["@sMessage"].Value.ToString(); _returnValue = 1; }

                    if (_sqlCommand.Parameters != null)
                    {
                        _sqlCommand.Parameters.Clear();

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _sqlConnection.Close();
                if (_sqlConnection != null) { _sqlConnection.Close(); }
                if (oParameters != null) { oParameters.Dispose(); }
                
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }

            return _returnValue;
        }


        public static bool ValidateReasonCodePayer(Int64 nBPRId, out string sMessage)
        {

            //SP: ERA_CheckValidations; @nBPRId		Numeric(18,0),@nCheckAmount	Numeric(15,2) out,@bIsDuplicateCheck	bit out
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            SqlConnection _sqlConnection = null;
            
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);    

            int _result = 0;
            bool _returnValue = false;

            sMessage = string.Empty;
            try
            {
                oParameters.Clear();
                oParameters.Add("@nBPRId", nBPRId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sMessage", sMessage, ParameterDirection.Output, SqlDbType.VarChar, 255);

                _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                _sqlConnection.Open();
                using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                {
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 5000;
                    _sqlCommand.CommandText = "ERA_ValidateReasonCodePayer";

                    _result = _sqlCommand.ExecuteNonQuery();

                    if (_sqlCommand.Parameters["@sMessage"].Value.ToString().Trim() != "")  // Display comments to the user.
                    { sMessage = _sqlCommand.Parameters["@sMessage"].Value.ToString(); _returnValue = true; }

                    if (_sqlCommand.Parameters != null)
                    {
                        _sqlCommand.Parameters.Clear();

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _sqlConnection.Close();
                if (_sqlConnection != null) { _sqlConnection.Close(); }
                if (oParameters != null) { oParameters.Dispose(); }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();  
                }

            }

            return _returnValue;
        }


        public static bool ReasonsToStopPaymentClaim(Int64 nBPRId, Int64 nCLPId, string ERAClaimNo, Int64 nContactId)
        {
            {
                //SP: ERA_ReasonsToStopPaymentClaim; 
                // @nBPRId
                // @nCLPId
                // @nClaimNo
                // @nContactId
                // @bIsStopPost (out)

                bool _blnFlag = false;
                int _result = 0;
                SqlConnection _sqlConnection = null;
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                string _sqlQuery = "";
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);    

                try
                {
                    _sqlQuery = "ERA_ReasonsToStopPaymentClaim";
                    oParameters.Clear();
                    oParameters.Add("@nBPRId", nBPRId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nCLPId", nCLPId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sERAClaimNo", ERAClaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nContactId", nContactId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@bIsStopPost", _blnFlag, ParameterDirection.Output, SqlDbType.Bit);

                    _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    //oDB.Connect(false);
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                    {
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandText = _sqlQuery;

                        _result = _sqlCommand.ExecuteNonQuery();

                        if (_sqlCommand.Parameters["@bIsStopPost"].Value.ToString().Trim() != "")  // Display comments to the user.
                        {
                            _blnFlag = bool.Parse(_sqlCommand.Parameters["@bIsStopPost"].Value.ToString());
                        }

                        if (_sqlCommand.Parameters != null)
                        {
                            _sqlCommand.Parameters.Clear();

                        }
                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    _sqlConnection.Close();
                    _sqlConnection.Dispose();
                    if (oParameters != null) { oParameters.Dispose(); }
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                    }
                }

                return _blnFlag;
            }
        }



        public static bool UpdateClaimStatus(Int64 nBPRId, Int64 nCLPID)
        {
            // Stored Procedure : ERA_UpdateCheckStatus
            // Param1 : @nBPRId  Numeric(18,0)
            // Param2 : @nCheckStatus	int

            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            SqlConnection _sqlConnection = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            bool blnFlag = false;
            int _result = 0;

            try
            {
                oParameters.Clear();
                oParameters.Add("@nBPRId", nBPRId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCLPID", nCLPID, ParameterDirection.Input, SqlDbType.BigInt);

                _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                _sqlConnection.Open();
                using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                {
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = "ERA_UpdateClaimStatus";

                    _result = _sqlCommand.ExecuteNonQuery();

                    if (_result > 0)
                        blnFlag = true;

                    if (_sqlCommand.Parameters != null)
                    {
                        _sqlCommand.Parameters.Clear();

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _sqlConnection.Close();
                if (_sqlConnection != null) { _sqlConnection.Close(); }
                if (oParameters != null) { oParameters.Dispose(); }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return blnFlag;

        }
        public static bool UpdateCheckStatus(Int64 nBPRId,Int32 CheckStatus)
        {
            // Stored Procedure : ERA_UpdateCheckStatus
            // Param1 : @nBPRId  Numeric(18,0)
            // Param2 : @nCheckStatus	int

            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            SqlConnection _sqlConnection = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);    

            bool blnFlag = false;
            int _result = 0;

            try
            {
                oParameters.Clear();
                oParameters.Add("@nBPRId", nBPRId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCheckStatus", CheckStatus, ParameterDirection.Input, SqlDbType.Int);

                _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                _sqlConnection.Open();
                using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                {
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = "ERA_UpdateCheckStatus";

                    _result = _sqlCommand.ExecuteNonQuery();

                    if (_result > 0)
                        blnFlag = true;
                    if (_sqlCommand.Parameters != null)
                    {
                        _sqlCommand.Parameters.Clear();

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _sqlConnection.Close();
                if (_sqlConnection != null) { _sqlConnection.Close(); }
                if (oParameters != null) { oParameters.Dispose(); }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return blnFlag;

        }

        public static bool DeleteClaimMatchDetailsonClose(Int64 nOperationID , Int64 nBPRID)
        {
            // Stored Procedure : ERA_UpdateCheckStatus
            // Param1 : @nBPRId  Numeric(18,0)
            // Param2 : @nCheckStatus	int

            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            SqlConnection _sqlConnection = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            bool blnFlag = false;
            int _result = 0;

            try
            {
                oParameters.Clear();
                oParameters.Add("@nOpetationID", nOperationID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
               _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                _sqlConnection.Open();
                using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                {
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = "ERA_DeleteClaimMatchingDetailsonClose";

                    _result = _sqlCommand.ExecuteNonQuery();

                    if (_result > 0)
                        blnFlag = true;
                    if (_sqlCommand.Parameters != null)
                    {
                        _sqlCommand.Parameters.Clear();

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _sqlConnection.Close();
                if (_sqlConnection != null) { _sqlConnection.Close(); }
                if (oParameters != null) { oParameters.Dispose(); }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return blnFlag;

        }

        public static bool IsCheckInProcess(Int64 nBPRId, out string sMessage)
        {

            //SP: ERA_MaintainCheckStatus; @nBPRId		Numeric(18,0)
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            SqlConnection _sqlConnection = null;
            bool blnFlag = false;
            int _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);    

            sMessage = string.Empty;
            try
            {
                oParameters.Clear();
                oParameters.Add("@nBPRId", nBPRId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sMessage", sMessage, ParameterDirection.Output, SqlDbType.VarChar, 255);

                _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                _sqlConnection.Open();
                using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                {
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = "ERA_MaintainCheckStatus";

                    _result = _sqlCommand.ExecuteNonQuery();

                    if (_sqlCommand.Parameters["@sMessage"].Value.ToString().Trim() != "")  // Display comments to the user.
                    { sMessage = _sqlCommand.Parameters["@sMessage"].Value.ToString(); blnFlag = true; }

                    if (_sqlCommand.Parameters != null)
                    {
                        _sqlCommand.Parameters.Clear();

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _sqlConnection.Close();
                if (_sqlConnection != null) { _sqlConnection.Close(); }
                if (oParameters != null) { oParameters.Dispose(); }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return blnFlag;
        }


        public static AdjustmentType GetAdjustmentTypeCode(Int64 nBPRId, string sGroupCode, string sReasonCode)
        {

            if (sGroupCode.Trim().Length == 0 || sReasonCode.Trim().Length == 0) { return AdjustmentType.None; }



            Int64 iAdjustmentType = 0;
            AdjustmentType oTemp = AdjustmentType.OtherAdjustment;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);    

            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            SqlConnection _sqlConnection = null;
            string _sqlQuery = "";
            int _result = 0;
            try
            {
                _sqlQuery = "ERA_GetCASReasonCodes";
                oParameters.Clear();
                oParameters.Add("@nBPRId", nBPRId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sReasonCodes", sGroupCode + sReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@CASType", iAdjustmentType, ParameterDirection.Output, SqlDbType.BigInt);

                _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                _sqlConnection.Open();
                using (SqlCommand _sqlCommand = oDB.GetCmdParameters(oParameters))
                {
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = _sqlQuery;

                    _result = _sqlCommand.ExecuteNonQuery();

                    if (_sqlCommand.Parameters["@CASType"].Value != null)
                    { iAdjustmentType = Convert.ToInt64(_sqlCommand.Parameters["@CASType"].Value); }

                    if (_sqlCommand.Parameters != null)
                    {
                        _sqlCommand.Parameters.Clear();

                    }
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _sqlConnection.Close();
                if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }


            if (AdjustmentType.CoInsurance.GetHashCode() == iAdjustmentType) { oTemp = AdjustmentType.CoInsurance; }
            if (AdjustmentType.Copay.GetHashCode() == iAdjustmentType) { oTemp = AdjustmentType.Copay; }
            if (AdjustmentType.Deductable.GetHashCode() == iAdjustmentType) { oTemp = AdjustmentType.Deductable; }
            if (AdjustmentType.WithHold.GetHashCode() == iAdjustmentType) { oTemp = AdjustmentType.WithHold; }
            if (AdjustmentType.WriteOff.GetHashCode() == iAdjustmentType) { oTemp = AdjustmentType.WriteOff; }
            if (AdjustmentType.OtherAdjustment.GetHashCode() == iAdjustmentType) { oTemp = AdjustmentType.OtherAdjustment; }
            if (AdjustmentType.PrevPaid.GetHashCode() == iAdjustmentType) { oTemp = AdjustmentType.OtherAdjustment; }

            return oTemp;
        }

        #endregion

    }

}
