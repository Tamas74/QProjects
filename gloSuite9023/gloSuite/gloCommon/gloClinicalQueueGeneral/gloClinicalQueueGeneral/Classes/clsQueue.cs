using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloClinicalQueueGeneral.Classes
{
    public class Queue
    {
        public string QueueNo { get; set; }
        public string QueueName { get; set; }
        public string SourcePath { get; set; }

        public string PatientCode { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientMiddleName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientDob { get; set; }
        public string ClaimType { get; set; }
        public int ClaimTypeID { get; set; }

        public List<QueueDocumentDocumentDetails> DocumentList { get; set; }

        public enum enumClaimType
        {
            None,
            CMS1500,
            UB04,
            Both
        }
    }

    //public class Document
    //{
    //    public string DocumentName { get; set; }
    //    public string DocumentType { get; set; }
    //    public string PrintType { get; set; }
    //    public string ClaimType { get; set; }

    //}
}
