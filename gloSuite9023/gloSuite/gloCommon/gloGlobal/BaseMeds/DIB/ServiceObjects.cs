using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace gloGlobal.DIB
{
    #region GetNdcnDrugname

    [DataContract()]
    public class DrugInfo : IDisposable
    {
      
        [DataMember()]
        public string ndc { get; set; }   

        [DataMember()]
        public string dnm { get; set; }

        [DataMember()]
        public string gnm { get; set; }

        [DataMember()]
        public string dosage { get; set; }

        [DataMember()]
        public string rt { get; set; }

        [DataMember()]
        public string rx { get; set; }

        [DataMember()]
        public int mp { get; set; }      

        public void Dispose()
        {
            //todo
        }
    }

    [DataContract()]
    public class ClassifiedDrugRequest : IDisposable
    {

        [DataMember()]
        public int Code { get; set; }


        public void Dispose()
        {
        }
    }

    [DataContract()]
    public class NDCandDrugNameRequest : IDisposable
    {

        [DataMember()]
        public string Code { get; set; }

        [DataMember()]
        public bool IsRxnorm { get; set; }

        public void Dispose()
        {
        }
    }

    [DataContract()]
    public class NdcMpidReq : IDisposable
    {

        [DataMember()]
        public string ndc { get; set; }

        [DataMember()]
        public int mpid { get; set; }

        public void Dispose()
        {
        }
    }


    [DataContract()]
    public class RxnormFlagRequest : IDisposable
    {

        [DataMember()]
        public string Code { get; set; }

        [DataMember()]
        public int flag { get; set; }

        public void Dispose()
        {
        }
    }

    [DataContract()]
    public class RxnormFlagInfo : IDisposable
    {

        [DataMember()]
        public string Code { get; set; }

        [DataMember()]
        public string Type { get; set; }

        public void Dispose()
        {
            this.Code = string.Empty;
            this.Type = string.Empty;
        }
    }

    [DataContract()]
    public class Rxnormdetails : IDisposable
    {
        [DataMember()]
        public string Ndc { get; set; }

        [DataMember()]
        public string Rxnorm { get; set; }

        [DataMember()]
        public string Genericname { get; set; }

        public void Dispose()
        {
        }
    }

    [DataContract()]
    public class ResultSetRxnorm : IDisposable
    {

        [DataMember()]
        public List<Rxnormdetails> lgrx { get; set; }

        public ResultSetRxnorm()
        {
            this.lgrx = new List<Rxnormdetails>();
        }
        public void Dispose()
        {
        }
    }  


    [DataContract()]
    public class DrugDetails : IDisposable
    {
        [DataMember()]
        public string DrugName { get; set; }

        [DataMember()]
        public string Dosage { get; set; }

        [DataMember()]
        public string NDC { get; set; }

        [DataMember()]
        public string RxType { get; set; }


        [DataMember()]
        public string Route { get; set; }

        [DataMember()]
        public string DrugForm { get; set; }

        [DataMember()]
        public int IsNarcotics { get; set; }

        [DataMember()]
        public int MarketedProductID { get; set; }
      
        public void Dispose()
        {

        }

    }

    [DataContract()]
    public class CQMResult : IDisposable
    {
        public CQMResult()
        {
             //////MU2
            this.MarketedProductIdSet1 = new List<string>();
            this.MarketedProductIdSet2 = new List<string>();
            this.NDCSet = new List<string>();
           
        }

        //////MU2
        [DataMember()]
        public List<string>  MarketedProductIdSet1 { get; set; }

        [DataMember()]
        public List<string> MarketedProductIdSet2 { get; set; }

        [DataMember()]
        public List<string> NDCSet { get; set; }

        
        public void Dispose()
        {
        }
    }
    [DataContract()]
    public class QRDAResult : IDisposable
    {
        //public QRDAResult()
        //{
        //    //////MU2
        //    this.MarketedProductIdSet1 = new List<string>();
        //    this.MarketedProductIdSet2 = new List<string>();
        //    this.NDCSet = new List<string>();

        //}

        //////MU2
        [DataMember()]
        public string MPID { get; set; }

        [DataMember()]
        public string RxNormCodes { get; set; }

        [DataMember()]
        public string CQMMeasure { get; set; }

        public void Dispose()
        {
        }
    }

    [DataContract()]
    public class QRDANDCRequest : IDisposable
    {

        [DataMember()]
        public string Code { get; set; }

        [DataMember()]
        public bool Resultfound { get; set; }

        [DataMember()]
        public string Valueset { get; set; }

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
    public class AlternativeDrugDetails : AlternativeDrug, IDisposable
    {
        public string DrugName { get; set; }
        public string RxType { get; set; }
        public string NDC { get; set; }
        public string FormularyStatus { get; set; }
        public string AbbrivatedCopay { get; set; }
        public bool IsPayerAlternative { get; set; }

        public void Dispose()
        {

        }
    }
    #endregion

#region "CQM Objects"
    [DataContract()]
    public class InfluenzaVaccineSnoMedRequest : IDisposable
    {

        [DataMember()]
        public List<string> lst_MU_Get { get; set; }

        [DataMember()]
        public List<string> lst_MU { get; set; }


        public void Dispose()
        {
        }
    }
#endregion

    [DataContract()]
    public class PIResp : IDisposable
    {

        [DataMember()]
        public int pid { get; set; }

        [DataMember()]
        public List<int> iid { get; set; }

        public PIResp()
        {
            iid = new List<int>();
        }

        public void Dispose()
        {
        }
    }
}
