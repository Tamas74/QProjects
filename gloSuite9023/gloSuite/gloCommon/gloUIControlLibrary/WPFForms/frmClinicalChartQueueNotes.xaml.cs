using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data ;
using gloUIControlLibrary.Classes.ClinicalChartQueue;
using System.Collections.ObjectModel;
using gloUIControlLibrary.Classes.ICD10;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace gloUIControlLibrary.WPFForms
{
    /// <summary>
    /// Interaction logic for frmClinicalChartQueueNotes.xaml
    /// </summary>
    public partial class frmClinicalChartQueueNotes : Window
    {
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        private static extern IntPtr GetForegroundWindow();

        private string _MessageBoxCaption = String.Empty;        
        private clsClinicalChartDBLayer DBLayer = null;
        
        public Int64 PatientID { get; set; }
        public Int64 QueueID { get; set; }

        public Int64 nAssociatedContactID { get; set; }
        public string Notes { get; set; }
        public string SelectedPrinter { get; set; }

        private string UserName;
        private Int64 UserID;

        public bool SaveAndClose { get; set; }
        public bool isClose { get; set; }

        public static System.IntPtr ChartQueueNotesHandle = IntPtr.Zero;
        public static System.IntPtr PropChartQueueNotesHandle
        {
            get
            {
                return ChartQueueNotesHandle;
            }
            set
            {
                ChartQueueNotesHandle = value;
            }
        }

        private List<clsPatientInsurance> lstPatientInsurance { get; set; }



        private string ConnectionString  ="";
        public frmClinicalChartQueueNotes(Int64 PatientID, 
                                            Int64 QueueID, 
                                                string UserName, 
                                                    Int64 UserID, 
                                                        string MessageBoxCaption,
                                                            string ConnectionString,
                                                                Boolean HideInsurancePanel = false,
                                                                    string PrinterName="")
        {
            InitializeComponent();
            
            this._MessageBoxCaption = MessageBoxCaption;
            this.ConnectionString = ConnectionString;
            
            this.PatientID = PatientID;
            this.QueueID = QueueID;

            this.UserName = UserName;
            this.UserID = UserID;
            this.SelectedPrinter = PrinterName;

            this.DBLayer = new clsClinicalChartDBLayer(this.ConnectionString);
            
            if (HideInsurancePanel == false)
            {
                this.lstPatientInsurance = this.LoadInsurance();
                this.dgInsurancePlans.DataContext = lstPatientInsurance;
                LoadPrinter();
                if (cmbPrinter.Items.Count > 0)
                {
                    cmbPrinter.SelectedItem = SelectedPrinter;
                }
            }
            else
            {
                pnlInsurancePlan.Visibility = System.Windows.Visibility.Collapsed;
                pnlClaim.Visibility = System.Windows.Visibility.Collapsed;
            }
            txtNotes.Focus();
        }

        

        private void btnSaveandClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Notes = txtNotes.Text;
                if (pnlClaim.Visibility!=System.Windows.Visibility.Collapsed)
                {
                    if (cmbPrinter.Text == "")
                    {
                        MessageBoxResult msgResult=MessageBox.Show("You have not selected any printer. So print setting for default printer will be load.", _MessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (msgResult == MessageBoxResult.No)
                            return;
                    }
                    SelectedPrinter = cmbPrinter.Text;
                }
                
                
                if (dgInsurancePlans.ItemsSource != null)
                {
                    if (dgInsurancePlans.ItemsSource is List<clsPatientInsurance>)
                    {
                        List<clsPatientInsurance> lstInsurance = (List<clsPatientInsurance>)dgInsurancePlans.ItemsSource;
                        if (lstInsurance.Any(p => p.Selected))
                        {
                            nAssociatedContactID = lstInsurance.FirstOrDefault(p => p.Selected).ContactID;
                        }
                        else
                        {
                            if (pnlInsurancePlan.Visibility != System.Windows.Visibility.Collapsed)
                            {
                                MessageBox.Show("Select Insurance Plan ", _MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                        }
                        lstInsurance = null;
                    }
                }
                else
                {
                    if (pnlInsurancePlan.Visibility != System.Windows.Visibility.Collapsed)
                    {
                        MessageBox.Show("Select Insurance Plan ", _MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                this.SaveAndClose = true;
                this.isClose = true;
                this.Close();

                //DBLayer.InsertNotes(QueueID, sNotes, nAssociatedContactID, this.UserName, this.UserID, enumNoteStatus.Queue,"");
              
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.Message, true);
            }            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.SaveAndClose = false;
            this.isClose = true;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
      
        private List<clsPatientInsurance> LoadInsurance()
        {
            List<clsPatientInsurance> lstInsurance = new List<clsPatientInsurance>();

            try
            {
                lstInsurance = DBLayer.GetPatientInsurancePlan(this.PatientID).AsEnumerable()
                                          .Select(
                                          p => new clsPatientInsurance()
                                          {
                                              ContactID = Convert.ToInt64(p["ContactID"]),
                                              InsuranceName = Convert.ToString(p["InsuranceName"]),
                                              Status = Convert.ToString(p["Status"]),
                                              SubscriberID = Convert.ToString(p["SubscriberID"]),
                                              Address = Convert.ToString(p["AddressLine1"]),
                                              AddressTwo = Convert.ToString(p["AddressLine2"]),
                                              City = Convert.ToString(p["City"]),
                                              State = Convert.ToString(p["State"]),
                                              ZipCode = Convert.ToString(p["ZipCode"]),
                                              Phone = Convert.ToString(p["Phone"]),
                                              Fax = Convert.ToString(p["Fax"]),
                                              Selected = false,
                                          }
                                          ).ToList();

                if (lstInsurance.Count == 1)
                {
                    lstInsurance[0].Selected = true;
                }

                return lstInsurance;
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.Message, true);
                return lstPatientInsurance;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgInsurancePlans.SelectedValue is clsPatientInsurance)
                {
                    clsPatientInsurance patientInsurance = (clsPatientInsurance)dgInsurancePlans.SelectedValue;

                    patientInsurance.Selected = true;
                }
            }
            catch (Exception Ex)
            { 
                LogException.ExceptionLog(Ex.Message, true);                
            }           
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (dgInsurancePlans.SelectedValue is clsPatientInsurance)
                //{
                //    clsPatientInsurance patientInsurance = (clsPatientInsurance)dgInsurancePlans.SelectedValue;

                //    patientInsurance.Selected = false;
                //}

                if (dgInsurancePlans.ItemsSource != null)
                {
                    if (dgInsurancePlans.ItemsSource is List<clsPatientInsurance>)
                    {
                        List<clsPatientInsurance> lstInsurance = (List<clsPatientInsurance>)dgInsurancePlans.ItemsSource;

                        foreach (clsPatientInsurance element in lstInsurance)
                        {
                            element.Selected = false;
                        }

                        lstInsurance = null;
                    }
                }

            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.Message, true);
            }                 
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
              // this.SaveAndClose = false;
                this.isClose = true;
                this.DBLayer = null;

                if (this.dgInsurancePlans != null)
                { this.dgInsurancePlans.DataContext = null; }

                if (this.lstPatientInsurance != null)
                {
                    foreach (clsPatientInsurance element in this.lstPatientInsurance)
                    { element.Dispose(); }

                    this.lstPatientInsurance.Clear();
                    this.lstPatientInsurance = null;
                }
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.Message, true);
            }                 
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ChartQueueNotesHandle = GetForegroundWindow();
        }

        private void LoadPrinter()
        {
            try
            {
                DataTable dtClaimPrinter = this.DBLayer.GetClaimPrinterList();
                foreach (DataRow dr in dtClaimPrinter.Rows)
                {
                    cmbPrinter.Items.Add(Convert.ToString(dr["Printer Name"]));
                }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        }

        private void cmbPrinter_DropDownOpened(object sender, EventArgs e)
        {
            this.Topmost = true;
            this.Topmost = false;
        }
    }
}
