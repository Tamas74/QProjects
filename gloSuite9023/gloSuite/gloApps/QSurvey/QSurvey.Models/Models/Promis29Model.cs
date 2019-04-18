using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace QSurvey.Models
{
    [Serializable]
    public class Promis29Model : Request
    {
        public Promis29Model()
            : base()
        {

            this.PF1 ="Without any difficulty";
            this.PF2 ="Without any difficulty";
            this.PF3 ="Without any difficulty";
            this.PF4 ="Without any difficulty";
            this.ANX1= "never";
            this.ANX2= "never";
            this.ANX3= "never";
            this.ANX4= "never";
            this.DP7_1 = "never";
            this.DP7_2 = "never";
            this.DP7_3 = "never";
            this.DP7_4 = "never";
            this.FDP7_1 = "not at all";
            this.FDP7_2 = "not at all";
            this.FDP7_3 = "not at all";
            this.FDP7_4 = "not at all";

                 this.SDP7_1 ="very poor";
            this.SDP7_2 ="not at all" ;
            this.SDP7_3 ="not at all" ;
            this.SDP7_4 ="not at all"; 
            this.SSR7_1 ="not at all" ;
            this.SSR7_2 ="not at all" ;
            this.SSR7_3 ="not at all"; 
            this.SSR7_4 ="not at all"; 
            
            this.PIFP7_1="not at all" ;
            this.PIFP7_2="not at all" ;
            this.PIFP7_3="not at all"; 
            this.PIFP7_4="not at all"; 
            this.PINP7_1= "0";
            this.Save = "false";
          
        }

        public long RequestID { get; set; }

        #region Validation

        [JsonIgnore]
        public Boolean Validation_Anxiety
        {
            get
            {
                return Validation(new List<string>() { ANX1, ANX2, ANX3, ANX4 });
            }
        }

        [JsonIgnore]
        public Boolean Validation_Physical
        {
            get
            {
                return Validation(new List<string>() { PF1, PF2, PF3, PF4 });
            }
        }

        [JsonIgnore]
        public Boolean Validation_Depression
        {
            get
            {
                return Validation(new List<string>() { DP7_1, DP7_2, DP7_3, DP7_4 });
            }
        }

        [JsonIgnore]
        public Boolean Validation_Fatigue
        {
            get
            {
                return Validation(new List<string>() { FDP7_1, FDP7_2, FDP7_3, FDP7_4 });
            }
        }

        [JsonIgnore]
        public Boolean Validation_Sleep
        {
            get
            {
                return Validation(new List<string>() { SDP7_1, SDP7_2, SDP7_3, SDP7_4 });
            }
        }

        [JsonIgnore]
        public Boolean Validation_Pain
        {
            get
            {
                return Validation(new List<string>() { PF1, PF2, PF3, PF4 });
            }
        }

        [JsonIgnore]
        public Boolean Validation_SocialRoles
        {
            get
            {
                return Validation(new List<string>() { SSR7_1, SSR7_2, SSR7_3, SSR7_4 });
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


                if (Validation_Physical)
                {
                    lstReturned.Add(new Tuple<string, string, string>("Physical", LOINC_Physical, string.Empty));
                }

                if (Validation_Anxiety)
                {
                    lstReturned.Add(new Tuple<string, string, string>("Anxiety", LOINC_Anxiety, string.Empty));
                }

                if (Validation_Depression)
                {
                    lstReturned.Add(new Tuple<string, string, string>("Depression", LOINC_Depression, string.Empty));
                }

                if (Validation_Fatigue)
                {
                    lstReturned.Add(new Tuple<string, string, string>("Fatigue", LOINC_Fatigue, string.Empty));
                }
                
                if (Validation_Sleep)
                {
                    lstReturned.Add(new Tuple<string, string, string>("Sleep", LOINC_Sleep, string.Empty));
                }

                if (Validation_Pain)
                {
                    lstReturned.Add(new Tuple<string, string, string>("Sleep", LOINC_Pain, string.Empty));
                }

                if (Validation_SocialRoles)
                {
                    lstReturned.Add(new Tuple<string, string, string>("SocialRoles", LOINC_SocialRoles, string.Empty));
                }

                return lstReturned;
            }
        }

        #region "LOINC Codes"
        public string LOINC_Physical { get { return "71959-1"; } }
        public string LOINC_Anxiety { get { return "71967-4"; } }
        public string LOINC_Depression { get { return "71965-8"; } }

        public string LOINC_Fatigue { get { return "71963-3"; } }
        public string LOINC_Sleep { get { return "71955-9"; } }        
        public string LOINC_Pain { get { return "71961-7"; } }

        public string LOINC_SocialRoles { get { return "71957-5"; } }
        #endregion

        #region Questions
        public string PF1 { get; set; }
        public string PF2 { get; set; }
        public string PF3 { get; set; }
        public string PF4 { get; set; }
        public string ANX1 { get; set; }
        public string ANX2 { get; set; }
        public string ANX3 { get; set; }
        public string ANX4 { get; set; }
        public string DP7_1 { get; set; }
        public string DP7_2 { get; set; }
        public string DP7_3 { get; set; }
        public string DP7_4 { get; set; }
        public string FDP7_1 { get; set; }
        public string FDP7_2 { get; set; }
        public string FDP7_3 { get; set; }
        public string FDP7_4 { get; set; }
                      
        public string SDP7_1 { get; set; }
        public string SDP7_2 { get; set; }
        public string SDP7_3 { get; set; }
        public string SDP7_4 { get; set; }
        public string SSR7_1 { get; set; }
        public string SSR7_2 { get; set; }
        public string SSR7_3 { get; set; }
        public string SSR7_4 { get; set; }
        public string Save { get; set; }

       
        public string PIFP7_1 { get; set; }
        public string PIFP7_2 { get; set; }
        public string PIFP7_3 { get; set; }
        public string PIFP7_4 { get; set; }
        public string PINP7_1 { get; set; }
       
        #endregion
    }
}
