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
using gloUIControlLibrary.Classes.ICD10;

namespace gloUIControlLibrary.WPFUserControl.ICD10
{
    /// <summary>
    /// Interaction logic for ICD10DataSetTreeView.xaml
    /// </summary>
    public partial class ICD10DataSetTreeView : UserControl
    {
 //       DataRow SelectedRow = null;

        public delegate void Itemchanged(DataRow DataRow);
        public event Itemchanged SelectedItemChanged;

        public ICD10DataSetTreeView()
        {
            InitializeComponent();
        }

        private void treeView1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (
                    treeView1.SelectedItem != null 
                    && treeView1.SelectedItem is System.Data.DataRowView
                    && this.SelectedItemChanged != null)
                {
                    DataRow dRow = ((System.Data.DataRowView)(treeView1.SelectedItem)).Row;//["sICDCode"]

                    if (dRow != null)
                    { this.SelectedItemChanged(dRow); }
                                        
                    dRow = null;
                }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }            
        }
       
    }
}
