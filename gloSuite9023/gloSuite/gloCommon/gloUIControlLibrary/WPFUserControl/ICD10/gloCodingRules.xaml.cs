using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using gloUIControlLibrary.Classes.ICD10;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Data;

namespace gloUIControlLibrary.WPFUserControl.ICD10
{
    /// <summary>
    /// Interaction logic for gloCodingRules.xaml
    /// </summary>
    public partial class gloCodingRules : UserControl, IDisposable
    {
      
        public string ICD10Code { get; set; }
        public string ICD10Description { get; set; }
        public string DBConnectionString { get; set; }

        public bool NoData { get; set; }

        List<string> lstICDCodes = null;
        XDocument xDocument = null;

        public gloCodingRules()
        { InitializeComponent(); }

        public gloCodingRules(string ICD10Code, string Description, string dbConnectionString)
        {
            this.ICD10Code = ICD10Code;
            this.ICD10Description = Description;
            this.DBConnectionString = dbConnectionString;            
        }
             
        private void lstCodeFirst_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            try
            {
                e.Handled = true;

                MouseWheelEventArgs args = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);

                args.RoutedEvent = UIElement.MouseWheelEvent;
                args.Source = sender;

                scrlViewer.RaiseEvent(args);

            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        }

        public GroupedICD10Notes ShowXMLData(List<string> ICD10Codes, XDocument xDocument)
        {
            ICD10Notes Notes = null;
            IEnumerable<XElement> notesXElement = null;
            GroupedICD10Notes grouped = new GroupedICD10Notes();

            try
            {
                if (ICD10Codes != null && xDocument != null && xDocument.Document != null)
                {
                    if (xDocument != null)
                    {
                        foreach (string sICD10Code in ICD10Codes)
                        {
                            notesXElement = from XElement element in xDocument.Descendants("name")
                                            where element.Value.ToLower() == sICD10Code.ToLower() && element.Ancestors("section").Any()
                                            select element;

                            if (notesXElement.Any())
                            {
                                foreach (XElement Element in notesXElement)
                                {
                                    Notes = GetElementDetails(notesXElement, "diag");

                                    if (Notes != null && Notes.HasData)
                                    { grouped.CodingRules.Add(Notes); }

                                    Notes = null;
                                }
                            }
                        }

                        if (notesXElement != null)
                        {
                            Notes = GetElementDetails(notesXElement, "section");

                            if (Notes != null && Notes.HasData)
                            { grouped.CodingRules.Add(Notes); }

                            Notes = null;
                        }

                        if (notesXElement != null)
                        {
                            Notes = GetElementDetails(notesXElement, "chapter");

                            if (Notes != null && Notes.HasData)
                            {
                                this.NoData = false;
                                grouped.CodingRules.Add(Notes);
                            }
                            else
                            { this.NoData = true; }
                            

                            Notes = null;
                        }
                       
                    }
                }

                return grouped;
            }
            catch (Exception Ex)
            { 
                LogException.ExceptionLog(Ex.ToString(), true);
                return null;
            }

        }

        private ICD10Notes GetElementDetails(IEnumerable<XElement> notesXElement, string anchestorType)
        {
            ICD10Notes Notes = null;
            XElement xSection = null;

            try
            {
                if (anchestorType == "diag")
                {
                    if (notesXElement.FirstOrDefault() != null)
                    { xSection = notesXElement.FirstOrDefault().Ancestors("diag").FirstOrDefault(); }
                }
                else
                {
                    if (notesXElement.FirstOrDefault(p => p.Value.Length == 3) != null)
                    { xSection = notesXElement.FirstOrDefault(p => p.Value.Length == 3).Ancestors(anchestorType).FirstOrDefault(); }
                }

                if (xSection != null)
                {
                    Notes = new ICD10Notes();
                    switch (anchestorType)
                    {
                        case "diag":
                            Notes.ICDCode = xSection.Element("name").Value;
                            Notes.SortRank = 3;
                            break;
                        case "section":
                            Notes.SortRank = 2;
                            break;
                        case "chapter":
                            Notes.SortRank = 1;
                            break;
                        default:
                            Notes.ICDCode = "";
                            break;
                    }

                    Notes.Description = xSection.Element("desc").Value;

                    if (xSection.Elements("notes").Any())
                    { Notes.Notes = xSection.Element("notes"); }

                    if (xSection.Elements("useAdditionalCode").Any())
                    { Notes.UseAdditionalCode = xSection.Element("useAdditionalCode"); }

                    if (xSection.Elements("inclusionTerm").Any())
                    { Notes.InclusionTerm = xSection.Element("inclusionTerm"); }

                    if (xSection.Elements("includes").Any())
                    { Notes.Includes = xSection.Element("includes"); }

                    if (xSection.Elements("excludes1").Any())
                    { Notes.Excludes = xSection.Element("excludes1"); }

                    if (xSection.Elements("excludes2").Any())
                    { Notes.Excludes2 = xSection.Element("excludes2"); }

                    if (xSection.Elements("codeFirst").Any())
                    { Notes.CodeFirst = xSection.Element("codeFirst"); }

                    if (xSection.Elements("codeAlso").Any())
                    { Notes.CodeAlso = xSection.Element("codeAlso"); }

                }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
            finally
            {
                xSection = null;
            }

            return Notes;
        }

        public void LoadNotes(bool ExecuteShowXMLData)
        {
            clsICD10 _selectedCode = null;
            clsICD10DBLayer _dbLayer = null;                                    
            XmlDataProvider xDocIndex = null;

            try
            {
                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\ICD10CM_FY2014_Full_XML_Tabular.xml"))
                {
                    xDocIndex = new XmlDataProvider();
                    xDocIndex.Document = new XmlDocument();
                    xDocIndex.Document.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ICD10CM_FY2014_Full_XML_Tabular.xml");
                    
                    xDocument = XDocument.Load(new XmlNodeReader(xDocIndex.Document));

                    if (lstICDCodes == null)
                    { lstICDCodes = new List<string>(); }
                    else
                    { lstICDCodes.Clear(); }

                    
                    _dbLayer = new clsICD10DBLayer(DBConnectionString);
                    _selectedCode = new clsICD10(ICD10Code, ICD10Description, -1, "");
                    lstICDCodes = _dbLayer.GetParentCodes(ICD10Code).AsEnumerable().Select(p => Convert.ToString(p["sICDCode"])).ToList();

                    if (ExecuteShowXMLData)
                    {this.DataContext = this.ShowXMLData(lstICDCodes, xDocument); }
                    

                }
                else { MessageBox.Show("File not exists"); }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
            finally
            {               
                if (xDocIndex != null)
                { xDocIndex = null; }

                if (_selectedCode != null)
                {
                    _selectedCode.Dispose();
                    _selectedCode = null;
                }

                if (_dbLayer != null)
                {
                    _dbLayer = null;
                }
                this.Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        public void Dispose()
        {
            try
            {
                if (this.lstICDCodes != null)
                {
                    this.lstICDCodes.Clear();
                    this.lstICDCodes = null;
                }

                this.xDocument = null;
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
           
        }
              
    }
}
