using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloGlobal.Common;
using gloGlobal.Schemas.PDR;
using gloGlobal.Schemas.PDR.Acknowledgement;
using System.Xml.Serialization;
using System.IO;

namespace gloGlobal.PDR
{
    public class gloPDRHelper<P> : IDisposable
    {
        private string ServiceURL { get; set; }
        public string filePath { get; set; }
        public string Time { get; set; }

        public gloPDRHelper(string _ServiceURL)
        {
            this.ServiceURL = _ServiceURL;
        }

        public ProgramResponse GetProgrammes(ProgramRequest request)
        {
            BaseServiceHelper<ProgramResponseBase> helper = new BaseServiceHelper<ProgramResponseBase>(ServiceURL);
            helper.RemoveDefaultNamespace = true;
            ProgramResponse result = new ProgramResponse();
            ProgramResponseBase returned = helper.GetResponse(request);

            List<Program> programs = new List<Program>();
            programs.AddRange(returned.programs);
            result.Programs = programs;

            result.TransactionID = returned.transactionID;
            result.RxNumber = request.rxNumber;            

            helper = null;

            if (!string.IsNullOrEmpty(filePath))
            {
                WriteXML(request, "PDR_Request_");
                WriteXML(result, "PDR_Response_");
            }
            return result;
        }

        public printConfirmationResponse SendPrintConfirmation(AcknowledgementRequest Request)
        {
            printConfirmationResponse returned = null;            
            BaseServiceHelper<printConfirmationResponse> helper = new BaseServiceHelper<printConfirmationResponse>(ServiceURL);
            helper.RemoveDefaultNamespace = true;
            returned = helper.GetResponse(Request);
            helper = null;

            if (!string.IsNullOrEmpty(filePath))
            {
                WriteXML(Request, "PDR_ConfirmationRequest_");
                WriteXML(returned, "PDR_ConfirmationResponse_");
            }

            return returned;
        }

        public void WriteXML(object request, string FileName)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer serializer = new XmlSerializer(request.GetType());
            StreamWriter fileWriter = new StreamWriter(filePath + FileName + Time + ".xml");
            
            serializer.Serialize(fileWriter, request, ns);
            fileWriter.Close();

            serializer = null;
            fileWriter.Dispose();
            fileWriter = null;            
        }

        public void Dispose()
        {

        }
    }
}
