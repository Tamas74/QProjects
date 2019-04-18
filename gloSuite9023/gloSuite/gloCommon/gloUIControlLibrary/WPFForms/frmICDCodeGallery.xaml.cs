using System.Windows;
using gloUIControlLibrary.Classes.ICD10;
using System.Xml.Linq;
using System.Windows.Data;
using System.Data;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Xml;
using gloUIControlLibrary.WPFUserControl.ICD10;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using System.Text;

namespace gloUIControlLibrary.WPFForms
{
    /// <summary>
    /// Interaction logic for frmICDCodeGallery.xaml
    /// </summary>
    public partial class frmICDCodeGallery : Window
    {

        #region Properties
        public Int64 ClinicID { get; set; }

        public string ConnectionString { get; set; }        

        #endregion

        #region Local Variables
        XmlDataProvider xDocIndex = null;
        clsICD10DBLayer ICD10DBlayer = null;

        private List<CurrentICD10> lstCurrentCodes = null;
        private List<clsSpeciality> lstSpeciality = null;
        private static frmICDCodeGallery frm;

        DispatcherTimer searchTimer = null;

        #endregion

        #region Constructors
      
        

        public static frmICDCodeGallery GetInstance(string ConnectionString)
        {
            try
            {
                if (frm == null)
                { frm = new frmICDCodeGallery(ConnectionString); }                
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }
            
            return frm;
        }

        public static frmICDCodeGallery CheckFormOpen()
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
            catch (Exception) { return null; }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                frm = null;
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }
        }

        private frmICDCodeGallery(string ConnectionString)
        {
            InitializeComponent();

            try
            {
                this.ConnectionString = ConnectionString;
                this.ICD10DBlayer = new clsICD10DBLayer(ConnectionString);

                this.gloICD10IndexControl.DoubleClicked += new WPFUserControl.ICD10.gloICD10IndexControl.ItemDoubleClicked(gloICD10IndexControl_DoubleClicked);

                this.gloICDCurrentCodes.BindComboItems(ICD10DBlayer.GetSpeciality(true));

                this.gloICDSubCode.CodeRemovedFromMaster += new ICDSubCodeControl.codeRemovedFromCurrent(gloICDSubCode_CodeRemovedFromMaster);
                this.gloICDSubCode.CodeAddedToCurrent += new ICDSubCodeControl.billableCodeAddedToCurrent(gloICDSubCode_CodeAddedToCurrent);

                this.gloICDSubCode.CodeRemovedFromICD10Master += new ICDSubCodeControl.masterCodeRemoved(gloICDSubCode_CodeRemovedFromICD10Master);

                this.lstCurrentCodes = new List<CurrentICD10>();
                this.lstSpeciality = new List<clsSpeciality>();

                this.searchTimer = new DispatcherTimer();
                this.searchTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                this.searchTimer.Tick += new EventHandler(searchTimer_Tick);

            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }            
        }

      
        #endregion

        #region Code shifting logic
        void gloICDSubCode_CodeAddedToCurrent()
        {
            Int64 SpecialityID = 0;
            List<clsICD10> CodesToSave = null;

            try
            {
                SpecialityID = gloICDCurrentCodes.GetSelectedSpeciality.SpecialityID;
                CodesToSave = gloICDSubCode.GetCodesToSave();

                if (CodesToSave != null && CodesToSave.Any())
                {
                    DataTable dtICD10Codes = new DataTable("ICD10Codes");

                    var cols = dtICD10Codes.Columns;
                    cols.Add(new DataColumn("sICD9Code", System.Type.GetType("System.String")));
                    cols.Add(new DataColumn("sDescription", System.Type.GetType("System.String")));
                    cols.Add(new DataColumn("nSpecialtyID", System.Type.GetType("System.Int64")));

                    cols.Add(new DataColumn("nClinicID", System.Type.GetType("System.Int64")));
                    cols.Add(new DataColumn("bIsBlocked", System.Type.GetType("System.Boolean")));
                    cols.Add(new DataColumn("bInActive", System.Type.GetType("System.Boolean")));

                    cols.Add(new DataColumn("nImmediacyDefault", System.Type.GetType("System.Int32")));
                    cols.Add(new DataColumn("nICDRevision", System.Type.GetType("System.Int16")));
                    cols.Add(new DataColumn("nCodeType", System.Type.GetType("System.Int16")));

                    cols.Add(new DataColumn("sConceptID", System.Type.GetType("System.String")));
                    cols.Add(new DataColumn("sDescriptionID", System.Type.GetType("System.String")));
                    cols.Add(new DataColumn("sSnomedID", System.Type.GetType("System.String")));

                    cols.Add(new DataColumn("sSnomedDescription", System.Type.GetType("System.String")));
                    cols.Add(new DataColumn("sSnomedDefination", System.Type.GetType("System.String")));


                    foreach (clsICD10 element in CodesToSave)
                    {
                        DataRow dRow = dtICD10Codes.NewRow();

                        dRow["sICD9Code"] = element.ICD10Code;
                        dRow["sDescription"] = element.LongDescription;
                        dRow["nSpecialtyID"] = SpecialityID;

                        dRow["nClinicID"] = this.ClinicID;
                        dRow["bIsBlocked"] = DBNull.Value;
                        dRow["bInActive"] = false;

                        dRow["nImmediacyDefault"] = 3;
                        dRow["nICDRevision"] = 10;
                        dRow["nCodeType"] = 1;

                        dRow["sConceptID"] = DBNull.Value;
                        dRow["sDescriptionID"] = DBNull.Value;
                        dRow["sSnomedID"] = DBNull.Value;

                        dRow["sSnomedDescription"] = DBNull.Value;
                        dRow["sSnomedDefination"] = DBNull.Value;

                        dtICD10Codes.Rows.Add(dRow);                        
                    }

                    ICD10DBlayer.SaveICD10Codes(dtICD10Codes);

                    if (dtICD10Codes != null)
                    {
                        dtICD10Codes.Rows.Clear();
                        dtICD10Codes.Dispose();
                        dtICD10Codes = null;
                    }

                    gloICDSubCode.RemoveSelectedNode();

                    this.gloICDCurrentCodes.BatchAdd(CodesToSave);

                    //foreach (clsICD10 element in CodesToSave)
                    //{ this.gloICDCurrentCodes.AddToCurrent(element); }
                    
                    MessageBox.Show(CodesToSave.Count().ToString() + " ICD 10 code(s) imported.", ProductInformation.GetProductName, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Please select an ICD-10 code or a Category from ICD-10 Gallery to add.", ProductInformation.GetProductName, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }
            finally 
            {
                if (CodesToSave != null)
                { 
                    CodesToSave.Clear();
                    CodesToSave = null;
                }
             }
          
        }

        void gloICDSubCode_CodeRemovedFromMaster()
        {
            try
            {
                clsICD10 SelectedCode = gloICDCurrentCodes.GetSelectedICDCode();
                if (SelectedCode != null)
                {
                    if (ICD10DBlayer.IsInUseICD(SelectedCode.ICD10Code))
                    {
                        MessageBox.Show("ICD10 code " + SelectedCode.ICD10Code + " is in use and cannot be deleted.", ProductInformation.GetProductName, MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }


                    this.ICD10DBlayer.DeleteICD10Code(SelectedCode.ICD10Code);
                    this.gloICDCurrentCodes.RemoveFromCurrent(SelectedCode.ICD10Code);

                    DataTable dt = this.ICD10DBlayer.GetRemovedICD10Codes(this.gloICDSubCode.StartRange, this.gloICDSubCode.EndRange, SelectedCode.ICD10Code);                  
                    this.gloICDSubCode.BindRemovedCodes(dt);
                }
                else
                { MessageBox.Show("Please select an ICD 10 code from Current ICD-10 to remove.", ProductInformation.GetProductName, MessageBoxButton.OK, MessageBoxImage.Information); }
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }            

           
        } 
        #endregion

        #region Load Range
        void gloICD10IndexControl_DoubleClicked(System.Xml.XmlElement XmlElement)
        {
            string sFirst = string.Empty;
            string sLast = string.Empty;
            string sDescription = string.Empty;
            DataTable dt = null;
            try
            {
                sFirst = XmlElement.Attributes["first"].Value;
                sLast = XmlElement.Attributes["last"].Value;
                sDescription = XmlElement.InnerText.Replace("\r\n", "").Trim();

                //ICD10Range.StartCode = sFirst;
                //ICD10Range.EndCode = sLast;
                //ICD10Range.Description = sDescription;

                this.gloICDSubCode.StartRange = sFirst;
                this.gloICDSubCode.EndRange = sLast;
                dt = ICD10DBlayer.LoadCategoryCodes(sFirst, sLast);
                this.gloICDSubCode.BindTreeNodes(dt);

                if (!dt.AsEnumerable().Any(p => Convert.ToInt16(p["nCodeType"]) == 1))
                {
                    MessageBox.Show("All billable codes under this range have been imported.", ProductInformation.GetProductName, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }            
            finally
            {
                if (dt != null)
                { 
                    dt.Dispose();
                    dt = null;
                }
            }
        } 
        #endregion

        #region Window Loaded Event
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            { this.gloICD10IndexControl.SetBinding(xDocIndex); }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }
            
        }
        #endregion

        #region Load Index XML Data
        public void LoadIndexXMLData()
        {
            try { xDocIndex = ICD10DBlayer.GetICD10Notes(); }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }
        }
        #endregion     

        #region Button Clicks
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Dispose();
                this.Close();
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }            
        }

        private void btnShowRules_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {

                if (gloICDSubCode.GetSelectedICDCode == null || gloICDSubCode.GetSelectedICDCode.ICD10Code.Trim() == "")
                {
                    MessageBox.Show("Please select an ICD-10 code or Category from ICD-10 Gallery to show its coding rules.", ProductInformation.GetProductName, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    frmShowCodingRules frmRulesWindow = new frmShowCodingRules(gloICDSubCode.GetSelectedICDCode.ICD10Code, gloICDSubCode.GetSelectedICDCode.LongDescription, ConnectionString);
                    frmRulesWindow.Owner = this;
                    frmRulesWindow.LoadNotes();

                    if (frmRulesWindow.NoData)
                    { MessageBox.Show("No notes available for code " + gloICDSubCode.GetSelectedICDCode.ICD10Code, ProductInformation.GetProductName, MessageBoxButton.OK, MessageBoxImage.Information); }
                    else
                    { frmRulesWindow.ShowDialog(); }
                    
                    if (frmRulesWindow != null)
                    { frmRulesWindow = null; }
                }
               
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        } 
        #endregion

        #region Dispose
        public void Dispose()
        {
            try
            {
                if (this.lstCurrentCodes != null)
                {
                    this.lstCurrentCodes.Clear();
                    this.lstCurrentCodes = null;
                }

                if (this.lstSpeciality != null)
                {
                    this.lstSpeciality.Clear();
                    this.lstSpeciality = null;
                }

                this.searchTimer = null;

                if (this.gloICDSubCode != null)
                {
                    this.gloICDSubCode.CodeRemovedFromMaster -= this.gloICDSubCode_CodeRemovedFromMaster;
                    this.gloICDSubCode.CodeAddedToCurrent -= this.gloICDSubCode_CodeAddedToCurrent;

                    this.gloICDSubCode.Dispose();
                    this.gloICDSubCode = null;
                }

                if (this.gloICDCurrentCodes != null)
                {
                    this.gloICDCurrentCodes.Dispose();
                    this.gloICDCurrentCodes = null;
                }

                if (this.gloICD10IndexControl != null)
                { 
                    this.gloICD10IndexControl.DoubleClicked -= this.gloICD10IndexControl_DoubleClicked;                    
                    this.gloICD10IndexControl = null;
                }

                if (this.ICD10DBlayer != null)
                { this.ICD10DBlayer = null; }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.ToString());
            }
        }
        #endregion

        #region Coding Rules Button Click
        private void ShowRulesButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnShowRules_Click(this, new RoutedEventArgs());
        }
        #endregion

        #region Close Button Click Event
        private void CloseButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnClose_Click(this, new RoutedEventArgs());
        }
        #endregion

        #region ICD 10 Master

        #region Speciality ComboBox Selection Changed
        private void ICDMasterSearch(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string sSearchText = txtSearch.Text.Trim().Replace("[", "[[]").Replace("%", "\\%").Replace("_","\\_");
                this.BindMasterICD10Codes(((clsSpeciality)cmbSpeciality.SelectedItem).SpecialityID, sSearchText);
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }
        }

        private void cmbSpeciality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbSpeciality.SelectedItem != null && cmbSpeciality.SelectedItem is clsSpeciality)
                {
                    string sSearchText = txtSearch.Text.Trim().Replace("[", "[[]").Replace("%", "\\%").Replace("_","\\_");
                    this.BindMasterICD10Codes(((clsSpeciality)cmbSpeciality.SelectedItem).SpecialityID, sSearchText); 
                }

            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }
        }
        #endregion

        #region ICD 10 Master Search Functionality
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (this.searchTimer != null)
                {
                    this.searchTimer.Stop();
                    this.searchTimer.Start();
                }
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }
        }

        void searchTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.searchTimer.Stop();
                this.ICDMasterSearch(this, null);
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }
        }
        #endregion

        #region ICD 10 Master Binding
        public void BindSpecialityComboItems(DataTable DataTable)
        {
            try
            {
                lstSpeciality.Clear();

                lstSpeciality = DataTable
                    .AsEnumerable()
                    .Select
                    (p => new clsSpeciality
                        (
                            Convert.ToInt64(p["nSpecialtyID"]),
                            Convert.ToString(p["sDescription"])
                        )
                    ).ToList();

                this.cmbSpeciality.ItemsSource = lstSpeciality;

                if (lstSpeciality.Any(p => p.SpecialityID == 0))
                { cmbSpeciality.SelectedItem = lstSpeciality.First(p => p.SpecialityID == 0); }

            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }

        }

        private void BindMasterICD10Codes(Int64 SpecialityID, string SearchText)
        {
            DataTable dt = null;

            try
            {
                if (
                    this.ICD10DBlayer != null
                    &&
                    cmbSpeciality.SelectedItem != null
                    &&
                    cmbSpeciality.SelectedItem is clsSpeciality
                    )
                {
                    dt = ICD10DBlayer.GetMasterCD10Codes(SpecialityID, SearchText);

                    if (dt != null)
                    {
                        lstCurrentCodes = dt.AsEnumerable()
                            .Select
                            (p => new CurrentICD10()
                            {
                                Speciality = Convert.ToInt64(p["nSpecialityID"]),
                                CodeType = 1,
                                IsChecked = false,
                                ICD10Code = Convert.ToString(p["sICD10Code"]),
                                LongDescription = Convert.ToString(p["sDescription"])
                            }
                            ).ToList();
                    }
                    this.trvCurrentICDNodes.DataContext = lstCurrentCodes;
                }
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }
        }
        #endregion

        #region Get Selected ICD 10 Master Codes
        private IEnumerable<CurrentICD10> GetSelectedMasterCodes()
        {
            IEnumerable<CurrentICD10> enumReturned = new List<CurrentICD10>().AsEnumerable();

            try
            {
                if (
                    trvCurrentICDNodes.Items != null
                    &&
                    trvCurrentICDNodes.Items.SourceCollection != null
                    &&
                    trvCurrentICDNodes.Items.SourceCollection is List<CurrentICD10>
                    )

                { enumReturned = ((List<CurrentICD10>)trvCurrentICDNodes.Items.SourceCollection).Where(p => p.IsChecked); }
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }

            return enumReturned;

        }
        #endregion

        #region ICD 10 Code removed from Master
        void gloICDSubCode_CodeRemovedFromICD10Master()
        {
            IEnumerable<CurrentICD10> currentCodes = null;
            IEnumerable<CurrentICD10> masterCodes = null;

            List<CurrentICD10> lstInUseICD10 = null;
            StringBuilder sb = null;

            try
            {
                masterCodes = this.GetSelectedMasterCodes();
                
                if (masterCodes.Any())
                {
                    currentCodes = this.gloICDCurrentCodes.GetAllCodes();
                    lstInUseICD10 = new List<CurrentICD10>();                

                    foreach (CurrentICD10 code in masterCodes)
                    {
                        if (!ICD10DBlayer.IsInUseICD(code.ICD10Code))
                        {
                            this.ICD10DBlayer.DeleteICD10Code(code.ICD10Code);

                            if (currentCodes.Any(p => p.ICD10CodeWithoutDecimal == code.ICD10CodeWithoutDecimal))
                            {
                                this.gloICDCurrentCodes.RemoveFromCurrent(code.ICD10Code);

                                using (DataTable dtSubCodeControlBinding = this.ICD10DBlayer.GetRemovedICD10Codes(this.gloICDSubCode.StartRange, this.gloICDSubCode.EndRange, code.ICD10Code))
                                { this.gloICDSubCode.BindRemovedCodes(dtSubCodeControlBinding); }
                            }
                        }
                        else
                        { lstInUseICD10.Add(code); }

                    }

                    this.cmbSpeciality_SelectionChanged(this, null);

                    if (lstInUseICD10.Any())
                    {
                        sb = new StringBuilder();

                        if (lstInUseICD10.Count > 10)
                        {
                            sb.AppendLine("More than 10 ICD-10 codes are in use and cannot be deleted.");
                            sb.AppendLine("A list of some of them is as follows: " + Environment.NewLine);
                        }
                        else { sb.AppendLine("The following ICD-10 codes are in use and cannot be deleted: " + Environment.NewLine); }

                        foreach (CurrentICD10 element in lstInUseICD10)
                        {
                            sb.Append(element.ICD10Code + " - ");
                            sb.AppendLine(element.LongDescription);
                        }

                        MessageBox.Show(sb.ToString(), ProductInformation.GetProductName, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                { MessageBox.Show("Please select an ICD 10 code to remove.", ProductInformation.GetProductName, MessageBoxButton.OK, MessageBoxImage.Information); }                
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }
            finally
            {
                if (lstInUseICD10 != null)
                {
                    lstInUseICD10.Clear();
                    lstInUseICD10 = null;
                }

                if (sb != null)
                {
                    sb.Clear();
                    sb = null;
                }
            }
        }
        #endregion

        #endregion

        #region Tab Control Selection Changed
        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;

            if (e.Source != null && e.Source is TabControl && sender is TabControl && (sender as TabControl).SelectedItem is TabItem)
            {
                string sTabName = Convert.ToString(((sender as TabControl).SelectedItem as TabItem).Name);
                switch (sTabName)
                {
                    case "tabICD10Current":
                        this.gloICDSubCode.ShowICD10GallerySetup();
                        break;

                    case "tabICD10Master":
                        this.gloICDSubCode.ShowRemoveCurrentCodeImage();
                        if (lstCurrentCodes != null && !lstCurrentCodes.Any())
                        { this.BindSpecialityComboItems(this.ICD10DBlayer.GetSpeciality(true)); }
                        else
                        { this.cmbSpeciality_SelectionChanged(this, null); }
                        break;

                    default:
                        break;
                }

            }
        }
        #endregion

        #region ICD 10 Master Key Down Event
        private void trvMaster_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Space || e.Key == Key.Enter)
                {
                    e.Handled = true;
                    if (trvCurrentICDNodes.SelectedItem != null && trvCurrentICDNodes.SelectedItem is CurrentICD10)
                    { ((CurrentICD10)trvCurrentICDNodes.SelectedItem).IsChecked = !((CurrentICD10)trvCurrentICDNodes.SelectedItem).IsChecked; }
                }
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }
        }
        #endregion

        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.txtSearch.Text = string.Empty;
                this.searchTimer_Tick(this, new EventArgs());
            }
            catch (Exception Ex)
            { throw Ex; }
        }
              
    }
}
