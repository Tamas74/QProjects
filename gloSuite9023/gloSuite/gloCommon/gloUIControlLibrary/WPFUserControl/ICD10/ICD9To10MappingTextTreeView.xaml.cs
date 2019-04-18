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

namespace gloUIControlLibrary.WPFUserControl.ICD10
{
    /// <summary>
    /// Interaction logic for ICD9To10MappingTextTreeView.xaml
    /// </summary>
    public partial class ICD9To10MappingTextTreeView : UserControl
    {
        public delegate void ICD10CodeSelectedEvent(string Code);
        public event ICD10CodeSelectedEvent CodeSelected;

        public ICD9To10MappingTextTreeView()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Hyperlink && this.CodeSelected != null)
                {
                    Hyperlink hyperLink = sender as Hyperlink;

                    if (hyperLink.Tag != null)
                    { this.CodeSelected(Convert.ToString(hyperLink.Tag)); }

                    hyperLink = null;
                }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }



        }
    }
}
