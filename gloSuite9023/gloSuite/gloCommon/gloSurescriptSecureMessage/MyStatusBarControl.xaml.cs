using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Input;
using System.Data;
using System.ComponentModel;

namespace gloSurescriptSecureMessage_InBox
{
    public partial class MyStatusBarControl
    {
        public MyStatusBarControl()
        {
            this.InitializeComponent();
        }

        private void ConnectionStatusPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //myConnectionStatusStackPanel.ContextMenu.PlacementTarget = myConnectionStatusStackPanel;
            //myConnectionStatusStackPanel.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Top;
            //ContextMenuService.SetPlacement(myConnectionStatusStackPanel, System.Windows.Controls.Primitives.PlacementMode.Top);
            //myConnectionStatusStackPanel.ContextMenu.IsOpen = true;
        }
    }
}