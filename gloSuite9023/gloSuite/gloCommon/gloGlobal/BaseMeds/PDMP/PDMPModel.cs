using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMP = gloGlobal.PDMP.XSD;
using System.ComponentModel;


namespace gloGlobal.PDMP.Meds
{
    [Serializable]
    public class PDMPReportRequest : IDisposable
    {
        public PDMPReportRequest() 
        {             
        }
        
        public string ReportID { get; set; }
                
        public PMP.ReportRequestType ReportRequest { get; set; }

        public PDMPReportRequest(string ReportID, PMP.ReportRequestType ReportRequest)
        {
            this.ReportID = ReportID;
            this.ReportRequest = ReportRequest;
        }

        public void Dispose()
        {
          
            this.ReportID = null;
            this.ReportRequest = null;
           
        }
    }

    public class NARXScores : INotifyPropertyChanged
    {
        #region Variables
        private string _narcotic;
        private bool _Loading;
        private bool _NoContent;
        private bool _HasContent;
        private string _sedative;
        private string _overdose;
        private string _stimulant; 
        #endregion

        public NARXScores() 
        { 
            this.Loading = true;            
        }

        #region "Properties"
        public bool Loading
        {
            get { return _Loading; }
            set { _Loading = value; this.NotifyPropertyChanged("Loading"); }
        }

        public bool NoContent
        {
            get { return _NoContent; }
            set { _NoContent = value; this.NotifyPropertyChanged("NoContent"); }
        }

        public bool HasContent
        {
            get { return _HasContent; }
            set { _HasContent = value; this.NotifyPropertyChanged("HasContent"); }
        }

        public string Narcotic
        {
            get { return _narcotic; }
            set { _narcotic = value; this.SetHasContent(); this.NotifyPropertyChanged("Narcotic"); }
        }

        public string Sedative
        {
            get { return _sedative; }
            set { _sedative = value; this.SetHasContent(); this.NotifyPropertyChanged("Sedative"); }
        }

        public string Stimulant
        {
            get { return _stimulant; }
            set { _stimulant = value; this.SetHasContent(); this.NotifyPropertyChanged("Stimulant"); }
        }

        public string Overdose
        {
            get { return _overdose; }
            set { _overdose = value; this.SetHasContent(); this.NotifyPropertyChanged("Overdose"); }
        } 
        #endregion
      
        private void SetHasContent()
        {
            List<string> lstValues = new List<string>();
            lstValues.Add(Narcotic);
            lstValues.Add(Sedative);
            lstValues.Add(Stimulant);
            lstValues.Add(Overdose);

            this.HasContent = lstValues != null && lstValues.Any(p => p != null && 
                                                                                    p is string 
                                                                                    && !string.IsNullOrEmpty(p) 
                                                                                    && !string.IsNullOrWhiteSpace(p) 
                                                                                    && Convert.ToString(p).Length > 0);            

            lstValues.Clear();
            lstValues = null;
        }

        #region "Notify Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }   
        #endregion
    }
}
