using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Data;
using gloUIControlLibrary.Classes.ICD10;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Timers;
using System.Windows.Threading;

namespace gloUIControlLibrary
{
	/// <summary>
	/// Interaction logic for ICDSpecialCodeControl.xaml
	/// </summary>
	public partial class ICDSpecialCodeControl : UserControl
	{
        #region Properties and Variables
        DispatcherTimer searchTimer = null;


        private List<clsSpeciality> lstSpeciality = null;
        private ObservableCollection<CurrentICD10> lstCurrentICD10 = null;

        private string _SearchText = string.Empty;
        public string SearchText
        {
            get
            { return this._SearchText.TrimEnd(' ') ?? string.Empty; }
            set { this._SearchText = value ?? string.Empty; }
        }

        CollectionView view = null;

        public clsSpeciality GetSelectedSpeciality
        {
            get
            {
                clsSpeciality returned = null;

                if (this.cmbSpeciality.SelectedItem != null && this.cmbSpeciality.SelectedItem is clsSpeciality)
                { returned = (clsSpeciality)cmbSpeciality.SelectedItem; }

                return returned;
            }
        } 
        #endregion

        #region Dispose
        public void Dispose()
        {
            try
            {
                if (this.lstCurrentICD10 != null)
                {
                    lstCurrentICD10.Clear();
                    lstCurrentICD10 = null;
                }

                if (this.lstSpeciality != null)
                {
                    this.lstSpeciality.Clear();
                    this.lstSpeciality = null;
                }

                if (this.searchTimer != null)
                {
                    this.searchTimer.Tick -= searchTimer_Tick;
                    this.searchTimer = null;
                }

                if (this.view != null)
                {
                    view.Filter = null;

                    if (this.view.SortDescriptions.Any())
                    { this.view.SortDescriptions.Clear(); }

                    this.view = null;
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        } 
        #endregion

        #region Constructor
        public ICDSpecialCodeControl()
        {
            this.InitializeComponent();

            try
            {
                this.SearchText = string.Empty;

                searchTimer = new DispatcherTimer();
                searchTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                searchTimer.Tick += new EventHandler(searchTimer_Tick);

                this.lstSpeciality = new List<clsSpeciality>();
                this.lstCurrentICD10 = new ObservableCollection<CurrentICD10>();

                this.trvCurrentICDNodes.ItemsSource = this.lstCurrentICD10;
                view = (CollectionView)CollectionViewSource.GetDefaultView(this.lstCurrentICD10);

                if (view != null)
                {
                    view.SortDescriptions.Add(new SortDescription("ICDCode", ListSortDirection.Ascending));
                    view.Filter = UserFilter;
                }
            }
            catch (Exception Ex)
            { throw Ex; }


        } 
        #endregion

        #region Binding
        public ICollectionView ICD10CodeFilteredView
        {
            get
            {
                try
                {
                    this.SearchCurrentCodes(this.SearchText);
                    return this.view;
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }

            }
        } 
        #endregion

        #region Speciality Combobox
        public void BindComboItems(DataTable DataTable)
        {
            try
            {
                lstSpeciality.Clear();

                lstSpeciality = DataTable.AsEnumerable().Select
                    (p => new clsSpeciality
                        (Convert.ToInt64(p["nSpecialtyID"]),
                            Convert.ToString(p["sDescription"]))).ToList();

                this.cmbSpeciality.ItemsSource = lstSpeciality;

                if (lstSpeciality.Any(p => p.SpecialityID == 0))
                { cmbSpeciality.SelectedItem = lstSpeciality.First(p => p.SpecialityID == 0); }


            }
            catch (System.Exception Ex)
            {

                throw Ex;
            }

        }

        private void cmbSpeciality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.SearchCurrentCodes(this.SearchText);
            }
            catch (Exception Ex)
            { throw Ex; }


        } 
        #endregion

        public clsICD10 GetSelectedICDCode()
        {
            clsICD10 returned = null;

            try
            {
                if (trvCurrentICDNodes.SelectedItem != null && trvCurrentICDNodes.SelectedItem is CurrentICD10)
                {
                    CurrentICD10 currentCode = (CurrentICD10)trvCurrentICDNodes.SelectedItem;                                                            
                    returned = new clsICD10(currentCode.ICD10Code, currentCode.LongDescription, currentCode.CodeType, currentCode.ParentCode);
                    currentCode = null;
                }
            }
            catch (Exception Ex)
            {
                
                throw Ex;
            }

            return returned;
        }

        #region Search Functionality
        
        void searchTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                searchTimer.Stop();
                this.SearchText = this.txtSearch.Text;
                this.SearchCurrentCodes(SearchText);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }               


        private void btnClearSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                this.txtSearch.Text = string.Empty;
                this.SearchText = this.txtSearch.Text;
                this.SearchCurrentCodes(this.SearchText);
            }
            catch (Exception Ex)
            { throw Ex; }

        }

        private bool UserFilter(object item)
        {
            bool bReturned = false;

            try
            {
                if (item != null && item is CurrentICD10)
                {
                    CurrentICD10 Code = (CurrentICD10)item;

                    if (this.GetSelectedSpeciality.SpecialityID == 0)
                    {
                        bReturned =
                            Code.LongDescription.ToLower().Contains(this.SearchText.ToLower())
                            ||
                            Code.ICD10Code.ToLower().Contains(this.SearchText.ToLower());
                    }
                    else
                    {
                        bReturned =
                            (
                                Code.LongDescription.ToLower().Contains(this.SearchText.ToLower())
                                ||
                                Code.ICD10Code.ToLower().Contains(this.SearchText.ToLower())
                            )
                            && Code.Speciality == this.GetSelectedSpeciality.SpecialityID;
                    }
                  
                }
                return bReturned;
            }
            catch (Exception Ex) { throw Ex; }

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void SearchCurrentCodes(string Text)
        {
            try
            {
                if (this.view != null) { view.Refresh(); }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        } 
        #endregion

        #region Code Shifting Logic
        //public void AddToCurrent(clsICD10 Code)
        //{
        //    try
        //    {
        //        if (!this.lstCurrentICD10.Any(p => p.ICD10Code == Code.ICD10Code))
        //        {
        //            CurrentICD10 CodeToAdd = new CurrentICD10();
        //            CodeToAdd.CodeType = Code.CodeType;
        //            CodeToAdd.ICD10Code = Code.ICD10Code;
        //            CodeToAdd.LongDescription = Code.LongDescription;
        //            CodeToAdd.Speciality = this.GetSelectedSpeciality.SpecialityID;
        //            CodeToAdd.ParentCode = Code.ParentCode;

        //            this.lstCurrentICD10.Add(CodeToAdd);
        //            CodeToAdd = null;
        //        }
        //    }
        //    catch (Exception Ex)
        //    { throw Ex; }

        //}


        public void BatchAdd(List<clsICD10> CodesList)
        {
            try
            {
                var lstCodes = this.lstCurrentICD10;

                this.lstCurrentICD10 = null;

                foreach (clsICD10 Code in CodesList)
                {
                    if (!lstCodes.Any(p => p.ICD10Code == Code.ICD10Code))
                    {
                        CurrentICD10 CodeToAdd = new CurrentICD10();
                        CodeToAdd.CodeType = Code.CodeType;
                        CodeToAdd.ICD10Code = Code.ICD10Code;
                        CodeToAdd.LongDescription = Code.LongDescription;
                        CodeToAdd.Speciality = this.GetSelectedSpeciality.SpecialityID;
                        CodeToAdd.ParentCode = Code.ParentCode;

                        lstCodes.Add(CodeToAdd);

                        CodeToAdd = null;
                    }                                 
                }

                this.lstCurrentICD10 = lstCodes;

                lstCodes = null;
            }
            catch (Exception Ex) { throw Ex; }
        }

        public void RemoveFromCurrent(string ICDCode)
        {
            try
            {
                if (this.lstCurrentICD10.Any(p => p.ICD10Code == ICDCode))
                { this.lstCurrentICD10.Remove(this.lstCurrentICD10.First(p => p.ICD10Code == ICDCode)); }
            }
            catch (Exception Ex)
            { throw Ex; }


        } 
        #endregion

        public IEnumerable<CurrentICD10> GetAllCodes()
        {
            try
            {
                return this.lstCurrentICD10.AsEnumerable();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        
    }
}