using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace QSurvey.Models
{
    [Serializable]
    public class Promis10Model : Request
    {
        public Promis10Model()
            : base()
        {

            this.Global01="poor";
            this.Global02="poor";
            this.Global03="poor";
            this.Global04="poor";
            this.Global05="poor";
            this.Global06="not at all";
            this.Global07="0";
            this.Global08="none";
            this.Global09="poor";
            this.Global10="never";
            this.Save = "false";
        }

        public long RequestID { get; set; }

        #region Validation

        [JsonIgnore]
        public Boolean Validation_Mental
        {
            get
            {
                return Validation(new List<string>() { Global02, Global04, Global05, Global10 });
            }
        }

        [JsonIgnore]
        public Boolean Validation_Physical
        {
            get
            {
                return Validation(new List<string>() { Global03, Global06, Global07, Global08 });
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
        public string LOINC_Physical { get { return "71971-6"; } }
        public string LOINC_Mental { get { return "71969-0"; } }       
        #endregion

        #region Questions
        public string Global01 { get; set; }
        public string Global02 { get; set; }
        public string Global03 { get; set; }
        public string Global04 { get; set; }
        public string Global05 { get; set; }
        public string Global06 { get; set; }
        public string Global07 { get; set; }
        public string Global08 { get; set; }
        public string Global09 { get; set; }
        public string Global10 { get; set; }
        public string Save { get; set; }
        #endregion
    }
}
