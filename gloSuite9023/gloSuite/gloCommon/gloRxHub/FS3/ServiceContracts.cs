using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace gloRxHub.FS3
{
    public enum ResourceLinkEnum
    {
        AgeLimits = 1,
        Coapy = 2,
        CoverageExclusion = 3,
        Formulary = 4,
        GeneralInfo = 5,
        GenderLimits = 6,
        PriorAuthorization = 7,
        QuantityLimits = 8,
        StepTherapy = 9
    }

    [DataContract()]
    public class DrugFormularyInfo : IDisposable
    {
        [DataMember()]
        public Int64 id { get; set; }

        [DataMember()]
        public string fs { get; set; }

        [DataMember()]
        public List<CopayFactor> cop { get; set; }

        [DataMember()]
        public List<CoverageFactor> cov { get; set; }

        public void Dispose()
        {
            //todo
        }
    }

    [DataContract()]
    public class CopayFactor : IDisposable
    {
        /// <summary>
        /// Copay List Type 
        /// Drug Specific, Summary level
        /// </summary>
        [DataMember()]
        public string ddid { get; set; }

        /// <summary>
        /// Copay List Type 
        /// Drug Specific, Summary level
        /// </summary>
        [DataMember()]
        public string type { get; set; }

        /// <summary>
        /// Pharmacy Type 
        /// Mail, Retail, Long Term, Speciality, Any, Blank
        /// </summary>
        [DataMember()]
        public string ptype { get; set; }

        /// <summary>
        /// Flat Copay Amount
        /// </summary>
        [DataMember()]
        public string flat { get; set; }

        /// <summary>
        /// Days supply 
        /// </summary>
        [DataMember()]
        public string days { get; set; }

        /// <summary>
        /// First Copay Term
        /// </summary>
        [DataMember()]
        public string firstterm { get; set; } //FirstCoPayTerm

        #region Percent Copay Rage

        /// <summary>
        /// Percent Copay Rate
        /// </summary>
        [DataMember()]
        public string rate { get; set; }            //PercentCoPayRate

        /// <summary>
        /// Minimum Copay
        /// </summary>
        [DataMember()]
        public string mincop { get; set; } //MinimumCoPay

        /// <summary>
        /// Maximum Copay
        /// </summary>
        [DataMember()]
        public string maxcop { get; set; } //MaximumCoPay

        #endregion

        #region Out of pocket

        /// <summary>
        /// Start range - Out of pocket 
        /// </summary>
        [DataMember()]
        public string spocket { get; set; } //PercentCoPayRate

        /// <summary>
        /// End range - Out of pocket
        /// </summary>
        [DataMember()]
        public string epocket { get; set; } //PercentCoPayRate        

        #endregion

        #region Tier

        /// <summary>
        /// Min Copay Tier
        /// </summary>
        [DataMember()]
        public string mintier { get; set; } //CoPayTier

        /// <summary>
        /// Max Copay Tier
        /// </summary>
        [DataMember()]
        public string maxtier { get; set; } //MaximumCoPayTier

        #endregion

        //[DataMember()]
        //public string FormularyStatus { get; set; } //ProductID

        //[DataMember()]
        //public string ProductID { get; set; } //ProductID

        //[DataMember()]
        //public string CopayID { get; set; } //CopayID

        public void Dispose()
        {
            //todo
        }
    }

    [DataContract()]
    public class CoverageFactor : IDisposable
    {
        /// <summary>
        /// Age Limit
        /// </summary>
        [DataMember()]
        public AgeLimit al { get; set; }

        /// <summary>
        /// Gender Limit
        /// </summary>
        [DataMember()]
        public string gl { get; set; }

        /// <summary>
        /// Prior Authorization 
        /// </summary>
        [DataMember()]
        public bool pa { get; set; }

        /// <summary>
        /// Step therapy
        /// </summary>
        [DataMember()]
        public bool st { get; set; }

        /// <summary>
        /// Quantity Limits
        /// </summary>
        [DataMember()]
        public List<QuantityLimit> ql { get; set; }

        /// <summary>
        /// Coverage Message 
        /// Long Message if present
        /// else Short Message of long msg is not present
        /// </summary>
        [DataMember()]
        public List<string> msg { get; set; }

        /// <summary>
        /// Drug Exlusion
        /// </summary>
        [DataMember()]
        public bool de { get; set; }

        /// <summary>
        /// Step Medications
        /// </summary>
        [DataMember()]
        public List<StepMeds> sm { get; set; }

        /// <summary>
        /// Resource Lin
        /// </summary>
        [DataMember()]
        public List<ResourceLinks> rl { get; set; }

        public CoverageFactor()
        { }

        public void Dispose()
        {
            //todo
        }
    }

    #region Coverage Factors

    [DataContract()]
    public class AgeLimit : IDisposable
    {
        /// <summary>
        /// Minimum Age Limit
        /// </summary>
        [DataMember()]
        public string minage { get; set; }

        /// <summary>
        /// Minimum Age Qualifier
        /// </summary>
        [DataMember()]
        public string minageq { get; set; }

        /// <summary>
        /// Maximum Age Limit
        /// </summary>
        [DataMember()]
        public string maxage { get; set; }

        /// <summary>
        /// Maximum Age Qualifier
        /// </summary>
        [DataMember()]
        public string maxageq { get; set; }

        void IDisposable.Dispose()
        {
            //todo
        }
    }

    [DataContract()]
    public class ResourceLinks : IDisposable
    {
        [DataMember()]
        public ResourceLinkEnum type { get; set; }

        [DataMember()]
        public string url { get; set; }

        public void Dispose()
        {
            //todo
        }
    }

    [DataContract()]
    public class QuantityLimit : IDisposable
    {
        /// <summary>
        /// Max amount
        /// </summary>
        [DataMember()]
        public string maxamt { get; set; }

        /// <summary>
        /// Max amount Qualifier
        /// </summary>
        [DataMember()]
        public string maxamtq { get; set; }

        /// <summary>
        /// Max amount Units
        /// </summary>
        [DataMember()]
        public string maxamtu { get; set; }

        /// <summary>
        /// Max amount Time Period
        /// </summary>
        [DataMember()]
        public string maxamtp { get; set; }

        public void Dispose()
        {
            //todo
        }
    }

    [DataContract()]
    public class StepMeds : IDisposable
    {
        [DataMember()]
        public string smid { get; set; }

        [DataMember()]
        public string order { get; set; }

        public void Dispose()
        {
            //todo
        }
    }

    #endregion
}
