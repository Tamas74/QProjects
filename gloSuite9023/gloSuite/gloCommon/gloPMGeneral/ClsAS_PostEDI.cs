using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using gloPMGeneral.RTEligibilityService;
using System.Data;

namespace gloPMGeneral
{
   
        public class ClsPostEDI : IDisposable
        {

            Eligibility wsProxy;
            AuthSOAPHeader AuthHdr;
            WSX12EligibilityInquiry X12EIObject;
            WSX12EligibilityResponse X12ERObject;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private string _messageBoxCaption = "";

            public string _FilePath = "";
            public string _databaseConnectionString = "";
            public string PostEDIResult = "";

            public void PostEDI(string PayerId, string DatabaseConnectionString, Int64 PatientID)
            {
                try
                {
                    #region " Retrieve MessageBoxCaption from AppSettings "

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

                    _databaseConnectionString = DatabaseConnectionString;
                    if (System.IO.File.Exists(_FilePath) == true)
                    {
                        string oData = "";
                        StreamReader oStreamReader = new StreamReader(_FilePath);
                        oData = oStreamReader.ReadToEnd().Trim();
                        oStreamReader.Close();

                        if (oData.Length > 0)
                        {

                            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                            wsProxy = new RTEligibilityService.Eligibility();
                            AuthHdr = new AuthSOAPHeader();
                            X12EIObject = new WSX12EligibilityInquiry();

                            //gloPM5040
                            DataTable dtClearinghouse = new DataTable();
                            dtClearinghouse = getClearinghouseDetails();

                            if (dtClearinghouse != null && dtClearinghouse.Rows.Count > 0)
                            {
                                //AuthHdr.User = "V2EL";
                                //AuthHdr.Password = "8evga13m";

                                AuthHdr.User = Convert.ToString(dtClearinghouse.Rows[0]["sUserName"]);

                                gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();
                                string _encryptionKey = "12345678";
                                if (dtClearinghouse.Rows[0]["sPassword"] != null && Convert.ToString(dtClearinghouse.Rows[0]["sPassword"]).Trim() != "")
                                { AuthHdr.Password = oClsEncryption.DecryptFromBase64String(dtClearinghouse.Rows[0]["sPassword"].ToString(), _encryptionKey); }
                                else
                                { AuthHdr.Password = ""; }

                            }

                            wsProxy.AuthSOAPHeaderValue = AuthHdr;
                            X12EIObject.X12Input = oData;
                            X12EIObject.GediPayerID = PayerId;
                            X12EIObject.ResponseDataType = WSResponseDataType.RawPayerData;
                            X12ERObject = new WSX12EligibilityResponse();

                            try
                            {
                                X12ERObject = wsProxy.DoInquiryByX12Data(X12EIObject);
                            }
                            catch (System.Net.WebException ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                return;
                            }

                            string sEdiFile = "271.X12";
                            string sPath = gloSettings.FolderSettings.AppTempFolderPath;  //AppDomain.CurrentDomain.BaseDirectory;
                            if (X12ERObject.SuccessCode == SuccessCode.Success)
                            {
                                if (X12ERObject.OriginalInquiry.ResponseDataType == WSResponseDataType.RawPayerData)
                                {
                                    //MessageBox.Show("Response: " + X12ERObject.ResponseAsRawString,_messageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                                    if (File.Exists(sPath + sEdiFile))
                                    {
                                        File.Delete(sPath + sEdiFile);
                                    }
                                    System.IO.StreamWriter oWriter = new System.IO.StreamWriter(sPath + sEdiFile);
                                    oWriter.Write(X12ERObject.ResponseAsRawString.Trim());
                                    oWriter.Close();
                                    frmEligibilityResponse ofrm = new frmEligibilityResponse(DatabaseConnectionString, PatientID, 0);
                                    ofrm.ShowDialog(ofrm.Parent);
                                    PostEDIResult = ofrm.EDIReturnResult;
                                    ofrm.Dispose();
                                    ofrm = null;
                                }
                                else
                                {
                                    // MessageBox.Show("Response: " + X12ERObject.ResponseAsXml, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else if (X12ERObject.SuccessCode == SuccessCode.PayerEnrollmentRequired)
                            {

                                string[] ErrorMsgs = X12ERObject.ExtraProcessingInfo.AllMessages;
                                System.Text.StringBuilder strErrorMessages = default(System.Text.StringBuilder);
                                strErrorMessages = new System.Text.StringBuilder();

                                for (int i = 0; i <= ErrorMsgs.Length - 1; i++)
                                {
                                    strErrorMessages.Append(ErrorMsgs[i].ToString());
                                }
                                if (strErrorMessages.ToString().Length > 0)
                                {
                                    MessageBox.Show(strErrorMessages.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                            else if (X12ERObject.SuccessCode == SuccessCode.PayerEnrollmentRequired)
                            {
                                //MessageBox.Show("Payer enrollment is required for this service.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                string[] ErrorMsgs = X12ERObject.ExtraProcessingInfo.AllMessages;
                                System.Text.StringBuilder strErrorMessages = default(System.Text.StringBuilder);
                                strErrorMessages = new System.Text.StringBuilder();

                                for (int i = 0; i <= ErrorMsgs.Length - 1; i++)
                                {
                                    strErrorMessages.Append(ErrorMsgs[i].ToString());
                                }
                                if (strErrorMessages.ToString().Length > 0)
                                {
                                    MessageBox.Show(strErrorMessages.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else if (X12ERObject.SuccessCode == SuccessCode.PayerNotSupported)
                            {
                                //MessageBox.Show("Payer is not supported by this service.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                string[] ErrorMsgs = X12ERObject.ExtraProcessingInfo.AllMessages;
                                System.Text.StringBuilder strErrorMessages = default(System.Text.StringBuilder);
                                strErrorMessages = new System.Text.StringBuilder();

                                for (int i = 0; i <= ErrorMsgs.Length - 1; i++)
                                {
                                    strErrorMessages.Append(ErrorMsgs[i].ToString());
                                }
                                if (strErrorMessages.ToString().Length > 0)
                                {
                                    MessageBox.Show(strErrorMessages.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else if (X12ERObject.SuccessCode == SuccessCode.PayerTimeout)
                            {
                                //MessageBox.Show("Payer's Timeout for this service.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                string[] ErrorMsgs = X12ERObject.ExtraProcessingInfo.AllMessages;
                                System.Text.StringBuilder strErrorMessages = default(System.Text.StringBuilder);
                                strErrorMessages = new System.Text.StringBuilder();

                                for (int i = 0; i <= ErrorMsgs.Length - 1; i++)
                                {
                                    strErrorMessages.Append(ErrorMsgs[i].ToString());
                                }
                                if (strErrorMessages.ToString().Length > 0)
                                {
                                    MessageBox.Show(strErrorMessages.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else if (X12ERObject.SuccessCode == SuccessCode.ProductRequired)
                            {
                                //MessageBox.Show("Product Required for the eligibility.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                string[] ErrorMsgs = X12ERObject.ExtraProcessingInfo.AllMessages;
                                System.Text.StringBuilder strErrorMessages = default(System.Text.StringBuilder);
                                strErrorMessages = new System.Text.StringBuilder();

                                for (int i = 0; i <= ErrorMsgs.Length - 1; i++)
                                {
                                    strErrorMessages.Append(ErrorMsgs[i].ToString());
                                }
                                if (strErrorMessages.ToString().Length > 0)
                                {
                                    MessageBox.Show(strErrorMessages.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else if (X12ERObject.SuccessCode == SuccessCode.ProviderEnrollmentRequired)
                            {
                                //MessageBox.Show("Provider Enrollment is required.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                string[] ErrorMsgs = X12ERObject.ExtraProcessingInfo.AllMessages;
                                System.Text.StringBuilder strErrorMessages = default(System.Text.StringBuilder);
                                strErrorMessages = new System.Text.StringBuilder();

                                for (int i = 0; i <= ErrorMsgs.Length - 1; i++)
                                {
                                    strErrorMessages.Append(ErrorMsgs[i].ToString());
                                }
                                if (strErrorMessages.ToString().Length > 0)
                                {
                                    MessageBox.Show(strErrorMessages.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else if (X12ERObject.SuccessCode == SuccessCode.SystemError)
                            {
                                string[] ErrorMsgs = X12ERObject.ExtraProcessingInfo.AllMessages;
                                System.Text.StringBuilder strErrorMessages = default(System.Text.StringBuilder);
                                strErrorMessages = new System.Text.StringBuilder();

                                for (int i = 0; i <= ErrorMsgs.Length - 1; i++)
                                {
                                    strErrorMessages.Append(ErrorMsgs[i].ToString());
                                }
                                if (strErrorMessages.ToString().Length > 0)
                                {
                                    MessageBox.Show(strErrorMessages.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else if (X12ERObject.SuccessCode == SuccessCode.ValidationFailure)
                            {
                                //ValidationFailure[] oValidationFailure = new ValidationFailure();
                                //ValidationFailureCollection oValidationFailures = new ValidationFailureCollection();
                                //oValidationFailures = X12ERObject.ExtraProcessingInfo.Failures;
                                //oValidationFailure = oValidationFailures.Failures[0];
                                string[] ErrorMsgs = X12ERObject.ExtraProcessingInfo.AllMessages;
                                System.Text.StringBuilder strErrorMessages = default(System.Text.StringBuilder);
                                strErrorMessages = new System.Text.StringBuilder();

                                for (int i = 0; i <= ErrorMsgs.Length - 1; i++)
                                {
                                    strErrorMessages.Append(ErrorMsgs[i].ToString());
                                }
                                if (strErrorMessages.ToString().Length > 0)
                                {
                                    MessageBox.Show(strErrorMessages.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("The File Path is invalid", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }

                finally
                {
                    if (wsProxy != null)
                    {
                        wsProxy.Dispose();
                        wsProxy = null;
                    }
                    if (AuthHdr != null)
                    {
                        AuthHdr = null;
                    }
                    if (X12EIObject != null)
                    {
                        X12EIObject = null;
                    }
                    if (X12ERObject != null)
                    {
                        X12ERObject = null;
                    }
                }
            }

            //gloPM5040
            public DataTable getClearinghouseDetails()
            {
                gloDatabaseLayer.DBLayer ODB = null;
                DataTable _dtClearinghouse = null;

                try
                {
                    ODB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                    ODB.Connect(false);
                    _dtClearinghouse = new DataTable();
                    string _sqlquery = "Select sUserName,sPassword from Bl_Clearinghouse_DTL where sURL='ftp.gatewayedi.com'";
                    ODB.Retrive_Query(_sqlquery, out _dtClearinghouse);


                    //gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();
                    //string _encryptionKey = "12345678";
                    //if (oDataTable.Rows[0]["sPassword"] != null && Convert.ToString(oDataTable.Rows[0]["sPassword"]).Trim() != "")
                    //{ Password = oClsEncryption.DecryptFromBase64String(oDataTable.Rows[0]["sPassword"].ToString(), _encryptionKey); }
                    //else
                    //{ Password = ""; }


                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (ODB != null)
                    {
                        ODB.Disconnect();
                        ODB.Dispose();
                    }
                }
                return _dtClearinghouse;
            }


            //Public Function PostDocument() As Byte() 
            // Dim result As System.Net.WebResponse = Nothing 
            // Try 
            // Dim restURL As New System.Text.StringBuilder 
            // Dim restRequest As HttpWebRequest 
            // 'Dim restResponse As HttpWebResponse 
            // ' XmlDocument xDoc = new XmlDocument(); 

            // restURL.AppendFormat("https://testservices.gatewayedi.com/Eligibility/Service.asmx") 
            // restRequest = DirectCast(WebRequest.Create(restURL.ToString()), HttpWebRequest) 

            // ' the key line. This adds the base64-encoded authentication information to the request header 
            // ' restRequest.Headers.Add("Authorization: Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes("glo$treamU$er:m@3r4501g"))) 
            // restRequest.Headers.Add("Authorization: Basic " + "V2EJ:9999") 

            // 'restRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic" & Convert.ToBase64String(Encoding.UTF8.GetBytes("glo$treamU$er:m@3r4501g"))) 

            // restRequest.Method = "POST" 
            // restRequest.ContentType = "text/xml" 

            // Dim oData As String = "" 
            // Dim oStreamReader As StreamReader = New StreamReader(_FilePath) 
            // oData = oStreamReader.ReadToEnd().Trim 
            // oStreamReader.Close() 

            // Dim someBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(oData) 
            // restRequest.ContentLength = someBytes.Length 
            // Dim newStream As System.IO.Stream = restRequest.GetRequestStream() 
            // newStream.Write(someBytes, 0, someBytes.Length) 
            // newStream.Close() 

            // result = restRequest.GetResponse() 
            // Dim oReader As New System.IO.BinaryReader(result.GetResponseStream()) 
            // Dim bytesRead As Byte() = oReader.ReadBytes(result.ContentLength) 
            // Return bytesRead 

            // 'show the output returned from the API...should be XML contract details. 


            // 'TD1.InnerHtml = " Response Stream Reader from SureScripts :" + reader.ReadToEnd() + "" 
            // Catch ex As Exception 
            // 'If error, show in on-screen text box. this.txtReturned.Text = ex.Message; 
            // 'TD1.InnerHtml = " Exception: " + ex.ToString() 
            // Return Nothing 
            // Finally 
            // If result IsNot Nothing Then 
            // result.Close() 
            // End If 
            // End Try 
            //End Function 



            // To detect redundant calls 
            private bool disposedValue = false;

            // IDisposable 
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: free managed resources when explicitly called 
                    }

                    // TODO: free shared unmanaged resources 
                }
                this.disposedValue = true;
            }

            #region " IDisposable Support "
            // This code added by Visual Basic to correctly implement the disposable pattern. 
            public void Dispose()
            {
                // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above. 
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            #endregion

        }
        public class gloEligibilityResponse
        {
            #region "Constructor & Destructor"

            public gloEligibilityResponse()
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

            ~gloEligibilityResponse()
            {
                Dispose(false);
            }

            #endregion

            #region "Private Variables"

            private Int64 nPatientID = 0;
            private string sReferenceID = "";
            private string sPayerName = "";
            private string sPayerID = "";
            private string sPayerContactName = "";
            private string sPayerContactNumber = "";
            private string sReceiverName = "";
            private string sReceiverID = "";
            private string sReceiverAdditionalID = "";
            private string sSubscriberName = "";
            private string sSubscriberID = "";
            private string sSubscriberAdditionalID = "";
            private Int64 nSubscriberDOB = 0;
            private Int64 nEligibilityDate = 0;
            private string sSubscriberGender = "";
            private Int64 nClinicID = 0;
            private DateTime _dtEligibilityCheck;
            private gloEligibilities oEligibilities = null;

            #endregion

            #region "Property Procedures"

            public string ReferenceID
            {
                get { return sReferenceID; }
                set { sReferenceID = value; }
            }
            public Int64 PatientID
            {
                get { return nPatientID; }
                set { nPatientID = value; }
            }
            public Int64 ClinicID
            {
                get { return nClinicID; }
                set { nClinicID = value; }
            }
            public Int64 SubscriberDOB
            {
                get { return nSubscriberDOB; }
                set { nSubscriberDOB = value; }
            }
            public string PayerName
            {
                get { return sPayerName; }
                set { sPayerName = value; }
            }
            public string PayerID
            {
                get { return sPayerID; }
                set { sPayerID = value; }
            }
            public string PayerContactName
            {
                get { return sPayerContactName; }
                set { sPayerContactName = value; }
            }
            public string PayerContactNumber
            {
                get { return sPayerContactNumber; }
                set { sPayerContactNumber = value; }
            }
            public string ReceiverName
            {
                get { return sReceiverName; }
                set { sReceiverName = value; }
            }
            public string ReceiverID
            {
                get { return sReceiverID; }
                set { sReceiverID = value; }
            }
            public string ReceiverAdditionalID
            {
                get { return sReceiverAdditionalID; }
                set { sReceiverAdditionalID = value; }
            }
            public string SubscriberName
            {
                get { return sSubscriberName; }
                set { sSubscriberName = value; }
            }
            public string SubscriberAdditionalID
            {
                get { return sSubscriberAdditionalID; }
                set { sSubscriberAdditionalID = value; }
            }
            public string SubscriberGender
            {
                get { return sSubscriberGender; }
                set { sSubscriberGender = value; }
            }
            public gloEligibilities Eligibilities
            {
                get { return oEligibilities; }
                set { oEligibilities = value; }
            }
            public string SubscriberID
            {
                get { return sSubscriberID; }
                set { sSubscriberID = value; }
            }
            public Int64 EligibilityDate
            {
                get { return nEligibilityDate; }
                set { nEligibilityDate = value; }
            }
            public DateTime dtEligibilityCheck
            {
                get { return _dtEligibilityCheck; }
                set { _dtEligibilityCheck = value; }
            }
            #endregion
        }

        public class gloEligibility
        {
            #region "Constructor & Destructor"

            public gloEligibility()
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

            ~gloEligibility()
            {
                Dispose(false);
            }

            #endregion

            #region "Private Variables"

            private string sBenefitInformation = "";
            private string sCoverageLevel = "";
            private string sServiceType = "";
            private string sTimePeriod = "";
            private string sMessage = "";
            private decimal nEligibilityAmount = 0;
            private bool bIsPlanNetwork = false;

            #endregion

            #region "Property Procedures"

            public Decimal EligibilityAmount
            {
                get { return nEligibilityAmount; }
                set { nEligibilityAmount = value; }
            }
            public string BenefitInformation
            {
                get { return sBenefitInformation; }
                set { sBenefitInformation = value; }
            }
            public string CoverageLevel
            {
                get { return sCoverageLevel; }
                set { sCoverageLevel = value; }
            }
            public string ServiceType
            {
                get { return sServiceType; }
                set { sServiceType = value; }
            }
            public string TimePeriod
            {
                get { return sTimePeriod; }
                set { sTimePeriod = value; }
            }
            public string Message
            {
                get { return sMessage; }
                set { sMessage = value; }
            }
            public bool IsPlanNetwork
            {
                get { return bIsPlanNetwork; }
                set { bIsPlanNetwork = value; }
            }

            #endregion
        }

        public class gloEligibilities
        {
            protected System.Collections.ArrayList _innerlist;

            #region "Constructor & Destructor"

            public gloEligibilities()
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


            ~gloEligibilities()
            {
                Dispose(false);
            }
            #endregion

            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(gloEligibility item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(gloEligibility item)
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

            public gloEligibility this[int index]
            {
                get
                { return (gloEligibility)_innerlist[index]; }
            }

            public bool Contains(gloEligibility item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(gloEligibility item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(gloEligibility[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        public class EligibilityResponse
        {
            #region "Constructor & Destructor"

            public EligibilityResponse(string DatabaseConnectionString)
            {
                _databaseconnectionstring = DatabaseConnectionString;
            }

            private bool disposed = false;
            private string _databaseconnectionstring = "";
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

            ~EligibilityResponse()
            {
                Dispose(false);
            }

            #endregion

            #region " Private and Public Methods "

            public Int64 AddEligibility(gloEligibilityResponse oEligibility)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                Int64 _result = 0;
                object TempID = null;
                try
                {
                    oDB.Connect(false);
                    if (oEligibility.ReceiverAdditionalID == null)
                    {
                        oEligibility.ReceiverAdditionalID = "";
                    }
                    if (oEligibility.SubscriberID == null)
                    { oEligibility.SubscriberID = ""; }
                    if (oEligibility.SubscriberAdditionalID == null)
                    { oEligibility.SubscriberAdditionalID = ""; }
                    object _intresult = 0;
                    //nPatientID, sReferenceID, sPayerName, sPayerID, sPayerContactName, sPayerContactNumber, sReceiverName, sReceiverID, sReceiverAdditionalID,
                    // sSubscriberName, sSubscriberID, sSubscriberAdditionalID, nSubscriberDOB, sSubscriberGender, nEligibilityDate, nClinicID,nEligibilityResponseID
                    oDBParameters.Add("@nPatientID", oEligibility.PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@sReferenceID", oEligibility.ReferenceID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sPayerName", oEligibility.PayerName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sPayerID", oEligibility.PayerID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sPayerContactName", oEligibility.PayerContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sPayerContactNumber", oEligibility.PayerContactNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sReceiverName", oEligibility.ReceiverName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sReceiverID", oEligibility.ReceiverID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sReceiverAdditionalID", oEligibility.ReceiverAdditionalID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sSubscriberName", oEligibility.SubscriberName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sSubscriberID", oEligibility.SubscriberID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sSubscriberAdditionalID", oEligibility.SubscriberAdditionalID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@nSubscriberDOB", oEligibility.SubscriberDOB, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@sSubscriberGender", oEligibility.SubscriberGender, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@nEligibilityDate", oEligibility.EligibilityDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nClinicID", oEligibility.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@dtEligibilityCheck", oEligibility.dtEligibilityCheck, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                    oDBParameters.Add("@nEligibilityResponseID", 0, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);



                    _intresult = oDB.Execute("BL_INSERT_EligibilityResponse_MST", oDBParameters,out TempID);

                    if (oEligibility.Eligibilities.Count > 0)
                    {
                        for (int _index = 0; _index < oEligibility.Eligibilities.Count; _index++)
                        {
                            //nPatientID, sReferenceID, sBenefitInformation, sCoverageLevel, sServiceType, sTimePeriod, nEligibilityAmount, 
                            //bIsPlanNetwork, sMessage, nClinicID, nEligibilityResponseID
                            if (oDBParameters != null)
                            {
                                oDBParameters.Dispose();
                                oDBParameters = null;
                            }
                            oDBParameters = new gloDatabaseLayer.DBParameters();
                            oDBParameters.Add("@nPatientID", oEligibility.PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@sReferenceID", oEligibility.ReferenceID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            oDBParameters.Add("@sBenefitInformation", oEligibility.Eligibilities[_index].BenefitInformation, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            oDBParameters.Add("@sCoverageLevel", oEligibility.Eligibilities[_index].CoverageLevel, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            oDBParameters.Add("@sServiceType", oEligibility.Eligibilities[_index].ServiceType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            oDBParameters.Add("@sTimePeriod", oEligibility.Eligibilities[_index].TimePeriod, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            oDBParameters.Add("@nEligibilityAmount", oEligibility.Eligibilities[_index].EligibilityAmount, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@bIsPlanNetwork", oEligibility.Eligibilities[_index].IsPlanNetwork, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                            oDBParameters.Add("@sMessage", oEligibility.Eligibilities[_index].Message, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            oDBParameters.Add("@nClinicID", oEligibility.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@nEligibilityResponseID", Convert.ToInt64(TempID), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                           
                            _result = oDB.Execute("BL_INSERT_EligibilityResponse_DTL", oDBParameters);
                            if (oDBParameters != null)
                            {
                                oDBParameters.Dispose();
                                oDBParameters = null;
                            }
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
                        oDB.Disconnect();
                        oDB.Dispose();
                    }
                    if (oDBParameters != null)
                    {
                        oDBParameters.Dispose();
                        oDBParameters = null;
                    }
                }
                return _result;
            }

            #endregion " Private and Public Methods "
        }
        public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
        {

            public TrustAllCertificatePolicy()
            {
            }


            public bool CheckValidationResult(System.Net.ServicePoint srvPoint, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Net.WebRequest request, int certificateProblem)
            {
                return true;
            }
            //Public Function CheckValidationResult(ByVal sp As ServicePoint, ByVal cert As X509Certificate, ByVal req As WebRequest, ByVal problem As Integer) As Boolean 
            // Return True 
            //End Function 
        } 
    
}
