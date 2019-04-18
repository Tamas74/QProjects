using System;
using System.Text;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.ComponentModel;
using System.Collections.Generic;

namespace gloGlobal.Common
{

    //#region Factory Pattern
    
    //public interface IService<I, O>
    //{
    //    O GetResponse(I parameter);
    //}

    //public class ServiceJson<I, O> : IService<I, O>
    //{
    //    public string URL { get; set; }

    //    public ServiceJson(string URL)
    //    { this.URL = URL; }
        
    //    public O GetResponse(I parameter)
    //    {
    //        return default(O);
    //    }
    //}

    //public class ServiceHTML<I, O> : IService<I, O>
    //{
    //    public string URL { get; set; }

    //    public ServiceHTML(string URL)
    //    { this.URL = URL; }

    //    public O GetResponse(I parameter)
    //    {
    //        return default(O);
    //    }
    //}

    //public class ServiceFactory<I, O>
    //{
    //    public static IService<I, O> GetService(string URL, Boolean JSON)
    //    {
    //        if (JSON)
    //        { return new ServiceJson<I, O>(URL); }
    //        else
    //        { return new ServiceHTML<I, O>(URL); }
    //    }
    //}

    //#endregion
 
    public class BaseServiceHelper<P>
    {
        private string ServiceURL { get; set; }

        public DecompressionMethods DecompressionMethod { get; set; }
        public bool RemoveDefaultNamespace { get; set; }
        
        [DefaultValue(false)]
        public bool JsonType { get; set; }

        [DefaultValue(false)]
        public bool ContentType { get; set; }

        public BaseServiceHelper(string URL)
        {
            this.ServiceURL = URL;
            this.DecompressionMethod = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        }

        public P GetResponse(object Parameters)
        {
            string xmlParams = string.Empty;
            string sResponse = string.Empty;

            P response = default(P);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            XmlSerializer serializer = new XmlSerializer(Parameters.GetType());
            XmlSerializer deserializer = new XmlSerializer(typeof(P));
            
            try
            {
                if (this.RemoveDefaultNamespace) { ns.Add("", ""); }                
                using (StringWriter textWriter = new StringWriter())
                {                    
                    serializer.Serialize(textWriter, Parameters, ns);
                    xmlParams = textWriter.ToString();
                }
                
                sResponse = this.GetResponse(xmlParams);

                if (!String.IsNullOrWhiteSpace(sResponse))
                {
                    if (sResponse.ToLower().Contains("<error>"))
                    {
                        throw new Exception("PDR Validation error: " + sResponse);
                    }
                }

                using (StringReader read = new StringReader(sResponse))
                {
                    using (XmlTextReader xmlReader = new XmlTextReader(read))
                    {  
                        object obj = deserializer.Deserialize(xmlReader);
                        if (obj is P) { response = (P)obj; }
                    }                    
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                serializer = null;
                deserializer = null;
                ns = null;
            }
        }

        public string GetResponse(string xmlRequest, Dictionary<string, string> CustomHeaders)
        {
            string sURL = null;
            byte[] bytes = null;
            HttpWebRequest request = null;

            try
            {
                sURL = ServiceURL;
                bytes = Encoding.UTF8.GetBytes(xmlRequest);
                string xmlResponseString = string.Empty;

                request = (HttpWebRequest)WebRequest.Create(sURL);

                request.Method = "POST";
                
                if (JsonType)
                { request.ContentType = "application/json"; }
                else
                { request.ContentType = "application/html"; }

                if (ContentType == false)
                { request.Accept = "application/html"; }

                if (CustomHeaders != null && CustomHeaders.Count > 0)
                {
                    foreach (KeyValuePair<string, string> t in CustomHeaders)
                    {
                        request.Headers.Add(t.Key, t.Value);
                    }
                }

                request.AutomaticDecompression = this.DecompressionMethod;

                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(bytes, 0, bytes.Length);
                    postStream.Close();
                }

                xmlResponseString = this.PostRequest(request);
                return xmlResponseString;
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sURL = null;
                bytes = null;
                request = null;
            }
        }

        public string GetResponse(string xmlRequest)
        {
            string sURL = null;
            byte[] bytes = null;
            HttpWebRequest request = null;

            try
            {
                sURL = ServiceURL;
                bytes = Encoding.UTF8.GetBytes(xmlRequest);
                string xmlResponseString = string.Empty;

                request = (HttpWebRequest)WebRequest.Create(sURL);

                request.Method = "POST";

                if (JsonType)
                { request.ContentType = "application/json"; }
                else
                { request.ContentType = "application/html"; }

                if (ContentType == false)
                { request.Accept = "application/html"; }
                
                

                request.AutomaticDecompression = this.DecompressionMethod;

                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(bytes, 0, bytes.Length);
                    postStream.Close();
                }

                xmlResponseString = this.PostRequest(request);                
                return xmlResponseString;
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sURL = null;
                bytes = null;
                request = null;
            }
        }

        private string PostRequest(HttpWebRequest Request)
        {
            string sReturned = "";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)Request.GetResponse())
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
            {
                throw ex;
            }

            return sReturned;
        }
    }

    public class BaseServiceHelper<P,E>
    {
        private string ServiceURL { get; set; }

        public DecompressionMethods DecompressionMethod { get; set; }

        public BaseServiceHelper(string FormularyURL)
        {
            this.ServiceURL = FormularyURL;
            this.DecompressionMethod = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        }

        public P GetResponse(object Params, E URLOption)
        {
            string jsonParams = string.Empty;
            string jsonResponse = string.Empty;

            P response;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            try
            {

                jsonParams = serializer.Serialize(Params);
                jsonResponse = this.GetResponse(jsonParams, URLOption);
                response = serializer.Deserialize<P>(jsonResponse);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (serializer != null) { serializer = null; }
            }
        }

        private string GetResponse(string jsonRequest, E URLOption)
        {
            string sURL = null;
            byte[] bytes = null;
            HttpWebRequest request = null;

            try
            {
                sURL = ServiceURL + URLOption;
                bytes = Encoding.UTF8.GetBytes(jsonRequest);
                string jsonResponseString = string.Empty;

                request = (HttpWebRequest)WebRequest.Create(sURL);

                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";
                request.AutomaticDecompression = this.DecompressionMethod;

                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(bytes, 0, bytes.Length);
                    postStream.Close();
                }

                jsonResponseString = this.PostRequest(request);

                return jsonResponseString;
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sURL = null;
                bytes = null;
                request = null;
            }
        }

        private string PostRequest(HttpWebRequest Request)
        {
            string sReturned = "";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)Request.GetResponse())
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
            {
                throw ex;
            }

            return sReturned;
        }
    }
}
