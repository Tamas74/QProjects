using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace QSurvey.Models
{   
    [Serializable]
    public class HoosJRHipModel : Request
    {
        public HoosJRHipModel()
            : base()
        {            
            this.P1 = "never";
            this.P2 = "none";

            this.S1 = "none";
            this.S2  = "none";
            this.S3  = "none";
            this.S4  = "none";
            this.Save = "false";
        }

        public long RequestID { get; set; }

        #region Validation        

        [JsonIgnore]
        public Boolean Validate
        {
            get
            {
                return Validation(new List<string>() { P1, P2, S1, S2, S3, S4 });
            }
        }

        //[JsonIgnore]
        //public Boolean Validation_FunctionDailyLiving
        //{
        //    get
        //    {
        //        return Validation(new List<string>() { S1, S2, S3, S4});
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


                if (Validate)
                {
                    lstReturned.Add(new Tuple<string, string, string>("Pain", LOINC_Pain, string.Empty));
                }

                return lstReturned;
            }
        }

        #region "LOINC Codes"
    //    public string LOINC_Symptoms { get { return "72096-1"; } }
        public string LOINC_Pain { get { return "72097-9"; } }
      //  public string LOINC_FunctionDailyLiving { get { return "72095-3"; } }

     //   public string LOINC_FunctionSportsRecreationalActivities { get { return "72094-6"; } }
    //    public string LOINC_QualityOfLife { get { return "72093-8"; } }
        #endregion

        #region Questions       
        public string P1 { get; set; }
        public string P2 { get; set; }
        
        public string S1 { get; set; }
        public string S2 { get; set; }
        public string S3 { get; set; }
        public string S4 { get; set; }
        public string Save { get; set; }
        #endregion
    }
}
