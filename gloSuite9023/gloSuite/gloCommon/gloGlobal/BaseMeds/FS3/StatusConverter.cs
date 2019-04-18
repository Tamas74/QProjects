using System;
using System.Drawing;

namespace gloGlobal.FS3
{
    public class StatusConverter : IDisposable
    {
        public string StatusCode { get; set; }

        public string DisplayText
        {
            get
            {
                return FormularyStatusDisplay();
            }
        }

        public Bitmap DisplayIcon
        {
            get
            {
                return FormularyStatusIcon();
            }
        }

        public StatusConverter(string Code)
        {
            this.StatusCode = Code;
        }

        private string FormularyStatusDisplay()
        {
            string displaytext = "Unknown";
            try
            {
                Int32 fs = -1;
                Int32 pl = 0;

                if (Int32.TryParse(StatusCode, out fs) && fs >= 0)
                {
                    if (fs == 0)
                    { displaytext = "Non Reimbursable"; }
                    else if (fs == 1)
                    { displaytext = "Non Formulary"; }
                    else if (fs == 2)
                    { displaytext = "On Formulary"; }
                    else if (fs >= 3 && fs <= 99)
                    { pl = fs - 2; displaytext = "On Formulary PL-" + Convert.ToString(pl); }
                }
                return displaytext;
            }
            catch
            {
                return "";
            }
        }

        private Bitmap FormularyStatusIcon()
        {
            Bitmap displayIcon = global::gloGlobal.Properties.Resources.U;
            Int32 fs = -1;

            if (Int32.TryParse(StatusCode, out fs) && fs >= 0)
            {
                if (fs == 0)
                { displayIcon = global::gloGlobal.Properties.Resources.Not_Reimbursable; }
                else if (fs == 1)
                { displayIcon = global::gloGlobal.Properties.Resources.Off_Formulary; }
                else if (fs >= 2 && fs <= 99)
                { displayIcon = global::gloGlobal.Properties.Resources.On_Formulary; }
            }
            return displayIcon;
        }

        public void Dispose()
        {
            this.StatusCode = null;
            this.DisplayIcon.Dispose();
        }
    }
}
