using System.IO;
//using System.Transactions;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
//using Microsoft.SharePoint.Client;
using System.Web;
using Microsoft.IdentityModel.Protocols.WSTrust;
using System.ServiceModel;
using System.Security.Principal;
using System.ServiceModel.Channels;
//using Microsoft.SharePoint.Client.Utilities;
using System;
using System.Windows.Forms;
using System.Configuration;
using ClientOmAuth;
using HtmlAgilityPack;

namespace gloCommunity.Classes
{
    public class Authentication
    {
        public string GetSamlToken()
        {

            string ret = string.Empty;

            try
            {
                //Step1. get token from STS (from ADFS, we need to get the SAML token)
                string stsUrl = clsGeneral.gstrADFSServer;
                //Complete ADFS end point where we need to send the request

                string stsResponse = GetResponse(stsUrl);

                //Step 2. generate response to ACS (Pass the Saml Token from ADFS to ACS)
                //Wreply: is the URL which ADFS will redirect with the resulting token
                //"pr=wsfederation&rm=urn%3aglostreamservices.com%3aapps%3aglodemo&ry=&cx=https%3a%2f%2fglostreamservices.com%3a6110%2fglocommunity%2f_layouts%2fAuthenticate.aspx%3fSource%3d%252Fglocommunity"
                //string ACSSiteWctx = "pr=wsfederation&rm=" + ConfigurationManager.AppSettings["urn"] + "%3a" + ConfigurationManager.AppSettings["SpSiteName"] + "%3a" + ConfigurationManager.AppSettings["apps"] + "%3a" + ConfigurationManager.AppSettings["RelamSite"] + "&ry=&cx=" + ConfigurationManager.AppSettings["https"] + "%3a%2f%2f" + ConfigurationManager.AppSettings["SpServerName"] + "%3a" + ConfigurationManager.AppSettings["Portno"] + "%2f" + clsGeneral.gstrSharepointSiteNm + "%2f_layouts%2fAuthenticate.aspx%3fSource%3d%252F" + clsGeneral.gstrSharepointSiteNm + "";

                string[] strArr = clsGeneral.gstrSharepointSrvNm.Split(':');
                //strArr[0] for https,strArr[1] for SpServerName(Address),strArr[2] for port number
                string ACSSiteWctx = "";
                if (strArr.Length > 0)
                {
                    if (clsGeneral.gblnIscommunityStaging == true)
                        ACSSiteWctx = "pr=wsfederation&rm=" + ConfigurationManager.AppSettings["urn"] + "%3a" + ConfigurationManager.AppSettings["SpSiteName"] + "%3a" + ConfigurationManager.AppSettings["apps"] + "%3a" + ConfigurationManager.AppSettings["RelamSite"] + "&ry=&cx=" + strArr[0] + "%3a%2f%2f" + strArr[1].Replace("//", "") + "%3a" + strArr[2] + "%2f" + clsGeneral.gstrSharepointSiteNm + "%2f_layouts%2fAuthenticate.aspx%3fSource%3d%252F" + clsGeneral.gstrSharepointSiteNm + "";
                    else
                        ACSSiteWctx = "pr=wsfederation&rm=" + ConfigurationManager.AppSettings["Productionurn"] + "%3a" + ConfigurationManager.AppSettings["ProductionSpSiteName"] + "%3a" + ConfigurationManager.AppSettings["Productionapps"] + "%3a" + ConfigurationManager.AppSettings["ProductionRelamSite"] + "&ry=&cx=" + strArr[0] + "%3a%2f%2f" + strArr[1].Replace("//", "") + "%3a" + strArr[2] + "%2f" + clsGeneral.gstrSharepointSiteNm + "%2f_layouts%2fAuthenticate.aspx%3fSource%3d%252F" + clsGeneral.gstrSharepointSiteNm + "";
                }
                dynamic ACSSite = new
                {
                    Wctx = ACSSiteWctx,
                    Wreply = clsGeneral.gstrMgmtServiceReply
                };



                string ADFSData = String.Format("wa=wsignin1.0&wctx={0}&wresult={1}", HttpUtility.UrlEncode(ACSSite.Wctx), HttpUtility.UrlEncode(stsResponse));
                HttpWebRequest ACSRequest = HttpWebRequest.Create(ACSSite.Wreply) as HttpWebRequest;
                //Create the next HTTPRequest to pass the Saml Token to the ACS URL
                ACSRequest.Method = "POST";
                ACSRequest.ContentType = "application/x-www-form-urlencoded";
                ACSRequest.AllowAutoRedirect = false;
                Stream newStream = ACSRequest.GetRequestStream();
                byte[] data = Encoding.UTF8.GetBytes(ADFSData);
                //Pass the SAMl details which we received from ADFS after encoding
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                HttpWebResponse acsResponse = ACSRequest.GetResponse() as HttpWebResponse;
                // Response from ACS


                // Get the Saml Details from the Response object (of ACS)

                XmlDocument myXMLDocument = new XmlDocument();
                XmlTextReader myXMLReader = new XmlTextReader(acsResponse.GetResponseStream());
                myXMLDocument.Load(myXMLReader);
                string html = myXMLDocument.InnerXml;
                string escapedBody = ParseHtmlResponse(html);
                escapedBody = escapedBody.Replace("&amp;", "&");
                escapedBody = escapedBody.Replace("&lt;", "<");
                escapedBody = escapedBody.Replace("&gt;", ">");
                escapedBody = escapedBody.Replace("&apos;", "'");
                escapedBody = escapedBody.Replace("&quot;", "\"");


                //Step 3. generate response to Sharepoint (Pass the Saml Token to sharepoint & get FEDAuth cookie in response)
                //Wreply: is the URL which Sharepoint will redirect with the FEDAuth Cookie
                HttpWebRequest sharepointRequest = default(HttpWebRequest);
                dynamic sharepointSite = new
                {
                    Wctx = clsGeneral.gstrSharePointAutthPage,
                    Wreply = clsGeneral.gstrSharePointSiteReply
                };

                string ACSData = String.Format("wa=wsignin1.0&wctx={0}&wresult={1}", HttpUtility.UrlEncode(sharepointSite.Wctx), HttpUtility.UrlEncode(escapedBody));
                sharepointRequest = HttpWebRequest.Create(sharepointSite.Wreply) as HttpWebRequest;
                //Create the next HTTPRequest to pass the Saml Token to the Sharepoint Site
                sharepointRequest.Method = "POST";
                sharepointRequest.ContentType = "application/x-www-form-urlencoded";
                sharepointRequest.CookieContainer = new CookieContainer();
                sharepointRequest.AllowAutoRedirect = false;

                Stream newStream_ = sharepointRequest.GetRequestStream();
                byte[] data_ = Encoding.UTF8.GetBytes(ACSData);
                //Pass the SAMl details which we received from ACS after encoding
                newStream_.Write(data_, 0, data_.Length);
                newStream_.Close();

                HttpWebResponse SharePointResponse = sharepointRequest.GetResponse() as HttpWebResponse;
                //Get FedAuth Cookie
                ret = SharePointResponse.Cookies["FedAuth"].Value;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: " + ex.Message);
                //clsGeneral.UpdateLog("Error  while getting SamlToken: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            return ret;

        }

        private static string ParseHtmlResponse(string html)
        {
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);
            HtmlAgilityPack.HtmlNodeCollection inputs = htmlDoc.DocumentNode.SelectNodes("//input");
            string parseResult = "missing wresult value";
            foreach (HtmlAgilityPack.HtmlNode htmlNode in inputs)
            {
                if (htmlNode.OuterHtml.Contains("wresult"))
                {
                    parseResult = htmlNode.GetAttributeValue("value", "missing wresult value");
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            return parseResult;
        }

        private string GetResponse(string stsUrl)
        {

            RequestSecurityToken rst = new RequestSecurityToken();

            rst.RequestType = WSTrust13Constants.RequestTypes.Issue;

            //'
            //bearer token, no encryption
            rst.AppliesTo = new EndpointAddress(clsGeneral.gstrACSRelyingPartyurl);
            //'("https://glodemo.accesscontrol.windows.net/FederationMetadata/2007-06/FederationMetadata.xml") ''(realm)

            rst.KeyType = WSTrust13Constants.KeyTypes.Bearer;


            WSTrust13RequestSerializer trustSerializer = new WSTrust13RequestSerializer();
            WSHttpBinding binding = new WSHttpBinding();

            binding.Security.Mode = SecurityMode.Transport;

            binding.Security.Message.ClientCredentialType = MessageCredentialType.None;
            binding.Security.Message.EstablishSecurityContext = false;

            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            EndpointAddress address = new EndpointAddress(stsUrl);

            WSTrust13ContractClient trustClient = new ClientOmAuth.WSTrust13ContractClient(binding, address);

            trustClient.ClientCredentials.Windows.AllowNtlm = true;
            trustClient.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;

            trustClient.ClientCredentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;

            System.ServiceModel.Channels.Message response = trustClient.EndIssue(trustClient.BeginIssue(System.ServiceModel.Channels.Message.CreateMessage(MessageVersion.Default, WSTrust13Constants.Actions.Issue, new ClientOmAuth.RequestBodyWriter(trustSerializer, rst)), null, null));
            trustClient.Close();

            XmlDictionaryReader reader = response.GetReaderAtBodyContents();
            return reader.ReadOuterXml();
        }

        public bool GetSucurityToken()
        {
            bool _IsGetSecurityToken = false;
            try
            {
                clsGeneral.SamlToken = GetSamlToken();

                if (!string.IsNullOrEmpty(clsGeneral.SamlToken))
                {
                    FillCookieContainer();
                    _IsGetSecurityToken = true;
                }
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while getting Security Token : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            return _IsGetSecurityToken;
        }

        public void FillCookieContainer()
        {
            if (!string.IsNullOrEmpty(clsGeneral.SamlToken))
            {

                clsGeneral.oCookie = new CookieContainer();
                Cookie samlAuth = AddCookie();
                clsGeneral.oCookie.Add(samlAuth);
            }
        }

        private Cookie AddCookie()
        {
            Cookie samlAuth = new Cookie("FedAuth", clsGeneral.SamlToken);
            samlAuth.Expires = DateTime.Now.AddYears(50);
            //'AddHours(1)
            samlAuth.Path = "/";
            samlAuth.Secure = true;
            samlAuth.HttpOnly = true;
            Uri samlUri = new Uri(clsGeneral.gstrCommunityWebUrl);
            samlAuth.Domain = samlUri.Host;
            return samlAuth;
        }
    }

}
