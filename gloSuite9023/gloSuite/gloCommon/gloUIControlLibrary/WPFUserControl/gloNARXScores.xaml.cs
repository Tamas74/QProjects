using System.Windows.Controls;
using System.Windows.Navigation;
using System.Diagnostics;

namespace gloUIControlLibrary.WPFUserControl
{
    /// <summary>
    /// Interaction logic for gloCopayControl.xaml
    /// </summary>
    public partial class gloNARXScores : UserControl
    {
        public delegate void HyperlinkClicked();
        public event HyperlinkClicked DrugAccepted;

        public gloNARXScores()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DrugAccepted != null)
            {
                this.DrugAccepted();
            }
        }

    }   
}
