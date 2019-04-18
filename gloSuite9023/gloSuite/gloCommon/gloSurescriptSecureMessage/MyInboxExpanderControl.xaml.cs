using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using gloSurescriptSecureMessage;
using System.Timers;
using System.Windows.Threading;

namespace gloSurescriptSecureMessage_InBox
{
    public class SearchEventArgs : EventArgs
    {        
        public string SearchText { get; set; }
        public InboxSearchType SearchType { get; set; }

        public SearchEventArgs(InboxSearchType SearchType, String SearchText)
        {
            this.SearchType = SearchType;
            this.SearchText = SearchText;
        }
    }

    public partial class MyInboxExpanderControl
    {        
        public delegate void SearchFiredEventHander(object sender, EventArgs e);
        public event SearchFiredEventHander SearchFired;
        DispatcherTimer SearchTimer = null;

        

        public MyInboxExpanderControl()
        {
            this.InitializeComponent();
            this.SearchTimer = new DispatcherTimer();
            this.SearchTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            this.SearchTimer.Tick += new EventHandler(SearchTimer_Tick);            
        }

        void SearchTimer_Tick(object sender, EventArgs e)
        {
            this.SearchTimer.Stop();
            RaiseSearchEvent(SearchType, SearchText);
        }

       

        private void ComboBox_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                ComboBox cbx = ((ComboBox)sender);
                //cbx.SelectionChanged += new RoutedEventHandler(myInboxSearchComboBox_TextChanged);
                if ((bool)e.NewValue && cbx.Text == (string)cbx.ToolTip)
                {
                    //cbx.Text = "";
                    cbx.Foreground = Brushes.Black;
                }
                else if (cbx.Text == "")
                {
                    //cbx.Text = (string)cbx.ToolTip;
                    cbx.Foreground = Brushes.LightGray;
                }
            }
            catch (Exception ex)
            {
                
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }


        private void myInboxSearchComboBox_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (myInboxSearchComboBox.Text != "Search Inbox")
                {
                    this.SearchText = Convert.ToString(myInboxSearchComboBox.Text);
                    this.SearchType = InboxSearchType.General;
                    this.SearchTimer.Start();
                }              
            }
            catch (Exception exSearch)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exSearch.ToString(), false);

            }
        }

        InboxSearchType SearchType = InboxSearchType.General;
        string SearchText = "";

       

        private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                this.SearchTimer.Stop();
                TextBox obj = sender as System.Windows.Controls.TextBox;
                
                if (obj != null)
                {                    
                    if (obj.Name == "General")
                    {
                        SearchType = InboxSearchType.General;
                    }
                    else if (obj.Name == "txtSearchFrom")
                    {
                        SearchType = InboxSearchType.From;
                    }
                    else if (obj.Name == "txtSearchReceived")
                    {
                        SearchType = InboxSearchType.Received;
                    }
                    else if (obj.Name == "txtSearchSubject")
                    {
                        SearchType = InboxSearchType.Subject;
                    }

                    this.SearchText = obj.Text;                    
                    this.SearchTimer.Start();                    
                }
            }
            catch (Exception exSearch)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exSearch.ToString(), false);

            }
        }

        private void RaiseSearchEvent(InboxSearchType SearchType, string SearchText)
        {
            try
            { 
                if (this.SearchFired != null) 
                {                    
                    this.SearchFired(this, new SearchEventArgs(SearchType, SearchText)); 
                } 
            }
            catch (Exception exsearchMails)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(exsearchMails.ToString(), false);
            }
        }

        

       
        //private void Test(InboxSearchType sSearchFor, string sSearchText)
        //{
        //    try
        //    {
        //        this.RaiseSearchEvent(sSearchFor, sSearchText);

        //        DataSet myXmlDataSet = new DataSet();
        //        myXmlDataSet.ReadXml("");
        //        DataView dv = myXmlDataSet.Tables[0].DefaultView;

        //        if (sSearchFor == InboxSearchType.General)
        //        {
     
        //            dv.RowFilter = dv.Table.Columns["From"].ColumnName + " Like '%" + sSearchText + "%' " +
        //                            " OR " + dv.Table.Columns["sTo"].ColumnName + " Like '%" + sSearchText + "%'  " +
        //                            " OR " + dv.Table.Columns["Subject"].ColumnName + " Like '%" + sSearchText + "%'  " +
        //                            " OR " + dv.Table.Columns["Subject"].ColumnName + " Like '%" + sSearchText + "%'  " +
        //                            " OR " + dv.Table.Columns["StatusCode"].ColumnName + " Like '%" + sSearchText + "%'  " +
        //                            " OR " + dv.Table.Columns["StatusDescription"].ColumnName + " Like '%" + sSearchText + "%'  " +
        //                            " OR " + dv.Table.Columns["Received"].ColumnName + " Like '%" + sSearchText + "%'";


        //            if (myXmlDataSet.Tables[0].Columns.Contains("sDelegatedUser"))
        //            { 
        //                dv.RowFilter = dv.RowFilter + " OR " + dv.Table.Columns["sDelegatedUser"].ColumnName + " Like '%" + sSearchText + "%'"; 
        //            }
                    
        //        }
        //        else if (sSearchFor == InboxSearchType.From)
        //        {
        //            dv.RowFilter = dv.Table.Columns["From"].ColumnName + " Like '%" + sSearchText + "%' ";
        //        }
        //        else if (sSearchFor == InboxSearchType.Received)
        //        {
        //            dv.RowFilter = dv.Table.Columns["Received"].ColumnName + " Like '%" + sSearchText + "%' ";
        //        }
        //        else if (sSearchFor == InboxSearchType.Subject)
        //        {
        //            dv.RowFilter = dv.Table.Columns["Subject"].ColumnName + " Like '%" + sSearchText + "%' ";
        //        }

        //        var dsInbox = new DataSet("Inbox");
        //        var dtMails = dv.ToTable();
        //        dsInbox.Tables.Add(dtMails);

        //        dsInbox.WriteXml(gloSettings.FolderSettings.AppFolderPath + "SearchInbox.xml", XmlWriteMode.IgnoreSchema);

        //        Window parentWindow = Window.GetWindow(this);
        //        var provider = (XmlDataProvider)parentWindow.Resources["inboxDS"];
        //        provider.Source = new Uri(gloSettings.FolderSettings.AppFolderPath + "SearchInbox.xml", UriKind.Absolute);
        //        provider.Refresh();


        //    }
        //    catch (Exception exsearchMails)
        //    {

        //        gloAuditTrail.gloAuditTrail.ExceptionLog(exsearchMails.ToString(), false);
        //    }
        //}

       
        
    }
}