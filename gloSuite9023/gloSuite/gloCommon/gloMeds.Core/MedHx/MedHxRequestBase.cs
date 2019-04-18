using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloMeds.Core.MedHx
{
    [Serializable]
    public class MedHxRequestBaseExt
    {
        public string Consent { get; set; }

        public Patient PatientInfo { get; set; }
        public Prescriber ProviderInfo { get; set; }
        public IEnumerable<BenifitsCoordinations> PBMInfo { get; set; }

        public MedHxRequestBaseExt()
        {

        }
    }

    [Serializable]
    public class MedHxRequestBase
    {
        public string Consent { get; set; }

        public Patient PatientInfo { get; set; }
        public Prescriber ProviderInfo { get; set; }
        public BenifitsCoordinations PBMInfo { get; set; }

        public MedHxRequestBase()
        {

        }
    }

    [Serializable]
    public class Patient
    {
        readonly int RelationShip = 1;

        public Int64 PatientID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    [Serializable]
    /// <summary>
    /// Provider Information
    /// </summary>
    public class Prescriber
    {
        public Int64 ProviderID { get; set; }
        public string NPI { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }

    [Serializable]
    /// <summary>
    /// Rx Eligibility Information (PBM)
    /// </summary>
    public class BenifitsCoordinations
    {
        public string PayerID { get; set; }
        public string PayerName { get; set; }
        public string PlanName { get; set; }
        public string PBMMemberID { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string RelatesToMessageID { get; set; }
    }
}
