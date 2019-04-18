using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using gloGlobal.Common;

namespace gloGlobal.EPA
{
   
    [DataContract()]
    public class AuthRequest
    {
        public AuthRequest()
        {
            roles = new roles();
        }

        [DataMember]
        public string endUserId { get; set; }

        [DataMember]
        public roles roles { get; set; }

        [DataMember]
        public string systemUserID { get; set; }

        [DataMember]
        public string systemUserQualifier { get; set; }
    }

    [DataContract()]
    public class AuthResponse
    {
        [DataMember]
        public string token { get; set; }

        [DataMember]
        public string utcExpiration { get; set; }
    }

    [DataContract]
    public class PAStatusRequest
    {
        public PAStatusRequest(string reqid)
        { this.paRefID = reqid; }

        [DataMember]
        public string paRefID { get; set; }
    }

    [DataContract]
    public class PAStatusResponse
    {
        public PAStatusResponse()
        { this.stat = "R"; }

        [DataMember]
        public string stat { get; set; }

        [DataMember]
        public string pan { get; set; }

        [DataMember]
        public string note { get; set; }
       
        [DataMember]
        public DateTime? exp { get; set; }

        [DataMember]
        public string error { get; set; }
    }

    [DataContract()]
    public class MedicationPrescribed 
    {
        public MedicationPrescribed()
        {
            this.Pharmacy = new ServiceObjectBase.Pharmacy();
            this.Medication = new ServiceObjectBase.Medication();
        }

        [DataMember]
        public PAInitRequest.Pharmacy Pharmacy { get; set; }

        [DataMember]
        public PAInitRequest.Medication Medication { get; set; }
    }

    [DataContract]
    public class PAInitRequest : ServiceObjectBase
    {
        public PAInitRequest()
        {
            this.patient = new Patient();
            this.prescriber = new Prescriber();            
            this.pbm = new BenifitCoordination();
            this.MedicationPrescribed = new List<EPA.MedicationPrescribed>();
        }

        [DataMember]
        public string paRefID { get; set; }

        [DataMember]
        public string from { get; set; }

        [DataMember]
        public BenifitCoordination pbm { get; set; }

        [DataMember]
        public Patient patient { get; set; }

        [DataMember]
        public Prescriber prescriber { get; set; }

        [DataMember]
        public Prescriber submitter { get; set; }

        [DataMember]
        public List<MedicationPrescribed> MedicationPrescribed { get; set; }
    }

    [DataContract]
    public class PAInitResponse
    {
        public PAInitResponse()
        {

        }

        [DataMember]
        public string paRefID { get; set; }

        [DataMember]
        public string MsgID { get; set; }

        [DataMember]
        public string sCode { get; set; }

        [DataMember]
        public string sDesc { get; set; }

        [DataMember]
        public string sType { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public bool IsSuccesful { get; set; }

        [DataMember]
        public PAInitRequest.MedicationBase Med { get; set; }

        //[DataMember]
        //public string mpid { get; set; }

        //[DataMember]
        //public string ndc { get; set; }
    } 

    [DataContract()]
    public class roles
    {
        public roles()
        {
            commonRoles = new System.Collections.Generic.List<string>();
            providers = new System.Collections.Generic.List<provider>();
        }

        [DataMember]
        public List<string> commonRoles { get; set; }

        [DataMember]
        public List<provider> providers { get; set; }
    }

    [DataContract()]
    public class provider
    {
        public provider()
        {

        }

        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string qualifier
        {
            get;
            set;
        }
    }
}
