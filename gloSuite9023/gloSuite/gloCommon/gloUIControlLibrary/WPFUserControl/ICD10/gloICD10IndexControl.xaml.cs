using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using System.Xml;

namespace gloUIControlLibrary.WPFUserControl.ICD10
{
    /// <summary>
    /// Interaction logic for gloICD10IndexControl.xaml
    /// </summary>
    public partial class gloICD10IndexControl : UserControl
    {
        public delegate void ItemDoubleClicked(XmlElement XmlElement);
        public event ItemDoubleClicked DoubleClicked;

        public gloICD10IndexControl()
        {
            InitializeComponent();
        }

        public void SetBinding(XmlDataProvider XDocument)
        {
            try
            {
                if (this.trvICD != null)
                {
                    this.trvICD.DataContext = XDocument;
                }
            }
            catch (System.Exception Ex)
            {
                
                throw Ex;
            }
   
           
        }

        private void trvICD_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (this.DoubleClicked != null && sender is TreeViewItem)
                {
                    if (((TreeViewItem)sender).IsSelected)
                    {
                        if (trvICD != null && trvICD.SelectedItem != null && trvICD.SelectedItem is XmlElement)
                        {
                            XmlElement xSelectedElement = (XmlElement)trvICD.SelectedItem;
                            if (xSelectedElement.Name == "sectionRef")
                            {
                                this.DoubleClicked(xSelectedElement);
                            }
                        }
                    }
                }
            }
            catch (System.Exception Ex)
            { throw Ex; }

            

            
        }

      
    }
}
