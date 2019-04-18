using System.Windows;
using gloUIControlLibrary.Classes.ClaimRules;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace gloUIControlLibrary.WPFForms
{
    /// <summary>
    /// Interaction logic for frmTriggeredRules.xaml
    /// </summary>
    public partial class frmTriggeredRules : Window
    {

        ICollectionView view;
        Boolean isTestingRule = false;

        public frmTriggeredRules()
        {
            InitializeComponent();
            
        }

        public frmTriggeredRules(List<TriggeredRuleInfo> TriggeredRulesList,bool Testing)
        {
            InitializeComponent();
            TriggeredRuleInfoObservable myList = new TriggeredRuleInfoObservable();
            isTestingRule = Testing;
            try
            {

                foreach (TriggeredRuleInfo ruleInfo in TriggeredRulesList)
                {
                    myList.TriggeredRuleInfoList.Add(ruleInfo);
                }

                view = CollectionViewSource.GetDefaultView(myList.TriggeredRuleInfoList);
                view.SortDescriptions.Add(new SortDescription("RuleTypeInfo", ListSortDirection.Ascending));
                this.lbTriggeredRules.ItemsSource = view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        public frmTriggeredRules(List<TriggeredRuleInfo> TriggeredRulesList)
        {
            InitializeComponent();
            TriggeredRuleInfoObservable myList = new TriggeredRuleInfoObservable();

            try
            {

                foreach (TriggeredRuleInfo ruleInfo in TriggeredRulesList)
                {
                    myList.TriggeredRuleInfoList.Add(ruleInfo);
                }

                view = CollectionViewSource.GetDefaultView(myList.TriggeredRuleInfoList);
                view.SortDescriptions.Add(new SortDescription("RuleTypeInfo", ListSortDirection.Ascending));
                this.lbTriggeredRules.ItemsSource = view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isTestingRule)
            {
                btnVerified.Visibility = Visibility.Visible;
                btnOK.Visibility = Visibility.Collapsed;
                btnClose.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnVerified.Visibility = Visibility.Collapsed;
                btnOK.Visibility = Visibility.Visible;
                btnClose.Visibility = Visibility.Visible;
            }
            //ContentPresenter contentPresenter = FindVisualChild <ContentPresenter>(lbTriggeredRules);
            //DataTemplate dtemplate = contentPresenter.ContentTemplate;
            //MediaElement mediaEle = dtemplate.FindName("stkpMain", contentPresenter) as MediaElement;
            //if (mediaEle!=null)
            //{
                
            //}
        }
        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnVerified_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

       

        
    }
}
