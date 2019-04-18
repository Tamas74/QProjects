using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Data;

namespace gloUIControlLibrary.Classes.ClinicalChartQueue
{
    public class clsGroupedQueue : IDisposable
    {
        #region Properties
        public List<clsPatientQueue> ClinicalChartQueueList { get; set; } 
        #endregion

        #region Constructor
        public clsGroupedQueue()
        {
            this.ClinicalChartQueueList = new List<clsPatientQueue>();
        } 
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (this.ClinicalChartQueueList != null)
            {
                foreach (clsPatientQueue element in this.ClinicalChartQueueList)
                {
                    element.Dispose();
                }

                this.ClinicalChartQueueList.Clear();
                this.ClinicalChartQueueList = null;
            }
        }
        #endregion
    }

    public class clsPatientQueue:IDisposable
    {
        #region Properties
        public Int64 QueueID { get; set; }
        public string QueueName { get; set; }
        public Int64 PatientID { get; set; }
        public String PatientCode { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String QueueStatus { get; set; }
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public String Notes { get; set; }
        public Int64 ContactID { get; set; }
        public String InsurancePlan { get; set; }
        public DateTime QueueFromDate { get; set; }
        public DateTime QueueToDate { get; set; }
        public string QueuePrinter { get; set; }

        public List<clsQueueDetail> QueueDetails { get; set; }

        private ICollectionView _viewBinding;
        public ICollectionView ViewBinding
        {

            get 
            {
                _viewBinding = CollectionViewSource.GetDefaultView(this.QueueDetails);
                _viewBinding.GroupDescriptions.Add(new PropertyGroupDescription("enumQueueType"));
                _viewBinding.SortDescriptions.Add(new SortDescription("PrintSequence", ListSortDirection.Ascending));
                return _viewBinding;
            }
            
        }

        #endregion

        #region Constructor
        public clsPatientQueue()
        {
            this.QueueDetails = new List<clsQueueDetail>();
            
        } 
        #endregion

        #region Dispose
        public void Dispose()
        {
            //if (this.QueueDetails != null)
            //{
            //    foreach (clsQueueDetail element in this.QueueDetails)
            //    {
            //        element.Dispose();
            //    }

            //    this.QueueDetails.Clear();
            //    this.QueueDetails = null;
            //}
        } 
        #endregion
    }

    public class clsQueueDetail:IDisposable
    {
        #region Properties
        public Int64 DetailID { get; set; }
        public Int64 QueueID { get; set; }

        public DateTime DateOfService { get; set; }
        public String DisplayText { get; set; }

        public enumDocType enumQueueType { get; set; }

        #region Transaction IDs
        public string TranID_I { get; set; }
        public string TranID_II { get; set; }
        public string TranID_III { get; set; }
        public string TranID_IV { get; set; }
        public string TranID_V { get; set; }
        #endregion

        public int PrintSequence { get; set; }

        #endregion

        #region Constructor
        public clsQueueDetail()
        {
            this.TranID_I = string.Empty;
            this.TranID_II = string.Empty;
            this.TranID_III = string.Empty;
            this.TranID_IV = string.Empty;
            this.TranID_V = string.Empty;

            this.DisplayText = string.Empty;
        } 
        #endregion

        #region Dispose
        public void Dispose()
        {

        } 
        #endregion
    }


    public class clsPatientInsurance : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Boolean selected;

        public Boolean Selected
        {
            get { return selected; }
            set
            {
                selected = value;

                if (PropertyChanged != null)
                { this.PropertyChanged(this, new PropertyChangedEventArgs("Selected")); }
            }
        }


        public Int64 ContactID { get; set; }
        public string InsuranceName { get; set; }
        public string Status { get; set; }

        public string SubscriberID { get; set; }
        public string Address { get; set; }
        public string AddressTwo { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public string Phone { get; set; }
        public string Fax { get; set; }


        public void Dispose()
        {
            this.InsuranceName = string.Empty;
            this.InsuranceName = null;

            this.Status = string.Empty;
            this.Status = null;

            this.SubscriberID = string.Empty;
            this.SubscriberID = null;

            this.Address = string.Empty;
            this.Address = null;

            this.AddressTwo = string.Empty;
            this.AddressTwo = null;

            this.City = string.Empty;
            this.City = null;

            this.State = string.Empty;
            this.State = null;

            this.ZipCode = string.Empty;
            this.ZipCode = null;

            this.Phone = string.Empty;
            this.Phone = null;

            this.Fax = string.Empty;
            this.Fax = null;

        }
    }
}
