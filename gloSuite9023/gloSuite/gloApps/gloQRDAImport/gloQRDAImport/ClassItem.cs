using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Newtonsoft.Json;

namespace gloQRDAImport
{
   
    public class Item
    {
      //  [JsonProperty(PropertyName = "birthdate")]
        public string birthdate;
         // [JsonProperty(PropertyName = "first")]
        public string first;
        //  [JsonProperty(PropertyName = "last")]
        public string last;
         // [JsonProperty(PropertyName = "gender")]
        public string gender;

        // [JsonProperty(PropertyName = "expected_values")]
        public List<expected_values> expected_values { get; set; }
        //  [JsonProperty(PropertyName = "race")]
        public race race { get; set; }
        //  [JsonProperty(PropertyName = "ethnicity")]
        public ethnicity ethnicity { get; set; }
         // [JsonProperty(PropertyName = "conditions")]
        public List<conditions> conditions { get; set; }
       // public List<Array> encounters { get; set; }
        public List<source_data_criteria> source_data_criteria { get; set; }
    }
   
    public class expected_values
    {
      //  [JsonProperty(PropertyName = "measure_id")]
        public string measure_id;
       //  [JsonProperty(PropertyName = "IPP")]
        public int IPP;
      //   [JsonProperty(PropertyName = "DENOM")]
        public int DENOM;
      //    [JsonProperty(PropertyName = "DENEX")]
        public int DENEX;
       //   [JsonProperty(PropertyName = "NUMER")]
        public int NUMER;
       //   [JsonProperty(PropertyName = "DENEXCEP")]
        public int DENEXCEP;
        //   [JsonProperty(PropertyName = "population_index")]
        public int population_index;


    }

    public class race
    {
         // [JsonProperty(PropertyName = "name")]
        public string name;
    }
    public class ethnicity
    {
        //   [JsonProperty(PropertyName = "name")]
        public string name;
    }

    public class conditions
    {
      
           public List<codes> codes{get;set;}
    }
    public class codes
    {

     //   [JsonProperty("ICD-10-CM")]
        public List<string> ICD10{get;set;}
      //  [JsonProperty("ICD-9-CM")]
        public List<string> ICD9 { get; set; }
       // [JsonProperty("SNOMED-CT")]
        public List<string> Snomedct{ get; set; }

    }

    public class source_data_criteria
    {
        public string id { get; set; }
        public List<BPvalues> value { get; set; }
    }
    public class BPvalues
    {
        public string Value { get; set; }
    }
   
   
}
