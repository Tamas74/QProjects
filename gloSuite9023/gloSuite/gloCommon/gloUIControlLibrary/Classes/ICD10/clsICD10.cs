using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections;
using System.Xml.Linq;
using System.Windows.Media.Imaging;

namespace gloUIControlLibrary.Classes.ICD10
{   
    public class clsICD10 : IDisposable
    {
        #region Code Removed Event
        public delegate void ICD10Removed(clsICD10 ICD10Code);
        public event ICD10Removed CodeRemoved;

        public delegate void removeChild(clsICD10 ICD10Code);
        public event removeChild RemoveMe; 

        #endregion

        #region Properties
        public ObservableCollection<clsICD10> ChildrenICD10 { get; set; }
        public string ICD10Code { get; set; }
        public Int16 CodeType { get; set; }
        public Int64 ICDCodeID { get; set; }

        private CollectionView view = null;

        public bool IsRemoved { get; set; }               
        public string SearchString { get; set; }
        public string LongDescription { get; set; }

        public string ParentCode { get; set; }        
        public string ParentCodeWithoutDecimal { get { return this.ParentCode.Replace(".", ""); } }
        public string ICD10CodeWithoutDecimal { get { return this.ICD10Code.Replace(".", ""); } }

        private bool bFoundInSearch = true;
        public bool FoundInSearch
        {
            get { return this.bFoundInSearch; }
            set
            {
                this.bFoundInSearch = value;

            }
        } 
        #endregion

        #region Constructor
        
        public clsICD10(string ICD10Code, string LongDescription, Int16 CodeType, string ParentCode)
        {
            this.ChildrenICD10 = new ObservableCollection<clsICD10>();
            this.ICD10Code = ICD10Code;
            this.LongDescription = LongDescription;
            this.CodeType = CodeType;
            this.ParentCode = ParentCode;

            view = (CollectionView)CollectionViewSource.GetDefaultView(this.ChildrenICD10);
            if (view != null)
            {
                view.Filter = UserFilter;

                if (!view.SortDescriptions.Any())
                { view.SortDescriptions.Add(new SortDescription("ICD10CodeWithoutDecimal", ListSortDirection.Ascending)); }
            }
        }

        public clsICD10(Int64 ICDCodeID, string ICD10Code, string LongDescription, Int16 CodeType, string ParentCode):this(ICD10Code, LongDescription, CodeType,ParentCode)
        {
            this.ICDCodeID = ICDCodeID;
        }   

        #endregion

        #region Child Adding Functionality
        public void AddChild(clsICD10 ChildICD10)
        {
            try
            {
                ChildICD10.IsRemoved = false;

                if (ChildICD10.CodeRemoved != this.ChildICD10_CodeRemoved) { ChildICD10.CodeRemoved += new ICD10Removed(ChildICD10_CodeRemoved); }
                if (ChildICD10.RemoveMe != this.ChildICD10_RemoveMeRequest) { ChildICD10.RemoveMe += new removeChild(ChildICD10_RemoveMeRequest); }

                this.ChildrenICD10.Add(ChildICD10);             
                this.RefreshView();
            }
            catch (Exception ex) { throw ex; }

        }

        public void RemoveCodeFromParent() 
        { 
            try 
            { if (this.RemoveMe != null) 
            { this.RemoveMe(this); } } 
            catch (Exception Ex) { throw Ex; } }

        public void ChildICD10_RemoveMeRequest(clsICD10 ICD10Code)
        {
            try
            { if (this.ChildrenICD10.Contains(ICD10Code)) { this.RemoveChild(ICD10Code); } }
            catch (Exception Ex) { throw Ex; }
            
        } 
        #endregion

        #region Code Removal Functionality
        public void ChildICD10_CodeRemoved(clsICD10 ICD10Code)
        {
            try
            {
                if (!ICD10Code.ChildrenICD10.Any())
                {
                    if (ICD10Code.CodeRemoved != null && ICD10Code.CodeRemoved == this.ChildICD10_CodeRemoved)
                    { ICD10Code.CodeRemoved -= ChildICD10_CodeRemoved; }

                    if (ICD10Code.RemoveMe != null && ICD10Code.RemoveMe == ChildICD10_RemoveMeRequest)
                    { ICD10Code.CodeRemoved -= ChildICD10_RemoveMeRequest; }

                    ICD10Code.IsRemoved = true;
                    this.ChildrenICD10.Remove(ICD10Code);

                    if (!this.ChildrenICD10.Any() && this.CodeRemoved != null) { this.CodeRemoved(this); }
                }
            }
            catch (Exception Ex) { throw Ex; }
            
        }

        public void RemoveChild(clsICD10 ChildICD10)
        {
            try
            {
                string sParentCode = ChildICD10.ICD10CodeWithoutDecimal.Substring(0, ChildICD10.ICD10CodeWithoutDecimal.Length - 1);

                if (this.ChildrenICD10.Contains(ChildICD10))
                {
                    if ( ChildICD10.CodeRemoved != null && ChildICD10.CodeRemoved == ChildICD10_CodeRemoved )
                    { ChildICD10.CodeRemoved -= ChildICD10_CodeRemoved; }

                    if (ChildICD10.RemoveMe != null && ChildICD10.RemoveMe == ChildICD10_RemoveMeRequest)
                    { ChildICD10.CodeRemoved -= ChildICD10_RemoveMeRequest; }

                    ChildICD10.IsRemoved = true;
                    this.ChildrenICD10.Remove(ChildICD10);

                    if (!this.ChildrenICD10.Any() && this.CodeRemoved != null) { this.CodeRemoved(this); }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void RemoveAll()
        {
            try
            {
                foreach (clsICD10 element in this.ChildrenICD10)
                {
                    element.IsRemoved = true;
                    element.RemoveAll();
                    if (
                        element.CodeRemoved != null
                        &&
                        element.CodeRemoved == this.ChildICD10_CodeRemoved
                        )
                    { element.CodeRemoved -= this.ChildICD10_CodeRemoved; }

                    if (
                           element.RemoveMe != null
                           &&
                           element.RemoveMe == ChildICD10_RemoveMeRequest
                           )
                    { element.CodeRemoved -= ChildICD10_RemoveMeRequest; }                    
                }
                
                this.ChildrenICD10.Clear(); 
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
                      
        } 
        #endregion

        #region View
        public ICollectionView ICD10CodeFilteredView
        { get { return view; } }

        public void RefreshView()
        {
            try
            { if (view != null) { view.Refresh(); } }
            catch (Exception Ex) { throw Ex; }
        }
        #endregion
       
        #region Search Functionality
      
        private bool UserFilter(object item)
        {
            bool bReturned = true;

            try
            {
                if (this.SearchString != null && item is clsICD10 && ((clsICD10)item).CodeType == 1)
                {
                    clsICD10 Code = (clsICD10)item;
                    bReturned = Code.LongDescription.ToLower().Contains(this.SearchString.ToLower()) || Code.ICD10Code.ToLower().Contains(this.SearchString.ToLower());

                }
                else if (this.SearchString != null && item is clsICD10 && ((clsICD10)item).CodeType == 0)
                {
                    clsICD10 Code = (clsICD10)item;
                    bReturned = Code.SearchInChildren(SearchString);                    
                }

                this.FoundInSearch = bReturned;
            }
            catch (Exception Ex)
            {
                
                throw Ex;
            }

            
            return bReturned;
        }

        public bool SearchInChildren(string SearchText)
        {
            try
            {
                if (this.CodeType == 1)
                { return this.LongDescription.ToLower().Contains(SearchText.ToLower()) || this.ICD10Code.ToLower().Contains(SearchText.ToLower()); }
                else { return this.ChildrenICD10.Any(p => p.SearchInChildren(SearchText)); }
            }
            catch (Exception Ex)
            {                
                throw Ex;
            }            
        }

        #endregion



        private IEnumerable<clsICD10> GetBillableCodes()
        {
            if (this.CodeType == 1)
            {
                yield return this;
            }
            else
            {
                foreach (clsICD10 element in this.ChildrenICD10)
                {
                    IEnumerable<clsICD10> children = element.GetBillableCodes();

                    foreach (clsICD10 elementICD10 in children)
                    { yield return elementICD10; }
                }
            }

        }

        public IEnumerable<clsICD10> GetAllBillableCodes()
        {
            try
            {
                List<clsICD10> returned = this.GetBillableCodes().ToList();
                return returned;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public void Dispose()
        {
            if (this.view != null)
            {
                if (view.SortDescriptions != null)
                { view.SortDescriptions.Clear(); }

                this.view = null; 
            }
        }
    }

    public class ICD10RangeCategory
    {
        #region Properties
        public ObservableCollection<clsICD10> ChildrenICD10 { get; set; }

        public string StartCode { get; set; }
        public string EndCode { get; set; }

        public string SearchString { get; set; }
        public bool FoundInSearch { get; set; }

        public string Description { get; set; } 
        #endregion

        #region Constructors
        public ICD10RangeCategory()
        {
            this.ChildrenICD10 = new ObservableCollection<clsICD10>();
            this.FoundInSearch = true;
        } 
        #endregion

        #region Add To Range
        public void AddToRange(clsICD10 ChildICD10)
        {
            if (!this.ChildrenICD10.Contains(ChildICD10))
            {
                this.ChildrenICD10.Add(ChildICD10);
                ChildICD10.CodeRemoved += new clsICD10.ICD10Removed(icdCode_CodeRemoved);
                ChildICD10.RemoveMe += new clsICD10.removeChild(ChildICD10_RemoveMe);
            }
        } 
        #endregion

        #region Code Removing Event Handlers
        void ChildICD10_RemoveMe(clsICD10 ICD10Code)
        {
            if (this.ChildrenICD10.Contains(ICD10Code))
            { this.ChildrenICD10.Remove(ICD10Code); }
        }

        void icdCode_CodeRemoved(clsICD10 ICD10Code)
        {
            if (!ICD10Code.ChildrenICD10.Any() && this.ChildrenICD10.Contains(ICD10Code))
            {
                ICD10Code.CodeRemoved -= icdCode_CodeRemoved;
                ICD10Code.RemoveMe -= ChildICD10_RemoveMe;

                ICD10Code.IsRemoved = true;
                this.ChildrenICD10.Remove(ICD10Code);
            }
        } 
        #endregion
      
        #region Views and Search
        public ICollectionView Level3CategoriesFilteredView
        {
            get
            {
                try
                {
                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(this.ChildrenICD10);
                    if (view != null)
                    {
                        view.Filter = UserFilter;
                        if (!view.SortDescriptions.Any())
                        { view.SortDescriptions.Add(new SortDescription("ICD10CodeWithoutDecimal", ListSortDirection.Ascending)); }
                    }
                    return view;
                }
                catch (Exception Ex)
                { throw Ex; }
               
            }
        }

        public void RefreshSearch()
        {
            try
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(this.ChildrenICD10);
                if (view != null && view.Filter != null) { view.Refresh(); }
            }
            catch (Exception Ex) { throw Ex; }
            
        }

        private bool UserFilter(object item)
        {
            bool bReturned = true;

            try
            {
                if (this.SearchString != null && item is clsICD10 && ((clsICD10)item).CodeType == 1)
                {
                    clsICD10 Code = (clsICD10)item;
                    bReturned = Code.LongDescription.ToLower().Contains(this.SearchString.ToLower()) || Code.ICD10Code.ToLower().Contains(this.SearchString.ToLower());

                }
                else if (this.SearchString != null && item is clsICD10 && ((clsICD10)item).CodeType == 0)
                {
                    clsICD10 Code = (clsICD10)item;
                    bReturned = Code.SearchInChildren(SearchString);
                }
            }
            catch (Exception Ex)
            { throw Ex; }                 
            return bReturned;

        } 
        #endregion
        
    }
    
    public class clsSpeciality
    {
        public long SpecialityID { get; set; }
        public string Description { get; set; }        

        public clsSpeciality(long SpecialityID, string Description)
        {
            this.SpecialityID = SpecialityID;
            this.Description = Description;
        }
    }

    public class CurrentICD10 : INotifyPropertyChanged
    {
        public string ICD10Code { get; set; }
        public Int16 CodeType { get; set; }


        public string ParentCode { get; set; }
        public string ParentCodeWithoutDecimal { get { return this.ParentCode.Replace(".", ""); } }

        public Int64 Speciality { get; set; }
        public string LongDescription { get; set; }
        public string ICD10CodeWithoutDecimal { get { return this.ICD10Code.Replace(".", ""); } }

        private bool _IsChecked = false;
        public bool IsChecked
        {
            get { return this._IsChecked; }
            set { this._IsChecked = value; this.RaiseEvent("IsChecked"); }
        }

        private void RaiseEvent(string PropertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ICD10Notes
    {
        public string ICDCode { get; set; }
        public string Description { get; set; }

        public XElement Includes { get; set; }
        public XElement InclusionTerm { get; set; }

        public XElement UseAdditionalCode { get; set; }
        public XElement Excludes { get; set; }
        
        public XElement Excludes2 { get; set; }
        public XElement CodeFirst { get; set; }

        public XElement CodeAlso { get; set; }

        public XElement Notes { get; set; }

        public IEnumerable<string> IncludesBinding
        {
            get 
            {
                IEnumerable<string> returned = null;

                if (this.Includes != null && this.Includes.Descendants("note").Any())
                { returned = this.Includes.Descendants("note").Select(p => p.Value); }
                //else
                //{
                //    List<string> lstNoData = new List<string>();
                //    lstNoData.Add("<No information available>");
                //    returned = lstNoData.AsEnumerable();
                //}

                return returned;
            }
        }

        public IEnumerable<string> InclusionTermBinding
        {
            get
            {
                IEnumerable<string> returned = null;

                if (this.InclusionTerm != null && this.InclusionTerm.Descendants("note").Any())
                { returned = this.InclusionTerm.Descendants("note").Select(p => p.Value); }
                //else
                //{
                //    List<string> lstNoData = new List<string>();
                //    lstNoData.Add("<No information available>");
                //    returned = lstNoData.AsEnumerable();
                //}
                return returned;
            }
        }

        public IEnumerable<string> UseAdditionalCodeBinding
        {
            get
            {
                IEnumerable<string> returned = null;

                if (this.UseAdditionalCode != null && this.UseAdditionalCode.Descendants("note").Any())
                { returned = this.UseAdditionalCode.Descendants("note").Select(p => p.Value); }
                //else
                //{
                //    List<string> lstNoData = new List<string>();
                //    lstNoData.Add("<No information available>");
                //    returned = lstNoData.AsEnumerable();
                //}
                return returned;
            }
        }

        public IEnumerable<string> ExcludesBinding
        {
            get
            {
                IEnumerable<string> returned = null;

                if (this.Excludes != null && this.Excludes.Descendants("note").Any())
                { returned = this.Excludes.Descendants("note").Select(p => p.Value); }
                //else
                //{
                //    List<string> lstNoData = new List<string>();
                //    lstNoData.Add("<No information available>");
                //    returned = lstNoData.AsEnumerable();
                //}
                return returned;
            }
        }

        public IEnumerable<string> Excludes2Binding
        {
            get
            {
                IEnumerable<string> returned = null;

                if (this.Excludes2 != null && this.Excludes2.Descendants("note").Any())
                { returned = this.Excludes2.Descendants("note").Select(p => p.Value); }
                //else
                //{
                //    List<string> lstNoData = new List<string>();
                //    lstNoData.Add("<No information available>");
                //    returned = lstNoData.AsEnumerable();
                //}
                return returned;
            }
        }

        public IEnumerable<string> CodeFirstBinding
        {
            get
            {
                IEnumerable<string> returned = null;

                if (this.CodeFirst != null && this.CodeFirst.Descendants("note").Any())
                { returned = this.CodeFirst.Descendants("note").Select(p => p.Value); }
                //else
                //{
                //    List<string> lstNoData = new List<string>();
                //    lstNoData.Add("<No information available>");
                //    returned = lstNoData.AsEnumerable();
                //}
                return returned;
            }
        }

        public IEnumerable<string> CodeAlsoBinding
        {
            get
            {
                IEnumerable<string> returned = null;

                if (this.CodeAlso != null && this.CodeAlso.Descendants("note").Any())
                { returned = this.CodeAlso.Descendants("note").Select(p => p.Value); }
                //else
                //{
                //    List<string> lstNoData = new List<string>();
                //    lstNoData.Add("<No information available>");
                //    returned = lstNoData.AsEnumerable();
                //}
                return returned;
            }
        }

        public IEnumerable<string> NotesBinding
        {
            get
            {
                IEnumerable<string> returned = null;

                if (this.Notes != null && this.Notes.Descendants("note").Any())
                { returned = this.Notes.Descendants("note").Select(p => p.Value); }
                
                return returned;
            }
        }

        public bool HasData 
        {
            get
            {
               return Includes != null
                   || InclusionTerm != null
                   || UseAdditionalCode != null
                   || Excludes != null
                   || Excludes2 != null
                   || CodeFirst != null
                   || CodeAlso != null
                   || Notes != null;

            }
         
        }

        public int SortRank { get; set; }
    }

    public class GroupedICD10Notes
    {
        public List<ICD10Notes> CodingRules { get; set; }
        private CollectionView _bindingView {get; set;}

        public CollectionView BindingView
        {
            get             
            {
                if (this._bindingView == null)
                { 
                    this._bindingView = ((CollectionView)CollectionViewSource.GetDefaultView(this.CodingRules));

                    if (this._bindingView != null)
                    {
                        this._bindingView.SortDescriptions.Add(new SortDescription("SortRank", ListSortDirection.Ascending)); 
                        this._bindingView.SortDescriptions.Add(new SortDescription("ICDCode", ListSortDirection.Ascending)); 
                    }
                }
                return this._bindingView;
            }
        }

        public GroupedICD10Notes()
        {
            this.CodingRules = new List<ICD10Notes>();
            //this.BindingView = (CollectionView)CollectionViewSource.GetDefaultView(this.CodingRules);

            //if (this.BindingView != null)
            //{
            //    this.BindingView.SortDescriptions.Add(new SortDescription("ICDCode", ListSortDirection.Ascending));
            //}
        }
    }

    class CodeVisibilityConverter : IMultiValueConverter
    {
        private static BitmapImage GreenIcon = new BitmapImage(new Uri("ICO/ICD10GalleryGreen.ico", UriKind.Relative));
        private static BitmapImage RedIcon = new BitmapImage(new Uri("ICO/ICD10GalleryRed.ico", UriKind.Relative)); 
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BitmapImage returned = null; 
            
            //string returned = string.Empty;
            if (values != null)
            {
                //if (values.Any(p => Convert.ToString(p).Trim() == "0A7321DE72D"))
                //{ 
                //    returned = new BitmapImage(new Uri("ICO/ICD10GalleryYellow.ico",UriKind.Relative)); 
                //}
                //else 
                if (values.Any(p => Convert.ToString(p) == "1"))
                {
                    returned = GreenIcon;
                }
                else 
                {
                    returned = RedIcon;
                }

            }
            else
            {
                returned = RedIcon;
            }
            return returned;

        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        { return null; }
    }
   
}
