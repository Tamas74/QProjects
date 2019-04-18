using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace gloGlobal.DI
{
    [DataContract()]
    public class RequestBase : IDisposable
    {
        [DataMember()]
        public string sev { get; set; }

        [DataMember()]
        public string dl { get; set; }

        [DataMember()]
        public string onset { get; set; }

        public virtual void Dispose()
        {

        }
    }

    [DataContract()]
    public class DoseCheckRequest 
    {
        [DataMember()]
        public string gender { get; set; }

        [DataMember()]
        public DateTime dob { get; set; }

        [DataMember()]
        public string ndc { get; set; }

        [DataMember()]
        public decimal qty { get; set; }

        [DataMember()]
        public int freq { get; set; }

        [DataMember()]
        public int interval { get; set; }

    }

    [DataContract()]
    public class DIRequest : RequestBase
    {
        [DataMember()]
        public string gender { get; set; }

        [DataMember()]
        public DateTime dob { get; set; }

        [DataMember()]
        public List<string> mx { get; set; }

        [DataMember()]
        public List<string> rx { get; set; }

        [DataMember()]
        public List<dxItem> dxl { get; set; }

        [DataMember()]
        public List<string> al { get; set; }

        [DataMember()]
        public bool dta_by_al { get; set; }

        public DIRequest()
        {
            this.mx = new List<string>();
            this.rx = new List<string>();
            this.dxl = new List<dxItem>();
            this.al = new List<string>();
        }

        public DIRequest(bool UseAllergy)
        {
            this.mx = new List<string>();
            this.rx = new List<string>();
            this.dxl = new List<dxItem>();
            this.al = new List<string>();
            this.dta_by_al = UseAllergy;
        }

        public override void Dispose()
        {
            this.mx.Clear();
            this.rx.Clear();
            this.dxl.Clear();
            this.al.Clear();
            base.Dispose();
        }
    }

    [DataContract()]
    public class AlertFilters
    {
        [DataMember()]
        public string ade_sev { get; set; }

        [DataMember()]
        public string di_sev { get; set; }

        [DataMember()]
        public string dfa_sev { get; set; }

        [DataMember()]
        public string di_dl { get; set; }

        [DataMember()]
        public string dfa_dl { get; set; }

        [DataMember()]
        public string onset { get; set; }

        [DataMember()]
        public bool is_dta { get; set; }

        [DataMember()]
        public bool is_dt { get; set; }

        [DataMember()]
        public bool is_ade { get; set; }

        [DataMember()]
        public bool is_di { get; set; }

        [DataMember()]
        public bool is_dtf { get; set; }

        [DataMember()]
        public bool is_dtd { get; set; }
    }

    [DataContract()]
    public class AlertRequest 
    {
        [DataMember()]
        public string gender { get; set; }

        [DataMember()]
        public DateTime dob { get; set; }

        [DataMember()]
        public List<string> mx { get; set; }

        [DataMember()]
        public List<string> rx { get; set; }

        [DataMember()]
        public List<dxItem> dxl { get; set; }

        [DataMember()]
        public List<string> al { get; set; }

        [DataMember()]
        public bool dta_by_al { get; set; }

        [DataMember()]
        public AlertFilters filters { get; set; }

        public AlertRequest()
        {
            this.mx = new List<string>();
            this.rx = new List<string>();
            this.dxl = new List<dxItem>();
            this.al = new List<string>();
            this.filters = new AlertFilters();
        }

        public AlertRequest(bool UseAllergy)
        {
            this.mx = new List<string>();
            this.rx = new List<string>();
            this.dxl = new List<dxItem>();
            this.al = new List<string>();
            this.filters = new AlertFilters();
            this.dta_by_al = UseAllergy;
        }

        public void Dispose()
        {

        }
    }

    [DataContract()]
    public class AlertResponse
    {
        [DataMember()]
        public List<AlertMessage> ade { get; set; }

        [DataMember()]
        public List<AlertMessage> di { get; set; }

        [DataMember()]
        public List<AlertMessage> dta { get; set; }

        [DataMember()]
        public List<AlertMessage> dt { get; set; }

        [DataMember()]
        public List<AlertMessage> dtf { get; set; }

        [DataMember()]
        public List<AlertMessage> dtd { get; set; }

    }


    [DataContract()]
    public class AlertMessage
    {
        [DataMember()]
        public string drug { get; set; }

        [DataMember()]
        public string response { get; set; }
    }


    [DataContract()]
    public class dxItem : RequestBase
    {
        [DataMember()]
        public int type { get; set; }

        [DataMember()]
        public string dx { get; set; }

        public dxItem(int Type, string Dx)
        {
            this.type = Type;
            this.dx = Dx;
        }
    }

    [DataContract()]
    public class dx : IDisposable
    {
        [DataMember()]
        public string code { get; set; }

        [DataMember()]
        public int type { get; set; }

        public void Dispose() { }
    }


    //[DataContract()]
    //public class a_base : IDisposable
    //{
    //    [DataMember()]
    //    public bool call { get; set; }

    //    public void Dispose() { }
    //}

    //[DataContract()]
    //public class a_di : a_base
    //{
    //    [DataMember()]
    //    public string sev { get; set; }

    //    [DataMember()]
    //    public string dl { get; set; }
    //}

    //[DataContract()]
    //public class a_dfa : a_base
    //{
    //    [DataMember()]
    //    public string sev { get; set; }

    //    [DataMember()]
    //    public string dl { get; set; }
    //}

    //[DataContract()]
    //public class a_ade : a_base
    //{
    //    [DataMember()]
    //    public string sev { get; set; }

    //    [DataMember()]
    //    public string onset { get; set; }
    //}

    //[DataContract()]
    //public class a_filter : IDisposable
    //{
    //    public a_base di { get; set; }
    //    public a_base par { get; set; }
    //    public a_base prc { get; set; }
    //    public a_dfa dfa { get; set; }
    //    public a_base dt { get; set; }
    //    public a_ade ade { get; set; }

    //    public void Dispose() { }
    //}

    //[DataContract()]
    //public class DIAlertRequest : IDisposable
    //{
    //    [DataMember()]
    //    public List<string> rxl { get; set; }

    //    [DataMember()]
    //    public List<string> mxl { get; set; }

    //    [DataMember()]
    //    public List<string> al { get; set; }

    //    [DataMember()]
    //    public List<dx> dxl { get; set; }

    //    public DIAlertRequest()
    //    {
    //        this.rxl = new List<string>();
    //        this.mxl = new List<string>();
    //        this.al = new List<string>();
    //        this.dxl = new List<dx>();
    //    }

    //    public void Dispose()
    //    {
    //    }
    //}

}
