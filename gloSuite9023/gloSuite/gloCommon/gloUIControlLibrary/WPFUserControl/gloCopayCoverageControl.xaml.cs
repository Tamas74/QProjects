using System.Windows.Controls;
using System.Windows.Navigation;
using System.Diagnostics;

namespace gloUIControlLibrary.WPFUserControl
{
    /// <summary>
    /// Interaction logic for gloCopayControl.xaml
    /// </summary>
    public partial class gloCopayControl : UserControl
    {
        public gloCopayControl()
        {
            InitializeComponent();

            
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
