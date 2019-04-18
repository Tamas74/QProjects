using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloBilling.C1GridFilter
{
    class AccountLogTypeFilter : C1.Win.C1FlexGrid.IC1ColumnFilter
    {

        #region "Class Variables"

        List<String> _filters = new List<String>();

        public List<String> Filters
        {
            get { return _filters; }
        } 

        #endregion

        #region "Properties"

        public bool HoldChecked { get; set; }
        public bool InsuranceRemit { get; set; }
        public bool Notes { get; set; } 

        #endregion

        #region "IC1ColumnFilter Implementations"

        bool C1.Win.C1FlexGrid.IC1ColumnFilter.Apply(object value)
        {
            if (value != null)
            {
                foreach (string selected in _filters)
                {
                    if (value.ToString().Equals(selected)) { return true; }
                }
            }

            return false;
        }

        C1.Win.C1FlexGrid.IC1ColumnFilterEditor C1.Win.C1FlexGrid.IC1ColumnFilter.GetEditor()
        {
            return new AccountLogTypeFilterEditor();
        }

        bool C1.Win.C1FlexGrid.IC1ColumnFilter.IsActive
        {
            get { return _filters.Count > 0; }
        }

        void C1.Win.C1FlexGrid.IC1ColumnFilter.Reset()
        {
            _filters.Clear();
            Notes = false;
        } 

        #endregion

    }
}
