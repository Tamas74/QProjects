using System;
using System.Text;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;

namespace gloRxHub.FS3
{
    public enum URLOption
    {
        GetDrugFormularyInfo,
        GetDrugListFormularyInfo
    }

    public class BaseServiceHelper<P>
    {
        private string ServiceURL { get; set; }

        public DecompressionMethods DecompressionMethod { get; set; }

        public BaseServiceHelper(string FormularyURL)
        {
            this.ServiceURL = FormularyURL;
            this.DecompressionMethod = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        }

        public P GetResponse(object Params, URLOption URLOption)
        {
            string jsonParams = string.Empty;
            string jsonResponse = string.Empty;

            P response;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
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

        private string GetResponse(string jsonRequest, URLOption URLOption)
        {
            try
            {
                string sURL = ServiceURL + URLOption.ToString();
                byte[] bytes = Encoding.UTF8.GetBytes(jsonRequest);
                string jsonResponseString = string.Empty;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);

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
            catch (Exception ex)
            {
                throw ex;
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
