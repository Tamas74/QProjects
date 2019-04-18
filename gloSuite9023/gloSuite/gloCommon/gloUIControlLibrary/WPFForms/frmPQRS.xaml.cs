using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Data; 
using gloUIControlLibrary.Classes.PQRS;
using System.Text;
using System;
using System.Runtime.InteropServices;
namespace gloUIControlLibrary.WPFForms.PQRS
{
    public partial class frmPQRS : Window
    {
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        private static extern IntPtr GetForegroundWindow();
        public static  DataTable _dtpqrs = null;
        public static DataTable _tempdtpqrs = null;
        StringBuilder sbPqrsCodes = null;
       public bool _IsSave   =false;
       private static frmPQRS frm;
       public bool _isClose = false;
       public static  System.IntPtr frmPQrsHandle = IntPtr.Zero;  
        public frmPQRS()
        {
           

            InitializeComponent();
            this.DataContext = PQRSViewModel.CreateQDSCodes();  
          
        }
        public static System.IntPtr PropfrmPQrsHandle
        {
            get
            {
            return frmPQrsHandle;
            }
            set
            {
                frmPQrsHandle = value;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            sbPqrsCodes = new StringBuilder();
            _IsSave = true; 
            foreach (  PQRSViewModel treechild  in this.tree.Items )
            {
                if (treechild.IsChecked!=null )
                {
                    if (treechild.IsChecked == true)
                    {
                        _isClose = true; 
                        this.Close();
                        return;
                    }
                
                   }
                foreach (PQRSViewModel tree in treechild.Children)
                    {
                        
                        if ((bool)tree.IsChecked == true)
                        {
                            sbPqrsCodes.Append("'" + tree.Code + "',");
 
                        }
                    }
               }

            if (sbPqrsCodes.ToString().Length > 1)
            {
                string strcodes = sbPqrsCodes.ToString().Substring(0, sbPqrsCodes.Length - 1);
                DataRow[] drr = _dtpqrs.Select("sCPTCode in(" + strcodes + ")");
                if (drr.Length > 0)
                {
                    _tempdtpqrs = drr.CopyToDataTable();
                    _dtpqrs.Rows.Clear();

                    _dtpqrs = _tempdtpqrs;
                    _tempdtpqrs.Dispose();
                    _tempdtpqrs = null; 
                }

            }
            else
            {
                _dtpqrs.Rows.Clear();
            }
            _isClose = true; 
            this.Close();
        }


        public static frmPQRS GetInstance()
        {

            try
            {
                if (frm != null)
                {

                    return frm;

                }
                else
                {
                    frm = new frmPQRS();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return frm;
        }
        public static frmPQRS CheckFormOpen()
        {
            try
            {
                if (frm == null)
                {
                    return null;
                }
                else
                {
                    return frm;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        
        private void Window_Loaded(object sender1, RoutedEventArgs e1)
        {
          
            PQRSViewModel root = this.tree.Items[0] as PQRSViewModel;
            root.IsChecked  = true; 
            //base.CommandBindings.Add(
            //   new CommandBinding(
            //       ApplicationCommands.Undo,
            //       (sender, e) => // Execute
            //       {
            //           e.Handled = true;
            //           root.IsChecked = false;
            //           this.tree.Focus();
            //       },
            //       (sender, e) => // CanExecute
            //       {
            //           e.Handled = true;
            //           e.CanExecute = (root.IsChecked != false);
            //       }));
          
          

            this.tree.Focus();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            _dtpqrs.Clear();
            _isClose = true; 
            this.Close();
        }

       

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
                if (_IsSave == false)
                {
                    if (_dtpqrs != null)
                    {
                        _dtpqrs.Clear();
                    }
                }
                if (frm != null)
                {

                    frm = null;
                }
           
           
}

        private void Window_Activated(object sender, EventArgs e)
        {
            frmPQrsHandle = GetForegroundWindow(); 
        
        }
        //Fixed issue #92309- WPF Window Disappears on Tooltip selection
        private void ContentPresenter_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            this.Topmost = true;
            this.Topmost = false;
        }
    }
}