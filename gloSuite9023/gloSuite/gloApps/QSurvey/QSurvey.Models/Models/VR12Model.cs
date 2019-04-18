using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QSurvey.Models
{
    [Serializable]
    public class VR12Model : Request
    {
        public VR12Model()
            : base()
        {            
            this.Q1 = "excellent";

            this.Q2A = "nonotlimitedatall";
            this.Q2B = "nonotlimitedatall";
            this.Q3A = "nononeofthetime";
            this.Q3B = "nononeofthetime";
            this.Q4A = "nononeofthetime";
            this.Q4B = "nononeofthetime";

            this.Q5 = "notatall";
            this.Q6A = "allofthetime";
            this.Q6B = "allofthetime";
            this.Q6C = "noneofthetime";
            this.Q7 = "noneofthetime";

            this.Q8 = "muchbetter";
            this.Q9 = "muchbetter";
            this.Save = "false";
        }

        public long RequestID { get; set; }

        #region Validation
        [JsonIgnore]
        public Boolean Validation_Mental
        {
            get
            {
                return Validation(new List<string>() { Q4A, Q4B, Q6A, Q6C, Q9 });
            }
        }

        [JsonIgnore]
        public Boolean Validation_Physical
        {
            get
            {
                return Validation(new List<string>() { Q2A, Q2B, Q3A, Q3B, Q5, Q6B, Q7, Q8 });
            }
        }

        public Boolean Validation(List<string> InputList)
        {
            return InputList.TrueForAll(p => p != null
                && !string.IsNullOrWhiteSpace(p)
                && !string.IsNullOrEmpty(p)
                && p.Length > 0);
        }
        #endregion

        [JsonIgnore]
        public List<Tuple<string, string, string>> Categories
        {
            get
            {
                List<Tuple<string, string, string>> lstReturned = new List<Tuple<string, string, string>>();

                if (Validation_Mental)
                {
                    lstReturned.Add(new Tuple<string, string, string>("Mental", LOINC_Mental, string.Empty));
                }
                
                if (Validation_Physical)
                {
                    lstReturned.Add(new Tuple<string, string, string>("Physical", LOINC_Physical, string.Empty));
                }
               
                return lstReturned;
            }
        }

        #region "LOINC Codes"
        public string LOINC_Mental { get { return "72026-8"; } }
        public string LOINC_Physical { get { return "72025-0"; } }        
        #endregion

        #region Questions        
        public string Q1 { get; set; }
        public string Q2A { get; set; }
        public string Q2B { get; set; }
        public string Q3A { get; set; }
        public string Q3B { get; set; }
        public string Q4A { get; set; }
        public string Q4B { get; set; }
        public string Q5 { get; set; }
        public string Q6A { get; set; }
        public string Q6B { get; set; }
        public string Q6C { get; set; }
        public string Q7 { get; set; }
        public string Q8 { get; set; }
        public string Q9 { get; set; }
        public string Save { get; set; }
        #endregion
    }
}