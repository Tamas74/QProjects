using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace gloGlobal.FS3
{
    #region Formulary Status
    
    [DataContract()]
    public class StatusRequest : IDisposable
    {
        [DataMember()]
        public string sid { get; set; }

        [DataMember()]
        public string fid { get; set; }

        [DataMember()]
        public List<Int64> ddids { get; set; }

        public StatusRequest()
        {
            this.ddids = new List<long>();
        }

        public void Dispose()
        {
        }
    }

    [DataContract()]
    public class StatusResponse :IDisposable
    {
        [DataMember()]
        public NonListedStatus nls { get; set; }

        [DataMember()]
        public List<ListedStatus> ls { get; set; }

        public StatusResponse()
        { this.ls = new List<ListedStatus>(); }

        public void Dispose()
        {
        }
    }

    [DataContract()]
    public class ListedStatus
    {
        [DataMember()]
        public Int64 id { get; set; }

        [DataMember()]
        public string fs { get; set; }
    }

    [DataContract()]
    public class NonListedStatus
    {
        [DataMember()]
        public string BRD { get; set; } //NonListedRxBrandFormularyStatus

        [DataMember()]
        public string GEN { get; set; } //NonListedRxGenericFormularyStatus

        [DataMember()]
        public string OTCB { get; set; } //NonListedOTCBrandFormularyStatus

        [DataMember()]
        public string OTCG { get; set; } //NonListedOTCGenericFormularyStatus

        [DataMember()]
        public string SUP { get; set; } //NonListedSuppliesFormularyStatus
    }

    [DataContract()]
    public class FormularyStatus : IDisposable
    {
        [DataMember]
        public Int64 id { get; set; }

        [DataMember]
        public string fs { get; set; }

        public FormularyStatus(Int64 ProductID)
        {
            this.id = ProductID;
            this.fs = "-1";
        }

        public FormularyStatus(Int64 ProductID, string Status)
        {
            this.id = ProductID;
            this.fs = Status;
        }

        public void Dispose()
        {
        }
    }

    public class FormularyDrug
    {
        public Int64 ddid { get; set; }
        public string rxtype { get; set; }

        public FormularyDrug() { }

        public FormularyDrug(Int64 DDID, string RxType)
        {
            this.ddid = DDID;
            this.rxtype = RxType;
        }
    }

    #endregion

    #region Copay

    [DataContract()]
    public class CopayRequest : IDisposable
    {
        [DataMember()]
        public string sid { get; set; }

        [DataMember()]
        public string copid { get; set; }

        [DataMember()]
        public List<Int64> ddids { get; set; }

        [DataMember()]
        public List<string> fsl { get; set; }

        public CopayRequest()
        {
            this.ddids = new List<Int64>();
            this.fsl = new List<string>();
        }

        public void Dispose()
        {
        }
    }

    [DataContract()]
    public class CopayResponse : IDisposable
    {
        [DataMember()]
        public List<CopayFactor> ds { get; set; }

        [DataMember()]
        public List<CopayFactor> sl { get; set; }

        public CopayResponse()
        {
            this.ds = new List<CopayFactor>();
            this.sl = new List<CopayFactor>();
        }

        public void Dispose()
        {

        }
    }

    [DataContract()]
    public class CopayFactor : IDisposable
    {
        /// <summary>
        /// Copay List Type 
        /// Applicable only for DrugSpecific
        /// </summary>
        [DataMember()]
        public Int64 ddid { get; set; }

        /// <summary>
        /// Copay List Type 
        /// Applicable only for SummaryLevel
        /// </summary>
        [DataMember()]
        public string fs { get; set; }

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

    [DataContract]
    public class FormularyCopay : IDisposable
    {
        [DataMember]
        public Int64 id { get; set; }

        [DataMember]
        public List<CopayFactor> cop { get; set; }

        public FormularyCopay()
        {
            this.cop = new List<CopayFactor>();
        }

        public void Dispose()
        {
        }
    }

    #endregion

    #region Payer Alternaties

    [DataContract]
    public class AlternativeRequest : IDisposable
    {
        [DataMember()]
        public string sid { get; set; }

        [DataMember()]
        public string fid { get; set; }

        [DataMember()]
        public string aid { get; set; }

        [DataMember()]
        public Int64 ddid { get; set; }

        public AlternativeRequest()
        {

        }

        public void Dispose()
        {
        }
    }

    [DataContract]
    public class AlternativeResponse : IDisposable
    {
        [DataMember()]
        public List<AlternativeDrug> alt { get; set; }

        [DataMember()]
        public bool IsFilePresent { get; set; }

        public AlternativeResponse()
        {
            this.alt = new List<AlternativeDrug>();
        }

        public void Dispose()
        {
        }
    }

    [DataContract()]
    public class AlternativeDrug
    {
        [DataMember()]
        public Int64 id { get; set; }//ProductID        

        [DataMember()]
        public string fs { get; set; }//FormularyStatus

        [DataMember()]
        public string pl { get; set; }//Preference Level     
    }
        
    public class FormularyAlternatives 
    {
        public AltListType ListType { get; set; }
        public List<gloGlobal.DIB.AlternativeDrugDetails> AlternativeList { get; set; }

        public FormularyAlternatives()
        {
            this.ListType = AltListType.None;
            this.AlternativeList = new List<gloGlobal.DIB.AlternativeDrugDetails>();
        }
    }

    #endregion

    #region Coverage

    [DataContract()]
    public class CoverageRequest : IDisposable
    {
        [DataMember()]
        public string sid { get; set; }

        [DataMember()]
        public string covid { get; set; }

        [DataMember()]
        public Int64 ddid { get; set; }

        public CoverageRequest()
        {
        }

        public void Dispose()
        {
        }
    }

    [DataContract()]
    public class CoverageResponse : IDisposable
    {
        [DataMember()]
        public CoverageFactor cov { get; set; }

        public CoverageResponse()
        {
            this.cov = new CoverageFactor();
        }
        public void Dispose()
        {
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
        {
            this.al = new AgeLimit();
            this.ql = new List<QuantityLimit>();
            this.msg = new List<string>();
            this.sm = new List<StepMeds>();
            this.rl = new List<ResourceLinks>();
        }

        public void Dispose()
        {
            //todo
        }
    }

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
        public string smnm { get; set; }

        [DataMember()]
        public string order { get; set; }

        public void Dispose()
        {
            //todo
        }
    }

    #endregion

    public class CopayCoverageGroup : IDisposable
    {
        public FormularyCopay Copay { get; set; }
        public CoverageFactor Coverage { get; set; }

        public void Dispose()
        {
            this.Copay = null;
            this.Coverage = null;
        }
    }
}
