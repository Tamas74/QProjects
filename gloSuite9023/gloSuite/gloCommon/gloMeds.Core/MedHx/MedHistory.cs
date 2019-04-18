using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloMeds.Core.MedHx
{
    public class MedHistory
    {
        public MedHistory()
        {

        }
            public Int64 nPatientID;
            public string DrugName{get;set;}
            public string DrugQuantity { get; set; }
            public String ProductCode { set; get; }
            public string Strength { get; set; }
            public String Quantity { get; set; }
            public String Potency { get; set; }
            public string Duration { get; set;}
            public string Refills { get; set; }         
            public Int64 ProviderID{set;get;}
            public string Pharmacy { get; set; }
            public string MedicationDate { get; set; }
            public string StartDate { set; get; }
            public string EndDate { set; get; }
            public string Status { set; get; }
            public string Route { set; get; }
            public String Diagnosis { set; get; }
        
           

    }


}
