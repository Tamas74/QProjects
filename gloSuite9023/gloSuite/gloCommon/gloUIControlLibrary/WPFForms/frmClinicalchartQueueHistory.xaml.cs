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
using System.Data.SqlClient;
using System.Data;
using gloUIControlLibrary.Classes.ClinicalChartQueue;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace gloUIControlLibrary.WPFForms
{
    /// <summary>
    /// Interaction logic for frmClinicalchartQueueHistory.xaml
    /// </summary>
    public partial class frmClinicalchartQueueHistory : Window
    {
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        private static extern IntPtr GetForegroundWindow();

        Int64 nQueueID = 0;
        string sConnectionString = string.Empty;
           
        public static System.IntPtr ChartQueueHistoryHandle = IntPtr.Zero;
        public static System.IntPtr PropChartQueueHistoryHandle
        {
            get
            {
                return ChartQueueHistoryHandle;
            }
            set
            {
                ChartQueueHistoryHandle = value;
            }
        }


        public frmClinicalchartQueueHistory()
        {
            InitializeComponent();
        }

        public frmClinicalchartQueueHistory(Int64 nQueueID, string sConnectionString) : this()
        {
            this.nQueueID = nQueueID;
            this.sConnectionString = sConnectionString;


            DataTable dt = null;
            clsClinicalChartDBLayer oclsClinicalChartDBLayer = new clsClinicalChartDBLayer(sConnectionString);
            dt = oclsClinicalChartDBLayer.GetQueueHistory(nQueueID);
            dgQueue.ItemsSource = dt.DefaultView; 
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ChartQueueHistoryHandle = GetForegroundWindow();
        }
    }
}
