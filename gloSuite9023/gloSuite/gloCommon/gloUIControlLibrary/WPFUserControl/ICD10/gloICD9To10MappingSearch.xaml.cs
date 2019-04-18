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
using System.Windows.Threading;
using System.Data;
using gloUIControlLibrary.Classes.ICD10.ICD9To10Mapping;
using gloUIControlLibrary.Classes.ICD10;

namespace gloUIControlLibrary.WPFUserControl.ICD10
{
    /// <summary>
    /// Interaction logic for gloICD9To10MappingSearch.xaml
    /// </summary>
    public partial class gloICD9To10MappingSearch : UserControl
    {
        #region Private Variables
        DispatcherTimer searchTimer = null;
        private clsICD9To10MappingDBLayer dblayer = null;
        private ICD9To10MappingContainer ICD9To10MappingContainer = null; 
        #endregion

        #region Delegates
        public delegate void controlClose();
        public event controlClose ControlClosed;  
        #endregion

        #region Constructor
        public gloICD9To10MappingSearch(string ConnectionString)
        {
            InitializeComponent();
            searchTimer = new DispatcherTimer();
            searchTimer.Interval = new TimeSpan(0, 0, 0, 0, 450);
            searchTimer.Tick += new EventHandler(searchTimer_Tick);
            this.dblayer = new clsICD9To10MappingDBLayer(ConnectionString);

            this.ICD10CodesGrid.ICD9CodeSelected += new ICD10.ICD10CodesGrid.codeSelected(ICD10CodesGrid_ICD9CodeSelected);
        } 
        #endregion

        #region Load Codes
        public void LoadMappingCodes(string ICD9Code)
        {
            List<ICD9To10Mapping> ListOfMappingCodes = new List<ICD9To10Mapping>();

            try
            {
                ListOfMappingCodes = dblayer.Get9To10Mapping(ICD9Code);

                this.ICD9To10MappingContainer = new ICD9To10MappingContainer();
                this.ICD9To10MappingContainer.GenerateStructure(ListOfMappingCodes);

                this.gloICD9To10MappingTreeView.DataContext = this.ICD9To10MappingContainer;

            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }

            finally
            {
                if (ListOfMappingCodes != null)
                {
                    ListOfMappingCodes.Clear();
                    ListOfMappingCodes = null;
                }
            }
        } 
        #endregion

        #region Code Selected
        private void ICD10CodesGrid_ICD9CodeSelected(DataRow Datarow)
        {
            Cursor currentCursor = Mouse.OverrideCursor;

            try
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                this.popSearch.IsOpen = false;
                if (Datarow != null)
                {
                    string sICD9Code = Datarow["sICDCode"].ToString();
                    this.LoadMappingCodes(sICD9Code);
                    this.txtSearch.TextChanged -= txtSearch_TextChanged;
                    this.txtSearch.Text = sICD9Code;
                    this.txtSearch.TextChanged += txtSearch_TextChanged;

                }

            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
            finally
            {
                Mouse.OverrideCursor = currentCursor;
                currentCursor = null;
            }
        } 
        #endregion

        #region Clear Search
        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.txtSearch.Focus();
                this.txtSearch.Text = string.Empty;

            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        }  
        #endregion

        #region Search Functionality

        private void searchTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                searchTimer.Stop();
                DataTable dtICD9Codes = null;
                dtICD9Codes = dblayer.Get9Codes(txtSearch.Text.Trim());
                this.popSearch.IsOpen = false;

                if (dtICD9Codes.Rows.Count > 0)
                {
                    this.ICD10CodesGrid.DataContext = dtICD9Codes;
                    this.popSearch.IsOpen = true;
                    this.ICD10CodesGrid.SetFocusToFirstRow();
                }

            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (searchTimer != null)
                {
                    searchTimer.Stop();
                    searchTimer.Start();
                }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }

        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Down)
                { this.ICD10CodesGrid.SetFocus(); }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }

        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (searchTimer != null)
                {
                    searchTimer.Stop();
                    searchTimer.Start();
                }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        } 
        #endregion

        #region Close Control
        
        private void btnCloseMapping_Click(object sender, RoutedEventArgs e)
        {
            if (this.ControlClosed != null)
            { this.ControlClosed(); }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            try { this.ICD10CodesGrid.ICD9CodeSelected -= new ICD10.ICD10CodesGrid.codeSelected(ICD10CodesGrid_ICD9CodeSelected); }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        }

        #endregion
       
    }
}
