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

namespace gloSurescriptSecureMessage
{
    /// <summary>
    /// Interaction logic for PagingControl.xaml
    /// </summary>
    public partial class PagingControl : UserControl
    {
        public PagingControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PageIndexProperty = DependencyProperty.Register("PageIndex", typeof(int), typeof(PagingControl));
        public static readonly DependencyProperty NumberOfPagesProperty = DependencyProperty.Register("NumberofPages", typeof(int), typeof(PagingControl));

        public int PageIndex
        {
            get
            { return Int32.Parse(GetValue(PageIndexProperty).ToString()); }
            set
            { SetValue(PageIndexProperty, value); }
        }

        public int NumberOfPages
        {
            get { return Int32.Parse(GetValue(NumberOfPagesProperty).ToString()); }
            set { SetValue(NumberOfPagesProperty, value); }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        


        public static readonly RoutedEvent NextPageClick = EventManager.RegisterRoutedEvent("Next", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PagingControl));
        public event RoutedEventHandler NextPage
        {
            add { AddHandler(NextPageClick, value); }
            remove { RemoveHandler(NextPageClick, value); }

        }

        public static readonly RoutedEvent PreviousPageClick = EventManager.RegisterRoutedEvent("Previous", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PagingControl));
        public event RoutedEventHandler PreviousPage
        {
            add { AddHandler(PreviousPageClick, value); }
            remove { RemoveHandler(PreviousPageClick, value); }

        }

        public static readonly RoutedEvent FirstPageClick = EventManager.RegisterRoutedEvent("First", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PagingControl));
        public event RoutedEventHandler FirstPage
        {
            add { AddHandler(FirstPageClick, value); }
            remove { RemoveHandler(FirstPageClick, value); }

        }

        public static readonly RoutedEvent LastPageClick = EventManager.RegisterRoutedEvent("Last", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PagingControl));
        public event RoutedEventHandler LastPage
        {
            add { AddHandler(LastPageClick, value); }
            remove { RemoveHandler(LastPageClick, value); }

        }

        
        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {

            RaiseEvent(new RoutedEventArgs(FirstPageClick));
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(PreviousPageClick));
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(NextPageClick));
        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(LastPageClick));
        }

     

       

    }
}
