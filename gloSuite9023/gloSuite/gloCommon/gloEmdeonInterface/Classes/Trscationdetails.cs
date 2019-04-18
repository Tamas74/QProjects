using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloEmdeonInterface.Classes
{
    public class Trscationdetails
    {
        public Int32 SrNo { get; set; }
        public Int64 nmemberID { get; set; }
        public DateTime dtDownloaded { get; set; }
        public string CustomFormID { get; set; }
        public string sFormName { get; set; }
        public string TrscationType { get; set; }

        public Trscationdetails(Int32 SrNo, Int64 nmemberID, DateTime dtDownloaded, string CustomFormID, string sFormName, string TrscationType)
        {
            this.SrNo = SrNo;
            this.nmemberID = nmemberID;
            this.dtDownloaded = dtDownloaded;
            this.CustomFormID = CustomFormID;
            this.sFormName = sFormName;
            this.TrscationType = TrscationType;

        }

    }
}
