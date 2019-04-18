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
using System.Data;

namespace gloUIControlLibrary.WPFUserControl
{
    /// <summary>
    /// Interaction logic for gloFormularyAlternatives.xaml
    /// </summary>
    public partial class gloFormularyAlternatives : UserControl
    {
        public delegate void AlternativeAccepted(object value);
        public event AlternativeAccepted DrugAccepted;

        public delegate void DrugSelected(object value);
        public event DrugSelected DrugSelectedEvent;


        //public gloFormularyAlternatives()
        //{
        //    InitializeComponent();
        //}

        public gloFormularyAlternatives(Boolean ShowNDCInAlternatives)
        {
            InitializeComponent();
            if (ShowNDCInAlternatives)
            { this.colNDC.Visibility = System.Windows.Visibility.Visible; }
            else
            { this.colNDC.Visibility = System.Windows.Visibility.Collapsed; }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (this.DrugAccepted != null)
            {
                if (dataGrid1.SelectedItem != null)
                { this.DrugAccepted(dataGrid1.SelectedItem); }
            }

            
                                      
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        { this.RefreshSearch(); }

        public void RefreshSearch()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(dataGrid1.ItemsSource);
            if (this.txtFilter != null) { this.txtFilter.Text = string.Empty; }
            if (view != null) { view.Filter = UserFilter; }
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
            {
                dynamic myObj = item;
                string drugName = myObj.DrugName;

                return (drugName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

            }
        }

        private void dataGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(dataGrid1);
                if (scrollViewer != null)
                {
                    scrollViewer.ScrollToTop();
                }
            }
            catch
            {
            }
        }
        private static T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dataGrid1.ItemsSource).Refresh();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DrugSelectedEvent != null)
            {
                if (dataGrid1.SelectedItem != null)
                { this.DrugSelectedEvent(dataGrid1.SelectedItem); }
            }

            
        }
    }
}
