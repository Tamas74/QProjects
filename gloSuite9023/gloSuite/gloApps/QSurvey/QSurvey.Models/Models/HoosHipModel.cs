﻿using System;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace QSurvey.Models
{

    public interface Request
    {
        Int64 RequestID { get; set; }
        List<Tuple<string, string, string>> Categories { get; }
    }

    [Serializable]
    public class HoosHipModel : Request
    {

      
        public HoosHipModel()
            : base()
        {
            this.S1 = "never";
            this.S2 = "none";
            this.S3 = "none";
            this.S4 = "none";
            this.S5 = "none";

            this.P1 = "never";
            this.P2 = "none";
            this.P3 = "none";
            this.P4 = "none";
            this.P5 = "none";
            this.P6 = "none";
            this.P7 = "none";
            this.P8 = "none";
            this.P9 = "none";
            this.P10 = "none";

            this.A1 = "none";
            this.A2  = "none";
            this.A3  = "none";
            this.A4  = "none";
            this.A5  = "none";
            this.A6  = "none";
            this.A7  = "none";
            this.A8  = "none";
            this.A9  = "none";
            this.A10 = "none";
            this.A11 = "none";
            this.A12 = "none";
            this.A13 = "none";
            this.A14 = "none";
            this.A15 = "none";
            this.A16 = "none";
            this.A17 = "none";

            this.SP1  = "none";
            this.SP2  = "none";
            this.SP3  = "none";
            this.SP4  = "none";

            this.Q1 = "never";
            this.Q2 = "not at all";
            this.Q3 = "not at all";
            this.Q4 = "not at all";
            this.Save = "false";
        }

        public long RequestID { get; set; }

        #region Validation
        [JsonIgnore]
        public Boolean Validation_Symptoms
        {
            get
            {
                return Validation(new List<string>() { S1, S2, S3 });
            }
        }

        [JsonIgnore]
        public Boolean Validation_Stiffness
        {
            get
            {
                return Validation(new List<string>() { S4, S5 });
            }
        }

        [JsonIgnore]
        public Boolean Validation_Pain
        {
            get
            {
                return Validation(new List<string>() { P1, P2, P3, P4, P5, P6, P7, P8, P9, P10 });
            }
        }

        [JsonIgnore]
        public Boolean Validation_FunctionDailyLiving
        {
            get
            {
                return Validation(new List<string>() { A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12, A13, A14, A15, A16, A17 });
            }
        }

        [JsonIgnore]
        public Boolean Validation_FunctionSportsRecreationalActivities
        {
            get
            {
                return Validation(new List<string>() { SP1, SP2, SP3, SP4 });
            }
        }

        [JsonIgnore]
        public Boolean Validation_QualityOfLife
        {
            get
            {
                return Validation(new List<string>() { Q1, Q2, Q3, Q4 });
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

                if (Validation_Symptoms)
                {
                    lstReturned.Add(new Tuple<string, string, string>("Symptoms", LOINC_Symptoms, string.Empty));
                }

                if (Validation_Pain)
                {
                    lstReturned.Add(new Tuple<string, string, string>("Pain", LOINC_Pain, string.Empty));
                }

                if (Validation_FunctionDailyLiving)
                {
                    lstReturned.Add(new Tuple<string, string, string>("FunctionDailyLiving", LOINC_FunctionDailyLiving, string.Empty));
                }

                if (Validation_FunctionSportsRecreationalActivities)
                {
                    lstReturned.Add(new Tuple<string, string, string>("FunctionSportsRecreationalActivities", LOINC_FunctionSportsRecreationalActivities, string.Empty));
                }

                if (Validation_QualityOfLife)
                {
                    lstReturned.Add(new Tuple<string, string, string>("QualityOfLife", LOINC_QualityOfLife, string.Empty));
                }

                return lstReturned;
            }
        }

        #region "LOINC Codes"
        public string LOINC_Symptoms { get { return "72096-1"; } }
        public string LOINC_Pain { get { return "72097-9"; } }
        public string LOINC_FunctionDailyLiving { get { return "72095-3"; } }

        public string LOINC_FunctionSportsRecreationalActivities { get { return "72094-6"; } }
        public string LOINC_QualityOfLife { get { return "72093-8"; } }
        #endregion

        #region Questions
        public string S1 { get; set; }
        public string S2 { get; set; }
        public string S3 { get; set; }
        public string S4 { get; set; }
        public string S5 { get; set; }

        public string P1 { get; set; }
        public string P2 { get; set; }
        public string P3 { get; set; }
        public string P4 { get; set; }
        public string P5 { get; set; }
        public string P6 { get; set; }
        public string P7 { get; set; }
        public string P8 { get; set; }
        public string P9 { get; set; }
        public string P10 { get; set; }

        public string A1 { get; set; }
        public string A2 { get; set; }
        public string A3 { get; set; }
        public string A4 { get; set; }
        public string A5 { get; set; }
        public string A6 { get; set; }
        public string A7 { get; set; }
        public string A8 { get; set; }
        public string A9 { get; set; }
        public string A10 { get; set; }
        public string A11 { get; set; }
        public string A12 { get; set; }
        public string A13 { get; set; }
        public string A14 { get; set; }
        public string A15 { get; set; }
        public string A16 { get; set; }
        public string A17 { get; set; }

        public string SP1 { get; set; }
        public string SP2 { get; set; }
        public string SP3 { get; set; }
        public string SP4 { get; set; }

        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
        public string Q4 { get; set; }
        public string Save { get; set; }
        #endregion
    }
}
