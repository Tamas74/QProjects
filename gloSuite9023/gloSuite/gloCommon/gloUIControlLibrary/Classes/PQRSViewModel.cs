using System.Collections.Generic;
using System.ComponentModel;
using System.Data; 
using gloUIControlLibrary.WPFForms.PQRS;
namespace gloUIControlLibrary.Classes.PQRS
{
    public class PQRSViewModel : INotifyPropertyChanged
    {
        #region Data

        bool? _isChecked = false;
        PQRSViewModel _parent;

        #endregion // Data

        #region QDCCOdes

        public static List<PQRSViewModel> CreateQDSCodes()
        {

            PQRSViewModel root = new PQRSViewModel("Quality Data Codes");
            root.IsInitiallySelected=true; 
           
        foreach(DataRow dr in frmPQRS._dtpqrs.Rows)   
        {
            root.Children.Add(new PQRSViewModel(dr[0].ToString() + " - " + dr[1].ToString(),dr[0].ToString()));  
        }
            root.Initialize();
            return new List<PQRSViewModel> { root };
        }

        PQRSViewModel(string name,string Code="0")
        {
            this.Name = name;
            this.Code = Code;
            this.Children = new List<PQRSViewModel>();
        }

        void Initialize()
        {
            foreach (PQRSViewModel child in this.Children)
            {
                child._parent = this;
                child.Initialize();
            }
        }

        #endregion // CreateQDSCodes

        #region Properties

        public List<PQRSViewModel> Children { get; private set; }

        public bool IsInitiallySelected { get; private set; }

        public string Name { get; private set; }
        public string Code { get; private set; }
        #region IsChecked

        /// <summary>
        /// Gets/sets the state of the associated UI toggle (ex. CheckBox).
        /// The return value is calculated based on the check state of all
        /// child FooViewModels.  Setting this property to true or false
        /// will set all children to the same check state, and setting it 
        /// to any value will cause the parent to verify its check state.
        /// </summary>
        public bool? IsChecked
        {
            get { return _isChecked; }
            set { this.SetIsChecked(value, true, true); }
        }

        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked)
                return;

            _isChecked = value;

            if (updateChildren && _isChecked.HasValue)
                this.Children.ForEach(c => c.SetIsChecked(_isChecked, true, false));

            if (updateParent && _parent != null)
                _parent.VerifyCheckState();

            this.OnPropertyChanged("IsChecked");
        }

        void VerifyCheckState()
        {
            bool? state = null;
            for (int i = 0; i < this.Children.Count; ++i)
            {
                bool? current = this.Children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }
            this.SetIsChecked(state, false, true);
        }

        #endregion // IsChecked

        #endregion // Properties

        #region INotifyPropertyChanged Members

        void OnPropertyChanged(string prop)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}