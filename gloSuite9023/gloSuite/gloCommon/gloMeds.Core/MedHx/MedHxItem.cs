using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloMeds.Core.MedHx
{
    //[Serializable]

    public class MedHxItemNew
    {
        public MedHxItemNew()
        {
            this.Select = true;
        }

        public Int32 UniqueNo { get; set; }  //newlly added
        public String PayerName { get; set; }


        public bool Select { get; set; }
        public String Dup { get; set; }
        public string DrugName { get; set; }
        public string NDCCode { get; set; }
        public string DrugQty { get; set; }
        public string DaySupply { get; set; }
        public DateTime MedicationDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Refills { get; set; }
        public string PotencyCode { get; set; }

        public String Status { get; set; }
        public bool AllowSubstition { get; set; }

        public string Pharmacy { get; set; }
        public string NCPDPId { get; set; }
        public string NPI { get; set; }
        public string PharmacyAddress1 { get; set; }
        public string PharmacyAddress2 { get; set; }
        public string PharmacyCity { get; set; }
        public string PharmacyState { get; set; }
        public string PharmacyZip { get; set; }
        public string PharmacyPhone { get; set; }
        public string PharmacyFax { get; set; }
        public string PharmacyEmail { get; set; }

        public string PrescriberNPI { get; set; }
        public string Direction { get; set; }


    }

    public class MedHxItem 
    {
        public MedHxItem()
        {
            this.Select = true;
        }

        public Int32 UniqueNo { get; set; }  //newlly added
        public String PayerName { get; set; }


        public bool Select { get; set; }
        public String Dup { get;set;}
        public string DrugName { get; set; }
        public string NDCCode { get; set; }
        public int DrugQty { get; set; }
        public int DaySupply { get; set; }
        public DateTime MedicationDate { get; set; }
        public DateTime StartDate { get; set; }
        public int Refills { get; set; }
        public string PotencyCode { get; set; }

        public String Status { get; set; }
        public bool AllowSubstition { get; set; }

        public string Pharmacy { get; set; }
        public string NCPDPId { get; set; }
        public string NPI { get; set; }
        public string PharmacyAddress1 { get; set; }
        public string PharmacyAddress2 { get; set; }
        public string PharmacyCity { get; set; }
        public string PharmacyState { get; set; }
        public string PharmacyZip { get; set; }
        public string PharmacyPhone { get; set; }
        public string PharmacyFax{ get; set; }
        public string PharmacyEmail { get; set; }

        public string PrescriberNPI { get; set; }
        public string Direction { get; set; } 
        
       
    }
}
