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
using gloUIControlLibrary.Classes;
using gloUIControlLibrary.Classes.ICD10;
using gloUIControlLibrary.Classes.ICD10.ICD9To10Mapping;
using System.Xml.Linq;
using System.Xml;
using System.Data;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace gloUIControlLibrary.WPFForms
{
    /// <summary>
    /// Interaction logic for frmCodeGuide.xaml
    /// </summary>
    public partial class frmCodeGuide : Window
    {

        #region " Variable Declaration "

        private static frmCodeGuide ofrmCodeGuide = null;

        #endregion " Variable Declaration "        

        #region Constructor

        public static frmCodeGuide CheckInstance()
        { return ofrmCodeGuide; }

        public static frmCodeGuide GetCodeGuideInstance(string databaseConnectionString, string icd9Code)
        {
            if (ofrmCodeGuide == null)
            { ofrmCodeGuide = new frmCodeGuide(icd9Code,databaseConnectionString); }

            return ofrmCodeGuide;
        }

        private frmCodeGuide(string icd9Code,string ConnectionString)
        {
            InitializeComponent();
            this.ConnectionString = ConnectionString;
            this.dblayer = new clsICD9To10MappingDBLayer(ConnectionString);
            this.gloCodingRules.DBConnectionString = ConnectionString;
            this.ICD9Code = icd9Code;

            this.gloICD9To10MappingTreeView.CodeSelected += new WPFUserControl.ICD10.ICD9To10MappingTreeView.ICD10CodeSelectedEvent(gloICD9To10MappingTreeView_CodeSelected);
            this.gloICD10DataSetTreeView.SelectedItemChanged += new WPFUserControl.ICD10.ICD10DataSetTreeView.Itemchanged(gloICD10DataSetTreeView_SelectedItemChanged);

            this.ICD10CodesGrid.ICD9CodeSelected += new WPFUserControl.ICD10.ICD10CodesGrid.codeSelected(ICD10CodesGrid_ICD9CodeSelected);

            searchTimer = new DispatcherTimer();
            searchTimer.Interval = new TimeSpan(0, 0, 0, 0, 450);
            searchTimer.Tick += new EventHandler(searchTimer_Tick);

            if (ICD9Code.Trim().Length > 0)
            { this.LoadMappingCodes(ICD9Code); }

        }        
        //public frmCodeGuide(string ICD9Code, string ConnectionString)
        //    : this(ConnectionString)
        //{ this.ICD9Code = ICD9Code; } 

        #endregion

        #region Properties and Constructor
        DispatcherTimer searchTimer = null;

        public string ConnectionString { get; set; }
        private string ICD9Code { get; set; }
       
        private ICD9To10MappingContainer ICD9To10MappingContainer = null;
        private clsICD9To10MappingDBLayer dblayer = null;

        #endregion

        #region Event Handlers

        private void ToggleCodingRulesVisibility()
        {
            try
            {             
                if (this.gloCodingRules.NoData)
                { this.gloCodingRules.Visibility = System.Windows.Visibility.Collapsed; }
                else
                { this.gloCodingRules.Visibility = System.Windows.Visibility.Visible; }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
          
        }

        private void gloICD10DataSetTreeView_SelectedItemChanged(DataRow DataRow)
        {
            Cursor currentCursor = Mouse.OverrideCursor;

            try
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                string sICD10Code = DataRow["sICDCode"].ToString();
                string sDescription = DataRow["sDescriptionLong"].ToString();

                if (sICD10Code.Length >= 4 && !sICD10Code.Contains("."))
                { sICD10Code = sICD10Code.Insert(2, "."); }

                this.gloCodingRules.ICD10Code = sICD10Code;
                this.gloCodingRules.ICD10Description = sDescription;
                this.txtCodeI.Text = sICD10Code;

                this.gloCodingRules.LoadNotes(true);

                this.ToggleCodingRulesVisibility();                
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
            finally
            { 
                Mouse.OverrideCursor = currentCursor;
                currentCursor = null;
            }
        }

        private void gloICD9To10MappingTreeView_CodeSelected(string Code)
        {
            Cursor currentCursor = Mouse.OverrideCursor;

            try
            {              
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                this.gloICD10DataSetTreeView.DataContext = dblayer.GetCategoryTreeSource(Code);

                if (Code.Length >= 4 && !Code.Contains("."))
                { Code = Code.Insert(3, "."); }

                this.txtCodeI.Text = Code;
                this.txtCodeII.Text = Code;

                this.gloCodingRules.ICD10Code = Code;
                this.gloCodingRules.LoadNotes(true);

                this.ToggleCodingRulesVisibility();               

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

        #region Load 9 To 10 Mapping Structure
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
        #endregion

        #region Form Closing
        
        public void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            { this.Close(); }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                ofrmCodeGuide = null;
                this.Dispose();
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        }

        #endregion

        #region Disposal
        private void Dispose()
        {
            try
            {
                this.gloICD9To10MappingTreeView.CodeSelected -= gloICD9To10MappingTreeView_CodeSelected;
                this.gloICD10DataSetTreeView.SelectedItemChanged -= gloICD10DataSetTreeView_SelectedItemChanged;
                this.ICD10CodesGrid.ICD9CodeSelected -= ICD10CodesGrid_ICD9CodeSelected;
                this.searchTimer.Tick -= searchTimer_Tick;

                this.gloICD9To10MappingTreeView = null;
                this.gloICD10DataSetTreeView = null;
                this.ICD10CodesGrid = null;
                this.searchTimer = null;

                if (this.ICD9To10MappingContainer != null)
                {
                    this.ICD9To10MappingContainer.Dispose();
                    this.ICD9To10MappingContainer = null;
                }

            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        } 
        #endregion

        #region Search Reset
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

        #region Search Focus Change
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
        #endregion

        #region Search Got Focus
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
                                         
    }
}
