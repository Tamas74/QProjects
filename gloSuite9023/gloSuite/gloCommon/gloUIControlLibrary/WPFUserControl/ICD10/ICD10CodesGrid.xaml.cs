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
using gloUIControlLibrary.Classes.ICD10;
using System.Data;
using System.Windows.Controls.Primitives;

namespace gloUIControlLibrary.WPFUserControl.ICD10
{
    /// <summary>
    /// Interaction logic for ICD10CodesGrid.xaml
    /// </summary>
    public partial class ICD10CodesGrid : UserControl
    {
        #region Delegates
        public delegate void codeSelected(DataRow Datarow);
        public event codeSelected ICD9CodeSelected; 
        #endregion

        #region Dependency Property
        public int ICD10CodesGridColumnWidth
        {
            get { return (int)GetValue(ICD10CodesGridColumnWidthProperty); }
            set { SetValue(ICD10CodesGridColumnWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ICD10CodesGridColumnWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ICD10CodesGridColumnWidthProperty =
            DependencyProperty.Register("ICD10CodesGridColumnWidth", typeof(int), typeof(ICD10CodesGrid), new UIPropertyMetadata(default(int), OnColumnWidthPropertyChanged));

        private static void OnColumnWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        { if (d != null && d is ICD10CodesGrid) { (d as ICD10CodesGrid).ChangeControlDisplay((int)e.NewValue); } }

        public void ChangeControlDisplay(int value)
        {
            this.columnDescription.Width = new DataGridLength(value);
            this.Height = value;
        } 
        #endregion

        #region Constructor
        public ICD10CodesGrid()
        { InitializeComponent(); } 
        #endregion

        #region Event Raising
        private void RaiseCodeSelectedEvent()
        {
            try
            {
                if (this.ICD9CodeSelected != null && dataGridCodes.SelectedItem != null && dataGridCodes.SelectedItem is System.Data.DataRowView)
                {
                    DataRow dRow = ((System.Data.DataRowView)(dataGridCodes.SelectedValue)).Row;
                    this.ICD9CodeSelected(dRow);
                    dRow = null;
                }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }


        } 
        #endregion

        #region Data Grid Double Click Event
        private void dataGrid1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dataGridCodes.SelectedValue is System.Data.DataRowView && e.ClickCount == 2)
                { this.RaiseCodeSelectedEvent(); }

            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        } 
        #endregion

        #region DataGrid Focus
        public void SetFocus()
        {
            try
            {
                if (this.dataGridCodes != null && this.dataGridCodes.HasItems)
                { dataGridCodes.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next)); }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        }

        public void SetFocusToFirstRow()
        {
            try
            {
                if (this.dataGridCodes != null && this.dataGridCodes.HasItems)
                { dataGridCodes.SelectedItem = dataGridCodes.Items[0]; }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        } 
        #endregion

        #region Data Grid Key Hits
        private void dataGridCodes_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (
                    e.Key == Key.Enter
                    &&
                    this.dataGridCodes != null
                    &&
                    this.dataGridCodes.HasItems
                    &&
                    dataGridCodes.SelectedValue is System.Data.DataRowView)
                {
                    this.RaiseCodeSelectedEvent();
                }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        }

        private void dataGridCodes_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            { if (e.Key == Key.Enter) { e.Handled = true; } }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
        } 
        #endregion
       
    }
}
