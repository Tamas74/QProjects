using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QSurvey.Models
{
    [Serializable]
    public class PHQ2Model : Request
    {
        #region Constructor
        public PHQ2Model(Int32 AgeInYears)
            : this()
        {
            this.AgeInYears = AgeInYears;
        }

        public PHQ2Model()
            : base()
        {
            this.Q1 = "no";
            this.Q2 = "no";
            
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
                Q1,Q2
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
                    lstReturned.Add(new Tuple<string, string, string>("DepressionCode", LOINC_DepressionCode, IsNegativeScreening ? LOINC_NegativeDepression : string.Empty));                                   
                }

                return lstReturned;
            }
        }

        public long RequestID { get; set; }
        public Int32 AgeInYears { get; set; }
        #endregion

        #region "LOINC Codes"
        public string LOINC_NegativeDepression
        {
            get
            {
                return "428171000124102";                
            }
        }

        public bool IsNegativeScreening
        {
            get
            {
                return Q1.ToLower() == "no" && Q2.ToLower() == "no";
            }
        }

        public string LOINC_DepressionCode
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
        #endregion

        #region Questions
        public string Q1 { get; set; }
        public string Q2 { get; set; }        
        public string Save { get; set; }
        #endregion
    }
}
