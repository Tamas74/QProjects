using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace gloRxHub.FS3
{
    [DataContract()]
    public class ServiceParameters 
    {
        [DataMember()]
        public string sid { get; set; }

        [DataMember()]
        public string fid { get; set; }

        [DataMember()]
        public string copid { get; set; }

        [DataMember()]
        public string covid { get; set; }
    }

    [DataContract()]
    public class DrugFormularyParameter
    {
        [DataMember()]
        public ServiceParameters serviceParameters { get; set; }

        [DataMember()]
        public DrugParameter drugParameter { get; set; }

        public DrugFormularyParameter()
        {
            this.serviceParameters = new ServiceParameters();
            this.drugParameter = new DrugParameter();
        }
    }

    [DataContract()]
    public class DrugListFormularyParameter
    {
        [DataMember()]
        public ServiceParameters serviceParameters { get; set; }

        [DataMember()]
        public List<DrugParameter> drugListParameter { get; set; }

        public DrugListFormularyParameter()
        {
            this.serviceParameters = new ServiceParameters();
            this.drugListParameter = new List<DrugParameter>();
        }
    }

    [DataContract()]
    public class DrugParameter
    {
        [DataMember()]
        public Int64 ddid { get; set; }

        [DataMember()]
        public string rxtype { get; set; }

        public DrugParameter() { }

        public DrugParameter(Int64 DDID, string RxType)
        {
            this.ddid = DDID;
            this.rxtype = RxType;
        }
    }
}
