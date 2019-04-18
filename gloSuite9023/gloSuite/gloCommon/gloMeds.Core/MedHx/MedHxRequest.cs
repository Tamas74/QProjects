using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloMeds.Core.MedHx
{
    [Serializable]
    public class MedHxRequests
    {
        public List<MedHxRequest> 
            HxRequests { get; set; }

        public MedHxRequests()
        {
            HxRequests = new List<MedHxRequest>();
        }
    }

    [Serializable]
    public class MedHxRequest : MedHxRequestBase
    {
        public Int64 RequestID { get; set; }
        public Guid MessageID { get; set; }
        public Int64 PracticeID { get; set; }

        public string ParticipantID { get; set; }
        public string RxHubPassword { get; set; }

        public string ReceiverIdentification = "S00000000000001";
        public string CompanyName = "TRIARQ Practice Services";
        public string ProductName = "gloSuite";

        public string gloSuiteVersion { get; set; }
        public string ScriptVersion { get; set; }
        public string RequestXML { get; set; }
        public Guid? ParentMessageID { get; set; }
        public string TestMessage { get; set; }

        public MedHxRequest()
        {
            MessageID = Guid.NewGuid(); // Generate a unique ID
            //PracticeID = 0; // Pass
        }
    }

    [Serializable]
    public class MedHxResponse : MedHxRequest
    {
        public MedHxResponse(Guid msgID)
        {
            MessageID = msgID;
        }
        public string ResponseXML { get; set; }
        public string ResponseFile { get; set; }
        public bool IsErrorResponse { get; set; }

        public bool IsMoreHxAvaiable { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
