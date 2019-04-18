using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QSurvey.Models
{
    [Serializable]
    public class PHQ9Model : Request
    {
        #region Constructor
        public PHQ9Model(Int32 AgeInYears)
            : this()
        {
            this.AgeInYears = AgeInYears;
        }

        public PHQ9Model()
            : base()
        {
            this.Q1 = "Not At All";
            this.Q2 = "Not At All";
            this.Q3 = "Not At All";
            this.Q4 = "Not At All";
            this.Q5 = "Not At All";
            this.Q6 = "Not At All";
            this.Q7 = "Not At All";
            this.Q8 = "Not At All";
            this.Q9 = "Not At All";
            this.Save = "false";
        }
        
        #endregion
        
        #region Validation
        [JsonIgnore]
        public Boolean Validation_Screening
        {
            get
            {
                return Validation(new List<string>() 
                {
                Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9
                });
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

        #region Properties
        [JsonIgnore]
        public List<Tuple<string, string, string>> Categories
        {
            get
            {
                List<Tuple<string, string, string>> lstReturned = new List<Tuple<string, string, string>>();

                if (Validation_Screening)
                {                    
                    if (IsPHQ2Completed)
                    {
                        lstReturned.Add(new Tuple<string, string, string>("DepressionScreening", LOINC_Depression, SNOMED_PHQ2Screening));
                    }
                }

                return lstReturned;
            }
        }

        public long RequestID { get; set; }
        public Int32 AgeInYears { get; set; } 
        #endregion

        #region "LOINC Codes"

        public string LOINC_Depression
        {
            get
            {
                if (AgeInYears >= 18)
                {
                    return this.LOINCDepression_Adult;
                }
                else
                {
                    return this.LOINCDepression_Adolescent;
                }
            }
        }

        public string LOINCDepression_Adult { get { return "73832-8"; } }
        public string LOINCDepression_Adolescent { get { return "73831-0"; } }
        public string SNOMED_PHQ2Screening { get { return "428181000124104"; } }        
        public Boolean IsPHQ2Completed { get; set; }

        #endregion

        #region Questions
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
        public string Q4 { get; set; }
        public string Q5 { get; set; }
        public string Q6 { get; set; }
        public string Q7 { get; set; }
        public string Q8 { get; set; }
        public string Q9 { get; set; }
       public string Save { get; set; }

   
        #endregion
    }
}