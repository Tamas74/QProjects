using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace gloSurescriptSecureMessage_InBox
{
    public partial class MyNavigationPaneControl
    {
        public MyNavigationPaneControl()
        {
            this.InitializeComponent();

            // Insert code required on object creation below this point.
        }

        private void myTreeView_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.OriginalSource is TextBlock || e.OriginalSource is Image)
            {
                this.Visibility = Visibility.Hidden;
            }
        }
    }
}