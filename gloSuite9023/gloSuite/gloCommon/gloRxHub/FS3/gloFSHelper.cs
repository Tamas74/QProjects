using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloRxHub.FS3
{
    public class gloFSHelper
    {
        public string ServiceURL { get; set; }

        public gloFSHelper(string FormularyURL)
        {
            ServiceURL = FormularyURL;
        }

        public DrugFormularyInfo GetDrugFormularyInfo(DrugFormularyParameter Parameters)
        {
            BaseServiceHelper<DrugFormularyInfo> helper = new BaseServiceHelper<DrugFormularyInfo>(ServiceURL);
            return helper.GetResponse(Parameters, URLOption.GetDrugFormularyInfo); ;
        }

        public List<DrugFormularyInfo> GetDrugListFormularyInfo(DrugListFormularyParameter Parameters)
        {
            BaseServiceHelper<List<DrugFormularyInfo>> helper = new BaseServiceHelper<List<DrugFormularyInfo>>(ServiceURL);
            return helper.GetResponse(Parameters, URLOption.GetDrugListFormularyInfo); ;
        }
    }
}
