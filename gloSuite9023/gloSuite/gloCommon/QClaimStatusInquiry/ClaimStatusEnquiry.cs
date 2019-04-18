using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TriArqEDIRealTimeClaimStatus.CSICoreService;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

using System.Configuration;
using System.ServiceModel.Configuration;

namespace ClaimStatusRealTime
{
    public class ClaimStatusEnquiry : IDisposable
    {

        public ClaimStatusEnquiry()
        { }

        public void Dispose()
        {

        }

        public CSIResponse DoRealTimeClaimStatusCheck(CSIRequest requestdata)
        {
            CSIResponse resultResponse = null;

            try
            {
                resultResponse = CoreEnvelopeRealTimeRequest(requestdata);
            }
            catch (Exception ex)
            {
                resultResponse = new CSIResponse(null, ex.Source, ex.Message);
            }

            return resultResponse;
        }

        private CSIResponse CoreEnvelopeRealTimeRequest(CSIRequest requestdata)
        {
            CSIResponse resultResponse = null;

            try
            {
                ClientSection clientSettings = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;

                string address = null;

                foreach (ChannelEndpointElement endpoint in clientSettings.Endpoints)
                {
                    if (endpoint.Name == "CoreConnect")
                    {
                        address = endpoint.Address.ToString();
                        break;
                    }
                }


                CORETransactionsClient csiClient = new CORETransactionsClient("CoreConnect");
                csiClient.ClientCredentials.UserName.UserName = requestdata.Username;
                csiClient.ClientCredentials.UserName.Password = requestdata.Password;

                ServicePointManager.ServerCertificateValidationCallback = SetServerCertificateValidationCallback();

                COREEnvelopeRealTimeRequest csiRequest = new COREEnvelopeRealTimeRequest()
                {
                    PayloadType = requestdata.PayloadType,
                    ProcessingMode = requestdata.ProcessingMode,
                    PayloadID = requestdata.PayloadID,
                    TimeStamp = requestdata.TimeStamp,
                    SenderID = requestdata.SenderID,
                    ReceiverID = requestdata.ReceiverID,
                    CORERuleVersion = requestdata.CORERuleVersion,
                    Payload = requestdata.Request
                };

                csiClient.Open();

                if (csiClient.State == CommunicationState.Opened)
                {
                    COREEnvelopeRealTimeResponse csiResponse = csiClient.RealTimeTransaction(csiRequest);

                    if (csiResponse != null)
                    {
                        resultResponse = new CSIResponse(csiResponse.Payload, csiResponse.ErrorCode, csiResponse.ErrorMessage);
                    }
                }

                if (csiClient.State != CommunicationState.Faulted) { csiClient.Close(); } else { csiClient.Abort(); }

            }
            catch (Exception ex)
            {
                resultResponse = new CSIResponse(null, ex.Source, ex.Message);
            }

            return resultResponse;
        }

        public CSIBatchResponse DoBatchClaimStatusCheck(CSIBatchRequest requestdata)
        {
            CSIBatchResponse resultResponse = null;

            try
            {
                resultResponse = CoreEnvelopeBatchRequest(requestdata);
            }
            catch (Exception ex)
            {
                resultResponse = new CSIBatchResponse(null, ex.Source, ex.Message);
            }

            return resultResponse;
        }

        private CSIBatchResponse CoreEnvelopeBatchRequest(CSIBatchRequest requestdata)
        {
            CSIBatchResponse resultResponse = null;

            try
            {
                ClientSection clientSettings = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;

                string address = null;

                foreach (ChannelEndpointElement endpoint in clientSettings.Endpoints)
                {
                    if (endpoint.Name == "CoreConnect")
                    {
                        address = endpoint.Address.ToString();
                        break;
                    }
                }


                CORETransactionsClient csiClient = new CORETransactionsClient("CoreConnect");
                csiClient.ClientCredentials.UserName.UserName = requestdata.Username;
                csiClient.ClientCredentials.UserName.Password = requestdata.Password;

                ServicePointManager.ServerCertificateValidationCallback = SetServerCertificateValidationCallback();

                COREEnvelopeBatchSubmission csiRequest = new COREEnvelopeBatchSubmission()
                {
                    PayloadType = requestdata.PayloadType,
                    ProcessingMode = requestdata.ProcessingMode,
                    PayloadID = requestdata.PayloadID,
                    TimeStamp = requestdata.TimeStamp,
                    SenderID = requestdata.SenderID,
                    ReceiverID = requestdata.ReceiverID,
                    CORERuleVersion = requestdata.CORERuleVersion,
                    Payload = requestdata.Request
                };

                csiClient.Open();

                if (csiClient.State == CommunicationState.Opened)
                {
                    COREEnvelopeBatchSubmissionResponse csiResponse = csiClient.BatchSubmitTransaction(csiRequest);
                    if (csiResponse != null)
                    {
                        resultResponse = new CSIBatchResponse(csiResponse.Payload, csiResponse.ErrorCode, csiResponse.ErrorMessage);
                    }
                }

                if (csiClient.State != CommunicationState.Faulted) { csiClient.Close(); } else { csiClient.Abort(); }

            }
            catch (Exception ex)
            {
                resultResponse = new CSIBatchResponse(null, ex.Source, ex.Message);
            }

            return resultResponse;
        }

        private static RemoteCertificateValidationCallback SetServerCertificateValidationCallback()
        {
            return (object o, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => true;
        }
    }


    public class CSIRequest
    {
        #region " Variable Declaration "

        private string username = string.Empty;
        public  string Username { get { return username; } }

        private string password = string.Empty;
        public  string Password { get { return password; } }

        private string request = string.Empty;
        public string Request { get { return request; } }

        private string payloadtype = string.Empty;
        public  string PayloadType { get { return payloadtype; } }

        private string processingmode = string.Empty;
        public  string ProcessingMode { get { return processingmode; } }

        private string payloadid = string.Empty;
        public  string PayloadID { get { return payloadid; } }

        private string timestamp = string.Empty;
        public  string TimeStamp { get { return timestamp; } }

        private string senderid = string.Empty;
        public  string SenderID { get { return senderid; } }

        private string receiverid = string.Empty;
        public  string ReceiverID { get { return receiverid; } }

        private string coreruleversion = string.Empty;
        public  string CORERuleVersion { get { return coreruleversion; } }

        private string response = string.Empty;
        public string Response { get { return response; } }

        private string errorcode = string.Empty;
        public  string ErrorCode { get { return errorcode; } }

        private string errormessage = string.Empty;
        public  string ErrorMessage { get { return errormessage; } }

        #endregion " Variable Declaration "

        public CSIRequest(string user, string password, string requestdata)
        {
            try
            {
                this.username = user;
                this.password = password;
                this.request = requestdata;

                if (ValidateParameters() == false) { throw new Exception("Invalid paramerter value"); }

                payloadtype = "X12_276_Request_005010X212";
                processingmode = "Realtime";
                coreruleversion = "2.2.0";

                receiverid = "Trizetto/GatewayEDI";
                senderid = "V2TW";

                payloadid = Convert.ToString(Guid.NewGuid());
                timestamp = "2017-11-15T04:34:17Z";

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidateParameters()
        {
            bool returnResult = true;

            try
            {
                if (this.Username == null && this.Username.Trim().Length <= 0) { returnResult = false; }
                if (this.Password == null && this.Password.Trim().Length <= 0) { returnResult = false; }
                if (this.Request == null && this.Request.Trim().Length <= 0) { returnResult = false; }

            }
            catch (Exception)
            {
                returnResult = false;
            }

            return returnResult;
        }

    }

    public class CSIResponse
    {
        #region " Variable Declaration "

        private string response = string.Empty;
        public string Response { get { return response; } }

        private string errorcode = string.Empty;
        public  string ErrorCode { get { return errorcode; } }

        private string errormessage = string.Empty;
        public  string ErrorMessage { get { return errormessage; } }

        #endregion " Variable Declaration "

        public CSIResponse(string responsemessage, string errorcode, string errormessage)
        {
            try
            {
                this.response = responsemessage;
                this.errorcode = errorcode;
                this.errormessage = errormessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    public class CSIBatchRequest
    {
        #region " Variable Declaration "

        private string username = string.Empty;
        public string Username { get { return username; } }

        private string password = string.Empty;
        public string Password { get { return password; } }

        private byte[] request = null;
        public byte[] Request { get { return request; } }

        private string payloadtype = string.Empty;
        public string PayloadType { get { return payloadtype; } }

        private string processingmode = string.Empty;
        public string ProcessingMode { get { return processingmode; } }

        private string payloadid = string.Empty;
        public string PayloadID { get { return payloadid; } }

        private string timestamp = string.Empty;
        public string TimeStamp { get { return timestamp; } }

        private string senderid = string.Empty;
        public string SenderID { get { return senderid; } }

        private string receiverid = string.Empty;
        public string ReceiverID { get { return receiverid; } }

        private string coreruleversion = string.Empty;
        public string CORERuleVersion { get { return coreruleversion; } }

        private byte[] response = null;
        public byte[] Response { get { return response; } }

        private string errorcode = string.Empty;
        public string ErrorCode { get { return errorcode; } }

        private string errormessage = string.Empty;
        public string ErrorMessage { get { return errormessage; } }

        #endregion " Variable Declaration "

        public CSIBatchRequest(string user, string password, byte[] requestdata)
        {
            try
            {
                this.username = user;
                this.password = password;
                this.request = requestdata;

                if (ValidateParameters() == false) { throw new Exception("Invalid paramerter value"); }

                payloadtype = "X12_276_Request_005010X212";
                processingmode = "Realtime";
                coreruleversion = "2.2.0";

                receiverid = "Trizetto/GatewayEDI";
                senderid = "V2TW";

                payloadid = Convert.ToString(Guid.NewGuid());
                timestamp = "2017-11-15T04:34:17Z";


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidateParameters()
        {
            bool returnResult = true;

            try
            {
                if (this.Username == null && this.Username.Trim().Length <= 0) { returnResult = false; }
                if (this.Password == null && this.Password.Trim().Length <= 0) { returnResult = false; }
                if (this.Request == null ) { returnResult = false; }

            }
            catch (Exception)
            {
                returnResult = false;
            }

            return returnResult;
        }

    }

    public class CSIBatchResponse
    {
        #region " Variable Declaration "

        private byte[] batchresponse = null;
        public byte[] BatchResponse { get { return batchresponse; } }

        private string errorcode = string.Empty;
        public string ErrorCode { get { return errorcode; } }

        private string errormessage = string.Empty;
        public string ErrorMessage { get { return errormessage; } }

        #endregion " Variable Declaration "

        public CSIBatchResponse(byte[] responsemessage, string errorcode, string errormessage)
        {
            try
            {
                this.batchresponse = responsemessage;
                this.errorcode = errorcode;
                this.errormessage = errormessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
