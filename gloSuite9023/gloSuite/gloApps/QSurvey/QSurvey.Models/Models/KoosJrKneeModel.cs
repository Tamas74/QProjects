using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace QSurvey.Models
{   
    [Serializable]
    public class KoosJrKneeModel : Request
    {
        public KoosJrKneeModel()
            : base()
        {
            this.S1 = "none";
            this.P1 = "none";
            this.P2 = "none";
            this.P3 = "none";
            this.P4 = "none";

          
            this.F1 = "none";

            this.F2= "none";
            this.Save = "false";
        }

        public long RequestID { get; set; }

        #region Validation        

        [JsonIgnore]
        public Boolean Validation_Pain
        {
            get
            {
                return Validation(new List<string>() {S1, P1, P2,P3,P4,F1,F2    });
            }
        }

        //[JsonIgnore]
        //public Boolean Validation_FunctionDailyLiving
        //{
        //    get
        //    {
        //        return Validation(new List<string>() { S1});
        //    }
        //}

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

             
                if (Validation_Pain)
                {
                    lstReturned.Add(new Tuple<string, string, string>("Pain", LOINC_Pain, string.Empty));
                }
                return lstReturned;
            }
        }

        #region "LOINC Codes"
        public string LOINC_Pain { get { return "82324-5"; } }
        #endregion

        #region Questions       
        public string P1 { get; set; }
        public string P2 { get; set; }
          public string P3 { get; set; }
        public string P4 { get; set; }
           public string F1 { get; set; }
        public string F2 { get; set; }
        
        public string S1 { get; set; }
        public string Save { get; set; }
          
        #endregion
    }
}
