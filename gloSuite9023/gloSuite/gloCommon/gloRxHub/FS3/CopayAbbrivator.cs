using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloRxHub.FS3
{
    public class CopayAbbrivator : IDisposable
    {
        public string DisplayText
        {
            get
            {
                return AbbreviatedCopayDisplay();
            }
        }

        private List<CopayFactor> CopayFactors { get; set; }

        public CopayAbbrivator(List<CopayFactor> CopayList)
        {
            this.CopayFactors = CopayList;
        }

        private string AbbreviatedCopayDisplay()
        {
            string sReturned = string.Empty;
            StringBuilder sBuilder = null;
            IEnumerable<CopayFactor> sortedCopay = null;

            try
            {
                sBuilder = new StringBuilder();

                sortedCopay = from CopayFactor copay in this.CopayFactors
                              orderby copay.ptype == "A"
                              orderby copay.ptype == ""
                              orderby copay.ptype == "R"
                              orderby copay.ptype == "M"
                              orderby copay.ptype == "S"
                              orderby copay.ptype == "L"
                              select copay;

                foreach (CopayFactor cop in sortedCopay)
                {
                    if (cop.ptype.Trim() != "") { sBuilder.Append(cop.ptype + ": "); }
                    if (cop.flat.Trim() != "") { sBuilder.Append("$" + cop.flat + " "); }

                    if (cop.rate.Trim() != "")
                    {
                        if (cop.ptype == "M") { sBuilder.Append(" + "); }
                        sBuilder.Append((Convert.ToDecimal(cop.rate) * 100) + "% ");
                    }

                    if (cop.mincop.Trim() != "") { sBuilder.Append("($" + cop.mincop + "-"); }
                    if (cop.maxcop.Trim() != "") { sBuilder.Append("$" + cop.maxcop + ") "); }

                    if (cop.mintier.Trim() != "") { sBuilder.Append("Tier:(" + cop.mintier + " - "); }
                    if (cop.maxtier.Trim() != "") { sBuilder.Append(cop.maxtier + ") "); }

                    if (cop.days.Trim() != "") { sBuilder.Append(cop.days + "D "); }
                }

                sReturned = sBuilder.ToString();

                return sReturned;
            }
            catch
            {
                return "";
            }
            finally
            {
                if (sBuilder != null)
                {
                    sBuilder.Clear();
                    sBuilder = null;
                }
            }
        }

        public void Dispose()
        {
            this.CopayFactors = null;
        }
    }
}
