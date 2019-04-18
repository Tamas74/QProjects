using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Linq;

namespace gloCentralizedDIB
{
    public class CentralizedDIBException:Exception
    {
        public CentralizedDIBException(string Message) : base(Message) { }
    }

    public static class InitialiseServiceVars
    {
        // Code added to declare gloMedispan REST service variables - for Centralized DB implementation by Ujwala - as on 30092014 - Start
        
        public static string gstrDIBServiceURL = string.Empty ;
        public static bool gblnUseDIBService =false ;

        // Code added to declare gloMedispan REST service variables - for Centralized DB implementation by Ujwala - as on 30092014 - End
    }

    public class FormularyDIBArgumentPacker
    {
        public static string GetJsonString(object objectToSerialize)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string sReturned = serializer.Serialize(objectToSerialize);
                serializer = null;

                return sReturned;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }

    public class DIBJsonConverter<t>
    {
        public t GetConvertedResults(string jsonString)
        {
            JavaScriptSerializer Desrializer = new JavaScriptSerializer();
            Desrializer.MaxJsonLength = Int32.MaxValue;
            
            return Desrializer.Deserialize<t>(jsonString);
        }

    }

    public class FormularyDIBService
    {
        public FormularyDIBService()
        {
           
        }

        public string GetResponseString(string jsonRequestString, URL url)
        {
            try
            {
                string sURL = InitialiseServiceVars.gstrDIBServiceURL.ToString() + url.ToString();
                byte[] bytes = Encoding.UTF8.GetBytes(jsonRequestString);
                string jsonResponseString = string.Empty;

                System.Net.HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);

                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(bytes, 0, bytes.Length);
                    postStream.Close();
                }
                
                jsonResponseString = this.GetResponse(request);                
                return jsonResponseString;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }      

        private string GetResponse(HttpWebRequest Request)
        {
            string sReturned = "";
            try 
            { 
                using (HttpWebResponse response =(HttpWebResponse)Request.GetResponse()) 
                {  
                    using (StreamReader reader = new StreamReader(response.GetResponseStream())) 
                        
                    {   
                        sReturned = reader.ReadToEnd();   
                        reader.Close();  
                    }  
                    response.Close(); 
                }
            } 
            catch (Exception ex)
            { throw ex; } 
            return sReturned;
        }
       
    }

    public class CentralizedDIBStringCaller : IDisposable
    {
        private FormularyDIBService service;

       

        public CentralizedDIBStringCaller()
        {
            this.service = new FormularyDIBService();
        }

        public string GetServiceResponse(object argument, URL url)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sSerialized = string.Empty;
            string sResponse = string.Empty;            
            try
            {
                sSerialized = serializer.Serialize(argument);
                sResponse = this.service.GetResponseString(sSerialized, url);
                return sResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                serializer = null;
                sSerialized = string.Empty;
                //sResponse = string.Empty;
            }
        }

        public string CallService(object argument, URL url)
        {
            JavaScriptSerializer Deserializer = new JavaScriptSerializer();
            string sReturnedString = string.Empty;
            string sResponse = string.Empty;
            try
            {
                sResponse = this.GetServiceResponse(argument, url);
                sReturnedString = Deserializer.Deserialize<string>(sResponse);
                return sReturnedString;
            }
            catch (Exception ex)
            {                
                throw ex;
            }            
            finally
            {
                Deserializer = null;
                sResponse = string.Empty;
            }
            

            

        }
        
        public void Dispose()
        {
            if (this.service != null) { this.service = null; }
        }
    }
}
