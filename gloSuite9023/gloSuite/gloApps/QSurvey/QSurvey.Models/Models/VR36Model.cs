using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QSurvey.Models
{
    [Serializable]
    public class VR36Model : Request
    {
        public VR36Model()
            : base()
        {
            this.Q1 = "Excellent";

            this.Q2A = "No, Not Limited At ALL";
            this.Q2B = "No, Not Limited At ALL";
            this.Q2C = "No, Not Limited At ALL"; 
            this.Q2D = "No, Not Limited At ALL"; 
            this.Q2E = "No, Not Limited At ALL"; 
            this.Q2F = "No, Not Limited At ALL"; 
            this.Q2G = "No, Not Limited At ALL"; 
            this.Q2H = "No, Not Limited At ALL"; 
            this.Q2I = "No, Not Limited At ALL"; 
            this.Q2J = "No, Not Limited At ALL"; 
            this.Q3A ="No, None Of The Time";
            this.Q3B ="No, None Of The Time";
            this.Q3C ="No, None Of The Time";
            this.Q3D ="No, None Of The Time";
            this.Q4A ="No, None Of The Time";
            this.Q4B ="No, None Of The Time";
            this.Q4C ="No, None Of The Time";
            this.Q5 = "Not At All";
            this.Q6 = "None";
            this.Q7 = "Not At All";
            this.Q8A = "None Of The Time";
            this.Q8B = "None Of The Time";
            this.Q8C = "None Of The Time";
            this.Q8D = "None Of The Time";
            this.Q8E = "None Of The Time";
            this.Q8F = "None Of The Time";
            this.Q8G = "None Of The Time";
            this.Q8H = "None Of The Time";
            this.Q8I = "None Of The Time";
            this.Q9 = "None Of The Time";
            this.Q10A = "Definitely False";
            this.Q10B = "Definitely False";
            this.Q10C = "Definitely False";
            this.Q10D = "Definitely False";
            this.Q11 = "Much Worse";
            this.Q12 = "Much Worse";
            this.Save = "false";
        }

        public long RequestID { get; set; }

        #region Validation
        [JsonIgnore]
        public Boolean Validation_Physical
        {
            get
            {
                return Validation(new List<string>() {
                Q1,Q2A,Q2B,Q2C,Q2D,Q2E,Q2F, Q2G, Q2H, Q2I, Q2J, Q3A, Q3B, Q3C, Q3D, Q5 ,Q6 , Q7 ,
                Q9 , Q10A,  Q10B, Q10C, Q10D,  Q11, Q12 
                });
            }             
        }

        [JsonIgnore]
        public Boolean Validation_Mental
        {
            get
            {
                return Validation(new List<string>() {
                Q4A, Q4B, Q4C, Q4D, Q8A, Q8B, Q8C, Q8D, Q8E, Q8F, Q8G, Q8H, Q8I
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

                if (Validation_Mental)
                {
                    lstReturned.Add(new Tuple<string, string, string>("Mental", LOINC_Mental, string.Empty));
                }
                return lstReturned;
            }
        }

        #region "LOINC Codes"
        public string LOINC_Mental { get { return "72008-6"; } }
        public string LOINC_Physical { get { return "72007-8"; } }        
        #endregion

        #region Questions
        public string Q1 { get; set; }
        public string Q2A { get; set; }
        public string Q2B { get; set; }
        public string Q2C { get; set; }
        public string Q2D { get; set; }
        public string Q2E { get; set; }
        public string Q2F { get; set; }
        public string Q2G { get; set; }
        public string Q2H { get; set; }
        public string Q2I { get; set; }
        public string Q2J { get; set; }

    public string  Q3A   { get; set; }
    public string  Q3B   { get; set; }
    public string  Q3C   { get; set; }
    public string  Q3D   { get; set; }
    public string  Q4A   { get; set; }
    public string  Q4B   { get; set; }
    public string  Q4C   { get; set; }
    public string  Q4D   { get; set; }
    public string  Q5    { get; set; }
    public string  Q6    { get; set; }
    public string  Q7    { get; set; }
    public string  Q8A   { get; set; }
    public string  Q8B   { get; set; }
    public string  Q8C   { get; set; }
    public string  Q8D   { get; set; }
    public string  Q8E   { get; set; }
    public string  Q8F   { get; set; }
    public string  Q8G   { get; set; }
    public string  Q8H   { get; set; }
    public string  Q8I   { get; set; }
    public string  Q9    { get; set; }
    public string  Q10A  { get; set; }
    public string  Q10B  { get; set; }
    public string  Q10C  { get; set; }
    public string  Q10D  { get; set; }
    public string  Q11   { get; set; }
    public string  Q12   { get; set; }
    public string Save { get; set; }
        #endregion
    }
}