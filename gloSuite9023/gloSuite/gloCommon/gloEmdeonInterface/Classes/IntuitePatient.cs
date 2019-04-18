using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloEmdeonInterface.Classes
{
    public class IntuitePatient
    {
        public bool Sel { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 MemberID { get; set; }
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }

        public IntuitePatient(bool Sel, Int64 MemberID, Int64 PatientId, string PatientCode, string PatientName, string Gender, DateTime Dob)
        {
            this.Sel = Sel;
            this.MemberID = MemberID;
            this.PatientId = PatientId;
            this.PatientCode = PatientCode;
            this.PatientName = PatientName;
            this.Gender = Gender;
            this.Dob = Dob;
        }


    }
}
