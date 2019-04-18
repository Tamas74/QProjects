using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.ServiceModel;
using System.Net;
using System.Net.Security;
using System.ServiceModel.Configuration;
using TriArqEDIRealTimeClaimStatus.CSICoreService;

namespace TriArqEDIRealTimeClaimStatus
{
    public class clsRealTimeClaimStatus
    {
        private string _DataBaseConnectionString;
        private gloDatabaseLayer.DBLayer oDB;
        private gloDatabaseLayer.DBParameters oDBPara;

        public Tuple<string, string, string, bool> DoRealTimeClaimStatusByRequestString(string EDI276Request, string ConnString, long ContactId = 0, long ClinicId = 0)
        {
            Tuple<string, string, string, bool> ClaimStatus = null;
            bool isEDI277Parsed = false;
            string EDI277Response = null;
            string EDI277ResponseError = null;
            string address = null;
            try
            {
              
                ClientSection clientSettings = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;

                foreach (ChannelEndpointElement endpoint in clientSettings.Endpoints)
                {
                    if (endpoint.Name == "CoreConnect")
                    {
                        address = endpoint.Address.ToString();
                        break;
                    }
                }
                Tuple<string, string> ClearingHouseDetails = GetGatewaySettings(ContactId, ClinicId);
                ClaimStatusRealTime.CSIRequest request = new ClaimStatusRealTime.CSIRequest(ClearingHouseDetails.Item1, ClearingHouseDetails.Item2, EDI276Request);

                ClaimStatusRealTime.ClaimStatusEnquiry claimStatusEnquiry = new ClaimStatusRealTime.ClaimStatusEnquiry();
                ClaimStatusRealTime.CSIResponse response = claimStatusEnquiry.DoRealTimeClaimStatusCheck(request);
                if (response != null)
                {
                    EDI277Response = response.Response;
                    EDI277ResponseError = response.ErrorMessage;
                }

                if (EDI277Response != null)
                {
                    cls_EDI277_ResponseProcessing oResponseProcessing = new cls_EDI277_ResponseProcessing(ConnString);
                    isEDI277Parsed = oResponseProcessing.ParseEDI277String(EDI277Response);
                }
                ClaimStatus = new Tuple<string, string, string, bool>(EDI276Request, EDI277Response, EDI277ResponseError, isEDI277Parsed);

            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ex, false);
            }
            return ClaimStatus;
        }

        public Tuple<string, string, string, string, bool> DoRealTimeClaimStatusByClaim(string ClaimNumber, string ConnString, long ContactId = 0, long ClinicId = 0)
        {
            Tuple<string, string, string, string, bool> ClaimStatus = null;
            Tuple<string, string> ClaimStatusRequest = null;
            bool isEDI277Parsed = false;
            string EDI277Response = null;
            string EDI276RequestFile = null;
            string EDI276Request = null;
            string EDI277ResponseError = null;
            string address = null;

            try
            {
                cls_EDI276_Requestprocessing oRequestProcessing = new cls_EDI276_Requestprocessing(ClaimNumber, ConnString);
                TRIARQClaim oTRIARQClaim = oRequestProcessing.FillTriarqClaimData();
                if (oRequestProcessing.ValidateTriarqClaimData(oTRIARQClaim))
                {
                    ClaimStatusRequest = oRequestProcessing.Generate276RequestString(oTRIARQClaim);
                    EDI276RequestFile = ClaimStatusRequest.Item1;//Claim Status Request File Path 
                    EDI276Request = ClaimStatusRequest.Item2;//Claim Status Request String 

                    ClientSection clientSettings = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;

                    foreach (ChannelEndpointElement endpoint in clientSettings.Endpoints)
                    {
                        if (endpoint.Name == "CoreConnect")
                        {
                            address = endpoint.Address.ToString();
                            break;
                        }
                    }
                    Tuple<string, string> ClearingHouseDetails = GetGatewaySettings(ContactId, ClinicId);
                   // ClaimStatusRealTime.CSIRequest request = new ClaimStatusRealTime.CSIRequest("V2TW", "cwrzo6ae", EDI276Request);
                    ClaimStatusRealTime.CSIRequest request = new ClaimStatusRealTime.CSIRequest(ClearingHouseDetails.Item1, ClearingHouseDetails.Item2, EDI276Request);

                    


                    ClaimStatusRealTime.ClaimStatusEnquiry claimStatusEnquiry = new ClaimStatusRealTime.ClaimStatusEnquiry();
                    ClaimStatusRealTime.CSIResponse response = claimStatusEnquiry.DoRealTimeClaimStatusCheck(request);
                    if (response != null)
                    {
                        EDI277Response = response.Response;
                        EDI277ResponseError = response.ErrorMessage;
                    }

                    if (EDI277Response != null)
                    {
                        cls_EDI277_ResponseProcessing oResponseProcessing = new cls_EDI277_ResponseProcessing(ConnString);
                        isEDI277Parsed = oResponseProcessing.ParseEDI277String(EDI277Response);


                    }
                    ClaimStatus = new Tuple<string, string, string, string, bool>(EDI276RequestFile, EDI276Request, EDI277Response, EDI277ResponseError, isEDI277Parsed);
                }


            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ex, false);
            }
            return ClaimStatus;
        }


        private long GenerateCSIRequest(int RequestType, string ConnString)
        {
            //INUP_CSI_Request
            _DataBaseConnectionString = ConnString;
            long CSIRequestID = 0;
            object _result = null;            
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@CSIRequestId", 0, ParameterDirection.Output, SqlDbType.BigInt);
                    oDBPara.Add("@CSIRequestDateTime", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oDBPara.Add("@LoginSessionId", 1, ParameterDirection.Input, SqlDbType.Decimal);
                    oDBPara.Add("@RequestType", RequestType, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@LoginUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.Decimal);
                    oDBPara.Add("@CreatedOn", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oDB.Execute("INUP_CSI_Request", oDBPara, out _result);
                    if ((_result != null) && (Convert.ToString(_result) != "" ) )
                    {
                        CSIRequestID = Convert.ToInt64(_result);
                    }
                    CloseConnection();
                }                
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ex, false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return CSIRequestID;
        }

        private void SaveCSIRequestDetails(long CSIRequestId, long CSIRequestFileId, string ClaimNumber, long TrnMasterId, string PayerId)
        {
            //INUP_CSI_RequestDetails
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@CSIRequestDetailsId", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@CSIRequestId", CSIRequestId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@CSIRequestFileId", CSIRequestFileId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@ClaimNumber", ClaimNumber, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@TransactionMasterID", TrnMasterId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@PayerId", PayerId, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@LoginUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@CreatedOn", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oDB.Execute("INUP_CSI_RequestDetails", oDBPara);
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ex, false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void SaveCSIREsponseDetails(long CSIResponseId, long CSIResponseFileId, string ClaimNumber, long TrnMasterId, string PayerId)
        {
            //INUP_CSI_ResponseDetails
            object _result = null;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@CSIResponseDetailsId", 0, ParameterDirection.Output, SqlDbType.BigInt);
                    oDBPara.Add("@CSIResponseId", CSIResponseId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@CSIResponseFileId", CSIResponseFileId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@ClaimNumber", ClaimNumber, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@TransactionMasterID", TrnMasterId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@PayerId", PayerId, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@LoginUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@CreatedOn", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oDB.Execute("INUP_CSI_ResponseDetails", oDBPara, out _result);
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ex, false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void SaveCSIStatus(long RequestId, long RequestFileId, long ResponseId, long ResponseFileId,string StatusMessage, string StatusCategoryCode, string StatusCategoryCodeDesc, string StatusCode, string StatusCodeDesc, string ResponseError, string StatusEffectiveDate)
        {
            //INUP_CSI_Status
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = new object();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@ClaimStatusId", 0, ParameterDirection.Output, SqlDbType.BigInt);
                oParameters.Add("@CSIRequestFileId", RequestFileId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@CSIRequestId", RequestId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@CSIResponseFileId", ResponseFileId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@CSIResponseId", ResponseId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@StatusMessage", StatusMessage, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@ClaimStatusCategoryCode", StatusCategoryCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@ClaimStatusCatgoryDescription", StatusCategoryCodeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@ClaimStatusCode", StatusCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@ClaimStatusDescription", StatusCodeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@ClaimStatusError", ResponseError, ParameterDirection.Input, SqlDbType.VarChar);
                if (StatusEffectiveDate == null || StatusEffectiveDate =="")
                {
                    oParameters.Add("@ClaimStatusEffectiveDate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                }
                else
                {
                    oParameters.Add("@ClaimStatusEffectiveDate", StatusEffectiveDate, ParameterDirection.Input, SqlDbType.DateTime);
                }
                oParameters.Add("@ClaimStatusDate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@LoginUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@CreatedOn", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDB.Execute("INUP_CSI_Status", oParameters, out _oResult);
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
        }

        public Tuple<string, string> GetGatewaySettings(long ContactId, long ClinicId)
        {
            Tuple<string, string> ClearingHouse = null;
            try
            {
                DataTable dt;

                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@ContactId", ContactId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@ClinicId", ClinicId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("EDI_GetClearingHouse_CSI", oDBPara, out dt);
                    CloseConnection();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string UserName = Convert.ToString(dt.Rows[0]["sRealTimeClaimUserName"]);
                            clsEncryption oEncypt = new clsEncryption();
                            string Password = oEncypt.DecryptFromBase64String(Convert.ToString(dt.Rows[0]["sRealTimeClaimPassword"]), "12345678");
                            ClearingHouse = new Tuple<string, string>(UserName, Password);
                            oEncypt.Dispose();
                            oEncypt = null;
                            UserName = string.Empty;
                            Password = string.Empty;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GenerateSegmentIDs, ex, ActivityOutCome.Failure);
                return null;
            }
            return ClearingHouse;
        }

        public cls_277CA_RealTimeClaimStatus DoRealTimeCSI(string ClaimNumber, string ConnString, long ContactId = 0, long ClinicId = 0, long TrnMasterId = 0)
        {
            Tuple<string, string,long> ClaimStatusRequest = null;
            string EDI277Response = null;
            string EDI276RequestFile = null;
            string EDI276Request = null;
            string EDI277ResponseError = null;
            long EDI276RequestFileID = 0;
            long CSIRequestID = 0;


            string address = null;

            CSIRequestID = GenerateCSIRequest(Convert.ToInt32(clsGeneral.RequestType.RealTime), ConnString);

            cls_277CA_RealTimeClaimStatus oClaimStatus = new cls_277CA_RealTimeClaimStatus();
            try
            {
                cls_EDI276_Requestprocessing oRequestProcessing = new cls_EDI276_Requestprocessing(ClaimNumber, ConnString);
                TRIARQClaim oTRIARQClaim = oRequestProcessing.FillTriarqClaimData();
                if (oRequestProcessing.ValidateTriarqClaimData(oTRIARQClaim))
                {
                    ClaimStatusRequest = oRequestProcessing.Generate276RealTimeCSIString(oTRIARQClaim,CSIRequestID);
                    EDI276RequestFile = ClaimStatusRequest.Item1;//Claim Status Request File Path 
                    EDI276Request = ClaimStatusRequest.Item2;//Claim Status Request String 
                    EDI276RequestFileID = ClaimStatusRequest.Item3; // Claim Status Request File ID

                    ClientSection clientSettings = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;

                    foreach (ChannelEndpointElement endpoint in clientSettings.Endpoints)
                    {
                        if (endpoint.Name == "CoreConnect")
                        {
                            address = endpoint.Address.ToString();
                            break;
                        }
                    }
                    Tuple<string, string> ClearingHouseDetails = GetGatewaySettings(ContactId ,ClinicId);
                    ClaimStatusRealTime.CSIRequest request = new ClaimStatusRealTime.CSIRequest(ClearingHouseDetails.Item1 , ClearingHouseDetails.Item2, EDI276Request);

                    ClaimStatusRealTime.ClaimStatusEnquiry claimStatusEnquiry = new ClaimStatusRealTime.ClaimStatusEnquiry();
                    ClaimStatusRealTime.CSIResponse response = claimStatusEnquiry.DoRealTimeClaimStatusCheck(request);
                    if (response != null)
                    {
                        EDI277Response = response.Response;
                        EDI277ResponseError = response.ErrorMessage;
                    }

                    if (EDI277Response != null)
                    {
                        cls_EDI277_ResponseProcessing oResponseProcessing = new cls_EDI277_ResponseProcessing(ConnString);
                        oClaimStatus = oResponseProcessing.Parse277RealTimeCSIString(EDI277Response, ClaimNumber,CSIRequestID);
                    }

                    oClaimStatus.RequestId = CSIRequestID;
                    oClaimStatus.RequestFileId = EDI276RequestFileID;
                    oClaimStatus.RequestFilePath = EDI276RequestFile;
                    oClaimStatus.RequestString = EDI276Request;
                    oClaimStatus.ResponseString = EDI277Response;
                    oClaimStatus.ResponseError = EDI277ResponseError;
                   
                    SaveCSIRequestDetails(oClaimStatus.RequestId,oClaimStatus.RequestFileId,ClaimNumber,TrnMasterId,oClaimStatus.PayerId);
                    SaveCSIREsponseDetails(oClaimStatus.ResponseId, oClaimStatus.ResponseFileId, ClaimNumber, TrnMasterId, oClaimStatus.PayerId);
                    SaveCSIStatus(oClaimStatus.RequestId, oClaimStatus.RequestFileId, oClaimStatus.ResponseId, oClaimStatus.ResponseFileId,oClaimStatus.StatusMessge,oClaimStatus.StatusCategoryCode, oClaimStatus.StatusCategoryCodeDesc, oClaimStatus.StatusCode, oClaimStatus.StatusCodeDesc, oClaimStatus.ResponseError, oClaimStatus.StatusEffectiveDate);
                }
             

            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ex, false);
            }
            return oClaimStatus;
        }

        private bool OpenConnection(bool withParameters)
        {
            bool _Result = false;
            try
            {
                if (_DataBaseConnectionString != "")
                {
                    oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                    oDB.Connect(false);
                    if (withParameters)
                        oDBPara = new gloDatabaseLayer.DBParameters();
                    _Result = true;
                }
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI277Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            return _Result;
        }

        private void CloseConnection()
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
  
   
}
