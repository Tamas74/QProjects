using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using gloPatient.RTEligibilityService;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace gloPatient
{
   
        public class ClsPostEDI : IDisposable
        {
            #region " Variable Declarations "

            Eligibility wsProxy;
            AuthSOAPHeader AuthHdr;
            WSX12EligibilityInquiry X12EIObject;
            WSX12EligibilityResponse X12ERObject;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            public string _FilePath = "";
            public string _databaseConnectionString = "";
            public string PostEDIResult = "";
            private string _MessageBoxCaption = "";

            #endregion

            #region " Properties "

            public string EligibilityUserName { get; set; }

            public string EligibilityPassword { get; set; }

            public string EligibilityUrl { get; set; }

            public string SubmitterID { get; set; }

            #endregion

            #region " Method "

            public Boolean PostEDI(string PayerId, string DatabaseConnectionString, Int64 PatientID, Int64 ContactID, bool bIsInsuranceAdd=false)
            {
               Boolean bIsEligibilityCheckSuccess = false;
                try
                {
                    #region " Retrieve MessageBoxCaption from AppSettings "
                    //MessageBoxCaption
                    if (appSettings["MessageBOXCaption"] != null)
                    {
                        if (appSettings["MessageBOXCaption"] != "")
                        {
                            _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                        }
                        else
                        {
                            _MessageBoxCaption = "gloPM";
                        }
                    }
                    else
                    { _MessageBoxCaption = "gloPM"; }

                    #endregion
                    

                    _databaseConnectionString = DatabaseConnectionString;
                    
                    if (System.IO.File.Exists(_FilePath) == true)
                    {
                        string oData = "";
                        StreamReader oStreamReader = new StreamReader(_FilePath);
                        oData = oStreamReader.ReadToEnd().Trim();                   
                        oStreamReader.Close();
                        oStreamReader.Dispose();

                        if (oData.Length > 0)
                        {

                            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                            wsProxy = new RTEligibilityService.Eligibility();
                            AuthHdr = new AuthSOAPHeader();
                            X12EIObject = new WSX12EligibilityInquiry();

                            //gloPM5040
                            //DataTable dtClearinghouse = new DataTable();
                            //dtClearinghouse = getClearinghouseDetails(ContactID);

                            if (Convert.ToString(EligibilityUserName).Trim() != "" && Convert.ToString(EligibilityPassword).Trim() != "" && Convert.ToString(EligibilityUrl).Trim() != "")
                            {

                                AuthHdr.User = Convert.ToString(EligibilityUserName);

                                gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();
                                string _encryptionKey = "12345678";
                                AuthHdr.Password = oClsEncryption.DecryptFromBase64String(Convert.ToString(EligibilityPassword), _encryptionKey);
                                //SLR: Free oClsEncryption
                                if (oClsEncryption != null)
                                {
                                    oClsEncryption.Dispose();                                    
                                }
                            }

                            if (AuthHdr.User == null || Convert.ToString(AuthHdr.User).Trim() == "" || AuthHdr.Password == null || Convert.ToString(AuthHdr.Password).Trim() == "")
                            {
                              return DisplayErrorMessage("Clearinghouse parameters are missing. ",_MessageBoxCaption, bIsInsuranceAdd);
                               //MessageBox.Show("Clearinghouse parameters are missing. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                //return false;
                            }

                            wsProxy.AuthSOAPHeaderValue = AuthHdr;
                            X12EIObject.X12Input = oData;
                            X12EIObject.GediPayerID = PayerId;
                            X12EIObject.ResponseDataType = WSResponseDataType.RawPayerData;
                            X12ERObject = new WSX12EligibilityResponse();


                            try
                            {
                                wsProxy.Url = EligibilityUrl;
                                X12ERObject = wsProxy.DoInquiryByX12Data(X12EIObject);
                            }
                            catch (System.Net.WebException ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                return DisplayErrorMessage("Could not connect to clearing house. ", _MessageBoxCaption, bIsInsuranceAdd);
                               //MessageBox.Show("Could not connect to clearing house. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //return false;
                            }
                            catch (System.UriFormatException ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                return DisplayErrorMessage("Could not connect to clearing house. ", _MessageBoxCaption, bIsInsuranceAdd); 
                               //MessageBox.Show("Could not connect to clearing house. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //return false;
                            }

                            string sEdiFile = "271.X12";
                            string sPath = gloSettings.FolderSettings.AppTempFolderPath; //AppDomain.CurrentDomain.BaseDirectory;
                            if (X12ERObject.SuccessCode == SuccessCode.Success)
                            {
                                if (X12ERObject.OriginalInquiry.ResponseDataType == WSResponseDataType.RawPayerData)
                                {
                                    //MessageBox.Show("Response: " + X12ERObject.ResponseAsRawString,"gloPM",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                    
                                    if (File.Exists(sPath + sEdiFile))
                                    {
                                        File.Delete(sPath + sEdiFile);
                                    }
                                    System.IO.StreamWriter oWriter = new System.IO.StreamWriter(sPath + sEdiFile);
                                    oWriter.Write(X12ERObject.ResponseAsRawString.Trim());
                                    oWriter.Flush();
                                    oWriter.Close();
                                    oWriter.Dispose();

                                    string oVersionData = "";
                                    string sANSIVersion = "";
                                    StreamReader oVersionStreamReader = new StreamReader(sPath + sEdiFile);


                                    oVersionData =  oVersionStreamReader.ReadToEnd().Trim();
                                    sANSIVersion =  CheckFileVersion(oVersionData);

                                    oVersionStreamReader.Close();
                                   
                                    if (oVersionStreamReader != null)
                                    { oVersionStreamReader.Dispose(); }
                                   
                                    if (sANSIVersion == "00401")
                                    {
                                       if (bIsInsuranceAdd == false)
                                       {
                                          frmEligibilityResponse ofrm = new frmEligibilityResponse(DatabaseConnectionString, PatientID, ContactID);
                                          ofrm.ShowDialog(ofrm.Parent);
                                          bIsEligibilityCheckSuccess = true;
                                          PostEDIResult = ofrm.EDIReturnResult;
                                          //SLR: Dispose ofrm
                                          ofrm.Dispose();
                                       }
                                       else
                                       {
                                          frmEligibilityResponse ofrm = new frmEligibilityResponse(DatabaseConnectionString, PatientID, ContactID);
                                          string sEligibilityRejection = ofrm.CheckEligibilityRejections();
                                          if (sEligibilityRejection == "")
                                          {
                                             ofrm.ShowDialog(ofrm.Parent);
                                             bIsEligibilityCheckSuccess = true;
                                          }
                                          else
                                          {
                                             bIsEligibilityCheckSuccess= DisplayErrorMessage(sEligibilityRejection, _MessageBoxCaption, bIsInsuranceAdd);
                                             //MessageBox.Show(sEligibilityRejection, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                          }
                                          PostEDIResult = ofrm.EDIReturnResult;
                                          //SLR: Dispose ofrm
                                          ofrm.Dispose();
                                       }
                                    }
                                    else if (sANSIVersion == "00501")
                                    {
                                       if (bIsInsuranceAdd == false)
                                       {
                                          frmEligibilityResponse_5010 ofrm = new frmEligibilityResponse_5010(DatabaseConnectionString, PatientID, ContactID);
                                          ofrm.ShowDialog(ofrm.Parent);
                                          bIsEligibilityCheckSuccess = true;
                                          PostEDIResult = ofrm.EDIReturnResult;
                                          //SLR: Dispose ofrm
                                          ofrm.Dispose();
                                       }
                                       else
                                       {
                                          frmEligibilityResponse_5010 ofrm = new frmEligibilityResponse_5010(DatabaseConnectionString, PatientID, ContactID);
                                          string sEligibilityRejection = ofrm.CheckEligibilityRejections();
                                          if (sEligibilityRejection == "")
                                          {
                                             ofrm.ShowDialog(ofrm.Parent);
                                             bIsEligibilityCheckSuccess = true;
                                          }
                                          else
                                          {
                                             bIsEligibilityCheckSuccess = DisplayErrorMessage(sEligibilityRejection, _MessageBoxCaption, bIsInsuranceAdd);
                                             //MessageBox.Show(sEligibilityRejection, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                          }
                                          PostEDIResult = ofrm.EDIReturnResult;
                                          //SLR: Dispose ofrm
                                          ofrm.Dispose();
                                       }
                                    }
                                    else
                                    {
                                       return DisplayErrorMessage("Eligibility response could not be processed. Version number is " + sANSIVersion + ".", _MessageBoxCaption, bIsInsuranceAdd);
                                       //MessageBox.Show("Eligibility response could not be processed. Version number is " + sANSIVersion + ".", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    // MessageBox.Show("Response: " + X12ERObject.ResponseAsXml, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                   return DisplayErrorMessage(strErrorMessages.ToString(), _MessageBoxCaption, bIsInsuranceAdd);
                                    //MessageBox.Show(strErrorMessages.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                //SLR: Free strErrorMessages
                                strErrorMessages.Clear();
                                strErrorMessages = null;
                                bIsEligibilityCheckSuccess = false;

                            }
                            else if (X12ERObject.SuccessCode == SuccessCode.PayerEnrollmentRequired)
                            {
                                //MessageBox.Show("Payer enrollment is required for this service.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                string[] ErrorMsgs = X12ERObject.ExtraProcessingInfo.AllMessages;
                                System.Text.StringBuilder strErrorMessages = default(System.Text.StringBuilder);
                                strErrorMessages = new System.Text.StringBuilder();

                                for (int i = 0; i <= ErrorMsgs.Length - 1; i++)
                                {
                                    strErrorMessages.Append(ErrorMsgs[i].ToString());
                                }
                                if (strErrorMessages.ToString().Length > 0)
                                {
                                   return DisplayErrorMessage(strErrorMessages.ToString(), _MessageBoxCaption, bIsInsuranceAdd);
                                   //MessageBox.Show(strErrorMessages.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                //SLR: Free strErrorMessages
                                strErrorMessages.Clear();
                                strErrorMessages = null;
                                bIsEligibilityCheckSuccess = false;
                            }
                            else if (X12ERObject.SuccessCode == SuccessCode.PayerNotSupported)
                            {
                                //MessageBox.Show("Payer is not supported by this service.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                string[] ErrorMsgs = X12ERObject.ExtraProcessingInfo.AllMessages;
                                System.Text.StringBuilder strErrorMessages = default(System.Text.StringBuilder);
                                strErrorMessages = new System.Text.StringBuilder();

                                for (int i = 0; i <= ErrorMsgs.Length - 1; i++)
                                {
                                    strErrorMessages.Append(ErrorMsgs[i].ToString());
                                }
                                if (strErrorMessages.ToString().Length > 0)
                                {
                                   return DisplayErrorMessage(strErrorMessages.ToString(), _MessageBoxCaption, bIsInsuranceAdd);
                                   //MessageBox.Show(strErrorMessages.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                //SLR: Free strErrorMessages
                                strErrorMessages.Clear();
                                strErrorMessages = null;
                                bIsEligibilityCheckSuccess = false;
                            }
                            else if (X12ERObject.SuccessCode == SuccessCode.PayerTimeout)
                            {
                                //MessageBox.Show("Payer's Timeout for this service.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                string[] ErrorMsgs = X12ERObject.ExtraProcessingInfo.AllMessages;
                                System.Text.StringBuilder strErrorMessages = default(System.Text.StringBuilder);
                                strErrorMessages = new System.Text.StringBuilder();

                                for (int i = 0; i <= ErrorMsgs.Length - 1; i++)
                                {
                                    strErrorMessages.Append(ErrorMsgs[i].ToString());
                                }
                                if (strErrorMessages.ToString().Length > 0)
                                {
                                   return DisplayErrorMessage(strErrorMessages.ToString(), _MessageBoxCaption, bIsInsuranceAdd);
                                   //MessageBox.Show(strErrorMessages.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                //SLR: Free strErrorMessages
                                strErrorMessages.Clear();
                                strErrorMessages = null;
                                bIsEligibilityCheckSuccess = false;
                            }
                            else if (X12ERObject.SuccessCode == SuccessCode.ProductRequired)
                            {
                                //MessageBox.Show("Product Required for the eligibility.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                string[] ErrorMsgs = X12ERObject.ExtraProcessingInfo.AllMessages;
                                System.Text.StringBuilder strErrorMessages = default(System.Text.StringBuilder);
                                strErrorMessages = new System.Text.StringBuilder();

                                for (int i = 0; i <= ErrorMsgs.Length - 1; i++)
                                {
                                    strErrorMessages.Append(ErrorMsgs[i].ToString());
                                }
                                if (strErrorMessages.ToString().Length > 0)
                                {
                                   return DisplayErrorMessage(strErrorMessages.ToString(), _MessageBoxCaption, bIsInsuranceAdd);
                                   //MessageBox.Show(strErrorMessages.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                //SLR: Free strErrorMessages
                                strErrorMessages.Clear();
                                strErrorMessages = null;
                                bIsEligibilityCheckSuccess = false;
                            }
                            else if (X12ERObject.SuccessCode == SuccessCode.ProviderEnrollmentRequired)
                            {
                                //MessageBox.Show("Provider Enrollment is required.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                string[] ErrorMsgs = X12ERObject.ExtraProcessingInfo.AllMessages;
                                System.Text.StringBuilder strErrorMessages = default(System.Text.StringBuilder);
                                strErrorMessages = new System.Text.StringBuilder();

                                for (int i = 0; i <= ErrorMsgs.Length - 1; i++)
                                {
                                    strErrorMessages.Append(ErrorMsgs[i].ToString());
                                }
                                if (strErrorMessages.ToString().Length > 0)
                                {
                                   return DisplayErrorMessage(strErrorMessages.ToString(), _MessageBoxCaption, bIsInsuranceAdd);
                                   //MessageBox.Show(strErrorMessages.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                //SLR: Free strErrorMessages
                                strErrorMessages.Clear();
                                strErrorMessages = null;
                                bIsEligibilityCheckSuccess = false;
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
                                   return DisplayErrorMessage(strErrorMessages.ToString(), _MessageBoxCaption, bIsInsuranceAdd);
                                   //MessageBox.Show(strErrorMessages.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                //SLR: Free strErrorMessages
                                strErrorMessages.Clear();
                                strErrorMessages = null;
                                bIsEligibilityCheckSuccess = false;
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
                                   if (strErrorMessages.ToString().Contains("Invalid NPI"))
                                   {
                                      return DisplayErrorMessage("Clearinghouse did not accept the request due to Invalid Primary Eligibility ID.", _MessageBoxCaption, bIsInsuranceAdd);
                                      //MessageBox.Show("Clearinghouse did not accept the request due to Invalid Primary Eligibility ID.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                   }
                                   else
                                   {
                                      return DisplayErrorMessage(strErrorMessages.ToString(), _MessageBoxCaption, bIsInsuranceAdd);
                                      //MessageBox.Show(strErrorMessages.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                   }
                                }
                                else
                                {
                                   return DisplayErrorMessage("Service validation failed.", _MessageBoxCaption, bIsInsuranceAdd);
                                }
                                //SLR: Free strErrorMessages
                                strErrorMessages.Clear();
                                strErrorMessages = null;
                                bIsEligibilityCheckSuccess = false;
                            }
                        }
                    }
                    else
                    {
                       bIsEligibilityCheckSuccess = DisplayErrorMessage("The File Path is invalid", _MessageBoxCaption, bIsInsuranceAdd);
                       //MessageBox.Show("The File Path is invalid", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //bIsEligibilityCheckSuccess = false;
                    }

                }
                catch (Exception ex)
                {
                   bIsEligibilityCheckSuccess = false;
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
                return bIsEligibilityCheckSuccess;
            }

            private bool DisplayErrorMessage(string ErrorMessage,string MessageCaption, bool bIsInsuranceAdd)
            {
               bool bResult = false;
               DialogResult digResult = DialogResult.None;
               
               if (bIsInsuranceAdd)
               {
                  string sMessage = string.Format("Unable to perform insurance eligibility check due to following reason:\n\n{0}\n\nContinue save insurance?", ErrorMessage);
                  digResult = MessageBox.Show(sMessage, MessageCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
               }
               else
               {
                  digResult= MessageBox.Show(ErrorMessage, MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
               if (digResult == DialogResult.OK || digResult == DialogResult.Yes)
               {
                  bResult = true;
               }
               else
               {
                  bResult = false;
               }
               return bResult;
            }

            //gloPM5040
            public DataTable getClearinghouseDetails(Int64 ContactID)
            {
                gloDatabaseLayer.DBLayer oDB = null;
                DataTable dtClearingHouse = null;
                try
                {                    
                    dtClearingHouse = new DataTable();
                    oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                    gloDatabaseLayer.DBParameters oParameters = null;
                    oDB.Connect(false);
                    oParameters = new gloDatabaseLayer.DBParameters();
                    oParameters.Add("@nContactId", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicId", gloSettings.AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Retrive("EDI_GetClearingHouse", oParameters, out dtClearingHouse);
                    oDB.Disconnect();
                    //SLR: Free oParameters
                    oParameters.Dispose();
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return dtClearingHouse;
            }

            public string CheckFileVersion(string strbData)
            {
                string _result = String.Empty;
         
                try
                {
                    char[] arr = { '*' };
                    string[] Array = Convert.ToString(strbData).Replace("\n", "").Split(arr, StringSplitOptions.RemoveEmptyEntries);
                    if (Array.Length > 0)
                    {
                       if(Array[0].Contains("ISA"))
                       {
                            _result = Array[12].ToString();
                       }

                    }
                    //sEdiFile.Split("*",StringSplitOptions.RemoveEmptyEntries)
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                return _result;
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


            #endregion

            #region " IDisposable Support "

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

            // This code added by Visual Basic to correctly implement the disposable pattern. 
            public void Dispose()
            {
                // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above. 
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            #endregion

        }


        public class ClsPostEDIRealMed : IDisposable
        {
            #region " Variable Declarations "

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            public string _FilePath = "";
            public string _databaseConnectionString = "";
            public string PostEDIResult = "";
            private string _MessageBoxCaption = "";

            #endregion

            #region " Properties "

            public string EligibilityUserName { get; set; }

            public string EligibilityPassword { get; set; }

            public string EligibilityUrl { get; set; }

            public string SubmitterID { get; set; }

            #endregion

            #region " Methods "

            public Boolean PostEDI(string PayerId, string DatabaseConnectionString, Int64 PatientID, Int64 ContactID, bool bIsInsuranceAdd = false)
            {
                wsElig270.RequestHeader request = null;
                wsElig270.ResponseHeader response = null;
                wsElig270.wsElig270 elig = null;
                Boolean bIsEligibilityCheckSuccess = false;
                try
                {
                    #region " Retrieve MessageBoxCaption from AppSettings "

                    if (appSettings["MessageBOXCaption"] != null)
                    {
                        if (appSettings["MessageBOXCaption"] != "")
                        {
                            _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                        }
                        else
                        {
                            _MessageBoxCaption = "gloPM";
                        }
                    }
                    else
                    { _MessageBoxCaption = "gloPM"; }

                    #endregion

                    _databaseConnectionString = DatabaseConnectionString;                   

                    if (System.IO.File.Exists(_FilePath) == true)
                    {
                        string oData = "";
                        StreamReader oStreamReader = new StreamReader(_FilePath);
                        oData = oStreamReader.ReadToEnd().Trim();
                        oStreamReader.Close();
                        oStreamReader.Dispose();

                        if (oData.Length > 0)
                        {

                            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

                            //DataTable dtClearinghouse = new DataTable();
                            //dtClearinghouse = getClearinghouseDetails(ContactID);

                            if (Convert.ToString(EligibilityUserName).Trim() != "" && Convert.ToString(EligibilityPassword).Trim() != "" && Convert.ToString(EligibilityUrl).Trim() != "")
                            {

                                request = new wsElig270.RequestHeader();

                                //USERID
                                request.SubmitterID = SubmitterID;
                                //USERNAME
                                request.UserName = Convert.ToString(EligibilityUserName);
                                //PASSWORD
                                gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();
                                string _encryptionKey = "12345678";
                                request.Password = oClsEncryption.DecryptFromBase64String(Convert.ToString(EligibilityPassword), _encryptionKey);
                                //SLR: FRee oclsEncryption
                                if (oClsEncryption != null)
                                {
                                    oClsEncryption.Dispose();
                                }

                                if (request.UserName == null || Convert.ToString(request.UserName).Trim() == "" || request.Password == null || Convert.ToString(request.Password).Trim() == "")
                                {
                                   return DisplayErrorMessage("Clearinghouse parameters are missing. ", _MessageBoxCaption, bIsInsuranceAdd);
                                   //MessageBox.Show("Clearinghouse parameters are missing. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                   //return false;
                                }


                                request.ExternalPayerID = PayerId;
                                //string _flePath = @"E:\\TFS Working folder on glosvr04\\glo6011\\gloSuite\\gloPM\\gloPM\\bin\\Debug\\270OUTPUT.x12";
                                //string strAnsi270Request = System.IO.File.ReadAllText(_flePath);
                                request.AnsiRequest = oData;//strAnsi270Request;
                                request.UserDefined1 = "";
                                request.UserDefined2 = "";
                                request.TimeStamp = System.DateTime.Now.ToString("MM/dd/yyyy");

                                try
                                {
                                    elig = new wsElig270.wsElig270();
                                    elig.Url = EligibilityUrl;
                                    response = elig.ProcessEligibility(request);
                                }
                                catch (System.UriFormatException ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    return DisplayErrorMessage("Could not connect to clearing house. ", _MessageBoxCaption, bIsInsuranceAdd);
                                    //MessageBox.Show("Could not connect to clearing house. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //return false;
                                }

                                catch (System.Net.WebException ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    return DisplayErrorMessage("Could not connect to clearing house. ", _MessageBoxCaption, bIsInsuranceAdd);
                                   //MessageBox.Show("Could not connect to clearing house. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //return false;
                                }
                                

                                string sEdiFile = "271.X12";

                                string sPath = gloSettings.FolderSettings.AppTempFolderPath; //AppDomain.CurrentDomain.BaseDirectory;
                                if (response.AnsiResponse != null)
                                {
                                    StringBuilder sbr = new StringBuilder();
                                    sbr.Append(Convert.ToString(response.AnsiResponse.Trim()));
                                    string _result = CheckFileType(sbr);
                                    sbr = null;

                                    if (_result.Trim() != "271" || (_result.Trim() == "997" || _result.Trim() == "999" || _result.Trim() == "TA1"))
                                    {
                                        sEdiFile = _result + ".X12";

                                        bIsEligibilityCheckSuccess = DisplayErrorMessage("The eligibility response is not available because there is a problem with the request or the payer is unable to respond at this time.  ", _MessageBoxCaption, bIsInsuranceAdd);
                                        //MessageBox.Show("The eligibility response is not available because there is a problem with the request or the payer is unable to respond at this time.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        if (File.Exists(sPath + sEdiFile))
                                        {
                                            File.Delete(sPath + sEdiFile);
                                        }
                                        System.IO.StreamWriter oWriter = new System.IO.StreamWriter(sPath + sEdiFile);
                                        oWriter.Write(Convert.ToString(response.AnsiResponse.Trim()));
                                        oWriter.Flush();
                                        oWriter.Close();
                                        oWriter.Dispose();
                                        bIsEligibilityCheckSuccess = false;
                                    }
                                    else
                                    {
                                        if (File.Exists(sPath + sEdiFile))
                                        {
                                            File.Delete(sPath + sEdiFile);
                                        }

                                        System.IO.StreamWriter oWriter = new System.IO.StreamWriter(sPath + sEdiFile);
                                        oWriter.Write(Convert.ToString(response.AnsiResponse.Trim()));
                                        oWriter.Flush();
                                        oWriter.Close();
                                        oWriter.Dispose();

                                        string oVersionData = "";
                                        string sANSIVersion = "";
                                        StreamReader oVersionStreamReader = new StreamReader(sPath + sEdiFile);                                        
                                        oVersionData = oVersionStreamReader.ReadToEnd().Trim();

                                        oVersionStreamReader.Close();
                                        oVersionStreamReader.Dispose();
                                        

                                        sANSIVersion = CheckFileVersion(oVersionData);

                                        //if (sANSIVersion == "00401")
                                        //{
                                        //    frmEligibilityResponse ofrm = new frmEligibilityResponse(DatabaseConnectionString, PatientID, ContactID);
                                        //    ofrm.ShowDialog(ofrm.Parent);
                                        //    PostEDIResult = ofrm.EDIReturnResult;
                                        //    //SLR: Free ofrm
                                        //    ofrm.Dispose();
                                        //}
                                        //else if (sANSIVersion == "00501")
                                        //{
                                        //    frmEligibilityResponse_5010 ofrm = new frmEligibilityResponse_5010(DatabaseConnectionString, PatientID, ContactID);
                                        //    ofrm.ShowDialog(ofrm.Parent);
                                        //    PostEDIResult = ofrm.EDIReturnResult;
                                        //    //SLR: Free ofrm
                                        //    ofrm.Dispose();
                                        //}
                                        //else
                                        //{
                                        //    MessageBox.Show("Eligibility response could not be processed. Version number is " + sANSIVersion + ".", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //}
                                        if (sANSIVersion == "00401")
                                        {
                                           if (bIsInsuranceAdd == false)
                                           {
                                              frmEligibilityResponse ofrm = new frmEligibilityResponse(DatabaseConnectionString, PatientID, ContactID);
                                              ofrm.ShowDialog(ofrm.Parent);
                                              bIsEligibilityCheckSuccess = true;
                                              PostEDIResult = ofrm.EDIReturnResult;
                                              //SLR: Dispose ofrm
                                              ofrm.Dispose();
                                           }
                                           else
                                           {
                                              frmEligibilityResponse ofrm = new frmEligibilityResponse(DatabaseConnectionString, PatientID, ContactID);
                                              string sEligibilityRejection = ofrm.CheckEligibilityRejections();
                                              if (sEligibilityRejection == "")
                                              {
                                                 ofrm.ShowDialog(ofrm.Parent);
                                                 bIsEligibilityCheckSuccess = true;
                                              }
                                              else
                                              {
                                                 bIsEligibilityCheckSuccess = DisplayErrorMessage(sEligibilityRejection, _MessageBoxCaption, bIsInsuranceAdd);
                                                 //MessageBox.Show(sEligibilityRejection, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                              }
                                              PostEDIResult = ofrm.EDIReturnResult;
                                              //SLR: Dispose ofrm
                                              ofrm.Dispose();
                                           }
                                        }
                                        else if (sANSIVersion == "00501")
                                        {
                                           if (bIsInsuranceAdd == false)
                                           {
                                              frmEligibilityResponse_5010 ofrm = new frmEligibilityResponse_5010(DatabaseConnectionString, PatientID, ContactID);
                                              ofrm.ShowDialog(ofrm.Parent);
                                              bIsEligibilityCheckSuccess = true;
                                              PostEDIResult = ofrm.EDIReturnResult;
                                              //SLR: Dispose ofrm
                                              ofrm.Dispose();
                                           }
                                           else
                                           {
                                              frmEligibilityResponse_5010 ofrm = new frmEligibilityResponse_5010(DatabaseConnectionString, PatientID, ContactID);
                                              string sEligibilityRejection = ofrm.CheckEligibilityRejections();
                                              if (sEligibilityRejection == "")
                                              {
                                                 ofrm.ShowDialog(ofrm.Parent);
                                                 bIsEligibilityCheckSuccess = true;
                                              }
                                              else
                                              {
                                                 bIsEligibilityCheckSuccess = DisplayErrorMessage(sEligibilityRejection, _MessageBoxCaption, bIsInsuranceAdd);
                                                 //MessageBox.Show(sEligibilityRejection, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                              }
                                              PostEDIResult = ofrm.EDIReturnResult;
                                              //SLR: Dispose ofrm
                                              ofrm.Dispose();
                                           }
                                        }
                                        else
                                        {
                                           bIsEligibilityCheckSuccess = DisplayErrorMessage("Eligibility response could not be processed. Version number is " + sANSIVersion + ".", _MessageBoxCaption, bIsInsuranceAdd);
                                           //MessageBox.Show("Eligibility response could not be processed. Version number is " + sANSIVersion + ".", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        //frmEligibilityResponse ofrm = new frmEligibilityResponse(DatabaseConnectionString, PatientID, InsuranceID);
                                        //ofrm.ShowDialog();
                                        //PostEDIResult = ofrm.EDIReturnResult;
                                        //bIsEligibilityCheckSuccess = true;
                                    }

                                }
                                else
                                {
                                   bIsEligibilityCheckSuccess = DisplayErrorMessage("The eligibility response is not available because there is a problem with the request or the payer is unable to respond at this time.  ", _MessageBoxCaption, bIsInsuranceAdd);
                                   //MessageBox.Show("The eligibility response is not available because there is a problem with the request or the payer is unable to respond at this time.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                            else
                            {
                               bIsEligibilityCheckSuccess = DisplayErrorMessage("Clearinghouse parameters are missing. ", _MessageBoxCaption, bIsInsuranceAdd);
                               //MessageBox.Show("Clearinghouse parameters are missing. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    
                            }
                        }
                    }
                    else
                    {
                       bIsEligibilityCheckSuccess = DisplayErrorMessage("The File Path is invalid", _MessageBoxCaption, bIsInsuranceAdd);
                       //MessageBox.Show("The File Path is invalid", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception ex)
                {
                   bIsEligibilityCheckSuccess = false;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }

                finally
                {
                    if (elig != null)
                    {
                        elig = null;
                    }
                    if (request != null)
                    {
                        request = null;
                    }
                    if (response != null)
                    {
                        response = null;
                    }
                }
                return bIsEligibilityCheckSuccess;
            }

            private bool DisplayErrorMessage(string ErrorMessage, string MessageCaption, bool bIsInsuranceAdd)
            {
               bool bResult = false;
               DialogResult digResult = DialogResult.None;

               if (bIsInsuranceAdd)
               {
                  string sMessage = string.Format("Unable to perform insurance eligibility check due to following reason:\n\n{0}\n\nContinue save insurance?", ErrorMessage);
                  digResult = MessageBox.Show(sMessage, MessageCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
               }
               else
               {
                  digResult = MessageBox.Show(ErrorMessage, MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
               if (digResult == DialogResult.OK || digResult == DialogResult.Yes)
               {
                  bResult = true;
               }
               else
               {
                  bResult = false;
               }
               return bResult;
            }
            public bool CertificateValidationCallBack(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            }

            public DataTable getClearinghouseDetails(Int64 ContactID)
            {
                gloDatabaseLayer.DBLayer oDB = null;
                DataTable dtClearingHouse = null;
                try
                {
                    dtClearingHouse = new DataTable();
                    oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                    gloDatabaseLayer.DBParameters oParameters = null;
                    oDB.Connect(false);
                    oParameters = new gloDatabaseLayer.DBParameters();
                    oParameters.Add("@nContactId", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicId", gloSettings.AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Retrive("EDI_GetClearingHouse", oParameters, out dtClearingHouse);
                    oDB.Disconnect();
                    //SLR: Finaly free oParameters
                    oParameters.Dispose();
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return dtClearingHouse;
            }

             public string CheckFileType(StringBuilder strbData)
            {
                string _result = String.Empty;
                try
                {
                    char[] arr={'*'};
                    string[] Array = Convert.ToString(strbData).Replace("\n", "").Split(arr, StringSplitOptions.RemoveEmptyEntries);
                    if(Array.Length>0)
                    {
                        for (int _Count = 0; _Count < Array.Length; _Count++)
                        {
                            if (Convert.ToString(Array[_Count]).Contains("~ST"))
                            {
                                _result = Convert.ToString(Array[_Count+1]);
                                break;
                            }
                            else if (Convert.ToString(Array[_Count]).Contains("TA1"))
                            {
                                _result = "TA1";
                                break;
                            }
                        }

                    }
                            //sEdiFile.Split("*",StringSplitOptions.RemoveEmptyEntries)
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                return _result;
            }

             public string CheckFileVersion(string strbData)
             {
                 string _result = String.Empty;
                 string[] Array=null;
                 try
                 {
                     char[] arr = { '*' };
                     Array = Convert.ToString(strbData).Replace("\n", "").Split(arr, StringSplitOptions.RemoveEmptyEntries);
                     if (Array.Length > 0)
                     {
                         if (Array[0].Contains("ISA"))
                         {
                             _result = Array[12].ToString();
                         }
                     }
                     //sEdiFile.Split("*",StringSplitOptions.RemoveEmptyEntries)
                 }
                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                 }
                 finally 
                 {
                     if (Array != null)
                     { Array = null; }
                 }
                 return _result;
             }

            #endregion

            #region " IDisposable Support "

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

           
            // This code added by Visual Basic to correctly implement the disposable pattern. 
            public void Dispose()
            {
                // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above. 
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

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

            private bool _IsPayerRejected = false;

            private string sPayerURL = "";

            private String _PayerRejectionReason = "";

            private String _FollowUp = "";

            private bool _IsReceiverRejected = false;


            private String _ReceiverRejectionReason = "";


            private String _ReceiverFollowUp = "";

            private String _SubscriberRejectionReason = "";

            private String _SubscriberFollowUp = ""; // values shld be "C"

            private string sInsuranceTypeDescription;
            private string sInsuranceTypeCode;
            private string sErrorNote = "";

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

            public bool IsPayerRejected
            {
                get { return _IsPayerRejected; }
                set { _IsPayerRejected = value; }
            }


            public String PayerRejectionReason
            {
                get { return _PayerRejectionReason; }
                set { _PayerRejectionReason = value; }
            }

            public String FollowUp
            {
                get { return _FollowUp; }
                set { _FollowUp = value; }
            }

            public bool IsReceiverRejected
            {
                get { return _IsReceiverRejected; }
                set { _IsReceiverRejected = value; }
            }

            public String ReceiverRejectionReason
            {
                get { return _ReceiverRejectionReason; }
                set { _ReceiverRejectionReason = value; }
            }

            public String ReceiverFollowUp
            {
                get { return _ReceiverFollowUp; }
                set { _ReceiverFollowUp = value; }
            }




            public bool IsSubscriberRejected
            {
                get { return IsSubscriberRejected; }
                set { IsSubscriberRejected = value; }
            }


            public String SubscriberRejectionReason
            {
                get { return _SubscriberRejectionReason; }
                set { _SubscriberRejectionReason = value; }
            }

            public String SubscriberFollowUp // values shld be "C"
            {
                get { return _SubscriberFollowUp; }
                set { _SubscriberFollowUp = value; }
            }

            public String PayerURL 
            {
                get { return sPayerURL; }
                set { sPayerURL = value; }
            }

            public string InsuranceTypeCode
            {
                get { return sInsuranceTypeCode; }
                set { sInsuranceTypeCode = value; }
            }

            public string InsuranceTypeDescription
            {
                get { return sInsuranceTypeDescription; }
                set { sInsuranceTypeDescription = value; }
            }
            public string ErrorNote
            {
                get { return sErrorNote; }
                set { sErrorNote = value; }
            }
            public Int64 ContactID
            {
                get;
                set;
            }

            public int ANSIVersion
            {
                get;
                set;
            }

            public Int64 BatchPatientID
            {
                get;
                set;
            }

            public string PrimaryCarePhysicainName
            {
                get;
                set;
            }

            public string PrimaryCareAddress
            {
                get;
                set;
            }

            public string PrimaryCareCity
            {
                get;
                set;
            }

            public string PrimaryCareState
            {
                get;
                set;
            }

            public string PrimaryCareZip
            {
                get;
                set;
            }

            public string PrimaryCarePhysicainContactName
            {
                get;
                set;
            }

            public string PrimaryCarePhysicainContactNumber
            {
                get;
                set;
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
            private bool? bIsPlanNetwork = null;
            private bool? bIsAuthRequire = null;
            private string sSubscriberDate = "";
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
            public bool ? IsPlanNetwork
            {
                get { return bIsPlanNetwork; }
                set { bIsPlanNetwork = value; }
            }

            public bool ? IsAuthRequire
            {
                get { return bIsAuthRequire; }
                set { bIsAuthRequire = value; }
            }

            public string SubscriberDate
            {
                get { return sSubscriberDate; }
                set { sSubscriberDate = value; }
            }

            public string EligibilityAmountFormatted
            {
                get ; 
                set ; 
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
                        //SLR: FRee innerlist
                        if (_innerlist != null)
                        {
                            _innerlist.Clear();
                            _innerlist = null;
                        }
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
                    oDBParameters.Add("@sPayerURL", oEligibility.PayerURL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@nContactID", oEligibility.ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@sInsuranceType", oEligibility.InsuranceTypeCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sInsuranceTypeDesc", oEligibility.InsuranceTypeDescription, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@nAnsiVersion", oEligibility.ANSIVersion.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    oDBParameters.Add("@sPrimaryCarePhysicainName", oEligibility.PrimaryCarePhysicainName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sPrimaryCareAddress", oEligibility.PrimaryCareAddress, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sPrimaryCareCity", oEligibility.PrimaryCareCity, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sPrimaryCareState", oEligibility.PrimaryCareState, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sPrimaryCareZip", oEligibility.PrimaryCareZip, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sPrimaryCarePhysicainContactName", oEligibility.PrimaryCarePhysicainContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sErrorNote", oEligibility.ErrorNote, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);


                    _intresult = oDB.Execute("BL_INSERT_EligibilityResponse_MST", oDBParameters, out TempID);

                    //SLR: FRee odbParameters
                    if (oDBParameters != null)
                    {
                        oDBParameters.Dispose();
                    }
                    if (oEligibility.Eligibilities.Count > 0)
                    {
                        for (int _index = 0; _index < oEligibility.Eligibilities.Count; _index++)
                        {
                            //nPatientID, sReferenceID, sBenefitInformation, sCoverageLevel, sServiceType, sTimePeriod, nEligibilityAmount, 
                            //bIsPlanNetwork, sMessage, nClinicID,nEligibilityResponseID
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
                            oDBParameters.Add("@bIsAuthRequire",oEligibility.Eligibilities[_index].IsAuthRequire,System.Data.ParameterDirection.Input,System.Data.SqlDbType.Bit);
                            oDBParameters.Add("@sSubscriberDate", oEligibility.Eligibilities[_index].SubscriberDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            oDBParameters.Add("@sEligibilityAmountFormatted", oEligibility.Eligibilities[_index].EligibilityAmountFormatted, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            _result = oDB.Execute("BL_INSERT_EligibilityResponse_DTL", oDBParameters);
                            //SLR: FRee odbParameters
                            if (oDBParameters != null)
                            {
                                oDBParameters.Dispose();
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
                    }
                }
                return _result;
            }

            #endregion " Private and Public Methods "
        }
}
