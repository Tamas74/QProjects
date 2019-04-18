using System;
using System.Collections.Generic;
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
using gloUIControlLibrary.Classes.ICD10;
using System.Data;
using System.Linq;
using System.Windows.Threading;

namespace gloUIControlLibrary
{
	/// <summary>
	/// Interaction logic for ICDSubCodeControl.xaml
	/// </summary>
	public partial class ICDSubCodeControl : UserControl
	{

        #region Properties and Variables
        public string StartRange { get; set; }
        public string EndRange { get; set; }
        DispatcherTimer searchTimer = null;

        private List<clsICD10> lstAllCodes = null;
        private ICD10RangeCategory ICD10Range = null; //new ICD10RangeCategory();
      

        public delegate void codeRemovedFromCurrent();
        public event codeRemovedFromCurrent CodeRemovedFromMaster;

        public delegate void billableCodeAddedToCurrent();
        public event billableCodeAddedToCurrent CodeAddedToCurrent;

        public delegate void codeImported();
        public event codeImported CodeSelectedForImport;

        public delegate void masterCodeRemoved();
        public event masterCodeRemoved CodeRemovedFromICD10Master;

        public bool DisplayRedesignedForSmartTreatment { get; set; }
      
        public clsICD10 GetSelectedICDCode 
        {
            get
            {
                clsICD10 returned = null;

                try
                {
                    if (trvSubCode.SelectedItem != null && trvSubCode.SelectedItem is clsICD10)
                    { returned = (clsICD10)trvSubCode.SelectedItem; }
                }
                catch (Exception Ex)
                { throw Ex; }

                return returned;
            }
        }

        public delegate void searchFunctionFired(string Text);
        public event searchFunctionFired SearchFired;

        #endregion

        #region Constructor
        public ICDSubCodeControl()
        {
            this.InitializeComponent();
            try
            {
                this.lstAllCodes = new List<clsICD10>();
                this.ICD10Range = new ICD10RangeCategory();

                searchTimer = new DispatcherTimer();
                searchTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                searchTimer.Tick += new EventHandler(searchTimer_Tick);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        } 
        #endregion

        #region Disposal
        public void Dispose()
        {
            try
            {
                if (this.searchTimer != null)
                {
                    this.searchTimer.Tick -= searchTimer_Tick;
                    this.searchTimer = null;
                }

                this.RemoveAllNodes();
                this.lstAllCodes = null;
                this.ICD10Range = null;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.ToString());
            }
        }
        #endregion

        #region Search Functionality
        public void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            this.txtGallerySearch.Clear();
        }

        void searchTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                searchTimer.Stop();

                if (this.SearchFired != null)
                { this.SearchFired(txtGallerySearch.Text.TrimEnd(' ') ?? string.Empty); }

                foreach (clsICD10 icdCode in lstAllCodes.Where(p => p.CodeType == 0).OrderByDescending(p => p.ICD10CodeWithoutDecimal))
                {
                    icdCode.SearchString = txtGallerySearch.Text.TrimEnd(' ') ?? string.Empty;
                    icdCode.RefreshView();
                }

                ICD10Range.SearchString = txtGallerySearch.Text.TrimEnd(' ') ?? string.Empty;
                ICD10Range.RefreshSearch();

            }
            catch (Exception Ex)
            {
                throw Ex;
            }


        }

        private void txtGallerySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchTimer != null)
            {
                searchTimer.Stop();
                searchTimer.Start();
            }
        } 
        #endregion
       
        #region Tree Nodes Binding
        public void BindTreeNodes(DataTable DataTable)
        {
            try
            {

                this.RemoveAllNodes();
                List<clsICD10> lstCategories = null;
                if (DataTable.Columns.Contains("nICD10ID"))
                {
                    lstAllCodes = DataTable.AsEnumerable()
                       .Where(p => Convert.ToString(p["nCodeType"]) == "1")
                       .Select
                               (
                                   p =>
                                       new clsICD10(Convert.ToInt64(p["nICD10ID"]),
                                                    Convert.ToString(p["sICD10Code"]),
                                                       Convert.ToString(p["sDescriptionLong"]),
                                                               Convert.ToInt16(p["nCodeType"]),
                                                                       Convert.ToString(p["sICDParentCode"]))
                                                                           ).ToList();

                    lstCategories = DataTable.AsEnumerable().Where(p => Convert.ToString(p["nCodeType"]) == "0")
                        .Select(
                                    p =>
                                        new clsICD10(Convert.ToInt64(p["nICD10ID"]), 
                                                        Convert.ToString(p["sICD10Code"]),
                                                            Convert.ToString(p["sDescriptionLong"]),
                                                                Convert.ToInt16(p["nCodeType"]),
                                                                    Convert.ToString(p["sICDParentCode"]))
                                                                ).ToList();
                }
                else
                {
                    lstAllCodes = DataTable.AsEnumerable()
                      .Where(p => Convert.ToString(p["nCodeType"]) == "1")
                      .Select
                              (
                                  p =>
                                      new clsICD10(Convert.ToString(p["sICD10Code"]),
                                                      Convert.ToString(p["sDescriptionLong"]),
                                                              Convert.ToInt16(p["nCodeType"]),
                                                                      Convert.ToString(p["sICDParentCode"]))
                                                                          ).ToList();

                    lstCategories = DataTable.AsEnumerable().Where(p => Convert.ToString(p["nCodeType"]) == "0")
                        .Select(
                                    p =>
                                        new clsICD10(Convert.ToString(p["sICD10Code"]),
                                                        Convert.ToString(p["sDescriptionLong"]),
                                                            Convert.ToInt16(p["nCodeType"]),
                                                                Convert.ToString(p["sICDParentCode"]))
                                                                ).ToList();
                }

                

                foreach (clsICD10 ICDCode in lstAllCodes.OrderByDescending(p => p.ICD10CodeWithoutDecimal.Length))
                {
                    clsICD10 Parent = lstCategories.FirstOrDefault(p => p.ICD10CodeWithoutDecimal == ICDCode.ParentCodeWithoutDecimal);

                    if (Parent != null) { Parent.AddChild(ICDCode); }
                    Parent = null;

                    if (ICDCode.ICD10CodeWithoutDecimal.Length == 3 || ICDCode.ParentCode == "0A7321DE72D")
                    { ICD10Range.AddToRange(ICDCode); }
                }

                foreach (clsICD10 ICDCode in lstCategories.OrderByDescending(p => p.ICD10CodeWithoutDecimal.Length))
                {
                    clsICD10 Parent = lstCategories.FirstOrDefault(p => p.ICD10CodeWithoutDecimal == ICDCode.ParentCodeWithoutDecimal);

                    if (Parent != null) { Parent.AddChild(ICDCode); }
                    Parent = null;

                    if (ICDCode.ICD10CodeWithoutDecimal.Length == 3)
                    { ICD10Range.AddToRange(ICDCode); }
                }


                lstAllCodes.AddRange(lstCategories);

                lstCategories.Clear();
                lstCategories = null;

                foreach (clsICD10 ICDCode in lstAllCodes
                    .Where(p => p.CodeType == 0 && !p.ChildrenICD10.Any())
                    )
                {
                    ICDCode.RemoveCodeFromParent();
                }

                this.DataContext = null;
                this.DataContext = ICD10Range;


            }
            catch (System.Exception Ex)
            {

                throw Ex;
            }

        }      

        public void BindRemovedCodes(DataTable DataTable)
        {
            List<clsICD10> lstRemoved = null;

            try
            {
                lstRemoved = DataTable.AsEnumerable().Select(p => new clsICD10(Convert.ToString(p["sICD10Code"]), Convert.ToString(p["sDescriptionLong"]), Convert.ToInt16(p["nCodeType"]), Convert.ToString(p["sICDParentCode"]))).ToList();

                if (lstRemoved.Any(p => p.ICD10CodeWithoutDecimal.Length == 3))
                {

                    foreach (clsICD10 ICDCode in lstRemoved.OrderByDescending(p => p.ICD10CodeWithoutDecimal.Length))
                    {
                        if (!lstAllCodes.Any(p => p.ICD10CodeWithoutDecimal == ICDCode.ICD10CodeWithoutDecimal))
                        { lstAllCodes.Add(ICDCode); }

                        if (lstAllCodes.Any(p => p.ICD10CodeWithoutDecimal == ICDCode.ParentCodeWithoutDecimal))
                        {
                            if (!lstAllCodes.First(p => p.ICD10CodeWithoutDecimal == ICDCode.ParentCodeWithoutDecimal).ChildrenICD10.Any(p => p.ICD10CodeWithoutDecimal == ICDCode.ICD10CodeWithoutDecimal))
                            {
                                clsICD10 toAdd = lstAllCodes.First(p => p.ICD10CodeWithoutDecimal == ICDCode.ParentCodeWithoutDecimal);

                                toAdd.IsRemoved = false;
                                toAdd.AddChild(lstAllCodes.First(p => p.ICD10CodeWithoutDecimal == ICDCode.ICD10CodeWithoutDecimal));
                                toAdd.RefreshView();

                                toAdd = null;                                                                
                            }
                        }

                        if (ICDCode.ICD10CodeWithoutDecimal.Length == 3 && !ICD10Range.ChildrenICD10.Any(p => p.ICD10CodeWithoutDecimal == ICDCode.ICD10CodeWithoutDecimal))
                        { 
                            ICD10Range.AddToRange(lstAllCodes.First(p => p.ICD10CodeWithoutDecimal == ICDCode.ICD10CodeWithoutDecimal));
                            ICD10Range.RefreshSearch();
                        }
                    }

                    this.txtGallerySearch.Clear();
                }


            }
            catch (System.Exception Ex)
            {

                throw Ex;
            }
            finally
            {
                if (lstRemoved != null)
                {
                    lstRemoved.Clear();
                    lstRemoved = null;
                }
            }

        } 
        #endregion

        #region Remove Nodes Logic
        private void trvICD_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.Source is TreeViewItem && (e.Source as TreeViewItem).IsSelected)
                {
                    if (trvSubCode.SelectedItem is clsICD10 && ((clsICD10)trvSubCode.SelectedItem).CodeType == 1)
                    {
                        this.CodeAddedToCurrent_Click(this, null);                 
                    }
                }                
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void RemoveAllNodes()
        {
            try
            {
                if (this.ICD10Range != null && this.ICD10Range.ChildrenICD10 != null)
                {
                    foreach (clsICD10 element in this.ICD10Range.ChildrenICD10)
                    {
                        element.RemoveAll();
                        element.Dispose();
                    }

                    ICD10Range.ChildrenICD10.Clear();
                }

                if (this.lstAllCodes != null)
                { lstAllCodes.Clear(); }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public void RemoveSelectedNode()
        {
            if (
                this.trvSubCode.SelectedItem != null
                &&
                this.trvSubCode.SelectedItem is clsICD10
                )
            {
                clsICD10 SelectedCode = (clsICD10)trvSubCode.SelectedItem;

                if (SelectedCode != null)
                {
                    if (SelectedCode.ICD10CodeWithoutDecimal.Length == 3)
                    {
                        if (ICD10Range.ChildrenICD10.Any(p => p.ICD10CodeWithoutDecimal == SelectedCode.ICD10CodeWithoutDecimal))
                        { ICD10Range.ChildrenICD10.FirstOrDefault(p => p.ICD10CodeWithoutDecimal == SelectedCode.ICD10CodeWithoutDecimal).RemoveAll(); }

                        SelectedCode.IsRemoved = true;
                        ICD10Range.ChildrenICD10.Remove(SelectedCode);
                    }
                    else
                    {
                        string sParentCode = SelectedCode.ICD10CodeWithoutDecimal.Substring(0, SelectedCode.ICD10CodeWithoutDecimal.Length - 1);

                        while (sParentCode.Length >= 3 && !lstAllCodes.Any(p => p.ICD10CodeWithoutDecimal == sParentCode))
                        { sParentCode = sParentCode.Substring(0, sParentCode.Length - 1); }

                        if (lstAllCodes.Any(p => p.ICD10CodeWithoutDecimal == sParentCode))
                        {
                            if (SelectedCode.CodeType == 1)
                            {
                                lstAllCodes
                                   .FirstOrDefault(p => p.ICD10CodeWithoutDecimal == sParentCode)
                                   .RemoveChild(SelectedCode);
                            }
                            else
                            {
                                lstAllCodes
                                   .FirstOrDefault(p => p.ICD10CodeWithoutDecimal == SelectedCode.ICD10CodeWithoutDecimal)
                                   .RemoveAll();

                                lstAllCodes
                                  .FirstOrDefault(p => p.ICD10CodeWithoutDecimal == sParentCode)
                                  .RemoveChild(SelectedCode);
                            }
                        }


                    }
                }
            }
        } 
        #endregion

        public List<clsICD10> GetCodesToSave()
        {
            List<clsICD10> returned = null;

            try
            {
                if (
                       this.trvSubCode.SelectedItem != null
                       &&
                       this.trvSubCode.SelectedItem is clsICD10
                    )
                {
                    clsICD10 SelectedCode = (clsICD10)trvSubCode.SelectedItem;
                                        
                    if (SelectedCode.CodeType == 1)
                    {
                        returned = new List<clsICD10>();
                        returned.Add(SelectedCode); 
                    }
                    else
                    {  returned = SelectedCode.GetAllBillableCodes().ToList();  }
                                            
                    SelectedCode = null;
                }

                return returned;
            }
            catch (Exception Ex)
            {
                
                throw Ex;
            }

           
        }

        #region Button Clicks
        private void CodeRemovedFromMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.CodeRemovedFromMaster != null)
                { this.CodeRemovedFromMaster(); }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }


        }

        private void CodeAddedToCurrent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.CodeAddedToCurrent != null)
                { this.CodeAddedToCurrent(); }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        private void CodeImported_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.CodeSelectedForImport != null)
                { this.CodeSelectedForImport(); }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        #endregion

        public void RedesignDisplay()
        {
            try
            {
                if (this.pnlTreeview != null)
                {
                    pnlHeaderICD.Visibility = System.Windows.Visibility.Collapsed;

                    pnlRightSaveButton.Visibility = System.Windows.Visibility.Collapsed;
                    pnlLeftSaveButton.Visibility = System.Windows.Visibility.Visible;

                    pnlTreeview.SetValue(Grid.ColumnProperty, 1);
                    pnlTreeview.SetValue(Grid.ColumnSpanProperty, 2);

                    pnlTreeview.SetValue(Grid.RowProperty, 1);
                    pnlTreeview.SetValue(Grid.RowSpanProperty, 2);

                    pnlHeaderICD.SetValue(Grid.ColumnProperty, 1);
                    pnlHeaderICD.SetValue(Grid.ColumnSpanProperty, 2);

                    pnlClearSearch.SetValue(Grid.ColumnProperty, 1);
                    pnlClearSearch.SetValue(Grid.ColumnSpanProperty, 2);

                    pnlClearSearch.SetValue(Grid.RowProperty, 0);

                    this.DisplayRedesignedForSmartTreatment = true;
                }

            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public void ShowRightArrowImage()
        {
            try
            {
                if (this.grdSaveButtons != null)
                {
                    this.btnAddCode.Visibility = System.Windows.Visibility.Collapsed;
                    this.btnRemoveCode.Visibility = System.Windows.Visibility.Collapsed;
                    this.btnAddCodeArrow.Visibility = System.Windows.Visibility.Visible;
                    this.pnlHeaderICD.Visibility = System.Windows.Visibility.Collapsed;

                    pnlClearSearch.SetValue(Grid.RowProperty, 0);
                    
                    pnlTreeview.SetValue(Grid.RowProperty, 1);
                    pnlTreeview.SetValue(Grid.RowSpanProperty, 2);
                }
            }
            catch (Exception Ex)
            {
                
                throw Ex;
            }
        }

        public void ShowICD10GallerySetup()
        {
            try
            {
                if (this.grdSaveButtons != null)
                {
                    this.btnAddCode.Visibility = System.Windows.Visibility.Visible;
                    this.btnRemoveCode.Visibility = System.Windows.Visibility.Visible;

                    this.btnAddCodeArrow.Visibility = System.Windows.Visibility.Collapsed;
                    this.btnRemoveMasterCode.Visibility = System.Windows.Visibility.Collapsed;                   
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }


        public void ShowRemoveCurrentCodeImage()
        {
            try
            {
                if (this.grdSaveButtons != null)
                {
                    this.btnAddCode.Visibility = System.Windows.Visibility.Collapsed;
                    this.btnRemoveCode.Visibility = System.Windows.Visibility.Collapsed;
                    this.btnAddCodeArrow.Visibility = System.Windows.Visibility.Collapsed;

                    this.btnRemoveMasterCode.Visibility = System.Windows.Visibility.Visible;                   
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void btnRemoveMasterCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.CodeRemovedFromICD10Master != null)
                { this.CodeRemovedFromICD10Master(); }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
        //Added code by Mayuri :16-Jan-2016-WPF window disappers on tooltip
        private void TextBlock_ToolTipOpening(object sender, ToolTipEventArgs e)
        {

            var source = (System.Windows.Interop.HwndSource)PresentationSource.FromDependencyObject(this);
            if (source != null)
            {
                if (System.Windows.Forms.Control.FromChildHandle(source.Handle) != null)
                {
                    var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromChildHandle(source.Handle).TopLevelControl;
                    if (form != null)
                    {
                        form.TopMost = true;
                        form.TopMost = false;
                    }
                }
                else
                {
                    Window ParentWindow = Window.GetWindow(this);
                    if (ParentWindow != null)
                    {
                        ParentWindow.Topmost = true;
                        ParentWindow.Topmost = false;
                    }

                }
            }

          


        }
                    
	}
}