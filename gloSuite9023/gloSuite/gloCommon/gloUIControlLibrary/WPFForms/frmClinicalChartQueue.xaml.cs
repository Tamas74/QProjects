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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using gloUIControlLibrary.Classes.ClinicalChartQueue;
using gloUIControlLibrary.Classes.ICD10;
using System.Windows.Interop;
using System.Runtime.InteropServices;






namespace gloUIControlLibrary.WPFForms
{
    /// <summary>
    /// Interaction logic for frmClinicalChartQueue.xaml
    /// </summary>
    public partial class frmClinicalChartQueue : Window
    {
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        private static extern IntPtr GetForegroundWindow(); 

        public delegate void GetQueuedata();
        public event GetQueuedata EvnMsgQueue;

        public string ConnectionString { get; set; }
        public Boolean isClosed { get; set; }
        public long QueueId { get; set; }
        public long PatientID { get; set; }
        public long ContactID { get; set; }
        public DateTime QueueFromDate { get; set; }
        public DateTime QueueToDate { get; set; }
        public string QueuePrinter { get; set; }


        public clsGroupedQueue groupedQueue { get; set; }
        //ICollectionView view;
        
        public delegate void PrintClick(Object Sender, EventArgs e);
        public event PrintClick Print_Click;

        //public frmClinicalChartQueueNotes oNotes;
        public delegate void ViewClick(Int64 PatientID,Int64 QueueId,Int64 ContactId,DateTime dtFromDate,DateTime dtToDate);
        public event ViewClick View_Click;


        clsClinicalChartDBLayer DBLayer = null;
        DataSet dsQueue = new DataSet();
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _username = string.Empty;
        private static frmClinicalChartQueue ofrmClinicalChartQueue = null;

        public static System.IntPtr ChartQueueHandle = IntPtr.Zero;
        public static System.IntPtr PropChartQueueHandle
        {
            get
            {
                return ChartQueueHandle;
            }
            set
            {
                ChartQueueHandle = value;
            }
        }

        public static frmClinicalChartQueue CheckInstance()
        { return ofrmClinicalChartQueue; }


        public static frmClinicalChartQueue GetClinicalChartQueueInstance(string databaseConnectionString)
        {
            if (ofrmClinicalChartQueue == null)
            { ofrmClinicalChartQueue = new frmClinicalChartQueue(databaseConnectionString); }

            return ofrmClinicalChartQueue;
        }

        private frmClinicalChartQueue() 
        { InitializeComponent(); }
             

      
        public frmClinicalChartQueue(string ConnectionString) : this()
        {
            this.ConnectionString = ConnectionString;
            this.DBLayer = new clsClinicalChartDBLayer(this.ConnectionString);
            this.groupedQueue = new clsGroupedQueue();
            LoadFilters();
            GetQueueData(Convert.ToDateTime("1/1/1900"), DateTime.Today);
            groupedQueue = this.LoadQueue();
            dgQueue.ItemsSource = groupedQueue.ClinicalChartQueueList;
           
            RoutedCommand cmdView = new RoutedCommand();
            cmdView.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(cmdView, btnView_Click));
            
            RoutedCommand cmdClose = new RoutedCommand();
            cmdClose.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(cmdClose, btnClose_Click));

            RoutedCommand cmdRefresh = new RoutedCommand();
            cmdRefresh.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(cmdRefresh, btnRefresh_Click));

            RoutedCommand cmdHistory = new RoutedCommand();
            cmdHistory.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(cmdHistory, btnHistory_Click));

        }
      
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                ofrmClinicalChartQueue = null;
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        }




        private void LoadFilters()
        {
            try
            {
                foreach (var en in Enum.GetValues(typeof(enumPrintStatus)))
                {
                    if (Convert.ToString(en) != "None" && Convert.ToString(en) != "ReQueue")
                    {
                        cmbFilter.Items.Add(Convert.ToString(en));
                    }
                }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        }

        private DataSet GetQueueData(DateTime StartDate, DateTime EndDate)
        {
            
            try
            {
                dsQueue = this.DBLayer.LoadQueue(StartDate, EndDate);
                return dsQueue;
            }
            catch (Exception Ex)
            { 
                LogException.ExceptionLog(Ex.Message, true);
                return dsQueue;
            }
        }

        private clsGroupedQueue LoadQueue()
        {
            clsGroupedQueue returned = new clsGroupedQueue();
            try
            {
                if (dsQueue != null)
                {
                    //ComboBoxItem typeItem = (ComboBoxItem)cmbFilter.SelectedItem;
                    string QueueStatus = Convert.ToString(cmbFilter.SelectedItem);
                    List<clsPatientQueue> lstQueue = dsQueue.Tables[0]
                                                        .AsEnumerable()
                                                        .Where(p => Convert.ToString(p["nQueueStatus"]) == QueueStatus)
                                                        .Select(p => new clsPatientQueue()
                                                        {
                                                            QueueID = Convert.ToInt64(p["nQueueID"]),
                                                            QueueName = Convert.ToString(p["Queue#"]),
                                                            PatientID = Convert.ToInt64(p["nPatientID"]),
                                                            PatientCode = Convert.ToString(p["sPatientCode"]),
                                                            FirstName = Convert.ToString(p["sFirstName"]),
                                                            LastName = Convert.ToString(p["sLastName"]),
                                                            QueueStatus = Convert.ToString(p["nQueueStatus"]),
                                                            UserID = Convert.ToInt64(p["nUserID"]),
                                                            UserName = Convert.ToString(p["sUserName"]),
                                                            CreatedDateTime = Convert.ToDateTime(p["dtCreatedDateTime"]),
                                                            Notes = Convert.ToString(p["Notes"]),
                                                            ContactID = Convert.ToInt64(p["ContactId"]),
                                                            InsurancePlan = Convert.ToString(p["InsurancePlan"]),
                                                            QueueFromDate= Convert.ToDateTime(p["FromDate"]),
                                                            QueueToDate = Convert.ToDateTime(p["ToDate"]),
                                                            QueuePrinter = Convert.ToString(p["QueuePrinter"])
                                                        }).ToList();

                    //foreach (clsPatientQueue element in lstQueue)
                    //{
                    //    List<clsQueueDetail> lstQueueDetail = null;
                    //    lstQueueDetail = Dataset.Tables[1]
                    //                      .AsEnumerable()
                    //                      .Where(p => Convert.ToInt64(p["nQueueID"]) == element.QueueID)
                    //                      .Select(p => new clsQueueDetail()
                    //                      {
                    //                          DetailID = Convert.ToInt64(p["nDetailID"]),
                    //                          QueueID = Convert.ToInt64(p["nQueueID"]),
                    //                          enumQueueType = (enumDocType)Enum.Parse(typeof(enumDocType), Convert.ToString(p["sEnum"])),                                          
                    //                          TranID_I = Convert.ToString(p["sTranID_I"]),
                    //                          TranID_II = Convert.ToString(p["sTranID_II"]),
                    //                          TranID_III = Convert.ToString(p["sTranID_III"]),
                    //                          TranID_IV = Convert.ToString(p["sTranID_IV"]),
                    //                          TranID_V = Convert.ToString(p["sTranID_V"]),
                    //                          DisplayText = Convert.ToString(p["sDisplayText"]),
                    //                          PrintSequence = Convert.ToInt32(p["nPrintSequenceNo"])
                    //                      }
                    //                      ).ToList();
                    //    foreach (clsQueueDetail detailElement in lstQueueDetail.OrderBy(p => p.PrintSequence))
                    //    {
                    //        element.QueueDetails.Add(detailElement);
                    //    }
                    //}
                    returned.ClinicalChartQueueList.AddRange(lstQueue);
                    //groupedQueue = returned;
                }
                return returned;
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.Message, true);
                return returned;
            }

        }

        private void RefreshQueue()
        {
            try
            {
                if (dsQueue.Tables.Count > 0)
                {
                    dgQueue.ItemsSource = null;
                    groupedQueue = this.LoadQueue();
                    dgQueue.ItemsSource = groupedQueue.ClinicalChartQueueList;
                }
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.Message, true);
            }
        }

        private void SaveStatus(clsPatientQueue Queue, int oStatus)
        {
            try
            {
                frmClinicalChartQueueNotes oNotes = new frmClinicalChartQueueNotes(Queue.PatientID, Queue.QueueID, Queue.UserName, Queue.UserID, "", ConnectionString, true);
                oNotes.ShowDialog();
                string sNotes = "";
                if (oNotes.SaveAndClose)
                {
                    if (appSettings["UserName"] != null)
                    {
                        if (appSettings["UserName"] != "")
                        {
                            _username = Convert.ToString(appSettings["UserName"]);
                        }
                    }
                    sNotes = oNotes.Notes;
                    if (_username == "")
                    {
                        _username = Queue.UserName;
                    }
                    this.DBLayer.InsertQueueNotes(Queue.QueueID, oNotes.Notes, Queue.ContactID, _username, Queue.UserID, oStatus, oStatus);
                    GetQueueData(Convert.ToDateTime("1/1/1900"), DateTime.Today);
                    RefreshQueue();
                }
                //oNotes = null;
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.Message, true);
            }
        }


        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Print_Click != null)
                {
                    Print_Click(sender, e);
                }
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.Message, true);
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgQueue.SelectedItem != null)
                {
                    if (btnView.IsEnabled == true)
                    {
                        var drv = (clsPatientQueue)dgQueue.SelectedItem;
                        QueueId = Convert.ToInt64((drv.QueueID));
                        PatientID = Convert.ToInt64((drv.PatientID));
                        ContactID = Convert.ToInt64((drv.ContactID));
                        QueueFromDate = Convert.ToDateTime(drv.QueueFromDate);
                        QueueToDate = Convert.ToDateTime(drv.QueueToDate);
                        QueuePrinter = Convert.ToString(drv.QueuePrinter);
                        if (View_Click != null)
                        {
                            View_Click(PatientID, QueueId, ContactID, QueueFromDate, QueueToDate);
                            GetQueueData(Convert.ToDateTime("1/1/1900"), DateTime.Today);
                            RefreshQueue();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.Message, true);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            isClosed = true;
            this.Close();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetQueueData(Convert.ToDateTime("1/1/1900"), DateTime.Today);
                RefreshQueue();
            }
            catch (Exception ex)
            {
                LogException.ExceptionLog(ex.Message, true);
            }
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgQueue.SelectedItem != null)
                {
                    var drv = (clsPatientQueue)dgQueue.SelectedItem;
                    QueueId = Convert.ToInt64((drv.QueueID));
                    frmClinicalchartQueueHistory oHistory = new frmClinicalchartQueueHistory(QueueId, this.ConnectionString);
                    System.Windows.Interop.WindowInteropHelper _interophelper = new System.Windows.Interop.WindowInteropHelper(oHistory);
                    _interophelper.Owner = new WindowInteropHelper(this).Handle;
                    oHistory.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LogException.ExceptionLog(ex.Message, true);
            }
           
        }



   
        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                RefreshQueue();
              
                cmnu.Height = Double.NaN;
                cmnu.Width = Double.NaN; 
                if (Convert.ToString(cmbFilter.SelectedItem) == Convert.ToString(enumPrintStatus.Queued))
                {
                    cmReQueue.Visibility = Visibility.Hidden;
                    cmProcessed.Visibility = Visibility.Visible;
                    cmCancel.Visibility = Visibility.Visible;
                    cmReQueue.Height = 0;
                    cmProcessed.Height = 25;
                    cmCancel.Height = 25;
                    btnView.IsEnabled = true;
                    btnView.Opacity = 1;
                    lblView.Opacity = 1;

                }
                else if (Convert.ToString(cmbFilter.SelectedItem) == Convert.ToString(enumPrintStatus.Processed))
                {
                    cmReQueue.Visibility = Visibility.Visible;
                    cmProcessed.Visibility = Visibility.Hidden;
                    cmCancel.Visibility = Visibility.Visible;
                    cmReQueue.Height = 25;
                    cmProcessed.Height = 0;
                    cmCancel.Height = 25;
                    btnView.IsEnabled = false;
                    btnView.Opacity = 0.25;
                    lblView.Opacity = 0.25;
                }
                if (Convert.ToString(cmbFilter.SelectedItem) == Convert.ToString(enumPrintStatus.Cancel))
                {
                    cmReQueue.Visibility = Visibility.Visible;
                    cmProcessed.Visibility = Visibility.Visible;
                    cmCancel.Visibility = Visibility.Hidden;
                    cmReQueue.Height = 25;
                    cmProcessed.Height = 25;
                    cmCancel.Height = 0;
                    btnView.IsEnabled = false;
                    btnView.Opacity = 0.25;
                    lblView.Opacity = 0.25;
                }
                if (Convert.ToString(cmbFilter.SelectedItem) == Convert.ToString(enumPrintStatus.ErrorProcessed))
                {
                    cmReQueue.Visibility = Visibility.Visible;
                    cmProcessed.Visibility = Visibility.Visible;
                    cmCancel.Visibility = Visibility.Visible;
                    cmReQueue.Height = 25;
                    cmProcessed.Height = 25;
                    cmCancel.Height = 25;
                    btnView.IsEnabled = false;
                    btnView.Opacity = 0.25;
                    lblView.Opacity = 0.25;
                }
                if (Convert.ToString(cmbFilter.SelectedItem) == Convert.ToString(enumPrintStatus.InProgress ))
                {
                    cmnu.Width = 0;
                    cmnu.Height = 0; 
                    btnView.IsEnabled = false ;
                    btnView.Opacity = 0.25;
                    lblView.Opacity = 0.25;

                }
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.Message, true);
            }
        }

        private void Update_Status(object sender, RoutedEventArgs e)
        {
            //Get the clicked MenuItem
            try
            {
                var menuItem = (MenuItem)sender;
                if (menuItem != null)
                {
                    var contextMenu = (ContextMenu)menuItem.Parent;
                    if (contextMenu != null)
                    {
                        var item = (DataGrid)contextMenu.PlacementTarget;
                        if (item != null)
                        {
                            var Queue = (clsPatientQueue)item.SelectedCells[0].Item;

                            if (menuItem.Name == "cmProcessed")
                            {
                                SaveStatus(Queue, enumPrintStatus.Processed.GetHashCode());

                            }
                            else if (menuItem.Name == "cmCancel")
                            {
                                SaveStatus(Queue, enumPrintStatus.Cancel.GetHashCode());
                            }
                            else if (menuItem.Name == "cmReQueue")
                            {
                                SaveStatus(Queue, enumPrintStatus.ReQueue.GetHashCode());
                            }

                           

                            if (EvnMsgQueue != null)
                            {
                                EvnMsgQueue();
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.Message, true);
            }

        }

        private void dgQueue_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DependencyObject dep = (DependencyObject)e.OriginalSource;
                // iteratively traverse the visual tree
                while ((dep != null) &&
                        !(dep is DataGridCell) &&
                        !(dep is DataGridColumnHeader))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                {
                    dgQueue.ContextMenu.Visibility = Visibility.Hidden;
                    dgQueue.ContextMenu.IsOpen = false;
                }
                if (dep is DataGridColumnHeader)
                {
                    dgQueue.ContextMenu.Visibility = Visibility.Hidden;
                    dgQueue.ContextMenu.IsOpen = false;
                }
                if (dep is DataGridCell)
                {
                    dgQueue.ContextMenu.IsOpen = false;
                    dgQueue.ContextMenu.Visibility = Visibility.Visible;
                    dgQueue.ContextMenu.IsOpen = true;
                    DataGridCell cell = dep as DataGridCell;
                    // navigate further up the tree
                    while ((dep != null) && !(dep is DataGridRow))
                    {
                        dep = VisualTreeHelper.GetParent(dep);
                    }
                    if (dep is DataGridRow)
                    {
                        DataGridRow row = dep as DataGridRow;
                        row.IsSelected = true;
                        dgQueue.SelectedItem = row;
                        dgQueue.ContextMenu.PlacementTarget = dgQueue;
                    }


                    //DataGrid daatgrid = sender as DataGrid;
                    //pt = e.GetPosition(daatgrid);
                    //dgQueue.ContextMenu.CustomPopupPlacementCallback = new CustomPopupPlacementCallback(placePopup);
                }
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.Message, true);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            isClosed = true;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ChartQueueHandle = GetForegroundWindow();
        }

        private void cmbFilter_DropDownOpened(object sender, EventArgs e)
        {
            this.Topmost = true;
            this.Topmost = false;
        }

       
 
      
        //public CustomPopupPlacement[] placePopup(Size popupSize,  Size targetSize, Point offset)
        //{
        //    CustomPopupPlacement placement1 = new CustomPopupPlacement(pt, PopupPrimaryAxis.Vertical);
        //    CustomPopupPlacement placement2 = new CustomPopupPlacement(pt, PopupPrimaryAxis.Horizontal);
        //    CustomPopupPlacement[] ttplaces = new CustomPopupPlacement[] { placement1, placement2 };
        //    return ttplaces;
        //}



        //[DllImport("user32.dll")]
        //private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        //[DllImport("user32.dll")]
        //private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        //private const int GWL_STYLE = -16;
        //private const int WS_MAXIMIZEBOX = 0x10000;
        //private void Window_SourceInitialized(object sender, EventArgs e)
        //{
        //    var hwnd = new WindowInteropHelper((Window)sender).Handle;
        //    var value = GetWindowLong(hwnd, GWL_STYLE);
        //    SetWindowLong(hwnd, GWL_STYLE, (int)(value & ~WS_MAXIMIZEBOX));
        //}

    }
}
