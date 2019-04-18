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

namespace gloSurescriptSecureMessage_InBox
{
    public partial class MySidebarControl
    {
        private MyNavigationPaneControl myNavigationPaneControl = null;
        private DockPanel dockPanel = null;
        private MouseButtonEventHandler mbEventHandler = null;
        private DependencyPropertyChangedEventHandler visibilityEventHandler = null;

        public MySidebarControl()
        {
            this.InitializeComponent();
            myNavigationPaneControl = new MyNavigationPaneControl();
        }

        private void myNavigationPaneButton_Click(object sender, RoutedEventArgs e)
        {
            if (myNavigationPaneControl.Parent == null)
            {
                Grid grid = (Grid)this.Parent;
                grid.Children.Add(myNavigationPaneControl);
                myNavigationPaneControl.SetValue(Grid.ColumnSpanProperty, 3);
                myNavigationPaneControl.SetValue(Grid.RowSpanProperty, 2);
                myNavigationPaneControl.HorizontalAlignment = HorizontalAlignment.Left;
                myNavigationPaneControl.VerticalAlignment = VerticalAlignment.Top;
                myNavigationPaneControl.Visibility = Visibility.Hidden;
                if (dockPanel == null)
                {
                    dockPanel = (DockPanel)(((Border)grid.Parent).Parent);
                }
                if (mbEventHandler == null)
                {
                    mbEventHandler = new MouseButtonEventHandler(dockPanel_PreviewMouseLeftButtonUp);
                }
                if (visibilityEventHandler == null)
                {
                    visibilityEventHandler = new DependencyPropertyChangedEventHandler(myNavigationPaneControl_IsVisibleChanged);
                }
            }
            if (myNavigationPaneControl.Visibility == Visibility.Hidden)
            {
                myNavigationPaneButton.SetValue(Button.BackgroundProperty, (Brush)MyApp.Current.Resources["MyOrangeSolidBrush"]);
                myNavigationPaneControl.Margin = new Thickness(27, 32, 0, 0);
                myNavigationPaneControl.Visibility = Visibility.Visible;
                dockPanel.PreviewMouseLeftButtonUp += mbEventHandler;
                myNavigationPaneControl.IsVisibleChanged += visibilityEventHandler;
            }
            else
            {
                hideNavigationPane();
            }
        }

        void myNavigationPaneControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (myNavigationPaneControl.Visibility == Visibility.Hidden)
            {
                hideNavigationPane();
            }
        }

        void dockPanel_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.OriginalSource != myNavigationPaneButton && e.Source != myNavigationPaneControl)
            {
                if (myNavigationPaneControl.Visibility == Visibility.Visible)
                {
                    hideNavigationPane();
                }
            }
        }

        private void hideNavigationPane()
        {
            myNavigationPaneButton.SetValue(Button.BackgroundProperty, (Brush)MyApp.Current.Resources["MyBrightBlueSolidBrush2"]);
            myNavigationPaneControl.Visibility = Visibility.Hidden;
            dockPanel.PreviewMouseLeftButtonUp -= mbEventHandler;
            myNavigationPaneControl.IsVisibleChanged -= visibilityEventHandler;
        }
    }
}